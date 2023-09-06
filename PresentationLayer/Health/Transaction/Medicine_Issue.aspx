<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Medicine_Issue.aspx.cs" Inherits="Health_Transaction_Medicine_Issue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <script type="text/javascript">
        ; debugger
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>--%>
    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">MEDICINE ISSUE ENTRY</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlSearch" runat="server">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Medicine Issue Entry</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Search Criteria</label>
                                            </div>
                                            <asp:RadioButtonList ID="rdlSerch" runat="server" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="rdlSerch_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0" Selected="True"> Patient No </asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="SingleParagraph" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="StockItem" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Enter Patient No. </label>
                                            </div>
                                            <asp:TextBox ID="txtPatientNo" runat="server" MaxLength="50" Enabled="true" TabIndex="1"
                                                CssClass="form-control" ToolTip="Enter Search Value"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPatientNo" runat="server" ControlToValidate="txtPatientNo"
                                                Display="None" SetFocusOnError="True" ErrorMessage="Patient Number is must to show details."
                                                ValidationGroup="StockItem"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:Button ID="btnSerch" runat="server" Text="Click To Search" TabIndex="2" ValidationGroup="StockItem" CssClass="btn btn-outline-primary" OnClick="btnSerch_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="SingleParagraph" ShowMessageBox="true" ShowSummary="false" ValidationGroup="StockItem" />

                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlPatientDetails" runat="server">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Patient Details</h5>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Patient No. :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblopdNO" runat="server" /></span>
                                                <asp:Label ID="lblPatientNO" runat="server" Visible="false"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Patient Name /Sex/ Age :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblPatientName" runat="server" />

                                                        <asp:Label ID="lblPatientSex" runat="server" />

                                                        <asp:Label ID="lblPatientAge" runat="server" /></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Patient Complaints :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblPatientCompl" runat="server" />
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>OPD Date :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblTreatmentDate" runat="server" /></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Doctor Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblDocName" runat="server" />
                                                        <asp:Label ID="lblDocNo" runat="server" Visible="false" />
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>


                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnsave" runat="server" Text="Issue & Save" TabIndex="2" ValidationGroup="StockItem"
                                    Visible="false" OnClick="btnsave_Click" CssClass="btn btn-outline-primary" ToolTip="Click here to Issue And Save" />
                                <asp:Button ID="btnclear" runat="server" Text="Cancel" TabIndex="2" ValidationGroup="StockItem"
                                    Visible="false" OnClick="btnclear_Click" CssClass="btn btn-outline-danger" ToolTip="Click here to Reset" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlStudGrid" runat="server" Visible="false" ScrollBars="Auto">
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>Medicine Details</h5>
                                        </div>
                                        <asp:GridView ID="lvMedicineissue" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-striped table-bordered table-hover" HeaderStyle-BackColor="ActiveBorder" ShowFooter="true"
                                            OnRowEditing="EditCustomer"
                                            OnRowCancelingEdit="CancelEdit">
                                            <%--OnRowUpdating="UpdateCustomer"--%>
                                            <HeaderStyle CssClass="bg-light-blue" />
                                            <%--<AlternatingRowStyle BackColor="#FFFFD2" />--%>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No" ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Medicine Name" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>

                                                        <asp:TextBox ID="txtItemname" runat="server" Text='<%# Eval("ITEMNAME")%>' OnTextChanged="txtItemname_TextChanged"
                                                            AutoPostBack="true" CssClass="form-control" ToolTip="Medicine Name" />
                                                        <asp:HiddenField ID="hfItemName" runat="server" />



                                                        <ajaxToolKit:AutoCompleteExtender ID="autAgainstAc" runat="server" TargetControlID="txtItemname"
                                                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                            ServiceMethod="GetItemName" OnClientShowing="clientShowing" OnClientItemSelected="ItemName">
                                                        </ajaxToolKit:AutoCompleteExtender>

                                                        <asp:Label ID="lblino" runat="server" Visible="false" Text='<%# Eval("INO")%>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>

                                                        <asp:TextBox ID="txtQtyMain" runat="server" CssClass="form-control" Text='<%# Eval("QTY")%>'
                                                            onkeypress="return CheckNumeric(event, this);" MaxLength="5" ToolTip="Quantity" />
                                                        <asp:Label ID="lblqty" runat="server" Visible="false" Text='<%# Eval("QTY")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtItemQty" runat="server" Text='<%# Eval("QTY")%>'></asp:TextBox>
                                                    </EditItemTemplate>

                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Doses" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%"
                                                    HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>

                                                        <asp:TextBox ID="txtDoses" runat="server" Text='<%# Eval("DNAME")%>' CssClass="form-control"
                                                            OnTextChanged="txtDoses_TextChanged" AutoPostBack="true" ToolTip="Doses"></asp:TextBox>
                                                        <asp:HiddenField ID="hfDoses" runat="server" />



                                                        <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtDoses"
                                                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                            ServiceMethod="GetDoseName" OnClientShowing="clientShowing" OnClientItemSelected="DoseName">
                                                        </ajaxToolKit:AutoCompleteExtender>


                                                        <asp:Label ID="lblDno" runat="server" Visible="false" Text='<%# Eval("DOSES")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtItemDoses" runat="server" Text='<%# Eval("DNAME")%>' onblur="OneTextToother();"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issue" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkIssue" runat="server"
                                                            CommandArgument='<%# Eval("ITEM_NO") %>'
                                                            ToolTip="Click To Issue" Checked='<%# (Eval("ISSUE_STATUS").ToString() == "1" ? true : false) %>' />

                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="chkIssue1" runat="server"
                                                            CommandArgument='<%# Eval("ITEM_NO") %>'
                                                            ToolTip="Click To Issue" />
                                                    </EditItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issue Quantity" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtIssueQty" runat="server" CssClass="form-control" ToolTip="Enter Issue Quantity for patient"
                                                            Text='<%# Eval("QTY_ISSUE")%>'
                                                            onkeypress="return CheckNumeric(event, this);" MaxLength="5" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtItemIssueQty" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Available Quantity" ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAvailQty" runat="server" CssClass="form-control" Text='<%# Eval("ITEM_MAX_QTY")%>' Enabled="false" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtItemAvailQty" runat="server" CssClass="form-control" Text='<%# Eval("ITEM_MAX_QTY")%>'
                                                            Enabled="false"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="Add" OnClick="AddNewCustomer" />
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField FooterStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkRemove" runat="server" CommandArgument='<%# Eval("INO")%>'
                                                            Text="Delete" OnClientClick="return confirm('Do you want to delete?')"
                                                            OnClick="DeleteCustomer" ForeColor="Red"></asp:LinkButton>
                                                        <%--<asp:ImageButton ID="lnkRemove" AlternateText="Delete Record" CommandArgument='<%# Eval("INO") %>'
                                                                                ToolTip="Delete Record" runat="server" ImageUrl="~/images/delete.gif" OnClientClick="return confirm('Do you want to delete?')" OnClick="DeleteCustomer" />&nbsp;--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#CCCCCC" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lvMedicineissue" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">

        ; debugger
        function ItemName(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            var Name = idno[0].split("-");
            ////document.getElementById('ctl00_ContentPlaceHolder1_lvMedicineissue_ctl' + index + '_hfItemName').value = Name[0];
            ////document.getElementById('ctl00_ContentPlaceHolder1_lvMedicineissue_ctl' + index + '_lblino').value = Name[0];
        }

        function DoseName(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            var Name = idno[0].split("-");
            //document.getElementById('ctl00_ContentPlaceHolder1_lvMedicineissue_ctl03_txtDoses').value = idno[1];
            //document.getElementById('ctl00_ContentPlaceHolder1_lvMedicineissue_ctl03_hfDoses').value = Name[0];
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        function EnableTextBox() {
            try {
                var itemCount = 0;
                var tb;
                var cb;
                var check = document.getElementById('<%=lvMedicineissue.ClientID %>')

                for (i = 2; i <= check.rows.length - 1 ; i++) {
                    cb = document.getElementById('ctl00_ContentPlaceHolder1_lvMedicineissue_ctl0' + i + '_chkIssue');
                    tb = document.getElementById('ctl00_ContentPlaceHolder1_lvMedicineissue_ctl0' + i + '_txtIssueQty')

                    if (cb.Checked == true) {
                        tb.disabled = true;
                        //break;
                    }
                    else {
                        tb.disabled = false;
                        //break;
                    }
                }
            }
            catch (ex) {
                alert(ex.message);
            }

        }
        function bestofrule() {
            var GVMaintainReceiptMaster = document.getElementById('<%= lvMedicineissue.ClientID %>');
            for (var rowId = 1; rowId < GVMaintainReceiptMaster.rows.length; rowId++) {
                var txtnew = (GVMaintainReceiptMaster.rows[rowId].cells[1].children[0]).value;
                var txtold = (GVMaintainReceiptMaster.rows[rowId].cells[2].children[0]).innerHTML;
                //alert(txtnew.value + ' ' + txtold.innerHTML);
                if (Number(txtnew) > Number(txtold)) {
                    //alert(Number(txtnew));
                    (GVMaintainReceiptMaster.rows[rowId].cells[3].children[0]).value = txtnew;
                    (GVMaintainReceiptMaster.rows[rowId].cells[3].children[1]).value = txtnew;
                    //alert((GVMaintainReceiptMaster.rows[rowId].cells[3].children[1]).value);
                }
                else {
                    (GVMaintainReceiptMaster.rows[rowId].cells[3].children[0]).value = txtold;
                    (GVMaintainReceiptMaster.rows[rowId].cells[3].children[1]).value = txtold;
                    //alert((GVMaintainReceiptMaster.rows[rowId].cells[3].children[1]).value);
                    //alert(Number(txtold));
                }
            }
            return false;
        }

        function OneTextToother() {
            try {
                var GVMaintainReceiptMaster = document.getElementById('<%= lvMedicineissue.ClientID %>');
                for (var rowId = 1; rowId <= GVMaintainReceiptMaster.rows.length - 1; rowId++) {
                    var txtnew = (GVMaintainReceiptMaster.rows[rowId].cells[5].children[0]).value;
                    var txtavalqty = (GVMaintainReceiptMaster.rows[rowId].cells[6].children[0]).value;
                    var txtbalqty = (GVMaintainReceiptMaster.rows[rowId].cells[7].children[0]).value;
                    var txtgivenqty = (GVMaintainReceiptMaster.rows[rowId].cells[2].children[0]).innerHTML;
                    if (Number(txtnew) > Number(txtavalqty)) {
                        alert('Allot quantity is not greater than available quantity');
                        (GVMaintainReceiptMaster.rows[rowId].cells[5].children[0]).value = null;
                    }
                }
            }
            catch (ex) {

            }
        }
    </script>


    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                    </td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
                    </td>
                </tr>
            </table>
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
</asp:Content>

