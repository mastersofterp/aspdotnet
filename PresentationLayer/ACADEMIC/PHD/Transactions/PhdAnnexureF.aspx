<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PhdAnnexureF.aspx.cs" Inherits="Academic_StudentInfoEntry" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="https://www.google.com/jsapi?key=YourKeyHere">
    </script>

    <script lang="javascript" type="text/javascript">
        google.load("elements", "1", {
            packages: "transliteration"
        });
        function onLoad() {
            var options = {
                sourceLanguage: google.elements.transliteration.LanguageCode.ENGLISH,
                destinationLanguage: google.elements.transliteration.LanguageCode.HINDI, // available option English, Bengali, Marathi, Malayalam etc.
                shortcutKey: 'ctrl+g',
                transliterationEnabled: true
            };
            var control = new google.elements.transliteration.TransliterationControl(options);
            control.makeTransliteratable(['ctl00_ContentPlaceHolder1_txtThesistitleHindi']);
        }
        google.setOnLoadCallback(onLoad);
    </script>
    <%-----------------------------------------------------------------------------------------------------------%>

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
    <%-- <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>--%>
    <!--Create New User-->


    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">PHD ANNEXURE-F</h3>
                </div>

                <div class="box-body">
                    <asp:UpdatePanel ID="updEdit" runat="server">
                        <ContentTemplate>


                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
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

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">


                                        <asp:Panel ID="pnltextbox" runat="server">
                                            <div id="divtxt" runat="server" style="display: block">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Search String</label>
                                                </div>
                                                <%--onkeypress="return Validate()"--%>
                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
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
                                    <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" CssClass="btn btn-primary" OnClientClick="return submitPopup(this.name);" />

                                    <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                </div>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panellistview" runat="server">
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
                                                                <th style="display: none">IdNo
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
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
                            <asp:PostBackTrigger ControlID="lvStudent" />
                        </Triggers>
                    </asp:UpdatePanel>

                    <asp:Panel ID="pnDisplay" runat="server">
                        <div class="col-12">

                            <div id="divGeneralInfo" style="display: block;">
                            </div>

                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>General Information</h5>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>ID No. :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Enrollment No. :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Date of Joining :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblDateOfJoining" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Status :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblStatus" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Student Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblStudName" runat="server" Font-Bold="True"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Father Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblFatherName" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Department :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblBranch" runat="server" Font-Bold="true"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Supervisor :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblSupervisor" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>

                        <div class="col-12 mt-3">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Discipline</label>
                                    </div>
                                    <asp:DropDownList ID="ddlDescipline" runat="server" TabIndex="2" CssClass="form-control"
                                        ToolTip="Please Select Descipline" data-select2-enable="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlDescipline" ValidationGroup="submit"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Descipline">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Thesis Title</label>
                                    </div>
                                    <asp:TextBox ID="txtThesistitle" runat="server" TabIndex="3" TextMode="MultiLine" ToolTip="Please Enter Thesis Title"
                                        class="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtThesistitle"
                                        Display="None" ErrorMessage="Please Enter Thesis Title" ValidationGroup="submit"
                                        SetFocusOnError="true">  </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-6 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Thesis Title (In Hindi) (Do not translate in Hindi,English title will be same in Hindi)</label>
                                    </div>
                                    <asp:TextBox ID="txtThesistitleHindi" runat="server" TabIndex="4" TextMode="MultiLine" ToolTip="Please Enter Thesis Title In Hindi" class="form-control" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtThesistitleHindi"
                                        Display="None" ErrorMessage="Please Enter Thesis Title In Hindi " ValidationGroup="submit"
                                        SetFocusOnError="true">  </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="col-12 btn-footer">
                            <%-- <b>NOTE : For Hindi Conversion Use This &nbsp; </b><a href="https://translate.google.com/#en/hi/" style="color: red;" target="_blank">HINDI CONVERSION UTILITY </a>--%>
                            <b>NOTE : For Hindi Conversion Use This &nbsp; </b><a href="https://www.google.com/inputtools/try/" style="color: red;" target="_blank">HINDI CONVERSION UTILITY </a>
                        </div>

                        <div class="col-12 btn-footer">
                            <asp:Button ID="Btnsubmit" runat="server" Text="Submit Details" Visible="false" ValidationGroup="submit"
                                CssClass="btn btn-primary" OnClick="Btnsubmit_Click" />
                            <asp:Button ID="btnpayment" runat="server" Text="Pay Online" Visible="false" ValidationGroup="submit"
                                CssClass="btn btn-primary" OnClick="btnpayment_Click" />
                            <asp:Button ID="btnReport" runat="server" Text="Annexure-F Report"
                                Visible="false" CssClass="btn btn-info" OnClick="btnReport_Click1" TabIndex="5" />

                            <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="submit"
                                DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                        </div>

                        <div class="col-12">
                            <asp:ListView ID="lvphddetails" runat="server">
                                <LayoutTemplate>
                                    <div class="vista-grid">
                                        <div class="sub-heading">
                                            <h5>Student Progress Report Details</h5>
                                        </div>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="id11">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr No .</th>
                                                    <th style="display: none">Download Report  
                                                    </th>
                                                    <th>Session
                                                    </th>
                                                    <th>Supervisior
                                                    </th>
                                                    <th>Joint Supervisior
                                                    </th>
                                                    <th>Institute Faculty
                                                    </th>
                                                    <th>Drc Member
                                                    </th>
                                                    <th>Drc Chairman
                                                    </th>
                                                    <th>Dean 
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
                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label></td>
                                        <td id="phdreceiptdwnld" runat="server" style="display: none">
                                            <asp:LinkButton ID="lblphdReportdownload" runat="server" OnClick="lblphdReportdownload_Click" Visible="false">Click here for Report</asp:LinkButton>
                                        </td>
                                        <td>
                                            <%# Eval("SESSIONNO")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSup" runat="server" Text='<%# Eval("SUPERVISORSTATUS")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbljointsup" runat="server" Text='<%# Eval("JOINTSUPERVISORSTATUS")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblinsfac" runat="server" Text='<%# Eval("INSTITUTEFACULTYSTATUS")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbldrcstatus" runat="server" Text='<%# Eval("DRCSTATUS")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbldrcchairman" runat="server" Text='<%# Eval("DRCCHAIRMAN")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbldean" runat="server" Text='<%# Eval("DEANSTATUS")%>' ToolTip='<%# Eval("DEAN")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" lang="javascript">

        $(document).ready(function () {
            debugger
            $("#<%= pnltextbox.ClientID %>").hide();

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
                        alert('Please Enter Data to Search.');
                        //$(searchtxt).focus();
                        document.getElementById('<%=txtSearch.ClientID %>').focus();
                        return false;
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

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122)) {
                return true;
            }
            else {
                return false;
            }

        }
    }
    </script>
</asp:Content>
