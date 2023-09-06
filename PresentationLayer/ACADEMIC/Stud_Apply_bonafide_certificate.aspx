<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Stud_Apply_bonafide_certificate.aspx.cs" Inherits="ACADEMIC_Stud_Apply_bonafide_certificate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updStudBonafide"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <asp:UpdatePanel ID="updStudBonafide" runat="server">
                    <ContentTemplate>
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>APPLY FOR CERTIFICATE</b></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-md-12 mt-3">
                                <asp:Panel runat="server" ID="pnlAdmin" Visible="false">

                                    <div class="col-sm-4 form-group" runat="server" id="divregno">
                                        <label><span style="color: red;">*</span> Registration No.</label>
                                        <asp:TextBox ID="txtRegNo" runat="server" CssClass="form-control" ValidationGroup="apply" AutoComplete="OFF" TabIndex="1" />
                                        <asp:RequiredFieldValidator ID="rfvregno" runat="server" ControlToValidate="txtRegNo"
                                            ValidationGroup="submit" Display="None" ErrorMessage="Please Enter Enrollement No." />

                                    </div>

                                    <div class="box-footer">
                                        <div class="col-md-12" style="text-align: center">
                                            <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show"
                                                Font-Bold="true" ValidationGroup="Show " CssClass="btn btn-primary" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="5" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                                            </p>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>

                        <div id="divStudentInfo" runat="server" visible="false" style="display: block;">
                            <div class="col-sm-12">
                                <%--<h3 style="margin-top: 5px; margin-bottom: 0px;">Student Information</h3>--%>
                              <%--  <h3 class="box-title">Student Information</h3>--%>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <ul class="list-group list-group-unbordered" style="margin-bottom: 0px;">
                                        <li class="list-group-item"><b>Student Name :</b><a class="pull-right">
                                            <asp:Label ID="lblStudename" runat="server" Font-Bold="True"></asp:Label></a> </li>
                                        <li class="list-group-item"><b>Admission Batch :</b><a class="pull-right">
                                            <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label></a> </li>
                                        <li class="list-group-item"><b>Mobile No:</b><a class="pull-right">
                                            <asp:Label ID="lblMobNo" runat="server" Font-Bold="True"></asp:Label></a> </li>
                                        <%--  <li class="list-group-item" style="display: none"><b>Enrollment No. :</b><a class="pull-right">
                                            <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>--%>
                                        <li class="list-group-item"><b>Registration No. :</b><a class="pull-right">
                                            <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label></a> </li>
                                    </ul>
                                </div>
                                <div class="col-md-4">
                                    <ul class="list-group list-group-unbordered" style="margin-bottom: 0px;">
                                        <li class="list-group-item"><b>Degree:</b><a class="pull-right">
                                            <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label></a> </li>
                                        <li class="list-group-item"><b>Semester :</b><a class="pull-right">
                                            <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Branch :</b><a class="pull-right">
                                            <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a> </li>
                                        <li class="list-group-item"><b>School/Institute Name:</b><a class="pull-right">
                                            <asp:Label ID="lblSchool" runat="server" Font-Bold="True"></asp:Label></a> </li>
                                    </ul>
                                </div>

                                <div class="col-md-4">
                                    <ul class="list-group list-group-unbordered" style="margin-bottom: 0px;">
                                        <li class="list-group-item"><b>Date of Birth :</b><a class="pull-right">
                                            <asp:Label ID="lblDOB" runat="server" Font-Bold="True"></asp:Label></a> </li>
                                        <li class="list-group-item"><b>Email ID:</b><a class="pull-right">
                                            <asp:Label ID="lblEmailID" runat="server" Font-Bold="True"></asp:Label></a> </li>

                                        <asp:Label ID="lblbatch" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lbldegreeno" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblbranchno" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblsemesterno" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="cert_no" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblcollege_id" runat="server" Visible="false"></asp:Label>

                                        <li class="list-group-item"><b style="color:red">*</b><b>Certificate :</b><a class="pull-right">
                                             <asp:DropDownList ID="ddlCertificate" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                             CssClass="form-control" data-select2-enable="true">
                                                 <asp:ListItem Value="0">Please select</asp:ListItem>
                                        </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvcertificate" runat="server" ControlToValidate="ddlCertificate"
                                                Display="None" ErrorMessage="Please Select Certificate" SetFocusOnError="true" ValidationGroup="apply" InitialValue="0" />
                                        </a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                             <div class="col-12 btn-footer">
                            <div class="col-md-12" runat="server" id="divbutton" visible="false">
                                <div class=" text-center" style="text-align: center !important;">
                                    <asp:Button ID="btnApply" runat="server" OnClick="btnApply_Click" Enabled="false" TabIndex="2" Text="Apply" ToolTip="Click to Apply"
                                        ValidationGroup="apply" CssClass="btn btn-success" />
                                    <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="3" CssClass="btn btn-info" OnClick="btnReport_Click" Enabled="false" Visible="false" />
                                    <asp:ValidationSummary ID="vssummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="apply" TabIndex="9" />
                                </div>
                            </div>
                                 </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel2" runat="server">
                                    <asp:ListView ID="lvIssueCert" runat="server" EnableModelValidation="True">
                                        <EmptyDataTemplate>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Search Results</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Reg No.
                                                        <th>Branch
                                                        </th>
                                                        <th>Semester
                                                        </th>
                                                        <td>
                                                            Certificate 
                                                        </td>
                                                        <th>ApprovePerson
                                                        </th>
                                                        <th>ApproveDate
                                                        </th>
                                                        <th>Status
                                                        </th>
                                                        <th>
                                                            Print Certificate 
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
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("STUDNAME")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%# Eval("REGNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SHORTNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTERNAME")%>
                                                </td>
                                                <td>
                                                       <%# Eval("CERT_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("UA_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("APPROVED_DATE", "{0:dd/MM/yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("APPROVE_STATUS")%>
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="updnpfPreview" runat="server" >
                                                    <ContentTemplate>
                                                        <asp:ImageButton ID="btnPrint" runat="server" CommandArgument='<%# Eval("CERT_SHORT_NAME") %>' ImageUrl="~/images/downarrow.jpg" Text="Preview" ToolTip='<%# Eval("idno") %>' Width="20px" Visible='<%# Convert.ToString(Eval("STATUS"))=="0"?false:true %>' OnClick="btnPrint_Click"/>
                                                        <asp:Label ID="lblnpfPreview" Text='<%# Convert.ToString(Eval("STATUS"))=="0" ?"Print not available":"" %>' runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnPrint" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </div>
                        </div>
           
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnReport" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>

        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

