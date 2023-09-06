<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ReceiveApplicationStatus.aspx.cs" Inherits="ReceiveApplicationStatus"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlUser"
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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Receive Application Status</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Application ID</label>
                                </div>
                                <asp:TextBox ID="txtApplicationID" runat="server" MaxLength="15" TabIndex="1" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvApplicationID" runat="server" ErrorMessage="Please Enter Student Application ID."
                                        ControlToValidate="txtApplicationID" Display="None" SetFocusOnError="True" ValidationGroup="Show" />
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label></label>
                                </div>
                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" TabIndex="2" ValidationGroup="Show"
                                    OnClick="btnShow_Click" />
                                <asp:ValidationSummary ID="valShowSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" />
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <asp:Panel ID="pnlDetails" runat="server">
                            <div class="sub-heading">
                                <h5>Student Details</h5>
                            </div>

                            <asp:UpdatePanel ID="updpnlUser" runat="server">
                                <ContentTemplate>
                                    <div id="divStudentInfo" style="display: block;">
                                        <div class="row">
                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Student Name :</b>
                                                        <a class="sub-label"><asp:Label ID="lblStudentname" runat="server" Font-Bold="true" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Date of Birth :</b>
                                                        <a class="sub-label"><asp:Label ID="lblDateOfBirth" runat="server" Font-Bold="true" /></a>   
                                                    </li>
                                                    <li class="list-group-item"><b>Address :</b>
                                                        <a class="sub-label"><asp:Label ID="lblAddress" runat="server" Font-Bold="true" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Pin Code :</b>
                                                        <a class="sub-label"><asp:Label ID="lblPinCode" runat="server" Font-Bold="true" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Mobile No :</b>
                                                        <a class="sub-label"><asp:Label ID="lblMobile" runat="server" Font-Bold="true" /></a>
                                                    </li>  
                                                    <li class="list-group-item"><b>Email ID :</b>
                                                        <a class="sub-label"><asp:Label ID="lblEmail" runat="server" Font-Bold="true" /></a>
                                                    </li>  
                                                </ul>
                                            </div>

                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Photo :</b>
                                                        <asp:Image ID="imgPhoto" runat="server" Height="128px" Width="128px" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Applied for Program :</b>
                                                        <asp:Label ID="lblProgram" runat="server" Font-Bold="true" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Branch :</b>
                                                        <asp:Label ID="lblBranch" runat="server" Font-Bold="true" /></a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnRecieved" runat="server" TabIndex="12" Text="Received" CssClass="btn btn-primary" ValidationGroup="Submit"
                                            OnClick="btnRecieved_Click" />
                                        <asp:Button ID="btnCancel" runat="server" TabIndex="13" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancel_Click" />
                                        <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Submit" />
                                        <div id="divMsg" runat="server">
                                        </div>  
                                    </div>

                                    <div class="col-md-12">                                
                                        <asp:Panel ID="pnlStudents" runat="server">
                                            <asp:ListView ID="lvStudents" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Student List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>
                                                                    Sr.No
                                                                </th>
                                                                <th>
                                                                    Application ID
                                                                </th>
                                                                <th>
                                                                    Student Name
                                                                </th>
                                                                <th>
                                                                    Degree
                                                                </th>
                                                                <th>
                                                                    Branch
                                                                </th>
                                                                <th>
                                                                    Application Status
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <EmptyDataTemplate>
                                                    <center>
                                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" /></center>
                                                </EmptyDataTemplate>
                                                <ItemTemplate>
                                                    <tr class="item">
                                                        <td>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("USERNAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("STUDENTNAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("DEGREE")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("BRANCH")%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("APPLICATION_STATUS")%>'
                                                                ForeColor="Green"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>                                   
                                    </div>  

                                </ContentTemplate>
                            </asp:UpdatePanel>                       
                        </asp:Panel>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
