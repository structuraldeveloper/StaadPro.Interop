using System;
using System.IO;
using System.Text;

namespace StaadPro.Interop.Services
{
    internal interface IRootModelFiles
    {
        void ValidateExistingFile(string path);
        bool IsLocked(string path);
        void WriteAtomically(string path, string[] lines);
    }

    internal sealed class RootModelFiles : IRootModelFiles
    {
        public void ValidateExistingFile(string path)
        {
            var attributes = File.GetAttributes(path);
            if ((attributes & FileAttributes.Directory) != 0) throw new IOException("A model file, not a directory, is required.");
            if ((attributes & FileAttributes.ReadOnly) != 0) throw new UnauthorizedAccessException("The model file is read-only.");
        }

        public bool IsLocked(string path)
        {
            try
            {
                using (new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None)) return false;
            }
            catch (IOException error) when ((error.HResult & 0xffff) == 32 || (error.HResult & 0xffff) == 33)
            {
                return true;
            }
        }

        public void WriteAtomically(string path, string[] lines)
        {
            string temporary = Path.Combine(Path.GetDirectoryName(path), ".StaadRoot." + Guid.NewGuid().ToString("N") + ".tmp");
            bool ownsTemporary = false;
            try
            {
                using (var stream = new FileStream(temporary, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                {
                    ownsTemporary = true;
                    using (var writer = new StreamWriter(stream, new UTF8Encoding(false)))
                        foreach (string line in lines) writer.WriteLine(line);
                }
                // Existing targets only. A replacement failure never falls back to truncating the target.
                File.Replace(temporary, path, null);
            }
            finally
            {
                if (ownsTemporary)
                {
                    try { File.Delete(temporary); }
                    catch (IOException) { }
                    catch (UnauthorizedAccessException) { }
                }
            }
        }
    }
}
