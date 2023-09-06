<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SemesterRegistration_Report.aspx.cs" Inherits="SemesterRegistration_Report" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:UpdatePanel runat="server" ID="updReport">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                             <%--<h3 class="box-title">Semester Registration Report</h3>--%>
                            <h3 class="box-title" ><b>Semester Registration Report</b>
                                <%--<asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>--%></h3>
                        </div>

                        <div class="box-body">
                            <%--Search Pannel Start by Swapnil --%>
                            <div id="myModal2" role="dialog" runat="server">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updEdit"
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

                                <asp:UpdatePanel ID="updEdit" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>College</label>
                                                    </div>
                             
                                                    <asp:DropDownList runat="server" class="form-control" ID="ddlcollege" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlcollege_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                     
                                                    </asp:DropDownList>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlcollege" Display="None"
                                                        ErrorMessage="Please Select College" ValidationGroup="report"  SetFocusOnError="True" InitialValue="0">
                                                    </asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Session</label>
                                                    </div>
                             
                                                    <asp:DropDownList runat="server" class="form-control" ID="ddlSession" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                     
                                                    </asp:DropDownList>
                                                     <asp:RequiredFieldValidator ID="rfvsession" runat="server" ControlToValidate="ddlSession" Display="None"
                                                        ErrorMessage="Please Select Session" ValidationGroup="report"  SetFocusOnError="True" InitialValue="0">
                                                    </asp:RequiredFieldValidator>

                                                </div>
                                                  </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnExcel" runat="server" Text="Excel Report"  CssClass="btn btn-primary" ValidationGroup="report" OnClick="btnExcel_Click"/>

                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click"/>
                                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                           ShowSummary="false" ValidationGroup="report"  />
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                            </div>
                                               </ContentTemplate>
                            
                                </asp:UpdatePanel>
                            </div>
                               </div>
                           </div>
                       </div>
                   </div>
              </ContentTemplate>
                              <Triggers>
           
           <asp:PostBackTrigger ControlID="btnExcel" />
           <asp:PostBackTrigger ControlID="btnCancel" />
       </Triggers>
                  </asp:UpdatePanel>
      <div id="divMsg" runat="server">
    </div>
                            </asp:Content>
