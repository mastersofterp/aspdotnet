<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Calendar.aspx.cs" Inherits="ITLE_Calendar" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <div>   
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
   
        <cc1:tabcontainer ID="TabContainer1" runat="server" ActiveTabIndex="2" 
            Height="350px" Width="316px">
       
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                <ContentTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
                    <asp:Button ID="Button1" runat="server" Text="Button" />
                </ContentTemplate>
            </cc1:TabPanel>
           
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                <ContentTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Height="68px"></asp:TextBox><br />
                    <asp:Button ID="Button2" runat="server" Text="Button" />
                </ContentTemplate>
            </cc1:TabPanel>
                      
            <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
                <ContentTemplate>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><br />
                    <asp:Button ID="Button3" runat="server" Text="Button" />
                </ContentTemplate>
            </cc1:TabPanel>
           
        </cc1:tabcontainer>
        <br />
   
    </div>

</asp:Content>

