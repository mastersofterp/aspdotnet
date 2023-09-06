<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Submit_Presynopsis.aspx.cs" Inherits="ACADEMIC_PHD_Submit_Presynopsis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="myModal2" role="dialog" runat="server">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div>
                    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updEdit"
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

            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:Panel ID="pnDisplay" runat="server">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>
                        </h3>
                        <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                    </div>
                    <div class="box-body">
                        <asp:UpdatePanel ID="updEdit" runat="server">
                            <ContentTemplate>
                                <div class="col-12">
                                    <div class="row">

                                       <div class="form-group col-lg-3 col-md-6 col-12" id="divCriteria" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Search Criteria</label>
                                            </div>
                                            <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>

                                           <div class="form-group col-lg-3 col-md-6 col-12" id="divAdmBatch" runat="server" visible="false">
                                            <span style="color: red;">*</span><label>Admission Batch</label>
                                            <asp:DropDownList ID="ddlAdmBatch" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True" ToolTip="Please Select Admission Batch" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                                Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="submit" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">

                                            <asp:Panel ID="pnltextbox" runat="server">
                                                <div id="divtxt" runat="server" style="display: block">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Search String</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </asp:Panel>

                                            <asp:Panel ID="pnlDropdown" runat="server">
                                                <div id="divDropDown" runat="server" style="display: block">
                                                    <div class="label-dynamic">
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
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClientClick="return submitPopup(this.name);" OnClick="btnSearch_Click" />

                                        <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" OnClick="btnClose_Click" />
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
                        <div id="divmain" runat="server" visible="false">
                            <div class="accordion" id="accordionExample">
                                <div class="card" runat="server" id="DivSutLog">
                                    <div class="card-header" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true">
                                        <span class="title">General Information</span>
                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                    </div>
                                    <div id="collapseOne" class="collapse show">
                                        <div class="card-body">
                                            <div class="col-12" id="DivGenInfo" runat="server" visible="true">
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>ID No. :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblidno" runat="server" Font-Bold="True"></asp:Label>
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Enrollment No. :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblenrollmentnos" runat="server" Font-Bold="True"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Date of Joining :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lbljoiningdate" runat="server" Font-Bold="True"></asp:Label>
                                                                    <asp:HiddenField ID="hfdegreenos" runat="server" />
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Status :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblstatussup" runat="server" Font-Bold="True"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Student Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblnames" runat="server" Font-Bold="True"></asp:Label>
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Father Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblfathername" runat="server" Font-Bold="True"></asp:Label>
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Department :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblDepartment" runat="server" Font-Bold="true"></asp:Label>
                                                                    <asp:HiddenField ID="hfDepartment" runat="server" />
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Admission Batch :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lbladmbatch" runat="server" Font-Bold="True"></asp:Label>
                                                                    <asp:HiddenField ID="hfadmbatch" runat="server" />
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>

                                                <div class="row mt-3" id="DivDrops" runat="server" visible="false">
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Pre-Synopsis Title</label>
                                                        </div>
                                                        <asp:TextBox ID="txtTitle" runat="server" CssClass="unwatermarked" Rows="1" class="form-control" TextMode="MultiLine" TabIndex="1"> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTitle"
                                                            Display="None" ErrorMessage="Please Enter Pre-Synopsis Title" SetFocusOnError="True"
                                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DivUpload">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Pre-Synopsis Upload</label>
                                                        </div>
                                                        <asp:FileUpload ID="fuDoc" runat="server" Width="220px" />
                                                         <asp:Label ID="lblFileName" runat="server" Visible="false"></asp:Label>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DivDownload">
                                                        <div class="label-dynamic">
                                                          <%--  <sup>* </sup>--%>
                                                            <label>Pre-Synopsis Download</label>                                                  
                                                        </div>
                                                        <asp:Button ID="btnDownload" runat="server" Text="Download Pre-Synopsis" CssClass="btn btn-primary" OnClick="btnDownload_Click" TabIndex="2" />
                                                    </div>                                                
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>                          
                            <div class="col-12 btn-footer">

                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary"  TabIndex="3" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="Academic" />
                                <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-primary"  TabIndex="4" Text="Approve" OnClick="btnApprove_Click" ValidationGroup="Academic" Visible="false" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning"   TabIndex="5" Text="Cancel" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Academic"
                                    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                            </div>
                               <div class="col-12">
                                <asp:Panel ID="pnlApprove" runat="server">
                                    <asp:ListView ID="lvApprove" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <div class="sub-heading">
                                                    <h5>Alloted Supervisor List</h5>
                                                </div>
                                                <asp:Panel ID="Panel2" runat="server">
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Sr.No.
                                                                </th>                                                                
                                                                <th>Supervisior
                                                                </th>
                                                                <th>Joint Supervisior One
                                                                </th>
                                                                <th>Institute Faculty
                                                                </th>
                                                                <th>Joint Supervisior Two
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
                                                </asp:Panel>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Container.DataItemIndex + 1%></td>
                                                <td> <asp:Label ID="lblSupApprove" runat="server" Text='<%#Eval("APPROVAL_SUP") %>' ToolTip='<%# Eval("IDNO")%>'></asp:Label></td>
                                                <td><%#Eval("APPROVAL_J1") %></td>
                                                <td><%#Eval("APPROVAL_INS") %></td>
                                                <td><%#Eval("APPROVAL_J2") %></td>
                                                <td><%#Eval("APPROVAL_DRC") %></td>
                                                <td><%#Eval("APPROVAL_DRC_Chairman") %></td>
                                                <td><%#Eval("APPROVAL_DEAN") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
    </asp:Panel>

    <%--<script type="text/javascript">
        jQuery(function ($) {
            $(document).ready(function () {
                bindDataTable();
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
            });
            function bindDataTable() {
                var myDT = $('#id1').DataTable({
                });
            }

        });
    </script>--%>
  

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

