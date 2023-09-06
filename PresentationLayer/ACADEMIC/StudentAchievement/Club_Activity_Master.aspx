<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Club_Activity_Master.aspx.cs" Inherits="ACADEMIC_StudentAchievement_Club_Activity_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="pnlFeeTable"
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
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }

    </script>


    <asp:UpdatePanel ID="pnlFeeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Club Activity Master</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Activity Name</label>
                                        </div>
                                        <asp:TextBox ID="txtName" runat="server" AutoComplete="off" TabIndex="1" MaxLength="20" CssClass="form-control"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="rfvtxtName" runat="server" ControlToValidate="txtName"
                                            ErrorMessage="Please Enter Activity Name" TabIndex="1" Display="None"
                                            ValidationGroup="ClubActivity"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Description</label>
                                        </div>
                                        <asp:TextBox ID="txtDescription" runat="server" AutoComplete="off" CssClass="form-control"
                                            TabIndex="2" TextMode="MultiLine"  MaxLength="300" Width="600px" Height="80px"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription" Display="None"
                                            ErrorMessage="Please Enter Description" ValidationGroup="ClubActivity"></asp:RequiredFieldValidator>

                                      <%--   <asp:RegularExpressionValidator ID="frvdev" runat="server" ControlToValidate="txtDescription"
                                            ValidationExpression="^[\s\S]{0,10}$"
                                            ErrorMessage="Please enter a maximum of 100 characters" 
                                            Display="none" ValidationGroup="ClubActivity">
                                        </asp:RegularExpressionValidator>--%>
                                    </div>

                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnSubmitClub" runat="server" CssClass="btn btn-primary" ValidationGroup="ClubActivity" OnClick="btnSubmitClub_Click">Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnCancelClub" runat="server" CssClass="btn btn-warning" OnClick="btnCancelClub_Click">Cancel</asp:LinkButton>
                            </div>


                            <div class="col-12 mt-3">
                                <div class="sub-heading">
                                    <h5>Club Activity List</h5>
                                </div>
                                <asp:Panel ID="pnlClub" runat="server" Visible="true">
                                    <asp:ListView ID="lvClubActivity" runat="server" ItemPlaceholderID="itemPlaceholder">
                                        <LayoutTemplate>

                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblEvent">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit</th>
                                                        <th>Name</th>
                                                        <th>Description</th>
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
                                                    <asp:ImageButton ID="btnEditCreateEvent" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%#Eval("ACTIVITYID") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditCreateEvent_Click" />
                                                </td>
                                                <td><%# Eval("ACTIVITY_NAME") %></td>
                                                <td><%# Eval("ACTIVITY_DESCRIPTION") %></td>
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
            <%--<asp:PostBackTrigger ControlID="btnSearch" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="valsum" runat="server" ValidationGroup="ClubActivity" DisplayMode="List" ShowMessageBox="true" ShowModelStateErrors="false" ShowSummary="false" />

   <%-- <script>
        function checkSpecialKeys(e) {
            if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                return false;
            else
                return true;
        }
    </script>
   --%>

</asp:Content>

