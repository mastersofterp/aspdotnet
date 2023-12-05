<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OtherMiscelnew.aspx.cs" Inherits="ESTABLISHMENT_ServiceBook_OtherMiscelnew" MasterPageFile="~/ServiceBookMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div id="div2" runat="server"></div>
            <form role="form">
                <div class="box-body">
                    <div class="col-md-12">

                        <div class="panel panel-info">
                            <%--<div class="panel-heading">Miscellaneous Information </div>--%>
                            <div class="panel-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Miscellaneous Information</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12 form-group">
                                    <div class="row">
                                        <div class="form-group col-md-3">
                                            <label>No. of PhD student under your Guidance :</label>
                                            <asp:TextBox ID="txtunderGuidence" runat="server" CssClass="form-control" onkeypress="return CheckNumeric(event,this);" ToolTip="Enter Ph.D Under Guidance"
                                                MaxLength="50" TabIndex="11"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>No. of student PhD awarded under you :</label>
                                            <asp:TextBox ID="txtAwarded" runat="server" CssClass="form-control" onkeypress="return CheckNumeric(event,this);" ToolTip="Enter Ph.D Awarded"
                                                MaxLength="40" TabIndex="12"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Indexing Information</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="pnl" runat="server">
                                    <asp:Label ID="lblMsg" runat="server" SkinID="lblmsg"></asp:Label>
                                    <div class="form-group col-md-12">

                                        <asp:UpdatePanel ID="upKannada" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="panel panel-info">
                                                    <%--<div class="panel-heading">--%>
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <%--  <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Miscellaneous Detail</h5>
                                                </div>
                                            </div>--%>
                                                        </div>
                                                    </div>
                                                    <%-- Miscellaneous Detail--%>
                                                    <%-- <div class="box-tools pull-right">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/collapse_blue.jpg" alt=""
                                                            onclick="javascript:toggleExpansion(this,'divGeneralInfo')" />
                                                    </div>--%>
                                                    <%--  </div>--%>
                                                    <div id="divGeneralInfo" style="display: block;">
                                                        <div class="panel-body">
                                                            <asp:Label ID="Label1" runat="server" SkinID="Msglbl"></asp:Label>

                                                            <div class="form-group col-md-12">
                                                                <div class="row">

                                                                    <div class="form-group col-md-3">
                                                                        <label>Indexing Factors :<span style="color: Red">*</span></label>
                                                                        <asp:DropDownList ID="ddlIndexFac" runat="server" AppendDataBoundItems="true" TabIndex="1" ToolTip="Select Indexing Factors"
                                                                            CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="H-Index" Value="H-Index"></asp:ListItem>
                                                                            <asp:ListItem Text="i10-index" Value="i10-index"></asp:ListItem>
                                                                            <asp:ListItem Text="Citations" Value="Citations"></asp:ListItem>
                                                                        </asp:DropDownList>

                                                                        <asp:TextBox ID="txtHindex" runat="server" CssClass="form-control" TabIndex="2" Visible="false" />

                                                                        <asp:RequiredFieldValidator ID="rfvIndexFactor" runat="server" ControlToValidate="ddlIndexFac"
                                                                            Display="None" ErrorMessage="Please Enter Indexing Factors" SetFocusOnError="true"
                                                                            ValidationGroup="MisceDetail" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div class="form-group col-md-3">
                                                                        <label>Indexing Factor Value:<span style="color: Red">*</span></label>
                                                                        <asp:TextBox ID="txtindexVal" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Enter Indexing Factor Value"
                                                                            MaxLength="50" />

                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtindexVal"
                                                                            Display="None" ErrorMessage="Please Enter Indexing Factors Value" SetFocusOnError="true"
                                                                            ValidationGroup="MisceDetail"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div class="form-group col-md-3">
                                                                        <label>Indexing Date :<span style="color: Red">*</span></label>
                                                                        <div class="input-group date">
                                                                            <div class="input-group-addon">
                                                                                <asp:Image ID="imgFac" runat="server" class="fa fa-calendar text-blue" />
                                                                            </div>
                                                                            <asp:TextBox ID="txtIndexDt" runat="server" CssClass="form-control" TabIndex="4" />
                                                                            <ajaxToolKit:CalendarExtender ID="CeIndexFac" runat="server" Format="dd/MM/yyyy"
                                                                                TargetControlID="txtIndexDt" PopupButtonID="imgFac" Enabled="true" EnableViewState="true"
                                                                                PopupPosition="BottomLeft">
                                                                            </ajaxToolKit:CalendarExtender>
                                                                            <ajaxToolKit:MaskedEditExtender ID="meeIndexDt" runat="server" TargetControlID="txtIndexDt"
                                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                                            </ajaxToolKit:MaskedEditExtender>
                                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeIndexDt"
                                                                                ControlToValidate="txtIndexDt" InvalidValueMessage="Date is Invalid (Enter dd/mm/yyyy Format)"
                                                                                Display="None" TooltipMessage="Please Enter Indexing Date" EmptyValueBlurredText="Empty"
                                                                                InvalidValueBlurredMessage="Invalid Indexing Date" ValidationGroup="MisceDetail" SetFocusOnError="True" IsValidEmpty="false"
                                                                                InitialValue="__/__/____" /><%--EmptyValueMessage="Please Enter From Date"--%>
                                                                        </div>

                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtindexVal"
                                                                            Display="None" ErrorMessage="Please Enter Indexing Date" SetFocusOnError="true"
                                                                            ValidationGroup="MisceDetail"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>

                                                            </div>

                                                            <div class="col-md-12 form-group">
                                                                <p class="text-center">
                                                                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" TabIndex="5" ToolTip="Click to add" ValidationGroup="MisceDetail"
                                                                        OnClick="btnAdd_Click" />
                                                                    <asp:ValidationSummary ID="valsumAuthor" runat="server" ValidationGroup="MisceDetail"
                                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                            </div>

                                                            <div class="col-md-12">
                                                                <div class="col-md-12">
                                                                    <asp:Panel ID="pnllIndexFact" runat="server" ScrollBars="Auto">
                                                                        <asp:ListView ID="lvIndexFact" runat="server">
                                                                            <EmptyDataTemplate>
                                                                                <p class="text-center text-bold">
                                                                                    <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" />
                                                                                </p>
                                                                            </EmptyDataTemplate>
                                                                            <LayoutTemplate>
                                                                                <div class="sub-heading">
                                                                                    <h5>Indexing Factor Detail</h5>
                                                                                </div>
                                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                                    <thead>
                                                                                        <tr class="bg-light-blue">

                                                                                            <th>Action</th>
                                                                                            <th>Sr.No.</th>
                                                                                            <th>Indexing Factor</th>
                                                                                            <th>Indexing Factor Value</th>
                                                                                            <th>Date</th>
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
                                                                                        <asp:ImageButton ID="btnEditTr" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%#Eval("SRNO")%>'
                                                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditTr_Click" />

                                                                                        <asp:ImageButton ID="btnIndexDelete" runat="server" CausesValidation="false"
                                                                                            CommandArgument='<%# Eval("SRNO")%>' ImageUrl="~/Images/delete.png" ToolTip="Delete Record"
                                                                                            OnClick="btnIndexDelete_Click" EnableViewState="true"
                                                                                            OnClientClick="javascript:return confirm('Are you sure you want to delete ?')" />

                                                                                    </td>
                                                                                    <td><%#Container.DataItemIndex+1 %></td>
                                                                                    <td>
                                                                                        <%# Eval("INDEXFACTOR")%>
                                                                                        <asp:HiddenField ID="hdnCpdaType" runat="server" Value='<%# Eval("INDEXFACTOR")%>' />
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("INDEXVALUE")%>
                                                                                        <asp:HiddenField ID="hdnMaxLimit" runat="server" Value='<%# Eval("INDEXVALUE")%>' />
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("DATE","{0:dd/MM/yyyy}")%>
                                                                                        <asp:HiddenField ID="hdnBalanceAmount" runat="server" Value='<%# Eval("DATE")%>' />
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="div1" runat="server" style="display: none">
                                                    <div class="panel panel-info">
                                                        <%--  <div class="panel-heading">--%>
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <div class="sub-heading">
                                                                        <h5>Bond  Details</h5>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%-- Bond  Details--%>
                                                        <%-- <div class="box-tools pull-right">
                                                            <img id="img6" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divStatusDetails')" />
                                                        </div>--%>
                                                        <%--   </div>--%>
                                                        <div id="divStatusDetails" style="display: block;">
                                                            <div class="panel-body">
                                                                <div class="form-group col-md-12">
                                                                    <div class="row">
                                                                        <div class="form-group col-md-3">
                                                                            <label>Bond :</label>
                                                                            <asp:TextBox ID="txtbond" runat="server" CssClass="form-control" TabIndex="6" ToolTip="Enter Bond" MaxLength="50"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-md-3">
                                                                            <label>From Date : <span style="color: #FF0000">*</span> </label>
                                                                            <div class="input-group date">
                                                                                <div class="input-group-addon">
                                                                                    <asp:Image ID="imgCal" runat="server" class="fa fa-calendar text-blue" />
                                                                                </div>
                                                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" onBlur="CalDuration();" onChange="CalDuration();" TabIndex="7"></asp:TextBox>
                                                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                                                    TargetControlID="txtFromDate" PopupButtonID="imgCal" Enabled="true" EnableViewState="true"
                                                                                    PopupPosition="BottomLeft">
                                                                                </ajaxToolKit:CalendarExtender>
                                                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                                                    Display="None" ErrorMessage="Please Select From Date"
                                                                                    ValidationGroup="BondDetail" SetFocusOnError="True">
                                                                                </asp:RequiredFieldValidator>
                                                                                <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                                                </ajaxToolKit:MaskedEditExtender>
                                                                                <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meFromDate"
                                                                                    ControlToValidate="txtFromDate" InvalidValueMessage="From Date is Invalid (Enter dd/mm/yyyy Format)"
                                                                                    Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="BondDetail" SetFocusOnError="True"
                                                                                    IsValidEmpty="false" InitialValue="__/__/____" /><%--EmptyValueMessage="Please Enter From Date"--%>
                                                                            </div>
                                                                        </div>


                                                                        <div class="form-group col-md-3">
                                                                            <label>To Date : <span style="color: #FF0000">*</span></label>
                                                                            <div class="input-group date">
                                                                                <div class="input-group-addon">
                                                                                    <asp:Image ID="Image1" runat="server" class="fa fa-calendar text-blue" />
                                                                                </div>
                                                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" onBlur="CalDuration();" onChange="CalDuration();" TabIndex="8"></asp:TextBox>
                                                                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                                                    PopupButtonID="Image1" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                                                </ajaxToolKit:CalendarExtender>
                                                                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                                                    Display="None" ErrorMessage="Please Select To Date" ValidationGroup="BondDetail"
                                                                                    SetFocusOnError="True">
                                                                                </asp:RequiredFieldValidator>
                                                                                <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                                                </ajaxToolKit:MaskedEditExtender>
                                                                                <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meToDate"
                                                                                    ControlToValidate="txtToDate" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                                    Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="BondDetail" SetFocusOnError="True" IsValidEmpty="false"
                                                                                    InitialValue="__/__/____" /><%--EmptyValueMessage="Please Enter To Date"--%>
                                                                                <%--<asp:CompareValidator ID="cvToDate" runat="server" Display="None"
                                        ErrorMessage="Traning To Date  Should be Greater than  or equal to From Date"
                                        ValidationGroup="ServiceBook" SetFocusOnError="True" ControlToCompare="txtFromDate"
                                        ControlToValidate="txtToDate" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>--%>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group col-md-3">
                                                                            <label>Document Attachment :</label>
                                                                            <asp:FileUpload ID="flupld" runat="server" ToolTip="Click here to Upload Document" TabIndex="9" />
                                                                            <asp:Label ID="Label2" runat="server" Text=" Please Select valid Document file(e.g. .pdf,.jpg) upto 5MB" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-md-12">

                                                            <div class="col-md-12 form-group">

                                                                <p class="text-center">
                                                                    <asp:Button ID="btnAddBond" runat="server" Text="Add" CssClass="btn btn-primary" TabIndex="10" ToolTip="Click to add" ValidationGroup="BondDetail"
                                                                        OnClick="btnAddBond_Click" />
                                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="BondDetail"
                                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                            </div>
                                                        </div>

                                                        <div class="col-md-12">
                                                            <div class="col-md-12">
                                                                <asp:Panel ID="pnlBond" runat="server" ScrollBars="Auto">
                                                                    <asp:ListView ID="lvBondList" runat="server">
                                                                        <EmptyDataTemplate>
                                                                            <p class="text-center text-bold">
                                                                                <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" />
                                                                            </p>
                                                                        </EmptyDataTemplate>
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Bond Detail</h5>
                                                                            </div>
                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                                <thead>
                                                                                    <tr class="bg-light-blue">
                                                                                        <th>Action</th>
                                                                                        <th>Sr.No.</th>

                                                                                        <th>Bond</th>
                                                                                        <th>From Date</th>

                                                                                        <th>To Date</th>

                                                                                        <th>File Name</th>
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
                                                                                    <asp:ImageButton ID="btnEditBond" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%#Eval("SBNO")%>'
                                                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditBond_Click" />

                                                                                    <asp:ImageButton ID="btnBondDelete" runat="server" CausesValidation="false"
                                                                                        CommandArgument='<%# Eval("SBNO")%>' ImageUrl="~/Images/delete.png" ToolTip='<%# Eval("FILENAME")%>'
                                                                                        OnClick="btnBondDelete_Click" EnableViewState="true" AlternateText='<%# Eval("FILEPATH")%>'
                                                                                        OnClientClick="javascript:return confirm('Are you sure you want to delete ?')" />

                                                                                </td>
                                                                                <td><%#Container.DataItemIndex+1 %></td>
                                                                                <td>
                                                                                    <%# Eval("BOND")%>
                                                                                           
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("FROMDATE","{0:dd/MM/yyyy}")%>                                                                                       
                                                                                </td>

                                                                                <td>
                                                                                    <%# Eval("TODATE","{0:dd/MM/yyyy}")%>
                                                                                            
                                                                                </td>

                                                                                <td>
                                                                                    <%-- <%# Eval("FILENAME")%>--%>

                                                                                    <%--<asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" 
                                                                                                NavigateUrl='<%# GetTempFileNamePath(Eval("FILENAME"),Eval("CP_ID"),Eval("CMAID"))%>'><%# Eval("FILENAME")%>
                                                                                            </asp:HyperLink>--%>
                                                                                    <%--OnClick="lnkDownload_Click"--%>


                                                                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("FILENAME"),Eval("FILEPATH"))%>'><%# Eval("FILENAME")%></asp:HyperLink>

                                                                                    <%-- <asp:LinkButton ID="lnkDownload" runat="server" CausesValidation="false"
                                                                                            CommandArgument='<%# Eval("FILEPATH")%>' AlternateText="Download File"
                                                                                            OnClick="lnkDownload_Click" ToolTip='<%# Eval("FILENAME")%>'> <%# Eval("FILENAME")%> </asp:LinkButton>--%>

                                                                                </td>


                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                                </div>
                                                </div>


                                                <div id="divServiceDetails" runat="server">
                                                    <div class="panel panel-info">
                                                        <%-- <div class="panel-heading">--%>
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <div class="sub-heading">
                                                                        <h5>Ph.D Details</h5>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%-- Ph.D Details--%>
                                                        <%--  <div class="box-tools pull-right">
                                                                <img id="img4" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divServiceTypeDetails')" />

                                                            </div>--%>
                                                        <%--  </div>--%>
                                                        <div id="divServiceTypeDetails" style="display: block;">
                                                            <div class="panel-body">
                                                                <div class="form-group col-md-12">
                                                                    <div class="row">
                                                                        <%--<div class="form-group col-md-6">
                                                                             <div class="form-group col-md-10">--%>
                                                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                                            <label>Ph.D Under Guidance :</label>
                                                                            <asp:TextBox ID="txtphdgui" runat="server" CssClass="form-control" onkeypress="return CheckAlphabet(event,this);" ToolTip="Enter Ph.D Under Guidance"
                                                                                MaxLength="50" TabIndex="11"></asp:TextBox>
                                                                            <%--<ajaxToolKit:FilteredTextBoxExtender ID="ftbephdguide" runat="server" TargetControlID="txtphdgui" FilterType="Numbers"></ajaxToolKit:FilteredTextBoxExtender>--%>
                                                                            <%--</div>--%>
                                                                        </div>
                                                                        <%--  <div class="form-group col-md-6">
                                                                   <div class="form-group col-md-10">--%>
                                                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                                            <label>Ph.D Awarded :</label>
                                                                            <asp:TextBox ID="txtphdawd" runat="server" CssClass="form-control" onkeypress="return CheckAlphabet(event,this);" ToolTip="Enter Ph.D Awarded"
                                                                                MaxLength="40" TabIndex="12"></asp:TextBox>
                                                                            <%--  <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtphdawd" FilterType="Numbers"></ajaxToolKit:FilteredTextBoxExtender>--%>
                                                                            <%-- </div>--%>
                                                                        </div>

                                                                        <%--<div class="form-group col-md-6">
                                                                           <div class="form-group col-md-10">--%>
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <label>Name of Candidate :<span style="color: Red">*</span></label>
                                                                            <asp:TextBox ID="txtCandidate" runat="server" CssClass="form-control" onkeypress="return CheckAlphabet(event,this);" ToolTip="Enter Name of Candidate"
                                                                                MaxLength="100" TabIndex="13"></asp:TextBox>


                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCandidate"
                                                                                Display="None" ErrorMessage="Please Enter Name of Candidate" ValidationGroup="PhdDetails"
                                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                                        </div>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <label>Registration Date :<span style="color: Red">*</span></label>
                                                                            <div class="input-group date">
                                                                                <div class="input-group-addon">
                                                                                    <i id="imgDT" runat="server" class="fa fa-calendar text-blue"></i>
                                                                                </div>
                                                                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" ToolTip="Enter Date"
                                                                                    TabIndex="14" Style="z-index: 0;"></asp:TextBox>
                                                                                <ajaxToolKit:CalendarExtender ID="ceDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate"
                                                                                    PopupButtonID="imgDT" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                                                </ajaxToolKit:CalendarExtender>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDate"
                                                                                    Display="None" ErrorMessage="Please Select Registration Date" ValidationGroup="PhdDetails"
                                                                                    SetFocusOnError="True">
                                                                                </asp:RequiredFieldValidator>
                                                                                <ajaxToolKit:MaskedEditExtender ID="meDate" runat="server" TargetControlID="txtDate"
                                                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                                                </ajaxToolKit:MaskedEditExtender>
                                                                                <ajaxToolKit:MaskedEditValidator ID="mevDate" runat="server" ControlExtender="meDate"
                                                                                    ControlToValidate="txtDate" InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                                    Display="None" TooltipMessage="Please Enter Date" EmptyValueBlurredText="Empty"
                                                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" IsValidEmpty="false" InitialValue="__/__/____" />
                                                                            </div>

                                                                        </div>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <label>Name of University :<span style="color: Red">*</span></label>
                                                                            <asp:TextBox ID="txtUniversity" runat="server" CssClass="form-control" onkeypress="return CheckAlphabet(event,this);" ToolTip="Enter Name of University"
                                                                                MaxLength="200" TabIndex="15"></asp:TextBox>

                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtUniversity"
                                                                                Display="None" ErrorMessage="Please Enter Name of University" ValidationGroup="PhdDetails"
                                                                                SetFocusOnError="True">

                                                                            </asp:RequiredFieldValidator>
                                                                        </div>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <label>Research Centre Name :</label>
                                                                            <asp:TextBox ID="txtResearch" runat="server" CssClass="form-control" onkeypress="return CheckAlphabet(event,this);" ToolTip="Enter Name of Research Centre"
                                                                                MaxLength="200" TabIndex="16"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <label>Research Guide :</label>
                                                                            <asp:TextBox ID="txtResGuide" runat="server" CssClass="form-control" onkeypress="return CheckAlphabet(event,this);" ToolTip="Enter Name of Research Guide"
                                                                                MaxLength="100" TabIndex="17"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <label>No of Publication related to Ph.D :</label>
                                                                            <asp:TextBox ID="txtNo" runat="server" CssClass="form-control" onkeypress="return CheckNumeric(event,this);" ToolTip="Enter Number of Publication Detail Related to Ph.D"
                                                                                MaxLength="5" TabIndex="18"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <label>Any research grant receive for Ph.D research :</label>
                                                                            <asp:TextBox ID="txtGrant" runat="server" CssClass="form-control" ToolTip="Enter Any research grant receive for Ph.D research"
                                                                                MaxLength="5" TabIndex="19"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <label>Patent published related to Ph.D :</label>
                                                                            <asp:TextBox ID="txtPatent" runat="server" CssClass="form-control" onkeypress="return CheckAlphabet(event,this);" ToolTip="Enter Patent published related to Ph.D"
                                                                                MaxLength="100" TabIndex="20"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <label>Registration Number :</label>
                                                                            <asp:TextBox ID="txtRegNum" runat="server" CssClass="form-control" ToolTip="Enter Research Number of the Scholar"
                                                                                MaxLength="20" TabIndex="21"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <label>Status :</label>
                                                                            <asp:RadioButtonList ID="rdbStatus" runat="server" AutoPostBack="true" TabIndex="22"
                                                                                RepeatDirection="Horizontal" ToolTip="Select Employment Yes/NO" OnSelectedIndexChanged="rdbStatus_SelectedIndexChanged">
                                                                                <asp:ListItem Enabled="true" Selected="True" Text="Ongoing" Value="O"></asp:ListItem>
                                                                                <asp:ListItem Enabled="true" Text="Completed" Value="C"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </div>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divyear" runat="server" visible="false">
                                                                            <label>Year of Completion :</label>
                                                                            <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" onkeypress="return CheckNumeric(event,this);" ToolTip="Enter Year of Completion"
                                                                                MaxLength="4" TabIndex="23"></asp:TextBox>
                                                                        </div>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <label>Title of Thesis :</label>
                                                                            <asp:TextBox ID="txtThesis" runat="server" CssClass="form-control" onkeypress="return CheckAlphabet(event,this);" ToolTip="Enter title of Thesis"
                                                                                MaxLength="500" TabIndex="24"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                </div>

                                                <%--Add for phd details--%>
                                                <div class="col-md-12 form-group">
                                                    <p class="text-center">
                                                        <asp:Button ID="btnPhd" runat="server" Text="Add" CssClass="btn btn-primary" TabIndex="25" ToolTip="Click to add" ValidationGroup="PhdDetails"
                                                            OnClick="btnPhd_Click" />
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="PhdDetails"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>


                                                <div class="col-md-12">
                                                    <div class="col-md-12">
                                                        <asp:Panel ID="panelphd" runat="server" ScrollBars="Auto">
                                                            <asp:ListView ID="lvphd" runat="server">
                                                                <EmptyDataTemplate>
                                                                    <p class="text-center text-bold">
                                                                        <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" />
                                                                    </p>
                                                                </EmptyDataTemplate>
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Ph.D Guidance Detail</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">

                                                                                <th>Action</th>
                                                                                <th>Sr.No.</th>
                                                                                <th>Name of Candidate</th>
                                                                                <th>Registration Date</th>
                                                                                <th>Name of University</th>
                                                                                <th>Research Centre Name</th>
                                                                                <th>Research Guide</th>
                                                                                <th>No of Publication related to Ph.D</th>
                                                                                <th>Any research grant receive for Ph.D research</th>
                                                                                <th>Patent published related to Ph.D</th>
                                                                                <th>Registration Number</th>
                                                                                <th>Status</th>
                                                                                <th>Year of Completion</th>
                                                                                <th>Title of Thesis</th>
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
                                                                            <asp:ImageButton ID="btnEditPhdDetails" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%#Eval("SPNO")%>'
                                                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditPhdDetails_Click" />

                                                                            <asp:ImageButton ID="btnPhdDelete" runat="server" CausesValidation="false"
                                                                                OnClick="btnPhdDelete_Click" CommandArgument='<%# Eval("SPNO")%>' ImageUrl="~/Images/delete.png" ToolTip="Delete Record"
                                                                                EnableViewState="true"
                                                                                OnClientClick="javascript:return confirm('Are you sure you want to delete ?')" />

                                                                        </td>
                                                                        <td><%#Container.DataItemIndex+1 %></td>
                                                                        <td>
                                                                            <%# Eval("CANDIDATENAME")%>
                                                                            <%--<asp:HiddenField ID="hdnphdguided" runat="server" Value='<%# Eval("PHDGUIDED")%>' />--%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("REGDATE","{0:dd/MM/yyyy}")%>
                                                                            <asp:HiddenField ID="hdnregdate" runat="server" Value='<%# Eval("REGDATE")%>' />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("UNIVERSITYNAME")%>
                                                                            <%-- <asp:HiddenField ID="hdnphdaward" runat="server" Value='<%# Eval("PHDAWARD")%>' />--%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("RESEARCHNAME")%>
                                                                            <%-- <asp:HiddenField ID="hdnphdaward" runat="server" Value='<%# Eval("PHDAWARD")%>' />--%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("GUIDENAME")%>
                                                                            <%-- <asp:HiddenField ID="hdnphdaward" runat="server" Value='<%# Eval("PHDAWARD")%>' />--%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("PUBLICATIONPHDNO")%>
                                                                            <%-- <asp:HiddenField ID="hdnphdaward" runat="server" Value='<%# Eval("PHDAWARD")%>' />--%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("PHDGRANT")%>
                                                                            <%-- <asp:HiddenField ID="hdnphdaward" runat="server" Value='<%# Eval("PHDAWARD")%>' />--%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("PHDPATENT")%>
                                                                            <%-- <asp:HiddenField ID="hdnphdaward" runat="server" Value='<%# Eval("PHDAWARD")%>' />--%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("REGISTRATIONNO")%>
                                                                            <%-- <asp:HiddenField ID="hdnphdaward" runat="server" Value='<%# Eval("PHDAWARD")%>' />--%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("PHDSTATUS")%>
                                                                            <%-- <asp:HiddenField ID="hdnphdaward" runat="server" Value='<%# Eval("PHDAWARD")%>' />--%>
                                                                        </td>
                                                                         <td>
                                                                            <%# Eval("YEAR")%>
                                                                            <%-- <asp:HiddenField ID="hdnphdaward" runat="server" Value='<%# Eval("PHDAWARD")%>' />--%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("THESISTITLE")%>
                                                                            <%-- <asp:HiddenField ID="hdnphdaward" runat="server" Value='<%# Eval("PHDAWARD")%>' />--%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>

                                                <%--                                                Add for phd details--%>


                                                <%--  </div>--%>
                                                <%--Added for title--%>
                                                <asp:Panel ID="pnlID" runat="server">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>ID Details</h5>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Web of Science Researcher ID :<span style="color: Red">*</span></label>
                                                                </div>
                                                                <asp:TextBox ID="txtWeb" runat="server" CssClass="form-control" ToolTip="Enter Web of Science Researcher ID"
                                                                    MaxLength="150" TabIndex="26"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvWeb" runat="server" ControlToValidate="txtWeb"
                                                                    Display="None" ErrorMessage="Please Enter Web of Science Researcher ID" ValidationGroup="IDDetail"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Scopus Author ID :<span style="color: Red">*</span></label>
                                                                </div>
                                                                <asp:TextBox ID="txtScopus" runat="server" CssClass="form-control" ToolTip="Enter Scopus Author ID"
                                                                    MaxLength="150" TabIndex="27"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvScopus" runat="server" ControlToValidate="txtScopus"
                                                                    Display="None" ErrorMessage="Please Enter Scopus Author ID" ValidationGroup="IDDetail"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Orchid id :</label>
                                                                </div>
                                                                <asp:TextBox ID="txtOrchid" runat="server" CssClass="form-control" ToolTip="Enter Orchid ID"
                                                                    MaxLength="150" TabIndex="28"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCandidate"
                                                                    Display="None" ErrorMessage="Please Enter Name of Candidate" ValidationGroup="PhdDetails"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Research Supervisor Id (BSACIST) :</label>
                                                                </div>
                                                                <asp:TextBox ID="txtSupervisor" runat="server" CssClass="form-control" ToolTip="Enter Research Supervisor Id (BSACIST)"
                                                                    MaxLength="150" TabIndex="29"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtUniversity"
                                                                    Display="None" ErrorMessage="Please Enter Name of University" ValidationGroup="PhdDetails"
                                                                    SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12 form-group">
                                                        <p class="text-center">
                                                            <asp:Button ID="btnIDAdd" runat="server" Text="Add" CssClass="btn btn-primary" TabIndex="30" ToolTip="Click to add" ValidationGroup="IDDetail"
                                                                OnClick="btnIDAdd_Click" />
                                                            <asp:ValidationSummary ID="ValidationID" runat="server" ValidationGroup="IDDetail"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        </p>
                                                    </div>

                                                    <div class="col-md-12">
                                                        <div class="col-md-12">
                                                            <asp:Panel ID="pnlIDList" runat="server" ScrollBars="Auto">
                                                                <asp:ListView ID="lvIDList" runat="server">
                                                                    <EmptyDataTemplate>
                                                                        <p class="text-center text-bold">
                                                                            <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" />
                                                                        </p>
                                                                    </EmptyDataTemplate>
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>ID Detail</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                            <thead>
                                                                                <tr class="bg-light-blue">
                                                                                    <th>Action</th>
                                                                                    <th>Sr.No.</th>
                                                                                    <th>Web ID</th>
                                                                                    <th>Scopus ID</th>
                                                                                    <th>Orchid ID</th>
                                                                                    <th>Research Supervisor ID</th>
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
                                                                                <asp:ImageButton ID="btnEditIdDetails" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%#Eval("SIDNO")%>'
                                                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditIdDetails_Click" />

                                                                                <asp:ImageButton ID="btnIDDelete" runat="server" CausesValidation="false"
                                                                                    OnClick="btnIDDelete_Click" CommandArgument='<%# Eval("SIDNO")%>' ImageUrl="~/Images/delete.png" ToolTip="Delete Record"
                                                                                    EnableViewState="true"
                                                                                    OnClientClick="javascript:return confirm('Are you sure you want to delete ?')" />
                                                                            </td>
                                                                            <td><%#Container.DataItemIndex+1 %></td>
                                                                            <td>
                                                                                <%# Eval("WEB")%>                                                                        
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("SCOPUS")%>                                                                        
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("ORCHID")%>                                                                        
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("RESEARCH")%>                                                                        
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>

                                                </asp:Panel>
                                                <%------%>
                                                <%--Phd thesis Evaluated--%>
                                                <asp:Panel ID="pnlThesis" runat="server">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>PhD Thesis Evaluated</h5>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Thesis title :<span style="color: Red">*</span></label>
                                                                </div>
                                                                <asp:TextBox ID="txtThesisTitle" runat="server" CssClass="form-control" ToolTip="Enter Thesis title"
                                                                    MaxLength="500" TabIndex="31"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtThesisTitle"
                                                                    Display="None" ErrorMessage="Please Enter Thesis title" ValidationGroup="Thesis"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>University/Institute :<span style="color: Red">*</span></label>
                                                                </div>
                                                                <asp:TextBox ID="txtThesisUniversity" runat="server" CssClass="form-control" ToolTip="Enter University/Institue"
                                                                    MaxLength="150" TabIndex="32"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvUni" runat="server" ControlToValidate="txtThesisUniversity"
                                                                    Display="None" ErrorMessage="Please Enter University/Institute Name" ValidationGroup="Thesis"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Month :</label>
                                                                </div>
                                                                <asp:TextBox ID="txtMonth" runat="server" CssClass="form-control" ToolTip="Enter Month"
                                                                    MaxLength="20" TabIndex="33"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCandidate"
                                                                    Display="None" ErrorMessage="Please Enter Name of Candidate" ValidationGroup="PhdDetails"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Year :</label>
                                                                </div>
                                                                <asp:TextBox ID="txtThesisYear" runat="server" CssClass="form-control" ToolTip="Enter Year"
                                                                    MaxLength="4" TabIndex="34" onkeyup="validateNumeric(this);"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtUniversity"
                                                                    Display="None" ErrorMessage="Please Enter Name of University" ValidationGroup="PhdDetails"
                                                                    SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12 form-group">
                                                        <p class="text-center">
                                                            <asp:Button ID="btnThesis" runat="server" Text="Add" CssClass="btn btn-primary" TabIndex="35" ToolTip="Click to add" ValidationGroup="Thesis"
                                                                OnClick="btnThesis_Click" />
                                                            <asp:ValidationSummary ID="validateThesis" runat="server" ValidationGroup="Thesis"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        </p>
                                                    </div>

                                                    <div class="col-md-12">
                                                        <div class="col-md-12">
                                                            <asp:Panel ID="pnlThesisList" runat="server" ScrollBars="Auto">
                                                                <asp:ListView ID="lvThesis" runat="server">
                                                                    <EmptyDataTemplate>
                                                                        <p class="text-center text-bold">
                                                                            <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" />
                                                                        </p>
                                                                    </EmptyDataTemplate>
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Thesis Detail</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                            <thead>
                                                                                <tr class="bg-light-blue">
                                                                                    <th>Action</th>
                                                                                    <th>Sr.No.</th>
                                                                                    <th>Thesis Title</th>
                                                                                    <th>University</th>
                                                                                    <th>Month</th>
                                                                                    <th>Year</th>
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
                                                                                <asp:ImageButton ID="btnEditThesisDetails" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%#Eval("THIDNO")%>'
                                                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditThesisDetails_Click" />

                                                                                <asp:ImageButton ID="btnThesisDelete" runat="server" CausesValidation="false"
                                                                                    CommandArgument='<%# Eval("THIDNO")%>' ImageUrl="~/Images/delete.png" ToolTip="Delete Record"
                                                                                    EnableViewState="true" OnClick="btnThesisDelete_Click"
                                                                                    OnClientClick="javascript:return confirm('Are you sure you want to delete ?')" />
                                                                            </td>
                                                                            <td><%#Container.DataItemIndex+1 %></td>
                                                                            <td>
                                                                                <%# Eval("THESISTITLE")%>                                                                       
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("THESISUNIVERSITY")%>                                                                       
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("MONTH")%>                                                                        
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("YEAR")%>                                                                        
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>

                                                </asp:Panel>
                                                <%----%>
                                            </ContentTemplate>
                                            <Triggers>
                                                <%--<asp:AsyncPostBackTrigger ControlID="btnSubmit" />--%>
                                                <asp:PostBackTrigger ControlID="btnAddBond" />
                                                <asp:PostBackTrigger ControlID="btnAdd" />
                                                <asp:PostBackTrigger ControlID="btnSubmit" />
                                                <asp:PostBackTrigger ControlID="btnPhd" />
                                                <asp:PostBackTrigger ControlID="btnCancel" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>

                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="26"
                                ValidationGroup="emp" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="27"
                                CausesValidation="false" OnClick="btnCancel_Click" />
                            <asp:ValidationSummary ID="vsEmp" runat="server" ValidationGroup="emp" ShowMessageBox="true"
                                ShowSummary="false" DisplayMode="List" />
                        </div>

                        <div class="col-md-12">
                            <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="lvother" runat="server">
                                    <EmptyDataTemplate>
                                        <br />
                                        <p class="text-center text-bold">
                                            <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows in Miscellaneous Other Detail"></asp:Label>
                                        </p>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Miscellaneous Detail</h5>
                                        </div>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Action</th>
                                                    <%-- <th>H-Index
                                                        </th>
                                                        <th>Bond
                                                        </th>
                                                        <th>From Date
                                                        </th>
                                                        <th>To Date
                                                        </th>--%>
                                                    <th>Ph.D Under Guidance
                                                    </th>
                                                    <th>Ph.D Awarded
                                                    </th>
                                                    <%--<th>Attachment
                                                        </th>--%>
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
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("MOSNO")%>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                <%--OnClick="btnEdit_Click"--%>
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("MOSNO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record"
                                                    OnClientClick="showConfirmDel(this); return false;" OnClick="btnDelete_Click" />
                                                <%--OnClick="btnDelete_Click"--%>
                                            </td>
                                            <%-- <td>
                                                <%# Eval("H_Index")%>
                                            </td>
                                            <td>
                                                <%# Eval("FROM_DATE","{0:dd/MM/yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("TO_DATE", "{0:dd/MM/yyyy}")%>
                                            </td>--%>
                                            <td>
                                                <%# Eval("PHDGUIDEDNEW")%> 
                                            </td>
                                            <td>
                                                <%# Eval("PHDAWARDNEW")%>
                                            </td>
                                            <%--<td>
                                                <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("MOSNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                            </td>--%>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <div class="col-md-12">
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

        //function validateNumeric(txt) {
        //    if (isNaN(txt.value)) {
        //        txt.value = txt.value.substring(0, (txt.value.length) - 1);
        //        txt.value = '';
        //        txt.focus = true;
        //        alert("Only Numeric Characters allowed !");
        //        return false;
        //    }
        //    else
        //        return true;
        //}

        //function validateAlphabet(txt) {
        //    var expAlphabet = /^[A-Za-z .]+$/;
        //    if (txt.value.search(expAlphabet) == -1) {
        //        txt.value = txt.value.substring(0, (txt.value.length) - 1);
        //        txt.value = '';
        //        txt.focus = true;
        //        alert("Only Alphabets allowed!");
        //        return false;
        //    }
        //    else
        //        return true;
        //}


        function CheckNumeric(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            //alert(k);
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k > 45 && k < 58) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter numeric Value');
                obj.focus();
            }
            return false;
        }
        onkeypress = "return CheckAlphabet(event,this);"
        function CheckAlphabet(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
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


    <script type="text/javascript">

        function CalDuration() {

            var datejoing = document.getElementById('<%=txtFromDate.ClientID%>').value;
            var dateleaving = document.getElementById('<%=txtToDate.ClientID%>').value;
            if (datejoing != '' && dateleaving != '') {

                var dateElements = datejoing.split("/");
                var outputDateString = dateElements[2] + "/" + dateElements[1] + "/" + dateElements[0];
                var dateElementsnew = dateleaving.split("/");
                var outputDateStringnew = dateElementsnew[2] + "/" + dateElementsnew[1] + "/" + dateElementsnew[0];

                var date1 = new Date(outputDateString);
                var date2 = new Date(outputDateStringnew);

                if (date1 > date2) {
                    alert("To date should be greater than equal to from date.");
                    document.getElementById('<%=txtToDate.ClientID%>').value = '';
                    return;
                }
            }
            else
                document.getElementById('<%=txtToDate.ClientID%>').value = '';
        }

    </script>

    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

    </script>
</asp:Content>


