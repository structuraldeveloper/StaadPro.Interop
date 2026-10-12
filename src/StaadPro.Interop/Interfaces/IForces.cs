namespace StaadPro.Interop.Interfaces
{
    /// <summary>
    /// Contract defining a 6-DOF force and moment vector.
    /// </summary>
    public interface IForces
    {
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='IForces.P:Fx']/*" />
        double Fx { get; set; }
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='IForces.P:Fy']/*" />
        double Fy { get; set; }
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='IForces.P:Fz']/*" />
        double Fz { get; set; }
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='IForces.P:Mx']/*" />
        double Mx { get; set; }
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='IForces.P:My']/*" />
        double My { get; set; }
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='IForces.P:Mz']/*" />
        double Mz { get; set; }
    }
}
