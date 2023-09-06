<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ITLEProcessPendingResult.aspx.cs" Inherits="ITLE_ITLEProcessPendingResult" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">TEST RESULT</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlStudentResult" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Process Pending Objective Result</h5>
                                    </div>
                                </div>
                                <div class="col-lg-5 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Session :</b>
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

                        <div class="col-12 mt-3">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Select Test</label>
                                    </div>
                                    <asp:DropDownList ID="ddlTest" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlTest_SelectedIndexChanged"
                                        ValidationGroup="Select Schemeno" ToolTip="Select Test" TabIndex="2"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-12">
                            <asp:Panel ID="pnlResultList" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="lvRewsult" runat="server">
                                    <LayoutTemplate>
                                        <div id="demo-grid">
                                            <div class="sub-heading">
                                                <h5>Test Result</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sr.No
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Test Status
                                                        </th>
                                                        <th>Process Result
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
                                                <%#Container.DataItemIndex + 1%>
                                            </td>

                                            <td>
                                                <%# Eval("STUDNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("TEST_STATUS")%>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="btnProcessResult" runat="server" ToolTip='<%# Eval("TDNO") %>' CssClass="btn btn-primary" CommandArgument='<%# Eval("IDNO") %>' OnClick="btnProcessResult_Click">Process Result</asp:LinkButton>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>

                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

        function totAllIDs(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }

        }
        //    function totAllIDs(headchk) {
        //      

        //        var frm = document.forms[0]
        //        for (i = 0; i < document.forms[0].elements.length; i++) {
        //            var e = frm.elements[i];
        //            if (e.type == 'checkbox') {
        //                if (e.name.endsWith('chkAccept')) {
        //                    if (headchk.checked == true) {
        //                        e.checked = true;
        //                        
        //                    }
        //                    else
        //                        e.checked = false;

        //                }
        //            }
        //        }


        //            var frm = document.forms[0]
        //            for (i = 0; i < document.forms[0].elements.length; i++) {
        //                var e = frm.elements[i];
        //                if (e.type == 'checkbox') {
        //                    if (headchk.checked == true)
        //                        e.checked = true;
        //                   
        //                    else
        //                        e.checked = false;
        //                }
        //            }



    </script>

    <%--  Enable the button so it can be played again --%>
</asp:Content>

