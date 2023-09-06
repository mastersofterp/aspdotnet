<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="FinalDetaintion.aspx.cs" Inherits="ACADEMIC_FinalDetention" %>

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
            <asp:Panel ID="pnlCourse" runat="server">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div id="div1" runat="server"></div>
                            <div class="box-header with-border">
                                <%--<h3 class="box-title">FINAL DETENTION</h3>--%>
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
                                            <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname"
                                                Display="None" ErrorMessage="Please Select College & Regulation" InitialValue="0" ValidationGroup="show">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlClgname"
                                                Display="None" ErrorMessage="Please Select College & Regulation" InitialValue="0" ValidationGroup="Report">
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
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Report" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged"
                                                ValidationGroup="show" AutoPostBack="True" TabIndex="3" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSem"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="show" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-9 col-md-6 col-12" style="padding-top: 25px;">
                                            <asp:RadioButtonList ID="rblSelection" runat="server" AppendDataBoundItems="true" class="radiobuttonlist col-8" AutoPostBack="true"
                                                RepeatDirection="Horizontal" OnSelectedIndexChanged="rblSelection_SelectedIndexChanged">
                                                <asp:ListItem Value="1"><span style="font-size: 13px;font-weight:bold">Detention to Perticular Subject </span></asp:ListItem>
                                                <asp:ListItem Value="2"><span style="font-size: 13px;font-weight:bold"> Detention to all Subjects</span></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divCourse" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Course</label>--%>
                                                <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged"
                                                AutoPostBack="true" ValidationGroup="show" TabIndex="6">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" TabIndex="4" Text="Show Students"
                                        ValidationGroup="show" CssClass="btn btn-info" />
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                        ValidationGroup="show" Visible="False" TabIndex="5" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnReport" runat="server" Text="Final Detention Report" ValidationGroup="Report"
                                        TabIndex="6" CssClass="btn btn-primary" OnClick="btnReport_Click" />
                                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" TabIndex="7" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="vsStud" runat="server" DisplayMode="List" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="show" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="Report" />
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblMsg" runat="server" Visible="false" SkinID="lblmsg"></asp:Label>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:TextBox ID="txtAllSubjects" runat="server" Visible="false" CssClass="form-control" Enabled="false" Style="text-align: center;" Text="0" Width="30%"></asp:TextBox>
                                    <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                </div>

                                <div class="col-12">
                                    <%-- <asp:Label ID="lblshow" runat="server" Font-Bold="true" Font-Size="Medium" Visible="false" Text="Detention Student List"></asp:Label>--%>
                                    <asp:ListView ID="lvDetend" runat="server" OnItemDataBound="lvDetend_ItemDataBound">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Detention Student List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr id="Tr1" runat="server">
                                                        <th style="text-align: center; width: 10%">Select For Final Detention </th>
                                                        <th style="text-align: center; width: 15%">
                                                            <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th style="text-align: center; width: 45%">Student Name </th>
                                                        <%--   <th>Course Code </th>--%>
                                                        <th style="text-align: center; width: 15%">Provisional Detention </th>
                                                        <th style="text-align: center; width: 15%">Final Detention </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center; color: green; width: 10%">
                                                    <asp:CheckBox ID="chkAccept" ForeColor="Green" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                    <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("IDNO") %>' Visible="false" />
                                                    <%--Enabled='<%#(Convert.ToInt32(Eval("FINAL_DETAIN"))==1 ? false : true)%>'--%>
                                                </td>
                                                <td style="text-align: center; width: 15%">
                                                    <asp:Label ID="lblRegNo" runat="server" Text='<%# Eval("REGNO") %>' />
                                                </td>
                                                <td style="text-align: left; width: 45%">
                                                    <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDNAME") %>' ToolTip='<%# Eval("ROLLNO") %>' />
                                                </td>
                                                <%-- <td>
                                                    <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("COURSE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                </td>--%>
                                                <td style="text-align: center; width: 15%">
                                                    <asp:Label ID="lblProv" runat="server" Text='<%# Eval("PROV") %>' ToolTip='<%# Eval("FINAL_DETAIN")%>' />
                                                </td>
                                                <td style="text-align: center; width: 15%">
                                                    <asp:Label ID="lblFinal" runat="server" Text='<%# Eval("FINAL") %>' ToolTip='<%# Eval("FINAL_DETAIN")%>'> </asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr>
                                                <td style="text-align: center; color: green; width: 10%">
                                                    <asp:CheckBox ID="chkAccept" runat="server" ForeColor="Green" ToolTip='<%# Eval("IDNO") %>' />
                                                    <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("IDNO") %>' Visible="false" />
                                                    <%--Enabled='<%#(Convert.ToInt32(Eval("FINAL_DETAIN"))==1 ? false : true)%>'--%>
                                                </td>
                                                <td style="text-align: center; width: 15%">
                                                    <asp:Label ID="lblRegNo" runat="server" Text='<%# Eval("REGNO") %>' />
                                                </td>
                                                <td style="text-align: left; width: 45%">
                                                    <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDNAME") %>' ToolTip='<%# Eval("ROLLNO") %>' />
                                                </td>
                                                <%-- <td>
                                                    <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("COURSE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                </td>--%>
                                                <td style="text-align: center; width: 15%">
                                                    <asp:Label ID="lblProv" runat="server" Text='<%# Eval("PROV") %>' ToolTip='<%# Eval("FINAL_DETAIN")%>' />
                                                </td>
                                                <td style="text-align: center; width: 15%">
                                                    <asp:Label ID="lblFinal" runat="server" Text='<%# Eval("FINAL") %>' ToolTip='<%# Eval("FINAL_DETAIN")%>'> </asp:Label>
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                    </asp:ListView>
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
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
