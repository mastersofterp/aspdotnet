<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SpecializationCoursesAllotment.aspx.cs" Inherits="ACADEMIC_SpecializationCoursesAllotment" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />--%>
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>


    <style>
        .fa-plus {
            color: #17a2b8;
            border: 1px solid #17a2b8;
            border-radius: 50%;
            padding: 6px 8px;
            cursor: pointer;
        }

        .table-bordered > thead > tr > th {
            border-top: 1px solid #e5e5e5;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updclub"
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


    <asp:UpdatePanel ID="updclub" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SPECIALIZATION COURSES GROUP ALLOTMENT</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College Scheme </label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollegeScheme" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlCollegeScheme_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvcollege" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Select College Scheme" InitialValue="0" ControlToValidate="ddlCollegeScheme"
                                            Display="None" ValidationGroup="SHOW" />
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Group Name </label>
                                        </div>


                                        <asp:ListBox ID="ddlgroup" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>
                                        <%-- <asp:RequiredFieldValidator ID="rfvgroup" runat="server" SetFocusOnError="True"  
                                            ErrorMessage="Please Select GroupName " ControlToValidate="ddlgroup"
                                            Display="None" ValidationGroup="submit" />--%>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Semester </label>
                                        </div>
                                        <asp:DropDownList ID="ddlsemester" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvcourse" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Select Semester" InitialValue="0" ControlToValidate="ddlsemester"
                                            Display="None" ValidationGroup="submit" />
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-outline-info" OnClick="btnShow_Click" ValidationGroup="SHOW">Show Student List</asp:LinkButton>
                                    <asp:LinkButton ID="btnsubmit" runat="server" CssClass="btn btn-outline-info" OnClick="btnsubmit_Click" ValidationGroup="submit">Submit</asp:LinkButton>
                                    <asp:Button ID="btnReport" runat="server" Text="Report(Excel)" TabIndex="12" CssClass="btn btn-info" OnClick="btnReport_Click" ValidationGroup="SHOW" />
                                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                        ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                        ShowSummary="false" DisplayMode="List" ValidationGroup="SHOW" />
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="pnlCourseallotment" runat="server">
                                        <asp:ListView ID="lvCourseallotment" runat="server">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <%-- <th >SrNo
                                                        </th>--%>
                                                            <th>
                                                                <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" />
                                                            </th>
                                                            <th>Registration No</th>
                                                            <th>StudentName
                                                            </th>
                                                            <th>Collge scheme
                                                            </th>
                                                            <th>Group Status</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <%--<td>
                                                        <%#Container.DataItemIndex+1%>
                                                              </td>--%>
                                                    <td>
                                                        <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNO")%>' />
                                                        <asp:Label ID="lblIdNo" runat="server" Text='<%# Eval("IDNO")%>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbregno" runat="server" Text='<%# Eval("REGNO")%>'></asp:Label></td>
                                                    <%-- <td><%# Eval("REGNO") %></td>--%>
                                                    <td><%# Eval("STUDNAME") %></td>
                                                    <td><%# Eval("COL_SCHEME_NAME") %></td>
                                                    <td>
                                                        <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("GROUP_STATUS")%>' ToolTip='<%# Eval("GROUP_STATUS")%>'></asp:Label></td>

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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });


    </script>
    <script>
        function checkout() {
            var checkBoxes = document.getElementsByClassName('ddlgroup');
            var nbChecked = 0;
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].checked) {
                    nbChecked++;
                };
            };
            if (nbChecked > 3) {
                alert('Please Select Maximun 3 Group.');
                return false;
            } else if (nbChecked == 0) {
                alert('Please, select at least one Group!');
                return false;
            } else {
                //Do what you need for form submission, if needed...
            }
        }


        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            var j = 0;
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                debugger;
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        if (j != 0) {
                            e.checked = true;
                        }
                        j++;
                    }
                    else
                        e.checked = false;
                }
            }

        }
        function Checkedfalse(headchk) {

        }
    </script>
</asp:Content>
