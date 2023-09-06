<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPExam_Mark_Upload.aspx.cs" Inherits="Exam_Mark_Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>
        <asp:UpdatePanel ID="updSchedule" runat="server">
        <ContentTemplate>
    <div class="row">
        <asp:UpdateProgress ID="UpdateProgress4" runat="server"
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
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Exam Mark Upload</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Admission Batch </label>
                                </div>
                                <asp:DropDownList ID="ddlAdmissionBatch" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Program Type </label>
                                </div>
                                  <asp:DropDownList ID="ddlProgramType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" OnSelectedIndexChanged="ddlProgramType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                            <asp:ListItem Value="1">UG</asp:ListItem>
                                            <asp:ListItem Value="2">PG</asp:ListItem>
                                        </asp:DropDownList>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Degree </label>
                                </div>
                                <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Program Code </label>
                                </div>
                                <asp:ListBox ID="lstProgram" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" TabIndex="6" AutoPostBack="true" OnSelectedIndexChanged="lstProgram_SelectedIndexChanged"></asp:ListBox>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Upload File </label>
                                </div>
                              <asp:FileUpload ID="fuUpload" runat="server" TabIndex="5" ToolTip="Upload File"/>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="fuUpload" runat="server" ErrorMessage="Please Select File To Upload."
                                                    Display="None" ValidationGroup="upload" ></asp:RequiredFieldValidator>
                            </div>

                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnDownloadExcel" runat="server" Text="Download Excel" TabIndex="6" CssClass="btn btn-primary" OnClick="btnDownloadExcel_Click" />
                        <asp:Button ID="btnUploadExcel" runat="server" Text="Upload Excel" TabIndex="7" CssClass="btn btn-primary" OnClick="btnUploadExcel_Click" />
                          <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" TabIndex="8" ToolTip="Click to Submit" Visible="false" OnClick="btnSubmit_Click" />
                          <asp:Button ID="btnLockMark" runat="server" Text="Lock" CssClass="btn btn-warning" TabIndex="8" ToolTip="Click to Lock" Visible="false" OnClick="btnLockMark_Click"/>
                        <asp:Button ID="btnExcelReport" runat="server" Text="Excel(Report)" TabIndex="8" CssClass="btn btn-primary" OnClick="btnExcelReport_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="9" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                    </div>

                        <div class="col-md-12 table table-responsive" style="margin-top: 1px" runat="server" id="divExamMarkEntryExcelData" visible="false">
                                        <div>
                                    <%--    <h4>Entrance Exam Mark Entry Excel Data</h4>--%>
                                        <asp:Panel ID="pnlInfo" runat="server" ScrollBars="Auto">
                                            <asp:ListView ID="lvMarkEntry" runat="server"  OnItemDataBound="lvMarkEntry_ItemDataBound">
                                                <LayoutTemplate>
                                                    <div id="demo-grid" class="tableFixHead">
                                                        <table id="tblMarkEntryExcelData" class="table table-hover table-bordered">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th style="width:10px; font-weight:bold; font-size:14px;">Sr.No</th>
                                                                    <th style="width:200px; font-weight:bold; font-size:14px;">Applicant Id</th>
                                                                    <th style="width:550px; font-weight:bold; font-size:14px;">Applicant Name</th>                                                                 
                                                                    <th style="width:500px; font-weight:bold; font-size:14px;">Marks</th>
                                                                    <th style="width:500px; font-weight:bold; font-size:14px;">Lock Status</th>
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
                                                        <td style="text-align: center">
                                                            <%# Container.DataItemIndex+1 %>
                                                            <%--<asp:HiddenField ID="hdnUser" runat="server" Value='<%#Eval("USERNO") %>' />--%>
                                                        </td>
                                                        <td>
                                                          <asp:Label ID="lblApp" runat="server" Text='<%# Eval("Applicant_ID") %>'></asp:Label>
                                                        </td>
                                                        <td>

                                                          <asp:Label ID="lblName" runat="server" Text='<%# Eval("Applicant_Name") %>'></asp:Label>
                                                        </td> 
                                                                                                                                                                         
                                                        <td>
                                                            <asp:Label ID="lblMarks" runat="server" Text='<%# Eval("Marks") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblLockStstus" runat="server" Text='<%#  Convert.ToInt32(Eval("LockStatus"))==1 ? "Lock" :  "-" %>'
                                                                ForeColor='<%# Convert.ToInt32(Eval("LockStatus"))==1 ?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
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
                <asp:PostBackTrigger ControlID="btnDownloadExcel" />
                 <asp:PostBackTrigger ControlID="btnUploadExcel" />  
                  <asp:PostBackTrigger ControlID="btnExcelReport" />  
            </Triggers>
            </asp:UpdatePanel>
      <script type="text/javascript">
          $(document).ready(function () {
              $('.multi-select-demo').multiselect({
                  includeSelectAllOption: true,
                  maxHeight: 200,
                  enableFiltering: true,
                  filterPlaceholder: 'Search',
                  enableCaseInsensitiveFiltering: true,
              });
          });
          var parameter = Sys.WebForms.PageRequestManager.getInstance();
          parameter.add_endRequest(function () {
              $(document).ready(function () {
                  $('.multi-select-demo').multiselect({
                      includeSelectAllOption: true,
                      maxHeight: 200,
                      enableFiltering: true,
                      filterPlaceholder: 'Search',
                      enableCaseInsensitiveFiltering: true,
                  });
              });
          });
    </script>

</asp:Content>

