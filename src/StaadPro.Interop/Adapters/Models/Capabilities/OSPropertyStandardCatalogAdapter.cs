using System;
using StaadPro.Interop.Models.Property;
using StaadPro.Interop.Enums.Property;
using StaadPro.Interop.Adapters.Interfaces.Capabilities;
using StaadPro.Interop.Services.Property.Capabilities;
using static StaadPro.Interop.Services.Property.PropertyValueReader;
namespace StaadPro.Interop.Adapters.Models.Capabilities
{
    internal sealed class OSPropertyStandardCatalogAdapter : IOSPropertyStandardCatalog
    {
        private readonly PropertyCatalogAccess access; private readonly Version version;
        internal OSPropertyStandardCatalogAdapter(PropertyCatalogAccess access, Version version) { this.access = access; this.version = version; }
        /// <summary>Gets standard profile default database folder path.</summary>
        /// <returns>Exact native default-folder text; native null/DBNull/empty returns EmptyOrUnavailable with no Data. Non-string/NUL text is rejected; no filesystem existence or fallback is inferred.</returns>
        /// <remarks>When to use: observe the native standard-section catalog through the separately acquired property.Capabilities.GetStandardCatalog capability. Acquisition requires exact native 25.00.01.424 or 26.00.00.340 and all five compatible member signatures, without invoking catalog functions. Every call rechecks the captured binding and native version under the shared Property gate. The host version does not enable native APIs. No catalog parsing, profile normalization, file probing, unit conversion, model mutation, retry or engineering inference occurs. A grant is metadata qualification, not live runtime certification.</remarks>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// var grant = property.Capabilities.GetStandardCatalog();
        /// if (grant.IsAvailable)
        /// {
        ///     var result = grant.Capability.GetDefaultStandardProfileDBFolder();
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.Data);
        /// }
        /// </code></example>
        public PropertyQueryResult<string> GetDefaultStandardProfileDBFolder()
        {
            return access.Run(version, p => { const string op = "GetDefaultStandardProfileDBFolder"; object raw = p.GetDefaultStandardProfileDBFolder();
                if (raw == null || raw == DBNull.Value) return new PropertyQueryResult<string>(op, PropertyQueryStatus.EmptyOrUnavailable);
                if (!(raw is string text) || text.IndexOf('\0') >= 0) throw new InvalidOperationException(op + " returned malformed native text.");
                if (text.Length == 0) return new PropertyQueryResult<string>(op, PropertyQueryStatus.EmptyOrUnavailable);
                return new PropertyQueryResult<string>(op, PropertyQueryStatus.Success, text);
            });
        }
        /// <summary>Checks if the specified section property reference number is from standard section database source or not.</summary>
        /// <param name="propertyId">Positive native section property reference ID, distinct from analytical member IDs, table IDs and native indices.</param>
        /// <returns>Success with requested property identity and the actual Boolean/integral 0/1 origin observation. False is valid data identifying a nonstandard source, not a native failure or a missing-reference proof.</returns>
        /// <remarks>When to use: observe the native standard-section catalog through the separately acquired property.Capabilities.GetStandardCatalog capability. Acquisition requires exact native 25.00.01.424 or 26.00.00.340 and all five compatible member signatures, without invoking catalog functions. Every call rechecks the captured binding and native version under the shared Property gate. The host version does not enable native APIs. No catalog parsing, profile normalization, file probing, unit conversion, model mutation, retry or engineering inference occurs. A grant is metadata qualification, not live runtime certification.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">The requested propertyId is nonpositive; rejected before native access.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// var grant = property.Capabilities.GetStandardCatalog();
        /// if (grant.IsAvailable)
        /// {
        ///     var result = grant.Capability.IsStandardDatabaseSection(7);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.Data.IsStandardDatabase);
        /// }
        /// </code></example>
        public PropertyQueryResult<StandardSectionSource> IsStandardDatabaseSection(int propertyId)
        {
            Positive(propertyId, nameof(propertyId));
            return access.Run(version, p => { const string op = "IsStandardDatabaseSection"; object raw = p.IsStandardDatabaseSection((object)propertyId);
                bool flag = Flag(raw, op); return new PropertyQueryResult<StandardSectionSource>(op, PropertyQueryStatus.Success, new StandardSectionSource(propertyId, flag), raw is bool ? (int?)null : Integer(raw, op), raw is bool b ? b : (bool?)null);
            });
        }
        /// <summary>Gets standard section database name for the specified section property reference number.</summary>
        /// <param name="propertyId">Positive native section property reference ID, distinct from analytical member IDs, table IDs and native indices.</param>
        /// <returns>Success with requested property identity and exact nonempty native text. Empty string returns EmptyOrUnavailable because the property is not in the applicable standard database/table. Undocumented null/DBNull, non-string and embedded NUL responses are rejected. Whitespace is preserved, not trimmed.</returns>
        /// <remarks>When to use: observe the native standard-section catalog through the separately acquired property.Capabilities.GetStandardCatalog capability. Acquisition requires exact native 25.00.01.424 or 26.00.00.340 and all five compatible member signatures, without invoking catalog functions. Every call rechecks the captured binding and native version under the shared Property gate. The host version does not enable native APIs. No catalog parsing, profile normalization, file probing, unit conversion, model mutation, retry or engineering inference occurs. A grant is metadata qualification, not live runtime certification.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">The requested propertyId is nonpositive; rejected before native access.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// var grant = property.Capabilities.GetStandardCatalog();
        /// if (grant.IsAvailable)
        /// {
        ///     var result = grant.Capability.GetStandardSectionDatabaseName(7);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.Data.Text);
        /// }
        /// </code></example>
        public PropertyQueryResult<StandardSectionText> GetStandardSectionDatabaseName(int propertyId)
        {
            Positive(propertyId, nameof(propertyId));
            return access.Run(version, p => { const string op = "GetStandardSectionDatabaseName"; object raw = p.GetStandardSectionDatabaseName((object)propertyId);
                if (!(raw is string text) || text.IndexOf('\0') >= 0) throw new InvalidOperationException(op + " returned malformed native text.");
                if (text.Length == 0) return new PropertyQueryResult<StandardSectionText>(op, PropertyQueryStatus.EmptyOrUnavailable);
                return new PropertyQueryResult<StandardSectionText>(op, PropertyQueryStatus.Success, new StandardSectionText(propertyId, text));
            });
        }
        /// <summary>Get the table name from the standard section database for the specified standard section property reference number.</summary>
        /// <param name="propertyId">Positive native section property reference ID, distinct from analytical member IDs, table IDs and native indices.</param>
        /// <returns>Success with requested property identity and exact nonempty native text. Empty string returns EmptyOrUnavailable because the property is not in the applicable standard database/table. Undocumented null/DBNull, non-string and embedded NUL responses are rejected. Whitespace is preserved, not trimmed.</returns>
        /// <remarks>When to use: observe the native standard-section catalog through the separately acquired property.Capabilities.GetStandardCatalog capability. Acquisition requires exact native 25.00.01.424 or 26.00.00.340 and all five compatible member signatures, without invoking catalog functions. Every call rechecks the captured binding and native version under the shared Property gate. The host version does not enable native APIs. No catalog parsing, profile normalization, file probing, unit conversion, model mutation, retry or engineering inference occurs. A grant is metadata qualification, not live runtime certification.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">The requested propertyId is nonpositive; rejected before native access.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// var grant = property.Capabilities.GetStandardCatalog();
        /// if (grant.IsAvailable)
        /// {
        ///     var result = grant.Capability.GetStandardSectionTableName(7);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.Data.Text);
        /// }
        /// </code></example>
        public PropertyQueryResult<StandardSectionText> GetStandardSectionTableName(int propertyId)
        {
            Positive(propertyId, nameof(propertyId));
            return access.Run(version, p => { const string op = "GetStandardSectionTableName"; object raw = p.GetStandardSectionTableName((object)propertyId);
                if (!(raw is string text) || text.IndexOf('\0') >= 0) throw new InvalidOperationException(op + " returned malformed native text.");
                if (text.Length == 0) return new PropertyQueryResult<StandardSectionText>(op, PropertyQueryStatus.EmptyOrUnavailable);
                return new PropertyQueryResult<StandardSectionText>(op, PropertyQueryStatus.Success, new StandardSectionText(propertyId, text));
            });
        }
        /// <summary>Get the section name from the standard section database and table for the specified standard section property reference number.</summary>
        /// <param name="propertyId">Positive native section property reference ID, distinct from analytical member IDs, table IDs and native indices.</param>
        /// <returns>Success with requested property identity and exact nonempty native text. Empty string returns EmptyOrUnavailable because the property is not in the applicable standard database/table. Undocumented null/DBNull, non-string and embedded NUL responses are rejected. Whitespace is preserved, not trimmed.</returns>
        /// <remarks>When to use: observe the native standard-section catalog through the separately acquired property.Capabilities.GetStandardCatalog capability. Acquisition requires exact native 25.00.01.424 or 26.00.00.340 and all five compatible member signatures, without invoking catalog functions. Every call rechecks the captured binding and native version under the shared Property gate. The host version does not enable native APIs. No catalog parsing, profile normalization, file probing, unit conversion, model mutation, retry or engineering inference occurs. A grant is metadata qualification, not live runtime certification.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">The requested propertyId is nonpositive; rejected before native access.</exception>
        /// <exception cref="InvalidOperationException">The captured binding is disposed/replaced or a native response is malformed or undocumented.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Native transport fails; the original exception propagates without retry.</exception>
        /// <example><code>
        /// var grant = property.Capabilities.GetStandardCatalog();
        /// if (grant.IsAvailable)
        /// {
        ///     var result = grant.Capability.GetStandardSectionName(7);
        ///     System.Console.WriteLine(result.Status);
        ///     if (result.IsSuccessful) System.Console.WriteLine(result.Data.Text);
        /// }
        /// </code></example>
        public PropertyQueryResult<StandardSectionText> GetStandardSectionName(int propertyId)
        {
            Positive(propertyId, nameof(propertyId));
            return access.Run(version, p => { const string op = "GetStandardSectionName"; object raw = p.GetStandardSectionName((object)propertyId);
                if (!(raw is string text) || text.IndexOf('\0') >= 0) throw new InvalidOperationException(op + " returned malformed native text.");
                if (text.Length == 0) return new PropertyQueryResult<StandardSectionText>(op, PropertyQueryStatus.EmptyOrUnavailable);
                return new PropertyQueryResult<StandardSectionText>(op, PropertyQueryStatus.Success, new StandardSectionText(propertyId, text));
            });
        }
    }
}
