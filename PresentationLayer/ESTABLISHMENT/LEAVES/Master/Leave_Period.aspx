<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Leave_Period.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Master_Leave_Period" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>

    <script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table2').DataTable();
        });
    </script>

    <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };

        onkeypress = "return CheckAlphabet(event,this);"
        function CheckAlphabet(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }
    </script>

    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">LEAVE PERIOD</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Add/Edit LEAVE PERIOD</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Period Name</label>
                                            </div>
                                            <asp:TextBox ID="txtPeriodName" runat="server" MaxLength="20" CssClass="form-control"
                                                onkeypress="return CheckAlphabet(event,this);" TabIndex="1" ToolTip="Enter Period Name" />
                                            <asp:RequiredFieldValidator ID="rfvHolyType" runat="server" ControlToValidate="txtPeriodName"
                                                Display="None" ErrorMessage="Please Enter  Period Name " ValidationGroup="HolyType"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Period From</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPeriodFrom" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                TabIndex="2" ToolTip="Select Period From">
                                                <asp:ListItem Selected="True" Value="0" Text="Please Select"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPeriodFrom"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select Period From "
                                                ValidationGroup="HolyType" SetFocusOnError="True">                                                               
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Period To</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPeriodTo" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                TabIndex="3" ToolTip="Select Period To">
                                                <asp:ListItem Selected="True" Value="0" Text="Please Select"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlPeriodTo"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select Period To "
                                                ValidationGroup="HolyType" SetFocusOnError="True">                                                               
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                    <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                </div>
                            </asp:Panel>                        
                        <div class="col-12 btn-footer">
                            <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" Text="Add New"
                                CssClass="btn btn-primary" ToolTip="Click here to Add New Leave Period" TabIndex="4"></asp:LinkButton>
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="HolyType" OnClick="btnSave_Click"
                                CssClass="btn btn-primary" ToolTip="Click here to Submit" TabIndex="5" />
                            <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click"
                                TabIndex="6" CssClass="btn btn-primary" ToolTip="Click here to Return to Previous Menu" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="7"
                                OnClick="btnCancel_Click" ToolTip="Click here to Reset" CssClass="btn btn-warning" />

                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="HolyType"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </div>
                        <div class="col-12">
                            <asp:Panel ID="pnlList" runat="server">
                                <asp:ListView ID="lvPeriod" runat="server">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Click Add New To Enter Leave Period" CssClass="d-block text-center mt-3" />
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Leave Period List</h5>
                                        </div>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                    <th>PERIOD NAME
                                                    </th>
                                                    <th>PERIOD FROM
                                                    </th>
                                                    <th>PERIOD TO
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("PERIOD") %>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="8" />&nbsp;
                                            </td>
                                            <td>
                                                <%# Eval("PERIOD_NAME") %>   
                                            </td>
                                            <td>
                                                <%# Eval("PERIOD_FROM") %>                                         
                                                
                                            </td>
                                            <td>
                                                <%# Eval("PERIOD_TO") %>    
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                                <div class="vista-grid_datapager d-none">
                                    <div class="text-center">
                                        <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvPeriod" PageSize="1000"
                                            OnPreRender="dpPager_PreRender">
                                            <Fields>
                                                <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                                <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
                                                <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                    ShowLastPageButton="true" ShowNextPageButton="true" />

                                            </Fields>
                                        </asp:DataPager>
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
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

