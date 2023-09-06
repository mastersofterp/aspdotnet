<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentODLimitConfig.aspx.cs" Inherits="ACADEMIC_ODTracker_StudentODLimitConfig" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hfdODConfig" runat="server" ClientIDMode="Static" />
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updConfig"
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
    <asp:UpdatePanel ID="updConfig" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Student OD Limit Config</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmbatch" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlAdmbatch_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Select Admbatch" ControlToValidate="ddlAdmbatch" InitialValue="0"
                                            Display="None" ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Select Scheme" ControlToValidate="ddlScheme" InitialValue="0"
                                            Display="None" ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Days Allowed</label>
                                        </div>
                                        <asp:TextBox ID="txtAllowedDays" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="2" onkeypress="return isNumber(event)"
                                            ToolTip="Please Enter Allowed Days for OD" placeholder="Allowed Days" />
                                        <asp:RequiredFieldValidator ID="rfvAllowDays" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Enter Allowed days" ControlToValidate="txtAllowedDays"
                                            Display="None" ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Status</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdActiveODConfig" name="switch" checked />
                                            <label data-on="Active" data-off="Inactive" for="rdActiveODConfig"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <%--<asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" OnClientClick="return validate();" OnClick="btnShow_Click"/>--%>
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" OnClientClick="validateODConfig();" CausesValidation="true" ValidationGroup="submit" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvCourse" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divCourselist">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>SRNO</th>
                                                        <th>Admission Batch</th>
                                                        <th>Scheme</th>
                                                        <th>Config Status
                                                        </th>
                                                        <th>OD Allowed Days
                                                        </th>
                                                        <%--<th>Course Code
                                                            <asp:Label ID="lblDYddlSchool" runat="server" Visible="false" Text='<%#Eval("COURSENO")%>'> </asp:Label>
                                                        </th>
                                                        <th>Course Name
                                                        </th>
                                                        --%>
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
                                                    <asp:Label ID="lblSrNo" runat="server" Text='<%# (Container.DataItemIndex)+1%>'> </asp:Label>
                                                </td>
                                                <td>
                                                    <%# Eval("BATCHNAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("SCHEMENAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("IS_CONFIG_DONE").ToString() =="1"?"Yes":"No" %>
                                                </td>
                                                <td>
                                                    <%# Eval("ALLOWED_OD_DAYS") %>
                                                </td>
                                                <%--  <td>
                                                    <%# Eval("CCODE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("COURSE_NAME") %>
                                                </td>
                                                
                                                --%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlCourse" runat="server">
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function SetODConfig(val) {
            $('#rdActiveODConfig').prop('checked', val);
        }

        function validateODConfig() {
            $('#hfdODConfig').val($('#rdActiveODConfig').prop('checked'));
        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
