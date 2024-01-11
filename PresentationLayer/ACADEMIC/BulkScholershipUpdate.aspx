<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BulkScholershipUpdate.aspx.cs" Inherits="ACADEMIC_BulkScholershipUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>

    <style>
        .sea-rch i {
            color: #5b5b5b;
            cursor: pointer;
        }

            .sea-rch i:hover {
                color: red;
            }
    </style>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updtime"
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

    <asp:UpdatePanel ID="updtime" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">

                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Search By</label>
                                        </div>
                                        <asp:RadioButton ID="rdoSingleStudent" runat="server" Text="Single Student" GroupName="search" Checked="true" AutoPostBack="true" OnCheckedChanged="rdoSingleStudent_CheckedChanged" />
                                        <asp:RadioButton ID="rdoBulkStudent" runat="server" Text="Bulk Student" GroupName="search" AutoPostBack="true" OnCheckedChanged="rdoBulkStudent_CheckedChanged" />
                                        <asp:RadioButton ID="rdoIdno" runat="server" Text="Id No." Checked="true" GroupName="search" Visible="false" />
                                    </div>
                                </div>
                            </div>

                            <asp:Panel ID="pnlSingleStud" runat="server" Visible="false">
                                <div id="pnlSearch" runat="server" class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                            <div class="label-dynamic">
                                                <label>Session Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSessionSingleStud" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Session"
                                                ControlToValidate="ddlSessionSingleStud" Display="None" ValidationGroup="search" InitialValue="0" />--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Enter </label>
                                                <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:TextBox ID="txtEnrollno" runat="server" CssClass="form-control" ToolTip="Enter text to search."></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtEnrollno"
                                                Display="None" ErrorMessage="Please enter Reg.No." SetFocusOnError="true"
                                                ValidationGroup="search" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="search" />
                                            <asp:Button ID="btnSearch" runat="server" OnClick="btnProceed_Click" Text="Show" ValidationGroup="search" CssClass="btn btn-primary" />
                                        </div>
                                    </div>
                                </div>

                                <div id="divCourses" runat="server" visible="false">
                                    <div runat="server" id="noDuesSingleStud" class="col-12">
                                        <div class="row mb-3">
                                            <div class="col-lg-5 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Student Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblName" runat="server" Font-Bold="false" />
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>
                                                        <asp:Label ID="lblDYlvEnrollmentNo" runat="server" Font-Bold="true"></asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="false"></asp:Label></a>
                                                    </li>
                                                    <li class="list-group-item"><b>
                                                        <asp:Label ID="lblDYlvAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="false"></asp:Label></a>
                                                    </li>

                                                    <li class="list-group-item"><b>
                                                        <asp:Label ID="Label1" Text="Year" runat="server" Font-Bold="true"></asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblYear" runat="server" Font-Bold="false"></asp:Label>
                                                        </a>
                                                    </li>

                                                </ul>
                                            </div>
                                            <div class="col-lg-7 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">

                                                    <li class="list-group-item"><b>
                                                        <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>-<asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblBranch" runat="server" Font-Bold="false"></asp:Label>
                                                            <asp:Label ID="lblDegrreno" runat="server" Font-Bold="false"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>
                                                        <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblScheme" runat="server" Font-Bold="false"></asp:Label></a>
                                                    </li>

                                                    <li class="list-group-item"><b>
                                                        <asp:Label ID="lblDYtxtSchoolname" runat="server" Font-Bold="true"></asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSingCollege" runat="server" Font-Bold="false"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item d-none"><b>
                                                        <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSemester" runat="server" Font-Bold="false"></asp:Label>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <%--<div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                   
                                                </ul>
                                            </div>--%>
                                        </div>

                                        <div class="row mt-3">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Scholorship Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlScholorshipType" runat="server" AppendDataBoundItems="True" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlScholorshipType_SelectedIndexChanged" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12 " id="divSchlWise" runat="server">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Scholarship Category</label>
                                                </div>

                                                <asp:DropDownList ID="ddlSchlWise" runat="server" AppendDataBoundItems="True" ToolTip="Please Select " CssClass="form-control" data-select2-enable="true" TabIndex="5" AutoPostBack="true" OnSelectedIndexChanged="ddlSchlWise_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">SemesterWise</asp:ListItem>
                                                    <asp:ListItem Value="2">YearWise</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divModeSingl" runat="server">
                                                <div class="label-dynamic">
                                                    <sup id="sup1" runat="server">* </sup>
                                                    <label>Scholarship Mode</label>
                                                </div>
                                                <div>
                                                    <asp:DropDownList ID="ddlSchlModeSingle" runat="server" TabIndex="32" AppendDataBoundItems="true"
                                                        CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Scholarship Mode" AutoPostBack="true" OnSelectedIndexChanged="ddlSchlModeSingle_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select</asp:ListItem>
                                                        <asp:ListItem Value="1">Percentage Wise</asp:ListItem>
                                                        <asp:ListItem Value="2">Amount Wise</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:RequiredFieldValidator ID="rfvddlSchlModeSingle" runat="server" ControlToValidate="ddlSchMode"
                                                        Display="None" ErrorMessage="Please Select Scholarship Mode" ValidationGroup="show" InitialValue="0"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divamtSingl" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup id="sup2" runat="server">*</sup>
                                                    <asp:Label runat="server" ID="Label2" Font-Bold="true">Enter Percentage</asp:Label>
                                                </div>

                                                <div>
                                                    <asp:TextBox runat="server" ID="txtPerAmountSingStud" TabIndex="27" onkeyup="validateNumericAndNotZero(this);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtPerAmountSingStud" runat="server" ControlToValidate="txtPerAmountSingStud"
                                                        Display="None" ErrorMessage="Please Enter Percentage" ValidationGroup="show"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>





                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                <div class="label-dynamic">
                                                    <label>No Dues Status</label>
                                                </div>
                                                <asp:RadioButton ID="rdoYesSingle" runat="server" GroupName="Sex" TabIndex="10" Text="Complete" Style="color: green"></asp:RadioButton>
                                                <asp:RadioButton ID="rdoNoSingle" runat="server" GroupName="Sex" TabIndex="11" Text="Pending" Style="color: red"></asp:RadioButton>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                <div class="label-dynamic">
                                                    <label></label>
                                                </div>
                                                <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" />&nbsp;
                                                <asp:Label ID="lblMotherName" runat="server" Font-Bold="False" />
                                            </div>


                                            <%--     <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Photo</label>
                                                </div>
                                                <asp:Image ID="imgPhoto" runat="server" Width="50%" Height="80%" />
                                            </div>--%>
                                            <div class="form-group col-lg-3 col-md-6 col-12 " id="divShowPerButton" runat="server">
                                                <div class="label-dynamic">
                                                    <label></label>
                                                </div>
                                                <asp:Button ID="btnShowSchMode" runat="server" OnClick="btnShowSchMode_Click" Text="Calculate" CssClass="btn btn-primary" />
                                            </div>


                                        </div>



                                        <div class="col-12">
                                            <asp:GridView ID="gvSemesters" runat="server" ShowFooter="false" Width="100%" class="table table-bordered" OnRowDataBound="gvSemesters_RowDataBound"
                                                AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SrNo." ControlStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSemestersNo" runat="server" Text='<%# Eval("SemesterNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#758aa8" ForeColor="#ffffff" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Year/Semester" ControlStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="13%" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSemesters" runat="server" Text='<%# Eval("RowNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#758aa8" ForeColor="#ffffff" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Academic Year" ControlStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAcadYear" runat="server" Text='<%# Eval("AcadYear") %>' Visible="false"  />
                                                            <asp:DropDownList ID="ddlAcadYear" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" >
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#758aa8" ForeColor="#ffffff" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Amount" ControlStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="17%" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtSemestersAmount" runat="server" MaxLength="10" onKeyUp="numericFilter(this);"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="rfvSemAmt" runat="server" InitialValue="" ControlToValidate="txtSemestersAmount"
                                                                Display="Dynamic" ErrorMessage="Please Enter Semester Amount">
                                                            </asp:RequiredFieldValidator>--%>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbe" runat="server"
                                                                TargetControlID="txtSemestersAmount" ValidChars="1234567890." />
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#758aa8" ForeColor="#ffffff" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ControlStyle-Font-Bold="true" HeaderText="Remove Scholarship Allotment" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDeleteAllotment" runat="server" AlternateText="Delete Allotment" ImageUrl="~/images/delete.png"
                                                                OnClick="btnDeleteAllotment_Click" OnClientClick="return showConfirmAllotment();" Visible="false" />
                                                            <asp:Label ID="lblTxt2" runat="server" Visible="true" Text="Scholarship Allotment not found"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#758aa8" ForeColor="#ffffff" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ControlStyle-Font-Bold="true" HeaderText="Remove Scholarship Adjustment" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Delete Adjustment" ImageUrl="~/images/delete.png"
                                                                OnClick="btnDelete_Click" OnClientClick="return showConfirm();" Visible="false" />
                                                            <asp:Label ID="lblTxt" runat="server" Visible="true" Text="Scholarship Adjustment not found"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle BackColor="#758aa8" ForeColor="#ffffff" />
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="backsem"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                            <asp:Button ID="btnSubmitForSingleStu" runat="server" CssClass="btn btn-primary" Font-Bold="true" OnClick="btnSubmitForSingleStu_Click" Text="Submit" ValidationGroup="SingleSubmit" />
                                            <asp:Button ID="btnCancel1" runat="server" CssClass="btn btn-warning" Font-Bold="true" OnClick="btnCancel1_Click" Text="Cancel" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlBulkStud" runat="server" Visible="false">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12 ">
                                            <div class="label-dynamic">
                                                <%--<sup>* </sup>--%>
                                                <%--<label>Admission Batch</label>--%>
                                                <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="True" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                                ToolTip="Please Select Session" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlAdmBatch"
                                                Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Academic Year</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAcdYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcdYear_SelectedIndexChanged" AppendDataBoundItems="true" TabIndex="6" ValidationGroup="show" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="rfvAcademicYear" runat="server" ControlToValidate="ddlAcdYear"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Academic Year" ValidationGroup="show">
                                            </asp:RequiredFieldValidator>--%>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <%--<label>Institute Name</label>--%>
                                                <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="True" TabIndex="2" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged"
                                                CssClass="form-control" data-select2-enable="true" AutoPostBack="True" ToolTip="Please Select Institute">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvColg" runat="server" ControlToValidate="ddlColg"
                                                Display="None" ErrorMessage="Please Select Institute" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <%--<label>Degree</label>--%>
                                                <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" ToolTip="Please Select Degree" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <%--<label>Branch</label>--%>
                                                <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" TabIndex="4" CssClass="form-control" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ToolTip="Please Select Branch" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12 " id="divSemester" runat="server">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <%-- <label>Semester</label>--%>

                                                <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Semester" CssClass="form-control" data-select2-enable="true" TabIndex="5" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                            <%-- <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 ">
                                            <div class="label-dynamic">
                                                <asp:Label ID="lblYearMandatory" runat="server" Style="color: red" Visible="false">*</asp:Label>
                                                <%-- <sup></sup>--%>
                                                <%--lblDYddlYear--%>
                                                <asp:Label ID="lblDYddlYear" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Year" CssClass="form-control" data-select2-enable="true" TabIndex="5" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                            <%--<asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                                                Display="None" ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSchltype" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Scholarship Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlScholarShipsType" runat="server" AutoPostBack="true" AppendDataBoundItems="True" CssClass="form-control" OnSelectedIndexChanged="ddlScholarShipsType_SelectedIndexChanged" data-select2-enable="true"
                                                ToolTip="Please Select ScholarShip Type">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="rfvddlScholarShipsType" runat="server" ControlToValidate="ddlScholarShipsType"
                                                Display="None" ErrorMessage="Please Select Scholarship Type " InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divschmode" runat="server">
                                            <div class="label-dynamic">
                                                <sup id="supschmode" runat="server">* </sup>
                                                <label>Scholarship Mode</label>
                                            </div>
                                            <div>
                                                <asp:DropDownList ID="ddlSchMode" runat="server" TabIndex="32" AppendDataBoundItems="true"
                                                    CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Scholarship Mode" AutoPostBack="true" OnSelectedIndexChanged="ddlSchMode_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please select</asp:ListItem>
                                                    <asp:ListItem Value="1">Percentage Wise</asp:ListItem>
                                                    <asp:ListItem Value="2">Amount Wise</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="rfvSchMode" runat="server" ControlToValidate="ddlSchMode"
                                                    Display="None" ErrorMessage="Please Select Scholarship Mode" ValidationGroup="show" InitialValue="0"
                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divAmt" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup id="supAmt" runat="server">*</sup>
                                                <asp:Label runat="server" ID="lblamt" Font-Bold="true">Enter Percentage</asp:Label>
                                            </div>

                                            <div>
                                                <asp:TextBox runat="server" ID="txtschAmt" TabIndex="27" onkeyup="validateNumericAndNotZero(this);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvschamt" runat="server" ControlToValidate="txtschAmt"
                                                    Display="None" ErrorMessage="Please Enter Percentage" ValidationGroup="show"
                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <label>Date of Issue</label>
                                            </div>
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox runat="server" ID="txtDateofissue" TabIndex="7" ToolTip="Please Enter Date"></asp:TextBox>
                                                <%-- <asp:Image ID="imgExamDate" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                                <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtDateofissue" PopupButtonID="imgExamDate" />
                                                <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" TargetControlID="txtDateofissue"
                                                    Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                    MaskType="Date" />
                                                <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" EmptyValueMessage="Please Enter Date of Issue"
                                                    ControlExtender="meExamDate" ControlToValidate="txtDateofissue" IsValidEmpty="false"
                                                    InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Date of Issue"
                                                    InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDateofissue"
                                                Display="None" ErrorMessage="Please Select/Enter Date" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 " id="divddlSchlWiseBulk" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Scholarship Category</label>
                                            </div>

                                            <asp:DropDownList ID="ddlSchlWiseBulk" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Scholarship Category" CssClass="form-control" data-select2-enable="true" TabIndex="5" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">SemesterWise</asp:ListItem>
                                                <asp:ListItem Value="2">YearWise</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvScholrarshipCategory" runat="server" ControlToValidate="ddlSchlWiseBulk"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Scholarship Category " ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12 " id="divddlSort" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Sort By</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSort" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                ValidationGroup="show" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Regno</asp:ListItem>
                                                <asp:ListItem Value="2">Category</asp:ListItem>
                                                <asp:ListItem Value="3">Payment Type</asp:ListItem>
                                                <asp:ListItem Value="4">Student Name</asp:ListItem>		
                                            </asp:DropDownList>                                       
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:RadioButtonList ID="rbRegEx" runat="server" RepeatDirection="Horizontal" TabIndex="8" AutoPostBack="true" OnSelectedIndexChanged="rbRegEx_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Selected="True">&nbsp;Regular Student &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="1">&nbsp;Ex Student</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                      <%--   <div class="form-group col-lg-3 col-md-6 col-12 ">
                                            <div class="label-dynamic">
                                                <label>Scholership Apply Type </label>
                                                 <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                                ValidationGroup="show">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Demand-wise</asp:ListItem>
                                                <asp:ListItem Value="2">AmountWise</asp:ListItem>                                              
                                            </asp:DropDownList>
                                            </div>
                                            
                                        </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divamount"  runat="server" Visible="false">
                                            <div class="label-dynamic">
                                                <label>Amount</label>                                          
                                            </div>
                                             <asp:TextBox ID="txtAmountsch" runat="server" onkeyup="return IsNumeric(this);" MaxLength="9"></asp:TextBox>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnShow" runat="server" Text="Show Student" OnClick="btnShow_Click" TabIndex="8"
                                                ToolTip="Shows Students under Selected Criteria." ValidationGroup="show" CssClass="btn btn-primary" />

                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="9"
                                                ValidationGroup="show" CssClass="btn btn-primary" OnClientClick="return validateAssign();" CausesValidation="false" />

                                            <asp:Button ID="btnPrintReport" runat="server" Text="Admit Card" TabIndex="999" CssClass="btn btn-info"
                                                OnClick="btnPrintReport_Click" ToolTip="Print Card under Selected Criteria." ValidationGroup="show" Visible="false" />

                                            <asp:Button ID="btnSendEmail" runat="server" Text="Send To Email" TabIndex="10" CssClass="btn btn-primary"
                                                OnClick="btnSendEmail_Click1" ToolTip="Send Card By Email" ValidationGroup="show" Visible="false" />

                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="11"
                                                ToolTip="Cancel Selected under Selected Criteria." CssClass="btn btn-warning" />
                                            <asp:HiddenField ID="hftot" runat="server" />
                                            <asp:HiddenField ID="txtTotStud" runat="server" />

                                        </div>

                                        <div class="form-group col-lg-9 col-md-12 col-12" id="divNote" runat="server" visible="false">
                                            <div class=" note-div">
                                                <h5 class="heading">Note </h5>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Scholarship Allotment Amount after scholarship Adjustment can be modify only by Single Student scholarship allotment provision.</span></p>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:ListView ID="lvStudentRecords" runat="server" OnItemDataBound="lvStudentRecords_ItemDataBound" PageSize="10">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Student List</h5>
                                                </div>
                                                <div class="row mb-1">
                                                    <div class="col-lg-2 col-md-6 offset-lg-7">
                                                        <button type="button" class="btn btn-outline-primary float-lg-right saveAsExcel">Export Excel</button>
                                                    </div>

                                                    <div class="col-lg-3 col-md-6">
                                                        <div class="input-group sea-rch">
                                                            <input type="text" id="FilterData" class="form-control" placeholder="Search" />
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-search"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="table-responsive" style="height: 500px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblStudent">
                                                        <thead class="bg-light-blue">
                                                            <tr id="Tr1" runat="server">
                                                                <th>
                                                                    <asp:CheckBox ID="chkIdentityCard" runat="server" onClick="SelectAll(this);" ToolTip="Select or Deselect All Records" Visible="true" />
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                </th>
                                                                <%--<th>Roll No.</th>--%>
                                                                <th>Student Name
                                                                </th>
                                                                <th style="display: none">Student Email
                                                                </th>
                                                                 <th>Payment Type</th>
                                                                <th>Category
                                                                </th>
                                                                <th>
                                                                    <%--<asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                Year
                                                                </th>
                                                                <th>Scholarship Amount</th>
                                                                <%-- <th>Scholarship Type</th>--%>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>

                                            <ItemTemplate>
                                                <%--  <asp:UpdatePanel runat="server" ID="updList">
                                                <ContentTemplate>--%>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkReport" runat="server" onClick="totStudents(this);" ToolTip='<%# Eval("IDNO") %>' />
                                                        <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                    </td>

                                                    <td>
                                                        <%# Eval("REGNO")%>
                                                    </td>

                                                    <%--<td><%# Eval("ROLLNO")%></td>--%>

                                                    <td>
                                                        <%# Eval("STUDNAME")%>
                                                        <asp:HiddenField ID="hdfAppliid" runat="server" Value='<%# Eval("STUDNAME") %>' />
                                                        <asp:HiddenField ID="hdfBranchno" runat="server" Value='<%# Eval("BRANCHNO") %>' />
                                                        <asp:HiddenField ID="hdfdegreeno" runat="server" Value='<%# Eval("DEGREENO") %>' />
                                                        <%--<asp:HiddenField ID="hdfadmbatch" runat="server" Value='<%# Eval("ADMBATCH") %>' />--%>
                                                        <asp:HiddenField ID="hdfAcademicYearId" runat="server" Value='<%# Eval("ACADEMIC_YEAR_ID") %>' />
                                                    </td>

                                                    <td style="display: none">
                                                        <%--<%# Eval("EMAILID_INS")%>--%>
                                                        <%--<asp:HiddenField ID="Hdfemail" runat="server" Value='<%# Eval("EMAILID_INS") %>' />--%>
                                                    </td>
                                                     <td>
                                                        <%# Eval("PAYTYPENAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CATEGORY")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("YEAR")%>

                                                        <asp:Label ID="lblschamt" runat="server" Text='<%# Eval("YEAR") %>' ToolTip='<%# Eval("DEGREENO") %>' Visible="false"></asp:Label>
                                                        <asp:HiddenField ID="hfdAdmbatch" runat="server" Value='<%# Eval("ADMBATCH") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSemesterAmount" runat="server" Text='<%# Eval("SCHL_AMOUNT") %>'></asp:TextBox>
                                                    </td>

                                                    <%--<td>
                                                        <asp:Label ID="lblDDLData" runat="server" Text='<%# Eval("SCHOLARSHIP_ID") %>' Visible="false"></asp:Label>

                                                        <asp:DropDownList ID="ddlScholarShipsType" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                            ToolTip="Please Select ScholarShip Type">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>--%>
                                                </tr>

                                                <%--</ContentTemplate>
                                                <Triggers>
                                                        <asp:PostBackTrigger ControlID="rdoYes" />
                                                        <asp:PostBackTrigger ControlID="rdoNo" />
                                                </Triggers>
                                            </asp:UpdatePanel>--%>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="show" DisplayMode="List" />
                                <div id="divMsg" runat="server">
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lvStudentRecords" />
            <asp:PostBackTrigger ControlID="btnShow" />
           <%-- <asp:PostBackTrigger ControlID="gvSemesters" />--%>
        </Triggers>

        <%--<Triggers>   
            <asp:AsyncPostBackTrigger ControlID="btnPrintReport" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
               <asp:PostBackTrigger ControlID="btnShow" />
        </Triggers>--%>

    </asp:UpdatePanel>
    <script type="text/javascript">
        function showConfirm() {
            var ret = confirm('Do You Really Want  To  Remove Scholarship Adjustment ?');
            if (ret == true)
                return true;
            else
                return false;
        }

        function showConfirmAllotment() {
            var ret = confirm('Do You Really Want To  Remove Scholarship Allotment ?');
            if (ret == true)
                return true;
            else
                return false;
        }
    </script>
    <script type="text/javascript">
        function SelectAll(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>');
            for (i = 0; i < hftot.value; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_chkReport');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        lst.checked = true;
                        txtTot.value = hftot.value;

                        //document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoYes').disabled = false;
                        //document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoNo').disabled = false;
                    }
                    else {
                        lst.checked = false;
                        txtTot.value = 0;

                        //document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoYes').disabled = true;
                        //document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoNo').disabled = true;

                        //$('#ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoYes').prop('checked', false);
                        //$('#ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoNo').prop('checked', false);
                    }
                }

            }
        }


        function validateNumericAndNotZero(txt) {
            if (isNaN(txt.value)) {
                txt.value ='';
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

        function validateAssign() {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;
            if (txtTot == 0) {
                alert('Please Check atleast one student ');
                return false;
            }
            else 
            {

                var chk = document.forms[0];
                for (var i = 0; i < chk.length; i++) {
                    if (chk.checked == true) {
                        var text = document.getElementById("txtSemestersAmount").value;
                        var selected = document.getElementById("ddlScholorshipType");

                        if (text == "" || selected.value == 0) {
                            alert("Please Enter Amount and Scholarship Type");
                            return false;
                        }
                        else
                            return true;
                    }
                }
                return true;
            }
        }

        function LoadImage() {
            document.getElementById("ctl00_ContentPlaceHolder1_imgCollegeLogo").src = document.getElementById("ctl00_ContentPlaceHolder1_fuCollegeLogo").value;
        }


        function IsNumeric(txt) {
            var ValidChars = ".0123456789";
            var num = true;
            var mChar;
            cnt = 0

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);

                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Please enter Numeric values only")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }
    </script>

    <script type="text/javascript">
        var count = 0;
        var count2 = 0;
        var isClicked = false;
        function Call() {

            debugger;

            if (document.getElementById('tblStudent') != null) {
                var dataRows = document.getElementById('tblStudent').getElementsByTagName('tr');

                if (dataRows.length > 0) {
                    isClicked = true;
                    for (i = 0; i <= (dataRows.length - 1) ; i++) {

                        if ((document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_chkReport').checked) && (document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_chkReport').disabled == false)) {
                            //if (isClicked) {
                            // alert('checked')

                            totSelectedCount();
                            document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoYes').disabled = false;
                            document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoNo').disabled = false;
                            //alert(count);
                            isClicked = false;

                            // }
                        }
                        else if (!document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_chkReport').checked) {

                            //alert('unchecked')

                            document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoYes').disabled = true;
                            document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoNo').disabled = true;

                            $('#ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoYes').prop('checked', false);
                            $('#ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoNo').prop('checked', false);

                            //for (j = 0; j <= (dataRows.length - 1) ; j++) {
                            //    if (document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + j + '_chkReport').checked) {
                            //        if (isClicked) {
                            //            alert('aaaaaaaaaa')
                            //            totSelectedCount();
                            //            isClicked = false;
                            //        }
                            //    }
                            //}
                        }
                    }

                }
            }
        }

        function totSelectedCount() {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            txtTot.value = Number(txtTot.value) + 1;
        }

        function totStudents(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;
        }

        function numericFilter(txb) {
            txb.value = txb.value.replace(/[^\0-9]/ig, "");
        }

        function validScholarship() {
            debugger;
            var chk = document.forms[0];
            for (var i = 0; i < chk.length; i++) {
                if (chk.checked == true) {
                    var text = document.getElementById("txtSemestersAmount").value;
                    var selected = document.getElementById("ddlScholorshipType").value;

                    if (text == "" || selected.value == 0) {
                        alert("Please Enter Amount and Scholarship Type");
                        return false;
                    }
                }

            }
        }


    </script>

    <script>
        function toggleSearch(searchBar, table) {
            var tableBody = table.querySelector('tbody');
            var allRows = tableBody.querySelectorAll('tr');
            var val = searchBar.value.toLowerCase();
            allRows.forEach((row, index) => {
                var insideSearch = row.innerHTML.trim().toLowerCase();
            //console.log('data',insideSearch.includes(val),'searchhere',insideSearch);
            if (insideSearch.includes(val)) {
                row.style.display = 'table-row';
            }
            else {
                row.style.display = 'none';
            }

        });
        }

        function test5() {
            var searchBar5 = document.querySelector('#FilterData');
            var table5 = document.querySelector('#tblStudent');

            //console.log(allRows);
            searchBar5.addEventListener('focusout', () => {
                toggleSearch(searchBar5, table5);
        });

        $(".saveAsExcel").click(function () {
               
            //if (confirm('Do You Want To Apply for New Program?') == true) {
            //    return false;
            //}
            var workbook = XLSX.utils.book_new();
            var allDataArray = [];
            allDataArray = makeTableArray(table5, allDataArray);
            var worksheet = XLSX.utils.aoa_to_sheet(allDataArray);
            workbook.SheetNames.push("Test");
            workbook.Sheets["Test"] = worksheet;
            XLSX.writeFile(workbook, "ScholarshipAllotment.xlsx");
        });
        }

        function makeTableArray(table, array) {
            var allTableRows = table.querySelectorAll('tr');
            allTableRows.forEach(row => {
                var rowArray = [];
            if (row.querySelector('td')) {
                var allTds = row.querySelectorAll('td');
                allTds.forEach(td => {
                    if (td.querySelector('span')) {
                        rowArray.push(td.querySelector('span').textContent);
            }
            else if (td.querySelector('input')) {
                rowArray.push(td.querySelector('input').value);
            }
            else if (td.querySelector('select')) {
                rowArray.push(td.querySelector('select').value);
            }
            else if (td.innerText) {
                rowArray.push(td.innerText);
            }
            else{
                rowArray.push('');
            }
        });
        }
        if (row.querySelector('th')) {
            var allThs = row.querySelectorAll('th');
            allThs.forEach(th => {
                if (th.textContent) {
                    rowArray.push(th.textContent);
        }
        else {
            rowArray.push('');
        }
        });
        }
        // console.log(allTds);

        array.push(rowArray);
        });
        return array;
        }

    </script>

</asp:Content>
