<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PayEmployeeBulkSalaryProcess.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_PayEmployeeBulkSalaryProcess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">EMPLOYEE BULK SALARY PROCESS</h3>
                        </div>

                        <div class="box-body">
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Which Month salary do you want to calculate</h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Label ID="lblerror" runat="server" SkinID="Errorlbl" />
                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note</h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>1) Please Dont close the browser when salary is processed</span></p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>2) Please click salary process button only once</span></p>
                                        </div>
                                    </div>

                                   <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                     <sup>* </sup>
                                                    <label>Month &amp; Year</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="ImaCalStartDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtMonthYear" runat="server" AutoPostBack="true" onblur="return checkdate(this);" OnTextChanged="txtMonthYear_TextChanged"
                                                        ToolTip="Enter Month and Year" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                    
                                                    <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtMonthYear">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvtxtStartDate" runat="server" ControlToValidate="txtMonthYear"
                                                        Display="None" ErrorMessage="Please Month &amp; Year in (MM/YYYY Format)" SetFocusOnError="True"
                                                        ValidationGroup="payroll">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                   <%-- <sup>* </sup>--%>
                                                    <label>College</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollege" runat="server" ToolTip="Select College" AppendDataBoundItems="true" data-select2-enable="true"
                                                    AutoPostBack="true" TabIndex="2" CssClass="form-control"  OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <%--<label>Staff</label>--%>
                                                    <label>Scheme/Staff</label>
                                                </div>
                                                <asp:DropDownList ID="ddlStaff" runat="server" ToolTip="Select Scheme/Staff" AppendDataBoundItems="true" AutoPostBack="true" data-select2-enable="true"
                                                    TabIndex="3" CssClass="form-control" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                             </div>
                                            
                                        </div>
                                     </div>  

                                                                                                                  
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnshow" runat="server" Text="Show" ValidationGroup="payroll"
                                    CssClass="btn btn-primary" OnClick="btnshow_Click" TabIndex="7"/>
                                <asp:ValidationSummary ID="vsSelection" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="payroll" />
                            </div>
                        <div class="col-12" id="div_BulkSalaryprocess" runat="server" visible="true">
                        <asp:Panel ID="pnlList" runat="server">
                            <asp:ListView ID="LstBulkSalaryProcess" runat="server" OnItemDataBound="LstBulkSalaryProcess_ItemDataBound">
                                <EmptyDataTemplate>
                                    <br />
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%!important">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                 <th>
                                                  <asp:CheckBox ID="chkAll" runat="server" onclick="totalAppointment(this)" />
                                                 </th>
                                                <th>Sr.No
                                                </th>
                                                <th>College Name
                                                </th>
                                                <th>Scheme
                                                </th>
                                                <th>Status
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
                                         <td  width="3%">
                                            <asp:CheckBox ID="chkidno" runat="server" AlternateText="Select  Employee " ToolTip='<%# Eval("COLLEGE_NO")%>' />
                                          </td>   
                                        <td>
                                            <%# Container.DataItemIndex + 1%>
                                        </td>
                                        <td>
                                     
                                            <asp:Label ID="lblcollegename" runat="server" Text='<%# Eval("COLLEGE_NAME")%>'></asp:Label>
                                            <asp:HiddenField  runat="server" ID="hdncollegeid" Value='<%# Eval("COLLEGE_NO")%>' />
                                        </td>
                                        <td>
                                          
                                             <asp:Label ID="lblstaffname" runat="server" Text='<%# Eval("STAFF")%>'></asp:Label>
                                            <asp:HiddenField ID="hdnstaffid" runat="server" Value='<%# Eval("STAFF_NO")%> ' />
                                        </td>
                                        <td>
                                         <asp:Label ID="lblsalarystatus" runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                             
                                        <asp:HiddenField ID="hdnstatus" runat="server" Value='<%# Eval("SAL_PROCESS_STATUS") %>' />
                                             <%--<asp:Label ID="Label1" runat="server" Text='<%# Eval("SAL_PROCESS_STATUS") %>'></asp:Label>--%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                        <%--<asp:ImageButton ID="imgbutExporttoexcel" runat="server" ToolTip="Export to excel"
                            ImageUrl="~/IMAGES/excel.jpeg" Height="50px" Width="50px" OnClick="imgbutExporttoexcel_Click" />--%>
                    </div>
                        <br />
                   
                             <div class="col-12 btn-footer">
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" >
                                    <ProgressTemplate>
                                        <asp:Image ID="imgmoney" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                                        Processing Salary .........................................
                                    </ProgressTemplate>
                                </asp:UpdateProgress>

                            </div>
                             <div class="col-12 btn-footer">
                                <asp:Button ID="butSalaryProcess" runat="server" Text="Process Salary" ValidationGroup="payroll"
                                    CssClass="btn btn-primary" TabIndex="7" OnClick="butSalaryProcess_Click" OnClientClick="javascript:ShowProgressBar()"/>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="payroll" />
                            </div>

                        <div class="col-12" id="div_BulkSalaryprocess1"  runat="server" visible="true">
                        <asp:Panel ID="Panel1" runat="server"  visible="true">
                            <asp:ListView ID="lstBulkSalaryProcess1" runat="server">
                                <EmptyDataTemplate>
                                    <br />
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%!important">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>College Name
                                                </th>
                                                <th>Scheme
                                                </th>
                                                <th>
                                                    Status
                                                </th>
                                                <th>Remark
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
                                        <td>
                                            <asp:Label ID="lblcollegename" runat="server" Text='<%# Eval("College")%>'></asp:Label>
                                        </td>
                                        <td>
                                             <asp:Label ID="lblstaffname" runat="server" Text='<%# Eval("Staff")%>'></asp:Label>
                                        </td>
                                        <td>
                                             <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                                        </td>
                                        <td>
                                         <asp:Label ID="lblsalarystatus" runat="server" Text='<%# Eval("Remark")%>'></asp:Label>
                                             <%--<asp:Label ID="Label1" runat="server" Text='<%# Eval("SAL_PROCESS_STATUS") %>'></asp:Label>--%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                        <%--<asp:ImageButton ID="imgbutExporttoexcel" runat="server" ToolTip="Export to excel"
                            ImageUrl="~/IMAGES/excel.jpeg" Height="50px" Width="50px" OnClick="imgbutExporttoexcel_Click" />--%>
                    </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
      
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<center>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2" align="center">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <asp:Image ID="imgmoney" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                            Processing Salary .........................................
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
            </tr>
        </table>
    </center>--%>

    <script type="text/javascript" language="javascript">
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
    </script>
     <script type="text/javascript" language="javascript">
         function totalAppointment(chkcomplaint) {
             var frm = document.forms[0];
             for (i = 0; i < document.forms[0].elements.length; i++) {
                 var e = frm.elements[i];
                 if (e.type == 'checkbox') {
                     if (chkcomplaint.checked == true)
                         e.checked = true;
                     else
                         e.checked = false;
                 }
             }
         }
    </script>
    <script type="text/javascript">
        function ShowProgressBar() {
            document.getElementById('UpdateProgress2').style.visibility = 'visible';
        }
  </script>

</asp:Content>


