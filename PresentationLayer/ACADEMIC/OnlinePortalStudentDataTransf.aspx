<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="OnlinePortalStudentDataTransf.aspx.cs" Inherits="ACADEMIC_OnlinePortalStudentDataTransf" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server"
        DynamicLayout="true" DisplayAfter="0">
        <ProgressTemplate>
            <div>
                <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updTab2"
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

        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="box-body">
        <asp:Panel ID="pnDisplay" runat="server">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>
                            </h3>
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </div>
                        <asp:UpdatePanel ID="updTab2" runat="server">
                            <ContentTemplate>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12 ml-3">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Application ID</label>
                                            </div>
                                            <asp:TextBox ID="txtAppIdPhD" runat="server" MaxLength="15" TabIndex="1" CssClass="form-control" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" style="margin-top: 20px">
                                            <asp:Button ID="btnShowPhD" runat="server" Text="Show" CssClass="btn btn-primary" TabIndex="1"
                                                OnClick="btnShowPhD_Click" OnClientClick="return ValidateShow();" />
                                            <%--<asp:ValidationSummary ID="vsSummaryPhD" runat="server" ValidationGroup="ShowPhd" DisplayMode="List" ShowSummary="false"
                                                                    ShowMessageBox="true" />--%>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnlPhD" runat="server" Visible="false">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Student Details</h5>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="divPhD">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Student Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblStudNamePhD" runat="server" Font-Bold="true" /></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Date of Birth :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblDOBPhD" runat="server" Font-Bold="true" /></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Address :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblAddPhD" runat="server" Font-Bold="true" /></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Pin Code :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblPinPhD" runat="server" Font-Bold="true" /></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Mobile No :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblMobPhD" runat="server" Font-Bold="true" /></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Email ID :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblEmailPhD" runat="server" Font-Bold="true" /></a>
                                                            </li>
                                                           
                                                            <div>
                                                                <asp:ListView ID="lvFees" runat="server" Visible="false">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Application Fees Details</h5>
                                                                        </div>
                                                                        <%--  <h6>Application Fees Details</h6>--%>
                                                                        <table class="table table-striped table-bordered" style="width: 100%;">
                                                                            <thead>
                                                                                <tr class="bg-light-blue">
                                                                                    <th>Programme/Branch</th>
                                                                                    <th>Fees</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <tr class="item">
                                                                            <td>
                                                                                <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("LONGNAME")%>' />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblFees" runat="server" Text='<%# Eval("TOTAL_AMT") %>' />
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </div>
                                                              
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <%--<li class="list-group-item"><%--<b>Photo :</b>--%>
                                                                <a class="sub-label">
                                                                    <asp:Image ID="imgPhotoPhD" runat="server" Height="128px" Width="128px" /></a>
                                                          <%--  </li>--%>

                                                        </ul>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Admission Batch</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlAdmbatchPHD" runat="server" ToolTip="Please select Admission Batch." Enabled="false" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlAdmbatchPHD_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmbatchPHD"
                                                            Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="submit" />

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>School/Institute</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSchoolPhD" runat="server" ToolTip="Please select School/Institute." Enabled="false" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSchoolPhD_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSchoolPhD"
                                                            Display="None" ErrorMessage="Please select School/Institute" InitialValue="0" ValidationGroup="submit" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Degree</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDegreePhD" runat="server" ToolTip="Please select degree." Enabled="false" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                                            OnSelectedIndexChanged="ddlDegreePhD_SelectedIndexChanged" AppendDataBoundItems="True">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDegreePhD"
                                                            Display="None" ErrorMessage="Please select degree." InitialValue="0" ValidationGroup="submit" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Div2">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Branch</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlBranchPhD" runat="server" ToolTip="Please select branch" Enabled="false"
                                                            AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBranchPhD"
                                                            Display="None" ErrorMessage="Please select branch" InitialValue="0" ValidationGroup="submit" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Payment Type</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlPayTypePhD" CssClass="form-control" data-select2-enable="true" runat="server" ToolTip="Please select payment type."
                                                            AppendDataBoundItems="True">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlPayTypePhD"
                                                            Display="None" ErrorMessage="Please select payment type." InitialValue="0" ValidationGroup="submit" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnSubmitPhD" runat="server" TabIndex="12" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmitPhD_Click" OnClientClick="return validateSubmit()" ValidationGroup="submit" />
                                                <asp:Button ID="btnPrintPhD" runat="server" Visible="false" TabIndex="12" Text="Print" OnClick="btnPrintPhD_Click" CssClass="btn btn-primary" />
                                                <asp:Button ID="btnCancelPhD" CssClass="btn btn-warning" runat="server" TabIndex="1" Text="Cancel" OnClick="btnCancelPhD_Click" />
                                                <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="submit"
                                                    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />

                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Label ID="Label8" runat="server" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
    <script>
        function ValidateShow() {
            var alertMsg = "";
            var AppId = document.getElementById('<%=txtAppIdPhD.ClientID%>').value;
            if (AppId == "") {
                if (AppId == 0) {
                    alertMsg = alertMsg + 'Please enter application id.\n';
                    document.getElementById('<%=txtAppIdPhD.ClientID%>').focus();
                }

                alert(alertMsg);
                return false;
            }
        }
        function validateSubmit() {
            var alertMsg = '';
            var degree = document.getElementById('<%=ddlDegreePhD.ClientID%>');
                                   var branch = document.getElementById('<%=ddlBranchPhD.ClientID%>');
                                   var payType = document.getElementById('<%=ddlPayTypePhD.ClientID%>');
                                   if (degree.value == 0 || branch.value == 0 || payType.value == 0) {
                                       if (degree.value == "0") {
                                           alertMsg = 'Please select degree.\n';
                                       }
                                       if (branch.value == "0") {
                                           alertMsg = alertMsg + 'Please select branch.\n';

                                       }
                                       if (payType.value == "0") {
                                           alertMsg = alertMsg + 'Please select payment type.\n';
                                       }
                                       alert(alertMsg);
                                       return false;
                                   }
                               }
    </script>
    <%--<script type="text/javascript">
    document.addEventListener("contextmenu", function (e) {
        e.preventDefault();
    }
    );
</script>
    <script type="text/javascript">
        document.addEventListener("keydown", function (e)
        {
            // Prevent F12 key
            if (e.key === "F12" || e.keyCode === 123)
            {
                e.preventDefault();
            }
        });
</script>--%>
    <script>
        (function () {
            function isDeveloperToolsOpen() {
                return /./.test(function () { console.log(''); });
            }

            if (isDeveloperToolsOpen()) {
                // Developer tools are open, take appropriate action
                // e.g., display warning message or redirect the user
            }
        })();
    </script>
</asp:Content>

