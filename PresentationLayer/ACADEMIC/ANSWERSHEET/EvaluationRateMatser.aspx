<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EvaluationRateMatser.aspx.cs"
    Inherits="ACADEMIC_ANSWERSHEET_EvaluationRateMatser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="divMsg" runat="server"></div>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSession"
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

    <asp:UpdatePanel ID="updSession" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Evaluation Price Master</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label>
                                           <%-- <label>Degree</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" data-select2-enable="true" AppendDataBoundItems="true" CssClass="form-control"
                                            TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server"  ControlToValidate="ddlDegree" ValidationGroup="submit"
                                            ErrorMessage="Please Select Degree" InitialValue="0" Display="None"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Per Unit price</label>
                                            </div>
                                        <div>
                                            <asp:TextBox ID="txtPerUnit" runat="server" ValidationGroup="submit" MaxLength="4"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPerUnit" runat="server" ControlToValidate="txtPerUnit" ValidationGroup="submit"
                                                ErrorMessage="Please Enter Per Unit Price" Display="None"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="AjPerUnit" runat="server" FilterType="Numbers"
                                                TargetControlID="txtPerUnit">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class=" col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                    TabIndex="9" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="10" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                            </div>
                            <div class="col-12">
                                  <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                <asp:Repeater ID="lvSession" runat="server">
                                    <HeaderTemplate>
                                      
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Edit </th>
                                                    <th>Degree Name </th>
                                                    <th>Per Paper Rate </th>
                                                </tr>
                                                </thead>
                                                    <tbody>
                                                        <%--<tr id="itemPlaceholder" runat="server" />--%>
                                                   
                                             
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("SRNO")%>' ImageUrl="~/images/edit.png" OnClick="btnEdit_Click" TabIndex="12" ToolTip="Edit Record" />
                                            </td>
                                            <td><%# Eval("DEGREE") %></td>
                                            <td><%# Eval("PER_PAPER_RATE")%></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                         </tbody>
                                    </FooterTemplate>
                                </asp:Repeater>
                                </table>
                            </div>
                           
                        </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

