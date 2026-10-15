namespace StaadPro.Interop.Interfaces
{
    /// <summary>Lets a consuming application supply file selection and user notifications without a UI dependency.</summary>
    public interface IStaadInteropInteractionService
    {
        /// <summary>Requests an existing STAAD model path from the host.</summary>
        /// <returns>The selected path, or null when no selection is made.</returns>
        string SelectExistingStaadModelFile();
        /// <summary>Requests a destination for a new STAAD model from the host.</summary>
        /// <param name="fileName">Suggested file name.</param>
        /// <param name="initialDirectory">Suggested directory, or null for the host default.</param>
        /// <returns>The selected path, or null when cancelled.</returns>
        string SelectNewStaadModelFile(string fileName, string initialDirectory = null);
        /// <summary>Requests a directory from the host.</summary>
        /// <param name="currentDirectoryPath">Suggested directory, or null for the host default.</param>
        /// <returns>The selected directory, or null when cancelled.</returns>
        string SelectDirectory(string currentDirectoryPath = null);
        /// <summary>Requests an informational notification from the host.</summary>
        /// <param name="message">Notification text.</param>
        /// <param name="title">Notification title.</param>
        void ShowInformation(string message, string title);
        /// <summary>Requests a warning notification from the host.</summary>
        /// <param name="message">Notification text.</param>
        /// <param name="title">Notification title.</param>
        void ShowWarning(string message, string title);
        /// <summary>Requests an error notification from the host.</summary>
        /// <param name="message">Notification text.</param>
        /// <param name="title">Notification title.</param>
        void ShowError(string message, string title);
    }
}
