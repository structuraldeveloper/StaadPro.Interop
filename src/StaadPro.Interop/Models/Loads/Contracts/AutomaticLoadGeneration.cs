using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Enums.Loads;
using StaadPro.Interop.Services.Load;

namespace StaadPro.Interop.Models.Loads.Contracts
{
    /// <summary>Selects either all current cases explicitly or an owned, nonempty list of case IDs.</summary>
    /// <remarks>Prevents native null/empty automatic-generation arrays from accidentally meaning all cases.</remarks>
    public sealed class LoadCaseSelection
    {
        private LoadCaseSelection(bool all, int[] ids) { IncludesAllCases = all; LoadCaseIds = Array.AsReadOnly(ids); }
        /// <summary>Gets whether every applicable current case was explicitly requested.</summary>
        public bool IncludesAllCases { get; }
        /// <summary>Gets the owned IDs, preserving order and duplicates; empty only for AllCases.</summary>
        public IEnumerable<int> LoadCaseIds { get; }
        /// <summary>Creates an explicit all-current-cases selection.</summary>
        /// <returns>A selection whose future native packet deliberately uses all-case semantics.</returns>
        public static LoadCaseSelection AllCases() => new LoadCaseSelection(true, new int[0]);
        /// <summary>Snapshots a nonempty explicit case selection without changing the source.</summary>
        /// <param name="loadCaseIds">Positive case IDs; enumerated once.</param>
        /// <returns>An independent, ordered selection.</returns>
        /// <exception cref="ArgumentNullException">The sequence is null.</exception>
        /// <exception cref="ArgumentException">The sequence is empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">An ID is nonpositive.</exception>
        public static LoadCaseSelection SpecifiedCases(IEnumerable<int> loadCaseIds)
        {
            if (loadCaseIds == null) throw new ArgumentNullException(nameof(loadCaseIds));
            var ids = loadCaseIds.ToArray();
            if (ids.Length == 0) throw new ArgumentException("Choose AllCases explicitly instead of an empty selection.", nameof(loadCaseIds));
            foreach (int id in ids) LoadContractArguments.Positive(id, nameof(loadCaseIds));
            return new LoadCaseSelection(false, ids);
        }
    }

    /// <summary>Describes code/category-driven automatic combination generation without native invocation.</summary>
    public sealed class AutomaticCombinationRequest
    {
        /// <summary>Creates a reviewed automatic generation request.</summary>
        /// <param name="codeName">Nonblank code name from the acquired installation's Codes.ini; preserved verbatim.</param>
        /// <param name="categoryName">Nonblank category from its rule file; preserved verbatim.</param>
        /// <param name="cases">Explicit all-cases or nonempty specified-cases selection.</param>
        /// <param name="preferredStartCaseId">Positive preferred start ID; native generation may choose a later available ID.</param>
        /// <exception cref="ArgumentException">A name is blank.</exception>
        /// <exception cref="ArgumentNullException">The selection is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The preferred start ID is nonpositive.</exception>
        public AutomaticCombinationRequest(string codeName, string categoryName, LoadCaseSelection cases, int preferredStartCaseId)
        {
            CodeName = LoadContractArguments.Text(codeName, nameof(codeName)); CategoryName = LoadContractArguments.Text(categoryName, nameof(categoryName));
            Cases = cases ?? throw new ArgumentNullException(nameof(cases));
            PreferredStartCaseId = LoadContractArguments.Positive(preferredStartCaseId, nameof(preferredStartCaseId));
        }
        /// <summary>Gets the exact installed rule-code name; not a guessed engineering-code enum.</summary>
        public string CodeName { get; }
        /// <summary>Gets the exact installed rule category.</summary>
        public string CategoryName { get; }
        /// <summary>Gets the immutable case selection.</summary>
        public LoadCaseSelection Cases { get; }
        /// <summary>Gets the requested start, independently of the native actual start.</summary>
        public int PreferredStartCaseId { get; }
    }

    /// <summary>Contains optional automatic notional-generation settings.</summary>
    public sealed class AutomaticNotionalOptions
    {
        /// <summary>Creates explicit direction/factor settings; no defaults enable hidden directions.</summary>
        /// <param name="directions">At least one defined X/-X/Z/-Z managed flag.</param>
        /// <param name="factor">Finite dimensionless multiplication factor.</param>
        /// <param name="useGb50017Factor">Whether the native GB50017 factor option is requested.</param>
        /// <param name="floorCount">Positive floor count required only with the GB50017 option; otherwise null.</param>
        /// <exception cref="ArgumentOutOfRangeException">Flags, factor or floor count are invalid.</exception>
        /// <exception cref="ArgumentException">Floor count and GB50017 selection are inconsistent.</exception>
        public AutomaticNotionalOptions(AutomaticNotionalDirections directions, double factor, bool useGb50017Factor = false, int? floorCount = null)
        {
            if (directions == AutomaticNotionalDirections.None || ((int)directions & ~15) != 0) throw new ArgumentOutOfRangeException(nameof(directions));
            if (useGb50017Factor != floorCount.HasValue) throw new ArgumentException("Specify floorCount only when GB50017 factor calculation is enabled.", nameof(floorCount));
            if (floorCount.HasValue) LoadContractArguments.Positive(floorCount.Value, nameof(floorCount));
            Directions = directions; Factor = LoadContractArguments.Finite(factor, nameof(factor)); UseGb50017Factor = useGb50017Factor; FloorCount = floorCount;
        }
        /// <summary>Gets the selected managed direction flags.</summary>
        public AutomaticNotionalDirections Directions { get; }
        /// <summary>Gets the finite dimensionless factor.</summary>
        public double Factor { get; }
        /// <summary>Gets whether native GB50017 factor calculation is requested.</summary>
        public bool UseGb50017Factor { get; }
        /// <summary>Gets the positive GB50017 floor count, or null when inapplicable.</summary>
        public int? FloorCount { get; }
    }

    /// <summary>Describes automatic repeat generation, reusing the same code/category/case selection model.</summary>
    public sealed class AutomaticRepeatLoadRequest
    {
        /// <summary>Creates an explicit repeat-generation request.</summary>
        /// <param name="generation">Non-null immutable rule and selection settings.</param>
        /// <param name="includeReferenceLoads">Whether reference loads participate.</param>
        /// <param name="notionalOptions">Null disables notional generation; a non-null value explicitly enables it.</param>
        /// <exception cref="ArgumentNullException">Generation settings are null.</exception>
        public AutomaticRepeatLoadRequest(AutomaticCombinationRequest generation, bool includeReferenceLoads = false, AutomaticNotionalOptions notionalOptions = null)
        { Generation = generation ?? throw new ArgumentNullException(nameof(generation)); IncludeReferenceLoads = includeReferenceLoads; NotionalOptions = notionalOptions; }
        /// <summary>Gets the shared rule, case and preferred-start settings.</summary>
        public AutomaticCombinationRequest Generation { get; }
        /// <summary>Gets whether reference loads were explicitly included.</summary>
        public bool IncludeReferenceLoads { get; }
        /// <summary>Gets enabled notional-generation settings, or null when disabled.</summary>
        public AutomaticNotionalOptions NotionalOptions { get; }
    }

    /// <summary>Contains validated native generation outputs; never infers counts or contiguous created IDs.</summary>
    public sealed class AutomaticLoadGenerationData
    {
        internal AutomaticLoadGenerationData(int requested, int actual, int? count) { PreferredStartCaseId = requested; ActualStartCaseId = actual; GeneratedCount = count; }
        /// <summary>Gets the caller's original preferred start.</summary>
        public int PreferredStartCaseId { get; }
        /// <summary>Gets the actual start written by STAAD.</summary>
        public int ActualStartCaseId { get; }
        /// <summary>Gets the native generated repeat count, or null when the native combination API supplies no count.</summary>
        public int? GeneratedCount { get; }
    }
}
