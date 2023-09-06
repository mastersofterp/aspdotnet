<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TimeSlot.aspx.cs" Inherits="ACADEMIC_MASTERS_TimeSlot" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_pnlTimeTable .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        .switch .time_chk {
            height: 30px;
            width: 50px;
            background: #f00;
            color: #fff;
        }

            .switch .time_chk:after {
                content: '';
                position: absolute;
                top: 1.5px;
                left: 3.7px;
                width: 7.2px;
                height: 26.5px;
                background: #fff;
            }

        .switch label:before {
            font-size: 14px;
        }

        .switch input:checked + .time_chk:before {
            content: attr(data-on);
            position: absolute;
            left: 0;
            font-size: 14px;
            padding: 3px 10px;
            font-weight: 400;
            color: #fff;
            transition: 0.35s;
        }

        .switch input:checked + .time_chk:after {
            transform: translateX(34px);
        }
    </style>
    <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl"
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

    <%--  Shrink the info panel out of view --%>
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--  <h3 class="box-title">Time Slot Master Entry</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlQualification" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Slot Name </label>
                                            </div>
                                            <asp:TextBox ID="txtslotname" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSlotName" runat="server" TabIndex="1"
                                                ControlToValidate="txtslotname" Display="None" SetFocusOnError="true"
                                                ErrorMessage="Please Enter Slot Name" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>

<%--                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Time From </label>
                                            </div>
                                            <asp:TextBox ID="txtfrom" runat="server" MaxLength="5"
                                                TabIndex="2" ToolTip="Please Enter Time From" CssClass="form-control">
                                            </asp:TextBox>
                                            <asp:CheckBox ID="chkFrom" runat="server" AutoPostBack="True" Checked="true" OnCheckedChanged="chkFrom_CheckedChanged"
                                                Text="AM" ToolTip="Unchecked for PM" />
                                            <label style="color: green">( Unchecked for PM ) </label>

                                            <asp:RequiredFieldValidator ID="rfvTimeFrom" runat="server"
                                                ControlToValidate="txtfrom" Display="None" SetFocusOnError="true"
                                                ErrorMessage="Please Enter Time from" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,./;<>?'{}[]\|-&&quot;qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM'" TargetControlID="txtfrom" />

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Time To </label>
                                            </div>
                                            <asp:TextBox ID="txtTo" runat="server" Style="text-align: left" MaxLength="5"
                                                TabIndex="3" ToolTip="Please Enter Time To"
                                                CssClass="form-control">  
                                            </asp:TextBox>
                                            <asp:CheckBox ID="chkTo" runat="server" AutoPostBack="True" Checked="true" OnCheckedChanged="chkTo_CheckedChanged"
                                                Text="AM" ToolTip="Unchecked for PM" />
                                            <label style="color: green">( Unchecked for PM) </label>

                                            <asp:RequiredFieldValidator ID="rfvTimeTo" runat="server"
                                                ControlToValidate="txtTo" Display="None" SetFocusOnError="true"
                                                ErrorMessage="Please Enter Time To" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server"
                                                FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,./;<>?'{}[]\|-&&quot;qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM'" TargetControlID="txtTo" />
                                        </div>--%>
                                          <asp:Panel ID="PnlDateTime" runat="server">
                                          <div class="col-12">
                                         <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <label><span style="color: red">*</span>Time From</label>

                                            <asp:TextBox ID="txtfrom" runat="server" CssClass="form-control" ToolTip="Please Enter Time" TabIndex="4" AutoComplete="off"></asp:TextBox>
                                            <ajaxToolKit:MaskedEditExtender ID="meStarttime" runat="server" TargetControlID="txtfrom"
                                                Mask="99:99" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" AcceptAMPM="true"
                                                MaskType="Time"/>
                                             <asp:RequiredFieldValidator ID="rfvTimeFrom" runat="server"
                                                ControlToValidate="txtfrom" Display="None" SetFocusOnError="true"
                                                ErrorMessage="Please Enter Time from" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid Time From"
                                                 ControlToValidate="txtfrom" Display="NONE" SetFocusOnError="true" ValidationGroup="submit" 
                                                ValidationExpression= "((1[0-2]|0?[0-9]):([0-5][0-9]) ?([AaPp][Mm]))"></asp:RegularExpressionValidator>
                                        
                                        </div>

                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <label><span style="color: red">*</span>Time To</label>

                                            <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" ToolTip="Please Enter Time" TabIndex="4" AutoComplete="off"></asp:TextBox>
                                            <ajaxToolKit:MaskedEditExtender ID="meEndtime" runat="server" TargetControlID="txtTo"
                                                Mask="99:99" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" AcceptAMPM="true"
                                                MaskType="Time"/>
                                              <asp:RequiredFieldValidator ID="rfvTimeTo" runat="server"
                                                ControlToValidate="txtTo" Display="None" SetFocusOnError="true"
                                                ErrorMessage="Please Enter Time To" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter a valid Time To"
                                                 ControlToValidate="txtTo" Display="NONE" SetFocusOnError="true" ValidationGroup="submit" 
                                                ValidationExpression= "((1[0-2]|0?[0-9]):([0-5][0-9]) ?([AaPp][Mm]))"></asp:RegularExpressionValidator>
                                       
                                        </div>
                                             </div>
                                              </div>
                                        </asp:Panel>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Slot Type </label>--%>
                                                <asp:Label ID="lblDYddlSlotType" runat="server" Font-Bold="true"></asp:Label>

                                            </div>
                                            <asp:DropDownList ID="ddlSlotType" runat="server" AppendDataBoundItems="true" TabIndex="4" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                ControlToValidate="ddlSlotType" Display="None" SetFocusOnError="true"
                                                ErrorMessage="Please Select Slot Type." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>School/Institute Name</label>--%>
                                                <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <%--<asp:DropDownList ID="ddlIstitute" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                AutoPostBack="true" CssClass="form-control" TabIndex="1" OnSelectedIndexChanged="ddlIstitute_SelectedIndexChanged">
                                                <asp:ListItem>Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="ddlIstitute" Display="None" SetFocusOnError="true"
                                                ErrorMessage="Please Select School/Institute Name." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            --%>
                                            <div class="checkbox-list-box">
                                                <asp:CheckBox ID="chkIstitute" runat="server" Text="All Institute" AutoPostBack="true" onClick="SelectAllInstitute()" CssClass="select-all-checkbox" TabIndex="5" />
                                                <asp:CheckBoxList ID="chkIstitutelist" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chkIstitutelist_SelectedIndexChanged"
                                                    RepeatColumns="1" RepeatDirection="Horizontal">
                                                </asp:CheckBoxList>
                                            </div>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Degree </label>--%>
                                                <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <%--<asp:DropDownList ID="ddldegree" runat="server" AppendDataBoundItems="true" TabIndex="2" data-select2-enable="true"
                                                AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddldegree_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server"
                                                ControlToValidate="ddlDegree" Display="None" SetFocusOnError="true"
                                                ErrorMessage="Please Select Degree." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>

                                            <div class="checkbox-list-box">
                                                <asp:CheckBox ID="chkDegree" runat="server" Text="All Degree" AutoPostBack="true" onClick="SelectAllDegree()" CssClass="select-all-checkbox" TabIndex="6" />
                                                <asp:CheckBoxList ID="chkDegreeList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chkDegreeList_SelectedIndexChanged"
                                                    RepeatColumns="1" RepeatDirection="Horizontal">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Sequence No. </label>
                                            </div>
                                            <asp:TextBox ID="txtSequenceNo" runat="server" Style="text-align: left" MaxLength="32000" TabIndex="7" ToolTip="Please Enter Sequence No."
                                                CssClass="form-control">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSequenceNo" runat="server"
                                                ControlToValidate="txtSequenceNo" Display="None" SetFocusOnError="true"
                                                ErrorMessage="Please Enter Sequence No." InitialValue="" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeSequenceNo" runat="server"
                                                TargetControlID="txtSequenceNo"
                                                FilterType="Custom,Numbers"
                                                FilterMode="ValidChars"
                                                ValidChars="">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Active Status</label>
                                            </div>
                                            <div class="switch form-inline">
                                                <input type="checkbox" id="rdActive" name="switch" checked />
                                                <label data-on="Active" tabindex="7" class="newAddNew Tab" data-off="Inactive" for="rdActive"></label>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <asp:HiddenField ID="hdnoldslotno" runat="server" />
                                            <asp:HiddenField ID="hdnidno" runat="server" />
                                            <asp:HiddenField ID="hdnStatus" runat="server" />
                                            <asp:HiddenField ID="hfFromAM_PM" runat="server" />
                                            <asp:HiddenField ID="hfToAM_PM" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnsubmit" runat="server" OnClick="btnsubmit_Click" Text="Submit" OnClientClick="return validate();"
                                        CssClass="btn btn-primary" ValidationGroup="submit" TabIndex="8" />

                                    <asp:Button ID="btncancel" runat="server" CausesValidation="False" OnClick="btncancel_Click"
                                        Text="Cancel" CssClass="btn btn-warning" TabIndex="9" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="false"
                                        ValidationGroup="submit" />
                                </div>
                            </asp:Panel>

                            <div class="col-12 table table-responsive">
                                <asp:Panel ID="pnlTimeTable" runat="server">
                                    <div class="sub-heading">
                                        <h5>Time Slot Entry</h5>
                                    </div>
                                    <asp:ListView ID="lvTimeTable" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%;" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center;">Edit
                                                        </th>
                                                        <%-- <th>Session
                                                        </th>--%>
                                                        <th>
                                                            <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>Slot Name
                                                        </th>
                                                        <th>Slot Type
                                                        </th>
                                                        <th>Time From
                                                        </th>
                                                        <th>Time To
                                                        </th>
                                                        <th>Sequence No.
                                                        </th>
                                                        <th>Status
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
                                                <td style="text-align: center;">
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" OnClick="btnEdit_Click"
                                                        CommandArgument='<%# Eval("IDNO")%>' ImageUrl="~/Images/edit.png" ToolTip="Edit Record" />
                                                    <asp:HiddenField ID="hdnsession" runat="server" Value='<%#Eval("SESSIONNO") %>' />
                                                </td>
                                                <%-- <td style="text-align:center;">
                                                           <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("IDNO") %>'
                                                            ToolTip="Delete Record" OnClientClick="return confirm('Yor are deleting record permanently. \r\n Are you sure ?');"
                                                            OnClick="btnDelete_Click" />
                                                    </td>--%>
                                                <td>
                                                    <asp:Label ID="lbldegree" runat="server" Text='<%# Eval("DEGREE") %>' ToolTip='<%# Eval("SLOTTYPE") %>' />
                                                    <asp:HiddenField ID="hdnDegreeNo" runat="server" Value='<%#("DEGREENO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblslotname" runat="server" Text='<%# Eval("SLOTNAME") %>' />
                                                    <asp:HiddenField ID="hdnSlotNo" runat="server" Value='<%#("IDNO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSlotType" runat="server" Text='<%# Eval("SLOTTYPE_NAME") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbltimefrom" runat="server" Text='<%#Eval("TIMEFROM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbltimeto" runat="server" Text='<%#Eval("TIMETO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSequenceNo" runat="server" Text='<%#Eval("SEQUENCENO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# (Convert.ToString(Eval("ACTIVESTATUS"))=="1" ? "Active": "Inactive")%>'></asp:Label>
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
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>
    <script type="text/javascript" language="javascript">
        function validateNumeric(txt)
        {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }

        function changetimeFromAMtoPM() {
            var time = $('#ctl00_ContentPlaceHolder1_txtfrom').val();
            time = time.split(':')[0];
            if (time >= 12) {
                $('#ctl00_ContentPlaceHolder1_chkFromAM_PM').prop('checked', false); // SetTimeFrom('false');
                $('#hfFromAM_PM').val('false');
            }
            else {
                $('#ctl00_ContentPlaceHolder1_chkFromAM_PM').prop('checked', true); //
                $('#hfFromAM_PM').val('true');
            }
        }

        function changetimetoAMtoPM() {
            var time = $('#ctl00_ContentPlaceHolder1_txtTo').val();
            time = time.split(':')[0];
            if (time >= 12) {
                $('#ctl00_ContentPlaceHolder1_chkToAM_PM').prop('checked', false);
                $('#hfToAM_PM').val('false');
            }
            else {
                $('#ctl00_ContentPlaceHolder1_chkToAM_PM').prop('checked', true);
                $('#hfToAM_PM').val('true');
            }
        }
    </script>

    <script>
        function SetStat(val) {
            $('#rdActive').prop('checked', val);
        }

        function validate() {
            if (Page_ClientValidate()) {
                $('#hfdStat').val($('#rdActive').prop('checked'));
                //if ($('#ctl00_ContentPlaceHolder1_chkFromAM_PM').prop('checked') == true)
                //    $('#hfFromAM_PM').val('true');
                //else
                //    $('#hfFromAM_PM').val('false');

                //if ($('#chkToAM_PM').prop('checked') == true)
                //    $('#hfToAM_PM').val('true');
                //else
                //    $('#hfToAM_PM').val('false');

                //$('#hfFromAM_PM').val($('#chkFromAM_PM').prop('checked'));
                //$('#hfToAM_PM').val($('#chkToAM_PM').prop('checked'));
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnsubmit').click(function () {
                    validate();
                });
            });
        });
    </script>
    <script> //add by maithili [29-08-2022]
        function SelectAllDegree() {
            debugger;
            var CHK = document.getElementById("<%=chkDegreeList.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");

            var chkDeg = document.getElementById('ctl00_ContentPlaceHolder1_chkDegree');

            for (var i = 0; i < checkbox.length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_chkDegreeList_' + i);
                if (chkDeg.checked == true) {
                    chk.checked = true;
                }
                else {
                    chk.checked = false;
                }
            }
        }


        function SelectAllInstitute() {
            debugger;

            var CHK = document.getElementById("<%=chkIstitutelist.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");

            var chkDeg = document.getElementById('ctl00_ContentPlaceHolder1_chkIstitute');

            for (var i = 0; i < checkbox.length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_chkIstitutelist_' + i);
                if (chkDeg.checked == true) {
                    chk.checked = true;
                }
                else {
                    chk.checked = false;
                }
            }
        }
    </script>

    <script type="text/javascript">
        function SetTimeFrom(val) {
            debugger;
            $('#chkFromAM_PM').prop('checked', val);
        }
        function SetTimeTo(val) {
            debugger;
            $('#chkToAM_PM').prop('checked', val);
        }
    </script>


</asp:Content>

