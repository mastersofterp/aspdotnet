<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="GenerateOfferLetter.aspx.cs"
    Inherits="Academic_GenerateOfferLetter" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("[id$='cbHead']").live('click', function () {
                $("[id$='chkAllot']").attr('checked', this.checked);
            });
        });
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
                            <h3 class="box-title">GENERATE OFFER LETTER</h3>
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
                                            ValidationGroup="submit">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAdmissionBatch" runat="server" ErrorMessage="Please Select Admission Batch" InitialValue="0" ControlToValidate="ddlListType" ValidationGroup="submit" Display="None">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" TabIndex="2" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                            AutoPostBack="true"
                                            ValidationGroup="submit">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ErrorMessage="Please Select Degree" InitialValue="0" ControlToValidate="ddlDegree" ValidationGroup="submit" Display="None">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Entrance Exam Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlEntrance" TabIndex="3" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True" ValidationGroup="submit">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvEnterance" runat="server" ControlToValidate="ddlEntrance"
                                            Display="None" ErrorMessage="Please Select Entrance Exam Name." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Select List Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlListType" TabIndex="4" CssClass="form-control" data-select2-enable="true" runat="server" AutoPostBack="true"
                                            ValidationGroup="submit"
                                            OnSelectedIndexChanged="ddlListType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Confirm Student List</asp:ListItem>
                                            <asp:ListItem Value="2">Waiting Student List</asp:ListItem>
                                            <asp:ListItem Value="3">Confirm-Waiting Student List</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvListType" runat="server" ErrorMessage="Please Select List Type" InitialValue="0" ControlToValidate="ddlListType" ValidationGroup="submit" Display="None">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Select Round</label>
                                        </div>
                                        <asp:DropDownList ID="ddlRound" TabIndex="5" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Round-1</asp:ListItem>
                                            <asp:ListItem Value="2">Round-2</asp:ListItem>
                                            <asp:ListItem Value="3">Round-3</asp:ListItem>
                                            <asp:ListItem Value="4">Round-4</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Offer Letter Print  Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgCalStartDate">
                                                <span class="fa fa-calendar text-blue"></span>
                                            </div>
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TabIndex="6"/>
                                            <%--  <asp:Image ID="imgCalStartDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy"
                                                TargetControlID="txtDate" PopupButtonID="imgCalStartDate" Enabled="true"
                                                EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:CompareValidator ID="valStartDateType" runat="server" ControlToValidate="txtDate"
                                                Display="None" ErrorMessage="Please enter a valid date." Operator="DataTypeCheck"
                                                SetFocusOnError="true" Type="Date" CultureInvariantValues="true" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgCalFromDate">
                                                <span class="fa fa-calendar text-blue"></span>
                                            </div>

                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TabIndex="7"></asp:TextBox>
                                            <%--  <asp:Image ID="imgCalFromDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtenderFromDate" runat="server" Format="dd-MM-yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true"
                                                EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:CompareValidator ID="valFromDateType" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please enter a valid date." Operator="DataTypeCheck"
                                                SetFocusOnError="true" Type="Date" CultureInvariantValues="true" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgClaToDate">
                                                <span class="fa fa-calendar text-blue"></span>
                                            </div>

                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TabIndex="8"></asp:TextBox>
                                            <%--  <asp:Image ID="imgClaToDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtenderToDate" runat="server" Format="dd-MM-yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="imgClaToDate" Enabled="true"
                                                EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:CompareValidator ID="valToDateType" runat="server" ControlToValidate="txtToDate"
                                                Display="None" ErrorMessage="Please enter a valid date." Operator="DataTypeCheck"
                                                SetFocusOnError="true" Type="Date" CultureInvariantValues="true" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="submit" OnClick="btnShow_Click" />
                                <asp:Button runat="server" ID="btnOfferletter" Text="Generate Offer Letter" CssClass="btn btn-primary" OnClick="btnOfferletter_Click" />
                                <asp:Button runat="server" ID="btnSendEmail" Text="Send Mail" OnClick="btnSendEmail_Click" CssClass="btn btn-primary" />
                                <asp:Button runat="server" ID="btnDownload" Text="Download" OnClick="btnDownload_Click" Visible="false" ToolTip="Download" CssClass="btn btn-primary" />
                                <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" CausesValidation="false" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ValidationGroup="submit" ShowSummary="False" />
                            </div>

                            <div class="col-12" id="dvListView">
                                <asp:ListView ID="lvStudents" runat="server" OnItemDataBound="lvStudents_ItemDataBound">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Select Students to Generate Offer Letter</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="cbHead" runat="server" />
                                                        Plz Check
                                                    </th>
                                                    <th>Sr No.
                                                    </th>
                                                    <th>Merit list no.
                                                    </th>
                                                    <th>Application ID
                                                    </th>
                                                    <th>Email ID
                                                    </th>
                                                    <th>Student Name
                                                    </th>
                                                    <th>Branch Preference
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
                                            <td>
                                                <asp:CheckBox ID="chkAllot" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("USERNO")%>' />
                                            </td>
                                            <td>
                                                <%# Eval("SRNO")%>
                                                <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("USERNO") %>' />
                                            </td>
                                            <td>
                                                <%# Eval("MERIT_LIST_NO")%>
                                            </td>

                                            <td>
                                                <%# Eval("APPLICATIONID")%>
                                                <asp:HiddenField ID="hdfAppliid" runat="server" Value='<%# Eval("APPLICATIONID") %>' />

                                            </td>

                                            <td>
                                                <%# Eval("FIRSTNAME")%>
                                                <%# Eval("LASTNAME")%>
                                                <asp:HiddenField ID="hdfirstname" runat="server" Value='<%# Eval("FIRSTNAME") %>' />
                                                <asp:HiddenField ID="hdlastname" runat="server" Value='<%# Eval("LASTNAME") %>' />
                                            </td>
                                            <td>
                                                <%# Eval("EMAILID")%>
                                                <asp:HiddenField ID="hdfEmailid" runat="server" Value='<%# Eval("EMAILID") %>' />
                                            </td>
                                            <td>
                                                <%#Eval("BRANCHNAME_PREF")%>
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
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnOfferletter" />
            <asp:PostBackTrigger ControlID="btnSendEmail" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
