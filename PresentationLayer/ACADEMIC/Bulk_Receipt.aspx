<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Bulk_Receipt.aspx.cs" Inherits="ACADEMIC_Bulk_Receipt" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="pnlFeeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">BULK FEES REPORT</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                            TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                    </div>--%>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <%--<label>Addmission Batch</label>--%>
                                            <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmbatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="2" AutoPostBack="true" ToolTip="Please select Institute" />
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please select Institute" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="show" />--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Academic Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAcdYear" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="show" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <%--        <asp:RequiredFieldValidator ID="rfvAcademicYear" runat="server" ControlToValidate="ddlAcdYear"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Academic Year" ValidationGroup="report">
                                            </asp:RequiredFieldValidator>--%>
                                    </div>




                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Institute Name</label>--%>
                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchClg" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlSchClg_SelectedIndexChanged" ToolTip="Please select Institute" />
                                        <asp:RequiredFieldValidator ID="rfvSchClg" runat="server" ControlToValidate="ddlSchClg"
                                            Display="None" ErrorMessage="Please select Institute" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="teacherreport" />
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Degree</label>--%>
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvDegree2" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <%--<label>Branch</label>--%>
                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="teacherreport"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<label>Semester</label>--%>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True" Enabled="false"
                                            ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Receipt Status</label>
                                        </div>
                                        <asp:DropDownList ID="ddlReceiptstatus" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                            TabIndex="1">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Regular Receipt</asp:ListItem>
                                            <asp:ListItem Value="1">Cancel Receipt</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlReceiptstatus"
                                            Display="None" ErrorMessage="Please Select Receipt Status" InitialValue="-1" SetFocusOnError="True"
                                            ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Receipt type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlRecType" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            ValidationGroup="Show" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvReceiptType" runat="server" ControlToValidate="ddlRecType"
                                            Display="None" ErrorMessage="Please Select Receipt Type" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="teacherreport"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlRecType"
                                            Display="None" ErrorMessage="Please Select Receipt Type" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="ExcelReport"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divRdoCountertype" runat="server">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:RadioButtonList ID="rbRegEx" runat="server" RepeatDirection="Horizontal" TabIndex="8" AutoPostBack="true" OnSelectedIndexChanged="rbRegEx_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Selected="True">&nbsp;Counter&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="1">&nbsp;Online</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divCounter" runat="server">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Counter</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCounter" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            ValidationGroup="Show" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCounter"
                                            Display="None" ErrorMessage="Please Select Counter" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="teacherreport"></asp:RequiredFieldValidator>--%>
                                    </div>



                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgCalFromDate">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" CssClass="form-control" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                            <%--<asp:Image ID="imgCalFromDate" runat="server" src="../IMAGES/calendar.png"
                                                Style="cursor: hand" TabIndex="0" />--%>
                                            <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please Select From Date" SetFocusOnError="true"
                                                ValidationGroup="teacherreport" />

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please Select From Date" SetFocusOnError="true"
                                                ValidationGroup="ExcelReport" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgCalToDate">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" CssClass="form-control" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                            <%--<asp:Image ID="imgCalToDate" runat="server" src="../IMAGES/calendar.png"
                                                Style="cursor: hand" TabIndex="0" />--%>
                                            <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                                                Display="None" ErrorMessage="Please Select To Date" SetFocusOnError="true"
                                                ValidationGroup="teacherreport" />

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                                                Display="None" ErrorMessage="Please Select To Date" SetFocusOnError="true"
                                                ValidationGroup="ExcelReport" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">

                                <asp:Button ID="btnShow" runat="server" Text="Show Student" CssClass="btn btn-primary" OnClick="btnShow_Click"
                                    ToolTip="Shows Students under Selected Criteria." ValidationGroup="teacherreport" Visible="false" />

                                <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info"
                                    ValidationGroup="teacherreport" OnClick="btnReport_Click" />

                                <asp:Button ID="btnReportExcel" runat="server" Text="Cancel Report(Excel)" CssClass="btn btn-info"
                                    OnClick="btnReportExcel_Click" Visible="false" ValidationGroup="ExcelReport" />

                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />

                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="ExcelReport" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="teacherreport" />

                            </div>


                            <div class="form-group col-lg-3 col-md-6 col-12" id="divTotal" runat="server">
                                <div class="label-dynamic">
                                    <label>Total Selected</label>
                                </div>
                                <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False" CssClass="form-control"
                                    Style="text-align: center;" Font-Bold="True" Font-Size="Small"></asp:TextBox>
                                <%--  Shrink the info panel out of view --%>
                                <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtTotStud"
                                    WatermarkText="0" WatermarkCssClass="watermarked" Enabled="True" />
                                <asp:HiddenField ID="hftot" runat="server" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <asp:ListView ID="lvStudentRecords" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Search Results</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">

                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="chkIdentityCard" runat="server" onClick="SelectAll(this);" ToolTip="Select or Deselect All Records" />
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Reciept No.
                                                        </th>
                                                        <th>Semester
                                                        </th>
                                                        <th>Reciept Date
                                                        </th>
                                                        <th>Paid Amount
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
                                                    <asp:CheckBox ID="chkReport" runat="server" onClick="totSubjects(this);" />
                                                    <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("STUDNAME")%>                                       
                                                </td>
                                                <td><%# Eval("REC_NO")%>                                              
                                                </td>
                                                <td><%# Eval("SEMESTERNAME")%>
                                                    <asp:HiddenField ID="hidDcrno" runat="server" Value='<%# Eval("DCR_NO") %>' />
                                                </td>
                                                <td><%# Eval("REC_DT")%>                                                   
                                                </td>
                                                <td><%# Eval("TOTAL_AMT")%>                                                  
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReportExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">
        function SelectAll(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
             var hftot = document.getElementById('<%= hftot.ClientID %>');
             for (i = 0; i < hftot.value; i++) {

                 var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_chkReport');
                 if (lst.type == 'checkbox') {
                     if (chk.checked == true) {
                         lst.checked = true;
                         txtTot.value = hftot.value;
                     }
                     else {
                         lst.checked = false;
                         txtTot.value = 0;
                     }
                 }

             }
         }

         function totSubjects(chk) {
             var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;

        }

        function validateAssign() {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;
            if (txtTot == 0) {
                alert('Please Check atleast one student ');
                return false;
            }
            else
                return true;
        }

        //function LoadImage() {
        //    document.getElementById("ctl00_ContentPlaceHolder1_imgCollegeLogo").src = document.getElementById("ctl00_ContentPlaceHolder1_fuCollegeLogo").value;
        //}

    </script>

</asp:Content>
