<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="FuelSuppilerMaster.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Master_FuelSuppilerMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
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

        function Clear() {
            document.getElementById('<%=txtSuppilerAddress.ClientID%>').value = '';
            document.getElementById('<%=txtSuppilerCntNo.ClientID%>').value = '';
            document.getElementById('<%=txtSuppilerContactPerson.ClientID%>').value = '';
            document.getElementById('<%=txtSuppilerName.ClientID%>').value = '';


        }
    </script>
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
                            <h3 class="box-title">FUEL SUPPLIER MASTER</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Fuel Supplier Name</label>
                                            </div>
                                            <asp:TextBox ID="txtSuppilerName" runat="server" MaxLength="50" CssClass="form-control"
                                                ToolTip="Fuel Enter Supplier Name" TabIndex="1"
                                                onkeypress="return CheckAlphabet(event, this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSuppilerName" runat="server" SetFocusOnError="true" Display="None"
                                                ErrorMessage="Please Enter Fuel Supplier Name."
                                                ValidationGroup="Submit" ControlToValidate="txtSuppilerName"></asp:RequiredFieldValidator>
                                            <asp:HiddenField ID="hdnViewstate" runat="server" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Fuel Supplier Contact No. </label>
                                            </div>
                                            <asp:TextBox ID="txtSuppilerCntNo" runat="server" MaxLength="15" CssClass="form-control"
                                                ToolTip="Enter Fuel Supplier Contact Number" TabIndex="2">
                                            </asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers"
                                                ValidChars="" TargetControlID="txtSuppilerCntNo">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvDrvrCont" ValidationGroup="Submit" ControlToValidate="txtSuppilerCntNo"
                                                Display="None" ErrorMessage="Please Enter Fuel Supplier Contact No." SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Contact Person </label>
                                            </div>
                                            <asp:TextBox ID="txtSuppilerContactPerson" runat="server" MaxLength="50" ToolTip="Fuel Enter Contact Person Name"
                                                CssClass="form-control" onkeypress="return CheckAlphabet(event, this);" TabIndex="3"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Fuel Supplier Address  </label>
                                            </div>
                                            <asp:TextBox ID="txtSuppilerAddress" TextMode="MultiLine" runat="server" MaxLength="500"
                                                CssClass="form-control" ToolTip="Enter Fuel Supplier Address" TabIndex="5"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDrvrAdd1" Display="None" runat="server" SetFocusOnError="true"
                                                ValidationGroup="Submit" ErrorMessage="Please Enter Fuel Supplier Address."
                                                ControlToValidate="txtSuppilerAddress"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click"
                                        CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" TabIndex="6" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="7"
                                        CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Submit" />

                                </div>
                                <div class="col-12 mt-3">
                                    <asp:Panel ID="pnlList" runat="server">
                                        <asp:ListView ID="lvSuppiler" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading">
                                                        <h5>Fuel Supplier Entry List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>EDIT
                                                                </th>
                                                                <th>FUEL SUPPLIER NAME
                                                                </th>
                                                                <th>FUEL SUPPLIER CONTACT NO.
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
                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                            CommandArgument='<%# Eval("FUEL_SUPPILER_ID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                            OnClick="btnEdit_Click" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("FUEL_SUPPILER_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("FUEL_CONTACT_NUMBER")%>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

