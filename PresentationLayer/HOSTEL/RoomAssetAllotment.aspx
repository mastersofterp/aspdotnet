<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="RoomAssetAllotment.aspx.cs" Inherits="HOSTEL_RoomAssetAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <script type="text/javascript">
            //On UpdatePanel Refresh
            //var prm = Sys.WebForms.PageRequestManager.getInstance();
            //if (prm != null) {
            //    prm.add_endRequest(function (sender, e) {
            //        if (sender._postBackSettings.panelsToUpdate != null) {
            //            $('#table2').dataTable();
            //        }
            //    });
            //};

            //Below function added by Saurabh L on 30/09/2022
            //Purpose: for validation 
            function CheckInteger(event, element) {
                var charCode = (event.which) ? event.which : event.keyCode
                if (charCode == 8) return true;
                if ((charCode < 48 || charCode > 57))
                    return false;
                return true;
            };
            //--------- End by Saurabh L on 30/09/2022          
    </script>



    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">ASSET ALLOTMENT</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel </label>
                                </div>
                                <asp:DropDownList ID="ddlHostel" runat="server" TabIndex="1" AppendDataBoundItems="True"
                                    AutoPostBack="True"  CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlHostel_SelectedIndexChanged"/>
                                <asp:RequiredFieldValidator ID="rfvHostel" runat="server" ControlToValidate="ddlHostel"
                                    Display="None" ErrorMessage="Please Select Hostel." ValidationGroup="Submit"
                                    SetFocusOnError="True" InitialValue="0" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Block </label>
                                </div>
                               <asp:DropDownList ID="ddlBlock" runat="server" TabIndex="2" AppendDataBoundItems="True"
                                    AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                      OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvBlock" runat="server" ControlToValidate="ddlBlock"
                                    Display="None" ErrorMessage="Please Select Block." ValidationGroup="Submit" SetFocusOnError="True"
                                    InitialValue="0" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Floor </label>
                                </div>
                                  <asp:DropDownList ID="ddlFloor" runat="server" TabIndex="3" AppendDataBoundItems="True" AutoPostBack="True"
                                    data-select2-enable="true" CssClass="form-control" OnSelectedIndexChanged="ddlFloor_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvFloor" runat="server" ControlToValidate="ddlFloor"
                                    Display="None" ErrorMessage="Please Select Floor." ValidationGroup="Submit" SetFocusOnError="True"
                                    InitialValue="0" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Rooms </label>
                                </div>
                                <asp:DropDownList ID="ddlRoom" runat="server" TabIndex="4" data-select2-enable="true"
                                    AppendDataBoundItems="True"  AutoPostBack="True" CssClass="form-control" Enabled="false" OnSelectedIndexChanged="ddlRoom_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvRoom" runat="server" ControlToValidate="ddlRoom"
                                    Display="None" ErrorMessage="Please Select Room." ValidationGroup="Submit" SetFocusOnError="True"
                                    InitialValue="0" />
                            </div>
                        </div>
                        <div class="row">

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Student In Room </label>
                                </div>
                                  <asp:TextBox ID="txtRoomCount" runat="server" TabIndex="5" MaxLength="5" Enabled="false"  CssClass="form-control"/>
                        
                             </div>


                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Asset </label>
                                </div>
                                <asp:DropDownList ID="ddlAsset" runat="server" TabIndex="6" data-select2-enable="true"
                                    AppendDataBoundItems="true" AutoPostBack="True"  CssClass="form-control" OnSelectedIndexChanged="ddlAsset_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvAsset" runat="server" ControlToValidate="ddlAsset"
                                    Display="None" ErrorMessage="Please Select Asset." ValidationGroup="Submit" SetFocusOnError="True"
                                    InitialValue="0" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Available Asset Quantity </label>
                                </div>
                                  <asp:TextBox ID="txtAvailableAssetQty" runat="server" TabIndex="7" MaxLength="5" Enabled="false"  CssClass="form-control"/>
                        
                             </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Allot Asset Quantity </label>
                                </div>
                              <asp:TextBox ID="txtAssetQty" runat="server" TabIndex="8" data-select2-enable="true" onkeypress="return CheckInteger(event,this);"    CssClass="form-control"/>
                                <asp:RequiredFieldValidator ID="rfvAssetQty" runat="server" ControlToValidate="txtAssetQty"
                                    Display="None" ErrorMessage="Please Enter Allot Asset Quantity." ValidationGroup="Submit"
                                    SetFocusOnError="True" />
                            </div>

                           
                        </div>
                        <div class="row">
                             <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Allotment Date </label>
                                </div>
                                <div class="input-group">
                                <div class="input-group-addon">
                                        <i id="imgSessionStart" runat="server" class="fa fa-calendar"></i>
                                    </div>
                                <asp:TextBox ID="txtAllotmentDate" runat="server" TabIndex="9"  CssClass="form-control"/>
                                    <%--  <asp:Image ID="imgAllotmentDate" runat="server" ImageUrl="~/IMAGES/calendar.png"
                                    Width="16px" AlternateText="sdfgserafgsdrgsdfg" />--%>
                                    <ajaxToolKit:CalendarExtender ID="ceAllotmentDate" runat="server" Format="dd/MM/yyyy"
                                        PopupButtonID="imgAllotmentDate" TargetControlID="txtAllotmentDate">
                                    </ajaxToolKit:CalendarExtender>
                                    <ajaxToolKit:MaskedEditExtender ID="meAllotmentDate" runat="server" TargetControlID="txtAllotmentDate"
                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                        MaskType="Date" />
                                    <ajaxToolKit:MaskedEditValidator ID="mvAllotmentDate" runat="server" EmptyValueMessage="Please Select Allotment Date"
                                        ControlExtender="meAllotmentDate" ControlToValidate="txtAllotmentDate" IsValidEmpty="false"
                                        InvalidValueMessage="Date is invalid" Display="None" ErrorMessage="Please Select Allotment Date"
                                        InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                            </div>
                        </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Allotment Code </label>
                                </div>
                                  <asp:TextBox ID="txtAllotmentCode" runat="server" TabIndex="10" MaxLength="50" Enabled="false"  CssClass="form-control"/>
                              
                            </div>
                            </div>

                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="Submit"
                             CssClass="btn btn-primary" TabIndex="11" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                             CssClass="btn btn-warning" TabIndex="12" OnClick="btnCancel_Click" />
                        <asp:ValidationSummary ID="valSummary" runat="server" ValidationGroup="Submit"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>

                    <div class="col-12">  
                        <asp:ListView ID="lvAssetAllotment" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblAsset">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Edit
                                                            </th>
                                                            <th>Room Name
                                                            </th>
                                                            <th>Asset Name
                                                            </th>
                                                            <th>Quantity
                                                            </th>
                                                            <th>Allotment Date
                                                            </th>
                                                            <th>Allotment Code
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                        CommandArgument='<%# Eval("ASSET_ALLOTMENT_NO") %>' AlternateText="Edit Record"
                                                        ToolTip="Edit Record" OnClick="btnEdit_Click"  TabIndex="10" />
                                                </td>
                                                <td>
                                                    <%# Eval("ROOM_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ASSET_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("QUANTITY") %>
                                                </td>
                                                <td>
                                                    <%# Eval("ALLOTMENT_DATE", "{0:d}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ALLOTMENT_CODE")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>



</asp:Content>

