<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentFeesUpload.aspx.cs" Inherits="ACADEMIC_StudentFeesUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updfeesupload"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size:50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updfeesupload" runat="server">
        <ContentTemplate>
   
        <div class="row">
        <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title"><b>Student Fees Upload</b></h3>   
                <div class="box-tools pull-right">
                         <div style="color: Red; font-weight: bold">
                             &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory</div>
                </div>            
            </div>
            <div id="divMsg" runat="server">
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="form-group col-md-8">
                        <div class="form-group col-md-6">
                            <label><span style="color: red;">*</span> Session</label>
                            <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" CssClass="form-control" AppendDataBoundItems="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Please Select Session"
                                ControlToValidate="ddlSession" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="upload">
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvsession" runat="server" ErrorMessage="Please Select Session"
                                ControlToValidate="ddlSession" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="report">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-md-6">
                            <label><span style="color: red;">*</span> Institute Name</label>
                            <asp:DropDownList ID="ddlCollegeName" runat="server" CssClass="form-control" TabIndex="2" AppendDataBoundItems="True"
                                ValidationGroup="Branch" ToolTip="Please Select Institute" AutoPostBack="True" OnSelectedIndexChanged="ddlCollegeName_SelectedIndexChanged">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList><asp:RequiredFieldValidator ID="rfvcolg" runat="server" ControlToValidate="ddlCollegeName"
                                Display="None" ErrorMessage="Please Select Institute " ValidationGroup="upload"
                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-md-6" id="divdegree" runat="server" visible="false">
                            <label><span style="color: red;">*</span> Degree </label>
                            <asp:DropDownList ID="ddlDegreeName" runat="server" CssClass="form-control" TabIndex="3" AppendDataBoundItems="True"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlDegreeName_SelectedIndexChanged" ValidationGroup="upload" ToolTip="Degree Name">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDegreeName" runat="server" ControlToValidate="ddlDegreeName"
                                Display="None" ErrorMessage="Please Select Degree " ValidationGroup="upload"
                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-md-6"  id="divbranch" runat="server" visible="false">
                            <label><span style="color: red;">*</span> Branch </label>
                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" TabIndex="4"  AppendDataBoundItems="True" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Select Branch"
                                ControlToValidate="ddlBranch" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="upload">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-md-6"  id="divsemester" runat="server" visible="false">
                            <label><span style="color: red;">*</span> Semester </label>
                            <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" TabIndex="4" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlSemester"
                                Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="upload" />
                        </div>
                        <div class="form-group col-md-6">
                                        <label><span style="color: red;">*</span> Receipt Type</label>
                                        <asp:DropDownList ID="ddlReceiptType" runat="server" AppendDataBoundItems="true"
                                            CssClass="form-control" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="valReceiptType" runat="server" ControlToValidate="ddlReceiptType"
                                            Display="None" ErrorMessage="Please select Receipt Type." InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="upload" />
                                    </div>
                         <div class="col-md-6">
                              <fieldset class="fieldset" style="text-align: center;">
                            <legend class="legend">Download Format</legend>
                            <asp:LinkButton ID="lbExcelFormat" runat="server" TabIndex="5"  Font-Underline="true" ToolTip="Click Here For Downloading Sample Format" style="font-weight: bold;" CssClass="stylink" OnClick="lbExcelFormat_Click"><span style="color:green;">Pre-requisite excel format for upload</span></asp:LinkButton>
                        </fieldset>
                             </div>
                        <div class="row">
                            <div class=" col-md-12">
                         <div class="form-group col-md-12">
                                <label><span style="color: red;">*</span> Upload Excel File </label>
                                 <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Please Select file to Import" CssClass="btn btn-info"/>
                             </div>
                             </div>
                            </div>
                      <%--  <div class="row">
                            <div class="col-md-12">
                            <div class="col-md-12 form-group">
                              
                                </div>
                            </div>

                        </div>--%>
                 <%--           <div class="row">--%>
                            <div class="form-group col-md-12">
                                  <asp:Label ID="lblTotalMsg" Style="font-weight: bold; color: red;" runat="server"></asp:Label>
                                </div>
                           <%--     </div>--%>
                    </div>
                    <div class="form-group col-md-4">
                         <fieldset class="fieldset" style="padding: 5px; color: Green">
                                        <legend style="text-align: center">Note</legend>
                                        <span style="font-weight: bold; color: Red;">For Upload and Verify  : </span>
                                        <br />
                                        Please Select -> Session -> Institute Name -> Receipt Type
                                        <br />
                                        <span style="font-weight: bold; color: Red;">For Report  : </span>
                                        <br />
                                        Please Select -> Session
                                    </fieldset>
                       
                    </div>
                </div>
            </div>
            <div class="box-footer">
                <p class="text-center">
                  <asp:Button ID="btnUpload" runat="server" ValidationGroup="upload" TabIndex="6" OnClick="btnUpload_Click"
                                             Text="Upload and Verify" CssClass="btn btn-primary" ToolTip="Click to Upload  & Verify"  />
                          <asp:Button ID="btnVerifyRegister" runat="server" ValidationGroup="upload" CssClass="btn btn-primary" Enabled="false"  OnClick="btnVerifyRegister_Click"
                                            TabIndex="7" Text="Save and Register" ToolTip="Click to Verify Students Fees" Visible="false"/>
                                <asp:Button ID="btnPrintRegSlip" runat="server" Text="Report" ValidationGroup="report" ToolTip="Click Here To Generate Student Fees Report"
                                        OnClick="btnPrintRegSlip_Click" CssClass="btn btn-info" />

                       <asp:Button ID="btnsummary" runat="server" Text="Summary Report" ValidationGroup="report" ToolTip="Click Here To Generate Student Fees Summary Report"
                                        OnClick="btnsummary_Click" CssClass="btn btn-info" />

                                        <asp:Button ID="btnCancel" runat="server" TabIndex="8" CssClass="btn btn-warning"  Text="Cancel" ToolTip="Click To Cancel"
                                            OnClick="btnCancel_Click" />
                    <asp:ValidationSummary id="validationsummary" runat="server" DisplayMode="List" ValidationGroup="upload" ShowSummary="false" ShowMessageBox="true" />
                    <asp:ValidationSummary id="validationsummary1" runat="server" DisplayMode="List" ValidationGroup="report" ShowSummary="false" ShowMessageBox="true" />
                    </p>
                <div class="form-group col-md-12">
                    <div class="col-md-12 table table-responsive" id="tblenrollmentno" runat="server" Visible="false">
                                    <asp:Panel ID="pnlstuenrollno" runat="server"  ScrollBars="Auto" Height="350px">
                                    <asp:ListView ID="lvStudent" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <h4>Roll No Mismatch List </h4>
                                                <table id="tblHead" class="table table-hover table-bordered table-striped">
                                                  <thead>
                                                        <tr class="bg-light-blue" id="trRow">
                                                            <th>Sr.No.
                                                            </th>
                                                            <th>Roll No.
                                                            </th>
                                                            <th>Student Name
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
                                                    <%# Eval("SRNO") %>
                                                </td>
                                                <td>
                                                    <%# Eval("REGNO") %>
                                                </td>
                                                <td>
                                                    <%# Eval("SNAME") %>
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
            </div>
            </ContentTemplate>
           <Triggers>
                            <asp:PostBackTrigger ControlID="btnUpload" />
                            <asp:PostBackTrigger ControlID="lbExcelFormat" />
                        </Triggers>
        </asp:UpdatePanel>
    <style>
        .stylink 
a
{ font:10px; color:white;  }

a:hover
{ font:12px; color:#ff0; }
    </style>
  </asp:Content>
