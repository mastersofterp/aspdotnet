<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PayDesignation.aspx.cs"
    Inherits="PAYROLL_MASTERS_PayDesignation"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../Css/transliteration.css" rel="stylesheet" />
    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">DESIGNATION MANAGEMENT</h3>
                        </div>

                        <div class="box-body">
                            <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                            <asp:Panel ID="pnlPfMaster" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Add/Edit Designation Management</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Designation Name</label>
                                            </div>
                                            <asp:TextBox ID="txtDesigName" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True" onkeypress="return lettersOnly(event);" 
                                                TabIndex="1" ToolTip="Please Enter Designation Name">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator" ControlToValidate="txtDesigName" runat="server" ErrorMessage="Enter Designation Name" ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Short Name</label>
                                            </div>
                                            <asp:TextBox ID="txtDesigShortName" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True" onkeypress="return lettersOnly(event);"
                                                TabIndex="2" ToolTip="Please Enter Designation Short Name">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtDesigShortName" runat="server" ErrorMessage="Enter Designation Short Name" ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Sequence Number</label>
                                            </div>
                                            <asp:TextBox ID="txtDesigSeqNumber" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True" onKeyUp="validateNumeric(this)"
                                                TabIndex="3" ToolTip="Please Enter Sequence Number">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtDesigSeqNumber" runat="server" ErrorMessage="Enter Sequence Number" ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>
                                        </div>
                                        <%-- <div class="form-group col-md-4">
                                                <label>Designation Kannada : </label>
                                                <asp:TextBox ID="txtDesigKannad" runat="server" Text="" CssClass="form-control" IsRequired="True"
                                                    IsValidate="True" TabIndex="4" ToolTip="Please Enter Designation in Kannada">
                                                </asp:TextBox>--%>
                                        <%--onpaste="return false"--%>
                                        <%-- </div>--%>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnsubmit" runat="server" TabIndex="4" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="submit"
                                    ToolTip="Submit" CssClass="btn btn-primary" />
                                 <asp:Button ID="btnPrint" runat="server" TabIndex="5" Text="Report" OnClick="btnPrint_Click" ValidationGroup="payroll"
                                    ToolTip="Submit" CssClass="btn btn-info" Visible="false" />
                                <asp:Button ID="btncancel" runat="server" TabIndex="6" Text="Cancel" OnClick="btncancel_Click" CausesValidation="False"
                                    ToolTip="Cancel" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvDesignation" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
	                                            <h5>Designation List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width:100%"> 
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>ID
                                                        </th>
                                                        <th>Designation Name
                                                        </th>
                                                        <th>Designation Short Name
                                                        </th>
                                                        <%-- <th>Designation Kannada
                                                        </th>--%>
                                                        <th>Sequence Number
                                                        </th>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-center">
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("SUBDESIGNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDesigno" runat="server" Text='<%# Eval("SUBDESIGNO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDesig" runat="server" Text='<%# Eval("SUBDESIG") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDesigShort" runat="server" Text='<%# Eval("SUBSDESIG") %>' />
                                                </td>
                                                <%--<td>
                                                    <asp:Label ID="lblDesigKannada" runat="server" Text='<%# Eval("SUBDESIG_KANNADA") %>' />
                                                </td>--%>
                                                <td>
                                                    <asp:Label ID="lblDesigSeqNo" runat="server" Text='<%# Eval("SEQNO") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="vista-grid_datapager d-none">
                                <div class="text-center">
                                    <asp:DataPager ID="DataPager1" runat="server" OnPreRender="dpPager_PreRender" PagedControlID="lvDesignation"
                                        PageSize="500">
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
            control.makeTransliteratable(['ctl00_ContentPlaceHolder1_txtDesigKannad']);
        }
        google.setOnLoadCallback(onLoad);
    </script>
    <script type="text/javascript" language="javascript">
                 function validateNumeric(txt) {
                     if (isNaN(txt.value)) {
                         txt.value = txt.value.substring(0, (txt.value.length) - 1);
                         txt.value = '';
                         txt.focus = true;
                         alert("Only Numeric Characters allowed !");
                         return false;
                     }
                     else
                         return true;
                 }
    </script>
        <script type="text/javascript">
            function lettersOnly() {
                var charCode = event.keyCode;

                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode == 32) || (charCode == 8))

                    return true;
                else
                    return false;
                alert("Only Alphabets allowed");
            }
            </script>
    


</asp:Content>


