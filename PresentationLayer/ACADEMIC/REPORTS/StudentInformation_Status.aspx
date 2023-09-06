<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentInformation_Status.aspx.cs" Inherits="ACADEMIC_REPORTS_StudentInformation_Status" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="updCertificate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Student Information Status</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Select Admission Batch </label>
                                        </div>
                                        <asp:DropDownList ID="ddlStudentInfo" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true"
                                            TabIndex="1">
                                        </asp:DropDownList>

                                        <%--<asp:DropDownList ID="ddlStudentInfo" CssClass="form-control" runat="server" AppendDataBoundItems="true" ToolTip="Please Select Batch" TabIndex="1" data-select2-enable="true" AutoPostBack="true">
                                 <%--   <asp:ListItem Value="0">Please Select</asp:ListItem>                                    
                                </asp:DropDownList>--%>
                                        <asp:RequiredFieldValidator ID="admbatch" runat="server" ControlToValidate="ddlStudentInfo" ErrorMessage="Please Select Admission Batch" 
                                        Display="None"    InitialValue="0" ValidationGroup="report">
                                        </asp:RequiredFieldValidator>
                                      
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:LinkButton ID="lnReport" runat="server" ToolTip="Submithow Report"
                                            CssClass="btn btn-outline-info"  ValidationGroup="report" OnClick="lnReport_Click" TabIndex="10">Export To Excel</asp:LinkButton>
                                          <asp:ValidationSummary ID="ValidationSummary2" Enabled="true" AutoPostBack="false" runat="server" ShowMessageBox="True"
                                                            ShowSummary="False" ValidationGroup="report" Style="text-align: center" />
                                      
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnReport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

