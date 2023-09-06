<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="tallyConfigurationNew.aspx.cs" Inherits="Tally_tallyConfigurationNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">TALLY CONFIGURATION</h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Server Configuration</a>
                            </li>
                            <li class="nav-item">
                              <%--   <a class="nav-link"id="step2" data-toggle="tab" href="#tab_2" tabindex="2">Tally Company Configuration </a>--%>
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2" style="display: none;">Tally Company Configuration </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_3" tabindex="2">FeeHeads Configuration </a>
                            </li>
                        </ul>

                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <div>
                                    <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upDetails"
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
                                <asp:UpdatePanel ID="upDetails" runat="server">
                                    <ContentTemplate>

                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Tally Server IP </label>
                                                    </div>
                                                    <asp:TextBox ID="txtServerIp" onkeydown="return (event.keyCode!=13);" runat="server" CssClass="form-control" MaxLength="20" placeholder="Please Enter Tally server IP" TabIndex="1">
                                                    </asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbBankCode" runat="server"
                                                        TargetControlID="txtServerIp"
                                                        FilterType="Numbers,Custom"
                                                        FilterMode="ValidChars"
                                                        ValidChars=".">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="rfvBankCode" runat="server" ControlToValidate="txtServerIp" Display="None" ErrorMessage="Please Enter Tally Server IP" ValidationGroup="Submit"
                                                        SetFocusOnError="True" Text="*"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Port Number </label>
                                                    </div>
                                                    <asp:TextBox ID="txtPortNumber" onkeydown="return (event.keyCode!=13);" runat="server" CssClass="form-control" placeholder="Please Enter Port Number" MaxLength="8" TabIndex="2">
                                                    </asp:TextBox>

                                                    <ajaxToolKit:FilteredTextBoxExtender ID="fteBankName" runat="server"
                                                        TargetControlID="txtPortNumber"
                                                        FilterType="Custom,Numbers"
                                                        FilterMode="ValidChars"
                                                        ValidChars=" ">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="rfvPortNumber" runat="server" ControlToValidate="txtPortNumber" Display="None" ErrorMessage="Please Enter Port Number" ValidationGroup="Submit"
                                                        SetFocusOnError="True" Text="*"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Active </label>
                                                    </div>
                                                    <asp:CheckBox ID="chkIsActive" onkeydown="return (event.keyCode!=13);" runat="server" TabIndex="4" Checked="true" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSubmit" runat="server" TabIndex="5" Text="Submit" ToolTip="Click to Save" ValidationGroup="Submit" OnClick="btnSubmit_Click" UseSubmitBehavior="false" CssClass="btn btn-primary" />
                                            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" class="btn btn-warning" TabIndex="7" Text="Cancel" ToolTip="Click to Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />

                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                        </div>

                                        <div class="col-12" id="tblSearchResults" runat="server">
                                            <asp:Panel ID="pnlconfiguration" runat="server" Visible="false">
                                                <asp:ListView ID="lvRep_Company" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Tally Configuration</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>ACTION
                                                                    </th>
                                                                    <th>SR.NO.
                                                                    </th>


                                                                    <th>TALLY SERVER IP
                                                                    </th>

                                                                    <th>PORT NUMBER
                                                                    </th>

                                                                    <th>ACTIVE STATUS  
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
                                                                <asp:ImageButton ID="btnEdit" runat="server" TabIndex="8" Text="EDIT"
                                                                    CommandArgument='<%# Eval("TallyConfigId") %>' ToolTip="Edit Record"
                                                                    ImageUrl="~/Images/edit.png" OnClick="btnEdit_click" />
                                                            </td>

                                                            <td>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </td>


                                                            <td>
                                                                <%# Eval("ServerName")%>
                                                                        
                                                            </td>

                                                            <td>
                                                                <%# Eval("PortNumber")%>
                                                                    
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lblActiveStatus" runat="server"
                                                                    Text='<%# ((Eval("ActiveStatus")))%>'
                                                                    ForeColor='<%#(Convert.ToBoolean(Eval("IsActive"))==true?System.Drawing.Color.DarkGreen:System.Drawing.Color.Red)%>'></asp:Label>
                                                            </td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                    </Triggers>
                                </asp:UpdatePanel>

                                <div id="divMsg" runat="server" />
                            </div>

                            <div class="tab-pane fade" id="tab_2" style="display: none;">         
                                <div>
                                    <asp:UpdateProgress ID="updProga" runat="server" AssociatedUpdatePanelID="Updtallycompany"
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
                                <asp:UpdatePanel ID="Updtallycompany" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>
                                                            <asp:Label ID="lblReceiptBook" runat="server" Text="Select Reciept Type"></asp:Label>
                                                        </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlReceiptBook" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlReceiptBook_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlReceiptBook" Display="None" ErrorMessage="Please Select Receipt Book" ValidationGroup="submittallycompany"
                                                        SetFocusOnError="True" Text="*" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>
                                                            <asp:Label ID="lblServerName" runat="server" Text="Select Server"></asp:Label>
                                                        </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlServer" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:RequiredFieldValidator ID="rfvServer" runat="server" ControlToValidate="ddlServer" Display="None" ErrorMessage="Please Select Server IP" ValidationGroup="submittallycompany"
                                                        SetFocusOnError="True" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblCompanyName" runat="server" Text="Tally Company Name"></asp:Label>
                                                    </div>
                                                    <asp:TextBox ID="txtTallyCompany" onkeypress="return lettersOnly(event)" runat="server" placeholder="Please Enter Tally Company Name" CssClass="form-control" MaxLength="256" TabIndex="3">
                                                    </asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTallyCompany" Display="None" ErrorMessage="Please Get Company Name" ValidationGroup="submittallycompany"
                                                        SetFocusOnError="True" Text="*"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label></label>
                                                    </div>
                                                    <asp:Button ID="btnSearch" TabIndex="4" runat="server" CssClass="btn btn-primary" Text="Get Current Company" OnClick="btnSearch_Click" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>
                                                            <asp:Label ID="lblIsActive" CssClass="control-label" runat="server" Text="Active"></asp:Label>
                                                        </label>
                                                    </div>
                                                    <asp:CheckBox ID="CheckBox1" onkeydown="return (event.keyCode!=13);" runat="server" TabIndex="5" Checked="true" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnsubmittallycompany" runat="server" TabIndex="6" Text="Submit" ToolTip="Click to Save" ValidationGroup="submittallycompany" CssClass="btn btn-primary" OnClientClick="if (!Page_ClientValidate('submittallycompany')){ return false; } this.disabled = true; this.value = 'Processing...';" UseSubmitBehavior="false" OnClick="btnsubmittallycompany_Click" />

                                            <asp:Button ID="btnReporttallycompany" Visible="false" runat="server" CssClass="btn btn-info" TabIndex="6" Text="Report" ToolTip="Click to Report" OnClick="btnReporttallycompany_Click" />

                                            <asp:Button ID="btncanceltallycompany" runat="server" CausesValidation="False" CssClass="btn btn-warning" OnClientClick="Cancel()" TabIndex="7" Text="Cancel" ToolTip="Click to Cancel" OnClick="btncanceltallycompany_Click" />

                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submittallycompany" />
                                        </div>

                                        <div class="col-12" id="Table2" runat="server">
                                            <asp:Panel ID="pnlCompany" runat="server" Visible="false">
                                                <asp:ListView ID="lvRep_CompanyConfiguration" runat="server">
                                                    <LayoutTemplate>
                                                        <%--<div id="listViewGrid">--%>
                                                        <div class="sub-heading">
                                                            <h5>Company Configuration</h5>
                                                        </div>

                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResultscompany">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th style="width: 5%; text-align: left;">ACTION
                                                                    </th>
                                                                    <th>SR.NO.
                                                                    </th>

                                                                    <th>RECEIPT BOOK
                                                                    </th>

                                                                    <th>TALLY COMPANY  
                                                                    </th>

                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                        <%-- </div>--%>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btnEdittallycompany" runat="server" TabIndex="8" Text="EDIT"
                                                                    CommandArgument='<%# Eval("TallyCompanyConfigId") %>' ToolTip="Edit Record"
                                                                    ImageUrl="~/Images/edit.png" OnClick="btnEdittallycompany_Click" />
                                                            </td>

                                                            <td>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </td>

                                                            <td>
                                                                <%# Eval("CashBookName")%>
                                                                        
                                                            </td>

                                                            <td>
                                                                <strong><%# Eval("TallyCompanyName")%></strong>

                                                            </td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <%--   <asp:AsyncPostBackTrigger ControlID="ddlServer"/>--%>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane fade" id="tab_3">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdFeehead"
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

                                <asp:UpdatePanel ID="UpdFeehead" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>
                                                            Receipt Type
                                                        </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCashBook" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Please Select Cash Book" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddlCashBook_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvCourseCode" runat="server" ControlToValidate="ddlCashBook"
                                                        Display="None" ErrorMessage="Please Select Reciept Type" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="SubmitFeehead"></asp:RequiredFieldValidator>

                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmitFeehead" runat="server" Text="Submit" TabIndex="2" UseSubmitBehavior="false" ValidationGroup="SubmitFeehead" CssClass="btn btn-primary" OnClick="btnSubmitFeehead_Click" />
                                                    <asp:Button ID="btncancelFeehead" runat="server" TabIndex="3" Text="Cancel" CssClass="btn btn-warning" OnClick="btncancelFeehead_Click" />

                                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="SubmitFeehead" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12">
                                            <asp:Panel ID="pnlFeeHeads" runat="server" Visible="false">
                                                <asp:ListView ID="repFeeHeads" runat="server" OnItemDataBound="repFeeHeads_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <div id="listViewGrid">
                                                            <div class="sub-heading">
                                                                <h5>Fee Heads</h5>
                                                            </div>

                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResultsfeehead">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>SR.NO.
                                                                        </th>
                                                                        <th>HEAD NAME
                                                                        </th>

                                                                        <th>SHORT NAME
                                                                        </th>

                                                                        <th>CASH LEDGER 
                                                                        </th>

                                                                        <th>BANK LEDGER
                                                                        </th>
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
                                                                <%# Container.DataItemIndex + 1 %>
                                                                <asp:HiddenField ID="hdnFeeHeadId" runat="server" Value='<%# Eval("FEE_HEAD") %>' />
                                                            </td>
                                                            <td>
                                                                <strong><%# Eval("FEE_LONGNAME")%></strong>
                                                            </td>

                                                            <td>
                                                                <strong><%# Eval("FEE_SHORTNAME")%></strong>

                                                            </td>

                                                            <td>
                                                                <asp:DropDownList ID="ddlCashLedgerName" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                </asp:DropDownList>

                                                            </td>

                                                            <td>
                                                                <asp:DropDownList ID="ddlBankLedgerName" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                </asp:DropDownList>

                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-- <script>
      
        $(document).ready(function () {   
            $("#step2").click(function () {
                $('[id*=ddlReceiptBook]').val("0").trigger("change");
                $('[id*=ddlServer]').empty();
                $('#<%=txtTallyCompany.ClientID %>').val('');
            });
        });

   </script>--%>
    <script>

        function lettersOnly() {
            var charCode = event.keyCode;

            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8)

                return true;
            else
                return false;
        }

    </script>
</asp:Content>

