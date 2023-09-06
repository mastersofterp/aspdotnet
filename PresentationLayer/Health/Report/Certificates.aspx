<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Certificates.aspx.cs" Inherits="Health_Report_Certificates" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../includes/prototype.js" type="text/javascript"></script>

    <script src="../../includes/scriptaculous.js" type="text/javascript"></script>

    <script src="../../includes/modalbox.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="updOpdTransaction" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">GENERATE CERTIFICATES</h3>
                        </div>
                        <div>
                            <form role="form">
                                <div class="box-body">
                                    <div class="col-md-12">
                                        Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                        <asp:Panel ID="pnlAdd" runat="server">
                                            <div class="panel panel-info">
                                                <div class="panel panel-heading">Generate Certificates</div>
                                                <div class="panel panel-body">
                                                    <div class="form-group col-md-12">
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group col-md-5">
                                                                <label><span style="color: Red">*</span>Search Patient:</label>
                                                            </div>
                                                            <div class="form-group col-md-5">
                                                                <asp:TextBox ID="txtPatientName" runat="server" CssClass="form-control" ToolTip="Search Patient"
                                                                    MaxLength="100" TabIndex="1"></asp:TextBox>
                                                                <%--<asp:DropDownList ID="ddlEmployee" runat="server" AppendDataBoundItems="true" 
                                                                    Width="255px" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                                                    </asp:DropDownList>--%>
                                                                <asp:RequiredFieldValidator ID="rfvPatientName" runat="server" ControlToValidate="txtPatientName"
                                                                    Display="None" ErrorMessage="Please Select Patient Name" SetFocusOnError="true"
                                                                    ValidationGroup="Submit" />
                                                                <asp:Label ID="lblPatientCat" runat="server" Text="" Visible="false"></asp:Label>
                                                            </div>
                                                            <div class="form-group col-md-2">
                                                                <a href="#" title="Search Patient Details" onclick="Modalbox.show($('divdemo2'), 
                                                                    {title: this.title, width: 600,overlayClose:false});return false;">
                                                                    <asp:Image ID="imgSearch" runat="server" ImageUrl="~/images/search.png" TabIndex="2" />
                                                                    <asp:HiddenField ID="hfPatientName" runat="server" />
                                                                </a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-12" id="tr1" runat="server" visible="false">
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group col-md-5">
                                                                <label><span style="color: #FF0000">*</span>Patient's Visit Date:</label>
                                                            </div>
                                                            <div class="form-group col-md-7">
                                                                <asp:DropDownList ID="ddlVisitDate" runat="server" TabIndex="3" AppendDataBoundItems="true"
                                                                    CssClass="form-control" ToolTip="Select Patients's Visit Date">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvVDt" runat="server" SetFocusOnError="true" Display="None" ErrorMessage="Please select patients visit date."
                                                                    ValidationGroup="Submit" ControlToValidate="ddlVisitDate" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group col-md-5">
                                                                <label><span style="color: #FF0000">*</span>Issue Date:</label>
                                                            </div>
                                                            <div class="form-group col-md-7">
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                    </div>
                                                                    <asp:TextBox ID="txtIssueDt" runat="server" CssClass="form-control" ToolTip="Enter Issue Date"
                                                                        Style="z-index: 0;" TabIndex="4"></asp:TextBox>
                                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true"
                                                                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtIssueDt">
                                                                    </ajaxToolKit:CalendarExtender>
                                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtIssueDt"
                                                                        ClearMaskOnLostFocus="true">
                                                                    </ajaxToolKit:MaskedEditExtender>
                                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" IsValidEmpty="false"
                                                                        ControlExtender="MaskedEditExtender2" ControlToValidate="txtIssueDt" Display="None"
                                                                        ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Text="*"
                                                                        InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                                        ValidationGroup="Submit"></ajaxToolKit:MaskedEditValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group col-md-5">
                                                                <label><span style="color: Red">*</span>Remark:</label>
                                                            </div>
                                                            <div class="form-group col-md-7">
                                                                <asp:TextBox ID="txtRemark" runat="server" MaxLength="100" CssClass="form-control"
                                                                    TextMode="MultiLine" ToolTip="Enter Remark" TabIndex="5"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvRem" runat="server" ControlToValidate="txtRemark" Display="None" ErrorMessage="Please enter remark" SetFocusOnError="true"
                                                                    ValidationGroup="Submit" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <div class="form-group col-md-6">
                                                            <div class="form-group col-md-5">
                                                                <label><span style="color: Red">*</span>Certificates Name:</label>
                                                            </div>
                                                            <div class="form-group col-md-7">
                                                                <asp:DropDownList ID="ddlCertiName" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                                    ToolTip="Select Certificates Name" TabIndex="6">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">Medical Certificate</asp:ListItem>
                                                                    <asp:ListItem Value="2">Reference Certificate</asp:ListItem>
                                                                    <asp:ListItem Value="3">Fitness Certificate</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvCertiName" runat="server" ControlToValidate="ddlCertiName" Display="None" ErrorMessage="Please select certificate name" SetFocusOnError="true"
                                                                    ValidationGroup="Submit" InitialValue="0" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </form>
                        </div>
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary"
                                    ValidationGroup="Submit" ToolTip="Click here to Submit" TabIndex="7" />&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning"
                                    ToolTip="Click here to Reset" TabIndex="8" />&nbsp;&nbsp;
                                <%--<asp:Button ID="btnMedCertificate" runat="server" Text="Medical Certificate" OnClick="btnMedCertificate_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnRefCertificate" runat="server" Text="Reference Certificate" OnClick="btnRefCertificate_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnFitCertificate" runat="server" Text="Fitness Certificate" OnClick="btnFitCertificate_Click" />--%>
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" />
                            </p>
                            <div class="col-md-12">
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvCertificate" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <h4 class="box-title">Issue Certificates Entry List
                                                </h4>
                                                <table class="table table-bordered table-hover">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>EDIT
                                                            </th>
                                                            <th>ISSUE DATE
                                                            </th>
                                                            <th>CERTIFICATE NAME
                                                            </th>
                                                            <th>PATIENT NAME
                                                            </th>
                                                            <th>REMARK
                                                            </th>
                                                            <th>PRINT
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                                        CommandArgument='<%# Eval("CI_ID") %>' AlternateText="Edit Record"
                                                        ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <%# Eval("CI_DATE", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PATIENT_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("REMARK")%>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnPrint" runat="server" Text="Print" CommandArgument=' <%# Eval("CERTIFICATENAME")%>' ToolTip="Take print"
                                                        OnClick="btnPrint_Click1" CommandName=' <%# Eval("OPDID")%>' CssClass="btn btn-primary" />
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
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <%-- <td class="vista_page_title_bar" valign="top" style="height: 30px">GENERATE CERTIFICATES                       
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                    </td>--%>
                </tr>
                <%--PAGE HELP--%>
                <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
                <tr>
                    <td>
                        <!-- "Wire frame" div used to transition from the button to the info panel -->
                        <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                        </div>
                        <!-- Info panel to be displayed as a flyout when the button is clicked -->
                        <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                            <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                                <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                    ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                            </div>
                            <div>
                                <p class="page_help_head">
                                    <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                                    <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                                    Edit Record
                                </p>
                                <p class="page_help_text">
                                    <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                                </p>
                            </div>
                        </div>

                        <script type="text/javascript" language="javascript">
                            // Move an element directly on top of another element (and optionally
                            // make it the same size)
                            function Cover(bottom, top, ignoreSize) {
                                var location = Sys.UI.DomElement.getLocation(bottom);
                                top.style.position = 'absolute';
                                top.style.top = location.y + 'px';
                                top.style.left = location.x + 'px';
                                if (!ignoreSize) {
                                    top.style.height = bottom.offsetHeight + 'px';
                                    top.style.width = bottom.offsetWidth + 'px';
                                }
                            }
                        </script>
                    </td>
                </tr>
                <tr>
                    <%--<td>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                    </td>--%>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divdemo2" style="display: none; height: 550px">
        <asp:UpdatePanel ID="updEdit" runat="server">
            <ContentTemplate>
                <div class="form-group col-md-12">
                    <div class="form-group col-md-6">
                        <div class="form-group col-md-3">
                            <label>Search Criteria:</label>
                        </div>
                        <div class="form-gorup col-md-9">
                            <asp:RadioButtonList ID="rbPatient" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Emp. Code" Value="Employee Code" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Emp. Name" Value="Employee Name"></asp:ListItem>
                                <asp:ListItem Text="Student Name" Value="Student Name"></asp:ListItem>
                                <asp:ListItem Text="Student RegNo" Value="Student RegNo"></asp:ListItem>
                                <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
                <div class="form-group col-md-12">
                    <div class="form-group col-md-6">
                        <div class="form-group col-md-3">
                            <label>Search String:</label>
                        </div>
                        <div class="form-gorup col-md-9">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" ToolTip="Enter Search String"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group col-md-12">
                    <div class="text-center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" ToolTip="Click here to Search"
                            OnClientClick="return submitPopup(this.name);" />
                        <asp:Button ID="btnCanceModal" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Reset"
                            OnClientClick="return ClearSearchBox(this.name)" />
                    </div>
                </div>
                <div class="form-group col-md-12">
                    <asp:Panel ID="pnlSearchlist" runat="server" ScrollBars="Auto">
                        <asp:ListView ID="lvEmp" runat="server">
                            <LayoutTemplate>
                                <div id="lgv1">
                                    <h4 class="box-title">Login Details
                                    </h4>
                                    <table class="table table-bordered table-hover">
                                        <thead>
                                            <tr class="bg-light-blue" id="trHeader" runat="server">
                                                <th>Name
                                                </th>
                                                <th>Employee Code
                                                </th>
                                                <th>Department
                                                </th>
                                                <th>Designation
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
                                        <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name")%>' CommandArgument='<%# Eval("IDNo")%>'
                                            OnClick="lnkId_Click"></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%# Eval("PFILENO")%>
                                    </td>
                                    <td>
                                        <%# Eval("SUBDEPT")%>
                                    </td>
                                    <td>
                                        <%# Eval("SUBDESIG")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript" language="javascript">

        function submitPopup(btnsearch) {
            var rbText;
            var searchtxt;
            if (document.getElementById('ctl00_ContentPlaceHolder1_rbPatient_0').checked == true)
                rbText = "EmployeeCode";
            else if (document.getElementById('ctl00_ContentPlaceHolder1_rbPatient_1').checked == true)
                rbText = "EmployeeName";
            else if (document.getElementById('ctl00_ContentPlaceHolder1_rbPatient_2').checked == true)
                rbText = "StudentName";
            else if (document.getElementById('ctl00_ContentPlaceHolder1_rbPatient_3').checked == true)
                rbText = "StudentRegNo";
            else if (document.getElementById('ctl00_ContentPlaceHolder1_rbPatient_4').checked == true)
                rbText = "Other";



            //if (document.getElementById('ContentPlaceHolder1_rbPatient_0').checked == true)
            //    rbText = "EmployeeCode";
            //else if (document.getElementById('ContentPlaceHolder1_rbPatient_1').checked == true)
            //    rbText = "EmployeeName";
            //else if (document.getElementById('ContentPlaceHolder1_rbPatient_2').checked == true)
            //    rbText = "StudentName";
            //else if (document.getElementById('ContentPlaceHolder1_rbPatient_3').checked == true)
            //    rbText = "StudentRegNo";
            //else if (document.getElementById('ContentPlaceHolder1_rbPatient_4').checked == true)
            //    rbText = "Other";


            searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;
            __doPostBack(btnsearch, rbText + ',' + searchtxt);
            return true;
        }

        function ClearSearchBox() {
            document.getElementById('<%=txtSearch.ClientID %>').value = '';
        }

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
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

        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../images/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../images/collapse_blue.jpg";
            }
        }
    </script>

    <script type="text/javascript">

        function ItemName(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            var Name = idno[0].split("-");
            document.getElementById('ctl00_ContentPlaceHolder1_txtItemName').value = idno[1];
            document.getElementById('ctl00_ContentPlaceHolder1_hfItemName').value = Name[0];
        }
        //  keeps track of the delete button for the row      
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');
            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }
    </script>
    <div id="divMsg" runat="server">
    </div>

</asp:Content>

