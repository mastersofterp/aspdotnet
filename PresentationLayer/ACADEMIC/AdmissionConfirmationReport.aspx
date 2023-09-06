<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AdmissionConfirmationReport.aspx.cs" Inherits="ACADEMIC_AdmissionConfirmationReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSelection"
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

    <asp:UpdatePanel ID="updSelection" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Admission Confirmation Report</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                            Display="None" ValidationGroup="Show" InitialValue="0"
                                            ErrorMessage="Please Select Admission Batch"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvddlAdmBatch1" runat="server" ControlToValidate="ddlAdmBatch"
                                            Display="None" ValidationGroup="Report" InitialValue="0"
                                            ErrorMessage="Please Select Admission Batch"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="2">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ValidationGroup="Show" InitialValue="0"
                                            ErrorMessage="Please Select Degree"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvddlDegree1" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ValidationGroup="Report" InitialValue="0"
                                            ErrorMessage="Please Select Degree"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="3" >
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSem" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvddlSem1" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" TabIndex="4" ValidationGroup="Show" OnClick="btnShow_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="5" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Show"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Report"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-12">
                                <asp:Panel runat="server" ID="pnllist">
                                    <asp:ListView ID="lvStudent" runat="server" OnItemCommand="lvStudent_ItemCommand">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Student Not found" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Student List </h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th >SrNo
                                                        </th>
                                                        <th >Name
                                                        </th>
                                                        <th >Degree
                                                        </th>
                                                        <th >Semester
                                                        </th>
                                                        <th >Admission Date
                                                        </th>

                                                        <%--   <th style="width: 15%" align="left">Receipt Date
                                                        </th>--%>                                                                
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                                    <%-- <tr id="itemPlaceholder" runat="server" />--%>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td >
                                                    <%# Container.DataItemIndex + 1%>
                                                </td>
                                                <td >
                                                    <%# Eval("NAME")%>
                                                </td>
                                                <td >
                                                    <%# Eval("DEGREENAME")%>
                                                </td>
                                                <td >
                                                    <%# Eval("SEMESTERNAME")%>
                                                </td>
                                                <td >
                                                    <%# Eval("admdate")%>
                                                </td>
                                                <%--   <td style="width: 15%"  >
                                                    <%# Eval("REC_DT")%>
                                                </td>--%>

                                                <td style="text-align:center">
                                                    <asp:Button ID="btnConfirm" runat="server"  CssClass="btn btn-info" CommandName="Report" CommandArgument='<%# Eval("IDNO")%>' Text='<%# (Convert.ToInt32(Eval("admcan"))==1 ?  "Confirm" : "Print") %>' ToolTip='<%# (Convert.ToInt32(Eval("admcan"))==1 ?  "1" : "0") %>' /> 

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr class="altitem">
                                                <td >
                                                    <%# Container.DataItemIndex + 1%>
                                                </td>
                                                <td >
                                                    <%# Eval("NAME")%> 
                                                </td>
                                                <td >
                                                    <%# Eval("DEGREENAME")%>
                                                </td>
                                                <td >
                                                    <%# Eval("SEMESTERNAME")%>
                                                </td>
                                                <td >
                                                    <%# Eval("admdate")%>
                                                </td>
                                                <%-- <td style="width: 15%">
                                                    <%# Eval("REC_DT")%>
                                                </td>--%>

                                                <td style="text-align:center">
                                                    <asp:Button ID="btnConfirm" runat="server"  CssClass="btn btn-info" CommandName="Report" CommandArgument='<%# Eval("IDNO")%>' Text='<%# (Convert.ToInt32(Eval("admcan"))==1 ?  "Confirm" : "Print") %>' ToolTip='<%# (Convert.ToInt32(Eval("admcan"))==1 ?  "1" : "0") %>' />

                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

