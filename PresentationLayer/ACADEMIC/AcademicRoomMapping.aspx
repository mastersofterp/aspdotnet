<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AcademicRoomMapping.aspx.cs" Inherits="ACADEMIC_AcademicRoomMapping" %>

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

   <%-- <link href="../plugins/multi-select/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../plugins/multi-select/bootstrap-multiselect.js"></script>--%>

    <script>
        //var MulSel = $.noConflict();
        $(document).ready(function () {
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
      <script>
          //var MulSel = $.noConflict();
          $(document).ready(function () {
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
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAcademroom"
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

    <asp:UpdatePanel ID="updAcademroom" runat="server">
        <ContentTemplate>
            <div id="divMsg" runat="server">
            </div>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">ROOM ALLOTMENT DEPARTMENT WISE</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Department</label>--%>
                                            <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddldept_SelectedIndexChanged" data-select2-enable="true" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddldept"
                                            Display="None" ErrorMessage="Please Select Department" ValidationGroup="submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlClgScheme" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                             data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlClgScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" AppendDataBoundItems="true" 
                                            data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"
                                            AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<sup>*</sup>--%>
                                            <label>Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBatch" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"
                                            AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control" AppendDataBoundItems="true" 
                                            data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Room</label>
                                        </div>
                                       <%-- <ul id="check-list-box" >
                                            <li class="list-group-item">--%>
                                                <asp:ListBox ID="lstRoom" runat="server" AppendDataBoundItems="true" SelectionMode="Multiple" CssClass="multi-select-demo"
                                                    TabIndex="2" BorderStyle="None"></asp:ListBox>
                                         <%--   </li>
                                        </ul>--%>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnsubmit_Click" OnClientClick="return controller_Validation();" />
                                <asp:Button ID="btnreport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnreport_Click" Visible="false" />
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btncancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="panel1" runat="server">
                                    <asp:ListView ID="lvlist" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Room Department Mapping List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divacadroomlist">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sr.no
                                                        </th>
                                                        <th>
                                                            <asp:Label ID="lblDYtxtSession" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>
                                                            <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>
                                                            <asp:Label ID="lblDYtxtSection" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>Batch</th>
                                                        <th>
                                                            <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>Room name
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Container.DataItemIndex + 1%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSession" runat="server" Text='<%# Eval("SESSION_PNAME") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblScheme" runat="server" Text='<%# Eval("SCHEMENAME") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSection" runat="server" Text='<%# Eval("SECTIONNAME") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblBatch" runat="server" Text='<%# Eval("BATCHNAME") %>'></asp:Label>
                                                </td>
                                                 <td>
                                                    <asp:Label ID="lblCourseno" runat="server" Text='<%# Eval("COURSE_NAME") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblroom" runat="server" Text='<%# Eval("ROOMNAME") %>'></asp:Label>
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
    </asp:UpdatePanel>



    <script type="text/javascript">

        var atLeast = 1

        function Validate() {
            if (document.getElementById("<%=ddldept.ClientID%>").value == "0") {
               // alert("Please Select Department");
                document.getElementById("<%=ddldept.ClientID%>").focus();
                return true;
            }
            var CHK = document.getElementById("<%=lstRoom.ClientID%>");

            var checkbox = CHK.getElementsByTagName("input");

            var counter = 0;

            for (var i = 0; i < checkbox.length; i++) {

                if (checkbox[i].checked) {

                    counter++;

                }

            }

            if (atLeast > counter) {

                alert("Please Select Atleast one Room.");

                return false;

            }

            return true;

        }

        function controller_Validation() {
            try{
                var ddlclgscheme = $("#ctl00_ContentPlaceHolder1_ddlClgScheme").val();
                var ddlSession = $("#ctl00_ContentPlaceHolder1_ddlSession").val();
                var ddlSection = $("#ctl00_ContentPlaceHolder1_ddlSection").val();
                var ddlCourse = $("#ctl00_ContentPlaceHolder1_ddlCourse").val();
                var lstRoom = $("#ctl00_ContentPlaceHolder1_lstRoom").val();
                var lblClgScheme = $("#ctl00_ContentPlaceHolder1_lblDYddlColgScheme").text();
                var lblSession = $("#ctl00_ContentPlaceHolder1_lblDYddlSession").text();
                var lblSection = $("#ctl00_ContentPlaceHolder1_lblDYddlSection").text();
                var lblCourse = $("#ctl00_ContentPlaceHolder1_lblDYddlCourse").text();
                var msg = "";
                if (ddlclgscheme == 0) {
                    msg += "Please Select " + lblClgScheme + ".\n";
                }
                if (ddlSession == 0) {
                    msg += "Please Select " + lblSession + ".\n";
                }
                if (ddlSection == 0) {
                    msg += "Please Select " + lblSection + ".\n";
                }
                if (ddlCourse == 0) {
                    msg += "Please Select " + lblCourse + ".\n";
                }
                if (lstRoom == 0) {
                    msg += "Please Select Room.";
                }
                if (msg != "") {
                    alert(msg);
                    return false;
                }
                else {
                    return true;
                }
            }
            catch (err) {
                alert(err.message);
            }
        }

    </script>

</asp:Content>

