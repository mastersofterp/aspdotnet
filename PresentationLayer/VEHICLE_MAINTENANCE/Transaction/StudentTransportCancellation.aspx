<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentTransportCancellation.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_StudentTransportCancellation" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../jquery/jquery-3.2.1.min.js"></script>
    <link href="../../jquery/jquery.multiselect.css" rel="stylesheet" />
    <script src="../../jquery/jquery.multiselect.js"></script>
    <script type="text/javascript">
        
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
                                    <div class="form-group col-md-12">Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span> </span></div>
                                    <div class="form-group col-md-12">
                                        <div class="pannel panel-info">
                                            <div class="panel panel-heading">Student Details</div>
                                            <div class="panel panel-body">
                                                <%--      <div class="col-md-4">
                                                        <label><span style="color: red">*</span> Department :</label>
                                                        <asp:DropDownList ID="ddldept" runat="server" AppendDataBoundItems="true" CssClass="form-control"></asp:DropDownList>
                                                    </div>--%>
                                                <div class="form-group col-md-12">
                                                    <div class="col-md-4">
                                                        <label>Degree :</label>
                                                        <asp:DropDownList ID="ddldegree" runat="server" AppendDataBoundItems="true" CssClass="form-control"></asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddldegree" InitialValue="0" Display="None" ErrorMessage="Please Select Degree." ValidationGroup="Transport"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label>Branch :</label>
                                                        <asp:DropDownList ID="ddlbranch" runat="server" AppendDataBoundItems="true" CssClass="form-control"></asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ID="rfbranch" runat="server" ControlToValidate="ddlbranch" Display="None" InitialValue="0" ErrorMessage="Please Select Branch." ValidationGroup="Transport"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label>Year :</label>
                                                        <asp:DropDownList ID="ddlyear" runat="server" AppendDataBoundItems="true" CssClass="form-control"></asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlyear" InitialValue="0" Display="None" ErrorMessage="Please Select Year." ValidationGroup="Transport"></asp:RequiredFieldValidator>--%>
                                                    </div>

                                                </div>
                                                <div class="form-group col-md-12">

                                                    <div class="col-md-4">
                                                        <label><span style="color: red"></span>Semester :</label>
                                                        <asp:DropDownList ID="ddlsem" runat="server" AppendDataBoundItems="true" CssClass="form-control"></asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlsem" InitialValue="0" Display="None" ErrorMessage="Please Select Semester." ValidationGroup="Transport"></asp:RequiredFieldValidator>--%>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <label>Transport Type</label>
                                                        <asp:RadioButtonList ID="rdotrasnsporttyepe" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="C" Text="Regular &nbsp&nbsp" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="A" Text="Transport  &nbsp&nbsp"></asp:ListItem>
                                                            <asp:ListItem Value="E" Text="Transport Exempted"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-md-4" runat="server" visible="false"> 
                                                        <asp:DropDownList ID="ddlRegType" runat="server" AppendDataBoundItems="True" CssClass="form-control" TabIndex="2" ToolTip="Please Select Student Type">
                                                            <asp:ListItem Value="">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="0" Selected="True">Regular</asp:ListItem>
                                                            <asp:ListItem Value="1">Hosteler</asp:ListItem>
                                                            <%--<asp:ListItem Value="2">Transport</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                    </div>


                                                </div>
                                                <div class="form-group col-md-12 text-center">
                                                    <%-- <asp:Button ID="btnsave" Text="Submit" runat="server" CssClass="btn btn-primary" OnClick="btnsave_Click" ValidationGroup="Transport" />--%>
                                                    <asp:Button ID="btncancel" Text="Clear" runat="server" CssClass="btn btn-warning" OnClick="btncancel_Click" />
                                                    <asp:Button ID="btnReport" Text="Report" runat="server" CssClass="btn btn-info" OnClick="btnrpt_Click" />
                                                    <asp:Button ID="btnHostelerReport" Text="Hosteler Report" runat="server" CssClass="btn btn-info" OnClick="btnHostelerReport_Click" />
                                                    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Transport" DisplayMode="List" ShowSummary="false" ShowMessageBox="true" />
                                                </div>
                                                <div id="divMsg" runat="server">
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

    </asp:UpdatePanel>
</asp:Content>

