<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ViewRoomStatusAndAllotment.aspx.cs" Inherits="HOSTEL_ViewRoomStatusAndAllotment"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .close {
            opacity: 1;
            color: #ff4d4d;
            padding: 4px 10px;
        }
    </style>

    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

        .modalPopup {
            background-color: #FFFFFF;
            width: 70%;
            min-height: 400px;
            /*border: 3px solid #0DA9D0;*/
            /*border-radius: 12px;*/
            padding: 0;
        }

            .modalPopup .header {
                background-color: #cee8f6;
                height: 30px;
                color: #333;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
            }

            .modalPopup .body {
                min-height: 35px;
            }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary" runat="server" id="Mainpnl">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">STUDENT ALLOT ROOM</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel Session</label>
                                </div>
                                <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" TabIndex="1" AppendDataBoundItems="True"
                                    Enabled="false" data-select2-enable="true">
                                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="valSession" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" ValidationGroup="submit"
                                    SetFocusOnError="True" InitialValue="0" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel </label>
                                </div>
                                <asp:DropDownList ID="ddlHostel" runat="server" CssClass="form-control" TabIndex="2" AppendDataBoundItems="true"
                                    data-select2-enable="true"  AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlHostel_SelectedIndexChanged" />
                                <%--    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                </asp:DropDownList>--%>

                                <asp:RequiredFieldValidator ID="valHostel" runat="server" ControlToValidate="ddlHostel"
                                    Display="None" ErrorMessage="Please Select Hostel" ValidationGroup="submit" SetFocusOnError="True"
                                    InitialValue="0" />
                            </div>

                           
                            <div class="form-group col-lg-3 col-md-6 col-12" id="plnAdmin" runat="server" >
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Degree </label>
                                </div>
                                <asp:DropDownList ID="ddlDeg" runat="server" CssClass="form-control" TabIndex="3" AppendDataBoundItems="true" data-select2-enable="true">
                                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                </asp:DropDownList>
                                <%-- <asp:RequiredFieldValidator ID="valDegree2" runat="server" ControlToValidate="ddlDeg"
                            Display="None" ErrorMessage="Please Select Degree" ValidationGroup="submit" SetFocusOnError="True"
                            InitialValue="0" />--%>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12" id="plnAdmin1" runat="server">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Student Semester In </label>
                                </div>
                                <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" TabIndex="4" AppendDataBoundItems="true" data-select2-enable="true">
                                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="Semester" runat="server" ControlToValidate="ddlSemester"
                            Display="None" ErrorMessage="Please Select Semester" ValidationGroup="submit" SetFocusOnError="True"
                            InitialValue="0" />--%>
                            </div>
                               

                            <div class="form-group col-lg-3 col-md-6 col-12" id="plnStudent" runat="server">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Block </label>
                                </div>
                                <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control" TabIndex="5" AppendDataBoundItems="true" data-select2-enable="true">
                                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="vlBlock" runat="server" ControlToValidate="ddlBlock"
                                    Display="None" ErrorMessage="Please Select Block" ValidationGroup="submit" SetFocusOnError="True"
                                    InitialValue="0" />

                                <%--AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"--%>
                                <%--<asp:RequiredFieldValidator ID="valBlock" runat="server" ControlToValidate="ddlBlock"
                                Display="None" ErrorMessage="Please select Block." ValidationGroup="submit" SetFocusOnError="True"
                                InitialValue="0" />--%>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12" id="plnStudent1" runat="server">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Create Demand For Semester </label>
                                </div>
                                <asp:DropDownList ID="ddlDemandSem" runat="server" CssClass="form-control" TabIndex="5" AppendDataBoundItems="true" data-select2-enable="true">
                                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="Semester" runat="server" ControlToValidate="ddlDemandSem"
                                    Display="None" ErrorMessage="Please Select Semester for Demand" ValidationGroup="submit" SetFocusOnError="True"
                                    InitialValue="0" />
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:HiddenField ID="HiddenRoomno" runat="server" />
                        <asp:HiddenField ID="HiddenVacancy" runat="server" />
                        <asp:HiddenField ID="HiddenRoomtypeno" runat="server" />
                        <asp:Label ID="lblRoomAllot" runat="server" Font-Bold="True" Style="color: red; text-align: center"></asp:Label>

                        <asp:Button ID="btnShow" runat="server" Text="Show Rooms" 
                            OnClick="btnShow_Click" TabIndex="9" CssClass="btn btn-primary" />  <%--ValidationGroup="submit"--%>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                            OnClick="btnCancel_Click" TabIndex="10" CssClass="btn btn-warning" />
                        <!--don't make this button (btnPop) visible true. and also don't delete it other wise modal pop up will not work properly-->
                        <asp:Button ID="btnPop" runat="server" Visible="false" />

                        <asp:ValidationSummary ID="valSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="submit" />
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlRoomsTable" runat="server" ScrollBars="Auto" Visible="false" Height="300px">
                        </asp:Panel>
                        <div id="divMsg" runat="server"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <ajaxToolKit:ModalPopupExtender ID="mpe" BehaviorID="mdlPopupAllot" runat="server"
        PopupControlID="pnlPopup" TargetControlID="HiddenRoomno" BackgroundCssClass="modalBackground" CancelControlID="imbClose" OnCancelScript="cancelCloseClick();">
        <%--CancelControlID="imbClose" OnCancelScript="cancelCloseClick();"--%>
    </ajaxToolKit:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: block">
        <div class="header">
            <asp:Label ID="lblPopHeader" runat="server"></asp:Label>
            <asp:ImageButton ID="imbClose" runat="server" OnClientClick="ClosePopUp(this.id);" class="close" ImageUrl="~/Images/cancel.gif" />
        </div>

        <asp:UpdatePanel ID="udpInnerUpdatePanel" runat="Server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <div class="col-md-12" style="height:500px; overflow-x:scroll;">
                    <div class="row">

                        <div class="col-md-12" style="font-weight: bold; margin-bottom: 10px;">
                            Total Seats :
                                        <asp:Label ID="lblCapacity" runat="server"></asp:Label>
                            <asp:HiddenField ID="hidCapacity" runat="server" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                Available Seats :
                                        <asp:Label ID="lblVacancy" runat="server"></asp:Label>
                            <asp:HiddenField ID="hidVacancy" runat="server" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lkbtnStudInfo" runat="server" OnClick="lkbtnStudInfo_Click">Student Present In Room Info</asp:LinkButton>
                        </div>

                        <div class="col-md-12" id="divAdmin" runat="server">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>RRN. No.</label>
                                    </div>

                                    <asp:TextBox ID="txtSearchStud" runat="server" MaxLength="15" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>--- OR ---</label>
                                    </div>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Degree</label>
                                    </div>

                                    <asp:DropDownList ID="ddlDegree" AppendDataBoundItems="true" runat="server" CssClass="form-control"
                                        onchange="ddlChange(this.id);">
                                        <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    <%--onchange="ddlChange(this.id);"--%>
                                    <%--OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"--%>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Branch</label>
                                    </div>

                                    <asp:DropDownList ID="ddlBranch" AppendDataBoundItems="true" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Year</label>
                                    </div>

                                    <asp:DropDownList ID="ddlYear" AppendDataBoundItems="true" runat="server" CssClass="form-control"
                                        onchange="ddlChange(this.id);">
                                        <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    <%--OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" AutoPostBack="true"--%>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Semester</label>
                                    </div>

                                    <asp:DropDownList ID="ddlSem" AppendDataBoundItems="true" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                         </div>

                            <div id="divStudent" runat="server" visible="false">

                                <div class="col-12">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>RRN. No.</label>
                                        </div>
                                        <asp:TextBox ID="txtStudREGNO" runat="server" MaxLength="15" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudName" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>College Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudClg" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Degree :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudDegree" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Branch :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudBranch" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnPay" runat="server" class="btn btn-primary" Text="Pay Now" OnClick="btnPay_Click" />
                                    </div>

                                </div>
                                <%----Below List view(ListAllotedStudent) added by Saurabh L on 23 Feb 2023 Purpose: To show selected Room Alloted student Details show-----%>
                                <div class="col-12">
                            <asp:ListView ID="ListAllotedStudent" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Room Alloted Student List</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap" style="width: 100% " id="myTable">
                                        <thead class="bg-light-blue" bgcolor="blue">
                                            <tr>
                                                <th>RRN No.</th>
                                                <th>Student Name</th>
                                                <th>Degree</th>
                                                <th>Branch</th>
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
                                            <%# Eval("REGNO")%>                                           
                                        </td>
                                        <td>
                                             <%# Eval("STUDNAME") %>
                                        </td>
                                        <td>
                                            <%# Eval("DEGREENAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("BRANCH")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyItemTemplate>
                                    -- Record Not Found --
                                </EmptyItemTemplate>
                            </asp:ListView>

                        </div>

                                  <div class="text-center mt-3">
                                <asp:Label ID="lblAllotStudInfo" runat="server" Font-Bold="True" Style="color: red; text-align: center"></asp:Label>
                            </div>
                            </div>
                        

                        <div class="col-12 btn-footer" runat="server" id="btnBlock">

                            <input id="btnSearchStud" type="button" class="btn btn-primary" value="Search Student" onclick="SubmitSearch(this.id);" />

                            <input id="btnAllotRoom" class="btn btn-primary" type="button" value="Allot Room" onclick="ValidateSubmitRecord(this.id);" causesvalidation="false" />
                            <input id="btnClearModalSearch" type="button" class="btn btn-warning" value="Clear Search" onclick="ClearSearch(this.id);" />
                            <div class="text-center mt-3">
                                <asp:Label ID="lblStudSearchStatus" runat="server" Font-Bold="True" Style="color: red; text-align: center"></asp:Label>
                            </div>
                        </div>

                        <div class="col-12">
                     <asp:Label ID="lblDisciplinary" runat="server" Visible="false" Font-Bold="True" Style="color: red; text-align: center"></asp:Label>
                        </div>

                        <div class="col-12">
                            <asp:ListView ID="lvStudent" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Search Results</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap" style="width: 100% " id="myTable">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Select</th>
                                                <th>Name</th>
                                                <th>Reg.No</th>
                                                <th>Identity.No</th>
                                                <th>Branch</th>
                                                <th>Semester</th>
                                                <th>Hostel Fee</th> 
                                            </tr>
                                        </thead>
                                        <tbody id="tblSearchResults">
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                        <tbody>
                                            <tr id="Tr1" runat="server" />
                                        </tbody>
                                    </table>
                                    <%--<div class="listview-container">
                                                <div id="demo-grid" class="vista-grid">
                                                    <table id="tblSearchResults" class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>--%>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkSelect" runat="server" ToolTip="Click to select student for allotment"
                                                ClientIDMode="Static" class="eachLoop" CausesValidation="false" />
                                            <asp:HiddenField ID="hdnIdno" runat="server" Value='<%# Eval("IDNO")%>' />
                                        </td>
                                        <td>
                                            <%# Eval("NAME") %>
                                        </td>
                                        <td>
                                            <%# Eval("REGNO")%>
                                        </td>
                                        <td>
                                            <%# Eval("IDNO")%>
                                        </td>
                                        <td>
                                            <%# Eval("SHORTNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("SEMESTERNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("AMOUNT")%>   
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyItemTemplate>
                                    -- Record Not Found --
                                </EmptyItemTemplate>
                            </asp:ListView>

                        </div>

                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnPay" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

    <%-- edit for Kota to Show the room Name and std_fee--%>
    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showRoomAllotPopup(source) {
            try {
                var str = source.id;
                var resCapacity = str.replace("btnRow", "hdnCapRow");
                var capacity = document.getElementById(resCapacity).value;

                var resVacant = str.replace("btnRow", "hdnVacRow");
                var vacant = document.getElementById(resVacant).value;

                var resRoomName = str.replace("btnRow", "hdnRoomNameRow");
                var roomName = document.getElementById(resRoomName).value;

                var resRoomNo = str.replace("btnRow", "hdnRoomNoRow");
                var roomNo = document.getElementById(resRoomNo).value;

                var resBlockName = str.replace("btnRow", "hdnBlockNameRow");
                var blockName = document.getElementById(resBlockName).value;

                var resBlockNo = str.replace("btnRow", "hdnBlockNoRow");
                var blockNo = document.getElementById(resBlockNo).value;

                var resRoomtypeno = str.replace("btnRow", "hdnRoomTypenoRow");
                var Roomtypeno = document.getElementById(resRoomtypeno).value;

                if (vacant > 0) {
                    document.getElementById('<%= lblPopHeader.ClientID %>').innerHTML =
                                            'Allot Room for ' + document.getElementById('<%= ddlHostel.ClientID %>').options[document.getElementById('<%= ddlHostel.ClientID %>').selectedIndex].text + ', ' +
                                           blockName + ' (Room-' + roomName + ')';

                    document.getElementById('<%= lblCapacity.ClientID %>').innerHTML = capacity;
                    document.getElementById('<%= lblVacancy.ClientID %>').innerHTML = vacant;

                    document.getElementById('<%= hidCapacity.ClientID %>').value = capacity;
                    document.getElementById('<%= hidVacancy.ClientID %>').value = vacant;


                    document.getElementById('<%= HiddenVacancy.ClientID %>').value = vacant;
                    document.getElementById('<%= HiddenRoomno.ClientID %>').value = roomNo;


                     document.getElementById('<%= HiddenRoomtypeno.ClientID %>').value = Roomtypeno;

                    this._source = source;
                    this._popup = $find('mdlPopupAllot');
                    //  find the confirm ModalPopup and show it
                    this._popup.show();
                }
                else {
                    alert('Their is no vacancy in this room, all seats are alloted.');
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelCloseClick() {
            //  find the confirm ModalPopup and hide it
            this._popup.hide();
            //$jQfind('mdlPopupAllot_backgroundElement').hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
            __doPostBack('ClosePopUp');
        }
        function ClosePopUp() {
            //  find the confirm ModalPopup and hide it
            this._popup.hide();
            //$jQfind('mdlPopupAllot_backgroundElement').hide();
            //  clear the event source
           
            this._source = null;
            this._popup = null;
            __doPostBack('ClosePopUp');
        }
        function ClosePopUp(ClosePopUp) {
            try {
                __doPostBack(ClosePopUp);
            }
            catch (e) {
                alert("Error: " + e.description);
            }

            return;
        }
    </script>

    <%-- edit for Kota to Show the room Name and std_fee--%>

   <%-- <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showRoomAllotPopup(source) {
            try {
                var str = source.id;
                var resCapacity = str.replace("btnRow", "hdnCapRow");
                var capacity = document.getElementById(resCapacity).value;

                var resVacant = str.replace("btnRow", "hdnVacRow");
                var vacant = document.getElementById(resVacant).value;

                var resRoomName = str.replace("btnRow", "hdnRoomNameRow");
                var roomName = document.getElementById(resRoomName).value;

                var resRoomNo = str.replace("btnRow", "hdnRoomNoRow");
                var roomNo = document.getElementById(resRoomNo).value;

                var resBlockName = str.replace("btnRow", "hdnBlockNameRow");
                var blockName = document.getElementById(resBlockName).value;

                var resBlockNo = str.replace("btnRow", "hdnBlockNoRow");
                var blockNo = document.getElementById(resBlockNo).value;

                var resRoomtypeno = str.replace("btnRow", "hdnRoomTypenoRow");
                var Roomtypeno = document.getElementById(resRoomtypeno).value;

                if (vacant > 0) {
                    document.getElementById('<%= lblPopHeader.ClientID %>').innerHTML =
                                            'Allot Room for ' + document.getElementById('<%= ddlHostel.ClientID %>').options[document.getElementById('<%= ddlHostel.ClientID %>').selectedIndex].text + ', ' +
                                           blockName + ' (Room-' + roomName + ')';

                    document.getElementById('<%= lblCapacity.ClientID %>').innerHTML = capacity;
                    document.getElementById('<%= lblVacancy.ClientID %>').innerHTML = vacant;

                    document.getElementById('<%= hidCapacity.ClientID %>').value = capacity;
                    document.getElementById('<%= hidVacancy.ClientID %>').value = vacant;


                    document.getElementById('<%= HiddenVacancy.ClientID %>').value = vacant;
                    document.getElementById('<%= HiddenRoomno.ClientID %>').value = roomNo;


                    this._source = source;
                    this._popup = $find('mdlPopupAllot');
                    //  find the confirm ModalPopup and show it
                    this._popup.show();
                }
                else {
                    alert('Their is no vacancy in this room, all seats are alloted.');
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelCloseClick() {
            //  find the confirm ModalPopup and hide it
            this._popup.hide();
            //$jQfind('mdlPopupAllot_backgroundElement').hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
            __doPostBack('ClosePopUp');
        }
        function ClosePopUp() {
            //  find the confirm ModalPopup and hide it
            this._popup.hide();
            //$jQfind('mdlPopupAllot_backgroundElement').hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
            __doPostBack('ClosePopUp');
        }
        function ClosePopUp(ClosePopUp) {
            try {
                __doPostBack(ClosePopUp);
            }
            catch (e) {
                alert("Error: " + e.description);
            }

            return;
        }
    </script>--%>

    <script type="text/javascript">

        //var $jQ = jQuery.noConflict();
        function ValidateSubmitRecord(btnAllotRoom) {
            debugger;
            var totvacant = 0;
            var selectedStudent = 0;
            var totalStudent = 0;

            var dataRows = document.getElementById('tblSearchResults').getElementsByTagName('tr');

            if (dataRows != null) {
                
                // alert(dataRows); commented by Saurabh on 11/08/2022
                for (i = 0; i <= dataRows.length - 1; i++) {
                    var chk = dataRows.item(i).getElementsByTagName('td');

                    var temp = chk.item(0).getElementsByTagName('input').item(0);
                   
                    if (temp.checked) {
                        selectedStudent++;

                    }

                }
            }

            totvacant = parseInt(document.getElementById('<%= lblVacancy.ClientID %>').innerHTML);
            // alert(totvacant);  commented by Saurabh on 11/08/2022
            if (selectedStudent > 0) {
                if (selectedStudent <= totvacant) {
                    __doPostBack(btnAllotRoom);
                }
                else {
                    alert('Only (' + totvacant + ') seats are Available in this room. \nYou can select maximum (' + totvacant + ') students to allot this room.');
                }
            }
            else {
                alert('Please atleast single student to allot selected room.');
            }
            return;
        }


        function vacantlist() {
            debugger;
            var totvacant = 0;
            var selectedStudent = 0;
            var totalStudent = 0;

            var dataRows = document.getElementById('tblSearchResults').getElementsByTagName('tr');

            if (dataRows != null) {
                //alert(dataRows);
                for (i = 0; i <= dataRows.length - 1; i++) {
                    var chk = dataRows.item(i).getElementsByTagName('td');

                    var temp = chk.item(0).getElementsByTagName('input').item(0);
                    if (temp.checked) {
                        selectedStudent++;
                    }
                }
            }

            totvacant = parseInt(selectedStudent) - parseInt(document.getElementById('<%= lblVacancy.ClientID %>').innerHTML);
            document.getElementById('<%= lblVacancy.ClientID %>').innerHTML = totvacant;





        }
        //Remove the DIV text related field by shubham On 20072022
        function SubmitSearch(btnsearch) {
            try {
                var searchParams = "";
                var txt = document.getElementById('<%= txtSearchStud.ClientID %>').value.trim();
                var ddlDeg = document.getElementById('<%= ddlDegree.ClientID %>').selectedIndex;
                var ddlBran = document.getElementById('<%= ddlBranch.ClientID %>').selectedIndex;



                if (txt != '' || (ddlDeg > 0 && ddlBran > 0)) {
                    searchParams = "RegNo=" + document.getElementById('<%= txtSearchStud.ClientID %>').value.trim();
                    searchParams += ",DegreeNo=" + document.getElementById('<%= ddlDegree.ClientID %>').options[document.getElementById('<%= ddlDegree.ClientID %>').selectedIndex].value;
                    searchParams += ",BranchNo=" + document.getElementById('<%= ddlBranch.ClientID %>').options[document.getElementById('<%= ddlBranch.ClientID %>').selectedIndex].value;
                    searchParams += ",YearNo=" + document.getElementById('<%= ddlYear.ClientID %>').options[document.getElementById('<%= ddlYear.ClientID %>').selectedIndex].value;
                    searchParams += ",SemNo=" + document.getElementById('<%= ddlSem.ClientID %>').options[document.getElementById('<%= ddlSem.ClientID %>').selectedIndex].value;


                    __doPostBack(btnsearch, searchParams);
                }
                else {
                    alert('Please enter RRN.No. (OR) Select Degree and Branch to search student');
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return;
        }
        function ClearSearch(btnclear) {
            try {
                __doPostBack(btnclear);
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return;
        }
        function ddlChange(ddl) {
            try {
                __doPostBack(ddl);
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return;
        }
        function Report(btnreport) {
            try {
                __doPostBack(btnreport);
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return;
        }
    </script>

     <%--<script type="text/javascript">
        $(document).ready(function () {
            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });
        function bindDataTable() {
            var myDT = $('#myTable').DataTable({
                "scrollY": "400px",
                "scrollX": true,
                "scrollCollapse": true

            });
        }
    </script>--%>

</asp:Content>
