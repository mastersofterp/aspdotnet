<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Acc_GST_TDS_Report.aspx.cs" Inherits="ACCOUNT_Acc_GST_TDS_Report" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../Scripts/jquery.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-impromptu.2.6.min.js" type="text/javascript"></script>

    <link href="../Scripts/impromptu.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function confirmDeleteResult(v, m, f) {
            if (v) //user clicked OK 
                $('#' + f.hidID).click();
        }

    </script>

   
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">GST ON TDS REPORT</h3>

                        </div>


                        <div class="box-body">
                            <div class="col-md-12">
                                <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span></h5>
                                <asp:Panel ID="pnlDSRDetails" runat="server" Visible="True">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">GST On TDS Report</div>
                                        <div class="panel-body">
                                            <div class="form-group col-md-12">
                                                <asp:Panel ID="Panel_Confirm" runat="server" CssClass="Panel_Confirm" EnableViewState="false"
                                                    Visible="false">
                                                    <table class="table table-bordered table-hover table-responsive">
                                                        <tr>
                                                            <td style="width: 3%; vertical-align: top"></td>
                                                            <td>
                                                                <asp:Label ID="Label_ConfirmMessage" runat="server" Style="font-family: Verdana; font-size: 11px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </div>
                                            <div class="form-group col-md-12">
                                                 <div class="form-group col-md-2">
                                                <label>Select Report :<span style="color: red;">*</span></label>
                                                     </div>
                                                      <div class="form-group col-md-4">
                                                <asp:RadioButtonList ID="rblGroup" runat="server" TabIndex="1" RepeatDirection="Horizontal" >
                                                  
                                                    <asp:ListItem Enabled="true" Selected="True" Text="GST TDS REPORT &nbsp;" Value="1"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text="GST REPORT &nbsp;" Value="2" ></asp:ListItem>
                                                   
                                                </asp:RadioButtonList>
                                                          </div>
                                            </div>

                                          
                                            <div class="form-group col-md-12">
                                                <div class="form-group col-md-2">
                                                    <label>From Date:<span style="color: red;">*</label>
                                                    </div>
                                                <div class="form-group col-md-4">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" ToolTip="Enter From Date" TabIndex="1" Text=""></asp:TextBox>
                                                        <div class="input-group-addon">
                                                            <asp:ImageButton ID="imgFromDate" runat="server" ImageUrl="~/IMAGES/calendar.png" />
                                                        </div>

                                                        <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" DisplayMoney="Left"
                                                            Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                            PopupButtonID="imgFromDate" TargetControlID="txtFromDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtFromDate" 
                                                           ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" 
                                                           ErrorMessage="Invalid date format" ValidationGroup="GstTdsReport" Display="None" />
                                                        <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                            Display="None" ValidationGroup="GstTdsReport" SetFocusOnError="true" ErrorMessage="Please Select From Date">
                                                        </asp:RequiredFieldValidator>
                                                        
                                                    </div>
                                                    </div>
                                                </div>
                                          
                                          <div class="form-group col-md-12">
                                                <div class="form-group col-md-2">
                                                    <label>To Date:<span style="color: red;">*</label>
                                                    </div>
                                               <div class="form-group col-md-4">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" Text="" CssClass="form-control" ToolTip="Enter To Date"></asp:TextBox>
                                                        <div class="input-group-addon">
                                                            <asp:ImageButton ID="imgToDate" runat="server" ImageUrl="~/IMAGES/calendar.png" TabIndex="7" />
                                                        </div>
                                                        <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" DisplayMoney="Left"
                                                            Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate">
                                                        </ajaxToolKit:MaskedEditExtender>

                                                        <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgToDate"
                                                            TargetControlID="txtToDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <asp:CompareValidator ID="cmpvDate" runat="server" ErrorMessage="To Date Should be greater than or equal to  From Date"
                                                            ControlToCompare="txtFromDate" ControlToValidate="txtToDate" Display="None" ValueToCompare="Date"
                                                            Type="Date" Operator="GreaterThanEqual" ValidationGroup="GstTdsReport"></asp:CompareValidator>
                                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtToDate" 
                                                           ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" 
                                                           ErrorMessage="Invalid date format" ValidationGroup="GstTdsReport" Display="None" />
                                                        <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select To Date" ValidationGroup="GstTdsReport"></asp:RequiredFieldValidator>
                                                        
                                                       
                                                    </div>
                                                </div>
                                          
                                              </div>

                                            <div class="col-md-8 text-center">
                                                <asp:Button ID="btnshow" runat="server" Text="Show"  OnClick="btnshow_Click"
                                                    CssClass="btn btn-info" TabIndex="3" ToolTip="Click To Show Data" ValidationGroup="GstTdsReport" />
                                             
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                    CssClass="btn btn-warning" TabIndex="4" ToolTip="Click To Reset" />

                                                   <asp:Button ID="btnRpt" runat="server" Text=" Report" OnClick="btnRpt_Click"
                                                    CssClass="btn btn-info" TabIndex="3" ToolTip="Click To Show Report" ValidationGroup="GstTdsReport" />
                                               
                                                   <asp:ImageButton ID="imgbutExporttoexcel" runat="server" ToolTip="Export to excel" AlternateText="Export To Excel"  
                                                    OnClick="imgbutExporttoexcel_Click"  CssClass="btn btn-info" ValidationGroup="GstTdsReport" />
                                                    
                                                  <asp:ValidationSummary ID="vs1" runat="server" ShowMessageBox="True" ShowSummary="False"  ValidationGroup="GstTdsReport" />  
                                                <br />
                                                <br />                    
                                            </div>

                                                <div class="form-group col-md-12" id="DivGSTONTDS" runat="server" visible="false" >
                                                 <asp:Panel runat="server" ID="pnlinnovativeteaching" ScrollBars="Auto">
                                                                                        <asp:ListView ID="lvGSTONTDS" runat="server">
                                                                                            <LayoutTemplate>
                                                                                                <div id="lgv1">

                                                                                                    <table class="table table-bordered table-hover">
                                                                                                        <thead>
                                                                                                            <tr class="bg-light-blue">
                                                                                                               <th>Sr.No</th>
                                                                                                                <th>DATE
                                                                                                                </th>
                                                                                                                <th>GST NO
                                                                                                                </th>
                                                                                                                <th>
                                                                                                                    PARTY NAME
                                                                                                                </th>
                                                                                                                <th>TAXABLE AMOUNT</th>
                                                                                                                <th>
                                                                                                                    CGST
                                                                                                                </th>
                                                                                                                <th>SGST</th>
                                                                                                                <th>IGST</th>
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
                                                                                                         <asp:Label ID="Label6" runat="server" Text=' <%# Container.DataItemIndex + 1 %>' />
                                                                                                     
                                                                                                        
                                                                                                    </td>

                                                                                                    <td>
                                                                                                        <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("DATE","{0:dd/MM/yyyy}")%>' />

                                                                                                    </td>

                                                                                                    <td>
                                                                                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("GST_NO")%>' />

                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblImpactonStudent" runat="server" Text='<%# Eval("PARTY_NAME")%>' />
                                                                                                    </td>
                                                                                                   
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("TAXABLE_AMOUNT")%>' />
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("CGST_AMOUNT")%>' />
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("SGST_AMOUNT")%>' />
                                                                                                    </td>
                                                                                                     <td>
                                                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("IGST_AMOUNT")%>' />
                                                                                                    </td>
                                                                                                    
                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:ListView>



                                                      <asp:GridView ID="grdTDSExcel" runat="server" AutoGenerateColumns="false" Width="100%" >
                                                    <Columns>
                                        <asp:BoundField DataField="Sr_No" HeaderText="Sr_No" ControlStyle-Font-Size="Smaller">
                                            <HeaderStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DATE" HeaderText="DATE" ControlStyle-Font-Size="Smaller">
                                            <HeaderStyle HorizontalAlign="Left" Width="30%" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Left" Width="30%" Font-Size="Smaller" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GST_NO" HeaderText="GST NO " ControlStyle-Font-Size="Smaller">
                                            <HeaderStyle HorizontalAlign="Left" Width="40%" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="True" Width="40%" Font-Size="Smaller" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PARTY_NAME" HeaderText=" PARTY_NAME" ControlStyle-Font-Size="Smaller">
                                            <HeaderStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TAXABLE_AMOUNT" HeaderText="TAXABLE_AMOUNT" ControlStyle-Font-Size="Smaller">
                                            <HeaderStyle HorizontalAlign="Left" Width="5%" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Left" Width="5%" Font-Size="Smaller" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CGST_AMOUNT" HeaderText="CGST_AMOUNT" ControlStyle-Font-Size="Smaller">
                                            <HeaderStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SGST_AMOUNT" HeaderText="SGST_AMOUNT " ControlStyle-Font-Size="Smaller">
                                            <HeaderStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IGST_AMOUNT" HeaderText="IGST_AMOUNT" ControlStyle-Font-Size="Smaller">
                                            <HeaderStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                        </asp:BoundField>
                                       
                                       
                                    </Columns>
                                                   <HeaderStyle HorizontalAlign="Center" />
                                                         </asp:GridView>
                                                                                    </asp:Panel>


                                                </div>

                                                <div class="form-group col-md-12" id="Divgst" runat="server"  visible="false">
                                                 <asp:Panel runat="server" ID="Panel1" ScrollBars="Auto">
                                                                                        <asp:ListView ID="lvgst" runat="server">
                                                                                            <LayoutTemplate>
                                                                                                <div id="lgv1">

                                                                                                    <table class="table table-bordered table-hover">
                                                                                                        <thead>
                                                                                                            <tr class="bg-light-blue">
                                                                                                               <th>Sr.No</th>
                                                                                                                <th>DATE
                                                                                                                </th>
                                                                                                                <th> INVOICE NO</th>
                                                                                                                <th>GST NO
                                                                                                                </th>
                                                                                                                <th>
                                                                                                                    PARTY NAME
                                                                                                                </th>
                                                                                                                <th>GROSS AMOUNT</th>
                                                                                                                <th>
                                                                                                                    CGST
                                                                                                                </th>
                                                                                                                <th>SGST</th>
                                                                                                                <th>IGST</th>
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
                                                                                                       <%# Container.DataItemIndex + 1 %>
                                                                                                        
                                                                                                    </td>

                                                                                                    <td>
                                                                                                        <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("DATE")%>' />

                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("INVOICE_NO")%>' />

                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("GST_NO")%>' />

                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="lblImpactonStudent" runat="server" Text='<%# Eval("PARTY_NAME")%>' />
                                                                                                    </td>
                                                                                                   
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("BILL_AMT")%>' />
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("CGST_AMOUNT")%>' />
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("SGST_AMOUNT")%>' />
                                                                                                    </td>
                                                                                                     <td>
                                                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("IGST_AMOUNT")%>' />
                                                                                                    </td>
                                                                                                    
                                                                                                </tr>
                                                                                            </ItemTemplate>
                                                                                        </asp:ListView>

                                                                                      <asp:GridView ID="GRIDGST" runat="server" AutoGenerateColumns="false" Width="100%" >
                                                    <Columns>
                                        <asp:BoundField DataField="Sr_No" HeaderText="Sr_No" ControlStyle-Font-Size="Smaller">
                                            <HeaderStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DATE" HeaderText="DATE" ControlStyle-Font-Size="Smaller">
                                            <HeaderStyle HorizontalAlign="Left" Width="30%" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Left" Width="30%" Font-Size="Smaller" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GST_NO" HeaderText="GST NO " ControlStyle-Font-Size="Smaller">
                                            <HeaderStyle HorizontalAlign="Left" Width="40%" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="True" Width="40%" Font-Size="Smaller" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PARTY_NAME" HeaderText=" PARTY_NAME" ControlStyle-Font-Size="Smaller">
                                            <HeaderStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BILL_AMT" HeaderText="GROSS AMOUNT" ControlStyle-Font-Size="Smaller">
                                            <HeaderStyle HorizontalAlign="Left" Width="5%" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Left" Width="5%" Font-Size="Smaller" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CGST_AMOUNT" HeaderText="CGST_AMOUNT" ControlStyle-Font-Size="Smaller">
                                            <HeaderStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SGST_AMOUNT" HeaderText="SGST_AMOUNT " ControlStyle-Font-Size="Smaller">
                                            <HeaderStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IGST_AMOUNT" HeaderText="IGST_AMOUNT" ControlStyle-Font-Size="Smaller">
                                            <HeaderStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                        </asp:BoundField>
                                       
                                       
                                    </Columns>
                                                   <HeaderStyle HorizontalAlign="Center" />
                                                         </asp:GridView>
                                                                         
                                                                                    </asp:Panel>
                                                </div>


                                        </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
       

    <div>
        <asp:ValidationSummary runat="server" ID="vdReqField" DisplayMode="List" ShowMessageBox="true"
            ShowSummary="false" ValidationGroup="Store" />
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

