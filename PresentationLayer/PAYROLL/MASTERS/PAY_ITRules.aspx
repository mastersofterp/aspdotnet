<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PAY_ITRules.aspx.cs" Inherits="PAY_ITRules" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">IT RULE</h3>
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
                                            <h5>Income Tax Rule</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                      <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>IT Rules Name :</label>
                                       </div>
                                     <asp:DropDownList ID="ddlITRule" AppendDataBoundItems="true" runat="server"  CssClass="form-control" ToolTip="Select IT Rules" data-select2-enable="true" OnSelectedIndexChanged="ddlITRule_SelectedIndexChanged" AutoPostBack="true">
                                     <asp:ListItem Value="0">Please Select</asp:ListItem>
                                     </asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="rfvITRule" runat="server" ControlToValidate="ddlITRule" InitialValue="0" ForeColor="Red"
                                      ErrorMessage="Please Select IT Rule Name" SetFocusOnError="True"
                                      ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                           <sup>* </sup>
                                           <label>Scheme Type:</</label>
                                         </div>
                                         <asp:RadioButtonList ID="rblschemetype" runat="server" RepeatDirection="Horizontal" TabIndex="21"
                                            OnSelectedIndexChanged="rblschemetype_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text="Old Scheme" Value="Old Scheme"></asp:ListItem> 
                                            <asp:ListItem Text="New Scheme" Value="New Scheme" ></asp:ListItem>
                                           </asp:RadioButtonList>
                                                          </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Minimum Limit</label>
                                        </div>
                                        <asp:TextBox ID="txtMinLimit" runat="server" Text="" IsRequired="True" DataType="NumberType"
                                            IsValidate="True" TabIndex="1" ToolTip="Enter Minimum Limit" CssClass="form-control" onKeyUp="validateNumeric(this);" />
                                        <asp:RequiredFieldValidator ID="rfvMinLimit" runat="server" ControlToValidate="txtMinLimit"
                                            Display="None" ErrorMessage="Please Enter Minimum Limit" SetFocusOnError="True"
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <%--<ajaxToolKit:ValidatorCalloutExtender ID="rfvMinLimitCallout" runat="server"
                                            Enabled="True" TargetControlID="rfvMinLimit">
                                        </ajaxToolKit:ValidatorCalloutExtender>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Maximum Limit</label>
                                        </div>
                                        <asp:TextBox ID="txtMaxLimit" runat="server" Text="" DataType="NumberType"
                                            IsRequired="True" IsValidate="True" TabIndex="2" ToolTip="Enter Maximum Limit" CssClass="form-control"  onKeyUp="validateNumeric(this);"/>
                                        <asp:RequiredFieldValidator ID="rfvMaxLimit" runat="server" ControlToValidate="txtMaxLimit"
                                            Display="None" ErrorMessage="Please Enter Maximum Limit" SetFocusOnError="True"
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                       <%-- <ajaxToolKit:ValidatorCalloutExtender ID="rfvMaxLimitCallout" runat="server"
                                            Enabled="True" TargetControlID="rfvMaxLimit" />--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>* </sup>
                                            <%--Text-align : left change (Amol sawarkar)--%>
                                            <label>Fix Amount</label>
                                        </div>
                                        <asp:TextBox ID="txtFixAmount" runat="server" Text="" DataType="NumberType" IsRequired="True" IsValidate="True" Style="text-align:  left" ToolTip="Enter Fixed Amount" onKeyUp="validateNumeric(this);"
                                            TabIndex="3" ShowMessage="" CssClass="form-control" />
                                        <asp:RequiredFieldValidator  ID="rfvfixamount" runat="server" ControlToValidate="txtFixAmount" ErrorMessage="Enter Fixed Amount" SetFocusOnError="true" Display="None" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>* </sup>
                                             <%--Text-align : left change (Amol sawarkar)--%>
                                            <label>Percentage</label>
                                        </div>
                                        <asp:TextBox ID="txtPercentage" runat="server" Text="" DataType="NumberType" CssClass="form-control"
                                            IsRequired="True" IsValidate="True" TabIndex="4" Style="text-align: left" ToolTip="Enter Percentage"  onKeyUp="validateNumeric(this);" onchange="return Increment2();"/>
                                        <asp:RequiredFieldValidator  ID="rfvper" runat="server" ControlToValidate="txtPercentage" ErrorMessage="Enter Percentage " SetFocusOnError="true" Display="None" ValidationGroup="submit"></asp:RequiredFieldValidator>
                               
                                        
                                          </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            
                                            <label>IT Rules For</label>

                                        </div>
                                        <asp:DropDownList ID="ddlITFor" AppendDataBoundItems="true" runat="server" AutoPostBack="True" CssClass="form-control" ToolTip="Select IT Rules for" TabIndex="5" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Sr. Citizen</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" ValidationGroup="submit" TabIndex="6" ToolTip="Click To Save" CssClass="btn btn-primary" />
                                <asp:Button ID="btnPrint" runat="server" Text="Print" TabIndex="7" ToolTip="Click To Print" OnClick="btnPrint_Click" CssClass="btn btn-info" />
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" TabIndex="8" ToolTip="Click To Cancel" OnClick="btncancel_Click" CssClass="btn btn-warning" />                                
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
                                                            <th>Action
                                                            </th>
                                                            <th>Minimum Amount
                                                            </th>
                                                            <th>Maximum Amount
                                                            </th>
                                                            <th>Fixed Amount
                                                            </th>
                                                            <th>Percentage
                                                            </th>
                                                            <th>IT Rules For
                                                            </th>
                                                            <th>
                                                               IT Rule Name
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("ITNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMinAmount" runat="server" Text='<%# Eval("F_RANGE") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMaxAmount" runat="server" Text='<%# Eval("T_RANGE") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFixAmount" runat="server" Text='<%# Eval("FIXAMT") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPercentage" runat="server" Text='<%# Eval("PER") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("STATUS") %>' />
                                                </td>
                                               <td>
                                                     <asp:Label ID="lblITRuleName" runat="server" Text='<%# Eval("IT_RULE_NAME") %>' />
                                                     <asp:Label ID="lblITRuleId" runat="server" Text='<%# Eval("IT_RULE_ID") %>' Visible="false" />
                                                </td>
                                                <td>
                                                  <asp:Label ID="lblschemetype" runat="server" Text='<%#Eval("Scheme") %>'/>
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
              
             <script type="text/javascript" >
                 function Increment2() {
                     debugger
                     //validateNumeric(txt);


                     var SANCTIONAMOUNT = document.getElementById('<%=txtPercentage.ClientID%>').value;

                     if (SANCTIONAMOUNT > 100) {
                         alert("Percentage should be less than 100%");
                         document.getElementById('<%=txtPercentage.ClientID%>').value = '';
                         return false;
                     }
                     return true;
                 }
          </script> 
               <div id="divMsg" runat="server"></div>   
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
