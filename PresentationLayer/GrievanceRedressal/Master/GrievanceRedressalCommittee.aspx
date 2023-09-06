<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="GrievanceRedressalCommittee.aspx.cs"
    Inherits="GrievanceRedressal_Master_GrievanceRedressalCommittee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updRedressalCommittee"
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
    <asp:UpdatePanel ID="updRedressalCommittee" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">GRIEVANCE REDRESSAL COMMITTEE TYPE</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlGriv" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Add/Edit Grievance Redressal Committee Type</h5>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup><label>Grievance Redressal Committee Type</label>
                                                
                                            </div>
                                            <asp:TextBox ID="txtCommitteeType" runat="server"
                                                ValidationGroup="Submit" MaxLength="120" TabIndex="1" CssClass="form-control" ToolTip="Enter Grievance Redressal Committee Type"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCommitteeType" runat="server" ControlToValidate="txtCommitteeType"
                                                Display="None" ErrorMessage="Please Enter Grievance Redressal Committee Type" ValidationGroup="Submit"
                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="Filteredyearstartconsult" runat="server"
                                                FilterType="Custom,UppercaseLetters, LowercaseLetters,Numbers" TargetControlID="txtCommitteeType"
                                                ValidChars=" ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup><label>Is Department Level</label>                                               
                                            </div>
                                             <asp:CheckBox ID="chkCommitteeTypeDept" runat="server" TabIndex="1" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class=" col-12 btn-footer">

                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" CausesValidation="true" TabIndex="1" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" OnClick="btnCancel_Click" CausesValidation="false" TabIndex="1" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />

                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlCommittee" runat="server" >
                                    <asp:ListView ID="lvCommittee" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>GRIEVANCE REDRESSAL COMMITTEE TYPE LIST </h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>EDIT
                                                            </th>
                                                            <th>GRIEVANCE REDRESSAL COMMITTEE TYPE 
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                        CommandArgument='<%# Eval("GRCT_ID") %>' ImageUrl="~/Images/edit.png" TabIndex="1"
                                                        ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <%# Eval("GR_COMMITTEE")%>
                                                </td>
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
    </asp:UpdatePanel>
</asp:Content>



