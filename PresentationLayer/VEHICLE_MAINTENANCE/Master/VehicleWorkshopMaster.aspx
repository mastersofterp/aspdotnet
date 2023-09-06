<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="VehicleWorkshopMaster.aspx.cs"
    Inherits="VEHICLE_MAINTENANCE_Master_VehicleWorkshopMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">VEHICLE WORKSHOP MASTER</h3>
                </div>
                <div class="box-body">
                    <asp:UpdatePanel ID="updActivity" runat="server">
                        <ContentTemplate>
                            <div class="col-12">
                               <%-- <div class="sub-heading">Vehicle WorkShop Details</div>--%>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Workshop Name</label>
                                        </div>
                                        <asp:TextBox ID="txtWorkshopName" runat="server" MaxLength="50" CssClass="form-control" TabIndex="1"
                                            ToolTip="Enter Workshop Name" onkeypress="return CheckAlphabet(event, this);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvWorkshopName" runat="server" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Enter Workshop Name."
                                            ValidationGroup="Submit" ControlToValidate="txtWorkshopName"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Workshop Contact No.</label>
                                        </div>
                                        <asp:TextBox ID="txtWorkshopContactNo" runat="server" MaxLength="12" CssClass="form-control"
                                            ToolTip="Enter Workshop Contact Number" TabIndex="2">
                                        </asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers"
                                            ValidChars="+- " TargetControlID="txtWorkshopContactNo">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvDrvrCont" ValidationGroup="Submit" ControlToValidate="txtWorkshopContactNo"
                                            Display="None" ErrorMessage="Please Enter Workshop Contact No." SetFocusOnError="true" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Contact Person</label>
                                        </div>
                                        <asp:TextBox ID="txtWorkshopContactPerson" runat="server" MaxLength="40" CssClass="form-control"
                                            onkeypress="return CheckAlphabet(event, this);" ToolTip="Enter Contact Person" TabIndex="3"></asp:TextBox>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Workshop Address </label>
                                        </div>
                                        <asp:TextBox ID="txtWorkshopAddress" TextMode="MultiLine" runat="server" TabIndex="4"
                                            MaxLength="200" CssClass="form-control" ToolTip="Enter Workshop Address"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDrvrAdd1" Display="None" runat="server"
                                            SetFocusOnError="true" ValidationGroup="Submit" ErrorMessage="Please Enter Workshop  Address."
                                            ControlToValidate="txtWorkshopAddress"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="5"
                            OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" UseSubmitBehavior="false" OnClientClick="handleButtonClick()"/>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="6"
                            CssClass="btn btn-warning" ToolTip="Click here to Reset" CausesValidation="false" />
                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Submit" />
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlList" runat="server">
                            <asp:ListView ID="lvWorkshop" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>WORKSHOP ENTRY LIST</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>EDIT
                                                    </th>
                                                    <th>WORKSHOP NAME
                                                    </th>
                                                    <th>WORKSHOP CONTACT NO.
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
                                                CommandArgument='<%# Eval("WSNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                OnClick="btnEdit_Click" />
                                        </td>
                                        <td>
                                            <%# Eval("WORKSHOP_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("PHONE")%>
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
       <script>
           function handleButtonClick() {
               var button = document.getElementById('<%= btnSubmit.ClientID %>');

              // Disable the button and update text
              button.disabled = true;
              button.value = "Please Wait...";

              // Enable the button after 10 seconds
              setTimeout(function () {
                  button.disabled = false;
                  button.value = "Submit";
              }, 10000); // 10000 milliseconds = 10 seconds
          }
</script>
</asp:Content>


