<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MessAdvance.aspx.cs" Inherits="Mess_Advance" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table2').DataTable();
        });
</script>

<script type="text/javascript">
    //On UpdatePanel Refresh
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                $('#table2').dataTable();
            }
        });
    };

    function CheckNumeric(event, obj) {
        var k = (window.event) ? event.keyCode : event.which;
        //alert(k);
        if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
            obj.style.backgroundColor = "White";
            return true;
        }
        if (k > 45 && k < 58) {
            obj.style.backgroundColor = "White";
            return true;

        }
        else {
            alert('Please Enter numeric Value');
            obj.focus();
        }
        return false;
    }
    onkeypress = "return CheckAlphabet(event,this);"
    function CheckAlphabet(event, obj) {

        var k = (window.event) ? event.keyCode : event.which;
        if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
            obj.style.backgroundColor = "White";
            return true;

        }
        if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
            obj.style.backgroundColor = "White";
            return true;

        }
        else {
            alert('Please Enter Alphabets Only!');
            obj.focus();
        }
        return false;
    }
</script>


<div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Mess Advance</h3>
                <div class="box-tools pull-right"></div>
            </div>

            <div style="color: Red; font-weight: bold">
              &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                <asp:Label ID="Label1" runat="server" Font-Names="Trebuchet MS" />
            </div>

            <div class="container">
                <div class="box-body row">
                    <div class="form-group col-md-4">
                        <label> Hostel Session</label>
                        <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" 
                                    AppendDataBoundItems="true" 
                                      />
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" ValidationGroup="submit"
                                    SetFocusOnError="True" InitialValue="0" />
                    </div>

                    <div class="form-group col-md-4">
                        <label><span style="color: Red;">*</span> Mess Type:</label>
                        <asp:DropDownList ID="ddlMess" runat="server" TabIndex="2" 
                                    AppendDataBoundItems="true" 
                                     OnSelectedIndexChanged="ddlMess_SelectedIndexChanged" AutoPostBack="True" />
                                <asp:RequiredFieldValidator ID="rfvmess" runat="server" ControlToValidate="ddlMess"
                                    Display="None" ErrorMessage="Please Select Mess Type." ValidationGroup="submit"
                                    SetFocusOnError="True" InitialValue="0" />
                    </div>

                    <div class="form-group col-md-4">
                        <label><span style="color: Red;">*</span> Advance Amt:</label>
                        <asp:TextBox ID="txtadvamt" runat="server"  Style="text-align: right"  TabIndex="3"/>
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtadvamt" ValidChars="1234567890."></ajaxToolKit:FilteredTextBoxExtender>
                                 <asp:RequiredFieldValidator ID="rfvBlockName" runat="server" ControlToValidate="txtadvamt"
                                    Display="None" ErrorMessage="Please Enter Advance Amount." ValidationGroup="submit" 
                                    SetFocusOnError="True" />
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-md-4">
                        <label><span style="color: Red;">*</span> Advance Date:</label>
                        <%--<asp:TextBox ID="txtadvdt" runat="server" MaxLength="50"  TabIndex="4" Width="200px"/>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/IMAGES/calendar.png" Style="cursor: hand"/>--%>
                            <%--<ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtadvdt" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true">
                            </ajaxToolKit:CalendarExtender>--%>

                        <%--change in date textbox by shubham barke on 22-03-22--%>
                                                                      <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgSessionStart" runat="server" class="fa fa-calendar"></i>
                                                </div>
                                          <asp:TextBox ID="txtadvdate" runat="server" TabIndex="4"  MaxLength="50"  ValidationGroup="submit" />
                                <ajaxToolKit:CalendarExtender ID="ceMessadvdate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtadvdate" PopupButtonID="imgSessionStart" />
                                <ajaxToolKit:MaskedEditExtender ID="metxtadvdate" runat="server" TargetControlID="txtadvdate"
                                    Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                    MaskType="Date" />
                                <ajaxToolKit:MaskedEditValidator ID="mvMessadvdate" runat="server" EmptyValueMessage="Please enter Advance date"
                                    ControlExtender="metxtadvdate" ControlToValidate="txtadvdate" IsValidEmpty="false"
                                    InvalidValueMessage="Date is invalid" Display="None" ErrorMessage="Please Select Date"
                                    InvalidValueBlurredMessage="*" ValidationGroup="submit" SetFocusOnError="true" />
                                <%--<asp:CompareValidator ID="cvStartDate" runat="server" ControlToValidate="txtSessionStart"
                                    Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter a valid start datemm/dd/yyyy)."
                                    EnableClientScript="False" ValidationGroup="submit">
                                </asp:CompareValidator>--%>
                                  <%--<asp:RequiredFieldValidator ID="rfvaddate" runat="server" ControlToValidate="txtadvdate"
                                    Display="None" ErrorMessage="Please Select Date." ValidationGroup="submit" SetFocusOnError="True" /> --%>  
                                        </div>
                               
                    </div>

                    <div class="form-group col-md-4">
                        <label>Committee Member :</label>
                        <asp:DropDownList ID="ddlCommMem" runat="server"  TabIndex="5" 
                                    AppendDataBoundItems="true" >
                                    <asp:ListItem Value="0" >Please Select</asp:ListItem>
                              </asp:DropDownList>
                    </div>

                    <div class="form-group col-md-4">
                        <label>Advance Remark</label>
                        <asp:TextBox ID="txtadvremark" runat="server"  TabIndex="6"
                                    TextMode="MultiLine" />
                    </div>
                </div>
            </div>

            <div class="box-footer">
                <p class="text-center">
                    <asp:Button ID="Button1" runat="server" Text="Submit" ValidationGroup="submit" CssClass="btn btn-primary"
                                    OnClick="btnSubmit_Click" TabIndex="7"/>
                                <asp:Button ID="Button2" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-warning"
                                    OnClick="btnCancel_Click" TabIndex="8"/>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" />
                </p>

      
                    
                        

                        <asp:Repeater ID="lvMess" runat="server">
                            <HeaderTemplate>
                                <div class="sub-heading">
                                    <h5>List of Mess</h5>
                                </div>
                                <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>
                                        Edit
                                    </th>
                                    <th>
                                        Delete
                                    </th>
                                    <th>
                                        Mess
                                    </th>
                                    <th>
                                        Amount
                                    </th>
                                    <th>
                                       Date
                                    </th>
                                    <th>
                                       Committee Member 
                                    </th>
                                    <th>
                                      Remark
                                    </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>

                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("MESSSRNO") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="15" />&nbsp;
                                                
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("MESSSRNO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td>
                              <%# Eval("MESS_NAME") %>
                            </td>
                            <td>
                              <%# Eval("ADV_AMOUNT") %>
                            </td>
                            <td>
                               <%# Eval("ADV_DATE") %>
                            </td>
                            <td>
                               <%# Eval("NAME") %>
                            </td>
                            <td>
                               <%# Eval("ADV_REMARK") %>
                            </td>
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


    <div id="divMsg" runat="server">
    </div>
</asp:Content>

