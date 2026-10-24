# Property API naming correction

The 1.0.0-preview.20261024.11 preview renames the following public UPT methods to PascalCase. Update source calls and rebuild consumers. This is a source/binary compatibility change: the prior names are not retained as aliases in this preview. Parameters, defaults, generic constraints and return types are unchanged. Existing immutable input types already use Upt casing. Bentley's native COM members remain unchanged and are invoked using their exact original names. PropertyMutationResult.NativeFunction continues to identify the native member.

| Previous public/native name | Current public name |
| --- | --- |
| `AddUPTPropertyWIDEFLANGE` | `AddUptPropertyWideFlange` |
| `AddUPTPropertyWIDEFLANGEUNEQUAL` | `AddUptPropertyWideFlangeUnequal` |
| `AddUPTPropertyWIDEFLANGECOMPOSITE` | `AddUptPropertyWideFlangeComposite` |
| `AddUPTPropertyCHANNEL` | `AddUptPropertyChannel` |
| `AddUPTPropertyANGLE` | `AddUptPropertyAngle` |
| `AddUPTPropertyDOUBLEANGLE` | `AddUptPropertyDoubleAngle` |
| `AddUPTPropertyTEE` | `AddUptPropertyTee` |
| `AddUPTPropertyPIPE` | `AddUptPropertyPipe` |
| `AddUPTPropertyTUBE` | `AddUptPropertyTube` |
| `AddUPTPropertyGENERAL` | `AddUptPropertyGeneral` |
| `AddUPTPropertyISECTION` | `AddUptPropertyISection` |
| `AddUPTPropertyPRISMATIC` | `AddUptPropertyPrismatic` |
| `CreateUPTTable` | `CreateUptTable` |
| `CreateUPTTableEx` | `CreateUptTableEx` |
| `CreatePropertyFromUPTTable` | `CreatePropertyFromUptTable` |
| `RemoveUPTTable` | `RemoveUptTable` |
| `RemovePropertyFromUPTTable` | `RemovePropertyFromUptTable` |

```csharp
var primary = new StaadPro.Interop.Models.Property.UptWideFlangeDefinition(0.01, 0.3, 0.006, 0.15, 0.01, 0.000001, 0.00001, 0.0001, 0.002, 0.003);
var definition = new StaadPro.Interop.Models.Property.UptChannelDefinition(primary, 0.04);
bool applyChanges = false;
if (applyChanges)
{
    var result = property.AddUptPropertyChannel(7, "CHANNEL", definition);
    Console.WriteLine(result.NativeFunction); // AddUPTPropertyCHANNEL
}
```

The [rename index](contracts/PropertyApiRenames.json) records the complete mapping. The [UPT guide](Property-UPT-Profiles-and-Catalogs.md) uses the current public names. Historical package archives remain immutable.
