<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_ServiceBookEntry.aspx.cs" Inherits="PayRoll_Pay_ServiceBookEntry"
    Title="" %>

<%@ Register Src="~/PayRoll/Transactions/Pay_PersonalMemoranda.ascx" TagName="PersonalMemoranda"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_FamilyParticulars.ascx" TagName="FamilyParticulars"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/PAY_Qualification.ascx" TagName="Qualification"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_Training.ascx" TagName="Training" TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_DepartmentalExam.ascx" TagName="DepartmentalExam"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_PreviousService.ascx" TagName="PreviousService"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_ForeginService.ascx" TagName="ForeginService"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_LoansAndAdvance.ascx" TagName="LoansAndAdvance"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_Nomination.ascx" TagName="Nomination"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_Leave.ascx" TagName="Leave" TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_Matters.ascx" TagName="Matters" TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_PayRevision.ascx" TagName="PayRevision"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_Increment_Termination.ascx" TagName="Increment_Termination"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_ImageUpload.ascx" TagName="ImageUpload"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_publication_Details.ascx" TagName="PublicationDetails"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_Invited_Talks.ascx" TagName="InvitedTalks"
    TagPrefix="uc1" %>
<%@ Register Src="~/PayRoll/Transactions/Pay_Admin_Responsibilities.ascx" TagName="AdminResponsibilities"
    TagPrefix="uc1" %>
<%@ Register Src="~/PAYROLL/TRANSACTIONS/Pay_Training_Conducted.ascx" TagName="Traningconducted"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">SERVICEBOOK ENTRY</h3>
                </div>
                <div>
                    <form role="form">
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlSelection" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel panel-heading">Select Criteria</div>
                                        <div class="panel panel-body">
                                            <asp:UpdatePanel ID="upEmployee" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group col-md-12">
                                                        <div class="form-group col-md-4" id="trorder" runat="server">
                                                            <label>Order By :</label>
                                                            <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="1"
                                                                runat="server" CssClass="form-control" ToolTip="Select Order by"
                                                                OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged">
                                                                <asp:ListItem Value="1" Selected="True">IdNo</asp:ListItem>
                                                                <asp:ListItem Value="2">Name</asp:ListItem>
                                                                <%--<asp:ListItem Value ="3">RFIDNo</asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                        </div>
                                                      <%--  <div class="form-group col-md-4" id="trstaff" runat="server">
                                                            <label>Select RFID No :</label>
                                                            <asp:DropDownList ID="ddlRFID" runat="server" CssClass="form-control" ToolTip="Select RFID Number"
                                                                AppendDataBoundItems="true" TabIndex="2"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlRFID_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>--%>
                                                        <div class="form-group col-md-4">
                                                            <label>Employee Name :</label>
                                                            <asp:DropDownList ID="ddlEmployee" AppendDataBoundItems="true" runat="server" CssClass="form-control"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged"
                                                                TabIndex="3" ToolTip="Select EMployee Name">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee Name"
                                                                ValidationGroup="ServiceBook" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlMenu" runat="server" CssClass="panel panel-responsive">
                                    <div class="row">
                                        <%--<div class="col-md-12" style="background-color: #71AFCC; height: 40px; padding-left: 0px;z-index: 1">--%>
                                        <div class="col-md-12" style="background-color:#dee2ea; height: 40px; padding-left: 0px;z-index: 1">
                                            <asp:UpdatePanel ID="updSelection1" runat="server">
                                                <ContentTemplate>
                                                    <asp:Menu ID="mainMenu" Height="10px" runat="server" Orientation="Horizontal">
                                                        <StaticMenuItemStyle CssClass="MenuItem"></StaticMenuItemStyle>
                                                        <DynamicMenuItemStyle CssClass="DynamicMenuItem" />
                                                        <StaticHoverStyle CssClass="MenuItemHover"></StaticHoverStyle>
                                                        <Items>
                                                            <asp:MenuItem Text="Personal" Value="Personal" Selected="true">
                                                                <asp:MenuItem Text="Personal Memoranda" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=1" />
                                                                <asp:MenuItem Text="Family Particulars" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=2" />
                                                                <asp:MenuItem Text="Nomination" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=9" />
                                                                <asp:MenuItem Text="Matters" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=11" />
                                                                <asp:MenuItem Text="Image Upload" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=14" />
                                                            </asp:MenuItem>
                                                            <asp:MenuItem Text="Academic" Value="Academic">
                                                                <asp:MenuItem Text="Qualification" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=3" />
                                                                <asp:MenuItem Text="Dept. Examination" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=5" />
                                                            </asp:MenuItem>
                                                            <asp:MenuItem Text="Professional" Value="Professional">
                                                                <asp:MenuItem Text="Previous Experience" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=6" />
                                                                <asp:MenuItem Text="Administrative Responsibilities" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=16" />
                                                                <asp:MenuItem Text="Publication Details" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=15" />
                                                                <asp:MenuItem Text="Invited Talks" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=17" />
                                                                <asp:MenuItem Text="Training/Short Term Course/Conference Attended" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=4" />
                                                                <asp:MenuItem Text="Training/Short Term Course/Conference Conducted" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=7" />
                                                            </asp:MenuItem>
                                                            <asp:MenuItem Text="Financial" Value="Financial">
                                                                <asp:MenuItem Text="Loan & Advance" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=8" />
                                                                <asp:MenuItem Text="Leave" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=10" />
                                                                <asp:MenuItem Text="Pay Revision" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=12" />
                                                                <asp:MenuItem Text="Transaction Type Details" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=13" />
                                                            </asp:MenuItem>
                                                        </Items>
                                                        <StaticItemTemplate>
                                                            <%# Eval("Text") %>
                                                        </StaticItemTemplate>
                                                    </asp:Menu>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <div class="col-md-12">
                                    <asp:Panel ID="upWebUserControl" runat="server">
                                        <uc1:PersonalMemoranda ID="UserControl1" runat="server" />
                                        <uc1:FamilyParticulars ID="UserControl2" runat="server" Visible="false" />
                                        <uc1:Qualification ID="UserControl3" runat="server" Visible="false" />
                                        <uc1:Training ID="UserControl4" runat="server" Visible="false" />
                                        <uc1:DepartmentalExam ID="UserControl5" EnableViewState="true" runat="server" Visible="false" />
                                        <uc1:PreviousService ID="UserControl6" runat="server" Visible="false" />
                                        <%--  <uc1:ForeginService ID="UserControl7" runat="server" Visible="false" />--%>
                                        <uc1:Traningconducted ID="UserControl7" runat="server" Visible="false" />
                                        <uc1:LoansAndAdvance ID="UserControl8" runat="server" Visible="false" />
                                        <uc1:Nomination ID="UserControl9" runat="server" Visible="false" />
                                        <uc1:Leave ID="UserControl10" runat="server" Visible="false" />
                                        <uc1:Matters ID="UserControl11" runat="server" Visible="false" />
                                        <uc1:PayRevision ID="UserControl12" runat="server" Visible="false" />
                                        <uc1:Increment_Termination ID="UserControl13" runat="server" Visible="false" />
                                        <uc1:PublicationDetails ID="UserControl15" runat="server" Visible="false" />
                                        <uc1:AdminResponsibilities ID="UserControl16" runat="server" Visible="false" />
                                        <uc1:InvitedTalks ID="UserControl17" runat="server" Visible="false" />
                                    </asp:Panel>
                                    <uc1:ImageUpload ID="UserControl14" runat="server" Visible="false" />
                                </div>
                            </div>
                        </div>
                    </form>
                </div>               
            </div>
        </div>
    </div>

    <table cellpadding="0" cellspacing="0" width="100%">
        <%--PAGE HELP--%>
        <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
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
            </td>
        </tr>
        <tr>
            <td>&nbsp
            </td>
        </tr>
        <tr>
            <td>
                <%--<asp:Panel ID="pnlSelection" runat="server" Style="text-align: left; width: 98%; padding-left: 5px;">
                    <fieldset class="fieldsetPay">
                        <legend class="legendPay">Select Criteria</legend>
                        <br />
                        <asp:UpdatePanel ID="upEmployee" runat="server">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr id="trorder" runat="server">
                                        <td class="form_left_label" style="width: 20%; padding-left: 15px">Order By:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" AutoPostBack="true"
                                                runat="server" Width="100px" OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Selected="True">IdNo</asp:ListItem>
                                                <asp:ListItem Value="2">Name</asp:ListItem>--%>
                <%--Already Committed<asp:ListItem Value ="3">RFIDNo</asp:ListItem>--%>
                <%-- </asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr id="trstaff" runat="server">
                                        <td class="form_left_label" style="padding-left: 15px">Select RFID No :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlRFID" runat="server" Width="400px" AppendDataBoundItems="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlRFID_SelectedIndexChanged">
                                            </asp:DropDownList>--%>
                <%--Already Committed<asp:TextBox ID="txtrfid" AutoPostBack ="true"  runat="server" 
                                                ontextchanged="txtrfid_TextChanged" ></asp:TextBox>--%>

                <%--    </td>
                                    </tr>

                                    <tr>
                                        <td class="form_left_label" style="padding-left: 15px">Employee Name :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlEmployee" AppendDataBoundItems="true" runat="server" Width="400px"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee Name"
                                                ValidationGroup="ServiceBook" InitialValue="0"></asp:RequiredFieldValidator>
                                        <td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </asp:Panel>--%>
            </td>
        </tr>
        <tr>
            <td>&nbsp
            </td>
        </tr>

        <%-- <tr style="height: 30px;" class="f">
            <td class="ui-widget-header">

                <asp:Menu ID="mainMenu" Height="5px" runat="server" Orientation="Horizontal">
                    <StaticMenuItemStyle CssClass="MenuItem"></StaticMenuItemStyle>
                    <DynamicMenuItemStyle CssClass="DynamicMenuItem" />
                    <StaticHoverStyle CssClass="MenuItemHover"></StaticHoverStyle>
                    <Items>
                        <asp:MenuItem Text="Personal" Value="Personal" Selected="true">
                            <asp:MenuItem Text="Personal Memoranda" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=1" />
                            <asp:MenuItem Text="Family Particulars" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=2" />
                            <asp:MenuItem Text="Nomination" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=9" />
                            <asp:MenuItem Text="Matters" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=11" />
                            <asp:MenuItem Text="Image Upload" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=14" />
                        </asp:MenuItem>
                        <asp:MenuItem Text="Academic" Value="Academic">
                            <asp:MenuItem Text="Qualification" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=3" />
                            <asp:MenuItem Text="Dept. Examination" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=5" />
                        </asp:MenuItem>
                        <asp:MenuItem Text="Professional" Value="Professional">
                            <asp:MenuItem Text="Previous Experience" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=6" />
                            <asp:MenuItem Text="Administrative Responsibilities" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=16" />
                            <asp:MenuItem Text="Publication Details" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=15" />
                            <asp:MenuItem Text="Invited Talks" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=17" />
                            <asp:MenuItem Text="Training/Short Term Course/Conference Attended" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=4" />
                            <asp:MenuItem Text="Training/Short Term Course/Conference Conducted" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=7" />
                        </asp:MenuItem>
                        <asp:MenuItem Text="Financial" Value="Financial">
                            <asp:MenuItem Text="Loan & Advance" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=8" />
                            <asp:MenuItem Text="Leave" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=10" />
                            <asp:MenuItem Text="Pay Revision" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=12" />
                            <asp:MenuItem Text="Transaction Type Details" NavigateUrl="~/PAYROLL/TRANSACTIONS/Pay_ServiceBookEntry.aspx?refno=13" />
                        </asp:MenuItem>
                    </Items>
                    <StaticItemTemplate>
                        <%# Eval("Text") %>
                    </StaticItemTemplate>
                </asp:Menu>
            </td>
        </tr>--%>


        <tr>
            <td>&nbsp
            </td>
        </tr>
        <tr>
            <td width="100%" colspan="30">
                <%-- <asp:Panel ID="upWebUserControl" runat="server">
                    <uc1:PersonalMemoranda ID="UserControl1" runat="server" />
                    <uc1:FamilyParticulars ID="UserControl2" runat="server" Visible="false" />
                    <uc1:Qualification ID="UserControl3" runat="server" Visible="false" />
                    <uc1:Training ID="UserControl4" runat="server" Visible="false" />
                    <uc1:DepartmentalExam ID="UserControl5" EnableViewState="true" runat="server" Visible="false" />
                    <uc1:PreviousService ID="UserControl6" runat="server" Visible="false" />--%>
                <%--ALready Committed <uc1:ForeginService ID="UserControl7" runat="server" Visible="false" />--%>
                <%--<uc1:Traningconducted ID="UserControl7" runat="server" Visible="false" />
                    <uc1:LoansAndAdvance ID="UserControl8" runat="server" Visible="false" />
                    <uc1:Nomination ID="UserControl9" runat="server" Visible="false" />
                    <uc1:Leave ID="UserControl10" runat="server" Visible="false" />
                    <uc1:Matters ID="UserControl11" runat="server" Visible="false" />
                    <uc1:PayRevision ID="UserControl12" runat="server" Visible="false" />
                    <uc1:Increment_Termination ID="UserControl13" runat="server" Visible="false" />
                    <uc1:PublicationDetails ID="UserControl15" runat="server" Visible="false" />
                    <uc1:AdminResponsibilities ID="UserControl16" runat="server" Visible="false" />
                    <uc1:InvitedTalks ID="UserControl17" runat="server" Visible="false" />
                </asp:Panel>
                <uc1:ImageUpload ID="UserControl14" runat="server" Visible="false" />--%>
                <%--Already Committed<div id="divTab1" runat="server">
                            personal
                        </div>
                        <div id="divTab2" style="display: none" runat="server">
                            family
                        </div>
                        <div id="divTab3" runat="server">
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                              <ContentTemplate>
                                  <uc1:Qualification ID="Qualification" runat="server" />
                             </ContentTemplate>
                             </asp:UpdatePanel>  
                        </div>
                        <div id="divTab4" style="display: none" runat="server">
                            traning
                        </div>
                        <div id="divTab5" style="display: none" runat="server">
                             <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                              <ContentTemplate>
                                <uc1:DepartmentalExam ID="DepartmentalExam" runat="server"/>
                             </ContentTemplate>
                             </asp:UpdatePanel> 
                        </div>
                        <div id="divTab6" style="display: none" runat="server">
                            Previous Quali.Service
                        </div>
                        <div id="divTab7" style="display: none" runat="server">
                            Foregin Service
                        </div>
                        <div id="divTab8" style="display: none" runat="server">
                            Loans & Advance
                        </div>
                        <div id="divTab9" style="display: none" runat="server">
                            Nomination
                        </div>
                        <div id="divTab10" style="display: none" runat="server">
                            Leave
                        </div>
                        <div id="divTab11" style="display: none" runat="server">
                            Matters
                        </div>
                        <div id="divTab12" style="display: none" runat="server">
                            Pay Revision
                         </div>
                        <div id="divTab13" style="display: none" runat="server">
                            Increment / Termination
                        </div>--%>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        function ShowTab(tabNo) {
            for (i = 1; i <= 17; i++) {

                var tab = document.getElementById("ctl00_ContentPlaceHolder1_divTab" + i);
                if (tab != null) {

                    if (i == tabNo) {
                        document.getElementById("ctl00_ContentPlaceHolder1_divTab" + i).style.display = "block";
                        //document.getElementById("ctl00_ContentPlaceHolder1_UpdatePanel2").disabled = true;
                        //document.getElementById("ctl00_ContentPlaceHolder1_DepartmentalExam").disabled = true;
                    }
                    else {
                        document.getElementById("ctl00_ContentPlaceHolder1_divTab" + i).style.display = "none";

                    }
                }
            }
        }

    </script>



</asp:Content>


