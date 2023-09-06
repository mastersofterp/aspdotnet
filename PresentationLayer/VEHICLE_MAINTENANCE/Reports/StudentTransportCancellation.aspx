<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentTransportCancellation.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_StudentTransportCancellation" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../jquery/jquery-3.2.1.min.js"></script>
    <link href="../../jquery/jquery.multiselect.css" rel="stylesheet" />
    <script src="../../jquery/jquery.multiselect.js"></script>
    <script type="text/javascript">
        
    </script>--%>
    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">STUDENT TRANSPORT STATUS REPORT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class=" sub-heading">
                                    <h5>Student Details</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddldegree" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"></asp:DropDownList>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlbranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"></asp:DropDownList>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlyear" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"></asp:DropDownList>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlsem" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"></asp:DropDownList>

                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Transport Type</label>
                                        </div>
                                        <asp:RadioButtonList ID="rdotrasnsporttyepe" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="C" Text="Transport Cancel &nbsp&nbsp" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="A" Text="Transport Requested/Opted  &nbsp&nbsp"></asp:ListItem>
                                            <asp:ListItem Value="E" Text="Transport Exempted"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                        <asp:DropDownList ID="ddlRegType" runat="server" AppendDataBoundItems="True" CssClass="form-control" TabIndex="2" ToolTip="Please Select Student Type">
                                            <asp:ListItem Value="">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0" Selected="True">Regular</asp:ListItem>
                                            <asp:ListItem Value="1">Hosteler</asp:ListItem>
                                            <%--<asp:ListItem Value="2">Transport</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <%-- <asp:Button ID="btnsave" Text="Submit" runat="server" CssClass="btn btn-primary" OnClick="btnsave_Click" ValidationGroup="Transport" />--%>
                                <asp:Button ID="btncancel" Text="Clear" runat="server" CssClass="btn btn-warning" OnClick="btncancel_Click" />
                                <asp:Button ID="btnReport" Text="Report" runat="server" CssClass="btn btn-info" OnClick="btnrpt_Click" />
                                <asp:Button ID="btnHostelerReport" Text="Hostel Cancel Report" runat="server" CssClass="btn btn-info" OnClick="btnHostelerReport_Click" />
                                <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Transport" DisplayMode="List" ShowSummary="false" ShowMessageBox="true" />
                            </div>
                            <div id="divMsg" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

