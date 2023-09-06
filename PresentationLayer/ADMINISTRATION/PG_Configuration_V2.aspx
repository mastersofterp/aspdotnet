<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PG_Configuration_V2.aspx.cs" Inherits="ACADEMIC_PG_Configuration_V2_aspx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        /*#ctl00_ContentPlaceHolder1_pnlPGCongig .dataTables_scrollHeadInner {
            width: max-content !important;
        }*/
        /*#ctl00_ContentPlaceHolder1_lvPayConfig .dataTables_scrollHeadInner {
            width: max-content !important;
        }
          #ctl00_ContentPlaceHolder1_lvPaymentGatewayMapping .dataTables_scrollHeadInner {
            width: max-content !important;
        }*/
    </style>

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdActive1" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdActive2" runat="server" ClientIDMode="Static" />

    <%--  ClientIDMode="Static" --%>
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="updPGconfiguration"
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

    <asp:UpdatePanel ID="updPGconfiguration" runat="server" UpdateMode="conditional">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>

                        <div class="box-header with-border">
                            <h3 class="box-title"><b>PG CONFIGURATION</b></h3>
                        </div>

                        <div class="box-body">
                            <div class="nav-tabs-custom">

                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Payment Gateway Master</a> </li>
                                    <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="1">Payment Gateway Configuration </a></li>
                                    <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#tab_3" tabindex="1">Payment Gateway Mapping</a> </li>
                                </ul>

                                <br />

                                <div class="tab-content" id="my-tab-content">

                                    <div class="tab-pane active" id="tab_1">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updPay_master"
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

                                        <asp:UpdatePanel ID="updPay_master" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <%-- <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />--%>

                                                <asp:Panel ID="pnlPay_master" runat="server" Visible="true">

                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Gateway Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtPaymentGName" runat="server" TabIndex="1" MaxLength="50" CssClass="form-control" AutoComplete="off"
                                                                    ToolTip="Please Enter Payment Gateway Name" />

                                                                <asp:RequiredFieldValidator ID="rfvpayname" runat="server" ControlToValidate="txtPaymentGName"
                                                                    Display="None" ErrorMessage="Please Enter Payment Gateway Name1." ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftepaymentname" runat="server" TargetControlID="txtPaymentGName" FilterType="Custom" FilterMode="InvalidChars"
                                                                    InvalidChars="~`!@#$%^*()_+=,-./:;<>?'{}[]\|&&quot;'" />

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="row">
                                                                    <div class="form-group col-6">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Status</label>
                                                                        </div>
                                                                        <div class="switch form-inline">
                                                                            <input type="checkbox" id="rdActive" name="switch" checked />
                                                                            <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-4 col-md-12 col-12">
                                                                <div class=" note-div">
                                                                    <h5 class="heading">Note (Please Select)</h5>
                                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Checked : Status- - <span style="color: green; font-weight: bold">Active</span></span>  </p>
                                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>UnChecked : Status - <span style="color: red; font-weight: bold">InActive</span></span></p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnSaveV2" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                                            OnClick="btnSaveV2_Click" TabIndex="4" CssClass="btn btn-info" OnClientClick="return validate();" />

                                                        <asp:Button ID="btnCancelV2" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                            OnClick="btnCancelV2_Click" TabIndex="5" CssClass="btn btn-warning" />

                                                        <asp:ValidationSummary ID="vssummary" runat="server" ValidationGroup="submit"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    </div>

                                                    <div class="col-12">

                                                        <asp:ListView ID="lvPayMaster" runat="server">
                                                            <LayoutTemplate>
                                                                <div id="demo-grid">
                                                                    <h4>Payment Gateway List</h4>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>

                                                                            <th style="text-align: center; width: 65px;">Edit</th>
                                                                            <th style="width: 65px">Sr. No.</th>
                                                                            <th>Payment Gateway Name</th>
                                                                            <th style="text-align: center">Status </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="text-align: center; width: 65px;">
                                                                        <asp:ImageButton ID="btnEditV2" runat="server" ImageUrl="~/Images/edit1.png" OnClick="btnEditV2_Click" CommandArgument='<%# Eval("PAYID") %>'
                                                                            AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" />
                                                                    </td>
                                                                    <td>
                                                                        <%# Container.DataItemIndex + 1%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("PAY_GATEWAY_NAME")%>
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <asp:Label ID="lblIsActive" runat="server" CssClass='<%# Eval("ACTIVESTATUS")%>' Text='<%# Eval("ACTIVESTATUS")%>'></asp:Label>
                                                                        <%--  <%# Eval("ACTIVESTATUS") %>--%>
                                                                        <%--<asp:Label ID="lblstatus" runat="server" Font-Bold="true" ForeColor='<%# Convert.ToInt32(Eval("ACTIVESTATUS")) == 1 ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'  Text='<%# Convert.ToInt32(Eval("ACTIVESTATUS")) ==1 ? "Active" : "DeActive" %>'></asp:Label>--%>
                                                    
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>

                                                        </asp:ListView>
                                                        </table>
                                                    </div>


                                                </asp:Panel>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane fade" id="tab_2">

                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="upd_PayConfig"
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

                                        <asp:UpdatePanel ID="upd_PayConfig" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <div id="pnlPayConfig" runat="server" visible="true">

                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Payment Gateway Name</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlpayment" runat="server" AppendDataBoundItems="True"
                                                                    TabIndex="1" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlpayment" runat="server" ControlToValidate="ddlpayment"
                                                                    Display="None" ErrorMessage="Please Select Payment Gateway Name." SetFocusOnError="true" ValidationGroup="submitTab2"
                                                                    InitialValue="0" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Instance</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlinstance" runat="server" AppendDataBoundItems="True"
                                                                    TabIndex="2" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">Test</asp:ListItem>
                                                                    <asp:ListItem Value="2">Live</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlinstance" runat="server" ControlToValidate="ddlinstance"
                                                                    Display="None" ErrorMessage="Please Select Instance." SetFocusOnError="true" ValidationGroup="submitTab2"
                                                                    InitialValue="0" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Merchant Id/Key </label>
                                                                </div>
                                                                <asp:TextBox ID="txtmerchantid" runat="server" TabIndex="3" MaxLength="50" CssClass="form-control" AutoComplete="off" placeholder="Merchant Id"
                                                                    ToolTip="Please Enter Merchant Id" />

                                                                <asp:RequiredFieldValidator ID="rfvtxtmerchantid" runat="server" ControlToValidate="txtmerchantid"
                                                                    Display="None" ErrorMessage="Please Enter Merchant Id." ValidationGroup="submitTab2" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">

                                                                    <label>Sub Merchant Id/Key </label>
                                                                </div>
                                                                <asp:TextBox ID="txtsubmerchantid" runat="server" TabIndex="4" MaxLength="50" CssClass="form-control" AutoComplete="off" placeholder="Sub Merchant Id"
                                                                    ToolTip="Please Enter Sub Merchant Id" />
                                                                <asp:RequiredFieldValidator ID="efvtxtsubmerchantid" runat="server" ControlToValidate="txtsubmerchantid"
                                                                    Display="None" ErrorMessage="Please Enter Sub Merchant Id." ValidationGroup="submitTab2" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                            </div>

                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Bank Fee Type </label>
                                                                </div>
                                                                <asp:TextBox ID="txtbankfeetype" runat="server" TabIndex="5" MaxLength="50" CssClass="form-control" AutoComplete="off" placeholder="Fee Type"
                                                                    ToolTip="Please Enter Fee Type" />
                                                                <asp:RequiredFieldValidator ID="rfvtxtbankfeetype" runat="server" ControlToValidate="txtbankfeetype"
                                                                    Display="None" ErrorMessage="Please Enter Fee Type" ValidationGroup="submitTab2" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtbankfeetype" runat="server" TargetControlID="txtbankfeetype" FilterType="Custom" FilterMode="InvalidChars"
                                                                    InvalidChars="~`!@#$%^*()+=,-./:;<>?'{}[]\|&&quot;'" />

                                                            </div>

                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Access Code/Encryption Key</label>
                                                                </div>
                                                                <asp:TextBox ID="txtaccesscode" runat="server" TabIndex="6" MaxLength="50" CssClass="form-control" AutoComplete="off" placeholder=" Access Code"
                                                                    ToolTip="Please Enter Access Code" />
                                                                <asp:RequiredFieldValidator ID="rfvtxtaccesscode" runat="server" ControlToValidate="txtaccesscode"
                                                                    Display="None" ErrorMessage="Please Enter Access Code." ValidationGroup="submitTab2" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                            </div>

                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Checksum Key/Working Key/Salt key/Sign Key</label>
                                                                </div>
                                                                <asp:TextBox ID="txtchecksumkey" runat="server" TabIndex="7" MaxLength="50" CssClass="form-control" AutoComplete="off" placeholder="Checksum Key/Working Key"
                                                                    ToolTip="Please Enter Checksum Key/Working Key" />
                                                                <asp:RequiredFieldValidator ID="tfvchecksumkey" runat="server" ControlToValidate="txtchecksumkey"
                                                                    Display="None" ErrorMessage="Please Enter Checksum Key/Working Key." ValidationGroup="submitTab2" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                            </div>

                                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Hash Sequence/EncryptionIV </label>
                                                                </div>
                                                                <asp:TextBox ID="txtHashSequence" runat="server" TabIndex="8" CssClass="form-control" AutoComplete="off" placeholder="Hash Sequence" MaxLength="32"
                                                                    ToolTip="Please Enter Hash Sequence" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtHashSequence" runat="server" TargetControlID="txtHashSequence" FilterType="Custom" FilterMode="InvalidChars"
                                                                    InvalidChars="~`!@#$%^*()_+=,-./:;<>?'{}[]\|&&quot;'" />

                                                            </div>

                                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Request URL/Base Url</label>
                                                                </div>
                                                                <asp:TextBox ID="txtrequesturl" runat="server" TabIndex="9" CssClass="form-control" AutoComplete="off" placeholder="Request URL"
                                                                    ToolTip="Please Enter Request URL" />

                                                                <asp:RequiredFieldValidator ID="rfvtxtrequesturl" runat="server" ControlToValidate="txtrequesturl"
                                                                    Display="None" ErrorMessage="Please Enter Request URL." ValidationGroup="submitTab2" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                            </div>

                                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Response URL</label>
                                                                </div>
                                                                <asp:TextBox ID="txtresponseurl" runat="server" TabIndex="10" CssClass="form-control" AutoComplete="off" placeholder="Response URL"
                                                                    ToolTip="Please Enter Response URL" />
                                                                <asp:RequiredFieldValidator ID="rfvtxtresponseurl" runat="server" ControlToValidate="txtresponseurl"
                                                                    Display="None" ErrorMessage="Please Enter Response URL." ValidationGroup="submitTab2" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                            </div>

                                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>PG Page URL</label>
                                                                </div>
                                                                <asp:TextBox ID="txtPgPageUrl" runat="server" TabIndex="11" CssClass="form-control" AutoComplete="off" placeholder="Response URL"
                                                                    ToolTip="Please Enter PG Page URL" />
                                                                <asp:RequiredFieldValidator ID="rfvtxtPgPageUrl" runat="server" ControlToValidate="txtPgPageUrl"
                                                                    Display="None" ErrorMessage="Please Enter PG Page." ValidationGroup="submitTab2" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="row">
                                                                    <div class="form-group col-6">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Status</label>
                                                                        </div>
                                                                        <div class="switch form-inline">
                                                                            <input type="checkbox" id="rdActive1" name="switch" checked />
                                                                            <label data-on="Active" data-off="Inactive" for="rdActive1"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-4 col-md-12 col-12">
                                                                <div class=" note-div">
                                                                    <h5 class="heading">Note (Please Select)</h5>
                                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Checked : Status- - <span style="color: green; font-weight: bold">Active</span></span>  </p>
                                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>UnChecked : Status - <span style="color: red; font-weight: bold">InActive</span></span></p>
                                                                </div>
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:Button ID="btnSubmitV2" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submitTab2" OnClientClick="return validate1();"
                                                                    OnClick="btnSubmitV2_Click" TabIndex="12" CssClass="btn btn-info" />

                                                                <asp:Button ID="btnclearV2" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                                    OnClick="btnclearV2_Click" TabIndex="13" CssClass="btn btn-warning" />

                                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submitTab2"
                                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <asp:Panel ID="pnlPGCongig" runat="server" Visible="false">

                                                        <div class="col-12">
                                                            <asp:ListView ID="lvPayConfig" runat="server">
                                                                <LayoutTemplate>

                                                                    <div id="demo-grid">
                                                                        <h4>Payment Gateway Configuration List</h4>
                                                                    </div>
                                                                    <div class="table-responsive" style="overflow: scroll;">
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist1">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th style="text-align: center">Edit</th>

                                                                                    <th style="width: 65px">Payment Gateway Name</th>
                                                                                    <th style="text-align: center">Merchant Id/Key</th>
                                                                                    <th style="text-align: center">Access Code</th>
                                                                                    <th style="text-align: center">Checksum Key</th>
                                                                                    <th style="text-align: center">Instance</th>
                                                                                    <th style="text-align: center">Status</th>
                                                                                    <%--<th style="text-align: center">Activity Name</th>--%>
                                                                                    <th style="text-align: center">Request URL </th>
                                                                                    <th style="text-align: center">Response URL </th>
                                                                                    <%-- <th style="text-align: center">Degree Name</th>
                                                                                <th style="text-align: center">College Name</th>--%>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody style="width: 100%">
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>

                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:ImageButton ID="btnEditrecords" runat="server" ImageUrl="~/Images/edit1.png" CommandArgument='<%# Eval("CONFIG_ID") %>'
                                                                                OnClick="btnEditrecords_Click" CausesValidation="false" AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="12" />
                                                                            <%--<asp:ImageButton ID="btnEditpp" runat="server" OnClick="btnEditrecords_Click1" ImageUrl="~/images/edit.gif"/>--%>
                                                                        </td>

                                                                        <td>
                                                                            <%# Eval("PAY_GATEWAY_NAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("MERCHANT_ID")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ACCESS_CODE")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("CHECKSUM_KEY")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("INSTANCE")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ACTIVESTATUS")%>
                                                                        </td>
                                                                        <%-- <td>
                                                                                     <%# Eval("ACTIVITYNAME")%>
                                                                                 </td>--%>
                                                                        <td>
                                                                            <%# Eval("REQUEST_URL")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("RESPONSE_URL")%>
                                                                        </td>
                                                                        <%-- <td>
                                                                                     <%# Eval("DEGREENAME")%>
                                                                                 </td>
                                                                                       <td>
                                                                                     <%# Eval("COLLEGENAME")%>
                                                                                 </td>--%>
                                                                    </tr>

                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                            <%-- </table>--%>
                                                        </div>
                                                    </asp:Panel>

                                                </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>

                                    <div class="tab-pane fade" id="tab_3">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="upd_payGatewayMapp"
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

                                        <asp:UpdatePanel ID="upd_payGatewayMapp" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Payment Gateway Name</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlpaymentV2" runat="server" AppendDataBoundItems="True"
                                                                TabIndex="1" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlpaymentV2_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlpaymentV2" runat="server" ControlToValidate="ddlpaymentV2"
                                                                Display="None" ErrorMessage="Please Select Payment Gateway Name." SetFocusOnError="true" ValidationGroup="submitTab3"
                                                                InitialValue="0" />

                                                        </div>



                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Activity Name</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlActivitynameV2" runat="server" AppendDataBoundItems="True"
                                                                TabIndex="2" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlActivitynameV2" runat="server" ControlToValidate="ddlActivitynameV2"
                                                                Display="None" ErrorMessage="Please Select Activity Name." SetFocusOnError="true"
                                                                InitialValue="0" ValidationGroup="submitTab3" />

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Reciept Type</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlReciepttypeV2" runat="server" AppendDataBoundItems="True"
                                                                TabIndex="3" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <%-- <asp:ListItem Value="1">Test</asp:ListItem>
                                                                <asp:ListItem Value="2">Live</asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlReciepttypeV2" runat="server" ControlToValidate="ddlReciepttypeV2"
                                                                Display="None" ErrorMessage="Please Select Instance." SetFocusOnError="true" ValidationGroup="submitTab3"
                                                                InitialValue="0" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Payment Gateway Configuration</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlPayGatewayConfigV2" runat="server" AppendDataBoundItems="True"
                                                                TabIndex="4" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <%-- <asp:ListItem Value="1">APIMER-HITKAN-SEN_TUTIONF-745DE2134D213F7CE39D639920028CAF</asp:ListItem>
                                                                <asp:ListItem Value="2">APIMER-HITKAN-1YR_TUHOSBU-745DE2134D213F7CE39D639920028CAF</asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlPayGatewayConfigV2" runat="server" ControlToValidate="ddlPayGatewayConfigV2"
                                                                Display="None" ErrorMessage="Please Select." SetFocusOnError="true" InitialValue="0" ValidationGroup="submitTab3" />

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>College</label>
                                                            </div>
                                                            <asp:ListBox runat="server" ID="ddlCollegeV2" SelectionMode="Multiple" AppendDataBoundItems="true" CssClass="form-control multi-select-demo">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:ListBox>

                                                            <%-- <asp:DropDownList ID="ddlCollegeV2" runat="server" AppendDataBoundItems="True"
                                                        TabIndex="5" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>--%>

                                                            <asp:RequiredFieldValidator ID="rfvddlCollegeV2" runat="server" ControlToValidate="ddlCollegeV2"
                                                                Display="None" ErrorMessage="Please Select College." SetFocusOnError="true" ValidationGroup="submitTab3"
                                                                InitialValue="0" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Degree</label>
                                                            </div>
                                                            <asp:ListBox runat="server" ID="ddlDegreeV2" SelectionMode="Multiple" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" TabIndex="6">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:ListBox>

                                                            <%--  <asp:DropDownList ID="ddlDegreeV2" runat="server" AppendDataBoundItems="True"
                                                        TabIndex="6" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>--%>

                                                            <asp:RequiredFieldValidator ID="rfvddlDegreeV2" runat="server" ControlToValidate="ddlDegreeV2"
                                                                Display="None" ErrorMessage="Please Select Degree." SetFocusOnError="true" ValidationGroup="submitTab3"
                                                                InitialValue="0" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Branch</label>
                                                            </div>
                                                            <asp:ListBox runat="server" ID="ddlBranchV2" SelectionMode="Multiple" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" TabIndex="7">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:ListBox>

                                                            <%-- <asp:DropDownList ID="ddlBranchV2" runat="server" AppendDataBoundItems="True"
                                                        TabIndex="7" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>--%>

                                                            <asp:RequiredFieldValidator ID="rfvddlBranchV2" runat="server" ControlToValidate="ddlBranchV2"
                                                                Display="None" ErrorMessage="Please Select Degree." SetFocusOnError="true" ValidationGroup="submitTab3"
                                                                InitialValue="0" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Semester</label>
                                                            </div>
                                                            <asp:ListBox runat="server" ID="ddlSemesterV2" SelectionMode="Multiple" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" TabIndex="8">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:ListBox>

                                                            <%-- <asp:DropDownList ID="ddlSemesterV2" runat="server" AppendDataBoundItems="True"
                                                        TabIndex="8" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>--%>

                                                            <asp:RequiredFieldValidator ID="rfvddlSemesterV2" runat="server" ControlToValidate="ddlSemesterV2"
                                                                Display="None" ErrorMessage="Please Select Semester." SetFocusOnError="true" ValidationGroup="submitTab3"
                                                                InitialValue="0" />
                                                        </div>


                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="row">
                                                                <div class="form-group col-6">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Status</label>
                                                                    </div>
                                                                    <div class="switch form-inline">
                                                                        <input type="checkbox" id="rdActive2" name="switch" checked />
                                                                        <label data-on="Active" data-off="Inactive" for="rdActive2"></label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-4 col-md-12 col-12">
                                                            <div class=" note-div">
                                                                <h5 class="heading">Note (Please Select)</h5>
                                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Checked : Status- - <span style="color: green; font-weight: bold">Active</span></span>  </p>
                                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>UnChecked : Status - <span style="color: red; font-weight: bold">InActive</span></span></p>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="btnSubmitPayConfiMappV2" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submitTab3" OnClientClick="return validate2();"
                                                                OnClick="btnSubmitPayConfiMappV2_Click" TabIndex="9" CssClass="btn btn-info" />

                                                            <asp:Button ID="btnSubmitPayConfiMappClearV2" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                                OnClick="btnSubmitPayConfiMappClearV2_Click" TabIndex="10" CssClass="btn btn-warning" />

                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="submitTab3"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="col-12">
                                                    <asp:Panel ID="div_PaymentGatewayMappingList" runat="server" Visible="false">

                                                        <asp:ListView ID="lvPaymentGatewayMapping" runat="server">
                                                            <LayoutTemplate>
                                                                <div id="demo-grid">
                                                                    <h4>Payment Gateway Mapping list</h4>
                                                                </div>

                                                                <div class="table-responsive" style="overflow: scroll;">
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist2">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>

                                                                                <th style="text-align: center">Edit</th>
                                                                                <%--<th style="width: 65px">Sr. No.</th>--%>
                                                                                <th>Paymeny Gateway Name</th>
                                                                                <th>Reciept Type</th>
                                                                                <th>Activity Name</th>
                                                                                <th>Payment Mapping Code</th>
                                                                                <th>College</th>
                                                                                <th>Degree</th>
                                                                                <th>Branch</th>
                                                                                <th>Semester</th>
                                                                                <th style="text-align: center">Status </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody style="width: 100%">
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="text-align: center">
                                                                        <asp:ImageButton ID="btnEditMappingV2" runat="server" ImageUrl="~/Images/edit1.png" OnClick="btnEditMappingV2_Click" CommandArgument='<%# Eval("PGM_ID") %>'
                                                                            AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="11" />
                                                                    </td>
                                                                    <%-- <td>
                                                                        <%# Container.DataItemIndex + 1%>
                                                                    </td>--%>
                                                                    <td>
                                                                        <%# Eval("PAY_GATEWAY_NAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("RECIEPT_TYPE")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ACTIVITYNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("CONFIG_NAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("COLLEGE_NAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("DEGREE_NAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("BRANCH_NAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SEMESTER_NAME")%>
                                                                    </td>


                                                                    <td style="text-align: center">
                                                                        <asp:Label ID="lblIsActive" runat="server" CssClass='<%# Eval("ACTIVESTATUS")%>' Text='<%# Eval("ACTIVESTATUS")%>'></asp:Label>
                                                                        <%--  <%# Eval("ACTIVESTATUS") %>--%>
                                                                        <%--<asp:Label ID="lblstatus" runat="server" Font-Bold="true" ForeColor='<%# Convert.ToInt32(Eval("ACTIVESTATUS")) == 1 ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'  Text='<%# Convert.ToInt32(Eval("ACTIVESTATUS")) ==1 ? "Active" : "DeActive" %>'></asp:Label>--%>
                                                    
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>

                                                        </asp:ListView>
                                                        <%--</table>--%>
                                                    </asp:Panel>

                                                </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>

            </div>

        </ContentTemplate>
        <Triggers></Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function ValidTextbox() {
            var charactersOnly = document.getElementById('ctl00_ContentPlaceHolder1_txtPaymentGName').value;
            if (!/^[a-zA-Z ]*$/g.test(charactersOnly)) {
                alert("Enter Characters Only");
                document.getElementById('ctl00_ContentPlaceHolder1_txtPaymentGName').value = " ";
                return false;
            }
        }

        function Demo() {
            //alert("Hii");
        }
    </script>


    <script>
        $(document).ready(function () {

            //binddatatable();
            //sys.webforms.pagerequestmanager.getinstance().add_endrequest(binddatatable);

            var val = true;
            SetStatusActive(val);
            SetStatusActive1(val);
            SetStatusActive2(val);
        });

        function bindDataTable() {
            //var myDT2 = $('#divsessionlist2').DataTable({});
            //var myDT3 = $('#divsessionlist3').DataTable({});
        }


    </script>

    <script>

        function SetStatusActive(val) {
            $('#rdActive').prop('checked', val);
            $("#<%= hfdActive.ClientID %>").val("true");

        }
        function validate() {
            if ($('#rdActive').prop('checked') == true) {
                $("#<%= hfdActive.ClientID %>").val("true");
            }
            else {
                $("#<%= hfdActive.ClientID %>").val("false");
            }

        }

        function SetStatusActive1(val) {
            $('#rdActive1').prop('checked', val);
            $("#<%= hfdActive1.ClientID %>").val("true");
        }
        function validate1() {
            if ($('#rdActive1').prop('checked') == true) {
                $("#<%= hfdActive1.ClientID %>").val("true");
            }
            else {
                $("#<%= hfdActive1.ClientID %>").val("false");
            }
        }


        function SetStatusActive2(val) {
            $('#rdActive2').prop('checked', val);
            $("#<%= hfdActive2.ClientID %>").val("true");
        }
        function validate2() {
            if ($('#rdActive2').prop('checked') == true) {
                $("#<%= hfdActive2.ClientID %>").val("true");
            }
            else {
                $("#<%= hfdActive2.ClientID %>").val("false");
            }
        }


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitV2').click(function () {
                    validate1();
                });

                $('#btnSaveV2').click(function () {
                    validate();
                });

                $('#btnSubmitPayConfiMappV2').click(function () {
                    validate2();
                });
            });
        });

    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>

</asp:Content>

