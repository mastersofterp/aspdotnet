<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="Document_Status.aspx.cs" Inherits="ACADEMIC_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script type="text/javascript">
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



    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><%--Document Status--%>
                        <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>
                    </h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlsearchnew" runat="server">
                        <asp:UpdatePanel ID="updEdit" runat="server">
                            <ContentTemplate>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <asp:UpdatePanel ID="upnldearch" runat="server"></asp:UpdatePanel>
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Search Criteria</label>
                                            </div>

                                            <%--onchange=" return ddlSearch_change();"--%>
                                            <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <%-- <asp:ListItem>Please Select</asp:ListItem>
                                                        <asp:ListItem>BRANCH</asp:ListItem>
                                                        <asp:ListItem>ENROLLMENT NUMBER</asp:ListItem>
                                                        <asp:ListItem>REGISTRATION NUMBER</asp:ListItem>
                                                        <asp:ListItem>FatherName</asp:ListItem>
                                                        <asp:ListItem>IDNO</asp:ListItem>
                                                        <asp:ListItem>MOBILE NUMBER</asp:ListItem>
                                                        <asp:ListItem>MotherName</asp:ListItem>
                                                        <asp:ListItem>NAME</asp:ListItem>
                                                        <asp:ListItem>ROLLNO</asp:ListItem>
                                                        <asp:ListItem>SEMESTER</asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="valBankName" runat="server" ControlToValidate="ddlSearch"
                                                Display="None" ErrorMessage="Please select Search Criteria." ValidationGroup="docstatus"
                                                InitialValue="0" SetFocusOnError="true" />

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">


                                            <asp:Panel ID="pnltextbox" runat="server">
                                                <div id="divtxt" runat="server" style="display: block">
                                                    <div class="label-dynamic">
                                                        <label>Search String</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                                </div>
                                            </asp:Panel>

                                            <asp:Panel ID="pnlDropdown" runat="server">
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
                                        <asp:UpdatePanel ID="upvlbtnsearch" runat="server">
                                            <ContentTemplate>

                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" ValidationGroup="docstatus" />
                                                <asp:ValidationSummary ID="valSummary4" DisplayMode="List" runat="server" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="docstatus" />
                                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnSearch" />
                                                <asp:PostBackTrigger ControlID="btnClose" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                    </div>

                                    <div class="col-12">
                                        <asp:UpdatePanel ID="upnllvStudent" runat="server">
                                            <ContentTemplate>

                                                <asp:Panel ID="Panel3" runat="server">
                                                    <asp:ListView ID="lvStudent" runat="server">
                                                        <LayoutTemplate>
                                                            <div>
                                                                <div class="sub-heading">
                                                                    <h5>Student List</h5>
                                                                </div>
                                                                <asp:Panel ID="Panel2" runat="server">
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Name
                                                                                </th>
                                                                                <th>IdNo
                                                                                </th>
                                                                                <th>Roll No.
                                                                                </th>
                                                                                <th><%--Branch--%>
                                                                                    <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th><%--Semester--%>
                                                                                    <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
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
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                </asp:Panel>
                                                            </div>

                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                        OnClick="lnkId_Click"></asp:LinkButton>
                                                                </td>
                                                                <td>
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
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lvStudent" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                            </ContentTemplate>
                            <%--<Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlSearch" />
                        </Triggers>--%>
                        </asp:UpdatePanel>
                    </asp:Panel>
















                    <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <%-- <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Search By</label>
                                    </div>
                                    <asp:Label ID="lblEnrollno" runat="server" Font-Bold="true" >Enrollment No./Regstration No.</asp:Label>
                                 
                                </div>
                                <div>
                                   <asp:TextBox ID="txtEnrollment" runat="server" ValidationGroup="search" class="form-control"></asp:TextBox>
                                </div>--%>
                                </div>
                            </div>
                        </div>



                        <div id="divstuddetails" class="col-12" runat="server" visible="false">
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
                                                    <asp:Label ID="lblDegee" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                </a>
                                            </li>

                                            <li class="list-group-item"><b>Branch:</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-12 pl-md-0">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Degree. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Semester :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>

                                        </ul>
                                    </div>
                                </div>
                            </div>






                        </div>
                        <br />

                        <asp:Panel ID="pnlrdb" runat="server">
                            <div id="divStatus" class="form-group col-lg-6 col-md-6 col-12 checkbox-list-column">
                                <div class="label-dynamic">
                                    <label>Document Status:</label>&nbsp;&nbsp;
                                                   
                                                   
                                                    <asp:RadioButton ID="rdoSubmit" runat="server" Text="Submitted" Checked="true" GroupName="act_status"
                                                        TabIndex="5" Visible="true" />&nbsp&nbsp
                                                    <asp:RadioButton ID="rdoReturn" runat="server" Text="Return" GroupName="act_status"
                                                        TabIndex="10" Visible="true" />
                                </div>


                            </div>
                            <br />
                        </asp:Panel>



                        <div class="form-group col-lg-12 col-md-12 col-12 text-center">

                            <%--<asp:Button ID="btnShow" runat="server" Text="Search" ValidationGroup="search" OnClick="btnShow_Click"  CssClass="btn btn-info" />
                                    <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtEnrollment" Display="None"
                                        ErrorMessage="Please Enter Enrollment No. or Registration No." SetFocusOnError="True" ValidationGroup="search" />
                                    <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                        ValidationGroup="search" />--%>
                            <asp:UpdatePanel ID="pnlbtn" runat="server">
                                <ContentTemplate>


                                    <%-- <asp:Panel ID="pnlbtn" runat="server">--%>
                                    <%-- <div class="row">--%>
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />

                                    <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnReport_Click" />

                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                                    <%-- </div>--%>
                                    <%-- </asp:Panel>--%>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSubmit" />
                                    <asp:PostBackTrigger ControlID="btnReport" />
                                    <asp:PostBackTrigger ControlID="btnCancel" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>

                    </asp:Panel>



                    <%--           <div id="div1" class="col-12" runat="server" visible="false">
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
                                                <asp:Label ID="lblDegee" runat="server" Font-Bold="True" Visible="false"></asp:Label> 
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Degree. :</b>
                                            <a class="sub-label"><asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Branch:</b>
                                            <a class="sub-label"><asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>       
                                    </ul>
                                </div>
                                <div class="col-lg-6 col-md-6 col-12 pl-md-0">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Semester :</b>
                                            <a class="sub-label"><asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label> </a>
                                        </li>
                                        
                                    </ul>
                                </div>
                            </div>
                        </div>--%>






                    <div id="divDetails" class="col-12" runat="server" visible="true">
                        <div class="col-12" style="margin-top: 10px;">
                            <asp:Panel ID="pnlBind" runat="server">
                                <asp:Label ID="lblmessageShow" runat="server"></asp:Label>
                                <asp:UpdatePanel ID="upnllvBinddata" runat="server">
                                    <ContentTemplate>
                                        <asp:ListView ID="lvBinddata" runat="server" OnItemDataBound="lvBinddata_ItemDataBound">
                                            <LayoutTemplate>
                                                <div id="demo-grid">
                                                    <div class="sub-heading">
                                                        <h5>Document List</h5>
                                                    </div>
                                                    <div style="width: 100%; height: 400px; overflow: auto">
                                                        <table class="table table-striped table-bordered nowrap">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Sr.NO</th>
                                                                    <%-- ToolTip='<%# Eval("IDNo")%>' --%>
                                                                    <th>
                                                                        <asp:CheckBox ID="CheckBox1" runat="server" onclick="totAllSubjects(this)" />Select All

                                                                    </th>
                                                                    <%--  <th>  <asp:CheckBox ID="CheckBox1" runat="server" />Select All</th>--%>
                                                                    <th>Document Name</th>
                                                                    <th>Remark</th>
                                                                    <th>STATUS</th>
                                                                    <th>Date</th>
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
                                                    <td style="width: 5px"><%# Container.DataItemIndex +1 %></td>
                                                    <td>
                                                        <asp:CheckBox ID="chkDocsingle" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblStar" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblDocument" runat="server" ToolTip='<%# Eval("DOCUMENTNO") %>' Text='<%# Eval("DOCUMENTNAME") %>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("DOCUMENTNO") %>' />
                                                    </td>

                                                    <%-- <td style="width:20%"><%# Eval("DOCUMENTNAME") %></td>--%>
                                                    <td>
                                                        <asp:TextBox ID="txtDoc" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <%# Eval("STATUS") %>                                                                                
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                    </td>


                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="lvBinddata" />
                                        <asp:PostBackTrigger ControlID="btnsubmit" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </asp:Panel>

                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div id="divMsg" runat="server">
        </div>

        <script type="text/javascript">
            function totAllSubjects(headchk) {
                var frm = document.forms[0]
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    var e = frm.elements[i];
                    if (e.type == 'checkbox') {
                        if (headchk.checked == true)
                            e.checked = true;
                        else {
                            e.checked = false;
                            headchk.checked = false;
                        }
                    }

                }

            }
        </script>
        <script>
            function CloseSearchBox(btnClose) {
                document.getElementById('<%=txtSearch.ClientID %>').value = '';
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

        <script type="text/javascript" lang="javascript">

            $(document).ready(function () {
                debugger
                // $("#<%= pnltextbox.ClientID %>").hide();

                $("#<%= pnlDropdown.ClientID %>").hide();
            });
            function submitPopup(btnsearch) {

                debugger
                var rbText;
                var searchtxt;

                var e = document.getElementById("<%=ddlSearch.ClientID%>");
            var rbText = e.options[e.selectedIndex].text;
            var ddlname = e.options[e.selectedIndex].text;
            if (rbText == "Please Select") {
                alert('Please select Criteria as you want search...')
            }

            else {


                if (rbText == "ddl") {
                    var skillsSelect = document.getElementById("<%=ddlDropdown.ClientID%>").value;

                    var searchtxt = skillsSelect;
                    if (searchtxt == "0") {
                        alert('Please Select ' + ddlname + '..!');
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
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
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);

                        return true;
                    }

                }
                else {
                    searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;
                    if (searchtxt == "" || searchtxt == null) {
                        alert('Please Enter Data you want to search..');
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        //$("#<%= divpanel.ClientID %>").hide();
                        //$("#<%= pnltextbox.ClientID %>").show();

                        return true;
                    }
                }
        }
    }

    function ClearSearchBox(btncancelmodal) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btncancelmodal, '');
        return true;
    }
    function CloseSearchBox(btnClose) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
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

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122) || (char = 32)) {
                return true;
            }
            else {
                return false;
            }

        }
    }


        </script>

        <%--<script>
                        function EnableCurrentRowControls(chkID)
                        {
                        var chkBox = document.getElementById(CheckBox1);
                        var chkIDList = document.getElementsByTagName("input");
                        for (i = 0; i < chkIDList.length; i++)
                        {
                            if (chkIDList[i].type == "checkbox" && chkIDList[i].id != chkBox.id)
                            {

                                if (chkIDList[i].type == "checkbox" && chkIDList[i].id != chkBox.id)
                                {
                                    chkIDList[i].enabled = false;
                                }
                            }
                        }
                    }
                        </script>



        --%>
</asp:Content>


