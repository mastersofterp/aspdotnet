<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentApplicationForm.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_EXAMINATION_StudentApplicationForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updApplication"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updApplication" runat="server">
        <ContentTemplate>
            <div class="box box-primary">
                <div class="box-body">
                    <div class="row" runat="server" id="divstudentsection" visible="false">
                        <div class="col-md-12">
                            <%--  <div class="box box-primary">--%>
                            <div class="box-header with-border">
                                <h3 class="box-title">Student Application Form  </h3>
                                <div class="box-tools pull-right">
                                </div>
                            </div>
                            <br />
                            <div style="color: Red; font-weight: bold; margin-top: -20px; margin-right: -5px;">
                                &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                            </div>
                            <div class="box-body">
                                <div class="col-md-12">
                                    <div id="divstatus" runat="server" visible="false" class="text-center">
                                        <h5>Application Status:
                                            <asp:Label ID="lblstatus" runat="server" Font-Bold="True"></asp:Label></h5>
                                    </div>
                                    <br />
                                </div>
                                <div class="col-md-7">
                                    <div class="form-group col-md-6">
                                        Student Name :
                     <asp:Label ID="lblName" runat="server" Font-Bold="True" />
                                    </div>
                                    <div class="form-group col-md-6">
                                        <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" /><br />
                                        <asp:Label ID="lblMotherName" runat="server" Font-Bold="False" />
                                    </div>

                                    <div class="form-group col-md-6">
                                        College :
                     <asp:Label ID="lblCollege" runat="server" Font-Bold="True"></asp:Label>
                                        <asp:HiddenField ID="hdnCollege" Value="" runat="server" />
                                    </div>
                                    <div class="form-group col-md-6">
                                        USN No :
                      <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label>
                                    </div>

                                    <div class="form-group col-md-6">
                                        Admission Batch :
                                        <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="form-group col-md-6">
                                        Current Semester :
                                        <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="form-group col-md-6">
                                        Degree  :
                                        <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="form-group col-md-6">
                                        Branch :
                                        <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="form-group col-md-6">
                                        Scheme :
                                        <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            Current Session :
                                            <asp:Label ID="lblsession" runat="server" Font-Bold="True"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="form-group col-md-6">
                                        <label><span style="color: red;">*</span> Application For :</label>
                                        <asp:DropDownList ID="ddlApplication" runat="server" AppendDataBoundItems="True" TabIndex="1" OnSelectedIndexChanged="ddlApplication_SelectedIndexChanged"
                                            AutoPostBack="True" ToolTip="Please Select Application">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlApplication"
                                            Display="None" ErrorMessage="Please Select Application" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                    </div>
                                    <div id="divReason" runat="server" visible="false">
                                        <div class="form-group col-md-6">
                                            <label><span style="color: red;">*</span> Reason :</label>
                                            <asp:TextBox ID="txtReason" runat="server" Rows="2" TabIndex="2" TextMode="MultiLine" ToolTip="Please Enter Reason" MaxLength="200" />
                                            <asp:RequiredFieldValidator ID="rfvremark" runat="server" ControlToValidate="txtReason" Display="None" ErrorMessage="Please Enter Reason"
                                                SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div id="divaddress" runat="server" visible="false">
                                        <div class="form-group col-md-4" id="divnoofcopies" runat="server" visible="false">

                                            <label><span style="color: red;">*</span>No of Copies Required:</label>
                                            <asp:TextBox ID="txtnoofcopies" runat="server" TabIndex="2" onkeyup="validateNumeric(this);" ToolTip="Please Enter No of Copies Required" MaxLength="2" />
                                            <asp:RequiredFieldValidator ID="rfvnoofcopies" runat="server" ControlToValidate="txtnoofcopies" Display="None" ErrorMessage="Please Enter No of Copies Required"
                                                SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-8">

                                            <label><span style="color: red;">*</span> Mailing Address :</label>
                                            <asp:TextBox ID="txtmailadd" runat="server" Rows="2" TabIndex="2" TextMode="MultiLine" ToolTip="Please Enter Mailing Address" MaxLength="200" />
                                            <asp:RequiredFieldValidator ID="rfvtxtmailadd" runat="server" ControlToValidate="txtmailadd" Display="None" ErrorMessage="Please Enter Mailing Address"
                                                SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group col-md-5">
                                    <fieldset class="fieldset" style="padding: 4%; color: Green">
                                        <legend class="legend">Note :</legend>
                                        <span style="color: red;">*</span>
                                        <label style="color: black;">You Can Apply For only one Application in one Session </label>
                                        <span style="font-weight: bold; text-align: center; color: red;"><u>APPLICATION FEES FOR</u><br />
                                        </span>


                                        <asp:Label ID="lblappname1" runat="server" Style="font-weight: bold; text-align: center; color: black;"></asp:Label>
                                        -
                             <asp:Label ID="lblappamount1" runat="server" Style="font-weight: bold; text-align: center; color: green;"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblappname2" runat="server" Style="font-weight: bold; text-align: center; color: black;"></asp:Label>
                                        -
                             <asp:Label ID="lblappamount2" runat="server" Style="font-weight: bold; text-align: center; color: green;"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblappname3" runat="server" Style="font-weight: bold; text-align: center; color: black;"></asp:Label>
                                        -
                             <asp:Label ID="lblappamount3" runat="server" Style="font-weight: bold; text-align: center; color: green;"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblappname4" runat="server" Style="font-weight: bold; text-align: center; color: black;"></asp:Label>
                                        -
                             <asp:Label ID="lblappamount4" runat="server" Style="font-weight: bold; text-align: center; color: green;"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblappname5" runat="server" Style="font-weight: bold; text-align: center; color: black;"></asp:Label>
                                        -
                             <asp:Label ID="lblappamount5" runat="server" Style="font-weight: bold; text-align: center; color: green;"></asp:Label>

                                        <asp:Label ID="lblOrderID" runat="server" CssClass="data_label" Visible="false"></asp:Label>

                                    </fieldset>
                                </div>
                                <div class="row" runat="server" id="divnamecorrection" visible="false">
                                    <div class="col-md-12 form-group">
                                        <asp:ListView ID="lvnamecorrection" runat="server">
                                            <LayoutTemplate>
                                                <div id="demo-grid" class="vista-grid">
                                                    <h4>Corrections to be incorporated in the Grade Card</h4>
                                                    <table class="table table-hover table-bordered table-responsive">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th></th>

                                                                <th>As Printed
                                                                </th>
                                                                <th>To be corrected to read as
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                </div>
                                                <tr id="itemPlaceholder" runat="server" />
                                                </table>
                                      
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td align="center">Name of the Candidate                                   
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstudname" runat="server" Text='<%# Eval("STUDNAME")%>'></asp:Label>
                                                    </td>


                                                    <td>
                                                        <asp:TextBox ID="txtcorrectedname" runat="server" TabIndex="3" Text='<%# Eval("CORR_STUDNAME") %>' ToolTip="Please Enter Correction Name" MaxLength="250" />
                                                        <asp:RequiredFieldValidator ID="rfvremark" runat="server" ControlToValidate="txtcorrectedname" Display="None" ErrorMessage="Please Enter Correction Name"
                                                            SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td align="center">Name of the Father                                   
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstudfatname" runat="server" Text='<%# Eval("FATHERNAME")%>'></asp:Label>
                                                    </td>
                                                    <td>

                                                        <asp:TextBox ID="txtcorrectedfatname" runat="server" TabIndex="4" Text='<%# Eval("CORR_FATHERNAME") %>' ToolTip="Please Enter Correction Father's Name" MaxLength="100" />
                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtcorrectedfatname" Display="None" ErrorMessage="Please Enter Correction Father's Name" 
                            SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                              
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">Name of the Mother                                   
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstudmotname" runat="server" Text='<%# Eval("MOTHERNAME")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtcorrectedmotname" runat="server" TabIndex="5" Text='<%# Eval("CORR_MOTHERNAME") %>' ToolTip="Please Enter Correction Mother's Name" MaxLength="100" />
                                                        <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtcorrectedmotname" Display="None" ErrorMessage="Please Enter Correction Mother's Name" 
                            SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                              
                                                    </td>
                                                </tr>

                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                                <div class="col-md-12" id="divremark" runat="server" visible="false">
                                    <div class="col-md-5 form-group">
                                        <label><span style="color: red;">*</span> Remark :</label>
                                        <asp:TextBox ID="txtRemark" runat="server" Rows="2" TabIndex="9" TextMode="MultiLine" ToolTip="Please Enter Remark" MaxLength="200" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRemark" Display="None" ErrorMessage="Please Enter Remark"
                                            SetFocusOnError="True" ValidationGroup="approve"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="box-footer">
                                        <p class="row text-center">
                                            <div id="divapprove" style="padding-left: 32%" runat="server" visible="false">
                                                <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click" TabIndex="10" Text="Approve" ToolTip="Approve" ValidationGroup="approve" CssClass="btn btn btn-success" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnreject" runat="server" OnClick="btnreject_Click" TabIndex="11" Text="Reject" ToolTip="Reject" ValidationGroup="approve" CssClass="btn btn btn-warning" />
                                                &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" TabIndex="12" Text="Back" ToolTip="Back" CssClass="btn btn-primary" />&nbsp;&nbsp;&nbsp;
                                            </div>
                                            <asp:Button ID="BtnPrntChalan" runat="server" Visible="false" Text="Print Chalan" TabIndex="5" Font-Bold="true"
                                                CssClass="btn btn-success" OnClick="BtnPrntChalan_Click" />
                                            <asp:Button ID="BtnOnlinePay" runat="server" Visible="false" Text="Click To Pay" TabIndex="6" Font-Bold="true"
                                                CssClass="btn btn-primary" OnClick="BtnOnlinePay_Click" />
                                            <asp:Button ID="btnSubmit" runat="server" Visible="false" OnClick="btnSubmit_Click" TabIndex="3" Text="Save & Continue" ToolTip="Submit" ValidationGroup="submit" CssClass="btn btn btn-info" />
                                            &nbsp;<asp:Button ID="btnRemoveList" runat="server" OnClick="btnRemoveList_Click" Text="Clear" Font-Bold="true" Visible="false"
                                                CssClass="btn btn-warning" />
                                            <%-- <asp:Button ID="btnreprintchalan" runat="server" Visible="false" OnClick="btnreprintchalan_Click" TabIndex="3" Text="Re-Print Chalan" ToolTip="Re-Print Chalan" ValidationGroup="submit" CssClass="btn btn btn-primary" />--%>

                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="approve" />
                                        </p>
                                    </div>
                                    <br />
                                    <div style="padding-left: 32%">
                                        <asp:RadioButtonList ID="radiolist" runat="server" TabIndex="4" RepeatDirection="Horizontal" AutoPostBack="True" Visible="false" OnSelectedIndexChanged="radiolist_SelectedIndexChanged">
                                            <asp:ListItem Value="1" Enabled="false">Online Pay (ICICI Payment Gateway)&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2" Selected="True">Pay Through Chalan</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="row text-center">
                                    </div>
                                </div>
                                <div class="col-md-12 form-group" id="divreciept" runat="server" visible="false">
                                    <asp:ListView ID="lvrecieptinfo" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid" class="vista-grid">
                                                <h4>Previous Application(s) Information</h4>
                                                <table class="table table-hover table-bordered table-responsive">
                                                    <thead>
                                                        <tr class="bg-light-blue">

                                                            <th>Application Name
                                                            </th>
                                                            <th>Reason
                                                            </th>

                                                            <th>Receipt No
                                                            </th>

                                                            <th>Date
                                                            </th>
                                                            <th>Semester
                                                            </th>

                                                            <th>Pay Type
                                                            </th>
                                                            <th>Amount 
                                                            </th>
                                                            <th>Payment Status
                                                            </th>
                                                            <th>Approve Status
                                                            </th>
                                                            <th>Print Challan
                                                            </th>
                                                            <th>Print Application
                                                            </th>


                                                        </tr>
                                                    </thead>
                                            </div>
                                            <tr id="itemPlaceholder" runat="server" />
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>

                                            <tr id="item">

                                                <td>
                                                    <%# Eval("APNAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("REASON") %>
                                                </td>

                                                <td>
                                                    <%# Eval("REC_NO") %>
                                                </td>


                                                <td>
                                                    <%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %>
                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTERNAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("PAY_TYPE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TOTAL_AMT")%>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="lblpaystatus" runat="server" Text='<%# Eval("PAY_STATUS")%>' Style="font-size: 14px"></asp:Label>

                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="lblstuapprstatus" runat="server" Text='<%# Eval("APPROVE_STATUS")%>' Style="font-size: 14px"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:ImageButton ID="btnPrintReceipt" runat="server" OnClick="btnPrintReceipt_Click"
                                                        CommandArgument='<%# Eval("DCR_NO") %>' ImageUrl="~/images/print.gif" ToolTip='<%# Eval("RECIEPT_CODE")%>' CausesValidation="False" />
                                                </td>
                                                <td align="center">
                                                    <asp:ImageButton ID="btnprintapplication" runat="server" OnClick="btnprintapplication_Click"
                                                        CommandArgument='<%# Eval("APP_TRANS_ID") %>' ImageUrl="~/images/print.gif" ToolTip='<%# Eval("RECIEPT_CODE")%>' CausesValidation="False" />
                                                </td>
                                                <asp:HiddenField ID="hdnapno" runat="server" Value='<%# Eval("APNO") %>' />
                                                <asp:HiddenField ID="hdnstudcrno" runat="server" Value='<%# Eval("DCR_NO") %>' />


                                            </tr>

                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">

                                                <td>
                                                    <%# Eval("APNAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("REASON") %>
                                                </td>
                                                <td>
                                                    <%# Eval("REC_NO") %>
                                                </td>

                                                <td>
                                                    <%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %>
                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTERNAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("PAY_TYPE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TOTAL_AMT")%>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="lblpaystatus" runat="server" Text='<%# Eval("PAY_STATUS")%>' Style="font-size: 14px"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:Label ID="lblstuapprstatus" runat="server" Text='<%# Eval("APPROVE_STATUS")%>' Style="font-size: 14px"></asp:Label>
                                                </td>
                                                <td align="center">
                                                    <asp:ImageButton ID="btnPrintReceipt" runat="server" OnClick="btnPrintReceipt_Click"
                                                        CommandArgument='<%# Eval("DCR_NO") %>' ImageUrl="~/images/print.gif" ToolTip='<%# Eval("RECIEPT_CODE")%>' CausesValidation="False" />
                                                </td>
                                                <td align="center">
                                                    <asp:ImageButton ID="btnprintapplication" runat="server" OnClick="btnprintapplication_Click"
                                                        CommandArgument='<%# Eval("APP_TRANS_ID") %>' ImageUrl="~/images/print.gif" ToolTip='<%# Eval("RECIEPT_CODE")%>' CausesValidation="False" />
                                                </td>

                                                <asp:HiddenField ID="hdnapno" runat="server" Value='<%# Eval("APNO") %>' />
                                                <asp:HiddenField ID="hdnstudcrno" runat="server" Value='<%# Eval("DCR_NO") %>' />
                                            </tr>

                                        </AlternatingItemTemplate>

                                    </asp:ListView>
                                    <div id="div2" runat="Server">
                                    </div>
                                </div>

                            </div>
                            <%--  </div>--%>
                        </div>
                    </div>
                    <div class="row" id="divadminsection" runat="server" visible="false">
                        <div class="col-md-12">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Student Application Form Approval</h3>

                                </div>
                                <div class="box-body">
                                    <div class="col-md-7">
                                        <div class="col-md-12"></div>
                                        <div class="col-md-8 form-group">
                                            <label><span style="color: red;">*</span> Show Applications For :</label>
                                            <asp:DropDownList ID="ddlApplicationForAdmin" runat="server" AppendDataBoundItems="True" TabIndex="7" OnSelectedIndexChanged="ddlApplicationForAdmin_SelectedIndexChanged"
                                                AutoPostBack="True" ToolTip="Please Select Application">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlApplication"
                                                Display="None" ErrorMessage="Please Select Application" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-5"></div>
                                </div>
                                <asp:Panel ID="pnlapplications" runat="server" ScrollBars="Auto">
                                    <div class="box-footer">
                                        <div class="col-md-12 form-group" id="divstudentList" runat="server" visible="false">
                                            <asp:ListView ID="lvStudentApplication" runat="server">
                                                <LayoutTemplate>
                                                    <div id="demo-grid" class="vista-grid">
                                                        <h4>Student Application List</h4>
                                                        <table class="table table-hover table-bordered table-responsive">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>View
                                                                    </th>
                                                                    <th>Student Name
                                                                    </th>
                                                                    <th>US No.
                                                                    </th>

                                                                    <th>Degree
                                                                    </th>
                                                                    <th>Branch 
                                                                    </th>
                                                                    <th>Payment Type
                                                                    </th>
                                                                    <th>Application Date
                                                                    </th>
                                                                    <th>Payment Status
                                                                    </th>
                                                                    <th>Approve Status
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                    </div>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>

                                                    <tr id="item">
                                                        <td>
                                                            <asp:LinkButton ID="btnview" runat="server" CommandArgument='<%# Eval("DCR_NO") %>' Text="View Application"
                                                                AlternateText="View Application" ToolTip="View Application" OnClick="btnview_Click" TabIndex="8" />
                                                            <asp:HiddenField ID="hdnapid" runat="server" Value='<%# Eval("APP_TRANS_ID") %>' />
                                                            <asp:HiddenField ID="hdnidno" runat="server" Value='<%# Eval("IDNO") %>' />
                                                            <asp:HiddenField ID="hdnregno" runat="server" Value='<%# Eval("REGNO") %>' />

                                                        </td>

                                                        <td>
                                                            <%# Eval("STUDNAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("REGNO")%>
                                                        </td>

                                                        <td>
                                                            <%# Eval("DEGREENAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("LONGNAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PAY_TYPE")%>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblappdate" runat="server" Text='<%# Eval("APPL_DATE", "{0:d-MM-yyyy}") %>' Style="font-size: 14px"></asp:Label>

                                                            <%--  <%# Eval("APPL_DATE")%>--%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PAY_STATUS")%>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblapprstatus" runat="server" Text='<%# Eval("APPROVE_STATUS")%>' Style="font-size: 14px"></asp:Label>
                                                        </td>


                                                    </tr>

                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                        <td>
                                                            <asp:LinkButton ID="btnview" runat="server" CommandArgument='<%# Eval("DCR_NO") %>' Text="View Application"
                                                                AlternateText="View Application" ToolTip="View Application" OnClick="btnview_Click" TabIndex="8" />
                                                            <asp:HiddenField ID="hdnapid" runat="server" Value='<%# Eval("APP_TRANS_ID") %>' />

                                                            <asp:HiddenField ID="hdnidno" runat="server" Value='<%# Eval("IDNO") %>' />
                                                            <asp:HiddenField ID="hdnregno" runat="server" Value='<%# Eval("REGNO") %>' />
                                                        </td>

                                                        <td>
                                                            <%# Eval("STUDNAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("REGNO")%>
                                                        </td>

                                                        <td>
                                                            <%# Eval("DEGREENAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("LONGNAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PAY_TYPE")%>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblappdate" runat="server" Text='<%# Eval("APPL_DATE", "{0:d-MM-yyyy}") %>' Style="font-size: 14px"></asp:Label>

                                                            <%--  <%# Eval("APPL_DATE")%>--%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PAY_STATUS")%>
                                                        </td>

                                                        <td align="center">
                                                            <asp:Label ID="lblapprstatus" runat="server" Text='<%# Eval("APPROVE_STATUS")%>' Style="font-size: 14px"></asp:Label>
                                                        </td>
                                                    </tr>

                                                </AlternatingItemTemplate>

                                            </asp:ListView>
                                            <div id="divMsg" runat="Server">
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

    <script type="text/javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
    </script>

</asp:Content>
