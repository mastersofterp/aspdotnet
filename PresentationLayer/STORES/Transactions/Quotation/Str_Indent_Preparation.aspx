<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Str_Indent_Preparation.aspx.cs" Inherits="Stores_Transactions_Quotation_Str_Indent_Preparation"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
    <%--<script src="../../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-impromptu.2.6.min.js" type="text/javascript"></script>
    <link href="../../Scripts/impromptu.css" rel="stylesheet" type="text/css" />
    <script src="../../../jquery/jquery-1.10.2.js" type="text/javascript"></script>--%>
    <link href="../../../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../plugins/multiselect/bootstrap-multiselect.js"></script>

    <script language="javascript" type="text/javascript">

        function confirmDeleteResult(v, m, f) {
            if (v) //user clicked OK 
                $('#' + f.hidID).click();
        }


        function checkAll(ctrl) {
            var check = ctrl;
            debugger;
            if (ctrl.checked) {

                var count = $("#<%=grdItemList.ClientID %> tr").length;
                for (var i = 02; i <= count; i++) {
                    if (i < 10)
                        var control = "ctl00_ContentPlaceHolder1_grdItemList_ctl" + i + "_chk";
                    else
                        var control = "ctl00_ContentPlaceHolder1_grdItemList_ctl" + i + "_chk";

                    document.getElementById(control).checked = true;
                }
            }
            else {
                var count = $("#<%=grdItemList.ClientID %> tr").length;
                for (var i = 02; i <= count; i++) {
                    if (i < 10)
                        var control = "ctl00_ContentPlaceHolder1_grdItemList_ctl" + i + "_chk";
                    else
                        var control = "ctl00_ContentPlaceHolder1_grdItemList_ctl" + i + "_chk";

                    document.getElementById(control).checked = false;
                }
            }
        }

        //        $("#<%=btnAdd.ClientID %>").click(
        //          function() {
        //              var i = confirm("Are you sure");
        //              return false;
        //          });

        function checkValidation() {
            var count = $("#<%=grdItemList.ClientID %> tr").length;
            var countSelReq = 0;
            for (var i = 2; i <= count; i++) {
                if (i < 10) {
                    var control = "ctl00_ContentPlaceHolder1_grdItemList_ctl" + i + "_chk";
                    if (document.getElementById(control).checked == true)
                        countSelReq = countSelReq + 1;
                }
                else {
                    var control = "ctl00_ContentPlaceHolder1_grdItemList_ctl" + i + "_chk";
                    if (document.getElementById(control).checked == true)
                        countSelReq = countSelReq + 1;
                }
            }
            if (countSelReq == 0) {
                alert("Please select at least one item");
                return false;
            }
        }

        function confirmDelete() {
            if (confirm("Are you sure, you want to delete")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
       <script type="text/javascript" language="javascript">

           Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
           function BeginRequestHandler(sender, args) { var oControl = args.get_postBackElement(); oControl.disabled = true; }

    </script>

    <script type="text/javascript">
        function SelectAllTrainer(headchk) {
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (e.name.endsWith('chk')) {
                        if (headchk.checked == true) {
                            e.checked = true;
                            hdfTot.value = Number(hdfTot.value) + 1;
                        }
                        else

                            e.checked = false;
                    }
                }
            }
            if (headchk.checked == false)
                hdfTot.value = "0";
        }

        function validateAssign() {
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>').value;

            if (hdfTot == 0) {
                alert('Please Select Atleast One Item From The List');
                return false;
            }
            else
                return true;
        }
    </script>

    <%--    <asp:UpdatePanel ID="updatePanel1" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">INDENT PREPARATION</h3>
                </div>

                <div class="box-body">
                    <div class="col-12" id="divRequisitionList" runat="server" visible="true">
                        <asp:Panel ID="pnlReqIndent" runat="server">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Filter Indent</h5>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <asp:CheckBox ID="chkDept" runat="server" Text="Department Wise" AutoPostBack="True" OnCheckedChanged="chkDept_CheckedChanged" />
                                    </div>
                                    <asp:DropDownList ID="ddlDepts" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlDepts_SelectedIndexChanged" Visible="False" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Text="Please Select" Value="0" />
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <asp:CheckBox ID="chkDate" runat="server" Text="Date Wise" AutoPostBack="True" OnCheckedChanged="chkDate_CheckedChanged" />
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divSD" runat="server" visible="false">
                                    <label><span style="color: #FF0000">*</span>From Date :</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" ValidationGroup="StoreSearch"></asp:TextBox>
                                        <div class="input-group-addon">
                                            <asp:Image ID="ImaCalStartDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                        </div>
                                        <ajaxToolKit:MaskedEditExtender ID="meReqdate" runat="server" AcceptNegative="Left" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Left" Enabled="True" ErrorTooltipEnabled="True"
                                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtStartDate">
                                        </ajaxToolKit:MaskedEditExtender>

                                        <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            PopupButtonID="ImaCalStartDate" TargetControlID="txtStartDate">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meReqdate" ControlToValidate="txtStartDate"
                                            IsValidEmpty="false" ErrorMessage="Please Enter From Date" EmptyValueMessage="Please Enter From Date"
                                            InvalidValueMessage="From Date Is Invalid Enter In [dd/MM/yyyy] Format" Display="None" SetFocusOnError="true"
                                            Text="*" ValidationGroup="StoreSearch"></ajaxToolKit:MaskedEditValidator>



                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divED" runat="server" visible="false">
                                    <label><span style="color: #FF0000">*</span>To Date :</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtLastDate" runat="server" CssClass="form-control" ValidationGroup="StoreSearch"></asp:TextBox>
                                        <div class="input-group-addon">
                                            <asp:Image ID="ImaCalLastDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                        </div>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                            CultureTimePlaceholder="" DisplayMoney="Left" Enabled="True" ErrorTooltipEnabled="True"
                                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtLastDate">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:CalendarExtender ID="ceLastDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            PopupButtonID="ImaCalLastDate" TargetControlID="txtLastDate">
                                        </ajaxToolKit:CalendarExtender>

                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtLastDate"
                                            IsValidEmpty="false" ErrorMessage="Please Enter To Date" EmptyValueMessage="Please Enter To Date"
                                            InvalidValueMessage="To Date Is Invalid Enter In [dd/MM/yyyy] Format" Display="None" SetFocusOnError="true"
                                            Text="*" ValidationGroup="StoreSearch"></ajaxToolKit:MaskedEditValidator>



                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" CssClass="btn btn-info" ValidationGroup="StoreSearch" Visible="False" />
                                    <asp:ValidationSummary ID="validationsummary3" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="StoreSearch" />
                                </div>

                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Select Requisition For Items</h5>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <asp:GridView ID="grdReqList" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue" DataKeyNames="REQTRNO" EmptyDataText="There Is No Item For Indent" OnPageIndexChanging="grdReqList_PageIndexChanging" OnSelectedIndexChanging="grdReqList_SelectedIndexChanging" OnSorting="grdReqList_Sorting" PageSize="20">
                                        <Columns>
                                            <asp:BoundField DataField="REQTRNO" HeaderText="REQTRNO" HeaderStyle-CssClass="bg-light-blue" />
                                            <asp:BoundField DataField="REQ_NO" HeaderText="REQ. NO." SortExpression="REQ_NO" HeaderStyle-CssClass="bg-light-blue">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MDNAME" HeaderText="Department" SortExpression="MDNAME" HeaderStyle-CssClass="bg-light-blue">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="REQ_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-CssClass="bg-light-blue" HeaderText="REQ. Date" SortExpression="REQ_DATE">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:CommandField SelectText="Show Items" HeaderText="Action" ShowSelectButton="True" HeaderStyle-CssClass="bg-light-blue">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:CommandField>
                                        </Columns>
                                        <EmptyDataRowStyle BackColor="WhiteSmoke" />
                                        <HeaderStyle CssClass="gv_header" />
                                        <PagerSettings Mode="NextPrevious" NextPageText="Next" PreviousPageText="Prev" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="grid_bg " />
                                    </asp:GridView>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnReqListNext" runat="server" Width="100px" CssClass="btn btn-primary" Text="Next" OnClick="btnReqListNext_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>

                    <div class="col-12" id="divItemDetails" runat="server" visible="false">
                        <asp:Panel ID="Panel3" runat="server">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Item List For Selected REQ</h5>
                                    </div>
                                </div>
                                <div class="col-12 table-responsive">
                                    <asp:GridView ID="grdItemList" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue" DataKeyNames="REQ_TNO" EmptyDataText="No Items For Selection" OnRowDeleting="grdItemList_RowDeleting" OnSelectedIndexChanging="grdItemList_SelectedIndexChanging" ToolTip="Select Item For Indent">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkall" runat="server" onclick="SelectAllTrainer(this)" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="REQ_NO" HeaderText="REQ NO." HeaderStyle-CssClass="bg-light-blue">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ITEM_NAME" HeaderText="ITEM NAME" HeaderStyle-CssClass="bg-light-blue">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="REQ_QTY" HeaderText="REQ QTY" HeaderStyle-CssClass="bg-light-blue">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ITEM_SPECIFICATION" HeaderText="ITEM SPECIFICATION" HeaderStyle-CssClass="bg-light-blue">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LAST_PURCHASE_DATE" HeaderText="LAST PURCHASE DATE" HeaderStyle-CssClass="bg-light-blue">
                                                <HeaderStyle HorizontalAlign="Left"/>
                                                <ItemStyle HorizontalAlign="Left"/> 
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LAST_RATE" HeaderText="LAST PURCHASE RATE" HeaderStyle-CssClass="bg-light-blue">
                                                 <HeaderStyle HorizontalAlign="Left"/>
                                                <ItemStyle HorizontalAlign="Left"/> 
                                           </asp:BoundField>
                                            <asp:BoundField DataField="AVAILABLE_QTY" HeaderText="AVAILABLE STOCK" HeaderStyle-CssClass="bg-light-blue">
                                                 <HeaderStyle HorizontalAlign="Left"/>
                                                <ItemStyle HorizontalAlign="Left"/> 
                                           </asp:BoundField>

                                        </Columns>
                                        <EmptyDataRowStyle BackColor="WhiteSmoke" />
                                        <SelectedRowStyle CssClass="grid_bg " />
                                    </asp:GridView>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:HiddenField ID="hdfTot" runat="server" />
                                    <asp:Button ID="btnDivIDBack" runat="server" Width="100px" CssClass="btn btn-primary" Text="Back" OnClick="btnDivIDBack_Click" />
                                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" OnClientClick="checkValidation()" CssClass="btn btn-primary" Text="Add" />
                                    <asp:Button ID="btnDivIDNext" runat="server" Width="100px" CssClass="btn btn-primary" Text="Next" OnClick="btnDivIDNext_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>

                    <div class="col-12" id="divItemList" runat="server" visible="false">
                        <div class="col-12">
                            <div class="sub-heading">
                                <h5>Indent Item List</h5>
                            </div>
                        </div>
                        <div class="col-12 table-responsive">
                            <asp:GridView ID="grdIndentItemList" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue" DataKeyNames="REQ_TNO" OnRowDataBound="grdIndentItemList_RowDataBound" OnRowDeleting="grdIndentItemList_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="REQ_NO" HeaderText="REQ NO" HeaderStyle-CssClass="bg-light-blue">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ITEM_NAME" HeaderStyle-Width="30%" HeaderText="ITEM NAME" HeaderStyle-CssClass="bg-light-blue">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>

                                    <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="10%">
                                        <ItemTemplate>

                                            <asp:TextBox ID="txtIndQty" runat="server" Enabled="true" MaxLength="5" Text='<%#Eval("REQ_QTY") %>'></asp:TextBox>
                                            <asp:HiddenField ID="hdfItemNo" runat="server" Value='<%#Eval("ITEM_NO") %>' />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtDiscFilter" runat="server" FilterType="Numbers,Custom"
                                                FilterMode="ValidChars" TargetControlID="txtIndQty" ValidChars=".">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                     <asp:BoundField DataField="LAST_RATE" HeaderText="LAST RATE" HeaderStyle-CssClass="bg-light-blue">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="LAST_PURCHASE_DATE" HeaderText="LAST PURCHASE DATE" HeaderStyle-CssClass="bg-light-blue">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="AVAILABLE_QTY" HeaderText="AVAILABLE QTY" HeaderStyle-CssClass="bg-light-blue">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>

                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>

                                            <asp:Button ID="btnDelete" runat="server"  CommandName="Delete" CssClass="btn btn-danger" OnClientClick="return confirmDelete()" Text="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#CCCCCC" />
                                <SelectedRowStyle CssClass="grid_bg " />
                            </asp:GridView>
                        </div>
                    </div>

                    <div class="col-12" id="divIndentDetails" runat="server" visible="false">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Indent Ref. No.</h5>
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12" id="span1" runat="server">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Indent Ref. No.</label>
                                </div>
                                <asp:TextBox ID="txtIndRefNo" runat="server" Enabled="false" TabIndex="1" ToolTip="Enter Indent Ref No" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtIndRefNo" runat="server" ControlToValidate="txtIndRefNo" Display="None" ErrorMessage="Indent Ref No Required" ValidationGroup="Store"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlIndent" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlIndent_SelectedIndexChanged" ToolTip="Select Indent For Update" Visible="False">
                                    <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Person Name</label>
                                </div>
                                <asp:TextBox ID="txtPName" runat="server" TabIndex="3" ToolTip="Enter Person Name" MaxLength="100" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Indent Date</label>
                                </div>
                                <div class="input-group">
                                    <asp:TextBox ID="txtindDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <div class="input-group-addon">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                    </div>
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" AcceptNegative="Left" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Left" Enabled="True" ErrorTooltipEnabled="True"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtindDate">
                                    </ajaxToolKit:MaskedEditExtender>

                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="Image1" TargetControlID="txtindDate">
                                    </ajaxToolKit:CalendarExtender>

                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender4" ControlToValidate="txtindDate"
                                        IsValidEmpty="false" ErrorMessage="Please Select Indent Date" EmptyValueMessage="Please Select Indent Date"
                                        InvalidValueMessage="Indent Date Is Invalid Enter In [dd/MM/yyyy] Format" Display="None" SetFocusOnError="true"
                                        Text="*" ValidationGroup="Store"></ajaxToolKit:MaskedEditValidator>



                                </div>

                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Remark</label>
                                </div>
                                <asp:TextBox ID="txtRemark" runat="server" MaxLength="200" TabIndex="4" ToolTip="Enter Remark" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                <asp:ValidationSummary ID="validationsummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Store" />


                            </div>
                        </div>

                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Send To</h5>
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12" id="span3" runat="server">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Department</label>
                                </div>
                                <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="5" ToolTip="Select Department">
                                    <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlDept" runat="server" ControlToValidate="ddlDept" Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="Store"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-6 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Type</label>
                                </div>
                                <asp:RadioButtonList ID="rdbtnList" runat="server" TabIndex="6" RepeatColumns="3">
                                    <asp:ListItem Selected="True" Text="Quotation" Value="Q"></asp:ListItem>
                                    <asp:ListItem Text="Open Tender" Value="T" TabIndex="7"></asp:ListItem>
                                    <asp:ListItem Text="Direct PO" Value="D" TabIndex="8"></asp:ListItem>
                                    <%--<asp:ListItem Text="Proprietor" Value="P" TabIndex="9"></asp:ListItem>
                                    <asp:ListItem Text="Limited Tender" Value="L" TabIndex="10"></asp:ListItem>--%>
                                </asp:RadioButtonList>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnDivIndentDBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnDivIndentDBack_Click" />
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Save Indent" ToolTip="Click To Save " TabIndex="11" CssClass="btn btn-primary" ValidationGroup="Store" />
                                <asp:Button ID="btnModify" runat="server" OnClick="btnModify_Click" TabIndex="12" Text="Modify" ToolTip="Click To Modify" CssClass="btn btn-primary" />
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" TabIndex="13" Text="Cancel" ToolTip="Click To Reset" CssClass="btn btn-warning" />
                                <asp:Button ID="btnDivIndentDNext" runat="server" CssClass="btn btn-primary" Text="Next" OnClick="btnDivIndentDNext_Click" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12" id="divIndentReport" runat="server" visible="false">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Select Date</h5>
                                </div>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <%--<div class="label-dynamic">
                                    <label>From Date</label>
                                </div>--%>
                                <%--<div class="input-group date">
                                    <div class="input-group-addon" id="ImaCalFromDate" runat="server">
                                        <i class="fa fa-calendar text-blue" id="ImaCalFromDate1"></i>
                                    </div>
                                    <asp:TextBox ID="txtFromDate" Style="z-index: 0" runat="server" ValidationGroup="StoreDate" TabIndex="1" ToolTip="Enter Start Date"></asp:TextBox>

                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Left" Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDate">
                                    </ajaxToolKit:MaskedEditExtender>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="ImaCalFromDate1" TargetControlID="txtFromDate">
                                    </ajaxToolKit:CalendarExtender>
                                </div>--%>


                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Start Date </label>
                                </div>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i id="ImaCalFromDate" class="fa fa-calendar text-blue"></i>
                                    </div>
                                    <asp:TextBox ID="txtFromDate" runat="server" ToolTip="Select Start Date" CssClass="form-control"
                                        TabIndex="1" ValidationGroup="StoreInd"></asp:TextBox>

                                    <%--<asp:Image ID="ImaCalFromDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>

                                    <ajaxToolKit:MaskedEditExtender
                                        ID="MaskedEditExtender5" runat="server" TargetControlID="txtFromDate" Mask="99/99/9999"
                                        MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True">
                                    </ajaxToolKit:MaskedEditExtender>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                        Format="dd/MM/yyyy" PopupButtonID="ImaCalFromDate" TargetControlID="txtFromDate">
                                    </ajaxToolKit:CalendarExtender>


                                    <ajaxToolKit:MaskedEditValidator ID="mevFrom" runat="server"
                                        ControlExtender="MaskedEditExtender5" ControlToValidate="txtFromDate" Display="None"
                                        EmptyValueBlurredText="Empty" EmptyValueMessage="Please Select Start Date"
                                        InvalidValueBlurredMessage="Invalid Date"
                                        InvalidValueMessage="Start Date is Invalid (Enter dd/MM/yyyy Format)"
                                        SetFocusOnError="true" TooltipMessage="Please Select From Date" IsValidEmpty="false"
                                        ValidationGroup="StoreInd">
                                    </ajaxToolKit:MaskedEditValidator>

                                    <%-- <asp:RequiredFieldValidator ID="rfvtxtFromDate" runat="server" ControlToValidate="txtFromDate" SetFocusOnError="true"
                                                    Display="None" ValidationGroup="StoreRep" ErrorMessage="Please Enter Start  Date "></asp:RequiredFieldValidator>--%>
                                </div>

                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <%--<div class="label-dynamic">
                                    <label>To Date</label>
                                </div>--%>
                                <%--<div class="input-group date">
                                    <div class="input-group-addon" id="ImaCalToDate" runat="server">
                                        <i class="fa fa-calendar text-blue" id="ImaCalToDate1"></i>
                                    </div>
                                    <asp:TextBox ID="txtToDate" runat="server" Style="z-index: 0" ValidationGroup="StoreDate" CssClass="form-control" ToolTip="Enter End Date" TabIndex="2"></asp:TextBox>

                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptNegative="Left" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Left" Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate">
                                    </ajaxToolKit:MaskedEditExtender>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="ImaCalToDate1" TargetControlID="txtToDate">
                                    </ajaxToolKit:CalendarExtender>
                                    <asp:CompareValidator ID="cmpvDate" runat="server" ControlToCompare="txtFromDate" ControlToValidate="txtToDate" Display="None" ErrorMessage="To Date should be greater than From Date " Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="StoreInd"></asp:CompareValidator>
                                </div>--%>

                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>End Date</label>
                                </div>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i id="ImaCalToDate" class="fa fa-calendar text-blue"></i>
                                    </div>
                                    <asp:TextBox ID="txtToDate" runat="server" ToolTip="Select End Date" CssClass="form-control" TabIndex="2"
                                        ValidationGroup="StoreInd"></asp:TextBox>

                                    <%--<asp:Image ID="ImaCalToDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>

                                    <ajaxToolKit:MaskedEditExtender
                                        ID="MaskedEditExtender6" runat="server" TargetControlID="txtToDate" Mask="99/99/9999"
                                        MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True">
                                    </ajaxToolKit:MaskedEditExtender>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                        Format="dd/MM/yyyy" PopupButtonID="ImaCalToDate" TargetControlID="txtToDate">
                                    </ajaxToolKit:CalendarExtender>
                                    <%-- <asp:RequiredFieldValidator ID="rfvtxtToDate" runat="server" ControlToValidate="txtToDate"
                                                        Display="None" ValidationGroup="StoreRep" ErrorMessage="Please Enter End  Date "></asp:RequiredFieldValidator>--%>
                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server"
                                        ControlExtender="MaskedEditExtender6" ControlToValidate="txtToDate" Display="None"
                                        EmptyValueBlurredText="Empty" EmptyValueMessage="Please Select End Date"
                                        InvalidValueBlurredMessage="Invalid Date"
                                        InvalidValueMessage="End Date is Invalid (Enter dd/MM/yyyy Format)"
                                        SetFocusOnError="true" TooltipMessage="Please Select To Date" IsValidEmpty="false"
                                        ValidationGroup="StoreInd">
                                    </ajaxToolKit:MaskedEditValidator>
                                    <asp:CompareValidator ID="cmpValidator" runat="server" ControlToCompare="txtFromDate"
                                        ControlToValidate="txtToDate" Display="None" ErrorMessage="End Date should be greater than or equal to Start Date"
                                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="StoreInd"></asp:CompareValidator>
                                </div>



                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSearchRep" runat="server" CssClass="btn btn-primary" TabIndex="3" ToolTip="Click To Search" OnClick="btnSearchRep_Click" Text="Search" ValidationGroup="StoreInd" />
                                <asp:Button ID="btnReportDate" runat="server" Visible="false" CssClass="btn btn-info" TabIndex="3" ToolTip="Click To Report" OnClick="btnReportDate_Click" Text="Report" ValidationGroup="StoreInd" />
                                <asp:ValidationSummary ID="validationsummary2" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="StoreInd" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Indent Ref No.</h5>
                                </div>
                            </div>






                            <div class="form-group col-lg-3 col-md-6 col-12" id="span4" style="width: 500px;">
                                <div class="label-dynamic">
                                    <label>Select Indent To Display The Report</label>
                                </div>
                                <asp:ListBox ID="lstIndent" runat="server" AutoPostBack="True" TabIndex="4" OnSelectedIndexChanged="lstIndent_SelectedIndexChanged" CssClass="form-control multi-select-demo"></asp:ListBox>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnIndentReportBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnIndentReportBack_Click" />
                                <asp:Button ID="btnRpt" runat="server" CssClass="btn btn-info" OnClick="btnRpt_Click" Text="Show Report" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
                //maxWidth: 1000
            });
        });
        var parameter = Sys.WebForms.PageRequestManager;
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
                //maxWidth: 1000
            });
        });










    </script>






    <%--  Reset the sample so it can be played again --%>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
