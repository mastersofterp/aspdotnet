<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="GrievanceReply.aspx.cs"
    Inherits="GrievanceRedressal_Transaction_GrievanceReply" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>--%>


    <%--<style type="text/css">
    .disable {pointer-events:none}
    </style>


    <script>
        function DeptLevel() {            
            $("#tabs").find('li a').eq(1).hide();
            $("#tabs").find('li a').eq(2).hide();
        } 
     </script>

     <script>
         function InstituteLevel() {             
             $("#tabs").find('li a').eq(0).hide();
             $("#tabs").find('li a').eq(2).hide();
         }  
     </script>

     <script>

         function CentralLevel() {            
             $("#tabs").find('li a').eq(0).hide();
             $("#tabs").find('li a').eq(1).hide();
         }
     </script>

     <script>
         function DeptInstiLevel() {
             $("#tabs").find('li a').eq(0).show();
             $("#tabs").find('li a').eq(1).show();
             $("#tabs").find('li a').eq(2).hide();
         }

     </script>

    <script>
        function InstiCentral() {
            $("#tabs").find('li a').eq(0).hide();
            $("#tabs").find('li a').eq(1).show();
            $("#tabs").find('li a').eq(2).show();
        }

     </script>

    <script>
        function DeptCentral() {
            $("#tabs").find('li a').eq(0).show();
            $("#tabs").find('li a').eq(1).hide();
            $("#tabs").find('li a').eq(2).show();
        }

     </script>

    <script>
        function DeptInstiCentralLevel() {
            $("#tabs").find('li a').eq(0).show();
            $("#tabs").find('li a').eq(1).show();
            $("#tabs").find('li a').eq(2).show();
        }

     </script>--%>

    <%-- <asp:UpdatePanel ID="upGrievanceReply" runat="server">
        <ContentTemplate>--%>
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div4" runat="server"></div>
                <%--<div class="box-header with-border">
                    <h3 class="box-title">GRIEVANCE REPLY</h3>
                </div>--%>
                <asp:Panel ID="pnlComWise" runat="server">
                    <div class="box-body">
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="nav-item">

                                    <a class="nav-link active" data-toggle="tab" href="#tab_1">Department Level Grievances</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#tab_2">Institute Level Grievances</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#tab_3">Central Level Grievances</a>
                                </li>
                            </ul>

                            <div class="tab-content" id="my-tab-content">

                                <div class="tab-pane active" id="tab_1">
                                    <div>
                                        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upGrievanceReply"
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
                                    <asp:UpdatePanel ID="upGrievanceReply" runat="server">
                                        <ContentTemplate>
                                            <div class="box-body">
                                                <asp:Panel ID="pnlGrReply" runat="server">
                                                    <%-- <div class="col-12">
                                                    <div class="sub-heading"><h5>Grievance Reply</h5></div>
                                                </div>--%>

                                                    <asp:Panel ID="pnlStudentDetails" runat="server" Visible="false">
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Student Name </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtstudname" runat="server" MaxLength="5" CssClass="form-control" TabIndex="1" ToolTip="Student Name" Enabled="false" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Degree </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtDegree" runat="server" MaxLength="5" CssClass="form-control" TabIndex="2" Enabled="false" ToolTip="Degree Name" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Branch </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtbranch" runat="server" MaxLength="5" Enabled="false" CssClass="form-control" TabIndex="3" ToolTip="Branch Name" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Semester </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtsemester" runat="server" MaxLength="5" Enabled="false" CssClass="form-control" TabIndex="4" ToolTip="Semester Name" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Mobile No. </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtMobile" runat="server" MaxLength="20" CssClass="form-control" TabIndex="5" ToolTip="Mobile No" Enabled="false" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Email Id </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="60" CssClass="form-control" Enabled="false" ToolTip="Email Id" TabIndex="6" onblur="validateEmail(this.value);"> </asp:TextBox>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Grievance Type </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtGrievanceType" runat="server" MaxLength="20" CssClass="form-control" TabIndex="7" Enabled="false" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Grievance Application Date </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtGADate" runat="server" MaxLength="20" CssClass="form-control" TabIndex="8" ToolTip="Grievance Application Date " Enabled="false" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Grievance Application No. </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtGAD" runat="server" MaxLength="20" CssClass="form-control" TabIndex="9" ToolTip="Grievance Application No " Enabled="false" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Student Admission No. </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtAdmissionNo" runat="server" MaxLength="20" CssClass="form-control" TabIndex="10" ToolTip="Student Admission No " Enabled="false" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Grievance </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtGrievance" runat="server" MaxLength="3000" CssClass="form-control" ToolTip="Enter Grievance"
                                                                        TextMode="MultiLine" TabIndex="11" Enabled="false"></asp:TextBox>
                                                                </div>

                                                                <div class="col-12" id="divDocument" runat="server">
                                                                    <div class="sub-heading">
                                                                        <h5>Download Document</h5>
                                                                    </div>
                                                                    <%--<div class="form-group col-md-8">--%>
                                                                    <asp:ListView ID="lvDownload" runat="server">
                                                                        <EmptyDataTemplate>
                                                                            <br />
                                                                            <asp:Label ID="ibler" runat="server" Text=""></asp:Label>
                                                                        </EmptyDataTemplate>
                                                                        <LayoutTemplate>
                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </table>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr class="item">
                                                                                <td>
                                                                                    <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("GRIV_ATTACHMENT"),Eval("GAID"),Eval("STUD_IDNO"))%>'><%# Eval("GRIV_ATTACHMENT")%></asp:HyperLink>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                    <%-- </div>--%>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12">
                                                            <asp:Panel ID="pnlCommittees" runat="server">
                                                                <asp:ListView ID="lvCommiteeReply" runat="server" Visible="false">
                                                                    <LayoutTemplate>
                                                                        <div id="lgv1">
                                                                            <div class="sub-heading">
                                                                                <h5>Committee Reply </h5>
                                                                            </div>
                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <%-- <th>EDIT
                                                                                        </th>--%>
                                                                                        <th>COMMITTEE 
                                                                                        </th>
                                                                                        <th>REPLY
                                                                                        </th>
                                                                                        <%--<th>FILE
                                                                                    </th>--%>
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
                                                                            <%--  <td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                                CommandArgument='<%# Eval("GRCT_ID") %>' ImageUrl="~/images/edit.gif"
                                                                ToolTip="Edit Record"  />
                                                            <%--   <asp:HiddenField ID="hdnsrno" runat="server" Value='<%# Eval("SRNO") %>' OnClick="btnEdit_Click" />--%>
                                                                            <%--</td>--%>
                                                                            <td>
                                                                                <%# Eval("GR_COMMITTEE")%>
                                                                                <%-- <asp:HiddenField ID="hdnuano" runat="server" Value='<%# Eval("UANO") %>' />--%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("REPLY")%>
                                                                                <%-- <asp:HiddenField ID="hdndesignId" runat="server" Value='<%# Eval("GR_DESIGID") %>' />--%>
                                                                            </td>
                                                                            <%--<td>
                                                                             <asp:HyperLink ID="lnkDownloadF" runat="server" Target="_blank" NavigateUrl='<%# GetFileName(Eval("REPLY_ATTACHMENT"),Eval("REPLY_ID"),Eval("GAID"))%>'><%# Eval("REPLY_ATTACHMENT")%></asp:HyperLink>
                                                                        </td>--%>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>

                                                        <div class="col-12" id="divReply" runat="server" visible="false">
                                                            <div class="row">
                                                                <div class="form-group col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label><span style="color: #FF0000">*</span>Reply by Committee </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtGrReply" runat="server" MaxLength="3000" CssClass="form-control" ToolTip="Enter Reply"
                                                                        TextMode="MultiLine" TabIndex="13"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvreply" runat="server" ControlToValidate="txtGrReply"
                                                                        Display="None" ErrorMessage="Please Enter Reply For Selected Grievance." ValidationGroup="GrievanceValidate" SetFocusOnError="true">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>


                                                                <%-- <div class="form-group col-md-4">
                                                      <div class="label-dynamic">
                                                        <label>Attach File</label>
                                                    </div>
                                                        <asp:UpdatePanel ID="updateFile" runat="server">
                                                            <ContentTemplate>
                                                        <asp:FileUpload ID="flup" runat="server" ToolTip="Select file to upload" TabIndex="9" />
                                                                </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="btnSave" />
                                                            </Triggers>
                                                            </asp:UpdatePanel>                                                                                                                      
                                                     </div>--%>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer" id="divButton" runat="server" visible="false">
                                                            <asp:Button ID="btnSave" runat="server" Text="submit" ValidationGroup="GrievanceValidate"
                                                                CssClass="btn btn-primary" TabIndex="10" ToolTip="Click here to Submit" OnClick="btnSave_Click" />
                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                                                CssClass="btn btn-warning" TabIndex="11" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                                            <asp:Button ID="btnBack" runat="server" CausesValidation="false" Visible="false"
                                                                Text="Back" CssClass="btn btn-primary" TabIndex="12" ToolTip="Click here to Go back to Previous Menu" />
                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="GrievanceValidate"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        </div>
                                                    </asp:Panel>


                                                    <div class="col-12" runat="server" id="DeptLv">
                                                        <asp:Panel ID="pnlGrievanceReply" runat="server">
                                                            <asp:ListView ID="lvGrReplyDept" runat="server">
                                                                <LayoutTemplate>
                                                                    <div id="lgv1">
                                                                        <div class="sub-heading">
                                                                            <h5>List of Grievances</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Edit
                                                                                    </th>
                                                                                    <th>Student Name
                                                                                    </th>
                                                                                    <th>Grievance Type
                                                                                    </th>
                                                                                    <th>Grievance Detail
                                                                                    </th>
                                                                                    <th>Grievance Date
                                                                                    </th>
                                                                                    <%--<th>Mobile no
                                                                                </th>--%>
                                                                                    <th>Application No
                                                                                    </th>
                                                                                    <th>Branch
                                                                                    </th>
                                                                                    <th>Year
                                                                                    </th>
                                                                                    <th>Committee Reply
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
                                                                            <asp:ImageButton ID="btnEditRecord" runat="server" CausesValidation="false"
                                                                                CommandArgument='<%# Eval("GAID") %>' ImageUrl="~/Images/edit.png" OnClick="btnEditRecord_Click"
                                                                                ToolTip="Edit Record" CommandName='<%# Eval("STUD_IDNO")%>' AlternateText='<%# Eval("REPLY_ID")%>' />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("STUDNAME")%>                                                   
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("GT_NAME")%>                                                   
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("GRIEVANCE")%>                                                   
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("GR_APPLICATION_DATE")%>                                                   
                                                                        </td>
                                                                        <%--<td>
                                                                        <%# Eval("MOBILE_NO")%>                                                    
                                                                    </td>--%>
                                                                        <td>
                                                                            <%# Eval("GRIV_CODE")%>                                                    
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SHORTNAME")%>                                                    
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("YEARNAME")%>                                                    
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("REPLY") %>
                                                                        </td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                </asp:Panel>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSave" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                </div>

                                <div class="tab-pane fade" id="tab_2">
                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatePInstitue"
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
                                    <asp:UpdatePanel ID="updatePInstitue" runat="server">
                                        <ContentTemplate>
                                            <div class="box-body">
                                                <asp:Panel ID="pnlInstitute" runat="server">
                                                    <%-- <div class="col-12">
                                                    <div class="sub-heading"><h5>Grievance Reply</h5></div>
                                                </div>--%>

                                                    <asp:Panel ID="pnlStudentDetailsI" runat="server" Visible="false">
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Student Name </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtstudnameI" runat="server" MaxLength="5" CssClass="form-control" TabIndex="1" ToolTip="Student Name" Enabled="false" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Degree </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtDegreeI" runat="server" MaxLength="5" CssClass="form-control" TabIndex="2" Enabled="false" ToolTip="Degree Name" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Branch </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtbranchI" runat="server" MaxLength="5" Enabled="false" CssClass="form-control" TabIndex="3" ToolTip="Branch Name" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Semester </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtsemesterI" runat="server" MaxLength="5" Enabled="false" CssClass="form-control" TabIndex="4" ToolTip="Semester Name" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Mobile No. </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtMobileI" runat="server" MaxLength="20" CssClass="form-control" TabIndex="5" ToolTip="Mobile No" Enabled="false" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Email Id </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtEmailI" runat="server" MaxLength="60" CssClass="form-control" Enabled="false" ToolTip="Email Id" TabIndex="6" onblur="validateEmail(this.value);"> </asp:TextBox>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Grievance Type </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtGrievanceTypeI" runat="server" MaxLength="20" CssClass="form-control" TabIndex="7" Enabled="false" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Grievance Application Date </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtGADateI" runat="server" MaxLength="20" CssClass="form-control" TabIndex="8" ToolTip="Grievance Application Date " Enabled="false" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Grievance Application No. </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtGAI" runat="server" MaxLength="20" CssClass="form-control" TabIndex="9" ToolTip="Grievance Application No " Enabled="false" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Student Admission No. </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtAdmissionNoI" runat="server" MaxLength="20" CssClass="form-control" TabIndex="10" ToolTip="Student Admission No " Enabled="false" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Grievance </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtGrievanceI" runat="server" MaxLength="3000" CssClass="form-control" ToolTip="Enter Grievance"
                                                                        TextMode="MultiLine" TabIndex="11" Enabled="false"></asp:TextBox>
                                                                </div>

                                                                <div class="col-12" id="divDocumentI" runat="server">
                                                                    <div class="sub-heading">
                                                                        <h5>Download Document</h5>
                                                                    </div>
                                                                    <%--<div class="form-group col-md-8">--%>
                                                                    <asp:ListView ID="lvDownloadI" runat="server">
                                                                        <EmptyDataTemplate>
                                                                            <br />
                                                                            <asp:Label ID="iblerI" runat="server" Text=""></asp:Label>
                                                                        </EmptyDataTemplate>
                                                                        <LayoutTemplate>
                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </table>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr class="item">
                                                                                <td>
                                                                                    <asp:HyperLink ID="lnkDownloadI" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("GRIV_ATTACHMENT"),Eval("GAID"),Eval("STUD_IDNO"))%>'><%# Eval("GRIV_ATTACHMENT")%></asp:HyperLink>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                    <%-- </div>--%>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12">
                                                            <asp:Panel ID="pnlCommitteesI" runat="server">
                                                                <asp:ListView ID="lvCommiteeReplyI" runat="server" Visible="false">
                                                                    <LayoutTemplate>
                                                                        <div id="lgv1">
                                                                            <div class="sub-heading">
                                                                                <h5>Reply of Previous Committees</h5>
                                                                            </div>
                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <%-- <th>EDIT
                                                                    </th>--%>
                                                                                        <th>COMMITTEE LEVEL
                                                                                        </th>
                                                                                        <th>REPLY
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
                                                                            <%--  <td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                                CommandArgument='<%# Eval("GRCT_ID") %>' ImageUrl="~/images/edit.gif"
                                                                ToolTip="Edit Record"  />
                                                            <%--   <asp:HiddenField ID="hdnsrno" runat="server" Value='<%# Eval("SRNO") %>' OnClick="btnEdit_Click" />--%>
                                                                            <%--</td>--%>
                                                                            <td>
                                                                                <%# Eval("GR_COMMITTEE")%>
                                                                                <%-- <asp:HiddenField ID="hdnuano" runat="server" Value='<%# Eval("UANO") %>' />--%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("REPLY")%>
                                                                                <%-- <asp:HiddenField ID="hdndesignId" runat="server" Value='<%# Eval("GR_DESIGID") %>' />--%>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>

                                                        <div class="col-12" id="divReplyI" runat="server" visible="false">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label><span style="color: #FF0000">*</span>Reply by Committee </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtGrReplyI" runat="server" MaxLength="3000" CssClass="form-control" ToolTip="Enter Reply"
                                                                        TextMode="MultiLine" TabIndex="9"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGrReplyI"
                                                                        Display="None" ErrorMessage="Please Enter Reply For Selected Grievance." ValidationGroup="GrievanceValidateI" SetFocusOnError="true">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer" id="divButtonI" runat="server" visible="false">
                                                            <asp:Button ID="btnSaveI" runat="server" Text="submit" ValidationGroup="GrievanceValidateI"
                                                                CssClass="btn btn-primary" TabIndex="11" ToolTip="Click here to Submit" OnClick="btnSaveI_Click" />
                                                            <asp:Button ID="btnBackI" runat="server" CausesValidation="false" Visible="false"
                                                                Text="Back" CssClass="btn btn-primary" TabIndex="12" ToolTip="Click here to Go back to Previous Menu" />
                                                            <asp:Button ID="btnCancelI" runat="server" Text="Cancel" CausesValidation="false"
                                                                CssClass="btn btn-warning" TabIndex="13" ToolTip="Click here to Reset" OnClick="btnCancelI_Click" />
                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="GrievanceValidateI"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        </div>
                                                    </asp:Panel>


                                                    <div class="col-12">
                                                        <asp:Panel ID="pnlGrievanceReplyI" runat="server">
                                                            <asp:ListView ID="lvGrReplyDeptI" runat="server">
                                                                <LayoutTemplate>
                                                                    <div id="lgv1">
                                                                        <div class="sub-heading">
                                                                            <h5>List of Grievances</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Edit
                                                                                    </th>
                                                                                    <th>Student Name
                                                                                    </th>
                                                                                    <th>Grievance Type
                                                                                    </th>
                                                                                    <th>Grievance Detail
                                                                                    </th>
                                                                                    <th>Grievance Date
                                                                                    </th>
                                                                                    <%--<th>Mobile no
                                                                                </th>--%>
                                                                                    <th>Application No
                                                                                    </th>
                                                                                    <th>Branch
                                                                                    </th>
                                                                                    <th>Year
                                                                                    </th>
                                                                                    <th>Committee Reply
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" style="width: 50%" />
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEditRecordI" runat="server" CausesValidation="false"
                                                                                CommandArgument='<%# Eval("GAID") %>' ImageUrl="~/Images/edit.png" OnClick="btnEditRecordI_Click"
                                                                                ToolTip="Edit Record" CommandName='<%# Eval("STUD_IDNO")%>' AlternateText='<%# Eval("REPLY_ID")%>' />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("STUDNAME")%>                                                   
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("GT_NAME")%>                                                   
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("GRIEVANCE")%>                                                   
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("GR_APPLICATION_DATE")%>                                                   
                                                                        </td>
                                                                        <%--<td>
                                                                        <%# Eval("MOBILE_NO")%>                                                    
                                                                    </td>--%>
                                                                        <td>
                                                                            <%# Eval("GRIV_CODE")%>                                                    
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SHORTNAME")%>                                                    
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("YEARNAME")%>                                                    
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("REPLY") %>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="tab-pane fade" id="tab_3">
                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePCentral"
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
                                    <asp:UpdatePanel ID="UpdatePCentral" runat="server">
                                        <ContentTemplate>
                                            <div class="box-body">
                                                <asp:Panel ID="pnlcentrallv" runat="server">
                                                    <%--<div class="col-12">
                                                    <div class="sub-heading"><h5>Grievance Reply</h5></div>
                                                </div>--%>

                                                    <asp:Panel ID="pnlStudentDetailsC" runat="server" Visible="false">
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Student Name </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtstudnameC" runat="server" MaxLength="5" CssClass="form-control" TabIndex="1" ToolTip="Student Name" Enabled="false" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Degree </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtDegreeC" runat="server" MaxLength="5" CssClass="form-control" TabIndex="2" Enabled="false" ToolTip="Degree Name" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Branch </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtbranchC" runat="server" MaxLength="5" Enabled="false" CssClass="form-control" TabIndex="3" ToolTip="Branch Name" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Semester </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtsemesterC" runat="server" MaxLength="5" Enabled="false" CssClass="form-control" TabIndex="4" ToolTip="Semester Name" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Mobile No </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtMobileC" runat="server" MaxLength="20" CssClass="form-control" TabIndex="5" ToolTip="Mobile No" Enabled="false" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Email Id </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtEmailC" runat="server" MaxLength="60" CssClass="form-control" Enabled="false" ToolTip="Email Id" TabIndex="6" onblur="validateEmail(this.value);"> </asp:TextBox>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Grievance Type </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtGrievanceTypeC" runat="server" MaxLength="20" CssClass="form-control" TabIndex="7" Enabled="false" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Grievance Application Date </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtGADateC" runat="server" MaxLength="20" CssClass="form-control" TabIndex="8" ToolTip="Grievance Application Date " Enabled="false" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Grievance Application No. </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtGAC" runat="server" MaxLength="20" CssClass="form-control" TabIndex="9" ToolTip="Grievance Application No " Enabled="false" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Student Admission No. </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtAdmissionNoC" runat="server" MaxLength="20" CssClass="form-control" TabIndex="10" ToolTip="Student Admission No " Enabled="false" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Grievance </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtGrievanceC" runat="server" MaxLength="3000" CssClass="form-control" ToolTip="Enter Grievance"
                                                                        TextMode="MultiLine" TabIndex="11" Enabled="false"></asp:TextBox>
                                                                </div>

                                                                <div class="col-12" id="divDocumentC" runat="server">
                                                                    <div class="sub-heading">
                                                                        <h5>Download Document</h5>
                                                                    </div>
                                                                    <%--<div class="form-group col-md-8">--%>
                                                                    <asp:ListView ID="lvDownloadC" runat="server">
                                                                        <EmptyDataTemplate>
                                                                            <br />
                                                                            <asp:Label ID="iblerC" runat="server" Text=""></asp:Label>
                                                                        </EmptyDataTemplate>
                                                                        <LayoutTemplate>
                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </table>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr class="item">
                                                                                <td>
                                                                                    <asp:HyperLink ID="lnkDownloadC" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("GRIV_ATTACHMENT"),Eval("GAID"),Eval("STUD_IDNO"))%>'><%# Eval("GRIV_ATTACHMENT")%></asp:HyperLink>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                    <%-- </div>--%>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12">
                                                            <asp:Panel ID="pnlCommitteesC" runat="server">
                                                                <asp:ListView ID="lvCommiteeReplyC" runat="server" Visible="false">
                                                                    <LayoutTemplate>
                                                                        <div id="lgv1">
                                                                            <div class="sub-heading">
                                                                                <h5>Reply of Previous Committees</h5>
                                                                            </div>
                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <%-- <th>EDIT
                                                                                    </th>--%>
                                                                                        <th>COMMITTEE LEVEL
                                                                                        </th>
                                                                                        <th>REPLY
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
                                                                            <%--  <td>
                                                                        <asp:ImageButton ID="btnEditC" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                                            CommandArgument='<%# Eval("GRCT_ID") %>' ImageUrl="~/images/edit.gif"
                                                                            ToolTip="Edit Record"  />
                                                                        <asp:HiddenField ID="hdnsrno" runat="server" Value='<%# Eval("SRNO") %>' OnClick="btnEditC_Click" />
                                                                        </td>--%>
                                                                            <td>
                                                                                <%# Eval("GR_COMMITTEE")%>
                                                                                <%-- <asp:HiddenField ID="hdnuano" runat="server" Value='<%# Eval("UANO") %>' />--%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("REPLY")%>
                                                                                <%-- <asp:HiddenField ID="hdndesignId" runat="server" Value='<%# Eval("GR_DESIGID") %>' />--%>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>

                                                        <div class="col-12" id="divReplyC" runat="server" visible="false">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label><span style="color: #FF0000">*</span> Reply by Committee </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtGrReplyC" runat="server" MaxLength="3000" CssClass="form-control" ToolTip="Enter Reply"
                                                                        TextMode="MultiLine" TabIndex="9"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtGrReplyC"
                                                                        Display="None" ErrorMessage="Please Enter Reply For Selected Grievance." ValidationGroup="GrievanceValidateC" SetFocusOnError="true">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer" id="divButtonC" runat="server" visible="false">
                                                            <asp:Button ID="btnSaveC" runat="server" Text="submit" ValidationGroup="GrievanceValidateC"
                                                                CssClass="btn btn-primary" TabIndex="12" ToolTip="Click here to Submit" OnClick="btnSaveC_Click" />

                                                            <asp:Button ID="btnbackC" runat="server" CausesValidation="false" Visible="false"
                                                                Text="Back" CssClass="btn btn-primary" TabIndex="13" ToolTip="Click here to Go back to Previous Menu" />

                                                            <asp:Button ID="btnCancelC" runat="server" Text="Cancel" CausesValidation="false"
                                                                CssClass="btn btn-warning" TabIndex="14" ToolTip="Click here to Reset" OnClick="btnCancelC_Click" />
                                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="GrievanceValidateC"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        </div>
                                                    </asp:Panel>

                                                    <div class="col-12">
                                                        <asp:Panel ID="pnlGrievanceReplyC" runat="server">
                                                            <asp:ListView ID="lvGrReplyDeptC" runat="server">
                                                                <LayoutTemplate>
                                                                    <div id="lgv1">
                                                                        <div class="sub-heading">
                                                                            <h5>List of Grievances </h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Edit
                                                                                    </th>
                                                                                    <th>Student Name
                                                                                    </th>
                                                                                    <th>Grievance Type
                                                                                    </th>
                                                                                    <th>Grievance Detail
                                                                                    </th>
                                                                                    <th>Grievance Date
                                                                                    </th>
                                                                                    <%--<th>Mobile no
                                                                                </th>--%>
                                                                                    <th>Application No
                                                                                    </th>
                                                                                    <th>Branch
                                                                                    </th>
                                                                                    <th>Year
                                                                                    </th>
                                                                                    <th>Committee Reply
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" style="width: 50%" />
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEditRecordC" runat="server" CausesValidation="false"
                                                                                CommandArgument='<%# Eval("GAID") %>' ImageUrl="~/Images/edit.png" OnClick="btnEditRecordC_Click"
                                                                                ToolTip="Edit Record" CommandName='<%# Eval("STUD_IDNO")%>' AlternateText='<%# Eval("REPLY_ID")%>' />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("STUDNAME")%>                                                   
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("GT_NAME")%>                                                   
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("GRIEVANCE")%>                                                   
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("GR_APPLICATION_DATE")%>                                                   
                                                                        </td>
                                                                        <%--<td>
                                                                        <%# Eval("MOBILE_NO")%>                                                    
                                                                    </td>--%>
                                                                        <td>
                                                                            <%# Eval("GRIV_CODE")%>                                                    
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SHORTNAME")%>                                                    
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("YEARNAME")%>                                                    
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("REPLY") %>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                </asp:Panel>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlSubGrivWise" runat="server">
                    <div class="box-body">
                        <asp:UpdatePanel ID="updSubGriv" runat="server">
                            <ContentTemplate>
                                <div class="box-body">
                                    <asp:Panel ID="pnlGrivList" runat="server">
                                        <%-- <div class="col-12">
                                                    <div class="sub-heading"><h5>Grievance Reply</h5></div>
                                                </div>--%>

                                        <asp:Panel ID="pnlStuList" runat="server" Visible="false">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Student Name </label>
                                                        </div>
                                                        <asp:TextBox ID="txtSubStud" runat="server" MaxLength="5" CssClass="form-control" TabIndex="1" ToolTip="Student Name" Enabled="false" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Degree </label>
                                                        </div>
                                                        <asp:TextBox ID="txtDeg" runat="server" MaxLength="5" CssClass="form-control" TabIndex="2" Enabled="false" ToolTip="Degree Name" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Branch </label>
                                                        </div>
                                                        <asp:TextBox ID="txtBranchS" runat="server" MaxLength="5" Enabled="false" CssClass="form-control" TabIndex="3" ToolTip="Branch Name" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Semester </label>
                                                        </div>
                                                        <asp:TextBox ID="txtSem" runat="server" MaxLength="5" Enabled="false" CssClass="form-control" TabIndex="4" ToolTip="Semester Name" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Mobile No. </label>
                                                        </div>
                                                        <asp:TextBox ID="txtCont" runat="server" MaxLength="20" CssClass="form-control" TabIndex="5" ToolTip="Mobile No" Enabled="false" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Email Id </label>
                                                        </div>
                                                        <asp:TextBox ID="txtEmailIds" runat="server" MaxLength="60" CssClass="form-control" Enabled="false" ToolTip="Email Id" TabIndex="6" onblur="validateEmail(this.value);"> </asp:TextBox>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Grievance Type </label>
                                                        </div>
                                                        <asp:TextBox ID="txtGrivT" runat="server" MaxLength="20" CssClass="form-control" TabIndex="7" Enabled="false" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Grievance Application Date </label>
                                                        </div>
                                                        <asp:TextBox ID="txtAppDt" runat="server" MaxLength="20" CssClass="form-control" TabIndex="8" ToolTip="Grievance Application Date " Enabled="false" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Grievance Application No. </label>
                                                        </div>
                                                        <asp:TextBox ID="txtAppNo" runat="server" MaxLength="20" CssClass="form-control" TabIndex="9" ToolTip="Grievance Application No " Enabled="false" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Student Admission No. </label>
                                                        </div>
                                                        <asp:TextBox ID="txtStuAdm" runat="server" MaxLength="20" CssClass="form-control" TabIndex="10" ToolTip="Student Admission No " Enabled="false" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Grievance </label>
                                                        </div>
                                                        <asp:TextBox ID="txtGrivDe" runat="server" MaxLength="3000" CssClass="form-control" ToolTip="Enter Grievance"
                                                            TextMode="MultiLine" TabIndex="11" Enabled="false"></asp:TextBox>
                                                    </div>

                                                    <div class="col-12" id="divDoc" runat="server">
                                                        <div class="sub-heading">
                                                            <h5>Download Document</h5>
                                                        </div>

                                                        <asp:ListView ID="lvSubGriv" runat="server">
                                                            <EmptyDataTemplate>
                                                                <br />
                                                                <asp:Label ID="ibler" runat="server" Text=""></asp:Label>
                                                            </EmptyDataTemplate>
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item">
                                                                    <td>
                                                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("GRIV_ATTACHMENT"),Eval("GAID"),Eval("STUD_IDNO"))%>'><%# Eval("GRIV_ATTACHMENT")%></asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                        <%-- </div>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <asp:Panel ID="pnlGrivType" runat="server">
                                                    <asp:ListView ID="lvReply" runat="server" Visible="false">
                                                        <LayoutTemplate>
                                                            <div id="lgv1">
                                                                <div class="sub-heading">
                                                                    <h5>Previous Reply </h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <%-- <th>EDIT
                                                                                        </th>--%>
                                                                            <%--<th>COMMITTEE 
                                                                            </th>--%>
                                                                            <th>REPLY
                                                                            </th>
                                                                            <%--<th>FILE
                                                                                    </th>--%>
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
                                                                <%--  <td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                                CommandArgument='<%# Eval("GRCT_ID") %>' ImageUrl="~/images/edit.gif"
                                                                ToolTip="Edit Record"  />
                                                            <%--   <asp:HiddenField ID="hdnsrno" runat="server" Value='<%# Eval("SRNO") %>' OnClick="btnEdit_Click" />--%>
                                                                <%--</td>--%>
                                                                <%--<td>
                                                                    <%# Eval("GR_COMMITTEE")%>
                                                                    <%-- <asp:HiddenField ID="hdnuano" runat="server" Value='<%# Eval("UANO") %>' />--%>
                                                                <%--</td>--%>
                                                                <td>
                                                                    <%# Eval("REPLY")%>
                                                                    <%-- <asp:HiddenField ID="hdndesignId" runat="server" Value='<%# Eval("GR_DESIGID") %>' />--%>
                                                                </td>
                                                                <%--<td>
                                                                             <asp:HyperLink ID="lnkDownloadF" runat="server" Target="_blank" NavigateUrl='<%# GetFileName(Eval("REPLY_ATTACHMENT"),Eval("REPLY_ID"),Eval("GAID"))%>'><%# Eval("REPLY_ATTACHMENT")%></asp:HyperLink>
                                                                        </td>--%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>

                                            <div class="col-12">
                                                <asp:Panel ID="pnlstudent" runat="server">
                                                    <asp:ListView ID="lvStuRply" runat="server" Visible="false">
                                                        <LayoutTemplate>
                                                            <div id="lgv1">
                                                                <div class="sub-heading">
                                                                    <h5>Student Reply </h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>STUDENT REPLY
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
                                                                    <%# Eval("STUD_REMARK")%>                                                                
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>

                                            <div class="col-12" id="divGrivReply" runat="server" visible="false">
                                                <div class="row">
                                                    <div class="form-group col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label><span style="color: #FF0000">*</span>Reply by Committee </label>
                                                        </div>
                                                        <asp:TextBox ID="txtSubReply" runat="server" MaxLength="3000" CssClass="form-control" ToolTip="Enter Reply"
                                                            TextMode="MultiLine" TabIndex="13"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtGrReply"
                                                            Display="None" ErrorMessage="Please Enter Reply For Selected Grievance." ValidationGroup="GrievanceValidate" SetFocusOnError="true">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label></label>
                                                        </div>
                                                        <asp:RadioButton ID="rbtSatified" runat="server" Checked="false" Text="Closed" TabIndex="3" AutoPostBack="true" />                                                        
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer" id="divGrivbutton" runat="server" visible="false">
                                                <asp:Button ID="btnGrivReply" runat="server" Text="submit" ValidationGroup="GrievanceValidate"
                                                    CssClass="btn btn-primary" TabIndex="10" ToolTip="Click here to Submit" OnClick="btnGrivReply_Click" />
                                                <asp:Button ID="btnReplyCancel" runat="server" Text="Cancel" CausesValidation="false"
                                                    CssClass="btn btn-warning" TabIndex="11" ToolTip="Click here to Reset" OnClick="btnReplyCancel_Click" />
                                                <asp:Button ID="btnReplyBack" runat="server" CausesValidation="false" Visible="false"
                                                    Text="Back" CssClass="btn btn-primary" TabIndex="12" ToolTip="Click here to Go back to Previous Menu" OnClick="btnReplyBack_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="GrievanceValidate"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>
                                        </asp:Panel>


                                        <div class="col-12" runat="server" id="Div5">
                                            <asp:Panel ID="pnldetail" runat="server">
                                                <asp:ListView ID="lvStuDetail" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="lgv1">
                                                            <div class="sub-heading">
                                                                <h5>List of Grievances</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Student Name
                                                                        </th>
                                                                        <th>Grievance Type
                                                                        </th>
                                                                        <%-- <th>Grievance Detail
                                                                        </th>--%>
                                                                        <th>Grievance Date
                                                                        </th>
                                                                        <%--<th>Mobile no
                                                                                </th>--%>
                                                                        <th>Application No
                                                                        </th>
                                                                        <%--<th>Branch
                                                                        </th>
                                                                        <th>Year
                                                                        </th>--%>
                                                                        <th>Committee Reply
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
                                                                <asp:ImageButton ID="btnEditGriv" runat="server" CausesValidation="false"
                                                                    CommandArgument='<%# Eval("GAID") %>' ImageUrl="~/Images/edit.png" OnClick="btnEditGriv_Click"
                                                                    ToolTip='<%# Eval("GAT_ID") %>' CommandName='<%# Eval("STUD_IDNO")%>' AlternateText='<%# Eval("REPLY_ID")%>' />
                                                                <%--Enabled='<%#Eval("COMMITTEE_STATUS").ToString() == "C" ? false : true %>'--%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("STUDNAME")%>                                                   
                                                            </td>
                                                            <td>
                                                                <%# Eval("GT_NAME")%>                                                   
                                                            </td>
                                                            <%-- <td>
                                                                <%# Eval("GRIEVANCE")%>                                                   
                                                            </td>--%>
                                                            <td>
                                                                <%# Eval("GR_APPLICATION_DATE", "{0:d}")%>                                                   
                                                            </td>
                                                            <%--<td>
                                                                        <%# Eval("MOBILE_NO")%>                                                    
                                                                    </td>--%>
                                                            <td>
                                                                <%# Eval("GRIV_CODE")%>                                                    
                                                            </td>
                                                            <%-- <td>
                                                                <%# Eval("SHORTNAME")%>                                                    
                                                            </td>
                                                            <td>
                                                                <%# Eval("YEARNAME")%>                                                    
                                                            </td>--%>
                                                            <td>
                                                                <%# Eval("REPLY") %>
                                                            </td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>

                                    </asp:Panel>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnGrivReply" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
