<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentBulkPhotoUpload.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_StudentBulkPhotoUpload" %>



<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1">Upload Photo</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2">Show Photo</a>
                            </li>
                        </ul>

                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <div>
                                    <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="uploading"
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

                                <asp:UpdatePanel ID="uploading" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Photo Category</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlcategory" AutoPostBack="True" AppendDataBoundItems="true"
                                                        runat="server" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Student Photo</asp:ListItem>
                                                        <asp:ListItem Value="2">Student Signature</asp:ListItem>
                                                        <asp:ListItem Value="3">Student's Father</asp:ListItem>
                                                        <asp:ListItem Value="4">Student's Mother</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlcategory"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select Category" ValidationGroup="academic"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Select Images</label>
                                                    </div>
                                                    <asp:FileUpload ID="fuStudPhoto" runat="server" accept=".jpg,.jpeg" AllowMultiple="true"></asp:FileUpload>
                                                    <span>
                                                        <p style="color: red; padding-top: 5px">Note: Image format must be [.jpg / .jpeg]</p>
                                                    </span>
                                                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="fuStudPhoto"
                                                        ErrorMessage="Please Select Images" Display="None" ValidationGroup="academic"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" CssClass="btn btn-primary" ValidationGroup="academic" OnClick="btnShow_Click" Text="Show" runat="server" />
                                    <asp:Button ID="btnSave" type="button" CssClass="btn btn-primary" runat="server" OnClientClick="return ProgressBar()" Text="Upload" OnClick="btnSave_Click"></asp:Button>
                                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="vsSelection" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="academic" />

                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
                                </div>

                                <div class="col-12" id="listview_div">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                        <ContentTemplate>
                                            <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="itemPlaceHolder">
                                                <LayoutTemplate>
                                                    <div class="table-responsive">
                                                        <table class="table table-striped table-bordered display" style="width: 100%" id="id1">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>SR.NO</th>
                                                                    <th>
                                                                        <asp:Label runat="server" ID="lblDYRRNo" Font-Bold="true"></asp:Label></th>
                                                                    <th>SIZE</th>
                                                                    <th>ACTION</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server"></tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("SRNO") %></td>
                                                        <td>
                                                            <asp:Label runat="server" Text='<%# Eval("NAME") %>' ID="RegNo"></asp:Label></td>
                                                        <td><%# Eval("SIZE") %></td>
                                                        <td>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument='<%# Eval("ACTION") %>' ImageUrl="~/images/delete.gif" ToolTip="Delete" OnClick="btnDelete_Click" OnClientClick="return ConfirmSubmit();" /></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <div class="tab-pane fade" id="tab_2">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
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

                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label runat="server" ID="lblDYddlColgScheme" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlClgScheme" AutoPostBack="true" AppendDataBoundItems="true"
                                                        runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlClgScheme_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlClgScheme" Enabled="false"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select College&Scheme" ValidationGroup="Acd"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label runat="server" ID="lblDYddlAdmBatch" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlAdmBatch" AutoPostBack="true" AppendDataBoundItems="true"
                                                        runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAdmBatch"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select Admission Year" ValidationGroup="Acd"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label runat="server" ID="lblDYddlDegree" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegree" AutoPostBack="True" AppendDataBoundItems="true"
                                                        runat="server" CssClass="form-control" data-select2-enable="true"
                                                        OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDegree" Enabled="false"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select Degree" ValidationGroup="Acd"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>
                                                            <asp:Label runat="server" ID="lblDYddlBranch" Font-Bold="true"></asp:Label></label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBranch" AutoPostBack="true" AppendDataBoundItems="true"
                                                        runat="server" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlBranch" Enabled="false"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select Branch" ValidationGroup="Acd"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlType" AutoPostBack="true" AppendDataBoundItems="true"
                                                        runat="server" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Having Photo</asp:ListItem>
                                                        <asp:ListItem Value="2">Not Having Photo</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" ControlToValidate="ddlType"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select Type" ValidationGroup="Acd"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Photo Category</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlPhotoCategory" AutoPostBack="true" AppendDataBoundItems="true"
                                                        runat="server" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Student's Photo</asp:ListItem>
                                                        <asp:ListItem Value="2">Student's Signature</asp:ListItem>
                                                        <asp:ListItem Value="3">Student's Father</asp:ListItem>
                                                        <asp:ListItem Value="4">Student's Mother</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" ControlToValidate="ddlPhotoCategory"
                                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select Photo Category" ValidationGroup="Acd"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:HiddenField ID="hidTAB" runat="server" Value="1a" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShowReport" CssClass="btn btn-primary" OnClick="btnShowReport_Click" OnClientClick="return validation();" Text="Show Report" runat="server" />
                                    <asp:Button ID="Button3" runat="server" OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Acd" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

                <!-- The Modal -->
                <div class="modal fade" id="myModal1">
                    <div class="modal-dialog">
                        <div class="modal-content">

                            <!-- Modal Header -->
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>

                            <!-- Modal body -->
                            <div class="modal-body">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 btn-footer">
                                            <asp:Label ID="lblmessageShow" Style="font-weight: bold" runat="server" Text="Regno"></asp:Label>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-warning" data-dismiss="modal" />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <!-- Modal footer -->
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                            </div>

                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>


    <div id="divMsg" runat="server"></div>

    <%-- <script type="text/javascript">
        window.onsubmit = function () {
            //if (Page_IsValid) {
                var updateProgress = $find("<%= UpdateProgress1.ClientID %>");

                window.setTimeout(function () {
                    updateProgress.set_visible(true);
                }, 10);
            //}
        }
    </script>--%>
    <script type="text/javascript">
        $(function () {
            $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                localStorage.setItem('lastTab', $(this).attr('href'));
            });
            var lastTab = localStorage.getItem('lastTab');
            if (lastTab) {
                $('[href="' + lastTab + '"]').tab('show');
            }
        });

    </script>

    <script type="text/javascript">

        function ConfirmSubmit() {
            var ret = confirm('Are you sure to remove this photo ?');
            if (ret == true)
                return true;
            else
                return false;
        }
    </script>
    <script type="text/javascript">
        function showModal() {
            $("#myModal1").modal('show');
        }

        function validation() {
            try{
                var ddlClgScheme = $("#ctl00_ContentPlaceHolder1_ddlClgScheme").val();
                var ddlAdmBatch = $("#ctl00_ContentPlaceHolder1_ddlAdmBatch").val();
                var ddlType = $("#ctl00_ContentPlaceHolder1_ddlType").val();
                var ddlPhotoCategory = $("#ctl00_ContentPlaceHolder1_ddlPhotoCategory").val();
                var lblClgScheme = $("#ctl00_ContentPlaceHolder1_lblDYddlColgScheme").text();
                var lblAdmBatch = $("#ctl00_ContentPlaceHolder1_lblDYddlAdmBatch").text();
                var msg="";
                if (ddlClgScheme == 0) {
                    msg+="Please Select "+lblClgScheme+".\n"
                }
                if (ddlAdmBatch == 0) {
                    msg += "Please Select " + lblAdmBatch + ".\n"
                }
                if (ddlType == 0) {
                    msg += "Please Select Type.\n"
                }
                if (ddlPhotoCategory == 0) {
                    msg += "Please Select Photo Category."
                }
                if (msg != "") {
                    alert(msg);
                    return false;
                }
                else {
                    return true;
                }
            }
            catch (err) {
                alert(err.message);
            }
        }
    </script>
</asp:Content>

