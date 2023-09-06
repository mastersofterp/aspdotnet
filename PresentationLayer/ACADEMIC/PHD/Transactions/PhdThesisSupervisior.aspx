<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PhdThesisSupervisior.aspx.cs" Inherits="Academic_PhdThesisSupervisior" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
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
    <%--  <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>--%>
    <!--Create New User-->
    <asp:Panel ID="pnDisplay" runat="server">
        <div class="col-sm-12">

            <div class="box box-info">
                <div class="box-header with-border">
                    <h3 class="box-title">PHD THESIS DETAILS</h3>
                    <div class="notice"><span>Note : * marked fields are Mandatory</span></div>
                    <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                </div>



                <asp:Panel ID="pnlsearch" runat="server">
                                                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <div class="col-12">
                                                                    <div class="row">
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <label>Search Criteria</label>
                                                                            </div>

                                                                            <%--onchange=" return ddlSearch_change();"--%>
                                                                            <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                               
                                                                            </asp:DropDownList>

                                                                        </div>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">


                                                                            <asp:Panel ID="pnltextbox" runat="server" >
                                                                                <div id="divtxt" runat="server" style="display: block">
                                                                                    <div class="label-dynamic">
                                                                                        <label>Search String</label>
                                                                                    </div>
                                                                                    <asp:TextBox ID="txtsearchstu" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                                                                </div>
                                                                            </asp:Panel>
                                                                                <asp:Panel ID="pnlDropdown" runat="server" >
                                                                                <div id="divDropDown" runat="server" style="display: block">
                                                                                    <div class="label-dynamic">
                                                                                        <%-- <label id="lblDropdown"></label>--%>
                                                                                        <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                                                                    </div>
                                                                                    <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                                                    </asp:DropDownList>

                                                                                </div>
                                                                                      </asp:Panel>

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-12 btn-footer">
                                                                        <%-- OnClientClick="return submitPopup(this.name);"--%>
                                                                        <asp:Button ID="btnsearchstu" runat="server" Text="Search"  CssClass="btn btn-primary" OnClientClick="return submitPopup(this.name);" OnClick="btnsearchstu_Click"/>
                                                                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-warning"  OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" OnClick="btnClose_Click" />
                                                                    </div>
                                                                    <div class="col-12 btn-footer">
                                                                        <asp:Label ID="Label3" runat="server" SkinID="lblmsg" />
                                                                    </div>

                                                                 <div class="col-12">
                                                                        <asp:Panel ID="pnlLV" runat="server">
                                                                            <asp:ListView ID="liststudent" runat="server">
                                                                                <LayoutTemplate>
                                                                                    <div>
                                                                                        <div class="sub-heading">
                                                                                            <h5>Student List</h5>
                                                                                        </div>
                                                                                        <asp:Panel ID="Panel2" runat="server">
                                                                                            <table class="table table-striped table-bordered " >
                                                                                                <thead class="bg-light-blue">
                                                                                                    <tr>
                                                                                                        <th>Name
                                                                                                        </th>
                                                                                                        <th style="display: none">IdNo
                                                                                                        </th>
                                                                                                        <th>
                                                                                                            <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true">RRN</asp:Label>
                                                                                                        </th>
                                                                                                        <th>
                                                                                                            <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true">BRANCH</asp:Label>
                                                                                                        </th>
                                                                                                        <th>
                                                                                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true">SEMESTER</asp:Label>
                                                                                                        </th>
                                                                                                        <th>Father Name
                                                                                                        </th>
                                                                                                        <th>Mother Name
                                                                                                        </th>
                                                                                                        <th>Mobile No.
                                                                                                        </th>
                                                                                                    </tr>
                                                                                                </thead>
                                                                                                <tbody>
                                                                                                    <tr id="itemPlaceholder" runat="server"/>
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </asp:Panel>
                                                                                    </div>

                                                                                </LayoutTemplate>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                                                OnClick="lnkId_Click"></asp:LinkButton>
                                                                                        </td>
                                                                                        <td style="display: none">
                                                                                            <%# Eval("idno")%>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                                                        </td>
                                                                                        <td>
                                                                                            <%# Eval("SEMESTERNO")%>
                                                                                        </td>
                                                                                        <td>
                                                                                            <%# Eval("FATHERNAME") %>
                                                                                        </td>
                                                                                        <td>
                                                                                            <%# Eval("MOTHERNAME") %>
                                                                                        </td>
                                                                                        <td>
                                                                                            <%#Eval("STUDENTMOBILE") %>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>
                                                                        </asp:Panel>
                                                                    </div>
                                                            </ContentTemplate>
                                                      <Triggers>
                                                         <asp:PostBackTrigger ControlID="liststudent" />
                                                      </Triggers>
                                                        </asp:UpdatePanel>

                    </asp:Panel>



                <div class="box-body">
                    <%-----GENRAL INFO-------%>
                    <div class="col-sm-12 form-group">
                        <div class="row">

                           <!-- general form elements -->
                            <div class="box box-primary ">
                                <div class="box-header with-border" id="dvgeneral" runat="server">
                                    <h3 class="box-title">General Student Information</h3>
                                    <div class="box-tools pull-right">
                                        <a href="#slide" data-toggle="collapse">
                                            <span class="btn btn-box-tool" data-widget="collapse">
                                                <i class="fa fa-minus"></i>
                                            </span>
                                        </a>
                                    </div>
                                </div>
                                <div id="slide" >
                                    <div class="box-body">
                                        <div id="divGeneralInfo" style="display: block;" runat="server">

                                            <asp:Panel ID="pnlId" runat="server">
                                                <div class="form-group col-sm-4 col-sm-offset-4">
                                                    <div class="input-group date">
                                                        <asp:TextBox ID="txtIDNo" runat="server" class="form-control" TabIndex="1" Enabled="False" placeholder="Search Here..." />
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgSearch" runat="server" ImageUrl="~/IMAGES/search.png" TabIndex="1" data-toggle="modal" data-target="#myModals"
                                                                AlternateText="Search" ToolTip="Search Student by IDNo, Name, Reg. No, Branch, Semester" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>


                                       <%-- <div class="col-12" id="dvdetails" runat="server">

                                                    <%--Search Pannel Start by Swapnil --%>
                                                   <%-- <div id="myModal2" role="dialog" runat="server">
                                                        

                                                      
                                                    </div>
                                                    <%--Search Pannel End--%>
                                             <%--   </div>--%>


                            </div>
                        </div>
                    </div>
                    </div>
                 <%--   </div>
                    </div>--%>

                        <asp:Panel ID="pnldetails" runat="server">
                                        <div class="col-sm-12" >
                                            <div class="col-md-6">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item">
                                                        <b>ID No.:</b><a class="pull-right">
                                                            <asp:Label ID="lblidno" runat="server" Font-Bold="True"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item">
                                                        <b>Enrollment No :</b><a class="pull-right">
                                                            <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item">
                                                        <b>Date of Joining :</b><a class="pull-right">
                                                            <asp:Label ID="lblDateOfJoining" runat="server" Font-Bold="True"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item">
                                                        <b>Status :</b><a class="pull-right">
                                                            <asp:Label ID="lblStatus" runat="server" Font-Bold="True"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item">
                                                        <b>Credit Given :</b><a class="pull-right">
                                                            <asp:Label ID="lblCreditgiven" runat="server" Font-Bold="True"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item">
                                                        <b>Credit Completed :</b><a class="pull-right">
                                                            <asp:Label ID="lblCreditCmplt" runat="server" Font-Bold="True"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item">
                                                        <b>Date of Synopsis Submission :</b><a class="pull-right">
                                                            <asp:Label ID="lblsyssdate" runat="server" Font-Bold="True"></asp:Label>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div class="col-md-6">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item">
                                                        <b>Student Name :</b><a class="pull-right">
                                                            <asp:Label ID="lblStudName" runat="server" Font-Bold="True"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item">
                                                        <b>Father Name :</b><a class="pull-right">
                                                            <asp:Label ID="lblFatherName" runat="server" Font-Bold="True"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item">
                                                        <b>Department :</b><a class="pull-right">
                                                            <asp:Label ID="lblBranch" runat="server" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item">
                                                        <b>Supervisor :</b><a class="pull-right">
                                                            <asp:Label ID="lblSupervisor" runat="server" Font-Bold="True"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item">
                                                        <b>Due Date of Thesis Submission :</b><a class="pull-right">
                                                            <asp:Label ID="lblthsdate" runat="server" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item">
                                                        <b>Date of Thesis Extension :</b><a class="pull-right">
                                                            <asp:Label ID="lblthsextn" runat="server" Font-Bold="True"></asp:Label>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                            </asp:Panel>
                                        <div class="col-sm-6 form-group" id="dvuploadthesis" runat="server">
                                            <label><span style="color: red;">*</span>Upload Thesis File :</label>

                                            <asp:FileUpload ID="fuExtended" runat="server" CssClass="form-control" ToolTip="Select file to upload" accept=".pdf" />

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="file Required"
                                                ControlToValidate="fuExtended" ValidationGroup="submit"
                                                runat="server" Display="Dynamic" ForeColor="Red" />

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.PDF|.pdf)$"
                                                ControlToValidate="fuExtended" runat="server" ForeColor="Red" ErrorMessage="Please select a valid PDF File ." ValidationGroup="submit" Display="Dynamic" />
                                        </div>
                                        <div class="form-group col-md-12 table-responsive">
                                            <asp:ListView ID="lvUpload" runat="server" Visible="true">
                                                <LayoutTemplate>
                                                    <div id="demo-grid" class="vista-grid">
                                                        <div class="titlebar">
                                                            <h4>
                                                                <label class="label label-default">Phd Thesis list</label></h4>
                                                        </div>
                                                        <table id="example1" class="table table-bordered table-hover text-center">
                                                            <tr class="bg-light-blue">
                                                                <th>Thesis Document
                                                                </th>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="item">
                                                        <td>
                                                            <asp:LinkButton ID="lnkDownloadDoc" runat="server" OnClick="lnkDownloadDoc_Click"
                                                                class="form-control" Text='<%# Eval("FILENAME") %>' CommandArgument='<%# Eval("IDNO") %>'>
                                                            </asp:LinkButton>
                                                            <asp:HiddenField ID="hdfFilename" runat="server" Value='<%#Eval("PATH") %> ' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                        <div class="text-center form-group col-sm-12 " id="dvbutton" runat="server">
                                            <p>
                                                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                                            </p>
                                            <asp:Button ID="Btnsubmit" runat="server" Text="Submit Details" ValidationGroup="submit"
                                                CssClass="btn btn-primary" OnClick="Btnsubmit_Click" />
                                            <asp:Button ID="btncertificate" runat="server" Text="Thesis Uploaded Report"
                                                CssClass="btn btn-info" OnClick="btncertificate_Click" />

                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="9" CssClass="btn btn-danger"
                                                OnClick="btnCancel_Click" />

                                            <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="submit"
                                                DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                   


    </asp:Panel>

    <%-- </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="fuExtended" />
        </Triggers>
    </asp:UpdatePanel>--%>
    <!-- Modal -->
<%--    <div class="modal fade" id="myModals" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Serach Details</h4>
                </div>
                <div class="modal-body" id="divdemo2">
                    <div class="container-fluid">
                        <asp:UpdatePanel ID="updEdit" runat="server">
                            <ContentTemplate>

                                <div>
                                    <label for="city  label label-default">Search Criteria </label>
                                    <br />
                                    <span>
                                        <asp:RadioButton ID="rbName" runat="server" Text="Name" GroupName="edit" /></span>
                                    <span>
                                        <asp:RadioButton ID="rbIdNo" runat="server" Text="IdNo" GroupName="edit" /></span>
                                    <span>
                                        <asp:RadioButton ID="rbBranch" runat="server" Text="Branch" GroupName="edit" /></span>
                                    <span>
                                        <asp:RadioButton ID="rbSemester" runat="server" Text="Semester" GroupName="edit" /></span>
                                    <span>
                                        <asp:RadioButton ID="rbEnrollmentNo" runat="server" Text="Enrollmentno" GroupName="edit"
                                            Checked="True" /></span>
                                    <span>
                                        <asp:RadioButton ID="rbRegNo" runat="server" Text="Rollno" GroupName="edit"
                                            Checked="True" />
                                    </span>

                                    <div class="form-group">
                                        <label for="city">Search String </label>
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group text-center">
                                        <p class="text-center">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="return submitPopup(this.name);" CssClass="btn btn-info" />
                                            <button type="button" class="btn btn-warning" data-dismiss="modal">Close</button>
                                            <br />
                                            <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                        </p>
                                    </div>

                                    <div class="form-group col-md-12">
                                        <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="updEdit">
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

                                    <div class=" col-md-12  table-responsive" style="height: auto;">
                                        <asp:ListView ID="lvStudent" runat="server">
                                            <LayoutTemplate>
                                                <div class="vista-grid">
                                                    <div class="titlebar">
                                                        <h4>
                                                            <label class="label label-default">Login Details </label>
                                                        </h4>
                                                    </div>
                                                    <table id="id1" class="table table-hover table-bordered">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Name
                                                                </th>
                                                                <th>IdNo
                                                                </th>
                                                                <th>Roll No.
                                                                </th>
                                                                <th>Branch
                                                                </th>
                                                                <th>Semester
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
                                                <tr class="item">
                                                    <td>
                                                        <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                            OnClick="lnkId_Click"></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <%# Eval("idno")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("EnrollmentNo")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("longname")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("semesterno")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>--%>

    <script type="text/javascript" lang="javascript">
        
    </script>



     <script type="text/javascript" lang="javascript">

         $(document).ready(function () {
             debugger
             $("#<%= pnltextbox.ClientID %>").hide();

            $("#<%= pnlDropdown.ClientID %>").hide();
        });
        function submitPopupsearch(btnsearchstu) {

            debugger
            var rbText;
            var searchtxt;

            var e = document.getElementById("<%=ddlSearch.ClientID%>");
            var rbText = e.options[e.selectedIndex].text;
            var ddlname = e.options[e.selectedIndex].text;
            if (rbText == "Please Select") {
                alert('Please Select Search Criteria.')
                $(e).focus();
                return false;
            }

            else {


                if (rbText == "ddl") {
                    var skillsSelect = document.getElementById("<%=ddlDropdown.ClientID%>").value;

                    var searchtxt = skillsSelect;
                    if (searchtxt == "0") {
                        alert('Please Select ' + ddlname + '..!');
                    }
                    else {
                        __doPostBack(btnsearchstu, rbText + ',' + searchtxt);
                        return true;
                        //$("#<%= divpanel.ClientID %>").hide();
                        $("#<%= pnltextbox.ClientID %>").hide();

                    }
                }
                else if (rbText == "BRANCH") {

                    if (searchtxt == "Please Select") {
                        alert('Please Select Branch..!');

                    }
                    else {
                        __doPostBack(btnsearchstu, rbText + ',' + searchtxt);

                        return true;
                    }

                }
                else {
                    searchtxt = document.getElementById('<%=txtsearchstu.ClientID %>').value;
                    if (searchtxt == "" || searchtxt == null) {
                        alert('Please Enter Data to Search.');
                        //$(searchtxt).focus();
                        document.getElementById('<%=txtsearchstu.ClientID %>').focus();
                        return false;
                    }
                    else {
                        __doPostBack(btnsearchstu, rbText + ',' + searchtxt);
                        //$("#<%= divpanel.ClientID %>").hide();
                        //$("#<%= pnltextbox.ClientID %>").show();

                        return true;
                    }
                }
        }
    }

    function ClearSearchBox(btncancelmodal) {
        document.getElementById('<%=txtsearchstu.ClientID %>').value = '';
            __doPostBack(btncancelmodal, '');
            return true;
        }
        function CloseSearchBox(btnClose) {
            document.getElementById('<%=txtsearchstu.ClientID %>').value = '';
        __doPostBack(btnClose, '');
        return true;
    }




    function Validate() {

        debugger

        var rbText;

        var e = document.getElementById("<%=ddlSearch.ClientID%>");
        var rbText = e.options[e.selectedIndex].text;

        if (rbText == "IDNO" || rbText == "Mobile") {

            var char = (event.which) ? event.which : event.keyCode;
            if (char >= 48 && char <= 57) {
                return true;
            }
            else {
                return false;
            }
        }
        else if (rbText == "NAME") {

            var char = (event.which) ? event.which : event.keyCode;

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122)) {
                return true;
            }
            else {
                return false;
            }

        }
    }
    </script>

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('#id1').dataTable({
                paging: false,
                searching: true,
                bDestroy: true
            });
        });


    </script>
</asp:Content>
