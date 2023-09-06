<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_Bulk_Photo_Update.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_Bulk_Photo_Update" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">BULK UPDATE OF PHOTOS</h3>
                </div>
                <div class="box-body">
                        <asp:Panel ID="divPaySlip" runat="server">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Select Staff Type and Department</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" ToolTip="Select College" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="true"
                                            AutoPostBack="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                            ValidationGroup="Acd" ErrorMessage="Please select College" SetFocusOnError="true"
                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Staff Name</label>--%>
                                            <label>Scheme/Staff</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStaffName" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlStaffName"
                                            Display="None" ErrorMessage="Please Select Scheme/Staff" ValidationGroup="Acd"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Department</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDepartment" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                             CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" data-select2-enable="true" TabIndex="3">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDepartment"
                                                        Display="None" ErrorMessage="Please Select Department" ValidationGroup="Acd"
                                                        InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click"
                                    ValidationGroup="Acd" CssClass="btn btn-primary" TabIndex="4"/>
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Acd" Visible="false"
                                    OnClick="bntSubmit_Click" CssClass="btn btn-primary" TabIndex="5"/>
                                <asp:Button ID="btnReport" runat="server" Text="ShowReport" ValidationGroup="Acd" Visible="false"
                                    OnClick="btnReport_Click" CssClass="btn btn-info" TabIndex="6"/>
                               <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" TabIndex="7"
                                    Text="Cancel" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="vsSelection" runat="server" ShowMessageBox="true" ShowSummary="false" TabIndex="8"
                                    DisplayMode="List" ValidationGroup="Acd" />
                            </div>
                        </asp:Panel>
                    <div class="col-12">
                        <asp:Panel ID="pnlEmpPhoto" runat="server">
                            <asp:ListView ID="lvUpdatePhoto" runat="server">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Employee Photo & Signature</h5>
                                    </div>
                                    <table  style="width: 100%"> <%--class="table table-striped table-bordered nowrap display"--%>
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>ID.No
                                                </th>
                                                <th>Name
                                                </th>
                                                <th>Photo
                                                </th>
                                                <th>Browse Photo
                                                </th>
                                                <th>Signature
                                                </th>
                                                <th>Browse Signature
                                                </th>
                                            </tr>
                                        <thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item">
                                        <td>
                                            <%#Eval("IDNO")%>
                                        </td>
                                        <td>
                                            <%#Eval("NAME")%>
                                        </td>
                                        <td>
                                            <asp:Image ToolTip='<%#Eval("NAME")%>' ID="ImgPhoto" Height="50px" Width="50px"
                                                runat="server" />
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fuEmpPhoto" runat="server"   /> <%--onchange="ShowpImagePreview(this);"--%>
                                            <%-- --%>
                                        </td>
                                        <td>
                                            <asp:Image ToolTip='<%#Eval("NAME")%>' ID="ImgSignature" Height="50px" Width="50px"
                                                runat="server" />
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fuEmpSignature" runat="server"  /> <%--onchange="ShowpSignPreview(this);"--%>
                                            <%-- --%>
                                        </td>
                                    </tr>
                                    <asp:HiddenField ID="hididno" Value='<%#Eval("IDNO")%>' runat="server" />
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                    <div id="divMsg" runat="server">
                    </div>
                </div>
            </div>
        </div>
    </div>
 <%--   <script type="text/javascript">
        var jq = $.noConflict();

        function ShowpImagePreview(input) {

            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {

                    jq('#ctl00_ContentPlaceHolder1_lvUpdatePhoto_ctrl10_ImgPhoto').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        function ShowpSignPreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    jq('#ctl00_ContentPlaceHolder1_lvUpdatePhoto_ctrl10_ImgSignature').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>--%>
</asp:Content>

