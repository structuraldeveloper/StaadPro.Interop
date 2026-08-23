# StaadPro.Interop — Strategic Product Roadmap

This document outlines the architectural roadmap and upcoming milestone releases for the **StaadPro.Interop** open-source ecosystem. 

Our mission is to provide the structural engineering and computational design community with a modern, high-performance, strongly-typed .NET suite covering the entire Bentley STAAD.Pro automation lifecycle.

---

## Milestone Overview

```mermaid
gantt
    title StaadPro.Interop Roadmap & Module Expansion
    dateFormat  YYYY-Q#
    section Core Geometry
    StaadPro.Interop (Geometry) (v1.0)      :done, g1, 2026-Q1, 2026-Q3
    section Engineering Modeling
    StaadPro.Interop.Loads (v1.1)            :active, l1, 2026-Q3, 2026-Q4
    StaadPro.Interop.Properties (v1.2)       :p1, 2026-Q4, 2027-Q1
    StaadPro.Interop.Supports (v1.3)         :s1, 2027-Q1, 2027-Q2
    section Analysis & Post-Processing
    StaadPro.Interop.Results (v1.4)          :r1, 2027-Q2, 2027-Q3
    StaadPro.Interop.Design (v1.5)           :d1, 2027-Q3, 2027-Q4
    StaadPro.Interop.Visualization (v1.6)    :v1, 2027-Q4, 2028-Q1
    section Interop & Cloud
    StaadPro.Interop.IdeaStatiCa Bridge (v2.0):i1, 2028-Q1, 2028-Q2
    StaadPro.Interop.Core (.NET 8 Parser) (v2.5):c1, 2028-Q2, 2028-Q4
```

---

## Phase 1: StaadPro.Interop Geometry (v1.0 — Current Release)

*Status: **Released & Active***

- **Fluent Geometry Generation**: Node, Beam, Plate, and Physical Member creation with fluent chaining.
- **Parametric Surface & Mesh Generation**:
  - Annular surfaces with inner/outer boundary generation and polygonal openings.
  - Solid circular surfaces with radial mesh control points.
  - Linear and point mesh density controllers (`AddDensityPointToSurface`, `AddDensityLineToSurface`).
- **High-Performance Batch & Async Queries**:
  - Multi-threaded entity extraction (`GetAllEntities<T>`, `GetEntityGroups<T>`).
  - Thread-safe Task-based async wrapper methods.
  - Incident element topology querying (`GetPlatesConnectedAtNode`, `GetBeamsConnectedAtNode`).
- **Analytical Vector & Math Extensions**:
  - 3D coordinate transforms, plan angle calculations, radial distance helpers, torque distribution algorithms.
- **CI/CD & Offline Testing**:
  - Full GitHub Actions CI matrix on `windows-latest`.
  - Comprehensive NUnit test suite decoupled from live COM dependencies.

---

## Phase 2: StaadPro.Interop.Loads & Combinations (v1.1 — Planned)

*Status: **Planned for Q4 2026***

Expands the suite with strongly-typed load generation, combination building, and load group management (`IOSLoad` and `OSLoadAdapter`):

- **Load Case Management**:
  - Primary load case definitions (Dead, Live, Wind, Seismic, Snow, Thermal).
  - Load combinations (Linear superposition, SRSS, ABS combinations).
  - Repeat load definitions for second-order P-Delta analysis.
- **Nodal Loading**:
  - Concentrated force ($F_x, F_y, F_z$) and moment ($M_x, M_y, M_z$) assignments.
  - Inclined nodal loads with coordinate system transforms.
- **Beam Loading**:
  - Uniform distributed forces and moments (global and local axes).
  - Concentrated point loads and concentrated moments at fractional distances.
  - Linear trapezoidal distributed loads and hydrostatic linear variations.
- **Area & Floor Pressure Loading**:
  - Automatic one-way and two-way tributary area load distribution algorithms.
  - Elevation Y-range floor load definitions with column exclusion rules.
- **Plate & Surface Loading**:
  - Uniform pressure over full plate surface (local Z, global projections).
  - Hydrostatic triangular pressure distributions across plate groups.
  - Thermal gradient and temperature variation loading.
- **Environmental & Dynamic Load Generators**:
  - Seismic definition parameters (ASCE 7, IS 1893, Eurocode 8, NBC).
  - Wind load definitions (ASCE 7 wind intensity vs height profiles, exposure factors).
- **Parallel & Async Load Operations**:
  - Multi-threaded load assignments across large element selections.

---

## Phase 3: StaadPro.Interop.Properties & Materials (v1.2 — Planned)

*Status: **Planned for Q1 2027***

Provides structural section catalogs, material models, and element specification management (`IOSProperty` and `OSPropertyAdapter`):

- **Standard Section Database Integration**:
  - Strongly-typed section library mapping for AISC, British (BS), European (Euroroll), Indian (IS), Australian (AS), and Japanese (JIS) steel profiles.
  - Wide Flange (W), Channels (C/MC), Angles (L/2L), Hollow Structural Sections (HSS/Pipe/Tube), Tees (WT).
- **Prismatic & Parametric Profiles**:
  - Rectangular, Circular, Tee, Trapezoidal, and Custom cross-sections.
  - User-Defined Tables (UPT) and General section definitions ($A_x, A_y, A_z, I_x, I_y, I_z, C_w, J$).
  - Tapered beam definitions with variable start/end dimensions.
- **Plate & Shell Properties**:
  - Uniform thickness assignment and multi-layered composite shell definitions.
  - Surface thickness modifiers and orthotropic membrane definitions.
- **Constitutive Material Models**:
  - Isotropic and Orthotropic materials: Modulus of Elasticity ($E$), Poisson's Ratio ($
u$), Density ($ho$), Thermal Expansion Coefficient ($lpha$), Damping Ratio ($eta$).
  - Standard material presets: Structural Steel (A992, A36, S355, E250), Concrete (C25/30, C30/37, 4000 psi), Aluminum, Timber.
- **Member Specifications & Releases**:
  - Beam end releases (Start/End $F_x, F_y, F_z, M_x, M_y, M_z$ with partial moment release and elastic spring constants).
  - Truss / Axial-only member specifications.
  - Tension-only and Compression-only non-linear member behavior.
  - Section property modifiers (cracked concrete reduction factors for $I_{yy}, I_{zz}, A_x$).
- **Local Coordinate System Orientation**:
  - Beta angle assignments and reference node orientation alignments.

---

## Phase 4: StaadPro.Interop.Supports & Boundaries (v1.3 — Planned)

*Status: **Planned for Q2 2027***

Comprehensive boundary condition, support constraint, and foundation spring management (`IOSSupport` and `OSSupportAdapter`):

- **Standard Support Conditions**:
  - Pinned, Fixed, Roller (X, Y, Z release), and Free.
- **6-DOF Elastic Foundation & Spring Supports**:
  - Translational springs ($K_{fx}, K_{fy}, K_{fz}$) and Rotational springs ($K_{mx}, K_{my}, K_{mz}$).
  - Compression-only and Tension-only foundation spring behavior.
- **Subgrade Modulus & Elastic Mat Foundations**:
  - Automatic spring stiffness calculation based on soil subgrade reaction modulus ($k_s$) and tributary influence areas.
- **Multilinear & Non-Linear Spring Curves**:
  - Force-displacement ($P-\delta$) piecewise linear spring curves for soil-structure interaction.
- **Inclined & Skewed Support Coordinates**:
  - Cylindrical and rotated Cartesian support reference frames.

---

## Phase 5: StaadPro.Interop.Results & Analysis Engine (v1.4 — Planned)

*Status: **Planned for Q3 2027***

High-throughput post-processing, solver invocation, and analytical result extraction (`IOSOutput` and `OSOutputAdapter`):

- **Solver Invocation & Health Monitoring**:
  - Programmatic analysis execution (Linear Static, P-Delta, Modal Frequency, Response Spectrum, Buckling).
  - Solver convergence verification and error log parser.
- **Nodal Results**:
  - Global displacements and rotations across all load cases/combinations.
  - Support reaction forces and moments with equilibrium checks ($\sum F, \sum M$).
  - Modal eigenvectors, natural frequencies, participation factors, and modal mass percentages.
- **Beam Internal Forces & Deflections**:
  - Member end forces ($F_x, F_y, F_z, M_x, M_y, M_z$).
  - Intermediate section internal force extraction ($N$ sections along beam length).
  - Maximum/minimum force envelopes across load combinations.
  - Beam local deflections and relative span deflection ratios ($L/\Delta$).
- **Plate & Shell Stress Resultants**:
  - Center and corner node stresses: Principal stresses ($\sigma_1, \sigma_2$), Von Mises equivalent stress ($\sigma_{vm}$), Maximum shear ($	au_{max}$).
  - Membrane forces ($N_x, N_y, N_{xy}$), Bending moments ($M_x, M_y, M_{xy}$), Out-of-plane transverse shear ($Q_x, Q_y$).
  - Top and bottom surface stress layer extraction.

---

## Phase 6: StaadPro.Interop.Design & Code Checkers (v1.5 — Planned)

*Status: **Planned for Q4 2027***

Design code automation, parameter assignment, and code compliance evaluation (`IOSDesign` and `OSDesignAdapter`):

- **Design Code Parameter Management**:
  - Steel design parameters: Yield strength (`FYLD`), Ultimate tensile strength (`FU`), Unbraced length ratios (`UNT`, `UNB`), Effective length factors (`KY`, `KZ`), Moment amplification coefficients (`CMX`, `CMZ`).
  - Supported design codes: AISC 360 (LRFD/ASD), IS 800 (LSM/WSM), Eurocode 3 (EN 1993-1-1), AS 4100, BS 5950.
- **Code Check Execution & Optimization**:
  - Automated `CHECK CODE` and `SELECT MEMBER` batch execution.
  - Extraction of governing load cases, critical design clauses, and governing failure modes.
  - Maximum utilization ratio ($UR$) extraction across all structural members.
  - Automated member profile resizing and weight optimization loops.

---

## Phase 7: StaadPro.Interop.Visualization & Viewport (v1.6 — Planned)

*Status: **Planned for Q1 2028***

Graphical viewport manipulation, diagnostic coloring, and automated report rendering (`IOSView` and `OSViewAdapter`):

- **Viewport Camera Control**:
  - Zoom Extents, Pan, Isometric, Plan (X-Z), Elevation (X-Y, Y-Z), and custom viewpoint angles.
- **Diagnostic Color Shading**:
  - Color shading by Property ID, Material Type, Group Name, or Utilization Ratio heatmaps.
- **Diagram Overlays & Screen Captures**:
  - Programmatic activation of Bending Moment Diagrams (BMD), Shear Force Diagrams (SFD), Axial Force Diagrams (AFD), and Deformed Shapes.
  - High-resolution lossless screenshot captures for automated PDF calculation report generation.

---

## Phase 8: StaadPro.Interop.Commands & Direct Scripting (v1.7 — Planned)

*Status: **Planned for Q1 2028***

Direct access to low-level STAAD command streams (`IOSCommands` and `OSCommandsAdapter`):

- **Raw Command Stream Execution**:
  - Dispatch raw OpenSTAAD command strings directly to the active model buffer.
- **Batch Command Queuing**:
  - Transaction-style command batching with rollback support on error.

---

## Phase 9: StaadPro.Interop.IdeaStatiCa Bridge (v2.0 — Planned)

*Status: **Planned for Q2 2028***

Direct, automated structural steel connection design bridge to **IDEA StatiCa Connection** (`IOpenStaadIdeaStatiCaAdapter`):

- **Analytical Node & Joint Topology Extraction**:
  - Identify analytical connection nodes and all incident connected members.
  - Determine member analytical roles: Continuous Bearing Member, Attached Beam, Web Diagonal, Flange Diagonal.
  - Extract physical member geometric cut-planes, insertion points, and eccentricities.
- **Cross-Section & Material Mapping**:
  - Automatic translation of STAAD cross-sections and materials to IDEA StatiCa Open Model (IOM).
- **Internal Force Envelope Transfer**:
  - Direct transfer of all 6-DOF internal forces ($N, V_y, V_z, M_x, M_y, M_z$) at joint node cuts across all active load combinations.
  - Automated generation of `.ideacon` project files for instant FEA connection verification.

---

## Phase 10: StaadPro.Interop.Core — Cross-Platform .NET 8 Parser (v2.5 — Planned)

*Status: **Planned for Q4 2028***

A 100% standalone, cross-platform .NET 8 / .NET Standard parser that reads and writes STAAD.Pro input files (`.std`) without requiring a STAAD.Pro license or Windows COM:

- **Cross-Platform Compatibility**: Runs seamlessly on **Linux**, **macOS**, **Windows**, and **WebAssembly (WASM)**.
- **Full `.std` Grammar Lexer/Parser**: High-performance streaming parser for geometry, property blocks, load cases, combinations, and analysis commands.
- **BIM & Open Formats Interoperability**:
  - Direct export from `.std` models to **IFC4 (Structural Analysis View)**.
  - **Speckle** cloud stream connector for web-based 3D structural collaboration.
  - **glTF / Three.js** exporter for interactive browser-based 3D rendering.

---

## How to Contribute to the Roadmap

We encourage structural engineers, software developers, and research institutions to participate in shaping the future of StaadPro.Interop:

1. **Vote on Features**: Check out our [GitHub Discussions](https://github.com/OpenSTAAD/StaadPro.Interop/discussions) and upvote proposals.
2. **Submit RFCs**: Propose new design code support or BIM workflows via [Feature Requests](.github/ISSUE_TEMPLATE/feature_request.md).
3. **Contribute Code**: Check out good first issues in our repository and review [CONTRIBUTING.md](CONTRIBUTING.md).

---

*StaadPro.Interop is an independent open-source project and is not officially affiliated with or endorsed by Bentley Systems, Incorporated.*
