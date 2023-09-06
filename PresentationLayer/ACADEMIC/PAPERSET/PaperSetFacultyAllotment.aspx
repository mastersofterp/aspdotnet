<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PaperSetFacultyAllotment.aspx.cs" Inherits="ACADEMIC_PaperSetFacultyAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updFacAllot"
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
    </div>--%>
    <asp:UpdatePanel ID="updFacAllot" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PAPER SETTER ALLOTMENT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                           <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                         <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                            ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                            AppendDataBoundItems="True" TabIndex="1" ToolTip="Please Select Session">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" ValidationGroup="Show"
                                            InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                       <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged"
                                            AppendDataBoundItems="True" TabIndex="1" ToolTip="Please Select ddlScheme">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDepartment" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select Scheme" ValidationGroup="Show"
                                            InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Select Semester" ValidationGroup="Show" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" TabIndex="3">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            InitialValue="0" Display="None" ErrorMessage="Please Select Semester" ValidationGroup="Show"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <%-- <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note</h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Once locked, the paper setter selection cannot be changed by BOS. </span></p>

                                        </div>
                                    </div>--%>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" ToolTip="Click to Show" runat="server" Text="Show" OnClick="btnShow_Click"
                                    ValidationGroup="Show" TabIndex="5" CssClass="btn btn-primary" />
                                <asp:Button ID="btnReport" ToolTip="Click to get Report" runat="server" Text="Report(Set Paper)"
                                    OnClick="btnReport_Click" ValidationGroup="Show" TabIndex="6"
                                    CssClass="btn btn-info" />
                                <asp:Button ID="btnReportNotSet" ToolTip="Click to get Report" runat="server" Text="Report(Not Set Paper)"
                                    TabIndex="6" ValidationGroup="Show"
                                    CssClass="btn btn-info" OnClick="btnReportNotSet_Click" />
                                <asp:Button ID="btnClear" runat="server" ToolTip="Select to clear" Text="Clear"
                                    OnClick="btnClear_Click" TabIndex="7" CssClass="btn btn-warning" />
                            </div>
                            <div class="col-12 mt-3 mb-3">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvCourse" Visible="false" OnItemDataBound="lvCourse_ItemDataBound" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <div class="sub-heading">
                                                    <h5>Courses</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Code </th>
                                                            <th>Course</th>
                                                            <th>Paper Setter 1 </th>
                                                            <th>Quantity1</th>
                                                            <th style="display: none;">MOI1 </th>
                                                            <th>Paper Setter 2 </th>
                                                            <th>Quantity2 </th>
                                                            <th style="display: none;">MOI2 </th>
                                                            <th>Paper Setter 3 </th>
                                                            <th>Quantity3 </th>
                                                            <th style="display: none;">MOI3 </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td>
                                                    <asp:Label ID="lblCCode" ToolTip="Course code" runat="server" Text='<%# Eval("CCODE") %>' />
                                                    <asp:HiddenField runat="server" ID="hffaculty1" Value='<%# Eval("FACULTY1") %>' />
                                                    <asp:HiddenField runat="server" ID="hffaculty2" Value='<%# Eval("FACULTY2") %>' />
                                                    <asp:HiddenField runat="server" ID="hffaculty3" Value='<%# Eval("FACULTY3") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCourseName" ToolTip="Course Name" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFaculty1" ToolTip="Select Degree" AppendDataBoundItems="true"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlFaculty1_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true"
                                                        runat="server">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:HiddenField runat="server" ID="hfBosLock" Value='<%# Eval("BOS_LOCK") %>' />
                                                    <asp:HiddenField runat="server" ID="hfDeanLock" Value='<%# Eval("DEAN_LOCK") %>' />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txQt1" ToolTip="Paperset quantity for paper setter 1" MaxLength="1" CssClass="form-control"
                                                        runat="server" Text='<%# Eval("QT1") %>' />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftetxQt1" runat="server" FilterType="Numbers"
                                                        TargetControlID="txQt1">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                                <td style="display: none;">
                                                    <asp:TextBox ID="txtMOI1" ToolTip="Paperset MOI for paper setter 1" MaxLength="1"
                                                        runat="server" Text='<%# Eval("MOI1") %>' Visible="false" CssClass="form-control" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtMOI1" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtMOI1">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFaculty2" ToolTip="Select Paper setter" AppendDataBoundItems="true"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlFaculty2_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true"
                                                        runat="server">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txQt2" ToolTip="Paperset quantity for paper setter 2" MaxLength="1"
                                                        runat="server" Text='<%# Eval("QT2") %>' />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftetxQt2" runat="server" FilterType="Numbers"
                                                        TargetControlID="txQt2">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                                <td style="display: none;">
                                                    <asp:TextBox ID="txtMOI2" ToolTip="Paperset MOI for paper setter 2" MaxLength="1"
                                                        runat="server" Text='<%# Eval("MOI2") %>' Visible="false" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtMOI2" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtMOI2">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFaculty3" ToolTip="Select Paper setter" AppendDataBoundItems="true"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlFaculty3_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true"
                                                        runat="server">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txQt3" ToolTip="Paperset quantity for paper setter 3" MaxLength="1"
                                                        runat="server" Text='<%# Eval("QT3") %>' />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftetxQt3" runat="server" FilterType="Numbers"
                                                        TargetControlID="txQt3">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                                <td style="display: none;">
                                                    <asp:TextBox ID="txtMOI3" ToolTip="Paperset MOI for paper setter 3" MaxLength="1"
                                                        runat="server" Text='<%# Eval("MOI3") %>' Visible="false" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtMOI3" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtMOI3">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div class="col-12 btn-footer mt-4">
                                <%--  <p class="text-center">--%>
                                <asp:Button ID="btnSave" Visible="false" ToolTip="Click to Save" runat="server" Text="Save"
                                    OnClick="btnSave_Click" ValidationGroup="Show" CssClass="btn btn-primary" />
                                <asp:Button ID="btnLock" Visible="false" runat="server" Text="Lock" OnClick="btnLock_Click"
                                    ToolTip="Select to Lock" OnClientClick="return showConfirm();"
                                    ValidationGroup="Show" CssClass="btn btn-primary" />
                                <asp:Button ID="btnCancel" Visible="false" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    ToolTip="Cancel" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" />
                                <%--</p>--%>
                            </div>


                        </div>
                    </div>
                </div>
            </div>



            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
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
    <script type="text/javascript">
        function showConfirm() {
            var ret = confirm('Are You Sure You Want To Proceed ? After Done You Never Make Any Changes');
            if (ret == true) {
                validate = true;
            }
            else
                validate = false;
            return validate;
        }

    </script>

    <%--END MODAL POPUP EXTENDER FOR DELETE CONFIRMATION --%>
</asp:Content>
