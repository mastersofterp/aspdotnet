<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Direct_Gradee_System_External.aspx.cs" Inherits="ACADEMIC_EXAMINATION_Direct_Gradee_System" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .multiselect-container {
            position: absolute;
            transform: translate3d(0px, -46px, 0px);
            top: 0px;
            left: 0px;
            will-change: transform;
            height: 200px;
            overflow: auto;
        }
    </style>

    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <script type="text/javascript">
        //var MulSel = $.noConflict();
        $(document).ready(function () {
            debugger
            $('.multi-select-demo').multiselect();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                    enableHTML: true,
                    templates: {
                        filter: '<li class="multiselect-item multiselect-filter"><div class="input-group mb-3"><div class="input-group-prepend"><span class="input-group-text"><i class="fa fa-search"></i></span></div><input class="form-control multiselect-search" type="text" /></div></li>',
                        filterClearBtn: '<span class="input-group-btn"><button class="btn btn-default multiselect-clear-filter" style="height: 33px;" type="button"><i style="margin-right: 4px;" class="fa fa-eraser"></i></button></span>'
                    }
                    //dropRight: true,
                    //search: true,
                });

            });
        });
    </script>
 
   
     <div>
         
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updDirectGrade"
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
        <asp:UpdatePanel ID="updDirectGrade" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div id="div1" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">Direct Grading System</h3>
                            </div>

                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-4 col-md-12 col-12">
                                            <div class="row">
                                               
                                                <div class="form-group col-lg-12 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>College Scheme</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollegeScheme" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" Visible="false" TabIndex="1">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:ListBox ID="lstcollege" runat="server" AppendDataBoundItems="true" Width="100px" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="lstcollege_SelectedIndexChanged" CssClass="multi-select-demo"
                                                    TabIndex="1"></asp:ListBox>
                                                   <%-- <asp:RequiredFieldValidator ID="lstcollege1" runat="server" ControlToValidate="lstcollege"
                                                        Display="None" ErrorMessage="Please Select College/Scheme." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>

                                                </div>
                                                <div class="form-group col-lg-12 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Level</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlLevel" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">CGPA/AGPA</asp:ListItem>
                                                        <asp:ListItem Value="2">Marks Range</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="ddllevel1" runat="server" ControlToValidate="ddlLevel"
                                                        Display="None" ErrorMessage="Please Select College/Scheme." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:ListView ID="lvGrade" runat="server" Visible="false">


                                            <LayoutTemplate>
                                                <div class="col-12 col-lg-6 offset-lg-2">
                                                    <div class="sub-heading">
                                                        <h5>Component Level </h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                        <thead>
                                                            <tr>
                                                                <th>Grade </th>
                                                                <th>Grade Point</th>
                                                                <th>Minimum Range</th>
                                                                <th>Maximum Range</th>
                                                                <th>Active</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tbody>
                                                    <tr>


                                                        <asp:HiddenField ID="hfdValue" runat="server" Value='<%#Eval("GRADENO")%>' />
                                                        <td><%#Eval("GRADE")%></td>


                                                        <td>
                                                            <asp:TextBox CssClass="form-control" ID="txtGraadePoint" Text='<%# Eval("GRADEPOINT")%>' runat="server"> </asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtGraadePoint"
                                                                ValidChars="1234567890.!@#$%^*()_+=,./:;<>?'{}[]\|-&&" FilterMode="ValidChars" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control" ID="txtRangeMin" Text='<%# Eval("MINIRANGE")%>' runat="server">  </asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtRangeMin"
                                                                ValidChars="1234567890.!@#$%^*()_+=,./:;<>?'{}[]\|-&&" FilterMode="ValidChars" />

                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control" ID="txtRangeMax" Text='<%# Eval("MAXIRANGE")%>' runat="server"> </asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtRangeMax"
                                                                ValidChars="1234567890.!@#$%^*()_+=,./:;<>?'{}[]\|-&&" FilterMode="ValidChars" />
                                                        </td>
                                                        <td>
                                                            <%-- <asp:TextBox CssClass="form-control" ID="txtActive" Text='<%# Eval("ACTIVESTATUS")%>' runat="server" onkeypress="return isNumber(event)"> </asp:TextBox></td>--%>
                                                            <asp:CheckBox ID="chkStatus" runat="server" Checked='<%# Eval("ACTIVESTATUS").ToString()=="1"?true:false %>' />
                                                    </tr>

                                                </tbody>
                                            </ItemTemplate>
                                        </asp:ListView>


                                        <asp:ListView ID="lvCGPA" runat="server" Visible="false">
                                            <LayoutTemplate>
                                                <div class="col-12 col-lg-6 offset-lg-2">
                                                    <div class="sub-heading">
                                                        <h5>Course / GPA Level </h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                        <thead>
                                                            <tr>
                                                                <th>Grade </th>
                                                                <th>Minimum Range</th>
                                                                <th>Maximum Range</th>
                                                                <th>Indicator</th>
                                                                <th>Active</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tbody>
                                                    <tr>
                                                        <%-- <asp:HiddenField ID="hfdID1" runat="server" Value='<%#Eval("ID")%>'/>--%>
                                                        <asp:HiddenField ID="hfdValue1" runat="server" Value='<%#Eval("GRADENO")%>' />
                                                        <td><%#Eval("GRADE")%>
                                                 
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control" ID="txtMinRange" Text='<%# Eval("MINIRANGE")%>' runat="server"> </asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtMinRange"
                                                                ValidChars="1234567890.!@#$%^*()_+=,./:;<>?'{}[]\|-&&" FilterMode="ValidChars" />
                                                            <%-- <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtMinRange"
                                                        InvalidChars="" FilterMode="InvalidChars" />--%>
                                                        </td>

                                                        <td>
                                                            <asp:TextBox CssClass="form-control" ID="txtMaxRange" Text='<%# Eval("MAXIRANGE")%>' runat="server"> </asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMaxRange"
                                                                ValidChars="1234567890.!@#$%^*()_+=,./:;<>?'{}[]\|-&&" FilterMode="ValidChars" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control" ID="txtIndicator" Text='<%# Eval("INDICATOR")%>' runat="server" onkeypress="return AllowAlphabet(event)"> </asp:TextBox></td>
                                                        <td>
                                                            <asp:CheckBox ID="chkStatus1" runat="server" Checked='<%# Eval("ACTIVESTATUS").ToString()=="1"?true:false %>' /></td>
                                                        <%--  <asp:TextBox CssClass="form-control" ID="txtActive" Text='<%# Eval("ACTIVESTATUS")%>' runat="server" onkeypress="return isNumber(event)"> </asp:TextBox></td>--%>
                                                        <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtIndicator"
                                                 FilterMode="ValidChars" />--%>
                                                    </tr>
                                                </tbody>
                                            </ItemTemplate>
                                        </asp:ListView>

                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    
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
    <script type="text/javascript">
        function AllowAlphabet(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45')
                return true;
            else {
                //alert('Please Enter Only Character values.');
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function verify() {
            debugger
            var n = getelementbyID().val();
            if (n >= $("#txtMinRange").val()) {

            }
            else {
                alert("Min Mark Not Greater Than Max Mark");
            }
        }

    </script>

    <script>
        function UpdateTotalAndBalance() {
            try {
                // var totalFeeAmt = 0.00;
                // var dataRows = null;
                debugger
                list = 'lvGrade';
                var dataRows = document.getElementsByTagName('tr');
                for (i = 0; i < dataRows.length - 1; i++) {
                    testmark = document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_' + 'ctrl' + i + '_' + 'txtRangeMax').value;
                    interview = document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_' + 'ctrl' + i + '_' + 'txtRangeMin').value;
                    if (testmark >= interview) {

                    }
                    else {
                        alert("Min Mark Not Greater Than Max Mark");
                    }
                    // FinalAmount = parseFloat(interview) + parseFloat(testmark);
                    //document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_' + 'ctrl' + i + '_' + 'TxtTotal').value = FinalAmount;

                }
            }



            catch (e) {
            }
        }
    </script>
    <script>
        function UpdateTotalAndBalance1() {
            try {
                // var totalFeeAmt = 0.00;
                // var dataRows = null;
                debugger
                list = 'lvCGPA';
                var dataRows = document.getElementsByTagName('tr');
                //  var FinalAmount = 0;
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        //  var testmark = 0;
                        //var interview = 0;
                        testmark = document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_' + 'ctrl' + i + '_' + 'txtMaxRange').value;
                        interview = document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_' + 'ctrl' + i + '_' + 'txtMinRange').value;
                        if (testmark >= interview) {

                        }
                        else {
                            alert("Min Mark Not Greater Than Max Mark");
                        }
                        // FinalAmount = parseFloat(interview) + parseFloat(testmark);
                        //document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_' + 'ctrl' + i + '_' + 'TxtTotal').value = FinalAmount;
                    }
                }


            }
            catch (e) {
            }
        }
    </script>
</asp:Content>

