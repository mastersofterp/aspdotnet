<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CityMaster.aspx.cs" Inherits="Academic_Masters_CityMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdDistrict" runat="server" ClientIDMode="Static" />
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBatch"
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

    <asp:UpdatePanel ID="updBatch" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">CITY  MASTER</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>State</label>
                                        </div>
                                        <asp:DropDownList ID="ddlState" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" ToolTip="Please Select State" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <%--  Added By Sachin on 06-07-2022--%>
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>District</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDistrict" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" ToolTip="Please Select District">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDistrict" runat="server" ControlToValidate="ddlDistrict"
                                            Display="None" ErrorMessage="Please Select District" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>City</label>
                                        </div>
                                        <asp:TextBox ID="txtCity" runat="server" TabIndex="3"
                                            MaxLength="50" ToolTip="Please Enter  City Name" />
                                    </div>



                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Status</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdActive" name="switch" checked />
                                            <label data-on="Active" tabindex="7" class="newAddNew Tab" data-off="Inactive" for="rdActive"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit" OnClientClick="return validate();"
                                    CssClass="btn btn-primary" OnClick="btnSave_Click" TabIndex="4" />
                                <%--<asp:Button ID="btnShowReport" runat="server" CausesValidation="False" OnClick="btnShowReport_Click" Visible="false"
                                    TabIndex="4" Text="Report" ToolTip="Show Report" CssClass="btn btn-info" />--%>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="5" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-md-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <asp:Repeater ID="lvBatchName" runat="server">
                                            <HeaderTemplate>
                                                <div class="sub-heading">
                                                    <h5>City Name List</h5>
                                                </div>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>City Name
                                                        </th>
                                                        <th>State
                                                        </th>
                                                        <th>District
                                                        </th>
                                                        <th>Status
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <%--<tr id="itemPlaceholder" runat="server" />--%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("CITYNO") %>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="6" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("CITY")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("STATENAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DISTRICTNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ACTIVESTATUS")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </asp:Panel>
                            </div>

                        </div>
                        <div id="divMsg" runat="server">
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

            $('#hfdStat').val($('#rdActive').prop('checked'));

            var ddlState = $("[id$=ddlState]").attr("id");
            var ddlState = document.getElementById(ddlState);
            // alert(txtOwnershipStatusName.value.length)
            if (ddlState.value == 0) {
                alert('Please Select  State', 'Warning!');
                $(ddlState).focus();
                return false;
            }

            var txtCity = $("[id$=txtCity]").attr("id");
            var txtCity = document.getElementById(txtCity);
            // alert(txtOwnershipStatusName.value.length)
            if (txtCity.value.length == 0) {
                alert('Please Enter City Name', 'Warning!');
                $(txtCity).focus();
                return false;
            }



            var ddlDistrict = $("[id$=ddlDistrict]").attr("id");       //added by sachin on 07-07-2022
            var ddlDistrict = document.getElementById(ddlDistrict);
            // alert(txtOwnershipStatusName.value.length)
            if (ddlDistrict.value == 0) {
                alert('Please Select District', 'Warning!');
                $(ddlDistrict).focus();
                return false;
            }


        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSave').click(function () {
                    validate();
                });
            });
        });
    </script>
</asp:Content>
