<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Condonation_Apply.aspx.cs" Inherits="ACADEMIC_Condonation_Apply" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdApplyCondolance"
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
    <div id="divCourses" runat="server" visible="false">
        <asp:UpdatePanel ID="UpdApplyCondolance" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div id="div1" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            </div>
                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Student Information</h5>
                                            </div>
                                        </div>
<%--                                        <div class="col-lg-2 col-md-12 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item text-center">
                                                    <asp:Image ID="imgPhoto" runat="server"  Width="140px" Height="140px" />
                                                </li>
                                            </ul>
                                        </div>--%>
                                        <div class="col-lg-5 col-md-12 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Student Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblStudentName1" runat="server" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Apply date :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblApplydate" runat="server" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-5 col-md-12 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Student Id :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblAdmissionNo" runat="server" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>

                                                <li class="list-group-item"><b>Semester :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblSemester" runat="server" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>

                                    </div>
                                </div>
                                <br />
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Session</label>
                                            </div>
                                            <asp:DropDownList runat="server" class="form-control" ID="ddlsession" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlsession_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlsession" InitialValue="0"
                                                Display="None" ErrorMessage="Please select Session"
                                                SetFocusOnError="true" ValidationGroup="submitt" />
                                        </div>
                                    </div>
                                </div>

                               
                                <div class="col-12" runat="server" visible="false" id="Coursesdetails">

                                    <div class="col-12">
                                         <div class="row">
                                            <div class="col-lg-3 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Selected Course Fee :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSelectedCourseFee" runat="server" Text="0.00"></asp:Label>
                                                            <asp:HiddenField ID="hdnSelectedCourseFee" runat="server" Value="0" />                                    
                                                            <asp:HiddenField ID="hdfAmount" runat="server" Value="0" />
                                                            <asp:HiddenField ID="hdfServiceCharge" runat="server" Value="0" />
                                                            <asp:HiddenField ID="hdfTotalAmount" runat="server" Value="0" />
                                                        </a>
                                                    </li>
                                                </ul>

                                            </div>
                                        </div>
                                        <br />
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:ListView ID="lvAttendanceDetails" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Student Courses List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblCourseLst">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>
                                                                    <%--<asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckbox(this)" />--%>

                                                                    Select
                                                                </th>
                                                                <th>Courses
                                                                </th>
                                                                <th>Current Attendance(%)
                                                                </th>
                                                                <th>Min Current Attendance(%)
                                                                </th>
                                                                <th>Shortage Attendance(%)
                                                                </th>
                                                                <th>Action
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
<%--                                                            <asp:CheckBox ID="chkallotment" runat="server" onclick="ValidateFeeDetail()"  ToolTip='<%# Eval("COURSENO") %>' Enabled='<%#(Eval("SHOERAGEATTMAINE").ToString().Substring(0,1))== "-" ?  false : true %>' />--%>
                                                     <asp:CheckBox ID="chkallotment" runat="server" onclick="ValidateFeeDetail()"  ToolTip='<%# Eval("COURSENO") %>'  />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblcoursename" runat="server" Text='<%# Eval("COURSE") %>'></asp:Label>

                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblattendace" runat="server" Text='<%# Eval("ATTENDANCE%") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblrange" runat="server" Text='<%# Eval("RANGE") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdfrangfrom" runat="server" Value='<%# Eval("RANGE_FROM") %>' />
                                                              <asp:HiddenField ID="hdfoperatorfrom" runat="server" Value='<%# Eval("OPERATOR_FROM") %>' />
                                                              <asp:HiddenField ID="hdfopratoto" runat="server" Value='<%# Eval("OPERATOR_TO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblshortage" runat="server" Text='<%# Eval("SHOERAGEATTMAINE") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdfcoursefees" runat="server" Value='<%# Eval("MODULEFEES") %>' />
                                                             <asp:HiddenField ID="hdnoverallcoursefee" runat="server"  Value='<%# Eval("OVERALLFEES_DEFINE") %>' />                                                      
                                                        </td>
                                                    </tr>

                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-12 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Reason</label>
                                                </div>
                                                <asp:TextBox ID="txtRequestDescription" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRequestDescription"
                                                    Display="None" ErrorMessage="Please Enter Reason." ValidationGroup="Submit"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>
                                    
                                            <div class="form-group col-lg-6 col-md-12 col-12">
                                                <div class="label-dynamic">
                                                    <label style="color: black;">Attachment</label>
                                                </div>
                                                <div class="logoContainer">
                                                    <img src='<%=Page.ResolveClientUrl("~/Images/default-fileupload.png")%>' alt="upload image" tabindex="2"/>
                                                </div>
                                                <div class="fileContainer sprite pl-1">
                                                    <span runat="server" id="ufFile"
                                                        cssclass="form-control" tabindex="7">Upload File</span>
                                                    <asp:FileUpload ID="fuDocument" runat="server" ToolTip="Select file to upload"
                                                        CssClass="form-control" onkeypress="" />
                                                </div>
                                                <span style="color: red; font-size: small">
                                                    <asp:Label ID="lblMsg" runat="server" Text="Document Size Should be 1 MB and allow only PDF format." meta:resourcekey="lblMsgResource1"></asp:Label>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ValidationGroup="Submit" />
                                        <asp:Button ID="btnreport" runat="server" Text="Report"  OnClick="btnreport_Click" Visible="false" CssClass="btn btn-primary"/>
                                        <asp:Button ID="btnPy" runat="server" Text="Pay" TextMode="MultiLine" OnClick="btnPy_Click" CssClass  ="btn btn-primary"  />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                        <asp:ValidationSummary ID="valSummery1" DisplayMode="List" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Submit" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSubmit" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript" language="javascript">

        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvAttendanceDetails$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvAttendanceDetails$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>

    <script>
        $(document).ready(function () {
            $(document).on("click", ".logoContainer", function () {
                $("#ctl00_ContentPlaceHolder1_fuDocument").click();
            });
            $(document).on("keydown", ".logoContainer", function () {
                if (event.keyCode === 13) {
                    // Cancel the default action, if needed
                    event.preventDefault();
                    // Trigger the button element with a click
                    $("#ctl00_ContentPlaceHolder1_fuDocument").click();
                }
            });
        });
    </script>

    <script>
        $("input:file").change(function () {
            var maxFileSize = 1000000;
            var fi = document.getElementById('ctl00_ContentPlaceHolder1_fuDocument');
            myfile = $(this).val();
            var ext = myfile.split('.').pop();
            var res = ext.toUpperCase();
            if (res != "PDF") {
                alert("Please Select PDF File Only.");
                //$('.logoContainer img').attr('src', "../IMAGES/excel.png");
                $(this).val('');
            }
            //$(this).val('');
            //document.getElementById('ctl00_ContentPlaceHolder1_fuDocument').value = "";               
            for (var i = 0; i <= fi.files.length - 1; i++) {
                //alert("hii")
                var fsize = fi.files.item(i).size;
                //alert(fsize)
                if (fsize >= maxFileSize) {
                    alert("File Size should be less than 1 MB");
                    $("#ctl00_ContentPlaceHolder1_fuDocument").val("");
                }
            }

        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("input:file").change(function () {
                var maxFileSize = 1000000;
                var fi = document.getElementById('ctl00_ContentPlaceHolder1_fuDocument');
                myfile = $(this).val();
                var ext = myfile.split('.').pop();
                var res = ext.toUpperCase();
                if (res != "PDF") {
                    alert("Please Select PDF File Only.");
                    //$('.logoContainer img').attr('src', "../IMAGES/excel.png");
                    $(this).val('');
                }
                //$(this).val('');
                //document.getElementById('ctl00_ContentPlaceHolder1_fuDocument').value = "";               
                for (var i = 0; i <= fi.files.length - 1; i++) {
                    //alert("hii")
                    var fsize = fi.files.item(i).size;
                    //alert(fsize)
                    if (fsize >= maxFileSize) {
                        alert("File Size should be less than 1 MB");
                        $("#ctl00_ContentPlaceHolder1_fuDocument").val("");
                    }
                }

            });
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
                    //$('.logoContainer img').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        $("input:file").change(function () {
            readURL(this);
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
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
                        //$('.logoContainer img').attr('src', e.target.result);
                    }
                    reader.readAsDataURL(input.files[0]);
                }
            }
            $("input:file").change(function () {
                readURL(this);
            });
        });
    </script>
    <script type="text/javascript">
        function ValidateFeeDetail() {
            debugger;

            var totCoursefee = 0.0;
            var listview = document.getElementById('tblCourseLst');
            try {

                for (j = 0; j < listview.rows.length - 1 ; j++) {
                    var chkid = 'ctl00_ContentPlaceHolder1_lvAttendanceDetails_ctrl' + j + '_chkallotment';
                    var overallfees = document.getElementById('ctl00_ContentPlaceHolder1_lvAttendanceDetails_ctrl' + j + '_hdnoverallcoursefee').value;
                    var coursefees = document.getElementById('ctl00_ContentPlaceHolder1_lvAttendanceDetails_ctrl' + j + '_hdfcoursefees').value;
                    if (document.getElementById(chkid).checked) {
                        var selAmt = 0.0;
                        selAmt = parseInt(coursefees);
                        var ovamt = 0.0;
                        ovamt = parseInt(overallfees);
                        if (overallfees == null || overallfees == "" || overallfees == "NaN") {
                           
                            totCoursefee = totCoursefee + selAmt;
                        }
                        else {
                            totCoursefee = ovamt;
                        }
                    }
                }
                document.getElementById('ctl00_ContentPlaceHolder1_lblSelectedCourseFee').innerHTML = totCoursefee;
                document.getElementById('ctl00_ContentPlaceHolder1_hdnSelectedCourseFee').value = totCoursefee;
            }
            catch (e) {
                //alert(e);
                valid = false;
            }
            return valid;
        }
    </script>

</asp:Content>

