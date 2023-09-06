<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage2.master" AutoEventWireup="true"
    CodeFile="Srcategory.aspx.cs" Inherits="ACADEMIC_MASTERS_Srcategory"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script type="text/javascript" language="javascript" src="../../Javascripts/FeeCollection.js"></script>
    <script type="text/javascript" language="javascript" src="../../includes/prototype.js"></script>
    <script type="text/javascript" language="javascript" src="../../includes/scriptaculous.js"></script>
    <script type="text/javascript" language="javascript" src="../../includes/modalbox.js"></script>--%>
    <%--<table cellpadding="2" cellspacing="2" border="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                MISCELLANIOUS FEES
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>--%>
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

    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Admission Intake Category Wise</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddldegree" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddldegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvdegree" runat="server" ControlToValidate="ddldegree"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Degree." InitialValue="0" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <label>SR Category</label>
                                        <asp:DropDownList ID="ddlsrcategory" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlsrcategory_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvsrcatgry" runat="server" ControlToValidate="ddlsrcategory"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please Select SR Category."
                                            InitialValue="0" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <label>College Code</label>
                                        <asp:DropDownList ID="ddlcollegecode" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlcollegecode_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">E021</asp:ListItem>
                                            <asp:ListItem Value="2">E057</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvcollegecode" runat="server" ControlToValidate="ddlcollegecode"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please Select College Code."
                                            InitialValue="0" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnshow" runat="server" TabIndex="4" CssClass="btn btn-primary" ValidationGroup="Submit" Text="Show" OnClick="btnshow_Click" />
                                <asp:Button ID="btnsubmit" runat="server" ValidationGroup="Submit" TabIndex="5" Enabled="false" CssClass="btn btn-primary" Text="Submit" OnClick="btnsubmit_Click" />
                                <asp:Button ID="btnclear" runat="server" TabIndex="6" CssClass="btn btn-warning" Text="Cancel" OnClick="btnclear_Click" />
                                <asp:ValidationSummary ID="validationsummary1" runat="server" ValidationGroup="Submit" EnableTheming="true"
                                    ShowMessageBox="true" ShowSummary="false" />

                            </div>


                            <div id="trListview" runat="server">
                                <div class="col-12">
                                    <asp:ListView ID="lvcategorylist" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>List of Branches</h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sr.No
                                                        </th>
                                                        <th>Branch Name
                                                        </th>
                                                        <th>Intake
                                                        </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <EmptyDataTemplate>
                                        </EmptyDataTemplate>
                                        <ItemTemplate>
                                            <%-- DO NOT FORMAT FOLLOWING 5-6 LINES. JAVASCRIPT IS DEPENDENT ON ELEMENT HIERARCHY --%>
                                            <tr class="item">
                                                <td>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </td>
                                                <td>
                                                    <%# Eval("LONGNAME")%>
                                                    <asp:HiddenField ID="hdncat" runat="server" Value='<%# Eval("INTAKEID")%>' />
                                                    <asp:HiddenField ID="hdnbranch" runat="server" Value='<%# Eval("BRANCHNO")%>' />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtintake" Width="70px" Text='<%# Eval("INTAKE")%>' runat="server" MaxLength="5" onkeyup="chk(this);"></asp:TextBox>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="divMsg" runat="server">
            </div>

            <script type="text/javascript">
                function chk(txt) {
                    if (isNaN(txt.value)) {
                        txt.value = '';
                        alert('Enter Numbers Only!');
                        txt.focus();
                        return;
                    }
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
