<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="AllotSupervisor.aspx.cs" Inherits="Academic_StudentInfoEntry" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <<%--script src="../includes/prototype.js" type="text/javascript"></script>

    <script src="../includes/scriptaculous.js" type="text/javascript"></script>

    <script src="../includes/modalbox.js" type="text/javascript"></script>--%>

    <%--FOLLOWING SCRIPT USED FOR THE ONLY DATE--%>
    <%--<script src="../JAVASCRIPTS/jquery-1.5.1.js" type="text/javascript"></script>

    <script src="../JAVASCRIPTS/jquery.ui.core.js" type="text/javascript"></script>

    <script src="../JAVASCRIPTS/jquery.ui.widget.js" type="text/javascript"></script>

    <script src="../JAVASCRIPTS/jquery.ui.datepicker.js" type="text/javascript"></script>--%>

    <%--BEGIN : FOLLOWING CODE ALLOWS THE AUTOCOMPLETE TO BE FIRED IN UPDATEPANEL--%>
    <%-- <script  type="text/javascript">
    function RunThisAfterEachAsyncPostback()
    {
   

        Autocomplete();
    }
    </script>
    <script  type="text/javascript">
       RunThisAfterEachAsyncPostback();
       Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
    <%--END : FOLLOWING CODE ALLOWS THE AUTOCOMPLETE TO BE FIRED IN UPDATEPANEL--%>


    <asp:Panel ID="pnDisplay" runat="server">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">General Student Information</h3>
                    </div>

                    <div class="box-body">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>City List</h5>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-12" id="divGeneralInfo" style="display: block;">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <asp:Panel ID="pnlId" runat="server" Visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>ID No. </label>
                                        </div>
                                        <asp:TextBox ID="txtIDNo" runat="server" CssClass="form-control" TabIndex="1" Enabled="False" />
                                        <%--  Enable the button so it can be played again --%>
                                        <a href="#" title="Search Student for Modification" onclick="Modalbox.show($('divdemo2'), {title: this.title, width: 600,overlayClose:false});return false;">
                                            <asp:Image ID="imgSearch" runat="server" ImageUrl="~/Images/search.png" TabIndex="1"
                                                AlternateText="Search Student by IDNo, Name, Reg. No, Branch, Semester" ToolTip="Search Student by IDNo, Name, Reg. No, Branch, Semester" />
                                        </a>
                                    </asp:Panel>
                                </div>
                            </div>

                            <div class="row mt-3 mb-3">
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>ID. No. :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblRegNo" runat="server" Font-Bold="true"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Enrollment No. :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="true"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Student Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblStudName" runat="server" Font-Bold="true"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Father&#39;s Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblFatherName" runat="server" Font-Bold="true"></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Date of Joining :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblDateOfJoining" runat="server" Font-Bold="true"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Department :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblBranch" runat="server" Font-Bold="true"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Status :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Supervisor Name  </label>
                                    </div>
                                    <asp:DropDownList ID="ddlSupervisor" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSupervisor_SelectedIndexChanged"
                                        CssClass="form-control" data-select2-enable="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSupervisor" runat="server"
                                        ControlToValidate="ddlSupervisor" Display="None"
                                        ErrorMessage="Please Select Supervisor" InitialValue="0" SetFocusOnError="true"
                                        ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="rfvSupervisorType" runat="server"
                                        ControlToValidate="ddlSupervisorType" Display="None"
                                        ErrorMessage="Please Select Supervisor Type" InitialValue="0" SetFocusOnError="true"
                                        ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Supervisor Type  </label>
                                    </div>
                                    <asp:DropDownList ID="ddlSupervisorType" runat="server"
                                        AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">Internal</asp:ListItem>
                                        <asp:ListItem Value="2">External</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Co-Supervisor1 Name  </label>
                                    </div>
                                    <asp:DropDownList ID="ddlCoSupevisor1" runat="server"
                                        AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlCoSupevisor1_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Co-Supervisor1 Type  </label>
                                    </div>
                                    <asp:DropDownList ID="ddlCoSupervisorType1" runat="server"
                                        AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">Internal</asp:ListItem>
                                        <asp:ListItem Value="2">External</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Co-Supervisor2 Name  </label>
                                    </div>
                                    <asp:DropDownList ID="ddlCoSupevisor2" runat="server"
                                        AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlCoSupevisor2_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Co-Supervisor2 Type  </label>
                                    </div>
                                    <asp:DropDownList ID="ddlCoSupervisorType2" runat="server"
                                        AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">Internal</asp:ListItem>
                                        <asp:ListItem Value="2">External</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                    ValidationGroup="Academic" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnReport" runat="server" Text="Annexure-C Report" Visible="false"
                                    OnClick="btnReport_Click1" CssClass="btn btn-info" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" />

                                <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Academic"
                                    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                            </div>
                        </div>

                        <div id="divdemo2" style="display: none; height: 550px">
                            <asp:UpdatePanel ID="updEdit" runat="server">
                                <ContentTemplate>
                                    <table cellpadding="1" cellspacing="1" width="100%">
                                        <tr>
                                            <td>Search Criteria:
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="rbName" runat="server" Text="Name" GroupName="edit" />
                                                <asp:RadioButton ID="rbIdNo" runat="server" Text="IdNo" GroupName="edit" />
                                                <asp:RadioButton ID="rbBranch" runat="server" Text="Branch" GroupName="edit" />
                                                <asp:RadioButton ID="rbSemester" runat="server" Text="Semester" GroupName="edit" />
                                                <asp:RadioButton ID="rbEnrollmentNo" runat="server" Text="Enrollmentno" GroupName="edit"
                                                    Checked="True" />
                                                <asp:RadioButton ID="rbRegNo" runat="server" Text="Rollno" GroupName="edit"
                                                    Checked="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Search String :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="return submitPopup(this.name);" />
                                                <asp:Button ID="btnCancelModal" runat="server" Text="Cancel" OnClientClick="return ClearSearchBox(this.name)" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="updEdit">
                                                    <ProgressTemplate>
                                                        <asp:Image ID="imgProg" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                                                        Loading.. Please Wait!
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="2" width="100%">
                                                <asp:ListView ID="lvStudent" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="vista-grid">
                                                            <div class="titlebar">
                                                                Login Details
                                                            </div>
                                                            <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                <thead>
                                                                    <tr class="header">
                                                                        <th style="width: 20%">Name
                                                                        </th>
                                                                        <th style="width: 15">IdNo
                                                                        </th>
                                                                        <th style="width: 20%">Roll No.
                                                                        </th>
                                                                        <th style="width: 30%">Branch
                                                                        </th>
                                                                        <th style="width: 15%">Semester
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                            </table>
                                                        </div>
                                                        <div class="listview-container">
                                                            <div id="demo-grid" class="vista-grid">
                                                                <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="item">
                                                            <td style="width: 20%">
                                                                <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                    OnClick="lnkId_Click"></asp:LinkButton>
                                                            </td>
                                                            <td style="width: 15%">
                                                                <%# Eval("idno")%>
                                                            </td>
                                                            <td style="width: 20%">
                                                                <%# Eval("EnrollmentNo")%>
                                                            </td>
                                                            <td style="width: 30%">
                                                                <%# Eval("longname")%>
                                                            </td>
                                                            <td style="width: 15%">
                                                                <%# Eval("semesterno")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <script type="text/javascript" language="javascript">
        //function toggleExpansion(imageCtl, divId) {
        //    if (document.getElementById(divId).style.display == "block") {
        //        document.getElementById(divId).style.display = "none";
        //        imageCtl.src = "../images/expand_blue.jpg";
        //    }
        //    else if (document.getElementById(divId).style.display == "none") {
        //        document.getElementById(divId).style.display = "block";
        //        imageCtl.src = "../images/collapse_blue.jpg";
        //    }
        //}




        function submitPopup(btnsearch) {
            var rbText;
            var searchtxt;
            if (document.getElementById('<%=rbName.ClientID %>').checked == true)
                rbText = "name";
            else if (document.getElementById('<%=rbIdNo.ClientID %>').checked == true)
                rbText = "idno";
            else if (document.getElementById('<%=rbBranch.ClientID %>').checked == true)
                rbText = "branch";
            else if (document.getElementById('<%=rbSemester.ClientID %>').checked == true)
                rbText = "sem";
            else if (document.getElementById('<%=rbEnrollmentNo.ClientID %>').checked == true)
                rbText = "enrollmentno";
            else if (document.getElementById('<%=rbRegNo.ClientID %>').checked == true)
                rbText = "regno";

    searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;

            __doPostBack(btnsearch, rbText + ',' + searchtxt);

            return true;
        }

        function ClearSearchBox(btncancelmodal) {
            document.getElementById('<%=txtSearch.ClientID %>').value = '';
            __doPostBack(btncancelmodal, '');
            return true;
        }



        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.focus();
                alert('Only Alphabets Allowed');
                return false;
            }
            else
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
        function validateAlphaNumeric(txt) {
            var expAN = /[$\\@\\\#%\^\&\*\(\)\[\]\+\_popup\{\}|`\~\=\|]/;
            var strPass = txt.value;
            var strLength = strPass.length;
            var lchar = txt.value.charAt((strLength) - 1);

            if (lchar.search(expAN) != -1) {
                txt.value(txt.value.substring(0, (strLength) - 1));
                txt.focus();
                alert('Only Alpha-Numeric Characters Allowed!');
            }
            return true;
        }
        function LoadImage() {
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").src = document.getElementById("ctl00_ContentPlaceHolder1_fuPhotoUpload").value;
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").height = '96px';
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").width = '96px';
        }

        //Round to two digits
        fixedTo = function (number, n) {
            var k = Math.pow(10, n);
            return (Math.round(number * k) / k);
        }



    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
