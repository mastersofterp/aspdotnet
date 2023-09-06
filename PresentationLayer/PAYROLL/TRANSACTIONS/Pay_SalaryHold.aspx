<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_SalaryHold.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_SalaryHold" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SALARY HOLD ENTRY</h3>
                            <p class="text-center">
                            </p>
                            <div class="box-tools pull-right">
                                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                    AlternateText="Page Help" ToolTip="Page Help" />
                            </div>
                        </div>

                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
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

                                </div>
                                <div class="col-md-12">
                                    <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br />
                                    </h5>
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Select Staff</div>
                                        <div class="panel-body">
                                            <div style="display:none">
                                                <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                                            <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>

                                            </div>
                                            <asp:Panel ID="pnlSelect" runat="server">

                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-6">
                                                        <div class="form-group col-md-10">
                                                            <label>College :<span style="color: Red">*</span></label>

                                                            <%--    AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"--%>
                                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" TabIndex="1"
                                                                ToolTip="Select College" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege" ValidationGroup="payroll"
                                                                ErrorMessage="Please select College" SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-10">
                                                            <label>Staff :<span style="color: Red">*</span></label>
                                                            <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" CssClass="form-control">
                                                                <%--OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged" AutoPostBack="true"--%>
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                                                Display="None" ErrorMessage="Select Staff" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                                            <asp:CheckBox ID="chkIdno" runat="server" Text="Order By Idno" AutoPostBack="true"
                                                                OnCheckedChanged="chkIdno_CheckedChanged" />
                                                            <asp:CheckBox ID="chkAbsent" runat="server" Text="Show Hold Employees Only" AutoPostBack="true"
                                                                OnCheckedChanged="chkAbsent_CheckedChanged" />
                                                        </div>
                                                        <div class="form-group col-md-10">

                                                            <label>Month :<span style="color: Red">*</span></label>
                                                            <asp:DropDownList ID="ddlMonth" AppendDataBoundItems="true" runat="server" CssClass="form-control"
                                                                OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true">
                                                                <%--OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged"--%>
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvMonth" runat="server" ControlToValidate="ddlMonth"
                                                                Display="None" ErrorMessage="Select Month" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>

                                                        </div>
                                                      
                                                    </div>
                                                </div>
                                          
                                                    <div class="form-group col-md-12">
                                                        <asp:Panel ID="pnlNote" runat="server" Style="padding-left: 10px;" Width="90%">
                                                            <table class="table table-responsive">
                                                                <tr>
                                                                    <td style="font-family: Verdana; font-size: 12px; color: #fff; background-color: #006595;">Note :-
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td id="tdAbsentDays" runat="server" style="font-family: Verdana; font-size: 12px; color: #fff; background-color: #006595;">*1) Check Hold/Release column to Hold Salary *2) Uncheck Hold/Release column to release salary
                                                                only 0 (or) Blank
                                                                    </td>
                                                                    <td id="tdPresentDays" runat="server" style="font-family: Verdana; font-size: 12px; color: #fff; background-color: #006595;">*1) Enter Present Day's in Day's Column *2) If Present in whole month then enter
                                                                only 0 (or) Blank
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <asp:Panel ID="pnlAttendance" runat="server" Style="padding-left: 10px;" Width="90%">

                                                            <asp:ListView ID="lvAttendance" runat="server">
                                                                <EmptyDataTemplate>
                                                                    <br />
                                                                    <center>
                                                                              <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" /></center>
                                                                </EmptyDataTemplate>
                                                                <LayoutTemplate>
                                                                    <div class="vista-grid">
                                                                        <div class="titlebar">
                                                                            <h4>Salary Hold/Release  Entry</h4>


                                                                        </div>
                                                                        <table class="table table-bordered table-hover table-responsive">

                                                                            <thead>
                                                                                <tr class="bg-light-blue">
                                                                                    <th width="5%">Idno
                                                                                    </th>
                                                                                    <th width="25%">Name
                                                                                    </th>
                                                                                    <th width="20%">Designation
                                                                                    </th>
                                                                                    <th width="15%">Hold/Release
                                                                                    </th>
                                                                                </tr>
                                                                                <thead>
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
                                                                    <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                                        <td width="5%">
                                                                            <%#Eval("IDNO")%>
                                                                        </td>
                                                                        <td width="25%">
                                                                            <%#Eval("EMPNAME")%>
                                                                        </td>
                                                                        <td width="20%">
                                                                            <%#Eval("SUBDESIG")%>
                                                                        </td>
                                                                        <td width="15%">
                                                                            <asp:CheckBox ID="chkHoldSalary" runat="server" onclick="CountSelection();" Font-Bold="true" ForeColor="Green"
                                                                                Text='<%# (Convert.ToInt32(Eval("HOLDSALARY") )== 1 ?  "HOLD" : "" )%>'
                                                                                Checked='<%# (Convert.ToInt32(Eval("HOLDSALARY") )== 0 ?  false : true )%>'
                                                                                ToolTip='<%#Eval("IDNO")%>' />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                                        <td width="5%">
                                                                            <%#Eval("IDNO")%>
                                                                        </td>
                                                                        <td width="25%">
                                                                            <%#Eval("EMPNAME")%>
                                                                        </td>
                                                                        <td width="20%">
                                                                            <%#Eval("SUBDESIG")%>
                                                                        </td>
                                                                        <td width="15%">
                                                                            <asp:CheckBox ID="chkHoldSalary" runat="server" onclick="CountSelection();" Font-Bold="true" ForeColor="Green"
                                                                                Text='<%# (Convert.ToInt32(Eval("HOLDSALARY") )== 1 ?  "HOLD" : "" )%>'
                                                                                Checked='<%# (Convert.ToInt32(Eval("HOLDSALARY") )== 0 ?  false : true )%>'
                                                                                ToolTip='<%#Eval("IDNO")%>' />
                                                                        </td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                            </asp:ListView>
                                                            <br />
                                                            <center>
                                                                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll" class="btn btn-primary"
                                                                                OnClick="btnSub_Click" />&nbsp;
                                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                                                                OnClick="btnCancel_Click" class="btn btn-primary" />
                                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                                        </center>
                                                        </asp:Panel>
                                                    </div>

                                               
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>




        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function check(me) {
            if (ValidateNumeric(me) == true) {
                var myArr = new Array();
                var myArrdays = new Array();
                myString = "" + me.id + "";
                myArr = myString.split("_");
                var index = myArr[3].substring(4, myArr[3].length);
                var Attenddays = document.getElementById("ctl00_ContentPlaceHolder1_lvAttendance_ctrl" + index + "_txtDays");
                var Attend_days = Attenddays.value;
                myArrdays = Attend_days.split(".");

                if (!(Attend_days > 31)) {
                    if (myArrdays[1] > 0) {
                        if (myArrdays[1] > 5 || myArrdays[1] < 5) {
                            alert("Please enter 5 only after decimal");
                            document.getElementById("ctl00_ContentPlaceHolder1_lvAttendance_ctrl" + index + "_txtDays").value = "";
                            document.getElementById("ctl00_ContentPlaceHolder1_lvAttendance_ctrl" + index + "_txtDays").focus();
                        }
                    }
                }
                else {
                    alert("Please enter days less than 32");
                    me.value = "";
                    me.focus();
                }
            }
        }


        function ValidateNumeric(txt) {


            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = "";
                txt.focus();
                alert("Only Numeric Characters alloewd");
                return false;
            }
            else {
                return true;
            }
        }

    </script>
</asp:Content>

