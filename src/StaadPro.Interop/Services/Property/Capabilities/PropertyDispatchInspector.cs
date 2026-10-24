using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using TYPEATTR = System.Runtime.InteropServices.ComTypes.TYPEATTR;
using FUNCDESC = System.Runtime.InteropServices.ComTypes.FUNCDESC;
using TYPEDESC = System.Runtime.InteropServices.ComTypes.TYPEDESC;
using ELEMDESC = System.Runtime.InteropServices.ComTypes.ELEMDESC;
using PARAMFLAG = System.Runtime.InteropServices.ComTypes.PARAMFLAG;
using INVOKEKIND = System.Runtime.InteropServices.ComTypes.INVOKEKIND;
using ITypeInfo = System.Runtime.InteropServices.ComTypes.ITypeInfo;

namespace StaadPro.Interop.Services.Property.Capabilities
{
    internal sealed class PropertyDispatchInspector
    {
        private const int UnknownName = unchecked((int)0x80020006);
        private const int NotImplemented = unchecked((int)0x80004001);

        [ComImport, Guid("00020400-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IDispatchMetadata
        {
            [PreserveSig] int GetTypeInfoCount(out uint count);
            [PreserveSig] int GetTypeInfo(uint index, int lcid, out ITypeInfo typeInfo);
            [PreserveSig] int GetIDsOfNames(ref Guid iid,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] string[] names,
                uint count, int lcid, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] int[] ids);
        }

        public PropertyMemberEvidence Inspect(object root, PropertyCatalogNativeContract contract)
        {
            if (!Marshal.IsComObject(root)) return InspectManaged(root.GetType(), contract);
            var dispatch = (IDispatchMetadata)root;
            Guid iid = Guid.Empty;
            int[] ids = { -1 };
            int lcid = CultureInfo.CurrentCulture.LCID;
            int hr = dispatch.GetIDsOfNames(ref iid, new[] { contract.Name }, 1, lcid, ids);
            if (hr == UnknownName) return PropertyMemberEvidence.Absent;
            Marshal.ThrowExceptionForHR(hr);
            if (hr != 0 || ids[0] == -1) return PropertyMemberEvidence.SignatureUnverified;
            hr = dispatch.GetTypeInfoCount(out uint count);
            if (hr == NotImplemented) return PropertyMemberEvidence.SignatureUnverified;
            Marshal.ThrowExceptionForHR(hr);
            if (hr != 0 || count != 1) return PropertyMemberEvidence.SignatureUnverified;
            ITypeInfo info = null;
            try
            {
                hr = dispatch.GetTypeInfo(0, lcid, out info);
                if (hr == NotImplemented) return PropertyMemberEvidence.SignatureUnverified;
                Marshal.ThrowExceptionForHR(hr);
                if (hr != 0 || info == null) return PropertyMemberEvidence.SignatureUnverified;
                return InspectTypeInfo(info, ids[0], contract);
            }
            finally
            {
                // Release only the type-info reference acquired above, never the caller's root.
                if (info != null && Marshal.IsComObject(info)) Marshal.ReleaseComObject(info);
            }
        }

        internal static PropertyMemberEvidence InspectManaged(Type type, PropertyCatalogNativeContract contract)
        {
            if (!type.IsVisible) return PropertyMemberEvidence.SignatureUnverified;
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.Name == contract.Name && !m.IsSpecialName).ToArray();
            if (methods.Length == 0) return PropertyMemberEvidence.Absent;
            int matches = methods.Count(m => !m.IsGenericMethod && m.ReturnType == typeof(object) &&
                m.GetParameters().Select(p => p.ParameterType).SequenceEqual(contract.ParameterTypes) &&
                m.GetParameters().All(p => !p.IsOptional && !p.IsOut));
            return methods.Length == 1 && matches == 1 ? PropertyMemberEvidence.Compatible : PropertyMemberEvidence.SignatureUnverified;
        }

        internal static PropertyMemberEvidence InspectTypeInfo(ITypeInfo info, int memberId, PropertyCatalogNativeContract contract)
        {
            IntPtr attributes = IntPtr.Zero;
            int matches = 0, declarations = 0;
            try
            {
                info.GetTypeAttr(out attributes);
                if (attributes == IntPtr.Zero) return PropertyMemberEvidence.SignatureUnverified;
                var type = (TYPEATTR)Marshal.PtrToStructure(attributes, typeof(TYPEATTR));
                for (int index = 0; index < type.cFuncs; index++)
                {
                    IntPtr pointer = IntPtr.Zero;
                    try
                    {
                        info.GetFuncDesc(index, out pointer);
                        if (pointer == IntPtr.Zero) return PropertyMemberEvidence.SignatureUnverified;
                        var function = (FUNCDESC)Marshal.PtrToStructure(pointer, typeof(FUNCDESC));
                        if (function.memid != memberId || function.invkind != INVOKEKIND.INVOKE_FUNC) continue;
                        declarations++;
                        if (Matches(function, contract)) matches++;
                    }
                    finally { if (pointer != IntPtr.Zero) info.ReleaseFuncDesc(pointer); }
                }
            }
            finally { if (attributes != IntPtr.Zero) info.ReleaseTypeAttr(attributes); }
            return declarations == 1 && matches == 1 ? PropertyMemberEvidence.Compatible : PropertyMemberEvidence.SignatureUnverified;
        }

        private static bool Matches(FUNCDESC function, PropertyCatalogNativeContract contract)
        {
            if (function.cParamsOpt != 0 || function.cParams < 0 || function.cParams > contract.ParameterTypes.Length + 1)
                return false;
            if (function.cParams != 0 && function.lprgelemdescParam == IntPtr.Zero) return false;
            int argument = 0, returnParameters = 0;
            bool returnMatches = MatchesType(function.elemdescFunc.tdesc, typeof(object));
            int elementSize = Marshal.SizeOf(typeof(ELEMDESC));
            for (int i = 0; i < function.cParams; i++)
            {
                var parameter = (ELEMDESC)Marshal.PtrToStructure(IntPtr.Add(function.lprgelemdescParam, i * elementSize), typeof(ELEMDESC));
                var flags = parameter.desc.paramdesc.wParamFlags;
                if ((flags & PARAMFLAG.PARAMFLAG_FRETVAL) != 0)
                {
                    returnParameters++;
                    if ((flags & PARAMFLAG.PARAMFLAG_FOUT) == 0) return false;
                    returnMatches = MatchesType(parameter.tdesc, typeof(object).MakeByRefType());
                    continue;
                }
                if ((flags & (PARAMFLAG.PARAMFLAG_FOPT | PARAMFLAG.PARAMFLAG_FLCID)) != 0 ||
                    argument >= contract.ParameterTypes.Length) return false;
                var expected = contract.ParameterTypes[argument++];
                if (!expected.IsByRef && (flags & PARAMFLAG.PARAMFLAG_FOUT) != 0) return false;
                if (!MatchesType(parameter.tdesc, expected)) return false;
            }
            bool directReturn = MatchesType(function.elemdescFunc.tdesc, typeof(object));
            bool hresultReturn = function.elemdescFunc.tdesc.vt == (short)VarEnum.VT_HRESULT;
            return argument == contract.ParameterTypes.Length && returnMatches &&
                ((returnParameters == 0 && directReturn) || (returnParameters == 1 && hresultReturn));
        }

        private static bool MatchesType(TYPEDESC description, Type expected)
        {
            var kind = (VarEnum)description.vt;
            if (expected.IsByRef)
            {
                if ((kind & VarEnum.VT_BYREF) != 0)
                {
                    description.vt = (short)(kind & ~VarEnum.VT_BYREF);
                    return MatchesType(description, expected.GetElementType());
                }
                if (kind != VarEnum.VT_PTR || description.lpValue == IntPtr.Zero) return false;
                return MatchesType((TYPEDESC)Marshal.PtrToStructure(description.lpValue, typeof(TYPEDESC)), expected.GetElementType());
            }
            if (expected == typeof(int)) return kind == VarEnum.VT_I4 || kind == VarEnum.VT_INT;
            if (expected == typeof(double)) return kind == VarEnum.VT_R8;
            if (expected == typeof(string)) return kind == VarEnum.VT_BSTR || kind == VarEnum.VT_LPWSTR;
            if (expected == typeof(object)) return kind == VarEnum.VT_VARIANT;
            return false;
        }
    }
}
