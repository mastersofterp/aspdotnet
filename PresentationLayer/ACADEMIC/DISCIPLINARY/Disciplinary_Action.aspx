<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Disciplinary_Action.aspx.cs" Inherits="DISCIPLINARY_Disciplinary_Action" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">DISCIPLINARY ACTION</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Event Date</label>
                                </div>
                                <asp:TextBox ID="txtEventDate" runat="server" TabIndex="3" ValidationGroup="submit"
                                    CssClass="form-control" />
                                <asp:Image ID="imgExamDate" runat="server" ImageUrl="~/images/calendar.png" />
                                <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtEventDate" PopupButtonID="imgExamDate" />
                                <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" TargetControlID="txtEventDate"
                                    Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                    MaskType="Date" />
                                <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" EmptyValueMessage="Please Enter Event Date"
                                    ControlExtender="meExamDate" ControlToValidate="txtEventDate" IsValidEmpty="false"
                                    InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Event Date"
                                    InvalidValueBlurredMessage="*" ValidationGroup="submit" SetFocusOnError="true" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Event Title</label>
                                </div>
                                <asp:TextBox ID="txtEventTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvBatchName" runat="server" ControlToValidate="txtEventTitle"
                                    Display="None" ErrorMessage="Please Enter Event Title" ValidationGroup="submit"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Event Category</label>
                                </div>
                                <asp:DropDownList ID="ddlEventCat" runat="server" CssClass="form-control" data-select2-enable="true"
                                    AppendDataBoundItems="true">
                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlEventCat"
                                    Display="None" ErrorMessage="Please Select Event Category." ValidationGroup="submit"
                                    SetFocusOnError="True" InitialValue="0">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Event Description</label>
                                </div>
                                <asp:TextBox ID="txtEventDesc" runat="server" TextMode="MultiLine" Rows="1"
                                    CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Student Involved</label>
                                </div>
                                <asp:TextBox ID="txtStudInvol" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStudInvol"
                                    Display="None" ErrorMessage="Please Enter Student Registration Number." ValidationGroup="add"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:Button ID="btnAdd" runat="server"
                                    Text="Add" ValidationGroup="add" OnClick="btnAdd_Click" Width="50px" />
                            </div>
                        </div>
                    </div>
                    <div class="col-12">
                        <asp:ListView ID="lvStudInvol" runat="server">
                            <LayoutTemplate>
                                <div id="demo-grid" class="vista-grid">
                                    <div class="sub-heading">
                                        <h5>STUDENTS INVOLVED</h5>
                                    </div>

                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>DELETE
                                                </th>
                                                <th>REG NO
                                                </th>
                                                <th>STUDENT NAME
                                                </th>
                                                <th>BRANCH
                                                </th>
                                                <th>SEMESTER
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
                                        <asp:ImageButton ID="btnDelete" runat="server" CommandArgument=' <%# Eval("IDNO")%>'
                                            ImageUrl="~/Images/delete.png" OnClick="btnDelete_Click" ToolTip="Delete Record" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblIDNO" runat="server" Text=' <%# Eval("REGNO")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStudName" runat="server" Text=' <%# Eval("Studname")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblLong" runat="server" Text='<%# Eval("longname")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSem" runat="server" Text=' <%# Eval("semestername")%>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnDelete" runat="server" CommandArgument=' <%# Eval("IDNO")%>'
                                            ImageUrl="~/Images/delete.png" OnClick="btnDelete_Click" ToolTip="Delete Record" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblIDNO" runat="server" Text=' <%# Eval("REGNO")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStudName" runat="server" Text=' <%# Eval("Studname")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblLong" runat="server" Text='<%# Eval("longname")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSem" runat="server" Text=' <%# Eval("semestername")%>' />
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>
                    </div>

                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Punishment/Action Taken</label>
                                </div>
                                <asp:TextBox ID="txtActTaken" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtActTaken"
                                    Display="None" ErrorMessage="Please Enter Action Taken." ValidationGroup="submit"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Authority Involved</label>
                                </div>
                                <asp:TextBox ID="txtAuthInvol" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAuthInvol"
                                    Display="None" ErrorMessage="Please Enter Authority Involved." ValidationGroup="submit"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" ValidationGroup="Acd" OnClick="btnCancel_Click" />

                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="add"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>
                </div>

            </div>
        </div>
    </div>

    <div id="divMsg" runat="server"></div>

</asp:Content>

