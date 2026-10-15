using StaadPro.Interop.Interfaces;

namespace StaadPro.Interop.Services
{
    /// <summary>Supplies the default host interaction service captured by new wrappers.</summary>
    public static class StaadInteropHostServices
    {
        private static readonly IStaadInteropInteractionService Fallback = new NullInteractionService();
        private static IStaadInteropInteractionService interaction = Fallback;
        /// <summary>Gets or sets the host service; assigning null restores the noninteractive fallback.</summary>
        /// <remarks>The fallback cancels selections and ignores notifications. Existing wrappers keep their captured service.</remarks>
        public static IStaadInteropInteractionService Interaction { get => interaction; set => interaction = value ?? Fallback; }

        private sealed class NullInteractionService : IStaadInteropInteractionService
        {
            public string SelectExistingStaadModelFile() => null;
            public string SelectNewStaadModelFile(string fileName, string initialDirectory = null) => null;
            public string SelectDirectory(string currentDirectoryPath = null) => null;
            public void ShowInformation(string message, string title) { }
            public void ShowWarning(string message, string title) { }
            public void ShowError(string message, string title) { }
        }
    }
}
