<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_Increment.aspx.cs" Inherits="PayRoll_Pay_Increment" %>

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
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>College</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College" AppendDataBoundItems="true" data-select2-enable="true"
                                                    AutoPostBack="true" TabIndex="1"
                                                    OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                                    ValidationGroup="payroll" ErrorMessage="Please select College" SetFocusOnError="true"
                                                    InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <%--<label>Staff</label>--%>
                                                    <label>Scheme/Staff</label>
                                                </div>
                                                <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" CssClass="form-control" ToolTip="Select Staff" TabIndex="2" data-select2-enable="true"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Increment Month</label>
                                                </div>
                                                <asp:DropDownList ID="ddlIncMonth" AppendDataBoundItems="true" runat="server" CssClass="form-control" ToolTip="Select Increment Month" TabIndex="3" data-select2-enable="true"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlIncMonth_SelectedIndexChanged" >
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                </asp:Panel>
                            <div class="col-12 btn-footer">
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
                                                    <h5>Change Of Increment</h5>
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
                                                            <th>Inc.Date
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
                                                    <td class="form_left_label" colspan="7" style="background-color: #FFFFAA">Scale :
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
                                                            CssClass="form-control" TabIndex="4" onkeyup="return Increment(this);" />

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
                        </div>
                    </div>
                </div>
            </div>

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

                        //if (hidGradePay.value == 0 || hidGradePay == 0.00 || hidGradePay == null || hidGradePay <= 0) {
                        //    for (i = 0; i <= myArrScale.length; i++) {
                        //        //if((Number(txtnewbasic.value)>= Number(myArrScale[i])) && (Number(txtnewbasic.value)<= Number(myArrScale[i+2])))
                        //        //{ 
                        //        //   txtnewbasic.value=Number(txtnewbasic.value)+Number(myArrScale[i+1]);
                        //        // }
                        //        alert('Scale');
                        //        if ((Number(txtoldbasic.value) >= Number(myArrScale[i])) && (Number(txtoldbasic.value) < Number(myArrScale[i + 2]))) {
                        //            txtnewbasic.value = Number(txtoldbasic.value) + Number(myArrScale[i + 1]);
                        //        }
                        //    }
                        //}
                        //else {
                           
                            var per = ((Number(txtnewbasic.value) + Number(hidGradePay.value)) * 3 / 100);
                            //txtnewbasic.value=Math.round(Number(txtnewbasic.value)+ Number(per));
                            txtnewbasic.value = Number(txtnewbasic.value) + Number(per);
                            var newbasic = txtnewbasic.value;
                            adsvalue = newbasic.split(".");
                            txtnewbasic.value = roundTens(adsvalue[0]);
                        //}
                    }
                    // lblscale.innerText=hidscale.value;
                }
                else {
                    hidCheck.value = null;
                    hidCheck.value = "";
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
