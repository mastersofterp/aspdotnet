<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="UploadDocument.aspx.cs" Inherits="ACADEMIC_UploadDocument" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        
            #sidebar {
            display: none;
        }
            .page-wrapper.toggled .page-content {
            padding-left: 15px;
        }

        .panel-info > .panel-heading b {
            padding: 8px;
            font-size: 12px;
        }

        .sidebar-menu {
            padding: 0;
            list-style: none;
        }

        .sidebar-menu .treeview {
            padding: 0px 0px;
            color: #255282;
        }

        .treeview i {
            padding-left: 10px;
        }

        .treeview span a {
            color: #255282 !important;
            font-weight: 600;
            padding-left: 3px;
        }

        .treeview span a:hover {
            color: #0d70fd !important;
        }

        .treeview.active i, .treeview.active span a {
            color: #0d70fd !important;
        }

        hr {
            margin: 12px 0px;
            border-top: 1px solid #ccc;
        }

        #ctl00_ContentPlaceHolder1_divtabs {
            box-shadow: rgb(0 0 0 / 20%) 0px 5px 10px;
            padding: 15px 5px;
            margin: 5px 0px 15px 0px;
        }

        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <style>
        #ImageButton1, #txtIssueDate {
            display: inline-block;
        }

        .modalPopup {
            background-color: #fff;
            border: 1px solid #eee;
            box-shadow: 0 3px 9px rgba(0,0,0,.5);
            padding: 10px;
        }
        table .form-control {padding: 0.375rem 0.15rem;}
    </style>

    <script type="text/javascript">
        function openModal() {
            $('#myModalpdf').modal('show');
        }
    </script>
<%--    <script type="text/javascript">
        $(function () {
            $("#TestBtn").click(function () {
                opendialog("https://accounts.digitallocker.gov.in/signin/oauth_partner/%252Foauth2%252F1%252Fauthorize%253Fresponse_type%253Dcode%2526client_id%253DE006C74F%2526state%253D2236%25252CAadhar%252BCard%2526redirect_uri%253Dhttp%25253A%25252F%25252Flocalhost%25253A63344%25252FPresentationLayer%25252FACADEMIC%25252FDocument_Submission.aspx%2526orgid%253D005095%2526txn%253D60bf57043d3a4f338a4834b9oauth21623152388%2526hashkey%253D2b5662df0b0c07290a582c48de78ee6b661d57d58e86aa2633d1411544db9c39%2526requst_pdf%253DY%2526signup%253Dsignup");
            });

            function opendialog(page) {
                debugger;
                var $dialog = $('#testDiv')
                    .html('<iframe style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
                    .dialog({
                        title: "Page",
                        autoOpen: false,
                        dialogClass: 'dialog_fixed,ui-widget-header',
                        modal: true,
                        height: 500,
                        minWidth: 400,
                        minHeight: 400,
                        draggable: true
                    });
                $dialog.dialog('open');
            }
        });

    </script>--%>
    <%--   <asp:UpdatePanel ID="updUploadDocument" runat="server">
        <ContentTemplate>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">STUDENT INFORMATION</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-lg-2 col-md-4 col-12" id="divtabs" runat="server">
                                <aside class="sidebar">

                                    <!-- sidebar: style can be found in sidebar.less -->
                                    <section class="sidebar" style="background-color: #ffffff">
                                        <ul class="sidebar-menu">
                                            <!-- Optionally, you can add icons to the links -->
                                            <li class="treeview" id="divhome" runat="server">
                                                <i class="fa fa-search"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkGoHome"
                                                        ToolTip="Please Click Here To Go To Home" OnClick="lnkGoHome_Click" Text="Search New Student"> 
                                                    </asp:LinkButton>
                                                </span>
                                                <hr />
                                            </li>
                                            <li class="treeview">
                                                <i class="fa fa-user"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkPersonalDetail"
                                                        ToolTip="Please select Personal Details" OnClick="lnkPersonalDetail_Click" Text="Personal Details"> 
                                                    </asp:LinkButton>
                                                </span>
                                                <hr />
                                            </li>

                                            <li class="treeview ">
                                                <i class="fa fa-map-marker"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkAddressDetail"
                                                        ToolTip="Please select Address Details" OnClick="lnkAddressDetail_Click" Text="Address Details"> 
                                                    </asp:LinkButton>
                                                </span>
                                                <hr />
                                            </li>

                                            <li class="treeview" id="divadmissiondetails" runat="server">
                                                <i class="fa fa-university"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkAdmissionDetail"
                                                        ToolTip="Please select Admission Details" OnClick="lnkAdmissionDetail_Click" Text="Admission Details"> 
                                                    </asp:LinkButton>
                                                </span>
                                                <hr />
                                            </li>

                                            <li class="treeview" style="display: none">
                                                <i class="fa fa-info-circle"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkDasaStudentInfo"
                                                        ToolTip="Please select DASA Student Information" Text="Information"> 
                                                    </asp:LinkButton>
                                                </span>
                                                <hr />
                                            </li>

                                            <li class="treeview active">
                                                <i class="fa fa-file"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkUploadDocument"
                                                        ToolTip="Please Upload Documents" OnClick="lnkUploadDocument_Click" Text="Document Upload"> 
                                                    </asp:LinkButton>
                                                </span>
                                                <hr />
                                            </li>
                                            <li class="treeview">
                                                <i class="fa fa-graduation-cap"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkQualificationDetail"
                                                        ToolTip="Please select Qualification Details" OnClick="lnkQualificationDetail_Click" Text="Qualification Details"> 
                                                    </asp:LinkButton>
                                                </span>
                                                <hr />
                                            </li>

                                            <li class="treeview">
                                                <i class="fa fa-stethoscope"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkCovid" Visible="true"
                                                        ToolTip="Covid Vaccination Details" OnClick="lnkCovid_Click" Text="Covid Information"> 
                                                    </asp:LinkButton>
                                                </span>
                                                <hr />
                                            </li>

                                            <li class="treeview">
                                                <i class="fa fa-link"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkotherinfo"
                                                        ToolTip="Please select Other Information." OnClick="lnkotherinfo_Click" Text="Other Information"> 
                                                    </asp:LinkButton>
                                                </span>
                                                <hr />
                                            </li>

                                            <li class="treeview" id="divAdmissionApproval" runat="server">
                                                <i class="fa fa-check-circle"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkApproveAdm"
                                                        ToolTip="Verify Information" OnClick="lnkApproveAdm_Click" Text="Verify Information"> 
                                                    </asp:LinkButton>
                                                </span>
                                                <hr />
                                            </li>

                                            <li class="treeview" id="divPrintReport" runat="server" visible="false">
                                                <i class="fas fa-print"></i>
                                                <span>
                                                    <asp:LinkButton runat="server" ID="lnkprintapp" OnClick="lnkprintapp_Click" Text="Print"></asp:LinkButton>
                                                </span>
                                            </li>
                                        </ul>
                                    </section>
                                </aside>
                            </div>

                            <div class="col-lg-10 col-md-8 col-12">
                                <div class="col-12 pl-0 pr-0 pl-lg-2 pr-lg-2">
                                    <div class="row" id="divDocumentUpload" style="display: block;">
                                        <asp:Panel ID="pnlBind" runat="server">
                                            <asp:Label ID="lblmessageShow" runat="server"></asp:Label>
                                            <asp:ListView ID="lvBinddata" runat="server" OnItemDataBound="lvBinddata_ItemDataBound1">
                                                <LayoutTemplate>
                                                    <div class="col-md-12">
                                                        <div class="sub-heading">
                                                            <h5>Upload Document </h5>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-5 col-md-6 col-12" runat="server" id="divNote">
                                                        <div class="note-div" style="color: red;">
                                                            <h5 class="heading">Note</h5>
                                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Only .pdf, .jpeg and .jpg required, up to 500 KB only.</span></p>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12">
                                                        <div style="width: 100%; height: 370px; overflow: auto">
                                                            <table class="table table-striped table-bordered nowrap">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Sr.No</th>
                                                                        <%-- <th>Document No.</th>--%>

                                                                        <th>Document Name</th>
                                                                        <th>Mandatory</th>                   <%--  //Added by sachin on 27-07-2022--%>
                                                                        <th>Upload Document</th>
                                                                        <th>Upload</th>
                                                                        <th>Status</th>
                                                                        <th>Preview</th>
                                                                        <th style="display:none">Certificate No.</th>
                                                                        <th style="display:none">Issue Date</th>
                                                                        <th style="display:none">Authority</th>
                                                                        <th style="display:none">District</th>    
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Container.DataItemIndex +1 %></td>
                                                        <%-- <td style="width:5%"><%# Eval("DOCUMENTNO") %></td>--%>

                                                        <td>
                                                            <asp:Label ID="lblStar" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblDocument" Width="150px" ToolTip='<%# Eval("DOCUMENTNO") %>' runat="server" Text='<%# Eval("DOCUMENTNAME") %>'></asp:Label>
                                                            <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("DOCUMENTNO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
<%--                                                            <asp:Label ID="lblMandatory" Width="150px" ToolTip='<%# Eval("CATEGORYNO") %>' runat="server" Text='<%# Eval("MANDATORY") %>'></asp:Label>--%>
                                                       <asp:TextBox ID="txtMandatory" runat="server" CssClass="form-control" Width="100px" Text='<%# Eval("MANDATORY") %>' Enabled="false" ></asp:TextBox></td>     <%--  //Added by sachin on 27-07-2022--%>
                                                         
                                                        </td>
                                                        <td>
                                                            <asp:FileUpload ID="fuFile" runat="server" ToolTip='<%# Eval("DOCUMENTNO") %>' onchange="return CheckFileSize(this)"/>
                                                            <asp:HiddenField ID="hdnFile" runat="server" />
                                                        </td>

                                                         <td>
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Upload" ValidationGroup="Submit" OnClick="btnSubmit_Click1"
                                                                CssClass="btn btn-info" CommandArgument='<%# Eval("DOCUMENTNO") %>' ToolTip='<%# Eval("DOCUMENTNO") %>' /></td>

                                                        
                                                        <td>
                                                            <%--<asp:Label ID="status" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>--%>
                                                            <%--                                                    <asp:LinkButton runat="server" ID ="status"  Target="_blank" Text='<%# Eval("STATUS") %>'  NavigateUrl='<%# Eval("NEWDOC_PATH")%>'><%#  Eval("DOCUMENT_NAME")%>'</asp:LinkButton>--%>
                                                            <%-- <asp:Label ID="status" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>  --%>
                                                            <asp:LinkButton ID="status" runat="server" Text='<%# Bind("STATUS") %>'
                                                                CommandArgument='<%#Eval("DOCUMENTNO") %>'
                                                                ToolTip='<%# Eval("DOCUMENT_NAME")%>' OnClick="btnDownloadFile_Click"></asp:LinkButton>
                                                            <%-- <asp:HiddenField runat="server" ID="hdfnDocName" Value='<%# Eval("DOCUMENTNAME") %>' />--%>
                                                   
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:UpdatePanel ID="updPreview" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click"
                                                                        Text="Preview" ImageUrl="~/Images/search-svg.png" ToolTip='<%# Eval("DOCUMENT_NAME") %>' data-toggle="modal" data-target="#preview"
                                                                        CommandArgument='<%# Eval("DOCUMENT_NAME") %>' Visible='<%# Convert.ToString(Eval("DOCUMENT_NAME")) == string.Empty?false:true %>'></asp:ImageButton>
                                                                    <%--   <asp:ImageButton ID="imgbtnPreviewdoc" runat="server" OnClick="imgbtnPrevDoc_Click1" data-toggle="modal" data-target="#PassModel"
                                                                        Text="Preview" ImageUrl="~/Images/search-svg.png" Height="32px" Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>' 
                                                                        Width="30px" CommandArgument='<%# Eval("DOCUMENT_NAME") %>' Visible='<%# Convert.ToString(Eval("DOCUMENT_NAME"))==string.Empty?false:true %>'></asp:ImageButton>--%>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="imgbtnPreview" EventName="Click" />
                                                                  
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </td>


                                                        <td style="display:none">
                                                            <asp:TextBox ID="txtDocNo" runat="server" CssClass="form-control" Width="100px"   Text='<%# Eval("CERTIFICATE_NO") %>'></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtDocNo"
                                                            ValidChars="1234567890" FilterMode="ValidChars" />
                                                        </td>
                                                            
                                                        <td style="display:none">

                                                            <asp:ImageButton runat="Server" ID="ImageButton1" ImageUrl="~/Images/calender11.jpg" Visible="false" Width="20px" Height="20px" AlternateText="" />
                                                            <asp:TextBox ID="txtIssueDate" runat="server" CssClass="form-control" Width="100px" placeholder="DD/MM/YYYY" Text='<%# Eval("ISSUE_DATE") %>'></asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="ceDOB" runat="server" Enabled="True" TargetControlID="txtIssueDate" PopupButtonID="ImageButton1" Format="dd-MM-yyyy"></ajaxToolKit:CalendarExtender>

                                                        </td>
                                                        <td style="display:none">
                                                            <asp:DropDownList ID="ddlAuthority" runat="server" CssClass="form-control" Width="100px" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList></td>

                                                        <td style="display:none">
                                                            <asp:TextBox ID="txtDistrict" runat="server" CssClass="form-control"  Width="100px" Text='<%# Eval("DISTRICT") %>' onkeydown="return((event.keyCode >= 64 && event.keyCode <= 91) || (event.keyCode==32)|| (event.keyCode==8)|| (event.keyCode==9));" ></asp:TextBox></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                        <div class="modal fade" id="preview" role="dialog" style="display: none; margin-left: -100px;">
                                            <div class="modal-dialog text-center">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <!-- Modal content-->
                                                        <div class="modal-content" style="width: 700px;">
                                                            <div class="modal-header">
                                                                <%--   <button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                                                                <h4 class="modal-title">Document</h4>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div class="col-md-12">

                                                                    <asp:Literal ID="ltEmbed" runat="server" />

                                                                    <%--<iframe runat="server" style="width: 100; height: 100px" id="iframe2"></iframe>--%>

                                                                    <%--<asp:Image ID="imgpreview" runat="server" Height="530px" Width="600px"  />--%>
                                                                </div>
                                                            </div>
                                                            <div class="modal-footer">
                                                                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                                            </div>
                                                        </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 from-group table-responsive" style="display: none;">
                                            <asp:ListView ID="lvDocumentList" runat="server" OnItemDataBound="lvDocumentList_ItemDataBound">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <h4>Document List</h4>
                                                        <div class="box-tools pull-right">
                                                            <div class="pull-right">

                                                                <div style="color: green; font-weight: bold">
                                                                    <asp:UpdatePanel ID="updFetch" runat="server">
                                                                        <ContentTemplate>
                                                                            <span>Get Documents From DigiLocker</span><asp:ImageButton runat="server" ID="btnFetch" CommandName='<%# Eval("DOCUMENTNAME") %>'
                                                                                ImageUrl="~/IMAGES/digiLocker2.jpg" Width="115px" Height="60px" Style="margin-left: 70%; margin-top: -45px" OnClick="btnFetch_Click" />

                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="btnFetch" EventName="Click" />
                                                                            <%--  <asp:PostBackTrigger ControlID="btnFetch"/>--%>
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <table class="table table-hover table-bordered table-responsive" style="width: 100%">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th style="width: 5%">Sr.No</th>
                                                                    <th style="width: 20%">Document</th>
                                                                    <th style="width: 25%">Upload Scan Document</th>
                                                                    <th style="width: 30%">Upload</th>
                                                                    <th style="width: 10%">Status</th>
                                                                    <th style="width: 10%">Preview</th>
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
                                                        <td style="width: 5%"><%# Container.DataItemIndex +1 %></td>
                                                        <td style="width: 20%">
                                                            <asp:Label ID="lblStar" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblDocument" ToolTip='<%# Eval("DOCUMENTNO") %>' runat="server" Text='<%# Eval("DOCUMENTNAME") %>'></asp:Label>
                                                            <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("DOCUMENTNO") %>' />
                                                        </td>
                                                        <td style="width: 25%">
                                                            <asp:FileUpload ID="fuFile" runat="server" ToolTip="DOCUMENTNO" onchange="CheckFileSize(this)"/>
                                                            <asp:HiddenField ID="hdnFile" runat="server" />
                                                            <br />
                                                        </td>
                                                        <td style="width: 30%">
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Upload" ValidationGroup="Submit" OnClick="btnSubmit_Click"
                                                                CssClass="btn btn-success" CommandArgument='<%# Eval("DOCUMENTNAME") %>' ToolTip='<%# Eval("DOCUMENTNAME") %>' />
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:Label ID="lblStatus" Font-Bold="true" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:UpdatePanel ID="updPreview" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:ImageButton ID="imgbtnPrevDoc" runat="server" OnClick="imgbtnPrevDoc_Click" data-toggle="modal" data-target="#PassModel"
                                                                        Text="Preview" ImageUrl="~/IMAGES/icons8-search-480 (1).png" Height="32px"
                                                                        Width="30px" CommandArgument='<%# Eval("DOCUMENT_NAME") %>' Visible='<%# Convert.ToString(Eval("DOCUMENT_NAME"))==string.Empty?false:true %>'></asp:ImageButton>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="imgbtnPrevDoc" EventName="Click" />
                                                                    <%--  <asp:PostBackTrigger ControlID="btnFetch"/>data-toggle="modal" data-target="#PassModel"--%>
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                        <div class="col-sm-12 from-group table-responsive" style="display: none;">
                                            <asp:ListView ID="lvDocumentsAdmin" runat="server" OnItemDataBound="lvDocumentList1_ItemDataBound">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <h4>Document List</h4>
                                                        <table class="table table-hover table-bordered table-responsive">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th style="width: 5%">Sr.No</th>
                                                                    <th>Document</th>
                                                                    <th>Status</th>
                                                                    <th style="width: 10%">View</th>
                                                                    <th>Download</th>
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
                                                        <td style="width: 5%"><%# Container.DataItemIndex +1 %></td>
                                                        <td style="width: 20%">
                                                            <asp:Label ID="lblDocument" ToolTip='<%# Eval("DOCUMENTNO") %>' runat="server" Text='<%# Eval("DOCUMENTNAME") %>'></asp:Label>
                                                            <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("DOCUMENTNO") %>' />
                                                        </td>
                                                        <td style="width: 15%">
                                                            <asp:Label ID="lblStatus" Font-Bold="true" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="padding-left: 26px; padding-top: 12px">
                                                            <asp:UpdatePanel ID="updPreview" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:ImageButton ID="imgbtnPrevDoc1" runat="server" OnClick="imgbtnPrevDoc1_Click" data-toggle="modal" data-target="#PassModel"
                                                                        Text="Preview" ImageUrl="~/IMAGES/icons8-search-480 (1).png" Height="32px"
                                                                        Width="30px" CommandArgument='<%# Eval("DOCUMENT_NAME") %>' Visible='<%# Convert.ToString(Eval("DOCUMENT_NAME"))==string.Empty?false:true %>'></asp:ImageButton>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="imgbtnPrevDoc1" EventName="Click" />
                                                                    <%--  <asp:PostBackTrigger ControlID="btnFetch"/>data-toggle="modal" data-target="#PassModel"--%>
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                        <td style="width: 20%">
                                                            <asp:LinkButton ID="lnkDownloadDoc" runat="server" OnClick="lnkDownloadDoc_Click" Visible='<%# Convert.ToString(Eval("DOCUMENT_NAME"))==string.Empty?false:true %>'
                                                                Text="Click to Download" ToolTip="Click here to download document"
                                                                CommandArgument='<%# Eval("DOCUMENTNO") %>'> 
                                                            </asp:LinkButton>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer mt-4">
                                        <asp:Button ID="btnSave" runat="server" TabIndex="29" Text="Next & Continue >>" ToolTip="Click to Report"
                                            CssClass="btn btn-primary" OnClick="btnSave_Click" ValidationGroup="Academic" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="Academic" />
                                        <button runat="server" id="btnGohome" visible="false" tabindex="30" onserverclick="btnGohome_Click" class="btn btn-warning btnGohome" tooltip="Click to Go Back Home">
                                            Go Back Home
                                        </button>
                                    </div>
                                    <%--DigiLocker--%>
                                    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
                                    <ajaxToolKit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="pnlModal" TargetControlID="lnkDummy"
                                        CancelControlID="btnCloseModal" BackgroundCssClass="modalBackground">
                                    </ajaxToolKit:ModalPopupExtender>

                                    <asp:Panel ID="pnlModal" runat="server" CssClass="modalPopup" align="center" Visible="false" Style="display: none;">
                                        <div class="header">
                                            Documents fetched from Digital Locker Account
                                        </div>
                                        <div class="body">
                                            <div class="nav-tabs-custom">
                                                <ul class="nav nav-tabs">
                                                    <li class="active"><a href="#tab_1" data-toggle="tab">Issued Documents</a></li>
                                                    <li><a href="#tab_2" data-toggle="tab">Uploaded Documents</a></li>
                                                    <li class="pull-right"><a href="#" class="text-muted"><i class="far fa-window-close"></i></a></li>
                                                </ul>
                                            </div>

                                            <div class="tab-content">
                                                <div id="tab_1" class="container tab-pane active" style="width: 700px">
                                                    <asp:ListView ID="lvIssuedDoc" runat="server" Style="width: 700px">
                                                        <LayoutTemplate>
                                                            <div id="demo-grid">
                                                                <h4>Issued Document List</h4>
                                                                <table class="table table-hover table-bordered table-responsive" style="width: 100%">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th style="width: 5%">Sr.No</th>
                                                                            <th style="width: 20%">Name</th>
                                                                            <th style="width: 20%">Download</th>
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
                                                                <td style="width: 5%"><%# Container.DataItemIndex +1 %></td>
                                                                <td style="width: 20%">
                                                                    <%# Eval("Name") %>
                                                                </td>
                                                                <td style="width: 10%">
                                                                    <asp:UpdatePanel ID="updDownlodSelf" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:Button ID="btnDownload" Text="Download" OnClick="btnDownload_Click"
                                                                                runat="server" CommandName='<%# Eval("URI") %>' CommandArgument='<%# Eval("token") %>' />

                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <%--  <asp:AsyncPostBackTrigger ControlID="btnDownload" EventName="Click" />--%>

                                                                            <asp:PostBackTrigger ControlID="btnDownload" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                                <%--OnClick="btnDownload_Click"   OnClientClick="return ajaxCall(this.id)"  --%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                                <div id="tab_2" class="container tab-pane fade" style="width: 700px">
                                                    <asp:ListView ID="lvUploadedDoc" runat="server" Style="width: 700px">
                                                        <LayoutTemplate>
                                                            <div id="demo-grid">
                                                                <h4>Uploaded Document List</h4>
                                                                <table class="table table-hover table-bordered table-responsive" style="width: 100%">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th style="width: 5%">Sr.No</th>
                                                                            <th style="width: 20%">Name</th>
                                                                            <th style="width: 20%">Download</th>
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
                                                                <td style="width: 5%"><%# Container.DataItemIndex +1 %></td>
                                                                <td style="width: 20%">
                                                                    <%# Eval("Name") %>
                                                                </td>
                                                                <td style="width: 10%">


                                                                    <asp:Button ID="btnDownloadSelf" Text="Download" OnClick="btnDownload_Click"
                                                                        runat="server" CommandName='<%# Eval("URI") %>' CommandArgument='<%# Eval("token") %>' />

                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>

                                                </div>
                                            </div>

                                            <br />
                                            <asp:Button ID="btnCloseModal" runat="server" Text="Close" />

                                        </div>
                                    </asp:Panel>

                                    <%--  Digilocker--%>

                                    <div class="modal fade" id="PassModel" role="dialog" style="display: none;">
                                        <div class="modal-dialog text-center">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <!-- Modal content-->
                                                    <div class="modal-content" style="width: 900px;">
                                                        <div class="modal-header">
                                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                            <h4 class="modal-title">Document</h4>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="col-md-12">
                                                                <iframe runat="server" style="width: 800px; height: 1100px" id="iframeView"></iframe>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                                        </div>
                                                    </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>

    <script type="text/javascript">
        function CheckFileSize(chk) {

            var maxFileSize = 500000;
            var fi = document.getElementById(chk.id);
            for (var i = 0; i <= fi.files.length - 1; i++) {

                var fsize = fi.files.item(i).size;

                if (fsize >= maxFileSize) {
                    alert("File size should not be greater than 500kb");
                    $(chk).val("");
                    return false;

                }
            }
            var fileExtension = ['pdf','jpg','jpeg'];
            if ($.inArray($(chk).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert("Only formats are allowed : " + fileExtension.join(', '));
                $(chk).val("");
            }
        }

    </script>

<%--    <script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#lvDocumentList').DataTable({
                
            });
        }
    </script>
    <script type="text/javascript">
        function callPostBack(lst) {
            alert("callpostback")
            ////var rows = document.getElementsByTagName("table")[0].rows;
            //var row = lst.parentNode.parentNode.parentNode;
            //alert(row.getElementsByTagName("table")[0]);
            //var customerId = row.cells[1].getElementById("lblDocument").value;
            //alert(customerId)

            //var city = row.cells[1].getElementsByTagName("input")[0].value;
            //row.style.backgroundColor = "#FF0000";
            //alert("RowIndex: " + rowIndex + " CustomerId: " + customerId + " City:" + city);

            //var oldURL = window.location.protocol + "//" + window.location.host + window.location.pathname;
            //var newUrl = oldURL + "?Id=" + id;
            //if (window.history != 'undefined' && window.history.pushState != 'undefined') {
            //    window.history.pushState({ path: newUrl }, '', newUrl);
            //}



            //var listview = document.getElementById('lvDocumentList');

            //alert(listview)

            //var listview = document.getElementById('<%=lvDocumentList.ClientID %>'); 

            PageMethods.FetchClick("Aadhar Card", onSucess, onError)

            function onSucess(result) {
                alert(result);
            }

            function onError(result) {
                alert('Something wrong.');
            }

            if (1 == 2)
                return true;
            else
                return false;
        }
        function loadEmployees() {
            var xhttp = new XMLHttpRequest();
            var accToken = '';
            var authorizeData = {
                response_type: 'password',
                client_id: 'E006C74F',
                redirect_uri: 'http://localhost:63344/PresentationLayer/ACADEMIC/Document_Submission.aspx?pageno=2280',
                state: 'state',
                //username: self.loginEmail(),
                //password: self.loginPassword()
            };
            // Set GET method and ajax file path with parameter
            xhttp.open("GET", "https://api.digitallocker.gov.in/public/oauth2/1/file/in.gov.transport-DRVLC-MH4020140004569", true);

            // Content-type
            xhttp.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
            xhttp.setRequestHeader('Authorization', 'Bearer ' + accToken);
            // call on request changes state
            xhttp.onreadystatechange = function () {
                if (this.readyState == 4 && this.status == 200) {

                    // Parse this.responseText to JSON object
                    var response = JSON.parse(this.responseText);

                    // Select <table id='empTable'> <tbody>
                    var empTable =
             document.getElementById("empTable").getElementsByTagName("tbody")[0];

                    // Empty the table <tbody>
                    empTable.innerHTML = "";

                    // Loop on response object
                    for (var key in response) {
                        if (response.hasOwnProperty(key)) {
                            var val = response[key];

                            // insert new row
                            var NewRow = empTable.insertRow(0);
                            var name_cell = NewRow.insertCell(0);
                            var username_cell = NewRow.insertCell(1);
                            var email_cell = NewRow.insertCell(2);

                            name_cell.innerHTML = val['emp_name'];
                            username_cell.innerHTML = val['salary'];
                            email_cell.innerHTML = val['email'];

                        }
                    }

                }
            };

            // Send request
            xhttp.send();
        }
        function ajaxCall(lnk) {
            debugger;
            alert(lnk);

            //alert(Cmdname)
            //var authorizeData = {
            var response_type = 'code';
            var client_id = 'E006C74F';
            var redirect_uri = 'http://localhost:63344/PresentationLayer/ACADEMIC/Document_Submission.aspx?pageno=2280';
            var state = 'state';
            //username: self.loginEmail(),
            //password: self.loginPassword()
            //};

            var authorizeData = new Object();
            authorizeData.response_type = 'code';
            authorizeData.client_id = 'E006C74F';
            authorizeData.redirect_uri = 'http://localhost:63344/PresentationLayer/ACADEMIC/Document_Submission.aspx?pageno=2280';
            authorizeData.state = 'state';
            //$.post('https://api.digitallocker.gov.in/public/oauth2/1/authorize', authorizeData, function (data) {
            //    console.log(data);
            //});

            $.ajax({
                url: 'https://api.digitallocker.gov.in/public/oauth2/1/file/in.gov.transport-DRVLC-MH4020140004569',
                //url: 'https://api.digitallocker.gov.in/public/oauth2/1/authorize?response_type=' + response_type +
                //    '&client_id=' + client_id + '&redirect_uri=' + redirect_uri + '&state=' + state,
                type: "GET",
                dataType: "jsonp",
                contentType: "text/html; charset=UTF-8",
                processData: false, // Not to process data  
                crossDomain: true,
                //data: authorizeData,
                success: function (result) {
                    alert("success");
                },
                error: function (errResponse) {
                    alert("error");
                    console.log(errResponse);
                }
            });
        }
    </script>
   --%>
</asp:Content>

