<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="SeatingArrangment.aspx.cs" Inherits="ACADEMIC_SEATINGARRANGEMENT_SeatingArrangment"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 60px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updplRoom"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 212px; padding-left: 5px; height: 18px;">
                    <img src="../../IMAGES/ajax-loader.gif" alt="Loading" />
                    Please Wait..
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" colspan="3" valign="top" style="height: 30px">
                SEATING ARRANGEMENT
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="updplRoom" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="2" cellspacing="2" border="0">
                <tr>
                    <td>
                        <fieldset class="fieldset">
                            <legend class="legend">Seating Arrangement</legend>
                            <table width="100%" cellpadding="2" cellspacing="2" border="0">
                                <tr>
                                    <td width="10%" class="form_left_label">
                                        Session :
                                    </td>
                                    <td colspan="2" width="55%">
                                        <asp:DropDownList ID="ddlSession" runat="server" Width="35%" TabIndex="1" AppendDataBoundItems="True"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="10%" class="form_left_label">
                                        Room :
                                    </td>
                                    <td colspan="2" width="55%">
                                        <asp:DropDownList ID="ddlRoom" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlRoom_SelectedIndexChanged" TabIndex="2" Width="35%">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvRoom" runat="server" ControlToValidate="ddlRoom"
                                            Display="None" ErrorMessage="Please Select Room" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr id="trRoomCapacity" runat="server" visible="false">
                                    <td width="10%" class="form_left_label">
                                        &nbsp;
                                    </td>
                                    <td>
                                        Actual Capacity :
                                        <asp:Label ID="lblActualCapacity" runat="server"></asp:Label>
                                        &nbsp; <%--Capacity :--%>
                                        <asp:Label ID="lbl1" runat="server" Visible="false"></asp:Label> 
                                        <asp:Label ID="lbl2" runat="server" Visible="false"></asp:Label> 
                                        <asp:Label ID="lbl3" runat="server" Visible="false"></asp:Label> 
                                    </td>
                                </tr>
                                <tr id="trbranch1" runat="server" visible="false">
                                    <td width="10%" class="form_left_label">
                                        Branch1 :
                                    </td>
                                    <td width="50%" class="form_left_label">
                                        <asp:DropDownList ID="ddlBranch1" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlBranch1_SelectedIndexChanged" TabIndex="3" Width="35%">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;<asp:CheckBox ID="chkAdd1" runat="server" Text="Add Branch" Enabled="False"
                                            AutoPostBack="True" OnCheckedChanged="chkAdd1_CheckedChanged" />
                                        &nbsp;&nbsp; Semester 1 :
                                        <asp:DropDownList ID="ddlSemester1" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            TabIndex="1" Width="15%">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="trbranch2" runat="server" visible="false">
                                    <td width="10%" class="form_left_label">
                                        Branch2 :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBranch2" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlBranch2_SelectedIndexChanged" TabIndex="4" Width="35%">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;<asp:CheckBox ID="chkAdd2" runat="server" Text="Add Branch" Enabled="False"
                                            AutoPostBack="True" OnCheckedChanged="chkAdd2_CheckedChanged" />
                                        &nbsp; Semester 2 :
                                        <asp:DropDownList ID="ddlSemester2" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            TabIndex="1" Width="15%" OnSelectedIndexChanged="dlSemester2_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="trbranch3" runat="server" visible="false">
                                    <td width="10%" class="form_left_label">
                                        Branch3 :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBranch3" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlRoom_SelectedIndexChanged" TabIndex="5" Width="35%">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp; Semester 3 :
                                        <asp:DropDownList ID="ddlSemester3" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            TabIndex="6" Width="15%" OnSelectedIndexChanged="dlSemester3_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                               <tr >
                                    <td>
                                        Arrangement
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlArrangement" runat="server" AppendDataBoundItems="True"
                                            AutoPostBack="True" TabIndex="7" Width="20%">
                                            
                                            <asp:ListItem Value="1">Vertical</asp:ListItem>
                                            
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        &nbsp;<asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="configure" />
                                        <asp:ValidationSummary ID="vsRoomNm" runat="server" DisplayMode="List" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="Submit" />
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="btnConfigure" runat="server" Text="Configure" Width="100px" ValidationGroup="configure"
                                            TabIndex="8" OnClick="btnConfigure_Click" />&nbsp;
                                        <asp:Button ID="btnRoomwise" runat="server" Text="Roomwise Report" Width="15%" ValidationGroup="process"
                                            OnClick="btnRoomwise_Click" TabIndex="14"  Visible="false"/>
                                        
                                        <asp:Button ID="btnStastical" runat="server" Text="Statistical Report" ValidationGroup="process"
                                            Width="15%" OnClick="btnStastical_Click" TabIndex="15"   Visible="false"/>
                                        
                                        <asp:Button ID="btnRoomSeats" runat="server" Text="Room Seat Report" ValidationGroup="process"
                                            Width="15%" OnClick="btnRoomSeats_Click" TabIndex="16"   Visible="false"/>
                                        
                                         <asp:Button ID="btnRoomSeatsEx" Visible = "false" runat="server" Text="Export Room Seat Report" ValidationGroup="process"
                                            Width="17%" OnClick="btnRoomSeatsEx_Click" TabIndex="16" />
                                        &nbsp;  
                                        <asp:Button ID="btnMasterSeating" runat="server" Text="Master Seating Plan" ValidationGroup="process"
                                            Width="15%" OnClick="btnMasterSeating_Click" TabIndex="15" />
                                        &nbsp;
                                        <asp:Button ID="btnConsolidateSeating" runat="server" Text="Consolidate Seating Plan" ValidationGroup="process"
                                            Width="15%" OnClick="btnConsolidateSeating_Click" TabIndex="15" />
                                        &nbsp;
                                        <asp:Button ID="btnClear" runat="server" Width="100px" OnClick="btnCancel_Click"
                                            Text="Cancel" />
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
