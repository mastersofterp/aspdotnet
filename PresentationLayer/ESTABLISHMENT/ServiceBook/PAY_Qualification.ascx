<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PAY_Qualification.ascx.cs"
    Inherits="Masters_PAY_Qualification" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<link href="../Css/master.css" rel="stylesheet" type="text/css" />
<link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />

<br />
<div class="row">
    <div class="col-md-12">
        <form role="form">
            <div class="box-body">
                <div class="col-md-12">
                    <div class="col-md-6">
                        <asp:Panel ID="pnlAdd" runat="server">
                            <div class="panel panel-info">
                                <div class="panel panel-heading">Qualification</div>
                                <div class="panel panel-body">
                                    <div class="form-group col-md-6">
                                        <label>Qualification Level :</label>
                                        <asp:DropDownList ID="ddlLevel" AppendDataBoundItems="true" runat="server" AutoPostBack="true" TabIndex="4"
                                            CssClass="from-control" ToolTip="Select Qualification Level" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlLevel" InitialValue="0"
                                            Display="None" ErrorMessage="Please Select Qualification Level " ValidationGroup="ServiceBook"
                                            SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6" id="trqualification" runat="server">
                                        <label>Qualification:</label>
                                        <asp:DropDownList ID="ddlQualification" AppendDataBoundItems="true" runat="server"
                                            CssClass="from-control" ToolTip="Select Qualification" TabIndex="5">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlQualification" InitialValue="0"
                                            Display="None" ErrorMessage="Please Select Qualification" ValidationGroup="ServiceBook"
                                            SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Examination Passed :</label>
                                        <asp:TextBox ID="txtExam" runat="server" CssClass="from-control" ToolTip="Enter Examination Passed" TabIndex="6"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="txtExam"
                                                            Display="None" ErrorMessage="Please Enter Examination Passed" ValidationGroup="ServiceBook"
                                                             SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>University Name :</label>
                                        <asp:TextBox ID="txtUniversity" runat="server" CssClass="from-control" ToolTip="Enter University Name" TabIndex="7"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvUniversity" runat="server" ControlToValidate="txtUniversity"
                                                    Display="None" ErrorMessage="Please Enter University Name" ValidationGroup="ServiceBook"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Institute Name :</label>
                                        <asp:TextBox ID="txtInstituteName" runat="server" CssClass="from-control" ToolTip="Enter Institute Name" TabIndex="8"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Location :</label>
                                        <asp:TextBox ID="txtLocation" runat="server" CssClass="from-control" ToolTip="Enter Location" TabIndex="9"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Year of passing :</label>
                                        <asp:TextBox ID="txtYearOfPassing" runat="server" MaxLength="4" CssClass="from-control" ToolTip="Enter Year of passing"
                                            onkeypress="return CheckNumeric(event,this);" TabIndex="10"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator ID="rfvYearOfPassing" runat="server" ControlToValidate="txtYearOfPassing"
                                                    Display="None" ErrorMessage="Please Enter Year of passing" ValidationGroup="ServiceBook"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-md-6" runat="server" visible="false">
                                        <label>Council Reg.no :</label>
                                        <asp:TextBox ID="txtRegNo" runat="server" CssClass="from-control" ToolTip="Enter Council Registration Number"
                                            TabIndex="11"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ID="rfvtxtRegNo" runat="server" ControlToValidate="txtRegNo"
                                                    Display="None" ErrorMessage="Please Enter Reg.no" ValidationGroup="ServiceBook"
                                                    SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-md-6" runat="server" visible="false">
                                        <label>DCI/MCI ID Card No :</label>
                                        <asp:TextBox ID="txtIDCardNo" runat="server" CssClass="from-control" ToolTip="Enter DCI/MCI ID Card No"
                                            TabIndex="12"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ID="rfvtxtRegNo" runat="server" ControlToValidate="txtRegNo"
                                                    Display="None" ErrorMessage="Please Enter Reg.no" ValidationGroup="ServiceBook"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-md-6" runat="server" visible="false">
                                        <label>Reg. Council Name :</label>
                                        <asp:TextBox ID="txtRegName" runat="server" CssClass="from-control" TabIndex="13"
                                            ToolTip="Enter Registration Council Name:"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6" id="trDate" runat="server">
                                        <label>Date :</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <asp:Image ID="imgCalDateOfBirth" runat="server" ImageUrl="~/images/calendar.png"
                                                    Style="cursor: pointer" />
                                            </div>
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="from-control" ToolTip="Enter Date" Enabled="true" ValidationGroup="emp"
                                                TabIndex="14" Style="z-index: 0;" />
                                            <ajaxToolKit:CalendarExtender ID="ceBirthDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtDate" PopupButtonID="imgCalDateOfBirth" Enabled="true"
                                                EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeBirthDate" runat="server" TargetControlID="txtDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                        </div>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Specialization :</label>
                                        <asp:TextBox ID="txtSpecialization" runat="server" CssClass="from-control" ToolTip="Enter Specialization"
                                            onkeypress="return CheckAlphabet(event,this);" TabIndex="15"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Upload Document :</label>
                                        <asp:FileUpload ID="flupld" runat="server" ToolTip="Click here to Upload Document" TabIndex="16" />
                                    </div>
                                    <div class="form-group col-md-12">
                                        <p class="text-center">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="17"
                                                OnClick="btnSubmit_Click" CssClass="btn btn-success" ToolTip="Click here to Submit" />&nbsp;
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="18"
                                                OnClick="btnCancel_Click" CssClass="btn btn-danger" ToolTip="Click here to Reset" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-md-6">
                        <asp:Panel ID="pnlQuali" runat="server" ScrollBars="Auto">
                            <asp:ListView ID="lvQuali" runat="server">
                                <EmptyDataTemplate>
                                    <br />
                                    <p class="text-center text-bold">
                                        <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Qualification"></asp:Label>
                                    </p>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <h4 class="box-title">Qualification Details
                                        </h4>
                                        <table class="table table-bordered table-hover">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Action
                                                    </th>
                                                    <th>Level Name
                                                    </th>
                                                    <th>Qualification
                                                    </th>
                                                    <%-- <th width="15%" >
                                                   Qualification
                                                </th>--%>
                                                    <th>University Name
                                                    </th>
                                                    <th>Institute Name
                                                    </th>
                                                    <th>Passing Year
                                                    </th>
                                                    <th>Reg. No
                                                    </th>
                                                    <th>Specialization
                                                    </th>
                                                    <th>Attachment
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("QNO")%>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("QNO") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                        </td>
                                        <td>
                                            <%# Eval("QUALILEVELNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("ExamName")%>
                                        </td>
                                        <%-- <td width="10%" >
                                        <%# Eval("QUALI")%>
                                    </td>--%>
                                        <td>
                                            <%# Eval("UNIVERSITY_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("inst")%>
                                        </td>
                                        <td>
                                            <%# Eval("passyear")%>
                                        </td>
                                        <td>
                                            <%# Eval("regno")%>
                                        </td>
                                        <td>
                                            <%# Eval("speci")%>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("QNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </form>
    </div>
</div>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td>&nbsp
        </td>
    </tr>
    <tr>
        <td valign="top" width="48%">
            <%-- <asp:Panel ID="pnlAdd" runat="server" Style="text-align: left; width: 95%; padding-left: 5px;">
                <fieldset class="fieldsetPay">
                    <legend class="legendPay">Qualification</legend>
                    <br />--%>
            <%-- <table cellpadding="0" cellspacing="0" style="width: 100%;">--%>

            <%-- ALready Committed<tr>
                            <td class="form_left_label">
                               Select Category :
                            </td>
                            <td class="form_left_text">
                                <asp:RadioButtonList ID="rblCategory" runat="server" RepeatDirection ="Horizontal">
                                <asp:ListItem Enabled ="true" Selected ="True" Text ="UG" Value ="1"></asp:ListItem>
                                <asp:ListItem Enabled ="true" Text ="PG" Value ="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>--%>

            <%--  <tr>
                            <td class="form_left_label">Qualification Level:
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlLevel" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                    Width="200px" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlLevel" InitialValue="0"
                                    Display="None" ErrorMessage="Please Select Qualification Level " ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trqualification" runat="server">
                            <td class="form_left_label">Qualification:
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlQualification" AppendDataBoundItems="true" runat="server"
                                    Width="200px">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlQualification" InitialValue="0"
                                    Display="None" ErrorMessage="Please Select Qualification" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>


                        <tr>
                            <td class="form_left_label">Examination Passed :
                            </td>
                            <td class="form_left_text">--%><%-- Already COmmitted onkeyup="return validateAlphabet(this);"--%>
            <%-- <asp:TextBox ID="txtExam" runat="server" Width="200px"></asp:TextBox>--%>
            <%-- ALready COmmitted<asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="txtExam"
                                    Display="None" ErrorMessage="Please Enter Examination Passed" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>--%>
            <%-- </td>
                        </tr>

                        <tr>
                            <td class="form_left_label">University Name :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtUniversity" runat="server" Width="200px"></asp:TextBox>--%>
            <%--Already Committed<asp:RequiredFieldValidator ID="rfvUniversity" runat="server" ControlToValidate="txtUniversity"
                                    Display="None" ErrorMessage="Please Enter University Name" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>--%>
            <%-- </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Institute Name:
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtInstituteName" runat="server" Width="200px"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Location:
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtLocation" runat="server" Width="200px"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Year of passing :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtYearOfPassing" runat="server" MaxLength="4" Width="200px" onkeypress="return CheckNumeric(event,this);"></asp:TextBox>--%>
            <%--Already COmmitted <asp:RequiredFieldValidator ID="rfvYearOfPassing" runat="server" ControlToValidate="txtYearOfPassing"
                                    Display="None" ErrorMessage="Please Enter Year of passing" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>--%>
            <%--  </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Council Reg.no :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtRegNo" runat="server" Width="200px"></asp:TextBox>--%>
            <%--Already Committed<asp:RequiredFieldValidator ID="rfvtxtRegNo" runat="server" ControlToValidate="txtRegNo"
                                    Display="None" ErrorMessage="Please Enter Reg.no" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>--%>
            <%-- </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">DCI/MCI ID Card No :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtIDCardNo" runat="server" Width="200px"></asp:TextBox>--%>
            <%--Already Committed<asp:RequiredFieldValidator ID="rfvtxtRegNo" runat="server" ControlToValidate="txtRegNo"
                                    Display="None" ErrorMessage="Please Enter Reg.no" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>--%>
            <%--  </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Reg. Council Name:
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtRegName" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>

                        <tr id="trDate" runat="server">
                            <td class="form_left_label" style="width: 25%">Date :
                            </td>
                            <td class="form_left_text" style="width: 25%">
                                <asp:TextBox ID="txtDate" runat="server" Width="80px" Enabled="true" ValidationGroup="emp"
                                    TabIndex="13" />
                                <asp:Image ID="imgCalDateOfBirth" runat="server" ImageUrl="~/images/calendar.png"
                                    Style="cursor: pointer" TabIndex="14" />
                                <ajaxToolKit:CalendarExtender ID="ceBirthDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtDate" PopupButtonID="imgCalDateOfBirth" Enabled="true"
                                    EnableViewState="true">
                                </ajaxToolKit:CalendarExtender>
                                <ajaxToolKit:MaskedEditExtender ID="meeBirthDate" runat="server" TargetControlID="txtDate"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="True" />

                            </td>
                            <td style="width: 25%"></td>
                            <td style="width: 25%"></td>
                        </tr>





                        <tr>
                            <td class="form_left_label">Specialization: </td>--%>
            <%-- <td class="form_left_text">--%><%--onkeyup="return validateAlphabet(this);"--%>
            <%-- <asp:TextBox ID="txtSpecialization" runat="server" Width="200px" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>--%>
            <%--Already Committed <asp:RequiredFieldValidator ID="rfvSpecialization" runat="server" ControlToValidate="txtSpecialization"
                                    Display="None" ErrorMessage="Please Enter Specialization" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>--%>
            <%--</td>
                        </tr>


                        <tr>
                            <td class="form_left_label">Upload Document :
                            </td>
                            <td class="form_left_text">
                                <asp:FileUpload ID="flupld" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook"
                                    OnClick="btnSubmit_Click" Width="80px" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    OnClick="btnCancel_Click" Width="80px" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>--%>
        </td>
        <td colspan="2" align="center" valign="top">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="center">
                        <%--  <asp:ListView ID="lvQuali" runat="server">
                            <EmptyDataTemplate>
                                <br />
                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Qualification"></asp:Label>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <div class="vista-gridServiceBook">
                                    <div class="titlebar-ServiceBook">
                                        Qualification
                                    </div>
                                    <table class="datatable-ServiceBook" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <thead>
                                            <tr class="header-ServiceBook">
                                                <th width="10%">Action
                                                </th>
                                                <th width="10%">Level Name
                                                </th>
                                                <th width="10%">Qualification
                                                </th>--%>
                        <%--Already COmmitted <th width="15%" >
                                                   Qualification
                                                </th>--%>
                        <%-- <th width="15%">University Name
                                                </th>
                                                <th width="15%">Institute Name
                                                </th>
                                                <th width="10%">Passing Year
                                                </th>
                                                <th width="10%">Reg. No
                                                </th>
                                                <th width="15%">Specialization
                                                </th>
                                                <th width="15%">Attachment
                                                </th>
                                            </tr>
                                            <thead>
                                    </table>
                                </div>
                                <div class="listview-container-servicebook">
                                    <div id="Div1" class="vista-gridServiceBook">
                                        <table class="datatable-ServiceBook" cellpadding="0" cellspacing="0" style="width: 100%;">
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="item-ServiceBook" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td width="10%">
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("QNO")%>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("QNO") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td width="10%">
                                        <%# Eval("QUALILEVELNAME")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("ExamName")%>
                                    </td>--%>
                        <%--Already COmmitted <td width="10%" >
                                        <%# Eval("QUALI")%>
                                    </td>--%>
                        <%-- <td width="10%">
                                        <%# Eval("UNIVERSITY_NAME")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("inst")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("passyear")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("regno")%>
                                    </td>




                                    <td width="10%">
                                        <%# Eval("speci")%>
                                    </td>


                                    <td width="15%">
                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("QNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="item-ServiceBook" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td width="10%">
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("QNO")%>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("QNO") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td width="10%">
                                        <%# Eval("QUALILEVELNAME")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("ExamName")%>
                                    </td>--%>
                        <%-- Already Committed   <td width="10%" >
                                        <%# Eval("QUALI")%>
                                    </td>--%>
                        <%--<td width="10%">
                                        <%# Eval("UNIVERSITY_NAME")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("inst")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("passyear")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("regno")%>
                                    </td>




                                    <td width="10%">
                                        <%# Eval("speci")%>
                                    </td>


                                    <td width="15%">
                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("QNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>--%>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>&nbsp
        </td>
    </tr>
</table>
<%--<%# Eval("ExamName")%> <%# Eval("inst")%>--%>
<ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
    runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
    OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
    BackgroundCssClass="modalBackground" />
<div class="col-md-12">
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div class="text-center">
            <div class="modal-content">
                <div class="modal-body">
                    <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                    <div class="text-center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</div>

<script type="text/javascript">
    //  keeps track of the delete button for the row
    //  that is going to be removed
    var _source;
    // keep track of the popup div
    var _popup;

    function showConfirmDel(source) {
        this._source = source;
        this._popup = $find('mdlPopupDel');

        //  find the confirm ModalPopup and show it    
        this._popup.show();
    }

    function okDelClick() {
        //  find the confirm ModalPopup and hide it    
        this._popup.hide();
        //  use the cached button as the postback source
        __doPostBack(this._source.name, '');
    }

    function cancelDelClick() {
        //  find the confirm ModalPopup and hide it 
        this._popup.hide();
        //  clear the event source
        this._source = null;
        this._popup = null;
    }

    function CheckAlphabet(event, obj) {

        var k = (window.event) ? event.keyCode : event.which;
        if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
            obj.style.backgroundColor = "White";
            return true;

        }
        if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
            obj.style.backgroundColor = "White";
            return true;

        }
        else {
            alert('Please Enter Alphabets Only!');
            obj.focus();
        }
        return false;
    }
    function CheckNumeric(event, obj) {
        var k = (window.event) ? event.keyCode : event.which;
        //alert(k);
        if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
            obj.style.backgroundColor = "White";
            return true;
        }
        if (k > 45 && k < 58) {
            obj.style.backgroundColor = "White";
            return true;

        }
        else {
            alert('Please Enter numeric Value');
            obj.focus();
        }
        return false;
    }

</script>

