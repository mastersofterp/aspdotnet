<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CurrencyHeads.ascx.cs" Inherits="ACADEMIC_MASTERS_CurrencyHeads" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

   
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">FEE HEAD CURRENCY MAPPING</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Select Receipt Type</label>
                                </div>
                                <asp:DropDownList ID="ddlRecType" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                    AppendDataBoundItems="true" ValidationGroup="Fees" ToolTip="Please Select Receipt Type" TabIndex="1"
                                    OnSelectedIndexChanged="ddlRecType_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvRecType" runat="server" ErrorMessage="Please Select Receipt Type"
                                    Display="None" ControlToValidate="ddlRecType" InitialValue="0" SetFocusOnError="true"
                                    ValidationGroup="Fees" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Select Payment Type</label>
                                </div>
                                <asp:DropDownList ID="ddlPayType" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                    AppendDataBoundItems="true" ValidationGroup="Fees" ToolTip="Please Select Payment Type" TabIndex="2"
                                    OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPayType" runat="server" ErrorMessage="Please Select Pay Type"
                                    Display="None" ControlToValidate="ddlPayType" InitialValue="0" SetFocusOnError="true"
                                    ValidationGroup="Fees" />
                            </div>

                             <div class="form-group col-lg-3 col-md-6 col-12" id="divCurencyType" runat="server" visible="false">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Currency Type</label>
                                </div>
                                  <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="form-control"  AppendDataBoundItems="true"  data-select2-enable="true">
                                       <asp:ListItem Value="0">Please Select</asp:ListItem>
                                  </asp:DropDownList>                           
                            </div>

                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:HiddenField ID="hfdcount" runat="server" Value="0" />
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="Fees" TabIndex="3"
                                CssClass="btn btn-primary" OnClick="btnSave_Click" />
                        <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" TabIndex="4" Visible="false"
                                Enabled="False"  CssClass="btn btn-info"/> 
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false" TabIndex="5"
                            CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                    </div>

                    <div class="col-12">
                        <asp:Panel ID="pnlfees" runat="server">
                            <asp:ListView ID="lvFeesHead" runat="server" >
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Currency Heads Defination</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tbl">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>
                                                    Head
                                                </th>
                                                <th>
                                                    Long Name
                                                </th>
                                                <th>
                                                    Short Name
                                                </th>
                                                <th>
                                                    Currency
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
                                            <asp:Label ID="txtHead" runat="server" Text='<%#Eval("FEE_HEAD")%>' ToolTip='<%#Eval("FEE_TITLE_NO")%>'
                                                Width="10px" Font-Bold="true" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLName" MaxLength="30" runat="server" Text='<%#Eval("FEE_LONGNAME")%>'
                                                CssClass="form-control" />
                                            <asp:CustomValidator ClientValidationFunction="ValidateShortName" ControlToValidate="txtLName"
                                                Display="None" EnableClientScript="true" ErrorMessage="Please enter short as well."
                                                ID="valShortName" runat="server" ValidateEmptyText="false" ValidationGroup="Fees" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSName" MaxLength="8" runat="server" Text='<%#Eval("FEE_SHORTNAME")%>'
                                                 CssClass="form-control" />
                                        </td>
                                        <td>
<%--                                            <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="form-control" data-select2-enable="true">
                                            </asp:DropDownList>--%>
                                           <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("CUR_NAME")%>'></asp:Label> 
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="Fees" />
                    </div>
                    
                    <div class="col-12 btn-footer">
                        <asp:Label ID="lblerror" runat="server" SkinID="Errorlbl" />
                        <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg" />
                    </div>
                    <div id="divMsg" runat="server">
                    </div>
                </div>

            </div>
        </div>
    </div>
      <%--===== Data Table Script added by gaurav =====--%>
        

    <script type="text/javascript" language="javascript">

    function ValidateShortName(sender, args)
    {
        try
        {
            if(args.Value.length > 0 && sender.id != '')
            {
                var txtShortNameId = sender.id.substr(0, sender.id.indexOf('valShortName'));
                txtShortNameId += 'txtSName';
                
                if(txtShortNameId != null && txtShortNameId != '')
                {
                    var txtShortName = document.getElementById(txtShortNameId);
                    if(txtShortName != null && txtShortName.value.trim() == '')
                    {
                        args.IsValid = false;
                        document.getElementById(txtShortName.id).focus();
                    }
                }
            }
        }
        catch(e)
        {
            alert("Error: " + e.description);
        }
    }
            </script>

