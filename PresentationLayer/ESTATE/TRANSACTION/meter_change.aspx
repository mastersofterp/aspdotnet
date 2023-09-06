<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="meter_change.aspx.cs"
    Inherits="ESTATE_Transaction_meter_change" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">METER CHANGE</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Add/Edit Meter Change
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Resident Type<span style="color: red;">*</span>:</label>
                                                    <asp:DropDownList ID="ddlConsumerType" runat="server" AppendDataBoundItems="true"
                                                        CssClass="form-control" AutoPostBack="true" TabIndex="1"
                                                        OnSelectedIndexChanged="ddlConsumerType_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlConsumerType"
                                                        ErrorMessage="Please Select Resident Type" Display="None" ValidationGroup="materialvacant"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <asp:ValidationSummary ID="valsummaterial" runat="server" ValidationGroup="materialvacant" DisplayMode="List"
                                                        ShowSummary="false" ShowMessageBox="true" />
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Name<span style="color: red;">*</span>:</label>
                                                    <asp:DropDownList ID="ddloccupantName" runat="server" AppendDataBoundItems="true"
                                                        CssClass="form-control" TabIndex="2" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddloccupantName_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddloccupantName"
                                                        ErrorMessage="Please Select Name." Display="None" ValidationGroup="materialvacant"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Select Date:</label>
                                                    <asp:TextBox ID="txtselectdt" runat="server" CssClass="form-control" TabIndex="3" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Quarter Type:</label>
                                                    <asp:TextBox ID="txtQuatertype" runat="server" CssClass="form-control" TabIndex="4" Enabled="false"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnquaterno" runat="server" />

                                                </div>

                                                <div class="col-md-4">
                                                    <label>Quarter No:</label>
                                                    <asp:TextBox ID="txtqaterno" runat="server" CssClass="form-control" TabIndex="5" Enabled="false"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnquatertypeno" runat="server" />
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="panel panel-info"></div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <asp:CheckBox ID="chkEbillModification" runat="server" Text=" Electric Bill Modification" TabIndex="6" />
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="panel panel-info"></div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Select Old Meter No :</label>
                                                    <asp:DropDownList ID="ddloldMeterId" runat="server" AppendDataBoundItems="true" Enabled="false"
                                                        CssClass="form-control" TabIndex="7">
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Meter Type :</label>
                                                    <asp:DropDownList ID="ddlmeterType" runat="server" AppendDataBoundItems="true" Enabled="false"
                                                        CssClass="form-control" TabIndex="8">
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Prv. Month Reading :</label>
                                                    <asp:TextBox ID="txtPreMonthReading" runat="server" CssClass="form-control" TabIndex="9" Enabled="false"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbePreMonthReading" runat="server" TargetControlID="txtPreMonthReading"
                                                        ValidChars="0123456789.">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Closing Reading<span style="color: red;">*</span>:</label>
                                                    <asp:TextBox ID="txtClosingReading" runat="server" CssClass="form-control" TabIndex="10" MaxLength="6"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeClosingReading" runat="server" TargetControlID="txtClosingReading"
                                                        ValidChars="0123456789.">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtClosingReading"
                                                        ErrorMessage="Please Enter Closing reading." Display="None" ValidationGroup="materialvacant"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Select New Meter Type :</label>
                                                    <asp:DropDownList ID="ddlNewMeterType" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="11"
                                                        CssClass="form-control" OnSelectedIndexChanged="ddlNewMeterType_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Meter No<span style="color: red;">*</span>:</label>
                                                    <asp:DropDownList ID="ddlMeterId" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                        TabIndex="12">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddloccupantName"
                                                        ErrorMessage="Please Select Meter No." Display="None" ValidationGroup="materialvacant" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Meter Start Reading<span style="color: red;">*</span>:</label>
                                                    <asp:TextBox ID="txtMeterStartReading" runat="server" CssClass="form-control" TabIndex="13" MaxLength="6"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeMeterStartReading" runat="server" TargetControlID="txtMeterStartReading" ValidChars="0123456789."></ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtMeterStartReading"
                                                        ErrorMessage="Please Enter Meter Start reading." Display="None" ValidationGroup="materialvacant" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Diff for Prv Meter Reading<span style="color: red;">*</span>:</label>
                                                    <asp:TextBox ID="txtDifference" runat="server" CssClass="form-control" TabIndex="14" onClick="calcadiff();" MaxLength="6"> </asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtDifference" ValidChars="0123456789."></ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDifference"
                                                        ErrorMessage="Please Enter Diff. of meter reading." Display="None" ValidationGroup="materialvacant" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="panel panel-info"></div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <div class="text-center">
                                                        <asp:Button ID="btnMeterChange" runat="server" Text="Meter Change" CssClass="btn btn-primary"
                                                            TabIndex="15" OnClick="btnMeterChange_Click" ValidationGroup="materialvacant" />
                                                        <asp:Button ID="btnPrint" runat="server" Text="Report" CssClass="btn btn-info"
                                                            OnClick="btnPrint_Click" TabIndex="16" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Reset" CssClass="btn btn-warning"
                                                            OnClick="btnCancel_Click" TabIndex="17" />
                                                    </div>
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
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function calcadiff() {
            var tot = 0.00;
            var oldAmt = document.getElementById('<%=txtPreMonthReading.ClientID%>').value;
            var currAmt = document.getElementById('<%=txtClosingReading.ClientID%>').value;
            if (currAmt != '' & oldAmt != '') {
                if (parseInt(currAmt) >= parseInt(oldAmt)) {
                    tot = (parseInt(currAmt) - parseInt(oldAmt));
                }
                else {
                    alert("Wrong value of Closing Reading");
                    document.getElementById('<%=txtClosingReading.ClientID%>').value = '';
                    return false;
                }
            }
            else {
                alert("Please Check Current Reading && Old Reading");
            }
            document.getElementById('<%=txtDifference.ClientID%>').value = tot;
        }
    </script>

    <%--       function calWaterDiff() {

                   var tot = 0.00;
                    
                    var oldAmt = document.getElementById('<%=txtPreMonthReading_water.ClientID%>').value;
                    var currAmt = document.getElementById('<%=txtWaterClosingReading.ClientID%>').value;

                    if (currAmt != '' & oldAmt != '') {

                        if (parseInt(currAmt) >= parseInt(oldAmt)) {
                            tot = (parseInt(currAmt) - parseInt(oldAmt));
                       }
                        else {

                        alert("Wrong value of Closing Reading");
                            document.getElementById('<%=txtWaterClosingReading.ClientID%>').value = '';
                            return false;
                        }
                  }
                    else
                    {
                        alert("Please Check Current Reading && Old Reading");

                  }

                   document.getElementById('<%=txtWaterMeterDifference.ClientID%>').value = tot;

                     
                }--%>
</asp:Content>

