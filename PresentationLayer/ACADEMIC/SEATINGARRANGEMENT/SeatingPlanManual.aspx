<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SeatingPlanManual.aspx.cs" Inherits="ACADEMIC_SEATINGARRANGEMENT_SeatingPlanManual" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    <script src="../../Content/jquery.js"></script>

     <script type="text/javascript">


         function CheckAllProgram(txtmain, headid) {
             CheckAllChild(txtmain, headid);
         }

         function CheckAllChild(txtmain, headid) {
             debugger;
             var studCount = 0;
             var txtSelectedStudsCount = document.getElementById('<%= txtSelectedStudStrengh.ClientID %>');
                var items = document.getElementsByClassName('item');
                var headerchk = document.getElementById('headeritem');
                var headerFirstChild = headerchk.firstElementChild.nextElementSibling.firstElementChild;
                if (headerFirstChild.checked) {
                    for (var i = 0; i < items.length; i++) {
                        items[i].firstElementChild.nextElementSibling.firstElementChild.checked = true;
                        items[i].lastElementChild.firstElementChild.disabled = false;
                        var CellData = items[i].firstElementChild.nextElementSibling.nextElementSibling.nextElementSibling.nextElementSibling.nextElementSibling.firstElementChild.innerHTML;
                        studCount += Number(CellData);
                    }
                    txtSelectedStudsCount.value = studCount
                }
                else {
                    for (var i = 0; i < items.length; i++) {
                        items[i].firstElementChild.nextElementSibling.firstElementChild.checked = false;
                        items[i].lastElementChild.firstElementChild.disabled = true;
                        items[i].lastElementChild.firstElementChild.value = "";
                        var CellData = items[i].firstElementChild.nextElementSibling.nextElementSibling.nextElementSibling.nextElementSibling.nextElementSibling.firstElementChild.innerHTML;
                        txtSelectedStudsCount.value = Number(txtSelectedStudsCount.value) - Number(CellData);

                    }
                    txtSelectedStudsCount.value = studCount
                }


            }

         function chkIndividualProgram(txtmain, headid) {
             debugger;
                var txtSelectedStudsCount = document.getElementById('<%= txtSelectedStudStrengh.ClientID %>');
                var hdnStudCount = document.getElementById('<%= hdStudCount.ClientID %>');
                var studCount = 0;
                debugger;
                var items = document.getElementsByClassName('item')
                    for (var i = 0; i < items.length; i++) {
                        if (items[i].firstElementChild.nextElementSibling.firstElementChild.checked) {
                        items[i].lastElementChild.firstElementChild.disabled = false;
                        var CellData = items[i].firstElementChild.nextElementSibling.nextElementSibling.nextElementSibling.nextElementSibling.nextElementSibling.firstElementChild.innerHTML;
                        console.log(CellData);
                        studCount += Number(CellData);
                    }
                    else {
                        items[i].lastElementChild.firstElementChild.disabled = true;
                        items[i].lastElementChild.firstElementChild.value = "";
                    }
                    txtSelectedStudsCount.value = studCount
                    hdnStudCount.value = studCount
            }
        }

        //validate form
        function validateForm() {
            debugger;

            var ddlRoom = document.getElementById('<%= ddlRoom.ClientID %>');

            if (ddlRoom.value == 0) {
                alert('Please Select Room !!!!!');
                return false;
            }
           
            var txtTot = document.getElementById('<%= txtTSelected.ClientID %>').value;

            if (txtTot == 0 || txtTot == '') {
                alert('Please Select Atleast One Student from Student List');
                return false;
            }
           
            var hdStudCount = document.getElementById('<%= hdStudCount.ClientID %>');
            var RoomStrength = document.getElementById('<%=hfroomcapacity.ClientID %>')
            var RemRoomStrength = document.getElementById('<%= txtRemRoomCapacity.ClientID %>')
            var TSelectedStudents = document.getElementById('<%= txtTSelected.ClientID %>')
            var txtSelectedStudsCount = document.getElementById('<%= txtSelectedStudStrengh.ClientID %>');
            var txtRoomCapacity = document.getElementById('<%= txtSelctedRommCap.ClientID %>');
           
            if (document.getElementById('<%= txtSelectedStudStrengh.ClientID %>').value != '' && document.getElementById('<%= txtRemRoomCapacity.ClientID %>').value != '') {
                if (Number(TSelectedStudents.value) > Number(RemRoomStrength.value)) {
                    alert('Selected Students Strength is exceeding the remaining room capacity.')
                    return false;
                }
            }
        };
      
    </script>


    <div style="z-index: 1; position: absolute; top: 75px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updplRoom"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updplRoom" runat="server">
        <ContentTemplate>

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
            </script>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title"><b>MANUAL SEATING ARRANGEMENT </b></h3>
                            <asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>
                            <div class="pull-right"><span style="font-weight:bold;color:red">Note : * Marked fields are mandatory</span></div>
                        </div>
                        <div class="box-body">
                          <div class="row">

                             <div class="col-md-12">
                               <div class="col-md-6">

                                <div class="col-md-12">
                                     <div class="form-group col-md-6">
                                        <label><span style="color: red;">*</span> Session  :</label>
                                        <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                            ValidationGroup="filter" Display="None" ErrorMessage="Please Select Session"
                                            SetFocusOnError="true" InitialValue="0" />
                                    </div>

                                     <div class="form-group col-md-6">
                                        <label><span style="color: red;">*</span>   Exam Date :  </label>
                                        <div class="input-group">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtExamDate" runat="server" TabIndex="3" ValidationGroup="submit" OnTextChanged="txtExamDate_TextChanged" AutoPostBack="true" />
                                            <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtExamDate" PopupButtonID="imgExamDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" TargetControlID="txtExamDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" EmptyValueMessage="Please Enter Exam Date"
                                                ControlExtender="meExamDate" ControlToValidate="txtExamDate" IsValidEmpty="false"
                                                InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Exam Date"
                                                InvalidValueBlurredMessage="*" ValidationGroup="configure" SetFocusOnError="true" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtExamDate"
                                            ValidationGroup="filter" Display="None" ErrorMessage="Please Select Exam Date"
                                            SetFocusOnError="true" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtExamDate"
                                            Display="None" ErrorMessage="Please Select  Exam Date"  ValidationGroup="configure"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    
                                     <div class="form-group col-md-6">
                                        <label><span style="color: red;">*</span>Exam Slot :</label>
                                        <asp:DropDownList ID="ddlslot" runat="server" TabIndex="1" AppendDataBoundItems="True"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlslot_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlslot"
                                            Display="None" ErrorMessage="Please Select Slot" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlslot"
                                            ValidationGroup="filter" Display="None" ErrorMessage="Please Select Exam Slot"
                                            SetFocusOnError="true" InitialValue="0" />
                                    </div>

                                     <div class="col-md-6 form-group">
                                        <label><span style="color: red;">*</span>Exam Name</label>
                                        <asp:DropDownList ID="ddlExamName" runat="server" AppendDataBoundItems="true"
                                            TabIndex="7" OnSelectedIndexChanged="ddlExamName_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                             <asp:ListItem Value="1">MID SEM</asp:ListItem>
                                             <asp:ListItem Value="2">END SEM</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExamName"
                                            ValidationGroup="Report" Display="None" ErrorMessage="Please Select Exam Name"
                                            SetFocusOnError="true" InitialValue="0" />
                                        <asp:RequiredFieldValidator ID="rfvExa" runat="server" ControlToValidate="ddlExamName"
                                            ValidationGroup="configure" Display="None" ErrorMessage="Please Select Exam Name"
                                            SetFocusOnError="true" InitialValue="0" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlExamName"
                                            ValidationGroup="filter" Display="None" ErrorMessage="Please Select Exam Name"
                                            SetFocusOnError="true" InitialValue="0" />
                                    </div>

                                     <div class="form-group col-md-6">
                                        <label>Course :</label>
                                        <asp:DropDownList ID="ddlCourse" runat="server" TabIndex="1" AppendDataBoundItems="True"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                     <div class="form-group col-md-6">
                                        <label><span style="color: red;">*</span>Room :</label>
                                        <asp:DropDownList ID="ddlRoom" runat="server" TabIndex="1" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlRoom_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvRoom" runat="server" ControlToValidate="ddlRoom"
                                            ValidationGroup="configure" Display="None" ErrorMessage="Please Select Room"
                                            SetFocusOnError="true" InitialValue="0" />--%>
                                      
                                    </div>

                                     <div class="form-group col-md-12" style="margin-top:25px"> 
                                        <label>Total Registered Students for Selected Course:</label>
                                        <div class="form-group col-md-3 pull-right">
                                            <asp:TextBox ID="txtSelectedStudStrengh" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                                            <asp:HiddenField ID="hdStudCount" runat="server" />
                                        </div>
                                     </div>

                                     <div class="form-group col-md-12"> 
                                        <label>Total Selected Students :</label>
                                        <div class="form-group col-md-3 pull-right">
                                            <asp:TextBox ID="txtTSelected" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                                        </div>
                                     </div>

                                    
                                     <div class="form-group col-md-12"> 
                                        <label>Selected Room Capacity :</label>
                                        <div class="form-group col-md-3 pull-right">
                                          <asp:TextBox ID="txtSelctedRommCap" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                                            <asp:HiddenField ID="hfroomcapacity" runat="server" />
                                        </div>
                                     </div>

                                     <div class="form-group col-md-12"> 
                                        <label>Remaining Room Capacity :</label>
                                        <div class="form-group col-md-3 pull-right">
                                          <asp:TextBox ID="txtRemRoomCapacity" runat="server" Width="100px" Enabled="false"></asp:TextBox>
                                            <asp:HiddenField ID="hdnRemRoomCapacity" runat="server" />
                                        </div>
                                     </div>
                                   
                                </div>
                               
                              </div>
                              <div class="col-md-6" >
                                    <asp:Panel ID="pnlProgramName" runat="server"  Visible="false" ScrollBars="Auto" Height="400px">
                                        <div class="panel panel-body" style="margin-top:-35px">
                                            <div class="panel panel-heading" style="text-align: left">
                                                <h5><b>Course List</b></h5>
                                            </div>
                                                <asp:ListView ID="lvProgramNames" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="table-responsive">
                                                        <table class="table table-hover table-bordered table-strip " border="1" id="tblProgramesName">
                                                            <thead>                                                                  
                                                                <tr class="bg-light-blue" id="headeritem">                                                                 
                                                                    <th>Sr No</th>
                                                                    <th style="width:10px">Select All
                                                                    <asp:CheckBox ID="chckallProgram" runat="server" Checked="false" onclick="SelectAllCourse(this,1,'chckProgram');" AutoPostBack="true" OnCheckedChanged="chckallProgram_CheckedChanged" />
                                                                    </th>
                                                                    <th>Branch
                                                                    </th>
                                                                    <th>Sem
                                                                    </th>
                                                                    <th>Course
                                                                    </th>
                                                                    <th style="width:5px">Reg.Students</th>                                                             
                                                                </tr>  
                                                                </thead>
                                                            <tbody>                                                                  
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                       </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="item">
                                                             <td><%#Container.DataItemIndex+1 %></td>
                                                             <td class="text-center" style="width:10px">
                                                                <asp:CheckBox ID="chckProgram" runat="server" onclick="chkIndividualProgram(this,1);" AutoPostBack="true" OnCheckedChanged="chckProgram_CheckedChanged" />                                                               
                                                                <asp:HiddenField ID="hfSchemeno" runat="server" Value='<%# Eval("BRANCHNO") %>' />                                                                
                                                            </td>
                                                            <td>                                                               
                                                                <asp:Label ID="lblSchemeName" runat="server" Text='<%# Eval("SHORTNAME")%>' ToolTip='<%# Eval("BRANCHNO") %>'/>
                                                            </td>
                                                            <td>                                                               
                                                                <asp:Label ID="lblSem" runat="server" Text='<%# Eval("SEMESTERNAME")%>' ToolTip='<%# Eval("SEMESTERNO") %>'/>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblExamCourse" runat="server" Text='<%# Eval("COURSES")%>' ToolTip='<%# Eval("COURSENO") %>' />
                                                                <asp:Label ID="lblCcode" Visible="false" runat="server" Text='<%# Eval("CCODE")%>' ToolTip='<%# Eval("DEGREENO")%>'/>
                                                            </td>
                                                             <td style="width:5px">                                                               
                                                                <asp:Label ID="lblStudCount" runat="server" Text='<%# Eval("STUDCOUNT")%>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                        </div>
                                    </asp:Panel>
                              </div>
                            </div>
                               <div style="text-align:center">
                                   <asp:Button ID="btnFilter" runat="server" Text="Filter Students" Width="120px" ValidationGroup="filter"
                                        TabIndex="8" CssClass="btn btn-primary" OnClick="btnFilter_Click"/>
                                    <asp:Button ID="btnConfigure" runat="server" Text="Configure" Width="90px" ValidationGroup="configure" Visible="false"
                                        TabIndex="8" CssClass="btn btn-success" OnClick="btnConfigure_Click" OnClientClick="return validateForm(this);" />
                                      <asp:Button ID="btnPDFReport" runat="server" Text="Report" ValidationGroup="configure" OnClick="btnPDFReport_Click"
                                        Width="90px" TabIndex="15"  CssClass="btn btn-info" />
                                    <asp:Button ID="btnReport" runat="server" Text="Excel Report" ValidationGroup="configure" OnClick="btnReport_Click"
                                        Width="120px" TabIndex="15"  CssClass="btn btn-info" />
                                    <asp:Button ID="btnDeallocate" runat="server" CssClass="btn btn-primary" Text="Deallocate" ValidationGroup="process"
                                        Width="90px" TabIndex="15" Visible="false" OnClick="btnDeallocate_Click" />
                                    <asp:Button ID="btnClear" runat="server" Width="100px" CssClass="btn btn-danger" OnClick="btnClear_Click"
                                        Text="Cancel" />
                                    <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="configure" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="filter" />
                                </div>


                          </div>
                           
                            <div class="box-footer">

                                 <div class="col-md-12">
                                            <asp:Panel ID="pnlFilteredStudent" runat="server" Height="400px" ScrollBars="Auto" Visible="false">
                                                <asp:ListView ID="lvFilteredStudent" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="demo-grid">
                                                            <h5><b>STUDENTS LIST </b></h5>
                                                            <table class="table table-hover table-bordered table-responsive" id="tblFilteredStudent">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th style="width:45px"><asp:CheckBox ID="chkAllFilteredStudent" runat="server" Text="Select All" onclick="SelectAllFilteredStudent(this,2,'chkFilteredStudent');" /></th>
                                                                        <th>Enrollment No </th>
                                                                        <th>Student Name </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <td style="width:45px;text-align:center"><asp:CheckBox ID="chkFilteredStudent" runat="server" onclick="totSTudents(this)"  /></td>
                                                        <td><%# Eval("REGNO")%></td>
                                                        <td><asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDNAME")%>' ToolTip='<%# Eval("IDNO") %>'></asp:Label></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                 </div>
                                       
                            </div>
                      
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">
        function SelectAllCourse(headerid, headid, chk) {
            debugger;
            var ddl = document.getElementById('<%= ddlCourse.ClientID %>');
            if (headerid.checked) {
                ddl.disabled = true;
            }
            else {
                ddl.disabled = false;
            }

            var tbl = '';
            var list = '';
            if (headid == 1) {
                tbl = document.getElementById('tblProgramesName');
                list = 'lvProgramNames';
            }

            try {
                var dataRows = tbl.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        var chkid = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_' + chk;
                        if (headerid.checked) {
                            document.getElementById(chkid).checked = true; 
                        }
                        else {
                            document.getElementById(chkid).checked = false;
                        }
                        chkid = '';
                    }
                }
            }
            catch (e) {
            }

            CheckAllProgram(this, headid);
        } 
    </script>

    <script type="text/javascript">
        function SelectAllFilteredStudent(headerid, headid, chk) {
            var tbl = '';
            var list = '';
            if (headid == 2) {
                tbl = document.getElementById('tblFilteredStudent');
                list = 'lvFilteredStudent';
            }

            try {
                var dataRows = tbl.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        var chkid = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_' + chk;
                        if (headerid.checked) {
                            document.getElementById(chkid).checked = true;
                        }
                        else {
                            document.getElementById(chkid).checked = false;
                        }
                        chkid = '';
                    }
                }
            }
            catch (e) {
            }

            totAllSTudents(headerid);
        }
    </script>

     <script type="text/javascript" language="javascript">

         function totSTudents(chk) {
             debugger;
             var txtTot = document.getElementById('<%= txtTSelected.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;

         }
      </script>

     <script type="text/javascript" language="javascript">

         function totAllSTudents(headerid) {
             debugger;
             var txtTot = document.getElementById('<%= txtTSelected.ClientID %>');
             var tbl = '';
             var list = '';
             tbl = document.getElementById('tblFilteredStudent');
             list = 'lvFilteredStudent';

             try {
                 var dataRows = tbl.getElementsByTagName('tr');
                 if (dataRows != null) {
                     if (headerid.checked) {
                         txtTot.value = 0;
                     }
                     for (i = 0; i < dataRows.length - 1; i++) {
                         if (headerid.checked) {
                             txtTot.value = Number(txtTot.value) + 1;
                         }
                         else {
                             txtTot.value = Number(txtTot.value) - 1;
                         }
                         chkid = '';
                     }

                     if (headerid.checked == false) {
                         txtTot.value = 0;
                     }
                 }
             }
             catch (e) {
             }

         }
      </script>

    <script type="text/javascript">
        function validateAssign() {
            debugger;
            //var ddlRoom = document.getElementById('<%= ddlRoom.ClientID %>');

            //if (ddlRoom.value == 0) {
               // alert('Please Select Room !!!!!');
               // return false;
            //}
            //else
              //  return true;

            var txtTot = document.getElementById('<%= txtTSelected.ClientID %>').value;

            if (txtTot == 0) {
                alert('Please Select Atleast One Student from Student List');
                return false;
            }
            else
                return true;
        }
    </script>

</asp:Content>

