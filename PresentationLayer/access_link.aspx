<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="access_link.aspx.cs" Inherits="access_links" %>

<%@ Register Assembly="MangoChat" Namespace="ASPNETChatControl" TagPrefix="cc2" %>

<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="Content/jquery.js"></script>--%>
    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdTrans" runat="server" ClientIDMode="Static" />
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updlink"
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


    <asp:UpdatePanel ID="updlink" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">ACCESS LINK MANAGEMENT</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-4 col-12">
                                            <div class="label-dynamic">
                                                <label>User Access Domain</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDomain" OnSelectedIndexChanged="ddlDomain_SelectedIndexChanged"
                                                AutoPostBack="true" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvAccessDomain" runat="server" ControlToValidate="ddlDomain"
                                                Display="None" ErrorMessage="User Access Domain Required" InitialValue="-1">
                                            </asp:RequiredFieldValidator>

                                            <%--<ajaxToolKit:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlParentLink"
                                                Category="City" PromptText="Please Select" LoadingText="[Loading Parent Links...]"
                                                ServicePath="WebService.asmx" ServiceMethod="GetParentLinks" ParentControlID="ddlDomain" />--%>
                                        </div>

                                        <div class="form-group col-lg-1 col-md-1 col-12">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:Label ID="lbldomain" runat="server" Font-Bold="true" CssClass="form-control"></asp:Label>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-4 col-12">
                                            <div class="label-dynamic">
                                                <label>Parent Link Level</label>
                                            </div>
                                            <asp:DropDownList ID="ddllevel" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddllevel_SelectedIndexChanged"
                                                AutoPostBack="true" AppendDataBoundItems="true">
                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Level 1</asp:ListItem>
                                                <asp:ListItem Value="2">Level 2</asp:ListItem>
                                            </asp:DropDownList>

                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddllevel"
                                                Display="None" ErrorMessage="Link Level Required" InitialValue="-1"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-1 col-md-1 col-12">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:Label ID="lbllevel" runat="server" Font-Bold="true" CssClass="form-control"></asp:Label>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-4 col-12">
                                            <div class="label-dynamic">
                                                <label>Parent Link </label>
                                            </div>
                                            <asp:DropDownList ID="ddlParentLink" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlParentLink_SelectedIndexChanged"
                                                AutoPostBack="true" AppendDataBoundItems="true">
                                                <%-- <asp:ListItem Value="-1">Please Select</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-1 col-md-1 col-12">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:Label ID="lblsrno" runat="server" Font-Bold="true" CssClass="form-control"></asp:Label>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Link Title</label>
                                            </div>
                                            <asp:TextBox ID="txtLinkTitle" runat="server" CssClass="form-control" MaxLength="50" />
                                            <asp:RequiredFieldValidator ID="rfvLinkTitle" runat="server" ControlToValidate="txtLinkTitle"
                                                Display="None" ErrorMessage="Link Title Required"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Link URL </label>
                                            </div>
                                            <asp:TextBox ID="txtLinkUrl" runat="server" CssClass="form-control" MaxLength="150" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Link Sr. No. </label>
                                            </div>
                                            <asp:TextBox ID="txtSrNo" runat="server" CssClass="form-control" MaxLength="7" />
                                            <asp:RequiredFieldValidator ID="rfvSrNo" runat="server" ControlToValidate="txtSrNo"
                                                Display="None" ErrorMessage="Sr No Required"></asp:RequiredFieldValidator>
                                        </div>

                                        <%--<div class="form-group col-lg-3 col-md-3 col-12">
                                            <div class="label-dynamic">
                                                <label>Active Status </label>
                                            </div> 
                                            <asp:CheckBox ID="chknlstatus" runat="server" TextAlign="Left"/>
                                        </div>--%>

                                        <!--===== Added By Rishabh on Dated 28/10/2021=====-->
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="row">
                                                <div class="form-group col-6">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="rdActive" name="switch" checked />
                                                        <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--===== Added By Nikhil L. on Dated 24/04/2023=====-->
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="row">
                                                <div class="form-group col-6">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Transaction</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="chkTrans" name="chkTrans" />
                                                        <label data-on="Yes" data-off="No" for="chkTrans"></label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--===== Added By Nikhil L. on Dated 24/04/2023=====-->
                                        <div class="form-group col-lg-4 col-md-12 col-12">
                                            <div class=" note-div">
                                                <h5 class="heading">Note (Please Select)</h5>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Checked : Status- - <span style="color: green; font-weight: bold">Active</span></span>  </p>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>UnChecked : Status - <span style="color: red; font-weight: bold">InActive</span></span></p>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return validate();" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                        CausesValidation="False" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" />
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlList" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Domain</label>
                                            </div>
                                            <asp:DropDownList ID="ddlModule" runat="server" AppendDataBoundItems="true" AutoPostBack="true" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlModule_SelectedIndexChanged" CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Bulk Updation</label>
                                            </div>
                                            <asp:CheckBox ID="chk_Edit" runat="server" OnSelectedIndexChanged="chk_Edit" OnCheckedChanged="chk_Edit_CheckedChanged"
                                                AutoPostBack="true" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" CssClass="btn btn-primary">Add New</asp:LinkButton>
                                </div>

                                <div class="col-12">
                                    <asp:UpdatePanel ID="updLinks" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:ListView ID="lvALinks" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Access Link List</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Action
                                                                    </th>
                                                                    <th>Domain
                                                                    </th>
                                                                    <th>Link Title
                                                                    </th>
                                                                    <th>Link URL
                                                                    </th>
                                                                    <th>Link Sr.No.
                                                                    </th>
                                                                    <th>Level No.
                                                                    </th>
                                                                    <th>Status
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
                                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("al_no")%>'
                                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                            </td>
                                                            <td>
                                                                <%# Eval("as_title")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("al_link")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("al_url")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("SrNo")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("LevelNo")%>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblactinestatus" runat="server" Text='<%# Eval("Active_Status")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlListEdit" runat="server" Visible="false">
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btn_UpdateAll_Edit" runat="server" Text="Update All" OnClick="btn_UpdateAll_Edit_Click" Visible="false" CssClass="btn btn-primary" />
                                </div>
                                <div class="col-12">
                                    <asp:UpdatePanel ID="updLinks_Edit" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel2" runat="server">
                                                <asp:ListView ID="lvALinks_Edit" runat="server" OnItemDataBound="lvALinks_Edit_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Access Link List Edited Mode</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="table1">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Al No
                                                                    </th>
                                                                    <th>Domain
                                                                    </th>
                                                                    <th>Link Title
                                                                    </th>
                                                                    <th>Link URL
                                                                    </th>
                                                                    <th>AS No
                                                                    </th>
                                                                    <th>Master No
                                                                    </th>
                                                                    <th>Sr No
                                                                    </th>
                                                                    <th>Level No
                                                                    </th>
                                                                    <th>Active Status
                                                                        <%--<asp:Label Text="Active_Status" runat="server"></asp:Label>--%>
                                                                        <asp:CheckBox ID="cbHead" runat="server" ToolTip="Select or Deselect All Records" CssClass="form-control" />
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
                                                                <asp:Label ID="lbl_al_no_Edit" runat="server" Text='<%# Eval("al_no")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txt_as_title_Edit" runat="server" Text=' <%# Eval("as_title")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txt_al_link_Edit" runat="server" Text='<%# Eval("al_link")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txt_al_url_Edit" runat="server" Text='<%# Eval("al_url")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txt_al_asno_Edit" runat="server" Text='<%# Eval("AL_ASNo")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Mastno_Edit" runat="server" Text='<%# Eval("MastNo")%>'></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Srno_Edit" runat="server" Text='<%# Eval("SrNo")%>' CssClass="form-control" Width="70px"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_Srno_Edit"
                                                                    ValidChars=".0123456789">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Level_No_Edit" runat="server" Text='<%# Eval("LevelNo")%>' CssClass="form-control"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("Active_Status")%>' CssClass="form-control" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        //function totAllSubjects(chklnkstatus) {
        //    var frm = document.forms[0]
        //    for (i = 0; i < document.forms[0].elements.length; i++) {
        //        var e = frm.elements[i];
        //        if (e.type == 'checkbox') {
        //            if (chklnkstatus.checked == true) {
        //                if (e.ch00000ecked == false)
        //                    e.checked = true;
        //            }
        //            else {
        //                if (e.disabled == false)
        //                    e.checked = false;
        //            }
        //        }
        //    }
        //}
        //Added By Hemanth G
        $(function () {
            $("#table1 [id*=cbHead]").click(function () {
                if ($(this).is(":checked")) {
                    $("#table1 [id*=cbRow]").attr("checked", "checked");
                } else {
                    $("#table1 [id*=cbRow]").removeAttr("checked");
                }
            });
            $("#table1 [id*=cbRow]").click(function () {
                if ($("#table1 [id*=cbRow]").length == $("#table1 [id*=cbRow]:checked").length) {
                    $("#table1 [id*=cbHead]").attr("checked", "checked");
                } else {
                    $("#table1 [id*=cbHead]").removeAttr("checked");
                }
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            $("#table1 [id*=cbHead]").click(function () {
                if ($(this).is(":checked")) {
                    $("#table1 [id*=cbRow]").attr("checked", "checked");
                } else {
                    $("#table1 [id*=cbRow]").removeAttr("checked");
                }
            });
            $("#table1 [id*=cbRow]").click(function () {
                if ($("#table1 [id*=cbRow]").length == $("#table1 [id*=cbRow]:checked").length) {
                    $("#table1 [id*=cbHead]").attr("checked", "checked");
                } else {
                    $("#table1 [id*=cbHead]").removeAttr("checked");
                }
            });
        });

        //Added By Hemanth G

    </script>
    <script>
        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }

        function SetStatTrans(val) {
            $('#chkTrans').prop('checked', val);
            //$('#hfdTrans').val($('#chkTrans').prop('checked'));
        }
        function validate() {
            if (Page_ClientValidate()) {
                $('#hfdActive').val($('#rdActive').prop('checked'));
                $('#hfdTrans').val($('#chkTrans').prop('checked'));
            }
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
