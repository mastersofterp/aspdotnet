<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="FetchDetailRegistration.aspx.cs" Inherits="FetchDetailRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
#ctl00_ContentPlaceHolder1_pnlCourse .dataTables_scrollHeadInner {
width: max-content !important;
}
</style>
    <asp:UpdatePanel ID="updCollege1" runat="server"></asp:UpdatePanel>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Online Applied Students</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-4 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Admission Batch</label>
                                </div>
                                <asp:DropDownList ID="ddlAdmbatch" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAdmBatch"
                                            Display="None" ValidationGroup="stud" InitialValue="0"
                                            ErrorMessage="Please Select Admission Batch" Visible="true"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlAdmBatch"
                                            Display="None" ValidationGroup="summary" InitialValue="0"
                                            ErrorMessage="Please Select Admission Batch." Visible="true"></asp:RequiredFieldValidator>
                            </div>

                            <%--<div class="form-group col-lg-4 col-md-4 col-12" style="display: none">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Select Program</label>
                                </div>
                                <asp:RadioButtonList ID="rdbDegree" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                    <asp:ListItem Value="1" Selected="True"> Degree Wise &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="2"> Branch Wise</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>--%>

                            <%--<div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Select Filter</label>
                                </div>
                                <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                    AppendDataBoundItems="True" AutoPostBack="True">
                                    <asp:ListItem>Please Select</asp:ListItem>
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                    AppendDataBoundItems="True" Visible="false" AutoPostBack="True">
                                    <asp:ListItem>Please Select</asp:ListItem>
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                            </div>--%>

                            <%--<div class="form-group col-lg-4 col-md-6 col-12" style="display: none">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>As On Date</label>
                                </div>
                                <div class="input-group">
                                    <div class="input-group-addon" id="ImaCaltxtDate">
                                        <span class="fa fa-calendar text-blue"></span>
                                    </div>
                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"></asp:TextBox>

                                       &nbsp;<asp:Image ID="ImaCaltxtDate" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Style="cursor: pointer" />
                                    <ajaxToolKit:CalendarExtender ID="cetxtDate" runat="server" Enabled="true"
                                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="ImaCaltxtDate" TargetControlID="txtDate">
                                    </ajaxToolKit:CalendarExtender>
                                    <ajaxToolKit:MaskedEditExtender ID="metxtDate" runat="server" AcceptNegative="Left"
                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                        MessageValidatorTip="true" TargetControlID="txtDate">
                                    </ajaxToolKit:MaskedEditExtender>
                                    <ajaxToolKit:MaskedEditValidator ID="mevtxtDate" runat="server" ControlExtender="metxtDate"
                                        ControlToValidate="txtDate" Display="None" EmptyValueBlurredText="Empty"
                                        EmptyValueMessage="Please Select Date" InvalidValueBlurredMessage="Invalid Date"
                                        InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="True"
                                        TooltipMessage="Please Enter Date" ValidationGroup="stud" />
                                </div>
                            </div>--%>
                            <div class="form-group col-lg-4 col-md-4 col-12">

                                <div class="label-dynamic">
                                   <%-- <sup>* </sup>--%>
                                    <label>Programme Type</label>
                                </div>
                                <asp:DropDownList ID="ddlProgrammeType" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">UG</asp:ListItem>
                                    <asp:ListItem Value="2">PG</asp:ListItem>
                                </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlProgrammeType"
                                            Display="None" ValidationGroup="stud" InitialValue="0"
                                            ErrorMessage="Please Select  Programme Type."></asp:RequiredFieldValidator>--%>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Visible="false" ControlToValidate="ddlProgrammeType"
                                            Display="None" ValidationGroup="studData" InitialValue="0"
                                            ErrorMessage="Please Select  Programme Type."></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlProgrammeType"
                                            Display="None" ValidationGroup="summary" InitialValue="0"
                                            ErrorMessage="Please Select  Programme Type."></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group col-lg-4 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <%--<sup>* </sup>--%>
                                    <label>Degree</label>
                                </div>
                                <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="3" AppendDataBoundItems="true"  CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                 <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Visible="false" ControlToValidate="ddlProgrammeCode"
                                            Display="None" ValidationGroup="stud" InitialValue="0"
                                            ErrorMessage="Please Select  Programme Code."></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="6" CssClass="form-control" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                            AppendDataBoundItems="True" Visible="false" AutoPostBack="True">
                                            <asp:ListItem>Please Select</asp:ListItem>
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Visible="false" ControlToValidate="ddlBranch"
                                            Display="None" ValidationGroup="stud" InitialValue="0"
                                            ErrorMessage="Please Select Programme Code."></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCollege" runat="server" TabIndex="7" CssClass="form-control" AppendDataBoundItems="true" Visible="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Visible="false" ControlToValidate="ddlCollege"
                                            Display="None" ValidationGroup="stud" InitialValue="0"
                                            ErrorMessage="Please Select Programme Code."></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ValidationGroup="studData" InitialValue="0"
                                            ErrorMessage="Please Select Programme Code."></asp:RequiredFieldValidator>--%>
                            </div>
                              <div class="form-group col-lg-4 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <%--<sup>* </sup>--%>
                                    <label>Branch</label>
                                </div>
                                <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="4" AppendDataBoundItems="true" AutoPostBack="true"  CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                  </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Application Status</label>
                                </div>
                                <asp:RadioButtonList ID="rdoStatus" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdoStatus_SelectedIndexChanged" TabIndex="5">
                                    <asp:ListItem Value="1" Selected="True">Complete&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="0">Incomplete &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True">Both</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>

                    <%--<div class="col-12 btn-footer" style="display: none">
                        <asp:Button ID="btnShowStudent" runat="server" TabIndex="12" Text="Show Student" CssClass="btn btn-primary" ToolTip="Show Student" OnClick="btnShowStudent_Click" ValidationGroup="stud" />
                        <asp:Button ID="btnExcel" runat="server" TabIndex="12" Text="Export In Excel" CssClass="btn btn-primary" ToolTip="Export In Excel" OnClick="btnExcel_Click" ValidationGroup="stud" />
                        <asp:Button ID="btnExcel2" runat="server" TabIndex="13" Text="Export Student list(SVET)" CssClass="btn btn-primary" ToolTip="Export Student list" OnClick="btnExcel2_Click" ValidationGroup="stud" Visible="false" />
                        <asp:Button ID="btnExcel3" runat="server" TabIndex="14" Text="Fee Paid Student Details" CssClass="btn btn-primary" ToolTip="Export Student list Payment Details" OnClick="btnExcel3_Click" ValidationGroup="stud" />

                        <asp:ValidationSummary ID="vsStud" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="stud" />

                        <asp:Button ID="btnPendingAmt" runat="server" TabIndex="15" Text="Applicant Fill Up Status" CssClass="btn btn-primary" ToolTip="Pending Registration Fee Student List" OnClick="btnPendingAmt_Click" ValidationGroup="stud" />
                        <asp:Button ID="btnBranchPref" runat="server" TabIndex="15" Text="Branch Pref and Entrance Details" CssClass="btn btn-primary" ToolTip="Applicant Branch Preference with Entrance Details" OnClick="btnBranchPref_Click" ValidationGroup="stud" />
                        <asp:Button ID="btnShowReport" runat="server" TabIndex="16" Text="Show Report" CssClass="btn btn-info" ToolTip="Show Report" OnClick="btnShowReport_Click" ValidationGroup="stud" />
                    </div>--%>
                    <div class="col-12 btn-footer" >
                        <div class="row" style="text-align:center; margin-left:280px;" >
                            <asp:Button ID="btnShowStud" runat="server" TabIndex="4" Text="Show Student List" CssClass="btn btn-primary " ToolTip="Show Student" OnClick="btnShowStud_Click" ValidationGroup="stud" />
                                <asp:Button ID="btnStudDetails" runat="server" TabIndex="7" Text="Student Complete Data(Excel)" CssClass="btn btn-info"  ValidationGroup="studData" OnClick="btnStudDetails_Click"/>
                                 
                        </div>    
                        <p class="text-center">

                                           
                                <asp:Button ID="btnExcelForeignStud" runat="server" TabIndex="10" Text="Foreign Student Registration List (Excel)" ToolTip="Export Foreign Student Registration List" CssClass="btn btn-primary form-group" OnClick="btnExcelForeignStud_Click" Style="margin-top: 10px"  OnClientClick="return validationForeignStudent();" Visible="false" /><br />
                                <asp:Button ID="btnMinumum" runat="server" TabIndex="14" Text="Minimum Information Report(Excel)" CssClass="btn btn-primary"  Style="margin-top: 10px" OnClick="btnMinumum_Click" ValidationGroup="summary" Visible="false" />
                               
                                <asp:Button ID="btnShowReport" runat="server" TabIndex="5" Text="Show Report" CssClass="btn btn-primary form-group" ToolTip="Show Report"  ValidationGroup="stud" Visible="false" />
                                <asp:Button ID="btnAdmissionCount" runat="server" TabIndex="5" Text="Admission Count Report(Excel)" CssClass="btn btn-primary form-group" OnClick="btnAdmissionCount_Click" ToolTip="Export In Excel" Visible="false" />
                                <asp:Button ID="btnExcel" runat="server" TabIndex="5" Text="Admission Detail Report(Excel)" CssClass="btn btn-primary form-group" ToolTip="Export In Excel" OnClick="btnExcel_Click"  ValidationGroup="stud" Visible="false" />
                                <asp:Button ID="btnFormFillExcel" runat="server" TabIndex="6" Text="Form Filling Status(Excel)" OnClientClick="return validateAdmissionBatch();" CssClass="btn btn-info form-group" OnClick="btnFormFillExcel_Click" Visible="false" />
                                
                                <asp:Button ID="btnExcel2" runat="server" TabIndex="8" Text="Export Student list(SVET)" CssClass="btn btn-primary form-group" ToolTip="Export Student list"  ValidationGroup="stud"  OnClick="btnExcel2_Click" Visible="false" />
                                <asp:Button ID="btnExcel3" runat="server" TabIndex="9" Text="Fee Paid Student Details" CssClass="btn btn-primary form-group" ToolTip="Export Student list Payment Details" ValidationGroup="stud" OnClick="btnExcel3_Click" Visible="false" />
                               
                                <asp:Button ID="btnPaymentDetails" runat="server" TabIndex="10" Text="Online Payment Student Details(Excel)" ToolTip="Online Payment Student Details" CssClass="btn btn-primary form-group" Style="margin-top: 10px" OnClick="btnPaymentDetails_Click"  OnClientClick="return validateProgrammeType();" Visible="false" />
                                <asp:Button ID="btnConfirmStudent" runat="server" TabIndex="11" Text="Student Confirmation List(Excel)" ToolTip="Student Confirmation" CssClass="btn btn-primary form-group" Style="margin-top: 10px" OnClick="btnConfirmStudent_Click"  OnClientClick="return validateProgrammeType();" Visible="false" />
                                <asp:Button ID="btnDocumentList" runat="server" TabIndex="12" Text="Document List Status(Excel)" ToolTip="Document List Status"  CssClass="btn btn-primary form-group" OnClick="btnDocumentList_Click" Style="margin-top: 10px" Visible="false"  />
                                <asp:Button ID="btnPhd" runat="server" TabIndex="12" Text="Phd Students Complete Data(Excel)" CssClass="btn btn-primary" Style="margin-top: 10px" OnClick="btnPhd_Click" OnClientClick="return validateAdmissionBatch();" Visible="true"/> 
                                <asp:Button ID="btnSummaryReport" runat="server" TabIndex="13" Text="Admission Summary Report(Excel)" CssClass="btn btn-primary"  Style="margin-top: 10px" OnClick="btnSummaryReport_Click" ValidationGroup="summary" Visible="false" />
                                
                                  <asp:Button ID="btnCompleteDetail" runat="server" TabIndex="16" Text="Student Complete Details" CssClass="btn btn-primary form-group" ToolTip="Student Complete Details" OnClick="btnCompleteDetail_Click" ValidationGroup="stud" Visible="false" />   
                            <asp:Button ID="btnCancel" runat="server" TabIndex="15" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click to Cancel" OnClick="btnCancel_Click"/>      
                            <asp:ValidationSummary ID="vsStud" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="stud" DisplayMode="List" />
                                <p>
                                  &nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="studData" />
                                    <p>
                                        &nbsp;<asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="summary" />
                                        <div style="color: red; font-weight: bold; display:none" >
                                            Note : 1.Only Select Admission Batch for Form Filling Status(Excel) &amp; Foreign Student Registration List(Excel). 2.Only Select Degree for Student Complete Data(Excel).
                                        </div>
                        
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="updCollege" runat="server">
                            <asp:ListView ID="lvStudent" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Students List</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width:100%" id="tblstudDetails">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>SrNo
                                                </th>
                                                <th>UserID
                                                </th>
                                                <th>Name
                                                </th>
                                                <th>Mobile No
                                                </th>
                                                <th>Email Id
                                                </th>
                                                <th>Reg Date
                                                </th>
                                                <th>Degree
                                                </th>
                                                <th>Branch
                                                </th>
                                                <th>Integrated Application ID
                                                </th>
                                                <%-- <th>Branch Name
                                                </th>--%>
                                                <%-- <th>State
                                                </th>--%>
                                                <%-- <th></th>
                                                <th></th>--%>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item">
                                        <td>
                                            <%# Container.DataItemIndex + 1%>
                                        </td>
                                        <td>
                                            <%# Eval("USERNAME")%>
                                            <%--<%# Eval("LASTNAME")%>--%> 
                                        </td>
                                        <td>
                                            <%# Eval("FIRSTNAME")%>
                                            <%--<%# Eval("LASTNAME")%>--%> 
                                        </td>
                                        
                                        <td>
                                            <%# Eval("MOBILENO")%>
                                        </td>
                                        <td>
                                            <%# Eval("EMAILID")%>
                                        </td>
                                        <td>
                                            <%# Eval("REGDATE")%>
                                        </td>
                                        <td>
                                            <%# Eval("DEGREE")%>
                                        </td>
                                        <td>
                                            <%# Eval("BRANCH")%>
                                        </td>
                                        <td>
                                            <%# Eval("PREVIOUS APPLICATIONID")%>
                                        </td>
                                        <%--<td>
                                            <%# Eval("PSTATE")%>
                                        </td>--%>
                                        <%-- <td>
                                            <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-info" CommandName="Report" CommandArgument='<%# Eval("USERNO")%>' />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnDownload" runat="server" Text="Download" CssClass="btn btn-info" CommandName="Download" CommandArgument='<%# Eval("USERNO")%>' />
                                        </td>--%>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>

                </div>
            </div>
        </div>
    </div>


    <%--  </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
            <asp:AsyncPostBackTrigger ControlID="lvStudent" />
            <asp:AsyncPostBackTrigger ControlID="btnShowStudent" />
            <asp:PostBackTrigger ControlID="btnBranchPref" />
            <asp:AsyncPostBackTrigger ControlID="btnShowReport" />
            <asp:PostBackTrigger ControlID="btnExcel2" />
            <asp:PostBackTrigger ControlID="btnExcel3" />
            <asp:PostBackTrigger ControlID="btnPendingAmt" />
        </Triggers>--%>

    <%--</asp:UpdatePanel>--%>
    <div id="divMsg" runat="server">
    </div>
        <script>
            function validateProgrammeCode() {
                var selectedValue = document.getElementById('<%= ddlDegree.ClientID%>').value;
            if (selectedValue == "0") {
                alert('Please Select Degree.');
                return false;
            }
            else {
                return true;
            }
        }
        
        function validateProgrammeType() {
            var selectedValue = document.getElementById('<%= ddlProgrammeType.ClientID%>').value;
            if (selectedValue == "0") {
                alert('Please Select Programme Type.');
                return false;
            }
            else {
                return true;
            }
        }

        
        function validateControl() {
          
            var selectedValue = document.getElementById('<%= ddlProgrammeType.ClientID%>').value;
            if (selectedValue == "0") {
                alert('Please Select Programme Type.');
                return false;
            }
            else {
                return true;
            }
        }
            function validateAdmissionBatch() {
                var admBatch = document.getElementById('<%=ddlAdmbatch.ClientID%>');
                if (admBatch.value == 0)
                {
                    alert('Please Select Admission Batch.');
                    return false;
                }
            }
    </script>
</asp:Content>
