<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ValurAdded.aspx.cs" Inherits="ValurAdded" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        #ctl00_ContentPlaceHolder1_pnlvalue .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        .fa-plus {
            color: #17a2b8;
            border: 1px solid #17a2b8;
            border-radius: 50%;
            padding: 6px 8px;
            cursor: pointer;
        }

        .table-bordered > thead > tr > th {
            border-top: 1px solid #e5e5e5;
        }
    </style>
    <!-- -->

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">VALUE ADDED / SPECIALIZATION COURSES</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-6 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>College Scheme </label>
                                </div>
                                <asp:DropDownList ID="ddlCollegeScheme" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlCollegeScheme_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvcollege" runat="server" SetFocusOnError="True"
                                    ErrorMessage="Please Select College Scheme" InitialValue="0" ControlToValidate="ddlCollegeScheme"
                                    Display="None" ValidationGroup="submit" />
                            </div>


                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Semester </label>
                                </div>

                                <asp:DropDownList ID="ddlsemester" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlsemester_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="True"
                                    ErrorMessage="Please Select Semester." InitialValue="0" ControlToValidate="ddlsemester"
                                    Display="None" ValidationGroup="submit" />
                            </div>


                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Group Name </label>
                                </div>

                                <asp:DropDownList ID="ddlgroupname" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True"
                                    ErrorMessage="Please Select Group Name." InitialValue="0" ControlToValidate="ddlgroupname"
                                    Display="None" ValidationGroup="submit" />


                                <%--<asp:TextBox ID="txtGroupName" runat="server" CssClass="form-control" TabIndex="2" MaxLength="128"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvgroup" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Enter GroupName " ControlToValidate="txtGroupName"
                                            Display="None" ValidationGroup="submit" />--%>
                            </div>
                        </div>

                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Course </label>
                                </div>
                                <asp:DropDownList ID="ddlcourse" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="4" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvcourse" runat="server" SetFocusOnError="True"
                                    ErrorMessage="Please Select Course" InitialValue="0" ControlToValidate="ddlcourse"
                                    Display="None" ValidationGroup="submit" />
                            </div>

                            <div class="form-group col-lg-3 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Duration </label>
                                </div>
                                <asp:DropDownList ID="ddlDuration" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="5" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">Semester Long</asp:ListItem>
                                    <asp:ListItem Value="2">Year Long</asp:ListItem>
                                    <asp:ListItem Value="3">Dates</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="rfvduration" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Select Duration"  InitialValue="0" ControlToValidate="ddlDuration"
                                            Display="None" ValidationGroup="submit" />--%>
                            </div>

                            <%-- <div class="form-group col-lg-3 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Start End Date</label>
                                </div>
                                <div id="picker" class="form-control" tabindex="3">
                                    <i class="fa fa-calendar"></i>&nbsp;
                                            <span id="date"></span>
                                    <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                      
                                    
                                </div>--%>

                            <div class="form-group col-lg-3 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Start Date</label>
                                </div>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i id="dvcal1" runat="server" class="fa fa-calendar text-blue"></i>
                                    </div>
                                    <asp:TextBox ID="txtStartDate" runat="server" ValidationGroup="submit" onpaste="return false;"
                                        TabIndex="6" ToolTip="Please Enter Start Date" CssClass="form-control" Style="z-index: 0;" />

                                    <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtStartDate" PopupButtonID="dvcal1" Enabled="true" EnableViewState="true" />
                                    <%-- <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                                Display="None" ErrorMessage="Please Enter Session Start Date" SetFocusOnError="True"
                                                ValidationGroup="submit" />--%>
                                    <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                        TargetControlID="txtStartDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                        DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                    <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" ControlExtender="meeStartDate"
                                        ControlToValidate="txtStartDate" EmptyValueMessage="Please Enter Start Date"
                                        InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                        TooltipMessage="Please Enter Start Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                        ValidationGroup="submit" SetFocusOnError="True" />
                                    <%-- <asp:RequiredFieldValidator ID="rfvstartdate" runat="server" ControlToValidate="txtStartDate"
                                                Display="None" ErrorMessage="Please Select Start Date" SetFocusOnError="true"   ValidationGroup="submit" />--%>
                                </div>
                            </div>


                            <div class="form-group col-lg-3 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>End Date</label>
                                </div>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i id="dvcal2" runat="server" class="fa fa-calendar text-blue"></i>
                                    </div>
                                    <asp:TextBox ID="txtEndDate" runat="server" ValidationGroup="submit" TabIndex="7"
                                        ToolTip="Please Enter End Date" CssClass="form-control" Style="z-index: 0;" />
                                    <%-- <asp:Image ID="imgEDate" runat="server" ImageUrl="~/images/calendar.png" ToolTip="Select Date"
                                                    AlternateText="Select Date" Style="cursor: pointer" />--%>


                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtEndDate" PopupButtonID="dvcal2" Enabled="true" EnableViewState="true" />
                                    <%-- <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" SetFocusOnError="True"
                                                ErrorMessage="Please Enter Session End Date" ControlToValidate="txtEndDate" Display="None"
                                                ValidationGroup="submit" />--%>
                                    <ajaxToolKit:MaskedEditExtender ID="meeEndDate" runat="server" OnInvalidCssClass="errordate"
                                        TargetControlID="txtEndDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                        DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                    <ajaxToolKit:MaskedEditValidator ID="mevEndDate" runat="server" ControlExtender="meeEndDate"
                                        ControlToValidate="txtEndDate" EmptyValueMessage="Please Enter Session End Date"
                                        InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                        TooltipMessage="Please Enter End Date" EmptyValueBlurredText="Empty"
                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="submit" SetFocusOnError="True" />
                                    <asp:CompareValidator ID="CompareValidator1" ValidationGroup="submit" ForeColor="Red" runat="server"
                                        ControlToValidate="txtStartDate" ControlToCompare="txtEndDate" Operator="LessThan" Type="Date" Display="None"
                                        ErrorMessage="Start Date must be less than End Date."></asp:CompareValidator>
                                    <%-- <asp:RequiredFieldValidator ID="rfvenddate" runat="server" ControlToValidate="txtEndDate"
                                Display="None" ErrorMessage="Please Select End Date" SetFocusOnError="true"
                                ValidationGroup="submit" />--%>
                                </div>
                            </div>
                        </div>

                        <div class="row">

                            <div class="form-group col-lg-2 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Assessment Involved </label>
                                </div>
                                <asp:DropDownList ID="ddlassessment" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="8">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:DropDownList>
                                <%-- <asp:RequiredFieldValidator ID="rfvassessment" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Select Assessment Involved" InitialValue="0" ControlToValidate="ddlassessment"
                                            Display="None" ValidationGroup="submit" />--%>
                            </div>

                            <div class="form-group col-lg-2 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Show on Grade Card </label>
                                </div>
                                <asp:DropDownList ID="ddlgrade" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="9">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="rfvgrade" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Select Show on Grade Card"  InitialValue="0"  ControlToValidate="ddlgrade"
                                            Display="None" ValidationGroup="submit" />--%>
                            </div>

                            <div class="form-group col-lg-1 col-md-2 col-12">
                                <div class="label-dynamic">
                                    <label></label>
                                </div>
                                <%--<i class="fa fa-plus" aria-hidden="true"></i>--%>
                                <%-- <asp:ImageButton ID="btnImg" runat="server" CssClass="fa fa-plus" OnClick="btnImg_Click" />--%>


                                <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnadd_Click" ValidationGroup="submit" TabIndex="10"><i class="fa fa-plus" ></i></asp:LinkButton>

                            </div>
                        </div>



                        <div class="form-group col-md-12">
                            <asp:Panel ID="pnlvaluecourse" runat="server">
                                <asp:ListView ID="lvvaluecourse" runat="server">
                                    <LayoutTemplate>
                                        <div class="table table-responsive">
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit
                                                        </th>
                                                        <th>College Scheme</th>
                                                        <th>Semester</th>
                                                        <th>Group Name 
                                                        </th>
                                                        <th>Course
                                                        </th>
                                                        <th>Duration
                                                        </th>
                                                        <th>Start Date
                                                        </th>
                                                        <th>End Date
                                                        </th>
                                                        <th>Assessment Involved</th>
                                                        <th>Show on Grade Card
                                                        </th>
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
                                            <td>
                                                <%--<asp:ImageButton ID="btnEdit" runat="server" OnClick="btnEdit_Click"  CommandArgument='<%# Eval("COLLEGE_ID")%>'
                                                    ImageUrl="~/images/edit.gif" />--%>
                                                <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("VALUE_NO")%>'
                                                    ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click" />
                                            </td>
                                            <td id="colscheme" runat="server">
                                                <asp:Label ID="lblcolscheme" runat="server" Text='<%# Eval("COL_SCHEME_NAME") %>'></asp:Label>
                                                <asp:HiddenField ID="hdcolscheme" runat="server" Value='<%# Eval("COSCHNO") %>' />
                                            </td>
                                            <td id="Td1" runat="server">
                                                <asp:Label runat="server" ID="lblsemestername" Text='<%#Eval("SEMESTERNAME") %>'></asp:Label>
                                            </td>
                                            <td id="groupname" runat="server">
                                                <asp:Label runat="server" ID="lblgroupname" Text='<%#Eval("GROUPNAME") %>'></asp:Label>
                                            </td>
                                            <td id="coursename" runat="server">
                                                <asp:Label runat="server" ID="lblcoursename" Text='<%#Eval("COURSE_NAME") %>'></asp:Label>
                                                <asp:HiddenField ID="hdcoursename" runat="server" Value='<%# Eval("COURSENO") %>' />

                                            </td>
                                            <td id="duration" runat="server">
                                                <asp:Label runat="server" ID="lblDuration" Text='<%#Eval("DURATION") %>'></asp:Label>
                                                <asp:HiddenField ID="hdduration" runat="server" Value='<%# Eval("DURATION") %>' />
                                            </td>
                                            <td id="startdate" runat="server">
                                                <asp:Label runat="server" ID="lblstartdate" Text='<%#Eval("STARTDATE") %>'></asp:Label>
                                            </td>
                                            <td id="enddate" runat="server" style="text-align: center">
                                                <asp:Label runat="server" ID="lblenddate" Text='<%#Eval("ENDDATE") %>'></asp:Label>
                                            </td>
                                            <td id="assessment" runat="server" style="text-align: center">
                                                <asp:Label runat="server" ID="lblassessment" Text='<%#Eval("ASSESSMENT") %>'></asp:Label>
                                                <asp:HiddenField ID="hddassessment" runat="server" Value='<%# Eval("ASSESSMENT") %>' />
                                            </td>

                                            <td id="grade" runat="server" style="text-align: center">
                                                <asp:Label runat="server" ID="lblgrade" Text='<%#Eval("GRADE") %>'></asp:Label>
                                                <asp:HiddenField ID="hddgrade" runat="server" Value='<%# Eval("GRADE") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>

                        <div class="col-12 btn-footer">
                            <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click" TabIndex="11">Submit</asp:LinkButton>
                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" TabIndex="12">Cancel</asp:LinkButton>
                            <asp:LinkButton ID="btnview" runat="server" CssClass="btn btn-outline-info" OnClick="btnview_Click" TabIndex="13">View Group</asp:LinkButton>

                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                        </div>


                        <div class="col-12 mt-3">
                            <asp:Panel ID="pnlvalue" runat="server">
                                <asp:ListView ID="lvvalueaddedcourse" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Value Added List</h5>
                                        </div>

                                        <div class="table table-responsive">
                                            <table class="table table-striped table-bordered display nowrap" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit</th>
                                                        <th>College Scheme</th>
                                                        <th>Semester</th>
                                                        <th>Group Name 
                                                        </th>
                                                        <th>Course
                                                        </th>
                                                        <th>Duration
                                                        </th>
                                                        <th>Start Date
                                                        </th>
                                                        <th>End Date
                                                        </th>
                                                        <th>Assessment Involved</th>
                                                        <th>Show on Grade Card
                                                        </th>
                                                        <%-- <th>View</th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <%--  <tbody>--%>
                                        <tr>
                                            <td>
                                                <%--<i class="far fa-edit"></i>--%>
                                                <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("VALUE_NO")%>' CommandName='<%# Eval("GROUPID")%>'
                                                    ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click1" />

                                            </td>
                                            <td id="colscheme" runat="server">
                                                <asp:Label ID="lblcolscheme" runat="server" Text='<%# Eval("COL_SCHEME_NAME") %>'></asp:Label>
                                                <asp:HiddenField ID="hdcolscheme" runat="server" Value='<%# Eval("COSCHNO") %>' />
                                            </td>
                                            <td id="Td2" runat="server">
                                                <asp:Label runat="server" ID="lblsemester" Text='<%#Eval("SEMESTERNAME") %>'></asp:Label>
                                            </td>


                                            <td id="groupname" runat="server">
                                                <asp:Label runat="server" ID="lblgroupname" Text='<%#Eval("GROUP_NAME") %>'></asp:Label>
                                            </td>
                                            <td id="coursename" runat="server">
                                                <asp:Label runat="server" ID="lblcoursename" Text='<%#Eval("COURSENAME") %>'></asp:Label>
                                                <asp:HiddenField ID="hdcoursename" runat="server" Value='<%# Eval("COURSENO") %>' />

                                            </td>
                                            <td id="duration" runat="server">
                                                <asp:Label runat="server" ID="lblDuration" Text='<%#Eval("DURATION") %>'></asp:Label>
                                                <asp:HiddenField ID="hdduration" runat="server" Value='<%# Eval("DURATION") %>' />
                                            </td>
                                            <td id="startdate" runat="server">
                                                <asp:Label runat="server" ID="lblstartdate" Text='<%#Eval("STARTDATE") %>'></asp:Label>
                                            </td>
                                            <td id="enddate" runat="server" style="text-align: center">
                                                <asp:Label runat="server" ID="lblenddate" Text='<%#Eval("ENDDATE") %>'></asp:Label>
                                            </td>
                                            <td id="assessment" runat="server" style="text-align: center">
                                                <asp:Label runat="server" ID="lblassessment" Text='<%#Eval("ASSESSMENT") %>'></asp:Label>
                                                <asp:HiddenField ID="hddassessment" runat="server" Value='<%# Eval("ASSESSMENT") %>' />
                                            </td>
                                            <td id="grade" runat="server" style="text-align: center">
                                                <asp:Label runat="server" ID="lblgrade" Text='<%#Eval("GRADE") %>'></asp:Label>
                                                <asp:HiddenField ID="hddgrade" runat="server" Value='<%# Eval("GRADE") %>' />
                                            </td>

                                            <%-- <td>College Scheme</td>
                                        <td>Group Name details</td>--%>
                                            <%--                                        <td><asp:ImageButton ID="btnview"  runat="server"  CommandArgument='<%# Eval("VALUE_NO") %>' /><i class="fa fa-eye" aria-hidden="true" data-toggle="modal" data-target="#myModal_view" style="color: #28a745; font-size: 20px;"></i></asp:ImageButton></td>--%>
                                            <%--                                      <td><asp:ImageButton ID="btnview" runat="server" CommandArgument='<%# Eval("VALUE_NO") %>' data-target="#myModal_view" data-toggle="modal" Height="20px"   Text="<i class='fa fa-eye'></i>"  Width="20px"  /></td>--%>
                                            <%--  <td><i class="fa fa-eye" aria-hidden="true" data-toggle="modal" data-target="#myModal_view" style="color: #28a745; font-size: 20px;"></i>--%>
                                            <%--  <td>
                                          <%--  <asp:ImageButton ID="btneye" runat="server"  CommandArgument='<%# Eval("VALUE_NO") %>' 
                                             OnClick="btneye_Click" class="fa fa-eye" data-toggle="modal"  data-target="#myModal_view" style="color: #28a745; font-size: 20px;"/>--%>


                                            <%-- <asp:ImageButton ID="btneye" runat="server"  CommandArgument='<%# Eval("VALUE_NO")%>'
                                                            ImageUrl="~/Images/view2.png" OnClick="btneye_Click"/>
                                        
                                            </td>
                                     </tr>
                                    <%--</tbody>--%>
                                            <%-- </table>--%>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>

                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- The Modal -->
    <div class="form-group col-lg-12">
    </div>
    <div class="form-group col-lg-12">

        <asp:Button ID="btnForPopUpModel" Style="display: none" runat="server" Text="For PopUp Model Box" />

        <ajaxToolKit:ModalPopupExtender ID="upd_ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
            PopupControlID="pnl" TargetControlID="btnForPopUpModel"
            Enabled="True">
        </ajaxToolKit:ModalPopupExtender>
        <asp:Panel ID="pnl" runat="server" meta:resourcekey="pnlResource1">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">

                    <!-- Modal Header -->
                    <div class="modal-header">
                        <h4 class="modal-title">Value Added Details</h4>

                    </div>

                    <!-- Modal body -->
                    <div class="modal-body">
                        <div class="form-group col-lg-12">
                            <div class="row">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="form-group col-md-12">
                                            <div>
                                                <asp:DropDownList ID="ddlgroup" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="3" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlgroup" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>

                                <asp:UpdatePanel ID="updmodal" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">

                                            <asp:Panel ID="pnlmodal" runat="server">
                                                <asp:ListView ID="lvmodal" runat="server">
                                                    <LayoutTemplate>


                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Course</th>
                                                                    <th>Duration</th>
                                                                    <th>Start Date</th>
                                                                    <th>End Date</th>
                                                                    <th>Assessment Involved</th>
                                                                    <th>Show on Grade Card</th>
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

                                                            <td id="coursename" runat="server">
                                                                <asp:Label runat="server" ID="lblcoursename" Text='<%#Eval("COURSE_NAME") %>'></asp:Label>
                                                                <asp:HiddenField ID="hdcoursename" runat="server" Value='<%# Eval("COURSENO") %>' />

                                                            </td>
                                                            <td id="duration" runat="server">
                                                                <asp:Label runat="server" ID="lblDuration" Text='<%#Eval("DURATION") %>'></asp:Label>
                                                                <asp:HiddenField ID="hdduration" runat="server" Value='<%# Eval("DURATION") %>' />
                                                            </td>
                                                            <td id="startdate" runat="server">
                                                                <asp:Label runat="server" ID="lblstartdate" Text='<%#Eval("STARTDATE") %>'></asp:Label>
                                                            </td>
                                                            <td id="enddate" runat="server" style="text-align: center">
                                                                <asp:Label runat="server" ID="lblenddate" Text='<%#Eval("ENDDATE") %>'></asp:Label>
                                                            </td>
                                                            <td id="assessment" runat="server" style="text-align: center">
                                                                <asp:Label runat="server" ID="lblassessment" Text='<%#Eval("ASSESSMENT") %>'></asp:Label>
                                                                <asp:HiddenField ID="hddassessment" runat="server" Value='<%# Eval("ASSESSMENT") %>' />
                                                            </td>
                                                            <td id="grade" runat="server" style="text-align: center">
                                                                <asp:Label runat="server" ID="lblgrade" Text='<%#Eval("GRADE") %>'></asp:Label>
                                                                <asp:HiddenField ID="hddgrade" runat="server" Value='<%# Eval("GRADE") %>' />
                                                            </td>


                                                    </ItemTemplate>
                                                </asp:ListView>
                                                <%--  <div class="modal-footer">    
                               <asp:Button ID="btnback"  runat="server" Text="Close" OnClick="btnback_Click" class="btn btn-danger"/>
                   
                </div>--%>
                                                <%-- <div class="modal-footer">
                    <asp:Button ID="btnback"  runat="server" Text="Backs" OnClick="btnback_Click" />
                </div>--%>
                                            </asp:Panel>

                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                        <%--<asp:AsyncPostBackTrigger ControlID="ddlgroup"/>--%>
                                    </Triggers>
                                </asp:UpdatePanel>
                                <div class="modal-footer">
                                    <asp:Button ID="btnback" runat="server" Text="Close" OnClick="btnback_Click" class="btn btn-danger" />

                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Modal footer -->


                </div>
            </div>

        </asp:Panel>
    </div>


    <!-- ========= Daterange Picker With Full Functioning Script added by gaurav 21-05-2021 ========== -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                ranges: {
                    //                    'Today': [moment(), moment()],
                    //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                },
                //<!-- ========= Disable dates after today ========== -->
                //maxDate: new Date(),
                //<!-- ========= Disable dates after today END ========== -->
            },
        function (start, end) {
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
        });

            $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });



    </script>
    <script language="javascript" type="text/javascript">

        function getCurrentYear() {
            var cDate = new Date();
            return cDate.getFullYear();
        }

        function CheckDate(sender, args) {
            //var txtfrm = document.getElementById('txtFromDate')
            //var txtto = document.getElementById('txtToDate')
            if (sender._selectedDate > new Date()) {
                sender._selectedDate = new Date();
                alert("Do not select Future Date!");
                sender._textbox.set_Value("");
                document.getElementById("txtStartDate").value = '';
                document.getElementById("txtEndDate").value = "";
            }
        }
    </script>
</asp:Content>

