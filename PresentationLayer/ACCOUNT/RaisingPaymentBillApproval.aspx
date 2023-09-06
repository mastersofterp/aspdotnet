<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master"
    CodeFile="RaisingPaymentBillApproval.aspx.cs" Inherits="ACCOUNT_RaisingPaymentBillApproval" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>--%>
    <style>
        .drp-txt {
            border-top: 0!important;
            border-left: 0!important;
            border-right: 0!important;
        }
        /*table {
            height: auto;
            display: block;
            overflow-x:auto;
            overflow-y:auto;
        }*/
    </style>
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
            if (document.getElementById('<%=txtLedgerHead.ClientID%>').value == '') {
                alert('Please Enter Party Name.');
                return false;
            }
            if (document.getElementById('<%=txtExpenseLedger.ClientID%>').value == '') {
                alert('Please Enter Expense Ledger.');
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
            var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>');
            if (chkSecurity.checked) {
                if (document.getElementById('<%=txtSecurityAmt.ClientID%>').value == '' || document.getElementById('<%=txtSecurityAmt.ClientID%>').value == 0) {
                    alert('Please Enter Security  Amount.');
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
                    var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>').checked;
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
                    if (chkSecurity) {
                        SecurityAmount = document.getElementById('<%=txtSecurityAmt.ClientID%>').value != '' ? document.getElementById('<%=txtSecurityAmt.ClientID%>').value : 0;
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
                    var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>').checked;
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
                    if (chkSecurity) {
                        SecurityAmount = document.getElementById('<%=txtSecurityAmt.ClientID%>').value;
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
                    var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>').checked;
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
                    if (chkSecurity) {
                        SecurityAmount = document.getElementById('<%=txtSecurityAmt.ClientID%>').value;
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

                    var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>').checked;
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
                    if (chkSecurity) {
                        SecurityAmount = document.getElementById('<%=txtSecurityAmt.ClientID%>').value;
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
                var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>').checked;
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
                if (chkSecurity) {
                    SecurityAmount = document.getElementById('<%=txtSecurityAmt.ClientID%>').value;
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
                var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>').checked;
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
                if (chkSecurity) {
                    SecurityAmount = document.getElementById('<%=txtSecurityAmt.ClientID%>').value;
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
                var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>').checked;
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
                if (chkSecurity) {
                    SecurityAmount = document.getElementById('<%=txtSecurityAmt.ClientID%>').value;
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
            var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>').checked;
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
            if (chkSecurity && document.getElementById('<%=txtSecurityAmt.ClientID%>').value != '') {
                SecurityAmount = document.getElementById('<%=txtSecurityAmt.ClientID%>').value;
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
                var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>').checked;
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
                if (chkSecurity) {
                    SecurityAmount = document.getElementById('<%=txtSecurityAmt.ClientID%>').value;
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
                var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>').checked;
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
                if (chkSecurity) {
                    SecurityAmount = document.getElementById('<%=txtSecurityAmt.ClientID%>').value;
                }

                var amountnet = parseFloat(parseFloat(BillAmt2.value) + parseFloat(CgstAmount) + parseFloat(SgstAmount) + parseFloat(IgstAmount)).toFixed(2);
                document.getElementById("<%=txtTotalBillAmt.ClientID%>").value = parseFloat(parseFloat(BillAmt2.value) + parseFloat(IgstAmount)).toFixed(2);
                document.getElementById('<%=txtNetAmt.ClientID%>').value = parseFloat(amountnet - TdsAmount - TdsonGSTAmount - TdsonCGSTAmount - TdsonSGSTAmount - SecurityAmount).toFixed(2);
                //document.getElementById('<%=txtTotalTDSAmt.ClientID%>').value = parseFloat(TdsAmount + TdsonGSTAmount + TdsonCGSTAmount + TdsonSGSTAmount + SecurityAmount).toFixed(2);
                document.getElementById('<%=txtTotalTDSAmt.ClientID%>').value = parseFloat(parseFloat(TdsAmount) + parseFloat(TdsonGSTAmount) + parseFloat(TdsonCGSTAmount) + parseFloat(TdsonSGSTAmount) + parseFloat(SecurityAmount)).toFixed(2);
            }
        }

        function CalSECURITY() {

            var chkIGST2 = document.getElementById('<%= chkSecurity.ClientID%>').checked;
            var BillAmt2 = document.getElementById('<%= txtBillAmt.ClientID %>');
            document.getElementById('<%=txtSecurityAmt.ClientID%>').value = parseFloat(document.getElementById('<%=txtSecurityAmt.ClientID%>').value).toFixed(2);
            if (chkIGST2) {
                IGSTAmt2 = document.getElementById('<%=txtSecurityAmt.ClientID%>').value;
                document.getElementById('<%=txtSecurityPer.ClientID%>').value = parseFloat(document.getElementById('<%=txtSecurityPer.ClientID%>').value).toFixed(2);
                IGSTPer2 = document.getElementById('<%=txtSecurityPer.ClientID%>').value;
                CalIGST2 = parseFloat(BillAmt2.value) * parseFloat(IGSTPer2) * 0.01;

                document.getElementById('<%=txtSecurityAmt.ClientID%>').value = parseFloat(CalIGST2).toFixed(2);



                var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                var chkTDSonGSt4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                var chkTDSonCGStSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;
                var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>').checked;
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
                if (chkSecurity) {
                    SecurityAmount = document.getElementById('<%=txtSecurityAmt.ClientID%>').value;
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

            var chkIGST2 = document.getElementById('<%= chkSecurity.ClientID%>').checked;
            var BillAmt2 = document.getElementById('<%= txtBillAmt.ClientID %>');

            if (chkIGST2) {

                var chkGST4 = document.getElementById('<%= chkGST.ClientID%>').checked;
                var chkIGST4 = document.getElementById('<%= chkIGST.ClientID%>').checked;
                var chkTDS4 = document.getElementById('<%=chkTDSApplicable.ClientID%>').checked;
                var chkTDSonGSt4 = document.getElementById('<%=chkTDSOnGst.ClientID%>').checked;
                var chkTDSonCGStSGST4 = document.getElementById('<%=chkTdsOnCGSTSGST.ClientID%>').checked;
                var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>').checked;
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
                if (chkSecurity && document.getElementById('<%=txtSecurityAmt.ClientID%>').value != '') {
                    SecurityAmount = document.getElementById('<%=txtSecurityAmt.ClientID%>').value;
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
            var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>').checked;
            var SecurityAmount = 0;
            var TdsonGstamt = 0;
            var TdsonCGstamt = 0;
            var TdsonSGstamt = 0;
            if (chkTDSonGSt) {
                TdsonGstamt = document.getElementById('<%=txtTDSonGSTAmount.ClientID%>').value;
            }
            if (chkSecurity) {
                SecurityAmount = document.getElementById('<%=txtSecurityAmt.ClientID%>').value;
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
    <%--<script type="text/javascript">
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
                    var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>').checked;
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

                    if (chkSecurity) {
                        SecurityAmount = document.getElementById('<%=txtSecurityAmt.ClientID%>').value;
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
                    var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>').checked;
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
                    if (chkSecurity) {
                        SecurityAmount = document.getElementById('<%=txtSecurityAmt.ClientID%>').value;
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
                    var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>').checked;
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
                    if (chkSecurity) {
                        SecurityAmount = document.getElementById('<%=txtSecurityAmt.ClientID%>').value;
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
                    var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>').checked;
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
                    if (chkSecurity) {
                        SecurityAmount = document.getElementById('<%=txtSecurityAmt.ClientID%>').value;
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
                    var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>').checked;
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

                    if (chkSecurity) {
                        SecurityAmount = document.getElementById('<%=txtSecurityAmt.ClientID%>').value;
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
                    var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>').checked;
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
                    if (chkSecurity) {
                        SecurityAmount = document.getElementById('<%=txtSecurityAmt.ClientID%>').value;
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
                    var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>').checked;
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
                    if (chkSecurity) {
                        SecurityAmount = document.getElementById('<%=txtSecurityAmt.ClientID%>').value;
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
                    var chkSecurity = document.getElementById('<%=chkSecurity.ClientID%>').checked;
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
                    if (chkSecurity) {
                        SecurityAmount = document.getElementById('<%=txtSecurityAmt.ClientID%>').value;
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

    <input id="hdnAskSave" runat="server" type="hidden">

    <%-- <asp:UpdatePanel ID="UPDLedger" runat="server">
            <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Payment Bill Approval</h3>
                </div>
                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                <div class="box-body">
                      <asp:Panel ID="pnlBillList" runat="server">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Account</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCompAccount" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="false" TabIndex="2">
                                            <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label></label>
                                </div>
                                <asp:Button ID="btnShow" Text="Show" runat="server" CssClass="btn btn-primary" ToolTip="Click to Show Pending Bill" OnClick="btnShow_Click" />

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
                                            <table class="table table-striped table-bordered " style="width: 100%" id="tbl">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th id="thAction" runat="server">Action
                                                        </th>
                                                        <th>Status
                                                        </th>
                                                        <th>Trans.No
                                                        </th>
                                                        <th>Name
                                                        </th>
                                                        <th>Nature Of Work
                                                        </th>
                                                        <%-- <th>TOTAL_BILL_AMT
                                                                                    </th>--%>
                                                        <th>Net Amount
                                                        </th>
                                                        <th>Bill Type
                                                        </th>
                                                        <th>Bill Raised By
                                                        </th>
                                                        <th>Authority Remarks</th>
                                                        <th>Approve/Reject
                                                        </th>
                                                        <th style="display: none">Modify
                                                        </th>
                                                         <th style="display: none">Voucher Report
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
                                                <asp:Panel ID="pnlShowQA" runat="server" Style="cursor: pointer; vertical-align: top; float: left">
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
                                                <%--  <%# Eval("BILL_NO")%>--%>
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
                                                <%# Eval("UA_FULLNAME") %>
                                            </td>
                                            <td>
                                                <%# Eval("RETURN_REMARK") %>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnApproval" runat="server" Text="Select" CommandArgument='<%# Eval("RAISE_PAY_NO")%>' TabIndex="2"
                                                    ToolTip="Select to Approve/Reject" OnClick="btnApproval_Click" CssClass="btn btn-primary" />
                                            </td>
                                            <td style="display: none">
                                                <asp:Button ID="btnModify" runat="server" Text="Modify" CommandArgument='<%# Eval("RAISE_PAY_NO")%>' TabIndex="3"
                                                    ToolTip="Select to Modify and Approve Bill" OnClick="btnModify_Click" CssClass="btn btn-primary" />
                                            </td>
                                             <td id="idPrint" runat="server">
                                                    <asp:ImageButton ID="btnPrintv" runat="server" ImageUrl="~/Images/print.png" Width="25px" CommandArgument='<%# Eval("BILL_NO") %>'
                                                        AlternateText="Voucher Print" ToolTip="Edit Record" OnClick="btnPrint_Click1" />
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
                                            ExpandedText="Hide Reason" CollapsedText="Show Reason" CollapsedImage="~/images/action_down.png"
                                            ExpandedImage="~/images/action_up.png" ImageControlID="imgExp" Collapsed="true">
                                        </ajaxToolKit:CollapsiblePanelExtender>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                            <%--      <div class="vista-grid_datapager">
                                                        <div class="text-center">
                                                            <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvPendingList" PageSize="10"
                                                                OnPreRender="dpPager_PreRender">
                                                                <Fields>
                                                                    <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                                        ShowLastPageButton="false" ShowNextPageButton="false" />
                                                                    <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
                                                                    <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                                        ShowLastPageButton="true" ShowNextPageButton="true" />
                                                                </Fields>
                                                            </asp:DataPager>
                                                        </div>
                                                    </div>--%>
                        </asp:Panel>


                    <asp:Panel ID="pnlBillDetails" runat="server" Visible="false">
                        <%--  <div class="col-12">
                            <div class="sub-heading">
                                <h5>Raising Payment Bill</h5>
                            </div>
                        </div>--%>

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
                                <div class="col-12">
                                    <div class="form-group row">
                                        <div class="col-md-2">
                                            <label>Transaction No </label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label ID="lblBillId" runat="server" Text="" CssClass="form-control" Style="color: red; font-weight: bold" Visible="false"></asp:Label>
                                            <asp:Label ID="lblSerialNo" runat="server" Text="" CssClass="form-control" Style="color: red; font-weight: bold" ></asp:Label>

                                        </div>
                                        <div class="col-md-2">
                                            <label><span style="color: red">*</span> Select Company </label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlSelectCompany" runat="server" TabIndex="1" AppendDataBoundItems="true" AutoPostBack="true"
                                                CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSelectCompany_SelectedIndexChanged">
                                                <%--   <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>--%>
                                            </asp:DropDownList>

                                            <asp:HiddenField ID="hdnTDSonCGSTAmount" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnGSTAmount" runat="server" Value="0" />

                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <%--  <div class="sub-heading">
                                        <h5>Heading</h5>
                                    </div>--%>
                                    <div class="panel-body">
                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <label><span style="color: red; font-weight: bold">*</span>  Account</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlAccount" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="false" TabIndex="2" Enabled="false">
                                                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> Department/Branch</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlDeptBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="false" Enabled="false"
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
                                                <asp:TextBox ID="lblApprovedBy" runat="server" ToolTip="Please Enter Approval Name" TabIndex="6" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2" style="display: none">
                                                <asp:DropDownList ID="ddlApprovedBy" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="6">
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
                                                <label>Budget Group </label>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group date">
                                                    <asp:DropDownList ID="ddlBudgethead" runat="server" AppendDataBoundItems="true" AutoPostBack="true"  Enabled="false"
                                                        ToolTip="Please Select Budget Group" CssClass="form-control " data-select2-enable="true" TabIndex="7" OnSelectedIndexChanged="ddlBudgethead_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:Label ID="lblBudgetClBal" Enabled="false" runat="server" CssClass="form-control drp-txt" Text="0"></asp:Label>

                                            </div>
                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> Party Name  </label>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group date">
                                                    <asp:TextBox ID="txtLedgerHead" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="8"  Enabled="false"
                                                        ToolTip="Please Enter Party Name" OnTextChanged="txtLedgerHead_TextChanged"></asp:TextBox>

                                                    <div class="input-group-addon">
                                                        <asp:Label ID="lblLedgerClBal" runat="server" Style="background-color: whitesmoke" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                                <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtLedgerHead"
                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                    ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                </ajaxToolKit:AutoCompleteExtender>

                                            </div>
                                        </div>
                                        <div id="trSponsor" runat="server" class="row">
                                            <div class="col-md-2">
                                                <label>Sponsor Project<span style="color: red"></span>  </label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlSponsor" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"  Enabled="false"
                                                    OnSelectedIndexChanged="ddlSponsor_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> Expense Ledger  </label>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group date">
                                                    <asp:TextBox ID="txtExpenseLedger" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="8"  Enabled="false"
                                                        ToolTip="Please Enter Expense Ledger" ></asp:TextBox>

                                                    <div class="input-group-addon">
                                                        <asp:Label ID="lblExpenseLedger" runat="server" Style="background-color: whitesmoke" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                                <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender11" runat="server" TargetControlID="txtExpenseLedger"
                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                    ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                </ajaxToolKit:AutoCompleteExtender>

                                            </div>
                                            <%-- <div class="col-md-3">
                                                                <asp:TextBox ID="TextBox1" runat="server" BorderColor="White"
                                                                    BorderStyle="None" Style="background-color: Transparent; margin-left: 0px;" ReadOnly="True"
                                                                    Font-Size="Small" Font-Bold="true"></asp:TextBox>
                                                                <asp:TextBox ID="TextBox2" runat="server" Height="23px" Width="21px" BorderColor="White"
                                                                    BorderStyle="None"
                                                                    Style="background-color: Transparent;" ReadOnly="True"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </div>--%>
                                        </div>
                                        <div class="row mt-3" runat="server" id="trSubHead">
                                            <br />
                                            <div class="col-md-2">
                                                <label>Project Sub Head</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlProjSubHead" runat="server" AppendDataBoundItems="true" Enabled="false"
                                                    CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlProjSubHead_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                Remaining amount:
                                                               <asp:Label ID="lblRemainAmt" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                        <br />

                                        <br />
                                    </div>
                                </div>

                                <div class="col-12">
                                    <%--  <div class="sub-heading">
                                        <h5>Billing</h5>
                                    </div>--%>
                                    <div class="panel-body">
                                        <div class="form-group row">
                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span>  Select Type</label>
                                            </div>
                                            <div class="col-md-10">
                                                <asp:RadioButtonList ID="rdbBillList" runat="server" RepeatColumns="2">
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
                                                <asp:TextBox ID="txtInvoiceNo" runat="server" ToolTip="Please Enter Bill/Invoice No" CssClass="form-control" TabIndex="9" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label>Bill/Invoice Date  </label>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="Image1">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>

                                                    <asp:TextBox ID="txtInvoiceDate" runat="server" ToolTip="Please Enter Bill/Invoice Date" CssClass="form-control"
                                                        TabIndex="10" Style="width: 150px"></asp:TextBox>
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
                                                <label>Nature of Service /Goods etc</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtNatureOfService" runat="server" CssClass="form-control" TabIndex="11" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2" runat="server" id="Diveservices" visible="false" Enabled="false">
                                                <label>Supplier's/Service Provider Name<span style="color: red"></span></label>
                                            </div>
                                            <div class="col-md-4" runat="server" id="DivServicesText" visible="false">
                                                <asp:TextBox ID="txtServiceName" TextMode="MultiLine" runat="server" CssClass="form-control" TabIndex="12" onchange="ServiceFunction(this.value)"></asp:TextBox>
                                                <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtServiceName"
                                                                    Display="None" ErrorMessage="Supplier's/Service Provider Name" SetFocusOnError="true" ValidationGroup="AccMoney">
                                                                </asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="col-md-2" runat="server" id="divProviderType">
                                                <label>Supplier's/Service Provider Type</label>
                                            </div>

                                            <div class="col-md-3" runat="server" id="DivddlEmptype">
                                                <asp:DropDownList ID="ddlEmpType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpType_SelectedIndexChanged"  Enabled="false">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Employee</asp:ListItem>
                                                    <asp:ListItem Value="2">Payee</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-2" id="divEmployee1" runat="server" visible="false">
                                                <label><span style="color: red">*</span> Select Employee</label>
                                            </div>
                                            <div class="col-md-4" id="divEmployee2" runat="server" visible="false">
                                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" Enabled="false">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-2" id="divPayeeNature1" runat="server" visible="false">
                                                <label><span style="color: red">*</span> Select Payee Nature</label>
                                            </div>
                                            <div class="col-md-4" id="divPayeeNature2" runat="server" visible="false">
                                                <asp:DropDownList ID="ddlPayeeNature" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPayeeNature_SelectedIndexChanged" Enabled="false">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2" id="divPayee1" runat="server" visible="false">
                                                <label><span style="color: red">*</span> Select Payee Name</label>
                                            </div>
                                            <div class="col-md-4" id="divPayee2" runat="server" visible="false">
                                                <asp:DropDownList ID="ddlPayee" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPayee_SelectedIndexChanged" Enabled="false">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>

                                        <br />
                                        <div class="row">
                                            <div class="col-md-2">
                                                <label>Cheque to be Issued to Payee Name Address </label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtPayeeNameAddress" TextMode="MultiLine" TabIndex="13" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-2">
                                                <span style="color: red; font-weight:bold">*</span><label>GSTIN NO</label>
                                            </div>

                                            <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtGSTINNo" runat="server" MaxLength="15" Style="text-transform: uppercase" CssClass="form-control" TabIndex="14" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                       <br />
                                        <div class="row">
                                            <div class="form-group col-md-2">
                                                <span style="color: red; font-weight: bold">*</span><label>Pan No. </label>
                                                <%--<span style="color: red">*</span>--%>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <asp:TextBox ID="txtPanNo" runat="server" MaxLength="10" TabIndex="20" Style="text-transform: uppercase" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class=" row">
                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> Bill Amount </label>
                                                <asp:TextBox ID="txtBillAmt" runat="server" CssClass="form-control" TabIndex="15" MaxLength="13"
                                                    autocomplete="off" onblur="CopyAmount();" AutoPostBack="true"  ToolTip="Please Enter Bill Amount" Enabled="false"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtBillAmt"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />

                                            </div>
                                            <%--<div class="col-md-2">
                                                                <label>TDS Applibale:</label>
                                                            </div>--%>
                                            <div class="col-md-1">
                                                <label>&nbsp;</label>
                                                <asp:RadioButtonList ID="rdbTDS" RepeatColumns="2" TabIndex="16" runat="server" AutoPostBack="true"
                                                    OnSelectedIndexChanged="rdbTDS_SelectedIndexChanged" CssClass="form-control" Visible="false">
                                                    <asp:ListItem Value="1" Text="&nbsp;Yes"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="&nbsp;No" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>

                                            <div class="col-md-8 mt-3">
                                                <label>&nbsp;</label>
                                                <asp:CheckBox ID="chkTDSApplicable" runat="server" TabIndex="16" AutoPostBack="true" Enabled="false"
                                                    Text="&nbsp;TDS (Y/N)" OnCheckedChanged="chkTDSApplicable_CheckedChanged" />
                                                <asp:CheckBox ID="chkGST" runat="server" TabIndex="16" AutoPostBack="true"  Enabled="false"
                                                    Text="&nbsp;GST (Y/N)" OnCheckedChanged="chkGST_CheckedChanged" />
                                                <asp:CheckBox ID="chkTdsOnCGSTSGST" runat="server" TabIndex="16" AutoPostBack="true"  Enabled="false"
                                                    Text="&nbsp;TDS ON GST (Y/N)" OnCheckedChanged="chkTdsOnCGSTSGST_CheckedChanged" />
                                                <asp:CheckBox ID="chkIGST" runat="server" TabIndex="16" AutoPostBack="true"  Enabled="false"
                                                    Text="&nbsp;IGST (Y/N)" OnCheckedChanged="chkIGST_CheckedChanged" />
                                                <asp:CheckBox ID="chkTDSOnGst" runat="server" TabIndex="16" AutoPostBack="true"  Enabled="false"
                                                    Text="&nbsp;TDS ON IGST (Y/N)" OnCheckedChanged="chkTDSOnGst_CheckedChanged" />
                                                <asp:CheckBox ID="chkSecurity" runat="server" TabIndex="16" AutoPostBack="true"  Enabled="false"
                                                    Text="&nbsp;SECURITY (Y/N)" OnCheckedChanged="chkSecurity_CheckedChanged" />
                                            </div>
                                        </div>
                                        <br />
                                        <div id="dvSection" runat="server" visible="false" class="row">
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> TDS On Amount </label>
                                                <asp:TextBox ID="txtTdsOnAmt" runat="server" TabIndex="19" CssClass="form-control" Style="text-align: right"
                                                    onkeypress="CheckAmount(this);" onblur="CalPerAmountforTDS(this)" ToolTip="Please enter TDS On Amount" Enabled="false"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtTdsOnAmt"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> Section</label>
                                                <asp:DropDownList ID="ddlSection" runat="server" TabIndex="17" CssClass="form-control" data-select2-enable="true" AutoPostBack="true"  Enabled="false"
                                                    AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> Per(%)</label>
                                                <asp:TextBox ID="txtTDSPer" runat="server" Enabled="false" TabIndex="18" CssClass="form-control" Style="text-align: right"
                                                    onblur="javascript:CalcTDS();" ToolTip="Please Enter Percentage"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtTDSPer"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span>  TDS Amount</label>
                                                <asp:TextBox ID="txtTDSAmt" runat="server" TabIndex="19" Enabled="false" CssClass="form-control" Style="text-align: right"
                                                    ToolTip="Please enter TDS Amount" onchange="TDSAmountCopy(this.value)" ></asp:TextBox><%--onclick="CalcTDS();"--%>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtTDSAmt"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                <asp:HiddenField runat="server" ID="hdntxtTDSAmt" />
                                            </div>

                                        </div>

                                        <div id="divTdsOnGst" runat="server" visible="false" class="row">

                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span>TDS(IGST) On Amount </label>
                                                <asp:TextBox ID="txtTDSGSTonAmount" runat="server" TabIndex="19" CssClass="form-control" Style="text-align: right"
                                                    onkeypress="CheckAmountTdsGst(this);" onblur="CalPerAmountforTDSonGst(this)" ToolTip="Please enter TDS On Amount" Enabled="false"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtTDSGSTonAmount"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <label>Section<span style="color: red">*</span>:</label>
                                                <asp:DropDownList ID="ddlTDSonGSTSection" runat="server" TabIndex="17" CssClass="form-control" AutoPostBack="true"  Enabled="false"
                                                    AppendDataBoundItems="true" OnSelectedIndexChanged="ddlTDSonGSTSection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> Per(%)</label>
                                                <asp:TextBox ID="txtTDSonGSTPer" runat="server" Enabled="false" TabIndex="18" CssClass="form-control" Style="text-align: right"
                                                    onblur="javascript:CalcTDSonGst();" ToolTip="Please Enter Percentage" ></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txtTDSonGSTPer"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-2">
                                                <%--07/03/2022 tanu--%>
                                                <label><span style="color: red">*</span> TDS On IGST Amount </label>
                                                <asp:TextBox ID="txtTDSonGSTAmount" runat="server" TabIndex="19" CssClass="form-control" Style="text-align: right"
                                                    onchange="TDSonIGSTAmountCopy(this.value)" ToolTip="Please enter TDS Amount" Enabled="false"></asp:TextBox>
                                                <%--Enabled="false"  onclick="CalcTDSonGst();"--%>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="txtTDSonGSTAmount"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                <asp:HiddenField ID="hdntxtTDSonGSTAmount" runat="server" />
                                            </div>


                                        </div>

                                        <div id="divTdsOnCGST" runat="server" visible="false" class="row">

                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span>  TDS(CGST) On Amount</label>
                                                <asp:TextBox ID="txtTDSCGSTonAmount" runat="server" TabIndex="19" CssClass="form-control" Style="text-align: right"
                                                    onblur="CalPerAmountforTDSonCGst(this)" ToolTip="Please enter TDS On Amount" Enabled="false"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" TargetControlID="txtTDSCGSTonAmount"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> Section</label>
                                                <asp:DropDownList ID="ddlTDSonCGSTSection" runat="server" TabIndex="17" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" Enabled="false"
                                                    AppendDataBoundItems="true" OnSelectedIndexChanged="ddlTDSonCGSTSection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> Per(%)</label>
                                                <asp:TextBox ID="txtTDSonCGSTPer" runat="server" TabIndex="18" Enabled="false" CssClass="form-control" Style="text-align: right"
                                                    onblur="javascript:CalcTDSonCGst();" ToolTip="Please Enter Percentage" ></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" TargetControlID="txtTDSonCGSTPer"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> TDS On CGST Amount </label>
                                                <asp:TextBox ID="txtTDSonCGSTAmount" runat="server" TabIndex="19" Enabled="false" CssClass="form-control" Style="text-align: right"
                                                    ToolTip="Please enter TDS Amount" onchange="TDSonCGSTAmountCopy(this.value)" ></asp:TextBox><%--onblur="CalcTDSonCGst();"--%>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" TargetControlID="txtTDSonCGSTAmount"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                <asp:HiddenField runat="server" ID="hdntxtTDSonCGSTAmount" />

                                            </div>

                                        </div>

                                        <div id="divTDSOnSGST" runat="server" visible="false" class="row">

                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> TDS(SGST) On Amount </label>
                                                <asp:TextBox ID="txtTDSSGSTonAmount" runat="server" TabIndex="19" CssClass="form-control" Style="text-align: right"
                                                    onblur="CalPerAmountforTDSonSGst(this)" ToolTip="Please enter TDS On Amount" Enabled="false"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" TargetControlID="txtTDSSGSTonAmount"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> Section</label>
                                                <asp:DropDownList ID="ddlTDSonSGSTSection" runat="server" TabIndex="17" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" Enabled="false"
                                                    AppendDataBoundItems="true" OnSelectedIndexChanged="ddlTDSonSGSTSection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> Per(%)</label>
                                                <asp:TextBox ID="txtTDSonSGSTPer" runat="server" TabIndex="18" Enabled="false" CssClass="form-control" Style="text-align: right" 
                                                    onblur="javascript:CalcTDSonSGst();" ToolTip="Please Enter Percentage"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" TargetControlID="txtTDSonSGSTPer"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> TDS On SGST Amount </label>
                                                <asp:TextBox ID="txtTDSonSGSTAmount" runat="server" Enabled="true" TabIndex="19" CssClass="form-control" Style="text-align: right" 
                                                    ToolTip="Please enter TDS Amount" onchange="TDSonSGSTAmountCopy(this.value)"></asp:TextBox><%--onclick="CalcTDSonSGst();"--%>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" TargetControlID="txtTDSonSGSTAmount"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                <asp:HiddenField ID="hdntxtTDSonSGSTAmount" runat="server" />
                                            </div>
                                        </div>
                                        <br />

                                        <div id="dvIGST" runat="server" visible="false" class="row">
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span>IGST Per(%)</label>
                                                <asp:TextBox ID="txtIgstPer" runat="server" TabIndex="18" CssClass="form-control" Style="text-align: right" Enabled="false"
                                                    onblur="javascript:CalIGST();" ToolTip="Please Enter Percentage"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtIgstPer"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> IGST Amount</label>
                                                <asp:TextBox ID="txtIgstAmount" runat="server" TabIndex="19" CssClass="form-control" Style="text-align: right" Enabled="false"
                                                    onblur="CalIGSTTxt();" ToolTip="Please enter IGST Amount"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtIgstAmount"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div id="Div1" class="col-md-3" runat="server" visible="false">
                                                <label>IGST Section<span style="color: red">*</span>:</label>
                                                <asp:DropDownList ID="ddlIgstSection" runat="server" TabIndex="17" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true" Enabled="false"
                                                    OnSelectedIndexChanged="ddlIgstSection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3"></div>

                                            <div class="col-md-3">
                                                <label>&nbsp;</label>
                                            </div>
                                        </div>
                                        <br />

                                        <div id="dvCgst" runat="server" visible="false" class="row">
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> CGST Per(%)</label>
                                                <asp:TextBox ID="txtCGSTPER" runat="server" TabIndex="18" CssClass="form-control" Style="text-align: right" Enabled="false"
                                                    onblur="CalGST(1);" ToolTip="Please Enter Percentage"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtCGSTPER"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> CGST Amount </label>
                                                <asp:TextBox ID="txtCgstAmount" runat="server" TabIndex="19" CssClass="form-control" Style="text-align: right" Enabled="false"
                                                    onblur="CalGSTTxt(1);" ToolTip="Please enter CGST Amount"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtCgstAmount"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span>SGST Per(%)</label>
                                                <asp:TextBox ID="txtSgstPer" runat="server" TabIndex="18" CssClass="form-control" Style="text-align: right" Enabled="false"
                                                    onblur="CalSGST(2);" ToolTip="Please Enter Percentage"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtSgstPer"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-2">
                                                <label><span style="color: red">*</span> SGST Amount </label>
                                                <asp:TextBox ID="txtSgstAmount" runat="server" TabIndex="19" CssClass="form-control" Style="text-align: right" Enabled="false"
                                                    onblur="CalGSTTxt(2);" ToolTip="Please enter SGST Amount"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtSgstAmount"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>

                                            <div id="Div3" class="col-md-3" runat="server" visible="false">
                                                <label><span style="color: red">*</span> CGST Section</label>
                                                <asp:DropDownList ID="ddlCgstSection" runat="server" TabIndex="17" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true" Enabled="false"
                                                    OnSelectedIndexChanged="ddlCgstSection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3"></div>

                                            <div class="col-md-3">
                                                <label>&nbsp;</label>
                                            </div>
                                        </div>
                                        <br />
                                        <div id="divSecurity" runat="server" visible="false" class="row">
                                            <div class="col-md-2" id="divPer" runat="server" visible="false">
                                                <label><span style="color: red">*</span> Security Per(%)</label>
                                                <asp:TextBox ID="txtSecurityPer" runat="server" TabIndexv="18" CssClass="form-control" Style="text-align: right"
                                                    onblur="javascript:CalSECURITY();" ToolTip="Please Enter Percentage" Enabled="false"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server" TargetControlID="txtSecurityPer"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div class="col-md-3">
                                                <label><span style="color: red">*</span> Security Amount </label>
                                                <asp:TextBox ID="txtSecurityAmt" runat="server" TabIndex="19" CssClass="form-control" Style="text-align: right"
                                                    onblur="CalSECURITYAmt();" ToolTip="Please enter IGST Amount" Enabled="false"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" TargetControlID="txtSecurityAmt"
                                                    FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                            </div>
                                            <div id="Div6" class="col-md-3" runat="server" visible="false">
                                            </div>
                                            <div class="col-md-3"></div>

                                            <div class="col-md-3">
                                                <label>&nbsp;</label>
                                            </div>
                                        </div>
                                        <br />
                                        <div id="dvSgst" runat="server" visible="false" class="form-group col-md-12">

                                            <div id="Div4" class="col-md-3" runat="server" visible="false">
                                                <label><span style="color: red">*</span>SGST Section</label>
                                                <asp:DropDownList ID="ddlSgstSection" runat="server" TabIndex="17" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true"
                                                    OnSelectedIndexChanged="ddlSgstSection_SelectedIndexChanged" Enabled="false">
                                                </asp:DropDownList>
                                            </div>


                                        </div>


                                        <div class="form-group col-md-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>GST Amount</label>
                                                    </div>
                                                    <asp:TextBox ID="txtGSTAmount" runat="server" TabIndex="21" Enabled="false" CssClass="form-control" MaxLength="13"
                                                        ToolTip="Please Enter GST Amount"></asp:TextBox>
                                                    <%--onblur="javascript:CalcNet();"--%>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtGSTAmount"
                                                        FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Gross Amount</label>
                                                    </div>
                                                    <asp:TextBox ID="txtTotalBillAmt" runat="server" TabIndex="99" Enabled="false" CssClass="form-control" MaxLength="13" Style="text-align: right"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtTotalBillAmt"
                                                        FilterType="Custom, Numbers" ValidChars="." Enabled="True" />

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Total Deduction</label>
                                                    </div>
                                                    <asp:TextBox ID="txtTotalTDSAmt" runat="server" TabIndex="99" Enabled="false" CssClass="form-control" MaxLength="13" Style="text-align: right"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" TargetControlID="txtTotalTDSAmt"
                                                        FilterType="Custom, Numbers" ValidChars="." Enabled="True" />

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Net Amount</label>
                                                    </div>
                                                    <asp:TextBox ID="txtNetAmt" runat="server" CssClass="form-control" Enabled="false" TabIndex="22" MaxLength="13" Style="text-align: right"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtNetAmt"
                                                        FilterType="Custom, Numbers" ValidChars="." Enabled="True" />

                                                </div>
                                            </div>

                                        </div>
                                    </div>


                                    <div class="form-group row">
                                        <div class="col-md-2">
                                            <label>Remark / Note </label>
                                        </div>
                                        <div class="col-md-10">
                                            <asp:TextBox TextMode="MultiLine" runat="server" TabIndex="23" ID="txtRemark" CssClass="form-control"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>

                            </ContentTemplate>
                            <Triggers>
                                <%--<asp:PostBackTrigger ControlID="btnSubmit" />--%>
                                <%-- <asp:PostBackTrigger ControlID="txtGSTAmount" />
                                                <asp:PostBackTrigger ControlID="txtTDSonSGSTAmount" />--%>
                            </Triggers>
                        </asp:UpdatePanel>

                
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="col-12 mt-3" id="dvLedgers" runat="server" visible="true">
                                    <%-- <div class="sub-heading">
                                        <h5>Transaction Details</h5>
                                    </div>--%>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Transaction Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                </div>
                                                <asp:TextBox ID="txtTransDate" runat="server" CssClass="form-control" TabIndex="5"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    PopupButtonID="Image2" TargetControlID="txtTransDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                    OnInvalidCssClass="errordate" TargetControlID="txtTransDate" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True">
                                                </ajaxToolKit:MaskedEditExtender>
                                            </div>
                                        </div>

                                       

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Select Cash/Bank</label>
                                            </div>
                                            <asp:TextBox ID="txtBankLedger" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="8" ToolTip="Please Enter TDS Ledger Name"></asp:TextBox>
                                            <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="txtBankLedger"
                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                ServiceMethod="GetAgainstAccount" OnClientShowing="clientShowing">
                                            </ajaxToolKit:AutoCompleteExtender>
                                        </div>

                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic" id="tdslabel" runat="server" visible="false">
                                                <sup>*</sup>
                                                <label>TDS Ledger</label>
                                            </div>
                                            <div id="tdstextbox" runat="server" visible="false">
                                                <asp:TextBox ID="txtTDSLedger" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="8" ToolTip="Please Enter TDS Ledger Name" ></asp:TextBox>
                                                <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtTDSLedger"
                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                    ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                </ajaxToolKit:AutoCompleteExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-12" runat="server" id="dvGSTLedger" visible="false">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>SGST Ledger</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSgstLedger" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="8" ToolTip="Please Enter SGST Ledger Name"></asp:TextBox>
                                                    <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" TargetControlID="txtSgstLedger"
                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                        ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                    </ajaxToolKit:AutoCompleteExtender>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>CGST Ledger</label>
                                                    </div>
                                                    <asp:TextBox ID="txtCGSTLedger" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="8" ToolTip="Please Enter CGST Ledger Name" ></asp:TextBox>
                                                    <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" TargetControlID="txtCGSTLedger"
                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                        ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                                    </ajaxToolKit:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="dvIgstledger" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>IGST Ledger</label>
                                            </div>
                                            <asp:TextBox ID="txtIGSTLedger" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="8" ToolTip="Please Enter IGST Ledger Name" ></asp:TextBox>
                                            <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" TargetControlID="txtIGSTLedger"
                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                            </ajaxToolKit:AutoCompleteExtender>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divTdsonGstLedger" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>TDS On IGST Account</label>
                                            </div>
                                            <asp:TextBox ID="txtTDSonGSTLedger" runat="server" AutoPostBack="true" CssClass="form-control" ToolTip="Please Enter TDS Ledger" ></asp:TextBox>
                                            <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" TargetControlID="txtTDSonGSTLedger"
                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                            </ajaxToolKit:AutoCompleteExtender>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divTdsonSGstLedger" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>TDS On SGST Account</label>
                                            </div>
                                            <asp:TextBox ID="txtTDSonSGSTLedger" runat="server" CssClass="form-control" ToolTip="Please Enter TDS Ledger"></asp:TextBox>
                                            <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender9" runat="server" TargetControlID="txtTDSonSGSTLedger"
                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                            </ajaxToolKit:AutoCompleteExtender>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divTdsonCGstLedger" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>TDS On CGST Account</label>
                                            </div>
                                            <asp:TextBox ID="txtTDSonCGSTLedger" runat="server" CssClass="form-control" ToolTip="Please Enter TDS Ledger"></asp:TextBox>
                                            <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" TargetControlID="txtTDSonCGSTLedger"
                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                            </ajaxToolKit:AutoCompleteExtender>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSecurityLedger" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Security Ledger</label>
                                            </div>
                                            <asp:TextBox ID="txtSecurityLedger" runat="server" CssClass="form-control" ToolTip="Please Enter Security Ledger"></asp:TextBox>
                                            <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender10" runat="server" TargetControlID="txtSecurityLedger"
                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                ServiceMethod="GetAccount" OnClientShowing="clientShowing">
                                            </ajaxToolKit:AutoCompleteExtender>
                                        </div>
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Narration</label>
                                            </div>
                                            <asp:TextBox ID="txtNarration" runat="server" AutoPostBack="false" Width="100%" TextMode="MultiLine" ToolTip="Enter Narration for Voucher"></asp:TextBox>

                                        </div>
                                    </div>

                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                          <div class="row" id="divPaymode" runat="server">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                             <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Transaction Mode</label>
                                            </div>
                                                <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="C">CHEQUE</asp:ListItem>
                                                    <asp:ListItem Value="N">NEFT</asp:ListItem>
                                                    <asp:ListItem Value="R">RTGS</asp:ListItem>
                                                </asp:DropDownList>


                                            </div>

                               <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Cheque No</label>
                                            </div>
                                    <asp:TextBox ID="txtChqNo2" runat="server" AutoPostBack="false" ToolTip="Please Enter Account Name"
                                            MaxLength="6" CssClass="form-control" ReadOnly="false"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender TargetControlID="txtChqNo2" ID="ajxtklfiltertxtBox"
                                            runat="server" ValidChars="0123456789">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                   </div>
                              </div>


                     

                         <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Select</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSelect" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select Approve/Reject"
                                                    AppendDataBoundItems="true" TabIndex="45" AutoPostBack="false" OnSelectedIndexChanged="ddlSelect_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label id="lblRemark" runat="server">Approval Remarks</label>
                                                    <label id="lblReturn" runat="server" visible="false">Return Remarks</label>
                                                </div>
                                                <asp:TextBox ID="txtApproveRemarks" runat="server" TextMode="MultiLine" CssClass="form-control" TabIndex="46"
                                                    ToolTip="Enter Remarks" />
                                            </div>

                                        </div>
                                    </div>
                           <asp:Panel ID="pnlNewFiles" runat="server" Visible="false">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Uploaded Bills</h5>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <%-- <div class="table-responsive">--%>
                                    <asp:ListView ID="lvNewFiles" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <%-- <div class="titlebar">
                                                                                        <h4>Document List</h4>
                                                                                    </div>--%>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Sr.No.</th>
                                                            <th>Bill Name</th>
                                                            <th>File Name</th>
                                                            <th>Download</th>
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
                                                <td><%#Container.DataItemIndex+1 %></td>
                                                <td><%#Eval("DOCUMENTNAME") %></td>
                                                <td>
                                                    <%#Eval("DisplayFileName") %>                                                                             
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imgFile" runat="Server" ImageUrl="~/Images/action_down.png"
                                                        CommandArgument='<%#Eval("Filepath") %> ' ToolTip='<%#Eval("DisplayFileName") %> ' OnClick="imgdownload_Click" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <%--</div>--%>
                                </div>

                            </asp:Panel>


                         <div id="dvbuttons" runat="server">
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="btn btn-primary" OnClientClick="return AskSave();" ValidationGroup="TAX" OnClick="btnSave_Click" />
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary" OnClick="btnBack_Click" />
                                <asp:ValidationSummary ID="vstax" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="TAX" />
                            </div>

                        </div>
                    </asp:Panel>


                        
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Button ID="btnForPopUpModel" Style="display: none" runat="server" Text="For PopUp Model Box" />
                            </div>

                            <div class="col-10">
                                <ajaxToolKit:ModalPopupExtender ID="upd_ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                    DropShadow="True" PopupControlID="pnl" TargetControlID="btnForPopUpModel2" DynamicServicePath=""
                                    Enabled="True">
                                </ajaxToolKit:ModalPopupExtender>
                                <asp:Panel ID="pnl" runat="server" Width="600px" BorderColor="#0066FF" BackColor="White" meta:resourcekey="pnlResource1">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <%-- <h5>Transaction</h5>--%>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnForPopUpModel2" Style="display: none" runat="server" Text="For PopUp Model Box" />
                                        <asp:Button ID="btnPrint" runat="server" Text="Print Voucher" ValidationGroup="Validation"
                                            CssClass="btn btn-info" OnClick="btnPrint_Click" meta:resourcekey="btnPrintResource1" />
                                        <asp:Button ID="btnClose" runat="server" Text="Close" ValidationGroup="Validation"
                                            CssClass="btn btn-warning" OnClick="btnClose_Click" meta:resourcekey="btnBackResource1" />
                                        <asp:Button ID="btnchequePrint" runat="server" CssClass="btn btn-primary" Text="Print Cheque" Style="display: none" OnClick="btnchequePrint_Click" />
                                        <asp:HiddenField ID="hdnBack" runat="server" />
                                    </div>
                                    <div class="col-12 mt-3">
                                        <asp:ListView ID="lvGrp" runat="server">
                                            <LayoutTemplate>
                                                <h4 id="demo-grid">
                                                    <div class="sub-heading">
                                                        <h5>Transaction</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered " style="width: 100%" id="">
                                                        <%--  nowrap display--%>
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Particulars
                                                                </th>
                                                                <th>Debit
                                                                </th>
                                                                <th>Credit
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
                                                        <%# Eval("LEDGER")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEBIT") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CREDIT") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("LEDGER")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEBIT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CREDIT")%>
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:ListView>

                                    </div>
                                </asp:Panel>
                            </div>

                        </div>

                </div>
            </div>
        </div>
    </div>

    <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    <%-- </div>--%>
</asp:Content>
