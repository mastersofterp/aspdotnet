<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Approval_Requisition.aspx.cs" Inherits="STORES_Transactions_Quotation_Approval_Requisition"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">REQUISITION APPROVAL</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlDept" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-12 col-md-6 col-12">
                                    <div class="sub-heading">
                                        <h5>Requisitions Approval Or Rejection</h5>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Approval Department</label>
                                    </div>
                                    <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" data-select2-enable="true" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack="true" TabIndex="1" ToolTip="Select Approval Department">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label></label>
                                    </div>
                                    <asp:CheckBox ID="chkClubRequisition" Text="Club Requisitions" runat="server" Visible="false"
                                        OnCheckedChanged="chkClubRequisition_onchange" AutoPostBack="True" TabIndex="2" ToolTip="Club Requisitions" />

                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <div class="col-md-6" id="divPrevList" runat="server" visible="false">
                        <div id="tdpending" runat="server">
                            <div class="col-md-12 table-responsive">
                                <asp:Panel ID="pnllist" runat="server">

                                    <asp:GridView runat="server" ID="GridView1" AutoGenerateColumns="False" DataKeyNames="ApplicationID"
                                        CellPadding="4" ForeColor="#333333" CssClass="table table-bordered table-hover" HeaderStyle-CssClass="bg-light-blue" GridLines="None" Font-Names="Tahoma" Font-Size="Small">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select Requisitions" HeaderStyle-CssClass="bg-light-blue">
                                                <ItemTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox runat="server" ID="cbApplication" Text='<%# Eval("ApplicationName") %>'
                                                                    AutoPostBack="true" OnCheckedChanged="cbApplication_CheckedChanged" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:DataList Caption="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>REFERENCE NO</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>REQ. DATE</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>STATUS<b>"
                                                                    ID="dlTestCases" DataKeyField="CaseID" BorderStyle="Solid" BorderColor="Blue"
                                                                    BorderWidth="1px" runat="server" CellPadding="15" RepeatColumns="1" RepeatDirection="Horizontal"
                                                                    RepeatLayout="Table" AlternatingItemStyle-BackColor="White" CssClass="table table-bordered table-hover">
                                                                    <ItemTemplate>

                                                                        <%-- <asp:ImageButton runat="server" ID="edit" OnClick="edit_click" ImageUrl="~/IMAGES/edit.gif"
                                                                                                ToolTip='<%# Eval("REFS") %>' />--%>
                                                                        <asp:Label runat="server" ID="cbApplication" Text='<%# Eval("CaseName") %>' CssClass="" />

                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#EFF3FB" />
                                    </asp:GridView>

                                </asp:Panel>
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:ListView ID="lvReqList" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>Requisition List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Edit</th>
                                                    <th>Req. For</th>
                                                    <th>Requisition No.</th>
                                                    <th>Requestor</th>
                                                    <th>Req. Date</th>
                                                    <th>Status</th>
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
                                            <asp:ImageButton runat="server" ID="edit" OnClick="edit_click" ImageUrl="~/IMAGES/edit.png" ToolTip='<%# Eval("REQTRNO") %>' /></td>
                                        <td><%# Eval("REQ_FOR")%></td>
                                        <td><%# Eval("REQ_NO")%></td>
                                        <td><%# Eval("NAME")%></td>
                                        <td>
                                            <%# Eval("REQ_DATE", "{0:dd-MM-yyyy}")%>   
                                        </td>
                                        <td>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>

                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                    <div id="tdapprove" runat="server" visible="false">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Budget Head </label>
                                    </div>
                                    <asp:DropDownList ID="ddlbudget" data-select2-enable="true" runat="server" AppendDataBoundItems="true">
                                        <asp:ListItem Selected="True" Value="0" Text="--Please Select--"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <asp:DataList Caption="<b>APPROVED REQUISITIONS</b>" ID="dlTestCases" DataKeyField="CaseID"
                                        BorderStyle="Solid" BorderWidth="1px" runat="server" CellPadding="15" Font-Bold="true"
                                        HeaderStyle-ForeColor="Black" RepeatColumns="4" RepeatDirection="Horizontal" RepeatLayout="Table" AlternatingItemStyle-BackColor="White">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkReq" runat="server" ToolTip='<%# Eval("REFS") %>' />
                                            <asp:Label runat="server" ID="cbApplication" Text='<%# Eval("CaseName") %>' />
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnClubReq" CssClass="btn btn-primary" ToolTip="Click to Club Requisition" runat="server" Text="Club Requisition" OnClick="btnClubReq_click" />

                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-warning" ToolTip="Click to Clear" />
                            <asp:HiddenField ID="ReqNOS" runat="server" />

                        </div>
                    </div>

                    <asp:Panel ID="pnlItemDetail" runat="server">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>Requisition Slip No. :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblReqSlipNo" runat="server" Text=""></asp:Label>
                                        </a>
                                    </li>
                                    <li class="list-group-item"><b>Requisition Date :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblReqDate" runat="server" Text=""></asp:Label></a>
                                    </li>
                                    <li class="list-group-item"><b>Department :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblDeptName" runat="server" Text=""></asp:Label></a>
                                    </li>
                                    <li class="list-group-item"><b>Requisition By :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblAuthorityName" runat="server" Text=""></asp:Label></a>
                                    </li>
                                </ul>
                            </div>

                            <div class="col-lg-6 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item" id="divBudgetHead" runat="server" visible="false"><b>Budget Head Name :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblBudgetHead" runat="server" Text=""></asp:Label></a>
                                    </li>
                                    <li class="list-group-item" id="divShowAmt" runat="server" visible="false"><b>Budget Amount Balance :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblBudgetBalAmt" runat="server" Text=""></asp:Label></a>
                                    </li>
                                    <li class="list-group-item" id="divShowInprocessAmt" runat="server" visible="false"><b>Inprocess Budget Amount :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblInprocessBudgetAmt" runat="server" Text=""></asp:Label></a>
                                    </li>
                                </ul>
                            </div>
                        </div>


                        <div class="col-md-12">

                            <asp:ListView ID="lvitemReq" runat="server" OnItemCommand="lvitemReq_ItemCommand" OnItemDataBound="lvitemReq_ItemDataBound">
                                <EmptyDataTemplate>
                                    <br />
                                    <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" />
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div id="demo-grid">
                                        <div class="sub-heading mt-4">
                                            <h5>Requisition Details</h5>
                                        </div>
                                        <%-- <div class="form-group col-lg-6 col-md-12 col-12 mb-2">
                                            <div class=" note-div">
                                                <h5 class="heading">Note </h5>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Please check Items to accept and uncheck Items to reject</span> </p>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Approximate total cost is of accepted and rejected items both</span> </p>
                                            </div>
                                        </div>--%>

                                        <div>

                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th id="thEdit" runat="server" visible="false">Edit</th>
                                                        <th id="thAccRej" runat="server" visible="false">Accept/Reject</th>
                                                        <%--<th style="width: 20%">Req. No.
                                                                </th>--%>
                                                        <%-- <th style="width: 10%">Req.Date.
                                                                </th>--%>
                                                        <th>Items
                                                        </th>
                                                        <th>Requested Qty

                                                        </th>
                                                        <th>Approved QtY
                                                        </th>
                                                        <th id="thApproxCost" runat="server">Approximate Cost
                                                        </th>
                                                        <th id="thItemSpec" runat="server" visible="false">Item Specification
                                                        </th>

                                                        <th id="thStatus" runat="server" visible="false">Status
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
                                        <td id="tdedit" runat="server" visible="false">
                                            <asp:ImageButton ID="btnEditItem" runat="server" CommandArgument='<%# Eval("item_no") %>'
                                                CommandName="modify" ImageUrl="~/images/edit.gif" ToolTip="Edit Record" AlternateText="Edit Record" />
                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("item_no") %>'
                                                CommandName="remove" ImageUrl="~/images/delete.gif" ToolTip="Delete Record" AlternateText="Delete Record"
                                                OnClientClick="return showpopup();" Visible="false" />
                                        </td>
                                        <td id="thEditAccRej" runat="server" visible="false">
                                            <asp:CheckBox ID="chkItem" runat="server" ToolTip='<%# Eval("REQ_TNO")%>' />

                                        </td>

                                        <td>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM_NAME")%>'></asp:Label>
                                            <asp:HiddenField ID="hdReqtrNo" runat="server" Value='<%# Eval("REQTRNO")%>' />
                                            <asp:HiddenField ID="hdbAcceptReject" runat="server" Value='<%# Eval("ITEM_ACCEPT_REJECT")%>' />
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Label ID="lblREQUESTED_QTY" runat="server" Visible="true" Text='<%# Eval("REQUESTED_QTY")%>'></asp:Label>

                                        </td>
                                        <td>
                                            <asp:Label ID="lblQty" runat="server" Visible="false" Text='<%# Eval("REQ_QTY")%>'></asp:Label>
                                            <asp:HiddenField ID="hdnRate" runat="server" Value='<%# Eval("RATE")%>'></asp:HiddenField>
                                            <asp:TextBox ID="txtqty" CssClass="form-control" MaxLength="4" Visible="true" runat="server" Text='<%# Eval("REQ_QTY")%>' onblur="return CalApproximatCost(this);"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeQty" runat="server"
                                                FilterType="Custom,Numbers" TargetControlID="txtqty" ValidChars="">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </td>
                                        <td id="tdApproxCost" runat="server">
                                            <asp:Label ID="lblCost" runat="server" Text='<%# Eval("Tot_Cost")%>'></asp:Label>
                                        </td>
                                        <td id="tdItemSpec" runat="server" visible="false">
                                            <asp:Label ID="lblItemSpeci" runat="server" Text='<%# Eval("ITEM_SPECIFICATION")%>'></asp:Label>
                                        </td>


                                        <td id="tdStatus" runat="server" visible="false" style="width: 15%">
                                            <%-- <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("AR_STATUS") == "R" ? "Reject":"Accept" %>'></asp:Label>   --%>
                                            <%# Eval("AR_STATUS")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                        </div>
                        <div class="col-12 mb-4" runat="server" id="divApproxCost">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Approximate Total Cost :</b>
                                            <a class="sub-label">
                                                <asp:Label runat="server" Font-Bold="true" ID="lblTotAppCost"></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnrowcount" runat="server" Value="0" />
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="col-12 mt-3">
                            <div class="row">
                                <div class="col-12 mt-3">
                                    <div class="sub-heading">
                                        <h5>Requisitions Approval Or Rejection Table</h5>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divStage" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Stage Name </label>
                                    </div>
                                    <asp:Label ID="lblLeaveName" runat="server"></asp:Label>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divRefNo" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Reference Number </label>
                                    </div>
                                    <asp:Label ID="lblReason" runat="server"></asp:Label>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Select</label>
                                    </div>
                                    <asp:DropDownList ID="ddlSelect" runat="server" CssClass="form-control" Font-Size="12px"
                                        AppendDataBoundItems="true">
                                        <asp:ListItem Value="A">Approve</asp:ListItem>
                                        <asp:ListItem Value="R">Reject</asp:ListItem>
                                        <%-- <asp:ListItem Value="F">Final Approve</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">

                                        <label>Remarks</label>
                                    </div>
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine"
                                        Height="50px" />
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <asp:ListView ID="lvStatus" runat="server">
                                <EmptyDataTemplate>
                                    <asp:Label ID="ibler" runat="server" Text=""></asp:Label>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div id="temp_grid">
                                        <div class="sub-heading">
                                            <h5>Approval Status</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr.No.
                                                    </th>
                                                    <th>Authority Name
                                                    </th>
                                                    <th>User Name
                                                    </th>
                                                    <th>Status
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                            </tbody>

                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("sno")%>                                                                              
                                        </td>
                                        <td>
                                            <%# Eval("PANAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("PAusername")%>
                                        </td>
                                        <td>
                                            <%# Eval("STATUS")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>

                            </asp:ListView>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leaveapp"
                                CssClass="btn btn-primary" OnClick="btnSave_Click" />

                            <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-primary"
                                OnClick="btnBack_Click" />
                            <asp:Button ID="btnreport" runat="server" Text="Report" CausesValidation="false"
                                CssClass="btn btn-info" ToolTip="Print Related Report" OnClick="btnreport_Click" Visible="False" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                        </div>

                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>

    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolKit:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server"
        BackgroundCssClass="modalBackground" RepositionMode="RepositionOnWindowScroll"
        TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="div" CancelControlID="btnNoDel">
    </ajaxToolKit:ModalPopupExtender>
    <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />

    <asp:Panel ID="div" runat="server" Style="display: block" CssClass="modalPopup">
        <div class="text-center">
            <div class="modal-content">
                <div class="modal-body">
                    <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                    <td>&nbsp;&nbsp;Are you sure to final Approve? This will Approve Requisition & Stop further Approval.</td>
                    <div class="text-center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" OnClick="btnOkDel_Click" TabIndex="180" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" TabIndex="181" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

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

    <%--END MODAL POPUP EXTENDER FOR DELETE CONFIRMATION --%>

    <%--  <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <%--  <script src="../../../JAVASCRIPTS/jquery.min_1.js" language="javascript" type="text/javascript"></script>
    <script src="../../../JAVASCRIPTS/jquery-ui.min_1.js" language="javascript" type="text/javascript"></script>
    <script src="../../../JAVASCRIPTS/AutoComplete.js" language="javascript" type="text/javascript"></script>
    <script src="../../../jquery/jquery-1.10.2.js" type="text/javascript"></script>--%>

    <script type="text/javascript">
        function showpopup() {
            var r = confirm("Are you sure to delete this item");
            if (r == true) {
                return true;
            } else {
                return false;
            }
        }

        $("#<%=btnClubReq.ClientID %>").click(
        function () {
            var lenghthReq = $("#<%=dlTestCases.ClientID %> td").length;
            var countSelReq = 0;
            var reqnos = "0";
            for (i = 0; i < lenghthReq; i++) {
                var cbchecked = '#ctl00_ContentPlaceHolder1_dlTestCases_ctl0' + i + '_chkReq';
                if ($(cbchecked).is(':checked')) {
                    countSelReq = countSelReq + 1;
                    if (reqnos == "0")
                        reqnos = $(cbchecked).parent().attr("title");
                    else
                        reqnos = reqnos + "," + $(cbchecked).parent().attr("title");
                }

            }
            if (countSelReq > 0) {
                $("#<%=ReqNOS.ClientID %>").val(reqnos);

            }
            else {
                alert('Please select at least one requisition');
                return false;

            }
        });

        $("#<%=btnClear.ClientID %>").click(
        function () {
            var lenghthReq = $("#<%=dlTestCases.ClientID %> td").length;
            var countSelReq = 0;
            var reqnos = "0";
            for (i = 0; i < lenghthReq; i++) {
                var cbchecked = '#ctl00_ContentPlaceHolder1_dlTestCases_ctl0' + i + '_chkReq';
                $(cbchecked).prop("checked", false);
            }
            return false;
        });

        $("#<%=chkClubRequisition.ClientID %>").click(
        function () {
            var valDept = $("#<%=ddlDept.ClientID %> option:selected").val();
            if (valDept == 0) {
                alert("Please select department");
                return false;
            }
            else {
                return true;
            }
        });
    </script>
    <script type="text/javascript" language="javascript">

        function CalApproximatCost(crl) {
            ctl00_ContentPlaceHolder1_lvitemReq_ctrl0_txtqty
            var st = crl.id.split("ctl00_ContentPlaceHolder1_lvitemReq_ctrl");
            //vishwas
            var i = st[1].split("_txtqty");
            // alert(i);
            var index = i[0];
            //calculate Bal Qty             
            var ApproveQty = document.getElementById('ctl00_ContentPlaceHolder1_lvitemReq_ctrl' + index + '_txtqty').value;
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_lvitemReq_ctrl' + index + '_hdnRate').value;
            // alert('ApproveQty=' + ApproveQty);
            //alert('rate' + rate);
            var lblCost = (Number(rate).toFixed(2) * ApproveQty);
            //alert(lblCost);
            document.getElementById('ctl00_ContentPlaceHolder1_lvitemReq_ctrl' + index + '_lblCost').innerHTML = lblCost.toFixed(2);
            // document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxableAmt').value = totamount.toFixed(2);// 0; ctl00_ContentPlaceHolder1_lvitemReq_ctrl0_lblCost
            var TotalCost = 0;

            var ROWS = Number(document.getElementById('<%=hdnrowcount.ClientID%>').value);
            // alert('ROWS=' + ROWS);
            var i = 0;
            for (i = 0; i < ROWS; i++) {
                //  alert('IN')
                // TotalCost = TotalCost + Number(document.getElementById('ctl00_ContentPlaceHolder1_lvitemReq_ctrl' + i + '_lblCost').value == '' ? 0 : document.getElementById('ctl00_ContentPlaceHolder1_lvitemReq_ctrl' + i + '_lblCost').value);
                TotalCost += Number(document.getElementById('ctl00_ContentPlaceHolder1_lvitemReq_ctrl' + i + '_lblCost').innerHTML);

                // alert('TotalCost=' + TotalCost);
            }
            document.getElementById('<%= lblTotAppCost.ClientID %>').innerHTML = TotalCost.toFixed(2);

        }
    </script>

</asp:Content>
