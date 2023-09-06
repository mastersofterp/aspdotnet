<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="UploadAdmissionData.aspx.cs" Inherits="ADMINISTRATION_UploadAdmissionData" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

    <%--<script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#example').DataTable({

            });
        }

    </script>--%>

    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title"><%--Upload Admission Data<--%>
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <div class=" note-div">
                                                <h5 class="heading">Note</h5>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Before import excel, kindly ensure that School/College, Degree, Branch available in ERP master. If not available then do the Master entry in ERP then upload excel.</span>  </p>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Before import excel, kindly ensure that Column Names And Data Format is same as available in blank Excel Sheet.</span></p>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Student Admission Data Import</h5>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Admission Batch</label>--%>
                                                <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>

                                            </div>
                                            <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged" AutoPostBack="true"
                                                TabIndex="1">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                                Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Attach Excel File</label>
                                            </div>
                                            <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Select file to upload" TabIndex="2" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divRecords" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Already Saved Records</label>
                                            </div>
                                            <asp:Label ID="lblValue" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click1" CssClass="btn btn-primary" TabIndex="3"
                                        Text="Download Blank Excel Sheet" ToolTip="Click to download blank excel format file" Enabled="true"> Download Blank Excel Sheet</asp:LinkButton>
                                    <asp:LinkButton ID="btnUpload" runat="server" ValidationGroup="report" OnClick="btnUpload_Click" CssClass="btn btn-primary" TabIndex="4"
                                        Text="Upload Excel Sheet" ToolTip="Click to Upload" Enabled="true">Upload Excel</asp:LinkButton>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="report" Style="text-align: center" />
                                </div>

                                <div class="form-group col-12 text-center" id="divNote" runat="server" visible="false">
                                    <label><span style="color: red">Note: Excel Sheet Data is not imported, Please correct following data and upload the Excel again.</span></label>
                                </div>

                                <div class="col-12">
                                    <asp:ListView ID="lvStudent" runat="server" OnItemDataBound="lvStudent_ItemDataBound">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Student List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Row No.</th>
                                                        <th><%--Reg. No.--%>
                                                            <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>Roll No. 
                                                        </th>
                                                        <th>Gender
                                                        </th>
                                                        <th><%--Admission Type --%>
                                                            <asp:Label ID="blDYddlAdmType" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th><%--Sem--%>
                                                             <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>Year
                                                        </th>
                                                        <%--  <th>Adm. Batch
                                                        </th>--%>
                                                        <th>Student Name
                                                        </th>
                                                        <th>DOB
                                                        </th>
                                                        <th>Father's Name
                                                        </th>
                                                        <th>Category
                                                        </th>
                                                        <th>Phy. Hand.
                                                        </th>
                                                        <th>Mobile Number
                                                        </th>
                                                        <th>Email Id
                                                        </th>
                                                        <th>College Name
                                                        </th>
                                                        <th><%--Degree--%>
                                                            <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th><%--Branch--%>
                                                             <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>Address1
                                                        </th>
                                                        <th>Address2
                                                        </th>
                                                        <th>Address3
                                                        </th>
                                                        <th>State
                                                        </th>
                                                        <th>District
                                                        </th>
                                                        <th>Pin Code
                                                        </th>
                                                        <th>Board
                                                        </th>
                                                        <th>YR_12
                                                        </th>
                                                        <th>PR_12
                                                        </th>
                                                        <th>Blood Grp
                                                        </th>
                                                        <th>Payment Type
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
                                                <td><%# Container.DataItemIndex +1 %></td>
                                                <td>
                                                    <asp:Label ID="lblRegNo" runat="server" Text='<%# Eval("REGISTRATIONNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRollNo" runat="server" Text='<%# Eval("ROLLNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblGender" runat="server" Text='<%# Eval("GENDER")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblIdType" runat="server" Text='<%# Eval("ADMISSIONTYPE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblYear" runat="server" Text='<%# Eval("YEAR")%>'></asp:Label>
                                                </td>
                                                <%--<td>
                                                    <asp:Label ID="lblAdmBatch"  runat="server" Text='<%# Eval("ADMBATCH")%>'></asp:Label>
                                                </td>--%>
                                                <td>
                                                    <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDENTNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDOB" runat="server" Text='<%# Eval("DOB")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFatherName" runat="server" Text='<%# Eval("FATHERNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CATEGORY")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPH" runat="server" Text='<%# Eval("PHYSICALLY_HANDICAPPED")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("MOBILENO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("EMAILID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCollege" runat="server" Text='<%# Eval("COLLEGENAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BRANCH")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAdd1" runat="server" Text='<%# Eval("ADDRESS1")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAdd2" runat="server" Text='<%# Eval("ADDRESS2")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAdd3" runat="server" Text='<%# Eval("ADDRESS3")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblState" runat="server" Text='<%# Eval("STATE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("DISTRICT")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPin" runat="server" Text='<%# Eval("PINCODE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblBoard" runat="server" Text='<%# Eval("BOARD")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblYR12" runat="server" Text='<%# Eval("YR_12")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPR12" runat="server" Text='<%# Eval("PR_12")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblBloogGrp" runat="server" Text='<%# Eval("BLOODGROUP")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPayType" runat="server" Text='<%# Eval("PAYMENT_TYPE")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
     
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

