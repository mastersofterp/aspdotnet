<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineAdm_BulkDownload.aspx.cs" Inherits="ACADEMIC_OnlineAdm_BulkDownload"  MasterPageFile="~/SiteMasterPage.master"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <div>
        <asp:UpdateProgress ID="updOA" runat="server" AssociatedUpdatePanelID="updManual"
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
    <script>
        function Validate()
        {
            var alertMsg = "";
            var admBatch = "";
            var degreeType = "";
          
            admBatch = document.getElementById('<%=ddlBatch.ClientID%>').value;
            degreeType = document.getElementById('<%=ddlDegreeType.ClientID%>').value;
            degree = document.getElementById('<%=ddlDegree.ClientID%>').value;
            branch = document.getElementById('<%=ddlBranch.ClientID%>').value;
            if (admBatch == "0" || degreeType == "0") {
                if (admBatch == "0") {
                    alertMsg += "Please select admission batch.\n";
                }
                if (degreeType == "0") {
                    alertMsg += "Please select degree type.\n";
                }
                alert(alertMsg);
                return false;
            }
            else {
                return true;
            }
        }
    </script>
    <asp:UpdatePanel ID="updManual" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblBatch" runat="server" Font-Bold="true" Text="Admission Batch"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlBatch" runat="server" TabIndex="1"  ToolTip="Please select admission batch." AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDegreeType" runat="server" Font-Bold="true" Text="Degree Type"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegreeType" runat="server" TabIndex="1" ToolTip="Please select degree type." AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDegreeType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <asp:Label ID="lblDegree" runat="server" Font-Bold="true" Text="Degree"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="1" ToolTip="Please select degree." AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <asp:Label ID="lblBranch" runat="server" Font-Bold="true" Text="Branch"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="1" ToolTip="Please select branch." AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnExtract" runat="server" Text="Extract Document" OnClick="btnExtract_Click" CssClass="btn btn-primary" TabIndex="1" ToolTip="Click to extract." OnClientClick="return Validate();" />
                                <asp:Button ID="btnPhotoExtract" runat="server" Text="Extract Photo" OnClick="btnPhotoExtract_Click" CssClass="btn btn-primary" TabIndex="1" ToolTip="Click to extract." OnClientClick="return Validate();" />
                                <asp:Button ID="btnSignExtract" runat="server" Text="Extract Sign" OnClick="btnSignExtract_Click" CssClass="btn btn-primary" TabIndex="1" ToolTip="Click to extract." OnClientClick="return Validate();" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="1" ToolTip="Click to cancel." />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExtract" />
            <asp:PostBackTrigger ControlID="btnPhotoExtract" />
            <asp:PostBackTrigger ControlID="btnSignExtract" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>