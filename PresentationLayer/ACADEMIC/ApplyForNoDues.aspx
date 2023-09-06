<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ApplyForNoDues.aspx.cs" Inherits="ACADEMIC_ApplyForNoDues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_pnlAAPaList .dataTables_scrollHeadInner
        {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updTeach"
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
    <asp:UpdatePanel ID="updTeach" runat="server">

        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <%--<asp:Label ID="lblHeading" runat="server" Text=""></asp:Label>--%>
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row" >
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudName" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblRegno" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item d-none"><b>Roll No :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudRollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblDYtxtSchoolname" runat="server" Font-Bold="true"></asp:Label>
                                                :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudClg" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblDYtxtDegree" runat="server" Font-Bold="true"></asp:Label>
                                                :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudDegree" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblstudSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Mobile No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblMobileNo" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>E-Mail ID :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblMailId" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Gender :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblGender" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Passing Year :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblYearPass" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="col-12 btn-footer" id="divchk" runat="server">
                                <%--<div class="row">--%>
                                <asp:CheckBox ID="chkselect" runat="server" AutoPostBack="true" OnCheckedChanged="chkselect_CheckedChanged" />
                                <b>Click here to check Approval Status</b>
                                <%--</div>--%>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnSubmit" runat="server" Text="Submit"  OnClick="btnSubmit_Click" Visible="false"
                                    CssClass="btn btn-primary"> Submit</asp:LinkButton>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnStatus" runat="server" Text="Check Approval Status"  OnClick="btnStatus_Click" Visible="false"
                                    CssClass="btn btn-primary" />
                                <asp:Button ID="btnPrint" runat="server" Text="Print NoDues Certificate"  OnClick="btnPrint_Click" Visible="false"
                                    CssClass="btn btn-primary" />
                            </div>
                            <br />
                            <br />
                            <div class="col-12">
                                <div class="row">
                                    <asp:ListView ID="lvTrackingDetails" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>Approval Status Details</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="example2">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                           <%-- <th>Sr No.</th>
                                                            <th>Registration Number</th>
                                                            <th>Apply Date</th>
                                                            <th><asp:Label ID="lblHODStatus" runat="server"></asp:Label></th>
                                                            <th>Pending Reason By HOD</th>
                                                            <th>T&P Approved Status</th>
                                                            <th>Pending Reason By T&P</th>
                                                            <th>Library Approved Status</th>
                                                            <th>Pending Reason By Library</th>
                                                            <th>Registrar Approved Status</th>
                                                            <th>Pending Reason By Registrar</th>
                                                            <th>Finance Approved Status</th>
                                                            <th>Pending Reason By Finance</th>--%>
                                                             <th>Sr No.</th>
                                                            <th>Registration Number</th>
                                                            <th>Apply Date</th>
                                                            <th><asp:Label ID="lblappproval1Status" runat="server"></asp:Label></th>
                                                            <th><asp:Label ID="lblpendingreasonappproval1" runat="server"></asp:Label></th> 
                                                            <th><asp:Label ID="lblappproval2status" runat="server"></asp:Label></th>                                                           
                                                            <th><asp:Label ID="lblpendingappproval2" runat="server"></asp:Label></th>                                                           
                                                            <th><asp:Label ID="lblappproval3status" runat="server"></asp:Label></th>
                                                            <th><asp:Label ID="lblpendingappproval3" runat="server"></asp:Label></th>
                                                            <th><asp:Label ID="lblappproval4status" runat="server"></asp:Label></th>          
                                                            <th><asp:Label ID="lblpendingresappproval4" runat="server"></asp:Label></th>          
                                                            <th><asp:Label ID="lblappproval5status" runat="server"></asp:Label></th>          
                                                            <th><asp:Label ID="lblpendingappproval5" runat="server"></asp:Label></th>       
                                                          
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
                                                    <%#Container.DataItemIndex+1%>
                                                </td>
                                                <td><%# Eval("REGNO")%></td>
                                                <td><%# Eval("APPLY_DATE","{0: dd/MM/yyyy}")%></td>
                                                <td>
<%--                                                 <asp:Label ID="Label1" runat="server" Text='<%# Eval("APPROVAL1")%>' Visible="false"></asp:Label>--%>
<%--                                                    <asp:HiddenField ID="hdapproval" runat="server" Value='<%# Eval("APPROVAL1")%>>' />--%>
                                                    <asp:Label ID="lblHOD" runat="server" Text='<%# Eval("HOD STATUS")%>' ForeColor='<%# (Convert.ToInt32(Eval("HOD_APPROVED") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblapp1reject" runat="server" Text='<%# Eval("REJECT_REASON_BY_1_APPROVAL")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTandP" runat="server" Text='<%# Eval("T_AND_P STATUS")%>' ForeColor='<%# (Convert.ToInt32(Eval("T_AND_P_APPROVED") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblapp2reject" runat="server" Text='<%# Eval("REJECT_REASON_BY_2_APPROVAL")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLibrary" runat="server" Text='<%# Eval("LIBRARY STATUS")%>' ForeColor='<%# (Convert.ToInt32(Eval("LIBRARY_APPROVED") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblapp3reject" runat="server" Text='<%# Eval("REJECT_REASON_BY_3_APPROVAL")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRegistrar" runat="server" Text='<%# Eval("REGISTRAR STATUS")%>' ForeColor='<%# (Convert.ToInt32(Eval("REGISTRAR_APPROVED") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblapp4reject" runat="server" Text='<%# Eval("REJECT_REASON_BY_4_APPROVAL")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFinanace" runat="server" Text='<%# Eval("FINANCE STATUS")%>' ForeColor='<%# (Convert.ToInt32(Eval("FINANCE_DEPT_APPROVED") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblapp5reject" runat="server" Text='<%# Eval("REJECT_REASON_BY_5_APPROVAL")%>'></asp:Label>
                                                </td>
                                                <%--<td><%# Eval("HOD STATUS")%></td>--%>
                                                <%--<td><%# Eval("LIBRARY STATUS")%></td>
                                                    <td><%# Eval("REGISTRAR STATUS")%></td>
                                                    <td><%# Eval("FINANCE STATUS")%></td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
     <div id="divMsg" runat="server" />

</asp:Content>

