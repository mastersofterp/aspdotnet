<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudViewTPlan.aspx.cs" Inherits="Itle_StudViewTPlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../plugins/ckeditor/ckeditor.js"></script>
    <script src="../plugins/ckeditor/ckeditor_basic.js"></script>
    <%--   <script src="ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="ckeditor/ckeditor_basic.js" type="text/javascript"></script>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>



    <%-- <style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>--%>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TEACHING PLAN LIST FOR STUDENT</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlStudentView" runat="server">
                                <asp:Panel ID="pnlView" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <%-- <div class=" sub-heading">
                                                    <h5>Teaching Plan List</h5>
                                                </div>--%>
                                            <div class="col-lg-5 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Session Term :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label></a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-7 col-md-12 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Course Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblCourseName" runat="server" Font-Bold="True"></asp:Label></a>
                                                    </li>
                                                </ul>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-12 mt-3" id="divDesc" runat="server">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Details About Selected Teaching Plan</h5>
                                                </div>
                                            </div>

                                            <div class="col-lg-5 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Subject :</b>
                                                        <a class="sub-label">
                                                            <span id="tdSubject" runat="server">
                                                                <%# Eval("SUBJECT") %>
                                                            </span>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div class="form-group col-lg-7 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Description</label>
                                                </div>
                                                <asp:Panel ID="pnlTeachingPlan" runat="server" BorderColor="Navy" BorderWidth="3px"
                                                    Heigh="150px">
                                                    <div style="height: 150px; background-color: #D1EDD1; padding-top: 25px; padding-left: 5px; padding-left: 5px;">
                                                        <div id="divDescription" runat="server" style="height: 95%; padding-left: 10px; padding-top: 5px; padding-right: 5px; padding-bottom: 5px; background-color: White; border-radius: 25px; overflow: auto">
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn btn-primary"
                                                ToolTip="Click here to Go Back" />
                                        </div>
                                    </div>
                                </asp:Panel>

                                <div class="col-12 mt-4">
                                    <div id="DivTeachingPlanList" runat="server" visible="false">
                                        <div class="sub-heading">
                                            <h5>Teaching Plan List</h5>
                                        </div>

                                        <asp:Panel ID="pnlViewList" runat="server">
                                            <asp:ListView ID="lvTPlan" runat="server" DataKeyNames="TP_NO">
                                                <LayoutTemplate>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Unit No.</th>
                                                                 <th>Topic No.</th>
                                                                <th>Topic</th>
                                                                <th>Schedule Date</th>
                                                               
                                                                <%-- <th>Start Time</th>
                                                                <th>End Time</th>--%>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>

                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <%--<td>
                                                            <asp:LinkButton ID="lnkPlan" runat="server" Text='<%#Eval("TOPIC_COVERED")%>' CommandArgument='<%#Eval("TP_NO")%>'
                                                                OnClick="lnkPlan_Click"></asp:LinkButton>
                                                        </td>--%>
                                                         <td>
                                                            <%# Eval("UNIT_NO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("LECTURE_NO")%>
                                                        </td>
                                                         <td>
                                                            <%#Eval("TOPIC_COVERED")%>' 
                                                               
                                                        </td>
                                                        <td>
                                                            <%# Eval("DATE","{0:dd-MMM-yyyy}")%>
                                                        </td>
                                                       
                                                         <%--<td>
                                                            <%# Eval("START_DATE", "{0:hh:mm:ss tt}")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("END_DATE", "{0:hh:mm:ss tt}")%>
                                                        </td>--%>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>

                                    </div>
                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
