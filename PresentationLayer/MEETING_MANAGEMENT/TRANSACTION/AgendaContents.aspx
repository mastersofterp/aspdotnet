<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AgendaContents.aspx.cs" Inherits="MEETING_MANAGEMENT_TRANSACTION_AgendaContents" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <%--    <h3 class="box-title">AGENDA DETAILS</h3>--%>
                        <h3 class="box-title">AGENDA DETAILS</h3>
                    </div>
                    <div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                    <asp:Panel ID="pnlDesig" runat="server">
                                        <div class="panel panel-info">
                                            <%--<div class="panel panel-heading">Add/Edit Agenda Details</div>--%>
                                           <%-- <div class="panel panel-heading">Add/Edit Meeting Details</div>--%>
                                            <div class="sub-heading">
                                                    <h5>Add/Edit Meeting Details</h5>
                                                    
                                                </div>
                                            <div class="panel panel-body">
                                                <%-- <div class="form-group col-md-4">
                                                    <label><span style="color: #FF0000"></span>Select Type  :</label>
                                                    <asp:RadioButtonList ID="rdbCommitteeType" runat="server" RepeatDirection="Horizontal" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="rdbCommitteeType_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Value="U">University</asp:ListItem>
                                                        <asp:ListItem Value="C">Institute</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>--%>




                                                <div class="form-group col-md-4">
                                                    <label><span style="color: #FF0000">*</span>Committee :</label>
                                                    <asp:DropDownList ID="ddlCommitee" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                        CssClass="form-control" ToolTip="Select Committee" OnSelectedIndexChanged="ddlCommitee_SelectedIndexChanged" TabIndex="3">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvcommitee" runat="server" ErrorMessage="Please Select Committee"
                                                        ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlCommitee" Display="None"
                                                        Text="*">
                                                    </asp:RequiredFieldValidator>
                                                </div>


                                                <div class="form-group col-md-4">
                                                    <label><span style="color: #FF0000">*</span>Select Meeting :</label>
                                                    <asp:DropDownList ID="ddlpremeeting" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                                        CssClass="form-control" ToolTip="Select Meeting" OnSelectedIndexChanged="ddlpremeeting_SelectedIndexChanged" TabIndex="4">
                                                         <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvMeeting" runat="server" ErrorMessage="Please Select Meeting"
                                                        ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlpremeeting" Display="None"
                                                        Text="*">
                                                    </asp:RequiredFieldValidator>
                                                </div>


                                                <div class="form-group col-md-4">
                                                    <%--<label><span style="color: #FF0000">*</span>Select Agenda :</label>--%>
                                                    <label><span style="color: #FF0000">*</span>Meeting Title :</label>
                                                    <asp:DropDownList ID="ddlAgenda" runat="server" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select Agenda" TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="ddlAgenda_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvAgenda" runat="server" ErrorMessage="Please Select Meeting Title"
                                                        ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlAgenda" Display="None"
                                                        Text="*"></asp:RequiredFieldValidator>
                                                </div>


                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="Panel1" runat="server">
                                        <div class="panel panel-info">
                                            <%--<div class="panel panel-heading">Add Agenda Contents</div>--%>
                                           <%-- <div class="panel panel-heading">Add Agenda Contents</div>--%>
                                            <div class="sub-heading">
                                                    <h5>Add Agenda Contents</h5>
                                                    
                                                </div>
                                            <div class="panel panel-body">
                                                <div class="form-group col-md-4">
                                                    <%--  <label><span style="color: #FF0000">*</span>Agenda Contents :</label>--%>
                                                    <label><span style="color: #FF0000">*</span>Agenda Contents :</label>
                                                    <asp:TextBox ID="txtAgendaDetails" runat="server" TabIndex="5" ToolTip="Add Agenda Details" CssClass="form-control" TextMode="MultiLine" ValidationGroup="Submit"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvAgendaDetails" runat="server" ControlToValidate="txtAgendaDetails" Display="None" ErrorMessage="Please Add Agenda Contents." ValidationGroup="Add"></asp:RequiredFieldValidator>

                                                </div>


                                                <div class="form-group col-md-4">
                                                    <br />
                                                    <label><span style="color: #FF0000"></span></label>
                                                    <asp:Button ID="btnAdd" runat="server" TabIndex="6" ToolTip="Add Details" CssClass="btn btn-primary" Text="Add" ValidationGroup="Add" OnClick="btnAdd_Click" />
                                                    <asp:ValidationSummary ID="vsAdd" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Add" />
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <asp:Panel ID="pnlAgendaList" runat="server" ScrollBars="Auto" Visible="false">
                                                <asp:ListView ID="lvContent" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="lgv1">
                                                            <%-- <h4 class="box-title">AGENDA CONTENTS LIST
                                            </h4>--%>
                                                            <%--<h4 class="box-title">AGENDA CONTENT LIST
                                                            </h4>--%>
                                                            <div class="sub-heading">
                                                    <h5>AGENDA CONTENT LIST</h5>
                                                   
                                                </div>
                                                            <table class="table table-bordered table-hover">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Agenda Content
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
                                                                <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("SRNO") %>'
                                                                    ImageUrl="~/images/edit.png" OnClick="btnEditRec_Click" ToolTip="Edit Record" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblContentName" runat="server" Text='<%# Eval("CONTENT_DETAILS") %>' />
                                                            </td>



                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </asp:Panel>


                                </div>
                            </div>
                        </form>
                    </div>
                    <div class="box-footer">
                        <p class="text-center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" TabIndex="5" CausesValidation="true" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" OnClick="btnCancel_Click" TabIndex="6" CausesValidation="false" />
                            <asp:Button ID="btnReport" runat="server" Text="Agenda Contents Report" CssClass="btn btn-info" ToolTip="Click to get Report" OnClick="btnReport_Click" TabIndex="7" CausesValidation="false" />
                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                        </p>

                        <div class="col-md-12">
                            <asp:Panel ID="pnlContent" runat="server" ScrollBars="Auto" Visible="false">
                                <asp:ListView ID="lvAgendaDetails" runat="server">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <%-- <h4 class="box-title">AGENDA DETAILS LIST
                                            </h4>--%>
                                            <%--<h4 class="box-title">MEETING DETAILS LIST
                                            </h4>--%>
                                            <div class="sub-heading">
                                                    <h5>MEETING DETAILS LIST</h5>
                                                </div>
                                            <table class="table table-striped table-bordered nowrap display">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Edit
                                                        </th>
                                                        <th>Meeting No
                                                        </th>
                                                        <th>Meeting Title
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
                                                <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("ACID") %>'
                                                    ImageUrl="~/images/edit.png" OnClick="btnEdit_Click" ToolTip="Edit Record"
                                                    Enabled='<%# (Eval("LOCK").ToString() == "N" ? true : false) %>' />  <%--shaikh juned 25-06-2022  Add Edit Image Url--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAgendaNo" runat="server" Text='<%# Eval("AGENDANO") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAgendaTitle" runat="server" Text='<%# Eval("AGENDATITAL") %>' />
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            </div> 
        </ContentTemplate>
    </asp:UpdatePanel>

    <div style="width: 90%; padding: 10px">
    </div>

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
    </script>
</asp:Content>

