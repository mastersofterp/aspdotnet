<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="NewStudentApproval.aspx.cs" Inherits="Academic_NewStudentApproval" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_pnllist .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <%--<script src="plugins/datatables/dataTables.bootstrap.min.js"></script>--%>

    <script type="text/javascript">
        $('#myDatepicker2').datetimepicker({
            format: 'DD/MM/YYYY'
        });
    </script>

    <script type="text/javascript">
        if (document.layers) {
            //Capture the MouseDown event.
            document.captureEvents(Event.MOUSEDOWN);

            //Disable the OnMouseDown event handler.
            document.onmousedown = function () {
                return false;
            };
        }
        else {
            //Disable the OnMouseUp event handler.
            document.onmouseup = function (e) {
                if (e != null && e.type == "mouseup") {
                    //Check the Mouse Button which is clicked.
                    if (e.which == 2 || e.which == 3) {
                        //If the Button is middle or right then disable.
                        return false;
                    }
                }
            };
        }

        //Disable the Context Menu event.
        document.oncontextmenu = function () {
            return false;
        };
    </script>

    <asp:UpdatePanel ID="pnlFeeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">New Student Registration Approval</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">

                            <div class="box-body" runat="server" id="DivDetail" visible="true">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                            <label>TAN : </label>
                                            <span class="form-inline">
                                                <asp:TextBox ID="txtEnrollmentSearch" runat="server" ValidationGroup="Show" class="form-control"
                                                    PlaceHolder="Enter TAN"></asp:TextBox>
                                                <%-- <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtEnrollmentSearch"
                                                            Display="None" ErrorMessage="Please Enter TAN" ValidationGroup="Show">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:ValidationSummary ID="ValidationSummary5" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                            ShowSummary="False" ValidationGroup="Show" />--%>
                                                       
                                            </span>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Admission Batch</label>--%>
                                                <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlAdmBatch" runat="server" data-select2-enable="true" AppendDataBoundItems="true" CssClass="form-control" TabIndex="1" Style="margin-left: 7px">
                                                <asp:ListItem Value="0">Please Select </asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlAdmBatch" runat="server" ControlToValidate="ddlAdmBatch" SetFocusOnError="true"
                                                Display="None" ValidationGroup="Show" InitialValue="0"
                                                ErrorMessage="Please Select Admission Batch"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlAdmBatch" SetFocusOnError="true"
                                                Display="None" ValidationGroup="Report" InitialValue="0"
                                                ErrorMessage="Please Select Admission Batch"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Degree </label>--%>
                                                <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" data-select2-enable="true" AppendDataBoundItems="true" CssClass="form-control" AutoPostBack="true"
                                                TabIndex="2" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select </asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree" SetFocusOnError="true"
                                                Display="None" ValidationGroup="Show" InitialValue="0"
                                                ErrorMessage="Please Select Degree"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlDegree" SetFocusOnError="true"
                                                Display="None" ValidationGroup="Report" InitialValue="0"
                                                ErrorMessage="Please Select Degree"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <%--<label>Branch </label>--%>
                                                <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlbranch" runat="server" AppendDataBoundItems="true"
                                                TabIndex="3" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="rfvddlSem" runat="server" ControlToValidate="ddlSem"
                                                Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">

                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" TabIndex="4" ValidationGroup="Show" OnClick="btnShow_Click" />
                                    <asp:Button ID="btnreport1" runat="server" OnClick="btnreport1_Click" Text="Student Status list" CssClass="btn btn-primary" ValidationGroup="Report" />
                                    <asp:Button ID="btncancel1" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btncancel1_Click" TabIndex="5" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Show"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />


                                    <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Report" />

                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="pnllist" runat="server" Visible="false">
                                        <asp:ListView ID="lvStudent" runat="server" OnItemCommand="lvStudent_ItemCommand" Style="width: 100%">
                                            <EmptyDataTemplate>
                                                <br />
                                                <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="Student Not found" />
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>

                                                <div class="sub-heading">
                                                    <h5>Students List</h5>
                                                </div>

                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Sr No.</th>
                                                            <th>
                                                                <asp:Label ID="lblDYlvEnrollmentNo" runat="server" Font-Bold="true"></asp:Label></th>
                                                            <th>Application ID </th>
                                                            <th>Name </th>
                                                            <th>
                                                                <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label>(<asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>)
                                                            </th>
                                                            <th>
                                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label></th>
                                                            <th>Pay Status </th>
                                                            <th>Admission Date </th>
                                                            <th>Action</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item">
                                                    <td><%# Container.DataItemIndex + 1%></td>
                                                    <td><%# Eval("ENROLLNO")%></td>
                                                    <td><%# Eval("APPLICATIONID")%></td>
                                                    <td><%# Eval("STUDNAME")%></td>
                                                    <td><%# Eval("DEGREE")%></td>
                                                    <%--  <td><%# Eval("LONGNAME")%></td>--%>
                                                    <td><%# Eval("SEMESTERNAME")%></td>
                                                    <td><%# Eval("FEESTATUS")%></td>
                                                    <td><%# Eval("ADMDATE")%></td>
                                                    <td style="text-align: center">
                                                        <asp:Button ID="btnConfirm" runat="server" CommandArgument='<%# Eval("IDNO")%>' CommandName="Report" CssClass="btn btn-info" Text='<%# (Convert.ToInt32(Eval("admcan"))==1 ?  "Confirmed" : "Not Confirm") %>' ToolTip='<%# (Convert.ToInt32(Eval("admcan"))==1 ?  "1" : "0") %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="altitem">
                                                    <td><%# Container.DataItemIndex + 1%></td>
                                                    <td><%# Eval("ENROLLNO")%></td>
                                                    <td><%# Eval("APPLICATIONID")%></td>
                                                    <td><%# Eval("STUDNAME")%></td>
                                                    <td><%# Eval("DEGREE")%></td>
                                                    <%--  <td><%# Eval("LONGNAME")%></td>--%>
                                                    <td><%# Eval("SEMESTERNAME")%></td>
                                                    <td><%# Eval("FEESTATUS")%></td>
                                                    <td><%# Eval("ADMDATE")%></td>
                                                    <td style="text-align: center">
                                                        <asp:Button ID="btnConfirm" runat="server" CommandArgument='<%# Eval("IDNO")%>' CommandName="Report" CssClass="btn btn-info" Text='<%# (Convert.ToInt32(Eval("admcan"))==1 ?  "Confirmed" : "Not Confirm") %>' ToolTip='<%# (Convert.ToInt32(Eval("admcan"))==1 ?  "1" : "0") %>' />
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>



                            </div>
                            <div id="DivStudDetails" runat="server" visible="false">
                                <div id="dvdisplay" runat="server">

                                    <div class="col-12 mt-3">

                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">

                                            <label>Student Initial/Surname :</label>
                                            <span class="form-inline">
                                                <asp:TextBox ID="txtStudInitial" runat="server" class="form-control" Width="135px" placeholder="Enter Student Initial/Surname"></asp:TextBox>
                                                <asp:Label ID="lbllastname" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                            </span>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">

                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item">
                                                        <label>Student Email Id :</label>
                                                        <a class="sub-label">
                                                            <asp:Label ID="txtEmail" runat="server" Font-Bold="true" placeholder="Enter Student Email Id"></asp:Label>
                                                            <asp:Label ID="lblEmail" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>
                                                        <asp:Label runat="server" ID="lblDYtxtEnrollmentNoo" Font-Bold="true">Enrollment No.</asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblenrollno" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>

                                                    <li class="list-group-item"><b>
                                                        <asp:Label runat="server" ID="lblDYtxtDegree" Font-Bold="true">Degree Name </asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lbldegree" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>
                                                        <asp:Label runat="server" ID="lblDYtxtBranchName" Font-Bold="true">Branch Name </asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblbranch" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item">
                                                        <label>Fees Amount :</label>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblAmount" runat="server" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">

                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item">
                                                        <label>Student Mobile Number :</label>
                                                        <a class="sub-label">
                                                            <asp:Label ID="txtMobile" runat="server" placeholder="Enter Student Mobile Number" Font-Bold="true"></asp:Label>
                                                            <asp:Label ID="lblmobile" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item">
                                                        <label>Application Number :</label>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblapp" runat="server" Text="" Font-Bold="true"></asp:Label>

                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>
                                                        <asp:Label runat="server" ID="lblDYddlSemester_Tab" Font-Bold="true">Semester </asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblsem" runat="server" Text="" ToolTip="1" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>

                                                    <li class="list-group-item"><b>
                                                        <asp:Label runat="server" ID="lblDYlvAdmBatch" Font-Bold="true">Admission Batch </asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lbladmbatch" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item" runat="server" visible="false">
                                                        <label>Session :</label>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSession" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </a>

                                                    </li>
                                                    <li class="list-group-item">
                                                        <label>Fees Paid Status / Paid Amount :</label>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblFeeStatus" runat="server" Font-Bold="true"></asp:Label>
                                                            <asp:Label Text="/" runat="server"></asp:Label>
                                                            <asp:Label ID="lblpaidamt" runat="server" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>

                                                </ul>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Student Name</label>
                                                </div>

                                                <asp:TextBox ID="txtStudName" runat="server" class="form-control" placeholder="Enter Student Name" CssClass="form-control"></asp:TextBox>
                                                <asp:TextBox ID="lblname" runat="server" Text="" Font-Bold="true" Visible="false"></asp:TextBox>

                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Category</label>
                                                </div>
                                                <asp:DropDownList ID="ddlcategory" runat="server" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlcategory"
                                                    Display="None" ErrorMessage="Please Select Category" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="submit">
                                                </asp:RequiredFieldValidator>
                                                <asp:Label ID="Label1" runat="server" Visible="false" Text="" Font-Bold="true"></asp:Label>

                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Allotted Category</label>
                                                </div>
                                                <asp:DropDownList ID="ddlAdmQuota" runat="server" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAdmQuota"
                                                    Display="None" ErrorMessage="Please Select Allotted Category" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="submit">
                                                </asp:RequiredFieldValidator>
                                                <asp:Label ID="lbladmquota" runat="server" Visible="false" Text="" Font-Bold="true"></asp:Label>

                                            </div>




                                            <div class="form-group col-md-6" style="display: none">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Hostel Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlhostel" runat="server" data-select2-enable="true" AppendDataBoundItems="true" CssClass="form-control">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Transport With AC</label>
                                                </div>

                                                <asp:RadioButton ID="rdbTransportACyes" runat="server" Text="YES" GroupName="Link4" Enabled="false" TabIndex="6" />
                                                &nbsp;&nbsp;&nbsp;
                                                    <asp:RadioButton ID="rdbTransportACno" runat="server" Text="NO" GroupName="Link4" Enabled="false" TabIndex="6" />

                                            </div>



                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Student Admission Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="txtAdmdate1" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtAdmdate" class="form-control" data-inputmask="'mask': '99/99/9999'"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtAdmdate" PopupButtonID="txtAdmdate1" Enabled="true"
                                                        EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>

                                                    <asp:RequiredFieldValidator ID="valStartDate" runat="server" ControlToValidate="txtAdmdate"
                                                        Display="None" ErrorMessage="Please Enter Student Admission Date." SetFocusOnError="true"
                                                        ValidationGroup="submit" />

                                                </div>

                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Payment Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlpaytype" runat="server" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" Enabled="false">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlpaytype"
                                                    Display="None" ErrorMessage="Please Select Payment Type" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit">
                                                </asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group col-lg-2 col-md-6 col-12" style="display: none;">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Hostel Facility</label>
                                                </div>

                                                <asp:RadioButton ID="rdbhostelyes" runat="server" Text="YES" GroupName="Link2" Enabled="false" TabIndex="6" />
                                                &nbsp;&nbsp;&nbsp;
                                                    <asp:RadioButton ID="rdbhostelNo" runat="server" Text="NO" GroupName="Link2" Enabled="false" TabIndex="6" />


                                            </div>

                                            <div class="form-group col-lg-2 col-md-6 col-12" style="display: none;">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Transport Facility</label>
                                                </div>
                                                <asp:RadioButton ID="rdbtransportyes" runat="server" Text="YES" GroupName="Link3" Enabled="false" TabIndex="6" />
                                                &nbsp;&nbsp;&nbsp;
                                                    <asp:RadioButton ID="rdbtransportNo" runat="server" Text="NO" GroupName="Link3" Enabled="false" TabIndex="6" />

                                            </div>

                                            <div class="form-group col-lg-2 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Scholarship</label>
                                                </div>

                                                <asp:RadioButton ID="rdoscholyes" runat="server" Text="YES" GroupName="Link5" Enabled="false" TabIndex="6" />
                                                &nbsp;&nbsp;&nbsp;
                                                    <asp:RadioButton ID="rdoscholno" runat="server" Text="NO" GroupName="Link5" Enabled="false" TabIndex="6" />


                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="statusdiv">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>
                                                        Admission Status :
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:RadioButton ID="rdopending" runat="server" GroupName="Status" TabIndex="6" />
                                                    Pending
                                            <asp:RadioButton ID="rdoconfirm" runat="server" GroupName="Status" TabIndex="7" Checked="true" />
                                                    Confirm
                                            <asp:RadioButton ID="rdoreject" runat="server" GroupName="Status" TabIndex="8" />
                                                    Reject  
                                                </div>

                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Remark :</label>
                                                </div>
                                                <asp:TextBox ID="TxtRemark" runat="server" class="form-control" MaxLength="300" TextMode="MultiLine" TabIndex="16"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Enter Remark."
                                                    ControlToValidate="TxtRemark" Display="None" SetFocusOnError="true" ValidationGroup="submit" />
                                            </div>

                                            <div class="col-12">
                                                <asp:ListView ID="lvDocumentsAdmin" runat="server" OnItemDataBound="lvDocumentsAdmin_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Document List</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Sr.No</th>
                                                                    <th>Document</th>
                                                                    <th>Status</th>
                                                                    <th runat="server" visible="false">View</th>
                                                                    <th runat="server" visible="false">Download</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>

                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Container.DataItemIndex +1 %></td>
                                                            <td>
                                                                <asp:Label ID="lblDocument" ToolTip='<%# Eval("DOCUMENTNO") %>' runat="server" Text='<%# Eval("DOCUMENTNAME") %>'></asp:Label>
                                                                <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("DOCUMENTNO") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblStatus" Font-Bold="true" runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                            </td>

                                                            <td runat="server" visible="false">
                                                                <asp:UpdatePanel ID="updPreview" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:ImageButton ID="imgbtnPrevDoc1" runat="server" OnClick="imgbtnPrevDoc1_Click" data-toggle="modal" data-target="#PassModel"
                                                                            Text="Preview" ImageUrl="~/IMAGES/icons8-search-480 (1).png" Height="32px"
                                                                            Width="30px" CommandArgument='<%# Eval("DOCUMENT_NAME") %>' Visible='<%# Convert.ToString(Eval("DOCUMENT_NAME"))==string.Empty?false:true %>'></asp:ImageButton>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="imgbtnPrevDoc1" EventName="Click" />
                                                                        <%--  <asp:PostBackTrigger ControlID="btnFetch"/>data-toggle="modal" data-target="#PassModel"--%>
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                            <td runat="server" visible="false">
                                                                <asp:LinkButton ID="lnkDownloadDoc" runat="server" OnClick="lnkDownloadDoc_Click" Visible='<%# Convert.ToString(Eval("DOCUMENT_NAME"))==string.Empty?false:true %>'
                                                                    Text="Click to Download" ToolTip="Click here to download document"
                                                                    CommandArgument='<%# Eval("DOCUMENTNO") %>'> 
                                                                </asp:LinkButton>
                                                            </td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-12 btn-footer" id="dvaction" runat="server">
                                    <asp:HiddenField ID="hdfadmbatch" runat="server" />
                                    <asp:Button ID="btnapproval" runat="server" Text="Submit" OnClick="btnapproval_Click" ValidationGroup="submit" CssClass="btn btn-primary" OnClientClick="return showDeclarationConfirm();" />
                                    <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" CssClass="btn btn-primary" Text="Report" />
                                    <asp:Button ID="btncancel" runat="server" Text="Back" OnClick="btncancel_Click" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False"
                                        ValidationGroup="submit" />

                                    &nbsp;<asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="show" />

                                </div>

                                <br />

                                <div class="modal fade" id="PassModel" role="dialog">
                                    <div class="modal-dialog text-center">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <!-- Modal content-->
                                                <div class="modal-content" style="width: 900px;">
                                                    <div class="modal-header">
                                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                        <h4 class="modal-title">Document</h4>
                                                    </div>
                                                    <div class="modal-body">
                                                        <div class="col-md-12">
                                                            <iframe runat="server" style="width: 800px; height: 1100px" id="iframeView"></iframe>
                                                        </div>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                                    </div>
                                                </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>




                                    </div>
                                </div>
                            </div>

                            <div class="col-12" style="text-align: left">
                                <asp:Panel ID="pnlBind" runat="server">
                                    <div class="col-12 btn-footer" id="divdoc" runat="server">


                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit Document Status" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />

                                        <asp:Button ID="btnreportdoc" runat="server" Text="Document Report" CssClass="btn btn-info" OnClick="btnReportdoc_Click" />

                                        <asp:Button ID="btncanceldoc" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCanceldoc_Click" />
                                    </div>
                                    <div class="col-12">
                                        <asp:Panel ID="pnlrdb" runat="server">
                                            <div id="divStatus" class="form-group col-lg-6 col-md-6 col-12 checkbox-list-column">
                                                <div class="label-dynamic">
                                                    <label>Document Status:</label>&nbsp;&nbsp;                                                                                                     
                                                    <asp:RadioButton ID="rdoSubmit" runat="server" Text="Submitted" Checked="true" GroupName="act_status"
                                                        TabIndex="5" Visible="true" />&nbsp&nbsp
                                                    <asp:RadioButton ID="rdoReturn" runat="server" Text="Return" GroupName="act_status"
                                                        TabIndex="10" Visible="true" />
                                                </div>


                                            </div>
                                            <br />
                                        </asp:Panel>
                                    </div>
                                    <asp:Label ID="lblmessageShow" runat="server"></asp:Label>
                                    <%-- <asp:UpdatePanel ID="upnllvBinddata" runat="server">
                                                <ContentTemplate>--%>
                                    <asp:ListView ID="lvBinddata" runat="server" OnItemDataBound="lvBinddata_ItemDataBound">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>Document List</h5>
                                                </div>
                                                <div style="width: 100%; height: 400px; overflow: auto">
                                                    <table class="table table-striped table-bordered nowrap dispaly">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Sr.NO</th>
                                                                <%-- ToolTip='<%# Eval("IDNo")%>' --%>
                                                                <th>
                                                                    <asp:CheckBox ID="CheckBox1" runat="server" onclick="totAllSubjects(this)" />Select All

                                                                </th>
                                                                <%--  <th>  <asp:CheckBox ID="CheckBox1" runat="server" />Select All</th>--%>
                                                                <th>Document Name</th>
                                                                <th>Remark</th>
                                                                <th>STATUS</th>
                                                                <th>Date</th>
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
                                                <td style="width: 5px"><%# Container.DataItemIndex +1 %></td>
                                                <td>
                                                    <asp:CheckBox ID="chkDocsingle" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStar" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                                                    <asp:Label ID="lblDocument" runat="server" ToolTip='<%# Eval("DOCUMENTNO") %>' Text='<%# Eval("DOCUMENTNAME") %>'></asp:Label>
                                                    <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("DOCUMENTNO") %>' />
                                                </td>

                                                <%-- <td style="width:20%"><%# Eval("DOCUMENTNAME") %></td>--%>
                                                <td>
                                                    <asp:TextBox ID="txtDoc" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <%# Eval("STATUS") %>                                                                                
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <%-- <cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy"> </cc1:CalendarExtender>--%>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtAdmdate" PopupButtonID="txtDate" Enabled="true"
                                                        EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                </td>


                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <%--</ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lvBinddata" />
                                                </Triggers>
                                            </asp:UpdatePanel>--%>
                                </asp:Panel>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnapproval" />
            <asp:PostBackTrigger ControlID="btncancel" />
            <asp:PostBackTrigger ControlID="btnreport1" />
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="lvDocumentsAdmin" />
            <asp:PostBackTrigger ControlID="lvBinddata" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>



    <%-- <div id="divDetails" class="col-12" runat="server" visible="true">                     
                            <div class="col-12" style="margin-top:10px;">
                                <asp:Panel ID="pnlBind" runat="server">
                                    <asp:Label ID="lblmessageShow" runat="server"></asp:Label>
                                    <asp:UpdatePanel ID="upnllvBinddata" runat="server">
                                        <ContentTemplate>
                                   
                                            </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lvBinddata" />
                                        </Triggers>
                                        </asp:UpdatePanel>
                                </asp:Panel>

                            </div>
                      
                    </div>--%>

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '0';
                alert('Only Numeric Value Allowed!');
                txt.focus();
                return;
            }
        };
    </script>

    <script>
        function showpreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgpreview').css('visibility', 'visible');
                    $('#imgpreview').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>

    <script type="text/javascript">

        function showDeclarationConfirm() {
            var ret = confirm('Are You Sure You want to Approve this Admission');
            if (ret == true)
                return true;
            else
                return false;
        }


    </script>
    <script>
        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else {
                        e.checked = false;
                        headchk.checked = false;
                    }
                }

            }

        }
    </script>

</asp:Content>
