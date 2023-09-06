<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="dashboard_configuration.aspx.cs" Inherits="dashboard_configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upd1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SESSION CREATION</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
	                                    <div class="label-dynamic">
		                                    <sup>* </sup>
		                                    <label>User Type</label>
	                                    </div>
                                        <asp:DropDownList ID="ddlusertype" runat="server" AppendDataBoundItems="true" CssClass="form-control" OnSelectedIndexChanged="ddlusertype_SelectedIndexChanged" data-select2-enable="true"
                                            TabIndex="1" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvusertype" runat="server" ControlToValidate="ddlusertype"
                                            Display="None" ErrorMessage="Please select UserType." InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="show" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
	                                    <div class="label-dynamic">
		                                    <sup>* </sup>
		                                    <label>DashBoard</label>
	                                    </div>
                                         <div class="form-group">
                                            <div class="well" style="max-height: 200px; overflow: auto;min-height: 34px;">
                                                <ul id="check-list-box" class="list-group checked-list-box">
                                                    <li class="list-group-item">
                                                        <asp:CheckBoxList ID="chkdashboard" runat="server" AppendDataBoundItems="true" TabIndex="1" BorderStyle="None">
                                                        </asp:CheckBoxList></li>
                                                </ul>
                                            </div>
                                        </div>
                                        <%-- <asp:DropDownList ID="ddldashboard" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                            TabIndex="3" AutoPostBack="true" >
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                               </asp:DropDownList>
                                          <asp:RequiredFieldValidator ID="rfvdashboard" runat="server" ControlToValidate="ddldashboard"
                                            Display="None" ErrorMessage="Please select Dashboard." InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="show" />--%>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnsubmit_Click" ValidationGroup="show" TabIndex="1"/>
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="1"/>
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="show" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

