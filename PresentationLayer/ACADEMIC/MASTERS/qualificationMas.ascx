<%@ Control Language="C#" AutoEventWireup="true" CodeFile="qualificationMas.ascx.cs"
    Inherits="Masters_qualificationMas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%--<link href="../Css/master.css" rel="stylesheet" type="text/css" />
<link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
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


<div id="divMsg" runat="server">
</div>


<div class="row">
    <div class="col-md-12 col-sm-12 col-12">
        <div class="box box-primary">
            <div id="div1" runat="server"></div>
            <div class="box-header with-border">
                <h3 class="box-title">QUALIFICATION MANAGEMENT</h3>
            </div>

            <div class="box-body">
                <div class="col-12">
                    <div class="row">
                        <div class="form-group col-lg-3 col-md-6 col-12">
                            <div class="label-dynamic">
                                <sup>* </sup>
                                <label>Qualification Type</label>
                            </div>
                            <asp:DropDownList ID="ddlQType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                ToolTip="Please Select Qualification Type" TabIndex="1">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvQType" runat="server" InitialValue="0" ControlToValidate="ddlQType"
                                Display="None" ErrorMessage="Please Select Qualification Type" ValidationGroup="qual"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group col-lg-3 col-md-6 col-12">
                            <div class="label-dynamic">
                                <sup>* </sup>
                                <label>Qualification</label>
                            </div>
                            <asp:TextBox ID="txtQualification" runat="server" MaxLength="20" CssClass="form-control" TabIndex="2"
                                ToolTip="Please Enter Qualification(only alphabets)" />
                            <asp:RequiredFieldValidator ID="rfvQualification" runat="server" ControlToValidate="txtQualification"
                                Display="None" ErrorMessage="Please Enter Qualification Name" ValidationGroup="qual"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revQualification" runat="server" ErrorMessage="Please Enter Alphabets Only for Qualification"
                                ValidationGroup="qual" ControlToValidate="txtQualification" ValidationExpression="^[a-zA-Z ]+$"
                                Display="None" />
                        </div>
                    </div>
                </div>

                <div class="col-12 btn-footer">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                        ValidationGroup="qual" ToolTip="Submit" TabIndex="3" CssClass="btn btn-primary" />
                    <asp:Button ID="btnShowReport" runat="server" Text="Report" CausesValidation="False"
                        ToolTip="Show Report" TabIndex="5" CssClass="btn btn-info" OnClick="btnShowReport_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                        CausesValidation="False" ToolTip="Cancel" TabIndex="4" CssClass="btn btn-warning" />

                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="qual"
                        DisplayMode="List" ShowSummary="false" ShowMessageBox="true" />

                    <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                </div>

                <div class="col-12">
                    <asp:Repeater ID="lvQualification" runat="server">
                        <HeaderTemplate>
                            <div class="sub-heading">
                                <h5>Qualification List</h5>
                            </div>

                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                <thead class="bg-light-blue">
                                    <tr>

                                        <th>Action</th>
                                        <th>Qualification</th>
                                        <th>Qualification Level</th>
                                    </tr>
                                    <%----%>
                                </thead>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server" />
                                </tbody>
                            </table>

                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="item">
                                <td>
                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("qualino") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                    <asp:Label ID="lblQLevelNo" runat="server" Text='<%# Eval("qualilevelno") %>' Visible="false" />
                                </td>
                                <td>
                                    <asp:Label ID="lblQuali" runat="server" Text='<%# Eval("quali") %>' /></td>
                                <td><%# Eval("qualilevelname") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody></table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</div>
