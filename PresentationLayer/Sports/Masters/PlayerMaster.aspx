<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PlayerMaster.aspx.cs" Inherits="Sports_Masters_PlayerMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        ; debugger
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 9999999999;
        }


        function ValidateTextBox() {
            if (document.getElementById("txtStudName").value == "") {
                alert("Please enter Name!");
                return false;
            }

        }

    </script>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="divSearch" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PLAYER MASTER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="pnlDesig" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Search Student</label>
                                            </div>
                                            <%--   //Modified by Saahil Trivedi 13/1/2022--%>
                                            <asp:TextBox ID="txtStudName" runat="server" CssClass="form-control" ToolTip="Search Student" TabIndex="1" placeholder="Enter Student Name" onkeypress="return CheckAlphabet(event, this);"></asp:TextBox>
                                            <asp:HiddenField ID="hfStudNo" runat="server" />
                                            <asp:RequiredFieldValidator ID="rfvEventTypeName" runat="server"
                                                SetFocusOnError="true" Display="None" ErrorMessage="Please Enter Student Name" ValidationGroup="Search" ControlToValidate="txtStudName"></asp:RequiredFieldValidator>
                                            <cc1:AutoCompleteExtender ServiceMethod="GetStudName"
                                                MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtStudName"
                                                ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false" OnClientItemSelected="StudName" OnClientShowing="clientShowing">
                                            </cc1:AutoCompleteExtender>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <br />
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CausesValidation="true" OnClick="btnSearch_Click" TabIndex="2" CssClass="btn btn-primary" ToolTip="Click to Search" ValidationGroup="Search" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Search" />

                                        </div>
                                    </div>

                                </asp:Panel>
                            </div>

                            <div class="panel panel-info">
                                <%--<div class="panel panel-heading">Student Details</div>--%>
                                 <div class="sub-heading">
                                                    <h5>Student Details</h5>
                                                </div>
                                <div class="panel panel-body">
                                    <div class="form-group col-md-4">
                                        <label><span style="color: #FF0000"></span></label>
                                        <asp:RadioButtonList ID="rdbPlayerType" runat="server" RepeatDirection="Horizontal" TabIndex="3" ToolTip="Select Player Type" AutoPostBack="true" OnSelectedIndexChanged="rdbPlayerType_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="U">University Player</asp:ListItem>
                                            <asp:ListItem Value="O">Other Player</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trCollegeNo" runat="server">
                                                <label><span style="color: #FF0000">*</span>Institute :</label>
                                                <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" ValidationGroup="Summary" CssClass="form-control" ToolTip="Select Institute" TabIndex="4">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                    Display="None" ErrorMessage="Please Select Institute" InitialValue="0" ValidationGroup="Submit" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trCollegeName" runat="server" visible="false">
                                                <label><span style="color: #FF0000">*</span>Institute :</label>
                                                <asp:TextBox ID="txtCollegeName" runat="server" MaxLength="60" CssClass="form-control" ToolTip="Enter Institute" onkeypress="return CheckAlphabet(event, this);" TabIndex="5"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvCollegeName" ValidationGroup="Submit" ControlToValidate="txtCollegeName"
                                                    Display="None" ErrorMessage="Please Enter Institute." SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>
                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trDegree" runat="server">
                                                <label><span style="color: #FF0000">*</span>Degree :</label>
                                                <asp:DropDownList ID="ddlDegree" CssClass="form-control" ToolTip="Select Degree" runat="server" AppendDataBoundItems="true" TabIndex="6">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ErrorMessage="Please Select Degree." ControlToValidate="ddlDegree" InitialValue="0" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trDegreeOther" runat="server" visible="false">
                                                <label><span style="color: #FF0000">*</span>Degree :</label>
                                                <asp:TextBox ID="txtDegree" runat="server" MaxLength="150" CssClass="form-control" ToolTip="Enter Degree" onkeypress="return CheckAlphabet(event, this);" TabIndex="7"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDegreeOther" ValidationGroup="Submit" ControlToValidate="txtDegree"
                                                    Display="None" ErrorMessage="Please Enter Degree Name..!!" SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trBranch" runat="server">
                                                <label><span style="color: #FF0000">*</span>Branch :</label>
                                                <asp:DropDownList ID="ddlBranch" CssClass="form-control" ToolTip="Select Branch" runat="server" AppendDataBoundItems="true" TabIndex="8">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ErrorMessage="Please Select Branch." ControlToValidate="ddlBranch" InitialValue="0" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trBranchOther" runat="server" visible="false">
                                                <label><span style="color: #FF0000">*</span>Branch :</label>
                                                <asp:TextBox ID="txtBranch" runat="server" MaxLength="150" CssClass="form-control" ToolTip="Enter Branch" onkeypress="return CheckAlphabet(event, this);" TabIndex="9"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvBranchOther" ValidationGroup="Submit" ControlToValidate="txtBranch"
                                                    Display="None" ErrorMessage="Please Enter Branch Name..!!" SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trScheme" runat="server">
                                                <label><span style="color: #FF0000">*</span>Scheme :</label>
                                                <asp:DropDownList ID="ddlScheme" CssClass="form-control" ToolTip="Select Scheme" runat="server" AppendDataBoundItems="true" TabIndex="10">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ErrorMessage="Please Select Scheme." ControlToValidate="ddlScheme" InitialValue="0" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trSchemeOther" runat="server" visible="false">
                                                <label><span style="color: #FF0000">*</span>Scheme :</label>
                                                <asp:TextBox ID="txtScheme" runat="server" MaxLength="150" CssClass="form-control" ToolTip="Enter Scheme" onkeypress="return CheckAlphabet(event, this);" TabIndex="11"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvSchemeOther" ValidationGroup="Submit" ControlToValidate="txtScheme" Display="None" ErrorMessage="Please Enter Scheme Name..!!" SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <label><span style="color: #FF0000">*</span>Semester :</label>
                                                <asp:DropDownList ID="ddlSem" CssClass="form-control" ToolTip="Select Semester" runat="server" AppendDataBoundItems="true" TabIndex="12"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSem" runat="server" ErrorMessage="Please Select Semester." ControlToValidate="ddlSem" InitialValue="0" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trPlayerName" runat="server" visible="false">
                                                <label><span style="color: #FF0000">*</span>Player Name :</label>
                                                <asp:TextBox ID="txtOtherPlayerName" runat="server" MaxLength="100" CssClass="form-control" ToolTip="Enter Player Name" TabIndex="13"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvOPlayer" runat="server" ErrorMessage="Please Enter Player Name."
                                                    ControlToValidate="txtOtherPlayerName" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <label><span style="color: #FF0000"></span>Player Reg. No.:</label>
                                                <asp:TextBox ID="txtRegNo" runat="server" MaxLength="60" CssClass="form-control" ToolTip="Enter Player Reg. No." Enabled="False" TabIndex="14"></asp:TextBox>
                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <label><span style="color: #FF0000">*</span>Contact No. :</label>
                                                <asp:TextBox ID="txtContactNo" runat="server" MaxLength="12" CssClass="form-control" ToolTip="Enter Contact No." TabIndex="15"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    FilterType="Custom, Numbers" ValidChars="+- " TargetControlID="txtContactNo">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="rfvContact" runat="server" ErrorMessage="Please Enter Contact No." ControlToValidate="txtContactNo" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <label><span style="color: #FF0000">*</span>Address :</label>
                                                <asp:TextBox ID="txtAddress" runat="server" MaxLength="60" CssClass="form-control" ToolTip="Enter Address" TextMode="MultiLine" TabIndex="16" Height="35px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ErrorMessage="Please Enter Players Address." ControlToValidate="txtAddress" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="panel panel-info" runat="server" visible="false">
                                <div class="panel panel-heading">Attendance Details</div>
                                <div class="panel panel-body">
                                    <div class="form-group col-md-4">
                                        <label><span style="color: #FF0000"></span></label>
                                    </div>
                                </div>
                            </div>


                            <div class="panel panel-info" runat="server" visible="false">
                                <div class="panel panel-heading">Backlog Details</div>
                                <div class="panel panel-body">
                                    <div class="form-group col-md-4">
                                        <label><span style="color: #FF0000"></span></label>
                                    </div>
                                </div>
                            </div>


                            <div class="panel panel-info" runat="server" visible="false">
                                <div class="panel panel-heading">Outstanding Dues</div>
                                <div class="panel panel-body">
                                    <div class="form-group col-md-4">
                                        <label><span style="color: #FF0000"></span></label>
                                    </div>
                                </div>
                            </div>


                            <div class="panel panel-info">
                                <%--<div class="panel panel-heading">Registration Details</div>--%>
                                 <div class="sub-heading">
                                                    <h5>Registration Details</h5>
                                                </div>
                                <div class="panel panel-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <label><span style="color: #FF0000">*</span>Academic Year :</label>
                                                <asp:DropDownList ID="ddlAcadYear" CssClass="form-control" ToolTip="Select Academic Year" runat="server" AppendDataBoundItems="true" TabIndex="17">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvAcadyr" runat="server" ErrorMessage="Please Select Academic Year."
                                                    ControlToValidate="ddlAcadYear" InitialValue="0" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <label><span style="color: #FF0000">*</span>Participation Year :</label>
                                                <asp:TextBox ID="txtPYear" runat="server" MaxLength="4" CssClass="form-control" ToolTip="Enter Participation Year" TabIndex="18"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                    FilterType="Custom, Numbers" ValidChars=" " TargetControlID="txtPYear"> <%--Shaikh juned 29-08-2022  Change on maxlength ValidChars--%>
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="rfvPyear" runat="server" ErrorMessage="Please Enter Participation Year." ControlToValidate="txtPYear" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <label><span style="color: #FF0000">*</span>Sport Type  :</label>
                                                <asp:DropDownList ID="ddlSportType" CssClass="form-control" ToolTip="Select Sport Type" runat="server" AppendDataBoundItems="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSportType_SelectedIndexChanged" TabIndex="19">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSportType" runat="server" ErrorMessage="Please Select Sport Type."
                                                    ControlToValidate="ddlSportType" InitialValue="0" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <label><span style="color: #FF0000">*</span>Sport Name :</label>
                                                <asp:DropDownList ID="ddlSportName" CssClass="form-control" AutoPostBack="true" ToolTip="Enter Sport Name" runat="server" AppendDataBoundItems="true" TabIndex="20"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSportName" runat="server" ErrorMessage="Please Select Sport Name."
                                                    ControlToValidate="ddlSportName" InitialValue="0" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <p class="text-center">
                                            <asp:Button ID="btnAdd" runat="server" CausesValidation="true" CssClass="btn btn-primary" OnClick="btnAdd_Click" TabIndex="21" Text="Add" ToolTip="Click here to Add" ValidationGroup="Submit" Visible="false" />
                                            <asp:ValidationSummary ID="VSAdd" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Add" />
                                    </div>

                                </div>
                            </div>
                             <div class="panel panel-info" id="divSportList" runat="server" visible="false">
                                    <%--<div class="panel panel-heading">Sport List</div>--%>
                                     <div class="sub-heading">
                                                    <h5>Sport List</h5>
                                                </div>
                                    <div class="panel panel-body">
                                        <asp:Panel ID="pnlSportList" runat="server" ScrollBars="Auto">
                                            <asp:ListView ID="lvSprtName" runat="server">
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <h4 class="box-title"></h4>
                                                        <table class="table table-bordered table-hover">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Delete
                                                                    </th>
                                                                    <th>Sport Name
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
                                                        <td style="width: 10%;">
                                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("SRNO") %>'
                                                                ImageUrl="~/Images/delete.png" OnClick="btnDelete_Click" ToolTip="Delete Record" />
                                                        </td>
                                                        <td style="width: 70%;">
                                                            <asp:Label ID="lblSprtName" runat="server" Text='<%# Eval("SNAME") %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            <p class="text-center">
                                &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" TabIndex="22" />
                                &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Cancel" CausesValidation="true" TabIndex="23" />
                                <asp:Button ID="btnShowReport" runat="server" Text="Report" CssClass="btn btn-info" ToolTip="Click to get Report" OnClick="btnShowReport_Click" TabIndex="24" Visible="false" />
                                <%-- Modified by Saahil Trivedi 07-02-2022--%>
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                               
                                <div class="form-group col-md-12" >
                                      <div class="panel panel-info">
                                        <div class="panel panel-heading">
                                            </div>
                                        <div class="panel panel-body">
                                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                                <asp:ListView ID="lvPlayer" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="lgv1">
                                                             <div class="sub-heading">
                                                    <h5>PLAYER ENTRY LIST</h5>
                                                </div>
                                                           <%-- <h4 class="box-title">
                                                            </h4>--%>
                                                            <table class="table table-bordered table-hover">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Sport
                                                                        </th>
                                                                        <th>Player
                                                                        </th>
                                                                        <th>Reg No.
                                                                        </th>
                                                                        <th>Player Address
                                                                        </th>
                                                                        <th>Contact No.
                                                                        </th>
                                                                        <th>Participation Year.
                                                                        </th>
                                                                        <th>Institute
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
                                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                    CommandArgument='<%# Eval("PLAYER_REGNO") %>' AlternateText='<%# Eval("PLAYER_PYEAR") %>' OnClick="btnEdit_Click" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("SNAME")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("PLAYERNAME")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("PLAYER_REGNO")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("PLAYER_ADDRESS")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("PLAYER_CONTACTNO")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("PLAYER_PYEAR") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("COLLEGE_NAME") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                               
                        </div>
                    </div>

                        </form>
                </div>




            </div>
            </div>
            </div> 
        
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">

        function StudName(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            var Name = idno[0].split("-");
            document.getElementById('ctl00_ContentPlaceHolder1_txtStudName').value = idno[1];
            document.getElementById('ctl00_ContentPlaceHolder1_hfStudNo').value = Name[0];
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .btn-primary {
            text-align: center;
        }
    </style>
</asp:Content>

