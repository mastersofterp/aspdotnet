<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ApprovalofEmployeeResignation.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_ApprovalofEmployeeResignation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

 

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div style="display: none">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </div>

                        <div class="box-header with-border">
                            <h3 class="box-title">APPROVAL OF EMPLOYEE RESIGNATION</h3>
                            <p class="text-center">
                            </p>
                            <div class="box-tools pull-right">
                                <asp:Button ID="btnHelp" runat="server" Visible="false" />
                            </div>
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Select Criteria</div>
                                        <div class="panel-body">
                                            <asp:Panel ID="pnlSelection" runat="server">
                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-6">
                                                        <div class="form-group col-md-7">
                                                                <label>From Date :</label>
                                                                <div class="input-group">
                                                                    <asp:TextBox ID="txtfromdate" runat="server"  onblur="return checkdate(this);" 
                                                                        ToolTip="Enter Month and Year" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                    <div class="input-group-addon">
                                                                        <asp:Image ID="ImaCalStartDate" runat="server" ImageUrl="~/images/calendar.png"
                                                                            Style="cursor: pointer" />
                                                                    </div>
                                                                    <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Enabled="true" EnableViewState="true"
                                                                        Format="dd/MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtfromdate">
                                                                    </ajaxToolKit:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="rfvtxtStartDate" runat="server" ControlToValidate="txtfromdate"
                                                                        Display="None" ErrorMessage="Please Month &amp; Year in (dd/MM/yyyy Format)" SetFocusOnError="True"
                                                                        ValidationGroup="payroll">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                    </div>
                                                     <div class="form-group col-md-6">
                                                        <div class="form-group col-md-7">
                                                                <label>To Date :</label>
                                                                <div class="input-group">
                                                                    <asp:TextBox ID="txttodate" runat="server"  onblur="return checkdate(this);" 
                                                                        ToolTip="Enter Month and Year" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                    <div class="input-group-addon">
                                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png"
                                                                            Style="cursor: pointer" />
                                                                    </div>

                                                                    <ajaxToolKit:CalendarExtender ID="cetxttoDate" runat="server" Enabled="true" EnableViewState="true"
                                                                        Format="dd/MM/yyyy" PopupButtonID="ImaCaltoDate" TargetControlID="txttodate">
                                                                    </ajaxToolKit:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="rfvtxttoDate" runat="server" ControlToValidate="txttodate"
                                                                        Display="None" ErrorMessage="Please Month &amp; Year in (dd/MM/yyyy Format)" SetFocusOnError="True"
                                                                        ValidationGroup="payroll">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                         <div class="form-group col-md-2">
                                                            <asp:Button ID="Search" runat="server" Text="Search" OnClick="Search_Click"
                                                            CssClass="btn btn-info" TabIndex="6" ToolTip="Click here to Search Employee" style="margin-top:20px;" />
                                                             </div>
                                                         
                                                    </div>
                                                    
                                                    <div class="form-group col-md-6">
                                                    </div>
                                                </div>

                                            </asp:Panel>
                                        </div>
                                    </div>

                                    <asp:Panel ID="pnlassetallotment" runat="server">
                                        <div class="table-responsive" runat="server" id="DivGrid">
                                            <asp:ListView ID="lvEmployeeresignation" runat="server" OnItemCommand="lvEmployeeresignation_ItemCommand">
                                                        <LayoutTemplate>
                                                            <div class="vista-grid">
                                                                <div class="titlebar">
                                                                    <h4>
                                                                        <label class="label label-default">Employee Resignation Details</label>
                                                                    </h4>
                                                                </div>
                                                                <table id="id1" class="table table-hover table-bordered">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            
                                                                            <th style="text-align: center;">Employee No
                                                                            </th>
                                                                            <th>Name
                                                                            </th>
                                                                            <th style="text-align: center;">Department
                                                                            </th>
                                                                            <th>
                                                                               Resignation Date
                                                                            </th>
                                                                                                                                                       
                                                                            <th>Status
                                                                            </th>
                                                                            <th>
                                                                                Notice Period in Month
                                                                            </th>
                                                                            <th>
                                                                    Accept
                                                                         </th>
                                                                            <th>
                                                                                Reject
                                                                            </th>
                                                                            <th>
                                                                                View
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
                                                            <tr class="item">
                                                                
                                                                <td>
                                                                    <%# Eval("IDNO")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("TITLE")%>
                                                                    <%# Eval("FNAME")%>
                                                                    <%# Eval("MNAME")%>
                                                                    <%# Eval("LNAME")%>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <%# Eval("SUBDEPT")%>
                                                                </td>
                                                                <td>
                                                                   <%-- <%# Eval("RESIGNATIONDATE")%>--%>
                                                                    <asp:Label ID="lblresgnationdate" runat="server" Text='<%# Eval("RESIGNATIONDATE")%>'></asp:Label>
                                                                </td>
                                                                 
                                                                <td style="text-align: center;">
                                                                    <%# Eval("REG_STATUS_DETAILS")%>
                                                                    <asp:Label ID="lblresignation_STATUS" runat="server" Text='<%# Eval("REG_STATUS")%>' Visible="false"></asp:Label>
                                                                </td> 
                                                                <td>
                                                                    <asp:TextBox ID="txtnoticeperiod" type="text" runat="server" Width="80" Text='<%#Eval("REG_NOTICE_PERIOD") %>'></asp:TextBox>
                                                                </td>
                                                                                                                                
                                                                <td style="text-align: center;">
                                                                    <asp:Button ID="btnapproved" runat="server" Text="Accept"  CommandArgument='<%# Eval("EMPRESIGNATIONID")%>' ToolTip="Approve Record" BackColor="Navy" ForeColor="White" Width="80px"  CommandName="Accept"   />
                                                                    
                                                                </td>
                                                                 <td style="text-align: center;">
                                                                    <asp:Button ID="btnrejected" runat="server" Text="Reject"  CommandArgument='<%# Eval("EMPRESIGNATIONID")%>' ToolTip="Reject Record" BackColor="Navy" ForeColor="White" Width="100px"  CommandName="Reject"  />
                                                                    
                                                                </td>
                                                                <td>
                                                                    <asp:HiddenField ID="hdnRegDate" runat="server" Value='<%# Eval("RESIGNATIONDATE") %>' />
                                                                    <asp:HiddenField ID="hdnRegRemark" runat="server" Value='<%# Eval("RESIGNATIONREMARK") %>' />
                                                                    <%-- <button type="button" class="btn btn-primary btn-flat" data-toggle="modal" data-target="#myModal">view</button></td>       --%>
                                                                    <%-- <asp:LinkButton ID="lnkFake" runat="server" Text="Resignation Reason" CommandName="View"  CommandArgument='<%# Eval("EMPRESIGNATIONID")%>'>View</asp:LinkButton>  --%>   
                                                                       <asp:Button ID="BTNVIEW" runat="server" CommandName="View" CommandArgument='<%# Eval("EMPRESIGNATIONID")%>'  OnClick="btnView_Click" BackColor="Navy" ForeColor="White"  Width="100px" Text="View"/>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                            <div class="vista-grid_datapager text-center">
                                                <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvEmployeeresignation" PageSize="10" OnPreRender="dpPager_PreRender"
                                                    >
                                                    <Fields>
                                                        <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                            ShowLastPageButton="false" ShowNextPageButton="false" />
                                                        <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                        <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                            ShowLastPageButton="true" ShowNextPageButton="true" />
                                                    </Fields>
                                                </asp:DataPager>
                                            </div>
                                        </div>
     
                                        
                                    </asp:Panel>
                                </div>
                            </div>
                            
                        </form>

                    </div>
                </div>

            </div>
              
            
            <script type="text/javascript">

                //Calculating the total amount
                function totalamount(val) {
                    if (ValidateNumeric(val)) {
                        var txtMonthlyDedAmt = document.getElementById("ctl00_ContentPlaceHolder1_txtMonthlyDedAmt");
                        var txtTotalAmount = document.getElementById("ctl00_ContentPlaceHolder1_txtTotalAmount");
                        var txtOutStandingAmt = document.getElementById("ctl00_ContentPlaceHolder1_txtOutStandingAmt");
                        var txtNoofInstPaid = document.getElementById("ctl00_ContentPlaceHolder1_txtNoofInstPaid");
                        txtTotalAmount.value = Number(val.value) * Number(txtMonthlyDedAmt.value);
                        txtNoofInstPaid.value = 0;
                        txtOutStandingAmt.value = txtTotalAmount.value;
                    }
                }


                function ValidateNumeric(txt) {
                    if (isNaN(txt.value)) {
                        txt.value = txt.value.substring(0, (txt.value.length) - 1);
                        txt.value = "";
                        txt.focus();
                        alert("Only Numeric Characters alloewd");
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            </script>
            <%--<script type="text/javascript">
               function showPopup() {
                   $('#myModal').modal('show');
                }
            </script>--%>
            
       <%-- </ContentTemplate>
       
    </asp:UpdatePanel>--%>


      <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
    <ajaxToolKit:ModalPopupExtender  ID="panel1modelextender" runat="server"  BackgroundCssClass="modalBackground"  TargetControlID="hiddenTargetControlForModalPopup"   RepositionMode="RepositionOnWindowScroll"
        PopupControlID="div" CancelControlID="btnNoDel">
    </ajaxToolKit:ModalPopupExtender>
      <asp:Panel ID="div" runat="server" Width="800px" Height="800px" CssClass="modalPopup" >
          <div id="divj" runat="server" ></div>
        <div>
            <div class="modal-content">
                <div class="modal-body">
                    <asp:Panel  GroupingText="Employee Resignation Details" BackColor="White" runat="server">

                        <table border="0" cellpadding="0" cellspacing="0" runat="server">
                            <tr>
                                <td>
                                    Resignation Date:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtresignationdate" runat="server"  Width="150px"></asp:TextBox>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Resignation Reason:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtresignationresaon" runat="server"  TextMode="MultiLine" Width="650px" Height="500px"></asp:TextBox>

                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                     
                                            
                    <div class="text-center">
                        <asp:Button ID="btnNoDel" runat="server" Text="Close" CssClass="btn-primary" TabIndex="181" Width="80px" />

                    </div>
                    
                </div>

            </div>

        </div>

    </asp:Panel>
     <%--<div id="myModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="myModalLabel">Customer Details</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="gvDetails" runat="server"></asp:GridView>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>--%>

                  

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

