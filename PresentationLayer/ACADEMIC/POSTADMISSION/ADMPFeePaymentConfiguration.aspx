<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPFeePaymentConfiguration.aspx.cs" Inherits="ADMPFeePaymentConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>

        function setMaxLength(control) {
            //get the isMaxLength attribute
            var mLength = control.getAttribute ? parseInt(control.getAttribute("isMaxLength")) : ""

            //was the attribute found and the length is more than the max then trim it
            if (control.getAttribute && control.value.length > mLength) {
                control.value = control.value.substring(0, mLength);
                //alert('Length exceeded');
            }

            //display the remaining characters
            var modid = control.getAttribute("id") + "_remain";
            if (document.getElementById(modid) != null) {
                document.getElementById(modid).innerHTML = mLength - control.value.length;
            }
        }

    </script>

    <script>
        function SetParticipation(val) {

            $('#rdActive').prop('checked', val);

        }
        function validate() {
            $('#hfdActive').val($('#rdActive').prop('checked'));
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });
    </script>
    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />

    <asp:UpdatePanel ID="updSession" runat="server">
        <ContentTemplate>

            <div class="row">
                <asp:UpdateProgress ID="UpdateProgress4" runat="server"
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


                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Fee Payment Configuration</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <asp:RadioButtonList ID="rdoActivityFor" runat="server" OnSelectedIndexChanged="rdoActivityFor_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                                            <%--       <asp:ListItem Value="2" Selected="True"> Phd Student Registration Form Activity &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>--%>
                                            <asp:ListItem Value="1" Selected="True"> Provisional Admission Payment Activity</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch </label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" SetFocusOnError="True"
                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Program Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlProgramType" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlProgramType_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvddlProgramType" runat="server" ControlToValidate="ddlProgramType"
                                            Display="None" ErrorMessage="Please Select Program Type" SetFocusOnError="True"
                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree </label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" SetFocusOnError="True"
                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Status </label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdActive" name="Started" checked />
                                            <label data-on="Started" data-off="Stopped" for="rdActive"></label>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Start Date</label>
                                        </div>
                                        <asp:TextBox ID="txtStartDate" runat="server" type="date" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtStartDate" runat="server" ControlToValidate="txtStartDate"
                                            Display="None" ErrorMessage="Please Select Start Date" SetFocusOnError="True"
                                            ValidationGroup="Academic" InitialValue=""></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>End Date</label>
                                        </div>
                                        <asp:TextBox ID="txtEndDate" runat="server" type="date" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtEndDate_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtEndDate" runat="server" ControlToValidate="txtEndDate"
                                            Display="None" ErrorMessage="Please Select End Date" SetFocusOnError="True"
                                            ValidationGroup="Academic" InitialValue=""></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" ValidationGroup="Academic" ForeColor="Red" runat="server"
                                            ControlToValidate="txtStartDate" ControlToCompare="txtEndDate" Operator="LessThan" Type="Date"
                                            Display="None" ErrorMessage="Start date must be less than End date."></asp:CompareValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Payment Category </label>
                                        </div>
                                        <asp:DropDownList ID="ddlPaymentCategory" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlPaymentCategory_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Fix Payment</asp:ListItem>
                                            <asp:ListItem Value="2">Percentage</asp:ListItem>
                                            <asp:ListItem Value="3">Full Payment</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvddlPaymentCategory" runat="server" ControlToValidate="ddlPaymentCategory"
                                            Display="None" ErrorMessage="Please Select Payment Category" SetFocusOnError="True"
                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divAmount" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label id="lblAmount" runat="server">Amount </label>
                                        </div>
                                        <%--||event.charCode == 46--%>
                                        <asp:TextBox ID="txtAmount" runat="server" type="number" TextMode="Number" OnTextChanged="txtAmount_TextChanged" AutoPostBack="true" min="0" onKeyUp="setMaxLength(this)" onBlur="setMaxLength(this)" isMaxLength="8" CssClass="form-control" onkeypress="return (event.charCode >= 48 && event.charCode <= 57)"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="rfvtxtAmount" runat="server" ControlToValidate="txtAmount"
                                            Display="None" ErrorMessage="Please Enter Amount/Percentage" SetFocusOnError="True"
                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="validate();" CssClass="btn btn-primary" ValidationGroup="Academic" OnClick="btnSubmit_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="false" ValidationGroup="Academic" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" CausesValidation="false" OnClick="btnCancel_Click" />
                            </div>

                            <div class="col-12 mt-3">
                                <div class="sub-heading">
                                    <h5>Fee Payment Configuration List</h5>
                                </div>
                                <asp:Panel ID="pnlFeePayConfig" runat="server" Visible="false">
                                    <%--OnItemEditing="lvFeePayConfig_ItemEditing"--%>
                                    <asp:ListView ID="lvFeePayConfig" runat="server" ItemPlaceholderID="itemPlaceholder">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblFeePayConfig">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit</th>
                                                        <th>Admission Batch</th>
                                                        <th>Program Type</th>
                                                        <th>Degree</th>
                                                        <th>Start Date</th>
                                                        <th>End Date</th>
                                                        <th>Payment Category</th>
                                                        <th>Amount/Percentage</th>
                                                        <th>Status</th>
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
                                                    <asp:LinkButton ID="btnEditFeePay" runat="server" OnClick="btnEditFeePay_Click" CssClass="fas fa-edit"
                                                        CausesValidation="false" CommandArgument='<%#Eval("CONFIGID") %>'></asp:LinkButton>
                                                </td>
                                                <td><%# Eval("ADMISSIONBATCH") %></td>
                                                <td><%# Eval("PROGRAMTYPE") %></td>
                                                <td><%# Eval("DEGREE") %></td>
                                                <%--<td><%# Convert.ToDateTime (Eval("STDATE")).ToString("d") %></td>
                                        <td><%# Convert.ToDateTime (Eval("ENDDATE")).ToString("d") %></td>--%>
                                                <td><%# Eval("STARTDATE") %></td>
                                                <td><%# Eval("ENDDATE") %></td>

                                                <td><%# Eval("PAYMENTCATEGORY") %></td>
                                                <td><%# Eval("FEEPAYMENT") %></td>
                                                <td><%--<%# Eval("ACTIVITYSTATUS") %>--%>
                                                    <asp:Label ID="lblStatus" runat="server" CssClass="badge" Text='<%# Eval("ACTIVITYSTATUS") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>

                                <%--<table class="table table-striped table-bordered nowrap display" style="width: 100%">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th>Edit</th>
                                    <th>Admission Batch</th>
                                    <th>Program Type</th>
                                    <th>Degree</th>
                                    <th>Start Date</th>
                                    <th>End Date</th>
                                    <th>Status</th>
                                    <th>Payment Category</th>
                                    <th>Amount</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Image ID="imgEdit" runat="server" ImageUrl="~/Images/edit.png" /></td>
                                    <td>Jan - June 2022</td>
                                    <td>Program Type</td>
                                    <td>Degree</td>
                                    <td>01-11-2022</td>
                                    <td>30-11-2022</td>
                                    <td>Started</td>
                                    <td>Fix Payment</td>
                                    <td>50,000</td>
                                </tr>
                            </tbody>
                        </table>--%>
                            </div>

                        </div>

                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>

            <asp:AsyncPostBackTrigger ControlID="rdoActivityFor" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlProgramType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlPaymentCategory" EventName="SelectedIndexChanged" />

            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />

        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

