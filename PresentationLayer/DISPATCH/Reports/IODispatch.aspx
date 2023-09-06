<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="IODispatch.aspx.cs" Inherits="DISPATCH_Reports_IODispatch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">DISPATCH INWARD AND OUTWARD REGISTER</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlMain" runat="server">
                        <div class="col-12">
                          <%--  <div class="sub-heading">
                                <h5></h5>
                            </div>--%>
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Dispatch Register</label>
                                    </div>
                                    <asp:RadioButtonList ID="radlSelect" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="radlSelect_SelectedIndexChanged" ToolTip="Select Dispatch Register Type" TabIndex="1">
                                        <asp:ListItem Selected="True" Value="I">Inward</asp:ListItem>
                                        <asp:ListItem Value="O">Outward</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Department</label>
                                    </div>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" TabIndex="2" ToolTip="Select Department" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChange">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>From/To User :</label>
                                    </div>
                                    <asp:DropDownList ID="ddlFrmTo" runat="server" AppendDataBoundItems="true" TabIndex="3" ToolTip="Select From/To User" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Post Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlPostType" runat="server" AppendDataBoundItems="true" TabIndex="4" ToolTip="Select Post Type" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Carrier Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCarrier" runat="server" AppendDataBoundItems="true" TabIndex="5" ToolTip="Select Carrier Name" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>


                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Letter Category </label>
                                    </div>
                                    <asp:DropDownList ID="ddlLCat" runat="server" AppendDataBoundItems="true" TabIndex="6" ToolTip="Select Letter Category" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">Official</asp:ListItem>
                                        <asp:ListItem Value="2">Personal</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divCheque" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Cheque/DD</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCheque" runat="server" AppendDataBoundItems="true" ToolTip="Select Cheque/DD" TabIndex="7" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">Cheque</asp:ListItem>
                                        <asp:ListItem Value="2">DD</asp:ListItem>
                                    </asp:DropDownList>
                                </div>


                                <div class="form-group col-lg-3 col-md-6 col-12" id="divUT" runat="server">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>User Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlUserType" runat="server" AppendDataBoundItems="true" ToolTip="Select User Type" TabIndex="7" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="P">Principal</asp:ListItem>
                                        <asp:ListItem Value="S">Secretary</asp:ListItem>
                                        <asp:ListItem Value="C">Chairman</asp:ListItem>
                                        <asp:ListItem Value="H">HOD</asp:ListItem>
                                       <%-- <asp:ListItem Value="V">SVCE</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>


                                  <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgFrmDt" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFrmDate" runat="server" MaxLength="100" TabIndex="8" CssClass="form-control"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="rfvFrmDt" runat="server" ControlToValidate="txtFrmDate"
                                                Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Date" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                            <ajaxToolKit:CalendarExtender ID="ceFrmDt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrmDate"
                                                PopupButtonID="imgFrmDt" Enabled="true" EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>

                                              <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    OnInvalidCssClass="errordate" TargetControlID="txtFrmDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server"  ControlExtender="medt" ControlToValidate="txtFrmDate"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Date Format [dd/MM/yyyy] "
                                                    InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy] " Display="None" Text="*"
                                                    ValidationGroup="Date"></ajaxToolKit:MaskedEditValidator>

                                        </div>
                                     </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>To Date </label>
                                        </div>
                                     <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="18" TabIndex="9" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTodt" runat="server" ControlToValidate="txtToDate"
                                                Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="true" ValidationGroup="Date"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:CalendarExtender ID="ceTodt" runat="server" Enabled="true" EnableViewState="true"
                                                Format="dd/MM/yyyy" PopupButtonID="imgTodt" TargetControlID="txtToDate">
                                            </ajaxToolKit:CalendarExtender>

                                           <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtToDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Date Format [dd/MM/yyyy] " ControlToValidate="txtToDate"
                                                    InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy] " Display="None" Text="*" ValidationGroup="Date">
                                                </ajaxToolKit:MaskedEditValidator>

                                        </div>
                                    </div>
                                
                                 <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                              <sup></sup>
                                            <label></label>
                                        </div>
                                        <asp:Label ID="lblerror" runat="server" SkinID="Errorlbl"></asp:Label>
                                    <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                     </div>


                                
                            </div>
                        </div>
                    </asp:Panel>
                    <div class=" col-12 btn-footer">
                    
                                <asp:Button ID="btnSubmit" Style="text-align: center" runat="server" Text="Show" TabIndex="10"
                                    ValidationGroup="Date" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="CLick here to Submit" />
                              <asp:Button ID="btnShow" Style="text-align: center" runat="server" Text="Print" ValidationGroup="Date"
                                  OnClick="btnShow_Click" CssClass="btn btn-info" ToolTip="Click here to Show Dispatch" TabIndex="12" />
                          <asp:Button ID="btnClear" Style="text-align: center" runat="server" Text="Clear" TabIndex="11" OnClick="btnClear_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                            
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Date" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        
                     
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="Panel1" runat="server">

                            <asp:ListView ID="lvLetterDetails" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading"><h5>Letter Details</h5>
                                        </div>
                                      <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                    <th>Letter date And Post Type
                                                    </th>
                                                    <th>Reference No.
                                                    </th>
                                                    <th>From Address
                                                    </th>
                                                    <th>Subject
                                                    </th>
                                                    <th>To Address
                                                    </th>
                                                    <th>Attendant
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
                                            <%# Eval("RECSENTDT","{0:dd-MMM-yyyy}")%>
                                            <%# Eval("POSTTYPENAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("OUTREFERENCENO")%>
                                        </td>
                                        <td>
                                            <%# Eval("IOFROM")%>
                                            <%# Eval("ADDRESS")%>
                                        </td>
                                        <td>
                                            <%# Eval("SUBJECT")%>
                                        </td>
                                        <td >
                                            <%# Eval("DEPARTMENTNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("PEON")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                        </asp:Panel>
                        </div>
                       <div class="col-12">
                        <asp:Panel ID="Panel2" runat="server">
                            <asp:ListView ID="lvOutward" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <div id="lgv2">
                                        <div class="sub-heading"><h5>Letter Details</h5>
                                        </div>
                                          <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                    <th>Letter date
                                                    </th>
                                                    <th>Sender Name
                                                    </th>
                                                    <th>Receiver Name/Address
                                                    </th>
                                                    <th>Subject
                                                    </th>
                                                    <th>Post Type
                                                    </th>
                                                    <th>Tracking No.
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
                                    <%--    <tr>
                            <td style="width: 10%;">
                                <%# Eval("ENTRYDATE", "{0:dd-MMM-yyyy}")%>
                            </td>
                            <td style="width: 15%;">
                                <%# Eval("TRACKING_NO")%>
                            </td>                            
                            <td style="width: 20%;">
                                <%# Eval("SUBJECT")%>
                            </td>
                            <td>
                                <%# Eval("UA_FULLNAME")%>
                                <br />
                                <%# Eval("DEPARTMENTNAME")%>
                            </td>
                            <td style="width: 20%;">
                                <%# Eval("IOTO")%>
                                <%# Eval("MULTIPLE_ADDRESS")%>
                                <%# Eval("CITY")%>
                                <%# Eval("PINNO")%>
                                <%# Eval("STATENAME")%>
                            </td>
                            <td style="width: 15%;">
                                <%# Eval("POSTTYPENAME")%>
                            </td>
                        </tr>--%>
                                    <tr>
                                        <td>
                                           <%-- <%# Eval("ENTRYDATE", "{0:dd-MMM-yyyy}")%>--%>
                                             <%# Eval("SENTDT", "{0:dd-MMM-yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("UA_FULLNAME")%>
                                            <br />
                                            <%# Eval("DEPARTMENTNAME")%>
                                        </td>
                                        <td style="width: 20%;">
                                            <%# Eval("IOTO")%>
                                            <%# Eval("MULTIPLE_ADDRESS")%>
                                            <%# Eval("CITY")%>
                                            <%# Eval("PINNO")%>
                                            <%# Eval("STATENAME")%>
                                        </td>
                                        <td style="width: 20%;">
                                            <%# Eval("SUBJECT")%>
                                        </td>
                                        <td style="width: 15%;">
                                            <%# Eval("POSTTYPENAME")%>
                                        </td>
                                        <td style="width: 15%;">
                                            <%# Eval("TRACKING_NO")%>
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
    <div id="divMsg" runat="server"></div>
</asp:Content>


