<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain_FacultyDiary.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_Maintain_FacultyDiary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel runat="server" ID="updp">
        <ContentTemplate>
            <div class="row" id="div11" runat="server">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Faculty Diary for Visits and Training</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server">Faculty Diary for Visits and Training</asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College/Institute</label>
                                        </div>
                                        <asp:DropDownList ID="ddlcollege" runat="server" AppendDataBoundItems="True" TabIndex="3" ToolTip="Please Select Institute" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlcollege_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlcollege" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Institute" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Academic Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAcdYear" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="show" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlAcdYear_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvtxtwight" runat="server" ControlToValidate="ddlAcdYear"
                                            Display="None" ErrorMessage="Please Select Academic Year" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="show" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="show" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="show" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlYear"
                                            Display="None" ErrorMessage="Please Select Year" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div2" runat="server" visible="true">
                                        <div class="label-dynamic">
                                            <sup id="sup2" runat="server"></sup>
                                            <label>Lots of Student</label>
                                        </div>
                                        <%--     <asp:ImageButton ID="imgbToCopyLocalAddress" Visible="false" runat="server" ImageUrl="~/images/copy.png"
                                            OnClientClick="copyLocalAddr(this)" ToolTip="Copy Local Address" TabIndex="11" />--%>
                                        <asp:TextBox ID="txtLot" CssClass="form-control" runat="server"
                                            Rows="1" MaxLength="50" ToolTip="Please Enter Lot of Student"
                                            TabIndex="12" Enabled="false" Text="0" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTravel"
                                            Display="None" ErrorMessage="Please Enter Travelling Details" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                        <%--   <asp:TextBox ID="txtPdistrict" CssClass="form-control" runat="server" Visible="False"></asp:TextBox>--%>
                                        <%--   <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                                FilterMode="InvalidChars" FilterType="Custom" InvalidChars="1234567890" TargetControlID="txtPdistrict" />--%>
                                    </div>

                                </div>

                                <div class="row">
                                    <div class="form-group  col-md-6 col-12">
                                    </div>
                                    <div class="form-group  col-md-6 col-12">
                                        <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-primary" OnClick="btnShow_Click">Show</asp:LinkButton>

                                        <asp:LinkButton ID="btnCancelweight" runat="server" CssClass="btn btn-warning" OnClick="btnCancelweight_Click">Cancel</asp:LinkButton>
                                    </div>
                                </div>




                                <div class="row">


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div5" visible="false" runat="server">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Training Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="txtStartDate1" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtStartDate" runat="server" ValidationGroup="submit" TabIndex="5" CssClass="form-control" placeholder="DD/MM/YYYY" />
                                            <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtStartDate" PopupButtonID="txtStartDate1" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtStartDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" OnFocusCssClass="MaskedEditFocus"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" ControlExtender="meeStartDate"
                                                ControlToValidate="txtStartDate" EmptyValueMessage="Please Enter From Date" IsValidEmpty="false"
                                                InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None" ErrorMessage="Start Date is Invalid (Enter dd/mm/yyyy Format)"
                                                TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                ValidationGroup="submit" SetFocusOnError="True" />
                                            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                                Display="None" SetFocusOnError="True" ErrorMessage="Please Enter Training Date"
                                                ValidationGroup="PvalId" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divAddDetails" visible="false" runat="server">
                                        <div class="label-dynamic">
                                            <sup id="supAddDetails" runat="server">* </sup>
                                            <label>Travelling Details</label>
                                        </div>
                                        <%--     <asp:ImageButton ID="imgbToCopyLocalAddress" Visible="false" runat="server" ImageUrl="~/images/copy.png"
                                            OnClientClick="copyLocalAddr(this)" ToolTip="Copy Local Address" TabIndex="11" />--%>
                                        <asp:TextBox ID="txtTravel" CssClass="form-control" runat="server" TextMode="MultiLine"
                                            Rows="3" MaxLength="200" ToolTip="Please Enter Address"
                                            TabIndex="12" />
                                        <asp:RequiredFieldValidator ID="rfvPermAddress" runat="server" ControlToValidate="txtTravel"
                                            Display="None" ErrorMessage="Please Enter Travelling Details" SetFocusOnError="True"
                                            ValidationGroup="PvalId"></asp:RequiredFieldValidator>

                                        <%--   <asp:TextBox ID="txtPdistrict" CssClass="form-control" runat="server" Visible="False"></asp:TextBox>--%>
                                        <%--   <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                                FilterMode="InvalidChars" FilterType="Custom" InvalidChars="1234567890" TargetControlID="txtPdistrict" />--%>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div1" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup id="sup1" runat="server">* </sup>
                                            <label>College Institute/ Industry Visit Name </label>
                                        </div>
                                        <%-- <asp:ImageButton ID="ImageButton1" Visible="false" runat="server" ImageUrl="~/images/copy.png"
                                            OnClientClick="copyLocalAddr(this)" ToolTip="Copy Local Address" TabIndex="11" />--%>
                                        <asp:TextBox ID="txtCollegeAdd" CssClass="form-control" runat="server" TextMode="MultiLine"
                                            Rows="3" MaxLength="200" ToolTip="Please Enter Address"
                                            TabIndex="12" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCollegeAdd"
                                            Display="None" ErrorMessage="Please Enter College Institute/ Industry Visit Name" SetFocusOnError="True"
                                            ValidationGroup="PvalId"></asp:RequiredFieldValidator>

                                        <%--   <asp:TextBox ID="txtPdistrict" CssClass="form-control" runat="server" Visible="False"></asp:TextBox>--%>
                                        <%--   <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                                FilterMode="InvalidChars" FilterType="Custom" InvalidChars="1234567890" TargetControlID="txtPdistrict" />--%>
                                    </div>


                                </div>

                                <div class="row">
                                    <div class="form-group  col-md-6 col-12">
                                    </div>
                                    <div class="form-group  col-md-6 col-12">
                                        <asp:LinkButton ID="btnSubmitWeight" runat="server" OnClientClick="return validate();" CssClass="btn btn-primary" ValidationGroup="PvalId" OnClick="btnSubmitWeight_Click" Visible="false">Submit</asp:LinkButton>
                                        <asp:LinkButton ID="btnReport" runat="server" CssClass="btn btn-primary" OnClick="btnReport_Click" ValidationGroup="report" Visible="false">Report</asp:LinkButton>
                                        <asp:LinkButton ID="btnclear1" runat="server" CssClass="btn btn-warning" OnClick="btnCancelweight_Click" Visible="false" ValidationGroup="report">Cancel</asp:LinkButton>
                                    </div>
                                </div>

                            </div>

                            <div class="col-12">
                            </div>
                            <asp:ValidationSummary ID="PraticipationValidation" runat="server" DisplayMode="List" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="PvalId" />
                            <div class="col-12 mt-3">

                                <div class="table-responsive">
                                    <asp:Panel ID="pnlSession" runat="server" Visible="false">
                                        <div class="sub-heading">
                                            <h5>Student List</h5>
                                        </div>
                                        <asp:ListView ID="lvphdweightage" runat="server" ItemPlaceholderID="itemPlaceholder">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>
                                                                <asp:CheckBox ID="chkSelect" TabIndex="1" runat="server" onclick="totAllSubjects(this)" />Select</th>
                                                            <th>RegNo</th>
                                                            <th>Name</th>
                                                            <th>Degree</th>
                                                            <th>Branch</th>
                                                            <th>Travelling Date</th>
                                                            <th>Travelling Detail</th>
                                                            <th>Institute /College Name</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <asp:UpdatePanel runat="server" ID="updParticipation">
                                                    <ContentTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chkSelect" TabIndex="1" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                                <asp:HiddenField ID="hdnidno" Value='<%# Eval("IDNO")%>' runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblregno" runat="server" Text='<%# Eval("REGNO") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblname" runat="server" Text='<%# Eval("STUDNAME") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbldegree" runat="server" Text='<%# Eval("DEGREE") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblbranch" runat="server" Text='<%# Eval("BRANCH") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbldate" runat="server" Text='<%# Eval("TRAVELLING_DATE") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbltravelling" runat="server" Text='<%# Eval("TRAVELLING_DETAIL") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbladd" runat="server" Text='<%# Eval("INSTITUDE_COLLEGE_NAME") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ContentTemplate>
                                                    <%-- <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnWeightageLink" />
                                                    </Triggers>--%>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmitWeight" />
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnCancelweight" />
            <asp:PostBackTrigger ControlID="btnReport" />
             <asp:PostBackTrigger ControlID="btnclear1" />
        </Triggers>
    </asp:UpdatePanel>

    <script>
        function totAllSubjects(headchk) {
            debugger;
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

</asp:Content>

