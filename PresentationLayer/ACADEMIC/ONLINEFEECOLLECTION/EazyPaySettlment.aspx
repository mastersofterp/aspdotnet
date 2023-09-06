<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EazyPaySettlment.aspx.cs" Inherits="ACADEMIC_ONLINEFEECOLLECTION_EazyPaySettlment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">   
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
                                <asp:Label ID="lblDynamicPageTitleEazyPaySettelment" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12" id="pnlSelection" runat="server">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" >
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date:</label>
                                        </div>
                                        <asp:TextBox ID="txtFromDate" runat="server" TabIndex="2" onkeydown="javascript:preventInput(event);"></asp:TextBox>

                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy"
                                            TargetControlID="txtFromDate" PopupButtonID="imgCalStartDate" Enabled="true"
                                            EnableViewState="true">
                                        </ajaxtoolkit:calendarextender>   
                                        </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date</label>
                                            <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" onkeydown="javascript:preventInput(event);"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="imgCalStartDate" Enabled="true"
                                                EnableViewState="true">
                                        </ajaxtoolkit:calendarextender>   
                                          </div>
                                    </div>                                    
                            </div>                                 
                                    <div class="form-group col-lg-3 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                         <asp:Button ID="btnShow" runat="server" CssClass="btn btn-info" Text="Show" OnClick="btnShow_Click"  />
                                          <asp:Button ID="btnPrintReport" runat="server" CssClass="btn btn-info" Text="Print Report" OnClick="btnPrintReport_Click"   />
                                    </div>

                         <div class="col-md-12">
                               <asp:Panel ID="pnllvSh" runat="server" Visible="false">
                                <asp:ListView ID="lvReport" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div id="demo-grid">
                                            <div class="table-responsive" style="height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tbllist">
                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                        <tr>                                                          
                                                            <th>Reference No</th>
                                                            <th>SubMerchant Id</th>
                                                            <th>PGAmount</th>
                                                            <th>Applicant Name</th>
                                                            <th>Application ID</th>
                                                            <th>Application Type</th>
                                                            <th>Transaction ID</th>
                                                            <th>Transaction date</th>
                                                            <th>Mode of Payment</th>                                                                                                               
                                                             <th>STATUS</th>                                                                                                                         
                                                             <th>Base Amount</th>
                                                            <th>Processing Fee</th>
                                                            <th>Recon Date</th>
                                                            <th>Settlement Date</th>
                                                            <th>Transaction Amount</th>
                                                            <th>Transaction Initiated On</th>                                                                                                                       
                                                             <th>Mobile No</th>
                                                             <th>Email Id</th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                         
                                            <td><%# Eval("Reference No") %></td>
                                            <td><%# Eval("SubMerchant Id") %></td>
                                            <td><%# Eval("PGAmount") %></td>
                                            <td><%# Eval("Applicant Name") %></td>
                                            <td><%# Eval("Application ID") %></td> 
                                            <td><%# Eval("Application Type") %></td> 
                                            <td><%# Eval("Transaction ID") %></td> 
                                            <td><%# Eval("Transaction date") %></td> 
                                            <td><%# Eval("Mode of Payment") %></td> 
                                            <td><%# Eval("Status") %></td> 
                                            <td><%# Eval("Base Amount") %></td> 
                                            <td><%# Eval("Processing Fee") %></td> 
                                            <td><%# Eval("Recon Date") %></td> 
                                            <td><%# Eval("Settlement Date") %></td> 
                                            <td><%# Eval("Transaction Amount") %></td> 
                                            <td><%# Eval("Transaction Initiated On") %></td> 
                                            <td><%# Eval("Mobile No") %></td> 
                                            <td><%# Eval("Email Id") %></td>                                           
                                            
                                                
                                        <%--  <asp:HiddenField ID="hdnUserNo" Visible="false" runat="server" Value='<%# Eval("USERNO") %>' />
                                            <asp:HiddenField ID="hdnACNO" Visible="false" runat="server" Value='<%# Eval("ACNO") %>' />
                                            <asp:HiddenField ID="hdnAttendanceNO" Visible="false" runat="server" Value='<%# Eval("ATTENDANCE_NO") %>' />
                                           <td> <asp:Label ID="lblStatus" runat="server" Text='<%#  Convert.ToString(Eval("IsAttend"))==""? "-" : Convert.ToString(Eval("IsAttend"))=="True"? "Present": "Absent" %>'                                              
                                               ForeColor='<%# Convert.ToString(Eval("IsAttend"))=="True"?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label></td> 
                                          --%> 

                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                                   </asp:Panel>
                            </div>
                                </div>
                            </div>                            
                    </div>
                </div>
            </div>
            </div>
           
        </ContentTemplate>
        <Triggers>
              <asp:PostBackTrigger ControlID="btnPrintReport" />
        </Triggers>
    </asp:UpdatePanel>   
</asp:Content>

