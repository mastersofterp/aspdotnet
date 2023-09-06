<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="ExternalDoubleMarkEntry.aspx.cs" Inherits="ACADEMIC_ExternalDoubleMarkEntry" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 75px; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updSection"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();

        }

        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });

        }
    </script>
    <script type="text/javascript">
        function chkmax(txt) {
            debugger;
            var id = 0; var s1 = 0;
            id = document.getElementById('hdS1max').value;
            s1 = document.getElementById('txtTotMarks').value;
            if (s1 > id) {
                alert('mark should not be greather than max mark');
            }

        }

    </script>
    <script src="../Content/jquery.js" type="text/javascript"></script>

    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <%--<script type="text/javascript" charset="utf-8">
        $(document).ready(function () {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });

    </script>--%>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>



    <asp:Panel ID="pnlMain" runat="server">
        <asp:UpdatePanel ID="updSection" runat="server">
            <ContentTemplate>

                <div class="row">

                    <div class="col-sm-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><b>MARKS ENTRY BY OPERATOR</b></h3>
                              <%--  <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                    AlternateText="Page Help" ToolTip="Page Help" />--%>
                                <div class="pull-right">
                                    <div style="color: Red; font-weight: bold">
                                        Note : * Marked fields are mandatory
                                    </div>
                                </div>
                            </div>


                            <div class="box-body">
                              <div class="col-md-12">
                                  <div class="col-md-4">
                                     <label><span style="color: red;">*</span>Session :</label>
                                            <asp:DropDownList ID="ddlSession"  runat="server" class="form-group"  TabIndex="1" ValidationGroup="teacherallot">
                                                <asp:ListItem>Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSession"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator> 
                                  </div>

                                  <div class="col-md-4">
                                       <label><span style="color: red;">*</span>Exam Name :</label>
                                        <asp:DropDownList ID="ddlExamName" runat="server" class="form-group"  AppendDataBoundItems="true"  TabIndex="10" AutoPostBack="true" OnSelectedIndexChanged="ddlExamName_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">MID SEM</asp:ListItem>
                                            <asp:ListItem Value="2">END SEM</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvExamName" runat="server" ControlToValidate="ddlExamName"
                                            Display="None" ErrorMessage="Please Select Exam Name" InitialValue="0" ValidationGroup="teacherallot">
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
                              </div> 


                            <div class="text-center">
                            </div>


                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="teacherallot"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />


                            <div class="box-footer col-sm-12">
                               <asp:Panel ID="pnlDetails" runat="server" Visible="false">
                                     <div class="col-sm-12 table table-responsive" style="margin-top: 1px">
                                    <div>
                                        <asp:Panel ID="pnlStudent" runat="server" ScrollBars="auto">
                                            <asp:ListView ID="lvCourse" runat="server">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <h4>Course List </h4>
                                                        <table class="table table-hover table-bordered">
                                                            <div class="titlebar">
                                                            </div>
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Course Name
                                                                    </th>

                                                                    <th>Status</th>
                                                                    <th>Lock/Unlock</th>
                                                                    <th style="text-align: center;">Mark Entry Report</th>
                                                                </tr>
                                                                <tr id="Tr1" runat="server" />
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                        <td>
                                                            <asp:LinkButton ID="lnkbtnCourse" runat="server" Text='<%# Eval("ccode") + " - " + Eval("COURSE_NAME")  + "-" + Eval("Scheme") %>'
                                                                ToolTip='<%# Eval("COURSENO")%>' CommandArgument='<%# Eval("COLLEGE_ID") + "+" +  Eval("OPID")%>'
                                                                OnClick="lnkbtnCourse_Click" />
                                                            <asp:HiddenField runat="server" ID="hdfcourseno" Value='<%# Eval("COURSENO")%>' />
                                                            <asp:HiddenField runat="server" ID="hdncolg" Value='<%# Eval("COLLEGE_ID")%>' />
                                                            <asp:HiddenField runat="server" ID="hdnopid" Value='<%# Eval("OPID")%>' />
                                                            <asp:HiddenField runat="server" ID="hdnlock" Value='<%# Eval("lock")%>' />
                                                            <asp:Label runat="server" ID="lblrandomno" Text="-" Font-Bold="true"></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:Label runat="server" ID="lblcompstatus" Text="-"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lbllockstatus" Text="-"></asp:Label>
                                                        </td>
                                                        <td style="text-align: center;">
                                                            <asp:ImageButton ID="ImgReport" runat="server" CausesValidation="false" ImageUrl="~/IMAGES/print.gif"
                                                                AlternateText='<%# Eval("OPID")%>' ToolTip="Report" CommandArgument='<%# Eval("COURSENO")%>'
                                                                OnClick="btnReportS_Click" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                               </asp:Panel>  <%--added on 27-03-2020 by Vaishali--%>


                                <div class="text-center" runat="server" id="divButtons">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="teacherallot" OnClick="btnSave_Click" Visible="false"
                                        Width="100px" ToolTip="SAVE" CssClass="btn btn-success" />&nbsp;&nbsp;
                                    <asp:Button ID="btnLock" runat="server" Visible="false" Text="Lock" ValidationGroup="teacherallot" OnClick="btnLock_Click" CssClass="btn btn-danger"
                                    Width="100px" ToolTip="Lock" OnClientClick="return showLockConfirm(this,'val');"  />&nbsp;&nbsp;
                                    <asp:Button ID="btnClear" runat="server" Text="Back to Course List"  Visible="false" Width="150px" CssClass="btn btn-primary" OnClick="btnClear_Click" />
                                </div>
                              <%--  </asp:Panel>--%>  <%--commented on 27-03-2020 by Vaishali--%>

                                <br />
                                <br />
                                <!--ListView for marks--->
                                <div class="col-md-12" id="tdStudent" runat="server">
                                    <asp:Panel ID="pnlStudGrid" runat="server" Visible="false">
                                        <div class="titlebar" runat="server" id="title" style="font-weight: bold; font-size: medium; text-align: center;">
                                        </div>
                                        
                                        <br />
                                        <div id="demo-grid">
                                            <%--<h4>Enter Marks for following Students </h4>--%>
                                            <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-bordered table-hover table-responsive">
                                                <HeaderStyle CssClass="bg-light-blue" HorizontalAlign="Center" />
                                                <AlternatingRowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No."
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex+1%>' Font-Size="9pt" />

                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Enrollment No."
                                                        ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIDNO" runat="server" Text='<%# Bind("ENROLLNO") %>' ToolTip='<%# Bind("IDNO") %>'
                                                                Font-Size="9pt" />
                                                            <asp:HiddenField runat="server" ID="hdnlock" Value='<%# Bind("lock") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Roll No."
                                                        ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIDNO1" runat="server" Text='<%# Bind("Roll_NO") %>' ToolTip='<%# Bind("IDNO") %>'
                                                                Font-Size="9pt" />
                                                            <asp:HiddenField runat="server" ID="hdnlock1" Value='<%# Bind("lock") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField Visible="false" HeaderText="Student Name"
                                                        ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCoursename" runat="server"
                                                                Font-Size="9pt" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                    </asp:TemplateField>

                                                    <%--EXAM MARK ENTRY--%>
                                                    <asp:TemplateField HeaderText="C1" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtExternmark" runat="server" Text='<%# Bind("MARKS")  %>' Width="70px" MaxLength="5" onblur="return CheckMark(this);"
                                                                Font-Bold="true" Style="text-align: center; text-align-last: center" ToolTip='<%# Bind("MARKS") %>' Enabled='<%# (Eval("LOCK").ToString() == "1") ? false : true %>' />
                                                            <asp:HiddenField ID="hdS1max" runat="server" Value='<%# Bind("MAXMARKS") %>' />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtTutMarks" runat="server" FilterType="Custom"
                                                                ValidChars="0123456789-." TargetControlID="txtExternmark">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:CompareValidator ID="comTutMarks" ValueToCompare='<%# Bind("MAXMARKS") %>' ControlToValidate="txtExternmark"
                                                                Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                                                ValidationGroup="teacherallot" Text="*"></asp:CompareValidator>
                                                            <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comTutMarks" ID="cecomTutMarks" runat="server">
                                                            </ajaxToolKit:ValidatorCalloutExtender>
                                                           <%-- <asp:HiddenField runat="server" ID="hdnpaternno" Value='<%#Eval("patternno")%>' />--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>



                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>
                                </div>

                            </div>

                                </div>


                        </div>
                    </div>

                </div>
           
            </ContentTemplate>


        </asp:UpdatePanel>
    </asp:Panel>

    <div id="divMsg" runat="server"></div>

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
    <script language="javascript" type="text/javascript">
        function IsNumeric(txt) {
            var ValidChars = "0123456789.-";
            var num = true;
            var mChar;

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);
                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Error! Only Numeric Values Are Allowed")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }

        function IsNumeric(txt1) {
            var ValidChars = "0123456789.-";
            var num = true;
            var mChar;

            for (i = 0; i < txt1.value.length && num == true; i++) {
                mChar = txt1.value.charAt(i);
                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt1.value = '';
                    alert("Error! Only Numeric Values Are Allowed")
                    txt1.select();
                    txt1.focus();
                }
            }
            return num;
        }

        function IsNumeric(txt2) {
            var ValidChars = "0123456789.-";
            var num = true;
            var mChar;

            for (i = 0; i < txt2.value.length && num == true; i++) {
                mChar = txt2.value.charAt(i);
                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt2.value = '';
                    alert("Error! Only Numeric Values Are Allowed")
                    txt2.select();
                    txt2.focus();
                }
            }
            return num;
        }

        function IsNumeric(txt3) {
            var ValidChars = "0123456789.-";
            var num = true;
            var mChar;

            for (i = 0; i < txt3.value.length && num == true; i++) {
                mChar = txt3.value.charAt(i);
                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt3.value = '';
                    alert("Error! Only Numeric Values Are Allowed")
                    txt3.select();
                    txt3.focus();
                }
            }
            return num;
        }


        function showLockConfirm() {
            var ret = confirm('Do you want to really Lock Mark Statement for selected Exam ');
            if (ret == true)
                return true;
            else
                return false;
        }
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

    </script>

    <script type="text/javascript" language="javascript">
        function totAllSubjects(headchk) {
            debugger;
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
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
