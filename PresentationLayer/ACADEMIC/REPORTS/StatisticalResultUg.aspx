<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
     CodeFile="StatisticalResultUg.aspx.cs" Inherits="ACADEMIC_EXAMINATION_StatisticalResultUg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <asp:UpdatePanel ID="updpnlExam" runat="server">
        <ContentTemplate>
             <div style="z-index: 1; position: absolute; top: 50px; left: 600px;">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updpnlExam">
                                <ProgressTemplate>
                                    <asp:Image ID="imgLoad" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                                    <span style="font-size: 8pt">Loading...</span>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Statistical Result UG</h3>
                        </div>
                        
                            <div class="box-body">
                                <div class="col-md-8">
                                    <fieldset>
                                        <legend>Selection</legend>
                                        <div class="form-group col-md-6">
                                            <label>Admission Batch</label>
                                            <asp:DropDownList ID="ddlAdmbatch" runat="server" AppendDataBoundItems="True" ValidationGroup="Degreewise"
                                                AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddladmbatch" runat="server" ControlToValidate="ddlAdmbatch"
                                                Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="Degreewise"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="rfvddladmbatch1" runat="server" ControlToValidate="ddlAdmbatch"
                                                Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="Collegewise"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label>Session </label>
                                           <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" ValidationGroup="Degreewise"
                                              AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Degreewise"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                         <asp:RequiredFieldValidator ID="rfvSession1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Collegewise"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label>Degree </label>
                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" ValidationGroup="Collegewise"
                                                TabIndex="1" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Collegewise"></asp:RequiredFieldValidator>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Show"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label>Branch </label>
                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True" ValidationGroup="Collegewise"
                                                TabIndex="2" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="Collegewise"></asp:RequiredFieldValidator>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="Show"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-md-6">
                                            <label> Semester</label>
                                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" AutoPostBack="true" ValidationGroup="Degreewise" 
                                                TabIndex="4">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester" Display="None" ErrorMessage="Please Select Semester"
                                             InitialValue="0" ValidationGroup="Degreewise"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSemester" Display="None"
                                             ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Collegewise"></asp:RequiredFieldValidator>
                                        
                                        </div>                                       
                                    </fieldset>
                                </div>
                                <div class="col-md-4">
                                     <fieldset class="fieldset" style="padding: 5px; color: Green">
                                            <legend class="legend">Note</legend>Please Select<br />
                                            <span style="font-weight: bold; color: Red;">Statistical Result University : </span>
                                            <br />For all Degrees :<br />
                                            Admission Batch->Session->Semester<br />
                                            For Particular Degree :<br />
                                            Admission Batch->Session->Degree->Branch->Semester<br />
                                            <span style="font-weight: bold; color: Red;">Statistical Result Collegewise : </span>
                                            <br />Admission Batch->Session->Degree->Branch->Semester<br />
                                        </fieldset>
                                        <br />
                                        <asp:Label ID="lblStudents" runat="server" Font-Bold="true" />
                                </div>
                            </div>
                       <div class="box-footer">
                           <p class="text-center">
                                <asp:Button ID="btnDegreewise" Text="Statistical Result University(PDF)" runat="server"
                                          ValidationGroup="Degreewise"   OnClick="btnDegreewise_Click" CssClass="btn btn-info"/>
                                        <asp:Button ID="btnExcelDegree" Text="Statistical Result University(Excel)" runat="server"
                                          ValidationGroup="Degreewise"   OnClick="btnExcelDegree_Click" CssClass="btn btn-info"/>
                                        <asp:Button ID="btnWordDegree" Text="Statistical Result University(Word)" runat="server"
                                          ValidationGroup="Degreewise"   OnClick="btnWordDegree_Click" CssClass="btn btn-info"/>
                                          <asp:Button ID="btnCollegewise" Text="Statistical Result Collegewise(PDF)" runat="server"
                                           ValidationGroup="Collegewise" style="margin-left: 1px" OnClick="btnCollegewise_Click" CssClass="btn btn-info"/> 
                           </p>
                           <p class="text-center">
                                <asp:Button ID="btnExcelCollege" Text="Statistical Result Collegewise(Excel)" runat="server"
                                          ValidationGroup="Collegewise"  OnClick="btnExcelCollege_Click" CssClass="btn btn-info"/>
                                        <asp:Button ID="btnWordCollege" Text="Statistical Result Collegewise(Word)" runat="server"
                                          ValidationGroup="Collegewise" OnClick="btnWordCollege_Click" CssClass="btn btn-info"/>
                                          <asp:Button ID="btnRankHolders" Text="Rank List" runat="server"
                                           ValidationGroup="Collegewise" OnClick="btnRankHolders_Click" CssClass="btn btn-info"/>
                                        <asp:Button ID="btnClear" runat="server" Text="Cancel" CssClass="btn btn-warning"
                                            TabIndex="11" OnClick="btnClear_Click" />
                                         <asp:ValidationSummary ID="vsDegree" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Degreewise"/>
                                        <asp:ValidationSummary ID="vsCollege" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Collegewise"/>
                           </p>
                       </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
         <%-- <Triggers>
          <asp:PostBackTrigger ControlID="btnExcelDegree" />
             <asp:PostBackTrigger ControlID="btnExcelCollege" />
         </Triggers>--%>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">
        function totAll(headchk) {
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
        }
    </script>

</asp:Content>

