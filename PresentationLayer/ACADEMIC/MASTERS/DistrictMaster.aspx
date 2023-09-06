<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DistrictMaster.aspx.cs" Inherits="ACADEMIC_MASTERS_DistrictMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script type="text/javascript">
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
    <script src="../../Content/jquery.js" type="text/javascript"></script>
    <script src="../../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>
    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updDistrict"
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

    <asp:UpdatePanel ID="updDistrict" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">DISTRICT MANAGEMENT</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>State </label>
                                        </div>
                                        <asp:DropDownList ID="ddlState" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select Status Type" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="ddlState"
                                            Display="None" ErrorMessage="Please Select State" ValidationGroup="submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>District </label>
                                        </div>
                                        <asp:TextBox ID="txtDistrict" runat="server" TabIndex="2" MaxLength="50" CssClass="form-control"
                                            ToolTip="Please Enter District" />
                                        <asp:RequiredFieldValidator ID="rfvDistrict" runat="server" ControlToValidate="txtDistrict"
                                            Display="None" ErrorMessage="Please Enter District Name" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" OnClick="btnSave_Click" ValidationGroup="submit"
                                    CssClass="btn btn-primary" TabIndex="3" />
                                <asp:Button ID="btnShowReport" runat="server" OnClick="btnShowReport_Click" CausesValidation="False"
                                    TabIndex="4" Text="Report" ToolTip="Show Report" CssClass="btn btn-info" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" OnClick="btnCancel_Click" CausesValidation="false"
                                    CssClass="btn btn-warning" TabIndex="5" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:Repeater ID="lvDistrict" runat="server">
                                        <HeaderTemplate>
                                            <div class="sub-heading">
                                                <h5>District List</h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Id
                                                        </th>
                                                        <th>District
                                                        </th>
                                                        <th>State
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" OnClick="btnEdit_Click" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("DISTRICTNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" />
                                                </td>
                                                <td>
                                                    <%# Eval("DISTRICTNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DISTRICTNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STATENAME")%>
                                                </td>
                                            </tr>

                                        </ItemTemplate>

                                    </asp:Repeater>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="divMsg" runat="server">
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
