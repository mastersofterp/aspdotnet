<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="maingroup.aspx.cs" Inherits="Account_maingroup" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            text-align: right;
        }
    </style>

    <link href="../plugins/multi-select/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../plugins/multi-select/bootstrap-multiselect.js"></script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UPDMainGroup"
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
    <asp:UpdatePanel ID="UPDMainGroup" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="divMsg" runat="server">
                            <div id="div3" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">FINAL ACCOUNT GROUP</h3>
                            </div>
                            <div class="box-body">
                                <div id="divCompName" runat="server"  style="text-align: center; font-size:x-large;"></div>
                                <asp:Panel ID="pnl" runat="server">
                                    <%-- <div class="col-md-8">--%>
                                    <div class="col-12 mt-2">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Group Name</label>
                                                </div>
                                                <asp:TextBox ID="txtGroupName" runat="server" enableSelection="Yes" Style="text-transform: uppercase"
                                                    Text="" ToolTip="Please Enter Group Name" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvGroupName" runat="server" ControlToValidate="txtGroupName"
                                                    Display="None" ErrorMessage="Please Enter Group Name" SetFocusOnError="True"
                                                    ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Parent Group</label>
                                                </div>
                                                <asp:DropDownList ID="ddlParentGroup" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlParentGroup_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Final Account Head</label>
                                                </div>
                                                <asp:DropDownList ID="ddlFAHead" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                    OnSelectedIndexChanged="ddlFAHead_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Account Code</label>
                                                </div>
                                                <asp:TextBox ID="txtAccCode" Style="text-transform: uppercase" ToolTip="Please Enter Account Code"
                                                    runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Label ID="lblStatus" runat="server" SkinID="lblmsg"></asp:Label>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                            CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                            CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                                    </div>

                                    <div class="col-12 mb-5">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Search Criteria</label>
                                                </div>
                                                <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True" OnTextChanged="txtSearch_TextChanged"
                                                    Text="" ToolTip="Please Enter Group Name" CssClass="form-control"></asp:TextBox>

                                            <div class="mt-2">
                                                <asp:ListBox ID="lstGroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstGroup_SelectedIndexChanged"
                                                    Rows="10" CssClass="form-control" Style="text-align: left; height:350px!important;"></asp:ListBox>
                                            </div>
                                                </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:Panel ID="panel2" runat="server" ScrollBars="Both" Height="400px" BorderStyle="Outset">
                                                    <asp:TreeView ID="tvHierarchy" runat="server" ImageSet="Arrows">
                                                    </asp:TreeView>
                                                </asp:Panel>
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
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="tvHierarchy" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

    </script>
</asp:Content>
