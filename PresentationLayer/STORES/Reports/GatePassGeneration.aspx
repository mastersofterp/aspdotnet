<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="GatePassGeneration.aspx.cs" Inherits="STORES_Reports_GatePassGeneration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script language="javascript" type="text/javascript">
        function confirmDeleteResult(v, m, f) {
            if (v) //user clicked OK 
                $('#' + f.hidID).click();
        }

    </script>

    <%--    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">GATE PASS/OUTWARD-RETURNABLE REPORT</h3>
                </div>
                <div class="box-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnl1" runat="server">
                                <div class="col-12">
                                    <div class="row">

                                        <div class="form-group col-12">

                                            <asp:RadioButtonList ID="rdlSelect" runat="server" RepeatDirection="Horizontal"
                                                AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="rdlSelect_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Selected="True">GatePassReport</asp:ListItem>
                                                <asp:ListItem Value="1">Outward-Returnable Report</asp:ListItem>
                                            </asp:RadioButtonList>


                                        </div>
                                        <div class="form-group col-12">
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <asp:Label ID="lblGp" runat="server" ForeColor="Red">*</asp:Label>
                                                    <label>Gate Pass Number :</label>
                                                </div>

                                                <asp:DropDownList ID="ddlGatePass" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Please Select main department name"
                                                    AppendDataBoundItems="true" TabIndex="1">
                                                </asp:DropDownList>
                                               <%-- <asp:RequiredFieldValidator ID="rfvddlDept" runat="server" ControlToValidate="ddlGatePass"
                                                    Display="None" ErrorMessage="Please Select Gate Pass Number" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="submit"></asp:RequiredFieldValidator>--%>

                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnReport" runat="server" Text="Show Report" CssClass="btn btn-info" ToolTip="Click To Print" ValidationGroup="submit" TabIndex="3" OnClick="btnReport_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click To Reset" TabIndex="4" OnClick="btnCancel_Click"/>
                                    <asp:ValidationSummary ID="valsum" runat="server" ValidationGroup="submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </asp:Panel>
                        </ContentTemplate>

                    </asp:UpdatePanel>

                </div>

            </div>

        </div>

    </div>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
