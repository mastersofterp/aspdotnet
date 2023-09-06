<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="RevalMarkChange.aspx.cs" Inherits="ACADEMIC_EXAMINATION_RevalMarkChange" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMain" runat="server" Visible="true">

        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title"><b>Reval Mark Entry</b></h3>
                        <div class="box-tools pull-right">
                            <div style="color: Red; font-weight: bold">
                                &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                            </div>
                        </div>
                    </div>

                    <%--<div class="box-header with-border">
                 <h3 class="box-title"><b>Result Analysis Report</b></h3>
                      <div class="pull-right">
                   <div style="color: Red; font-weight: bold;">
                            &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                        </div>
                </div>
                        </div>--%>

                    <div class="box-body">

                        <div class="col-md-9">
                            <%--<div class="form-group col-md-12">
                                <asp:RadioButtonList ID="rblRevalStatus" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="REVAL">Revaluation Entry</asp:ListItem>
                                    <asp:ListItem Value="SCRUTINY">Scrutiny Entry</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>--%>
                            <div class="form-group col-md-6">
                                <%--<label>Session</label>--%>
                                <span style="color: Red">*</span>
                                <label>Session :</label>
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" AutoPostBack="true" Font-Bold="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvsessionrpt" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-6">
                                <%--<label>Scheme</label>--%>
                                <label><span style="color: Red">*</span>Scheme :</label>
                                <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="True"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPath" runat="server" ControlToValidate="ddlScheme"
                                    Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" SetFocusOnError="True" ValidationGroup="show"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvscheme" runat="server" ControlToValidate="ddlScheme"
                                    Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Report"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-6">
                                <%--<label>Semester</label>--%>
                                <label><span style="color: Red">*</span>Semester :</label>
                                <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="show"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvSemester0" runat="server" ControlToValidate="ddlSemester"
                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Report"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-6">
                                <%--<label>Course</label>--%>
                                <label><span style="color: Red">*</span>Course :</label>
                                <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="True"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                    Display="None" ErrorMessage="Please Select Course" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="show"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvCourse0" runat="server" ControlToValidate="ddlCourse"
                                    Display="None" ErrorMessage="Please Select Course" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Report"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-6">
                                <%--<label>Exam</label>--%>
                                <label><span style="color: Red">*</span>Exam :</label>
                                <asp:DropDownList ID="ddlExam" runat="server" AppendDataBoundItems="True" Enabled="false"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlExam_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExam"
                                    Display="None" ErrorMessage="Please Select Exam" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="show"></asp:RequiredFieldValidator>
                            </div>
                           <%-- <div class="form-group col-md-6">
                                <%--<label>Valuer</label>
                                <label><span style="color: Red">*</span>Valuer :</label>
                                <asp:DropDownList ID="ddlValuer" runat="server" AppendDataBoundItems="True" Width="55%"
                                    ValidationGroup="show" AutoPostBack="true" OnSelectedIndexChanged="ddlValuer_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="V1">Valuer-1</asp:ListItem>
                                    <asp:ListItem Value="V2">Valuer-2</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvValuer" runat="server" ControlToValidate="ddlValuer"
                                    Display="None" ErrorMessage="Please Select Valuer" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="show"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvValuer0" runat="server" ControlToValidate="ddlValuer"
                                    Display="None" ErrorMessage="Please Select Valuer" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Report"></asp:RequiredFieldValidator>
                            </div>--%>
                        </div>

                        <div class="col-md-3">
                            <fieldset class="fieldset" style="padding: 5px; color: Green">
                                <legend class="legend">Note</legend>
                                <span style="font-weight: bold; color: Red;">
                                    <br />
                                    Please Save and Lock for
                                    Final Submission of Marks</span>
                                <br />
                                <b>Enter :<br />
                                   
                                    "-1" for Absent Student<br />
                                   
                                    <%--"-2" for Not Eligible Student --%></b>
                            </fieldset>

                        </div>

                    </div>
                    <div class="box-footer">
                        <p class="text-center">
                            <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show"
                                ValidationGroup="show" Font-Bold="True" CssClass="btn btn-primary" />

                            <asp:Button ID="btnSave" runat="server" Visible="false" Text="Save"
                                OnClick="btnSave_Click" Font-Bold="True" ValidationGroup="val" CssClass="btn btn-primary" />

                            <asp:Button ID="btnLock" runat="server" Visible="false" OnClientClick="return showLockConfirm();"
                                Text="Lock" OnClick="btnLock_Click" Font-Bold="True" ValidationGroup="val" CssClass="btn btn-primary" />

                            <asp:Button ID="btnUnlock" runat="server" Visible="false" Font-Bold="True" OnClientClick="return showUnLockConfirm();"
                                Text="UnLock" OnClick="btnUnlock_Click" CssClass="btn btn-primary" />

                            <asp:Button ID="btnreport" runat="server" Font-Bold="True" Text="Report"
                                OnClick="btnreport_Click" ValidationGroup="Report" CssClass="btn btn-info" />

                            <asp:Button ID="btnCancel2" runat="server" Text="Cancel" OnClick="btnCancel2_Click"
                                Font-Bold="True" CssClass="btn btn-warning" />

                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="show" />
                            <p>
                                &nbsp;<asp:ValidationSummary ID="validationreport" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Report" />
                                <div class="col-md-12">
                                    <asp:Panel ID="pnlStudGrid" runat="server" ScrollBars="Auto" Visible="false">
                                        <div id="demo-grid">
                                            <h4>Enter Marks for following Students</h4>
                                            <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Height="15px" HeaderStyle-HorizontalAlign="Center" HeaderText="Sr.No." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <HeaderStyle Height="15px" HorizontalAlign="Center" />
                                                        <ItemStyle Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Reg No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIDNO" runat="server" Text='<%# Bind("REGNO") %>' ToolTip='<%# Bind("IDNO") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="STUDNAME" HeaderStyle-HorizontalAlign="Left" HeaderText="Student Name" ItemStyle-CssClass="gridItem" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                    </asp:BoundField>
                                                    <%--EXAM MARK ENTRY--%>
                                                    <asp:TemplateField HeaderStyle-Height="50px" HeaderStyle-HorizontalAlign="Center" HeaderText="OLD MARK" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOldMarks" runat="server" Style="text-align: center; margin-left: 55px; font-weight: bold;" Text='<%# Bind("MARK") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Height="50px" HeaderStyle-HorizontalAlign="Center" HeaderText="VALUER MARK" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtVMrk" runat="server" Font-Bold="true" MaxLength="4" onsubmit="return confirmInput(this)" onkeyup="return CheckMark(this);"  Style="text-align: center; margin-left: 55px; margin-top: 20px" Text='<%# Bind("VMARK").ToString() == "0.00" ? string.Empty : Bind("VMARK") %>' Width="80px" />
                                                            <asp:Label ID="lblV1Marks" runat="server" ToolTip='<%# Bind("LOCKV") %>' Visible="false" />
                                                            <asp:Label ID="lblMarks" runat="server" Text='<%# Bind("SMAX") %>' ToolTip='<%# Bind("LOCKV") %>' Visible="false" />
                                                            <%--<asp:Label ID="lblMinMarks" runat="server" Text='<%# Bind("SMIN") %>' Visible="false" />--%>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtV1Marks" runat="server" FilterType="Custom" TargetControlID="txtVMrk" ValidChars="0123456789.-">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:CompareValidator ID="comV1Marks" runat="server" ControlToValidate="txtVMrk" ErrorMessage="Marks should be less than or equal to max marks" Operator="LessThanEqual" Text="*" Type="Double" ValidationGroup="val" ValueToCompare='<%# Bind("SMAX") %>'>
                                                            </asp:CompareValidator>
                                                            <ajaxToolKit:ValidatorCalloutExtender ID="cecomV1Marks" runat="server" TargetControlID="comV1Marks">
                                                            </ajaxToolKit:ValidatorCalloutExtender>
                                                            <asp:CompareValidator ID="cvAbsentStud1" runat="server" ControlToValidate="txtVMrk" ErrorMessage="-1 for absent student" Operator="NotEqual" Text="*" Type="Double" ValidationGroup="val1" ValueToCompare="-1">
                                                            </asp:CompareValidator>
                                                            <ajaxToolKit:ValidatorCalloutExtender ID="vceAbsentStud1" runat="server" TargetControlID="cvAbsentStud1">
                                                            </ajaxToolKit:ValidatorCalloutExtender>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                <RowStyle ForeColor="#000066" />
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </p>
                        </p>

                    </div>
                </div>
            </div>
        </div>


        <div id="divMsg" runat="server">
        </div>
    </asp:Panel>

    
<script>
    function confirmInput(id) {
        //fname = document.forms[0].fname.value;
        //txtVMrk.text = document.getElementById("txtVMrk")value;
        if (id.value = 0.00)
            alert("Empty Marks are not allowed!");
    }
</script>

    <script language="javascript" type="text/javascript">

        function CheckMark(id) {
            //alert(id.value);
            if (id.value < 0) {
                if (id.value == -1)  ///id.value != -2 ||
                {
                    //alert("You have marked student as ABSENT.");
                }
                else {
                    alert("Marks Below Zero are Not allowed. Only -1 can be entered.");
                    id.value = '';
                    id.focus();
                }
            }
            //if (id.value = 0.00) {
            //    alert("cannot enter null");
            //    id.validate = '';
            //    id.focus();
            //}
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
