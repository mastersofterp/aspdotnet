<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CourseSlotMapping.aspx.cs" Inherits="ACADEMIC_MASTERS_CourseSlotMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        #ctl00_ContentPlaceHolder1_Coursepanel .select2.select2-container {
            width: 220px !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updcourseslot"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">
                            Loading<span>.</span><span>.</span><span>.</span>
                        </p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDYpagecourseslot" runat="server"></asp:Label></h3>
                </div>
                <div class="box-body">
                    <div>
                        <asp:UpdateProgress ID="updprograss" runat="server" AssociatedUpdatePanelID="updcourseslot"
                            DynamicLayout="true" DisplayAfter="0">
                            <ProgressTemplate>
                                <div id="preloader">
                                    <div id="loader-img">
                                        <div id="loader">
                                        </div>
                                        <p class="saving">
                                            Loading<span>.</span><span>.</span><span>.</span>
                                        </p>
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <asp:UpdatePanel ID="updcourseslot" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>
                                            <asp:Label ID="lblDYddlSession" runat="server"></asp:Label></label>
                                    </div>
                                    <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" data-select2-enable="true"
                                        AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                        TabIndex="1">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlsession" runat="server" ControlToValidate="ddlSession"
                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                        ValidationGroup="submit" />
                                    <asp:RequiredFieldValidator ID="rfv1ddlsession" runat="server" ControlToValidate="ddlSession"
                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                        ValidationGroup="show" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>
                                            <asp:Label ID="lblDYddlSchemeName" runat="server"></asp:Label></label>
                                    </div>
                                    <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                        data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>
                                            <asp:Label ID="lblDYddlSemester" runat="server"></asp:Label></label>
                                    </div>
                                    <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                        data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>
                                            <asp:Label ID="lblDYddlSubtype" runat="server"></asp:Label></label>
                                    </div>
                                    <asp:DropDownList ID="ddlSubjecttype" runat="server" AppendDataBoundItems="true"
                                        data-select2-enable="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlSubjecttype_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="btn btn-info"
                                    TabIndex="1" OnClick="btnshow_Click" ValidationGroup="show" />
                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-info"
                                    TabIndex="1" CausesValidation="true" OnClick="btnsubmit_Click"
                                    ValidationGroup="submit" />
                                <asp:Button ID="btnreport" runat="server" Text="Report" CssClass="btn btn-info"
                                    TabIndex="1" OnClick="btnreport_Click" ValidationGroup="show" />
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-warning"
                                    TabIndex="1" OnClick="btncancel_Click" />
                                <asp:ValidationSummary ID="valsumcommon" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="show" />
                                <asp:ValidationSummary ID="valsubmit" runat="server" DisplayMode="List"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                            </div>
                            <asp:Panel ID="pnlcommonapply" runat="server" Visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>
                                                <asp:Label ID="lblDYddlcourseslot" runat="server"></asp:Label></label>
                                        </div>
                                        <asp:DropDownList ID="ddlcourseslot" runat="server" AppendDataBoundItems="true"
                                            data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Range From</label>
                                        </div>
                                        <div style="display: flex;">
                                            <div class="form-inline">
                                                <asp:TextBox ID="txtrowFrom" runat="server" CssClass="form-control" TabIndex="1" MaxLength="4" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtEnrollFrom" runat="server" TargetControlID="txtrowFrom" FilterType="Numbers, Custom" ValidChars="123" FilterMode="ValidChars">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <label>To</label>
                                                <asp:TextBox ID="txtrowTo" runat="server" CssClass="form-control" TabIndex="1" MaxLength="4" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtEnrollTo" runat="server" TargetControlID="txtrowTo" FilterType="Numbers, Custom" ValidChars="123" FilterMode="ValidChars">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                            <div>
                                                <asp:Button ID="btnapply" runat="server" Text="Apply" CssClass="btn btn-primary"
                                                    TabIndex="1" OnClick="btnapply_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="Coursepanel" runat="server">
                                <div class="col-12 mt-4" id="gridrow">
                                    <asp:ListView ID="lvcourse" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Course List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Select</th>
                                                        <th>Course Code</th>
                                                        <th>Course Name</th>
                                                        <th>Slot</th>
                                                        <th>Semester</th>
                                                        <th>Scheme Name</th>
                                                        <th>Course Type</th>
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
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:CheckBox ID="chkAccept" runat="server" ToolTip='<%# Container.DataItemIndex + 1 %>' TabIndex="1" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblccode" runat="server" Text='<%# Eval("CCODE")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblcoursename" runat="server" Text='<%# Eval("COURSE_NAME")%>'></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCourseSlot" runat="server" AppendDataBoundItems="true" CausesValidation="false"
                                                        data-select2-enable="true" TabIndex="1" Enabled="false">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hdf_slotno" runat="server" Value='<%# Eval("SLOTNO")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblsemester" runat="server" Text='<%# Eval("SEMESTERNAME")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblscheme" runat="server" Text='<%# Eval("SCHEMENAME")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblcoursetype" runat="server" Text='<%# Eval("SUBNAME")%>'></asp:Label></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </asp:Panel>
                            <div id="divMsg" runat="Server">
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnreport"/>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" language="javascript">
        $(function () {
            debugger;
            //Enable Disable TextBoxes in a Row when the Row CheckBox is checked.
            $("[id*=chkAccept]").on("click", function () {
                //Find and reference the GridView.
                var List = $(this).closest("table");
                //If the CheckBox is Checked then enable the TextBoxes in the Row.
                if (!$(this).is(":checked")) {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#FFF" });
                    $("select", td).prop("disabled", "disabled");
                } else {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#D8EBF2" });
                    $("select", td).removeAttr("disabled");
                }
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            //Enable Disable TextBoxes in a Row when the Row CheckBox is checked.
            $("[id*=chkAccept]").on("click", function () {
                //Find and reference the GridView.
                var List = $(this).closest("table");
                //If the CheckBox is Checked then enable the TextBoxes in the Row.
                if (!$(this).is(":checked")) {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#FFF" });
                    $("select", td).prop("disabled", "disabled");
                } else {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#D8EBF2" });
                    $("select", td).removeAttr("disabled");
                }
            });
        });
    </script>
</asp:Content>

