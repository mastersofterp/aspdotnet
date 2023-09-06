<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="User" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        //ADDED BY Yogesh Kumbhar 
        function previewFilePhoto() {

            var type = document.querySelector('#<%=fuimgEmpPhoto.ClientID %>').files[0].type;
            // allow jpg, png, jpeg, bmp, gif, ico
            //var type_reg = /^image\/(jpg|png|jpeg|bmp|gif|ico)$/;
            var type_reg = /^image\/(jpg|jpeg)$/;

            if (type_reg.test(type)) {
                // file is ready to upload

            } else {
                alert('Please upload jpg/jpeg file only.');
                $('#<%=imgEmpPhoto.ClientID %>').attr("src", "IMAGES/nophoto.jpg");//
                reset($('#<%=fuimgEmpPhoto.ClientID %>'));

            }

            //ADDED BY Yogesh Kumbhar
            if (validateFileSize()) {

                var preview = document.querySelector('#<%=imgEmpPhoto.ClientID %>');
                var file = document.querySelector('#<%=fuimgEmpPhoto.ClientID %>').files[0];
                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                    alet(reader.result);
                    $('#hfImgPath').val(reader.result);
                }

                if (file) {

                    reader.readAsDataURL(file);

                } else {
                    preview.src = "";
                }
            }
            else {
                alert('File size should be less than or equal to 500KB.');
                $imgEmpPhoto.val('');
                //***********************

                return;
            }

        }
        //ADDED BY Yogesh Kumbhar
        function validateFileSize() {
            var uploadControl = document.getElementById('<%= fuimgEmpPhoto.ClientID %>');
            if (uploadControl.files[0].size > 500000) {
                return false;
            }
            else {
                return true;
            }
        }
    </script>
    <script type="text/javascript">
        function Preview() {
            var fileInput = document.getElementById('<%= fuimgEmpPhoto.ClientID  %>');
            var filePreview = document.getElementById('<%= imgEmpPhoto.ClientID %>');
            var file = fileInput.files[0];
            var imageType = /image.*/;

            if (file.type.match(imageType)) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    filePreview.src = reader.result;
                }

                reader.readAsDataURL(file);
            }
            else {
                alert('Not an image file!');
            }
        }
    </script>

    <div>
        <%-- <script src="../../Scripts/jquery.js" type="text/javascript"></script>

        <script src="../../Scripts/jquery-impromptu.2.6.min.js" type="text/javascript"></script>

        <link href="../../Scripts/impromptu.css" rel="stylesheet" type="text/css" />--%>
        <%--<div>
            <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlUserProfile"
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
        <asp:UpdatePanel ID="updpnlUserProfile" runat="server">
            <ContentTemplate>
                <%--Employee Profile  ----%>
                <asp:Panel ID="pnlempprof" runat="server">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-12">
                            <div class="box box-primary">
                                <div id="div3" runat="server"></div>
                                <div class="box-header with-border">
                                    <h3 class="box-title">User Profile </h3>
                                    <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                                    <%--  <span style="color: red" class="pull-right"><span style="color: black;"><b></b></span>
                                        <b>This page only to Update User Profile</b></span>--%>
                        </div>

                        <div class="box-body">

                            <div id="divGeneralInfo" style="display: block;">
                                <div class="row">
                                    <div class="col-lg-10">
                                        <div class="row">

                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>User Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblName" runat="server"></asp:Label></a>
                                                    </li>

                                                </ul>
                                            </div>
                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Designation :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                                        </a>
                                                    </li>

                                                </ul>
                                            </div>
                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Department :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblDept" runat="server"></asp:Label>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Employee Bio ID :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblId" runat="server"></asp:Label>
                                                        </a>
                                                    </li>

                                                </ul>
                                            </div>
                                             <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>User Login ID :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblUserLoginId" runat="server"></asp:Label>
                                                        </a>
                                                    </li>

                                                </ul>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12 mt-2">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Email ID</label>
                                                </div>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <div class="fa fa-envelope text-blue"></div>
                                                    </div>
                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                                        ValidationGroup="Save" Display="None" ErrorMessage="Email ID is Required"
                                                        SetFocusOnError="true" />
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12 mt-2">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Mobile No.</label>
                                                </div>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <div class="fa fa-mobile text-blue"></div>
                                                    </div>

                                                    <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvPhoneNo" runat="server" ControlToValidate="txtPhoneNo"
                                                        ValidationGroup="Save" Display="None" ErrorMessage="Phone No is Required"
                                                        SetFocusOnError="true" />
                                                </div>
                                            </div>




                                        </div>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <%--<label>Photo :</label>--%>
                                        <asp:HiddenField ID="hfImgPath" runat="server" />
                                        <asp:Image ID="imgEmpPhoto" runat="server" Width="130px" Height="130px" BorderColor="Gray"
                                            ImageUrl="~/IMAGES/nophoto.jpg" BorderStyle="Solid" BorderWidth="1" Style="margin-bottom: 8px;" />
                                        <div id="divFileUpload" runat="server" style="display: none;">
                                            <asp:FileUpload ID="fuimgEmpPhoto" runat="server" TabIndex="1" accept=".jpg,.jpeg" onchange="previewFilePhoto()" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="fuimgEmpPhoto" runat="Server"
                                                ErrorMessage="Only jpg or jpeg files are allowed" ValidationExpression="^.*\.(jpeg|JPEG|jpg|JPG|jpe|JPE)$"
                                                Display="None" />
                                        </div>
                                        <%--<asp:Image ID="imgEmpPhoto" runat="server" Height="120px" Width="120px" />--%>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Office Location</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Block</label>
                                            </div>
                                            <asp:TextBox ID="txtBlock1" runat="server" CssClass="form-control" Enabled="False" MaxLength="25"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Room No</label>
                                            </div>
                                            <asp:TextBox ID="txtRoomNo1" runat="server" CssClass="form-control" Enabled="False" MaxLength="8"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Cabin No</label>
                                            </div>
                                            <asp:TextBox ID="txtCabinNo1" runat="server" CssClass="form-control" Enabled="False" MaxLength="8"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Intercom No</label>
                                            </div>
                                            <asp:TextBox ID="txtIntercomNo1" runat="server" CssClass="form-control" Enabled="False" MaxLength="6"></asp:TextBox>

                                        </div>


                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-md-12" id="divnotemsg" runat="server" style="display: none;">
                                        </div>
                                        <div class="form-group col-lg-12 col-md-12 col-12" id="divnote" runat="server" style="display: none;">
                                            <div class=" note-div">
                                                <h5 class="heading">Note </h5>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>You Need To Change The information Before Generating OTP </span></p>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-12 col-md-12 col-12" id="divnoteOTP" runat="server" style="display: none;">
                                            <div class=" note-div">
                                                <h5 class="heading">Note </h5>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Please Enter Your OTP and Click on Save Button </span></p>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-md-3" style="display: none">
                                            <label>Other Office Location </label>
                                            <br />
                                            <label>Block</label>
                                            <asp:TextBox ID="txtBlock2" runat="server" CssClass="form-control" Enabled="False" MaxLength="3"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-md-3" style="display: none">
                                            <label>Room No</label>
                                            <asp:TextBox ID="txtRoomNo2" runat="server" CssClass="form-control" Enabled="False" MaxLength="3"></asp:TextBox>

                                        </div>

                                        <div class="form-group col-md-3" style="display: none">
                                            <label>Cabin No </label>
                                            <asp:TextBox ID="txtCabinNo2" runat="server" CssClass="form-control" Enabled="False" MaxLength="3"></asp:TextBox>

                                        </div>

                                        <div class="form-group col-md-3" style="display: none">
                                            <label>Intercom No</label>
                                            <asp:TextBox ID="txtIntercomNo2" runat="server" CssClass="form-control" Enabled="False" MaxLength="3"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trOtpF" visible="false" runat="server">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Enter OTP</label>
                                            </div>
                                            <asp:TextBox ID="txtOtpF" runat="server" CssClass="form-control"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Intercom No </label>
                                            </div>
                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" Enabled="False" MaxLength="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtIntercomNo2"
                                                ValidationGroup="Save" Display="None" Enabled="false" ErrorMessage="Additional Office Location Intercom No is Required"
                                                SetFocusOnError="true" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tr2" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Specialization</label>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tr3" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>1 </label>
                                            </div>
                                            <asp:TextBox ID="txtSP1" runat="server"
                                                CssClass="form-control" Enabled="False"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSP1" runat="server" ControlToValidate="txtSP1"
                                                ValidationGroup="Save" Display="None" ErrorMessage="Specialization 1 is Required"
                                                SetFocusOnError="true" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tr4" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>2</label>
                                            </div>
                                            <asp:TextBox ID="txtSP2" runat="server"
                                                CssClass="form-control" Enabled="False"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSP2" runat="server" ControlToValidate="txtSP2"
                                                ValidationGroup="Save" Display="None" ErrorMessage="Specialization 2 is Required"
                                                SetFocusOnError="true" />
                                        </div>

                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tr5" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>3</label>
                                            </div>
                                            <asp:TextBox ID="txtSP3" runat="server"
                                                CssClass="form-control" Enabled="False"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSP3" runat="server" ControlToValidate="txtSP3"
                                                ValidationGroup="Save" Display="None" ErrorMessage="Specialization 3 is Required"
                                                SetFocusOnError="true" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tr6" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>4</label>
                                            </div>
                                            <asp:TextBox ID="txtSP4" runat="server"
                                                CssClass="form-control" Enabled="False"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSP4" runat="server" ControlToValidate="txtSP4"
                                                ValidationGroup="Save" Display="None" ErrorMessage="Specialization 4 is Required"
                                                SetFocusOnError="true" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tr7" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>5</label>
                                            </div>
                                            <asp:TextBox ID="txtSP5" runat="server"
                                                CssClass="form-control" Enabled="False"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSP5" runat="server" ControlToValidate="txtSP5"
                                                ValidationGroup="Save" Display="None" ErrorMessage="Specialization 5 is Required"
                                                SetFocusOnError="true" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12" id="tr1" runat="server" visible="false">
                                    <div class="sub-heading">
                                        <h5>Research Projects</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Project Name</label>
                                            </div>
                                            <asp:TextBox ID="txtProjectName" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvProjectName" runat="server" ControlToValidate="txtProjectName"
                                                ValidationGroup="Add" Display="None" ErrorMessage="Project Name is Required"
                                                SetFocusOnError="true" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Project Period From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgCalFromDate">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtProFromDate"
                                                    runat="server" Enabled="False" CssClass="form-control">
                                                </asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    Enabled="true" EnableViewState="true" Format="dd/MM/yyyy"
                                                    PopupButtonID="imgCalFromDate" TargetControlID="txtProFromDate" />
                                                <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                    Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    OnInvalidCssClass="errordate" TargetControlID="txtProFromDate" />
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                                                    ControlExtender="meeFromDate" ControlToValidate="txtProFromDate"
                                                    Display="Dynamic" EmptyValueBlurredText="*"
                                                    EmptyValueMessage="From date is required" InvalidValueBlurredMessage="*"
                                                    InvalidValueMessage="From date is invalid" IsValidEmpty="False"
                                                    ValidationGroup="Add" />
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Sponsored by</label>
                                            </div>
                                            <asp:TextBox ID="txtSponsoredBy" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSponsoredBy" runat="server" ControlToValidate="txtSponsoredBy"
                                                ValidationGroup="Add" Display="None" ErrorMessage="Sponsored By is Required"
                                                SetFocusOnError="true" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Project Period To Date </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgCalToDate">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtProToDate"
                                                    runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                    Enabled="true" EnableViewState="true" Format="dd/MM/yyyy"
                                                    PopupButtonID="imgCalToDate" TargetControlID="txtProToDate" />
                                                <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                    Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    OnInvalidCssClass="errordate" TargetControlID="txtProToDate" />
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                                    ControlExtender="meeToDate" ControlToValidate="txtProToDate" Display="Dynamic"
                                                    EmptyValueBlurredText="*" EmptyValueMessage="To date is required"
                                                    InvalidValueBlurredMessage="*" InvalidValueMessage="To date is invalid"
                                                    IsValidEmpty="False" ValidationGroup="Add" />
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Detail of Project </label>
                                            </div>
                                            <asp:TextBox ID="txtDetailProject"
                                                runat="server" Enabled="False" CssClass="form-control" Rows="1"
                                                TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDetail" runat="server"
                                                ControlToValidate="txtDetailProject" Display="None"
                                                ErrorMessage="Detail Of Project is Required" SetFocusOnError="true"
                                                ValidationGroup="Add" />
                                        </div>

                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add"
                                            ValidationGroup="Add" CssClass="btn btn-primary" />
                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List"
                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="Add" />

                                    </div>

                                    <div class="col-12">
                                        <asp:Panel ID="pnlProjectList" runat="server">
                                            <asp:ListView ID="lvProjects" runat="server" ScrollBars="Both">
                                                <LayoutTemplate>
                                                    <div id="listViewGrid" class="vista-grid">
                                                        <div class="sub-heading">
                                                            <h5>Project list</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchProjects">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Edit
                                                                    </th>
                                                                    <th>Delete
                                                                    </th>
                                                                    <th>Project Name
                                                                    </th>
                                                                    <th>Sponsored by
                                                                    </th>
                                                                    <th>From Date
                                                                    </th>
                                                                    <th>To Date
                                                                    </th>
                                                                    <th>Project Details
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEdit1" runat="server" AlternateText="Edit Record"
                                                                CausesValidation="false" CommandArgument='<%# Eval("FPNO") %>'
                                                                ImageUrl="~/images/edit.png" OnClick="btnEditProject_Click"
                                                                ToolTip='<%# Eval("FPNO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="btnDelete" runat="server"
                                                                CommandArgument='<%# Eval("FPNO") %>' ImageUrl="~/images/delete.png"
                                                                OnClick="btnDelete_Click" OnClientClick="return ConfirmSubmit();"
                                                                ToolTip='<%# Eval("FPNO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="lblProName" runat="server" ReadOnly="true"
                                                                Text='<%# Eval("PROJECT_NAME")%>' TextMode="MultiLine"
                                                                ToolTip='<%# Eval("FPNO")%>' Rows="1"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="lblSponsBy" runat="server" ReadOnly="true"
                                                                Text='<%# Eval("SPONSORED_BY")%>' TextMode="MultiLine" Rows="1"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblFromDate" runat="server" Text='<%# Eval("FROM_DATE")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblToDate" runat="server" Text='<%# Eval("TO_DATE")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="lblProjDetails" runat="server" Height="15%" ReadOnly="true"
                                                                Text='<%# Eval("PROJECT_DETAIL")%>' TextMode="MultiLine" Width="95%" Rows="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit"
                                    CssClass="btn btn-primary" />

                                <asp:HiddenField ID="hdnEditMode" runat="server" Visible="false" Value="" />
                                <asp:Button ID="btnOtpF" runat="server" Text="Generate OTP" CssClass="btn btn-info"
                                    Visible="false" OnClick="btnOtpF_Click" />
                                <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click"
                                    Text="Save" CssClass="btn btn-primary" ValidationGroup="Save" Visible="false" />
                                <asp:Button ID="empbtncancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnempbtncancel_Click" />

                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Save" />

                            </div>
                        </div>
                    </div>


                </div>
            </div>

        </asp:Panel>

        <%--Student Profile  ----%>

        <asp:Panel ID="pnlstudprof" runat="server">
            <div class="row" id="div1" style="display: block;">
                <!--academic Calendar-->
                <div class="col-12">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>Student Profile</b> </h3>
                            <asp:Label ID="Label2" runat="server" SkinID="Msglbl"></asp:Label>
                            <span style="color: red" class="pull-right"><span style="color: black;"><b></b></span><b>This page only to Update User Profile</b></span>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-md-10">
                                        <div class="row">
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Reg No.</label>
                                                </div>
                                                <asp:Label ID="lblstudid" runat="server" Style="display: none;"></asp:Label>
                                                <asp:TextBox ID="txtstudregno" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Name</label>
                                                </div>
                                                <asp:TextBox ID="txtstudname" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Branch</label>
                                                </div>
                                                <asp:Label ID="txtstudbranch" runat="server" disabled="disabled" Enabled="false" CssClass="form-control"></asp:Label>

                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Semester</label>
                                                </div>
                                                <asp:Label ID="lblstudsem" runat="server" disabled="disabled" Enabled="false" CssClass="form-control"></asp:Label>

                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Email ID </label>
                                                </div>
                                                <div class="input-group">
                                                    <div class="input-group-addon"><span class="fa fa-calender text-blue"></span></div>
                                                    <asp:TextBox ID="txtstudemail" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Mobile No.</label>
                                                </div>
                                                <div class="input-group">
                                                    <div class="input-group-addon"><span class="fa fa-mobile text-blue"></span></div>
                                                    <asp:TextBox ID="txtstudphone" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <label>Photo :</label>
                                        <asp:Image ID="imgStudPhoto" runat="server" Height="120px" Width="120px" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-md-12" id="div2" runat="server" style="display: none;">
                                    </div>
                                    <div class="form-group col-lg-6 col-md-12 col-12" id="studivotp" runat="server" style="display: none;">
                                        <div class=" note-div">
                                            <h5 class="heading">Note</h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>You Need To Change The information Before Generating OTP </span></p>

                                        </div>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-12 col-12" id="studivmsg" runat="server" style="display: none;">
                                        <div class=" note-div">
                                            <h5 class="heading">Note </h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Please Enter Your OTP and Click on Save Button</span></p>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12" id="idOtp" runat="server" visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Enter OTP</label>
                                        </div>
                                        <asp:TextBox ID="txtotp" Width="60%" Style="text-align: center; margin-left: 70px;" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>

                                </div>


                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btneditStud" runat="server" Text="Edit" CssClass="btn btn-primary" OnClick="btneditStud_Click" />
                                <asp:Button ID="btnverify" runat="server" Text="Generate OTP" CssClass="btn btn-info "
                                    Visible="false" OnClick="btnverify_Click" />

                                <asp:Button ID="btnUpdStud" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnUpdStud_Click"
                                    Visible="false" />

                            </div>


                        </div>
                    </div>

                </div>
            </div>
            <%--Change Password  ----%>
        </asp:Panel>
        <asp:Panel ID="pnlchangepwd" runat="server" Visible="false">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div4" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Change Password</h3>
                            <asp:LinkButton ID="lnkchangepwd" runat="server" OnClick="lnkchangepwd_Click" Visible="false">Change Password</asp:LinkButton>

                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Change Password</h5>
                                    <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl"></asp:Label>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Old Password</label>
                                        </div>
                                        <asp:TextBox ID="txtOldPassword" runat="server" MaxLength="20" TextMode="Password"
                                            CssClass="form-control" Wrap="False" />
                                        <asp:RequiredFieldValidator ID="rfvOldPass" runat="server" ControlToValidate="txtOldPassword"
                                            Display="None" ErrorMessage="Old Password Required" ValidationGroup="changePassword">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>New Password</label>
                                        </div>
                                        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" MaxLength="20" CssClass="form-control"
                                            Wrap="False" />
                                        <asp:RequiredFieldValidator ID="rfvNewPass" runat="server" ControlToValidate="txtNewPassword"
                                            Display="None" ErrorMessage="New Password Required" ValidationGroup="changePassword">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Confirm Password</label>
                                        </div>
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" MaxLength="20" CssClass="form-control"
                                            Wrap="False" />
                                        <asp:RequiredFieldValidator ID="rfvConfirmPass" runat="server" ControlToValidate="txtConfirmPassword"
                                            Display="None" ErrorMessage="Confirm Password Required" ValidationGroup="changePassword"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="New &amp; Confirm Password Not Matching"
                                            ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword" Display="None"
                                            ValidationGroup="changePassword"></asp:CompareValidator>

                                    </div>


                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Label ID="lblMessage" runat="server" SkinID="Errorlbl" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary"
                                    ValidationGroup="changePassword" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary"
                                    CausesValidation="False" OnClick="btnBack_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning"
                                    CausesValidation="False" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="changePassword"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </asp:Panel>
        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>


</asp:Content>
