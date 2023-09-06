<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BranchChangeApproveSecondLevel.aspx.cs" Inherits="ACADEMIC_BranchChangeApproveSecondLevel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../JAVASCRIPTS/jquery.min_1.js" type="text/javascript"></script>

    <script src="../JAVASCRIPTS/jquery-ui.min_1.js" type="text/javascript"></script>--%>
     <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }

 

        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

 

        .modalPopup
        {
            background-color: #FFFFFF;
            width: 700px;
            border: 3px solid #858788; /*#0DA9D0;*/
            border-radius: 12px;
            padding: 0;
        }

 

            .modalPopup.right
            {
                right: 0 !important;
                top: 0 !important;
                left: inherit !important;
                border-radius: 12px;
                height: 100%;
            }

 

            .modalPopup .header
            {
                background-color: #858788; /*#2FBDF1;*/
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }

 

            .modalPopup .body
            {
                padding: 10px;
                min-height: 50px;
                text-align: center;
                font-weight: bold;
            }

 

            .modalPopup .footer
            {
                padding: 6px;
            }

 

            .modalPopup .yes, .modalPopup .no
            {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }

 

            .modalPopup .yes
            {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }

 

            .modalPopup .no
            {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }

 

        element.style
        {
            font-family: Verdana !important;
            font-size: 10pt !important;
            color: red !important;
        }
    </style>

    <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            Autocomplete();
        }

        function Autocomplete() {
            $(function () {
                $(".tb").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: "../HealthService.asmx/GetData_BranchChange",
                            data: "{'data': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataFilter: function (data) {; return data; },
                            success: function (data) {
                                response($.map(data.d, function (item) {
                                    return {
                                        value: item
                                    }
                                }))
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert(textStatus);
                            }
                        });
                    },
                    minLength: 3
                });
            });
        }
    </script>
    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>
    <div>
        <%--<div style="z-index: 1; position: absolute; top: 10px; left: 600px;">--%>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <%-- <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>--%>
                <%--added new loader on 12052020 srikanth--%>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-body">
                            <div class="form-group col-md-12">
                                <div class="box-header with-border">
                                    <h3 class="box-title"><b>Programme/Branch Change Final Approval</b></h3>
                                    <div class="box-tools pull-right">
                                        <div style="color: Red; font-weight: bold">
                                            &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                       <%-- <div class="col-md-9">--%>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div></div>
                                                    <div class="col-md-5">

                                                       
                                                       <%-- <label>Reg. No:</label>--%>
                                                        <div class="form-group">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtStudent" Visible="false" runat="server" class="form-control" />
                                                                <label>  <span style="color: red">*</span> Student List</label>
                                                                <asp:DropDownList ID="ddlStudentList"  runat="server" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlStudentList_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="input-group-btn">
                                                                    <asp:Button ID="btnShow" Visible="false" runat="server" Text="Show" CssClass="btn btn-primary btn-flat" OnClick="btnShow_Click" ValidationGroup="Show" />
                                                                </span>
                                                                
                                                            </div>
                                                        </div> 
                                                        <asp:RequiredFieldValidator ID="rfvRollNumber" Visible="false" runat="server"
                                                            ControlToValidate="txtStudent" Display="None"
                                                            ErrorMessage="Please Enter Reg. No" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="rfvRollNumber_submit" Visible="false" runat="server"
                                                            ControlToValidate="txtStudent" Display="None"
                                                            ErrorMessage="Please Enter Roll Number" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                        <asp:Label ID="lblPreSession" runat="server" Font-Bold="true" Visible="false" />
                                                    </div>
                                                    <div class="col-md-4" style="margin-top:28px">
                                                        <span><asp:Button ID="btnReport" runat="server" Text="View Approval Report" OnClick="btnReport_Click" CssClass="btn btn-primary btn-flat" /> </span>
                                                    </div>

                                                    <div class="col-md-3" style="margin-top:28px">
                                                    <%--  <asp:Image ID="imgEmpPhoto" runat="server" style="float:left" ImageUrl="~/IMAGES/nophoto.jpg" Height="100px" Width="100px" />

                                                <div>
                                                    <fieldset class="fieldset">
                                                        <legend class="legend"></legend><b style="color: #FF0000">Note : </b>Student's username will also be changed after branch change.                                   
                                                    </fieldset>

                                                </div>--%>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <%-- <div class="col-md-1"></div>--%>
                                                    <div class="col-md-12">
                                                        <asp:Label ID="lblNote" Font-Bold="true" Visible="false" Text="Note: Please do not refresh page Or Do not search new student once you processed demand for current student." Style="color: red;" runat="server" SkinID="lblmsg"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" id="divdata" runat="server" visible="false">
                                                <div class="col-md-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel-body">
                                                            <div class="form-group col-md-6">
                                                                <label>Student Name :</label>
                                                                <asp:Label ID="lblName" runat="server" Font-Bold="True" Style="color: green"></asp:Label>
                                                                <asp:Label ID="lblAdmbatch" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblYear" runat="server" Font-Bold="True" Visible="false"></asp:Label>

                                                            </div>
                                                            <div class="form-group col-md-6">
                                                                <label>Current Programme/Branch :</label>
                                                                <asp:Label ID="lblBranch" runat="server" Style="color: green" Font-Bold="True"></asp:Label>
                                                            </div>
                                                            <div class="form-group col-md-6">
                                                                <label>Reg. No. :</label>
                                                                <asp:Label ID="lblRegNo" runat="server" Font-Bold="True" Style="color: green"></asp:Label>
                                                            </div>
                                                            <div class="form-group col-md-6">
                                                                <label>Roll No. :</label>
                                                                <asp:Label ID="lblRollNo" runat="server" Font-Bold="True" Style="color: green"></asp:Label>
                                                            </div>
                                                            <div class="form-group col-md-6">
                                                                <label>Degree. :</label>
                                                                <asp:Label ID="lblDegree" runat="server" Font-Bold="True" Style="color: green"></asp:Label>
                                                            </div>
                                                            <div class="form-group col-md-6">
                                                                <label>College Name :</label>
                                                                <asp:Label ID="lblColg" runat="server" Font-Bold="True" Style="color: green"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                     <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="panel panel-default">
                                                                <div class="panel-body">
                                                                    <div class="form-group col-md-12">
                                                                         <label>Programme/Branch Change Requested Remark:</label><asp:Label ID="lblRequestRemark" Font-Bold="true" ForeColor="Green" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="form-group col-md-12">
                                                                         <label>Programme/Branch Change Academic Remark:</label><asp:Label ID="lblAcademicRemark" Font-Bold="true" ForeColor="Green" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="form-group col-md-12">
                                                                         <label>Programme/Branch Change Approved Remark:</label><asp:Label ID="lblApprovedRemark" Font-Bold="true" ForeColor="Green" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="panel panel-default">
                                                                <div class="panel-body">
                                                                    <div class="form-group col-md-12">
                                                                        <asp:RadioButton AutoPostBack="true" ID="rdWithoutFee" OnCheckedChanged="rdWithoutFee_CheckedChanged" Font-Bold="true" Text="Programme/Branch Change if Fees Not Paid" runat="server" GroupName="fees" />&nbsp;&nbsp;
                                                                        <asp:RadioButton AutoPostBack="true" ID="rdWithFee" OnCheckedChanged="rdWithFee_CheckedChanged" Font-Bold="true" Text="Programme/Branch Change After Fees Paid" runat="server" GroupName="fees" />
                                                                        <span style="margin-left:200px;font-weight:bold">Document Preview:<asp:LinkButton ID="lnkView" runat="server" CommandName='<%# Eval("FILE_NAME") %>' OnClick="lnkView_Click" CommandArgument='<%# Eval("PREVIEW_PATH") %>'  ToolTip='<%# Eval("PREVIEW_PATH") %>'><image style="height:25px" src="../IMAGES/view.gif" data-toggle="modal" data-target="#myModal22"></image></asp:LinkButton><asp:Label  ID="lblView" runat="server" Font-Bold="false" ForeColor="Red" Visible="false"></asp:Label>
                                                                            </span>
                                                                    </div>
                                                                    <div>
                                                                         
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                     

                                                    <asp:Panel ID="pnlBranchChange" runat="server" Visible="false">
                                                        <div class="panel panel-default">
                                                            <div class="panel-body">
                                                                <div class="form-group col-md-4">
                                                                    <label><span style="color: red">*</span>Select New College</label>
                                                                    <asp:DropDownList ID="ddlNewCollege" runat="server" AppendDataBoundItems="true" Enabled="false">
                                                                        <asp:ListItem Value="0">Pleas Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="form-group col-md-4">
                                                                    <label><span style="color: red">*</span>Select New Degree</label>
                                                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" Enabled="false"
                                                                        OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" ToolTip="Please Select Degree" AutoPostBack="True">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="form-group col-md-4">
                                                                    <span style="color: red">*</span>
                                                                    <label>Select New Programme/Branch :</label>
                                                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" onChange='return ShowConfirm(this);' Enabled="false"
                                                                        CssClass="form-control" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                                                        AutoPostBack="True">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                                        Display="None" ErrorMessage="Please Select Branch" InitialValue="0"
                                                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                                                </div>

                                                                <div class="form-group col-md-6" style="display:none">
                                                                    <label>Generated New Roll No :</label>
                                                                    <asp:TextBox ID="txtNewRegNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>



                                                                <div class="form-group col-md-6">
                                                                    <span style="color: red">*</span>
                                                                    <label>Remark : <span style="color: red">(Maximum limit 300 characters.)</span></label>
                                                                    <asp:TextBox ID="txtRemark" runat="server" Height="70px" MaxLength="300"
                                                                        CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rvfRemark" runat="server"
                                                                        ControlToValidate="txtRemark" ErrorMessage="Please Enter Remark"
                                                                        ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-md-6">
                                                                    <span style="color: red"></span>
                                                                    <label>Current Programme/Branch Fees:</label>
                                                                    <i class="fa fa-inr"></i>&nbsp;<asp:Label ID="lblCurrentfeess" runat="server" Font-Bold="True" Text="0"></asp:Label>
                                                                </div>

                                                                <div class="form-group col-md-6">
                                                                    <span style="color: red"></span>
                                                                    <label>Paid Fees:</label>
                                                                    <i class="fa fa-inr"></i>&nbsp;<asp:Label ID="lblPaidFees" runat="server" Font-Bold="True" Text="0"></asp:Label>
                                                                </div>

                                                                <div class="form-group col-md-6">
                                                                    <span style="color: red"></span>
                                                                    <label>Selected New Programme/Branch Fees:</label>
                                                                    <i class="fa fa-inr"></i>&nbsp;<asp:Label ID="lblNewBranchFee" runat="server" Font-Bold="True" Visible="false" Text="0"></asp:Label>
                                                                    <asp:Label ID="lblExcessAmt" runat="server" Font-Bold="True" Visible="false" Text="0"></asp:Label>
                                                                       <asp:Label ID="lbldemand" runat="server"   ToolTip='<%# Eval("DM_NO")%>' ></asp:Label>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </asp:Panel>


                                                    <%--   <div class="form-group col-md-6">
                                                    <span style="color: red"></span>
                                                    <label>New Branch Fees:</label>
                                                    <asp:Label ID="lblNewBranchFee" runat="server" Font-Bold="True"></asp:Label>
                                                </div>--%>
                                                </div>
                                            </div>
                                       <%-- </div>--%>
                                        
                                        <div class="col-md-3">
                                            <asp:Image ID="imgEmpPhoto" runat="server"  Visible="false" ImageUrl="~/IMAGES/nophoto.jpg" Height="100px" Width="100px" Style="text-align: center;" />
                                            <div style="display: none;">
                                                <fieldset class="fieldset">
                                                    <legend class="legend"></legend><b style="color: #FF0000">Note : </b>Student's username will also be changed after Programme/Branch change.                                   
                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-8">
                                        <div id="divDemandButton" runat="server" visible="false" style="color:red;font-weight:bold;text-align:center"> 
                                                If You Want Enter Programme/Branch Change Fee Then 
                                            <asp:LinkButton ID="lnkDemandNew" runat="server" Text="Click Here" OnClick="lnkDemandNew_Click"/>
                                        </div>
                                     </div>
                                    <br />
                                    <br />
                                    <div class="col-md-12" id="divFeeItems" runat="server" visible="false">
                                        <div class="col-md-12">
                                            <fieldset>
                                                <%--<legend>Fee Items</legend>--%>

                                                <asp:ListView ID="lvFeeItems" runat="server"  OnItemDataBound="lvFeeItems_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <div id="divlvFeeItems">

                                                            <h4>Available Fee Items</h4>

                                                            <table id="tblFeeItems" runat="server" class="table table-hover table-bordered">
                                                                <tr class="bg-light-blue">
                                                                    <th>Sr. No.
                                                                    </th>
                                                                    <th>Fee Heads
                                                                    </th>
                                                                    <th>Currency
                                                                    </th>
                                                                    <th>Amount
                                                                    </th>
                                                                </tr>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblFeeHeadSrNo" runat="server" Text='<%# Eval("SRNO") %>' Visible="false" />
                                                                <%# Container.DataItemIndex + 1%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("FEE_LONGNAME")%>
                                                                <asp:HiddenField ID="hdnfld_FEE_LONGNAME" runat="server" Value='<%# Eval("FEE_LONGNAME")%>' />
                                                            </td>
                                                           <%-- <td>
                                                                <%# Eval("CURRENCY")%>
                                                            </td>--%>
                                                            <td>
                                                                <asp:TextBox ID="txtFeeItemAmount" onblur="UpdateTotalAndBalance();" onkeyup="IsNumeric(this);"
                                                                    Text='<%# Eval("AMOUNT")%>' Style="text-align: right" runat="server" CssClass="form-control"
                                                                    TabIndex="15" />
                                                                <asp:HiddenField ID="hidFeeItemAmount" runat="server" Value='<%# Eval("AMOUNT") %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>

                                            </fieldset>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group col-md-3" style="display:none">
                                                <label>Total Cash Amount</label>
                                                <asp:TextBox ID="txtTotalCashAmt" Text="0" CssClass="data_label" runat="server" onkeydown="javascript:return false;" />
                                            </div>
                                            <div class="form-group col-md-3" style="display:none">
                                                <label>Total D.D. Amount</label>
                                                <asp:TextBox ID="txtTotalDDAmount" Text="0" CssClass="data_label" runat="server"
                                                    onkeydown="javascript:return false;" />
                                            </div>
                                            <div class="form-group col-md-3">
                                                <label>Total Fee Amount</label>
                                                <asp:TextBox ID="txtTotalFeeAmount" CssClass="data_label" Text="0" runat="server"
                                                    onkeydown="javascript:return false;"  />
                                            </div>

                                            <div class="form-group col-md-3" style="display:none">
                                                <label>Amount to be Paid</label>
                                                <asp:TextBox ID="txtTotalAmount" Text="0" CssClass="data_label" runat="server"/>
                                            </div>

                                            <div class="form-group col-md-3" style="display:none">
                                                <label>Excess/Deposit Amount</label>
                                                <asp:TextBox ID="txtFeeBalance" Text="0" CssClass="data_label" runat="server" onkeydown="javascript:return false;" />                                                
                                            </div>
                                        </div>
                                        <div class="col-md-12" style="display:none">
                                            <div class="form-group col-md-4">
                                                <label>Remark</label>
                                                <asp:TextBox ID="txtRemarkFee" runat="server" TextMode="MultiLine" Rows="4" MaxLength="400"
                                                    TabIndex="133" />
                                            </div>
                                        </div>

                                        <div class="form-group col-md-3" style="display:none;">
                                                <label>Total Amount</label>
                                                <asp:TextBox ID="txtTotalAmountShow" CssClass="data_label" runat="server" ReadOnly="true" />
                                            <asp:Label ID="lblamtpaid" CssClass="data_label" runat="server" />
                                          
                                            
                                        </div>

                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-4"></div>
                                        <div class="col-md-8">
                                                                                        
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" Visible="false"
                                                ValidationGroup="Submit" OnClick="btnSubmit_Click" />
                                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="btn btn-danger" Visible="false"
                                                Text="Cancel"  /><br />

                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show" />
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />
                                        </div>
                                    </div>
                                    <div>
                                        <asp:DropDownList ID="ddlExamType" AppendDataBoundItems="true" runat="server"
                                                    CssClass="form-control" Enabled="true" Visible="False">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Regular</asp:ListItem>
                                                    <asp:ListItem Value="2">Repeater</asp:ListItem>
                                                    <asp:ListItem Value="3">Revaluation</asp:ListItem>
                                                </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-10">
                                            <asp:Label ID="lblMsg" Font-Bold="true" runat="server" SkinID="lblmsg"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
         <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="lnkView" />
        </Triggers>
    </asp:UpdatePanel>
    <%-- <asp:Button ID="btnPrint" runat="server" Text="Print Registration Slip" Width="180px"
                                                ValidationGroup="Show" OnClick="btnPrint_Click" />--%>
    
     <ajaxToolKit:ModalPopupExtender ID="mpeViewDocument" BehaviorID="mpeViewDocument" runat="server" PopupControlID="pnlPopup"
        TargetControlID="lnkFake" CancelControlID="btnClose" BackgroundCssClass="modalBackground"
        OnOkScript="ResetSession()">
    </ajaxToolKit:ModalPopupExtender>

     <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup">
        <div class="header">
            Document
                 
        </div>
        <div class="body">
            <iframe runat="server" width="680px" height="550px" id="iframeView"></iframe>
        </div>
        <div class="footer" align="right">
            <asp:Button ID="btnClose" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Larger" Text="Close" CssClass="no" />
        </div>
    </asp:Panel>

    <div id="divMsg" runat="server" />
    <script type="text/javascript">
        function ShowConfirm(yourDropDown) {
            //If this is true - Perform your PostBack manually
            if (confirm("New demand will be create for selected Programme/Branch. Are you sure?")) {
                __doPostBack('ddlBranch', '');
                return true;
            }
                //Otherwise the user selected "Cancel" - revert the selection and don't PostBack
            else {

                yourDropDown.selectedIndex = 0;//(yourDropDown.value == "No") ? 0 : 1;
                return false;
            }
        }

        function RollBackConfirmation() {
            return confirm("ALERT: Created new Programme/Branch demand will be RollBack. Are you sure?");
        }


    </script>

    <script>
        function UpdateTotalAndBalance() {
            try {
                var totalFeeAmt = 0.00;
                var dataRows = null;

                if (document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems') != null)
                    dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvFeeItems_tblFeeItems').getElementsByTagName('tr');

                if (dataRows != null) {
                    for (i = 1; i < dataRows.length; i++) {
                        var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                        var dataCell = dataCellCollection.item(2);
                        var controls = dataCell.getElementsByTagName('input');
                        var txtAmt = controls.item(0).value;
                        if (txtAmt.trim() != '')
                            totalFeeAmt += parseFloat(txtAmt);
                    }
                    if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalFeeAmount') != null)
                        document.getElementById('ctl00_ContentPlaceHolder1_txtTotalFeeAmount').value = totalFeeAmt;

                    var totalPaidAmt = 0;
                    if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount') != null && document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim() != '')
                        totalPaidAmt = document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount').value.trim();

                    if (document.getElementById('ctl00_ContentPlaceHolder1_txtFeeBalance') != null)
                        document.getElementById('ctl00_ContentPlaceHolder1_txtFeeBalance').value = (parseFloat(totalPaidAmt) - parseFloat(totalFeeAmt));
                }
                UpdateCash_DD_Amount();
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            function UpdateCash_DD_Amount() {
                try {
                    var txtPayType = document.getElementById('ctl00_ContentPlaceHolder1_txtPayType');
                    var txtPaidAmt = document.getElementById('ctl00_ContentPlaceHolder1_txtTotalAmount');

                    if (txtPayType != null && txtPaidAmt != null) {
                        if (txtPayType.value.trim() == "C" && document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt') != null) {
                            document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = txtPaidAmt.value.trim();
                        }
                        else if (txtPayType.value.trim() == "T" && document.getElementById('tblDD_Details') != null) {
                            var totalDDAmt = 0.00;
                            var dataRows = document.getElementById('tblDD_Details').getElementsByTagName('tr');
                            if (dataRows != null) {
                                for (i = 1; i < dataRows.length; i++) {
                                    var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                                    var dataCell = dataCellCollection.item(6);
                                    if (dataCell != null) {
                                        var txtAmt = dataCell.innerHTML.trim();
                                        totalDDAmt += parseFloat(txtAmt);
                                    }
                                }
                                if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalDDAmount') != null &&
                                    document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt') != null) {
                                    document.getElementById('ctl00_ContentPlaceHolder1_txtTotalDDAmount').value = totalDDAmt;
                                    document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = parseFloat(txtPaidAmt.value.trim()) - parseFloat(totalDDAmt);
                                }
                            }
                        }
                        else if (txtPayType.value.trim() == "D" && document.getElementById('tblDD_Details') != null) {
                            var totalDDAmt = 0.00;
                            var dataRows = document.getElementById('tblDD_Details').getElementsByTagName('tr');
                            if (dataRows != null) {
                                for (i = 1; i < dataRows.length; i++) {
                                    var dataCellCollection = dataRows.item(i).getElementsByTagName('td');
                                    var dataCell = dataCellCollection.item(6);
                                    if (dataCell != null) {
                                        var txtAmt = dataCell.innerHTML.trim();
                                        totalDDAmt += parseFloat(txtAmt);
                                    }
                                }
                                if (document.getElementById('ctl00_ContentPlaceHolder1_txtTotalDDAmount') != null &&
                            document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt') != null) {
                                    document.getElementById('ctl00_ContentPlaceHolder1_txtTotalDDAmount').value = totalDDAmt;
                                    document.getElementById('ctl00_ContentPlaceHolder1_txtTotalCashAmt').value = parseFloat(txtPaidAmt.value.trim()) - parseFloat(totalDDAmt);
                                }
                            }
                        }
                    }
                }
                catch (e) {
                    alert("Error: " + e.description);
                }
            }
        }
    </script>

</asp:Content>