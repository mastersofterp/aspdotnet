<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Registration_Grade_Master.aspx.cs" Inherits="ACADEMIC_EXAMINATION_Registration_Grade_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <style>
        .Tab:focus {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }
    </style>

    <script src="../../Content/jquery.js" type="text/javascript"></script>

    <script src="../../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updGrade"
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


    <asp:UpdatePanel ID="updGrade" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title"><b>GRADE REGISTRATION</b></h3>--%>
                             <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>

                        <div class="box-body">

                            <div class="col-md-12">
                                <%--<asp:Label ID="Label1" runat="server" Font-Bold="True" Style="color: Red"></asp:Label>--%>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <%--<label><span style="color: red;">*</span> Activity Name</label><br />--%>
                                    <label><span style="color: red;">*</span><asp:Label ID="lblDYActivityName" runat="server" Font-Bold="true"></asp:Label></label><br />
                                    <asp:DropDownList ID="ddlActivityName" runat="server" TabIndex="1" ToolTip="Please Select Activity Name" data-select2-enable="true" AppendDataBoundItems="true"  AutoPostBack="True" OnSelectedIndexChanged="ddlActivityName_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvActivityName" runat="server" ControlToValidate="ddlActivityName"
                                        Display="None" ErrorMessage="Please Select Activity Name" ValidationGroup="submit"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-4">
                                    <%--<label><span style="color: red;">*</span> Grade</label><br />--%>
                                    <label><span style="color: red;">*</span><asp:Label ID="lblDYddlGrade" runat="server" Font-Bold="true"></asp:Label></label><br />
                                    <asp:ListBox ID="ddlGrade" runat="server" SelectionMode="Multiple" TabIndex="1"
                                        CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>

                                    <asp:RequiredFieldValidator ID="rvfGradeName" runat="server" ControlToValidate="ddlGrade"
                                        Display="None" ErrorMessage="Please Enter Grade Name" ValidationGroup="submit"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>



                                <div class="col-md-4">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <%--<label>Status</label>--%>
                                        <asp:Label ID="lblDYStatus" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="switch form-inline">
                                        <input type="checkbox" id="rdActive" tabindex="1" checked />
                                        <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                        <asp:HiddenField ID="hfActive" runat="server" ClientIDMode="Static" />
                                    </div>
                                </div>

                                <div class="form-group col-lg-6 col-md-6 col-12">
                                    <div class=" note-div">
                                        <h5 class="heading"><i class="fa fa-star" aria-hidden="true"></i>Note</h5>
                                        <p>
                                            <span><sup>*</sup> Marked fields are mandatory.</span><br />
                                        </p>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-12 col-md-12 d-flex justify-content-center" style="margin-top: 25px">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                    TabIndex="3" CssClass="btn btn-success" OnClick="btnSave_Click" OnClientClick="return SetStat(this)" />
                                &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    TabIndex="4" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>


                            <div class="col-md-12 mt-5">
                                <table class="table table-striped table-bordered nowrap display">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Action
                                            </th>
                                            <th>Activity Name
                                            </th>
                                            <th>Grade
                                            </th>
                                            <th>Status</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:ListView ID="lvSession" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png"
                                                            AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6"
                                                            CommandArgument='<%# Eval("ACTIVITY_NO")%>'
                                                            OnClick="btnEdit_Click" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("CRS_ACTIVITY_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("GRADE")%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblIsActive" runat="server" CssClass='<%# Eval("ACTIVE_STATUS")%>'
                                                            Text='<%# Eval("ACTIVE_STATUS")%>' ForeColor='<%# Eval("ACTIVE_STATUS").ToString().Equals("Active")?
                                                    System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </tbody>
                                </table>
                            </div>
                            </fieldset>
                        </div>


                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
     
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200
                });
            });
        });

        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }
        function SetStat(val) {
            debugger
            var chkActive = document.getElementById("rdActive");

            if (chkActive.checked) {

                $('#hfActive').val(true);

            }
            else {
                $('#hfActive').val(false);
            }

        }

      
    </script>

    <%-- <script>

        function validateForm(val) {
            var activityName = document.getElementById('" + ddlActivityName.ClientID + @"');
            var grade = document.getElementById('" + ddlGrade.ClientID + @"');

            if (activityName.value == '0') {
                alert('Please select Activity Name');
                activityName.focus();
                return false;
            }

            if (grade.options.length == 0) {
                alert('Please select at least one Grade');
                grade.focus();
                return false;
            }

            return true;
        }



    </script>--%>
</asp:Content>

