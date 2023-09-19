<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Exam_Assesment.aspx.cs" Inherits="ACADEMIC_Exam_Assesment" ClientIDMode="AutoID" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <style type="text/css">
        element.style {
            width: auto;
            width: 1045px;
        }

        #gridrow.width {
            width: auto;
            width: 1045px;
        }
        .layout-top-nav {
        font-weight: 700;
    }
    </style>--%>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSession"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updSession" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Exam Component</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College/Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollegeIdDepMap" OnSelectedIndexChanged="ddlCollegeIdDepMap_SelectedIndexChanged" runat="server" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="ddlcollege" runat="server" ControlToValidate="ddlCollegeIdDepMap"
                                            Display="None" ErrorMessage="Please Select College." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                            TabIndex="2" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="DivSubtype">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Course/Subject</label>
                                        </div>
                                        <asp:DropDownList ID="ddlsubjecttype" runat="server" TabIndex="3" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlsubjecttype_SelectedIndexChanged"
                                            ToolTip="Please Select Subject Type" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvProgram" runat="server" ControlToValidate="ddlsubjecttype"
                                            Display="None" ErrorMessage="Please Select Subject Type" ValidationGroup="submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12" id="divMarksDetails" runat="server" visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Internal Marks</label>
                                        </div>
                                        <asp:TextBox ID="txtCaPer1" runat="server" CssClass="form-control" MaxLength="15" ReadOnly="true" TabIndex="4" onblur="return CheckMark(this);" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">

                                            <label>External Marks</label>
                                        </div>
                                        <asp:TextBox ID="txtFinal" runat="server" CssClass="form-control" MaxLength="15" ReadOnly="true" TabIndex="5" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">

                                            <label>Min Internal(%)</label>
                                        </div>
                                        <asp:TextBox ID="txtMinCa" runat="server" CssClass="form-control" MaxLength="15" ReadOnly="true" TabIndex="6" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">

                                            <label>Min External(%)</label>
                                        </div>
                                        <asp:TextBox ID="txtMinFinal" runat="server" CssClass="form-control" MaxLength="15" ReadOnly="true" TabIndex="7" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                        <div class="label-dynamic">

                                            <label>Total Marks</label>
                                        </div>
                                        <asp:TextBox ID="txtOverall" runat="server" CssClass="form-control" MaxLength="15" ReadOnly="true" TabIndex="8" />
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="col-12" id="gridrow">
                                    <asp:ListView ID="lvAssessment" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>SrNo</th>
                                                        <th>Sub Exam Component </th>
                                                        <th>Marks (Out of)</th>
                                                        <th>Weightage(%)</th>
                                                        <th>Total Marks</th>
                                                        <th>Cancel</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td class="float-right"><strong>Total</strong></td>
                                                        <td>
                                                            <strong>
                                                                <asp:Label ID="lblTotalM" runat="server"></asp:Label>
                                                                <%--Text='<%#Eval("Mark") %>'--%>
                                                            </strong>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hfsrno" runat="server" Value='<%# Container.DataItemIndex + 1 %>' />
                                                    <asp:HiddenField ID="hfdvalue" runat="server" Value='<%#Eval("SubExamComponent") %>' />

                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAssessment" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" Enabled="true"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlAssessment_SelectedIndexChanged">
                                                        <asp:ListItem Text="Please Select" Value="0"> </asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hdfIsLock" runat="server" Value='<%#Eval("LOCK") %>'/>
                                                </td>
                                                <%--      <td>
                                                            <asp:TextBox ID="txtAssessmentComponent" runat="server" CssClass="form-control" Placeholder="Assessment Name" MaxLength="50">
                                                            </asp:TextBox>
                                                        </td>--%>
                                                <td>
                                                    <asp:TextBox ID="txtOutOfMarks" runat="server" CssClass="form-control" Placeholder="Out Of Marks" MaxLength="5" Text='<%#Eval("MarksOutof1") %>' Enabled="true"> </asp:TextBox>
                                                    <%--onblur="return CheckMark(this);"--%>
                                                    <%--MarksOutof--%>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="fteValue" runat="server"
                                                        FilterType="Custom" FilterMode="ValidChars" ValidChars="0123456789." TargetControlID="txtOutOfMarks">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtWeightage" runat="server" CssClass="form-control" Placeholder="Weightage(%)" onblur="return CheckMark(this);" Text='<%#Eval("Weightage") %>'
                                                        MaxLength="5" AutoPostBack="true" OnTextChanged="txtWeightage_TextChanged"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                        FilterType="Custom" FilterMode="ValidChars" ValidChars="0123456789." TargetControlID="txtWeightage">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTotalMarks" runat="server" Text='<%#Eval("TotalMark") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label runat="server" ID="lblFix" Text='<%#Eval("Fix") %>' Visible="false"></asp:Label>
                                                    <asp:ImageButton ID="imgbtnDeleteAssessmentNew" runat="server" OnClick="imgbtnDeleteAssessmentNew_Click1" CommandArgument='<%#Eval("SubExamComponent") %>'
                                                        ImageUrl="../Images/delete.png" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                    <%--<div class="form-group col-lg-6 col-md-6 col-12">
                                        <asp:Label ID="lblTotalMark" Text="Total" runat="server" Font-Bold="true"></asp:Label>
                                    </div>--%>

                                    <%-- <div class="row">
                                        <div id="Div1" class="form-group col-lg-9 col-md-6 col-12" runat="server">

                                        </div>


                                        <div id="Div3" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                            <div class="label-dynamic">

                                                <label>Total Marks</label>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="col-12 btn-footer mt-4" id="DivAdd" runat="server" visible="false">
                                        <asp:Button ID="BtnAddAssesment" runat="server" Text="Add Assesment" OnClick="btnAdd_Click"
                                            TabIndex="9" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                            ValidationGroup="submit" TabIndex="10" CssClass="btn btn-primary" />
                                         <asp:Button ID="btnLock" runat="server" Text="Submit & Lock" ValidationGroup="submit" CssClass="btn btn-primary" TabIndex="11" OnClientClick="return confirm('Do you really want to Submit & Lock Exam Components!')" OnClick="btnLock_Click" Visible="false"  />
                                        <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-primary" OnClick="btnReport_Click" Visible="false" TabIndex="11" />
                                         <asp:Button ID="btnReportExcel" runat="server" Text="Component Not Defined" CssClass="btn btn-primary" OnClick="btnReportExcel_Click" Visible="false" TabIndex="12" />

                                           <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click"
                                            TabIndex="12" CssClass="btn btn-warning" />
                                        <%--  <asp:LinkButton ID="btnCancel2" runat="server" CssClass="btn btn-outline-danger" TabIndex="3" OnClick="btnCancel_Click">Cancel</asp:LinkButton>--%>

                                        <asp:ValidationSummary ID="valSummery" DisplayMode="List" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="submit" />
                                    </div>
                                </div>

                            </asp:Panel>

                            <div class="col-12 mt-3">
                                <asp:Panel ID="Panel4" runat="server">
                                    <asp:ListView ID="lvAssessmentComponent" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid" style="width: 1000px">
                                                <div class="sub-heading" id="dem">
                                                    <h5>Exam Components List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap dsiplay" id="ord-table">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Sr. No</th>
                                                            <th>Sub Exam Component</th>
                                                            <%-- <th>Assesment Name</th>--%>
                                                            <th>Mark(Out Of)</th>
                                                            <th>Weightage (Out Of)</th>
                                                            <th>Session Name</th>
                                                            <th>College Name</th>
                                                            <th>Course/Subject Name</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hfsrno" runat="server" Value='<%# Container.DataItemIndex + 1 %>' />
                                                    <asp:HiddenField ID="hfdvalue" runat="server" Value="0" />
                                                </td>
                                                <td>
                                                    <%# Eval("SUBEXAM_COMPONENT")%>
                                                </td>
                                                <%--<td>
                                                        <%# Eval("ASSESMENT_NAME")%>
                                                    </td>--%>
                                                <td>
                                                    <%# Eval("MARKOUTOF")%>
                                                </td>
                                                <td>
                                                    <%# Eval("WEIGHTAGE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SESSION_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("COLLEGE_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("COURSE_NAME")%>
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
       <%--     <asp:PostBackTrigger ControlID="BtnAddAssesment" />
            <asp:PostBackTrigger ControlID="btnsubmit" />--%>

               <asp:PostBackTrigger ControlID="btnReportExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function IsNumeric(txt) {
            var ValidChars = "0123456789.";

            var num = true;
            var mChar;
            cnt = 0

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);

                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Please enter Numeric values only")
                    txt.select();
                    txt.focus();
                }
            }

            //var rowIndex = id.offsetParent.parentNode.rowIndex - 1;
            //var cellIndex = id.offsetParent.cellIndex;

            //var Apllicable = 0; var Discount = 0;
            //Apllicable = document.getElementById("ctl00_ContentPlaceHolder1_lvAssessment_ctrl" + rowIndex + "_txtOutOfMarks").value;
            //alert(Apllicable);
            return num;
        }
    </script>
    <%--<script>
        function CheckMark(id) {
            alert('123')
            Internal = document.getElementById("ctl00_ContentPlaceHolder1_txtCaPer1").value;
            var rowIndex = id.offsetParent.parentNode.rowIndex - 1;
            var cellIndex = id.offsetParent.cellIndex;

            var Apllicable = 0; var Total = 0; var Internal = 0;

            Apllicable = document.getElementById("ctl00_ContentPlaceHolder1_lvAssessment_ctrl" + rowIndex + "_txtOutOfMarks").value;
            Apllicable1 = document.getElementById("ctl00_ContentPlaceHolder1_lvAssessment_ctrl" + rowIndex + "_txtWeightage").value;
            alert(Apllicable);
            alert(Apllicable1);

            Total = Apllicable * (Apllicable1 / 100);
            alert(Total);
           // document.getElementById("ctl00_ContentPlaceHolder1_lvAssessment_ctrl" + rowIndex + "_txtWeightage").value = Total
            //document.getElementById("ctl00_ContentPlaceHolder1_lvAssessment_ctrl" + rowIndex + "lblTotalMarks").text = Total
            $("#ctl00_ContentPlaceHolder1_lvAssessment_ctrl"+rowIndex+"_lblTotalMarks").text(Total);


            //Discount = document.getElementById("ctl00_ContentPlaceHolder1_lvlConcession_ctrl" + rowIndex + "_ddlDiscount");


            //var option = Discount.options[Discount.selectedIndex];
            //var Discounts = option.text;

            //if (Discounts == 'Please Select') {
            //    var Discounts = 0;
            //}
            //if (Apllicable == '') {
            //    var Apllicable = 0;
            //}
            //var total = 100
            //ConvertMark = (Number(Discounts) / Number(total)) * Number(Apllicable)
            //var Netpayable = (Number(Apllicable) - ConvertMark);

            //document.getElementById("ctl00_ContentPlaceHolder1_lvlConcession_ctrl" + rowIndex + "_txtDiscountFee").value = ConvertMark;

            //document.getElementById("ctl00_ContentPlaceHolder1_lvlConcession_ctrl" + rowIndex + "_txtNetPayable").value = Netpayable;
        }
    </script>--%>
</asp:Content>

