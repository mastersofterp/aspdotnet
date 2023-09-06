<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ReceiptGenerationMaster.aspx.cs" Inherits="ACADEMIC_MASTERS_ReceiptGenerationMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updReceiptGen"
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
    <asp:UpdatePanel ID="updReceiptGen" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-4 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblReceiptCode" runat="server" Font-Bold="true"> Receipt Code</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlReceiptCode" runat="server" TabIndex="1" CssClass="form-control" ToolTip="Please Select Receipt Code." OnSelectedIndexChanged="ddlReceiptCode_SelectedIndexChanged" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvReceiptCode" runat="server" ControlToValidate="ddlReceiptCode"
                                            Display="None" ValidationGroup="Submit" InitialValue="0" ErrorMessage="Please Select Receipt Code."></asp:RequiredFieldValidator>
                                    </div>
                                   
                                 
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Counter</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCounter" runat="server" AppendDataBoundItems="true" TabIndex="2" CssClass="form-control" data-select2-enable="true" 
                                            ToolTip="Please enter Counter">
                                              <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCounter" runat="server" ControlToValidate="ddlCounter"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please enter Counter"
                                            InitialValue="0" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Receipt Code Class</label>
                                        </div>
                                        <asp:TextBox ID="txtReceiptClass" runat="server"  TabIndex="3" CssClass="form-control" ToolTip="Please Enter Receipt Code Class"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                      FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,/:;<>?'{}[]\|.:-&&quot;0123456789'" TargetControlID="txtReceiptClass" />
                                          <asp:RequiredFieldValidator ID="valCounterName" runat="server" ControlToValidate="txtReceiptClass"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter Receipt Code Class" />
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblPaymentMode" runat="server" Font-Bold="true">Payment Mode</asp:Label>
                                        </div>
                                        <asp:RadioButtonList ID="rblPaymentMode" runat="server" TabIndex="4" ToolTip="Please Select Payment Mode" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">Online</asp:ListItem>
                                            <asp:ListItem Value="2">Offline</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvPaymentMode" runat="server" Display="None" ErrorMessage="Please Select Payment Mode"
                                            ControlToValidate="rblPaymentMode" ForeColor="Red" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                       <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic mt-4">
                                      
                                            <asp:CheckBox ID="chkIsMiss" runat="server" TabIndex="6" ToolTip="Is Miscellaneous" OnCheckedChanged="chkIsMiss_CheckedChanged" AutoPostBack="true"/>
                                            <asp:Label ID="lblIsMiss" runat="server" Text="Is Miscellaneous"></asp:Label>
                                        </div>
                                       
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12" id="degree" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>--%>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="5" CssClass="form-control" ToolTip="Please Select Degree" data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ValidationGroup="Submit" InitialValue="0" ErrorMessage="Please Select Degree"></asp:RequiredFieldValidator>
                                    </div>
                                 
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer">
                              <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="7" ValidationGroup="Submit" OnClick="btnSubmit_Click"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"  CssClass="btn btn-warning" TabIndex="8" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit"/>
                            </div>
                                                    <div class="col-12">
                                <div class="sub-heading">
                                    <h5>List of Receipt Number Generation</h5>
                                </div>
                                <asp:ListView ID="lvReceiptGeneration" runat="server">
                                    <LayoutTemplate>
                                        <table id="tblHead" class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                            <thead  class="bg-light-blue">
                                                <tr id="trRow">
                                                   <%-- <th>Edit
                                                    </th>--%>
                                                    <th>Degree Name
                                                    </th>
                                                    <th>Receipt Name
                                                    </th>
                                                    <th>Payment Mode
                                                    </th>
                                                    <th>Counter Name
                                                    </th>
                                                    <th> Class
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <%--<td>
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                    CommandArgument='<%# Eval("COUNTERNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                    OnClick="btnEdit_Click" TabIndex="10" />
                                            </td>--%>
                                            <td>
                                                <%# Eval("DEGREENAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("RECIEPT_TITLE")%>
                                            </td>
                                             <td>
                                                <%# Eval("PAY_MODE_CODE")%>
                                            </td>
                                             <td>
                                                <%# Eval("COUNTERNAME")%>
                                            </td>
                                             <td>
                                                <%# Eval("CLASS")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                    </div>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
<%--   <script>
       function myFunction() {
           var checkBox = document.getElementById("chkIsMiss");
           var text = document.getElementById("degree");
           if (checkBox.checked == true) {
             
               text.style.display = "none";
           } else {
               text.style.display = "block";
           }
       }
</script>--%>
</asp:Content>

