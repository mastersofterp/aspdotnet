<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TCIssueReport.aspx.cs" Inherits="ACADEMIC_TcIssueReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
#ctl00_ContentPlaceHolder1_pnlStud .dataTables_scrollHeadInner {
width: max-content !important;
}
</style>
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdTc"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="UpdTc" runat="server">

        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title"></h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
           
                       <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <%--<label>Branch</label>--%>
                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Branch." AutoPostBack="true"
                                            CssClass="form-control" data-select2-enable="true" ValidationGroup="Show" TabIndex="1">
                                        </asp:DropDownList>
                                     <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select Branch." InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>

                                  <div class="form-group col-lg-3 col-md-6 col-12">
                                      <label><span style="color: red;">*</span> From Date</label>
                                      <div class="input-group">
                                   <div class="input-group-addon">
                                                <i id="dvcal1" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" CssClass="form-control"  />
                                    <%--<asp:Image ID="imgCalFromDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckDate"
                                        TargetControlID="txtFromDate" PopupButtonID="dvcal1" Enabled="true" EnableViewState="true">
                                    </ajaxToolKit:CalendarExtender>
                                <%--    <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                                        Display="None" ErrorMessage="Please enter initial date for report." SetFocusOnError="true"
                                        ValidationGroup="report" />--%>
                                    <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                            <asp:RequiredFieldValidator ID="rfvtxtfromdate" runat="server" ControlToValidate="txtFromDate"
                                            Display="None" ErrorMessage="Please Select From Date." SetFocusOnError="true"
                                            ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                   
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                        <label><span style="color: red;">*</span> To Date</label>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                                <i id="dvtodate" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" CssClass="form-control"  onchange="return checkToDate();"/>
                                    <%-- <asp:Image ID="imgCalToDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" OnClientDateSelectionChanged="CheckDate"
                                        TargetControlID="txtToDate" PopupButtonID="dvtodate" Enabled="true" EnableViewState="true">
                                    </ajaxToolKit:CalendarExtender>
                                   <%-- <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                                        Display="None" ErrorMessage="Please enter last date for report." SetFocusOnError="true"
                                        ValidationGroup="report" />--%>
                                    <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                    <asp:CompareValidator ID="CompareValidator1" ValidationGroup="report" ForeColor="Red" runat="server"
                                        ControlToValidate="txtFromDate" ControlToCompare="txtToDate" Operator="LessThan" Type="Date" Display="None"
                                        ErrorMessage="From date must be less than To date."></asp:CompareValidator>
                                      <asp:RequiredFieldValidator ID="rfvtodate" runat="server" ControlToValidate="txtToDate"
                                            Display="None" ErrorMessage="Please Select To Date." SetFocusOnError="true"
                                            ValidationGroup="Show"> </asp:RequiredFieldValidator>
                                </div>
                                    </div>


                                </div>                      
                        </div>

                       <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" TabIndex="4" ValidationGroup="Show" OnClick="btnShow_Click" />
                                 <asp:Button ID="btnReport" runat="server" Text="Report"
                                    TabIndex="3" ValidationGroup="Show" CssClass="btn btn-info" OnClick="btnReport_Click"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning"  TabIndex="5" OnClick="btnCancel_Click"/>
                               
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Show"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                              
                            </div>
                
                           

                            <div class="col-12" >
                                    
                                    <asp:Panel ID="pnlStud" runat="server">
                                    <asp:ListView ID="lvStudent" runat="server">

                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                             <h5>List of Students</h5>
                                           </div>
                                      
                                               <table class="table table-striped table-bordered nowrap display" id="divadmissionlist">                                         
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                       <th>SrNo</th>
                                                        <th >Student Id </th>                                                    
                                                        <th>TCNo   </th>                                                       
                                                        <th >Admission Batch</th>
                                                        <th >Academic Year </th>
                                                        <th>Branch</th>
                                                        <th>Student Name</th>
                                                        <th>Year of Admission</th>
                                                        <th>DOB</th>
                                                        <th>Year of Leaving</th>
                                                        <th>Issue Count</th>        
                                                        <th>Reason </th>
                                                        <th>RollNo </th>
                                                         <th>Remark </th>
                                                    </tr>
                                                </thead>

                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                              <td >
                                                    <%# Container.DataItemIndex + 1%>
                                                </td>
                                                <td >
                                                    <%# Eval("REGNO")%>
                                                </td>
                                                <td >
                                                    <%# Eval("TCNO")%>
                                                </td>
                                                <td >
                                                    <%# Eval("BATCHNAME")%>
                                                </td>
                                                <td >
                                                    <%# Eval("YEARNAME")%>
                                                </td>
                                                <td >
                                                    <%# Eval("BRANCH")%>
                                                </td>
                                                <td >
                                                    <%# Eval("STUDENTNAME")%>
                                                </td>
                                                 <td >
                                                    <%# Eval("ADMDATE","{0:dd/MM/yyyy}")%>
                                                </td>
                                                 <td >
                                                    <%# Eval("DOB")%>
                                                </td>
                                                 <td >
                                                    <%# Eval("ISSUEDATE" ,"{0:dd/MM/yyyy}")%>
                                                </td>
                                                 <td >
                                                    <%# Eval("ISSUECOUNT")%>
                                                </td>
                                                 <td >
                                                    <%# Eval("REASON")%>
                                                </td>
                                                  <td >
                                                    <%# Eval("ROLL_NO")%>
                                                </td>
                                                 <td >
                                                    <%# Eval("REMARK")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                        </asp:Panel>
                                </div>
                           <div id="divMsg" runat="server">
                         </div>
                    </div>
                </div>
            </div>
         </div>
        </ContentTemplate>
           <Triggers>
          <asp:PostBackTrigger ControlID="btnReport" />
      </Triggers>
    </asp:UpdatePanel>
    

    <script language="javascript" type="text/javascript">

        function getCurrentYear() {
            var cDate = new Date();
            return cDate.getFullYear();
        }

        function CheckDate(sender, args) {
            //var txtfrm = document.getElementById('txtFromDate')
            //var txtto = document.getElementById('txtToDate')
            if (sender._selectedDate > new Date()) {
                sender._selectedDate = new Date();
                alert("Do not select Future Date!");
                //sender._textbox.set_Value("");
                document.getElementById("txtFromDate").value = '';
                document.getElementById("txtToDate").value = "";
            }
        }
    </script>

    <script type="text/javascript">
        //function checkDate()
        //{
        //    alert(1);
        //    //debugger;
        //    var datefrom = $("[id*=ctl00_ContentPlaceHolder1_TextBox1]").val();
        //    var dateTo = $("[id*=ctl00_ContentPlaceHolder1_TextBox2]").val();
        //    var fromdate = new Date(datefrom)
        //    var todate = new Date(dateTo)
        //    //alert(datefrom);
        //    //alert(dateTo);
        //    alert(fromdate);
        //    alert(todate);

        //    // I change the < operator to >
        //    if (dateTo < datefrom)
        //    {
        //        alert("To Date should be greater than From Date");
        //        //sender._selectedDate = new Date();
        //        // set the date back to the current date
        //        dateTo.value('');
        //    }

        //}

        function checkToDate() {
            var datefrom = $("[id*=ctl00_ContentPlaceHolder1_txtFromDate]").val();
            var dateTo = $("[id*=ctl00_ContentPlaceHolder1_txtToDate]").val();
            var fromdate = new Date(datefrom)
            var todate = new Date(dateTo)
            var dd = String(fromdate.getDate()).padStart(2, '0');
            var mm = String(fromdate.getMonth() + 1).padStart(2, '0'); //January is 0!
            var yyyy = fromdate.getFullYear();
            var selDate = dateTo.split("/");
            var selday = selDate[0];
            var selmonth = selDate[1] - 1;
            var selyear = selDate[2];
            var SDate = new Date(selyear, selmonth, selday);



            var ddFrom = String(todate.getDate()).padStart(2, '0');
            var mmFrom = String(todate.getMonth() + 1).padStart(2, '0'); //January is 0!
            var yyyyFrom = todate.getFullYear();
            var FromDate = datefrom.split("/");
            var Fromday = FromDate[0];
            var Frommonth = FromDate[1] - 1;
            var Fromyear = FromDate[2];
            var FDate = new Date(Fromyear, Frommonth, Fromday);



            if (SDate < FDate) {
                alert('To Date Should Be Greater Than From Date.');
                document.getElementById('<%=txtToDate.ClientID%>').value = "";
                return true;
            }
            else {
                return false;
            }
        }

    </script>

    
</asp:Content>
