<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ClubRegistrationApproval.aspx.cs" Inherits="ClubRegistrationApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <link href="../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
     <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
           <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>


    <style>
        .dataTables_scrollHeadInner {
width: max-content!important;
}
    </style>
     <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updclub"
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
    
   <asp:UpdatePanel ID="updclub" runat="server">
        <ContentTemplate>
             <asp:Panel runat="server" ID="pnlMain">
              <div class="row">
                <div class="col-md-12 col-sm-12 col-12" id="divclub" runat="server">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Club Activity Registration Approval</h3>
                            <%--<h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>--%>
                        </div>
                          <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                      <div class="form-group col-lg-3 col-md-6 col-12" id="divSubCounsellor" runat="server">
                                                 <div class="label-dynamic">
                                                      <sup>* </sup>
                                                     <label>Select Club</label>
                                                    <%-- <asp:Label ID="lblDYsubuser" runat="server" Font-Bold="true" Text="SubCounsellors"></asp:Label>--%>
                                                       </div> 
                                         <asp:DropDownList ID="ddlclub" runat="server" CssClass="form-control" TabIndex="1" data-select2-enable="true"
                                            AppendDataBoundItems="True"  ToolTip="Please Select Club">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                           
                                      </div>
                        <div class="form-group col-lg-4 col-md-6 col-12" >
                                                 <div class="label-dynamic">
                                                      <sup> </sup>
                                                     <label>Select College</label>
                                                    <%-- <asp:Label ID="lblDYsubuser" runat="server" Font-Bold="true" Text="SubCounsellors"></asp:Label>--%>
                                                       </div> 
                             <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" TabIndex="1" data-select2-enable="true"
                                            AppendDataBoundItems="True"  ToolTip="Please Select College" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" >
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" ValidationGroup="submit"></asp:RequiredFieldValidator>
                               <%-- <asp:ListBox ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>--%>
                         <%--   <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" Display="None"
                                              ErrorMessage="Please Select Session" ValidationGroup="submit" SetFocusOnError="True" >
                                              </asp:RequiredFieldValidator>--%>
                                      
                                      </div>
                                     <div class="col-12 btn-footer">
                                         <asp:Button ID="btnShow" runat="server" ValidationGroup="submit" Text="Show Student List" 
                                     CssClass="btn btn-primary" OnClick="btnShow_Click" />
                                <asp:Button ID="btnapprove" runat="server" Text="Approve student" 
                                    TabIndex="11" CssClass="btn btn-primary" OnClick="btnapprove_Click"  />
                                <asp:Button ID="btnReport" runat="server" Text="Report(Excel)"
                                    TabIndex="12" CssClass="btn btn-info" OnClick="btnReport_Click"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    TabIndex="13" CssClass="btn btn-warning" OnClick="btnCancel_Click"/>

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                            </div>
                           <div class="col-12">
                                <asp:Panel ID="pnlclubRegapprove" runat="server" >
                                    <asp:ListView ID="lvclubRegapprove" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            SrNo
                                                        </th>
                                                        <th>
                                                            Select for Approve
                                                        </th>
                                                        <th>
                                                          Session
                                                        </th>
                                                         <th>Student Name</th>
                                                        <th>
                                                            Club
                                                        </th>
                                                        <th>
                                                           Title of Event
                                                        </th>
                                                        <th>
                                                            Venue
                                                        </th>
                                                         <th>
                                                            FromDate
                                                        </th>
                                                        <th>
                                                            ToDate
                                                        </th>
                                                        <th>
                                                            Duration
                                                        </th>
                                                        <th>
                                                            Description of Event
                                                        </th>
                                                         <th>
                                                             Student Role
                                                         </th>
                                                        <th>
                                                            Campus
                                                        </th>
                                                        <th>
                                                            Hours
                                                        </th>
                                                        <th>
                                                            Points
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
                                                        <%#Container.DataItemIndex+1%>
                                                  </td>
                                                <td>
                                                       <asp:CheckBox ID="chkapprove" runat="server"  ToolTip='<%# Eval("IDNO") %>' />
                                                     <asp:HiddenField ID="hdclubno" runat="server" Value='<%# Eval("CLUB_NO") %>'/>
                                                </td>
                                             <%--   <td style="text-align: center;">
                                                     <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("CLUB_NO")%>'
                                                                    AlternateText="Edit Record" ToolTip="Edit Record"/>
                                                    <%--<asp:ImageButton ID="ImageButton1" runat="server"  CommandArgument='<%# Eval("CLUBNO")%>'
                                                    ImageUrl="~/images/edit.gif" />--%>
                                                   
                                                <%--</td>--%>
                                               
                                                <td><%# Eval("SESSION_NAME") %></td>
                                                <%--<asp:Label ID="lblname" runat="server" Text='<%# Eval("StudentName")%>' ToolTip='<%# Eval("CLUBACTIVITY_TYPE") %>')></asp:Label>--%>
                                                <td><%# Eval("StudentName") %></td>
                                                <td>
                                                     <asp:Label ID="lbltype" runat="server" Text='<%# Eval("CLUB_ACTIVITY_TYPE")%>' ToolTip='<%# Eval("APPROVAL_1") %>'></asp:Label>
                                                    <asp:HiddenField ID="hdnCumulativeNo" runat="server" Value='<%# Eval("CLUBACTIVITY_TYPE") %>'/>
                                                    <asp:HiddenField ID="hdclubactivityno" runat="server" Value='<%# Eval("CLUB_ACTIVITY_NO") %>'/>
                                                    <%--<%# Eval("CLUB_ACTIVITY_TYPE" ) %>' ToolTip='<%# Eval("APPROVESTATUS") %>--%>
                                                </td>
                                                <td>
                                                    <%# Eval("TITLE_OF_EVENT") %>
                                                </td>
                                                 <td>
                                                    <%# Eval("VENUE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("FROMDATE")%>
                                                </td>
                                                 <td>
                                                    <%# Eval("TODATE")%>
                                                </td>
                                                 <td>
                                                    <%# Eval("DURATION")%>
                                                </td>
                                                <td>
                                                      <%# Eval("DESCRIPTION_OF_EVENT")%>
                                                </td>
                                                 <td>
                                                    <%# Eval("WEIGHTAGE_NAME")%>
                                                </td>
                                                <td>
                                                      <%# Eval("CAMPUS_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("COUNT_NAME")%>
                                                </td>
                                                <td>
                                                       <asp:Label ID="lblPoints" runat="server" Text=' <%# Eval("POINTS").ToString()==""?"NA": Eval("POINTS")%>'></asp:Label>
                                                </td>
                                                 <asp:HiddenField ID="hfidno" runat="server" Value='<%# Eval("IDNO")%>' />
                                                 <%--<asp:HiddenField ID="hdclubactivityno" runat="server" Value='<%# Eval("CLUB_ACTIVITY_NO")%>' />--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                                    </div>
                                </div>
                  </div>
                        </div>
                    </div>
                            </div>
                           
                 </asp:Panel>
                            </ContentTemplate>
       <Triggers>
          <asp:PostBackTrigger ControlID="btnReport" />
         <%--   <asp:PostBackTrigger ControlID="ddlsubuser" />--%>
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