<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Approve_Bonafide_Certificate.aspx.cs" Inherits="ACADEMIC_Approve_Bonafide_Certificate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../newbootstrap/js/jquery-3.3.1.min.js"></script>--%>

    <script src="../bootstrap/js/jquery-3.6.0.min.js"></script>
    <%--<script src="../../newbootstrap/js/jquery-3.3.1.min.js"></script>--%>
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBonafide"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updBonafide" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>APPROVE CERTIFICATE</b></h3>
                            <div class="box-tools pull-right">
                                <span style="Color: Red;"><b>Note : * Marked fields are Mandatory</b></span>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="col-12 mt-3">
                                <div class="row">
                                    <%-- <legend>Criteria for Report Generation</legend>--%>
                                    <div class="form-group col-md-3">
                                        <label><span style="Color: Red;">* </span>Admission Batch</label>
                                        <asp:DropDownList ID="ddlAdmbatch" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlAdmbatch_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvadmbatch" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" SetFocusOnError="true" ValidationGroup="submit"
                                            InitialValue="0" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" SetFocusOnError="true" ValidationGroup="SHOW"
                                            InitialValue="0" />
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label><span style="Color: Red;">* </span>Certificate</label>
                                        <asp:DropDownList ID="ddlCertificate" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlCertificate_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvClg" runat="server" ControlToValidate="ddlCertificate"
                                            Display="None" ErrorMessage="Please Select Certificate" SetFocusOnError="true" ValidationGroup="submit" InitialValue="0" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCertificate"
                                            Display="None" ErrorMessage="Please Select Certificate" SetFocusOnError="true" ValidationGroup="SHOW" InitialValue="0" />
                                    </div>
                                    <div class="form-group col-md-3" id="div2" runat="server" visible="false">
                                        <label><span style="Color: Red;">* </span>Select Certificate</label>
                                        <asp:DropDownList ID="ddlSelectcertificate" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" AppendDataBoundItems="true" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                            <asp:ListItem Value="1">TC Certificate Full Time</asp:ListItem>
                                            <asp:ListItem Value="2">TC Certificate Part Time</asp:ListItem>
                                            <asp:ListItem Value="3">DisContinue</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSelectcertificate"
                                            Display="None" ErrorMessage="Please Select Certificate" SetFocusOnError="true" ValidationGroup="submit" InitialValue="0" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="issuedate" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>
                                                Date Of Issue
                                              <asp:CompareValidator ID="cvissuedate" runat="server" ControlToValidate="txtissuedate"
                                                  EnableClientScript="False" ErrorMessage="Please enter Date of Issue(mm/dd/yyyy)."
                                                  Operator="DataTypeCheck" Type="Date" ValidationGroup="Print"></asp:CompareValidator>
                                            </label>

                                        </div>

                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgissuedate">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtissuedate" runat="server"></asp:TextBox>
                                            <%-- <asp:Image ID="imgissuedate" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />--%>
                                            <ajaxToolKit:CalendarExtender ID="ceissuedate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                PopupButtonID="imgissuedate" TargetControlID="txtissuedate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meissuedate" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" Mask="99/99/9999" MaskType="Date" OnInvalidCssClass="errordate"
                                                TargetControlID="txtissuedate">
                                            </ajaxToolKit:MaskedEditExtender>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-3" id="divconduct" runat="server" visible="false">
                                        <label><span style="Color: Red;">* </span>Conduct and Character</label>
                                        <asp:DropDownList ID="ddlconductcharacter" runat="server" AppendDataBoundItems="True" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlconductcharacter"
                                            Display="None" ErrorMessage="Please Select Conduct & Character for selected Students." SetFocusOnError="true" ValidationGroup="submit" InitialValue="0" />
                                    </div>
                                    <div class="form-group col-md-3" id="divGRegNo" runat="server" visible="false">
                                        <label><span style="Color: Red;">* </span>G.RegNo</label>
                                        <asp:TextBox ID="txtGReg" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtGReg"
                                            Display="None" ErrorMessage="Please Enter General Registration No." SetFocusOnError="true" ValidationGroup="submit" InitialValue="0" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <p class="text-center">
                            <asp:Button ID="btnShow" runat="server" TabIndex="2" Text="Show" ToolTip="Click to Show Student" OnClick="btnShow_Click" CssClass="btn btn-info" ValidationGroup="SHOW" />
                            <asp:Button ID="btnSubmit" runat="server" TabIndex="3" Text="Submit" ToolTip="Click to Submit" OnClick="btnSubmit_Click"
                                class="btn btn-success" ValidationGroup="submit" />
                            <asp:Button ID="btnExcelReport" runat="server" Text="Excel Report" ValidationGroup="SHOW" OnClick="btnExcelReport_Click" TabIndex="4" CssClass="btn btn-info" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="5" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                            <asp:ValidationSummary ID="vssummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="submit" />
                             <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="SHOW" />

                            <div class="col-md-2" runat="server" id="divtotalselect" visible="false">
                                <label>
                                    Total Selected Student</label>
                                <asp:TextBox ID="txtTotStud" runat="server" CssClass="watermarked" />
                                <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                            </div>
                        </p>

                        <div class="box-footer">
                            <div class="form-group col-md-12 table table-responsive">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="400px" Visible="false">
                                    <asp:ListView ID="lvStudents" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <h4>Student List</h4>
                                            </div>

                                            <table class="table table-hover table-bordered table-striped" id="tbllist">
                                                <%--    <table class="table table-hover table-bordered table-striped" id="divschemelist">--%>
                                                <thead>
                                                    <tr class="bg-light-blue" style="background-color: #337ab7; color: black">
                                                        <%--  <tr class="bg-light-blue">--%>
                                                        <th>
                                                            <asp:CheckBox ID="cbHead" runat="server" onclick="totAllStudent(this)" />
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Admission Batch
                                                        </th>
                                                        <th>Certificate Name
                                                        </th>
                                                        <th>Registration No.
                                                        </th>
                                                        <th>Degree
                                                        </th>
                                                        <th>Branch 
                                                        </th>
                                                        <th>Approve Status 
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
                                                    <asp:CheckBox ID="cbRow" runat="server" Enabled='<%# Convert.ToInt32(Eval("APPROVESTATUS")) ==1 ? false :true %>' ToolTip='<%# Eval("IDNO")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("STUDNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("BATCHNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CERT_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("REGNO")%>               
                                                </td>
                                                <td>
                                                    <%# Eval("DEGREENAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("LONGNAME")%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblApprovestatus" runat="server" Font-Bold="true" ForeColor='<%# Convert.ToInt32(Eval("APPROVESTATUS")) == 1 ? System.Drawing.Color.Green : System.Drawing.Color.Red %>' Text='<%# Convert.ToInt32(Eval("APPROVESTATUS")) ==1 ? "Approve" : "Pending" %>'></asp:Label>
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
            <asp:PostBackTrigger ControlID="btnExcelReport" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function totAllStudent(headchk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');

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

            if (headchk.checked == true)
                txtTot.value = hdfTot.value - 2;
            else
                txtTot.value = 0;

        }
        //function check(chk) {
        //    var checkid = chk.id;
        //    var myArray = new Array();
        //    var myString = "" + chk.id + "";
        //    myArray = myString.split("_");
        //    var index = myArray[3];
        //    if (chk.checked)
        //        document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_" + index + "_ddlBranch").disabled = false;
        //    else
        //        document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_" + index + "_ddlBranch").disabled = true;
        //}

        $(document).ready(function () {
            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });
        function bindDataTable() {
            var myDT = $('#tbllist').DataTable({
                "bDestroy": true,
                'pageLength': 50,
                'lengthMenu': [50, 100, 250]

            });
        }
    </script>
</asp:Content>

