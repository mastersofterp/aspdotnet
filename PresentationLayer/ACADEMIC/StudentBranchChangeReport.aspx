<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentBranchChangeReport.aspx.cs" Inherits="ACADEMIC_StudentBranchChangeReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updtime"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updtime" runat="server">
        <ContentTemplate>         
                  <div class="row">     
                      <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>  
                         <div class="box-header with-border">
                            <h3 class="box-title"><b>Branch Change Report</b></h3>
                            <%--<div class="box-tools pull-right">
                                 <div style="color: Red; font-weight: bold">
                             &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory</div>
                            </div>--%>
                        </div> 
                        <div class="box-body">
                            <fieldset>   
                                 <%--<div class="form-group col-md-3">
                                    <label><span style="color: red">*</span>Admission Batch</label>
                                    <asp:DropDownList ID="ddlAdmbatch" runat="server" TabIndex="2"
                                        AppendDataBoundItems="True" ToolTip="Please Select Admbatch"
                                        OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmbatch"
                                        Display="None" ErrorMessage="Please Select Admbatch" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                </div>--%>

                                <%--<div class="form-group col-md-3">
                                    <label><span style="color: red">*</span>Institute Name</label>
                                    <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="True" TabIndex="1" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged"
                                        AutoPostBack="True" ToolTip="Please Select Institute">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvColg" runat="server" ControlToValidate="ddlColg"
                                        Display="None" ErrorMessage="Please Select Institute" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                </div> --%>                      
                                <div class="form-group col-md-4">
                                    <label>Degree</label>
                                    <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" AppendDataBoundItems="True" TabIndex="1"
                                        OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" ToolTip="Please Select Degree" AutoPostBack="True">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Branch</label>
                                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" AppendDataBoundItems="True" TabIndex="2"
                                        OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ToolTip="Please Select Branch" AutoPostBack="True">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                </div>
                                
                            
                            </fieldset>

                        </div>
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnPrintReport" runat="server" Text="Branch Change Report" TabIndex="3" CssClass="btn btn-info"
                                    OnClick="btnPrintReport_Click"/>                            
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="4" CssClass="btn btn-danger" />
                            </p>

                        
                            <p>
                              <%-- <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="show" DisplayMode="List" />--%>
                                <div id="divMsg" runat="server">
                                </div>
                            </p>
                        </div>
                        </div>
                          </div>
                      </div>
               </ContentTemplate>
        <%--<Triggers>   
            <asp:AsyncPostBackTrigger ControlID="btnPrintReport" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
               <asp:PostBackTrigger ControlID="btnShow" />
        </Triggers>--%>
    </asp:UpdatePanel>
</asp:Content>

