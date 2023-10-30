<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GrievanceApp.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="GrievanceRedressal_Transaction_GrievanceApp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
       
        #DataTables_Table_0_wrapper.table.dataTable.nowrap th, table.dataTable.nowrap td {
    white-space: inherit;
}
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updGrievanceApplication"
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

    <asp:UpdatePanel ID="updGrievanceApplication" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">GRIEVANCE APPLICATION</h3>
                        </div>

                        <div class="box-body">
                            <div id="divGR" runat="server">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Application Date </label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <%--<asp:Image ID="imgCalFromdt" runat="server" ImageUrl="~/Images/calendar.png" Style="cursor: pointer" />--%>

                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TabIndex="1"
                                                        MaxLength="10" ToolTip="Application Date" Enabled="false" Style="z-index: 0;"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvdt" runat="server" ControlToValidate="txtDate"
                                                        Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Student Name </label>
                                                </div>
                                                <asp:TextBox ID="txtstudname" runat="server" MaxLength="70" Enabled="false" CssClass="form-control" TabIndex="2" ToolTip="Student Name" />
                                                <asp:RequiredFieldValidator ID="rfvstudname" runat="server" ControlToValidate="txtstudname"
                                                    Display="None" ErrorMessage="Please Enter Student Name"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Student Admission No. </label>
                                                </div>
                                                <asp:TextBox ID="txtAdmissionNo" runat="server" MaxLength="70" Enabled="false" CssClass="form-control" TabIndex="2" ToolTip="Admission No" />
                                                <asp:RequiredFieldValidator ID="rfvadmissionNo" runat="server" ControlToValidate="txtAdmissionNo"
                                                    Display="None" ErrorMessage=" "></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Degree </label>
                                                </div>
                                                <asp:TextBox ID="txtDegree" runat="server" MaxLength="70" CssClass="form-control" TabIndex="3" Enabled="false" ToolTip="Degree Name" />
                                                <asp:RequiredFieldValidator ID="rfvdegree" runat="server" ControlToValidate="txtDegree"
                                                    Display="None" ErrorMessage="Please Enter Degree Name"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Branch </label>
                                                </div>
                                                <asp:TextBox ID="txtbranch" runat="server" MaxLength="50" Enabled="false" CssClass="form-control" TabIndex="4" ToolTip="Branch Name" />
                                                <asp:RequiredFieldValidator ID="rfvbranch" runat="server" ControlToValidate="txtbranch"
                                                    Display="None" ErrorMessage="Please Enter Branch Name"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Semester </label>
                                                </div>
                                                <asp:TextBox ID="txtsemester" runat="server" MaxLength="30" Enabled="false" CssClass="form-control" TabIndex="5" ToolTip="Semester Name" />
                                                <asp:RequiredFieldValidator ID="rfvsemester" runat="server" ControlToValidate="txtsemester"
                                                    Display="None" ErrorMessage="Please Enter Semester Name"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Mobile No. </label>
                                                </div>
                                                <asp:TextBox ID="txtMobile" runat="server" MaxLength="20" Enabled="false" CssClass="form-control" TabIndex="6" ToolTip="Mobile No" />
                                                <asp:RequiredFieldValidator ID="rfvmobileNo" runat="server" ControlToValidate="txtMobile" ErrorMessage="Please Enter Mobile No."
                                                    Display="None"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTxtExtmobileno" runat="server"
                                                    FilterType="Numbers" TargetControlID="txtMobile" ValidChars=" ">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Email Id </label>
                                                </div>
                                                <asp:TextBox ID="txtEmail" runat="server" MaxLength="60" Enabled="false" CssClass="form-control" ToolTip="Email Id" TabIndex="7" onblur="validateEmail(this.value);"> 
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="*" ControlToValidate="txtEmail"
                                                    ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="rgvemail" runat="server" ErrorMessage="Please Enter Valid Email ID"
                                                    ControlToValidate="txtEmail" CssClass="requiredFieldValidateStyle" ForeColor="Red"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Grievance Type </label>
                                                </div>
                                                <asp:DropDownList ID="ddlGrievanceT" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="8" AppendDataBoundItems="true"
                                                    ToolTip="Select Grievance Type" AutoPostBack="true" OnSelectedIndexChanged="ddlGrievanceT_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvGrievanceType" runat="server" ControlToValidate="ddlGrievanceT" InitialValue="0"
                                                    Display="None" ValidationGroup="GrievanceValidate" ErrorMessage="Please Select Grievance Type " SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="GrApplnNo" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Grievance Application No. </label>
                                                </div>
                                                <asp:TextBox ID="txtGrAppNo" runat="server" MaxLength="100" Enabled="false" CssClass="form-control" ToolTip="Grievance Application No"
                                                    TabIndex="7"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvGrApplnNo" runat="server" ControlToValidate="txtGrAppNo"
                                                    Display="None" ErrorMessage="Please Enter Grievance Application No" ValidationGroup="GrievanceValidate" SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Grievance Details </label>
                                                </div>
                                                <asp:TextBox ID="txtGrievance" runat="server" MaxLength="3000" CssClass="form-control" ToolTip="Enter Grievance"
                                                    TextMode="MultiLine" TabIndex="9"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvGrievancee" runat="server" ControlToValidate="txtGrievance"
                                                    Display="None" ErrorMessage="Please Enter Grievance  " ValidationGroup="GrievanceValidate" SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="col-12">
                                                <label>
                                                    <span style="color: #FF0000">Valid files : (.jpg,.pdf,.doc,.txt should be of 10 Mb size.)</span>
                                                </label>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Upload Document </label>
                                                </div>
                                                <asp:UpdatePanel ID="UpdatePanelfile" runat="server">
                                                    <ContentTemplate>
                                                        <asp:FileUpload ID="flupDoc" runat="server" ValidationGroup="GrievanceValidate" ToolTip="Select file to upload" TabIndex="9" />
                                                        <%--<asp:Button ID="btnAdd" runat="server" Text="Add"  OnClick="btnAdd_Click"
                                                                        ValidationGroup="GrievanceValidate" CssClass="btn btn-primary"
                                                                        CausesValidation="False" TabIndex="10" />--%>
                                                        <%-- <asp:Label ID="lblResult" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>--%>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnSave" />
                                                        <%--<asp:PostBackTrigger ControlID="btnAdd" />--%>
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 table-responsive">
                                        <asp:Panel ID="pnlAttach" runat="server" Visible="false">
                                            <asp:ListView ID="lvattachment" runat="server">
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <div class="sub-heading">
                                                            <h5>Download files</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Action
                                                                    </th>
                                                                    <th>File Name
                                                                    </th>
                                                                    <th>Download
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
                                                            <asp:ImageButton ID="btnFileDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%#DataBinder.Eval(Container, "DataItem") %>'
                                                                AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnFileDelete_Click"
                                                                OnClientClick="return deleletconfig()" />
                                                        </td>
                                                        <td>
                                                            <%#GetFileName(DataBinder.Eval(Container, "DataItem")) %>
                                                        </td>

                                                        <td>
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:ImageButton ID="imgFileDownload" runat="Server" ImageUrl="~/Images/action_down.png"
                                                                        AlternateText='<%#DataBinder.Eval(Container, "DataItem") %>' ToolTip='<%#DataBinder.Eval(Container, "DataItem")%>'
                                                                        OnClick="imgFileDownload_Click" />
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="imgFileDownload" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>

                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </asp:Panel>

                                <div id="divButton" runat="server">
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="GrievanceValidate"
                                            CssClass="btn btn-primary" TabIndex="10" ToolTip="Click here to Submit" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnBack" runat="server" CausesValidation="false" Visible="false"
                                            Text="Back" CssClass="btn btn-primary" TabIndex="12" ToolTip="Click here to Go back to Previous Menu" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                            CssClass="btn btn-warning" TabIndex="11" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />

                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="GrievanceValidate"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>

                                    <div class="col-12">
                                        <asp:Panel ID="pnlGrievance" runat="server">
                                            <asp:ListView ID="lvGrApplication" runat="server">
                                                <LayoutTemplate>
                                                    <div id="Div2">
                                                        <div class="sub-heading">
                                                            <h5>Grievance Application Details</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap  display" id="" style="width:100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Edit </th>
                                                                    <th>Grievance </th>
                                                                    <th>Mobile No </th>
                                                                    <th>Email Id </th>
                                                                    <th>Uploaded File</th>
                                                                    <th>Status </th>
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
                                                            <asp:ImageButton ID="btnEditRecord" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("GAID") %>' ImageUrl="~/Images/edit1.gif" OnClick="btnEditRecord_Click" ToolTip="Edit Record" />
                                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("GAID") %>' ImageUrl="~/Images/delete.png" OnClick="btnDelete_Click" OnClientClick="return deleletconfig()" ToolTip="Delete Record" />
                                                            <asp:HiddenField ID="hdngrivId" runat="server" Value='<%# Eval("GRIV_ID") %>' />
                                                        </td>
                                                        <td><%# Eval("GRIEVANCE")%></td>
                                                        <td><%# Eval("MOBILE_NO")%></td>
                                                        <td><%# Eval("EMAIL_ID")%></td>
                                                        <td>
                                                            <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("GRIV_ATTACHMENT"),Eval("GAID"),Eval("STUD_IDNO"))%>'><%# Eval("GRIV_ATTACHMENT")%></asp:HyperLink>
                                                        </td>
                                                        <td><%# Eval("STATUS")%></td>
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
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>

    <script>
        function deleletconfig() {

            var del = confirm("Are you sure you want to Delete this Record?");
            if (del == true) {
                //  alert("Record Deleted")
            } else {
                //  alert("Record Not Deleted")
            }
            return del;
        }
    </script>
      
</asp:Content>
