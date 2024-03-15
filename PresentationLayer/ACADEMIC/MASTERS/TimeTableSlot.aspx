<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="TimeTableSlot.aspx.cs" Inherits="ACADEMIC_MASTERS_TimeTableSlot" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();

        }

        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });

        }
    </script>--%>

    <%--   <script src="../../Content/jquery.js" type="text/javascript"></script>

    <script src="../../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>--%>

    <%--    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDYpageTImeTableSlot" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>
                                        <asp:Label ID="lblDYtxtslotname" runat="server"></asp:Label></label>
                                </div>
                                <asp:TextBox ID="txtSlotName" runat="server" TabIndex="2"
                                    ToolTip="Please Enter Slot Name" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSlotName" runat="server" ControlToValidate="txtSlotName"
                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter Slot Name"
                                    SetFocusOnError="true" />
                                <%--<ajaxToolKit:FilteredTextBoxExtender ID="fltSlot" runat="server" FilterType="UppercaseLetters,LowercaseLetters,Numbers" TargetControlID="txtSlotName"></ajaxToolKit:FilteredTextBoxExtender>--%>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>
                                        <asp:Label ID="lblDYtimeform" runat="server"></asp:Label></label>
                                </div>
                                <asp:TextBox ID="txtTimeFrom" runat="server" TabIndex="3"
                                    ToolTip="Please Enter From Time Format hh:mm" CssClass="form-control"></asp:TextBox>
                                <ajaxToolKit:MaskedEditExtender ID="meTimeFrom" runat="server" TargetControlID="txtTimeFrom"
                                    Mask="99:99" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" AcceptAMPM="true"
                                    MaskType="Time" />
                                <ajaxToolKit:MaskedEditValidator ID="mevTimeFrom" runat="server" EmptyValueMessage="Please Enter From Time"
                                    ControlExtender="meTimeFrom" ControlToValidate="txtTimeFrom" IsValidEmpty="false"
                                    InvalidValueMessage="From Time is invalid" Display="None" ErrorMessage="Please Enter From Time"
                                    InvalidValueBlurredMessage="*" SetFocusOnError="true" ValidationGroup="Submit" />

                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>
                                        <asp:Label ID="lblDYtimeto" runat="server"></asp:Label></label>
                                </div>
                                <asp:TextBox ID="txtTimeTo" runat="server" TabIndex="4"
                                    ToolTip="Please Enter To Time format hh:mm" CssClass="form-control"></asp:TextBox>
                                <ajaxToolKit:MaskedEditExtender ID="meTimeTo" runat="server" TargetControlID="txtTimeTo"
                                    Mask="99:99" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" AcceptAMPM="true"
                                    MaskType="Time" />
                                <ajaxToolKit:MaskedEditValidator ID="mevTimeTo" runat="server" EmptyValueMessage="Please Enter To Time"
                                    ControlExtender="meTimeTo" ControlToValidate="txtTimeTo" IsValidEmpty="false"
                                    InvalidValueMessage="To Time is invalid" Display="None" ErrorMessage="Please Enter To Time"
                                    InvalidValueBlurredMessage="*" SetFocusOnError="true" ValidationGroup="Submit" />

                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>
                                        <asp:Label ID="lblDYcourseslot" runat="server"></asp:Label></label>
                                </div>
                                <div class="switch form-inline" tabindex="0">
                                    <input type="checkbox" id="rdcourseslot" name="switch" />
                                    <label data-on="Yes" data-off="No" for="rdcourseslot" tabindex="0"></label>
                                    <asp:HiddenField ID="hfdCourseSlot" runat="server" ClientIDMode="Static" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit"
                            OnClick="btnSubmit_Click" TabIndex="5" CssClass="btn btn-primary" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="6" OnClick="btnCancel_Click"
                            CssClass="btn btn-warning" />
                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Submit" />
                    </div>
                    <div class="col-12 mt-3 mb-3">
                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="Table1">
                            <asp:Repeater ID="lvSlot" runat="server">
                                <HeaderTemplate>
                                    <div id="demo-grid" class="vista-grid">
                                        <div class="sub-heading">
                                            <h5>Exam Time Table Slot List</h5>
                                        </div>
                                        <thead>
                                            <tr class="bg-light-blue">
                                                <th>Edit
                                                </th>
                                                <th>Slot Name
                                                </th>
                                                <th>Time From - To
                                                </th>
                                                <th>Course Slot
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />

                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="item">
                                        <td>
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                CommandArgument='<%# Eval("SLOTNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                OnClick="btnEdit_Click" TabIndex="7" />
                                        </td>

                                        <td>
                                            <%# Eval("SLOTNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("TIMEFROM")%> - <%# Eval("TIMETO")%>
                                        </td>
                                        <td><%# (Eval("COURSE_SLOT").ToString() == "1") ? "Yes" : "No"%></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                                </FooterTemplate>
                            </asp:Repeater>
                        </table>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $("#rdcourseslot").on("click", function (event) {
            $('#hfdCourseSlot').val($('#rdcourseslot').prop('checked'));
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $("#rdcourseslot").on("click", function (event) {
                $('#hfdCourseSlot').val($('#rdcourseslot').prop('checked'));
            });
        });

        function SetCourseSlot(val) {
            $('#rdcourseslot').prop('checked', val);
        }
    </script>
</asp:Content>
