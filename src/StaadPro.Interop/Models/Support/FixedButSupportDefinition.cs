using System;
using System.Linq;
using StaadPro.Interop.Entities;
namespace StaadPro.Interop.Models.Support
{
    /// <summary>Owns explicit fixed-but creation flags and six spring constants copied from the existing Releases and Springs models.</summary>
    /// <remarks>Read-back SupportDofState.Spring=-1 is not a creation flag. Creation uses six Double 0/1 release flags and six Double spring values. No null-to-zero coercion, unit conversion or managed restraint synthesis is performed.</remarks>
    public sealed class FixedButSupportDefinition
    {
        private readonly double[] releases, springs;
        /// <summary>Snapshots existing managed release/spring packets for a later native creation.</summary>
        /// <param name="releaseFlags">Non-null six Boolean creation flags; caller changes after construction do not alter this request.</param>
        /// <param name="springConstants">Non-null packet with every slot explicitly finite and nonnegative. Translational units are force/length; rotational units are moment/angle.</param>
        /// <exception cref="ArgumentNullException">A packet or spring slot is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">A stiffness is negative or nonfinite.</exception>
        public FixedButSupportDefinition(Releases releaseFlags,Springs springConstants)
        {
            if(releaseFlags==null)throw new ArgumentNullException(nameof(releaseFlags));
            if(springConstants==null)throw new ArgumentNullException(nameof(springConstants));
            var slots=new[]{springConstants.KFx,springConstants.KFy,springConstants.KFz,springConstants.KMx,springConstants.KMy,springConstants.KMz};
            if(slots.Any(x=>!x.HasValue))throw new ArgumentNullException(nameof(springConstants),"Every stiffness slot must be explicit.");
            springs=slots.Select(x=>x.Value).ToArray();
            if(springs.Any(x=>double.IsNaN(x)||double.IsInfinity(x)||x<0))throw new ArgumentOutOfRangeException(nameof(springConstants));
            releases=releaseFlags.ToDoubleArray();
        }
        /// <summary>Gets an independent copy of the six creation flags using the existing Releases model.</summary>
        /// <returns>A mutable copy; editing it cannot change this definition.</returns>
        public Releases ToReleaseFlags()=>new Releases{Fx=releases[0]==1,Fy=releases[1]==1,Fz=releases[2]==1,Mx=releases[3]==1,My=releases[4]==1,Mz=releases[5]==1};
        /// <summary>Gets an independent copy of all six stiffnesses using the existing Springs model.</summary>
        /// <returns>A mutable copy in native KFX/KFY/KFZ/KMX/KMY/KMZ order.</returns>
        public Springs ToSpringConstants()=>new Springs{KFx=springs[0],KFy=springs[1],KFz=springs[2],KMx=springs[3],KMy=springs[4],KMz=springs[5]};
        internal double[] NativeReleases()=>(double[])releases.Clone();
        internal double[] NativeSprings()=>(double[])springs.Clone();
    }
}
