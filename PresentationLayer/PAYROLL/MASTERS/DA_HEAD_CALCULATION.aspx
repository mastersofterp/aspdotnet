<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DA_HEAD_CALCULATION.aspx.cs" Inherits="PAYROLL_MASTERS_DA_HEAD_CALCULATION" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">DA / HRA HEAD CALCULATION</h3>
                        </div>
                        <div class="box-body">
                            <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                            <asp:Panel ID="pnlPfMaster" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Add/Edit DA HEAD </h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">

                                        <%--   <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>DA / HRA HEAD Description</label>
                                        </div>--%>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>DA / HRA Description</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDAHRA" runat="server" ToolTip="Select DA/HRA head Desc" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlDAHRA"
                                                ValidationGroup="submit" ErrorMessage="Please select DA /HRA" SetFocusOnError="true"
                                                InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                        </div>



                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>DA Percentage</label>
                                            </div>
                                            <asp:TextBox ID="txtDAper" runat="server" CssClass="form-control" TabIndex="2" MaxLength="16" ToolTip="Enter DA Percentage" />
                                            <asp:RequiredFieldValidator ID="rfvtxtminlimit" runat="server" ControlToValidate="txtDAper"
                                                Display="None" ErrorMessage="Enter DA Percentage" ValidationGroup="submit"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>

                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeDAper" runat="server"
                                                TargetControlID="txtDAper"
                                                FilterType="Custom"
                                                FilterMode="ValidChars"
                                                ValidChars="0123456789.">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>HRA Percentage</label>
                                            </div>
                                            <asp:TextBox ID="txtHRAPer" runat="server" CssClass="form-control" TabIndex="3" MaxLength="16" ToolTip="Enter HRA Percentage" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtHRAPer"
                                                Display="None" ErrorMessage="Enter HRA Percentage" ValidationGroup="submit"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>

                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                TargetControlID="txtHRAPer"
                                                FilterType="Custom"
                                                FilterMode="ValidChars"
                                                ValidChars="0123456789.">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                   <sup>* </sup>
                                                <label>Effect From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgCalDateOfBirth" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtEffectDate" runat="server" CssClass="form-control" Enabled="true" ValidationGroup="emp"
                                                    TabIndex="4" />
                                                <ajaxToolKit:CalendarExtender ID="ceBirthDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtEffectDate" PopupButtonID="imgCalDateOfBirth" Enabled="true"
                                                    EnableViewState="true" OnClientDateSelectionChanged="CheckDateEalier">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeBirthDate" runat="server" TargetControlID="txtEffectDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meeBirthDate"
                                                    ControlToValidate="txtEffectDate" EmptyValueMessage="Please Enter Effect Date"
                                                    InvalidValueMessage="BirthDate is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                    TooltipMessage="Please Enter Birth Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                    ValidationGroup="emp" SetFocusOnError="True" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEffectDate"
                                                    Display="None" ErrorMessage="Please select Effect Date" ValidationGroup="submit"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>



                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">

                                                <label>Yearly HRA Calculation</label>
                                            </div>
                                            <asp:CheckBox runat="server" ID="chkYrHRACal" AutoPostBack="true" OnCheckedChanged="chkYrHRACal_CheckedChanged" TabIndex="5" />
                                        </div>


                                    </div>
                                </div>
                                <div class="col-12">

                                    <div class="row" runat="server" id="divHracal">

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>From Year</label>
                                            </div>
                                            <asp:TextBox ID="txtFrYr" runat="server" CssClass="form-control" TabIndex="6" MaxLength="16" ToolTip="Enter From Year" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFrYr"
                                                Display="None" ErrorMessage="Enter From Year" ValidationGroup="Add"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>

                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                TargetControlID="txtFrYr"
                                                FilterType="Custom"
                                                FilterMode="ValidChars"
                                                ValidChars="0123456789.">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>To Year</label>
                                            </div>
                                            <asp:TextBox ID="txttoyr" runat="server" CssClass="form-control" TabIndex="7" MaxLength="16" ToolTip="Enter To Year" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txttoyr"
                                                Display="None" ErrorMessage="Enter To Year" ValidationGroup="Add"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>

                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                TargetControlID="txttoyr"
                                                FilterType="Custom"
                                                FilterMode="ValidChars"
                                                ValidChars="0123456789.">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>HRA Percent</label>
                                            </div>
                                            <asp:TextBox ID="txtHraPernew" runat="server" CssClass="form-control" TabIndex="8" MaxLength="16" ToolTip="Enter HRA Percent" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtHraPernew"
                                                Display="None" ErrorMessage="Enter HRA Percent" ValidationGroup="Add"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>

                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                TargetControlID="txtHraPernew"
                                                FilterType="Custom"
                                                FilterMode="ValidChars"
                                                ValidChars="0123456789.">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <asp:Button runat="server" ID="btnAdd" Text="ADD" OnClick="btnAdd_Click" CssClass="btn btn-info" ValidationGroup="Add" TabIndex="9" />
                                            <asp:ValidationSummary ID="ValidationSummary2" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Add" runat="server" />
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">

                                            <asp:ListView ID="lstHraList" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>HRA HEAD CALCULATION LIST</h5>
                                                    </div>
                                                    <table class="table table-striped" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>

                                                                <th>From Year
                                                                </th>
                                                                <th>To Year
                                                                </th>
                                                                <th>HRA Per.
                                                                </th>
                                                                <th>Delete
                                                                </th>

                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtFromYear" runat="server" Text='<%# Eval("FROM_YEAR") %>' Enabled="false" CssClass="form-control" Height="39px"></asp:TextBox>
                                                        </td>

                                                        <td>
                                                            <asp:TextBox ID="txtToYear" runat="server" Text='<%# Eval("TO_YEAR") %>' Enabled="false" CssClass="form-control" Height="39px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtHRAPer" runat="server" Text='<%# Eval("HEAD_PER") %>' Enabled="false" CssClass="form-control" Height="39px"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: center">
                                                            <asp:ImageButton ID="btnDelete" TabIndex="8" runat="server" AlternateText="Delete Record" CommandArgument='<%# Container.DataItemIndex + 1%>'
                                                                ImageUrl="~/Images/delete.png" OnClick="btnDelete_Click" ToolTip="Delete Record" />
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>

                                        </div>


                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnsubmit" runat="server" TabIndex="10" Text="Submit" ValidationGroup="submit" OnClick="btnsubmit_Click"
                                        ToolTip="Submit" CssClass="btn btn-primary" />
                                    <asp:Button ID="btncancel" runat="server" TabIndex="11" Text="Cancel" OnClick="btncancel_Click" CausesValidation="False"
                                        ToolTip="Cancel" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="submit" runat="server" />
                                </div>

                            </asp:Panel>



                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvheaddescription" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>DA HEAD CALCULATION LIST</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>DA HEAD Description
                                                        </th>
                                                        <th>DA Per.
                                                        </th>
                                                        <th>HRA Per.
                                                        </th>
                                                        <th>Effect Date
                                                        </th>
                                                        <th>Yearly HRA Cal.
                                                        </th>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="../../Images/edit.png" CommandArgument='<%# Eval("DA_HEADID") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="12" />

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDAHeadDescription" runat="server" Text='<%# Eval("DA_HEAD_DESCRIPTION") %>' />
                                                    <asp:HiddenField ID="hdnDaHeadId" Value='<%# Eval("DA_HEADID") %>' runat="server" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDAHEADPER" runat="server" Text='<%# Eval("DA_PER") %>' Enabled="false" CssClass="form-control" Height="39px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHRAPer" runat="server" Text='<%# Eval("HRA_PER") %>' Enabled="false" CssClass="form-control" Height="39px"></asp:TextBox>
                                                </td>
                                                <td>

                                                    <asp:TextBox ID="txtdaheaddate" runat="server" CssClass="form-control" Height="39px" Enabled="false" Text='<%# Eval("DA_HEAD_CALCULATION_DATE") %>'></asp:TextBox>
                                                </td>
                                                <td>

                                                    <asp:HiddenField ID="hdnDetail" Value='<%# Eval("IS_DETAILCAL") %>' runat="server" />
                                                    <asp:Label ID="lblIsDetail" runat="server" Text='<%# (Convert.ToInt32(Eval("IS_DETAILCAL") )== 0 ?  "No" : "YES" )%>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>


                            <div class="vista-grid_datapager d-none">
                                <div class="text-center">
                                    <asp:DataPager ID="DataPager1" runat="server" OnPreRender="DataPager1_PreRender" PagedControlID="lvheaddescription"
                                        PageSize="100">
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
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server"></div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" src="https://www.google.com/jsapi">
    </script>
    <script src="../../INCLUDES/transliteration.l.js"></script>
    <script type="text/javascript">
        // Load the Google Transliterate API
        google.load("elements", "1", {
            packages: "transliteration"
        });

        function onLoad() {
            var options = {
                sourceLanguage:
                google.elements.transliteration.LanguageCode.ENGLISH,
                destinationLanguage:
                [google.elements.transliteration.LanguageCode.KANNADA],
                shortcutKey: 'ctrl+e',
                transliterationEnabled: true
            };

            // Create an instance on TransliterationControl with the required
            // options.
            var control =
            new google.elements.transliteration.TransliterationControl(options);

            // Enable transliteration in the textbox with id
            // 'transliterateTextarea'.
            control.makeTransliteratable(['ctl00_ContentPlaceHolder1_txtDeptKannad']);
        }
        google.setOnLoadCallback(onLoad);
    </script>

    <script type="text/javascript" language="javascript">
        function totalAppointment(chkcomplaint) {
            var frm = document.forms[0];
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (chkcomplaint.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>

</asp:Content>
