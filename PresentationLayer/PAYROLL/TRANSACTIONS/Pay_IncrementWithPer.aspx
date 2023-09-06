<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_IncrementWithPer.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_IncrementWithPer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">SALARY INCREMENT ENTRY FORM</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlSelect" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Select Staff and Increment Month</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">


                                        <div class="form-group col-lg-3 col-md-6 col-12">

                                            <sup>* </sup>
                                            <label>College</label>


                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College" AppendDataBoundItems="true"
                                                AutoPostBack="true" TabIndex="1"
                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                                ValidationGroup="payroll" ErrorMessage="Please select College" SetFocusOnError="true"
                                                InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <%--<label>Staff :</label>--%>
                                            <label>Scheme/Staff</label>

                                            <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" CssClass="form-control" ToolTip="Select Staff" TabIndex="2"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Department :</label>

                                            <asp:DropDownList ID="ddldepartment" AppendDataBoundItems="true" runat="server" CssClass="form-control" ToolTip="Select Department" TabIndex="3"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Increment Month :</label>

                                            <asp:DropDownList ID="ddlIncMonth" AppendDataBoundItems="true" runat="server" CssClass="form-control" ToolTip="Select Increment Month" TabIndex="4"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlIncMonth_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>



                                    </div>
                                </div>

                            </asp:Panel>
                            <div class="text-center">
                                <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                            </div>
                            <asp:Panel ID="pnlIncrement" runat="server">
                                <div class="col-12">
                                    <asp:ListView ID="lvIncrement" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Employees For Increment" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                 <h5 class="heading">Note</h5>
                                                 <p><i class="fa fa-star" aria-hidden="true"></i><span>For salary increment please enter 'N' in Increment textbox.</span></p>
                                            </h6>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Idno
                                                        </th>
                                                        <th>Name
                                                        </th>
                                                        <th>Designation
                                                        </th>
                                                        <th>Old Basic
                                                        </th>
                                                        <th>Increment
                                                        </th>
                                                        <th>Increment %
                                                        </th>
                                                        <th>Increment Date
                                                        </th>
                                                        <th>New Basic
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td class="form_left_label" colspan="8" style="background-color: #FFFFAA">Scale :
                                            <%#Eval("scale")%>
                                                </td>
                                            </tr>
                                            <tr class="item">
                                                <td>
                                                    <%#Eval("IDNO")%>
                                                </td>
                                                <td>
                                                    <%#Eval("EMPNAME")%>
                                                </td>
                                                <td>
                                                    <%#Eval("SUBDESIG")%>
                                                </td>
                                                <td>

                                                    <input type="hidden" id="hidObasic" runat="server" value='<%#Eval("OBASIC")%>' />
                                                    <input type="hidden" id="hidNewbasic" runat="server" value='<%#Eval("BASIC")%>' />
                                                    <input type="hidden" id="hidCheck" runat="server" />
                                                    <input type="hidden" runat="server" id="hidscale" value='<%#Eval("scale")%>' />
                                                    <input type="hidden" runat="server" id="hidGradePay" value='<%#Eval("GRADEPAY")%>' />
                                                    <asp:TextBox ID="txtOldBasic" runat="server" MaxLength="6" Text='<%#Eval("OBASIC")%>'
                                                        ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" Enabled="false" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInc" runat="server" MaxLength="1" Text='<%#Eval("INCYN")%>' ToolTip='<%#Eval("IDNO")%>'
                                                        CssClass="form-control" TabIndex="4" onkeyup="return Increment(this);"  />

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIncPer" runat="server" MaxLength="4" Text='<%#Eval("INCPER")%>' ToolTip='<%#Eval("IDNO")%>'
                                                        CssClass="form-control" TabIndex="4" onkeyup="return IncrementPercentage(this);" />

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIncDate" runat="server" Text='<%#Eval("DOI","{0:dd/MM/yyyy}")%>'
                                                        ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" TabIndex="5" Enabled="false" />
                                                    <ajaxToolKit:MaskedEditExtender ID="metxtIncDate" runat="server" TargetControlID="txtIncDate"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                    </ajaxToolKit:MaskedEditExtender>

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNewBasic" MaxLength="6" runat="server" Text='<%#Eval("BASIC")%>'
                                                        ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" TabIndex="6" Enabled="false" />

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="4" OnClick="btnSub_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="4" />
                                </div>
                            </asp:Panel>
                            <%--  <asp:Panel ID="pnlIncrement" runat="server" Width="100%">
                                   
                                    <asp:ListView ID="lvIncrement" runat="server">
                                       <EmptyDataTemplate>
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Employees For Increment" />
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Change Of Increment</h5>
                                                </div>
                                                <table class="table table-bordered table-hover table-responsive">

                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th >Idno
                                                            </th>
                                                            <th >Name
                                                            </th>
                                                            <th >Designation
                                                            </th>
                                                            <%--<th width="20%">Department
                                                                    </th>
                                                            <th >Old Basic
                                                            </th>
                                                            <th >Inc
                                                            </th>
                                                            <th >Inc %
                                                            </th>
                                                            <th >Inc.Date
                                                            </th>
                                                            <th >New Basic
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                </table>
                                           
                                           
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td class="form_left_label" colspan="8" style="background-color: #FFFFAA">Scale :
                                            <%#Eval("scale")%>
                                                </td>
                                            </tr>

                                            <tr class="item">
                                                <td >
                                                    <%#Eval("IDNO")%>
                                                </td>
                                                <td >
                                                    <%#Eval("EMPNAME")%>
                                                </td>
                                                <td >
                                                    <%#Eval("SUBDESIG")%>
                                                </td>
                                                <%-- <td width="15%">
                                                            <%#Eval("SUBDEPTNO")%>
                                                        </td>
                                                <td >

                                                    <input type="hidden" id="hidObasic" runat="server" value='<%#Eval("OBASIC")%>' />
                                                    <input type="hidden" id="hidNewbasic" runat="server" value='<%#Eval("BASIC")%>' />
                                                    <input type="hidden" id="hidCheck" runat="server" />
                                                    <input type="hidden" runat="server" id="hidscale" value='<%#Eval("scale")%>' />
                                                    <input type="hidden" runat="server" id="hidGradePay" value='<%#Eval("GRADEPAY")%>' />
                                                    <asp:TextBox ID="txtOldBasic" runat="server" MaxLength="6" Text='<%#Eval("OBASIC")%>'
                                                        ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" Enabled="false" />
                                                </td>
                                                <td >
                                                    <asp:TextBox ID="txtInc" runat="server" MaxLength="1" Text='<%#Eval("INCYN")%>' ToolTip='<%#Eval("IDNO")%>'
                                                        CssClass="form-control" TabIndex="4" onkeyup="return Increment(this);" />

                                                </td>
                                                <td >
                                                    <asp:TextBox ID="txtIncPer" runat="server" MaxLength="4" Text='<%#Eval("INCPER")%>' ToolTip='<%#Eval("IDNO")%>'
                                                        CssClass="form-control" TabIndex="4" onkeyup="return IncrementPercentage(this);" />

                                                </td>
                                                <td >
                                                    <asp:TextBox ID="txtIncDate" runat="server" Text='<%#Eval("DOI","{0:dd/MM/yyyy}")%>'
                                                        ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" TabIndex="5" Enabled="false" />
                                                    <ajaxToolKit:MaskedEditExtender ID="metxtIncDate" runat="server" TargetControlID="txtIncDate"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                    </ajaxToolKit:MaskedEditExtender>

                                                </td>
                                                <td >
                                                    <asp:TextBox ID="txtNewBasic" MaxLength="6" runat="server" Text='<%#Eval("BASIC")%>'
                                                        ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" TabIndex="6" Enabled="false" />

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                    <div class="text-center">
                                        <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="4" OnClick="btnSub_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                            OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="4" />
                                    </div>

                                </asp:Panel>--%>
                        </div>

                    </div>

                </div>

            </div>











            <table cellpadding="0" cellspacing="0" width="100%">

                <%-- <tr>
                    <td class="vista_page_title_bar" valign="top" style="height: 30px">SALARY INCREMENT ENTRY FORM&nbsp;
                        <!-- Button used to launch the help (animation) -->
                        
                    </td>
                </tr>--%>
                <%--PAGE HELP--%>
                <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
                <tr>
                    <td>
                        <!-- "Wire frame" div used to transition from the button to the info panel -->
                        <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                            <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                AlternateText="Page Help" ToolTip="Page Help" Visible="false" />
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
                    <td>&nbsp
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 10px">
                        <%--<asp:Panel ID="pnlSelect" runat="server">--%>
                        <div style="text-align: left; width: 95%;">
                            <fieldset class="fieldsetPay">
                                <legend class="legendPay"></legend>
                                <br>
                                <table cellpadding="0" cellspacing="0" style="width: 100%;">

                                    <tr>
                                        <td class="form_left_label"><%--College :<span style="color: Red">*</span>
                                                <asp:DropDownList ID="ddlCollege" runat="server" Width="300px" AppendDataBoundItems="true"
                                                    AutoPostBack="true" TabIndex="1"
                                                    OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                                    ValidationGroup="payroll" ErrorMessage="Please select College" SetFocusOnError="true"
                                                    InitialValue="0" Display="None"></asp:RequiredFieldValidator>--%>

                                            <%--Staff :<span style="color: Red">*</span>
                                                <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" Width="300px" TabIndex="2"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                                </asp:DropDownList>--%>
                                            <%--<asp:RegularExpressionValidator ID="reStaff" runat="server" ControlToValidate="ddlStaff"
                                                    Display="None" ErrorMessage="Please Select Staff" ValidationGroup="payinc" InitialValue="0"></asp:RegularExpressionValidator>--%>
                                            <%--Increment Month :<span style="color: Red">*</span>
                                                <asp:DropDownList ID="ddlIncMonth" AppendDataBoundItems="true" runat="server" Width="150px" TabIndex="3"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlIncMonth_SelectedIndexChanged">
                                                </asp:DropDownList>--%>
                                            <%--  <asp:RegularExpressionValidator ID="reStaffIncrementMonth" runat="server" ControlToValidate="ddlIncMonth"
                                                    InitialValue="0" Display="None" ErrorMessage="Please Select  Increment Month"
                                                    ValidationGroup="payinc"></asp:RegularExpressionValidator>--%>
                                        </td>
                                    </tr>

                                </table>
                                <br />
                            </fieldset>
                        </div>
                        <%--</asp:Panel>--%>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 10px"></td>
                </tr>
                <tr>
                    <td align="center">&nbsp
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">&nbsp
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function Increment(me, vall) {
            debugger;
            if (me.value == "Y" || me.value == "N" || me.value == "y" || me.value == "n") {

                var txtinc = document.getElementById("" + me.id + "");
                var myArr = new Array();
                var myArrScale = new Array();
                var myString = new String();
                var myScale = new String();
                var adsvalue = new Array();
                var count = 0;
                myString = "" + me.id + "";
                myArr = myString.split("_");
                var index = myArr[3].substring(4, myArr[3].length);

                var hidscale = document.getElementById("ctl00_ContentPlaceHolder1_lvIncrement_ctrl" + index + "_hidscale");
                myScale = "" + hidscale.value + "";
                myArrScale = myScale.split("-");

                var txtoldbasic = document.getElementById("ctl00_ContentPlaceHolder1_lvIncrement_ctrl" + index + "_txtOldBasic");
                var txtnewbasic = document.getElementById("ctl00_ContentPlaceHolder1_lvIncrement_ctrl" + index + "_txtNewBasic");
                var txtIncper = document.getElementById("ctl00_ContentPlaceHolder1_lvIncrement_ctrl" + index + "_txtIncPer");
                var hidObasic = document.getElementById("ctl00_ContentPlaceHolder1_lvIncrement_ctrl" + index + "_hidObasic");
                var hidNewbasic = document.getElementById("ctl00_ContentPlaceHolder1_lvIncrement_ctrl" + index + "_hidNewbasic");
                var hidGradePay = document.getElementById("ctl00_ContentPlaceHolder1_lvIncrement_ctrl" + index + "_hidGradePay");
                // var lblscale = document.getElementById("ctl00_ContentPlaceHolder1_lblscale");
                var hidCheck = document.getElementById("ctl00_ContentPlaceHolder1_lvIncrement_ctrl" + index + "_hidCheck");

                if (me.value == "y") {
                    me.value = "Y"
                }

                if (me.value == "n") {
                    me.value = "N"
                }

                if (txtinc.value == "Y") {
                    if (!(hidCheck.value == me.id)) {
                        hidCheck.value = me.id;

                        if (hidGradePay.value == 0 || hidGradePay == 0.00 || hidGradePay == null || hidGradePay <= 0) {
                            for (i = 0; i <= myArrScale.length; i++) {
                                alert(hidGradePay.value);
                                //if((Number(txtnewbasic.value)>= Number(myArrScale[i])) && (Number(txtnewbasic.value)<= Number(myArrScale[i+2])))
                                //{ 
                                //   txtnewbasic.value=Number(txtnewbasic.value)+Number(myArrScale[i+1]);
                                // }
                                if ((Number(txtoldbasic.value) >= Number(myArrScale[i])) && (Number(txtoldbasic.value) < Number(myArrScale[i + 2]))) {
                                    txtnewbasic.value = Number(txtoldbasic.value) + Number(myArrScale[i + 1]);
                                }
                            }
                        }
                        else {
                            if (Number(txtIncper.value) == "") { Number(txtIncper.value) = "0"; }
                            var per = ((Number(txtnewbasic.value) + Number(hidGradePay.value)) * Number(txtIncper.value) / 100);
                            //txtnewbasic.value=Math.round(Number(txtnewbasic.value)+ Number(per));

                            txtnewbasic.value = Number(txtnewbasic.value) + Number(per);
                            var newbasic = txtnewbasic.value;

                            adsvalue = newbasic.split(".");
                            txtnewbasic.value = roundTens(adsvalue[0]);
                        }
                    }
                    // lblscale.innerText=hidscale.value;
                }
                else {
                    hidCheck.value = null;
                    hidCheck.value = "";
                    txtIncper.value = "0";
                    //lblscale.innerText=hidscale.value;
                    txtoldbasic.value = hidObasic.value;
                    txtnewbasic.value = hidNewbasic.value;

                }

            }
            else {
                alert("Please Enter  (Y or N) only");
                me.value = null;
                me.value = "";
                document.getElementById("" + me.id + "").focus();
                return false;
            }

        }



        function IncrementPercentage(me, vall) {
            debugger;

            if (isNaN(me.value)) {
                me.value = me.value.substring(0, (me.value.length) - 1);
                me.value = '';
                me.focus = true;
                alert("Only Numeric allowed !");
                return false;
            }


            if (me.value != "") {

                var myArr = new Array();
                var myArrScale = new Array();
                var myString = new String();
                var myScale = new String();
                var adsvalue = new Array();
                var count = 0;

                myString = "" + me.id + "";
                myArr = myString.split("_");
                var index = myArr[3].substring(4, myArr[3].length);

                var txtinc = document.getElementById("ctl00_ContentPlaceHolder1_lvIncrement_ctrl" + index + "_txtInc");

                if (txtinc.value == "Y" || txtinc.value == "N" || txtinc.value == "y" || txtinc.value == "n") {


                    // var txtinc = document.getElementById("" + me.id + "");
                    var hidscale = document.getElementById("ctl00_ContentPlaceHolder1_lvIncrement_ctrl" + index + "_hidscale");
                    myScale = "" + hidscale.value + "";
                    myArrScale = myScale.split("-");


                    var txtIncper = document.getElementById("ctl00_ContentPlaceHolder1_lvIncrement_ctrl" + index + "_txtIncPer");
                    var txtoldbasic = document.getElementById("ctl00_ContentPlaceHolder1_lvIncrement_ctrl" + index + "_txtOldBasic");
                    var txtnewbasic = document.getElementById("ctl00_ContentPlaceHolder1_lvIncrement_ctrl" + index + "_txtNewBasic");
                    var hidObasic = document.getElementById("ctl00_ContentPlaceHolder1_lvIncrement_ctrl" + index + "_hidObasic");
                    var hidNewbasic = document.getElementById("ctl00_ContentPlaceHolder1_lvIncrement_ctrl" + index + "_hidNewbasic");
                    var hidGradePay = document.getElementById("ctl00_ContentPlaceHolder1_lvIncrement_ctrl" + index + "_hidGradePay");
                    // var lblscale = document.getElementById("ctl00_ContentPlaceHolder1_lblscale");
                    var hidCheck = document.getElementById("ctl00_ContentPlaceHolder1_lvIncrement_ctrl" + index + "_hidCheck");


                    if (txtinc.value == "Y") {
                        //alert(hidCheck.value);
                        //if (!(hidCheck.value == txtinc.value)) {
                        //    hidCheck.value = txtinc.value;

                        if (hidGradePay.value == 0 || hidGradePay == 0.00 || hidGradePay == null || hidGradePay <= 0) {

                            for (i = 0; i <= myArrScale.length; i++) {

                                //if((Number(txtnewbasic.value)>= Number(myArrScale[i])) && (Number(txtnewbasic.value)<= Number(myArrScale[i+2])))
                                //{ 
                                //   txtnewbasic.value=Number(txtnewbasic.value)+Number(myArrScale[i+1]);
                                // }
                                if ((Number(txtoldbasic.value) >= Number(myArrScale[i])) && (Number(txtoldbasic.value) < Number(myArrScale[i + 2]))) {
                                    txtnewbasic.value = Number(txtoldbasic.value) + Number(myArrScale[i + 1]);
                                }
                            }
                        }
                        else {


                            // if (Number(txtIncper.value) == "") { Number(txtIncper.value) = "0"; }


                            var per = ((Number(hidObasic.value) + Number(hidGradePay.value)) * Number(txtIncper.value) / 100);

                            //txtnewbasic.value=Math.round(Number(txtnewbasic.value)+ Number(per));
                            txtnewbasic.value = Number(hidObasic.value) + Number(per);

                            var newbasic = txtnewbasic.value;
                            adsvalue = newbasic.split(".");
                            txtnewbasic.value = roundTens(adsvalue[0]);

                        }
                        //  }
                        // lblscale.innerText=hidscale.value;
                    }
                    else {
                        hidCheck.value = null;
                        hidCheck.value = "";
                        txtIncper.value = "0";
                        //lblscale.innerText=hidscale.value;
                        txtoldbasic.value = hidObasic.value;
                        txtnewbasic.value = hidNewbasic.value;
                    }

                }
                else {
                    alert("Please Enter greter than 0 amount");
                    me.value = null;
                    me.value = "";
                    document.getElementById("" + me.id + "").focus();
                    return false;
                }
            }
        }



        function roundTens(val) {
            var x = val;
            for (var i = 0; i < 9; i++) {
                if (x % 10 == 0) {
                    break;
                }
                else {
                    x = Number(x) + 1;
                }
            }
            return x;
        }

    </script>

</asp:Content>

