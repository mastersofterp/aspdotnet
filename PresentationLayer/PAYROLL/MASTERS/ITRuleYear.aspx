<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ITRuleYear.aspx.cs" Inherits="PAYROLL_MASTERS_ITRuleYear" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">IT RULE YEAR</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="Panel_Error" runat="server" CssClass="Panel_Error" EnableViewState="false"
                                Visible="false">
                                <%--<table class="table table-bordered table-hover">
                                    <tr>
                                        <td style="width: 3%; vertical-align: top">
                                            <img src="../../../images/error.gif" class="text-center" alt="Error" />
                                        </td>
                                        <td style="width: 97%">
                                            <font style="font-family: Verdana; font-family: 11px; font-weight: bold; color: #CD0A0A">
                                        </font>
                                            <asp:Label ID="Label_ErrorMessage" runat="server" Style="font-family: Verdana; font-size: 11px; color: #CD0A0A"></asp:Label>
                                        </td>
                                    </tr>
                                </table>--%>
                            </asp:Panel>
                            <asp:Panel ID="Panel_Confirm" runat="server" CssClass="Panel_Confirm" EnableViewState="false"
                                Visible="false">
                                <%--<table class="table table-bordered table-hover">
                                    <tr>
                                        <td style="width: 3%; vertical-align: top">
                                            <img src="../../../images/confirm.gif" class="text-center" alt="confirm" />
                                        </td>
                                        <td style="width: 97%">
                                            <asp:Label ID="Label_ConfirmMessage" runat="server" Style="font-family: Verdana; font-size: 11px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>--%>
                            </asp:Panel>
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Income Tax IT Rule Year</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">                                  
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>IT Rules Name </label>
                                        </div>
                                       <asp:TextBox ID="txtrulename"  runat="server" class="form-control" Style="text-align:left" IsRequired="True" 
                                       Text="" TabIndex="1"  ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvITRule" runat="server" ControlToValidate="txtrulename" 
                                        ErrorMessage="Please Enter IT Rule Name" SetFocusOnError="True"
                                       ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                      <div class="label-dynamic">
                                       <label>Scheme Type:</label>
                                      </div>
                                     <asp:RadioButtonList ID="rblschemetype" runat="server" RepeatDirection="Horizontal" TabIndex="21">
                                       <asp:ListItem Text="Old Scheme" Value="Old Scheme"></asp:ListItem>  
                                       <asp:ListItem Text="New Scheme" Value="New Scheme" ></asp:ListItem>
                                      </asp:RadioButtonList>
                                      </div>
                                       <div class="col-md-4">
                                          <div class="form-group col-md-10">
                                           <label><span style="color: #FF0000">*</span> Is Active:</label>
                                           <asp:CheckBox ID="chkisactive" runat="server" />
                                          </div>
                                        </div>
                                     </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnsubmit" runat="server" Text="Save"  OnClick="btnsubmit_Click" ValidationGroup="submit" TabIndex="6" ToolTip="Click To Save" CssClass="btn btn-primary" />
                                <asp:Button ID="btnPrint" runat="server" Text="Print" TabIndex="7" ToolTip="Click To Print"  OnClick="btnPrint_Click" CssClass="btn btn-info" Visible="false"/>
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" TabIndex="8" ToolTip="Click To Cancel" OnClick="btncancel_Click"  CssClass="btn btn-warning" />                                
                                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" runat="server" />
                            </div>
                             <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvITRule" runat="server">
                                        <LayoutTemplate>
                                             <div class="sub-heading">
	                                          <h5>IT RULES</h5>
                                             </div>
                                              <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                                  <thead class="bg-light-blue">
                                                   <tr>
                                                         <th class="text-center">Action
                                                          </th>
                                                         <th class="text-center">IT Rule Name
                                                         </th>
                                                        <th>
                                                          IsActive
                                                         </th>
                                                         <th>
                                                            Scheme
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
                                                <td class="text-center">
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("IT_RULE_ID") %>'
                                                  AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>
                                                <td class="text-center">
                                                   <asp:Label ID="lblITRuleName" runat="server" Text='<%# Eval("IT_RULE_NAME") %>' />
                                                   <asp:Label ID="lblITRuleId" runat="server" Text='<%# Eval("IT_RULE_ID") %>' Visible="false" />
                                                </td>
                                                 <td>
                                                  <asp:Label ID="lblisactive" runat="server" Text='<%# Eval("IsActive") %>' />
                                                </td>
                                                <td>
                                                  <asp:Label ID="lblscheme" runat="server" Text='<%# Eval("Scheme") %>' />
                                                </td>
                                                </tr>
                                          </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                                <div class="vista-grid_datapager d-none">
                                    <div class="text-center">
                                        <asp:DataPager ID="dpPager" runat="server" OnPreRender="dpPager_PreRender" PagedControlID="lvITRule"
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
               <div id="divMsg" runat="server"></div>   
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
