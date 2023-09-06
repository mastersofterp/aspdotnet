<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CollegeMaster.aspx.cs" Inherits="ACADEMIC_MASTERS_CollegeMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updCollege"
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

    <asp:UpdatePanel ID="updCollege" runat="server">
        <ContentTemplate>
            <div id="divMsg" runat="server">
            </div>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Style="color: Red"></asp:Label>
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">INSTITUTE MASTER</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Name</label>
                                        </div>
                                        <asp:TextBox ID="txtName" runat="server" TabIndex="1" CssClass="form-control"
                                            MaxLength="100" ToolTip="Please Enter Name" />
                                        <%--  <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtName" FilterType="LowercaseLetters, UppercaseLetters,Custom" ValidChars="" />--%>
                                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                                            Display="None" ErrorMessage="Please Enter Institute Name" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Location</label>
                                        </div>
                                        <asp:TextBox ID="txtLocName" runat="server" TabIndex="2" CssClass="form-control"
                                            MaxLength="100" ToolTip="Please Enter Location Name" />                                  
                                        <asp:RequiredFieldValidator ID="rfvLocName" runat="server" ControlToValidate="txtLocName"
                                            Display="None" ErrorMessage="Please Enter Location" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Short Name</label>
                                        </div>
                                        <asp:TextBox ID="txtShortName" runat="server"  TabIndex="3" CssClass="form-control"
                                            MaxLength="100" ToolTip="Please Enter Short Name" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtShortName"
                                            Display="None" ErrorMessage="Please Enter Short Name" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Code</label>
                                        </div>
                                        <asp:TextBox ID="txtcode" runat="server" TabIndex="4" CssClass="form-control"
                                            MaxLength="100" ToolTip="Please Enter Code" />
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcode"
                                            Display="None" ErrorMessage="Please Enter Code" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollegeType" runat="server" TabIndex="5" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True"
                                            ToolTip="Please Select Type">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="S">School</asp:ListItem>
                                            <asp:ListItem Value="C">College</asp:ListItem>
                                            <asp:ListItem Value="P">Partner Institutes</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCollegeType" runat="server" ControlToValidate="ddlCollegeType"
                                            Display="None" ErrorMessage="Please Select Type" ValidationGroup="submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                     OnClick="btnSave_Click" TabIndex="6" CssClass="btn btn-primary"/>
                                <asp:Button ID="btnShowReport" runat="server" CssClass="btn btn-info"
                                    TabIndex="7" Text="Report" ToolTip="Show Report"  OnClick="btnShowReport_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    TabIndex="8" OnClick="btnCancel_Click" CssClass="btn btn-warning"/>

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </div>

                            <div class="col-12">
                                <%-- <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">--%>
                                <asp:ListView ID="lvCollegeDetails" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Institute Details List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divcollegelist">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th style="text-align: center;">Edit
                                                    </th>
                                                    <th>Name
                                                    </th>
                                                    <th>Location
                                                    </th>
                                                    <th>Type
                                                    </th>
                                                    <th>Short Name
                                                    </th>
                                                    <th>Code
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
                                            <td style="text-align:center;">
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit1.png" CommandArgument='<%# Eval("COLLEGE_ID") %>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                            </td>
                                            <td>
                                                <%# Eval("COLLEGE_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("LOCATION")%>
                                            </td>
                                            <td>
                                                <%# Eval("COLLEGETYPE")%>
                                            </td>
                                            <td>
                                                <%# Eval("SHORT_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("CODE")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnShowReport"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btnShowReport"></asp:PostBackTrigger>
        </Triggers>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShowReport" />
        </Triggers>
    </asp:UpdatePanel>
    
    <%--<script>
        //$(document).ready(function () {

        //    bindDataTable();
        //    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        //});

        //function bindDataTable() {
        //    var myDT = $('#divcollegelist').DataTable({

        //    });
        //}

        </script>--%>
</asp:Content>



