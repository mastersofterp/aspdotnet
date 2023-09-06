<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_EmployeeBulkPhotoUpload.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_EmployeeBulkPhotoUpload" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <div style="z-index: 1; position: absolute; top: 220px; left: 500px;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="uploading">
            <ProgressTemplate>
                <asp:Image ID="imgLoad" runat="server" AlternateText="Processing..." Width="100px" Height="100px" ImageUrl="~/images/Processing.gif"></asp:Image>
                <span style="font-size: 8pt">Loading...</span>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>--%>


    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Upload Bulk Photo</h3>
                </div>

                <%-- <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#1a" tabindex="1">Upload Bulk Photo</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#2a" tabindex="2">Show Photo</a>
                            </li>
                        </ul>
                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane fade" id="1a">--%>

                <div>

                    <%--<asp:LinkButton ID="lnkItem"  class="btn btn-success" runat="server" ForeColor="Black" OnClick="lnkItem_Click"><span>Show Photo</span></asp:LinkButton>--%>
                    <asp:LinkButton ID="lnkShowphoto" class="btn btn-success" runat="server" ForeColor="Black" OnClick="lnkShowphoto_Click"><span>Show Photo</span></asp:LinkButton>


                    <asp:LinkButton ID="lnkUploadBulkPhoto" CssClass="btn btn-warning" runat="server" ForeColor="Black" OnClick="lnkUploadBulkPhoto_Click"><span>Upload Bulk Photo</span></asp:LinkButton>
                </div>

                <br />

                <asp:MultiView ID="MultiViewShowPhoto" runat="server" ActiveViewIndex="0">
                    <asp:View ID="View" runat="server">
                        <div class="box-body">
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
                                    <div class="col-12">
                                        <div class="row">

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class=" note-div">
                                                    <h5 class="heading">Note</h5>
                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>After selecting photos click on <span style="color: #00a65a">"SHOW"</span> button to verify chosen photos then upload.</span></p>
                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Image format must be [.jpg / .jpeg] [all selected files size must be less than 1GB]</span></p>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Photo Category</label>
                                                </div>
                                                <asp:DropDownList ID="ddlcategory" AutoPostBack="True" AppendDataBoundItems="true"
                                                    runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Employee Photo</asp:ListItem>
                                                    <asp:ListItem Value="2">Employee Signature</asp:ListItem>
                                                    <%-- <asp:ListItem Value="3">Student's Father</asp:ListItem>
                                            <asp:ListItem Value="4">Student's Mother</asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlcategory"
                                                    Display="None" SetFocusOnError="true" ErrorMessage="Please Select Category" ValidationGroup="academic"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Select Images</label>
                                                </div>
                                                <asp:FileUpload ID="fuStudPhoto" runat="server" accept=".jpg,.jpeg" AllowMultiple="true"></asp:FileUpload>

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
                                <asp:Button ID="btnSave" type="button" CssClass="btn btn-primary" runat="server" OnClientClick="return ProgressBar()" Text="Upload" OnClick="btnSave_Click"></asp:Button>

                                <asp:Button ID="btnShow" CssClass="btn btn-primary" ValidationGroup="academic" OnClick="btnShow_Click" Text="Show" runat="server" />
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="vsSelection" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="academic" />

                            </div>



                            <asp:PlaceHolder ID="PlaceHolder1" runat="server" />

                            <div class="col-12" id="listview_div">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                    <ContentTemplate>
                                        <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="itemPlaceHolder">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="id1">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>SR.NO</th>
                                                            <th>REG NO</th>
                                                            <th>SIZE</th>
                                                            <th>ACTION</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="lstview_body">
                                                        <tr id="itemPlaceholder" runat="server"></tr>
                                                    </tbody>

                                                </table>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item">
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
                        <%--</div>
                              

                            <div class="tab-pane active" id="2a">--%>
                    </asp:View>
                    <asp:View ID="View1" runat="server" EnableTheming="true">
                        <div class="box-body">
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
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Show Photo</h5>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>College</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true"
                                                    runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                                    Display="None" SetFocusOnError="true" ErrorMessage="Please Select College" ValidationGroup="Acd"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <%--<label>Staff</label>--%>
                                                    <label>Scheme/Staff</label>
                                                </div>
                                                <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true"
                                                    runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Department</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDepartment" AppendDataBoundItems="true"
                                                    runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlType" AppendDataBoundItems="true"
                                                    runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Having Photo</asp:ListItem>
                                                    <asp:ListItem Value="2">Not Having Photo</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlType"
                                                    Display="None" SetFocusOnError="true" ErrorMessage="Please Select Type" ValidationGroup="Acd"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                  <%--  <sup>*</sup>--%>
                                                    <label>Photo Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlphototype" AppendDataBoundItems="true"
                                                    runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Employee Photo</asp:ListItem>
                                                    <asp:ListItem Value="2">Employee Signature</asp:ListItem>
                                                </asp:DropDownList>
                                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlphototype"
                                                    Display="None" SetFocusOnError="true" ErrorMessage="Please photo Type" ValidationGroup="Acd"
                                                    InitialValue="0"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hidTAB" runat="server" Value="1a" />
                                </ContentTemplate>

                            </asp:UpdatePanel>

                            <div class="form-group col-md-12" style="text-align: center">
                                <label style="margin: 10px"></label>
                                <div class="col-lg-12">
                                    <asp:Button ID="btnShowReport" class="btn btn-success" ValidationGroup="Acd" OnClick="btnShowReport_Click" Text="Show Report" runat="server" />
                                    <asp:Button ID="Button3" runat="server" OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Acd" />
                                </div>
                            </div>

                        </div>
                    </asp:View>
                </asp:MultiView>

                <%--/div>
     
                        </div>
                    </div>
                </div>--%>
            </div>

        </div>
        <%-- </div> --%>

        <div class="modal fade" id="myModal1" role="dialog">
            <div class="modal-dialog" style="width: 30%">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="box-body modal-warning">
                                <div class="form-group" style="text-align: center">
                                    <asp:Label ID="lblmessageShow" Style="font-weight: bold" runat="server" Text="Regno"></asp:Label>
                                </div>
                                <div class="box-footer">
                                    <p class="text-center">
                                        <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-default"
                                            data-dismiss="modal" />
                                    </p>
                                </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div id="divMsg" runat="server"></div>

        <script type="text/javascript">
            window.onsubmit = function () {
                //if (Page_IsValid) {
                var updateProgress = $find("<%= UpdateProgress1.ClientID %>");

            window.setTimeout(function () {
                updateProgress.set_visible(true);
            }, 10);
            //}
        }
        </script>
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
        </script>
</asp:Content>



