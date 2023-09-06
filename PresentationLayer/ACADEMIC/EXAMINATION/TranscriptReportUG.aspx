<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="TranscriptReportUG.aspx.cs" Inherits="ACADEMIC_EXAMINATION_TranscriptReportUG" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="updpnlExam" runat="server">
        <ContentTemplate>
            <div style="z-index: 1; position: absolute; top: 50px; left: 600px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updpnlExam">
                    <ProgressTemplate>
                        <asp:Image ID="imgLoad" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                        <span style="font-size: 8pt">Loading...</span>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">TRANSCRIPT REPORT UG</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-md-4">
                                <div class="input-group margin" style="z-index: 0">
                                   <%-- <label>Exam Roll No.</label>--%>
                                    <asp:TextBox ID="txtEnrollmentSearch" runat="server" ValidationGroup="submit" ToolTip="Enter Exam Roll No."></asp:TextBox>
                                    <span class="input-group-btn" >
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" OnClick="btnSearch_Click"
                                            ValidationGroup="submit" />
                                        <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtEnrollmentSearch"
                                            Display="None" ErrorMessage="Please Enter Reg no." ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" Width="20%" ShowSummary="False" ValidationGroup="submit" />
                                </div>                               
                                </div>
                            <div class="col-md-12">
                                <p class="text-center">
                             <%--    <div class="col-md-2" style="margin-top:10px">--%>
                                     <asp:Button ID="btnTranscriptWithHeader" runat="server" Text="Transcript Report"  ValidationGroup="submit"
                                OnClick="btnTranscriptWithHeader_Click" CssClass="btn btn-info"/>
                               <%-- </div>
                                <div class="col-md-2" style="margin-top:10px">--%>
                                    <asp:Button ID="btnReport" runat="server" Text="All Result" CssClass="btn btn-primary" ValidationGroup="submit" OnClick="btnReport_Click" />
                             <%--   </div>
                                 <div class="col-md-1" style="margin-left:-50px; margin-top:10px">--%>
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel"  CssClass="btn btn-warning" OnClick="btnCancel_Click" />
<%--                            </div>--%>
                                    </p>
                            </div>

                            <div class="col-md-12">
                                <fieldset>
                                    <legend>Student Information
                                        <div class="box-tools pull-right">
                                            <img id="img1" style="cursor: pointer;" alt="" src="../../IMAGES/collapse_blue.jpg"
                                                onclick="javascript:toggleExpansion(this,'divStudentInfo')" />
                                        </div>
                                          <div id="divStudentInfo" style="display: block;">
                                        <div class="col-md-6">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item">
                                                    <b>Exam Roll No. :</b><a class="pull-right">
                                                        <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Student Name  :</b><a class="pull-right">
                                                        <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Father's Name :</b><a class="pull-right">
                                                        <asp:Label ID="lblMName" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Branch :</b><a class="pull-right">
                                                       <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Semester :</b><a class="pull-right">
                                                       <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Landline No :</b><a class="pull-right">
                                                        <asp:Label ID="lblLLNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Mobile No  :</b><a class="pull-right">
                                                         <asp:Label ID="lblMobNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Date of Birth :</b><a class="pull-right">
                                                         <asp:Label ID="lblDOB" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                  <li class="list-group-item">
                                                    <b>Caste :</b><a class="pull-right">
                                                          <asp:Label ID="lblCaste" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                
                                            </ul>
                                        </div>
                                        <div class="col-md-6">
                                            <ul class="list-group list-group-unbordered">
                                                 <li class="list-group-item">
                                                    <b>Photo :</b>
                                                </li>
                                                <li>
                                                    <a >
                                                         <asp:Image ID="imgPhoto" runat="server" Height="120px" Width="128px" /></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Category :</b><a class="pull-right">
                                                         <asp:Label ID="lblCategory" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Nationality :</b><a class="pull-right">
                                                         <asp:Label ID="lblNationality" runat="server" Text='<%# Eval("") %>' Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Religion :</b><a class="pull-right">
                                                         <asp:Label ID="lblReligion" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Local Address :</b><a class="pull-right">
                                                         <asp:Label ID="lblLAdd" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>City :</b><a class="pull-right">
                                                         <asp:Label ID="lblCity" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Permanent Address :</b><a class="pull-right">
                                                         <asp:Label ID="lblPAdd" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>City :</b><a class="pull-right">
                                                         <asp:Label ID="lblPCity" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
</div>
                                    </legend>
                                    
                                </fieldset>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
           
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">

        /* To collapse and expand page sections */
        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../../IMAGES/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../../IMAGES/collapse_blue.jpg";
            }
        }
    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

