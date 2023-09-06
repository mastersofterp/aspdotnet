<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PhD_Admission_Session.aspx.cs" Inherits="ACADEMIC_PHD_PhD_Admission_Session" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <style>
        .dataTables_scrollHeadInner
        {
            width: max-content !important;
        }
    </style>
    <style>
        .Tab:focus
        {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }
    </style>

    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdStart" runat="server" ClientIDMode="Static" />

    <div>
        <asp:UpdateProgress ID="UPDPROG" runat="server" AssociatedUpdatePanelID="UPDROLE"
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


    <asp:UpdatePanel runat="server" ID="UPDROLE">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">SESSION CREATION</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session Name</label>
                                        </div>
                                        <asp:TextBox ID="txtName" runat="server" AutoComplete="off" CssClass="form-control" TabIndex="1"
                                            ToolTip="Please Enter Session Name" placeholder="Enter Session Name" />
                                        <asp:RequiredFieldValidator ID="rfvSessionName" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Enter Session Name" ControlToValidate="txtName"
                                            Display="None" ValidationGroup="submit" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Start Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="txtFromDate1" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" ValidationGroup="Submit"
                                                CssClass="form-control" AutoPostBack="true" />
                                            <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                                PopupButtonID="txtFromDate1" />
                                            <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" EmptyValueMessage="Please Enter To Date"
                                                ControlExtender="meFromDate" ControlToValidate="txtFromDate" IsValidEmpty="false"
                                                InvalidValueMessage="Date is invalid" Display="None" ErrorMessage="Please Enter From Date"
                                                InvalidValueBlurredMessage="*" ValidationGroup="submit" SetFocusOnError="true" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="tdToDate" runat="server" visible="true">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>End Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="txtToDate1" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" TabIndex="1" ValidationGroup="Submit"
                                                CssClass="form-control" AutoPostBack="true" />

                                            <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                PopupButtonID="txtToDate1" />
                                            <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" EmptyValueMessage="Please Enter To Date"
                                                ControlExtender="meToDate" ControlToValidate="txtToDate" IsValidEmpty="false"
                                                InvalidValueMessage="Date is invalid" Display="None" ErrorMessage="Please Enter To Date"
                                                InvalidValueBlurredMessage="*" ValidationGroup="submit" SetFocusOnError="true" />
                                        </div>
                                    </div>

                                    <asp:HiddenField ID="hdnDate" runat="server" />


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="row">
                                            <div class="form-group col-6">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Active Status</label>
                                                </div>
                                                <div class="switch form-inline">
                                                    <input type="checkbox" id="rdActive" name="switch" checked />
                                                    <label data-on="Active" data-off="Inactive" for="rdActive" tabindex="3"></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                    TabIndex="4" CssClass="btn btn-primary" OnClientClick="return validate();" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    TabIndex="5" CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                            </div>

                            <div class="col-12">

                                <asp:Panel ID="pnlSession" runat="server" Visible="false">
                                    <asp:ListView ID="lvSession" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>PhD Admission Session List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center;">Action
                                                        </th>
                                                        <th>Session Name
                                                        </th>
                                                        <th>Start Date
                                                        </th>
                                                        <th>End Date
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
                                                    <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                        OnClick="btnEdit_Click" CommandArgument='<%# Eval("ADM_SESSION_ID")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        TabIndex="6" />
                                                </td>
                                                <td>
                                                    <%# Eval("SESSION_NAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("SESSION_STDATE","{0:dd-MMM-yyyy}") %>
                                                </td>
                                                <td>
                                                    <%# Eval("SESSION_ENDDATE", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblIsActive" runat="server" CssClass='<%# Eval("IS_ACTIVE")%>' Text='<%# Eval("IS_ACTIVE")%>' ForeColor='<%# Eval("IS_ACTIVE").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>

    <script>
        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }

        function validate() {

            $('#hfdActive').val($('#rdActive').prop('checked'));
            $('#hfdStart').val($('#rdStart').prop('checked'));

            var idtxtweb = $("[id$=txtName]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Session Name', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }
            var idtxtweb = $("[id$=txtFromDate]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Start Date', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }

            var idtxtweb = $("[id$=txtToDate]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter End Date', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
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
</asp:Content>

