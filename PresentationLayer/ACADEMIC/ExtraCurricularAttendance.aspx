<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ExtraCurricularAttendance.aspx.cs" Inherits="ACADEMIC_ExtraCurricularAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<style type="text/css">
        .ajax__calendar_container {
            z-index: 1000;
        }

        #ctl00_ContentPlaceHolder1_txtFromDate {
            z-index: 0;
        }
    </style>--%>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl_details"
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

    <asp:UpdatePanel ID="updpnl_details" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title"><%--Extra-Curricular Attendance--%></h3>
                            <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                           <%-- <label>Session</label>--%>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                           <%-- <label>Degree</label>--%>
                                             <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddldegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddldegree_SelectedIndexChanged" TabIndex="2" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddldegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                           <%-- <label>Scheme</label>--%>
                                            <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" TabIndex="3" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                         <%--   <label>Semester</label>--%>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlTerm" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged" TabIndex="4" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTerm"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Section</label>--%>
                                            <asp:Label ID="lblDYtxtSection" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlsection" runat="server" AppendDataBoundItems="true" AutoPostBack="True" ToolTip="Section Filter for Course only"
                                            OnSelectedIndexChanged="ddlsection_SelectedIndexChanged" TabIndex="5" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlsection"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar" id="imgCalFromDate" Style="cursor: pointer"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" ValidationGroup="submit" CssClass="form-control" TabIndex="6" />
                                            <ajaxToolKit:CalendarExtender ID="cetxtFromDate" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                PopupButtonID="imgCalFromDate" TargetControlID="txtFromDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" Mask="99/99/9999"
                                                MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                TargetControlID="txtFromDate" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" ControlExtender="meFromDate"
                                                ControlToValidate="txtFromDate" Display="None" EmptyValueMessage="Please Enter From Date"
                                                ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate" ValidationGroup="Show" 
                                                Display="None" ErrorMessage="Please Enter From Date">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar" id="imgCalToDate" Style="cursor: pointer"></i>
                                            </div>
                                            <asp:TextBox ID="txtTodate" runat="server" TabIndex="7" ValidationGroup="submit" CssClass="form-control" />
                                            <ajaxToolKit:CalendarExtender ID="ceTodate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgCalToDate"
                                                TargetControlID="txtTodate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtTodate" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" ControlExtender="meToDate"
                                                ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                            <ajaxToolKit:MaskedEditValidator ID="rfvMonth" runat="server" ControlExtender="meFromDate"
                                                ControlToValidate="txtFromDate" Display="None" EmptyValueMessage="Please Enter From Date"
                                                ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Daywise" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTodate" ValidationGroup="Show" 
                                                Display="None" ErrorMessage="Please Enter To Date">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Condition</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCondtion" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="8">
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>=</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Percentage</label>
                                        </div>
                                        <asp:TextBox ID="txtPercentage" runat="server" CssClass="form-control" TabIndex="9" placeholder="Please Enter Percentage"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvpercentage" runat="server" ControlToValidate="txtPercentage" ValidationGroup="Show" 
                                            Display="None" ErrorMessage="Please Enter Percentage">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <asp:ListView ID="lvCourse" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Course</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="Table3">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="cbHead" runat="server" AutoPostBack="true" OnCheckedChanged="cbHeadCourse_OnCheckChanged" />
                                                    </th>
                                                    <th><%--Course Code--%>
                                                        <asp:Label ID="lblDYtxtCourseCode" runat="server" Font-Bold="true"></asp:Label>
                                                    </th>
                                                    <th><%--Course Name--%>
                                                        <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label>
                                                    </th>
                                                    <th><%--Scheme Name--%>
                                                        <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label>
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
                                                <asp:CheckBox ID="cbRow" runat="server" />
                                                <asp:HiddenField ID="hdfIdNo" runat="server" Value='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblcode" Text='<%# Eval("CCODE")%>' runat="server" ToolTip='<%# Eval("CCODE")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblExamDate" Text='<%# Eval("COURSENAME")%>' runat="server" ToolTip='<%# Eval("CCODE")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSchemeName" Text='<%# Eval("SCHEMENAME")%>' runat="server" ToolTip='<%# Eval("SCHEMENO")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server" visible="false">
                                <div class="label-dynamic">
                                    <label>Address</label>
                                </div>
                                <asp:RadioButtonList ID="rblAddress" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">Local Address</asp:ListItem>
                                    <asp:ListItem Value="1">Permanent Address</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnExtraAttendance" runat="server" TabIndex="10" Text="Extra-Curricular Attendance Report" ValidationGroup="Show" CssClass="btn btn-info" OnClick="btnExtraAttendance_Click"><i class="fa fa-file" aria-hidden="true"></i> Extra-Curricular Attendance Report </asp:LinkButton>
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click" TabIndex="11" Text="Cancel" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="vlsSubmit" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />
                            </div>

                            <div class=" col-md-12">
                                <asp:ListView ID="lvStudent" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Student List
                                                <asp:Button ID="btnShowAllStudent" runat="server" CssClass="btn btn-warning" OnClick="btnShowAllStudent_Click" TabIndex="6" Text="Show All Students" Visible="false" />
                                            </h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="example2">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="cbHead" runat="server" AutoPostBack="true" OnCheckedChanged="cbHeadStudent_OnCheckChanged" />
                                                    </th>
                                                    <th>Sr. No. </th>
                                                    <th>Roll No </th>
                                                    <th>Student Name </th>
                                                    <th><%--Scheme --%> 
                                                        <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label>
                                                    </th>
                                                    <th>Sem. </th>
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
                                                <asp:CheckBox ID="cbRow" runat="server" />
                                                <asp:HiddenField ID="hdfIdNo" runat="server" Value='<%# Eval("IDNO")%>' />
                                            </td>
                                            <td><%# Container.DataItemIndex+1 %></td>
                                            <td><%# Eval("ROLL_NO") %></td>
                                            <td>
                                                <asp:Label ID="lblExamDate" runat="server" Text='<%# Eval("STUDNAME")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblScheme" runat="server" Text='<%# Eval("SCHEMENAME")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTERNAME")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2">

                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;" Visible="false"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <tr>
            <td>
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/Images/edit.png" AlternateText="Edit Record" />
                            Edit Record&nbsp;&nbsp;
                            <asp:Image ID="imgDelete" runat="server" ImageUrl="~/Images/delete.png" AlternateText="Delete Record" />
                            Delete Record
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </p>
                    </div>
                </div>

                <script type="text/javascript" language="javascript">
                    // Move an element directly on top of another element (and optionally
                    // make it the same size)
                    function Cover(bottom, top, ignoreSize) {
                        var location = Sys.UI.DomElement.getLocation(bottom);
                        top.style.position = 'absolute';
                        top.style.top = location.y + 'px';
                        top.style.left = location.x + 'px';
                        if (!ignoreSize) {
                            top.style.height = bottom.offsetHeight + 'px';
                            top.style.width = bottom.offsetWidth + 'px';
                        }
                    }

                    function toggleCheckBoxes(source, table1) {

                        var listView = document.getElementById(table1);
                        alert(listView);
                        for (var i = 0; i < listView.rows.length; i++) {
                            var inputs = listView.rows[i].getElementsByTagName('input');
                            for (var j = 0; j < inputs.length; j++) {
                                if (inputs[j].type == "checkbox")
                                    inputs[j].checked = source.checked;
                            }
                        }
                    }
                </script>

            </td>
        </tr>
    </table>

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">

        function SelectAll(headchk) {
            if (headchk.checked == true) {
                alert("ok1");
                for (var i = 0; i < 10; i++) {
                    alert("ok2");
                    var cbrows = document.getElementById('ctl00_ContentPlaceHolder1_lvCAE_CAEHead');
                    cbrows.checked = true;
                }
            }
        }
    </script>

</asp:Content>
