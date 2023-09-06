<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SubGroupWise_PA.aspx.cs" Inherits="STORES_Masters_SubGroupWise_PA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div3" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">ITEM SUB GROUP WISE PASSING AUTHORITY</h3>
                </div>
                <form role="form">
                    <div class="box-body">
                        <div class="col-md-12">
                            <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span></h5>
                            <asp:Panel ID="pnlDSRDetails" runat="server" Visible="True">
                                <div class="panel panel-info">
                                    <div class="panel-heading">SUBGROUP WISE APPROVAL</div>
                                    <div class="panel-body">

                                        <div class="col-md-12">
                                            <div class="form-group col-md-4">
                                                <%--<label>Department:<span style="color: #FF0000">*</span></label>--%>
                                                <asp:DropDownList CssClass="form-control" ToolTip="Select Department" runat="server" ID="ddlDept" AppendDataBoundItems="true" Visible="false"
                                                    TabIndex="1">
                                                    <asp:ListItem Text="Please Select" Value="0">                                                                           
                                                    </asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlDepartment" runat="server" ControlToValidate="ddlDept"
                                                    Display="None" ErrorMessage="Please Select Department" ValidationGroup="store"
                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-12 table-responsive">
                                            <asp:Panel ID="pnlItemMaster" runat="server">
                                                <div class="col-md-12 table-responsive">
                                                    <asp:ListView ID="lvItemMaster" runat="server">
                                                        <EmptyDataTemplate>
                                                            <br />
                                                            <div class="text-center">
                                                                <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" />
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div id="demo-grid" class="vista-grid">
                                                                <div class="titlebar">
                                                                    <h4>SubGroup</h4>
                                                                </div>
                                                                <table class="table table-bordered table-hover table-responsive">
                                                                    <tr class="bg-light-blue">
                                                                        <th>SELECT
                                                                        </th>

                                                                        <th>SUBGROUP NAME
                                                                        </th>
                                                                         <th>REMARKS
                                                                        </th>
                                                                    </tr>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>

                                                                <td>
                                                                    <%--<%# Eval("MISGNO")%>--%>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("MISGNO") %>' />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("MISGNAME")%>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtRemark" runat="server" placeholder="Remarks" CssClass="form-control"></asp:TextBox>
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:ListView>

                                                </div>
                                            </asp:Panel>
                                        </div>




                                        <div class="col-md-12 text-center">

                                            <asp:Button ID="btnApprove" runat="server" ValidationGroup="store" Text="Approve" CssClass="btn btn-info" ToolTip="Click To Approve" TabIndex="6" OnClick="btnApprove_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click To Reset" TabIndex="7" Visible="false" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="store"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>
                                    </div>
                                </div>


                            </asp:Panel>
                        </div>
                    </div>
                </form>
            </div>

        </div>

    </div>
    <div id="divMsg" runat="server"></div>
</asp:Content>

