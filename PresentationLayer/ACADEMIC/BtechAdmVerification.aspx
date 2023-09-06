<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BtechAdmVerification.aspx.cs" Inherits="ACADEMIC_BtechAdmVerification" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updStudent"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updStudent" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h5 class="box-title" style="font-size: x-large;">B.TECH ADMISSION VERIFICATION</h5>
                            <div class="box-tools pull-right">
                                <span style="Color: Red;">Note : * marked fields are Mandatory</span>
                            </div>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <%-- <legend>Criteria for Report Generation</legend>--%>
                                <div class="form-group col-md-4">
                                    <label><span style="Color: Red;">*</span> Admission Batch</label>
                                    <asp:DropDownList ID="ddlAdmbatch" runat="server" AppendDataBoundItems="True" TabIndex="1" OnSelectedIndexChanged="ddlAdmbatch_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmbatch"
                                        Display="None" ErrorMessage="Please Select Admission Batch" SetFocusOnError="true" ValidationGroup="Show"
                                        InitialValue="0" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAdmbatch"
                                        Display="None" ErrorMessage="Please Select Admission Batch" SetFocusOnError="true" ValidationGroup="Report"
                                        InitialValue="0" />

                                </div>


                                <div class="form-group col-md-4">
                                    <label><span style="color: red;">*</span>Degree</label>
                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                        AutoPostBack="True" TabIndex="3">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="Show">
                                    </asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-md-4">
                                    <label><span style="color: red;">*</span> Programme/Branch</label>
                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                        AutoPostBack="True" TabIndex="4">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Programme/Branch" ValidationGroup="Show">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </fieldset>

                        </div>
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnShow" runat="server" Text="Show Students" OnClick="btnShow_Click"
                                    TabIndex="5" ValidationGroup="Show" CssClass="btn btn-primary" />
                                <asp:Button ID="btnSubmit" runat="server" TabIndex="6" Text="Submit" ToolTip="Click to Submit" Visible="false"
                                    class="btn btn-success" OnClick="btnSubmit_Click" ValidationGroup="Submit" />
                                <asp:Button ID="btnReport" runat="server" TabIndex="7" Text="Report"
                                    class="btn btn-info" OnClick="btnReport_Click" ValidationGroup="Report" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="8" CssClass="btn btn-danger" />

                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" TabIndex="9" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Report" TabIndex="10" />
                            </p>
                            <div id="divstudentdetail" style="display: block" visible="false" runat="server">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="box box-info">
                                            <div class="box-header with-border">
                                                <h3 class="box-title">Student List</h3>
                                            </div>
                                            <div class="box-body">
                                                <asp:ListView ID="lvStudentDetail" runat="server" OnItemDataBound="lvStudentDetail_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <div id="divlvFeeItems" class="vista-grid">
                                                            <table id="tblStudent" class="dataTable table table-bordered table-striped table-hover" style="width: 100%">
                                                                <tr class="header bg-light-blue">
                                                                    <th>Student Name</th>
                                                                    <th>Roll No.</th>
                                                                    <th>Branch</th>
                                                                    <th>Mobile No.</th>
                                                                    <th>View Document 1</th>
                                                                    <th>View Document 2</th>
                                                                    <th>Allotted category</th>
                                                                    <th>Category Rank</th>
                                                                    <th>Payment Status</th>
                                                                    <th>Verify</th>
                                                                    <%--<th> <asp:CheckBox ID="cbHead" runat="server" onclick="SelectAll(this)" ToolTip="Select All" Text="Verify" /></th>--%>
                                                                </tr>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="item">
                                                            <td>
                                                                <%# Eval("STUDNAME")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("ROLLNO")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("BRANCH")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("MOBILENO")%>
                                                                
                                                            </td>

                                                            <td>
                                                                <%# Eval("DOCUMENT1")%>
                                                            </td>
                                                            <td>
                                                              <%# Eval("DOCUMENT2")%>
                                                            </td>
                                                            <td>
                                                               <%# Eval("ALLOTEDCATEGORY")%>
                                                            </td>
                                                              <td>
                                                               <%# Eval("CATEGORYRANK")%>
                                                            </td>
                                                             <td>
                                                               <%# Eval("PAYMEENTSTATUS")%>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                                <asp:HiddenField ID="hidStudentNo" runat="server" Value='<%# Eval("IDNO") %>' />
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
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
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

