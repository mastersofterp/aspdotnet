<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ArrearSubmission.aspx.cs" Inherits="ESTATE_Transaction_ArrearSubmission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=lvBillProcess.ClientID %>').Scrollable({
                ScrollHeight: 300
            });

        });
    </script>

    <style type="text/css">
        .Calendar .ajax__calendar_body {
            width: 200px;
            height: 170px; /* modified */
            position: relative;
            border: solid 0px;
        }

        .Calendar .ajax__calendar_container {
            background-color: #ffffff;
            border: 1px solid #646464;
            color: #000000;
            width: 195px;
            height: 210px;
        }

        .Calendar .ajax__calendar_footer {
            border: solid top 1px;
            cursor: pointer;
            padding-top: 3px;
            height: 6px;
        }

        .Calendar .ajax__calendar_day {
            cursor: pointer;
            height: 17px;
            padding: 0 2px;
            text-align: right;
            width: 18px;
        }

        .Calendar .ajax__calendar_year {
            border: solid 1px #E0E0E0;
            /*font-family: Tahoma, Calibri, sans-serif;*/
            font-family: Verdana;
            font-size: 10px;
            text-align: center;
            font-weight: bold;
            text-shadow: 0px 0px 2px #D3D3D3;
            text-align: center !important;
            vertical-align: middle;
            margin: 1px;
            height: 40px; /* added */
        }

        .Calendar .ajax__calendar_month {
            border: solid 1px #E0E0E0;
            /*font-family: Tahoma, Calibri, sans-serif;*/
            font-family: Verdana;
            font-size: 10px;
            text-align: center;
            font-weight: bold;
            text-shadow: 0px 0px 2px #D3D3D3;
            text-align: center !important;
            vertical-align: middle;
            /* margin: 1px;
        height: 40px; /* added */
        }
    </style>

    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ARREAR SUBMISSION</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Employee List For Arrear Submission 
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                        <asp:ListView ID="lvBillProcess" runat="server">
                                                            <EmptyDataTemplate>
                                                                <p class="text-center text-bold">
                                                                    <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" />
                                                                </p>
                                                            </EmptyDataTemplate>
                                                            <LayoutTemplate>
                                                                <div class="vista-grid">
                                                                    <table id="tblitems" class="table table-bordered table-hover">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <th>EMPLOYEE NAME
                                                                                </th>
                                                                                <th>ENERGY CONSUMER NO.
                                                                                </th>
                                                                                <th>UNIT SOLD
                                                                                </th>
                                                                                <th>PRESENT DEMAND
                                                                                </th>
                                                                                <th>WITHIN DUE DATE AMT
                                                                                </th>
                                                                                <th>AFTER DUE DATE AMT
                                                                                </th>
                                                                                <th>ARREAR
                                                                                </th>
                                                                                <th>ARREAR INTEREST
                                                                                </th>
                                                                            </tr>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                            <thead>
                                                                            </thead>
                                                                        </thead>
                                                                    </table>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblname" runat="server" Font-Names="Verdana"
                                                                            Text='<%#Eval("EMP_NAME")%>' ToolTip='<%#Eval("PID")%>'></asp:Label>
                                                                        <asp:HiddenField ID="hdnQAID" runat="server" Value='<%#Eval("QA_ID")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblMno" runat="server" Font-Names="Verdana" ReadOnly="true"
                                                                            Text='<%#Eval("QUARTER_NAME") %>'> </asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblqrtNo" runat="server" Font-Names="Verdana" ReadOnly="true"
                                                                            Text='<%#Eval("UNIT_SOLD") %>'> </asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblmeterNo" runat="server" Font-Names="Verdana" ReadOnly="true"
                                                                            Text='<%#Eval("PRESENT_DEMAND") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label2" runat="server" Font-Names="Verdana" ReadOnly="true"
                                                                            Text='<%#Eval("WITHIN_DUE_DATE_AMT") %>'> </asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label3" runat="server" Font-Names="Verdana" ReadOnly="true"
                                                                            Text='<%#Eval("AFTER_DUE_DATE_AMT") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtArrear" runat="server" Font-Names="Verdana" MaxLength="10" Text='<%#Eval("ARREAR") %>'
                                                                            onblur="CalculateInterest();" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbold" runat="server" FilterType="Custom, Numbers"
                                                                            TargetControlID="txtArrear" ValidChars=".">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtArrearInst" runat="server" Font-Names="Verdana" MaxLength="10" CssClass="form-control"
                                                                            Text='<%#Eval("ARREAR_INTEREST")%>' onClick="CalculateInterest();" TabIndex="2"></asp:TextBox>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftxNumber" runat="server" FilterType="Custom, Numbers"
                                                                            TargetControlID="txtArrearInst" ValidChars=".">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row" id="trshowbutton" runat="server">
                                                <div class="col-md-12">
                                                    <div class="text-center">
                                                        <asp:Button ID="btnSubmitData" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="3"
                                                            OnClick="btnSubmitData_Click" />
                                                        <asp:Button ID="btnreset" runat="server" Text="Reset" CssClass="btn btn-warning" TabIndex="4"
                                                            OnClick="btnreset_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">

        function CalculateInterest() {

            var table = document.getElementById('tblitems');
            var tot = 0.00;
            var total = 0.00;
            for (var r = 0; r < table.rows.length - 1; r++) {
                var lblArrear = document.getElementById('ctl00_ContentPlaceHolder1_lvBillProcess_ctrl' + r.toString() + '_txtArrear');

                var txtAI = document.getElementById('ctl00_ContentPlaceHolder1_lvBillProcess_ctrl' + r.toString() + '_txtArrearInst');
                document.getElementById('ctl00_ContentPlaceHolder1_lvBillProcess_ctrl' + r.toString() + '_txtArrearInst').value = '';

                if (lblArrear.value != '') {

                    tot = (parseInt(lblArrear.value) * 0.02);
                    total = tot.toFixed(2);
                    document.getElementById('ctl00_ContentPlaceHolder1_lvBillProcess_ctrl' + r.toString() + '_txtArrearInst').value = total;

                }
                else {
                    tot = 0;
                    document.getElementById('ctl00_ContentPlaceHolder1_lvBillProcess_ctrl' + r.toString() + '_txtArrearInst').value = tot;
                }

            }
        }


    </script>

</asp:Content>

