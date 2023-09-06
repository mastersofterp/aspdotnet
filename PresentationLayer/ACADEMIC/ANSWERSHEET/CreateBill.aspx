<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CreateBill.aspx.cs"
    Inherits="ACADEMIC_ANSWERSHEET_CreateBill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
    <%-- </div>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">CREATE BILL DETAILS</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divclgscheme" runat="server">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>College & Scheme </label>
                                        </div>
                                        <asp:DropDownList ID="ddlcollege" OnSelectedIndexChanged="ddlcollege_SelectedIndexChanged" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="True" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlcollege"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="MrksheetRcev"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label>Session</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True"
                                            ValidationGroup="MrksheetRcev">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            ValidationGroup="MrksheetRcev" Display="None" InitialValue="0" ErrorMessage="Please Select Session"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Evalutor Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlEvalutorType" AppendDataBoundItems="true" runat="server" ValidationGroup="MrksheetRcev"
                                            AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlEvalutorType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Internal</asp:ListItem>
                                            <asp:ListItem Value="2">External</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvEvaluator" runat="server" ControlToValidate="ddlEvalutorType"
                                            Display="None" ErrorMessage="Please Select Evalutor Type" InitialValue="0" ValidationGroup="MrksheetRcev">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Evaluator Name  </label>
                                        </div>
                                        <asp:HiddenField ID="hdissuerid" runat="server" />
                                        <asp:DropDownList ID="ddlFaculty" AppendDataBoundItems="True" runat="server" ValidationGroup="MrksheetRcev" AutoPostBack="true"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvFaculty" runat="server" ControlToValidate="ddlFaculty"
                                            Display="None" ErrorMessage="Please Enter Faculty Name" InitialValue="0" ValidationGroup="MrksheetRcev"></asp:RequiredFieldValidator>

                                    </div>


                                </div>

                                <div class=" col-12 btn-footer">

                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnShow_Click" Visible="false" />

                                    <asp:Button ID="btnSubmit" runat="server" ValidationGroup="MrksheetRcev" Text="Submit"
                                        OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" CssClass="btn btn-info" Text="Bill Report" />
                                    <%--    <asp:Button Visible="false" ID="btnDateReport" runat="server" OnClick="btnDateReport_Click" CssClass="btn btn-info"  Text="Datewise Report" />--%>
                                    <asp:Button ID="btnClear" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnClear_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="MrksheetRcev" />
                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="pnlStudent" runat="server">

                                        <asp:ListView ID="lvStudentsIssuer" runat="server">
                                            <LayoutTemplate>

                                                <div class="sub-heading">
                                                    <h5>LIST OF COURSE</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Course Name </th>
                                                            <th>Name Of Evaluator </th>
                                                            <th>Bundle No. </th>
                                                            <th>Number Of Paper </th>
                                                            <th>Per Paper Amount </th>
                                                            <th>Total Amount </th>
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
                                                    <td><%# Eval("COURSENAME")%>
                                                        <asp:HiddenField ID="hdCourseno" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                    </td>
                                                    <td><%# Eval("EVALUATOR")%>
                                                        <asp:HiddenField ID="hdfEvaluator" runat="server" Value='<%# Eval("EVALUATOR") %>' />
                                                    </td>
                                                    <td><%# Eval("BUNDLE_NO")%>
                                                        <asp:HiddenField ID="hdBundle" runat="server" Value='<%# Eval("BUNDLE_NO") %>' />
                                                    </td>
                                                    <td><%# Eval("QUANTITY")%>
                                                        <asp:HiddenField ID="hdQty" runat="server" Value='<%# Eval("QUANTITY") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAmountEachPaper" runat="server" CssClass="form-control" Enabled="false" Text='<%#Eval("PER_PAPER_RATE")%>' ToolTip='<%#Eval("PER_PAPER_RATE")%>'> </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" Enabled="true" Text='<%#Eval("TOTAMOUNT")%>' ToolTip='<%#Eval("TOTAMOUNT")%>'></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>

                                        <div id="divMsg" runat="server">
                                        </div>
                                    </asp:Panel>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <br />

                <script language="javascript" type="text/javascript">
                    function IsNumeric(txt) {
                        var ValidChars = "0123456789";
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
                        return num;
                    }


                </script>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
