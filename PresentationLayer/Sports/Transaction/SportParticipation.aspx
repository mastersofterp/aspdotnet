<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="SportParticipation.aspx.cs" Inherits="Sports_Transaction_SportParticipation"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div class="loader-container">
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__ball"></div>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <!----done by ashwini 02-03-2022---->

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SPORTS PARTICIPATION</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="pnlDesig" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Participation Status</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStatus" CssClass="form-control" data-select2-enable="true" ToolTip="Select Participation Status" runat="server" AppendDataBoundItems="true" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Inter</asp:ListItem>
                                                <asp:ListItem Value="2">Intra</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ErrorMessage="Please Select Participation Status."
                                                ControlToValidate="ddlStatus" InitialValue="0" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Event Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlEventType" runat="server" AppendDataBoundItems="true" TabIndex="2" CssClass="form-control" data-select2-enable="true" ToolTip="Select Event Type" AutoPostBack="true" OnSelectedIndexChanged="ddlEventType_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvET" runat="server" ControlToValidate="ddlEventType" Display="None"
                                                ErrorMessage="Please Select Event Type." InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Event Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlEvent" CssClass="form-control" data-select2-enable="true" ToolTip="Select Event" runat="server" AppendDataBoundItems="true"
                                                AutoPostBack="True" InitialValue="0" OnSelectedIndexChanged="ddlEvent_SelectedIndexChanged" TabIndex="3">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvEvent" runat="server" ErrorMessage="Please Select Event Name."
                                                ControlToValidate="ddlEvent" InitialValue="0" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Sport Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSportName" CssClass="form-control" data-select2-enable="true" ToolTip="Select Sport" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlSportName_SelectedIndexChanged" TabIndex="4">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSportName" runat="server" ErrorMessage="Please Select Sport Name."
                                                ControlToValidate="ddlSportName" InitialValue="0" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Team Name </label>
                                            </div>
                                            <asp:DropDownList ID="ddlTeam" CssClass="form-control" data-select2-enable="true" ToolTip="Select Team" runat="server" AutoPostBack="True" AppendDataBoundItems="True" TabIndex="5"
                                                OnSelectedIndexChanged="ddlTeam_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvTeam" runat="server" ErrorMessage="Please Select Team Name."
                                                ControlToValidate="ddlTeam" InitialValue="0" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Result</label>
                                            </div>

                                            <asp:DropDownList ID="ddlResult" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select Result" AppendDataBoundItems="True" TabIndex="6">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvVenue" runat="server" ErrorMessage="Please Select Result."
                                                ControlToValidate="ddlResult" InitialValue="0" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Description</label>
                                            </div>
                                            <asp:TextBox ID="txtDescription" runat="server" MaxLength="60" CssClass="form-control" ToolTip="Select Description" Height="35px" TextMode="MultiLine" TabIndex="7"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="Please Enter Description."
                                                ControlToValidate="txtDescription" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup> </sup>
                                                <label>Attach File</label>
                                            </div>
                                             <asp:FileUpload ID="FileUpload1" runat="server" ValidationGroup="submit" ToolTip="Select file to upload" TabIndex="8" />
                                        </div>
                                        </div>
                                    <div class="col-12 btn-footer">
                                           
                                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" TabIndex="9" CausesValidation="False" CssClass="btn btn-outline-info" ToolTip="Click here to Add" />
                                        </div>
                                </asp:Panel>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" CausesValidation="true" OnClick="btnSubmit_Click" Text="Submit" TabIndex="10" ValidationGroup="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                <asp:Button ID="btnReport" runat="server" CausesValidation="false" OnClick="btnReport_Click" Text="Report" TabIndex="12" CssClass="btn btn-info" ToolTip="Click to get Report" />
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="false" OnClick="btnCancel_Click" Text="Cancel" TabIndex="11" CssClass="btn btn-warning" ToolTip="Click here to Cancel" />


                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvPlayer" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>LIST OF PLAYERS AND STAFF MEMBERS</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Player Name
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Eval("PLAYERNAME")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlFile" runat="server" Visible="false">
                                    <asp:ListView ID="lvfile" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Download files</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action
                                                            </th>
                                                            <th>File Name
                                                            </th>
                                                            <th>Download
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%#DataBinder.Eval(Container, "DataItem") %>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                        OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" />
                                                </td>
                                                <td>
                                                    <%#GetFileName(DataBinder.Eval(Container, "DataItem")) %>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imgFile" runat="Server" ImageUrl="~/Images/action_down.png"
                                                        AlternateText='<%#DataBinder.Eval(Container, "DataItem") %>' ToolTip='<%#DataBinder.Eval(Container, "DataItem")%>'
                                                        OnClick="imgdownload_Click" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </asp:Panel>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlListView" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvSportParticipation" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>SPORT PARTICIPATION LIST</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit
                                                            </th>
                                                            <th>Inter / Intra
                                                            </th>
                                                            <th>Event Name
                                                            </th>
                                                            <th>Team Name
                                                            </th>
                                                            <th>Sport Name
                                                            </th>
                                                            <th>Result
                                                            </th>
                                                            <%--<th>Print
                                                        </th>--%>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                        CommandArgument='<%# Eval("PARTICID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <%# Eval("NITSTATUS")%>
                                                </td>
                                                <td>
                                                    <%# Eval("EVENTNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TEAMNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("RESULT")%>
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
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="lvfile" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
