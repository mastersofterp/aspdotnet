<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="BulkEmailLog_Excel.aspx.cs" Inherits="ACADEMIC_BulkEmailLog_Excel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" 
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


<%--    <asp:UpdatePanel ID="updBatch" runat="server">
        <ContentTemplate>--%>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Bulk Email Sms Whatsaap Log</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Activity</label>
                                        </div>
                                        <asp:DropDownList ID="ddlActivity" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" ToolTip="Please Select State" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    </div>
                                </div>

                             <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Excel Report" ToolTip="Excel" ValidationGroup="submit"
                                    CssClass="btn btn-primary"  TabIndex="4" OnClick="btnSave_Click" />
                              </div>
                            </div>
                        </div>
                    </div>
                </div>
        <%--    </ContentTemplate>
        </asp:UpdatePanel>--%>

    </asp:Content>