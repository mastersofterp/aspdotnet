<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Hostel_Module_Configuration.aspx.cs" Inherits="HOSTEL_Hostel_Module_Configuration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 30px;
            height: 15px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 10px;
                width: 10px;
                left: 4px;
                bottom: 4px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }
        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }

        table, tr, td {
            padding-right: 20px;
            padding-bottom: 25px;
        }

        checkbox {
            margin-left: 500px;
        }
        /*table, td {
            padding-right:20px;
        }*/
    </style>

    <asp:UpdatePanel ID="mainpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Hostel Configuration Details</h3>
                        </div>

                        <div class="box-body">

                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Allow Admin Room Allotment </label>
                                        </div>
                                        <%-- <input type="checkbox" class="custom-control-input" id="chkAdminAllowRoomAllot" onclick="return SetStat(this);" />--%>
                                        <asp:CheckBox runat="server" ID="chkAdminAllowRoomAllot" />
                                        <%-- <label class="custom-control-label" for="chkAdminAllowRoomAllot"></label>--%>
                                        <asp:HiddenField ID="hdfAdminAllowRoomAllot" runat="server" ClientIDMode="Static" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Allow Student Room Allotment </label>
                                        </div>
                                        <%--  <input type="checkbox" class="custom-control-input" id="chkStudAllowRoomAllot" onclick="return SetStat(this);">--%>
                                        <asp:CheckBox runat="server" ID="chkStudAllowRoomAllot" />
                                        <%-- <label class="custom-control-label" for="chkStudAllowRoomAllot"></label>--%>
                                        <asp:HiddenField ID="hdfStudAllowRoomAllot" runat="server" ClientIDMode="Static" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Fee Collection Through </label>
                                        </div>
                                        <asp:DropDownList ID="ddlFeeCollectionType" runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Payment Gateway</asp:ListItem>
                                            <asp:ListItem Value="2">Academic</asp:ListItem>
                                            <asp:ListItem Value="3">Admin Collection</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdfFeeCollectionType" runat="server" ClientIDMode="Static" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Payment Gateway </label>
                                        </div>
                                        <%-- <input type="checkbox" class="custom-control-input" id="chkPaymentGateway" onclick="return SetStat(this);">--%>
                                        <asp:CheckBox runat="server" ID="chkPaymentGateway" />
                                        <%--<label class="custom-control-label" for="chkPaymentGateway"></label>--%>
                                        <asp:HiddenField ID="hdfPaymentGateway" runat="server" ClientIDMode="Static" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Allow Attendence Hostel Wise </label>
                                        </div>
                                        <%-- <input type="checkbox" class="custom-control-input" id="chkAdminAllowRoomAllot" onclick="return SetStat(this);" />--%>
                                        <asp:CheckBox runat="server" ID="chkAtteHoswise" />
                                        <%-- <label class="custom-control-label" for="chkAdminAllowRoomAllot"></label>--%>
                                        <asp:HiddenField ID="hdfAtteHostelWise" runat="server" ClientIDMode="Static" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Allow Attendence Block Wise </label>
                                        </div>
                                        <%-- <input type="checkbox" class="custom-control-input" id="chkAdminAllowRoomAllot" onclick="return SetStat(this);" />--%>
                                        <asp:CheckBox runat="server" ID="chkAttenBlockWise" />
                                        <%-- <label class="custom-control-label" for="chkAdminAllowRoomAllot"></label>--%>
                                        <asp:HiddenField ID="hdfAttenBlockWise" runat="server" ClientIDMode="Static" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Allow Hostel Booking Day Wise </label>
                                        </div>
                                        <%-- <input type="checkbox" class="custom-control-input" id="chkAdminAllowRoomAllot" onclick="return SetStat(this);" />--%>
                                        <asp:CheckBox runat="server" ID="chkHosBookDayWise" />
                                        <%-- <label class="custom-control-label" for="chkAdminAllowRoomAllot"></label>--%>
                                        <asp:HiddenField ID="hdnHosBookDayWise" runat="server" ClientIDMode="Static" />
                                    </div>

                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Allow Hostel Disciplinary Actions </label>
                                        </div>
                     
                                        <asp:CheckBox runat="server" ID="chkHosDisciplinary" />
                           
                                        <asp:HiddenField ID="hdnHosDisciplinary" runat="server" ClientIDMode="Static" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Allow Create Demand On Room Allotment </label>
                                        </div>
                     
                                        <asp:CheckBox runat="server" ID="chkCreateDemandOnRoomAllotment" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Allow Student Online Pay From Online Payment Form </label>
                                        </div>
                     
                                        <asp:CheckBox runat="server" ID="chkAllowDirectPayFromOnlinePaymentForm" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" ToolTip="Submit" Text="Save" CssClass="btn btn-primary"
                                    OnClick="btnSave_Click" />
                                <%-- OnClientClick="return SetStat(this)"--%>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>

    </asp:UpdatePanel>

    <script>
        function SetActive(val, chkValue) {
            var chk = val.id;
            var select = chkValue;
            //alert(chk+" and "+select);
            if (chk == "chkAdminAllowRoomAllot") {
                //alert("in")
                if (chkValue == "true") {
                    //alert("inn")
                    $('#chkAdminAllowRoomAllot').prop('checked', true);
                    $('#hdnAdminAllowRoomAllot').val($('#chkAdminAllowRoomAllot').prop('checked'));
                }
                else {
                    //alert("out")
                    $('#chkAdminAllowRoomAllot').prop('checked', false);
                    $('#chkAdminAllowRoomAllot').val(false);
                }


            }
            if (chk == "chkStudAllowRoomAllot") {
                if (chkValue == "true") {
                    $('#chkStudAllowRoomAllot').prop('checked', true);
                    $('#hdnStudAllowRoomAllot').val(true);
                }
                else {
                    $('#chkStudAllowRoomAllot').prop('checked', false);
                    $('#hdnStudAllowRoomAllot').val($('#chkStudAllowRoomAllot').prop('checked'));
                }
                //$('#rdRollNo').prop('checked', false);
                //$('#rdenroll').prop('checked', false);

            }
            if (chk == "chkPaymentGateway") {
                if (chkValue == "true") {
                    $('#chkPaymentGateway').prop('checked', true);
                    $('#hfPaymentGateway').val($('#chkPaymentGateway').prop('checked'));
                } else {
                    $('#chkPaymentGateway').prop('checked', false);
                    $('#hfPaymentGateway').val(false);
                }
                //$('#rdRollNo').prop('checked', false);
                //$('#rdRegno').prop('checked', false);

            }




        }

    </script>
    <script>
        function SetStat(val) {

            var chk = val.id;

            if (chk == "chkAdminAllowRoomAllot") {
                if (val.checked) {
                    $('#hdnAdminAllowRoomAllot').val($('#chkAdminAllowRoomAllot').prop('checked'));
                    //$('#rdRegno').prop('checked', false);
                    //$('#rdenroll').prop('checked', false);
                }
                else {
                    $('#hdnAdminAllowRoomAllot').val(false);
                }
            }

            if (chk == "chkStudAllowRoomAllot") {

                if (val.checked) {

                    $('#hdnStudAllowRoomAllot').val($('#chkStudAllowRoomAllot').prop('checked'));
                    //$('#rdRollNo').prop('checked', false);
                    //$('#rdenroll').prop('checked', false);
                }
                else {
                    $('#hdnStudAllowRoomAllot').val(false);
                }
            }

            if (chk == "chkPaymentGateway") {

                if (val.checked) {
                    $('#hfPaymentGateway').val($('#chkPaymentGateway').prop('checked'));
                    //$('#rdRollNo').prop('checked', false);
                    //$('#rdRegno').prop('checked', false);
                }
                else {
                    $('#hfPaymentGateway').val(false);
                }
            }


        }


    </script>

</asp:Content>

