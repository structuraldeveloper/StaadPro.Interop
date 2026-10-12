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

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='NodalLoad..ctor()']/*" />
        public NodalLoad() : base(LoadItemType.NodalLoad)
        {
            Forces = new Forces();
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='NodalLoad..ctor(Forces)']/*" />
        public NodalLoad(Forces forces) : base(LoadItemType.NodalLoad)
        {
            Forces = forces ?? new Forces();
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='NodalLoad..ctor(double,double,double,double,double,double)']/*" />
        public NodalLoad(double fx, double fy, double fz, double mx, double my, double mz) : base(LoadItemType.NodalLoad)
        {
            Forces = new Forces(fx, fy, fz, mx, my, mz);
        }

        #endregion

        #region Public Properties

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='NodalLoad.P:Forces']/*" />
        [JsonProperty]
        public Forces Forces { get; set; }

        #endregion

        #region Public Methods

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='NodalLoad.DeepCopy`0()']/*" />
        public NodalLoad DeepCopy()
        {
            return new NodalLoad(Forces.Fx, Forces.Fy, Forces.Fz, Forces.Mx, Forces.My, Forces.Mz)
            {
                LoadCase = this.LoadCase
            };
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='NodalLoad.ConvertUsing`0(UnitsConverter)']/*" />
        public void ConvertUsing(UnitsConverter uc)
        {
            Forces?.ConvertUsing(uc);
        }

        #endregion
    }
}
