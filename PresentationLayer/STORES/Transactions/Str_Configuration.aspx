<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_Configuration.aspx.cs" Inherits="STORES_Transactions_Str_Configuration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../jquery/jquery-3.2.1.min.js"></script>
    <link href="../../jquery/jquery.multiselect.css" rel="stylesheet" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="../../jquery/jquery.multiselect.js"></script>
    <script src="jquery/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ChkYear() {
            var today = new Date();
            var year = today.getFullYear();
            var nextyear = today.getFullYear() + 1
            // document.getElementById('<%%>').value = year + "-" + nextyear.toString().substring(2); =hfyear.ClientID
        }

        function CheckDate(val) {

            var PrasentDate = new Date();
            if (PrasentDate < date(val)) {
                alert('Future Date is Not Allowed');
                document.getElementById(val.id).value = '';
            }
        }


    </script>
    <script type="text/javascript" language="javascript">
        function SelectAllTrainer(chkcomplaint) {
            var frm = document.forms[0];
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (chkcomplaint.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>
    <%--    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">STORE CONFIGURATION</h3>

                </div>
                <div class="box-body">
                    <div class="col-md-12">
                        <%-- <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span></h5>--%>
                        <asp:Panel ID="pnlStrConfig" runat="server">
                            <div class="panel panel-info">
                                <%-- <div class="panel-heading">Store Configuration</div>--%>
                                <div class="panel-body">
                                    <div class="row" id="divAMC" runat="server" visible="true">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label><span style="color: red"></span>College Name :</label>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" Enabled="true" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control"></asp:DropDownList>
                                                    <%--  <asp:RequiredFieldValidator ID="rfvdeprt" runat="server" Display="None" ErrorMessage="Please Select College" ControlToValidate="ddlCollege"
                                                            InitialValue="0" ValidationGroup="Store"></asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label><span style="color: red"></span>Main Store Department :</label>
                                                    <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control"></asp:DropDownList>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label><span style="color: red"></span>Department Store User :</label>
                                                    <asp:DropDownList ID="ddlStoreUser" runat="server" CssClass="form-control" AppendDataBoundItems="true" Enabled="true" TabIndex="3">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label><span style="color: red"></span>Is Comparative Stat. Approval :</label>
                                                    <asp:CheckBox ID="chkCompSApproval" runat="server" CssClass="form-control" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label><span style="color: red"></span>Comparative Statement Authority :</label>
                                                    <asp:DropDownList ID="ddlCompStmntAuth" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control"></asp:DropDownList>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label><span style="color: red"></span>Is Item Serial Number :</label>
                                                    <asp:CheckBox ID="chkDsrCreation" runat="server" CssClass="form-control" />

                                                </div>
                                                <%--<div class="form-group col-md-4">
                                                        <label><span style="color: #FF0000"></span>Previous DSR Year :</label>
                                                         <asp:TextBox ID="txtPreDsrYear" CssClass="form-control" runat="server" TabIndex="2"
                                                                MaxLength="7" ToolTip="Enter Year of Make"></asp:TextBox>                                                      
                                                    </div>--%>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label>Is Dept Wise Item :</label>
                                                    <asp:CheckBox ID="chkDeptwiseitem" runat="server" CssClass="form-control" />
                                                </div>
                                                <%--<div class="form-group col-md-4">
                                                        <label><span style="color: #FF0000"></span>Current DSR Year :</label>
                                                         <asp:TextBox ID="txtCurDsrYear" CssClass="form-control" runat="server" TabIndex="2"
                                                                MaxLength="7" ToolTip="Enter Current DSR Year" ></asp:TextBox>                                                      
                                                    </div>--%>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label><span style="color: red"></span>Is Special Authority :</label>
                                                    <asp:RadioButtonList ID="rdbSactionAuth" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="Y">Yes&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="N">No</asp:ListItem>
                                                    </asp:RadioButtonList>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label><span style="color: red"></span>Is PO Approval :</label>
                                                    <asp:CheckBox ID="chkPoApproval" runat="server" CssClass="form-control" />

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label>Phone:</label>
                                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" MaxLength="15" onkeyup="return validateNumeric(this)"></asp:TextBox>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label><span style="color: red"></span>Email :</label>
                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="50" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="rxvEmailId" runat="server" ControlToValidate="txtEmail"
                                                        Display="None" ErrorMessage="Enter Email Id Correctly" SetFocusOnError="True"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Store"></asp:RegularExpressionValidator>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label><span style="color: red"></span>Code Standard :</label>
                                                    <asp:TextBox ID="txtCodeStandard" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label><span style="color: red"></span>Is Mail Send :</label>
                                                    <asp:CheckBox ID="chkMailSend" runat="server" CssClass="form-control" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label><span style="color: red"></span>College State :</label>
                                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AppendDataBoundItems="true" Enabled="true" TabIndex="3">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label><span style="color: red"></span>Is Security Gate Pass Entry :</label>
                                                    <asp:CheckBox ID="chkIsSecGPEntry" runat="server" CssClass="form-control" />
                                                </div>

                                                <%--//------------26/09/2022--Add this feild for making BudgetHead Optional-----//--%>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label><span style="color: red"></span>Is Budget Head Req:</label>
                                                    <asp:CheckBox ID="chkBudgetHeadReq" runat="server" CssClass="form-control" />
                                                </div>
                                                <%--//---------end 26/09/2022-----------//--%>
                                               <%-- ///--------------------27/01/2023-----------------------------------//--%>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label><span style="color: red"></span>Is Available Qty Show :</label>
                                                    <asp:RadioButtonList ID="rdblAvailableQty" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="Y">Yes&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="N">No</asp:ListItem>
                                                    </asp:RadioButtonList>

                                                </div>
                                                <%--//-----------------------------------------27/01/2023-------------//--%>
                                                <%--<div class="form-group col-lg-3 col-md-6 col-12">

                                                    <label>Is Available Qty Show:</label>
                                                    <asp:RadioButtonList ID="rdblAvailableQty" runat="server" Repeatedirection="Horizontal">
                                                        <asp:ListItem Value="Y">Yes&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="N">No</asp:ListItem>
                                                    </asp:RadioButtonList>

                                                </div>--%>

                                            </div>
                                        </div>

                                        <div class="col-sm-12 form-group text-center">
                                            <asp:Button ID="btnSave" Text="Save" CssClass="btn btn-primary" TabIndex="16" runat="server" ValidationGroup="Store" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" TabIndex="17" OnClick="btnCancel_Click" />
                                            <asp:ValidationSummary ID="vsAMC" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Store" DisplayMode="List" />
                                        </div>

                                    </div>

                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>


    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }
    </script>
</asp:Content>


