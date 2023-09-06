<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="DRCMaster.aspx.cs" Inherits="ACADEMIC_MASTERS_DRCMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <script type="text/javascript">
      function RunThisAfterEachAsyncPostback()
       {
            RepeaterDiv();

       }
    
   function RepeaterDiv()
{
  $(document).ready(function() {
      
            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
 
}
    </script>

    <script src="../../Content/jquery.js" type="text/javascript"></script>

    <script src="../../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBatch"
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

    <asp:UpdatePanel ID="updBatch" runat="server">
        <ContentTemplate>

            <div id="divMsg" runat="server">
            </div>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PHD DRC MASTER</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>DRC Name</label>
                                        </div>

                                        <asp:TextBox ID="txtDRCName" runat="server" TabIndex="1" MaxLength="50" CssClass="form-control"
                                            ToolTip="Please Enter DRC Name" />
                                        <asp:RequiredFieldValidator ID="rfvDRCName" runat="server" ControlToValidate="txtDRCName"
                                            Display="None" ErrorMessage="Please Enter DRC Name" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Department</label>
                                        </div>

                                        <asp:DropDownList ID="ddlDept" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select Department" AutoPostBack="True"
                                            AppendDataBoundItems="True">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                                            Display="None" ErrorMessage="Please Select Department" ValidationGroup="submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Type</label>
                                        </div>

                                        <asp:DropDownList ID="ddlType" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select Type" AutoPostBack="True"
                                            AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Internal</asp:ListItem>
                                            <asp:ListItem Value="2">External</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                    OnClick="btnSave_Click" TabIndex="3" CssClass="btn btn-primary" />
                                <asp:Button ID="btnShowReport" runat="server" CausesValidation="False" OnClick="btnShowReport_Click"
                                    TabIndex="4" Text="Report" ToolTip="Show Report" CssClass="btn btn-info" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    OnClick="btnCancel_Click" TabIndex="5" CssClass="btn btn-warning" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                    <asp:Repeater ID="lvDRCName" runat="server">
                                        <HeaderTemplate>
                                            <div class="sub-heading">
                                                <h5>DRC Name List</h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>DRC Name
                                                        </th>
                                                        <th>Department
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <%-- <tr id="itemPlaceholder" runat="server" />
                                                </tbody>--%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("DRCNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="6" />
                                                </td>
                                                <td>
                                                    <%# Eval("DRCNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DEPARTMENT")%>
                                                </td>
                                            </tr>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody></table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
