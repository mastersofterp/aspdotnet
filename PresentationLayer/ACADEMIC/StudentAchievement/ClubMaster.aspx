<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ClubMaster.aspx.cs" Inherits="ACADEMIC_ClubActivityMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:HiddenField ID="hfdActiveClub" runat="server" ClientIDMode="Static" />

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updclub"
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


    <asp:UpdatePanel ID="updclub" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Club Master</h3>
                            <%--<h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>--%>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Club Activity Type</label>
                                        </div>
                                        <asp:TextBox ID="txttypeactivity" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="100" TabIndex="1"
                                            ToolTip="Please Enter Type of Activity" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Enter Club Activity Type" ControlToValidate="txttypeactivity"
                                            Display="None" ValidationGroup="submit" />

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Incharge</label>
                                        </div>
                                        <asp:DropDownList ID="ddlIncharge" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvIncharge" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Select Incharge" InitialValue="0" ControlToValidate="ddlIncharge"
                                            Display="None" ValidationGroup="submit" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Email</label>
                                        </div>
                                        <asp:TextBox ID="txtemail" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="100" TabIndex="2"
                                            ToolTip="Please Enter EmailId" />
                                        <%--  <asp:RequiredFieldValidator ID="rfvemail" runat="server" ControlToValidate="txtemail" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Enter Email"  ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email Address" ControlToValidate="txtemail"
                                            ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" SetFocusOnError="True" Display="None" ValidationGroup="submit" />

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Total Registration Limit</label>
                                        </div>
                                        <asp:TextBox ID="txtregno" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="3" TabIndex="3"
                                            ToolTip="Please Enter Registration No" onkeydown="return (event.keyCode!=13);" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteBankName" runat="server"
                                            TargetControlID="txtregno"
                                            FilterType="Custom,Numbers"
                                            FilterMode="ValidChars"
                                            ValidChars=" ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <%--   <asp:RequiredFieldValidator ID="rfvregno" runat="server" ControlToValidate="txtregno" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Enter Registration No"  ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        --%>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Status</label>
                                                </div>
                                                <div class="switch form-inline">
                                                    <input type="checkbox" id="rdActiveClub" name="switch" checked />
                                                    <label data-on="Active" data-off="InActive" for="rdActiveClub"></label>
                                                </div>
                                            </div>
                                            <%--<div class="form-group col-6">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Status</label>
                                                </div>
                                                <asp:CheckBox ID="chkActive" runat="server" ForeColor="#005500" TabIndex="4" onclick="return show(chkActive);" Checked="true" />
                                            </div>--%>


                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="validate();" ValidationGroup="submit"
                                    TabIndex="5" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnReport" runat="server" Text="Report"
                                    TabIndex="6" CssClass="btn btn-info" Style="display: none;" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    TabIndex="7" CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                            </div>

                            <div class="col-12 mt-3">
                                <asp:Panel ID="pnlclub" runat="server">
                                    <asp:ListView ID="lvclub" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center;">Action
                                                        </th>
                                                        <th>Club Activity Type
                                                        </th>
                                                        <th>Incharge
                                                        </th>
                                                        <th>Email
                                                        </th>
                                                        <th>Total Registration Limit
                                                        </th>
                                                        <th>ActiveStatus
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
                                                <td style="text-align: center;">
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("CLUB_ACTIVITY_NO")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                    <%--<asp:ImageButton ID="ImageButton1" runat="server"  CommandArgument='<%# Eval("CLUBNO")%>'
                                                    ImageUrl="~/images/edit.gif" />--%>
                                                   
                                                </td>
                                                <td>
                                                    <%# Eval("CLUB_ACTIVITY_TYPE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("INCHARGE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("EMAIl") %>
                                                </td>
                                                <td>
                                                    <%# Eval("TOTAL_REGNO_LIMIT") %>
                                                </td>
                                                <td>
                                                    <%--<%# Eval("ACTIVESTATUS")%>--%>
                                                     <asp:Label ID="lblStatus" runat="server" CssClass="badge" Text='<%# Eval("ACTIVESTATUS") %>'></asp:Label>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divMsg" runat="server">
                </div>
            </div>

        </ContentTemplate>

    </asp:UpdatePanel>
    <script>
        function SetActiveClub(val) {

            $('#rdActiveClub').prop('checked', val);

        }
        function validate() {

            $('#hfdActiveClub').val($('#rdActiveClub').prop('checked'));

        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });
    </script>
</asp:Content>
