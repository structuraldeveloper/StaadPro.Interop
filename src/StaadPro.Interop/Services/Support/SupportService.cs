using System;
using System.Collections.Generic;
using System.Linq;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Enums.Support;
using StaadPro.Interop.Models;
using StaadPro.Interop.Models.Support;
using StaadPro.Interop.Services.Interop;
using static StaadPro.Interop.Services.Property.PropertyValueReader;
namespace StaadPro.Interop.Services.Support
{
    // Reviewed common contracts. Existing strict native readers are shared; no per-year clones.
    internal sealed class SupportService
    {
        private readonly OpenStaadWrapper wrapper;
        private readonly object native;
        private readonly object gate=new object();
        internal SupportService(OpenStaadWrapper wrapper,object native){this.wrapper=wrapper;this.native=native;}
        private dynamic Binding()
        {if(wrapper.RawOpenStaad==null||!ReferenceEquals((object)wrapper.RawSupport,native))throw new InvalidOperationException("The Support binding is disposed or replaced. Reacquire wrapper.Support.");return native;}
        private T Run<T>(Func<dynamic,T> operation){lock(gate){dynamic receiver=Binding();T result=operation(receiver);Binding();return result;}}
        private static SupportQueryResult<T> Q<T>(string op,T data=default,SupportQueryStatus status=SupportQueryStatus.Success,object raw=null,int? node=null,int? support=null,string diagnostic=null)=>
            new SupportQueryResult<T>(op,status,data,node,support,raw,diagnostic);
        private static int Maximum(int maximumResults){if(maximumResults<1||maximumResults>100000)throw new ArgumentOutOfRangeException(nameof(maximumResults));return maximumResults;}
        private static int[] Nodes(IEnumerable<int> nodeIds,int maximumResults)
        {
            if(nodeIds==null)throw new ArgumentNullException(nameof(nodeIds));Maximum(maximumResults);
            int[] copy=nodeIds.Take(maximumResults+1).ToArray();
            if(copy.Length>maximumResults)throw new ArgumentException("Selection exceeds maximumResults.",nameof(nodeIds));
            if(copy.Any(x=>x<=0))throw new ArgumentOutOfRangeException(nameof(nodeIds));return copy;
        }
        private static int[] IdBuffer(int count)=>Enumerable.Repeat(int.MinValue,count).ToArray();
        private static int[] NodeArray(object buffer,int count,string op)
        {int[] ids=IntegerArray(buffer,count,op);if(ids.Any(x=>x<=0))throw new InvalidOperationException(op+" returned unwritten or invalid node IDs.");return ids;}
        private static FoundationSupportDirection Direction(object raw,string op)
        {int value=Integer(raw,op);if(value<0||value>5)throw new InvalidOperationException(op+" returned an unknown foundation direction.");return (FoundationSupportDirection)value;}
        private static double NonnegativeNumber(object raw,string op)
        {double value=Number(raw,op);if(value<0)throw new InvalidOperationException(op+" returned a negative value.");return value;}
        internal SupportQueryResult<int> GetSupportedNodeCount()=>Run(p=>{const string op="GetSupportCount";object raw=p.GetSupportCount();return Q(op,Count(raw,op),raw:raw);});
        internal SupportQueryResult<int> GetElasticMatDefinitionCount()=>Run(p=>{const string op="GetCountOfElasticMat";object raw=p.GetCountOfElasticMat();return Q(op,Count(raw,op),raw:raw);});
        internal SupportQueryResult<int> GetPlateMatDefinitionCount()=>Run(p=>{const string op="GetCountOfPlateMat";object raw=p.GetCountOfPlateMat();return Q(op,Count(raw,op),raw:raw);});
        internal SupportQueryResult<int> GetElasticFootingDefinitionCount()=>Run(p=>{const string op="GetCountOfElasticFooting";object raw=p.GetCountOfElasticFooting();return Q(op,Count(raw,op),raw:raw);});
        internal SupportQueryResult<IReadOnlyList<int>> GetSupportedNodeIds(int maximumResults)
        {
            Maximum(maximumResults);
            return Run(p=>{const string op="GetSupportNodes";int before=Count((object)p.GetSupportCount(),op+" preflight");
                if(before>maximumResults)return Q<IReadOnlyList<int>>(op,status:SupportQueryStatus.LimitExceeded,diagnostic:"Supported-node count exceeded maximumResults.");
                if(before==0){int afterEmpty=Count((object)p.GetSupportCount(),op+" recheck");return afterEmpty==0?Q<IReadOnlyList<int>>(op,Array.AsReadOnly(new int[0])):Q<IReadOnlyList<int>>(op,status:SupportQueryStatus.StateChanged);}
                object values=IdBuffer(before);object raw=p.GetSupportNodes(ref values);int returned=Integer(raw,op);
                if(returned==-1)return Q<IReadOnlyList<int>>(op,status:SupportQueryStatus.NativeFailure,raw:raw);
                if(returned<0)throw new InvalidOperationException(op+" returned an undocumented negative count.");
                int after=Count((object)p.GetSupportCount(),op+" recheck");
                if(before!=returned||before!=after)return Q<IReadOnlyList<int>>(op,status:SupportQueryStatus.StateChanged,raw:raw);
                return Q<IReadOnlyList<int>>(op,Array.AsReadOnly(NodeArray(values,before,op)),raw:raw);});
        }
        internal SupportQueryResult<SupportTypeObservation> GetSupportType(int nodeId)
        {
            Positive(nodeId,nameof(nodeId));return Run(p=>{const string op="GetSupportType";
                // Help's logical input is a mutable reference in C++, but exported CLR metadata takes Object by value.
                object raw=p.GetSupportType(nodeId);int code=Integer(raw,op);
                if(code==-1)return Q<SupportTypeObservation>(op,status:SupportQueryStatus.NativeFailure,raw:raw,node:nodeId);
                if(code<0)throw new InvalidOperationException(op+" returned an undocumented negative type.");
                return Q(op,new SupportTypeObservation(nodeId,code,code<=13?(SupportType?)code:null),raw:raw,node:nodeId);});
        }
        internal SupportQueryResult<SupportInformation> GetSupportInformation(int nodeId)
        {
            Positive(nodeId,nameof(nodeId));return Run(p=>{const string op="GetSupportInformation";object states=IdBuffer(6),springs=Buffer(6);
                object raw=p.GetSupportInformation(nodeId,ref states,ref springs);int code=Integer(raw,op);
                if(code==-1)return Q<SupportInformation>(op,status:SupportQueryStatus.NativeFailure,raw:raw,node:nodeId);
                if(code==0)return Q<SupportInformation>(op,status:SupportQueryStatus.NoSupport,raw:raw,node:nodeId);
                if(code<0)throw new InvalidOperationException(op+" returned an undocumented negative type.");
                if(code>13)return Q<SupportInformation>(op,status:SupportQueryStatus.UnsupportedNativeType,raw:raw,node:nodeId);
                int[] r=IntegerArray(states,6,op);double[] s=FloatingArray(springs,6,op);
                if(r.Any(v=>v < -1||v>1)||s.Any(v=>v<0))throw new InvalidOperationException(op+" returned invalid DOF states or stiffnesses.");
                var dofs=r.Select((v,i)=>new SupportDof((SupportDofState)v,s[i])).ToArray();
                return Q(op,new SupportInformation(nodeId,(SupportType)code,dofs[0],dofs[1],dofs[2],dofs[3],dofs[4],dofs[5]),raw:raw,node:nodeId);});
        }
        internal SupportQueryResult<SupportUniqueId> GetSupportUniqueId(int supportId)
        {
            Positive(supportId,nameof(supportId));return Run(p=>{const string op="GetSupportUniqueID";object raw=p.GetSupportUniqueID(supportId);
                if(raw==null||raw==DBNull.Value||raw is string empty&&empty.Length==0)return Q<SupportUniqueId>(op,status:SupportQueryStatus.EmptyOrUnavailable,support:supportId);
                if(!(raw is string text)||text.IndexOf('\0')>=0||!Guid.TryParse(text,out Guid guid))throw new InvalidOperationException(op+" returned malformed GUID text.");
                return Q(op,new SupportUniqueId(supportId,guid,text),support:supportId);});
        }
        internal SupportQueryResult<ElasticMatObservation> GetElasticMat(int supportId)
        {Positive(supportId,nameof(supportId));return Run(p=>(SupportQueryResult<ElasticMatObservation>)Mat(p,supportId));}
        private static SupportQueryResult<ElasticMatObservation> Mat(dynamic p,int supportId)
        {
            const string op="GetElasticMatDetail";object direction=int.MinValue,subgrade=double.NaN,print=int.MinValue,springType=int.MinValue,count=int.MinValue;
            object raw=p.GetElasticMatDetail(supportId,ref direction,ref subgrade,ref print,ref springType,ref count);
            if(!Flag(raw,op,true))return Q<ElasticMatObservation>(op,status:SupportQueryStatus.NativeFailure,raw:raw,support:supportId);
            int printing=Integer(print,op),kind=Integer(springType,op);
            if(printing<0||printing>1||kind<0||kind>2)throw new InvalidOperationException(op+" returned unknown print/spring codes.");
            return Q(op,new ElasticMatObservation(supportId,Direction(direction,op),NonnegativeNumber(subgrade,op),printing==1,(FoundationSpringType)kind,Count(count,op)),raw:raw,support:supportId);
        }
        internal SupportQueryResult<ElasticFootingObservation> GetElasticFooting(int supportId)
        {Positive(supportId,nameof(supportId));return Run(p=>(SupportQueryResult<ElasticFootingObservation>)Footing(p,supportId));}
        private static SupportQueryResult<ElasticFootingObservation> Footing(dynamic p,int supportId)
        {
            const string op="GetElasticFootingDetail";object length=double.NaN,width=double.NaN,direction=int.MinValue,subgrade=double.NaN,count=int.MinValue;
            object raw=p.GetElasticFootingDetail(supportId,ref length,ref width,ref direction,ref subgrade,ref count);
            if(!Flag(raw,op))return Q<ElasticFootingObservation>(op,status:SupportQueryStatus.NativeFailure,raw:raw,support:supportId);
            double l=Number(length,op),w=Number(width,op);if(l<=0||w<=0)throw new InvalidOperationException(op+" returned invalid footing dimensions.");
            return Q(op,new ElasticFootingObservation(supportId,l,w,Direction(direction,op),NonnegativeNumber(subgrade,op),Count(count,op)),raw:raw,support:supportId);
        }
        private static bool Same(ElasticMatObservation a,ElasticMatObservation b)=>a.SupportId==b.SupportId&&a.Direction==b.Direction&&a.SubgradeModulus==b.SubgradeModulus&&a.PrintInfluence==b.PrintInfluence&&a.SpringType==b.SpringType&&a.AssignedNodeCount==b.AssignedNodeCount;
        private static bool Same(ElasticFootingObservation a,ElasticFootingObservation b)=>a.SupportId==b.SupportId&&a.Length==b.Length&&a.Width==b.Width&&a.Direction==b.Direction&&a.SubgradeModulus==b.SubgradeModulus&&a.AssignedNodeCount==b.AssignedNodeCount;
        internal SupportQueryResult<IReadOnlyList<int>> GetElasticMatAssignedNodeIds(int supportId,int maximumResults)=>Assignments(supportId,maximumResults,true);
        internal SupportQueryResult<IReadOnlyList<int>> GetElasticFootingAssignedNodeIds(int supportId,int maximumResults)=>Assignments(supportId,maximumResults,false);
        private SupportQueryResult<IReadOnlyList<int>> Assignments(int supportId,int maximumResults,bool mat)
        {
            Positive(supportId,nameof(supportId));Maximum(maximumResults);
            return Run(p=>{
                string op=mat?"GetElasticMatAssignmentList":"GetElasticFootingAssignmentList";
                var beforeMat=mat?(SupportQueryResult<ElasticMatObservation>)Mat(p,supportId):null;
                var beforeFoot=mat?null:(SupportQueryResult<ElasticFootingObservation>)Footing(p,supportId);
                bool ready=mat?beforeMat.IsSuccessful:beforeFoot.IsSuccessful;
                if(!ready)return Q<IReadOnlyList<int>>(op,status:SupportQueryStatus.NativeFailure,support:supportId,diagnostic:"Foundation detail preflight failed; no assignment list was invoked.");
                int count=mat?beforeMat.Data.AssignedNodeCount:beforeFoot.Data.AssignedNodeCount;
                if(count>maximumResults)return Q<IReadOnlyList<int>>(op,status:SupportQueryStatus.LimitExceeded,support:supportId);
                object ids=IdBuffer(count),raw=null;
                if(count>0){raw=mat?(object)p.GetElasticMatAssignmentList(supportId,ref ids):(object)p.GetElasticFootingAssignmentList(supportId,ref ids);
                    if(!Flag(raw,op,mat))return Q<IReadOnlyList<int>>(op,status:SupportQueryStatus.NativeFailure,raw:raw,support:supportId);}
                int[] copy=NodeArray(ids,count,op);
                bool stable;
                if(mat){var after=(SupportQueryResult<ElasticMatObservation>)Mat(p,supportId);stable=after.IsSuccessful&&Same(beforeMat.Data,after.Data);}
                else{var after=(SupportQueryResult<ElasticFootingObservation>)Footing(p,supportId);stable=after.IsSuccessful&&Same(beforeFoot.Data,after.Data);}
                return stable?Q<IReadOnlyList<int>>(op,Array.AsReadOnly(copy),raw:raw,support:supportId)
                    :Q<IReadOnlyList<int>>(op,status:SupportQueryStatus.StateChanged,raw:raw,support:supportId);
            });
        }
        internal SupportMutationResult CreateFixedSupport()=>Run(p=>Creation("CreateSupportFixed",(object)p.CreateSupportFixed()));
        internal SupportMutationResult CreatePinnedSupport()=>Run(p=>Creation("CreateSupportPinned",(object)p.CreateSupportPinned()));
        internal SupportMutationResult CreateFixedButSupport(FixedButSupportDefinition definition)
        {
            if(definition==null)throw new ArgumentNullException(nameof(definition));
            double[] releases=definition.NativeReleases(),springs=definition.NativeSprings();
            return Run(p=>Creation("CreateSupportFixedBut",(object)p.CreateSupportFixedBut(releases,springs)));
        }
        private static SupportMutationResult Creation(string op,object raw)
        {
            int id=Integer(raw,op);
            if(id==-1)return new SupportMutationResult(op,SupportMutationStatus.NativeFailure,raw);
            if(id<=0)throw new InvalidOperationException(op+" did not return a qualified positive support reference.");
            return new SupportMutationResult(op,SupportMutationStatus.Succeeded,raw,id);
        }
        internal SupportMutationResult AssignSupportToNodes(int supportId,IEnumerable<int> nodeIds,int maximumResults)
        {
            Positive(supportId,nameof(supportId));int[] ids=Nodes(nodeIds,maximumResults);
            return Run(p=>{const string op="AssignSupportToNode";
                if(ids.Length==0)return new SupportMutationResult(op,SupportMutationStatus.NoOperation,supportId:supportId,nodes:ids);
                object input=(int[])ids.Clone();object raw=p.AssignSupportToNode(input,supportId);
                return new SupportMutationResult(op,Zero(raw,op),raw,supportId,ids);});
        }
        private static SupportMutationStatus Zero(object raw,string op)
        {int code=Integer(raw,op);if(code!=0&&code!=-1)throw new InvalidOperationException(op+" returned an undocumented status.");return code==0?SupportMutationStatus.Succeeded:SupportMutationStatus.NativeFailure;}
        internal SupportMutationResult RemoveSupportFromNode(int nodeId)
        {Positive(nodeId,nameof(nodeId));return Run(p=>{const string op="RemoveSupportFromNode";object raw=p.RemoveSupportFromNode(nodeId);return new SupportMutationResult(op,Flag(raw,op)?SupportMutationStatus.Succeeded:SupportMutationStatus.NativeFailure,raw,nodes:new[]{nodeId});});}
        internal SupportMutationResult DeleteSupport(int supportId)
        {Positive(supportId,nameof(supportId));return Run(p=>{const string op="DeleteSupport";object raw=p.DeleteSupport(supportId);return new SupportMutationResult(op,Zero(raw,op),raw,supportId);});}
        internal SupportMutationResult SetSupportUniqueId(int supportId,Guid uniqueId)
        {
            Positive(supportId,nameof(supportId));if(uniqueId==Guid.Empty)throw new ArgumentException("Supply an explicit nonempty support GUID.",nameof(uniqueId));
            string text=uniqueId.ToString("D");
            return Run(p=>{const string op="SetSupportUniqueID";p.SetSupportUniqueID(supportId,text);
                return new SupportMutationResult(op,SupportMutationStatus.InvokedWithoutAcknowledgement,supportId:supportId,uniqueId:uniqueId);});
        }
        private static void FoundationInput(FoundationSupportDirection direction,double subgradeModulus)
        {if(!Enum.IsDefined(typeof(FoundationSupportDirection),direction))throw new ArgumentOutOfRangeException(nameof(direction));if(double.IsNaN(subgradeModulus)||double.IsInfinity(subgradeModulus)||subgradeModulus<0)throw new ArgumentOutOfRangeException(nameof(subgradeModulus));}
        private static SupportMutationResult FoundationCreation(string op,object raw)
        {int id=Integer(raw,op);if(id==0)return new SupportMutationResult(op,SupportMutationStatus.NativeFailure,raw);if(id<0)throw new InvalidOperationException(op+" returned an undocumented negative reference.");return new SupportMutationResult(op,SupportMutationStatus.Succeeded,raw,id);}
        internal SupportMutationResult CreateElasticMat(FoundationSupportDirection direction,double subgradeModulus,bool printInfluenceAreas,FoundationSpringType springType)
        {FoundationInput(direction,subgradeModulus);if(!Enum.IsDefined(typeof(FoundationSpringType),springType))throw new ArgumentOutOfRangeException(nameof(springType));return Run(p=>FoundationCreation("CreateElasticMat",(object)p.CreateElasticMat((int)direction,subgradeModulus,printInfluenceAreas?1:0,(int)springType)));}
        internal SupportMutationResult CreateElasticFooting(double length,double width,FoundationSupportDirection direction,double subgradeModulus)
        {FoundationInput(direction,subgradeModulus);if(double.IsNaN(length)||double.IsInfinity(length)||length<=0)throw new ArgumentOutOfRangeException(nameof(length));if(double.IsNaN(width)||double.IsInfinity(width)||width<=0)throw new ArgumentOutOfRangeException(nameof(width));return Run(p=>FoundationCreation("CreateElasticFooting",(object)p.CreateElasticFooting(length,width,(int)direction,subgradeModulus)));}
        private SupportMutationResult RemoveFoundation(string op,int id,bool node,bool variantBoolean)
        {Positive(id,node?"nodeId":"supportId");return Run(p=>{object raw;switch(op){case "RemoveElasticMat":raw=p.RemoveElasticMat(id);break;case "RemoveElasticMatFromNode":raw=p.RemoveElasticMatFromNode(id);break;case "RemovePlateMat":raw=p.RemovePlateMat(id);break;case "RemoveElasticFooting":raw=p.RemoveElasticFooting(id);break;case "RemoveElasticFootingFromNode":raw=p.RemoveElasticFootingFromNode(id);break;default:throw new InvalidOperationException("Unknown foundation operation.");}return new SupportMutationResult(op,Flag(raw,op,variantBoolean)?SupportMutationStatus.Succeeded:SupportMutationStatus.NativeFailure,raw,node?(int?)null:id,node?new[]{id}:null);});}
        internal SupportMutationResult RemoveElasticMat(int supportId)=>RemoveFoundation("RemoveElasticMat",supportId,false,false);
        internal SupportMutationResult RemoveElasticMatFromNode(int nodeId)=>RemoveFoundation("RemoveElasticMatFromNode",nodeId,true,false);
        internal SupportMutationResult RemovePlateMat(int supportId)=>RemoveFoundation("RemovePlateMat",supportId,false,false);
        internal SupportMutationResult RemoveElasticFooting(int supportId)=>RemoveFoundation("RemoveElasticFooting",supportId,false,false);
        internal SupportMutationResult RemoveElasticFootingFromNode(int nodeId)=>RemoveFoundation("RemoveElasticFootingFromNode",nodeId,true,false);
        internal SupportMutationResult AssignSupportToEntities(int supportId,SupportEntityKind entityKind,IEnumerable<int> entityIds,int maximumResults)
        {Positive(supportId,nameof(supportId));if(!Enum.IsDefined(typeof(SupportEntityKind),entityKind))throw new ArgumentOutOfRangeException(nameof(entityKind));int[] ids=Nodes(entityIds,maximumResults);return Run(p=>{const string op="AssignSupportToEntityList";if(ids.Length==0)return new SupportMutationResult(op,SupportMutationStatus.NoOperation,supportId:supportId,nodes:entityKind==SupportEntityKind.Nodes?ids:null,entityKind:entityKind,entities:ids);object raw=p.AssignSupportToEntityList(supportId,(object)(int[])ids.Clone());return new SupportMutationResult(op,Flag(raw,op)?SupportMutationStatus.Succeeded:SupportMutationStatus.NativeFailure,raw,supportId,entityKind==SupportEntityKind.Nodes?ids:null,entityKind:entityKind,entities:ids);});}
    }
}
