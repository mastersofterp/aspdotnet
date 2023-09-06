<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ChangeCoreSubject.aspx.cs" Inherits="ACADEMIC_ChangeCoreSubject" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <script src="../Content/jquery.js" type="text/javascript"></script>

    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>
    <div id="divOptions" runat="server" visible="false" style="padding:8px 0px 8px 10px;" >
        <div style="width:100px; font-weight:bold;float:left;">Options :</div>
  
    </div>
    <div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">CORE SUBJECT CHANGE</h3>                
            </div>
           
                <div class="box-body" id="divCourses" runat="server" visible="false">
                    <div class="col-md-12">
                        <div class="col-md-3">
                            <label> Examination Roll No</label>
                            <asp:TextBox ID="txtRollNo" runat="server"  MaxLength="15" />                            
                            <asp:RequiredFieldValidator ID="rfvRollNo" ControlToValidate="txtRollNo" runat="server"
                                Display="None" ErrorMessage="Please enter Student Roll No." ValidationGroup="Show" />                           
                        </div>
                        <div class="col-md-2" style="margin-top:25px">
                            <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show"
                                Font-Bold="true" ValidationGroup="Show" CssClass="btn btn-primary" />
                             <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="Show" />
                        </div>
                    </div>
                    <div class="col-md-12" id="tblInfo" runat="server" visible="false">
                        <div class="col-md-6">
                            <ul class="list-group list-group-unbordered">
                                <li class="list-group-item">
                                    <b>Student Name :</b><a class="pull-right">
                                        <asp:Label ID="lblName" runat="server" Font-Bold="True" /></a>
                                </li>
                                <li class="list-group-item">
                                    <b>Father Name :</b><a class="pull-right">
                                        <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" /></a>
                                </li>
                                <li class="list-group-item">
                                    <b>Mother Name :</b><a class="pull-right">
                                        <asp:Label ID="lblMotherName" runat="server" Font-Bold="False" /></a>
                                </li>
                                <li class="list-group-item">
                                    <b>College Name :</b><a class="pull-right">
                                        <asp:Label ID="lblCollege" runat="server" Font-Bold="True"></asp:Label></a>
                                </li>
                                <li class="list-group-item">
                                    <b>Roll No. :</b><a class="pull-right">
                                        <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                </li>
                                <li class="list-group-item">
                                    <b>Registration No.:</b><a class="pull-right">
                                        <asp:Label ID="lblRegNo" runat="server" Style="font-weight: 700"></asp:Label></a>
                                </li>
                            </ul>
                        </div>
                        <div class="col-md-6">
                            <ul class="list-group list-group-unbordered">
                                <li class="list-group-item">
                                    <b>Admission Batch :</b><a class="pull-right">
                                        <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label></a>
                                </li>
                                <li class="list-group-item">
                                    <b>Semester :</b><a class="pull-right">
                                        <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                </li>
                                <li class="list-group-item">
                                    <b>Degree / Branch:</b><a class="pull-right">
                                        <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                </li>
                                <li class="list-group-item">
                                    <b>Phone No. :</b><a class="pull-right">
                                        <asp:Label ID="lblPH" runat="server" Style="font-weight: 700"></asp:Label></a>
                                </li>
                                <li class="list-group-item">
                                    <b>Scheme :</b><a class="pull-right">
                                        <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label></a>
                                </li>
                                <li class="list-group-item">
                                    <b>Core Subject:</b><a class="pull-right">
                                        <asp:Label ID="lblCoreSub" runat="server" Font-Bold="True"></asp:Label></a>
                                </li>
                            </ul>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-4">
                                <label>Subject Name</label>
                                <asp:DropDownList ID="ddlCoreSubjet" runat="server" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4" style="display: none">
                                <div class="col-md-6">
                                    <label>Total Subjects</label>
                                    <asp:TextBox ID="txtAllSubjects" runat="server" Enabled="false" Text="0"
                                        Style="text-align: center;"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label>Total Credits</label>
                                    <asp:TextBox ID="txtCredits" runat="server" Enabled="false" Text="0"
                                        Style="text-align: center;"></asp:TextBox>
                                    <asp:HiddenField ID="hdfCredits" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                </div>
                            </div>
                        </div>

                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnChange" runat="server" Text="Change" CssClass="btn btn-primary"
                                    ValidationGroup="SUBMIT" OnClick="btnChange_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Clear"
                                    ValidationGroup="Show" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="vssubmit" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="SUBMIT" />
                            </p>
                        </div>
                    </div>
                </div>
          
                <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
        
    <div id="divMsg" runat="server">
    </div>
           
        </div>
    </div>
</div>



    
    <table cellpadding="0" cellspacing="0" width="100%">
      <%--  <tr>
            <td class="vista_page_title_bar" style="height: 30px">
                Core Subject Change
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>--%>
    </table>
   <%-- <div id="divOptions" runat="server" visible="false" style="padding:8px 0px 8px 10px;" >
        <div style="width:100px; font-weight:bold;float:left;">Options :</div>
  
    </div>
    <div id="divCourses" runat="server" visible="false">  
        <fieldset class="fieldset">
            <legend class="legend">Core Subject Chagne</legend>--%>
           <%-- <table id="tblSession" runat="server" cellpadding="0" cellspacing="0" width="100%">
              
                <tr >
                    <td style="width: 15%; text-align: right">
                        Examination Roll No :
                    </td>
                    <td class="form_left_text">
                        <asp:TextBox ID="txtRollNo" runat="server" Width="150px" MaxLength="15" />
                        &nbsp;
                        <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" 
                            Font-Bold="true" ValidationGroup="Show" Width="80px" />
                        &nbsp;
                      
                        <asp:RequiredFieldValidator ID="rfvRollNo" ControlToValidate="txtRollNo" runat="server"
                              Display="None" ErrorMessage="Please enter Student Roll No." ValidationGroup="Show" />
                        <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Show" />
                    </td>
                </tr>
            </table>--%>
       <%--     <table cellpadding="0" cellspacing="0" width="100%" id="tblInfo" runat="server" visible="false">--%>
                <%--<tr>
                    <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                        Student Name :
                    </td>
                    <td class="form_left_text">
                        <asp:Label ID="lblName" runat="server" Font-Bold="True" />
                    </td>
                </tr>--%>
               <%-- <tr>
                    <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                        &nbsp;
                    </td>
                    <td class="form_left_text">
                        <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" />&nbsp;
                        <asp:Label ID="lblMotherName" runat="server" Font-Bold="False" />
                    </td>
                </tr>--%>
                <%-- <tr>
                    <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                        College Name :
                    </td>
                    <td class="form_left_text">
                        <asp:Label ID="lblCollege" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>--%>
               <%-- <tr>
                    <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                        Roll No. :
                    </td>
                    <td class="form_left_text">
                        <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>--%>
               <%-- <tr>
                    <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                        Registration No.:</td>
                    <td class="form_left_text">
                        <asp:Label ID="lblRegNo" runat="server" style="font-weight: 700"></asp:Label>
                    </td>
                </tr>--%>
               <%-- <tr>
                    <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                        Admission Batch :
                    </td>
                    <td class="form_left_text">
                        <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label>
                        &nbsp;&nbsp; Semester :
                        <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>--%>
               <%-- <tr>
                    <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                        Degree / Branch :
                    </td>
                    <td class="form_left_text">
                        <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; PH :
                        <asp:Label ID="lblPH" runat="server" style="font-weight: 700"></asp:Label>
                    </td>
                </tr>--%>
               <%-- <tr>
                    <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                        Scheme :
                    </td>
                    <td class="form_left_text">
                        <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>--%>
               <%-- <tr>
                    <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">
                        Core Subject :
                    </td>
                    <td class="form_left_text">
                        <asp:Label ID="lblCoreSub" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>--%>

               <%-- <tr>
                    <td style="width: 15%; text-align: right">
                        Subject Name :
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlCoreSubjet" runat="server" AppendDataBoundItems="True" Width="30%">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>--%>
               <%-- <tr style="display:none">
                    <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;" valign="top">
                        Total Subjects :
                    </td>
                    <td class="form_left_text">
                        <asp:TextBox ID="txtAllSubjects" runat="server" Enabled="false" Text="0" Width="10%"
                            Style="text-align: center;"></asp:TextBox>
                        &nbsp;&nbsp; Total Credits :
                        <asp:TextBox ID="txtCredits" runat="server" Enabled="false" Text="0" Width="10%"
                            Style="text-align: center;"></asp:TextBox>
                        <asp:HiddenField ID="hdfCredits" runat="server" Value="0" />
                        <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                    </td>
                </tr>--%>
               <%-- <tr>
                    <td colspan="2" style="padding: 10px; text-align: center;">
                        <asp:Button ID="btnChange" runat="server" Text="Change" Width="80px"
                           ValidationGroup="SUBMIT"  OnClick="btnChange_Click"/>&nbsp;
                          <asp:Button ID="btnCancel" runat="server" Text="Clear"
                             ValidationGroup="Show" Width="80px" OnClick="btnCancel_Click"/>
                     
                        <asp:ValidationSummary ID="vssubmit" runat="server" DisplayMode="List" ShowMessageBox="true"
                             ShowSummary="false" ValidationGroup="SUBMIT" />
                    </td>
                </tr>--%>
           <%-- </table>
        </fieldset>--%>
    <%--</div>--%>
   <%-- <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
        
    <div id="divMsg" runat="server">
    </div>--%>

    <script type="text/javascript" language="javascript">
        //        //To change the colour of a row on click of check box inside the row..
        //        $("tr :checkbox").live("click", function() {
        //        $(this).closest("tr").css("background-color", this.checked ? "#FFFFD2" : "#FFFFFF");
        //        });

        function SelectAll(headerid, headid, chk) {
            var tbl = '';
            var list = '';
            if (headid == 1) {
                tbl = document.getElementById('tblCurrentSubjects');
                list = 'lvCurrentSubjects';
            }
            else if (headid == 2) {
                tbl = document.getElementById('tblBacklogSubjects');
                list = 'lvBacklogSubjects';
            }
            else {
                tbl = document.getElementById('tblAuditSubjects');
                list = 'lvAuditSubjects';
            }

            try {
                var dataRows = tbl.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        var chkid = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_' + chk;
                        if (headerid.checked) {
                            document.getElementById(chkid).checked = true;
                        }
                        else {
                            document.getElementById(chkid).checked = false;
                        }
                        chkid = '';
                    }
                }
            }
            catch (e) {
                alert(e);
            }
        }

        function ChkHeader(chklst, head, chk) {
            try {
                var headid = '';
                var tbl = '';
                var list = '';
                var chkcnt = 0;
                if (chklst == 1) {
                    tbl = document.getElementById('tblCurrentSubjects');
                    headid = 'ctl00_ContentPlaceHolder1_lvCurrentSubjects_' + head;
                    list = 'lvCurrentSubjects';
                }
                else if (chklst == 2) {
                    tbl = document.getElementById('tblBacklogSubjects');
                    headid = 'ctl00_ContentPlaceHolder1_lvBacklogSubjects_' + head;
                    list = 'lvBacklogSubjects';
                }
                else {
                    tbl = document.getElementById('tblAuditSubjects');
                    headid = 'ctl00_ContentPlaceHolder1_lvAuditSubjects_' + head;
                    list = 'lvAuditSubjects';
                }

                var dataRows = tbl.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        var chkid = document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_' + chk);
                        if (chkid.checked)
                            chkcnt++;
                    }
                }
                if (chkcnt > 0)
                    document.getElementById(headid).checked = true;
                else
                    document.getElementById(headid).checked = false;
            }
            catch (e) {
                alert(e);
            }
        }
        function showConfirm() {
            var ret = confirm('Do you Really want to Confirm/Submit this Courses for Course Registration?');
            if (ret == true)
                return true;
            else
                return false;
        }

    </script>
</asp:Content>

