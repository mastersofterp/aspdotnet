<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="NoDuesProforma.aspx.cs" Inherits="ACADEMIC_NoDuesProforma" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Panel ID="pnlStart" runat="server">


    <div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title"><b>NO DUES PROFORMA FOR STUDENTS</b></h3>     
                <div class="pull-right">
                         <div style="color: Red; font-weight: bold">
                             &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory</div>
                </div>            
            </div>
           
                <div class="box-body">
                    <asp:Panel ID="pnlSearch" runat="server">
                        <div class="col-md-12">
                            <div class="col-md-4" id="tblSearch">
                                <label>Enter Roll No. </label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtEnrollno" runat="server"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEnrollno"
                            Display="None" ErrorMessage="Please Enter Enroll No" ValidationGroup="show"
                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <span class="input-group-btn">
                                        <asp:Button ID="btnSearch" runat="server" OnClick="btnProceed_Click"
                                            Text="Show" CssClass="btn btn-primary" ValidationGroup="show" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="show"
                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </span>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div id="divCourses" runat="server" visible="false">           
            <div class="col-md-12" id="tblInfo" runat="server">
                        <fieldset>
                            <legend>No Dues Fee Proforma for Student</legend>
                            <div class="col-md-6">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item">
                                        <b>Student Name :</b><a class="pull-right">
                                            <asp:Label ID="lblName" runat="server" Font-Bold="True" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Father's Name :</b><a class="pull-right">
                                            <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" Style="font-weight: 700" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>IdNo :</b><a class="pull-right">
                                            <asp:Label ID="lblIdno" runat="server" Font-Bold="False" Style="font-weight: 700" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Semester :</b><a class="pull-right">
                                            <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Degree :</b><a class="pull-right">
                                            <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>
                                   
                                </ul>

                            </div>
                            <div class="col-md-6">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item">
                                        <a class="">
                                            <asp:Image ID="imgPhoto" runat="server" Width="128px" Height="120px" /></a>
                                    </li>
                                     <li class="list-group-item">
                                        <b>Branch :</b><a class="pull-right">
                                            <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>DueFee :</b><a class="pull-right">
                                            <asp:Label ID="lblDueFee" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>

                                </ul>

                            </div>
                        </fieldset>
                        <div class="col-md-12">
                            <p class="text-center">
                                 <asp:Button ID="btnReport" runat="server" 
                                 onclick="btnShow_Click1" Text="Report" ValidationGroup="backsem" 
                                 CssClass="btn btn-primary"/>
                                <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                                Text="Cancel" ValidationGroup="backsem" CssClass="btn btn-warning"/>
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
</div>

    </asp:Panel>

    <script type="text/javascript" language="javascript">
        //        //To change the colour of a row on click of check box inside the row..
        //        $("tr :checkbox").live("click", function() {
        //        $(this).closest("tr").css("background-color", this.checked ? "#FFFFD2" : "#FFFFFF");
        //        });

        function SelectAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }


        function CheckSelectionCount(chk) {
            var count = -1;
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (count == 2) {
                    chk.checked = false;
                    alert("You have reached maximum limit!");
                    return;
                }
                else if (count < 2) {
                    if (e.checked == true) {
                        count += 1;
                    }
                }
                else {
                    return;
                }
            }
        }


       
    </script>
</asp:Content>

