<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="HostelGatePassRequest.aspx.cs" Inherits="HOSTEL_GATEPASS_HostelGatePassRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <style>
            ul.ui-autocomplete
            {
                max-height: 180px !important;
                overflow: auto !important;
            }
        </style>
    <script src="../../plugins/jQuery/jQuery-2.2.0.min.js"></script>
    <link href="../../Css/COMMON/Commonstyle.css" rel="stylesheet" />
    <link href="../../Css/COMMON/FixHeader.css" rel="stylesheet" />
    <script src="../../Js/COMMON/Validation.js"></script>
    <link href="../../Css/COMMON/ajaxCalender.css" rel="stylesheet" />
    <script src="../../Datatable/jquery.dataTables.min.js"></script>
    <script src="../../Datatable/dataTables.bootstrap.min.js"></script>
    <script src="../../Datatable/dataTables.responsive.min.js"></script>
    <link href="../../Datatable/responsive.bootstrap.min.css" rel="stylesheet" />
    <link href="../../Datatable/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../../Datatable/responsive.bootstrap.min.css" rel="stylesheet" />
      
    <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };

        onkeypress = "return CheckAlphabet(event,this);"
        function CheckAlphabet(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;
            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }

        function formatNumber(input) {
            var value = input.value;
            if (value < 10) {
                input.value = '0' + value;
            }
        }
   
        function validate(key) {

            var rbs = $('#<%= rboption.ClientID %> input:checked').val();
                var keycode = (key.which) ? key.which : key.keyCode;
                if (rbs == '1') {

                    if (!(keycode == 8 || keycode == 46 || keycode == 32 || keycode == 9) && (keycode < 64 || keycode > 91) && (keycode < 97 || keycode > 122)) {
                        return false;
                    }
                    else {
                        return true;
                    }
                }
                else if (rbs == '2') {

                    if (!(keycode == 8 || keycode == 46 || keycode == 9) && (keycode < 48 || keycode > 57)) {
                        return false;
                    }
                }

                else if (rbs == '3') {

                    if (!(keycode == 8 || keycode == 46 || keycode == 9) && (keycode < 48 || keycode > 57)) {
                        return false;
                    }
                }

            }
        </script>

     <script>
         var prm = Sys.WebForms.PageRequestManager.getInstance();
         if (prm != null) {
             prm.add_endRequest(function (sender, e) {
                 if (sender._postBackSettings.panelsToUpdate != null) {

                     $("#<%=txtSearch.ClientID %>").autocomplete({
                            source: function (request, response) {
                                var obj = {};
                                obj.prefix = request.term;
                                obj.type = $('#<%= rboption.ClientID %> input:checked').val()
                                obj.CollegeId = $("#<%=hdnCollegeId.ClientID%>").val()
                                obj.SessionId = $("#<%=hdnSessionId.ClientID%>").val()

                                $.ajax({


                                    url: '<%=ResolveUrl("WebService.asmx/GetStudentForCancellation") %>',
                                    data: JSON.stringify(obj),
                                    dataType: "json",
                                    type: "POST",
                                    scroll: true,
                                    scrollHeight: 180,
                                    contentType: "application/json; charset=utf-8",
                                    success: function (data) {
                                        response($.map(data.d, function (item) {
                                            return {
                                                label: item.split('☺')[0],
                                                val: item.split('☺')[1]
                                            }
                                        }))
                                    },
                                    error: function (response) {
                                        alert(response.responseText);
                                    },
                                    failure: function (response) {
                                        alert(response.responseText);
                                    }
                                });
                            },

                            select: function (e, i) {
                                $("#<%=hdnStudentId.ClientID %>").val(i.item.val);
                            },
                            minLength: 1
                        });

                        }
                });
                };
        </script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js" type="text/javascript"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
        <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />


        <script type="text/javascript">

            $(document).ready(function () {

                $("#<%=txtSearch.ClientID %>").autocomplete({
                    source: function (request, response) {
                        var obj = {};
                        obj.prefix = request.term;
                        obj.type = $('#<%= rboption.ClientID %> input:checked').val()
                       obj.CollegeId = $("#<%=hdnCollegeId.ClientID%>").val()
                       obj.SessionId = $("#<%=hdnSessionId.ClientID%>").val()

                       $.ajax({


                           url: '<%=ResolveUrl("WebService.asmx/GetStudentForCancellation") %>',
                           data: JSON.stringify(obj),
                           dataType: "json",
                           type: "POST",
                           scroll: true,
                           scrollHeight: 180,
                           contentType: "application/json; charset=utf-8",
                           success: function (data) {
                               response($.map(data.d, function (item) {
                                   return {
                                       label: item.split('☺')[0],
                                       val: item.split('☺')[1]
                                   }
                               }))
                           },
                           error: function (response) {
                               alert(response.responseText);
                           },
                           failure: function (response) {
                               alert(response.responseText);
                           }
                       });
                   },

                    select: function (e, i) {
                        $("#<%=hdnStudentId.ClientID %>").val(i.item.val);
                   },
                   minLength: 1
                });

            });

        </script>--%>


    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Hostel Gate Pass Request</h3>
                </div>
                <br /><br /><br />
                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Search By</label>
                                </div>
                                <div class="col-sm-8">
                                                <asp:RadioButtonList ID="rboption" onkeydown="return (event.keyCode!=13);" TabIndex="4" runat="server" RepeatDirection="Horizontal" Enabled="True">
                                                    <asp:ListItem Value="1" Selected="True" Text="Name"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Student Id"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="UID"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Search </label>
                                </div>
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search Student Here" TabIndex="5" MaxLength="64">
                                                    </asp:TextBox>
                                </div>

                                <asp:HiddenField ID="hdnStudentId" runat="server" />
                                                <asp:HiddenField ID="hdnCourseId" runat="server" />
                                                <asp:HiddenField ID="hdnCollegeId" runat="server" />
                                                <asp:HiddenField ID="hdnSessionId" runat="server" />
                        </div>
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>OUT Date </label>
                                </div>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i id="imgoutDate" runat="server" class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtoutDate" runat="server" TabIndex="1" CssClass="form-control" AutoPostBack="true" ValidationGroup="submit"
                                        />
                                    <ajaxToolKit:CalendarExtender ID="ceoutDate" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtoutDate" PopupButtonID="imgoutDate" />
                                    <ajaxToolKit:MaskedEditExtender ID="meoutDate" runat="server" TargetControlID="txtoutDate"
                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" MaskType="Date" />
                                    <ajaxToolKit:MaskedEditValidator ID="mvoutDate" runat="server" EmptyValueMessage="Please Select Out Date"
                                        ControlExtender="meoutDate" ControlToValidate="txtoutDate" IsValidEmpty="false"
                                        InvalidValueMessage="Date is invalid" Display="None" ErrorMessage="Please Select Date"
                                        InvalidValueBlurredMessage="*" ValidationGroup="submit" SetFocusOnError="true" />
                                    <asp:CompareValidator ID="cvoutDate" runat="server" ControlToValidate="txtoutDate"
                                        Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter a valid out date mm/dd/yyyy)."
                                        EnableClientScript="False" ValidationGroup="submit">
                                    </asp:CompareValidator>
                                </div>
                            </div>
                            <div class="form-group col-lg-1 col-md-4 col-12">
                            </div>
                            <div class="form-group col-lg-2 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hour From</label>
                                </div>
                                <asp:TextBox ID="txtoutHourFrom" oninput="formatNumber(this)" CssClass="form-control" runat="server" TabIndex="2" TextMode="Number" Min="1" Max="12"/>
                                <asp:RequiredFieldValidator ID="rfvoutHourFrom" Display="None" runat="server" ErrorMessage="Please Select Out Hour From" ControlToValidate="txtoutHourFrom" ValidationGroup="submit"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-2 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Minutes From</label>
                                </div>
                                <asp:TextBox ID="txtoutMinFrom" oninput="formatNumber(this)" CssClass="form-control" runat="server" TabIndex="3" TextMode="Number" Min="0" Max="60"/>
                                <asp:RequiredFieldValidator ID="rfvoutMinFrom" Display="None" runat="server" ErrorMessage="Please Select Out Minutes From" ControlToValidate="txtoutMinFrom" ValidationGroup="submit"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-2 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>AM/PM</label>
                                </div>
                                <asp:DropDownList ID="DropDownList1" AppendDataBoundItems="true" runat="server" TabIndex="4" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem>AM</asp:ListItem>
                                    <asp:ListItem>PM</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDropDownList1" Display="None" runat="server" ErrorMessage="Please Select AM/PM For Out Date" ControlToValidate="DropDownList1" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>IN Date </label>
                                </div>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i id="imginDate" runat="server" class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtinDate" runat="server" TabIndex="5" CssClass="form-control" AutoPostBack="true" ValidationGroup="submit"
                                        />
                                    <ajaxToolKit:CalendarExtender ID="ceinDate" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtinDate" PopupButtonID="imginDate" />
                                    <ajaxToolKit:MaskedEditExtender ID="meinDate" runat="server" TargetControlID="txtinDate"
                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" MaskType="Date" />
                                    <ajaxToolKit:MaskedEditValidator ID="mvinDate" runat="server" EmptyValueMessage="Please Select In Date"
                                        ControlExtender="meinDate" ControlToValidate="txtinDate" IsValidEmpty="false"
                                        InvalidValueMessage="Date is invalid" Display="None" ErrorMessage="Please Select Date"
                                        InvalidValueBlurredMessage="*" ValidationGroup="submit" SetFocusOnError="true" />
                                    <asp:CompareValidator ID="cvinDate" runat="server" ControlToValidate="txtinDate"
                                        Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter a valid in date mm/dd/yyyy)."
                                        EnableClientScript="False" ValidationGroup="submit">
                                    </asp:CompareValidator>
                                </div>
                            </div>
                            <div class="form-group col-lg-1 col-md-4 col-12">
                            </div>
                            <div class="form-group col-lg-2 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hour From</label>
                                </div>
                                <asp:TextBox ID="txtinHourFrom" oninput="formatNumber(this)" CssClass="form-control" runat="server" TabIndex="6" TextMode="Number" Min="1" Max="12"/>
                                <asp:RequiredFieldValidator ID="rfvinHourFrom" Display="None" runat="server" ErrorMessage="Please Select In Hour From" ControlToValidate="txtinHourFrom" ValidationGroup="submit"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-2 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Minutes From</label>
                                </div>
                                <asp:TextBox ID="txtinMinFrom" oninput="formatNumber(this)" CssClass="form-control" runat="server" TabIndex="7" TextMode="Number" Min="0" Max="60"/>
                                <asp:RequiredFieldValidator ID="rfvinMinFrom" Display="None" runat="server" ErrorMessage="Please Select In Minutes From" ControlToValidate="txtinMinFrom" ValidationGroup="submit"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-2 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>AM/PM</label>
                                </div>
                                <asp:DropDownList ID="DropDownList2" AppendDataBoundItems="true" runat="server" TabIndex="8" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                    <asp:ListItem>AM</asp:ListItem>
                                    <asp:ListItem>PM</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDropDownList2" Display="None" runat="server" ErrorMessage="Please Select AM/PM For In Date" ControlToValidate="DropDownList2" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                         <div class="form-group col-lg-8 col-md-4 col-12">
                             <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Purpose</label>
                                </div>
                                <asp:DropDownList ID="ddlPurpose" AppendDataBoundItems="true" runat="server" TabIndex="9" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlPurpose_SelectedIndexChanged">
                                <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPurpose" Display="None" runat="server" ErrorMessage="Please Select Purpose" ControlToValidate="ddlPurpose" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                         </div>

                        <div class="form-group col-lg-8 col-md-4 col-12">
                                <asp:TextBox ID="txtOther" runat="server" CssClass="form-control" TabIndex="10" Visible="False" PlaceHolder="Enter Your Purpose" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvOther" runat="server" ErrorMessage="Please Enter Other Purpose"
                                    Display="None" ControlToValidate="txtOther" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                         </div>

                        <div class="form-group col-lg-8 col-md-4 col-12">
                             <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Remark</label>
                                </div>
                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" TabIndex="11" Rows="1" Height="74px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtRemark" runat="server" ErrorMessage="Please Enter Remark"
                                    Display="None" ControlToValidate="txtRemark" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                         </div>
                    </div>
                    <br /><br />
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit" TabIndex="12"
                            CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="13"
                             CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                        <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="submit" />
                    </div>

                    <div class="col-12">
                        <asp:Repeater ID="lvGatePass" runat="server">
                            <HeaderTemplate>
                                <div class="sub-heading">
                                    <h5>List of Hostel Purposes</h5>
                                </div>
                                <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Edit
                                            </th>
                                            <th>Student Name
                                            </th>
                                            <th>Out Date
                                            </th>
                                            <th>In Date
                                            </th>
                                            <th>Purpose
                                            </th>
                                            <th>Remarks
                                            </th>
                                            <th>
                                                Status
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("STUDNAME") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="6" />&nbsp;
                                    </td>
                                    <td>
                                         <%# Eval("STUDNAME") %>
                                    </td>
                                    <td>
                                        <%# Eval("OUTDATE ") %>
                                    </td>
                                    <td>
                                        <%# Eval("INDATE ") %>
                                    </td>
                                    <td>
                                        <%# Eval("PURPOSE_NAME") %>
                                    </td>
                                    <td>
                                        <%# Eval("REMARKS") %>
                                    </td>
                                    <td>
                                        <%# Eval("STATUS") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody></table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .form-control {}
    </style>
</asp:Content>



