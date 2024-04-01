<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="create_user.aspx.cs" Inherits="create_user" ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <%--<script type="text/javascript" charset="utf-8">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();

        }
        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display,#tbluser").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });
         
            });

        }

    </script>--%>

    <style>
        .select-all-checkbox-container {
            display: flex;
            align-items: center;
            margin-left: 5px; /* Adjust the value as needed */
            margin-top: 2px;
            margin-bottom: 2px;
        }

            .select-all-checkbox-container .select-all-label {
                margin-left: 5px;
                margin-bottom: 2px;
                margin-top: 2px;
                /* Adjust the value as needed */
            }
    </style>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlUser"
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

    <asp:UpdatePanel ID="updpnlUser" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">USER MANAGEMENT</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlOrganization" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlOrg" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlOrg_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlAccessDomain" runat="server" ControlToValidate="ddlOrg"
                                            Display="None" ErrorMessage="Organization Required !" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Username(Login id)</label>
                                        </div>
                                        <span class="form-inline">
                                            <asp:TextBox ID="txtUsername" Width="40%" runat="server" TabIndex="1" MaxLength="20" ValidationGroup="SubmitGroup" CssClass="form-control" />

                                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUsername"
                                                ErrorMessage="Username Required !" Display="None" ValidationGroup="submit">
                                            </asp:RequiredFieldValidator>

                                            <asp:Button ID="btnCheckID" runat="server" Text="Check Availability" CssClass="btn btn-primary m-top" ValidationGroup="CheckID"
                                                OnClick="btnCheckID_Click" Visible="true" />
                                        </span>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>User Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlUserType" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" OnClientClick="showProgress()"
                                            TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="req_usertype" runat="server" ControlToValidate="ddlUserType"
                                            ErrorMessage="User Type Required" Display="None" InitialValue="0" ValidationGroup="submit" />
                                        <%--<a href="#" title="Search User for Modification" onclick="Modalbox.show($('divdemo2'), {title: this.title, width: 600,overlayClose:false});return false;">
                                                <asp:Image ID="imgSearch" runat="server" ImageUrl="~/images/search.png" TabIndex="1"
                                                    AlternateText="Search User by IDNo, Name, UserName, User Type" ToolTip="Search User by IDNo, Name, UserName, User Type" />
                                            </a>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlUserType"
                                            Display="None" ErrorMessage="Please Select User Type" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="SubmitGroup"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trSubDept" visible="false" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Department (Pay)</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubDept" runat="server" AppendDataBoundItems="True" TabIndex="3" OnSelectedIndexChanged="ddlSubDept_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvpay" runat="server" ControlToValidate="ddlSubDept"
                                                ErrorMessage="Department Required !" InitialValue="0" Display="None" ValidationGroup="submit">
                                            </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trDept" visible="false" runat="server">
                                        <div class="label-dynamic">
                                            <%-- <sup>* </sup>--%>
                                            <label>Department (Acd)</label>
                                        </div>
                                        <asp:ListBox ID="ddlDept" runat="server" SelectionMode="Multiple" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"
                                            CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>
                                        <%--data-select2-enable="true"--%>
                                        <%-- <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="True" AutoPostBack="true" TabIndex="4" OnClientClick="showProgress()" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>--%>
                                        <%--<asp:RequiredFieldValidator ID="rfvddlDept" runat="server" ControlToValidate="ddlDept"
                                                ErrorMessage="Department Required !" InitialValue="0" Display="None" ValidationGroup="submit">
                                            </asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Full Name</label>
                                        </div>
                                        <asp:TextBox ID="txtFName" runat="server" CssClass="form-control" TabIndex="5" MaxLength="50" Style="text-transform: uppercase" />
                                        <asp:RequiredFieldValidator ID="rfvFullName" runat="server" ControlToValidate="txtFName"
                                            ErrorMessage="Full Name Required !" Display="None" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Qtr / Room No</label>
                                        </div>
                                        <asp:TextBox ID="txtQtrRoomNo" runat="server" TabIndex="6" MaxLength="20" CssClass="form-control" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Designation</label>
                                        </div>
                                        <asp:TextBox ID="txtDesignatio" runat="server" CssClass="form-control" TabIndex="7" MaxLength="50" Style="text-transform: uppercase" />
                                        <asp:TextBox ID="txtdesig" runat="server" CssClass="form-control" Style="display: none" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Password</label>
                                        </div>
                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" TabIndex="8"
                                            CssClass=" form-control" MaxLength="20" ValidationGroup="SubmitGroup" autocomplete="off" />
                                        <asp:Label ID="lblPassStatus" runat="server" SkinID="Msglbl" />
                                        <%--<asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                                                ErrorMessage="Password Required" Display="None" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                        <asp:HiddenField ID="hdfPassword" runat="server" />
                                        <ajaxToolKit:PasswordStrength ID="PS" runat="server"
                                            TargetControlID="txtPassword"
                                            DisplayPosition="RightSide"
                                            StrengthIndicatorType="Text"
                                            PreferredPasswordLength="10"
                                            PrefixText="Strength:"
                                            TextCssClass="TextIndicator_TextBox1"
                                            MinimumNumericCharacters="0"
                                            MinimumSymbolCharacters="0"
                                            RequiresUpperAndLowerCaseCharacters="false"
                                            TextStrengthDescriptions="Very Poor;Weak;Average;Strong;Excellent"
                                            TextStrengthDescriptionStyles="css_poor;css_weak;css_average;css_Strong;css_Excellent"
                                            CalculationWeightings="50;15;15;20" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Email</label>
                                        </div>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="100" TabIndex="9" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please Enter Valid Email Id"
                                            ControlToValidate="txtEmail" ValidationExpression="^(?!.*\.{3,})\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                                            ValidationGroup="submit"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail"
                                            ErrorMessage="Email Id Required !" Display="None" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Mobile No.</label>
                                        </div>
                                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="10" TabIndex="10" onkeypress="return event.charCode >= 48 && event.charCode <= 57" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please Enter Valid Mobile No."
                                            ControlToValidate="txtMobile" ValidationExpression="^[0-9]{10}"
                                            ValidationGroup="submit"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMobile"
                                            ErrorMessage="Mobile No. Required !" Display="None" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                      <div id="divmessage" runat="server">
                                <div class=" note-div">
                                    <h5 class="heading">Note</h5>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><b><span>New User can not be Created for Student User Type. Only Details can be Edited.</span></b></p>
                                </div>
                                </div>
                                    <div class="col-12" id="pnlStudent" runat="server" visible="false">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>College</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" TabIndex="11" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Degree</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" TabIndex="12" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Branch</label>
                                                </div>
                                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" TabIndex="13" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Semester</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="14" OnClientClick="showProgress()" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Status</label>
                                        </div>
                                        <asp:UpdatePanel ID="Updpnldetails" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:CheckBox ID="chkActive" runat="server" Text="Active" Font-Bold="True" TabIndex="15"
                                                    Checked="True" onclick="return show(chkActive);" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                               
                                                            <asp:CheckBox ID="chkDEC" runat="server" Text="D.E.C" TabIndex="16" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="chkActive" />
                                                <asp:PostBackTrigger ControlID="chkDEC" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="ddlNewUser" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>New User Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlNewUserType" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" OnClientClick="showProgress()"
                                            TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>



                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <%-- Added by sachin 07-12-2022--%>
                                        <div class="label-dynamic">
                                            <label>Remark</label>
                                        </div>
                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" MaxLength="400" TabIndex="17" />
                                    </div>
                                </div>
                            </div>

                          
                                <div class="form-group col-lg-12 col-md-12 col-12 h-100">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Institute Name</label>
                                        <div class="checkbox-column select-all-checkbox-container">
                                            <asp:CheckBox ID="chkAll" onclick="checkAll(this)" runat="server" CssClass="select-all-checkbox" />
                                            <span class="select-all-label"><b>Select All</b></span>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-12 checkbox-list-box">
                                        <div class="checkbox-row">
                                            <div class="checkbox-column">
                                                <asp:CheckBoxList ID="chkCollegeName" runat="server" RepeatColumns="4" AppendDataBoundItems="true" RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style" />
                                            </div>
                                        </div>
                                    </div>
                                </div>



                                <div class="col-md-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit" OnClientClick="showProgress()"
                                        OnClick="btnSubmit_Click" CssClass="btn btn-primary" />

                                    <asp:Button ID="btnExporttoExcel" runat="server" Text="Export to Excel" ValidationGroup="SubmitGroup" Enabled="true" AutoPostBack="false" OnClick="btnExporttoExcel_Click"
                                        CausesValidation="False" CssClass="btn btn-info" />


                                    <%--<asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" OnClientClick="showProgress()" ValidationGroup="checkdept" CssClass="btn btn-info" />--%>
                                    <asp:Button ID="btnReset" runat="server" Text="Clear" ValidationGroup="SubmitGroup"
                                        OnClick="btnReset_Click" CausesValidation="False" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" DisplayMode="List" ValidationGroup="submit" />
                                </div>

                                <div class="col-12">
                                    <asp:Label ID="lblStatus" runat="server" Text="" SkinID="Errorlbl" />
                                    <asp:Label ID="lblSubmitStatus" runat="server" SkinID="lblmsg" />
                                    <div id="divMsg" runat="server">
                                    </div>
                                    <asp:Label ID="lblEmpty" Font-Bold="true" Text="No Record!" Visible="false" runat="server"></asp:Label>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                    <asp:TextBox ID="txtUserID" runat="server" Visible="false" MaxLength="10" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="req_userid" runat="server" ErrorMessage="User name Required"
                                        ControlToValidate="txtUsername" Display="None" ValidationGroup="CheckID" />
                                    <asp:ValidationSummary ID="val_summary" runat="server" ShowMessageBox="True" ShowSummary="False"
                                        DisplayMode="List" ValidationGroup="CheckID" />
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDept"
                                    ErrorMessage="Department Required !" InitialValue="0" Display="None" ValidationGroup="checkdept">
                                </asp:RequiredFieldValidator>--%>
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" ShowSummary="False"
                                        DisplayMode="List" ValidationGroup="checkdept" />
                                </div>

                                <div class="col-12">
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tbluser">
                                        <asp:Repeater ID="lvlinks" runat="server">
                                            <HeaderTemplate>
                                                <div class="sub-heading">
                                                    <h5>Users Account Info List</h5>
                                                </div>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center;">Action
                                                        </th>
                                                        <th>Name
                                                        </th>
                                                        <th>User Name
                                                        </th>
                                                         <%-- added by rutuja 20-02-2024 (add the columns for  Parent type )--%>
                                                         <div id="pdiv" runat="server" class="pdiv" visible='<%# ddlUserType.SelectedValue == "14" %>'>
                                                        <!-- Your content here -->
                                                        <th>Student Name</th>
                                                        <th>Student Registration No</th>
                                                    </div>
                                                        <%--<th style="width: 20%; text-align:left">
                                                                Password
                                                            </th>--%>
                                                        <th>Type
                                                        </th>
                                                        <th>Status
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <%-- <tr id="itemPlaceholder" runat="server" />--%>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <tr style="text-align: left;">
                                                            <td>
                                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit1.gif" CommandArgument='<%# Eval("UA_NO") %>'
                                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="6" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblUser" runat="server" Text='<%# Eval("UA_FULLNAME")%>' ToolTip='<%# Eval("UA_NO")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <%# Eval("UA_NAME")%>
                                                            </td>
                                                         <%-- added by rutuja 20-02-2024 (add the columns for  Parent type )--%>
                                                        <td id="tdPSname" runat="server" visible='<%#Eval("UA_TYPE").ToString()=="14"?true:false%>'>
                                                            <asp:Label ID="lblPname" runat="server" Text='<%# Eval("STUDNAME")%>' ToolTip='<%# Eval("UA_NO")%>'></asp:Label>
                                                        </td>
                                                        <td id="tdPSno" runat="server" visible='<%#Eval("UA_TYPE").ToString()=="14"?true:false%>'>
                                                            <asp:Label ID="lblPno" runat="server" Text='<%# Eval("REGNO")%>'></asp:Label>
                                                        </td>
                                                    
                                                            <%--<td style="width: 20%; text-align:left">
                                                            <asp:Label ID="lblUserpass" runat="server" Text='<%# Eval("UA_PWD")%>' Visible ="false"></asp:Label>
                                                        </td>--%>
                                                            <td>
                                                                <%# Eval("USERDESC")%>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblUStatus" Style="font-size: 9pt;" runat="server" Text='<%# Eval("UA_STATUS")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <%--   <asp:PostBackTrigger ControlID="btnEdit" />--%>
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody> 
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>
        </ContentTemplate>
        <Triggers>
            <%--    <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="ddlDept" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="ddlSemester" />
            <asp:PostBackTrigger ControlID="ddlUserType" />--%>

            <asp:PostBackTrigger ControlID="btnExporttoExcel" />


        </Triggers>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">

        function lnkIDClick(lnkID) {
            __doPostBack(lnkID.id, lnkID.title);
            __doPostBack(lnkID.id, lnkID.title);
            __doPostBack(lnkID.id, lnkID.title);
            alert('User ID : ' + lnkID.title);
            return true;
        }
    </script>
    <script language="javascript" type="text/javascript">
        //utility function to get the container of an element by tagname
        function GetParentByTagName(parentTagName, childElementObj) {
            var parent = childElementObj.parentNode;
            while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
                parent = parent.parentNode;
            }
            return parent;
        }
    </script>

    <%--<script>
        $(function () {
            //alert("1")
            //$.noConflict();
            $("#tbluser").DataTable();
            //$('#tbluser').DataTable({
            //    "paging": true,
            //    "lengthChange": false,
            //    "searching": false,
            //    "ordering": true,
            //    "info": true,
            //    "autoWidth": false
            //});
        });
    </script>--%>
    <script type="text/javascript">
        function showProgress() {
            var updateProgress = $get("<%= updpnlUser.ClientID %>");
            updateProgress.style.display = "block";
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>

    <script>
        $(document).ready(function () {
            // Check if all checkboxes are checked
            function updateSelectAll() {
                var allChecked = $('.checkbox-list-style input[type="checkbox"]').not('#chkAll').length === $('.checkbox-list-style input[type="checkbox"]:checked').not('#chkAll').length;
                $('#chkAll').prop('checked', allChecked);
            }

            // Handle click event for "Select All" checkbox
            $('#chkAll').on('click', function () {
                $('.checkbox-list-style input[type="checkbox"]').not(this).prop('checked', this.checked);
            });

            // Handle click event for other checkboxes
            $('.checkbox-list-style input[type="checkbox"]').not('#chkAll').on('click', function () {
                updateSelectAll();
            });

            // Initialize the state of "Select All" checkbox
            updateSelectAll();
        });
    </script>

    <script>
        function checkAll(source) {
            var checkboxes = document.querySelectorAll('tbody input[type="checkbox"]');
            var allChecked = true;

            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = source.checked;

                if (!checkboxes[i].checked) {
                    allChecked = false;
                }
            }

            var selectAllCheckbox = document.getElementById('chkAll');
            selectAllCheckbox.checked = allChecked || (source.checked && allCheckboxesManuallyChecked());
        }

        function allCheckboxesManuallyChecked() {
            var checkboxes = document.querySelectorAll('tbody input[type="checkbox"]');
            var manuallyChecked = true;

            for (var i = 0; i < checkboxes.length; i++) {
                if (!checkboxes[i].checked && !checkboxes[i].getAttribute('disabled')) {
                    manuallyChecked = false;
                    break;
                }
            }

            return manuallyChecked;
        }
    </script>

</asp:Content>

