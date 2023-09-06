<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_DailyWages.aspx.cs"
    Inherits="PAYROLL_TRANSACTIONS_Pay_DailyWages" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">DAILY WAGES ENTRY FORM</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlSelect" runat="server">                                    
                                    <div class="col-12">
	                                    <div class="row">
		                                    <div class="col-12">
		                                    <div class="sub-heading">
				                                    <h5>Select Staff and Month</h5>
			                                    </div>
		                                    </div>
	                                    </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="row"> 
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College" ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                        </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Staff</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Month</label>
                                            </div>
                                            <asp:DropDownList ID="ddlIncMonth" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlIncMonth_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>                                     
                                        </div>
                                    </div>
                                    <div class="col-12">
                                            <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                            <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                        </div>                                     
                                        <asp:Panel ID="pnlIncrement" runat="server">
                                            <div class="col-12">
                                                <asp:ListView ID="lvIncrement" runat="server">
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Employees For Increment" CssClass="text-center mt-3"/>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                            <div class="sub-heading">
	                                                            <h5>Daily Wages</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Idno
                                                                        </th>
                                                                        <th>Name
                                                                        </th>
                                                                        <th>Designation
                                                                        </th>
                                                                        <th>Daily Amount
                                                                        </th>
                                                                        <th>Month Days
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
                                                        <%--  <tr class="item">
                                        <td class="form_left_label" colspan="7" style="background-color: #FFFFAA">
                                            Scale :
                                            <%#Eval("scale")%>
                                        </td>
                                    </tr>--%>
                                                        <%--<tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">--%>
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

                                                                <asp:TextBox ID="txtfixamt" runat="server" MaxLength="6" Text='<%#Eval("FIX_AMT")%>'
                                                                    ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" onkeyup="return Increment(this);" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMonth" runat="server" MaxLength="3" Text='<%#Eval("MONTH")%>'
                                                                    CssClass="form-control" onkeyup="return IncrementMonth(this);" />

                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBasic" runat="server" Text='<%#Eval("BASIC")%>'
                                                                    ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" Enabled="false" />


                                                            </td>
                                                            <td>
                                                                <%--    <asp:TextBox ID="txtNewBasic" MaxLength="6" runat="server" Text='<%#Eval("BASIC")%>'
                                                ToolTip='<%#Eval("IDNO")%>' Width="70px" Enabled="false" />--%>
                                           
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSub_Click" TabIndex="4"/>
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-warning" TabIndex="5"
                                                    OnClick="btnCancel_Click" />              
                                            </div>
                                        </asp:Panel>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function Increment(vall) {

            var st = vall.id.split("lvIncrement_ctrl");
            var i = st[1].split("_txtfixamt");
            var index = i[0];
            var fixamt = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + '_txtfixamt').value;
            var txtmonth = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + '_txtMonth').value;
            var taxbasic = (Number(fixamt) * Number(txtmonth));
            document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + '_txtBasic').value = taxbasic.toFixed(0);
        }




        //function roundTens(val)
        //{
        // var x = val;
        // for(var i = 0; i < 9; i++)
        // {
        //   if(x % 10 == 0)
        //   {
        //     break;
        //   }
        //   else
        //   {
        //     x=Number(x)+1;
        //   }            
        // }
        // return x;
        //}

    </script>

    <script type="text/javascript" language="javascript">
        function IncrementMonth(vall) {

            var st = vall.id.split("lvIncrement_ctrl");
            var i = st[1].split("_txtMonth");
            var index = i[0];
            var fixamt = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + '_txtfixamt').value;
            var txtmonth = document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + '_txtMonth').value;

            var taxbasic = (Number(fixamt) * Number(txtmonth));
            document.getElementById('ctl00_ContentPlaceHolder1_lvIncrement_ctrl' + index + '_txtBasic').value = taxbasic.toFixed(0);

        }

    </script>


</asp:Content>

