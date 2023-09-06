<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BacklogExam.aspx.cs" Inherits="ACADEMIC_BacklogExam" MasterPageFile="~/SiteMasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updStudent"
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
    <asp:UpdatePanel runat="server" ID="updStudent">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">BULK BACKLOG EXAM REGISTRATION</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="1" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0"
                                            ValidationGroup="register"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="2" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="register"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="3" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0"
                                            ValidationGroup="register"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" TabIndex="4" data-select2-enable="true"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select Scheme" InitialValue="0"
                                            ValidationGroup="register"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" TabIndex="5" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0"
                                            ValidationGroup="register"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
								        <div class="label-dynamic">
									        <label>Total Students Selected</label>
								        </div>
                                         <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False" CssClass="watermarked form-control" TabIndex="9" 
                                            Style="text-align: center;" Font-Bold="True" Font-Size="Small"
                                            ForeColor="#000066"></asp:TextBox>
							        </div>

                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShowStudent" runat="server" Text="Show Students" ValidationGroup="register"
                                    OnClick="btnShowStudent_Click" CssClass="btn btn-primary" TabIndex="6"/>
                                <asp:Button ID="btnRegistration" runat="server" Text="Do Registration" ValidationGroup="register" TabIndex="7"
                                    OnClick="btnRegistration_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click" CssClass="btn btn-warning" TabIndex="8"/>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="register"
                                    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />

                                <asp:HiddenField ID="hftot" runat="server" />
                            </div>
                            
                            <div class="col-12">
                                <asp:ListView ID="lvStudent" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading new-header-lv">
	                                        <h5>Backlog Student List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" id="divbackloglist" style="width:100%;">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="cbRows" runat="server" ToolTip="Check Record" onClick="SelectAll(this);"
                                                            Checked="true" />
                                                    </th>
                                                    <th>Enroll.No.
                                                    </th>
                                                    <th>Roll No.
                                                    </th>
                                                    <th>Student Name
                                                    </th>
                                                    <th>CCODES
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
                                                <asp:CheckBox ID="chkStudent" runat="server" ToolTip='<%# Eval("IDNO") %>' onClick="totSubjects(this);"
                                                    Checked="true" />
                                            </td>
                                            <td>
                                                <%# Eval("REGNO")%>
                                            </td>
                                            <td>
                                                <%# Eval("ROLL_NO")%>
                                            </td>
                                            <td>
                                                <%# Eval("STUDNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("CCODES")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnRegistration" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
    <script lang="javascript" type="text/javascript">

        function validateAssign() {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;
            if (txtTot == 0) {
                alert('Please Check atleast one student ');
                return false;
            }
            else
                return true;
        }
    </script>
    <script type="text/javascript" lang="javascript">
        function SelectAll(chk) {
            debugger;
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>');
            for (i = 0; i < hftot.value; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudent_ctrl' + i + '_chkStudent');
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
</asp:Content>
