<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="NoDuesCollection.aspx.cs" Inherits="ACADEMIC_NoDuesCollection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../Content/jquery.js"></script>
    <script src="../plugins/jQuery/jQuery-2.2.0.min.js"></script>

    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updNoDues"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updNoDues" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>NO DUES FINE COLLECTION:</b></h3>
                            <div class="pull-right">
                                <div style="color: Red; font-weight: bold;">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>
                            </div>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlSearch" runat="server">
                                <div class="col-md-12">
                                    <div class="form-group col-md-4">
                                        <label><span style="color: red">*</span>Session: </label>
                                        <asp:DropDownList ID="ddlSession" TabIndex="1" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            Font-Bold="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-4" id="tblSearch">
                                        <label><span style="color: red">*</span>Enter Roll No. </label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtEnrollno" class="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEnrollno"
                                                Display="None" ErrorMessage="Please Enter Enroll No" ValidationGroup="show"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <span class="input-group-btn" style="padding-left:10px">
                                                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" OnClientClick="resetAmount();"
                                                    Text="Show" CssClass="btn btn-primary" ValidationGroup="show" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="show"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div id="divCourses" runat="server" visible="false">
                                <div class="col-md-12" id="tblInfo" runat="server">
                                    <fieldset>
                                        <legend>Student Details:</legend>
                                        <div class="col-md-6">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item">
                                                    <b>Student Name :</b><a class="pull-right">
                                                        <asp:Label ID="lblName" runat="server" Font-Bold="True" /></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Father's Name :</b><a class="pull-right">
                                                        <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" Style="font-weight: 700" /></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>IdNo :</b><a class="pull-right">
                                                        <asp:Label ID="lblIdno" runat="server" Font-Bold="False" Style="font-weight: 700" /></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Semester :</b><a class="pull-right">
                                                        <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                                    <asp:HiddenField ID="hdnSemesterNo" runat="server" Value="" />
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Degree :</b><a class="pull-right">
                                                        <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label></a>
                                                    <asp:HiddenField ID="hdnDegreeNo" runat="server" Value="" />
                                                </li>

                                            </ul>

                                        </div>
                                        <div class="col-md-6">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item">
                                                    <a class="">
                                                        <asp:Image ID="imgPhoto" runat="server" Width="128px" Height="120px" /></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Branch :</b><a class="pull-right">
                                                        <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                                    <asp:HiddenField ID="hdnBranchNo" runat="server" Value="" />
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Outstanding Fee :</b><a class="pull-right">
                                                        <asp:Label ID="lblDueFee" runat="server" Font-Bold="True"></asp:Label></a>
                                                    <asp:HiddenField ID="hdnCollegeId" runat="server" Value="" />
                                                    <asp:HiddenField ID="hdnSchemeNo" runat="server" Value="" />
                                                    <asp:HiddenField ID="hdnPType" runat="server" Value="" />

                                                </li>
                                                <span style="color: red;">Note: Please make payment for outstanding fees before No-Dues collection.<br />
                                                    (if outstanding fees balance)  </span>
                                            </ul>

                                        </div>
                                    </fieldset>

                                </div>
                            </div>

                            <div class="col-md-12">
                                <asp:Panel ID="pnlLv" runat="server" ScrollBars="Auto" Visible="false">
                                    <legend>Fee Details:</legend>
                                    <div class="col-md-12">
                                        <asp:Panel ID="pnlNoduesF" runat="server" ScrollBars="Auto" Visible="false">
                                            <p class="text-center" style="color: red; font-weight: bold; font-size: x-large;">
                                                NO-DUES FOUND FOR FILTERED STUDENT.
                                            </p>
                                        </asp:Panel>
                                    </div>
                                    <asp:ListView ID="lvStudentFee" runat="server" OnItemDataBound="lvStudentFee_ItemDataBound">
                                        <LayoutTemplate>

                                            <table id="tblFeeItems" runat="server" class="table table-hover table-bordered table-responsive">
                                                <%--<thead>--%>
                                                <tr class="bg-light-blue">

                                                    <th style="text-align: center; width: 15%;">Co-Ordinator Amt
                                                    </th>

                                                    <th style="text-align: center; width: 20%;">Library Incharge Amt
                                                    </th>

                                                    <th style="text-align: center; width: 20%;">Academic Dept. Amt
                                                    </th>

                                                    <%--                                                            <th style="text-align: center; width: 15%;">Outstanding Amt
                                                            </th>--%>

                                                    <th style="text-align: center; width: 20%;">Accounts Incharge Amt
                                                    </th>

                                                    <th style="text-align: center; width: 25%;">Remarks
                                                    </th>

                                                    <%--   <th style="text-align: center; width: 10%;">Status
                                                            </th>--%>
                                                </tr>
                                                <%--</thead>
                                                <tbody>--%>
                                                <tr id="itemPlaceholder" runat="server" />
                                                <tr class="item">
                                                    <td colspan="2"></td>
                                                    <td class="data_label" style="text-align: center; font-weight: bold;">TOTAL AMOUNT:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTotalAmount" Enabled="false" Style="text-align: center; font-size: large; font-weight: bold;" onkeydown="javascript:return false;"
                                                            runat="server" CssClass="data_label" />

                                                    </td>
                                                </tr>
                                                <%--</tbody>--%>
                                            </table>

                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center; width: 15%;">
                                                    <asp:Label ID="lblCordinatorAmt" Font-Bold="true" runat="server" Text='<%# Eval("CORDINATOR_AMOUNT")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                                </td>

                                                <td style="text-align: center; width: 20%;">
                                                    <asp:Label ID="lblLibAmt" Font-Bold="true" runat="server" Text='<%# Eval("LIB_INCHRG_AMOUNT")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                                </td>

                                                <td style="text-align: center; width: 20%;">
                                                    <asp:Label ID="lblAcadAmt" Font-Bold="true" runat="server" Text='<%# Eval("ACAD_DEPT_AMOUNT")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                                </td>

                                                <%-- <td style="text-align: center; width: 15%;">
                                                    <asp:Label ID="lblOutstandingFee" Font-Bold="true" runat="server"></asp:Label>
                                                </td>--%>

                                                <td style="text-align: center; width: 20%;" class="amount">
                                                    <asp:Label ID="lblAcctAmount" Font-Bold="true" Visible="false" runat="server" Text='<%# Eval("ACCT_INCHRG_AMOUNT")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                                    <asp:TextBox ID="txtAcctAmount" onblur="UpdateTotalAmount();" Style="text-align: center;" Visible="false" Text='<%# Eval("ACCT_INCHRG_AMOUNT")%>' BorderStyle="Groove" runat="server" TextMode="SingleLine" Height="50px" class="form-control"></asp:TextBox>
                                                    <ajaxToolKit:TextBoxWatermarkExtender ID="watersub" WatermarkText="Enter Amount" TargetControlID="txtAcctAmount" runat="server">
                                                    </ajaxToolKit:TextBoxWatermarkExtender>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtAmount" runat="server" FilterType="Numbers, Custom"
                                                        ValidChars="." TargetControlID="txtAcctAmount" />
                                                    <asp:HiddenField ID="hdTotAmount" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hdnIsFine" runat="server" Value='<%# Eval("IS_FINE")%>' />
                                                </td>

                                                <td style="text-align: center; width: 25%;">
                                                    <asp:TextBox ID="txtAcctRemarks" Style="text-align: center;" BorderStyle="Groove" runat="server" Text='<%# Eval("ACCT_INCHRG_REMARKS")%>' TextMode="MultiLine" Height="50px" class="form-control"></asp:TextBox>
                                                    <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="Enter Remarks" TargetControlID="txtAcctRemarks" runat="server">
                                                    </ajaxToolKit:TextBoxWatermarkExtender>
                                                </td>

                                                <%--  <td style="text-align: center; width: 10%;">
                                                    <asp:Label runat="server" ID="lblStatus"></asp:Label>
                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>

                                </asp:Panel>
                            </div>


                            <div class="col-md-12">
                                <p class="text-center">
                                    <asp:Button ID="btnSubmit" runat="server" Visible="false" OnClick="btnSubmit_Click"
                                        Text="Submit" ValidationGroup="backsem"
                                        CssClass="btn btn-success" />
                                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Visible="false"
                                        Text="Cancel" ValidationGroup="backsem" CssClass="btn btn-danger" />
                                </p>
                            </div>


                        </div>

                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        //$(document).ready(function () {
        //    alert('test!');

        //});

        //function Count(event) {
        //    var amount = jQuery(this).closest("tr").find('td.amount').find("input[type=hidden]").val();
        //    alert(amount.toString());
        //}

        function resetAmount() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_lvStudentFee_txtTotalAmount') != null) {

                document.getElementById('ctl00_ContentPlaceHolder1_lvStudentFee_txtTotalAmount').value = '0.00';
                document.getElementById('ctl00_ContentPlaceHolder1_lvStudentFee_ctrl0_hdTotAmount').value = '0.00';
            }
        }

        function UpdateTotalAmount() {
            debugger;
            try {
                //alert('hii');
                var totalFeeAmt = 0.00;
                var CordinateAmt = 0.00;
                var LibraryAmt = 0.00;
                var AcademicAmt = 0.00;

                var dataRows = null;

                if (document.getElementById('ctl00_ContentPlaceHolder1_lvStudentFee_tblFeeItems') != null)
                    dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentFee_tblFeeItems').getElementsByTagName('tr');

                if (dataRows != null) {
                    for (i = 1; i < (dataRows.length - 1) ; i++) {
                        var dataCellCollection = dataRows.item(i).getElementsByTagName('td');

                        var dataCell3 = dataCellCollection.item(3);
                        var control3 = dataCell3.getElementsByTagName('input');
                        var txtAmt = control3.item(0).value;

                        var cordtAmt = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentFee_ctrl0_lblCordinatorAmt').innerText;
                        var libAmt = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentFee_ctrl0_lblLibAmt').innerText;
                        var acadAmt = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentFee_ctrl0_lblAcadAmt').innerText;

                        if (cordtAmt.trim() != 'PENDING') {
                            CordinateAmt = parseFloat(cordtAmt);
                        }

                        if (libAmt.trim() != 'PENDING') {
                            LibraryAmt = parseFloat(libAmt);
                        }

                        if (acadAmt.trim() != 'PENDING') {
                            AcademicAmt = parseFloat(acadAmt);
                        }

                        if (txtAmt.trim() != '')
                            totalFeeAmt += parseFloat(txtAmt);
                    }
                    if (document.getElementById('ctl00_ContentPlaceHolder1_lvStudentFee_txtTotalAmount') != null) {
                        var str = 'Rs. ' + (totalFeeAmt + CordinateAmt + LibraryAmt + AcademicAmt);

                        document.getElementById('ctl00_ContentPlaceHolder1_lvStudentFee_txtTotalAmount').value = str.toString();
                        document.getElementById('ctl00_ContentPlaceHolder1_lvStudentFee_ctrl0_hdTotAmount').value = (totalFeeAmt + CordinateAmt + LibraryAmt + AcademicAmt);
                    }
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }
    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

