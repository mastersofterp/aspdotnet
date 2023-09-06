<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Manual_Fees_Entry.aspx.cs" Inherits="ACADEMIC_Manual_Fees_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                            <h3 class="box-title">Manual Fees Entry</h3>
                            <%--<h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>--%>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlSingleStud" runat="server" Visible="true">
                                <div class="col-12" id="pnlSearch" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Degree Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="1" CssClass="form-control" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">UG</asp:ListItem>
                                                <asp:ListItem Value="2">PG</asp:ListItem>
                                                <asp:ListItem Value="3">NRI</asp:ListItem>
                                                <asp:ListItem Value="4">B.TECH</asp:ListItem>
                                                <asp:ListItem Value="5">B.TECH LATERAL</asp:ListItem>
                                                <asp:ListItem Value="6">PHD</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ValidationGroup="search"
                                                Display="None" ControlToValidate="ddlDegree" ErrorMessage="Please Select Degree Type." InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Application ID</label>
                                            </div>
                                            <asp:TextBox ID="txtApplicationId" runat="server" CssClass="form-control" onkeyup="validateNumericAndNotZero(this);" MaxLength="12" ToolTip="Enter text to search." TabIndex="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtApplicationId"
                                                Display="None" ErrorMessage="Please Enter Application ID." SetFocusOnError="true"
                                                ValidationGroup="search" />
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="search" />
                                            <asp:Button ID="btnSearch" runat="server" Text="Show Details" OnClick="btnSearch_Click" TabIndex="3" ValidationGroup="search" CssClass="btn btn-primary" />
                                        </div>
                                    </div>
                                </div>

                                <div id="divCourses" runat="server" visible="false">
                                    <div class="sub-heading">
                                        <h5>Student Details</h5>
                                    </div>

                                    <div class="col-12" id="divStudInfo" runat="server">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Student's Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblName" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                    
                                                    <li class="list-group-item"><b>Student's Email :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblEmail" Font-Bold="true" runat="server" />
                                                        </a>
                                                    </li>

                                                    <li class="list-group-item"><b>Student's Gender :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblGender" Font-Bold="true" runat="server" />
                                                        </a>
                                                    </li>

                                                    <li class="list-group-item"><b>Applicable Fees :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblAppliedFees" Font-Bold="true" runat="server" />
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div class="col-lg-6 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Degree/Branch :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblDegreeName" Font-Bold="true" runat="server"></asp:Label>
                                                        </a>
                                                    </li>

                                                    <li class="list-group-item"><b>Student's Mobile Number :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblMobNo" Font-Bold="true" runat="server" />
                                                        </a>
                                                    </li>

                                                    <li class="list-group-item"><b>Student's Date Of Birth :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblDOB" Font-Bold="true" runat="server" />
                                                        </a>
                                                    </li>

                                                    <li class="list-group-item"><b>Student's Pay Status :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblPayStatus" Font-Bold="true" runat="server" />
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer mt-3">
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="backsem"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" Text="Collect Manual Fees" ValidationGroup="search" />
                                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" OnClick="btnCancel_Click" Text="Cancel" />

                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>--%>
    </asp:UpdatePanel>

    <script>
        function validateNumericAndNotZero(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return false;
            }
            if (txt.value == '0') {
                txt.value = '';
                alert('Please Enter Values Greater Than Zero!');
                txt.focus();
                return false;
            }

        }
    </script>
</asp:Content>

