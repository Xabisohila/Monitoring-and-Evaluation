<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WorkflowStatusPanel.ascx.cs" Inherits="WorkflowStatusPanel" %>

<style>
/* ── Floating workflow panel ───────────────────────────────── */
#wfFloat {
    position: fixed;
    bottom: 28px;
    right: 24px;
    z-index: 9990;
    font-family: "Segoe UI", Roboto, Arial, sans-serif;
    font-size: 13px;
}

/* Pill toggle button */
#wfToggleBtn {
    display: flex;
    align-items: center;
    gap: 9px;
    background: #1a2b4a;
    color: #fff;
    border: none;
    border-radius: 30px;
    padding: 10px 18px 10px 14px;
    cursor: pointer;
    box-shadow: 0 4px 22px rgba(0,0,0,.30);
    transition: background .15s;
    white-space: nowrap;
}
#wfToggleBtn:hover { background: #0b5ed7; }

#wfToggleBtn .wf-dot {
    width: 10px; height: 10px;
    border-radius: 50%;
    flex-shrink: 0;
}
#wfToggleBtn .wf-dot.completed { background: #22c55e; }
#wfToggleBtn .wf-dot.active    { background: #facc15; animation: wfPulse 1.4s infinite; }
#wfToggleBtn .wf-dot.locked    { background: #94a3b8; }
#wfToggleBtn .wf-dot.all-done  { background: #22c55e; }

@keyframes wfPulse {
    0%,100% { opacity:1; transform:scale(1); }
    50%      { opacity:.5; transform:scale(1.25); }
}

/* Expanded panel */
#wfPanel {
    display: none;
    position: absolute;
    bottom: 52px;
    right: 0;
    width: 340px;
    background: #fff;
    border-radius: 16px;
    box-shadow: 0 10px 50px rgba(0,0,0,.22);
    overflow: hidden;
}

.wf-panel-head {
    padding: 14px 18px;
    background: #1a2b4a;
    color: #fff;
    display: flex;
    justify-content: space-between;
    align-items: center;
}
.wf-panel-head strong { font-size: 14px; letter-spacing: .3px; }
.wf-panel-close {
    background: none; border: none; color: #94a3b8;
    font-size: 20px; cursor: pointer; line-height: 1; padding: 0;
}
.wf-panel-close:hover { color: #fff; }

/* Active / next-action card */
.wf-next-card {
    margin: 14px 14px 0;
    padding: 14px 16px;
    border-radius: 10px;
    border-left: 4px solid #0b5ed7;
    background: #f0f6ff;
}
.wf-next-card.all-done {
    border-left-color: #22c55e;
    background: #f0fdf4;
}
.wf-next-card .wf-nc-eyebrow {
    font-size: 10px;
    font-weight: 700;
    text-transform: uppercase;
    letter-spacing: .6px;
    color: #0b5ed7;
    margin-bottom: 4px;
}
.wf-next-card.all-done .wf-nc-eyebrow { color: #16a34a; }
.wf-next-card .wf-nc-title  { font-size: 15px; font-weight: 700; color: #1a2b4a; margin-bottom: 4px; }
.wf-next-card .wf-nc-detail { font-size: 12px; color: #475569; margin-bottom: 4px; }
.wf-next-card .wf-nc-role   { font-size: 11px; color: #64748b; }
.wf-next-card .wf-nc-role span { font-weight: 600; color: #1a2b4a; }
.wf-nc-go {
    display: inline-block;
    margin-top: 10px;
    padding: 7px 14px;
    background: #0b5ed7;
    color: #fff;
    border-radius: 7px;
    font-size: 12px;
    font-weight: 600;
    text-decoration: none;
}
.wf-nc-go:hover { background: #094db0; color:#fff; text-decoration:none; }

/* Blocked card */
.wf-blocked-card {
    margin: 8px 14px 0;
    padding: 10px 14px;
    border-radius: 10px;
    background: #fff7ed;
    border-left: 4px solid #f59e0b;
    font-size: 12px;
    color: #78350f;
}
.wf-blocked-card .wf-bc-label {
    font-size: 10px; font-weight: 700; text-transform: uppercase;
    letter-spacing: .5px; color: #d97706; margin-bottom: 3px;
}

/* Stage list */
.wf-stage-list {
    padding: 12px 14px 14px;
}
.wf-stage-row {
    display: flex;
    align-items: flex-start;
    gap: 10px;
    padding: 7px 0;
    border-bottom: 1px solid #f1f5f9;
}
.wf-stage-row:last-child { border-bottom: none; }

.wf-sr-icon {
    width: 24px; height: 24px;
    border-radius: 50%;
    display: flex; align-items: center; justify-content: center;
    font-size: 11px; font-weight: 700;
    flex-shrink: 0; margin-top: 1px;
}
.wf-sr-icon.completed { background: #dcfce7; color: #16a34a; }
.wf-sr-icon.active    { background: #dbeafe; color: #1d4ed8; }
.wf-sr-icon.locked    { background: #f1f5f9; color: #94a3b8; }

.wf-sr-body { flex: 1; min-width: 0; }
.wf-sr-name {
    font-size: 12px; font-weight: 600;
    color: #1e293b;
}
.wf-sr-name.locked { color: #94a3b8; }
.wf-sr-count {
    font-size: 11px; color: #64748b; margin-top: 1px;
}
.wf-sr-action {
    font-size: 11px; font-weight: 600;
    color: #0b5ed7; text-decoration: none;
}
.wf-sr-action:hover { text-decoration: underline; }
</style>

<div id="wfFloat">
    <!-- Expandable panel (rendered above toggle button) -->
    <div id="wfPanel">
        <div class="wf-panel-head">
            <strong>&#9654; Planning Workflow</strong>
            <button class="wf-panel-close" onclick="wfClose()" title="Close">&#215;</button>
        </div>

        <asp:Literal runat="server" ID="litNextCard" />
        <asp:Literal runat="server" ID="litBlockedCard" />

        <div class="wf-stage-list">
            <asp:Literal runat="server" ID="litStages" />
        </div>
    </div>

    <!-- Toggle pill -->
    <button id="wfToggleBtn" onclick="wfToggle()" title="Planning workflow status">
        <span id="wfStatusDot" class="wf-dot active"></span>
        <span id="wfStatusLabel">Workflow</span>
    </button>
</div>

<script>
(function () {
    // Restore open state from localStorage
    if (localStorage.getItem('wfOpen') === '1') wfOpen();
})();

function wfToggle() {
    var p = document.getElementById('wfPanel');
    if (p.style.display === 'none' || p.style.display === '') { wfOpen(); }
    else { wfClose(); }
}
function wfOpen() {
    document.getElementById('wfPanel').style.display = 'block';
    localStorage.setItem('wfOpen', '1');
}
function wfClose() {
    document.getElementById('wfPanel').style.display = 'none';
    localStorage.setItem('wfOpen', '0');
}
</script>
