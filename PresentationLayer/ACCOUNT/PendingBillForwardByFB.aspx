<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="PendingBillForwardByFB.aspx.cs" Inherits="ACCOUNT_PendingBillForwardByFB" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>--%>

    <style>
        hr {
            display: block;
            margin-top: 0.5em;
            margin-bottom: 0.5em;
            margin-left: auto;
            margin-right: auto;
            border-style: inset;
            border-width: 1px;
        }
    </style>
    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }

        function AskSave() {
            var str = "";
            if ($('#<%= ddlSelect.ClientID%> option:selected').val() == "F") {
                str = "Forward";
            }
            else {
                str = "Return";
            }

            if (confirm('Are you sure Want To ' + str.toString() + ' The Bill ? ') == true) {
                document.getElementById('ctl00_ContentPlaceHolder1_hdnAskSave').value = 1;
                return true;
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_hdnAskSave').value = 0;
                document.getElementById('ctl00_ContentPlaceHolder1_btnSave').disabled = false;
                return false;
            }
        }

    
    </script>


    <script type="text/javascript">
        function CopyAmount()
        {
          
            var TotalBill=parseFloat(document.getElementById('<%= txtBillAmt.ClientID %>').value).toFixed(2);
            document.getElementById('<%= txtBillAmt.ClientID %>').value= document.getElementById('<%=txtNetAmt.ClientID%>').value =document.getElementById('<%= txtTotalBillAmt.ClientID %>').value=TotalBill;
        }
    </script>



    <script type="text/javascript">
        function CalcTDS() {

            var BillAmt = document.getElementById('<%= txtBillAmt.ClientID %>');
            var chkTDS = document.getElementById('<%= chkTDSApplicable.ClientID%>').checked;
            var TotAmt = document.getElementById('<%= txtTotalBillAmt.ClientID %>');
         
            //added by vijay andoju on 09092020
          
            if (chkTDS) {
                TDSPer = document.getElementById('<%= txtTDSPer.ClientID %>');
                TDSAmt = document.getElementById('<%= txtTDSAmt.ClientID %>');
                if (BillAmt.value != "" && TDSPer.value != "") {

                    CalTDS = parseFloat(BillAmt.value) * parseFloat(TDSPer.value) * 0.01;
                   
                    TDSAmt.value = Math.round(parseFloat(CalTDS));
                    var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                    var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                    var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                    var TdsAmount = 0;
                    var CgstAmount = 0;
                    var SgstAmount = 0;
                    var IgstAmount = 0;
                    if(chkIGST4)
                    {
                        IgstAmount =  document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                    }
                    if(chkGST4)
                    {
                        CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value ;
                        SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                    }
                    var amountnet= parseFloat(parseFloat(BillAmt.value)+  parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));
                      
                    document.getElementById('<%=txtNetAmt .ClientID%>').value=parseFloat(amountnet)-parseFloat(TDSAmt.value);
                   
                }
            }
        }

    </script>

    <script type="text/javascript">
        function NetAmount() {
            
          
            var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
            var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
            var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
            var TdsAmount = 0;
            var CgstAmount = 0;
            var SgstAmount = 0;
            var IgstAmount = 0;

            if (chkTDS4) {
                TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;
            }
            if(chkIGST4)
            {
                IgstAmount =  document.getElementById('<%=txtIgstAmount.ClientID%>').value;
            }
            if(chkGST4)
            {
                CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value ;
                SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
            }
            

            var NetAmount = document.getElementById('<%=txtNetAmt.ClientID%>').value;
            document.getElementById('<%=txtGSTAmount.ClientID%>').value = parseFloat(parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));
            var Amount =parseFloat(parseFloat(document.getElementById('<%=txtGSTAmount.ClientID%>').value)+parseFloat(document.getElementById('<%=txtBillAmt.ClientID%>').value);

            NetAmount = parseFloat(parseFloat(Amount) - parseFloat(TdsAmount));



        }
    </script>

    <script type="text/javascript">


        function CalGST(val) {

      
            var chkGST3 = document.getElementById('<%= chkGST.ClientID%>').checked;
            var BillAmt3 = document.getElementById('<%= txtBillAmt.ClientID %>').value;

       
           
            CGSTPer3 = document.getElementById('<%=txtCGSTPER.ClientID%>').value;
          
            SGSTPer3 = document.getElementById('<%=txtSgstPer.ClientID%>').value;

            if(CGSTPer3=='')
            {
                CGSTPer3=0;
            }
            if(SGSTPer3=='')
            {
                SGSTPer3 =0;
            }
           
           
           
           
          

            if (chkGST3) {
                document.getElementById('<%= txtSgstAmount.ClientID %>').value =parseFloat(BillAmt3) * parseFloat(SGSTPer3) * 0.01;
                document.getElementById('<%= txtCgstAmount.ClientID %>').value =parseFloat(BillAmt3) * parseFloat(CGSTPer3) * 0.01;
               
                var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                var TdsAmount = 0;
                var CgstAmount = 0;
                var SgstAmount = 0;
                var IgstAmount = 0;

                if (chkTDS4) {
                    TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;
                }
                if(chkIGST4)
                {
                    IgstAmount =  document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                }
                if(chkGST4)
                {
                   
                    CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value ;
                    
                    SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                    
                }
            
                var amountnet= parseFloat(parseFloat(BillAmt3)+  parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));
                      
                document.getElementById('<%=txtNetAmt.ClientID%>').value= amountnet-TdsAmount ;
                document.getElementById("<%=txtGSTAmount.ClientID%>").value=parseFloat( parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));
            }
        }
    </script>

    <script type="text/javascript">


        function CalGST(val) {

            debugger;
            var chkGST3 = document.getElementById('<%= chkGST.ClientID%>').checked;
            var BillAmt3 = document.getElementById('<%= txtBillAmt.ClientID %>').value;

            if(document.getElementById('<%=txtSgstPer.ClientID%>').value=='')
            {
                document.getElementById('<%=txtSgstPer.ClientID%>').value='0';
            }

       
            document.getElementById('<%=txtCGSTPER.ClientID%>').value=parseFloat(document.getElementById('<%=txtCGSTPER.ClientID%>').value).toFixed(2);
            CGSTPer3 = document.getElementById('<%=txtCGSTPER.ClientID%>').value;
            document.getElementById('<%=txtSgstPer.ClientID%>').value=parseFloat(document.getElementById('<%=txtSgstPer.ClientID%>').value).toFixed(2);
            SGSTPer3 = document.getElementById('<%=txtSgstPer.ClientID%>').value;

            if(CGSTPer3=='')
            {
                CGSTPer3=0;
            }
            if(SGSTPer3=='')
            {
                SGSTPer3 =0;
            }
           
           
           
           
          

            if (chkGST3) {
                document.getElementById('<%= txtSgstAmount.ClientID %>').value =parseFloat( parseFloat(BillAmt3).toFixed(2) * parseFloat(SGSTPer3).toFixed(2) * 0.01).toFixed(2);
                document.getElementById('<%= txtCgstAmount.ClientID %>').value =parseFloat(parseFloat(BillAmt3).toFixed(2) * parseFloat(CGSTPer3).toFixed(2) * 0.01).toFixed(2);
               
                var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                var TdsAmount = 0;
                var CgstAmount = 0;
                var SgstAmount = 0;
                var IgstAmount = 0;

                if (chkTDS4) {
                    TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;
                }
                if(chkIGST4)
                {
                    IgstAmount =  document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                }
                if(chkGST4)
                {
                   
                    CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value ;
                    
                    SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                    
                }
            
                var amountnet= parseFloat(parseFloat(BillAmt3)+  parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));
                      
                document.getElementById('<%=txtNetAmt.ClientID%>').value=parseFloat( parseFloat(amountnet).toFixed(2)-parseFloat(TdsAmount).toFixed(2) ).toFixed(2);
                document.getElementById("<%=txtGSTAmount.ClientID%>").value=parseFloat( parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount)).toFixed(2);
            }
        }
    </script>

    <script type="text/javascript">

        function CalIGST() {
          
            var chkIGST2 = document.getElementById('<%= chkIGST.ClientID%>').checked;
            var BillAmt2 = document.getElementById('<%= txtBillAmt.ClientID %>');
            document.getElementById('<%=txtIgstAmount.ClientID%>').value=parseFloat(document.getElementById('<%=txtIgstAmount.ClientID%>').value).toFixed(2);
            if (chkIGST2) {
                IGSTAmt2 = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                document.getElementById('<%=txtIgstPer.ClientID%>').value=parseFloat( document.getElementById('<%=txtIgstPer.ClientID%>').value).toFixed(2);
                IGSTPer2 = document.getElementById('<%=txtIgstPer.ClientID%>').value;
                CalIGST2 = parseFloat(BillAmt2.value) * parseFloat(IGSTPer2) * 0.01;
                document.getElementById('<%=txtIgstAmount.ClientID%>').value =parseFloat(CalIGST2).toFixed(2);
                document.getElementById('<%=txtGSTAmount.ClientID%>').value = parseFloat(CalIGST2).toFixed(2);
               
                var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                var TdsAmount = 0;
                var CgstAmount = 0;
                var SgstAmount = 0;
                var IgstAmount = 0;

                if (chkTDS4) {
                    TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;
                }
                if(chkIGST4)
                {
                    IgstAmount =  document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                }
                if(chkGST4)
                {
                    CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value ;
                    SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                }
            
                var amountnet= parseFloat(parseFloat(BillAmt2.value)+  parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount)).toFixed(2);
                      
                document.getElementById('<%=txtNetAmt.ClientID%>').value=parseFloat( amountnet-TdsAmount ).toFixed(2);
            }
        }




    </script>

    <script type="text/javascript">




        function Per(val, con) {
           
            var BillAmount =parseFloat( document.getElementById('<%=txtBillAmt.ClientID%>').value).toFixed(2);
            var per =parseFloat( BillAmount / 100).toFixed(2);
            if (con == "1") {


                document.getElementById('<%=txtTDSAmt.ClientID%>').value =parseFloat( per * val).toFixed(2);
                document.getElementById('<%=txtNetAmt.ClientID%>').value =parseFloat(parseFloat( document.getElementById('<%=txtNetAmt.ClientID%>').value).toFixed(2) -parseFloat( document.getElementById('<%=txtTDSAmt.ClientID%>').value).toFixed(2)).toFixed(2);

            }
            if (con == "2") {


                document.getElementById('<%=txtIgstAmount.ClientID%>').value =parseFloat( per * val);
                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(document.getElementById('<%=txtNetAmt.ClientID%>').value) + parseFloat(document.getElementById('<%=txtIgstAmount.ClientID%>').value);
                document.getElementById('<%=txtGSTAmount.ClientID%>').value =parseFloat( document.getElementById('<%=txtIgstAmount.ClientID%>').value)
            }
            if (con == "3") {
                document.getElementById('<%=txtCgstAmount.ClientID%>').value =parseFloat( per * val);
                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(document.getElementById('<%=txtNetAmt.ClientID%>').value) + parseFloat(document.getElementById('<%=txtCgstAmount.ClientID%>').value);
                document.getElementById('<%=txtGSTAmount.ClientID%>').value =parseFloat( document.getElementById('<%=txtCgstAmount.ClientID%>').value);
            }
            if (con == "4") {
                document.getElementById('<%=txtSgstAmount.ClientID%>').value =parseFloat( per * val);
                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(document.getElementById('<%=txtNetAmt.ClientID%>').value) + parseFloat(document.getElementById('<%=txtSgstAmount.ClientID%>').value);
                document.getElementById('<%=txtGSTAmount.ClientID%>').value = parseFloat(document.getElementById('<%=txtGSTAmount.ClientID%>').value) + parseFloat(document.getElementById('<%=txtSgstAmount.ClientID%>').value);
            }


        }
    </script>

    <script type="text/javascript">
        function CheckGstinNo()
        {
      
            var GstInno=document.getElementById('<%=txtGSTINNo.ClientID%>').value;
      
            var GstLength=GstInno.length;
        
            if(GstLength<15)
            {
                alert('GSTIN No Number Must be 15 Characters:');
                document.getElementById('<%=txtGSTINNo.ClientID%>').value='';
            }
        }
    </script>


    <script type="text/javascript">
        function SHOPOPUP(vall) {
           
            var myArr = new Array();
            myString = "" + vall.id + "";
            myArr = myString.split("_");
            var index=myArr[0]+'_'+myArr[1]+'_'+myArr[2]+'_'+myArr[3]+'_'+'lblBillNo';
            var Id=document.getElementById(index).innerText;
            var popUrl = 'Acc_TrackPaymentBill.aspx?obj='+Id;
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=no,' +
                                  'status=no,toolbar=no,titlebar=no,' +
                                  'left=50,top=35,width=900px,height=650px';
            var openWindow = window.open(popUrl, name, appearence);
            //var openWindow = window.showModalDialog(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }
    </script>
    <script type="text/javascript">
        function SHOPOPUP(vall) {
            debugger;
            var myArr = new Array();
            myString = "" + vall.id + "";
            myArr = myString.split("_");
            var index=myArr[0]+'_'+myArr[1]+'_'+myArr[2]+'_'+myArr[3]+'_'+'lblBillNo';
            var Id=document.getElementById(index).innerText;
            var popUrl = 'Acc_TrackPaymentBill.aspx?obj='+Id;
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=no,' +
                                  'status=no,toolbar=no,titlebar=no,' +
                                  'left=50,top=35,width=900px,height=650px';
            var openWindow = window.open(popUrl, name, appearence);
            //var openWindow = window.showModalDialog(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }
    </script>

    <%-- ===================================================================================================================================================================================================--%>

    <script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table2').DataTable();
        });

        function ServiceFunction(val) {
            // var servicename = document.getElementById  //txtServiceName
            var payeename = document.getElementById('<%= txtPayeeNameAddress.ClientID%>');

            payeename.value = servicename.value;
        }

        function TDSAmountCopy(val) {
            var TDSAmt = document.getElementById('<%= txtTDSAmt.ClientID%>');
            var hdnTDSAmt = document.getElementById('<%= hdntxtTDSAmt.ClientID%>');

            hdnTDSAmt.value = TDSAmt.value;
            CalTotDedNetAmtOnChangeinTDSManually(val);
            //alert(hdnTDSAmt.value);
        }

        function TDSonCGSTAmountCopy(val) {
            var TDSAmt = document.getElementById('<%= txtTDSonCGSTAmount .ClientID%>');
            var hdnTDSAmt = document.getElementById('<%= hdntxtTDSonCGSTAmount .ClientID%>');


            CalTotDedNetAmtOnChangeinTDSonCGSTManually(val);
            hdnTDSAmt.value = TDSAmt.value;

            //alert(hdnTDSAmt.value);
        }

        function TDSonSGSTAmountCopy(val) {
            var TDSAmt = document.getElementById('<%= txtTDSonSGSTAmount .ClientID%>');
            var hdnTDSAmt = document.getElementById('<%= hdntxtTDSonSGSTAmount .ClientID%>');


            CalTotDedNetAmtOnChangeinTDSonSGSTManually(val);
            hdnTDSAmt.value = TDSAmt.value;
            //alert(hdnTDSAmt.value);
        }

        function TDSonIGSTAmountCopy(val) {
            var TDSAmt = document.getElementById('<%= txtTDSonGSTAmount .ClientID%>');
            var hdnTDSAmt = document.getElementById('<%= hdntxtTDSonGSTAmount .ClientID%>');
            CalTotDedNetAmtOnChangeinTDSonIGSTManually(val);
            hdnTDSAmt.value = TDSAmt.value;

            //alert(hdnTDSAmt.value);
        }
    </script>

    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }

        function AskSave() {

            if (document.getElementById('<%=ddlSelectCompany.ClientID%>').value == 0) {
                alert('Please Select Company.');
                return false;
            }
            if (document.getElementById('<%=ddlAccount.ClientID%>').value == 0) {
                alert('Please Select Account.');
                return false;
            }
            if (document.getElementById('<%=ddlDeptBranch.ClientID%>').value == 0) {
                alert('Please Select Department/Branch.');
                return false;
            }


            if (document.getElementById('<%=txtBillAmt.ClientID%>').value == '') {
                alert('Please Enter Bill Amount.');
                return false;
            }

            //Gst Fields Validation
            var chkGst = document.getElementById('<%=chkGST.ClientID%>');
            if (chkGst.checked) {
                if (document.getElementById('<%=txtCGSTPER.ClientID%>').value == '' || document.getElementById('<%=txtCGSTPER.ClientID%>').value == 0) {
                    alert('Please Enter CGST Per(%).');
                    return false;
                }
                if (document.getElementById('<%=txtCgstAmount.ClientID%>').value == '' || document.getElementById('<%=txtCgstAmount.ClientID%>').value == 0) {
                    alert('Please Enter CGST Amount.');
                    return false;
                }
                if (document.getElementById('<%=txtSgstPer.ClientID%>').value == '' || document.getElementById('<%=txtSgstPer.ClientID%>').value == 0) {
                    alert('Please Enter SGST Per(%).');
                    return false;
                }
                if (document.getElementById('<%=txtSgstAmount.ClientID%>').value == '' || document.getElementById('<%=txtSgstAmount.ClientID%>').value == 0) {
                    alert('Please Enter SGST Amount.');
                    return false;
                }

                document.getElementById('<%=hdnGSTAmount.ClientID%>').value = document.getElementById('<%=txtGSTAmount.ClientID%>').value;
            }

            //Tds On Gst Fields Validation
            var chkCGstSGSt = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>');
            if (chkCGstSGSt.checked) {
                if (document.getElementById('<%=txtTDSCGSTonAmount.ClientID%>').value == '' || document.getElementById('<%=txtTDSCGSTonAmount.ClientID%>').value == 0) {
                    alert('Please Enter TDS(CGST) On Amount.');
                    return false;
                }
                if (document.getElementById('<%=ddlTDSonCGSTSection.ClientID%>').value == 0) {
                    alert('Please Select Section of Tds On CGST.');
                    return false;
                }
                if (document.getElementById('<%=txtTDSonCGSTPer.ClientID%>').value == '' || document.getElementById('<%=txtTDSonCGSTPer.ClientID%>').value == 0) {
                    alert('Please Enter Per(%) of Tds On CGST.');
                    return false;
                }
                if (document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value == '' || document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value == 0) {
                    alert('Please Enter TDS On CGST Amount.');
                    return false;
                }
                //
                if (document.getElementById('<%=txtTDSSGSTonAmount.ClientID%>').value == '' || document.getElementById('<%=txtTDSSGSTonAmount.ClientID%>').value == 0) {
                    alert('Please Enter TDS(SGST) On Amount.');
                    return false;
                }
                if (document.getElementById('<%=ddlTDSonSGSTSection.ClientID%>').value == 0) {
                    alert('Please Select Section of Tds On SGST.');
                    return false;
                }
                if (document.getElementById('<%=txtTDSonSGSTPer.ClientID%>').value == '' || document.getElementById('<%=txtTDSonSGSTPer.ClientID%>').value == 0) {
                    alert('Please Enter Per(%) of Tds On SGST.');
                    return false;
                }
                if (document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value == '' || document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value == 0) {
                    alert('Please Enter TDS On SGST Amount.');
                    return false;
                }
                document.getElementById('<%=hdnTDSonCGSTAmount.ClientID%>').value = document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value;
            }
            //IGST Fields Validation
            var chkIGST = document.getElementById('<%=chkIGST.ClientID%>');
            if (chkIGST.checked) {
                if (document.getElementById('<%=txtIgstPer.ClientID%>').value == '' || document.getElementById('<%=txtIgstPer.ClientID%>').value == 0) {
                    alert('Please Enter IGST Per(%).');
                    return false;
                }
                if (document.getElementById('<%=txtIgstAmount.ClientID%>').value == '' || document.getElementById('<%=txtIgstAmount.ClientID%>').value == 0) {
                    alert('Please Enter IGST Amount.');
                    return false;
                }
                document.getElementById('<%=hdnGSTAmount.ClientID%>').value = document.getElementById('<%=txtGSTAmount.ClientID%>').value;
            }

            //Tds On IGST Fields Validation 

            var chkTDSOnGst = document.getElementById('<%=chkTDSOnGst.ClientID%>');
            if (chkTDSOnGst.checked) {
                if (document.getElementById('<%=txtTDSGSTonAmount.ClientID%>').value == '' || document.getElementById('<%=txtTDSGSTonAmount.ClientID%>').value == 0) {
                    alert('Please Enter TDS(IGST) On Amount.');
                    return false;
                }
                if (document.getElementById('<%=ddlTDSonGSTSection.ClientID%>').value == 0) {
                    alert('Please Select Section of Tds On IGST.');
                    return false;
                }
                if (document.getElementById('<%=txtTDSonGSTPer.ClientID%>').value == '' || document.getElementById('<%=txtTDSonGSTPer.ClientID%>').value == 0) {
                    alert('Please Enter Per(%) of Tds On IGST.');
                    return false;
                }
                if (document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value == '' || document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value == 0) {
                    alert('Please Enter TDS On IGST Amount.');
                    return false;
                }
            }



            if (confirm('Do you want to submit the Bill?') == true) {
                document.getElementById('ctl00_ContentPlaceHolder1_hdnAskSave').value = 1;
                return true;
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_hdnAskSave').value = 0;
                document.getElementById('ctl00_ContentPlaceHolder1_btnSubmit').disabled = false;
                return false;
            }
        }

    </script>

    <script type="text/javascript">
        function CopyAmount() {

            var TotalBill = parseFloat(document.getElementById('<%= txtBillAmt.ClientID %>').value).toFixed(2);
            document.getElementById('<%= txtBillAmt.ClientID %>').value = document.getElementById('<%=txtNetAmt.ClientID%>').value = document.getElementById('<%= txtTotalBillAmt.ClientID %>').value = TotalBill;
        }

        function CopyAmount2() {

            var TotalBill = parseFloat(document.getElementById('<%= txtBillAmt.ClientID %>').value).toFixed(2);
            document.getElementById('<%= txtTdsOnAmt.ClientID %>').value = parseFloat(TotalBill).toFixed(2);

        }
    </script>


    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }

        function AskSave() {

            if (document.getElementById('<%=ddlSelectCompany.ClientID%>').value == 0) {
                alert('Please Select Company.');
                return false;
            }
            if (document.getElementById('<%=ddlAccount.ClientID%>').value == 0) {
                alert('Please Select Account.');
                return false;
            }
            if (document.getElementById('<%=ddlDeptBranch.ClientID%>').value == 0) {
                alert('Please Select Department/Branch.');
                return false;
            }


            if (document.getElementById('<%=txtBillAmt.ClientID%>').value == '') {
                alert('Please Enter Bill Amount.');
                return false;
            }

            //Gst Fields Validation
            var chkGst = document.getElementById('<%=chkGST.ClientID%>');
            if (chkGst.checked) {
                if (document.getElementById('<%=txtCGSTPER.ClientID%>').value == '' || document.getElementById('<%=txtCGSTPER.ClientID%>').value == 0) {
                    alert('Please Enter CGST Per(%).');
                    return false;
                }
                if (document.getElementById('<%=txtCgstAmount.ClientID%>').value == '' || document.getElementById('<%=txtCgstAmount.ClientID%>').value == 0) {
                    alert('Please Enter CGST Amount.');
                    return false;
                }
                if (document.getElementById('<%=txtSgstPer.ClientID%>').value == '' || document.getElementById('<%=txtSgstPer.ClientID%>').value == 0) {
                    alert('Please Enter SGST Per(%).');
                    return false;
                }
                if (document.getElementById('<%=txtSgstAmount.ClientID%>').value == '' || document.getElementById('<%=txtSgstAmount.ClientID%>').value == 0) {
                    alert('Please Enter SGST Amount.');
                    return false;
                }

                document.getElementById('<%=hdnGSTAmount.ClientID%>').value = document.getElementById('<%=txtGSTAmount.ClientID%>').value;
            }

            //Tds On Gst Fields Validation
            var chkCGstSGSt = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>');
            if (chkCGstSGSt.checked) {
                if (document.getElementById('<%=txtTDSCGSTonAmount.ClientID%>').value == '' || document.getElementById('<%=txtTDSCGSTonAmount.ClientID%>').value == 0) {
                    alert('Please Enter TDS(CGST) On Amount.');
                    return false;
                }
                if (document.getElementById('<%=ddlTDSonCGSTSection.ClientID%>').value == 0) {
                    alert('Please Select Section of Tds On CGST.');
                    return false;
                }
                if (document.getElementById('<%=txtTDSonCGSTPer.ClientID%>').value == '' || document.getElementById('<%=txtTDSonCGSTPer.ClientID%>').value == 0) {
                    alert('Please Enter Per(%) of Tds On CGST.');
                    return false;
                }
                if (document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value == '' || document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value == 0) {
                    alert('Please Enter TDS On CGST Amount.');
                    return false;
                }
                //
                if (document.getElementById('<%=txtTDSSGSTonAmount.ClientID%>').value == '' || document.getElementById('<%=txtTDSSGSTonAmount.ClientID%>').value == 0) {
                    alert('Please Enter TDS(SGST) On Amount.');
                    return false;
                }
                if (document.getElementById('<%=ddlTDSonSGSTSection.ClientID%>').value == 0) {
                    alert('Please Select Section of Tds On SGST.');
                    return false;
                }
                if (document.getElementById('<%=txtTDSonSGSTPer.ClientID%>').value == '' || document.getElementById('<%=txtTDSonSGSTPer.ClientID%>').value == 0) {
                    alert('Please Enter Per(%) of Tds On SGST.');
                    return false;
                }
                if (document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value == '' || document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value == 0) {
                    alert('Please Enter TDS On SGST Amount.');
                    return false;
                }
                document.getElementById('<%=hdnTDSonCGSTAmount.ClientID%>').value = document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value;
            }
            //IGST Fields Validation
            var chkIGST = document.getElementById('<%=chkIGST.ClientID%>');
            if (chkIGST.checked) {
                if (document.getElementById('<%=txtIgstPer.ClientID%>').value == '' || document.getElementById('<%=txtIgstPer.ClientID%>').value == 0) {
                    alert('Please Enter IGST Per(%).');
                    return false;
                }
                if (document.getElementById('<%=txtIgstAmount.ClientID%>').value == '' || document.getElementById('<%=txtIgstAmount.ClientID%>').value == 0) {
                    alert('Please Enter IGST Amount.');
                    return false;
                }
                document.getElementById('<%=hdnGSTAmount.ClientID%>').value = document.getElementById('<%=txtGSTAmount.ClientID%>').value;
            }

            //Tds On IGST Fields Validation 

            var chkTDSOnGst = document.getElementById('<%=chkTDSOnGst.ClientID%>');
            if (chkTDSOnGst.checked) {
                if (document.getElementById('<%=txtTDSGSTonAmount.ClientID%>').value == '' || document.getElementById('<%=txtTDSGSTonAmount.ClientID%>').value == 0) {
                    alert('Please Enter TDS(IGST) On Amount.');
                    return false;
                }
                if (document.getElementById('<%=ddlTDSonGSTSection.ClientID%>').value == 0) {
                    alert('Please Select Section of Tds On IGST.');
                    return false;
                }
                if (document.getElementById('<%=txtTDSonGSTPer.ClientID%>').value == '' || document.getElementById('<%=txtTDSonGSTPer.ClientID%>').value == 0) {
                    alert('Please Enter Per(%) of Tds On IGST.');
                    return false;
                }
                if (document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value == '' || document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value == 0) {
                    alert('Please Enter TDS On IGST Amount.');
                    return false;
                }
            }

            //IGST Fields Validation




            if (confirm('Do you want to submit the Bill?') == true) {
                document.getElementById('ctl00_ContentPlaceHolder1_hdnAskSave').value = 1;
                return true;
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_hdnAskSave').value = 0;
                document.getElementById('ctl00_ContentPlaceHolder1_btnSubmit').disabled = false;
                return false;
            }
        }

    </script>
    <script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table2').DataTable();
        });

        function ServiceFunction(val) {
            // var servicename = document.getElementById  //txtServiceName
            var payeename = document.getElementById('<%= txtPayeeNameAddress.ClientID%>');

            payeename.value = servicename.value;
        }

        function TDSAmountCopy(val) {
            var TDSAmt = document.getElementById('<%= txtTDSAmt.ClientID%>');
            var hdnTDSAmt = document.getElementById('<%= hdntxtTDSAmt.ClientID%>');

            hdnTDSAmt.value = TDSAmt.value;
            CalTotDedNetAmtOnChangeinTDSManually(val);
            //alert(hdnTDSAmt.value);
        }

        function TDSonCGSTAmountCopy(val) {
            var TDSAmt = document.getElementById('<%= txtTDSonCGSTAmount .ClientID%>');
            var hdnTDSAmt = document.getElementById('<%= hdntxtTDSonCGSTAmount .ClientID%>');


            CalTotDedNetAmtOnChangeinTDSonCGSTManually(val);
            hdnTDSAmt.value = TDSAmt.value;

            //alert(hdnTDSAmt.value);
        }

        function TDSonSGSTAmountCopy(val) {
            var TDSAmt = document.getElementById('<%= txtTDSonSGSTAmount .ClientID%>');
            var hdnTDSAmt = document.getElementById('<%= hdntxtTDSonSGSTAmount .ClientID%>');


            CalTotDedNetAmtOnChangeinTDSonSGSTManually(val);
            hdnTDSAmt.value = TDSAmt.value;
            //alert(hdnTDSAmt.value);
        }

        function TDSonIGSTAmountCopy(val) {
            var TDSAmt = document.getElementById('<%= txtTDSonGSTAmount .ClientID%>');
            var hdnTDSAmt = document.getElementById('<%= hdntxtTDSonGSTAmount .ClientID%>');
            CalTotDedNetAmtOnChangeinTDSonIGSTManually(val);
            hdnTDSAmt.value = TDSAmt.value;

            //alert(hdnTDSAmt.value);
        }
    </script>
    <script type="text/javascript">
        function CopyAmount() {
            var TotalBill = parseFloat(document.getElementById('<%= txtBillAmt.ClientID %>').value).toFixed(2);
            document.getElementById('<%= txtTDSSGSTonAmount.ClientID %>').value = document.getElementById('<%= txtTDSCGSTonAmount.ClientID %>').value = document.getElementById('<%= txtTDSGSTonAmount.ClientID %>').value = document.getElementById('<%= txtTdsOnAmt.ClientID %>').value = document.getElementById('<%= txtBillAmt.ClientID %>').value = document.getElementById('<%=txtNetAmt.ClientID%>').value = document.getElementById('<%= txtTotalBillAmt.ClientID %>').value = Number(TotalBill).toFixed(2);


        }
        function CopyAmount2() {

            var TotalBill = parseFloat(document.getElementById('<%= txtBillAmt.ClientID %>').value).toFixed(2);
            document.getElementById('<%= txtTdsOnAmt.ClientID %>').value = parseFloat(TotalBill).toFixed(2);

        }
    </script>
    <script type="text/javascript">
        function CalcTDS() {

            var BillAmt = document.getElementById('<%= txtBillAmt.ClientID %>');
            var chkTDS = document.getElementById('<%= chkTDSApplicable.ClientID%>').checked;
            var TotAmt = document.getElementById('<%= txtTotalBillAmt.ClientID %>');

            //added by vijay andoju on 09092020

            if (chkTDS) {
                TDSPer = document.getElementById('<%= txtTDSPer.ClientID %>');
                TDSAmt = document.getElementById('<%= txtTDSAmt.ClientID %>');
                if (BillAmt.value != "" && TDSPer.value != "") {

                    CalTDS = parseFloat(BillAmt.value) * parseFloat(TDSPer.value) * 0.01;

                    TDSAmt.value = Math.round(parseFloat(CalTDS));
                    var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                    var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                    var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                    var chkTDSonGST4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                    var chkTDSonCGSTSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;

                    var SecurityAmount = 0;
                    var TdsAmount = 0;
                    var TdsonCGSTAmount = 0;
                    var TdsonSGSTAmount = 0;
                    var TdsonGSTAmount = 0;
                    var CgstAmount = 0;
                    var SgstAmount = 0;
                    var IgstAmount = 0;
                    if (chkTDSonCGSTSGST4) {
                        TdsonCGSTAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value != '' ? document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value : 0;
                        TdsonSGSTAmount = document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value != '' ? document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value : 0;
                    }
                    if (chkIGST4) {
                        IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value != '' ? document.getElementById('<%=txtIgstAmount.ClientID%>').value : 0;
                    }
                    if (chkGST4) {
                        CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value != '' ? document.getElementById('<%=txtCgstAmount.ClientID%>').value : 0;
                        SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value != '' ? document.getElementById('<%=txtSgstAmount.ClientID%>').value : 0;
                    }
                    if (chkTDSonGST4) {
                        TdsonGSTAmount = document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value != '' ? document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value : 0;
                    }

                    var amountnet = parseFloat(parseFloat(BillAmt.value) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));

                    document.getElementById('<%=txtNetAmt .ClientID%>').value = parseFloat(amountnet) - parseFloat(parseFloat(TDSAmt.value) - parseFloat(TdsonGSTAmount) - parseFloat(TdsonCGSTAmount) - parseFloat(TdsonSGSTAmount) - parseFloat(SecurityAmount)).toFixed(2);
                    document.getElementById('<%=txtTotalTDSAmt .ClientID%>').value = parseFloat(parseFloat(TDSAmt.value) + parseFloat(TdsonGSTAmount) + parseFloat(TdsonCGSTAmount) + parseFloat(TdsonSGSTAmount) + parseFloat(SecurityAmount)).toFixed(2);

                }
            }
        }

        function CalcTDSonGst() {

            var BillAmt = document.getElementById('<%= txtBillAmt.ClientID %>');
            var chkTDSonGST = document.getElementById('<%= chkTDSOnGst.ClientID%>').checked;
            var TotAmt = document.getElementById('<%= txtTotalBillAmt.ClientID %>');

            //added by vijay andoju on 09092020

            if (chkTDSonGST) {
                TDSonGSTPer = document.getElementById('<%= txtTDSonGSTPer.ClientID %>');
                TDSonGSTAmt = document.getElementById('<%= txtTDSonGSTAmount.ClientID %>');
                if (BillAmt.value != "" && TDSonGSTPer.value != "") {

                    CalTDS = parseFloat(BillAmt.value) * parseFloat(TDSonGSTPer.value) * 0.01;

                    TDSonGSTAmt.value = Math.round(parseFloat(CalTDS));
                    var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                    var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                    var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked
                    var chkTDSonGST4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                    var chkTDSonCGSTSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;

                    var SecurityAmount = 0;
                    var TdsonCGSTAmount = 0;
                    var TdsonSGSTAmount = 0;

                    var TdsAmount = 0;
                    var CgstAmount = 0;
                    var SgstAmount = 0;
                    var IgstAmount = 0;
                    if (chkIGST4) {
                        IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                    }
                    if (chkTDSonCGSTSGST4) {
                        TdsonCGSTAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
                        TdsonSGSTAmount = document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value;
                    }
                    if (chkGST4) {
                        CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value;
                        SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                    }

                    if (chkTDS4) {
                        TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;

                    }

                    var amountnet = parseFloat(parseFloat(BillAmt.value) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));
                    document.getElementById('<%=txtTotalTDSAmt.ClientID%>').value = parseFloat(parseFloat(TDSonGSTAmt.value) + parseFloat(TdsAmount) + parseFloat(SecurityAmount) + parseFloat(TdsonCGSTAmount) + parseFloat(TdsonSGSTAmount)).toFixed(2);
                    document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(parseFloat(amountnet) - parseFloat(TDSonGSTAmt.value) - parseFloat(TdsAmount) - parseFloat(SecurityAmount) - parseFloat(TdsonCGSTAmount) - parseFloat(TdsonSGSTAmount)).toFixed(2);

                    //document.getElementById('<%=txtTotalTDSAmt.ClientID%>').value = parseFloat(parseFloat(TDSonGSTAmt.value) - parseFloat(TdsAmount) - parseFloat(SecurityAmount) - parseFloat(TdsonCGSTAmount) - parseFloat(TdsonSGSTAmount));


                }
            }
        }

        function CalcTDSonCGst() {

            var BillAmt = document.getElementById('<%= txtBillAmt.ClientID %>');
            var chkTDSonCGSTSGST = document.getElementById('<%= chkTdsOnCGSTSGST.ClientID%>').checked;
            var TotAmt = document.getElementById('<%= txtTotalBillAmt.ClientID %>');

            //added by vijay andoju on 09092020

            if (chkTDSonGST) {
                TDSonGSTPer = document.getElementById('<%= txtTDSonCGSTPer.ClientID %>');
                TDSonGSTAmt = document.getElementById('<%= txtTDSonGSTAmount.ClientID %>');
                if (BillAmt.value != "" && TDSonGSTPer.value != "") {

                    CalTDS = parseFloat(BillAmt.value) * parseFloat(TDSonGSTPer.value) * 0.01;

                    TDSonGSTAmt.value = Math.round(parseFloat(CalTDS));
                    var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                    var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                    var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked
                    var chkTDSonGST4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                    var chkTDSonCGSTSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;

                    var SecurityAmount = 0;
                    var TdsAmount = 0;
                    var TdsOnCGSTAmount = 0;
                    var TdsOnSGSTAmount = 0;
                    var CgstAmount = 0;
                    var SgstAmount = 0;
                    var IgstAmount = 0;
                    if (chkTDSonCGSTSGST4) {
                        TdsOnCGSTAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
                    }
                    if (chkTDSonCGSTSGST4) {
                        TdsOnSGSTAmount = document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value;
                    }

                    if (chkIGST4) {
                        IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                    }
                    if (chkGST4) {
                        CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value;
                        SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                    }
                    if (chkTDS4) {
                        TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;

                    }
                    var amountnet = parseFloat(parseFloat(BillAmt.value) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));

                    document.getElementById('<%=txtNetAmt .ClientID%>').value = parseFloat(amountnet) - parseFloat(parseFloat(TDSonGSTAmt.value) - parseFloat(TdsAmount) - parseFloat(TdsOnCGSTAmount) - parseFloat(TdsOnSGSTAmount) - parseFloat(SecurityAmount)).toFixed(2);
                    document.getElementById('<%=txtTotalTDSAmt .ClientID%>').value = parseFloat(parseFloat(TDSonGSTAmt.value) + parseFloat(TdsAmount) + parseFloat(TdsOnCGSTAmount) + parseFloat(TdsOnSGSTAmount) + parseFloat(SecurityAmount)).toFixed(2);

                }
            }
        }

        function CalcTDSonSGst() {

            var BillAmt = document.getElementById('<%= txtBillAmt.ClientID %>');
            var chkTDSonCGSTSGST = document.getElementById('<%= chkTdsOnCGSTSGST.ClientID%>').checked;
            var TotAmt = document.getElementById('<%= txtTotalBillAmt.ClientID %>');

            //added by vijay andoju on 09092020

            if (chkTDSonGST) {
                TDSonGSTPer = document.getElementById('<%= txtTDSonSGSTPer.ClientID %>');
                TDSonGSTAmt = document.getElementById('<%= txtTDSonGSTAmount.ClientID %>');
                if (BillAmt.value != "" && TDSonGSTPer.value != "") {

                    CalTDS = parseFloat(BillAmt.value) * parseFloat(TDSonGSTPer.value) * 0.01;

                    TDSonGSTAmt.value = Math.round(parseFloat(CalTDS));
                    var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                    var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                    var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked
                    var chkTDSonGST4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                    var chkTDSonCGSTSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;


                    var SecurityAmount = 0;
                    var TdsAmount = 0;
                    var TdsOnCGSTAmount = 0;
                    var TdsOnSGSTAmount = 0;
                    var CgstAmount = 0;
                    var SgstAmount = 0;
                    var IgstAmount = 0;
                    if (chkTDSonCGSTSGST4) {
                        TdsOnCGSTAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
                    }

                    if (chkTDSonCGSTSGST4) {
                        TdsOnSGSTAmount = document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value;
                    }
                    if (chkIGST4) {
                        IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                    }
                    if (chkGST4) {
                        CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value;
                        SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                    }
                    if (chkTDS4) {
                        TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;

                    }
                    var amountnet = parseFloat(parseFloat(BillAmt.value) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));

                    document.getElementById('<%=txtNetAmt .ClientID%>').value = parseFloat(amountnet) - parseFloat(parseFloat(TDSonGSTAmt.value) - parseFloat(TdsAmount) - parseFloat(TdsOnCGSTAmount) - parseFloat(TdsOnSGSTAmount) - parseFloat(SecurityAmount)).toFixed(2);
                    document.getElementById('<%=txtTotalTDSAmt .ClientID%>').value = parseFloat(parseFloat(TDSonGSTAmt.value) + parseFloat(TdsAmount) + parseFloat(TdsOnCGSTAmount) + parseFloat(TdsOnSGSTAmount) + parseFloat(SecurityAmount)).toFixed(2);

                }
            }
        }
    </script>
    <script type="text/javascript">
        function NetAmount() {


            var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
            var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
            var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
            var TdsAmount = 0;
            var CgstAmount = 0;
            var SgstAmount = 0;
            var IgstAmount = 0;

            if (chkTDS4) {
                TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;
            }
            if (chkIGST4) {
                IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
            }
            if (chkGST4) {
                CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value;
                SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
            }


            var NetAmount = document.getElementById('<%=txtNetAmt.ClientID%>').value;
            document.getElementById('<%=txtGSTAmount.ClientID%>').value = parseFloat(parseFloat(parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount))).toFixed(2);
            // document.getElementById('<%=txtGSTAmount.ClientID%>').value =parseFloat(document.getElementById('<%=txtBillAmt.ClientID%>').value)+ parseFloat(parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));
            var Amount = parseFloat(parseFloat(document.getElementById('<%=txtGSTAmount.ClientID%>').value) + parseFloat(document.getElementById('<%=txtBillAmt.ClientID%>').value));

            NetAmount = parseFloat(parseFloat(Amount) - parseFloat(TdsAmount));



        }
    </script>
    <script type="text/javascript">

        function CalSGST(val) {

            var chkGST3 = document.getElementById('<%= chkGST.ClientID%>').checked;
            var BillAmt3 = document.getElementById('<%= txtBillAmt.ClientID %>').value;



            CGSTPer3 = document.getElementById('<%=txtCGSTPER.ClientID%>').value;

            SGSTPer3 = document.getElementById('<%=txtSgstPer.ClientID%>').value;

            if (CGSTPer3 == '') {
                CGSTPer3 = 0;
            }
            if (SGSTPer3 == '') {
                SGSTPer3 = 0;
            }

            if (chkGST3) {
                document.getElementById('<%= txtSgstAmount.ClientID %>').value = parseFloat(parseFloat(BillAmt3) * parseFloat(SGSTPer3) * 0.01).toFixed(2);
                //document.getElementById('<%= txtCgstAmount.ClientID %>').value = parseFloat(BillAmt3) * parseFloat(CGSTPer3) * 0.01;

                var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                var chkTDSonGST4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                var chkTDSonCGSTSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;

                var SecurityAmount = 0;
                var TdsAmount = 0;
                var TdsonGSTAmount = 0;
                var TdsonCGSTAmount = 0;
                var TdsonSGSTAmount = 0;
                var CgstAmount = 0;
                var SgstAmount = 0;
                var IgstAmount = 0;

                if (chkTDS4) {
                    TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;
                }

                if (chkTDSonGST4) {
                    TdsonGSTAmount = document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value;
                }
                if (chkTDSonCGSTSGST4) {
                    TdsonCGSTAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
                }
                if (chkTDSonCGSTSGST4) {
                    TdsonSGSTAmount = document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value;
                }
                if (chkIGST4) {
                    IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                }
                if (chkGST4) {
                    if (document.getElementById('<%=txtCgstAmount.ClientID%>').value != '') {
                        CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value;
                    }

                    SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;

                }

                var amountnet = parseFloat(parseFloat(BillAmt3) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount)).toFixed(2);

                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(amountnet - TdsAmount - TdsonGSTAmount - TdsonCGSTAmount - TdsonSGSTAmount - SecurityAmount).toFixed(2);
                document.getElementById("<%=txtGSTAmount.ClientID%>").value = parseFloat(parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount)).toFixed(2);
                document.getElementById("<%=txtTotalBillAmt.ClientID%>").value = parseFloat(parseFloat(BillAmt3) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount)).toFixed(2);

                document.getElementById('<%=txtTotalTDSAmt.ClientID%>').value = parseFloat(parseFloat(TdsAmount) + parseFloat(TdsonGSTAmount) + parseFloat(TdsonCGSTAmount) + parseFloat(TdsonSGSTAmount) + parseFloat(SecurityAmount)).toFixed(2);
            }
        }



        function CalGSTTxt(val) {


            var chkGST3 = document.getElementById('<%= chkGST.ClientID%>').checked;
            var BillAmt3 = document.getElementById('<%= txtBillAmt.ClientID %>').value;



            CGSTPer3 = document.getElementById('<%=txtCGSTPER.ClientID%>').value;

            SGSTPer3 = document.getElementById('<%=txtSgstPer.ClientID%>').value;

            if (CGSTPer3 == '') {
                CGSTPer3 = 0;
            }
            if (SGSTPer3 == '') {
                SGSTPer3 = 0;
            }

            if (chkGST3) {
                //document.getElementById('<%= txtSgstAmount.ClientID %>').value = parseFloat(BillAmt3) * parseFloat(SGSTPer3) * 0.01;
                //document.getElementById('<%= txtCgstAmount.ClientID %>').value = parseFloat(BillAmt3) * parseFloat(CGSTPer3) * 0.01;

                var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                var chkTDSonGST4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                var chkTDSonCGSTSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;

                var SecurityAmount = 0;
                var TdsAmount = 0;
                var TdsonGSTAmount = 0;
                var TdsonCGSTAmount = 0;
                var TdsonSGSTAmount = 0;
                var CgstAmount = 0;
                var SgstAmount = 0;
                var IgstAmount = 0;

                if (chkTDS4) {
                    TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;
                }

                if (chkTDSonGST4) {
                    TdsonGSTAmount = document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value;
                }
                if (chkTDSonCGSTSGST4) {
                    TdsonCGSTAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
                }
                if (chkTDSonCGSTSGST4) {
                    TdsonSGSTAmount = document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value;
                }
                if (chkIGST4) {
                    IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                }
                if (chkGST4) {
                    if (document.getElementById('<%=txtCgstAmount.ClientID%>').value != '') {
                        CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value;
                    }
                    if (document.getElementById('<%=txtSgstAmount.ClientID%>').value != '') {
                        SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                    }

                }

                var amountnet = parseFloat(parseFloat(BillAmt3) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));

                //alert(amountnet);
                //alert(parseFloat(TdsAmount));
                //alert(parseFloat(TdsonGSTAmount));
                //alert(parseFloat(TdsonCGSTAmount));
                //alert(parseFloat(TdsonSGSTAmount));
                //alert(parseFloat(SecurityAmount));

                //  alert(parseFloat(parseFloat(amountnet) - parseFloat(TdsAmount) - parseFloat(TdsonGSTAmount) - parseFloat(TdsonCGSTAmount) - parseFloat(TdsonSGSTAmount) - parseFloat(SecurityAmount)).toFixed(2));
                //  alert( parseFloat(parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount)).toFixed(2));
                //  alert( parseFloat(parseFloat(BillAmt3) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount)).toFixed(2));
                //alert( parseFloat(parseFloat(TdsAmount) + parseFloat(TdsonGSTAmount) + parseFloat(TdsonCGSTAmount) + parseFloat(TdsonSGSTAmount) + parseFloat(SecurityAmount)).toFixed(2));


                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(parseFloat(amountnet) - parseFloat(TdsAmount) - parseFloat(TdsonGSTAmount) - parseFloat(TdsonCGSTAmount) - parseFloat(TdsonSGSTAmount) - parseFloat(SecurityAmount)).toFixed(2);
                document.getElementById("<%=txtGSTAmount.ClientID%>").value = parseFloat(parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount)).toFixed(2);
                document.getElementById("<%=txtTotalBillAmt.ClientID%>").value = parseFloat(parseFloat(BillAmt3) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount)).toFixed(2);
                document.getElementById('<%=txtTotalTDSAmt.ClientID%>').value = parseFloat(parseFloat(TdsAmount) + parseFloat(TdsonGSTAmount) + parseFloat(TdsonCGSTAmount) + parseFloat(TdsonSGSTAmount) + parseFloat(SecurityAmount)).toFixed(2);
            }
        } function CalGSTTxt(val) {


            var chkGST3 = document.getElementById('<%= chkGST.ClientID%>').checked;
            var BillAmt3 = document.getElementById('<%= txtBillAmt.ClientID %>').value;



            CGSTPer3 = document.getElementById('<%=txtCGSTPER.ClientID%>').value;

            SGSTPer3 = document.getElementById('<%=txtSgstPer.ClientID%>').value;

            if (CGSTPer3 == '') {
                CGSTPer3 = 0;
            }
            if (SGSTPer3 == '') {
                SGSTPer3 = 0;
            }

            if (chkGST3) {
                //document.getElementById('<%= txtSgstAmount.ClientID %>').value = parseFloat(BillAmt3) * parseFloat(SGSTPer3) * 0.01;
                //document.getElementById('<%= txtCgstAmount.ClientID %>').value = parseFloat(BillAmt3) * parseFloat(CGSTPer3) * 0.01;

                var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                var chkTDSonGST4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                var chkTDSonCGSTSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;

                var SecurityAmount = 0;
                var TdsAmount = 0;
                var TdsonGSTAmount = 0;
                var TdsonCGSTAmount = 0;
                var TdsonSGSTAmount = 0;
                var CgstAmount = 0;
                var SgstAmount = 0;
                var IgstAmount = 0;

                if (chkTDS4) {
                    TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;
                }

                if (chkTDSonGST4) {
                    TdsonGSTAmount = document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value;
                }
                if (chkTDSonCGSTSGST4) {
                    TdsonCGSTAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
                }
                if (chkTDSonCGSTSGST4) {
                    TdsonSGSTAmount = document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value;
                }
                if (chkIGST4) {
                    IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                }
                if (chkGST4) {
                    if (document.getElementById('<%=txtCgstAmount.ClientID%>').value != '') {
                        CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value;
                    }
                    if (document.getElementById('<%=txtSgstAmount.ClientID%>').value != '') {
                        SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                    }

                }

                var amountnet = parseFloat(parseFloat(BillAmt3) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));

                //alert(amountnet);
                //alert(parseFloat(TdsAmount));
                //alert(parseFloat(TdsonGSTAmount));
                //alert(parseFloat(TdsonCGSTAmount));
                //alert(parseFloat(TdsonSGSTAmount));
                //alert(parseFloat(SecurityAmount));

                //  alert(parseFloat(parseFloat(amountnet) - parseFloat(TdsAmount) - parseFloat(TdsonGSTAmount) - parseFloat(TdsonCGSTAmount) - parseFloat(TdsonSGSTAmount) - parseFloat(SecurityAmount)).toFixed(2));
                //  alert( parseFloat(parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount)).toFixed(2));
                //  alert( parseFloat(parseFloat(BillAmt3) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount)).toFixed(2));
                //alert( parseFloat(parseFloat(TdsAmount) + parseFloat(TdsonGSTAmount) + parseFloat(TdsonCGSTAmount) + parseFloat(TdsonSGSTAmount) + parseFloat(SecurityAmount)).toFixed(2));


                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(parseFloat(amountnet) - parseFloat(TdsAmount) - parseFloat(TdsonGSTAmount) - parseFloat(TdsonCGSTAmount) - parseFloat(TdsonSGSTAmount) - parseFloat(SecurityAmount)).toFixed(2);
                document.getElementById("<%=txtGSTAmount.ClientID%>").value = parseFloat(parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount)).toFixed(2);
                document.getElementById("<%=txtTotalBillAmt.ClientID%>").value = parseFloat(parseFloat(BillAmt3) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount)).toFixed(2);
                document.getElementById('<%=txtTotalTDSAmt.ClientID%>').value = parseFloat(parseFloat(TdsAmount) + parseFloat(TdsonGSTAmount) + parseFloat(TdsonCGSTAmount) + parseFloat(TdsonSGSTAmount) + parseFloat(SecurityAmount)).toFixed(2);
            }
        }
    </script>
    <script type="text/javascript">


        function CalGST(val) {

            debugger;
            var chkGST3 = document.getElementById('<%= chkGST.ClientID%>').checked;
            var BillAmt3 = document.getElementById('<%= txtBillAmt.ClientID %>').value;

            if (document.getElementById('<%=txtSgstPer.ClientID%>').value == '') {
                document.getElementById('<%=txtSgstPer.ClientID%>').value = '0';
            }


            document.getElementById('<%=txtCGSTPER.ClientID%>').value = parseFloat(document.getElementById('<%=txtCGSTPER.ClientID%>').value).toFixed(2);
            CGSTPer3 = document.getElementById('<%=txtCGSTPER.ClientID%>').value;
            document.getElementById('<%=txtSgstPer.ClientID%>').value = parseFloat(document.getElementById('<%=txtSgstPer.ClientID%>').value).toFixed(2);
            SGSTPer3 = document.getElementById('<%=txtSgstPer.ClientID%>').value;

            if (CGSTPer3 == '') {
                CGSTPer3 = 0;
            }
            if (SGSTPer3 == '') {
                SGSTPer3 = 0;
            }

            if (chkGST3) {
                //document.getElementById('<%= txtSgstAmount.ClientID %>').value = parseFloat(parseFloat(BillAmt3).toFixed(2) * parseFloat(SGSTPer3).toFixed(2) * 0.01).toFixed(2);
                document.getElementById('<%= txtCgstAmount.ClientID %>').value = parseFloat(parseFloat(BillAmt3).toFixed(2) * parseFloat(CGSTPer3).toFixed(2) * 0.01).toFixed(2);

                var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                var chkTDSonGST4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                var chkTDSonCGSTSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;

                var SecurityAmount = 0;
                var TdsAmount = 0;
                var TdsonGSTAmount = 0;
                var TdsonCGSTAmount = 0;
                var TdsonSGSTAmount = 0;
                var CgstAmount = 0;
                var SgstAmount = 0;
                var IgstAmount = 0;

                if (chkTDS4) {
                    TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;
                }

                if (chkTDSonCGSTSGST4) {
                    TdsonCGSTAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
                }
                if (chkTDSonCGSTSGST4) {
                    TdsonSGSTAmount = document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value;
                }
                if (chkTDSonGST4) {
                    TdsonGSTAmount = document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value;
                }
                if (chkIGST4) {
                    IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                }
                if (chkGST4) {

                    CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value;
                    if (document.getElementById('<%=txtSgstAmount.ClientID%>').value != '') {
                        SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                    }

                }

                var amountnet = parseFloat(parseFloat(BillAmt3) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));

                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(parseFloat(amountnet).toFixed(2) - parseFloat(TdsAmount).toFixed(2) - parseFloat(TdsonGSTAmount).toFixed(2) - parseFloat(TdsonCGSTAmount).toFixed(2) - parseFloat(TdsonSGSTAmount).toFixed(2) - parseFloat(SecurityAmount).toFixed(2)).toFixed(2);
                document.getElementById("<%=txtGSTAmount.ClientID%>").value = parseFloat(parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount)).toFixed(2);
                document.getElementById("<%=txtTotalBillAmt.ClientID%>").value = parseFloat(parseFloat(BillAmt3) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount)).toFixed(2);
                document.getElementById('<%=txtTotalTDSAmt.ClientID%>').value = parseFloat(parseFloat(TdsAmount) + parseFloat(TdsonGSTAmount) + parseFloat(TdsonCGSTAmount) + parseFloat(TdsonSGSTAmount) + parseFloat(SecurityAmount)).toFixed(2);
            }
        }
    </script>
    <script type="text/javascript">
        function CalNetAmountNew() {
            var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
            var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
            var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
            var chkTDSonGSt4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
            var chkTDSonCGStSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;

            var BillAmt2 = document.getElementById('<%= txtBillAmt.ClientID %>');

            var SecurityAmount = 0;

            var TdsAmount = 0;
            var TdsonGSTAmount = 0;
            var CgstAmount = 0;
            var SgstAmount = 0;
            var IgstAmount = 0;
            var TdsonCGSTAmount = 0;
            var TdsonSGSTAmount = 0;

            if (chkTDSonCGStSGST4 && document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value != '') {
                TdsonCGSTAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
            }

            if (chkTDSonCGStSGST4 && document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value != '') {
                TdsonSGSTAmount = document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value;
            }
            if (chkTDS4 && document.getElementById('<%=txtTDSAmt.ClientID%>').value != '') {
                TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;
            }
            if (chkTDSonGSt4 && document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value != '') {
                TdsonGSTAmount = document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value;
            }
            if (chkIGST4 && document.getElementById('<%=txtIgstAmount.ClientID%>').value != '') {
                IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
            }
            if (chkGST4 && document.getElementById('<%=txtCgstAmount.ClientID%>').value != '') {
                CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value;

            }
            if (chkGST4 && document.getElementById('<%=txtSgstAmount.ClientID%>').value != '') {

                SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
            }
            document.getElementById('<%=txtTotalTDSAmt.ClientID%>').value = parseFloat(parseFloat(TdsAmount) + parseFloat(TdsonGSTAmount) + parseFloat(TdsonCGSTAmount) + parseFloat(TdsonSGSTAmount) + parseFloat(SecurityAmount)).toFixed(2);
            var amountnet = parseFloat(parseFloat(BillAmt2.value) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount)).toFixed(2);

            document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(parseFloat(amountnet) - parseFloat(TdsAmount) - parseFloat(TdsonGSTAmount) - parseFloat(TdsonCGSTAmount) - parseFloat(TdsonSGSTAmount) - parseFloat(SecurityAmount)).toFixed(2);

        }


        function CalIGST() {

            var chkIGST2 = document.getElementById('<%= chkIGST.ClientID%>').checked;
            var BillAmt2 = document.getElementById('<%= txtBillAmt.ClientID %>');
            document.getElementById('<%=txtIgstAmount.ClientID%>').value = parseFloat(document.getElementById('<%=txtIgstAmount.ClientID%>').value).toFixed(2);
            if (chkIGST2) {
                IGSTAmt2 = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                document.getElementById('<%=txtIgstPer.ClientID%>').value = parseFloat(document.getElementById('<%=txtIgstPer.ClientID%>').value).toFixed(2);
                IGSTPer2 = document.getElementById('<%=txtIgstPer.ClientID%>').value;
                CalIGST2 = parseFloat(BillAmt2.value) * parseFloat(IGSTPer2) * 0.01;
                document.getElementById('<%=txtIgstAmount.ClientID%>').value = parseFloat(CalIGST2).toFixed(2);
                document.getElementById('<%=txtGSTAmount.ClientID%>').value = parseFloat(CalIGST2).toFixed(2);

                //document.getElementById('<%=txtTotalBillAmt.ClientID%>').value = parseFloat(BillAmt2.value) + parseFloat(CalIGST2).toFixed(2);

                var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                var chkTDSonGSt4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                var chkTDSonCGStSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;

                var SecurityAmount = 0;
                var TdsAmount = 0;
                var TdsonGSTAmount = 0;
                var TdsonCGSTAmount = 0;
                var TdsonSGSTAmount = 0;
                var CgstAmount = 0;
                var SgstAmount = 0;
                var IgstAmount = 0;

                if (chkTDS4) {
                    TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;
                }
                if (chkTDSonCGStSGST4) {
                    TdsonCGSTAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
                }
                if (chkTDSonCGStSGST4) {
                    TdsonSGSTAmount = document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value;
                }
                if (chkTDSonGSt4) {
                    TdsonGSTAmount = document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value;
                }
                if (chkIGST4) {
                    if (document.getElementById('<%=txtIgstAmount.ClientID%>').value != '') {
                        IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                    }
                }
                if (chkGST4) {
                    if (document.getElementById('<%=txtCgstAmount.ClientID%>').value != '') {
                        CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value;
                    }
                    if (document.getElementById('<%=txtSgstAmount.ClientID%>').value != '') {
                        SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                    }
                }


                var amountnet = parseFloat(parseFloat(BillAmt2.value) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount)).toFixed(2);
                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(parseFloat(amountnet) - parseFloat(TdsAmount) - parseFloat(TdsonGSTAmount) - parseFloat(TdsonCGSTAmount) - parseFloat(TdsonSGSTAmount) - parseFloat(SecurityAmount)).toFixed(2);
                //document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(amountnet - TdsAmount - TdsonGSTAmount - TdsonCGSTAmount - TdsonSGSTAmount - SecurityAmount).toFixed(2);
                document.getElementById("<%=txtTotalBillAmt.ClientID%>").value = parseFloat(parseFloat(BillAmt2.value) + parseFloat(IgstAmount)).toFixed(2);
                document.getElementById('<%=txtTotalTDSAmt.ClientID%>').value = parseFloat(parseFloat(TdsAmount) + parseFloat(TdsonGSTAmount) + parseFloat(TdsonCGSTAmount) + parseFloat(TdsonSGSTAmount) + parseFloat(SecurityAmount)).toFixed(2);
            }
        }

        function CalIGSTTxt() {

            var chkIGST2 = document.getElementById('<%= chkIGST.ClientID%>').checked;
            var BillAmt2 = document.getElementById('<%= txtBillAmt.ClientID %>');
            //document.getElementById('<%=txtIgstAmount.ClientID%>').value = parseFloat(document.getElementById('<%=txtIgstAmount.ClientID%>').value).toFixed(2);
            if (chkIGST2) {
                IGSTAmt2 = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                document.getElementById('<%=txtIgstPer.ClientID%>').value = parseFloat(document.getElementById('<%=txtIgstPer.ClientID%>').value).toFixed(2);
                IGSTPer2 = document.getElementById('<%=txtIgstPer.ClientID%>').value;
                CalIGST2 = parseFloat(BillAmt2.value) * parseFloat(IGSTPer2) * 0.01;
                //document.getElementById('<%=txtIgstAmount.ClientID%>').value = parseFloat(CalIGST2).toFixed(2);
                document.getElementById('<%=txtGSTAmount.ClientID%>').value = parseFloat(CalIGST2).toFixed(2);

                //document.getElementById('<%=txtTotalBillAmt.ClientID%>').value = parseFloat(BillAmt2.value) + parseFloat(CalIGST2).toFixed(2);

                var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                var chkTDSonGSt4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                var chkTDSonCGStSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;

                var SecurityAmount = 0;
                var TdsAmount = 0;
                var TdsonGSTAmount = 0;
                var TdsonCGSTAmount = 0;
                var TdsonSGSTAmount = 0;
                var CgstAmount = 0;
                var SgstAmount = 0;
                var IgstAmount = 0;

                if (chkTDS4) {
                    TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;
                }
                if (chkTDSonCGStSGST4) {
                    TdsonCGSTAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
                }
                if (chkTDSonCGStSGST4) {
                    TdsonSGSTAmount = document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value;
                }
                if (chkTDSonGSt4) {
                    TdsonGSTAmount = document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value;
                }
                if (chkIGST4) {
                    if (document.getElementById('<%=txtIgstAmount.ClientID%>').value != '') {
                        IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                    }
                }
                if (chkGST4) {
                    if (document.getElementById('<%=txtCgstAmount.ClientID%>').value != '') {
                        CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value;
                    }
                    if (document.getElementById('<%=txtSgstAmount.ClientID%>').value != '') {
                        SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                    }
                }


                var amountnet = parseFloat(parseFloat(BillAmt2.value) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount)).toFixed(2);
                document.getElementById("<%=txtTotalBillAmt.ClientID%>").value = parseFloat(parseFloat(BillAmt2.value) + parseFloat(IgstAmount)).toFixed(2);
                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(amountnet - TdsAmount - TdsonGSTAmount - TdsonCGSTAmount - TdsonSGSTAmount - SecurityAmount).toFixed(2);
                //document.getElementById('<%=txtTotalTDSAmt.ClientID%>').value = parseFloat(TdsAmount + TdsonGSTAmount + TdsonCGSTAmount + TdsonSGSTAmount + SecurityAmount).toFixed(2);
                document.getElementById('<%=txtTotalTDSAmt.ClientID%>').value = parseFloat(parseFloat(TdsAmount) + parseFloat(TdsonGSTAmount) + parseFloat(TdsonCGSTAmount) + parseFloat(TdsonSGSTAmount) + parseFloat(SecurityAmount)).toFixed(2);
            }
        }

        function CalSECURITY() {


            var BillAmt2 = document.getElementById('<%= txtBillAmt.ClientID %>');

            if (chkIGST2) {

                CalIGST2 = parseFloat(BillAmt2.value) * parseFloat(IGSTPer2) * 0.01;



                var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                var chkTDSonGSt4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                var chkTDSonCGStSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;

                var SecurityAmount = 0;
                var TdsAmount = 0;
                var TdsonGSTAmount = 0;
                var TdsonCGSTAmount = 0;
                var TdsonSGSTAmount = 0;
                var CgstAmount = 0;
                var SgstAmount = 0;
                var IgstAmount = 0;


                if (chkTDS4) {
                    TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;
                }
                if (chkTDSonCGStSGST4) {
                    TdsonCGSTAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
                }
                if (chkTDSonCGStSGST4) {
                    TdsonSGSTAmount = document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value;
                }
                if (chkTDSonGSt4) {
                    TdsonGSTAmount = document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value;
                }
                if (chkIGST4) {
                    IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                }

                if (chkGST4) {
                    CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value;
                    SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                }

                var amountnet = parseFloat(parseFloat(BillAmt2.value) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount)).toFixed(2);
                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(parseFloat(amountnet) - parseFloat(TdsAmount) - parseFloat(TdsonGSTAmount) - parseFloat(TdsonCGSTAmount) - parseFloat(TdsonSGSTAmount) - parseFloat(SecurityAmount)).toFixed(2);
                //document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(amountnet - TdsAmount - TdsonGSTAmount - TdsonCGSTAmount - TdsonSGSTAmount - SecurityAmount).toFixed(2);
                //document.getElementById('<%=txtTotalTDSAmt.ClientID%>').value = parseFloat(TdsAmount + TdsonGSTAmount + TdsonCGSTAmount + TdsonSGSTAmount + SecurityAmount).toFixed(2);
                document.getElementById('<%=txtTotalTDSAmt.ClientID%>').value = parseFloat(parseFloat(TdsAmount) + parseFloat(TdsonGSTAmount) + parseFloat(TdsonCGSTAmount) + parseFloat(TdsonSGSTAmount) + parseFloat(SecurityAmount)).toFixed(2);

            }
        }

        function CalSECURITYAmt() {


            var BillAmt2 = document.getElementById('<%= txtBillAmt.ClientID %>');

            if (chkIGST2) {

                var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                var chkTDSonGSt4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                var chkTDSonCGStSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;

                var SecurityAmount = 0;
                var TdsAmount = 0;
                var TdsonGSTAmount = 0;
                var TdsonCGSTAmount = 0;
                var TdsonSGSTAmount = 0;
                var CgstAmount = 0;
                var SgstAmount = 0;
                var IgstAmount = 0;


                if (chkTDS4 && document.getElementById('<%=txtTDSAmt.ClientID%>').value != '') {
                    TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;
                }
                if (chkTDSonCGStSGST4 && document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value != '') {
                    TdsonCGSTAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
                }
                if (chkTDSonCGStSGST4 && document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value != '') {
                    TdsonSGSTAmount = document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value;
                }
                if (chkTDSonGSt4 && document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value != '') {
                    TdsonGSTAmount = document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value;
                }
                if (chkIGST4 && document.getElementById('<%=txtIgstAmount.ClientID%>').value != '') {
                    IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                }

                if (chkGST4 && document.getElementById('<%=txtCgstAmount.ClientID%>').value != '') {
                    CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value;
                }
                if (chkGST4 && document.getElementById('<%=txtSgstAmount.ClientID%>').value != '') {
                    SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                }

                var amountnet = parseFloat(parseFloat(BillAmt2.value) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount)).toFixed(2);
                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(parseFloat(amountnet) - parseFloat(TdsAmount) - parseFloat(TdsonGSTAmount) - parseFloat(TdsonCGSTAmount) - parseFloat(TdsonSGSTAmount) - parseFloat(SecurityAmount)).toFixed(2); document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(parseFloat(amountnet) - parseFloat(TdsAmount) - parseFloat(TdsonGSTAmount) - parseFloat(TdsonCGSTAmount) - parseFloat(TdsonSGSTAmount) - parseFloat(SecurityAmount)).toFixed(2);
                //document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(amountnet - TdsAmount - TdsonGSTAmount - TdsonCGSTAmount - TdsonSGSTAmount - SecurityAmount).toFixed(2);
                document.getElementById('<%=txtTotalTDSAmt.ClientID%>').value = parseFloat(parseFloat(TdsAmount) + parseFloat(TdsonGSTAmount) + parseFloat(TdsonCGSTAmount) + parseFloat(TdsonSGSTAmount) + parseFloat(SecurityAmount)).toFixed(2);
            }
        }


    </script>
    <script type="text/javascript">
        function Per(val, con) {

            var BillAmount = parseFloat(document.getElementById('<%=txtBillAmt.ClientID%>').value).toFixed(2);
            var TDSonAmount = parseFloat(document.getElementById('<%=txtTdsOnAmt.ClientID%>').value).toFixed(2)

            var per = parseFloat(BillAmount / 100).toFixed(2);
            var TDSPer = parseFloat(TDSonAmount / 100).toFixed(2);

            // var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
            var chkTDSonGSt = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
            var chkTDSonCGStSGST = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;

            var SecurityAmount = 0;
            var TdsonGstamt = 0;
            var TdsonCGstamt = 0;
            var TdsonSGstamt = 0;
            if (chkTDSonGSt) {
                TdsonGstamt = document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value;
            }


            if (chkTDSonCGStSGST) {
                TdsonCGstamt = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
            }
            if (chkTDSonCGStSGST) {
                TdsonSGstamt = document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value;
            }

            if (con == "1") {


                document.getElementById('<%=txtTDSAmt.ClientID%>').value = parseFloat(TDSPer * val).toFixed(2);
                //document.getElementById('<%=txtNetAmt.ClientID%>').value =parseFloat(parseFloat( document.getElementById('<%=txtBillAmt.ClientID%>').value).toFixed(2) -parseFloat( document.getElementById('<%=txtTDSAmt.ClientID%>').value).toFixed(2)-parseFloat(TdsonGstamt).toFixed(2)-parseFloat(TdsonCGstamt).toFixed(2)-parseFloat(TdsonSGstamt).toFixed(2)-parseFloat(SecurityAmount)).toFixed(2);

            }
            if (con == "2") {


                document.getElementById('<%=txtIgstAmount.ClientID%>').value = parseFloat(per * val).toFixed(2);
                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(parseFloat(document.getElementById('<%=txtNetAmt.ClientID%>').value) + parseFloat(document.getElementById('<%=txtIgstAmount.ClientID%>').value)).toFixed(2);
                document.getElementById('<%=txtGSTAmount.ClientID%>').value = parseFloat(document.getElementById('<%=txtIgstAmount.ClientID%>').value).toFixed(2);
            }
            if (con == "3") {
                document.getElementById('<%=txtCgstAmount.ClientID%>').value = parseFloat(per * val).toFixed(2);
                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(parseFloat(document.getElementById('<%=txtNetAmt.ClientID%>').value) + parseFloat(document.getElementById('<%=txtCgstAmount.ClientID%>').value)).toFixed(2);
                document.getElementById('<%=txtGSTAmount.ClientID%>').value = parseFloat(document.getElementById('<%=txtCgstAmount.ClientID%>').value).toFixed(2);
            }
            if (con == "4") {
                document.getElementById('<%=txtSgstAmount.ClientID%>').value = parseFloat(per * val).toFixed(2);
                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(parseFloat(document.getElementById('<%=txtNetAmt.ClientID%>').value) + parseFloat(document.getElementById('<%=txtSgstAmount.ClientID%>').value)).toFixed(2);
                document.getElementById('<%=txtGSTAmount.ClientID%>').value = parseFloat(parseFloat(document.getElementById('<%=txtGSTAmount.ClientID%>').value) + parseFloat(document.getElementById('<%=txtSgstAmount.ClientID%>').value)).toFixed(2);
            }
            CalNetAmountNew();

        }

        function PerTdsonGst(val, con) {

            var BillAmount = parseFloat(document.getElementById('<%=txtBillAmt.ClientID%>').value).toFixed(2);
            var TDSGSTonAmount = parseFloat(document.getElementById('<%=txtTDSGSTonAmount.ClientID%>').value).toFixed(2);

            var per = parseFloat(BillAmount / 100).toFixed(2);
            var TDSonGSTper = parseFloat(TDSGSTonAmount / 100).toFixed(2);

            var chkTDS = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;

            var TdsonAmt = 0;
            if (chkTDS) {
                TdsonAmt = document.getElementById('<%=txtTDSAmt.ClientID%>').value
            }
            if (con == "1") {


                document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value = parseFloat(TDSonGSTper * val).toFixed(2);
                //document.getElementById('<%=txtNetAmt.ClientID%>').value =parseFloat(parseFloat( document.getElementById('<%=txtBillAmt.ClientID%>').value).toFixed(2) -parseFloat( document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value).toFixed(2)-parseFloat( document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value).toFixed(2)-parseFloat( document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value).toFixed(2)-parseFloat(TdsonAmt).toFixed(2)).toFixed(2);

            }
            if (con == "2") {


                document.getElementById('<%=txtIgstAmount.ClientID%>').value = parseFloat(per * val).toFixed(2);
                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(parseFloat(document.getElementById('<%=txtNetAmt.ClientID%>').value) + parseFloat(document.getElementById('<%=txtIgstAmount.ClientID%>').value)).toFixed(2);
                document.getElementById('<%=txtGSTAmount.ClientID%>').value = parseFloat(document.getElementById('<%=txtIgstAmount.ClientID%>').value).toFixed(2);
            }
            if (con == "3") {
                document.getElementById('<%=txtCgstAmount.ClientID%>').value = parseFloat(per * val).toFixed(2);
                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(parseFloat(document.getElementById('<%=txtNetAmt.ClientID%>').value) + parseFloat(document.getElementById('<%=txtCgstAmount.ClientID%>').value)).toFixed(2);
                document.getElementById('<%=txtGSTAmount.ClientID%>').value = parseFloat(document.getElementById('<%=txtCgstAmount.ClientID%>').value).toFixed(2);
            }
            if (con == "4") {
                document.getElementById('<%=txtSgstAmount.ClientID%>').value = parseFloat(per * val).toFixed(2);
                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(parseFloat(document.getElementById('<%=txtNetAmt.ClientID%>').value) + parseFloat(document.getElementById('<%=txtSgstAmount.ClientID%>').value)).toFixed(2);
                document.getElementById('<%=txtGSTAmount.ClientID%>').value = parseFloat(parseFloat(document.getElementById('<%=txtGSTAmount.ClientID%>').value) + parseFloat(document.getElementById('<%=txtSgstAmount.ClientID%>').value)).toFixed(2);
            }

            CalNetAmountNew();
        }
        function PerTdsonCGst(val, con) {

            var BillAmount = parseFloat(document.getElementById('<%=txtBillAmt.ClientID%>').value).toFixed(2);
            var TDSGSTonAmount = parseFloat(document.getElementById('<%=txtTDSCGSTonAmount.ClientID%>').value).toFixed(2);

            var per = parseFloat(BillAmount / 100).toFixed(2);
            var TDSonGSTper = parseFloat(TDSGSTonAmount / 100).toFixed(2);

            var chkTDS = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;

            var TdsonAmt = 0;
            if (chkTDS) {
                TdsonAmt = document.getElementById('<%=txtTDSAmt.ClientID%>').value
            }
            if (con == "1") {


                document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value = parseFloat(TDSonGSTper * val).toFixed(2);
                //document.getElementById('<%=txtNetAmt.ClientID%>').value =parseFloat(parseFloat( document.getElementById('<%=txtBillAmt.ClientID%>').value).toFixed(2) -parseFloat( document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value).toFixed(2)-parseFloat( document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value).toFixed(2)-parseFloat( document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value).toFixed(2)-parseFloat(TdsonAmt).toFixed(2)).toFixed(2);

            }
            if (con == "2") {


                document.getElementById('<%=txtIgstAmount.ClientID%>').value = parseFloat(per * val).toFixed(2);
                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(parseFloat(document.getElementById('<%=txtNetAmt.ClientID%>').value) + parseFloat(document.getElementById('<%=txtIgstAmount.ClientID%>').value)).toFixed(2);
                document.getElementById('<%=txtGSTAmount.ClientID%>').value = parseFloat(parseFloat(document.getElementById('<%=txtIgstAmount.ClientID%>').value)).toFixed(2);
            }
            if (con == "3") {
                document.getElementById('<%=txtCgstAmount.ClientID%>').value = parseFloat(per * val).toFixed(2);
                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(parseFloat(document.getElementById('<%=txtNetAmt.ClientID%>').value) + parseFloat(document.getElementById('<%=txtCgstAmount.ClientID%>').value)).toFixed(2);
                document.getElementById('<%=txtGSTAmount.ClientID%>').value = parseFloat(document.getElementById('<%=txtCgstAmount.ClientID%>').value).toFixed(2);
            }
            if (con == "4") {
                document.getElementById('<%=txtSgstAmount.ClientID%>').value = parseFloat(per * val).toFixed(2);
                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(parseFloat(document.getElementById('<%=txtNetAmt.ClientID%>').value) + parseFloat(document.getElementById('<%=txtSgstAmount.ClientID%>').value)).toFixed(2);
                document.getElementById('<%=txtGSTAmount.ClientID%>').value = parseFloat(parseFloat(document.getElementById('<%=txtGSTAmount.ClientID%>').value) + parseFloat(document.getElementById('<%=txtSgstAmount.ClientID%>').value)).toFixed(2);
            }

            CalNetAmountNew();
        }
        function PerTdsonSGst(val, con) {

            var BillAmount = parseFloat(document.getElementById('<%=txtBillAmt.ClientID%>').value).toFixed(2);
            var TDSGSTonAmount = parseFloat(document.getElementById('<%=txtTDSSGSTonAmount.ClientID%>').value).toFixed(2);

            var per = parseFloat(BillAmount / 100).toFixed(2);
            var TDSonGSTper = parseFloat(TDSGSTonAmount / 100).toFixed(2);

            var chkTDS = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;

            var TdsonAmt = 0;
            if (chkTDS) {
                TdsonAmt = document.getElementById('<%=txtTDSAmt.ClientID%>').value
            }
            if (con == "1") {


                document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value = parseFloat(TDSonGSTper * val).toFixed(2);
                // document.getElementById('<%=txtNetAmt.ClientID%>').value =parseFloat(parseFloat( document.getElementById('<%=txtBillAmt.ClientID%>').value).toFixed(2) -parseFloat( document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value).toFixed(2)-parseFloat( document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value).toFixed(2)-parseFloat( document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value).toFixed(2)-parseFloat(TdsonAmt).toFixed(2)).toFixed(2);

            }
            if (con == "2") {


                document.getElementById('<%=txtIgstAmount.ClientID%>').value = parseFloat(per * val).toFixed(2);
                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(parseFloat(document.getElementById('<%=txtNetAmt.ClientID%>').value) + parseFloat(document.getElementById('<%=txtIgstAmount.ClientID%>').value)).toFixed(2);
                document.getElementById('<%=txtGSTAmount.ClientID%>').value = parseFloat(document.getElementById('<%=txtIgstAmount.ClientID%>').value).toFixed(2);
            }
            if (con == "3") {
                document.getElementById('<%=txtCgstAmount.ClientID%>').value = parseFloat(per * val).toFixed(2);
                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(parseFloat(document.getElementById('<%=txtNetAmt.ClientID%>').value) + parseFloat(document.getElementById('<%=txtCgstAmount.ClientID%>').value)).toFixed(2);
                document.getElementById('<%=txtGSTAmount.ClientID%>').value = parseFloat(document.getElementById('<%=txtCgstAmount.ClientID%>').value).toFixed(2);
            }
            if (con == "4") {
                document.getElementById('<%=txtSgstAmount.ClientID%>').value = parseFloat(per * val).toFixed(2);
                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(parseFloat(document.getElementById('<%=txtNetAmt.ClientID%>').value) + parseFloat(document.getElementById('<%=txtSgstAmount.ClientID%>').value)).toFixed(2);
                document.getElementById('<%=txtGSTAmount.ClientID%>').value = parseFloat(parseFloat(document.getElementById('<%=txtGSTAmount.ClientID%>').value) + parseFloat(document.getElementById('<%=txtSgstAmount.ClientID%>').value)).toFixed(2);
            }

            CalNetAmountNew();
        }
    </script>
    <script type="text/javascript">
        function CheckGstinNo() {

            var GstInno = document.getElementById('<%=txtGSTINNo.ClientID%>').value;

            var GstLength = GstInno.length;

            if (GstLength < 15) {
                alert('GSTIN No Number Must be 15 Characters:');
                document.getElementById('<%=txtGSTINNo.ClientID%>').value = '';
            }
        }
    </script>
    <%-- <script type="text/javascript">
        function SHOPOPUP(vall) {

            var myArr = new Array();
            myString = "" + vall.id + "";
            myArr = myString.split("_");
            var index = myArr[0] + '_' + myArr[1] + '_' + myArr[2] + '_' + myArr[3] + '_' + 'hdBillNo';
            alert(index)
            var Id = document.getElementById(index).value;
            var popUrl = 'Acc_TrackPaymentBill.aspx?obj=' + Id;
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=no,' +
                                  'status=no,toolbar=no,titlebar=no,' +
                                  'left=50,top=35,width=900px,height=650px';
            var openWindow = window.open(popUrl, name, appearence);
            //var openWindow = window.showModalDialog(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }
    </script>--%>
    <script type="text/javascript">
        function SHOPOPUP(vall) {
            debugger;
            var myArr = new Array();
            myString = "" + vall.id + "";
            myArr = myString.split("_");
            var index = myArr[0] + '_' + myArr[1] + '_' + myArr[2] + '_' + myArr[3] + '_' + 'hdBillNo';
            var Id = document.getElementById(index).value;
            //var Id = document.getElementById(index).innerText;
            var popUrl = 'Acc_TrackPaymentBill.aspx?obj=' + Id;
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=no,' +
                                  'status=no,toolbar=no,titlebar=no,' +
                                  'left=50,top=35,width=900px,height=650px';
            var openWindow = window.open(popUrl, name, appearence);
            //var openWindow = window.showModalDialog(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }

        function CheckAmount(val) {

            var Amount = parseFloat(document.getElementById("<%=txtTdsOnAmt.ClientID%>").value);
            var Gvienamount = val.value;
            if (Gvienamount != Amount) {
                alert("Bill Amount Should be less equal to Amount");
                val.value = Amount;

            }

        }
        function CheckAmountTdsGst(val) {

            var Amount = parseFloat(document.getElementById("<%=txtTDSGSTonAmount.ClientID%>").value);
            var Gvienamount = val.value;
            if (Gvienamount != Amount) {
                alert("Bill Amount Should be less equal to Amount");
                val.value = Amount;

            }

        }
        function CheckAmountTdsCGst(val) {

            var Amount = parseFloat(document.getElementById("<%=txtTDSCGSTonAmount.ClientID%>").value);
            var Gvienamount = val.value;
            if (Gvienamount != Amount) {
                alert("Bill Amount Should be less equal to Amount");
                val.value = Amount;

            }

        }
        function CheckAmountTdsSGst(val) {

            var Amount = parseFloat(document.getElementById("<%=txtTDSCGSTonAmount.ClientID%>").value);
            var Gvienamount = val.value;
            if (Gvienamount != Amount) {
                alert("Bill Amount Should be less equal to Amount");
                val.value = Amount;

            }

        }

        function CalPerAmountforTDS(val) {

            var AmtWithoutTDS = document.getElementById('<%= txtTdsOnAmt.ClientID %>').value;
            var AmtWithTDS = document.getElementById('<%= txtBillAmt.ClientID %>').value;

            var ChTranAmount = Number(AmtWithTDS);
            var ChTdsAmount = Number(AmtWithoutTDS);

            if (ChTranAmount < ChTdsAmount) {
                alert('TDS On Amount Should be Less than Or Equals to Amount');
                document.getElementById('<%= txtTdsOnAmt.ClientID %>').value = '';
                return;
            }


            var TDSper = document.getElementById('<%= txtTDSPer.ClientID %>');
            var NetTDSAmt = document.getElementById('<%= txtTDSAmt.ClientID %>');

            var Calc = 0, Netamt = 0;

            if (TDSper != "") {
                Calc = parseFloat(AmtWithoutTDS) * parseFloat(TDSper.value) * 0.01;
                NetTDSAmt.value = Math.round(parseFloat(Calc));
            }
            else {

            }

            var BillAmt = document.getElementById('<%= txtTdsOnAmt.ClientID %>');
            var chkTDS = document.getElementById('<%= chkTDSApplicable.ClientID%>').checked;
            var TotAmt = document.getElementById('<%= txtTotalBillAmt.ClientID %>');

            //added by vijay andoju on 09092020

            if (chkTDS) {
                TDSPer = document.getElementById('<%= txtTDSPer.ClientID %>');
                TDSAmt = document.getElementById('<%= txtTDSAmt.ClientID %>');
                if (BillAmt.value != "" && TDSPer.value != "") {

                    CalTDS = parseFloat(BillAmt.value) * parseFloat(TDSPer.value) * 0.01;

                    TDSAmt.value = Math.round(parseFloat(CalTDS));
                    var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                    var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                    var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                    var chkTDSonGST4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                    var chkTDSonCGSTSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;

                    var SecurityAmount = 0;
                    var TdsonCGSTAmount = 0;
                    var TdsonSGSTAmount = 0;
                    var TdsAmount = 0;
                    var CgstAmount = 0;
                    var SgstAmount = 0;
                    var IgstAmount = 0;
                    var TdsonGSTAmount = 0;
                    if (chkTDSonGST4) {
                        TdsonGSTAmount = document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value;
                    }
                    if (chkTDSonCGSTSGST4) {
                        TdsonCGSTAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
                        TdsonSGSTAmount = document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value;
                    }


                    if (chkIGST4) {
                        IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                    }
                    if (chkGST4) {
                        CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value;
                        SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                    }
                    //var amountnet= parseFloat(parseFloat(BillAmt.value)+  parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));                      
                    // document.getElementById('<%=txtNetAmt .ClientID%>').value=parseFloat(amountnet)-parseFloat(TDSAmt.value);

                    var ActualBillAmount = document.getElementById('<%= txtBillAmt.ClientID %>');
                    var amountnet = parseFloat(parseFloat(ActualBillAmount.value) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));

                    document.getElementById('<%=txtNetAmt .ClientID%>').value = parseFloat(parseFloat(amountnet) - parseFloat(TDSAmt.value) - parseFloat(TdsonGSTAmount) - parseFloat(TdsonCGSTAmount) - parseFloat(TdsonSGSTAmount) - parseFloat(SecurityAmount)).toFixed(2);
                    document.getElementById('<%=txtTotalTDSAmt .ClientID%>').value = parseFloat(parseFloat(TDSAmt.value) + parseFloat(TdsonGSTAmount) + parseFloat(TdsonCGSTAmount) + parseFloat(TdsonSGSTAmount) + parseFloat(SecurityAmount)).toFixed(2);

                }
            }

        }

        function CalPerAmountforTDSonGst(val) {

            var AmtWithoutTDS = document.getElementById('<%= txtTDSGSTonAmount.ClientID %>').value;
            var AmtWithTDS = document.getElementById('<%= txtBillAmt.ClientID %>').value;

            var ChTranAmount = Number(AmtWithTDS);
            var ChTdsAmount = Number(AmtWithoutTDS);

            if (ChTranAmount < ChTdsAmount) {
                alert('TDS(IGST) On Amount Should be Less than Or Equals to Amount');
                document.getElementById('<%= txtTDSGSTonAmount.ClientID %>').value = '';
                return;
            }


            var TDSper = document.getElementById('<%= txtTDSonGSTPer.ClientID %>');
            var NetTDSAmt = document.getElementById('<%= txtTDSonGSTAmount.ClientID %>');

            var Calc = 0, Netamt = 0;

            if (TDSper != "") {
                Calc = parseFloat(AmtWithoutTDS) * parseFloat(TDSper.value) * 0.01;
                NetTDSAmt.value = Math.round(parseFloat(Calc));
            }
            else {

            }

            var BillAmt = document.getElementById('<%= txtTDSGSTonAmount.ClientID %>');
            var chkTDS = document.getElementById('<%= chkTDSOnGst.ClientID%>').checked;
            var TotAmt = document.getElementById('<%= txtTotalBillAmt.ClientID %>');

            //added by vijay andoju on 09092020

            if (chkTDS) {
                TDSonGstPer = document.getElementById('<%= txtTDSonGSTPer.ClientID %>');
                TDSonGstAmt = document.getElementById('<%= txtTDSonGSTAmount.ClientID %>');
                if (BillAmt.value != "" && TDSonGstPer.value != "") {

                    CalTDS = parseFloat(BillAmt.value) * parseFloat(TDSonGstPer.value) * 0.01;

                    TDSonGstAmt.value = Math.round(parseFloat(CalTDS));
                    var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                    var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                    var chkTDSonGST4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                    var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                    var chkTDSonCGSTSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;

                    var SecurityAmount = 0;
                    var TdsonCGSTAmount = 0;
                    var TdsonSGSTAmount = 0;
                    var TdsAmount = 0;
                    var CgstAmount = 0;
                    var SgstAmount = 0;
                    var IgstAmount = 0;
                    if (chkTDS4) {
                        TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;
                    }
                    if (chkTDSonCGSTSGST4) {
                        TdsonCGSTAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
                        TdsonSGSTAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
                    }

                    if (chkIGST4) {
                        IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                    }
                    if (chkGST4) {
                        CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value;
                        SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                    }
                    var ActualBillAmount = document.getElementById('<%= txtBillAmt.ClientID %>');
                    var amountnet = parseFloat(parseFloat(ActualBillAmount.value) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));

                    document.getElementById('<%=txtNetAmt .ClientID%>').value = parseFloat(parseFloat(amountnet) - parseFloat(TDSonGstAmt.value) - parseFloat(TdsAmount) - parseFloat(SecurityAmount) - parseFloat(TdsonCGSTAmount) - parseFloat(TdsonSGSTAmount)).toFixed(2);
                    document.getElementById('<%=txtTotalTDSAmt .ClientID%>').value = parseFloat(parseFloat(TDSonGstAmt.value) + parseFloat(TdsAmount) + parseFloat(SecurityAmount) + parseFloat(TdsonCGSTAmount) + parseFloat(TdsonSGSTAmount)).toFixed(2);

                }
            }

        }


        function CalPerAmountforTDSonSGst(val) {

            var AmtWithoutTDS = document.getElementById('<%= txtTDSSGSTonAmount.ClientID %>').value;
            var AmtWithTDS = document.getElementById('<%= txtBillAmt.ClientID %>').value;

            var ChTranAmount = Number(AmtWithTDS);
            var ChTdsAmount = Number(AmtWithoutTDS);

            if (ChTranAmount < ChTdsAmount) {
                alert('TDS(SGST) On Amount Should be Less than Or Equals to Amount');
                document.getElementById('<%= txtTDSSGSTonAmount.ClientID %>').value = '';
                return;
            }


            var TDSper = document.getElementById('<%= txtTDSonSGSTPer.ClientID %>');
            var NetTDSAmt = document.getElementById('<%= txtTDSonSGSTAmount.ClientID %>');

            var Calc = 0, Netamt = 0;

            if (TDSper != "") {
                Calc = parseFloat(AmtWithoutTDS) * parseFloat(TDSper.value) * 0.01;
                NetTDSAmt.value = Math.round(parseFloat(Calc));
            }
            else {

            }

            var BillAmt = document.getElementById('<%= txtTDSSGSTonAmount.ClientID %>');
            var chkTDS = document.getElementById('<%= chkTdsOnCGSTSGST.ClientID%>').checked;
            var TotAmt = document.getElementById('<%= txtTotalBillAmt.ClientID %>');

            //added by vijay andoju on 09092020

            if (chkTDS) {
                TDSonGstPer = document.getElementById('<%= txtTDSonSGSTPer.ClientID %>');
                TDSonSGstAmt = document.getElementById('<%= txtTDSonSGSTAmount.ClientID %>');
                if (BillAmt.value != "" && TDSonGstPer.value != "") {

                    CalTDS = parseFloat(BillAmt.value) * parseFloat(TDSonGstPer.value) * 0.01;

                    TDSonSGstAmt.value = Math.round(parseFloat(CalTDS));
                    var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                    var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                    var chkTDSonGST4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                    var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                    var chkTDSonCGSTSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;

                    var SecurityAmount = 0;
                    var TdsAmount = 0;
                    var TdsonGstAmount = 0;
                    var TdsonCgstSgstAmount = 0;
                    var CgstAmount = 0;
                    var SgstAmount = 0;
                    var IgstAmount = 0;
                    if (chkTDSonCGSTSGST4) {
                        TdsonCgstAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
                    }

                    if (chkTDSonGST4) {
                        TdsonGstAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
                    }
                    if (chkTDS4) {
                        TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;
                    }
                    if (chkIGST4) {
                        IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                    }
                    if (chkGST4) {
                        CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value;
                        SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                    }
                    var ActualBillAmount = document.getElementById('<%= txtBillAmt.ClientID %>');
                    var amountnet = parseFloat(parseFloat(ActualBillAmount.value) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));
                    //document.getElementById('<%=txtNetAmt .ClientID%>').value=parseFloat(amountnet)-parseFloat(TDSonSGstAmt.value)-parseFloat(TdsAmount);
                    document.getElementById('<%=txtNetAmt .ClientID%>').value = parseFloat(parseFloat(amountnet) - parseFloat(TDSonSGstAmt.value) - parseFloat(TdsAmount) - parseFloat(TdsonCgstAmount) - parseFloat(TdsonGstAmount) - parseFloat(SecurityAmount)).toFixed(2);
                    document.getElementById('<%=txtTotalTDSAmt.ClientID%>').value = parseFloat(parseFloat(TDSonSGstAmt.value) + parseFloat(TdsAmount) + parseFloat(TdsonCgstAmount) + parseFloat(TdsonGstAmount) + parseFloat(SecurityAmount)).toFixed(2);

                }
            }

        }

        function CalPerAmountforTDSonCGst(val) {

            var AmtWithoutTDS = document.getElementById('<%= txtTDSCGSTonAmount.ClientID %>').value;
            var AmtWithTDS = document.getElementById('<%= txtBillAmt.ClientID %>').value;

            var ChTranAmount = Number(AmtWithTDS);
            var ChTdsAmount = Number(AmtWithoutTDS);

            if (ChTranAmount < ChTdsAmount) {
                alert('TDS(CGST) On Amount Should be Less than Or Equals to Amount');
                document.getElementById('<%= txtTDSCGSTonAmount.ClientID %>').value = '';
                return;
            }


            var TDSper = document.getElementById('<%= txtTDSonCGSTPer.ClientID %>');
            var NetTDSAmt = document.getElementById('<%= txtTDSonCGSTAmount.ClientID %>');

            var Calc = 0, Netamt = 0;

            if (TDSper != "") {
                Calc = parseFloat(AmtWithoutTDS) * parseFloat(TDSper.value) * 0.01;
                NetTDSAmt.value = Math.round(parseFloat(Calc));
            }
            else {

            }

            var BillAmt = document.getElementById('<%= txtTDSCGSTonAmount.ClientID %>');
            var chkTDSCGST = document.getElementById('<%= chkTdsOnCGSTSGST.ClientID%>').checked;
            var TotAmt = document.getElementById('<%= txtTotalBillAmt.ClientID %>');

            //added by vijay andoju on 09092020

            if (chkTDSCGST) {
                TDSonGstPer = document.getElementById('<%= txtTDSonCGSTPer.ClientID %>');
                TDSonCGstAmt = document.getElementById('<%= txtTDSonCGSTAmount.ClientID %>');
                if (BillAmt.value != "" && TDSonGstPer.value != "") {

                    CalTDS = parseFloat(BillAmt.value) * parseFloat(TDSonGstPer.value) * 0.01;

                    TDSonCGstAmt.value = Math.round(parseFloat(CalTDS));
                    var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                    var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                    var chkTDSonGST4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                    var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                    var chkTDSonCGSTSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;

                    var SecurityAmount = 0;
                    var TdsAmount = 0
                    var TdsonGstAmount = 0;
                    var TdsonCGSTAmount = 0;
                    var CgstAmount = 0;
                    var SgstAmount = 0;
                    var IgstAmount = 0;
                    if (chkTDSonCGSTSGST4) {
                        TdsonSGSTAmount = document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value;
                    }

                    if (chkTDSonGST4) {
                        TdsonGstAmount = document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value;
                    }
                    if (chkTDS4) {
                        TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;
                    }
                    if (chkIGST4) {
                        IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                    }
                    if (chkGST4) {
                        CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value;
                        SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                    }
                    var ActualBillAmount = document.getElementById('<%= txtBillAmt.ClientID %>');
                    var amountnet = parseFloat(parseFloat(ActualBillAmount.value) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));
                    //document.getElementById('<%=txtNetAmt .ClientID%>').value=parseFloat(amountnet)-parseFloat(TDSonCGstAmt.value)-parseFloat(TdsAmount);
                    document.getElementById('<%=txtNetAmt .ClientID%>').value = parseFloat(parseFloat(amountnet) - parseFloat(TDSonCGstAmt.value) - parseFloat(TdsAmount) - parseFloat(TdsonSGSTAmount) - parseFloat(TdsonGstAmount) - parseFloat(SecurityAmount)).toFixed(2);
                    document.getElementById('<%=txtTotalTDSAmt.ClientID%>').value = parseFloat(parseFloat(TDSonCGstAmt.value) + parseFloat(TdsAmount) + parseFloat(TdsonSGSTAmount) + parseFloat(TdsonGstAmount) + parseFloat(SecurityAmount)).toFixed(2);

                }
            }

        }

        function CalTotDedNetAmtOnChangeinTDSManually(val) {

            var AmtWithoutTDS = document.getElementById('<%= txtTdsOnAmt.ClientID %>').value;
            var AmtWithTDS = document.getElementById('<%= txtBillAmt.ClientID %>').value;

            var ChTranAmount = Number(AmtWithTDS);
            var ChTdsAmount = Number(AmtWithoutTDS);

            if (ChTranAmount < ChTdsAmount) {
                alert('TDS On Amount Should be Less than Or Equals to Amount');
                document.getElementById('<%= txtTdsOnAmt.ClientID %>').value = '';
                return;
            }


            var TDSper = document.getElementById('<%= txtTDSPer.ClientID %>');
            var NetTDSAmt = document.getElementById('<%= txtTDSAmt.ClientID %>');

            var Calc = 0, Netamt = 0;

            //if (TDSper != "") {
            //    Calc = parseFloat(AmtWithoutTDS) * parseFloat(TDSper.value) * 0.01;
            //    NetTDSAmt.value = Math.round(parseFloat(Calc));
            //}
            //else {

            //}

            var BillAmt = document.getElementById('<%= txtTdsOnAmt.ClientID %>');
            var chkTDS = document.getElementById('<%= chkTDSApplicable.ClientID%>').checked;
            var TotAmt = document.getElementById('<%= txtTotalBillAmt.ClientID %>');

            //added by vijay andoju on 09092020

            if (chkTDS) {
                TDSPer = document.getElementById('<%= txtTDSPer.ClientID %>');
                TDSAmt = document.getElementById('<%= txtTDSAmt.ClientID %>');
                if (BillAmt.value != "" && TDSPer.value != "") {

                    //CalTDS = parseFloat(BillAmt.value) * parseFloat(TDSPer.value) * 0.01;
                    //TDSAmt.value = Math.round(parseFloat(CalTDS));

                    TDSAmt.value = NetTDSAmt.value;
                    var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                    var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                    var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                    var chkTDSonGST4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                    var chkTDSonCGSTSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;

                    var SecurityAmount = 0;
                    var TdsonCGSTAmount = 0;
                    var TdsonSGSTAmount = 0;
                    var TdsAmount = 0;
                    var CgstAmount = 0;
                    var SgstAmount = 0;
                    var IgstAmount = 0;
                    var TdsonGSTAmount = 0;
                    if (chkTDSonGST4) {
                        TdsonGSTAmount = document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value;
                    }
                    if (chkTDSonCGSTSGST4) {
                        TdsonCGSTAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
                        TdsonSGSTAmount = document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value;
                    }


                    if (chkIGST4) {
                        IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                    }
                    if (chkGST4) {
                        CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value;
                        SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                    }
                    //var amountnet= parseFloat(parseFloat(BillAmt.value)+  parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));                      
                    // document.getElementById('<%=txtNetAmt .ClientID%>').value=parseFloat(amountnet)-parseFloat(TDSAmt.value);

                    var ActualBillAmount = document.getElementById('<%= txtBillAmt.ClientID %>');
                    var amountnet = parseFloat(parseFloat(ActualBillAmount.value) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));

                    document.getElementById('<%=txtNetAmt .ClientID%>').value = parseFloat(parseFloat(amountnet) - parseFloat(TDSAmt.value) - parseFloat(TdsonGSTAmount) - parseFloat(TdsonCGSTAmount) - parseFloat(TdsonSGSTAmount) - parseFloat(SecurityAmount)).toFixed(2);
                    document.getElementById('<%=txtTotalTDSAmt .ClientID%>').value = parseFloat(parseFloat(TDSAmt.value) + parseFloat(TdsonGSTAmount) + parseFloat(TdsonCGSTAmount) + parseFloat(TdsonSGSTAmount) + parseFloat(SecurityAmount)).toFixed(2);

                }
            }

        }

        function CalTotDedNetAmtOnChangeinTDSonCGSTManually(val) {

            var AmtWithoutTDS = document.getElementById('<%= txtTDSCGSTonAmount.ClientID %>').value;
            var AmtWithTDS = document.getElementById('<%= txtBillAmt.ClientID %>').value;

            var ChTranAmount = Number(AmtWithTDS);
            var ChTdsAmount = Number(AmtWithoutTDS);

            if (ChTranAmount < ChTdsAmount) {
                alert('TDS(CGST) On Amount Should be Less than Or Equals to Amount');
                document.getElementById('<%= txtTDSCGSTonAmount.ClientID %>').value = '';
                return;
            }


            var TDSper = document.getElementById('<%= txtTDSonCGSTPer.ClientID %>');
            var NetTDSAmt = document.getElementById('<%= txtTDSonCGSTAmount.ClientID %>');

            var Calc = 0, Netamt = 0;

            //if (TDSper != "") {
            //    Calc = parseFloat(AmtWithoutTDS) * parseFloat(TDSper.value) * 0.01;
            //    NetTDSAmt.value = Math.round(parseFloat(Calc));
            //}
            //else {

            //}

            var BillAmt = document.getElementById('<%= txtTDSCGSTonAmount.ClientID %>');
            var chkTDSCGST = document.getElementById('<%= chkTdsOnCGSTSGST.ClientID%>').checked;
            var TotAmt = document.getElementById('<%= txtTotalBillAmt.ClientID %>');

            //added by vijay andoju on 09092020

            if (chkTDSCGST) {
                TDSonGstPer = document.getElementById('<%= txtTDSonCGSTPer.ClientID %>');
                TDSonCGstAmt = document.getElementById('<%= txtTDSonCGSTAmount.ClientID %>');
                if (BillAmt.value != "" && TDSonGstPer.value != "") {

                    //CalTDS = parseFloat(BillAmt.value) * parseFloat(TDSonGstPer.value) * 0.01;
                    //TDSonCGstAmt.value = Math.round(parseFloat(CalTDS));

                    TDSonCGstAmt.value = NetTDSAmt.value;

                    var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                    var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                    var chkTDSonGST4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                    var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                    var chkTDSonCGSTSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;

                    var SecurityAmount = 0;
                    var TdsAmount = 0
                    var TdsonGstAmount = 0;
                    var TdsonCGSTAmount = 0;
                    var CgstAmount = 0;
                    var SgstAmount = 0;
                    var IgstAmount = 0;
                    if (chkTDSonCGSTSGST4) {
                        TdsonSGSTAmount = document.getElementById('<%=txtTDSonSGSTAmount.ClientID%>').value;
                    }

                    if (chkTDSonGST4) {
                        TdsonGstAmount = document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value;
                    }
                    if (chkTDS4) {
                        TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;
                    }
                    if (chkIGST4) {
                        IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                    }
                    if (chkGST4) {
                        CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value;
                        SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                    }
                    var ActualBillAmount = document.getElementById('<%= txtBillAmt.ClientID %>');
                    var amountnet = parseFloat(parseFloat(ActualBillAmount.value) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));
                    //document.getElementById('<%=txtNetAmt .ClientID%>').value=parseFloat(amountnet)-parseFloat(TDSonCGstAmt.value)-parseFloat(TdsAmount);
                    document.getElementById('<%=txtNetAmt .ClientID%>').value = parseFloat(parseFloat(amountnet) - parseFloat(TDSonCGstAmt.value) - parseFloat(TdsAmount) - parseFloat(TdsonSGSTAmount) - parseFloat(TdsonGstAmount) - parseFloat(SecurityAmount)).toFixed(2);
                    document.getElementById('<%=txtTotalTDSAmt.ClientID%>').value = parseFloat(parseFloat(TDSonCGstAmt.value) + parseFloat(TdsAmount) + parseFloat(TdsonSGSTAmount) + parseFloat(TdsonGstAmount) + parseFloat(SecurityAmount)).toFixed(2);

                }
            }

        }


        function CalTotDedNetAmtOnChangeinTDSonSGSTManually(val) {

            var AmtWithoutTDS = document.getElementById('<%= txtTDSSGSTonAmount.ClientID %>').value;
            var AmtWithTDS = document.getElementById('<%= txtBillAmt.ClientID %>').value;

            var ChTranAmount = Number(AmtWithTDS);
            var ChTdsAmount = Number(AmtWithoutTDS);

            if (ChTranAmount < ChTdsAmount) {
                alert('TDS(SGST) On Amount Should be Less than Or Equals to Amount');
                document.getElementById('<%= txtTDSSGSTonAmount.ClientID %>').value = '';
                return;
            }


            var TDSper = document.getElementById('<%= txtTDSonSGSTPer.ClientID %>');
            var NetTDSAmt = document.getElementById('<%= txtTDSonSGSTAmount.ClientID %>');

            var Calc = 0, Netamt = 0;

            //if (TDSper != "") {
            //    Calc = parseFloat(AmtWithoutTDS) * parseFloat(TDSper.value) * 0.01;
            //    NetTDSAmt.value = Math.round(parseFloat(Calc));
            //}
            //else {

            //}

            var BillAmt = document.getElementById('<%= txtTDSSGSTonAmount.ClientID %>');
            var chkTDS = document.getElementById('<%= chkTdsOnCGSTSGST.ClientID%>').checked;
            var TotAmt = document.getElementById('<%= txtTotalBillAmt.ClientID %>');

            //added by vijay andoju on 09092020

            if (chkTDS) {
                TDSonGstPer = document.getElementById('<%= txtTDSonSGSTPer.ClientID %>');
                TDSonSGstAmt = document.getElementById('<%= txtTDSonSGSTAmount.ClientID %>');
                if (BillAmt.value != "" && TDSonGstPer.value != "") {

                    //CalTDS = parseFloat(BillAmt.value) * parseFloat(TDSonGstPer.value) * 0.01;

                    //TDSonSGstAmt.value = Math.round(parseFloat(CalTDS));

                    TDSonSGstAmt.value = NetTDSAmt.value;

                    var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                    var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                    var chkTDSonGST4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                    var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                    var chkTDSonCGSTSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;

                    var SecurityAmount = 0;
                    var TdsAmount = 0;
                    var TdsonGstAmount = 0;
                    var TdsonCgstSgstAmount = 0;
                    var CgstAmount = 0;
                    var SgstAmount = 0;
                    var IgstAmount = 0;
                    if (chkTDSonCGSTSGST4) {
                        TdsonCgstAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
                    }

                    if (chkTDSonGST4) {
                        TdsonGstAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
                    }
                    if (chkTDS4) {
                        TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;
                    }
                    if (chkIGST4) {
                        IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                    }
                    if (chkGST4) {
                        CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value;
                        SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                    }
                    var ActualBillAmount = document.getElementById('<%= txtBillAmt.ClientID %>');
                    var amountnet = parseFloat(parseFloat(ActualBillAmount.value) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));
                    //document.getElementById('<%=txtNetAmt .ClientID%>').value=parseFloat(amountnet)-parseFloat(TDSonSGstAmt.value)-parseFloat(TdsAmount);
                    document.getElementById('<%=txtNetAmt .ClientID%>').value = parseFloat(parseFloat(amountnet) - parseFloat(TDSonSGstAmt.value) - parseFloat(TdsAmount) - parseFloat(TdsonCgstAmount) - parseFloat(TdsonGstAmount) - parseFloat(SecurityAmount)).toFixed(2);
                    document.getElementById('<%=txtTotalTDSAmt.ClientID%>').value = parseFloat(parseFloat(TDSonSGstAmt.value) + parseFloat(TdsAmount) + parseFloat(TdsonCgstAmount) + parseFloat(TdsonGstAmount) + parseFloat(SecurityAmount)).toFixed(2);

                }
            }

        }

        function CalTotDedNetAmtOnChangeinTDSonIGSTManually(val) {

            var AmtWithoutTDS = document.getElementById('<%= txtTDSGSTonAmount.ClientID %>').value;
            var AmtWithTDS = document.getElementById('<%= txtBillAmt.ClientID %>').value;

            var ChTranAmount = Number(AmtWithTDS);
            var ChTdsAmount = Number(AmtWithoutTDS);

            if (ChTranAmount < ChTdsAmount) {
                alert('TDS(IGST) On Amount Should be Less than Or Equals to Amount');
                document.getElementById('<%= txtTDSGSTonAmount.ClientID %>').value = '';
                return;
            }


            var TDSper = document.getElementById('<%= txtTDSonGSTPer.ClientID %>');
            var NetTDSAmt = document.getElementById('<%= txtTDSonGSTAmount.ClientID %>');

            var Calc = 0, Netamt = 0;

            //if (TDSper != "") {
            //    Calc = parseFloat(AmtWithoutTDS) * parseFloat(TDSper.value) * 0.01;
            //    NetTDSAmt.value = Math.round(parseFloat(Calc));
            //}
            //else {

            //}

            var BillAmt = document.getElementById('<%= txtTDSGSTonAmount.ClientID %>');
            var chkTDS = document.getElementById('<%= chkTDSOnGst.ClientID%>').checked;
            var TotAmt = document.getElementById('<%= txtTotalBillAmt.ClientID %>');

            //added by vijay andoju on 09092020

            if (chkTDS) {
                TDSonGstPer = document.getElementById('<%= txtTDSonGSTPer.ClientID %>');
                TDSonGstAmt = document.getElementById('<%= txtTDSonGSTAmount.ClientID %>');
                if (BillAmt.value != "" && TDSonGstPer.value != "") {

                    //CalTDS = parseFloat(BillAmt.value) * parseFloat(TDSonGstPer.value) * 0.01;
                    //TDSonGstAmt.value = Math.round(parseFloat(CalTDS));

                    TDSonGstAmt.value = NetTDSAmt.value;

                    var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                    var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                    var chkTDSonGST4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                    var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                    var chkTDSonCGSTSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;

                    var SecurityAmount = 0;
                    var TdsonCGSTAmount = 0;
                    var TdsonSGSTAmount = 0;
                    var TdsAmount = 0;
                    var CgstAmount = 0;
                    var SgstAmount = 0;
                    var IgstAmount = 0;
                    if (chkTDS4) {
                        TdsAmount = document.getElementById('<%=txtTDSAmt.ClientID%>').value;
                    }
                    if (chkTDSonCGSTSGST4) {
                        TdsonCGSTAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
                        TdsonSGSTAmount = document.getElementById('<%=txtTDSonCGSTAmount.ClientID%>').value;
                    }

                    if (chkIGST4) {
                        IgstAmount = document.getElementById('<%=txtIgstAmount.ClientID%>').value;
                    }
                    if (chkGST4) {
                        CgstAmount = document.getElementById('<%=txtCgstAmount.ClientID%>').value;
                        SgstAmount = document.getElementById('<%=txtSgstAmount.ClientID%>').value;
                    }
                    var ActualBillAmount = document.getElementById('<%= txtBillAmt.ClientID %>');
                    var amountnet = parseFloat(parseFloat(ActualBillAmount.value) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount));

                    document.getElementById('<%=txtNetAmt .ClientID%>').value = parseFloat(parseFloat(amountnet) - parseFloat(TDSonGstAmt.value) - parseFloat(TdsAmount) - parseFloat(SecurityAmount) - parseFloat(TdsonCGSTAmount) - parseFloat(TdsonSGSTAmount)).toFixed(2);
                    document.getElementById('<%=txtTotalTDSAmt .ClientID%>').value = parseFloat(parseFloat(TDSonGstAmt.value) + parseFloat(TdsAmount) + parseFloat(SecurityAmount) + parseFloat(TdsonCGSTAmount) + parseFloat(TdsonSGSTAmount)).toFixed(2);

                }
            }

        }

    </script>

    <%--   =========================================================================================================================================================================================================--%>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UPDLedger"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="UPDLedger" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">BILL VERIFICATION</h3>
                        </div>
                        <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                        <div class="box-body">
                            <asp:Panel ID="Panel1" runat="server">

                                <asp:Panel ID="pnlBillList" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Account</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCompAccount" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="false" TabIndex="2">
                                                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-2 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:Button ID="btnShow" Text="Show" runat="server" CssClass="btn btn-primary" TabIndex="3" ToolTip="Click to Show Pending Bill" OnClick="btnShow_Click" />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <asp:ListView ID="lvPendingList" runat="server">
                                            <EmptyDataTemplate>
                                                <p class="text-center text-bold">
                                                    <asp:Label ID="lblErr" runat="server" Text=" No more pending list of bills for approval">
                                                    </asp:Label>
                                                </p>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading">
                                                        <h5>Pending List of Raising Payment Bill Approval</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Action
                                                                </th>
                                                                <th>Status
                                                                </th>
                                                                <th>Trans.No
                                                                </th>
                                                                <th>Name
                                                                </th>
                                                                <th>Nature Of Work
                                                                </th>
                                                                <th>Net Amount
                                                                </th>
                                                                <%-- <th>TOTAL_BILL_AMT
                                                                                    </th>--%>
                                                                <th>Bill Type
                                                                </th>
                                                                <th>Authority Remarks
                                                                </th>
                                                                <th>Approve/Reject
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="pnlShowQA" runat="server">
                                                            <asp:Image ID="imgExp" runat="server" ImageUrl="~/Images/action_down.png" TabIndex="1" />
                                                        </asp:Panel>
                                                        <%--<asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/IMAGES/edit.png" ToolTip="Please Click for Edit"
                                                                    CommandArgument='<%# Eval("RAISE_PAY_NO") %>'/>--%>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnStatus" runat="server" Text="Track Bill" ToolTip="Check Transaction Status" CssClass="btn btn-primary" OnClientClick="SHOPOPUP(this);" TabIndex="9" />

                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblBillNo" runat="server" Text='<%# Eval("BILL_NO")%>'></asp:Label>
                                                        <%--<%# Eval("BILL_NO")%>--%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("EmpName")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NATURE_SERVICE")%>
                                                    </td>
                                                    <td>
                                                        <%-- <%# Eval("TOTAL_BILL_AMT")%>--%>
                                                        <%# Eval("NET_AMT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("BILL_TYPENAME") %>
                                                        <asp:HiddenField ID="hdnBilltype" runat="server" Value='<%# Eval("BILL_TYPE") %>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("RETURN_REMARK") %>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnApproval" runat="server" Text="Select" CommandArgument='<%# Eval("RAISE_PAY_NO")%>' TabIndex="2"
                                                            ToolTip="Select to Approve/Reject" OnClick="btnApproval_Click" CssClass="btn btn-primary" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="9" style="text-align: left; padding-left: 10px; padding-right: 10px">
                                                        <asp:Panel ID="pnlQues" runat="server" CssClass="collapsePanel">
                                                            <table class="table table-bordered table-hover">
                                                                <tr class="bg-light-blue">
                                                                    <th>Reason
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <%# Eval("REASON") %>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <ajaxToolKit:CollapsiblePanelExtender ID="cpeQA" runat="server" ExpandDirection="Vertical"
                                                    TargetControlID="pnlQues" ExpandControlID="pnlShowQA" CollapseControlID="pnlShowQA"
                                                    ExpandedText="Hide Reason" CollapsedText="Show Reason" CollapsedImage="~/Images/action_down.png"
                                                    ExpandedImage="~/Images/action_up.png" ImageControlID="imgExp" Collapsed="true">
                                                </ajaxToolKit:CollapsiblePanelExtender>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>

                                </asp:Panel>
                                <input id="hdnAskSave" runat="server" type="hidden">
                                <asp:Panel ID="pnlBillDetails" runat="server" Visible="false">

                                    <div class="form-group row">
                                        <div class="col-md-2">
                                            <label>Transaction No :</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label ID="lblSerialNo" runat="server" Text="" CssClass="form-control" Style="color: red; font-weight: bold"></asp:Label>
                                        </div>
                                        <div class="col-md-2">
                                            <label><span style="color: red">*</span> Select Company :</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlSelectCompany" runat="server" TabIndex="1" AppendDataBoundItems="true" AutoPostBack="true"
                                                CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSelectCompany_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdnTDSonCGSTAmount" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnGSTAmount" runat="server" Value="0" />
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Heading</h5>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <label><span style="color: red; font-weight: bold">*</span> Account :</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlAccount" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="false" TabIndex="2">
                                                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                    <%--<asp:ListItem Value="1" Text="UG"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="PG"></asp:ListItem>
                                                                    <asp:ListItem Value="3" Text="NGA"></asp:ListItem>
                                                                    <asp:ListItem Value="4" Text="EPC"></asp:ListItem>
                                                                    <asp:ListItem Value="5" Text="CPF"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> Department/Branch</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlDeptBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlDeptBranch_SelectedIndexChanged" TabIndex="3">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row" id="dvAuthorityPath" runat="server" visible="false">
                                            <div class="col-md-2">
                                                <label>Authority Path :</label>
                                            </div>
                                            <div class="col-md-10">
                                                <asp:Label ID="lblAuthorityPath" runat="server" CssClass="form-control" Style="background-color: whitesmoke"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group row" id="dvApproval" runat="server" visible="false">
                                            <div class="col-md-2">
                                                <label>Approval No :</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtApprovalNo" runat="server" TabIndex="4" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label>Approval Date:</label>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="Image3">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtApprovalDate" runat="server" CssClass="form-control" TabIndex="5"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                        PopupButtonID="Image3" TargetControlID="txtApprovalDate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                        DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                        OnInvalidCssClass="errordate" TargetControlID="txtApprovalDate" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <label>Approval by :</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="lblApprovedBy" runat="server" CssClass="form-control" TabIndex="6"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2" style="display: none">
                                                <asp:DropDownList ID="ddlApprovedBy" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="7">
                                                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Principal"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Management"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2" style="display: none">
                                                <asp:Label ID="lblApprovedBy1" runat="server" Style="background-color: whitesmoke" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> Budget Head :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group date">
                                                    <asp:DropDownList ID="ddlBudgethead" runat="server" AppendDataBoundItems="true" AutoPostBack="false"
                                                        ToolTip="Please Select Budget" CssClass="form-control" data-select2-enable="true" TabIndex="8" OnSelectedIndexChanged="ddlBudgethead_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvbudget" runat="server" ControlToValidate="ddlBudgethead" ErrorMessage="Please Select Budget" InitialValue="0" ValidationGroup="CASEWORKER" Display="None"></asp:RequiredFieldValidator>

                                                </div>
                                            </div>
                                            <div class="col-md-1">
                                                <div class="input-group-addon">
                                                    <asp:Label ID="lblBudgetClBal" Enabled="false" runat="server" Style="background-color: whitesmoke" Text="0"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> Ledger Head :</label>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group date">
                                                    <asp:TextBox ID="txtLedgerHead" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="9"
                                                        ToolTip="Please Enter Ledger Name" OnTextChanged="txtLedgerHead_TextChanged"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLedgerHead" ErrorMessage="Please Enter LedgerHead" ValidationGroup="CASEWORKER" Display="None"></asp:RequiredFieldValidator>

                                                    <div class="input-group-addon">
                                                        <asp:Label ID="lblLedgerClBal" runat="server" Style="background-color: whitesmoke" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                                <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtLedgerHead"
                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                    ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                </ajaxToolKit:AutoCompleteExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                    ControlToValidate="txtLedgerHead" Display="None"
                                                    ErrorMessage="Please Select Ledger" SetFocusOnError="true"
                                                    ValidationGroup="AccMoney">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Billing</h5>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> Select Type :</label>
                                            </div>
                                            <div class="col-md-10">
                                                <asp:RadioButtonList ID="rdbBillList" runat="server" RepeatColumns="2" TabIndex="10">
                                                    <asp:ListItem Value="1" Selected="True" Text="&nbsp;Bill of Supplier's &nbsp;&nbsp;"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="&nbsp;Reimbursement"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <label>Bill/Invoice No. :</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtInvoiceNo" runat="server" TabIndex="11" ToolTip="Please Enter Bill/Invoice No" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label>Bill/Invoice Date : </label>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="Image1">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtInvoiceDate" runat="server" ToolTip="Please Enter Bill/Invoice Date" CssClass="form-control"
                                                        TabIndex="12"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                        PopupButtonID="Image1" TargetControlID="txtInvoiceDate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                        DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                        OnInvalidCssClass="errordate" TargetControlID="txtInvoiceDate" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> Nature of Service /Goods etc :</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtNatureOfService" runat="server" CssClass="form-control" TabIndex="13" TextMode="MultiLine"></asp:TextBox>
                                                <%--<asp:DropDownList ID="ddlNatureService" runat="server" CssClass="form-control" TabIndex="3">
                                                                    <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">Pemanent</asp:ListItem>
                                                                    <asp:ListItem Value="1">Temporary</asp:ListItem>
                                                                </asp:DropDownList>--%>
                                            </div>
                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> Supplier's/Service Provider Name :</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtServiceName" TextMode="MultiLine" runat="server" CssClass="form-control" TabIndex="14"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtServiceName"
                                                    Display="None" ErrorMessage="Supplier's/Service Provider Name" SetFocusOnError="true" ValidationGroup="AccMoney">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="row mt-2">
                                            <div class="col-md-2">
                                                <label>Check to be Issued to Payee Name Address :</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtPayeeNameAddress" TextMode="MultiLine" TabIndex="15" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label>GSTIN NO :-</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtGSTINNo" runat="server" Style="text-transform: uppercase" CssClass="form-control" TabIndex="16"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> Bill Amount :</label>
                                                <asp:TextBox ID="txtBillAmt" runat="server" CssClass="form-control" TabIndex="17" MaxLength="13" AutoComplete="off"
                                                    onblur="CopyAmount();" ToolTip="Please Enter Bill Amount"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtBillAmt"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>


                                            <div class="col-md-8 mt-3">
                                                <label>&nbsp;</label>
                                                <asp:CheckBox ID="chkTDSApplicable" runat="server" TabIndex="18" AutoPostBack="true"
                                                    Text="&nbsp;TDS (Y/N)" OnCheckedChanged="chkTDSApplicable_CheckedChanged" />
                                                <asp:CheckBox ID="chkGST" runat="server" TabIndex="19" AutoPostBack="true"
                                                    Text="&nbsp;GST (Y/N)" OnCheckedChanged="chkGST_CheckedChanged" />
                                                <asp:CheckBox ID="chkTdsOnCGSTSGST" runat="server" TabIndex="16" AutoPostBack="true"
                                                    Text="&nbsp;TDS ON GST (Y/N)" OnCheckedChanged="chkTdsOnCGSTSGST_CheckedChanged" />
                                                <asp:CheckBox ID="chkIGST" runat="server" TabIndex="20" AutoPostBack="true"
                                                    Text="&nbsp;IGST (Y/N)" OnCheckedChanged="chkIGST_CheckedChanged" />
                                                <asp:CheckBox ID="chkTDSOnGst" runat="server" TabIndex="16" AutoPostBack="true"
                                                    Text="&nbsp;TDS ON IGST (Y/N)" OnCheckedChanged="chkTDSOnGst_CheckedChanged" />
                                            </div>
                                        </div>
                                    </div>
                                    <div id="dvSection" runat="server" visible="false" class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> TDS On Amount :</label>
                                                <asp:TextBox ID="txtTdsOnAmt" runat="server" TabIndex="19" CssClass="form-control" Style="text-align: right"
                                                    onkeypress="CheckAmount(this);" onblur="CalPerAmountforTDS(this)" ToolTip="Please enter TDS On Amount"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtTdsOnAmt"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> Section:</label>
                                                <asp:DropDownList ID="ddlSection" runat="server" TabIndex="21" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true"
                                                    OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> Per(%):</label>
                                                <asp:TextBox ID="txtTDSPer" runat="server" TabIndex="22" CssClass="form-control"
                                                    onblur="javascript:CalcTDS();" ToolTip="Please Enter Percentage"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtTDSPer"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> TDS Amount :</label>
                                                <asp:TextBox ID="txtTDSAmt" runat="server" TabIndex="23" CssClass="form-control"
                                                    onclick="CalcTDS();" onblur="javascript:CalcNet();" ToolTip="Please enter TDS Amount"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtTDSAmt"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                <asp:HiddenField runat="server" ID="hdntxtTDSAmt" />
                                            </div>
                                            <div class="form-group col-md-3">
                                                <label><span style="color: red">*</span> Pan No :</label>
                                                <asp:TextBox ID="txtPanNo" runat="server" MaxLength="10" TabIndex="24" Style="text-transform: uppercase"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divTdsOnGst" runat="server" visible="false" class="form-group col-md-12">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> TDS(IGST) On Amount :</label>
                                                <asp:TextBox ID="txtTDSGSTonAmount" runat="server" TabIndex="19" CssClass="form-control"
                                                    onkeypress="CheckAmountTdsGst(this);" onblur="CalPerAmountforTDSonGst(this)" ToolTip="Please enter TDS On Amount"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtTDSGSTonAmount"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> Section:</label>
                                                <asp:DropDownList ID="ddlTDSonGSTSection" runat="server" TabIndex="17" CssClass="form-control" data-select2-enable="true" AutoPostBack="true"
                                                    AppendDataBoundItems="true" OnSelectedIndexChanged="ddlTDSonGSTSection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> Per(%):</label>
                                                <asp:TextBox ID="txtTDSonGSTPer" runat="server" Enabled="false" TabIndex="18" CssClass="form-control"
                                                    onblur="javascript:CalcTDSonGst();" ToolTip="Please Enter Percentage"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txtTDSonGSTPer"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <%--07/03/2022 tanu--%>
                                                <label><span style="color: red">*</span> TDS On IGST Amount :</label>
                                                <asp:TextBox ID="txtTDSonGSTAmount" runat="server" TabIndex="19" CssClass="form-control"
                                                    onchange="TDSonIGSTAmountCopy(this.value)" ToolTip="Please enter TDS Amount"></asp:TextBox>
                                                <%--Enabled="false"  onclick="CalcTDSonGst();"--%>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="txtTDSonGSTAmount"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                <asp:HiddenField ID="hdntxtTDSonGSTAmount" runat="server" />
                                            </div>
                                        </div>

                                    </div>

                                    <div id="divTdsOnCGST" runat="server" visible="false" class="form-group col-md-12">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> TDS(CGST) On Amount :</label>
                                                <asp:TextBox ID="txtTDSCGSTonAmount" runat="server" TabIndex="19" CssClass="form-control"
                                                    onblur="CalPerAmountforTDSonCGst(this)" ToolTip="Please enter TDS On Amount"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" TargetControlID="txtTDSCGSTonAmount"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> Section:</label>
                                                <asp:DropDownList ID="ddlTDSonCGSTSection" runat="server" TabIndex="17" CssClass="form-control" data-select2-enable="true" AutoPostBack="true"
                                                    AppendDataBoundItems="true" OnSelectedIndexChanged="ddlTDSonCGSTSection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> Per(%):</label>
                                                <asp:TextBox ID="txtTDSonCGSTPer" runat="server" TabIndex="18" Enabled="false" CssClass="form-control"
                                                    onblur="javascript:CalcTDSonCGst();" ToolTip="Please Enter Percentage"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" TargetControlID="txtTDSonCGSTPer"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> TDS On CGST Amount :</label>
                                                <asp:TextBox ID="txtTDSonCGSTAmount" runat="server" TabIndex="19" Enabled="true" CssClass="form-control"
                                                    ToolTip="Please enter TDS Amount" onchange="TDSonCGSTAmountCopy(this.value)"></asp:TextBox><%--  onblur="CalcTDSonCGst();"--%>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" TargetControlID="txtTDSonCGSTAmount"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                <asp:HiddenField runat="server" ID="hdntxtTDSonCGSTAmount" />

                                            </div>
                                        </div>
                                    </div>

                                    <div id="divTDSOnSGST" runat="server" visible="false" class="form-group col-md-12">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> TDS(SGST) On Amount :</label>
                                                <asp:TextBox ID="txtTDSSGSTonAmount" runat="server" TabIndex="19" CssClass="form-control"
                                                    onblur="CalPerAmountforTDSonSGst(this)" ToolTip="Please enter TDS On Amount"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" TargetControlID="txtTDSSGSTonAmount"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> Section:</label>
                                                <asp:DropDownList ID="ddlTDSonSGSTSection" runat="server" TabIndex="17" CssClass="form-control" AutoPostBack="true"
                                                    AppendDataBoundItems="true" OnSelectedIndexChanged="ddlTDSonSGSTSection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> Per(%):</label>
                                                <asp:TextBox ID="txtTDSonSGSTPer" runat="server" TabIndex="18" Enabled="false" CssClass="form-control"
                                                    onblur="javascript:CalcTDSonSGst();" ToolTip="Please Enter Percentage"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" TargetControlID="txtTDSonSGSTPer"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> TDS On SGST Amount :</label>
                                                <asp:TextBox ID="txtTDSonSGSTAmount" runat="server" Enabled="true" TabIndex="19" CssClass="form-control"
                                                    ToolTip="Please enter TDS Amount" onclick="CalcTDSonSGst();" onchange="TDSonSGSTAmountCopy(this.value)"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" TargetControlID="txtTDSonSGSTAmount"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                <asp:HiddenField ID="hdntxtTDSonSGSTAmount" runat="server" />
                                            </div>
                                        </div>

                                    </div>

                                    <div id="dvIGST" runat="server" visible="false" class="col-md-12">
                                        <div id="Div2" class="col-md-3" runat="server" visible="false">
                                            <label><span style="color: red">*</span> IGST Section:</label>
                                            <asp:DropDownList ID="ddlIgstSection" runat="server" TabIndex="17" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true"
                                                OnSelectedIndexChanged="ddlIgstSection_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>

                                        <div runat="server" id="Div3" visible="false">
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>IGST Ledger :</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="false" CssClass="form-control" TabIndex="8" ToolTip="Please Enter IGST Ledger Name"></asp:TextBox>
                                                    <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" TargetControlID="txtIGSTLedger"
                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                        ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                    </ajaxToolKit:AutoCompleteExtender>
                                                </div>
                                            </div>
                                            <div runat="server" id="divTdsonGstLedger" visible="false">
                                                <div class="col-md-2">
                                                    <label>TDS On IGST Account:</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtTDSonGSTLedger" runat="server" CssClass="form-control" ToolTip="Please Enter TDS Ledger"></asp:TextBox>
                                                    <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" TargetControlID="txtTDSonGSTLedger"
                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                        ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                    </ajaxToolKit:AutoCompleteExtender>
                                                </div>

                                            </div>

                                        </div>


                                        <div class="form-group row">
                                            <div runat="server" id="divTdsonSGstLedger" visible="false">
                                                <div class="col-md-2">
                                                    <label>TDS On SGST Account:</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtTDSonSGSTLedger" runat="server" CssClass="form-control" ToolTip="Please Enter TDS Ledger"></asp:TextBox>
                                                    <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender9" runat="server" TargetControlID="txtTDSonSGSTLedger"
                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                        ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                    </ajaxToolKit:AutoCompleteExtender>
                                                </div>
                                            </div>
                                            <div runat="server" id="divTdsonCGstLedger" visible="false">
                                                <div class="col-md-2">
                                                    <label>TDS On CGST Account:</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtTDSonCGSTLedger" runat="server" CssClass="form-control" ToolTip="Please Enter TDS Ledger"></asp:TextBox>
                                                    <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender10" runat="server" TargetControlID="txtTDSonCGSTLedger"
                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                        ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                    </ajaxToolKit:AutoCompleteExtender>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> IGST Per(%):</label>
                                                <asp:TextBox ID="txtIgstPer" runat="server" TabIndex="18" CssClass="form-control" Style="text-align: right"
                                                    onblur="javascript:CalIGST();" ToolTip="Please Enter Percentage" MaxLength="5"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtIgstPer"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> IGST Amount :</label>
                                                <asp:TextBox ID="txtIgstAmount" runat="server" TabIndex="19" CssClass="form-control" Style="text-align: right"
                                                    onblur="CalIGST();" ToolTip="Please enter IGST Amount"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtIgstAmount"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <label>&nbsp;</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="dvCgst" runat="server" visible="false" class="col-md-12">
                                        <div class="row">
                                            <div id="Div4" class="col-md-3" runat="server" visible="false">
                                                <label><span style="color: red">*</span> CGST Section:</label>
                                                <asp:DropDownList ID="ddlCgstSection" runat="server" TabIndex="17" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true"
                                                    OnSelectedIndexChanged="ddlCgstSection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> CGST Per(%) :</label>
                                                <asp:TextBox ID="txtCGSTPER" runat="server" TabIndex="18" CssClass="form-control" Style="text-align: right"
                                                    onblur="CalGST(1);" ToolTip="Please Enter Percentage" MaxLength="5"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtCGSTPER"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> CGST GST Amount :</label>
                                                <asp:TextBox ID="txtCgstAmount" runat="server" TabIndex="19" CssClass="form-control" Style="text-align: right"
                                                    onblur="CalGST(1);" ToolTip="Please enter CGST Amount"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtCgstAmount"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <label>&nbsp;</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="dvSgst" runat="server" visible="false" class="col-md-12">
                                        <div class="row">
                                            <div id="Div5" class="col-md-3" runat="server" visible="false">
                                                <label><span style="color: red">*</span> SGST Section:</label>
                                                <asp:DropDownList ID="ddlSgstSection" runat="server" TabIndex="17" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true"
                                                    OnSelectedIndexChanged="ddlSgstSection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> SGST Per(%):</label>
                                                <asp:TextBox ID="txtSgstPer" runat="server" TabIndex="18" CssClass="form-control" Style="text-align: right"
                                                    onblur="CalSGST(2);" ToolTip="Please Enter Percentage"></asp:TextBox>
                                                <%--CalGST(2)--%>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtSgstPer"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> SGST Amount :</label>
                                                <asp:TextBox ID="txtSgstAmount" runat="server" TabIndex="19" CssClass="form-control" Style="text-align: right"
                                                    onblur="CalGSTTxt(2);" ToolTip="Please enter SGST Amount"></asp:TextBox>
                                                <%-- "javascript:CalGST(2);"--%>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtSgstAmount"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 mt-2">
                                        <div class="row">
                                            <div class="form-group col-md-3">
                                                <label>GST Amount:</label>
                                                <asp:TextBox ID="txtGSTAmount" runat="server" TabIndex="34" CssClass="form-control" MaxLength="13" ReadOnly="true"
                                                    onblur="javascript:CalcNet();" ToolTip="Please Enter GST Amount"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtGSTAmount"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> Total Bill Amount :</label>
                                                <asp:TextBox ID="txtTotalBillAmt" runat="server" TabIndex="35" CssClass="form-control" MaxLength="13"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtTotalBillAmt"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <label><span style="color: red"></span>Total Deduction:</label>
                                                <asp:TextBox ID="txtTotalTDSAmt" runat="server" TabIndex="99" Enabled="false" CssClass="form-control" MaxLength="13" Style="text-align: right"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" TargetControlID="txtTotalTDSAmt"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="form-group col-md-3">
                                                <label><span style="color: red">*</span> Net Amount :</label>
                                                <asp:TextBox ID="txtNetAmt" runat="server" Enabled="false"  CssClass="form-control" TabIndex="36" MaxLength="13"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtNetAmt"
                                                    FilterType="Custom, Numbers" ValidChars="." />
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-md-1">
                                                <label>Remark / Note :</label>
                                            </div>
                                            <div class="col-md-11">
                                                <asp:TextBox TextMode="MultiLine" runat="server" TabIndex="37" ID="txtRemark" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12" id="dvLedgers" runat="server" visible="true">
                                        <div class="sub-heading">
                                            <h5>Transaction Details</h5>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <span style="color: red ; font-weight: bold">*</span><label>Transaction Date :</label>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="Image33">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtTransDate" runat="server" CssClass="form-control" TabIndex="38"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                        PopupButtonID="Image33" TargetControlID="txtTransDate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                        DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                        OnInvalidCssClass="errordate" TargetControlID="txtTransDate" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                     <asp:RequiredFieldValidator ID="rfvTransDate" runat="server" Display="None" ErrorMessage="Please Select Transaction Date" ValidationGroup="CASEWORKER" ControlToValidate="txtTransDate"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-md-2" id="tdslabel" runat="server">
                                                <label><span style="color: red">*</span> TDS Ledger:</label>
                                            </div>
                                            <div class="col-md-4" id="tdstextbox" runat="server">
                                                <asp:TextBox ID="txtTDSLedger" runat="server" AutoPostBack="false" CssClass="form-control" TabIndex="39" ToolTip="Please Enter TDS Ledger Name"></asp:TextBox>
                                                <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtTDSLedger"
                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                    ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                </ajaxToolKit:AutoCompleteExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="None" ErrorMessage="Please Select Tds Ledger" ValidationGroup="CASEWORKER" ControlToValidate="txtTDSLedger"></asp:RequiredFieldValidator>

                                            </div>

                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> Select Cash/Bank :</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtBankLedger" runat="server" AutoPostBack="false" CssClass="form-control" TabIndex="40" ToolTip="Please Enter TDS Ledger Name"></asp:TextBox>
                                                <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="txtBankLedger"
                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                    ServiceMethod="GetAgainstAccount" OnClientShowing="clientShowing">
                                                </ajaxToolKit:AutoCompleteExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="None" ErrorMessage="Please Select Cash/Bank Ledger" ValidationGroup="CASEWORKER" ControlToValidate="txtBankLedger"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="form-group row" runat="server" id="dvGSTLedger">
                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> SGST Ledger :</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtSgstLedger" runat="server" AutoPostBack="false" CssClass="form-control" TabIndex="41" ToolTip="Please Enter SGST Ledger Name"></asp:TextBox>
                                                <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" TargetControlID="txtSgstLedger"
                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                    ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                </ajaxToolKit:AutoCompleteExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None" ErrorMessage="Please Select Sgst Ledger" ValidationGroup="CASEWORKER" ControlToValidate="txtSgstLedger"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> CGST Ledger :</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtCGSTLedger" runat="server" AutoPostBack="false" CssClass="form-control" TabIndex="42" ToolTip="Please Enter CGST Ledger Name"></asp:TextBox>
                                                <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" TargetControlID="txtCGSTLedger"
                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                    ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                </ajaxToolKit:AutoCompleteExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="None" ErrorMessage="Please Select Cgst Ledger" ValidationGroup="CASEWORKER" ControlToValidate="txtCGSTLedger"></asp:RequiredFieldValidator>

                                            </div>

                                        </div>

                                        <div class="form-group row" runat="server" id="dvIgstledger">
                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> IGST Ledger :</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtIGSTLedger" runat="server" AutoPostBack="false" CssClass="form-control" TabIndex="43" ToolTip="Please Enter IGST Ledger Name"></asp:TextBox>
                                                <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" TargetControlID="txtIGSTLedger"
                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                    ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                </ajaxToolKit:AutoCompleteExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None" ErrorMessage="Please Select Igst Ledger" ValidationGroup="CASEWORKER" ControlToValidate="txtIGSTLedger"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <label>Narration :</label>
                                            </div>
                                            <div class="col-md-10">
                                                <asp:TextBox ID="txtNarration" runat="server" TextMode="MultiLine" Width="100%" TabIndex="44" ToolTip="Enter Narration for Voucher"></asp:TextBox>
                                            </div>
                                        </div>



                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <label>Select :</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlSelect" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select Approve/Reject"
                                                    AppendDataBoundItems="true" TabIndex="45" AutoPostBack="true">
                                                    <asp:ListItem Value="F">Forward To Next Authority(Recommended)</asp:ListItem>
                                                    <%--<asp:ListItem Value="A">Approve & Final Submit</asp:ListItem>--%>
                                                    <asp:ListItem Value="R">Return</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <label>Approval Remarks :</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtApproveRemarks" runat="server" TextMode="MultiLine" CssClass="form-control" TabIndex="46"
                                                    ToolTip="Enter Remarks" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <div class="" id="dvbuttons" runat="server">
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="47" OnClientClick="return AskSave();" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-warning" TabIndex="48" OnClick="btnBack_Click" />
                                        <asp:ValidationSummary ID="vsCaseWorker" runat="server" ValidationGroup="CASEWORKER" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />

                                    </div>
                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
