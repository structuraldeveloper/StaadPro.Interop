using System;
using StaadPro.Interop.Enums.Design;
using System.Collections.Generic;
using StaadPro.Interop.Services.Design;

namespace StaadPro.Interop.Models.Design
{
    /// <summary>Owns one immutable command request, replacing source reflection over Value and MemberList.</summary>
    /// <remarks>Member IDs are enumerated once, capped at 100,000, positive, and copied. Order controls stable ordering within a batch; equal orders preserve input order. GROUP uses the documented 2025/2026 single-option grammar. Extended GROUP remains a separate pending contract.</remarks>
    public sealed class DesignCommandAssignment
    {
        /// <summary>Creates a validated immutable request without accessing STAAD.</summary>
        /// <param name = "command">Documented command kind, subject to native material/code restrictions.</param>
        /// <param name = "memberIds">Nonempty analytical member IDs; duplicates and input order are retained.</param>
        /// <param name = "order">Nonnegative ordering priority within a batch.</param>
        /// <param name = "groupProperty">GROUP option 0..3; must be None for every other command.</param>
        /// <exception cref = "ArgumentException">The selection is null/empty/oversized or an ID, enum or order is invalid.</exception>
        public DesignCommandAssignment(DesignCommandKind command, IEnumerable<int> memberIds, int order = 0, DesignGroupProperty groupProperty = DesignGroupProperty.None)
        {
            if (!Enum.IsDefined(typeof(DesignCommandKind), command))
                throw new ArgumentOutOfRangeException(nameof(command));
            if (!Enum.IsDefined(typeof(DesignGroupProperty), groupProperty) || (command != DesignCommandKind.Group && groupProperty != DesignGroupProperty.None))
                throw new ArgumentOutOfRangeException(nameof(groupProperty));
            DesignInputs.Nonnegative(order, nameof(order));
            Command = command;
            GroupProperty = groupProperty;
            Order = order;
            MemberIds = Array.AsReadOnly(DesignInputs.Ids(memberIds, 100000, nameof(memberIds), false));
        }

        /// <summary>Gets the typed native command intent.</summary>
        public DesignCommandKind Command { get; }
        /// <summary>Gets the single GROUP option; None for non-GROUP commands.</summary>
        public DesignGroupProperty GroupProperty { get; }
        /// <summary>Gets the nonnegative stable batch priority.</summary>
        public int Order { get; }
        /// <summary>Gets an owned read-only selection of analytical member IDs, not list indices.</summary>
        public IReadOnlyList<int> MemberIds { get; }
    }

    /// <summary>Owns one immutable parameter assignment with an invariant native value.</summary>
    /// <remarks>Adapts the main source's named value/order/member-selection intent. Numeric values retain current native units. Formatting does not validate engineering meaning, parameter availability, or code-specific ranges.</remarks>
    public sealed class DesignParameterAssignment
    {
        /// <summary>Validates and snapshots a parameter before any native mutation.</summary>
        /// <param name = "parameterName">Single STAAD keyword beginning with a letter, followed by letters, digits or underscores.</param>
        /// <param name = "value">Nonblank single-line string, Boolean (1/0), finite numeric scalar, or Int32-range enum. Numbers use invariant round-trip formatting; no arbitrary object's ToString is invoked.</param>
        /// <param name = "memberIds">Nonempty positive analytical member IDs, capped at 100,000; order and duplicates are retained.</param>
        /// <param name = "order">Nonnegative stable batch priority.</param>
        /// <exception cref = "ArgumentException">A name, value, selection or order is invalid or an unsupported value type is supplied.</exception>
        public DesignParameterAssignment(string parameterName, object value, IEnumerable<int> memberIds, int order = 0)
        {
            DesignInputs.Keyword(parameterName, nameof(parameterName));
            DesignInputs.Nonnegative(order, nameof(order));
            ParameterName = parameterName;
            NativeValue = DesignInputs.Value(value);
            Order = order;
            MemberIds = Array.AsReadOnly(DesignInputs.Ids(memberIds, 100000, nameof(memberIds), false));
        }

        /// <summary>Gets the exact validated keyword without case conversion.</summary>
        public string ParameterName { get; }
        /// <summary>Gets the owned invariant string that is passed to STAAD, without unit conversion.</summary>
        public string NativeValue { get; }
        /// <summary>Gets the nonnegative stable batch priority.</summary>
        public int Order { get; }
        /// <summary>Gets the owned read-only analytical member selection.</summary>
        public IReadOnlyList<int> MemberIds { get; }
    }
}
