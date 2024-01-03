<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DataValidation.aspx.cs" Inherits="ACADEMIC_DataValidation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
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


    <%-- <asp:UpdatePanel ID="updValidation" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblcPageTitle" runat="server">Data Sanity Check</asp:Label>
                    </h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="form-group col-lg-3 col-md-6 col-12">
                            <%-- <sup>*</sup>--%>
                            <%--<label></label>--%>

                            <asp:RadioButtonList ID="rdoDataValidate" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="True" OnSelectedIndexChanged="rdoPurpose_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="1">Student Wise Query</asp:ListItem>
                                <asp:ListItem Value="2">Session Wise Query</asp:ListItem>
                            </asp:RadioButtonList>
                            <br />
                            <div class="form-group col-lg-12 col-md-6 col-12" id="dvSession" runat="server">
                                <div class="label-dynamic" id="divsession">
                                    <sup id="supsession" runat="server">* </sup>
                                    <asp:Label ID="lblSession" runat="server" Font-Bold="true">Session</asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Session."  OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                    CssClass="form-control" data-select2-enable="true" ValidationGroup="submit">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-12 btn-footer" runat="server" visible="false">
                            <asp:Button ID="btnCheckHealth" runat="server" Text="Check System Health" TabIndex="3" OnClientClick="return validateCheckBoxes();"
                                ToolTip="Show " OnClick="btnCheckHealth_Click" ValidationGroup="show" CssClass="btn btn-primary" Visible="false" />
                        </div>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="show" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>
                </div>

                <asp:ListView ID="Lvdatav" runat="server" EnableModelValidation="True">
                    <EmptyDataTemplate>
                        <div>
                            -- No Student Record Found --
                        </div>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                        <div class="sub-heading">
                            <h5>Search List</h5>
                        </div>
                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblCheckSanityHealth">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th>Check</th>
                                    <th>PURPOSE</th>
                                    <th>ENTRY COUNT</th>
                                    <th>STATUS </th>
                                    <th>Download</th>
                                    <%-- <th>Last Check-In</th>--%>
                                    <th>Last Date of Sanity Check</th>
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
                                <asp:CheckBox ID="CheckDV" runat="server" ToolTip='<% #Eval("ID")%>' Enabled='<%# (Convert.ToInt32(Eval("ENTRY COUNT") )> 0 ? true : false )%>' /></td>

                            <asp:HiddenField ID="hdid" runat="server" Value='<% #Eval("ID")%>' />

                            <td>
                                <asp:Label ID="lblPurpose" runat="server" Text='<% #Eval("PURPOSE")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblEntryCount" runat="server" Text='<% #Eval("ENTRY COUNT")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCheck" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnDownload" runat="server" ImageUrl="~/Images/downarrow.jpg" ToolTip="Download Excel" OnClick="btnDownload_Click" Style="width: 15px; height: 15px; margin-top: -55px" CommandArgument='<%# Eval("ID") %>' /></td>
                            <td>
                                <asp:Label ID="lblCurrentd" runat="server" Text='<% #Eval("LAST_CHECKIN")%>'></asp:Label></td>
                            <%----%>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>

    </div>

    <script type="text/javascript">
        function validateCheckBoxes() {
            var count = 0;
            var numberOfChecked = $('[id*=tblCheckSanityHealth] td input:checkbox:checked').length;
            if (numberOfChecked == 0) {
                alert("Please select atleast one Purpose.");
                return false;
            }
            else
                return true;
        }
    </script>



    <%--  </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnCheckHealth" />
            <asp:PostBackTrigger ControlID="btnDownload" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>

