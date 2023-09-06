<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BoardingPassGeneration.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_BoardingPassGeneration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .numeric_button {
            background-color: #384B69;
            color: #FFFFFF;
            font-family: Arial;
            font-weight: bold;
            padding: 2px;
            border: none;
        }

        .current_page {
            background-color: #09151F;
            color: #FFFFFF;
            font-family: Arial;
            font-weight: bold;
            padding: 2px;
        }

        .next_button {
            background-color: #1F3548;
            color: #FFFFFF;
            font-family: Arial;
            font-weight: bold;
            padding: 2px;
        }

        .style1 {
            height: 28px;
        }
    </style>
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">BOARDING PASS GENERATION </h3>
                </div>
                <div>
                    <form role="form">
                        <div class="box-body">
                            <div class="col-md-12">
                                Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                <asp:Panel ID="pnlSelect" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel panel-heading">Selection Criteria</div>
                                        <div class="panel panel-body">
                                            <div class="form-group col-md-3">
                                                <br />
                                                <asp:RadioButtonList ID="rdblist" runat="server" RepeatDirection="Horizontal" ToolTip="Select Students or Employee"
                                                    OnSelectedIndexChanged="rdblist_SelectedIndexChanged" AutoPostBack="true" TabIndex="1">
                                                    <asp:ListItem Selected="True" Value="1">Students&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="2">Employee</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="form-group col-md-4" id="trSchool" runat="server" visible="false">
                                                <label><span style="color: #FF0000">*</span>School :</label>
                                                <asp:DropDownList ID="ddlSchool" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                    AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged" ToolTip="Select School">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvVehicle" runat="server" ErrorMessage="Please Select School."
                                                    ValidationGroup="Show" InitialValue="0" ControlToValidate="ddlSchool" Display="Dynamic" Text="*">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-4" id="trDept" runat="server" visible="false">
                                                <label><span style="color: #FF0000">*</span>Department :</label>
                                                <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                    TabIndex="3" ToolTip="Select Department">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvRoute" runat="server" ErrorMessage="Please Select Department."
                                                    ValidationGroup="Show" InitialValue="0" ControlToValidate="ddlDept" Display="Dynamic" Text="*">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-4" id="trDegree" runat="server" visible="false">
                                                <label><span style="color: #FF0000">*</span>Degree :</label>
                                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control" AutoPostBack="True"
                                                    TabIndex="4" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" ToolTip="Select Degree">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Degree."
                                                    ValidationGroup="Show" InitialValue="0" ControlToValidate="ddlDegree" Display="Dynamic" Text="*">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-4" id="trBranch" runat="server" visible="false">
                                                <label><span style="color: #FF0000">*</span>Branch :</label>
                                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                    TabIndex="5" ToolTip="Select Branch">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Branch."
                                                    ValidationGroup="Show" InitialValue="0" ControlToValidate="ddlBranch" Display="Dynamic" Text="*">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <br />
                                            <br />
                                            <div class="form-group col-md-12">
                                                <p class="text-center">
                                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="Show"
                                                        OnClick="btnShow_Click" TabIndex="6" ToolTip="Click here to Show Details" />&nbsp;&nbsp;
                                                   <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click"
                                                       TabIndex="7" ToolTip="Click here to Reset" />
                                                    &nbsp;&nbsp;
                                                  <asp:Button ID="btnReport" runat="server" Visible="false" Text="Report" CssClass="btn btn-info" OnClick="btnReport_Click"
                                                      TabIndex="8" ToolTip="Click here to Show Report" />
                                                    <asp:ValidationSummary ID="VS1" runat="server" ShowMessageBox="true"
                                                        ShowSummary="false" DisplayMode="List" ValidationGroup="Show" HeaderText="Following Fields are mandatory" />
                                                    <asp:HiddenField ID="hdfTot" runat="server" />
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlDate" runat="server" Visible="false">
                                    <div class="panel panel-info">
                                        <div class="panel panel-heading">Boarding Details</div>
                                        <div class="panel panel-body">
                                            <div class="form-group col-md-4">
                                                <label>Date of Expiry</label>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <asp:Image ID="img3" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    </div>
                                                    <asp:TextBox ID="txtExpiry" runat="server" CssClass="form-control" ToolTip="Enter Date Of Expiry"
                                                        ValidationGroup="Submit" TabIndex="9" Style="z-index: 0;"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="img3" TargetControlID="txtExpiry">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                        ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                        OnInvalidCssClass="errordate" TargetControlID="txtExpiry" ClearMaskOnLostFocus="true" />
                                                    <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt" ControlToValidate="txtExpiry"
                                                        IsValidEmpty="false" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Text="*"
                                                        InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None" ValidationGroup="Submit" />
                                                </div>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label>Date of Allotment :</label>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    </div>
                                                    <asp:TextBox ID="txtAllot" runat="server" CssClass="form-control" ToolTip="Enter Date Of Allotment"
                                                        ValidationGroup="Submit" TabIndex="10" Style="z-index: 0;"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtAllot">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                        ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                        OnInvalidCssClass="errordate" TargetControlID="txtAllot" ClearMaskOnLostFocus="true" />
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="medt" ControlToValidate="txtAllot"
                                                        IsValidEmpty="false" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None" Text="*"
                                                        InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="Submit" />
                                                </div>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label>Approved By :</label>
                                                <asp:TextBox ID="txtApproved" runat="server" TabIndex="11" CssClass="form-control" ToolTip="Approved By"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <p class="text-center">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="Submit"
                                                        OnClick="btnSubmit_Click" TabIndex="12" ToolTip="Click here to Submit" />&nbsp;&nbsp;                                           
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                        ShowSummary="false" DisplayMode="List" ValidationGroup="Submit" HeaderText="Following Fields are mandatory" />
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="box-footer">                  
                    <div class="col-md-12">
                        <asp:Panel ID="pnllist" runat="server" ScrollBars="Auto">
                            <asp:ListView ID="lvList" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <h4 class="box-title">List Of Selection
                                        </h4>
                                        <table class="table table-bordered table-hover">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>SELECT
                                                        <asp:CheckBox ID="chkAll" runat="server" Checked="true" onclick="totAllSubjects(this)" />
                                                    </th>
                                                    <th>NAME</th>
                                                    <th>
                                                        <asp:Label ID="lblFields" runat="server" Text=""></asp:Label>
                                                    </th>
                                                    <th>FEE PAID</th>
                                                    <th>BOARDING ALLOTTED</th>
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
                                            <asp:CheckBox ID="chkAccept" runat="server" Checked="true" ToolTip='<%# Eval("IDNO")%>' />
                                        </td>
                                        <td><%# Eval("NAME") %></td>
                                        <td><%# Eval("REGNO") %></td>
                                        <td><%# (Eval("TRANSPORT").ToString()) == "1" ? "YES" : "NO" %></td>
                                        <td><%# Eval("STATUS") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <div class="vista-grid_datapager">
                                <div class="text-center">
                                    <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvList" PageSize="20"
                                        OnPreRender="dpPager_PreRender">
                                        <Fields>
                                            <asp:NumericPagerField ButtonCount="3" ButtonType="Link" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <%--<td class="vista_page_title_bar" style="height: 30px">
                             
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>--%>
        </tr>
        <%--  Shrink the info panel out of view --%>        <%--  Reset the sample so it can be played again --%>
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
                            Edit Record&nbsp;&nbsp;
                            <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                            Delete Record
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


    <script type="text/javascript">
        function totAllSubjects(headchk) {
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (e.name.endsWith('chkAccept')) {
                        if (headchk.checked == true) {
                            e.checked = true;
                            hdfTot.value = Number(hdfTot.value) + 1;
                        }
                        else
                            e.checked = false;

                    }
                }
            }

            if (headchk.checked == false) hdfTot.value = "0";
        }

        function validateAssign() {
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>').value;

            if (hdfTot == 0) {
                alert('Please Select Atleast One User from User List');
                return false;
            }
            else
                return true;
        }

    </script>

    <%--   <asp:UpdatePanel ID="updActivity" runat="server">
        

    </asp:UpdatePanel>--%>
</asp:Content>
