<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ReservationConfiguration.aspx.cs" Inherits="ACADEMIC_ReservationConfiguration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updLists"
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

    <asp:UpdatePanel ID="updLists" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Reservation Configuration</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem>Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ValidationGroup="submit" InitialValue="0"
                                            ErrorMessage="Please Select Degree"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranchName" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                            TabIndex="2">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranchName" runat="server" ControlToValidate="ddlBranchName"
                                            InitialValue="0" Display="None" ValidationGroup="submit" ErrorMessage="Please Select Branch"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Total Intake</label>
                                        </div>
                                        <asp:TextBox ID="txtIntake" runat="server" CssClass="form-control" ToolTip="Please Enter Intake Capacity" MaxLength="3" TabIndex="3" />
                                        <asp:RequiredFieldValidator ID="rfvIntake" runat="server" ControlToValidate="txtIntake"
                                            Display="None" ValidationGroup="submit" ErrorMessage="Please Enter Intake Capacity"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteIntake" runat="server" FilterType="Custom"
                                            ValidChars="0123456789" TargetControlID="txtIntake">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Reservation Quota</h5>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>SC</label>
                                        </div>
                                        <asp:TextBox ID="txtSC" runat="server" CssClass="form-control" ToolTip="Please Enter Quota for SC Category" MaxLength="3" TabIndex="4" />

                                        <asp:RequiredFieldValidator ID="rfvSC" runat="server" ControlToValidate="txtSC"
                                            Display="None" ValidationGroup="submit" ErrorMessage="Please Enter Quota for SC Category"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteSC" runat="server" FilterType="Custom"
                                            ValidChars="0123456789" TargetControlID="txtSC">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>ST</label>
                                        </div>
                                        <asp:TextBox ID="txtST" runat="server" CssClass="form-control" ToolTip="Please Enter Quota for ST Category" MaxLength="3" TabIndex="5" />
                                        
                                        <asp:RequiredFieldValidator ID="rfvST" runat="server" ControlToValidate="txtST"
                                            Display="None" ValidationGroup="submit" ErrorMessage="Please Enter Quota for ST Category"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteST" runat="server" FilterType="Custom"
                                            ValidChars="0123456789" TargetControlID="txtST">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>General</label>
                                        </div>
                                        <asp:TextBox ID="txtGen" runat="server" CssClass="form-control" ToolTip="Please Enter Quota for General Category" MaxLength="3" TabIndex="6" />
                                        
                                        <asp:RequiredFieldValidator ID="rfvGeneral" runat="server" ControlToValidate="txtGen"
                                            Display="None" ValidationGroup="submit" ErrorMessage="Please Enter Quota for General Category"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteGeneral" runat="server" FilterType="Custom"
                                            ValidChars="0123456789" TargetControlID="txtGen">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>OBC</label>
                                        </div>
                                        <asp:TextBox ID="txtOBC" runat="server" CssClass="form-control" ToolTip="Please Enter Quota for OBC Category" MaxLength="3" TabIndex="6" />
                                        
                                        <asp:RequiredFieldValidator ID="rfvOBC" runat="server" ControlToValidate="txtOBC"
                                            Display="None" ValidationGroup="submit" ErrorMessage="Please Enter Quota for OBC Category"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteOBC" runat="server" FilterType="Custom"
                                            ValidChars="0123456789" TargetControlID="txtOBC">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="10" ValidationGroup="submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="11" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlconfiguration" runat="server">
                                    <asp:ListView ID="lvConfiguration" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Reservation Configuration List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divreservationlist">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align:center;">Edit
                                                        </th>
                                                        <th >Degree
                                                        </th>
                                                        <th>Branch
                                                        </th>
                                                        <th >Intake
                                                        </th>
                                                        <th >SC Quota
                                                        </th>
                                                        <th >ST Quota
                                                        </th>
                                                        <th >General Quota
                                                        </th>
                                                        <th >OBC Quota
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                       <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>                       
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td style="width: 5%;text-align:center;">
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/IMAGES/edit1.gif" CommandArgument='<%# Eval("CNFNO") %>'
                                                        AlternateText="Edit Record" OnClick="btnEdit_Click"
                                                        ToolTip='<%# Eval("CNFNO") %>' />
                                                </td>
                                                <td >
                                                    <%# Eval("DEGREE") %>   
                                                </td>
                                                <td>
                                                    <%# Eval("BRANCH") %>   
                                                </td>
                                                <td >
                                                    <%# Eval("INTAKE") %>   
                                                </td>
                                                <td >
                                                    <%# Eval("SC") %>   
                                                </td>
                                                <td >
                                                    <%# Eval("ST") %>   
                                                </td>
                                                <td >
                                                    <%# Eval("GEN") %>   
                                                </td>
                                                <td >
                                                    <%# Eval("OBC") %>   
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />

            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>

         <%--<script>
             $(document).ready(function () {

                 bindDataTable();
                 Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
             });

             function bindDataTable() {
                 var myDT = $('#divreservationlist').DataTable({

                 });
             }

        </script>--%>

</asp:Content>

