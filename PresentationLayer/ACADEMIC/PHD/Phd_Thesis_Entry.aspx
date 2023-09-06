<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Phd_Thesis_Entry.aspx.cs" Inherits="ACADEMIC_PHD_Phd_Thesis_Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>


    <style>
        #ctl00_ContentPlaceHolder1_pnlSession .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        .dataTables_scrollHeadInner {
            width: max-content!important;
        }
    </style>
    <style>
        .Tab:focus {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }

        .Background {
            background-color: black;
            opacity: 0.9;
        }
    </style>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSession"
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

    <asp:UpdatePanel ID="updSession" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">SESSION CREATION</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server">Ph.D. Thesis Entry</asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Thesis Submission Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="dvcal1" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtThesisdate" runat="server" ValidationGroup="submit" onpaste="return false; __/__/____"
                                                TabIndex="1" ToolTip="Please Enter Thesis Submission Date" CssClass="form-control" Style="z-index: 0;" OnClientDateSelectionChanged="checkDate" />
                                            <ajaxToolKit:CalendarExtender ID="ceDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtThesisdate" PopupButtonID="dvcal1" OnClientDateSelectionChanged="checkDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtThesisdate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevDate" runat="server" ControlExtender="meeDate"
                                                ControlToValidate="txtThesisdate" EmptyValueMessage="Please Enter Thesis Submission Date"
                                                InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                TooltipMessage="Please Enter Thesis Submission Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                ValidationGroup="submit" SetFocusOnError="True" />
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True"
                                                ErrorMessage="Please Enter Thesis Submission Date" ControlToValidate="txtThesisdate"
                                                Display="None" ValidationGroup="submit" />--%>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Thesis Title</label>
                                        </div>
                                        <asp:TextBox ID="txtTitleThesis" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="300" TabIndex="2"
                                            ToolTip="Please Enter Thesis Title" placeholder="Enter Thesis Title" />

                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender36" runat="server" TargetControlID="txtTitleThesis" FilterMode="InvalidChars"
                                            InvalidChars="~`!@#$%^&*+<>?">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Synopsis Submission Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="dvcal2" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtSynopsisDate" runat="server" ValidationGroup="submit" onpaste="return false; __/__/____"
                                                TabIndex="3" ToolTip="Please Enter Synopsis Submission Date" CssClass="form-control" Style="z-index: 0;" OnClientDateSelectionChanged="checkDate" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtSynopsisDate" PopupButtonID="dvcal2" OnClientDateSelectionChanged="checkDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtSynopsisDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeDate"
                                                ControlToValidate="txtSynopsisDate" EmptyValueMessage="Please Enter Synopsis Submission"
                                                InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                TooltipMessage="Please Enter Synopsis Submission" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                ValidationGroup="submit" SetFocusOnError="True" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                        TabIndex="4" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="5" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                        ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                </div>
                            </div>

                            <div class="col-12" id="divTracking" runat="server">
                                <div class="sub-heading">
                                    <h5>Student List</h5>
                                </div>
                                <asp:Panel ID="pnlTracking" runat="server">
                                    <asp:ListView ID="lvTracking" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <asp:PlaceHolder runat="server" ID="PlaceHolder1" />
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <asp:PlaceHolder runat="server" ID="itemPlaceHolder1" />
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <div class="table-responsive">
                                        <asp:GridView ID="grvStatusDetails" runat="server" OnSelectedIndexChanged="grvStatusDetails_SelectedIndexChanged" CssClass="table table-striped table-bordered nowrap" Style="width: 100%">
                                            <Columns>
                                                <asp:CommandField HeaderText="Show" ShowSelectButton="True" SelectImageUrl="~/Images/view.png" ButtonType="Image"></asp:CommandField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <asp:LinkButton ID="lnkPopup" runat="server"></asp:LinkButton>
                                </asp:Panel>
                            </div>


                            <asp:Panel ID="pnlHistory" runat="server">
                                <asp:UpdatePanel ID="updMaindiv" runat="server">
                                    <ContentTemplate>
                                        <div class="modal-dialog  modal-lg">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h4 class="modal-title" style="font-weight: bold">Status History</h4>
                                                    <button type="button" id="btnClose" class="close" data-dismiss="modal">&times;</button>
                                                </div>

                                                <div class="modal-body" style="padding: 2rem">
                                                    <div class="col-12">

                                                        <asp:ListView ID="lvstustslist" runat="server" RepeatDirection="Vertical">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap table-responsive">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th style="padding: 10px">Status</th>
                                                                            <th style="padding: 10px">Remarks</th>
                                                                        </tr>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="padding: 10px">
                                                                        <asp:Label ID="lblStatusname" runat="server" Text='<%# Eval("STATUSNAME")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="padding: 10px">
                                                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("REMARK")%>'></asp:Label>
                                                                    </td>
                                                                </tr>

                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                            <ajaxToolKit:ModalPopupExtender ID="MpHistory" runat="server" TargetControlID="lnkPopup" PopupControlID="pnlHistory" BackgroundCssClass="Background" CancelControlID="btnClose"></ajaxToolKit:ModalPopupExtender>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function checkDate(sender, args) {
            // I change the < operator to >
            if (sender._selectedDate > new Date()) {
                alert("Unable to select future date !!!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value('')
            }

        }
    </script>

</asp:Content>

