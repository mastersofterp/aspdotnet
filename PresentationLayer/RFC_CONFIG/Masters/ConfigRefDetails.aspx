<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ConfigRefDetails.aspx.cs" Inherits="RFC_CONFIG_Masters_ConfigRefDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .Tab:focus
        {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updRef" runat="server" AssociatedUpdatePanelID="updConfigRef"
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

    <asp:UpdatePanel ID="updConfigRef" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title" style="margin-left: 25px">Config Reference Details</h3>
                        </div>
                        <div id="div1" runat="server"></div>
                        <div class="box-body">
                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Organization</label>
                                            </div>
                                            <asp:DropDownList ID="ddlOrganization" runat="server" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Project Name</label>
                                            </div>
                                            <asp:TextBox ID="txtProjectName" Placeholder="Please Enter Project Name" CssClass="form-control" onkeypress="return alphaOnly(event);"
                                                TabIndex="2" ToolTip="Enter Project Name" AutoComplete="off" runat="server"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteProname" runat="server" TargetControlID="txtProjectName" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Server Name</label>
                                            </div>
                                            <asp:TextBox ID="txtServerName" Placeholder="Please Enter Server Name" CssClass="form-control"
                                                TabIndex="3" ToolTip="Enter Server Name" AutoComplete="off" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>User ID</label>
                                            </div>
                                            <asp:TextBox ID="txtUserID" Placeholder="Please Enter User ID" CssClass="form-control"
                                                TabIndex="4" ToolTip="Enter User ID" AutoComplete="off" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Password</label>
                                            </div>
                                            <asp:TextBox ID="txtPassword" Placeholder="Please Enter Password" CssClass="form-control"
                                                TabIndex="5" ToolTip="Enter Password" AutoComplete="off" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Database Name</label>
                                            </div>
                                            <asp:TextBox ID="txtDatabaseName" Placeholder="Please Enter Database Name" CssClass="form-control" onkeypress="return alphaOnly(event);"
                                                TabIndex="6" ToolTip="Enter Database Name" AutoComplete="off" runat="server"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteDatabase" runat="server" TargetControlID="txtDatabaseName" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="~`!@#$%^*()+=,./:;<>?'{}[]\|&&quot;'" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Organization URL</label>
                                            </div>
                                            <asp:TextBox ID="txtOrgUrl" Style="text-transform: lowercase" Placeholder="Please Enter Organization URL" CssClass="form-control"
                                                TabIndex="6" ToolTip="Enter Organization URL" AutoComplete="off" runat="server"></asp:TextBox>
                                            <%-- <asp:RegularExpressionValidator ID="RegExUrl" runat="server" ForeColor="Red" ErrorMessage="Please Enter URL in Correct Format" ControlToValidate="txtOrgUrl" ValidationExpression="(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Default Page Name</label>
                                            </div>
                                            <asp:TextBox ID="txtDefaultpage" Placeholder="Please Enter Default Page Name" CssClass="form-control"
                                                TabIndex="6" ToolTip="Enter Default Page Name" AutoComplete="off" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" OnClick="btnSubmit_Click" OnClientClick="return validate();"
                                        TabIndex="7" CssClass="btn btn-primary" />

                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" OnClick="btnCancel_Click"
                                        TabIndex="8" CssClass="btn btn-warning" />

                                    <%-- <asp:Button ID="btnGoBack" Visible="false" runat="server" Text="Go Back To List" ToolTip="Go Back To List" OnClick="btnGoBack_Click"
                                        TabIndex="8" CssClass="btn btn-info" />--%>
                                </div>


                                <div class="col-12">
                                    <asp:Panel ID="pnlConfigRef" runat="server" Visible="false">

                                        <asp:ListView ID="lvConfigRef" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Config Reference Details List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divlist">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th style="text-align: center;">Edit
                                                            </th>
                                                            <th>Organization
                                                            </th>
                                                            <th>Project Name
                                                            </th>
                                                            <th>Server Name
                                                            </th>
                                                            <th>User ID
                                                            </th>
                                                            <th>Password
                                                            </th>
                                                            <th>Database Name
                                                            </th>
                                                            <th>Organization URL
                                                            </th>
                                                            <th>Default Page Name
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
                                                        <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                            CommandArgument='<%# Eval("ReferenceDetailsId")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                            TabIndex="9" OnClick="btnEdit_Click" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("OrgName") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ProjectName")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ServerName")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("UserId")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Password")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DBName")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("URL_LINK")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEFAULT_PAGE")%>
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
            </div>
            </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%-- <asp:PostBackTrigger ControlID="btnSubmitDepMapp" />--%>
        </Triggers>
    </asp:UpdatePanel>
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

    <%--<script type="text/javascript">
        function ValidURL(str) {
            var regex = /(http[s]?:\/\/)(www[\w]?\.)[\w]*\.[\w]{2,3}(\.[\w]{2})?$/;
            if (!regex.test(str)) {
                alert("Please enter valid URL.");
                //document.getElementById('txtOrgUrl').value == '';
                document.getElementById("<%=txtOrgUrl.ClientID%>").value = "";
                return false;
            } else {
                alert("Valid URL.");
                return true;
            }
        }
    </script>--%>

    <script>
        function validate() {

            var ddlO = $("[id$=ddlOrganization]").attr("id");
            var ddlO = document.getElementById(ddlO);
            if (ddlO.value == 0) {
                alert('Please Select Organization', 'Warning!');
                $(ddlO).focus();
                return false;
            }

            var txtProject = $("[id$=txtProjectName]").attr("id");
            var txtProject = document.getElementById(txtProject);
            if (txtProject.value == 0) {
                alert('Please Enter Project Name', 'Warning!');
                $(txtProject).focus();
                return false;
            }

            var txtServer = $("[id$=txtServerName]").attr("id");
            var txtServer = document.getElementById(txtServer);
            if (txtServer.value == 0) {
                alert('Please Enter Server Name', 'Warning!');
                $(txtServer).focus();
                return false;
            }

            var txtU = $("[id$=txtUserID]").attr("id");
            var txtU = document.getElementById(txtU);
            if (txtU.value == 0) {
                alert('Please Enter User ID', 'Warning!');
                $(txtU).focus();
                return false;
            }

            var txtPass = $("[id$=txtPassword]").attr("id");
            var txtPass = document.getElementById(txtPass);
            if (txtPass.value == 0) {
                alert('Please Enter Password', 'Warning!');
                $(txtPass).focus();
                return false;
            }

            var txtDb = $("[id$=txtDatabaseName]").attr("id");
            var txtDb = document.getElementById(txtDb);
            if (txtDb.value == 0) {
                alert('Please Enter Database Name', 'Warning!');
                $(txtDb).focus();
                return false;
            }

            var txtO = $("[id$=txtOrgUrl]").attr("id");
            var txtO = document.getElementById(txtO);
            if (txtO.value == 0) {
                alert('Please Enter Organization Url', 'Warning!');
                $(txtO).focus();
                return false;
            }

            var txtpage = $("[id$=txtDefaultpage]").attr("id");
            var txtpage = document.getElementById(txtpage);
            if (txtpage.value == 0) {
                alert('Please Enter Default Page Name', 'Warning!');
                $(txtpage).focus();
                return false;
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validateCol();
                });
            });
        });

    </script>
</asp:Content>

