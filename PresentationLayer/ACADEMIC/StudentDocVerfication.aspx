<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="StudentDocVerfication.aspx.cs"
    Inherits="ACADEMIC_StudentDocVerfication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   <%-- <script type="text/javascript">
        function Validate() {
            var isValid = $("#dvListView input[type=checkbox]:checked").length > 0;
            if (!isValid) {
                alert("Verifying at least one document");
            }
            return isValid;
        }
    </script>--%>

    <%--<script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <script language="javascript" type="text/javascript">
        function chk(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Enter Numbers Only!');
                txt.focus();
                return;
            }
        }
    </script>

    <%-- <script language="javascript" type="text/javascript">
        function valid() {
            var lst = document.getElementById('<%=lvDocumentList.ClientID%>');
            var chk = document.getElementById('<%= chkSelect.ClientID%>');
        }
    </script>--%>

    <script type="text/javascript">
        function Validate() {
            var isValid = $("#dvListView input[type=checkbox]:checked").length > 0;
            if (!isValid) {
                alert("Verifying at least one document");
            }
            return isValid;
        }
    </script>

    <%--<script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updLists"
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

    <asp:UpdatePanel ID="updLists" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">ADMISSION  DOCUMENT VERIFICATIONN</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <%--<div class="row">
                                        <div class="form-group col-md-4">
                                            <label><span style="color: red;">*</span> Select Session:</label>
                                            <asp:DropDownList ID="ddlSession" Width="20%" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Session."
                                                ValidationGroup="submit" Font-Bold="True">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Application ID</label>
                                        </div>
                                        <asp:TextBox ID="txtAppID" runat="server" ToolTip="Please Enter Application ID" ValidationGroup="submit" MaxLength="16"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None" ErrorMessage="Please Enter Application ID"
                                        ValidationGroup="submit" ControlToValidate="txtAppID"></asp:RequiredFieldValidator>
                                        <%-- <asp:RegularExpressionValidator Display = "None" ControlToValidate = "txtAppID" ID="RegularExpressionValidator1" ValidationExpression = "^[\s\S]{0,16}$" runat="server" ErrorMessage="Maximum 16 characters allowed."></asp:RegularExpressionValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnShow" runat="server" Text="Show Details" ValidationGroup="submit" OnClick="btnShow_Click" CssClass="btn btn-primary"  />
                                        <asp:Button ID="btnPdfreport" runat="server" Text="Doc Verification Student List" OnClick="btnPdfreport_Click"  CssClass="btn btn-info" visible ="false" />
                                        <asp:Button ID="btncancel" runat="server" Text="Cancel"  OnClick="btncancel_Click" CssClass="btn btn-warning" />
                                            
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ValidationGroup="submit" ShowSummary="False" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ValidationGroup="submit1" ShowSummary="False" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="fldstudent" runat="server"  visible="false">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Student Information</h5>
                                            </div>
                                        </div>
                                        
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Application ID :</b>
                                                    <a class="sub-label"><asp:Label ID="lblAppID" runat="server" Text="" Font-Bold="true"></asp:Label> </a>
                                                </li>
                                                <li class="list-group-item"><b>Name of Candidate :</b>
                                                    <a class="sub-label"><asp:Label ID="lblName" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>        
                                            </ul>
                                        </div>

                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Mobile No :</b>
                                                    <a class="sub-label"><asp:Label ID="lblMobileNo" runat="server" Text="" Font-Bold="true"></asp:Label> </a>
                                                </li>
                                                <li class="list-group-item"><b>Email ID :</b>
                                                    <a class="sub-label"><asp:Label ID="lblEmail_id" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>        
                                            </ul>
                                        </div>

                                        <div class="col-lg-4 col-md-6 col-12" id="TREntranceDetails" runat="server" visible="false">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Entrance Exam Details :</b>
                                                    <a class="sub-label"><asp:Label ID="lblexamname" runat="server" Font-Bold="true"></asp:Label> </a>
                                                </li>
                                                <li class="list-group-item"><b>Roll No. :</b>
                                                    <a class="sub-label"><asp:Label ID="lblJEERollNo" runat="server" Font-Bold="true"></asp:Label></a>
                                                </li>  
                                                <li class="list-group-item"><b>Total :</b>
                                                    <a class="sub-label"><asp:Label ID="lblJtotal" runat="server" Font-Bold="true"></asp:Label></a>
                                                </li>        
                                            </ul>
                                        </div>
                                    </div>
                               
                                    <div id="dvListView" class="col-12">
                                        <asp:ListView ID="lvDocumentList" runat="server" Visible="false">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Checklist Of Documents</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblstudDetails">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th style="text-align: center;">Sr.No.
                                                            </th>
                                                            <th>Certificate List 
                                                            </th>
                                                            <th style="text-align: center;">Checklist
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr >
                                                    <td align="center">
                                                        <%# Eval("SRNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DOCUMENTNAME")%>
                                                    </td>
                                                    <td align="center">
                                                        <asp:CheckBox ID="chkSelect" runat="server"  AutoPostBack="true"  ToolTip='<%# Eval("DOCUMENTNO")%>' Checked='<%# Eval("DOCUMENT_STATUS").ToString() == "1" ? true : false %>'/>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>

                                    <div class="col-12 btn-footer">  
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit"  CssClass="btn btn-primary"  OnClick="btnSubmit_Click" OnClientClick="return Validate()" />
                                        <asp:Button ID="btnSingleReport" runat="server" Text="Report" CssClass="btn btn-info"  OnClick="btnSingleReport_Click" />
                                    </div>

                                    <div class="col-12 btn-footer">  
                                        <b> <asp:Label ID="lblNote1" runat="server" Text=""></asp:Label></b>
                                        <div id="divMsg" runat="server">
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div> 
                    </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnSingleReport" />
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btncancel" />
            <asp:PostBackTrigger ControlID="btnPdfreport" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
