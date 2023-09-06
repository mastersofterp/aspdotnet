<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="TimeTableShift.aspx.cs" Inherits="ACADEMIC_TimeTable_TimeTableShift" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
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
    </div>
    <asp:UpdatePanel ID="updTimeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchoolInstitute" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSchoolInstitute_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSchoolInstitute"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" TabIndex="2" AutoPostBack="true" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                   
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" TabIndex="3" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDepartment"
                                            Display="None" ErrorMessage="Please Select ddlDepartment" InitialValue="0" ValidationGroup="course">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="4"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="5"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Programme/ Branch" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" TabIndex="6"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select scheme" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" AutoPostBack="True" data-select2-enable="true"
                                            CssClass="form-control" TabIndex="7">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="True" AutoPostBack="true" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" CssClass="form-control" TabIndex="8">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSection" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSlotType" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSlotType" runat="server" AppendDataBoundItems="True" TabIndex="9" data-select2-enable="true"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlSlotType_SelectedIndexChanged" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSlotType" runat="server" ControlToValidate="ddlSlotType"
                                            Display="None" ErrorMessage="Please Select Slot Type" InitialValue="0" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>



                                    <div class="form-group col-lg-3 col-md-6 col-12 ">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Existing Dates</label>
                                        </div>
                                        <asp:DropDownList ID="ddlExistingDates" runat="server" AppendDataBoundItems="True" AutoPostBack="true" data-select2-enable="true"
                                            TabIndex="10" OnSelectedIndexChanged="ddlExistingDates_SelectedIndexChanged" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlExistingDates"
                                            Display="None" ErrorMessage="Please Select Existing Dates" InitialValue="0" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>



                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Time Table Day </label>
                                        </div>
                                        <asp:DropDownList ID="ddlTimeTableDays" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlTimeTableDays_SelectedIndexChanged" AutoPostBack="true"
                                            ValidationGroup="teacherallot" TabIndex="11" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlTimeTableDays" runat="server" ControlToValidate="ddlTimeTableDays"
                                            Display="None" ErrorMessage="Please Select Time Table Day" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12 ">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Time Table (Shift) Date  </label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="shiftDate" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtShiftTTDate" runat="server" TabIndex="12" ValidationGroup="submit"
                                                CssClass="form-control" AutoPostBack="true" OnTextChanged="txtShiftTTDate_TextChanged" placeholder="DD/MM/YYYY" />
                                            <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtShiftTTDate" PopupButtonID="shiftDate" />
                                            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtShiftTTDate"
                                                Display="None" ErrorMessage="Please Enter Time Table(Shift) Date" SetFocusOnError="True"
                                                ValidationGroup="submit" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtShiftTTDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                       
                        <div class="col-12 btn-footer">
                            <asp:LinkButton ID="btnShow" runat="server" TabIndex="13" ValidationGroup="submit"
                                OnClick="btnShow_Click" CssClass="btn btn-primary">Show </asp:LinkButton>

                            <asp:LinkButton ID="btnSubmit" runat="server" TabIndex="14" ValidationGroup="submit" Enabled="false" OnClick="btnSubmit_Click" OnClientClick="return confirm ('Do you really want to shift selected time table, once submit you can not change or modify!');"
                                CssClass="btn btn-primary"> Submit</asp:LinkButton>

                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click"
                                TabIndex="15" />

                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />

                        </div>
                        <div class="col-12">
                         <div class="row">
                            <div class="form-group col-lg-5 col-md-12 col-12">
                            <div class=" note-div">
                                <h5 class="heading">Note</h5>
                                <p runat="server" id="spanNote" ><i class="fa fa-star" aria-hidden="true"></i><span>After Shifting time table you can't change/modify it.</span></p>
                                               
                            </div>
                        </div>
                       </div>
                         </div>

                        <div class="col-12">
                            <asp:Panel ID="pnllv" runat="server" Visible="true">
                              
                                    <asp:ListView ID="lvTimeTableShift" runat="server">
                                        <LayoutTemplate>
                                            <table id="example" class="table table-striped table-bordered nowrap display">
                                              <div class="sub-heading">
                                                <h5>Selected Day TimeTable </h5>
                                            </div>
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Sr.No. 
                                                        </th>
                                                        <th>
                                                            Select All<asp:CheckBox ID="cbHead" ToolTip="Select all" Checked="true" Enabled="false" runat="server" AutoPostBack="false" onclick="SelectAll(this)" />
                                                        </th>
                                                        <th>FACULTY 
                                                        </th>
                                                        <th>CCODE
                                                        </th>
                                                        <th><asp:label ID="lblDYtxtCourseName" runat="server" Font-Bold="true" style="text-transform: uppercase;"></asp:label>
                                                        </th>
                                                        <th>BATCH  
                                                        </th>
                                                        <th>TIME SLOT
                                                        </th>
                                                        <th><asp:label ID="lblDYddlCourseType" runat="server" Font-Bold="true" style="text-transform: uppercase;"></asp:label>
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
                                                <td ><%# Container.DataItemIndex+1 %></td>
                                                <td>
                                                    <asp:CheckBox ID="chkAccept" runat="server" ToolTip='<%# Eval("COURSENO")%>'
                                                        Enabled="false" />
                                                    <asp:HiddenField ID="hdnStatus" runat="server" Value='<%# Eval("Status")%>' />
                                                    <%-- Enabled='<%# (Eval("Status").ToString() == "1" ? false : true)%>'
                                                Checked='<%# Eval("Status").ToString()=="1"?true:false%>'--%>
                                                </td>
                                                <td>
                                                    <%# Eval("UA_FULLNAME")%>
                                                    <asp:HiddenField ID="hdnUaNo" runat="server" Value='<%# Eval("UA_NO")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("CCODE")%>
                                                    <asp:HiddenField ID="hdnSubid" runat="server" Value='<%# Eval("SUBID")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("COURSE_NAME")%>
                                                    <asp:HiddenField ID="hdnCourseNo" runat="server" Value='<%# Eval("COURSENO")%>' />
                                                </td>
                                                <td style="text-align: center">
                                                    <%# Eval("BATCHNAME")%>
                                                    <asp:HiddenField ID="hdnbatchNo" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("TIMESLOT")%>
                                                    <asp:HiddenField ID="hdnSlotNo" runat="server" Value='<%# Eval("SLOTNO")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("SUBNAME")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr>
                                                <td><%# Container.DataItemIndex+1 %></td>
                                                <td>
                                                    <asp:CheckBox ID="chkAccept" runat="server" ToolTip='<%# Eval("COURSENO")%>'
                                                        Enabled="false" />
                                                    <asp:HiddenField ID="hdnStatus" runat="server" Value='<%# Eval("Status")%>' />
                                                    <%-- Enabled='<%# (Eval("Status").ToString() == "1" ? false : true)%>'
                                                Checked='<%# Eval("Status").ToString()=="1"?true:false%>'--%>
                                                </td>
                                                <td>
                                                    <%# Eval("UA_FULLNAME")%>
                                                    <asp:HiddenField ID="hdnUaNo" runat="server" Value='<%# Eval("UA_NO")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("CCODE")%>
                                                    <asp:HiddenField ID="hdnSubid" runat="server" Value='<%# Eval("SUBID")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("COURSE_NAME")%>
                                                    <asp:HiddenField ID="hdnCourseNo" runat="server" Value='<%# Eval("COURSENO")%>' />
                                                </td>
                                                <td style="text-align: center">
                                                    <%# Eval("BATCHNAME")%>
                                                    <asp:HiddenField ID="hdnbatchNo" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("TIMESLOT")%>
                                                    <asp:HiddenField ID="hdnSlotNo" runat="server" Value='<%# Eval("SLOTNO")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("SUBNAME")%>
                                                </td>
                                            </tr>
                                        
                                        </AlternatingItemTemplate>
                                        <EmptyItemTemplate>
                                            <p>-- Record not found --</p>
                                        </EmptyItemTemplate>
                                    </asp:ListView>
                                
                            </asp:Panel>
                        </div>
                         </div>

                        <!-- /.box-body -->
                        <div id="divMsg" runat="Server">
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>

    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        function SelectAll(obj) {
            var table = $(obj).closest('table');
            var th_s = table.find('th');
            var current_th = $(obj).closest('th');
            var columnIndex = th_s.index(current_th) + 1;
            table.find('td:nth-child(' + (columnIndex) + ') input').prop("checked", obj.checked);
        }
    </script>

    <%--  <script type="text/javascript">
        $(document).ready(function () {
            // alert('ok');
            bindDataTable1();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable1);
        });
        function bindDataTable1() {
            var myDT = $('#example').DataTable();
        }
    </script>--%>

    

</asp:Content>

