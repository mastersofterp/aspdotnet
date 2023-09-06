<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ITLE_Bulk_stud_log_creation.aspx.cs" Inherits="Itle_ITLE_Bulk_stud_log_creation"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdBulkLogin"
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
    <asp:UpdatePanel ID="UpdBulkLogin" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">BULK STUDENT LOGIN CREATION</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlBulkStudent" runat="server">
                                <div class="col-12">
                                    <asp:Panel ID="pnl1" runat="server">
                                        <div class="row">
                                            <%--  <div class="col-lg-12 col-md-12 col-12">
                                                <div class="sub-heading">
                                                    <h5>Bulk Student Login Creation By Faculty</h5>
                                                </div>
                                            </div>--%>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Session</label>
                                                </div>
                                                <asp:DropDownList ID="Ddlsession" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                    OnSelectedIndexChanged="Ddlsession_SelectedIndexChanged" TabIndex="1"
                                                    ToolTip="Select Session" CssClass="form-control" data-select2-enable="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="Ddlsession"
                                                    Display="None" ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Degree</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true"
                                                    ToolTip="Select Degree" TabIndex="2">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0"
                                                    ValidationGroup="course"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Branch</label>
                                                </div>
                                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                                    ToolTip="Select Branch" TabIndex="3">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Branch"
                                                    ValidationGroup="course"></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="DropDownList2" runat="server" Visible="false" CssClass="form-control"
                                                    AppendDataBoundItems="true">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">

                                                    <label>Scheme</label>
                                                </div>
                                                <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged"
                                                    ToolTip="Select Scheme" TabIndex="4">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Progam"
                                                    ValidationGroup="course"></asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlHidden" runat="server" Visible="false" CssClass="form-control"
                                                    AppendDataBoundItems="true">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">

                                                    <label>Semester</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged"
                                                    ToolTip="Select Semester" TabIndex="5">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="course">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="6"
                                                ToolTip="Click To Save Exam Registration" CssClass="btn btn-primary"
                                                OnClick="btnSubmit_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="7"
                                                ToolTip="Click To Cancel Exam Registration" CssClass="btn btn-warning"
                                                OnClick="btnCancel_Click" />
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
                                <div class="col-12" id="pnlPages" runat="server" visible="false">
                                    <div class="sub-heading">
                                        <h5>Authorised Pages</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap " style="width: 100%" id="">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>
                                                    <asp:CheckBox ID="chkAll" runat="server" Enabled="true" onclick="totAllSubjects(this);" ToolTip="Select All Pages" />
                                                </th>
                                                <th>Page Name</th>
                                            </tr>
                                    </table>

                                    <div class="col-12 DocumentList">
                                        <asp:Panel ID="pnlPagesList" runat="server">
                                            <asp:ListView ID="lvPages" runat="server">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <table class="table table-bordered table-hover">
                                                            <thead>
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
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblPageName" runat="server" Text='<%#Eval("AL_LINK") %>' ToolTip='<%#Eval("AL_NO") %>' />
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
                                <div class="col-12" id="pnlStudGrid" runat="server" visible="false">
                                    <div class="sub-heading">
                                        <h5>Student List</h5>
                                    </div>
                                    <asp:Panel ID="pnlStudGridList" runat="server">
                                        <asp:ListView ID="LsvStudList" runat="server">
                                            <LayoutTemplate>

                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>
                                                                <asp:CheckBox ID="CheckBox1" runat="server" Enabled="false" Checked="true" onclick="totAllSubjects(this);" />
                                                            </th>
                                                            <th>Student Name</th>
                                                            <th>Roll No</th>
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
                                                        <asp:CheckBox ID="chkSelect" Enabled="false" Checked="true" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblStudentName" runat="server" Text='<%#Eval("STUDNAME") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRollNo" runat="server" Text='<%#Eval("roll_NO") %>' ToolTip='<%#Eval("IDNO") %>' />
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

                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">
        function totAllSubjects(headchk) {
            var hdfTot = document.getElementById('<%= pnlPages.ClientID %>');

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (e.name.endsWith('chkSelect')) {
                        if (headchk.checked == true) {
                            e.checked = true;
                            hdfTot.value = Number(hdfTot.value) + 1;
                        }
                        else
                            e.checked = false;

                    }
                }
            }

            if (headchk.checked == false) hdfTot.value = "0";
        }

    </script>
    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }
    </script>

</asp:Content>
