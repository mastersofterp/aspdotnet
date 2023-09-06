<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ChalanReconciliation.aspx.cs" Inherits="Academic_ChalanReconciliation"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_lvStudent_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <style>
        /*.modal-body {
            height: calc(100vh - 126px);
            overflow-y: scroll;
        }

        .modalPopup {
            background-color: #fff;
            border: 1px solid #eee;
            box-shadow: 0 3px 9px rgba(0,0,0,.5);
            padding: 10px;
            position: fixed;
        }

        .fixed-div {
            width: 100%;
            position: fixed;
            z-index: 1051; 
            background: #fff;
            box-shadow: 0 5px 20px 4px rgba(0,0,0,.1);
        }*/
        /*.dataTables_scrollHeadInner {
            width: max-content !important;
        }*/

        .modalPopup {
            background-color: #fff;
            border: 1px solid #eee;
            box-shadow: 0 3px 9px rgba(0,0,0,.5);
            padding: 10px;
        }

        .modal-dialog1 {
            width: 545px;
            /*margin: 30px auto;*/
            position: relative;
            /*width: auto;*/
            margin: 10px;
            transition: transform .3s ease-out,-webkit-transform .3s ease-out,-o-transform .3s ease-out;
            transform: translate(0,0);
        }

        .modal-body1 {
            position: relative;
        }

        .modal-content {
            width: 630px;
        }

        .dataTables_scrollHeadInner {
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

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnlFeeTable"
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

    <asp:UpdatePanel ID="pnlFeeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">CHALLAN RECONCILIATION</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">

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
                                                            <sup>*</sup>
                                                            <label>Search Criteria</label>
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
                                                            <div id="divtxt" runat="server" style="display: block">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Search String</label>
                                                                </div>
                                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                                            </div>
                                                            <asp:Label ID="lblDateFormat" runat="server" Text="{dd/mm/yyyy}" Visible="false">
                                                            </asp:Label>
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
                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="div1">
                                                        <asp:Panel ID="pnlstatus" runat="server">

                                                            <div class="col-md-12" style="display: block">
                                                                <div class="label-dynamic">
                                                                    <%-- <label id="lblDropdown"></label>--%>
                                                                    <sup>*</sup>
                                                                    <b>Challan Status</b>
                                                                </div>
                                                                <asp:DropDownList runat="server" class="form-control" ID="ddlchalanstatus" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlchalanstatus_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">Approved Challans</asp:ListItem>
                                                                    <asp:ListItem Value="2">Pending Challans</asp:ListItem>
                                                                    <asp:ListItem Value="3">Deleted/Removed Challans</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvbankdd" runat="server" ControlToValidate="ddlchalanstatus" Display="None"
                                                                    ErrorMessage="Please Select Challan Status." ValidationGroup="submitstatus" SetFocusOnError="True" InitialValue="0">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                        </asp:Panel>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <%-- OnClientClick="return submitPopup(this.name);"--%>
                                                <asp:Button ID="Button1" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return submitPopup(this.name);" CssClass="btn btn-primary" />
                                                <asp:Button ID="btnview" runat="server" Text="View Challan" OnClick="btnview_Click" CssClass="btn btn-primary" ValidationGroup="submitstatus" />
                                                <asp:Button ID="BtnSubmit" runat="server" Text="Reconcile Challan" OnClick="BtnSubmit_Click" ValidationGroup="submit" CssClass="btn btn-primary" Visible="false" />
                                                <%-- <asp:Button id="" runat="server" type="button" value="Delete / Remove Challan"
                                                    visible="false" class="btn btn-warning" />--%>
                                                <asp:Button ID="btnExcelReport" runat="server" Text="Pending Challan(Excel)" OnClick="btnExcelReport_Click" CssClass="btn btn-primary" Visible="false" />
                                                <%--<asp:Button ID="btndeletechallan" runat="server" Text="View Delete/Remove Challan" CssClass="btn btn-primary" OnClick="btndeletechallan_Click" />--%>
                                                <asp:Button ID="btnClose" runat="server" Text="Clear" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="submitstatus" ShowMessageBox="true" ShowSummary="false"
                                                    DisplayMode="List" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit" ShowMessageBox="true" ShowSummary="false"/>
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
                                                                        <table class="table table-striped table-bordered" style="width: 100%" id="">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Name
                                                                                    </th>
                                                                                    <th style="display: none">IdNo
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
                                                                    <td style="display: none">
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


                                            <div class="col-12">
                                                <asp:ListView ID="lvSearchResults" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="listViewGrid">
                                                            <div class="sub-heading">
                                                                <h5>Challan Search Results</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered" style="width: 100%" id="tblSearchResults">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Select
                                                                        </th>
                                                                        <th>Challan No.
                                                                        </th>
                                                                        <th>Challan Date
                                                                        </th>
                                                                        <th>Receipt Type
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                        </th>
                                                                        <th>Pay Type
                                                                        </th>
                                                                        <th>Amount
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <%-- DO NOT FORMAT FOLLOWING 5-6 LINES. JAVASCRIPT IS DEPENDENT ON ELEMENT HIERARCHY --%>
                                                    <EmptyDataTemplate>
                                                    </EmptyDataTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <input id="rdoSelectRecord" value='<%# Eval("DCR_NO") %>' name="Receipts" type="radio"
                                                                    onclick="ShowRemark(this);" /><asp:HiddenField ID="hidRemark" runat="server" Value='<%# Eval("REMARK") %>' />
                                                                <asp:HiddenField ID="hidDcrNo" runat="server" Value='<%# Eval("DCR_NO") %>' />
                                                                <asp:HiddenField ID="hidDcrSemesterNo" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                                <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                            </td>
                                                            <td>
                                                                <%# Eval("REC_NO") %>
                                                            </td>
                                                            <td>
                                                                <%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("RECIEPT_TITLE") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("SEMESTERNAME")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("PAY_MODE_CODE")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("TOTAL_AMT")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>

                                            <div class="col-12" id="divRemark" runat="server" visible="false">
                                                <div class="form-group col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Reason for deleting challan</label>
                                                    </div>
                                                    <asp:TextBox ID="txtRemark" Rows="4" TextMode="MultiLine" Width="450px" MaxLength="400"
                                                        runat="server" />
                                                    
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <input id="btnReconcile" onclick="ReconcileChalan();" runat="server" type="button" visible="false"
                                                    value="Reconcile Challan" class="btn btn-primary" />
                                                <input id="btnDelete" onclick="DeleteChalan();" runat="server" type="button" value="Delete / Remove Challan"
                                                    visible="false" class="btn btn-warning" />
                                            </div>
                                            <div class="col-12" id="divReconDel" runat="server" visible="false">
                                                <div class="form-group col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Remark for Reconciliation</label>
                                                    </div>
                                                    <asp:TextBox ID="txtBulkRemark" Rows="4" TextMode="MultiLine" Width="450px" MaxLength="400"
                                                        runat="server" />
                                                    <asp:RequiredFieldValidator ID="valtxtBulkRemark" runat="server" ControlToValidate="txtBulkRemark" 
                                            ValidationGroup="submit" Display="None" ErrorMessage="Please enter remark for reconciliation" />
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                            </div>


                                            <div class="col-md-12">
                                                <asp:Panel ID="pnlChallan" runat="server" Visible="false">
                                                    <asp:ListView ID="lvChallan" runat="server" OnItemDataBound="lvChallan_ItemDataBound">

                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Student List</h5>
                                                            </div>
                                                            <table class="table table-hover table-bordered table-striped nowrap display" id="divsessionlist" style="width: 100%">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th style="text-align: center" class="re-move">
                                                                            <asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckbox(this)" />
                                                                        </th>
                                                                        <th>Studname
                                                                        </th>
                                                                        <th>Enrollment No. 
                                                                        </th>
                                                                        <th>Branch
                                                                        </th>
                                                                        <th>Semester
                                                                        </th>
                                                                        <th>Reciept Title
                                                                        </th>
                                                                        <th>Reciept No
                                                                        </th>
                                                                        <th>
                                                                            DDNO/Cheque No
                                                                        </th>
                                                                        <th>Reciept Date
                                                                        </th>
                                                                        <th>Total Amount
                                                                        </th>
                                                                        <th>Status</th>
                                                                        <%--<th>Preview
                                                                        </th>--%>
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
                                                                <td style="text-align: center">
                                                                    <asp:CheckBox ID="chkadm" runat="server" ToolTip='<%# Eval("IDNO")%>' />
                                                                    <asp:Label ID="lbldcr" runat="server" Text='<% #Eval("DCR_NO")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblsemesterno" runat="server" Text='<% #Eval("SEMESTERNO")%>' Visible="false"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("STUDNAME") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("REGNO") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("BRANCH")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SEMESTERNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("RECIEPT_TITLE")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("REC_NO")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("DD_NO")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("REC_DT")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("TOTAL_AMT")%>
                                                                </td>
                                                                <%--     <td>
                                                                    <asp:UpdatePanel ID="updPreview" runat="server">
                                                                        <%--<img src="../Images/view2.png" />--%>
                                                                <%--        <ContentTemplate>
                                                                            <asp:ImageButton ID="imgbtnPrevDoc" runat="server" CommandArgument='<%# Eval("FILENAME_SLIP") %>' data-target="#PassModel" data-toggle="modal" Height="20px" ImageUrl="../Images/search.png" Text="Preview" ToolTip='<%# Eval("FILENAME_SLIP") %>' OnClick="imgbtnPrevDoc_Click" Width="20px" Visible='<%# Convert.ToString(Eval("FILENAME_SLIP"))==string.Empty?false:true %>' />
                                                                            <asp:Label ID="lblPreview" Text='<%# Convert.ToString(Eval("FILENAME_SLIP"))==string.Empty ?"Preview is not available":"" %>' runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                                        </ContentTemplate>
                                                                        <%--<Triggers>
                                                                <asp:PostBackTrigger ControlID="imgbtnPrevDoc"/>
                                                            </Triggers>   --%>
                                                                <%--       </asp:UpdatePanel>
                                                                </td>--%>
                                                                <td>
                                                                    <asp:Label ID="lblstatus" runat="server" Text='<% #Eval("STATUS")%>' Font-Bold="true" ForeColor='<%# (Convert.ToInt32(Eval("RECON") )== 1 ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <%--<FooterTemplate>
                                        </tbody></table>
                                    </FooterTemplate>--%>
                                                    </asp:ListView>
                                                </asp:Panel>

                                            </div>



                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnExcelReport" />
                                            <asp:PostBackTrigger ControlID="Button1" />
                                            <%-- <asp:PostBackTrigger ControlID="lvChallan" />--%>
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <%--Search Pannel End--%>
                            </div>



                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>



        </ContentTemplate>
        <Triggers>

            <asp:PostBackTrigger ControlID="btnReconcile" />
            <asp:PostBackTrigger ControlID="btnDelete" />
            <%--<asp:PostBackTrigger ControlID="lvChallan" />--%>
        </Triggers>
    </asp:UpdatePanel>

    <ajaxToolKit:ModalPopupExtender ID="mpeViewDocument" BehaviorID="mpeViewDocument" runat="server" PopupControlID="pnlmpopup"
        TargetControlID="lnkPreview" CancelControlID="btnClose" BackgroundCssClass="modalBackground"
        OnOkScript="ResetSession()">
    </ajaxToolKit:ModalPopupExtender>
    <asp:LinkButton ID="lnkPreview" runat="server" CommandArgument='<%# Eval("FILENAME_SLIP") %>'></asp:LinkButton>

    <div class="modal fade" id="PassModel">
        <div class="modal-dialog">
            <div class="modal-content">

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlmpopup" runat="server" CssClass="modalPopup">
                            <div class="modal-header">
                                <h4 class="modal-title">Document</h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>

                            <!-- Modal body -->
                            <div class="modal-body">
                                <div class="col-12">
                                    <iframe runat="server" width="550px" height="450px" id="iframeView"></iframe>
                                </div>
                            </div>

                            <!-- Modal footer -->
                            <div class="modal-footer" style="display: none">
                                <asp:Button ID="btnclose1" runat="server" Text="Close" CssClass="btn btn-danger no" />
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnclose" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <script type="text/javascript" language="javascript">

        function ReconcileChalan() {
            try {
                if (ValidateRecordSelection()) {
                    __doPostBack("ReconcileChalan", "");
                }
                else {
                    alert("Please Select a Chalan to Reconcile.");
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function DeleteChalan() {
            try {
                if (ValidateRecordSelection()) {
                    if (document.getElementById('<%= txtRemark.ClientID %>').value.trim() != "") {
                        if (confirm("If you delete this chalan, it will not be appear in the system.\n\nAre you sure you want to delete this chalan?")) {
                            __doPostBack("DeleteChalan", "");
                        }
                    }
                    else
                        alert('Please Enter Reason of deleting chalan.');
                }
                else {
                    alert("Please select a Chalan to delete.");
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function ValidateRecordSelection() {
            var check = false;
            var gridView = document.getElementById("tblSearchResults");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "radio") {
                    if (checkBoxes[i].checked) {
                        check = true;
                    }

                }
            }
            return check;
        }

        function BulkDeleteChalan() {
            try {

                if (document.getElementById('<%= txtBulkRemark.ClientID %>').value.trim() != "") {
                    if (confirm("If you delete this chalan, it will not be appear in the system.\n\nAre you sure you want to delete this chalan?")) {
                        __doPostBack("DeleteChalan", "");
                    }
                }
                else
                    alert('Please Enter Reason of deleting chalan.');

            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }
        //function ValidateRecordSelection() {
        //    var isValid = true;
        //    try {
        //        var tbl = document.getElementById('tblSearchResults');
        //        if (tbl != null && tbl.rows && tbl.rows.length > 0) {
        //            for (i = 1; i < tbl.rows.length; i++) {
        //                var dataRow = tbl.rows[i];
        //                var dataCell = dataRow.firstChild;
        //                var rdo = dataCell.firstChild;
        //                if (rdo.checked) {
        //                    isValid = true;
        //                }
        //            }
        //        }
        //    }
        //    catch (e) {
        //        alert("Error: " + e.description);
        //    }
        //    return isValid;
        //    }



        function ShowRemark(rdoSelect) {
            try {
                if (rdoSelect != null && rdoSelect.nextSibling != null) {
                    var hidRemark = rdoSelect.nextSibling;
                    if (hidRemark != null)
                        document.getElementById('<%= txtRemark.ClientID %>').value = hidRemark.value;
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }
    </script>
    <script type="text/javascript">

        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvChallan$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvChallan$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>


    <%--Search Box Script Start--%>
    <script type="text/javascript" lang="javascript">

        $(document).ready(function () {
            debugger
            $("#<%= pnltextbox.ClientID %>").hide();

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
                        $("#<%= pnltextbox.ClientID %>").hide();

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
    <%-- <script>
        function show(ddl) {             
            //    var ddl = 0;
            //    ddl = document.getElementById("ctl00_ContentPlaceHolder1_ddlchalanstatus");
            //    alert(ddl)
            //    if (ddl == 1) {
            //        chkmain.visible = true;
            //    }
            //    else {
                    
            //    }
            //}
            
    </script>--%>
    <%-- <script>
        $('select#selcat').change(function () {
            $("#maplist tr").hide().filter($(this).val()).show();
        }).change();
    </script>--%>
</asp:Content>
