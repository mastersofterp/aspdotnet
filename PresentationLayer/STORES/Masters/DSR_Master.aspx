<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="DSR_Master.aspx.cs" Inherits="Stores_Masters_DSR_Master" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
                            <h3 class="box-title">DEAD STOCK MASTER</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading"><h5>Add/Edit DSR</h5></div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="spanDSRNo" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>DSR NO.</label>
                                            </div>
                                            <asp:TextBox ID="txtDRNO" runat="server" CssClass="form-control" ToolTip="Enter DSR No" MaxLength="20" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtDRNO" runat="server" ControlToValidate="txtDRNO"
                                                Display="None" ErrorMessage="Please Enter DSR NO." ValidationGroup="store" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <%-- <asp:CompareValidator ID="cmptxtValue" runat="server" Display="None" ErrorMessage="Enter Only Numeric Value for DSR No."
                                                ControlToValidate="txtDRNO" Type="Integer" Operator="DataTypeCheck" ValidationGroup="store"></asp:CompareValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="spanGrant" runat="server">
                                            <div class="label-dynamic">
                                                <label>Grant</label>
                                            </div>
                                            <asp:DropDownList ID="ddlGrand" CssClass="form-control" data-select2-enable="true" ToolTip="Select Grant" AppendDataBoundItems="true"
                                                runat="server" TabIndex="2">
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="rfvddlGrand" runat="server" ControlToValidate="ddlGrand"
                                                Display="None" ErrorMessage="Please Select Grant" ValidationGroup="store" SetFocusOnError="True"
                                                InitialValue="0"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="spanShortName" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>DSR Short Name</label>
                                            </div>
                                            <asp:TextBox ID="txtDsrShortName" runat="server" CssClass="form-control" ToolTip="Enter DSR Short Name" MaxLength="30"
                                                onKeyUp="LovercaseToUppercase(this);" TabIndex="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtDsrShortName" runat="server" ControlToValidate="txtDsrShortName"
                                                Display="None" ErrorMessage="Please Enter DSR Short Name." ValidationGroup="store"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="spanDept" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDepartment" CssClass="form-control" data-select2-enable="true" ToolTip="Select Department" AppendDataBoundItems="true"
                                                runat="server" TabIndex="4">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlDepartment" runat="server" ControlToValidate="ddlDepartment"
                                                Display="None" ErrorMessage="Please Select Department Name" ValidationGroup="store"
                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="spanName" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>DSR Name</label>
                                            </div>
                                            <asp:TextBox ID="txtDSRName" runat="server" CssClass="form-control" ToolTip="Enter DSR Name" MaxLength="100"
                                                onKeyUp="LovercaseToUppercase(this);" TabIndex="5"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtDSRName" runat="server" ControlToValidate="txtDSRName"
                                                Display="None" ErrorMessage="Please Enter DSR Name." ValidationGroup="store"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="spanYear" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>DSR Year</label>
                                            </div>
                                            <asp:DropDownList ID="ddlyear" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select DSR Year" AppendDataBoundItems="true"
                                                AutoPostBack="true" TabIndex="6">
                                                <asp:ListItem Selected="True" Enabled="true" Text="Please select" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                              <asp:RequiredFieldValidator ID="rfvtxtdsryear" runat="server" ControlToValidate="ddlyear"
                                                Display="None" ErrorMessage="Please Select DSR Year." InitialValue="0" ValidationGroup="store"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="butSubmit" ValidationGroup="store" Text="Submit" runat="server" ToolTip="Click To submit" CssClass="btn btn-primary"
                                    OnClick="butSubmit_Click" TabIndex="7" />
                                <asp:Button ID="btnshowrpt" Text="Report" runat="server" Visible="false" ToolTip="Click To Show Report" CssClass="btn btn-info"
                                    OnClick="btnshowrpt_Click" TabIndex="8" />
                                <asp:Button ID="Button1" Text="Report" runat="server" ToolTip="Click To Show Report" CssClass="btn btn-info"
                                    OnClick="Button1_Click" TabIndex="8" />
                                <asp:Button ID="butCancel" Text="Cancel" runat="server" ToolTip="Click To Reset" CssClass="btn btn-warning"
                                    OnClick="butCancel_Click" TabIndex="9" />
                                
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="store"  ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <asp:Panel ID="pnlDeptUser" runat="server">
                                <div class="col-12">
                                    <asp:ListView ID="lvDSR" runat="server" OnPreRender="lvDSR_PreRender">
                                        <EmptyDataTemplate>
                                            <center>
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                            </center>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Dead Stock Registration</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Department
                                                        </th>
                                                        <%-- <th>Grant
                                                        </th>
                                                        <th>DSR Year
                                                        </th>--%>
                                                        <th>DSR Name
                                                        </th>
                                                        <th>DSR Short Name
                                                        </th>
                                                        <th>DSR NO.
                                                        </th>
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("DSTKNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="10"/>&nbsp;
                                                </td>
                                                <td>
                                                    <%# Eval("MDNAME")%>
                                                </td>
                                                <%-- <td>
                                                    <%# Eval("GRAND_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DSTK_YEAR")%>
                                                </td>--%>
                                                <td>
                                                    <%# Eval("DSTK_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DSTK_SHORT_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DRNO")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
                                    <%--<div class="vista-grid_datapager text-center">
                                        <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvDSR" PageSize="5" OnPreRender="dpPager_PreRender">
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnshowrpt" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>
    <script type="text/javascript">
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

        function LovercaseToUppercase(txt) {
            var textValue = txt.value;
            txt.value = textValue.toUpperCase();

        }
    </script>
</asp:Content>
