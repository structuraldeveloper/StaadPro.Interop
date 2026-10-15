using System;

namespace StaadPro.Interop.Models.Root
{
    /// <summary>Contains immutable CONNECTED project identifier and name text.</summary>
    /// <remarks>Preserves the supplied strings without trimming, GUID conversion, login or network access.
    /// A native successful query may supply empty text, so this data model allows it; the assignment API separately rejects blank fields.</remarks>
    public sealed class ConnectedProjectInfo
    {
        /// <summary>Creates a project-information value from the supplied text.</summary>
        /// <param name="projectId">Non-null identifier text, stored unchanged; empty text is allowed in a data snapshot.</param>
        /// <param name="projectName">Non-null project name, stored unchanged; empty text is allowed in a data snapshot.</param>
        /// <exception cref="ArgumentNullException">Either text argument is null.</exception>
        public ConnectedProjectInfo(string projectId, string projectName)
        {
            ProjectId = projectId ?? throw new ArgumentNullException(nameof(projectId));
            Name = projectName ?? throw new ArgumentNullException(nameof(projectName));
        }

        /// <summary>Gets the CONNECTED project identifier exactly as supplied.</summary>
        /// <value>Non-null text; no GUID syntax or cloud identity has been verified.</value>
        public string ProjectId { get; }

        /// <summary>Gets the CONNECTED project name exactly as supplied.</summary>
        /// <value>Non-null text, including original whitespace, Unicode and an explicitly supplied empty name.</value>
        public string Name { get; }
    }
}
