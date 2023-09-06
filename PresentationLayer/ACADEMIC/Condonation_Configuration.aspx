<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Condonation_Configuration.aspx.cs" Inherits="ACADEMIC_Condonation_Configuration" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdCondolance"
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
    <asp:UpdatePanel ID="UpdCondolance" runat="server">
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
                                            <sup>*</sup>
                                            <label>Operator From </label>
                                        </div>
                                        <asp:DropDownList ID="ddlOperator" runat="server" TabIndex="11" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem Selected="True">&gt;=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>=</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Range From</label>
                                        </div>
                                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvSearchtring" runat="server" ControlToValidate="txtFrom"
                                            Display="None" ErrorMessage="Please Enter Range From"
                                            SetFocusOnError="true" ValidationGroup="submitt" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Operator To </label>
                                        </div>
                                        <asp:DropDownList ID="ddlopratorto" runat="server" TabIndex="11" CssClass="form-control" data-select2-enable="true">
                                            <%--<asp:ListItem>&gt;</asp:ListItem>--%>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <%--<asp:ListItem Selected="True">&gt;=</asp:ListItem>--%>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem Selected="True" >=</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Range To</label>
                                        </div>
                                        <asp:TextBox ID="TxtTo" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtTo"
                                            Display="None" ErrorMessage="Please Enter Range To"
                                            SetFocusOnError="true" ValidationGroup="submitt" />
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12" runat="server">
                                        <asp:CheckBox ID="chkallowfee" runat="server" Text="Cheked if Fees Collection is required to apply for condonation"
                                            AutoPostBack="True" OnCheckedChanged="chkallowfee_CheckedChanged" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-12" id="chekfess" runat="server" visible="false">
                                <div class="form-group col-lg-4 col-md-6 col-12" style="text-align: center" runat="server" id="rdoselection">
                                    <asp:RadioButtonList ID="rdoSelect" runat="server" AppendDataBoundItems="true" RepeatDirection="Horizontal"
                                        AutoPostBack="true" OnSelectedIndexChanged="rdoSelect_SelectedIndexChanged">
                                        <asp:ListItem Value="1">&nbsp;&nbsp; Course Wise Fees &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="2">&nbsp;&nbsp;Overall Courses Fees</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rdoSelect"
                                        Display="None" ErrorMessage="Please Select Option To Define Fees"
                                        SetFocusOnError="true" ValidationGroup="submitt" />
                                </div>

                                <div class="col-12" id="cousewiese" runat="server" visible="false">
                                    <div class="col-12">
                                        <div class="row">

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>School/Scheme</label>
                                                </div>
                                                <asp:DropDownList runat="server" class="form-control" ID="ddlschool" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlschool_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlschool" InitialValue="0"
                                                    Display="None" ErrorMessage="Please select School"
                                                    SetFocusOnError="true" ValidationGroup="submitt" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Session</label>
                                                </div>
                                                <asp:DropDownList runat="server" class="form-control" ID="ddlsession" AppendDataBoundItems="true" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlsession" InitialValue="0"
                                                    Display="None" ErrorMessage="Please select Session"
                                                    SetFocusOnError="true" ValidationGroup="submitt" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Semester</label>
                                                </div>
                                                <asp:DropDownList runat="server" class="form-control" ID="ddlsemester" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlsemester_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlsemester" InitialValue="0"
                                                    Display="None" ErrorMessage="Please select Semester"
                                                    SetFocusOnError="true" ValidationGroup="submitt" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12" id="divCourseDetail" runat="server" visible="false">
                                        <asp:Panel ID="pnlCourse" runat="server">
                                            <asp:ListView ID="lvCourse" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Search List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblCourseLst">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <%--<th>Select</th>--%>
                                                                <th>
                                                                    <asp:Label ID="lblDYtxtCourseCode" runat="server" Font-Bold="true"></asp:Label></th>
                                                                <th>
                                                                    <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label></th>
                                                                <th>Fees</th>
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
                                                            <%--Enabled='<%# (Convert.ToInt32(Eval("REGISTERED") ) == 1 ?  false : true )%>'--%>
                                                        <%--<asp:CheckBox  ID="chkoffered" runat="server" ToolTip='<%# Eval("COURSENO") %>' />
                                                        </td>--%>
                                                        <td><%# Eval("CCODE")%></td>
                                                        <td>
                                                            <asp:HiddenField ID="hdfcoursno" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                            <asp:HiddenField ID="hdfcondlanceid" runat="server" Value='<%# Eval("CONDOLANCE_ID") %>' />
                                                            <asp:Label ID="lblcoursename" runat="server" Text='<%# Eval("COURSE_NAME")%>'></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:TextBox ID="TxtAMount" runat="server" Text='<%# Eval("FEES_DEFINE")%>'> </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-12" id="div2" runat="server">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:ListView ID="LvAllDetails" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Condonation Configuration List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblCourseLst">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit</th>
                                                                <th>Operator From</th>
                                                                <th>Range From</th>
                                                                <th>Operator To</th>
                                                                <th>Range to</th>
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
                                                            <asp:ImageButton ID="btnEditCou" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                                CommandArgument='<%# Eval("CONDOLANCE_ID") %>' ToolTip='<%# Eval("CONDOLANCE_ID") %>' AlternateText="Edit Record"
                                                                OnClick="btnEditCou_Click" />

                                                        </td>
                                                        <td><%# Eval("OPERATOR_FROM")%></td>
                                                        <td><%# Eval("RANGE_FROM")%></td>
                                                        <td><%# Eval("OPERATOR_TO") %></td>
                                                        <td><%# Eval("RANGE_TO")%></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="col-12" id="overall" runat="server" visible="false">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>School/Scheme</label>
                                                </div>
                                                <asp:DropDownList runat="server" class="form-control" ID="ddlschollOvera" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlschollOvera_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlschollOvera" InitialValue="0"
                                                    Display="None" ErrorMessage="Please select School"
                                                    SetFocusOnError="true" ValidationGroup="submitt" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Session</label>
                                                </div>
                                                <asp:ListBox ID="lstSession" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Semester</label>
                                                </div>
                                                <asp:DropDownList runat="server" class="form-control" ID="ddlsemoverall" AppendDataBoundItems="true" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlsemoverall" InitialValue="0"
                                                    Display="None" ErrorMessage="Please select Semester"
                                                    SetFocusOnError="true" ValidationGroup="submitt" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Define Fees</label>
                                                </div>
                                                <asp:TextBox ID="txtAmount" runat="server" TabIndex="3" CssClass="form-control" ToolTip="Please Enter Amount" onkeypress="CheckNumeric(event);" MaxLength="9">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAmount"
                                                    Display="None" ErrorMessage="Please Enter Define Fees"
                                                    SetFocusOnError="true" ValidationGroup="submitt" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12" id="div3" runat="server">
                                        <asp:Panel ID="Panel2" runat="server">
                                            <asp:ListView ID="LvOverall" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Overall Courses Fees List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblCourseLst">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit</th>
                                                                <th>School</th>
                                                                <th>Session</th>
                                                                <th>Semester</th>
                                                                <th>Operator From</th>
                                                                <th>Range From</th>
                                                                <th>Operator To</th>
                                                                <th>Range to</th>
                                                                <th>Fees Type</th>
                                                                <th>Fees</th>
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
                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                                CommandArgument='<%# Eval("CONDOLANCE_ID") %>' ToolTip='<%# Eval("CONDOLANCE_ID") %>' AlternateText="Edit Record"
                                                                OnClick="btnEdit_Click" />
                                                        </td>
                                                        <td><%# Eval("COLLEGE_NAME")%></td>
                                                        <td>
                                                            <%# Eval("SESSION_NAME")%>
                                                        </td>
                                                        <td><%# Eval("SEMESTERNAME")%></td>
                                                        <td><%# Eval("OPERATOR_FROM")%></td>
                                                        <td><%# Eval("RANGE_FROM")%></td>
                                                        <td><%# Eval("OPERATOR_TO") %></td>
                                                        <td><%# Eval("RANGE_TO")%></td>
                                                        <td><%# Eval("feestype")%></td>
                                                        <td><%# Eval("FEES_DEFINE")%></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ValidationGroup="submitt" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummery1" DisplayMode="List" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submitt" />
                            </div>

                        </div>
                    </div>
        </ContentTemplate>
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
</asp:Content>

