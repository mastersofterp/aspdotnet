<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TransportRequitionApplication.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_TransportRequitionApplication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updApplication"
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
    <asp:UpdatePanel ID="updApplication" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TRANSPORT REQUISITION APPLICATION</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12" id="divGR" runat="server">

                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="col-12">
                                        <%--  <div class=" sub-heading">
                                                <h5>Transport Requisition Application</h5>
                                            </div>--%>
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>College/School Name</label>
                                                </div>
                                                <asp:TextBox ID="txtInstitution" runat="server" MaxLength="70" Enabled="false" CssClass="form-control" TabIndex="1"
                                                    ToolTip="Intitution Name" />

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Degree</label>
                                                </div>
                                                <asp:TextBox ID="txtDegree" runat="server" MaxLength="70" CssClass="form-control" TabIndex="5" Enabled="false" ToolTip="Degree Name" />

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Branch</label>
                                                </div>
                                                <asp:TextBox ID="txtbranch" runat="server" MaxLength="50" Enabled="false" CssClass="form-control" TabIndex="7" ToolTip="Branch Name" />

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Admission Batch</label>
                                                </div>
                                                <asp:TextBox ID="txtBatch" runat="server" MaxLength="70" CssClass="form-control" TabIndex="6" Enabled="false" ToolTip="Batch Name" />

                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Session</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Semester / Year</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSemester" AppendDataBoundItems="true" runat="server" Enabled="false"
                                                    CssClass="form-control" data-select2-enable="true" TabIndex="2">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Student Name </label>
                                                </div>
                                                <asp:TextBox ID="txtstudname" runat="server" MaxLength="70" Enabled="false" CssClass="form-control" TabIndex="2" ToolTip="Student Name" />

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>TAN / PAN</label>
                                                </div>
                                                <asp:TextBox ID="txtAdmissionNo" runat="server" MaxLength="70" Enabled="false" CssClass="form-control" TabIndex="4" ToolTip="Admission No" />

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Application Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="Image2">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TabIndex="3"
                                                        MaxLength="10" ToolTip="Application Date" Enabled="false" Style="z-index: 0;"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="rfvdt" runat="server" ControlToValidate="txtDate"
                                                                    Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Stop Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlVehicleStop" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="9" AppendDataBoundItems="true"
                                                    ToolTip="Select Stop" OnSelectedIndexChanged="ddlVehicleStop_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlVehicleStop" InitialValue="0"
                                                    Display="None" ValidationGroup="Validate" ErrorMessage="Please Select Stop " SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Vehicle Category </label>
                                                </div>
                                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="8" AppendDataBoundItems="true"
                                                    ToolTip="Select Category" AutoPostBack="false" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategory" InitialValue="0"
                                                    Display="None" ValidationGroup="Validate" ErrorMessage="Please Select Category " SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Fees Amount</label>
                                                </div>
                                                <asp:TextBox ID="txtFeesAmount" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Fees Amount"
                                                    Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12" id="divPeriod" runat="server">
                                                <div class="row">
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Period From </label>
                                                        </div>
                                                        <asp:TextBox ID="txtPeriodfrom" runat="server" MaxLength="10" Enabled="true" CssClass="form-control"
                                                            ToolTip="Period From" TabIndex="10"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvPeriodFrom" runat="server" ControlToValidate="txtPeriodfrom"
                                                            Display="None" ErrorMessage="  " ValidationGroup="Validate" SetFocusOnError="true">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Period To</label>
                                                        </div>
                                                        <asp:TextBox ID="txtPeriodTo" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter Period To"
                                                            TabIndex="11" Enabled="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvPeriodTo" runat="server" ControlToValidate="txtPeriodTo"
                                                            Display="None" ErrorMessage="Please enter period to" ValidationGroup="Validate" SetFocusOnError="true">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divsem" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Semester</label>
                                                </div>
                                                <asp:TextBox ID="txtsemester" runat="server" MaxLength="30" Enabled="false" CssClass="form-control" TabIndex="12" ToolTip="Semester Name" />

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divmob" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Mobile No</label>
                                                </div>
                                                <asp:TextBox ID="txtMobile" runat="server" MaxLength="20" Enabled="false" CssClass="form-control" TabIndex="13" ToolTip="Mobile No" />
                                                <%-- <asp:RequiredFieldValidator ID="rfvmobileNo" runat="server" ControlToValidate="txtMobile" ErrorMessage="Please Enter Mobile No."
                                                                Display="None"></asp:RequiredFieldValidator>--%>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTxtExtmobileno" runat="server"
                                                    FilterType="Numbers" TargetControlID="txtMobile" ValidChars=" ">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divemail" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Email Id </label>
                                                </div>
                                                <asp:TextBox ID="txtEmail" runat="server" MaxLength="60" Enabled="false" CssClass="form-control" ToolTip="Email Id" TabIndex="14" onblur="validateEmail(this.value);"> 
                                                </asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="*" ControlToValidate="txtEmail"
                                                                ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                <asp:RegularExpressionValidator ID="rgvemail" runat="server" ErrorMessage="Please Enter Valid Email ID"
                                                    ControlToValidate="txtEmail" CssClass="requiredFieldValidateStyle" ForeColor="Red"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                                            </div>





                                        </div>
                                    </div>
                                </asp:Panel>

                            </div>
                            <div class="box-footer" id="divButton" runat="server">

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Validate"
                                        CssClass="btn btn-primary" TabIndex="15" ToolTip="Click here to Submit" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnBack" runat="server" CausesValidation="false" Visible="false"
                                        Text="Back" CssClass="btn btn-primary" TabIndex="17" ToolTip="Click here to Go back to Previous Menu" />

                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        CssClass="btn btn-warning" TabIndex="16" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Validate"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>

                                <div class="col-12 mt-3">
                                    <asp:Panel ID="pnlTransport" runat="server">
                                        <asp:ListView ID="lvTRApplication" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading">
                                                        <h5>Transport Requisition Application Details</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>
                                                                <th>Application Date 
                                                                </th>
                                                                <th>Category
                                                                </th>
                                                                <th>Amount
                                                                </th>
                                                                <th>Stop
                                                                </th>
                                                                <th>Status
                                                                </th>
                                                               <%-- <th>Session
                                                                </th>
                                                                <th>Semester/ Year
                                                                </th>--%>
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
                                                        <asp:ImageButton ID="btnEditRecord" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                            CommandArgument='<%# Eval("VTRAID") %>' ImageUrl="~/Images/edit.png" OnClick="btnEditRecord_Click"
                                                            ToolTip="Edit Record" />
                                                        <%-- <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("GAID") %>'
                                                        ToolTip="Delete Record" OnClick="btnDelete_Click" OnClientClick="return deleletconfig()" />--%>
                                                    
                                                    </td>
                                                    <td>
                                                        <%# Eval("APP_DATE")%>
                                                   
                                                    </td>
                                                    <td>
                                                        <%# Eval("CATEGORYNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("AMOUNT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("STOPNAME")%>                                                    
                                                    </td>
                                                    <td>
                                                        <%# Eval("STATUS")%>                                                    
                                                    </td>
                                                    <%--<td>
                                                        <%# Eval("SESSION_NAME")%>                                                    
                                                    </td>
                                                    <td><%#Eval("SEMESTERNAME")%> <%#Eval("lbl_SEM_YEAR")%> </td>--%>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        function deleletconfig() {

            var del = confirm("Are you sure you want to delete this record?");
            if (del == true) {
                alert("record deleted")
            } else {
                alert("Record Not Deleted")
            }
            return del;
        }
    </script>


</asp:Content>

