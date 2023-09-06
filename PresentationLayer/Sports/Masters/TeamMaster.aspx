<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TeamMaster.aspx.cs"
    Inherits="Sports_Masters_TeamMaster" Title=" " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div class="loader-container">
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__ball"></div>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <!----page done by ashwini-02-03-2022----->
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TEAM MASTER</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="pnlDesig" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Team Type</label>
                                            </div>
                                            <asp:RadioButtonList ID="rdbTeamType" runat="server" RepeatDirection="Horizontal" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="rdbTeamType_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="U">University Teams</asp:ListItem>
                                                <asp:ListItem Value="O">Other Teams</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trCollegeNo" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Institute Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="2"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control" ToolTip="Select Institute">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select Institute" ValidationGroup="Submit" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trCollegeName" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Institute Name</label>
                                            </div>
                                            <asp:TextBox ID="txtCollegeName" runat="server" MaxLength="60" onkeypress="return CheckAlphabet(event, this);" TabIndex="2" CssClass="form-control" ToolTip="Enter Institute Name"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCollegeName" ValidationGroup="Submit" ControlToValidate="txtCollegeName"
                                                Display="None" ErrorMessage="Please Enter Institute Name..!!" SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Academic Year</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAcadYear" runat="server" AppendDataBoundItems="True" TabIndex="3" CssClass="form-control" data-select2-enable="true" ToolTip="Select Academic Year">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddl" runat="server" ErrorMessage="Please Select Academic Year."
                                                ControlToValidate="ddlAcadYear" InitialValue="0" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Sport Type </label>
                                            </div>
                                            <asp:DropDownList ID="ddlSportType" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlSportType_SelectedIndexChanged" TabIndex="4" CssClass="form-control" data-select2-enable="true" ToolTip="Select Sport Type">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSptyp" runat="server"
                                                ErrorMessage="Please Select Sport Type." ControlToValidate="ddlSportType" InitialValue="0" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Sport Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSportName" runat="server" AppendDataBoundItems="True" TabIndex="5" CssClass="form-control" data-select2-enable="true" ToolTip="Select Sport Name">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSportName" runat="server"
                                                SetFocusOnError="true" Display="None" ErrorMessage="Please Enter Sport Name...!!"
                                                ValidationGroup="Submit" ControlToValidate="ddlSportName" InitialValue="0"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Team Name </label>
                                            </div>
                                            <asp:TextBox ID="txtTeamName" runat="server" MaxLength="60" onkeypress="return CheckAlphabet(event, this);" TabIndex="6" CssClass="form-control" ToolTip="Select Team"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTeamName" ValidationGroup="Submit" ControlToValidate="txtTeamName"
                                                Display="None" ErrorMessage="Please Enter Team Name..!!" SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>

                                        </div>

                                    </div>

                                </asp:Panel>
                            </div>
                            <div class=" col-12 btn-footer">
                                <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click" CausesValidation="true" TabIndex="7" CssClass="btn btn-outline-info" ToolTip="Click here to Submit" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" ToolTip="Click here to Cancel" CausesValidation="false" TabIndex="8" />
                                <asp:Button ID="btnShowReport" runat="server" Text="Report" OnClick="btnShowReport_Click" TabIndex="9" CssClass="btn btn-outline-primary" ToolTip="Click to get Report" />--%>
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" TabIndex="5" />
                            <asp:Button ID="btnShowReport" runat="server" Text="Report" CssClass="btn btn-info" ToolTip="Click to get Report" OnClick="btnShowReport_Click" TabIndex="7"  />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Cancel" CausesValidation="false" TabIndex="6" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvTeam" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>TEAM ENTRY LIST</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit
                                                            </th>
                                                            <th>Academic Year
                                                            </th>
                                                            <th>Sport Name
                                                            </th>
                                                            <th>Team Name
                                                            </th>
                                                            <th>Institute
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
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                        CommandArgument='<%# Eval("TEAMID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <%# Eval("BATCHNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TEAMNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("COLLEGE_NAME")%>
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
</asp:Content>
