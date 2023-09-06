<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BranchChangeAcademicApproval.aspx.cs" Inherits="ACADEMIC_BranchChangeAcademicApproval" %>

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
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSession"
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
    <asp:UpdatePanel ID="updSession" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>Programme/Branch Change Academic Approval</b></h3>
                            <div class="box-tools pull-right">
                                <div style="color: Red; font-weight: bold">
                                    &nbsp;&nbsp;&nbsp;Note : Only pending request are shown here <%--Note : * Marked fields are mandatory--%>
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                   
                                    
                                    <asp:Panel ID="pnlBranchChange" runat="server" Visible="false">
                                        <asp:ListView ID="lvBranchChange" runat="server" OnItemDataBound="lvBranchChange_ItemDataBound">
                                            <LayoutTemplate>
                                                <table class="table table-hover table-bordered table-striped" id="divsessionlist" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <%--<th style="text-align:center;">Edit
                                                    </th>--%>
                                                            <th>SRNO
                                                            </th>
                                                            <th>Student Name
                                                            </th>
                                                            <th>REGNO
                                                            </th>
                                                            <th>Old Programme/Branch
                                                            </th>
                                                            <th>New Programme/Branch
                                                            </th>
                                                            <th>Approve Remark
                                                            </th>
                                                            <th>Approve
                                                            </th>
                                                            <th>
                                                                Request Remark
                                                            </th>
                                                            <th>
                                                                Document Preview
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
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("STUDNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REGNO")%>
                                                        <asp:Label ID="lblIDNO" runat="server" Text='<%# Eval("IDNO")%>' ToolTip='<%# Eval("REGNO")%>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("OLD_BRANCH")%>
                                                        <asp:Label ID="lblOldBranch" runat="server" Text='<%# Eval("OLD_BRANCH")%>' ToolTip='<%# Eval("STUDNAME")%>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NEW_BRANCH")%>
                                                        <asp:Label ID="lblNewBranch" runat="server" Text=' <%# Eval("NEW_BRANCH")%>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFirstLevelRemark" runat="server" MaxLength="300"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkApprove" runat="server" />
                                                    </td>
                                                    <td >
                                                       <%-- <%# Eval("REQUEST_REMARK") %>--%>
                                                        <asp:LinkButton ID="lnkRequestRemark" runat="server" Text="View Remark" OnClientClick="RequestRemark(this);" CommandName='<%# Eval("REQUEST_REMARK") %>'></asp:LinkButton>
                                                        <asp:HiddenField ID="hdnRequestRemark" runat="server" Value='<%# Eval("REQUEST_REMARK") %>'/>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lnkView" runat="server" CommandName='<%# Eval("FILE_NAME") %>' OnClick="lnkView_Click" CommandArgument='<%# Eval("PREVIEW_PATH") %>'  ToolTip='<%# Eval("PREVIEW_PATH") %>'><image style="height:25px" src="../IMAGES/view.gif" data-toggle="modal" data-target="#myModal22"></image></asp:LinkButton>
                                                        <asp:Label ID="lblView" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </>
                                   
                                <div>
                                    <p class="text-center">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click"
                                            TabIndex="9" CssClass="btn btn-success" />
                                        <asp:Button ID="btnReport" runat="server" Text="Requested Programme/Branch Change Report (Excel)" OnClick="btnReport_Click"
                                            TabIndex="10" CssClass="btn btn-info" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                            TabIndex="10" CssClass="btn btn-danger" />
                                </div>
                                <%--<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />--%>


                            </p>
                                      <div class="col-md-4" style="margin-bottom:20px">
                                        <label>College</label>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                     <div class="col-md-4" style="margin-bottom:20px">
                                        <label>Degree</label>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div style="color:red;font-weight:bold;margin-top:50px" class="col-md-5">Note : Approve Remark Maximum limit 300 characters.</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers >
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="lvBranchChange" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>

     <ajaxToolKit:ModalPopupExtender ID="mpeViewDocument" BehaviorID="mpeViewDocument" runat="server" PopupControlID="pnlPopup"
        TargetControlID="lnkFake" CancelControlID="btnClose" BackgroundCssClass="modalBackground"
        OnOkScript="ResetSession()">
    </ajaxToolKit:ModalPopupExtender>

     <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup">
        <div class="header">
            Document
                 
        </div>
        <div class="body">
            <iframe runat="server" width="680px" height="550px" id="iframeView"></iframe>
        </div>
        <div class="footer" align="right">
            <asp:Button ID="btnClose" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Larger" Text="Close" CssClass="no" />
        </div>
    </asp:Panel>


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
      <script type="text/javascript" language="javascript">

          /* To collapse and expand page sections */
          function toggleExpansion(imageCtl, divId) {
              if (document.getElementById(divId).style.display == "block") {
                  document.getElementById(divId).style.display = "none";
                  imageCtl.src = "../../images/expand_blue.jpg";
              }
              else if (document.getElementById(divId).style.display == "none") {
                  document.getElementById(divId).style.display = "block";
                  imageCtl.src = "../../images/collapse_blue.jpg";
              }
          }
    </script>
    <script>
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


