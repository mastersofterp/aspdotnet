<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="UserManual.aspx.cs" Inherits="ACADEMIC_UserManual" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updUserManual"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updUserManual" runat="server">
        <ContentTemplate>

            <div id="divMsg" runat="server">
            </div>

            <div class="row">
                <div class="col-sm-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b> User Manual Upload / Download </b></h3>
                            <div style="color: Red; font-weight: bold; padding-top: 15px" id="divNote" runat="server" visible="false">
                                &nbsp;&nbsp;Note : 
                             1) Upload the User Manual Files only with following formats: .pdf, .doc
                            </div>
                        </div>
   
                        <div class="col-md-12">
                            <asp:ListView ID="lvUserManualList" runat="server" OnItemDataBound="lvUserManualList_ItemDataBound">
                                <LayoutTemplate>
                                    <div id="demo-grid">
                                       <%-- <h4>User Manual List</h4>--%>
                                        <table class="table table-hover table-bordered table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                   <%-- <th>Select</th>--%>
                                                    <th>Module</th>
                                                    <th>Download</th>
                                                    <th><asp:Label ID="lblUserManual" runat="server" Text="Upload User Manual"></asp:Label></th>
                                                    <th><asp:Label ID="lblUpload" runat="server" Text="Upload"></asp:Label></th>
                                                    <%--<th><asp:Label ID="lblThStatus" runat="server" Text="Status"></asp:Label></th>--%>
                                                  
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
                                       <%-- <td style="width: 8%">
                                           <asp:CheckBox ID="chkUserManual" runat="server" ToolTip="Please Select Module" />
                                        </td>--%>
                                        <td style="width: 30%">
                                            <asp:Label ID="lblUMNo" ToolTip='<%# Eval("ASNo") %>' runat="server" Text='<%# Eval("AS_Title") %>'></asp:Label>
                                            <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("AS_No") %>' />
                                        </td>

                                         <td style="width: 20%">               
                                            <asp:LinkButton ID="lnkDownloadDoc" runat="server" OnClick="lnkDownloadDoc_Click" Visible="false"
                                                Text="Click to Download" ToolTip="Click here to download User Manual"
                                                CommandArgument='<%# Eval("ASNo") %>'> 
                                            </asp:LinkButton>
                                            <asp:Label ID="lblDownload" Font-Bold="true" runat="server"></asp:Label>
                                        </td>

                                        <td style="width: 8%">
                                            <asp:FileUpload ID="fuFile" runat="server" ToolTip="UMNO" />
                                            <asp:HiddenField ID="hdnFile" runat="server" />
                                            <br />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSubmit" runat="server" Text="Upload" ValidationGroup="Submit" OnClick="btnSubmit_Click" Width="80px" Height="35px"
                                                CssClass="btn btn-success" CommandArgument='<%# Eval("AS_Title") %>' ToolTip='<%# Eval("AS_Title") %>' />
                                        </td>
                                      <%--  <td runat="server" id="tdlblStatus">
                                            <asp:Label ID="lblStatus" Font-Bold="true" runat="server"></asp:Label>
                                        </td>--%>
                                 
                                       

                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-5 form-group">
                                </div>
                                <div class="col-md-7 form-group" style="padding-left: 75px">
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-danger" Visible="false" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>


        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="lvUserManualList" />
        </Triggers>

    </asp:UpdatePanel>

</asp:Content>

