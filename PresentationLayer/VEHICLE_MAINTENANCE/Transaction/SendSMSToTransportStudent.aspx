<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SendSMSToTransportStudent.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_SendSMSToTransportStudent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--    <script src="../Content/jquery.js" type="text/javascript"></script>
    <script src="../Content/jquery.dataTables.js" type="text/javascript"></script>
        <script src="../../jquery/jquery-3.2.1.min.js"></script>
    <link href="../../jquery/jquery.multiselect.css" rel="stylesheet" />
    <script src="../../jquery/jquery.multiselect.js"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {


            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(InitAutoCompl)


            // if you use jQuery, you can load them when dom is read.          
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            // Place here the first init of the autocomplete
            InitAutoCompl();

            function InitializeRequest(sender, args) {
            }

            function EndRequest(sender, args) {
                // after update occur on UpdatePanel re-init the Autocomplete
                InitAutoCompl();
            }
            function InitAutoCompl() {
                $('select[multiple]').val('').multiselect({
                    columns: 3,     // how many columns should be use to show options            
                    search: true, // include option search box
                    searchOptions: {
                        delay: 250,                         // time (in ms) between keystrokes until search happens
                        clearSelection: true,
                        showOptGroups: false,                // show option group titles if no options remaining

                        searchText: true,                 // search within the text

                        searchValue: true,                // search within the value

                        onSearch: function (element) { } // fires on keyup before search on options happens
                    },

                    // plugin texts

                    texts: {

                        placeholder: 'Please Select ', // text to use in dummy input

                        search: 'Search',         // search input placeholder text

                        selectedOptions: ' selected',      // selected suffix text

                        selectAll: 'Select all',     // select all text

                        unselectAll: 'Unselect all',   // unselect all text

                        //noneSelected: 'None Selected'   // None selected text

                    },

                    // general options

                    selectAll: true, // add select all option

                    selectGroup: false, // select entire optgroup

                    minHeight: 200,   // minimum height of option overlay

                    maxHeight: null,  // maximum height of option overlay

                    maxWidth: null,  // maximum width of option overlay (or selector)

                    maxPlaceholderWidth: null, // maximum width of placeholder button

                    maxPlaceholderOpts: 10, // maximum number of placeholder options to show until "# selected" shown instead

                    showCheckbox: true,  // display the checkbox to the user

                    optionAttributes: [],

                });
            }


            //Get checked checkbox value
            //$('#ctl00_ContentPlaceHolder1_btnSave').click(function () {
            //    alert('hi');
            //    debugger;
            //    var selected = $("#ctl00_ContentPlaceHolder1_ddlDegreeMultiCheck option:selected");
            //    var message = "";
            //    selected.each(function () {
            //        message += $(this).text() + " " + $(this).val() + "\n";
            //    });
            //    alert(message);
            //});

        });

        function AllDetails() {
            debugger;
            getRouteName();


        }
        function getRouteName() {
            debugger;
            var RouteNamearray = []
            var RouteName;

            var checkboxes = document.querySelectorAll("#Name input[type=checkbox]:checked")

            for (var i = 0; i < checkboxes.length; i++) {
                //array.push(checkboxes[i].value)    
                if (RouteName == undefined) {
                    RouteName = checkboxes[i].value;
                }
                else {
                    RouteName += ',' + checkboxes[i].value;
                }
            }

            $('#<%= hdnRoute.ClientID %>').val(RouteName);

        }
    </script>

    <div>
      <%--  <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updCollege"
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
        </asp:UpdateProgress>--%>
    </div>
    <%--<asp:UpdatePanel ID="updCollege" runat="server">
        <ContentTemplate>--%>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Send SMS To Transport Student</h3>
                        </div>
                        <div class="box-body">
                            
                                <asp:Label ID="lblHelp" runat="server" Font-Bold="True" Style="color: Red"></asp:Label>
                                <asp:Panel ID="Studpanel" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Degree</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <%--<asp:ListItem></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Branch</label>
                                                </div>
                                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="2" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <%--  <asp:ListItem></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Semester</label>
                                                </div>
                                                <asp:DropDownList ID="ddlsemester" runat="server" AppendDataBoundItems="true" TabIndex="3" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <%--  <asp:ListItem></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Name">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Route</label>
                                                </div>
                                                <asp:DropDownList ID="ddlRoute" runat="server" AppendDataBoundItems="false" TabIndex="4" name="Route" multiple="multiple" CssClass="form-control" data-select2-enable="true">

                                                    <%-- <asp:ListItem></asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdnRoute" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShowStudent" runat="server" class="btn btn-primary" TabIndex="12" Text="Show Student" ValidationGroup="ShowStudent" ToolTip="Show Student" OnClick="btnShowStudent_Click" OnClientClick="return AllDetails();" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="ShowStudent" />

                                    </div>

                                </asp:Panel>
                            </div>
                            <div class="col-12" id="trStudent" runat="server">
                                <asp:Panel ID="pnlstud" runat="server" Visible="false" ScrollBars="Vertical" Height="400px">
                                    <asp:ListView ID="lvStudent" runat="server">
                                        <EmptyDataTemplate>
                                            <br />
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Student To Shows" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div id="demp_grid" class="vista-grid">
                                                <div class="titlebar">
                                                    <div class="sub-heading">
                                                        <h5>Student List</h5>
                                                    </div>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>
                                                                <asp:CheckBox ID="chkSelect1" runat="server" onclick="totAllSubjects(this)" />Select
                                                            </th>
                                                            <th>Univ. Reg. No.</th>
                                                            <th>Admission No.</th>
                                                            <th>Student Name</th>
                                                            <th>Phone No.</th>
                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td>
                                                    <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRegno" runat="server" Text=' <%# Eval("REGNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblEnrollno" runat="server" Text=' <%# Eval("ENROLLNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStudname" runat="server" Text=' <%# Eval("STUDNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStudmobile" runat="server" Text=' <%# Eval("STUDENTMOBILE")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12" id="divMsgSMS" runat="server" visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Message</label>
                                        </div>
                                        <asp:TextBox ID="txtMessage" CssClass="form-control" runat="server" TextMode="MultiLine" ToolTip="Please Enter  message" Height="90px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMessage"
                                            ErrorMessage="Please Enter Message !" Display="None" SetFocusOnError="true" ValidationGroup="SendEmail"></asp:RequiredFieldValidator>

                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSndSms" runat="server" TabIndex="12" CssClass="btn btn-primary" Text="Send SMS" ValidationGroup="SendEmail" ToolTip="Send SMS" OnClick="btnSndSms_Click" />
                                    <asp:Button ID="btnCancel" runat="server" TabIndex="12" CssClass="btn btn-warning" Text="Cancel" ToolTip="Clear All" OnClientClick="validcan()" OnClick="btnCancel_Click" />

                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="SendEmail" />

                                </div>
                            </div>

                        </div>
                    </div>
                </div>
           

<%--        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSndSms" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>--%>


    <script>
        function totAllSubjects(headchk) {
            debugger;
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
        }
    </script>
    <div id="divMsg" runat="server">
    </div>

</asp:Content>

