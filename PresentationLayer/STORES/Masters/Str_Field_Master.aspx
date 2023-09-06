<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Str_Field_Master.aspx.cs" Inherits="Stores_Masters_Str_Field_Master"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">FIELD MASTER</h3>

                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Add/Edit Field</h5>
                                </div>

                                <asp:Panel ID="pnl" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Field Name</label>

                                            </div>
                                            <asp:TextBox ID="txtFieldName" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Enter Field Name" MaxLength="50"
                                                onKeyUp="LowercaseToUppercase(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtFieldName" runat="server" ControlToValidate="txtFieldName"
                                                Display="None" ErrorMessage="Please Enter Field Name" ValidationGroup="store"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" ID="regtxtFieldName" ControlToValidate="txtFieldName"
                                                ValidationGroup="store" ValidationExpression="^[a-z|A-Z|]+[a-z|A-Z|0-9|\s]*"
                                                ErrorMessage="Enter Valid Field Name" Display="None"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Serial No.</label>

                                            </div>
                                            <asp:TextBox ID="txtsrno" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Enter Serial No" MaxLength="5"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtsrno" runat="server" ControlToValidate="txtsrno"
                                                Display="None" ErrorMessage="Please Enter Field Serial No." ValidationGroup="store"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cmptxtsrno" runat="server" ErrorMessage="Please Enter Numeric Value For Serial No."
                                                Display="None" SetFocusOnError="true" ControlToValidate="txtsrno" ValidationGroup="store"
                                                Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>

                                        </div>
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Cal on Basic Amt</label>
                                                <sup></sup>
                                            </div>
                                            <asp:CheckBox ID="chkCalOnBasicAmt" runat="server" TabIndex="6" Text="Please check if calculated on Basic amount" />
                                        </div>
                                        <div class="form-group col-lg-2 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>India / Foreign</label>
                                                <sup></sup>
                                            </div>
                                            <asp:RadioButton ID="radIndia" runat="server" TabIndex="4" GroupName="radstore" Text="India" Checked="true" />
                                            <asp:RadioButton ID="radForegin" runat="server" TabIndex="5" GroupName="radstore" Text="Foreign" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Field Type</label>

                                            </div>
                                            <asp:DropDownList ID="ddlType" runat="server" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="2" CssClass="form-control" ToolTip="Select Type">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="C">[C] -> Calculative</asp:ListItem>
                                                <asp:ListItem Value="I">[I] -> Informative</asp:ListItem>
                                                <asp:ListItem Value="P">[P] -> Percent</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlType" runat="server" ControlToValidate="ddlType"
                                                Display="None" ErrorMessage="Please Select Field Type" ValidationGroup="store"
                                                InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-2 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Add/Deduct</label>
                                                <sup></sup>
                                            </div>
                                            <asp:CheckBox ID="chkDeductTax" runat="server" TabIndex="6" />

                                        </div>
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class=" note-div">
                                                <h5 class="heading">Note</h5>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Check If The Amount To Be Deducted.</span> </p>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Uncheck If The Amount To Be Added.</span> </p>
                                            </div>
                                        </div>
                                </asp:Panel>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="butSubmit" ValidationGroup="store" Text="Submit" runat="server" CssClass="btn btn-primary" ToolTip="Click To Submit" TabIndex="7"
                                    OnClick="butSubmit_Click" />
                                <asp:Button ID="btnshowrpt" Text="Report" runat="server" CssClass="btn btn-info" ToolTip="Click To Show Report" TabIndex="8" OnClick="btnshowrpt_Click"/>
                                <asp:Button ID="butCancel" Text="Cancel" runat="server" CssClass="btn btn-warning" ToolTip="Click To Reset" TabIndex="9" OnClick="butCancel_Click" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="store"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <asp:Panel ID="pnlField" runat="server">
                                <div class="col-12">
                                    <asp:ListView ID="lvField" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>Field Details</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action
                                                            </th>
                                                            <th>Field Name
                                                            </th>
                                                            <th>Field Type
                                                            </th>
                                                            <th>Serial No.
                                                            </th>
                                                            <th>India/Foreign
                                                            </th>
                                                            <th>Cal On Basic Amt
                                                            </th>
                                                            <th>Add/Deduct
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
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("FNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                </td>
                                                <td>
                                                    <%# Eval("FNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FTYPE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FSRNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("IND_FOR")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ADDED_IN_BASIC")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TAX_DEDUCTED")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
                                    <%--<div class="vista-grid_datapager text-center">
                                        <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvField" PageSize="10"
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
                                        </asp:DataPager>
                                    </div>--%>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>


                </div>

            </div>


            </div>


            </div>

        </ContentTemplate>

          <Triggers>
            <asp:PostBackTrigger ControlID="btnshowrpt" />
        </Triggers>

    </asp:UpdatePanel>
        <div id="divMsg" runat="server"></div>

    <script type="text/javascript">
        function LowercaseToUppercase(txt) {
            var textValue = txt.value;
            txt.value = textValue.toUpperCase();

        }

    </script>
</asp:Content>
