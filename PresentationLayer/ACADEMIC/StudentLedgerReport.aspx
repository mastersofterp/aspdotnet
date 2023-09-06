<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentLedgerReport.aspx.cs" Inherits="Academic_StudentLedgerReport"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_lvStudent_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updFee"
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

    <asp:UpdatePanel runat="server" ID="updFee">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title" id="spanPageHeader" runat="server">STUDENT LEDGER REPORT</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <%--Search Pannel Added by Amit Bhumbur on date 13/01/22 --%>
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
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSearch" InitialValue="0"
                                                           Display="None" ErrorMessage="Please select search string from the given list"
                                                            SetFocusOnError="true" ValidationGroup="submit" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">
                                                <asp:Panel ID="pnltextbox" runat="server">
                                                    <div id="divtxt" runat="server" style="display: block">
                                                        <div class="label-dynamic">
                                                             <sup>* </sup>
                                                            <label>Search String</label>
                                                        </div>
                                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvSearchtring" runat="server" ControlToValidate="txtSearch" InitialValue="" Enabled="false"
                                                            Display="None" ErrorMessage="Please Enter search string in the given text box"
                                                            SetFocusOnError="true" ValidationGroup="submit" />
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlDropdown" runat="server">
                                                    <div id="divDropDown" runat="server" style="display: block">
                                                        <div class="label-dynamic">
                                                             <sup>* </sup>
                                                            <%-- <label id="lblDropdown"></label>--%>
                                                            <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvDDL" runat="server" ControlToValidate="ddlDropdown" InitialValue="0" Enabled="false"
                                                            Display="None" ErrorMessage="Please select search string from the given list"
                                                            SetFocusOnError="true" ValidationGroup="submit" />
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <%-- OnClientClick="return submitPopup(this.name);"--%>
                                            <asp:Button ID="btnSearchCriteria" runat="server" Text="Search" OnClick="btnSearchCriteria_Click" CssClass="btn btn-primary" ValidationGroup="submit" />
                                            <asp:Button ID="btnCloseCriteria" runat="server" Text="Close" CssClass="btn btn-warning" OnClick="btnCloseCriteria_Click"
                                                OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                            <asp:ValidationSummary ID="valSummery1" DisplayMode="List" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="submit" />
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
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
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Sr.No.</th>
                                                                            <th>Name
                                                                            </th>
                                                                           <%-- <th style="display: none">IdNo
                                                                            </th>--%>
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
                                                            <td> <%# Container.DataItemIndex +1 %></td>
                                                            <td>
                                                                <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                    OnClick="lnkId_Click"></asp:LinkButton>
                                                            </td>
                                                           <%-- <td style="display: none">
                                                                <%# Eval("idno")%>
                                                            </td> comment this or delete this plz --%>
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
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-12" id="divStudentSearch" runat="server">
                                <div class="row">
                                    <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Enter Enrollment No.</label>
                                        </div>
                                        <asp:TextBox ID="txtEnrollNo" runat="server" MaxLength="15" TabIndex="1" CssClass="form-control" />
                                        <span class="">
                                            <asp:Image ID="imgSearch" runat="server" ImageUrl="~/images/search.png" Visible="false" TabIndex="4" />
                                        </span>
                                        <asp:RequiredFieldValidator ID="valEnrollNo" runat="server" ControlToValidate="txtEnrollNo"
                                            Display="None" ErrorMessage="Please Enter Enrollment Number." SetFocusOnError="true"
                                            ValidationGroup="studSearch" />
                                    </div>--%>

                                    <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <%# (Eval("DD_DT").ToString() != string.Empty) ? ((DateTime)Eval("DD_DT")).ToShortDateString() : Eval("DD_DT") %>
                                            <asp:Button ID="btnShowInfo" runat="server" Text="Show Information" OnClick="btnShowInfo_Click"
                                                TabIndex="3" ValidationGroup="studSearch" CssClass="btn btn-info" />
                                            <asp:DropDownList ID="ddlExamType" AppendDataBoundItems="true" runat="server"
                                                CssClass="form-control" Enabled="true" Visible="False">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Regular</asp:ListItem>
                                                <asp:ListItem Value="2">Repeater</asp:ListItem>
                                                <asp:ListItem Value="3">Revaluation</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:ValidationSummary ID="valSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="studSearch" />
                                    </div>--%>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Payment for Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" AppendDataBoundItems="true" runat="server"
                                            CssClass="form-control" Enabled="true" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%# Eval("DD_NO") %>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" InitialValue="0" ErrorMessage="Please select semester" SetFocusOnError="true"
                                            ValidationGroup="studSearch" />--%>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12" id="divStudInfo" runat="server" style="display: none">
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblDYlvEnrollmentNo" runat="server" Font-Bold="true"></asp:Label>
                                                :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblRegNo" CssClass="data_label" runat="server" /></a>
                                            </li>
                                            <li class="list-group-item"><b>Student's Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudName" CssClass="data_label" runat="server" /></a>
                                            </li>
                                            <li class="list-group-item"><b>Gender :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSex" CssClass="data_label" runat="server" /></a>
                                            </li>
                                            <li class="list-group-item"><b>Date of Admission :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDateOfAdm" CssClass="data_label" runat="server" /></a>
                                            </li>
                                            <li class="list-group-item"><b>Payment Type :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblPaymentType" CssClass="data_label" runat="server" /></a>
                                            </li>
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblDYtxtSession" runat="server" Font-Bold="true"></asp:Label>
                                                :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="Label8" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDegree" CssClass="data_label" runat="server" /></a>
                                            </li>
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblBranch" CssClass="data_label" runat="server" /></a>
                                            </li>
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblDYtxtYear" runat="server" Font-Bold="true"></asp:Label>
                                                :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblYear" CssClass="data_label" runat="server" /></a>
                                            </li>
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSemester" CssClass="data_label" runat="server" /></a>
                                            </li>
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblDYtxtBatch" runat="server" Font-Bold="true"></asp:Label>
                                                :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblBatch" CssClass="data_label" runat="server" /></a>
                                            </li>

                                            <li class="list-group-item"><b>
                                                <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Admission Status"></asp:Label>
                                                :</b>
                                                <a class="sub-label">
                                                <asp:Label ID="lblAdmStatus" CssClass="data_label" runat="server" /></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer" id="divButton" runat="server">
                                <asp:Button ID="btnShowReport" runat="server" Text="Ledger Report" OnClick="btnShowReport_Click" Visible="false" CssClass="btn btn-info" />
                                <asp:Button ID="btnShowReportFormat2" runat="server" Text="Ledger Report Format-II" OnClick="btnShowReportFormat2_Click" Visible="false" CssClass="btn btn-info" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" Visible="false" CssClass="btn btn-warning" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShowReport" />
        </Triggers>
    </asp:UpdatePanel>

    <table width="100%" cellpadding="2" cellspacing="2" runat="server" visible="false" border="0">
        <%-- Page Title --%>
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">STUDENT LEDGER&nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--PAGE HELP--%>
        <tr>
            <td>
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </p>
                    </div>
                </div>

                <script type="text/javascript" language="javascript">
                    // Move an element directly on top of another element (and optionally
                    // make it the same size)
                    function Cover(bottom, top, ignoreSize) {
                        var location = Sys.UI.DomElement.getLocation(bottom);
                        top.style.position = 'absolute';
                        top.style.top = location.y + 'px';
                        top.style.left = location.x + 'px';
                        if (!ignoreSize) {
                            top.style.height = bottom.offsetHeight + 'px';
                            top.style.width = bottom.offsetWidth + 'px';
                        }
                    }
                </script>

                <ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
                    <Animations>
                    <OnClick>
                        <Sequence>
                            <%-- Disable the button so it can't be clicked again --%>
                            <EnableAction Enabled="false" />
                            
                            <%-- Position the wire frame on top of the button and show it --%>
                            <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                            
                            <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
                            <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                            <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                            <FadeIn AnimationTarget="info" Duration=".2"/>
                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                            
                            <%-- Flash the text/border red and fade in the "close" button --%>
                            <Parallel AnimationTarget="info" Duration=".5">
                                <Color PropertyKey="color" StartValue="#666666" EndValue="#FF0000" />
                                <Color PropertyKey="borderColor" StartValue="#666666" EndValue="#FF0000" />
                            </Parallel>
                            <Parallel AnimationTarget="info" Duration=".5">
                                <Color PropertyKey="color" StartValue="#FF0000" EndValue="#666666" />
                                <Color PropertyKey="borderColor" StartValue="#FF0000" EndValue="#666666" />
                                <FadeIn AnimationTarget="btnCloseParent" MaximumOpacity=".9" />
                            </Parallel>
                        </Sequence>
                    </OnClick>
                    </Animations>
                </ajaxToolKit:AnimationExtender>
                <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
                    <Animations>
                    <OnClick>
                        <Sequence AnimationTarget="info">
                            <%--  Shrink the info panel out of view --%>
                            <StyleAction Attribute="overflow" Value="hidden"/>
                            <Parallel Duration=".3" Fps="15">
                                <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                <FadeOut />
                            </Parallel>
                            
                            <%--  Reset the sample so it can be played again --%>
                            <StyleAction Attribute="display" Value="none"/>
                            <StyleAction Attribute="width" Value="250px"/>
                            <StyleAction Attribute="height" Value=""/>
                            <StyleAction Attribute="fontSize" Value="12px"/>
                            <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />
                            
                            <%--  Enable the button so it can be played again --%>
                            <EnableAction AnimationTarget="btnHelp" Enabled="true" />
                        </Sequence>
                    </OnClick>
                    <OnMouseOver>
                        <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                    </OnMouseOver>
                    <OnMouseOut>
                        <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                    </OnMouseOut>
                    </Animations>
                </ajaxToolKit:AnimationExtender>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset>
                    <legend>Search Student</legend>
                    <table width="100%">
                        <tr>
                            <td>Search by:&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:RadioButton ID="rdoEnrollmentNo" Text="Enrollment No"
                                Checked="true" runat="server" GroupName="stud" TabIndex="1" />
                                &nbsp;&nbsp;<asp:RadioButton ID="rdoStudentName" Text="Student Name" runat="server"
                                    GroupName="stud" TabIndex="2" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtSearchText" runat="server" Width="350px" TabIndex="3" />&nbsp;&nbsp;
                                <asp:Button ID="btnSearch" Text="Search" runat="server" OnClick="btnSearch_Click"
                                    TabIndex="4" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <asp:ListView ID="lvStudentRecords" runat="server">
                                    <LayoutTemplate>
                                        <div id="listViewGrid" class="vista-grid">
                                            <div class="titlebar">
                                                Student Search Result(s)
                                            </div>
                                            <table class="datatable" cellpadding="0" cellspacing="0">
                                                <tr class="header">
                                                    <th>Report
                                                    </th>
                                                    <th>Student Name
                                                    </th>
                                                    <th>Enrollment No.
                                                    </th>
                                                    <th>Degree
                                                    </th>
                                                    <th>Branch
                                                    </th>
                                                    <th>Year
                                                    </th>
                                                    <th>Semester
                                                    </th>
                                                    <th>Batch
                                                    </th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td>
                                                <asp:ImageButton ID="btnShowReport" runat="server" AlternateText="Show Report" CommandArgument='<%# Eval("IDNO") %>'
                                                    ImageUrl="~/images/print.gif" ToolTip="Show Report" OnClick="btnShowReport_Click" TabIndex="5" />
                                            </td>
                                            <td>
                                                <%# Eval("NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("ENROLLMENTNO")%>
                                            </td>
                                            <td>
                                                <%# Eval("CODE")%>
                                            </td>
                                            <td>
                                                <%# Eval("SHORTNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("YEARNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("SEMESTERNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("BATCHNAME")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <div align="center" class="data_label">
                                            -- No Student Record Found --
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnCancel1" runat="server" Text="Cancel" CausesValidation="false"
                    OnClick="btnCancel_Click" TabIndex="6" />
                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                    ShowSummary="false" ValidationGroup="report" />
            </td>
        </tr>
    </table>

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">


        //////////added by Amit B. on date 16/01/22

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

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122) || (char =32)) {
                return true;
            }
            else {
                return false;
            }

        }
    }

    ///////////
    </script>




</asp:Content>
