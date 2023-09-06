<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ComparativeStatApproval.aspx.cs" Inherits="STORES_Transactions_Quotation_ComparativeStatApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <script src="../../../JAVASCRIPTS/jquery.min_1.js"  type="text/javascript"></script>
    <script src="../../../JAVASCRIPTS/jquery-ui.min_1.js"  type="text/javascript"></script>
    <script src="../../../JAVASCRIPTS/AutoComplete.js"  type="text/javascript"></script>
    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div4" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">COMPARATIVE STATEMENT APPROVAL</h3>
                </div>
                <form role="form">
                    <div class="box-body">
                        <div class="panel panel-info">
                            <div class="panel-heading">Comparative Statement Approval</div>
                            <div class="panel-body">
                                <div class="col-md-12">
                                    <asp:Panel ID="pnlCmpst" runat="server">
                                        <div class="box-body">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-md-6">
                                                        <div class="panel panel-info">
                                                            <div class="panel-heading">Quotation List</div>
                                                            <div class="panel-body">
                                                                <asp:Panel ID="Panel4" runat="server">
                                                                    <div class="form-group col-md-12">
                                                                        <asp:ListBox ID="lstQuot" CssClass="form-control" Height="150px" runat="server" AutoPostBack="True" TabIndex="1" ToolTip="Quotation List"
                                                                            OnSelectedIndexChanged="lstQuot_SelectedIndexChanged"></asp:ListBox>
                                                                        <asp:RequiredFieldValidator ID="rfvLstQuot" runat="server" ControlToValidate="lstQuot" Display="None" ErrorMessage="Please Select Quotation No."
                                                                             SetFocusOnError="true" InitialValue="" ValidationGroup="Store"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <div class="panel panel-info">
                                                            <div class="panel-heading">Item List</div>
                                                            <div class="panel-body">
                                                                <asp:Panel ID="Panel10" runat="server">

                                                                    <div class="form-group col-md-12">
                                                                        <asp:ListBox ID="lstItem" CssClass="form-control" Height="150px" runat="server" TabIndex="2" ToolTip="Item List"></asp:ListBox>
                                                                    </div>

                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12 text-center">
                                                        <asp:Button ID="btncmpall" runat="server" ToolTip="Click To Get Comparative Statement" TabIndex="3" CssClass="btn btn-info" OnClick="btncmpall_Click" Text="Get Comparative Statement" ValidationGroup="Store" />
                                                           <asp:Button ID="btncmpallNew" runat="server" ToolTip="Click To Get Comparative Statement" TabIndex="3" CssClass="btn btn-info" OnClick="btncmpallNew_Click" Text="Get Comparative Statement  Excel" ValidationGroup="Store" />
                                                        <asp:Button ID="btnCmpAllPdf" runat="server" TabIndex="4" ToolTip="Click To Export to PDF" CssClass="btn btn-info" Text="Export to PDF" OnClick="btnCmpAllPdf_Click" Visible="false" />
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ValidationGroup="Store" ShowSummary="False"  />
                                                    </div>
                                                    <div class="col-md-12 text-center">
                                                        <br />
                                                    </div>
                                                    <div class="form-group  col-md-12" id="divRemark" runat="server" visible="false">
                                                        <div class="panel panel-info">
                                                            <div class="panel-heading">Approval Details</div>
                                                            <div class="panel-body">
                                                                <asp:Panel ID="pnlDetails" runat="server">
                                                                    <div class="col-md-4">
                                                                        <label>Select :</label>
                                                                        <asp:DropDownList ID="ddlSelect" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="A">Approve</asp:ListItem>
                                                                            <asp:ListItem Value="R">Reject</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <label>Remark :</label>
                                                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <br />
                                                                        <asp:Button ID="btnSubmit" runat="server" ToolTip="Click To Submit" TabIndex="3" CssClass="btn btn-primary" OnClick="btnSubmit_Click" Text="Submit" />
                                                                         <asp:Button ID="btnCancel" runat="server" ToolTip="Click To Cancel" TabIndex="3" CssClass="btn btn-warning" OnClick="btnCancel_Click" Text="Cancel" />
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btncmpall" />
                                                    <asp:PostBackTrigger ControlID="btncmpallNew" />

                                                    
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </asp:Panel>

                                </div>
                            </div>
                        </div>
                    </div>

                </form>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>

    <%--  Reset the sample so it can be played again --%>

    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '0';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }

    </script>
</asp:Content>

