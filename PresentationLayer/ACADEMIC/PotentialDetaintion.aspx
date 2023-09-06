<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PotentialDetaintion.aspx.cs" Inherits="ACADEMIC_PotentialDetaintion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">PROVISIONAL DETENTION</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" TabIndex="1"
                                            ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Regulation" InitialValue="0" ValidationGroup="show">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" AppendDataBoundItems="true" ValidationGroup="show" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                            runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="3" ClientIDMode="Static" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester" SetFocusOnError="true"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-9 col-md-6 col-12" style="padding-top: 25px;">
                                        <asp:RadioButtonList ID="rblSelection" runat="server" AppendDataBoundItems="true" TabIndex="4" class="radiobuttonlist col-8" AutoPostBack="true"
                                            RepeatDirection="Horizontal" OnSelectedIndexChanged="rblSelection_SelectedIndexChanged">
                                            <asp:ListItem Value="1"><span style="font-size: 13px;font-weight:bold">Particular Subject Detention </span></asp:ListItem>
                                            <asp:ListItem Value="2"><span style="font-size: 13px;font-weight:bold">All Subject Detention</span></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divCourse" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%-- <label>Course</label>--%>
                                            <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" ValidationGroup="show" TabIndex="5">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>


                                    <div class="col-12">
                                        <div class="row" id="Perticularsub" runat="server">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Operator</label>
                                                </div>
                                                <asp:DropDownList ID="ddlOperator" runat="server" TabIndex="5" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem>&gt;</asp:ListItem>
                                                    <asp:ListItem>&lt;=</asp:ListItem>
                                                    <asp:ListItem Selected="True">&gt;=</asp:ListItem>
                                                    <asp:ListItem>&lt;</asp:ListItem>
                                                    <asp:ListItem>=</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Percentage</label>
                                                </div>
                                                <asp:TextBox ID="txtPercentage" runat="server" MaxLength="3" onblur="return CheckPercentage(this);" Text="0" CssClass="form-control"
                                                    onkeypress="return AllowNumeric(event)" AutoComplete="off" TabIndex="5"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show Students" ValidationGroup="show"
                                    TabIndex="5" CssClass="btn btn-info" />
                                <asp:Button ID="btnReport" runat="server" Text="Provisional Detention Reprt" ValidationGroup="Report"
                                    TabIndex="5" CssClass="btn btn-primary" OnClick="btnReport_Click" Visible="false" />

                                <asp:Label ID="lbldisp" runat="server" Visible="False"></asp:Label>
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                    ValidationGroup="show" TabIndex="6" Visible="False" CssClass="btn btn-primary" />

                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                    TabIndex="7" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="vsStud" runat="server" DisplayMode="List" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="show" />

                                <asp:ValidationSummary ID="VsReport" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Report" />
                            </div>

                            <div class="col-12">
                                <asp:Label ID="lblshow" runat="server" Font-Bold="true" Font-Size="Medium" Visible="false" Text="Student List"></asp:Label>
                                <asp:Panel ID="pnlStudents" runat="server">
                                    <div id="tbid">
                                        <asp:ListView ID="lvCourses" runat="server" OnItemDataBound="lvCourses_ItemDataBound">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="example2">
                                                    <thead class="bg-light-blue">
                                                        <tr id="Tr1" runat="server">
                                                            <th style="text-align: center; width: 5%">Select Student
                                                            </th>
                                                            <th style="text-align: center; width: 15%">
                                                                <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                            </th>
                                                            <th style="text-align: center; width: 70%">Student Name
                                                            </th>
                                                            <th style="text-align: center; width: 10%">Provisional Detention Status
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
                                                    <td style="text-align: center; width: 5%">
                                                        <asp:CheckBox ID="chkAccept" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                        <%--Checked='<%# Eval("PROV_DETAIN").ToString() == "1" ? true : false %>'--%> 
                                                    </td>
                                                    <td style="text-align: center; width: 15%">
                                                        <asp:Label ID="lblRoll" runat="server" Text='<%# Eval("REGNO") %>' />
                                                    </td>
                                                    <td style="text-align: left; width: 70%">
                                                        <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("STUDNAME") %>' />

                                                    </td>
                                                    <td style="text-align: center; width: 10%">
                                                        <asp:Label ID="lblProv" runat="server" Text='<%# Eval("PROV") %>' ToolTip='<%# Eval("PROV_DETAIN")%>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr>
                                                    <td style="text-align: center; width: 5%">
                                                        <asp:CheckBox ID="chkAccept" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNO") %>' />
                                                        <%--Checked='<%# Eval("PROV_DETAIN").ToString() == "1" ? true : false %>'--%>
                                                    </td>
                                                    <td style="text-align: center; width: 15%">
                                                        <asp:Label ID="lblRoll" runat="server" Text='<%# Eval("REGNO") %>' />
                                                    </td>
                                                    <td style="text-align: left; width: 70%">
                                                        <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("STUDNAME") %>' />
                                                    </td>
                                                    <td style="text-align: center; width: 10%">
                                                        <asp:Label ID="lblProv" runat="server" Text='<%# Eval("PROV") %>' ToolTip='<%# Eval("PROV_DETAIN")%>' />
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
                                <div id="divMsg" runat="server">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>
    <script>
        function CheckPercentage(id) {
            //debugger;
            if (id.value > 100) {
                if (id.value == -1) {
                }
                else {
                    alert("Percentage Should be less than or equal to 100");
                    id.value = '';
                    id.focus();
                    id.css("border", "solid 1px #ccc");
                }
            }
        }
    </script>
    <script type="text/javascript">
        function AllowNumeric(evt) {
            var regex = new RegExp("^[0-9]");
            var key = String.fromCharCode(event.charCode ? event.which : event.charCode);
            if (!regex.test(key)) {
                event.preventDefault();
                alert('Accept Only Numeric Value');
                return false;
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>

