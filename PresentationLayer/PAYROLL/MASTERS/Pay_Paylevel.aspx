<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_Paylevel.aspx.cs" Inherits="PAYROLL_MASTERS_Pay_Paylevel" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PAY LEVEL</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12" visible="false">
                                <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-8 col-md-12 col-12">
                                        <asp:Panel ID="pnlSelect" runat="server">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Staff</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        TabIndex="1">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                                        Display="None" ErrorMessage="Select Staff" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Pay Level</label>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtpayLevel" CssClass="form-control" MaxLength="10"
                                                        TabIndex="2"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtpayLevel"
                                                        Display="None" ErrorMessage="Please Enter Pay Level" SetFocusOnError="true" ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Pay Level Sr. No.</label>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtPaylevelSrno" CssClass="form-control" MaxLength="10"
                                                        TabIndex="3"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPaylevelSrno"
                                                        Display="None" ErrorMessage="Please Enter Pay Level Sr. No." SetFocusOnError="true"
                                                        ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>TA Formula</label>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtTAFarmula" CssClass="form-control" MaxLength="25"
                                                        TabIndex="4"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTAFarmula"
                                                        Display="None" ErrorMessage="Please Enter  TA Formula" SetFocusOnError="true"
                                                        ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:HiddenField runat="server" ID="hdnpayLevelId" />
                                                <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll" CssClass="btn btn-primary"
                                                    OnClick="btnSub_Click" TabIndex="5" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="6" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>

                                            <div class="col-12">
                                                <asp:ListView ID="LstEdit" runat="server">
                                                    <EmptyDataTemplate>
                                                        <center>
                                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" /></center>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>PF Loan</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Edit
                                                                    </th>
                                                                    <th>Staff Name
                                                                    </th>
                                                                    <th>Pay level Name
                                                                    </th>
                                                                    <th>Pay level Sr. No.
                                                                    </th>
                                                                    <th>TA Formula
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
                                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("Paylevel_No")%>'
                                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                            </td>
                                                            <td>
                                                                <%#Eval("STAFF")%>
                                                            </td>
                                                            <td>
                                                                <%#Eval("Paylevel_Name")%>
                                                            </td>
                                                            <td>
                                                                <%#Eval("Paylevel_Srno")%>
                                                            </td>
                                                            <td>
                                                                <%#Eval("TA_Formula")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("Paylevel_No")%>'
                                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                            </td>
                                                            <td>
                                                                <%#Eval("STAFF")%>
                                                            </td>
                                                            <td>
                                                                <%#Eval("Paylevel_Name")%>
                                                            </td>
                                                            <td>
                                                                <%#Eval("Paylevel_Srno")%>
                                                            </td>
                                                            <td>
                                                                <%#Eval("TA_Formula")%>
                                                            </td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                </asp:ListView>
                                            </div>

                                        </asp:Panel>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-12 col-12">
                                        <asp:Panel ID="pnlAttendance" runat="server">
                                            <asp:ListView ID="lvCEllno" runat="server">
                                                <EmptyDataTemplate>
                                                    <center>
                                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" /></center>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Enter Cell Number Info</h5>
                                                    </div>
                                                    <div class="table-responsive" style="height: 450px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                            <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                <tr>
                                                                    <th>Cell Number
                                                                    </th>
                                                                    <th>Amount
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
                                                            <asp:Label runat="server" ID="lblCellno" Text='<%#Eval("CellNo")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPayLevelAmount" runat="server" MaxLength="15" Text='<%#Eval("Amount")%>'
                                                                CssClass="form-control" onKeyUp="return validateNumeric(this)" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblCellno" Text='<%#Eval("CellNo")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPayLevelAmount" runat="server" MaxLength="15" Text='<%#Eval("Amount")%>'
                                                                CssClass="form-control" onKeyUp="return validateNumeric(this)" />
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                            <div class="vista-grid_datapager">
                                                <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvCEllno"
                                                    PageSize="50">
                                                </asp:DataPager>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

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
</asp:Content>


