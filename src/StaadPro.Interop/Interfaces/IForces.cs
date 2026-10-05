namespace StaadPro.Interop.Interfaces
{
    /// <summary>
    /// Contract defining a 6-DOF force and moment vector.
    /// </summary>
    public interface IForces
    {
        double Fx { get; set; }
        double Fy { get; set; }
        double Fz { get; set; }
        double Mx { get; set; }
        double My { get; set; }
        double Mz { get; set; }
    }
}
