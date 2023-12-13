<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_BulkPhotoSignExtract.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_BulkPhotoSignExtract" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div style="z-index: 1; position: absolute; top: 25%; left: 50%;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
 <ContentTemplate>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">EMPLOYEE PHOTO AND SIGNATURE EXTRACT</h3>
                </div>
                <div class="box-body">
                        <asp:Panel ID="divPaySlip" runat="server">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Select College & Staff</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                             <%--<div class="box box-body">--%>
                                  <div class="row">
                                    <div class="form-group col-md-12">
                                        <div class="form-group col-md-12">
                                            <label>Extract Type :</label>
                                            <asp:RadioButton ID="rdoEMPPHOTO" runat="server" Text="Employee Photo" GroupName="rolllist"
                                               AutoPostBack="True"  OnCheckedChanged="rdoEMPPHOTO_CheckedChanged"/>&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoEMPSIG" runat="server" Text="Employee Signature" GroupName="rolllist"
                                            AutoPostBack="True"  OnCheckedChanged="rdoEMPSIG_CheckedChanged" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                           
                                        </div>
                                    </div>
                                </div>
                            <%-- </div>--%>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" ToolTip="Select College" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="true"
                                            AutoPostBack="true" TabIndex="1" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                            ValidationGroup="report" ErrorMessage="Please Select College" SetFocusOnError="true"
                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Staff Name</label>--%>
                                            <label>Scheme/Staff</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStaffName" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" OnSelectedIndexChanged="ddlStaffName_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlStaffName"
                                            Display="None" ErrorMessage="Please Select Scheme/Staff" ValidationGroup="report"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Staff Name</label>--%>
                                            <label>Extracted By</label>
                                        </div>
                                         <asp:DropDownList ID="ddlExtractedby" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" 
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlExtractedby_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                  <asp:ListItem Value="1">EmployeeCode</asp:ListItem>
                                                  <asp:ListItem Value="2">IDNO</asp:ListItem>
                                                  <asp:ListItem Value="3">SequenceNumber</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlExtractedby"
                                            Display="None" ErrorMessage="Please Select Extracted By" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                      
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                 <div class="row">
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Total Records :</label>
                                            <b>
                                                <asp:Label ID="lbltotal" Style="color: #347fbe;" runat="server"></asp:Label>
                                            </b>
                                        </div>
                                    </div>
                                 </div>
                             </div>

                            <div class="col-12 btn-footer">
                               <asp:Button ID="btnExtract" runat="server" OnClick="btnExtract_Click" Text="Extract"
                                CssClass="btn btn-primary" ValidationGroup="report" />&nbsp;&nbsp;
                                <asp:Button ID="btncancel" runat="server" OnClick="btncancel_Click" Text="Clear"
                                CssClass="btn btn-warning" />
                               <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="report" />
                            </div>
                        </asp:Panel>
                    <div id="divMsg" runat="server">
                    </div>
                </div>
            </div>
        </div>
    </div>
 <%--   <script type="text/javascript">
        var jq = $.noConflict();

        function ShowpImagePreview(input) {

            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {

                    jq('#ctl00_ContentPlaceHolder1_lvUpdatePhoto_ctrl10_ImgPhoto').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        function ShowpSignPreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    jq('#ctl00_ContentPlaceHolder1_lvUpdatePhoto_ctrl10_ImgSignature').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>--%>
      </ContentTemplate>
       <Triggers>
       <asp:PostBackTrigger ControlID="btnExtract" />
      </Triggers>
   </asp:UpdatePanel>
</asp:Content>

