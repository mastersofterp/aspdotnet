<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TP_Disciplinary_Action.aspx.cs" Inherits="TP_Disciplinary_Action" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upPanel"
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
    <asp:UpdatePanel ID="upPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TP DISCIPLINARY ACTION</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>TP Disciplinary Action Information</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>R.R.N.</label>
                                        </div>
                                        <asp:TextBox ID="txtRegno" runat="server"
                                            CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRegno" runat="server" ControlToValidate="txtRegno"
                                            Display="None" ErrorMessage="Please Enter Registration No." ValidationGroup="submit"
                                            SetFocusOnError="True" meta:resourcekey="rfvTodtResource1"></asp:RequiredFieldValidator>
                                        <%--onkeypress="return isSpecialKey(event)"--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Disciplinary Date From</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="txtFromdt1">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromdt" runat="server" MaxLength="12" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFromdt" runat="server" ControlToValidate="txtFromdt"
                                                Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Schedule"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <%-- <asp:Image ID="imgCalFromdt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                            <ajaxToolKit:CalendarExtender ID="ceFromdt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromdt"
                                                PopupButtonID="txtFromdt1" Enabled="True">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeFromdt" runat="server" TargetControlID="txtFromdt"
                                                Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevFromdt" runat="server" ControlExtender="meeFromdt"
                                                ControlToValidate="txtFromdt" EmptyValueMessage="Please Enter From Date" InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Schedule" SetFocusOnError="True"
                                                ErrorMessage="mevFromdt" meta:resourcekey="mevFromdtResource1"></ajaxToolKit:MaskedEditValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Disciplinary Date To</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="txtTodt1">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtTodt" runat="server" MaxLength="12" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfvTodt" runat="server" ControlToValidate="txtTodt"
                                                Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="Schedule"
                                                SetFocusOnError="True" meta:resourcekey="rfvTodtResource1"></asp:RequiredFieldValidator>
                                            <%--<asp:Image ID="imgCalTodt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer"
                                                        meta:resourcekey="imgCalTodtResource1" />--%>
                                            <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTodt"
                                                PopupButtonID="txtTodt1" Enabled="True">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" TargetControlID="txtTodt"
                                                Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevTodt" runat="server" ControlExtender="meeTodt"
                                                ControlToValidate="txtTodt" EmptyValueMessage="Please Enter To Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Schedule" SetFocusOnError="True"
                                                ErrorMessage="mevTodt"></ajaxToolKit:MaskedEditValidator>

                                            <asp:Label ID="lblTentativeDt" runat="server" Text="Tentative Date" Font-Bold="True"
                                                Style="color: Red" Visible="False"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Remark</label>
                                        </div>
                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" onkeypress="return validate(event);" MaxLength="200"></asp:TextBox>
                                        <%--<asp:RegularExpressionValidator ID="revRemark" runat="server" ControlToValidate="txtRemark" ErrorMessage="Please Enter Valid Remark" ValidationExpression="^[a-zA-Z''-'\s]{33,57}$"></asp:RegularExpressionValidator>--%>
                                        <asp:RequiredFieldValidator ID="rfvRemark" runat="server" ControlToValidate="txtRemark" ValidationGroup="submit"
                                            Display="None" ErrorMessage="Please Enter Remark" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                            </div>
                            <%-- <div class="row" style="margin-left:5px";>                                                                                    
                                                    <label>Active Status: </label>&nbsp;                                   
                                    <asp:CheckBox ID="chknlstatus" runat="server" TextAlign="Left"/>
                                 
                                     <h5 > <strong><span style="color:darkred;">Note:-</span>Checked:Status-<span style="color:green;">Active</strong></span>&nbsp;
                                                    <strong> UnChecked:Status-<span style="color:red;">InActive</span></strong> </h5>                                                                                                  
                                          </div>--%>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <%--<asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="remark" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />--%>
                            </div>
                            <div class="col-12">
                                <asp:Panel runat="server">
                                    <div class="sub-heading">
                                        <h5>List of Disciplinary Student </h5>
                                    </div>
                                    <asp:ListView ID="lvDisciplinaryStudent" runat="server">
                                        <EmptyDataTemplate>
                                            <div class="text-center">
                                                <asp:Label ID="lblErr" runat="server" Text="No More Data Of Diciplinary Student ">
                                                </asp:Label>
                                            </div>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div id="demp_grid">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <%--<th style="text-align: center">
                                                             <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" />
                                                            </th>--%>
                                                            <th>Edit
                                                            </th>
                                                            <th>R.R.N
                                                            </th>
                                                            <th>Student Name
                                                            </th>
                                                            <th>Disciplinary Start Date
                                                            </th>
                                                            <th>Disciplinary End Date
                                                            </th>
                                                            <th>Status
                                                            </th>
                                                            <%--<th>Semester
                                                            </th>                                                  
                                                            <th>Status
                                                            </th>   --%>
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
                                                <%--<td style="width: 5%">
                                                <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("REGNO")%>' />
                                                </td>--%>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                        CommandArgument='<%# Eval("REGNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <%# Eval("ENROLLNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STUDNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DISCIPLINARY_START_DATE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DISCIPLINARY_END_DATE")%>
                                                </td>
                                                <td>
                                                    <%# GetStatus(Eval("STATUS")) %>                               
                                                </td>

                                                <%--<td>
                                                    <%# Eval("SEMESTER")%>
                                                </td>
                                               
                                                <td style="color:red">
                                                     <%# Eval("STATUS")%>
                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


    <style type="text/css">
        .less {
            color: #00C000;
        }

        .more {
            color: #C00000;
        }
    </style>

    <script type="text/javascript">
        // function isSpecialKey(evt)
        //{
        //   var charCode = (evt.which) ? evt.which : event.keyCode
        //   if ((charCode >= 48 || charCode <= 57))
        //         return true;

        //   return false;
        // }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            // if(charCode !=)
            if ((charCode >= 48 && charCode <= 57) || (charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122))
                return true;

            return false;
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });
        function bindDataTable() {
            var myDT = $('#myTable').DataTable({
                "scrollY": "400px",
                "scrollX": true,
                "scrollCollapse": true

            });

            console.log('datatable function call');
        }
    </script>
    <%--   <script>
    $(document).ready(function(){
    var tolerance = 5;
    $('.target').each(function(){
        if($(this).text() > tolerance){
            $(this).addClass('text-red');
        }else {
            $(this).addClass('text-green');
        }
    });
    });
    </script>--%>
    <%--  <script>
         function totAllSubjects(headchk) {
             var frm = document.forms[0]
             for (i = 0; i < document.forms[0].elements.length; i++) {
                 var e = frm.elements[i];
                 if (e.type == 'checkbox') {
                     if (headchk.checked == true)
                         e.checked = true;
                     else
                         e.checked = false;
                 }
             }

         }
    </script>--%>
    <script>
        function validate(e) {
            var k;

            document.all ? k = e.keyCode : k = e.which;
            //        return (k != 46 && k > 31 
            //&& (k < 48 || k > 57) || (k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 49 && k <= 57))
            return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32);
        }
    </script>
</asp:Content>

