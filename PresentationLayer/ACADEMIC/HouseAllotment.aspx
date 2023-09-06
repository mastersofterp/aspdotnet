<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="HouseAllotment.aspx.cs" Inherits="ACADEMIC_HouseAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updHouse"
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

    <asp:UpdatePanel ID="updHouse" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>House ID</label>
                                        </div>
                                        <asp:TextBox ID="txtHouseId" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="1" TabIndex="1"
                                            ToolTip="Please Enter House ID" placeholder="Please Enter House ID" onkeyup="validateNumeric(this);" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Either Value</label>
                                        </div>
                                        <asp:TextBox ID="txtEitherValue" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="1" TabIndex="1"
                                            ToolTip="Please Enter Either Value" placeholder="Please Enter Either Value" onkeyup="validateNumeric(this);" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Or Value</label>
                                        </div>
                                        <asp:TextBox ID="txtOrValue" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="1" TabIndex="1"
                                            ToolTip="Please Enter Or Value" placeholder="Please Enter Or Value" onkeyup="validateNumeric(this);" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>House Name</label>
                                        </div>
                                        <asp:TextBox ID="txtHouseName" runat="server" AutoComplete="off" CssClass="form-control" TabIndex="1" MaxLength="20"
                                            ToolTip="Please Enter House Name" placeholder="Please Enter House Name" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>House Code</label>
                                        </div>
                                        <asp:TextBox ID="txtHouseCode" runat="server" AutoComplete="off" CssClass="form-control" TabIndex="1" MaxLength="16"
                                            ToolTip="Please Enter House Code" placeholder="Please Enter House Code" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Colour</label>
                                        </div>
                                        <asp:TextBox ID="txtColour" runat="server" AutoComplete="off" CssClass="form-control" TabIndex="1" onkeypress="return alphaOnly(event);"
                                            ToolTip="Please Enter Colour" placeholder="Please Enter Colour" MaxLength="12"/>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                            FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" TargetControlID="txtColour" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="row">
                                            <div class="form-group col-6">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Status</label>
                                                </div>
                                                <div class="switch form-inline">
                                                    <input type="checkbox" id="rdActive" name="switch" checked />
                                                    <label data-on="Active" tabindex="1" data-off="Inactive" for="rdActive"></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                    OnClick="btnSubmit_Click" TabIndex="1" CssClass="btn btn-primary" OnClientClick="return validate();" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    TabIndex="1" CssClass="btn btn-warning" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                            </div>

                            <div class="col-12">
                                <asp:ListView ID="lvHouseAllotment" runat="server">
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th style="text-align: center;">Edit
                                                    </th>
                                                    <th>House Id
                                                    </th>
                                                    <th>Either Value
                                                    </th>
                                                    <th>Or Value
                                                    </th>
                                                    <th>House Name
                                                    </th>
                                                    <th>House Code
                                                    </th>
                                                    <th>Colour
                                                    </th>
                                                    <th>Active Status
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
                                            <td style="text-align: center;">
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                    CommandArgument='<%# Eval("HAID")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                    OnClick="btnEdit_Click" TabIndex="1" />
                                            </td>
                                            <td>
                                                <%# Eval("HOUSE_ID") %>
                                            </td>
                                            <td>
                                                <%# Eval("EITHER_VALUE") %>
                                            </td>
                                            <td>
                                                <%# Eval("OR_VALUE")%>
                                            </td>
                                            <td>
                                                <%# Eval("HOUSE_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("HOUSE_CODE")%>
                                            </td>
                                            <td>
                                                <%# Eval("COLOUR")%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblActive" runat="server" ForeColor='<%# Eval("ACTIVE_STATUS").ToString().Equals("ACTIVE")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' Text='<%# Eval("ACTIVE_STATUS")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>

    </asp:UpdatePanel>

    <script>
        function SetStat(val) {
            $('#rdActive').prop('checked', val);
        }

        function validate() {

            $('#hfdActive').val($('#rdActive').prop('checked'));

            var txtHouseId = $("[id$=txtHouseId]").attr("id");
            var txtHouseId = document.getElementById(txtHouseId);
            if (txtHouseId.value.length == 0) {
                alert('Please Enter House Id', 'Warning!');
                $(txtHouseId).focus();
                return false;
            }

            var txtEitherValue = $("[id$=txtEitherValue]").attr("id");
            var txtEitherValue = document.getElementById(txtEitherValue);
            if (txtEitherValue.value.length == 0) {
                alert('Please Enter Either Value', 'Warning!');
                $(txtEitherValue).focus();
                return false;
            }

            var txtOrValue = $("[id$=txtOrValue]").attr("id");
            var txtOrValue = document.getElementById(txtOrValue);
            if (txtOrValue.value.length == 0) {
                alert('Please Enter Or Value', 'Warning!');
                $(txtOrValue).focus();
                return false;
            }

            var txtHouseName = $("[id$=txtHouseName]").attr("id");
            var txtHouseName = document.getElementById(txtHouseName);
            if (txtHouseName.value.length == 0) {
                alert('Please Enter House Name', 'Warning!');
                $(txtHouseName).focus();
                return false;
            }

            var txtHouseCode = $("[id$=txtHouseCode]").attr("id");
            var txtHouseCode = document.getElementById(txtHouseCode);
            if (txtHouseCode.value.length == 0) {
                alert('Please Enter House Code', 'Warning!');
                $(txtHouseCode).focus();
                return false;
            }

            var txtColour = $("[id$=txtColour]").attr("id");
            var txtColour = document.getElementById(txtColour);
            if (txtColour.value.length == 0) {
                alert('Please Enter Colour', 'Warning!');
                $(txtColour).focus();
                return false;
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });
    </script>

    <script>

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
    </script>

    <script type="text/javascript">
        function alphaOnly(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }
    </script>

</asp:Content>


