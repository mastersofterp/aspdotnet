<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EazyPayDoubleVerification.aspx.cs" Inherits="ACADEMIC_BulkBilldeskDoubleVerification" %>

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

    <style type="text/css">
        table {
            border-collapse: collapse;
            border: 5px medium black;
            width: 100%;
        }

        td {
            width: 50%;
            height: 2em;
            border: 1px solid #ccc;
        }
    </style>
    <asp:UpdatePanel ID="updServertoServer" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitleEazyPay" runat="server"></asp:Label></h3>
                        </div>


                        <div class="box-body">


                            <div class="col-12">
                                <asp:RadioButton ID="rdbOnline" runat="server" GroupName="SelectRadio" Text="Online Admission" AutoPostBack="true" OnCheckedChanged="rdbOnline_CheckedChanged" Checked="true" />
                                   <asp:RadioButton ID="rdbMIS" runat="server" GroupName="SelectRadio" Text="MIS Admission" AutoPostBack="true" OnCheckedChanged="rdbMIS_CheckedChanged" />
                                <asp:Panel ID="pnlonline" runat="server" Visible="false">
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
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">B.Tech Admission</asp:ListItem>
                                                        <%-- <asp:ListItem Value="2" >PHD Admission</asp:ListItem>
                                                <asp:ListItem Value="3" >NRI Admission</asp:ListItem>
                                                <asp:ListItem Value="4" >UG/PG Admission</asp:ListItem>--%>
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
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
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
                                                <asp:Button ID="btnShow" runat="server" CssClass="btn btn-info" Text="Show" ValidationGroup="Submit" OnClick="btnShow_Click" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancel_Click" />

                                                <asp:ValidationSummary ID="vsNotice" runat="server" DisplayMode="List" ShowMessageBox="true" ValidationGroup="Submit" ShowSummary="false" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" id="pnlDetails" runat="server" visible="true">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Student Name</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblname" Font-Bold="true" runat="server" /></b></a>
                                                    </li>
                                                    <li class="list-group-item"><b>User Name.</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblEnrollNo" Font-Bold="true" runat="server" /></b></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Degree-Branch</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblbranch" Font-Bold="true" runat="server" /></b></a>
                                                    </li>
                                                    <%--   <li class="list-group-item"><b>Degree</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lbldegree" Font-Bold="true" runat="server" /></b></a>
                                            </li>--%>
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
                                        <asp:ListView ID="lvlist" runat="server" OnItemDataBound="lvlist_ItemDataBound">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5></h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divacadroomlist">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Sr.no
                                                            </th>
                                                            <th>Order ID
                                                            </th>
                                                            <th>Merchant Transaction Reference
                                                            </th>

                                                            <th>Transaction Date
                                                            </th>
                                                            <th>Status
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

                                                        <asp:Button ID="btnManage" runat="server" CssClass="btn btn-info" Text="Check Status" OnClick="btnManage_Click" CommandArgument='<%# Eval("ORDER_ID") %>' CommandName='<%# Eval("IDNO") %>' data-keyboard="false" /><%--Enabled='<%#Convert.ToString(Eval("TRANSACTIONSTATUS")).Equals("Success")?false:true %>' />--%>
                                                        <asp:HiddenField ID="hdid" runat="server" Value='<%# Eval("IDNO") %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                        <%-- </asp:Panel>--%>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlMIS" runat="server" Visible="false">

                                    <div>
                                        <p>
                                            <div class="col-md-12" id="pnlonlinedetails" runat="server">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Transaction Id:</label>
                                                        </div>
                                                        <asp:TextBox ID="TextBox1" runat="server" MaxLength="500" TabIndex="1" CssClass="form-control" ToolTip="Please Enter Transaction Id"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="rfvNotice" runat="server" ControlToValidate="txtTransactionId" ErrorMessage="Please Enter Transaction Id" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Degree</label>
                                                            <asp:DropDownList ID="ddldegree" runat="server" CssClass="form-control" ToolTip="Please Select Degree." AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <%-- <asp:ListItem Value="1">B.Tech Admission</asp:ListItem>--%>
                                                                <%-- <asp:ListItem Value="2" >PHD Admission</asp:ListItem>
                                                <asp:ListItem Value="3" >NRI Admission</asp:ListItem>
                                                <asp:ListItem Value="4" >UG/PG Admission</asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRegno" ValidationGroup="SubmitOnline" Display="None"
                                                                InitialValue="0" ErrorMessage="Please Select Degree." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12 " style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Receipt Code</label>
                                                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" ToolTip="Please Select Receipt Code." AppendDataBoundItems="true" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCode" ValidationGroup="Submit" Display="None"
                                                InitialValue="0" ErrorMessage="Please Select Receipt Code." SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Registration No.</label>
                                                        </div>
                                                        <asp:TextBox CssClass="form-control" ID="txtRegno" runat="server" TabIndex="2" ToolTip="Please Enter Registration No"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRegno"
                                                            Display="None" ErrorMessage="Please Enter Registration No" InitialValue="" SetFocusOnError="true"
                                                            ValidationGroup="SubmitOnline">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-12">
                                                        <div class="label-dynamic">
                                                            <label></label>
                                                        </div>
                                                        <asp:Button ID="btnshowonline" runat="server" CssClass="btn btn-info" Text="Show" ValidationGroup="SubmitOnline" OnClick="btnshowonline_Click" />
                                                        <asp:Button ID="btncancelonline" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btncancelonline_Click" />

                                                        <asp:ValidationSummary ID="rfvsubmit" runat="server" DisplayMode="List" ShowMessageBox="true" ValidationGroup="SubmitOnline" ShowSummary="false" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12" id="showonline" runat="server" visible="false">
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Student Name</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblStudentName" Font-Bold="true" runat="server" /></b></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Registration No.</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblRegno" Font-Bold="true" runat="server" /></b></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Degree-Branch</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblDeg" Font-Bold="true" runat="server" /></b></a>
                                                            </li>
                                                            <%--   <li class="list-group-item"><b>Degree</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lbldegree" Font-Bold="true" runat="server" /></b></a>
                                            </li>--%>
                                                            <asp:Label ID="Label5" Font-Bold="true" runat="server" />
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

                                                    </ul>
                                                </div>
                                                <%--  </div>--%>
                                                <%-- </div>--%>

                                                <%--  <div class="col-12">--%>
                                                <asp:Panel ID="panelOnlinelv" runat="server">
                                                    <asp:ListView ID="lvOnline" runat="server" OnItemDataBound="lvOnline_ItemDataBound1">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5></h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divacadroomlist">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Sr.no
                                                                        </th>
                                                                        <th>Order ID
                                                                        </th>
                                                                        <th>Merchant Transaction Reference
                                                                        </th>

                                                                        <th>Transaction Date
                                                                        </th>
                                                                        <th>Status
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

                                                                    <asp:Button ID="btnManageOnline" runat="server" CssClass="btn btn-info" Text="Check Status" OnClick="btnManageOnline_Click" CommandArgument='<%# Eval("ORDER_ID") %>' CommandName='<%# Eval("IDNO") %>' data-keyboard="false" /><%--Enabled='<%#Convert.ToString(Eval("TRANSACTIONSTATUS")).Equals("Success")?false:true %>' />--%>
                                                                    <asp:HiddenField ID="hdid" runat="server" Value='<%# Eval("IDNO") %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </p>
                                    </div>
                                </asp:Panel>
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
              <asp:PostBackTrigger ControlID="rdbOnline" />
            <asp:PostBackTrigger ControlID="rdbMIS" />
            <asp:PostBackTrigger ControlID="rdbMIS" />
        </Triggers>
    </asp:UpdatePanel>
    <!-- Button to Open the Modal -->

    <!-- The Modal -->
    <div class="modal" id="myModal2" data-keyboard="false" data-backdrop="static">

        <div class="modal-dialog">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title" style="font-weight: bold">Eazy Pay Verification/Re-Query Response</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <!-- Modal body -->
                        <div class="modal-body">
                            <%--   <div class="form-group col-lg-6 col-md-6 col-12">
       
          </div>
           <div class="form-group col-lg-6 col-md-6 col-12">
      
          </div>
          <div class="form-group col-lg-6 col-md-6 col-12">
       
              <br />
         
          </div>--%>

                            <%--  <div class="col-md-12" style="text-align:center">
           
      </div>--%>

                            <table>
                                <tbody>
                                    <tr>
                                        <td style="font-weight: bold; font-size: 13px">Transaction Id :</td>
                                        <td>
                                            <asp:Label ID="txnLabel" Font-Bold="true" runat="server"></asp:Label></td>

                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold; font-size: 13px">Amount :</td>
                                        <td>
                                            <asp:Label ID="lblAmount" Font-Bold="true" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold; font-size: 13px">Transaction Status :</td>
                                        <td>
                                            <asp:Label ID="lblStatus" Font-Bold="true" runat="server"></asp:Label></td>

                                    </tr>
                                </tbody>
                            </table>

                            <!-- Modal footer -->
                            <div class="col-12 mt-3 text-center">
                                <asp:Button ID="btnSubmit" runat="server" ToolTip="Click To Submit." Text="Submit To Manage" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                                <button type="button" class="btn btn-warning" data-dismiss="modal">Close</button>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>




            </div>
        </div>
    </div>




    <div class="modal" id="modalonline" data-keyboard="false" data-backdrop="static">

        <div class="modal-dialog">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title" style="font-weight: bold">Eazy Pay Verification/Re-Query Response</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <!-- Modal body -->
                        <div class="modal-body">


                            <table>
                                <tbody>
                                    <tr>
                                        <td style="font-weight: bold; font-size: 13px">Transaction Id :</td>
                                        <td>
                                            <asp:Label ID="lbltransid" Font-Bold="true" runat="server"></asp:Label></td>

                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold; font-size: 13px">Amount :</td>
                                        <td>
                                            <asp:Label ID="lblamountonline" Font-Bold="true" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold; font-size: 13px">Transaction Status :</td>
                                        <td>
                                            <asp:Label ID="lbltransstatus" Font-Bold="true" runat="server"></asp:Label></td>

                                    </tr>
                                </tbody>
                            </table>

                            <!-- Modal footer -->
                            <div class="col-12 mt-3 text-center">
                                <asp:Button ID="btnsubmitonline" runat="server" ToolTip="Click To Submit." Text="Submit To Manage" OnClick="btnsubmitonline_Click" CssClass="btn btn-primary" />
                                <button type="button" class="btn btn-warning" data-dismiss="modal">Close</button>
                            </div>
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




     <script type="text/javascript">
         function showPopupOnline() {
             $('#modalonline').modal('show');
         }
    </script>
    <script type="text/javascript">
        function closePopupOnline() {
            $('#modalonline').modal('hide');
        }
    </script>
</asp:Content>

