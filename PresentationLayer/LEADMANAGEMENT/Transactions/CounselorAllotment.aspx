<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CounselorAllotment.aspx.cs" Inherits="ACADEMIC_CounselorAllotment" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <!-- jQuery library -->
   <%-- <link href="../../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet"/>--%>
     <%--<link href="<%#Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css") %>" rel="stylesheet" />--%>
    <%--<link href="../../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet"/>--%>
    <link href="../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
     <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
           <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>



     <style>
             
        /*--======= toggle switch css added by gaurav 29072021 =======--*/
        .switch input[type=checkbox] {
	        height: 0;
	        width: 0;
	        visibility: hidden;
        }
        .switch label {
	        cursor: pointer;
	        width: 82px;
            height: 34px;
	        background: #dc3545;
	        display: block;
	        border-radius: 4px;
	        position: relative;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }
        .switch label:hover {
            background-color: #c82333;
        }
        .switch label:before {
	        content: attr(data-off);
	        position: absolute;
	        right: 0;
	        font-size: 16px;
            padding: 4px 8px;
            font-weight: 400;
            color: #fff;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;

        }
        .switch input:checked + label:before {
	        content: attr(data-on);
	        position: absolute;
	        left: 0;
	        font-size: 16px;
            padding: 4px 15px;
            font-weight: 400;
            color: #fff;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }
        .switch label:after {
	        content: '';
	        position: absolute;
	        top: 1.5px;
            left: 1.7px;
            width: 10.2px;
            height: 31.5px;
            background: #fff;
            border-radius: 2.5px;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }
        .switch input:checked + label {
	        background: #28a745;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }
        .switch input:checked + label:hover {
	        background: #218838;
        }
        .switch input:checked + label:after {
	        transform: translateX(68px);
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }
   </style>
        
     <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static"/>
    
    
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updGradeEntry"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div class="loader-container">
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__ball"></div>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

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

    <div id="contents">
        <%--This is testing--%>
    </div>
    <asp:UpdatePanel ID="updGradeEntry" runat="server">
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
                                                    <asp:Label ID="lblIntake" runat="server" Font-Bold="true" Text="Admission Batch"></asp:Label> 
                                                    </div>
                                            <asp:DropDownList ID="ddlAdmbatch" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True"
                                            ToolTip="Please Select Batch" TabIndex="2" ClientIDMode="Static">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvADmBatch" runat="server" Display="None" ControlToValidate="ddlAdmbatch" ErrorMessage="Please Select Admission Batch."
                                            InitialValue="0" ValidationGroup="Submit" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                     </div> 
                                   <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                     <sup>* </sup>
                                                     <asp:Label ID="lblDYstudylevel" runat="server" Font-Bold="true" Text="Programme Type"></asp:Label>
                                                  
                                                </div>
                                            <asp:DropDownList ID="ddlProgrammeType" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True"
                                            ToolTip="Please Select Programme Type." TabIndex="2" ClientIDMode="Static">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="ddlProgrammeType" ErrorMessage="Please Select Programme Type."
                                            InitialValue="0" ValidationGroup="Submit" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                   </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblDYmainuser" runat="server" Font-Bold="true" Text="Main Counsellor"></asp:Label>
                                                   
                                                </div>
                                            <asp:DropDownList ID="ddlmainuser" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True"
                                            ToolTip="Please Select Main User." TabIndex="3" ClientIDMode="Static">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None" ControlToValidate="ddlmainuser" ErrorMessage="Please Select Main User."
                                            InitialValue="0" ValidationGroup="Submit" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                     </div>
                                  
                                     <div class="form-group col-lg-3 col-md-6 col-12" id="divSubCounsellor" runat="server">
                                                 <div class="label-dynamic">
                                                      <sup>* </sup>
                                                     <asp:Label ID="lblDYsubuser" runat="server" Font-Bold="true" Text="SubCounsellors"></asp:Label>
                                                       </div>
                                            <asp:ListBox ID="ddlsubuser" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"
                                            SelectionMode="multiple" TabIndex="8"></asp:ListBox>
                                         <asp:RequiredFieldValidator ID="rfvSubUser" runat="server" ControlToValidate="ddlsubuser" Display="None" ErrorMessage="Please Select Sub User." ValidationGroup="Submit" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                      </div>

                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblDYStatus" runat="server" Font-Bold="true" Text="Status"></asp:Label> 
                                                                </div>
                                                <div class="switch form-inline">
                                                    <input type="checkbox" id="switch" name="switch" class="switch" checked/>
                                                    <label data-on="Active" data-off="Inactive" for="switch"></label>
                                                </div>
                                     </div>
                            </div>
                            </div>
                                          <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSave" runat="server" ToolTip="Submit"
                                            CssClass="btn btn-primary" OnClick="btnSave_Click" TabIndex="8" Text="Submit" OnClientClick="return validate();"></asp:Button>
                                              <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-warning" TabIndex="3" OnClick="btnCancel_Click">Clear</asp:LinkButton>
                                              <asp:ValidationSummary ID="vsSummary" runat="server" ValidationGroup="Submit" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                                    </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlSession" runat="server">
                                    <asp:ListView ID="lvlist" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                    <div class="sub-heading">
                                                    <h5>Counselor Allotment List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divdepartmentlist">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Action </th>
                                                            <th>Admission Batch</th>
                                                            <th>Programme Type</th>
                                                            <th>Main Counselor</th>
                                                            <th>SubCounsellor</th>
                                                            <th>Status</th>
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit1.png" CommandArgument='<%# Eval("SR_NO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" OnClientClick="return InitAutoCompl();" />
                                                </td>
                                                <td><%# Eval("BATCHNAME") %></td>
                                                <td><%# Eval("ua_sectionname") %></td>
                                                <td><%# Eval("UA_FULLNAME") %></td>
                                                <td><%# Eval("SUBUSER_UA_NO") %> </td>                                                
                                                <td><asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' ForeColor=' <%# Convert.ToString(Eval("Status")).Equals("Active")? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label></td>
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
            <%--<asp:AsyncPostBackTrigger ControlID="lvlist" />--%>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="ddlsubuser" />
            
        </Triggers>
    </asp:UpdatePanel>
     

 <%--   <script type="text/javascript">

        function validate() {
            alert("in");
            var summary = "";
            if ($('#ddlAdmbatch').val() == "0")
                summary += '<br>Please Select Admission Batch.';
            if ($('#ddlProgrammeType').val() == "0")
                summary += '<br>Please Select Programme Type.';
            if ($('#ddlmainuser').val() == "0")
                summary += '<br>Please Select Main Counsellor.';
            if ($('#ddlsubuser').val() == "0")
                summary += '<br>Please Select Sub Counsellors.';
            if (summary != "") {
                customAlert(summary);
                summary = "";
                return false
            }
        }
    </script>--%>

         <script type="text/javascript">
             $(document).ready(function () {
                 $('.multi-select-demo').multiselect({
                     includeSelectAllOption: true,
                     maxHeight: 200
                 });
             });
             var parameter = Sys.WebForms.PageRequestManager.getInstance();
             parameter.add_endRequest(function () {
                 $('.multi-select-demo').multiselect({
                     includeSelectAllOption: true,
                     maxHeight: 200
                 });
             });

        </script>
        <script>
            function SetStat(val) {
                $('#switch').prop('checked', val);
            }

            function validate() {
                var summary = "";
                var admBatch = document.getElementById('<%=ddlAdmbatch.ClientID%>').value;
                var programmeType = document.getElementById('<%=ddlProgrammeType.ClientID%>').value;
                var mainUser = document.getElementById('<%=ddlmainuser.ClientID%>').value;
                var subUser = document.getElementById('<%=ddlsubuser.ClientID%>').value;
                
                
                if (admBatch == "0") {
                    //alert("in");
                    summary += "Please Select Admission Batch.\n";
                    //alert(summary);
                }
                if (programmeType == "0") {
                    summary += "Please Select Programme Type.\n";
                }
                if (mainUser == "0") {
                    summary += "Please Select Main User.\n";
                }
                if (subUser == "") {
                    summary += "Please Select Sub User.\n";
                }
                if (summary != "") {
                    alert(summary);
                    summary = "";
                    return false;
                }
                $('#hfdStat').val($('#switch').prop('checked'));
            }

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(function () {
                    $('#btnSave').click(function () {
                        validate();
                    });
                });
            });
    </script>

</asp:Content>

