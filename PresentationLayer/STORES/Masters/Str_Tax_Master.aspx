<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_Tax_Master.aspx.cs" Inherits="STORES_Masters_Str_Tax_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function ValidNumeric() {

            var charCode = (event.which) ? event.which : event.keyCode;
            if (charCode >= 48 && charCode <= 57)
            { return true; }
            else
            {
                alert('Please Enter Numeric Value.');
                return false;
            }
        }
    </script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlMain"
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
    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TAX MASTER</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="spanName" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Tax Name</label>
                                            </div>
                                            <asp:TextBox ID="txtTaxName" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Enter Tax Name" MaxLength="50"
                                                onKeyUp="LowercaseToUppercase(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtFieldName" runat="server" ControlToValidate="txtTaxName"
                                                Display="None" ErrorMessage="Please Enter Tax Name" ValidationGroup="store"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <%-- <asp:RegularExpressionValidator runat="server" ID="regtxtFieldName" ControlToValidate="txtTaxName"
                                                                    ValidationGroup="store" ValidationExpression="^[a-z|A-Z|]+[a-z|A-Z|0-9|\s]*"
                                                                    ErrorMessage="Enter Valid Tax Name" Display="None"></asp:RegularExpressionValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="spanCode" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Tax Code</label>
                                            </div>
                                            <asp:TextBox ID="txtTaxCode" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Enter Tax Code" MaxLength="10"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="RfvtxtTaxCode" runat="server" ControlToValidate="txtTaxCode"
                                                Display="None" ErrorMessage="Please Enter Tax Code" ValidationGroup="store"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSerNo" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Serial No</label>
                                            </div>
                                            <asp:TextBox ID="txtsrno" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Enter Serial No" onkeypress="return ValidNumeric()"></asp:TextBox>
                                          
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Is State Tax</label>
                                            </div>
                                            <asp:RadioButtonList ID="rbIsStateTax" runat="server" RepeatDirection="Horizontal" TabIndex="4" AutoPostBack="true">
                                                <asp:ListItem Value="Y" Selected="True">Yes</asp:ListItem>
                                                <asp:ListItem Value="N">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="spanType" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Is Percentage</label>
                                            </div>
                                            <asp:RadioButtonList ID="rblIsPercentage" runat="server" RepeatDirection="Horizontal" TabIndex="5" AutoPostBack="true" OnSelectedIndexChanged="rblIsPercentage_SelectedIndexChanged">
                                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rblIsPercentage"
                                                Display="None" ErrorMessage="Please Select Percent" ValidationGroup="store"
                                                InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divCalOnBasic" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Cal on Basic Amt</label>
                                            </div>
                                            <asp:CheckBox ID="chkCalOnBasicAmt" runat="server" TabIndex="6" Text="Please check if calculated on Basic amount" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="TaxPer">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Tax Percentage(%)</label>
                                            </div>
                                            <asp:TextBox ID="txtTaxPer" runat="server" CssClass="form-control" TabIndex="7" ToolTip="Enter Tax Percentage(%)"  MaxLength="5" onkeypress="return CheckFloat(event,this);"></asp:TextBox><%--onkeypress="return ValidNumeric()"--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTaxPer"
                                                Display="None" ErrorMessage="Please Enter Tax Percentage." ValidationGroup="store"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                           <%--  <asp:CompareValidator ID="cmptxtPercent" runat="server" ErrorMessage="Percentage (%) should be Less than or Equal to 100 ." ValidationGroup="store" Display="None" ControlToValidate="txtTaxPer" Operator="DataTypeCheck" Type="Double" ></asp:CompareValidator>--%>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Percentage (%) should be Less than or Equal to 100 "
                                                Display="None" SetFocusOnError="true" ControlToValidate="txtTaxPer" ValidationGroup="store"
                                                Operator="LessThanEqual" Type="Double" ValueToCompare="100"></asp:CompareValidator>
                                            
                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="butSubmit" ValidationGroup="store" Text="Submit" runat="server" CssClass="btn btn-primary" ToolTip="Click To Submit" TabIndex="8" OnClick="butSubmit_Click" />
                                <asp:Button ID="btnshowrpt" Text="Report" runat="server" CssClass="btn btn-info" ToolTip="Click To Show Report" TabIndex="10" OnClick="btnshowrpt_Click" />
                                <asp:Button ID="butCancel" Text="Cancel" runat="server" CssClass="btn btn-warning" ToolTip="Click To Reset" TabIndex="9" OnClick="butCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="store"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <asp:Panel ID="pnlField" runat="server">
                                <div class="col-12">
                                    <asp:ListView ID="lvField" runat="server">
                                        <EmptyDataTemplate>

                                            <center>
                                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                            </center>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div id="demo-grid" class="vista-grid">
                                                <div class="sub-heading"><h5>Tax Entry List</h5></div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action
                                                            </th>
                                                            <th>Tax Name
                                                            </th>
                                                            <th>Tax Code
                                                            </th>
                                                            <th>Tax Percentage(%)
                                                            </th>
                                                            <th>State Tax
                                                            </th>
                                                            <th id="thCalOnBasic" runat="server" visible="false">Cal On Basic Amt
                                                            </th>
                                                        </tr>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                        
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                               
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("TAXID") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                </td>
                                                <td>
                                                    <%# Eval("TAX_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TAX_CODE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TAX_PER")%>
                                                </td>
                                                <td>
                                                    <%# Eval("IS_STATE_TAX")%>
                                                </td>
                                                <td  id="tdCalOnBasic" runat="server" visible="false">
                                                    <%# Eval("CAL_ON_BASIC")%>
                                                </td>

                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
                                    <div class="vista-grid_datapager text-center">
                                       <%-- <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvField" PageSize="10"
                                            OnPreRender="dpPager_PreRender">
                                            <Fields>
                                                <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                                <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                                            </Fields>
                                        </asp:DataPager>--%>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                   
                </div>

            </div>
            <script type="text/javascript" language="javascript">
                // Move an element directly on top of another element (and optionally
                // make it the same size)
                function Cover(bottom, top, ignoreSize) {
                    var location = Sys.UI.DomElement.getLocation(bottom);
                    top.style.position = 'absolute';
                    top.style.top = location.y + 'px';
                    top.style.left = location.x + 'px';
                    if (!ignoreSize) {
                        top.style.height = bottom.offsetHeight + 'px';
                        top.style.width = bottom.offsetWidth + 'px';
                    }
                }

                function CheckFloat(key, object) {
                    var keycode = (key.which) ? key.which : key.keyCode;
                    //comparing pressed keycodes
                    if (!(keycode == 8 || keycode == 9 || keycode == 46) && (keycode < 48 || keycode > 57)) {
                        return false;
                    }
                    else {
                        var parts = object.value.split('.');
                        if (parts.length > 1 && keycode == 46) {
                            return false;
                        }
                        return true;
                    }
                }

            </script>

          
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnshowrpt" />
        </Triggers>

    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>
</asp:Content>

