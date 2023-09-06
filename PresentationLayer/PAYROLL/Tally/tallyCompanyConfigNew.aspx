<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="tallyCompanyConfigNew.aspx.cs" Inherits="Tally_Transactions_tallyCompanyConfigNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)
        function Cover(bottom, top, ignoreSize) {
            var location = Sys.UI.DomElement.getLocation(bottom);
            top.style.position = 'absolute';
            top.style.top = location.y + 'px';
            top.style.left = location.x + 'px';
            if (!ignoreSize) {
                top.style.height = bottom.offsetHeight + 'px';
                top.style.width = bottom.offsetWidth + 'px';
            }
        }
    </script>
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upDetails"
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


    <asp:UpdatePanel ID="upDetails" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <%--<table cellpadding="0" cellspacing="0" border="0" width="100%" id="tblDetails" runat="server">--%>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Selection Criteria</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Selection Criteria </h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>
                                                <asp:Label ID="Label1" CssClass="control-label" runat="server" Text="Select College"></asp:Label></label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege" Display="None" ErrorMessage="Please Select College" ValidationGroup="Submit"
                                            SetFocusOnError="True" Text="*" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>
                                                <asp:Label ID="lblReceiptBook" CssClass="control-label" runat="server" Text="Select Staff Type"></asp:Label></label>
                                        </div>
                                        <asp:DropDownList ID="ddlstaffType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">

                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvBankCode" runat="server" InitialValue="0" ControlToValidate="ddlstaffType" Display="None" ErrorMessage="Please Select Staff type" ValidationGroup="Submit"
                                            SetFocusOnError="True" Text="*"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>
                                                <asp:Label ID="lblServerName" CssClass="control-label" runat="server" Text="Select Server"></asp:Label></label>
                                        </div>
                                        <asp:DropDownList ID="ddlServer" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvServer" runat="server" ControlToValidate="ddlServer" Display="None" ErrorMessage="Please Select Server IP" ValidationGroup="Submit"
                                            SetFocusOnError="True" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>
                                                <asp:Label ID="lblCompanyName" CssClass="control-label" runat="server" Text="Tally Company Name"></asp:Label></label>
                                        </div>
                                        <asp:TextBox ID="txtTallyCompany" onkeydown="return (event.keyCode!=13);" runat="server" placeholder="Please Enter Tally Company Name" CssClass="form-control" MaxLength="256" TabIndex="3">
                                        </asp:TextBox>
                                        <asp:DropDownList ID="ddlTallyCompany" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AppendDataBoundItems="true" Visible="false" OnSelectedIndexChanged="ddltallCompany_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTallyCompany" Display="None" ErrorMessage="Please Get Company Name" ValidationGroup="Submit"
                                            SetFocusOnError="True" Text="*"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">

                                        <asp:Button ID="btnSearch" TabIndex="4" runat="server" CssClass="btn btn-warning" Text="Get Current Company" OnClick="btnSearch_Click" Font-Bold="true" />

                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>
                                                <asp:Label ID="lblIsActive" CssClass="control-label" runat="server" Text="Active"></asp:Label></label>
                                        </div>
                                        <asp:CheckBox ID="chkIsActive" onkeydown="return (event.keyCode!=13);" runat="server" TabIndex="5" Checked="true" />

                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" TabIndex="6" Text="Submit" ToolTip="Click to Save" ValidationGroup="Submit" CssClass="btn btn-primary progress-button" OnClick="btnSubmit_Click" OnClientClick="if (!Page_ClientValidate('Submit')){ return false; } this.disabled = true; this.value = 'Processing...';" UseSubmitBehavior="false" />

                                <asp:Button ID="btnReport" Visible="false" runat="server" class="btn btn-info" OnClick="btnReport_Click" TabIndex="6" Text="Report" ToolTip="Click to Report" />

                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="btn btn-danger" OnClick="btnCancel_Click" OnClientClick="Cancel()" TabIndex="7" Text="Cancel" ToolTip="Click to Cancel" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            </div>

                            <div class="col-12 mt-2 mb-4">
                                <asp:Panel ID="DivCompany" runat="server" Visible="false">
                                    <asp:ListView ID="Rep_Company" runat="server">
                                        <LayoutTemplate>
                                            <div id="listViewGrid" class="vista-grid">
                                                <div class="titlebar" style="display: none">
                                                    Company Configuration
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>SR.NO.
                                                            </th>

                                                            <th>ACTION
                                                            </th>

                                                            <th>STAFF TYPE
                                                            </th>

                                                            <th>TALLY COMPANY  
                                                            </th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <div id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>

                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </td>

                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" TabIndex="8" Text="EDIT"
                                                        CommandArgument='<%# Eval("PayrollTallyCompanyConfigId") %>' ToolTip="Edit Record"
                                                        ImageUrl="~/Images/edit.png" OnClick="btnEdit_click" />
                                                </td>

                                                <td>

                                                    <%# Eval("StaffType")%>
                                                                        
                                                </td>

                                                <td>

                                                    <strong><%# Eval("TallyCompanyName")%></strong>

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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

