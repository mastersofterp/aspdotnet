<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PayUDoubleVerification.aspx.cs" Inherits="ACADEMIC_ONLINEFEECOLLECTION_PayUDoubleVerification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 550px;">
        <asp:UpdateProgress ID="updprog" runat="server"
            DynamicLayout="true" DisplayAfter="0" AssociatedUpdatePanelID="updServertoServer">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px;">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading...</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updServertoServer" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>


                        <div class="box-body">
                            <div class="col-md-12" id="pnlSelection" runat="server" visible="false">
                                <div class="row">
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Activity Name</label>
                                            <asp:DropDownList ID="ddlActivityName" runat="server" CssClass="form-control" ToolTip="Please Select Activity Name." AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlActivityName_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0" >Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvActivity" runat="server" ControlToValidate="ddlActivityName" ValidationGroup="Submit" Display="None"
                                                InitialValue="0" ErrorMessage="Please Select Activity Name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                         </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Transaction Id:</label>
                                        </div>
                                        <asp:TextBox ID="txtTransactionId" runat="server" MaxLength="500" TabIndex="1" CssClass="form-control" ToolTip="Please Enter Transaction Id"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvNotice" runat="server" ControlToValidate="txtTransactionId" ErrorMessage="Please Enter Transaction Id" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Order ID</label>
                                        </div>
                                        <asp:TextBox CssClass="form-control" ID="txtOrderId" runat="server" TabIndex="2" ToolTip="Please Enter Order ID"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="txtOrderId"
                                            Display="None" ErrorMessage="Please Enter Order ID" InitialValue="" SetFocusOnError="true"
                                            ValidationGroup="Submit">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CssClass="btn btn-info" Text="Show Details" ValidationGroup="Submit" />
                                        <asp:Button ID="btnexcelreport" runat="server" CssClass="btn btn-success" Text="Excel Report" OnClick="btnexcelreport_Click"     />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" OnClick="btnCancel_Click" Text="Cancel" />

                                        <asp:ValidationSummary ID="vsNotice" runat="server" DisplayMode="List" ShowMessageBox="true" ValidationGroup="Submit" ShowSummary="false" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" id="pnlDetails" runat="server" visible="true">
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Name</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblname" Font-Bold="true" runat="server" /></b></a>
                                            </li>
                                            <li class="list-group-item"><b>Enrollment No.</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblEnrollNo" Font-Bold="true" runat="server" /></b></a>
                                            </li>
                                            <li class="list-group-item"><b>Session Name</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblsessionName" Font-Bold="true" runat="server" /></b></a>
                                            </li>
                                            <li class="list-group-item"><b>Semester</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSem" Font-Bold="true" runat="server" /></b></a>
                                            </li>
                                            <li class="list-group-item"><b>Year</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblyear" Font-Bold="true" runat="server" /></b></a>
                                            </li>
                                            <li class="list-group-item"><b>Reciept Type</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblRecieptType" Font-Bold="true" runat="server" /></b></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Reciept No</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblrecno" Font-Bold="true" runat="server" /></b></a>
                                            </li>
                                            <li class="list-group-item"><b>Merchant Transaction Reference</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="Label_MerchTxnRef" Font-Bold="true" runat="server" /></b></a>
                                            </li>
                                            <li class="list-group-item"><b>Order Id</b></td>
                                               <a class="sub-label">
                                                   <asp:Label ID="Label_OrderInfo" Font-Bold="true" runat="server" /></b></a>
                                            </li>

                                            <li class="list-group-item"><b>Transaction Date</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lbldate" Font-Bold="true" runat="server" /></b></a>
                                            </li>
                                            <li class="list-group-item"><b>Transaction Amount</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="Label_Amount" Font-Bold="true" runat="server" /></b></a>
                                            </li>

                                            <li class="list-group-item"><b>Status</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="Label_Message" Font-Bold="true" runat="server" /></b></a>
                                            </li>

                                        </ul>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer" id="divFooter" runat="server" visible="false">
                                <br />

                                <asp:Button ID="btnManage" runat="server" CssClass="btn btn-info" Text="Manage" OnClick="btnManage_Click" />
                                <asp:Button ID="btnhomered" runat="server" class="btn btn-success" Text="Go Back" ForeColor="White" OnClick="btnhomered_Click" />


                                <asp:Label ID="lblResponse" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
           
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnexcelreport" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function PrintDiv() {
            debugger;

            var divContents = document.getElementById("dvContents").innerHTML;

            var printWindow = window.open('', '', 'height=1000,width=1500');
            printWindow.document.write('<html><head><title></title><link rel="shortcut icon" href="../../imgnew1/MAKAUAT_LOGO.png" type="image/x-icon" /><link href="../../cssnew1/bootstrap.min.css" rel="stylesheet"/><link href="../../bootstrap/font-awesome-4.6.3/css/font-awesome.min.css" rel="stylesheet"/><link href="../../dist/css/AdminLTE.min.css" rel="stylesheet"/><style>.div1{display:none;box-shadow:1px 2px 5px #BCBCBC;} body{background-color:#E6E6E6;}</style><style type="text/css">#btnPrint,#home {display: none;}</style>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }

        function BacktoHome() {
            window.location.href = "Default.aspx";
        }

    </script>
</asp:Content>

