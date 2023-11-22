<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ConvocationMaster.aspx.cs" Inherits="ACADEMIC_EXAMINATION_ConvocationMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title">SESSION CREATION</h3>--%>
                    <h3 class="box-title">Convocation Question Master
                    </h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Convocation </label>
                                </div>
                                <asp:DropDownList ID="ddlConvocation" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCon" runat="server"
                                    ControlToValidate="ddlConvocation" Display="None"
                                    ErrorMessage="Please Select Convocation" InitialValue="0"
                                    SetFocusOnError="True" ValidationGroup="Submit">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Convocation Question</label>
                                </div>
                                <asp:TextBox ID="txtConvocationQuestion" runat="server" TextMode="MultiLine" CssClass="form-control"
                                    placeholder="Please Enter Convocation Question (Max. 500 char) ."
                                    ToolTip="Please Enter Feedback Question">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvQuestion" runat="server"
                                    ControlToValidate="txtConvocationQuestion" Display="None"
                                    ErrorMessage="Please Enter Convocation Question" SetFocusOnError="True"
                                    ValidationGroup="Submit">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>is Consider For Comment</label>
                                </div>
                                <asp:CheckBox ID="ChkCmt" runat="server" />
                            </div>

                        </div>
                    </div>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShow" runat="server" Text="Show"
                            TabIndex="0" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                    </div>
                    <div class="col-12" id="divansoption" runat="server" visible="false">
                        <div class="sub-heading">
                            <h5>Answer Options</h5>
                        </div>
                        <asp:GridView ID="gvAnswers" runat="server" AutoGenerateColumns="false"
                            ShowFooter="true" CssClass="table table-hovered table-bordered" OnRowCreated="gvAnswers_RowCreated">
                            <Columns>
                                <asp:BoundField DataField="RowNumber" HeaderText="Sr.No." ItemStyle-Width="200px" />



                                <asp:TemplateField HeaderText="Answers">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAnsOption" runat="server" MaxLength="150" class="form-control"
                                            ondrop="return false;" onpaste="return false;" onkeypress="return RestrictCommaSemicolon(event);" onkeyup="ConvertEachFirstLetterToUpper(this.id)">
                                        </asp:TextBox>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ControlStyle Width="300px"></ControlStyle>
                                    <ItemStyle Width="300px"></ItemStyle>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Value">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtValue" runat="server" MaxLength="5" class="form-control">
                                        </asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteValue" runat="server"
                                            FilterType="Custom" FilterMode="ValidChars" ValidChars="0.123456789" TargetControlID="txtValue">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ControlStyle Width="300px"></ControlStyle>
                                    <ItemStyle Width="300px"></ItemStyle>

                                    <FooterStyle />
                                    <FooterTemplate>
                                        <asp:Button ID="btnAdd" runat="server"
                                            Text="Add New Option" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                                    </FooterTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>

                                        <asp:ImageButton ID="lnkRemove" runat="server"
                                            ImageUrl="~/IMAGES/delete.png" AlternateText="Remove Row" OnClick="lnkRemove_Click"
                                            OnClientClick="return UserDeleteConfirmation();"></asp:ImageButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ControlStyle Width="20px"></ControlStyle>
                                    <ItemStyle Width="50px"></ItemStyle>
                                </asp:TemplateField>

                            </Columns>
                            <HeaderStyle CssClass="bg-light-blue" />
                        </asp:GridView>
                    </div>
                    <div class="col-12 btn-footer" runat="server" id="divbtns" visible ="false">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                            TabIndex="0" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="Submit" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                            TabIndex="0" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />
                    </div>
                    <div class="col-12">
                        <asp:ListView ID="lvQuestion" runat="server">
                            <LayoutTemplate>
                                <div class="sub-heading">
                                    <h5>Questions</h5>
                                </div>
                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <%-- <th>Sr.No.
                                                    </th>--%>
                                            <th>Edit
                                            </th>
                                            <th>Question
                                            </th>
                                            <th>Options
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
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record"
                                            CommandArgument='<%# Eval("QUESTIONID") %>' ImageUrl="~/images/Edit.png"
                                            ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                    </td>
                                    <td>
                                        <%# Eval("QUESTIONNAME")%>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rblOptions" runat="server" CssClass="radionButtonList"
                                            RepeatDirection="Horizontal" RepeatLayout="Flow" ToolTip="Options to be select">
                                        </asp:RadioButtonList>

                                        <asp:HiddenField ID="hdnOptions" runat="server"
                                            Value='<%# Eval("QUESTIONID") %>' />
                                    </td>

                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

