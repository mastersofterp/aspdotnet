<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentTransportStatusReport.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Reports_StudentTransportStatusReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../jquery/jquery-3.2.1.min.js"></script>
    <link href="../../jquery/jquery.multiselect.css" rel="stylesheet" />
    <script src="../../jquery/jquery.multiselect.js"></script>
    <script type="text/javascript">
        function Clear() {
            document.getElementById('<%=ddlBranch.ClientID%>').value = "0";
           document.getElementById('<%=ddlDegree.ClientID%>').value = "0";
           document.getElementById('<%=ddlSem.ClientID%>').value = "0";
       }
    </script>

    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">STUDENT TRANSPORT STATUS REPORT</h3>
                        </div>
                        <div>
                            <div class="box-body">
                                <div class="col-md-12">
                                    <div class="form-group col-md-12"></div>
                                    <div class="form-group col-md-12">
                                        <div class="pannel panel-info">
                                            <div class="panel panel-heading">Student Transport Status Report</div>
                                            <div class="panel panel-body">
                                                <div class="form-group col-md-12">


                                                    <div class="col-md-4">
                                                        <label>Select Degree</label>
                                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <label>Select Branch</label>
                                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>

                                                     <div class="col-md-4">
                                                        <label>Select Year</label>
                                                        <asp:DropDownList ID="ddlyear" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <div class="col-md-4">
                                                        <label>Select Semester</label>
                                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                      <div class="col-md-4">
                                                        <label>Select Status</label>
                                                        <asp:DropDownList ID="ddlstatus" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                                             <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                                             <asp:ListItem Text="Transport" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Hosteler" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Regular" Value="3"></asp:ListItem>
                                                        </asp:DropDownList>
                                                         
                                                      </div>
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <div style="text-align: center">
                                                        <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnReport_Click" />

                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" OnClientClick="Clear();" />
                                                    
                                                        <asp:Button ID="btnExcel" runat="server" Text="Excel Report" CssClass="btn btn-info" OnClick="btnExcel_Click" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>


