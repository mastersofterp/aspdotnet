<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Generate_RollNo.aspx.cs" Inherits="ACADEMIC_Generate_RollNo" Title="" %>

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

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPnl"
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

    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">CLASS ROLL NUMBER GENERATION</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree </label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                            AutoPostBack="True" ToolTip="Please Select Degree">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" InitialValue="0" SetFocusOnError="true"
                                            ControlToValidate="ddlDegree" Display="None" ErrorMessage="Please Select Degree."
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch </label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                            AutoPostBack="True" ToolTip="Please Select Branch">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" InitialValue="0" SetFocusOnError="true"
                                            ControlToValidate="ddlBranch" Display="None" ErrorMessage="Please Select Branch"
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True"
                                            OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"
                                            AutoPostBack="True" ToolTip="Please Select Semester">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" InitialValue="0" SetFocusOnError="true"
                                            ControlToValidate="ddlSemester" Display="None" ErrorMessage="Please Select Semester."
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Section </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" TabIndex="4" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True"
                                            AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"
                                            ToolTip="Please Select Section">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSection" runat="server" InitialValue="0" SetFocusOnError="true"
                                            ControlToValidate="ddlSection" Display="None" ErrorMessage="Please Select Section."
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Gender </label>
                                        </div>
                                        <asp:RadioButtonList ID="rdoSex" runat="server" RepeatDirection="Horizontal" TabIndex="5"
                                            OnSelectedIndexChanged="rdoSex_SelectedIndexChanged" AutoPostBack="True"
                                            ToolTip="Please Select Gender">
                                            <asp:ListItem Value="M">Male</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="F">Female</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Roll No. From </label>
                                        </div>
                                        <asp:TextBox ID="txtFrmRollno" runat="server" TabIndex="6" onkeyup="validateNumeric(this);"
                                            CssClass="form-control" MaxLength="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFrmRollno" runat="server" SetFocusOnError="true"
                                            ControlToValidate="txtFrmRollno" Display="None" ErrorMessage="Please Enter Roll No. From"
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Roll No. To </label>
                                        </div>
                                        <asp:TextBox ID="txtToRollno" runat="server" CssClass="form-control" TabIndex="7" MaxLength="3"
                                            onkeyup="validateNumeric(this);"
                                            ToolTip="Please Enter Roll No." />
                                        <asp:RequiredFieldValidator ID="rfvToRollno" runat="server" SetFocusOnError="true"
                                            ControlToValidate="txtToRollno" Display="None" ErrorMessage="Please Enter Roll No. To"
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cvRoll" runat="server" ControlToCompare="txtFrmRollno"
                                            ControlToValidate="txtToRollno" Display="None" ErrorMessage="RollNo. From can not be grater than RollNo. To"
                                            Operator="GreaterThan" Type="Integer" ValidationGroup="submit"></asp:CompareValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:ValidationSummary ID="vsGenRoll" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" />

                                <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" CssClass="btn btn-primary"
                                    Text="Show Students" TabIndex="8" />

                                <asp:Button ID="btnGenerate" runat="server" Text="Generate" TabIndex="9" ValidationGroup="submit"
                                    CssClass="btn btn-primary" OnClick="btnGenerate_Click" />

                                <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="10" CausesValidation="False"
                                    OnClick="btnReport_Click" Visible="False" CssClass="btn btn-info" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="11" CausesValidation="False"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" />

                                <asp:Label ID="lblCount" runat="server" Style="color: Red; font-weight: bold;"></asp:Label>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlStud" runat="server" Visible="false">
                                    <div class="sub-heading">
                                        <h5>Student List</h5>
                                    </div>
                                    <asp:Repeater ID="lvStudents" runat="server">
                                        <HeaderTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Roll No.
                                                        </th>
                                                        <th>Enrollment No.
                                                        </th>
                                                        <th>Name
                                                        </th>
                                                        <th>Branch
                                                        </th>
                                                        <th>Semester
                                                        </th>
                                                        <th>Section
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <%--<tr id="itemPlaceholder" runat="server" />--%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td>
                                                    <%# Eval("ROLLNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ENROLLMENTNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STUDNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("BRANCH")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTER")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SECITON")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody></table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
   
    <div id="divMsg" runat="server">
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
    </script>

</asp:Content>
