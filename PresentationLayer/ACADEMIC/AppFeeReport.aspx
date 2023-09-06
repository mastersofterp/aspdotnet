<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppFeeReport.aspx.cs" Inherits="ACADEMIC_AppFeeReport" MasterPageFile="~/SiteMasterPage.master" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updatepanel1"
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
    <script>
        function validate() {
            var batch = document.getElementById('<%=ddlAdmBatch.ClientID%>').value;
            var fromDate = document.getElementById('<%=txtFromDate.ClientID%>').value;
            var toDate = document.getElementById('<%=txtToDate.ClientID%>').value;
            var degType = document.getElementById('<%=ddlDegreeType.ClientID%>').value;
            //var degree = document.getElementById('<%=ddlDegree.ClientID%>').value;
            var alertMsg = "";
            if (batch == 0 || fromDate == "" || toDate == "" || degType == 0)// degree == 0)
            {
                if (batch == 0) {
                    alertMsg += "Please select admission batch.\n";
                }
                if (fromDate == "") {
                    alertMsg += "Please enter from date.\n";
                }
                if (toDate == "") {
                    alertMsg += "Please enter to date.\n";
                }
                if (degType == 0) {
                    alertMsg += "Please select degree type.\n";
                }
                alert(alertMsg);
                return false;
            }
        }
        function checkDate() {
            var selectDate = document.getElementById('<%=txtFromDate.ClientID%>').value;          
            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
            var yyyy = today.getFullYear();
            var selDate = selectDate.split("/");
            var selday = selDate[0];
            var selmonth = selDate[1] - 1;
            var selyear = selDate[2];
            var SDate = new Date(selyear, selmonth, selday);
            if (SDate > today) {
                alert('Future Date Cannot Be Selected.');
                document.getElementById('<%=txtFromDate.ClientID%>').value = "";
                document.getElementById('<%=txtFromDate.ClientID%>').focus();
                return true;
            }
            else {
                return false;
            }
        }
        function chkGreaterDate()
        {
            var fromDate = document.getElementById('<%=txtFromDate.ClientID%>').value;
            var toDate = document.getElementById('<%=txtToDate.ClientID%>').value;
            var from = fromDate.split("/");
            var fromday = from[0];
            var frommonth = from[1]// - 1;
            var fromyear = from[2];
            var FDate = new Date(fromyear, frommonth, fromday);
            var to = toDate.split("/");
            var to_day = to[0];
            var tomonth = to[1] //- 1;
            var toyear = to[2];
         
            var toDate = new Date(toyear, tomonth, to_day);

            if (FDate > toDate)
            {
                alert('To Date Cannot Be Smaller Than From Date.');
                document.getElementById('<%=txtToDate.ClientID%>').value = "";
                document.getElementById('<%=txtToDate.ClientID%>').focus();
                return true;
            }
        }
    </script>
    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" TabIndex="1" CssClass="form-control" ToolTip="Please select admission batch." data-select2-enable="true" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgFromDate">
                                                <div class="fa fa-calendar"></div>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" CssClass="form-control" ToolTip="Please enter from date." onchange="checkDate();"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="imgFromDate" Enabled="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meStartdate" runat="server" TargetControlID="txtFromDate"
                                                Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgToDate">
                                                <div class="fa fa-calendar text-blue"></div>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" TabIndex="1" ToolTip="Please enter to date." CssClass="form-control" onchange="return chkGreaterDate();"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceEndDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="imgToDate" Enabled="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meEndDate" runat="server" TargetControlID="txtToDate"
                                                Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup> </sup>
                                            <label>Mode of Payment</label>
                                        </div>
                                        <asp:DropDownList ID="ddlMode" TabIndex="1" CssClass="form-control" ToolTip="Please select mode of payment." data-select2-enable="true" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlMode_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegreeType" TabIndex="1" CssClass="form-control" ToolTip="Please select degree type." data-select2-enable="true" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDegreeType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" TabIndex="1" CssClass="form-control" ToolTip="Please select degree." data-select2-enable="true" runat="server" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" ToolTip="Click to show." TabIndex="1" OnClick="btnShow_Click" OnClientClick="return validate();" />
                                <asp:Button ID="btnExcel" runat="server" Text="Excel Report" CssClass="btn btn-primary" tol="Click to export excel report." TabIndex="1" OnClick="btnExcel_Click" OnClientClick="return validate();" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click to cancel." TabIndex="1" OnClick="btnCancel_Click" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto" Visible="false">
                                    <asp:ListView ID="lvPayList" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h6>Fee Payment</h6>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tab-le">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Applicant Id</th>
                                                            <th>Student Name</th>
                                                            <th>Transaction No.</th>
                                                            <th>Mode of Payment</th>
                                                            <th>Payment Date</th>
                                                            <th>Amount</th>
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
                                                    <asp:Label ID="lblAppId" runat="server" Text='<%#Eval("Application Id") %>'></asp:Label>
                                                </td>
                                                 <td>
                                                     <asp:Label ID="lblStudName" runat="server" Text='<%#Eval("Student Name") %>'></asp:Label>
                                                </td>
                                                 <td>
                                                     <asp:Label ID="lblTranId" runat="server" Text='<%#Eval("Transaction No") %>'></asp:Label>
                                                </td>
                                                 <td>
                                                     <asp:Label ID="lblModePay" runat="server" Text='<%#Eval("Mode of Payment") %>'></asp:Label>
                                                </td>
                                                <td>
                                                     <asp:Label ID="lblPayDate" runat="server" Text='<%#Eval("Payment Date") %>'></asp:Label>
                                                </td>
                                                <td>
                                                     <asp:Label ID="lblAmt" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
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
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
