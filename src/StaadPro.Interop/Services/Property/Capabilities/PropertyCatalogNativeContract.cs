using System;
namespace StaadPro.Interop.Services.Property.Capabilities
{
    internal sealed class PropertyCatalogNativeContract
    {
        internal PropertyCatalogNativeContract(string name, params Type[] parameterTypes) { Name = name; ParameterTypes = parameterTypes; }
        internal string Name { get; }
        internal Type[] ParameterTypes { get; }
        internal static readonly PropertyCatalogNativeContract[] All = {
            new PropertyCatalogNativeContract("GetDefaultStandardProfileDBFolder"),
            new PropertyCatalogNativeContract("IsStandardDatabaseSection", typeof(object)),
            new PropertyCatalogNativeContract("GetStandardSectionDatabaseName", typeof(object)),
            new PropertyCatalogNativeContract("GetStandardSectionTableName", typeof(object)),
            new PropertyCatalogNativeContract("GetStandardSectionName", typeof(object)) };
    }
    internal enum PropertyMemberEvidence { Compatible, Absent, SignatureUnverified }
}
