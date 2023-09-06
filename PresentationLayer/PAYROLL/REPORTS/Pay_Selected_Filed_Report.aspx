<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_Selected_Filed_Report.aspx.cs" Inherits="PAYROLL_REPORT_Pay_Selected_Filed_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .bootstrap-duallistbox-container .buttons {
            display: flex;
        }

        :focus {
            outline: 0;
        }

        table {
            border: 1px solid #f4f4f4;
        }

        th {
            background-color: #255282 !important;
            color: #fff !important;
            padding: 2px 8px;
            font-weight: 400 !important;
        }

        td {
            color: #333333 !important;
            background: #fff !important;
            padding: 2px 8px;
        }

        .arrow-top {
            margin-top: 75px;
        }

        @media (max-width:576px) {
            .arrow-top {
                margin-top: 10px;
            }
        }
    </style>

    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">EMPLOYEE SELECTED FIELD REPORT </h3>
                        </div>

                        <div class="box-body">
                            <div class="panel panel-info">

                                <div class="panel-body">
                                    <asp:Panel runat="server" ID="pnlgrdSelectFieldReport">
                                        <%--ScrollBars="Vertical"--%>
                                        <div class="table-responsive" style="height: 375px; overflow-Y: scroll;">
                                            <table class="table table-hover table-bordered" style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <br />
                                                        <asp:GridView ID="grdSelectFieldReport" CssClass="vista-grid" HeaderStyle-BackColor="ActiveBorder"
                                                            runat="server" AlternatingRowStyle-BackColor="#FFFFAA" Height="10px">
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlSelectionCriteria" runat="server">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>
                                                            <asp:Label ID="lblFieldsToSelect" runat="server" Font-Bold="true" Text="Fields to select"></asp:Label></label>
                                                    </div>
                                                    <asp:ListBox ID="lstFieldsToSelect" runat="server" SelectionMode="Multiple" style="height: 200px!important"
                                                        CssClass="form-control" TabIndex="1" ToolTip="Select Fields" AppendDataBoundItems="true"></asp:ListBox>
                                                </div>
                                                <div class="form-group col-lg-1 col-md-6 col-12 arrow-top text-center">
                                                    <asp:ImageButton ID="imgNextFieldsToSelect" runat="server" ImageUrl="~/Images/arrow2.jpg"
                                                        OnClick="imgNextFieldsToSelect_Click" TabIndex="2" ToolTip="Click to Transfer the Field" /><br />
                                                    <%--<img src="~/Images/arrow-border.jpg" alt="arrow-border" /><br />--%>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/arrow-border.jpg" />

                                                    <asp:ImageButton ID="imgPrevFieldsToSelect" runat="server" ImageUrl="~/Images/arrow1.jpg"
                                                        OnClick="imgPrevFieldsToSelect_Click" TabIndex="3" ToolTip="Click to Transfer the Field" CssClass="mt-2" />

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>
                                                            <asp:Label ID="lblSelectedFields" runat="server" Font-Bold="true" Text="Selected Fields"></asp:Label></label>
                                                    </div>
                                                    <asp:ListBox ID="lstSelectedFields" runat="server" SelectionMode="Multiple" style="height: 200px!important"
                                                        CssClass="form-control" AppendDataBoundItems="true" TabIndex="4" ToolTip="Select Fields"></asp:ListBox>
                                                </div>
                                                <div class="form-group col-lg-1 col-md-6 col-12 arrow-top text-center">

                                                    <asp:ImageButton ID="imgNextSelectedFields" runat="server" ImageUrl="~/Images/arrow2.jpg"
                                                        OnClick="imgNextSelectedFields_Click" TabIndex="5" ToolTip="Click to Transfer the Field" /><br />
                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/arrow-border.jpg" />
                                                    <asp:ImageButton ID="imgPrevSelectedFields" runat="server" ImageUrl="~/Images/arrow1.jpg"
                                                        OnClick="imgPrevSelectedFields_Click" TabIndex="6" ToolTip="Click to Transfer the Field" CssClass="mt-2" />

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>
                                                            <asp:Label ID="lblOrderBy" runat="server" Font-Bold="true" Text="Order By"></asp:Label></label>
                                                    </div>
                                                    <asp:ListBox ID="lstOrderBy" runat="server" SelectionMode="Multiple" style="height: 200px!important"
                                                        CssClass="form-control" AppendDataBoundItems="true" TabIndex="7" ToolTip="Select Fields"></asp:ListBox>
                                                </div>
                                                <div class="form-group col-lg-1 col-md-6 col-12 arrow-top text-center">
                                                    <asp:ImageButton ID="imgUpOrderBy" runat="server" ImageUrl="~/Images/uparrow.jpg"
                                                        OnClick="imgUpOrderBy_Click" TabIndex="8" ToolTip="Click to Transfer the Field" /><br />
                                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/arrow-border.jpg" />
                                                    <asp:ImageButton ID="imgDownOrderBy" runat="server" ImageUrl="~/Images/downarrow.jpg"
                                                        OnClick="imgDownOrderBy_Click" TabIndex="9" ToolTip="Click to Transfer the Field" CssClass="mt-2" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                Pay Status (Working Employee Only)
                                         <asp:DropDownList ID="ddlstatus" CssClass="form-control" runat="server" AppendDataBoundItems="true">
                                             <asp:ListItem Value="Y"> Working </asp:ListItem>
                                             <asp:ListItem Value="N"> Not Working </asp:ListItem>
                                             <asp:ListItem Value=""> Both </asp:ListItem>
                                         </asp:DropDownList>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="butCondition" runat="server" ValidationGroup="SelectedFieldReport"
                                    Text="Cick here for filters" OnClick="butCondition_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="butShowReport" runat="server" Text="Show Report" CssClass="btn btn-info" OnClick="butShowReport_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="SelectedFieldReport"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <div class="col-12">
                                <asp:Panel runat="server" ID="pnlFilters" ScrollBars="Vertical">
                                    <asp:GridView ID="grdvFilters" CssClass="vista-grid" HeaderStyle-BackColor="ActiveBorder"
                                        runat="server" EmptyDataText="No Records Found" AutoGenerateColumns="false" AlternatingRowStyle-BackColor="#FFFFAA" Width="100%">
                                        <Columns>
                                            <asp:BoundField HeaderText="Field Name" DataField="display_field_name" />
                                            <asp:TemplateField HeaderText="Condition">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlFilters" CssClass="form-control" runat="server" AppendDataBoundItems="true" data-select2-enable="true">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtFilter" runat="server" Text='<%# Eval("SFTRXNO") %>' CssClass="form-control" Visible="false"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                            <div class="col-12 btn-footer mt-3">
                                <asp:ImageButton ID="imgbutExporttoexcel" runat="server" ToolTip="Export to excel"
                                    ImageUrl="~/Images/excel.jpeg" Height="45px" Width="45px" Visible="false"
                                    OnClick="imgbutExporttoexcel_Click" />
                                <asp:ImageButton ID="imgbutExporttoWord" runat="server" ToolTip="Export to word"
                                    ImageUrl="~/Images/word.jpeg" Height="45px" Width="40px" Visible="false"
                                    OnClick="imgbutExporttoWord_Click" />
                                <asp:ImageButton ID="imgbutBack" runat="server" ToolTip="Back" ImageUrl="~/Images/arrow1.jpg" OnClick="btnBack_Click"  Visible="false"/>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgbutExporttoexcel" />
            <asp:PostBackTrigger ControlID="imgbutExporttoWord" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
