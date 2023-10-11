<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StrUserReq.aspx.cs" Inherits="STORES_Transactions_Quotation_StrUserReq" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../../JAVASCRIPTS/jquery.min_1.js" language="javascript" type="text/javascript"></script>
    <script src="../../../JAVASCRIPTS/jquery-ui.min_1.js" language="javascript" type="text/javascript"></script>
    <script src="../../../JAVASCRIPTS/AutoComplete.js" language="javascript" type="text/javascript"></script>--%>

    <%--  <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">DEPARTMENT USER REQUISITION</h3>
                </div>


                <div class="box-body">
                    <asp:Panel ID="PnlReport" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-12">
                                    <div class="sub-heading">
                                        <h5>Select Requisition Number</h5>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-md-12 col-12">
                                    <asp:Label ID="lblmsg" runat="server" SkinID="Msglbl"></asp:Label>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Requisition Slip No.</label>
                                    </div>
                                    <asp:DropDownList ID="ddlReportIndentSlipNo" TabIndex="1" runat="server" CssClass="form-control" data-select2-enable="true"
                                        AppendDataBoundItems="true" ValidationGroup="report" AutoPostBack="False">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvReportIndent" runat="server" ControlToValidate="ddlReportIndentSlipNo"
                                        ErrorMessage="Please Select Requisition Slip No." Display="None" ValidationGroup="report"
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnReport" Text="Report" runat="server" TabIndex="2" ToolTip="Click To Show Report" CssClass="btn btn-info" ValidationGroup="report"
                                        OnClick="btnReport_Click" />
                                    <asp:Button ID="btnSpecificationDoc" Text="Show Spec.Doc" TabIndex="2" ToolTip="Click To Show Spec.Doc" CssClass="btn btn-info" runat="server" ValidationGroup="StoreReq"
                                        OnClick="btnSpecificationDoc_Click" Visible="false" />
                                    <asp:Button ID="btnBack" Text="Back" runat="server" TabIndex="2" ToolTip="Click To Go Back" CssClass="btn btn-info" OnClick="btnBack_Click" />
                                    <asp:ValidationSummary ID="vsReport" runat="server" ValidationGroup="report" ShowMessageBox="true" ShowSummary="false" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlReqFor" runat="server">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Select Requisition For</label>
                                    </div>
                                    <asp:RadioButtonList ID="rdbReqFor" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbReqFor_SelectedIndexChanged">
                                        <asp:ListItem Value="P">Purchase&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="I">Issue</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="butReport" Text="Show Report" runat="server" TabIndex="20" CssClass="btn btn-info" OnClick="butReport_Click" />
                                <asp:Button ID="btnTrackReq" runat="server" Visible="false" Text="Track My Requisition" CssClass="btn btn-primary pull-right" OnClick="btnTrackReq_Click" />

                            </div>
                        </div>

                    </asp:Panel>
                    <asp:Panel ID="PnlIndentDetails" runat="server" Enabled="True" Visible="false">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Enter Requisition Number</h5>
                                    </div>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Requisition Slip No.</label>
                                    </div>
                                    <asp:TextBox ID="txtIndentSlipNo" runat="server" Style="z-index: 0" CssClass="form-control" TabIndex="1" ToolTip="Enter Requisition Slip No"
                                        MaxLength="50" Enabled="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtIndentSlipNo" runat="server" ControlToValidate="txtIndentSlipNo"
                                        ErrorMessage="Please Enter Requisition Slip No." Display="None" ValidationGroup="StoreReq"></asp:RequiredFieldValidator>

                                    <asp:DropDownList ID="ddlIndentSlipNo" runat="server" TabIndex="1" CssClass="form-control" ToolTip="Select Requisition Slip No"
                                        AppendDataBoundItems="true" OnSelectedIndexChanged="ddlIndentSlipNo_SelectedIndexChanged"
                                        AutoPostBack="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlIndSlipNo" runat="server" ControlToValidate="ddlIndentSlipNo"
                                        ErrorMessage="Please Select Requisition Slip No." InitialValue="0" Display="None" ValidationGroup="StoreReq"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Person Name </label>
                                    </div>
                                    <asp:TextBox ID="txtPersonName" runat="server" TabIndex="5" CssClass="form-control" MaxLength="90" Enabled="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtPersonName" runat="server" ControlToValidate="txtPersonName"
                                        ErrorMessage="Please Enter Person Name" Display="None" ValidationGroup="StoreReq"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Department</label>
                                    </div>
                                    <asp:DropDownList runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AppendDataBoundItems="true"
                                        ID="ddlDepartment" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlDepartment" runat="server" ControlToValidate="ddlDepartment"
                                        ErrorMessage="Please Select Department" Display="None" ValidationGroup="StoreItem"
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Requisition Slip Date </label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="ImaCalStartDate">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtIndentSlipDate" runat="server" ToolTip="Enter Requisition Slip Date" CssClass="form-control" TabIndex="3"></asp:TextBox>

                                        <ajaxToolKit:CalendarExtender ID="cetxtIndentSlipDate" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtIndentSlipDate">
                                        </ajaxToolKit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfvtxtIndentSlipDate" runat="server" ControlToValidate="txtIndentSlipDate"
                                            Display="None" ErrorMessage="Please Select Requisition Slip Date in (dd/MM/yyyy Format)"
                                            SetFocusOnError="True" ValidationGroup="StoreItem">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:MaskedEditExtender ID="meIndDate" runat="server" TargetControlID="txtIndentSlipDate"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="mevIndDate" runat="server" ControlExtender="meIndDate"
                                            ControlToValidate="txtIndentSlipDate" EmptyValueMessage="Please Indent Slip Date"
                                            InvalidValueMessage="Requisition Slip Date is Invalid (Enter dd/MM/yyyy Format)"
                                            Display="None" TooltipMessage="Please Enter Indent Slip Date" EmptyValueBlurredText="Empty"
                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="StoreReq" SetFocusOnError="True" />
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divBudgetHead" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Budget Head </label>
                                    </div>
                                    <asp:DropDownList runat="server" TabIndex="4" CssClass="form-control" data-select2-enable="true" ID="ddlBudgetHead" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlBudgetHead_SelectedIndexChanged">
                                    </asp:DropDownList>

                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="divInstitute" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label></label>
                                    </div>
                                    <asp:RadioButton runat="server" TabIndex="6" ID="radInstitute" Text="Institute" GroupName="RadReq" Checked="true" />
                                    <asp:RadioButton runat="server" TabIndex="7" ID="radTEQIP" Text="TEQIP" GroupName="RadReq" Visible="false" />

                                </div>

                                <div class="col-lg-3 col-md-6 col-12" id="divShowAmt" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        
                                    </div>

                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Budget Balance Amount :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblBudgetBalAmt" runat="server" Text=""></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-lg-3 col-md-6 col-12" id="divInprocessAmt" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        
                                    </div>


                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Inprocess Requisition Amount :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblInprocessAmt" runat="server" Text=""></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>


                                <div class="col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                       
                                    </div>
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Passing Path:</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblPassingPath" Font-Bold="true" runat="server"></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>

                            </div>
                        </div>

                    </asp:Panel>
                    <asp:Panel ID="pnlItemDetails" runat="server" Visible="false">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <div class="sub-heading">
                                        <h5>Enter Item Details</h5>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Item Name </label>
                                    </div>
                                    <asp:DropDownList ID="ddlItemName" CssClass="form-control" data-select2-enable="true" TabIndex="9" ToolTip="Select Item Name" AppendDataBoundItems="true"
                                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlItemName" runat="server" ControlToValidate="ddlItemName"
                                        ErrorMessage="Please Select Item Name" Display="None" ValidationGroup="StoreItem"
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <%--//-------------------------------23/01/2023----------------------------------//--%>

                                 <div class="form-group col-lg-3 col-md-6 col-12" id="divAvailableQty" visible="false" runat="server" >
                                    <div class="label-dynamic">
                                      
                                        <label>Available Qty </label>
                                    </div>
                                    <asp:TextBox ID="txtAvailableQty" runat="server" TabIndex="10" enabled="false" ToolTip="Available Qty" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRequiredQty"
                                        ErrorMessage="Please Enter Required Qty" Display="None" ValidationGroup="StoreItem"></asp:RequiredFieldValidator>--%>
                                   <%-- <asp:CompareValidator ID="CompareValidator1" runat="server" Display="None" ErrorMessage="Enter Only Numeric Value for Quantity"
                                        ControlToValidate="txtRequiredQty" Type="Double" Operator="DataTypeCheck" ValidationGroup="StoreItem"></asp:CompareValidator>--%>
                                    <%-- <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtRequiredQty"
                                        ValidChars="0123456789" FilterMode="ValidChars">
                                    </ajaxToolKit:FilteredTextBoxExtender>--%>
                                   
                                </div>

                               <%-- //-----------------end added available qty-------------------------------------//--%>












                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Required Qty </label>
                                    </div>
                                    <asp:TextBox ID="txtRequiredQty" runat="server" TabIndex="10" ToolTip="Enter Required Qty" CssClass="form-control" MaxLength="4"
                                        ValidationGroup="StoreItem"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtRequiredQty" runat="server" ControlToValidate="txtRequiredQty"
                                        ErrorMessage="Please Enter Required Qty" Display="None" ValidationGroup="StoreItem"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cmptxtRequiredQty" runat="server" Display="None" ErrorMessage="Enter Only Numeric Value for Quantity"
                                        ControlToValidate="txtRequiredQty" Type="Double" Operator="DataTypeCheck" ValidationGroup="StoreItem"></asp:CompareValidator>
                                     <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtRequiredQty"
                                        ValidChars="0123456789" FilterMode="ValidChars">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                    <span>
                                        <asp:Label ID="lbtUnit" runat="server"></asp:Label></span>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divAppRate" runat="server">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Unit Price</label>
                                    </div>
                                    <asp:TextBox ID="txtRate" runat="server" TabIndex="11" ToolTip="Enter Unit Price" CssClass="form-control" MaxLength="10"
                                        ValidationGroup="StoreItem"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvRate" runat="server" ControlToValidate="txtRate"
                                        ErrorMessage="Please Enter Unit Price" Display="None" ValidationGroup="StoreItem"></asp:RequiredFieldValidator>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeCatagoryShort" runat="server" TargetControlID="txtRate"
                                        ValidChars="0123456789." FilterMode="ValidChars">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                    <asp:RangeValidator ID="rngUnit" runat="server" ControlToValidate="txtRate" ErrorMessage="Unit Price Should Be Greater Than Zero" MinimumValue="1" MaximumValue="99999999999" ValidationGroup="StoreItem" Display="None"></asp:RangeValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Attach Tech. Specification</label>
                                    </div>
                                    <asp:FileUpload ID="FileUpload1" runat="server" TabIndex="12" />
                                    <asp:Label ID="lblFileName" runat="server" Text="" Visible="false"></asp:Label>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divTecSpe" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Technical Specification</label>
                                    </div>
                                    <asp:TextBox ID="txtTechSpe" TabIndex="13" runat="server" Enabled="false" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divPurJustification" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Purchase Justification</label>
                                    </div>
                                    <asp:TextBox ID="txtRemark" TabIndex="13" runat="server" CssClass="form-control" TextMode="MultiLine"
                                        onkeyDown="checkTextAreaMaxLength(this,event,'150');" onkeyup="textCounter(this, this.form.remLen, 150);"></asp:TextBox>
                                    <asp:ValidationSummary ID="ValidsummaryItem" runat="server" ValidationGroup="StoreItem"
                                        ShowMessageBox="true" ShowSummary="false" />
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="divItemSpecification" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Item Specification </label>
                                    </div>
                                    <asp:TextBox ID="txtItemSpecification" TabIndex="13" runat="server" CssClass="form-control" TextMode="MultiLine" onkeyDown="checkTextAreaMaxLength(this,event,'150');" onkeyup="textCounter(this, this.form.remLen, 150);"></asp:TextBox>
                                </div>

                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="butSubmit" Text="Add Item" TabIndex="14" ToolTip="Click To Submit" runat="server" CssClass="btn btn-primary" ValidationGroup="StoreItem"
                                    OnClick="butAddItem_Click" />
                                <asp:Button ID="butCancel" CssClass="btn btn-warning" TabIndex="15" ToolTip="Click To Reset" Text="Cancel" runat="server" OnClick="butCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="StoreItem" ShowMessageBox="true" ShowSummary="false" />
                            </div>
                        </div>

                        <div class="col-12 table-responsive">
                            <asp:ListView ID="lvItemDetails" runat="server">
                                <EmptyDataTemplate>
                                    <center>
                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" ></asp:Label>
                                                        </center>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div id="demo-grid" class="vista-grid">
                                        <div class="sub-heading">
                                            <h5>Item Details</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                    <th>Item Name
                                                    </th>
                                                    <th>Quantity
                                                    </th>
                                                    <th>Unit Price
                                                    </th>
                                                    <th>Total
                                                    </th>

                                                    <th>Download
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
                                            <asp:ImageButton ID="btnEditItem" runat="server" OnClick="btnEdit_Click" CommandArgument='<%# Eval("REQ_TNO") %>'
                                                ImageUrl="~/Images/edit.png" ToolTip="Edit Record" AlternateText="Edit Record" />
                                            <asp:ImageButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CommandArgument='<%# Eval("REQ_TNO") %>'
                                                ImageUrl="~/Images/delete.png" ToolTip="Delete Record" AlternateText="Delete Record"
                                                OnClientClick="return confirm('Are You Sure You Want To Delete This Record?');" />

                                        </td>
                                        <td>
                                            <%# Eval("ITEM_NAME")%>
                                           
                                        </td>
                                        <td>
                                            <%# Eval("REQ_QTY")%>
                                        </td>
                                        <%--<td>
                                                                            <%# Eval("ITEM_UNIT")%>
                                                                        </td>--%>
                                        <td>
                                            <%# Eval("RATE")%>
                                        </td>
                                        <td>
                                            <%# Eval("TOTAL")%>
                                        </td>
                                        <%--<td>
                                                                            <%# Eval("ITEM_SPECIFICATION")%>
                                                                        </td>--%>
                                        <td>
                                           <%-- <asp:ImageButton ID="imgFile" runat="Server" ImageUrl="~/Images/action_down.png" CommandArgument='<%# Eval("FILEPATH")%>'
                                                AlternateText='<%# Eval("FILENAME")%>' CommandName='<%# Eval("ITEM_NAME")%>' OnClick="imgdownload_Click" />--%>

                                              <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgdownload_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("FILENAME") %>'
                                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("FILENAME") %>' Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>'></asp:ImageButton>

                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>

                          <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                        <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                        <asp:HiddenField ID="hdnBlobCon" runat="server" />
                        <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                        <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                    </div>



                        <div class="col-12">

                            <div class="row">
                                <div class="form-group col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Purchase Justification </label>
                                    </div>
                                    <asp:TextBox ID="txtIndentRemark" runat="server" TabIndex="8" CssClass="form-control"
                                        TextMode="MultiLine"></asp:TextBox>
                                </div>

                            </div>
                        </div>


                        <div class="col-12 btn-footer">
                            <asp:Button ID="butSubmitReq" Text="Submit" runat="server" TabIndex="16" CssClass="btn btn-primary" OnClick="butSubmitReq_Click"
                                ValidationGroup="StoreReq" />
                            <asp:Button ID="butAddNew" Text="AddNew" runat="server" TabIndex="17" Visible="false" CssClass="btn btn-primary" OnClick="butAddNew_Click" />
                            <asp:Button ID="butModify" Text="Modify" runat="server" TabIndex="18" CssClass="btn btn-primary" OnClick="butModify_Click" />
                            <asp:Button ID="butCancel1" Text="Cancel" runat="server" TabIndex="19" CssClass="btn btn-warning" OnClick="butCancel1_Click" />

                            <asp:ValidationSummary ID="validSummaryReq" runat="server" ValidationGroup="StoreReq" ShowMessageBox="true" ShowSummary="false" />
                        </div>
                    </asp:Panel>
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
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup" BackColor="#999999" BorderStyle="Solid">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
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
    <asp:UpdatePanel ID="updPanel" runat="server">
    </asp:UpdatePanel>
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

        // function for numeric textbox
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }
    </script>

    <script type="text/javascript">
        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event) {//IE
                        e.returnValue = false;
                    }
                    else {//Firefox
                        e.preventDefault();
                    }
                }
            }
        }


        function textCounter(field, countfield, maxlimit) {
            if (field.value.length > maxlimit)
                field.value = field.value.substring(0, maxlimit);
            else
                countfield.value = maxlimit - field.value.length;
        }
    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

