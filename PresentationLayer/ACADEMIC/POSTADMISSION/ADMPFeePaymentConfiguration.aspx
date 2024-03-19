<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPFeePaymentConfiguration.aspx.cs" Inherits="ADMPFeePaymentConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

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
 
 

<<<<<<< HEAD
      <script>
=======



    <script>
>>>>>>> 47e1c9f2 ([ENHANCEMENT][56245][Changes for added branch and dates])
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
    <script>
      
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
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" SetFocusOnError="True"
                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>

                                        </div>
                                        <asp:ListBox ID="lstBranch" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo"
                                            AppendDataBoundItems="true"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="lstBranch" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="" ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Office Report  Start Date </label>
                                        </div>
<<<<<<< HEAD
                                        <asp:TextBox ID="txtOfficeVisitStartDate" runat="server" type="date" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtOfficeVisitStartDate_TextChanged"   ></asp:TextBox>
=======
                                        <asp:TextBox ID="txtOfficeVisitStartDate" runat="server" type="date" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtOfficeVisitStartDate_TextChanged"></asp:TextBox>
>>>>>>> 47e1c9f2 ([ENHANCEMENT][56245][Changes for added branch and dates])
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOfficeVisitStartDate"
                                            Display="None" ErrorMessage="Please Select Office Report Start Date" SetFocusOnError="True"
                                            ValidationGroup="Academic" InitialValue=""></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Office Report End Date</label>
                                        </div>
<<<<<<< HEAD
                                        <asp:TextBox ID="txtOfficeVisitEndDate" runat="server" type="date" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtOfficeVisitEndDate_TextChanged" ></asp:TextBox>
=======
                                        <asp:TextBox ID="txtOfficeVisitEndDate" runat="server" type="date" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtOfficeVisitEndDate_TextChanged"></asp:TextBox>
>>>>>>> 47e1c9f2 ([ENHANCEMENT][56245][Changes for added branch and dates])
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtOfficeVisitEndDate"
                                            Display="None" ErrorMessage="Please Select Office Report End Date" SetFocusOnError="True"
                                            ValidationGroup="Academic" InitialValue=""></asp:RequiredFieldValidator>
                                        <%--  <asp:CompareValidator ID="CompareValidator2" ValidationGroup="Academic" ForeColor="Red" runat="server"
                                            ControlToValidate="txtOfficeVisitStartDate" ControlToCompare="txtOfficeVisitEndDate" Operator="LessThan" Type="Date"
                                            Display="None" ErrorMessage=" Office Report  End Date Should Be Graeter Than Office Report Start Date."></asp:CompareValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Payment Start Date</label>
                                        </div>
                                        <asp:TextBox ID="txtStartDate" runat="server" type="date" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtStartDate_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtStartDate" runat="server" ControlToValidate="txtStartDate"
<<<<<<< HEAD
                                            Display="None" ErrorMessage="Please Select Payment Start Date" SetFocusOnError="True"
=======
                                            Display="None" ErrorMessage="Please Select Payment  Start Date" SetFocusOnError="True"
>>>>>>> 47e1c9f2 ([ENHANCEMENT][56245][Changes for added branch and dates])
                                            ValidationGroup="Academic" InitialValue=""></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Payment End Date</label>
                                        </div>
                                        <asp:TextBox ID="txtEndDate" runat="server" type="date" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtEndDate_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtEndDate" runat="server" ControlToValidate="txtEndDate"
                                            Display="None" ErrorMessage="Please Select Payment End Date" SetFocusOnError="True"
                                            ValidationGroup="Academic" InitialValue=""></asp:RequiredFieldValidator>
                                        <%--  <asp:CompareValidator ID="CompareValidator1" ValidationGroup="Academic" ForeColor="Red" runat="server"
                                            ControlToValidate="txtStartDate" ControlToCompare="txtEndDate" Operator="LessThan" Type="Date"
                                            Display="None" ErrorMessage="Payment Start  Date Should Be Graeter Than Office Report End Date."></asp:CompareValidator>--%>
                                    </div>



                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Provisional Admission Offer Valid Date </label>
                                        </div>
                                        <asp:TextBox ID="txtProvisionalAdmissionValidDate" runat="server" type="date" CssClass="form-control" autopostback="true"  OnTextChanged="txtProvisionalAdmissionValidDate_TextChanged1"></asp:TextBox>
                                        <%-- AutoPostBack="true" OnTextChanged="txtProvisionalAdmissionValidDate_TextChanged"--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtProvisionalAdmissionValidDate"
                                            Display="None" ErrorMessage="Please Select Provisional Admission Offer Valid Date" SetFocusOnError="True"
                                            ValidationGroup="Academic" InitialValue=""></asp:RequiredFieldValidator>
                                    </div>
<<<<<<< HEAD
=======



>>>>>>> 47e1c9f2 ([ENHANCEMENT][56245][Changes for added branch and dates])
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
                                        <asp:TextBox ID="txtAmount" runat="server" type="number" TextMode="Number" OnTextChanged="txtAmount_TextChanged"  min="0" CssClass="form-control" onKeyUp="setMaxLength(this)"  onkeypress="return (event.charCode >= 48 && event.charCode <= 57)"  isMaxLength="8" ></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="rfvtxtAmount" runat="server" ControlToValidate="txtAmount"
                                            Display="None" ErrorMessage="Please Enter Amount/Percentage" SetFocusOnError="True"
                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Status </label>
                                        </div>
                                        <div class="switch form-inline">
<<<<<<< HEAD
                                            <input type="checkbox" id="rdActive" name="Started" checked /> 

=======
                                            <input type="checkbox" id="rdActive" name="Started" checked />
>>>>>>> 47e1c9f2 ([ENHANCEMENT][56245][Changes for added branch and dates])
                                            <label data-on="Started" data-off="Stopped" for="rdActive"></label>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="validate();" CssClass="btn btn-primary" ValidationGroup="Academic" OnClick="btnSubmit_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="false" ValidationGroup="Academic" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" CausesValidation="false" OnClick="btnCancel_Click" />
                            </div>

                            <div class="col-12 mt-3" style="overflow-y: auto; max-height: 500px;">
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
                                                        <th>Branch</th>
<<<<<<< HEAD
                                                        <th>Payment Start Date</th>
                                                        <th>Payment End Date</th>
=======
                                                        <th>Start Date</th>
                                                        <th>End Date</th>
>>>>>>> 47e1c9f2 ([ENHANCEMENT][56245][Changes for added branch and dates])
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
                                                <td><%# Eval("LONGNAME") %></td>

                                                <td>
                                                    <%# Eval("STARTDATE") %>
                                                    <asp:HiddenField ID="hdnOfficeVisitStartDate" runat="server" Value='<%# Eval("OFFICE_VISIT_START_DATE") %>' />
                                                </td>


                                                <td>
                                                    <%# Eval("ENDDATE") %>
                                                    <asp:HiddenField ID="hdnOfficeVisitEndDate" runat="server" Value='<%# Eval("OFFICE_VISIT_End_DATE") %>' />
                                                    <asp:HiddenField ID="hdnProvisinalDate" runat="server" Value='<%# Eval("PROVISIONAL_ADMISSION_OFFER_VALID_DATE") %>' />
                                                </td>


                                                <td><%# Eval("PAYMENTCATEGORY") %></td>
                                                <td><%# Eval("FEEPAYMENT") %></td>
                                                <td><%--<%# Eval("ACTIVITYSTATUS") %>--%>
                                                    <asp:Label ID="lblStatus" runat="server" CssClass="badge" Text='<%# Eval("ACTIVITYSTATUS") %>'></asp:Label>
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

            <asp:AsyncPostBackTrigger ControlID="rdoActivityFor" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlProgramType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlPaymentCategory" EventName="SelectedIndexChanged" />

            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />

        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });


        $(document).ready(function () {
            ValidateDate();
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {

                ValidateDate();
            });
        });

        function ValidateDate() {
            $('#<%= txtStartDate.ClientID %>').click(function () {
                var endDate = $('#<%= txtOfficeVisitEndDate.ClientID %>').val();
                 var startDate = $(this).val();

                 if (endDate === '') {
                     alert('Please Select Office Report End Date First');
                     $('#<%= txtOfficeVisitEndDate.ClientID %>').focus();
                    $(this).val(''); // Clear the value of Start Date textbox
                }
             });



            $('#<%= txtOfficeVisitEndDate.ClientID %>').click(function () {
                var startDate = $('#<%= txtOfficeVisitStartDate.ClientID %>').val();

                if (startDate === '') {
                    alert('Please Select Office Report Start Date first');
                    $('#<%= txtOfficeVisitStartDate.ClientID %>').focus();
                    $('#<%= txtOfficeVisitEndDate.ClientID %>').val('');
                }
            });


            $('#<%= txtEndDate.ClientID %>').click(function () {
                var startDate = $('#<%= txtStartDate.ClientID %>').val();
                var endDate = $(this).val();

                if (startDate === '') {
                    alert('Please Select Payment Start Date first');
                    $('#<%= txtStartDate.ClientID %>').focus();
<<<<<<< HEAD
                    $(this).val(''); 
=======
                    $(this).val(''); // Clear the value of End Date textbox
>>>>>>> 47e1c9f2 ([ENHANCEMENT][56245][Changes for added branch and dates])
                }
            });


            $('#<%= txtProvisionalAdmissionValidDate.ClientID %>').click(function () {
                var endDate = $('#<%= txtEndDate.ClientID %>').val();
                var paymentValidDate = $(this).val();

                if (endDate === '') {
                    alert('Please Select Payment End Date first');
                    $('#<%= txtEndDate.ClientID %>').focus();
                    $(this).val(''); // Clear the value of Payment Valid Date textbox
                }
            });
        }
    </script>
</asp:Content>
