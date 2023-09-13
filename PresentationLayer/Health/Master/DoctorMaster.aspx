<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DoctorMaster.aspx.cs" Inherits="Health_Master_DoctorMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upddoctorentries"
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
    </div>--%>
    <asp:UpdatePanel ID="upddoctorentries" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">DOCTOR MASTER</h3>
                        </div>
                        <div class="box-body">
                            <div id="trAdd" runat="server">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="col-12">
                                        <%-- <div class=" sub-heading">
                                            <h5>Add/Edit Doctors</h5>
                                        </div>--%>
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divDoc" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Select Doctor</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDoctor" runat="server" AppendDataBoundItems="true" ValidationGroup="Doctor"
                                                    OnSelectedIndexChanged="ddlDoctor_SelectedIndexChanged" CssClass="form-control"
                                                    AutoPostBack="true" TabIndex="1" ToolTip="Select Doctor">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hfDoctorsName" runat="server" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Doctor Full Name </label>
                                                </div>
                                                <asp:TextBox ID="txtDoctorName" runat="server" CssClass="form-control"
                                                    MaxLength="100" TabIndex="2" ToolTip="Enter Doctor Full Name"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDoctorName" runat="server" ControlToValidate="txtDoctorName"
                                                    Display="None" ErrorMessage="Please Enter Doctor Name" SetFocusOnError="true"
                                                    ValidationGroup="Doctor" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Doctor's Degree </label>
                                                </div>
                                                <asp:TextBox ID="txtDegree" runat="server" CssClass="form-control"
                                                    MaxLength="100" TabIndex="3" ToolTip="Enter Doctor's Degree"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="txtDegree"
                                                    Display="None" ErrorMessage="Please Enter Doctor's Degree" SetFocusOnError="true" ValidationGroup="Doctor" />

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Phone Number</label>
                                                </div>
                                                <asp:TextBox ID="txtPhoneNo" runat="server" MaxLength="10" CssClass="form-control"
                                                    TabIndex="4" ToolTip="Enter Phone Number"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhoneNo"
                                                    Display="None" ErrorMessage="Please Enter Phone Number" SetFocusOnError="true" ValidationGroup="Doctor" />

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Home Address</label>
                                                </div>
                                                <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server"
                                                    TextMode="MultiLine" MaxLength="500" TabIndex="5" ToolTip="Enter Home Address"></asp:TextBox>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Doctor's Status</label>
                                                </div>
                                                <asp:RadioButton ID="rdbActive" runat="server" GroupName="Doctor" Checked="true"
                                                    Text="Active" TabIndex="6" ToolTip="Check If Active" />
                                                <asp:RadioButton ID="rdbNotActive" runat="server" GroupName="Doctor"
                                                    Text="Not Active" TabIndex="9" ToolTip="Check If Not Active" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Designation</label>
                                                </div>
                                                <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" MaxLength="100"
                                                    TabIndex="7 " ToolTip="Enter Designation"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ControlToValidate="txtDesignation"
                                                    Display="None" ErrorMessage="Please Enter Designation" SetFocusOnError="true"
                                                    ValidationGroup="Doctor" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Hospital Name</label>
                                                </div>
                                                <asp:TextBox ID="txtHospitalName" runat="server" MaxLength="50" CssClass="form-control"
                                                    TabIndex="8" ToolTip="Enter Hospital Name"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Hospital Address </label>
                                                </div>
                                                <asp:TextBox ID="txtHospitalAddress" CssClass="form-control" runat="server"
                                                    TextMode="MultiLine" MaxLength="500" TabIndex="9" ToolTip="Enter Hospital Address"></asp:TextBox>

                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Hospital Phone Number </label>
                                                </div>
                                                <asp:TextBox ID="txtHospitalPhone" runat="server" MaxLength="15" CssClass="form-control"
                                                    TabIndex="10" ToolTip="Enter Hospital Phone Number"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftb1" runat="server"
                                                    TargetControlID="txtHospitalPhone"
                                                    FilterType="Custom"
                                                    FilterMode="ValidChars"
                                                    ValidChars="1234567890.">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer mt-2">
                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Doctor" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Doctor"
                                            OnClick="btnSubmit_OnClick" CssClass="btn btn-outline-primary" ToolTip="Click here to Submit" TabIndex="11" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_OnClick"
                                            CssClass="btn btn-outline-danger" ToolTip="Click here to Reset" TabIndex="12" />
                                        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_OnClick"
                                            CssClass="btn btn-outline-primary" ToolTip="Click here to Go Back" TabIndex="13" />

                                    </div>
                                </asp:Panel>
                            </div>
                            <div id="trView" runat="server">
                                <asp:Panel ID="pnlView" runat="server">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Doctor Details</h5>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" Text="Add New" CssClass="btn btn-outline-primary"
                                            OnClick="btnAdd_Click" ToolTip="Click here to Add New Doctor Details"></asp:LinkButton>
                                        <asp:Button ID="Report" runat="server" Text="Show Report" CssClass="btn btn-outline-info"
                                            OnClick="Report_Click" />
                                    </div>

                                    <div class="col-12 mt-3">
                                        <asp:Panel ID="pnlDoctorList" runat="server" ScrollBars="Auto">
                                            <asp:ListView ID="lvDoctor" runat="server">
                                                <EmptyDataTemplate>
                                                    <div class="text-center">
                                                        <asp:Label ID="lblEmpty" Text="No Records Found" runat="server"></asp:Label>
                                                    </div>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <div class="sub-heading">
                                                            <h5>Doctor List</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Action
                                                                    </th>
                                                                    <%--<th>Employee Code
                                                                                        </th>--%>
                                                                    <th>Doctor Name
                                                                    </th>
                                                                    <th>Degree
                                                                    </th>
                                                                    <th>Designation
                                                                    </th>
                                                                    <th>Phone No.
                                                                    </th>
                                                                    <th>Hospital Name
                                                                    </th>
                                                                    <th>Hospital Phn.
                                                                    </th>
                                                                    <th>Status
                                                                    </th>
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
                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("DRID")%>'
                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                        </td>
                                                        <%--  <td>
                                                                                <%# Eval("PFILENO")%>                                      
                                                                            </td>--%>
                                                        <td>
                                                            <%# Eval("DRNAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("DEGREE")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("DESIG")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PHONE")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("HOSPITALNAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("HPHONE")%>
                                                        </td>
                                                        <td style="color: Green">
                                                            <%--   <%# Eval("STATUS1")%>--%>
                                                            <%# GetStatus(Eval("STATUS1"))%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                        <%--  <div class="vista-grid_datapager">
                                            <div class="text-center">
                                                <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvDoctor" PageSize="15"
                                                    OnPreRender="dpPager_PreRender">
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
                                        </div>--%>
                                    </div>

                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

        function IsNumeric() {
            XX1 = document.getElementById('ctl00_ContentPlaceHolder1_txtPhoneNo').value;
            var ValidChars = "0123456789.-()+/";
            var num = true;
            var mChar;
            for (i = 0; i <= XX1.length && num == true; i++) {
                mChar = XX1.charAt(i);
                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    XX1 = 0;
                    alert("Error! Only Phone Number Characters Are Allowed")
                    document.getElementById('ctl00_ContentPlaceHolder1_txtPhoneNo').value = eval("0");
                    document.getElementById('ctl00_ContentPlaceHolder1_txtPhoneNo').select();
                    document.getElementById('ctl00_ContentPlaceHolder1_txtPhoneNo').focus()
                }
            }
            return num;
        }

        function IAmSelected(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            var Name = idno[0].split("-");
            document.getElementById('ctl00_ContentPlaceHolder1_txtCityName').value = Name[0];
            document.getElementById('ctl00_ContentPlaceHolder1_hfCity').value = idno[1];
        }



    </script>

    <div id="divMsg" runat="server">
    </div>

</asp:Content>

