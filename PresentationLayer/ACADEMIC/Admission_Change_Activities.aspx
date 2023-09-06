<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Admission_Change_Activities.aspx.cs" Inherits="ACADEMIC_ReAdmission_Branch_Payment_Type_Change" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .radiobuttonlist {
            font: bold;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%-- <h3 class="box-title">Admission Change Activities</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <%--Search Pannel Start by Swapnil --%>
                            <div id="myModal2" role="dialog" runat="server">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updEdit"
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

                                <asp:UpdatePanel ID="updEdit" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Search Criteria</label>
                                                    </div>

                                                    <%--onchange=" return ddlSearch_change();"--%>
                                                    <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <%-- <asp:ListItem>Please Select</asp:ListItem>
                                                        <asp:ListItem>BRANCH</asp:ListItem>
                                                        <asp:ListItem>ENROLLMENT NUMBER</asp:ListItem>
                                                        <asp:ListItem>REGISTRATION NUMBER</asp:ListItem>
                                                        <asp:ListItem>FatherName</asp:ListItem>
                                                        <asp:ListItem>IDNO</asp:ListItem>
                                                        <asp:ListItem>MOBILE NUMBER</asp:ListItem>
                                                        <asp:ListItem>MotherName</asp:ListItem>
                                                        <asp:ListItem>NAME</asp:ListItem>
                                                        <asp:ListItem>ROLLNO</asp:ListItem>
                                                        <asp:ListItem>SEMESTER</asp:ListItem>--%>
                                                    </asp:DropDownList>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">
                                                    <asp:Panel ID="pnltextbox" runat="server">
                                                        <div id="divtxt" runat="server">
                                                            <div class="label-dynamic">
                                                                <label>Search String</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                                        </div>
                                                    </asp:Panel>

                                                    <asp:Panel ID="pnlDropdown" runat="server">
                                                        <div id="divDropDown" runat="server" style="display: block">
                                                            <div class="label-dynamic">
                                                                <%-- <label id="lblDropdown"></label>--%>
                                                                <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                            </asp:DropDownList>

                                                        </div>
                                                    </asp:Panel>






                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <%-- OnClientClick="return submitPopup(this.name);"--%>
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" />

                                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                            </div>

                                            <div class="col-12">
                                                <asp:Panel ID="Panel3" runat="server">
                                                    <asp:ListView ID="lvStudent" runat="server">
                                                        <LayoutTemplate>
                                                            <div>
                                                                <div class="sub-heading">
                                                                    <h5>Student List</h5>
                                                                </div>
                                                                <asp:Panel ID="Panel2" runat="server">
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Name
                                                                                </th>
                                                                                <th>IdNo
                                                                                </th>
                                                                                <th>Roll No.
                                                                                </th>
                                                                                <th>Branch
                                                                                </th>
                                                                                <th>Semester
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
                                                                </asp:Panel>
                                                            </div>

                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                        OnClick="lnkId_Click"></asp:LinkButton>
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
                                                                    <%# Eval("SEMESTERNO")%>
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
                                            </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <%--Search Pannel End--%>



                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Reg. No</label>
                                        </div>
                                        <asp:TextBox ID="txtStudent" runat="server" class="form-control" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary btn-flat" ValidationGroup="Show" />

                                        <asp:RequiredFieldValidator ID="rfvRollNumber" runat="server"
                                            ControlToValidate="txtStudent" Display="None"
                                            ErrorMessage="Please Enter Reg. No" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvRollNumber_submit" runat="server"
                                            ControlToValidate="txtStudent" Display="None"
                                            ErrorMessage="Please Enter Roll Number" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        <asp:Label ID="lblPreSession" runat="server" Font-Bold="true" Visible="false" />
                                    </div>

                                    <%--********************** Student Detals ***************************************--%>
                                    <div class="form-group col-12" id="divStudInfo" runat="server" style="display: none">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Enrollment No. :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblEnroll" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Registration No. :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblRegno" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Roll No. :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblRollno" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Student Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblStudname" Font-Bold="true" runat="server" /></a>
                                                        <asp:HiddenField ID="hdfAdmBatch" runat="server" Value="0" />

                                                    </li>
                                                    <li class="list-group-item"><b>Payment Type :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblPaymentType" CssClass="data_label" runat="server" /></a>
                                                        <asp:HiddenField ID="hdfPaymentType" runat="server" Value="0" />
                                                    </li>
                                                </ul>

                                            </div>
                                            <div class="col-md-6">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>College :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblCollege" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Degree :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblDegree" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Branch :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblBranch" Font-Bold="true" runat="server" /></a>
                                                        <asp:HiddenField ID="hdfbranch" runat="server" Value="0" />
                                                    </li>
                                                    <li class="list-group-item"><b>Semester :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSemester" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item" id="litotalfee"><b>Total Applied Amount :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblAppliedAmount" Font-Bold="true" runat="server" />
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Total Paid Amount :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblPaidAmount" Font-Bold="true" runat="server" />
                                                        </a>
                                                    </li>
                                                </ul>

                                            </div>
                                        </div>
                                    </div>

                                    <asp:HiddenField ID="hdfOrganization" runat="server" />
                                    <%--******************************* End *****************************************--%>

                                    <div class="form-group col-12">

                                        <div class="row">
                                            <div class="form-group col-lg-12 col-md-6 col-12" id="divSelection" runat="server" visible="false">

                                                <asp:RadioButtonList ID="rblSelection" runat="server" AppendDataBoundItems="true" class="radiobuttonlist" AutoPostBack="true"
                                                    RepeatDirection="Horizontal" onchange="rblSelection_change();" OnSelectedIndexChanged="rblSelection_SelectedIndexChanged" Width="100%">
                                                    <asp:ListItem Value="1"><span style="font-size: 13px;font-weight:bold"> Readmission with Branch change and Payment type modification </span></asp:ListItem>
                                                    <asp:ListItem Value="2"><span style="font-size: 13px;font-weight:bold"> Only Branch Change</span></asp:ListItem>
                                                    <asp:ListItem Value="3"><span style="font-size: 13px;font-weight:bold"> Payment Type Modification</span></asp:ListItem>
                                                    <asp:ListItem Value="4"><span style="font-size: 13px;font-weight:bold"> Readmission in same branch</span></asp:ListItem>

                                                </asp:RadioButtonList>
                                                <asp:HiddenField ID="hdfPaidAmount" Value="0" runat="server" />

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group col-12" runat="server" id="divPaymentchangeWithBrach" style="margin-left: 230px;">
                                        <asp:RadioButtonList ID="rblWithBranch" runat="server" CssClass="radiobuttonlist col-4" RepeatDirection="Horizontal" RepeatColumns="2" Style="margin-left: 230px; display: none;" AutoPostBack="true"
                                            OnSelectedIndexChanged="rblWithBranch_SelectedIndexChanged" onchange="rblWithBranch_change();">
                                            <asp:ListItem Value="1"><span style="font-size: 13px;font-weight:bold">With Branch Change</span></asp:ListItem>
                                            <asp:ListItem Value="2"><span style="font-size: 13px;font-weight:bold">Without Branch Change</span></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divNewCollege" runat="server" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>New College</label>
                                        </div>
                                        <asp:DropDownList ID="ddlNewCollege" runat="server" AppendDataBoundItems="true" data-select2-enable="true" CssClass="form-control" OnSelectedIndexChanged="ddlNewCollege_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divNewDegree" runat="server" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>New Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlNewDegree" runat="server" AppendDataBoundItems="true" data-select2-enable="true" CssClass="form-control" OnSelectedIndexChanged="ddlNewDegree_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divNewBranch" runat="server" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>New Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlNewBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CausesValidation="false" data-select2-enable="true"
                                            CssClass="form-control" OnSelectedIndexChanged="ddlNewBranch_SelectedIndexChanged" onchange="return confirmmsgbranch();">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12" id="DivScheme" runat="server" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" data-select2-enable="true" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12" id="DivSemester" runat="server" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" data-select2-enable="true" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12" id="DivAdmBatch" runat="server" style="display: none;">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" data-select2-enable="true" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                   <div class="form-group col-lg-3 col-md-3 col-12" id="divregno" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Generate New Reg No </label>
                                            </div>
                                            <asp:CheckBox ID="chkRegno" runat="server" />                                        
                                        </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divNewPaymentype" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>New Payment Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlNewPaymentType" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlNewPaymentType_SelectedIndexChanged" data-select2-enable="true" onchange="return listvalidation();"
                                            CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divHeadchange" visible="false" style="display: none">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label></label>
                                        </div>
                                        <asp:CheckBox ID="chkHeadChange" Text="Fee Head Change" runat="server" onclick="change_feehead();" />
                                    </div>

                                    <div class="form-group col-lg-8 col-md-6 col-12" runat="server" id="divNewDemandAMounts" visible="false" style="display: none">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label></label>
                                        </div>
                                        <label style="font: bold">New Demand Total Amount : </label>
                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="lblNewTotal_Amount" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                <label style="font: bold">New Demand Paid Amount : </label>
                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="lblNewPaid_Amount" runat="server"></asp:Label>
                                    </div>

                                    <div class="col-12" id="divFeeItems" runat="server" style="display: none">
                                        <div class="row">
                                            <div class="col-6">
                                                <label style="font: bold">Total Amount : </label>
                                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lbllvTotal_Amount" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                <label style="font: bold">Paid Amount : </label>
                                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lbllvPaid_Amount" runat="server"></asp:Label>
                                                <asp:ListView ID="lvFeeItems" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="divlvFeeItems">
                                                            <span style="font: bold">Available Fee Items</span>
                                                            <table id="tblFeeItems" runat="server" class="table table-hover table-bordered">
                                                                <tr class="bg-light-blue">
                                                                    <th>Fee Heads
                                                                    </th>
                                                                    <th>Applied Amount
                                                                    </th>
                                                                    <th>Paid Amount
                                                                    </th>
                                                                </tr>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%# Eval("FEE_LONGNAME")%>
                                                                <asp:HiddenField ID="hdnfld_FEE_LONGNAME" runat="server" Value='<%# Eval("FEE_LONGNAME")%>' />
                                                                <asp:Label ID="lblFeeHeadSrNo" runat="server" Text='<%# Eval("SRNO") %>' Visible="false" />
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lblDAmount" runat="server" Text='<%# Eval("APPLIED_AMOUNT")%>'></asp:Label>

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("PAID_AMOUNT")%>'></asp:Label>
                                                                <asp:HiddenField ID="hidFeeItemAmount" runat="server" Value='<%# Eval("PAID_AMOUNT") %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>

                                            <div class="col-6">
                                                <%-- <legend>Fee Items</legend>--%>
                                                <asp:Label ID="lblTotal_Fee" runat="server" Font-Bold="true" Text=" New Demand Total Amount :"> </asp:Label>
                                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lbllvChngetotal_amount" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblPaid_Fee" runat="server" Text="New Demand Paid Amount :" Font-Bold="true"> </asp:Label>
                                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lbllvchangepaid_amount" runat="server"></asp:Label>
                                                <asp:ListView ID="lvFeeItemsChanges" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="divlvFeeItems">

                                                            <span>Fee Head Change</span>

                                                            <table id="tblHeadChangesItems" runat="server" class="table table-hover table-bordered">
                                                                <tr class="bg-light-blue">
                                                                    <th>Fee Heads
                                                                    </th>
                                                                    <th>Applied Amount
                                                                    </th>
                                                                    <th>Paid Amount
                                                                    </th>
                                                                </tr>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%# Eval("FEE_LONGNAME")%>
                                                                <asp:HiddenField ID="hdnfld_FEE_LONGNAME" runat="server" Value='<%# Eval("FEE_LONGNAME")%>' />
                                                                <asp:Label ID="lblFeeHeadSrNo" runat="server" Text='<%# Eval("SRNO") %>' Visible="false" />
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lblDAmount" runat="server" Text='<%# Eval("APPLIED_AMOUNT")%>'></asp:Label>
                                                                <asp:HiddenField ID="hidAmount1" runat="server" Value='<%# Eval("APPLIED_AMOUNT")%>'></asp:HiddenField>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtChangeHeadAmount" runat="server" Text='<%# Eval("PAID_AMOUNT")%>' Enabled='<%# Eval("FEE_LONGNAME").ToString()=="Tuition Fee Paid By Govt" ? false : true %>' MaxLength="7" onchange="return amount_validation(this);"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftext" runat="server" TargetControlID="txtChangeHeadAmount" FilterMode="ValidChars" ValidChars="0123456789.">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                <asp:HiddenField ID="hidFeeItemAmount" runat="server" Value='<%# Eval("PAID_AMOUNT") %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                    </div>


                                    <div style="text-align: center; display: none" class="from-group col-12" id="divButtons" runat="server">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return validation();" CssClass="btn btn-success" />
                                        <asp:Button ID="btnReport" runat="server" Text="Print Receipt" CssClass="btn btn-info" OnClick="btnReport_Click" Enabled="false" />
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-danger" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-12">
                            <asp:Label ID="lblNote" Font-Bold="true" Visible="false" Text="Note: Please do not refresh page Or Do not search new student once you processed demand for current student." Style="color: red;" runat="server" SkinID="lblmsg"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--   <asp:PostBackTrigger ControlID="ddlNewBranch" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <%-- <asp:Button ID="btnPrint" runat="server" Text="Print Registration Slip" Width="180px"
                                                ValidationGroup="Show" OnClick="btnPrint_Click" />--%>
    <div id="divMsg" runat="server" />

    <%--Search Box Script Start--%>
    <script type="text/javascript" lang="javascript">

        $(document).ready(function () {
            debugger
            // $("#<%= pnltextbox.ClientID %>").hide();

            $("#<%= pnlDropdown.ClientID %>").hide();
        });
        function submitPopup(btnsearch) {

            debugger
            var rbText;
            var searchtxt;

            var e = document.getElementById("<%=ddlSearch.ClientID%>");
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
                        //$("#<%= divpanel.ClientID %>").hide();
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
                    searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;
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
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btncancelmodal, '');
        return true;
    }
    function CloseSearchBox(btnClose) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btnClose, '');
        return true;
    }




    function Validate() {

        debugger

        var rbText;

        var e = document.getElementById("<%=ddlSearch.ClientID%>");
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
    <%--Search Box Script End--%>

    <script type="text/javascript">
        function ShowConfirm(yourDropDown) {
            //If this is true - Perform your PostBack manually
            //if (confirm("New demand will be create for selected branch. Are you sure?")) {
            //    __doPostBack('ddlBranch', '');
            //    return true;
            //}
            //    //Otherwise the user selected "Cancel" - revert the selection and don't PostBack
            //else {

            //    yourDropDown.selectedIndex = 0;//(yourDropDown.value == "No") ? 0 : 1;
            //    return false;
            //}
        }

        function RollBackConfirmation() {
            return confirm("ALERT: Created new Programme/Branch demand will be RollBack. Are you sure?");
        }


    </script>
    <script type="text/javascript">
        // function ValidateUploadFile(ID) {
        //debugger;
        // var maxFileSize = 1000000;//4MB -> 4 * 1024 * 1024

        //var fileUpload = document.getElementById(<%--= fuFile.ClientID--%>);

        //if(fileUpload[0].files[0].size<=maxFileSize)

        //  return true;
        //}else{

        //    alert('File Size Required Between 0 kb - 100 kb!!')
        //   return false;
        //}

        //}
    </script>
    <script type="text/javascript">


        function rblSelection_change() {
            var radioValue = $('#<%=rblSelection.ClientID %> input[type=radio]:checked').val();
            var withbranch = $('#<%=rblWithBranch.ClientID %> input[type=radio]:checked').val();
            var paid_amount = $("#ctl00_ContentPlaceHolder1_hdfPaidAmount").val();
            var orgid = '<%=Session["OrgId"]%>';
            if (radioValue == "1") {
                $("#ctl00_ContentPlaceHolder1_divNewPaymentype").show();
                $("#ctl00_ContentPlaceHolder1_divNewCollege").show();
                $("#ctl00_ContentPlaceHolder1_divNewDegree").show();
                $("#ctl00_ContentPlaceHolder1_divNewBranch").show();
                $("#ctl00_ContentPlaceHolder1_rblWithBranch").hide();
                $("#ctl00_ContentPlaceHolder1_divFeeItems").hide();

                if (parseFloat(paid_amount) > 0 )
                    $('#ctl00_ContentPlaceHolder1_divHeadchange').show();
                else
                    $('#ctl00_ContentPlaceHolder1_divHeadchange').hide();
                $('#ctl00_ContentPlaceHolder1_divButtons').show();
                this.change_feehead();
            }
            else if (radioValue == "4") {
                $("#ctl00_ContentPlaceHolder1_DivScheme").show();
                $("#ctl00_ContentPlaceHolder1_DivSemester").show();
                $("#ctl00_ContentPlaceHolder1_DivAdmBatch").show();
                $('#ctl00_ContentPlaceHolder1_divButtons').show();
            }
            else if (radioValue == "2" ) {
                $("#ctl00_ContentPlaceHolder1_divNewCollege").show();
                $("#ctl00_ContentPlaceHolder1_divNewDegree").show();
                $("#ctl00_ContentPlaceHolder1_divNewBranch").show();
                $("#ctl00_ContentPlaceHolder1_divNewPaymentype").hide();
                $("#ctl00_ContentPlaceHolder1_rblWithBranch").hide();
                $("#ctl00_ContentPlaceHolder1_divFeeItems").hide();
                $('#ctl00_ContentPlaceHolder1_divHeadchange').hide();
                $('#ctl00_ContentPlaceHolder1_divButtons').show();
            }
            else if (radioValue == "3") {
                $("#ctl00_ContentPlaceHolder1_rblWithBranch").show();
                $("ctl00_ContentPlaceHolder1_divPaymentchangeWithBrach").show();
                if (withbranch != '1' && withbranch != '2') {
                    $("#ctl00_ContentPlaceHolder1_divNewPaymentype").hide();
                    $("#ctl00_ContentPlaceHolder1_divNewCollege").hide();
                    $("#ctl00_ContentPlaceHolder1_divNewDegree").hide();
                    $("#ctl00_ContentPlaceHolder1_divNewBranch").hide();
                    $("#ctl00_ContentPlaceHolder1_divFeeItems").hide();
                    $('#ctl00_ContentPlaceHolder1_divHeadchange').hide();
                    $('#ctl00_ContentPlaceHolder1_divButtons').hide();
                    $("#ctl00_ContentPlaceHolder1_DivScheme").hide();
                    $("#ctl00_ContentPlaceHolder1_DivSemester").hide();
                    $("#ctl00_ContentPlaceHolder1_DivAdmBatch").hide();

                }
                else if (withbranch == '1') {
                    $("#ctl00_ContentPlaceHolder1_divNewPaymentype").show();
                    $("#ctl00_ContentPlaceHolder1_divNewCollege").show();
                    $("#ctl00_ContentPlaceHolder1_divNewDegree").show();
                    $("#ctl00_ContentPlaceHolder1_divNewBranch").show();
                    //$("#ctl00_ContentPlaceHolder1_divFeeItems").show();
                    if (parseFloat(paid_amount) > 0)
                        $('#ctl00_ContentPlaceHolder1_divHeadchange').show();
                    else
                        $('#ctl00_ContentPlaceHolder1_divHeadchange').hide();
                    $('#ctl00_ContentPlaceHolder1_divButtons').show();
                    this.change_feehead();
                }
                else if (withbranch == '2') {
                    $("#ctl00_ContentPlaceHolder1_divNewPaymentype").show();
                    $("#ctl00_ContentPlaceHolder1_divNewCollege").hide();
                    $("#ctl00_ContentPlaceHolder1_divNewDegree").hide();
                    $("#ctl00_ContentPlaceHolder1_divNewBranch").hide();
                    //$("#ctl00_ContentPlaceHolder1_divFeeItems").show();
                    if (parseFloat(paid_amount) > 0)
                        $('#ctl00_ContentPlaceHolder1_divHeadchange').show();
                    else
                        $('#ctl00_ContentPlaceHolder1_divHeadchange').hide();
                    $('#ctl00_ContentPlaceHolder1_divButtons').show();
                    this.change_feehead();
                }
            }
        }

        function rblWithBranch_change() {
            var radioValue = $('#<%=rblWithBranch.ClientID %> input[type=radio]:checked').val();
            var paid_amount = $("#ctl00_ContentPlaceHolder1_hdfPaidAmount").val();
            var orgid = '<%=Session["OrgId"]%>';
            if (radioValue == "1") {
                $("#ctl00_ContentPlaceHolder1_rblWithBranch").show();
                $("#ctl00_ContentPlaceHolder1_divNewCollege").show();
                $("#ctl00_ContentPlaceHolder1_divNewDegree").show();
                $("#ctl00_ContentPlaceHolder1_divNewBranch").show();
                $("#ctl00_ContentPlaceHolder1_divNewPaymentype").show();
                //$("#ctl00_ContentPlaceHolder1_divFeeItems").show();
                if (parseFloat(paid_amount) > 0)
                    $('#ctl00_ContentPlaceHolder1_divHeadchange').show();
                else
                    $('#ctl00_ContentPlaceHolder1_divHeadchange').hide();
                $('#ctl00_ContentPlaceHolder1_divButtons').show();
                this.change_feehead();

            }
            else if (radioValue == "2") {
                $("#ctl00_ContentPlaceHolder1_rblWithBranch").show();
                $("#ctl00_ContentPlaceHolder1_divNewPaymentype").show();
                $("#ctl00_ContentPlaceHolder1_divNewCollege").hide();
                $("#ctl00_ContentPlaceHolder1_divNewDegree").hide();
                $("#ctl00_ContentPlaceHolder1_divNewBranch").hide();
                //$("#ctl00_ContentPlaceHolder1_divFeeItems").show();
                if (parseFloat(paid_amount) > 0)
                    $('#ctl00_ContentPlaceHolder1_divHeadchange').show();
                else
                    $('#ctl00_ContentPlaceHolder1_divHeadchange').hide();
                $('#ctl00_ContentPlaceHolder1_divButtons').show();
                $("#lipaidfee").show();
                $("#litotalfee").show();
                this.change_feehead();
            }
        }

        function change_feehead() {
            var chk = document.getElementById('<%=chkHeadChange.ClientID%>');
            if (chk.type = 'checkbox') {
                if (chk.checked) {
                    //var chk = $('#ctl00_ContentPlaceHolder1_chkHeadChange input[type=checkbox]:checked');
                    //if (chk.type=='checkbox' && chk.checked) {
                    $("#ctl00_ContentPlaceHolder1_divFeeItems").show();
                    $("#ctl00_ContentPlaceHolder1_divNewDemandAMounts").hide();
                }
                else {
                    $("#ctl00_ContentPlaceHolder1_divFeeItems").hide();
                    $("#ctl00_ContentPlaceHolder1_divNewDemandAMounts").show();
                }
            }

        }

        function amount_validation(txt) {
            var myarray = new Array();
            var id = "" + txt.id + "";
            var myarray = id.split('_');
            var index = myarray[3];
            var applied_amount = $("#ctl00_ContentPlaceHolder1_lvFeeItemsChanges_" + index + "_hidAmount1").val();
            var fee_headname = $("#ctl00_ContentPlaceHolder1_lvFeeItemsChanges_" + index + "_hdnfld_FEE_LONGNAME").val();
            var amount = ($("#" + txt.id).val());
            if (parseFloat(applied_amount) < parseFloat(amount)) {
                alert("Change Amount should be less than or  equal to " + fee_headname + " amount.");
                $("#" + txt.id).val('');
                return false;
            }
            else
                return true;

        }

        function validation() {
            var radioValue = $('#<%=rblSelection.ClientID %> input[type=radio]:checked').val();
            var withbranch = $('#<%=rblWithBranch.ClientID %> input[type=radio]:checked').val();
            var chk = document.getElementById('<%=chkHeadChange.ClientID%>');
            var error = false;
            if (radioValue == "1") {
                if ($("#ctl00_ContentPlaceHolder1_ddlNewCollege").val() == "0") {
                    alert("Please select College.");
                    error = true;
                    return false;
                }
                else if ($("#ctl00_ContentPlaceHolder1_ddlNewDegree").val() == "0") {
                    alert("Please select Degree.");
                    error = true;
                    return false;
                }
                else if ($("#ctl00_ContentPlaceHolder1_ddlNewBranch").val() == "0") {
                    alert("Please select Branch.");
                    error = true;
                    return false;
                }
                else if ($("#ctl00_ContentPlaceHolder1_ddlNewPaymentType").val() == "0") {
                    alert("Please select Payment Type.");
                    error = true;
                    return false;
                }
                else if (error == false && chk.checked) {
                    this.validation_total_amount();
                }

                else if (confirm("Are you sure,You want to re-admit for selected student?"))
                    return true;
                else
                    return false;
            }
            else if (radioValue == "2") {
                if ($("#ctl00_ContentPlaceHolder1_ddlNewCollege").val() == "0") {
                    alert("Please select College.");
                    error = true;
                    return false;
                }
                else if ($("#ctl00_ContentPlaceHolder1_ddlNewDegree").val() == "0") {
                    alert("Please select Degree.");
                    error = true;
                    return false;
                }
                else if ($("#ctl00_ContentPlaceHolder1_ddlNewBranch").val() == "0") {
                    alert("Please select Branch.");
                    error = true;
                    return false;
                }
                else if (error == false && chk.checked) {
                    return this.validation_total_amount();
                }

                if (confirm("Are you sure,You want to change branch for selected student?"))
                    return true;
                else
                    return false;

            }
            else if (radioValue == "3") {
                if (withbranch == "1") {
                    if ($("#ctl00_ContentPlaceHolder1_ddlNewCollege").val() == "0") {
                        alert("Please select College.");
                        return false;
                    }
                    else if ($("#ctl00_ContentPlaceHolder1_ddlNewDegree").val() == "0") {
                        alert("Please select Degree.");
                        return false;
                    }
                    else if ($("#ctl00_ContentPlaceHolder1_ddlNewBranch").val() == "0") {
                        alert("Please select Branch.");
                        return false;
                    }
                    else if ($("#ctl00_ContentPlaceHolder1_ddlNewPaymentType").val() == "0") {
                        alert("Please select Payment Type.");
                        return false;
                    }
                    else if (error == false && chk.checked) {
                        return this.validation_total_amount();
                    }

                    if (confirm("Are you sure,You want to change pament type with branch change for selected student?"))
                        return true;
                    else
                        return false;
                }
                else if (withbranch == "2") {
                    if ($("#ctl00_ContentPlaceHolder1_ddlNewPaymentType").val() == "0") {
                        alert("Please select Payment Type.");
                        return false;
                    }
                    else if (error == false && chk.checked) {
                        return this.validation_total_amount();
                    }

                    if (confirm("Are you sure,You want to change payment type for selected student?"))
                        return true;
                    else
                        return false;
                }
            }
            else if (radioValue == "4") {
                if ($("#ctl00_ContentPlaceHolder1_ddlScheme").val() == "0") {
                    alert("Please select Scheme.");
                    error = true;
                    return false;
                }
                else if ($("#ctl00_ContentPlaceHolder1_ddlSemester").val() == "0") {
                    alert("Please select Semester.");
                    error = true;
                    return false;
                }
                else if ($("#ctl00_ContentPlaceHolder1_ddlAdmBatch").val() == "0") {
                    alert("Please select Admission Batch.");
                    error = true;
                    return false;
                }
                else if (error == false && chk.checked) {
                    //this.validation_total_amount();
                }

                else if (confirm("Are you sure,You want to re-admit for selected student?"))
                    return true;
                else
                    return false;
            }
        }

        function validation_total_amount() {
            var amount_total = 0;
            var paidamount = $('#ctl00_ContentPlaceHolder1_hdfPaidAmount').val();
            var rowCount = $('#ctl00_ContentPlaceHolder1_lvFeeItemsChanges_tblHeadChangesItems tr').length;
            for (var i = 0; i < rowCount.length - 1; i++) {
                var amount = $("#ctl00_ContentPlaceHolder1_lvFeeItemsChanges_ctrl" + i + "_txtChangeHeadAmount").val();
                amount_total += parseFloat(amount);
            }
            if (amount_total > paidamount) {
                alert('Total change amount should be less than or equal to paid amount');
                return false;
            }
            else
                return true;
        }

        function listvalidation() {
            var old_ptype = $("#<%=hdfPaymentType.ClientID%>").val();
            var new_ptype = $("#<%=ddlNewPaymentType.ClientID%> option:selected").val();

            if (confirm("Are you sure you want to change the selected payment type?")) {
                __doPostBack('ddlNewPaymentType', '');
                return true;
            }
            else {
                // $('#<%=ddlNewPaymentType.ClientID%>').empty();

                $('#<%=ddlNewPaymentType.ClientID%>').prepend($("<option selected='selected' />").val(0).text("Please Select"));
                var value = document.getElementById("<%=ddlNewPaymentType.ClientID%>");
                value.options[0].selected = true;
                //  $('#ddlNewPaymentType').find("option[value='0']").attr("selected", "selected");
                return false;
            }
            if (old_ptype == new_ptype) {
                $("#ctl00_ContentPlaceHolder1_divHeadchange").hide();
                document.getElementById('<%=chkHeadChange.ClientID%>').checked = false;
                return true;
            }
            else {
                $("#ctl00_ContentPlaceHolder1_divHeadchange").show();
                document.getElementById('<%=chkHeadChange.ClientID%>').checked = false;
                return true;
            }

        }

    </script>
    <script type="text/javascript">
        function confirmmsgbranch() {
            var radioValue = $('#<%=rblSelection.ClientID %> input[type=radio]:checked').val();
            var withbranch = $('#<%=rblWithBranch.ClientID %> input[type=radio]:checked').val();
            if (radioValue == "2") {
                if (confirm("Are you sure you want to change the selected branch?")) {
                    __doPostBack('ddlNewBranch', '');
                    return true;
                }
                else {
                    $('#<%=ddlNewBranch.ClientID%>').prepend($("<option selected='selected' />").val(0).text("Please Select"));
                    var value = document.getElementById("<%=ddlNewBranch.ClientID%>");
                    value.options[0].selected = true;
                    return false;
                }
            }
            else {
                __doPostBack('ddlNewBranch', '');
                return true;
            }
        }

        function GetMaster1Details() {
            var value = document.getElementById("<%=ddlNewPaymentType.ClientID%>");
            value.options[value.selectedIndex].value = '0';
            value.options[value.selectedIndex].text = 'Please select';
            alert(value.options[value.selectedIndex].value);
            // alert("value:-" + " " + getvalue + " " + "Text:-" + " " + gettext);
        }
    </script>

    <script type="text/javascript">
        $(function () {
            $(':text').bind('keydown', function (e) {
                //on keydown for all textboxes prevent from postback
                if (e.target.className != "searchtextbox") {
                    if (e.keyCode == 13) { //if this is enter key
                        document.getElementById('<%=btnSearch.ClientID%>').click();
                        e.preventDefault();
                        return true;
                    }
                    else
                        return true;
                }
                else
                    return true;
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $(':text').bind('keydown', function (e) {
                    //on keydown for all textboxes
                    if (e.target.className != "searchtextbox") {
                        if (e.keyCode == 13) { //if this is enter key
                            document.getElementById('<%=btnSearch.ClientID%>').click();
                                e.preventDefault();
                                return true;
                            }
                            else
                                return true;
                        }
                        else
                            return true;
                    });
                });

            });
    </script>
</asp:Content>

