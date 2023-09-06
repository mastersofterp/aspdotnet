<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="OtherReport.aspx.cs" Inherits="PAYROLL_REPORTS_OtherReport" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">OTHER REPORTS</h3>

                </div>

                <form role="form">
                    <div class="box-body">

                        <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br />
                            <br />
                            <div class="panel panel-info">
                                <div class="panel-heading">EMPLOYEES OTHER REPORTS</div>
                                <div class="panel-body">
                                    <asp:Panel ID="pnlOther" runat="server">
                                        <div class="form-group col-md-12">
                                            <div class="col-md-6">
                                                <div class="form-group col-md-10">
                                                    <label><sup>* </sup>Select Month:</label>

                                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control"
                                                        AppendDataBoundItems="true" TabIndex="1" ToolTip="Select Month">
                                                    </asp:DropDownList>
                                                     <asp:RequiredFieldValidator ID="rfvMonth" runat="server" Display="None" ControlToValidate="ddlMonth"
                                        ErrorMessage="Please Select Month." ValidationGroup="Payroll" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                          
                                                </div>

                                                <div class="form-group col-md-10">
                                                    <label>College:</label>

                                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control"
                                                        AppendDataBoundItems="true" TabIndex="2" ToolTip="Select College">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-md-10">

                                                    <label>Staff:</label>

                                                    <asp:DropDownList ID="ddlStaffNo" runat="server" CssClass="form-control" ToolTip="Select Staff"
                                                        AppendDataBoundItems="true" TabIndex="3">
                                                    </asp:DropDownList>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="ddlStaffNo"
                                        ErrorMessage="Please Select Scheme." ValidationGroup="Payroll" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                          --%>
                                          

                                                </div>
                                                <div class="form-group col-md-10">
                                                    <label>Show In Report:</label>

                                                    <asp:DropDownList ID="ddlShowInReport" runat="server" TabIndex="4" ToolTip="Select Show In Report" CssClass="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Bank Account No.</asp:ListItem>
                                                        <asp:ListItem Value="2">PF No.</asp:ListItem>
                                                        <asp:ListItem Value="3">PAN No.</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-md-10">
                                                    <label>Pay Heads:<span style="color: Red">*</span></label>
                                                    <asp:ListBox ID="lstParticularColumn" runat="server" SelectionMode="Multiple" ToolTip="Select Pay Heads"
                                                        Height="140px" CssClass="form-control" TabIndex="5" AppendDataBoundItems="True"></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="rfvLstParticularColumn" runat="server" ControlToValidate="lstParticularColumn"
                                                        Display="None" ErrorMessage="Please Select Pay Head" SetFocusOnError="true" InitialValue="0"
                                                        ValidationGroup="Payroll" />
                                                    <asp:HiddenField ID="hidIdNo" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-md-6"></div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-5">
                                </div>
                                <div class="col-md-4">
                                    <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                                        DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />

                                    <asp:Button ID="btnShowReport" runat="server" Text="Show Report"
                                        OnClick="btnShowReport_Click" ValidationGroup="Payroll" TabIndex="6" CssClass="btn btn-info" />

                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger"
                                        OnClick="btnCancel_Click" TabIndex="7" />
                                </div>
                                <div class="col-md-3">
                                </div>
                            </div>
                </form>
            </div>

        </div>

    </div>




    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <%-- Flash the text/border red and fade in the "close" button --%>
        <%--<tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">OTHER&nbsp; REPORTS&nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>--%>
        <%--<asp:CheckBox id="chkPartiularColumn" runat="server" Text="Particular Column" 
                     TabIndex="9" onclick="DisableListboxOnParticularColumn(true);" />--%>
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
    </table>
    <br />
    <center>
        <%-- <asp:Panel Width="60%" runat="server" ID="pnlOther">--%>
    <fieldset class="fieldsetPayNew">
       <%-- <legend class="legendPayNew"><b>Other Reports</b></legend>
        <table width="100%" cellpadding="2" cellspacing="2" border="0">--%>
            <tr>
               <td style="width:25%">
                   <%-- Select Month
                </td>
                <td style="width:1%"><b>:</b></td>
                <td>
                    <asp:DropDownList ID="ddlMonth" runat="server" width="60%" 
                        AppendDataBoundItems="true" TabIndex="5" >
                    </asp:DropDownList>--%>
                </td>
            </tr>
            <tr>
               <td style="width:25%">
                    <%--College
                </td>
                <td style="width:1%"><b>:</b></td>
                <td>
                    <asp:DropDownList ID="ddlCollege" runat="server" width="60%" 
                        AppendDataBoundItems="true" TabIndex="5" ></asp:DropDownList>--%>
                </td>
            </tr>
            
            <tr>
               <td style="width:25%">
                    <%--Staff
                </td>
                <td style="width:1%"><b>:</b></td>
                <td>
                    <asp:DropDownList ID="ddlStaffNo" runat="server" width="60%" 
                        AppendDataBoundItems="true" TabIndex="5" ></asp:DropDownList>--%>
                </td>
            </tr>
            
            
            <tr>
                  <td style="width:25%" valign="top">
                    <%--Show In Report
                </td>
                 <td style="width:1%"><b>:</b></td>
                <td>
                    <asp:DropDownList ID="ddlShowInReport" runat="server" width="60%" AppendDataBoundItems="true" >
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                            <asp:ListItem Value="1">Bank Account No.</asp:ListItem>
                            <asp:ListItem Value="2">PF No.</asp:ListItem>
                            <asp:ListItem Value="3">PAN No.</asp:ListItem>
                    </asp:DropDownList>--%>
                </td>
            </tr>
            <tr>
                <td style="width:25%" valign="top">
                    <%--<asp:CheckBox id="chkPartiularColumn" runat="server" Text="Particular Column" 
                     TabIndex="9" onclick="DisableListboxOnParticularColumn(true);" />--%>
                     <%--Pay Heads</td>
                       <td style="width:1%" valign="top"><b>:</b></td>
                <td>
                    <asp:ListBox ID="lstParticularColumn" runat="server" SelectionMode="Multiple" 
                        Height="140px" Width="60%" TabIndex="10" AppendDataBoundItems="True">
                    </asp:ListBox>
                     <asp:RequiredFieldValidator ID="rfvLstParticularColumn" runat="server" ControlToValidate="lstParticularColumn"
                        Display="None" ErrorMessage="Please Select Atleast one item." SetFocusOnError="true" InitialValue="0" 
                        ValidationGroup="Payroll" />
                    <asp:HiddenField ID="hidIdNo" runat="server" />--%>
                </td>
            </tr>
             </table>
    </fieldset>
    </br>
           <div>
           <center>
            <%-- <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                       DisplayMode="List" ShowMessageBox="true" ShowSummary="false" 
                        Width="123px" />
                        
                           &nbsp;<asp:Button ID="btnShowReport" runat="server" Text="Show Report" 
                        onclick="btnShowReport_Click" ValidationGroup="Payroll" TabIndex="11" Width="15%"/>
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="79px"
                        onclick="btnCancel_Click" TabIndex="12" />--%>
           </center>
           </div> 
           </asp:Panel>
    </center>
    <script type="text/javascript">
        function DisableDropDownListAllEmployee(disable) {
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').selectedIndex = 0;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled = disable;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlStaffNo').disabled = false;

        }
        function DisableDropDownListParticularEmployee(disable) {
            document.getElementById('ctl00_ContentPlaceHolder1_ddlStaffNo').selectedIndex = 0;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlStaffNo').disabled = disable;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled = false;
        }
        function DisableListboxOnParticularColumn(disable) {
            if (document.getElementById('ctl00_ContentPlaceHolder1_chkPartiularColumn').checked) {
                document.getElementById('ctl00_ContentPlaceHolder1_lstParticularColumn').disabled = disable;
            }
            if (!document.getElementById('ctl00_ContentPlaceHolder1_chkPartiularColumn').checked) {
                document.getElementById('ctl00_ContentPlaceHolder1_lstParticularColumn').disabled = false;
            }
        }
        function DisabledListboxForMonth(disable) {
            if (document.getElementById('ctl00_ContentPlaceHolder1_rdoAllMonth').checked) {
                document.getElementById('ctl00_ContentPlaceHolder1_lstMonth').selectedIndex = 0;
                document.getElementById('ctl00_ContentPlaceHolder1_lstMonth').disabled = disable;
            }
            if (!document.getElementById('ctl00_ContentPlaceHolder1_rdoAllMonth').checked) {
                document.getElementById('ctl00_ContentPlaceHolder1_lstMonth').disable = false;
            }
        }
        function EnabledListboxForMonth() {
            document.getElementById('ctl00_ContentPlaceHolder1_lstMonth').disabled = false;
        }
        function DateDiff() {

        }

    </script>
    <div id="divMsg" runat="server"></div>
</asp:Content>
