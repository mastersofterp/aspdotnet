<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_MailAuthorityConfiguration.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_MailAuthorityConfiguration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
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
                             <h3 class="box-title">EMPLOYEE MAIL AUTHORITY</h3>
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
                                        <div class="panel-heading">Employee Mail Authority</div>
                                        <div class="panel-body">
                                          <%--  <asp:Panel ID="pnlSelection" runat="server">--%>
                                                   <div class="form-group col-md-12">
                                                    <div class="form-group col-md-4">
                                                         <div class="form-group col-md-7">
                                                          <label>College Name :<span style="color: Red">*</span></label>
                                                            <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="2" Width="330px"
                                                                ToolTip="Select College Name"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                             <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select College"
                                                                ValidationGroup="ADD" InitialValue="0"></asp:RequiredFieldValidator>
                                                       </div>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                        <div class="form-group col-md-7">
                                                            <label>Employee Name :<span style="color: Red">*</span></label>
                                                             <asp:TextBox ID="txtemployeename" runat="server"   Width="330px"
                                                                 ToolTip="Enter Employee Name" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                             <asp:RequiredFieldValidator ID="rfvemployee" runat="server" ControlToValidate="txtemployeename"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Enter Employee Name"
                                                                ValidationGroup="ADD" InitialValue="0"></asp:RequiredFieldValidator>       
                                                        </div>
                                                         </div>
                                                         <div class="form-group col-md-4">
                                                         <div class="form-group col-md-7">
                                                            <label>Employee Email :<span style="color: Red">*</span></label>
                                                             <asp:TextBox ID="txtemployeeemail" runat="server"   Width="330px"
                                                                 ToolTip="Enter Employe Email" CssClass="form-control" TabIndex="1"></asp:TextBox>   <%--onblur="checkEmail(this)"--%>
                                                             <asp:RequiredFieldValidator ID="rfvempemail" runat="server" ControlToValidate="txtemployeeemail"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Enter Employee Email Id"
                                                                ValidationGroup="ADD" InitialValue="0"></asp:RequiredFieldValidator> 
                                                             <asp:RegularExpressionValidator ID="rxvEmailId" runat="server" ControlToValidate="txtemployeeemail"
                                                             Display="None" ErrorMessage="Enter Email Id Correctly" SetFocusOnError="True"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="ADD"></asp:RegularExpressionValidator> 
                                                        </div>
                                                          </div>
                                                        <div class="form-group col-md-4">
                                                         <div class="form-group col-md-7">
                                                            <label>Notification Days :</label>
                                                           <asp:TextBox ID="txtnotificationdays" runat="server"   Width="330px"
                                                            ToolTip="Enter Notification Days" CssClass="form-control" TabIndex="1" onKeyUp="validateNumeric(this)"  onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                        </div>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                       <div class="form-group col-md-7">
                                                            <label>Is Active :</label><br />
                                                            <asp:CheckBox ID="chkisactive" runat="server"  AutoPostBack="true" Checked="true"/>   
                                                        </div>
                                                        </div>
                                                      </div>
                                                       <%-- <div class="form-group col-md-10">--%>
                                                      <div class="col-md-12 text-center">
                                                    <div>
                                            <asp:Button ID="btnsave" runat="server" TabIndex="5" OnClick="btnsave_Click"
                                                CssClass="btn btn-primary" Text="Save" style="margin-left:5px" ValidationGroup="ADD"></asp:Button>

                                            <asp:Button ID="btncancel" runat="server" TabIndex="5" OnClick="btncancel_Click"
                                                CssClass="btn btn-primary" Text="Cancel"></asp:Button>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="ADD" />
                                        </div>
                                           </div>
                                                    </div>
                                           <%--  </asp:Panel>--%>
                                                </div> 
                                     <asp:Panel ID="pnlSatffList" runat="server">
                                      
                                        <div class="table-responsive" runat="server" id="DivGrid">
                                            <asp:ListView ID="lvempmaillist" runat="server">
                                                <EmptyDataTemplate>
                                                    <br />
                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="" />
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <div id="demo-grid" class="vista-grid">
                                                        <div class="titlebar">
                                                            <h4>Employee Mail List</h4>
                                                        </div>
                                                        <table class="table table-bordered table-hover table-responsive">
                                                            <tr class="bg-light-blue">
                                                                <th>
                                                                    Action
                                                                </th>                                                              
                                                                <th>Employee Name
                                                                </th>
                                                                <th>
                                                                    Email
                                                                </th>
                                                                <th>
                                                                    Is Active
                                                                </th>
                                                                <th>
                                                                    Notification_Days
                                                                </th>
                                                                
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                     <td style="text-align: left;">
                                                       <asp:ImageButton ID="btneditmail" runat="server" OnClick="btneditmail_Click"
                                                        CommandArgument='<%# Eval("EMPMAILAUTHID")%>' ImageUrl="~/images/edit.gif" ToolTip="Edit Record" />
                                                       <asp:HiddenField ID="HDNCOLLEGEID" runat="server" Value=<%# Eval("CollegeId")%> />
                                                          </td>
                                                        <td>      
                                                         <%# Eval("Name")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("MailID")%>
                                                        </td>
                                                         <td>
                                                            <%# Eval("Isactive")%>
                                                        </td>
                                                         <td>
                                                           <%# Eval("Notification_Days")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>

                                            </asp:ListView>
                                            <div class="vista-grid_datapager text-center">
                                                <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvempmaillist" PageSize="10" OnPreRender="dpPager_PreRender"
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
           <script type="text/javascript" language="javascript">
                function checkEmail(event) {
                    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                    if (!re.test(event.value)) {
                        alert("Please enter a valid email address");
                        return false;
                    }
                    return true;
                }
            </script>
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
        </ContentTemplate>
       <%-- <Triggers>
            <asp:PostBackTrigger ControlID="btnReport"/>
        </Triggers>--%>
    </asp:UpdatePanel>

     <div id="divMsg" runat="server"></div>
</asp:Content>



