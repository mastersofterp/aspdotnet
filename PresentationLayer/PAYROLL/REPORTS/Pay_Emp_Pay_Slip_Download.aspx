<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_Emp_Pay_Slip_Download.aspx.cs" Inherits="PAYROLL_REPORTS_Pay_Emp_Pay_Slip_Download" %>
<%@ Register Assembly ="CrystalDecisions.Web,Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SEND  PAY SLIP ON EMAIL </h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlSelect" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Month / Year</label>
                                                </div>
                                                <asp:DropDownList ID="ddlMonthYear" runat="server" CssClass="form-control" AppendDataBoundItems="True" data-select2-enable="true"
                                                    TabIndex="1" ToolTip="Select Month/Year" OnSelectedIndexChanged="ddlMonthYear_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvMonthYear" runat="server" SetFocusOnError="true"
                                                    ControlToValidate="ddlMonthYear" Display="None" ErrorMessage="Please Select Month/Year"
                                                    ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Select College"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                                data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Staff</label>--%>
                                                <label>Scheme/Staff</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Select Staff"
                                                 AutoPostBack="true" data-select2-enable="true" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                                Display="None" ErrorMessage="Please Select Scheme/Staff" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <%--<sup>* </sup>--%>
                                                <label>Employee Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlEmpType" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpType_SelectedIndexChanged"
                                                TabIndex="45">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlEmpType" ValidationGroup="payroll"
                                                ErrorMessage="Please select Employee Type" SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>--%>
                                        </div>
                                          <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Order By</label>
                                            </div>
                                            <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Select Order By"
                                                AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">IDNO</asp:ListItem>
                                                <asp:ListItem Value="2">SEQUENCE NO</asp:ListItem>
                                               <%-- <asp:ListItem Value="3">DOJ</asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlorderby"
                                                Display="None" ErrorMessage="Please Select Order By" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                         <div class="col-12 btn-footer">
                                              <asp:Button ID="btnshow" runat="server" Text=" Show" ToolTip="Click to Show Employee" ValidationGroup="payroll" CssClass="btn btn-primary" TabIndex="6"
                                                OnClick="btnshow_Click" />
                                             <asp:Button ID="btnsend" runat="server" Text="Send Email" ToolTip="Click to Save" ValidationGroup="payroll" CssClass="btn btn-primary" TabIndex="6" Visible="false"
                                              OnClick="btnsend_Click" />
                                             <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Click to Reset" CausesValidation="false"
                                              OnClick ="btnCancel_Click" CssClass="btn btn-warning" TabIndex="7" />
                                             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                             ShowMessageBox ="true" ShowSummary="false" DisplayMode="List" />
                                         </div>

                                </div>
                            </asp:Panel>
                            <div class="col-12">
                                <asp:Panel ID="pnlSeqNum" runat="server">
                                    <asp:ListView ID="lvEmployeeMonth" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Employees available in this month" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Employee Details</h5>
                                            </div>
                                       <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            Select
                                                        </th>
                                                        <th>
                                                            Sr No.
                                                        </th>
                                                        <th>Name
                                                        </th>
                                                        <th>Designation
                                                        </th>
                                                        <th>Department
                                                        </th>
                                                    </tr>
                                                    <thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkid" runat="server"  />
                                                </td>
                                                <td>
                                                    <%#: Container.DataItemIndex + 1 %>
                                                   <%-- <%#Eval("IDNO")%>--%>
                                                    <asp:HiddenField ID="hdnidno" runat="server" Value='<%#Eval("IDNO") %>' />
                                                 <%-- <asp:HiddenField ID="hdnemailid" runat="server" Value='<%#Eval("EMAILID") %>' />--%>
                                                </td>
                                                <td>
                                                    <%#Eval("NAME")%>
                                                     <asp:Label id="lblemailid" runat="server"  Text='<%#Eval("EMAILID") %>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <%#Eval("subdesig")%>
                                                </td>
                                                  <td>
                                                    <%#Eval("subdept")%>
                                                </td>
                                        
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                  <%--   <div class="vista-grid_datapager text-center">
                                                <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvEmployeeMonth" PageSize="25" OnPreRender="dpPager_PreRender" Visible="false"  >
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
                                            </div> --%>
                                </asp:Panel>
                            </div>
                            <div class="col-12 btn-footer" runat="server"  visible="false">
                              
                            </div>
                            <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                            <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
             <div id="dvReport">
        <CR:CrystalReportViewer ID="crViewer" runat="server" 
            AutoDataBind="true"
           BestFitPage="False" ToolPanelView="None"  OnUnload="crViewer_Unload"/>
      
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

        function ValidateNumeric(txt) {


            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                // alert(txt.value)
                txt.value = "";
                txt.focus();
                alert("Only Numeric Characters allowed");
                return false;
            }
            else {
                return true;
            }
        }
    </script>

</asp:Content>

