using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Com = System.Runtime.InteropServices.ComTypes;
using NUnit.Framework;
using StaadPro.Interop.Services.Property.Capabilities;
namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class PropertyDispatchInspectorTests
    {
        private sealed class TypeInfoDouble : Com.ITypeInfo, IDisposable
        {
            internal readonly List<Com.FUNCDESC> Functions = new List<Com.FUNCDESC>();
            private readonly HashSet<IntPtr> owned = new HashSet<IntPtr>();
            internal int ReleasedAttributes, ReleasedFunctions;
            internal Exception DescriptorFailure;
            internal bool NullDescriptor;
            internal TypeInfoDouble(PropertyCatalogNativeContract contract)
            {
                var parameters = new List<Com.ELEMDESC>();
                foreach (Type type in contract.ParameterTypes)
                    parameters.Add(Parameter(type, Com.PARAMFLAG.PARAMFLAG_FIN | (type.IsByRef ? Com.PARAMFLAG.PARAMFLAG_FOUT : 0)));
                Functions.Add(Function(parameters.ToArray()));
            }
            internal Com.ELEMDESC Parameter(Type type, Com.PARAMFLAG flags) =>
                new Com.ELEMDESC { tdesc = TypeDescription(type),
                    desc = new Com.ELEMDESC.DESCUNION { paramdesc = new Com.PARAMDESC { wParamFlags = flags } } };
            private Com.TYPEDESC TypeDescription(Type type)
            {
                if (type.IsByRef) return new Com.TYPEDESC { vt = (short)VarEnum.VT_PTR, lpValue = Allocate(TypeDescription(type.GetElementType())) };
                return new Com.TYPEDESC { vt = (short)(type == typeof(int) ? VarEnum.VT_I4 : type == typeof(double) ? VarEnum.VT_R8 :
                    type == typeof(string) ? VarEnum.VT_LPWSTR : VarEnum.VT_VARIANT) };
            }
            internal Com.FUNCDESC Function(Com.ELEMDESC[] parameters)
            {
                int size = Marshal.SizeOf(typeof(Com.ELEMDESC));
                IntPtr array = Marshal.AllocCoTaskMem(size * parameters.Length); owned.Add(array);
                for (int i = 0; i < parameters.Length; i++) Marshal.StructureToPtr(parameters[i], IntPtr.Add(array, i * size), false);
                return new Com.FUNCDESC
                {
                    memid = 42, invkind = Com.INVOKEKIND.INVOKE_FUNC, funckind = Com.FUNCKIND.FUNC_DISPATCH,
                    cParams = (short)parameters.Length, lprgelemdescParam = array,
                    elemdescFunc = new Com.ELEMDESC { tdesc = new Com.TYPEDESC { vt = (short)VarEnum.VT_VARIANT } }
                };
            }
            internal Com.ELEMDESC GetParameter(int index)
            {
                return (Com.ELEMDESC)Marshal.PtrToStructure(IntPtr.Add(Functions[0].lprgelemdescParam,
                    index * Marshal.SizeOf(typeof(Com.ELEMDESC))), typeof(Com.ELEMDESC));
            }
            internal void SetParameter(int index, Com.ELEMDESC parameter) =>
                Marshal.StructureToPtr(parameter, IntPtr.Add(Functions[0].lprgelemdescParam,
                    index * Marshal.SizeOf(typeof(Com.ELEMDESC))), false);
            private IntPtr Allocate<T>(T value)
            {
                IntPtr p = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(T))); owned.Add(p);
                Marshal.StructureToPtr(value, p, false); return p;
            }
            public void GetTypeAttr(out IntPtr value) => value = Allocate(new Com.TYPEATTR { cFuncs = (short)Functions.Count, typekind = Com.TYPEKIND.TKIND_DISPATCH });
            public void GetFuncDesc(int index, out IntPtr value)
            {
                value = IntPtr.Zero;
                if (DescriptorFailure != null) throw DescriptorFailure;
                if (!NullDescriptor) value = Allocate(Functions[index]);
            }
            public void ReleaseTypeAttr(IntPtr value) { ReleasedAttributes++; Free(value); }
            public void ReleaseFuncDesc(IntPtr value) { ReleasedFunctions++; Free(value); }
            private void Free(IntPtr p) { Assert.That(owned.Remove(p), Is.True); Marshal.FreeCoTaskMem(p); }
            public void Dispose() { foreach (IntPtr p in owned) Marshal.FreeCoTaskMem(p); owned.Clear(); }
            public void GetTypeComp(out Com.ITypeComp value) => throw new NotSupportedException();
            public void GetVarDesc(int index, out IntPtr value) => throw new NotSupportedException();
            public void GetNames(int id, string[] names, int max, out int count) => throw new NotSupportedException();
            public void GetRefTypeOfImplType(int index, out int value) => throw new NotSupportedException();
            public void GetImplTypeFlags(int index, out Com.IMPLTYPEFLAGS value) => throw new NotSupportedException();
            public void GetIDsOfNames(string[] names, int count, int[] ids) => throw new NotSupportedException();
            public void Invoke(object target, int id, short flags, ref Com.DISPPARAMS args, IntPtr result, IntPtr error, out int argumentError) => throw new NotSupportedException();
            public void GetDocumentation(int id, out string name, out string doc, out int context, out string help) => throw new NotSupportedException();
            public void GetDllEntry(int id, Com.INVOKEKIND kind, IntPtr dll, IntPtr name, IntPtr ordinal) => throw new NotSupportedException();
            public void GetRefTypeInfo(int reference, out Com.ITypeInfo value) => throw new NotSupportedException();
            public void AddressOfMember(int id, Com.INVOKEKIND kind, out IntPtr value) => throw new NotSupportedException();
            public void CreateInstance(object outer, ref Guid iid, out object value) => throw new NotSupportedException();
            public void GetMops(int id, out string value) => throw new NotSupportedException();
            public void GetContainingTypeLib(out Com.ITypeLib value, out int index) => throw new NotSupportedException();
            public void ReleaseVarDesc(IntPtr value) => throw new NotSupportedException();
        }

        [TestCase(0)][TestCase(1)][TestCase(2)][TestCase(3)][TestCase(4)]
        public void ReviewedVariantSignaturesAreAcceptedAndDescriptorsReleased(int index)
        {var c=PropertyCatalogNativeContract.All[index];using(var info=new TypeInfoDouble(c)){Assert.That(PropertyDispatchInspector.InspectTypeInfo(info,42,c),Is.EqualTo(PropertyMemberEvidence.Compatible));Assert.That(info.ReleasedAttributes,Is.EqualTo(1));Assert.That(info.ReleasedFunctions,Is.EqualTo(1));}}
        [TestCase(VarEnum.VT_I4)][TestCase(VarEnum.VT_BSTR)][TestCase(VarEnum.VT_BOOL)][TestCase(VarEnum.VT_VOID)]
        public void WrongReturnMetadataIsNeverGranted(VarEnum type)
        {var c=PropertyCatalogNativeContract.All[1];using(var info=new TypeInfoDouble(c)){var f=info.Functions[0];f.elemdescFunc.tdesc.vt=(short)type;info.Functions[0]=f;Assert.That(PropertyDispatchInspector.InspectTypeInfo(info,42,c),Is.EqualTo(PropertyMemberEvidence.SignatureUnverified));}}
        [TestCase(Com.PARAMFLAG.PARAMFLAG_FOUT)][TestCase(Com.PARAMFLAG.PARAMFLAG_FOPT)][TestCase(Com.PARAMFLAG.PARAMFLAG_FLCID)]
        public void WrongParameterFlagsAreRejected(Com.PARAMFLAG flags)
        {var c=PropertyCatalogNativeContract.All[1];using(var info=new TypeInfoDouble(c)){info.Functions[0]=info.Function(new[]{info.Parameter(typeof(object),flags)});Assert.That(PropertyDispatchInspector.InspectTypeInfo(info,42,c),Is.EqualTo(PropertyMemberEvidence.SignatureUnverified));}}
        [Test]
        public void HresultWithOneVariantRetvalIsAccepted()
        {var c=PropertyCatalogNativeContract.All[1];using(var info=new TypeInfoDouble(c)){var f=info.Function(new[]{info.Parameter(typeof(object),Com.PARAMFLAG.PARAMFLAG_FIN),info.Parameter(typeof(object).MakeByRefType(),Com.PARAMFLAG.PARAMFLAG_FOUT|Com.PARAMFLAG.PARAMFLAG_FRETVAL)});f.elemdescFunc.tdesc.vt=(short)VarEnum.VT_HRESULT;info.Functions[0]=f;Assert.That(PropertyDispatchInspector.InspectTypeInfo(info,42,c),Is.EqualTo(PropertyMemberEvidence.Compatible));}}
        [Test]
        public void AmbiguousDeclarationsAndTransportFailuresAreNotSilenced()
        {var c=PropertyCatalogNativeContract.All[0];using(var info=new TypeInfoDouble(c)){info.Functions.Add(info.Functions[0]);Assert.That(PropertyDispatchInspector.InspectTypeInfo(info,42,c),Is.EqualTo(PropertyMemberEvidence.SignatureUnverified));}using(var info=new TypeInfoDouble(c)){var error=new COMException("metadata");info.DescriptorFailure=error;Assert.That(Assert.Throws<COMException>(()=>PropertyDispatchInspector.InspectTypeInfo(info,42,c)),Is.SameAs(error));Assert.That(info.ReleasedAttributes,Is.EqualTo(1));}}
        [Test]
        public void UnavailableDescriptorWithholdsTheCapabilityAndReleasesAttributes()
        {var c=PropertyCatalogNativeContract.All[0];using(var info=new TypeInfoDouble(c)){info.NullDescriptor=true;Assert.That(PropertyDispatchInspector.InspectTypeInfo(info,42,c),Is.EqualTo(PropertyMemberEvidence.SignatureUnverified));Assert.That(info.ReleasedAttributes,Is.EqualTo(1));}}
    }
}
