<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ChangeHostelRoomAllotment.aspx.cs" Inherits="Hostel_ChangeRoomAllotment"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script type="text/javascript" language="javascript" src="../Javascripts/FeeCollection.js"></script>--%>

    <style>
        .search .input-group .input-group-addon {
            padding: 2px 12px;
        }
    </style>
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

      <asp:UpdatePanel ID="updStudSearch" runat="server">
        <ContentTemplate>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Change Hostel Room Allotment</h3>
                </div>

                <div class="box-body">
                    <div class="col-12" id="divStudentSearch" runat="server">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Session </label>
                                </div>
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                    TabIndex="1" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12 search">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label> Search Student </label>
                                </div>
                                <div class="input-group date">
                                    <asp:TextBox ID="txtEnrollNo" runat="server" MaxLength="20" TabIndex="1" CssClass="form-control" Enabled="false" />
                                    <div class="input-group-addon">
                                        <a href="#" title="Search Student for Modification" data-toggle="modal" data-target="#myModal121">
                                            <asp:Image ID="imgSearch" runat="server" ImageUrl="~/images/search.png" TabIndex="1"
                                                AlternateText="Search Student by IDNo, Name, Reg. No, Branch, Semester" ToolTip="Search Student by IDNo, Name, Reg. No, Branch, Semester" />
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShow" Visible="false" runat="server" Text="Show Information" OnClick="btnShow_Click"
                            TabIndex="2" ValidationGroup="search" CssClass="btn btn-primary" />

                        <asp:RequiredFieldValidator ID="valEnrollNo" runat="server" ControlToValidate="txtEnrollNo"
                            Display="None" ErrorMessage="Please Enter Registration No." SetFocusOnError="true"
                            ValidationGroup="search" />
                        <asp:ValidationSummary ID="valSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="search" />

                    </div>

                    <div id="divStudInfo" runat="server" visible="false" class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Student Information</h5>
                                </div>
                            </div>

                            <div class="col-lg-6 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>Admission No. :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblRegNo" Font-Bold="true" runat="server" />
                                        </a>
                                    </li>
                                    <li class="list-group-item"><b>Degree :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblDegree" Font-Bold="true" runat="server" />
                                        </a>
                                    </li>
                                    <li class="list-group-item"><b>Student's Name :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblStudName" Font-Bold="true" runat="server" />
                                        </a>
                                    </li>
                                    <li class="list-group-item"><b>Branch :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblBranch" runat="server" Font-Bold="true" />
                                        </a>
                                    </li>
                                    <li class="list-group-item"><b>Gender :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblSex" runat="server" Font-Bold="true" />
                                        </a>
                                    </li>

                                </ul>
                            </div>

                            <div class="col-lg-6 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>Semester :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblSemester" Font-Bold="true" runat="server" />
                                        </a>
                                    </li>
                                    <li class="list-group-item"><b>Date of Admission :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblDateOfAdm" Font-Bold="true" runat="server" />
                                        </a>
                                    </li>
                                    <li class="list-group-item"><b>Batch :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblBatch" Font-Bold="true" runat="server" />
                                        </a>
                                    </li>
                                    <li class="list-group-item"><b>Payment Type :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblPaymentType" Font-Bold="true" runat="server" />
                                        </a>
                                    </li>
                                    <li class="list-group-item"><b>Year :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblYear" Font-Bold="true" runat="server" />
                                        </a>
                                    </li>
                                </ul>
                            </div>

                        </div>
                    </div>

                    <div id="divshifthostel" runat="server" class="col-12 mt-3" visible="false">

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:ListView ID="lvAllotmentDetails" Visible="false" runat="server">
                                    <LayoutTemplate>
                                        <div id="demo-grid">
                                            <div class="sub-heading">
                                                <h5>Room Allotment Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <label>Hostel</label>
                                                        </th>
                                                        <th>
                                                            <label>Block</label>
                                                        </th>
                                                        <th>
                                                            <label>Room</label>
                                                        </th>
                                                        <th>
                                                            <label>Allotment Date</label>
                                                        </th>
                                                        <th>
                                                            <label>Session</label>
                                                        </th>
                                                        <th>
                                                            <label>Status</label>
                                                        </th>
                                                        <th>
                                                            <label>Hosteler</label>
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
                                                <%# Eval("HOSTEL_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("BLOCK_NAME")%>
                                            </td>

                                            <td>
                                                <%# Eval("ROOM_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("ALLOTMENT_DATE", "{0:dd-MMM-yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("SESSION_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("HOSTEL_STATUS")%>
                                            </td>
                                            <td>
                                                <%# Eval("HOSTELER")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Hostel Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lbloldhostel" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Room No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblroomname" runat="server" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>

                                <div class="row mt-3">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Shift To Hostel</h5>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Hostel Name </label>
                                        </div>
                                        <asp:DropDownList ID="ddlHostelNo" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlHostelNo_SelectedIndexChanged" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="valHostel" runat="server" ControlToValidate="ddlHostelNo"
                                            Display="None" ErrorMessage="Please Select Hostel" ValidationGroup="submit" SetFocusOnError="True"
                                            InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Room Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlRoomType" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="True" AutoPostBack="true"
                                             data-select2-enable="true" OnSelectedIndexChanged="ddlRoomType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvRoomType" runat="server" ControlToValidate="ddlRoomType"
                                            Display="None" ErrorMessage="Please Select Room Type" ValidationGroup="submit" SetFocusOnError="True"
                                            InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Room  </label>
                                        </div>
                                        <asp:DropDownList ID="ddlRoomNo" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="True" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlRoomNo_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="vaRoomNo" runat="server" ControlToValidate="ddlRoomNo"
                                            Display="None" ErrorMessage="Please Select Room" ValidationGroup="submit" SetFocusOnError="True"
                                            InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divUpdateDemand" runat="server">
                                       
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Update Demand </label>
                                        </div>                     
                                        <asp:CheckBox runat="server" ID="chkUpdateDemand" />
                                     
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Label ID="lblcapacity" runat="server"></asp:Label>
                                        <asp:Label ID="lblmsg" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnsubmit" runat="server" Text="Submit"
                                            TabIndex="8" Visible="false" CssClass="btn btn-primary" ValidationGroup="submit"
                                            OnClick="btnsubmit_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Clear" CssClass="btn btn-warning" CausesValidation="false" OnClick="btnCancel_Click"
                                            TabIndex="9" Visible="false" />
                                        <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="submit" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>

                </div>
                    <div id="divMsg" runat="server">
    </div>
            </div>
        </div>
    </div>

                  </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade" id="myModal121" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Search Student Here</h4>
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
                                    <div class="form-group col-lg-8 col-md-8 col-12">
                                        <div class="label-dynamic">
                                            <label>Search Criteria</label>
                                        </div>
                                        <asp:RadioButton ID="rbName" runat="server" Text="Name" GroupName="edit" />
                                        <asp:RadioButton ID="rbIdNo" runat="server" Text="IdNo" GroupName="edit" />
                                        <asp:RadioButton ID="rbBranch" runat="server" Text="Branch" GroupName="edit" />
                                        <asp:RadioButton ID="rbSemester" runat="server" Text="Semester" GroupName="edit" />
                                        <asp:RadioButton ID="rbEnrollmentNo" runat="server" Text="Enrollmentno" GroupName="edit"
                                            Checked="True" />
                                        <asp:RadioButton ID="rbRegNo" runat="server" Text="Rollno" GroupName="edit" Checked="True" />
                                    </div>

                                    <div class="form-group col-lg-4 col-md-4 col-12">
                                        <div class="label-dynamic">
                                            <label>Search String </label>
                                        </div>
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="return submitPopup(this.name);" CssClass="btn btn-primary" />
                                <asp:Button ID="btnCancelModal" runat="server" Text="Cancel" OnClientClick="return ClearSearchBox(this.name)" CssClass="btn btn-warning" />
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                            </div>

                            <div class="col-12">
                                <asp:ListView ID="lvStudent" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Login Details</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Name
                                                    </th>
                                                    <th>IdNo
                                                    </th>
                                                    <th>Roll No.
                                                    </th>
                                                    <th>Branch
                                                    </th>
                                                    <th>Semester
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>

                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td>
                                                <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                    OnClick="lnkId_Click"></asp:LinkButton>
                                            </td>
                                            <td>
                                                <%# Eval("idno")%>
                                            </td>
                                            <td>
                                                <%# Eval("EnrollmentNo")%>
                                            </td>
                                            <td>
                                                <%# Eval("longname")%>
                                            </td>
                                            <td>
                                                <%# Eval("semesterno")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer"></div>
            </div>
        </div>
    </div>



    <script type="text/javascript" language="javascript">



        function ValidateRecordSelection() {
            var isValid = false;
            try {
                var tbl = document.getElementById('tblSearchResults');
                if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                    for (i = 1; i < tbl.rows.length; i++) {
                        var dataRow = tbl.rows[i];
                        var dataCell = dataRow.firstChild;
                        var rdo = dataCell.firstChild;
                        if (rdo.checked) {
                            isValid = true;
                        }
                    }
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return isValid;
        }





        function submitPopup(btnsearch) {
            var rbText;
            var searchtxt;
            if (document.getElementById('<%=rbName.ClientID %>').checked == true)
                rbText = "name";
            else if (document.getElementById('<%=rbIdNo.ClientID %>').checked == true)
                rbText = "idno";
            else if (document.getElementById('<%=rbBranch.ClientID %>').checked == true)
                rbText = "branch";
            else if (document.getElementById('<%=rbSemester.ClientID %>').checked == true)
                rbText = "sem";
            else if (document.getElementById('<%=rbEnrollmentNo.ClientID %>').checked == true)
                rbText = "enrollmentno";
            else if (document.getElementById('<%=rbRegNo.ClientID %>').checked == true)
                rbText = "regno";

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
