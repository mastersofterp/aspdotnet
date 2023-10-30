<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DirectMedicineIssue.aspx.cs" Inherits="Health_Transaction_DirectMedicineIssue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script src="../../includes/prototype.js" type="text/javascript"></script>
    <script src="../../includes/scriptaculous.js" type="text/javascript"></script>
    <script src="../../includes/modalbox.js" type="text/javascript"></script>
    <link href="../../CSS/master.css" rel="stylesheet" />

    <script type="text/javascript">
        ; debugger
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 9999999999;
        }
    </script>--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>

    <link href="../plugins/jQuery/jquery_ui_min/jquery-ui.min.css" rel="stylesheet" />
    <script src="../plugins/jQuery/jquery_ui_min/jquery-ui.min.js"></script>
    <script type="text/javascript">
        var jq = $.noConflict();

    </script>
    <%--    <style type="text/css">
        .style2 {
            width: 100%;
        }

        h4 {
            font-size: 16px;
            font-family: "Trebuchet MS", Verdana;
            line-height: 18px;
        }

        .body_bg {
            background-image: url(Images/body_bg.jpg);
            background-repeat: repeat;
        }

        .Popup {
            border-style: solid;
            padding-top: 10px;
            padding-left: 10px;
            width: 100%;
            height: 100%;
            background-image: url(Images/bg_gray.png);
            background-repeat: repeat-x;
            background-position: left top;
        }

        .BackgroundReg {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .PopupReg {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-bottom: 15px;
            padding-left: 10px;
            padding-right: 10px;
            /*width: 80%;
            height: 80%;*/
            width: 30%;
            min-width: 60%;
            min-height: 20%;
        }

        .lblReg {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }

        .auto-style2 {
            width: 297px;
        }

        .auto-style3 {
            width: 203px;
        }
    </style>--%>
    <script type="text/javascript">

        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 9999999999;
        }

        var $ = $.noConflict();

        function ShowpImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#ContentPlaceHolder1_imgEmpPhoto').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
    <asp:UpdatePanel ID="updOpdTransaction" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">DIRECT MEDICINE ISSUE</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <%--<b><span style="color: #FF0000">Note:* Marked Is Mandatory !</span></b>--%>
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <%--                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Search<span style="color: Red">*</span>&nbsp&nbsp:</label>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <asp:RadioButtonList ID="rdbSearchList" runat="server" RepeatDirection="Horizontal" ToolTip="Search Patient">
                                                        <asp:ListItem Selected="True" Value="0">Staff ID&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="1">Admission No.</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group col-md-1">
                                                    <a href="#" title="Search Patient" data-toggle="modal" data-target="#divdemo2">
                                                        <asp:Image ID="imgSearch" runat="server" ImageUrl="~/images/search.png" />
                                                    </a>
                                                </div>
                                            </div>--%>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Search</label>
                                                </div>
                                                <div class="input-group date">
                                                    <asp:RadioButtonList ID="rdbSearchList" runat="server" RepeatDirection="Horizontal" ToolTip="Search Patient">
                                                        <asp:ListItem Selected="True" Value="0">Staff ID</asp:ListItem>
                                                        <asp:ListItem Value="1">Admission No.</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <div class="input-group-addon" id="Div3">
                                                        <a href="#" title="Search Patient Details" data-toggle="modal" data-target="#divdemo2">
                                                            <asp:Image ID="imgSearch" runat="server" ImageUrl="~/images/search.png" TabIndex="1" />
                                                        </a>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Search Patient </label>
                                                </div>
                                                <div class="input-group date">
                                                    <asp:TextBox ID="txtPatientName" runat="server" CssClass="form-control" MaxLength="100"
                                                        TabIndex="3" ToolTip="Search Patient"></asp:TextBox>
                                                    <asp:HiddenField ID="hfPatientName" runat="server" />
                                                    <asp:RequiredFieldValidator ID="rfvPatientName" runat="server" ControlToValidate="txtPatientName" Display="None"
                                                        ErrorMessage="Please search patient name." SetFocusOnError="true" ValidationGroup="Doctor" />

                                                    <div class="input-group-addon" id="Div4" style="border-bottom: 1px solid #fff;">
                                                        <asp:Button ID="btnSearchOnForm" runat="server" OnClick="btnSearchOnForm_Click" TabIndex="2" Text="Search"
                                                            ToolTip="Search Patient" CssClass="btn btn-primary" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <asp:Label ID="lblEmp" runat="server" Text="Employee Code" Font-Bold="true"></asp:Label>
                                                    <asp:Label ID="lblDot" runat="server" Text=":"></asp:Label>
                                                    <asp:Label ID="lblEmployeeCode" runat="server" Text=""></asp:Label>
                                                    <asp:Label ID="lblPatientCat" runat="server" Text="" Visible="false" CssClass="form-control"></asp:Label>
                                                </div>

                                                <div id="trDependent" class="col-lg-6" runat="server" visible="false">
                                                    <div class="row">
                                                        <div class="form-group col-lg-5 col-md-5 col-12">
                                                            <div class="label-dynamic">
                                                                <label></label>

                                                            </div>
                                                            <asp:RadioButtonList ID="rdbPCList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdbPCList_SelectedIndexChanged"
                                                                RepeatDirection="Horizontal" TabIndex="3">
                                                                <asp:ListItem Selected="True" Value="0">Self &nbsp;&nbsp;</asp:ListItem>
                                                                <asp:ListItem Value="1">Dependent</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>

                                                        <div class="form-group col-lg-7 col-md-7 col-12">
                                                            <div class="label-dynamic">
                                                                <label id="FirstDep" runat="server" visible="false"><sup>*</sup>Dependents</label>
                                                                <label id="SecDep" runat="server" visible="false"></label>
                                                            </div>
                                                            <div id="ThiDep" runat="server" visible="false">
                                                                <asp:DropDownList ID="ddlDependent" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                                                    OnSelectedIndexChanged="ddlDependent_SelectedIndexChanged" TabIndex="4" ValidationGroup="Doctor">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvDependent" runat="server" ControlToValidate="ddlDependent" Display="None" ErrorMessage="Please Select Dependent."
                                                                    InitialValue="0" SetFocusOnError="True" ValidationGroup="Doctor"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Age (yrs)</label>
                                                </div>
                                                <div class="input-group date">
                                                    <asp:TextBox ID="txtAge" runat="server" MaxLength="20" TabIndex="5" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvAge" runat="server" ControlToValidate="txtAge" Display="None"
                                                        ErrorMessage="Please enter age." SetFocusOnError="true" ValidationGroup="Doctor" />
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Weight (Kg)</label>
                                                </div>
                                                <div class="input-group date">
                                                    <asp:TextBox ID="txtWeight" runat="server" MaxLength="3" onkeyup="validateNumeric(this);"
                                                        TabIndex="9" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Sex </label>
                                                </div>
                                                <%-- <br />--%>
                                                <div class="input-group date">
                                                    <asp:RadioButton ID="rdbMale" runat="server" Checked="true" GroupName="Sex" TabIndex="7" Text="Male" />
                                                    <asp:RadioButton ID="rdbFemale" runat="server" GroupName="Sex" TabIndex="8" Text="Female" />
                                                </div>
                                            </div>
                                            <%--</div>
                                        <div class="row">--%>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trOtherCategory" runat="server" visible="true">
                                                <div class="label-dynamic">
                                                    <label>Complaint </label>
                                                </div>
                                                <div class="input-group date">
                                                    <asp:TextBox ID="txtOtherComplaint" runat="server" MaxLength="100" TextMode="MultiLine" TabIndex="1"
                                                        CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divRef" runat="server" visible="false">
                                                <label>Reference By&nbsp&nbsp:</label>
                                                <div class="input-group date">
                                                    <asp:TextBox ID="txtReference" runat="server" MaxLength="50" TabIndex="10" CssClass="form-control"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="ftbeCReference" runat="server" FilterType="Custom,LowerCaseLetters,UpperCaseLetters" TargetControlID="txtReference" ValidChars=".  ">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <div id="trPatientHist" runat="server">
                                    <asp:Panel ID="pnlPatientHist" runat="server">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Patient History</h5>
                                            </div>
                                            <div class="col-12">
                                                <asp:Panel ID="pnlPatientHistDetail" runat="server" ScrollBars="Horizontal">
                                                    <div class="col-12">
                                                        <asp:GridView ID="lvPatientHist" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="ActiveBorder"
                                                            AlternatingRowStyle-BackColor="#FFFFAA" CssClass="table table-bordered table-hover">
                                                            <HeaderStyle CssClass="header" />
                                                            <AlternatingRowStyle BackColor="#FFFFD2" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="View" HeaderStyle-CssClass="bg-light-blue" ItemStyle-CssClass="visible-lg">
                                                                    <ItemTemplate>
                                                                        <%--<asp:Button ID="btnDetails" runat="server" Text="Details" Width="80px" CommandArgument='<%# Eval("OPDID") %>'
                                                            OnClick="btnDetails_Click" />--%>
                                                                        <asp:ImageButton ID="btnDetails" runat="server" AlternateText="Show Details" CommandArgument='<%# Eval("OPDID") %>' ImageUrl="~/IMAGES/edit.gif"
                                                                            OnClick="btnDetails_Click" ToolTip="Edit Record" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Print Presc" HeaderStyle-CssClass="bg-light-blue" ItemStyle-CssClass="visible-lg">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnPrint" runat="server" AlternateText="Print Record" CommandArgument='<%# Eval("OPDID") %>' ImageUrl="~/IMAGES/print.gif" OnClick="btnPrint_Click" ToolTip='<%# Eval("OPDID") %>' />
                                                                        &nbsp;
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="NAME" HeaderText="Patient Name" HeaderStyle-CssClass="bg-light-blue" />
                                                                <asp:BoundField DataField="DRNAME" HeaderText="Doctor Name" HeaderStyle-CssClass="bg-light-blue" />
                                                                <asp:TemplateField HeaderText="OPD Date" HeaderStyle-CssClass="bg-light-blue" ItemStyle-CssClass="visible-lg">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDate" runat="server" Text='<%#Eval("OPDDATE","{0:d}") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="COMPLAINT" HeaderText="Complaint" HeaderStyle-CssClass="bg-light-blue" />
                                                                <asp:BoundField DataField="FINDING" HeaderText="Finding" HeaderStyle-CssClass="bg-light-blue" />
                                                                <asp:BoundField DataField="DIAGNOSIS" HeaderText="Diagnosis" HeaderStyle-CssClass="bg-light-blue" />
                                                                <asp:BoundField DataField="INSTRUCTION" HeaderText="Instructions" HeaderStyle-CssClass="bg-light-blue" />
                                                                <asp:BoundField DataField="HEIGHT" HeaderText="Height(In Inches)" HeaderStyle-CssClass="bg-light-blue" />
                                                                <asp:BoundField DataField="WEIGHT" HeaderText="Weight(In KGs)" HeaderStyle-CssClass="bg-light-blue" />
                                                                <asp:BoundField DataField="TEMP" HeaderText="Temperature" HeaderStyle-CssClass="bg-light-blue" />
                                                                <asp:BoundField DataField="PULSE" HeaderText="Pulse" HeaderStyle-CssClass="bg-light-blue" />
                                                                <asp:BoundField DataField="RESP" HeaderText="Respiration" HeaderStyle-CssClass="bg-light-blue" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>

                                <div id="trDirectIssueHistory" runat="server" visible="false">
                                    <asp:Panel ID="pnlDMIHis" runat="server">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Direct Medicine</h5>
                                            </div>
                                            <div class="col-12">
                                                <asp:Panel ID="Panel3" runat="server" ScrollBars="Horizontal">
                                                    <asp:GridView ID="gridDirectMIssue" runat="server" AlternatingRowStyle-BackColor="#FFFFAA" AutoGenerateColumns="False"
                                                        CssClass="table table-striped table-bordered table-hover" HeaderStyle-BackColor="ActiveBorder">
                                                        <HeaderStyle CssClass="bg-light-blue" />
                                                        <AlternatingRowStyle BackColor="#FFFFAA" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSR" runat="server" Text='<%# Eval("UNIQUE") %>'>                                                               
                                                                    </asp:Label>
                                                                    <asp:HiddenField ID="hdnINO" runat="server" Value='<%#Eval("INO")%>' />
                                                                    <asp:HiddenField ID="hdnDosesID" runat="server" Value='<%# Eval("DOSES") %>' />
                                                                    <asp:HiddenField ID="hdnSRNO" runat="server" Value='<%# Eval("UNIQUE") %>' />
                                                                    <asp:HiddenField ID="hdnPStatusID" runat="server" Value='<%# Eval("PRESCRIPTION_STATUS_ID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="INO" HeaderText="ITEM NUMBER" Visible="false" />
                                                            <asp:BoundField DataField="ITEMNAME" HeaderText="Item Name" />
                                                            <asp:BoundField DataField="NOOFDAYS" HeaderText="No Of Days" />
                                                            <asp:BoundField DataField="DOSES_ID" HeaderText="Doses" />
                                                            <asp:BoundField DataField="DOSES" HeaderText="DOSES_ID" Visible="false" />
                                                            <asp:BoundField DataField="QTY" HeaderText="Quantity" />
                                                            <asp:BoundField DataField="SPINST" HeaderText="Sp.Instruction" />
                                                            <asp:BoundField DataField="UNIQUE" HeaderText="SRNO" Visible="false" />
                                                            <asp:BoundField DataField="PRESCRIPTION_STATUS_ID" HeaderText="ISSUE_STATUS" Visible="false" />
                                                            <asp:BoundField DataField="PRESCRIPTION_STATUS" HeaderText="Issue Status" Visible="false" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>

                                <asp:Panel ID="pnlSGrid" runat="server" Visible="false">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Prescription List</h5>
                                        </div>
                                        <div class="col-12">
                                            <asp:GridView ID="SecondGrid" runat="server" AlternatingRowStyle-BackColor="#FFFFAA" AutoGenerateColumns="False"
                                                HeaderStyle-BackColor="ActiveBorder" CssClass="table table-striped table-bordered table-hover">
                                                <HeaderStyle CssClass="bg-light-blue" />
                                                <AlternatingRowStyle BackColor="#FFFFAA" />
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSR" runat="server" Text='<%# Eval("UNIQUE") %>'>                                                               
                                                            </asp:Label>
                                                            <%--<asp:ImageButton ID="btnDel" AlternateText="Delete Record" CommandArgument='<%# Eval("UNIQUE") %>'
                                                                ToolTip="Delete Record" runat="server" ImageUrl="~/images/delete.gif" OnClick="btnDelete_Click" />&nbsp;--%>
                                                            <asp:HiddenField ID="hdnINO" runat="server" Value='<%#Eval("INO")%>' />
                                                            <asp:HiddenField ID="hdnDosesID" runat="server" Value='<%# Eval("DOSES") %>' />
                                                            <asp:HiddenField ID="hdnSRNO" runat="server" Value='<%# Eval("UNIQUE") %>' />
                                                            <asp:HiddenField ID="hdnPStatusID" runat="server" Value='<%# Eval("PRESCRIPTION_STATUS_ID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="INO" HeaderText="ITEM NUMBER" Visible="false" />
                                                    <asp:BoundField DataField="ITEMNAME" HeaderText="Item Name" />
                                                    <asp:BoundField DataField="NOOFDAYS" HeaderText="No Of Days" />
                                                    <asp:BoundField DataField="DOSES_ID" HeaderText="Doses" />
                                                    <asp:BoundField DataField="DOSES" HeaderText="DOSES_ID" Visible="false" />
                                                    <asp:BoundField DataField="QTY" HeaderText="Quantity" />
                                                    <%-- <asp:BoundField DataField="SPINST" HeaderText="Sp.Instruction" />--%>
                                                    <asp:BoundField DataField="UNIQUE" HeaderText="SRNO" Visible="false" />
                                                    <asp:BoundField DataField="PRESCRIPTION_STATUS_ID" HeaderText="ISSUE_STATUS" Visible="false" />
                                                    <asp:BoundField DataField="PRESCRIPTION_STATUS" HeaderText="Issue Status" Visible="false" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <div id="trDetails" runat="server" visible="false">
                                    <asp:Panel ID="pnlDetails" runat="server">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Patient Complaints</h5>
                                            </div>
                                            <div class="col-12">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Complaints </label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <asp:TextBox ID="txtComplaints" runat="server" MaxLength="100" TabIndex="13"
                                                            TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                        <%-- <asp:RequiredFieldValidator ID="rfvComp" runat="server" ControlToValidate="txtComplaints" ToolTip="Add Patient Complaints" 
                                                        ValidationGroup="Doctor"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Findings </label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <asp:TextBox ID="txtFindings" runat="server" MaxLength="100" TabIndex="14" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>BP (mmHg)</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <asp:TextBox ID="txtBP" runat="server" MaxLength="10" TabIndex="17" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="ftbeBP" runat="server" FilterType="Custom, Numbers" TargetControlID="txtBP" ValidChars="/">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Temperature (degrees C)</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <asp:TextBox ID="txtTemprature" runat="server" MaxLength="3" onkeyup="validateNumeric(this);" TabIndex="18"
                                                            CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Pulse Rate (beats/min)</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <asp:TextBox ID="txtPulseRate" runat="server" MaxLength="3" TabIndex="19" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="ftbePulseRate" runat="server" FilterType="Numbers" TargetControlID="txtPulseRate"
                                                            ValidChars=" ">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Respiration (breaths/min)</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <asp:TextBox ID="txtRespiration" runat="server" MaxLength="3" TabIndex="20" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="ftbeRespi" runat="server" FilterType="Numbers" TargetControlID="txtRespiration" ValidChars="">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>

                                <asp:Panel ID="pnlMedicineGrid" runat="server" Visible="false">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Medicine Details</h5>
                                        </div>
                                        <asp:GridView ID="lvMedicineissue" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="CancelEdit"
                                            OnRowEditing="EditCustomer" ShowFooter="true" CssClass="table table-striped table-bordered table-hover">
                                            <HeaderStyle CssClass="bg-light-blue" />
                                            <AlternatingRowStyle BackColor="#FFFFAA" />
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Sr.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Eval("ADMNO")%>' />
                                                        <%-- <%# Container.DataItemIndex + 1%>--%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="Medicine Name" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtItemname" runat="server" AutoPostBack="true" OnTextChanged="txtItemname_TextChanged" Text='<%# Eval("ITEMNAME")%>' Width="150px" />
                                                        <asp:HiddenField ID="hfItemName" runat="server" />
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="100" CompletionSetCount="10" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="2" OnClientItemSelected="ItemName" ServiceMethod="GetItemName" TargetControlID="txtItemname">
                                                        </cc1:AutoCompleteExtender>
                                                        <asp:Label ID="lblino" runat="server" Text='<%# Eval("INO")%>' Visible="false" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="15%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="Doses" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDoses" runat="server" AutoPostBack="true" OnTextChanged="txtDoses_TextChanged" Text='<%# Eval("DNAME")%>' Width="100px"></asp:TextBox>
                                                        <asp:HiddenField ID="hfDoses" runat="server" />
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionInterval="100" CompletionSetCount="10" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="2" OnClientItemSelected="DoseName" ServiceMethod="GetDoseName" TargetControlID="txtDoses">
                                                        </cc1:AutoCompleteExtender>
                                                        <asp:Label ID="lblDno" runat="server" Text='<%# Eval("DOSES")%>' Visible="false" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtItemDoses" runat="server" onblur="OneTextToother();" Text='<%# Eval("DNAME")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="Issued Quantity" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQtyMain" runat="server" MaxLength="5" onkeypress="return CheckNumeric(event, this);" Text='<%# Eval("QTY_ISSUE")%>' Width="80px" />
                                                        <asp:Label ID="lblqty" runat="server" Text='<%# Eval("QTY_ISSUE")%>' Visible="false" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtItemIssuedQty" runat="server" Text='<%# Eval("QTY_ISSUE")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="Direct Issue Qty." ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIssueQty" runat="server" MaxLength="5" onkeypress="return CheckNumeric(event, this);" Text='<%# Eval("DIRECT_ISSUE")%>' ToolTip="Enter Direct Issue Quantity" Width="80px" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtItemIssueQty" runat="server" Width="80px"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="Available Quantity" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAvailQty" runat="server" Enabled="false" Text='<%# Eval("ITEM_MAX_QTY")%>' Width="80px" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtItemAvailQty" runat="server" Enabled="false" Text='<%# Eval("ITEM_MAX_QTY")%>' Width="80px"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Button ID="btnAdd" runat="server" OnClick="AddNewCustomer" Text="Add" Visible="false" />
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField FooterStyle-HorizontalAlign="Center">
                                                          <ItemTemplate>
                                                              <asp:LinkButton ID="lnkRemove" runat="server" CommandArgument='<%# Eval("INO")%>' OnClick="DeleteCustomer" OnClientClick="return confirm('Do you want to delete?')" Text="Delete"></asp:LinkButton>
                                                          </ItemTemplate>
                                                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                          <ItemStyle Width="10%" />
                                                      </asp:TemplateField>--%>
                                            </Columns>
                                            <EditRowStyle BackColor="#CCCCCC" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>

                                <div class="form-group col-md-12">
                                    <div class="text-center">
                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Doctor" />
                                        &nbsp;<asp:Button ID="btnPrescription" runat="server" OnClick="btnPrescription_OnClick" Text="Previous Prescription Based"
                                            Visible="false" CssClass="btn btn-primary" />
                                        &nbsp;<asp:Button ID="btnNewP" runat="server" OnClick="btnNewP_OnClick" Text="Add New Prescription"
                                            Visible="false" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_OnClick" Text="Save" ValidationGroup="Doctor"
                                            Visible="false" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnSubmitNew" runat="server" OnClick="btnSubmitNew_Click" Text="Submit" Visible="false"
                                            CssClass="btn btn-primary" ValidationGroup="Doctor" />
                                        &nbsp;<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_OnClick" Text="Cancel" Visible="false"
                                            CssClass="btn btn-warning" />
                                    </div>
                                </div>

                                <%--<div class="col-md-12">--%>
                                <%--<ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender2" runat="server"
                                        PopupControlID="Panel4" TargetControlID="btnNewP" BackgroundCssClass="BackgroundReg" BehaviorID="mdlPopupDel">
                                    </ajaxToolKit:ModalPopupExtender>--%>
                                <%--data-backdrop="static"--%>
                                <div id="divPrescription" class="modal fade" role="dialog">
                                    <%--data-backdrop="static"--%>
                                    <div class="modal-dialog">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h4 class="modal-title">Add Prescription</h4>
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            </div>
                                            <%--<div class="modal-body clearfix pb-0">--%>
                                            <div class="modal-body">
                                                <asp:UpdatePanel ID="UpdatePrescription" runat="server">
                                                    <ContentTemplate>
                                                        <%--<asp:Panel ID="Panel4" runat="server" CssClass="modalPopup" Style="display: none; height: auto; width: 60%;">--%>
                                                        <%--<asp:Panel ID="Panel4" runat="server" CssClass="PopupReg" align="center" Style="display: none; height: 70%; width: 60%;">--%>
                                                        <%--<div class="panel panel-info">--%>

                                                        <div class="col-md-12">
                                                            <asp:Button ID="btnCloseP" runat="server" Text="X" Style="font-family: Verdana; font-weight: bold; font-size: 10px"
                                                                CssClass="btnClosePop" Visible="false"
                                                                ToolTip="Close Prescription" OnClick="btnCloseP_Click" />
                                                            <%--</div>--%>
                                                            <div class="row">
                                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>Item Name</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control" ToolTip="Enter Item Name"></asp:TextBox>
                                                                    <asp:HiddenField ID="hfItemName" runat="server" />
                                                                    <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                                        TargetControlID="txtItemName"
                                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="100"
                                                                        ServiceMethod="GetItemName" OnClientShowing="clientShowing" OnClientItemSelected="ItemName">
                                                                    </ajaxToolKit:AutoCompleteExtender>
                                                                    <asp:RequiredFieldValidator ID="rfvItem" runat="server" ControlToValidate="txtItemName"
                                                                        Display="None" ErrorMessage="Please enter item name" ValidationGroup="prescription"
                                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>No. of Days</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtNumberofDays" runat="server" CssClass="form-control"
                                                                        ToolTip="Enter Number Of Days" MaxLength="3"></asp:TextBox>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeNoOfDays" runat="server" FilterType="Numbers"
                                                                        TargetControlID="txtNumberofDays" ValidChars="">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="rfvNoOfdays" runat="server" ControlToValidate="txtNumberofDays"
                                                                        Display="None" ErrorMessage="Please enter number of days." ValidationGroup="prescription"
                                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Issue Status</label>
                                                                    </div>
                                                                    <asp:RadioButton ID="rdbYes" runat="server" Checked="true" GroupName="Status" Text="Issued" />&nbsp;&nbsp;
                                                                    <asp:RadioButton ID="rdbNo" runat="server" GroupName="Status" Text="Prescribed" />
                                                                </div>

                                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>Dosage</label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlDosage" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                                        ToolTip="Select Dosage" data-select2-enable="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvDosage" runat="server" ControlToValidate="ddlDosage" InitialValue="0"
                                                                        Display="None" ErrorMessage="Please select dosages." ValidationGroup="prescription"
                                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>Quantity</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"
                                                                        ToolTip="Enter Quantity" MaxLength="5"></asp:TextBox>
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeQty" runat="server" FilterType="Numbers"
                                                                        TargetControlID="txtQuantity" ValidChars="">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                    <asp:RequiredFieldValidator ID="rfvQty" runat="server" ControlToValidate="txtQuantity"
                                                                        Display="None" ErrorMessage="Please enter quantity." ValidationGroup="prescription"
                                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-lg-6 col-md-6 col-12" id="divSpeInst" runat="server" visible="false">
                                                                    <div class="label-dynamic">
                                                                        <sup></sup>
                                                                        <label>Special Instructions:</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtSpecialInstructions" TextMode="MultiLine" runat="server" CssClass="form-control" ToolTip="Enter Special Instructions" MaxLength="30"></asp:TextBox>
                                                                </div>

                                                                <div class="col-12 btn-footer">
                                                                    <%--<p class="text-center">--%>
                                                                    <asp:Button ID="btnAddPresc" runat="server" Text="Add Prescription" OnClick="btnAddPrescription_OnClick"
                                                                        ValidationGroup="prescription" CssClass="btn btn-primary" ToolTip="Click here to Add Prescriptions" />
                                                                    <asp:ValidationSummary ID="vsPriscription" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                        ShowSummary="false" ValidationGroup="prescription" />
                                                                    <%--</div>--%>

                                                                    <asp:Button ID="btnConfirm" runat="server" Text="Confirm" OnClick="btnConfirm_OnClick"
                                                                        Visible="false" CssClass="btn btn-primary" ToolTip="Click here to Confirm" />&nbsp;&nbsp;  
                                                                <asp:Button ID="btnCancelP" runat="server" Text="Cancel" OnClick="btnCancelP_Click" Visible="false" CssClass="btn btn-warning" />
                                                                    <%--</p>--%>
                                                                </div>

                                                                <div class="form-group col-md-12 text-center">
                                                                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">
                                                                        <asp:GridView ID="gvPrisc" runat="server" AutoGenerateColumns="False"
                                                                            CssClass="table table-striped table-bordered table-hover"
                                                                            HeaderStyle-BackColor="ActiveBorder" AlternatingRowStyle-BackColor="#FFFFAA">
                                                                            <HeaderStyle CssClass="bg-light-blue" />

                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="item">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="btnDelete" AlternateText="Delete Record"
                                                                                            CommandArgument='<%# Eval("UNIQUE") %>'
                                                                                            ToolTip="Delete Record" runat="server" ImageUrl="~/images/delete.gif"
                                                                                            OnClick="btnDelete_Click" />&nbsp;
                                                                                <asp:HiddenField ID="hdnINO" runat="server" Value='<%# Eval("INO") %>' />
                                                                                        <asp:HiddenField ID="hdnDosesID" runat="server" Value='<%# Eval("DOSES") %>' />
                                                                                        <asp:HiddenField ID="hdnSRNO" runat="server" Value='<%# Eval("UNIQUE") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="INO" HeaderText="ITEM NUMBER" Visible="false" />
                                                                                <asp:BoundField DataField="ITEMNAME" HeaderText="ITEM NAME" />
                                                                                <asp:BoundField DataField="NOOFDAYS" HeaderText="NOOFDAYS" />
                                                                                <asp:BoundField DataField="DOSES_ID" HeaderText="DOSES" />
                                                                                <asp:BoundField DataField="DOSES" HeaderText="DOSES_ID" Visible="false" />
                                                                                <asp:BoundField DataField="QTY" HeaderText="QUANTITY" />
                                                                                <%--<asp:BoundField DataField="SPINST" HeaderText="SPINST" />--%>
                                                                                <asp:BoundField DataField="UNIQUE" HeaderText="SRNO" Visible="false" />

                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <%-- </asp:Panel>--%>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnPrescription" />
                                                        <asp:PostBackTrigger ControlID="btnAddPresc" />
                                                        <asp:PostBackTrigger ControlID="btnConfirm" />
                                                        <asp:PostBackTrigger ControlID="btnCancelP" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lvMedicineissue" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="divdemo2" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Search</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="col-md-12">
                                <div class="form-group col-md-12">
                                    <label>Search Criteria:</label>
                                    <br />
                                    <asp:RadioButtonList ID="rbPatient" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Staff ID" Value="EmployeeCode" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Emp. Name" Value="EmployeeName"></asp:ListItem>
                                        <asp:ListItem Text="Student Name" Value="StudentName"></asp:ListItem>
                                        <asp:ListItem Text="Admission No." Value="StudentRegNo"></asp:ListItem>
                                        <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="form-group col-md-6">
                                    <label>Search String :</label>
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" ToolTip="Enter Search String"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-12">
                                    <p class="text-center">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info"
                                            OnClientClick="return submitPopup(this.name);" ToolTip="Click here to Search" />
                                        <asp:Button ID="btnCanceModal" runat="server" Text="Cancel" CssClass="btn btn-warning"
                                            OnClientClick="return ClearSearchBox()" ToolTip="Click here to Reset" OnClick="btnCanceModal_Click" />
                                        <asp:Button ID="Button1" runat="server" Text="Close" class="btn btn-default" data-dismiss="modal"
                                            AutoPostBack="True" CssClass="btn btn-primary" />
                                    </p>
                                </div>
                                <div class="form-group col-md-12">
                                    <asp:UpdatePanel ID="updEdit" runat="server">
                                        <ContentTemplate>
                                            <%--<asp:Panel ID="pnlSearch" runat="server" ScrollBars="Auto" Height="300px">--%>
                                            <%--<asp:Panel ID="pnlSearch" runat="server" ScrollBars="Auto" Height="300px" Width="520px">--%>
                                            <asp:ListView ID="lvEmp" runat="server">
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <%--<h4 class="box-title">Login Details
                                                        </h4>--%>
                                                        <asp:Panel ID="pnlSearch" runat="server" ScrollBars="Vertical" Height="300px">
                                                            <table class="table table-bordered table-hover">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                    <tr id="trHeader" runat="server" class="bg-light-blue">
                                                                        <th>Name
                                                                        </th>
                                                                        <th>Employee Code
                                                                        </th>
                                                                        <th>Department
                                                                        </th>
                                                                        <th>Designation
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name")%>' CommandArgument='<%# Eval("IDNo")%>'
                                                                OnClick="lnkId_Click"></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PFILENO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SUBDEPT")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SUBDESIG")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                            <%--</asp:Panel>--%>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                </div>
            </div>

        </div>
    </div>

    <script type="text/javascript" language="javascript">

        function submitPopup(btnSearch) {
            //debugger;
            var rbText;
            var searchtxt;

            if (document.getElementById('ctl00_ContentPlaceHolder1_rbPatient_0').checked == true)
                rbText = "EmployeeCode";
            else if (document.getElementById('ctl00_ContentPlaceHolder1_rbPatient_1').checked == true)
                rbText = "EmployeeName";
            else if (document.getElementById('ctl00_ContentPlaceHolder1_rbPatient_2').checked == true)
                rbText = "StudentName";
            else if (document.getElementById('ctl00_ContentPlaceHolder1_rbPatient_3').checked == true)
                rbText = "StudentRegNo";
            else if (document.getElementById('ctl00_ContentPlaceHolder1_rbPatient_4').checked == true)
                rbText = "Other";

            searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;
            __doPostBack(btnSearch, rbText + ',' + searchtxt);
            return true;
        }

        function ClearSearchBox() {
            document.getElementById('<%=txtSearch.ClientID %>').value = '';
        }

        function validateNumeric(txt) {
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

        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Alphabets allowed!");
                return false;
            }
            else
                return true;
        }

        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../images/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../images/collapse_blue.jpg";
            }
        }

    </script>

    <script type="text/javascript">

        ; debugger
        function ItemName(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            var Name = idno[0].split("-");

        }

        function DoseName(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            var Name = idno[0].split("-");
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>

    <script type="text/javascript">

        function ItemName(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            //alert(idno);
            var Name = idno[0].split("-");
            // alert(Name);

            document.getElementById("<%= txtItemName.ClientID %>").value = idno[1];
            document.getElementById("<%= hfItemName.ClientID %>").value = Name[0];
        }
        //  keeps track of the delete button for the row

        //document.getElementById('ctl00_ContentPlaceHolder1_txtItemName').value = idno[1];
        //document.getElementById('ctl00_ContentPlaceHolder1_hfItemName').value = Name[0];

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
    </script>
    <script>

        function Close() {
            //$("#Details_Veiw").Show();
            //alert("Close");
            $("#myModal2").modal('hide');
        }
    </script>

    <div id="divMsg" runat="server">
    </div>

</asp:Content>

