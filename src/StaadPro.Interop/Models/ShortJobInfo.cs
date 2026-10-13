using StaadPro.Interop.Common;

namespace StaadPro.Interop.Models
{
    /// <summary>Editable managed snapshot of a STAAD model's short job information.</summary>
    /// <remarks>Changes raise property notifications but do not write to STAAD. Text is stored unchanged;
    /// factory callers may store null, while successful native queries return string values.</remarks>
    public class ShortJobInfo : ValidatablePropertyStore
    {
        /// <summary>Creates an uninitialized managed job record; text properties initially contain null.</summary>
        public ShortJobInfo() { }
        /// <summary>Gets or sets the job name, preserving native text and whitespace.</summary>
        public string Name { get => Get<string>(); set => Set(value); }
        /// <summary>Gets or sets the job client text.</summary>
        public string Client { get => Get<string>(); set => Set(value); }
        /// <summary>Gets or sets the originating engineer's name.</summary>
        public string Originator { get => Get<string>(); set => Set(value); }
        /// <summary>Creates a fresh short job snapshot from supplied text without native calls.</summary>
        /// <param name="name">Job name to store unchanged, including null or empty text.</param>
        /// <param name="client">Client text to store unchanged.</param>
        /// <param name="originator">Originating engineer text to store unchanged.</param>
        /// <returns>A new independent ShortJobInfo object.</returns>
        /// <remarks>The factory has no native side effects; editing the returned record changes managed data only.</remarks>
        public static ShortJobInfo From(string name, string client, string originator)
            => new ShortJobInfo { Name = name, Client = client, Originator = originator };
    }
}
