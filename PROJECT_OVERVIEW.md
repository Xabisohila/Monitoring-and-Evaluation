# Provincial M&E System — Project Overview

## What Is This System?

The **Provincial Monitoring & Evaluation (M&E) System** is a government web application used by a South African provincial government to manage its performance planning, monitoring, and reporting cycle. It tracks whether provincial departments and entities are meeting their planned targets each quarter, and routes performance reports through a structured approval workflow before they are accepted.

Built with **ASP.NET WebForms (C#)**, backed by **SQL Server** (`MnE_Copy_2`), and deployed as an IIS web application.

---

## Technology Stack

| Layer | Technology |
|---|---|
| Frontend | ASP.NET WebForms (.aspx), Bootstrap 3, jQuery, Ajax Control Toolkit |
| Backend | C# (.NET 4.x), code-behind pattern (.aspx.cs) |
| Data access | ADO.NET with SQL Server stored procedures, DAL classes in `App_Code/` |
| Auth | Session-based, login with Persal Number (SA government employee ID) |
| Notifications | TextMagic REST API (SMS) |
| File handling | Excel uploads via OleDb, document uploads (PDF/Word/Excel/images) |

---

## Core Domain Concepts

| Concept | Description |
|---|---|
| **PMTDP** | Provincial Medium Term Development Plan — the master government planning document uploaded by the Planning Unit |
| **POA** | Plan of Action — groups one or more Interventions under a Desired Outcome; the operational plan departments execute quarterly |
| **PDP Goal** | Top-level Provincial Development Plan goal (e.g. "Goal 1: Innovative and inclusive growing economy") |
| **Priority Focus** | Strategic priority sitting under a PDP Goal (e.g. "Economic Growth") → `c_Priority` |
| **Integration Programme** | A cross-cutting programme grouping Working Groups under a Priority (e.g. "Inclusive Economic Growth") → `c_IntegrationProgramme` |
| **Impact** | The desired societal impact statement for the Integration Programme (e.g. "Sustainable and inclusive economic growth") |
| **Desired Outcome** | A specific result the province wants to achieve, linked to a Priority and Integration Programme → `c_Outcome` |
| **Outcome Indicator** | A high-level metric measuring whether the Desired Outcome is being achieved (long-term, multi-year) → `c_Indicator` |
| **Intervention Indicator** | A specific measurable activity indicator sitting under an Intervention (what departments report quarterly) → sub-level of `c_Indicator` |
| **Indicator Type** | Framework reference tag on an indicator: Africa Agenda 2063, NDP 2030, PDP 2030, SDG 8 |
| **Implementing Institution** | The department or entity responsible for executing an Intervention (lead + supporting/dependency institutions) → `c_Institution` |
| **Intervention** | A planned action/project linked to a POA, with a lead institution, budget, spatial reference, and start/end year → `Intervention` |
| **Cluster** | Top-level organisational grouping of departments/sectors (e.g. ESIEID, SPCHD) |
| **Working Group (WG)** | Sub-group within a Cluster; has a Lead Institution and a Convener |
| **Annual Target** | The planned value for an Intervention Indicator for a full financial year (e.g. "2025/26 Target") → `c_AnnualTarget` |
| **Term Target** | The long-term planned value for an indicator by 2030 → stored on `c_Indicator.TermTargetValue` |
| **Baseline** | The starting reference value for an indicator (two baselines: PDP 2019/2020 and recent 2023/24) → `c_Indicator.BaselineValue` |
| **Quarterly Target** | The portion of the Annual Target planned for a specific quarter → `c_QuarterlyTarget` |
| **Quarterly Report** | A department's actual performance value submitted for one Intervention Indicator in a specific quarter → `c_QuarterlyReport` |
| **Term Budget** | The total multi-year budget allocated to an Intervention |
| **Annual Budget** | The budget allocated to an Intervention for the current financial year |
| **Spatial Referencing** | The geographic area (district/municipality) where an Intervention is delivered → `c_Municipality` |
| **System Period** | An admin-controlled open/close window that controls when quarterly reporting is allowed → `c_SystemPeriod` |
| **Evidence (POE)** | Proof of Evidence — supporting documents uploaded alongside a quarterly performance report |

---

## PMTDP & POA Data Structure

The PMTDP is a nested planning document. Each level in the document maps directly to a domain entity in the system:

```
PDP Goal
("Goal 1: Innovative and inclusive growing economy")
  │
  └─▶ Priority Focus  [c_Priority]
      ("Economic Growth")
        │
        └─▶ Integration Programme  [c_IntegrationProgramme]
            ("Inclusive Economic Growth")
              │
              └─▶ Impact
                  ("Sustainable and inclusive economic growth")
                    │
                    └─▶ Desired Outcome  [c_Outcome]
                        ("Promote sustained, inclusive and sustainable economic
                          growth, full and productive employment and decent work
                          for all")
                          │
                          └─▶ Outcome Indicator  [c_Indicator]
                              ("GDP growth rate")
                              ├─ Indicator Type: PDP 2030, NDP 2030, SDG 8
                              ├─ PDP Baseline 2019/2020: 1.5%
                              ├─ PDP Target 2030: 5.4%
                              │
                              ├─▶ Implementing Institution(s)  [c_Institution]
                              │   Lead: DEDEAT
                              │   Supporting: (dependency institutions)
                              │
                              └─▶ Intervention  [Intervention]
                                  ("Increased contribution of key sectors to
                                    provincial GDP and job creation")
                                    │
                                    ├─▶ Intervention Indicator  [c_Indicator sub-level]
                                    │   ("% increase in GDP contribution from key sectors")
                                    │   ├─ Baseline 2023/24: New indicator
                                    │   ├─ 2030 Term Target: 15%
                                    │   ├─ Term Budget: Operational budget
                                    │   ├─ 2025/26 Annual Target: N/A
                                    │   ├─ Annual Budget
                                    │   └─ Spatial Referencing: N/A
                                    │
                                    └─▶ POA  [POADAL]
                                        (groups this Intervention with others
                                         under the same Desired Outcome)
```

### Two Levels of Indicators

This is one of the most important distinctions in the system:

| | Outcome Indicator | Intervention Indicator |
|---|---|---|
| **Purpose** | Measures whether the Desired Outcome is being achieved | Measures the specific activity a department is doing |
| **Timeframe** | Long-term (baseline 2019/20, target 2030) | Annual + quarterly |
| **Who tracks it** | Province-wide (OTP Monitoring) | Implementing department/entity |
| **Example** | "GDP growth rate: 1.5% → 5.4% by 2030" | "% increase in GDP contribution from key sectors" |
| **What gets reported** | Not submitted quarterly by departments | This is what departments submit quarterly reports against |

### Multiple Institutions per Intervention

One Intervention can have:
- A **Lead Implementing Institution** (primary responsible department, e.g. DEDEAT)
- One or more **Supporting/Dependency Institutions** (e.g. DHS, ELIDZ, ECDC)

This is managed through `c_ProgrammeInstitution` and the Supporting Institutions setup pages.

### Target Timescales

Every Intervention Indicator carries targets at multiple timescales simultaneously:

| Target | Description |
|---|---|
| PDP Baseline 2019/2020 | Historical reference point from the original PDP document |
| Baseline 2023/24 | More recent starting point (often "New indicator") |
| 2030 Term Target | The long-term benchmark the province aims to hit |
| 2025/26 Annual Target | The current financial year's step toward the 2030 target |
| Quarterly Target (Q1–Q4) | The quarterly slice of the annual target, reported against each quarter |

### New / Provisional Indicators

Some indicators in the PMTDP are marked **"New indicator"** with targets **"To be confirmed later"** (highlighted in yellow in the source document). The system must handle these gracefully — they exist in the data but have null or placeholder target values until confirmed by the Planning Unit.

---

## Reporting Workflow

Every quarterly report goes through a multi-stage approval chain before it is accepted:

```
Department/Entity
      |
      |  Submits quarterly performance report + evidence
      v
  [Submitted]
      |
      |  WG Coordinator / OTP Monitoring performs QA check
      v
  [Awaiting QA]  →  QA Fail → back to Department/Entity
      |
      |  QA passes
      v
  [Awaiting Approval]
      |
      |  HOD / Entity CEO approves or rejects
      v
  [Approved / Rejected]
      |
      |  (Optional) Signoff stage
      v
  [Signed Off]
```

Workflow history is recorded in `c_WorkflowHistory` (stage = `"QA"`, `"Approval"`, `"Signoff"`).

---

## User Roles

Users log in with their **Persal Number** (government employee ID). Each user is assigned a `UserType` which determines their role and which menus are visible to them.

---

### Role 32 — Administrator

The system superuser. Has full visibility of all menus and all functionality.

**Access:**
- Everything the Planning Unit can do
- Everything the Department/Entity Capturer can do
- Everything the HOD/CEO can do
- Access to the Updates (god-mode) menu with direct links to all pages
- Can see and manage all clusters, working groups, priorities, outcomes, indicators, targets, reports, and approvals across the entire system

**Key pages:** All pages in the system

---

### Role 37 — Planning Unit

The administrative/setup role. Configures the structural foundation before any department can plan or report.

**Responsibilities:**
- Set up **Clusters**, **Working Groups**, and **Implementation Institutions** (the org structure)
- Load **PMTDP targets** from Excel — uploaded by one user, approved by a second (two-person control)
- Load **POA targets** from Excel
- Define **Strategic Priorities**, **Outcomes**, and **Indicators**
- Set **Annual Targets** per indicator
- Manage **System Periods** — open and close reporting windows per quarter
- Manage **Integration Programmes** and **Supporting Institutions** per Working Group

**Menu items visible:**
- Upload → PMTDP Upload, POA Upload, Approval List, Approval View
- Inbox (stub, not yet implemented)

**Key pages:**
- [pu_ClusterSetup.aspx](pu_ClusterSetup.aspx) — create/edit/delete Clusters
- [pu_WGSetup.aspx](pu_WGSetup.aspx) — create/edit/delete Working Groups (linked to Cluster + Lead Institution)
- [pu_ImplementationInstitutionsSetup.aspx](pu_ImplementationInstitutionsSetup.aspx) — register Implementation Institutions
- [i_PMTDPUpload.aspx](i_PMTDPUpload.aspx) — upload PMTDP Excel file, preview, submit for approval
- [j_PMTDPApprovalList.aspx](j_PMTDPApprovalList.aspx) — list of pending PMTDP uploads awaiting approval
- [j_PMTDPApprovalView.aspx](j_PMTDPApprovalView.aspx) — approve or reject an upload (uploader cannot approve their own)
- [i_POAUpload.aspx](i_POAUpload.aspx) — upload POA targets
- [i_PriorityAdmin.aspx](i_PriorityAdmin.aspx) — manage Strategic Priorities
- [i_OutcomeAdmin.aspx](i_OutcomeAdmin.aspx) — manage Outcomes
- [i_IndicatorAdmin.aspx](i_IndicatorAdmin.aspx) — manage Indicators (linked to Outcomes)
- [i_AnnualTargets.aspx](i_AnnualTargets.aspx) — set Annual Targets per Indicator per financial year
- [i_SystemPeriodsAdmin.aspx](i_SystemPeriodsAdmin.aspx) — open/close reporting periods per quarter
- [i_ProgrammeAdmin.aspx](i_ProgrammeAdmin.aspx) — manage Integration Programmes (WG-level)
- [i_ProgrammeInstitutions.aspx](i_ProgrammeInstitutions.aspx) — manage Supporting Institutions per WG
- [new_PU_Selection.aspx](new_PU_Selection.aspx) — filter/select POAs by Cluster, WG, Priority, Financial Year, Quarter

---

### Role 38 — Department / Entity

Represents a provincial department or government entity. This is the primary data capturer — they submit quarterly performance reports for their indicators.

**Responsibilities:**
- Upload or manually capture **Quarterly Targets** for their assigned indicators
- Submit **Quarterly Performance Reports** (actual achieved values + evidence documents)
- Track and manage their own submissions
- Submit **Change Requests** if a target or report needs to be amended

**Menu items visible:**
- My Tasks
- Reports → Add New, View Submissions
- Manage → Upload Q Targets, Update Existing Q Targets

**Key pages:**
- [i_ReportSubmit.aspx](i_ReportSubmit.aspx) — submit a quarterly performance report for an indicator, attach evidence (POE)
- [i_MySubmissions.aspx](i_MySubmissions.aspx) — view all submissions made by the current user
- [i_MyTasks.aspx](i_MyTasks.aspx) — task list showing reports awaiting action (resubmission after QA fail, etc.)
- [i_QuarterlyTargets.aspx](i_QuarterlyTargets.aspx) — view and update existing quarterly targets
- [ii_DeptQuarterlyTargetsUpload.aspx](ii_DeptQuarterlyTargetsUpload.aspx) — bulk-upload quarterly targets from Excel
- [ii_ChangeRequests.aspx](ii_ChangeRequests.aspx) — submit and track change requests

---

### Role 39 — WG Coordinator

The Working Group Coordinator is responsible for quality-assuring reports submitted by departments within their Working Group before forwarding them for HOD approval.

**Responsibilities:**
- Review submitted reports in the **QA Inbox**
- Pass or fail each report
- Assign indicator owners
- Assign indicators to Working Groups

**Key pages:**
- [i_QAInbox.aspx](i_QAInbox.aspx) — inbox of reports awaiting QA, with pass/fail action
- [i_MyTasks.aspx](i_MyTasks.aspx) — task list filtered to QA actions
- [ii_AssignIndicatorOwners.aspx](ii_AssignIndicatorOwners.aspx) — assign ownership of indicators to specific users
- [ii_AssignIndicatorWorkingGroup.aspx](ii_AssignIndicatorWorkingGroup.aspx) — assign indicators to Working Groups
- [i_ReportDetails.aspx](i_ReportDetails.aspx) — view full details of a specific report

---

### Role 40 — WG Convener

The Working Group Convener chairs the Working Group. Their role in the system overlaps with the Coordinator but may have additional oversight of the WG's overall performance.

> Menu not yet fully configured in code — functionality is being developed.

**Key pages (expected):**
- [i_MyTasks.aspx](i_MyTasks.aspx) — task list
- [i_MonitoringDashboard.aspx](i_MonitoringDashboard.aspx) — monitor WG-level performance

---

### Role 41 — OTP Monitoring

"OTP" = Office of the Premier. This role monitors overall provincial performance across all clusters and departments, from the Premier's office perspective.

**Responsibilities:**
- Cross-cluster, cross-department performance monitoring
- Access to monitoring dashboards and review reports

**Key pages:**
- [i_MonitoringDashboard.aspx](i_MonitoringDashboard.aspx) — dashboard with FY, Quarter, WG, and Department filters showing performance across the province
- [ReviewMonitoring.aspx](ReviewMonitoring.aspx) — detailed review of monitoring data
- [ReviewMonitoringWithGraphsAndChart.aspx](ReviewMonitoringWithGraphsAndChart.aspx) — charts and visualisations of monitoring data
- [i_NonCompliance.aspx](i_NonCompliance.aspx) — report on departments not submitting reports

---

### Role 42 — HOD (Head of Department)

The Head of Department is the final approver for their department's quarterly performance reports after QA has been completed.

**Responsibilities:**
- Review reports that have passed QA and are awaiting approval
- Approve or reject each report with a comment
- Cannot approve reports submitted by themselves (self-approval prevention)

**Menu items visible:**
- HOD/Entity CEO → Approval Inbox, Approval (HOD/CEO)

**Key pages:**
- [i_Approval.aspx](i_Approval.aspx) — list of reports awaiting HOD approval; open and approve/reject each
- [ii_ApprovalInbox.aspx](ii_ApprovalInbox.aspx) — inbox view of pending approvals
- [i_Signoff.aspx](i_Signoff.aspx) — final signoff on approved reports (optional final stage)

---

### Role 43 — Report Viewer

A read-only role for stakeholders who need to see reports and dashboards but do not capture or approve data.

> Menu not yet fully configured in code — read-only access is the expected intent.

**Key pages (expected):**
- [i_MonitoringDashboard.aspx](i_MonitoringDashboard.aspx)
- [ReviewMonitoring.aspx](ReviewMonitoring.aspx)
- [i_QuarterSummary.aspx](i_QuarterSummary.aspx) — summary of quarterly performance (publicly accessible — no auth required per Web.config)
- [ReportSummary.aspx](ReportSummary.aspx)

---

## Role Summary Table

| Role ID | Role Name | Primary Function |
|---|---|---|
| 32 | Administrator | Full system access and superuser management |
| 37 | Planning Unit | System setup: org structure, PMTDP/POA uploads, indicators, targets, system periods |
| 38 | Department / Entity | Submit quarterly performance reports and manage targets |
| 39 | WG Coordinator | QA-check submitted reports within the Working Group |
| 40 | WG Convener | Chair and oversee the Working Group's performance |
| 41 | OTP Monitoring | Provincial-level cross-cluster monitoring (Office of the Premier) |
| 42 | HOD | Approve or reject QA-passed reports for their department |
| 43 | Report Viewer | Read-only access to reports and dashboards |

---

## Project Structure

```
MnE2_Planning/
├── App_Code/               # All DAL and business model classes
│   ├── c_*.cs              # Core entity models (Indicator, Outcome, Priority, etc.)
│   ├── c_*DAL.cs           # Data access layer per entity (stored procedure calls)
│   ├── cc_*.cs             # Composite/view models (ReportViewModel, WorkflowItem, etc.)
│   ├── cls_*.cs            # Service/repository classes (Planning Unit, Monitoring, etc.)
│   ├── clsUser.cs          # Legacy user class
│   └── d_PMTDP*.cs         # PMTDP upload DAL classes
├── App_Data/               # Local data files
├── Content/                # Additional content assets
├── Dashboard/              # Dashboard-related pages
├── Evidence/               # Evidence file handling
├── ManE/                   # Additional M&E module pages
├── Targets/                # Target-related pages
├── Workflow/               # Workflow-related pages
├── Other/                  # Miscellaneous pages
├── DesignMode/             # Design/prototype pages
├── css/                    # Bootstrap and custom stylesheets
├── js/                     # JavaScript files
├── img/                    # Images and logos
├── fonts/                  # Font Awesome fonts
├── uploads/                # Uploaded files (evidence, PMTDP Excel files)
├── backups/ backups2/      # Backup copies of pages
├── bin/                    # Compiled assemblies
├── akshara.master          # Main site master page (navigation + layout)
├── Login.master            # Login page master
├── Web.config              # Application config (connection string, auth)
├── MnE.sln                 # Visual Studio solution file
│
│── Key Pages (prefixed by role/function):
│   i_*                     # Core system pages (indicators, approval, QA, reports)
│   ii_*                    # Department-level pages (dept uploads, change requests)
│   j_*                     # PMTDP approval pages
│   pu_*                    # Planning Unit setup pages
│   new_PU_*                # Newer Planning Unit redesign pages
│   page*                   # Planning/monitoring detail pages (newer design)
│   designed*               # Redesigned versions of legacy pages
│   Planning*.aspx          # Legacy planning pages (ST, GA, ED variants)
│   Monitoring.aspx         # Legacy monitoring page
└── i_PMTDPUpload.aspx      # PMTDP Excel upload (currently open in IDE)
```

---

## Key Architectural Notes

- **No MVC** — pure WebForms; each page owns its own data binding in `Page_Load`.
- **DAL pattern** — every entity has a `*DAL.cs` class; all DB calls go via ADO.NET stored procedures.
- **Session-based auth** — `Session["UserID"]`, `Session["RoleID"]`, `Session["UserTypeD"]`, `Session["Department"]` are set at login and checked on every page.
- **Menu visibility** is controlled in `akshara.master.cs` `UserTypeSettings()` method by showing/hiding `runat="server"` `<li>` elements based on `Session["UserTypeD"]`.
- **Dual code paths** — many features exist in both a legacy version (`PlanningST.aspx`, `Monitoring.aspx`) and a redesigned version (`designedPlanningOverview.aspx`, `i_MonitoringDashboard.aspx`). The redesigned pages are the active development direction.
- **Financial year convention** — South African financial year (April–March). FY "2025" = April 2025 – March 2026. Quarters: Q1 = Apr–Jun, Q2 = Jul–Sep, Q3 = Oct–Dec, Q4 = Jan–Mar.
- **Persal Number** — the South African government HR system employee identifier, used as the username for login.
