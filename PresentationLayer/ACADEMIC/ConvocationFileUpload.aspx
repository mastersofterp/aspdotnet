<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ConvocationFileUpload.aspx.cs" Inherits="ACADEMIC_StudentFileUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <div id="divMsg" runat="server">
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Student Record Upload</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                      
                                   <div class="form-group col-lg-4 col-md-6 col-12">
								        <div class="label-dynamic" runat="server" id="Div2">
									        <sup>* </sup>
									        <label>Convocation Name</label> 
								        </div>
                                        <asp:DropDownList ID="ddlConvocationNo" runat="server" AutoPostBack="True" AppendDataBoundItems="true" ToolTip="Please Select Convocation Name"
                                            OnSelectedIndexChanged="ddlConvocationNo_SelectedIndexChanged" TabIndex="1" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlConvocationNo"
                                            Display="None" ErrorMessage="Please Select Convocation Name" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
							        </div>

                                    
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic" runat="server" id="Div3">
                                            <sup>*</sup>
                                            <label>Convocation Type</label> 
                                        </div>
                                        <asp:DropDownList ID="ddlConvoType" runat="server" AutoPostBack="True" AppendDataBoundItems="true" ToolTip="Please Select Convocation Type"
                                            TabIndex="2" data-select2-enable="true">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                 <asp:ListItem Value="0">Convocation</asp:ListItem>
                                                 <asp:ListItem Value="1">Degree Award Ceremony</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlConvoType"
                                            Display="None" ErrorMessage="Please Select Convocation Type" InitialValue="-1" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                     
                                    <div class="form-group col-lg-4 col-md-6 col-12" style="display:none;">
								        <div class="label-dynamic" runat="server" id="spInstitue">
									        <sup>* </sup>
									        <%--<label>School/Institute Name</label>lblDYtxtSchoolname--%>
                                             <asp:Label ID="lblDYtxtSchoolname" runat="server" Font-Bold="true"></asp:Label>
								        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AutoPostBack="True" AppendDataBoundItems="true" ToolTip="Please Select Institute"
                                            OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" TabIndex="1" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                   <%--     <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select School/Institute Name " InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
							        </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12" style="display:none;">
                                        <div class="label-dynamic">
                                            <%--<label>Admission Batch </label>--%>
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYtxtSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                      <%--  <asp:RequiredFieldValidator ID="rfvddlAdmBatch" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ValidationGroup="academic" InitialValue="0" ErrorMessage="Please Select Session">
                                        </asp:RequiredFieldValidator>--%>
                              
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12" style="display:none;">
                                        <div class="label-dynamic">
                                            <%--<label>Degree </label>--%>
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="2">
                                            <asp:ListItem>Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                   <%--     <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree" SetFocusOnError="true"
                                            Display="None" ValidationGroup="academic" InitialValue="0" ErrorMessage="Please Select Degree">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Excel File </label>
                                        </div>
                                        <asp:FileUpload ID="FileUpload1" runat="server" TabIndex="3" ToolTip="Select file to Import" />
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12" style="display:none;">

                                        <div class="label-dynamic">

                                            <label>Download </label>
                                        </div>
                                        <asp:HyperLink ID="hyperBTECH" NavigateUrl="~/ExcelData/Excel Format/ConvocationExcelSheetFormat.xls" runat="server" TabIndex="4"
                                            Text="Excel Format to Upload Data" Target="_parent"></asp:HyperLink>
                                    </div>

                                    <div class="col-12 btn-footer">
                                         <asp:Button ID="btnExcelReport" runat="server" ValidationGroup="Show" OnClick="btnExcelReport_Click"
                                            TabIndex="4" Text="Excel Format to Upload Data" ToolTip="Click for Excel Format" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnUpload" runat="server" ValidationGroup="Show" OnClick="btnUpload_Click"
                                            TabIndex="5" Text="Upload" ToolTip="Click to Upload" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnsheet" runat="server" ValidationGroup="Show" OnClick="btnsheet_Click"
                                            TabIndex="6" Text="Excel Uploaded Data" ToolTip="Click to Get Excel Uploaded Data" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnCancel" runat="server" TabIndex="7" Text="Cancel" ToolTip="Click To Cancel"
                                            OnClick="btnCancel_Click" CssClass="btn btn-warning" />

                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />
                                  <%--      <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="sheet" />--%>
                                    </div>

                                    <div class="col-12">
                                        <asp:Label ID="lblTotalMsg" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
            <asp:PostBackTrigger ControlID="btnsheet" />
            <asp:PostBackTrigger ControlID="btnExcelReport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
