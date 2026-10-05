using Newtonsoft.Json;
using StaadPro.Interop.Common.Units;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Interfaces;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Entities
{
    /// <summary>
    /// Represents a concentrated force and moment load applied to a structural node.
    /// </summary>
    public class NodalLoad : LoadItem
    {
        #region Constructors

        public NodalLoad() : base(LoadItemType.NodalLoad)
        {
            Forces = new Forces();
        }

        public NodalLoad(Forces forces) : base(LoadItemType.NodalLoad)
        {
            Forces = forces ?? new Forces();
        }

        public NodalLoad(double fx, double fy, double fz, double mx, double my, double mz) : base(LoadItemType.NodalLoad)
        {
            Forces = new Forces(fx, fy, fz, mx, my, mz);
        }

        #endregion

        #region Public Properties

        [JsonProperty]
        public Forces Forces { get; set; }

        #endregion

        #region Public Methods

        public NodalLoad DeepCopy()
        {
            return new NodalLoad(Forces.Fx, Forces.Fy, Forces.Fz, Forces.Mx, Forces.My, Forces.Mz)
            {
                LoadCase = this.LoadCase
            };
        }

        public void ConvertUsing(UnitsConverter uc)
        {
            Forces?.ConvertUsing(uc);
        }

        #endregion
    }
}
