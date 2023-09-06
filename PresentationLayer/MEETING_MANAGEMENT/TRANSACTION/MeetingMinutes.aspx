<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="MeetingMinutes.aspx.cs" Inherits="MEETING_MANAGEMENT_Transaction_MeetingMinutes"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <%--<asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity" DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
    </div>
    <%-- <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script> <%--////07/04/2022--%>
    <div>
        <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updActivity"
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
        </asp:UpdateProgress>--%> <%--////07/04/2022--%>
    </div>
    <%--   <asp:UpdatePanel ID="updActivity" runat="server"> //07/04/2022
        <ContentTemplate>--%>
    <asp:Panel ID="panel2" runat="server"> <%--////07/04/2022--%>
        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div id="div2" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">MEETING DETAILS</h3>
                    </div>

                    <div class="box-body">
                        <div class="col-12">
                            <asp:Panel ID="pnlMeeting" runat="server">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Committee</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCommitee" runat="server" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlCommitee_SelectedIndexChanged" CssClass="form-control" ToolTip="Select Committee" TabIndex="3">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvcommitee" runat="server" ControlToValidate="ddlCommitee"
                                            Display="None" ErrorMessage="Please Select Commitee" InitialValue="0" Text="*"
                                            ValidationGroup="Submit">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Select Meeting</label>
                                        </div>
                                        <asp:DropDownList ID="ddlMeeting" runat="server" AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="true" Enabled="false"
                                            CssClass="form-control" ToolTip="Select Meeting" OnSelectedIndexChanged="ddlMeeting_SelectedIndexChanged" TabIndex="4">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Agenda No </label>
                                        </div>
                                        <asp:TextBox ID="txtAgendaNo" runat="server" Enabled="false" CssClass="form-control">
                                        </asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Lock Meeting </label>
                                        </div>
                                        <asp:CheckBox ID="chklock" AutoPostBack="true" runat="server" />
                                    </div>




                                </div>
                            </asp:Panel>
                        </div>

                        <asp:Panel ID="pnlMemberList" runat="server" Visible="false">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Member Details</h5>
                                </div>
                            </div>
                            <div class="col-12 mb-4">
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvStudents" runat="server" Visible="true" OnItemDataBound="lvStudents_ItemDataBound">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Member List </h5>
                                                    <%--(Click Checkboxes for Present)--%>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>
                                                                <asp:CheckBox ID="cbHead" runat="server" onclick="SelectAll(this)" Visible="true" />&nbsp Select
                                                            </th>
                                                            <th>Name
                                                            </th>
                                                            <th>Designation
                                                            </th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                   <%-- <asp:CheckBox ID="chkRow" runat="server" />
                                                    <asp:HiddenField ID="hdnmember" runat="server" Value='<%# Eval("FK_MEMBER")%>' />--%>
                                                      <asp:CheckBox ID="chkRow" runat="server" OnCheckedChanged="chkRow_CheckedChanged" ToolTip='<%# Eval("FK_MEMBER")%>' AutoPostBack="true"/>  <%-- SHAIKH JUNED 27-06-2022--%>
                                                    <asp:HiddenField ID="hdnmember" runat="server" Value='<%# Eval("FK_MEMBER")%>' />  <%-- SHAIKH JUNED 27-06-2022--%>
                                                </td>
                                                <td>
                                                    <%# Eval("NAME")%>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddldesig" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </asp:Panel>
                        <asp:Panel ID="pnlAgenda" runat="server" Visible="false">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Meeting Details</h5>
                                </div>
                            </div>
                            <div class="col-12 mb-4">
                                <asp:Panel ID="Panel4" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvAgenda" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Meeting List </h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Select
                                                            </th>
                                                            <th>Meeting No
                                                            </th>
                                                            <th>Meeting Details
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnSelect" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                        CommandArgument='<%# Eval("PK_AGENDA") %>' AlternateText="Edit Record" ToolTip='<%# Eval("PK_AGENDA") %>'
                                                        OnClick="btnSelect_Click" />
                                                </td>
                                                <td>
                                                    <%# Eval("MEETING_CODE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("AGENDATITAL")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </asp:Panel>
                        <div class="col-12">
                            <asp:Panel ID="pnlAgendaDetails" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Add/Edit Agenda Details</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Meeting Title </label>
                                        </div>
                                        <asp:Label ID="lblagtital" runat="server"></asp:Label>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Meeting Description</label>   <%--shaikh juned 27-06-2022--%>
                                        </div>
                                        <asp:TextBox ID="txtAgendaDetails" runat="server" TextMode="MultiLine" Width="90%" TabIndex="5" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAgendaDetails" runat="server" ControlToValidate="txtAgendaDetails"
                                            Display="None" ErrorMessage="Please Enter Meeting Minutes" Text="*"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Attach Files</label>
                                        </div>
                                        <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Select file to upload" />
                                    </div>
                                </div>
                                <div class=" btn-footer">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="btn btn-primary" ToolTip="Click here to Add" />
                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="lvfile" runat="server" Visible="false">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading">
                                                        <h5>Download files</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Action
                                                                </th>
                                                                <th>File Name
                                                                </th>
                                                                <th>Download
                                                                </th>

                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png" CommandArgument='<%#DataBinder.Eval(Container, "DataItem") %>'
                                                            AlternateText="Delete Record" OnClick="btnDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" />
                                                    </td>
                                                    <td>
                                                        <%#GetFileName(DataBinder.Eval(Container, "DataItem")) %>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imgFile" runat="Server" ImageUrl="~/images/action_down.png"
                                                            AlternateText='<%#DataBinder.Eval(Container, "DataItem") %>' ToolTip='<%#DataBinder.Eval(Container, "DataItem")%>'
                                                            OnClick="imgdownload_Click" CausesValidation="False" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>

                        </div>
                        <div class="col-12 btn-footer">

                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSave_Click" CausesValidation="true" CssClass="btn btn-primary" ToolTip="Click here to Submit" TabIndex="10" />
                            <asp:Button ID="btnPrint" runat="server" Text="Report" OnClick="btnPrint_Click" CausesValidation="false" CssClass="btn btn-info" ToolTip="Click here to Print" TabIndex="12" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click1" TabIndex="11" CausesValidation="false" CssClass="btn btn-warning" ToolTip="Click here to Reset" />

                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />

                        </div>
                    </div>
                </div>
            </div>
         </div>   <%--////07/04/2022--%>
    </asp:Panel>  <%--////07/04/2022--%>

    <%--    </ContentTemplate>--%>
    <%-- <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="lvfile" />
        </Triggers>--%>
    <%--    </asp:UpdatePanel>--%>






    <script type="text/javascript" language="javascript">
        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../../IMAGES/expand_blue.jpg";
            } //../images/action_up.gif
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../../IMAGES/collapse_blue.jpg";
            }
        }
        function SelectAll(mainChk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (mainChk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>

</asp:Content>
