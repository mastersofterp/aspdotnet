<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="NPFDTransfer.aspx.cs" Inherits="ACADEMIC_NPFDTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <style>
        .dataTables_scrollHeadInner {
            width: max-content!important;
        }
    </style>

    <asp:UpdatePanel ID="updpnUploadExcel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <%-- <div class="form-group col-lg-3 col-md-6 col-12">--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>NPF Excel Upload</label>
                                        </div>
                                        <asp:FileUpload ID="fuexelUpload" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">

                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" TabIndex="1" ToolTip="Please Upload File" CssClass="btn btn-primary" OnClick="btnUpload_Click" />
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSampleDownload" runat="server" Text="Sample Excel Download" ToolTip="Download Sample Excel" TabIndex="2" CssClass="btn btn-primary" OnClick="btnSampleDownload_Click" />
                                    <asp:Button ID="btnSubmit" runat="server" TabIndex="3" ToolTip="Submit" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnShow" runat="server" TabIndex="4" ToolTip="Show" Text="Show Existing Record" CssClass="btn btn-info" OnClick="btnShow_Click" />
                                    <asp:Button ID="btnCancel" runat="server" TabIndex="5" ToolTip="Cancel" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                                </div>
                            </div>
                        </div>
                        <div>
                            <asp:UpdatePanel ID="updList" runat="server">
                                <ContentTemplate>
                                    <div class="box-footer" style="text-align: center">
                                        <div class="form-group col-md-12 mt-2" style="margin-top: 10px">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:ListView ID="lvBinddata" runat="server" EnableModelValidation="True">

                                                    <LayoutTemplate>

                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>SR.No</th>
                                                                    <th>NPF_Discipline</th>
                                                                    <th>NPF_Programme</th>
                                                                    <th>NPF_Specialization</th>
                                                                    <th>School Name</th>
                                                                    <th>Degree</th>
                                                                    <th>Programme/Branch Name</th>
                                                                    <th>COURSE_TYPE</th>


                                                                    <%-- <th style="text-align: center;">COURSE_TYPE</th>   --%>
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
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </td>
                                                            <td><%# Eval("NPF_Discipline")%></td>
                                                            <td><%# Eval("NPF_Programme")%></td>
                                                            <td><%# Eval("NPF_Specialization")%></td>
                                                            <td><%# Eval("[School Name]")%></td>
                                                            <td><%# Eval("Degree")%></td>
                                                            <td><%# Eval("Programme/Branch Name")%></td>
                                                            <td><%# Eval("COURSE_TYPE")%></td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </div>
                        <div id="exceltodt">
                            <asp:UpdatePanel ID="updpnlExceldata" runat="server">
                                <ContentTemplate>
                                    <div class="box-footer" style="text-align: center">
                                        <div class="form-group col-md-12 mt-2" style="margin-top: 10px">
                                            <asp:Panel ID="Panel2" runat="server">
                                                <asp:ListView ID="lvDatatableDisplay" runat="server" EnableModelValidation="True">

                                                    <LayoutTemplate>

                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>SR.No</th>
                                                                    <th>NPF_Discipline</th>
                                                                    <th>NPF_Programme</th>
                                                                    <th>NPF_SpecializationID</th>
                                                                    <th>NPF_Specialization</th>
                                                                    <th>College_ID</th>
                                                                    <th>School Name</th>
                                                                    <th>DegreeNo</th>
                                                                    <th>Degree</th>
                                                                    <th>Branchno</th>
                                                                    <th>Programme/Branch Name</th>
                                                                    <th>COURSE_TYPE</th>
                                                                    <th>IDTYPE</th>


                                                                    <%-- <th style="text-align: center;">COURSE_TYPE</th>   --%>
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
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </td>
                                                            <td><%# Eval("NPF_Discipline")%></td>
                                                            <td><%# Eval("NPF_Programme")%></td>
                                                            <td><%# Eval("NPF_SpecializationID")%></td>
                                                            <td><%# Eval("NPF_Specialization")%></td>
                                                            <td><%# Eval("College_ID")%></td>
                                                            <td><%# Eval("School Name")%></td>
                                                            <td><%# Eval("DegreeNo")%></td>
                                                            <td><%# Eval("Degree")%></td>
                                                            <td><%# Eval("Branchno")%></td>
                                                            <td><%# Eval("Programme/Branch Name")%></td>
                                                            <td><%# Eval("COURSE_TYPE")%></td>
                                                            <td><%# Eval("IDTYPE")%></td>


                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lvDatatableDisplay" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnSampleDownload" />
            <asp:PostBackTrigger ControlID="btnUpload" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

