<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ComplaintCellMonitor.aspx.cs" Inherits="Complaints_TRANSACTION_ComplaintCellMonitor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">COMPLAINT CELL</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Complaint Cell
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnlDetails" runat="server" Visible="false">
                                                <div class="form-group row">
                                                    <div class="col-md-2">
                                                        <label>Complaint No.:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblComplaintNo" runat="server" Text=""></asp:Label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>Complaint Date:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblComplaintDate" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-2">
                                                        <label>Complaint:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblComplaint" runat="server" Text=""></asp:Label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>Complaintee Name:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblComplainteeName" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-2">
                                                        <label>Area:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblArea" runat="server" Text=""></asp:Label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>Location:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblComplainteeAddress" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-2">
                                                        <label>Contact No.:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblContactNo" runat="server" Text=""></asp:Label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>Other Contact No.:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblOtherContactNo" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-2">
                                                        <label>Complaint Nature:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblComplaintNature" runat="server" Text=""></asp:Label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>Work Priority:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblWorkPriority" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-2">
                                                        <label>Complaint Status:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblComplaintStatus" runat="server" Text=""></asp:Label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>WorkOut Date:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblWorkDate" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-2">
                                                        <label>WorkOut Details:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblWorkoutDetails" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-12 text-center">
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="6" />
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <asp:Panel ID="pnlList" runat="server">
                                                        <asp:ListView ID="lvComplaint" runat="server">
                                                            <EmptyDataTemplate>
                                                                <br />
                                                                <br />
                                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Complaints For Allotment"></asp:Label>
                                                            </EmptyDataTemplate>
                                                            <LayoutTemplate>
                                                                <div id="lgv1">
                                                                    <h4 class="box-title">Complaint Allotment</h4>
                                                                    <table class="table table-bordered table-hover">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <th>EDIT</th>
                                                                                <th>COMPLAINT NO.</th>
                                                                                <th>COMPLAINT DATE</th>
                                                                                <th>COMPLAINTEE NAME</th>
                                                                                <th>AREA</th>
                                                                                <th>LOCATION</th>
                                                                                <th>STATUS</th>
                                                                                <th>COMPLAINT TYPE</th>
                                                                                <th>WORK PRIORITY</th>
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
                                                                        <asp:ImageButton ID="btnEdit" runat="server" TabIndex="1" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                                                            CommandArgument='<%# Eval("COMPLAINTID") %>'
                                                                            ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                                    </td>
                                                                    <td><%# Eval("COMPLAINTNO")%></td>
                                                                    <td><%# Eval("COMPLAINTDATE","{0:dd-MMM-yyyy}")%></td>
                                                                    <td><%# Eval("COMPLAINTEE_NAME")%></td>
                                                                    <td><%# Eval("AREANAME")%></td>
                                                                    <td><%# Eval("COMPLAINTEE_ADDRESS")%></td>
                                                                    <td><%# Eval("COMPLAINTSTATUS")%></td>
                                                                    <td><%# Eval("TYPENAME")%></td>
                                                                    <td><%# Eval("PWNAME")%></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <div class="vista-grid_datapager">
                                                        <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvComplaint" PageSize="10"
                                                            OnPreRender="dpPager_PreRender">
                                                            <Fields>
                                                                <asp:NextPreviousPagerField ButtonType="Link" ShowPreviousPageButton="true" ShowNextPageButton="false" ShowLastPageButton="false" />
                                                                <asp:NumericPagerField ButtonCount="10" ButtonType="Link" />
                                                                <asp:NextPreviousPagerField ButtonType="Link" ShowPreviousPageButton="false" ShowNextPageButton="true" ShowLastPageButton="false" />
                                                            </Fields>
                                                        </asp:DataPager>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

