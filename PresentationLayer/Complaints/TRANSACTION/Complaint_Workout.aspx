<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Complaint_Workout.aspx.cs"
    Inherits="Estate_Complaint_Workout" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SERVICE REQUEST WORKOUT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Service Request Workout
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnlworkout" runat="server">
                                                <div class="form-group row">
                                                    <div class="col-md-2">
                                                        <label>Service No:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblCompNo" runat="server" Font-Bold="True"></asp:Label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>Employee:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblEmployee" runat="server" Font-Bold="True"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-2">
                                                        <label>Workout Date<span style="color: red;">*</span>:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" />
                                                            </div>
                                                            <asp:TextBox ID="txtWorkoutDate" runat="server" CssClass="form-control" disabled="true"></asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtWorkoutDate" PopupButtonID="Image1">
                                                            </ajaxToolKit:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>Service Allotted By:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblAllotterName" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-2">
                                                        <label>Service Location:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblLocation" runat="server" Text=""></asp:Label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>Service Contact No.:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblContactNo" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-2">
                                                        <label>Service Details<span style="color: red;">*</span>:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtcomplaintsdt" MaxLength="350" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine" />
                                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDetails"
                                                                        Display="None" ErrorMessage="Please Enter Complaint Details." ValidationGroup="complaint"></asp:RequiredFieldValidator>   --%>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>Action Taken<span style="color: red;">*</span>:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtDetails" MaxLength="3000" runat="server"
                                                            CssClass="form-control" TextMode="MultiLine" />
                                                        <asp:RequiredFieldValidator ID="rfvDetails" runat="server" ControlToValidate="txtDetails" Display="None" ErrorMessage="Please Enter Workout Details."
                                                            ValidationGroup="complaint"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-2">
                                                        <label>Service Status<span style="color: red;">*</span>:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButton ID="rdocomp1" runat="server" GroupName="rdocomp" Text="Incomplete" Checked="true" />
                                                        <asp:RadioButton ID="rdocomp2" runat="server" GroupName="rdocomp" Text="Completed" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>Add Items:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:CheckBox ID="chkAddItem" runat="server" AutoPostBack="true" ToolTip="Click Here To Add Items"
                                                            OnCheckedChanged="chkAddItem_CheckedChanged" />
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                            <asp:Panel ID="PnlAddDetails" runat="server" Visible="true">
                                                <div class="form-group row">
                                                    <div class="col-md-2">
                                                        <label>Service Type:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="ddlRMItemType" AppendDataBoundItems="true"
                                                            runat="server" CssClass="form-control"
                                                            OnSelectedIndexChanged="ddlRMItemType_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvItemType" runat="server"
                                                            ControlToValidate="ddlRMItemType" Display="None" ErrorMessage="Please Select Complaint Type."
                                                            InitialValue="-1" ValidationGroup="complaint"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>Item Name<span style="color: red;">*</span>:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="ddlRMItemName" AppendDataBoundItems="true"
                                                            runat="server" CssClass="form-control"
                                                            OnSelectedIndexChanged="ddlRMItemName_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvItemName" runat="server"
                                                            ControlToValidate="ddlRMItemName" Display="None" ErrorMessage="Please Select Item Name."
                                                            InitialValue="-1" ValidationGroup="complaint"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-2">
                                                        <label>Item Unit:</label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtItemUnit" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>Quantity Issued<span style="color: red;">*</span>:</label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtQtyIssued" runat="server" CssClass="form-control" MaxLength="3"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvQtyIssued" runat="server"
                                                            ControlToValidate="txtQtyIssued" ValidationGroup="complaint" Display="None" ErrorMessage="Please Enter Quantity Issued."></asp:RequiredFieldValidator>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeType" runat="server" TargetControlID="txtQtyIssued" FilterType="Numbers"></ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        


                                            <asp:Panel ID="Pnlbutton" runat="server">
                                                <div class="form-group row">
                                                    <div class="col-md-12 text-center">
                                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="complaint" OnClick="btnSave_Click" CssClass="btn btn-primary" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="complaint" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-12">
                                                        <asp:Label ID="LableErr" SkinID="Errorlbl" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <asp:Panel ID="pnlworkoutdetails" runat="server">
                                                        <asp:ListView ID="lvWorkoutDetails" runat="server">
                                                            <LayoutTemplate>
                                                                <div id="lgv1">
                                                                    <h4 class="box-title">SERVICE ACTION TAKEN DETAILS</h4>
                                                                    <table class="table table-bordered table-hover">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <th>ACTION TAKEN DATE</th>
                                                                                <th>ITEM TYPE</th>
                                                                                <th>ITEM NAME</th>
                                                                                <th>ITEM UNIT</th>
                                                                                <th>QTY ISSUED</th>
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
                                                                    <td><%# Eval("WORKDATE","{0:dd-MMM-yyyy}")%></td>
                                                                    <td><%# Eval("ITEMTYPE")%></td>
                                                                    <td><%# Eval("ITEMNAME")%></td>
                                                                    <td><%# Eval("ITEMUNIT")%></td>
                                                                    <td><%# Eval("QTYISSUED")%></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                        <%-- <div class="vista-grid_datapager">
                                        <asp:DataPager ID="dpPagerWorkout" runat="server" PagedControlID="lvWorkoutDetails" PageSize="20" OnPreRender="dpPagerWorkout_PreRender">
                                            <Fields>
                                                <asp:NumericPagerField ButtonCount="3" ButtonType="Link" />
                                            </Fields>
                                        </asp:DataPager>                                
                                 </div>--%>
                                                    </asp:Panel>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <asp:Panel ID="pnlList" runat="server">
                                                        <asp:ListView ID="lvComplaintDetails" runat="server">
                                                            <EmptyDataTemplate>
                                                                <br />
                                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Service Request To Workout" />
                                                            </EmptyDataTemplate>
                                                            <LayoutTemplate>
                                                                <div id="lgv1">
                                                                    <h4 class="box-title">SERVICE DETAILS</h4>
                                                                    <table class="table table-bordered table-hover">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <th>ACTION</th>
                                                                                <th>SERVICE NO.</th>
                                                                                <th>DATE</th>
                                                                                <th>REQUESTER</th>
                                                                                <th>STATUS</th>
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
                                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("COMPLAINTID")%>' AlternateText="Edit Record" ToolTip="Work Out" OnClick="btnEdit_Click" />&nbsp;
                                                                    </td>
                                                                    <td><%# Eval("COMPLAINTID")%></td>
                                                                    <td><%# Eval("COMPLAINTDATE","{0:dd-MMM-yyyy}")%></td>
                                                                    <td><%# Eval("COMPLAINT")%></td>
                                                                    <td><%# Eval("COMPLAINTSTATUS")%></td>

                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                        <%--<div class="vista-grid_datapager">
                                        <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvComplaintDetails" PageSize="20" OnPreRender="dpPager_PreRender">
                                            <Fields>
                                                <asp:NumericPagerField ButtonCount="3" ButtonType="Link" />
                                            </Fields>
                                        </asp:DataPager>                                
                                 </div>--%>
                                                    </asp:Panel>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
