<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TransRequisitionPrincipalApproval.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_TransRequisitionPrincipalApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--   <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updApprove"
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
    <asp:UpdatePanel ID="updApprove" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TRANSPORT REQUISITION APPROVAL</h3>
                        </div>
                        <div class="box-body">
                            <div id="divGR" runat="server">
                                <div class="col-12">
                                    <asp:Panel ID="pnlDetails" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>College/School Name</label>
                                                </div>
                                                <asp:TextBox ID="txtInstitution" runat="server" MaxLength="70" Enabled="false" CssClass="form-control"></asp:TextBox>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Degree</label>
                                                </div>
                                                <asp:TextBox ID="txtDegree" runat="server" MaxLength="70" CssClass="form-control" Enabled="false"></asp:TextBox>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Branch</label>
                                                </div>
                                                <asp:TextBox ID="txtbranch" runat="server" MaxLength="50" Enabled="false" CssClass="form-control"></asp:TextBox>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Admission Batch </label>
                                                </div>
                                                <asp:TextBox ID="txtBatch" runat="server" MaxLength="70" CssClass="form-control" Enabled="false"></asp:TextBox>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Session</label>
                                                </div>
                                                <asp:TextBox ID="txtSession" runat="server" MaxLength="70" CssClass="form-control" Enabled="false"></asp:TextBox>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Semester</label>
                                                </div>
                                                <asp:TextBox ID="txtSemYear" runat="server" MaxLength="70" CssClass="form-control" Enabled="false"></asp:TextBox>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Student Name</label>
                                                </div>
                                                <asp:TextBox ID="txtstudname" runat="server" MaxLength="70" Enabled="false" CssClass="form-control" />

                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>TAN / PAN </label>
                                                </div>
                                                <asp:TextBox ID="txtAdmissionNo" runat="server" MaxLength="70" Enabled="false" CssClass="form-control"></asp:TextBox>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Application Date </label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"
                                                        MaxLength="10" Enabled="false" Style="z-index: 0;"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Vehicle Category </label>
                                                </div>
                                                <asp:TextBox ID="txtCategory" runat="server" MaxLength="50" Enabled="false" CssClass="form-control"></asp:TextBox>


                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Stop Name</label>
                                                </div>
                                                <asp:TextBox ID="txtStopName" runat="server" MaxLength="50" Enabled="false" CssClass="form-control"></asp:TextBox>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Fees Amount</label>
                                                </div>
                                                <asp:TextBox ID="txtFeesAmount" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Fees Amount"
                                                    Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Period From</label>
                                                </div>
                                                <asp:TextBox ID="txtPeriodfrom" runat="server" MaxLength="10" Enabled="false" CssClass="form-control"></asp:TextBox>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Period To </label>
                                                </div>
                                                <asp:TextBox ID="txtPeriodTo" runat="server" MaxLength="10" CssClass="form-control"
                                                    Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divSem" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Semester</label>
                                                </div>
                                                <asp:TextBox ID="txtsemester" runat="server" MaxLength="30" Enabled="false" CssClass="form-control"></asp:TextBox>

                                            </div>

                                        </div>
                                    </asp:Panel>
                                </div>

                            </div>
                            <div class="box-footer" id="divList" runat="server">
                                <div class="col-12">
                                    <asp:Panel ID="pnlTransport" runat="server">
                                        <asp:ListView ID="lvRequisition" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading">
                                                        <h5>Pending Requisition List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Select
                                                                </th>
                                                                <th>Student Name 
                                                                </th>
                                                                <th>TAN / PAN 
                                                                </th>
                                                                <th>Application Date 
                                                                </th>
                                                                <th>Category
                                                                </th>
                                                                <th>Stop
                                                                </th>
                                                                <th>Fees Amount
                                                                </th>
                                                                <th>Approved Amount
                                                                </th>
                                                                <th>Status
                                                                </th>
                                                                <th>Remark
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
                                                        <asp:CheckBox ID="chkStatus" runat="server" ToolTip='<%# Eval("STUD_IDNO") %>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("STUDNAME")%>
                                                   
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="btnDetail" runat="server" CommandArgument='<%# Eval("STUD_IDNO")%>' CommandName='<%# Eval("VTRAID")%>' Text='<%# Eval("ENROLLNO")%>'
                                                            Font-Underline="true" Font-Bold="true" OnClick="btnDetail_Click"></asp:LinkButton>
                                                        <asp:HiddenField ID="hdnStudIdNo" runat="server" Value='<%# Eval("STUD_IDNO")%>' />
                                                        <asp:HiddenField ID="hdnVTRAID" runat="server" Value='<%# Eval("VTRAID")%>' />

                                                        <asp:HiddenField ID="hdnSessionNo" runat="server" Value='<%# Eval("SESSIONNO")%>' />
                                                        <asp:HiddenField ID="hdnDegreeNo" runat="server" Value='<%# Eval("DEGREENO")%>' />
                                                        <asp:HiddenField ID="hdnBranchNo" runat="server" Value='<%# Eval("BRANCHNO")%>' />
                                                        <asp:HiddenField ID="hdnSemesterNo" runat="server" Value='<%# Eval("SEMESTERNO")%>' />
                                                        <asp:HiddenField ID="hdnYear" runat="server" Value='<%# Eval("YEAR")%>' />

                                                    </td>
                                                    <td>
                                                        <%# Eval("APP_DATE")%>
                                                   
                                                    </td>
                                                    <td>
                                                        <%# Eval("CATEGORYNAME")%>
                                                   
                                                    </td>
                                                    <td>
                                                        <%# Eval("STOPNAME")%>                                                    
                                                    </td>
                                                    <td>
                                                        <%# Eval("AMOUNT")%>                                                    
                                                    </td>
                                                    <td>
                                                        <%--<%# Eval("AMOUNT")%>   --%>
                                                        <asp:TextBox ID="txtFeesAmount" runat="server" Text='<%# Eval("AMOUNT") %>' CssClass="form-control"
                                                            MaxLength="20" ToolTip="Enter Fees"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeAmount" runat="server" FilterType="Custom, Numbers"
                                                            TargetControlID="txtFeesAmount" ValidChars=".">
                                                        </ajaxToolKit:FilteredTextBoxExtender>

                                                        <asp:HiddenField ID="hdnActualAmount" runat="server" Value='<%# Eval("AMOUNT")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Select Status" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Accept</asp:ListItem>
                                                            <asp:ListItem Value="2">Reject</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Eval("APPROVE_REMARK") %>' CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />

                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

