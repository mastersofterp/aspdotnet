<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master"
    MaintainScrollPositionOnPostback="true" CodeFile="PaymentTypeModification.aspx.cs" Inherits="ACADEMIC_PaymentTypeModification" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


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
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">PAYMENT TYPE MODIFICATION</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Selection Criteria</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>College</label>--%>
                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" ToolTip="Please Select School/Institute." AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" ValidationGroup="submit">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select School/Institute." InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Session</label>--%>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Admission Batch."
                                            CssClass="form-control" data-select2-enable="true" ValidationGroup="submit">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Select Receipt Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlRecType" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="true"
                                            ValidationGroup="submit" ToolTip="Please Select Receipt Type">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvRecType" runat="server" ErrorMessage="Please Select Receipt Type"
                                            Display="None" ControlToValidate="ddlRecType" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="submit" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Registration No.</label>--%>
                                            <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtAppID" runat="server" ToolTip="Please Enter Application ID" ValidationGroup="submit"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvappid" runat="server" Display="None" ErrorMessage="Please Enter Registration No."
                                            ValidationGroup="submit" ControlToValidate="txtAppID"></asp:RequiredFieldValidator>
                                    </div>

                                    <div>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ValidationGroup="submit" ShowSummary="False" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ValidationGroup="submit1" ShowSummary="False" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Details" ValidationGroup="submit"
                                    class="buttonStyle ui-corner-all" OnClick="btnShow_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Update Payment Type & Modify Demand" Enabled="false" OnClick="btnSubmit_Click"
                                    class="buttonStyle ui-corner-all" CssClass="btn btn-primary" ValidationGroup="submit1" />
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click" class="buttonStyle ui-corner-all"
                                    CssClass="btn btn-warning" />

                                <asp:Label ID="lblNote" runat="server" Text="" ForeColor="Green"></asp:Label>

                                <asp:Label ID="lblNote2" runat="server" Text=""></asp:Label></b>
                            </div>

                            <div class="form-group col-lg-9 col-md-12 col-12">
                                <div class=" note-div">
                                    <h5 class="heading">Note </h5>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Previous Fees demand and receipt will be cancelled after payment type modification if Fees already paid by student.</span> </p>
                                </div>
                            </div>

                            <div id="dvListView" class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvstudList" runat="server" Visible="false" OnItemDataBound="lvstudList_ItemDataBound">
                                        <LayoutTemplate>
                                            <div id="listViewGrid">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblstudDetails">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <%-- <th>Sr. No.
                                                            </th>--%>
                                                            <th>Student Name
                                                            </th>
                                                            <th>
                                                                <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label></th>
                                                            </th>
                                                            <th>
                                                                <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label></th>
                                                            </th>
                                                            <th>
                                                                <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label></th>
                                                            </th>
                                                            <th>
                                                                <asp:Label ID="lblDYlvAdmBatch" runat="server" Font-Bold="true"></asp:Label></th>
                                                            </th>
                                                            <th>Current Payment Type</th>
                                                            <%--  <th>Payment Type
                                                            </th>--%>
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
                                                <%-- <td>

                                                    <%# Container.DataItemIndex + 1%>

                                                </td>--%>
                                                <td>

                                                    <asp:Label ID="lblfirstname" runat="server" Text='<%# Eval("STUDNAME")%>' ToolTip='<%# Eval("STUDNAME")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdnIDNO" runat="server" Value='<%# Eval("IDNO")%>' />
                                                </td>

                                                <td>

                                                    <asp:Label ID="lblusername" runat="server" Text='<%# Eval("REGNO")%>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lbldegree" runat="server" Text='<%# Eval("DEGREENAME")%>' ToolTip='<%# Eval("DEGREENO")%>'></asp:Label>


                                                </td>
                                                <td>
                                                    <asp:Label ID="lblbranch" runat="server" Text='<%# Eval("LONGNAME")%>' ToolTip='<%# Eval("BRANCHNO")%>'></asp:Label>

                                                </td>

                                                <td>
                                                    <asp:Label ID="lblAdmBatch" runat="server" Text='<%# Eval("BATCHNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPayType" runat="server" Text='<%# Eval("PAYTYPENAME")%>' ToolTip='<%# Eval("PTYPE")%>'></asp:Label>
                                                </td>
                                                <td style="display: none;">
                                                    <asp:DropDownList ID="ddlpaymenttype" runat="server" AppendDataBoundItems="True" Visible="false"></asp:DropDownList>

                                                    <asp:HiddenField ID="hdpay" runat="server" Value='<%# Eval("PTYPE")%>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Label ID="lblNote1" runat="server" Text=""></asp:Label>
                                <div id="divMsg" runat="server"></div>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel2" runat="server" Visible="false">
                                    <div id="divUpdatePayType" runat="server">
                                        <div class="sub-heading">
                                            <h5>Selection Criteria</h5>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Payment Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlPayType" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Payment Type."
                                                    CssClass="form-control" data-select2-enable="true" ValidationGroup="submit1">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPayType"
                                                    Display="None" ErrorMessage="Please Select Payment Type." InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="submit1">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <%--<label>Semester</label>--%>
                                                    <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label></th>
                                                </div>
                                                <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Semester"
                                                    CssClass="form-control" data-select2-enable="true" ValidationGroup="submit1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="submit1"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Remark</label>
                                                </div>
                                                <asp:TextBox ID="txtRemark" runat="server" ToolTip="Please Enter Application ID" ValidationGroup="submit1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None" ErrorMessage="Please Enter Remark"
                                                    ValidationGroup="submit1" ControlToValidate="txtRemark"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label></label>
                                                </div>
                                                <asp:CheckBox ID="chkOverwrite" Text="Cancel Existing Demand" runat="server" Checked="true" ValidationGroup="submit1" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Label ID="lblMsg" Font-Bold="true" runat="server" SkinID="lblmsg"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btncancel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
