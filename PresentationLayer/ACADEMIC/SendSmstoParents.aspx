<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendSmstoParents.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_SendSmstoParents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

    <script type="text/javascript" language="javascript">

        function SelectAllFirst(chk) {
            debugger;
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
               var hftot = document.getElementById('<%= hftot.ClientID %>');
               for (i = 0; i < hftot.value; i++) {

                   var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvfirstsms_ctrl' + i + '_cbRow');
                   if (lst.type == 'checkbox') {
                       if (chk.checked == true) {
                           lst.checked = true;
                           txtTot.value = hftot.value;
                       }
                       else {
                           lst.checked = false;
                           txtTot.value = 0;
                       }
                   }
               }
           }


           function SelectAllSecond(chk) {
               var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
               var hftot = document.getElementById('<%= hftot.ClientID %>');
               for (i = 0; i < hftot.value; i++) {

                   var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvattendancesecondsms_ctrl' + i + '_cbRow');
                   if (lst.type == 'checkbox') {
                       if (chk.checked == true) {
                           lst.checked = true;
                           txtTot.value = hftot.value;
                       }
                       else {
                           lst.checked = false;
                           txtTot.value = 0;
                       }
                   }
               }
           }


           function SelectAllThird(chk) {
               var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
               var hftot = document.getElementById('<%= hftot.ClientID %>');
               for (i = 0; i < hftot.value; i++) {

                   var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvmarks_ctrl' + i + '_cbRow');
                   if (lst.type == 'checkbox') {
                       if (chk.checked == true) {
                           lst.checked = true;
                           txtTot.value = hftot.value;
                       }
                       else {
                           lst.checked = false;
                           txtTot.value = 0;
                       }
                   }
               }
           }

           function totSubjects(chk) {
               var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

               if (chk.checked == true)
                   txtTot.value = Number(txtTot.value) + 1;
               else
                   txtTot.value = Number(txtTot.value) - 1;

           }


    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updDetained"
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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Send SMS to Parents</h3>
                </div>

                <div class="box-body">
                    <asp:UpdatePanel ID="updDetained" runat="server">
                        <ContentTemplate>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-4 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <label>Format</label>
                                        </div>
                                        <asp:RadioButtonList ID="rdbFormat" runat="server" TabIndex="1" RepeatDirection="vertical" OnSelectedIndexChanged="rdbFormat_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem>&nbsp;Attendance First SMS&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem>&nbsp;Attendance Second SMS&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem>&nbsp;SMS for IA Format&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvformat" runat="server" ControlToValidate="rdbFormat"
                                            Display="None" ErrorMessage="Please Select Format" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-8 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note </h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>For Attendance Second SMS Please Select - <span style="color: green;font-weight:bold">Session->Institute->Degree->Branch->Scheme->Semester</span></span></p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>For SMS for IA Format  Please Select - <span style="color: green;font-weight:bold">Session->Institute->Degree->Exam Name</span></span></p>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="2" Font-Bold="True" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Institute Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlcollege" runat="server" AppendDataBoundItems="True" TabIndex="3" ToolTip="Please Select Institute" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlcollege"
                                            Display="None" ErrorMessage="Please Select Institute" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="True" TabIndex="4" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="report" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" AutoPostBack="True" TabIndex="5" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="report" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divscheme" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" AutoPostBack="true" TabIndex="6" ValidationGroup="report" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" TabIndex="6" ValidationGroup="report" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divexamname" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Exam Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlIAMarks" runat="server" AppendDataBoundItems="True" TabIndex="6" ValidationGroup="report" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="S1">CIE-1</asp:ListItem>
                                            <asp:ListItem Value="S2">CIE-2</asp:ListItem>
                                            <asp:ListItem Value="S3">CIE-3</asp:ListItem>
                                            <asp:ListItem Value="S4">EVENT-1</asp:ListItem>
                                            <asp:ListItem Value="S5">EVENT-2</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvIAMark" runat="server" ControlToValidate="ddlIAMarks"
                                            Display="None" ErrorMessage="Please Select Exam Name" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShowStudentlist" runat="server" Text="Show Students" ValidationGroup="report" TabIndex="7"
                                    CssClass="btn btn-primary" OnClick="btnShowStudentlist_Click" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Send SMS" CssClass="btn btn-primary" TabIndex="8"
                                    ValidationGroup="submit" OnClick="btnSubmit_Click" />
                                <asp:Button ID="butCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="9" OnClick="butCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <label>Total Selected</label>
                                        </div>
                                        <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False" CssClass="form-control"
                                            Style="text-align: center;" Font-Bold="True" Font-Size="Small"></asp:TextBox>
                                        <%--  Reset the sample so it can be played again --%>
                                        <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtTotStud"
                                            WatermarkText="0" WatermarkCssClass="watermarked" Enabled="True" />
                                        <asp:HiddenField ID="hftot" runat="server" />
                                    </div>
                                </div>
                            </div>
                            
                            <div class="col-md-12" runat="server" id="divFirstsms">
                                <asp:Panel ID="pnlfirst" runat="server" Visible="false">
                                    <asp:ListView ID="lvfirstsms" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Student List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sl No.</th>
                                                        <th align="center">
                                                            <asp:CheckBox ID="cbHead" runat="server" Text="Select" onClick="SelectAllFirst(this);" TabIndex="10" />
                                                        </th>
                                                        <th>USN No</th>
                                                        <th>Student Name </th>
                                                        <th>Father Mobile Number</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Container.DataItemIndex+1 %></td>
                                                <td align="center">
                                                    <asp:CheckBox ID="cbRow" runat="server" onClick="totSubjects(this);" TabIndex="11" />
                                                </td>
                                                <td><%# Eval("ENROLLNO")%></td>
                                                <asp:HiddenField ID="hdnidno" Value='<%# Eval("IDNO")%>' runat="server" />
                                                <asp:HiddenField ID="hdnenroll" Value='<%# Eval("ENROLLNO")%>' runat="server" />
                                                <td><%# Eval("STUDNAME")%></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblParMobile" Text='<%# Eval("FATHERMOBILE")%>'>  </asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div class="col-md-12" runat="server" id="divattendancesecondsms">
                                <asp:Panel ID="pnlsecond" runat="server" Visible="false">
                                    <asp:ListView ID="lvattendancesecondsms" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Student List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sl No.</th>
                                                        <th align="center">
                                                            <asp:CheckBox ID="cbHead" runat="server" Text="Select" onClick="SelectAllSecond(this);" TabIndex="10" />
                                                        </th>
                                                        <th>University No</th>

                                                        <th>Student Name </th>
                                                        <th>Father Mobile Number</th>
                                                        <th>Registered Subjects List</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Container.DataItemIndex+1 %></td>
                                                <td align="center">
                                                    <asp:CheckBox ID="cbRow" runat="server" onClick="totSubjects(this);" TabIndex="11" />
                                                </td>

                                                <td><%# Eval("ENROLLMENT_NO")%></td>
                                                <asp:HiddenField ID="hdnidno" Value='<%# Eval("IDNO")%>' runat="server" />
                                                <asp:HiddenField ID="hdnenroll" Value='<%# Eval("ENROLLMENT_NO")%>' runat="server" />
                                                <asp:HiddenField ID="hdnsemesterno" Value='<%# Eval("SEMESTERNO")%>' runat="server" />
                                                <asp:HiddenField ID="hdndeptname" Value='<%# Eval("DEPTNAME")%>' runat="server" />
                                                <td>
                                                    <asp:Label runat="server" ID="lblname" Text='<%# Eval("STUDENT_NAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblParMobile" Text='<%# Eval("FATHERMOBILE")%>'>  </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblstuattendance" Text='<%# Eval("DETAIL_COURSE2")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>            
                                        
                            <div class="col-md-12" runat="server" id="divIAMarks">
                                <asp:Panel ID="pnlthird" runat="server" Visible="false">
                                    <asp:ListView ID="lvmarks" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Student List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sl No.</th>
                                                        <th align="center">
                                                            <asp:CheckBox ID="cbHead" runat="server" Text="Select" onClick="SelectAllThird(this);" TabIndex="11" />
                                                        </th>
                                                        <th>University No</th>
                                                        <th>Student Name </th>
                                                        <th>Father Mobile Number</th>
                                                        <th>Registered Subjects List</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Container.DataItemIndex+1 %></td>
                                                <td align="center">
                                                    <asp:CheckBox ID="cbRow" runat="server" onClick="totSubjects(this);" TabIndex="11" />
                                                </td>
                                                <td><%# Eval("ENROLLNO")%></td>
                                                <asp:HiddenField ID="hdnidno" Value='<%# Eval("IDNO")%>' runat="server" />
                                                <asp:HiddenField ID="hdnenroll" Value='<%# Eval("ENROLLNO")%>' runat="server" />
                                                <asp:HiddenField ID="hdnsemesterno" Value='<%# Eval("SEMESTERNO")%>' runat="server" />
                                                <td>
                                                    <asp:Label runat="server" ID="lblname" Text='<%# Eval("STUDNAME")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblParMobile" Text='<%# Eval("FATHERMOBILE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblIAMarks" Text='<%# Eval("DETAILED_COURSE")%>'></asp:Label></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>             
                            <div id="divMsg" runat="server"></div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
