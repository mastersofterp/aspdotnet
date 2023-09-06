<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="RecStat.aspx.cs" Inherits="ACADEMIC_PAPERSET_RecStat" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updRcvSatus"
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
    <asp:UpdatePanel ID="updRcvSatus" runat="server">
        <ContentTemplate>
            <%--   <div id="Div1" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
            </div>
            <!-- Info panel to be displayed as a flyout when the button is clicked -->
            <div id="Div2" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                <div id="Div3" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return false;" Text="X"
                        ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                </div>
                <div>
                    <p class="page_help_head">
                        <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                        Edit Record
                    </p>
                    <p class="page_help_text">
                        <asp:Label ID="Label1" runat="server" Font-Names="Trebuchet MS" />
                    </p>
                </div>
            </div>

            <script type="text/javascript" language="javascript">
                // Move an element directly on top of another element (and optionally
                // make it the same size)
                function Cover(bottom, top, ignoreSize) {
                    var location = Sys.UI.DomElement.getLocation(bottom);
                    top.style.position = 'absolute';
                    top.style.top = location.y + 'px';
                    top.style.left = location.x + 'px';
                    if (!ignoreSize) {
                        top.style.height = bottom.offsetHeight + 'px';
                        top.style.width = bottom.offsetWidth + 'px';
                    }
                }
            </script>--%>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">RECEIVE STATUS OF PAPER SET </h3>
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
                                            ValidationGroup="show4"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" ValidationGroup="show4" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" class="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSession4" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="show4"></asp:RequiredFieldValidator>
                                    </div>
                                    <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged"
                                            AppendDataBoundItems="True" TabIndex="1" ToolTip="Please Select ddlScheme">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDepartment4" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="show4"></asp:RequiredFieldValidator>
                                    </div>--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" class="form-control" AppendDataBoundItems="True"
                                            TabIndex="5" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester4_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSemester4" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="show4"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                            TabIndex="6" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit5" CssClass="btn btn-primary" runat="server" OnClick="btnRcvdAll_Click" Width="150px" Text="Received All"
                                    Font-Bold="true" ValidationGroup="show4" />
                                <asp:ValidationSummary ID="vsshow4" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="show4" />
                                <asp:Button ID="btnClear5" ValidationGroup="show3" runat="server" CssClass="btn btn-warning"
                                    Text="Clear" OnClick="btnClear5_Click" Font-Bold="true" />
                            </div>

                            <div class="col-12 mt-3">
                                <asp:Panel ID="pnlList4" runat="server" Visible="false">
                                    <asp:ListView ID="lvCourse4" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>Paper Setter List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Semester
                                                            </th>
                                                            <th>Code
                                                            </th>
                                                            <th>Course
                                                            </th>
                                                            <th>Approved Paper Setter
                                                            </th>
                                                            <th>QTY
                                                            </th>
                                                            <th>MOI
                                                            </th>
                                                            <th>Received QTY
                                                            </th>
                                                            <th>Received MOI
                                                            </th>
                                                            <th>Status
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
                                                    <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER")%>' ToolTip='<%# Eval("SEMESTERNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCcode" runat="server" Text='<%# Eval("CCODE")%>' ToolTip='<%# Eval("CCODE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("COURSE_NAME")%>' ToolTip='<%# Eval("CCODE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFaculty1" Width="60%" Text='<%# Eval("NAME")%>' runat="server"
                                                        ForeColor='<%# Eval("CANCEL").ToString().ToLower()=="true"? System.Drawing.Color.Red : System.Drawing.Color.Black %>'
                                                        ToolTip='<%# Eval("APPROVED")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblQTY" runat="server" Text='<%# Eval("QTY")%>' ToolTip='<%# Eval("QTY")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMOI" runat="server" Text='<%# Eval("MOI")%>' ToolTip='<%# Eval("MOI")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRcvdQTY" runat="server" MaxLength="2" Text='<%# Eval("QT_RCVD") ==DBNull.Value ? Eval("QTY") : Eval("QT_RCVD")%>'
                                                        ToolTip='<%# Eval("QT_RCVD") == DBNull.Value? Eval("CANCEL").ToString().ToLower()=="true"? " " : Eval("QTY") : Eval("QT_RCVD")%>'
                                                        Enabled='<%# Eval("RECEIVED").ToString().ToLower() == "true" ? false:Eval("CANCEL").ToString().ToLower()=="true"?false:true %>'></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtRcvdQTY" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtRcvdQTY">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRcvdMOI" runat="server" MaxLength="2" Text='<%# Eval("MOI_RCVD")==DBNull.Value ? Eval("MOI") : Eval("MOI_RCVD")%>'
                                                        ToolTip='<%# Eval("MOI_RCVD")==DBNull.Value ? Eval("CANCEL").ToString().ToLower()=="true"? " " : Eval("MOI") : Eval("MOI_RCVD")%>'
                                                        Enabled='<%# Eval("RECEIVED").ToString().ToLower() == "true" ? false:Eval("CANCEL").ToString().ToLower()=="true"?false:true %>'></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtRcvdMOI" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtRcvdMOI">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:Button runat="server" CommandArgument='<%# Eval("APPROVED")%>' Enabled='<%# Eval("RECEIVED").ToString().ToLower() == "true" ? false:Eval("CANCEL").ToString().ToLower()=="true"?false:true %>'
                                                        CommandName="1" ID="btnRCVd1" OnClick="btnRCVd_Click" Text="Received" CssClass="btn btn-primary" />
                                                    <asp:Button runat="server" CommandArgument='<%# Eval("APPROVED")%>' Enabled='<%# Eval("RECEIVED").ToString().ToLower() == "true" ? false:Eval("CANCEL").ToString().ToLower()=="true"?false:true %>'
                                                        CommandName='<%# Eval("CCODE")%>' ID="btnCancelStatus" OnClick="btnCancelStatus_Click"
                                                        Text="Cancel" CssClass="btn btn-primary" />
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
