<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ClubActivity.aspx.cs" Inherits="ACADEMIC_ClubActivityRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>--%>


    <style>
        /*--======= toggle switch css added by gaurav 29072021 =======--*/
        .switch input[type=checkbox] {
            height: 0;
            width: 0;
            visibility: hidden;
        }

        .switch label {
            cursor: pointer;
            width: 82px;
            height: 34px;
            background: #dc3545;
            display: block;
            border-radius: 4px;
            position: relative;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }

            .switch label:hover {
                background-color: #c82333;
            }

            .switch label:before {
                content: attr(data-off);
                position: absolute;
                right: 0;
                font-size: 16px;
                padding: 4px 8px;
                font-weight: 400;
                color: #fff;
                transition: 0.35s;
                -webkit-transition: 0.35s;
                -moz-user-select: none;
                -webkit-user-select: none;
            }

        .switch input:checked + label:before {
            content: attr(data-on);
            position: absolute;
            left: 0;
            font-size: 16px;
            padding: 4px 15px;
            font-weight: 400;
            color: #fff;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }

        .switch label:after {
            content: '';
            position: absolute;
            top: 1.5px;
            left: 1.7px;
            width: 10.2px;
            height: 31.5px;
            background: #fff;
            border-radius: 2.5px;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }

        .switch input:checked + label {
            background: #28a745;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }

            .switch input:checked + label:hover {
                background: #218838;
            }

            .switch input:checked + label:after {
                transform: translateX(68px);
                transition: 0.35s;
                -webkit-transition: 0.35s;
                -moz-user-select: none;
                -webkit-user-select: none;
            }
    </style>

    <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updclub"
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


    <asp:UpdatePanel ID="updclub" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlMain">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12" id="divclub" runat="server">
                        <div class="box box-primary">
                            <div id="div1" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">Club Activity</h3>
                                <%--<h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>--%>
                            </div>
                            <%--<asp:UpdatePanel ID="updStudentInfo" runat="server">
                <ContentTemplate>--%>

                            <div id="divStudent" runat="server" visible="true">
                                <div class="colapse-panel" id="accordion">

                                    <div class="">
                                        <div class="col-12 colapse-heading">
                                            <div class="row">
                                                <div class="col-12 " data-target="#divStudentInfo">
                                                    <%-- <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divStudentInfo" aria-expanded="true" aria-controls="collapseOne">--%>
                                                    <%--  <i class="more-less fas fa-minus"></i>--%>
                                                    <div class="sub-heading">
                                                        <h5>Student Information
                                                        </h5>
                                                        <%--<asp:Image ID="ImageSearch" runat="server" ImageUrl="~/images/collapse_blue.jpg" alt=""
                                                            onclick="javascript:toggleExpansion(this,'divEmployeeSearchModify')" /></span></h5>--%>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>

                                        <div id="divStudentInfo" class="collapse show" data-parent="#accordion">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Student Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item"><b>
                                                                <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Email :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblEmail" runat="server" Font-Bold="True"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item"><b>MobileNo :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblMobileNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                            </li>



                                                        </ul>
                                                    </div>




                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>



                            </div>

                            <%--  <asp:Panel runat="server" ID="Panel1">--%>
                            <div>
                                <div class="box-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div id="dv" runat="server" class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Session</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" TabIndex="1" data-select2-enable="true"
                                                    AppendDataBoundItems="True" ToolTip="Please Select Session">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSession"
                                                    Display="None" ErrorMessage="Please Select Session" ValidationGroup="show"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divSubCounsellor" runat="server">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Select Club</label>
                                                    <%-- <asp:Label ID="lblDYsubuser" runat="server" Font-Bold="true" Text="SubCounsellors"></asp:Label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlsubuser" runat="server" CssClass="form-control" TabIndex="2" data-select2-enable="true"
                                                    AppendDataBoundItems="True" ToolTip="Please Select Club">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvclub" runat="server" ControlToValidate="ddlsubuser" InitialValue="0"
                                                    Display="None" ErrorMessage="Please Select Club" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <%--                                        <asp:ListBox ID="ddlsubuser" runat="server" AppendDataBoundItems="true"   CssClass="form-control multi-select-demo" SelectionMode="Multiple"   ></asp:ListBox>--%>

                                                <%-- <asp:ListBox ID="ddlsubuser" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"
                                            SelectionMode="multiple" TabIndex="8"></asp:ListBox>--%>
                                                <%--                                         <asp:RequiredFieldValidator ID="rfvSubUser" runat="server" ControlToValidate="ddlsubuser" Display="None" ErrorMessage="Please Select Sub User." ValidationGroup="submit" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Title of the event</label>
                                                </div>
                                                <asp:TextBox ID="txttitle" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="100" TabIndex="3"
                                                    ToolTip="Please Enter Type of Activity" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True"
                                                    ErrorMessage="Please Enter Title of Event" ControlToValidate="txttitle"
                                                    Display="None" ValidationGroup="submit" />

                                            </div>



                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Venue</label>
                                                </div>
                                                <asp:TextBox ID="txtvenue" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="100" TabIndex="4"
                                                    ToolTip="Please Enter EmailId" />
                                                <asp:RequiredFieldValidator ID="rfvvenue" runat="server" ControlToValidate="txtvenue" SetFocusOnError="true"
                                                    Display="None" ErrorMessage="Please Enter Venue" ValidationGroup="submit"></asp:RequiredFieldValidator>


                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <label><span style="color: red;">*</span> From Date</label>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <i id="dvcal1" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="5" CssClass="form-control"></asp:TextBox>
                                                    <%--  <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" CssClass="form-control"  />--%>
                                                    <%--<asp:Image ID="imgCalFromDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtFromDate" PopupButtonID="dvcal1" Enabled="true" EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <%--    <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                                        Display="None" ErrorMessage="Please enter initial date for report." SetFocusOnError="true"
                                        ValidationGroup="report" />--%>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                    <asp:RequiredFieldValidator ID="rfvtxtfromdate" runat="server" ControlToValidate="txtFromDate"
                                                        Display="None" ErrorMessage="Please Select From Date." SetFocusOnError="true"
                                                        ValidationGroup="submit">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <label><span style="color: red;">*</span> To Date</label>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <i id="dvtodate" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TabIndex="6"></asp:TextBox>
                                                    <%--<asp:TextBox ID="txtToDate" runat="server" TabIndex="1" CssClass="form-control" ></asp:TextBox>--%>
                                                    <%-- <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" CssClass="form-control" />--%>
                                                    <%-- <asp:Image ID="imgCalToDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtToDate" PopupButtonID="dvtodate" Enabled="true" EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <%-- <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                                        Display="None" ErrorMessage="Please enter last date for report." SetFocusOnError="true"
                                        ValidationGroup="report" />--%>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                    <%-- <asp:CompareValidator ID="CompareValidator1" ValidationGroup="submit" ForeColor="Red" runat="server"
                                        ControlToValidate="txtFromDate" ControlToCompare="txtToDate" Operator="LessThan" Type="Date" Display="None"
                                        ErrorMessage="From date must be less than To date." ></asp:CompareValidator>--%>
                                                    <asp:RequiredFieldValidator ID="rfvtodate" runat="server" ControlToValidate="txtToDate"
                                                        Display="None" ErrorMessage="Please Select To Date." SetFocusOnError="true"
                                                        ValidationGroup="submit"> </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Duration in Hrs</label>
                                                </div>
                                                <asp:TextBox ID="txtduration" runat="server" CssClass="form-control" TabIndex="7" onblur="validateTime(this)"></asp:TextBox>
                                                <ajaxToolKit:MaskedEditExtender ID="meeduration" runat="server" TargetControlID="txtduration"
                                                    Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" MaskType="Time" AcceptAMPM="False" ErrorTooltipEnabled="True"  />
                                                <ajaxToolKit:MaskedEditValidator ID="mevduration" runat="server" ControlExtender="meeduration"
                                                    ControlToValidate="txtduration" IsValidEmpty="False" EmptyValueMessage="Time is required"
                                                    InvalidValueMessage="Time is invalid" Display="none" TooltipMessage="Input a time"
                                                    EmptyValueBlurredText="" InvalidValueBlurredMessage="*"  />
                                                 <TimeSectionProperties Visible="true">
            <TimeEditProperties DisplayFormatString="HH:mm" EditFormatString="HH:mm">
            </TimeEditProperties>
        </TimeSectionProperties>
                                                <asp:RequiredFieldValidator ID="rvTestDuration" runat="server" ControlToValidate="txtduration" Display="none"
                                                    ErrorMessage="Please Enter Duration in Hrs" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                                <%--  <asp:TextBox ID="txtduration" runat="server" AutoComplete="off" CssClass="form-control"  TabIndex="5" type="Time" 
                                            ToolTip="Please Enter Duration in Hrs"  />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtduration" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Enter Duration in Hrs"  ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                --%>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Description of the event </label>
                                                </div>
                                                <asp:TextBox ID="txtdescription" runat="server" AutoComplete="off" CssClass="form-control" TabIndex="8" TextMode="MultiLine"
                                                    ToolTip="Please Enter Description of the event" MaxLength="200" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtdescription" SetFocusOnError="true"
                                                    Display="None" ErrorMessage="Please Enter Description of the event " ValidationGroup="submit"></asp:RequiredFieldValidator>


                                            </div>

                                            <div class="form-group col-lg-5 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Upload Certificate  </label>
                                                </div>

                                                <div id="Div2" class="logoContainer" runat="server">
                                                    <img src="../../Images/default-fileupload.png" alt="upload image" runat="server" id="imgUpFile" />

                                                    <%--<img src="../Images/default-fileupload.png" alt="upload image" runat="server" id="imgUpFile" />--%>
                                                    <%--<img src="../../Images/default-fileupload.png" alt="upload image" runat="server" id="imgUpFile" />--%>
                                                </div>
                                                <div class="fileContainer sprite pl-1">
                                                    <span runat="server" id="ufFile"
                                                        cssclass="form-control" tabindex="9">Upload File</span>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Select file to upload" CssClass="form-control"  />
                                                    <p style="color: red">Note: Upload PDF File and File Size should be 5 MB.</p>
                                                    <%-- <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Select file to upload"
                                        CssClass="form-control" onkeypress="" />--%>
                                                </div>

                                            </div>



                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                    TabIndex="10" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnReport" runat="server" Text="Report"
                                    TabIndex="11" CssClass="btn btn-info" Style="display: none;" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    TabIndex="11" CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlclubReg" runat="server">
                                    <asp:ListView ID="lvclubReg" runat="server">
                                      <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>SrNo
                                                        </th>
                                                        <th>Action</th>
                                                        <th>Session
                                                        </th>
                                                        <th>Club
                                                        </th>
                                                        <%--<th style="display:none">
                                                            Club
                                                        </th>--%>
                                                        <th>Title of Event
                                                        </th>
                                                        <th>Venue
                                                        </th>
                                                        <th>FromDate
                                                        </th>
                                                        <th>ToDate
                                                        </th>
                                                        <th>Duration
                                                        </th>
                                                        <th>Description of Event
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
                                                <%--   <td style="text-align: center;">
                                                     <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("CLUB_NO")%>'
                                                                    AlternateText="Edit Record" ToolTip="Edit Record"/>
                                                    <%--<asp:ImageButton ID="ImageButton1" runat="server"  CommandArgument='<%# Eval("CLUBNO")%>'
                                                    ImageUrl="~/images/edit.gif" />--%>

                                                <%--</td>--%>
                                                <td>
                                                    <%# Container.DataItemIndex + 1%>
                                                </td>
                                                <td style="text-align: center;">
                                                    <%--asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("CLUB_NO")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click1"/>
                                                   --%>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("CLUB_NO")%>'
                                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" CausesValidation="false"/>
                                                </td>
                                                <td><%# Eval("SESSION_NAME") %></td>
                                                <td>
                                                    <%# Eval("CLUB_ACTIVITY_TYPE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("TITLE_OF_EVENT") %>
                                                </td>
                                                <td>
                                                    <%# Eval("VENUE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("FROMDATE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TODATE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DURATION")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DESCRIPTION_OF_EVENT")%>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="ddlsubuser" />
            
        </Triggers>
    </asp:UpdatePanel>
    <script>
        function comparedate() {
            var fromdate = document.getElementById("id_fromdate");
            var todate = document.getElementById("id_todate");
            if (fromdate < todate) {
                alert("Start date should be less than end date");
                return false;
            }
        }
    </script>
    <script>
        $(document).ready(function () {
            $(document).on("click", ".logoContainer", function () {
                $("#ctl00_ContentPlaceHolder1_FileUpload1").click();
            });
            $(document).on("keydown", ".logoContainer", function () {
                if (event.keyCode === 13) {
                    // Cancel the default action, if needed
                    event.preventDefault();
                    // Trigger the button element with a click
                    $("#ctl00_ContentPlaceHolder1_FileUpload1").click();
                }
            });
        });
    </script>

    <script type="text/javascript">
        function Focus() {
            //  alert("hii");
            document.getElementById("<%=imgUpFile.ClientID%>").focus();
         }
    </script>

    <%--    <script>
        $("input:file").change(function () {
            //$('.fuCollegeLogo').on('change', function () {

            var maxFileSize = 1000000;
            var fi = document.getElementById('ctl00_ContentPlaceHolder1_FileUpload1');
            myfile = $(this).val();
            var ext = myfile.split('.').pop();
            var res = ext.toUpperCase();
            if (res != "PDF" || res != "jpg") {
                alert("Please Select pdf and jpg  File Only.");
                $('.logoContainer img').attr('src', "../../Images/default-fileupload.png");
                $(this).val('');
            }

            for (var i = 0; i <= fi.files.length - 1; i++) {
                var fsize = fi.files.item(i).size;
                if (fsize >= maxFileSize) {
                    alert("File Size should be less than 1 MB");
                    $('.logoContainer img').attr('src', "../../Images/default-fileupload.png");
                    $("#ctl00_ContentPlaceHolder1_FileUpload1").val("");

                }
            }

        });
    </script>--%>
<%--    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });


    </script>--%>


    <script>
        function checkout() {
            var checkBoxes = document.getElementsByClassName('ddlsubuser');
            var nbChecked = 0;
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].checked) {
                    nbChecked++;
                };
            };
            if (nbChecked > 3) {
                alert('Please Select Maximun 3 Club.');
                return false;
            } else if (nbChecked == 0) {
                alert('Please, select at least one Club!');
                return false;
            } else {
                //Do what you need for form submission, if needed...
            }
        }
    </script>
    <script>
        $("input:file").change(function () {
            var fileName = $(this).val();

            newText = fileName.replace(/fakepath/g, '');
            var newtext1 = newText.replace(/C:/, '');
            //newtext2 = newtext1.replace('//', ''); 
            var result = newtext1.substring(2, newtext1.length);


            if (result.length > 0) {
                $(this).parent().children('span').html(result);
            }
            else {
                $(this).parent().children('span').html("Choose file");
            }
        });
        //file input preview
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('.logoContainer img').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        $("input:file").change(function () {
            readURL(this);
        });
    </script>
    <script>
        function getVal() {
            var array = []
            var nbChecked = 0;
            var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

            for (var i = 0; i < checkboxes.length; i++) {
                //array.push(checkboxes[i].value)    
                if (nbChecked == undefined) {
                    nbChecked = checkboxes[i].value + ',';
                }
                else {
                    nbChecked = nbChecked + 1;
                    //semNo += checkboxes[i].value + ',';
                }
            }
            //alert(semNo);


            if (nbChecked == 0) {
                alert('Please select atleast one Club.')
                return false
            }
            else if (nbChecked > 3) {

                alert('Please select Maximum Three Club.');
                //checkboxes.length[j].selected = false;
                return false
            }


            //document.getElementById(inpHide).value = "degreeNo";
        }

    </script>
    <script>
        function validateTime(x) {

            if (x.value.length == 5) {

                //var newreg = /^[0-2][0-3]:[0-5][0-9]$/;
                var newreg = /^(([0-1][0-9])|(2[0-3])):[0-5][0-9]$/;

                var first = x.value.split(":")[0];
                var second = x.value.split(":")[1];

                if (first > 24 || second > 59) {
                    alert("Invalid time format\n\n The valid format is hh:mm");
                    document.getElementById('txtTimeSpent').focus();
                }

            }
        }
    </script>

</asp:Content>
