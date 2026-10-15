using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using StaadPro.Interop.Adapters.Interfaces;
using StaadPro.Interop.Adapters.Models;
using StaadPro.Interop.Enums.Root;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Root;

namespace StaadPro.Interop.Tests.Adapter
{
    [TestFixture]
    public class OSRootConnectedProjectTests
    {
        public class NativeRoot
        {
            public object Id = "native-id", Name = "Project 雪", QueryFlag = 1, AssignFlag = 1;
            public bool WriteId = true, WriteName = true;
            public Exception Failure;
            public string[] Assigned;
            public readonly List<string> Calls = new List<string>();

            public object GetCONNECTEDProjectInfo(ref object projectId, ref object projectName)
            {
                Calls.Add("query");
                Assert.That(projectId, Is.TypeOf<string>());
                Assert.That(projectName, Is.TypeOf<string>());
                Assert.That((string)projectId, Does.Contain("UnwrittenProjectId"));
                Assert.That((string)projectName, Does.Contain("UnwrittenProjectName"));
                Assert.That(projectId, Is.Not.EqualTo(projectName));
                if (Failure != null) throw Failure;
                if (WriteId) projectId = Id;
                if (WriteName) projectName = Name;
                return QueryFlag;
            }

            public object SetCONNECTEDProjectInfo(object projectId, object projectName)
            {
                Calls.Add("assign");
                Assert.That(projectId, Is.TypeOf<string>());
                Assert.That(projectName, Is.TypeOf<string>());
                Assigned = new[] { (string)projectId, (string)projectName };
                if (Failure != null) throw Failure;
                return AssignFlag;
            }
        }

        [TestCase(true)] [TestCase(1)]
        public void SuccessfulQueriesReturnQualifiedImmutableProjectData(object flag)
        {
            var native = new NativeRoot { Id = "  arbitrary-id  ", Name = "Project 雪\r\nName", QueryFlag = flag };
            var result = new OSRootAdapter(native).GetConnectedProjectInfo();
            Assert.That(result.IsSuccessful, Is.True);
            Assert.That(result.Project.ProjectId, Is.EqualTo(native.Id));
            Assert.That(result.Project.Name, Is.EqualTo(native.Name));
            Assert.That(native.Calls, Is.EqualTo(new[] { "query" }));
        }

        [TestCase(false)] [TestCase(0)]
        public void NativeFalseReturnsNoProjectEvenWhenOutputsAreMalformedOrPartial(object flag)
        {
            foreach (var native in new[] {
                new NativeRoot { QueryFlag = flag, Id = null, Name = 42 },
                new NativeRoot { QueryFlag = flag, WriteId = false, WriteName = false },
                new NativeRoot { QueryFlag = flag, Id = "partial", WriteName = false }
            })
            {
                var result = new OSRootAdapter(native).GetConnectedProjectInfo();
                Assert.That(result.IsSuccessful, Is.False);
                Assert.That(result.Project, Is.Null);
                Assert.That(native.Calls, Is.EqualTo(new[] { "query" }));
            }
        }

        [TestCase(true)] [TestCase(false)]
        public void IntegralFlagRepresentationsAreAcceptedForQueriesAndAssignments(bool success)
        {
            foreach (object flag in success
                ? new object[] { (sbyte)1, (byte)1, (short)1, (ushort)1, 1, 1U, 1L, 1UL }
                : new object[] { (sbyte)0, (byte)0, (short)0, (ushort)0, 0, 0U, 0L, 0UL })
            {
                var root = new OSRootAdapter(new NativeRoot { QueryFlag = flag, AssignFlag = flag });
                Assert.That(root.GetConnectedProjectInfo().IsSuccessful, Is.EqualTo(success));
                Assert.That(root.SetConnectedProjectInfo("id", "name"), Is.EqualTo(success));
            }
        }

        [TestCase(null)] [TestCase(-1)] [TestCase(2)] [TestCase("1")] [TestCase(1d)]
        [TestCase(double.NaN)] [TestCase(long.MaxValue)] [TestCase(ulong.MaxValue)]
        public void UnsupportedNativeFlagsThrowWithoutTreatingThemAsOrdinaryFailures(object flag)
        {
            var native = new NativeRoot { QueryFlag = flag, AssignFlag = flag };
            var root = new OSRootAdapter(native);
            Assert.Throws<InvalidOperationException>(() => root.GetConnectedProjectInfo());
            Assert.Throws<InvalidOperationException>(() => root.SetConnectedProjectInfo("id", "name"));
            Assert.That(native.Calls, Is.EqualTo(new[] { "query", "assign" }));
            Assert.That(native.Assigned, Is.EqualTo(new[] { "id", "name" }), "Malformed returns do not trigger rollback or retry.");
        }

        [Test]
        public void DecimalAndDatabaseNullFlagsAreRejected()
        {
            foreach (object flag in new object[] { 1m, DBNull.Value })
            {
                var root = new OSRootAdapter(new NativeRoot { QueryFlag = flag, AssignFlag = flag });
                Assert.Throws<InvalidOperationException>(() => root.GetConnectedProjectInfo());
                Assert.Throws<InvalidOperationException>(() => root.SetConnectedProjectInfo("id", "name"));
            }
        }

        [TestCase("id")] [TestCase("name")]
        public void SuccessfulQueriesRejectUnwrittenOrNonStringFields(string field)
        {
            var unwritten = new NativeRoot { WriteId = field != "id", WriteName = field != "name" };
            Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(unwritten).GetConnectedProjectInfo());
            foreach (object value in new object[] { null, 42, true, DBNull.Value })
            {
                var native = new NativeRoot();
                if (field == "id") native.Id = value; else native.Name = value;
                Assert.Throws<InvalidOperationException>(() => new OSRootAdapter(native).GetConnectedProjectInfo());
                Assert.That(native.Calls, Is.EqualTo(new[] { "query" }));
            }
        }

        [Test]
        public void ExplicitEmptySuccessfulOutputsArePreservedWithoutInventingAssociationSemantics()
        {
            var result = new OSRootAdapter(new NativeRoot { Id = "", Name = "" }).GetConnectedProjectInfo();
            Assert.That(result.IsSuccessful, Is.True);
            Assert.That(result.Project.ProjectId, Is.Empty);
            Assert.That(result.Project.Name, Is.Empty);
        }

        [TestCase(true)] [TestCase(false)] [TestCase(1)] [TestCase(0)]
        public void AssignmentPreservesNativeArgumentOrderTextAndStatus(object flag)
        {
            var native = new NativeRoot { AssignFlag = flag };
            bool result = new OSRootAdapter(native).SetConnectedProjectInfo("  non-guid-id  ", "  项目 name  ");
            Assert.That(result, Is.EqualTo(flag is bool boolean ? boolean : (int)flag == 1));
            Assert.That(native.Assigned, Is.EqualTo(new[] { "  non-guid-id  ", "  项目 name  " }));
            Assert.That(native.Calls, Is.EqualTo(new[] { "assign" }));
        }

        [TestCase("projectId", null)] [TestCase("projectName", null)]
        [TestCase("projectId", "")] [TestCase("projectName", "")]
        [TestCase("projectId", " \t")] [TestCase("projectName", " \t")]
        public void InvalidAssignmentInputsAreRejectedBeforeNativeAccess(string parameter, string value)
        {
            var native = new NativeRoot();
            var error = Assert.Catch<ArgumentException>(() => new OSRootAdapter(native).SetConnectedProjectInfo(
                parameter == "projectId" ? value : "id", parameter == "projectName" ? value : "name"));
            Assert.That(error.ParamName, Is.EqualTo(parameter));
            if (value == null) Assert.That(error, Is.TypeOf<ArgumentNullException>());
            Assert.That(native.Calls, Is.Empty);
        }

        [Test]
        public void ProjectObjectOverloadReusesQualifiedQueryDataWithoutRequery()
        {
            var native = new NativeRoot(); var root = new OSRootAdapter(native);
            var project = root.GetConnectedProjectInfo().Project;
            native.Calls.Clear();
            Assert.That(root.SetConnectedProjectInfo(project), Is.True);
            Assert.That(native.Assigned, Is.EqualTo(new[] { project.ProjectId, project.Name }));
            Assert.That(native.Calls, Is.EqualTo(new[] { "assign" }));
        }

        [Test]
        public void NullProjectObjectAndBlankSnapshotFieldsCannotTriggerAssignment()
        {
            var native = new NativeRoot(); var root = new OSRootAdapter(native);
            Assert.That(Assert.Throws<ArgumentNullException>(() => root.SetConnectedProjectInfo((ConnectedProjectInfo)null)).ParamName,
                Is.EqualTo("projectInfo"));
            foreach (var project in new[] { new ConnectedProjectInfo("", "name"), new ConnectedProjectInfo("id", " ") })
                Assert.That(Assert.Throws<ArgumentException>(() => root.SetConnectedProjectInfo(project)).ParamName, Is.EqualTo("projectInfo"));
            Assert.That(native.Calls, Is.Empty);
        }

        [Test]
        public void DataSnapshotsAreIndependentAndBothClassesAreImmutable()
        {
            var native = new NativeRoot(); var root = new OSRootAdapter(native);
            var original = root.GetConnectedProjectInfo();
            native.Id = "changed"; native.Name = "changed";
            var next = root.GetConnectedProjectInfo();
            Assert.That(original.Project.ProjectId, Is.EqualTo("native-id"));
            Assert.That(next.Project.ProjectId, Is.EqualTo("changed"));
            Assert.That(original, Is.Not.SameAs(next));
            Assert.That(original.Project, Is.Not.SameAs(next.Project));
            foreach (var type in new[] { typeof(ConnectedProjectInfo), typeof(ConnectedProjectResult) })
            {
                Assert.That(type.IsSealed, Is.True);
                Assert.That(type.GetProperties().All(p => p.GetSetMethod(true) == null), Is.True);
            }
            Assert.That(typeof(ConnectedProjectResult).GetConstructors(), Is.Empty);
            Assert.That(Assert.Throws<ArgumentNullException>(() => new ConnectedProjectInfo(null, "name")).ParamName, Is.EqualTo("projectId"));
            Assert.That(Assert.Throws<ArgumentNullException>(() => new ConnectedProjectInfo("id", null)).ParamName, Is.EqualTo("projectName"));
        }

        [TestCase("query")] [TestCase("assign")] [TestCase("object")]
        public void NativeFailuresPropagateUnchangedWithoutFallbackOrRetry(string operation)
        {
            var failure = new COMException("Native failure", unchecked((int)0x80010108));
            var native = new NativeRoot { Failure = failure };
            Assert.That(Assert.Throws<COMException>(() => Invoke(new OSRootAdapter(native), operation)), Is.SameAs(failure));
            Assert.That(native.Calls, Is.EqualTo(new[] { operation == "query" ? "query" : "assign" }));
        }

        [TestCase("query")] [TestCase("assign")] [TestCase("object")]
        public void MissingMembersDoNotBecomeOrdinaryNativeFalseResults(string operation)
        {
            Assert.Throws<RuntimeBinderException>(() => Invoke(new OSRootAdapter(new object()), operation));
        }

        [Test]
        public void WrapperAndStandaloneBindingsStayOnTheirOwnInstances()
        {
            var first = new NativeRoot { Id = "first" }; var second = new NativeRoot { Id = "second" };
            using (var wrapper = new OpenStaadWrapper(first))
            {
                first.Calls.Clear();
                var right = new OSRootAdapter(second);
                Assert.That(wrapper.Root.GetConnectedProjectInfo().Project.ProjectId, Is.EqualTo("first"));
                Assert.That(right.GetConnectedProjectInfo().Project.ProjectId, Is.EqualTo("second"));
                wrapper.Root.SetConnectedProjectInfo("assigned-first", "name");
                Assert.That(second.Assigned, Is.Null);
                Assert.That(second.Calls, Is.EqualTo(new[] { "query" }));
            }
        }

        [Test]
        public void NewDataAndEngineMembersHaveCompiledXmlAndNoPublicReferenceParameters()
        {
            var assembly = typeof(IOSRoot).Assembly;
            var entries = XDocument.Load(Path.ChangeExtension(assembly.Location, ".xml"))
                .Descendants("member").ToDictionary(m => (string)m.Attribute("name"));
            foreach (var type in new[] { typeof(ConnectedProjectInfo), typeof(ConnectedProjectResult), typeof(AnalysisEngine) })
            {
                Assert.That(entries["T:" + type.FullName].Element("summary")?.Value, Is.Not.Null.And.Not.Empty);
                foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    var entry = entries["P:" + type.FullName + "." + property.Name];
                    Assert.That(entry.Element("summary")?.Value, Is.Not.Null.And.Not.Empty);
                    Assert.That(entry.Element("value")?.Value, Is.Not.Null.And.Not.Empty);
                }
                foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
                    Assert.That(entries["F:" + type.FullName + "." + field.Name].Element("summary")?.Value, Is.Not.Null.And.Not.Empty);
            }
            var constructor = entries["M:" + typeof(ConnectedProjectInfo).FullName + ".#ctor(System.String,System.String)"];
            Assert.That(constructor.Elements("param").Select(p => (string)p.Attribute("name")), Is.EquivalentTo(new[] { "projectId", "projectName" }));
            Assert.That(typeof(IOSRoot).GetMethods().Where(m => new[] { "SetFullJobInfo", "Analyze", "AnalyzeModel", "GetConnectedProjectInfo", "SetConnectedProjectInfo" }.Contains(m.Name))
                .SelectMany(m => m.GetParameters()).Any(p => p.IsOut || p.ParameterType.IsByRef), Is.False);
        }

        private static void Invoke(IOSRoot root, string operation)
        {
            if (operation == "query") root.GetConnectedProjectInfo();
            else if (operation == "assign") root.SetConnectedProjectInfo("id", "name");
            else root.SetConnectedProjectInfo(new ConnectedProjectInfo("id", "name"));
        }
    }
}
