<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ValuerEntry.aspx.cs" Inherits="ACADEMIC_EXAMINATION_ValuerEntry" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Content/jquery.js" type="text/javascript"></script>

    <script src="../../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript" charset="utf-8">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();

        }
        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });
            });
        }
    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>
    
    
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"> VALUER ENTRY</h3>
                 <div>
        <asp:UpdateProgress ID="upgupdValuer" runat="server" AssociatedUpdatePanelID="updValuer">
            <ProgressTemplate>
                <asp:Image ID="imgLoad1" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                <span style="font-size: 8pt">Loading...</span>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
                </div>
               <asp:UpdatePanel ID="updValuer" runat="server">
                    <ContentTemplate>
                    <div class="box-body">

                        <div class="form-group col-md-3">
                            <label>Session</label>
                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlSession_OnSelectedIndexChanged"
                                TabIndex="1">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                Display="None" ErrorMessage="Please Select Term" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="report"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-md-3">
                            <label>Degree</label>
                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server"
                                ControlToValidate="ddlDegree" Display="None"
                                ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="report"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-md-3">
                            <label>Branch</label>
                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDepartment" runat="server"
                                ControlToValidate="ddlBranch" Display="None"
                                ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="report"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-md-3">
                            <label>Scheme</label>
                            <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlScheme"
                                Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="report"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-md-3">
                            <label>Semester</label>
                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSemester"
                                Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="report"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-md-3">
                            <label>Course</label>
                            <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true"
                                TabIndex="4" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                Display="None" ErrorMessage="Please Select Course" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="report"></asp:RequiredFieldValidator></td>
                        </div>
                        <div class="form-group col-md-3">
                            <label>Faculty</label>
                            <asp:DropDownList ID="ddlFaculty" runat="server" AppendDataBoundItems="true"
                                TabIndex="4">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlFaculty" runat="server" ControlToValidate="ddlFaculty"
                                Display="None" ErrorMessage="Please Select Faculty" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="report"></asp:RequiredFieldValidator></td>
                        </div>      
                    </div>
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="report"
                                            OnClick="btnSubmit_Click" TabIndex="11" CssClass="btn btn-primary" />                                     
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                            TabIndex="12" CssClass="btn btn-warning"/>                               
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                            </p>
                            <div class="col-md-12">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                    <asp:Repeater ID="lvCourse" runat="server">
                                        <HeaderTemplate>
                                            <div id="demo-grid">                                               
                                                   <h4> Valuer List </h4>
                                            </div>
                                            <table class="table table-hover table-bordered table-responsive">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Course Name
                                                        </th>
                                                        <th>Scheme Name
                                                        </th>
                                                        <th>Valuer Name
                                                        </th>
                                                    </tr>                                                    
                                                    <thead>
                                                        <tbody><tr id="itemPlaceholder" runat="server" /></tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                <td>
                                                    <%# Eval("COURSE_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SCHEMENAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("UA_FULLNAME")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                          </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </asp:Panel>
                            </div>
                        </div>
               </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    
  
                
           
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">
        function totAllSubjects(headchk) {
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
