<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DivisionMaster.aspx.cs" Inherits="PAYROLL_MASTERS_DivisionMastert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>--%>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="../../Css/transliteration.css" rel="stylesheet" />--%>
    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">DIVISION MASTER</h3>
                        </div>
                        <div class="box-body">
                            <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                            <asp:Panel ID="pnlPfMaster" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                           <%-- <div class="sub-heading">
                                                <h5>Add/Edit Department Management</h5>
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                   <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Sub Department</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubDepartment" runat="server" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Select Quarter Type" TabIndex="1" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvMainPayhead" runat="server" InitialValue="0" ControlToValidate="ddlMainPayhead"
                                            Display="None" ErrorMessage="Please Select Main PayHead" ValidationGroup="payroll"></asp:RequiredFieldValidator>--%>
                                    </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Division Name</label>
                                            </div>
                                            <asp:TextBox ID="txtDeptName" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True"
                                                TabIndex="1" ToolTip="Please Enter Department Name" onkeypress="return lettersOnly(event)">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator" ControlToValidate="txtDeptName" runat="server" ErrorMessage="Enter Department Name" ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Division Code </label>
                                            </div>
                                            <asp:TextBox ID="txtCode" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True"
                                                TabIndex="2" ToolTip="Please Enter Code">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCode" runat="server" ErrorMessage="Enter Code" ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>
                                        </div>

                                      
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnsubmit" runat="server" TabIndex="3" Text="Submit" ValidationGroup="submit" OnClick="btnsubmit_Click"
                                    ToolTip="Submit" CssClass="btn btn-primary" />
                                 <asp:Button ID="btnPrint" runat="server" TabIndex="4" Text="Report"  ValidationGroup="payroll" OnClick="btnPrint_Click"
                                    ToolTip="Report" CssClass="btn btn-info" Visible="false" />
                                <asp:Button ID="btncancel" runat="server" TabIndex="5" Text="Cancel"  CausesValidation="False" OnClick="btncancel_Click"
                                    ToolTip="Cancel" CssClass="btn btn-warning" />  
                                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" runat="server" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvDepartment" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Division Master List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Sub Department Name
                                                        </th>
                                                        <th>Division Name
                                                        </th>
                                                        <th>Division Code
                                                        </th>
                                                        <%--<th>Division Code
                                                        </th>--%>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-center">
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("DIVIDNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click"  />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbldeptname" runat="server" Text='<%# Eval("SUBDEPT") %>' />
                                                    <asp:Label ID="lbldeptno" runat="server" Text='<%# Eval("SUBDEPTNO") %>' Visible="false" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDivName" runat="server" Text='<%# Eval("DIVNAME") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDivCode" runat="server" Text='<%# Eval("DIVCODE") %>' />
                                                </td>
                                                <%--<td>
                                                    <asp:Label ID="lblDeptKannada" runat="server" Text='<%# Eval("SUBDEPT_KANNADA") %>' />
                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                                <div class="vista-grid_datapager d-none">
                                    <div class="text-center">
                                        <asp:DataPager ID="DataPager1" runat="server" OnPreRender="DataPager1_PreRender" PagedControlID="lvDepartment"
                                            PageSize="100">
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
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server"></div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" src="https://www.google.com/jsapi">
    </script>
    <script src="../../INCLUDES/transliteration.l.js"></script>
    <script type="text/javascript">
        // Load the Google Transliterate API
        google.load("elements", "1", {
            packages: "transliteration"
        });

        function onLoad() {
            var options = {
                sourceLanguage:
                google.elements.transliteration.LanguageCode.ENGLISH,
                destinationLanguage:
                [google.elements.transliteration.LanguageCode.KANNADA],
                shortcutKey: 'ctrl+e',
                transliterationEnabled: true
            };

            // Create an instance on TransliterationControl with the required
            // options.
            var control =
            new google.elements.transliteration.TransliterationControl(options);

            // Enable transliteration in the textbox with id
            // 'transliterateTextarea'.
            control.makeTransliteratable(['ctl00_ContentPlaceHolder1_txtDeptKannad']);
        }
        google.setOnLoadCallback(onLoad);
    </script>
    <script type="text/javascript">
        function lettersOnly() {
            debugger;
            var charCode = event.keyCode;

            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode == 32) || (charCode == 8))

                return true;
            else
                alert("Only Alphabets allowed");
            return false;

        }
     </script>
</asp:Content>

