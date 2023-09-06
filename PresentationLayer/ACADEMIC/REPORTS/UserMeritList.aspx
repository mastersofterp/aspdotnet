<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="UserMeritList.aspx.cs" Inherits="Academic_UserMeritList" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updLists"
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

    <asp:UpdatePanel ID="updLists" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Applicant Merit List Generation</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" TabIndex="1" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True"
                                            ValidationGroup="submit" Font-Bold="True">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Admission Batch." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList runat="server" AutoPostBack="true" ValidationGroup="submit" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="2" AppendDataBoundItems="true" ID="ddlApplicantType" OnSelectedIndexChanged="ddlApplicantType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <%-- <asp:ListItem Value="1">UG</asp:ListItem>
                                                p:ListItem sValue="2">PG</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvApplicant" runat="server" ControlToValidate="ddlApplicantType"
                                            Display="None" ErrorMessage="Please select Degree" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Entrance Exam Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmcat" TabIndex="3" ValidationGroup="submit" CssClass="form-control" data-select2-enable="true"
                                            runat="server" AppendDataBoundItems="True">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAdmcat" runat="server" ControlToValidate="ddlAdmcat"
                                            Display="None" ValidationGroup="submit" ErrorMessage="Please Select Exam Type."
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Cut Off</label>
                                        </div>
                                        <asp:TextBox runat="server" ID="txtCutOff" CssClass="form-control" TabIndex="4" onkeypress="return isNumber(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvCutOff" ControlToValidate="txtCutOff"
                                            Display="None" ErrorMessage="Please Enter Cut Off Marks" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Application Considered Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgAppDate">
                                                <span class="fa fa-calendar text-blue"></span>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtAppDate" TabIndex="5" ToolTip="Please Enter Application Considered Date"></asp:TextBox>
                                                <%--         <asp:Image ID="imgAppDate" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                            <ajaxToolKit:CalendarExtender ID="ceAppDate" runat="server" Format="MM/dd/yyyy"
                                                TargetControlID="txtAppDate" PopupButtonID="imgAppDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meAppDate" runat="server" TargetControlID="txtAppDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvAppDate" runat="server" EmptyValueMessage="Please Enter Application Considered Date"
                                                ControlExtender="meAppDate" ControlToValidate="txtAppDate" IsValidEmpty="false"
                                                InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Date of Declared"
                                                InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtAppDate"
                                                Display="None" ErrorMessage="Please Application Considered Date" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShowStudent" runat="server" CssClass="btn btn-primary" ValidationGroup="submit" Text="Generate Merit List" OnClick="btnShowStudent_Click"/>
                                <asp:Button ID="btnReport" runat="server" ValidationGroup="submit" CssClass="btn btn-info" Text="Report" OnClick="btnReport_Click" />                                  
                                <asp:Button runat="server" ID="btnexport" Text="Excel Report" CssClass="btn btn-info" ValidationGroup="submit" OnClick="btnexport_Click" />
                                <asp:Button runat="server" ID="btnCancel" Text="Cancel" ValidationGroup="s" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ValidationGroup="submit" ShowSummary="False" />
                            </div>

                            <div class="col-md-12">
                                <asp:ListView ID="lvMeritList" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Applied student List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th >Sr No.
                                                    </th>
                                                    <th >User No
                                                    </th>
                                                    <th>Student Name
                                                    </th>
                                                    <th >Exam No.
                                                    </th>
                                                    <th >Qualify Exam
                                                    </th>
                                                    <th >Verify Mark
                                                    </th>
                                                    <th >Application Date
                                                    </th>
                                                    <th >Merit No.
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                 <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>                          
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td >
                                                  <%# Container.DataItemIndex + 1 %>
                                            </td>

                                            <td >
                                                <%# Eval("USERNAME") %>   
                                            </td>
                                            <td>
                                                <%# Eval("FIRSTNAME") %>   
                                            </td>
                                            <td >
                                                <%# Eval("REGNO") %>   
                                            </td>
                                            <td >
                                                <%# Eval("QUALIEXMNAME") %>   
                                            </td>
                                            <td >
                                                <%# Eval("VERIFY_MARKS") %>   
                                            </td>
                                             <td >
                                                <%# Eval("APPLY_DATE") %>   
                                            </td>
                                            <td >
                                                <%# Eval("RANKNO") %>   
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnexport" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
