<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Passed_Out_StudentList.aspx.cs" Inherits="ACADEMIC_Passed_Out_StudentList" ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }

 

        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

 

        .modalPopup
        {
            background-color: #FFFFFF;
            width: 700px;
            border: 3px solid #858788; /*#0DA9D0;*/
            border-radius: 12px;
            padding: 0;
        }

 

            .modalPopup.right
            {
                right: 0 !important;
                top: 0 !important;
                left: inherit !important;
                border-radius: 12px;
                height: 100%;
            }

 

            .modalPopup .header
            {
                background-color: #858788; /*#2FBDF1;*/
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }

 

            .modalPopup .body
            {
                padding: 10px;
                min-height: 50px;
                text-align: center;
                font-weight: bold;
            }

 

            .modalPopup .footer
            {
                padding: 6px;
            }

 

            .modalPopup .yes, .modalPopup .no
            {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }

 

            .modalPopup .yes
            {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }

 

            .modalPopup .no
            {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }

 

        element.style
        {
            font-family: Verdana !important;
            font-size: 10pt !important;
            color: red !important;
        }
    </style>

    <%-- <div style="z-index: 1; position:fixed; top: 50%; left: 600px;">
        <asp:UpdateProgress ID="updPanel" runat="server" AssociatedUpdatePanelID="updPassedOut"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size:50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>--%>

      <asp:UpdateProgress ID="updPanel" runat="server" AssociatedUpdatePanelID="updPassedOut"
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


    <asp:UpdatePanel ID="updPassedOut" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>Pass Out Student Allotment</b></h3>
                            <div class="box-tools pull-right">
                              <%--  <div style="color: Red; font-weight: bold">
                                    &nbsp;&nbsp;&nbsp;Note : Only pending request are shown here <%--Note : * Marked fields are mandatory--%>
                                </div>
                            </div>
                        
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:HiddenField ID="hdnCount" runat="server" Value="0" />
                                    <asp:Panel ID="pnlStudentList" runat="server" Visible="false">
                                        <asp:ListView ID="lvStudentList" runat="server" >
                                            <LayoutTemplate>
                                                <table class="table table-hover table-bordered table-striped" id="divsessionlist" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <%--<th style="text-align:center;">Edit
                                                    </th>--%>
                                                            <th>
                                                                <asp:CheckBox ID="chkHead" runat="server" onclick="selectAll(this);"/>
                                                            </th>
                                                            <th>Student Name
                                                            </th>
                                                            <th>Regno
                                                            </th>
                                                            <th>Degree
                                                            </th>
                                                            <th>Branch
                                                            </th>
                                                            <th>Mobile
                                                            </th>
                                                            <th>Email
                                                            </th>
                                                            <th>
                                                                Passout Session
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <%--<td style="text-align:center;">
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit1.gif"
                                                    CommandArgument='<%# Eval("SESSIONNO")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                      TabIndex="12" />
                                            </td>--%>
                                                    <td>
                                                        <asp:CheckBox ID="chkstud" runat="server" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("STUDNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REGNO")%>
                                                        <asp:Label ID="lblIDNO" runat="server" Text='<%# Eval("IDNO")%>' ToolTip='<%# Eval("REGNO")%>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEGREE")%>
                                                        <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREENO")%>' ToolTip='<%# Eval("COLLEGE_ID")%>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("BRANCH")%>
                                                        <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BRANCHNO")%>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                      <%# Eval("MOBILENO")%>

                                                    </td>
                                                    <td>
                                                          <%# Eval("EMAILID")%>
                                                    </td>
                                                     <td>
                                                          <%# Eval("PASS_SESSION")%>
                                                         <asp:Label ID="lblSession" runat="server" Text='<%# Eval("SESSIONNO")%>' Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                <div>
                                    <p class="text-center">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return validateStudents();"
                                            TabIndex="9" CssClass="btn btn-success" OnClick="btnSubmit_Click" Visible="false"/>
                                        <asp:Button ID="btnGetPassStudents" runat="server" Text="Get Pass Out Student for Allotment"
                                            TabIndex="9" CssClass="btn btn-info" OnClick="btnGetPassStudents_Click" />
                                               <asp:Button ID="btnDisplayStudExcel" runat="server" Text="Display Pass Out Student List Till Date After Allotment"
                                            TabIndex="9" CssClass="btn btn-info" OnClick="btnDisplayStudExcel_Click"/>
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                                            TabIndex="10" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                                        
                                </div>
                                <%--<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />--%>


                            </p>
                                     
                            </div>
                        </div>
                    </div>
                        </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers >
            <asp:PostBackTrigger ControlID="btnDisplayStudExcel" />
        </Triggers>
    </asp:UpdatePanel>

    <script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#divsessionlist').DataTable({
                scrollX: 'true'
            });
        }

    </script>
    <script>
        function selectAll(chkhd) {
            var count = document.getElementById('<%=hdnCount.ClientID%>').value;
            for (var i = 0 ; i < count; i++) {
                var chkstud = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentList_ctrl' + i + '_chkstud');
                if (chkhd.checked) {
                    chkstud.checked = true;
                }
                else {
                    chkstud.checked = false;
                }
            }
        }

        function validateStudents() {
            var stucount = false;
            var count;
            var count = document.getElementById('<%=hdnCount.ClientID%>').value;
            for (var i = 0 ; i < count; i++) {
                var chkstud = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentList_ctrl' + i + '_chkstud');
                if (chkstud.type == 'checkbox') {
                    if (chkstud.checked) {
                        stucount=true;
                    }
                }
            }
            if (!stucount) {
                alert('Please select atleast one pass out student from the list.');
                return false;
            }
            else {
                return true;
            }
        }

        function RequestRemark(ID) {

            var myArr = new Array();
            myString = "" + ID.id + "";
            myArr = myString.split("_");
            var index = myArr[3];
            // alert(myString);
            var remark = document.getElementById('ctl00_ContentPlaceHolder1_lvBranchChange_' + index + '_hdnRequestRemark').value;
            alert(remark);
        }
    </script>
</asp:Content>


