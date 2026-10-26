using StaadPro.Interop.Enums;
namespace StaadPro.Interop.Models.Output
{
    /// <summary>Owns a snapshot of caller-supplied or Load-adapter-resolved case identity.</summary>
    /// <remarks>Reference metadata is not proof of analysis freshness. Case numbers must be unambiguous across supplied families. Null title stays null rather than becoming invented native text.</remarks>
    public sealed class OutputLoadCaseIdentity
    {
        internal OutputLoadCaseIdentity(int id, string title, LoadCaseType kind, LoadType type)
        {
            Id = id;
            Title = title;
            CaseType = kind;
            Type = type;
        }
        /// <summary>Gets the positive captured case number.</summary>
        public int Id
        {
            get;
        }
        /// <summary>Gets captured title text, including null or explicitly empty supplied text.</summary>
        public string Title
        {
            get;
        }
        /// <summary>Gets the captured primary/combination/reference/repeat category.</summary>
        public LoadCaseType CaseType
        {
            get;
        }
        /// <summary>Gets the captured engineering load type without conversion.</summary>
        public LoadType Type
        {
            get;
        }
    }
}
