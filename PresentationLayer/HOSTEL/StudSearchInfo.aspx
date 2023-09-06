<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudSearchInfo.aspx.cs" Inherits="HOSTEL_StudSearchInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updStudSearch"
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
        .input-group .input-group-addon {
            padding: 3px 12px;
        }

        #stddet .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        #ctl00_ContentPlaceHolder1_Panel2 .table-bordered > thead > tr > th {
            border-top: 1px solid #e5e5e5;
        }
    </style>

    <asp:UpdatePanel ID="updStudSearch" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">HOSTEL SEARCH INFORMATION</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trRegno" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Enter Admission No. </label>
                                        </div>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtRegno" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                            <div class="input-group-addon">
                                                <a href="#" title="Search Student for Modification" data-toggle="modal" data-target="#myModal1">
                                                    <asp:Image ID="imgSearch" runat="server" ImageUrl="~/images/search.png" TabIndex="1"
                                                        AlternateText="Search Student by IDNo, Name, Reg. No, Branch, Semester" ToolTip="Search Student by IDNo, Name, Reg. No, Branch, Semester" />
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Roll No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblRegNo" Font-Bold="true" runat="server" />
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Student's Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudName" Font-Bold="true" runat="server" />
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Gender :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSex" Font-Bold="true" runat="server" />
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Date of Admission :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDateOfAdm" Font-Bold="true" runat="server" />
                                                </a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Degree :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDegree" Font-Bold="true" runat="server" />
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Branch :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblBranch" Font-Bold="true" runat="server" />
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Batch :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblBatch" Font-Bold="true" runat="server" />
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Semester :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSemester" Font-Bold="true" runat="server" />
                                                </a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-lg-2 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item">
                                                <asp:Image ID="imgPhoto" runat="server" Width="80px" Height="90px" />
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 mt-3">
                                <div class="nav-tabs-custom">
                                    <ul class="nav nav-tabs">
                                        <li class="nav-item"><a class="nav-link active" href="#Address" data-toggle="tab" aria-expanded="true">Personal Details</a></li>
                                        <li class="nav-item"><a class="nav-link " href="#HostelDetail" data-toggle="tab" aria-expanded="false">Hostel Details</a></li>
                                        <li class="nav-item"><a class="nav-link " href="#AssetDetail" data-toggle="tab" aria-expanded="false">Asset Details</a></li>
                                        <li class="nav-item"><a class="nav-link " href="#AttendanceDetail" data-toggle="tab" aria-expanded="false">Attendance Details</a></li>
                                    </ul>
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="Address">
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-lg-6 col-12">
                                                        <div class="row">
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>Personal Details</h5>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12 col-12 mb-3">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>Father Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblFatherName" runat="server" Font-Bold="true"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Father's Mobile  :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblFatherMobile" runat="server" Font-Bold="true"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Mother Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblMotherName" runat="server" Font-Bold="true"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Mother's Mobile :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblMotherMobile" runat="server" Font-Bold="true"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Email Id :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblEmail" runat="server" Font-Bold="true"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Mobile Number :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblmobile" runat="server" Font-Bold="true"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>City :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblCity" runat="server" Font-Bold="true"></asp:Label></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>State :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblState" runat="server" Font-Bold="true"></asp:Label></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Address :</b>
                                                                        <a class="sub-label">
                                                                            <asp:TextBox ID="lblAddress" CssClass="from-control" ReadOnly="true" TextMode="MultiLine" runat="server" Rows="1" /></a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-lg-6 col-12">
                                                        <div class="row">
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>Bank Details</h5>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>Bank :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblBank" runat="server" Font-Bold="true"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Bank Branch :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblBBranch" runat="server" Font-Bold="true"></asp:Label></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Account No. :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblAccno" runat="server" Font-Bold="true"></asp:Label></a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- /.tab-pane -->
                                        <div class="tab-pane" id="HostelDetail">
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-lg-6 col-12">
                                                        <div class="row">
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>Hostel Details</h5>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12 col-12 mb-3">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>Hostel Session :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblSession" runat="server" Font-Bold="true"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Floor Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblFloor" runat="server" Font-Bold="true"></asp:Label></a>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Room Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblRoom" runat="server" Font-Bold="true"></asp:Label></a>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Hostel Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblHostel" runat="server" Font-Bold="true"></asp:Label></a>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Block Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblBlock" runat="server" Font-Bold="true"></asp:Label></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Mess Type :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblMess" runat="server" Font-Bold="true"></asp:Label></a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-lg-6 col-12">
                                                        <div class="row">
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>Vehicle Details</h5>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>Vehicle Type :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblVehType" runat="server" Font-Bold="true"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Vehicle Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblVehName" runat="server" Font-Bold="true"></asp:Label></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Vehicle No. :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblVehNo" runat="server" Font-Bold="true"></asp:Label></a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <!-- /.tab-pane -->
                                        <div id="AssetDetail" class="tab-pane">
                                            <div class="col-12 mt-3">
                                                <asp:Panel ID="Panel1" runat="server">
                                                    <asp:ListView ID="lvAsset" runat="server">
                                                        <LayoutTemplate>
                                                            <div id="demo-grid">
                                                                <div class="sub-heading">
                                                                    <h5>List of Asset Details</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblFeeEntryGrid">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Room</th>
                                                                            <th>Asset Name
                                                                            </th>
                                                                            <th>Quantity
                                                                            </th>
                                                                            <th>Allotment Code
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
                                                                    <%# Eval("ROOM_NAME") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ASSET_NAME")%>
                                                                </td>
                                                                <td><%# Eval("QUANTITY")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ALLOTMENT_CODE")%>
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>

                                        <!-- /.tab-pane -->
                                        <div class="tab-pane" id="AttendanceDetail">
                                            <div class="col-12 mt-3">
                                                <asp:Panel ID="Panel2" runat="server">
                                                    <asp:ListView ID="lvAttendance" runat="server">
                                                        <LayoutTemplate>
                                                            <div id="demo-grid">
                                                                <div class="sub-heading">
                                                                    <h5>List of Attendance Details</h5>
                                                                </div>
                                                                <div class="table table-responsive">
                                                                    <table class="table table-striped table-bordered nowrap " style="width: 100%" id="tblAttendanceGrid">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Month
                                                                                </th>
                                                                                <th>1</th>
                                                                                <th>2</th>
                                                                                <th>3</th>
                                                                                <th>4</th>
                                                                                <th>5</th>
                                                                                <th>6</th>
                                                                                <th>7</th>
                                                                                <th>8</th>
                                                                                <th>9</th>
                                                                                <th>10</th>
                                                                                <th>11</th>
                                                                                <th>12</th>
                                                                                <th>13</th>
                                                                                <th>14</th>
                                                                                <th>15</th>
                                                                                <th>16</th>
                                                                                <th>17</th>
                                                                                <th>18</th>
                                                                                <th>19</th>
                                                                                <th>20</th>
                                                                                <th>21</th>
                                                                                <th>22</th>
                                                                                <th>23</th>
                                                                                <th>24</th>
                                                                                <th>25</th>
                                                                                <th>26</th>
                                                                                <th>27</th>
                                                                                <th>28</th>
                                                                                <th>29</th>
                                                                                <th>30</th>
                                                                                <th>31</th>
                                                                                <th>Present</th>
                                                                                <th>Absent</th>
                                                                                <th>Leave</th>
                                                                                <th>Late</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>

                                                                <td>
                                                                    <%# Eval("MONTHNO")%>-
                                                    <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("MONTH_NAME")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" CssClass="form-control" Text=' <%# Eval("DAY1")%>'></asp:Label><%# Eval("DAY1")%></td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" CssClass="form-control" Text=' <%# Eval("DAY2")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label3" runat="server" CssClass="form-control" Text=' <%# Eval("DAY3")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label4" runat="server" CssClass="form-control" Text=' <%# Eval("DAY4")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server" CssClass="form-control" Text=' <%# Eval("DAY5")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server" CssClass="form-control" Text=' <%# Eval("DAY6")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label7" runat="server" CssClass="form-control" Text=' <%# Eval("DAY7")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label8" runat="server" CssClass="form-control" Text=' <%# Eval("DAY8")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label9" runat="server" CssClass="form-control" Text=' <%# Eval("DAY9")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label10" runat="server" CssClass="form-control" Text=' <%# Eval("DAY10")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label11" runat="server" CssClass="form-control" Text=' <%# Eval("DAY11")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label12" runat="server" CssClass="form-control" Text=' <%# Eval("DAY12")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label13" runat="server" CssClass="form-control" Text=' <%# Eval("DAY13")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label14" runat="server" CssClass="form-control" Text=' <%# Eval("DAY14")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label15" runat="server" CssClass="form-control" Text=' <%# Eval("DAY15")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label16" runat="server" CssClass="form-control" Text=' <%# Eval("DAY16")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label17" runat="server" CssClass="form-control" Text=' <%# Eval("DAY17")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label18" runat="server" CssClass="form-control" Text=' <%# Eval("DAY18")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label19" runat="server" CssClass="form-control" Text=' <%# Eval("DAY19")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label20" runat="server" CssClass="form-control" Text=' <%# Eval("DAY20")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label21" runat="server" CssClass="form-control" Text=' <%# Eval("DAY21")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label22" runat="server" CssClass="form-control" Text=' <%# Eval("DAY22")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label23" runat="server" CssClass="form-control" Text=' <%# Eval("DAY23")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label24" runat="server" CssClass="form-control" Text=' <%# Eval("DAY24")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label25" runat="server" CssClass="form-control" Text=' <%# Eval("DAY25")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label26" runat="server" CssClass="form-control" Text=' <%# Eval("DAY26")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label27" runat="server" CssClass="form-control" Text=' <%# Eval("DAY27")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label28" runat="server" CssClass="form-control" Text='<%# Eval("DAY28")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label29" runat="server" CssClass="form-control" Text=' <%# Eval("DAY29")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label30" runat="server" CssClass="form-control" Text=' <%# Eval("DAY30")%>'></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label31" runat="server" CssClass="form-control" Text=' <%# Eval("DAY31")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label32" runat="server" CssClass="form-control" Text=' <%# Eval("PRESENT")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label33" runat="server" CssClass="form-control" Text=' <%# Eval("ABSENT")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label34" runat="server" CssClass="form-control" Text=' <%# Eval("LEAVE")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label35" runat="server" CssClass="form-control" Text=' <%# Eval("LATE")%>'></asp:Label>
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                            <!--/.col-md-6-->
                                        </div>
                                    </div>

                                </div>
                                <!-- /.tab-pane -->
                                <div id="divMsg" runat="server">
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="myModal1" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Search</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>

                </div>
                <div class="modal-body">
                    <div>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updEdit"
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
                    <asp:UpdatePanel ID="updEdit" runat="server">
                        <ContentTemplate>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Search Criteria </label>
                                        </div>
                                        <asp:RadioButton ID="rbName" runat="server" Text="Name" GroupName="edit" />
                                        <asp:RadioButton ID="rbIdNo" runat="server" Text="IdNo" GroupName="edit" />
                                        <asp:RadioButton ID="rbBranch" runat="server" Text="Branch" GroupName="edit" />
                                        <asp:RadioButton ID="rbEnrollmentNo" runat="server" Text="Enrollmentno" GroupName="edit" />
                                        <asp:RadioButton ID="rbRegNo" runat="server" Text="Rollno" GroupName="edit" Checked="True" />
                                        <asp:RadioButton ID="rbHostel" runat="server" Text="Hostel" GroupName="edit" />
                                        <asp:RadioButton ID="rbRoom" runat="server" Text="Room" GroupName="edit" />
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Search String </label>
                                        </div>
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClientClick="return submitPopup(this.name);" />
                                <asp:Button ID="btnCancelModal" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClientClick="return ClearSearchBox(this.name)" />
                                <button type="button" class="btn btn-warning" data-dismiss="modal">Close</button>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                            </div>

                            <div class="col-12" id="stddet">
                                <asp:ListView ID="lvStudent" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Student Details</h5>
                                        </div>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Name
                                                    </th>
                                                    <th>IdNo
                                                    </th>
                                                    <th>Reg No.
                                                    </th>
                                                    <th>Branch
                                                    </th>
                                                    <th>Hostel
                                                    </th>
                                                    <th>Room
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                    OnClick="lnkId_Click"></asp:LinkButton>
                                            </td>
                                            <td>
                                                <%# Eval("idno")%>
                                            </td>
                                            <td>
                                                <%# Eval("REGNO")%>
                                            </td>
                                            <td>
                                                <%# Eval("SHORTNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("HOSTEL_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("ROOM_NAME")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <script src="../JAVASCRIPTS/AjaxFunctions.js"></script>
    <script src="../JAVASCRIPTS/AutoComplete.js"></script>
    <script src="../JAVASCRIPTS/FeeCollection.js"></script>
    <script src="../JAVASCRIPTS/IITMSTextBox.js"></script>
    <%--<script src="../JAVASCRIPTS/jquery-1.5.1.js"></script>--%>
    <%--<script src="../JAVASCRIPTS/jquery-ui.min_1.js"></script>
    <script src="../JAVASCRIPTS/jquery.min_1.js"></script>
    <script src="../JAVASCRIPTS/jquery.ui.core.js"></script>
    <script src="../JAVASCRIPTS/jquery.ui.widget.js"></script>
    <script src="../JAVASCRIPTS/jquery.ui.datepicker.js"></script>--%>
    <%--<script src="../JAVASCRIPTS/ScrollableTablePlugin_1.0_min.js"></script>--%>
    <script src="../JAVASCRIPTS/JScriptAdmin_Module.js"></script>
    <script src="../JAVASCRIPTS/powerbi.js"></script>
    <script src="../JAVASCRIPTS/overlib.js"></script>

    <%--    <script src="Datatable/responsive.bootstrap.min.js"></script>
    <link href="Datatable/responsive.bootstrap.min.css" rel="stylesheet" />
    <script src="Datatable/jquery.dataTables.min.js"></script>
    <script src="Datatable/jquery-1.12.0.min.js"></script>
    <script src="Datatable/featherlight.min.js"></script>
    <link href="Datatable/featherlight.min.css" rel="stylesheet" />
    <script src="Datatable/dataTables.responsive.min.js"></script>
    <script src="Datatable/dataTables.bootstrap.min.js"></script>
    <link href="Datatable/dataTables.bootstrap.min.css" rel="stylesheet" />--%>
    <%--<link href="Datatable/bootstrap.min.css" rel="stylesheet" />--%>

    <%--    <script src="Datatable/dataTables.bootstrap.min.js"></script>
    <link href="Datatable/bootstrap.min.css" rel="stylesheet" />--%>



    <%--<script>
        var dt = $.noConflict();
        $(document).ready(function () {

            dt('#tblFeeEntryGrid').removeAttr('width').DataTable({
                //scrollY: "400px",
                //scrollX: true,
                //paging: false,
                //scrollCollapse: true,
                //columnDefs: [
                // { width: 200, targets: 0 }
                //],
                //fixedColumns: true
            });

            dt('#tblAttendanceGrid').removeAttr('width').DataTable({

                "lengthMenu": [[50, 100, 150, -1], [50, 100, 150, "All"]],
                scrollY: "400px",
                scrollX: true,
                paging: false,
                scrollCollapse: true,
                columnDefs: [
                 { width: 40, targets: 0 }
                ]
            });

        });
    </script>--%>
    <%--<script type="text/javascript" charset="utf-8">

        var dt = $.noConflict();
        $(document).ready(function () {
            dt('#tblstudDetails').removeAttr('width').DataTable({

                //"lengthMenu": [[50, 100, 150, -1], [50, 100, 150, "All"]],
                scrollY: "400px",
                //scrollX: true,
                paging: false,
                scrollCollapse: true
                //columnDefs: [
                // { width: 20, targets: 0 }
                //]
            });

        });
    </script>--%>
    <%-- <script language="javascript" type="text/javascript">
        function submitPopup(btnsearch) {
            var rbText;
            var searchtxt;
            if (document.getElementById('<%=rbName.ClientID %>').checked == true)
                rbText = "name";
            else if (document.getElementById('<%=rbIdNo.ClientID %>').checked == true)
                rbText = "idno";
            else if (document.getElementById('<%=rbBranch.ClientID %>').checked == true)
                rbText = "branch";
            else if (document.getElementById('<%=rbEnrollmentNo.ClientID %>').checked == true)
                rbText = "enrollmentno";
            else if (document.getElementById('<%=rbRegNo.ClientID %>').checked == true)
                rbText = "regno";
            else if (document.getElementById('<%=rbHostel.ClientID %>').checked == true)
                rbText = "hostel";
            else if (document.getElementById('<%=rbRoom.ClientID %>').checked == true)
                rbText = "room";
    searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;

            __doPostBack(btnsearch, rbText + ',' + searchtxt);

            return true;
        }

        function ClearSearchBox(btncancelmodal) {
            document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btncancelmodal, '');
        return true;
    }

    </script>--%>

    <script language="javascript" type="text/javascript">

        function submitPopup(btnsearch) {
            var rbText;
            var searchtxt;
            if (document.getElementById('<%=rbName.ClientID %>').checked == true)
                rbText = "name";
            else if (document.getElementById('<%=rbIdNo.ClientID %>').checked == true)
                rbText = "idno";
            else if (document.getElementById('<%=rbBranch.ClientID %>').checked == true)
                rbText = "branch";
            else if (document.getElementById('<%=rbEnrollmentNo.ClientID %>').checked == true)
                rbText = "enrollmentno";
            else if (document.getElementById('<%=rbRegNo.ClientID %>').checked == true)
                rbText = "regno";
            else if (document.getElementById('<%=rbHostel.ClientID %>').checked == true)
                rbText = "hostel";
            else if (document.getElementById('<%=rbRoom.ClientID %>').checked == true)
                rbText = "room";
    searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;

            __doPostBack(btnsearch, rbText + ',' + searchtxt);

            return true;
        }

        function ClearSearchBox(btncancelmodal) {
            document.getElementById('<%=txtSearch.ClientID %>').value = '';
            __doPostBack(btncancelmodal, '');
            return true;
        }

    </script>

</asp:Content>
