<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ExamRule.aspx.cs" Inherits="ACADEMIC_MASTERS_ExamRule" Title="" %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl"
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




    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
<<<<<<< HEAD
                        <h3 class="box-title">EXAM RULE</h3>
=======
                        <%--<h3 class="box-title">EXAM RULE</h3>--%>
                         <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
>>>>>>> f47b83fb ([ENHANCEMENT] [58131] Add Dynamic Label to Masters)
                    </div>
                    <div class="box-body">
                        <div class="nav-tabs-custom" id="Tabs" role="tabpanel">
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link active" data-toggle="tab" href="#tab_1">Course Wise</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#tab_2" id="tab2">Subject Type Wise</a>
                                </li>
                            </ul>
                            <div class="tab-content" id="my-tab-content">
                                <div class="tab-pane active" id="tab_1">
                                    <div>
                                        <asp:UpdateProgress ID="updprogupdSchemeType" runat="server" AssociatedUpdatePanelID="updSchemeType1"
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
                                    <asp:UpdatePanel ID="updSchemeType1" runat="server">
                                        <ContentTemplate>
                                            <div class="row mt-3">
                                                <div class="col-md-12 col-sm-12 col-12">
                                                    <div id="div1" runat="server"></div>
                                                    <%-- <div class="box-header with-border">
                            <h3 class="box-title">EXAM RULE</h3>
                        </div>--%>
                                                    <%-- <div class="box-body">--%>
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>College & Scheme</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                                    ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname"
                                                                    Display="None" ErrorMessage="Please Select College & Regulation" InitialValue="0" ValidationGroup="course">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Session</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                                    Display="None" ErrorMessage="Please Select Session." InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="DivSubtype" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Subject Type</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlsubjecttype" runat="server" TabIndex="1" AppendDataBoundItems="true"
                                                                    ToolTip="Please Select Subject Type" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvProgram" runat="server" ControlToValidate="ddlsubjecttype"
                                                                    Display="None" ErrorMessage="Please Select Subject Type" ValidationGroup="submit"
                                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Degree</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="2" CssClass="form-control" data-select2-enable="true">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                                    Display="None" ErrorMessage="Please Select Degree." InitialValue="0" ValidationGroup="coursee"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Branch</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="3" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Branch." ValidationGroup="coursee"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Regulation</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlScheme" runat="server" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                                    AutoPostBack="True" TabIndex="4">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Regulation." ValidationGroup="coursee"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Semester</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                                    AutoPostBack="True" TabIndex="5" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                                                    Display="None" ErrorMessage="Please Select Semester." InitialValue="0" ValidationGroup="course">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <%-- to copy --%>
                                                        <asp:Panel ID="pnlCsession" runat="server" Height="80px" Visible="false">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>Copy to Session</label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlCsession" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="1" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlCsession_SelectedIndexChanged">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <%--<asp:RequiredFieldValidator ID="rfvCsession" runat="server" ControlToValidate="ddlCsession"
                                            Display="None" ErrorMessage="Please Select Session." InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>--%>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnShow" Text="Show" runat="server" CssClass="btn btn-primary" ValidationGroup="course" OnClick="btnShow_Click" TabIndex="14" />
                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="course" OnClick="btnSubmit_Click" Enabled="false" />
                                                        <asp:Button ID="btnLock" runat="server" Text="Lock" CssClass="btn btn-primary" OnClick="btnLock_Click" Enabled="false" />
                                                        <asp:Button ID="btnCopy" runat="server" Text="Copy to Session" CssClass="btn btn-primary" OnClick="btnCopy_Click" Enabled="false" />
                                                        <asp:Button ID="btnCancel" runat="server" TabIndex="15" Text="Clear" OnClick="btnCancel_Click"
                                                            CausesValidation="False" CssClass="btn btn-warning" />
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="course" />
                                                    </div>
                                                    <div class="col-12">
                                                        <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl"></asp:Label>
                                                    </div>
                                                    <div id="dvExamRule" runat="server" class="col-12" visible="false">
                                                        <asp:Panel ID="pnlCourses" runat="server" Height="500px" ScrollBars="Auto">
                                                            <asp:ListView ID="lvCourseExamRule" runat="server" OnItemDataBound="lvCourseExamRule_ItemDataBound">
                                                                <LayoutTemplate>
                                                                    <div id="demo-grid">
                                                                        <div class="sub-heading">
                                                                            <h5>ExamRule</h5>
                                                                        </div>
                                                                        <%-- <h3><span class="label label-default">ExamRule</span></h3>--%>
                                                                        <%--  <table class="table table-bordered">--%>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="mytable">
                                                                            <thead>
                                                                                <tr class="bg-light-blue">
                                                                                    <th colspan="3" style="text-align: center">SUBJECT </th>
                                                                                    <th id="tbl_Rule1" colspan="6" style="text-align: center">RULE 1 </th>
                                                                                    <th id="tbl_Rule2" colspan="1" style="text-align: center; display: none;">RULE 2 </th>
                                                                                </tr>
                                                                            </thead>
                                                                            <thead>
                                                                                <tr class="bg-light-blue">
                                                                                    <th style="text-align: center">ACTION</th>
                                                                                    <th>CCODE</th>
                                                                                    <th>SUBJECT NAME</th>
                                                                                    <th id="th0" style="text-align: center; display: none;"></th>
                                                                                    <%--CAT 1 (%)--%>
                                                                                    <th id="th1" style="text-align: center; display: none;"></th>
                                                                                    <th id="th2" style="text-align: center; display: none;"></th>
                                                                                    <th id="th3" style="text-align: center; display: none;"></th>
                                                                                    <th id="th4" style="text-align: center; display: none;"></th>
                                                                                    <th id="th5" style="text-align: center; display: none;"></th>
                                                                                    <th id="th6" style="text-align: center; display: none;"></th>
                                                                                    <th id="th7" style="text-align: center; display: none;"></th>
                                                                                    <th id="th8" style="text-align: center; display: none;"></th>
                                                                                    <th id="th9" style="text-align: center; display: none;"></th>


                                                                                    <th id="th10" style="text-align: center; display: none;"></th>
                                                                                    <th id="th11" style="text-align: center; display: none;"></th>
                                                                                    <th id="th12" style="text-align: center; display: none;"></th>
                                                                                    <th id="th13" style="text-align: center; display: none;"></th>
                                                                                    <th id="th14" style="text-align: center; display: none;"></th>
                                                                                    <th id="th15" style="text-align: center; display: none;"></th>
                                                                                    <th id="th16" style="text-align: center; display: none;"></th>
                                                                                    <th id="th17" style="text-align: center; display: none;"></th>
                                                                                    <th id="th18" style="text-align: center; display: none;"></th>
                                                                                    <th id="th19" style="text-align: center; display: none;"></th>
                                                                                    <%-- <th id="tbl_cat3" style="text-align: center">CAT 3 (%)</th>
                                                                <th id="tbl_cat3_assign" style="text-align: center">ASSIGNMENT 3 (%)</th>--%>
                                                                                    <th style="text-align: center; display: none;">AI-Rule </th>
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
                                                                        <td style="display: none;">
                                                                            <center>
                                                        <asp:CheckBox ID="chkAccept" runat="server" OnCheckedChanged="chkAccept_CheckedChanged" AutoPostBack="true" TabIndex="13" ToolTip='<%# Eval("COURSENO") %>' Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>' />
                                                        <asp:Label ID="lblLock" runat="server" Text="LOCKED" Visible="false" style="color:white;font-size:10px;font-weight:bold;background-color:green;padding:5px;text-align: center;box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);"></asp:Label>
                                                        <%--onclick="GetCheckStatus(this)"--%>

                                                            <%--Style="text-align: center;box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.2), 0 3px 10px 0 rgba(0, 0, 0, 0.19);"--%>

                                                        <%--<asp:HiddenField runat="server" ID="hdfCourseNo" Value='<%# Eval("COURSENO")%>'></asp:HiddenField>--%>
                                                        </center>
                                                                        </td>
                                                                        <td style="display: none;">
                                                                            <asp:Label ID="lblccode" runat="server" Text='<%# Eval("CCODE") %>' />
                                                                        </td>
                                                                        <td style="display: none;">
                                                                            <asp:Label ID="lblcname" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                                        </td>
                                                                        <td id="td0" runat="server" style="display: none;"><%--<asp:HiddenField ID="hfExamNo" runat="server" Value='<%# Eval("SUBEXAMNO")%>' />--%>
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat1" runat="server" CssClass="form-control NumVal" Enabled="false" MaxLength="5"  Text='<%# Eval("1") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtCat1" runat="server" CssClass="form-control NumVal" Enabled="false" MaxLength="5" ></asp:TextBox>
                                                            <asp:HiddenField ID="hdfCat1" runat="server" />
                                                        </center>
                                                                        </td>
                                                                        <td id="td1" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat1asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5" Style="width: 80%" Text='<%# Eval("2") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtCat1asn"  runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5" ></asp:TextBox>
                                                            <asp:HiddenField ID="hdfCat1_Asign" runat="server" />
                                                        </center>
                                                                        </td>
                                                                        <td id="td2" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat2" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5" Style="width: 80%" Text='<%# Eval("3") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtCat2" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5" ></asp:TextBox>
                                                            <asp:HiddenField ID="hdfCat2" runat="server" />
                                                        </center>
                                                                        </td>
                                                                        <td id="td3" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat2asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5" Style="width: 80%" Text='<%# Eval("4") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtCat2asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5" 
                                                                Style="/*width: 80%*/"></asp:TextBox>
                                                            <asp:HiddenField ID="hdfCat2_Asign" runat="server" />
                                                        </center>
                                                                        </td>
                                                                        <td id="td4" runat="server" style="display: none;">


                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("5") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtCat3" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5" ></asp:TextBox>
                                                            <asp:HiddenField ID="hdfCat3" runat="server" />
                                                        </center>
                                                                        </td>
                                                                        <td id="td5" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5" Style="width: 80%" Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5" ></asp:TextBox>
                                                            <asp:HiddenField ID="hdfCat3_Asign" runat="server" />
                                                        </center>
                                                                        </td>
                                                                        <td id="td6" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt7" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5" ></asp:TextBox>
                                                            <asp:HiddenField ID="hdf7" runat="server" />
                                                        </center>
                                                                        </td>
                                                                        <td id="td7" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt8" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5" ></asp:TextBox>
                                                            <asp:HiddenField ID="hdf8" runat="server" />
                                                        </center>
                                                                        </td>
                                                                        <td id="td8" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt9" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5" ></asp:TextBox>
                                                            <asp:HiddenField ID="hdf9" runat="server" />
                                                        </center>
                                                                        </td>
                                                                        <td id="td9" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt10" runat="server"  CssClass="form-control NumVal" Enabled="false"  MaxLength="5" ></asp:TextBox>
                                                            <asp:HiddenField ID="hdf10" runat="server" />
                                                        </center>
                                                                        </td>


                                                                        <td id="td10" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt11" runat="server"  CssClass="form-control NumVal" Enabled="false"  MaxLength="5" ></asp:TextBox>
                                                            <asp:HiddenField ID="hdf11" runat="server" />
                                                        </center>

                                                                        </td>
                                                                        <td id="td11" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt12" runat="server"  CssClass="form-control NumVal" Enabled="false"  MaxLength="5" ></asp:TextBox>
                                                            <asp:HiddenField ID="hdf12" runat="server" />
                                                        </center>

                                                                        </td>
                                                                        <td id="td12" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt13" runat="server"  CssClass="form-control NumVal" Enabled="false"  MaxLength="5" ></asp:TextBox>
                                                            <asp:HiddenField ID="hdf13" runat="server" />
                                                        </center>

                                                                        </td>
                                                                        <td id="td13" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt14" runat="server"  CssClass="form-control NumVal" Enabled="false"  MaxLength="5" ></asp:TextBox>
                                                            <asp:HiddenField ID="hdf14" runat="server" />
                                                        </center>

                                                                        </td>
                                                                        <td id="td14" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt15" runat="server"  CssClass="form-control NumVal" Enabled="false"  MaxLength="5" ></asp:TextBox>
                                                            <asp:HiddenField ID="hdf15" runat="server" />
                                                        </center>

                                                                        </td>
                                                                        <td id="td15" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt16" runat="server"  CssClass="form-control NumVal" Enabled="false"  MaxLength="5" ></asp:TextBox>
                                                            <asp:HiddenField ID="hdf16" runat="server" />
                                                        </center>

                                                                        </td>
                                                                        <td id="td16" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt17" runat="server"  CssClass="form-control NumVal" Enabled="false"  MaxLength="5" ></asp:TextBox>
                                                            <asp:HiddenField ID="hdf17" runat="server" />
                                                        </center>

                                                                        </td>
                                                                        <td id="td17" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt18" runat="server"  CssClass="form-control NumVal" Enabled="false"  MaxLength="5" ></asp:TextBox>
                                                            <asp:HiddenField ID="hdf18" runat="server" />
                                                        </center>

                                                                        </td>
                                                                        <td id="td18" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt19" runat="server"  CssClass="form-control NumVal" Enabled="false"  MaxLength="5" ></asp:TextBox>
                                                            <asp:HiddenField ID="hdf19" runat="server" />
                                                        </center>

                                                                        </td>
                                                                        <td id="td19" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt20" runat="server"  CssClass="form-control NumVal" Enabled="false"  MaxLength="5" ></asp:TextBox>
                                                            <asp:HiddenField ID="hdf20" runat="server" />
                                                        </center>

                                                                        </td>

                                                                        <td style="display: none;">
                                                                            <center>
                                                            <asp:TextBox ID="txtrule2" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("RULE2") %>'></asp:TextBox>
                                                            <%--<asp:TextBox ID="txtrule2" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5" Style="width: 80%"></asp:TextBox>--%>
                                                        </center>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>

                                <div class="tab-pane fade" id="tab_2">
                                    <div>
                                        <asp:UpdateProgress ID="UpdprogupdSchemeCreation" runat="server" AssociatedUpdatePanelID="updSchemeCreation1"
                                            DynamicLayout="true" DisplayAfter="0" UpdateMode="Conditional">
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

                                    <asp:UpdatePanel ID="updSchemeCreation1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <%--<div class="box-body">--%>
                                            <div class="row mt-3">
                                                <div class="col-md-12 col-sm-12 col-12">
                                                    <div id="div2" runat="server"></div>

                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>College & Scheme</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlClgname2" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                                    ValidationGroup="course2" OnSelectedIndexChanged="ddlClgname2_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlClgname2"
                                                                    Display="None" ErrorMessage="Please Select College & Regulation" InitialValue="0" ValidationGroup="course2">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Session</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlsession2" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="1" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession2_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                                </asp:DropDownList><%--OnSelectedIndexChanged="ddlSession2_SelectedIndexChanged"--%>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlsession2"
                                                                    Display="None" ErrorMessage="Please Select Session." InitialValue="0" ValidationGroup="course2"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <%-- <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="Div3" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Subject Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlsubjecttype2" runat="server" TabIndex="1" AppendDataBoundItems="true"  visible="true"
                                            ToolTip="Please Select Subject Type" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlsubjecttype2"
                                            Display="None" ErrorMessage="Please Select Subject Type" ValidationGroup="course2"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>  --%>
                                                            <%-- </div>--%>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="Div3" visible="true">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Subject Type</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlsubjecttype2" runat="server" TabIndex="1" AppendDataBoundItems="true" ValidationGroup="course2"
                                                                    ToolTip="Please Select Subject Type" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlsubjecttype2"
                                                                    Display="None" ErrorMessage="Please Select Subject Type" ValidationGroup="course2"
                                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Semester</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlsem2" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                                    AutoPostBack="True" TabIndex="5" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged2">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlsem2"
                                                                    Display="None" ErrorMessage="Please Select Semester." InitialValue="0" ValidationGroup="course22">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                            <%-- </div>--%>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-4 col-md-12 col-12">
                                                        <div class=" note-div">
                                                            <h5 class="heading">Note </h5>
                                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>To get Consolidate Report only select Session.</span> </p>
                                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>To get All Active Session Report No Selection.</span> </p>
                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnshow2" Text="Show" runat="server" CssClass="btn btn-primary" ValidationGroup="course2" OnClick="btnShow2_Click" TabIndex="14" />
                                                        <asp:Button ID="btnSubmit2" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="course2" OnClick="btnSubmit_Click2" Enabled="false" />
                                                        <asp:Button ID="btnLock2" runat="server" Text="Lock" CssClass="btn btn-primary" OnClick="btnLock2_Click" Enabled="false" />
                                                        <asp:Button ID="btnReport2" runat="server" TabIndex="14" Text="Report" CssClass="btn btn-info" CausesValidation="false" OnClick="btnReport2_Click" Enabled="false" ValidationGroup="course2" />
                                                        <asp:Button ID="btnexcel" runat="server" TabIndex="16" Text="Export To Excel" OnClick="btnExcel_Click"
                                                            CausesValidation="False" CssClass="btn btn-info" />
                                                        <asp:Button ID="btnCancel2" runat="server" TabIndex="15" Text="Clear" OnClick="btnCancel_Click2"
                                                            CausesValidation="False" CssClass="btn btn-warning" />
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="course2" />
                                                    </div>

                                                    <div class="col-12">
                                                        <asp:Label ID="lblstatus2" runat="server" SkinID="Errorlbl2"></asp:Label>
                                                    </div>
                                                    <div id="dvExamRule2" runat="server" class="col-12" visible="false">
                                                        <asp:Panel ID="pnlCourses2" runat="server" Height="500px" ScrollBars="Auto">
                                                            <asp:ListView ID="lvSubjectWiseExamRule" runat="server">
                                                                <%--OnItemDataBound="lvSubjectWiseExamRule_ItemDataBound"--%>
                                                                <LayoutTemplate>
                                                                    <div id="demo-grid">
                                                                        <div class="sub-heading">
                                                                            <h5>ExamRule</h5>
                                                                        </div>
                                                                        <%-- <h3><span class="label label-default">ExamRule</span></h3>--%>
                                                                        <%--  <table class="table table-bordered">--%>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="mytable">
                                                                            <thead>
                                                                                <tr class="bg-light-blue">
                                                                                    <%--<th colspan="3" style="text-align: center">SUBJECT </th>--%>
                                                                                    <th id="tbl_Rule12" colspan="6" style="text-align: center">RULE 1 </th>
                                                                                    <th id="tbl_Rule22" colspan="1" style="text-align: center; display: none;">RULE 2 </th>
                                                                                </tr>
                                                                            </thead>
                                                                            <thead>
                                                                                <tr class="bg-light-blue">
                                                                                    <%--<th style="text-align: center; display: none;">ACTION</th>
                                                                    <th id="th1" style="text-align: center; display: none;">CCODE</th>
                                                                    <th id="th1" style="text-align: center; display: none;">SUBJECT NAME</th>--%>
                                                                                    <%--<th>SUBJECT TYPE</th>--%>
                                                                                    <th id="th0" style="text-align: center; display: none;"></th>
                                                                                    <%--CAT 1 (%)--%>
                                                                                    <th id="th1" style="text-align: center; display: none;"></th>
                                                                                    <th id="th2" style="text-align: center; display: none;"></th>
                                                                                    <th id="th3" style="text-align: center; display: none;"></th>
                                                                                    <th id="th4" style="text-align: center; display: none;"></th>
                                                                                    <th id="th5" style="text-align: center; display: none;"></th>
                                                                                    <th id="th6" style="text-align: center; display: none;"></th>
                                                                                    <th id="th7" style="text-align: center; display: none;"></th>
                                                                                    <th id="th8" style="text-align: center; display: none;"></th>
                                                                                    <th id="th9" style="text-align: center; display: none;"></th>


                                                                                    <th id="th10" style="text-align: center; display: none;"></th>
                                                                                    <th id="th11" style="text-align: center; display: none;"></th>
                                                                                    <th id="th12" style="text-align: center; display: none;"></th>
                                                                                    <th id="th13" style="text-align: center; display: none;"></th>
                                                                                    <th id="th14" style="text-align: center; display: none;"></th>
                                                                                    <th id="th15" style="text-align: center; display: none;"></th>
                                                                                    <th id="th16" style="text-align: center; display: none;"></th>
                                                                                    <th id="th17" style="text-align: center; display: none;"></th>
                                                                                    <th id="th18" style="text-align: center; display: none;"></th>
                                                                                    <th id="th19" style="text-align: center; display: none;"></th>
                                                                                    <%-- <th id="tbl_cat3" style="text-align: center">CAT 3 (%)</th>
                                                                <th id="tbl_cat3_assign" style="text-align: center">ASSIGNMENT 3 (%)</th>--%>
                                                                                    <th style="text-align: center; display: none;">AI-Rule </th>
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
                                                                        <%--    <td style="display: none;">
                                                            <center>
                                                        <asp:CheckBox ID="chkAccept2" runat="server" OnCheckedChanged="chkAccept_CheckedChanged2" AutoPostBack="true" TabIndex="13" ToolTip='<%# Eval("COURSENO") %>' Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>' />
                                                        <asp:Label ID="lblLock" runat="server" Text="LOCKED" Visible="false" style="color:white;font-size:10px;font-weight:bold;background-color:green;padding:5px;text-align: center;box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);"></asp:Label>
                                                      
                                                        </center>
                                                        </td>
                                                        <td style="display: none;">
                                                            <asp:Label ID="lblccode" runat="server" Text='<%# Eval("CCODE") %>' />
                                                        </td>
                                                        <td style="display: none;">
                                                            <asp:Label ID="lblcname" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                        </td>--%>
                                                                        <%-- <td style="display: none;">
                                                            <asp:Label ID="subjecttype" runat="server" Text='<%# Eval("SUBNAME") %>' />
                                                        </td>--%>
                                                                        <td id="td0" runat="server" style="display: none;"><%--<asp:HiddenField ID="hfExamNo" runat="server" Value='<%# Eval("SUBEXAMNO")%>' />--%>
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat1" runat="server" CssClass="form-control NumVal" Enabled="false" MaxLength="5"  Text='<%# Eval("1") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtCat12" runat="server" CssClass="form-control NumVal" MaxLength="5" Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>'></asp:TextBox>

                                                            <asp:HiddenField ID="hdfCat12" runat="server" />
                                                        </center>
                                                                        </td>
                                                                        <td id="td1" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat1asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5" Style="width: 80%" Text='<%# Eval("2") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtCat1asn2"  runat="server" CssClass="form-control NumVal"   MaxLength="5" Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>' ></asp:TextBox>
                                                            <asp:HiddenField ID="hdfCat1_Asign2" runat="server" />
                                                        </center>
                                                                        </td>
                                                                        <td id="td2" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat2" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5" Style="width: 80%" Text='<%# Eval("3") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtCat22" runat="server" CssClass="form-control NumVal"   MaxLength="5" Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>'></asp:TextBox>
                                                            <asp:HiddenField ID="hdfCat22" runat="server" />
                                                        </center>
                                                                        </td>
                                                                        <td id="td3" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat2asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5" Style="width: 80%" Text='<%# Eval("4") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtCat2asn2" runat="server" CssClass="form-control NumVal"   MaxLength="5" Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>'
                                                                Style="/*width: 80%*/"></asp:TextBox>
                                                            <asp:HiddenField ID="hdfCat2_Asign2" runat="server" />
                                                        </center>
                                                                        </td>
                                                                        <td id="td4" runat="server" style="display: none;">


                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("5") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtCat32" runat="server" CssClass="form-control NumVal"   MaxLength="5" Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>'></asp:TextBox>
                                                            <asp:HiddenField ID="hdfCat32" runat="server" />
                                                        </center>
                                                                        </td>
                                                                        <td id="td5" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5" Style="width: 80%" Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtCat3asn2" runat="server" CssClass="form-control NumVal"  MaxLength="5" Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>'></asp:TextBox>
                                                            <asp:HiddenField ID="hdfCat3_Asign2" runat="server" />
                                                        </center>
                                                                        </td>
                                                                        <td id="td6" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt72" runat="server" CssClass="form-control NumVal"  MaxLength="5" Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>'></asp:TextBox>
                                                            <asp:HiddenField ID="hdf72" runat="server" />
                                                        </center>
                                                                        </td>
                                                                        <td id="td7" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt82" runat="server" CssClass="form-control NumVal"   MaxLength="5" ></asp:TextBox>
                                                            <asp:HiddenField ID="hdf82" runat="server" />
                                                        </center>
                                                                        </td>
                                                                        <td id="td8" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt92" runat="server" CssClass="form-control NumVal"   MaxLength="5" Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>'></asp:TextBox>
                                                            <asp:HiddenField ID="hdf92" runat="server" />
                                                        </center>
                                                                        </td>
                                                                        <td id="td9" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt102" runat="server"  CssClass="form-control NumVal"   MaxLength="5" Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>'></asp:TextBox>
                                                            <asp:HiddenField ID="hdf102" runat="server" />
                                                        </center>
                                                                        </td>


                                                                        <td id="td10" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt112" runat="server"  CssClass="form-control NumVal"   MaxLength="5" Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>' ></asp:TextBox>
                                                            <asp:HiddenField ID="hdf112" runat="server" />
                                                        </center>

                                                                        </td>
                                                                        <td id="td11" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt122" runat="server"  CssClass="form-control NumVal"   MaxLength="5" Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>'></asp:TextBox>
                                                            <asp:HiddenField ID="hdf122" runat="server" />
                                                        </center>

                                                                        </td>
                                                                        <td id="td12" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt132" runat="server"  CssClass="form-control NumVal"   MaxLength="5" Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>'></asp:TextBox>
                                                            <asp:HiddenField ID="hdf132" runat="server" />
                                                        </center>

                                                                        </td>
                                                                        <td id="td13" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt142" runat="server"  CssClass="form-control NumVal"   MaxLength="5" Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>'></asp:TextBox>
                                                            <asp:HiddenField ID="hdf142" runat="server" />
                                                        </center>

                                                                        </td>
                                                                        <td id="td14" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt152" runat="server"  CssClass="form-control NumVal"   MaxLength="5" Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>' ></asp:TextBox>
                                                            <asp:HiddenField ID="hdf152" runat="server" />
                                                        </center>

                                                                        </td>
                                                                        <td id="td15" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt162" runat="server"  CssClass="form-control NumVal"   MaxLength="5" Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>' ></asp:TextBox>
                                                            <asp:HiddenField ID="hdf162" runat="server" />
                                                        </center>

                                                                        </td>
                                                                        <td id="td16" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt172" runat="server"  CssClass="form-control NumVal"   MaxLength="5" Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>'></asp:TextBox>
                                                            <asp:HiddenField ID="hdf172" runat="server" />
                                                        </center>

                                                                        </td>
                                                                        <td id="td17" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt182" runat="server"  CssClass="form-control NumVal"  MaxLength="5" Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>'></asp:TextBox>
                                                            <asp:HiddenField ID="hdf182" runat="server" />
                                                        </center>

                                                                        </td>
                                                                        <td id="td18" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt192" runat="server"  CssClass="form-control NumVal"   MaxLength="5" Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>'></asp:TextBox>
                                                            <asp:HiddenField ID="hdf192" runat="server" />
                                                        </center>

                                                                        </td>
                                                                        <td id="td19" runat="server" style="display: none;">
                                                                            <center>
                                                            <%--<asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5"  Text='<%# Eval("6") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="txt202" runat="server"  CssClass="form-control NumVal"   MaxLength="5" Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>'></asp:TextBox>
                                                            <asp:HiddenField ID="hdf202" runat="server" />
                                                        </center>

                                                                        </td>

                                                                        <td id="td20" runat="server" style="display: none;">
                                                                            <center>
                                                            <asp:TextBox ID="txtrule22" runat="server" CssClass="form-control NumVal"   MaxLength="5"  Text='<%# Eval("RULE2") %>' Enabled='<%# (Eval("ISLOCK").ToString() == "1" ? false : true)%>' ></asp:TextBox>
                                                            <%--<asp:TextBox ID="txtrule2" runat="server" CssClass="form-control NumVal" Enabled="false"  MaxLength="5" Style="width: 80%"></asp:TextBox>--%>
                                                        </center>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <asp:HiddenField ID="TabName" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnSubmit"   />
            <asp:PostBackTrigger ControlID="btnSubmit2" />--%>

            <asp:PostBackTrigger ControlID="ddlClgname" />
            <asp:PostBackTrigger ControlID="btnexcel" />
            <%-- <asp:PostBackTrigger ControlID="btnReport2" />--%>
        </Triggers>

    </asp:UpdatePanel>
    <div runat="server" id="divMsg">
    </div>


    <div class="modal fade" id="myModal1" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                </div>
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <div class="box-body modal-warning">
                            <div class="form-group" style="text-align: center">
                                <asp:Label ID="lblmessageShow" Style="font-weight: bold" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lblmessageShow2" Style="font-weight: bold" runat="server" Text=""></asp:Label>

                            </div>
                            <div class="box-footer">
                                <p class="text-center">
                                    <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-default"
                                        data-dismiss="modal" />
                                </p>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function showModal() {
            $("#myModal1").modal('show');
        }

    </script>
    <script type="text/javascript">
        function showModal2() {
            $("#myModal12").modal('show');
        }

    </script>

    <%--<script type="text/javascript">
            $(function () {
                alert('hi');
                var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "tab_1";
                alert(tabName);
                $('#Tabs a[href="#' + tabName + '"]').tab('show');
                $("#Tabs a").click(function () {
                    $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
                });
            });
</script>--%>





    <%--<script language="javascript" type="text/javascript">
        function CheckSelectionCount(chk) {
            debugger;
            var str = chk.id;
            var start = str.indexOf("_ctrl") + 5;
            var end = str.indexOf("_cbRow");
            var eindex = str.substring(start, end);

            //ctl00_ContentPlaceHolder1_lvCourseExamRule_ctrl0_txtCat1
            var extChk = document.getElementById("ctl00_ContentPlaceHolder1_lvCourseExamRule_ctrl" + eindex + "_txtCat1");
            if (chk.checked == true) {
                extChk.checked = true;
                extChk.disabled = false;
            }
            else {
                extChk.disabled = true;
            }
        }

        //function GetCheckStatus() {
        //    var srcControlId = event.srcElement.id;
        //    var targetControlId = event.srcElement.id.replace('chkAccept', 'txtCat1');
        //    var txtCat1asn = event.srcElement.id.replace('chkAccept', 'txtCat1asn');
        //    var txtCat2 = event.srcElement.id.replace('chkAccept', 'txtCat2');
        //    var txtCat2asn = event.srcElement.id.replace('chkAccept', 'txtCat2asn');
        //    var txtCat3 = event.srcElement.id.replace('chkAccept', 'txtCat3');
        //    var txtCat3asn = event.srcElement.id.replace('chkAccept', 'txtCat3asn');
        //    var txtrule2 = event.srcElement.id.replace('chkAccept', 'txtrule2');
        //    if (document.getElementById(srcControlId).checked) {
        //        document.getElementById(targetControlId).disabled = false;
        //        document.getElementById(txtCat1asn).disabled = false;
        //        document.getElementById(txtCat2).disabled = false;
        //        document.getElementById(txtCat2asn).disabled = false;
        //        document.getElementById(txtCat3).disabled = false;
        //        document.getElementById(txtCat3asn).disabled = false;
        //        document.getElementById(txtrule2).disabled = false;
        //    }
        //    else {
        //        document.getElementById(targetControlId).disabled = true;
        //        document.getElementById(txtCat1asn).disabled = true;
        //        document.getElementById(txtCat2).disabled = true;
        //        document.getElementById(txtCat2asn).disabled = true;
        //        document.getElementById(txtCat3).disabled = true;
        //        document.getElementById(txtCat3asn).disabled = true;
        //        document.getElementById(txtrule2).disabled = true;
        //    }
        //}
    </script>--%>
    <%--<script>
        $(document).ready(function () {
            $('td').click(function () {
                var row = $(this).parent().parent().children().index($(this).parent());
                $('#ctl00_ContentPlaceHolder1_ListView1_ctrl' + row + '_txtLimit').keypress(function (event) {
                    if ($('#ctl00_ContentPlaceHolder1_ListView1_ctrl' + row + '_txtLimit').val().length > 2) {
                        event.preventDefault();
                    }
                    if (event.which != 8 && isNaN(String.fromCharCode(event.which))) {
                        event.preventDefault();
                    }
                });
            });
        });
    </script>--%>
    <%--<script>
        function btnSubmit_validation() {
            var rowCount = $("#pnlCourses td").closest("tr").length;
            while (rowCount--) {
                if ($.trim($('#ctl00_ContentPlaceHolder1_ListView1_ctrl' + rowCount + '_txtLimit').val()).length == 0) {
                    alert('Please enter Exam Rule for  Selected Courses');
                    return false;
                }
            }
        }
    </script>--%>

    <script>

        $(document).ready(function () {
            //called when key is pressed in textbox
            $(".NumVal").keypress(function (e) {
                if (((e.which != 46 || (e.which == 46 && $(this).val() == '')) || $(this).val().indexOf('.') != -1) && (e.which < 48 || e.which > 57)) {
                    e.preventDefault();
                }
            }).on('paste', function (e) {
                e.preventDefault();
            });

            $(".NumVal").focusout(function () {
                $(this).css("border", "1px solid #d2d6de");
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(".NumVal").keypress(function (e) {
                    if (((e.which != 46 || (e.which == 46 && $(this).val() == '')) || $(this).val().indexOf('.') != -1) && (e.which < 48 || e.which > 57)) {
                        e.preventDefault();
                        //$(this).val("Digits Only").show().fadeOut("slow");
                        $(this).fadeOut("slow").css("border", "1px solid red");
                        $(this).fadeIn("slow");
                    }
                    else {
                        $(this).css("border", "1px solid #3c8dbc");
                    }
                }).on('paste', function (e) {
                    e.preventDefault();
                });

                $(".NumVal").focusout(function () {
                    $(this).css("border", "1px solid #d2d6de");
                });

            });
        });

    </script>
    <script>

        var index = $('#tabs ul').index($('#tabId'));
        $('#tabs ul').tabs('select', index);
    </script>

</asp:Content>
