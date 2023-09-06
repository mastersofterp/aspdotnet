<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="No_Due_Report.aspx.cs" Inherits="STORES_Reports_No_Due_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">NO DUE REPORT</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlDSRDetails" runat="server" Visible="True">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList CssClass="form-control" data-select2-enable="true" ToolTip="Select User Type" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged" runat="server" ID="ddlDept" AutoPostBack="true" AppendDataBoundItems="true"
                                                TabIndex="1">
                                                <asp:ListItem Text="Please Select" Value="0">                                                                           
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Employee Name</label>
                                            </div>
                                            <asp:DropDownList CssClass="form-control" data-select2-enable="true" ToolTip="Select Employee" OnSelectedIndexChanged="ddlEmployees_SelectedIndexChanged" runat="server" ID="ddlEmployees" AutoPostBack="true" AppendDataBoundItems="true"
                                                TabIndex="2">
                                                <asp:ListItem Text="Please Select" Value="0">                                                                           
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                     

                                    </div>
                                  
                                </div>


                            </asp:Panel>
                               <asp:Panel ID="pnlItemMaster" runat="server">
                                            <div class="col-12">
                                                <asp:ListView ID="lvEmployee" runat="server">
                                                    <EmptyDataTemplate>
                                                        <div class="text-center">
                                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                        </div>
                                                    </EmptyDataTemplate>

                                                    <LayoutTemplate>
                                                        <div id="demo-grid">
                                                            <div class="sub-heading">
                                                                <h5>Items</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Sr.No
                                                                        </th>
                                                                        <th>EMPLOYEE NAME
                                                                        </th>
                                                                        <th>ISSUED ITEMS
                                                                        </th>
                                                                        <th>QUANTITY
                                                                        </th>
                                                                        <th>STATUS
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
                                                                <%# Container.DataItemIndex + 1%>

                                                            </td>
                                                             <td>
                                                                <%# Eval("UA_FULLNAME")%>

                                                            </td>
                                                            <td>
                                                                <%# Eval("ITEM_NAME")%>

                                                            </td>
                                                            <td>
                                                                <%# Eval("Issued_qty")%>

                                                            </td>
                                                            <td>
                                                                <%# Eval("STATUS")%>

                                                            </td>

                                                        </tr>
                                                    </ItemTemplate>

                                                </asp:ListView>

                                            </div>
                                     <div class="col-12 btn-footer">
                                        <div id="btnButtons" runat="server" visible="false">
                                            <%--<asp:Button ID="btnPrint" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnPrint_Click" ToolTip="Click To Print" TabIndex="6" />--%>
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click To Reset" TabIndex="7" OnClick="btnCancel_Click" />
                                        </div>
                                    </div>
                                        </asp:Panel>
                        </div>

                    </div>

                </div>

            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

