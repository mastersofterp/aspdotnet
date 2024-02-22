<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RazorPay_Request.aspx.cs" Inherits="RazorPay_Request" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

 <%--   <script src="https://code.jquery.com/jquery-1.10.2.min.js"
        integrity="sha256-C6CB9UYIS9UJeqinPHWTHVqh/E1uhG5Twh+Y5qFQmYg="
        crossorigin="anonymous"  ></script>
       <script src="https://checkout.razorpay.com/v1/checkout.js"></script>--%>

     <script src="<%=Page.ResolveClientUrl("https://code.jquery.com/jquery-1.10.2.min.js")%>"></script>
     <script src="<%=Page.ResolveClientUrl("https://checkout.razorpay.com/v1/checkout.js")%>"></script>
  

</head>
<body>
    <form id="form1" runat="server" action="RazorPayOnlinePaymentResponse.aspx" method="post">
        <div>
            <asp:HiddenField ID="hdnKey" runat="server" Value="" />
            <asp:HiddenField ID="hdnMerchantName" runat="server" Value="" />
            <asp:HiddenField ID="hdnRazorId" runat="server" Value="" />
            <asp:HiddenField ID="hdnAmount" runat="server" Value="" />
            <asp:HiddenField ID="hdnStudentName" runat="server" Value="" />
            <asp:HiddenField ID="hdnStudentEmail" runat="server" Value="" />
            <asp:HiddenField ID="hdnMobileNumber" runat="server" Value="" />
            <asp:HiddenField ID="hdnCurrency" runat="server" Value="" />
            <asp:HiddenField ID="hdnPaymentId" runat="server" Value="" OnValueChanged="hdnPaymentId_ValueChanged" />
            <br />
            <center> <asp:Button ID="Button1" runat="server"  CssClass="btn-info"  Height="50px" Width="200px" ForeColor="White" Font-Bold="true" BackColor="Green" PostBackUrl="~/ACADEMIC/ONLINEFEECOLLECTION/RazorPayOnlinePaymentRequest.aspx" Text="Go Back To Dashboard" /></center>

            
           
            
        </div>
    </form>
</body>


<script>


    var amount = parseFloat($("#<%=hdnAmount.ClientID %>").val());
    // alert(amount);
    var amt = Math.round(amount * 100);
    //alert(amt);
    var options = {
        "key": $('#hdnKey').val(),
        "amount": parseInt(amt), // 2000 paise = INR 20h 
        "name": $('#hdnMerchantName').val(),
        "description": "Admission Fees",
        "order_id": "<%=orderId%>",
        //"image": "https://razorpay.com/favicon.png",
        "image": "../../Images/Login/Atlas_Logo.png",
        "handler": function (response) {

            //alert(response.razorpay_payment_id);
            $('#hdnPaymentId').val(response.razorpay_payment_id);

            // alert(<%=hdnPaymentId.UniqueID %>);

            setTimeout('__doPostBack(\'' + 'hdnPaymentId' + '\',\'\')', 0);

        },
        "prefill": {
            "name": $('#hdnStudentName').val(),
            "email": $('#hdnStudentEmail').val(),
            "contact": $('#hdnMobileNumber').val()
        },
        "notes": {
            "address": "NA",
            "merchant_order_id": "<%=MerchantId%>",
            "application_id": "<%=ApplicationId%>"

        },
        "theme": {
            "color": "#F37254"
        }
    };
    var rzp1 = new Razorpay(options);


    $(document).ready(function (e) {

        //alert($("#<%=hdnAmount.ClientID %>").val());
             rzp1.open();
             e.preventDefault();
         });
         //window.onbeforeunload = function () {
         //    return "You did not save your stuff";
         //   // window.location = "http://www.google.com";
         //}
</script>
</html>
