<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MarkEntryDashboardWireFrame.aspx.cs" Inherits="ACADEMIC_EXAMINATION_MarkEntryDashboardWireFrame" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updMarksEntryDetailReport"
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
   
    <asp:UpdatePanel runat="server" ID="updMarksEntryDetailReport">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Exam Mark Entry Pending Dashboard</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" AppendDataBoundItems="true" CssClass="form-control" placeholder="Please Select Session" runat="server" data-select2-enable="true">  <%--OnSelectedIndexChanged="ddlSession_OnSelectedIndexChanged" AutoPostBack="true"--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="Show" OnClick="btnShow_OnClick" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Show"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>
                                </div>
                            </div>

                            <div runat="server" id="divCourseMarksEntryDetail" Visible="false">
                                <div class="form-group col-lg-9 col-md-12 col-12">
                                    <div class=" note-div">
                                        <h5 class="heading">Note (Please Select)</h5>
                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>No Need to select Checkbox for Mail sending to all faculties. Select Checkbox in case of selected faculty mail sending.</span>  </p>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div style="overflow : auto; width:100%;">
                                        <asp:GridView ID="gvParent" runat="server" DataKeyNames="SRNO" Width="100%"
                                            CssClass="table table-bordered table-hover" AutoGenerateColumns="false" 
                                            OnRowDataBound="gvParent_RowDataBound" GridLines="Horizontal"
                                            ShowFooter="false" Visible="true" EmptyDataText="There are no data records to display.">

                                            <HeaderStyle CssClass="bg-light-blue" Width="100%" ForeColor="white" />
                                            <Columns>
                                                <asp:BoundField DataField="REG_COURSE_COUNT"  HeaderText="Total Number of Registered Courses" />
                                                <asp:BoundField DataField="EXAMNAME"  HeaderText="Exam Name"  />
                                                 <asp:TemplateField  HeaderText="Mark Entry Pending Courses View Details">
                                                    <ItemTemplate>
                                                        <div id="divcR" runat="server">
                                                            <a href="JavaScript:divexpandcollapse('div4<%# Eval("SRNO") %>');">
                                                             <%--  <img alt='<%# Eval("COURSE_MARKS_PENDING") %>' id='CLOSE<%# Eval("SRNO") %>' border="0" title='<%# Eval("COURSE_MARKS_PENDING") %>' />--%>
                                                               <asp:Label runat="server" id="lbl" Text='<%# Eval("COURSE_MARKS_PENDING") %>'></asp:Label>
                                                               <asp:HiddenField ID="hdfTempExam" runat="server" Value='<%# Eval("SRNO") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td colspan="100%">
                                                                <div id='div4<%# Eval("SRNO") %>' style="display: none;">
                                                                    <asp:GridView ID="gvChild" runat="server"  DataKeyNames="SRNO" AutoGenerateColumns="false" BorderStyle="Double"
                                                                        CssClass="table table-bordered table-hover"  OnRowDataBound="gvChild_RowDataBound"
                                                                        Width="100%" ShowFooter="false" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                        <HeaderStyle CssClass="bg-light-blue" ForeColor="White" />
                                                                        <FooterStyle ForeColor="White" />
                                                                        <RowStyle />
                                                                        <AlternatingRowStyle BackColor="White" />
                                                                        <HeaderStyle CssClass="bg-light-blue" ForeColor="white" />
                                                                        <Columns>
                                                                             <asp:TemplateField HeaderText="School/Institute Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <%# Eval("COLLEGE_NAME") %>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="left" />
                                                                                <ItemStyle />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Number of Pending Courses View Details">
                                                                                <ItemTemplate>
                                                                                    <div id="divcR" runat="server">
                                                                                        <a href="JavaScript:divexpandcollapse('div5<%# Eval("SRNOCOLLEGEID") %>');">
                                                                                            <asp:Label runat="server" id="lbl_1" Text='<%# Eval("REG_COURSE_COUNT") %>'></asp:Label>
                                                                                    </div>
                                                                                    <asp:HiddenField ID="hdfCollegeId" runat="server" Value='<%# Eval("COLLEGE_ID") %>' />
                                                                                    <asp:HiddenField ID="hdfTempExam" runat="server" Value='<%# Eval("SRNO") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td colspan="100%">
                                                                                            <div id='div5<%# Eval("SRNOCOLLEGEID") %>' style="display: none;">
                                                                                                <asp:GridView ID="gvChild_1" runat="server" AutoGenerateColumns="false" BorderStyle="Double"
                                                                                                    CssClass="table table-bordered table-hover" OnRowDataBound="gvChild_1_RowDataBound"
                                                                                                    Width="100%" ShowFooter="true" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                    <HeaderStyle CssClass="bg-light-blue" ForeColor="White" />
                                                                                                    <FooterStyle ForeColor="White" />
                                                                                                    <RowStyle />
                                                                                                    <AlternatingRowStyle BackColor="White" />
                                                                                                    <HeaderStyle CssClass="bg-light-blue" ForeColor="white" />
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField HeaderText="SELECT">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:CheckBox ID="chk"  runat="server" />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Department Names">
                                                                                                            <ItemTemplate>
                                                                                                                <%# Eval("DEPTNAME") %>
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle  />
                                                                                                            <ItemStyle />
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Number of Pending Courses View Details">
                                                                                                            <ItemTemplate>
                                                                                                                <div id="divcR" runat="server">
                                                                                                                    <a href="JavaScript:divexpandcollapse('div6<%# Eval("SRNODEPTNO") %>');">
                                                                                                                       <%-- <img alt='<%# Eval("REG_COURSE_COUNT") %>' id='CLOSE<%# Eval("COLLEGE_ID") %>' border="0" title='<%# Eval("REG_COURSE_COUNT") %>' />--%>
                                                                                                                        <asp:Label runat="server" id="lbl_2" Text='<%# Eval("REG_COURSE_COUNT") %>'></asp:Label>
                                                                                                                </div>
                                                                                                                <asp:HiddenField ID="hdfCollegeId" runat="server" Value='<%# Eval("COLLEGE_ID") %>' />
                                                                                                                <asp:HiddenField ID="hdfTempExam" runat="server" Value='<%# Eval("SRNO") %>' />
                                                                                                                <asp:HiddenField ID="hdfDeptNo" runat="server" Value='<%# Eval("DEPTNO") %>' />
                                                                                                                <asp:HiddenField ID="hdfHodName" runat="server" Value='<%# Eval("UA_FULLNAME") %>' />
                                                                                                                <asp:HiddenField ID="hdfMailID" runat="server" Value='<%# Eval("UA_EMAIL") %>' />
                                                                                                                <asp:HiddenField ID="hdfMobileNo" runat="server" Value='<%# Eval("UA_MOBILE") %>' />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Send E-Mail">
                                                                                                            <ItemTemplate>
                                                                                                              <asp:Button ID="btnSendDeptMail" Visible='<%# Convert.ToInt32(Eval("Row"))==1 ?true:false %>' OnClick="btnSendDeptMail_OnClick" CommandArgument='<%# Eval("DEPTNO") %>' Text="Send Mail" CssClass="btn btn-primary" ToolTip='<%# Eval("COLLEGE_ID") %>' runat="server" />
                                                                                                            </ItemTemplate>
                                                                                                            <HeaderStyle/>
                                                                                                            <ItemStyle />
                                                                                                        </asp:TemplateField>

                                                                                                        <asp:TemplateField>
                                                                                                            <ItemTemplate>
                                                                                                                <tr>
                                                                                                                    <td colspan="100%">
                                                                                                                        <div id='div6<%# Eval("SRNODEPTNO") %>' style="display: none;">
                                                                                                                            <asp:GridView ID="gvChild_2" runat="server" AutoGenerateColumns="false" 
                                                                                                                                CssClass="table table-bordered table-hover" Width="100%" ShowFooter="true" ShowHeaderWhenEmpty="true" EmptyDataText="No data Found">
                                                                                                                                <HeaderStyle CssClass="bg-light-blue" ForeColor="White" />
                                                                                                                                <FooterStyle ForeColor="White" />
                                                                                                                                <RowStyle />
                                                                                                                                <AlternatingRowStyle BackColor="White" />
                                                                                                                                <HeaderStyle CssClass="bg-light-blue" ForeColor="white" />
                                                                                                                                <Columns>
                                                                                                                                    <asp:TemplateField HeaderText="SELECT">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:CheckBox ID="chk" runat="server" />
                                                                                                                                             <asp:HiddenField ID="hdfMailID" runat="server" Value='<%# Eval("UA_EMAIL") %>' />
                                                                                                                                             <asp:HiddenField ID="hdfMobileNo" runat="server" Value='<%# Eval("UA_MOBILE") %>' />
                                                                                                                                             <asp:HiddenField ID="hdfCollegeId" runat="server" Value='<%# Eval("COLLEGE_ID") %>' />
                                                                                                                                             <asp:HiddenField ID="hdfTempExam" runat="server" Value='<%# Eval("SRNO") %>' />
                                                                                                                                             <asp:HiddenField ID="hdfDeptNo" runat="server" Value='<%# Eval("DEPTNO") %>' />
                                                                                                                                        </ItemTemplate>
                                                                                                                                    </asp:TemplateField>

                                                                                                                                    <asp:TemplateField HeaderText="Faculty Name">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <%# Eval("FACULTYNAME") %>
                                                                                                                                        </ItemTemplate>
                                                                                                                                        <HeaderStyle />
                                                                                                                                        <ItemStyle />
                                                                                                                                    </asp:TemplateField>

                                                                                                                                    <asp:TemplateField HeaderText="Pending Marks Entry Courses">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <%# Eval("COURSES") %>
                                                                                                                                        </ItemTemplate>
                                                                                                                                        <HeaderStyle/>
                                                                                                                                        <ItemStyle />
                                                                                                                                    </asp:TemplateField>

                                                                                                                                    <asp:TemplateField HeaderText="Status">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <%# Eval("STATUS") %>
                                                                                                                                        </ItemTemplate>
                                                                                                                                        <HeaderStyle HorizontalAlign="left" />
                                                                                                                                        <ItemStyle />
                                                                                                                                    </asp:TemplateField>

                                                                                                                                    <asp:TemplateField HeaderText="Send E-Mail">
                                                                                                                                        <ItemTemplate>
                                                                                                                                            <asp:Button ID="btnSendFacultyMail" Visible='<%# Convert.ToInt32(Eval("Row"))==1 ?true:false %>' OnClick="btnSendFacultyMail_OnClick" CommandName='<%# Eval("SRNO") %>' CommandArgument='<%# Eval("COLLEGE_ID") %>' Text="Send Mail" CssClass="btn btn-primary" ToolTip='<%# Eval("DEPTNO") %>' runat="server" />
                                                                                                                                        </ItemTemplate>
                                                                                                                                        <HeaderStyle/>
                                                                                                                                        <ItemStyle  />
                                                                                                                                    </asp:TemplateField>

                                                                                                                                </Columns>
                                                                                                                                <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" />
                                                                                                                            </asp:GridView>
                                                                                                                        </div>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>

                                                                                                    </Columns>
                                                                                                    <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red"/>
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                        <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red"/>
                                                                    </asp:GridView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                   </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <%-- <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
        </Triggers>--%>
    </asp:UpdatePanel>

    <div class="col-12">
        <div class="row">
            <div class="modal fade" data-backdrop="static" data-keyboard="false" aria-modal="true" id="Model_Message" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header text-center" style="background-color: #00C6D7 !important">
                            <h4 class="modal-title" style="font-style: normal; color: white">Send Mail</h4>
                        </div>
                        <div class="modal-body">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <div class="row">
                                            <div class="col-sm-12 form-group">
                                                <div class="row">

                                                    <div class="col-sm-12 form-group">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <label>To<sup>*</sup></label>
                                                            </div>
                                                            <div class="col-sm-12">
                                                                <asp:TextBox ID="txt_emailid" Enabled="false" placeholder="Email ID" runat="server" TabIndex="2" CssClass="form-control"></asp:TextBox>
                                                                <asp:HiddenField ID="hdfEmail" runat="server" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter To Email "
                                                                    ControlToValidate="txt_emailid" Display="None" ValidationGroup="EmailSend"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_emailid"
                                                                    Display="None" ErrorMessage="Please Enter Valid To Email " ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                    ValidationGroup="login1"></asp:RegularExpressionValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-12 form-group">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <label>Subject<sup>*</sup></label>
                                                            </div>
                                                            <div class="col-sm-12">
                                                                <asp:TextBox ID="txtSubject" runat="server" TabIndex="3" MaxLength="128" placeholder="Enter Subject" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSubject"
                                                                    ErrorMessage="Please Enter Subject" Display="None" ValidationGroup="EmailSend"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                     <div class="col-sm-12 form-group">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <label>Message Body<sup>*</sup></label>
                                                            </div>
                                                            <div class="col-sm-12">
                                                                <asp:TextBox ID="txtBody" runat="server" TabIndex="4" MaxLength="8000" TextMode="MultiLine" placeholder="Enter Message" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBody"
                                                                    ErrorMessage="Please Enter Message  " Display="None" ValidationGroup="EmailSend"></asp:RequiredFieldValidator>

                                                            </div>
                                                        </div>
                                                    </div>


                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>

                            <div class="modal-footer text-center">
                                <asp:Button ID="btnSent" runat="server" Text="Send" CssClass="btn btn-primary" UseSubmitBehavior="false"
                                    OnClientClick="if (!Page_ClientValidate('EmailSend')){ return false; } this.disabled = true; this.value ='  Please Wait...';"
                                    TabIndex="4" ValidationGroup="EmailSend" OnClick="btnSent_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Close" data-dismiss="modal" OnClientClick="ClearMessageText();" CssClass="btn btn-danger" TabIndex="5"></asp:Button>
                                <asp:HiddenField ID="hdfReceiver_id" runat="server" />
                                <asp:ValidationSummary ID="vsMessage" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="EmailSend" DisplayMode="List" />
                            </div>

                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>

    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "../IMAGES/minus.png";
            }
            else {
                div.style.display = "none";
                img.src = "../IMAGES/plus.gif";
            }
        }
    </script>

    <script>
        function View(txtvalue) {
            $("#Model_Message").modal();
            document.getElementById('ctl00_ContentPlaceHolder1_txt_emailid').value = txtvalue;
            document.getElementById('ctl00_ContentPlaceHolder1_txtSubject').focus();
        }

        //Clear Message Popup
        function ClearMessageText() {
            document.getElementById('<%=txtSubject.ClientID%>').value = "";
            document.getElementById('<%=txtBody.ClientID%>').value = "";
        }
    </script>

</asp:Content>


