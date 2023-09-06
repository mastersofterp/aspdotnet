<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage2.master" AutoEventWireup="true" CodeFile="Pay_ApprovalOfAssetAllotment.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_ApprovalOfAssetAllotment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div style="display: none">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </div>

                        <div class="box-header with-border">
                            <h3 class="box-title">APPROVAL OF ASSET ALLOTMENT OF EMPLOYEE </h3>
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
                                                                        Display="None" ErrorMessage="Please Month &amp; Year in ( dd/MM/yyyy Format)" SetFocusOnError="True"
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
                                                         <div class="form-group col-md-2">
                                                            <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click"
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
                                            <asp:ListView ID="lvEmpAssetallotment" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="vista-grid">
                                                                <div class="titlebar">
                                                                    <h4>
                                                                        <label class="label label-default">Asset Details</label>
                                                                    </h4>
                                                                </div>
                                                                <table id="id1" class="table table-hover table-bordered">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            
                                                                            <th style="text-align: left;">Employee No.
                                                                            </th>
                                                                            <th style="text-align: left;">Name
                                                                            </th>
                                                                             <th style="text-align: left;">Department
                                                                            </th>
                                                                             <th style="text-align: left;">
                                                                                Asset Name
                                                                            </th>
                                                                            <%--<th>Asset Remark
                                                                            </th>--%>
                                                                            <th style="text-align: left;">
                                                                               Application Date
                                                                            </th>
                                                                            <th style="text-align: left;">
                                                                                Approved Date
                                                                                
                                                                            </th>
                                                                            <th style="text-align: left;">Status
                                                                            </th>
                                                                            <th style="text-align: left;">
                                                                    Approve
                                                                         </th>
                                                                             <th style="text-align: left;">
                                                                                Not Approve
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
                                                                    <%# Eval("EmployeeId")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("TITLE")%>
                                                                    <%# Eval("FNAME")%>
                                                                    <%# Eval("MNAME")%>
                                                                    <%# Eval("LNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SUBDEPT")%>
                                                                </td>
                                                                 <td>
                                                                    <%# Eval("ASSETNAME")%>
                                                                </td>
                                                                </td>
                                                             <td>
                                                                    <%# Eval("CREATEDDATE")%>
                                                                   </td>
                                                             <td>
                                                                    <%# Eval("MODIFIEDDATE")%>
                                                              </td>
                                                                 <td style="text-align: center;">
                                                                    <%# Eval("ISAPPROVED_DETAILS")%>
                                                                    <asp:Label ID="lblISAPPROVED_STATUS" runat="server" Text='<%# Eval("ISAPPROVED")%>' Visible="false"></asp:Label>
                                                                   <asp:HiddenField ID="hdnidno" runat="server" Value='<%--<%# Eval("IDNO")%>' />
                                                                      
                                                                  </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Button ID="btnapproved" runat="server" Text="Approve"  CommandArgument='<%# Eval("ASSETALLOTID")%>' ToolTip="Approve Record" Width="80px" OnClick="btnapproved_Click"  CssClass="btn btn-primary btn-sm" />
                                                                    
                                                                </td>
                                                                 <td style="text-align: center;">
                                                                    <asp:Button ID="btnnotapproved" runat="server" Text="Not Approve"  CommandArgument='<%# Eval("ASSETALLOTID")%>' ToolTip="Not Approve Record" Width="100px" OnClick="btnnotapproved_Click"  CssClass="btn btn-primary btn-sm" />
                                                                    
                                                                </td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>


                                            <div class="vista-grid_datapager text-center">
                                                <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvEmpAssetallotment" PageSize="30" OnPreRender="dpPager_PreRender"
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
                //  keeps track of the delete button for the row
                //  that is going to be removed
                var _source;
                // keep track of the popup div
                var _popup;

                function showConfirmDel(source) {
                    debugger;
                    this._source = source;
                    this._popup = $find('mdlPopupDel');

                    //  find the confirm ModalPopup and show it    
                    $find('mdlPopupDel').show();
                }

                function okDelClick() {
                    //  find the confirm ModalPopup and hide it    
                    this._popup.hide();
                    //  use the cached button as the postback source
                    __doPostBack(this._source.name, '');
                }

                function cancelDelClick() {
                    //  find the confirm ModalPopup and hide it 
                    this._popup.hide();
                    //  clear the event source
                    this._source = null;
                    this._popup = null;
                }
            </script>


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
               
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport"/>
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

