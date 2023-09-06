<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="TeachingPlan.aspx.cs" Inherits="Itle_TeachingPlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="ckeditor/ckeditor_basic.js" type="text/javascript"></script>--%>
    <script src="../plugins/ckeditor/ckeditor.js"></script>
    <script src="../plugins/ckeditor/ckeditor_basic.js"></script>

    <style>
     .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdTeachingPlan"
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
    <asp:UpdatePanel ID="UpdTeachingPlan" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TEACHING PLAN CREATION</h3>
                        </div>


                        <asp:Panel ID="pnldemo" runat="server" Visible="false">
                             <div class="form-group col-lg-3 col-md-6 col-12 mb-3" >
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Start Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgStartDt" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>

                                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" TabIndex="1"
                                                        ToolTip="Enter Teaching Plan Start Date" Style="z-index: 0;"  />
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                        PopupButtonID="imgStartDt" TargetControlID="txtStartDate" OnClientDateSelectionChanged="CheckDateEalier" />
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtStartDate"
                                                        Mask="99/99/9999" AutoComplete="true" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                        OnInvalidCssClass="MaskedEditError" MaskType="Date" ErrorTooltipEnabled="True" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtStartDate"
                                                        Display="None" ErrorMessage="Please enter start date." ValidationGroup="submit" />
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12 mb-3">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Start Time</label>
                                                </div>
                                                <asp:TextBox ID="txtStartTime" runat="server" CssClass="form-control" TabIndex="2"
                                                    ToolTip="Enter Teaching Plan Start Time" />
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtStartTime"
                                                    Mask="99:99" AcceptAMPM="true" AutoComplete="true" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" MaskType="Time" ErrorTooltipEnabled="True" />

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>End Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgEndDt" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" TabIndex="3"
                                                        ToolTip="Enter Teaching Plan End Date" Style="z-index: 0;" />
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgEndDt"
                                                        TargetControlID="txtEndDate" OnClientDateSelectionChanged="CheckDateEalier" />
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="txtEndDate"
                                                        Mask="99/99/9999" AutoComplete="true" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                        OnInvalidCssClass="MaskedEditError" MaskType="Date" ErrorTooltipEnabled="True" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtEndDate"
                                                        Display="None" ErrorMessage="Please enter end date." ValidationGroup="submit" />
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>End Time</label>
                                                </div>
                                                <asp:TextBox ID="txtEndTime" runat="server" CssClass="form-control" TabIndex="4"
                                                    ToolTip="Enter Teaching Plan End Time" />
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender7" runat="server" TargetControlID="txtEndTime"
                                                    Mask="99:99" AcceptAMPM="true" AutoComplete="true" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" MaskType="Time" ErrorTooltipEnabled="True" />

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Plan Name</label>
                                                </div>
                                                <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" ToolTip="Enter Plan Name" TabIndex="5" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubject"
                                                    Display="None" ErrorMessage="Please enter Plan Name." ValidationGroup="submit" />

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Tr1" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Syllabus Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSyllabus" runat="server" AutoPostBack="true" TabIndex="6"
                                                    CssClass="form-control" ToolTip="Select Syllabus Name"
                                                    OnSelectedIndexChanged="ddlSyllabus_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <%--AppendDataBoundItems="true"--%>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSyllabus"
                                                    Display="None" ErrorMessage="Please Select Syllabus." ValidationGroup="submit" />

                                            </div>
                                           
                                        
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Unit Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlUnit" runat="server" AppendDataBoundItems="true" TabIndex="7"
                                                    CssClass="form-control" data-select2-enable="true" ToolTip="Select Unit Name" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlUnit"
                                                        Display="None" ErrorMessage="Please Select Unit" ValidationGroup="submit"
                                                        SetFocusOnError="True" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlUnit"
                                                    Display="None" ErrorMessage="Please Enter Unit." ValidationGroup="submit" />--%>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Topic Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlTopic" runat="server" AppendDataBoundItems="true" TabIndex="8"
                                                    CssClass="form-control" data-select2-enable="true" ToolTip="Enter Topic Name" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlTopic_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTopic"
                                                        Display="None" ErrorMessage="Please Select Topic" ValidationGroup="submit"
                                                        SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlTopic"
                                                    Display="None" ErrorMessage="Please Select Topic." ValidationGroup="submit" />--%>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Media</label>
                                                </div>
                                                <asp:TextBox ID="txtMedia" runat="server" CssClass="form-control" ToolTip="Enter Teaching Plan Media"
                                                    TabIndex="10" />
                                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMedia"
                                                    Display="None" ErrorMessage="Please Enter Media." ValidationGroup="submit" />--%>

                                            </div>
                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Description</label>
                                                </div>
                                                <div class="table table-responsive">
                                                    <CKEditor:CKEditorControl ID="ftbDescription" runat="server" Height="200" TabIndex="9"
                                                        BasePath="~/plugins/ckeditor" ToolbarStartupExpanded="false">		                        
                                                    </CKEditor:CKEditorControl>
                                                </div>

                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="submit"
                                                    OnClick="btnSubmit_Click" ToolTip="Click here to Submit" TabIndex="11" />
                                                <asp:Button ID="btnViewTeachingPlan" runat="server" Text="View Teaching Plan" CssClass="btn btn-primary"
                                                    OnClick="btnViewTeachingPlan_Click" ToolTip="Click here to view Teaching Plan"
                                                    TabIndex="13" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning"
                                                    OnClick="btnCancel_Click" TabIndex="12" ToolTip="Click here to Reset" />

                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                            </div>
                                            <div class="col-12">
                                                <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl" />
                                            </div>
                                        </div>
                        </asp:Panel>
                        <div class="box-body">
                            <asp:Panel ID="pnlTeaching" runat="server">
                                <div class="col-12">
                                    <asp:Panel ID="pnlTeachingPlan" runat="server">
                                        <div class="row">
                                            
                                            <div class="col-lg-5 col-md-6 col-12 mb-3">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Current Session :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSession" runat="server" Font-Bold="true" />

                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-7 col-md-6 col-12 mb-3">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Course Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblCourseName" runat="server" Font-Bold="true" />

                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                           

                                    </asp:Panel>
                                </div>

                                <div class="col-12 mb-5">
                                    <div class="sub-heading">
                                        <h5>Teaching-Plan</h5>
                                    </div>

                                    <asp:Panel ID="pnlTeachingPlanList" runat="server">
                                        <asp:ListView ID="lvTPlan" runat="server" DataKeyNames="TP_NO">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                          <%--  <th>Action</th>--%>
                                                             <%-- <th>Media</th>--%>
                                                             <%--<th>End Date</th>
                                                            <th>Status</th>--%>
                                                            <th>Unit No.</th>
                                                            <th>Topic No.</th>
                                                            <th>Topic</th>
                                                             <th>Section</th>
                                                              <th>Schedule Date</th>
                                                           <th>Conducted Date</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                   <%-- <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit1.png" CommandArgument='<%# Eval("PLAN_NO") %>'
                                                            ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" TabIndex="14" />&nbsp;
                                                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("PLAN_NO") %>'
                                                                                ToolTip="Delete Record" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" TabIndex="15" />
                                                    </td>--%>
                                                    
                                                    <td>
                                                        <%# Eval("UNIT_NO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("LECTURE_NO")%>                                                                  
                                                    </td>
                                                    <td>
                                                        <%# Eval("TOPIC_COVERED")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SECTIONNAME")%>                                                                  
                                                    </td>
                                                     <td>
                                                        <%# Eval("DATE" ,"{0:dd-MMM-yyyy}")%>                                                                  
                                                    </td>
                                                     <td>
                                                        <%# Eval("CONDUCT_DATE" ,"{0:dd-MMM-yyyy}")%>                                                                  
                                                    </td>

                                                   <%-- <td>
                                                        <%# Eval("MEDIA")%>
                                                    </td>--%>
                                                   
                                                    
                                                     
                                                   <%-- <td>
                                                        <%# Eval("END_DATE", "{0:dd-MMM-yyyy}")%>                                                     
                                                    </td>
                                                    <td>
                                                        <%# GetStatus(Eval("END_DATE"))%>                                                                   
                                                    </td>--%>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <p class="text-center text-bold">
                                                    <asp:Label ID="lblEmptyMsg" runat="server" Text="No Records Found"></asp:Label>
                                                </p>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                            </asp:Panel>
                        </div>
                    </div>
                    </form>
                </div>
            </div>
            </div>
            </div>

            <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel" runat="server"
                TargetControlID="div" PopupControlID="div"
                OkControlID="btnOkDel" OnOkScript="okDelClick();"
                CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();" BackgroundCssClass="modalBackground" />
            <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                <div class="text-center">
                    <div class="modal-content">
                        <div class="modal-body">
                            <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.png" />
                            <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                            <div class="text-center">
                                <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                                <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }
    </script>
    <%--  !-- ========= Daterange Picker With Full Functioning Script added by gaurav 21-05-2021 ========== -->--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
                timePicker: true,
                //startDate: moment().subtract(00, 'days'),
                //endDate: moment(),
                startDate: moment().startOf('hour'),
                endDate: moment().startOf('hour').add(32, 'hour'),
                locale: {
                    format: 'DD MMM, YYYY hh:mm A'
                },
                //timePicker: true,

                //locale: {
                //    format: 'M/DD hh:mm A'
                //}
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
            debugger
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
        });

            $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#picker').daterangepicker({
                    timePicker: true,
                    //startDate: moment().subtract(00, 'days'),
                    //endDate: moment(),
                    startDate: moment().startOf('hour'),
                    endDate: moment().startOf('hour').add(32, 'hour'),
                    locale: {
                        format: 'DD MMM, YYYY hh:mm A'
                    },
                    //timePicker: true,

                    //locale: {
                    //    format: 'M/DD hh:mm A'
                    //}
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
                debugger
                $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            });
        });

    </script>
     <script type="text/javascript">                                               //gayatri rode 14-01-2021
         function CheckDateEalier(sender, args) {
             if (sender._selectedDate < new Date()) {
                 alert("Past Date Not Accepted for Start Date");
                 sender._selectedDate = new Date();
                 sender._textbox.set_Value("");
             }
         }
    </script>

</asp:Content>
