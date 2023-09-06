<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentStatusDetails.aspx.cs" Inherits="ACADEMIC_StudentStatusDetails" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div style="z-index: 1; position: absolute; top: 10px; left: 550px;">
        <asp:UpdateProgress ID="updProg" runat="server"
            DynamicLayout="true" DisplayAfter="0" AssociatedUpdatePanelID="updLists">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>In-Progress</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updLists" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><b>Student Form Filling Status</b></h3>
                                <div class="pull-right">
                                    <div style="color: Red; font-weight: bold">
                                        &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                    </div>
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="col-md-12">
                                  
                                  
                                     <div class="row">
                                             <div class="form-group col-md-4">
                                                    <label><span style="color: red;">*</span> Admission batch </label>
                                                    <asp:DropDownList ID="ddlAdmbatch" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="1">
                                                        <asp:ListItem>Please Select </asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmbatch"
                                                        Display="None" ValidationGroup="show" InitialValue="0"
                                                        ErrorMessage="Please Select Admission batch"></asp:RequiredFieldValidator>
                                         </div>
                                     </div>
                                    <div class="row">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ValidationGroup="show" ShowSummary="False"/>
                                     
                                       
                                    </div>
                                    <div class="row">
                                        <p class="text-center">
                                            <asp:Button ID="btnReport" runat="server" Text="Show Details" ValidationGroup="show" OnClick="btnReport_Click" CssClass="btn btn-primary"/>
                                           
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                       </p>
                                    </div>

                                </div>
                            </div>
                            
                        </div>

                    </div>
                </div>
            </div>


            </div>
        </ContentTemplate>
        <Triggers>
           
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

