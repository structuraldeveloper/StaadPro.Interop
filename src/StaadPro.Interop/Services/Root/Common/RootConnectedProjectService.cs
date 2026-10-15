using System;
using StaadPro.Interop.Models.Root;
using StaadPro.Interop.Services.Interop;

namespace StaadPro.Interop.Services.Root.Common
{
    internal static class RootConnectedProjectService
    {
        internal static ConnectedProjectResult GetConnectedProjectInfo(object root)
        {
            string nonce = Guid.NewGuid().ToString("N");
            string idSentinel = "<StaadPro.Interop.UnwrittenProjectId." + nonce + ">";
            string nameSentinel = "<StaadPro.Interop.UnwrittenProjectName." + nonce + ">";
            object projectId = idSentinel, projectName = nameSentinel;
            object returned = ((dynamic)root).GetCONNECTEDProjectInfo(ref projectId, ref projectName);
            bool success = NativeValues.ReadBoolean(returned, "CONNECTED project query flag");
            if (!success) return new ConnectedProjectResult(false, null);

            string id = NativeValues.ReadText(projectId, "CONNECTED project ID");
            string name = NativeValues.ReadText(projectName, "CONNECTED project name");
            if (id == idSentinel || name == nameSentinel)
                throw new InvalidOperationException("OpenSTAAD GetCONNECTEDProjectInfo left successful outputs unwritten.");
            return new ConnectedProjectResult(true, new ConnectedProjectInfo(id, name));
        }

        internal static bool SetConnectedProjectInfo(object root, string projectId, string projectName)
        {
            if (projectId == null) throw new ArgumentNullException(nameof(projectId));
            if (projectName == null) throw new ArgumentNullException(nameof(projectName));
            if (string.IsNullOrWhiteSpace(projectId)) throw new ArgumentException("The project ID must contain text.", nameof(projectId));
            if (string.IsNullOrWhiteSpace(projectName)) throw new ArgumentException("The project name must contain text.", nameof(projectName));
            object returned = ((dynamic)root).SetCONNECTEDProjectInfo(projectId, projectName);
            return NativeValues.ReadBoolean(returned, "CONNECTED project assignment flag");
        }
    }
}
