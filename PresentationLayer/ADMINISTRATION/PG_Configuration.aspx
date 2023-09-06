<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PG_Configuration.aspx.cs" Inherits="ACADEMIC_PG_Configuration_aspx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
#ctl00_ContentPlaceHolder1_pnlPGCongig .dataTables_scrollHeadInner {
width: max-content !important;
}
</style>
    
    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdActive1" runat="server" ClientIDMode="Static" />
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="updPGconfiguration"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
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
                            <div class="col-12 btn-footer">
                                <div class="row">
                                    <asp:Panel ID="pnlradoselect" runat="server" Visible="false">
                                        <div class="col-md-12" style="text-align: center" runat="server" id="rdoselection">
                                            <asp:RadioButtonList ID="rblpg_config" runat="server" AppendDataBoundItems="true" RepeatDirection="Horizontal"
                                                AutoPostBack="true" OnSelectedIndexChanged="rblpg_config_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Selected="True">&nbsp;&nbsp; Payment Gateway Master &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2">&nbsp;&nbsp;Payment Gateway Configuration</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <asp:Panel ID="pnlPay_master" runat="server" Visible="false">


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
                                                Display="None" ErrorMessage="Please Enter Payment Gateway Name." ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftepaymentname" runat="server" TargetControlID="txtPaymentGName" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="~`!@#$%^*()_+=,-./:;<>?'{}[]\|&&quot;'" />
                                        </div>
                                        <!--===== Added By Rishabh on Dated 28/10/2021=====-->
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
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                        OnClick="btnSave_Click" TabIndex="4" CssClass="btn btn-info" OnClientClick="return validate();" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                        OnClick="btnCancel_Click" TabIndex="5" CssClass="btn btn-warning" />
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

                                                        <th style="text-align: center">Edit</th>
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
                                                <td style="text-align: center">
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit1.png" OnClick="btnEdit_Click" CommandArgument='<%# Eval("PAYID") %>'
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

                                <%--   </div>--%>
                            </asp:Panel>

                            <%--<asp:Panel  ID="pnlPayConfig" runat="server" Visible="false">--%>
                            <div id="pnlPayConfig" runat="server" visible="false">
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
                                            <asp:RequiredFieldValidator ID="rfvpayment" runat="server" ControlToValidate="ddlpayment"
                                                Display="None" ErrorMessage="Please Select Payment Gateway Name." SetFocusOnError="true" ValidationGroup="submit"
                                                InitialValue="0" />
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Merchant Id/Key</label>
                                            </div>
                                            <asp:TextBox ID="txtmerchantid" runat="server" TabIndex="2" MaxLength="50" CssClass="form-control" AutoComplete="off" placeholder="Merchant Id"
                                                ToolTip="Please Enter Merchant Id" />
                                            <asp:RequiredFieldValidator ID="rfvmerchantid" runat="server" ControlToValidate="txtmerchantid"
                                                Display="None" ErrorMessage="Please Enter Merchant Id." ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Access Code</label>
                                            </div>
                                            <asp:TextBox ID="txtaccesscode" runat="server" TabIndex="3" MaxLength="50" CssClass="form-control" AutoComplete="off" placeholder=" Access Code"
                                                ToolTip="Please Enter Access Code" />
                                            <asp:RequiredFieldValidator ID="rfvaccesscode" runat="server" ControlToValidate="txtaccesscode"
                                                Display="None" ErrorMessage="Please Enter Access Code." ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Checksum Key/Working Key/Salt key</label>
                                            </div>
                                            <asp:TextBox ID="txtchecksumkey" runat="server" TabIndex="4" MaxLength="50" CssClass="form-control" AutoComplete="off" placeholder="Checksum Key/Working Key"
                                                ToolTip="Please Enter Checksum Key/Working Key" />
                                            <asp:RequiredFieldValidator ID="tfvchecksumkey" runat="server" ControlToValidate="txtchecksumkey"
                                                Display="None" ErrorMessage="Please Enter Checksum Key/Working Key." ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Instance</label>
                                            </div>
                                            <asp:DropDownList ID="ddlinstance" runat="server" AppendDataBoundItems="True"
                                                TabIndex="5" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Test</asp:ListItem>
                                                <asp:ListItem Value="2">Live</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvinstance" runat="server" ControlToValidate="ddlinstance"
                                                Display="None" ErrorMessage="Please Select Instance." SetFocusOnError="true" ValidationGroup="submit"
                                                InitialValue="0" />
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Activity Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlActivityname" runat="server" AppendDataBoundItems="True"
                                                TabIndex="6" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                          
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvactivity" runat="server" ControlToValidate="ddlActivityname"
                                                Display="None" ErrorMessage="Please Select Activity Name." SetFocusOnError="true"
                                                InitialValue="0" />
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True"
                                                TabIndex="6" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                          
                                            </asp:DropDownList>
                                        <%--    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" ErrorMessage="Please Select Degree." SetFocusOnError="true"
                                                InitialValue="0" />--%>
                                        </div>
                                           <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Degree</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True"
                                                TabIndex="7" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                          
                                            </asp:DropDownList>
                                        <%--    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" ErrorMessage="Please Select Degree." SetFocusOnError="true"
                                                InitialValue="0" />--%>
                                        </div>
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <div class="label-dynamic">
                                                <label>Hash Sequence</label>
                                            </div>
                                            <asp:TextBox ID="txtHashSequence" runat="server" TabIndex="7" CssClass="form-control" AutoComplete="off" placeholder="Hash Sequence"
                                                ToolTip="Please Enter Hash Sequence" />

                                        </div>

                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Request URL/Base Url</label>
                                            </div>
                                            <asp:TextBox ID="txtrequesturl" runat="server" TabIndex="7" CssClass="form-control" AutoComplete="off" placeholder="Request URL"
                                                ToolTip="Please Enter Request URL" />
                                            <asp:RequiredFieldValidator ID="rfvrequesturl" runat="server" ControlToValidate="txtrequesturl"
                                                Display="None" ErrorMessage="Please Enter Request URL." ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Response URL</label>
                                            </div>
                                            <asp:TextBox ID="txtresponseurl" runat="server" TabIndex="8" CssClass="form-control" AutoComplete="off" placeholder="Response URL"
                                                ToolTip="Please Enter Response URL" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtresponseurl"
                                                Display="None" ErrorMessage="Please Enter Response URL." ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>PG Page URL</label>
                                            </div>
                                            <asp:TextBox ID="txtPgPageUrl" runat="server" TabIndex="9" CssClass="form-control" AutoComplete="off" placeholder="Response URL"
                                                ToolTip="Please Enter PG Page URL" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPgPageUrl"
                                                Display="None" ErrorMessage="Please Enter PG Page." ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>

                                        <!--===== Added By Rishabh on Dated 28/10/2021=====-->
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
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit" OnClientClick="return validate1();"
                                                OnClick="btnSubmit_Click" TabIndex="11" CssClass="btn btn-info" />
                                            <asp:Button ID="btnclear" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                OnClick="btnclear_Click" TabIndex="12" CssClass="btn btn-warning" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>

                                    </div>
                                </div>


                                <%-- style=" height:300px; overflow:scroll;"--%>
                                <asp:Panel ID="pnlPGCongig" runat="server" Visible="false">
                                    <div class="col-12">
                                        <asp:ListView ID="lvPayConfig" runat="server">
                                            <LayoutTemplate>

                                                <div id="demo-grid">
                                                    <h4>Payment Gateway Configuration List</h4>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th style="text-align: center">Edit</th>

                                                            <th style="width: 65px">Payment Gateway Name</th>
                                                            <th style="text-align: center">Merchant Id/Key</th>
                                                            <th style="text-align: center">Access Code</th>
                                                            <th style="text-align: center">Checksum Key</th>
                                                            <th style="text-align: center">Instance</th>
                                                            <th style="text-align: center">Active Status</th>
                                                            <th style="text-align: center">Activity Name</th>
                                                            <th style="text-align: center">Request URL </th>
                                                            <th style="text-align: center">Response URL </th>
                                                            <th style="text-align: center">Degree Name</th>
                                                            <th style="text-align: center">College Name</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <contenttemplate>
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
                                                        <td>
                                                            <%# Eval("ACTIVITYNAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("REQUEST_URL")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("RESPONSE_URL")%>
                                                        </td>
                                                            <td>
                                                            <%# Eval("DEGREENAME")%>
                                                        </td>
                                                              <td>
                                                            <%# Eval("COLLEGENAME")%>
                                                        </td>
                                                    </tr>
                                                        </contenttemplate>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
           
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lvPayConfig" />
            <%--<asp:PostBackTrigger ControlID="pnlPayConfig" />--%>
        </Triggers>
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
            alert("Hii");
        }
    </script>


    <script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#divpglist').DataTable({});
        }

    </script>

    <script>
        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }
        function validate() {
            if (Page_ClientValidate()) {
                $('#hfdActive').val($('#rdActive').prop('checked'));

            }
        }
        function SetStatActive1(val) {
            $('#rdActive1').prop('checked', val);
        }
        function validate1() {
            if (Page_ClientValidate()) {
                $('#hfdActive1').val($('#rdActive1').prop('checked'));

            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate1();
                });

                $('#btnSave').click(function () {
                    validate();
                });
            });
        });
    </script>
</asp:Content>

