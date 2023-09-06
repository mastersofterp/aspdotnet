<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Meeting_Scheduled_Send_Email_Sms.aspx.cs" Inherits="ACADEMIC_MentorMentee_Meeting_Scheduled_Send_Email_Sms" ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
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
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Meeting Scheduled And Email SMS</h3>
                        </div>
                        <%--<div class="col-12 mt-lg-4">--%>
                        <%--    <div class="note-div">
                            <h5 class="heading">Note</h5>
                            <i class="fa fa-star" aria-hidden="true"></i>
                            <span>
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="On Selection of Committee, ListView is Visible." ForeColor="Red"></asp:Label></span>
                        </div>--%>
                        <%--</div>--%>

                        <div class="box-body">
                            <asp:Panel ID="pnlDesig" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>

                                                <label>Committee</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCommitee" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCommitee_SelectedIndexChanged" AutoPostBack="true"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Select Committee" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                                             
                                              <asp:RequiredFieldValidator ID="rfvcommitee" runat="server" ControlToValidate="ddlCommitee"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select Committee" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            

                                        </div>

                                        <div id="Div2" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Meeting Scheduled Date Time</label>
                                            </div>
                                            <asp:DropDownList ID="ddlMeeting" runat="server" AutoPostBack="true" AppendDataBoundItems="true" ValidationGroup="submit"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Select Meeting Scheduled Date Time" TabIndex="2">
                                            </asp:DropDownList>
                                              <asp:RequiredFieldValidator ID="rfcmeeting" runat="server" ControlToValidate="ddlMeeting"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select Meeting Scheduled Date Time" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            
                                        </div>

                                        <div class="col-12 btn-footer">

                                            <asp:Button ID="btnSendParentEmail" runat="server" Enabled="true" Text="Send Email" ValidationGroup="submit" CausesValidation="true"  OnClick="btnSendParentEmail_Click" CssClass="btn btn-primary" />
                                            <asp:Button ID="btnPCancel" runat="server" Text="Cancel" CausesValidation="False" OnClick="btnPCancel_Click" CssClass="btn btn-warning" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" DisplayMode="List" ValidationGroup="submit" />


                                        </div>

                                        <div class="col-12">
                                            <div id="Div4">
                                                <asp:Panel ID="Panel2" runat="server">
                                                    <asp:ListView ID="lvParent" runat="server">
                                                        <LayoutTemplate>

                                                            <div id="demo-grid">
                                                                <div class="sub-heading">
                                                                    <h5>Parent List</h5>
                                                                </div>
                                                                <table id="tblParent" class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>
                                                                                <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" Visible="true" />
                                                                                Select </th>

                                                                            <th>Registration No. </th>
                                                                            <th>Student Name </th>
                                                                            <th>Father Name</th>
                                                                            <th>Father Email ID</th>
                                                                            <th>Father Mobile No</th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>

                                                                    <asp:CheckBox ID="chkSelect" runat="server" Checked="false" TabIndex="1" />
                                                                    <asp:HiddenField ID="hidStudentId" runat="server" Value='<%# Eval("IDNO")%>' />
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblPreg" runat="server" Text='<%# Eval("REGNO")%>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblPstudent" runat="server" Text='<%# Eval("STUDNAME")%>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblFathername" runat="server" Text='<%# Eval("FATHERNAME")%>' ToolTip='<%# Eval("FATHERNAME")%>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblFatherEmailID" runat="server" Text='<%# Eval("FATHER_EMAIL")%>' ToolTip='<%# Eval("FATHER_EMAIL")%>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblFatherMobile" runat="server" Text='<%# Eval("FATHERMOBILE")%>' ToolTip='<%# Eval("FATHERMOBILE")%>'></asp:Label>
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                                <div id="div5" runat="server">
                                                </div>
                                            </div>
                                        </div>
                            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

        function totAllSubjects(headchk) {
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


    </script>

</asp:Content>

