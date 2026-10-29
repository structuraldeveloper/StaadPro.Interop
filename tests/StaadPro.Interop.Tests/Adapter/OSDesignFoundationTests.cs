using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using NUnit.Framework;
using PackageVerification;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Design;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Design;
using StaadPro.Interop.Services.Design;
using Native = PackageVerification.DesignFoundationFixture.Native;
using Holder = PackageVerification.DesignFoundationFixture.Holder;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSDesignFoundationTests
    {
        private sealed class Holders : IDesignParameterHolderFactory
        {
            internal readonly List<Holder> Created = new List<Holder>();
            internal readonly List<object> Released = new List<object>();
            internal Exception CreateError, ReleaseError;
            public object Create()
            {
                if (CreateError != null)
                    throw CreateError;
                var holder = new Holder();
                Created.Add(holder);
                return holder;
            }

            public void Release(object holder)
            {
                Released.Add(holder);
                if (ReleaseError != null)
                    throw ReleaseError;
            }

            public object ReadIndexedValue(object holder, string propertyName, int index)
            {
                var value = (Holder)holder;
                switch (propertyName)
                {
                    case "Name": return value.get_Name(index);
                    case "Value": return value.get_Value(index);
                    case "Unit": return value.get_Unit(index);
                    case "Description": return value.get_Description(index);
                    case "Default": return value.get_Default(index);
                    default: throw new InvalidOperationException("Unknown holder property.");
                }
            }
        }

        private static DesignFoundationFixture.Root Root(Native n, int major = 26) => new DesignFoundationFixture.Root
        {
            Design = n,
            Major = major
        };
        private static OSDesignAdapter Api(OpenStaadWrapper w, Holders holders = null) => new OSDesignAdapter(w, holders ?? new Holders());
        private static DesignParameterAssignment Parameter(string name = "FYLD", int order = 0) => new DesignParameterAssignment(name, 250.25, new[] { 17, 41, 17 }, order);
        private static DesignCommandAssignment Command(int order = 0) => new DesignCommandAssignment(DesignCommandKind.CheckCode, new[] { 17, 41 }, order);
        private static MethodInfo[] Methods => typeof(IOSDesign).GetMethods().Where(m => !m.IsSpecialName).OrderBy(m => m.Name).ThenBy(m => string.Join(",", m.GetParameters().Select(p => p.ParameterType.Name))).ToArray();

        public sealed class IndexedName
        {
            public Exception Error;
            [System.Runtime.CompilerServices.IndexerName("Name")]
            public string this[int index] => Error == null ? "parameter-" + index : throw Error;
        }

        public sealed class AccessorMethodOnly
        {
            public string get_Name(int index) => "method-only";
        }

        [Test]
        public void NativeHolderReaderUsesTheIndexedPropertyAndRequestedIndex()
        {
            Assert.That(new DesignParameterHolderFactory().ReadIndexedValue(new IndexedName(), "Name", 2), Is.EqualTo("parameter-2"));
        }

        [Test]
        public void NativeHolderReaderPreservesOriginalGetterExceptions()
        {
            var original = new COMException("indexed getter");
            Assert.That(Assert.Throws<COMException>(() => new DesignParameterHolderFactory().ReadIndexedValue(new IndexedName { Error = original }, "Name", 0)), Is.SameAs(original));
        }

        [Test]
        public void NativeHolderReaderDoesNotGuessGeneratedAccessorMethodNames()
        {
            Assert.Throws<MissingMethodException>(() => new DesignParameterHolderFactory().ReadIndexedValue(new AccessorMethodOnly(), "Name", 0));
        }
        public static IEnumerable<TestCaseData> Ports => Enumerable.Range(0, Methods.Length).Select(i => new TestCaseData(i).SetName("DesignPort_" + Methods[i].Name + "_" + i));

        private static object[] Arguments(MethodInfo method) => method.GetParameters().Select(p => p.ParameterType == typeof(StaadDesignCode) ? (object)StaadDesignCode.ANSIAISC360Year16 : p.ParameterType == typeof(DesignParameterAssignment) ? Parameter() : p.ParameterType == typeof(DesignCommandAssignment) ? Command() : p.ParameterType == typeof(IEnumerable<DesignParameterAssignment>) ? new[] { Parameter() } : p.ParameterType == typeof(IEnumerable<DesignCommandAssignment>) ? new[] { Command() } : p.ParameterType == typeof(IEnumerable<int>) ? new[] { 17, 41, 17 } : (object)(p.Name.StartsWith("maximum") ? 100000 : p.Name == "designBriefId" ? 3 : 17)).ToArray();
        private static object Invoke(OSDesignAdapter api, int index)
        {
            try
            {
                return Methods[index].Invoke(api, Arguments(Methods[index]));
            }
            catch (TargetInvocationException e)
            {
                ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                throw;
            }
        }

        private static void Returns(Native n, string name, params object[] values) => n.Returns[name] = new Queue<object>(values);
        [TestCaseSource(nameof(Ports))]
        public void EveryPortHasMatchingComparativeXmlAndManagedSignatures(int index)
        {
            var method = Methods[index];
            var implementation = typeof(OSDesignAdapter).GetMethod(method.Name, method.GetParameters().Select(p => p.ParameterType).ToArray());
            Assert.That(implementation.ReturnType, Is.EqualTo(method.ReturnType));
            Assert.That(implementation.GetParameters().Select(p => p.Name), Is.EqualTo(method.GetParameters().Select(p => p.Name)));
            Assert.That(method.GetParameters().Any(p => p.IsOut || p.ParameterType.IsByRef || p.ParameterType == typeof(object)), Is.False);
            var xml = XDocument.Load(Path.ChangeExtension(method.DeclaringType.Assembly.Location, ".xml"));
            string prefix = "M:" + typeof(IOSDesign).FullName + "." + method.Name + "(";
            var candidates = xml.Descendants("member").Where(m => ((string)m.Attribute("name")).StartsWith(prefix));
            var docs = candidates.Single(m => m.Elements("param").Select(p => (string)p.Attribute("name")).SequenceEqual(method.GetParameters().Select(p => p.Name)));
            var adapter = xml.Descendants("member").Single(m => (string)m.Attribute("name") == ((string)docs.Attribute("name")).Replace(typeof(IOSDesign).FullName, typeof(OSDesignAdapter).FullName));
            Assert.That(adapter.Elements().Select(e => e.ToString()), Is.EqualTo(docs.Elements().Select(e => e.ToString())));
            foreach (string tag in new[]
            {
                "summary",
                "returns",
                "remarks",
                "example"
            }

            )
                Assert.That(docs.Element(tag)?.Value, Is.Not.Null.And.Not.Empty);
            Assert.That(docs.Element("remarks").Value, Does.Contain("When to use:").And.Contain("COM apartment"));
        }

        [TestCaseSource(nameof(Ports))]
        public void EveryPortRejectsStaleAndDisposedBindingsWithoutNativeWork(int index)
        {
            var n = new Native();
            var root = Root(n);
            var w = new OpenStaadWrapper(root);
            var api = Api(w);
            root.Design = new Native();
            w.RefreshComObjects();
            Assert.Throws<InvalidOperationException>(() => Invoke(api, index));
            w.Dispose();
            Assert.Throws<InvalidOperationException>(() => Invoke(api, index));
            Assert.That(n.Calls, Is.Empty);
        }

        [TestCaseSource(nameof(Ports))]
        public void EveryPortPropagatesOriginalTransportErrorAndDoesNotRetry(int index)
        {
            var n = new Native();
            var error = new COMException("transport", unchecked((int)0x80010108));
            n.OnCall = _ => throw error;
            using (var w = new OpenStaadWrapper(Root(n)))
                Assert.That(Assert.Throws<COMException>(() => Invoke(Api(w), index)), Is.SameAs(error));
            Assert.That(n.Calls.Count, Is.EqualTo(1));
        }

        [TestCase(24)]
        [TestCase(25)]
        [TestCase(26)]
        public void AllTenCommonPortsUseTheCapturedVersionBindingAndCallerThread(int version)
        {
            foreach (int index in Enumerable.Range(0, Methods.Length))
            {
                var n = new Native();
                using (var w = new OpenStaadWrapper(Root(n, version)))
                    Invoke(Api(w), index);
                Assert.That(n.Calls, Is.Not.Empty);
                Assert.That(n.Threads, Is.All.EqualTo(Thread.CurrentThread.ManagedThreadId));
            }
        }

        [TestCase(-1, false)]
        [TestCase(0, true)]
        [TestCase(37, true)]
        public void CreationPreservesNativeReferenceAndDistinctFailureSentinel(int raw, bool success)
        {
            var n = new Native();
            Returns(n, "CreateDesignBrief", raw);
            using (var w = new OpenStaadWrapper(Root(n)))
            {
                var result = Api(w).CreateDesignBrief(StaadDesignCode.ANSIAISC360Year16);
                Assert.That(result.IsSuccessful, Is.EqualTo(success));
                Assert.That(result.DesignBriefId, Is.EqualTo(success ? (int? )raw : null));
                Assert.That(n.Arguments.Single(), Is.EqualTo(new object[] { 1067 }));
            }
        }

        [TestCase("CreateDesignBrief", -2)]
        [TestCase("CreateDesignBrief", true)]
        [TestCase("CreateDesignBrief", "7")]
        [TestCase("GetDesignBriefCode", -1)]
        [TestCase("GetDesignBriefCode", false)]
        [TestCase("GetDesignBriefCode", 1067.0)]
        [TestCase("AssignDesignParameter", 1)]
        [TestCase("AssignDesignParameter", true)]
        [TestCase("AssignDesignCommand", -2)]
        [TestCase("GetMemberDesignParameters", 1)]
        [TestCase("GetMemberDesignParameters", false)]
        public void MalformedNativeResultsAreNeverCoercedIntoSuccess(string function, object raw)
        {
            var n = new Native();
            Returns(n, function, raw);
            var holders = new Holders();
            using (var w = new OpenStaadWrapper(Root(n)))
            {
                var api = Api(w, holders);
                Assert.Throws<InvalidOperationException>(() =>
                {
                    switch (function)
                    {
                        case "CreateDesignBrief":
                            api.CreateDesignBrief(StaadDesignCode.ANSIAISC360Year16);
                            break;
                        case "GetDesignBriefCode":
                            api.GetDesignBriefCode(3);
                            break;
                        case "AssignDesignCommand":
                            api.AssignDesignCommand(3, Command());
                            break;
                        case "AssignDesignParameter":
                            api.AssignDesignParameter(3, Parameter());
                            break;
                        default:
                            api.GetMemberDesignParameters(3, 17);
                            break;
                    }
                });
            }

            Assert.That(holders.Released.Count, Is.EqualTo(holders.Created.Count));
            Assert.That(n.Calls.Count, Is.EqualTo(1));
        }

        [TestCase(0)]
        [TestCase(1067)]
        [TestCase(99999)]
        public void BriefCodeSeparatesAbsenceKnownCodeAndUnknownCode(int raw)
        {
            var n = new Native();
            Returns(n, "GetDesignBriefCode", raw);
            using (var w = new OpenStaadWrapper(Root(n)))
            {
                var result = Api(w).GetDesignBriefCode(0);
                Assert.That(result.DesignBriefId, Is.Zero);
                if (raw == 0)
                {
                    Assert.That(result.Status, Is.EqualTo(DesignOperationStatus.NotFound));
                    Assert.Throws<InvalidOperationException>(() =>
                    {
                        var _ = result.Data;
                    });
                }
                else
                {
                    Assert.That(result.Data.NativeCode, Is.EqualTo(raw));
                    Assert.That(result.Data.KnownCode, Is.EqualTo(raw == 1067 ? (StaadDesignCode? )StaadDesignCode.ANSIAISC360Year16 : null));
                }
            }
        }

        [TestCase("en-US")]
        [TestCase("fr-FR")]
        [TestCase("de-DE")]
        public void ParametersUseInvariantValuesOwnedSelectionsAndExactNativeArgumentOrder(string culture)
        {
            var before = CultureInfo.CurrentCulture;
            var ids = new[]
            {
                17,
                41,
                17
            };
            DesignParameterAssignment request;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo(culture);
                request = new DesignParameterAssignment("FYLD", 250.25, ids);
            }
            finally
            {
                CultureInfo.CurrentCulture = before;
            }

            ids[0] = 999;
            var n = new Native();
            n.OnCall = _ => ((int[])n.Arguments.Single()[3])[0] = 888;
            using (var w = new OpenStaadWrapper(Root(n)))
            {
                var result = Api(w).AssignDesignParameter(0, request);
                Assert.That(result.MemberIds, Is.EqualTo(new[] { 17, 41, 17 }));
            }

            Assert.That(request.MemberIds, Is.EqualTo(new[] { 17, 41, 17 }));
            Assert.That(n.Arguments.Single().Take(3), Is.EqualTo(new object[] { 0, "FYLD", "250.25" }));
        }

        [TestCase(true, "1")]
        [TestCase(false, "0")]
        [TestCase(17, "17")]
        [TestCase("1 2 3", "1 2 3")]
        public void ExplicitParameterFormattingRetainsSupportedScalarIntent(object value, string expected)
        {
            Assert.That(new DesignParameterAssignment("LY", value, new[] { 17 }).NativeValue, Is.EqualTo(expected));
        }

        [Test]
        public void EnumsUseTheirNumericValueWithoutEnumNameReflection()
        {
            Assert.That(new DesignParameterAssignment("TRACK", DesignGroupProperty.SectionModulusZ, new[] { 17 }).NativeValue, Is.EqualTo("3"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("bad\nvalue")]
        [TestCase(double.NaN)]
        [TestCase(double.PositiveInfinity)]
        public void InvalidParameterValuesAreRejectedBeforeMutation(object value)
        {
            Assert.Throws(Is.InstanceOf<ArgumentException>(), () => new DesignParameterAssignment("LY", value, new[] { 17 }));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("FYLD 1")]
        [TestCase("1LY")]
        [TestCase("LY\n")]
        public void InvalidParameterKeywordsCannotInjectInstructions(string name)
        {
            Assert.Throws(Is.InstanceOf<ArgumentException>(), () => new DesignParameterAssignment(name, 1, new[] { 17 }));
        }

        [TestCase(DesignCommandKind.CheckCode, "CHECK CODE")]
        [TestCase(DesignCommandKind.FixedGroup, "FIXED GROUP")]
        [TestCase(DesignCommandKind.MemberTakeOff, "MEMBER TAKE OFF")]
        [TestCase(DesignCommandKind.Select, "SELECT")]
        [TestCase(DesignCommandKind.SelectOptimized, "SELECT OPTIMIZED")]
        [TestCase(DesignCommandKind.SelectWeld, "SELECT WELD")]
        [TestCase(DesignCommandKind.SelectWeldTruss, "SELECT WELD TRUSS")]
        [TestCase(DesignCommandKind.TakeOff, "TAKE OFF")]
        public void CommandsUseDocumentedNamesAndAnEmptyValueForNonGroupCommands(DesignCommandKind command, string name)
        {
            var n = new Native();
            using (var w = new OpenStaadWrapper(Root(n)))
                Api(w).AssignDesignCommand(3, new DesignCommandAssignment(command, new[] { 17 }));
            Assert.That(n.Arguments.Single(), Is.EqualTo(new object[] { 3, name, "", new[] { 17 } }));
        }

        [TestCase(25, DesignGroupProperty.None, "0")]
        [TestCase(26, DesignGroupProperty.Area, "1")]
        [TestCase(25, DesignGroupProperty.SectionModulusY, "2")]
        [TestCase(26, DesignGroupProperty.SectionModulusZ, "3")]
        public void GroupUsesOnlyTheQualifiedAcquiredProfile(int version, DesignGroupProperty option, string text)
        {
            var n = new Native();
            using (var w = new OpenStaadWrapper(Root(n, version)))
                Api(w).AssignDesignCommand(3, new DesignCommandAssignment(DesignCommandKind.Group, new[] { 17 }, groupProperty: option));
            Assert.That(n.Arguments.Single()[2], Is.EqualTo(text));
        }

        [TestCase(24)]
        [TestCase(27)]
        [TestCase(0)]
        public void UnqualifiedGroupRejectsTheWholeBatchBeforeAnyMutation(int version)
        {
            var n = new Native();
            using (var w = new OpenStaadWrapper(Root(n, version)))
                Assert.Throws<NotSupportedException>(() => Api(w).AssignDesignCommands(3, new[] { Command(), new DesignCommandAssignment(DesignCommandKind.Group, new[] { 17 }) }));
            Assert.That(n.Calls, Is.Empty);
        }

        [Test]
        public void ParameterBatchesRetainStableOrderPartialFailureAndUnattemptedIdentity()
        {
            var n = new Native();
            Returns(n, "AssignDesignParameter", 0, -1);
            using (var w = new OpenStaadWrapper(Root(n)))
            {
                var result = Api(w).AssignDesignParameters(3, new[] { Parameter("FYLD", 2), Parameter("LY", 1), Parameter("LZ", 1) });
                Assert.That(result.Items.Select(i => i.Name), Is.EqualTo(new[] { "LY", "LZ", "FYLD" }));
                Assert.That(result.Items.Select(i => i.InputIndex), Is.EqualTo(new int? [] { 1, 2, 0 }));
                Assert.That(result.Items.Select(i => i.Status), Is.EqualTo(new[] { DesignOperationStatus.Success, DesignOperationStatus.NativeFailure, DesignOperationStatus.NotAttempted }));
                Assert.That(result.SucceededCount, Is.EqualTo(1));
                Assert.That(result.FailedCount, Is.EqualTo(1));
                Assert.That(result.NotAttemptedCount, Is.EqualTo(1));
                Assert.That(result.IsSuccessful, Is.False);
                Assert.That(result.Items.Last().NativeReturnCode, Is.Null);
                Assert.That(n.Calls.Count, Is.EqualTo(2));
            }
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(12)]
        public void CreateAndAssignStopsOnCreationFailureAndRetainsCreatedBriefOnLaterFailure(int created)
        {
            var n = new Native();
            Returns(n, "CreateDesignBrief", created);
            Returns(n, "AssignDesignParameter", -1);
            using (var w = new OpenStaadWrapper(Root(n)))
            {
                var result = Api(w).AssignDesignParameters(StaadDesignCode.ANSIAISC360Year16, new[] { Parameter(), Parameter("LY") });
                Assert.That(result.Creation.NativeReturnCode, Is.EqualTo(created));
                Assert.That(result.DesignBriefId, Is.EqualTo(created < 0 ? (int? )null : created));
                Assert.That(result.NotAttemptedCount, Is.EqualTo(created < 0 ? 2 : 1));
                Assert.That(n.Calls.Count, Is.EqualTo(created < 0 ? 1 : 2));
            }
        }

        [Test]
        public void CompleteValidationPrecedesCreationAndEmptyInputsMakeNoNativeCalls()
        {
            var n = new Native();
            var holders = new Holders();
            using (var w = new OpenStaadWrapper(Root(n)))
            {
                var api = Api(w, holders);
                Assert.Throws<ArgumentNullException>(() => api.AssignDesignParameters(StaadDesignCode.ANSIAISC360Year16, new[] { Parameter(), null }));
                Assert.That(api.AssignDesignParameters(StaadDesignCode.ANSIAISC360Year16, new DesignParameterAssignment[0]).IsNoOperation, Is.True);
                Assert.That(api.AssignDesignCommands(3, new DesignCommandAssignment[0]).IsNoOperation, Is.True);
                Assert.That(api.GetDesignBriefCodes(new int[0]), Is.Empty);
                Assert.That(api.GetMemberDesignParameters(3, new int[0]), Is.Empty);
                Assert.Throws<ArgumentOutOfRangeException>(() => api.GetMemberDesignParameters(3, new[] { 17, 0 }));
                Assert.Throws<ArgumentOutOfRangeException>(() => api.GetDesignBriefCodes(new[] { 1, -1 }));
                Assert.Throws<ArgumentException>(() => api.GetDesignBriefCodes(new[] { 1, 2 }, 1));
            }

            Assert.That(n.Calls, Is.Empty);
            Assert.That(holders.Created, Is.Empty);
        }

        private sealed class Once<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> values;
            private int enumerations;
            internal Once(IEnumerable<T> values)
            {
                this.values = values;
            }

            public IEnumerator<T> GetEnumerator()
            {
                if (++enumerations != 1)
                    throw new InvalidOperationException("Enumerated twice");
                return values.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Test]
        public void CollectionsAreEnumeratedOnceAndQueryBatchesPreserveOrderDuplicatesAndFailures()
        {
            var n = new Native();
            Returns(n, "GetDesignBriefCode", 1067, 0, 99999);
            Returns(n, "GetMemberDesignParameters", 0, -1, 0);
            var holders = new Holders();
            using (var w = new OpenStaadWrapper(Root(n)))
            {
                var api = Api(w, holders);
                var codes = api.GetDesignBriefCodes(new Once<int>(new[] { 3, 8, 3 }));
                Assert.That(codes.Select(c => c.DesignBriefId), Is.EqualTo(new[] { 3, 8, 3 }));
                Assert.That(codes[1].Status, Is.EqualTo(DesignOperationStatus.NotFound));
                var members = api.GetMemberDesignParameters(0, new Once<int>(new[] { 17, 41, 17 }));
                Assert.That(members.Select(c => c.MemberId), Is.EqualTo(new int? [] { 17, 41, 17 }));
                Assert.That(members[1].Status, Is.EqualTo(DesignOperationStatus.NativeFailure));
                Assert.That(members[2].IsSuccessful, Is.True);
                Assert.That(holders.Created.Count, Is.EqualTo(3));
                Assert.That(holders.Released, Is.EqualTo(holders.Created));
                api.AssignDesignParameters(3, new Once<DesignParameterAssignment>(new[] { Parameter() }));
            }
        }

        [Test]
        public void ParameterSnapshotsAreOwnedAndAllNativeTextAndDefaultEvidenceAreRetained()
        {
            var n = new Native();
            n.Fill = h =>
            {
                h.Count = 1;
                h.Rows.Add(new DesignFoundationFixture.Row { Value = "2.50E+2", Unit = "", Description = "", Default = -1 });
            };
            var holders = new Holders();
            using (var w = new OpenStaadWrapper(Root(n)))
            {
                var result = Api(w, holders).GetMemberDesignParameters(0, 17);
                holders.Created.Single().Rows[0].Value = "changed";
                var row = result.Data.Parameters.Single();
                Assert.That(row.Value, Is.EqualTo("2.50E+2"));
                Assert.That(row.Unit, Is.Empty);
                Assert.That(row.Index, Is.Zero);
                Assert.That(row.IsDefault, Is.True);
                Assert.That(row.NativeDefaultFlag, Is.EqualTo(-1));
                Assert.That(holders.Released.Single(), Is.SameAs(holders.Created.Single()));
                Assert.Throws<NotSupportedException>(() => ((IList<MemberDesignParameterInfo>)result.Data.Parameters).Clear());
            }
        }

        [TestCase(-1)]
        [TestCase(100001)]
        [TestCase(true)]
        [TestCase("1")]
        public void InvalidOrUnwrittenHolderCountsAreRejectedAndHolderIsReleased(object count)
        {
            var n = new Native();
            n.Fill = h => h.Count = count;
            var holders = new Holders();
            using (var w = new OpenStaadWrapper(Root(n)))
                Assert.Throws<InvalidOperationException>(() => Api(w, holders).GetMemberDesignParameters(3, 17));
            Assert.That(holders.Released, Is.EqualTo(holders.Created));
        }

        [Test]
        public void MalformedHolderCountIsRejectedWhileAcknowledgedZeroIsValid()
        {
            var n = new Native();
            var holders = new Holders();
            n.Fill = _ =>
            {
            };
            using (var w = new OpenStaadWrapper(Root(n)))
            {
                var api = Api(w, holders);
                Assert.Throws<InvalidOperationException>(() => api.GetMemberDesignParameters(3, 17));
                n.Fill = h => h.Count = 0;
                Assert.That(api.GetMemberDesignParameters(3, 17).Data.Parameters, Is.Empty);
            }

            Assert.That(holders.Released, Is.EqualTo(holders.Created));
        }

        [TestCase("Name")]
        [TestCase("Value")]
        [TestCase("Unit")]
        [TestCase("Description")]
        [TestCase("Default")]
        public void MalformedRowsNeverPublishPartialDataAndAlwaysReleaseHolder(string field)
        {
            var n = new Native();
            n.Fill = h =>
            {
                h.Count = 1;
                var row = new DesignFoundationFixture.Row();
                typeof(DesignFoundationFixture.Row).GetField(field).SetValue(row, null);
                h.Rows.Add(row);
            };
            var holders = new Holders();
            using (var w = new OpenStaadWrapper(Root(n)))
                Assert.Throws<InvalidOperationException>(() => Api(w, holders).GetMemberDesignParameters(3, 17));
            Assert.That(holders.Released, Is.EqualTo(holders.Created));
        }

        [Test]
        public void HolderReleaseCannotHideTheOriginalTransportException()
        {
            var original = new COMException("original");
            var n = new Native
            {
                OnCall = _ => throw original
            };
            var cleanup = new InvalidOperationException("cleanup");
            var holders = new Holders
            {
                ReleaseError = cleanup
            };
            using (var w = new OpenStaadWrapper(Root(n)))
                Assert.That(Assert.Throws<COMException>(() => Api(w, holders).GetMemberDesignParameters(3, 17)), Is.SameAs(original));
            Assert.That(original.Data["DesignHolderCleanupError"], Is.SameAs(cleanup));
            Assert.That(holders.Released.Count, Is.EqualTo(1));
        }

        [Test]
        public void ActivationFailuresPropagateWithoutNativeDispatchOrRootFallback()
        {
            var original = new COMException("class not registered", unchecked((int)0x80040154));
            var n = new Native();
            var holders = new Holders
            {
                CreateError = original
            };
            using (var w = new OpenStaadWrapper(Root(n)))
                Assert.That(Assert.Throws<COMException>(() => Api(w, holders).GetMemberDesignParameters(3, 17)), Is.SameAs(original));
            Assert.That(n.Calls, Is.Empty);
            Assert.That(holders.Released, Is.Empty);
        }

        [Test]
        public void BindingChangeAfterCreationStopsBeforeAnyAssignment()
        {
            var n = new Native();
            var root = Root(n);
            using (var w = new OpenStaadWrapper(root))
            {
                n.OnCall = _ =>
                {
                    root.Design = new Native();
                    w.RefreshComObjects();
                };
                Assert.Throws<InvalidOperationException>(() => Api(w).AssignDesignParameters(StaadDesignCode.ANSIAISC360Year16, new[] { Parameter() }));
            }

            Assert.That(n.Calls, Is.EqualTo(new[] { "CreateDesignBrief" }));
        }

        [Test]
        public void BindingChangeDuringHolderExtractionAndCountChangeAreRejected()
        {
            var n = new Native();
            var root = Root(n);
            var holders = new Holders();
            using (var w = new OpenStaadWrapper(root))
            {
                n.Fill = h =>
                {
                    h.Count = 1;
                    h.Rows.Add(new DesignFoundationFixture.Row());
                    h.OnRead = () => h.Count = 0;
                };
                Assert.Throws<InvalidOperationException>(() => Api(w, holders).GetMemberDesignParameters(3, 17));
                n.Fill = h =>
                {
                    h.Count = 1;
                    h.Rows.Add(new DesignFoundationFixture.Row());
                    h.OnRead = () =>
                    {
                        root.Design = new Native();
                        w.RefreshComObjects();
                    };
                };
                Assert.Throws<InvalidOperationException>(() => Api(w, holders).GetMemberDesignParameters(3, 17));
            }

            Assert.That(holders.Released, Is.EqualTo(holders.Created));
        }

        [Test]
        public void NativeHolderActivationRejectsAnMtaCallerBeforeComActivation()
        {
            Exception failure = null;
            var thread = new Thread(() =>
            {
                try
                {
                    new DesignParameterHolderFactory().Create();
                }
                catch (Exception error)
                {
                    failure = error;
                }
            });
            thread.SetApartmentState(ApartmentState.MTA);
            thread.Start();
            thread.Join();
            Assert.That(failure, Is.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void DesignGateSerializesManagedFixtureCallsWithoutSchedulingNativeWork()
        {
            var n = new Native();
            int active = 0, peak = 0;
            n.OnCall = _ =>
            {
                int value = Interlocked.Increment(ref active);
                peak = Math.Max(peak, value);
                Thread.Sleep(10);
                Interlocked.Decrement(ref active);
            };
            using (var w = new OpenStaadWrapper(Root(n)))
            {
                var api = Api(w);
                Task.WaitAll(Task.Run(() => api.GetDesignBriefCode(1)), Task.Run(() => api.GetDesignBriefCode(2)));
            }

            Assert.That(peak, Is.EqualTo(1));
            Assert.That(n.Calls.Count, Is.EqualTo(2));
        }

        [Test]
        public void ExactPortSurfaceHasTenMethodsAndDoesNotPretendExtendedGroupIsAvailable()
        {
            Assert.That(Methods.Length, Is.EqualTo(10));
            Assert.That(typeof(IOSDesign).GetMethod("AssignDesignGroup"), Is.Null);
            Assert.That(Enum.GetNames(typeof(StaadDesignCode)).Length, Is.EqualTo(50));
            Assert.Throws<ArgumentException>(() => new DesignParameterAssignment("LY", new object (), new[] { 17 }));
            Assert.Throws<ArgumentOutOfRangeException>(() => new DesignCommandAssignment(DesignCommandKind.Select, new[] { 17 }, groupProperty: DesignGroupProperty.Area));
            Assert.Throws<ArgumentOutOfRangeException>(() => new DesignCommandAssignment((DesignCommandKind)99, new[] { 17 }));
        }

        [TestCaseSource(nameof(Ports))]
        public void PostCallBindingReplacementAbortsEveryPortWithoutRetry(int index)
        {
            var n = new Native();
            var root = Root(n);
            var holders = new Holders();
            using (var w = new OpenStaadWrapper(root))
            {
                var api = Api(w, holders);
                n.OnCall = _ =>
                {
                    root.Design = new Native();
                    w.RefreshComObjects();
                };
                Assert.Throws<InvalidOperationException>(() => Invoke(api, index));
            }

            Assert.That(n.Calls.Count, Is.EqualTo(1));
            Assert.That(holders.Released.Count, Is.EqualTo(holders.Created.Count));
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(100001)]
        public void InvalidBoundsFailBeforeFactoryActivationOrNativeCalls(int limit)
        {
            var n = new Native();
            var holders = new Holders();
            using (var w = new OpenStaadWrapper(Root(n)))
            {
                var api = Api(w, holders);
                Assert.Throws<ArgumentOutOfRangeException>(() => api.GetMemberDesignParameters(1, 17, limit));
                Assert.Throws<ArgumentOutOfRangeException>(() => api.GetMemberDesignParameters(1, new[] { 17 }, limit));
                Assert.Throws<ArgumentOutOfRangeException>(() => api.AssignDesignParameters(1, new[] { Parameter() }, limit));
                Assert.Throws<ArgumentOutOfRangeException>(() => api.AssignDesignCommands(1, new[] { Command() }, limit));
            }

            Assert.That(holders.Created, Is.Empty);
            Assert.That(n.Calls, Is.Empty);
        }

        [TestCase(StaadDesignCode.Unknown)]
        [TestCase((StaadDesignCode)99999)]
        public void InvalidCreationCodeCannotMutateOrCreateAPartialBrief(StaadDesignCode code)
        {
            var n = new Native();
            using (var w = new OpenStaadWrapper(Root(n)))
            {
                var api = Api(w);
                Assert.Throws<ArgumentOutOfRangeException>(() => api.CreateDesignBrief(code));
                Assert.Throws<ArgumentOutOfRangeException>(() => api.AssignDesignParameters(code, new[] { Parameter() }));
            }

            Assert.That(n.Calls, Is.Empty);
        }

        [Test]
        public void OversizedSuccessfulHolderDoesNotExtractAnyRows()
        {
            var n = new Native();
            var holders = new Holders();
            bool read = false;
            n.Fill = h =>
            {
                h.Count = 2;
                h.OnRead = () => read = true;
            };
            using (var w = new OpenStaadWrapper(Root(n)))
                Assert.Throws<InvalidOperationException>(() => Api(w, holders).GetMemberDesignParameters(1, 17, 1));
            Assert.That(read, Is.False);
            Assert.That(holders.Released, Is.EqualTo(holders.Created));
        }

        [Test]
        public void PublicDesignModelsEnumsPropertiesAndConstructorsHaveXmlDocumentation()
        {
            var assembly = typeof(DesignParameterAssignment).Assembly;
            var xml = XDocument.Load(Path.ChangeExtension(assembly.Location, ".xml"));
            foreach (Type type in assembly.GetExportedTypes().Where(t => t.Namespace == "StaadPro.Interop.Models.Design" || t.Namespace == "StaadPro.Interop.Enums.Design" || t == typeof(StaadDesignCode)))
            {
                Assert.That(xml.Descendants("member").Single(m => (string)m.Attribute("name") == "T:" + type.FullName).Element("summary")?.Value, Is.Not.Null.And.Not.Empty);
                foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    Assert.That(xml.Descendants("member").Single(m => (string)m.Attribute("name") == "P:" + type.FullName + "." + property.Name).Element("summary")?.Value, Is.Not.Null.And.Not.Empty);
                if (type.IsEnum)
                    foreach (string name in Enum.GetNames(type))
                        Assert.That(xml.Descendants("member").Single(m => (string)m.Attribute("name") == "F:" + type.FullName + "." + name).Element("summary")?.Value, Is.Not.Null.And.Not.Empty);
                foreach (var constructor in type.GetConstructors())
                    Assert.That(xml.Descendants("member").Count(m => ((string)m.Attribute("name")).StartsWith("M:" + type.FullName + ".#ctor(")), Is.EqualTo(type.GetConstructors().Length));
            }
        }
    }
}
