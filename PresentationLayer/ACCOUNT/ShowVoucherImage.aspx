<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowVoucherImage.aspx.cs" Inherits="ShowVoucherImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script language="javascript" type="text/javascript">
     function LoadImage()
    {
        document.getElementById("imgPhoto").src = document.getElementById("fuPhotoUpload").value;
    }
    
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Image ID="imgPhoto" runat="server" Width="785px" 
    Height="432px" />
    <br />
    <asp:FileUpload ID="fuPhotoUpload" runat="server" 
    TabIndex="19" onChange="LoadImage()" Width="212px" />
    <asp:Button ID="btnsave" Text="Save" runat="server" onclick="btnsave_Click" />
    <asp:Button ID="btncancel" Text="Cancel" runat="server" />
    
    </div>
    </form>
</body>
</html>
