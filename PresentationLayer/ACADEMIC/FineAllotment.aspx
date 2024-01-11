<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="FineAllotment.aspx.cs" Inherits="ACADEMIC_FineAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updFine"
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

    <asp:UpdatePanel ID="updFine" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Fine Allotment</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlSingleStud" runat="server" Visible="true">
                                <div class="col-12" id="pnlSearch" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Enter Reg. No</label>
                                            </div>
                                            <asp:TextBox ID="txtEnrollno" runat="server" CssClass="form-control" ToolTip="Enter text to search." TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtEnrollno"
                                                Display="None" ErrorMessage="Please Enter Reg.No." SetFocusOnError="true"
                                                ValidationGroup="search" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Semester</label>--%>
                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSemeseter" runat="server"
                                                ControlToValidate="ddlSemester" InitialValue="0" Display="None" ErrorMessage="Please Select Semester." ValidationGroup="search"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Receipt Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlReceipt" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvReceipt" runat="server" ValidationGroup="search"
                                                Display="None" ControlToValidate="ddlReceipt" ErrorMessage="Please Select Receipt Type." InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="search" />
                                            <asp:Button ID="btnSearch" runat="server" Text="Show" OnClick="btnSearch_Click" TabIndex="4" ValidationGroup="search" CssClass="btn btn-primary" />
                                        </div>
                                    </div>
                                </div>

                                <div id="divCourses" runat="server" visible="false">
                                    <div class="col-12" id="divStudInfo" runat="server">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>
                                                        <asp:Label ID="lblDYtxtEnrollmentNo" runat="server" Font-Bold="true"></asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblEnrollNo" Font-Bold="true" runat="server" />
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Student's Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblName" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>
                                                        <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblAdmBatch" Font-Bold="true" runat="server" />
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>
                                                        <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                        /
                                                        <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblBranch" Font-Bold="true" runat="server" />
                                                            <asp:Label ID="lblDegreeno" Font-Bold="true" Visible="false" runat="server"></asp:Label>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>
                                                        <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSemester" Font-Bold="true" runat="server" />
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>
                                                        <asp:Label ID="lblDYddlScheme" runat="server" Font-Bold="true"></asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblScheme" runat="server" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>
                                                        <asp:Label ID="lblDYtxtSchoolname" runat="server" Font-Bold="true"></asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSingCollege" runat="server" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>
                                                        <asp:Label ID="lblSession" runat="server" Font-Bold="true" Text="Session"></asp:Label>
                                                        :
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSessionName" runat="server" Font-Bold="true"></asp:Label>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>

                                    <div runat="server" id="noDuesSingleStud">
                                        <div class="col-12 d-none">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Scholorship Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlScholorshipType" runat="server" AppendDataBoundItems="True" CssClass="form-control">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>No Dues Status</label>
                                                    </div>
                                                    <asp:RadioButton ID="rdoYesSingle" runat="server" GroupName="Sex" TabIndex="10" Text="Complete" Style="color: green"></asp:RadioButton>
                                                    <asp:RadioButton ID="rdoNoSingle" runat="server" GroupName="Sex" TabIndex="11" Text="Pending" Style="color: red"></asp:RadioButton>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label></label>
                                                    </div>
                                                    <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" />&nbsp;
                                                    <asp:Label ID="lblMotherName" runat="server" Font-Bold="False" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Photo</label>
                                                    </div>
                                                    <asp:Image ID="imgPhoto" runat="server" Width="50%" Height="80%" />
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-12">
                                            <asp:ListView ID="lvFeeItems" runat="server">
                                                <LayoutTemplate>
                                                    <div id="divlvFeeItems">
                                                        <div class="sub-heading">
                                                            <h5>Available Fee Items</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblFeeItems">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Sr. No.
                                                                    </th>
                                                                    <th>Fee Heads
                                                                    </th>
                                                                    <th>Currency
                                                                    </th>
                                                                    <th>Amount
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
                                                            <asp:Label ID="lblFeeHeadSrNo" runat="server" Text='<%# Eval("SRNO") %>' Visible="false" />
                                                            <%# Container.DataItemIndex + 1%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("FEE_LONGNAME")%>
                                                            <asp:HiddenField ID="hdnfld_FEE_LONGNAME" runat="server" Value='<%# Eval("FEE_LONGNAME")%>' />
                                                        </td>
                                                        <td>
                                                            <%# Eval("CURRENCY")%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFeeItemAmount" Text='<%# Eval("AMOUNT") %>' onblur="UpdateTotalAndBalance();"
                                                                Style="text-align: right" runat="server" CssClass="form-control"
                                                                TabIndex="15" AutoComplete="off" MaxLength="7" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" ValidChars="0123456789."
                                                                FilterMode="ValidChars" TargetControlID="txtFeeItemAmount">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:HiddenField ID="hidFeeItemAmount" runat="server" Value='<%# Eval("AMOUNT") %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="backsem"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="SingleSubmit" />
                                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" OnClick="btnCancel_Click" Text="Cancel" />

                                        </div>
                                    </div>

                                </div>
                            </asp:Panel>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%-- <asp:PostBackTrigger ControlID="lvStudentRecords" />--%>
            <%-- <asp:PostBackTrigger ControlID="btnShow" />--%>
        </Triggers>
        <%--<Triggers>   
            <asp:AsyncPostBackTrigger ControlID="btnPrintReport" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
               <asp:PostBackTrigger ControlID="btnShow" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function showConfirm() {
            var ret = confirm('Do You Want To Really Remove Scholarship Adjustment!');
            if (ret == true)
                return true;
            else
                return false;
        }
    </script>

</asp:Content>

