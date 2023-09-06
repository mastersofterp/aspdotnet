<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="OperatorEndSemExamMarkEntry.aspx.cs" Inherits="OperatorEndSemExamMarkEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .divgrid
        {
            height: 200px;
            width: 370px;
        }

            .divgrid table
            {
                width: 350px;
            }

                .divgrid table th
                {
                    background-color: Green;
                    color: #fff;
                }
    </style>

    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <style>
                table#ctl00_ContentPlaceHolder1_gvStudent_ctl02_cecomTutMarks_popupTable {
                left: 980px !important;
                top: 85px !important;
                }
                </style>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>OPERATOR END SEM MARK ENTRY</b></h3>
                            <div class="pull-right">
                                <div style="color: Red; font-weight: bold;">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="box-body">
                            <asp:Panel ID="pnlMarkEntry" runat="server">

                                <div class="col-md-12">
                                    <div class="form-group col-md-9">
                                        <div class="form-group col-md-4">
                                            <span style="color: red">*</span><label>Session</label>
                                            <asp:DropDownList ID="ddlSession" TabIndex="1" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <span style="color: red">*</span><label>Institute Name</label>
                                            <asp:DropDownList ID="ddlcollege" TabIndex="2" runat="server" CssClass="form-control" AppendDataBoundItems="true" ToolTip="Please Select Institute"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlcollege_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlcollege"
                                                Display="None" ErrorMessage="Please Select Institute." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <span style="color: red">*</span><label>Degree</label>
                                            <asp:DropDownList ID="ddldegree" TabIndex="3" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddldegree_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvdegree" runat="server" ControlToValidate="ddldegree"
                                                Display="None" ErrorMessage="Please Select Degree." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <span style="color: red">*</span><label>Branch</label>
                                            <asp:DropDownList ID="ddlbranch" TabIndex="4" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvbranch" runat="server" ControlToValidate="ddlbranch"
                                                Display="None" ErrorMessage="Please Select Branch." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <span style="color: red">*</span><label>Scheme</label>
                                            <asp:DropDownList ID="ddlscheme" TabIndex="5" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvscheme" runat="server" ControlToValidate="ddlscheme"
                                                Display="None" ErrorMessage="Please Select Scheme." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <span style="color: red">*</span><label>Semester</label>
                                            <asp:DropDownList ID="ddlsemester" TabIndex="6" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlsemester_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvsemester" runat="server" ControlToValidate="ddlsemester"
                                                Display="None" ErrorMessage="Please Select Semester." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <span style="color: red">*</span><label>Subject Type</label>
                                            <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="True"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged"
                                                TabIndex="7" CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                                Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="show">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <span style="color: red">*</span><label>Course Name</label>
                                            <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="True"
                                                CssClass="form-control" AutoPostBack="True" TabIndex="8" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                                Display="None" ErrorMessage="Please Select Course Name." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <span style="color: red">*</span><label>Exam Name</label>
                                            <asp:DropDownList ID="ddlExam" runat="server" AppendDataBoundItems="True"
                                                CssClass="form-control" TabIndex="9" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExam"
                                                Display="None" ErrorMessage="Please Select Exam Name." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-4" style="margin-top: 25px">
                                        </div>

                                    </div>

                                    <div class="form-group col-md-3">
                                        <fieldset class="fieldset" style="padding: 5px; color: Green">
                                            <legend class="legend">Note</legend>
                                            <span style="font-weight: bold; color: Red;">Please Save and Lock for Final Submission of Marks
                                            </span>
                                            <br />
                                            <b>Enter :<br />
                                                "-1" for Absent Student<br />
                                                "-2" for UFM(Copy Case)
                                                <br />
                                                "-3" for Withdrawl<br />
                                                "-4" for Drop </b>
                                        </fieldset>
                                    </div>

                                    <div class="col-md-12">
                                        <asp:Label ID="lblStudents" runat="server" Font-Bold="true" />
                                    </div>

                                    <div class="col-md-12">
                                        <p class="text-center">
                                            <asp:Button ID="btnShow" runat="server" TabIndex="10" Font-Bold="True" OnClick="btnShow_Click"
                                                Text="Show Student" ValidationGroup="show"
                                                CssClass="btn btn-primary" />
                                            <asp:Button ID="btnSave" runat="server" TabIndex="12" Enabled="false" Font-Bold="true"
                                                OnClick="btnSave_Click" Text="Save" CssClass="btn btn-success" ValidationGroup="val" />

                                            <asp:Button ID="btnLock" runat="server" TabIndex="13" Enabled="false" Font-Bold="true"
                                                OnClick="btnLock_Click" OnClientClick="return showLockConfirm(this,'val');" Text="Lock"
                                                CssClass="btn btn-warning" />
                                         
                                            <asp:Button ID="btnUnlock" runat="server" TabIndex="14" Font-Bold="true" Visible="false"
                                                Text="Unlock" Enabled="false"
                                                CssClass="btn btn-primary" OnClick="btnUnlock_Click" OnClientClick="return showUnLockConfirm(this,'val');" />
                                                                 
                                             <asp:Button ID="btnReport" TabIndex="15" runat="server" Font-Bold="true" Text="Report" CssClass="btn btn-info"
                                                OnClick="btnReport_Click" Enabled="false" Visible="False" />
                                                                 
                                            <asp:Button ID="btnCancel2" runat="server" TabIndex="11" Font-Bold="true" OnClick="btnCancel2_Click"
                                                Text="Cancel" CssClass="btn btn-danger" />
                                           

                                            <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="show" DisplayMode="List" ShowSummary="false"
                                                ShowMessageBox="true" />
                                        </p>
                                        <div class="col-md-12" id="tdStudent" runat="server" >
                                            <asp:Panel ID="pnlStudGrid" runat="server" Visible="false" ScrollBars="Auto">
                                                <div id="demo-grid">
                                                    <h4>Enter Marks for following Students   </h4>
                                                    <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False"
                                                        CssClass="table table-hover table-bordered">
                                                        <HeaderStyle CssClass="bg-light-blue" />
                                                        <%-- --%>
                                                        <RowStyle Height="0px" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr.No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="DECODENO" HeaderText="Decode No.">
                                                                <ItemStyle />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIDNO" Visible="false" runat="server" Text='<%# Bind("REGNO") %>' ToolTip='<%# Bind("IDNO") %>'
                                                                        Font-Size="9pt" />
                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                            </asp:TemplateField>
                                                            <%--<asp:BoundField DataField="ROLLNO" HeaderText="Roll NO." Visible="false">
                                                                <ItemStyle />
                                                            </asp:BoundField>--%>
                                                            <asp:BoundField DataField="STUDNAME" HeaderText="Student Name" Visible="false">
                                                                <ItemStyle />
                                                            </asp:BoundField>

                                                            <asp:TemplateField HeaderText="MARKS" Visible="false">
                                                                <ItemTemplate>

                                                                    <asp:TextBox ID="txtMarks" runat="server" Text='<%# Bind("SMARK") %>'
                                                                        Enabled='<%# (Eval("LOCK").ToString() == "True") ? false : true %>'
                                                                        MaxLength="4" Font-Bold="true" Style="text-align: center" onkeyup="return CheckMark(this);" />
                                                                    <asp:Label ID="lblMarks" runat="server" Text='<%# Bind("SMAX") %>' ToolTip='<%# Bind("LOCK") %>'
                                                                        Visible="false" />
                                                                    <asp:HiddenField ID="hdfMarks" runat="server" Value='<%# Bind("SMARK") %>' />
                                                                    <asp:Label ID="lblMinMarks" runat="server" Text='<%# Bind("SMIN") %>' Visible="false" />
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtTutMarks" runat="server" FilterType="Custom"
                                                                        ValidChars="0123456789-." TargetControlID="txtMarks">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                    <asp:CompareValidator ID="comTutMarks" ValueToCompare='<%# Bind("SMAX") %>' ControlToValidate="txtMarks"
                                                                        Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                                                        ValidationGroup="val" Text="*"></asp:CompareValidator>
                                                                    <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comTutMarks" ID="cecomTutMarks"
                                                                        runat="server">
                                                                    </ajaxToolKit:ValidatorCalloutExtender>
                                                                    <asp:CompareValidator ID="cvAbsentStud" ValueToCompare="-1" ControlToValidate="txtMarks"
                                                                        Operator="NotEqual" Type="Double" runat="server" ErrorMessage="-1 for absent student"
                                                                        ValidationGroup="val1" Text="*">
                                                                    </asp:CompareValidator>
                                                                    <ajaxToolKit:ValidatorCalloutExtender TargetControlID="cvAbsentStud" ID="vceAbsentStud"
                                                                        runat="server">
                                                                    </ajaxToolKit:ValidatorCalloutExtender>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="25%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>

                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnLock" />
            <asp:PostBackTrigger ControlID="btnUnlock" />
        </Triggers>
    </asp:UpdatePanel>


    <div id="divMsg" runat="server">
    </div>
    <asp:HiddenField ID="hdfDegree" runat="server" Value="" />
    <script type="text/javascript">

        function CheckMark(id) {
            //alert(id.value);
            if (id.value < 0) {
                if (id.value == -1 || id.value == -2 || id.value == -3 || id.value == -4)  /// || id.value == -3
                {
                    //alert("You have marked student as ABSENT.");
                }
                else {
                    alert("Marks Below Zero are Not allowed. Only -1 ,-2,-3 and -4 can be entered.");
                    id.value = '';
                    id.focus();
                }
            }
        }

        function validateMark(txttut, txtmidsem, txtCT, txtCE, txtatt, txttot, col) {

            var mark1, mark2, mark3, mark4, mark5, total;

            if (txttut.value == "") {
                txttot.value = "";
                mark1 = 0
            }
            else {
                mark1 = txttut.value;

            }
            if (txtmidsem.value == "") {
                txttot.value = "";
                mark2 = 0;
            }
            else {
                mark2 = txtmidsem.value;

            }
            if (txtCT.value == "") {
                txttot.value = "";
                mark3 = 0;
            }
            else {
                mark3 = txtCT.value;

            }
            if (txtCE.value == "") {
                txttot.value = "";
                mark4 = 0;
            }
            else {
                mark4 = txtCE.value;

            }
            if (txtatt.value == "") {
                txttot.value = "";
                mark5 = 0;
            }
            else {
                mark5 = txtatt.value;

            }
            if (col == 2) {
                total = parseFloat(mark2) + parseFloat(mark3) + parseFloat(mark5);
            }
            else if (col == 1) {
                total = parseFloat(mark2) + parseFloat(mark3) + parseFloat(mark5) + parseFloat(mark1) + parseFloat(mark4);
            }
            else if (col == 3) {
                total = parseFloat(mark4);
            }
            else if (col == 4) {
                total = parseFloat(mark2) + parseFloat(mark3) + parseFloat(mark5) + parseFloat(mark1);
            }
            txttot.value = total;
        }

        function showLockConfirm(btn, group) {
            debugger;
            var validate = false;
            if (Page_ClientValidate(group)) {
                validate = ValidatorOnSubmit();
            }
            if (validate)    //show div only if page is valid and ready to submit
            {
                var ret = confirm('Do you really want to lock marks for selected exam?\n\nOnce Locked it cannot be modified or changed.');
                if (ret == true) {
                    var ret2 = confirm('You are about to lock entered marks, be sure before locking.\n\nClick OK to Continue....');
                    if (ret2 == true) {
                      
                        //validate = true; //COMMENT BY MAHESH ON DATED 17/12/2019

                        if (document.getElementById('ctl00_ContentPlaceHolder1_gvStudent') != null) {
                            dataRows = document.getElementById('ctl00_ContentPlaceHolder1_gvStudent').getElementsByTagName('tr');
                        }
                       
                        if (dataRows.length > 0) {
                            var GridId = "<%=gvStudent.ClientID %>";
                            var grid = document.getElementById(GridId);
                            var gridHeight = grid.offsetHeight;

                            if (gridHeight > 500) {
                                count = dataRows.length + 1;
                            }
                            else {
                                count = dataRows.length
                            }
                       
                            EmptyCount = 0;
                            var i = 0;
                            for (i = 2; i <= count; i++)
                            {
                                var Marks = '';
                                if (i < 10) {
                                    Marks = document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl0' + i + '_hdfMarks').value;
                                    if (Marks == "") {
                                        EmptyCount = 1;
                                    }
                                }
                                else {
                                    Marks = document.getElementById('ctl00_ContentPlaceHolder1_gvStudent_ctl' + i + '_hdfMarks').value;
                                    if (Marks == "") {
                                        EmptyCount = 1;
                                    }
                                }
                            }

                            if (EmptyCount == 1) {
                                alert("Marks Entry Not Completed!! Please Enter the Marks for all Students.");
                                validate = false;
                            }
                            else {
                                validate = true;
                            }
                        }
                    }
                    else
                        validate = false;
                }
                else
                    validate = false;
            }
            return validate;
        }

        function showUnLockConfirm(btn, group) {
            debugger;
            var validate = false;
            if (Page_ClientValidate(group)) {
                validate = ValidatorOnSubmit();
            }
            if (validate)    //show div only if page is valid and ready to submit
            {
                var ret = confirm('Do you really want to unlock marks for selected exam?');
                if (ret == true) {
                    // var ret2 = confirm('You are about to unlock entered marks, be sure before locking.\n\nClick OK to Continue....');
                    // if (ret2 == true) {
                    validate = true;
                }
                else
                    validate = false;
            }
            return validate;
        }

        function showLockConfirm_old() {
            var ret = confirm('Do you really want to lock marks for selected exam?');
            if (ret == true)
                return true;
            else
                return false;
        }
    </script>

    <!-- Gridview header freeze-->
    <script type="text/javascript">
        function FreezeGridHeader() {
            var GridId = "<%=gvStudent.ClientID %>";
              var ScrollHeight = 500;
              var grid = document.getElementById(GridId);
              var gridWidth = grid.offsetWidth;
              var gridHeight = grid.offsetHeight;
              var headerCellWidths = new Array();

              if (parseInt(gridHeight) > ScrollHeight) {
                  grid.parentNode.appendChild(document.createElement("div"));
                  var parentDiv = grid.parentNode;

                  var table = document.createElement("table");
                  for (i = 0; i < grid.attributes.length; i++) {
                      if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                          table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                      }
                  }
                  table.style.cssText = grid.style.cssText;
                  table.style.width = "99%";
                  table.appendChild(document.createElement("tbody"));
                  table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
                  var cells = table.getElementsByTagName("TH");

                  var gridRow = grid.getElementsByTagName("TR")[0];
                  for (var i = 0; i < cells.length; i++) {
                      var width;
                      if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                          width = headerCellWidths[i];
                      }
                      else {
                          width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
                      }
                      cells[i].style.width = parseInt(width - 2) + "px";
                      gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width) + "px";
                  }
                  parentDiv.removeChild(grid);

                  var dummyHeader = document.createElement("div");
                  dummyHeader.appendChild(table);
                  parentDiv.appendChild(dummyHeader);
                  var scrollableDiv = document.createElement("div");
                  if (parseInt(gridHeight) > ScrollHeight) {
                      //gridWidth = parseInt(gridWidth) + 17;
                      gridWidth = parseInt(gridWidth);
                  }
                  //scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
                  scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:100%";
                  //scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;";
                  scrollableDiv.appendChild(grid);
                  parentDiv.appendChild(scrollableDiv);
              }
          };
    </script>
    <!-- Gridview header freeze-->
</asp:Content>