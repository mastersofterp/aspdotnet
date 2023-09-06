<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BillsummaryReport.aspx.cs"
    Inherits="ESTATE_Report_BillsummaryReport" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="updReport" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">MONTHLY BILLING SUMMARY REPORT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Monthly Billing Summary Report
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Quater Type<%--<span style="color: red;">*</span> </td>--%>:</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlquartertype" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="11"  ></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Select Bill Month<span style="color: red;">*</span>:</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="input-group date">
                                                        <asp:TextBox ID="txtselectdate" runat="server" TabIndex="12" CssClass="form-control" MaxLength="7"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="calextenderdatebirth" runat="server" Format="MM/yyyy"
                                                            TargetControlID="txtselectdate" PopupButtonID="imgCal"
                                                            Enabled="True" />
                                                        <%--<ajaxToolKit:MaskedEditExtender ID="msedatebirth" runat="server" TargetControlID="txtselectdate"
                                                                Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" />--%>
                                                        <div class="input-group-addon">
                                                            <%--<asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                            <asp:ImageButton runat="Server" ID="imgCal" ImageUrl="~/images/calendar.png" AlternateText="Click to show calendar" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:Button ID="btnBillsummary" runat="server" Text="Bill Summary" CssClass="btn btn-primary" OnClick="btnBillsummary_Click" TabIndex="13"  />
                                                    <%-- <asp:Button ID="btnWaterReport" runat="server" Text="Water Billing" Width="110px" onclick="btnWaterReport_Click" 
       />
                                                    --%>
                                                    <asp:Button ID="btnreset" runat="server" Text="Reset" CssClass="btn btn-warning" OnClick="btnreset_Click"  TabIndex="14" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--<asp:Repeater ID="Repeater_billsummary" runat="server" 
                OnItemDataBound="Repeater_billsummary_OnItemDataBound" 
                onitemcommand="Repeater_billsummary_ItemCommand">
        <HeaderTemplate>
            <table cellpadding="0" cellspacing="0" class="table_repeaterCandiPro" style="width: 100%">
                <thead class="ui-widget-header">
                    <tr>
                        <th style="width: 2%; text-align: center">
                          QTRNO
                         </th>
                               
                        <th style="width: 40%; text-align: center;">
                          Name of Occupant
                        </th>
                        
                        <th style="width: 18%; text-align: center;">
                         Electric Bill
                        </th>
                        <th style="width: 20%; text-align: center;">
                          Water Bill
                        </th>
                        <th style="width: 20%; text-align: center;">
                         Total Amt
                        </th>
                        
                        
                        
                    </tr>
                </thead>
              <tbody>
                
        </HeaderTemplate>
       
         <ItemTemplate>
               <tr onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#D6ECF9'">
      
                <td style="width: 2%; text-align: center">
                <asp:Label ID="lblqauterno" runat="server" CssClass="label" Text=' <%#Eval("METER_TYPE")%>'></asp:Label> 
                    
                </td>
                             
                <td style="width: 23%; text-align: center;">
                    <asp:Label ID="lbloccupantname" runat="server" CssClass="label" Text=' <%#Eval("METER_TYPE")%>'></asp:Label>
                </td>
                <td style="width: 15%; text-align: center;">
                    <asp:Label ID="lblelectricamount" runat="server" CssClass="label" Text=' <%#Eval("METER_NO")%>'></asp:Label>
                </td>
                <td style="width: 15%; text-align: center;">
                    <asp:Label ID="lblwateramount" runat="server" CssClass="label" Text=' <%#Eval("RENT")%>'></asp:Label>
                </td>
                <td style="width: 15%; text-align: center;">
                    <asp:Label ID="lbltotalamount" runat="server" CssClass="label" Text=' <%#Eval("EMETER_MULTI")%>'></asp:Label>
                </td>
                
                
            </tr>
        </ItemTemplate>
       
         <AlternatingItemTemplate>
           
                <td style="width: 2%; text-align: center">
                <asp:Label ID="lblqauterno" runat="server" CssClass="label" Text=' <%#Eval("METER_TYPE")%>'></asp:Label> 
                    
                </td>
                             
                <td style="width: 23%; text-align: center;">
                    <asp:Label ID="lbloccupantname" runat="server" CssClass="label" Text=' <%#Eval("METER_TYPE")%>'></asp:Label>
                </td>
                <td style="width: 15%; text-align: center;">
                    <asp:Label ID="lblelectricamount" runat="server" CssClass="label" Text=' <%#Eval("METER_NO")%>'></asp:Label>
                </td>
                <td style="width: 15%; text-align: center;">
                    <asp:Label ID="lblwateramount" runat="server" CssClass="label" Text=' <%#Eval("RENT")%>'></asp:Label>
                </td>
                <td style="width: 15%; text-align: center;">
                    <asp:Label ID="lbltotalamount" runat="server" CssClass="label" Text=' <%#Eval("EMETER_MULTI")%>'></asp:Label>
                </td>
                
                
            </tr>
                
            
          </AlternatingItemTemplate>
       
          <FooterTemplate>
          </tbody>
          </table>
          </FooterTemplate>
        
          </asp:Repeater>--%>
            <div id="divMsg" runat="server">
            </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

