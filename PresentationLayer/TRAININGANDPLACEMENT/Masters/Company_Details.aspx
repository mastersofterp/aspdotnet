<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Company_Details.aspx.cs" Inherits="Company_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="<%=Page.ResolveClientUrl("../plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />--%>
    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <%--<link href="../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />--%>
       <style type="text/css">
        .modalBackground {
            background-color: black;
            filter: alpha(opacity=60);
            opacity: 0.9;
        }

        .modalPopup {
            background-color: white;
            padding-top: 10px;
            padding-bottom: 10px;
            padding-left: 10px;
            padding-right: 20px;
            width: 500px;
            height: 500px;
            overflow-y: auto;
        }

        .ledgermodalBackground {
            background-color: Gray;
            filter: alpha(opacity=60);
            opacity: 0.9;
        }

        .ledgermodalPopup {
            background-color: #e5ecf9;
            border-width: 3px;
            border-style: double;
            padding-top: 10px;
            padding-bottom: 10px;
            padding-left: 10px;
            padding-right: 20px;
            width: 80%;
            height: 600px;
        }
    </style>
    <script type="text/javascript">
        //var jq = $.noConflict();

        function ShowpImagePreview(input) {
            if (input.files && input.files[0]) {;
                var reader = new FileReader();
                reader.onload = function (e) {
                    jq('#ctl00_ContentPlaceHolder1_imgUpFile').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }




        function LoadImage() {
            alert('d');
            document.getElementById("ctl00_ContentPlaceHolder1_imgUpFile").src = document.getElementById("ctl00_ContentPlaceHolder1_imgUpFile").value;
        }
    </script>
    <style>
        .badge {
            display: inline-block;
            padding: 5px 10px 7px;
            border-radius: 15px;
            font-size: 100%;
            width: 80px;
        }

        .badge-warning {
            color: #fff !important;
        }
    </style>

    <style>
        /*FIle download*/
        .download-excel:hover {
            text-decoration: none !important;
        }

        .download-excel .filelabel-download {
            border-color: var(--main-secondary-modern);
        }

            .download-excel .filelabel-download:hover i {
                border-color: #1665c4;
                text-decoration: none !important;
            }

            .download-excel .filelabel-download:hover {
                border: 2px solid #6777ef;
            }

            .download-excel .filelabel-download .name {
                color: #1665c4;
            }

            .download-excel .filelabel-download:hover .name {
                color: #1665c4;
                text-decoration: underline;
            }

        /*File UPload*/
        .filelabel, .filelabel-download {
            border: 2px dashed grey;
            border-radius: 5px;
            display: block;
            padding: 30px;
            transition: border 300ms ease;
            cursor: pointer;
            text-align: center;
            margin: auto;
        }

            .filelabel i, .filelabel-download i {
                display: block;
                font-size: 30px;
                padding-bottom: 5px;
            }

            .filelabel i, .filelabel .title, .filelabel-download i {
                color: grey;
                transition: 200ms color;
            }

            .filelabel:hover {
                border: 2px solid #1665c4;
            }

                .filelabel:hover i, .filelabel-download:hover i {
                    color: #1665c4;
                    text-decoration: none;
                }

                .filelabel:hover .title {
                    color: var(--main-black-modern);
                }

            .filelabel .choosefile {
                color: #1665c4;
            }

                .filelabel .choosefile:hover {
                    color: #1665c4;
                    cursor: pointer;
                    text-decoration: underline;
                }

            .filelabel .warning {
                font-size: 12px;
                font-weight: 600;
                display: none;
                color: red;
            }

        .file-upload-all {
            display: none !important;
        }
    </style>

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#MyTable_Company').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,

                dom: 'lBfrtip',

                //Export functionality
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#MyTable_Company').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                                {
                                    extend: 'copyHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#MyTable_Company').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                                {
                                    extend: 'excelHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#MyTable_Company').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                                {
                                    extend: 'pdfHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#MyTable_Company').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#MyTable_Company').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,

                    dom: 'lBfrtip',

                    //Export functionality
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#MyTable_Company').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                                    {
                                        extend: 'copyHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#MyTable_Company').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'excelHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#MyTable_Company').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'pdfHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#MyTable_Company').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });
    </script>

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#Approve_Company').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,

                dom: 'lBfrtip',

                //Export functionality
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#Approve_Company').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                                {
                                    extend: 'copyHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#Approve_Company').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                                {
                                    extend: 'excelHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#Approve_Company').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                                {
                                    extend: 'pdfHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#Approve_Company').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#Approve_Company').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,

                    dom: 'lBfrtip',

                    //Export functionality
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#Approve_Company').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                                    {
                                        extend: 'copyHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#Approve_Company').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'excelHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#Approve_Company').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'pdfHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#Approve_Company').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });
    </script>
    <%--    <style>
     .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>--%>
    <style>
        ctl00_ContentPlaceHolder1_upnlAddCompany# .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="row">

                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><span>Company Details</span></h3>
                        </div>
                        <div id="Tabs" role="tabpanel">
                        <div class="box-body">
                            <div class="nav-tabs-custom" id="tabs">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1" id="tab1" runat="server">Add Company</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2" id="tab2" runat="server">Company Locations</a>
                                    </li>
                                    <%--Shaikh Juned 14-11-2022--%>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_3" id="tab3" runat="server">Approve Company</a>
                                    </li>
                                </ul>

                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab_1">

                                        <asp:UpdatePanel ID="upnlAddCompany" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12 mt-3">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="float-right">
                                                                <asp:LinkButton ID="btnExportPop" runat="server" CssClass="btn btn-outline-primary" data-toggle="modal" data-target="#Ex_port" Visible="false">Bulk Upload</asp:LinkButton>
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-5 col-md-6 col-12">
                                                            <%--<div class=" note-div">
                                                                <h5 class="heading">Note </h5>
                                                                 <p><i class="fa fa-star" aria-hidden="true"></i><span>Enter Company With Location</span> </p>
                                                             </div>--%>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Company Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" autocomplete="off" TabIndex="1" MaxLength="100" onkeypress="return alphaOnly(event);" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                                    TargetControlID="txtCompanyName" FilterType="Custom" FilterMode="InvalidChars"
                                                                    InvalidChars="~`!@#$%^*+=,/:;<>?'{}[]\|-&&quot;'" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                    ControlToValidate="txtCompanyName" Display="None"
                                                                    ErrorMessage="Please Enter Company Name." SetFocusOnError="True"
                                                                    ValidationGroup="show" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Short Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtShortName" runat="server" CssClass="form-control" TabIndex="2" MaxLength="80" onkeypress="return alphaOnly(event);" />

                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                                    TargetControlID="txtShortName" FilterType="Custom" FilterMode="InvalidChars"
                                                                    InvalidChars="~`!@#$%^*+=,/:;<>?'{}[]\|-&&quot;'" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>CIN/Registration No.</label>
                                                                </div>
                                                                <asp:TextBox ID="txtRegistrationNo" runat="server" CssClass="form-control" TabIndex="3" MaxLength="80" onkeypress="return Validatecin(this);" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                                    TargetControlID="txtRegistrationNo" FilterType="Custom" FilterMode="InvalidChars"
                                                                    InvalidChars="~`!@#$%^*+=,/()_:;<>?'{}[]\|-&&quot;'" />

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Website</label>
                                                                </div>

                                                                <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control" TabIndex="4" MaxLength="80" onblur="validate(this);" />
                                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server"
                                                                    ControlToValidate="txtWebsite" Display="None"
                                                                    ErrorMessage="Please Enter Website." SetFocusOnError="True"
                                                                    ValidationGroup="show" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Logo</label>
                                                                </div>
                                                                <div id="Div1" class="logoContainer" runat="server">
                                                                    <%--  ImageUrl="~/Images/default-fileupload.png"--%>
                                                                    <%-- <img src="../Images/default-fileupload.png"  alt="upload image" runat="server" id="imgUpFile" />--%>
                                                                    <%--<asp:Image ID="imgUpFile" runat="server" ToolTip="Select logo"
                                                    AlternateText="Select logo" Style="cursor: pointer" />--%>
                                                                    <asp:Image ID="imgUpFile" runat="server" ImageUrl="~/Images/default-fileupload.png" /><br />
                                                                    <%--  Height="128px"
                                                        Width="128px"--%>
                                                                    <%-- <asp:FileUpload ID="fuplEmpPhoto" runat="server" ToolTip="Please Browse Photo"
                                                        TabIndex="22" onchange="ShowpImagePreview(this);" />--%>
                                                                </div>
                                                                <div class="fileContainer sprite pl-1">
                                                                    <span runat="server" id="ufFile"
                                                                        cssclass="form-control" tabindex="7">Upload File</span>
                                                                    <asp:FileUpload ID="fuCollegeLogo" runat="server" ToolTip="Select file to upload"
                                                                        CssClass="form-control" accept=".jpg,.jpeg,.JPG,.JPEG,.PNG" onchange="ShowpImagePreview(this);" onkeypress="" TabIndex="5" />
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Job Sector</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlJobSector" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="6">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvstudent" runat="server" ControlToValidate="ddlJobSector"
                                                                    Display="None" ErrorMessage="Please Select Job Sector." ValidationGroup="show"
                                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Career Areas</label>
                                                                </div>
                                                                <asp:ListBox ID="lstbxCareerAreas" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true" TabIndex="7"></asp:ListBox>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Association for</label>
                                                                </div>
                                                                <asp:ListBox ID="lstbxAssociation" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true" TabIndex="8"></asp:ListBox>
                                                            </div>
                                                        </div>
                                                        <%--//---Start---//--%>
                                                        <%--     <div class="sub-heading">
                                                                <h5>Company Location</h5>
                                                            </div>--%>
                                                        <%-- <div class="row">
                                                           <div class="form-group col-lg-3 col-md-6 col-12" id="divComp" runat="server" visible="false"> 
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Company Name</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCompanyName"
                                                                Display="None" ErrorMessage="Please Select Company Name." ValidationGroup="CompLoc"
                                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Location Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtLocationName" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="100" onkeypress="return alphaOnly(event);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                                TargetControlID="txtLocationName" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'0-9" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Address</label>
                                                            </div>--%>
                                                        <%--<textarea class="form-control" rows="1"></textarea>--%>
                                                        <%--   <asp:TextBox ID="txtaddress" runat="server" CssClass="form-control" TextMode="MultiLine" Style="width: 240px; height: 35px;" MaxLength="300"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                                ControlToValidate="txtaddress" Display="None"
                                                                ErrorMessage="Please Enter Address." SetFocusOnError="True"
                                                                ValidationGroup="CompLoc" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>City</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Contact Person Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control" AutoComplete="off" onkeypress="return alphaOnly(event);" MaxLength="100" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                                TargetControlID="txtContactPerson" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Designation</label>
                                                            </div>--%>
                                                        <%-- <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>--%>
                                                        <%--  <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" onkeypress="return alphaOnly(event);"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                                TargetControlID="txtDesignation" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Official Contact No.</label>
                                                            </div>
                                                            <asp:TextBox ID="txtOfficialContact" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);" MaxLength="10" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtOfficialContact">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Official Mail ID</label>
                                                            </div>
                                                            <asp:TextBox ID="txtOfficialMail" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="80" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtOfficialMail"
                                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                ErrorMessage="Please Enter Valid EmailID" ValidationGroup="company"></asp:RegularExpressionValidator>
                                                        </div>

                                                            </div>--%>
                                                        <%--  //-----end---//--%>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:LinkButton ID="btnSubmitAddCompany" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSubmitAddCompany_Click" ValidationGroup="show" TabIndex="9">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnCancelAddCompany" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelAddCompany_Click" OnClientClick="return clearmulti();" TabIndex="10">Cancel</asp:LinkButton>
                                                    <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-outline-danger" OnClick="btnShow_Click"  TabIndex="11">Show</asp:LinkButton>
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="show"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:ListView ID="lvAddCompany" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Company List</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Edit</th>
                                                                        <th>Sr.No.</th>
                                                                        <th>Logo</th>
                                                                        <th>Company Name</th>
                                                                        <th>CIN/Registration No.</th>
                                                                        <th>WebSite</th>
                                                                        <th>Job Sector</th>
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
                                                                    <asp:ImageButton ID="btnEditAddCompany" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o"
                                                                        CommandArgument='<%# Eval("COMPID") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditAddCompany_Click" />
                                                                </td>
                                                                <td>
                                                                    <%# Container.DataItemIndex + 1%>
                                                                </td>
                                                                <td>

                                                                    <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# String.Format( "data:image/jpeg;base64,{0}", Convert.ToBase64String((byte[])Eval("LOGO")) ) %>' Height="30" Width="30" />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("COMPNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("COMPREGNO")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("WEBSITE")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("JOBSECTOR")%>
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="btnEditAddCompany" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o"
                                                                        CommandArgument='<%# Eval("COMPID") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditAddCompany_Click" />
                                                                </td>
                                                                <td>
                                                                    <%# Container.DataItemIndex + 1%>
                                                                </td>
                                                                <td>

                                                                    <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# String.Format("data:image/jpeg;base64,{0}", Convert.ToBase64String((byte[])Eval("LOGO")) ) %>' Height="30" Width="30" />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("COMPNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("COMPREGNO")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("WEBSITE")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("JOBSECTOR")%>
                                                                </td>

                                                            </tr>
                                                        </AlternatingItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <%-- <asp:PostBackTrigger ControlID="btnSubmitAddCompany" />
                                        <asp:PostBackTrigger ControlID="lvAddCompany" />
                                        <asp:PostBackTrigger ControlID="btnCancelAddCompany" />--%>
                                                <%--<asp:PostBackTrigger ControlID ="fuCollegeLogo" />--%>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane fade" id="tab_2">
                                        <asp:UpdatePanel ID="upnlCompanyLocation" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12 mt-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Company Name</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCompanyName"
                                                                Display="None" ErrorMessage="Please Select Company Name." ValidationGroup="CompLoc"
                                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Location Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtLocationName" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="100" onkeypress="return alphaOnly(event);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                                TargetControlID="txtLocationName" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'0-9" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Address</label>
                                                            </div>
                                                            <%--<textarea class="form-control" rows="1"></textarea>--%>
                                                            <asp:TextBox ID="txtaddress" runat="server" CssClass="form-control" TextMode="MultiLine" Style="width: 240px; height: 35px;" MaxLength="300"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                ControlToValidate="txtaddress" Display="None"
                                                                ErrorMessage="Please Enter Address." SetFocusOnError="True"
                                                                ValidationGroup="CompLoc" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Country</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>State</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>City</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Contact Person Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control" AutoComplete="off" onkeypress="return alphaOnly(event);" MaxLength="100" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                                TargetControlID="txtContactPerson" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Contact Person Designation</label>
                                                            </div>
                                                            <%-- <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>--%>
                                                            <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" onkeypress="return alphaOnly(event);"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                TargetControlID="txtDesignation" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Official Contact No.</label>
                                                            </div>
                                                            <asp:TextBox ID="txtOfficialContact" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);" MaxLength="10" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftrPermPIN" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtOfficialContact">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Official Mail ID</label>
                                                            </div>
                                                            <asp:TextBox ID="txtOfficialMail" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="80" />
                                                            <asp:RegularExpressionValidator ID="revPermEmailId" runat="server" ControlToValidate="txtOfficialMail"
                                                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                ErrorMessage="Please Enter Valid EmailID" ValidationGroup="company"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:LinkButton ID="btnSubmitCompanyLocation" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSubmitCompanyLocation_Click" ValidationGroup="CompLoc">Submit</asp:LinkButton>
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="CompLoc" />
                                                    <asp:LinkButton ID="btnCancelComapnyLocation" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelComapnyLocation_Click">Cancel</asp:LinkButton>
                                                    <asp:LinkButton ID="btnShow1" runat="server" CssClass="btn btn-outline-danger" OnClick="btnShow1_Click">Show</asp:LinkButton>
                                                    <%--   <asp:Button ID="btnCancelComapnyLocation" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelComapnyLocation_Click" Text="Cancel" />--%>
                                                </div>
                                                <div class="col-12 mt-3">
                                                    <asp:ListView ID="lvComapnyLocation" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Company List</h5>
                                                            </div>

                                                            <table class="table table-striped table-bordered" style="width: 100%" id="MyTable_Company">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Edit</th>
                                                                       <%-- <th>Sr.No.</th>--%>
                                                                        <th>Company Name</th>
                                                                        <th>Location Name</th>
                                                                        <<%--th>Company Address</th>
                                                                        <th>City</th>--%><th>Contact Person Name</th>
                                                                        <th>Designation</th>
                                                                        <th>Official Contact Mumber</th>
                                                                        <th>Official Mail ID</th>
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
                                                                    <asp:ImageButton ID="btnEditComapnyLocation" runat="server" CssClass="fa fa-pencil-square-o" ImageUrl="~/Images/edit.png"
                                                                        CommandArgument='<%# Eval("COMPLOID") %>' AlternateText="Edit Record" ToolTip="Edit Records" OnClick="btnEditComapnyLocation_Click" />
                                                                </td>
                                                                <%--<td>
                                                                    <%# Container.DataItemIndex + 1%>
                                                                </td>--%>
                                                                <td>
                                                                    <%# Eval("COMPNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("LOCATIONNAME")%>
                                                                </td>
                                                                <%--<td>
                                                                    <%# Eval("COMPADD")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("CITY")%>
                                                                </td>--%>
                                                                <td>
                                                                    <%# Eval("CONTPERSON")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("CONTDESIGNATION")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("PHONENO")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("EMAILID")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="btnEditComapnyLocation" runat="server" CssClass="fa fa-pencil-square-o" ImageUrl="~/Images/edit.png"
                                                                        CommandArgument='<%# Eval("COMPLOID") %>' AlternateText="Edit Record" ToolTip="Edit Records" OnClick="btnEditComapnyLocation_Click" />
                                                                </td>
                                                               <%-- <td>
                                                                    <%# Container.DataItemIndex + 1%>
                                                                </td>--%>
                                                                <td>
                                                                    <%# Eval("COMPNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("LOCATIONNAME")%>
                                                                </td>
                                                               <%-- <td>
                                                                    <%# Eval("COMPADD")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("CITY")%>
                                                                </td>--%>
                                                                <td>
                                                                    <%# Eval("CONTPERSON")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("CONTDESIGNATION")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("PHONENO")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("EMAILID")%>
                                                                </td>
                                                            </tr>
                                                        </AlternatingItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                                <asp:HiddenField ID="hidtab" runat="server" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnSubmitCompanyLocation" />
                                                <asp:AsyncPostBackTrigger ControlID="lvComapnyLocation" />
                                                <asp:PostBackTrigger ControlID="btnCancelComapnyLocation" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane fade" id="tab_3">
                                        <asp:UpdatePanel ID="upnlCompanyApproved" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12 mt-3 mb-3">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="form-check-inline">
                                                                <label class="form-check-label">
                                                                    <asp:RadioButton ID="rdbNewReq" CssClass="form-check-input" runat="server" Text="New Request" OnCheckedChanged="rdbNewReq_CheckedChanged" GroupName="reqstat" AutoPostBack="true" />
                                                                    <%--<input type="radio" class="form-check-input" name="optradio">New Request--%>
                                                                </label>
                                                            </div>
                                                            <div class="form-check-inline">
                                                                <label class="form-check-label">
                                                                    <asp:RadioButton ID="rdbAccepted" CssClass="form-check-input" runat="server" Text="Accepted" OnCheckedChanged="rdbAccepted_CheckedChanged" GroupName="reqstat" AutoPostBack="true" />
                                                                    <%-- <input type="radio" class="form-check-input" name="optradio">Accepted--%>
                                                                </label>
                                                            </div>
                                                            <div class="form-check-inline disabled">
                                                                <label class="form-check-label">
                                                                    <asp:RadioButton ID="rdbRejected" CssClass="form-check-input" runat="server" Text="Rejected" OnCheckedChanged="rdbRejected_CheckedChanged" GroupName="reqstat" AutoPostBack="true" />
                                                                    <%-- <input type="radio" class="form-check-input" name="optradio">Rejected --%>
                                                                </label>
                                                            </div>
                                                            <div class="form-check-inline disabled">
                                                                <label class="form-check-label">
                                                                    <asp:RadioButton ID="rdbAll" CssClass="form-check-input" runat="server" Text="All" OnCheckedChanged="rdbAll_CheckedChanged" GroupName="reqstat" AutoPostBack="true" Checked="true" />
                                                                    <%--<input type="radio" class="form-check-input" name="optradio" checked>All--%>
                                                                </label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <div class="col-12 mt-3">
                                            <asp:UpdatePanel ID="upnllvcompapp" runat="server">
                                                <ContentTemplate>
                                                    <asp:ListView ID="lvCompApproved" runat="server">
                                                        <LayoutTemplate>
                                                            <table class="table table-striped table-bordered" style="width: 100%" id="Approve_Company">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Company Name</th>
                                                                        <th>Short Name</th>
                                                                        <th>Website</th>
                                                                        <th>Job Sector</th>
                                                                        <th>Requested By</th>
                                                                        <th>Process</th>
                                                                        <th>Status</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("COMPNAME") %></td>
                                                                <td><%# Eval("COMPCODE") %></td>
                                                                <td><%# Eval("WEBSITE") %></td>
                                                                <td><%# Eval("JOBSECTOR") %></td>
                                                                <td><%# Eval("REQUESTED BY") %></td>
                                                                <td>
                                                                    <asp:LinkButton ID="btnProcessRequest" runat="server" CssClass="btn btn-outline-primary" CommandArgument='<%# Eval("COMPID") %>' OnClick="btnProcessRequest_Click">Process Request</asp:LinkButton>
                                                                </td>
                                                                <%--<td class="text-center"><span class="badge badge-success"><%# Eval("REQUESTSTATUS") %></span></td>--%>
                                                                <td class="text-center">
                                                                    <asp:Label ID="lblstatus" CssClass="badge" runat="server" Text='<%# Eval("REQUESTSTATUS")%>'></asp:Label></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                            <//div>


                        <!-- The Modal Process Request -->


                    </div>

                </div>
            </div>


        </ContentTemplate>


        <Triggers>

            <asp:PostBackTrigger ControlID="btnSubmitAddCompany" />
            <%-- <asp:PostBackTrigger ControlID="fuCollegeLogo" />--%>
            <%-- <asp:PostBackTrigger ControlID="imgUpFile" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>
  <%--  //---start---modal--%>


     <%-- <div class="row">
            <div class="col-md-2">
                <asp:Button ID="btnForPopUpModel" Style="display: none" runat="server" Text="For PopUp Model Box" />
            </div>
            <div class="col-md-5">
                <ajaxToolKit:ModalPopupExtender ID="upd_ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" PopupControlID="upnlreqprocess" TargetControlID="btnForPopUpModel" DynamicServicePath=""
                    Enabled="True">
                </ajaxToolKit:ModalPopupExtender>--%>

               <div class="modal fade" id="process_request">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:UpdatePanel ID="upnlreqprocess" runat="server">
                    <ContentTemplate>
                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">Company Details</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>

                        <!-- Modal body -->
                        <div class="modal-body">
                            <asp:Panel ID="pnlcompdetails" runat="server">
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Company Name</label>
                                            </div>
                                            <asp:TextBox ID="txtACompName" runat="server" CssClass="form-control" Enabled="false" MaxLength="28" />
                                        </div>

                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Short Name</label>
                                            </div>
                                            <asp:TextBox ID="txtAshortName" runat="server" CssClass="form-control" Enabled="false" MaxLength="18" />
                                        </div>

                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>CIN/Registration No.</label>
                                            </div>
                                            <asp:TextBox ID="txtARegno" runat="server" CssClass="form-control" MaxLength="18" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtARegno" ForeColor="Red"
                                                ValidationExpression="[a-zA-Z0-9]*$" ErrorMessage="*Valid characters: Alphabets and Numbers." ValidationGroup="company" />
                                        </div>

                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Website</label>
                                            </div>
                                            <asp:TextBox ID="txtAwebsite" runat="server" CssClass="form-control" Enabled="false" MaxLength="12" />
                                        </div>

                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Logo</label>
                                            </div>
                                            <div id="Div2" class="logoContainer" runat="server">
                                                <img src="~/Images/default-fileupload.png" alt="upload image" runat="server" id="img1" tabindex="6" />
                                            </div>

                                            <div class="fileContainer sprite pl-1">
                                                <span runat="server" id="Span1" cssclass="form-control" tabindex="7">Upload File</span>
                                                <asp:FileUpload ID="fuALogo" runat="server" ToolTip="Select file to upload"
                                                    CssClass="form-control" accept=".jpg,.jpeg,.JPG,.JPEG,.PNG" onkeypress="" Width="50" Height="200" />
                                            </div>
                                        </div>


                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Job Sector</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAJobSec" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" Enabled="false">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Career Areas</label>
                                            </div>
                                            <asp:ListBox ID="lstbxCareerArea" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>
                                        </div>

                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Association for</label>
                                            </div>
                                            <asp:ListBox ID="lstbxAssociate" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>
                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>


                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Remark</label>
                                        </div>
                                        <%-- <textarea class="form-control" rows="2"></textarea>--%>
                                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="330" Height="60" MaxLength="300"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnAddCompany" runat="server" CssClass="btn btn-outline-primary" OnClick="btnAddCompany_Click">Add Company</asp:LinkButton>
                                <asp:LinkButton ID="btnRejectCompany" runat="server" CssClass="btn btn-outline-danger" OnClick="btnRejectCompany_Click">Reject Company</asp:LinkButton>
                                <asp:LinkButton ID="btncanceladdcomp" runat="server" CssClass="btn btn-outline-danger" OnClick="btncanceladdcomp_Click">Cancel</asp:LinkButton>
                            </div>
                            <%--<div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnSubmitCom" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSubmitCom_Click">Submit</asp:LinkButton>
                                        </div>--%>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
                                                      <%--  </div>
                                                    </div>--%>

   

    <%--  //---end---modal--%>
    <!-- The Modal Process Request -->
    <div class="modal" id="Ex_port">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Bulk Upload</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-12">
                                <div class="form-group">
                                    <label><sup></sup>Download Excel Sheet <code></code></label>
                                    <a href="#" class='download-excel'>
                                        <label class="filelabel-download">
                                            <i class="fas fa-cloud-download-alt"></i>
                                            <span class='name'>Default Template</span>
                                        </label>
                                    </a>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-12 template-btn-move-up">
                                <div class="form-group">
                                    <label><sup>*</sup>Upload Excel Sheet<code></code></label>
                                    <label class="filelabel ">
                                        <i class="fas fa-cloud-upload-alt"></i>

                                        <span class="title">Click here to <span class="choosefile">choose file</span>.</span>
                                        <%--<div class="warning">NOT XML FORMAT</div>--%>
                                        <!--make sure to have unique id's for the input and initialize them separately each time using the js below -->
                                        <input class="file-upload-all" id="UploadedFile" name="booking_attachment" type="file" />
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="btnVerify" runat="server" CssClass="btn btn-outline-primary">Verify</asp:LinkButton>
                        <asp:LinkButton ID="btnUpload" runat="server" CssClass="btn btn-outline-primary">Upload</asp:LinkButton>
                    </div>

                </div>
            </div>
        </div>
    </div>

     <script>
         function TabShow(tabid) {
             var tabName = tabid;
             $('#Tabs a[href="#' + tabName + '"]').tab('show');
             $("#Tabs a").click(function () {
                 $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
             });
         }
    </script>

    <!-- MultiSelect Script -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });


    </script>

    <!-- Start End Date Script -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                ranges: {
                },
            },
        function (start, end) {
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
        });

            $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
        });
    </script>

    <!-- Export Pop Up Script -->
    <script>
        $("#UploadedFile").on('change', function (e) {
            var labelVal = $(".title").text();
            var oldfileName = $(this).val();
            fileName = e.target.value.split('\\').pop();

            if (oldfileName == fileName) { return false; }
            var extension = fileName.split('.').pop();


            if (extension == 'csv' || extension == 'xml' || extension == 'xlsx' || extension == 'xls') {
                $(".filelabel i").removeClass().addClass('fas fa-file-excel');
                $(".filelabel i, .filelabel .title").css({ 'color': '#208440' });
                $(".filelabel").css({ 'border': ' 2px solid #208440' });
                //$(".filelabel .warning").css({ 'display': 'none' });
            }

            else {
                $(".filelabel i").removeClass().addClass('fa fa-exclamation-triangle');
                $(".filelabel i").css({ 'color': 'red' })
                $(".filelabel .title").css({ 'color': '#1665c4' });
                $(".filelabel").css({ 'border': ' 2px solid red' });
                //$(".filelabel .warning").css({ 'display': 'inline-block' });
            }

            if (fileName) {
                if (fileName.length > 30) {
                    $(".filelabel .title").text(fileName.slice(0, 10) + '...' + extension);
                }
                else {
                    $(".filelabel .title").text(fileName);
                }
            }
            else {
                $(".filelabel .title").text(labelVal);
            }
        });
    </script>

    <%-- file upload script --%>
    <script>
        $(document).ready(function () {
            $(document).on("click", ".logoContainer", function () {
                $("#ctl00_ContentPlaceHolder1_fuCollegeLogo").click();
            });
            $(document).on("keydown", ".logoContainer", function () {
                if (event.keyCode === 13) {
                    // Cancel the default action, if needed
                    event.preventDefault();
                    // Trigger the button element with a click
                    $("#ctl00_ContentPlaceHolder1_fuCollegeLogo").click();
                }
            });
        });
    </script>

    <script type="text/javascript">
        function Focus() {
            //  alert("hii");
            document.getElementById("<%=imgUpFile.ClientID%>").focus();
        }
    </script>

    <script>
        function SetStat(val) {
            $('#switch').prop('checked', val);
        }

        var summary = "";
        $(function () {

            $('#btnSave').click(function () {
                localStorage.setItem("currentId", "#btnSave,Submit");
                debugger;
                ShowLoader('#btnSave');

                if ($('#txtName').val() == "")
                    summary += '<br>Please Enter Faculty/School Name.';
                if ($('#txtShortName').val() == "")
                    summary += '<br>Please Enter Short Name.';
                if ($('#txtcode').val() == "")
                    summary += '<br>Please Enter Code.';
                if ($('#ddlCollegeType').val() == "0")
                    summary += '<br>Please Select Type.';
                if ($('#txtCollege_Addr').val() == "")
                    summary += '<br>Please Enter Address.';

                if (summary != "") {
                    customAlert(summary);
                    summary = "";
                    return false
                }
                $('#hfdStat').val($('#switch').prop('checked'));
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSave').click(function () {
                    localStorage.setItem("currentId", "#btnSave,Submit");
                    ShowLoader('#btnSave');

                    if ($('#txtName').val() == "")
                        summary += '<br>Please Enter Faculty/School Name.';
                    if ($('#txtShortName').val() == "")
                        summary += '<br>Please Enter Short Name.';
                    if ($('#txtcode').val() == "")
                        summary += '<br>Please Enter Code.';
                    if ($('#ddlCollegeType').val() == "0")
                        summary += '<br>Please Select Type.';
                    if ($('#txtCollege_Addr').val() == "")
                        summary += '<br>Please Enter Address.';

                    if (summary != "") {
                        customAlert(summary);
                        summary = "";
                        return false
                    }
                    $('#hfdStat').val($('#switch').prop('checked'));
                });
            });
        });
    </script>

    <script>
        $("input:file").change(function () {
            //$('.fuCollegeLogo').on('change', function () {

            var maxFileSize = 1000000;
            var fi = document.getElementById('ctl00_ContentPlaceHolder1_fuCollegeLogo');
            myfile = $(this).val();
            var ext = myfile.split('.').pop();
            var res = ext.toUpperCase();

            //alert(res)
            if (res != "JPG" && res != "JPEG" && res != "PNG") {
                alert("Please Select jpg,jpeg,JPG,JPEG,PNG File Only.");
                $('.logoContainer img').attr('src', "../../IMAGES/default-fileupload.png");
                $(this).val('');
            }

            for (var i = 0; i <= fi.files.length - 1; i++) {
                var fsize = fi.files.item(i).size;
                if (fsize >= maxFileSize) {
                    alert("File Size should be less than 1 MB");
                    $('.logoContainer img').attr('src', "../../IMAGES/default-fileupload.png");
                    $("#ctl00_ContentPlaceHolder1_fuCollegeLogo").val("");

                }
            }

        });
    </script>

    <script>
        $("input:file").change(function () {
            var fileName = $(this).val();

            newText = fileName.replace(/fakepath/g, '');
            var newtext1 = newText.replace(/C:/, '');
            //newtext2 = newtext1.replace('//', ''); 
            var result = newtext1.substring(2, newtext1.length);


            if (result.length > 0) {
                $(this).parent().children('span').html(result);
            }
            else {
                $(this).parent().children('span').html("Choose file");
            }
        });
        //file input preview
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('.logoContainer img').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        $("input:file").change(function () {
            readURL(this);
        });
    </script>


    <%--<script>

        function validateAddCompany() {
            var txtCurrency = $("[id$=txtCompanyName]").attr("id");
            var txtCurrency = document.getElementById(txtCurrency);
            if (txtCurrency.value.length == 0) {

                alert('Please Enter Company Name ', 'Warning!');

                $(txtCurrency).focus();
                return false;
            }

            var txtwebsite = $("[id$=txtWebsite]").attr("id");
            var txtwebsite = document.getElementById(txtwebsite);
            if (txtwebsite.value.length == 0) {

                alert('Please Enter Website.', 'Warning!');

                $(txtwebsite).focus();
                return false;
            }

            var ddljobtype = $("[id$=ddlJobSector]").attr("id");
            var ss = document.getElementById('<%=ddlJobSector.ClientID%>').value;

            if (ss == '0') {

                alert('Please Select Job Sector.', 'Warning!');
                $(ddljobtype).focus();
                return false;
            }
        }

    </script>--%>

    <script>

        function validateCompanyLocation() {
            var ddljobtype = $("[id$=ddlCompanyName]").attr("id");

            var ss = document.getElementById('<%=ddlCompanyName.ClientID%>').value;

            if (ss == '0') {

                alert('Please Select Company Name. ', 'Warning!');

                $(ddljobtype).focus();
                return false;
            }
        }

        function clearmulti() {
            // alert('A');
            //  $("#lstbxAssociation option[value]").remove();

            $("#my-multi option:selected").prop("selected", false);
            $("#my-multi option").remove();
            $('#my-multi').multiselect('rebuild');
        }

    </script>
    <script>
        function Show() {
           // alert('show');
            $("#process_request").modal('show');
        }

        function Hide() {
           // alert('hide');
            $("#process_request").modal('hide');
           // alert('hide');
        }
    </script>
    <script>
        function alphaOnly(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
    </script>
    <script>
        function Hide() {

            $("#process_request").modal('hide');
        }
    </script>
    <script type="text/javascript">
        //function onlyAlphabets(e, t) {
        //    try {
        //        if (window.event) {
        //            var charCode = window.event.keyCode;
        //        }
        //        else if (e) {
        //            var charCode = e.which;
        //        }
        //        else { return true; }
        //        if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 32)
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch (err) {
        //        alert(err.Description);
        //    }
        //}

        function validate() {
            //alert('A');
            var url = document.getElementById("url").value;
            var pattern = /(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/;
            if (pattern.test(url)) {
                alert("Url is valid");
                return true;
            }
            alert("Url is not valid!");
            return false;

        }
        function Validatecin() {
            var isValid = false;
            var regex = /^[a-zA-Z0-9]*$/;
            isValid = regex.test(document.getElementById("<%=txtRegistrationNo.ClientID %>").value);
            document.getElementById("spnError").style.display = !isValid ? "block" : "none";
            return isValid;
        }
    </script>
</asp:Content>


