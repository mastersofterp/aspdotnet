<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_ResignationPassingAuthority.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_ResignationPassingAuthority" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div style="display: none">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </div>

                        <div class="box-header with-border">
                           
                             <h3 class="box-title">EMPLOYEE RESIGNATION PASSING PASS AUTHORITY</h3>
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
                                        <div class="panel-heading">Employee Resignation Passing Pass Authority</div>
                                        <div class="panel-body">
                                            <asp:Panel ID="pnlSelection" runat="server">
                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-6">
                                                        <div class="form-group col-md-10">
                                                               
                                                            <label>College Name:<span style="color: Red">*</span></label>
                                                               
                                                                   <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College" AppendDataBoundItems="true" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                                                  TabIndex="2">
                                                                  <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                              </asp:DropDownList>
                                                              <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                                                  ValidationGroup="ADD" ErrorMessage="Please Select College" SetFocusOnError="true"
                                                                  InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                               
                                                            </div>
                                                        <div class="form-group col-md-10">
                                                            <label>Passing Pass Name :<span style="color: Red">*</span></label>
                                                             <asp:TextBox ID="txtpassingpassname" runat="server" 
                                                                        ToolTip="Enter Passing Pass Name" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                  
                                                               <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="txtpassingpassname"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Enter Passing Pass Name"
                                                                ValidationGroup="ADD" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-10">
                                                            <label>Passing Pass Type :<span style="color: Red">*</span></label>
                                                             <asp:DropDownList ID="ddlpassingpasstype" runat="server" CssClass="form-control" ToolTip="Select Passing Pass Type" AppendDataBoundItems="true"
                                                                  AutoPostBack="true" TabIndex="2">
                                                                  <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                  <asp:ListItem Value="1">HOD</asp:ListItem>
                                                                  <asp:ListItem Value="2">Principal</asp:ListItem>
                                                                  <asp:ListItem Value="3">Vice Chancellor</asp:ListItem>
                                                              </asp:DropDownList>
                                                                  
                                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlpassingpasstype"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Enter Passing Pass Type"
                                                                ValidationGroup="ADD" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-10">
                                                            <label>User Name :<span style="color: Red">*</span></label>
                                                             <asp:DropDownList ID="ddlusername" runat="server" CssClass="form-control" ToolTip="Select User Name" AppendDataBoundItems="true" 
                                                                  AutoPostBack="true" TabIndex="2">
                                                                  <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                              </asp:DropDownList>
                                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlusername"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Enter User Name"
                                                                ValidationGroup="ADD" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-10">
                                                            <div>
                                            <asp:Button ID="btnsave" runat="server" TabIndex="5"  OnClick="btnsave_Click"
                                                CssClass="btn btn-primary" Text="Save" style="margin-left:5px" ></asp:Button>

                                            <asp:Button ID="btncancel" runat="server" TabIndex="5" OnClick="btncancel_Click"
                                                CssClass="btn btn-primary" Text="Cancel"></asp:Button>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="ADD" />
                                        </div>
                                                            </div>

                                                    </div>
                                                    
                                                </div>

                                            </asp:Panel>


                                        </div>
                                    </div>
                                    <asp:Panel ID="pnlSatffList" runat="server">
                                        <%--<div class="text-center">
                                            <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" ToolTip="Click Add New To Enter Installment Details" CssClass="btn btn-primary" Text="Add New" TabIndex="4"
                                                ValidationGroup="ADD"  OnClick="btnAdd_Click" Visible="false"></asp:LinkButton>
                                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Report"
                                                        CssClass="btn btn-warning" TabIndex="14" ToolTip="Click To Report"  Visible="false" />
                                            <asp:ValidationSummary ID="Vadd" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="ADD" />
                                        </div>--%>
                                        <div class="table-responsive" runat="server" id="DivGrid">
                                            <asp:ListView ID="lvResignationPassAuthName" runat="server">
                                                <EmptyDataTemplate>
                                                    <br />
                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="" />
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <div id="demo-grid" class="vista-grid">
                                                        <div class="titlebar">
                                                            <h4>Employee Resignation Passing Name</h4>
                                                        </div>
                                                        <table class="table table-bordered table-hover table-responsive">
                                                            <tr class="bg-light-blue">
                                                                <th>
                                                                    Action
                                                                </th>
                                                               <th>Passing Pass Name</th>
                                                                <th>
                                                                    Passing Pass Type
                                                                </th>
                                                                <th>
                                                                    User Name
                                                                </th>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                 <td style="text-align: left;">
                                                     <asp:ImageButton ID="btneditDOC" runat="server"  OnClick="btneditDOC_Click"
                                                        CommandArgument='<%# Eval("REGPASSID")%>' ImageUrl="~/images/edit.gif" ToolTip="Edit Record" />
                                                  </td>
                                                        <td>      
                                                         <%# Eval("PANAME")%>
                                                          <asp:HiddenField runat="server" ID="hdncollegeno" Value='<%# Eval("COLLEGE_NO")%>' />
                                                    <asp:HiddenField runat="server" ID="hdnptype" Value='<%# Eval("PASSTYPE")%>' />
                                                        </td>
                                                        <td>
                                                         <%# Eval("PASSTYPE")%> 
                                                        </td>
                                                        <td>
                                                        <%# Eval("UA_FULLNAME")%> 
                                                        </td>
                                                       
                                                    </tr>
                                                </ItemTemplate>

                                            </asp:ListView>
                                            <div class="vista-grid_datapager text-center">
                                                <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvResignationPassAuthName" PageSize="10" OnPreRender="dpPager_PreRender"
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
       <%-- <Triggers>
            <asp:PostBackTrigger ControlID="btnReport"/>
        </Triggers>--%>
    </asp:UpdatePanel>

     <div id="divMsg" runat="server"></div>
</asp:Content>



