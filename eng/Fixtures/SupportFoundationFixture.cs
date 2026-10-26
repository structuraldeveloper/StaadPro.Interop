using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
namespace PackageVerification
{
    /// <summary>Managed exact-signature fixture; no STAAD process is activated or changed.</summary>
    public static class SupportFoundationFixture
    {
        public sealed class Root
        {
            public object Support {get;set;}public object Geometry=>null;public object Load=>null;public int Major=26;
            public object GetApplicationVersion(ref object major,ref object update,ref object revision,ref object build){major=Major;update=0;revision=0;build=340;return Major+".0.0.340";}
        }
        public class Native
        {
            public readonly List<string> Calls=new List<string>();
            public readonly List<object[]> Arguments=new List<object[]>();
            public readonly List<int> Threads=new List<int>();
            public readonly Dictionary<string,Queue<object>> Returns=new Dictionary<string,Queue<object>>();
            public readonly Dictionary<string,object> Values=new Dictionary<string,object>();
            public readonly Dictionary<string,Array> Buffers=new Dictionary<string,Array>();
            public Action<string> OnCall;public Func<string,Array,object> ReplaceArray;
            public bool WriteOutputs=true;public int NodeCount=2,AssignmentCount=2;public object TypeCode=3,CreatedId=4;
            public object R(string op,object fallback,params object[] args){Calls.Add(op);Arguments.Add(args);Threads.Add(Thread.CurrentThread.ManagedThreadId);OnCall?.Invoke(op);return Returns.TryGetValue(op,out var q)&&q.Count>0?q.Dequeue():fallback;}
            private object V(string field,object fallback)=>Values.TryGetValue(field,out var value)?value:fallback;
            private void Fill(ref object output,string role)
            {
                var array=(Array)output;Buffers[role]=array;if(!WriteOutputs)return;
                if(ReplaceArray!=null){output=ReplaceArray(role,array);return;}
                int lower=array.GetLowerBound(0);
                for(int i=0;i<array.Length;i++)array.SetValue(array.GetType().GetElementType()==typeof(int)?(object)(role=="states"?new[]{-1,0,1,0,1,0}[i]:11+i):i+.25,lower+i);
            }
            public object GetSupportCount()=>R(nameof(GetSupportCount),NodeCount);
            public object GetSupportNodes(ref object nodes){var r=R(nameof(GetSupportNodes),NodeCount);Fill(ref nodes,"nodes");return r;}
            public object GetSupportType(object node)=>R(nameof(GetSupportType),TypeCode,node);
            public object GetSupportInformation(object node,ref object states,ref object springs){var r=R(nameof(GetSupportInformation),TypeCode,node);Fill(ref states,"states");Fill(ref springs,"springs");return r;}
            public object GetSupportUniqueID(object support)=>R(nameof(GetSupportUniqueID),V("guid","DFE2CB8E-1B42-487F-A02C-953AC006D64A"),support);
            public object GetCountOfElasticMat()=>R(nameof(GetCountOfElasticMat),2);
            public object GetCountOfPlateMat()=>R(nameof(GetCountOfPlateMat),2);
            public object GetCountOfElasticFooting()=>R(nameof(GetCountOfElasticFooting),2);
            public object GetElasticMatDetail(object id,ref object direction,ref object subgrade,ref object print,ref object springType,ref object count)
            {var r=R(nameof(GetElasticMatDetail),true,id);if(WriteOutputs){direction=V("direction",4);subgrade=V("subgrade",125.0);print=V("print",1);springType=V("springType",1);count=V("assignmentCount",AssignmentCount);}return r;}
            public object GetElasticFootingDetail(object id,ref object length,ref object width,ref object direction,ref object subgrade,ref object count)
            {var r=R(nameof(GetElasticFootingDetail),1,id);if(WriteOutputs){length=V("length",2.5);width=V("width",3.0);direction=V("direction",1);subgrade=V("subgrade",125.0);count=V("assignmentCount",AssignmentCount);}return r;}
            public object GetElasticMatAssignmentList(object id,ref object nodes){var r=R(nameof(GetElasticMatAssignmentList),true,id);Fill(ref nodes,"matNodes");return r;}
            public object GetElasticFootingAssignmentList(object id,ref object nodes){var r=R(nameof(GetElasticFootingAssignmentList),1,id);Fill(ref nodes,"footingNodes");return r;}
            public object CreateSupportFixed()=>R(nameof(CreateSupportFixed),CreatedId);
            public object CreateSupportPinned()=>R(nameof(CreateSupportPinned),CreatedId);
            public object CreateSupportFixedBut(object releases,object springs)=>R(nameof(CreateSupportFixedBut),CreatedId,releases,springs);
            public object AssignSupportToNode(object nodes,object support)=>R(nameof(AssignSupportToNode),0,nodes,support);
            public object RemoveSupportFromNode(object node)=>R(nameof(RemoveSupportFromNode),1,node);
            public object DeleteSupport(object support)=>R(nameof(DeleteSupport),0,support);
            public void SetSupportUniqueID(object support,object guid){R(nameof(SetSupportUniqueID),null,support,guid);}
        }
    }
}
