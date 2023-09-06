<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EmpPrescriptionDetails.aspx.cs" Inherits="Health_Report_EmpPrescriptionDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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
    </div>--%>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PRESCRIPTION DETAILS</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlEmpPrescription" runat="server">
                                <div class="col-12">
                                    <div class=" sub-heading">
                                        <h5>Employee Prescription Details</h5>
                                    </div>
                                        <asp:Panel ID="pnlEmpPrescriptionList" runat="server">
                                            <asp:ListView ID="lvPriscription" runat="server">
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <div class="sub-heading">
                                                            <h5>Prescription List</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>OPD DATE
                                                                    </th>
                                                                    <th>COMPLAINT
                                                                    </th>
                                                                    <th>FINDING
                                                                    </th>
                                                                    <th>VIEW
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <EmptyDataTemplate>
                                                    <div class="data_label text-center">
                                                        <label>-- No Record Found --</label>
                                                    </div>
                                                </EmptyDataTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%# Eval("OPDDATE", "{0:dd MMM, yyyy}")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("COMPLAINT")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("FINDING")%>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnView" runat="server" ToolTip="Click here To View Prescription"
                                                                CommandArgument='<%# Eval("OPDID")%>' CssClass="btn btn-info"
                                                                Text="Print" OnClick="btnView_Click" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                            <%--<div class="vista-grid_datapager">
                                                                <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvPriscription" PageSize="15"
                                                                    OnPreRender="dpPager_PreRender">
                                                                <Fields>
                                                                    <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                                        ShowLastPageButton="false" ShowNextPageButton="false" />
                                                                    <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                                    <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                                        ShowLastPageButton="true" ShowNextPageButton="true" />
                                                                </Fields>
                                                            </asp:DataPager>
                                                        </div>--%>
                                        </asp:Panel>
                                
                                </div>
                            </asp:Panel>
                        </div>
                    </div>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Alphabets allowed!");
                return false;
            }
            else
                return true;
        }
    </script>
</asp:Content>

