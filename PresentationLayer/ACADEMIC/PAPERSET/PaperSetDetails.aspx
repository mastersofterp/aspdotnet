<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PaperSetDetails.aspx.cs" Inherits="ACADEMIC_PaperSetDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPaperStock"
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
    <asp:UpdatePanel ID="updPaperStock" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PAPER SET DETAILS</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                             <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                            ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                            ControlToValidate="ddlClgname" Display="None"
                                            ErrorMessage="Please Select College & Scheme" InitialValue="0"
                                            SetFocusOnError="True" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                            TabIndex="1" ToolTip="Please Select Session">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSession" runat="server"
                                            ControlToValidate="ddlSession" Display="None"
                                            ErrorMessage="Please Select Session" InitialValue="0"
                                            SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="ddlSession" Display="None"
                                            ErrorMessage="Please Select Session" InitialValue="0"
                                            SetFocusOnError="True" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                    </div>
                                    <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                         <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" 
                                            AppendDataBoundItems="True" TabIndex="1" ToolTip="Please Select ddlScheme" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="ddlScheme" Display="None"
                                            ErrorMessage="Please Select Scheme" InitialValue="0"
                                            SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="ddlScheme" Display="None"
                                            ErrorMessage="Please Select Scheme" InitialValue="0"
                                            SetFocusOnError="True" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                    </div>--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                           <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" ToolTip="Please Select" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester"
                                            ValidationGroup="Show" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="ddlSemester" Display="None"
                                            ErrorMessage="Please Select Semester" InitialValue="0"
                                            SetFocusOnError="True" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYddlReport" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlReport" ToolTip="Please Select Report Name" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" TabIndex="1"
                                            AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Report(Paper Setter Name)</asp:ListItem>
                                            <asp:ListItem Value="2">Report(Paper Setter Not Alloted)</asp:ListItem>
                                            <asp:ListItem Value="3">Excel Report(Paper Setter Name)</asp:ListItem>
                                            <asp:ListItem Value="4">Excel Report(Paper Setter Not Alloted)</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvReport" runat="server" ControlToValidate="ddlReport"
                                            Display="None" ErrorMessage="Please Select Report Name"
                                            ValidationGroup="Report" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" ToolTip="Please Select Show" TabIndex="1"
                                    Text="Show" OnClick="btnShow_Click" ValidationGroup="Show" CssClass="btn btn-primary" />
                                <asp:Button ID="btnReport" runat="server" ToolTip="Please Select Report Name" TabIndex="1"
                                    Text="Reports" Visible="true" CssClass="btn btn-info" OnClick="btnReport_Click" ValidationGroup="Report" />
                                <asp:Button ID="btnClear" runat="server" ToolTip="Please Select to Clear" TabIndex="1"
                                    Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Report" />
                            </div>
                            <div class="col-12 mt-4 table-responsive">
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvCourse" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <div class="sub-heading"><h5>Courses</h5></div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Course Code </th>
                                                            <th>Course </th>
                                                            <th>Required Quantity </th>
                                                            <th>Total Student </th>
                                                            <th>Select </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <%-- <td style="width: 10%; text-align: left">
                                                            <asp:Label ID="lblSchemetype" ToolTip="Ccode" runat="server" Text='<%# Eval("SCHEMETYPE") %>' />
                                                        </td>--%>
                                                <td>
                                                    <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip="Ccode" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' ToolTip="Course Name" Width="95%" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblQuantity" runat="server" Font-Bold="true" ForeColor='<%# Eval("REQ_LEVEL") == DBNull.Value ? System.Drawing.Color.Red : System.Drawing.Color.Black %>' Text='<%# Eval("REQ_LEVEL") == DBNull.Value ? 0 : Eval("REQ_LEVEL") %>' ToolTip="Quantity" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTotalStudent" runat="server" Text='<%# Eval("TOTAL_STUDENT") %>' ToolTip="Registered student for this course"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbchk" runat="server"  ToolTip="Select To Apply" /> <%--Checked="true"--%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click" Text="Save" ToolTip="Click to Save" Visible="false" TabIndex="1" />
                           
                                <asp:Button ID="btnunlock" runat="server" CssClass="btn btn-primary" OnClick="btnunlock_Click" Text="Unlock" ToolTip="Click To Unlock" Visible="false" TabIndex="1" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" OnClick="btnCancel_Click" Text="Cancel" ToolTip="Click To Cancel" Visible="false" TabIndex="1" />
                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />

        </Triggers>
    </asp:UpdatePanel>


</asp:Content>
