<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPTestDetails_Score_Assign_Verify.aspx.cs" Inherits="ACADEMIC_POSTADMISSION_ADMPTestDetails_Score_Assign_Verify" ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        .modalPopup {
            background: #fff;
            padding: 15px;
            box-shadow: 0px 0px 12px rgba(0,0,0,0.4);
        }
         #ctl00_ContentPlaceHolder1_DataPager1 a {
            background-color: #fff;
            border: 1px solid;
            padding: 6px 10px;
            border-radius: 19px;
            text-decoration: none;
        }

        #ctl00_ContentPlaceHolder1_NumberDropDown {
            padding-top: 0.25rem;
            padding-bottom: 0.25rem;
            padding-left: 0.5rem;
            font-size: .845rem;
            border-radius: 0.25rem;
            border: 1px solid #ccc;
        }
        /*.default-c .paginate_button.page-item.active a {
        }*/

        #ctl00_ContentPlaceHolder1_DataPager1 span {
            background-color: #0d70fd;
            border: 1px solid #fff;
            padding: 6px 10px;
            border-radius: 19px;
            text-decoration: none;
            color: #fff;
        }
           .thverify {
            top: -1px;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="UpdateProgress4" runat="server"
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
    <asp:UpdatePanel ID="updtestdetails" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Application Batch</label>
                                    </div>
                                    <asp:DropDownList runat="server" class="form-control" ID="ddlAdmBatch" ValidationGroup="TestScore" AutoPostBack="true" AppendDataBoundItems="true"
                                        data-select2-enable="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch" ValidationGroup="TestScore"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Admission Batch">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Program Type</label>
                                    </div>
                                    <asp:DropDownList runat="server" class="form-control" ID="ddlProgramType" ValidationGroup="TestScore" AutoPostBack="true" AppendDataBoundItems="true"
                                        data-select2-enable="true" OnSelectedIndexChanged="ddlProgramType_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvProgramType" runat="server" ControlToValidate="ddlProgramType" ValidationGroup="TestScore"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Program Type">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Test</label>
                                    </div>
                                    <asp:DropDownList runat="server" class="form-control" ID="ddlTestName" ValidationGroup="TestScore" AppendDataBoundItems="true"
                                        data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvTestName" runat="server" ControlToValidate="ddlTestName" ValidationGroup="TestScore"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Test Name">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Payment Status</label>
                                    </div>
                                    <asp:DropDownList runat="server" class="form-control" ID="ddlpayment" ValidationGroup="TestScore" AppendDataBoundItems="true"
                                        data-select2-enable="true" OnSelectedIndexChanged="ddlpayment_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">Paid</asp:ListItem>
                                        <asp:ListItem Value="2">Unpaid</asp:ListItem>

                                    </asp:DropDownList>
                                  
                                </div>

                            </div>
                        </div>

                        <div class="col-12 btn-footer" id="buttonSection" runat="server">

                            <asp:LinkButton ID="btnShow" runat="server" ValidationGroup="TestScore" Text="Show" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnLock" runat="server" Text="Lock" CssClass="btn btn-outline-info" TabIndex="1" OnClick="btnLock_Click" Visible="false" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="TestScore" />
                        </div>

                        <div class="col-md-12">
                            <asp:Panel ID="pnlTestScore" runat="server" Visible="false">
                                 <div class="row">
                                        <div class="form-group col-lg-9 col-md-6 col-6">
                                            <asp:Label ID="lblshow" runat="server" Text="Show "></asp:Label>
                                            <asp:DropDownList ID="NumberDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="NumberDropDown_SelectedIndexChanged">
                                                <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                <asp:ListItem Text="300" Value="300"></asp:ListItem>
                                                <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                                <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                                                <asp:ListItem Text="1500" Value="1500"></asp:ListItem>
                                                <asp:ListItem Text="2000" Value="2000"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lblentries" runat="server" Text=" entries"></asp:Label>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-6" style="text-align:right">
                                            <input type="text" id="FilterData" class="form-control" placeholder="Search" />
                                            <div class="input-group-addon d-none">
                                                <i class="fa fasss-search"></i>
                                            </div>
                                        </div>
                                    </div>
                            
                                <asp:ListView ID="lvTestScore" runat="server" Visible="true" OnPagePropertiesChanging="lvTestScore_PagePropertiesChanging">
                                    <LayoutTemplate>
                                        <div class="table-responsive" style="height: 400px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                            <table class="table table-striped table-bordered nowrap"  id="tblData1">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th runat="server" id="thcheck">
                                                            <asp:CheckBox ID="chkAll" onclick="checkAll(this)" Text="" runat="server" />
                                                            Status</th>
                                                        <th>Candidate Name
                                                        </th>
                                                        <th style="display : none">Username
                                                        </th>
                                                        <th style="display : none">Mobile No.
                                                        </th>
                                                        <th>Application Category
                                                        </th>
                                                        <th>Application No./Registration No.
                                                        </th>
                                                        <th>Payment Status
                                                        </th>
                                                        <th>Mark Entry
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
                                        <tr class="item">
                                            <td runat="server">
                                                <asp:CheckBox ID="chkIsVerify" runat="server" TabIndex="1" Checked='<%# Eval("ISLOCK").ToString() == "False" ? false : true %>' />
                                                <asp:Label ID="lbllock" runat="server" Text='<%# Eval("IS_LOCK")%>' Enabled="false" TabIndex="1"></asp:Label>
                                            </td>

                                            <td>
                                                <asp:HiddenField ID="hfdScoreId" runat="server" Value='<%# Eval("SCOREID") %>' />
                                                <asp:Label ID="lblCandiateName" runat="server" Text='<%#Eval("CANDIDATE NAME").ToString().Replace("-", "<br />") %>' Enabled="false" TabIndex="1"></asp:Label>
                                              
                                            </td>
                                            <td style="display : none">
                                                <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("USERNAME") %>' Enabled="false" TabIndex="1"></asp:Label>
                                                
                                            </td>
                                            <td style="display : none">
                                                <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("MOBILENO") %>' Enabled="false" TabIndex="1"></asp:Label>
                                            </td>
                                            <asp:HiddenField ID="hfdUserNo" runat="server" Value='<%# Eval("USERNO") %>' />
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("APPLICATION_CATEGORY") %>' Enabled="false" TabIndex="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblRegNo" runat="server" Text='<%# Eval("REGNO") %>' Enabled="false" TabIndex="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("PAYMENT_STATUS") %>' Enabled="false" TabIndex="1"></asp:Label>
                                            </td>

                                            <td>
                                                <asp:UpdatePanel ID="updtestdetails" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnmarkentry" runat="server" Text="Mark Entry" OnClick="btnmarkentry_Click"
                                                            CommandArgument='<%# Eval("SCOREID") %>' ToolTip='<%# Eval("USERNO") %>' CssClass="btn btn-outline-info" TabIndex="1" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnmarkentry" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                                 <div style="text-align: left; margin-top: 0px;">
                                            <asp:DataPager ID="DataPager2" runat="server" PagedControlID="lvTestScore" PageSize="500">
                                                <Fields>
                                                    <asp:TemplatePagerField>
                                                        <PagerTemplate>
                                                            <b>showing
                                                                <asp:Label runat="server" ID="CurrentPageLabel"
                                                                    Text="<%# Container.StartRowIndex+1 %>" />
                                                                to
                                                                <asp:Label runat="server" ID="TotalPagesLabel"
                                                                    Text="<%# Convert.ToInt32(Container.StartRowIndex+ Container.PageSize) > Convert.ToInt32(Container.TotalRowCount) ? Convert.ToInt32(Container.TotalRowCount):Convert.ToInt32(Container.StartRowIndex+ Container.PageSize) %>"/>
                                                                ( of
                                                                <asp:Label runat="server" ID="TotalItemsLabel"
                                                                    Text="<%# Container.TotalRowCount%>" />
                                                                records)
                                                                <br />
                                                            </b>
                                                        </PagerTemplate>
                                                    </asp:TemplatePagerField>
                                                </Fields>
                                            </asp:DataPager> 
                                        </div>
                                        <div style="text-align: right; margin-top: 0px;">
                                            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvTestScore" PageSize="500">
                                                <Fields>
                                                    <asp:NumericPagerField />
                                                </Fields>
                                            </asp:DataPager>
                                        </div>
                            </asp:Panel>

                        </div>

                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnLock" />
        </Triggers>
    </asp:UpdatePanel>


    <div class="modal" id="testdetails" data-backdrop="false" style="background-color: rgba(0,0,0,0.6);">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <asp:UpdatePanel ID="updBindModel" runat="server">
                    <ContentTemplate>
                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">Mark Entry</h4>
                            <button type="button" id="btntestclose" class="close" data-dismiss="modal">&times;</button>
                        </div>

                        <!-- Modal body -->
                        <div class="modal-body pl-0 pr-0 pl-lg-2 pr-lg-2" style="overflow: scroll; height: 450px;">

                            <div class="col-12">
                                <div class="row">

                                    <div class="col-lg-6 col-md-6 col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <asp:Label ID="Label" runat="server" Text="Candidate Name :" Font-Bold="true"></asp:Label>
                                                <asp:Label ID="lblCandidateName" runat="server" Font-Bold="true"></asp:Label>
                                                <asp:HiddenField ID="hdfuserno" runat="server" />
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <asp:Label ID="Label4" runat="server" Text="Test :" Font-Bold="true"></asp:Label>
                                                <asp:Label ID="lbltest" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <asp:Label ID="Label2" runat="server" Text="Application No./Registration No. :" Font-Bold="true"></asp:Label>
                                                <asp:Label ID="lblregno" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <asp:Label ID="lblVerifiedBy" runat="server" Text="Last Verified By / On :" Font-Bold="true"></asp:Label>
                                                <asp:Label ID="lblVerifyBy" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <asp:Label ID="lblDate" runat="server" Text="Date Of Birth :" Font-Bold="true"></asp:Label>
                                                <asp:Label ID="lbldob" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Max Score</label>
                                                </div>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtMaxScore" runat="server" Enabled="false" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtObtainedMarks" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Total Score</label>
                                                </div>
                                                <asp:TextBox ID="txtObtainedMarks" runat="server" Enabled="true"
                                                    TabIndex="1" onkeypress="return (event.charCode >= 48 && event.charCode <= 57)||event.charCode == 46"
                                                    onchange="ValidatedScoreMarks()" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtObtainedMarks" runat="server" ControlToValidate="txtObtainedMarks" ValidationGroup="submittest"
                                                    ErrorMessage="Please Enter Total Score." Display="None">
                                                </asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator ID="rgx" ControlToValidate="txtObtainedMarks" runat="server" ValidationGroup="submittest"
                                                    ErrorMessage="After decimal only 7 digits allow" Display="None" ValidationExpression="^[0-9]{0,6}(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>--%>
                                                <%--  onchange="isValidMaxMarks(this)" ValidatedOQMarks();   onkeypress="return (event.charCode >= 48 && event.charCode <= 57)||event.charCode == 46"--%>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>CRL / All India Rank</label>
                                                </div>
                                                <asp:TextBox ID="txtAllIndiaRank" runat="server" Enabled="true" onkeypress="return (event.charCode >= 48 && event.charCode <= 57)"
                                                    MaxLength="8" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtAllIndiaRank" runat="server" ControlToValidate="txtAllIndiaRank" ValidationGroup="submittest"
                                                    ErrorMessage="Please Select CRL / All India Rank." Display="None">
                                                </asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Category</label>
                                                </div>
                                                <asp:DropDownList ID="ddlcategory" runat="server" AppendDataBoundItems="True" CssClass="form-control"
                                                    data-select2-enable="true" TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlcategory" runat="server" ControlToValidate="ddlcategory"
                                                    ValidationGroup="submittest" Display="None" InitialValue="0" ErrorMessage="Please Select Category.">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <%--<sup>* </sup>--%>
                                                    <label>Choose File</label>
                                                </div>

                                                <asp:FileUpload ID="futestdoc" runat="server" TabIndex="1" />

                                                <asp:Label ID="lbldocument" runat="server" ToolTip="Document Status" Visible="false" TabIndex="1"></asp:Label>
                                                <asp:HiddenField ID="hdforiginalname" runat="server" />

                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12 mt-3">
                                                <asp:UpdatePanel ID="Updpreview" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnPrevDoc" runat="server" OnClick="btnPrevDoc_Click"
                                                            Text="Preview" CssClass="btn btn-outline-info"
                                                            TabIndex="1" Visible="false" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnPrevDoc" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-lg-6 col-md-6 col-12">

                                        <asp:Panel ID="pnlPopup" runat="server" Visible="false">
                                            <div class="header">
                                                Document
                                            </div>
                                            <div class="body">
                                                <iframe runat="server" width="500px" height="450px" id="iframeView"></iframe>
                                            </div>

                                        </asp:Panel>

                                    </div>
                                </div>
                            </div>
                        </div>


                        <!-- Modal footer -->

                        <div class="modal-footer">
                            <asp:Button ID="btnUpload" runat="server" Text="Update and verify" ToolTip="Click to Upload" OnClick="btnUpload_Click"
                                CssClass="btn btn-outline-info" TabIndex="1" ValidationGroup="submittest" />
                            <asp:Button ID="btnclear" runat="server" class="btn btn-warning" Text="Cancel" OnClick="btnclear_Click" />
                            <button type="button" id="btnclose" class="btn btn-danger" data-dismiss="modal">Close</button>
                            <asp:ValidationSummary ID="vsshow" runat="server" ValidationGroup="submittest" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" />
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnclear" />
                        <asp:PostBackTrigger ControlID="btnUpload" />

                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    <script>
        $(document).ready(function () {
            $("[id*=btnclear]").addClass("btn btn-warning");
        })

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $("[id*=btnclear]").addClass("btn btn-warning");
        });

        function isValidMaxMarks(txtObtainedMarks) {
            var decimalRegex = /^\d{0,6}(\.\d{0,7})?$/;
            var obtained_score = parseInt(txtObtainedMarks.value);
            var Max_Score = parseInt(txtMaxScore.value);
            if (!decimalRegex.test(txtObtainedMarks.value)) {
                txtObtainedMarks.value = '';
                txtObtainedMarks.focus();
                alert("Please enter a valid decimal number with 7 digits after the decimal point.");
                return false;
            }
            else if (obtained_score > Max_Score) {
                txtObtainedMarks.value = '';
                txtObtainedMarks.focus();
                alert("Entered Score Obtained must be less than or equal to Max Score.");
                return false;
            }

            return true;
        };

        function ValidatedScoreMarks() {
            var maxscore = parseInt($('#<%=txtMaxScore.ClientID %>').val());
            var totalsocre = parseInt($('#<%=txtObtainedMarks.ClientID %>').val());
            if (maxscore > 0) {
                if (totalsocre > maxscore) {
                    $('#<%=txtObtainedMarks.ClientID %>').val("");
                    alert("Total Score should not be greater than Max Score");
                    return false;
                }
            }
            return true;
        }



        function checkAll(source) {
            var checkboxes = document.querySelectorAll('tbody input[type="checkbox"]');
            for (var i = 0; i < checkboxes.length; i++) {
                if (!checkboxes[i].disabled) {
                    checkboxes[i].checked = source.checked;
                }
            }
        }


    </script>

    <script type="text/javascript">
        //$('#ctl00_ContentPlaceHolder1_pnlPopup').show()
        //jQuery('#clzbtn').on('click', function () {
        //    jQuery('#ctl00_ContentPlaceHolder1_pnlPopup').toggle();
        //    $("[id*=divMarksheetList]").hide();

        //})
        //var prm = Sys.WebForms.PageRequestManager.getInstance();
        //prm.add_endRequest(function () {
        //    jQuery('#clzbtn').on('click', function () {
        //        //$("[id*=btnSubmitDocVerify]").show();
        //        jQuery('#ctl00_ContentPlaceHolder1_pnlPopup').toggle();
        //        $("[id*=divDocumentList]").show();
        //        $("[id*=divMarksheetList]").hide();
        //        $("[id*=btnSubmitDocVerify]").removeClass("d-none");
        //        $("[id*=btnSubmitDocVerify]").addClass("btn btn-primary");
        //    })
        //});

        jQuery('#btntestclose').on('click', function () {
            $("[id*=pnlPopup]").hide();
        })

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            jQuery('#btntestclose').on('click', function () {
                $("[id*=pnlPopup]").hide();
            })
        });



        function OpenPreview() {
            $('#testdetails').modal('show');
        }

        $(".scroll").click(function (event) {
            $('html,body').animate({ scrollTop: $(this.hash).offset().top }, 500);
        });

    </script>
    <script>
        function toggleSearch(searchBar, table) {
            var tableBody = table.querySelector('tbody');
            var allRows = tableBody.querySelectorAll('tr');
            var val = searchBar.value.toLowerCase();
            allRows.forEach((row, index) => {
                var insideSearch = row.innerHTML.trim().toLowerCase();
            //console.log('data',insideSearch.includes(val),'searchhere',insideSearch);
            if (insideSearch.includes(val)) {
                row.style.display = 'table-row';
            }
            else {
                row.style.display = 'none';
            }

        });
        }

        function test5() {
            var searchBar5 = document.querySelector('#FilterData');
            var table5 = document.querySelector('#tblData1');
            //console.log(allRows);
            //searchBar5.addEventListener('focusout', () => {
            searchBar5.addEventListener('keyup', () => {
                toggleSearch(searchBar5, table5);
        });

        $(".saveAsExcel").click(function () {
            let UserCall = prompt("Please Enter Password:");
            var ExcelDetails = '<%=Session["ExcelDetails"] %>';
            if (UserCall == null || UserCall == "") {
                return false;
            } else {
                if(UserCall == ExcelDetails)
                {

                }
                else {
                    alert('Password is not matched !!!')
                    return false;
                }
            }
            //if (confirm('Do You Want To Apply for New Program?') == true) {
            //    return false;
            //}
            var workbook = XLSX.utils.book_new();
            var allDataArray = [];
            allDataArray = makeTableArray(table5, allDataArray);
            var worksheet = XLSX.utils.aoa_to_sheet(allDataArray);
            workbook.SheetNames.push("Test");
            workbook.Sheets["Test"] = worksheet;
            XLSX.writeFile(workbook, "LeadData.xlsx");
        });
        }

        function makeTableArray(table, array) {
            var allTableRows = table.querySelectorAll('tr');
            allTableRows.forEach(row => {
                var rowArray = [];
            if (row.querySelector('td')) {
                var allTds = row.querySelectorAll('td');
                allTds.forEach(td => {
                    if (td.querySelector('span')) {
                        rowArray.push(td.querySelector('span').textContent);
            }
            else if (td.querySelector('input')) {
                rowArray.push(td.querySelector('input').value);
            }
            else if (td.querySelector('select')) {
                rowArray.push(td.querySelector('select').value);
            }
            else if (td.innerText) {
                rowArray.push(td.innerText);
            }
            else{
                rowArray.push('');
            }
        });
        }
        if (row.querySelector('th')) {
            var allThs = row.querySelectorAll('th');
            allThs.forEach(th => {
                if (th.textContent) {
                    rowArray.push(th.textContent);
        }
        else {
            rowArray.push('');
        }
        });
        }
        // console.log(allTds);

        array.push(rowArray);
        });
        return array;
        }

    </script>
</asp:Content>
