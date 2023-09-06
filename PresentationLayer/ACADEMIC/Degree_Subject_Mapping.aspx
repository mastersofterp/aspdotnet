<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Degree_Subject_Mapping.aspx.cs" Inherits="ACADEMIC_Degree_Subject_Mapping" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <style>
#ctl00_ContentPlaceHolder1_pnlListView .dataTables_scrollHeadInner {
width: max-content !important;
}
</style>
     <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSubject"
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
    <asp:UpdatePanel ID="updSubject" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div> 
                        <div class="box-header with-border">
                            <h3 class="box-title">Degree Subject Mapping</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                             <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" TabIndex="1" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="true" ToolTip="Please Select Degree.">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>                                           
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" ValidationGroup="Submit" ErrorMessage="Please Select Degree."
                                            Display="None" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                         <div class="label-dynamic">
                                             <sup>* </sup>
                                            <label>Subject Name</label>
                                        </div>
                                        <asp:TextBox ID="txtSubName" runat="server" ToolTip="Please Enter Subject Name." TabIndex="2" MaxLength="64" CssClass="form-control" placeholder="Enter Subject Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="tfvSubject" runat="server" ControlToValidate="txtSubName" ValidationGroup="Submit" ErrorMessage="Please Enter Subject Name."
                                            Display="None"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ajaxSub" runat="server" InvalidChars="~`!@#$%^&*()-+={}[]:;',<>?|\" FilterMode="InvalidChars" TargetControlID="txtSubName"></ajaxToolkit:FilteredTextBoxExtender>                                           
                                    </div>
                                    <div class="form-group col-lg-5 col-md-6 col-12">
                                        <div class="label-dynamic">
                                             <sup> </sup>
                                            <label>Options</label>
                                        </div>     
                                        <div style="padding-top:6px">                                   
                                        <asp:CheckBox ID="chkCutOff" runat="server" Text="Is Cut Off" TabIndex="3" ToolTip="Check for CutOff Subject." /> &nbsp;
                                        <asp:CheckBox ID="chkComp" runat="server" Text="Is Compulsory" TabIndex="4" ToolTip="Check for Compulsory Subject." /> &nbsp;
                                        <asp:CheckBox ID="chkOthers" runat="server" Text="Is Others" TabIndex="5" ToolTip="Check for Others Subject." /> 
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="6" ToolTip="Click To Submit." ValidationGroup="Submit" CssClass="btn btn-success" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="7" ToolTip="Click To Cancel." CausesValidation="false" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummary" runat="server" ValidationGroup="Submit" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                            </div>
                            <div class="col-md-12">
                                <asp:ListView ID="lvSubjectList" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                                <h5>Subjects List</h5>
                                            </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divSubList">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th style="width:5%">
                                                        Edit
                                                    </th>
                                                    <th>
                                                        Degree Name
                                                    </th>
                                                    <th>
                                                        Subject Name
                                                    </th>
                                                    <th>
                                                        Is Compulsory
                                                    </th>
                                                    <th>
                                                        Is Cut Off
                                                    </th>
                                                    <th>
                                                        Is Others
                                                    </th>
                                                </tr>
                                            </thead>
                                             <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                             <td style="width:5%">
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit1.gif"
                                                OnClick="btnEdit_Click" AlternateText="Edit Record" ToolTip="Edit Record" CommandArgument='<%# Eval("SUB_ID") %>'/>
                                                 <%--CommandArgument='<%# Eval("CONFIGID")%>'--%> 
                                                    </td>
                                            <td>
                                                <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREE") %>'></asp:Label>
                                            </td>
                                             <td>
                                                <asp:Label ID="lblSubName" runat="server" Text='<%# Eval("SUB_NAME") %>'></asp:Label>
                                            </td>
                                             <td style="font-weight:bold">
                                                <asp:Label ID="lblComp" runat="server" Text='<%# Eval("COMPULSORY") %>' ForeColor='<%# Eval("COMPULSORY").ToString().Equals("Yes")?System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label>
                                            </td> 
                                            <td style="font-weight:bold">
                                                <asp:Label ID="lblCutOff" runat="server" Text='<%# Eval("CUTOFF") %>' ForeColor='<%# Eval("CUTOFF").ToString().Equals("Yes")?System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label>
                                            </td> 
                                            <td style="font-weight:bold">
                                                <asp:Label ID="lblOther" runat="server" Text='<%# Eval("OTHERS") %>' ForeColor='<%# Eval("OTHERS").ToString().Equals("Yes")?System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>