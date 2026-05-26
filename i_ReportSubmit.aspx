<%@ Page Title="" Language="C#" MasterPageFile="~/akshara.master" AutoEventWireup="true" CodeFile="i_ReportSubmit.aspx.cs" Inherits="i_ReportSubmit" MaintainScrollPositionOnPostback="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <style type="text/css">
    :root{
      --bg:#f7f8fb; --surface:#fff; --border:#d9dee5; --text:#1f2937; --muted:#64748b;
      --primary:#0b5ed7; --primary-dark:#094db0; --danger:#dc3545;
    }
    /*body{background:var(--bg);font-family:"Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif;color:var(--text);}*/
    h2{margin:0 0 12px;font-size:26px;font-weight:700;color:var(--text);letter-spacing:.2px;}
    label{display:inline-block;font-size:13px;color:var(--muted);margin:8px 0 4px;}
    select, input[type="text"], textarea{
      display:inline-block;width:100%;min-width:220px;padding:9px 12px;border:1px solid var(--border);
      border-radius:8px;background:var(--surface);color:var(--text);
      transition:border-color .15s ease, box-shadow .15s ease;
    }
    input[type="text"]:focus,textarea:focus,select:focus{
      outline:none;border-color:var(--primary);box-shadow:0 0 0 3px rgba(11,94,215,.15);
    }
    input[type="date"]{
      display:inline-block;padding:9px 12px;border:1px solid var(--border);
      border-radius:8px;background:var(--surface);color:var(--text);
      transition:border-color .15s ease, box-shadow .15s ease;
      font-family:"Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif;
      font-size:14px;
    }
    input[type="date"]:focus{
      outline:none;border-color:var(--primary);box-shadow:0 0 0 3px rgba(11,94,215,.15);
    }
    input[type="date"]::-webkit-calendar-picker-indicator{
      cursor:pointer;opacity:0.6;
      transition:opacity .15s ease;
    }
    input[type="date"]::-webkit-calendar-picker-indicator:hover{
      opacity:1;
    }
    input[type="file"]{display:inline-block;padding:8px 0;color:var(--text);}
    .actions{margin-top:8px;}
    input[type="submit"],button{
      display:inline-block;margin-top:4px;padding:9px 14px;font-size:14px;font-weight:600;border-radius:8px;
      border:1px solid var(--primary);background:var(--primary);color:#fff;cursor:pointer;
      transition:background-color .15s ease, border-color .15s ease, box-shadow .15s ease;
    }
    input[type="submit"]:hover,button:hover{background:var(--primary-dark);border-color:var(--primary-dark);}
    input[type="submit"]:focus-visible,button:focus-visible{
      outline:none;box-shadow:0 0 0 3px rgba(11,94,215,.25);
    }
    hr{border:none;border-top:1px solid var(--border);margin:16px 0;}

    /* Status / validation */
    .state-ok{color:#107C10;font-weight:600;}
    .state-under{color:#C50F1F;font-weight:600;}
    .state-over{color:#005FB8;font-weight:600;}
    .error-border{border:3px solid #C50F1F !important;}
    .over-border{border:3px solid #005FB8 !important;}
    .hidden{display:none !important;}
    .help{font-size:12px;color:#666;}

    /* Grid */
    .form-grid{display:grid;grid-template-columns:220px 1fr;grid-gap:10px 14px;}
    .full-row{grid-column:1 / -1;}
    .actions{grid-column:2 / 3;}

    /* File list */
    .file-list{list-style:none;margin:8px 0 0;padding:0;}
    .file-item{
      display:flex;align-items:center;gap:10px;padding:8px 10px;border:1px solid var(--border);
      border-radius:8px;background:#fff;margin-bottom:8px;
    }
    .file-name{flex:1 1 auto;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;}
    .file-meta{color:#6b7280;font-size:12px;}
    .file-remove{
      background:#fff;color:#b91c1c;border:1px solid #b91c1c;border-radius:6px;padding:4px 8px;font-weight:600;
    }
    .file-remove:hover{background:#b91c1c;color:#fff;}
    .thumb{width:36px;height:36px;border-radius:6px;object-fit:cover;border:1px solid var(--border);}
    .badge{display:inline-block;background:#eef2ff;color:#3730a3;border-radius:999px;padding:2px 8px;font-size:12px;margin-left:8px;}
    .note{font-size:12px;color:#475569;}
    .callout{background:#F8FAFC;border-left:5px solid #0b5ed7;padding:10px;border-radius:6px;}
    .flex{display:flex;gap:10px;align-items:center;flex-wrap:wrap;}
    @media(max-width:640px){.form-grid{grid-template-columns:1fr;}.actions{grid-column:1 / -1;}}

    /* History cards */
.hist-card{
  background:#fff; border:1px solid var(--border); border-radius:8px; padding:10px 12px; margin:10px 0;
}
.hist-title{ font-weight:700; margin-bottom:6px; color:#111827; }
.badge{ display:inline-block; background:#eef2ff; color:#3730a3; border-radius:999px; padding:2px 8px; font-size:12px; margin-left:8px; }
  </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <br /><br /><br /><br />
  <div class="container">
    <div class="section-title text-center">
      <br />
      <div>
        <h2 class="background double animated wow fadeInUp color1" style="color:#000" data-wow-delay="0.2s">
          <span><strong>Quarterly Reporting</strong></span>
        </h2>
      </div>
    </div>
    <br /><br />

    <div class="form-grid">

      <!-- Filters -->
        <asp:Label runat="server" Text="Financial Year:" AssociatedControlID="ddlFY" />
<asp:DropDownList runat="server" ID="ddlFY" Width="250px" />

      <asp:Label runat="server" Text="Indicator:" AssociatedControlID="ddlIndicator" />
      <asp:DropDownList runat="server" ID="ddlIndicator" Width="400px" />

      

      <asp:Label runat="server" Text="Quarter:" AssociatedControlID="ddlQuarter" />
      <asp:DropDownList runat="server" ID="ddlQuarter" Width="250px">
        <asp:ListItem Text="Quarter 1 (Apr - Jun)" Value="1" />
        <asp:ListItem Text="Quarter 2 (Jul - Sept)" Value="2" />
        <asp:ListItem Text="Quarter 3 (Oct - Dec)" Value="3" />
        <asp:ListItem Text="Quarter 4 (Jan - Mar)" Value="4" />
      </asp:DropDownList>

      <span></span>
      <asp:Button runat="server" ID="btnLoad" Text="Load Planned Target" OnClick="btnLoad_Click" Width="250px" />

      <!-- Planned -->
      <span class="full-row">
        <asp:Label runat="server" ID="lblPlanned" Text="Planned Expenditure: -" Font-Bold="true" Font-Size="Medium"/>
      </span>

        <!-- Previous report(s) & outcome (auto-filled on Load Planned Target) -->
<asp:Panel ID="pnlHistory" runat="server" CssClass="full-row hidden" Visible="false" >
  <div class="callout">
    <div class="flex" style="justify-content:space-between">
      <div><strong>Previous Report(s)</strong></div>
      <div class="note">If available for this indicator/period</div>
    </div>
    <div style="margin-top:8px">
      <asp:Literal ID="litPrevReports" runat="server" />
    </div>
  </div>
</asp:Panel>


      <!-- Live deviation / remaining -->
      <div class="full-row callout">
        <div class="flex">
          

            <div id="boxRemaining"><strong>Remaining:</strong> <span id="spRemaining">0</span></div>
            <div id="spDeviationNote"></div>
<div ><strong>Deviation %:</strong> <span id="spDeviationPct">0.00%</span></div>
          <div ><strong>Deviation Amount:</strong> <span id="spDeviationAmt">0</span></div>
          
        </div>
        <div class="note" id="spDetailsNote" style="margin-top:6px;"></div>
      </div>

      <div class="full-row"><hr /></div>

      <!-- Actual -->
      <asp:Label runat="server" Text="Actual Achievement:" AssociatedControlID="txtActual" />
      <asp:TextBox runat="server" ID="txtActual" />

      <!-- UNDER group (shows whenever Actual < Planned) -->
      <div id="groupUnder" class="full-row hidden">
        <asp:Label runat="server" Text="Reason for Deviation:" AssociatedControlID="txtDeviation" />
        <asp:TextBox runat="server" ID="txtDeviation" TextMode="MultiLine" Rows="3" CssClass="full-row" />
        <span class="full-row" style="text-align:right;"><small id="spDeviationCount" class="help">0 / 1000</small></span>

          <br />

        <asp:Label runat="server" Text="Remedial Actions:" AssociatedControlID="txtRemedial" />
        <asp:TextBox runat="server" ID="txtRemedial" TextMode="MultiLine" Rows="3" CssClass="full-row" />

          <br /><br />

        <asp:Label runat="server" Text="Remedial Due Date:" AssociatedControlID="dtDue" />
        <asp:TextBox runat="server" ID="dtDue" TextMode="Date" Width="200px"/>
      </div>

      <!-- OVER group (shows whenever Actual > Planned) -->
      <div id="groupOver" class="full-row hidden">
        <asp:Label runat="server" Text="Over‑Achievement Reason:" AssociatedControlID="txtOver" />
        <asp:TextBox runat="server" ID="txtOver" TextMode="MultiLine" Rows="3" CssClass="full-row" />
      </div>

      <!-- Shared -->
      <asp:Label runat="server" Text="Spatial Reference:" AssociatedControlID="txtSpatial" />
      <asp:TextBox runat="server" ID="txtSpatial" />

      <div class="full-row"><hr /></div>

      <!-- REQUIRED PRIMARY UPLOAD -->
      <div class="full-row">
        <label for="fuPrimaryReport"><strong>Portfolio of Evidence (POE) <span class="badge">Required</span></strong></label>
        <input id="fuPrimaryReport" name="fuPrimaryReport" type="file" accept="application/pdf" />
        <div id="primaryPreview" class="file-list"></div>
        <div class="help">Upload the signed/official report PDF for this submission.</div>
      </div>

        <div class="full-row"><hr /></div>

      <!-- MULTI-FILE POE -->
      <asp:Label runat="server" Text="Additional Portfolio of Evidence (optional):" />
      <div>
        <input id="fuPOE" name="fuPOE" type="file" multiple="multiple"
               accept=".pdf,.doc,.docx,.xls,.xlsx,.png,.jpg,.jpeg" />
        <div class="help">
          Drag & drop or click to add. Allowed: PDF, DOC/DOCX, XLS/XLSX, PNG, JPG. Max 20 MB per file.
          <span id="spPoeCount" class="badge">0 files</span>
        </div>
        <ul id="poeList" class="file-list"></ul>
      </div>

        <div class="full-row"><hr /></div>

        <span></span>
      <%--<div class="actions">
        <asp:Button runat="server" ID="btnSubmit" Text="Submit Report" OnClick="btnSubmit_Click"  style="align-content:end"/>
      </div>--%>

      <!-- Hidden fields -->
      <span class="full-row">
        <asp:HiddenField runat="server" ID="hfQuarterlyTargetID" />
        <asp:HiddenField runat="server" ID="hfPlannedValue" />
        <asp:HiddenField runat="server" ID="hfTolerance" />
      </span>

      <!-- Server messages -->
      <span class="full-row">
        <asp:Literal runat="server" ID="litStatus" />
      </span>
    </div>

      <div class="actions">
          <asp:Button runat="server" ID="btnSubmit" Text="Submit Report" OnClick="btnSubmit_Click" Style="align-content: end" />
      </div>

  </div>
  <br /><br />

  <!-- Client-side logic -->
  <script type="text/javascript">
      (function () {
          // ----- Server control IDs -----
          var hfPlannedId = '<%= hfPlannedValue.ClientID %>';
    var hfToleranceId = '<%= hfTolerance.ClientID %>';
    var txtActualId = '<%= txtActual.ClientID %>';
    var txtDeviationId = '<%= txtDeviation.ClientID %>';
    var txtRemedialId = '<%= txtRemedial.ClientID %>';
  var txtOverId     = '<%= txtOver.ClientID %>';
  var dtDueId       = '<%= dtDue.ClientID %>';
  var btnSubmitId   = '<%= btnSubmit.ClientID %>';
  var hfTargetId    = '<%= hfQuarterlyTargetID.ClientID %>';

          // ----- Settings -----
          var deviationMaxChars = 1000;
          var requireRemedialOnOver = true;

          // File constraints
          var allowedExt = [".pdf", ".doc", ".docx", ".xls", ".xlsx", ".png", ".jpg", ".jpeg"];
          var maxFileSize = 20 * 1024 * 1024; // 20 MB

          // ----- Helpers -----
          function notify(type, msg) {
              // type: 'info' | 'error' | 'success' | 'warn'
              var bg = { info: '#EDF2FF', error: '#FFF5F5', success: '#ECFDF3', warn: '#FFFAEB' }[type] || '#EDF2FF';
              var border = { info: '#4C6EF5', error: '#C50F1F', success: '#12B76A', warn: '#F79009' }[type] || '#4C6EF5';
              var el = document.createElement('div');
              el.setAttribute('role', 'status');
              el.style.cssText = 'position:fixed; z-index:9999; right:16px; top:16px; max-width:420px;'
                  + 'background:' + bg + '; border-left:5px solid ' + border + '; padding:12px 14px; '
                  + 'box-shadow:0 4px 18px rgba(0,0,0,.08); color:#1f2937; border-radius:6px; font:14px/1.4 Segoe UI,Arial,sans-serif;';
              el.textContent = msg;
              document.body.appendChild(el);
              setTimeout(function () { try { document.body.removeChild(el); } catch (e) { } }, 3600);
          }

          function $(id) { return document.getElementById(id); }
          function parseNum(v) { if (v == null) return NaN; v = (v + '').replace(',', '.').trim(); if (v === '') return NaN; return Number(v); }
          function setAriaInvalid(el, inv) { if (el) el.setAttribute('aria-invalid', inv ? 'true' : 'false'); }
          function setRequired(el, req) { if (!el) return; if (req) el.setAttribute('data-required', 'true'); else el.removeAttribute('data-required'); }
          function isEmpty(el) { return !el || (el.value || '').replace(/\s+/g, '') === ''; }
          function removeClass(el, cls) { if (!el) return; el.className = (el.className || '').replace(new RegExp('\\b' + cls + '\\b', 'g'), '').replace(/\s{2,}/g, ' ').trim(); }
          function addClass(el, cls) { if (!el) return; if ((el.className || '').indexOf(cls) === -1) el.className = ((el.className || '') + ' ' + cls).trim(); }
          function fmt(n) { return (isNaN(n) ? 0 : n).toLocaleString(undefined, { maximumFractionDigits: 2 }); }

          function resetFieldVisuals() {
              var dev = $(txtDeviationId), rem = $(txtRemedialId), over = $(txtOverId), due = $(dtDueId);
              removeClass(dev, 'error-border'); removeClass(rem, 'error-border'); removeClass(due, 'error-border');
              removeClass(over, 'over-border');
              [dev, rem, over, due].forEach(function (el) { if (!el) return; setAriaInvalid(el, false); setRequired(el, false); });
              var note = $('spDeviationNote');
              if (note) { removeClass(note, 'state-ok'); removeClass(note, 'state-under'); removeClass(note, 'state-over'); note.innerHTML = ''; }
          }

          // ----- Visibility by sign (ignores tolerance) -----
          function setGroupVisibilityBySign(planned, actual) {
              var gUnder = $('groupUnder'), gOver = $('groupOver');
              if (gUnder) gUnder.classList.add('hidden');
              if (gOver) gOver.classList.add('hidden');

              if (isNaN(planned) || isNaN(actual)) return;

              if (actual < planned) { if (gUnder) gUnder.classList.remove('hidden'); }
              else if (actual > planned) { if (gOver) gOver.classList.remove('hidden'); }
              // equal → both hidden
          }

          // ----- Deviation evaluation -----
          function evaluateDeviation() {
              var plannedVal = parseNum($(hfPlannedId).value);
              var tolPct = parseNum($(hfToleranceId).value); if (isNaN(tolPct)) tolPct = 5;
              var actualVal = parseNum($(txtActualId).value);

              var pctEl = $('spDeviationPct'), amtEl = $('spDeviationAmt'), noteEl = $('spDeviationNote');
              var remBox = $('boxRemaining'), remEl = $('spRemaining'), details = $('spDetailsNote');

              var deviationBox = $(txtDeviationId), remedialBox = $(txtRemedialId), overBox = $(txtOverId), dueBox = $(dtDueId);

              resetFieldVisuals();

              if (pctEl) pctEl.innerHTML = '0.00%';
              if (amtEl) amtEl.innerHTML = '0';
              if (remEl) remEl.innerHTML = '0';
              if (details) details.innerHTML = '';

              var hasNumbers = !isNaN(plannedVal) && !isNaN(actualVal);
              if (!hasNumbers) { setGroupVisibilityBySign(NaN, NaN); return; }

              // Deviation %
              var deviationPct;
              if (plannedVal === 0) deviationPct = (actualVal === 0) ? 0 : 100; else deviationPct = ((actualVal - plannedVal) / plannedVal) * 100;
              deviationPct = Math.round(deviationPct * 100) / 100;

              var deviationAmt = actualVal - plannedVal;
              if (pctEl) pctEl.innerHTML = deviationPct.toFixed(2) + '%';
              if (amtEl) amtEl.innerHTML = (deviationAmt >= 0 ? '+' : '') + fmt(deviationAmt);

              // Remaining/Exceeded (always show)
              if (remBox) remBox.classList.remove('hidden');
              if (actualVal < plannedVal) {
                  if (remEl) remEl.innerHTML = fmt(plannedVal - actualVal);
                  if (details) details.innerHTML = 'You are below the planned target. Please explain the deviation and provide remedial actions.';
              } else if (actualVal > plannedVal) {
                  if (remEl) remEl.innerHTML = fmt(actualVal - plannedVal);
                  if (details) details.innerHTML = 'You exceeded the planned target. Please provide an over‑achievement reason.';
              } else {
                  if (remEl) remEl.innerHTML = '0';
                  if (details) details.innerHTML = 'Actual equals planned.';
              }

              // Status label uses tolerance (informational only)
              var withinTol = Math.abs(deviationPct) <= tolPct;
              var underSign = actualVal < plannedVal;
              var overSign = actualVal > plannedVal;

              if (noteEl) {
                  if (withinTol) { addClass(noteEl, 'state-ok'); noteEl.innerHTML = 'On Track'; }
                  else if (underSign) { addClass(noteEl, 'state-under'); noteEl.innerHTML = 'Under‑achievement'; }
                  else if (overSign) { addClass(noteEl, 'state-over'); noteEl.innerHTML = 'Over‑achievement'; }
              }

              // VISIBILITY: ignore tolerance; purely by sign
              setGroupVisibilityBySign(plannedVal, actualVal);

              // Field requirements (by sign)
              if (actualVal < plannedVal) {
                  setRequired(deviationBox, true); setAriaInvalid(deviationBox, isEmpty(deviationBox));
                  setRequired(remedialBox, true); setAriaInvalid(remedialBox, isEmpty(remedialBox));
                  setRequired(dueBox, true); setAriaInvalid(dueBox, isEmpty(dueBox));

                  if (isEmpty(deviationBox)) addClass(deviationBox, 'error-border');
                  if (isEmpty(remedialBox)) addClass(remedialBox, 'error-border');
                  if (isEmpty(dueBox)) addClass(dueBox, 'error-border');
              } else if (actualVal > plannedVal) {
                  setRequired(overBox, true); setAriaInvalid(overBox, isEmpty(overBox));
                  if (isEmpty(overBox)) addClass(overBox, 'over-border');

                  if (requireRemedialOnOver) {
                      setRequired(remedialBox, true); setAriaInvalid(remedialBox, isEmpty(remedialBox));
                      if (isEmpty(remedialBox)) addClass(remedialBox, 'over-border');
                  }
              }
          }

          // ----- Submit gate -----
          function beforeSubmit() {
              // Ensure target is loaded
              if (!$(hfTargetId).value) { notify('error', 'Please click "Load Planned Target" and select a valid period before submitting.'); return false; }

              // Actual required & numeric
              var actualVal = parseNum($(txtActualId).value);
              if (isNaN(actualVal)) { notify('error', 'Please enter a valid Actual Expenditure.'); try { $(txtActualId).focus(); } catch (e) { } return false; }

              // Primary PDF required
              var primary = $('fuPrimaryReport').files;
              if (!primary || primary.length === 0) { notify('error', 'Please upload the Primary Report (PDF).'); return false; }
              var pf = primary[0];
              if (!/\.pdf$/i.test(pf.name)) { notify('error', 'Primary Report must be a PDF file.'); return false; }
              if (pf.size > (20 * 1024 * 1024)) { notify('error', 'Primary Report exceeds 20 MB.'); return false; }

              // Validate POE selections (client side)
              var poeFiles = $('fuPOE').files;
              for (var i = 0; i < poeFiles.length; i++) {
                  var f = poeFiles[i], ext = (f.name.match(/\.[^.]+$/) || [''])[0]?.toLowerCase() || '';
                  if (allowedExt.indexOf(ext) === -1) { notify('error', 'Unsupported file type: ' + f.name); return false; }
                  if (f.size > maxFileSize) { notify('error', 'File too large (>20MB): ' + f.name); return false; }
              }

              // Check required fields per sign
              evaluateDeviation();

              var gUnderVisible = !($('groupUnder')?.classList.contains('hidden'));
              var gOverVisible = !($('groupOver')?.classList.contains('hidden'));

              var fields = [$(txtDeviationId), $(txtRemedialId), $(txtOverId), $(dtDueId)];
              for (var j = 0; j < fields.length; j++) {
                  var el = fields[j];
                  if (el && el.getAttribute('data-required') === 'true' && isEmpty(el)) {
                      setAriaInvalid(el, true);
                      try { el.focus(); } catch (ex) { }
                      if (gUnderVisible) {
                          if (el.id === $(txtDeviationId).id) notify('error', 'Please provide a Deviation Reason.');
                          else if (el.id === $(txtRemedialId).id) notify('error', 'Please provide Remedial Actions.');
                          else if (el.id === $(dtDueId).id) notify('error', 'Please provide a valid Remedial Due Date.');
                      } else if (gOverVisible) {
                          if (el.id === $(txtOverId).id) notify('error', 'Please provide an Over‑achievement Reason.');
                          else if (el.id === $(txtRemedialId).id) notify('error', 'Please provide Remedial Actions for the over‑achievement.');
                      } else {
                          notify('error', 'Please complete required fields.');
                      }
                      return false;
                  }
              }

              notify('success', 'Submitting your report...');
              return true;
          }

          // ----- Character counter for Deviation Reason -----
          function onDeviationInput() {
              var el = $(txtDeviationId), cnt = $('spDeviationCount');
              if (!el || !cnt) return;
              var len = (el.value || '').length;
              if (len > deviationMaxChars) { el.value = el.value.substring(0, deviationMaxChars); len = deviationMaxChars; }
              cnt.innerHTML = len + ' / ' + deviationMaxChars;
          }

          // ========================================================================
          //                          FILE PICKERS (POE)
          // ========================================================================
          // Robust per-file removal using DataTransfer + stable snapshot

          var dtPOE = (window.DataTransfer) ? new DataTransfer() : null;
          var poeInput = document.getElementById('fuPOE');
          var poeList = document.getElementById('poeList');
          var poeCount = document.getElementById('spPoeCount');

          function toArray(fileList) {
              var arr = [];
              for (var i = 0; i < fileList.length; i++) arr.push(fileList[i]);
              return arr;
          }

          function fileKey(f) {
              return [f.name, f.size, f.lastModified].join('|');
          }

          function renderPoeList() {
              if (!poeList) return;
              poeList.innerHTML = '';

              var files = toArray(poeInput.files);
              for (var i = 0; i < files.length; i++) {
                  (function (f) {
                      var li = document.createElement('li');
                      li.className = 'file-item';
                      li.setAttribute('data-key', fileKey(f));

                      var isImg = /^image\//i.test(f.type);
                      var thumb = document.createElement('img');
                      thumb.className = 'thumb';
                      thumb.alt = '';
                      thumb.src = isImg
                          ? URL.createObjectURL(f)
                          : 'data:image/svg+xml,<svg xmlns="http://www.w3.org/2000/svg" width="36" height="36"></svg>';

                      var name = document.createElement('div');
                      name.className = 'file-name';
                      name.title = f.name;
                      name.textContent = f.name;

                      var meta = document.createElement('div');
                      meta.className = 'file-meta';
                      meta.textContent = (f.type || '') + ' • ' + (Math.round(f.size / 1024)).toLocaleString() + ' KB';

                      var btn = document.createElement('button');
                      btn.type = 'button';
                      btn.className = 'file-remove';
                      btn.textContent = 'Remove';

                      // <<< Inserted improvement here
                      btn.addEventListener('click', function onClick(e) {
                          btn.disabled = true;                 // avoid double-removal / race conditions
                          removeOnePoeFileByKey(fileKey(f));   // remove just this one
                      });

                      li.appendChild(thumb);
                      li.appendChild(name);
                      li.appendChild(meta);
                      li.appendChild(btn);
                      poeList.appendChild(li);
                  })(files[i]);
              }

              if (poeCount) poeCount.textContent = files.length + (files.length === 1 ? ' file' : ' files');
          }

          function removeOnePoeFileByKey(keyToRemove) {
              if (!dtPOE) {
                  notify('warn', 'Your browser does not support removing individual files here. Please reselect the files.');
                  return;
              }
              var current = toArray(poeInput.files);
              var nextDT = new DataTransfer();
              for (var i = 0; i < current.length; i++) {
                  var f = current[i];
                  if (fileKey(f) !== keyToRemove) nextDT.items.add(f);
              }
              poeInput.files = nextDT.files;
              dtPOE = nextDT; // keep reference in sync
              renderPoeList();
          }

          function validateAndAppendSelection(fileList) {
              var newlySelected = toArray(fileList);
              for (var i = 0; i < newlySelected.length; i++) {
                  var f = newlySelected[i];
                  var ext = (f.name.match(/\.[^.]+$/) || [''])[0]?.toLowerCase() || '';
                  if (allowedExt.indexOf(ext) === -1) {
                      notify('error', 'Unsupported file type: ' + f.name);
                      continue;
                  }
                  if (f.size > maxFileSize) {
                      notify('error', 'File too large (>20MB): ' + f.name);
                      continue;
                  }
                  dtPOE.items.add(f);
              }
          }

          if (poeInput) {
              poeInput.addEventListener('change', function () {
                  if (!dtPOE) {
                      // Old browsers—no per-item remove capability
                      renderPoeList();
                      return;
                  }
                  // Add newly selected into our dtPOE, then assign back
                  validateAndAppendSelection(this.files);
                  poeInput.files = dtPOE.files;
                  renderPoeList();
              });
          }

          // ========================================================================
          //                    PRIMARY REPORT (single, PDF) PREVIEW
          // ========================================================================

          if ($('fuPrimaryReport')) {
              $('fuPrimaryReport').addEventListener('change', function () {
                  var box = $('primaryPreview'); if (!box) return;
                  box.innerHTML = '';
                  var fs = this.files;
                  if (!fs || fs.length === 0) return;
                  var f = fs[0];

                  var li = document.createElement('div'); li.className = 'file-item';
                  var icon = document.createElement('img'); icon.className = 'thumb'; icon.alt = ''; icon.src = 'data:image/svg+xml,<svg xmlns="http://www.w3.org/2000/svg" width="36" height="36"></svg>';
                  var name = document.createElement('div'); name.className = 'file-name'; name.title = f.name; name.textContent = f.name;
                  var meta = document.createElement('div'); meta.className = 'file-meta'; meta.textContent = 'PDF • ' + (Math.round(f.size / 1024)).toLocaleString() + ' KB';
                  var btn = document.createElement('button'); btn.type = 'button'; btn.className = 'file-remove'; btn.textContent = 'Remove';
                  btn.onclick = function () { $('fuPrimaryReport').value = ''; box.innerHTML = ''; };

                  li.appendChild(icon); li.appendChild(name); li.appendChild(meta); li.appendChild(btn);
                  box.appendChild(li);
              });
          }

          // ----- Wire events -----
          function wire() {
              var actualEl = $(txtActualId), deviationEl = $(txtDeviationId), remedialEl = $(txtRemedialId),
                  overEl = $(txtOverId), dueEl = $(dtDueId), btnSubmit = $(btnSubmitId);

              if (actualEl) { actualEl.oninput = evaluateDeviation; actualEl.onblur = evaluateDeviation; }
              if (deviationEl) { deviationEl.oninput = function () { onDeviationInput(); evaluateDeviation(); }; }
              if (remedialEl) { remedialEl.oninput = evaluateDeviation; }
              if (overEl) { overEl.oninput = evaluateDeviation; }
              if (dueEl) { dueEl.onchange = evaluateDeviation; }
              if (btnSubmit) { btnSubmit.onclick = beforeSubmit; }

              // Initial state
              $('groupUnder')?.classList.add('hidden');
              $('groupOver')?.classList.add('hidden');

              onDeviationInput();
              evaluateDeviation();
              renderPoeList();
          }

          if (document.readyState === 'complete' || document.readyState === 'interactive') setTimeout(wire, 0);
          else document.addEventListener('DOMContentLoaded', wire);
      })();
  </script>
</asp:Content>