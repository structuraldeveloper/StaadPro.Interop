using System;

namespace StaadPro.Interop.Models.Root
{
    /// <summary>Contains the native success flag and qualified data from one CONNECTED project query.</summary>
    /// <remarks>False is an ordinary native query response, distinct from COM/binder exceptions.
    /// Successful retrieval does not establish cloud synchronization, credentials, membership or that an association remains current.</remarks>
    public sealed class ConnectedProjectResult
    {
        internal ConnectedProjectResult(bool isSuccessful, ConnectedProjectInfo project)
        {
            if (isSuccessful != (project != null))
                throw new ArgumentException("Project data must be present exactly when the query succeeded.", nameof(project));
            IsSuccessful = isSuccessful;
            Project = project;
        }

        /// <summary>Gets the verified success flag reported by native GetCONNECTEDProjectInfo.</summary>
        /// <value>True for native Boolean true or integral one; false for Boolean false or integral zero.</value>
        public bool IsSuccessful { get; }

        /// <summary>Gets project data validated after a successful native response.</summary>
        /// <value>Non-null ConnectedProjectInfo when IsSuccessful is true; null on native false, even if partial outputs were written.</value>
        public ConnectedProjectInfo Project { get; }
    }
}
