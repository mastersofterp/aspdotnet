<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DirectComOff.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_DirectComOff" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

      <script type="text/javascript" language="javascript">
          // Move an element directly on top of another element (and optionally
          // make it the same size)


          function totAllSubjects(headchk) {
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

    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAll"
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
    <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">DIRECT COM-OFF CREDIT</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true"
                                                ToolTip="Select College Name">
                                            </asp:DropDownList>
                                            <%-- OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"  AutoPostBack="true"--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College Name" ValidationGroup="Leaveapp"
                                                SetFocusOnError="true" InitialValue="0" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddldeprtment" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="2"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Select Department" OnSelectedIndexChanged="ddldeprtment_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvdepartment" runat="server" ControlToValidate="ddldeprtment"
                                                Display="None" ErrorMessage="Please Select Department" SetFocusOnError="true" ValidationGroup="Leaveapp"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Employee Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlEmp" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="3"
                                                AutoPostBack="True" ToolTip="Select Employee Name">
                                                <%--OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged"--%>
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                         <%--   <asp:RequiredFieldValidator ID="rfvEmp" runat="server" ControlToValidate="ddlEmp"
                                                Display="None" ErrorMessage="Please Select Employee Name" ValidationGroup="Leaveapp"
                                                SetFocusOnError="true" InitialValue="0" />--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Working Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgCalWorkingDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtWDate" runat="server" CssClass="form-control" TabIndex="4" Style="z-index: 0;"
                                                    MaxLength="10" ToolTip="Enter Working Date" OnTextChanged="txtWDate_TextChanged"></asp:TextBox>
                                                <%--OnTextChanged="txtWDate_TextChanged"  SelectedDate="<%# DateTime.Today %>" --%>

                                                <ajaxToolKit:CalendarExtender ID="ceWorkingDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtWDate"
                                                    Enabled="true" EnableViewState="true" PopupButtonID="imgCalWorkingDate">
                                                </ajaxToolKit:CalendarExtender>

                                                <%-- <asp:RequiredFieldValidator ID="rfvWorkingDt" runat="server" ControlToValidate="txtWDate"
                                                                    Display="None" ErrorMessage="Please Enter Working Date" ValidationGroup="Leaveapp"
                                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>--%>

                                                <ajaxToolKit:MaskedEditExtender ID="meeWorkingDt" runat="server" TargetControlID="txtWDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevWorkingDt" runat="server" ControlExtender="meeWorkingDt"
                                                    ControlToValidate="txtWDate" EmptyValueMessage="Please Enter Working Date" InvalidValueMessage="Working Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter Working Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Leaveapp" SetFocusOnError="true" IsValidEmpty="false">
                                                </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>No.Of Day</label>
                                            </div>
                                            <asp:TextBox ID="txtdays" runat="server" CssClass="form-control" ToolTip="Enter No.of Days" TabIndex="5">1</asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rfvnodays" runat="server" ControlToValidate="txtdays" Display="None"
                                                        ErrorMessage="Please Enter No.of Day" ValidationGroup="Leaveapp" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtdays"
                                                        ValidChars=".0123456789" Enabled="True">
                                                    </ajaxToolKit:FilteredTextBoxExtender>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Reason</label>
                                            </div>
                                            <asp:textbox id="txtReason" runat="server" cssclass="form-control" textmode="MultiLine" tabindex="6" onkeypress="if (this.value.length > 100) { return false; }" xmlns:asp="#unknown" />
                                            <asp:RequiredFieldValidator ID="RFVReason" runat="server" ControlToValidate="txtReason"
                                                Display="None" ErrorMessage="Please Enter Valid Reason" ValidationGroup="Leaveapp"
                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                </div>
                                <div>
                                    <asp:Panel ID="pnlList" runat="server">
                                        <asp:ListView ID="lvEmpList" runat="server">
                                            <EmptyDataTemplate>
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Employees" CssClass="d-block text-center mt-3" />
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Select Employees</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Sr.No
                                                            </th>
                                                            <th>
                                                                <%--<asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Employee Name" Enabled="true" runat="server" onclick="checkAllEmployees(this)" />--%>
                                                                <asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Select All" Enabled="true" runat="server"
                                                                    onclick="totAllSubjects(this)" TabIndex="7" ToolTip="Check to Select All Employee" />
                                                            </th>
                                                            <th>Employee Name
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
                                                        <%#Container.DataItemIndex+1 %>
                                                    </td>
                                                    <td>
                                                        <%-- <asp:CheckBox ID="chkSelect" runat="server" TabIndex="9" ToolTip='<%#Eval("Idno")%>' /> Text='<%#Eval("NAME")%>'--%>
                                                        <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' ToolTip='<%#Eval("Idno")%>' />
                                                    </td>
                                                    <td>
                                                        <%#Eval("NAME")%>
                                                        <%-- <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' Text='<%#Eval("NAME")%>'/>--%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leaveapp"
                                    CssClass="btn btn-primary" TabIndex="14" ToolTip="Click here to Submit" OnClick="btnSave_Click" />
                                <%--OnClick="btnSave_Click"--%>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    CssClass="btn btn-warning" TabIndex="15" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                <asp:Button ID="btnBack" runat="server" CausesValidation="false" Visible="false"
                                    Text="Back" CssClass="btn btn-primary" TabIndex="16" ToolTip="Click here to Go back to Previous Menu" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leaveapp"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>


