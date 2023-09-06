<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LocationMaster.aspx.cs"
    Inherits="ESTABLISHMENT_LEAVES_Master_LocationMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>

    <script>
        onkeypress = "return CheckAlphabet(event,this);"
        function CheckAlphabet(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }
    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Biometric Location Master</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="panel panel-info">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Add/Edit Location</h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Location Name</label>
                                                </div>
                                                <asp:TextBox ID="txtLocName" runat="server" MaxLength="100" CssClass="form-control"
                                                    ToolTip="Enter Location Name" TabIndex="1" />
                                                <asp:RequiredFieldValidator ID="rfvLoc" runat="server" ControlToValidate="txtLocName"
                                                    Display="None" ErrorMessage="Please Enter Location Name" ValidationGroup="LeaveName"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Latitude</label>
                                                </div>
                                                <asp:TextBox ID="txtLatitude" runat="server" MaxLength="80" CssClass="form-control" ToolTip="Enter Latitude"
                                                    TabIndex="2" />
                                                <asp:RequiredFieldValidator ID="rfvLatitude" runat="server" ControlToValidate="txtLatitude"
                                                    Display="None" ErrorMessage="Please Enter Latitude" ValidationGroup="LeaveName"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Longitude</label>
                                                </div>
                                                <asp:TextBox ID="txtLongitude" runat="server" MaxLength="80" CssClass="form-control" ToolTip="Enter Longitude"
                                                    TabIndex="3"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvLong" runat="server" ControlToValidate="txtLongitude"
                                                    Display="None" ErrorMessage="Please Enter Longitude" ValidationGroup="LeaveName"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Radius</label>
                                                </div>
                                                <asp:TextBox ID="txtRadius" runat="server" MaxLength="50" CssClass="form-control" ToolTip="Enter Radius"
                                                    TabIndex="4" onkeypress="return CheckNumeric(event,this);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvRadius" runat="server" ControlToValidate="txtRadius" Display="None"
                                                    ErrorMessage="Please Enter Radius" ValidationGroup="LeaveName" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Address</label>
                                                </div>
                                                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" ToolTip="Enter Address"
                                                    CssClass="form-control" TabIndex="5" MaxLength="200"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvAdd" runat="server" ControlToValidate="txtAddress"
                                                    Display="None" ErrorMessage="Please Enter Address" ValidationGroup="LeaveName"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                        <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" TabIndex="7"
                                    CssClass="btn btn-primary" ToolTip="Click here to Add New Leave Type" Text="Add New"></asp:LinkButton>
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="LeaveName" TabIndex="8"
                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSave_Click" />
                                <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click"
                                    CssClass="btn btn-primary" ToolTip="Click here to Return to Previous Menu" TabIndex="9" />
                                <%-- <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-info" ToolTip="Click here to Show Report"
                                    Visible="false" OnClick="btnShowReport_Click" TabIndex="10" />--%>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" ToolTip="Click here to Reset"
                                    CssClass="btn btn-warning" TabIndex="11" OnClick="btnCancel_Click" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LeaveName"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <asp:Panel ID="pnlList" runat="server">
                                <div class="col-12">
                                    <asp:Repeater ID="lvLocation" runat="server">
                                        <HeaderTemplate>
                                            <div class="sub-heading">
                                                <h5>Location Name</h5>
                                            </div>
                                            <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Location
                                                        </th>
                                                        <th>Latitude
                                                        </th>
                                                        <th>Longitude
                                                        </th>
                                                        <th>Radius
                                                        </th>
                                                        <th>Location Address
                                                        </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("LOCNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="12" OnClick="btnEdit_Click" />
                                                    <%-- <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("PANO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />--%>
                                                </td>
                                                <td>
                                                    <%# Eval("LOCATION_NAME")%>
                                                </td>
                                                <td>
                                                    <%#Eval("LATITUDE")%>
                                                </td>
                                                <td>
                                                    <%#Eval("LONGITUDE")%>
                                                </td>
                                                <td>
                                                    <%#Eval("RADIUS")%>
                                                </td>
                                                <td>
                                                    <%#Eval("LOCATION_ADDRESS")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody></table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnBack" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>

    <%--  Enable the button so it can be played again --%>


    <script type="text/javascript">

        function CheckNumeric(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            //alert(k);
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k > 45 && k < 58) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter numeric Value');
                obj.focus();
            }
            return false;
        }
    </script>



</asp:Content>


