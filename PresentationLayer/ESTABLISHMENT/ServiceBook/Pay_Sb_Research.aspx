<%@ Page Title="" Language="C#" MasterPageFile="~/ServiceBookMaster.master" AutoEventWireup="true" CodeFile="Pay_Sb_Research.aspx.cs" Inherits="ESTABLISHMENT_SERVICEBOOK_Pay_Sb_Research" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">
    <link href="../../Css/master.css" rel="stylesheet" type="text/css" />
    <link href="../../Css/Theme1.css" rel="stylesheet" type="text/css" />
    <br />


    <div class="row">
        <div class="col-md-12">
            <div>
                <form role="form">
                    <div class="box-body">
                        <div class="col-md-12">
                            <asp:UpdatePanel ID="updDemo" runat="server">
                                <ContentTemplate>
                                    <div class="col-12">
                                    <div class="row">
                                    <div class="form-group col-md-12">
                                        <asp:Panel ID="pnlAdd" runat="server">
                                            <div class="panel panel-info">
                                                <%--<div class="panel panel-heading">Research And Development Project</div>--%>
                                                <div class="panel panel-body">
                                                     <div class="col-12">
                                                         <div class="row">

                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                        <label>Department :</label>
                                                        <asp:TextBox ID="txtDepartment" TabIndex="1" runat="server" CssClass="form-control" ToolTip="Enter Department" Enabled="false"></asp:TextBox>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <label><span style="color: #FF0000">*</span>Project Tilte :</label>
                                                        <asp:TextBox ID="txtTitle" TabIndex="2" runat="server" CssClass="form-control" MaxLength="250" ToolTip="Enter Project Tilte"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                                                            Display="None" ErrorMessage="Please Enter Project Tilte" ValidationGroup="ServiceBook" SetFocusOnError="true">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <label><span style="color: #FF0000">*</span>Name of Principal :</label>
                                                        <asp:TextBox ID="txtPrincipal" TabIndex="3" runat="server" CssClass="form-control" MaxLength="250" ToolTip="Enter Name of Principal"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvPrincipal" runat="server" ControlToValidate="txtPrincipal"
                                                            Display="None" ErrorMessage="Please Enter Name Of Principal" ValidationGroup="ServiceBook" SetFocusOnError="true">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <label>Nature Of Project:</label>
                                                        <asp:DropDownList ID="ddlProject" runat="server" AppendDataBoundItems="true"
                                                            CssClass="form-control" ToolTip="Select Nature Of Project" TabIndex="3">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Sponsored</asp:ListItem>
                                                            <asp:ListItem Value="2">Innovation</asp:ListItem>
                                                            <asp:ListItem Value="3">Incubation</asp:ListItem>
                                                            <asp:ListItem Value="4">Research</asp:ListItem>
                                                            <asp:ListItem Value="5">Commercial</asp:ListItem>
                                                            <asp:ListItem Value="6">Other</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <label>Sponsored By:</label>
                                                        <asp:DropDownList ID="ddlSponsered" runat="server" AppendDataBoundItems="true"
                                                            CssClass="form-control" ToolTip="Select Sponsered By" TabIndex="4">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Acadamic Institution</asp:ListItem>
                                                            <asp:ListItem Value="2">R&D  Institution </asp:ListItem>
                                                            <asp:ListItem Value="3">State Govt Body</asp:ListItem>
                                                            <asp:ListItem Value="4">Central Govt Body</asp:ListItem>
                                                            <asp:ListItem Value="5">Industry</asp:ListItem>
                                                            <asp:ListItem Value="6">Other</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <label>Funding Ajency Name:</label>
                                                        <asp:TextBox ID="txtAjency" TabIndex="5" runat="server" CssClass="form-control" ToolTip="Enter Funding Ajency Name"></asp:TextBox>
                                                    </div>

                                                    <%--<div class="form-group col-md-6">
                                                     <label>Client Name:</label>
                                                      <asp:TextBox ID="txtClient" TabIndex="6" runat="server" CssClass="form-control" ToolTip="Enter Client Name"></asp:TextBox>
                                                     </div>--%>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <label><span style="color: #FF0000">*</span>Amount:</label>
                                                        <asp:TextBox ID="txtAmount" TabIndex="6" runat="server" CssClass="form-control" ToolTip="Enter Amount" MaxLength="8"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeAmount" runat="server" FilterType="Custom, Numbers"
                                                            TargetControlID="txtAmount" ValidChars=".">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAmount"
                                                            Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="ServiceBook" SetFocusOnError="true">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <label><span style="color: #FF0000">*</span>Total Project Fund:</label>
                                                        <asp:TextBox ID="txtTotalProjectfund" TabIndex="7" runat="server" CssClass="form-control" ToolTip="Enter Total Project Fund" MaxLength="8"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftvTotalProjectfund" runat="server" FilterType="Custom, Numbers"
                                                            TargetControlID="txtTotalProjectfund" ValidChars=".">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="rfvTotalProjectfund" runat="server" ControlToValidate="txtTotalProjectfund"
                                                            Display="None" ErrorMessage="Please Enter Total Project Fund" ValidationGroup="ServiceBook" SetFocusOnError="true">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <label><span style="color: #FF0000">*</span>Period From Date :</label>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">  <%-- ImageUrl="~/images/calendar.png" Style="cursor: pointer"--%>
                                                                <asp:Image ID="Image1" runat="server" class="fa fa-calendar text-blue" />
                                                            </div>
                                                            <asp:TextBox ID="txtFromdate" TabIndex="8" Style="z-index: 0;" runat="server" CssClass="form-control" ToolTip="Enter Period From Date"></asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="ceDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromdate"
                                                                PopupButtonID="Image1" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                            </ajaxToolKit:CalendarExtender>

                                                            <ajaxToolKit:MaskedEditExtender ID="meDate" runat="server" TargetControlID="txtFromdate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                            <ajaxToolKit:MaskedEditValidator ID="mevDate" runat="server" ControlExtender="meDate"
                                                                ControlToValidate="txtFromdate" EmptyValueMessage="Please Enter Period From Date" InvalidValueMessage="Period From Date is Invalid (Enter dd/mm/yyyy Format)"
                                                                Display="None" TooltipMessage="Please Enter Period From Date" EmptyValueBlurredText="Empty"
                                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />

                                                            <asp:RequiredFieldValidator ID="rfvFromdate" runat="server" ControlToValidate="txtFromdate"
                                                                Display="None" ErrorMessage="Please Enter Period From Date" ValidationGroup="ServiceBook" SetFocusOnError="true">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>


                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <label><span style="color: #FF0000">*</span>Period To Date :</label>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">     <%--ImageUrl="~/images/calendar.png" Style="cursor: pointer" --%>
                                                                <asp:Image ID="Image2" runat="server" class="fa fa-calendar text-blue" />
                                                            </div>
                                                            <asp:TextBox ID="txtPeriodToDate" TabIndex="9" Style="z-index: 0;" runat="server" CssClass="form-control" ToolTip="Enter Period To Date"></asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPeriodToDate"
                                                                PopupButtonID="Image2" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                            </ajaxToolKit:CalendarExtender>

                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtPeriodToDate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meDate"
                                                                ControlToValidate="txtPeriodToDate" EmptyValueMessage="Please Enter Period To Date" InvalidValueMessage="Period To Date is Invalid (Enter dd/mm/yyyy Format)"
                                                                Display="None" TooltipMessage="Please Enter Period To Date" EmptyValueBlurredText="Empty"
                                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />

                                                            <asp:RequiredFieldValidator ID="rfvPeriodToDate" runat="server" ControlToValidate="txtPeriodToDate"
                                                                Display="None" ErrorMessage="Please Enter Period To Date" ValidationGroup="ServiceBook" SetFocusOnError="true">

                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <label><span style="color: #FF0000">*</span>Year:</label>
                                                        <asp:TextBox ID="txtYear" TabIndex="10" runat="server" CssClass="form-control" MaxLength="4" ToolTip="Enter Year"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers"
                                                            TargetControlID="txtYear">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                         <asp:RequiredFieldValidator ID="RFVYear" runat="server" ControlToValidate="txtYear"
                                                            Display="None" ErrorMessage="Please Enter Year" ValidationGroup="ServiceBook"
                                                            SetFocusOnError="True">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <label>Project Status:</label>
                                                        <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="true"
                                                            CssClass="form-control" ToolTip="Select Project Status" TabIndex="11">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Inprocess</asp:ListItem>
                                                            <asp:ListItem Value="2">Complete</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <label><span style="color: #FF0000">*</span>Total Fund Utilized:</label>
                                                        <asp:TextBox ID="txtUtilized" TabIndex="12" runat="server" CssClass="form-control" MaxLength="8" ToolTip="Enter Total Fund Utilized"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteUtilized" runat="server" FilterType="Custom, Numbers"
                                                            TargetControlID="txtUtilized" ValidChars=".">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="rfvUtilized" runat="server" ControlToValidate="txtUtilized"
                                                            Display="None" ErrorMessage="Please Enter Total Fund Utilized" ValidationGroup="ServiceBook" SetFocusOnError="true">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <label>Ownership:</label>
                                                        <asp:DropDownList ID="ddlOwnerType" runat="server" AppendDataBoundItems="true"
                                                            CssClass="form-control" ToolTip="Select Ownership" TabIndex="13">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Joint</asp:ListItem>
                                                            <asp:ListItem Value="2">Independent</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <label>Weather Join With:</label>
                                                        <asp:DropDownList ID="ddlJoinWith" runat="server" AppendDataBoundItems="true"
                                                            CssClass="form-control" ToolTip="Select Weather Join With" TabIndex="14">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Industry</asp:ListItem>
                                                            <asp:ListItem Value="2">R&D Institute</asp:ListItem>
                                                            <asp:ListItem Value="3">Academic Institute</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <label>Result Output:</label>
                                                        <asp:TextBox ID="txtoutput" TabIndex="15" runat="server" CssClass="form-control" ToolTip="Enter Result Output"></asp:TextBox>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <label>Impact Factor:</label>
                                                        <asp:TextBox ID="txtImpact" TabIndex="16" runat="server" CssClass="form-control" ToolTip="Enter Impact Factor"></asp:TextBox>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <label>Joint Belong To:</label>
                                                        <asp:DropDownList ID="ddlBelong" runat="server" AppendDataBoundItems="true"
                                                            CssClass="form-control" ToolTip="Select Joint Belong To" TabIndex="17">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">National</asp:ListItem>
                                                            <asp:ListItem Value="2">Internation</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <br />

                                                    <div class="form-group col-md-12">
                                                        <p class="text-center">
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="18"
                                                                CssClass="btn btn-primary" ToolTip="Click here to Save" OnClick="btnSubmit_Click" />&nbsp;
                                                       <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="19"
                                                           CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        </p>
                                                    </div>
                                                    <%-- </div>--%>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                         <div class="form-group col-md-12 table-responsive">
                                <asp:Panel ID="pnlInfo" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvResearch" runat="server">
                                        <EmptyDataTemplate>
                                            <br />
                                            <p class="text-center text-bold">
                                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows For Research And Development "></asp:Label>
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <h4 class="box-title">Research And Development Project
                                                </h4>
                                                <table class="table table-bordered table-hover">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Action
                                                            </th>
                                                            <th>Project Title
                                                            </th>
                                                            <th>Name of Principal
                                                            </th>
                                                            <th>Year
                                                            </th>
                                                            <th>Amount
                                                            </th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                            <%-- <div class="listview-container-servicebook">
                                            <div id="Div1" class="vista-gridServiceBook">
                                                <table class="datatable-ServiceBook table-responsive">
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>--%>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>

                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("RESEARNO")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                     <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("RESEARNO") %>'
                                                         AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                         OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <%# Eval("PROJECT_TITLE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("NAME_OF_PRINCIPAL")%>
                                                </td>
                                                <td>
                                                    <%# Eval("YEAR")%>
                                                </td>
                                                <td>
                                                    <%# Eval("AMOUNT")%>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                               </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSubmit" />
                                </Triggers>
                            </asp:UpdatePanel>

                           
                    <%--</ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSubmit" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                </form>
                <div class="box-footer">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                                <div class="text-center">
                                    <div class="modal-content">
                                        <div class="modal-body">
                                            <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                                            <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                                            <div class="text-center">
                                                <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                                                <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />


    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }
    </script>

</asp:Content>

