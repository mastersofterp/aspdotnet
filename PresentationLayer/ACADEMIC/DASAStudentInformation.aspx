<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DASAStudentInformation.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_DASAStudentInformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <div style="z-index: 1; position: absolute; top: 10px; left: 500px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upddasainformation"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                 <div style="width: 120px; padding-left: 5px;text-align:center">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
      <asp:UpdatePanel ID="upddasainformation" runat="server">
                                    <ContentTemplate>
                      <div class="box box-primary">
                    <div class="box-header with-border">
                        <span class="glyphicon glyphicon-user text-blue"></span>
                        <h3 class="box-title"><b>STUDENT INFORMATION</b></h3>
                        <div class="box-tools pull-right">                           
                        </div>
                    </div>     <div class="box-body">
                       
    <div class="row">
        <div class="col-md-3" id="divtabs" runat="server">
                            <div class="col-md-12">
                                <div class="panel panel-info" style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);">
                                    <div class="panel panel-heading"><b> Click To Open Respective Information</b></div>
                                    <div class="panel-body">         
               <aside class="sidebar">

    <!-- sidebar: style can be found in sidebar.less -->
    <section class="sidebar" style="background-color:#00569d">
     <ul class="sidebar-menu">
        <!-- Optionally, you can add icons to the links -->
      <br />
           <div id="divhome" runat="server" >
            
               <li class="treeview" >
              &nbsp; <i class="fa fa-search text-yellow"><span >
                      <asp:LinkButton runat="server"  ID="lnkGoHome"
                            ToolTip="Please Click Here For Personal Details." OnClick="lnkGoHome_Click" style="color:white;font-size:16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Search New Student"> 

                      </asp:LinkButton>
                  </span>
             </i>   <%-- <i id="iSpoSucess" runat="server" visible="false" class="fa fa-check-circle-o text-success"></i>
                        <i id="iSpoFail" runat="server" class="fa fa-times text-danger" aria-hidden="true"></i>--%>
             <hr />
          </li>
           </div>
          <li class="treeview" >
              &nbsp <i class="fa fa-user text-yellow"><span >
                      <asp:LinkButton runat="server"  ID="lnkPersonalDetail"
                            ToolTip="Please Click Here For Personal Details." OnClick="lnkPersonalDetail_Click" style="color:white;font-size:16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Personal Details"> 

                      </asp:LinkButton>
                  </span>
             </i>   <%-- <i id="iSpoSucess" runat="server" visible="false" class="fa fa-check-circle-o text-success"></i>
                        <i id="iSpoFail" runat="server" class="fa fa-times text-danger" aria-hidden="true"></i>--%>
             <hr />
          </li>
       
          <li class="treeview">
          &nbsp <i class="fa fa-map-marker text-yellow"><span>
                       <asp:LinkButton runat="server"  ID="lnkAddressDetail"
                            ToolTip="Please Click Here For Address Details." OnClick="lnkAddressDetail_Click" style="color:white;font-size:16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Address Details"> 
                      </asp:LinkButton>
                  </span>
             </i>
             <%-- <span id="iadressdetailSucess" runat="server" visible="false" class="fa fa-check-circle-o text-success" ></span>
                        <span id="iadressdetailFail" runat="server" class="fa fa-times text-danger" ></span>--%>
              <hr />
          </li>
         
           <div id="divadmissiondetails" runat="server" >
          <li class="treeview" >
          &nbsp<i class="fa fa-university text-yellow" ><span>
                       <asp:LinkButton runat="server"  ID="lnkAdmissionDetail"
                            ToolTip="Please Click Here For Personal Details." OnClick="lnkAdmissionDetail_Click" style="color:white;font-size:16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Admission Details"> 
                      </asp:LinkButton>
                  </span>
             </i> 
       <%--        <i id="iadmSucess" runat="server" visible="false" class="fa fa-check-circle-o text-success"></i>
                        <i id="iadmFail" runat="server" class="fa fa-times text-danger" aria-hidden="true"></i>--%>
             <hr />
          </li>
         </div>
          <li class="treeview" >
        <i class="fa fa-info-circle text-yellow"><span >
                       <asp:LinkButton runat="server"  ID="lnkDasaStudentInfo"
                            ToolTip="Please Click Here For DASA Student Information." OnClick="lnkDasaStudentInfo_Click" style="color:yellow;font-size:16px;" Text="DASA Student Information"> 
                      </asp:LinkButton>
                  </span>
             </i> 
             <%-- <i id="idasainfoSucess" runat="server" visible="false" class="fa fa-check-circle-o text-success"></i>
                        <i id="idasainfoFail" runat="server" class="fa fa-times text-danger" aria-hidden="true"></i>--%>
             <hr />
          </li>
          <li class="treeview" >
       &nbsp<i class="fa fa-graduation-cap text-yellow"><span >
                       <asp:LinkButton runat="server"  ID="lnkQualificationDetail"
                            ToolTip="Please Click Here For Qualification Details." OnClick="lnkQualificationDetail_Click" style="color:white;font-size:16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Qualification Details"> 
                      </asp:LinkButton>
                  </span>
             </i>
            <%--  <i id="i1qualidetailSucess" runat="server" visible="false" class="fa fa-check-circle-o text-success"></i>
                        <i id="iqualidetailFail" runat="server" class="fa fa-times text-danger" aria-hidden="true"></i>--%>
             <hr />
          </li>
             <li class="treeview" >
        &nbsp<i class="fa fa-link text-yellow"><span >
                       <asp:LinkButton runat="server"  ID="lnkotherinfo"
                            ToolTip="Please Click Here For Other Information." OnClick="lnkotherinfo_Click" style="color:white;font-size:16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Other Information"> 
                      </asp:LinkButton>
                  </span>
             </i>
               <%--  <i id="i1otherdetailSucess" runat="server" visible="false" class="fa fa-check-circle-o text-success"></i>
                        <i id="iotherdetailFail" runat="server" class="fa fa-times text-danger" aria-hidden="true"></i>--%>
                  <hr />
              
          </li>
            <li class="treeview" >
        &nbsp;<i class="glyphicon glyphicon-print text-yellow"><span >
            <asp:LinkButton runat="server" ID="lnkprintapp" OnClick="lnkprintapp_Click" style="color:white;font-size:16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Print"></asp:LinkButton>
                                   </span>
              
            </i>
                 <p></p>
                 </li>
         </ul>
        </section>
          </aside>
                                         </div>
                                </div>
                                </div>
                       </div>
                    <div class="col-md-9">
                <div class="box box-primary">
                    <div class="box-header with-border" id="Div2" runat="server">
                        <h3 class="box-title"><b>DASA Student Information</b></h3>
                        <div class="box-tools pull-right">
                  <div class="pull-right">
                         <div style="color: Red; font-weight: bold">
                             &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory</div>
                </div> 
                        </div>
                        
                    </div>
                    <div class="box-body">
                        <asp:Panel ID="dasaPanel" runat="server" Visible="true">
                            <div class="form-group col-md-4">
                                <label>Visa Expiry Date</label>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar text-blue"></i>
                                    </div>
                                    <asp:TextBox ID="txtVisaExpiry" runat="server" ToolTip="Please Enter Visa Expiry Date"
                                        CssClass="noteditable form-control" TabIndex="96" />
                                    <%--  <asp:Image ID="imgVisaExpiry" runat="server" src="../images/calendar.png" Style="cursor: pointer"
                                                Height="16px" />--%>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtVisaExpiry" PopupButtonID="imgVisaExpiry" Enabled="True">
                                    </ajaxToolKit:CalendarExtender>
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtVisaExpiry"
                                        Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True" />
                                </div>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Passport Expiry Date</label>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar text-blue"></i>
                                    </div>
                                    <asp:TextBox ID="txtPassportExpiry" runat="server" ToolTip="Please Enter Passport Expiry Date"
                                        CssClass="noteditable form-control" TabIndex="97" />
                                    <%-- <asp:Image ID="imgPassportExpiry" runat="server" src="../images/calendar.png" Style="cursor: pointer"
                                                Height="16px" />--%>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtPassportExpiry" PopupButtonID="imgPassportExpiry" Enabled="True">
                                    </ajaxToolKit:CalendarExtender>
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtPassportExpiry"
                                        Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True" />
                                </div>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Date of issue Passport</label>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar text-blue"></i>
                                    </div>
                                    <asp:TextBox ID="txtPassportIssueDate" runat="server" ToolTip="Please Enter Date Of Issue Passport"
                                        CssClass="noteditable form-control" TabIndex="99" />
                                    <%--  <asp:Image ID="imgDateofissuePassport" runat="server" src="../images/calendar.png" Style="cursor: pointer"
                                                Height="16px" />--%>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtPassportIssueDate" PopupButtonID="imgDateofissuePassport" Enabled="True">
                                    </ajaxToolKit:CalendarExtender>
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtPassportIssueDate"
                                        Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True" />
                                </div>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Stay Permit In India Valid Up</label>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar text-blue"></i>
                                    </div>
                                    <asp:TextBox ID="txtStayPermit" runat="server" ToolTip="Please Enter Date of Stay Permit In India Valid Upto"
                                        CssClass="noteditable form-control" TabIndex="100" />
                                    <%-- <asp:Image ID="imgPassportIssueDate" runat="server" src="../images/calendar.png" Style="cursor: pointer"
                                                Height="16px" />--%>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtStayPermit" PopupButtonID="imgPassportIssueDate" Enabled="True">
                                    </ajaxToolKit:CalendarExtender>
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtStayPermit"
                                        Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True" />
                                </div>
                            </div>
                            <div class="form-group col-md-4">
                                <label><%--<span style="color: red;">*</span>--%> To Whether of Indian Origin</label>
                                <br />
                                <%--                       <asp:RadioButton ID="rdoIndianOriginYes" TabIndex="101" runat="server" Text="Yes" GroupName="IndianOrigin" />
                                <asp:RadioButton ID="rdoIndianOriginNo" runat="server" Text="No" GroupName="IndianOrigin"
                                    Checked="True" />--%>

                                <asp:RadioButtonList ID="rdobtn_Indian" runat="server" TabIndex="101" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="&nbsp;Yes&nbsp;&nbsp;" Value="Y"></asp:ListItem>
                                    <asp:ListItem Text="&nbsp;No&nbsp;&nbsp;" Value="N"></asp:ListItem>
                                </asp:RadioButtonList>

                              <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Select Indian Origin Status"
                                    ControlToValidate="rdobtn_Indian" Display="None" ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Scholarship Scheme if any</label>
                                <asp:TextBox ID="txtScholarshipScheme" CssClass="form-control" runat="server" TabIndex="102" ToolTip="Please Enter Scholarship Scheme" onkeypress="return alphaOnly(event);"></asp:TextBox>
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender39" runat="server"
                                    TargetControlID="txtScholarshipScheme" FilterType="Custom" FilterMode="InvalidChars"
                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                            </div>
                            <div class="form-group col-md-4">
                                <label>Agency </label>
                                <asp:TextBox ID="txtAgency" runat="server" CssClass="form-control" TabIndex="103" ToolTip="Please Enter Agency Name" onkeypress="return alphaOnly(event);" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender40" runat="server"
                                    TargetControlID="txtAgency" FilterType="Custom" FilterMode="InvalidChars"
                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                            </div>
                            <div class="form-group col-md-4">
                                <label>Place of issue Passport</label>
                                <asp:TextBox ID="txtPassportPlace" CssClass="form-control" runat="server" TabIndex="104" ToolTip="Please Enter Place of issue Passport" onkeypress="return alphaOnly(event);" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender41" runat="server"
                                    TargetControlID="txtPassportPlace" FilterType="Custom" FilterMode="InvalidChars"
                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                            </div>
                            <div class="form-group col-md-4">
                                <label>Country/Citizenship</label>
                                <asp:TextBox ID="txtCitizenship" CssClass="form-control" runat="server" TabIndex="105" ToolTip="Please Enter Country/Citizenship" onkeypress="return alphaOnly(event);"></asp:TextBox>
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender42" runat="server"
                                    TargetControlID="txtCitizenship" FilterType="Custom" FilterMode="InvalidChars"
                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="box box-footer text-center">
                        <asp:Button ID="btnSubmit" runat="server" TabIndex="21" Text="Save & Continue >>" ToolTip="Click to Submit"
                                                class="btn btn-success"  OnClick="btnSubmit_Click" ValidationGroup="Submit" />
                             <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                               ShowSummary="False" ValidationGroup="Academic" />
                                             <%--<asp:Button ID="btnSubmit" runat="server" TabIndex="21" Text="Submit" ToolTip="Click to Report"
                                                class="btn btn-primary btnSubmit" UseSubmitBehavior="false" OnClick="btnSubmit_Click" ValidationGroup="Submit"/>--%>
                                            &nbsp;
                        
<%--                                            <asp:Button ID="btnHome" runat="server" Visible="false" TabIndex="23" Text="Go Back Home" ToolTip="Click to Go Back Home"
                                                class="btn btn-warning btnGohome" UseSubmitBehavior="false" OnClick="btnGohome_Click" />--%>

                         <button runat="server" id="btnGoHome" visible="false" onserverclick="btnGohome_Click" class="btn btn-warning btnGohome" ToolTip="Click to Go Back Home">
                                                                    <i class="fa fa-home"></i> Go Back Home
                                                                </button>

                             

                    </div>
                </div>
            </div>
         </div>
                          </div>

            
        </div>
                                        </ContentTemplate>
            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSubmit" />

                            </Triggers>
          </asp:UpdatePanel>
    
    <script type="text/javascript">
        function alphaOnly(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }
    </script>
   <div id="divMsg" runat="server"></div> 
        </asp:Content>
