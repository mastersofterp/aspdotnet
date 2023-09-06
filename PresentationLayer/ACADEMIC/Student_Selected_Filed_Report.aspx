<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Student_Selected_Filed_Report.aspx.cs" Inherits="Student_Selected_Filed_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlMain"
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
            background-color: #fff !important;
            color: #333 !important;
            padding: 2px 8px;
            font-weight:800 !important;
           
        }
        td {
            color: #333333 !important;
            background: #fff !important;
            padding: 2px 8px;
        }
        .arrow-top {
            margin-top:75px;
        }
        .lbheight {
            height:200px !important;
        }
        @media (max-width:576px) {
            .arrow-top {
                margin-top:10px;
            }
        }
    </style>

    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">STUDENTS SELECTED FIELD REPORT</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel runat="server" ID="pnlgrdSelectFieldReport">   <%--Height="400px" Width="1090px" ScrollBars="Vertical"--%>
                                    <div class="table-responsive" style="height:375px; overflow-Y:scroll;">
                                        <table class="table table-hover table-bordered"  style="width:100%;">
                                            <tr>
                                                <td>
                                                    <br />
                                                    <asp:GridView ID="grdSelectFieldReport" CssClass="vista-grid"
                                                        runat="server" Height="10px" Width="100%">
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                            </div>

                            <asp:Panel runat="server" ID="pnlSelectionCriteria">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><asp:Label ID="lblFieldsToSelect" runat="server" Font-Bold="true" Text="Fields to select"></asp:Label></label>
                                            </div>
                                            <asp:ListBox ID="lstFieldsToSelect" runat="server" SelectionMode="Multiple" 
                                                CssClass="form-control lbheight" AppendDataBoundItems="true"></asp:ListBox>
                                        </div>

                                        <div class="form-group col-lg-1 col-md-6 col-12 arrow-top text-center">
                                            <asp:ImageButton ID="imgNextFieldsToSelect" runat="server" ImageUrl="~/Images/arrow2.jpg"
                                                OnClick="imgNextFieldsToSelect_Click" /><br />
                                            <img src="../Images/arrow-border.jpg" alt="arrow-border"/><br />
                                            <asp:ImageButton ID="imgPrevFieldsToSelect" runat="server" ImageUrl="~/Images/arrow1.jpg"
                                                OnClick="imgPrevFieldsToSelect_Click" CssClass="mt-2"/>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><asp:Label ID="lblSelectedFields" runat="server" Font-Bold="true" Text="Selected Fields"></asp:Label></label>
                                            </div>
                                            <asp:ListBox ID="lstSelectedFields" runat="server" SelectionMode="Multiple" 
                                                AppendDataBoundItems="true" CssClass="form-control lbheight"></asp:ListBox>
                                        </div>

                                        <div class="form-group col-lg-1 col-md-6 col-12 arrow-top text-center">
                                            <asp:ImageButton ID="imgNextSelectedFields" runat="server" ImageUrl="~/Images/arrow2.jpg"
                                                OnClick="imgNextSelectedFields_Click" /><br />
                                            <img src="../Images/arrow-border.jpg" alt="arrow-border"/><br />
                                            <asp:ImageButton ID="imgPrevSelectedFields" runat="server" ImageUrl="~/Images/arrow1.jpg"
                                                OnClick="imgPrevSelectedFields_Click" CssClass="mt-2"/>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><asp:Label ID="lblOrderBy" runat="server" Font-Bold="true" Text="Order By"></asp:Label></label>
                                            </div>
                                            <asp:ListBox ID="lstOrderBy" runat="server" SelectionMode="Multiple" 
                                                AppendDataBoundItems="true" CssClass="form-control lbheight"></asp:ListBox>
                                        </div>

                                        <div class="form-group col-lg-1 col-md-6 col-12 arrow-top text-center">
                                            <asp:ImageButton ID="imgUpOrderBy" runat="server" ImageUrl="~/Images/uparrow.jpg"
                                                OnClick="imgUpOrderBy_Click" /><br />
                                            <img src="../Images/arrow-border.jpg" alt="arrow-border"/><br />
                                            <asp:ImageButton ID="imgDownOrderBy" runat="server" ImageUrl="~/Images/downarrow.jpg"
                                                OnClick="imgDownOrderBy_Click" CssClass="mt-2"/>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="butCondition" runat="server" ValidationGroup="SelectedFieldReport"
                                                Text="Cick here for filters" OnClick="butCondition_Click" CssClass="btn btn-primary" />
                                            <asp:Button ID="butShowReport" runat="server" Text="Show Report" OnClick="butShowReport_Click" CssClass="btn btn-info" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="SelectedFieldReport"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>

                                        <div class="col-12">
                                            <asp:Panel runat="server" ID="pnlFilters">  <%--Width="750px" Height="150px" ScrollBars="Vertical"--%>
                                                <asp:GridView ID="grdvFilters" CssClass="bg-light-blue" HeaderStyle-BackColor="ActiveBorder"
                                                    runat="server" EmptyDataText="No Records Found" AutoGenerateColumns="false" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Field Name" DataField="display_field_name" />
                                                        <asp:TemplateField HeaderText="Condition">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlFilters" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="txtFilter" runat="server" Text='<%# Eval("SFTRXNO") %>' Visible="false" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="col-12 btn-footer mt-3">
                                <asp:ImageButton ID="imgbutExporttoexcel" runat="server" ToolTip="Export to excel"
                                    ImageUrl="~/Images/excel.jpeg" Height="45px" Width="45px" OnClick="imgbutExporttoexcel_Click" />
                                <asp:ImageButton ID="imgbutExporttoWord" runat="server" ToolTip="Export to word"
                                    ImageUrl="~/Images/word.jpeg" Height="45px" Width="40px" OnClick="imgbutExporttoWord_Click" />
                                <asp:ImageButton ID="imgbutBack" runat="server" ToolTip="Back" ImageUrl="~/Images/arrow1.jpg" OnClick="btnBack_Click" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgbutExporttoexcel" />
            <asp:PostBackTrigger ControlID="imgbutExporttoWord" />
            <%--<asp:PostBackTrigger ControlID="imgbutBack" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
