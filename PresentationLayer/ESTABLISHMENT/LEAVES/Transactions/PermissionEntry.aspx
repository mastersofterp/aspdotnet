<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PermissionEntry.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_PermissionEntry" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Permission Entry / Short Leave</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnllist" runat="server" Visible="true">
                        <div class="col-12 text-center">
                            <asp:LinkButton ID="lnkNew" runat="server" Text="New  Application" CssClass="btn btn-primary" ToolTip="New Permission Application" OnClick="lnkNew_Click"> </asp:LinkButton>
                            <asp:LinkButton ID="lnkbut" runat="server" Text="Application Status" CssClass="btn btn-primary" ToolTip="Permission Application status" OnClick="lnkbut_Click"></asp:LinkButton>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlStatus" runat="server" Visible="false">
                        <div class="col-12 mt-3">
                            <asp:ListView ID="lvCPDA" runat="server">
                                <EmptyDataTemplate>
                                    <p class="text-center text-bold">
                                        NO RECORD FOUND                                                               
                                    </p>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div class="vista-grid">
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action</th>
                                                    <th>Sr.No.</th>
                                                    <th>Application Date</th>
                                                    <th>Status</th>
                                                    <th>Report</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>

                                        <td>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("PERTNO")%>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                        </td>
                                        <td>
                                            <%#Container.DataItemIndex+1 %>
                                        </td>
                                        <td>
                                            <%# Eval("DATE","{0:dd/MM/yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("STATUS")%>                                                                  
                                        </td>
                                        <td>
                                            <asp:Button ID="btnReport" runat="server" CssClass="btn btn-info" Text="Report" CommandArgument='<%# Eval("PERTNO")%>' OnClick="btnReport_Click" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                            <div class="text-center d-none">
                                <div class="vista-grid_datapager">
                                    <asp:DataPager ID="dpinfo" runat="server" PagedControlID="lvCPDA" PageSize="50" Visible="false"
                                        OnPreRender="dpinfo_PreRender">
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

                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnBackPanel" runat="server" Text="Back" CssClass="btn btn-primary" ToolTip="Click here to Go Back"
                                OnClick="btnBackPanel_Click" />
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlSave" runat="server" Visible="false">
                        <asp:UpdatePanel ID="updAdd" runat="server">
                            <ContentTemplate>
                                <div class="col-12 ">
                                    <div class="row">
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblUserName" runat="server" Text="  " TabIndex="1" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Department :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lbldept" runat="server" Text="  " TabIndex="2" Font-Bold="true"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Designation :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lbldesgin" runat="server" Text=" " TabIndex="3" Font-Bold="true"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label> Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="img">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" Style="z-index: 0;" runat="server" TabIndex="19" ToolTip="Enter From Date"
                                                    CssClass="form-control"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="ceDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                                    PopupButtonID="img" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvFromdt" runat="server"
                                                    ControlToValidate="txtFromDate" Display="None"
                                                    ErrorMessage="Please Enter Date" SetFocusOnError="true"
                                                    ValidationGroup="CPDAapp"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:MaskedEditExtender ID="meeFromdt" runat="server"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                    Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    TargetControlID="txtFromDate" />
                                                <%--  <ajaxToolKit:MaskedEditValidator ID="mevFromdt" runat="server"
                                                                ControlExtender="meeFromdt" ControlToValidate="txtFromDate" Display="None"
                                                                EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Date"
                                                                InvalidValueBlurredMessage="Invalid Date"
                                                                InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                SetFocusOnError="true" TooltipMessage="Please Enter Date"
                                                                ValidationGroup="CPDAapp" IsValidEmpty="false" InitialValue="__/__/____">
                                                &#160;&#160;
                                                            </ajaxToolKit:MaskedEditValidator>--%>
                                                <ajaxToolKit:MaskedEditValidator ID="mevFromdt" runat="server" ControlExtender="meeFromdt"
                                                    ControlToValidate="txtFromDate" InvalidValueMessage="Date  is Invalid (Enter dd/MM/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter Date " EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                    ValidationGroup="CPDAapp" SetFocusOnError="True" InitialValue="__/__/____" IsValidEmpty="false" />

                                            </div>
                                        </div>
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label> Reason</label>
                                            </div>
                                            <asp:textbox id="txtreson" runat="server" textmode="MultiLine" cssclass="form-control" onkeypress="if (this.value.length > 120) { return false; }" xmlns:asp="#unknown"></asp:textbox>
                                        </div>
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label> Path</label>
                                            </div>
                                            <%--<asp:Label ID="txtPath" runat="server" Text=" " CssClass="form-control"></asp:Label>--%>
                                            <asp:TextBox ID="txtPath" runat="server" Text=" " CssClass="form-control" Enabled="false"
                                                ToolTip="Path" TabIndex="25" TextMode="MultiLine" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label> Permission / Short Leave Type</label>
                                            </div>
                                            <asp:RadioButtonList ID="rblleavetype" runat="server" AutoPostBack="true"
                                                RepeatDirection="Horizontal" TabIndex="5" ToolTip="Select First Half or Second Half">
                                                <%--OnSelectedIndexChanged="rblleavetype_SelectedIndexChanged"--%>
                                                <asp:ListItem Enabled="true" Selected="True" Text="FN/First Half" Value="0" style="margin-right: 15px"> </asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="AN/Second Half" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="rfvday" runat="server"
                                                ControlToValidate="rblleavetype" Display="None"
                                                ErrorMessage="Please Enter Date" SetFocusOnError="true"
                                                ValidationGroup="CPDAapp"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>



                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div class="col-12 btn-footer mt-3">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="CPDAapp" TabIndex="39" ToolTip="Click To Submit"
                                CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnBack" runat="server" Text="Back" TabIndex="41" ToolTip="Click To Back"
                                CssClass="btn btn-primary" OnClick="btnBack_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="40" ToolTip="Click To Reset"
                                CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="CPDAapp"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </div>

                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
