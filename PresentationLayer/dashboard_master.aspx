<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="dashboard_master.aspx.cs" Inherits="dashboard_master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upd1"
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
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">DASHBOARD MASTER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
	                                    <div class="label-dynamic">
		                                    <sup>* </sup>
		                                    <label>DashBoard Name</label>
	                                    </div>
                                        <asp:TextBox ID="txtdashboardname" AutoComplete="off" placeholder="Enter DashBoard Name" runat="server" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvdashboard" runat="server" ControlToValidate="txtdashboardname"
                                            Display="None" ErrorMessage="Please Enter DashBoard Name" SetFocusOnError="True"
                                            ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
	                                    <div class="label-dynamic">
		                                    <sup>* </sup>
		                                    <label>Session</label>
	                                    </div>
                                        <asp:DropDownList ID="ddlsession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvsession" runat="server" ControlToValidate="ddlsession"
                                            Display="None" ErrorMessage="Please select Session." InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="show" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="row">
                                            <div class="form-group col-6">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Active Status</label>
                                                </div>
                                                <div class="switch form-inline">
                                                    <input type="checkbox" id="rdActive" name="switch" checked />
                                                    <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnsubmit" runat="server" OnClientClick="return validate();" Text="Submit" CssClass="btn btn-primary" TabIndex="4" OnClick="btnsubmit_Click" ValidationGroup="show" />
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="5" OnClick="btncancel_Click" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="show" />
                            </div>
                            <div class="col-12">
                                <asp:ListView ID="lvdashboard" runat="server" OnItemDataBound="lvdashboard_ItemDataBound">
                                    <LayoutTemplate>
                                        <div>
                                            <div class="sub-heading">
                                                <h5>Dash Board List</h5>
                                            </div>
                                            <table class="table table-bordered table-hover table-fixed table-striped display" id="divdashboardlist" style="width:100%;">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="width: 5%; text-align: center;">Action
                                                        </th>
                                                        <th style="width: 10%">Sr No.
                                                        </th>
                                                        <th>Name
                                                        </th>
                                                        <th>Session
                                                        </th>
                                                        <th>
                                                        Status
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
                                                <%-- circulamdetails,documentlist--%>
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit1.gif" CommandArgument='<%# Eval("ID") %>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                            </td>
                                            <td>
                                                <%# Container.DataItemIndex + 1%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblname" runat="server" Text='<%# Eval("NAME")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("SESSION_PNAME")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("STATUS")%>' ToolTip='<%# Eval("SESSIONNO")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#divdashboardlist').DataTable({

            });
        }

    </script>--%>
      <script>
          function SetStatActive(val) {
              $('#rdActive').prop('checked', val);
          }

          function validate() {

              $('#hfdActive').val($('#rdActive').prop('checked'));


              var idtxtweb = $("[id$=txtdashboardname]").attr("id");
              var txtweb = document.getElementById(idtxtweb);
              if (txtweb.value.length == 0) {
                  alert('Please Enter DashBoard Name', 'Warning!');
                  //$(txtweb).css('border-color', 'red');
                  $(txtweb).focus();
                  return false;
              }

              var ddlState = $("[id$=ddlsession]").attr("id");
              var ddlState = document.getElementById(ddlState);
              if (ddlState.value == 0) {
                  alert('Please select Session', 'Warning!');
                  $(ddlState).focus();
                  return false;
              }

          }
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              $(function () {
                  $('#btnsubmit').click(function () {
                      validate();
                  });
              });
          });
    </script>
</asp:Content>

