<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_ToolKit_Allocation.aspx.cs" Inherits="STORES_Transactions_Str_ToolKit_Allocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">TOOLKIT ALLOCATION</h3>
                </div>

                <div class="box-body">
                    <div class="col-md-12">

                        <div class="panel panel-info">
                            <div class="panel-heading">ToolKit Allocation</div>
                            <div class="panel-body">
                                <asp:Panel ID="pnlmain" runat="server">
                                    <div class="form-group col-md-12">
                                        <div class="form-group col-md-6">
                                            <div class="form-group col-md-10">
                                                <label>Ref No.:</label>
                                                <asp:TextBox ID="txtRefNo" runat="server" TabIndex="1" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-10">
                                                <label>ToolKit:<span style="color: red">*</span></label>
                                                <asp:DropDownList ID="ddltoolkit" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="2"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddltoolkit"
                                                    ErrorMessage="Please Select Toolkit" Display="None" ValidationGroup="StoreReq" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                           
                                            <div class="form-group col-md-10">
                                                <label>Year:<span style="color: red">*</span></label>
                                                <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="3"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlYear"
                                                    ErrorMessage="Please Select Year" Display="None" ValidationGroup="StoreReq" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-10">
                                                <label>Degree:<span style="color: red">*</span></label>
                                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="4"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDegree"
                                                    ErrorMessage="Please Select Course" Display="None" ValidationGroup="StoreReq" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-2" style="padding-top: 25px">
                                             
                                                <asp:Button ID="btnShow" Text="Show" runat="server" TabIndex="5" ValidationGroup="StoreReq" DisplayMode="List" OnClick="btnShow_Click" ToolTip="Click To Show" CssClass="btn btn-info" />
                                                 <asp:ValidationSummary ID="validSummaryReq" runat="server" DisplayMode="List" ValidationGroup="StoreReq"  ShowMessageBox="true" ShowSummary="false" />
                                            </div>
                                        </div>


                                    </div>
                                    <div class="form-group col-md-6">
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlAllo" runat="server" Visible="false">
                                    <div class="form-group col-md-12 table-responsive">
                                        <asp:ListView ID="lvitemInvoice" runat="server">
                                            <EmptyDataTemplate>
                                                <br />
                                                <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" />
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div id="demo-grid" class="vista-grid">
                                                    <div class="titlebar">
                                                        <h4>STUDENT LIST</h4>
                                                    </div>

                                                    <table class="table table-bordered table-hover table-responsive">
                                                        <tr class="bg-light-blue">
                                                            <th>SELECT</th>

                                                            <th>STUDENT NAME
                                                            </th>
                                                            <th>DEGREE
                                                            </th>
                                                            <th>YEAR
                                                            </th>

                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("IDNO") %>' TabIndex="6"/>

                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("NAME")%>'></asp:Label>
                                                    </td>
                                                    <td>

                                                        <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREENAME")%>'></asp:Label>
                                                    </td>
                                                    <td>

                                                        <asp:Label ID="lblYear" runat="server" Text='<%# Eval("YEARNAME")%>'></asp:Label>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>

                                        </asp:ListView>
                                    </div>
                                    <div class="col-md-12 text-center">
                                        <asp:Button ID="btnSubmit" Text="Submit" runat="server" TabIndex="7" OnClick="btnSubmit_Click" ToolTip="Click To Submit"  CssClass="btn btn-primary" />
                                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" TabIndex="8" ToolTip="Click To Cancel" CssClass="btn btn-warning" CausesValidation="false" OnClick="btnCancel_Click" />
                                       
                                      </div>                                                                                          
                                </asp:Panel>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            </div>
        </div>

</asp:Content>

