<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="DemandModification.aspx.cs" Inherits="Academic_DemandModification"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_lvStudent_Panel2 .dataTables_scrollHeadInner,
        #ctl00_ContentPlaceHolder1_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#divcolgglist').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false,

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#divcolgglist').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                                {
                                    extend: 'copyHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#divcolgglist').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                                {
                                    extend: 'excelHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#divcolgglist').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                                {
                                    extend: 'pdfHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#divcolgglist').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#divcolgglist').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false,

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#divcolgglist').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                                    {
                                        extend: 'copyHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#divcolgglist').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'excelHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#divcolgglist').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'pdfHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#divcolgglist').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });

    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <%-- <h3 class="box-title">FEE DEMAND MODIFICATION</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <div class="row">


                            <div class="col-12">

                                <%--Search Pannel Start by Swapnil --%>
                                <div id="myModal2" role="dialog" runat="server">
                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updEdit"
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
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup><label>Search Criteria</label>
                                                        </div>

                                                        <%--onchange=" return ddlSearch_change();"--%>
                                                        <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <%-- <asp:ListItem>Please Select</asp:ListItem>
                                                        <asp:ListItem>BRANCH</asp:ListItem>
                                                        <asp:ListItem>ENROLLMENT NUMBER</asp:ListItem>
                                                        <asp:ListItem>REGISTRATION NUMBER</asp:ListItem>
                                                        <asp:ListItem>FatherName</asp:ListItem>
                                                        <asp:ListItem>IDNO</asp:ListItem>
                                                        <asp:ListItem>MOBILE NUMBER</asp:ListItem>
                                                        <asp:ListItem>MotherName</asp:ListItem>
                                                        <asp:ListItem>NAME</asp:ListItem>
                                                        <asp:ListItem>ROLLNO</asp:ListItem>
                                                        <asp:ListItem>SEMESTER</asp:ListItem>--%>
                                                        </asp:DropDownList>

                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">


                                                        <asp:Panel ID="pnltextbox" runat="server">
                                                            <div id="divtxt" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Search String</label>
                                                                </div>
                                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                                            </div>
                                                        </asp:Panel>

                                                        <asp:Panel ID="pnlDropdown" runat="server">
                                                            <div id="divDropDown" runat="server" style="display: block">
                                                                <div class="label-dynamic">
                                                                    <%-- <label id="lblDropdown"></label>--%>
                                                                    <sup>*</sup>
                                                                    <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                                </asp:DropDownList>

                                                            </div>
                                                        </asp:Panel>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <%-- OnClientClick="return submitPopup(this.name);"--%>
                                                <asp:Button ID="Button1" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return submitPopup(this.name);" CssClass="btn btn-primary" />
                                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                            </div>

                                            <div>
                                                <div class="col-12">
                                                    <asp:Panel ID="pnlLV" runat="server">
                                                        <asp:ListView ID="lvStudent" runat="server">
                                                            <LayoutTemplate>
                                                                <div id="listViewGrid" class="vista-grid">
                                                                    <div class="sub-heading">
                                                                        <h5>Student List</h5>
                                                                    </div>
                                                                    <asp:Panel ID="Panel2" runat="server">
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Name
                                                                                    </th>
                                                                                    <th>IdNo
                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                                    </th>
                                                                                    <th>Father Name
                                                                                    </th>
                                                                                    <th>Mother Name
                                                                                    </th>
                                                                                    <th>Mobile No.
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
                                                                        <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                            OnClick="lnkId_Click"></asp:LinkButton>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("idno")%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SEMESTERNO")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("FATHERNAME") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("MOTHERNAME") %>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("STUDENTMOBILE") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <%--Search Pannel End--%>
                            </div>

                            <div class="col-12 mt-4">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="col-12">
                                    <ContentTemplate>
                                        <div id="divAllDemands" runat="server">
                                            <asp:Panel ID="Panel2" runat="server">
                                                <asp:ListView ID="lvAllDemands" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Available Fee Demands</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Edit
                                                                    </th>
                                                                    <th>Name
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                    </th>
                                                                    <th>Year
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lblDYddlSemester_Tab2" runat="server" Font-Bold="true"></asp:Label>
                                                                    </th>
                                                                    <th>Acad. Batch
                                                                    </th>
                                                                    <th>Receipt_Code
                                                                    </th>
                                                                    <th>Payment Cat.
                                                                    </th>
                                                                    <th>Demand
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
                                                                <asp:ImageButton ID="btnEditDemand" runat="server" OnClick="btnEditDemand_Click"
                                                                    CommandArgument='<%# Eval("DM_NO") %>' CommandName='<%# Eval("IDNO") %>' ImageUrl="~/Images/edit.png"
                                                                    ToolTip="Edit Demand" />
                                                            </td>
                                                            <td>
                                                                <%# Eval("NAME") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("ENROLLNMENTNO") %>
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
                                                            <td>
                                                                <%# Eval("BATCHNAME")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("RECIEPT_CODE")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("PAYTYPENAME")%>
                                                            </td>
                                                            <td style="text-align: center;">
                                                                <%# Eval("TOTAL_AMT")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>

                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="col-12 mt-4">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
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

                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <%-- <div id="divFeeItems" visible="false" runat="server" class="col-12 mt-5">--%>
                                        <asp:Panel ID="divFeeItems" runat="server" Visible="true">
                                            <asp:ListView ID="lvFeeItems" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Fee Items</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblFeeItems">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Sr.No.
                                                                </th>
                                                                <th>Fee Items
                                                                </th>
                                                                <th>Amount
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                        <tfoot>
                                                            <tr>

                                                                <td></td>
                                                                <td>TOTAL:</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTotalAmount" onkeydown="javascript:return false;" runat="server"
                                                                        AutoCompleteType="Disabled" CssClass="form-control" Font-Bold="true" />
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
                                                            <asp:TextBox ID="txtFeeItemAmount" onblur="UpdateTotalAmounts();" onkeyup="IsNumeric(this);"
                                                                Text='<%# Eval("AMOUNT")%>' Style="text-align: right" runat="server" CssClass="data_label"
                                                                TabIndex="14" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                        <div class="form-group col-lg-6 col-md-6 col-12 mt-4" id="remark" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtRemark" Rows="4" TextMode="MultiLine" MaxLength="400"
                                                runat="server" />

                                        </div>
                                        <div class="col-12 btn-footer" runat="server" id="btnSubCan">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                                Enabled="false" CssClass="btn btn-primary" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                        </div>
                                        <asp:HiddenField ID="hfConfirmInstalCancel" runat="server" />
                                        <%--  </div>--%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>




    <%--Search Box Script Start--%>
    <script type="text/javascript" lang="javascript">

        $(document).ready(function () {
            debugger
            //$("#<%= pnltextbox.ClientID %>").hide();

            $("#<%= pnlDropdown.ClientID %>").hide();
        });
        function submitPopup(btnsearch) {

            debugger
            var rbText;
            var searchtxt;

            var e = document.getElementById("<%=ddlSearch.ClientID%>");
            var rbText = e.options[e.selectedIndex].text;
            var ddlname = e.options[e.selectedIndex].text;
            if (rbText == "Please Select") {
                alert('Please Select Search Criteria.')
                $(e).focus();
                return false;
            }

            else {


                if (rbText == "ddl") {
                    var skillsSelect = document.getElementById("<%=ddlDropdown.ClientID%>").value;

                    var searchtxt = skillsSelect;
                    if (searchtxt == "0") {
                        alert('Please Select ' + ddlname + '..!');
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        return true;
                        //$("#<%= divpanel.ClientID %>").hide();
                        //$("#<%= pnltextbox.ClientID %>").hide();

                    }
                }
                else if (rbText == "BRANCH") {

                    if (searchtxt == "Please Select") {
                        alert('Please Select Branch..!');

                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);

                        return true;
                    }

                }
                else {
                    searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;
                    if (searchtxt == "" || searchtxt == null) {
                        alert('Please Enter Data to Search.');
                        //$(searchtxt).focus();
                        document.getElementById('<%=txtSearch.ClientID %>').focus();
                        return false;
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        //$("#<%= divpanel.ClientID %>").hide();
                        //$("#<%= pnltextbox.ClientID %>").show();

                        return true;
                    }
                }
        }
    }

    function ClearSearchBox(btncancelmodal) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btncancelmodal, '');
        return true;
    }
    function CloseSearchBox(btnClose) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btnClose, '');
        return true;
    }




    function Validate() {

        debugger

        var rbText;

        var e = document.getElementById("<%=ddlSearch.ClientID%>");
        var rbText = e.options[e.selectedIndex].text;

        if (rbText == "IDNO" || rbText == "Mobile") {

            var char = (event.which) ? event.which : event.keyCode;
            if (char >= 48 && char <= 57) {
                return true;
            }
            else {
                return false;
            }
        }
        else if (rbText == "NAME") {

            var char = (event.which) ? event.which : event.keyCode;

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122)) {
                return true;
            }
            else {
                return false;
            }

        }
    }
    </script>
    <%--Search Box Script End--%>

    <script type="text/javascript" language="javascript">
        function UpdateTotalAmounts() {
            try {
                var totalFeeAmt = 0.00;
                var dataRows = null;
                if (document.getElementById('tblFeeItems') != null)
                    dataRows = document.getElementById('tblFeeItems').getElementsByTagName('tr');
                //alert(dataRows.length)
                if (dataRows != null) {

                    for (sem = 2; sem < 5; sem++) {
                        //alert("sem "+sem)
                        totalFeeAmt = 0.00;
                        for (i = 1; i < (dataRows.length - 1) ; i++) {

                            var dataCellCollection = dataRows.item(i).getElementsByTagName('td');

                            var dataCell = dataCellCollection.item(sem);

                            var controls = dataCell.getElementsByTagName('input');

                            var txtAmt = controls.item(0).value;
                            //alert("txtAmt "+txtAmt)
                            if (txtAmt != '')
                                totalFeeAmt += parseFloat(txtAmt);
                            //alert("totalFeeAmt"+totalFeeAmt)
                            //if ((i + 2) == dataRows.length) {
                            var semcnt = sem - 1;
                            document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_txtTotalAmount').value = totalFeeAmt.toString();
                            //}
                        }
                    }
                }
            }
            catch (e) {
                //alert("Error: " + e.description);
            }
        }
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



        function UpdateTotalAmount() {
            try {
                var totalFeeAmt = 0.00;
                var dataRows = null;

                if (document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems') != null)
                    dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems');

                alert(dataRows)
                if (dataRows != null) {
                    for (i = 1; i < (dataRows.length - 1) ; i++) {

                        var dataCellCollection = dataRows.item(i).getElementsByTagName('td');

                        var dataCell = dataCellCollection.item(3);
                        var controls = dataCell.getElementsByTagName('input');
                        var txtAmt = controls.item(0).value;
                        if (txtAmt.trim() != '')
                            totalFeeAmt += parseFloat(txtAmt);
                        alert(totalFeeAmt)
                    }
                    if (document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_txtTotalAmount') != null)
                        document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_txtTotalAmount').value = totalFeeAmt;
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    document.getElementById(textbox.id).value = 0;
                }
            }
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $(':text').bind('keydown', function (e) {
                //on keydown for all textboxes prevent from postback
                if (e.target.className != "searchtextbox") {
                    if (e.keyCode == 13) { //if this is enter key
                        document.getElementById('<%=Button1.ClientID%>').click();
                          e.preventDefault();
                          return true;
                      }
                      else
                          return true;
                  }
                  else
                      return true;
              });
          });

          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              $(function () {
                  $(':text').bind('keydown', function (e) {
                      //on keydown for all textboxes
                      if (e.target.className != "searchtextbox") {
                          if (e.keyCode == 13) { //if this is enter key
                              document.getElementById('<%=Button1.ClientID%>').click();
                            e.preventDefault();
                            return true;
                        }
                        else
                            return true;
                    }
                    else
                        return true;
                });
            });

        });
    </script>
    <script type="text/javascript">

        function OpenConfirmDialog(studId, sem, demandId) {
            if (confirm('Installment Allotment found for this Demand. Do you want to remove the Installment for Demand Modification.')) {
                //$('#ctl00_ContentPlaceHolder1_hfConfirmInstalCancel').val('1');
                CancelInstallDemand(studId, sem, demandId);
                $('#ctl00_ContentPlaceHolder1_UpdatePanel2').show();
                return true;
            }
            else {
                // $('#ctl00_ContentPlaceHolder1_hfConfirmInstalCancel').val('0');
                $('#ctl00_ContentPlaceHolder1_UpdatePanel2').hide();
                return false;
            }
        }
        function CancelInstallDemand(studId, sem, demandId) {
            $.ajax({
                type: "POST",
                url: '<%=Page.ResolveUrl("~/ACADEMIC/DemandModification.aspx/CancelInstallnment")%>',
                data: '{studId: ' + studId + ',sem:' + sem + ',demandId:' + demandId + '}',
                contentType: "application/json",
                dataType: "json",
                success: function (data) {
                    var r = JSON.stringify(data.d);
                    var x = JSON.parse(r);                   
                    if (x === '2') {
                        alert('Installment Allotment cancelled Successfully.');
                        $('#ctl00_ContentPlaceHolder1_UpdatePanel2').show();
                    }
                    else { $('#ctl00_ContentPlaceHolder1_UpdatePanel2').hide(); return false; }
                }                
            });
        }
    </script>
    <div id="divMsg" runat="server">
    </div>

</asp:Content>
