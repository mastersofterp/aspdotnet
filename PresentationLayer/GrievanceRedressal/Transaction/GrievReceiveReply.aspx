<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="GrievReceiveReply.aspx.cs" Inherits="GrievanceRedressal_Transaction_GrievReceiveReply" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../../includes/prototype.js" type="text/javascript"></script>
    <script src="../../includes/scriptaculous.js" type="text/javascript"></script>
    <script src="../../includes/modalbox.js" type="text/javascript"></script>--%>

    <%--<script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updGrievReceiveReply"
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

    <asp:UpdatePanel ID="updGrievReceiveReply" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <%-- <div class="box-header with-border">
                            <h3 class="box-title">GRIEVANCE STATUS</h3>
                        </div>--%>

                        <div class="box-body">
                            <asp:Panel ID="pnlRply" runat="server">
                                <div id="divDetails" runat="server" visible="false">
                                    <asp:Panel ID="pnlreply" runat="server">
                                        <div class="col-12">
                                            <div class="row">
                                                <%--<div class="col-12">
                                                <div class="sub-heading"><h5>Grievance Status</h5></div>
                                            </div>--%>

                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Grievance :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblGrievance" runat="server" Font-Bold="true"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Reply :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblReply" runat="server" Font-Bold="true"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Grievance Type :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblGTtype" runat="server" Font-Bold="true"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>

                                                <div class="form-group col-lg-4 col-md-6 col-12" id="Remark" runat="server" visible="true">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Remark</label>
                                                    </div>
                                                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvremark" runat="server" ControlToValidate="txtRemark"
                                                        Display="None" ErrorMessage="Please Enter Remark" ValidationGroup="GrievanceValidate"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-4 col-md-6 col-12" id="checkUnCheck" runat="server" visible="true">
                                                    <div class="label-dynamic">
                                                        <label></label>
                                                    </div>
                                                    <asp:RadioButton ID="rbtSatified" runat="server" Checked="true" Text="Satisfied" TabIndex="3" AutoPostBack="true"
                                                        OnCheckedChanged="rbtSatified_CheckedChanged" />
                                                    <asp:RadioButton ID="rbtNotSatisfied" runat="server" Checked="false" Text="Not Satisfied" AutoPostBack="true"
                                                        TabIndex="4" OnCheckedChanged="rbtNotSatisfied_CheckedChanged" />
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>

                                <div class="col-12" id="divGR" runat="server">
                                    <asp:Panel ID="pnlAdd" runat="server">
                                        <%-- <div class="sub-heading"><h5>Grievance Status</h5></div>--%>
                                        <asp:Panel ID="pnlGrievance" runat="server">
                                            <asp:ListView ID="lvGrApplication" runat="server">
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <div class="sub-heading">
                                                            <h5>Grievance Status List</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Grievance By  
                                                                    </th>
                                                                    <th>Grievance Type
                                                                    </th>
                                                                    <th>Grievance Detail
                                                                    </th>
                                                                    <th>Grievance Date  
                                                                    </th>
                                                                    <th>Reply By
                                                                    </th>
                                                                    <th>Reply Date
                                                                    </th>
                                                                    <th>View 
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" style="width: 50%" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>

                                                        <td>
                                                            <%# Eval("GRIEVANCE_BY")%>                                                   
                                                        </td>
                                                        <td>
                                                            <%# Eval("GT_NAME")%>                                                   
                                                        </td>
                                                        <td>
                                                            <%# Eval("GRIEVANCE")%> 
                                                                                                                        
                                                        </td>
                                                        <td>
                                                            <%# Eval("GR_APPLICATION_DATE", "{0:dd-MM-yyyy}")%>                                                   
                                                        </td>
                                                        <td>
                                                            <%# Eval("GR_COMMITTEE")%>   <%-- REPLY_BY             --%>                                  
                                                        </td>
                                                        <td>
                                                            <%# Eval("REPLY_DATE", "{0:dd-MM-yyyy}")%>                                                    
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnDetails" runat="server" CommandArgument='<%# Eval("GAID") %>' CssClass="btn btn-primary"
                                                                OnClick="btnDetails_Click" CommandName='<%# Eval("REPLY_ID") %>' ToolTip='<%# Eval("GAT_ID") %>'
                                                                Enabled='<%#Eval("COMMITTEE_STATUS").ToString() == "C" ? false : true %>'
                                                                Text='<%#Eval("COMMITTEE_STATUS").ToString() == "P" ? "Receive" : "Details"  %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </asp:Panel>
                                </div>

                                <div class="col-12 btn-footer" id="divButton" runat="server" visible="false">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="GrievanceValidate"
                                        CssClass="btn btn-primary" TabIndex="10" ToolTip="Click here to Submit"
                                        OnClick="btnSave_Click" />

                                    <asp:Button ID="btnBack" runat="server" CausesValidation="false" Visible="false"
                                        Text="Back" CssClass="btn btn-primary" TabIndex="11" ToolTip="Click here to Go back to Previous Menu" />

                                    <asp:Button ID="btnForward" runat="server" Text="Forward" Visible="false" ValidationGroup="GrievanceValidate"
                                        CssClass="btn btn-primary" TabIndex="11" ToolTip="Click here to Submit" OnClick="btnForward_Click" />

                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        CssClass="btn btn-warning" TabIndex="12" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="GrievanceValidate"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlGrivTypeRply" runat="server">
                                <div id="divGriv" runat="server" visible="false">
                                    <asp:Panel ID="pnlSub" runat="server">
                                        <div class="col-12">
                                            <div class="row">
                                                <%--<div class="col-12">
                                                <div class="sub-heading"><h5>Grievance Status</h5></div>
                                            </div>--%>

                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Grievance :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblGDetail" runat="server" Font-Bold="true"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Reply :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblGReply" runat="server" Font-Bold="true"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Grievance Type :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblGT" runat="server" Font-Bold="true"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>

                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Sub Grievance Type :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblSubGt" runat="server" Font-Bold="true"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>

                                                <div class="form-group col-lg-4 col-md-6 col-12" id="Div3" runat="server" visible="true">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Remark</label>
                                                    </div>
                                                    <asp:TextBox ID="txtGMark" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGMark"
                                                        Display="None" ErrorMessage="Please Enter Remark" ValidationGroup="GrivValidate"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-4 col-md-6 col-12" id="Div4" runat="server" visible="true">
                                                    <div class="label-dynamic">
                                                        <label></label>
                                                    </div>
                                                    <asp:RadioButton ID="rdSat" runat="server" Checked="true" Text="Satisfied" TabIndex="3" AutoPostBack="true"
                                                        OnCheckedChanged="rdSat_CheckedChanged" />
                                                    <asp:RadioButton ID="rdUnSat" runat="server" Checked="false" Text="Not Satisfied" AutoPostBack="true"
                                                        TabIndex="4" OnCheckedChanged="rdUnSat_CheckedChanged" />
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>

                                <div class="col-12" id="divType" runat="server">
                                    <asp:Panel ID="pnlSt" runat="server">
                                        <%-- <div class="sub-heading"><h5>Grievance Status</h5></div>--%>
                                        <asp:Panel ID="pnlTyp" runat="server">
                                            <asp:ListView ID="lvSubGT" runat="server">
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <div class="sub-heading">
                                                            <h5>Grievance Status List</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Grievance By  
                                                                    </th>
                                                                    <th>Grievance Type
                                                                    </th>
                                                                    <th>Grievance Detail
                                                                    </th>
                                                                    <th>Grievance Date  
                                                                    </th>
                                                                    <%-- <th>Reply By
                                                                    </th>--%>
                                                                    <th>Reply Date
                                                                    </th>
                                                                    <th>Student Reply
                                                                    </th>
                                                                    <th>Authority Status
                                                                    </th>
                                                                    <th>View 
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" style="width: 50%" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>

                                                        <td>
                                                            <%# Eval("GRIEVANCE_BY")%>                                                   
                                                        </td>
                                                        <td>
                                                            <%# Eval("GT_NAME")%>                                                   
                                                        </td>
                                                        <td>
                                                            <%# Eval("GRIEVANCE")%> 
                                                                                                                        
                                                        </td>
                                                        <td>
                                                            <%# Eval("GR_APPLICATION_DATE", "{0:dd-MM-yyyy}")%>                                                   
                                                        </td>
                                                        <%--<td>
                                                            <%# Eval("GR_COMMITTEE")%>   <%-- REPLY_BY             --%>
                                                        <%--</td>--%>
                                                        <td>
                                                            <%# Eval("REPLY_DATE", "{0:dd-MM-yyyy}")%>                                                    
                                                        </td>
                                                        <td>
                                                            <%# Eval("STUD_REMARK")%>                                                    
                                                        </td>
                                                        <td>
                                                            <%# Eval("AuthortiyStatus")%>                                                    
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnReplyDet" runat="server" CommandArgument='<%# Eval("GAID") %>' CssClass="btn btn-primary"
                                                                CommandName='<%# Eval("REPLY_ID") %>' ToolTip='<%# Eval("GAT_ID") %>' OnClick="btnReplyDet_Click" Enabled='<%#Eval("COMMITTEE_STATUS").ToString() == "C" ? false : true %>'
                                                                Text='<%#Eval("COMMITTEE_STATUS").ToString() == "P" ? "Receive" : "Details"  %>' />

                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </asp:Panel>
                                </div>

                                <div class="col-12 btn-footer" id="divbtn" runat="server" visible="false">
                                    <asp:Button ID="btnSaveReply" runat="server" Text="Submit" ValidationGroup="GrivValidate"
                                        CssClass="btn btn-primary" TabIndex="10" ToolTip="Click here to Submit"
                                        OnClick="btnSaveReply_Click" />

                                    <asp:Button ID="btnReplyBack" runat="server" CausesValidation="false" Visible="false"
                                        Text="Back" CssClass="btn btn-primary" TabIndex="11" ToolTip="Click here to Go back to Previous Menu" />

                                    <asp:Button ID="btnForwrdReply" runat="server" Text="Forward" Visible="false" ValidationGroup="GrivValidate"
                                        CssClass="btn btn-primary" TabIndex="11" ToolTip="Click here to Submit" OnClick="btnForwrdReply_Click" />

                                    <asp:Button ID="btnClear" runat="server" Text="Cancel" CausesValidation="false"
                                        CssClass="btn btn-warning" TabIndex="12" ToolTip="Click here to Reset" OnClick="btnClear_Click" />

                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="GrivValidate"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>
