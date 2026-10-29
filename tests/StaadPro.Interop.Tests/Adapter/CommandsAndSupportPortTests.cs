using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PackageVerification;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.AnalysisCommands;
using StaadPro.Interop.Enums.Support;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Commands;
using StaadPro.Interop.Models.Support;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class CommandsAndSupportPortTests
    {
        private static JArray Contracts => (JArray)JObject.Parse(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "ContractReview", "CommandsAndSupportPortContractIndex.json")))["functions"];
        public static IEnumerable<TestCaseData> Cases => Contracts.Select(c => new TestCaseData((string)c["domain"], (string)c["name"], (string)c["native"]));
        public static IEnumerable<TestCaseData> AckCases => Cases.Where(c => !VoidNames.Contains((string)c.Arguments[1]));

        private static readonly string[] VoidNames =
        {
            "PerformAnalysis",
            "PerformPDeltaAnalysisNoConverge",
            "PerformPDeltaAnalysisEx",
            "PerformPDeltaAnalysis",
            "PerformCableAnalysis",
            "PerformBucklingAnalysis"
        };
        private static Type Api(string domain) => domain == "Commands" ? typeof(IOSCommands) : typeof(IOSSupport);
        private static object Receiver(OpenStaadWrapper w, string domain) => domain == "Commands" ? (object)w.Commands : w.Support;
        private static object[] Args(string domain, string name) => Api(domain).GetMethod(name).GetParameters().Select(p => p.ParameterType == typeof(PDeltaAnalysisOptions) ? (object)new PDeltaAnalysisOptions() : p.ParameterType == typeof(CableAnalysisOptions) ? new CableAnalysisOptions() : p.ParameterType == typeof(DirectAnalysisOptions) ? new DirectAnalysisOptions() : p.ParameterType == typeof(NonlinearAnalysisOptions) ? new NonlinearAnalysisOptions() : p.ParameterType == typeof(PrintOption) ? PrintOption.NoPrint : p.ParameterType == typeof(PDeltaDisplacementEffect) ? PDeltaDisplacementEffect.SmallDelta : p.ParameterType == typeof(BucklingAnalysisMethod) ? BucklingAnalysisMethod.Eigen : p.ParameterType == typeof(SoftStoryDesignCode) ? SoftStoryDesignCode.Is1893_2016 : p.ParameterType == typeof(IrregularitiesDesignCode) ? IrregularitiesDesignCode.Is1893_2016 : p.ParameterType == typeof(FoundationSupportDirection) ? FoundationSupportDirection.Y : p.ParameterType == typeof(FoundationSpringType) ? FoundationSpringType.None : p.ParameterType == typeof(SupportEntityKind) ? SupportEntityKind.Nodes : p.ParameterType == typeof(IEnumerable<int>) ? new[] { 17, 41, 17 } : p.ParameterType == typeof(bool) ? (object)true : p.ParameterType == typeof(double) ? (object)2.0 : p.Name == "maximumResults" ? 100 : 15).ToArray();
        private static object Invoke(object receiver, string domain, string name, object[] args = null)
        {
            try
            {
                return Api(domain).GetMethod(name).Invoke(receiver, args ?? Args(domain, name));
            }
            catch (TargetInvocationException e)
            {
                ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                throw;
            }
        }

        private static CommandsSupportFixture.Root Root(CommandsSupportFixture.Native n, int major = 26) => new CommandsSupportFixture.Root
        {
            Command = n,
            Support = n,
            Major = major
        };
        private static object Prop(object value, string name) => value.GetType().GetProperty(name).GetValue(value);
        private static void Returns(CommandsSupportFixture.Native n, string op, params object[] values) => n.Returns[op] = new Queue<object>(values);
        [TestCaseSource(nameof(Cases))]
        public void EveryPortDispatchesOnlyItsExactNativeTargetOnTheCallingThread(string domain, string name, string nativeName)
        {
            foreach (int version in new[]
            {
                24,
                25,
                26
            }

            )
            {
                var n = new CommandsSupportFixture.Native();
                using (var w = new OpenStaadWrapper(Root(n, version)))
                {
                    var result = Invoke(Receiver(w, domain), domain, name);
                    Assert.That(n.Calls, Is.EqualTo(new[] { nativeName }));
                    Assert.That(n.Threads, Is.EqualTo(new[] { Thread.CurrentThread.ManagedThreadId }));
                    Assert.That(Prop(result, "NativeFunction"), Is.EqualTo(nativeName));
                    Assert.That(Prop(result, "IsSuccessful"), Is.EqualTo(!VoidNames.Contains(name)));
                    if (VoidNames.Contains(name))
                        Assert.That(Prop(result, "Status"), Is.EqualTo(CommandSubmissionStatus.InvokedWithoutAcknowledgement));
                }
            }
        }

        [TestCaseSource(nameof(Cases))]
        public void EveryPortHasMatchingPublicSignaturesAndCompleteComparativeXml(string domain, string name, string nativeName)
        {
            var api = Api(domain);
            var implementation = domain == "Commands" ? typeof(OSCommandsAdapter) : typeof(OSSupportAdapter);
            var method = api.GetMethod(name);
            var actual = implementation.GetMethod(name);
            Assert.That(actual.ReturnType, Is.EqualTo(method.ReturnType));
            Assert.That(actual.GetParameters().Select(p => p.Name), Is.EqualTo(method.GetParameters().Select(p => p.Name)));
            Assert.That(method.GetParameters().Any(p => p.IsOut || p.ParameterType.IsByRef || p.ParameterType == typeof(object)), Is.False);
            var xml = XDocument.Load(Path.ChangeExtension(api.Assembly.Location, ".xml"));
            var docs = new[]
            {
                api,
                implementation
            }.Select(type =>
            {
                string prefix = "M:" + type.FullName + "." + name;
                return xml.Descendants("member").Single(m => (string)m.Attribute("name") == prefix || ((string)m.Attribute("name")).StartsWith(prefix + "("));
            }).ToArray();
            Assert.That(docs[0].Elements().Select(e => e.ToString()), Is.EqualTo(docs[1].Elements().Select(e => e.ToString())));
            foreach (var tag in new[]
            {
                "summary",
                "returns",
                "remarks",
                "example"
            }

            )
                Assert.That(docs[0].Element(tag)?.Value, Is.Not.Null.And.Not.Empty);
            Assert.That(docs[0].Elements("param").Select(p => (string)p.Attribute("name")), Is.EqualTo(method.GetParameters().Select(p => p.Name)));
            Assert.That(docs[0].Element("remarks").Value, Does.Contain("When to use:").And.Contain("COM apartment"));
            var contract = Contracts.Single(c => (string)c["domain"] == domain && (string)c["name"] == name);
            Assert.That(docs[0].Element("remarks").Value, Does.Contain((string)contract["detail"]));
            Assert.That(docs[0].Element("example").Value, Does.Contain("bool applyChanges = false"));
        }

        [TestCaseSource(nameof(Cases))]
        public void EveryPortRejectsDisposedAndReplacedBindingsBeforeDispatch(string domain, string name, string nativeName)
        {
            var n = new CommandsSupportFixture.Native();
            var root = Root(n);
            var w = new OpenStaadWrapper(root);
            var receiver = Receiver(w, domain);
            w.Dispose();
            Assert.Throws<InvalidOperationException>(() => Invoke(receiver, domain, name));
            Assert.That(n.Calls, Is.Empty);
            root = Root(n);
            using (w = new OpenStaadWrapper(root))
            {
                receiver = Receiver(w, domain);
                if (domain == "Commands")
                    root.Command = new CommandsSupportFixture.Native();
                else
                    root.Support = new CommandsSupportFixture.Native();
                w.RefreshComObjects();
                Assert.Throws<InvalidOperationException>(() => Invoke(receiver, domain, name));
                Assert.That(n.Calls, Is.Empty);
            }
        }

        [TestCaseSource(nameof(Cases))]
        public void EveryPortStopsAfterPostDispatchBindingReplacementWithoutRetry(string domain, string name, string nativeName)
        {
            var n = new CommandsSupportFixture.Native();
            var root = Root(n);
            using (var w = new OpenStaadWrapper(root))
            {
                var receiver = Receiver(w, domain);
                n.OnCall = op =>
                {
                    n.OnCall = null;
                    if (domain == "Commands")
                        root.Command = new CommandsSupportFixture.Native();
                    else
                        root.Support = new CommandsSupportFixture.Native();
                    w.RefreshComObjects();
                };
                Assert.Throws<InvalidOperationException>(() => Invoke(receiver, domain, name));
                Assert.That(n.Calls, Is.EqualTo(new[] { nativeName }));
            }
        }

        [TestCaseSource(nameof(Cases))]
        public void EveryPortPropagatesTheOriginalTransportErrorWithoutRetry(string domain, string name, string nativeName)
        {
            var n = new CommandsSupportFixture.Native();
            var error = new COMException("transport");
            n.OnCall = op => throw error;
            using (var w = new OpenStaadWrapper(Root(n)))
            {
                Assert.That(Assert.Throws<COMException>(() => Invoke(Receiver(w, domain), domain, name)), Is.SameAs(error));
                Assert.That(n.Calls.Count, Is.EqualTo(1));
            }
        }

        [TestCaseSource(nameof(AckCases))]
        public void AcknowledgementFailuresAndCreationZeroNeverBecomeSuccess(string domain, string name, string nativeName)
        {
            var n = new CommandsSupportFixture.Native();
            Returns(n, nativeName, 0);
            using (var w = new OpenStaadWrapper(Root(n)))
            {
                var r = Invoke(Receiver(w, domain), domain, name);
                Assert.That(Prop(r, "IsSuccessful"), Is.False);
                Assert.That(Prop(r, "NativeReturnCode"), Is.EqualTo(0));
                Assert.That(Prop(r, "NativeBooleanReturn"), Is.Null);
                if (name == "DeleteAllAnalysisCommands")
                    Assert.That(Prop(r, "Status"), Is.EqualTo(CommandSubmissionStatus.NoCommandsPresent));
                if (name.StartsWith("Create"))
                    Assert.That(Prop(r, "SupportId"), Is.Null);
            }
        }

        [TestCaseSource(nameof(AckCases))]
        public void MalformedAcknowledgementsAreRejectedWithoutFallback(string domain, string name, string nativeName)
        {
            var n = new CommandsSupportFixture.Native();
            using (var w = new OpenStaadWrapper(Root(n)))
                foreach (object raw in new object[]
                {
                    null,
                    DBNull.Value,
                    "1",
                    1.0,
                    2.5
                }

                )
                {
                    Returns(n, nativeName, raw);
                    Assert.Throws<InvalidOperationException>(() => Invoke(Receiver(w, domain), domain, name));
                }
        }

        public static IEnumerable<TestCaseData> InvalidInputs => Cases.SelectMany(c => Api((string)c.Arguments[0]).GetMethod((string)c.Arguments[1]).GetParameters().Select((p, i) => p.ParameterType == typeof(bool) ? null : new TestCaseData((string)c.Arguments[0], (string)c.Arguments[1], i)).Where(c => c != null));

        [TestCaseSource(nameof(InvalidInputs))]
        public void EveryParameterRejectsAnInvalidValueBeforeNativeDispatch(string domain, string name, int index)
        {
            var n = new CommandsSupportFixture.Native();
            var args = Args(domain, name);
            var p = Api(domain).GetMethod(name).GetParameters()[index];
            if (p.ParameterType == typeof(bool))
                return;
            args[index] = p.ParameterType.IsEnum ? Enum.ToObject(p.ParameterType, 999) : p.ParameterType == typeof(double) ? (object)double.NaN : p.ParameterType == typeof(int) ? 0 : null;
            using (var w = new OpenStaadWrapper(Root(n)))
            {
                Assert.That(() => Invoke(Receiver(w, domain), domain, name, args), Throws.InstanceOf<ArgumentException>());
                Assert.That(n.Calls, Is.Empty);
            }
        }

        [Test]
        public void PublicPortCountsMatchAndDeprecatedOrGatedEntriesRemainUnavailable()
        {
            Assert.That(Contracts.Count, Is.EqualTo(25));
            Assert.That(typeof(IOSCommands).GetMethods().Count(m => !m.IsSpecialName), Is.EqualTo(17));
            Assert.That(typeof(IOSSupport).GetMethods().Count(m => !m.IsSpecialName), Is.EqualTo(27));
            foreach (string name in new[]
            {
                "PerformPDeltaAnalysisConverge",
                "CreateSteelDesignCommand"
            }

            )
                Assert.That(typeof(IOSCommands).GetMethod(name), Is.Null);
            foreach (string name in new[]
            {
                "CreatePlateMat",
                "GetPlateMat",
                "RemovePlateMatFromPlate"
            }

            )
                Assert.That(typeof(IOSSupport).GetMethod(name), Is.Null);
        }

        private static object[] Expected(string name)
        {
            switch (name)
            {
                case "PerformAnalysis":
                    return new object[]
                    {
                        0
                    };
                case "PerformPDeltaAnalysisNoConverge":
                case "PerformCableAnalysis":
                case "PerformBucklingAnalysis":
                    return new object[]
                    {
                        15,
                        0
                    };
                case "PerformPDeltaAnalysisEx":
                    return new object[]
                    {
                        15,
                        0,
                        1,
                        (short)1
                    };
                case "PerformPDeltaAnalysis":
                    return new object[]
                    {
                        15,
                        0,
                        1,
                        (short)0
                    };
                case "PerformCableAnalysisEx":
                    return new object[]
                    {
                        (short)0,
                        null,
                        new double[]
                        {
                            145,
                            300,
                            1e-4,
                            0,
                            1,
                            1,
                            0
                        },
                        0
                    };
                case "PerformBucklingAnalysisEx":
                    return new object[]
                    {
                        1,
                        15,
                        0
                    };
                case "PerformDirectAnalysis":
                    return new object[]
                    {
                        1,
                        new double[]
                        {
                            .01,
                            .01,
                            1,
                            15
                        },
                        new short[]
                        {
                            0,
                            0
                        },
                        0
                    };
                case "PerformNonlinearAnalysisEx":
                    return new object[]
                    {
                        0,
                        0.0,
                        5,
                        1e-9,
                        1,
                        0,
                        (short)0,
                        null
                    };
                case "SetFloorDiaphragmBaseCommand":
                    return new object[]
                    {
                        2.0
                    };
                case "SetCheckSoftStoryCommand":
                case "SetCheckIrregularitiesCommand":
                    return new object[]
                    {
                        3
                    };
                case "CreateElasticMat":
                    return new object[]
                    {
                        1,
                        2.0,
                        1,
                        0
                    };
                case "CreateElasticFooting":
                    return new object[]
                    {
                        2.0,
                        2.0,
                        1,
                        2.0
                    };
                case "AssignSupportToEntities":
                    return new object[]
                    {
                        15,
                        new[]
                        {
                            17,
                            41,
                            17
                        }
                    };
                default:
                    return name.StartsWith("Remove") ? new object[]
                    {
                        15
                    }

                    : new object[0];
            }
        }

        [TestCaseSource(nameof(Cases))]
        public void MissingNativeMembersFailWithoutGuessingAnotherRoute(string domain, string name, string nativeName)
        {
            var root = new CommandsSupportFixture.Root { Command = new object(), Support = new object() };
            using (var wrapper = new OpenStaadWrapper(root))
                Assert.Throws<Microsoft.CSharp.RuntimeBinder.RuntimeBinderException>(() => Invoke(Receiver(wrapper, domain), domain, name));
        }

        [TestCaseSource(nameof(Cases))]
        public void EveryPortUsesTheReviewedArgumentOrderAndNativePacketTypes(string domain, string name, string nativeName)
        {
            var native = new CommandsSupportFixture.Native();
            using (var wrapper = new OpenStaadWrapper(Root(native)))
            {
                Invoke(Receiver(wrapper, domain), domain, name);
                object[] actual = native.Arguments.Single(), expected = Expected(name);
                Assert.That(actual, Is.EqualTo(expected));
                Assert.That(actual.Select(v => v?.GetType()), Is.EqualTo(expected.Select(v => v?.GetType())));
            }
        }

        [TestCase("PerformCableAnalysis")]
        [TestCase("PerformCableAnalysisEx")]
        [TestCase("PerformBucklingAnalysis")]
        [TestCase("PerformBucklingAnalysisEx")]
        public void ReservedModeShapePrintIsRejectedBeforeDispatch(string name)
        {
            var native = new CommandsSupportFixture.Native();
            var arguments = Args("Commands", name);
            if (name == "PerformCableAnalysisEx")
                ((CableAnalysisOptions)arguments[0]).PrintOption = PrintOption.PrintModeShapes;
            else
                arguments[Array.FindIndex(typeof(IOSCommands).GetMethod(name).GetParameters(), p => p.ParameterType == typeof(PrintOption))] = PrintOption.PrintModeShapes;
            using (var wrapper = new OpenStaadWrapper(Root(native)))
                Assert.Throws<ArgumentOutOfRangeException>(() => Invoke(wrapper.Commands, "Commands", name, arguments));
            Assert.That(native.Calls, Is.Empty);
        }

        [Test]
        public void AdvancedCableParametersAreCopiedAndInactiveBasicValuesAreNotMarshaled()
        {
            var native = new CommandsSupportFixture.Native();
            var options = new CableAnalysisOptions
            {
                Method = CableAnalysisMethod.Advanced
            };
            options.BasicParameters.LoadSteps = -1;
            options.AdvancedParameters.LoadSteps = 3;
            options.AdvancedParameters.EquilibriumIterations = 23;
            options.AdvancedOptions.IncludeGeometricStiffness = true;
            native.OnCall = op =>
            {
                options.AdvancedParameters.LoadSteps = 999;
                options.AdvancedOptions.UseFullNewtonRaphson = false;
            };
            using (var wrapper = new OpenStaadWrapper(Root(native)))
                wrapper.Commands.PerformCableAnalysisEx(options);
            Assert.That(native.Arguments.Single(), Is.EqualTo(new object[] { (short)1, new short[] { 1, 1 }, new double[] { 3, 23, 1e-6 }, 0 }));
        }

        [Test]
        public void DirectAndNonlinearOptionsPreserveAllSelectorsAndTargetUnits()
        {
            var native = new CommandsSupportFixture.Native();
            using (var wrapper = new OpenStaadWrapper(Root(native)))
            {
                wrapper.Commands.PerformDirectAnalysis(new DirectAnalysisOptions { DesignFormat = DirectAnalysisOption.Asd, TauBTolerance = .2, DisplacementTolerance = .3, DirectIterations = 4, PDeltaIterations = 9, UseReducedFlexuralStiffness = true, IterateTauB = true, PrintOption = PrintOption.PrintModeShapes });
                Assert.That(native.Arguments[0], Is.EqualTo(new object[] { 2, new double[] { .2, .3, 4, 9 }, new short[] { 1, 1 }, 4 }));
                wrapper.Commands.PerformNonlinearAnalysisEx(new NonlinearAnalysisOptions { ArcLength = .5, MaximumIterations = 17, ConvergenceTolerance = .0002, LoadSteps = 4, RebuildFrequency = StiffnessMatrixRebuildFrequency.EveryLoadStepAndIteration, IncludeGeometricStiffness = true, DisplacementTarget = new NonlinearDisplacementTarget { JointId = 42, DegreeOfFreedom = GlobalDegreeOfFreedom.RotationZ, TargetValue = -.02 } });
                Assert.That(native.Arguments[1], Is.EqualTo(new object[] { 0, .5, 17, .0002, 4, 1, (short)1, new double[] { 42, 6, -.02 } }));
            }
        }

        [TestCase(4, 300, .0001, 1, 1, 0)]
        [TestCase(146, 300, .0001, 1, 1, 0)]
        [TestCase(145, 9, .0001, 1, 1, 0)]
        [TestCase(145, 501, .0001, 1, 1, 0)]
        [TestCase(145, 300, 0, 1, 1, 0)]
        [TestCase(145, 300, .0001, -1, 1, 0)]
        [TestCase(145, 300, .0001, 1001, 1, 0)]
        [TestCase(145, 300, .0001, 1, 0, 0)]
        [TestCase(145, 300, .0001, 1, 1, 1.01)]
        public void BasicCableRangesAreValidatedBeforeNativeMutation(int steps, int iterations, double tolerance, double stability, int stabilitySteps, double small)
        {
            var native = new CommandsSupportFixture.Native();
            var options = new CableAnalysisOptions();
            options.BasicParameters.LoadSteps = steps;
            options.BasicParameters.EquilibriumIterations = iterations;
            options.BasicParameters.EquilibriumTolerance = tolerance;
            options.BasicParameters.StabilityStiffness = stability;
            options.BasicParameters.StabilityLoadSteps = stabilitySteps;
            options.BasicParameters.SmallStiffness = small;
            using (var wrapper = new OpenStaadWrapper(Root(native)))
                Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Commands.PerformCableAnalysisEx(options));
            Assert.That(native.Calls, Is.Empty);
        }

        [TestCase(0, 15)]
        [TestCase(1.01, 15)]
        [TestCase(.01, 0)]
        [TestCase(.01, 16)]
        public void DirectAnalysisRejectsOutOfRangeSettingsInsteadOfAllowingNativeClamping(double tolerance, int iterations)
        {
            var native = new CommandsSupportFixture.Native();
            using (var wrapper = new OpenStaadWrapper(Root(native)))
                Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Commands.PerformDirectAnalysis(new DirectAnalysisOptions { TauBTolerance = tolerance, PDeltaIterations = iterations }));
            Assert.That(native.Calls, Is.Empty);
        }

        [TestCase(SupportEntityKind.Nodes)]
        [TestCase(SupportEntityKind.Plates)]
        public void AssignmentOwnsInputsPreservesFamilyAndDoesNotExposeNativeRewrites(SupportEntityKind kind)
        {
            var native = new CommandsSupportFixture.Native();
            int[] input =
            {
                17,
                41,
                17
            };
            native.OnCall = op =>
            {
                ((int[])native.Arguments.Last()[1])[0] = 999;
                input[1] = 888;
            };
            using (var wrapper = new OpenStaadWrapper(Root(native)))
            {
                var result = wrapper.Support.AssignSupportToEntities(7, kind, input);
                Assert.That(result.EntityKind, Is.EqualTo(kind));
                Assert.That(result.EntityIds, Is.EqualTo(new[] { 17, 41, 17 }));
                Assert.That(result.NodeIds, kind == SupportEntityKind.Nodes ? Is.EqualTo(new[] { 17, 41, 17 }) : Is.Empty);
                Assert.Throws<NotSupportedException>(() => ((IList<int>)result.EntityIds)[0] = 3);
            }
        }

        [Test]
        public void EmptyAndOversizedAssignmentSelectionsNeverMutateNativeState()
        {
            var native = new CommandsSupportFixture.Native();
            using (var wrapper = new OpenStaadWrapper(Root(native)))
            {
                Assert.That(wrapper.Support.AssignSupportToEntities(7, SupportEntityKind.Plates, new int[0]).Status, Is.EqualTo(SupportMutationStatus.NoOperation));
                Assert.Throws<ArgumentException>(() => wrapper.Support.AssignSupportToEntities(7, SupportEntityKind.Nodes, new[] { 17, 41 }, 1));
                Assert.Throws<ArgumentOutOfRangeException>(() => wrapper.Support.AssignSupportToEntities(7, SupportEntityKind.Nodes, new[] { 0 }));
                Assert.That(native.Calls, Is.Empty);
            }
        }

        [TestCase("RemoveElasticMat")]
        [TestCase("RemoveElasticMatFromNode")]
        [TestCase("RemovePlateMat")]
        [TestCase("AssignSupportToEntities")]
        public void UndocumentedNegativeAssignmentAndRemovalFlagsAreRejected(string name)
        {
            var native = new CommandsSupportFixture.Native();
            string op = name == "AssignSupportToEntities" ? "AssignSupportToEntityList" : name;
            Returns(native, op, (short)-1);
            using (var wrapper = new OpenStaadWrapper(Root(native)))
            {
                Assert.Throws<InvalidOperationException>(() => Invoke(wrapper.Support, "Support", name));
            }
        }

        [TestCase("RemoveElasticFooting")]
        [TestCase("RemoveElasticFootingFromNode")]
        public void NumericFootingRemovalDoesNotTreatNegativeOneAsTrue(string name)
        {
            var native = new CommandsSupportFixture.Native();
            Returns(native, name, -1);
            using (var wrapper = new OpenStaadWrapper(Root(native)))
                Assert.Throws<InvalidOperationException>(() => Invoke(wrapper.Support, "Support", name));
        }

        [TestCase("CreateElasticMat", -1)]
        [TestCase("CreateElasticMat", true)]
        [TestCase("CreateElasticFooting", -1)]
        [TestCase("CreateElasticFooting", false)]
        public void FoundationCreationRequiresAnIntegralPositiveIdOrDocumentedZero(string name, object raw)
        {
            var native = new CommandsSupportFixture.Native();
            Returns(native, name, raw);
            using (var wrapper = new OpenStaadWrapper(Root(native)))
                Assert.Throws<InvalidOperationException>(() => Invoke(wrapper.Support, "Support", name));
        }

        [Test]
        public void CommandCallsAreSerializedWithoutMovingWorkToAnotherThread()
        {
            var native = new CommandsSupportFixture.Native();
            int active = 0, maximum = 0;
            native.OnCall = op =>
            {
                int current = Interlocked.Increment(ref active);
                maximum = Math.Max(maximum, current);
                Thread.Sleep(15);
                Interlocked.Decrement(ref active);
            };
            using (var wrapper = new OpenStaadWrapper(Root(native)))
                Task.WaitAll(Task.Run(() => wrapper.Commands.PerformAnalysis(PrintOption.NoPrint)), Task.Run(() => wrapper.Commands.SetFloorDiaphragmBaseCommand(0)));
            Assert.That(maximum, Is.EqualTo(1));
            Assert.That(native.Calls.Count, Is.EqualTo(2));
        }

        [Test]
        public void SourceOptionModelsAndEnumsHaveCompletePublicIntelliSense()
        {
            var assembly = typeof(PDeltaAnalysisOptions).Assembly;
            var xml = XDocument.Load(Path.ChangeExtension(assembly.Location, ".xml"));
            foreach (Type type in assembly.GetExportedTypes().Where(t => t.Namespace == "StaadPro.Interop.Models.Commands" || t.Namespace == "StaadPro.Interop.Enums.AnalysisCommands"))
            {
                Assert.That(xml.Descendants("member").Single(m => (string)m.Attribute("name") == "T:" + type.FullName).Element("summary")?.Value, Is.Not.Null.And.Not.Empty);
                foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    Assert.That(xml.Descendants("member").Single(m => (string)m.Attribute("name") == "P:" + type.FullName + "." + property.Name).Element("summary")?.Value, Is.Not.Null.And.Not.Empty);
            }
        }
    }
}
