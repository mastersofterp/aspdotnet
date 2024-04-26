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
   <%--<script src="<%=Page.ResolveClientUrl("https://checkout.razorpay.com/v1/checkout.js")%>"></script>--%>

</head>
<body>
 
     <form id="form1" runat="server" method="post" action="https://api.razorpay.com/v1/checkout/embedded">
            <input type="hidden" name="key_id" id="hdn_key_id" value=""  />
            <input type="hidden" name="amount" id="hdn_amount" value=""  />
            <input type="hidden" name="order_id" id="hdn_order_id" value="" />
            <input type="hidden" name="name" id="hdn_cmp_name" value="" />
            <input type="hidden" name="description" id="hdn_description" value="" />
            <input type="hidden" name="image" id="hdn_image" value="https://cdn.razorpay.com/logos/BUVwvgaqVByGp2_large.jpg" />
            <input type="hidden" name="prefill[name]" id="hdn_stud_name" value="" />
            <input type="hidden" name="prefill[contact]"  id="hdn_stud_contact" value="" />
            <input type="hidden" name="prefill[email]" id="hdn_stud_email" value="" />
            <input type="hidden" name="notes[shipping address]" id="hdn_stud_addr" value="" />
            <input type="hidden" name="callback_url" id="hdn_callback_url" value="" />
            <input type="hidden" name="cancel_url" id="hdn_cancel_url" value="" />
            <button id="submit" hidden >Submit</button>
   </form>

    <script type="text/javascript">  
        $("#hdn_key_id").val('<%=Session["RAZORPAYKEY"]%>');
        $("#hdn_amount").val('<%=Session["studAmt"]%>');
        $("#hdn_order_id").val('<%=Session["RazOrder_Id"]%>');
        $("#hdn_cmp_name").val('<%=Session["CollegeNm"]%>');
        $("#hdn_description").val('<%=Session["Desc"]%>');   // use for installment no.
        $("#hdn_stud_name").val('<%=Session["payStudName"]%>');
        $("#hdn_stud_contact").val('<%=Session["studPhone"]%>');
        $("#hdn_stud_email").val('<%=Session["studEmail"]%>');
        $("#hdn_stud_addr").val('<%=Session["studAddr"]%>');
        $("#hdn_callback_url").val('<%=Session["Callback_Url"]%>');
        $("#hdn_cancel_url").val('<%=Session["Cancel_url"]%>');

        $("#submit").trigger("click");
    </script>
</body>
   
</html>
