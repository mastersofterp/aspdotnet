<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudExamTransApproval.aspx.cs" Inherits="ACADEMIC_EXAMINATION_StudExamTransApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <%--<div>
        <asp:UpdateProgress ID="updDepart" runat="server" AssociatedUpdatePanelID="updexamtrans"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        &nbsp;<div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updexamtrans" runat="server">
        <ContentTemplate>--%>
            <div class="col-sm-12">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title"><b>STUDENT EXAM TRANSACTION APPROVAL </b></h3>

                    </div>

                    <div class="box-tools pull-right">
                        <div style="color: Red; font-weight: bold">
                            &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                        </div>
                    </div>

                    <div class="box-body">
                        <div class="col-12">
                            <div class="row">

                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>
                                                    <sup>* </sup>Session :
                                                </label>
                                            </div>
                                            <asp:DropDownList runat="server" ID="ddlSession" TabIndex="1" Width="100%" AppendDataBoundItems="true"
                                                ToolTip="Please Select Session." CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Select Session."
                                                ControlToValidate="ddlsession" Display="None" InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="regsubmit" />
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Session."
                                                ControlToValidate="ddlsession" Display="None" InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="ExReport" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>
                                                    Branch :
                                                </label>
                                            </div>
                                            <asp:DropDownList runat="server" ID="ddlBranch" TabIndex="2" Width="100%" AppendDataBoundItems="true"
                                                ToolTip="Please Select Branch." CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Semester."
                                                ControlToValidate="ddlsession" Display="None" InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="regsubmit" />--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>
                                                    Semester :
                                                </label>
                                            </div>
                                            <asp:DropDownList runat="server" ID="ddlSemester" TabIndex="3" Width="100%" AppendDataBoundItems="true"
                                                ToolTip="Please Select Semester." CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Semester."
                                                ControlToValidate="ddlsession" Display="None" InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="regsubmit" />--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>
                                                    Status :
                                                </label>
                                            </div>
                                            <asp:DropDownList runat="server" ID="ddlstatus" TabIndex="4" Width="100%" AppendDataBoundItems="true"
                                                ToolTip="Please Select Status." CssClass="form-control">
                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                <asp:ListItem Value="0">Pending</asp:ListItem>
                                                <asp:ListItem Value="1">Approve</asp:ListItem>
                                                <asp:ListItem Value="2">Reject</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <br />

                        <div class="box-footer" runat="server" id="Div1">
                            <p class="text-center">
                                <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" class="btn btn-primary" TabIndex="5" ValidationGroup="regsubmit"></asp:Button>

                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" class="btn btn-danger" TabIndex="6"></asp:Button>

                                <asp:Button ID="btnExReport" runat="server" Text="Excel-Report" CssClass="btn btn-info" OnClick="btnExReport_Click" TabIndex="7" ValidationGroup="ExReport" />

                                 <asp:Button ID="btnExremainlist" runat="server" Text="Remaining Student List" CssClass="btn btn-info" OnClick="btnExremainlist_Click" TabIndex="8"
                                     ValidationGroup="ExReport"/>

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="regsubmit"></asp:ValidationSummary>
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="ExReport"></asp:ValidationSummary>
                            </p>
                        </div>


                        <div class="col-sm-12 table-responsive">
                            <asp:Panel ID="pnlSeqNum" runat="server">
                                <asp:ListView ID="lvltransapprov" runat="server">
                                    <LayoutTemplate>
                                        <div id="demo-grid">
                                            <h4>Student Transaction Details</h4>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="mytable">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="width: 5%">Sr.No.
                                                        </th>
                                                        <th style="width: 10%">PRN No.
                                                        </th>
                                                        <th style="width: 20%">Name
                                                        </th>
                                                        <th style="width: 5%">Gender
                                                        </th>
                                                        <th style="width: 5%">Sem
                                                        </th>
                                                        <th style="width: 10%">Degree
                                                        </th>
                                                        <th style="width: 10%">Trans. ID
                                                        </th>
                                                        <th style="width: 10%">Amount
                                                        </th>
                                                        <th style="width: 10%">Trans. Date 
                                                        </th>
                                                        <th style="width: 10%">Status
                                                        </th>
                                                        <th style="width: 5%">Preview
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server"></tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td style="width: 5%">
                                                <%# Container.DataItemIndex + 1%>
                                            </td>
                                            <td style="width: 10%">
                                                <u style="color: #3c8dbc">
                                                    <asp:LinkButton runat="server" ID="lnkrollno" Text='<%# Eval("REGNO") %>' CommandName='<%#Eval("IDNO") %>'
                                                        OnClick="lnkrollno_Click" ToolTip="Click Here"></asp:LinkButton></u>
                                                <%-- <%# Eval("ROLLNO") %>--%>      
                                            </td>
                                            <td style="width: 20%">
                                                <%# Eval("STUDNAME") %>
                                            </td>
                                            <td style="width: 5%">
                                                <%# Eval("GENDER") %>
                                            </td>
                                            <td style="width: 5%">
                                                <%# Eval("SEMESTERNAME") %>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("DEGREE") %>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("TRANSACTION_NO") %>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("TRANSACTION_AMT") %>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("TRANS_DATE") %>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("STATUS") %>
                                                <asp:HiddenField ID="HdnStatus" runat="server" Value='<%#Eval("APPROVAL_STATUS") %>' />
                                            </td>
                                            <td style="width: 5%">
                                                
                                        <asp:UpdatePanel runat="server" ID="updpreview">
                                            <ContentTemplate>
                                                <asp:ImageButton ID="lnkTransDoc" ImageUrl="../../Images/Preview.png" runat="server" OnClick="lnkTransDoc_Click" Text="Preview" data-toggle="modal" data-target="#preview" TabIndex="6" ToolTip='<%#Eval("DOC_NAME") %>'></asp:ImageButton>
                                                <asp:HiddenField runat="server" ID="hftransdoc" />
                                            </ContentTemplate>
                                            <Triggers>
                                              <asp:AsyncPostBackTrigger ControlID="lnkTransDoc" EventName="click" />
                                              <%--  <asp:PostBackTrigger ControlID="lnkTransDoc" />--%>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                            </td>
                                        </tr>

                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

    <div class="modal fade" id="preview" role="dialog" style="display: none; margin-left: -100px;">
                <div class="modal-dialog text-center">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <!-- Modal content-->
                            <div class="modal-content" style="width: 700px;">
                                <div class="modal-header">
                                    <%--   <button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                                    <h4 class="modal-title">Document</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="col-md-12">

                                        <asp:Literal ID="ltEmbed" runat="server" />

                                        <%--<iframe runat="server" style="width: 100; height: 100px" id="iframe2"></iframe>--%>

                                        <%--<asp:Image ID="imgpreview" runat="server" Height="530px" Width="600px"  />--%>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
       <%-- </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="btnExReport" />
              <asp:PostBackTrigger ControlID="btnExremainlist" />
        </Triggers>
    </asp:UpdatePanel>--%>


</asp:Content>


