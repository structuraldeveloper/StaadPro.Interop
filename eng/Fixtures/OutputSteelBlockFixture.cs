using System;
using System.Collections.Generic;
using System.Threading;
namespace PackageVerification
{
    /// <summary>Managed native-signature stand-in used only for tests and installed documentation examples.</summary>
    public static class OutputSteelBlockFixture
    {
        public sealed class Root
        {
            public object Output {get;set;}
            public object Geometry=>null;public object Load=>null;
            public int Major=26;
            public object GetApplicationVersion(ref object major,ref object update,ref object revision,ref object build){major=Major;update=0;revision=0;build=340;return Major+".0.0.340";}
        }
        public class Native
        {
            public readonly List<string> Calls=new List<string>();
            public readonly List<object[]> Arguments=new List<object[]>();
            public readonly List<int> Threads=new List<int>();
            public object Flag=true,Ratio=.75,BlockName=" PARAMETER 1 ",Code="AISC 360-16",Status="PASS",Allowable=1.0,LoadCase=41,Clause="H1-1a",Section="W12X26";
            public bool WriteOutputs=true;public Action<string> OnCall;
            private object Record(string op,params object[] args){Calls.Add(op);Arguments.Add(args);Threads.Add(Thread.CurrentThread.ManagedThreadId);OnCall?.Invoke(op);return Flag;}
            public object GetSteelDesignParameterBlockNameByIndex(object index,ref object name){var r=Record(nameof(GetSteelDesignParameterBlockNameByIndex),index);if(WriteOutputs)name=BlockName;return r;}
            public object GetMultipleMemberSteelDesignRatio(object block,object member,ref object ratio){var r=Record(nameof(GetMultipleMemberSteelDesignRatio),block,member);if(WriteOutputs)ratio=Ratio;return r;}
            public object GetMultipleMemberSteelDesignMaxRatio(object member,ref object ratio){var r=Record(nameof(GetMultipleMemberSteelDesignMaxRatio),member);if(WriteOutputs)ratio=Ratio;return r;}
            public object GetMemberSteelDesignMinFailureRatio(ref object ratio){var r=Record(nameof(GetMemberSteelDesignMinFailureRatio));if(WriteOutputs)ratio=Ratio;return r;}
            public object GetMemberSteelDesignMaxFailureRatio(ref object ratio){var r=Record(nameof(GetMemberSteelDesignMaxFailureRatio));if(WriteOutputs)ratio=Ratio;return r;}
            public object GetMultipleMemberSteelDesignResults(object block,object member,ref object code,ref object status,ref object ratio,ref object allowable,ref object loadCase,ref object clause,ref object section)
            {var r=Record(nameof(GetMultipleMemberSteelDesignResults),block,member);if(WriteOutputs){code=Code;status=Status;ratio=Ratio;allowable=Allowable;loadCase=LoadCase;clause=Clause;section=Section;}return r;}
        }
    }
}
