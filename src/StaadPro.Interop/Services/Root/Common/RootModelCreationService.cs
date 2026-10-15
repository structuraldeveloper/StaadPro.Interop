using System;
using System.IO;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Services.Root.Common
{
    internal static class RootModelCreationService
    {
        internal static void NewSTAADFile(object root, string filePath, LengthInputUnit lengthUnit,
            ForceInputUnit forceUnit, Func<string> readCurrentPath)
        {
            int lengthCode = RootModelEditService.ValidateLengthUnit(lengthUnit);
            int forceCode = RootModelEditService.ValidateForceUnit(forceUnit);
            ValidateDestination(filePath);
            if (!string.IsNullOrWhiteSpace(readCurrentPath()))
                throw new InvalidOperationException("Creating a model requires an unnamed STAAD session. Close or save the current model explicitly first.");
            // Recheck after the native identity read, without claiming atomic no-overwrite.
            RequireAbsentTarget(filePath);
            ((dynamic)root).NewSTAADFile(filePath, lengthCode, forceCode);
        }

        private static void ValidateDestination(string filePath)
        {
            if (!string.Equals(Path.GetExtension(filePath), ".std", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("The new model path must end in .std.", nameof(filePath));
            string filename = Path.GetFileName(filePath);
            if (filename.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                throw new ArgumentException("The filename contains an invalid character.", nameof(filePath));
            string deviceName = filename.Split('.')[0].TrimEnd(' ');
            if (IsDeviceName(deviceName))
                throw new ArgumentException("A Windows device name cannot identify a new model.", nameof(filePath));
            var attributes = File.GetAttributes(Path.GetDirectoryName(filePath));
            if ((attributes & FileAttributes.Directory) == 0)
                throw new IOException("The model's parent path must be an existing directory.");
            RequireAbsentTarget(filePath);
        }

        private static bool IsDeviceName(string name)
        {
            if (string.Equals(name, "CON", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(name, "PRN", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(name, "AUX", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(name, "NUL", StringComparison.OrdinalIgnoreCase)) return true;
            if (name.Length != 4) return false;
            bool port = name.StartsWith("COM", StringComparison.OrdinalIgnoreCase) ||
                name.StartsWith("LPT", StringComparison.OrdinalIgnoreCase);
            return port && ("123456789\u00b9\u00b2\u00b3".IndexOf(name[3]) >= 0);
        }

        private static void RequireAbsentTarget(string filePath)
        {
            try { File.GetAttributes(filePath); }
            catch (FileNotFoundException) { return; }
            throw new IOException("The new model target already exists. Existing files and directories are not accepted.");
        }
    }
}
