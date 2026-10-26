using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Globalization;
public static class CornerEquilibriumProbe
{
  private static double[] Point(dynamic g,int id){object x=double.NaN,y=double.NaN,z=double.NaN;g.GetNodeCoordinates(id,ref x,ref y,ref z);return new[]{Convert.ToDouble(x),Convert.ToDouble(y),Convert.ToDouble(z)};}
  private static double[] Sub(double[] a,double[] b){return a.Zip(b,(x,y)=>x-y).ToArray();}
  private static double[] Cross(double[] a,double[] b){return new[]{a[1]*b[2]-a[2]*b[1],a[2]*b[0]-a[0]*b[2],a[0]*b[1]-a[1]*b[0]};}
  private static double Norm(double[] v){return Math.Sqrt(v.Sum(x=>x*x));}
  private static double[] Unit(double[] v){double n=Norm(v);return v.Select(x=>x/n).ToArray();}
  private static double[] Turn(double[] f,double[] x,double[] y,double[] z){return new[]{f[0]*x[0]+f[1]*y[0]+f[2]*z[0],f[0]*x[1]+f[1]*y[1]+f[2]*z[1],f[0]*x[2]+f[1]*y[2]+f[2]*z[2]};}
  private static int[] Incidence(dynamic g,int p){object a=int.MinValue,b=int.MinValue,c=int.MinValue,d=int.MinValue;g.GetPlateIncidence(p,ref a,ref b,ref c,ref d);return new[]{Convert.ToInt32(a),Convert.ToInt32(b),Convert.ToInt32(c),Convert.ToInt32(d)};}
  private static double[] Force(object raw){var a=(Array)raw;if(a.Length!=6)throw new Exception("Unexpected force shape");return Enumerable.Range(0,6).Select(i=>Convert.ToDouble(a.GetValue(a.GetLowerBound(0)+i))).ToArray();}
  public static string Run()
  {
    dynamic root=Marshal.GetActiveObject("StaadPro.OpenSTAAD");dynamic g=root.Geometry,o=root.Output,l=root.Load;
    if(!Convert.ToBoolean(o.AreResultsAvailable()))throw new Exception("No results");
    int pc=Convert.ToInt32(g.GetPlateCount()),cc=Convert.ToInt32(l.GetPrimaryLoadCaseCount());
    if(pc>50000||cc==0)throw new Exception("Bounded probe cannot proceed");
    object pl=new int[pc],cl=new int[cc];g.GetPlateList(ref pl);l.GetPrimaryLoadCaseNumbers(ref cl);
    int[] ids=((int[])pl),cases=((int[])cl).Take(2).ToArray();
    var incidence=new Dictionary<int,int[]>();var targetNodes=new HashSet<int>();
    foreach(int p in ids.Take(30)){var n=Incidence((object)g,p);foreach(int id in n.Where(i=>i>0))targetNodes.Add(id);}
    var connected=targetNodes.ToDictionary(n=>n,n=>new List<int>());
    foreach(int p in ids){var n=Incidence((object)g,p);foreach(int id in n.Distinct())if(connected.ContainsKey(id)){connected[id].Add(p);incidence[p]=n;}}
    var points=new Dictionary<int,double[]>();var results=new List<string>();int checkedNodes=0;
    foreach(int node in targetNodes.Take(70))
    {
      var plates=connected[node];if(plates.Count<2||plates.Count>20)continue;
      var bases=new Dictionary<int,double[][]>();
      foreach(int p in plates){var n=incidence[p];foreach(int id in n.Where(i=>i>0))if(!points.ContainsKey(id))points[id]=Point((object)g,id);var x=Unit(Sub(points[n[1]],points[n[0]]));var z=Unit(Cross(x,Sub(points[n[2]],points[n[0]])));var y=Cross(z,x);bases[p]=new[]{x,y,z};}
      for(int k=0;k<cases.Length;k++){
        var sum=new double[3];var rotated=new double[3];var moments=new double[3];var rotatedMoments=new double[3];double scale=0, momentScale=0;
        foreach(int p in plates){object f=Enumerable.Repeat(double.NaN,6).ToArray();object ok=o.GetPlateCornerForces(p,node,cases[k],ref f);if(!Convert.ToBoolean(ok))throw new Exception("Corner query failed");var v=Force(f);scale+=Norm(v.Take(3).ToArray());var r=Turn(v,bases[p][0],bases[p][1],bases[p][2]);var m=v.Skip(3).ToArray();momentScale+=Norm(m);var rm=Turn(m,bases[p][0],bases[p][1],bases[p][2]);for(int j=0;j<3;j++){sum[j]+=v[j];rotated[j]+=r[j];moments[j]+=m[j];rotatedMoments[j]+=rm[j];}}
        int bc=Convert.ToInt32(g.GetNoOfBeamsConnectedAtNode(node));object beams=new int[bc];if(bc>0)g.GetBeamsConnectedAtNode(node,ref beams);
        var bs=new double[3];var bm=new double[3];foreach(int b in (int[])beams){object a=0,e=0;g.GetMemberIncidence(b,ref a,ref e);int end=Convert.ToInt32(a)==node?0:1;object f=Enumerable.Repeat(double.NaN,6).ToArray();object ok=o.GetMemberEndForces(b,end,cases[k],ref f,1);if(!Convert.ToBoolean(ok))throw new Exception("Member query failed");var v=Force(f);scale+=Norm(v.Take(3).ToArray());for(int j=0;j<3;j++){bs[j]+=v[j];bm[j]+=v[j+3];}momentScale+=Norm(v.Skip(3).ToArray());}
        if(scale>1e-8){double raw=Math.Min(Norm(Sub(sum,bs)),Norm(sum.Zip(bs,(a,b)=>a+b).ToArray()))/scale;double loc=Math.Min(Norm(Sub(rotated,bs)),Norm(rotated.Zip(bs,(a,b)=>a+b).ToArray()))/scale;
          double rawM=momentScale==0?0:Math.Min(Norm(Sub(moments,bm)),Norm(moments.Zip(bm,(a,b)=>a+b).ToArray()))/momentScale;double locM=momentScale==0?0:Math.Min(Norm(Sub(rotatedMoments,bm)),Norm(rotatedMoments.Zip(bm,(a,b)=>a+b).ToArray()))/momentScale;
          if(raw<1e-6&&loc>1e-3)results.Add(string.Format(CultureInfo.InvariantCulture,"node={0},case={1},plates={2},beams={3},rawRelativeResidual={4:G6},rotatedRelativeResidual={5:G6},rawMomentResidual={6:G6},rotatedMomentResidual={7:G6}",node,cases[k],plates.Count,bc,raw,loc,rawM,locM));}
      }
      checkedNodes++;if(results.Count>=8)break;
    }
    object major=0,update=0,revision=0,build=0;root.GetApplicationVersion(ref major,ref update,ref revision,ref build);
    return "nativeVersion="+major+"."+update+"."+revision+"."+build+"; checkedNodes="+checkedNodes+"; evidenceSamples="+results.Count+Environment.NewLine+string.Join(Environment.NewLine,results);
  }
}