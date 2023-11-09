<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_DA_HEAD.aspx.cs" Inherits="PAYROLL_MASTERS_Pay_DA_HEAD" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:UpdatePanel ID="updpanel" runat="server">
       <ContentTemplate>
            <div class="row">
                 <div class="col-md-12 col-sm-12 col-12">
                      <div class="box box-primary">
                           <div id="div2" runat="server"></div>
                           <div class="box-header with-border">
                            <h3 class="box-title">DA HEAD MASTER</h3>
                           </div>
                          <div class="box-body">
                               <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                               <asp:Panel ID="pnlPfMaster" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Add/Edit DA HEAD </h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                    <div class="col-12">
                                         <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>DA HEAD Description</label>
                                            </div>
                                            <asp:TextBox ID="txtdaheaddescription" runat="server" Text="" CssClass="form-control" IsRequired="True" IsValidate="True"
                                                TabIndex="1" ToolTip="Please Enter Main Department Name" >
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator" ControlToValidate="txtdaheaddescription" runat="server" ErrorMessage="Enter DA Head Description" ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    </div>

                              </asp:Panel>
                                <div class="col-12 btn-footer">
                                <asp:Button ID="btnsubmit" runat="server" TabIndex="3" Text="Submit" OnClick="btnsubmit_Click" ValidationGroup="submit"
                                    ToolTip="Submit" CssClass="btn btn-primary" />
                                <asp:Button ID="btncancel" runat="server" TabIndex="5" Text="Cancel" OnClick="btncancel_Click" CausesValidation="False"
                                    ToolTip="Cancel" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" runat="server" />
                            </div>
                               <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvheaddescription" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>DA HEAD LIST</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>DA HEAD Description
                                                        </th>
                                                        <%--<th>Department Kannada
                                                        </th>--%>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("DA_HEADID") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDAHeadDescription" runat="server" Text='<%# Eval("DA_HEAD_DESCRIPTION") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                               <div class="vista-grid_datapager d-none">
                        <div class="text-center">
                            <asp:DataPager ID="DataPager1" runat="server" OnPreRender="DataPager1_PreRender" PagedControlID="lvheaddescription"
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
    <%--updated on 01112023--%>
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

