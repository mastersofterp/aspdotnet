<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentInfoEntry.aspx.cs" Inherits="Academic_StudentInfoEntry" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .row {
            display: -ms-flexbox;
            display: flex;
            -ms-flex-wrap: wrap;
            flex-wrap: wrap;
            /*margin-right: 0px;
            margin-left: 0px;*/
        }

        .sidebar-menu {
            padding: 0;
            list-style: none;
        }

            .sidebar-menu .treeview {
                padding: 0px 5px;
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
            padding: 15px 10px;
            margin: 5px 0px 15px 0px;
        }

        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>


    <asp:Panel ID="pnDisplay" Visible="true" Enabled="true" runat="server">
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

                                <div class="col-lg-2 col-md-4 col-12" id="divtabs" runat="server">
                                    <aside class="sidebar">
                                        <!-- sidebar: style can be found in sidebar.less -->
                                        <section class="sidebar" style="background-color: #ffffff">
                                            <ul class="sidebar-menu">
                                                <!-- Optionally, you can add icons to the links -->
                                                <li class="treeview">
                                                    <i class="fa fa-user"></i>
                                                    <span>
                                                        <asp:LinkButton runat="server" ID="lnkPersonalDetail"
                                                            ToolTip="Please select Personal Details" OnClick="lnkPersonalDetail_Click" Text="Personal Details"> 
                                                        </asp:LinkButton>
                                                    </span>
                                                    <hr />
                                                </li>

                                                <li class="treeview">
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

                                                <li class="treeview">
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

                                                <li class="treeview">
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

                                <%--Search Pannel Start by Swapnil --%>
                                <div class="col-lg-10 col-md-8 col-12" id="myModal2" role="dialog" runat="server">
                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updEdit"
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

                                    <asp:UpdatePanel ID="updEdit" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Search Criteria</label>
                                                        </div>

                                                        <%--onchange=" return ddlSearch_change();"--%>
                                                        <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>

                                                    <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="divpanel">
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
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <%-- OnClientClick="return submitPopup(this.name);"--%>
                                                <asp:Button ID="Button1" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" OnClientClick="return submitPopup(this.name);" />
                                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" style="font-weight:600;"/>
                                            </div>

                                            <div class="col-12">
                                                <asp:Panel ID="pnlLV" runat="server">
                                                    <asp:ListView ID="lvStudent" runat="server">
                                                        <LayoutTemplate>
                                                            <div>
                                                                <div class="sub-heading">
                                                                    <h5>Student List</h5>
                                                                </div>
                                                                <asp:Panel ID="Panel2" runat="server">
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divcolgglist">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Sr. No.</th>
                                                                                <th>Name
                                                                                </th>
                                                                                <th>Adm. Status
                                                                                </th>
                                                                                <th style="display: none">IdNo
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>Father Name
                                                                                </th>
                                                                                <th>Mother Name
                                                                                </th>
                                                                                <th>Mobile No.
                                                                                </th>
                                                                                <th>
                                                                                    Final Submit Status
                                                                                </th>
                                                                                <th>
                                                                                    Lock/Unlock Status
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
                                                                <td><%# Container.DataItemIndex + 1%></td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                        OnClick="lnkId_Click"></asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblAdmcan" Font-Bold="true" runat="server" ForeColor='<%# Eval("ADMCANCEL").ToString().Equals("ADMITTED")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' Text='<%# Eval("ADMCANCEL")%>'></asp:Label>
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
                                                                <td>
                                                                    <asp:Label ID="lblFinalSubmitStatus" style="text-align:center; vertical-align:middle;" Font-Bold="true" runat="server" ForeColor="Green" Text='<%# Eval("FINAL_SUBMIT_STATUS")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblLockUnlockStatus" style="text-align:center; vertical-align:middle;" Font-Bold="true" runat="server" ForeColor='Green' Text='<%# Eval("LOCK/UNLOCK_STATUS")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <%--Search Pannel End--%>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <%--Search Box Script Start--%>
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
    <%--Search Box Script End--%>
       <script type="text/javascript">
           $(function () {
               $(':text').bind('keydown', function (e) {
                   //on keydown for all textboxes prevent from postback
                   if (e.target.className != "searchtextbox") {
                       if (e.keyCode == 13) { //if this is enter key
                           document.getElementById('<%=Button1.ClientID%>').click();
                        e.preventDefault();
                        return true;
                    }
                    else
                        return true;
                }
                else
                    return true;
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $(':text').bind('keydown', function (e) {
                    //on keydown for all textboxes
                    if (e.target.className != "searchtextbox") {
                        if (e.keyCode == 13) { //if this is enter key
                            document.getElementById('<%=Button1.ClientID%>').click();
                            e.preventDefault();
                            return true;
                        }
                        else
                            return true;
                    }
                    else
                        return true;
                });
            });

        });
    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
