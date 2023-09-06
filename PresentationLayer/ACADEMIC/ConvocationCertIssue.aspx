<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ConvocationCertIssue.aspx.cs" Inherits="ACADEMIC_ConvocationCertIssue"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">CONVOCATION CERTIFICATE ISSUE</h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
								<div class="label-dynamic">
									<label>Session</label>
								</div>
                                 <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="1"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
							</div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
								<div class="label-dynamic">
									<label>Convocation</label>
								</div>
                                 <asp:DropDownList ID="ddlConvocation" runat="server" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="2">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlConvocation"
                                    Display="None" ErrorMessage="Please Select Convocation" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
							</div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
								<div class="label-dynamic">
									<label>Certificate Name</label>
								</div>
                                <asp:DropDownList ID="ddlCertificateNo" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlCertificateNo_SelectedIndexChanged" data-select2-enable="true" TabIndex="3">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCertificateNo" runat="server" ControlToValidate="ddlCertificateNo"
                                    Display="None" ErrorMessage="Please Select Certificate" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
							</div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
								<div class="label-dynamic">
									<label>Degree</label>
								</div>
                                 <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" data-select2-enable="true" TabIndex="4">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
							</div>
                            <div class="form-group col-lg-3 col-md-6 col-12" id="trDept" runat="server" visible="false">
								<div class="label-dynamic">
									<label>Department</label>
								</div>
                                 <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="True" data-select2-enable="true" TabIndex="5">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                                    Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
							</div>
                            <div class="form-group col-lg-3 col-md-6 col-12" id="trBranch" runat="server">
								<div class="label-dynamic">
									<label>Branch</label>
								</div>
                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" data-select2-enable="true" TabIndex="6">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                    Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
							</div>
                            <div class="form-group col-lg-3 col-md-6 col-12" id="trScheme" runat="server">
								<div class="label-dynamic">
									<label>Scheme</label>
								</div>
                                  <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" data-select2-enable="true" TabIndex="7">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="report"></asp:RequiredFieldValidator>
							</div>
                            <div class="form-group col-lg-3 col-md-6 col-12" id="trSem" runat="server">
								<div class="label-dynamic">
									<label>Semester</label>
								</div>
                                <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" data-select2-enable="true" TabIndex="8">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="report">
                                </asp:RequiredFieldValidator>
							</div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
								<div class="label-dynamic">
									<label>Regulation Date</label>
								</div>
                                <asp:TextBox ID="lblRegulationDate" runat="server" TabIndex="9"></asp:TextBox>
							</div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
								<div class="label-dynamic">
									<label>Convocation Date</label>
								</div>
                                <div class="input-group">
                                    <div class="input-group-addon" id="imgEDate">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtConvocationDate" runat="server" ValidationGroup="submit" TabIndex="10"/>
                                    <%-- <asp:Image ID="imgEDate" runat="server" AlternateText="Select Date" ImageUrl="~/images/calendar.png"
                            Style="cursor: pointer" ToolTip="Select Date" />--%>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                        Format="dd/MM/yyyy" PopupButtonID="imgEDate" TargetControlID="txtConvocationDate" />
                                    <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ControlToValidate="txtConvocationDate"
                                        Display="None" ErrorMessage="Please Enter Convocation Date" SetFocusOnError="True"
                                        ValidationGroup="report" />
                                    <ajaxToolKit:MaskedEditExtender ID="meeEndDate" runat="server" AcceptNegative="Left"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" DisplayMoney="Left" Enabled="True" ErrorTooltipEnabled="True"
                                        Mask="99/99/9999" MaskType="Date" OnInvalidCssClass="errordate" TargetControlID="txtConvocationDate" />
                                    <ajaxToolKit:MaskedEditValidator ID="mevEndDate" runat="server" ControlExtender="meeEndDate"
                                        ControlToValidate="txtConvocationDate" Display="None" EmptyValueBlurredText="Empty"
                                        EmptyValueMessage="Please Enter Convocation Date" ErrorMessage="mevEndDate" InvalidValueBlurredMessage="Invalid Date"
                                        InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="True"
                                        ValidationGroup="Show" />
                                </div>
							</div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShowData" runat="server" Text="Filter" ValidationGroup="report"
                            CssClass="btn btn-primary" OnClick="btnShowData_Click" TabIndex="11"/>
                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                            CssClass="btn btn-primary" ValidationGroup="report" TabIndex="12"/>
                        <asp:Button ID="btnReport" runat="server" Text="Certificate" CssClass="btn btn-primary" OnClick="btnReport_Click" TabIndex="13"/>
                        <asp:Button ID="btnStudReport" runat="server" Text="Student List" CssClass="btn btn-primary" OnClick="btnStudReport_Click" TabIndex="14"/>
                        <asp:Button ID="btnPassoutStudent" runat="server" Text="Passout Student List"
                            CssClass="btn btn-primary" OnClick="btnPassoutStudent_Click" TabIndex="15"/>
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="report" TabIndex="16"/>
                        <asp:Button ID="btnConvoReport" runat="server" Text="Convocation List" CssClass="btn btn-primary" OnClick="btnConvoReport_Click" TabIndex="17"/>
                        <asp:Button ID="btnConvReport" runat="server" Text="Without Photo List" CssClass="btn btn-primary" OnClick="btnConvReport_Click" TabIndex="18"/>
                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Clear"
                            CssClass="btn btn-warning" TabIndex="19"/>
                    </div>

                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-6 col-md-12 col-12">
                                <div class=" note-div">
                                    <h5 class="heading">Note (Please Select)</h5>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Passout Student List - <span style="color: green;font-weight:bold">Convocation->Certificate Name->Degree->Branch</span></span></p>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-12 col-12">
                                <div class="sub-heading">
	                                <h5>Convocation Report Type</h5>
                                </div>
                                <asp:RadioButtonList ID="rdoReportType" runat="server" RepeatDirection="Horizontal" TabIndex="20">
                                    <asp:ListItem Selected="True" Value="pdf">Adobe Reader&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="xls">MS-Excel&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="doc">MS-Word</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <asp:Panel ID="pnlConvocation" runat="server" Visible="false">
                            <asp:ListView ID="lvConvocationCertificate" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
	                                    <h5>Student Eligible for Issue Certificate</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width:100%;">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>
                                                    <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" ToolTip="Select All" />
                                                </th>
                                                <th>Reg No
                                                </th>
                                                <th>Student Name
                                                </th>
                                                <th>Degree
                                                </th>
                                                <th>Branch
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
                                            <asp:CheckBox ID="cbRow" runat="server" />
                                            <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("IDNO") %>' Visible="false" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRegno" runat="server" Text='<%# Eval("REGNO")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDNAME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREE")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BRANCH_SHORT")%>' ToolTip='<%# Eval("BRANCH_LONG")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                    <div id="divMsg" runat="server">
                    </div>
                </div>
            </div>            
        </div>
    </div>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
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
        });  
    </script>
</asp:Content>
