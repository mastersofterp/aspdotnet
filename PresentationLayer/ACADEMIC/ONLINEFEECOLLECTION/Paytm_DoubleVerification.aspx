<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Paytm_DoubleVerification.aspx.cs" Inherits="ACADEMIC_ONLINEFEECOLLECTION_Paytm_DoubleVerification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .d-none {
            margin-bottom: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updServertoServer"
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

    <asp:UpdatePanel ID="updServertoServer" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                            </h3>
                        </div>

                        <div class="box-body">

                            <div class="col-12" id="pnlSelection" runat="server" visible="false">
                                <div class="row">
                                    <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Transaction ID</label>
                                        </div>
                                        <asp:TextBox ID="txtTransactionId" runat="server" MaxLength="500" TabIndex="1" CssClass="form-control" ToolTip="Please Enter Transaction Id"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNotice" runat="server" ControlToValidate="txtTransactionId" ErrorMessage="Please Enter Transaction Id" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>--%>
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
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CssClass="btn btn-primary" Text="Submit" ValidationGroup="Submit" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" OnClick="btnCancel_Click" Text="Cancel" />
                                    <asp:ValidationSummary ID="vsNotice" runat="server" DisplayMode="List" ShowMessageBox="true" ValidationGroup="Submit" ShowSummary="false" />
                                </div>
                            </div>

                            <div id="pnlDetails" runat="server" visible="true">
                                <div id="dvContents" class="col-12">
                                    <div class="row">
                                        <div class="col-12 text-center d-none">
                                            <img alt="" src="Images/nophoto.jpg" style="width: 130px; height: 130px;" class="img-responsive" id="Img1" runat="server" />
                                            <h2>
                                                <asp:Label ID="lblColName" runat="server"></asp:Label>
                                            </h2>
                                        </div>
                                        <div class="col-12">
                                            <div class="sub-heading mb-3">
                                                <h5>Transaction Details</h5>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblname" runat="server" Font-Bold="true" />
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Enrollment No. :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="true" /></a>
                                                </li>
                                                <li class="list-group-item"><b>Session Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblsessionName" runat="server" Font-Bold="true" /></a>
                                                </li>
                                                <li class="list-group-item"><b>Semester :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblSem" runat="server" Font-Bold="true" />
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Year :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblyear" runat="server" Font-Bold="true" /></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Receipt Type :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblRecieptType" runat="server" Font-Bold="true" />
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Receipt No. :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblrecno" runat="server" Font-Bold="true" /></a>
                                                </li>
                                                <li class="list-group-item"><b>Merchant Transaction Reference :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="Label_MerchTxnRef" runat="server" Font-Bold="true" /></a>
                                                </li>
                                                <li class="list-group-item"><b>Order Id :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="Label_OrderInfo" runat="server" Font-Bold="true" /></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item d-none"><b>Merchant ID :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="Label_MerchantID" runat="server" Font-Bold="true" />
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Transaction Date :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lbldate" runat="server" Font-Bold="true" /></a>
                                                </li>
                                                <li class="list-group-item"><b>Transaction Amount :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="Label_Amount" runat="server" Font-Bold="true" /></a>
                                                </li>
                                                <li class="list-group-item"><b>Order Status :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblOrderSatus" runat="server" Font-Bold="true" /></a>
                                                </li>
                                                  <li class="list-group-item"><b>Status :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="Label_Message" runat="server" Font-Bold="true" /></a>
                                                </li>
                                            </ul>
                                        </div>

                                        <div class="col-12 btn-footer mt-3">
                                            <asp:Button ID="btnhomered" runat="server" CssClass="btn btn-primary" Text="Go Back" OnClick="btnhomered_Click" />
                                            <asp:Button ID="btnManage" runat="server" CssClass="btn btn-info" Text="Manage" OnClick="btnManage_Click" Visible="false" />
                                            <input id="btnPrint" type="button" value="Print" class="btn btn-info d-none" onclick="PrintDiv();" class="Printbutton"/>

                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Label ID="lblResponse" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
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

