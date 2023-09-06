<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Str_Dept_Master.aspx.cs" Inherits="Stores_Masters_Str_Dept_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">DEPARTMENT MASTER</h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#abc">Sub Department</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#pqr">Main Department</a>
                            </li>
                        </ul>

                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane fade" id="abc">
                                <div>
                                    <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updatePanel1"
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

                                <asp:UpdatePanel ID="updatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <asp:Panel ID="pnl" runat="server">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                                            <div class="sub-heading">
                                                                <h5>Add/Edit Sub Department</h5>
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="spanDept" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Main Department</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlMainDepartment" AppendDataBoundItems="true" runat="server"
                                                                CssClass="form-control" data-select2-enable="true" TabIndex="1" ToolTip="Select Main Department">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlMainDepartment" runat="server" ControlToValidate="ddlMainDepartment"
                                                                Display="None" ErrorMessage="Please Select Main Department" ValidationGroup="store1"
                                                                InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="span1" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Sub Department Name</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSubDept" AppendDataBoundItems="true" runat="server"
                                                                CssClass="form-control" data-select2-enable="true" TabIndex="2" ToolTip="Select Sub Department Name">
                                                                <asp:ListItem Value="-1">--Please Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvtxtSubDepartmentName" runat="server" ControlToValidate="ddlSubDept"
                                                                Display="None" ErrorMessage="Please Select Sub Department Name" ValidationGroup="store1" InitialValue="0"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="span2" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Sub Department Short Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSubDepartmentShortName" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Select Sub Department Short Name"
                                                                MaxLength="100" onKeyUp="LowercaseToUppercase(this);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtSubDepartmentShortName" runat="server" ControlToValidate="txtSubDepartmentShortName"
                                                                Display="None" ErrorMessage="Please Enter Sub Department Short Name" ValidationGroup="store1"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtSubDepartmentShortNameFilter" runat="server"
                                                                FilterMode="ValidChars" FilterType="LowercaseLetters, UppercaseLetters,Custom"
                                                                TargetControlID="txtSubDepartmentShortName" ValidChars=" ">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" style="display:none;">
                                                            <div class="label-dynamic">
                                                                <sup> </sup>
                                                                <label>Show</label>
                                                            </div>
                                                            <asp:RadioButton runat="server" ID="radYes" GroupName="radDept" TabIndex="4" Text="Yes" Checked="True" />
                                                            <asp:RadioButton runat="server" ID="radNo" GroupName="radDept" TabIndex="5" Text="No" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                            <div class="col-12 btn-footer">
                                                <asp:HiddenField ID="selected_tab" runat="server" />
                                                <asp:Button ID="butSubDepartment" ValidationGroup="store1" Text="Submit" runat="server"
                                                    CssClass="btn btn-primary" TabIndex="6" ToolTip="Click To Submit" OnClick="butSubDepartment_Click" />
                                                <asp:Button ID="btnShowrpt1" Text="Report" runat="server" CssClass="btn btn-info" TabIndex="7" ToolTip="Click To Show Report" OnClick="btnShowrpt1_Click"/>
                                                <asp:Button ID="butSubDepartmentCancel" Text="Cancel" runat="server" CssClass="btn btn-warning" TabIndex="7" ToolTip="Click To Reset"
                                                    OnClick="butSubDepartmentCancel_Click" />
                                                
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="store1"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>
                                             
                                            <asp:Panel ID="pnlSubDepartment" runat="server">
                                                <div class="col-12">
                                                    <asp:ListView ID="lvSubDepartment" runat="server">
                                                        <EmptyDataTemplate>
                                                            <center>
                                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                            </center>
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Sub Department master</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Action
                                                                        </th>
                                                                        <th>Dept Name
                                                                        </th>
                                                                        <th>Sub Dept Name
                                                                        </th>
                                                                        <%-- <th>
                                                                                Sub Dept Short Name
                                                                            </th>--%>
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
                                                                    <asp:ImageButton ID="btnEditParty" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("SDNO") %>'
                                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditSubDepartment_Click" />&nbsp;
                                                                    <%--<img src="../../Images/edit1.gif" />--%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("MDNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SDNAME")%>
                                                                </td>
                                                                <%--<td>
                                                                        <%# Eval("SNAME")%>
                                                                    </td>--%>
                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:ListView>
                                                    <%--<div class="vista-grid_datapager text-center">
                                                        <asp:DataPager ID="dpPagerSubDepartment" runat="server" PagedControlID="lvSubDepartment"
                                                            PageSize="10" OnPreRender="dpPagerSubDepartment_PreRender">
                                                            <Fields>
                                                                <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                                                <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                                <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                                                            </Fields>
                                                        </asp:DataPager>
                                                    </div>--%>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane active" id="pqr">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatePanel3"
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

                                <asp:UpdatePanel ID="updatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <asp:Panel ID="Panel3" runat="server">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                                            <div class="sub-heading">
                                                                <h5>Add/Edit Main Department</h5>
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="span3" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Department Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtDepartmentName" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Enter Department Name"
                                                                MaxLength="100" onKeyUp="LowercaseToUppercase(this);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvDepartmentName" runat="server" ControlToValidate="txtDepartmentName"
                                                                Display="None" ErrorMessage="Please Enter Department Name" ValidationGroup="store"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtDepartmentNameFilter" runat="server"
                                                                FilterMode="ValidChars" FilterType="LowercaseLetters, UppercaseLetters,Custom"
                                                                TargetControlID="txtDepartmentName" ValidChars=" ">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="span4" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Department Short Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtDepartmentShortName" runat="server"
                                                                CssClass="form-control" TabIndex="2" ToolTip="Enter Department Short Name" MaxLength="100" onKeyUp="LowercaseToUppercase(this);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvDepartmentShortName" runat="server" ControlToValidate="txtDepartmentShortName"
                                                                Display="None" ErrorMessage="Please Enter Department Short Name" ValidationGroup="store"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtSubDeptShortNameFilter" runat="server"
                                                                FilterMode="ValidChars" FilterType="LowercaseLetters, UppercaseLetters,Custom"
                                                                TargetControlID="txtDepartmentShortName" ValidChars=" ">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="butDepartMent" ValidationGroup="store" Text="Submit" runat="server"
                                                    CssClass="btn btn-primary" TabIndex="3" ToolTip="Click To Submit" OnClick="butDepartMent_Click" />
                                                <asp:Button ID="btnShowReport" Text="Report" runat="server" CssClass="btn btn-info" TabIndex="4" ToolTip="Click To Show Report" OnClick="btnshow_Click" />
                                                <asp:Button ID="butbutDepartMentCancel" Text="Cancel" runat="server"
                                                    CssClass="btn btn-warning" TabIndex="5" ToolTip="Click To Reset" OnClick="butbutDepartMentCancel_Click" />
                                                
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="store"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>

                                            <asp:Panel ID="pnlDepartment" runat="server">
                                                <div class="col-12">
                                                    <asp:ListView ID="lvDepartment" runat="server">
                                                        <EmptyDataTemplate>
                                                            <center>
                                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                            </center>
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Department</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Action
                                                                        </th>
                                                                        <th>Dept Name
                                                                        </th>
                                                                        <th>Dept Short Name
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
                                                                    <asp:ImageButton ID="btnEditPartyCategory" runat="server" ImageUrl="~/Images/edit.png"
                                                                        CommandArgument='<%# Eval("MDNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                        OnClick="btnEditDepartment_Click" />&nbsp;
                                                                </td>
                                                                <td>
                                                                    <%# Eval("MDNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("MDSNAME")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:ListView>
                                                           
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                </div>
            </div>   
        </div>   
    </div>   

                <script type="text/javascript">
                    function LowercaseToUppercase(txt) {
                        var textValue = txt.value;
                        txt.value = textValue.toUpperCase();

                    }

                </script>
                <script type="text/javascript">
                    var selected_tab = 1;
                    $(function () {
                        var tabs = $("#tabs").tabs({
                            select: function (e, i) {
                                selected_tab = i.index;
                            }
                        });
                        selected_tab = $("[id$=selected_tab]").val() != "" ? parseInt($("[id$=selected_tab]").val()) : 0;
                        tabs.tabs('select', selected_tab);
                        $("form").submit(function () {
                            $("[id$=selected_tab]").val(selected_tab);
                        });
                    });

                </script>
</asp:Content>
