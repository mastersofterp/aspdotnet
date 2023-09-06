<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DecodingGeneration.aspx.cs" Inherits="ACADEMIC_DecodingGeneration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updDCode" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>DECODING NUMBER GENERATION</b></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-md-4" style="display: none;">
                                        <label>Term</label>
                                        <asp:Label ID="lblPreSession" runat="server" Font-Bold="true" />
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AutoPostBack="true" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDept" runat="server" InitialValue="0" SetFocusOnError="true"
                                            ControlToValidate="ddlSession" Display="None" ErrorMessage="Please Select Session."
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rvfDegree" runat="server" InitialValue="0" SetFocusOnError="true"
                                            ControlToValidate="ddlDegree" Display="None" ErrorMessage="Please Select Degree"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server"
                                            AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" InitialValue="0" SetFocusOnError="true"
                                            ControlToValidate="ddlBranch" Display="None" ErrorMessage="Please Select Branch."
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server"
                                            AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" InitialValue="0" SetFocusOnError="true"
                                            ControlToValidate="ddlScheme" Display="None" ErrorMessage="Please Select Scheme"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Course</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server"
                                            AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" SetFocusOnError="true" 
                                            ControlToValidate="ddlCourse" Display="None" ErrorMessage="Please Select Course."
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Digits</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDigits" runat="server" Enabled="false" data-select2-enable="true">
                                            <asp:ListItem Value="5" Selected="True">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rvDigits" runat="server" InitialValue="0" ControlToValidate="ddlDigits"
                                            Display="None" ErrorMessage="Please Select Digits." ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                              <p><asp:Label ID="lblTot" runat="server" Font-Bold="true" />
                                <asp:Label ID="lblAb" runat="server" Font-Bold="true"  CssClass="ml-4"/></p>
                                <asp:Button ID="btnGenNo" runat="Server" Text="Generate Number" CssClass="btn btn-primary"
                                    ValidationGroup="Show" OnClick="btnGenNo_Click" />
                                <asp:Button ID="btnLock" runat="server" Text="Lock" CssClass="btn btn-primary"
                                    ValidationGroup="Show" OnClientClick="return confirmLock();"
                                    OnClick="btnLock_Click" />
                                <asp:Button ID="btnReport" runat="server" Text="Print Decode No" OnClick="btnReport_Click"
                                    CssClass="btn btn-info" ValidationGroup="Show" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvDecodeNo" runat="server"
                                        OnItemDataBound="lvDecodeNo_ItemDataBound">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Decoding Number Generation List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Enrollment No.
                                                        </th>
                                                        <th>Roll No.
                                                        </th>
                                                        <th>Decode No.
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Eval("REGNO") %>
                                                    <asp:HiddenField ID="hdfAB" runat="server" Value='<%# Eval("EXTERMARK") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("ROLL_NO") %>
                                                </td>
                                                <td>
                                                    <%# Eval("DECODENO")%>
                                                    <asp:Label ID="lblAB" runat="server" />
                                                    <asp:Label runat="server" ID="lblABP" Text=' <%# Eval("EXTERMARK") %> '> <%# Eval("EXTERMARK") %> </asp:Label>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server" />

    <script language="javascript" type="text/javascript">

        function confirmLock() {
            var ret = confirm('Do you want to Lock Decode Nos. for Current Selecttion.');
            return ret;
        }
    </script>

</asp:Content>

