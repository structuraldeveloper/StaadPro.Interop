using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using StaadPro.Interop.Helpers;
using StaadPro.Interop.Interfaces;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Services
{
    /// <summary>
    /// Provides factory methods to acquire an <see cref="OpenStaadWrapper"/> instance across various attachment and creation strategies.
    /// </summary>
    public static class OpenStaadWrapperProvider
    {
        private static IOpenStaadWrapperResolver _resolver = new DefaultOpenStaadWrapperResolver();

        /// <summary>
        /// Gets or sets the resolver used by <see cref="Get(string)"/>.
        /// </summary>
        public static IOpenStaadWrapperResolver Resolver
        {
            get => _resolver;
            set => _resolver = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Attempts to acquire an <see cref="OpenStaadWrapper"/> for interacting with STAAD via OpenSTAAD.
        /// </summary>
        /// <param name="fileFullPath">Optional full path to a STAAD (.std) file.</param>
        /// <returns>An <see cref="OpenStaadWrapper"/> instance.</returns>
        public static OpenStaadWrapper Get(string fileFullPath = null)
        {
            return Resolver.Get(fileFullPath);
        }

        /// <summary>
        /// Attaches to an already-running STAAD.Pro session without launching STAAD or creating a new model.
        /// </summary>
        /// <returns>An <see cref="OpenStaadWrapper"/> instance attached to the running STAAD session.</returns>
        public static OpenStaadWrapper GetRunning()
        {
            if (Resolver is DefaultOpenStaadWrapperResolver defaultResolver)
            {
                return defaultResolver.GetRunning();
            }

            return Resolver.Get();
        }
    }

    /// <summary>
    /// Default resolver implementation that queries the Running Object Table (ROT) or starts STAAD.
    /// </summary>
    internal sealed class DefaultOpenStaadWrapperResolver : IOpenStaadWrapperResolver
    {
        public OpenStaadWrapper Get(string fileFullPath = null)
        {
            try
            {
                IList<object> runningInstances = RotHelpers.GetRunningOpenStaadInstances();

                if (runningInstances.Count > 0)
                {
                    return OpenStaadWrapperHelpers.GetWhenRunningInstancesExist(fileFullPath, runningInstances);
                }
                else
                {
                    return OpenStaadWrapperHelpers.GetWhenNoRunningInstancesExist(fileFullPath);
                }
            }
            catch
            {
                return new OpenStaadWrapper(null, false);
            }
        }

        internal OpenStaadWrapper GetRunning()
        {
            try
            {
                object active = Marshal.GetActiveObject("StaadPro.OpenSTAAD");
                return new OpenStaadWrapper(active, isDedicated: false);
            }
            catch
            {
                IList<object> running = RotHelpers.GetRunningOpenStaadInstances();
                if (running.Count > 0)
                {
                    return new OpenStaadWrapper(running[0], isDedicated: false);
                }

                return new OpenStaadWrapper(null, false);
            }
        }
    }
}
