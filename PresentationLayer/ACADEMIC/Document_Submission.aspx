<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Document_Submission.aspx.cs" Inherits="ACADEMIC_Document_Submission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="divMsg" runat="server">
    </div>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <%--<h3 class="box-title">Document Submission</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>
           
            <div class="box-body">
                <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Search By</label>
                                </div>
                                <asp:RadioButton ID="rdoEnrollmentNo" runat="server" Text="Enrollment No." GroupName="search" Checked="true" />
                                <asp:RadioButton ID="rdoRollNo" runat="server" Text="Roll No." GroupName="search" />
                                <asp:RadioButton ID="rdoIdno" runat="server" Text="Id No." Checked="true" GroupName="search" Visible="false" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label></label>
                                </div>
                                <asp:TextBox ID="txtTempIdno" runat="server" ValidationGroup="search" class="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-lg-3 col-md-12 col-12 text-center">
                                <div class="label-dynamic">
                                    <label></label>
                                </div>
                                <asp:Button ID="btnShow" runat="server" Text="Search" ValidationGroup="search" OnClick="btnShow_Click" CssClass="btn btn-primary" />
                                <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtTempIdno" Display="None"
                                    ErrorMessage="Please Enter Enrollment No. or Roll No." SetFocusOnError="True" ValidationGroup="search" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                    ValidationGroup="search" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divtxtidno" visible="false">
                    <div class="label-dynamic">
                        <label>ID No.</label>
                    </div>
                    <asp:TextBox ID="txtIDNo" runat="server" CssClass="form-control" Enabled="False" Visible="false" />
                </div>

                <div id="divDetails" class="col-12" runat="server" visible="false">
                    <div class="sub-heading">
                        <h5>Student Details</h5>
                    </div>
                    <div class="col-12">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-12 pl-md-0">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>Student Name :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label>
                                            <asp:Label ID="lblYear" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                        </a>
                                    </li>
                                    <li class="list-group-item"><b>
                                        <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                        :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblRegno" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item"><b>
                                        <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                        :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblAdmbatch" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>
                                </ul>
                            </div>
                            <div class="col-lg-6 col-md-6 col-12 pl-md-0">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>
                                        <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                        :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label>
                                        </a>
                                    </li>
                                    <li class="list-group-item"><b>
                                        <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item"><b>
                                        <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                        :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <asp:Label ID="lblMsg" runat="server" SkinID="lblmsg"></asp:Label>
                    </div>
                </div>
                <div class="col-12 mt-3">
                    <div class="sub-heading">
                        <h5><b>Photo & Signature Details</b></h5>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-12" style="color: Red; font-weight: bold">
                            <span style="color: black">Note :</span>  Only JPG,JPEG,PNG files are allowed upto 150 KB size For Photo and Signature, (Only Passport Size Photo Allowed).
                        </div>

                        <div class="form-group col-lg-6 col-md-6 col-12">
                            <div class="label-dynamic">

                                <label>Photo</label>
                            </div>
                            <asp:Image ID="imgPhoto" runat="server" Width="100px" Height="100px" /><br />
                            <asp:FileUpload ID="fuPhotoUpload" runat="server" TabIndex="37" onchange="previewFilePhoto()" />
                            <asp:Button ID="btnPhotoUpload" runat="server" CssClass="btn btn-primary" Text="Upload" TabIndex="37" OnClick="btnPhotoUpload_Click" />

                            <%--  <asp:RequiredFieldValidator ID="rfvfuPhotoUpload" runat="server" ControlToValidate="fuPhotoUpload"
                                                        Display="None" ErrorMessage="Please Upload Photo" SetFocusOnError="True"
                                                        ValidationGroup="Academic" ></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="form-group col-lg-6 col-md-6 col-12">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="label-dynamic">
                                        <label>Signature</label>
                                    </div>
                                    <asp:Image ID="ImgSign" runat="server" Width="150px" Height="60px" /><br />
                                    <asp:FileUpload ID="fuSignUpload" runat="server" onChange="previewFilePhoto2()" TabIndex="38" />
                                </div>

                                <div class="col-lg-6 mt-5 pt-3">
                                    <asp:Button ID="btnSignUpload" CssClass="btn btn-primary" runat="server" Text="Upload" TabIndex="38" OnClick="btnSignUpload_Click" />
                                </div>
                            </div>
                        </div>
                    </div>


                </div>

                <div class="col-12" id="divDocumentUpload" runat="server" visible="false">
                    <asp:Panel ID="pnlBind" runat="server">
                        <asp:Label ID="lblmessageShow" runat="server"></asp:Label>
                        <div class="sub-heading">
                            <h5>Upload Document </h5>
                        </div>
                        <asp:ListView ID="lvBinddata" runat="server" OnItemDataBound="lvBinddata_ItemDataBound1">
                            <LayoutTemplate>
                                <table class="table table-striped table-bordered nowrap">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Sr.NO</th>
                                            <%-- <th>Document No.</th>--%>

                                            <th>Document Name</th>
                                            <th>Mandatory</th>
                                            <%--  //Added by sachin on 27-07-2022--%>
                                            <th>Upload Document</th>
                                            <th>Upload</th>
                                            <th>Status</th>
                                            <th>Preview</th>
                                            <%--<th>Certificate No.</th>
                                            <th>Issue Date</th>
                                            <th>Authority</th>
                                            <th>District</th>--%>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.DataItemIndex +1 %></td>

                                    <td>
                                        <asp:Label ID="lblStar" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                                        <asp:Label ID="lblDocument" Width="150px" ToolTip='<%# Eval("DOCUMENTNO") %>' runat="server" Text='<%# Eval("DOCUMENTNAME") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("DOCUMENTNO") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtMandatory" runat="server" CssClass="form-control" Width="100px" Text='<%# Eval("MANDATORY") %>' Enabled="false"></asp:TextBox></td>
                                    <td>
                                        <asp:FileUpload ID="fuFile" runat="server" ToolTip='<%# Eval("DOCUMENTNO") %>' onchange="return CheckFileSize(this)" />
                                        <asp:HiddenField ID="hdnFile" runat="server" />
                                    </td>

                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Upload" ValidationGroup="Submit" OnClick="btnSubmit_Click1"
                                            CssClass="btn btn-info" CommandArgument='<%# Eval("DOCUMENTNO") %>' ToolTip='<%# Eval("DOCUMENTNO") %>' /></td>
                                    <td>

                                        <asp:LinkButton ID="status" runat="server" Text='<%# Bind("STATUS") %>'
                                            CommandArgument='<%#Eval("DOCUMENTNO") %>'
                                            ToolTip='<%# Eval("DOCUMENT_NAME")%>' OnClick="btnDownloadFile_Click"></asp:LinkButton>

                                         
                                    </td>
                                    <td style="text-align: center">
                                        <asp:UpdatePanel ID="updPreview" runat="server">
                                            <ContentTemplate>
                                                <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click"
                                                    Text="Preview" ImageUrl="~/Images/search-svg.png" ToolTip='<%# Eval("DOCUMENT_NAME") %>' data-toggle="modal" data-target="#preview"
                                                    CommandArgument='<%# Eval("DOCUMENT_NAME") %>' Visible='<%# Convert.ToString(Eval("DOCUMENT_NAME"))==string.Empty?false:true %>'></asp:ImageButton>

                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="imgbtnPreview" EventName="Click" />

                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="d-none">
                                        <asp:TextBox ID="txtDocNo" runat="server" CssClass="form-control" Width="100px" Visible="false" Text='<%# Eval("CERTIFICATE_NO") %>'></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtDocNo"
                                            ValidChars="1234567890" FilterMode="ValidChars" />
                                    </td>
                                    <td class="d-none">

                                        <asp:ImageButton runat="Server" ID="ImageButton1" ImageUrl="~/Images/calender11.jpg" Visible="false" Width="20px" Height="20px" AlternateText="" />
                                        <asp:TextBox ID="txtIssueDate" runat="server" Visible="false" CssClass="form-control" Width="100px" placeholder="DD/MM/YYYY" Text='<%# Eval("ISSUE_DATE") %>'></asp:TextBox>
                                        <ajaxToolKit:CalendarExtender ID="ceDOB" runat="server" Enabled="True" TargetControlID="txtIssueDate" PopupButtonID="ImageButton1" Format="dd-MM-yyyy"></ajaxToolKit:CalendarExtender>

                                    </td>
                                    <td class="d-none">
                                        <asp:DropDownList ID="ddlAuthority" runat="server" CssClass="form-control" Width="100px" AppendDataBoundItems="true" Visible="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList></td>        
                                    <td class="d-none">
                                        <asp:TextBox ID="txtDistrict" runat="server" Visible="false" CssClass="form-control" Width="100px" Text='<%# Eval("DISTRICT") %>' onkeydown="return((event.keyCode >= 64 && event.keyCode <= 91) || (event.keyCode==32)|| (event.keyCode==8)|| (event.keyCode==9));"></asp:TextBox></td>


                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </asp:Panel>
                </div>
                <div class="col-12 btn-footer mt-3">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" Visible="false" />

                    <%--   <asp:Button ID="btnSubmit" runat="server" TabIndex="39" Text="Submit" ToolTip="Click to Submit"
                                                    CssClass="btn btn-primary" ValidationGroup="Academic" />--%>
                    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>

                    <ajaxToolKit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="pnlModal" TargetControlID="lnkDummy"
                        CancelControlID="btnCloseModal" BackgroundCssClass="modalBackground">
                    </ajaxToolKit:ModalPopupExtender>
                </div>
            </div>
        </div>
         </div>
    </div>
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

    <script type="text/javascript">
        function callPostBack(lst) {
            alert("callpostback")
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
            window.open(lnk, '_blank');
        }

    </script>
    <script>


        function previewFilePhoto() {
            debugger;
            var preview = document.querySelector('#<%=imgPhoto.ClientID %>');
            var file = document.querySelector('#<%=fuPhotoUpload.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }

        function previewFilePhoto2() {
            var preview = document.querySelector('#<%=ImgSign.ClientID %>');
            var file = document.querySelector('#<%=fuSignUpload.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
    </script>
    <script type="text/javascript">
        function pageLoad() {

            function previewFilePhoto() {
                var preview = document.querySelector('#<%=imgPhoto.ClientID %>');
                var file = document.querySelector('#<%=fuPhotoUpload.ClientID %>').files[0];
                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                }

                if (file) {
                    reader.readAsDataURL(file);
                } else {
                    preview.src = "";
                }
            }

            function previewFilePhoto2() {
                var preview = document.querySelector('#<%=ImgSign.ClientID %>');
                var file = document.querySelector('#<%=fuSignUpload.ClientID %>').files[0];
                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                }

                if (file) {
                    reader.readAsDataURL(file);
                } else {
                    preview.src = "";
                }
            }


        }

    </script>

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
            var fileExtension = ['pdf', 'jpg', 'jpeg'];
            if ($.inArray($(chk).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert("Only formats are allowed : " + fileExtension.join(', '));
                $(chk).val("");
            }
        }

    </script>
</asp:Content>


