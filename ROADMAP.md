# StaadPro.Interop — Strategic Product Roadmap

This document outlines the architectural roadmap and upcoming milestone releases for the **StaadPro.Interop** open-source ecosystem.

Our mission is to provide the structural engineering and computational design community with a modern, high-performance, strongly-typed .NET suite covering the entire Bentley STAAD.Pro automation lifecycle.

---

## Milestone Overview

```mermaid
gantt
    title StaadPro.Interop Strategic Product Roadmap (2026 - 2027)
    dateFormat YYYY-MM-DD
    axisFormat %b %Y
    section Core Foundation
    Geometry Foundation (v1.0)              :done, g1, 2026-08-01, 2026-09-15
    section Modeling & Properties
    Loads & Load Combinations (v1.1)        :active, l1, 2026-09-15, 2026-11-01
    Properties & Material Models (v1.2)     :p1, 2026-11-01, 2026-12-15
    Supports & Soil Boundaries (v1.3)       :s1, 2026-12-15, 2027-01-31
    section Solvers & Verification
    Results & Analysis Engine (v1.4)        :r1, 2027-02-01, 2027-03-15
    Design & Code Optimization (v1.5)       :d1, 2027-03-15, 2027-04-30
    Viewport & Command Automation (v1.6)    :v1, 2027-04-15, 2027-05-15
```

---

## Phase 1: StaadPro.Interop Geometry (v1.0 — Current Release)

*Status: **Released & Active (Q3 2026)***

- **OpenStaadWrapper Architecture**:
  - `OpenStaadWrapper` and `OpenStaadWrapperProvider` supporting ROT inspection, running process attachment, and automated startup.
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

## Phase 2: StaadPro.Interop.Loads & Combinations (v1.1 — Active Milestone)

*Status: **Target: Q4 2026***

Expands the suite with strongly-typed load generation, combination building, and load group management (`IOSLoad` and `OSLoadAdapter`):

- **Load Case Management**:
  - Primary load case definitions (`CreateNewPrimaryLoad`, `CreateNewPrimaryLoadEx`, `CreateNewPrimaryLoadEx2`).
  - Reference load definitions (`CreateNewReferenceLoad`).
  - Load case metadata querying (`GetLoadCaseTitle`).
  - Active load case switching (`SetLoadCaseActive`).
  - Primary and reference load case clearing (`ClearPrimaryLoadCase`, `ClearReferenceLoadCase`).
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

## Phase 3: StaadPro.Interop.Properties & Materials (v1.2)

*Status: **Target: Q4 2026***

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

## Phase 4: StaadPro.Interop.Supports & Boundaries (v1.3)

*Status: **Target: Q1 2027***

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

## Phase 5: StaadPro.Interop.Results & Analysis Engine (v1.4)

*Status: **Target: Q1 2027***

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

## Phase 6: StaadPro.Interop.Design & Code Optimization (v1.5)

*Status: **Target: Q2 2027***

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

## Phase 7: StaadPro.Interop.Visualization & Scripting Automation (v1.6)

*Status: **Target: Q2 2027***

Viewport graphical automation, diagnostic visualizers, and direct command stream execution (`IOSView` and `IOSCommands`):

- **Viewport Camera & Diagnostic Rendering**:
  - Viewport control: Zoom Extents, Pan, Isometric, Plan, Elevation views.
  - Diagnostic color shading by Property ID, Material Type, Group Name, or Utilization Ratio heatmaps.
  - Programmatic activation of BMD, SFD, AFD diagrams and high-resolution screenshot captures for PDF calculation reports.
- **Direct Command Stream Scripting**:
  - Low-level raw command dispatch and transactional batch queuing directly to the active model buffer.

---

## How to Contribute to the Roadmap

We encourage structural engineers, software developers, and research institutions to participate in shaping the future of StaadPro.Interop:

1. **Vote on Features**: Check out our [GitHub Discussions](https://github.com/structuraldeveloper/StaadPro.Interop/discussions) and upvote proposals.
2. **Submit RFCs**: Propose new design code support or engineering workflows via [Feature Requests](.github/ISSUE_TEMPLATE/feature_request.md).
---

*StaadPro.Interop is an independent open-source project and is not officially affiliated with or endorsed by Bentley Systems, Incorporated.*
