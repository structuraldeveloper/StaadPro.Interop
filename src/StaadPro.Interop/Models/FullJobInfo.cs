using System;
using System.Globalization;

namespace StaadPro.Interop.Models
{
    /// <summary>Editable managed snapshot of all thirteen STAAD job-information fields.</summary>
    /// <remarks>Inherits Name, Client and Originator from ShortJobInfo. Empty dates are represented by
    /// DateTime.MinValue; nonempty dates use the calling thread's CurrentCulture. Edits do not update STAAD.</remarks>
    public class FullJobInfo : ShortJobInfo
    {
        /// <summary>Creates an uninitialized record with null text and DateTime.MinValue dates.</summary>
        public FullJobInfo() { }
        /// <summary>Gets or sets the engineer date; DateTime.MinValue represents an unspecified date.</summary>
        public DateTime Originated { get => Get<DateTime>(); set => Set(value); }
        /// <summary>Gets or sets the checker name.</summary>
        public string Checker { get => Get<string>(); set => Set(value); }
        /// <summary>Gets or sets the checked date; DateTime.MinValue represents an unspecified date.</summary>
        public DateTime Checked { get => Get<DateTime>(); set => Set(value); }
        /// <summary>Gets or sets the approver name.</summary>
        public string Approver { get => Get<string>(); set => Set(value); }
        /// <summary>Gets or sets the approved date; DateTime.MinValue represents an unspecified date.</summary>
        public DateTime Approved { get => Get<DateTime>(); set => Set(value); }
        /// <summary>Gets or sets the job number text.</summary>
        public string Number { get => Get<string>(); set => Set(value); }
        /// <summary>Gets or sets the revision text.</summary>
        public string Revision { get => Get<string>(); set => Set(value); }
        /// <summary>Gets or sets the job part text.</summary>
        public string Part { get => Get<string>(); set => Set(value); }
        /// <summary>Gets or sets the job reference text.</summary>
        public string Reference { get => Get<string>(); set => Set(value); }
        /// <summary>Gets or sets comments, preserving whitespace, Unicode and line breaks.</summary>
        public string Comments { get => Get<string>(); set => Set(value); }

        /// <summary>Creates a fresh full job snapshot and parses its three date strings.</summary>
        /// <param name="name">Job name text to store unchanged.</param>
        /// <param name="client">Client text to store unchanged.</param>
        /// <param name="originator">Originating engineer name to store unchanged.</param>
        /// <param name="originated">Engineer date; null/empty/whitespace means unspecified, otherwise parsed using CurrentCulture.</param>
        /// <param name="checker">Checker name text to store unchanged.</param>
        /// <param name="dateChecked">Checked date using the same parsing/unspecified-date rules.</param>
        /// <param name="approver">Approver name text to store unchanged.</param>
        /// <param name="approved">Approved date using the same parsing/unspecified-date rules.</param>
        /// <param name="number">Job number text.</param>
        /// <param name="revision">Revision text.</param>
        /// <param name="part">Job part text.</param>
        /// <param name="reference">Reference text.</param>
        /// <param name="comments">Comment text to store unchanged.</param>
        /// <returns>A new FullJobInfo snapshot with independent property storage.</returns>
        /// <remarks>The reference factory signature and DateTime property types are retained. Nonempty
        /// dates follow DateTime.Parse with CurrentCulture and DateTimeStyles.None; ambiguous dates are
        /// not reinterpreted using another culture. DateTime.MinValue represents an unspecified field.
        /// Text may be null when supplied manually. No native calls, saving or date-format validation occurs before parsing.</remarks>
        /// <exception cref="FormatException">A nonempty date is invalid for the current culture.</exception>
        public static FullJobInfo From(string name, string client, string originator, string originated, string checker, string dateChecked,
            string approver, string approved, string number, string revision, string part, string reference, string comments)
        {
            return new FullJobInfo
            {
                Name = name, Client = client, Originator = originator,
                Originated = ParseJobDate(originated), Checker = checker, Checked = ParseJobDate(dateChecked),
                Approver = approver, Approved = ParseJobDate(approved), Number = number, Revision = revision,
                Part = part, Reference = reference, Comments = comments
            };
        }

        private static DateTime ParseJobDate(string value) => string.IsNullOrWhiteSpace(value)
            ? DateTime.MinValue : DateTime.Parse(value, CultureInfo.CurrentCulture, DateTimeStyles.None);
    }
}
