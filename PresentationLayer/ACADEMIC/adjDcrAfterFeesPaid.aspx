<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="adjDcrAfterFeesPaid.aspx.cs" Inherits="ACADEMIC_adjDcrAfterFeesPaid" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script type="text/javascript">
         function RunThisAfterEachAsyncPostback() {
             RepeaterDiv();

         }

         function RepeaterDiv() {
             $(document).ready(function () {

                 $(".display").dataTable({
                     "bJQueryUI": true,
                     "sPaginationType": "full_numbers"
                 });

             });

         }
         function ClearSearchBox(btnclear) {
             document.getElementById('<%=txtSearch.ClientID %>').value = '';
            __doPostBack(btnclear, '');
            return true;
        }
    </script>--%>

    <%--script src="../Content/jquery.js" type="text/javascript"></script>

    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>--%>

    <%--<script type="text/javascript" charset="utf-8">
        $(document).ready(function () {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>--%>

    <%--    <script type="text/javascript" language="javascript" src="../includes/prototype.js"></script>

    <script type="text/javascript" language="javascript" src="../includes/scriptaculous.js"></script>

    <script type="text/javascript" language="javascript" src="../includes/modalbox.js"></script>--%>

    <%--<script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <!-- Modal -->
    <div id="myModal1" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="updEdit" runat="server">
                <ContentTemplate>
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Search</h4>
                        </div>
                        <div class="modal-body">

                            <fieldset>

                                <div class="col-md-12">
                                    <label>Search by :&nbsp;&nbsp;&nbsp;&nbsp;</label>

                                    <asp:RadioButton ID="rdoName" Checked="true" runat="server" Text="Name" GroupName="edit" />&nbsp;&nbsp;
                                <asp:RadioButton ID="rdoEnrollmentNo" runat="server" Text="Enrollment No." GroupName="edit" />
                                </div>
                                <div class=" form-group col-md-12">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Width="350px"></asp:TextBox>
                                </div>

                                <div class="col-md-3">
                                    <label>Degree</label>
                                    <asp:DropDownList ID="ddlDegree" AppendDataBoundItems="true" runat="server" />
                                </div>
                                <div class="col-md-3">
                                    <label>Branch</label>
                                    <asp:DropDownList ID="ddlBranch" AppendDataBoundItems="true" runat="server" />
                                </div>
                                <div class="col-md-3">
                                    <label>Year</label>
                                    <asp:DropDownList ID="ddlYear" AppendDataBoundItems="true" runat="server" />
                                </div>
                                <div class="col-md-3">
                                    <label>Semester</label>
                                    <asp:DropDownList ID="ddlSem" AppendDataBoundItems="true" runat="server" />
                                </div>

                            </fieldset>

                            <asp:ListView ID="lvStudent" runat="server">
                                <LayoutTemplate>
                                    <div>

                                        <h4>Search Results</h4>
                                        <asp:Panel ID="Panel1" runat="server" Height="300px" ScrollBars="Vertical">
                                            <table class="table table-hover table-bordered">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Name
                                                        </th>
                                                        <th>Enroll.No
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
                                            </table>
                                        </asp:Panel>
                                    </div>

                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("NAME") %>' CommandArgument='<%# Eval("IDNO") %>'></asp:LinkButton>
                                        </td>
                                        <td>
                                            <%# Eval("ENROLLNO")%>
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

                        </div>
                        <div class="modal-footer">
                            <p class="text-center">
                                <input id="btnSearch" type="button" value="Search" onclick="SubmitSearch(this.id);" class="btn btn-primary" />
                                <%--          <input id="btnClear" type="button" value="Clear Text" class="btn btn-warning" onclick="javascript:document.getElementById('<%=txtSearch.ClientID %>    ').value = '';" />--%>
                                <asp:Button ID="btnClear" runat="server" Text="Cancel" OnClientClick="return ClearSearchBox(this.name)" CssClass="btn btn-danger" />
                                <asp:Button ID="btnCloseModalbox" runat="server" Text="Close" OnClientClick="return ClearSearchBox(this.name)" CssClass="btn btn-default" data-dismiss="modal" />
                                <%--          <input id="btnCloseModalbox" type="button" value="Close" data-dismiss="modal" class="btn btn-default" />    --%>
                            </p>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                         <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                    </h3>
                </div>

                <div class="box-body">
                    <div class="col-12" id="divsearchstud" runat="server" visible="true">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Search Student</label>
                                </div>
                                <asp:TextBox ID="txtEnrollNo" runat="server" MaxLength="15" CssClass="form-control" placeholder="Enter Enrollment No." TabIndex="1" />
                                <asp:RequiredFieldValidator ID="valEnrollNo" runat="server" ControlToValidate="txtEnrollNo"
                                    Display="None" ErrorMessage="Please enter enrollment number." SetFocusOnError="true"
                                    ValidationGroup="studSearch" />
                                <asp:ValidationSummary ID="valSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="studSearch" />

                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12 mt-3">
                                <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" TabIndex="1"
                                    ValidationGroup="studSearch" CssClass="btn btn-primary" />
                            </div>
                            <div class="col-md-2" style="margin-top: 15px; display: none">
                                Search:
                                <a href="#" title="Search Student for Fee Payment" data-toggle="modal" data-target="#myModal1"
                                    style="background-color: White">
                                    <asp:Image ID="imgSearch" runat="server" ImageUrl="~/images/search.png" TabIndex="3" />
                                </a>
                            </div>
                        </div>
                    </div>
                    
                    <div id="divAllDemands" runat="server" visible="false" class="col-12 mt-3">
                        <%--<div class="sub-heading">
                            <h5>Fee Payments</h5>
                        </div>--%>
                       <%-- <div class="row">--%>
                            <asp:Panel ID="Panel2" runat="server">
                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                    <asp:Repeater ID="lvAllDemands" runat="server">

                                        <HeaderTemplate>
                                            <div class="sub-heading">
                                                <h5>Available Fee Amount</h5>
                                            </div>
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Edit
                                                    </th>
                                                    <th>Name
                                                    </th>
                                                    <th>Enroll. No.
                                                    </th>
                                                    <th>Degree
                                                    </th>
                                                    <th>Branch
                                                    </th>
                                                    <th>Year
                                                    </th>
                                                    <th>Sem.
                                                    </th>
                                                    <th>Receipt No.
                                                    </th>
                                                    <th>Receipt_Code
                                                    </th>
                                                    <th>Payment Cat.
                                                    </th>
                                                    <th>Paid Amount
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                        </HeaderTemplate>

                                        <ItemTemplate>

                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEditDCR" runat="server"
                                                        CommandArgument='<%# Eval("DCR_NO") %>' CommandName='<%# Eval("IDNO") %>' ImageUrl="../Images/edit.png"
                                                        OnClick="btnEditDCR_Click" ToolTip='<%# Container.ItemIndex %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("NAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("ENROLLNMENTNO") %>
                                                </td>
                                                <td>
                                                    <%# Eval("CODE")%>
                                                    <asp:HiddenField runat="server" ID="hdnDegree" Value='<%# Eval("DEGREENO")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("LONGNAME")%>
                                                    <asp:HiddenField runat="server" ID="hdnBranch" Value='<%# Eval("BRANCHNO")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("YEARNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTERNAME")%>
                                                    <asp:HiddenField runat="server" ID="hdnSem" Value='<%# Eval("SEMESTERNO")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("REC_NO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("RECIEPT_CODE")%>
                                                    <asp:HiddenField runat="server" ID="hdnRcpt" Value='<%# Eval("RECIEPT_CODE")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("PAYTYPENAME")%>
                                                </td>
                                                <td style="text-align: center;">
                                                    <%# Eval("TOTAL_AMT")%>
                                           
                                                </td>
                                            </tr>

                                        </ItemTemplate>

                                        <FooterTemplate>
                                            </tbody>
                                        </FooterTemplate>

                                    </asp:Repeater>
                                </table>
                            </asp:Panel>
                        <%--</div>--%>
                    </div>

                    <div id="divFeeItems" visible="false" runat="server" class="col-12 mt-3">
                        <div class="row">
                            <div class="col-md-6 col-12">
                                <%--<div class="sub-heading">
                                    <h5>Fee Details</h5>
                                </div>--%>
                                <asp:Panel ID="Panel3" runat="server">
                                    <asp:ListView ID="lvFeeItems" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Fee Items</h5>
                                            </div>
                                            <table id="tblFeeItems" runat="server" class="table table-striped table-bordered nowrap display">
                                                <tr class="bg-light-blue">
                                                    <th style="font-weight: bold;">Sr. No.
                                                    </th>
                                                    <th style="font-weight: bold;">Fee Items
                                                    </th>
                                                    <th style="font-weight: bold;">Amount
                                                    </th>
                                                </tr>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <td></td>
                                                        <td class="data_label" style="font-weight: bold;">TOTAL AMOUNT:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTotalAmount" onkeydown="javascript:return false;" Style="text-align: right"
                                                                runat="server" CssClass="data_label" TabIndex="14" />
                                                        </td>
                                                    </tr>
                                                </tfoot>

                                            </table>

                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblFeeHeadSrNo" runat="server" Text='<%# Eval("SRNO") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("FEE_LONGNAME")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFeeItemAmount" onblur="UpdateTotalAmount(); validateAmount(this);" onkeyup="IsNumeric(this);"
                                                        Text='<%# Eval("AMOUNT")%>' Style="text-align: right" runat="server" CssClass="data_label"
                                                        TabIndex="14" />
                                                    <asp:HiddenField ID="hdnDemand" runat="server" Value='<%# Eval("DEMAND") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div class="col-md-6 col-12">
                                <div class="row">
                                    <div class="form-group col-12">
                                        <div class="label-dynamic">
                                            <label>Remark</label>
                                        </div>
                                        <asp:TextBox ID="txtRemark" Rows="4" TextMode="MultiLine" MaxLength="400" CssClass="form-control" runat="server" />
                                    </div>
                                    <div class="form-group col-12">
                                        <div class="label-dynamic">
                                            <label>Excess Amount</label>
                                        </div>
                                        <asp:TextBox ID="txtExcessAmt" onkeyup="IsNumeric(this);" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:HiddenField ID="hdnExcess" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                Enabled="false" CssClass="btn btn-primary" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <asp:HiddenField runat="server" ID="hdnTotalAmount" />
    <asp:HiddenField ID="hdnPrevExcessAmt" runat="server" />
    <div id="divModalboxContent" style="display: none;">
    </div>


    <script type="text/javascript" language="javascript">

        function ShowModalbox() {
            try {
                Modalbox.show($('divModalboxContent'), { title: 'Search Student for Fee Demand Modification', width: 600, overlayClose: false, slideDownDuration: 0.1, slideUpDuration: 0.1, afterLoad: SetFocus });
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return;
        }

        function SetFocus() {
            try {
                document.getElementById('<%= txtSearch.ClientID %>').focus();
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function SubmitSearch(btnsearch) {
            try {
                var searchParams = "";
                if (document.getElementById('<%= rdoName.ClientID %>').checked) {
                    searchParams = "Name=" + document.getElementById('<%= txtSearch.ClientID %>').value;
                    searchParams += ",EnrollNo=";
                }
                else if (document.getElementById('<%= rdoEnrollmentNo.ClientID %>').checked) {
                    searchParams = "EnrollNo=" + document.getElementById('<%= txtSearch.ClientID %>').value;
                    searchParams += ",Name=";
                }
            searchParams += ",DegreeNo=" + document.getElementById('<%= ddlDegree.ClientID %>').options[document.getElementById('<%= ddlDegree.ClientID %>').selectedIndex].value;
                searchParams += ",BranchNo=" + document.getElementById('<%= ddlBranch.ClientID %>').options[document.getElementById('<%= ddlBranch.ClientID %>').selectedIndex].value;
                searchParams += ",YearNo=" + document.getElementById('<%= ddlYear.ClientID %>').options[document.getElementById('<%= ddlYear.ClientID %>').selectedIndex].value;
                searchParams += ",SemNo=" + document.getElementById('<%= ddlSem.ClientID %>').options[document.getElementById('<%= ddlSem.ClientID %>').selectedIndex].value;

                __doPostBack(btnsearch, searchParams);
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return;
        }

        function UpdateTotalAmount() {
            try {
                debugger;
                var totalFeeAmt = 0.00;
                var dataRows = null;

                if (document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems') != null)
                    dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems').getElementsByTagName('tr');

                if (dataRows != null) {
                    for (i = 1; i < (dataRows.length - 1) ; i++) {
                        var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                        var dataCell = dataCellCollection.item(2);
                        var controls = dataCell.getElementsByTagName('input');
                        var txtAmt = controls.item(0).value;
                        if (txtAmt.trim() != '')
                            totalFeeAmt += parseFloat(txtAmt);
                    }

                    if (document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_txtTotalAmount') != null)
                        document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_txtTotalAmount').value = totalFeeAmt;
                    var hdnTotal = document.getElementById('<%= hdnTotalAmount.ClientID%>').value;
                    if (hdnTotal > totalFeeAmt) {
                        var Excess_amt = document.getElementById('<%= hdnPrevExcessAmt.ClientID%>').value;
                        document.getElementById('<%= txtExcessAmt.ClientID%>').value = parseFloat((hdnTotal - totalFeeAmt) + parseFloat(Excess_amt));
                        document.getElementById('<%= hdnExcess.ClientID%>').value = parseFloat((hdnTotal - totalFeeAmt) + parseFloat(Excess_amt));
                    }
                    else {
                        var Excess_amt = document.getElementById('<%= hdnPrevExcessAmt.ClientID%>').value;
                        document.getElementById('<%= txtExcessAmt.ClientID%>').value = parseFloat((hdnTotal - totalFeeAmt) + parseFloat(Excess_amt));
                        document.getElementById('<%= hdnExcess.ClientID%>').value = parseFloat((hdnTotal - totalFeeAmt) + parseFloat(Excess_amt));
                        //  document.getElementById('<%--= txtExcessAmt.ClientID--%>').value = 0.00;
                    }
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    document.getElementById(textbox.id).value = '';
                }
            }
        }

        function validateAmount(txt) {
            var myArr = new Array();
            myString = "" + txt.id + "";
            myArr = myString.split("_");
            var index = myArr[3];
            var demand = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_' + index + '_hdnDemand').value;

            var text = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_' + index + '_txtFeeItemAmount').value;
            if (parseFloat(demand) < parseFloat(text)) {
                alert('Amount should  be less than or Equal to Demand Amount');
                document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_' + index + '_txtFeeItemAmount').value = 0.00;
                document.getElementById('<%= txtExcessAmt.ClientID%>').value = 0.00;
                document.getElementById('<%= hdnExcess.ClientID%>').value = 0.00;
                UpdateTotalAmount();
            }



        }
    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

