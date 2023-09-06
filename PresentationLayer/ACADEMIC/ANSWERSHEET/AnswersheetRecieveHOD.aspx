<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="AnswersheetRecieveHOD.aspx.cs" Inherits="ACADEMIC_ANSWERSHEET__AnswersheetRecieveHOD" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .ajax__calendar_body {
            height: 149px !important;
            width: 214px !important;
        }

        .ajax__calendar_container {
            width: 214px !important;
        }

        .auto-style1 {
            height: 42px;
        }
    </style>

    <div style="z-index: 1; position: absolute; top: 40%; left: 50%;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="uptPnl"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px;">
                    <%--background-color: Aqua; padding-left: 5px--%>
                    <img src="../../images/anim_loading_75x75.gif" alt="Loading" />
                    Please Wait..
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
  
    <asp:UpdatePanel ID="uptPnl" runat="server">
        <ContentTemplate>

            <div class="col-md-12">
                <div class="box box-primary">

                    <fieldset>
                        <legend>ANSWERSHEET COLLECTION
                        </legend>
                    </fieldset>
                    <div class="box-tools pull-right">
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </p>
                    </div>
                    <div style="color: Red; font-weight: bold; margin-top: -20px; margin-left: -5px;">
                        &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                    </div>
                    <br />
                    <form role="form">

                        <div class="box-body">
                           
                            <div class="row">
                                <div class="col-md-12">
                                     <div class="form-group col-md-4">
                                    <label>Session  <span class="validstar" style="color: red;">*</span> </label>

                                    <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" AppendDataBoundItems="true" ValidationGroup="MrksheetRcev">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                        ValidationGroup="MrksheetRcev" Display="None" InitialValue="0" ErrorMessage="Please Select Session">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Degree  <span class="validstar" style="color: red;">*</span> </label>

                                    <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="True"
                                        ValidationGroup="MrksheetRcev" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="MrksheetRcev">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-md-4">
                                    <label>Branch  <span class="validstar" style="color: red;">*</span> </label>

                                    <asp:DropDownList ID="ddlBranch" AppendDataBoundItems="true" CssClass="form-control" Width="330px" runat="server" ValidationGroup="MrksheetRcev"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="MrksheetRcev">
                                    </asp:RequiredFieldValidator>
                                </div>
                              
                          
                                 <div class="form-group col-md-4">
                                    <label>
                                        Scheme  <span class="validstar" style="color: red;">*</span>
                                    </label>

                                    <asp:DropDownList ID="ddlScheme" AppendDataBoundItems="true" CssClass="form-control" runat="server" ValidationGroup="MrksheetRcev"
                                        OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                        Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" ValidationGroup="MrksheetRcev">
                                    </asp:RequiredFieldValidator>
                                </div>

                                 <div class="form-group col-md-4">
                                    <label>Semester  <span class="validstar" style="color: red;">*</span> </label>


                                    <asp:DropDownList ID="ddlSem" AppendDataBoundItems="true" runat="server" CssClass="form-control" ValidationGroup="MrksheetRcev" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="MrksheetRcev">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-md-4">

                                    <label>Exam Type  <span class="validstar" style="color: red;">*</span> </label>


                                    <asp:DropDownList ID="ddlExamType" runat="server" AppendDataBoundItems="True" CssClass="form-control"
                                        AutoPostBack="True" ValidationGroup="submit">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">Regular</asp:ListItem>
                                        <asp:ListItem Value="2">RE-Valuation</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlexamtype" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Select Exam Type" ControlToValidate="ddlExamType"
                                        Display="None" ValidationGroup="MrksheetRcev" InitialValue="0" />
                                </div>
                            

                           
                              <%--  <div class="form-group col-md-4">
                                    <label>Exam  <span class="validstar" style="color: red;">*</span> </label>


                                    <asp:DropDownList ID="ddlExam" runat="server" ValidationGroup="MrksheetRcev" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExam"
                                        Display="None" ErrorMessage="Please Select Exam" InitialValue="0" ValidationGroup="MrksheetRcev">
                                    </asp:RequiredFieldValidator>
                                </div>--%>

                                </div>
                            </div>&nbsp;
                            <div class="row text-center">
                                        <asp:Button ID="btnShow" runat="server" ValidationGroup="MrksheetRcev" Text="Show"
                                            OnClick="btnShow_Click"  CssClass="btn btn-primary" />
                                        &nbsp;
                                <asp:Button ID="btnSubmit"  runat="server" ValidationGroup="MrksheetRcev" Text="Submit"
                                    OnClick="btnSubmit_Click" CssClass="btn btn-success" />
                                        &nbsp;
                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Visible="false"
                                    Text="Report" />
                                        <asp:Button ID="btnPacking" runat="server" OnClick="btnPacking_Click"  Visible="false"
                                            Text="Packing Slip" />
                                        &nbsp;
                                <asp:Button ID="btnClear" runat="server" Text="Cancel"  CssClass="btn btn-warning" OnClick="btnClear_Click" />
                                        &nbsp;
                            <asp:ValidationSummary ID="VSReceived" runat="server" DisplayMode="List"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="MrksheetRcev" />

                                     </div>
                                

                           

                            <div class="col-md-12">
                                <fieldset>
                                    <legend>Note</legend></fieldset>
                                    <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <label>Session  <span class="validstar" style="color: red;">*</span> </label>
                                            <td>
                                                <asp:DropDownList ID="ddlSessionrpt" runat="server" AppendDataBoundItems="True" TabIndex="1"
                                                    ToolTip="Please Select Session">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSessionrpt"
                                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="ReportNew"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-4">
                                            <label>Branch  <span class="validstar" style="color: red;">*</span> </label>

                                            <asp:DropDownList ID="ddlBranchrpt" runat="server" AppendDataBoundItems="True" TabIndex="1"
                                                ToolTip="Please Select Branch">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBranchrpt"
                                                Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="ReportNew"></asp:RequiredFieldValidator>
                                        </div>


                                    
                                        
                                        
                                         <div class="col-md-4">
                                            <label>Exam Date  <span class="validstar" style="color: red;">*</span> </label>
                                               <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                            <asp:TextBox ID="txtExamDate" runat="server" TabIndex="3" ValidationGroup="ReportNew"
                                                Width="300px" Value='<%# Eval("DATE")%>' />
                                            <%--<asp:Image ID="imgExamDate" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                            <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate"
                                                TargetControlID="txtExamDate" PopupButtonID="imgExamDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" TargetControlID="txtExamDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" EmptyValueMessage="Please Enter  Date"
                                                ControlExtender="meExamDate" ControlToValidate="txtExamDate" IsValidEmpty="false"
                                                InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter  Date"
                                                InvalidValueBlurredMessage="*" ValidationGroup="ReportNew" SetFocusOnError="true" />
                                            <asp:HiddenField runat="server" ID="CalDate" Value='<%# Eval("DATE")%>' />
                                        </div>
                                             </div>
                                       
                                    </div>
                                        </div>
                                     <div class="box-footer">
                                            <p class="text-center">
                                                <asp:Button ID="btnReportReport" runat="server" Text="Report" OnClick="btnReportReport_Click"
                                                    ValidationGroup="ReportNew" CssClass="btn btn-primary" Width="140px" />
                                                <asp:Button ID="btnPackingReport" runat="server" Text="Packing Slip" CssClass="btn btn-info" Width="140px" OnClick="btnPackingReport_Click"
                                                    ValidationGroup="ReportNew" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="ReportNew" />
                                            </p>
                                        </div>
                                
                            </div>
                  


                        <%-- MARKSHEET RECIEVE STUDENT LIST--%>
                        <div class="col-md-12" table ="responsive">
                        <asp:Panel ID="pnlStudent" runat="server">
                            
                                <asp:ListView ID="lvStudents" runat="server" OnItemDataBound="lvStudents_ItemDataBound">
                                    <LayoutTemplate>
                                        <div id="demo-grid" class="vista-grid">
                                            <div class="titlebar">
                                                <b>
                                                List of Courses
                                                    </b>
                                            </div>
                                            <table class="table table-hover table-bordered">
                                                <tr class="header">
                                                    <th>Sr No
                                                    </th>
                                                    <th >Class & Subject
                                                    </th>
                                                    <th >Tot Ans Recd
                                                    </th>
                                                    <%--<th style="width: 14%">
                                            Ans Recd By
                                        </th>--%>
                                                    <th >Ans Sub By
                                                    </th>
                                                    <th>Date 
                                                    </th>
                                                    <th >Exam Time
                                                    </th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                            <td style="text-align: center">
                                                <asp:Label ID="lblsR" runat="server" Font-Bold="true" />
                                                <%#Container.DataItemIndex+1 %>
                                 
                                            </td>
                                            <td st text-transform: uppercase; line-height: 1">

                                                <%-- <asp:Label ID="lblcourse" runat="server" ToolTip='<%# Eval("TOT_STUD")%>'  ></asp:Label>--%>
                                                <%# Eval("COURSENAME")%>
                                                <asp:HiddenField runat="server" ID="hdfcoursename" Value='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td >
                                                <asp:TextBox ID="txtAnsRced" runat="server" Value='<%# Eval("TOT_ANS_RECD")%>' ToolTip='<%# Eval("TOT_STUD")%>' />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FteAnsrecd" runat="server"
                                                    FilterType="Numbers" TargetControlID="txtAnsRced" />
                                                <asp:RangeValidator ID="rvansered" runat="server" ErrorMessage="Please enter valid paper count." ControlToValidate="txtAnsRced"
                                                    ValidationGroup="MrksheetRcev" SetFocusOnError="true" EnableClientScript="true"></asp:RangeValidator>
                                            </td>
                                            <td >
                                                <asp:TextBox runat="server" ID="txtAnsSub"
                                                    Text='<%# Eval("SUBMIT_STAFF")%>' />

                                            </td>
                                            <td >
                                                <%--<div>--%>
                                                <asp:TextBox ID="txtExamDate" runat="server" TabIndex="3" ValidationGroup="submit"
                                                    Value='<%# Eval("DATE")%>' />
                                                <asp:Image ID="imgExamDate" runat="server" ImageUrl="~/images/calendar.png" />
                                                <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate"
                                                    TargetControlID="txtExamDate" PopupButtonID="imgExamDate" />
                                                <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" TargetControlID="txtExamDate"
                                                    Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                    MaskType="Date" />
                                                <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" EmptyValueMessage="Please Enter  Date"
                                                    ControlExtender="meExamDate" ControlToValidate="txtExamDate" IsValidEmpty="false"
                                                    InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter  Date"
                                                    InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                                <asp:HiddenField runat="server" ID="CalDate" Value='<%# Eval("DATE")%>' />
                                               
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRemark" runat="server"  Value='<%# Eval("REMARK")%>' ToolTip='<%# Eval("REMARK")%>' Visible="false" />

                                                <asp:DropDownList ID="ddlExamtime" runat="server" AppendDataBoundItems="true" ToolTip='<%# Eval("EXAMSLOT")%>'>
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                           
                        </asp:Panel>
                             </div>
                      </div>
                    </form>
                </div>
            </div>
        </ContentTemplate>
          <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>
    <script type="text/javascript">
        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select future date!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>

</asp:Content>


