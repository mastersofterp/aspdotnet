<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EmpStudAllotment.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_EmpStudAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table2').DataTable();
        });

    </script>

    <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };
    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <script type="text/javascript">
        ; debugger
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPanel"
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
    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ROUTE ALLOTMENT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note</h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Search employee by Employee Code and student by Enrollment No.</span> </p>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Select</label>
                                            </div>
                                            <asp:RadioButtonList ID="rdbUserType" runat="server" RepeatDirection="Horizontal" ToolTip="Select User Type"
                                                OnSelectedIndexChanged="rdbUserType_SelectedIndexChanged" AutoPostBack="true" TabIndex="1">
                                                <asp:ListItem Selected="True" Value="1">Employee&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2">Student</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>
                                                    <asp:Label ID="lblSearch" runat="server" Text=""></asp:Label></label>
                                            </div>
                                            <asp:TextBox ID="txtEmployee" runat="server" CssClass="form-control" TabIndex="9"
                                                placeholder="Type here to search " MaxLength="200"
                                                OnTextChanged="txtEmployee_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                            <asp:HiddenField ID="hfIdNo" runat="server"/>

                                            <ajaxToolKit:AutoCompleteExtender ID="autAgainstAc" runat="server" TargetControlID="txtEmployee"
                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                ServiceMethod="GetEmployeeName" OnClientShowing="clientShowing" OnClientItemSelected="GetEmpName">
                                            </ajaxToolKit:AutoCompleteExtender>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divEmp" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Employee</label>
                                            </div>
                                            <asp:DropDownList ID="ddlEmployee" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Select Employee" TabIndex="2">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lblVType" runat="server" Text=""></asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvEmployee" runat="server" ErrorMessage="Please Select Employee."
                                                ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlEmployee" Display="None">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divDegree" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Degree</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Select Degree" TabIndex="3" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ErrorMessage="Please Select Degree."
                                                ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlDegree" Display="None">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divBranch" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Branch</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Select Branch" TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ErrorMessage="Please Select Branch."
                                                ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlBranch" Display="None">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSem" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Semester</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Select Semester" TabIndex="5" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSem" runat="server" ErrorMessage="Please Select Semester."
                                                ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlSem" Display="None">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divStud" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Student</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStudent" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Select Student" TabIndex="6">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvStudent" runat="server" ErrorMessage="Please Select Student."
                                                ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlStudent" Display="None">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trRouteDrop" runat="server" visible="true">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Route</label>
                                            </div>
                                            <asp:DropDownList ID="ddlRoute" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged" TabIndex="7" ToolTip="Select Route">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvRoute" runat="server" ErrorMessage="Please Select Route."
                                                ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlRoute" Display="None">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divBP" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Boarding Point</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBoardingPoint" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                TabIndex="9" ToolTip="Select Boarding Point">
                                                <%--<asp:ListItem Value="0">Please Select</asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvBP" runat="server" ErrorMessage="Please Select Boarding Point."
                                                ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlBoardingPoint" Display="None">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="Submit"
                                    OnClick="btnSubmit_Click" TabIndex="10" ToolTip="Click here to Submit" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnReport_Click"
                                    TabIndex="12" ToolTip="Click here to Show Report" />
                                <asp:Button ID="btnStudReport" runat="server" Text="Student Report" CssClass="btn btn-info" OnClick="btnStudReport_Click"
                                    TabIndex="13" ToolTip="Student Report" />
                                <asp:Button ID="btnCancelAllot" runat="server" Text="Cancel Allotment" CssClass="btn btn-primary" OnClick="btnCancelAllot_Click"
                                    TabIndex="14" ToolTip="Cancel Allotment" Visible="false" />
                                <asp:Button ID="btnCAReport" runat="server" Text="Cancel Allotment Report" CssClass="btn btn-primary" OnClick="btnCAReport_Click"
                                    TabIndex="14" ToolTip="Cancel Allotment" Visible="false" />
                                <asp:ValidationSummary ID="VS1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="Submit" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click"
                                    TabIndex="11" ToolTip="Click here to Reset" />
                                <asp:ValidationSummary ID="VS2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Add" />

                            </div>
                            <div id="divCanRem" runat="server" class="col-md-12" visible="false">
                                <div class="form-group row">
                                    <div class="form-group col-md-4">
                                        <label>
                                            <span style="color: #FF0000">*</span>Cancel Remark :</label>
                                        <asp:TextBox ID="txtCanRemark" runat="server" CssClass="form-control" placeholder="Enter Remark For Cancel Allotment." TabIndex="15" TextMode="MultiLine" ToolTip="Enter Remark For Cancel Allotment."></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCanRem" runat="server" ControlToValidate="txtCanRemark" Display="None" ErrorMessage="Please Enter Cancel Remark." ValidationGroup="Cancel"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-2">

                                        <asp:Button ID="btnSaveCRemark" runat="server" CssClass="btn btn-primary" OnClick="btnSaveCRemark_Click" TabIndex="10" Text="Save Cancel Remark" ToolTip="Click here to Submit" ValidationGroup="Cancel" />
                                        <asp:ValidationSummary ID="VSCancel" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Cancel" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvAllotment" runat="server">
                                        <%-- <EmptyDataTemplate>
                                            <br />
                                            <br />
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Complaints For Allotment"></asp:Label>
                                        </EmptyDataTemplate>--%>
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Route Allotment Lists</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="table2">
                                                    <thead class="bg-light-blue">
                                                        <tr>

                                                            <th>ACTION </th>
                                                            <th>
                                                                <asp:Label ID="lblNumber" runat="server" Text="PFILENO"></asp:Label>
                                                            </th>
                                                            <th>
                                                                <asp:Label ID="lblUserName" runat="server" Text="EMPLOYEE NAME"></asp:Label>
                                                            </th>
                                                            <th>ROUTE NAME </th>
                                                            <th>BOARDING POINT </th>
                                                            <th>ROUTE PATH </th>
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CommandArgument='<%# Eval("URID") %>' ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                </td>
                                                <td><%# Eval("NUMBER")%></td>
                                                <td><%# Eval("NAME")%></td>
                                                <td><%# Eval("ROUTENAME")%></td>
                                                <td><%# Eval("STOPNAME")%></td>
                                                <td><%# Eval("ROUTEPATH")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function GetEmpName(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            var Name = idno[0].split("-");
            document.getElementById('ctl00_ContentPlaceHolder1_txtEmployee').value = idno[1];
            document.getElementById('ctl00_ContentPlaceHolder1_hfIdNo').value = Name[0];
            //document.getElementById('ctl00_ContentPlaceHolder1_hfIdNo').value = txtEmployee.Text;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployee').value = Name[0];


        }

    </script>
</asp:Content>


