<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ClearHostelAllotment.aspx.cs" Inherits="Clear_Hostel_Allotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">YEAR END PROCESS</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel Session</label>
                                </div>
                                <asp:DropDownList ID="ddlHostelSessionNo" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True"
                                    TabIndex="1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvHostelSessionNo" runat="server" ErrorMessage="Please Select Hostel Session"
                                    ControlToValidate="ddlHostelSessionNo" Display="None" InitialValue="0" ValidationGroup="Submit" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel Name</label>
                                </div>
                                <asp:DropDownList ID="ddlHostelNo" runat="server" CssClass="form-control"
                                    AppendDataBoundItems="True" AutoPostBack="True" data-select2-enable="true" OnSelectedIndexChanged="ddlHostelNo_SelectedIndexChanged" TabIndex="2">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvHostelNo" runat="server" ErrorMessage="Please Select Hostel Name"
                                    ControlToValidate="ddlHostelNo" Display="None" InitialValue="0" ValidationGroup="Submit" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered mt-3">
                                    <li class="list-group-item"><b>Total Record :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lbltotalrec" Text="0" runat="server" Font-Bold="true"></asp:Label>
                                        </a>
                                    </li>
                                </ul>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered mt-3">
                                    <li class="list-group-item"><b>Total Backup Record :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lbltotBKrecord" Text="0" runat="server" Font-Bold="true"></asp:Label>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Process Data" CssClass="btn btn-primary" ToolTip="Submit Fine Student"
                            TabIndex="3" ValidationGroup="Submit" OnClientClick="return ConfirmDelete();" OnClick="btnSubmit_Click" />

                        <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancel"
                            CssClass="btn btn-warning" TabIndex="4" OnClick="btnCancel_Click" />
                        <asp:ValidationSummary ID="vsStudent" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Submit" />

                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="Add" />

                    </div>

                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
    <script type="text/javascript" language="javascript">
        function ConfirmDelete() {
            var ret = confirm('Do You Want To Clear Hostel Room Allotment ???');
            if (ret == true) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>

