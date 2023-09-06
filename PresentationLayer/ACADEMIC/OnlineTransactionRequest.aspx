<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineTransactionRequest.aspx.cs" Inherits="ACADEMIC_OnlineTransactionRequest" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <div style="z-index: 1; position: absolute; top:10px;left:500px">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updLists"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size:50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updLists" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Online Transaction Request</h3>
                        </div>

                        <div class="box-body">
                            <fieldset>                               
                                <div class="col-md-4"></div>

                                <div class="form-group col-md-4">
                                    <label>Enrollment No.</label>
                                    <%-- <asp:TextBox ID="txtAppID" runat="server" ToolTip="Please Enter Application ID" ValidationGroup="submit"></asp:TextBox>--%>
                                    <div class="input-group">
                                        <asp:TextBox ID="txtAppID" runat="server" class="form-control" ToolTip="Please Enter Application ID" ValidationGroup="submit" />
                                        <span class="input-group-btn">
                                            <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="submit" class="buttonStyle ui-corner-all" CssClass="btn btn-primary btn-flat" OnClick="btnShow_Click" />
                                            
                                        </span>
                                        <span class="input-group-btn">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="buttonStyle ui-corner-all" CssClass="btn btn-danger" OnClick="btnCancel_Click" style="margin-left:10px" />
                                    </span>
                                            </div>
                                   
                                    <asp:RequiredFieldValidator ID="rfvappid" runat="server" Display="None" ErrorMessage="Please Enter Enrollment No."
                                        ValidationGroup="submit" ControlToValidate="txtAppID"></asp:RequiredFieldValidator>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ValidationGroup="submit" ShowSummary="False" />
                                </div>
                                <div class="col-md-4"></div>
                            </fieldset>
                        </div>
                        <div class="box-footer">
                            <div class="col-md-12" id="div_Studentdetail" runat="server">

                                <div class="form-group col-md-6">
                                    <label>Student Name :</label>
                                    <asp:Label ID="lblStudName" runat="server" Font-Bold="True" style="color:green"></asp:Label>
                                </div>
                                <div class="form-group col-md-6">
                                    <label>Enrollment No : </label>
                                    <asp:Label ID="lblStudEnrollNo" runat="server" Font-Bold="True" style="color:green"></asp:Label>
                                </div>
                                <div class="form-group col-md-6">
                                    <label>College Name :</label>
                                    <asp:Label ID="lblStudClg" runat="server" Font-Bold="True" style="color:green"></asp:Label>
                                </div>
                                <div class="form-group col-md-6">
                                    <label>Degree Name :</label>
                                    <asp:Label ID="lblStudDegree" runat="server" Font-Bold="True" style="color:green"></asp:Label>
                                </div>
                                <div class="form-group col-md-6">
                                    <label>Branch Name :</label>
                                    <asp:Label ID="lblStudBranch" runat="server" Font-Bold="True" style="color:green"></asp:Label>
                                </div>

                            </div>

                            <div id="dvListView" class="col-md-12">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvstudList" runat="server" OnItemCommand="lvstudList_ItemCommand" Visible="false">
                                        <LayoutTemplate>
                                            <div id="listViewGrid">
                                               
                                                <table id="tblstudDetails" class="table table-hover table-bordered table-responsive">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Sr. No.
                                                            </th>                                                       
                                                            <th>Order ID
                                                            </th>
                                                            <th>Order Date
                                                            </th>
                                                            <th>Amount
                                                            </th>
                                                            <th>Message
                                                            </th>
                                                            <th>Send Request
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

                                                    <%# Container.DataItemIndex + 1%>

                                                </td>                                             
                                                <td>
                                                    <asp:Label ID="lblORDERID" runat="server" Text='<%# Eval("ORDERDID")%>' ToolTip='<%# Eval("MERCHANTID")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdfORDERID" runat="server" Value='<%# Eval("ORDERDID")%>' />
                                                   
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblChllanDate" runat="server" Text='<%# Eval("CHALAN_DATE")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdfApTranId" runat="server" Value='<%# Eval("APTRANSACTIONID")%>' />
                                                </td>
                                                 <td>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT")%>' ToolTip='<%# Eval("AMOUNT")%>'></asp:Label>
                                                    
                                                </td>
                                                 <td>
                                                    <asp:Label ID="lblMsg" runat="server" Text='<%# Eval("MESSAGE")%>' ToolTip='<%# Eval("RECIEPT_CODE")%>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Button ID="btnRequest" runat="server" CommandName="getdata" CommandArgument='<%#Eval("TEMP_DCR_NO")%>' Text="Send Request" CssClass="btn btn-primary" />
                                                   <asp:HiddenField ID="hdfTempDcrNo" runat="server" Value='<%# Eval("TEMP_DCR_NO")%>' />
                                                     <asp:HiddenField ID="hdfFlag" runat="server" Value='<%# Eval("FLAG")%>' />

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <p>
                                <asp:Label ID="lblNote1" runat="server" Text=""></asp:Label></b> </p>
                            <div id="divMsg" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
