<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="TPStudentConfrimReport.aspx.cs" Inherits="TRAININGANDPLACEMENT_Reports_TPStudentConfrimReport"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updTPreports"
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
    <asp:UpdatePanel ID="updTPreports" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">STUDENT LIST FOR COMPANY CONFIRM</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Schedule</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchedule" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                            AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSchedule" runat="server" ControlToValidate="ddlSchedule"
                                            InitialValue="0" SetFocusOnError="true" ErrorMessage="Please Select Schedule"
                                            ValidationGroup="Select" Display="None"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="Select" OnClick="btnShow_Click"
                                   CssClass="btn btn-primary" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Select"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

        <%--ADDED BY SUMIT -- 18092019--%>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
        </Triggers>

    </asp:UpdatePanel>
    <div id="div2" runat="server">
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
