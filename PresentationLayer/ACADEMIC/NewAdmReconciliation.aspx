<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="NewAdmReconciliation.aspx.cs" Inherits="ACADEMIC_NewAdmReconciliation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updStudent"
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

     <asp:UpdatePanel ID="updStudent" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">New Admission Challan Reconciliation</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmbatch" runat="server" AppendDataBoundItems="True" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" SetFocusOnError="true" ValidationGroup="Show"
                                            InitialValue="0" />
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShow" runat="server" Text="Show Student" OnClick="btnShow_Click"
                                            TabIndex="10" ValidationGroup="Show" CssClass="btn btn-primary" />
                                         <asp:Button ID="btnSubmit" runat="server" TabIndex="6" Text="Submit" ToolTip="Click to Submit" Visible="false"
                                          class="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="Submit" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="11" CssClass="btn btn-warning" />

                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Show" TabIndex="12" />
                                    </div>
                                </div>
                            </div>

                            <div id="divstudentdetail" style="display: block" visible="false" runat="server">
                                <div class="col-12">
                                    <asp:ListView ID="lvStudentDetail" runat="server">
                                        <LayoutTemplate>
                                            <div id="divlvFeeItems">
                                                <div class="sub-heading">
                                                    <h5>Student List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblStudent">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th><asp:CheckBox ID="cbHead" runat="server" onclick="SelectAll(this)" ToolTip="Select All" Text="Select All" /></th>
                                                            <th>Student Name</th>
                                                            <th>Branch</th>
                                                            <th>Semester</th>
                                                            <th>Amount</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td>
                                                        <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                    <asp:HiddenField ID="hidStudentNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("STUDNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("LONGNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTERNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TOTAL_AMT")%>
                                                </td>
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
    <script type="text/javascript" language="javascript">

        function SelectAll(headchk) {
            var frm = document.forms[0];
            var tbl = document.getElementById('tblStudent');
            var chkHead = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentDetail_cbHead');


            for (i = 0; i < tbl.rows.length - 1; i++) {
                var chkRow = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentDetail_ctrl' + i + '_cbRow');
                //alert(chkRow)
                if (chkHead.checked == true)
                    chkRow.checked = true;
                else
                    chkRow.checked = false;
            }
        }


    </script>
</asp:Content>

