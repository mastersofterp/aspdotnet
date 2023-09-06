<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudExmTimeTable.aspx.cs" Inherits="ACADEMIC_EXAMINATION_StudExmTimeTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updtime"
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

    <asp:UpdatePanel ID="updtime" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Exam Time Table</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divCollScheme">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College & Scheme</label>
                                            <asp:DropDownList ID="ddlCollegeSheme" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlCollegeSheme_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlCollegeSheme" runat="server" ControlToValidate="ddlCollegeSheme"
                                                Display="None" ErrorMessage="Please Select College/Scheme" ValidationGroup="show" InitialValue="0"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true" TabIndex="1"
                                            AppendDataBoundItems="True" ToolTip="Please Select Session">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>

                                   <%-- <div class="form-group col-lg-3 col-md-6 col-12" id="divScheme" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" TabIndex="2"
                                            AppendDataBoundItems="True" AutoPostBack="true" ToolTip="Please Select Scheme">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>--%>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Exam Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlExam" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="3"
                                            AutoPostBack="True" ToolTip="Please Select Institute">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <%-- <asp:ListItem Value="1">Mid Sem</asp:ListItem>
                                            <asp:ListItem Value="2">End Sem</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvColg" runat="server" ControlToValidate="ddlExam"
                                            Display="None" ErrorMessage="Please Select Exam Name" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Time Table" OnClick="btnShow_Click" TabIndex="4"
                                    ToolTip="Shows Students under Selected Criteria." ValidationGroup="show" CssClass="btn btn-primary" />

                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="8"
                                    ValidationGroup="show" CssClass="btn btn-primary" CausesValidation="false" Visible="false" />
                                <asp:Button ID="btnLock" runat="server" Text="Lock" TabIndex="9" Visible="false"
                                    ValidationGroup="show" CssClass="btn btn-primary" />
                                <asp:Button ID="btnUnlock" runat="server" Text="Unlock" TabIndex="10" Visible="false"
                                    ValidationGroup="show" CssClass="btn btn-primary" />
                                <asp:Button ID="btnPrintReport" runat="server" Visible="false" Text="Print No-Dues Form" TabIndex="999" CssClass="btn btn-info"
                                    ToolTip="Print Card under Selected Criteria." ValidationGroup="show" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="5"
                                    ToolTip="Cancel Selected under Selected Criteria." OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvExamTimeTable" runat="server">
                                        <LayoutTemplate>
                                            <div id="listViewGrid" class="vista-grid">
                                                <div class="sub-heading">
                                                    <h5>Exam Time Table</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblStudent">
                                                    <thead class="bg-light-blue">
                                                        <tr id="Tr1">
                                                            <th>CCODE
                                                            </th>
                                                            <th>COURSE NAME
                                                            </th>
                                                            <th>SLOT NAME
                                                            </th>
                                                            <th>TIME FROM
                                                            </th>
                                                            <th>TIME TO
                                                            </th>
                                                            <th>SEMESTER
                                                            </th>
                                                            <th>DATE
                                                            </th>
                                                            <th>REGULAR/BACKLOG
                                                            </th>
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
                                                <td>
                                                    <%# Eval("CCODE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("COURSENAME")%>
                                                </td>
                                                <td><%# Eval("SLOTNAME")%></td>
                                                <td><%# Eval("TIMEFROM")%> </td>
                                                <td><%# Eval("TIMETO")%> </td>
                                                <td>
                                                    <%# Eval("SEMESTERNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("EXAMDATE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("REGULAR_BACKLOG")%>
                                                </td>


                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="show" DisplayMode="List" />
                                <div id="divMsg" runat="server">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

