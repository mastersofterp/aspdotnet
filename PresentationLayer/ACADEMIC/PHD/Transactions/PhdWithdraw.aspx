<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PhdWithdraw.aspx.cs" Inherits="Academic_PhdWithdraw" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="pnDisplay"
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

    <style>
        .card-header .title {
            font-size: 15px;
            color: #000;
        }

        .card-header .accicon {
            float: right;
            font-size: 20px;
            width: 1.2em;
        }

        .card-header {
            cursor: pointer;
            border-bottom: none;
            padding: .3rem 0.7rem;
        }

        .card {
            border: 1px solid #ddd;
            border-radius: 0rem;
        }

        .card-body {
            border-top: 1px solid #ddd;
            padding: 1.25rem 0rem;
        }

        .card-header:not(.collapsed) .rotate-icon {
            transform: rotate(180deg);
        }

        input[type=checkbox], input[type=radio] {
            margin: 0px 5px 0 0;
        }

        .fa-plus {
            color: #17a2b8;
            border: 1px solid #17a2b8;
            border-radius: 50%;
            padding: 6px 8px;
        }

        .company-logo img {
            width: 26px;
        }

        .MyLRMar {
            margin-left: 5px;
            margin-right: 5px;
        }
    </style>

    <style>
        #ctl00_ContentPlaceHolder1_lvStudent_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <asp:UpdatePanel ID="pnDisplay" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">PhD Student Withdraw</h3>
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </div>

                        <div class="box-body">

                            <div id="divGeneralInfo" style="display: block;">

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
                                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" OnClientClick="return submitPopup(this.name);" />

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
                                                                            <th>Branch
                                                                                                    <%--<asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                            </th>
                                                                            <th>Semester
                                                                                                    <%--<asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>--%>
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

                                <asp:Panel ID="pnlId" runat="server" Visible="false">
                                    <div class="form-group col-sm-4 col-sm-offset-4" id="dividno" runat="server">
                                        <label>ID No.</label>
                                        <span class="form-inline">
                                            <div class="input-group date">
                                                <asp:TextBox ID="txtIDNo" runat="server" class="form-control" TabIndex="1" Enabled="False" />
                                                <a href="#" title="Search Student for Modification"
                                                    onclick="Modalbox.show($('divdemo2'),title:is.title,width:600,overlayClose:false});return false;"></a>
                                                <div class="input-group-addon">
                                                    <asp:Image ID="imgSearch" runat="server" ImageUrl="~/IMAGES/search.png" TabIndex="1" data-toggle="modal" data-target="#myModal"
                                                        AlternateText="Search" ToolTip="Search Student by IDNo, Name, Reg. No, Branch, Semester" />
                                                </div>
                                            </div>
                                        </span>
                                    </div>
                                </asp:Panel>

                                <div id="divmain" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>General Information</h5>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Enrollment No. :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Date of Admission :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lbladmdate" runat="server" Font-Bold="True"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Status :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblstatus" runat="server" Font-Bold="True"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Total No.of credits :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblcredits" runat="server" Font-Bold="True"></asp:Label></a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Student Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblstudname" runat="server" Font-Bold="True"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Department :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lbldepartment" runat="server" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Supervisor Role :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblsuperrole" runat="server" Font-Bold="True"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Area of Research :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblreasearch" runat="server" Font-Bold="True"></asp:Label></a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>

                                        <div class="row mt-3">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Student Withdraw Remark</label>
                                                </div>
                                                <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtremark"
                                                    Display="None" ErrorMessage="Please Enter Withdraw Remark" ValidationGroup="Academic"
                                                    SetFocusOnError="true">  </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12" id="dgcremark" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Remark</label>
                                                </div>
                                                <asp:TextBox ID="txtdgcremark" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading" id="Div1" runat="server">
                                                    <h5>Doctoral guidance committee (DGC)</h5>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:Panel ID="Panel1" runat="server" Enabled="true">
                                            <div class="row">
                                                <div class="col-lg-6 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Supervisor :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblsup" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Institute faculty Expert :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblinst" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>
                                                        <li class="list-group-item"><b>DRC Chairman :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lbldrcchairman" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Joint-Supervisor *(s)(if any) :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lbljntsup" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>
                                                        <li class="list-group-item"><b>A DRC Nominee :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lbldrc" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>


                                    <div class="col-12 btn-footer mt-3">
                                        <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Academic"
                                            DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />

                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" TabIndex="131" Text="Submit" ValidationGroup="Academic" />
                                        <asp:Button ID="btnApply" runat="server" CssClass="btn btn-primary" OnClick="btnApply_Click" TabIndex="134" Text="Apply Student List" />
                                        <asp:Button ID="btnReport" runat="server" CssClass="btn btn-info" OnClick="btnReport_Click" TabIndex="133" Text="WithDraw Report" Visible="false" />

                                        <asp:Button ID="btnexcel" runat="server" CssClass="btn btn-primary" OnClick="btnexcel_Click" TabIndex="134" Text="Apply Student List Excel" Visible="false" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="132" Text="Cancel" />

                                    </div>
                                </div>
                            </div>
                            <%--</div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnApply" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnexcel" />
        </Triggers>
    </asp:UpdatePanel>

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
