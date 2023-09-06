<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PaymentCategoryConfig.aspx.cs" Inherits="ACADEMIC_PaymentCategoryConfig" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <script>
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200
                });
            });
        });
    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPaymentCatg"
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

    <asp:UpdatePanel ID="updPaymentCatg" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Receipt Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlReceiptType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Payment Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlPaymentType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Fee Title</label>
                                        </div>
                                        <asp:ListBox ID="ddlFeeTitle" runat="server" SelectionMode="Multiple" TabIndex="1"
                                            CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" TabIndex="1" OnClientClick="return ValidateListBox();" />
                                <asp:Button ID="btnCancel" runat="server" Text="Clear" CausesValidation="False" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="1" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlPaymentCatg" runat="server">
                                    <asp:ListView ID="lvPaymentCategory" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Payment Category Configuration</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center;">Edit
                                                        </th>
                                                        <th>Receipt Type
                                                        </th>
                                                        <th>Payment Type
                                                        </th>
                                                        <th>Fee Title
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" OnClick="btnEdit_Click"
                                                        CommandArgument='<%# Eval("PCCNO")%>' AlternateText="Edit" ToolTip="Edit Record" TabIndex="1" />
                                                </td>
                                                <td>
                                                    <%# Eval("RECIEPT_TITLE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PAYTYPENAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FEE_LONGNAME")%>
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
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function ValidateListBox(sender, args) {

            var ddlReceiptType = $("[id$=ddlReceiptType]").attr("id");
            var ddlReceiptType = document.getElementById(ddlReceiptType);
            if (ddlReceiptType.value == 0) {
                alert('Please Select Receipt Type', 'Warning!');
                $(ddlReceiptType).focus();
                return false;
            }

            var ddlPaymentType = $("[id$=ddlPaymentType]").attr("id");
            var ddlPaymentType = document.getElementById(ddlPaymentType);
            if (ddlPaymentType.value == 0) {
                alert('Please Select Payment Type', 'Warning!');
                $(ddlPaymentType).focus();
                return false;
            }

            var options = document.getElementById("<%=ddlFeeTitle.ClientID%>").options;
            for (var i = 0; i < options.length; i++) {
                if (options[i].selected == true) {
                    args.IsValid = true;
                    return;
                }
            }
            alert('Please Select Atleast one Fee Title', 'Warning!');
            $(options).focus();
            return false;
        }
    </script>

</asp:Content>

