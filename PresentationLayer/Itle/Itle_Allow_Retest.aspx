<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Itle_Allow_Retest.aspx.cs" Inherits="Itle_Itle_Allow_Retest" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function totAllIDs(headchk) {

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
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdPnlRetest"
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
    <asp:UpdatePanel ID="UpdPnlRetest" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ALLOW RETEST</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlAllow" runat="server">
                                <div class="col-12">
                                    <asp:Panel ID="pnl1AllowTest" runat="server">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Session</label>
                                                </div>
                                                <asp:DropDownList ID="Ddlsession" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                    OnSelectedIndexChanged="Ddlsession_SelectedIndexChanged" TabIndex="1"
                                                    ToolTip="Select Session" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="Ddlsession"
                                                    Display="None" ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Degree</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true"
                                                    TabIndex="2" ToolTip="Select Degree">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0"
                                                    ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Branch</label>
                                                </div>
                                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="3"
                                                    ToolTip="Select Branch">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Branch"
                                                    ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="DropDownList2" runat="server" Visible="false" CssClass="form-control"
                                                    AppendDataBoundItems="true">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Scheme</label>
                                                </div>
                                                <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" TabIndex="4"
                                                    ToolTip="Select Scheme">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Progam"
                                                    ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlHidden" runat="server" Visible="false" CssClass="form-control"
                                                    AppendDataBoundItems="true">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Semester</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" TabIndex="5"
                                                    ToolTip="Select Semester">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="submit">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Course</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="True" TabIndex="6" ToolTip="Select Course"
                                                    OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCourse"
                                                    Display="None" ErrorMessage="Please Select Course" InitialValue="0"
                                                    ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Select Test</label>
                                                </div>
                                                <asp:DropDownList ID="ddlTest" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="True" TabIndex="7" ToolTip="Select Test"
                                                    OnSelectedIndexChanged="ddlTest_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTest"
                                                    Display="None" ErrorMessage="Please Select Test" InitialValue="0"
                                                    ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>


                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="8" ToolTip="Click To Allow Retest"
                                                OnClick="btnSubmit_Click" CssClass="btn btn-primary" ValidationGroup="submit" />
                                            <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="10" CssClass="btn btn-info"
                                                ToolTip="Click To View Retest Report" OnClick="btnReport_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="9" CssClass="btn btn-warning"
                                                ToolTip="Click To Cancel Exam Registration" OnClick="btnCancel_Click" />

                                            <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="submit" runat="server" />
                                        </div>
                                        <div class="col-12">
                                            <asp:HiddenField ID="hdnSession" runat="server" />
                                            <asp:HiddenField ID="hdnCourse" runat="server" />
                                            <asp:HiddenField ID="hdnDivision" runat="server" />
                                            <asp:HiddenField ID="hdnSubjectType" runat="server" />
                                            <asp:HiddenField ID="hdnSubject" runat="server" />
                                            <asp:HiddenField ID="hdnTeacher" runat="server" />
                                        </div>

                                    </asp:Panel>
                                </div>
                            </asp:Panel>

                            <div class="col-12" id="DivRetestGrid" runat="server" visible="false">

                                <div class="sub-heading">
                                    <h5>Allow Retest List</h5>
                                </div>
                                <asp:Panel ID="pnlRetestList" runat="server">
                                    <asp:ListView ID="lvretest" runat="server">
                                        <LayoutTemplate>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="chkall" runat="server" Text="Retest All" onclick="totAllIDs(this);" />
                                                        </th>
                                                        <th> RRN</th>
                                                        <th>Student Name</th>
                                                        <th>Request Date</th>
                                                        <th>Previous Count</th>
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
                                                    <asp:CheckBox ID="chkstatus" runat="server" />
                                                </td>
                                                <td> 
                                                    <asp:Label ID="LBLRRN" runat ="server" Text='<%# Eval("REGNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblstudname" runat="server" Text='<%# Eval("STUDNAME")%>'
                                                        ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblreqdate" runat="server" Text='<%# Eval("APPDATE")%>'
                                                        ToolTip='<%# Eval("REQNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblcount" runat="server" Text='<%# Eval("Count")%>'
                                                        ToolTip='<%# Eval("UA_NO")%>'>  </asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <p class="text-center text-bold">

                                                <asp:Label ID="lblEmptyMsg" runat="server" Text="No Records Found"></asp:Label>
                                            </p>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </asp:Panel>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

