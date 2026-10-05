using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StaadPro.Interop.Enums
{
    /// <summary>
    /// Classifies the structural target or mechanism of an applied load item.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LoadItemType
    {
        [Description("Self Weight")]
        SelfWeight,
        [Description("Nodal Load")]
        NodalLoad,
        [Description("Member Load")]
        MemberLoad,
        [Description("Physical Member Load")]
        PhysicalMemberLoad,
        [Description("Element Load")]
        ElementLoad,
        [Description("Floor Load")]
        FloorLoad,
        [Description("Temperature Load")]
        TemperatureLoad,
        [Description("Seismic Load")]
        SeismicLoad,
        [Description("Wind Load")]
        WindLoad,
        [Description("Snow Load")]
        SnowLoad,
        [Description("Repeat Load")]
        RepeatLoad
    }
}
