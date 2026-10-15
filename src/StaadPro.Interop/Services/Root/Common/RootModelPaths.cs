using System;
using System.IO;
namespace StaadPro.Interop.Services.Root.Common
{
    internal static class RootModelPaths
    {
        internal static string Normalize(string path, string parameterName)
        {
            if (path == null) throw new ArgumentNullException(parameterName);
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException("The model path must contain text.", parameterName);
            return Path.GetFullPath(path);
        }
    }
}
