<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="VehicleTypeMaster.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Master_VehicleTypeMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">VEHICLE TYPE MASTER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Create Vehicle Type</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Vehicle Type Name</label>
                                        </div>
                                        <asp:TextBox ID="txtVTName" runat="server" MaxLength="50" CssClass="form-control"
                                            ToolTip="Enter Vehicle Type Name" TabIndex="1"
                                            onkeypress="return CheckAlphaNumeric(event, this);"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeVTName" runat="server"
                                            FilterType="Custom,LowerCaseLetters,UpperCaseLetters, Numbers" TargetControlID="txtVTName" ValidChars=" ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvVTName" runat="server" SetFocusOnError="true" Display="None"
                                            ErrorMessage="Please Enter Vehicle Type Name."
                                            ValidationGroup="Submit" ControlToValidate="txtVTName"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Route Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlRType" runat="server" AppendDataBoundItems="true"
                                            CssClass="form-control" data-select2-enable="true" ToolTip="Select Route Type" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Open</asp:ListItem>
                                            <asp:ListItem Value="2">Fixed</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSeqNo" ValidationGroup="Submit" ControlToValidate="ddlRType" InitialValue="0"
                                            Display="None" ErrorMessage="Please Select Route Type." SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="3"
                                    OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="4"
                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" CausesValidation="false" />
                                <%-- &nbsp;<asp:Button ID="btnRport" runat="server" Text="Report" TabIndex="5"   Width="80px" onclick="btnRport_Click" Visible="false" /> --%>
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvVType" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Vehicle Type List</h5>
                                                </div>
                                                <%--<table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">--%>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>EDIT
                                                            </th>
                                                            <th>VEHICLE TYPE NAME
                                                            </th>
                                                            <th>ROUTE TYPE NAME
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                          <tr id="itemPlaceholder" runat="server" />
                                                    
                                                    </tbody>
                                                   <%--   <tr id="Tr1" runat="server" />--%>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                        CommandArgument='<%# Eval("VTID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <%# Eval("VTNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ROUTE_TYPE_NO")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
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
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Alphabets allowed!");
                return false;
            }
            else
                return true;
        }
    </script>
</asp:Content>

