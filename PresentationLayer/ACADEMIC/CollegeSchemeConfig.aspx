<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CollegeSchemeConfig.aspx.cs" Inherits="ACADEMIC_CollegeSchemeConfig" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        table.cbl tr td label
        {
            margin-left: 5px;
            margin-right: 20px;
        }

        table.cbl tr td
        {
            pading-bottom: 10px;
        }
    </style>

    <%-- <div style="z-index: 1; position: absolute; top: 75px; left: 600px;">
        <asp:UpdateProgress ID="upupdDetained" runat="server" AssociatedUpdatePanelID="updPnl">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>--%>

    <div>
        <asp:UpdateProgress ID="upupdDetained" runat="server" AssociatedUpdatePanelID="updPnl"
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

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <%--<h3 class="box-title">College Regulation Configuration</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                    <%-- <div class="pull-right">
                        <div style="color: Red; font-weight: bold">Note : * Marked fields are Mandatory</div>
                    </div>--%>
                </div>
                <asp:UpdatePanel ID="updPnl" runat="server">
                    <ContentTemplate>
                        <div class="box-body">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <%--<label>College</label>--%>
                                    <asp:Label ID="lblDYddlSchool" Font-Bold="true" runat="server"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" />
                                <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege" ValidationGroup="submit" Display="None"
                                    ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="true" />
                            </div>


                            <div class="form-group col-md-12" id="divDegree" runat="server" visible="false">
                                <%--<legend class="legendPay" style="font-size: 16px"><span style="color: red;">* </span><b>Degree</b></legend>--%>
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Degree</label>
                                </div>
                                <asp:CheckBoxList ID="cblDegree" runat="server" AppendDataBoundItems="true" RepeatDirection="Horizontal" Style="margin-right: 5px"
                                    OnSelectedIndexChanged="cblDegree_SelectedIndexChanged" CssClass="cbl" CellPadding="5" RepeatLayout="Table" AutoPostBack="true">
                                </asp:CheckBoxList>
                            </div>

                            <div class="form-group col-md-12" id="divBranch" runat="server" visible="false">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Branch</label>
                                </div>
                                <asp:CheckBoxList ID="cblBranch" runat="server" AppendDataBoundItems="true" RepeatDirection="Horizontal" Style="margin-right: 5px"
                                    OnSelectedIndexChanged="cblBranch_SelectedIndexChanged" CssClass="cbl" CellPadding="5" RepeatLayout="Table" RepeatColumns="4"
                                    AutoPostBack="true">
                                </asp:CheckBoxList>
                            </div>

                            <div class="form-group col-md-12" id="divScheme" runat="server" visible="false">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Regulation/Scheme</label>
                                </div>
                                <asp:CheckBoxList ID="cblScheme" runat="server" AppendDataBoundItems="true" RepeatDirection="Horizontal" Style="margin-right: 5px"
                                    OnSelectedIndexChanged="cblScheme_SelectedIndexChanged" CssClass="cbl" CellPadding="5" RepeatLayout="Table" RepeatColumns="3"
                                    AutoPostBack="true">
                                </asp:CheckBoxList>
                            </div>

                        </div>

                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="submit" Enabled="false" CssClass="btn btn-primary" OnClientClick="return UserConfirmation();" />
                                <asp:Button ID="btnCancel" runat="server" Text="Clear" OnClick="btnCancel_Click" CssClass="btn btn-warning" />

                                <asp:ValidationSummary ID="vsReport" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                                <div id="divMsg" runat="server"></div>
                                <p>
                                </p>
                            </p>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <script type="text/javascript" language="javascript">
        function UserConfirmation() {
            if (confirm("Are you sure you want to Submit?"))
                return true;
            else
                return false;
        }
    </script>

</asp:Content>
