<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MarksCorrection.aspx.cs" Inherits="ACADEMIC_EXAMINATION_MarksCorrection" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updUpdate"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

      <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title"><b>MARKS CORRECTION</b></h3>
                       <div class="pull-right">
                                <div style="color: Red; font-weight: bold;">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are Mandatory
                                </div>
                            </div>
                    </div>
                    <asp:UpdatePanel ID="updUpdate" runat="server">
                        <ContentTemplate>

                            <div class="box-body">
                                <div class="col-md-12">
                                    <div class="form-group col-md-4">
                                        <label><span style="color: red">*</span>Session</label>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-4">
                                       <label><span style="color: red;">*</span>Exam Name :</label>
                                        <asp:DropDownList ID="ddlExamName" runat="server"  AppendDataBoundItems="true"  TabIndex="10">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">MID SEM</asp:ListItem>
                                            <asp:ListItem Value="2">END SEM</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvExamName" runat="server" ControlToValidate="ddlExamName"
                                            Display="None" ErrorMessage="Please Select Exam Name" InitialValue="0" ValidationGroup="show">
                                        </asp:RequiredFieldValidator>
                                  </div>

                                 <div class="col-md-4">
                                            <fieldset class="fieldset" style="padding: 3px; color: Green">
                                                <legend class="legend">Note</legend>
                                                <span style="font-weight: bold; color: Red;">Please Save and Lock Marks
                                                </span>
                                                <br />
                                                <b>Enter :<br />
                                                    "-1" for Absent Student<br />
                                                    "-2" for UFM(Copy Case)<br />
                                                    "-3" for WithDraw Student<br />
                                                    "-4" for Drop Student<br />
                                                </b>
                                            </fieldset>
                                   </div>

                                 <div class="form-group col-md-4" style="margin-top:-85px">
                                       <label><span style="color: red;">*</span>Enrollment No :</label>
                                       <asp:TextBox ID="txtEnrollmentNo" runat="server" class="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEnrollmentNo"
                                            Display="None" ErrorMessage="Please Enter Enrollment No." ValidationGroup="show">
                                        </asp:RequiredFieldValidator>
                                  </div>

                               </div>
                            
                            </div>
                            <div class="box-footer"> 
                                </p>
                                <div class="col-md-12">
                                    <p class="text-center">
                                        <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" ValidationGroup="show"
                                            Text="Show" CssClass="btn btn-info" />

                                         <asp:Button ID="btnSave" Visible="false" runat="server" OnClick="btnSave_Click" ValidationGroup="save"
                                            Text="Save" CssClass="btn btn-info" />

                                          <asp:Button ID="btnLock" Visible="false" runat="server" OnClick="btnLock_Click" ValidationGroup="save" OnClientClick="return showLockConfirm(this,'val');" 
                                            Text="Lock" CssClass="btn btn-info" />

                                         <asp:Button ID="btnClear" Visible="false" runat="server" OnClick="btnClear_Click"
                                            Text="Clear" CssClass="btn btn-success" />
                                    </p>
                                    <p>
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="show" />
                                    </p>
   
                                       <div class="col-md-12">
                                         <asp:Panel ID="pnlStudent" runat="server" ScrollBars="Auto" style="margin-top:25px">
                                            <asp:ListView ID="lvStudent" runat="server" OnItemDataBound="lvStudent_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                           <%# Container.DataItemIndex+1 %>
                                                        </td>

                                                        <td style="text-align:center">
                                                           <asp:Label ID="lblRegno" runat="server" Text='<%# Eval("REGNO")%>' ToolTip='<%# Eval("idno") %>'></asp:Label>
                                                        </td>
                                                         <td style="text-align:center">
                                                            <%# Eval("STUDNAME")%>
                                                        </td>
                                                         <td style="text-align:center">
                                                           <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("COURSENAME")%>' ToolTip='<%# Eval("COURSENO") %>'></asp:Label>
                                                        </td>
                                                         <td style="text-align:center">
                                                            <%# Eval("MAXMARKS")%>
                                                        </td>
                                                         <td style="text-align:center">
                                                            <asp:TextBox ID="txtOp1Marks" runat="server" onblur="return CheckMark(this);" Text='<%# Eval("OP1_MARKS") %>' Enabled='<%# (Eval("OP1_LOCK").ToString() == "1") ? false : true %>'></asp:TextBox>         
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtTutMarks" runat="server" FilterType="Custom"
                                                                ValidChars="0123456789-." TargetControlID="txtOp1Marks">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:CompareValidator ID="comTutMarks" ValueToCompare='<%# Bind("MAXMARKS") %>' ControlToValidate="txtOp1Marks"
                                                                Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                                                ValidationGroup="save" Text="*"></asp:CompareValidator>
                                                            <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comTutMarks" ID="cecomTutMarks" runat="server">
                                                            </ajaxToolKit:ValidatorCalloutExtender>
                                                        </td>
                                                          <td style="text-align:center">
                                                           <asp:TextBox ID="txtOp2Marks" runat="server" onblur="return CheckMark(this);" Text='<%# Eval("OP2_MARKS") %>' Enabled='<%# (Eval("LOCK").ToString() == "1") ? false : true %>'></asp:TextBox>  
                                                           <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                                                                ValidChars="0123456789-." TargetControlID="txtOp2Marks">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:CompareValidator ID="CompareValidator1" ValueToCompare='<%# Bind("MAXMARKS") %>' ControlToValidate="txtOp2Marks"
                                                                Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                                                ValidationGroup="save" Text="*"></asp:CompareValidator>
                                                            <ajaxToolKit:ValidatorCalloutExtender TargetControlID="CompareValidator1" ID="ValidatorCalloutExtender1" runat="server">
                                                            </ajaxToolKit:ValidatorCalloutExtender>
                                                              <asp:HiddenField ID="hdnOp1Lock" runat="server" Value='<%# Eval("OP1_LOCK") %>' />
                                                              <asp:HiddenField ID="hdnOp2Lock" runat="server" Value='<%# Eval("LOCK") %>' />
                                                        </td>
                                                    </tr>
                                                   
                                                </ItemTemplate>
                                                <LayoutTemplate>
                                                    <div id="listViewGrid">
                                                        <table class="table table-hover table-bordered table-responsive">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th style="text-align:center">Sr.No.
                                                                    </th>
                                                                    <th style="text-align:center">Enrollment No.
                                                                    </th>
                                                                    <th style="text-align:center">Student Name
                                                                    </th>
                                                                    <th style="text-align:center">Course Name
                                                                    </th>
                                                                    <th style="text-align:center">Max. Marks
                                                                    </th>
                                                                      <th style="text-align:center">Marks Entered by Faculty / Operator 1
                                                                    </th>
                                                                      <th style="text-align:center">Marks Entered by Operator 2
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>      
                                                    </div>
                                                </LayoutTemplate>
                                            </asp:ListView>    
                                        </asp:Panel>
                                       </div> 
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                </div>
              
            </div>
           
        </div>
     

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">
        function CheckMark(id) {
            if (id.value < 0) {
                if (id.value == -1 || id.value == -2 || id.value == -3 || id.value == -4) {
                }
                else {
                    alert("Marks Below Zero are Not allowed. Only -1,-2,-3,-4 can be entered.");
                    id.value = '';
                    id.focus();
                }
            }
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
                    var ret2 = confirm('You are about to lock entered marks, be sure before locking.\n\nOnce Locked it cannot be modified or changed. \n\nClick OK to Continue....');
                    if (ret2 == true) {
                        validate = true;
                    }
                    else
                        validate = false;
                }
                else
                    validate = false;
            }
            return validate;
        }
    </script>

</asp:Content>

