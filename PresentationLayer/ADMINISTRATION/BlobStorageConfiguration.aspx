<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BlobStorageConfiguration.aspx.cs" Inherits="ACADEMIC_BlobStorageConfiguration" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBlobConfig"
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

    <asp:UpdatePanel ID="updBlobConfig" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">BLOB STORAGE CONFIGURATION</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Account Name</label>
                                        </div>
                                        <asp:TextBox ID="txtAccountName" runat="server" TabIndex="1"
                                              CssClass="form-control" ToolTip="Please Enter Account Name" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAccountName" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Enter Account Name" ControlToValidate="txtAccountName"
                                            Display="None" ValidationGroup="submit" />
                                    </div>

                                    <div class="form-group col-lg-9 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Secret Key</label>
                                        </div>
                                        <asp:TextBox ID="txtSecretKey" runat="server" TabIndex="2" AutoComplete="off"
                                             CssClass="form-control" ToolTip="Please Enter Secret Key" ValidationGroup="submit" ></asp:TextBox>
                                    </div>
                                 </div>
                                <div class="row"> 
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Container Name</label>
                                        </div>
                                        <asp:TextBox ID="txtContainerName" runat="server" TabIndex="3" style='text-transform: lowercase'
                                                CssClass="form-control"  ToolTip="Please Enter Container Name" ValidationGroup="submit"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label id="lblactivity" runat="server">Activity</label>
                                        </div>
                                        <asp:DropDownList ID="ddlActivity"  runat="server" TabIndex="4" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" ToolTip="Please Select Activity Type" ValidationGroup="submit">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label id="lblinstances" runat="server">Instances</label>
                                        </div>
                                        <asp:DropDownList ID="ddlInstances" runat="server"  TabIndex="5" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" ToolTip="Please Select Instance Type" ValidationGroup="submit">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <%-- <asp:ListItem Value="1">Test</asp:ListItem>
                                            <asp:ListItem Value="2">Live</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Status</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdActive" name="switch" checked />
                                            <label data-on="Active" tabindex="7" class="newAddNew Tab" data-off="Inactive" for="rdActive"></label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row"> <%--Added by Vinay Mishra on 26/06/2023--%>
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Blob Storage Path</label>
                                        </div>
                                        <asp:TextBox ID="txtBlobStoragePath" runat="server" TabIndex="8" AutoComplete="off"
                                             CssClass="form-control" ToolTip="Please Enter Blob Storage Path" MaxLength="256" ValidationGroup="submit" ></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit" OnClientClick="return validate();"
                                        OnClick="btnSave_Click" CssClass="btn btn-primary"  TabIndex="6" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" 
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning"  TabIndex="7" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="pnlBlobConfig" runat="server">
                                        <asp:ListView ID="lvBlobConfig" runat="server">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th style="text-align: center;">Edit
                                                            </th>
                                                            <th>Account Name
                                                            </th>
                                                            <th>Secret Key
                                                            </th>
                                                            <th>Container Name
                                                            </th>
                                                            <th>Instance
                                                            </th>
                                                            <th>Activity
                                                            </th>
                                                            <th>Activity Status
                                                            </th>
                                                            <th>
                                                                Blob Storage Path
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
                                                             OnClick="btnEdit_Click" CommandArgument='<%# Eval("BLOB_ID")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                            TabIndex="14" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("ACCOUNT_NAME")%>
                                                    </td>

                                                    <td>
                                                        <%# Eval("SECRET_KEY") %>
                                                    </td>

                                                    <td>
                                                        <%# Eval("CONTAINER_NAME")%>
                                                    </td>

                                                    <td><%# Eval("INSTANCE_NAME")%>                                                           
                                                    </td>

                                                    <td><%# Eval("ACTIVITY_NAME")%>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lblIsActive" runat="server" Text='<%# Eval("ACTIVE_STATUS")%>' ForeColor='<%# Eval("ACTIVE_STATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("BLOB_STORAGE_PATH")%>
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
        </ContentTemplate>
    </asp:UpdatePanel>


    <script>
        function SetStat(val) {
            $('#rdActive').prop('checked', val);
        }

        function validate()
        {

            $('#hfdStat').val($('#rdActive').prop('checked'));

            var txtBatchName = $("[id$=txtAccountName]").attr("id");
            var txtBatchName = document.getElementById(txtBatchName);
            // alert(txtOwnershipStatusName.value.length)
            if (txtBatchName.value.length == 0)
            {
                alert('Please Enter Account Name', 'Warning!');
                $(txtBatchName).focus();
                return false;
            }
         

            var txtSecretKey = $("[id$=txtSecretKey]").attr("id");
            var txtSecretKey = document.getElementById(txtSecretKey);
            // alert(txtOwnershipStatusName.value.length)
            if (txtSecretKey.value.length == 0) {
                alert('Please Enter Secret Key', 'Warning!');
                $(txtSecretKey).focus();
                return false;
            }


            var txtContainerName = $("[id$=txtContainerName]").attr("id");
            var txtContainerName = document.getElementById(txtContainerName);
            // alert(txtOwnershipStatusName.value.length)
            if (txtContainerName.value.length == 0) {
                alert('Please Enter Container Name', 'Warning!');
                $(txtContainerName).focus();
                return false;
            }

            var rfvActivit = (document.getElementById("<%=lblactivity.ClientID%>").innerHTML);
            var ddlActivit = $("[id$=ddlActivity]").attr("id");
            var ddlActivit = document.getElementById(ddlActivit);
            // alert(txtOwnershipStatusName.value.length)
            if (ddlActivit.value == 0)
            {
                alert('Please Select ' + rfvActivit + ' .\n', 'Warning!');
                $(ddlActivit).focus();
                return false;
            }

            var rfvInstances = (document.getElementById("<%=lblinstances.ClientID%>").innerHTML);
            var ddlInstances = $("[id$=ddlInstances]").attr("id");
            var ddlInstances = document.getElementById(ddlInstances);
            // alert(txtOwnershipStatusName.value.length)
            if (ddlInstances.value == 0) {
                alert('Please Select ' + rfvInstances + ' .\n', 'Warning!');
                $(ddlInstances).focus();
                return false;
            }

            //Added by Vinay Mishra on 26/06/2023
            var txtBlobPath = $("[id$=txtBlobStoragePath]").attr("id");
            var txtBlobPath = document.getElementById(txtBlobPath);
            // alert(txtOwnershipStatusName.value.length)
            if (txtBlobPath.value.length == 0) {
                alert('Please Enter Blob Storage Path', 'Warning!');
                $(txtBlobPath).focus();
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

