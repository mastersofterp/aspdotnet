<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PanelCreation.aspx.cs" Inherits="panelcreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">PANEL CREATION</h3>
                </div>

                <div class="box-body">
                     <asp:UpdatePanel ID="updpanel" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <div class="col-12 ">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Admission Batch</label>
                                </div>
                                <asp:DropDownList ID="ddlAdmBatch" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged" AutoPostBack="true" TabIndex="1" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                    Display="None" ErrorMessage="Please Select Admission Batch " SetFocusOnError="True"
                                    InitialValue="0" ValidationGroup="PanelCreation"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Degree</label>
                                </div>

                                <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="2" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" SetFocusOnError="True" InitialValue="0"
                                    ValidationGroup="PanelCreation"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Program</label>
                                </div>

                                <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlProgram" runat="server" ControlToValidate="ddlProgram"
                                    Display="None" ErrorMessage="Please Select Program" SetFocusOnError="True" InitialValue="0"
                                    ValidationGroup="PanelCreation"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Schedule</label>
                                </div>

                                <asp:DropDownList ID="ddlschedule" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="4" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlschedule" runat="server" ControlToValidate="ddlschedule"
                                    Display="None" ErrorMessage="Please Select Schedule" SetFocusOnError="True" InitialValue="0"
                                    ValidationGroup="PanelCreation"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Panel Name</label>
                                </div>
                                <asp:TextBox ID="txtpanelName" runat="server" CssClass="form-control" MaxLength="100" TabIndex="5"  />
                                <asp:RequiredFieldValidator ID="rfvtxtpanelName" runat="server" ControlToValidate="txtpanelName"
                                    Display="None" ErrorMessage="Please Enter Panel Name " SetFocusOnError="True"
                                    ValidationGroup="PanelCreation"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>For </label>
                                </div>
                                <asp:DropDownList ID="ddlpanelfor" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="6">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">Interview</asp:ListItem>
                                    <asp:ListItem Value="2">Group Discussion</asp:ListItem>
                                    <asp:ListItem Value="3">Business Awareness Test (BAT)</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlpanelfor" runat="server" ControlToValidate="ddlpanelfor"
                                    Display="None" ErrorMessage="Please Select For " SetFocusOnError="True" InitialValue="0"
                                    ValidationGroup="PanelCreation"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Staff</label>
                                </div>

                                <asp:ListBox ID="lstbxStaff" runat="server" CssClass="form-control multi-select-demo"
                                    AppendDataBoundItems="true" SelectionMode="Multiple" TabIndex="7"></asp:ListBox>

                                <asp:RequiredFieldValidator ID="rfvddlstaff" runat="server" ControlToValidate="lstbxStaff"
                                    Display="None" ErrorMessage="Please Select Staff " SetFocusOnError="True"
                                    ValidationGroup="PanelCreation"></asp:RequiredFieldValidator>
                            </div>
                        <%--    <div class="form-group col-lg-1 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label></label>
                                </div>
                                <label style="font-size: 20px;">+</label>
                            </div>--%>
                        </div>
                    </div>

                    <div class="col-12 btn-footer ">
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnsubmit_Click" TabIndex="8" ValidationGroup="PanelCreation" />
                        <asp:ValidationSummary ID="vsapplicationstage" runat="server" DisplayMode="List" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="PanelCreation" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="9" />
                    </div>


                    <div class="col-12">
                        <asp:Panel ID="pnlpanel" runat="server" Visible="true">
                            <asp:ListView ID="lvPanel" runat="server" Visible="true">
                                <LayoutTemplate>

                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <thead class="bg-light-blue">
                                            <tr>

                                                <th>Edit
                                                </th>

                                                <th>Admission Batch
                                                </th>
                                                <th>Degree
                                                </th>
                                                <th>Program
                                                </th>
                                                <th>Schedule Date
                                                </th>
                                                <th>Schedule Time
                                                </th>
                                                <th>Panel Name
                                                </th>
                                                <th>Panel For
                                                </th>
                                                <th>Staff
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
                                            <asp:ImageButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" CommandArgument='<%# Eval("PANELID")%>'
                                                ImageUrl="~/Images/edit.png" />
                                        </td>
                                        <td id="Td1" runat="server">
                                            <asp:Label runat="server" ID="lblbatch" Text='<%#Eval("BATCHNAME") %>'></asp:Label>
                                        </td>
                                        <td id="Td2" runat="server">
                                            <asp:Label runat="server" ID="lbldegree" Text='<%#Eval("DEGREE_CODE") %>'></asp:Label>
                                        </td>
                                        <td id="Td3" runat="server">
                                            <asp:Label runat="server" ID="lblbranch" Text='<%#Eval("LONGNAME") %>'></asp:Label>
                                        </td>
                                        <td id="Td4" runat="server">
                                            <asp:Label runat="server" ID="lblscheduledate" Text='<%#Eval("SCHD_DATE","{0:dd-MM-yyyy}") %>'></asp:Label>
                                        </td>
                                        <td id="Td5" runat="server">
                                            <asp:Label runat="server" ID="lblscheduletime" Text='<%#Eval("SCHD_TIME") %>'></asp:Label>
                                        </td>
                                        <td id="Td6" runat="server">
                                            <asp:Label runat="server" ID="lblpanelname" Text='<%#Eval("PANEL_NAME") %>'></asp:Label>
                                        </td>
                                        <td id="Td7" runat="server">
                                            <asp:Label runat="server" ID="lblfor" Text='<%#Eval("PANEL_FOR_TEXT") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblstaff" Text='<%#Eval("UA_FULLNAME") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
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
                });
            });
        });
    </script>
</asp:Content>

