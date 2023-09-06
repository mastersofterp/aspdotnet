<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentBranchPrefRound2.aspx.cs" Inherits="ACADEMIC_StudentBranchPrefRound2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updLists"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updLists" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">BRANCH COUNSELING</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" TabIndex="1" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True"
                                            ValidationGroup="submit">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlAdmBatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" TabIndex="2" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Entrance Exam Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlEntrance" TabIndex="3" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlEntrance_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvEntrance" runat="server" ControlToValidate="ddlEntrance" 
                                            Display="None" ErrorMessage="Please Select Entrance Exam Name." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="TRRounds" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Select Round</label>
                                        </div>
                                        <asp:DropDownList ID="ddlRound" TabIndex="4" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlRound_SelectedIndexChanged" >
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>                                            
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvRound" runat="server" ControlToValidate="ddlRound"
                                            Display="None" ErrorMessage="Please Select Round." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Allotment Category</label>
                                        </div>
                                        <asp:RadioButtonList ID="rdCatAllot" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdCatAllot_SelectedIndexChanged">
                                            <asp:ListItem Value="1" Text="Categorywise"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="General"></asp:ListItem>
                                            </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="submit" OnClick="btnShow_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnAllot" runat="server" Text="Allot" ValidationGroup="submit" Enabled="false" OnClick="btnAllot_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnLock" runat="server" OnClick="btnLock_Click" Text="Lock" ValidationGroup="submit" CssClass="btn btn-primary" /> 
                                <asp:Button runat="server" ID="btnRCTRound" Text="Reset Round" OnClick="btnRCTRound_Click" ValidationGroup="submit" ToolTip="Cancel the allotment of selected round" CssClass="btn btn-primary" />
                                <asp:Button runat="server" ID="btnCloseRound" Text="Close Round" OnClick="btnCloseRound_Click" ValidationGroup="submit" ToolTip="Complete the admission of selected round" CssClass="btn btn-primary" />
                                <asp:Button runat="server" ID="btnpdf" Text="PDF" OnClick="btnpdf_Click" ValidationGroup="submit" ToolTip="Locked the selected entry" CssClass="btn btn-info" />
                                <asp:Button runat="server" ID="btnexport" Text="Report in Excel" OnClick="btnexport_Click" ValidationGroup="submit" ToolTip="Allotment summary" CssClass="btn btn-info" />
                                <asp:Button runat="server" ID="btnReport" Text="Admission Status Report" ValidationGroup="submit" OnClick="btnReport_Click" ToolTip="Get Admission allotment and available statusin report " CssClass="btn btn-info"  />&nbsp;
                                <asp:Button runat="server" ID="btnCancel" Text="Cancel" ValidationGroup="s" OnClick="btnCancel_Click" ToolTip="Cancel all selected Entry" CssClass="btn btn-warning" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ValidationGroup="submit" ShowSummary="False" />

                                <asp:TextBox ID="txtTotStud" runat="server" CssClass="watermarked" Enabled="false" Visible="false"
                                        Style="text-align: center"/>
                                <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                            </div>

                            <div class="col-12">
                                <asp:ListView ID="lvSeat" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Admission Status</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Degree
                                                    </th>
                                                    <th>Branch
                                                    </th>
                                                    <th>Tot Intake
                                                    </th>
                                                    <th>Tot Alloted
                                                    </th>
                                                    <th>Tot Admited
                                                    </th>
                                                    <th>Gen Admited
                                                    </th>
                                                    <th>Sc Admited
                                                    </th>
                                                    <th>St Admited
                                                    </th>
                                                    <th>OBC Admited
                                                    </th>
                                                    <th>Tot Remaining
                                                    </th>
                                                    <th>GEN Allot
                                                    </th>
                                                    <th>SC Allot
                                                    </th>
                                                    <th>ST Allot
                                                    </th>
                                                    <th>OBC Allot
                                                    </th>
                                                    <%--<th style="text-align: center; width: 10px; padding-right: 10px;">SVDET Allot
                                                    </th>--%>
                                                    <th>GEN Remaining
                                                    </th>
                                                    <th>SC Remaining
                                                    </th>
                                                    <th>ST Remaining
                                                    </th>
                                                    <th>OBC Remaining
                                                    </th>
                                                    <%--<th style="text-align: center; width: 10px; padding-right: 10px;">SVDET Remaining
                                                    </th>--%>
                                                    <th>GEN Wait
                                                    </th>
                                                    <th>SC Wait
                                                    </th>
                                                    <th>ST Wait
                                                    </th>
                                                    <th>OBC Wait
                                                    </th>
                                                    <%-- <th style="text-align: center; width: 10px; padding-right: 10px;">SVDET Wait
                                                    </th>--%>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td>
                                                <%# Eval("DEGREE")%>
                                            </td>
                                            <td>
                                                <%# Eval("BRANCH")%>
                                            </td>
                                            <td>
                                                <%# Eval("TOTALINTAKE")%>
                                            </td>
                                            <td>
                                                <%# Eval("TOT_ALLOTED")%>
                                            </td>
                                                <td>
                                                <%# Eval("TOT_ADMITTED")%>
                                            </td>
                                            <td>
                                                <%# Eval("GEN_ADMITTED")%>
                                            </td>
                                                <td>
                                                <%# Eval("SC_ADMITTED")%>
                                            </td>
                                                <td>
                                                <%# Eval("ST_ADMITTED")%>
                                            </td>
                                                <td>
                                                <%# Eval("OBC_ADMITTED")%>
                                            </td>
                                            <td>
                                                <%# Eval("TOT_REMAINING")%>
                                            </td>

                                            <td>
                                                <%# Eval("GEN_ALLOT")%>
                                            </td>
                                            <td>
                                                <%# Eval("SC_ALLOT")%>
                                            </td>
                                            <td>
                                                <%# Eval("ST_ALLOT")%>
                                            </td>
                                            <td>
                                                <%# Eval("OBC_ALLOT")%>
                                            </td>
                                            <%-- <td align="center" style="text-align: center; width: 10px;">
                                                <%# Eval("SVDET_ALLOT")%>
                                            </td>--%>

                                            <td>
                                                <%# Eval("GEN_REMAINING")%>
                                            </td>
                                            <td>
                                                <%# Eval("SC_REMAINING")%>
                                            </td>
                                            <td>
                                                <%# Eval("ST_REMAINING")%>
                                            </td>
                                            <td>
                                                <%# Eval("OBC_REMAINING")%>
                                            </td>
                                            <%--  <td align="center" style="text-align: center; width: 10px;">
                                                <%# Eval("SVDET_REMAINING")%>
                                            </td>--%>

                                            <td>
                                                <%# Eval("GEN_WAIT")%>
                                            </td>
                                            <td>
                                                <%# Eval("SC_WAIT")%>
                                            </td>
                                            <td>
                                                <%# Eval("ST_WAIT")%>
                                            </td>
                                            <td>
                                                <%# Eval("OBC_WAIT")%>
                                            </td>
                                            <%-- <td align="center" style="text-align: center; width: 10px;">
                                                <%# Eval("SVDET_WAIT")%>
                                            </td>--%>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                            <div class="col-12">
                                <asp:ListView ID="lstviewForSVDET" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Admission Status</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Short Name
                                                    </th>
                                                    <th>Tot Intake
                                                    </th>
                                                    <th>Tot Alloted
                                                    </th>
                                                    <th>Tot Admited
                                                    </th>
                                                    <th>Waiting
                                                    </th>
                                                    <th>Remaining
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>          
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td>
                                                <%# Eval("SHORTNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("SVDET_INTAKE")%>
                                            </td>
                                            <td>
                                                <%# Eval("SVDET_ALLOTED")%>
                                            </td>
                                            <td>
                                                <%# Eval("SVDET_ADMITTED")%>
                                            </td>
                                            <td>
                                                <%# Eval("SVDEDT_WAITING")%>
                                            </td>
                                            <td>
                                                <%# Eval("SVDEDT_REMAINING")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlStudents" runat="server" Visible="false">
                                    <asp:ListView ID="lvStudents" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Select Students to Allot Branch</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sr No.
                                                        </th>
                                                        <th>
                                                            <asp:CheckBox ID="cbHead" runat="server" onclick="SelectAll(this,1,'chkAllot');" />
                                                            Check All
                                                        </th>
                                                        <th>Merit list no.
                                                        </th>
                                                        <th>Application ID
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Branch Preference
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td>
                                                    <asp:Label ID="lblsrno" runat="server" Text='<%#Container.DataItemIndex+1 %>' />
                                                </td>
                                                <td>                                                
                                                    <asp:CheckBox ID="chkAllot" runat="server" onclick="ChkHeader(1,'cbHead','chkAllot');" ToolTip='<%# Eval("USERNO")%>' 
                                                        Checked='<%# Eval("LOCK").ToString() == "Y" ? true : false %>' Enabled='<%# Eval("LOCK").ToString() ==  "Y" ? false : true %>'/>
                                                    <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("LOCK") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("MERIT_LIST_NO")%>
                                                </td>
                                                <td>                                               
                                                    <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("USERNAME") %>' ToolTip='<%# Eval("USERNAME")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("FIRSTNAME")%>
                                                    <%# Eval("LASTNAME")%>
                                                </td>
                                                <td>
                                                    <%#Eval("BRANCHNAME_PREF")%>
                                                </td>
                                                <td align="center"></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
           
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnAllot" />
            <asp:PostBackTrigger ControlID="btnpdf" />
            <asp:PostBackTrigger ControlID="btnexport" />
            <asp:PostBackTrigger ControlID="btnCancel" />
           <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;
        }

        function totAllSubjects(headchk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }

            if (headchk.checked == true)
                txtTot.value = hdfTot.value;
            else
                txtTot.value = 0;
        }

        function validateAssign() {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;

            if (txtTot == 0) {
                alert('Please Select atleast one student from student list');
                return false;
            }
            else
                return true;
        }

        function SelectAll(headerid, headid, chk) {
            debugger;
            var tbl = '';
            var list = '';
            if (headid == 1) {
                tbl = document.getElementById('tblstudents');
                list = 'lvStudents';
            }

            try {
                var dataRows = tbl.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        var chkid = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_' + chk;
                        if (headerid.checked) {
                            document.getElementById(chkid).checked = true;
                        }
                        else {
                            document.getElementById(chkid).checked = false;
                        }
                        chkid = '';
                    }
                }
            }
            catch (e) {
                alert(e);
            }

            function ChkHeader(chklst, head, chk) {
                debugger;
                try {
                    var headid = '';
                    var tbl = '';
                    var list = '';
                    var chkcnt = 0;
                    if (chklst == 1) {
                        tbl = document.getElementById('tblstudents');
                        headid = 'ctl00_ContentPlaceHolder1_lvStudents_' + head;
                        list = 'lvStudents';
                    }

                    var dataRows = tbl.getElementsByTagName('tr');
                    if (dataRows != null) {
                        for (i = 0; i < dataRows.length - 1; i++) {
                            var chkid = document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_' + chk);
                            if (chkid.checked)
                                chkcnt++;
                        }
                    }
                    if (chkcnt > 0)
                        document.getElementById(headid).checked = true;
                    else
                        document.getElementById(headid).checked = false;
                }
                catch (e) {
                    alert(e);
                }
            }



        }

    </script>

</asp:Content>

