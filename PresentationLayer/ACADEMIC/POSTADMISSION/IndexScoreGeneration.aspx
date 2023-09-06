<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="IndexScoreGeneration.aspx.cs" Inherits="IndexScoreGeneration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .table-bordered > thead > tr > th {
            border-top: 1px solid #e5e5e5;
        }
    </style>

    <asp:UpdatePanel ID="updSession" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Index Score Generation</h3>
                        </div>

                        <div class="box-body">

                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" SetFocusOnError="True"
                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Application Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAppType" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlAppType_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlAppType" runat="server" ControlToValidate="ddlAppType"
                                            Display="None" ErrorMessage="Please Select Application Type" SetFocusOnError="True"
                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="3">
                                            <%--<asp:ListItem Value="0">Please Select</asp:ListItem>--%>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" SetFocusOnError="True"
                                            ValidationGroup="Academic" InitialValue="Please Select"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Program/Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3">
                                            <%--<asp:ListItem Value="0">Please Select</asp:ListItem>--%>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" SetFocusOnError="True"
                                            ValidationGroup="Academic" InitialValue="Please Select"></asp:RequiredFieldValidator>
                                    </div>
                                    <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Order By</label>
                                </div>
                                <asp:DropDownList ID="ddlStudentType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="4">
                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0">Application No.</asp:ListItem>
                                    <asp:ListItem Value="1">RAJ01234</asp:ListItem>
                                </asp:DropDownList>
                            </div>--%>
                                </div>
                            </div>


                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="btn btn-primary" ValidationGroup="Academic" OnClick="btnGenerate_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="false" ValidationGroup="Academic" />
                                <asp:Button ID="btnProcess" runat="server" Text="Process" CssClass="btn btn-primary" Visible="false" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" CausesValidation="false" OnClick="btnCancel_Click" />
                            </div>


                            <div class="col-12">
                                <asp:Panel ID="pnlIndexScoreGenerateList" runat="server" Visible="true">
                                    <asp:ListView ID="lvIndexScoreGenerateList" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divIndexScoreGenerateList">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sr.No. </th>
                                                        <th>Application No. </th>
                                                        <th>Name Of Student </th>
                                                        <th>Index Mark </th>
                                                        <th>Mark Details </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hfdScoreGen" runat="server" Value='<%# Eval("ID")+","+Eval("USERNO")+","+Eval("ADMBATCH")+","+Eval("BRPREF") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("APPLICATION_ID")%>
                                                </td>
                                                <td>
                                                    <%# Eval("APPLICANTNAME")%>
                                                </td>
                                                <td style="text-align: center;">
                                                    <%--<button type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#myModalIndexMark">
                                                        View
                                                    </button>--%>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnIndexMarks" class="btn btn-primary btn-sm"
                                                                runat="server" ValidationGroup="Academic" Text="View"
                                                                CommandArgument='<%# Eval("ID")+","+Eval("USERNO")+","+Eval("ADMBATCH")+","+Eval("BRPREF")+","+Eval("APPLICANTNAME")+","+Eval("APPLICATION_ID") %>'
                                                                AlternateText="Index Marks" ToolTip="Index Marks"
                                                                OnClick="btnIndexMarks_Click" TabIndex="6" />
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnIndexMarks" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td style="text-align: center;">
                                                    <%--<button type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#myModalMarksDetails">
                                                        View
                                                    </button>--%>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Button ID="btnMarksDetails" class="btn btn-primary btn-sm"
                                                                runat="server" CausesValidation="false" Text="View"
                                                                CommandArgument='<%# Eval("ID")+","+Eval("USERNO")+","+Eval("ADMBATCH")+","+Eval("BRPREF")+","+Eval("APPLICANTNAME")+","+Eval("APPLICATION_ID") %>'
                                                                AlternateText="Marks Details" ToolTip="Index Marks"
                                                                OnClick="btnMarksDetails_Click" TabIndex="6" />
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnMarksDetails" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>



                                <%--<table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Sr.No. </th>
                                            <th>Application No. </th>
                                            <th>Name Of Student </th>
                                            <th>Index Mark </th>
                                            <th>Mark Details </th>                                            
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>1 </td>
                                            <td>22BBA1001 </td>
                                            <td>Rahul Patil </td>
                                            <td>
                                                <button type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#myModalIndexMark">
                                                    View
                                                </button>
                                            </td>
                                            <td>
                                                <button type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#myModalMarksDetails">
                                                    View
                                                </button>
                                            </td>                                            
                                        </tr>
                                    </tbody>
                                </table>--%>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlAppType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlDegree" EventName="SelectedIndexChanged" />

            <asp:PostBackTrigger ControlID="btnGenerate" />
            <asp:PostBackTrigger ControlID="btnCancel" />

        </Triggers>
    </asp:UpdatePanel>
    <!-- The Modal Index Mark -->
    <div class="modal fade" id="myModalIndexMark" data-backdrop="false" style="background-color: rgba(0,0,0,0.6);">
        <div class="modal-dialog  modal-xl">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Index Mark</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    <div class="col-12 mt-3 table-responsive">

                        <asp:ListView ID="lvIndexMark" runat="server">
                            <LayoutTemplate>
                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divIndexMarkList">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Subjects </th>
                                            <th>Program 1 </th>
                                            <th>Program 2 </th>
                                            <th>Program 3 </th>
                                            <th>Program 4 </th>
                                        </tr>
                                        <%--Srno	Subjects	A	B	C	D	E	F--%>
                                    </thead>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>

                                <tr>
                                    <td>
                                        <%# Eval("Subjects")%>
                                    </td>
                                    <td>
                                        <%# Eval("A")%>
                                    </td>
                                    <td>
                                        <%# Eval("B")%>
                                    </td>
                                    <td>
                                        <%# Eval("C")%>
                                    </td>
                                    <td>
                                        <%# Eval("D")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>

                        <%--<table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblIndexMark">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th>Subjects </th>
                                    <th>Program 1 </th>
                                    <th>Program 2 </th>
                                    <th>Program 3 </th>
                                    <th>Program 4 </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Subject Mark</td>
                                    <td>518</td>
                                    <td>518</td>
                                    <td>518</td>
                                    <td>518</td>
                                </tr>
                                <tr>
                                    <td>Subject Weightage</td>
                                    <td>150</td>
                                    <td>0</td>
                                    <td>150</td>
                                    <td>150</td>
                                </tr>
                                <tr>
                                    <td>NSS Mark </td>
                                    <td>0</td>
                                    <td>0</td>
                                    <td>0</td>
                                    <td>0</td>
                                </tr>
                                <tr>
                                    <td>NCC Marks </td>
                                    <td>0</td>
                                    <td>0</td>
                                    <td>0</td>
                                    <td>0</td>
                                </tr>
                                <tr>
                                    <td>Other Bonus Marks </td>
                                    <td>0</td>
                                    <td>0</td>
                                    <td>0</td>
                                    <td>0</td>
                                </tr>
                                <tr style="color: #0000ff;">
                                    <td>Index Marks </td>
                                    <td>668</td>
                                    <td>518</td>
                                    <td>668</td>
                                    <td>668</td>
                                </tr>
                                <tr>
                                    <td>Tie Break First </td>
                                    <td>110</td>
                                    <td>110</td>
                                    <td>0</td>
                                    <td>0</td>
                                </tr>
                                <tr>
                                    <td>Tie Break Second </td>
                                    <td>518</td>
                                    <td>518</td>
                                    <td>518</td>
                                    <td>518</td>
                                </tr>
                            </tbody>
                        </table>--%>
                    </div>
                </div>

                <!-- Modal footer -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>

            </div>
        </div>
    </div>

    <!-- The Modal Marks Details -->
    <div class="modal" id="myModalMarksDetails" data-backdrop="false" style="background-color: rgba(0,0,0,0.6);">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Mark Details</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>Name Of Student :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblNameOfStd" runat="server" Text="-----" Font-Bold="true"></asp:Label>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                            <div class="col-lg-6 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>Application No. :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblAppNo" runat="server" Text="-----" Font-Bold="true"></asp:Label>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 mt-3 table-responsive">

                        <asp:ListView ID="lvMarksDetailsList" runat="server">
                            <LayoutTemplate>
                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divMarksDetailsList">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Subject Type</th>
                                            <th>Subject / Credit </th>
                                            <th>Max.Marks / Max CGPA</th>
                                            <th>Obtained / Obtained CGPA</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>

                                <tr>
                                    <%--UNO--%>
                                    <td>
                                        <%# Eval("SUBJECTTYPE")%>
                                    </td>
                                    <td>
                                        <%# Eval("SUBJECTNAME")%>
                                    </td>
                                    <td>
                                        <%--style="text-align: center;"--%>
                                        <%# Eval("OUT_OF_MARKS")%>
                                    </td>
                                    <td>
                                        <%# Eval("OBTAINED_MARKS")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>

                    </div>



                    <%--<table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblMarksDetails">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th>Subject Type</th>
                                    <th>Subject / Credit </th>
                                    <th>Max.Marks / Max CGPA</th>
                                    <th>Obtained / Obtained CGPA</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Part I - English</td>
                                    <td>English</td>
                                    <td>100</td>
                                    <td>88</td>
                                </tr>
                                <tr>
                                    <td>Part III</td>
                                    <td>Accountancy</td>
                                    <td>100</td>
                                    <td>55</td>
                                </tr>
                                <tr>
                                    <td>Part III</td>
                                    <td>Business Studies</td>
                                    <td>100</td>
                                    <td>55</td>
                                </tr>
                                <tr>
                                    <td>Part III</td>
                                    <td>Economics</td>
                                    <td>100</td>
                                    <td>72</td>
                                </tr>
                                <tr>
                                    <td>Part III</td>
                                    <td>Information Practices</td>
                                    <td>100</td>
                                    <td>77</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    --%>
                    <div class="col-12 mt-3 table-responsive">
                        <asp:ListView ID="lvProgramsList" runat="server">
                            <LayoutTemplate>
                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divProgramsList">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Programs</th>
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
                                        <%# Eval("PROGRAMNAME")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>

                        <%--<table class="table table-striped table-bordered nowrap" style="width: 100%" id="Table1">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th>Programs</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>BBA - Bachelor of Business Administration - CIMA (UK) / Business Analytics Self Finance</td>
                                </tr>
                                <tr>
                                    <td>BBA - Bachelor of Business Administration - CIMA (UK) / Business Analytics Self Finance</td>
                                </tr>
                                <tr>
                                    <td>BBA - Bachelor of Business Administration - CIMA (UK) / Business Analytics Self Finance</td>
                                </tr>
                                <tr>
                                    <td>BBA - Bachelor of Business Administration - CIMA (UK) / Business Analytics Self Finance</td>
                                </tr>
                            </tbody>
                        </table>--%>
                    </div>
                </div>

                <!-- Modal footer -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>

            </div>
        </div>
    </div>

    <!-- The Modal Profile -->
    <div class="modal fade" id="myModalProfile">
        <div class="modal-dialog">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Profile</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    Profile details shown here
                </div>

                <!-- Modal footer -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>

            </div>
        </div>
    </div>

    <script>
        function OpenIndexListModal() {
            $('#myModalIndexMark').modal('show');
        }

        function OpenMarksListModal() {
            $('#myModalMarksDetails').modal('show');
        }
    </script>
</asp:Content>

