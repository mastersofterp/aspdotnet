<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendEmailToStudents.aspx.cs" Inherits="ACADEMIC_SendEmailToStudents" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updEmail" DynamicLayout="true" DisplayAfter="0">
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

    <asp:UpdatePanel ID="updEmail" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                           <h3 class="box-title">
                            <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" TabIndex="1" ToolTip="Please Select Admission Batch." CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAdm" runat="server" ControlToValidate="ddlAdmBatch" ErrorMessage="Please Select Admission Batch."
                                            Display="None" ValidationGroup="Show" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Programme Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlProgramme" runat="server" AppendDataBoundItems="true" TabIndex="1" ToolTip="Please Select Programme Type." CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlProgramme_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlProgramme" ErrorMessage="Please Select Programme Type."
                                            Display="None" ValidationGroup="Show" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="1" ToolTip="Please Select Degree." CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDegree" ErrorMessage="Please Select Degree."
                                            Display="None" ValidationGroup="Show" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <asp:RadioButtonList ID="rdoList" runat="server" RepeatDirection="Horizontal" RepeatColumns="1" AutoPostBack="true" OnSelectedIndexChanged="rdoList_SelectedIndexChanged">
                                            <asp:ListItem Value="1" Selected="True">Application Complete &nbsp;</asp:ListItem>
                                            <asp:ListItem Value="0">Application Incomplete &nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rdoList" ErrorMessage="Please Select Application Status."
                                            Display="None" ValidationGroup="Show" InitialValue="-1"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Subject</label>
                                        </div>
                                        <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" TextMode="MultiLine" PlaceHolder="Please Enter Email Subject here"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="txtSubject" ErrorMessage="Please Enter Subject." TabIndex="1" Display="None"
                                            ValidationGroup="Send"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Message</label>
                                        </div>
                                        <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" PlaceHolder="Please Enter Email Message here."></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMessage" ErrorMessage="Please Enter Message." TabIndex="1" Display="None"
                                            ValidationGroup="Send"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show" TabIndex="1" ToolTip="Click To Show." CssClass="btn btn-primary" OnClick="btnShow_Click" ValidationGroup="Show" />
                                <asp:Button ID="btnSend" runat="server" Text="Send Email" TabIndex="1" ToolTip="Cllick To Send." CssClass="btn btn-primary" OnClick="btnSend_Click" ValidationGroup="Send"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="1" ToolTip="Click To Cancel." CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="vsShow" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />
                                <asp:ValidationSummary ID="vsSend" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Send" />

                            </div>
                            <div class="col-md-12">
                                <asp:Panel ID="pnlListView" runat="server">
                                    <asp:ListView ID="lvStudList" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Students List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 50%" id="tblstudent">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="width: 5%">
                                                            <asp:CheckBox ID="chkSelect1" runat="server" onclick="totAllSubjects(this)" />
                                                        </th>
                                                        <th>Application ID
                                                        </th>
                                                        <th>Name
                                                        </th>
                                                        <th>Email
                                                        </th>
                                                        <th>Mobile No
                                                        </th>
                                                        <th>Application Status
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td style="width: 5%">
                                                    <asp:CheckBox ID="chkSelect1" runat="server" ToolTip='<%# Eval("USERNO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblApp" runat="server" Text='<%#Eval("USERNAME") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("FIRSTNAME") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("EMAILID") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("MOBILENO") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblConfirm" runat="server" Text='<%#Eval("CONFIRM_STATUS") %>' ForeColor='<%#Eval("CONFIRM_STATUS").ToString().Equals("COMPLETE") ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
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
        function validation() {
            var sub = document.getElementById(<%=txtSubject.Text%>).value;
            var message = document.getElementById(<%=txtMessage.Text%>).value;
            alert(sub);
            return;
            if (sub == "")
            {
                alert("Please Enter Subject.");
                return false;
            }
            if (message == "")
            {
                alert("Please Enter Message.");
                return false;
            }
           var count = 0;
           var numberOfChecked = $('[id*=tblstudent] td input:checkbox:checked').length;
           if (numberOfChecked == 0) {
               alert("Please Select Atleast One Student.");
               return false;
           }
           else
               return true;
       }
    </script>
</asp:Content>
