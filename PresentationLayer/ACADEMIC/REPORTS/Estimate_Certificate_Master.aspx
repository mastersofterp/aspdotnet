<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Estimate_Certificate_Master.aspx.cs" Inherits="ACADEMIC_REPORTS_Estimate_Certificate_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .dataTables_scrollHeadInner {
            width: max-content!important;
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="Label1" runat="server">ESTIMATE CERTIFICATE</asp:Label></h3>
                </div>
                <div class="nav-tabs-custom col-12">
                    <ul class="nav nav-tabs mt-2" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link active" data-toggle="tab" href="#tabLC" tabindex="1">Manage Head</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-toggle="tab" href="#tabBC" tabindex="2">Generate Certificate</a>
                        </li>
                    </ul>

                    <div class="tab-content" id="my-tab-content">
                        <div class="tab-pane active" id="tabLC">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updpnlExam2"
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
                            <div class="box-body">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-12">
                                                        <div class="box box-primary">
                                                            <div id="div4" runat="server"></div>
                                                            <div class="box-body">
                                                                <div class="col-12">
                                                                    <div class="row">
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <label>Academic Year</label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddlAcadYear" runat="server" AppendDataBoundItems="True" AutoPostBack="True" ToolTip="Please Select Admission Year" CssClass="form-control" data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddlAcadYear_SelectedIndexChanged1">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAcadYear"
                                            Display="None" ErrorMessage="Please Select Academic Year" InitialValue="0"
                                            SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAcadYear"
                                            Display="None" ErrorMessage="Please Select Academic Year" InitialValue="0"
                                            SetFocusOnError="True" ValidationGroup="Confirm"></asp:RequiredFieldValidator>--%>
                                                                        </div>

                                                                        <%--  ADDED BY POOJA ON DATE 11-08-2023--%>
                                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="dvdegree" runat="server" visible="false">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <label>Degree</label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddldegreerc" runat="server" AppendDataBoundItems="True" AutoPostBack="True" ToolTip="Please Select Degree" CssClass="form-control" data-select2-enable="true" TabIndex="2" OnSelectedIndexChanged="ddldegreerc_SelectedIndexChanged">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>

                                                                        </div>
                                                                        <%--  ADDED BY POOJA ON DATE 11-08-2023--%>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <label>Approximate Expenditure for </label>
                                                                            </div>
                                                                            <div class="form-group col-lg-12 col-md-6 col-12">
                                                                                <asp:RadioButtonList ID="rbdExpenditure" runat="server" RepeatDirection="VERTICAL" AutoPostBack="true" OnSelectedIndexChanged="rbdExpenditure_SelectedIndexChanged">
                                                                                    <asp:ListItem Value="A">Institute </asp:ListItem>
                                                                                    <asp:ListItem Value="H">Hostel</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divgender" runat="server" visible="false">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <label>Gender</label>
                                                                            </div>
                                                                            <div class="form-group col-lg-12 col-md-6 col-12">
                                                                                <asp:RadioButtonList ID="rbdstudentGender" runat="server" RepeatDirection="VERTICAL" AutoPostBack="true" OnSelectedIndexChanged="rbdstudentGender_SelectedIndexChanged">
                                                                                    <asp:ListItem Value="M">Male</asp:ListItem>
                                                                                    <asp:ListItem Value="F">Female</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-12 btn-footer">
                                                                        <%--<asp:Button ID="btnShow" runat="server" TabIndex="6" Text="SHOW" CssClass="btn btn-primary"  OnClick="btnShow_Click" ValidationGroup="Confirm" />--%>
                                                                        <asp:Button ID="btnConfirmHead" runat="server" Text="Submit" TabIndex="3" CssClass="btn btn-primary" OnClick="btnConfirmHead_Click" ValidationGroup="Add" />
                                                                        <asp:Button ID="btnCancelHead" runat="server" TabIndex="4" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancelHead_Click" />
                                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Confirm" />
                                                                    </div>
                                                                    <div class="col-12">
                                                                        <asp:Panel ID="Panelgrid" runat="server" Visible="false">
                                                                            <div class="col-md-12 mt-3 pl-0 pr-0">
                                                                                <div class="table table-responsive">
                                                                                    <asp:Panel ID="pnlStudInstitute" runat="server" Visible="false">
                                                                                        <p style="color: red">Default admission fees head not displayed here,but it will display on estimate certificate.</p>
                                                                                        <div class="label-dynamic">
                                                                                            <label>Approximate Expenditure for Institute</label>
                                                                                        </div>
                                                                                        <asp:GridView ID="grdInstitute" runat="server" AutoGenerateColumns="False"
                                                                                            ShowFooter="True" CssClass="table table-hovered table-bordered" OnRowCommand="grdInstitute_RowCommand">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="Sr.No.">
                                                                                                    <ItemTemplate>
                                                                                                        <%# Container.DataItemIndex + 1 %>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Particular">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txtHead" runat="server" Text='<%# Bind("Particular") %>' class="form-control"></asp:TextBox>
                                                                                                        <%-- <ajaxToolKit:FilteredTextBoxExtender ID="fteValue1year" runat="server"
                                                                    FilterType="Custom" FilterMode="ValidChars" ValidChars="[a-zA-Z ]*$" TargetControlID="txtHead">
                                                                </ajaxToolKit:FilteredTextBoxExtender>--%>
                                                                                                        <asp:RequiredFieldValidator ID="fteValueH" runat="server" ErrorMessage="Please Enter Particular ."
                                                                                                            ControlToValidate="txtHead" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="For 1st Year">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txt1styear" runat="server" MaxLength="10" Text='<%# Bind("1st_Year") %>' class="form-control" placeholder="0.00"></asp:TextBox>
                                                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteValue1year" runat="server"
                                                                                                            FilterType="Custom" FilterMode="ValidChars" ValidChars="0.123456789" TargetControlID="txt1styear">
                                                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                                                        <asp:RequiredFieldValidator ID="fteValue3" runat="server" ErrorMessage="Please Enter 1st Year Amount."
                                                                                                            ControlToValidate="txt1styear" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="For 2nd Year">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txt2ndyear" runat="server" MaxLength="10" Text='<%# Bind("2nd_Year") %>' class="form-control" placeholder="0.00"></asp:TextBox>
                                                                                                        <%--asp:HiddenField ID="hfPARNO" runat="server" Value='<%#Bind("PARNO")%>'/>--%>
                                                                                                        <%--<asp:Label ID="lblParno" runat="server" Visible="false" Text='<%#Bind("PARNO")%>'></asp:Label>--%>
                                                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteValue2year" runat="server"
                                                                                                            FilterType="Custom" FilterMode="ValidChars" ValidChars="0.123456789" TargetControlID="txt2ndyear">
                                                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                                                        <asp:RequiredFieldValidator ID="fteValue2" runat="server" ErrorMessage="Please Enter 2nd Year Amount."
                                                                                                            ControlToValidate="txt2ndyear" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="For 3rd Year">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txt3rdyear" runat="server" MaxLength="10" Text='<%# Bind("3rd_Year") %>' class="form-control" placeholder="0.00">
                                                                                                        </asp:TextBox>
                                                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteValue3year" runat="server"
                                                                                                            FilterType="Custom" FilterMode="ValidChars" ValidChars="0.123456789" TargetControlID="txt3rdyear">
                                                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                                                        <asp:RequiredFieldValidator ID="fteValue1" runat="server" ErrorMessage="Please Enter 3rd Year Amount."
                                                                                                            ControlToValidate="txt3rdyear" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="For 4th Year">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txt4thyear" runat="server" MaxLength="10" Text='<%# Bind("4th_Year") %>' class="form-control" placeholder="0.00">
                                                                                                        </asp:TextBox>
                                                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteValue4year" runat="server"
                                                                                                            FilterType="Custom" FilterMode="ValidChars" ValidChars="0.123456789" TargetControlID="txt4thyear">
                                                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter 4th Year Amount."
                                                                                                            ControlToValidate="txt4thyear" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                    </EditItemTemplate>
                                                                                                    <ControlStyle Width="200px"></ControlStyle>
                                                                                                    <ItemStyle Width="200px"></ItemStyle>

                                                                                                    <FooterStyle />
                                                                                                    <FooterTemplate>
                                                                                                        <asp:Button ID="btnAdd" runat="server" ValidationGroup="Add" OnClick="btnAdd_Click"
                                                                                                            Text="Add Particular " CssClass="btn btn-primary" />
                                                                                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Add" />
                                                                                                    </FooterTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Remove">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:ImageButton ID="lnkRemoveInstitute" runat="server" ImageUrl="~/IMAGES/delete.png" OnClientClick="return confirm('Are you sure you want to delete this Particular?');" AlternateText="Remove Row" CommandName="Del" CommandArgument='<%#Bind("PARNO")%>' />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                    </EditItemTemplate>
                                                                                                    <ControlStyle Width="20px"></ControlStyle>
                                                                                                    <ItemStyle Width="50px"></ItemStyle>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                            <HeaderStyle CssClass="bg-light-blue" />
                                                                                        </asp:GridView>
                                                                                    </asp:Panel>
                                                                                    <asp:Panel ID="pnlStudHostel" runat="server" Visible="false">
                                                                                        <div class="label-dynamic">
                                                                                            <label>Approximate Expenditure for Hostel</label>
                                                                                        </div>
                                                                                        <asp:GridView ID="grdHostel" runat="server" AutoGenerateColumns="False"
                                                                                            ShowFooter="True" CssClass="table table-hovered table-bordered" OnRowCommand="grdHostel_RowCommand">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="Sr.No.">
                                                                                                    <ItemTemplate>
                                                                                                        <%# Container.DataItemIndex + 1 %>
                                                                                                        <%-- <asp:HiddenField runat="server" ID="hdnprnnoh" CommandArgument='<%# Eval("PRNO") %>' Text='<%# Bind("PRNO") %>' />--%>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Particular">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txthHead" runat="server" Text='<%# Bind("Particular") %>' class="form-control"></asp:TextBox>
                                                                                                        <%-- <ajaxToolKit:FilteredTextBoxExtender ID="fteValue1year" runat="server"
                                                                    FilterType="Custom" FilterMode="ValidChars" ValidChars="0.123456789" TargetControlID="txtHead">
                                                                </ajaxToolKit:FilteredTextBoxExtender>--%>
                                                                                                        <asp:RequiredFieldValidator ID="fteValueH" runat="server" ErrorMessage="Please Enter Particular."
                                                                                                            ControlToValidate="txthHead" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="For 1st Year">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txth1styear" runat="server" MaxLength="10" Text='<%# Bind("1st_Year") %>' class="form-control" placeholder="0.00"></asp:TextBox>
                                                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteValue1year" runat="server"
                                                                                                            FilterType="Custom" FilterMode="ValidChars" ValidChars="0.123456789" TargetControlID="txth1styear">
                                                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                                                        <asp:RequiredFieldValidator ID="fteValue3" runat="server" ErrorMessage="Please Enter 1st Year Amount."
                                                                                                            ControlToValidate="txth1styear" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="For 2nd Year">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txth2ndyear" runat="server" MaxLength="10" Text='<%# Bind("2nd_Year") %>' class="form-control" placeholder="0.00"></asp:TextBox>
                                                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteValue2year" runat="server"
                                                                                                            FilterType="Custom" FilterMode="ValidChars" ValidChars="0.123456789" TargetControlID="txth2ndyear">
                                                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                                                        <asp:RequiredFieldValidator ID="fteValue2" runat="server" ErrorMessage="Please Enter 2nd Year Amount."
                                                                                                            ControlToValidate="txth2ndyear" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="For 3rd Year">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txth3rdyear" runat="server" MaxLength="10" Text='<%# Bind("3rd_Year") %>' class="form-control" placeholder="0.00">
                                                                                                        </asp:TextBox>
                                                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteValue3year" runat="server"
                                                                                                            FilterType="Custom" FilterMode="ValidChars" ValidChars="0.123456789" TargetControlID="txth3rdyear">
                                                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                                                        <asp:RequiredFieldValidator ID="fteValue1" runat="server" ErrorMessage="Please Enter 3rd Year Amount."
                                                                                                            ControlToValidate="txth3rdyear" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="For 4th Year">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txth4thyear" runat="server" MaxLength="10" Text='<%# Bind("4th_Year") %>' class="form-control" placeholder="0.00">
                                                                                                        </asp:TextBox>
                                                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteValue4year" runat="server"
                                                                                                            FilterType="Custom" FilterMode="ValidChars" ValidChars="0.123456789" TargetControlID="txth4thyear">
                                                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter 4th Year Amount."
                                                                                                            ControlToValidate="txth4thyear" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                    </EditItemTemplate>
                                                                                                    <ControlStyle Width="200px"></ControlStyle>
                                                                                                    <ItemStyle Width="200px"></ItemStyle>

                                                                                                    <FooterStyle />
                                                                                                    <FooterTemplate>
                                                                                                        <asp:Button ID="btnhAdd" runat="server" OnClick="btnhAdd_Click"
                                                                                                            Text="Add Particular " CssClass="btn btn-primary" ValidationGroup="Add" />
                                                                                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Add" />
                                                                                                    </FooterTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Remove">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:ImageButton ID="lnkRemoveHostel" runat="server"
                                                                                                            ImageUrl="~/IMAGES/delete.png" AlternateText="Remove Row" CommandName="Del" OnClientClick="return confirm('Are you sure you want to delete this Particular?');" CommandArgument='<%# Bind("PARNO") %>'></asp:ImageButton>
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                    </EditItemTemplate>
                                                                                                    <ControlStyle Width="20px"></ControlStyle>
                                                                                                    <ItemStyle Width="50px"></ItemStyle>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                            <HeaderStyle CssClass="bg-light-blue" />
                                                                                        </asp:GridView>
                                                                                    </asp:Panel>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>

                                                                        <asp:Panel ID="Panelgrid_mca" runat="server" Visible="false">
                                                                            <div class="col-md-12 mt-3 pl-0 pr-0">
                                                                                <div class="table table-responsive">
                                                                                    <asp:Panel ID="pnlStudInstitute_mca" runat="server" Visible="false">
                                                                                        <p style="color: red">Default admission fees head not displayed here,but it will display on estimate certificate.</p>
                                                                                        <div class="label-dynamic">
                                                                                            <label>Approximate Expenditure for Institute</label>
                                                                                        </div>
                                                                                        <asp:GridView ID="grdInstitute_mca" runat="server" AutoGenerateColumns="False"
                                                                                            ShowFooter="True" CssClass="table table-hovered table-bordered" OnRowCommand="grdInstitute_mca_RowCommand">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="Sr.No.">
                                                                                                    <ItemTemplate>
                                                                                                        <%# Container.DataItemIndex + 1 %>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Particular">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txtHead_mca" runat="server" Text='<%# Bind("Particular") %>' class="form-control"></asp:TextBox>
                                                                                                        <%-- <ajaxToolKit:FilteredTextBoxExtender ID="fteValue1year" runat="server"
                                                                    FilterType="Custom" FilterMode="ValidChars" ValidChars="[a-zA-Z ]*$" TargetControlID="txtHead">
                                                                </ajaxToolKit:FilteredTextBoxExtender>--%>
                                                                                                        <asp:RequiredFieldValidator ID="fteValueH" runat="server" ErrorMessage="Please Enter Particular ."
                                                                                                            ControlToValidate="txtHead_mca" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="For 1st Year">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txt1styearmca" runat="server" MaxLength="10" Text='<%# Bind("1st_Year") %>' class="form-control" placeholder="0.00"></asp:TextBox>
                                                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteValue1year" runat="server"
                                                                                                            FilterType="Custom" FilterMode="ValidChars" ValidChars="0.123456789" TargetControlID="txt1styearmca">
                                                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                                                        <asp:RequiredFieldValidator ID="fteValue3" runat="server" ErrorMessage="Please Enter 1st Year Amount."
                                                                                                            ControlToValidate="txt1styearmca" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="For 2nd Year">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txt2ndyearmca" runat="server" MaxLength="10" Text='<%# Bind("2nd_Year") %>' class="form-control" placeholder="0.00"></asp:TextBox>
                                                                                                        <%--asp:HiddenField ID="hfPARNO" runat="server" Value='<%#Bind("PARNO")%>'/>--%>
                                                                                                        <%--<asp:Label ID="lblParno" runat="server" Visible="false" Text='<%#Bind("PARNO")%>'></asp:Label>--%>
                                                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteValue2year" runat="server"
                                                                                                            FilterType="Custom" FilterMode="ValidChars" ValidChars="0.123456789" TargetControlID="txt2ndyearmca">
                                                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                                                        <asp:RequiredFieldValidator ID="fteValue2" runat="server" ErrorMessage="Please Enter 2nd Year Amount."
                                                                                                            ControlToValidate="txt2ndyearmca" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                    </EditItemTemplate>
                                                                                                    <ControlStyle Width="200px"></ControlStyle>
                                                                                                    <ItemStyle Width="200px"></ItemStyle>

                                                                                                    <FooterStyle />
                                                                                                    <FooterTemplate>
                                                                                                        <asp:Button ID="btnAddmca" runat="server" ValidationGroup="Add" OnClick="btnAddmca_Click"
                                                                                                            Text="Add Particular " CssClass="btn btn-primary" />
                                                                                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Add" />
                                                                                                    </FooterTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Remove">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:ImageButton ID="lnkRemoveInstitute" runat="server" ImageUrl="~/IMAGES/delete.png" OnClientClick="return confirm('Are you sure you want to delete this Particular?');" AlternateText="Remove Row" CommandName="Del" CommandArgument='<%#Bind("PARNO")%>' />
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                    </EditItemTemplate>
                                                                                                    <ControlStyle Width="20px"></ControlStyle>
                                                                                                    <ItemStyle Width="50px"></ItemStyle>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                            <HeaderStyle CssClass="bg-light-blue" />
                                                                                        </asp:GridView>
                                                                                    </asp:Panel>

                                                                                    <asp:Panel ID="pnlStudHostel_mca" runat="server" Visible="false">
                                                                                        <div class="label-dynamic">
                                                                                            <label>Approximate Expenditure for Hostel MCA</label>
                                                                                        </div>
                                                                                        <asp:GridView ID="grdHostel_mca" runat="server" AutoGenerateColumns="False"
                                                                                            ShowFooter="True" CssClass="table table-hovered table-bordered" OnRowCommand="grdHostel_mca_RowCommand">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="Sr.No.">
                                                                                                    <ItemTemplate>
                                                                                                        <%# Container.DataItemIndex + 1 %>
                                                                                                        <%-- <asp:HiddenField runat="server" ID="hdnprnnoh" CommandArgument='<%# Eval("PRNO") %>' Text='<%# Bind("PRNO") %>' />--%>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Particular">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txthHead_hostelmca" runat="server" Text='<%# Bind("Particular") %>' class="form-control"></asp:TextBox>
                                                                                                        <%-- <ajaxToolKit:FilteredTextBoxExtender ID="fteValue1year" runat="server"
                                                                    FilterType="Custom" FilterMode="ValidChars" ValidChars="0.123456789" TargetControlID="txtHead">
                                                                </ajaxToolKit:FilteredTextBoxExtender>--%>
                                                                                                        <asp:RequiredFieldValidator ID="fteValueH" runat="server" ErrorMessage="Please Enter Particular."
                                                                                                            ControlToValidate="txthHead_hostelmca" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="For 1st Year">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txth1styear_mca" runat="server" MaxLength="10" Text='<%# Bind("1st_Year") %>' class="form-control" placeholder="0.00"></asp:TextBox>
                                                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteValue1year" runat="server"
                                                                                                            FilterType="Custom" FilterMode="ValidChars" ValidChars="0.123456789" TargetControlID="txth1styear_mca">
                                                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                                                        <asp:RequiredFieldValidator ID="fteValue3" runat="server" ErrorMessage="Please Enter 1st Year Amount."
                                                                                                            ControlToValidate="txth1styear_mca" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="For 2nd Year">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txth2ndyear_mca" runat="server" MaxLength="10" Text='<%# Bind("2nd_Year") %>' class="form-control" placeholder="0.00"></asp:TextBox>
                                                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteValue2year" runat="server"
                                                                                                            FilterType="Custom" FilterMode="ValidChars" ValidChars="0.123456789" TargetControlID="txth2ndyear_mca">
                                                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                                                        <asp:RequiredFieldValidator ID="fteValue2" runat="server" ErrorMessage="Please Enter 2nd Year Amount."
                                                                                                            ControlToValidate="txth2ndyear_mca" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                    </EditItemTemplate>
                                                                                                    <ControlStyle Width="200px"></ControlStyle>
                                                                                                    <ItemStyle Width="200px"></ItemStyle>

                                                                                                    <FooterStyle />
                                                                                                    <FooterTemplate>
                                                                                                        <asp:Button ID="btnhAdd_mca" runat="server" OnClick="btnhAdd_mca_Click"
                                                                                                            Text="Add Particular " CssClass="btn btn-primary" ValidationGroup="Add" />
                                                                                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Add" />
                                                                                                    </FooterTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Remove">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:ImageButton ID="lnkRemoveHostel" runat="server"
                                                                                                            ImageUrl="~/IMAGES/delete.png" AlternateText="Remove Row" CommandName="Del" OnClientClick="return confirm('Are you sure you want to delete this Particular?');" CommandArgument='<%# Bind("PARNO") %>'></asp:ImageButton>
                                                                                                    </ItemTemplate>
                                                                                                    <EditItemTemplate>
                                                                                                    </EditItemTemplate>
                                                                                                    <ControlStyle Width="20px"></ControlStyle>
                                                                                                    <ItemStyle Width="50px"></ItemStyle>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                            <HeaderStyle CssClass="bg-light-blue" />
                                                                                        </asp:GridView>
                                                                                    </asp:Panel>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnConfirmHead" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="tab-pane" id="tabBC">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updpnlExam2"
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
                            <div class="box-body">
                                <asp:UpdatePanel ID="updpnlExam2" runat="server">
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="updSession" runat="server">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-12">
                                                        <div class="box box-primary">
                                                            <div id="div1" runat="server"></div>
                                                            <div class="box-body">
                                                                <div class="col-12">
                                                                    <div class="row">
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <label>Academic Year</label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddlAdmyear" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Admission Year" CssClass="form-control" data-select2-enable="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmyear_SelectedIndexChanged">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvAdmyear" runat="server" ControlToValidate="ddlAdmyear"
                                                                                Display="None" ErrorMessage="Please Select Academic Year" InitialValue="0"
                                                                                SetFocusOnError="True" ValidationGroup="Show1"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <label>Degree</label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="True" ToolTip="Please Select Degree" CssClass="form-control" data-select2-enable="true" TabIndex="2"
                                                                                OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <label>Branch</label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Branch" CssClass="form-control" data-select2-enable="true" TabIndex="3" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">

                                                                                <label>Semester</label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" CssClass="form-control" ToolTip="Please Select Semester" data-select2-enable="true" TabIndex="4" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div id="DivBank" runat="server" visible="false">
                                                                    <%--RFC.Enhancement.Major.2 (01-09-2023)--%>
                                                                    <div class="row">
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <label>Bank </label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddlBank" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Bank" CssClass="form-control" Visible="false" data-select2-enable="true" TabIndex="5">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmyear"
                                                                                Display="None" ErrorMessage="Please Select Academic Year" InitialValue="0"
                                                                                SetFocusOnError="True" ValidationGroup="Show1"></asp:RequiredFieldValidator>

                                                                        </div>
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup>*</sup>
                                                                                <label>Type of Account</label>
                                                                                <asp:TextBox ID="txttypeofAC" runat="server" Visible="false" TabIndex="6" CssClass="form-control"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txttypeofAC"
                                                                                    Display="None" ErrorMessage="Please Enter Type of Account"
                                                                                    SetFocusOnError="True" ValidationGroup="Show1"></asp:RequiredFieldValidator>

                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <label>Account No.</label>
                                                                            </div>
                                                                            <asp:TextBox ID="txtACNO" runat="server" Visible="false" CssClass="form-control" TabIndex="7" MaxLength="24"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtACNO"
                                                                                Display="None" ErrorMessage="Please Enter Account No."
                                                                                SetFocusOnError="True" ValidationGroup="Show1"></asp:RequiredFieldValidator>
                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender38" runat="server"
                                                                                TargetControlID="txtACNO" FilterType="Numbers" />
                                                                        </div>
                                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <label>IFSC Code</label>
                                                                            </div>
                                                                            <asp:TextBox ID="txtIFSC" runat="server" MaxLength="24" Style="text-transform: uppercase" CssClass="form-control" Visible="false" TabIndex="8"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtIFSC"
                                                                                Display="None" ErrorMessage="Please Enter IFSC Code"
                                                                                SetFocusOnError="True" ValidationGroup="Show1"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-12 btn-footer">
                                                                    <asp:Button ID="btnShowData1" runat="server" Text="Show Students" ValidationGroup="Show1" CssClass="btn btn-primary" TabIndex="9" OnClick="btnShowData1_Click" />
                                                                    <asp:Button ID="btnConfirm" runat="server" Text="Confirm" TabIndex="10" CssClass="btn btn-primary" ValidationGroup="Show1" OnClick="btnConfirm_Click" />
                                                                    <asp:Button ID="btnPrint" runat="server" TabIndex="11" Text="Print Report" CssClass="btn btn-info" OnClick="btnPrint_Click" Enabled="false" />
                                                                    <asp:Button ID="btnCancel_LC" runat="server" TabIndex="12" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_LC_Click" />
                                                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show1" />
                                                                </div>
                                                            </div>
                                                            <div class="col-12">
                                                                <asp:Panel ID="Panel3" runat="server">
                                                                    <asp:ListView ID="lvStudentRecords" runat="server" EnableModelValidation="True">
                                                                        <EmptyDataTemplate>
                                                                            <%-- <div>
                                                                                        -- No Student Record Found --
                                                                                    </div>--%>
                                                                        </EmptyDataTemplate>
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Search Results</h5>
                                                                            </div>
                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th>
                                                                                            <%-- <asp:CheckBox ID="chkIdentityCard" runat="server" onClick="totAll(this);"
                                                                                                        ToolTip="Select or Deselect All Records" />--%>
                                                                                        </th>
                                                                                        <%-- <th>Registartion No..
                                                            </th>--%>
                                                                                        <th>
                                                                                            <asp:Label ID="lblDYRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                                        </th>
                                                                                        <th>Student Name
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblDYtxtYear" runat="server" Font-Bold="true"></asp:Label>
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblDYtxtBatch" runat="server" Font-Bold="true"></asp:Label>
                                                                                        </th>
                                                                                        <th>Remark
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
                                                                                    <asp:CheckBox ID="chkReport" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                                                    <asp:HiddenField ID="hidIdNo" runat="server"
                                                                                        Value='<%# Eval("IDNO") %>' />
                                                                                </td>
                                                                                <%--<td>
                                                        <%# Eval("ENROLLNO")%>
                                                    </td>--%>
                                                                                <td>
                                                                                    <%# Eval("REGNO")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("STUDNAME")%>
                                                                                </td>

                                                                                <td>
                                                                                    <%# Eval("CODE")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("SHORTNAME")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("YEARNAME")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("SEMESTERNAME")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("SHORTNAME")%>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtRemark" runat="server"></asp:TextBox>
                                                                                </td>
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server" />
    <script language="javascript" type="text/javascript">
        function totAll(headchk) {
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
</asp:Content>

