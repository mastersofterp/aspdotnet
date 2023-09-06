<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TransportManagement.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_TransportManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <script src="../../jquery/jquery-3.2.1.min.js"></script>
    <link href="../../jquery/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../jquery/bootstrap-multiselect.js"></script>--%>


    <%--Added By Abhinay Lad [24-02_2020]--%>
    <script>
        //var MulSel = $.noConflict();
        $(document).ready(function () {
            $('.multi-select-demo').multiselect();
            $('.multiselect').css("width", "100%");
            $(".multiselect-container").css("width", "100%");
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.multi-select-demo').multiselect({
                    allSelectedText: 'All',
                    maxHeight: 200,
                    maxWidth: '100%',
                    includeSelectAllOption: true
                });
                $('.multiselect').css("width", "100%");
                $(".multiselect-container").css("width", "100%")
            });
        });
    </script>
    <%--Ended By Abhinay Lad [24-02_2020]--%>

    <script type="text/javascript">
        $(document).ready(function () {

            //--*========Added by akhilesh on [2019-05-11]==========*--          
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(InitAutoCompl)

            //--*========Added by akhilesh on [2019-05-11]==========*--          
            // if you use jQuery, you can load them when dom is read.          
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            // Place here the first init of the autocomplete
            InitAutoCompl();

            function InitializeRequest(sender, args) {
            }

            function EndRequest(sender, args) {

            }
        });

    </script>

    <%--$('#<%= hdnReceiptno.ClientID%>').val(ReceiptNo);--%>
    <style>
        div.dd_chk_select {
            height: 35px;
            font-size: 14px !important;
            padding-left: 12px !important;
            line-height: 2.2 !important;
            width: 100%;
        }

            div.dd_chk_select div#caption {
                height: 35px;
            }
    </style>

    <style type="text/css">
        #load {
            width: 100%;
            height: 100%;
            position: fixed;
            z-index: 9999;
            /*background: url("/images/loading_icon.gif") no-repeat center center rgba(0,0,0,0.25);*/
        }
    </style>
    <script type="text/javascript">
        document.onreadystatechange = function () {
            var state = document.readyState
            if (state == 'interactive') {
                document.getElementById('contents').style.visibility = "hidden";
            } else if (state == 'complete') {
                setTimeout(function () {
                    document.getElementById('interactive');
                    document.getElementById('load').style.visibility = "hidden";
                    document.getElementById('contents').style.visibility = "visible";
                }, 1000);
            }
        }
    </script>
    <script>
        function AllDetails() {
            getVal();
            getYear();
            getRouteName();
            getBroadingPoint();
        }
        function getVal() {
            var array = []
            var degreeNo;

            var checkboxes = document.querySelectorAll('#ddlBranch input[type=checkbox]:checked')

            for (var i = 0; i < checkboxes.length; i++) {
                //array.push(checkboxes[i].value)   
                if (checkboxes[i].value != 'multiselect-all') {
                    if (degreeNo == undefined) {
                        degreeNo = checkboxes[i].value + '$';
                    }
                    else {
                        degreeNo += checkboxes[i].value + '$';
                    }
                }
            }

            $('#<%= hdnBranch.ClientID %>').val(degreeNo);
            //alert(degreeNo);
            //document.getElementById(inpHide).value = "degreeNo";
        }
        function getYear() {
            var YearArray = []
            var Year;

            var checkboxes = document.querySelectorAll("#Year input[type=checkbox]:checked")

            for (var i = 0; i < checkboxes.length; i++) {
                //array.push(checkboxes[i].value)  
                if (checkboxes[i].value != 'multiselect-all') {
                    if (Year == undefined) {
                        Year = checkboxes[i].value + '$';
                    }
                    else {
                        Year += checkboxes[i].value + '$';
                    }
                }
            }

            $('#<%= hdnYear.ClientID %>').val(Year);
            //alert(Year);
        }
        function getRouteName() {
            var RouteNamearray = []
            var RouteName;

            var checkboxes = document.querySelectorAll("#Name input[type=checkbox]:checked")

            for (var i = 0; i < checkboxes.length; i++) {
                //array.push(checkboxes[i].value) 
                if (checkboxes[i].value != 'multiselect-all') {
                    if (RouteName == undefined) {
                        RouteName = checkboxes[i].value + '$';
                    }
                    else {
                        RouteName += checkboxes[i].value + '$';
                    }
                }
            }

            $('#<%= hdnRoute.ClientID %>').val(RouteName);
            //alert(RouteName);
        }
        function getBroadingPoint() {
            var BroadingPointarray = []
            var BroadingPoint;

            var checkboxes = document.querySelectorAll("#Point input[type=checkbox]:checked")

            for (var i = 0; i < checkboxes.length; i++) {
                //array.push(checkboxes[i].value)    
                if (checkboxes[i].value != 'multiselect-all') {
                    if (BroadingPoint == undefined) {
                        BroadingPoint = checkboxes[i].value + '$';
                    }
                    else {
                        BroadingPoint += checkboxes[i].value + '$';
                    }
                }
            }

            $('#<%= hdnBroading.ClientID %>').val(BroadingPoint);
            //alert(BroadingPoint);
        }

    </script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPanel"
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
    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TRANSPORT REPORT</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Route Details</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="ddlBranch">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:ListBox ID="ddlBranchMultiCheck" runat="server" AppendDataBoundItems="true" TabIndex="3" AutoPostBack="true"
                                            CssClass="form-control multi-select-demo" SelectionMode="multiple" style="height:150px!important;"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBranchMultiCheck"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="Summary"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Year">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Year</label>
                                        </div>
                                        <asp:ListBox ID="ddlYear" runat="server" AppendDataBoundItems="true" TabIndex="3"
                                            CssClass="form-control multi-select-demo" SelectionMode="multiple"  style="height:150px!important;"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlYear"
                                            Display="None" ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="Summary"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Name">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Route Name</label>
                                        </div>
                                        <asp:ListBox ID="ddlrouteName" runat="server" AppendDataBoundItems="true" TabIndex="3" CssClass="form-control multi-select-demo"
                                            SelectionMode="multiple" OnSelectedIndexChanged="ddlrouteName_SelectedIndexChanged" AutoPostBack="true"  style="height:150px!important;"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlrouteName"
                                            Display="None" ErrorMessage="Please Select Route" InitialValue="0" ValidationGroup="A"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Point">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Boarding Point</label>
                                        </div>
                                        <asp:ListBox ID="ddlboardingPoint" runat="server" AppendDataBoundItems="true" TabIndex="3"
                                            CssClass="form-control multi-select-demo" SelectionMode="multiple"  style="height:150px!important;"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlboardingPoint"
                                            Display="None" ErrorMessage="Please Select Boarding Point" InitialValue="0" ValidationGroup="A"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>

                                </div>
                            </div>


                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" CssClass="btn btn-info" ValidationGroup="A"
                                    OnClientClick="return AllDetails();" CausesValidation="true" />

                                <asp:HiddenField ID="hdnBranch" runat="server" />
                                <asp:HiddenField ID="hdnYear" runat="server" />
                                <asp:HiddenField ID="hdnRoute" runat="server" />
                                <asp:HiddenField ID="hdnBroading" runat="server" />
                                <asp:Button ID="btnTR_Report" runat="server" Text="TransportReport" OnClick="btnTR_Report_Click" CssClass="btn btn-info" />
                                <asp:Button ID="btnTr_show" runat="server" Text="Show Passenger Count" OnClick="btnTr_show_Click" CssClass="btn btn-info" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Summary" />
                                   <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-warning" />
                             
                                
                            </div>
                            <%-- List view added on 13-jan-2020 by Andoju Vijay --%>
                            <div id="Trans_List" class="col-12 mt-3">
                                <asp:ListView ID="lvTransport" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Route No.</th>
                                                        <th>Route Name</th>
                                                        <th>No.of Students</th>
                                                        <th>No.of Employees</th>
                                                        <th>Total Passengers</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>

                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <div align="left" class="info">
                                            There are no records to display
                                        </div>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("ROUTE_NUMBER") %></td>
                                            <td><%# Eval("ROUTENAME") %></td>
                                            <td><%--<%# Eval("NO_OF_STUDENTS") %>--%>
                                                <asp:LinkButton Text='<%# Eval("NO_OF_STUDENTS") %>' ID="lnk_No_of_Students" CommandArgument='<%# Eval("ROUTEID") %>'
                                                    OnCommand="lnk_No_of_Students_Command" runat="server" ToolTip="Please Select No.of Student to see Report" />
                                            </td>
                                            <td>
                                                <asp:LinkButton Text='<%# Eval("NO_OF_EMPLOYEE") %>' ID="lnk_No_of_Employee" CommandArgument='<%# Eval("ROUTEID") %>'
                                                    OnCommand="lnk_No_of_Employee_Command" runat="server" ToolTip="Please Select No.of Employee to see Report" />
                                            </td>
                                            <td><%# Eval("TOTAL_PASSENGERS") %></td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <%-- <asp:PostBackTrigger ControlID="btnReport"/>
           <asp:PostBackTrigger ControlID="ddlBranchMultiCheck" />
             <asp:PostBackTrigger ControlID="ddlYear" />--%>
            <asp:AsyncPostBackTrigger ControlID="ddlrouteName" />
            <asp:PostBackTrigger ControlID="ddlboardingPoint" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
