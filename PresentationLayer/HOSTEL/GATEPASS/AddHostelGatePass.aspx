<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AddHostelGatePass.aspx.cs" Inherits="HOSTEL_GATEPASS_AddHostelGatePass" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };

        onkeypress = "return CheckAlphabet(event,this);"
        function CheckAlphabet(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;
            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }

        function formatNumber(input) {
            var value = input.value;
            if (value < 10) {
                input.value = '0' + value;
            }
        }
    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">ADD E-GATE PASS</h3>
                </div>
                <br />
                <div class="box-body">
                    <div class="col-6">
                        <div class="form-group col-lg-8 col-md-4 col-12">
                             <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Pass Number</label>
                                </div>
                                <asp:TextBox ID="txtPass" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPass" runat="server" ErrorMessage="Please Enter Pass Number"
                                    Display="None" ControlToValidate="txtPass" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                         </div>
                    </div>
                    <br />
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="submit" TabIndex="12"
                            CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnReport" runat="server" Text="Gate Pass Report" ValidationGroup="submit" TabIndex="12"
                            CssClass="btn btn-primary" Visible="false" OnClick="btnReport_Click"/>
                        &nbsp;<asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="submit" />
                    </div>
                </div>
            </div>
            <br />
            <asp:Panel ID="pnlinfo" runat="server" Visible="False">
            <div class="box box-primary">
                <div id="div2" runat="server" style="text-align:center;padding:9px;" >
                    <asp:Image ID="imgPhoto" runat="server" Height="120px" Width="128px" style="border-radius:50%;"/>
                </div>
                <div id="div3" runat="server" style="text-align:center;" >
                    <b><asp:Label ID="lblName" runat="server">Name</asp:Label></b>
                </div>
                <br />
                <div class="container">
                <div class="box-body">
                    <div class="col-6">
                        <div class="form-group col-lg-8 col-md-4 col-12">
                             <div class="label-dynamic">
                                    <label style="font-size:medium;"><b>Admission Number</b></label>
                                </div>
                                <asp:Label ID="lbladno" runat="server" Text="Label"></asp:Label>
                         </div>
                        <div class="form-group col-lg-8 col-md-4 col-12">
                             <div class="label-dynamic">
                                    <label style="font-size:medium;"><b>Hostel Name</b></label>
                                </div>
                                <asp:Label ID="lblhostel" runat="server" Text="Label"></asp:Label>
                         </div>
                        <div class="form-group col-lg-8 col-md-4 col-12">
                             <div class="label-dynamic">
                                    <label style="font-size:medium;"><b>Room No</b></label>
                                </div>
                                <asp:Label ID="lblRoom" runat="server" Text="Label"></asp:Label>
                         </div>
                        <div class="form-group col-lg-8 col-md-4 col-12">
                             <div class="label-dynamic">
                                    <label style="font-size:medium;"><b>Valid From</b></label>
                                </div>
                                <asp:Label ID="lblvalFrom" runat="server" Text="Label"></asp:Label>
                         </div>
                        <div class="form-group col-lg-8 col-md-4 col-12">
                             <div class="label-dynamic">
                                    <label style="font-size:medium;"><b>Valid To</b></label>
                                </div>
                                <asp:Label ID="lblvalTo" runat="server" Text="Label"></asp:Label>
                         </div>
                        <div class="form-group col-lg-8 col-md-4 col-12">
                             <div class="label-dynamic">
                                    <label style="font-size:medium;"><b>Approved By & Date</b></label>
                                </div>
                                <asp:Label ID="lblapproval" runat="server" Text="Label"></asp:Label>
                         </div>
                    </div>
                </div>
                </div>
            </div>
           </asp:Panel>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .form-control {}
    </style>
</asp:Content>



