<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="pay_modify_salary.aspx.cs" Inherits="PayRoll_pay_modify_salary" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">CHANGES IN MONTHLY SALARY FILE</h3>
                        </div>

                       <div class="box-body">
                            <asp:Panel ID="pnlSelect" runat="server">
                                <div class="col-12">
	                                <div class="row">
		                                <div class="col-12">
		                                <div class="sub-heading">
				                                <h5>Select Month & Year,PayHead and Staff</h5>
			                                </div>
		                                </div>
	                                </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <sup>* </sup>
									            <label>Month Year</label>
								            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="ImaCalStartDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtMonthYear" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Enter Month Year" onblur="return checkdate(this);"></asp:TextBox>        
                                                <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtMonthYear">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvtxtStartDate" runat="server" ControlToValidate="txtMonthYear"
                                                    Display="None" ErrorMessage="Please select Month And Year in (MM/YYYY Format)"
                                                    SetFocusOnError="True" ValidationGroup="payroll">
                                                </asp:RequiredFieldValidator>
                                            </div>
							            </div>                                                      
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <sup>* </sup>
									            <label>PayHead</label>
								            </div>
                                                <asp:DropDownList ID="ddlPayhead" AppendDataBoundItems="true" runat="server"
                                                CssClass="form-control" TabIndex="2" ToolTip="Select PayHead" AutoPostBack="True" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlPayhead_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvPayhead" runat="server" ControlToValidate="ddlPayhead"
                                                Display="None" ErrorMessage="Please Select PayHead" ValidationGroup="payroll"
                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
							            </div>                        
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <sup>* </sup>
									            <label>College</label>
								            </div>
                                            <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Select College" data-select2-enable="true"
                                                AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College" ValidationGroup="payroll" InitialValue="0"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
							            </div>                        
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <sup>* </sup>
									            <%--<label>Scheme</label>--%>
                                                <label>Scheme/Staff</label>
								            </div>
                                                <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="4" ToolTip="Select Staff" data-select2-enable="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                                Display="None" ErrorMessage="Please Select Scheme/Staff" ValidationGroup="payroll" InitialValue="0"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
							            </div>                      
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <%--<label>Staff</label>--%>
                                                <label>Employee Type</label>
								            </div>
                                                <asp:DropDownList ID="ddlEmployeeType" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" TabIndex="5" ToolTip="Select Staff">
                                        </asp:DropDownList>
							            </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <sup>* </sup>
									            <label>Order By</label>
								            </div>
                                                <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="6" ToolTip="Select Order By" data-select2-enable="true"
                                                AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">IDNO</asp:ListItem>
                                                <asp:ListItem Value="2">SEQUENCE NO</asp:ListItem>
                                                    <asp:ListItem Value="3">Employee Code</asp:ListItem>
                                                <asp:ListItem Value="4">Name</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlorderby"
                                                Display="None" ErrorMessage="Select Order" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
							            </div>
                                    </div>                                                    
                                </div>
                                <div class="col-12 text-center">
                                    <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="payroll" CssClass="btn btn-primary" ToolTip="Click To Show" TabIndex="7"
                                        OnClick="btnShow_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="payroll"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlMonthlyChanges" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <label>Total Employees</label>
								            </div>
                                            <asp:TextBox ID="txtEmpoyeeCount" BackColor="#006595" ForeColor="White" runat="server"
                                    CssClass="from-control" BorderStyle="None"></asp:TextBox>
                                            <asp:TextBox ID="txtPayheadName" BackColor="#006595" ForeColor="White" runat="server"
                                                BorderStyle="None" CssClass="from-control"></asp:TextBox>
                                            <asp:TextBox ID="txtAmount" BackColor="#006595" ForeColor="White" runat="server"
                                                CssClass="from-control" BorderStyle="None"></asp:TextBox>
                                            <asp:HiddenField ID="hidPayhead" runat="server" />
							            </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <asp:ListView ID="lvMonthlyChanges" runat="server">
                                        <EmptyDataTemplate>
                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" CssClass="text-center mt-3" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
	                                            <h5>Monthly Changes</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Srno
                                                            </th>
                                                            <%-- <th width="5%">Idno
                                                            </th>--%>
                                                                <th>Employee Code
                                                            </th>
                                                            <th>Name
                                                            </th>
                                                            <th>Designation
                                                            </th>
                                                            <th>DepartMent
                                                            </th>
                                                            <th>Basic
                                                            </th>
                                                            <th>Amount
                                                            </th>
                                                        </tr>
                                                        </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>                                                       
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align:center">
                                                    <%# Container.DataItemIndex + 1%>
                                                </td>
                                                <%-- <td width="5%">
                                                    <%#Eval("IDNO")%>
                                                </td>--%>

                                                    <td>
                                                    <%#Eval("PFILENO")%>
                                                </td>
                                                <td style="text-align:left">
                                                    <%#Eval("NAME")%>
                                                </td>
                                                <td>
                                                    <%#Eval("subdesig")%>
                                                </td>
                                                <td>
                                                    <%#Eval("subdept")%>
                                                </td>
                                                <td>
                                                    <%#Eval("Basic")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDays" runat="server" MaxLength="10" Text='<%#Eval("AMOUNT")%>'
                                                        ToolTip='<%#Eval("IDNO")%>' CssClass="form-control" TabIndex="6" onkeyup="return check(this);" onfocus="Check_Click(this)" />

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                                <div class="col-12">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll" CssClass="btn btn-primary"
                                        OnClick="btnSub_Click" OnClientClick="return ConfirmMessage();" TabIndex="8" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="9"/>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
            <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger  ControlID="ddlStaff"/>
            <asp:PostBackTrigger  ControlID="ddlCollege"/>
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function check(me) {


            //           //alert(me.id);
            //           //alert(document.getElementById("ctl00_ContentPlaceHolder1_lblEmployeeCount").value);
            //        
            //            var myArr = new Array();
            //            var myArrdays = new Array();
            //            
            //            var count=0.00;
            //            
            //            myString = ""+me.id+"";
            //            myArr = myString.split("_");
            //            var index= myArr[3].substring(4,myArr[3].length);
            //            var  Attenddays= document.getElementById("ctl00_ContentPlaceHolder1_lvMonthlyChanges_ctrl"+index+"_txtDays");
            //            var Attend_days= Attenddays.value;
            //            myArrdays  = Attend_days.split(".");
            //            if(myArrdays[1] > 5)
            //            {               
            //               alert("Please Enter 5 only after decimal");
            //               document.getElementById("ctl00_ContentPlaceHolder1_lvMonthlyChanges_ctrl"+index+"_txtDays").value="";
            //               document.getElementById("ctl00_ContentPlaceHolder1_lvMonthlyChanges_ctrl"+index+"_txtDays").focus();
            //            }
            if (ValidateNumeric(me) == true) {
                var count = 0.00;
                for (i = 0; i <= Number(document.getElementById("ctl00_ContentPlaceHolder1_txtEmpoyeeCount").value) - 1; i++) {

                    count = Number(count) + Number(document.getElementById("ctl00_ContentPlaceHolder1_lvMonthlyChanges_ctrl" + i + "_txtDays").value);

                }

                document.getElementById("ctl00_ContentPlaceHolder1_txtAmount").value = count;
            }


            // alert(count);

        }


        function Showclick() {
            var count = 0.00;
            for (i = 0; i <= Number(document.getElementById("ctl00_ContentPlaceHolder1_txtEmpoyeeCount").value) - 1; i++) {

                count = Number(count) + Number(document.getElementById("ctl00_ContentPlaceHolder1_lvMonthlyChanges_ctrl" + i + "_txtDays").value);

            }

            document.getElementById("ctl00_ContentPlaceHolder1_txtAmount").value = count;

        }


        function ConfirmMessage() {
            var payHead = document.getElementById("ctl00_ContentPlaceHolder1_ddlPayhead").value;
            var hidPayhead = document.getElementById("ctl00_ContentPlaceHolder1_hidPayhead").value;

            if (confirm("Do you want to save changes in " + hidPayhead + "->" + payHead)) {
                return true;
            }
            else {
                return false;
            }
        }


        function checkdate(input) {
            var validformat = /^\d{2}\/\d{4}$/ //Basic check for format validity
            var returnval = false
            if (!validformat.test(input.value)) {
                alert("Invalid Date Format. Please Enter in MM/YYYY Formate")
                document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").value = "";
                document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").focus();
            }
            else {
                var monthfield = input.value.split("/")[0]

                if (monthfield > 12 || monthfield <= 0) {
                    alert("Month Should be greate than 0 and less than 13");
                    document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").value = "";
                    document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").focus();
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

        function Check_Click(objRef) {

            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            if (objRef.focus) {
                for (var i = 0; i < inputList.length; i++) {

                    if (inputList[i].parentNode.parentNode == row) {

                        //If focused change color                  
                        row.style.backgroundColor = "#CCCC88";
                    }
                    else {
                        //inputList[i].parentNode.parentNode.style.backgroundColor = "White";

                        if (inputList[i].parentNode.parentNode.rowIndex % 2 == 0) {

                            //Alternating Row Color

                            inputList[i].parentNode.parentNode.style.backgroundColor = "White";
                        }

                        else {
                            inputList[i].parentNode.parentNode.style.backgroundColor = "#ffffd2";

                        }
                    }
                }
            }
        }

    </script>

</asp:Content>
