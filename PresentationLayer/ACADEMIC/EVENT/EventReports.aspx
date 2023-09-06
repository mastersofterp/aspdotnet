<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EventReports.aspx.cs" Inherits="ACADEMIC_EVENT_EventReports" MasterPageFile="~/SiteMasterPage.master" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
    <div>
        <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="updRpt">
            <ProgressTemplate>
            <progresstemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </progresstemplate>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
       
    <asp:UpdatePanel ID="updRpt" runat="server">
        <ContentTemplate>

             <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Event Reports</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                  <div class="row">
                                      <div class="col-lg-3 col-md-6 col-12 form-group">
                                        <span style="color: red">* </span>
                                        <label>Event Type</label>
                                        <asp:DropDownList ID="ddlEventType" runat="server" TabIndex="1" CssClass="form-control" AppendDataBoundItems="true" ValidationGroup="Show" AutoPostBack="true" OnSelectedIndexChanged="ddlEventType_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvEvent" runat="server" ControlToValidate="ddlEventType" ValidationGroup="Show" Display="None" ErrorMessage="Please Select Event Type" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12ss form-group">
                                        <label>Event Title</label>
                                        <asp:DropDownList ID="ddlEventTitle" runat="server" CssClass="form-control" TabIndex="2" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlEventTitle_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList> 
                                    </div>
                                  </div>
                                </div>
                               <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" TabIndex="3" Text="Show List of Events" ValidationGroup="Show" OnClick="btnShow_Click" />
                                    <asp:Button ID="btnParticipantDetails" runat="server" CssClass="btn btn-primary" TabIndex="3" Text="Show Participant List" ValidationGroup="Show" OnClick="btnParticipantDetails_Click" />
                                <asp:Button ID="btnParticipant" runat="server" CssClass="btn btn-primary" TabIndex="4" Text="Participant List(Excel)" ValidationGroup="Show" OnClick="btnParticipant_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" TabIndex="5" Text="Cancel" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="vsShow" runat="server" ValidationGroup="Show" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="pnlEvent" runat="server" Visible="false">
                                        <asp:ListView ID="lvEventDetails" runat="server" Visible="false">
                                            <LayoutTemplate>
                                                <div>
                                                    <h3><div class="label label-default">Event Details List</div></h3>
                                                    <table class="table table-striped table-bordered nowrap display" id="tbllist">
                                                        <thead>
                                                            <tr class="bg-light-blue" >
                                                                <th>Sr No.</th>
                                                                <th>Event Title
                                                                </th>                                                                
                                                                <th>Event Start Date
                                                                </th>
                                                                <th>Event End Date
                                                                </th>
                                                                <th>Event Registration Start Date
                                                                </th>
                                                                <th>Event Registration End Date
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
                                                        <%#Container.DataItemIndex+1 %>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblEventTitle" runat="server" Text='<%# Eval("EVENT_TITLE") %>'></asp:Label>
                                                    </td>                                                    
                                                    <td>
                                                        <asp:Label ID="lblEventStart" runat="server" Text='<%# Eval("EVENT_START_DATE") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblEventEnd" runat="server" Text='<%# Eval("EVENT_END_DATE") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblEventRegStart" runat="server" Text='<%# Eval("EVENT_REG_START_DATE") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblEventRegEnd" runat="server" Text='<%# Eval("EVENT_REG_END_DATE") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="pnlParticipant" runat="server" Visible="false">
                                        <asp:ListView ID="lvParticipant" runat="server" Visible="false">
                                            <LayoutTemplate>
                                                <div>
                                                    <h3><div class="label label-default">Participant Details List</div></h3>
                                                    <table class="table table-hover table-bordered" id="tbllist">
                                                        <thead>
                                                            <tr class="bg-light-blue" >
                                                                <th>Sr No.</th>
                                                                <th>
                                                                    Event Type
                                                                </th>
                                                                <th>Event Title
                                                                </th>                                                                
                                                                <th>Candidate Name
                                                                </th>
                                                                <th>Mobile No.
                                                                </th>
                                                                <th>Email Id
                                                                </th>
                                                                <th>Gender
                                                                </th>
                                                                <th>
                                                                    Participant Type
                                                                </th>
                                                                <th>
                                                                    State
                                                                </th>
                                                                <th>
                                                                    City
                                                                </th>
                                                                <th>
                                                                    Event Amount
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
                                                        <%#Container.DataItemIndex+1 %>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblEventType" runat="server" Text='<%# Eval("EVENT_TYPE") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblEventTitle" runat="server" Text='<%# Eval("EVENT_TITLE") %>'></asp:Label>
                                                    </td>                                                    
                                                    <td>
                                                        <asp:Label ID="lblCandidateName" runat="server" Text='<%# Eval("CANDIDATE_NAME") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("MOBILE_NO") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("EMAIL_ID") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblGender" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                                                    </td>
                                                     <td>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("PARTICIPANT_TYPE") %>'></asp:Label>
                                                    </td>
                                                     <td >
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("STATENAME") %>'></asp:Label>
                                                    </td>
                                                     <td>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("CITY") %>'></asp:Label>
                                                    </td>
                                                     <td>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("EventAmount") %>'></asp:Label>
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
            <asp:PostBackTrigger ControlID="btnParticipant" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

