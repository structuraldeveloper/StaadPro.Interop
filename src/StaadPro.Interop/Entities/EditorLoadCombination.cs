using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using StaadPro.Interop.Base;

namespace StaadPro.Interop.Entities
{
    /// <summary>Immutable combination text containing alternating case numbers and factors.</summary>
    public sealed class EditorLoadCombination : LoadCombination
    {
        /// <summary>Creates a managed combination definition without native access.</summary>
        /// <param name="id">Combination number.</param>
        /// <param name="title">Title sent unchanged when creating the combination.</param>
        /// <param name="combinationText">Whitespace-separated case/factor pairs using invariant-culture numbers, for example <c>1 1.5 2 -0.75</c>. Parsing occurs when Factors or IsValid is read.</param>
        [JsonConstructor]
        public EditorLoadCombination(int id, string title, string combinationText) : base(id, title) { CombinationText = combinationText; }
        /// <summary>Gets the original case/factor text.</summary>
        [JsonProperty]
        public string CombinationText { get; private set; }
        /// <summary>Gets a fresh dictionary parsed from CombinationText on each access.</summary>
        /// <remarks>Pairs use invariant culture. Case tokens retain the reference double-to-int conversion; duplicate IDs throw. Odd token counts throw InvalidOperationException. Null text and invalid numeric tokens throw; this property does not cache, normalize or read a model.</remarks>
        [JsonIgnore]
        public Dictionary<int, double> Factors
        {
            get
            {
                string[] tokens = CombinationText.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length % 2 != 0) throw new InvalidOperationException("CombinationText is in an invalid format.");
                var result = new Dictionary<int, double>();
                for (int i = 0; i < tokens.Length; i += 2)
                    result.Add((int)double.Parse(tokens[i], CultureInfo.InvariantCulture), double.Parse(tokens[i + 1], CultureInfo.InvariantCulture));
                return result;
            }
        }
        /// <summary>Builds a repeat-load command title from this object's Title.</summary>
        /// <param name="shrinkTitleTextSpaces">Removes ordinary spaces from the formula when true.</param>
        /// <returns>A repeat-load title, or null when the title does not match the LOAD COMB pattern.</returns>
        /// <remarks>No native access. A null Title throws; see the static overload for parsing.</remarks>
        public string GetRepeatLoadTitle(bool shrinkTitleTextSpaces) => GetRepeatLoadTitle(Title, shrinkTitleTextSpaces);
        /// <summary>Splits this object's LOAD COMB title into command, ID and formula.</summary>
        /// <returns>Three strings, or null when the title pattern does not match.</returns>
        /// <remarks>No native access; null Title throws.</remarks>
        public IEnumerable<string> SplitCombinationTitle() => SplitCombinationTitle(Title);
        /// <summary>Tests the reference nonempty, nonzero case/factor predicate.</summary>
        /// <returns>True when at least one pair exists and no case ID or factor is zero.</returns>
        /// <remarks>Factors are parsed eagerly and parsing exceptions propagate. This predicate does not check native existence, positive IDs or finite factors; creation additionally validates these inputs.</remarks>
        public bool IsValid()
        {
            var factors = Factors;
            if (factors.Count == 0) return false;
            foreach (var pair in factors) if (pair.Key == 0 || pair.Value == 0) return false;
            return true;
        }
        /// <summary>Builds a repeat-load command title from a LOAD COMB title.</summary>
        /// <param name="fromCombinationTitle">Title matching <c>LOAD COMB number formula</c>; null is invalid.</param>
        /// <param name="shrinkTitleTextSpaces">Removes ordinary spaces from the formula when true.</param>
        /// <returns><c>LOAD number LOADTYPE None formula</c>, or null for a nonmatching title.</returns>
        /// <remarks>Matching is case-sensitive and uses the reference pattern. No COM calls or numeric formula evaluation occurs.</remarks>
        public static string GetRepeatLoadTitle(string fromCombinationTitle, bool shrinkTitleTextSpaces)
        {
            var fragments = SplitCombinationTitle(fromCombinationTitle) as string[];
            if (fragments == null) return null;
            string text = shrinkTitleTextSpaces ? fragments[2].Replace(" ", "") : fragments[2];
            return $"LOAD {fragments[1]} LOADTYPE None {text}";
        }
        /// <summary>Splits a LOAD COMB title into command, number text and formula.</summary>
        /// <param name="title">A non-null title matching <c>LOAD COMB number formula</c>.</param>
        /// <returns>A three-element string array exposed as IEnumerable, or null when unmatched.</returns>
        /// <remarks>Uses the reference case-sensitive regular expression; does not normalize the formula or access STAAD.</remarks>
        public static IEnumerable<string> SplitCombinationTitle(string title)
        {
            Match match = Regex.Match(title, @"^(LOAD COMB)\s+(\d+)\s+(.*)$");
            return match.Success ? new[] { match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value } : null;
        }
        /// <summary>Applies the reference validity predicate to a nullable definition.</summary>
        /// <param name="loadCombination">Definition to parse, or null.</param>
        /// <returns>False for null; otherwise the instance IsValid result.</returns>
        /// <remarks>Parsing exceptions propagate. This method does not check model existence.</remarks>
        public static bool IsValid(EditorLoadCombination loadCombination) => loadCombination != null && loadCombination.IsValid();
        /// <summary>Compares combination number and exact combination text.</summary>
        /// <param name="obj">Object to compare.</param>
        /// <returns>True for an EditorLoadCombination with equal Id and CombinationText.</returns>
        /// <remarks>Title is not part of equality; no parsing or native access occurs.</remarks>
        public override bool Equals(object obj) => obj is EditorLoadCombination other && Equals(other);
        /// <summary>Compares combination number and exact combination text.</summary>
        /// <param name="combination">Other definition, possibly null.</param>
        /// <returns>True when IDs and combination text are equal.</returns>
        /// <remarks>Title is ignored; no parsing or native access occurs.</remarks>
        public bool Equals(EditorLoadCombination combination) => !(combination is null) && Id == combination.Id && CombinationText == combination.CombinationText;
        /// <summary>Returns the reference ID-based hash.</summary>
        /// <returns>The combination number's hash code.</returns>
        /// <remarks>Equal definitions have equal hashes; different texts can share an ID/hash.</remarks>
        public override int GetHashCode() => Id.GetHashCode();
        /// <summary>Compares nullable definitions by ID and text.</summary>
        public static bool operator ==(EditorLoadCombination left, EditorLoadCombination right) => ReferenceEquals(left, right) || (!(left is null) && left.Equals(right));
        /// <summary>Negates definition equality.</summary>
        public static bool operator !=(EditorLoadCombination left, EditorLoadCombination right) => !(left == right);
    }
}
