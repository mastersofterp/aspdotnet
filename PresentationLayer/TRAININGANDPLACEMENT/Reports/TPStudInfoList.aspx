<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="TPStudInfoList.aspx.cs" Inherits="TRAININGANDPLACEMENT_Reports_TPStudInfoList"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title"><strong>STUDENT INFORMATION LIST</strong></h3>

                </div>

                <form role="form">
                    <div class="box-body">
                        <div class="col-md-12" ID="pnlSelect" runat="server">
                            <div class="panel panel-info">
                                <div class="panel-heading"><strong>Selection Criteria</strong></div>
                                <div class="panel-body">

                                    <div class="form-group col-md-2 " style="display:none;">
                                         <label>Student Type</label>
                                        <asp:DropDownList ID="ddlStudType" runat="server" class="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Both</asp:ListItem>
                                            <asp:ListItem Value="R">Regular</asp:ListItem>
                                            <asp:ListItem Value="P">Pass-out</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-2">
                                         <label>Session</label>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" ValidationGroup="show"
                                            class="form-control"  OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Summary"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-3">
                                         <label>Degree</label>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"  class="form-control" TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-3">
                                         <label>Branch</label>
                                        <asp:DropDownList ID="ddlBranch" runat="server" class="form-control" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-3">
                                         <label>Scheme</label>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" ValidationGroup="show"
                                            class="form-control" TabIndex="3">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                    </div>
                                    
                                    <div class="form-group col-md-3">
                                         <label>Semester</label>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" class="form-control"
                                            TabIndex="4" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" ValidationGroup="save" InitialValue="0"></asp:RequiredFieldValidator>
                              
                                    </div>
                                    
                                    <div class="form-check col-md-3 " >
                                        <label class="form-check-label">
                                        <asp:CheckBox ID="chkTpReg" runat="server" Checked="true" class="form-check-input" />
                                             <label>Tp_Registered</label>
                                        </label>
                                    </div>

                                   













                                </div>
                                
                                        <div class="box-footer text-center">
                                            <div class="col-md-12">
                                            <%--<asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="Select" OnClick="btnShow_Click"
                                            Width="80px" />--%>
                                            <asp:Button ID="btnExcel" runat="server" Text="Excel Report" ValidationGroup="Select" OnClick="btnExcel_Click"
                                                class="btn btn-info" />

                                            <asp:Button ID="btnCan" runat="server" Text="Cancel" OnClick="btnCan_Click"
                                                class="btn btn-warning" />

                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Select"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>
                                    </div>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>


























    <table cellpadding="0" cellspacing="0" width="100%">
        <%--<tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                STUDENT INFORMATION LIST &nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>--%>
        <%--PAGE HELP--%>
        <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
        <tr>
            <td>
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                    border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);
                    font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                            font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                            <%--<asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record--%>
                            <%--<asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                            Delete Record--%>
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
                    </div>
                </div>

                <script type="text/javascript" language="javascript">
                            // Move an element directly on top of another element (and optionally
                            // make it the same size)
                            function Cover(bottom, top, ignoreSize) 
                            {
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
            <td>
                <br />
               <%-- <asp:Panel ID="pnlSelect" runat="server">--%>
                    <div style="text-align: left; width: 87%; padding-left: 10px;">
                        <fieldset class="fieldsetPay" style="width: 662px">
                         <%--   <legend class="legendPay">Selection Criteria</legend>--%>
                            <table>
                                
                                
                                
                                
                                
                                
                                
                               <%-- <tr>--%>
                                <%--<tr>
                                    <td class="form_left_label">
                                        Student Name :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlStudName" runat="server" Width="250px" AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>--%>
                                
                            </table>
                        </fieldset>
                    </div>
              <%--  </asp:Panel>--%>
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
