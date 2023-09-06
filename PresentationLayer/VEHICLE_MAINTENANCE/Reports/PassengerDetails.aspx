<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PassengerDetails.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Reports_PassengerDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <div class="col-md-12 text-center">
        <asp:UpdateProgress ID="GAUpdPro" runat="server">
            <ProgressTemplate>
                <asp:Image ID="imgmoney" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                Loading....
            </ProgressTemplate>
        </asp:UpdateProgress>

    </div>
     <asp:UpdatePanel ID="updApplication" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PASSENGER DETAILS REPORT</h3>
                        </div>
                        <div id="divGR" runat="server">
                            <form role="form">
                                <div class="box-body">
                                    <div class="col-md-12">
                                        <asp:Panel ID="pnlAdd" runat="server">
                                            <div class="panel panel-info">
                                                <div class="panel panel-heading">Passenger Details Report</div>
                                                <div class="panel panel-body">
                                                    <div class="form-group col-md-12">
                                                        <div class="col-md-4">
                                                            <label><span style="color: #FF0000">*</span>College/School Name :</label>
                                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" TabIndex="1" AppendDataBoundItems="true"
                                                                ToolTip="Select College">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege" InitialValue="0"
                                                                Display="None" ValidationGroup="Validate" ErrorMessage="Please Select College " SetFocusOnError="true">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                         <div class="col-md-4">
                                                            <label><span style="color: #FF0000">*</span>Admission Batch :</label>
                                                            <asp:DropDownList ID="ddlBatch" runat="server" CssClass="form-control" TabIndex="2" AppendDataBoundItems="true"
                                                                ToolTip="Select Batch">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvBatch" runat="server" ControlToValidate="ddlBatch" InitialValue="0"
                                                                Display="None" ValidationGroup="Validate" ErrorMessage="Please Select Batch" SetFocusOnError="true">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                       
                                                         <div class="col-md-4">
                                                            <label><span style="color: #FF0000">*</span>Route :</label>
                                                            <asp:DropDownList ID="ddlRoute" runat="server" CssClass="form-control" TabIndex="3" AppendDataBoundItems="true"
                                                                ToolTip="Select Route">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvRoute" runat="server" ControlToValidate="ddlRoute" InitialValue="0"
                                                                Display="None" ValidationGroup="Validate" ErrorMessage="Please Select Route " SetFocusOnError="true">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                       
                                                    </div> 
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                            </form>
                        </div>

                        <div class="box-footer" id="divButton" runat="server">
                            <div class="col-md-12">
                                <p class="text-center">
                                    <asp:Button ID="btnReport" runat="server" Text="Passenger Details" ValidationGroup="Validate"
                                        CssClass="btn btn-primary" TabIndex="4" ToolTip="Click here to Submit" OnClick="btnReport_Click" />
                                      <asp:Button ID="btnBusDetails" runat="server" Text="Bus Details" 
                                        CssClass="btn btn-primary" TabIndex="4" ToolTip="Click here to Submit" OnClick="btnBusDetails_Click"  CausesValidation="false"/>
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        CssClass="btn btn-warning" TabIndex="5" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />                                    
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Validate"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </p>                               
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

