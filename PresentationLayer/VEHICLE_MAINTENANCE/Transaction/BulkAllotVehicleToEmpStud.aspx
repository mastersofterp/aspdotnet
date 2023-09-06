<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BulkAllotVehicleToEmpStud.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_BulkAllotVehicleToEmpStud" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPanel"
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
    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">BULK ROUTE ALLOTMENT</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select User Type</label>
                                            </div>
                                            <asp:RadioButtonList ID="rdbUserType" runat="server" RepeatDirection="Horizontal" ToolTip="Select User Type"
                                                OnSelectedIndexChanged="rdbUserType_SelectedIndexChanged" AutoPostBack="true" TabIndex="1">
                                                <asp:ListItem Selected="True" Value="1">Employee &nbsp; &nbsp; &nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2">Student</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:RadioButtonList ID="rdbAllotted" runat="server" RepeatDirection="Horizontal" ToolTip="Select Allotted/Not Allotted"
                                                OnSelectedIndexChanged="rdbAllotted_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                                <asp:ListItem Selected="True" Value="1">Not Allotted  &nbsp; &nbsp; &nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2">Allotted</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12" id="divSelect" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divDegree" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Degree</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Select Degree" TabIndex="3" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ErrorMessage="Please Select Degree."
                                                ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlDegree" Display="None">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divBranch" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Branch</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Select Branch" TabIndex="4">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ErrorMessage="Please Select Branch."
                                                ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlBranch" Display="None">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSem" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Semester</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Select Semester" TabIndex="5"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSem" runat="server" ErrorMessage="Please Select Semester."
                                                ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlSem" Display="None">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divStud" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary"
                                                OnClick="btnShow_Click" TabIndex="10" ToolTip="Click here to Show" />
                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvAllotment" runat="server" OnItemDataBound="lvAllotment_ItemDataBound">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Route Allotment List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Select                        
                                                            </th>
                                                            <th>
                                                                <asp:Label ID="lblUserName" runat="server" Text="EMPLOYEE NAME"></asp:Label>
                                                            </th>
                                                            <th>ROUTE
                                                            </th>

                                                            <th>BOARDING POINT
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
                                                    <asp:CheckBox ID="chkRow" runat="server" ToolTip='<%# Eval("IDNO")%>' TabIndex="4" />
                                                    <asp:HiddenField ID="hdnIDNO" runat="server" Value='<%# Eval("IDNO")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("NAME")%>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlRoute" runat="server" AppendDataBoundItems="true"
                                                        AutoPostBack="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Route"
                                                        OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged" TabIndex="7">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvRoute" runat="server" ControlToValidate="ddlRoute"
                                                        Display="None" ErrorMessage="Please Select Route" ValidationGroup="Submit" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlBoardingPoint" runat="server" AppendDataBoundItems="true"
                                                        CssClass="form-control" data-select2-enable="true" ToolTip="Select Boarding Point" TabIndex="7">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvBP" runat="server" ControlToValidate="ddlBoardingPoint"
                                                        Display="None" ErrorMessage="Please Select Boarding Point" ValidationGroup="Submit" SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="Submit"
                                    OnClick="btnSubmit_Click" TabIndex="10" ToolTip="Click here to Submit" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnReport_Click"
                                    TabIndex="12" ToolTip="Click here to Show Report" />
                                <asp:Button ID="btnStudReport" runat="server" Text="Student Report" CssClass="btn btn-info" OnClick="btnStudReport_Click"
                                    TabIndex="13" ToolTip="Student Report" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click"
                                    TabIndex="11" ToolTip="Click here to Reset" />
                                <asp:ValidationSummary ID="VS1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="Submit" />
                                <asp:ValidationSummary ID="VS2" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" ValidationGroup="Add" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
