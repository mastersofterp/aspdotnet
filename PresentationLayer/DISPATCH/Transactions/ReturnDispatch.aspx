<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ReturnDispatch.aspx.cs" Inherits="DISPATCH_Transactions_ReturnDispatch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js"></script>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">RETURN DISPATCH ENTRY</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12" id="divAdd" runat="server">
                                <div class="sub-heading">
                                    <h5>Search Dispatch By </h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Disp.Tracking No..</label>
                                        </div>
                                        <asp:TextBox ID="txtRefNo" runat="server" MaxLength="50" CssClass="form-control" TabIndex="1" ToolTip="Enter Reference No." />
                                        <asp:HiddenField ID="hdnIOTRANNO" runat="server" Value="" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" TabIndex="2" CssClass="btn btn-primary" ToolTip="Click To Search" />


                                    </div>

                                </div>
                            </div>
                            <div id="divDetails" runat="server" visible="false">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Dispatch Details</h5>
                                    </div>
                                </div>
                                <div class="col-12" id="divPanel1" runat="server" visible="false">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Sender Name</label>
                                            </div>
                                            <asp:TextBox ID="txtFrom" runat="server" MaxLength="100" Enabled="false" CssClass="form-control" ToolTip="Enter From Address" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Subject</label>
                                            </div>
                                            <asp:TextBox ID="txtSubject" runat="server" MaxLength="100" Enabled="false" CssClass="form-control" ToolTip="Enter Subject"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Address Line 1 </label>
                                            </div>
                                            <asp:TextBox ID="txtAddress" runat="server" MaxLength="100" Enabled="false" CssClass="form-control" ToolTip="Enter Address" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Address Line 2 </label>
                                            </div>
                                            <asp:TextBox ID="txtAddLine" runat="server" MaxLength="100" Enabled="false" CssClass="form-control" ToolTip="Enter Address" />

                                        </div>
                                    </div>
                                </div>

                                <div class="col-12" id="divPanel2" runat="server" visible="false">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>City</label>
                                            </div>
                                            <asp:TextBox ID="txtCity" runat="server" MaxLength="100" Enabled="false" CssClass="form-control" ToolTip="Enter Address" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>State/Province/Region </label>
                                            </div>
                                            <asp:TextBox ID="txtState" runat="server" MaxLength="100" Enabled="false" CssClass="form-control" ToolTip="Enter Address" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Pin No.</label>
                                            </div>
                                            <asp:TextBox ID="txtPIN" runat="server" CssClass="form-control" Enabled="false" ToolTip="Enter PIN Code" MaxLength="6"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Country</label>
                                            </div>
                                            <asp:TextBox ID="txtCountry" runat="server" Enabled="false" CssClass="form-control" ToolTip="Enter PIN Code" MaxLength="6"></asp:TextBox>

                                        </div>

                                    </div>
                                </div>

                                <div class="col-12" id="divPanel3" runat="server" visible="false">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Attendant Name </label>
                                            </div>
                                              <asp:TextBox ID="txtPeon" runat="server" MaxLength="25" Enabled="false" CssClass="form-control" ToolTip="Enter Peon Name" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                        
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12"  id="Divtr1" runat="server" visible="false" >
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Receiver Name</label>
                                            </div>
                                             <asp:TextBox ID="txtToUser" CssClass="form-control" Enabled="false" ToolTip="Enter To User" TextMode="MultiLine" runat="server"></asp:TextBox>
                                     
                                        </div>

                                       
                                    </div>

                                </div>

                                <div class="col-12" id="divList" runat="server" visible="false">
                                    <asp:Panel ID="Panel4" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="lvTo" runat="server">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                            <th>Receiver Name
                                                            </th>
                                                            <th>Address Line1
                                                            </th>
                                                            <th>Address Line2
                                                            </th>
                                                            <th>City
                                                            </th>
                                                            <th>State
                                                            </th>
                                                            <th>Pin No.
                                                            </th>
                                                            <th>Country
                                                            </th>
                                                           
                                                            <th>Contact No.
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                                
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td id="IOTRANNO" runat="server">
                                                        <%# Eval("IOTO") %>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblMulAddress" runat="server" Text='<%# Eval("MULTIPLE_ADDRESS") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAddLine" runat="server" Text='<%# Eval("ADDLINE") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("CITYNO") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblState" runat="server" Text='<%# Eval("STATE") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblPinNo" runat="server" Text='<%# Eval("PINNO") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("COUNTRY") %>' />
                                                    </td>
                                                    <%--<td>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("REMARK") %>' />
                                                        </td>--%>
                                                    <td>
                                                        <asp:Label ID="lblContNo" runat="server" Text='<%# Eval("CONTACTNO") %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>


                            </div>
                            <div id="div2" runat="server" class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Add Return Details</h5>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Return Date </label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="imgReceivedDT" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtReturnDT" runat="server" TabIndex="3" MaxLength="10" CssClass="form-control" ToolTip="Select Return Date"></asp:TextBox>
                                        <ajaxToolKit:CalendarExtender ID="CeReceivedDT" runat="server" Enabled="true" EnableViewState="true"
                                            Format="dd/MM/yyyy" PopupButtonID="imgReceivedDT" TargetControlID="txtReturnDT">
                                        </ajaxToolKit:CalendarExtender>
                                         <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender31" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtReturnDT" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender31"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Date Format [dd/MM/yyyy] " ControlToValidate="txtReturnDT"
                                                    InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy] " Display="None" Text="*" ValidationGroup="Submit">
                                                </ajaxToolKit:MaskedEditValidator>
                                        <asp:RequiredFieldValidator ID="rfvReceivedDT" runat="server" ControlToValidate="txtReturnDT" Display="None" ErrorMessage="Please Enter Return Date." SetFocusOnError="true" ValidationGroup="Submit" />
                                    </div>
                                </div>


                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Return Remark   </label>
                                    </div>
                                    <asp:TextBox ID="txtRRemark" runat="server" CssClass="form-control" ToolTip="Enter Remark" TextMode="MultiLine" TabIndex="4"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="rfvRemark" runat="server" ControlToValidate="txtRRemark" Display="None" ErrorMessage="Please Enter Return Remark." SetFocusOnError="true" ValidationGroup="Submit" />
                                    </div>
                                </div>



                            </div>
                        </div>

                     <%--   ----------------Statrt 31/03/2022 shbina-----%>

                         <%--   <div class="col-12" id="divListview" runat="server">
                            <asp:Panel ID="Panel7" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="IvOutwardDispatch" runat="server">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <div class="sub-heading">
                                                <h5>Department Outward List</h5>
                                            </div>
                                            </h4>
                                               <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                   <thead class="bg-light-blue">
                                                       <tr>
                                                           <th>RFID Number
                                                           </th>
                                                           <th>From User
                                                           </th>
                                                           <th>Department
                                                           </th>
                                                           <th>Subject
                                                           </th>
                                                           <th>Post Type
                                                           </th>
                                                           <th>Letter Category
                                                           </th>
                                                           <th>Select
                                                           </th>
                                                           <th>Accept/Reject
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

                                            <%--<td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                                CommandArgument='<%# Eval("IOTRANNO") %>' ImageUrl="~/images/edit.gif" OnClick="btnEdit_Click"
                                                                ToolTip="Edit Record" />
                                                        </td>--%>
                                            <%--<td>
                                                <%# Eval("RFID_NUMBER")%>
                                                <asp:HiddenField ID="hdnID" runat="server" Value='<%# Eval("IOTRANNO") %>' />
                                            </td>
                                            <td>
                                                <%# Eval("UA_FULLNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("SUBDEPT")%>
                                            </td>
                                            <td>
                                                <%# Eval("SUBJECT")%>
                                            </td>
                                            <td>
                                                <%# Eval("POSTTYPENAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("LETTERCAT")%>                                                            
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkLetter" runat="server" ToolTip='<%# Eval("IOTRANNO") %>' />
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Select Status" CssClass="form-control">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">Accept</asp:ListItem>
                                                    <asp:ListItem Value="2">Reject</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>--%>

                        <%--   ----------------end 31/03/2022 shbina-----%>

<%--   ----------------Statrt 31/03/2022 shbina-----%>
                            <div class="col-12" id="divListview" runat="server">
                            <asp:Panel ID="Panel7" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="IvOutwardDispatch" runat="server">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <div class="sub-heading">
                                                <h5>Return Back Letter Entry List</h5>
                                            </div>
                                            </h4>
                                               <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                   <thead class="bg-light-blue">
                                                       <tr>
                                                           <th>CENTRAL REFERENCE NO
                                                           </th>
                                                           <th>DEPT SEND DATE
                                                           </th>
                                                           <th>RETURN DATE
                                                           </th>
                                                           <th>RETURN REMARK
                                                           </th>
                                                           <%--<th>Post Type
                                                           </th>
                                                           <th>Letter Category
                                                           </th>
                                                           <th>Select
                                                           </th>
                                                           <th>Accept/Reject
                                                           </th>--%>
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

                                            <%--<td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                                CommandArgument='<%# Eval("IOTRANNO") %>' ImageUrl="~/images/edit.gif" OnClick="btnEdit_Click"
                                                                ToolTip="Edit Record" />
                                                        </td>--%>
                                            <%--<td>
                                                <%# Eval("RFID_NUMBER")%>
                                                <asp:HiddenField ID="hdnID" runat="server" Value='<%# Eval("IOTRANNO") %>' />
                                            </td>--%>
                                            <td>
                                                <%# Eval("CENTRALREFERENCENO")%>
                                            </td>
                                            <td>
                                                <%# Eval("CENTRALRECSENTDT")%>
                                            </td>
                                            <td>
                                                <%# Eval("RETURN_DATE")%>
                                            </td>
                                            <td>
                                                <%# Eval("RETURN_REMARK")%>
                                            </td>
                                            <%--<td>
                                                <%# Eval("LETTERCAT")%>                                                            
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkLetter" runat="server" ToolTip='<%# Eval("IOTRANNO") %>' />
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlStatus" runat="server" ToolTip="Select Status" CssClass="form-control">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">Accept</asp:ListItem>
                                                    <asp:ListItem Value="2">Reject</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>--%>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>


  <%--   ----------------end 31/03/2022 shbina-----%>
                            <div class=" col-12 btn-footer" id="divSubmit" runat="server">

                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" TabIndex="5" />
                            <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" CssClass="btn btn-info" ToolTip="Click here to get Report" TabIndex="7" visible="false"/>
                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" TabIndex="6" />

                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />


                        </div>
                      </div>

                      </div>
                </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


