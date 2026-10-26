namespace StaadPro.Interop.Models.Output
{
    /// <summary>Contains a complete four-family plate-center observation with captured case/plate identity.</summary>
    /// <remarks>Families are observed sequentially; even success does not prove an atomic or fresh analysis snapshot. Individual native evidence is retained on the enclosing OutputQueryResult. No force/stress dimension is merged.</remarks>
    public sealed class PlateCenterResults
    {
        internal PlateCenterResults(int plate, OutputLoadCaseIdentity loadCase, PlateCenterForces forces, PlateCenterMoments moments, PlateCenterVonMises vonMises, PlatePrincipalStresses principal)
        {
            PlateId = plate;
            LoadCase = loadCase;
            Forces = forces;
            Moments = moments;
            VonMises = vonMises;
            PrincipalStresses = principal;
        }
        /// <summary>Gets the positive captured plate ID.</summary>
        public int PlateId
        {
            get;
        }
        /// <summary>Gets caller-supplied case identity as an owned metadata snapshot.</summary>
        public OutputLoadCaseIdentity LoadCase
        {
            get;
        }
        /// <summary>Gets the five native local stresses, despite the historical Forces name.</summary>
        public PlateCenterForces Forces
        {
            get;
        }
        /// <summary>Gets native local plate moments per unit width.</summary>
        public PlateCenterMoments Moments
        {
            get;
        }
        /// <summary>Gets native top/bottom Von Mises stresses.</summary>
        public PlateCenterVonMises VonMises
        {
            get;
        }
        /// <summary>Gets native top/bottom in-plane normal principal stresses.</summary>
        public PlatePrincipalStresses PrincipalStresses
        {
            get;
        }
    }
}
