using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        /// <returns>A wrapper with Geometry and Load available, or null if connection fails or times out.</returns>
        /// <remarks>
        /// With a path, attaches to that model if already running; otherwise opens it (or creates a blank
        /// model for a missing file). Waits up to two minutes for the matching model and both interfaces.
        /// Call on an STA thread and serialize subsequent COM access.
        /// </remarks>
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
            catch (Exception ex)
            {
                Trace.TraceWarning("Could not acquire OpenSTAAD for '{0}': {1}", fileFullPath, ex.Message);
                return null;
            }
        }

        internal OpenStaadWrapper GetRunning()
        {
            try
            {
                IList<object> running = RotHelpers.GetRunningOpenStaadInstances();
                return running.Count > 0
                    ? OpenStaadWrapperHelpers.GetWhenRunningInstancesExist(null, running)
                    : null;
            }
            catch (Exception ex)
            {
                Trace.TraceWarning("Could not attach to running OpenSTAAD: {0}", ex.Message);
                return null;
            }
        }
    }
}
