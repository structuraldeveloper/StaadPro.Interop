using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Com = System.Runtime.InteropServices.ComTypes;
using NUnit.Framework;
using StaadPro.Interop.Services.Root.Capabilities;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class RootDispatchInspectorTests
    {
        private sealed class TypeInfoDouble : Com.ITypeInfo, IDisposable
        {
            internal readonly List<Com.FUNCDESC> Functions = new List<Com.FUNCDESC>();
            private readonly HashSet<IntPtr> owned = new HashSet<IntPtr>();
            internal int ReleasedAttributes, ReleasedFunctions;
            internal Exception DescriptorFailure;
            internal bool NullDescriptor;
            internal TypeInfoDouble(RootNativeContract contract)
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
                    elemdescFunc = new Com.ELEMDESC { tdesc = new Com.TYPEDESC { vt = (short)VarEnum.VT_I4 } }
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

        [TestCase(0)] [TestCase(1)] [TestCase(2)] [TestCase(3)]
        public void CompatibleTypeInformationIsAcceptedAndOwnedDescriptorsAreReleased(int index)
        {
            var contract = new[] { RootNativeContract.Export, RootNativeContract.Errors, RootNativeContract.Warnings, RootNativeContract.AnalysisStatus }[index];
            using (var info = new TypeInfoDouble(contract))
            {
                Assert.That(RootDispatchInspector.InspectTypeInfo(info, 42, contract), Is.EqualTo(RootMemberEvidence.Compatible));
                Assert.That(info.ReleasedAttributes, Is.EqualTo(1));
                Assert.That(info.ReleasedFunctions, Is.EqualTo(1));
            }
        }

        [Test]
        public void HresultWithOneOutRetvalIsNormalizedWithoutConfusingItWithAnInput()
        {
            using (var info = new TypeInfoDouble(RootNativeContract.Export))
            {
                var function = info.Function(new[]
                {
                    info.Parameter(typeof(int), Com.PARAMFLAG.PARAMFLAG_FIN),
                    info.Parameter(typeof(int), Com.PARAMFLAG.PARAMFLAG_FIN),
                    info.Parameter(typeof(int).MakeByRefType(), Com.PARAMFLAG.PARAMFLAG_FOUT | Com.PARAMFLAG.PARAMFLAG_FRETVAL)
                });
                function.elemdescFunc.tdesc.vt = (short)VarEnum.VT_HRESULT;
                info.Functions[0] = function;
                Assert.That(RootDispatchInspector.InspectTypeInfo(info, 42, RootNativeContract.Export), Is.EqualTo(RootMemberEvidence.Compatible));
            }
        }

        [TestCase(0)] [TestCase(1)] [TestCase(2)] [TestCase(3)] [TestCase(4)] [TestCase(5)] [TestCase(6)]
        public void AmbiguousOptionalOrWrongSignaturesDoNotGrantAFeature(int shape)
        {
            using (var info = new TypeInfoDouble(RootNativeContract.Export))
            {
                var function = info.Functions[0];
                if (shape == 0) function.elemdescFunc.tdesc.vt = (short)VarEnum.VT_VARIANT;
                if (shape == 1) function.cParamsOpt = 1;
                if (shape == 2) function.invkind = Com.INVOKEKIND.INVOKE_PROPERTYGET;
                if (shape == 3) function.memid = 99;
                if (shape == 4) function.cParams = 1;
                if (shape == 5) function.lprgelemdescParam = IntPtr.Zero;
                if (shape == 6) info.Functions.Add(function);
                info.Functions[0] = function;
                Assert.That(RootDispatchInspector.InspectTypeInfo(info, 42, RootNativeContract.Export), Is.EqualTo(RootMemberEvidence.SignatureUnverified));
                Assert.That(info.ReleasedAttributes, Is.EqualTo(1));
                Assert.That(info.ReleasedFunctions, Is.EqualTo(info.Functions.Count));
            }
        }

        [TestCase(0)] [TestCase(1)] [TestCase(2)] [TestCase(3)]
        public void WrongDirectionsAndByValueOutputShapesAreRejected(int shape)
        {
            using (var info = new TypeInfoDouble(RootNativeContract.Errors))
            {
                var parameter = info.GetParameter(shape == 3 ? 0 : 1);
                if (shape == 0) parameter.tdesc = new Com.TYPEDESC { vt = (short)VarEnum.VT_VARIANT };
                if (shape == 1) parameter.tdesc = new Com.TYPEDESC { vt = (short)VarEnum.VT_USERDEFINED };
                if (shape == 2) parameter.desc.paramdesc.wParamFlags |= Com.PARAMFLAG.PARAMFLAG_FOPT;
                if (shape == 3) parameter.desc.paramdesc.wParamFlags |= Com.PARAMFLAG.PARAMFLAG_FOUT;
                info.SetParameter(shape == 3 ? 0 : 1, parameter);
                Assert.That(RootDispatchInspector.InspectTypeInfo(info, 42, RootNativeContract.Errors), Is.EqualTo(RootMemberEvidence.SignatureUnverified));
            }
        }

        [Test]
        public void VariantByrefFlagAndBstrInputAreAcceptedAsEquivalentAutomationShapes()
        {
            using (var info = new TypeInfoDouble(RootNativeContract.Errors))
            {
                var first = info.GetParameter(0); first.tdesc.vt = (short)VarEnum.VT_BSTR; info.SetParameter(0, first);
                var second = info.GetParameter(1); second.tdesc = new Com.TYPEDESC { vt = (short)(VarEnum.VT_VARIANT | VarEnum.VT_BYREF) }; info.SetParameter(1, second);
                Assert.That(RootDispatchInspector.InspectTypeInfo(info, 42, RootNativeContract.Errors), Is.EqualTo(RootMemberEvidence.Compatible));
            }
        }

        [Test]
        public void MetadataFailuresReleaseOwnedAttributesAndPropagateByIdentity()
        {
            var error = new COMException("Disconnected", unchecked((int)0x80010108));
            using (var info = new TypeInfoDouble(RootNativeContract.Export) { DescriptorFailure = error })
            {
                Assert.That(Assert.Throws<COMException>(() => RootDispatchInspector.InspectTypeInfo(info, 42, RootNativeContract.Export)), Is.SameAs(error));
                Assert.That(info.ReleasedAttributes, Is.EqualTo(1));
                Assert.That(info.ReleasedFunctions, Is.Zero);
            }
        }

        [Test]
        public void NullDescriptorDoesNotEstablishAnAbsentNativeMember()
        {
            using (var info = new TypeInfoDouble(RootNativeContract.Export) { NullDescriptor = true })
            {
                Assert.That(RootDispatchInspector.InspectTypeInfo(info, 42, RootNativeContract.Export), Is.EqualTo(RootMemberEvidence.SignatureUnverified));
                Assert.That(info.ReleasedAttributes, Is.EqualTo(1));
            }
        }

        [Test, Platform(Include = "Win")]
        public void AutomationMetadataInspectionDoesNotReleaseOrInvokeTheCallersObject()
        {
            object automation = Activator.CreateInstance(Type.GetTypeFromProgID("Scripting.Dictionary", true));
            try
            {
                var inspector = new RootDispatchInspector();
                Assert.That(inspector.Inspect(automation, RootNativeContract.Export), Is.EqualTo(RootMemberEvidence.Absent));
                Assert.That(inspector.Inspect(automation, new RootNativeContract("Count")), Is.EqualTo(RootMemberEvidence.SignatureUnverified));
                Assert.That(inspector.Inspect(automation, new RootNativeContract("Exists", typeof(object))), Is.EqualTo(RootMemberEvidence.SignatureUnverified));
                Assert.That((int)((dynamic)automation).Count, Is.Zero, "The caller's RCW remains usable and the object remains empty.");
            }
            finally { Marshal.ReleaseComObject(automation); }
        }
    }
}
