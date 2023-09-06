<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="FeesDetails.aspx.cs" Inherits="ACADEMIC_FeesRefundReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script type="text/javascript">
        function RunThisAfterEachAsyncPostback()
        {
            RepeaterDiv();

        }
        function RepeaterDiv()
        {
            $(document).ready(function() {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });
        }
    </script>

    <script src="../Content/jquery.js" type="text/javascript"></script>

    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript" charset="utf-8">
        $(document).ready(function() {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>

    <script type="text/javascript" language="javascript" src="../Javascripts/FeeCollection.js"></script>

    <script type="text/javascript" language="javascript" src="../includes/prototype.js"></script>

    <script type="text/javascript" language="javascript" src="../includes/scriptaculous.js"></script>

    <script type="text/javascript" language="javascript" src="../includes/modalbox.js"></script>
    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Student Ledger Report</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label></label>
                                </div>
                                <asp:TextBox ID="txtEnrollNo" runat="server" ValidationGroup="submit" ToolTip="Please Enter USN No." CssClass="form-group" placeholder="Enter USN No"></asp:TextBox>
                                <a href="#" title="Search Student for Fee Payment" data-toggle="modal" data-target="#myModal4"
                                    style="background-color: White">
                                    <asp:Image ID="imgSearch" runat="server" ImageUrl="~/Images/search.png" TabIndex="3" />
                                </a>
                            </div>
                            <div class="form-group col-lg-2 col-md-3 col-12">
                                <div class="label-dynamic">
                                    <label></label>
                                </div>
                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnShow_Click"
                                    ValidationGroup="studSearch" />
                                <%--  Shrink the info panel out of view --%>
                                <asp:RequiredFieldValidator ID="valEnrollNo" runat="server" ControlToValidate="txtEnrollNo"
                                    Display="None" ErrorMessage="Please enter enrollment number." SetFocusOnError="true"
                                    ValidationGroup="studSearch" />
                                <asp:ValidationSummary ID="valSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="studSearch" />
                            </div>
                        </div>
                    </div>

                    <!-- Modal -->
                    <div id="myModal4" class="modal fade" role="dialog">
                        <div class="modal-dialog">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Student Search</h4>
                                </div>
                                <div class="modal-body">
                                    <asp:UpdatePanel ID="updEdit" runat="server">
                                        <ContentTemplate>
                                            <div class="col-md-12">
                                                <label>Search By</label>
                                                <asp:RadioButton ID="rdoName" runat="server" Text="Name" GroupName="edit" />&nbsp;&nbsp;
                                <asp:RadioButton ID="rdoEnrollmentNo" runat="server" Checked="true" Text="USN No."
                                    GroupName="edit" />&nbsp;&nbsp;
                                            </div>
                                            <div class="col-md-12">
                                                <asp:TextBox ID="txtSearch" runat="server" Width="350px"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3" id="sfsdf" runat="server">
                                                <label>Degree</label>
                                                <asp:DropDownList ID="ddlDegree" AppendDataBoundItems="true" runat="server" Width="130px" />
                                            </div>
                                            <div class="col-md-3">
                                                <label>Branch</label>
                                                <asp:DropDownList ID="ddlBranch" AppendDataBoundItems="true" runat="server" Width="130px" />
                                            </div>
                                            <div class="col-md-3">
                                                <label>Year</label>
                                                <asp:DropDownList ID="ddlYear" AppendDataBoundItems="true" runat="server" Width="130px" />
                                            </div>
                                            <div class="col-md-3">
                                                <label>Semester</label>
                                                <asp:DropDownList ID="ddlSem" AppendDataBoundItems="true" runat="server" Width="130px" />
                                            </div>
                                            <div class="col-md-12" style="margin-top: 25px">
                                                <p class="text-center">
                                                    <input id="btnSearch" type="button" value="Search" onclick="SubmitSearch(this.id);" class="btn btn-primary" />
                                                    <input id="btnClear" type="button" value="Clear Text" class="btn btn-warning" onclick="javascript:document.getElementById('<%=txtSearch.ClientID %>    ').value = '';" />
                                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                                </p>
                                            </div>
                                            <div class="container-fluid">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <asp:Panel ID="pnllist" runat="server" CssClass="table-responsive">
                                                            <asp:ListView ID="lvStudent" runat="server">
                                                                <LayoutTemplate>
                                                                    <div>
                                                                        <h4>Search Results</h4>
                                                                        <table class="table table-hovered table-bordered table-stripped">
                                                                            <thead>
                                                                                <tr class="bg-light-blue">
                                                                                    <th>Name
                                                                                    </th>
                                                                                    <th>USN No.
                                                                                    </th>
                                                                                    <th>Identity.No
                                                                                    </th>
                                                                                    <th>Degree
                                                                                    </th>
                                                                                    <th>Branch
                                                                                    </th>
                                                                                    <th>Year
                                                                                    </th>
                                                                                    <th>Semester
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                    </div>

                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("NAME") %>' CommandArgument='<%# Eval("IDNO") %>'
                                                                                OnClick="lnkId_Click"></asp:LinkButton>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ENROLLNO")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("IDNO")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("CODE")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SHORTNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("YEARNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SEMESTERNAME")%>
                                                                        </td>
                                                                    </tr>

                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>

                                    </asp:UpdatePanel>
                                </div>

                            </div>

                        </div>
                    </div>

                    <div id="divstudinfo" runat="server" visible="false">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Student Information</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divStudentInfo" style="display: block;">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>USN No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Student's Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudName" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Father's Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblFatherName" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Mobile No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblMobileNo" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Semester :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item d-none"><b>Date of Admission :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDateOfAdm" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Year :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblYear" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Admission Batch :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblBatch" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item d-none"><b>Payment Type :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblPaymentType" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Degree :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Branch :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Sex :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSex" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvFeesDetails" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Fees Details</h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblDD_Details">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>USN No.
                                                        </th>
                                                        <th>Receipt Type
                                                        </th>
                                                        <th>Semester
                                                        </th>
                                                        <th>Demand
                                                        </th>
                                                        <th>Paid Amount
                                                        </th>
                                                        <th>Receipt No
                                                        </th>
                                                        <th>Receipt Date
                                                        </th>
                                                        <th>Voucher No
                                                        </th>
                                                        <th>Pay Type
                                                        </th>
                                                        <th>Print
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                            <%--</div>--%>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Container.DataItemIndex + 1%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRecieptCode" runat="server" Text='<%# Eval("RECIEPT_CODE") %>' />

                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTERNO") %>
                                                </td>
                                                <td>
                                                    <%# Eval("DEMAND") %>
                                              
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTotPaid" runat="server" Text='<%# Eval("PAID_AMT") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("REC_NO") %>
                                              
                                                </td>
                                                <td>
                                                    <%# Eval("REC_DT","{0:dd/MM/yyyy}") %>
                                              
                                                </td>
                                                <td>
                                                    <%# Eval("VOUCHERNO") %>
                                              
                                                </td>
                                                <td>
                                                    <%# Eval("PAY_MODE_CODE") %>
                                              
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImgPrintReceipt" runat="server" ImageUrl="~/Images/print.png" CommandArgument='<%# Eval("DCR_NO") %>'
                                                        ToolTip='<%# Eval("RECIEPT_CODE")%>' CausesValidation="False" OnClick="ImgPrintReceipt_Click" />

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div style="margin-left: 420px" id="divtotamt" runat="server" visible="false">
                                <span><b>Total Paid Amount :&nbsp;&nbsp;&nbsp;</b></span>
                                <b>
                                    <asp:Label ID="lblPaidAmt" runat="server"> </asp:Label></b>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnReport" runat="server" Text="Report" CausesValidation="false"
                                    OnClick="btnReport_Click" TabIndex="15" CssClass="btn btn-info" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    OnClick="btnCancel_Click" TabIndex="16" CssClass="btn btn-danger" />

                                <asp:ValidationSummary ID="valSummery1" DisplayMode="List" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">
    
        function ShowModalbox()
        {
            try
            {
                Modalbox.show($('divModalboxContent'), {title:'Search Student for Refund', width:700, overlayClose:false, slideDownDuration:0.1, slideUpDuration:0.1, afterLoad:SetFocus});           
            }
            catch(e)
            {
                alert("Error: " + e.description);
            }
            return;
        }
    
        function SetFocus()
        {
            try
            {
                document.getElementById('<%= txtSearch.ClientID %>').focus();
                }
                catch(e)
                {
                    alert("Error: " + e.description);
                }        
            }
    
            function SubmitSearch(btnsearch)
            {
                try
                {    
                    var searchParams = "";
                    if(document.getElementById('<%= rdoName.ClientID %>').checked)
                    {
                        searchParams = "Name=" + document.getElementById('<%= txtSearch.ClientID %>').value;
                        searchParams += ",SRNO=";
                    }
                    else if(document.getElementById('<%= rdoEnrollmentNo.ClientID %>').checked)
                    {
                        searchParams = "SRNO=" + document.getElementById('<%= txtSearch.ClientID %>').value;
                        searchParams += ",Name=";
                    }
                searchParams += ",DegreeNo=" + document.getElementById('<%= ddlDegree.ClientID %>').options[document.getElementById('<%= ddlDegree.ClientID %>').selectedIndex].value;
                    searchParams += ",BranchNo=" + document.getElementById('<%= ddlBranch.ClientID %>').options[document.getElementById('<%= ddlBranch.ClientID %>').selectedIndex].value;
                    searchParams += ",YearNo=" + document.getElementById('<%= ddlYear.ClientID %>').options[document.getElementById('<%= ddlYear.ClientID %>').selectedIndex].value;
                    searchParams += ",SemNo=" + document.getElementById('<%= ddlSem.ClientID %>').options[document.getElementById('<%= ddlSem.ClientID %>').selectedIndex].value;
                       
                    __doPostBack(btnsearch, searchParams);           
                }
                catch(e)
                {
                    alert("Error: " + e.description);
                }       
                return;        
            }
    </script>
</asp:Content>

