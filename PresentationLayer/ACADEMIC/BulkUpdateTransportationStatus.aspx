<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BulkUpdateTransportationStatus.aspx.cs"
    Inherits="ACADEMIC_BulkUpdateTransportationStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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


    <%-- <asp:UpdatePanel ID="uppnlmain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>

    <asp:UpdatePanel ID="updtime" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">BULK TRANSPORTATION/HOSTELER STATUS UPDATION</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">HOSTELLER/TRANSPORTATION STATUS UPDATE IN BULK</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="1">HOSTELLER/TRANSPORTATION STATUS UPDATE FOR SINGLE STUDENT
                                        </a>
                                    </li>
                                </ul>

                                <div class="tab-content" id="my-tab-content">

                                    <div class="tab-pane fade" id="tab_2">
                                        <asp:UpdatePanel ID="updtab2" runat="server">
                                            <ContentTemplate>
                                        <div class="box-body">
                                            <div class="box-body">
                                                <div class="col-12">
                                                    <div>
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Search Criteria</label>
                                                                    </div>

                                                                    <%--onchange=" return ddlSearch_change();"--%>
                                                                    <asp:DropDownList runat="server" class="form-control" ID="ddlSearchPanel" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchPanel_SelectedIndexChanged"
                                                                        AppendDataBoundItems="true" data-select2-enable="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">
                                                                    <asp:Panel ID="pnltextbox" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Search String</label>
                                                                        </div>
                                                                        <div id="divtxt" runat="server" style="display: none">
                                                                            <asp:TextBox ID="txtSearchPanel" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                                                        </div>
                                                                    </asp:Panel>

                                                                    <asp:Panel ID="pnlDropdown" runat="server">
                                                                        <div id="divDropDown" runat="server" style="display: none">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <%-- <label id="lblDropdown"></label>--%>
                                                                                <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                                                            </div>
                                                                            <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                                <%--   <div class="col-12 btn-footer">--%>
                                                                <div class="form-group col-lg-3 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label></label>
                                                                    </div>
                                                                    <asp:Button ID="btnSearchPanel" runat="server" Text="Search" CausesValidation="false" CssClass="btn btn-primary" OnClick="btnSearchPanel_Click" />
                                                                    <%--                                       <asp:Button ID="btnClose" runat="server" Text="Clear Search" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />--%>
                                                                    <asp:Button ID="btnClosePanel" runat="server" Text="Clear" CausesValidation="false" OnClick="btnClosePanel_Click"
                                                                        CssClass="btn btn-warning" TabIndex="4" />

                                                                    <asp:ValidationSummary ID="ValidationSummary2" DisplayMode="List" runat="server" ShowMessageBox="true"
                                                                        ShowSummary="false" ValidationGroup="submit" />
                                                                </div>
                                                            </div>
                                                            <div id="divfooter" runat="server" visible="false">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Payment for Semester</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="DropDownList1" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                                                            CssClass="form-control" Enabled="true" TabIndex="2">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <%# Eval("DD_NO") %>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSemester"
                                                                            Display="None" InitialValue="0" ErrorMessage="Please select semester" SetFocusOnError="true"
                                                                            ValidationGroup="studSearch" />
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Select Receipt Type</label>
                                                                        </div>
                                                                        <%--  OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged" AutoPostBack="true"--%>
                                                                        <asp:DropDownList ID="ddlReceiptType" runat="server" CssClass="form-control" TabIndex="3" AppendDataBoundItems="true" AutoPostBack="true" />
                                                                    </div>


                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Hosteller Status</label>
                                                                        </div>
                                                                        <%--  OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged" AutoPostBack="true"--%>
                                                                        <asp:DropDownList ID="ddlhostelerstatus" runat="server" CssClass="form-control" TabIndex="3" AppendDataBoundItems="true" AutoPostBack="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                            <asp:ListItem Value="0">NO</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Transportation Status</label>
                                                                        </div>
                                                                        <%--  OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged" AutoPostBack="true"--%>
                                                                        <asp:DropDownList ID="ddltransportStatus" runat="server" CssClass="form-control" TabIndex="3" AppendDataBoundItems="true" AutoPostBack="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                            <asp:ListItem Value="0">NO</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>


                                                        </div>
                                                    </div>


                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnsubmit1" runat="server" Text="Submit" OnClick="btnsubmit1_Click" Visible="false"
                                                            ToolTip="Shows Students under Selected Criteria." CssClass="btn btn-primary" />

                                                    </div>

                                                    <div class="col-12">
                                                        <asp:Panel ID="pnlLV" runat="server">
                                                            <asp:ListView ID="lvStudentPanel" runat="server">
                                                                <LayoutTemplate>
                                                                    <div id="listViewGrid" class="vista-grid">
                                                                        <div class="sub-heading">
                                                                            <h5>Student List</h5>
                                                                        </div>
                                                                        <asp:Panel ID="pnlstudlist" runat="server">
                                                                            <table class="table table-striped table-bordered" style="width: 100%" id="">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th>Name
                                                                                        </th>
                                                                                        <%--   <th>IdNo
                                                                                    </th>--%>
                                                                                        <th>
                                                                                            <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                                        </th>
                                                                                        <th>Receipt Type
                                                                                        </th>
                                                                                        <th>Balance
                                                                                        </th>
                                                                                        <th>Father Name
                                                                                        </th>
                                                                                        <th>Mother Name
                                                                                        </th>
                                                                                        <th>Mobile No.
                                                                                        </th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                </tbody>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </div>

                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkIdPanel" CausesValidation="false" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                                OnClick="lnkIdPanel_Click"></asp:LinkButton>
                                                                        </td>
                                                                        <%--<td>
                                                                        <%# Eval("idno")%>
                                                                    </td>--%>
                                                                        <td>
                                                                            <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTERNAME")%>' ToolTip='<%# Eval("SEMESTERNO")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblReceipttype" runat="server" Text='<%# Eval("RECIEPT_TITLE")%>' ToolTip='<%# Eval("RECIEPT_CODE")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("BALANCE")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("FATHERNAME") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("MOTHERNAME") %>
                                                                        </td>
                                                                        <td>
                                                                            <%#Eval("STUDENTMOBILE") %>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                        <layouttemplate>
                                    <itemtemplate>
                                        <div class="col-12 btn-footer">
                                                                <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                                            </div>
                                                <%--<tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkIdPanel" CausesValidation="false" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                            OnClick="lnkIdPanel_Click"></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <%# Eval("idno")%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstudsemester" runat="server" Text='<%# Eval("SEMESTERNAME")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("RECIEPT_TITLE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("BALANCE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("FATHERNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("MOTHERNAME") %>
                                                    </td>
                                                    <td>
                                                        <%#Eval("STUDENTMOBILE") %>
                                                    </td>
                                                </tr>
                                            </itemtemplate>
                                         </layouttemplate>
                                            </asp:ListView>
                        </asp:Panel>--%>
                                                    </div>

                                                    <div class="col-12">
                                                        <asp:Panel ID="Panel3" runat="server">

                                                            <asp:ListView ID="lvStudlist" runat="server" OnItemDataBound="lvStudlist_ItemDataBound">
                                                                <LayoutTemplate>

                                                                    <div class="sub-heading">
                                                                        <h5>Student Details</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblStudent">
                                                                        <thead class="bg-light-blue">
                                                                            <tr id="Tr1" runat="server">
                                                                                <th>
                                                                                    <%--<asp:CheckBox ID="chkIdentityCard1" runat="server" onClick="SelectAll(this);" ToolTip="Select or Deselect All Records" Visible="true" />--%>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYtxtRegNo1" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>Roll No.</th>
                                                                                <th>Student Name
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>Transportation Status</th>
                                                                                <th>Hosteler Status</th>
                                                                                <th>Hosteler Type</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </LayoutTemplate>

                                                                <ItemTemplate>
                                                                    <%-- <asp:UpdatePanel ID="upd22" runat="server">
                                                                                <ContentTemplate>--%>


                                                                    <tr>
                                                                        <td>
                                                                            <asp:CheckBox ID="chkReport1" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                                            <asp:HiddenField ID="hidIdNo1" runat="server" Value='<%# Eval("IDNO") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("REGNO")%>
                                                                        </td>
                                                                        <td><%# Eval("ROLLNO")%></td>
                                                                        <td>
                                                                            <%# Eval("STUDNAME")%>
                                                                            <asp:HiddenField ID="hdfAppliid1" runat="server" Value='<%# Eval("STUDNAME") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SEMESTERNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlTransport1" runat="server" TabIndex="27" AppendDataBoundItems="true"   SelectedValue='<%# Eval("STUD_TRANS_STATUS") %>'
                                                                                ToolTip="Please Select Transportation Status">
                                                                                <asp:ListItem Value="0">Please select</asp:ListItem>
                                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                <asp:ListItem Value="2">No</asp:ListItem>

                                                                            </asp:DropDownList>
                                                                            <asp:HiddenField ID="hdTRANSTATUSNO1" runat="server" Value='<%# Eval("TRANSTATUSNO") %>' />
                                                                        </td>

                                                                        <td>
                                                                            <%--<asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>--%>
                                                                            <asp:DropDownList ID="ddlHosteller1" runat="server" TabIndex="27" AppendDataBoundItems="true" SelectedValue='<%# Eval("HOSTELER") %>'
                                                                                ToolTip="Please Select Hosteler Status">
                                                                                <asp:ListItem Value="0">Please select</asp:ListItem>
                                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                <asp:ListItem Value="2">No</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <%--</ContentTemplate>
                                                                                            </asp:UpdatePanel>--%>

                                                                        </td>

                                                                        <td>
                                                                            <asp:DropDownList ID="ddlHostellerType1" runat="server" TabIndex="27" AppendDataBoundItems="true"
                                                                                ToolTip="Please Select Hosteler Status">
                                                                                <asp:ListItem Value="0">Please select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <%--</ContentTemplate>
                                                                            </asp:UpdatePanel>--%>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>

                                                  <div class="col-12 btn-footer">
                                                                <asp:HiddenField ID="hftot1" runat="server" />
                                                                <asp:HiddenField ID="txtTotStud1" runat="server" />
                                                            </div>
                                            </div>
                                        </div>
                                                </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnSearchPanel" />
                                                <asp:AsyncPostBackTrigger ControlID="btnsubmit1" />
                                                <asp:AsyncPostBackTrigger ControlID="lvStudlist" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlSearchPanel" />  
                                                <asp:AsyncPostBackTrigger ControlID="lvStudlist" />
                                            </Triggers>
                                            </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane active" id="tab_1">
                                        <asp:UpdatePanel ID="updReprint" runat="server">
                                            <ContentTemplate>
                                        <div>
                                            <asp:UpdateProgress ID="UpdprogReprint" runat="server" AssociatedUpdatePanelID="updReprint"
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
                                                    <div class="col-12">
                                                        <div class="row">


                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Institute Name</label>--%>
                                                                    <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="True" TabIndex="1" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged"
                                                                    CssClass="form-control" data-select2-enable="true" AutoPostBack="True" ToolTip="Please Select Institute">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvColg" runat="server" ControlToValidate="ddlColg" SetFocusOnError="true"
                                                                    Display="None" ErrorMessage="Please Select Institute" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>

                                                            </div>


                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Admission Batch</label>--%>
                                                                    <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlAdmbatch" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                                    AppendDataBoundItems="True" ToolTip="Please Select Admbatch"
                                                                    OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmbatch" SetFocusOnError="true"
                                                                    Display="None" ErrorMessage="Please Select Admbatch" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Degree</label>--%>
                                                                    <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                                                    OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" ToolTip="Please Select Degree" AutoPostBack="True">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" SetFocusOnError="true"
                                                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Branch</label>--%>
                                                                    <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" TabIndex="4" CssClass="form-control" data-select2-enable="true"
                                                                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ToolTip="Please Select Branch" AutoPostBack="True">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" SetFocusOnError="true"
                                                                    Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%-- <label>Admission Year</label>--%>
                                                                    <asp:Label ID="lblDYtxtAdmYear" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlYear" runat="server" TabIndex="5" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                                    ToolTip="Please Select Admission Year" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" />
                                                                <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                                                                    Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Admission Year"
                                                                    ValidationGroup="show"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--   <label>Semester</label>--%>
                                                                    <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Semester" TabIndex="6" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester" SetFocusOnError="true"
                                                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label></label>
                                                                </div>
                                                                <asp:RadioButtonList ID="rbRegEx" runat="server" RepeatDirection="Horizontal" TabIndex="777" AutoPostBack="true" OnSelectedIndexChanged="rbRegEx_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0" Selected="True">&nbsp;Regular Student &nbsp;&nbsp;</asp:ListItem>
                                                                    <asp:ListItem Value="1">&nbsp;Ex Student</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                            <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                                                </div>--%>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divtransport" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Transportation Status</label>
                                                                </div>
                                                                <%--  OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged" AutoPostBack="true"--%>
                                                                <asp:DropDownList ID="ddtransportstatus" runat="server" CssClass="form-control" TabIndex="3" AppendDataBoundItems="true" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddtransportstatus"
                                                                    Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Transport Status"
                                                                    ValidationGroup="show"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divhostel" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Hosteller Status</label>
                                                                </div>
                                                                <%--  OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged" AutoPostBack="true"--%>
                                                                <asp:DropDownList ID="ddlhosteller" runat="server" CssClass="form-control" TabIndex="3" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlhosteller_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                                </asp:DropDownList>
                                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlhosteller"
                                                                    Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Hosteller Status"
                                                                    ValidationGroup="show"></asp:RequiredFieldValidator>
                                                            </div>
                                                            

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divhostelerType" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <%--<sup>* </sup>--%>
                                                                    <label>Hosteller Type</label>
                                                                </div>
                                                                <%--  OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged" AutoPostBack="true"--%>
                                                                <asp:DropDownList ID="ddlHosteltypes" runat="server" CssClass="form-control" TabIndex="3" AppendDataBoundItems="true" data-select2-enable="true" >
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <%--<asp:ListItem Value="1">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="0">No</asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                                 <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlHosteltypes"
                                                                    Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Admission Year"
                                                                    ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            
                                                        </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:Button ID="btnShow" runat="server" Text="Show Student" OnClick="btnShow_Click" TabIndex="7"
                                                                    ToolTip="Shows Students under Selected Criteria." ValidationGroup="show" CssClass="btn btn-primary" />

                                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="8"
                                                                    ValidationGroup="show" CssClass="btn btn-primary" />

                                                                <asp:Button ID="btnLock" runat="server" Text="Lock" OnClick="btnLock_Click" TabIndex="9" Visible="false"
                                                                    ValidationGroup="show" CssClass="btn btn-primary" />

                                                                <asp:Button ID="btnUnlock" runat="server" Text="Unlock" OnClick="btnUnlock_Click" TabIndex="10" Visible="false"
                                                                    ValidationGroup="show" CssClass="btn btn-primary" />

                                                                <asp:Button ID="btnPrintReport" runat="server" Text="Admit Card" TabIndex="999" CssClass="btn btn-info"
                                                                    OnClick="btnPrintReport_Click" ToolTip="Print Card under Selected Criteria." ValidationGroup="show" Visible="false" />

                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="11"
                                                                    ToolTip="Cancel Selected under Selected Criteria." CssClass="btn btn-warning" />
                                                                 
                                                            </div>






                                                            <div class="col-12 btn-footer">
                                                                <asp:HiddenField ID="hftot" runat="server" />
                                                                <asp:HiddenField ID="txtTotStud" runat="server" />
                                                            </div>


                                                            <div class="col-12">
                                                                <asp:Panel ID="Panel1" runat="server">

                                                                    <asp:ListView ID="lvStudentRecords" runat="server" OnItemDataBound="lvStudentRecords_ItemDataBound">
                                                                        <LayoutTemplate>

                                                                            <div class="sub-heading">
                                                                                <h5>Student List</h5>
                                                                            </div>
                                                                            <table class="table table-striped table-bordered" style="width: 100%" id="tblStudent">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr id="Tr1" runat="server">
                                                                                        <th>
                                                                                            <asp:CheckBox ID="chkIdentityCard" runat="server" onClick="SelectAll(this);" ToolTip="Select or Deselect All Records" Visible="true" />
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                                        </th>
                                                                                        <th>Roll No.</th>
                                                                                        <th>Student Name
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                                                        </th>
                                                                                        <th>
                                                                                            Transportation Status
                                                                                        </th>
                                                                                        <th>
                                                                                            Hosteller Status
                                                                                        </th>
                                                                                        <th>
                                                                                            Hosteller Type
                                                                                        </th>
                                                                                        <th style="display:none">Transportation Status</th>
                                                                                        <th style="display:none">Hosteler Status</th>
                                                                                        <th style="display:none">Hosteler Type</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                </tbody>
                                                                            </table>
                                                                        </LayoutTemplate>

                                                                        <ItemTemplate>
                                                                            <asp:UpdatePanel ID="upd22" runat="server">
                                                                                <ContentTemplate>


                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkReport" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                                                            <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                                                        </td>
                                                                                        <td>
                                                                                            <%# Eval("REGNO")%>
                                                                                        </td>
                                                                                        <td><%# Eval("ROLLNO")%></td>
                                                                                        <td>
                                                                                            <%# Eval("STUDNAME")%>
                                                                                            <asp:HiddenField ID="hdfAppliid" runat="server" Value='<%# Eval("STUDNAME") %>' />
                                                                                        </td>
                                                                                        <td>
                                                                                            <%# Eval("SEMESTERNAME")%>
                                                                                        </td>
                                                                                        <td>
                                                                                             <%# Eval("TRANSPORT")%>  
                                                                                        </td>
                                                                                        <td>
                                                                                             <%# Eval("HOSTELER1")%>
                                                                                        </td>
                                                                                        <td>
                                                                                            <%# Eval("HOSTELER_STATUS")%>
                                                                                        </td>
                                                                                        <td style="display:none">
                                                                                            <asp:DropDownList ID="ddlTransport" runat="server" TabIndex="27" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlTransport_SelectedIndexChanged" SelectedValue='<%# Eval("STUD_TRANS_STATUS") %>'
                                                                                                ToolTip="Please Select Transportation Status">
                                                                                                <asp:ListItem Value="-1">Please select</asp:ListItem>
                                                                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                                <asp:ListItem Value="0">No</asp:ListItem>

                                                                                            </asp:DropDownList>
                                                                                            <asp:HiddenField ID="hdTRANSTATUSNO" runat="server" Value='<%# Eval("TRANSTATUSNO") %>' />
                                                                                        </td>

                                                                                        <td style="display:none">
                                                                                            <asp:UpdatePanel ID="upd11" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                                                                                    <asp:DropDownList ID="ddlHosteller" OnSelectedIndexChanged="ddlHosteller_SelectedIndexChanged" AutoPostBack="true" runat="server" TabIndex="27" AppendDataBoundItems="true" SelectedValue='<%# Eval("HOSTELER") %>'
                                                                                                        ToolTip="Please Select Hosteler Status">
                                                                                                        <asp:ListItem Value="-1">Please select</asp:ListItem>
                                                                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>

                                                                                        </td>

                                                                                        <td style="display:none">
                                                                                            <asp:DropDownList ID="ddlHostellerType" runat="server" TabIndex="27" AppendDataBoundItems="true"
                                                                                                ToolTip="Please Select Hosteler Status">
                                                                                                <asp:ListItem Value="0">Please select</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>

                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="show" DisplayMode="List" />
                                                            <div id="divMsg" runat="server">
                                                            </div>



                                                        </div>
                                                    </div>


                                                </div>
                                            </ContentTemplate>
                                            
                                        </asp:UpdatePanel>                                        
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="div1" runat="server"></div>

                    </div>
                </div>
            </div>
            <%--</div>--%>
        </ContentTemplate>

        <Triggers>
         <%--   <asp:AsyncPostBackTrigger ControlID="btnCancel" />--%>
                      <%--<asp:AsyncPostBackTrigger ControlID="ddlSearchPanel" />--%>

         <%--   <asp:PostBackTrigger ControlID="btnExcelReport" />--%>

        </Triggers>
    </asp:UpdatePanel>

    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>


     <script type="text/javascript">
         function SelectAll(chk) {
             var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>');

            for (i = 0; i < hftot.value; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_chkReport');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        lst.checked = true;

                        //document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoYes').disabled = false;
                        //document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoNo').disabled = false;
                    }
                    else {
                        lst.checked = false;


                        //document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoYes').disabled = true;
                        //document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoNo').disabled = true;

                        //$('#ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoYes').prop('checked', false);
                        //$('#ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoNo').prop('checked', false);
                    }
                }

            }
        }
    </script>

  <%--  <script type="text/javascript">
        function SelectAll(chk) {
            var txtTot1 = document.getElementById('<%= txtTotStud1.ClientID %>');
            var hftot1 = document.getElementById('<%= hftot1.ClientID %>');

            for (i = 0; i < hftot1.value; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudlist_ctrl' + i + '_chkReport1');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        lst.checked = true;

                        //document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoYes').disabled = false;
                        //document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoNo').disabled = false;
                    }
                    else {
                        lst.checked = false;


                        //document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoYes').disabled = true;
                        //document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoNo').disabled = true;

                        //$('#ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoYes').prop('checked', false);
                        //$('#ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_rdoNo').prop('checked', false);
                    }
                }

            }
        }
    </script>--%>

    <script type="text/javascript" lang="javascript">

       <%-- $(document).ready(function () {
            debugger
            $("#<%= divpanel.ClientID %>").css("display", "none");
            $("#<%= pnltextbox.ClientID %>").css("display", "none");
            $("#<%= pnlDropdown.ClientID %>").css("display", "none");
        });--%>
        function submitPopup(btnsearch) {

            debugger
            var rbText;
            var searchtxt;

            var e = document.getElementById("<%=ddlSearchPanel.ClientID%>");
            var rbText = e.options[e.selectedIndex].text;
            var ddlname = e.options[e.selectedIndex].text;
            if (rbText == "Please Select") {
                alert('Please select Criteria as you want search...')
            }

            else {


                if (rbText == "ddl") {
                    var skillsSelect = document.getElementById("<%=ddlDropdown.ClientID%>").value;

                    var searchtxt = skillsSelect;
                    if (searchtxt == "0") {
                        alert('Please Select ' + ddlname + '..!');
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        return true;
                        $("#<%= pnltextbox.ClientID %>").hide();

                      }
                  }
                  else if (rbText == "BRANCH") {

                      if (searchtxt == "Please Select") {
                          alert('Please Select Branch..!');

                      }
                      else {
                          __doPostBack(btnsearch, rbText + ',' + searchtxt);

                          return true;
                      }

                  }
                  else {
                      searchtxt = document.getElementById('<%=txtSearchPanel.ClientID %>').value;
                    if (searchtxt == "" || searchtxt == null) {
                        alert('Please Enter Data you want to search..');
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        //$("#<%= divpanel.ClientID %>").hide();
                        //$("#<%= pnltextbox.ClientID %>").show();

                        return true;
                    }
                }
        }
    }

    function ClearSearchBox(btncancelmodal) {
        document.getElementById('<%=txtSearchPanel.ClientID %>').value = '';
        __doPostBack(btncancelmodal, '');
        return true;
    }
    function CloseSearchBox(btnClose) {
        document.getElementById('<%=txtSearchPanel.ClientID %>').value = '';
        __doPostBack(btnClose, '');
        return true;
    }




    function Validate() {

        debugger

        var rbText;

        var e = document.getElementById("<%=ddlSearchPanel.ClientID%>");
        var rbText = e.options[e.selectedIndex].text;

        if (rbText == "IDNO" || rbText == "Mobile") {

            var char = (event.which) ? event.which : event.keyCode;
            if (char >= 48 && char <= 57) {
                return true;
            }
            else {
                return false;
            }
        }
        else if (rbText == "NAME") {

            var char = (event.which) ? event.which : event.keyCode;

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122)) {
                return true;
            }
            else {
                return false;
            }

        }
    }


    </script>
</asp:Content>
