<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DegreeMapping.aspx.cs" Inherits="ACADEMIC_MASTERS_DegreeMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- jQuery library -->
   <%--<link href="../plugins/multi-select/jquery.multiselect.css" rel="stylesheet" />--%>
    <link href="<%#Page.ResolveClientUrl("~/plugins/multi-select/jquery.multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/jquery.multiselect.js")%>"></script>
    <%--<script src="../plugins/multi-select/jquery.multiselect.js"></script>--%>

       <style type="text/css">
        .ms-options-wrap,
        .ms-options-wrap * {
            box-sizing: border-box;
        }

        .ms-options-wrap > button:focus,
        .ms-options-wrap > button {
            position: relative;
            width: 100%;
            text-align: left;
            height: 34px;
            padding: 6px 12px;
            border-bottom: 1px solid #ddd;
            border-top:0;
            border-right:0;
            border-left:0;
            background-color: #fff;
            margin-top: 0px;
            font-size: 13px;
            color: #555;
            outline-offset: 0px;
            outline: none;
            white-space: nowrap;
            border-color: #d2d6de;
        }

        .ms-options-wrap > button:focus {
            border-color: #3c8dbc;
            box-shadow: none;
        }

        .ms-options-wrap > button > span {
            display: inline-block;
        }

        .ms-options-wrap > button[disabled] {
            background-color: #e5e9ed;
            color: #808080;
            opacity: 0.6;
        }

        .ms-options-wrap > button > i {
            position: absolute;
            top: 34%;
            right: 5px;
            font-size: 10px;
        }

        .ms-options-wrap.ms-has-selections > button {
            color: #333;
        }

        .ms-options-wrap > .ms-options {
            position: absolute;
            left: 15px;
            width: 89%;
            margin-top: 1px;
            margin-bottom: 20px;
            background: white;
            z-index: 2000;
            border: 1px solid #aaa;
            overflow: auto;
            visibility: hidden;
        }

        .ms-options-wrap.ms-active > .ms-options {
            visibility: visible
        }

        .ms-options-wrap > .ms-options > .ms-search input {
            width: 100%;
            padding: 4px 5px;
            border: none;
            border-bottom: 1px groove;
            outline: none;
        }

        .ms-options-wrap > .ms-options .ms-selectall {
            display: inline-block;
            font-size: .9em;
            text-transform: lowercase;
            text-decoration: none;
        }
        .ms-options-wrap > .ms-options .ms-selectall:hover {
            text-decoration: underline;
        }

        .ms-options-wrap > .ms-options > .ms-selectall.global {
            margin: 4px 5px;
        }

        .ms-options-wrap > .ms-options > ul,
        .ms-options-wrap > .ms-options > ul > li.optgroup ul {
            list-style-type: none;
            /*display: inline;*/
            padding: 0;
            margin: 0;
        }

        .ms-options-wrap > .ms-options > ul li.ms-hidden {
            display: none;
        }

        .ms-options-wrap > .ms-options > ul > li.optgroup {
            padding: 5px;
        }
        .ms-options-wrap > .ms-options > ul > li.optgroup + li.optgroup {
            border-top: 1px solid #aaa;
        }

        .ms-options-wrap > .ms-options > ul > li.optgroup .label {
            display: block;
            padding: 5px 0 0 0;
            font-weight: bold;
        }

        .ms-options-wrap > .ms-options > ul label {
            position: relative;
            display: inline-block;
            width: 100%;
            padding: 4px 4px 4px 20px;
            margin: 1px 0;
            border: 1px dotted transparent;
        }
        .ms-options-wrap > .ms-options.checkbox-autofit > ul label,
        .ms-options-wrap > .ms-options.hide-checkbox > ul label {
            padding: 4px;
        }

        .ms-options-wrap > .ms-options > ul label.focused,
        .ms-options-wrap > .ms-options > ul label:hover {
            background-color: #efefef;
            border-color: #999;
        }

        .ms-options-wrap > .ms-options > ul li.selected label {
            background-color: #efefef;
            border-color: transparent;
        }

        .ms-options-wrap > .ms-options > ul input[type="checkbox"] {
            margin: 0 5px 0 0;
            position: absolute;
            left: 4px;
            top: 7px;
        }

        .ms-options-wrap > .ms-options.hide-checkbox > ul input[type="checkbox"] {
            position: absolute !important;
            height: 1px;
            width: 1px;
            overflow: hidden;
            clip: rect(1px 1px 1px 1px);
            clip: rect(1px, 1px, 1px, 1px);
        }
    </style>

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
                // after update occur on UpdatePanel re-init the Autocomplete
                InitAutoCompl();
            }
            function InitAutoCompl() {
                $('select[multiple]').val('').multiselect({
                    columns: 4,     // how many columns should be use to show options            
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

                        placeholder: 'Select degree', // text to use in dummy input

                        search: 'Search',         // search input placeholder text

                        selectedOptions: ' selected',      // selected suffix text

                        selectAll: 'Select all degree',     // select all text

                        unselectAll: 'Unselect all Degree',   // unselect all text

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
        });

    </script>
    <script>
        function getVal() {

            var id = (document.getElementById("<%=btnSave.ClientID%>"));
           
            //ShowLoader(id);
            var rfvCollege = (document.getElementById("<%=lblDYCollege.ClientID%>").innerHTML);
          
            var alertmsg = '';

            if (document.getElementById("<%=ddlColg.ClientID%>").value == "0") {
                if (alertmsg == '') {
                    document.getElementById("<%=ddlColg.ClientID%>").focus();
                }
                alertmsg += 'Please Select ' + rfvCollege + ' \n';
            }
          
         
            var array = []
            var degreeNo;
            var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

            for (var i = 0; i < checkboxes.length; i++) {
                //array.push(checkboxes[i].value)    
                if (degreeNo == undefined) {
                    degreeNo = checkboxes[i].value + ',';
                }
                else {
                    degreeNo += checkboxes[i].value + ',';
                }
            }
            //alert(degreeNo);
            $('#<%= hdndegreeno.ClientID %>').val(degreeNo);
            //document.getElementById(inpHide).value = "degreeNo";


            if (alertmsg != '') {
                alert(alertmsg);
                HideLoader(id, 'Submit');
                return false;
            }

            return true;
        }
    </script>

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

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updGradeEntry"
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
    <div id="contents">
        <%--This is testing--%>
    </div>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                 <%--   <h3 class="box-title"><b>DEGREE MAPPING</b></h3>--%>
                     <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div id="divMsg" runat="server"></div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <%-- <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label> College/School Name </label>
                                </div>--%>
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                     <label ID="lblDYCollege" runat="server">Institute Name</label>
                                    <%--<asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>--%>
                                </div>
                                <asp:DropDownList ID="ddlColg" runat="server"
                                    AppendDataBoundItems="True" AutoPostBack="True" ValidationGroup="submit" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged"
                                    ToolTip="Please Select Institute">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="ddlColg"
                                    ErrorMessage="Please Select College/School Name" ValidationGroup="submit"
                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>

                            <%-- <div class="form-group col-md-4" style="display:none;">
                                <label><span style="color: red;">*</span> Degree</label>
                                <asp:DropDownCheckBoxes ID="ddlDegree" runat="server" AddJQueryReference="true" UseButtons="True"
                                        UseSelectAllNode="True" AutoPostBack="false" RepeatDirection="Horizontal">
                                </asp:DropDownCheckBoxes>
                            </div>--%>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <%--<div class="label-dynamic">
                                    <sup>* </sup>
                                    <label> Degree</label>
                                </div>--%>
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <%--<asp:Label ID="lblDYDegree" runat="server" Font-Bold="true"></asp:Label>--%>
                                    <label> Degree</label>
                                </div>
                                <asp:DropDownList ID="ddlDegreeMultiCheck" runat="server" CssClass="form-control" multiple="multiple">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <asp:UpdatePanel ID="updGradeEntry" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="col-12 btn-footer">
                               <%-- <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                    CssClass="btn btn-primary" CausesValidation="true" OnClick="btnSave_Click" OnClientClick="return getVal();" />--%>

                                <asp:LinkButton ID="btnSave" runat="server" ToolTip="Submit" ValidationGroup="submit" OnClientClick="return getVal()"
                                    CssClass="btn btn-primary btnX" OnClick="btnSave_Click">Submit</asp:LinkButton>

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:HiddenField ID="hdndegreeno" runat="server" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSave" />
                            <%--<asp:AsyncPostBackTrigger ControlID="ddlColg" EventName="SelectedIndexChanged"/>--%>
                        </Triggers>
                    </asp:UpdatePanel>

                    <div class="col-12">
                        <asp:Panel ID="pnlconfiguration" runat="server">
                            <asp:ListView ID="lvlist" runat="server">
                                <LayoutTemplate>
                                    <div id="demo-grid">
                                        <div class="sub-heading">
                                             <h5>Institute Degree Mapping List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divdegreelist">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="text-align: center;">Action
                                                    </th>
                                                    <th> Faculty /School Degree Mapping
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
                                        <td style="text-align: center;">
                                            <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("col_degree_no") %>'
                                                AlternateText="Delete Record" OnClientClick="return UserDeleteConfirmation();" OnClick="btnDel_Click"
                                                TabIndex="6" ToolTip='<%# Eval("col_degree_no") %>' />
                                        </td>
                                        <td>
                                            <%# Eval("college_name") %>  - <%# Eval("degreename") %>   
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


    <script type="text/javascript">
        function UserDeleteConfirmation() {
            if (confirm("Are you sure you want to delete this Entry?"))
                return true;
            else
                return false;
        }
    </script>

    <%-- <script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#divdegreelist').DataTable({

            });
        }

        </script>--%>
    <%--<script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#divdegreelist').DataTable({

            });
        }

    </script>--%>
</asp:Content>



