<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StrIndentPrepare.aspx.cs" Inherits="STORES_Transactions_Quotation_StrIndentPrepare" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
    <script src="../../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-impromptu.2.6.min.js" type="text/javascript"></script>
    <link href="../../Scripts/impromptu.css" rel="stylesheet" type="text/css" />
    <script src="../../../jquery/jquery-1.10.2.js" type="text/javascript"></script>

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




    <asp:UpdatePanel ID="updatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">INDENT PREPRATION FORM</h3>
                        </div>
                        <div>
                            <form role="form">
                                <div class="box-body">
                                    <div class="col-md-12" id="divRequisitionList" runat="server" visible="true">
                                        Note <b>:</b> <span style="color: #FF0000">* Marked Fields Are Mandatory.</span>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">Indent List For Quotation</div>
                                            <div class="panel-body">
                                                <asp:Panel ID="pnlReqIndent" runat="server">
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">Filter Indent</div>
                                                        <div class="panel-body">
                                                            <div class="form-group col-md-12">
                                                                <div class="form-group col-md-3">
                                                                    <asp:CheckBox ID="chkDept" runat="server" Text="Department Wise" AutoPostBack="True" OnCheckedChanged="chkDept_CheckedChanged" />
                                                                    <div style="padding-top: 20px;">
                                                                        <asp:DropDownList ID="ddlDepts" runat="server" Style="padding-top: 8px" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlDepts_SelectedIndexChanged" Visible="False" CssClass="form-control">
                                                                            <asp:ListItem Text="Please Select" Value="0" />
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                </div>
                                                                <asp:CheckBox ID="chkDate" runat="server" Text="Date Wise" AutoPostBack="True" OnCheckedChanged="chkDate_CheckedChanged" /><br />

                                                                <div class="form-group col-md-3" id="divSD" runat="server" visible="false">
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
                                                                         <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meReqdate" ControlToValidate="txtStartDate"
                                                                                            IsValidEmpty="false" ErrorMessage="Please Enter Valid From Date In [dd/MM/yyyy] format"
                                                                                            InvalidValueMessage="Please Enter Valid  From Date [dd/MM/yyyy] format" Display="None" SetFocusOnError="true"
                                                                                            Text="*" ValidationGroup="StoreSearch"></ajaxToolKit:MaskedEditValidator>
                                                                          <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtStartDate" Display="None"
                                                                                            ErrorMessage="Please Enter From Date." ValidationGroup="StoreSearch" />


                                                                    </div>
                                                                </div>




                                                                <div class="form-group col-md-3" id="divED" runat="server" visible="false">
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
                                                                        <asp:CompareValidator ID="cmpValidator" runat="server" ControlToCompare="txtStartDate"
                                                                            ControlToValidate="txtLastDate" Display="None" ErrorMessage="To Date should be greater than equal to From Date"
                                                                            Operator="GreaterThanEqual" Type="Date" ValidationGroup="StoreSearch"></asp:CompareValidator>
                                                                          <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtLastDate"
                                                                                            IsValidEmpty="false" ErrorMessage="Please Enter Valid To Date In [dd/MM/yyyy] format"
                                                                                            InvalidValueMessage="Please Enter Valid  To Date [dd/MM/yyyy] format" Display="None" SetFocusOnError="true"
                                                                                            Text="*" ValidationGroup="StoreSearch"></ajaxToolKit:MaskedEditValidator>
                                                                                   <asp:RequiredFieldValidator runat="server" ID="rfvDOfBirth" ControlToValidate="txtLastDate" Display="None"
                                                                                            ErrorMessage="Please Enter To Date." ValidationGroup="StoreSearch" />


                                                                    </div>
                                                                </div>
                                                                <div class="form-group col-md-3" style="padding-top: 20px">
                                                                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" CssClass="btn btn-info" ValidationGroup="StoreSearch" Visible="False" />
                                                                    <asp:ValidationSummary ID="validationsummary3" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="StoreSearch" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">Select Requisition For Items</div>
                                                        <div class="panel-body">
                                                            <div class="form-group col-md-12">
                                                                <div class="col-md-12 table-responsive">
                                                                    <asp:GridView ID="grdReqList" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue" DataKeyNames="REQTRNO" EmptyDataText="There Is No Item For Indent" OnPageIndexChanging="grdReqList_PageIndexChanging" OnSelectedIndexChanging="grdReqList_SelectedIndexChanging" OnSorting="grdReqList_Sorting" PageSize="20">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="REQTRNO" HeaderText="ReqTr.N0" HeaderStyle-CssClass="bg-light-blue" />
                                                                            <asp:BoundField DataField="REQ_NO" HeaderText="Req.No" SortExpression="REQ_NO" HeaderStyle-CssClass="bg-light-blue">
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="MDNAME" HeaderText="Department" SortExpression="MDNAME" HeaderStyle-CssClass="bg-light-blue">
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="REQ_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-CssClass="bg-light-blue" HeaderText="Req.Date" SortExpression="REQ_DATE">
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:BoundField>
                                                                            <asp:CommandField SelectText="Show Items" ShowSelectButton="True" HeaderStyle-CssClass="bg-light-blue">
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
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="text-center">
                                                        <asp:Button ID="btnReqListNext" runat="server" Width="100px" CssClass="btn btn-info" Text="Next" OnClick="btnReqListNext_Click" />
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12" id="divItemDetails" runat="server" visible="false">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">Item List For Selected REQ.</div>
                                            <div class="panel-body">
                                                <asp:Panel ID="Panel3" runat="server">
                                                    <div class="col-md-12 table-responsive">
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
                                                                <asp:BoundField DataField="REQ_NO" HeaderText="REQ No" HeaderStyle-CssClass="bg-light-blue">
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

                                                            </Columns>
                                                            <EmptyDataRowStyle BackColor="WhiteSmoke" />
                                                            <SelectedRowStyle CssClass="grid_bg " />
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="text-center">
                                                        <asp:HiddenField ID="hdfTot" runat="server" />
                                                        <asp:Button ID="btnDivIDBack" runat="server" Width="100px" CssClass="btn btn-info" Text="Back" OnClick="btnDivIDBack_Click" />
                                                        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" OnClientClick="checkValidation()" CssClass="btn btn-primary" Text="Add" />
                                                        <asp:Button ID="btnDivIDNext" runat="server" Width="100px" CssClass="btn btn-info" Text="Next" OnClick="btnDivIDNext_Click" />
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12" id="divItemList" runat="server" visible="false">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">Indent Item List.</div>
                                            <div class="panel-body">
                                                <div class="col-md-12 table-responsive">
                                                    <asp:GridView ID="grdIndentItemList" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue" DataKeyNames="REQ_TNO" OnRowDataBound="grdIndentItemList_RowDataBound" OnRowDeleting="grdIndentItemList_RowDeleting">
                                                        <Columns>
                                                            <asp:BoundField DataField="REQ_NO" HeaderText="REQ No" HeaderStyle-CssClass="bg-light-blue">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ITEM_NAME" HeaderStyle-Width="30%" HeaderText="ITEM NAME" HeaderStyle-CssClass="bg-light-blue">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>

                                                            <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="10%">
                                                                <ItemTemplate>

                                                                    <asp:TextBox ID="txtIndQty" runat="server" Text='<%#Eval("REQ_QTY") %>' Enabled="false" CssClass="form-control" MaxLength="5"></asp:TextBox>
                                                                    <asp:HiddenField ID="hdfItemNo" runat="server" Value='<%#Eval("ITEM_NO") %>' />
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeQty" runat="server"
                                                                        FilterType="Custom,Numbers" TargetControlID="txtIndQty" ValidChars=".">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>

                                                                    <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Visible="false" CssClass="btn btn-danger" OnClientClick="return confirmDelete()" Text="Delete" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle BackColor="#CCCCCC" />
                                                        <SelectedRowStyle CssClass="grid_bg " />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12" id="divIndentDetails" runat="server" visible="false">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">Indent Ref. No</div>
                                            <div class="panel-body">
                                                <div class="col-md-12">
                                                    <div class="col-md-6">
                                                        <div class="col-md-10" id="span1" runat="server">
                                                            <label>Indent Ref. No:<span style="color: #FF0000">*</span></label>
                                                            <asp:TextBox ID="txtIndRefNo" runat="server" Enabled="false" TabIndex="1" ToolTip="Enter Indent Ref No" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtIndRefNo" runat="server" ControlToValidate="txtIndRefNo" Display="None" ErrorMessage="Indent Ref No Required" ValidationGroup="Store"></asp:RequiredFieldValidator>
                                                            <asp:DropDownList ID="ddlIndent" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlIndent_SelectedIndexChanged" ToolTip="Select Indent For Update" Visible="False">
                                                                <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="col-md-10">
                                                            <label>Person Name:</label>

                                                            <asp:TextBox ID="txtPName" runat="server" TabIndex="3" ToolTip="Enter Person Name" MaxLength="64" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="col-md-10">
                                                            <label>Indent Date:<span id="span2" style="color: #FF0000">*</span></label>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtindDate" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Enter Indent Date"></asp:TextBox>
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="ImaCalSDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" ToolTip="Select Date" />
                                                                </div>

                                                                <ajaxToolKit:CalendarExtender ID="ceLasteDateofReciptTime" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="ImaCalSDate" TargetControlID="txtindDate">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" AcceptNegative="Left"
                                                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                                                            OnInvalidCssClass="errordate" TargetControlID="txtindDate" ClearMaskOnLostFocus="true">
                                                                                        </ajaxToolKit:MaskedEditExtender>


                                                                

                                                                                        <ajaxToolKit:MaskedEditValidator ID="mevDate" runat="server" ControlExtender="MaskedEditExtender4" ControlToValidate="txtindDate"
                                                                                            IsValidEmpty="false" ErrorMessage="Please Enter Valid Indent Date In [dd/MM/yyyy] format" EmptyValueMessage="Please Select Indent Date"
                                                                                            InvalidValueMessage="Please Enter Valid  Indent Date In [dd/MM/yyyy] format" Display="None" SetFocusOnError="true"
                                                                                            Text="*" ValidationGroup="Store"></ajaxToolKit:MaskedEditValidator>

                                                                <asp:RequiredFieldValidator ID="rfvtxtindDate" runat="server" ControlToValidate="txtindDate" Display="None" ErrorMessage="Indent Date Required" ValidationGroup="Store"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-10">
                                                            <label>Remark:</label>

                                                            <asp:TextBox ID="txtRemark" runat="server" MaxLength="200" TabIndex="4" ToolTip="Enter Remark" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                            <asp:ValidationSummary ID="validationsummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Store" />

                                                            <asp:ValidationSummary ID="validationsummary2" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="StoreInd" />

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="panel panel-info">
                                            <div class="panel-heading">Send To</div>
                                            <div class="panel-body">
                                                <div class="col-md-12">
                                                    <div class="col-md-7">
                                                        <div class="form-group col-md-3" id="span3" runat="server">
                                                            <label>Department: <span style="color: #FF0000">*</span></label>
                                                        </div>
                                                        <div class="form-group col-md-9">
                                                            <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="True" CssClass="form-control" TabIndex="5" ToolTip="Select Department">
                                                                <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlDept" runat="server" ControlToValidate="ddlDept" Display="None" ErrorMessage="Select Department" InitialValue="0" ValidationGroup="Store"></asp:RequiredFieldValidator>

                                                        </div>
                                                        <div class="form-group col-md-3">
                                                            <label>Type: </label>
                                                        </div>

                                                        <div class="form-group col-md-4">
                                                            <asp:RadioButtonList ID="rdbtnList" runat="server" TabIndex="6">
                                                                <asp:ListItem Selected="True" Text="Quotation" Value="Q"></asp:ListItem>
                                                                <asp:ListItem Text="Open Tender" Value="T" ></asp:ListItem>
                                                                <asp:ListItem Text="Direct PO" Value="D" ></asp:ListItem>
                                                                <%--<asp:ListItem Text="Proprietor" Value="P" TabIndex="9"></asp:ListItem>--%>
                                                                <asp:ListItem Text="Limited Tender" Value="L" TabIndex="10"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>

                                                        <div class="col-md-3">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12 text-center">
                                                    <asp:Button ID="btnDivIndentDBack" runat="server" Width="100px" CssClass="btn btn-info" Text="Back" OnClick="btnDivIndentDBack_Click" />
                                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Save Indent" ToolTip="Click To Save " TabIndex="11" CssClass="btn btn-primary" ValidationGroup="Store" />
                                                    <asp:Button ID="btnModify" runat="server" OnClick="btnModify_Click" TabIndex="12" Text="Modify" ToolTip="Click To Modify" CssClass="btn btn-warning" />
                                                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" TabIndex="13" Text="Cancel" ToolTip="Click To Reset" CssClass="btn btn-warning" />
                                                    <asp:Button ID="btnDivIndentDNext" runat="server" Width="100px" CssClass="btn btn-info" Text="Next" OnClick="btnDivIndentDNext_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12" id="divIndentReport" runat="server" visible="false">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">Select Date </div>
                                            <div class="panel-body">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            <span style="color: red">*</span> From Date:
                                                        </label>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtFromDate" Style="z-index: 0" runat="server" ValidationGroup="StoreRepSearch" CssClass="form-control" TabIndex="1" ToolTip="Enter Start Date"></asp:TextBox>
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="ImaCalFromDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            </div>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Left" Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="ImaCalFromDate" TargetControlID="txtFromDate">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlToValidate="txtFromDate"
                                                                        ControlExtender="MaskedEditExtender2" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" SetFocusOnError="true"
                                                                        IsValidEmpty="true" InvalidValueMessage="Please Enter Valid From Date In [dd/MM/yyyy] format"
                                                                        Display="None" Text="*" ValidationGroup="StoreRepSearch">                                                                       
                                                                    </ajaxToolKit:MaskedEditValidator>
                                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFromDate"
                                                        ErrorMessage="Please Enter Form Date" Display="None" ValidationGroup="StoreRepSearch"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <label><span style="color: red">*</span> To Date:</label>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtToDate" runat="server" Style="z-index: 0" ValidationGroup="StoreRepSearch" CssClass="form-control" ToolTip="Enter End Date" TabIndex="2"></asp:TextBox>
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="ImaCalToDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            </div>

                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptNegative="Left" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                 CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Left" Enabled="True" 
                                                                ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="ImaCalToDate" TargetControlID="txtToDate">
                                                            </ajaxToolKit:CalendarExtender>
                                                             <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlToValidate="txtToDate"
                                                                        ControlExtender="MaskedEditExtender3" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" SetFocusOnError="true"
                                                                        IsValidEmpty="true" InvalidValueMessage="Please Enter Valid To Date In [dd/MM/yyyy] format"
                                                                        Display="None" Text="*" ValidationGroup="StoreRepSearch">                                                                       
                                                                    </ajaxToolKit:MaskedEditValidator>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtToDate"
                                                        ErrorMessage="Please Enter To Date" Display="None" ValidationGroup="StoreRepSearch"></asp:RequiredFieldValidator>
                                                            <%--<asp:CompareValidator ID="cmpvDate" runat="server" ControlToCompare="txtFromDate" ControlToValidate="txtToDate"
                                                                 Display="None" ErrorMessage="End Date should be greater than Start Date " Operator="GreaterThanEqual" 
                                                                SetFocusOnError="True" Type="Date" ValidationGroup="StoreInd"></asp:CompareValidator>--%>


                                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtFromDate"
                                                                ControlToValidate="txtToDate" Display="None" ErrorMessage="To Date should be greater than equal to From Date"
                                                                Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="StoreRepSearch"></asp:CompareValidator>


                                                        </div>
                                                    </div>
                                                    <div class="col-md-4" style="padding-top: 24px">
                                                        <label></label>
                                                        <asp:Button ID="btnSearchRep" runat="server" CssClass="btn btn-info" TabIndex="3" ToolTip="Click To Search" OnClick="btnSearchRep_Click" Text="Search" ValidationGroup="StoreRepSearch" />
                                                        <asp:ValidationSummary ID="validationsummary4" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="StoreRepSearch" />
                                                        <asp:Button ID="btnReportDate" runat="server" Visible="false" CssClass="btn btn-info" TabIndex="3" ToolTip="Click To Report" OnClick="btnReportDate_Click" Text="Report" ValidationGroup="StoreInd" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="panel panel-info">
                                            <div class="panel-heading">Indent Ref No.</div>
                                            <div class="panel-body">
                                                <div class="col-md-12" id="span4">
                                                    <label>
                                                        Select Indent To Display The Report<span style="color: #FF0000">*</span>
                                                    </label>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="col-md-12 table-responsive">
                                                        <asp:ListBox ID="lstIndent" runat="server" AutoPostBack="True" TabIndex="4" Height="200px" OnSelectedIndexChanged="lstIndent_SelectedIndexChanged" CssClass="form-control"></asp:ListBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-12 text-center">
                                                    <br />
                                                    <asp:Button ID="btnIndentReportBack" runat="server" Width="100px" CssClass="btn btn-info" Text="Back" OnClick="btnIndentReportBack_Click" />
                                                    <asp:Button ID="btnRpt" runat="server" Visible="false" CssClass="btn btn-info" OnClick="btnRpt_Click" Text="Show Report" />
                                                </div>
                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--  Reset the sample so it can be played again --%>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>


