<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPAdhocReportConfig.aspx.cs" Inherits="ACADEMIC_POSTADMISSION_ADMPAdhocReportConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .modal {
            text-align: center;
            padding: 0!important;
        }

        .modal:before {
            content: '';
            display: inline-block;
            height: 100%;
            vertical-align: middle;
            margin-right: -4px;
        }

        .modal-dialog  {
            display: inline-block;
            text-align: left;
            vertical-align: middle;
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Generate Report Configuration
                     <%--<asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>--%>
                    </h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-5 col-md-6 col-12">
                                <label><sup>*</sup> Report Name</label>
                                <asp:TextBox ID="txtReportName" runat="server" TabIndex="3" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtReportName" runat="server" ControlToValidate="txtReportName"
                                    Display="None" ErrorMessage="Please Enter Report Name" ValidationGroup="Report"
                                    SetFocusOnError="true"> </asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-5 col-md-6 col-12" id="divProcName">
                                <label><sup>*</sup> Proc Name</label>
                            <%--    <asp:DropDownList runat="server" ID="ddlProcName" TabIndex="5" CssClass="form-control" Enabled="true" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlProcName_SelectedIndexChanged">
                                    <%--OnSelectedIndexChanged="ddlSubType_SelectedIndexChanged"
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlProcName" runat="server" ControlToValidate="ddlProcName"
                                    Display="None" ValidationGroup="Report" InitialValue="Please Select"
                                    ErrorMessage="Please Select Proc Name"></asp:RequiredFieldValidator>--%>
                                <asp:TextBox ID="txtProcName" runat="server" onblur="txtProcNameLostFocus();"  TabIndex="5" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtProcName" runat="server" ControlToValidate="txtProcName"
                                    Display="None" ValidationGroup="Report"
                                    ErrorMessage="Please Enter Proc Name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-lg-5 col-md-5 col-12">
                                <label><sup></sup>Excel Tab Name</label>
                                <asp:TextBox ID="txtTabName" runat="server" TabIndex="3" CssClass="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="rfvtxtTabName" runat="server" ControlToValidate="txtTabName"
                                    Display="None" ErrorMessage="Please Enter Report Name" ValidationGroup="Report"
                                    SetFocusOnError="true">
                                </asp:RequiredFieldValidator>--%>
                                <span style="font-size: small; color: red;">(Note :Please Enter Excel Report Multiple Tab Name Comma Separated)</span>
                            </div>
                            <asp:HiddenField ID="hfdActiveStatus" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hfdDisplayStatus" runat="server" ClientIDMode="Static" />
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="row">
                                                                <div class="form-group col-6">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Active Status</label>
                                                                    </div>
                                                                    <div class="switch form-inline">
                                                                        <input type="checkbox" id="chkActiveStatus" name="switch" checked />
                                                                        <label data-on="Active" data-off="Inactive" for="chkActiveStatus"></label>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group col-6">     <%--Added By Shrikant W. on 11022024--%>
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Display Status</label>
                                                                    </div>
                                                                    <div class="switch form-inline">
                                                                        <input type="checkbox" id="chkDisplayStatus" name="switch" checked />

                                                                        <label data-on="Active" data-off="Inactive" for="chkDisplayStatus"></label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                        </div>

                    </div>
                    <div class="col-md-6 col-12">
                        <asp:Panel ID="pnlProcParamsList" runat="server" Visible="false">
                            <asp:ListView ID="lvProcParamsList" runat="server">
                                <LayoutTemplate>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divProcParamsList">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>SrNo</th>
                                                <th>Parameter</th>
                                                <th>Control</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="text-align: center">
                                            <%# Container.DataItemIndex + 1 %>
                                           <%--<asp:HiddenField ID="hfdAdhocId" runat="server" Value='<%# Eval("OBJECT_ID") %>' />--%>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblParamName" runat="server" Text='<%# Eval("PARAM_NAME") %>' />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="CONTROLID" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">ddlAdmissionBatch</asp:ListItem>
                                                <asp:ListItem Value="2">ddlADMPtype</asp:ListItem>
                                                <asp:ListItem Value="3">ddlDegree</asp:ListItem>
                                                <asp:ListItem Value="4">ddlBranch</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="btnSubmit" runat="server" Text="Submit" TabIndex="4" ValidationGroup="Report" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click" OnClientClick="return validateTST_Active();"  />
                        <%--OnClick="btnSubmit_Click"--%>
                        <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="Report"
                            DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="5" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                        <%--OnClick="btnCancel_Click"--%>
                    </div>

                    <div class="col-lg-10 col-12">
                        <asp:Panel ID="pnlReportConfig" runat="server" Visible="false">
                            <asp:ListView ID="lvReportConfig" runat="server">
                                <LayoutTemplate>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divReportConfig">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th style="text-align: center;">Edit</th>
                                                <th>Report Name</th>
                                                <th>Proc Name</th>
                                                <th>Active Status</th>
                                                <th>Display Status</th>      <%--Added By Shrikant W. on 11022024--%>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server"/>
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="text-align: center;">
                                            <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                CommandArgument='<%# Eval("ADHOCID")%>' AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" OnClick="btnEdit_Click" />
                                            <%--OnClick="btnEdit_Click"--%>                                                                    
                                        </td>
                                        <td>
                                            <%# Eval("REPORTNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("PROCEDURENAME")%>
                                        </td>
                                        <td>  <%--Modified By Shrikant W. on 11022024--%>
                                            <asp:Label ID="lblActiveStatus" runat="server" CssClass='<%# Eval("ACTIVE_STATUS", "{0}") == "True" ? "badge badge-success" : "badge badge-danger" %>'>
        <%# Eval("ACTIVE_STATUS", "{0}") == "True" ? "Active" : "Inactive" %>
                                            </asp:Label>
                                        </td>
                                        <td>   <%--Added By Shrikant W. on 11022024--%>
                                            <asp:Label ID="lblDisplayStatus" runat="server" CssClass='<%# Eval("DISPLAY_STATUS", "{0}") == "True" ? "badge badge-success" : "badge badge-danger" %>'>
        <%# Eval("DISPLAY_STATUS", "{0}") == "True" ? "Active" : "Inactive" %>
                                            </asp:Label>
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

    <div id="popup" runat="server">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="modal" id="myModalPopUp" data-backdrop="static" >
                    <div class="modal-dialog modal-md">
                        <div class="modal-content">
                            <div class="modal-body pl-0 pr-0 pl-lg-2 pr-lg-2">
                                <div class="col-12 mt-3">
                                    <h5 class="heading">Please Enter Developer's Password</h5>
                                    <div class="row">
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <label>PASSWORD</label>
                                            <asp:Label ID="lblPass" runat="server" Text="ybc@123" Visible="false"></asp:Label>
                                            <asp:TextBox ID="txtPass" TextMode="Password" runat="server" TabIndex="1" ToolTip="Please Enter Password" AutoComplete="new-password"
                                                MaxLength="50" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="req_password" runat="server" ErrorMessage="Password Required !" ControlToValidate="txtPass" 
                                                Display="None" ValidationGroup="password" ></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                        </div>
                                        <div class="btn form-group col-lg-12 col-md-12 col-12">
                                            <asp:Button ID="btnConnect" data-dismiss="myModalPopUp" data-keyboard="false" TabIndex="1" CssClass="btn btn-outline-primary" 
                                                runat="server" Text="Submit" OnClick="btnConnect_Click" ValidationGroup="password"  />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                   <asp:PostBackTrigger ControlID="btnConnect" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <asp:ValidationSummary ID="vsLogin" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" ValidationGroup="password" />
    <script type="text/javascript">
        function txtProcNameLostFocus() {
            // Call __doPostBack to trigger a postback to the server
            __doPostBack('<%= txtProcName.UniqueID %>', '');
    }
    </script>
    <script>
        function SetTST_Active(val) {
            $('#chkActiveStatus').prop('checked', val);
        }

        function Set_ActiveStatus(val) {
            $('#chkActiveStatus').prop('checked', val);
        }

        function Set_DisplayStatus(val) {
            $('#chkDisplayStatus').prop('checked', val);
        } 

        function validateTST_Active() {
            $('#hfdActiveStatus').val($('#chkActiveStatus').prop('checked'));
            $('#hfdDisplayStatus').val($('#chkDisplayStatus').prop('checked'));
        }


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validateTST_Active();
                });
            });
        });
    </script>

    <script>    <%--Added By Shrikant W. on 11022024--%>
        document.addEventListener('DOMContentLoaded', function () {
           
            var chkDisplayStatus = document.getElementById('chkDisplayStatus');
            var hfdDisplayStatus = document.getElementById('hfdDisplayStatus');
            hfdDisplayStatus.value = chkDisplayStatus.checked;

           
            chkDisplayStatus.addEventListener('change', function () {
                hfdDisplayStatus.value = chkDisplayStatus.checked;
            });
        });
</script>

    <script>   <%--Added By Shrikant W. on 11022024--%>
    document.addEventListener('DOMContentLoaded', function () {
   
    var chkActiveStatus = document.getElementById('chkActiveStatus');

   
    var hfdActiveStatus = document.getElementById('hfdActiveStatus');
    hfdActiveStatus.value = chkActiveStatus.checked;

    
    chkActiveStatus.addEventListener('change', function () {
        hfdActiveStatus.value = chkActiveStatus.checked;
    });

});
        </script>


    <script type="text/javascript">
        $(window).on('load', function () {
            $('#myModalPopUp').modal('show');
        });
</script>

</asp:Content>
