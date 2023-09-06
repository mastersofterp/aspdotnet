<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="TripType.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Master_TripType" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">TRIP TYPE</h3>
                </div>
                <div class="box-body">
                    <asp:UpdatePanel ID="updActivity" runat="server">
                        <ContentTemplate>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Trip Type Name </label>
                                        </div>
                                        <asp:TextBox ID="txtTripTypeName" runat="server" MaxLength="50" CssClass="form-control"
                                            ToolTip="Enter Trip Type Name" TabIndex="1"
                                            onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTripTypeName" runat="server" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Enter Trip Type Name." ValidationGroup="Submit"
                                            ControlToValidate="txtTripTypeName"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                        <asp:CheckBox ID="chkChargeable" runat="server" Text="Chargeable " TabIndex="2" />
                                        <asp:CheckBox ID="chkActive" runat="server" Text="Active" Checked="true" TabIndex="3" />

                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="4"
                            OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" UseSubmitBehavior="false" OnClientClick="handleButtonClick()" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="5"
                            CssClass="btn btn-warning" ToolTip="Click here to Reset" CausesValidation="false" />
                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Submit" HeaderText="Following Field(s) are mandatory" />

                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlList" runat="server">
                            <asp:ListView ID="lvTripType" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>Trip Type Entry List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>EDIT
                                                    </th>
                                                    <th>TRIP TYPE NAME
                                                    </th>
                                                    <th>CHARGEABLE
                                                    </th>
                                                    <th>STATUS
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
                                                CommandArgument='<%# Eval("TTID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                OnClick="btnEdit_Click" />
                                        </td>
                                        <td>
                                            <%# Eval("TRIPTYPENAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("CHARGEABLE")%>
                                        </td>
                                        <td>
                                            <%# Eval("ACTIVE")%>
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
