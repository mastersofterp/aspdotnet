<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StopMaster.aspx.cs"
    Inherits="VEHICLE_MAINTENANCE_Master_StopMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">STOP MASTER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Stop Name</label>
                                        </div>
                                        <asp:TextBox ID="txtStopName" runat="server" MaxLength="50" CssClass="form-control"
                                            ToolTip="Enter Stop Name" TabIndex="1"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeRoute" runat="server" FilterType="Custom,LowerCaseLetters,UpperCaseLetters,Numbers"
                                            TargetControlID="txtStopName" ValidChars="!@#$%^&*() ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvStopName" runat="server" SetFocusOnError="true" Display="None"
                                            ErrorMessage="Please Enter Stop Name."
                                            ValidationGroup="Submit" ControlToValidate="txtStopName"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Vehicle Category </label>
                                        </div>
                                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="8" AppendDataBoundItems="true"
                                            ToolTip="Select Category">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCType" runat="server" ControlToValidate="ddlCategory" InitialValue="0"
                                            Display="None" ValidationGroup="Submit" ErrorMessage="Please Select Vehicle Category " SetFocusOnError="true">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divStudent" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Student Fee</label>
                                        </div>
                                        <asp:TextBox ID="txtStudentFee" runat="server" MaxLength="9" CssClass="form-control"
                                            ToolTip="Enter Student Fee" TabIndex="1"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom,Numbers"
                                            TargetControlID="txtStudentFee" ValidChars=".">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divEmployee" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Employee Fee</label>
                                        </div>
                                        <asp:TextBox ID="txtEmployeeFee" runat="server" MaxLength="50" CssClass="form-control"
                                            ToolTip="Enter Employee Fee" TabIndex="1"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom,Numbers"
                                            TargetControlID="txtEmployeeFee" ValidChars=".">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Sequence No </label>
                                        </div>
                                        <asp:TextBox ID="txtSeqNo" runat="server" TabIndex="2" ToolTip="Enter Sequence Number"
                                            MaxLength="5" CssClass="form-control" onkeypress="return CheckNumeric(event, this);">                                
                                        </asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers"
                                            ValidChars="+-." TargetControlID="txtSeqNo">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvSeqNo" ValidationGroup="Submit" ControlToValidate="txtSeqNo"
                                            Display="None" ErrorMessage="Please Enter Sequence No." SetFocusOnError="true" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="3"
                                    OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" />
                                <asp:Button ID="btnRport" runat="server" Text="Report" TabIndex="5"
                                    CssClass="btn btn-info" ToolTip="Click here to Show Report" OnClick="btnRport_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="4"
                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" CausesValidation="false" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvStop" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Stop Entry List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>ACTION
                                                            </th>
                                                            <th>STOP NAME
                                                            </th>
                                                            <%-- <th>STUDENT FEE
                                                    </th>--%>
                                                            <th>VEHICLE CATEGORY
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                        CommandArgument='<%# Eval("STOPID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <%# Eval("STOPNAME")%>
                                                </td>
                                                <%-- <td>
                                            <%# Eval("STUDENT_FEE")%>
                                        </td>--%>
                                                <td>
                                                    <%# Eval("CATEGORYNAME")%>
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
    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Alphabets allowed!");
                return false;
            }
            else
                return true;
        }
    </script>
</asp:Content>

