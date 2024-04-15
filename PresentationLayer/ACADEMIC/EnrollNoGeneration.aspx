<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="EnrollNoGeneration.aspx.cs" Inherits="ACADEMIC_EnrollNoGeneration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <script type="text/javascript">
        function showConfirm(value) {
            var ret = "";
            if (value == 1) {
                ret = confirm('Do You Really Want to Generate Class Roll No.?');
            }
            else {
                ret = confirm('Do You Really Want to Generate Registration No.?');
            }
            if (ret == true) {
                FreezeScreen('Please Wait, Your Data is Being Processed...');
                validate = true;
            }
            else
                validate = false;
            return validate;
        }
    </script>
    <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true" Visible="false"></asp:Label>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--  <h3 class="box-title">REGISTRATION NO. AND CLASS ROLL NO. GENERATION</h3>--%>
                            <%--      <h3 class="box-title"><asp:Label runat="server" ID="LblHear"></asp:Label></h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="col-12 mb-3" style="padding: 0px; font-size: medium">
                                    <asp:RadioButton ID="radRegNoGen" runat="server" GroupName="SelectRadio" Text="PRN Number Generation" AutoPostBack="true" OnCheckedChanged="radRegNoGen_CheckedChanged" Checked="true" />&nbsp;&nbsp;&nbsp;&nbsp;
                                   <asp:RadioButton ID="radRollNoGen" runat="server" GroupName="SelectRadio" Text="Roll Number Generation" AutoPostBack="true" OnCheckedChanged="radRollNoGen_CheckedChanged" />
                                </div>

                                <asp:Panel runat="server" ID="pnlShow" Visible="false">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Admission Batch</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch" ValidationGroup="RegNoAllot"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Admission Batch">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>School/Institute Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                ValidationGroup="SeatAllot" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname"
                                                Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0" ValidationGroup="RegNoAllot">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Degree</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                                ValidationGroup="SeatAllot" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="RegNoAllot">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Programme/Branch</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBranch" AppendDataBoundItems="true" runat="server" ValidationGroup="RegNoAllot" TabIndex="4" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBranch"
                                                Display="None" ErrorMessage="Please Select Programme/Branch" InitialValue="0" ValidationGroup="RegNoAllot">
                                            </asp:RequiredFieldValidator>
                                        </div>


                                        <%--added by prafull on 281021--%>


                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="true" id="DivYear">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Year</label>
                                            </div>
                                            <asp:DropDownList ID="ddlyear" AppendDataBoundItems="true" runat="server" ValidationGroup="RegNoAllot" TabIndex="4" CssClass="form-control" data-select2-enable="true" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlyear"
                                                Display="None" ErrorMessage="Please Select year" InitialValue="0" ValidationGroup="RegNoAllot">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DivSem">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Semester</label>
                                            </div>
                                            <asp:DropDownList ID="ddlsemester" AppendDataBoundItems="true" runat="server" ValidationGroup="RegNoAllot" TabIndex="4" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlsemester_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlsemester"
                                                Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="RegNoAllot">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="Divsection">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Section</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSection" AppendDataBoundItems="true" runat="server" ValidationGroup="RegNoAllot" TabIndex="4" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSection"
                                                Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="RegNoAllot">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divAdType" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Admission Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlidtype" OnSelectedIndexChanged="ddlidtype_SelectedIndexChanged" TabIndex="5" AutoPostBack="true" AppendDataBoundItems="true" runat="server" ValidationGroup="RegNoAllot" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlidtype"
                                                Display="None" ErrorMessage="Please Select Admission Type" InitialValue="0" ValidationGroup="RegNoAllot">
                                            </asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%-- <asp:Label ID="lblyear" runat ="server" Font-Bold="true">Year</asp:Label>--%>
                                                <label>Sort By option 1</label>
                                            </div>
                                            <asp:DropDownList ID="ddlsort" AppendDataBoundItems="true" runat="server" ValidationGroup="RegNoAllot" TabIndex="4" CssClass="form-control" data-select2-enable="true" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvsortby" runat="server" ControlToValidate="ddlsort"
                                                Display="None" ErrorMessage="Please Select Sort By" InitialValue="0" ValidationGroup="RegNoAllot">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <%--<sup>* </sup>--%>
                                                <%-- <asp:Label ID="lblyear" runat ="server" Font-Bold="true">Year</asp:Label>--%>
                                                <label>Sort By option 2</label>
                                            </div>
                                            <asp:DropDownList ID="ddlgender" AppendDataBoundItems="true" runat="server"  TabIndex="4" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlgender_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Gender</asp:ListItem>
                                            </asp:DropDownList>
                                            
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12"  id="divgender" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Gender</label>
                                            </div>
                                            <asp:RadioButtonList ID="rdbgender" runat="server" RepeatDirection="Horizontal">
                                           <asp:ListItem Enabled="True" Text="Male" Value="M" />
                                           <asp:ListItem Enabled="True" Text="Female" Value="F" />
                                           </asp:RadioButtonList>
                                            
                                        </div>
                                        
                                    </div>
                                </asp:Panel>

                            </div>

                            <div class="col-12 btn-footer" id="buttonSection" runat="server" visible="false">
                                <asp:Button ID="btnShow" runat="server" ValidationGroup="RegNoAllot" Text="Show"
                                    OnClick="btnShow_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnGenerateRoll" runat="server" ValidationGroup="RegNoAllot" Text="Generate Roll Number"
                                    OnClick="btnGenerateRoll_Click" CssClass="btn btn-primary" Visible="false" OnClientClick="return showConfirm(1);" Enabled="false" />
                                <asp:Button ID="btnGenRegNo" runat="server" ValidationGroup="RegNoAllot" Text="Generate PRN No."
                                    OnClick="btnGenRegNo_Click" CssClass="btn btn-primary" Visible="false" OnClientClick="return showConfirm(2);" Enabled="false" />
                                <asp:Button ID="btnGenerateRR" runat="server" ValidationGroup="RegNoAllot" Text="Generate RR No."
                                    OnClick="btnGenerateRR_Click" CssClass="btn btn-primary" Visible="false" OnClientClick="return showConfirm(2);" Enabled="false" />
                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click"
                                    Text="Report" CssClass="btn btn-info" Enabled="false" Visible="false" ValidationGroup="RegNoAllot" />
                                <asp:Button ID="btnClear0" runat="server" Text="Cancel"
                                    OnClick="btnClear0_Click1" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="RegNoAllot" />
                            </div>

                            <div class="col-12">
                                <asp:ListView ID="lvStudents" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Student List</h5>
                                        </div>
                                        <asp:Panel ID="pnlStudent" runat="server">
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="lstTable">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sr No.</th>
                                                        <th>Student Name</th>
                                                        <th>PRN Number</th>
                                                        <%--  <th>Roll Number</th>--%>
                                                        <%-- <th>Previous Enrollment No. </th>--%>
                                                        <th>Admission Type </th>
                                                        <th>Merit No. </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </asp:Panel>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Container.DataItemIndex + 1 %></td>
                                            <td><%# Eval("STUDNAME")%></td>
                                            <td><%# Eval("REGNO")%></td>

                                            <%--  commented by prafull--%>
                                            <%--<td><%# Eval("ROLLNO")%></td>--%>

                                            <%--<td><%# Eval("ENROLLNO")%></td>--%>
                                            <%-- <td><%# Eval("ADM_TYPE")%></td>--%>
                                            <td>
                                                <asp:Label ID="lblAdmType" runat="server" Text='<%# Convert.ToInt32(Eval("IDTYPE"))==1 ? "REGULAR" : "D2D" %>'></asp:Label></td>
                                            <td><%# Eval("MERITNO") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                                <asp:ListView ID="lvlrollno" runat="server">
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr No. </th>
                                                    <th>Student Name </th>
                                                    <th>PRN Number </th>

                                                    <%--  commented by prafull--%>
                                                    <th>Roll Number</th>

                                                    <%-- <th>Previous Enrollment No. </th>--%>
                                                    <th>Admission Type </th>
                                                    <%-- <th>Merit No. </th>--%>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:PlaceHolder ID="ItemPlaceHolder" runat="server"></asp:PlaceHolder>
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Container.DataItemIndex + 1 %></td>
                                            <td><%# Eval("STUDNAME")%></td>
                                            <td><%# Eval("REGNO")%></td>

                                            <%--  commented by prafull--%>
                                            <td><%# Eval("ROLLNO")%></td>

                                            <%--<td><%# Eval("ENROLLNO")%></td>--%>
                                            <%-- <td><%# Eval("ADM_TYPE")%></td>--%>
                                            <td>
                                                <asp:Label ID="lblAdmType" runat="server" Text='<%# Convert.ToInt32(Eval("IDTYPE"))==1 ? "REGULAR" : "D2D" %>'></asp:Label></td>
                                            <%--<td><%# Eval("MERITNO") %></td>--%>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                                <asp:ListView ID="lvStudList" runat="server">
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr No. </th>
                                                    <th>Student Name </th>
                                                    <th>RRN Number </th>
                                                    <th>Admission Type </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:PlaceHolder ID="ItemPlaceHolder" runat="server"></asp:PlaceHolder>
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Container.DataItemIndex + 1 %></td>
                                            <td><%# Eval("STUDNAME")%></td>
                                            <td><%# Eval("REGNO")%></td>
                                            <td>
                                                <asp:Label ID="lblAdmType" runat="server" Text='<%# Convert.ToInt32(Eval("IDTYPE"))==1 ? "REGULAR" : "D2D" %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            var lbl = document.getElementById('<%= lblDYRRNo.ClientID %>').innerText;
            alert(lbl);
            if (lbl.indexOf('RR') > -1) {
                alert("true");
            }
            else {
                alert("false");
            }
        });

    </script>
    <%--<script>
        $(document).ready(function () {
            $('#pnlStudent').DataTable({
                "scrollY": 200,
                "scrollX": true,
                "paging": false,
                "lengthChange": false,
                "searching": false,
                "ordering": false,
                "info": false,
                "autoWidth": false
            });

        });
    </script>--%>
</asp:Content>
