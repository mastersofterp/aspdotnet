<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BulkBilldeskDoubleVerification_crescent.aspx.cs" Inherits="ACADEMIC_BulkBilldeskDoubleVerification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--    <div style="z-index: 1; position: absolute; top: 10px; left: 550px;">
        <asp:UpdateProgress ID="updprog" runat="server"
            DynamicLayout="true" DisplayAfter="0" AssociatedUpdatePanelID="updServertoServer">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px;">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading...</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>--%>

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
                            <div class="col-md-12" id="pnlSelection" runat="server">
                                <div class="row">
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
                                            <label>Degree</label>
                                                <asp:DropDownList ID="ddlActivityName" runat="server" CssClass="form-control" ToolTip="Please Select Activity Name." AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="true">
                                                <asp:ListItem Value="0" >Please Select</asp:ListItem>
                                                <asp:ListItem Value="1" >B.Tech Admission</asp:ListItem>
                                                <asp:ListItem Value="2" >PHD Admission</asp:ListItem>
                                                <asp:ListItem Value="3" >NRI Admission</asp:ListItem>
                                                <asp:ListItem Value="4" >UG/PG Admission</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvActivity" runat="server" ControlToValidate="ddlActivityName" ValidationGroup="Submit" Display="None"
                                                InitialValue="0" ErrorMessage="Please Select Activity Name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 " style="display: none">
                                         <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Receipt Code</label>
                                                <asp:DropDownList ID="ddlCode" runat="server" CssClass="form-control" ToolTip="Please Select Receipt Code." AppendDataBoundItems="true" data-select2-enable="true">
                                                <asp:ListItem Value="0" >Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCode" ValidationGroup="Submit" Display="None"
                                                InitialValue="0" ErrorMessage="Please Select Receipt Code." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Application Id.</label>
                                        </div>
                                        <asp:TextBox CssClass="form-control" ID="txtprnno" runat="server" TabIndex="2" ToolTip="Please Enter PRN NO"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="txtprnno"
                                            Display="None" ErrorMessage="Please Enter PRN NO" InitialValue="" SetFocusOnError="true"
                                            ValidationGroup="Submit">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnShow" runat="server"  CssClass="btn btn-info" Text="Show" ValidationGroup="Submit" OnClick="btnShow_Click" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning"  Text="Cancel" OnClick="btnCancel_Click" />
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
                                            <li class="list-group-item"><b>RRN No.</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblEnrollNo" Font-Bold="true" runat="server" /></b></a>
                                            </li>
                                            <li class="list-group-item"><b>Branch</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblbranch" Font-Bold="true" runat="server" /></b></a>
                                            </li>
                                            <li class="list-group-item"><b>Degree</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lbldegree" Font-Bold="true" runat="server" /></b></a>
                                            </li>
                                            <asp:Label ID="Label1" Font-Bold="true" runat="server" />
                                          <%--  <li class="list-group-item"><b>Session Name</b>
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
                                            </li>--%>
                                        </ul>
                                    </div>
                                    <%--<div class="col-lg-6 col-md-6 col-12">
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

                                            <%--<li class="list-group-item"><b>Transaction Date</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lbldate" Font-Bold="true" runat="server" /></b></a>
                                            </li>--%>
                                          <%--  <li class="list-group-item"><b>Transaction Amount</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="Label_Amount" Font-Bold="true" runat="server" /></b></a>
                                            </li>--%>

                                           <%-- <li class="list-group-item"><b>Status</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="Label_Message" Font-Bold="true" runat="server" /></b></a>
                                            </li>--%>

                                        </ul>
                                    </div>
                              <%--  </div>--%>
                                 <%-- </div>--%>
                                
                          <%--  <div class="col-12">--%>
                             <%--   <asp:Panel ID="paneLLIST" runat="server">--%>
                                    <asp:ListView ID="lvlist" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5></h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divacadroomlist">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sr.no
                                                        </th>
                                                        <th>
                                                            Order ID
                                                        </th>
                                                        <th>
                                                            Merchant Transaction Reference
                                                        </th>
                                                        
                                                        <th>
                                                            Transaction Date
                                                        </th>
                                                        <th>
                                                            Status
                                                        </th>
                                                        <th>Amount                                                        
                                                        </th>
                                                        
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Container.DataItemIndex + 1%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblorderId" runat="server" Text='<%# Eval("ORDER_ID") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblmertransactionid" runat="server" Text='<%# Eval("TRANSACTIONID") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbltransactiondate" runat="server" Text='<%# Eval("REC_DT") %>'></asp:Label>
                                               </td>
                                                <td>                                                    
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("TRANSACTIONSTATUS") %>' ForeColor='<%#Convert.ToString(Eval("TRANSACTIONSTATUS")).Equals("Success")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                                 <td>
                                                    <asp:Label ID="lblamount" runat="server" Text='<%# Eval("TOTAL_AMT") %>'></asp:Label>
                                                </td>                                                
                                                <td>

                                                   <asp:Button ID="btnManage" runat="server" CssClass="btn btn-info" Text="Check Status" OnClick="btnManage_Click" CommandArgument='<%# Eval("ORDER_ID") %>' CommandName='<%# Eval("IDNO") %>' /><%--Enabled='<%#Convert.ToString(Eval("TRANSACTIONSTATUS")).Equals("Success")?false:true %>' />--%>
                                                    <asp:HiddenField ID="hdid" runat="server" Value='<%# Eval("IDNO") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                               <%-- </asp:Panel>--%>
                            </div>

                          <%--  </div>--%>
                            <%--<div class="col-12 btn-footer" id="divFooter" runat="server" visible="false">
                                <br />

                            <%--    <asp:Button ID="btnManage" runat="server" CssClass="btn btn-info" Text="Manage" />--%>
                           <%--     <asp:Button ID="btnhomered" runat="server" class="btn btn-success" Text="Go Back" ForeColor="White"  />
                            </div>--%>

                            <asp:Label ID="lblResponse" runat="server" Visible="false" Text=""></asp:Label>

                        </div>
                    </div>
                </div>
            </div>
            </div>
           
        </ContentTemplate>
        <Triggers>
          <%--  <asp:PostBackTrigger ControlID="btnShow" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <!-- Button to Open the Modal -->

<!-- The Modal -->
<div class="modal" id="myModal2">
  <div class="modal-dialog">
    <div class="modal-content">

      <!-- Modal Header -->
      <div class="modal-header">
        <h4 class="modal-title">Double Verification/Query API</h4>
        <button type="button" class="close" data-dismiss="modal">&times;</button>
      </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                  <!-- Modal body -->
      <div class="modal-body">
          <div class="form-group col-lg-6 col-md-6 col-12">
        Transaction Id : <asp:Label ID="txnLabel" runat="server"></asp:Label>
          </div>
           <div class="form-group col-lg-6 col-md-6 col-12">
        Amount : <asp:Label ID="lblAmount" runat="server"></asp:Label>
          </div>
          <div class="form-group col-lg-6 col-md-6 col-12">
        Transaction Status : <asp:Label ID="lblStatus" runat="server"></asp:Label>
          </div>
          <div class="col-md-12" style="text-align:center">
              <asp:Button ID="btnSubmit" runat="server" ToolTip="Click To Submit." Text="Submit To Manage" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
          </div>
      </div>
                 <!-- Modal footer -->
      <div class="modal-footer">
        <button type="button" class="btn btn-warning" data-dismiss="modal">Close</button>
      </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    

     

    </div>
  </div>
</div>
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
        <script type="text/javascript">
            function showPopup() {
                $('#myModal2').modal('show');
            }
    </script>
            <script type="text/javascript">
                function closePopup() {
                    $('#myModal2').modal('hide');
                }
    </script>
</asp:Content>

