<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="HostelPre-AllotmentProcess.aspx.cs"
 Inherits="HostelPreAllotmentProcess"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" cellpadding="0" cellspacing="0" border="0">
     <div class="row">
                            <div class="col-md-12">
                                <div class="box box-primary">
                                     <div class="box-header with-border">
                                        <h3 class="box-title">HOSTEL PRE-ALLOTMENT PROCESS </h3>
                                        <div class="box-tools pull-right"></div>
                                         <asp:Label ID="lblHelp" runat="server" SkinID="Msglbl"></asp:Label>
                                    </div>
                                    <div class="box-body">
                                        <div class="form-group col-md-3">
                                        <label>Hostel Session No.:</label>
                                <asp:DropDownList ID="ddlHostelSessionNo" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                    TabIndex="1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvHostelSessionNo" runat="server" ErrorMessage="Please Select Hostel Session"
                                    ControlToValidate="ddlHostelSessionNo" Display="None" InitialValue="0" ValidationGroup="Submit" />
                   
                                        </div> 
                                      <div class="form-group col-md-3">
                                        <label>Hostel Name:</label>
                                           <asp:DropDownList ID="ddlHostelNo" runat="server" CssClass="form-control"  AppendDataBoundItems="True" >
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvHostelNo" runat="server" ErrorMessage="Please Select Hostel Name"
                                    ControlToValidate="ddlHostelNo" Display="None" InitialValue="0" ValidationGroup="Submit" />
               

                                        </div> 
                                        </div> 
                                         <div class="box-footer">
                                         <p class="text-center">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Pre-Allotment Process" Width="200px"
                                     TabIndex="7" ValidationGroup="Submit" CssClass="btn btn-primary" ClientClick="return Confirm1();" onclick="btnSubmit_Click" />
                                     &nbsp;
                                      <asp:Button ID="btnallotlastallotement" CssClass="btn btn-primary" runat="server" Text="Move Last Session Allotment(Raman Bhavan)"   TabIndex="8" ValidationGroup="Submit" OnClientClick="return Confirm2();" onclick="btnallotlastallotement_Click" />
                                    &nbsp;<asp:Button ID="btnCancel"  CssClass="btn btn-warning" runat="server" CausesValidation="False"  Text="Cancel"
                                    Width="88px"  TabIndex="9" onclick="btnCancel_Click" />
                      
                                         </p> 
                                         </div> 
        
                                       
                            </td>
                        </tr>
                        <asp:ValidationSummary ID="vsStudent" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Submit" />
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="Add" />
            </td>
        </tr>
       </table>
      </div>
    </div>
    </div>  
    <div id="divMsg" runat="server">
    </div>
<script type ="text/javascript" language="javascript"  >
    function Confirm1() {
        if (Page_ClientValidate()) {
            var ret = confirm('Do You Want To Run Pre-Allotment Process ???');
            if (ret == true) {
                return true;
            }
            else {
                return false;
            }
        }
    }
    function Confirm2() {

        if (Page_ClientValidate()) {
            var ret = confirm('Do You Want To Insert Last Year Allotment ???');
            if (ret == true) {
                return true;
            }
            else {
                return false;
            }
        }
    }
</script>
</asp:Content>

