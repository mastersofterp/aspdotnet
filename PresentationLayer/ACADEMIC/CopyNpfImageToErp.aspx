<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CopyNpfImageToErp.aspx.cs" Inherits="ACADEMIC_CopyNpfImageToErp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl"
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
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">OFFERED COURSE</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--  <label>College & Scheme</label>--%>
                                            <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmissionBatch" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="1" CssClass="form-control"
                                            ValidationGroup="show" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlAdmissionBatch" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="show">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Document Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlType" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="True" TabIndex="1" CssClass="form-control"
                                            ValidationGroup="show" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Photo</asp:ListItem>
                                            <asp:ListItem Value="2">Signature</asp:ListItem>
                                            <asp:ListItem Value="3">10th Marksheet</asp:ListItem>
                                            <asp:ListItem Value="4">12th/HSC Marksheet</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmissionBatch" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Photo Type" InitialValue="0" ValidationGroup="show">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="box-footer">
                                    <p class="text-center">
                                        <asp:Button ID="btnShow" runat="server" Text="Show Student" CssClass="btn btn-success" ValidationGroup="show"
                                            OnClick="btnShow_Click" TabIndex="8" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="false" ValidationGroup="show" TabIndex="1" OnClientClick="return validation();"
                                            OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                                         <asp:Button ID="btnSubmitDocument" runat="server" Text="Submit" Visible="false" ValidationGroup="show" TabIndex="1" OnClientClick="return validation();"
                                            OnClick="btnSubmitDocument_Click" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="11" />

                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" DisplayMode="List" ValidationGroup="show" />
                                    </p>

                                    <div class="col-12">
                                        Total Students Selected:
                                <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False" CssClass="unwatermarked"
                                    Style="text-align: center;" Width="4%" Height="20px" BackColor="#FFFFCC" Font-Bold="True"
                                    Font-Size="Small" ForeColor="#000066"></asp:TextBox>
                                        <asp:HiddenField ID="hftot" runat="server" />
                                        <asp:Panel ID="pnlSchemeAllot" runat="server">

                                            <asp:ListView ID="lvStudents" runat="server">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap display" id="tblstudent" style="width: 100%">
                                                        <div class="sub-heading">
                                                            <h5>Student List</h5>
                                                        </div>
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>
                                                                    Select
                                                                    <%--<asp:CheckBox ID="cbHead" TabIndex="1" runat="server" onClick="SelectAll(this);" />--%>
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblApplicationId" Text="Application Id" runat="server" Font-Bold="true"></asp:Label>
                                                                </th>
                                                                <th>NPF Application Id
                                                                </th>
                                                                <th>Name
                                                                </th>
                                                                <th>Mobile No
                                                                </th>
                                                                  <th>Degree
                                                                </th>
                                                                <th>Branch
                                                                </th>

                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="cbRow" runat="server" TabIndex="1" onclick="totSubjects(this)" ToolTip='<%# Eval("USERNO")%>' />
                                                        </td>

                                                        <td>
                                                            <%# Eval("USERNAME")%>
                                                        </td>

                                                        <td>
                                                            <%# Eval("APPLICATION_NO")%>
                                                        </td>
                                                         <td>
                                                            <%# Eval("FIRSTNAME")%>
                                                        </td>
                                                           <td>
                                                            <%# Eval("MOBILENO")%>
                                                        </td>
                                                          <td>
                                                            <%# Eval("DEGREENAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("LONGNAME")%>
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
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmitDocument" />
        </Triggers>
    </asp:UpdatePanel>
     <script>
         function validation() {
             var count = 0;
             var numberOfChecked = $('[id*=tblstudent] td input:checkbox:checked').length;
             if (numberOfChecked == 0) {
                 alert("Please select Atleast one student.");
                 return false;
             }
             else
                 return true;
         }
    </script>
    <script language="javascript" type="text/javascript">



        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;

        }

        function SelectAll(chkbox) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>');
            for (i = 0; i < hftot.value; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl' + i + '_cbRow');
                if (lst.type == 'checkbox') {
                    if (chkbox.checked == true)
                        lst.checked = true;
                    else
                        lst.checked = false;
                }

            }

            if (chkbox.checked == true) {
                txtTot.value = hftot.value;
            }
            else {
                txtTot.value = 0;
            }
        }
    </script>
</asp:Content>

