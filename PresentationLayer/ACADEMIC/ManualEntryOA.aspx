<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManualEntryOA.aspx.cs" Inherits="ACADEMIC_ManualEntryOA" Title="" MasterPageFile="~/SiteMasterPage.master" %>

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
        function validate() {
            var appId = document.getElementById('<%=txtAppId.ClientID%>');
            if (appId.value == "") {
                alert("Please enter application Id.");
                document.getElementById('<%=txtAppId.ClientID%>').focus();
                return false;
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
                                            <asp:Label ID="lblAppId" runat="server" Font-Bold="true" Text="Application Id"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtAppId" runat="server" TabIndex="1" CssClass="form-control" ToolTip="Please enter application Id." AutoComplete="off" MaxLength="10"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="txtValidate" runat="server" TargetControlID="txtAppId" FilterMode="InvalidChars" InvalidChars="~`!@#$%^&*()-_+={[}]|\:;'<,>.?/"></ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <asp:Button ID="btnSearch" runat="server" TabIndex="1" CssClass="btn btn-primary" ToolTip="Click to search" Text="Search" OnClick="btnSearch_Click" Style="margin-top: 15px" OnClientClick="return validate();"></asp:Button>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" id="divDetails" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12  col-12">
                                        <ul class="list-group list-group-unbordered ipad-view">
                                            <li class="list-group-item"><b>Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Email Id :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblEmail" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Mobile No :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblMobile" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Degree :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Total Amount :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblFees" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Payment Status :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblPayStatus" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Visible="false" Text="Manual Entry Payment" CssClass="btn btn-primary" OnClick="btnSubmit_Click" OnClientClick="return confirm('Click OK to make manual payment entry.');" />
                                 <asp:Button ID="btnreceipt" runat="server"  Text="Recipt" CssClass="btn btn-primary" OnClick="btnreceipt_Click" Visible="false" />
                                  <asp:Button ID="btnCancel" runat="server" Visible="false" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>

             <div id="divMsg" runat="server"></div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnreceipt" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
           
        </Triggers>
    </asp:UpdatePanel>
   
</asp:Content>
