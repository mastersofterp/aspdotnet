<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InstalmentAmount.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_StudentDocumentList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../plugins/jQuery/jquery_ui_min/jquery-ui.min.css" rel="stylesheet" />
    <script src="../plugins/jQuery/jquery_ui_min/jquery-ui.min.js"></script>
<%--    <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upBulkInstalment"
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

    <script type="text/javascript">
        function exefunction() {
            debugger;
            var count = 0;
            list = 'lvDocumentList';
            var dataRows = document.getElementsByTagName('tr');
            if (dataRows != null) {
                for (i = 0; i < dataRows.length - 1; i++) {

                    if (document.getElementById("ctl00_ContentPlaceHolder1_lvDocumentList_ctrl" + i + "_chkOriCopy").checked) {
                        count++;
                    }
                }
                if (count == 0) {
                    alert("Please check atleast one check box !!!");
                    return false;
                }
            }
        }
    </script>

    <asp:UpdatePanel ID="upBulkInstalment" runat="server">
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
                            <div class="col-12 btn-footer">
                                <div class="row">
                                    <asp:Panel ID="pnlradoselect" runat="server">
                                        <div class="col-md-12" style="text-align: center" runat="server" id="rdoselection">
                                            <asp:RadioButtonList ID="rblisintallmentconfig" runat="server" AppendDataBoundItems="true" RepeatDirection="Horizontal"
                                                AutoPostBack="true" OnSelectedIndexChanged="rblisintallmentconfig_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Selected="True">&nbsp;&nbsp; Amount-wise Single Student Fees Installment &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2">&nbsp;&nbsp;Bulk Student Fees Installment</asp:ListItem>
                                                <asp:ListItem Value="3">&nbsp;&nbsp;Single Student Fees Discount</asp:ListItem>
                                                <asp:ListItem Value="4">&nbsp;&nbsp;Bulk Student Fees Discount</asp:ListItem> 
                                            </asp:RadioButtonList>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <asp:Panel ID="pnlsingleinstallment" runat="server" Visible="false">
                                <div class="col-12">

                                    <asp:UpdatePanel ID="updEdit" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Search Criteria</label>
                                                        </div>
                                                        <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlSearch" InitialValue="0"
                                                            Display="None" ErrorMessage="Please select search string from the given list"
                                                            SetFocusOnError="true" ValidationGroup="submitt" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">
                                                        <asp:Panel ID="pnltextbox" runat="server">
                                                            <div id="divtxt" runat="server" style="display: block">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Search String</label>
                                                                </div>
                                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvSearchtring" runat="server" ControlToValidate="txtSearch" InitialValue="" Enabled="false"
                                                                    Display="None" ErrorMessage="Please Enter search string in the given text box"
                                                                    SetFocusOnError="true" ValidationGroup="submitt" />
                                                            </div>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlDropdown" runat="server">
                                                            <div id="divDropDown" runat="server" style="display: block">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%-- <label id="lblDropdown"></label>--%>
                                                                    <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvDDL" runat="server" ControlToValidate="ddlDropdown" InitialValue="0" Enabled="false"
                                                                    Display="None" ErrorMessage="Please select search string from the given list"
                                                                    SetFocusOnError="true" ValidationGroup="submitt" />
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <%-- OnClientClick="return submitPopup(this.name);"--%>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" ValidationGroup="submitt" />

                                                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                                    <asp:ValidationSummary ID="valSummery1" DisplayMode="List" runat="server" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="submitt" />
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                                </div>

                                                <div class="col-12">
                                                    <asp:Panel ID="Panel3" runat="server">
                                                        <asp:ListView ID="lvStudent" runat="server">
                                                            <LayoutTemplate>
                                                                <div>
                                                                    <div class="sub-heading">
                                                                        <h5>Student List</h5>
                                                                    </div>
                                                                    <asp:Panel ID="Panel2" runat="server">
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Name
                                                                                    </th>
                                                                                    <th>IdNo
                                                                                    </th>
                                                                                    <th>Roll No.
                                                                                    </th>
                                                                                    <th>Branch
                                                                                    </th>
                                                                                    <th>Semester
                                                                                    </th>
                                                                                    <th>Father Name
                                                                                    </th>
                                                                                    <th>Mother Name
                                                                                    </th>
                                                                                    <th>Mobile No.
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                    </asp:Panel>
                                                                </div>

                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                            OnClick="lnkId_Click"></asp:LinkButton>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("idno")%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SEMESTERNO")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("FATHERNAME") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("MOTHERNAME") %>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("STUDENTMOBILE") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>


                                </div>
                                <div class="col-12" id="divStudInfo" runat="server" visible="false">
                                    <div class="sub-heading">
                                        <h5>Student Information</h5>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Name of Student :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblStudName" runat="server" Font-Bold="True" />
                                                        <asp:Label ID="lblidno" Visible="false" runat="server" Font-Bold="True" />
                                                        <asp:HiddenField ID="hdfDmno" Value="0" runat="server" />
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Degree :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblDegree" runat="server" Font-Bold="True" /></a>
                                                </li>
                                                <li class="list-group-item"><b>Branch :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblBranch" runat="server" Font-Bold="True" /></a>
                                                </li>
                                                <li class="list-group-item"><b>No.of Installment :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblinstalment" runat="server" Font-Bold="True" /></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>PRN Number :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblRegno" runat="server" Font-Bold="True" />
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Payment Type :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblPaymentType" runat="server" Font-Bold="True" /></a>
                                                </li>
                                                <li class="list-group-item"><b>Total Demand :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblPayDemand" runat="server" Font-Bold="True" /></a>
                                                </li>
                                                <li class="list-group-item"><b>Total Installment Amount :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblDemand" runat="server" Font-Bold="True" /></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Semester/Year :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblSemester" runat="server" Font-Bold="True" />
                                                    </a>
                                                </li>
                                                <div class="label-dynamic">
                                                    <%--  <sup>* </sup>    --%>
                                                    <label>Remark by Approval Authority</label>
                                                </div>
                                                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="form-control" MaxLength="20" TabIndex="5"></asp:TextBox>
                                            </ul>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Receipt Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlReceiptType" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="2">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlReceiptType" runat="server" Display="None"
                                                InitialValue="0" ErrorMessage="Please Select Receipt Type." ValidationGroup="Show" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Semester</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="3">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlSemester" runat="server" Display="None"
                                                InitialValue="0" ErrorMessage="Please Select Semester." ValidationGroup="Show" />
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" TabIndex="4" ValidationGroup="Show"
                                                OnClick="btnShow_Click" />
                                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-warning" TabIndex="4"
                                                OnClick="btnClear_Click" />
                                            <asp:ValidationSummary ID="valShowSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="Show" />
                                        </div>
                                    </div>
                                    <div class="col-md-12 mt-3 pl-0 pr-0">
                                        <div class="table table-responsive">
                                            <asp:Panel ID="pnlStudinstalment" runat="server" Visible="true">
                                                <asp:GridView ID="grdinstalment" runat="server" AutoGenerateColumns="False"
                                                    ShowFooter="True" CssClass="table table-hovered table-bordered">
                                                    <Columns>
                                                        <%--<asp:BoundField DataField="RowNumber" HeaderText="Sr.No." ItemStyle-Width="200px" />--%>
                                                        <asp:TemplateField HeaderText="Sr.No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="INSTALMENT_NO" HeaderText="Installment No." ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="200px">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle Width="200px" />
                                                        </asp:BoundField>

                                                        <asp:TemplateField HeaderText="Due Date">
                                                            <ItemTemplate>
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon" id="ToDate" runat="server">
                                                                        <i class="fa fa-calendar" runat="server" id="Cal"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtDueDate" runat="server" class="form-control" Style="width: 230px"
                                                                        ondrop="return false;" placeholder="Due Date" onpaste="return false;" Text='<%# Bind("DUE_DATE") %>' onkeypress="return RestrictCommaSemicolon(event);" onkeyup="ConvertEachFirstLetterToUpper(this.id)">
                                                                    </asp:TextBox>
                                                                    <ajaxToolKit:CalendarExtender ID="meetodate" runat="server" Format="dd/MM/yyyy"
                                                                        TargetControlID="txtDueDate" PopupButtonID="Cal" Enabled="True">
                                                                    </ajaxToolKit:CalendarExtender>
                                                                    <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtDueDate" />
                                                                    <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" ControlExtender="meToDate"
                                                                        ControlToValidate="txtDueDate" Display="None" EmptyValueMessage="Please Enter Due Date"
                                                                        ErrorMessage="Please Enter Due Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                        IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                                                    <ajaxToolKit:MaskedEditValidator ID="rfvMonth" runat="server" ControlExtender="meToDate"
                                                                        ControlToValidate="txtDueDate" Display="None" EmptyValueMessage="Please Enter Due Date"
                                                                        ErrorMessage="Please Enter Due Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                        IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Daywise" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter DueDate."
                                                                        ControlToValidate="txtDueDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                            </EditItemTemplate>
                                                            <ControlStyle Width="300px"></ControlStyle>
                                                            <ItemStyle Width="300px"></ItemStyle>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAmount" runat="server" MaxLength="10" Text='<%# Bind("INSTALL_AMOUNT") %>'
                                                                    Enabled='<%# (Convert.ToInt32(Eval("RECON") ) == 1 ?  false : true )%>'
                                                                    class="form-control" placeholder="">
                                                                </asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="fteValue" runat="server"
                                                                    FilterType="Custom" FilterMode="ValidChars" ValidChars="1234567890" TargetControlID="txtAmount">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Amount."
                                                                    ControlToValidate="txtAmount" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                            </EditItemTemplate>
                                                            <ControlStyle Width="200px"></ControlStyle>
                                                            <ItemStyle Width="200px"></ItemStyle>

                                                            <FooterStyle />
                                                            <FooterTemplate>
                                                                <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                                                                    Text="Add New Installment" CssClass="btn btn-primary" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Remove">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="lnkRemove" runat="server" OnClick="lnkRemove_Click"
                                                                    ImageUrl="~/IMAGES/delete.png" AlternateText="Remove Row" Visible='<%# (Convert.ToInt32(Eval("RECON") ) == 1 ?  false : true )%>'
                                                                    OnClientClick="return UserDeleteConfirmation();"></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                            </EditItemTemplate>
                                                            <ControlStyle Width="20px"></ControlStyle>
                                                            <ItemStyle Width="50px"></ItemStyle>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%# (Convert.ToInt32(Eval("RECON") )== 1 ?  "Paid" : "Not Paid" )%>'
                                                                    ForeColor='<%# (Convert.ToInt32(Eval("RECON") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="100px" />
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <HeaderStyle CssClass="bg-light-blue" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" ValidationGroup="submit" CssClass="btn btn-primary" TabIndex="6"
                                            OnClick="submit_Click" />
                                        <asp:Button ID="btnRemove" runat="server" Text="Remove All Installment" CssClass="btn btn-primary" TabIndex="6"
                                            OnClick="btnRemove_Click" />
                                        <asp:Button ID="btncancel" runat="server" Text="Cancel" ToolTip="Cancel Student Information"
                                            CausesValidation="false" CssClass="btn btn-warning" TabIndex="7" OnClick="btnCancel_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="submit" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlBulkInstallment" runat="server" Visible="false">
                                <div class="box-body">
                                    <div class="col-12">
                                        <div class="row">


                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <%--<label>Institute Name</label>--%>
                                                    <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged" ToolTip="Please Select Institute">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlColg"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Collage/School" ValidationGroup="show">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlColg"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Collage/School" ValidationGroup="BulkSubmit">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <%--<label>Degree</label>--%>
                                                    <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="3" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlDegree"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="show">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlDegree"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="BulkSubmit">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <%--<label>Branch</label>--%>
                                                    <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="4" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBranch"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="show">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlBranch"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="BulkSubmit">
                                                </asp:RequiredFieldValidator>
                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <%--<label>Semester</label>--%>
                                                    <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlBulkSemester" runat="server" AppendDataBoundItems="true" TabIndex="6" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlBulkSemester"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="show">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlBulkSemester"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="BulkSubmit">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Receipt Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlBulkReceiptType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlBulkReceiptType" runat="server" ControlToValidate="ddlBulkReceiptType"
                                                    Display="None" ErrorMessage="Please Select Receipt Type" InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="show" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlBulkReceiptType"
                                                    Display="None" ErrorMessage="Please Select Receipt Type" InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="BulkSubmit" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Payment Type</label>

                                                </div>
                                                <asp:DropDownList ID="ddlPaymentType" runat="server" TabIndex="32" AppendDataBoundItems="true"
                                                    CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Payment Type">
                                                    <asp:ListItem Value="0">Please select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlPaymentType"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Payment Type" ValidationGroup="show">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlPaymentType"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Payment Type" ValidationGroup="BulkSubmit">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Installment Type</label>

                                                </div>
                                                <asp:DropDownList ID="ddlinstallmenttype" runat="server" AutoPostBack="true" TabIndex="32" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlinstallmenttype_SelectedIndexChanged"
                                                    CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Installment Type">
                                                    <asp:ListItem Value="0">Please select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlinstallmenttype"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Installment Type" ValidationGroup="BulkSubmit">
                                                </asp:RequiredFieldValidator>
                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="div1st" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>1st Installment Due Date</label>

                                                </div>
                                                <div class="input-group">
                                                    <div class="input-group-addon" id="Div2">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtDuedate1" runat="server" TabIndex="34" ValidationGroup="show" />

                                                    <ajaxToolKit:CalendarExtender ID="meetodate" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtDuedate1" PopupButtonID="Cal" Enabled="True">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtDuedate1" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" ControlExtender="meToDate"
                                                        ControlToValidate="txtDuedate1" Display="None" EmptyValueMessage="Please Enter 1st Installment Due Date"
                                                        ErrorMessage="Please Enter 1st Installment Due Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                        IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                                    <ajaxToolKit:MaskedEditValidator ID="rfvMonth" runat="server" ControlExtender="meToDate"
                                                        ControlToValidate="txtDuedate1" Display="None" EmptyValueMessage="Please Enter 1st Installment Due Date"
                                                        ErrorMessage="Please Enter 1st Installment Due Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                        IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter 1st Installment Due Date"
                                                        ControlToValidate="txtDuedate1" Display="None" SetFocusOnError="True" ValidationGroup="BulkSubmit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>




                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="div2nd" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>2nd Installment Due Date</label>

                                                </div>
                                                <div class="input-group">
                                                    <div class="input-group-addon" id="Div3">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtDuedate2" runat="server" TabIndex="34" ValidationGroup="show" />

                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtDuedate2" PopupButtonID="Cal" Enabled="True">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date"
                                                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtDuedate2" />
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meToDate"
                                                        ControlToValidate="txtDuedate2" Display="None" EmptyValueMessage="Please Enter 2nd Installment Due Date"
                                                        ErrorMessage="Please Enter 2nd Installment Due Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                        IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meToDate"
                                                        ControlToValidate="txtDuedate2" Display="None" EmptyValueMessage="Please Enter 2nd Installment Due Date"
                                                        ErrorMessage="Please Enter 2nd Installment Due Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                        IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Daywise" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Enter 2nd Installment Due Date"
                                                        ControlToValidate="txtDuedate2" Display="None" SetFocusOnError="True" ValidationGroup="BulkSubmit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="div3rd" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>3rd Installment Due Date</label>

                                                </div>
                                                <div class="input-group">
                                                    <div class="input-group-addon" id="Div4">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtDuedate3" runat="server" TabIndex="34" ValidationGroup="show" />

                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtDuedate3" PopupButtonID="Cal" Enabled="True">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date"
                                                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtDuedate3" />
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="meToDate"
                                                        ControlToValidate="txtDuedate3" Display="None" EmptyValueMessage="Please Enter 3rd Installment Due Date"
                                                        ErrorMessage="Please Enter 3rd Installment Due Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                        IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="meToDate"
                                                        ControlToValidate="txtDuedate3" Display="None" EmptyValueMessage="Please Enter 3rd Installment Due Date"
                                                        ErrorMessage="Please Enter 3rd Installment Due Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                        IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Daywise" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please Enter 3rd Installment Due Date"
                                                        ControlToValidate="txtDuedate3" Display="None" SetFocusOnError="True" ValidationGroup="BulkSubmit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="div4th" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>4th Installment Due Date</label>

                                                </div>
                                                <div class="input-group">
                                                    <div class="input-group-addon" id="Div5">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtDuedate4" runat="server" TabIndex="34" ValidationGroup="show" />

                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtDuedate4" PopupButtonID="Cal" Enabled="True">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999" MaskType="Date"
                                                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtDuedate4" />
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="meToDate"
                                                        ControlToValidate="txtDuedate4" Display="None" EmptyValueMessage="Please Enter 4th Installment Due Date"
                                                        ErrorMessage="Please Enter 4th Installment Due Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                        IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator6" runat="server" ControlExtender="meToDate"
                                                        ControlToValidate="txtDuedate4" Display="None" EmptyValueMessage="Please Enter 4th Installment Due Date"
                                                        ErrorMessage="Please Enter 4th Installment Due Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                        IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Daywise" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Please Enter 4th Installment Due Date"
                                                        ControlToValidate="txtDuedate4" Display="None" SetFocusOnError="True" ValidationGroup="BulkSubmit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="div5th" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>5th Installment Due Date</label>

                                                </div>
                                                <div class="input-group">
                                                    <div class="input-group-addon" id="Div6">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtDuedate5" runat="server" TabIndex="34" ValidationGroup="show" />

                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtDuedate5" PopupButtonID="Cal" Enabled="True">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999" MaskType="Date"
                                                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtDuedate5" />
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator7" runat="server" ControlExtender="meToDate"
                                                        ControlToValidate="txtDuedate5" Display="None" EmptyValueMessage="Please Enter 5th Installment Due Date"
                                                        ErrorMessage="Please Enter 5th Installment Due Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                        IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator8" runat="server" ControlExtender="meToDate"
                                                        ControlToValidate="txtDuedate5" Display="None" EmptyValueMessage="Please Enter 5th Installment Due Date"
                                                        ErrorMessage="Please Enter 5th Installment Due Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                        IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Daywise" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Please Enter 5th Installment Due Date"
                                                        ControlToValidate="txtDuedate5" Display="None" SetFocusOnError="True" ValidationGroup="BulkSubmit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>




                                            <div class="col-12 btn-footer">
                                                <asp:Label runat="server" ID="lblmsg" Text="" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnBulkShow" runat="server" TabIndex="7" Text="Show" ValidationGroup="show"
                                            CssClass="btn btn-primary" ToolTip="SHOW" OnClick="btnBulkShow_Click" />
                                        <asp:Button ID="btnSave" runat="server" TabIndex="8" Text="Submit" ValidationGroup="BulkSubmit"
                                            CssClass="btn btn-primary" OnClick="btnSave_Click" ToolTip="Submit" />

                                        <asp:Button ID="btnBulkCancel" runat="server" TabIndex="9" Text="Cancel" CssClass="btn btn-warning"
                                            OnClick="btnBulkCancel_Click" />

                                        <asp:Button ID="btnExcelReport" runat="server" TabIndex="9" Text="Installment Report(Excel)" ValidationGroup="show" CssClass="btn btn-success"
                                            OnClick="btnExcelReport_Click" />

                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="show"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="BulkSubmit"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>
                                    <div class="col-12">
                                        <asp:Panel ID="Panel1" runat="server"></asp:Panel>
                                        <asp:ListView ID="lvBulkStudent" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Student List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>
                                                                <asp:CheckBox ID="cbHead" Text="Select All" runat="server" onclick="return SelectAll(this)" ToolTip="Select/Select all" />
                                                            </th>
                                                            <th>PRN Number
                                                            </th>
                                                            <th>Student Name
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
                                                        <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("IDNO") %>' Enabled='<%# (Convert.ToInt32(Eval("STATUS") )== 1 ?  false : true )%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("REGNO") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDNAME") %>' />
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lblbulkstatus" runat="server" Text='<%# (Convert.ToInt32(Eval("STATUS") )== 1 ?  "Created" : "Pending" )%>'
                                                            ForeColor='<%# (Convert.ToInt32(Eval("STATUS") )== 0 ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>'></asp:Label>

                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlSingleDiscount" runat="server" Visible="false">
                                <div class="col-12">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Search Criteria</label>
                                                        </div>
                                                        <asp:DropDownList runat="server" class="form-control" ID="ddlsearchDisc" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlsearchDisc_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="ddlSearch" InitialValue="0"
                                                            Display="None" ErrorMessage="Please select search string from the given list"
                                                            SetFocusOnError="true" ValidationGroup="submitt" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanelDis">
                                                        <asp:Panel ID="pnltextboxDis" runat="server">
                                                            <div id="TxtSrchDis" runat="server" style="display: block">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Search String</label>
                                                                </div>
                                                                <asp:TextBox ID="txtSearchDis" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtSearchDis" InitialValue="" Enabled="false"
                                                                    Display="None" ErrorMessage="Please Enter search string in the given text box"
                                                                    SetFocusOnError="true" ValidationGroup="submitt" />
                                                            </div>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlDropdownDis" runat="server">
                                                            <div id="divddlDis" runat="server" style="display: block">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%-- <label id="lblDropdown"></label>--%>
                                                                    <asp:Label ID="lblDropdownDis" Style="font-weight: bold" runat="server"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList runat="server" class="form-control" ID="ddlDropdownDis" AppendDataBoundItems="true" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="ddlDropdownDis" InitialValue="0" Enabled="false"
                                                                    Display="None" ErrorMessage="Please select search string from the given list"
                                                                    SetFocusOnError="true" ValidationGroup="submitt" />
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnSerchDis" runat="server" Text="Search" OnClick="btnSerchDis_Click" CssClass="btn btn-primary" ValidationGroup="submitt" />
                                                        <asp:Button ID="btnCloseDis" runat="server" Text="Close" CssClass="btn btn-warning" OnClick="btnCloseDis_Click" OnClientClick="return CloseSearchBoxDis(this.name)" data-dismiss="modal" />
                                                        <asp:ValidationSummary ID="ValidationSummary4" DisplayMode="List" runat="server" ShowMessageBox="true"
                                                            ShowSummary="false" ValidationGroup="submitt" />
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <asp:Label ID="lblNoRecordsDis" runat="server" SkinID="lblmsg" />
                                                    </div>
                                                    <div class="col-12">
                                                        <asp:Panel ID="Panel4" runat="server">
                                                            <asp:ListView ID="lvStudentDis" runat="server">
                                                                <LayoutTemplate>
                                                                    <div>
                                                                        <div class="sub-heading">
                                                                            <h5>Student List</h5>
                                                                        </div>
                                                                        <asp:Panel ID="Panel2" runat="server">
                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th>Name
                                                                                        </th>
                                                                                        <th>IdNo
                                                                                        </th>
                                                                                        <th>Roll No.
                                                                                        </th>
                                                                                        <th>Branch
                                                                                        </th>
                                                                                        <th>Semester
                                                                                        </th>
                                                                                        <th>Father Name
                                                                                        </th>
                                                                                        <th>Mother Name
                                                                                        </th>
                                                                                        <th>Mobile No.
                                                                                        </th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                </tbody>
                                                                        </asp:Panel>
                                                                    </div>

                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkIdDis" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                                OnClick="lnkIdDis_Click"></asp:LinkButton>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("idno")%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                                                            <asp:HiddenField ID="hdfdegree" runat="server" Value='<%# Eval("DEGREENAME")%>' />
                                                                            <asp:HiddenField ID="hdfbranch" runat="server" Value='<%# Eval("branchname")%>' />
                                                                            <asp:HiddenField ID="hdfsemester" runat="server" Value='<%# Eval("semestername")%>' />
                                                                            <asp:HiddenField ID="hdfregno" runat="server" Value='<%# Eval("ENROLLMENTNO")%>' />
                                                                            <asp:HiddenField ID="hdfdemand" runat="server" Value='<%# Eval("total_amt")%>' />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SEMESTERNO")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("FATHERNAME") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("MOTHERNAME") %>
                                                                        </td>
                                                                        <td>
                                                                            <%#Eval("STUDENTMOBILE") %>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12" id="divStudInfoDis" runat="server" visible="false">
                                    <div class="sub-heading">
                                        <h5>Student Information</h5>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Name of Student :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblStudNameDis" runat="server" Font-Bold="True" />
                                                        <asp:Label ID="lblidnoDis" Visible="false" runat="server" Font-Bold="True" />
                                                        <asp:HiddenField ID="hdfDmnoDis" Value="0" runat="server" />
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Degree :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblDegreeDis" runat="server" Font-Bold="True" /></a>
                                                </li>

                                            </ul>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Regno :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblRegnoDis" runat="server" Font-Bold="True" />
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Total Demand :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblPayDemandDis" runat="server" Font-Bold="True" /></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Branch :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblBranchDis" runat="server" Font-Bold="True" /></a>
                                                </li>
                                                <li class="list-group-item"><b>Semestername :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblsemesterDis" runat="server" Font-Bold="True" /></a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Receipt Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlreceipt" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlreceipt_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="2">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" ControlToValidate="ddlreceipt" runat="server" Display="None"
                                                    InitialValue="0" ErrorMessage="Please Select Receipt Type." ValidationGroup="SDis" />
                                            </div>
                                             <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Payment Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlpaymentsingle" runat="server" TabIndex="32" AppendDataBoundItems="true"
                                                    CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Payment Type">
                                                    <asp:ListItem Value="0">Please select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="ddlpaymentsingle"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Payment Type" ValidationGroup="SDis">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Semester</label>
                                                </div>
                                                <asp:DropDownList ID="ddlsemesterShow" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="3">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" ControlToValidate="ddlsemester" runat="server" Display="None"
                                                    InitialValue="0" ErrorMessage="Please Select Semester." ValidationGroup="SDis" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divConcessionOptionSingle" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:RadioButtonList ID="rdoSelect" runat="server" TabIndex="1"
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Amount Wise &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="0">Percentage Wise  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                                    </asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShowDisSingle" runat="server" Text="Show"  OnClick="btnShowDisSingle_Click" CssClass="btn btn-primary" ValidationGroup="SDis" />
                                        <asp:Button ID="btnSubmitSingleDis" runat="server" Text="Submit"   OnClick="btnSubmitSingleDis_Click" CssClass="btn btn-primary" Visible="false"  />
                                        <asp:Button ID="CancelDis" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="CancelDis_Click"  />
                                        <asp:ValidationSummary ID="ValidationSummary6" DisplayMode="List" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="SDis" />
                                    </div>
                                    <div class="col-md-12">
                                        <div id="div8" runat="server">
                                            <asp:Panel ID="Panel5" runat="server">
                                                <asp:ListView ID="LvDiscountSingle" runat="server">
                                                    <LayoutTemplate>
                                                        <div>
                                                            <div class="sub-heading">
                                                                <h5></h5>
                                                            </div>

                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="mytable">
                                                                <thead class="bg-light-blue">
                                                                    <tr id="trRow">
                                                                        <th>
                                                                            <asp:CheckBox ID="chkRows" runat="server" onclick="return totAll(this);" /></th>
                                                                        <th>Student ID</th>
                                                                        <th>Student Name</th>
                                                                        <th>Applicable Fee</th>
                                                                        <th>Discount Type
                                                                                    <br />
                                                                            <div class="d-none DiscountType">
                                                                                <asp:DropDownList ID="dllSelectAllType" runat="server" AppendDataBoundItems="true" CssClass="form-control dllSelectAllType" data-select2-enable="true" onchange="return SelectDiscountType(this);">
                                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </th>
                                                                        <th class="Con_Disc">Discount %</th>
                                                                        <th>Discount Fee
                                                                          <input type="text" class="form-control txtForAllStudents d-none" onkeyup="return CalculateAmount1(this);" onfocus="return CalculateAmount1(this);" />
                                                                        </th>
                                                                        <th>Net Payable</th>
                                                                        <th>Status</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chktransfer" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblreg" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("DM_NO") %>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblIdno" Text='<%# Eval("IDNO") %>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblname" Text='<%# Eval("STUDNAME") %>'></asp:Label></td>
                                                            <td class="DemandAmount1">
                                                                <asp:TextBox ID="lbltotal" runat="server" CssClass="form-control DemandAmount1" Text='<%# Eval("TOTAL_AMT") %>' Enabled="false" onblur="return CheckMark(this);" /></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlConcession" runat="server" CssClass="form-control ddlConcession" data-select2-enable="true" AppendDataBoundItems="true" Enabled="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblConcessionno" runat="server" Text='<% #Eval("CONCESSION_TYPE")%>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td class="Con_Disc">
                                                                <asp:DropDownList ID="ddlDiscount" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" Enabled="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblDiscount" runat="server" Text='<% #Eval("DISCOUNT")%>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDiscountFee" runat="server" CssClass="form-control FinalAmount" Text='<%# Eval("DISCOUNT_FEES") %>' Enabled="false" /></td>
                                                            <td>
                                                                <asp:TextBox ID="txtNetPayable" runat="server" CssClass="form-control NetPayAmount" Text='<%# Eval("NET_PAYABLE") %>' Enabled="false" /></td>
                                                            <td>
                                                                <asp:Label ID="lbldcridno" runat="server" Text='<%# Eval("DCR_IDNO") %>' Style="width: 150px"></asp:Label>
                                                                <asp:HiddenField ID="hdfCollege" runat="server" Value='<%# Eval("COLLEGE_ID") %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlBulkDiscount" runat="server" Visible="false">
                                <div class="box-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <%--<label>Institute Name</label>--%>
                                                    <asp:Label ID="lblDYddlSchool_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlschool" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlschool_SelectedIndexChanged" ToolTip="Please Select Collage/School">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="ddlschool"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Collage/School" ValidationGroup="showdis">
                                                </asp:RequiredFieldValidator>

                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <%--<label>Degree</label>--%>
                                                    <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddldegreedis" runat="server" AppendDataBoundItems="true" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddldegreedis_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="ddldegreedis"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="showdis">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <%--<label>Branch</label>--%>
                                                    <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlbranchdis" runat="server" AppendDataBoundItems="true" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlbranchdis_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="ddlbranchdis"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="showdis">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <%--<label>Semester</label>--%>
                                                    <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlsemesterdis" runat="server" AppendDataBoundItems="true" TabIndex="4" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="ddlsemesterdis"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="showdis">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Receipt Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlreceiptdis" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="5" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="ddlreceiptdis"
                                                    Display="None" ErrorMessage="Please Select Receipt Type" InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="showdis" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Payment Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlpaymentdis" runat="server" TabIndex="32" AppendDataBoundItems="true"
                                                    CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Payment Type">
                                                    <asp:ListItem Value="0">Please select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="ddlpaymentdis"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Payment Type" ValidationGroup="showdis">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divConcessionOption" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:RadioButtonList ID="rdConcessionOption" runat="server" TabIndex="1"
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Amount Wise &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </asp:ListItem>
                                                    <asp:ListItem Value="0">Percentage Wise  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                                    </asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnshowdis" runat="server" TabIndex="7" Text="Show" ValidationGroup="showdis"
                                            CssClass="btn btn-primary" ToolTip="Show" OnClick="btnshowdis_Click" />
                                        <asp:Button ID="btnsubmitdis" runat="server" TabIndex="8" Text="Submit" Visible="false"
                                            CssClass="btn btn-primary" OnClick="btnsubmitdis_Click" ToolTip="Submit" />
                                        <asp:Button ID="btncanceldis" runat="server" TabIndex="9" Text="Cancel" CssClass="btn btn-warning"
                                            OnClick="btncanceldis_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="showdis"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>
                                    <div class="col-md-12">
                                        <div id="div7" runat="server">
                                            <asp:Panel ID="Panel2" runat="server">
                                                <asp:ListView ID="LvDiscount" runat="server">
                                                    <LayoutTemplate>
                                                        <div>
                                                            <div class="sub-heading">
                                                                <h5></h5>
                                                            </div>

                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="mytable">
                                                                <thead class="bg-light-blue">
                                                                    <tr id="trRow">
                                                                        <th>
                                                                            <asp:CheckBox ID="chkRows" runat="server" onclick="return totAll(this);" /></th>
                                                                        <th>Student ID</th>
                                                                        <th>Student Name</th>
                                                                        <th>Applicable Fee</th>
                                                                        <th>Discount Type
                                                                                    <br />
                                                                            <div class="d-none DiscountType">
                                                                                <asp:DropDownList ID="dllSelectAllType" runat="server" AppendDataBoundItems="true" CssClass="form-control dllSelectAllType" data-select2-enable="true" onchange="return SelectDiscountType(this);">
                                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </th>
                                                                        <th class="Con_Disc">Discount %</th>
                                                                        <th>Discount Fee
                                                                          <input type="text" class="form-control txtForAllStudents d-none" onkeyup="return CalculateAmount(this);" onfocus="return CalculateAmount(this);" />
                                                                        </th>
                                                                        <th>Net Payable</th>
                                                                        <th>Status</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chktransfer" runat="server" /></td>

                                                            <td>
                                                                <asp:Label runat="server" ID="lblreg" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("DM_NO") %>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblIdno" Text='<%# Eval("IDNO") %>' Visible="false"></asp:Label>
                                                            </td>

                                                            <td>
                                                                <asp:Label runat="server" ID="lblname" Text='<%# Eval("STUDNAME") %>'></asp:Label></td>
                                                            <td>
                                                                <div class="DemandAmount"></div>
                                                                <asp:TextBox ID="lbltotal" runat="server" CssClass="ass form-control DemandAmount" Text='<%# Eval("TOTAL_AMT") %>' Enabled="false" onblur="return CheckMark(this);"/></td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlConcession" runat="server" CssClass="form-control ddlConcession" data-select2-enable="true" AppendDataBoundItems="true" Enabled="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblConcessionno" runat="server" Text='<% #Eval("CONCESSION_TYPE")%>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td class="Con_Disc">
                                                                <asp:DropDownList ID="ddlDiscount" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" Enabled="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblDiscount" runat="server" Text='<% #Eval("DISCOUNT")%>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td >
                                                                <asp:HiddenField ID="hdfDiscountFee" runat="server" />
                                                                <asp:TextBox ID="txtDiscountFee" runat="server" CssClass="form-control" Text='<%# Eval("DISCOUNT_FEES") %>' Enabled="false" /></td>
                                                            <td >
                                                                <asp:HiddenField ID="hdfNetPayable" runat="server" />
                                                                <asp:TextBox ID="txtNetPayable" runat="server" CssClass="form-control" Text='<%# Eval("NET_PAYABLE") %>' Enabled="false" /></td>
                                                            <td>
                                                                <asp:Label ID="lbldcridno" runat="server" Text='<%# Eval("DCR_IDNO") %>' Style="width: 150px"></asp:Label></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>
                            <div id="divMsg" runat="server"></div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcelReport" />
            <asp:PostBackTrigger ControlID="btnsubmitdis" />
        </Triggers>
    </asp:UpdatePanel>
    <%--Search Box Script Start--%>
    <script type="text/javascript" lang="javascript">

        $(document).ready(function () {
            debugger
            //  $("#<%= pnltextbox.ClientID %>").hide();

            $("#<%= pnlDropdown.ClientID %>").hide();
        });
        function submitPopup(btnsearch) {

            debugger
            var rbText;
            var searchtxt;

            var e = document.getElementById("<%=ddlSearch.ClientID%>");
            var rbText = e.options[e.selectedIndex].text;
            var ddlname = e.options[e.selectedIndex].text;
            if (rbText == "Please Select") {
                alert('Please select Criteria as you want search...')
            }

            else {


                if (rbText == "ddl") {
                    var skillsSelect = document.getElementById("<%=ddlDropdown.ClientID%>").value;

                    var searchtxt = skillsSelect;
                    if (searchtxt == "0") {
                        alert('Please Select ' + ddlname + '..!');
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        return true;
                        //$("#<%= divpanel.ClientID %>").hide();
                        $("#<%= pnltextbox.ClientID %>").hide();

                    }
                }
                else if (rbText == "BRANCH") {

                    if (searchtxt == "Please Select") {
                        alert('Please Select Branch..!');

                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);

                        return true;
                    }

                }
                else {
                    searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;
                    if (searchtxt == "" || searchtxt == null) {
                        alert('Please Enter Data you want to search..');
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        //$("#<%= divpanel.ClientID %>").hide();
                        //$("#<%= pnltextbox.ClientID %>").show();

                        return true;
                    }
                }
        }
    }

    function ClearSearchBox(btncancelmodal) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btncancelmodal, '');
        return true;
    }
    function CloseSearchBox(btnClose) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btnClose, '');
        return true;
    }


    function Validate() {

        debugger

        var rbText;

        var e = document.getElementById("<%=ddlSearch.ClientID%>");
        var rbText = e.options[e.selectedIndex].text;

        if (rbText == "IDNO" || rbText == "Mobile") {

            var char = (event.which) ? event.which : event.keyCode;
            if (char >= 48 && char <= 57) {
                return true;
            }
            else {
                return false;
            }
        }
        else if (rbText == "NAME") {

            var char = (event.which) ? event.which : event.keyCode;

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122) || (char = 49)) {
                return true;
            }
            else {
                return false;
            }

        }
    }

    </script>

    <script type="text/javascript" lang="javascript">

        $(document).ready(function () {
            debugger
            //  $("#<%= pnltextboxDis.ClientID %>").hide();

            $("#<%= pnlDropdownDis.ClientID %>").hide();
        });
        function submitPopup(btnsearch) {

            debugger
            var rbText;
            var searchtxt;

            var e = document.getElementById("<%=ddlsearchDisc.ClientID%>");
            var rbText = e.options[e.selectedIndex].text;
            var ddlname = e.options[e.selectedIndex].text;
            if (rbText == "Please Select") {
                alert('Please select Criteria as you want search...')
            }

            else {


                if (rbText == "ddl") {
                    var skillsSelect = document.getElementById("<%=ddlDropdownDis.ClientID%>").value;

                    var searchtxt = skillsSelect;
                    if (searchtxt == "0") {
                        alert('Please Select ' + ddlname + '..!');
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        return true;
                        //$("#<%= divpanelDis.ClientID %>").hide();
                        $("#<%= pnltextboxDis.ClientID %>").hide();

                    }
                }
                else if (rbText == "BRANCH") {

                    if (searchtxt == "Please Select") {
                        alert('Please Select Branch..!');

                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);

                        return true;
                    }

                }
                else {
                    searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;
                    if (searchtxt == "" || searchtxt == null) {
                        alert('Please Enter Data you want to search..');
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        //$("#<%= divpanelDis.ClientID %>").hide();
                        //$("#<%= pnltextboxDis.ClientID %>").show();

                        return true;
                    }
                }
        }
    }

    function ClearSearchBoxDis(btncancelmodal) {
        document.getElementById('<%=txtSearchDis.ClientID %>').value = '';
        __doPostBack(btncancelmodal, '');
        return true;
    }
    function CloseSearchBoxDis(btnClose) {
        document.getElementById('<%=txtSearchDis.ClientID %>').value = '';
        __doPostBack(btnClose, '');
        return true;
    }


    function Validate() {

        debugger

        var rbText;

        var e = document.getElementById("<%=ddlsearchDisc.ClientID%>");
        var rbText = e.options[e.selectedIndex].text;

        if (rbText == "IDNO" || rbText == "Mobile") {

            var char = (event.which) ? event.which : event.keyCode;
            if (char >= 48 && char <= 57) {
                return true;
            }
            else {
                return false;
            }
        }
        else if (rbText == "NAME") {

            var char = (event.which) ? event.which : event.keyCode;

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122) || (char = 49)) {
                return true;
            }
            else {
                return false;
            }

        }
    }

    </script>
    <%--Search Box Script End--%>

    <script type="text/javascript" lang="javascript">
        function SelectAll(headchk) {
            debugger;
            var frm = document.forms[0]

            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {

                    if (headchk.checked == true) {

                        e.checked = true;

                    }
                    else {
                        e.checked = false;
                    }
                }
            }
            if (headchk.checked == true) {
            }
            else {

            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function totAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>
    <script>
      
        $("#ctl00_ContentPlaceHolder1_rdConcessionOption").click(function () {
            debugger;
            var radioValue = $('#<%=rdConcessionOption.ClientID %>input[type=radio]:checked').val();
           
            if (radioValue == 1) {
                $(".Con_Disc").addClass('d-none');
                $(".txtForAllStudents").removeClass('d-none');
                $(".DiscountType").removeClass('d-none');
             
            }
            else {
                $(".Con_Disc").removeClass('d-none');
                $(".txtForAllStudents").addClass('d-none');
                $(".DiscountType").addClass('d-none');
             
            }
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            debugger;
            $("#ctl00_ContentPlaceHolder1_rdConcessionOption").click(function () {

                var radioValue = $('#<%=rdConcessionOption.ClientID %>input[type=radio]:checked').val();
                if (radioValue == 1) {
                    $(".Con_Disc").addClass('d-none');
                    $(".txtForAllStudents").removeClass('d-none');
                    $(".DiscountType").removeClass('d-none');
                }
                else {
                    $(".Con_Disc").removeClass('d-none');
                    $(".txtForAllStudents").addClass('d-none');
                    $(".DiscountType").addClass('d-none');
                }
            });
        });
    </script>
    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("#ctl00_ContentPlaceHolder1_rdConcessionOption").click(function () {

                var radioValue = $('#<%=rdConcessionOption.ClientID %> input[type=radio]:checked').val();

                if (radioValue == 1) {
                    $(".Con_Disc").addClass('d-none');
                    $(".txtForAllStudents").removeClass('d-none');
                    $(".DiscountType").removeClass('d-none');
                }
                else {
                    $(".Con_Disc").removeClass('d-none');
                    $(".txtForAllStudents").addClass('d-none');
                    $(".DiscountType").addClass('d-none');
                }
            });
        });
    </script>
    
    <script>

        function CalculateAmount(Amount) {
            $(".DemandAmount").each(function (index, value) {  
                var List = $(this).closest("table");
                var td = $("td", $(this).closest("tr"));           
                var CheckBoxValue = $("[id*=chktransfer]", td).is(":checked");
                if($('.txtForAllStudents').val() != '')
                {
                    if(CheckBoxValue == true) 
                    {
                        
                        $("[id*=hdfDiscountFee]", td).val($('.txtForAllStudents').val());
                        $("[id*=txtDiscountFee]", td).val($('.txtForAllStudents').val());
                        Amount = document.getElementById('ctl00_ContentPlaceHolder1_' + 'LvDiscount' + '_' + 'ctrl' + index + '_' + 'lbltotal').value;
                        $("[id*=txtNetPayable]", td).val(parseFloat(Amount - $('.txtForAllStudents').val()));
                        $("[id*=hdfNetPayable]", td).val(parseFloat(Amount - $('.txtForAllStudents').val()));
                        
                    }                   
                }
                else
                {
                    if(CheckBoxValue == true) 
                    {
                        $("[id*=txtDiscountFee]", td).val('');$("[id*=txtNetPayable]", td).val('');                  
                    }
                }
            });
        }
    </script>
    <script>
        function CalculateAmount1(Amount) {
            $(".DemandAmount1").each(function (index, value) { 
                var List = $(this).closest("table");
                var td = $("td", $(this).closest("tr"));
                var CheckBoxValue = $("[id*=chktransfer]", td).is(":checked");
                if ($('.txtForAllStudents1').val() != '') {
                    if (CheckBoxValue == true) {
                        $("[id*=txtDiscountFee]", td).val($('.txtForAllStudents').val());
                        Amount = document.getElementById('ctl00_ContentPlaceHolder1_' + 'LvDiscountSingle' + '_' + 'ctrl' + index + '_' + 'lbltotal').value;
                        $("[id*=txtNetPayable]", td).val(parseFloat(Amount - $('.txtForAllStudents').val()));
                    }
                }
                else {
                    if (CheckBoxValue == true) {
                        $("[id*=txtDiscountFee]", td).val(''); $("[id*=txtNetPayable]", td).val('');
                    }
                }
            });
        }
    </script>
    <script>
        function SelectDiscountType(ddl) {
            $('.ddlConcession').val($('.dllSelectAllType option:selected').val()).change();
        }
    </script>
    <script>
        function CheckMark(id) {
            var ValidChars = "0123456789.-";
            var num = true;
            var mChar;
            mChar = id.value.charAt(0);
            if (ValidChars.indexOf(mChar) == -1) {
                num = false;
                id.value = '';
                alert("Error! Only Numeric Values Are Allowed")
                id.select();
                id.focus();
            }
        }
    </script>
    <script type="text/javascript">
        function IsNumeric(txt) {
            var ValidChars = "0123456789.";
            var num = true;
            var mChar;
            cnt = 0

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);

                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Please enter Numeric values only")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }
    </script>

    <script>
        function CheckMark(id) {
            var rowIndex = id.offsetParent.parentNode.rowIndex - 1;
            var cellIndex = id.offsetParent.cellIndex;
            var Apllicable = 0; var Discount = 0;
            Apllicable = document.getElementById("ctl00_ContentPlaceHolder1_LvDiscount_ctrl" + rowIndex + "_lbltotal").value;
            Discount = document.getElementById("ctl00_ContentPlaceHolder1_LvDiscount_ctrl" + rowIndex + "_ddlDiscount");
            var option = Discount.options[Discount.selectedIndex];
            var Discounts = option.text;
            if (Discounts == 'Please Select') {
                var Discounts = 0;
            }
            if (Apllicable == '') {
                var Apllicable = 0;
            }
            var total = 100
            ConvertMark = (Number(Discounts) / Number(total)) * Number(Apllicable)
            //alert(ConvertMark)
            var Netpayable = (Number(Apllicable) - ConvertMark);
            document.getElementById("ctl00_ContentPlaceHolder1_LvDiscount_ctrl" + rowIndex + "_txtDiscountFee").value = ConvertMark;
            document.getElementById("ctl00_ContentPlaceHolder1_LvDiscount_ctrl" + rowIndex + "_txtNetPayable").value = Netpayable;

            document.getElementById("ctl00_ContentPlaceHolder1_LvDiscount_ctrl" + rowIndex + "_hdfDiscountFee").value = ConvertMark;
            document.getElementById("ctl00_ContentPlaceHolder1_LvDiscount_ctrl" + rowIndex + "_hdfNetPayable").value = Netpayable;
        }
    </script>

    <script>

        $("[id*=ddlDiscount]").bind("change", function () {
            //Find and reference the GridView.
            var List = $(this).closest("table");
            var ddlValue = $(this).find('option:selected').text();
            var td = $("td", $(this).closest("tr"));
            var Apllicable = $("[id*=lbltotal]", td).val();
            var ConvertMark = (Number(ddlValue) / Number(100)) * Number(Apllicable)
            var Netpayable = (Number(Apllicable) - ConvertMark);
            $("[id*=txtDiscountFee]", td).val(ConvertMark);
            $("[id*=txtNetPayable]", td).val(Netpayable);
            $("[id*=hdfDiscountFee]", td).val(ConvertMark);
            $("[id*=hdfNetPayable]", td).val(Netpayable);
        });

    </script>

    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            $("[id*=ddlDiscount]").bind("change", function () {
                //Find and reference the GridView.
                var List = $(this).closest("table");
                var ddlValue = $(this).find('option:selected').text();
                var td = $("td", $(this).closest("tr"));
                var Apllicable = $("[id*=lbltotal]", td).val();
                var ConvertMark = (Number(ddlValue) / Number(100)) * Number(Apllicable)
                var Netpayable = (Number(Apllicable) - ConvertMark);
                $("[id*=txtDiscountFee]", td).val(ConvertMark);
                $("[id*=txtNetPayable]", td).val(Netpayable);
                $("[id*=hdfDiscountFee]", td).val(ConvertMark);
                $("[id*=hdfNetPayable]", td).val(Netpayable);
            });
        });
    </script>

    <%-- tabsingledis --%>
    <script>
          $("#ctl00_ContentPlaceHolder1_rdoSelect").click(function () {

              var radioValue = $('#<%=rdoSelect.ClientID %> input[type=radio]:checked').val();

            if (radioValue == 1) {
                $(".Con_Disc").addClass('d-none');
                $(".txtForAllStudents").removeClass('d-none');
                $(".DiscountType").removeClass('d-none');
            }
            else {
                $(".Con_Disc").removeClass('d-none');
                $(".txtForAllStudents").addClass('d-none');
                $(".DiscountType").addClass('d-none');
            }
        });
    </script>

    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("#ctl00_ContentPlaceHolder1_rdoSelect").click(function () {

                var radioValue = $('#<%=rdoSelect.ClientID %> input[type=radio]:checked').val();

                if (radioValue == 1) {
                    $(".Con_Disc").addClass('d-none');
                    $(".txtForAllStudents").removeClass('d-none');
                    $(".DiscountType").removeClass('d-none');
                }
                else {
                    $(".Con_Disc").removeClass('d-none');
                    $(".txtForAllStudents").addClass('d-none');
                    $(".DiscountType").addClass('d-none');
                }
            });
        });
    </script>
     <script>
         function CheckMark(id) {
             var rowIndex = id.offsetParent.parentNode.rowIndex - 1;
             var cellIndex = id.offsetParent.cellIndex;
             var Apllicable = 0; var Discount = 0;
             Apllicable = document.getElementById("ctl00_ContentPlaceHolder1_LvDiscountSingle_ctrl" + rowIndex + "_lbltotal").value;
             Discount = document.getElementById("ctl00_ContentPlaceHolder1_LvDiscountSingle_ctrl" + rowIndex + "_ddlDiscount");
             var option = Discount.options[Discount.selectedIndex];
             var Discounts = option.text;
             if (Discounts == 'Please Select') {
                 var Discounts = 0;
             }
             if (Apllicable == '') {
                 var Apllicable = 0;
             }
             var total = 100
             ConvertMark = (Number(Discounts) / Number(total)) * Number(Apllicable)
             var Netpayable = (Number(Apllicable) - ConvertMark);
             document.getElementById("ctl00_ContentPlaceHolder1_LvDiscountSingle_ctrl" + rowIndex + "_txtDiscountFee").value = ConvertMark;
             document.getElementById("ctl00_ContentPlaceHolder1_LvDiscountSingle_ctrl" + rowIndex + "_txtNetPayable").value = Netpayable;
         }
    </script>
</asp:Content>
