<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SemPromotionRules.aspx.cs" Inherits="ACADEMIC_EXAMINATION_SemPromotionRules" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSemPromotion"
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

    <asp:UpdatePanel ID="updSemPromotion" runat="server">
        <ContentTemplate>
            <div id="divMsg" runat="server">
            </div>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">EXAM PROMOTION RULES</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Please Select Scheme" AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Please Select Semester" AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" data-select2-enable="true" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Min. Earned Credits %</label>
                                        </div>
                                        <asp:TextBox ID="txtMinEarnedCreditsPer" runat="server" CssClass="form-control" MaxLength="2" TabIndex="3"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtTutMarks" runat="server" FilterType="Custom"
                                            ValidChars="0123456789" TargetControlID="txtMinEarnedCreditsPer">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvMinEarned" runat="server" ControlToValidate="txtMinEarnedCreditsPer"
                                            Display="None" ErrorMessage="Please Enter Min. Earned Credits %" ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Prev. Sem Course Cleared</label>
                                        </div>
                                        <asp:TextBox ID="txtPrevSemCourseCleared" runat="server" CssClass="form-control" MaxLength="2" TabIndex="4"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                                            ValidChars="0123456789" TargetControlID="txtPrevSemCourseCleared">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvPrevSem" runat="server" ControlToValidate="txtPrevSemCourseCleared"
                                            Display="None" ErrorMessage="Please Enter Prev. Sem Course Cleared" ValidationGroup="submit" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="submit"
                                    OnClick="btnSubmit_Click" TabIndex="5"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="6"/>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlDetails" runat="server" Visible="false">
                                    <asp:ListView ID="lvDetails" runat="server">
                                        <LayoutTemplate>
                                            <div id="listViewGrid">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th style="text-align: center">Action
                                                            </th>
                                                            <th>Scheme
                                                            </th>
                                                            <th>Semester
                                                            </th>
                                                            <th>Min.Earned Credits %
                                                            </th>
                                                            <th>Prev. Sem Course Cleared
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
                                                <td style="text-align: center">
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("RULE_ID") %>'
                                                        ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>

                                                <td>
                                                    <asp:Label ID="lblSchemeName" ToolTip='<%# Eval("SCHEMENO") %>' runat="server" Text='<%# Eval("SCHEMENAME")%>'></asp:Label>
                                                </td>

                                                <td>
                                                    <%# Eval("SEMESTERNAME")%>
                                                </td>

                                                <td>
                                                    <%# Eval("MIN_EARNED_CREDITS_PER")%>
                                                </td>

                                                <td>
                                                    <%# Eval("PREV_SEM_COURSE_CLEARED")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        
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

