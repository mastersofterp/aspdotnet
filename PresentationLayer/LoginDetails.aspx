<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginDetails.aspx.cs" Inherits="LoginDetails" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Login Analysis</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date :</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="Div1" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" MaxLength="25" HorizontalAlign="Center" onchange="return CheckFutureDate(this);"
                                                CssClass="form-control" TabIndex="1">
                                            </asp:TextBox>
                                        </div>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtFromDate" PopupButtonID="Div1" />
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" OnInvalidCssClass="errordate"
                                            TargetControlID="txtFromDate" Mask="99/99/9999" MessageValidatorTip="true"
                                            MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFromDate"
                                            ErrorMessage="Please Enter From Date" ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date :</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="Div2" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>

                                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="25" HorizontalAlign="Center" onchange="return CheckFutureDate(this);"
                                                CssClass="form-control" TabIndex="1">
                                            </asp:TextBox>
                                        </div>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtToDate" PopupButtonID="Div2" />
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" OnInvalidCssClass="errordate"
                                            TargetControlID="txtToDate" Mask="99/99/9999" MessageValidatorTip="true"
                                            MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtToDate"
                                            ErrorMessage="Please Enter To Date" ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>User Type :</label>
                                        </div>
                                        <asp:DropDownList ID="ddlUserType" runat="server" AppendDataBoundItems="true" ToolTip="Please Select User Type" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged" AutoPostBack="true" TabIndex="1" data-select2-enable="true">
                                            <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvUserType" runat="server" ControlToValidate="ddlUserType" ValidationGroup="submit"
                                            ErrorMessage="Please Select User Type" Display="None" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="bntShow" runat="server" Text="Show Details" OnClick="bntShow_Click" CssClass="btn btn-primary" TabIndex="1"
                                    ValidationGroup="submit" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="1" />
                                <asp:ValidationSummary ID="valSummery" DisplayMode="List" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvUserLists" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <%--class="vista-grid"--%>
                                                <div class="sub-heading">
                                                    <h5>Users List</h5>
                                                </div>
                                                <table id="tblHead" class="table table-bordered table-hover table-fixed display" style="width: 100%;">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th style="text-align: center">Sr.No
                                                            </th>
                                                            <th style="text-align: center">User Name
                                                            </th>
                                                            <th style="text-align: center">User Full Name
                                                            </th>
                                                            <th style="text-align: center">Login Count
                                                            </th>
                                                            <%-- <th>Out Time
                                                                    </th>--%>
                                                            <th style="text-align: center">Login Hours
                                                            </th>
                                                            <th>Last Login Date
                                                            </th>
                                                            <%-- <th>TP Fees Paid Status
                                                                    </th>--%>
                                                        </tr>
                                                    </thead>
                                                    <tbody style="overflow: auto;">
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center">
                                                    <%#Container.DataItemIndex +1 %>
                                                </td>
                                                <td style="text-align: center">
                                                    <%#Eval("UA_NAME") %>
                                                </td>
                                                <td style="text-align: center">
                                                    <%#Eval("UA_FULLNAME")%>
                                                </td>
                                                <td style="text-align: center">
                                                    <%#Eval("LOGIN_COUNT")%>
                                                </td>
                                                <td style="text-align: center">
                                                    <%# (Eval("ACTIVE_HOURS")=="" || Eval("ACTIVE_HOURS")==DBNull.Value) ? "00:00:00" : Eval("ACTIVE_HOURS")%>
                                                </td>
                                                <td>
                                                    <%#Eval("LOGIN_DATE")%>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="bntShow" />
        </Triggers>
    </asp:UpdatePanel>
    <script>
        function CheckFutureDate(date1) {
            var Selected_Date = null;
            var date = date1.value.split("/");
            Selected_Date = new Date(date[2], date[1] - 1, date[0]);
            var current_Date = Date.now();
            if (Selected_Date > current_Date) {
                alert('Future date not allowed.');
                date1.value = '';
                date1.id.focus();
                return false;
            }
            else {
                if (date1.id == 'ctl00_ContentPlaceHolder1_txtToDate') {
                    var Fromdate = document.getElementById('<%= txtFromDate.ClientID%>').value.split('/');
                    var from_date = new Date(Fromdate[2], Fromdate[1] - 1, Fromdate[0]);
                    if (Selected_Date < from_date) {
                        alert('To Date should be greater than or equal to From Date.');
                        date1.value = '';
                        date1.id.focus();
                        return false;
                    }
                }
                else {
                    return true;
                }
            }
        }
    </script>
    <%--<script type="text/javascript">
        $(document).ready(function () {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>
    <script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#tblHead').DataTable({
                'pageLength': 50,
                'lengthMenu': [50, 100, 200, 500]
            });
        }
    </script>--%>
</asp:Content>
