# Property section completion and native observations

This increment ports the 19 remaining active main-interface signatures plus six documented native Property observations. This checkpoint reached 125 overloads; the [latest material/member-section increment](Property-Materials-and-Member-Sections.md) reached 150; the [latest creation/specification increment](Property-Section-and-Specification-Creation.md) reached 175; the [latest fireproofing/attribute/UPT increment](Property-Fireproofing-Attributes-and-UPT.md) brings the current total to 200. Existing typed return collisions and five obsolete source overloads remain excluded; they cannot be represented as duplicate C# signatures or treated as working APIs.

## Supporting boundary and deliberate corrections

- Preserved the main SectionProperty/SectionDefinition.SectionProfile.SelectedSection/AdditionalSpecifications creation graph through small H/C/L/O/Box table carriers. Private catalog/material selection, UI validation and display integrations are not imported.
- IGenericSection preserves the identity, values, area and assignments required by the ports. Models carry data; the source model-level raw COM creation/catalog-display methods are excluded. All native writes stay inside the Property adapter.
- Selected 47 reviewed section layouts using the acquired STAAD 24/25/26 major version. 2024 differs from 2025/2026 in nine layouts. Required slots use unwritten sentinels; unused unwritten padding is zero-filled only after validation. Unknown versions/types fail explicitly.
- Preserved native Ax or the source tapered-I mean plate area. BeamD and UPT ISECTION omit Ax and return null from GetSectionalArea; their model Ax getter throws rather than fabricating a catalog/rectangular/default-zero area. No inertia-axis reinterpretation or private section database lookup is performed.
- Corrected the source candidate assignment query filtering an unfilled array. Both overloads now populate actual native assignment IDs, preserving supplied object identity.
- Preserved all source creation families/modifier routes, including channel BA, bottom-cover BC with Bottom dimensions, user-defined pipe/tube dimensions and the four source CT/FC/CW/CD composite values. Native acceptance/units require qualification. Unsupported requests fail before dispatch instead of silently doing nothing.
- Adapted the proprietary OSPropertyUI explicit-receiver parameter to dynamic/System.Object. It must be the same captured ComObject; foreign receivers fail before native access.
- Reused the repository UnitsConverter with E/G=force/length-squared and weight density=force/length-cubed aliases. Converter selectors and all packet inputs are snapshotted/validated. Values use repository double-precision constants; the private decimal units implementation is not imported. Creation statuses remain separate from IDs.
- Preserved direct native record zero and -1 outcomes without assuming one-based indexing. Scalar counts do not allocate; packet/inventory retrieval enforces its own limits.

## Shared rules

Shares this Property adapter's serialization and captured-binding checks; reacquire wrapper.Property after refresh. Native writes are checked around each dispatch and execute synchronously on the caller COM apartment. Other wrappers/raw calls and external edits are not locked out. Native writes may partially complete; no implicit save, analysis, retry or rollback occurs. Only the explicit material converter overloads transform units. Native/engineering qualification remains necessary.

Creation/assignment examples assume a suitable disposable native model, existing material and matching IDs. Package verification runs every example only against managed stand-ins.

## All 25 signatures

## 1. AssignSectionProperties

```csharp
IOSProperty AssignSectionProperties(HashSet<IGenericSection> gsSet)
```

Assigns native section IDs from retrieved section packets to their candidate beams.

- `gsSet`: Null/empty is a no-op. Non-null packets with nonempty AssignedBeams require positive existing section/member IDs; empty assignments are skipped.

**Returns:** The same Property adapter for fluent chaining after the requested operations complete.

When to use: Use with GetAllSections/GetSectionBy results when section definitions already exist. Copies candidate IDs before dispatch and calls AssignBeamProperty once per section with an Int32 array, requiring integral zero success. Does not create sections, hydrate geometry, change managed beam section references, save or roll back earlier writes.

Origin: main-interface method with the documented supporting-type adaptation.

Native routes: `AssignBeamProperty`.

```csharp
var section = property.GetSectionBy(37);
section.AssignedBeams.Add(new Beam { Id = 17 });
property.AssignSectionProperties(new System.Collections.Generic.HashSet<StaadPro.Interop.Interfaces.IGenericSection> { section });
```

## 2. AssignSectionProperties

```csharp
IOSProperty AssignSectionProperties(HashSet<SectionProperty> spSet)
```

Assigns existing section definition IDs to their managed member candidates.

- `spSet`: Null/empty is a no-op. Non-null packets with nonempty Beams require positive existing section/member IDs; creation definitions are not required for assignment.

**Returns:** The same Property adapter for fluent chaining after the requested operations complete.

When to use: Use after resolving or creating positive native IDs. All target IDs are validated and copied before the first native write. Calls AssignBeamProperty with each packet's member-ID array. Integral zero is success; nonzero codes throw and may describe partial assignment. Does not create definitions, infer materials, populate beam references, save, retry or roll back earlier writes.

Origin: main-interface method with the documented supporting-type adaptation.

Native routes: `AssignBeamProperty`.

```csharp
var definition = new StaadPro.Interop.Models.Sections.SectionDefinition();
definition.SectionProfile.SelectedSection = new StaadPro.Interop.Models.Sections.RolledSectionHShape { Country = Country.Indian, StaadName = "ISMB300", Designation = "ISMB300" };
var section = new SectionProperty { Id = 37, SectionDefinition = definition };
section.Beams.Add(new Beam { Id = 17 });
property.AssignSectionProperties(new System.Collections.Generic.HashSet<SectionProperty> { section });
```

## 3. AssignSectionProperties

```csharp
IOSProperty AssignSectionProperties(HashSet<SectionProperty> sectionProperties, int nThreads)
```

Assigns existing section definition IDs to their managed member candidates.

- `sectionProperties`: Null/empty is a no-op. Non-null packets with nonempty Beams require positive existing section/member IDs; creation definitions are not required for assignment.
- `nThreads`: Source-compatible degree from 1 through 512. All COM calls remain serialized on the caller apartment; no worker-thread COM dispatch occurs.

**Returns:** The same Property adapter for fluent chaining after the requested operations complete.

When to use: Use after resolving or creating positive native IDs. All target IDs are validated and copied before the first native write. Calls AssignBeamProperty for each individual member, preserving the source scalar route. Integral zero is success; nonzero codes throw and may describe partial assignment. Does not create definitions, infer materials, populate beam references, save, retry or roll back earlier writes.

Origin: main-interface method with the documented supporting-type adaptation.

Native routes: `AssignBeamProperty`.

```csharp
var definition = new StaadPro.Interop.Models.Sections.SectionDefinition();
definition.SectionProfile.SelectedSection = new StaadPro.Interop.Models.Sections.RolledSectionHShape { Country = Country.Indian, StaadName = "ISMB300", Designation = "ISMB300" };
var section = new SectionProperty { Id = 37, SectionDefinition = definition };
section.Beams.Add(new Beam { Id = 17 });
property.AssignSectionProperties(new System.Collections.Generic.HashSet<SectionProperty> { section }, 2);
```

## 4. AssignSectionProperty

```csharp
IOSProperty AssignSectionProperty(SectionProperty sp)
```

Assigns existing section definition IDs to their managed member candidates.

- `sp`: Null/empty is a no-op. Non-null packets with nonempty Beams require positive existing section/member IDs; creation definitions are not required for assignment.

**Returns:** The same Property adapter for fluent chaining after the requested operations complete.

When to use: Use after resolving or creating positive native IDs. All target IDs are validated and copied before the first native write. Calls AssignBeamProperty with each packet's member-ID array. Integral zero is success; nonzero codes throw and may describe partial assignment. Does not create definitions, infer materials, populate beam references, save, retry or roll back earlier writes.

Origin: main-interface method with the documented supporting-type adaptation.

Native routes: `AssignBeamProperty`.

```csharp
var definition = new StaadPro.Interop.Models.Sections.SectionDefinition();
definition.SectionProfile.SelectedSection = new StaadPro.Interop.Models.Sections.RolledSectionHShape { Country = Country.Indian, StaadName = "ISMB300", Designation = "ISMB300" };
var section = new SectionProperty { Id = 37, SectionDefinition = definition };
section.Beams.Add(new Beam { Id = 17 });
property.AssignSectionProperty(section);
```

## 5. AssignSectionProperty

```csharp
IOSProperty AssignSectionProperty(SectionProperty sectionProperty, int nThreads)
```

Assigns existing section definition IDs to their managed member candidates.

- `sectionProperty`: Null/empty is a no-op. Non-null packets with nonempty Beams require positive existing section/member IDs; creation definitions are not required for assignment.
- `nThreads`: Source-compatible degree from 1 through 512. All COM calls remain serialized on the caller apartment; no worker-thread COM dispatch occurs.

**Returns:** The same Property adapter for fluent chaining after the requested operations complete.

When to use: Use after resolving or creating positive native IDs. All target IDs are validated and copied before the first native write. Calls AssignBeamProperty for each individual member, preserving the source scalar route. Integral zero is success; nonzero codes throw and may describe partial assignment. Does not create definitions, infer materials, populate beam references, save, retry or roll back earlier writes.

Origin: main-interface method with the documented supporting-type adaptation.

Native routes: `AssignBeamProperty`.

```csharp
var definition = new StaadPro.Interop.Models.Sections.SectionDefinition();
definition.SectionProfile.SelectedSection = new StaadPro.Interop.Models.Sections.RolledSectionHShape { Country = Country.Indian, StaadName = "ISMB300", Designation = "ISMB300" };
var section = new SectionProperty { Id = 37, SectionDefinition = definition };
section.Beams.Add(new Beam { Id = 17 });
property.AssignSectionProperty(section, 2);
```

## 6. CreateAndAssignSectionProperties

```csharp
IOSProperty CreateAndAssignSectionProperties(HashSet<Beam> beams)
```

Groups explicit beam section definitions, creates them and assigns their members.

- `beams`: Non-null set of non-null positive-ID beams. Null SectionProperty references are ignored. Complete H/C/L/O/Box selections and valid additional values are required for non-null definitions.

**Returns:** The same Property adapter for fluent chaining after the requested operations complete.

When to use: Use to apply managed SectionProperty definitions in one ordered workflow. Groups equal selected profile identities, modifiers and additional values; retains canonical existing Beams and adds supplied beam references. Validates/snapshots all requests and target IDs first, creates every grouped definition, propagates each positive native ID to all equal source packets, then assigns grouped member-ID arrays. Set the intended existing current material beforehand. A later creation/assignment failure leaves earlier native changes and successful IDs applied; no transaction, cleanup, save, implicit material selection or conversion occurs. Empty input/no definitions is a no-op. Private catalog/UI services are not imported.

Origin: main-interface method with the documented supporting-type adaptation.

Native routes: `CreateBeamPropertyFromTable`, `CreateTeePropertyFromTable`, `CreateBeamPropertyFromTableWithCoverPlates`, `CreateBeamPropertyFromTableComposite`, `CreateChannelPropertyFromTable`, `CreateAnglePropertyFromTable`, `CreatePipePropertyFromTable`, `CreateTubePropertyFromTable`, `AssignBeamProperty`.

```csharp
var definition = new StaadPro.Interop.Models.Sections.SectionDefinition();
definition.SectionProfile.SelectedSection = new StaadPro.Interop.Models.Sections.RolledSectionHShape { Country = Country.Indian, StaadName = "ISMB300", Designation = "ISMB300" };
var section = new SectionProperty { Id = 37, SectionDefinition = definition };
section.Beams.Add(new Beam { Id = 17 });
property.SetMaterialName("STEEL");
var beam = new Beam { Id = 17, SectionProperty = section };
property.CreateAndAssignSectionProperties(new System.Collections.Generic.HashSet<Beam> { beam });
```

## 7. CreateAndAssignSectionProperties

```csharp
IOSProperty CreateAndAssignSectionProperties(HashSet<Beam> beams, int nThreads)
```

Groups explicit beam section definitions, creates them and assigns their members.

- `beams`: Non-null set of non-null positive-ID beams. Null SectionProperty references are ignored. Complete H/C/L/O/Box selections and valid additional values are required for non-null definitions.
- `nThreads`: Source-compatible degree from 1 through 512. All COM calls remain serialized on the caller apartment; no worker-thread COM dispatch occurs.

**Returns:** The same Property adapter for fluent chaining after the requested operations complete.

When to use: Use to apply managed SectionProperty definitions in one ordered workflow. Groups equal selected profile identities, modifiers and additional values; retains canonical existing Beams and adds supplied beam references. Validates/snapshots all requests and target IDs first, creates every grouped definition, propagates each positive native ID to all equal source packets, then assigns individual member IDs. Set the intended existing current material beforehand. A later creation/assignment failure leaves earlier native changes and successful IDs applied; no transaction, cleanup, save, implicit material selection or conversion occurs. Empty input/no definitions is a no-op. Private catalog/UI services are not imported.

Origin: main-interface method with the documented supporting-type adaptation.

Native routes: `CreateBeamPropertyFromTable`, `CreateTeePropertyFromTable`, `CreateBeamPropertyFromTableWithCoverPlates`, `CreateBeamPropertyFromTableComposite`, `CreateChannelPropertyFromTable`, `CreateAnglePropertyFromTable`, `CreatePipePropertyFromTable`, `CreateTubePropertyFromTable`, `AssignBeamProperty`.

```csharp
var definition = new StaadPro.Interop.Models.Sections.SectionDefinition();
definition.SectionProfile.SelectedSection = new StaadPro.Interop.Models.Sections.RolledSectionHShape { Country = Country.Indian, StaadName = "ISMB300", Designation = "ISMB300" };
var section = new SectionProperty { Id = 37, SectionDefinition = definition };
section.Beams.Add(new Beam { Id = 17 });
property.SetMaterialName("STEEL");
var beam = new Beam { Id = 17, SectionProperty = section };
property.CreateAndAssignSectionProperties(new System.Collections.Generic.HashSet<Beam> { beam }, 2);
```

## 8. CreateMaterialProperties

```csharp
IOSProperty CreateMaterialProperties(HashSet<MaterialProperty> mpSet, UnitsConverter uc)
```

Creates or updates isotropic materials using explicit dimensional conversion factors.

- `mpSet`: Non-null material packet, or non-null nonempty set without null packets. Finite named creation inputs are required; all packets are validated before native writes.
- `uc`: Non-null repository UnitsConverter. Valid source/target length/force selectors are copied once; E/G use force/length-squared and weight density uses force/length-cubed.

**Returns:** The same Property adapter for fluent chaining after the requested operations complete.

When to use: Use when caller constants need explicit conversion to the acquired native creation convention. Multiplies E, derived G and weight density by the copied corresponding factors, matching the source overload's dimensions; Poisson, alpha and damping pass unchanged. Does not apply the legacy E/G x1000 scale, alter the source packet or change native input units. Uses repository double-precision unit constants rather than the private decimal units library. Native 0=Created and 1=Updated are recorded in LastCreationStatus; Id remains unchanged. Strength fields are not sent. Earlier successes survive later failure; no assignment, save or rollback occurs.

Origin: main-interface method with the documented supporting-type adaptation.

Native routes: `CreateIsotropicMaterialProperties`.

```csharp
var material = new MaterialProperty("STEEL") { E = 200000000.0, Poisson = 0.3, Density = 77.0 };
var converter = new StaadPro.Interop.Common.Units.UnitsConverter(LengthInputUnit.Meter, ForceInputUnit.KiloNewton, LengthInputUnit.MilliMeter, ForceInputUnit.Newton);
property.CreateMaterialProperties(new System.Collections.Generic.HashSet<MaterialProperty> { material }, converter);
```

## 9. CreateMaterialProperty

```csharp
IOSProperty CreateMaterialProperty(MaterialProperty mp, UnitsConverter uc)
```

Creates or updates isotropic materials using explicit dimensional conversion factors.

- `mp`: Non-null material packet, or non-null nonempty set without null packets. Finite named creation inputs are required; all packets are validated before native writes.
- `uc`: Non-null repository UnitsConverter. Valid source/target length/force selectors are copied once; E/G use force/length-squared and weight density uses force/length-cubed.

**Returns:** The same Property adapter for fluent chaining after the requested operations complete.

When to use: Use when caller constants need explicit conversion to the acquired native creation convention. Multiplies E, derived G and weight density by the copied corresponding factors, matching the source overload's dimensions; Poisson, alpha and damping pass unchanged. Does not apply the legacy E/G x1000 scale, alter the source packet or change native input units. Uses repository double-precision unit constants rather than the private decimal units library. Native 0=Created and 1=Updated are recorded in LastCreationStatus; Id remains unchanged. Strength fields are not sent. Earlier successes survive later failure; no assignment, save or rollback occurs.

Origin: main-interface method with the documented supporting-type adaptation.

Native routes: `CreateIsotropicMaterialProperties`.

```csharp
var material = new MaterialProperty("STEEL") { E = 200000000.0, Poisson = 0.3, Density = 77.0 };
var converter = new StaadPro.Interop.Common.Units.UnitsConverter(LengthInputUnit.Meter, ForceInputUnit.KiloNewton, LengthInputUnit.MilliMeter, ForceInputUnit.Newton);
property.CreateMaterialProperty(material, converter);
```

## 10. CreatePlateThicknessProperty

```csharp
IOSProperty CreatePlateThicknessProperty(dynamic property, PlateThickness pt)
```

Creates a uniform thickness definition through the explicitly supplied captured receiver.

- `property`: This adapter's ComObject instance. The proprietary OSPropertyUI parameter is adapted to dynamic (System.Object) to avoid a vendor assembly dependency; null and foreign receivers are rejected before native access.
- `pt`: Non-null packet with finite positive scalar Thickness in the acquired native length convention. Creation does not use its candidate set.

**Returns:** The same Property adapter for fluent chaining after the requested operations complete.

When to use: Use for source compatibility with the explicit-receiver overload. Requires reference identity with this adapter's captured Property binding, so a foreign model cannot be modified through the wrapper accidentally. Sends the scalar thickness unchanged and stores only a positive created definition ID; failure preserves the prior ID. Does not assign plates, convert dimensions, save or roll back native creation.

Origin: main-interface method with the documented supporting-type adaptation.

Native routes: `CreatePlateThicknessProperty`.

```csharp
var thickness = new PlateThickness { Thickness = 0.2 };
property.CreatePlateThicknessProperty(property.ComObject, thickness);
System.Console.WriteLine(thickness.Id);
```

## 11. CreateSectionProperties

```csharp
IOSProperty CreateSectionProperties(HashSet<SectionProperty> spSet)
```

Creates native section definitions from explicit H/C/L/O/Box profile selections.

- `spSet`: Non-null complete section definition, or non-null nonempty set without null definitions. All selected names/country/modifier codes and applicable additional values are validated/copied before dispatch.

**Returns:** The same Property adapter for fluent chaining after the requested operations complete.

When to use: Use for the main table-selection creation graph. H supports ST/T/D/TC/BC/TB/CM, C supports ST/D/FR/BA, L supports ST/RA/SA/LD/SD, and pipe/tube support ST/UserDefined. Uses the corresponding native table routine and actual BC code with Bottom dimensions. Composite creation forwards source CT/FC/CW/CD values unchanged; acquired-build support requires qualification. Unsupported families/modifiers throw before native writes instead of silently doing nothing. Set the intended existing material first. Only positive native results replace packet IDs; earlier successful creations remain on later failure. No assignment, private catalog lookup, conversion, implicit save or rollback.

Origin: main-interface method with the documented supporting-type adaptation.

Native routes: `CreateBeamPropertyFromTable`, `CreateTeePropertyFromTable`, `CreateBeamPropertyFromTableWithCoverPlates`, `CreateBeamPropertyFromTableComposite`, `CreateChannelPropertyFromTable`, `CreateAnglePropertyFromTable`, `CreatePipePropertyFromTable`, `CreateTubePropertyFromTable`.

```csharp
var definition = new StaadPro.Interop.Models.Sections.SectionDefinition();
definition.SectionProfile.SelectedSection = new StaadPro.Interop.Models.Sections.RolledSectionHShape { Country = Country.Indian, StaadName = "ISMB300", Designation = "ISMB300" };
var section = new SectionProperty { Id = 37, SectionDefinition = definition };
section.Beams.Add(new Beam { Id = 17 });
property.SetMaterialName("STEEL");
property.CreateSectionProperties(new System.Collections.Generic.HashSet<SectionProperty> { section });
System.Console.WriteLine(section.Id);
```

## 12. CreateSectionProperty

```csharp
IOSProperty CreateSectionProperty(SectionProperty sp)
```

Creates native section definitions from explicit H/C/L/O/Box profile selections.

- `sp`: Non-null complete section definition, or non-null nonempty set without null definitions. All selected names/country/modifier codes and applicable additional values are validated/copied before dispatch.

**Returns:** The same Property adapter for fluent chaining after the requested operations complete.

When to use: Use for the main table-selection creation graph. H supports ST/T/D/TC/BC/TB/CM, C supports ST/D/FR/BA, L supports ST/RA/SA/LD/SD, and pipe/tube support ST/UserDefined. Uses the corresponding native table routine and actual BC code with Bottom dimensions. Composite creation forwards source CT/FC/CW/CD values unchanged; acquired-build support requires qualification. Unsupported families/modifiers throw before native writes instead of silently doing nothing. Set the intended existing material first. Only positive native results replace packet IDs; earlier successful creations remain on later failure. No assignment, private catalog lookup, conversion, implicit save or rollback.

Origin: main-interface method with the documented supporting-type adaptation.

Native routes: `CreateBeamPropertyFromTable`, `CreateTeePropertyFromTable`, `CreateBeamPropertyFromTableWithCoverPlates`, `CreateBeamPropertyFromTableComposite`, `CreateChannelPropertyFromTable`, `CreateAnglePropertyFromTable`, `CreatePipePropertyFromTable`, `CreateTubePropertyFromTable`.

```csharp
var definition = new StaadPro.Interop.Models.Sections.SectionDefinition();
definition.SectionProfile.SelectedSection = new StaadPro.Interop.Models.Sections.RolledSectionHShape { Country = Country.Indian, StaadName = "ISMB300", Designation = "ISMB300" };
var section = new SectionProperty { Id = 37, SectionDefinition = definition };
section.Beams.Add(new Beam { Id = 17 });
property.SetMaterialName("STEEL");
property.CreateSectionProperty(section);
System.Console.WriteLine(section.Id);
```

## 13. GetAllSections

```csharp
HashSet<IGenericSection> GetAllSections()
```

Reads native section definitions into owned version-decoded section packets.


**Returns:** A new mutable set of section packets with native IDs, identity, raw-capacity values, applicable field labels, area availability and empty AssignedBeams sets. Empty inventory returns an empty set.

When to use: Use to enumerate definitions without loading geometry. Uses bounded section inventory/count checks, reads each version-specific packet, then rechecks inventory IDs/order. Supports all 47 reviewed top-level layouts for acquired STAAD 24/25/26; unknown versions/types fail explicitly. Native identity is retained without private catalog naming/area substitutions. Count/order checks cannot rule out same-ID external edits. No native mutation, inferred units or hidden member lookup occurs.

Origin: main-interface method with the documented supporting-type adaptation.

Native routes: `GetSectionPropertyCount`, `GetSectionPropertyList`, `GetSectionPropertyType`, `GetCountofSectionPropertyValuesEx`, `GetSectionPropertyValuesEx`, `GetSectionPropertyName`, `GetSectionPropertyCountry`, `GetRecordForSection`.

```csharp
foreach (var section in property.GetAllSections()) System.Console.WriteLine(section.StaadName);
```

## 14. GetAllSectionsWithAssignedBeamsList

```csharp
HashSet<IGenericSection> GetAllSectionsWithAssignedBeamsList(IEnumerable<Beam> allBeams)
```

Reads section packets and populates their candidate member references.

- `allBeams`: Non-null finite sequence of non-null positive-ID candidate beams. Enumerated once and matching original references are reused.

**Returns:** A new mutable set of section packets whose AssignedBeams contain matching candidate/hydrated references; unknown native IDs are not synthesized.

When to use: Use the candidate overload when beams are already loaded; use Geometry for full hydration. Reads checked version-specific definitions and each native assignment subset using bounded count/list contracts. Does not assign members or alter supplied beam objects. Geometry/candidate provenance must match the model; separate observations are not a model-wide atomic snapshot. No private catalog graph, save or unit conversion is used.

Origin: main-interface method with the documented supporting-type adaptation.

Native routes: `GetSectionPropertyCount`, `GetSectionPropertyList`, `GetSectionPropertyValuesEx`, `GetSectionPropertyType`, `GetSectionPropertyName`, `GetSectionPropertyCountry`, `GetRecordForSection`, `GetSectionPropertyAssignedBeamCount`, `GetSectionPropertyAssignedBeamList`.

```csharp
var sections = property.GetAllSectionsWithAssignedBeamsList(new[] { new Beam { Id = 17 } });
System.Console.WriteLine(sections.Count);
```

## 15. GetAllSectionsWithAssignedBeamsList

```csharp
HashSet<IGenericSection> GetAllSectionsWithAssignedBeamsList(IOSGeometry geometry)
```

Reads section packets and populates their candidate member references.

- `geometry`: Non-null Geometry adapter for the same model; hydrates all beams using GetAllEntities<Beam>(1).

**Returns:** A new mutable set of section packets whose AssignedBeams contain matching candidate/hydrated references; unknown native IDs are not synthesized.

When to use: Use the candidate overload when beams are already loaded; use Geometry for full hydration. Reads checked version-specific definitions and each native assignment subset using bounded count/list contracts. Does not assign members or alter supplied beam objects. Geometry/candidate provenance must match the model; separate observations are not a model-wide atomic snapshot. No private catalog graph, save or unit conversion is used.

Origin: main-interface method with the documented supporting-type adaptation.

Native routes: `GetSectionPropertyCount`, `GetSectionPropertyList`, `GetSectionPropertyValuesEx`, `GetSectionPropertyType`, `GetSectionPropertyName`, `GetSectionPropertyCountry`, `GetRecordForSection`, `GetSectionPropertyAssignedBeamCount`, `GetSectionPropertyAssignedBeamList`.

```csharp
var sections = property.GetAllSectionsWithAssignedBeamsList(wrapper.Geometry);
System.Console.WriteLine(sections.Count);
```

## 16. GetSectionalArea

```csharp
double? GetSectionalArea(int propId)
```

Reads an available native section area, or the source mean tapered-I plate area.

- `propId`: Positive native section property reference ID; not a member ID or section type code.

**Returns:** Ax in native length-squared units where supplied, or the source mean start/end tapered-I area. Returns null for BeamD and UPT ISECTION, whose native layouts omit Ax; zero is retained only when explicitly returned/calculated.

When to use: Use when a nullable area is needed without touching an assigned member. The source already returns null for BeamD; this port also avoids its fabricated default-zero UPT ISECTION area. Does not fetch a private catalog area or infer an exact double-beam area from rectangular dimensions. Version/type/required-output failures throw. No unit conversion, native mutation or engineering adequacy check occurs.

Origin: main-interface method with the documented supporting-type adaptation.

Native routes: `GetSectionPropertyType`, `GetCountofSectionPropertyValuesEx`, `GetSectionPropertyValuesEx`, `GetSectionPropertyName`, `GetSectionPropertyCountry`, `GetRecordForSection`.

```csharp
double? area = property.GetSectionalArea(37);
System.Console.WriteLine(area);
```

## 17. GetSectionBy

```csharp
IGenericSection GetSectionBy(int propId)
```

Reads one native section definition with the acquired version-specific value layout.

- `propId`: Positive native section property reference ID, not an analytical member number.

**Returns:** A new owned GenericSection implementing IGenericSection. Id/native name/country/type/record and raw-capacity values are retained; AssignedBeams is empty. Native record -1 means unavailable/not applicable. Ax throws for missing area layouts; inspect GenericSection.HasSectionalArea or GetSectionalArea first.

When to use: Use before reading a section's identity/field values or populating its assignments. Selects one of 47 native layouts from wrapper.StaadVersion.Major 24, 25 or 26, never the host ApplicationVersion. All required slots must be explicitly written and finite; unused unwritten padding becomes zero. Capacity is bounded to 100000 and rechecked with type/country. Preserves native labels and inertia slot identities without axis reinterpretation. Non-table records are -1; table lookup -1 is retained without failing an otherwise valid packet. Native Ax is used where present; tapered-I mean area follows source plate geometry; BeamD/UPT ISECTION have no substituted area. Unknown profiles fail explicitly. No private catalog, raw COM methods on models, model writes or unit conversion.

Origin: main-interface method with the documented supporting-type adaptation.

Native routes: `GetSectionPropertyType`, `GetCountofSectionPropertyValuesEx`, `GetSectionPropertyValuesEx`, `GetSectionPropertyName`, `GetSectionPropertyCountry`, `GetRecordForSection`.

```csharp
var section = property.GetSectionBy(37);
System.Console.WriteLine(section.StaadName);
```

## 18. GetSectionByPropertyIdWithAssignedBeamsList

```csharp
IGenericSection GetSectionByPropertyIdWithAssignedBeamsList(int propId, IEnumerable<Beam> allBeams)
```

Reads one native section and fills its actual candidate member assignment subset.

- `propId`: Positive native section property reference ID.
- `allBeams`: Non-null finite sequence of non-null positive-ID candidate beams; enumerated once. Matching original references are reused.

**Returns:** A new section packet with AssignedBeams populated from the actual checked native assignment list; unmatched native IDs do not create new candidate objects.

When to use: Use to obtain identity/values and assignments for one section. Corrects the main candidate overload filtering an allocated but unfilled zero array: both overloads now use the actual returned assignment IDs. Reads the reviewed version-specific section packet and bounded assignment list, rechecking positive counts. Does not change supplied beams, assign sections, save or convert units; external edits across observations remain possible.

Origin: main-interface method with the documented supporting-type adaptation.

Native routes: `GetSectionPropertyValuesEx`, `GetSectionPropertyType`, `GetSectionPropertyCountry`, `GetSectionPropertyName`, `GetRecordForSection`, `GetSectionPropertyAssignedBeamCount`, `GetSectionPropertyAssignedBeamList`.

```csharp
var section = property.GetSectionByPropertyIdWithAssignedBeamsList(37, new[] { new Beam { Id = 17 } });
System.Console.WriteLine(section.AssignedBeams.Count);
```

## 19. GetSectionByPropertyIdWithAssignedBeamsList

```csharp
IGenericSection GetSectionByPropertyIdWithAssignedBeamsList(int propId, IOSGeometry geometry)
```

Reads one native section and fills its actual candidate member assignment subset.

- `propId`: Positive native section property reference ID.
- `geometry`: Non-null Geometry adapter for the same model; hydrates all beams with degree one.

**Returns:** A new section packet with AssignedBeams populated from the actual checked native assignment list; unmatched native IDs do not create new candidate objects.

When to use: Use to obtain identity/values and assignments for one section. Corrects the main candidate overload filtering an allocated but unfilled zero array: both overloads now use the actual returned assignment IDs. Reads the reviewed version-specific section packet and bounded assignment list, rechecking positive counts. Does not change supplied beams, assign sections, save or convert units; external edits across observations remain possible.

Origin: main-interface method with the documented supporting-type adaptation.

Native routes: `GetSectionPropertyValuesEx`, `GetSectionPropertyType`, `GetSectionPropertyCountry`, `GetSectionPropertyName`, `GetRecordForSection`, `GetSectionPropertyAssignedBeamCount`, `GetSectionPropertyAssignedBeamList`.

```csharp
var section = property.GetSectionByPropertyIdWithAssignedBeamsList(37, wrapper.Geometry);
System.Console.WriteLine(section.AssignedBeams.Count);
```

## 20. GetSectionPropertyCount

```csharp
int GetSectionPropertyCount()
```

Counts native section definitions.


**Returns:** The actual nonnegative native Int32-range count/capacity; zero is retained. This scalar observation does not allocate or return values.

When to use: Use to observe native inventory size/capacity directly. Does not enumerate definitions, infer assigned entity counts or provide a universal section schema. Rejects Boolean, floating-point, text and negative count responses instead of coercing them. Section packet retrieval applies its own allocation and positive-capacity limits. No writes, unit conversion or save occur.

Origin: documented native function.

Native routes: `GetSectionPropertyCount`.

```csharp
System.Console.WriteLine(property.GetSectionPropertyCount());
```

## 21. GetThicknessPropertyCount

```csharp
int GetThicknessPropertyCount()
```

Counts native thickness definitions.


**Returns:** The actual nonnegative native Int32-range count/capacity; zero is retained. This scalar observation does not allocate or return values.

When to use: Use to observe native inventory size/capacity directly. Does not enumerate definitions, infer assigned entity counts or provide a universal section schema. Rejects Boolean, floating-point, text and negative count responses instead of coercing them. Section packet retrieval applies its own allocation and positive-capacity limits. No writes, unit conversion or save occur.

Origin: documented native function.

Native routes: `GetThicknessPropertyCount`.

```csharp
System.Console.WriteLine(property.GetThicknessPropertyCount());
```

## 22. GetCountofSectionPropertyValuesEx

```csharp
int GetCountofSectionPropertyValuesEx()
```

Reads the native section-value buffer capacity.


**Returns:** The actual nonnegative native Int32-range count/capacity; zero is retained. This scalar observation does not allocate or return values.

When to use: Use to observe native inventory size/capacity directly. Does not enumerate definitions, infer assigned entity counts or provide a universal section schema. Rejects Boolean, floating-point, text and negative count responses instead of coercing them. Section packet retrieval applies its own allocation and positive-capacity limits. No writes, unit conversion or save occur.

Origin: documented native function.

Native routes: `GetCountofSectionPropertyValuesEx`.

```csharp
System.Console.WriteLine(property.GetCountofSectionPropertyValuesEx());
```

## 23. GetSectionPropertyType

```csharp
int GetSectionPropertyType(int varSecRefNo)
```

Reads one native section type code by section definition ID.

- `varSecRefNo`: Positive native section property reference ID, preserving the documented native parameter name.

**Returns:** The native integral section type code unchanged. Only positive type codes succeed; unfamiliar positive codes remain observable. Native negative errors such as -6025 throw.

When to use: Use for direct metadata discovery without parsing a buffer. Does not identify an analytical member, resolve a catalog profile, convert codes into a guessed shape, create definitions or save. Boolean/floating/text responses are rejected. GetSectionBy separately requires a reviewed native version/type layout.

Origin: documented native function.

Native routes: `GetSectionPropertyType`.

```csharp
System.Console.WriteLine(property.GetSectionPropertyType(37));
```

## 24. GetSectionPropertyCountry

```csharp
int GetSectionPropertyCountry(int varSecRefNo)
```

Reads one native country code by section definition ID.

- `varSecRefNo`: Positive native section property reference ID, preserving the documented native parameter name.

**Returns:** The native integral country code unchanged. Nonnegative country codes succeed; zero/non-enum codes are retained for non-table/future definitions. Native negative errors such as -6025 throw.

When to use: Use for direct metadata discovery without parsing a buffer. Does not identify an analytical member, resolve a catalog profile, convert codes into a guessed shape, create definitions or save. Boolean/floating/text responses are rejected. GetSectionBy separately requires a reviewed native version/type layout.

Origin: documented native function.

Native routes: `GetSectionPropertyCountry`.

```csharp
System.Console.WriteLine(property.GetSectionPropertyCountry(37));
```

## 25. GetRecordForSection

```csharp
int GetRecordForSection(int Country, string SectionName)
```

Looks up a native section database record for an exact table designation.

- `Country`: Positive native table country code; integer parameter name/type preserves the documented native input and allows future country codes.
- `SectionName`: Exact nonblank native table designation without NUL; case and whitespace pass unchanged.

**Returns:** The native nonnegative database record, including zero, or documented -1 when lookup fails. A record is not a model section property ID.

When to use: Use for table record identity, not to create or assign a model property. Preserves -1 without throwing or substituting a guessed record. Other negative codes and malformed nonintegral responses throw. Database/build record conventions are native-controlled; no assumed one-based adjustment, private catalog fallback, model mutation or save.

Origin: documented native function.

Native routes: `GetRecordForSection`.

```csharp
System.Console.WriteLine(property.GetRecordForSection((int)Country.Indian, "ISMB300"));
```

## Remaining source exceptions

Eight source signatures conflict with existing qualified return types (material name/beta/material/release reads). They remain available through the existing qualified APIs with their established parameter names. Five source overloads are obsolete(error=true): inactive-member listing (two), element-offset projections (two), and extended converted material creation. Those are not claimed as working ports. Native Property API coverage remains a separate inventory; completion of the active main surface does not establish native/engineering qualification.

## Verification checkpoint

Passed a Release build with zero warnings/errors and **4,156 offline tests**, including **225 new section cases**. Isolated installed-package verification compiles and runs **692 selected XML examples**, including all **250 Property interface/adapter examples**, through a fresh PackageReference-only consumer and cache. Managed stand-ins perform every example; live native mutation and engineering qualification remain pending.

Selected 47 reviewed section layouts per acquired STAAD 24/25/26 profile, reading native version metadata after wrapper initialization and rechecking it before returning packets. Corrected the source assignment query filtering an unfilled array. Preserved native identity/field labels, explicit missing-area results, operation-specific statuses and partial-write behavior. Added small section selection/data models without importing private catalog/UI services; reused the repository UnitsConverter for explicit modulus/weight-density conversion. Existing typed-return collisions (eight) and obsolete source overloads (five) remain documented exceptions.
