<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Call_Blob_Storage.aspx.cs" Inherits="Call_Blob_Storage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="downloadBlob" runat="server" Text="Download blob in Zip" OnClick="downloadBlob_Click" />
            <center>
                <h2>
                    Uploaded Files on Blob Storage
                    <asp:FileUpload runat="server" class="uploadlogo" ID="fuStudentPhoto1" accept=".jpg,.jpeg,.png" />
                    <asp:Button ID="btnUpload" runat="server" Text="Upload File" OnClick="btnUpload_Click" />
                </h2>
            </center>
            <center>

                <asp:GridView ID="gdvBlobs" runat="server" BackColor="#DEBA84"
    BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3"
    CellSpacing="2" onrowdeleting="gdvBlobs_RowDeleting"
    AutoGenerateColumns="False" Width="50%">
    <Columns>
        <asp:BoundField DataField="Id" HeaderText="Id" />
        
        <asp:TemplateField HeaderText="Image">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Image ID="imgBlob" runat="server"
                ImageUrl='<%# Eval("Url") %>' Height="50px" Width="50px"  />
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
    </Columns>
    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#FFF1D4" />
    <SortedAscendingHeaderStyle BackColor="#B95C30" />
    <SortedDescendingCellStyle BackColor="#F1E5CE" />
    <SortedDescendingHeaderStyle BackColor="#93451F" />
</asp:GridView>

            </center>


            

        </div>
    </form>
</body>
</html>
