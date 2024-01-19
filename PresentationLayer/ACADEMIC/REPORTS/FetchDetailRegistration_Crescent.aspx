<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="FetchDetailRegistration_Crescent.aspx.cs" Inherits="FetchDetailRegistration_Crescent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
.dataTables_scrollHeadInner {
    width: max-content !important;
}
        .Background
        {
            background-color:black;
            opacity:0.9;
        }
</style>

    <script>
        function validate() {
            var admBatch = document.getElementById('<%=ddlAdmbatch.ClientID%>').value;
            if (admBatch == 0)
            {
                alert("Please Select Admission Batch.");
                return false;
            }
        }
      
    </script>
    <script>
        function validateUGPG() {
            //alert("inn");
            var admBatch = document.getElementById('<%=ddlAdmbatch.ClientID%>').value;
            var ugpg = document.getElementById('<%=ddlProgrammeType.ClientID%>').value;
              var alertMsg = "";
              //if (admBatch == 0 || ugpg == 0) {
                  if (admBatch == 0) {
                      alert("Please select admission batch.");
                      return false;
                  }
                  if (ugpg == 0) {
                      alert("Please select programee type.");
                      return false;
                  }
                  
              //}
          }
    </script>
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
                            <div class="form-group col-lg-4 col-md-4 col-12">

                                <div class="label-dynamic">
                                    <label>Programme Type</label>
                                </div>
                                <asp:DropDownList ID="ddlProgrammeType" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlProgrammeType_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">UG</asp:ListItem>
                                    <asp:ListItem Value="2">PG</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvProgrammeType" runat="server" ControlToValidate="ddlProgrammeType"
                                    Display="None" ValidationGroup="stud" InitialValue="0"
                                    ErrorMessage="Please Select Programme Type." Visible="true"></asp:RequiredFieldValidator>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator65" runat="server" ControlToValidate="ddlProgrammeType"
                                     ValidationGroup="UGPG" InitialValue="0" ErrorMessage="Please Select Programme Type." Visible="true"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-4 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <label>Degree</label>
                                </div>
                                <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="3" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                    <%--            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ValidationGroup="stud" InitialValue="0"
                                    ErrorMessage="Please Select Degree." Visible="true"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group col-lg-4 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <label>Branch</label>
                                </div>
                                <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="4" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
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
                                <i class="fa fa-pencil" aria-hidden="true"><span style="color: red; font-family: Arial"> Note : To generate the "Student Complete Details (Btech)" only admission batch selection is required. </span></i><br />
                        </div>
                    </div>
                    <div class="col-12 btn-footer">
                        <div class="row" style="text-align: center; margin-left: 280px;">
                            <asp:Button ID="btnShowStud" runat="server" TabIndex="6" Text="Show Student List" CssClass="btn btn-primary form-group" ToolTip="Show Student" OnClick="btnShowStud_Click" ValidationGroup="stud" />
                            <asp:Button ID="btnStudDetails" runat="server" TabIndex="7" Text="Student Complete Data(Excel)" CssClass="btn btn-info form-group" ValidationGroup="studData" OnClick="btnStudDetails_Click" />
                            <asp:Button ID="btnBtech" runat="server" TabIndex="16" Text="Student Complete Details (Btech)" CssClass="btn btn-primary form-group" ToolTip="Student Complete Details (Btech)" OnClick="btnBtech_Click" Visible="true" ValidationGroup="bTech" OnClientClick="return validate();"/>
                            <asp:Button ID="btnUGPG" runat="server" TabIndex="17" Text="Student Complete Details (UG/PG)" CssClass="btn btn-primary form-group" ToolTip="Student Complete Details (UG/PG)" OnClick="btnUGPG_Click" Visible="true" OnClientClick="return validateUGPG();"/>

                        </div>
                        <p class="text-center">


                            <asp:Button ID="btnExcelForeignStud" runat="server" TabIndex="10" Text="Foreign Student Registration List (Excel)" ToolTip="Export Foreign Student Registration List" CssClass="btn btn-primary form-group" OnClick="btnExcelForeignStud_Click" Style="margin-top: 10px" OnClientClick="return validationForeignStudent();" Visible="false" /><br />
                            <asp:Button ID="btnMinumum" runat="server" TabIndex="14" Text="Minimum Information Report(Excel)" CssClass="btn btn-primary" Style="margin-top: 10px" OnClick="btnMinumum_Click" ValidationGroup="summary" Visible="false" />

                            <asp:Button ID="btnShowReport" runat="server" TabIndex="5" Text="Show Report" CssClass="btn btn-primary form-group" ToolTip="Show Report" ValidationGroup="stud" Visible="false" />
                            <asp:Button ID="btnAdmissionCount" runat="server" TabIndex="5" Text="Admission Count Report(Excel)" CssClass="btn btn-primary form-group" OnClick="btnAdmissionCount_Click" ToolTip="Export In Excel" Visible="false" />
                            <asp:Button ID="btnExcel" runat="server" TabIndex="5" Text="Admission Detail Report(Excel)" CssClass="btn btn-primary form-group" ToolTip="Export In Excel" OnClick="btnExcel_Click" ValidationGroup="stud" Visible="false" />
                            <asp:Button ID="btnFormFillExcel" runat="server" TabIndex="6" Text="Form Filling Status(Excel)" OnClientClick="return validateAdmissionBatch();" CssClass="btn btn-info form-group" OnClick="btnFormFillExcel_Click" Visible="false" />

                            <asp:Button ID="btnExcel2" runat="server" TabIndex="8" Text="Export Student list(SVET)" CssClass="btn btn-primary form-group" ToolTip="Export Student list" ValidationGroup="stud" OnClick="btnExcel2_Click" Visible="false" />
                            <asp:Button ID="btnExcel3" runat="server" TabIndex="9" Text="Fee Paid Student Details" CssClass="btn btn-primary form-group" ToolTip="Export Student list Payment Details" ValidationGroup="stud" OnClick="btnExcel3_Click" Visible="false" />

                            <asp:Button ID="btnPaymentDetails" runat="server" TabIndex="10" Text="Online Payment Student Details(Excel)" ToolTip="Online Payment Student Details" CssClass="btn btn-primary form-group" Style="margin-top: 10px" OnClick="btnPaymentDetails_Click" OnClientClick="return validateProgrammeType();" Visible="false" />
                            <asp:Button ID="btnConfirmStudent" runat="server" TabIndex="11" Text="Student Confirmation List(Excel)" ToolTip="Student Confirmation" CssClass="btn btn-primary form-group" Style="margin-top: 10px" OnClick="btnConfirmStudent_Click" OnClientClick="return validateProgrammeType();" Visible="false" />
                            <asp:Button ID="btnDocumentList" runat="server" TabIndex="12" Text="Document List Status(Excel)" ToolTip="Document List Status" CssClass="btn btn-primary form-group" OnClick="btnDocumentList_Click" Style="margin-top: 10px" Visible="false" />
                            <asp:Button ID="btnPhd" runat="server" TabIndex="12" Text="Phd Students Complete Data(Excel)" CssClass="btn btn-primary" Style="margin-top: 10px" OnClick="btnPhd_Click" OnClientClick="return validateAdmissionBatch();" Visible="true" />
                            <asp:Button ID="btnSummaryReport" runat="server" TabIndex="13" Text="Admission Summary Report(Excel)" CssClass="btn btn-primary" Style="margin-top: 10px" OnClick="btnSummaryReport_Click" ValidationGroup="summary" Visible="false" />

                            <asp:Button ID="btnCompleteDetail" runat="server" TabIndex="16" Text="Student Complete Details" CssClass="btn btn-primary form-group" ToolTip="Student Complete Details" OnClick="btnCompleteDetail_Click" ValidationGroup="stud" Visible="false" />
                            <asp:Button ID="btnCancel" runat="server" TabIndex="15" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click to Cancel" OnClick="btnCancel_Click" />
                            <asp:ValidationSummary ID="vsStud" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="stud" DisplayMode="List" />
                                &nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="studData" />
                                <p>
                                    &nbsp;<asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="summary" />
                                    <div style="color: red; font-weight: bold; display: none">
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
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblstudDetails">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>SrNo
                                                </th>
                                                <th>Application Number
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
                                                <th>
                                                    Confirm Status
                                                </th>
                                              <%--  <th>Branch
                                                </th>--%>
                                                <th>
                                                    Download Application Form
                                                </th>
                                                <th>
                                                  Document Download (zip)
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
                                        <td>
                                            <%# Container.DataItemIndex + 1%>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkUserId" CausesValidation="false" runat="server" Text='<%# Eval("USERNAME") %>' CommandArgument='<%# Eval("USERNO") %>' OnClick="lnkUserId_Click"></asp:LinkButton>
                                        </td>
                                        <td>
                                            <%# Eval("FIRSTNAME")%>
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
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Convert.ToInt32(Eval("CONFIRM_STATUS"))==1 ? "Confirmed" : "Not Confirmed"%>'
                                                ForeColor='<%#Convert.ToInt32(Eval("CONFIRM_STATUS"))==1 ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label>
                                        </td>
                                        <%--<td>
                                            <%# Eval("BRANCH")%>
                                        </td>--%>
                                        <td style="text-align:center">
                                            <asp:ImageButton ID="btnDownloadApp" runat="server" AlternateText="Download Application Form." ImageUrl="~/Images/pdf.png" OnClick="btnDownloadApp_Click" CommandArgument='<%# Eval("USERNO") %>' Height="32px" />
                                        </td>
                                        <td style="text-align:center">
                                            <asp:ImageButton ID="btnZip" runat="server" AlternateText="Document Download" OnClick="btnZip_Click" ImageUrl="~/Images/zip.png" Height="32px" CommandArgument='<%# Eval("DOC_FILENAME") %>'  Enabled='<%#Convert.ToString(Eval("DOC_FILENAME"))=="" ? false : true %>' ToolTip='<%# Eval("USERNO") %>' CommandName='<%# Eval("DOCUMENT_NO")%>'/>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <asp:LinkButton ID="lnkPopup" runat="server"></asp:LinkButton>
                        </asp:Panel>
                    </div>

                </div>
            </div>
        </div>
    </div>
    
    <div id="divMsg" runat="server">
    </div>


    <asp:Panel ID="pnlPopup" runat="server">
        <asp:UpdatePanel ID="updMaindiv" runat="server">
            <ContentTemplate>
                <div class="modal-dialog  modal-lg">
                    <div class="modal-content">

                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">Preview</h4>
                        </div>

                        <!-- Modal body -->
                        <div class="modal-body" style="overflow: scroll; height: 450px;">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Registration Details</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Name in Block Letters</label>
                                        </div>
                                        <asp:TextBox ID="txtPopName" runat="server" CssClass="form-control" ToolTip="Name in Block Letters" placeholder="Enter Name in Block Letters"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Gender </label>
                                        </div>
                                        <asp:RadioButtonList ID="rdoPopGender" runat="server" TextAlign="Right" RepeatDirection="Horizontal" ValidationGroup="Submit" TabIndex="1" ToolTip="Please Select Gender.">
                                            <asp:ListItem Value="1">Male</asp:ListItem>
                                            <asp:ListItem Value="2">Female</asp:ListItem>
                                            <asp:ListItem Value="3">Transgender</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvGender" runat="server" ControlToValidate="rdoPopGender" ErrorMessage="Please Select Gender." Display="None" ValidationGroup="Submit" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>DOB </label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="far fa-calendar-alt" id="icon1" runat="server"></i>
                                            </div>
                                            <asp:TextBox ID="txtPopDOB" runat="server" TabIndex="2" CssClass="form-control" ValidationGroup="Submit" placeholder="DD/MM/YYYY" onchange="checkDOB(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDOB" runat="server" ValidationGroup="Submit" ControlToValidate="txtPopDOB" Display="None"
                                                ErrorMessage="Please Enter DOB." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:CalendarExtender ID="ceAdmDt" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtPopDOB" PopupButtonID="dobIcon" Enabled="True">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeAdmDt" runat="server" TargetControlID="txtPopDOB"
                                                Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Religion </label>
                                        </div>
                                        <asp:DropDownList ID="ddlPopReligion" runat="server" TabIndex="3" CssClass="form-control"  ValidationGroup="Submit" ToolTip="Please Select Religion." AppendDataBoundItems="true" OnSelectedIndexChanged="ddlPopReligion_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvReligion" runat="server" ValidationGroup="Submit" ControlToValidate="ddlPopReligion" Display="None"
                                            ErrorMessage="Please Select Religion." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divPopReligion" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Others , Please Specify </label>

                                        </div>
                                        <asp:TextBox ID="txtPopOtherReligion" runat="server" CssClass="form-control" ValidationGroup="Submit" TabIndex="4" ToolTip="Please Enter Other Religion." MaxLength="32" placeholder="Enter Other Religion"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPopotherreligion" runat="server" ControlToValidate="txtPopOtherReligion" Display="None"
                                            ErrorMessage="Please Enter Other Religion." SetFocusOnError="true" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender36" runat="server" TargetControlID="txtPopOtherReligion" FilterMode="InvalidChars"
                                            InvalidChars="0123456789~`!@#$%^&*()_-+={[}]|\:;'<,>?">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Community </label>
                                        </div>
                                        <asp:DropDownList ID="ddlPopCommunity" runat="server" TabIndex="4" CssClass="form-control" ValidationGroup="Submit" ToolTip="Please Select Community." AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCommunity" runat="server" ValidationGroup="Submit" ControlToValidate="ddlPopCommunity" Display="None"
                                            ErrorMessage="Please Select Community." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Mother Tongue </label>
                                        </div>
                                        <asp:DropDownList ID="ddlPopTongue" runat="server" TabIndex="5" CssClass="form-control" ValidationGroup="Submit" ToolTip="Please Select Mother Tongue." AppendDataBoundItems="true" OnSelectedIndexChanged="ddlPopTongue_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvTongue" runat="server" ValidationGroup="Submit" ControlToValidate="ddlPopTongue" Display="None"
                                            ErrorMessage="Please Select Mother Tongue." InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divPopTongue" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Others , Please Specify</label>
                                        </div>
                                        <asp:TextBox ID="txtPopTongue" runat="server" CssClass="form-control" ValidationGroup="Submit" TabIndex="6" ToolTip="Please Enter Mother Tongue." MaxLength="32" placeholder="Enter Other Mother Tongue"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTounge" runat="server" ControlToValidate="txtPopTongue" Display="None"
                                            ErrorMessage="Please Enter Other Mother Tongue." SetFocusOnError="true" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender40" runat="server" TargetControlID="txtPopTongue" FilterMode="InvalidChars"
                                            InvalidChars="0123456789~`!@#$%^&*()_-+={[}]|\/:;'<,>?">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Aadhaar Card Number </label>
                                        </div>
                                        <asp:TextBox ID="txtPopAadhar" runat="server" CssClass="form-control" TabIndex="6" ToolTip="Please Enter Aadhar card Number." MaxLength="12" placeholder="Enter Aadhar card Number" AutoComplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAdhar" runat="server" ValidationGroup="Submit" ControlToValidate="txtPopAadhar" Display="None"
                                            ErrorMessage="Please Enter Aadhaar Card Number." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ajaxToolkit1" runat="server" TargetControlID="txtPopAadhar" FilterMode="ValidChars"
                                            ValidChars="0123456789">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPopAadhar"
                                            Display="None" SetFocusOnError="True" ValidationExpression="^[100000000000-999999999999]{12}$"
                                            ErrorMessage="Please Enter Valid Aadhaar Card Number" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Nationality </label>
                                        </div>
                                        <asp:DropDownList ID="ddlPopNationality" runat="server" TabIndex="7" CssClass="form-control" ValidationGroup="Submit" ToolTip="Please Select Nationality." AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvNational" runat="server" ValidationGroup="Submit" ControlToValidate="ddlPopNationality" Display="None"
                                            ErrorMessage="Please Select Nationality." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="updPraentsPar" runat="server">
                                <ContentTemplate>
                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Particulars of Parents </h5>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Father Name </label>
                                                </div>
                                                <asp:TextBox ID="txtPopFather" runat="server" CssClass="form-control" TabIndex="8" ToolTip="Please Enter Father Name." placeholder="Enter Father Name" MaxLength="150" Style="text-transform: uppercase" AutoComplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFather" runat="server" ValidationGroup="Submit" ControlToValidate="txtPopFather" Display="None"
                                                    ErrorMessage="Please Enter Father Name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPopFather" FilterMode="InvalidChars"
                                                    InvalidChars="0123456789~`!@#$%^&*()_-+{[}]:;'<,>.?/|\">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Occupation of Father </label>
                                                </div>
                                                <asp:DropDownList ID="ddlPopFOcc" runat="server" TabIndex="9" CssClass="form-control" data-select2-enable="false" ValidationGroup="Submit" ToolTip="Please Select Occupation of Father." AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPopFOcc_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvFocc" runat="server" ValidationGroup="Submit" ControlToValidate="ddlPopFOcc" Display="None"
                                                    ErrorMessage="Please Select Occupation of Father." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divPopFocc" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Others , Please Specify</label>

                                                </div>
                                                <asp:TextBox ID="txtPopFoccOther" runat="server" CssClass="form-control" TabIndex="10" ToolTip="Please Enter Other Occupation." placeholder="Enter Father Other Occupation" MaxLength="150"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFathOcc" runat="server" ControlToValidate="txtPopFoccOther" Display="None"
                                                    ErrorMessage="Please Enter Father's Other Occupation." SetFocusOnError="true" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender41" runat="server" TargetControlID="txtPopFoccOther" FilterMode="InvalidChars"
                                                    InvalidChars="0123456789~`!@#$%^&*()_-+{[}]:;'<,>.?/|\">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Father's Mobile Number </label>
                                                </div>
                                                <asp:TextBox ID="txtPopFmobile" runat="server" CssClass="form-control" TabIndex="10" ToolTip="Please Enter Father's Mobile Number." placeholder="Enter Father's Mobile Number" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFmob" runat="server" ValidationGroup="Submit" ControlToValidate="txtPopFmobile" Display="None"
                                                    ErrorMessage="Please Enter Father's Mobile Number." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPopFmobile" FilterMode="ValidChars"
                                                    ValidChars="0123456789">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator ID="revFmob" runat="server" ControlToValidate="txtPopFmobile"
                                                    Display="None" SetFocusOnError="True" ValidationExpression="^[0-9]{10}$"
                                                    ErrorMessage="Please Enter Valid Father's Mobile Number." ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Mother Name </label>
                                                </div>
                                                <asp:TextBox ID="txtPopMother" runat="server" CssClass="form-control" TabIndex="11" ToolTip="Please Enter Mother Name." placeholder="Enter Mother Name" MaxLength="150" Style="text-transform: uppercase" AutoComplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvMother" runat="server" ValidationGroup="Submit" ControlToValidate="txtPopMother" Display="None"
                                                    ErrorMessage="Please Enter Mother Name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtPopMother" FilterMode="InvalidChars"
                                                    InvalidChars="0123456789~`!@#$%^&*()_-+{[}]:;'<,>.?/|\">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Occupation of Mother </label>
                                                </div>
                                                <asp:DropDownList ID="ddlPopMOcc" runat="server" TabIndex="12" CssClass="form-control" data-select2-enable="false" ValidationGroup="Submit" ToolTip="Please Select Occupation of Mother." AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPopMOcc_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvpopMocc" runat="server" ValidationGroup="Submit" ControlToValidate="ddlPopMOcc" Display="None"
                                                    ErrorMessage="Please Select Occupation of Mother." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divMOccupation" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Others , Please Specify</label>

                                                </div>
                                                <asp:TextBox ID="txtPopMOccOther" runat="server" CssClass="form-control" TabIndex="13" ToolTip="Please Enter Other Occupation." placeholder="Enter Mother Other Occupation" MaxLength="150"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvMOcc" runat="server" ControlToValidate="txtPopMOccOther" Display="None"
                                                    ErrorMessage="Please Enter Mother's Other Occupation." SetFocusOnError="true" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender42" runat="server" TargetControlID="txtPopMOccOther" FilterMode="InvalidChars"
                                                    InvalidChars="0123456789~`!@#$%^&*()_-+{[}]:;'<,>.?/|\">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Mother's Mobile Number </label>
                                                </div>
                                                <asp:TextBox ID="txtPopMMobile" runat="server" CssClass="form-control" TabIndex="13" ToolTip="Please Enter Mother's Mobile Number." placeholder="Enter Mother's Mobile Number" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvMMobile" runat="server" ValidationGroup="Submit" ControlToValidate="txtPopMMobile" Display="None"
                                                    ErrorMessage="Please Enter Mother's Mobile Number." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtPopMMobile" FilterMode="ValidChars"
                                                    ValidChars="0123456789">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPopMMobile"
                                                    Display="None" SetFocusOnError="True" ValidationExpression="^[0-9]{10}$"
                                                    ErrorMessage="Please Enter Valid Mother's Mobile Number." ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Annual income of parents (Includes both parents)</label>
                                                </div>
                                                <asp:DropDownList ID="ddlPopIncome" runat="server" CssClass="form-control" TabIndex="14" ToolTip="Please Select Annual Income of Parents." AppendDataBoundItems="true" data-select2-enable="false" Enabled="false">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvIncome" runat="server" ValidationGroup="Submit" ControlToValidate="ddlPopIncome" InitialValue="0"
                                                    Display="None" ErrorMessage="Please Select Annual Income of Parents." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Course Preference </h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <asp:Label ID="lblPopPref1" runat="server"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlPopPref1" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select Programme Preference 1." TabIndex="15" Enabled="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <asp:Label ID="lblPopPref2" runat="server"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlPopPref2" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select Programme Preference 2." TabIndex="16" Enabled="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <asp:Label ID="lblPopPref3" runat="server"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlPopPref3" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select Programme Preference 3." TabIndex="17" Enabled="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>CIEAT Phase</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <br />
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>CIEAT Phase Preference</label>
                                            <asp:DropDownList ID="ddlPopPhase" runat="server" CssClass="form-control" ToolTip="Please Select CIEAT Phase." AppendDataBoundItems="true" data-select2-enable="false" Enabled="false">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1" Selected="True">CIEAT - Phase 1</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Address Details </h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Address Line 1 </label>
                                        </div>
                                        <asp:TextBox ID="txtPopAdd1" runat="server" CssClass="form-control" TabIndex="18" ToolTip="Please Enter Address Line 1." placeholder="Enter Address Line 1" MaxLength="200" AutoComplete="off" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAdd1" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter Address Line 1." Display="None" ControlToValidate="txtPopAdd1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtPopAdd1" FilterMode="InvalidChars"
                                            InvalidChars="~`!@#$%^&*_+={[}}:;|<>?'">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Address Line 2</label>
                                        </div>
                                        <asp:TextBox ID="txtPopAdd2" runat="server" CssClass="form-control" TabIndex="18" ToolTip="Please Enter Address Line 2." placeholder="Enter Address Line 2" MaxLength="200" AutoComplete="off" TextMode="MultiLine"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtPopAdd2" FilterMode="InvalidChars"
                                            InvalidChars="~`!@#$%^&*_+={[}}:;|<>?'">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Address Line 3</label>
                                        </div>
                                        <asp:TextBox ID="txtPopAdd3" runat="server" CssClass="form-control" TabIndex="18" ToolTip="Please Enter Address Line 3." placeholder="Enter Address Line 3" MaxLength="200" AutoComplete="off" TextMode="MultiLine"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtPopAdd3" FilterMode="InvalidChars"
                                            InvalidChars="~`!@#$%^&*_+={[}}:;|<>?'">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>State </label>
                                        </div>
                                        <asp:UpdatePanel ID="updAddress" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlPopState" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select State." TabIndex="19" AutoPostBack="true" OnSelectedIndexChanged="ddlPopState_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvState" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter State." Display="None" ControlToValidate="ddlPopState" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>District</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select District." TabIndex="20">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter District." Display="None" ControlToValidate="ddlDistrict" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>City </label>
                                        </div>
                                        <asp:TextBox ID="txtPopCity" runat="server" CssClass="form-control" TabIndex="21" ToolTip="Please Enter City." placeholder="Enter City" MaxLength="32" AutoComplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter City." Display="None" ControlToValidate="txtPopCity" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtPopCity" FilterMode="InvalidChars"
                                            InvalidChars="`~!@#$%^&*()_-+={[}]:;<,>.?/0123456789">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Pincode </label>
                                        </div>
                                        <asp:TextBox ID="txtPopPin" runat="server" CssClass="form-control" TabIndex="22" ToolTip="Please Enter Pincode." MaxLength="6" placeholder="Enter Pincode" AutoComplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPincode" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter Pincode." Display="None" ControlToValidate="txtPopPin" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtPopPin" FilterMode="ValidChars"
                                            ValidChars="0123456789">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtPopPin"
                                            Display="None" SetFocusOnError="True" ValidationExpression="^[0-9]{6}$"
                                            ErrorMessage="Please Enter Valid Pincode." ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Mobile Number </label>
                                        </div>
                                        <asp:TextBox ID="txtPopMob" runat="server" CssClass="form-control" ToolTip="Please Enter Mobile Number." placeholder="Enter Mobile Number" AutoComplete="off" MaxLength="10" Enabled="false"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Primary Email ID </label>
                                        </div>
                                        <asp:TextBox ID="txtPopEmailID" runat="server" CssClass="form-control" ToolTip="Please Enter Email." placeholder="Enter Email" AutoComplete="off" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="updEdu" runat="server">
                                <ContentTemplate>
                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Educational Information </h5>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Educational Information </label>
                                                </div>
                                                <asp:DropDownList ID="ddlPopEdu" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select Educational Information." TabIndex="23" AutoPostBack="true" OnSelectedIndexChanged="ddlPopEdu_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvEdu" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter Educational Information." Display="None" ControlToValidate="ddlPopEdu" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divPopEdu" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Educational Information (Others) </label>
                                                </div>
                                                <asp:TextBox ID="txtPopEduOthers" runat="server" CssClass="form-control" ToolTip="Please Enter Educational Information (Others)." TabIndex="24" placeholder="Enter Educational Information (Others)">                                            
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvEduoth" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter Educational Information (Others)." Display="None" ControlToValidate="txtPopEduOthers" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>12th Exam Registration No. </label>
                                                </div>
                                                <asp:TextBox ID="txtPopExmReg" runat="server" CssClass="form-control" ToolTip="Please Enter 12th Exam Registration No." TabIndex="24" placeholder="Enter 12 Exam Registration No" AutoComplete="off" MaxLength="20"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtPopExmReg"
                                                    InvalidChars="~`!@#$%^&*()_-+={[}]:;<,>.?|'" FilterMode="InvalidChars" />
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>School Name </label>
                                                </div>
                                                <asp:TextBox ID="txtPopSchool" runat="server" CssClass="form-control" ToolTip="Please Enter School Name" TabIndex="25" placeholder="Enter School Name" AutoComplete="off" MaxLength="200"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtPopSchool"
                                                    InvalidChars="1234567890" FilterMode="InvalidChars" />
                                                <asp:RequiredFieldValidator ID="rfvSchool" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter School Name." Display="None" ControlToValidate="txtPopSchool" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Month of Passing/Appearing </label>
                                                </div>
                                                <asp:DropDownList ID="ddlPopMPass" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select Month of Passing/Appearing." TabIndex="26">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter Month of Passing/Appearing." Display="None" ControlToValidate="ddlPopMPass" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Year of Passing/Appearing </label>
                                                </div>
                                                <asp:DropDownList ID="ddlPopYPass" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select Year of Passing/Appearing." TabIndex="27">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvYpass" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter Year of Passing/Appearing." Display="None" ControlToValidate="ddlPopYPass" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Medium of Instruction </label>
                                                </div>
                                                <asp:RadioButtonList ID="rdoPopMedium" runat="server" TextAlign="Right" RepeatDirection="Horizontal" ValidationGroup="Submit" TabIndex="28" ToolTip="Please Select Medium of Instruction." AutoPostBack="true" OnSelectedIndexChanged="rdoPopMedium_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">English</asp:ListItem>
                                                    <asp:ListItem Value="2">Others</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="rfvMedium" runat="server" ControlToValidate="rdoPopMedium" ErrorMessage="Please Select Medium of Instruction." Display="None" ValidationGroup="Submit" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divPopMedium" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Medium of Instruction (Others) </label>
                                                </div>
                                                <asp:TextBox ID="txtPopMediumOther" runat="server" CssClass="form-control" MaxLength="32" ToolTip="Please Enter Medium of Instruction (Others)" TabIndex="29" placeholder="Medium of Instruction (Others)"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvmedioth" runat="server" ControlToValidate="txtPopMediumOther" ErrorMessage="Please Enter Medium of Instruction (Others)." Display="None" ValidationGroup="Submit" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ajaxFTBX" runat="server" TargetControlID="txtPopMediumOther" InvalidChars="0123456789~`!@#$%^&*_-+={{}}|:;'<>?"
                                                    FilterMode="InvalidChars">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Country Name for which School last studied </label>
                                                </div>
                                                <asp:DropDownList ID="ddlPopLast" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select Country Name for which School last studied." TabIndex="29">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvLast" runat="server" ValidationGroup="Submit" ErrorMessage="Please Select Country Name for which School last studied." Display="None" ControlToValidate="ddlPopLast" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Enter The Marks Name If Available </label>
                                                </div>
                                                <asp:RadioButtonList ID="rdoPopAvailable" runat="server" TextAlign="Right" RepeatDirection="Horizontal" ValidationGroup="Submit" TabIndex="30" ToolTip="Enter The Marks Name If Available." AutoPostBack="true" OnSelectedIndexChanged="rdoPopAvailable_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="rfvAvail" runat="server" ControlToValidate="rdoPopAvailable" ErrorMessage="Please Select The Marks Name If Available." Display="None" ValidationGroup="Submit" InitialValue="-1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="updSubjects" runat="server">
                                <ContentTemplate>
                                    <div class="col-12 mt-3" id="divPopQual" runat="server">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Marks Obtained in Qualifying Examination </h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Subject </label>
                                        </div>
                                        <asp:DropDownList ID="ddlPopSub1" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select Subject." TabIndex="31" OnSelectedIndexChanged="ddlPopSub1_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSub" runat="server" ValidationGroup="Submit" ErrorMessage="Please Select Subject." ControlToValidate="ddlPopSub1" InitialValue="0" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Marks Obtained </label>
                                        </div>
                                        <asp:TextBox ID="txtPopMarksObt1" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="32" placeholder="Enter Marks Obtained" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_1();" onblur="checkMaxNo();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtPopMarksObt1"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="rfvMarksobt" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter Marks Obtained." ControlToValidate="txtPopMarksObt1" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Maximum Marks </label>
                                        </div>
                                        <asp:TextBox ID="txtPopMaxMarks1" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="33" placeholder="Enter Maximum Marks" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_1();" onblur="checkMaxNo();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txtPopMaxMarks1"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="rfvMaxMarks" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter Maximum Marks." ControlToValidate="txtPopMaxMarks1" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Percentage </label>
                                        </div>
                                        <asp:TextBox ID="txtPopPer1" runat="server" CssClass="form-control" ToolTip="Please Enter Percentage." TabIndex="34" AutoComplete="off" MaxLength="5" Enabled="false"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="txtPopPer1"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="rfvPopper" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter Percentage." ControlToValidate="txtPopPer1" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="hdnPer_1" runat="server" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Language </label>
                                        </div>
                                        <asp:TextBox ID="txtPopLang" runat="server" CssClass="form-control" ToolTip="Please Enter Percentage." TabIndex="35" AutoComplete="off" MaxLength="100"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" TargetControlID="txtPopLang"
                                            InvalidChars=".~`!@#$%^&*()_-+={[}]:;'<,>.?/" FilterMode="InvalidChars" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Marks Obtained </label>
                                        </div>
                                        <asp:TextBox ID="txtPopMarksObtLang" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="36" placeholder="Enter Marks Obtained" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_Lang();" onblur="checkMaxNo();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" TargetControlID="txtPopMarksObtLang"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Maximum Marks </label>
                                        </div>
                                        <asp:TextBox ID="txtPopMaxMarksLang" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="37" placeholder="Enter Maximum Marks" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_Lang();" onblur="checkMaxNo();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" TargetControlID="txtPopMaxMarksLang"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Percentage </label>
                                        </div>
                                        <asp:TextBox ID="txtPopPerLang" runat="server" CssClass="form-control" ToolTip="Please Enter Percentage." TabIndex="38" AutoComplete="off" MaxLength="5" Enabled="false"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" TargetControlID="txtPopPerLang"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:HiddenField ID="hdnPerLang" runat="server" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Subject </label>
                                        </div>
                                        <asp:DropDownList ID="ddlPopSub2" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select Subject." TabIndex="39" OnSelectedIndexChanged="ddlPopSub2_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvsub2" runat="server" ValidationGroup="Submit" ErrorMessage="Please Select Subject." Display="None" ControlToValidate="ddlPopSub2" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="hdnDDL2" runat="server" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Marks Obtained </label>
                                        </div>
                                        <asp:TextBox ID="txtPopMarksObt2" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="40" placeholder="Enter Marks Obtained" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_2();" onblur="checkMaxNo();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" TargetControlID="txtPopMarksObt2"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter Marks Obtained." Display="None" ControlToValidate="txtPopMarksObt2" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Maximum Marks </label>
                                        </div>
                                        <asp:TextBox ID="txtPopMaxMarks2" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="41" placeholder="Enter Maximum Marks" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_2();" onblur="checkMaxNo();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" TargetControlID="txtPopMaxMarks2"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter Maximum Marks." Display="None" ControlToValidate="txtPopMaxMarks2" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Percentage </label>
                                        </div>
                                        <asp:TextBox ID="txtPopPer2" runat="server" CssClass="form-control" ToolTip="Please Enter Percentage." TabIndex="42" AutoComplete="off" MaxLength="5" Enabled="false"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server" TargetControlID="txtPopPer2"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:HiddenField ID="hdnPer_2" runat="server" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Subject </label>
                                        </div>
                                        <asp:DropDownList ID="ddlPopSub3" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select Subject." TabIndex="43" OnSelectedIndexChanged="ddlPopSub3_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ValidationGroup="Submit" ErrorMessage="Please Select Subject." Display="None" ControlToValidate="ddlPopSub3" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="hdnDDL3" runat="server" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Marks Obtained </label>
                                        </div>
                                        <asp:TextBox ID="txtPopMarksObt3" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="44" placeholder="Enter Marks Obtained" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_3();" onblur="checkMaxNo();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" TargetControlID="txtPopMarksObt3"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter Marks Obtained." Display="None" ControlToValidate="txtPopMarksObt3" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Maximum Marks </label>
                                        </div>
                                        <asp:TextBox ID="txtPopMaxMarks3" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="45" placeholder="Enter Maximum Marks" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_3();" onblur="checkMaxNo();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" TargetControlID="txtPopMaxMarks3"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter Maximum Marks." Display="None" ControlToValidate="txtPopMaxMarks3" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Percentage </label>
                                            <asp:TextBox ID="txtPopPer3" runat="server" CssClass="form-control" ToolTip="Please Enter Percentage." TabIndex="46" AutoComplete="off" MaxLength="5" Enabled="false"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server" TargetControlID="txtPopPer3"
                                                ValidChars="1234567890." FilterMode="ValidChars" />
                                            <asp:HiddenField ID="hdnPer_3" runat="server" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Subject </label>
                                        </div>
                                        <asp:DropDownList ID="ddlPopSub4" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select Subject." TabIndex="47" OnSelectedIndexChanged="ddlPopSub4_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" ValidationGroup="Submit" ErrorMessage="Please Select Subject." Display="None" ControlToValidate="ddlPopSub4" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="hdnDDL4" runat="server" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Marks Obtained </label>
                                        </div>
                                        <asp:TextBox ID="txtPopMarksObt4" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="48" placeholder="Enter Marks Obtained" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_4();" onblur="checkMaxNo();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender26" runat="server" TargetControlID="txtPopMarksObt4"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator46" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter Marks Obtained." Display="None" ControlToValidate="txtPopMarksObt4" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Maximum Marks </label>
                                        </div>
                                        <asp:TextBox ID="txtPopMaxMarks4" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="49" placeholder="Enter Maximum Marks" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_4();" onblur="checkMaxNo();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender27" runat="server" TargetControlID="txtPopMaxMarks4"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator47" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter Maximum Marks." Display="None" ControlToValidate="txtPopMaxMarks4" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Percentage </label>
                                        </div>
                                        <asp:TextBox ID="txtPopPer4" runat="server" CssClass="form-control" ToolTip="Please Enter Percentage." TabIndex="50" AutoComplete="off" MaxLength="51" Enabled="false"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender28" runat="server" TargetControlID="txtPopPer4"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:HiddenField ID="hdnPer_4" runat="server" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Subject </label>
                                        </div>
                                        <asp:DropDownList ID="ddlPopSub5" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select Subject." TabIndex="51" OnSelectedIndexChanged="ddlPopSub5_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator49" runat="server" ValidationGroup="Submit" ErrorMessage="Please Select Subject." Display="None" ControlToValidate="ddlPopSub5" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Marks Obtained </label>
                                        </div>
                                        <asp:TextBox ID="txtPopMarksObt5" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="52" placeholder="Enter Marks Obtained" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_5();" onblur="checkMaxNo();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender29" runat="server" TargetControlID="txtPopMarksObt5"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter Marks Obtained." Display="None" ControlToValidate="txtPopMarksObt5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Maximum Marks </label>
                                        </div>
                                        <asp:TextBox ID="txtPopMaxMarks5" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="53" placeholder="Enter Maximum Marks" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_5();" onblur="checkMaxNo();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender30" runat="server" TargetControlID="txtPopMaxMarks5"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter Maximum Marks." Display="None" ControlToValidate="txtPopMaxMarks5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Percentage </label>
                                        </div>
                                        <asp:TextBox ID="txtPopPer5" runat="server" CssClass="form-control" ToolTip="Please Enter Percentage." TabIndex="54" AutoComplete="off" MaxLength="5" Enabled="false"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender31" runat="server" TargetControlID="txtPopPer5"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:HiddenField ID="hdnPer_5" runat="server" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divPopSpecify" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Others Please Specify </label>
                                        </div>
                                        <asp:TextBox ID="txtPopSpecify" runat="server" CssClass="form-control" ToolTip="Please Enter Others Please Specify." TabIndex="55" placeholder="Others Please Specify" AutoComplete="off" MaxLength="100"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender35" runat="server" TargetControlID="txtPopSpecify"
                                            InvalidChars="1234567890~`!@#$%^&*()_-+={[}]|\:;?/<>,." FilterMode="InvalidChars" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Cut Off Mark </label>
                                        </div>
                                        <asp:TextBox ID="txtPopCutOff" runat="server" CssClass="form-control" ToolTip="Please Enter Cut Off Mark." TabIndex="56" placeholder="Enter Cut Off Mark" AutoComplete="off" MaxLength="5" Enabled="true"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbxtxtCutOff" runat="server" TargetControlID="txtPopCutOff"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:HiddenField ID="hdnCutOff" runat="server" />
                                    </div>
                                </div>
                            </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            
                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Other Details</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-9 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>How do you know B.S.Abdur Rahman Crescent Institute of Science and Technology?</label>
                                        </div>
                                        <asp:DropDownList ID="ddlPopDoYouKnow" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="57" ToolTip="How do you know B.S.Abdur Rahman Crescent Institute of Science and Technology?" data-select2-enable="false" OnSelectedIndexChanged="ddlPopDoYouKnow_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDoYouKnow" runat="server" ControlToValidate="ddlPopDoYouKnow" ValidationGroup="Submit" Display="None" ErrorMessage="Please Select How Do You Know." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divDoyouknow" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Others, Please specify</label>
                                        </div>
                                        <asp:TextBox ID="txtPopDoYouKnow" runat="server" CssClass="form-control" ToolTip="Others, Please specify" TabIndex="58" placeholder="Others, Please specify" AutoComplete="off" MaxLength="128"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender39" runat="server" TargetControlID="txtPopDoYouKnow"
                                            InvalidChars="!~`@#$%^&*_+={[}]|:;'<,>?" FilterMode="InvalidChars" />
                                        <asp:RequiredFieldValidator ID="rfvDoyouknowother" runat="server" ControlToValidate="txtPopDoYouKnow" Display="None" ErrorMessage="Please Enter Other Field." SetFocusOnError="true" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Upload Photo </h5>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <asp:Image ID="imgPhoto" runat="server" Width="128px" Height="128px" ToolTip="Please Upload Photo" Style="margin-bottom: 5px;" />
                                        <asp:FileUpload ID="fuPhotoUpload" runat="server" TabIndex="58" onchange="previewFilePhoto()" accept=".jpg, .jpeg"/>
                                    </div>

                                    <div class="form-group col-lg-7 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading"><span style="color: red">Note</span></h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span style="color: red">jpg or jpeg file required to upload the photo & size should be less than 500 kb.</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Modal footer -->
                        <div class="modal-footer">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnSubmitPopup" runat="server" Font-Bold="true" Text="Submit" CssClass="btn btn-outline-success" TabIndex="59" data-dismiss="modal" ValidationGroup="Submit" OnClick="btnSubmitPopup_Click" />
                                    <asp:Button ID="btnClose" runat="server" Font-Bold="true" Text="Close" CssClass="btn btn-outline-danger" data-dismiss="modal" TabIndex="60" ValidationGroup="b" OnClick="btnClose_Click" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSubmitPopup" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <asp:ValidationSummary ID="vfSubmit" runat="server" ValidationGroup="Submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                    </div>
                </div>
                </span>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <ajaxToolKit:ModalPopupExtender ID="Mp1" runat="server" TargetControlID="lnkPopup" PopupControlID="pnlPopup" BackgroundCssClass="Background" CancelControlID="btnClose"></ajaxToolKit:ModalPopupExtender>


    <asp:Panel ID="pnlPopupForOthers" runat="server">
        <asp:UpdatePanel ID="udpForOthers" runat="server">
            <ContentTemplate>
                <div class="modal-dialog  modal-lg">
                    <div class="modal-content">

                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">Preview</h4>
                        </div>

                        <!-- Modal body -->
                        <div class="modal-body" style="overflow: scroll; height: 450px;">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Registration Details</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-5 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Select Programme(s) you are applying to</label>
                                        </div>
                                        <asp:DropDownList ID="ddlProgramme" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Programme you are applying to" AppendDataBoundItems="true" data-select2-enable="false">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12" id="divSpec" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Specialization</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSpec" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Please Select Specialization." AppendDataBoundItems="true" data-select2-enable="false" AutoPostBack="true" OnSelectedIndexChanged="ddlSpec_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvSpec" runat="server" ControlToValidate="ddlSpec" Display="None" InitialValue="0" SetFocusOnError="true" ErrorMessage="Please Select Specialization." ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                <div class="row" id="divNATA" runat="server" visible="false">
                                    <div class="col-md-12">
                                        <div class="sub-heading">
                                            <h5>
                                                <label id="lblNATA" runat="server"></label>
                                            </h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Score obtained in NATA</label>
                                        </div>
                                        <asp:TextBox ID="txtNATA" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Please Enter Score obtained in NATA" placeholder="Score obtained in NATA" MaxLength="5" AutoComplete="off"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvNATA" runat="server" ControlToValidate="txtNATA" ErrorMessage="Please Enter Score obtained in NATA."
                                                        Display="Dynamic" ValidationGroup="Submit2"></asp:RequiredFieldValidator>--%>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="tbeNATA" runat="server" TargetControlID="txtNATA" FilterMode="ValidChars" ValidChars="0123456789."></ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Personal Details</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Name in Block Letters</label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopName" runat="server" CssClass="form-control" ToolTip="Name in Block Letters" placeholder="Enter Name in Block Letters"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Gender </label>
                                        </div>
                                        <asp:RadioButtonList ID="rdootherPopGender" runat="server" TextAlign="Right" RepeatDirection="Horizontal" ValidationGroup="Submit2" TabIndex="1" ToolTip="Please Select Gender.">
                                            <asp:ListItem Value="1">Male</asp:ListItem>
                                            <asp:ListItem Value="2">Female</asp:ListItem>
                                            <asp:ListItem Value="3">Transgender</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvpop2Gender" runat="server" ControlToValidate="rdootherPopGender" ErrorMessage="Please Select Gender." Display="None" ValidationGroup="Submit2" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>DOB </label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="far fa-calendar-alt" id="i1" runat="server"></i>
                                            </div>
                                            <asp:TextBox ID="txtotherPopDOB" runat="server" TabIndex="2" CssClass="form-control" ValidationGroup="Submit2" placeholder="DD/MM/YYYY" onchange="checkDOBother(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Submit2" ControlToValidate="txtotherPopDOB" Display="None"
                                                ErrorMessage="Please Enter DOB." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtotherPopDOB" PopupButtonID="dobIcon" Enabled="True">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtotherPopDOB"
                                                Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Religion </label>
                                        </div>
                                        <asp:DropDownList ID="ddlotherPopReligion" runat="server" TabIndex="3" CssClass="form-control" ValidationGroup="Submit2" ToolTip="Please Select Religion." AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlotherPopReligion_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Submit2" ControlToValidate="ddlotherPopReligion" Display="None"
                                            ErrorMessage="Please Select Religion." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divotherPopReligion" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Others , Please Specify </label>

                                        </div>
                                        <asp:TextBox ID="txtotherPopOtherReligion" runat="server" CssClass="form-control" ValidationGroup="Submit2" TabIndex="4" ToolTip="Please Enter Other Religion." MaxLength="32" placeholder="Enter Other Religion"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtotherPopOtherReligion" Display="None"
                                            ErrorMessage="Please Enter Other Religion." SetFocusOnError="true" ValidationGroup="Submit2"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtotherPopOtherReligion" FilterMode="InvalidChars"
                                            InvalidChars="0123456789~`!@#$%^&*()_-+={[}]|\:;'<,>?">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Community </label>
                                        </div>
                                        <asp:DropDownList ID="ddlotherPopCommunity" runat="server" TabIndex="4" CssClass="form-control" ValidationGroup="Submit2" ToolTip="Please Select Community." AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Submit2" ControlToValidate="ddlotherPopCommunity" Display="None"
                                            ErrorMessage="Please Select Community." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Mother Tongue </label>
                                        </div>
                                        <asp:DropDownList ID="ddlotherPopTongue" runat="server" TabIndex="5" CssClass="form-control" ValidationGroup="Submit2" ToolTip="Please Select Mother Tongue." AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlotherPopTongue_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Submit2" ControlToValidate="ddlotherPopTongue" Display="None"
                                            ErrorMessage="Please Select Mother Tongue." InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divotherPopTongue" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Others , Please Specify</label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopTongueother" runat="server" CssClass="form-control" ValidationGroup="Submit2" TabIndex="6" ToolTip="Please Enter Mother Tongue." MaxLength="32" placeholder="Enter Other Mother Tongue"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtotherPopTongueother" Display="None"
                                            ErrorMessage="Please Enter Other Mother Tongue." SetFocusOnError="true" ValidationGroup="Submit2"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender32" runat="server" TargetControlID="txtotherPopTongueother" FilterMode="InvalidChars"
                                            InvalidChars="0123456789~`!@#$%^&*()_-+={[}]|\/:;'<,>?">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Aadhaar Card Number </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopAadhar" runat="server" CssClass="form-control" TabIndex="6" ToolTip="Please Enter Aadhar card Number." MaxLength="12" placeholder="Enter Aadhar card Number" AutoComplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="Submit2" ControlToValidate="txtotherPopAadhar" Display="None"
                                            ErrorMessage="Please Enter Aadhaar Card Number." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender33" runat="server" TargetControlID="txtotherPopAadhar" FilterMode="ValidChars"
                                            ValidChars="0123456789">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtotherPopAadhar"
                                            Display="None" SetFocusOnError="True" ValidationExpression="^[100000000000-999999999999]{12}$"
                                            ErrorMessage="Please Enter Valid Aadhaar Card Number" ValidationGroup="Submit2"></asp:RegularExpressionValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Nationality </label>
                                        </div>
                                        <asp:DropDownList ID="ddlotherPopNationality" runat="server" TabIndex="7" CssClass="form-control" ValidationGroup="Submit2" ToolTip="Please Select Nationality." AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="Submit2" ControlToValidate="ddlotherPopNationality" Display="None"
                                            ErrorMessage="Please Select Nationality." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="udpOtherParent" runat="server">
                                <ContentTemplate>
                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Particulars of Parents </h5>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Father Name </label>
                                                </div>
                                                <asp:TextBox ID="txtotherPopFather" runat="server" CssClass="form-control" TabIndex="8" ToolTip="Please Enter Father Name." placeholder="Enter Father Name" MaxLength="150" Style="text-transform: uppercase" AutoComplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="Submit2" ControlToValidate="txtotherPopFather" Display="None"
                                                    ErrorMessage="Please Enter Father Name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender34" runat="server" TargetControlID="txtotherPopFather" FilterMode="InvalidChars"
                                                    InvalidChars="0123456789~`!@#$%^&*()_-+{[}]:;'<,>.?/|\">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Occupation of Father </label>
                                                </div>
                                                <asp:DropDownList ID="ddlotherPopFOcc" runat="server" TabIndex="9" CssClass="form-control" data-select2-enable="false" ValidationGroup="Submit2" ToolTip="Please Select Occupation of Father." AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlotherPopFOcc_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="Submit2" ControlToValidate="ddlotherPopFOcc" Display="None"
                                                    ErrorMessage="Please Select Occupation of Father." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divotherPopFocc" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Others , Please Specify</label>

                                                </div>
                                                <asp:TextBox ID="txtotherPopFoccOther" runat="server" CssClass="form-control" TabIndex="10" ToolTip="Please Enter Other Occupation." placeholder="Enter Father Other Occupation" MaxLength="150"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtotherPopFoccOther" Display="None"
                                                    ErrorMessage="Please Enter Father's Other Occupation." SetFocusOnError="true" ValidationGroup="Submit2"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender37" runat="server" TargetControlID="txtotherPopFoccOther" FilterMode="InvalidChars"
                                                    InvalidChars="0123456789~`!@#$%^&*()_-+{[}]:;'<,>.?/|\">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Father's Mobile Number </label>
                                                </div>
                                                <asp:TextBox ID="txtotherPopFmobile" runat="server" CssClass="form-control" TabIndex="10" ToolTip="Please Enter Father's Mobile Number." placeholder="Enter Father's Mobile Number" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="Submit2" ControlToValidate="txtotherPopFmobile" Display="None"
                                                    ErrorMessage="Please Enter Father's Mobile Number." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender38" runat="server" TargetControlID="txtotherPopFmobile" FilterMode="ValidChars"
                                                    ValidChars="0123456789">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtotherPopFmobile"
                                                    Display="None" SetFocusOnError="True" ValidationExpression="^[0-9]{10}$"
                                                    ErrorMessage="Please Enter Valid Father's Mobile Number." ValidationGroup="Submit2"></asp:RegularExpressionValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Mother Name </label>
                                                </div>
                                                <asp:TextBox ID="txtotherPopMother" runat="server" CssClass="form-control" TabIndex="11" ToolTip="Please Enter Mother Name." placeholder="Enter Mother Name" MaxLength="150" Style="text-transform: uppercase" AutoComplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ValidationGroup="Submit2" ControlToValidate="txtotherPopMother" Display="None"
                                                    ErrorMessage="Please Enter Mother Name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender43" runat="server" TargetControlID="txtotherPopMother" FilterMode="InvalidChars"
                                                    InvalidChars="0123456789~`!@#$%^&*()_-+{[}]:;'<,>.?/|\">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Occupation of Mother </label>
                                                </div>
                                                <asp:DropDownList ID="ddlotherPopMOcc" runat="server" TabIndex="12" CssClass="form-control" data-select2-enable="false" ValidationGroup="Submit2" ToolTip="Please Select Occupation of Mother." AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlotherPopMOcc_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="Submit2" ControlToValidate="ddlotherPopMOcc" Display="None"
                                                    ErrorMessage="Please Select Occupation of Mother." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divotherMOccupation" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Others , Please Specify</label>

                                                </div>
                                                <asp:TextBox ID="txtotherPopMOccOther" runat="server" CssClass="form-control" TabIndex="13" ToolTip="Please Enter Other Occupation." placeholder="Enter Mother Other Occupation" MaxLength="150"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtotherPopMOccOther" Display="None"
                                                    ErrorMessage="Please Enter Mother's Other Occupation." SetFocusOnError="true" ValidationGroup="Submit2"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender44" runat="server" TargetControlID="txtotherPopMOccOther" FilterMode="InvalidChars"
                                                    InvalidChars="0123456789~`!@#$%^&*()_-+{[}]:;'<,>.?/|\">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Mother's Mobile Number </label>
                                                </div>
                                                <asp:TextBox ID="txtotherPopMMobile" runat="server" CssClass="form-control" TabIndex="13" ToolTip="Please Enter Mother's Mobile Number." placeholder="Enter Mother's Mobile Number" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ValidationGroup="Submit2" ControlToValidate="txtotherPopMMobile" Display="None"
                                                    ErrorMessage="Please Enter Mother's Mobile Number." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender45" runat="server" TargetControlID="txtotherPopMMobile" FilterMode="ValidChars"
                                                    ValidChars="0123456789">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtotherPopMMobile"
                                                    Display="None" SetFocusOnError="True" ValidationExpression="^[0-9]{10}$"
                                                    ErrorMessage="Please Enter Valid Mother's Mobile Number." ValidationGroup="Submit2"></asp:RegularExpressionValidator>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Annual income of parents (Includes both parents)</label>
                                                </div>
                                                <asp:DropDownList ID="ddlotherPopIncome" runat="server" CssClass="form-control" TabIndex="14" ToolTip="Please Select Annual Income of Parents." AppendDataBoundItems="true" data-select2-enable="false">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ValidationGroup="Submit2" ControlToValidate="ddlotherPopIncome" InitialValue="0"
                                                    Display="None" ErrorMessage="Please Select Annual Income of Parents." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Address Details </h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Address Line 1 </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopAdd1" runat="server" CssClass="form-control" TabIndex="18" ToolTip="Please Enter Address Line 1." placeholder="Enter Address Line 1" MaxLength="200" AutoComplete="off" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Enter Address Line 1." Display="None" ControlToValidate="txtotherPopAdd1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender46" runat="server" TargetControlID="txtotherPopAdd1" FilterMode="InvalidChars"
                                            InvalidChars="~`!@#$%^&*_+={[}}:;|<>?'">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Address Line 2</label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopAdd2" runat="server" CssClass="form-control" TabIndex="18" ToolTip="Please Enter Address Line 2." placeholder="Enter Address Line 2" MaxLength="200" AutoComplete="off" TextMode="MultiLine"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender47" runat="server" TargetControlID="txtotherPopAdd2" FilterMode="InvalidChars"
                                            InvalidChars="~`!@#$%^&*_+={[}}:;|<>?'">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Address Line 3</label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopAdd3" runat="server" CssClass="form-control" TabIndex="18" ToolTip="Please Enter Address Line 3." placeholder="Enter Address Line 3" MaxLength="200" AutoComplete="off" TextMode="MultiLine"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender48" runat="server" TargetControlID="txtotherPopAdd3" FilterMode="InvalidChars"
                                            InvalidChars="~`!@#$%^&*_+={[}}:;|<>?'">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>State </label>
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlotherPopState" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select State." TabIndex="19" AutoPostBack="true" OnSelectedIndexChanged="ddlotherPopState_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Enter State." Display="None" ControlToValidate="ddlotherPopState" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>District</label>
                                        </div>
                                        <asp:DropDownList ID="ddlotherPopDistrict" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select District." TabIndex="20">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Enter District." Display="None" ControlToValidate="ddlotherPopDistrict" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>City </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopCity" runat="server" CssClass="form-control" TabIndex="21" ToolTip="Please Enter City." placeholder="Enter City" MaxLength="32" AutoComplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Enter City." Display="None" ControlToValidate="txtotherPopCity" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender49" runat="server" TargetControlID="txtotherPopCity" FilterMode="InvalidChars"
                                            InvalidChars="`~!@#$%^&*()_-+={[}]:;<,>.?/0123456789">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Pincode </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopPin" runat="server" CssClass="form-control" TabIndex="22" ToolTip="Please Enter Pincode." MaxLength="6" placeholder="Enter Pincode" AutoComplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Enter Pincode." Display="None" ControlToValidate="txtotherPopPin" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender50" runat="server" TargetControlID="txtotherPopPin" FilterMode="ValidChars"
                                            ValidChars="0123456789">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtotherPopPin"
                                            Display="None" SetFocusOnError="True" ValidationExpression="^[0-9]{6}$"
                                            ErrorMessage="Please Enter Valid Pincode." ValidationGroup="Submit2"></asp:RegularExpressionValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Mobile Number </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopMobile" runat="server" CssClass="form-control" ToolTip="Please Enter Mobile Number." placeholder="Enter Mobile Number" AutoComplete="off" MaxLength="10"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ajxTextMobile" runat="server" TargetControlID="txtotherPopMobile" ValidChars="0123456789" FilterMode="ValidChars"></ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Primary Email ID </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopEmail" runat="server" CssClass="form-control" ToolTip="Please Enter Email." placeholder="Enter Email" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Educational Information </h5>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Educational Information </label>
                                                </div>
                                                <asp:DropDownList ID="ddlotherPopEdu" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select Educational Information." TabIndex="23" AutoPostBack="true" OnSelectedIndexChanged="ddlotherPopEdu_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Enter Educational Information." Display="None" ControlToValidate="ddlotherPopEdu" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divotherPopEdu" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Educational Information (Others) </label>
                                                </div>
                                                <asp:TextBox ID="txtotherPopEduOthers" runat="server" CssClass="form-control" ToolTip="Please Enter Educational Information (Others)." TabIndex="24" placeholder="Enter Educational Information (Others)">                                            
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Enter Educational Information (Others)." Display="None" ControlToValidate="txtotherPopEduOthers" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>12th Exam Registration No. </label>
                                                </div>
                                                <asp:TextBox ID="txtotherPopExmReg" runat="server" CssClass="form-control" ToolTip="Please Enter 12th Exam Registration No." TabIndex="24" placeholder="Enter 12 Exam Registration No" AutoComplete="off" MaxLength="20"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender51" runat="server" TargetControlID="txtotherPopExmReg"
                                                    InvalidChars="~`!@#$%^&*()_-+={[}]:;<,>.?|'" FilterMode="InvalidChars" />
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>School Name </label>
                                                </div>
                                                <asp:TextBox ID="txtotherPopSchool" runat="server" CssClass="form-control" ToolTip="Please Enter School Name" TabIndex="25" placeholder="Enter School Name" AutoComplete="off" MaxLength="200"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender52" runat="server" TargetControlID="txtotherPopSchool"
                                                    InvalidChars="1234567890" FilterMode="InvalidChars" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Enter School Name." Display="None" ControlToValidate="txtotherPopSchool" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Month of Passing/Appearing </label>
                                                </div>
                                                <asp:DropDownList ID="ddlotherPopMPass" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select Month of Passing/Appearing." TabIndex="26">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Enter Month of Passing/Appearing." Display="None" ControlToValidate="ddlotherPopMPass" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Year of Passing/Appearing </label>
                                                </div>
                                                <asp:DropDownList ID="ddlotherPopYPass" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select Year of Passing/Appearing." TabIndex="27">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Enter Year of Passing/Appearing." Display="None" ControlToValidate="ddlotherPopYPass" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Medium of Instruction </label>
                                                </div>
                                                <asp:RadioButtonList ID="rdootherPopMedium" runat="server" TextAlign="Right" RepeatDirection="Horizontal" ValidationGroup="Submit2" TabIndex="28" ToolTip="Please Select Medium of Instruction." AutoPostBack="true" OnSelectedIndexChanged="rdootherPopMedium_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">English</asp:ListItem>
                                                    <asp:ListItem Value="2">Others</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="rdootherPopMedium" ErrorMessage="Please Select Medium of Instruction." Display="None" ValidationGroup="Submit2" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divotherPopMedium" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Medium of Instruction (Others) </label>
                                                </div>
                                                <asp:TextBox ID="txtotherPopMediumOther" runat="server" CssClass="form-control" MaxLength="32" ToolTip="Please Enter Medium of Instruction (Others)" TabIndex="29" placeholder="Medium of Instruction (Others)"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="txtotherPopMediumOther" ErrorMessage="Please Enter Medium of Instruction (Others)." Display="None" ValidationGroup="Submit2" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender53" runat="server" TargetControlID="txtotherPopMediumOther" InvalidChars="0123456789~`!@#$%^&*_-+={{}}|:;'<>?"
                                                    FilterMode="InvalidChars">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Country Name for which School last studied </label>
                                                </div>
                                                <asp:DropDownList ID="ddlotherPopLast" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select Country Name for which School last studied." TabIndex="29">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Select Country Name for which School last studied." Display="None" ControlToValidate="ddlotherPopLast" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Enter The Marks Name If Available </label>
                                                </div>
                                                <asp:RadioButtonList ID="rdootherPopAvailable" runat="server" TextAlign="Right" RepeatDirection="Horizontal" ValidationGroup="Submit2" TabIndex="30" ToolTip="Enter The Marks Name If Available." AutoPostBack="true" OnSelectedIndexChanged="rdootherPopAvailable_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="rdootherPopAvailable" ErrorMessage="Please Select The Marks Name If Available." Display="None" ValidationGroup="Submit2" InitialValue="-1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-12 mt-3" id="divotherPopQual" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Marks Obtained in Qualifying Examination </h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Subject </label>
                                        </div>
                                        <asp:DropDownList ID="ddlotherPopSub1" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select Subject." TabIndex="31" AutoPostBack="true" OnSelectedIndexChanged="ddlotherPopSub1_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Select Subject." ControlToValidate="ddlotherPopSub1" InitialValue="0" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Marks Obtained </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopMarksObt1" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="32" placeholder="Enter Marks Obtained" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_1other();" onblur="checkMaxNoother();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender54" runat="server" TargetControlID="txtotherPopMarksObt1"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Enter Marks Obtained." ControlToValidate="txtotherPopMarksObt1" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Maximum Marks </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopMaxMarks1" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="33" placeholder="Enter Maximum Marks" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_1other();" onblur="checkMaxNoother();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender55" runat="server" TargetControlID="txtotherPopMaxMarks1"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Enter Maximum Marks." ControlToValidate="txtotherPopMaxMarks1" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Percentage </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopPer1" runat="server" CssClass="form-control" ToolTip="Please Enter Percentage." TabIndex="34" AutoComplete="off" MaxLength="5" Enabled="false"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender56" runat="server" TargetControlID="txtotherPopPer1"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Enter Percentage." ControlToValidate="txtotherPopPer1" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="hdnPer_1other" runat="server" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Language </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopLang" runat="server" CssClass="form-control" ToolTip="Please Enter Percentage." TabIndex="35" AutoComplete="off" MaxLength="100"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender57" runat="server" TargetControlID="txtotherPopLang"
                                            InvalidChars=".~`!@#$%^&*()_-+={[}]:;'<,>.?/" FilterMode="InvalidChars" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Marks Obtained </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopMarksObtLang" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="36" placeholder="Enter Marks Obtained" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_Langother();" onblur="checkMaxNoother();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender58" runat="server" TargetControlID="txtotherPopMarksObtLang"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Maximum Marks </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopMaxMarksLang" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="37" placeholder="Enter Maximum Marks" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_Langother();" onblur="checkMaxNoother();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender59" runat="server" TargetControlID="txtotherPopMaxMarksLang"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Percentage </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopPerLang" runat="server" CssClass="form-control" ToolTip="Please Enter Percentage." TabIndex="38" AutoComplete="off" MaxLength="5" Enabled="false"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender60" runat="server" TargetControlID="txtotherPopPerLang"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:HiddenField ID="hdnPerLangother" runat="server" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Subject </label>
                                        </div>
                                        <asp:DropDownList ID="ddlotherPopSub2" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select Subject." TabIndex="39" AutoPostBack="true" OnSelectedIndexChanged="ddlotherPopSub2_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator48" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Select Subject." Display="None" ControlToValidate="ddlotherPopSub2" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="hdnDDL2other" runat="server" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Marks Obtained </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopMarksObt2" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="40" placeholder="Enter Marks Obtained" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_2other();" onblur="checkMaxNoother();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender61" runat="server" TargetControlID="txtotherPopMarksObt2"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator52" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Enter Marks Obtained." Display="None" ControlToValidate="txtotherPopMarksObt2" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Maximum Marks </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopMaxMarks2" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="41" placeholder="Enter Maximum Marks" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_2other();" onblur="checkMaxNoother();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender62" runat="server" TargetControlID="txtotherPopMaxMarks2"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Enter Maximum Marks." Display="None" ControlToValidate="txtotherPopMaxMarks2" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Percentage </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopPer2" runat="server" CssClass="form-control" ToolTip="Please Enter Percentage." TabIndex="42" AutoComplete="off" MaxLength="5" Enabled="false"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender63" runat="server" TargetControlID="txtotherPopPer2"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:HiddenField ID="hdnPer_2other" runat="server" />
                                    </div>

                                     <div class="form-group col-lg-12 col-md-12 col-12" id="divOtherSub2" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Others , Please Specify</label>
                                        </div>
                                        <asp:TextBox ID="txtOtherSub2" runat="server" CssClass="form-control" ToolTip="Please Enter Other Subject 2." TabIndex="1" Width="23%"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ajxSub1" runat="server" TargetControlID="txtOtherSub2" FilterMode="InvalidChars" InvalidChars="~`!@#$%^&*()_-+={[}]|\:;'<,>.?/"></ajaxToolkit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Subject </label>
                                        </div>
                                        <asp:DropDownList ID="ddlotherPopSub3" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select Subject." TabIndex="43" AutoPostBack="true" OnSelectedIndexChanged ="ddlotherPopSub3_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Select Subject." Display="None" ControlToValidate="ddlotherPopSub3" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="hdnDDL3other" runat="server" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Marks Obtained </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopMarksObt3" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="44" placeholder="Enter Marks Obtained" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_3other();" onblur="checkMaxNoother();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender64" runat="server" TargetControlID="txtotherPopMarksObt3"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Enter Marks Obtained." Display="None" ControlToValidate="txtotherPopMarksObt3" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Maximum Marks </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopMaxMarks3" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="45" placeholder="Enter Maximum Marks" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_3other();" onblur="checkMaxNoother();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender65" runat="server" TargetControlID="txtotherPopMaxMarks3"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator56" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Enter Maximum Marks." Display="None" ControlToValidate="txtotherPopMaxMarks3" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Percentage </label>
                                            <asp:TextBox ID="txtotherPopPer3" runat="server" CssClass="form-control" ToolTip="Please Enter Percentage." TabIndex="46" AutoComplete="off" MaxLength="5" Enabled="false"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender66" runat="server" TargetControlID="txtotherPopPer3"
                                                ValidChars="1234567890." FilterMode="ValidChars" />
                                            <asp:HiddenField ID="hdnPer_3other" runat="server" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12 col-md-12 col-12" id="divOtherSub3" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Others , Please Specify</label>
                                        </div>
                                        <asp:TextBox ID="txtOtherSub3" runat="server" CssClass="form-control" ToolTip="Please Enter Other Subject 3." TabIndex="1" Width="23%"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender78" runat="server" TargetControlID="txtOtherSub3" FilterMode="InvalidChars" InvalidChars="~`!@#$%^&*()_-+={[}]|\:;'<,>.?/"></ajaxToolkit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Subject </label>
                                        </div>
                                        <asp:DropDownList ID="ddlotherPopSub4" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select Subject." TabIndex="47" OnSelectedIndexChanged="ddlotherPopSub4_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Select Subject." Display="None" ControlToValidate="ddlotherPopSub4" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="hdnDDL4other" runat="server" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Marks Obtained </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopMarksObt4" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="48" placeholder="Enter Marks Obtained" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_4other();" onblur="checkMaxNoother();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender67" runat="server" TargetControlID="txtotherPopMarksObt4"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator58" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Enter Marks Obtained." Display="None" ControlToValidate="txtotherPopMarksObt4" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Maximum Marks </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopMaxMarks4" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="49" placeholder="Enter Maximum Marks" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_4other();" onblur="checkMaxNoother();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender68" runat="server" TargetControlID="txtotherPopMaxMarks4"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator59" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Enter Maximum Marks." Display="None" ControlToValidate="txtotherPopMaxMarks4" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Percentage </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopPer4" runat="server" CssClass="form-control" ToolTip="Please Enter Percentage." TabIndex="50" AutoComplete="off" MaxLength="51" Enabled="false"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender69" runat="server" TargetControlID="txtotherPopPer4"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:HiddenField ID="hdnPer_4other" runat="server" />
                                    </div>

                                    <div class="form-group col-lg-12 col-md-12 col-12" id="divOtherSub4" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Others , Please Specify</label>
                                        </div>
                                        <asp:TextBox ID="txtOtherSub4" runat="server" CssClass="form-control" ToolTip="Please Enter Other Subject 4." TabIndex="1" Width="23%"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender79" runat="server" TargetControlID="txtOtherSub3" FilterMode="InvalidChars" InvalidChars="~`!@#$%^&*()_-+={[}]|\:;'<,>.?/"></ajaxToolkit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Subject </label>
                                        </div>
                                        <asp:DropDownList ID="ddlotherPopSub5" runat="server" CssClass="form-control" data-select2-enable="false" AppendDataBoundItems="true" ToolTip="Please Select Subject." TabIndex="51" AutoPostBack="true" OnSelectedIndexChanged="ddlotherPopSub5_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator60" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Select Subject." Display="None" ControlToValidate="ddlotherPopSub5" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Marks Obtained </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopMarksObt5" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="52" placeholder="Enter Marks Obtained" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_5other();" onblur="checkMaxNoother();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender70" runat="server" TargetControlID="txtotherPopMarksObt5"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Enter Marks Obtained." Display="None" ControlToValidate="txtotherPopMarksObt5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Maximum Marks </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopMaxMarks5" runat="server" CssClass="form-control" ToolTip="Please Enter Marks Obtained." TabIndex="53" placeholder="Enter Maximum Marks" AutoComplete="off" MaxLength="5" onkeyup="CalculatePer_5other();" onblur="checkMaxNoother();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender71" runat="server" TargetControlID="txtotherPopMaxMarks5"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" ValidationGroup="Submit2" ErrorMessage="Please Enter Maximum Marks." Display="None" ControlToValidate="txtotherPopMaxMarks5" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Percentage </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopPer5" runat="server" CssClass="form-control" ToolTip="Please Enter Percentage." TabIndex="54" AutoComplete="off" MaxLength="5" Enabled="false"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender72" runat="server" TargetControlID="txtotherPopPer5"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:HiddenField ID="hdnPer_5other" runat="server" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divotherPopSpecify" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Others Please Specify </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopSpecify" runat="server" CssClass="form-control" ToolTip="Please Enter Others Please Specify." TabIndex="55" placeholder="Others Please Specify" AutoComplete="off" MaxLength="100"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender73" runat="server" TargetControlID="txtotherPopSpecify"
                                            InvalidChars="1234567890~`!@#$%^&*()_-+={[}]|\:;?/<>,." FilterMode="InvalidChars" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Cut Off Mark </label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopCutOff" runat="server" CssClass="form-control" ToolTip="Please Enter Cut Off Mark." TabIndex="56" placeholder="Enter Cut Off Mark" AutoComplete="off" MaxLength="5" Enabled="true"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender74" runat="server" TargetControlID="txtotherPopCutOff"
                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                        <asp:HiddenField ID="hdnCutOffother" runat="server" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Total </label>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Marks Obtained </label>
                                            <asp:TextBox ID="txtTotObt" runat="server" TabIndex="1" ToolTip="Total Marks Obtained." CssClass="form-control" AutoComplete="Off" MaxLength="5" Enabled="false"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="tbetxtTotObt" runat="server" TargetControlID="txtTotObt" FilterMode="ValidChars" ValidChars="0123456789."></ajaxToolkit:FilteredTextBoxExtender>
                                            <asp:HiddenField ID="hdnTotObt" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Maximum Marks </label>
                                            <asp:TextBox ID="txtTotMax" runat="server" TabIndex="1" ToolTip="Total Maximum Marks." CssClass="form-control" AutoComplete="Off" MaxLength="5" Enabled="false"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender76" runat="server" TargetControlID="txtTotMax" FilterMode="ValidChars" ValidChars="0123456789."></ajaxToolkit:FilteredTextBoxExtender>
                                            <asp:HiddenField ID="hdnTotMax" runat="server" />

                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Percentage</label>
                                            <asp:TextBox ID="txtTotPer" runat="server" TabIndex="1" ToolTip="Total Percentage." CssClass="form-control" AutoComplete="Off" MaxLength="5" Enabled="false"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender77" runat="server" TargetControlID="txtTotPer" FilterMode="ValidChars" ValidChars="0123456789."></ajaxToolkit:FilteredTextBoxExtender>
                                            <asp:HiddenField ID="hdnTotPer" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Other Details</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-9 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>How do you know B.S.Abdur Rahman Crescent Institute of Science and Technology?</label>
                                        </div>
                                        <asp:DropDownList ID="ddlotherPopDoYouKnow" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="57" ToolTip="How do you know B.S.Abdur Rahman Crescent Institute of Science and Technology?" data-select2-enable="false" AutoPostBack="true" OnSelectedIndexChanged="ddlotherPopDoYouKnow_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" ControlToValidate="ddlotherPopDoYouKnow" ValidationGroup="Submit2" Display="None" ErrorMessage="Please Select How Do You Know." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divotherDoyouknow" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Others, Please specify</label>
                                        </div>
                                        <asp:TextBox ID="txtotherPopDoYouKnow" runat="server" CssClass="form-control" ToolTip="Others, Please specify" TabIndex="58" placeholder="Others, Please specify" AutoComplete="off" MaxLength="128"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender75" runat="server" TargetControlID="txtotherPopDoYouKnow"
                                            InvalidChars="!~`@#$%^&*_+={[}]|:;'<,>?" FilterMode="InvalidChars" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator64" runat="server" ControlToValidate="txtotherPopDoYouKnow" Display="None" ErrorMessage="Please Enter Other Field." SetFocusOnError="true" ValidationGroup="Submit2"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Upload Photo </h5>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <asp:Image ID="imgotherPhoto" runat="server" Width="128px" Height="128px" ToolTip="Please Upload Photo" Style="margin-bottom: 5px;" />
                                        <asp:FileUpload ID="fuotherPhotoUpload" runat="server" TabIndex="58" onchange="previewFilePhoto()" accept=".jpg, .jpeg" />
                                    </div>

                                    <div class="form-group col-lg-7 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading"><span style="color: red">Note</span></h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span style="color: red">jpg or jpeg file required to upload the photo & size should be less than 500 kb.</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Modal footer -->
                        <div class="modal-footer">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnSubmitPopup2" runat="server" Font-Bold="true" Text="Submit" CssClass="btn btn-outline-success" TabIndex="59" data-dismiss="modal" ValidationGroup="Submit2" OnClick="btnSubmitPopup2_Click"/>
                                    <asp:Button ID="btnpop2Close" runat="server" Font-Bold="true" Text="Close" CssClass="btn btn-outline-danger" data-dismiss="modal" TabIndex="60" ValidationGroup="c" OnClick="btnpop2Close_Click"/>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSubmitPopup2" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Submit2" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                    </div>
                </div>
                </span>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <ajaxToolKit:ModalPopupExtender ID="Mp2ForOthers" runat="server" TargetControlID="lnkPopup" PopupControlID="pnlPopupForOthers" BackgroundCssClass="Background" CancelControlID="btnpop2Close"></ajaxToolKit:ModalPopupExtender>


    <script>
        function ClosePopup() {
            $('#pnlPopup1').modal('hide');
        }
        function Open() {
            $('#pnlPopup1').modal('show');
        }

        $(document).ready(function () {
            $("[data-select2-enable=true]").addClass("select2 select-clik");
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("[data-select2-enable=true]").addClass("select2 select-clik");
        });

        $(document).ready(function () {
            $('.select2').select2({
                dropdownAutoWidth: true,
                width: '100%',
            })
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.select2').select2({
                dropdownAutoWidth: true,
                width: '100%',
            })
        });

        $(document).ready(function () {
            $(document).on("click", ".select2-search-clear-icon", function () {
                var sel2id = localStorage.getItem("sel2id");
                $('#' + sel2id).select2('close');
                $('#' + sel2id).select2('open');
            });

            $(document).on('click', '.select2', function () {
                debugger
                var key = $(this).parent().find('.select-clik').attr('id');
                localStorage.setItem("sel2id", key);
            });
        });
        </script>

        <script type="text/javascript">
            function previewFilePhoto() {
                if (validateFileSize()) {
                    $("#imgPhoto").show();
                    var preview = document.querySelector('#<%=imgPhoto.ClientID %>');
                    var file = document.querySelector('#<%=fuPhotoUpload.ClientID %>').files[0];
                    var reader = new FileReader();

                    reader.onloadend = function () {
                        preview.src = reader.result;
                    }

                    if (file) {

                        reader.readAsDataURL(file);

                    } else {
                        preview.src = "";
                    }
                }
                else {
                    alert('File size should be less than or equal to 500KB.');
                    $imgPhotoNoCrop.val('');
                    return;
                }
            }

            function validateFileSize() {
                var uploadControl = document.getElementById('<%= fuPhotoUpload.ClientID %>');
                if (uploadControl.files[0].size > 500000) {
                    return false;
                }
                else {
                    return true;
                }
            }
            function checkDOB() {
                var dateFrom = "31/12/2000";                            // Starting from 01/07/2001
                var dateTo = "01/01/2007";                                // End at 31/12/2006
                var dateCheck = document.getElementById("txtPopDOB").value;
                var d1 = dateFrom.split("/");
                var d2 = dateTo.split("/");
                var c = dateCheck.split("/");

                var from = new Date(d1[2], parseInt(d1[1]) - 1, d1[0]);  // -1 because months are from 0 to 11                
                var to = new Date(d2[2], parseInt(d2[1]) - 1, d2[0]);
                var check = new Date(c[2], parseInt(c[1]) - 1, c[0]);
                if (check > from && check < to) {
                }
                else {
                    alert('Date ' + dateCheck + ' is not acceptable.');
                    document.getElementById("txtPopDOB").value = "";
                }
            }
            function checkDOBother() {
                var dateFrom = "31/12/2000";                            // Starting from 01/07/2001
                var dateTo = "01/01/2007";                                // End at 31/12/2006
                var dateCheck = document.getElementById("txtotherPopDOB").value;
                var d1 = dateFrom.split("/");
                var d2 = dateTo.split("/");
                var c = dateCheck.split("/");

                var from = new Date(d1[2], parseInt(d1[1]) - 1, d1[0]);  // -1 because months are from 0 to 11                
                var to = new Date(d2[2], parseInt(d2[1]) - 1, d2[0]);
                var check = new Date(c[2], parseInt(c[1]) - 1, c[0]);
                if (check > from && check < to) {
                }
                else {
                    alert('Date ' + dateCheck + ' is not acceptable.');
                    document.getElementById("txtotherPopDOB").value = "";
                }
            }
            function checkDate() {
                var selectDate = document.getElementById("txtDDDate").value;
                var today = new Date();
                var dd = String(today.getDate()).padStart(2, '0');
                var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
                var yyyy = today.getFullYear();
                var selDate = selectDate.split("/");
                var selday = selDate[0];
                var selmonth = selDate[1] - 1;
                var selyear = selDate[2];
                var SDate = new Date(selyear, selmonth, selday);
                if (SDate > today) {
                    alert('Future Date Cannot Be Selected.');
                    document.getElementById('txtDDDate').value = "";
                    document.getElementById('txtDDDate').focus();
                    return true;
                }
                else {
                    return false;
                }
            }
        </script>

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
                if (admBatch.value == 0) {
                    alert('Please Select Admission Batch.');
                    return false;
                }
        }
            function checkDOB() {
                var dateFrom = "31/12/2000";                            // Starting from 01/07/2001
                var dateTo = "01/01/2007";                                // End at 31/12/2006
                var dateCheck = document.getElementById("txtPopDOB").value;
                var d1 = dateFrom.split("/");
                var d2 = dateTo.split("/");
                var c = dateCheck.split("/");

                var from = new Date(d1[2], parseInt(d1[1]) - 1, d1[0]);  // -1 because months are from 0 to 11                
                var to = new Date(d2[2], parseInt(d2[1]) - 1, d2[0]);
                var check = new Date(c[2], parseInt(c[1]) - 1, c[0]);
                if (check > from && check < to) {
                }
                else {
                    alert('Date ' + dateCheck + ' is not acceptable.');
                    document.getElementById("txtPopDOB").value = "";
                }
            }
            function checkDOBother() {
                var dateFrom = "31/12/2000";                            // Starting from 01/07/2001
                var dateTo = "01/01/2007";                                // End at 31/12/2006
                var dateCheck = document.getElementById("txtotherPopDOB").value;
                var d1 = dateFrom.split("/");
                var d2 = dateTo.split("/");
                var c = dateCheck.split("/");

                var from = new Date(d1[2], parseInt(d1[1]) - 1, d1[0]);  // -1 because months are from 0 to 11                
                var to = new Date(d2[2], parseInt(d2[1]) - 1, d2[0]);
                var check = new Date(c[2], parseInt(c[1]) - 1, c[0]);
                if (check > from && check < to) {
                }
                else {
                    alert('Date ' + dateCheck + ' is not acceptable.');
                    document.getElementById("txtotherPopDOB").value = "";
                }
            }
    </script>
    <script>
        function CalculatePer_1() {
            var marks_obtained_1 = 0.00;
            var outOfMarks_1 = 0.00;
            var percentage_1 = 0.00;
            var obt1 = document.getElementById('<%=txtPopMarksObt1.ClientID%>').value;
                var outOfMarks1 = document.getElementById('<%=txtPopMaxMarks1.ClientID%>').value;
                if (!outOfMarks1 == "")
                    percentage_1 = (obt1 / outOfMarks1) * 100;
                document.getElementById('<%=txtPopPer1.ClientID%>').value = parseFloat(percentage_1).toFixed(2);
                document.getElementById('<%=hdnPer_1.ClientID%>').value = parseFloat(percentage_1).toFixed(2);
            }
            function CalculatePer_Lang() {
                var marks_obtained_Lang = 0.00;
                var outOfMarks_Lang = 0.00;
                var percentage_Lang = 0.00;
                var obtLang = document.getElementById('<%=txtPopMarksObtLang.ClientID%>').value;
                var outOfMarksLang = document.getElementById('<%=txtPopMaxMarksLang.ClientID%>').value;

                if (!outOfMarksLang == "")
                    percentage_Lang = (obtLang / outOfMarksLang) * 100;
                document.getElementById('<%=txtPopPerLang.ClientID%>').value = parseFloat(percentage_Lang).toFixed(2);
                document.getElementById('<%=hdnPerLang.ClientID%>').value = parseFloat(percentage_Lang).toFixed(2);
            }

            function CalculatePer_2() {
                var marks_obtained_2 = 0.00;
                var outOfMarks_2 = 0.00;
                var percentage_2 = 0.00;
                var obt2 = document.getElementById('<%=txtPopMarksObt2.ClientID%>').value;
                var outOfMarks2 = document.getElementById('<%=txtPopMaxMarks2.ClientID%>').value;

                if (!outOfMarks2 == "")
                    percentage_2 = (obt2 / outOfMarks2) * 100;
                document.getElementById('<%=txtPopPer2.ClientID%>').value = parseFloat(percentage_2).toFixed(2);
                document.getElementById('<%=hdnPer_2.ClientID%>').value = parseFloat(percentage_2).toFixed(2);
                CutOff();

            }

            function CalculatePer_3() {
                var marks_obtained_3 = 0.00;
                var outOfMarks_3 = 0.00;
                var percentage_3 = 0.00;
                var obt3 = document.getElementById('<%=txtPopMarksObt3.ClientID%>').value;
                var outOfMarks3 = document.getElementById('<%=txtPopMaxMarks3.ClientID%>').value;
                if (!outOfMarks3 == "")
                    percentage_3 = (obt3 / outOfMarks3) * 100;
                document.getElementById('<%=txtPopPer3.ClientID%>').value = parseFloat(percentage_3).toFixed(2);
                document.getElementById('<%=hdnPer_3.ClientID%>').value = parseFloat(percentage_3).toFixed(2);
                CutOff();
            }

        function CalculatePer_4() {

                debugger;
                var marks_obtained_4 = 0.00;
                var outOfMarks_4 = 0.00;
                var percentage_4 = 0.00;
                var obt4 = document.getElementById('<%=txtPopMarksObt4.ClientID%>').value;
                var outOfMarks4 = document.getElementById('<%=txtPopMaxMarks4.ClientID%>').value;
                if (!outOfMarks4 == "")
                    percentage_4 = (obt4 / outOfMarks4) * 100;
                document.getElementById('<%=txtPopPer4.ClientID%>').value = parseFloat(percentage_4).toFixed(2);
                document.getElementById('<%=hdnPer_4.ClientID%>').value = parseFloat(percentage_4).toFixed(2);
                CutOff();
            }
            function CalculatePer_5() {
                var marks_obtained_5 = 0.00;
                var outOfMarks_5 = 0.00;
                var percentage_5 = 0.00;
                var obt5 = document.getElementById('<%=txtPopMarksObt5.ClientID%>').value;
                var outOfMarks5 = document.getElementById('<%=txtPopMaxMarks5.ClientID%>').value;
                if (!outOfMarks5 == "")
                    percentage_5 = (obt5 / outOfMarks5) * 100;
                document.getElementById('<%=txtPopPer5.ClientID%>').value = parseFloat(percentage_5).toFixed(2);
                document.getElementById('<%=hdnPer_5.ClientID%>').value = parseFloat(percentage_5).toFixed(2);
            }

            function CutOff() {
                //alert("in");
                var hidden2 = document.getElementById('<%=hdnDDL2.ClientID%>').value;
                var cutOff_Obt = 0.00;
                var cutoff_Max = 0.00;
                var cutOff = 0.00;
                var hidden3 = document.getElementById('<%=hdnDDL3.ClientID%>').value;
                var hidden4 = document.getElementById('<%=hdnDDL4.ClientID%>').value;
                var marks_obtained_2 = document.getElementById('<%=txtPopMarksObt2.ClientID%>').value;
                var marks_obtained_3 = document.getElementById('<%=txtPopMarksObt3.ClientID%>').value;
                var marks_obtained_4 = document.getElementById('<%=txtPopMarksObt4.ClientID%>').value;
                var outOfMarks2 = document.getElementById('<%=txtPopMaxMarks2.ClientID%>').value;
                var outOfMarks3 = document.getElementById('<%=txtPopMaxMarks3.ClientID%>').value;
                var outOfMarks4 = document.getElementById('<%=txtPopMaxMarks4.ClientID%>').value;
                if (hidden2 == 1 && hidden3 == 1 && hidden4 == 1) {
                    cutOff_Obt = (parseFloat(marks_obtained_2) + parseFloat(marks_obtained_3) + parseFloat(marks_obtained_4)) * 100;
                    cutoff_Max = parseFloat(outOfMarks2) + parseFloat(outOfMarks3) + parseFloat(outOfMarks4);
                    cutOff = (cutOff_Obt / cutoff_Max);
                    document.getElementById('<%=txtPopCutOff.ClientID%>').value = parseFloat(cutOff).toFixed(2);
                    document.getElementById('<%=hdnCutOff.ClientID%>').value = parseFloat(cutOff).toFixed(2);
                }
            }


        function CalculatePer_1other() {
            var marks_obtained_1 = 0.00;
            var outOfMarks_1 = 0.00;
            var percentage_1 = 0.00;
            var obt1 = document.getElementById('<%=txtotherPopMarksObt1.ClientID%>').value;
                var outOfMarks1 = document.getElementById('<%=txtotherPopMaxMarks1.ClientID%>').value;
            //alert(marks_obtained_1);
            //alert(percentage_1);
            //alert(outOfMarks1);

            if (!outOfMarks1 == "")
                percentage_1 = (obt1 / outOfMarks1) * 100;
            document.getElementById('<%=txtotherPopPer1.ClientID%>').value = parseFloat(percentage_1).toFixed(2);
                document.getElementById('<%=hdnPer_1other.ClientID%>').value = parseFloat(percentage_1).toFixed(2);
            OverAllPercent();
        }

        function CalculatePer_Langother() {
            var marks_obtained_Lang = 0.00;
            var outOfMarks_Lang = 0.00;
            var percentage_Lang = 0.00;
            var obtLang = document.getElementById('<%=txtotherPopMarksObtLang.ClientID%>').value;
                var outOfMarksLang = document.getElementById('<%=txtotherPopMaxMarksLang.ClientID%>').value;


                if (!outOfMarksLang == "")
                    percentage_Lang = (obtLang / outOfMarksLang) * 100;
                document.getElementById('<%=txtotherPopPerLang.ClientID%>').value = parseFloat(percentage_Lang).toFixed(2);
                document.getElementById('<%=hdnPerLangother.ClientID%>').value = parseFloat(percentage_Lang).toFixed(2);
                OverAllPercent();
            }

        function CalculatePer_2other() {
                var marks_obtained_2 = 0.00;
                var outOfMarks_2 = 0.00;
                var percentage_2 = 0.00;
                var obt2 = document.getElementById('<%=txtotherPopMarksObt2.ClientID%>').value;
                var outOfMarks2 = document.getElementById('<%=txtotherPopMaxMarks2.ClientID%>').value;

                if (!outOfMarks2 == "")
                    percentage_2 = (obt2 / outOfMarks2) * 100;
                document.getElementById('<%=txtotherPopPer2.ClientID%>').value = parseFloat(percentage_2).toFixed(2);
                document.getElementById('<%=hdnPer_2other.ClientID%>').value = parseFloat(percentage_2).toFixed(2);
                OverAllPercent();
                //CutOff();

            }

        function CalculatePer_3other() {
                var marks_obtained_3 = 0.00;
                var outOfMarks_3 = 0.00;
                var percentage_3 = 0.00;
                var obt3 = document.getElementById('<%=txtotherPopMarksObt3.ClientID%>').value;
                var outOfMarks3 = document.getElementById('<%=txtotherPopMaxMarks3.ClientID%>').value;
                if (!outOfMarks3 == "")
                    percentage_3 = (obt3 / outOfMarks3) * 100;
                document.getElementById('<%=txtotherPopPer3.ClientID%>').value = parseFloat(percentage_3).toFixed(2);
                document.getElementById('<%=hdnPer_3other.ClientID%>').value = parseFloat(percentage_3).toFixed(2);
                OverAllPercent();
                //CutOff();
            }

        function CalculatePer_4other() {
                //document.getElementById('<%=txtotherPopMaxMarks4.ClientID%>').value="";
                var marks_obtained_4 = 0.00;
                var outOfMarks_4 = 0.00;
                var percentage_4 = 0.00;
                var obt4 = document.getElementById('<%=txtotherPopMarksObt4.ClientID%>').value;
                var outOfMarks4 = document.getElementById('<%=txtotherPopMaxMarks4.ClientID%>').value;
                if (!outOfMarks4 == "")
                    percentage_4 = (obt4 / outOfMarks4) * 100;
                document.getElementById('<%=txtotherPopPer4.ClientID%>').value = parseFloat(percentage_4).toFixed(2);
                document.getElementById('<%=hdnPer_4other.ClientID%>').value = parseFloat(percentage_4).toFixed(2);
                //CutOff();
                OverAllPercent();
            }
        function CalculatePer_5other() {
                var marks_obtained_5 = 0.00;
                var outOfMarks_5 = 0.00;
                var percentage_5 = 0.00;
                var obt5 = document.getElementById('<%=txtotherPopMarksObt5.ClientID%>').value;
                var outOfMarks5 = document.getElementById('<%=txtotherPopMaxMarks5.ClientID%>').value;
                if (!outOfMarks5 == "")
                    percentage_5 = (obt5 / outOfMarks5) * 100;
                document.getElementById('<%=txtotherPopPer5.ClientID%>').value = parseFloat(percentage_5).toFixed(2);
                document.getElementById('<%=hdnPer_5other.ClientID%>').value = parseFloat(percentage_5).toFixed(2);
                OverAllPercent();
            }

            function CutOff() {
                var hidden2 = document.getElementById('<%=hdnDDL2other.ClientID%>').value;
                var cutOff_Obt = 0.00;
                var cutoff_Max = 0.00;
                var cutOff = 0.00;
                var hidden3 = document.getElementById('<%=hdnDDL3other.ClientID%>').value;
                var hidden4 = document.getElementById('<%=hdnDDL4other.ClientID%>').value;
                var marks_obtained_2 = document.getElementById('<%=txtotherPopMarksObt2.ClientID%>').value;
                var marks_obtained_3 = document.getElementById('<%=txtotherPopMarksObt3.ClientID%>').value;
                var marks_obtained_4 = document.getElementById('<%=txtotherPopMarksObt4.ClientID%>').value;
                var outOfMarks2 = document.getElementById('<%=txtotherPopMaxMarks2.ClientID%>').value;
                var outOfMarks3 = document.getElementById('<%=txtotherPopMaxMarks3.ClientID%>').value;
                var outOfMarks4 = document.getElementById('<%=txtotherPopMaxMarks4.ClientID%>').value;
                if (hidden2 == 1 && hidden3 == 1 && hidden4 == 1) {
                    cutOff_Obt = (parseFloat(marks_obtained_2) + parseFloat(marks_obtained_3) + parseFloat(marks_obtained_4)) * 100;
                    cutoff_Max = parseFloat(outOfMarks2) + parseFloat(outOfMarks3) + parseFloat(outOfMarks4);
                    cutOff = (cutOff_Obt / cutoff_Max);
                    document.getElementById('<%=txtotherPopCutOff.ClientID%>').value = parseFloat(cutOff).toFixed(2);
                    document.getElementById('<%=hdnCutOffother.ClientID%>').value = parseFloat(cutOff).toFixed(2);
                }
            }

        function OverAllPercent() {
            var obt_1 = document.getElementById('<%=txtotherPopMarksObt1.ClientID%>').value;

             var obt_Lang = document.getElementById('<%=txtotherPopMarksObtLang.ClientID%>').value;
             var obt_2 = document.getElementById('<%=txtotherPopMarksObt2.ClientID%>').value;
             var obt_3 = document.getElementById('<%=txtotherPopMarksObt3.ClientID%>').value;
             var obt_4 = document.getElementById('<%=txtotherPopMarksObt4.ClientID%>').value;
             var obt_5 = document.getElementById('<%=txtotherPopMarksObt5.ClientID%>').value;
             if (obt_1 == '') {
                 obt_1 = 0;
             }
             if (obt_Lang == '') {
                 obt_Lang = 0;
             }
             if (obt_2 == '') {
                 obt_2 = 0;
             }
             if (obt_3 == '') {
                 obt_3 = 0;
             }
             if (obt_4 == '') {
                 obt_4 = 0;
             }
             if (obt_5 == '') {
                 obt_5 = 0;
             }

             var max_1 = document.getElementById('<%=txtotherPopMaxMarks1.ClientID%>').value;
                var max_Lang = document.getElementById('<%=txtotherPopMaxMarksLang.ClientID%>').value;
             var max_2 = document.getElementById('<%=txtotherPopMaxMarks2.ClientID%>').value;
             var max_3 = document.getElementById('<%=txtotherPopMaxMarks3.ClientID%>').value;
             var max_4 = document.getElementById('<%=txtotherPopMaxMarks4.ClientID%>').value;
             var max_5 = document.getElementById('<%=txtotherPopMaxMarks5.ClientID%>').value;

             if (max_1 == '') {
                 max_1 = 0;
             }
             if (max_Lang == '') {
                 max_Lang = 0;
             }
             if (max_2 == '') {
                 max_2 = 0;
             }
             if (max_3 == '') {
                 max_3 = 0;
             }
             if (max_4 == '') {
                 max_4 = 0;
             }
             if (max_5 == '') {
                 max_5 = 0;
             }
             var overAllObt = parseFloat(obt_1) + parseFloat(obt_Lang) + parseFloat(obt_2) + parseFloat(obt_3) + parseFloat(obt_4) + parseFloat(obt_5)
             document.getElementById('<%=txtTotObt.ClientID%>').value = parseFloat(overAllObt).toFixed(2);

                var overAllMax = parseFloat(max_1) + parseFloat(max_Lang) + parseFloat(max_2) + parseFloat(max_3) + parseFloat(max_4) + parseFloat(max_5)
                document.getElementById('<%=txtTotMax.ClientID%>').value = parseFloat(overAllMax).toFixed(2);

                var overAllPer = parseFloat(overAllObt) * 100 / parseFloat(overAllMax);
                document.getElementById('<%=txtTotPer.ClientID%>').value = parseFloat(overAllPer).toFixed(2);
                document.getElementById('<%=hdnTotObt.ClientID%>').value = parseFloat(overAllObt).toFixed(2);
             document.getElementById('<%=hdnTotMax.ClientID%>').value = parseFloat(overAllMax).toFixed(2);
             document.getElementById('<%=hdnTotPer.ClientID%>').value = parseFloat(overAllPer).toFixed(2);
         }
        </script>

        <script>
            function checkMaxNo() {
                var obt1 = document.getElementById('<%=txtPopMarksObt1.ClientID%>').value;
                var outOfMarks1 = document.getElementById('<%=txtPopMaxMarks1.ClientID%>').value;

                var obtLang = document.getElementById('<%=txtPopMarksObtLang.ClientID%>').value;
                var outOfMarksLang = document.getElementById('<%=txtPopMaxMarksLang.ClientID%>').value;

                var obt2 = document.getElementById('<%=txtPopMarksObt2.ClientID%>').value;
                var outOfMarks2 = document.getElementById('<%=txtPopMaxMarks2.ClientID%>').value;

                var obt3 = document.getElementById('<%=txtPopMarksObt3.ClientID%>').value;
                var outOfMarks3 = document.getElementById('<%=txtPopMaxMarks3.ClientID%>').value;

                var obt4 = document.getElementById('<%=txtPopMarksObt4.ClientID%>').value;
                var outOfMarks4 = document.getElementById('<%=txtPopMaxMarks4.ClientID%>').value;

                var obt5 = document.getElementById('<%=txtPopMarksObt5.ClientID%>').value;
                var outOfMarks5 = document.getElementById('<%=txtPopMaxMarks5.ClientID%>').value;

                if (obt1 != "" && outOfMarks1 != "") {
                    if (parseFloat(obt1) > parseFloat(outOfMarks1)) {
                        alert("Maximum Marks Should Be Less Than Obtained Marks.");
                        document.getElementById('<%=txtPopMaxMarks1.ClientID%>').value = "";
                        document.getElementById('<%=txtPopPer1.ClientID%>').value = "";
                        document.getElementById('<%=txtPopMaxMarks1.ClientID%>').focus();
                    }
                }

                if (obtLang != "" && outOfMarksLang != "") {
                    if (parseFloat(obtLang) > parseFloat(outOfMarksLang)) {
                        alert("Maximum Marks Should Be Less Than Obtained Marks.");
                        document.getElementById('<%=txtPopMaxMarksLang.ClientID%>').value = "";
                        document.getElementById('<%=txtPopPerLang.ClientID%>').value = "";
                        document.getElementById('<%=txtPopMaxMarksLang.ClientID%>').focus();
                    }
                }
                if (obt2 != "" && outOfMarks2 != "") {
                    if (parseFloat(obt2) > parseFloat(outOfMarks2)) {
                        alert("Maximum Marks Should Be Less Than Obtained Marks.");
                        document.getElementById('<%=txtPopMaxMarks2.ClientID%>').value = "";
                        document.getElementById('<%=txtPopPer2.ClientID%>').value = "";
                        document.getElementById('<%=txtPopMaxMarks2.ClientID%>').focus();
                    }
                }

                if (obt3 != "" && outOfMarks3 != "") {
                    if (parseFloat(obt3) > parseFloat(outOfMarks3)) {
                        alert("Maximum Marks Should Be Less Than Obtained Marks.");
                        document.getElementById('<%=txtPopMaxMarks3.ClientID%>').value = "";
                        document.getElementById('<%=txtPopPer3.ClientID%>').value = "";
                        document.getElementById('<%=txtPopMaxMarks3.ClientID%>').focus();
                    }
                }

                if (obt4 != "" && outOfMarks4 != "") {
                    if (parseFloat(obt4) > parseFloat(outOfMarks4)) {
                        alert("Maximum Marks Should Be Less Than Obtained Marks.");
                        document.getElementById('<%=txtPopMaxMarks4.ClientID%>').value = "";
                        document.getElementById('<%=txtPopPer4.ClientID%>').value = "";
                        document.getElementById('<%=txtPopMaxMarks4.ClientID%>').focus();
                    }
                }
                if (obt5 != "" && outOfMarks5 != "") {
                    if (parseFloat(obt5) > parseFloat(outOfMarks5)) {
                        alert("Maximum Marks Should Be Less Than Obtained Marks.");
                        document.getElementById('<%=txtPopMaxMarks5.ClientID%>').value = "";
                        document.getElementById('<%=txtPopPer5.ClientID%>').value = "";
                        document.getElementById('<%=txtPopMaxMarks5.ClientID%>').focus();
                    }
                }
            }


            function checkMaxNoother() {
                var obt1 = document.getElementById('<%=txtotherPopMarksObt1.ClientID%>').value;
                var outOfMarks1 = document.getElementById('<%=txtotherPopMaxMarks1.ClientID%>').value;

                var obtLang = document.getElementById('<%=txtotherPopMarksObtLang.ClientID%>').value;
                var outOfMarksLang = document.getElementById('<%=txtotherPopMaxMarksLang.ClientID%>').value;

                var obt2 = document.getElementById('<%=txtotherPopMarksObt2.ClientID%>').value;
                var outOfMarks2 = document.getElementById('<%=txtotherPopMaxMarks2.ClientID%>').value;

                var obt3 = document.getElementById('<%=txtotherPopMarksObt3.ClientID%>').value;
                var outOfMarks3 = document.getElementById('<%=txtotherPopMaxMarks3.ClientID%>').value;

                var obt4 = document.getElementById('<%=txtotherPopMarksObt4.ClientID%>').value;
                var outOfMarks4 = document.getElementById('<%=txtPopMaxMarks4.ClientID%>').value;

                var obt5 = document.getElementById('<%=txtotherPopMarksObt5.ClientID%>').value;
                var outOfMarks5 = document.getElementById('<%=txtotherPopMaxMarks5.ClientID%>').value;

                if (obt1 != "" && outOfMarks1 != "") {
                    if (parseFloat(obt1) > parseFloat(outOfMarks1)) {
                        alert("Maximum Marks Should Be Less Than Obtained Marks.");
                        document.getElementById('<%=txtotherPopMaxMarks1.ClientID%>').value = "";
                        document.getElementById('<%=txtotherPopPer1.ClientID%>').value = "";
                        document.getElementById('<%=txtotherPopMaxMarks1.ClientID%>').focus();
                    }
                }

                if (obtLang != "" && outOfMarksLang != "") {
                    if (parseFloat(obtLang) > parseFloat(outOfMarksLang)) {
                        alert("Maximum Marks Should Be Less Than Obtained Marks.");
                        document.getElementById('<%=txtotherPopMaxMarksLang.ClientID%>').value = "";
                        document.getElementById('<%=txtotherPopPerLang.ClientID%>').value = "";
                        document.getElementById('<%=txtotherPopMaxMarksLang.ClientID%>').focus();
                    }
                }
                if (obt2 != "" && outOfMarks2 != "") {
                    if (parseFloat(obt2) > parseFloat(outOfMarks2)) {
                        alert("Maximum Marks Should Be Less Than Obtained Marks.");
                        document.getElementById('<%=txtotherPopMaxMarks2.ClientID%>').value = "";
                        document.getElementById('<%=txtotherPopPer2.ClientID%>').value = "";
                        document.getElementById('<%=txtotherPopMaxMarks2.ClientID%>').focus();
                    }
                }

                if (obt3 != "" && outOfMarks3 != "") {
                    if (parseFloat(obt3) > parseFloat(outOfMarks3)) {
                        alert("Maximum Marks Should Be Less Than Obtained Marks.");
                        document.getElementById('<%=txtotherPopMaxMarks3.ClientID%>').value = "";
                        document.getElementById('<%=txtotherPopPer3.ClientID%>').value = "";
                        document.getElementById('<%=txtotherPopMaxMarks3.ClientID%>').focus();
                    }
                }

                if (obt4 != "" && outOfMarks4 != "") {
                    if (parseFloat(obt4) > parseFloat(outOfMarks4)) {
                        alert("Maximum Marks Should Be Less Than Obtained Marks.");
                        document.getElementById('<%=txtotherPopMaxMarks4.ClientID%>').value = "";
                        document.getElementById('<%=txtotherPopPer4.ClientID%>').value = "";
                        document.getElementById('<%=txtotherPopMaxMarks4.ClientID%>').focus();
                    }
                }
                if (obt5 != "" && outOfMarks5 != "") {
                    if (parseFloat(obt5) > parseFloat(outOfMarks5)) {
                        alert("Maximum Marks Should Be Less Than Obtained Marks.");
                        document.getElementById('<%=txtotherPopMaxMarks5.ClientID%>').value = "";
                        document.getElementById('<%=txtotherPopPer5.ClientID%>').value = "";
                        document.getElementById('<%=txtotherPopMaxMarks5.ClientID%>').focus();
                    }
                }
            }
                
        </script>
   
</asp:Content>

