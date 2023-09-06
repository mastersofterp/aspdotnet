<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BulkStudentPasswordUpdate.aspx.cs" Inherits="ADMINISTRATION_BulkStudentLogin" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upduser"
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

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <asp:UpdatePanel ID="upduser" runat="server" UpdateMode="Conditional">
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
                                            <%--<label>Admission Batch</label>--%>
                                            <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged1" CssClass="form-control" data-select2-enable="true" TabIndex="1" AutoPostBack="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvadmbatch" runat="server" ControlToValidate="ddlAdmBatch"
                                            Display="None" ErrorMessage="Please Select Admission batch" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                     
                                      
                                        <asp:RadioButton ID="rdGeneratepass" GroupName="password" runat="server" AutoPostBack="true" Text=" Generate Random Password" /><br />
                                        <asp:RadioButton ID="rdoCopyPassword" GroupName="password" runat="server" AutoPostBack="true"  Text=" Copy Password" />


                                    </div>


                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" ValidationGroup="SubPercentage"
                                    CssClass="btn btn-primary" TabIndex="6" />

                                <asp:Button ID="btnUpdatePass" runat="server" Text="Update Password" ValidationGroup="SubPercentage" CssClass="btn btn-primary" OnClick="btnUpdatePass_Click" />


                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="9"
                                    CausesValidation="False" CssClass="btn btn-warning" />


                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="SubPercentage"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-12">
                                <div id="dvListView">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:ListView ID="lvStudents" runat="server">
                                            <LayoutTemplate>
                                                <div id="demo-grid">
                                                    <div class="sub-heading">
                                                        <h5>Student List</h5>
                                                    </div>
                                                    <table id="tblStudents" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <%--   <th>
                                                                    <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" Visible="true" />
                                                                    Select </th>--%>

                                                                <th>Enrollment No. </th>
                                                                <th>Student Name </th>
                                                                <th>User Name</th>
                                                                <th>Status </th>

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
                                                    <%-- <td>
                                                        <asp:CheckBox ID="chkRow" runat="server" Font-Bold="true" ForeColor="Green" onclick="CountSelection();" />
                                                        
                                                    </td>--%>

                                                    <td>
                                                        <asp:HiddenField ID="hidStudentId" runat="server" Value='<%# Eval("IDNO")%>' />
                                                        <asp:Label ID="lblreg" runat="server" Text='<%# Eval("REGNO")%>' ToolTip='<%# Eval("REGNO")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstud" runat="server" Text='<%# Eval("STUDNAME")%>' ToolTip='<%# Eval("STUDNAME")%>'></asp:Label>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UA_NAME")%>' ToolTip='<%# Eval("UA_NAME")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# (Convert.ToInt32(Eval("STATUSS") )== 1 ?  "CREATED" : "" )%>'></asp:Label>
                                                    </td>
                                                    <%--   <td>
                                                                    <asp:Label ID="lblDob" runat="server" Text='<%# Eval("DOB")%>' ToolTip='<%# Eval("DOB")%>'></asp:Label>
                                                                </td>--%>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                    <div id="div2" runat="server">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>

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

