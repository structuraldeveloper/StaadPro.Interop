using System;
using System.Runtime.InteropServices;
using Microsoft.CSharp.RuntimeBinder;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;
using StaadPro.Interop.Services.Interop;

namespace StaadPro.Interop.Services.Wrapper
{
    // Shared native normalization for the wrapper cache and standalone Root queries.
    internal static class WrapperMetadataReader
    {
        internal static BaseUnitSystem ReadBaseUnit(object root)
        {
            object value;
            try { value = ((dynamic)root).GetBaseUnit(); }
            catch (RuntimeBinderException) { return BaseUnitSystem.Unknown; }
            catch (COMException ex) when (BoundStaadModelServices.IsMissingMember(ex)) { return BaseUnitSystem.Unknown; }
            if (value == null || value == DBNull.Value) return BaseUnitSystem.Unknown;
            int code = NativeValues.ReadInt32(value, "base-unit code");
            return (BaseUnitSystem)code;
        }

        internal static ForceInputUnit ReadInputForceUnit(object root, BaseUnitSystem baseUnitSystem, ApplicationVersion version)
        {
            object output = string.Empty;
            object returned = null;
            try { returned = ((dynamic)root).GetInputUnitForForce(ref output); }
            catch { /* Main-project compatibility: a model in transition uses the base-system fallback. */ }
            if (WrapperUnitParser.TryParseForceInputUnit(output, out ForceInputUnit fromOutput)) return fromOutput;
            if (CanUseUnitReturn(returned, version) && WrapperUnitParser.TryParseForceInputUnit(returned, out ForceInputUnit fromReturn)) return fromReturn;
            return baseUnitSystem == BaseUnitSystem.Imperial ? ForceInputUnit.KiloPound : ForceInputUnit.KiloNewton;
        }

        internal static LengthInputUnit ReadInputLengthUnit(object root, BaseUnitSystem baseUnitSystem, ApplicationVersion version)
        {
            object output = string.Empty;
            object returned = null;
            try { returned = ((dynamic)root).GetInputUnitForLength(ref output); }
            catch { /* Main-project compatibility: a model in transition uses the base-system fallback. */ }
            if (WrapperUnitParser.TryParseLengthInputUnit(output, out LengthInputUnit fromOutput)) return fromOutput;
            if (CanUseUnitReturn(returned, version) && WrapperUnitParser.TryParseLengthInputUnit(returned, out LengthInputUnit fromReturn)) return fromReturn;
            return baseUnitSystem == BaseUnitSystem.Imperial ? LengthInputUnit.Feet : LengthInputUnit.Meter;
        }

        private static bool CanUseUnitReturn(object value, ApplicationVersion version)
        {
            if (value is bool) return false;
            // The 2024 help describes a success flag. Do not mistake that flag for a unit.
            // A return-only interpretation for that family requires the separate native-verified contract work.
            return version == null || version.Major != 24;
        }

        internal static ApplicationVersion ReadApplicationVersion(object root)
        {
            object major = -1, update = -1, revision = -1, build = -1;
            object returned;
            try { returned = ((dynamic)root).GetApplicationVersion(ref major, ref update, ref revision, ref build); }
            catch (RuntimeBinderException) { return null; }
            catch (COMException ex) when (BoundStaadModelServices.IsMissingMember(ex)) { return null; }
            if (!(returned is string text)) throw new InvalidOperationException("OpenSTAAD did not return version text.");
            ApplicationVersion result;
            try { result = ApplicationVersion.Parse(text); }
            catch (Exception ex) when (ex is ArgumentException || ex is FormatException || ex is OverflowException)
            { throw new InvalidOperationException("OpenSTAAD returned malformed version text.", ex); }
            object[] parts = { major, update, revision, build };
            int[] expected = { result.Major, result.Update, result.Revision, result.Build };
            bool allUnwritten = true;
            for (int i = 0; i < parts.Length; i++)
            {
                if (!(parts[i] is int n) || n != -1) allUnwritten = false;
            }
            // Some automation shims provide only the return text; do not invent output parts.
            if (!allUnwritten)
            {
                for (int i = 0; i < parts.Length; i++)
                {
                    int actual = NativeValues.ReadInt32(parts[i], "version component");
                    if (actual != expected[i]) throw new InvalidOperationException("OpenSTAAD version text and numeric outputs disagree.");
                }
            }
            return result;
        }

    }
}
