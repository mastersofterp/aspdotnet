<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPUploadDocument.aspx.cs" Inherits="ACADEMIC_POSTADMISSION_ADMPUploadDocument" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <style>
        .dataTables_scrollHeadInner {
            width: max-content!important;
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Upload Student Document</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Admission Batch </label>
                                </div>
                                <asp:DropDownList ID="ddlAdmissionBatch" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Program Type </label>
                                </div>
                                <asp:DropDownList ID="ddlProgramType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlProgramType_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select </asp:ListItem>
                                    <asp:ListItem Value="1">UG</asp:ListItem>
                                    <asp:ListItem Value="2">PG</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Degree </label>
                                </div>
                                <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Program/Branch </label>
                                </div>
                                <asp:ListBox ID="lstProgram" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" TabIndex="6" AutoPostBack="true"></asp:ListBox>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Search By</label>
                                </div>
                                <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="false" AppendDataBoundItems="true"
                                    data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">NAME</asp:ListItem>
                                    <asp:ListItem Value="2">Application Id</asp:ListItem>
                                    <%--  OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged"--%>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Search By</label>
                                </div>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShow" runat="server" Text="Show Student List" TabIndex="5" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="8" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                        <asp:HiddenField ID="hdnUserNo" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnDegreeNo" runat="server" Value="0" />
                    </div>


                    <asp:Panel ID="pnlGV1" runat="server" Visible="false">
                        <div class="col-12">
                            <asp:ListView ID="lvSchedule" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <div id="demo-grid">
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tbllist">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Admission Batch</th>
                                                    <th>Application Id</th>
                                                    <th>Student Name</th>
                                                    <th>Email Id</th>
                                                    <th>Degree Name</th>
                                                    <th>Program/Branch</th>
                                                    <th>Document Status</th>
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

                                        <%-- <asp:HiddenField ID="hdnAdmBatch" runat="server" Value='<%#Eval("BATCHNO")%>' />
                                                <asp:HiddenField ID="hdnDegreeNo" runat="server" Value='<%# Eval("DEGREENO") %>' />
                                                <asp:HiddenField ID="hdnBranchNo" runat="server" Value='<%# Eval("BRANCHNO") %>' />
                                                <asp:HiddenField ID="hdnUserNo" runat="server" Value='<%# Eval("USERNO") %>' />--%>

                                        <td><%# Eval("ADMBATCH") %></td>
                                        <td>
                                            <asp:Label ID="lblApplicationId" runat="server" Text='<%# Eval("APPLICATION_ID") %>'></asp:Label>
                                            <asp:HiddenField ID="hdnLvDegreeNo" runat="server" Value='<%# Eval("DEGREENO") %>' />
                                        </td>
                                        <%--      <td><%# Eval("STUDENTNAME") %></td>--%>

                                        <td>
                                            <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("STUDENTNAME") %>' CommandArgument='<%# Eval("USERNO") %>' OnClick="lnkId_Click"></asp:LinkButton></td>
                                        <td><%# Eval("EMAILID") %></td>
                                        <td><%# Eval("DEGREENAME") %></td>
                                        <td><%# Eval("BRANCHNAME") %></td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text='<%#  Convert.ToInt32(Eval("Diff")) <=0 ? "Uploaded" : "Pending" %>'
                                                ForeColor='<%# Convert.ToInt32(Eval("Diff"))<=0 ?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlStudent" runat="server" Visible="false">
                        <div class="col-12" mt-4 mb-4>
                            <div class="row">
                                <div class="col-lg-3 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Student Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblStudentName" runat="server" Font-Bold="true"></asp:Label>
                                            </a>
                                        </li>

                                    </ul>
                                </div>
                                <div class="col-lg-3 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Application Id:</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblAppId" runat="server" Font-Bold="true">
                                                </asp:Label></a>
                                        </li>
                                    </ul>
                                </div>

                            </div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlBind" runat="server" Visible="false">
                        <asp:ListView ID="lvBinddata" runat="server">
                            <LayoutTemplate>
                                <div class="col-12 mt-4">
                                    <div class="sub-heading">
                                        <h5>Upload Document </h5>
                                        <label style="color: red; font-weight: bold;">NOTE:- Please Upload PDF File Only </label>
                                    </div>
                                </div>
                                <div class="col-12">
                                 <div class="table-responsive" style="height: 500px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                            <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                <tr>

                                                    <th>Sr.NO</th>
                                                    <th style="width: 170px">Document Name</th>
                                                    <th>Doc Status</th>
                                                    <th>Status</th>
                                                    <th>Upload Document</th>
                                                    <th>Document Status</th>
                                                    <th>Preview</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.DataItemIndex +1 %></td>
                                    <%-- <td style="width:5%"><%# Eval("DOCUMENTNO") %></td>--%>

                                    <td style="width: 170px">
                                        <asp:Label ID="lblStar" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                                        <asp:Label ID="lblDocument" Width="150px" ToolTip='<%# Eval("DOCUMENTNO") %>' runat="server" Text='<%# Eval("DOCUMENTNAME") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("DOCUMENTNO") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Text='<%#  Convert.ToInt32(Eval("MANDATORY"))==1 ? "Mandatory" : "Non Mandatory" %>'
                                            ForeColor='<%# Convert.ToInt32(Eval("MANDATORY"))==1 ?System.Drawing.Color.Red:System.Drawing.Color.Green %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="fuFile" runat="server" ToolTip='<%# Eval("DOCUMENTNO") %>' />
                                        <asp:HiddenField ID="hdnFile" runat="server" />

                                    </td>

                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Upload" ValidationGroup="Submit" OnClick="btnSubmit_Click"
                                            CssClass="btn btn-info" CommandArgument='<%# Eval("DOCUMENTNO") %>' ToolTip='<%# Eval("DOCUMENTNO") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Font-Bold="true" Text='<%#  Convert.ToInt32(Eval("Status")) > 0 ? "Uploaded" : "" %>'
                                            ForeColor='<%# Convert.ToInt32(Eval("Status"))> 0  ?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                    </td>

                                    <td style="text-align: center">
                                        <asp:UpdatePanel ID="updPreview" runat="server">
                                            <ContentTemplate>
                                                <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click"
                                                    Text="Preview" ImageUrl="~/Images/search-svg.png" ToolTip='<%# Eval("DOCUMENT_NAME") %>' data-toggle="modal" data-target="#preview"
                                                    CommandArgument='<%# Eval("DOCUMENTNAME") %>' Visible='<%# Convert.ToString(Eval("DOCUMENTNAME"))==string.Empty?false:true %>'></asp:ImageButton>

                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="imgbtnPreview" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <%--   <td>
                                            <asp:ImageButton runat="Server" ID="ImageButton1" ImageUrl="~/Images/calender11.jpg" Visible="false" Width="20px" Height="20px" AlternateText="" />
                                        </td>  --%>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="preview" role="dialog" style="display: none; margin-left: -100px;">
        <div class="modal-dialog text-center">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <!-- Modal content-->
                    <div class="modal-content" style="width: 700px;">
                        <div class="modal-header">
                            <%--   <button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                            <h4 class="modal-title">Document</h4>
                        </div>
                        <div class="modal-body">
                            <div class="col-md-12">

                                <asp:Literal ID="ltEmbed" runat="server" />

                                <%--<iframe runat="server" style="width: 100; height: 100px" id="iframe2"></iframe>--%>

                                <%--<asp:Image ID="imgpreview" runat="server" Height="530px" Width="600px"  />--%>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <%--  <asp:Panel ID="Panel1" runat="server">
            <div class="box-body">
                 <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="form-group col-lg-3 col-md-6 col-12">
                            <div class="label-dynamic">
                                <sup>* </sup>
                                <label style="color: red; font-weight: bold;">NOTE:- Please Upload PDF File Only </label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>--%>
    <%-- </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSubmit" />
           <asp:PostBackTrigger ControlID="fuFile" />
            </Triggers>
        </asp:UpdatePanel>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>
</asp:Content>

