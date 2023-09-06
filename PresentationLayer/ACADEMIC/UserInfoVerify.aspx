<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="UserInfoVerify.aspx.cs" Inherits="Academic_UserInfoVerify" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .modalPopup {
            background-color: #fff;
            border: 1px solid #eee;
            box-shadow: 0 3px 9px rgba(0,0,0,.5);
            padding: 10px;
        }

        .modal-dialog1 {
            width: 100%;
            /*margin: 30px auto;*/
            position: relative;
            /*width: auto;*/
            margin: 10px;
            transition: transform .3s ease-out,-webkit-transform .3s ease-out,-o-transform .3s ease-out;
            transform: translate(0,0);
        }

        .modal-body1 {
            position: relative;
        }

        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <script type="text/javascript" lang="javascript">
        function CountCharacters() {
            var maxSize = 100;



            if (document.getElementById('<%= txtRemarkSingle.ClientID %>')) {
                var ctrl = document.getElementById('<%= txtRemarkSingle.ClientID %>');
                var len = document.getElementById('<%= txtRemarkSingle.ClientID %>').value.length;
                if (len <= maxSize) {
                    var diff = parseInt(maxSize) - parseInt(len);
                }
                else {
                    ctrl.value = ctrl.value.substring(0, maxSize);
                }
            }



            return false ;
        }

        function CancelCharacters() {
            var maxSize = 100;



            if (document.getElementById('<%= txtCancelRemark.ClientID %>')) {
                var ctrl = document.getElementById('<%= txtCancelRemark.ClientID %>');
                var len = document.getElementById('<%= txtCancelRemark.ClientID %>').value.length;
                if (len <= maxSize) {
                    var diff = parseInt(maxSize) - parseInt(len);
                }
                else {
                    ctrl.value = ctrl.value.substring(0, maxSize);
                }
            }



            return false;
        }
        //function calculate() {

        //    debugger;
        //    dataRows = document.getElementById('tblstudDetails').getElementsByTagName('tr');
        //    var count = 0;

        //    for (i = 0; i < dataRows.length - 1; i++) {

        //        var chk;
        //        var ph = document.getElementById('ctl00_ContentPlaceHolder1_lvApplicantdata_ctrl' + i + '_txtJEEPhysics').value;
        //        var cm = document.getElementById('ctl00_ContentPlaceHolder1_lvApplicantdata_ctrl' + i + '_txtJEEChemistry').value;
        //        var math = document.getElementById('ctl00_ContentPlaceHolder1_lvApplicantdata_ctrl' + i + '_txtJEEMaths').value;
        //        var result = document.getElementById('ctl00_ContentPlaceHolder1_lvApplicantdata_ctrl' + i + '_txtJEETotal');

        //        //result.value = "";
        //        //var myResult = parseFloat(ph) + parseFloat(cm) + parseFloat(math);

        //        //result.value = myResult.toString();
        //        //  result.value = myResult.toString();

        //        if (ph == "") {
        //            ph = 0;
        //        }
        //        else {
        //            result.value = parseFloat(ph);
        //        }



        //        if (cm == "") {
        //            cm = 0;
        //        }
        //        else {
        //            var txtone = cm;
        //            result.value = parseFloat(txtone) + parseFloat(ph);
        //        }


        //        if (math == "") {
        //            math = 0;
        //        }
        //        else {
        //            var txttwo = math;
        //            result.value = parseFloat(txttwo) + parseFloat(ph) + parseFloat(txtone);
        //        }


        //    }


        //    return false;


        //}

    </script>
     <script type="text/javascript" lang="javascript">
         function npfCountCharacters() {
             var maxSize = 100;



             if (document.getElementById('<%= txtnpfremark.ClientID %>')) {
                var ctrl = document.getElementById('<%= txtnpfremark.ClientID %>');
                var len = document.getElementById('<%= txtnpfremark.ClientID %>').value.length;
                if (len <= maxSize) {
                    var diff = parseInt(maxSize) - parseInt(len);
                }
                else {
                    ctrl.value = ctrl.value.substring(0, maxSize);
                }
            }



            return false;
        }

        function npfCancelCharacters() {
            var maxSize = 100;



            if (document.getElementById('<%= txtnpfcancel.ClientID %>')) {
                var ctrl = document.getElementById('<%= txtnpfcancel.ClientID %>');
                var len = document.getElementById('<%= txtnpfcancel.ClientID %>').value.length;
                if (len <= maxSize) {
                    var diff = parseInt(maxSize) - parseInt(len);
                }
                else {
                    ctrl.value = ctrl.value.substring(0, maxSize);
                }
            }



            return false;
        }
         </script>
         <script type="text/javascript" lang="javascript">
             function erpCountCharacters() {
                 var maxSize = 100;



                 if (document.getElementById('<%= txterpremark.ClientID %>')) {
                 var ctrl = document.getElementById('<%= txterpremark.ClientID %>');
                 var len = document.getElementById('<%= txterpremark.ClientID %>').value.length;
                 if (len <= maxSize) {
                     var diff = parseInt(maxSize) - parseInt(len);
                 }
                 else {
                     ctrl.value = ctrl.value.substring(0, maxSize);
                 }
             }



             return false;
         }

         function erpCancelCharacters() {
             var maxSize = 100;



             if (document.getElementById('<%= txterpcancel.ClientID %>')) {
                var ctrl = document.getElementById('<%= txterpcancel.ClientID %>');
                var len = document.getElementById('<%= txterpcancel.ClientID %>').value.length;
                if (len <= maxSize) {
                    var diff = parseInt(maxSize) - parseInt(len);
                }
                else {
                    ctrl.value = ctrl.value.substring(0, maxSize);
                }
            }



            return false;
        }
         </script>
        
<%--    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updMarks"
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
    </div>--%>

    <%--<asp:UpdatePanel ID="updMarks" runat="server" ViewStateMode="Disabled" UpdateMode="Conditional">
        <ContentTemplate>--%>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">SESSION CREATION</h3>--%>
                            <h3 class="box-title">APPLICANT MARKS/DOCUMENT VERIFICATION
                            </h3>
                        </div>

                             <div class="box-body">
                                <div class="nav-tabs-custom">
                                    <ul class="nav nav-tabs" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Search NPF Data</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="1">APPLICANT MARKS/DOCUMENT VERIFICATION</a>
                                        </li>
                                    </ul>



                                      <div class="tab-content" id="my-tab-content">
                                        <div class="tab-pane active" id="tab_1">  
                                            
                                            <div>
                                    <asp:UpdateProgress ID="upduserinfo" runat="server" AssociatedUpdatePanelID="updnpfuserinfo"
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
                                <asp:UpdatePanel ID="updnpfuserinfo" runat="server">
                                    <ContentTemplate>                                     
                                       <div class="box-body">
                                <div class="col-12" id="divGeneralInfo" runat="server">
                                    <div class="row">
                                        <asp:Panel ID="Paneladmissionno" runat="server">
                                          <div class="form-group col-lg-12 col-md-12 col-12">
                                        <asp:RadioButtonList ID="rdoadmissionno" runat="server" RepeatDirection="Horizontal" TabIndex="1" OnSelectedIndexChanged="rdoadmissionno_SelectedIndexChanged" 
                                            AutoPostBack="true">
                                             <%--<asp:ListItem  Value="">&nbsp;Cancel&nbsp;&nbsp;&nbsp;</asp:ListItem>--%>
                                            <asp:ListItem  Value="1">&nbsp;NPF Application No&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">&nbsp;ERP Application No&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                        
                                    </div> 
                                  </asp:Panel>   
                                     <asp:Panel ID="PanelNPFadmissionno" runat="server" Visible="false">
                                        <div class="input-group">
                                                <asp:TextBox ID="txtREGNo" runat="server" TabIndex="2" class="watermarked" CssClass="form-control" Width="50%" />
                                                <%--<span class="input-group-addon" style="height: 50%"><a href="#" title="Search Student for Modification" data-toggle="modal" data-target="#myModal2">
                                                    <asp:Image ID="imgSearch" runat="server" ImageUrl="~/Images/search-svg.png" TabIndex="2"
                                                        AlternateText="Search Student by  Name, Prospectus No" Style="padding-left: -500px" ToolTip="Search Student by Name , Prospectus No" /></a>
                                                   
                                                </span>--%>
                                                <ajaxToolKit:TextBoxWatermarkExtender ID="watREGNo" TargetControlID="txtREGNo" runat="server" WatermarkText="Enter NPF Application No">
                                                </ajaxToolKit:TextBoxWatermarkExtender>
                                                <asp:RequiredFieldValidator ID="rfvnpfno" runat="server" ControlToValidate="txtREGNo"
                                                    ErrorMessage="Please Enter NPF Application No" Display="None" ValidationGroup="Show"
                                                    SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>
                                                <div class="form-group col-md-3">
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" 
                                                        CssClass="btn btn-info" ValidationGroup="Show" TabIndex="3" OnClick="btnSearch_Click"/>
                                                    
                                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Show"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>
                                             <div class="form-group col-md-3">
                                            <asp:Button ID="btncancelreset" runat="server" Text="Reset" 
                                                        CssClass="btn btn-warning"  TabIndex="4" OnClick="btncancelreset_Click" />
                                                 </div>
                                               <%-- <div class="form-group col-md-2">
                                                    <asp:Button ID="btnNewStudentS" runat="server" Text="New Student" ToolTip="New Student Registration" OnClick="btnNewStudentS_Click"
                                                        CssClass="btn btn-primary" TabIndex="3" />
                                                </div>--%>
                                            </div>
                                         </asp:Panel>


                                         <asp:Panel ID="PanelERPadmissionno" runat="server" Visible="false">
                                        <div class="input-group">
                                                <asp:TextBox ID="txterpregno" runat="server" TabIndex="3" class="watermarked" CssClass="form-control" Width="50%" />
                                                <%--<span class="input-group-addon" style="height: 50%"><a href="#" title="Search Student for Modification" data-toggle="modal" data-target="#myModal2">
                                                    <asp:Image ID="imgsearcherp" runat="server" ImageUrl="~/Images/search-svg.png" TabIndex="4"
                                                        AlternateText="Search Student by  Name, Prospectus No" Style="padding-left: -500px" ToolTip="Search Student by Name , Prospectus No" /></a>
                                                    
                                                </span>--%>
                                                <ajaxToolKit:TextBoxWatermarkExtender ID="watERPREGNo" TargetControlID="txterpregno" runat="server" WatermarkText="Enter ERP Application No">
                                                </ajaxToolKit:TextBoxWatermarkExtender>
                                                <asp:RequiredFieldValidator ID="rfverpno" runat="server" ControlToValidate="txterpregno"
                                                    ErrorMessage="Please Enter ERP Application No" Display="None" ValidationGroup="Show"
                                                    SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>
                                                <div class="form-group col-md-3">
                                                    <asp:Button ID="btnsearchERP" runat="server" Text="Search" 
                                                        CssClass="btn btn-info" ValidationGroup="Show" TabIndex="5" OnClick="btnsearchERP_Click"/>
                                                    <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="Show"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>
                                            <div class="form-group col-md-3">
                                                    <asp:Button ID="btncancelerp" runat="server" Text="Reset" OnClick="btncancelerp_Click" 
                                                        CssClass="btn btn-warning" TabIndex="6"/>                                                                                               
                                                </div>
                                               <%-- <div class="form-group col-md-2">
                                                    <asp:Button ID="btnNewStudentS" runat="server" Text="New Student" ToolTip="New Student Registration" OnClick="btnNewStudentS_Click"
                                                        CssClass="btn btn-primary" TabIndex="3" />
                                                </div>--%>
                                            </div>
                                         </asp:Panel>


                                        </div>


                                    </div>
                             

                                   </div>


                                <div class="col-12">
                                <div id="divnpfApplicantDetail" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-md-8 col-12">
                                            <asp:Panel ID="Pnlnpfpreview" runat="server">
                                                <asp:GridView ID="gvdata" runat="server" AutoGenerateColumns="true">

                                                </asp:GridView>

                                                <asp:ListView ID="Lvnpfpreview" runat="server"  >
                                                    <%--OnItemDataBound="lvApplicantDocDetails_ItemDataBound"--%>
                                                    <LayoutTemplate>
                                                        <div id="listViewGrid">
                                                            <div class="sub-heading">
                                                                <h5>NPF Applicant Document Details</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblDocDetails">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Document</th>
                                                                        <th>Fetch Document from NPF</th>
                                                                        <th>Preview</th>
                                                                        <th>Document Download</th>
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
                                                                 <asp:Label ID="lblNPFDocNo" runat="server"  Text='<%# Eval("DOCUMENTS") %>' ToolTip='<%# Eval("DOCUMENTNO") %>'  ></asp:Label>   
                                                                <asp:HiddenField ID="hdlinks" runat="server" Value='<%# Eval("DOCUMENTNO") %>'/>
                                                                                                            
                                                             <%--   <asp:Label ID="lblNPFDocNo" runat="server"  Text='<%# Eval("photo_file_name") %>' ToolTip='<%# Eval("FILE_UPLOAD_PASSPORT_SIZE_PHOTOGRAPH") %>' ></asp:Label>--%>
                                                                  
                                                            </td>
                                                           <%-- <td>                                                             
                                                                <asp:Label ID="lblsign" runat="server"  Text='<%# Eval("sign_file_name") %>' ToolTip='<%# Eval("FILE_UPLOAD_SIGNATURE") %>' ></asp:Label>
                                                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("NPF_ID") %>'/>
                                                            </td>--%>

                                                            <td style="text-align: center">
                                                                <%--Enabled='<%#Convert.ToInt32(Eval("DOC_STATUS"))==1 ? false : true %>'--%>
                                                                   <asp:Button ID="btnfetch" runat="server"  CssClass="btn btn-primary"  CommandArgument='<%# Eval("DOCUMENTNO") %>' Text="Fetch" OnClick="btnfetch_Click1"/>
                                                            </td>
                                                            <td>

                                                                <asp:UpdatePanel ID="updnpfPreview" runat="server">
                                                                    <%--<img src="../Images/view2.png" />--%>
                                                                    <ContentTemplate>
                                                                        <asp:ImageButton ID="imgbtnpfPrevDoc" runat="server" CommandArgument='<%# Eval("FILENAME") %>' data-target="#PassModel" data-toggle="modal" Height="20px" ImageUrl="../Images/search.png"  Text="Preview" ToolTip='<%# Eval("FILENAME") %>' Width="20px" Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>' OnClick="imgbtnpfPrevDoc_Click" />
                                                                        <%--Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>' --%>
                                                                        <asp:Label ID="lblnpfPreview" Text='<%# Convert.ToString(Eval("FILENAME"))==string.Empty ?"Preview not available":"" %>' runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                         <asp:PostBackTrigger ControlID="lbnpfdownload"/>
                                   
                                                                        <asp:AsyncPostBackTrigger ControlID="imgbtnpfPrevDoc" EventName="Click" />
                                                                        <%--<asp:AsyncPostBackTrigger ControlID="imgbtnPrevDoc" EventName="Click" />--%>
                                                                        <%--  <asp:PostBackTrigger ControlID="btnFetch"/>data-toggle="modal" data-target="#PassModel"--%>
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                            




                                                            <td>
<%--                                                                <asp:LinkButton ID="lbnpfdownload" runat="server" Text="Download" Height="40px" Width="90px"  Font-Size="Medium"  ToolTip='<%# Eval("DOCUMENTNO") %>' OnClick="lbnpfdownload_Click"></asp:LinkButton>--%>
                                                        <asp:LinkButton ID="lbnpfdownload" runat="server"  CommandArgument='<%# Eval("DOCUMENTNO") %>' Text="Download" Height="40px" Width="90px"  Font-Size="Medium"  OnClick="lbnpfdownload_Click"></asp:LinkButton>
                                                                
                                                            </td>
                                                        </tr>
                                                        

                                                    </ItemTemplate>
                                                    <EmptyDataTemplate>
                                                        <div style="text-align: center; font-family: Arial; font-size: medium; color: red; font-weight: bold">
                                                            No Record Found !
                                                        </div>
                                                    </EmptyDataTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                        <div class="col-md-4 col-12 mt-md-4 mb-3">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Application Id :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblNPFApplicationId" runat="server" Font-Bold="True"></asp:Label>
                                                    </a>
                                                </li>
                                                 <li class="list-group-item"><b>Integrated Application Id :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblERPApplicationId" runat="server" Font-Bold="True"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Student Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblnpfname" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Student Email :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblnpfemail" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item d-none"><b>Student Mobile :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblnpfmobile" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Date Of Birth </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="I1" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtnpfdob" runat="server" CssClass="form-control" TabIndex="9"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="icon" TargetControlID="txtnpfdob">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Left" Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtnpfdob" />
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divnpfRemark" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtnpfremark" runat="server" CssClass="form-control" TextMode="MultiLine" AutoComplete="off" onkeyup="return npfCountCharacters();"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Cancel Verification</label>
                                            </div>
                                            <asp:CheckBox ID="chknpfverify" runat="server" ToolTip="Cancel Verification" OnCheckedChanged="chknpfverify_CheckedChanged" AutoPostBack="true" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="dvnpfcancel" runat="server" visible="false">
                                            
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Cancel Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtnpfcancel" runat="server" ToolTip="Enter Cancel Remark" CssClass="form-control" TextMode="MultiLine" onkeyup="return npfCancelCharacters();"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer" id="divnpfbtn" runat="server">
                                        <asp:Button ID="btnnpfsYes" runat="server" CssClass="btn btn-primary"  Text="Verify" OnClick="btnnpfsYes_Click" />
                                        <asp:Button runat="server" ID="btnnpfcancel" Text="Cancel"  CssClass="btn btn-warning" OnClick="btnnpfcancel_Click" />
                                        <asp:Button ID="btnnpfno" runat="server" CssClass="btn btn-warning"  Text="Back to Student List" />
                                    </div>

                                  

                                

                                </div>
                            </div>

                                          <div class="col-12">
                                <div id="diverpapplicationDetails" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-md-8 col-12">
                                            <asp:Panel ID="Pnlerppreview" runat="server">
                                                <asp:ListView ID="Lverppreview" runat="server" >
                                                    <%-- OnItemDataBound="lvApplicantDocDetails_ItemDataBound"--%>
                                                    <LayoutTemplate>
                                                        <div id="listViewGrid">
                                                            <div class="sub-heading">
                                                                <h5>ERP Applicant Document Details</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblDocDetails">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Document Name </th>
                                                                        <th>Fetch Document From ERP</th>
                                                                        <th>Preview</th>
                                                                        <th>Download Document</th>
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
                                                                    <asp:Label ID="lblerpDocNo" runat="server"  Text='<%# Eval("DOCUMENTS") %>' ToolTip='<%# Eval("DOCUMENTNO") %>'  ></asp:Label>   
                                                                <asp:HiddenField ID="hdlinkserp" runat="server" Value='<%# Eval("DOCUMENTNO") %>'/>
                                                               <%-- <asp:Label ID="lblerpDocNo" runat="server" Text='<%# Eval("DOCNAME") %>' ToolTip='<%# Eval("DOCNO") %>'></asp:Label>--%>
                                                            </td>
                                                              <td style="text-align: center">
                                                                   <asp:Button ID="btnfetcherp" runat="server"  CssClass="btn btn-primary"  Text="Fetch" OnClick="btnfetcherp_Click" CommandArgument='<%# Eval("DOCUMENTNO") %>' Enabled='<%#Convert.ToInt32(Eval("DOC_STATUS"))==1 ? false : true %>'/>
                                                            </td>
                                                            <td>

                                                                <asp:UpdatePanel ID="upderpPreview" runat="server">
                                                                    <%--<img src="../Images/view2.png" />--%>
                                                                    <ContentTemplate>
                                                                        <asp:ImageButton ID="imgbtnerpPrevDoc" runat="server" CommandArgument='<%# Eval("FILENAME") %>' data-target="#PassModel" data-toggle="modal" Height="20px" ImageUrl="../Images/search.png"  Text="Preview" ToolTip='<%# Eval("FILENAME") %>' Width="20px" Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>' OnClick="imgbtnerpPrevDoc_Click" />
                                                                        <%--Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>' --%>
                                                                        <asp:Label ID="lblerpPreview" Text='<%# Convert.ToString(Eval("FILENAME"))==string.Empty ?"Preview not available":"" %>' runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                         
                                                                         <asp:PostBackTrigger ControlID="lbdownloaderp"/>
                                   
                                                                        <asp:AsyncPostBackTrigger ControlID="imgbtnerpPrevDoc" EventName="Click" />
                                                                        <%--  <asp:PostBackTrigger ControlID="btnFetch"/>data-toggle="modal" data-target="#PassModel"--%>
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                            <td>
                                                        <asp:LinkButton ID="lbdownloaderp" runat="server" Text="Download" Height="40px" Width="90px"  Font-Size="Medium" CommandArgument='<%# Eval("DOCUMENTNO") %>' OnClick="lbdownloaderp_Click"></asp:LinkButton>

                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <EmptyDataTemplate>
                                                        <div style="text-align: center; font-family: Arial; font-size: medium; color: red; font-weight: bold">
                                                            No Record Found !
                                                        </div>
                                                    </EmptyDataTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                        <div class="col-md-4 col-12 mt-md-4 mb-3">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Application Id :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblerpapplicationno" runat="server" Font-Bold="True"></asp:Label>
                                                    </a>
                                                </li>
                                                 <li class="list-group-item"><b>Integrated Application Id :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblerpid" runat="server" Font-Bold="True"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Student Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblerpname" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Student Email :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblerpemail" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item d-none"><b>Student Mobile :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblerpmobile" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Date Of Birth </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="I2" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txterpdob" runat="server" CssClass="form-control" TabIndex="9"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="icon" TargetControlID="txterpdob">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptNegative="Left" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Left" Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txterpdob" />
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="diverpremark" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Remark</label>
                                            </div>
                                            <asp:TextBox ID="txterpremark" runat="server" CssClass="form-control" TextMode="MultiLine" AutoComplete="off" onkeyup="return erpCountCharacters();"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Cancel Verification</label>
                                            </div>
                                            <asp:CheckBox ID="chkerpremark" runat="server" ToolTip="Cancel Verification" OnCheckedChanged="chkerpremark_CheckedChanged" AutoPostBack="true"/>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="dverpcancel" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Cancel Remark</label>
                                            </div>
                                            <asp:TextBox ID="txterpcancel" runat="server" ToolTip="Enter Cancel Remark" CssClass="form-control" TextMode="MultiLine" onkeyup="return erpCancelCharacters();"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer" id="dverpbtn" runat="server">
                                        <asp:Button ID="btberpyes" runat="server" CssClass="btn btn-primary"  Text="Verify" OnClick="btberpyes_Click"/>
                                          <asp:Button runat="server" ID="btnerpcancel" Text="Cancel"  CssClass="btn btn-warning" OnClick="btnerpcancel_Click" />
                                        <asp:Button ID="btnerpno" runat="server" CssClass="btn btn-warning"  Text="Back to Student List" Visible="false"/>
                                    </div>

                                  

                                

                                </div>
                            </div>

                                     <%-- <div class="col-12 btn-footer">
                                <asp:Button ID="btnnpfsubmit" runat="server" Text="Show" TabIndex="6" CssClass="btn btn-primary" ValidationGroup="submit"
                                     />
                                <asp:Button ID="btnnpfverify" runat="server" Enabled="false" CssClass="btn btn-primary" Text="Verify" ValidationGroup="submit" Visible="false"
                                     />

                                <asp:Button runat="server" ID="Button3" TabIndex="7" Text="Export to Excel" CssClass="btn btn-info"  Visible="false" />
                                <asp:Button runat="server" ID="btnnpfcancel" Text="Cancel" TabIndex="8" ValidationGroup="s" CssClass="btn btn-warning"  />
                                <asp:ValidationSummary ID="ValidationSummary5" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ValidationGroup="submit" ShowSummary="False" />
                            </div>--%>

                                      </ContentTemplate>
                                     <Triggers>
                                       <%--  <asp:AsyncPostBackTrigger ControlID="btnfetch" />--%>
                                      <%-- <asp:PostBackTrigger ControlID="btnfetch"/>--%>
                                     </Triggers>
                                </asp:UpdatePanel>
                            </div>

                    <div class="tab-pane fade" id="tab_2">
                                                <div>
                                                <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updMarks"
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
                       <%--   </div>--%>
                                            <asp:UpdatePanel ID="updMarks" runat="server">
                                                <ContentTemplate>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch </label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Admission Batch" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged"
                                            ValidationGroup="submit" Font-Bold="True">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlAdmBatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Programme Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlProgramType" CssClass="form-control" data-select2-enable="true" runat="server" ToolTip="Please Select Programme Type" TabIndex="2" AppendDataBoundItems="True"
                                            ValidationGroup="submit" Font-Bold="True" AutoPostBack="true" OnSelectedIndexChanged="ddlProgramType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">UG</asp:ListItem>
                                            <asp:ListItem Value="2">PG</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlProgramType"
                                            Display="None" ErrorMessage="Please Select Programme Type." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree </label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" ToolTip="Please Select Degree" Font-Bold="true" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Programme/Branch </label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="4" AppendDataBoundItems="true" ToolTip="Please Select Programme/Branch" Font-Bold="true" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Programme/Branch" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Application Status </label>
                                        </div>
                                        <asp:RadioButtonList ID="rdoStatus" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdoStatus_SelectedIndexChanged" TabIndex="5">
                                            <asp:ListItem Value="1" Selected="True">Complete&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="0">Incomplete &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2" Selected="True">Both</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <asp:Label runat="server" ID="lblerrormsg" Visible="false" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Show" TabIndex="6" CssClass="btn btn-primary" ValidationGroup="submit"
                                    OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnVerify" runat="server" Enabled="false" CssClass="btn btn-primary" Text="Verify" ValidationGroup="submit" Visible="false"
                                    OnClick="btnVerify_Click" />

                                <asp:Button runat="server" ID="btnexport" TabIndex="7" Text="Export to Excel" CssClass="btn btn-info" OnClick="btnexport_Click" Visible="false" />
                                <asp:Button runat="server" ID="btnCancel" Text="Cancel" TabIndex="8" ValidationGroup="s" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ValidationGroup="submit" ShowSummary="False" />
                            </div>

                            <div class="col-12">
                                <div id="divDetails" runat="server" visible="false">
                                    <asp:Panel ID="pnlList" runat="server">
                                        <asp:ListView ID="lvApplicantdata" runat="server" Visible="false">
                                            <LayoutTemplate>
                                                <div id="listViewGrid">
                                                    <div class="sub-heading">
                                                        <h5>Applicant Document Verification</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblstudDetails">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Sr No. </th>
                                                                <th>Application ID </th>
                                                                <th>Integrated Application ID </th>
                                                                <th>Name </th>
                                                                <th>DEGREE</th>
                                                                <th>BRANCH</th>
                                                                <th>DOB
                                                                                <br />
                                                                    (dd/mm/yyyy) </th>
                                                                <th>Application Status </th>
                                                                <th>Verify (Marks/Document) </th>
                                                                <th>Verification Cancel Status</th>
                                                                <th>Edit </th>
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
                                                        <asp:Label ID="lblsrno" runat="server" Text="<%#Container.DataItemIndex+1 %>" />
                                                    </td>
                                                    <td><%# Eval("USERNAME")%></td>
                                                    <td><%# Eval("NPF_APPLICATION_ID")%></td>
                                                    <td><%# Eval("FIRSTNAME")%>
                                                        <asp:HiddenField ID="hiduserno" runat="server" Value='<%# Eval("USERNO") %>' />
                                                    </td>
                                                    <td><%# Eval("DEGREENAME")%></td>
                                                    <td><%# Eval("LONGNAME")%></td>
                                                    <td><%# Eval("DOB")%></td>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" ForeColor='<%# (Convert.ToInt32(Eval("CSTATUS"))==0 ? System.Drawing.Color.Red:System.Drawing.Color.Green) %>' Text='<%# (Convert.ToString(Eval("CSTATUS"))=="1" ? "COMPLETE" : "INCOMPLETE") %>' />
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:CheckBox ID="chkVerify" runat="server" BorderColor="Green" Checked='<%# (Convert.ToInt32(Eval("VERIFY_STATUS"))==1 ? true : false) %>' Enabled='<%# (Convert.ToInt32(Eval("VERIFY_STATUS"))==1 ? false : true) %>' Font-Bold="true" ToolTip=' <%# Eval("VERIFY_STATUS")%>' ValidationGroup="Submit" Width="90%" />
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="lblCancel" runat="server" ForeColor='<%#(Convert.ToInt32(Eval("CANCEL"))==1?System.Drawing.Color.Red:System.Drawing.Color.Green) %>' Text='<%#(Convert.ToString(Eval("CANCEL"))=="1"?"Yes":"No") %>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: center; border: 1PX solid #DFDFDF">

                                                        <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("USERNO")%>' ImageUrl="../Images/edit1.gif" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                        <%--Enabled='<%# (Convert.ToInt32(Eval("VERIFY_STATUS"))==1 ? false : true) %>'--%> 
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="col-12">
                                <div id="divApplicantDetail" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-md-8 col-12">
                                            <asp:Panel ID="pnlDocument" runat="server">
                                                <asp:ListView ID="lvApplicantDocDetails" runat="server" OnItemDataBound="lvApplicantDocDetails_ItemDataBound">
                                                    <%-- OnItemDataBound="lvApplicantDocDetails_ItemDataBound"--%>
                                                    <LayoutTemplate>
                                                        <div id="listViewGrid">
                                                            <div class="sub-heading">
                                                                <h5>Applicant Document Details</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblDocDetails">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Document Name </th>
                                                                        <th>Preview</th>
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
                                                                <asp:Label ID="lblDocNo" runat="server" Text='<%# Eval("DOCNAME") %>' ToolTip='<%# Eval("DOCNO") %>'></asp:Label>
                                                            </td>
                                                            <td>

                                                                <asp:UpdatePanel ID="updPreview" runat="server">
                                                                    <%--<img src="../Images/view2.png" />--%>
                                                                    <ContentTemplate>
                                                                        <asp:ImageButton ID="imgbtnPrevDoc" runat="server" CommandArgument='<%# Eval("FILENAME") %>' data-target="#PassModel" data-toggle="modal" Height="20px" ImageUrl="../Images/search.png" OnClick="imgbtnPrevDoc_Click" Text="Preview" ToolTip='<%# Eval("FILENAME") %>' Width="20px" Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>' />
                                                                        <%--Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>' --%>
                                                                        <asp:Label ID="lblPreview" Text='<%# Convert.ToString(Eval("FILENAME"))==string.Empty ?"Preview not available":"" %>' runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <%--<asp:AsyncPostBackTrigger ControlID="imgbtnPrevDoc" EventName="Click" />--%>
                                                                        <%--  <asp:PostBackTrigger ControlID="btnFetch"/>data-toggle="modal" data-target="#PassModel"--%>
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <EmptyDataTemplate>
                                                        <div style="text-align: center; font-family: Arial; font-size: medium; color: red; font-weight: bold">
                                                            No Record Found !
                                                        </div>
                                                    </EmptyDataTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                        <div class="col-md-4 col-12 mt-md-4 mb-3">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Application Id :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblApplicationId" runat="server" Font-Bold="True"></asp:Label>
                                                    </a>
                                                </li>
                                                 <li class="list-group-item"><b>Integrated Application Id :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblInteratedApplicationId" runat="server" Font-Bold="True"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Student Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Student Email :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblEmail" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item d-none"><b>Student Mobile :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblMobile" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Date Of Birth </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="dvcal1" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="form-control" TabIndex="9"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="icon" TargetControlID="txtDateOfBirth">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" DisplayMoney="Left" Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDateOfBirth" />
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divRemark" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtRemarkSingle" runat="server" CssClass="form-control" TextMode="MultiLine" AutoComplete="off" onkeyup="return CountCharacters();"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Cancel Verification</label>
                                            </div>
                                            <asp:CheckBox ID="chkCancel" runat="server" ToolTip="Cancel Verification" OnCheckedChanged="chkCancel_CheckedChanged" AutoPostBack="true" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divCancelRemark" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Cancel Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtCancelRemark" runat="server" ToolTip="Enter Cancel Remark" CssClass="form-control" TextMode="MultiLine" onkeyup="return CancelCharacters();"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer" id="divbtn" runat="server">
                                        <asp:Button ID="btnsYes" runat="server" CssClass="btn btn-primary" OnClick="btnsYes_Click" Text="Verify" />
                                        <asp:Button ID="btnsNo" runat="server" CssClass="btn btn-warning" OnClick="btnsNo_Click" Text="Back to Student List" />
                                    </div>

                                    <div style="display: none">
                                        <asp:Panel ID="pnlMarks" runat="server" Visible="false">
                                            <asp:ListView ID="lvApplicantMarksDetail" runat="server" Visible="false">
                                                <LayoutTemplate>
                                                    <div id="listViewGrid" class="vista-grid">
                                                        <div class="demo-grid">
                                                            <h4>Applicant Marks Details </h4>
                                                            <table id="tblstudDetails" class="dataTable table table-bordered table-striped table-hover display">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th style="width: 5%">Sr No. </th>
                                                                        <th>Degree </th>
                                                                        <th>Branch</th>
                                                                        <th>Qualifying Exam/Subject </th>
                                                                        <th>Compulsory Subject </th>
                                                                        <th>Other Subject </th>
                                                                        <th>Year of Passing </th>
                                                                        <th>Marks Obtained in Qualifying Exam/Subject </th>
                                                                        <th>Total Marks in Qualifying Exam/Subject </th>
                                                                        <th>Percentage </th>
                                                                        <th>Remark</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </LayoutTemplate>
                                                <EmptyDataTemplate>
                                                </EmptyDataTemplate>
                                                <ItemTemplate>
                                                    <tr class="item">
                                                        <td>
                                                            <asp:Label ID="lblsrno" runat="server" Text="<%#Container.DataItemIndex+1 %>" Width="10px" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbldegree" runat="server" Text='<%# Eval("DEGREENAME") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblbranch" runat="server" Text='<%# Eval("LONGNAME") %>'></asp:Label>
                                                            <asp:HiddenField ID="hiduserno" runat="server" Value='<%# Eval("UNO") %>' />
                                                            <asp:HiddenField ID="hidstlqno" runat="server" Value='<%# Eval("STLQNO") %>' />
                                                        </td>
                                                        <td id="qualifyno" runat="server">
                                                            <asp:Label ID="lblExam" runat="server" Text='<%# Eval("QUALIFYEXAMNAME")%>' />
                                                            <asp:HiddenField ID="hdnQualify" runat="server" Value='<%# Eval("QUALIFYNO") %>' />
                                                        </td>
                                                        <td id="Td2" runat="server" style="text-align: center">
                                                            <%--Enabled='<%# Convert.ToInt32(Eval("QSTATUS"))==0?false:true %>'--%>
                                                            <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control" AppendDataBoundItems="true" Width="200px">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%-- <asp:HiddenField ID="hdnCourse" runat="server" Value='<%#Eval("COLUMNID") %>' />--%><%--COMMENT BY JAY --%>
                                                            <%-- <asp:Label ID="lblComplCourse"  runat="server" Text='<%# Eval("COURSENAME")%>' />--%><%--COMMENT BY JAY --%>
                                                        </td>
                                                        <td id="tdOther" runat="server">
                                                            <asp:TextBox ID="txtOther" runat="server" CssClass="form-control" Width="150px">                                                                        
                                                            </asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="fbeOther" runat="server" TargetControlID="txtOther" InvalidChars="~`!@#$%^&*+=|}}{{:;?<>,_" FilterMode="InvalidChars"></ajaxToolKit:FilteredTextBoxExtender>

                                                            <%--<asp:HiddenField ID="hdnOther" runat="server" Value='<%#Eval("COLUMNID") %>' />--%><%--COMMENT BY JAY --%>
                                                            <%--<asp:HiddenField ID="hdnOtherName" runat="server" Value='<%#Eval("COLUMNID") %>' />--%><%--COMMENT BY JAY --%>

                                                            <%--<asp:Label ID="lblOtherSub" runat="server" Text='<%# Eval("OTHER_SUB") %>'></asp:Label>--%><%--COMMENT BY JAY --%>
                                                        </td>
                                                        <td id="td1" runat="server" style="text-align: center;">
                                                            <asp:Label ID="lblyearofpassing" runat="server" Text='<%# Eval("YEAR_OF_PASSING") %>'></asp:Label>
                                                        </td>

                                                        <td>

                                                            <asp:TextBox ID="txtObtMarks" runat="server" AutoPostBack="true" OnTextChanged="txtObtMarks_TextChanged" Text='<%# Eval("OBTAINED_MARKS") %>' ValidationGroup="Submit" Width="90%">          
                                                                        <%--onchange="onload(this);"--%>                                      
                                                            </asp:TextBox>

                                                            <asp:HiddenField ID="hdnOldObtMarks" runat="server" Value='<%# Eval("OBTAINED_MARKS") %>' />
                                                        </td>

                                                        <asp:UpdatePanel ID="updApplicant" runat="server" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <td>

                                                                    <asp:TextBox ID="txtMaxMarks" runat="server" AutoPostBack="true" OnTextChanged="txtMaxMarks_TextChanged" Text='<%# Eval("OUT_OF_MARKS") %>' ValidationGroup="Submit" Width="90%">
                                                                    </asp:TextBox>

                                                                    <asp:HiddenField ID="hdnOldMaxMarks" runat="server" Value='<%# Eval("OUT_OF_MARKS") %>' />
                                                                </td>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblPercent" runat="server" Text='<%# Eval("PERCENTAGE")%>' />
                                                            <asp:HiddenField ID="hdnOldPercent" runat="server" Value='<%# Eval("PERCENTAGE") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRemarkMult" runat="server" ToolTip="Enter Remark" MaxLength="100" Text='<%# Eval("REMARK") %>' Width="250px" AutoComplete="off" onkeyup="return CountCharacters();"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlU32" runat="server" Visible="false">
                                            <asp:ListView ID="lvU32" runat="server" Visible="false">
                                                <LayoutTemplate>
                                                    <div id="listViewGrid" class="vista-grid">
                                                        <div class="demo-grid">
                                                            <h4>Applicant Marks Details </h4>
                                                            <table id="tblstudDetails" class="dataTable table table-bordered table-striped table-hover">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th style="width: 5%">Sr No. </th>
                                                                        <th>Programme Code </th>
                                                                        <th>Qualifying Exam/Subject </th>
                                                                        <th>Method Subject</th>
                                                                        <th>Qualify Paper</th>
                                                                        <th>12th Subject Type</th>
                                                                        <th>12th Subject Name</th>
                                                                        <th>Sub Degree</th>
                                                                        <th>Sub Branch</th>
                                                                        <%--<th>Compulsory Subject </th>--%>
                                                                        <%-- <th>Other Subject </th>
                                                                                <th>Year of Passing </th>--%>
                                                                        <th>Marks Obtained in Qualifying Exam/Subject </th>
                                                                        <th>Total Marks in Qualifying Exam/Subject </th>
                                                                        <th>Percentage </th>
                                                                        <th>12th Average percentage</th>
                                                                        <th>Remark</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </LayoutTemplate>
                                                <EmptyDataTemplate>
                                                </EmptyDataTemplate>
                                                <ItemTemplate>
                                                    <tr class="item">
                                                        <td>
                                                            <asp:Label ID="lblsrnoU32" runat="server" Text="<%#Container.DataItemIndex+1 %>" Width="10px" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblProgrammeU32" runat="server" Text='<%# Eval("PROGRAMME_CODE") %>'></asp:Label>
                                                            <asp:HiddenField ID="hidusernoU32" runat="server" Value='<%# Eval("UNO") %>' />
                                                            <asp:HiddenField ID="hidstlqnoU32" runat="server" Value='<%# Eval("STLQNO") %>' />
                                                        </td>
                                                        <td id="qualifynoU32" runat="server">
                                                            <asp:Label ID="lblExamU32" runat="server" Text='<%# Eval("QUALIFYEXAMNAME")%>' />
                                                            <asp:HiddenField ID="hdnQualifyU32" runat="server" Value='<%# Eval("QUALIFYNO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblMethod" runat="server" Text='<%# Eval("U32_METHOD")%>' />
                                                            <asp:HiddenField ID="hdnMethod" runat="server" Value='<%# Eval("U32_METHOD_SUB") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtQualPaper" runat="server" Text='<%#Eval("QUALIFYPAPER") %>' Width="250px" Enabled='<%# Eval("QUALIFYPAPER").Equals("NA")? false:true %>' MaxLength="120"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl12subType" runat="server" Text='<%# Eval("12TH_SUB_TYPE")%>' Width="150px" />
                                                            <asp:HiddenField ID="hdn12subType" runat="server" Value='<%# Eval("12TH_SUBNO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt12SubType" runat="server" Text='<%# Eval("12TH_SUB_NAME") %>' Width="250px" Enabled='<%# Eval("12TH_SUB_NAME").Equals("NA")? false:true %>' MaxLength="120"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblsubDegree" runat="server" Text='<%# Eval("SUB_DEGREE_NAME")%>' />
                                                            <asp:HiddenField ID="hdnsubDegree" runat="server" Value='<%# Eval("SUB_DEGREE") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblsubBranch" runat="server" Text='<%# Eval("SUB_BRANCH_NAME")%>' />
                                                            <asp:HiddenField ID="hdnsubBranch" runat="server" Value='<%# Eval("SUB_BRANCH") %>' />
                                                        </td>
                                                        <%--<td id="Td2U32" runat="server" style="text-align:center">--%>
                                                        <%--Enabled='<%# Convert.ToInt32(Eval("QSTATUS"))==0?false:true %>'--%>
                                                        <%-- <asp:DropDownList ID="ddlCourseU32"   runat="server" CssClass="form-control" AppendDataBoundItems="true" Width="200px">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:HiddenField ID="hdnCourseU32" runat="server" Value='<%#Eval("COLUMNID") %>' />
                                                                    <asp:Label ID="lblComplCourseU32"  runat="server" Text='<%# Eval("COURSENAME")%>' />--%>
                                                        <%--</td>--%>
                                                        <%--<td id="tdOtherU32" runat="server">--%>
                                                        <%--<asp:TextBox ID="txtOtherU32"   runat="server" CssClass="form-control" Width="150px">                                                                        
                                                                    </asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="fbeOtherU32" runat="server" TargetControlID="txtOther" InvalidChars="~`!@#$%^&*+=|}}{{:;?<>,_" FilterMode="InvalidChars"></ajaxToolkit:FilteredTextBoxExtender>--%>

                                                        <%--<asp:HiddenField ID="hdnOther" runat="server" Value='<%#Eval("COLUMNID") %>' />--%>
                                                        <%--<asp:HiddenField ID="hdnOtherName" runat="server" Value='<%#Eval("COLUMNID") %>' />--%>

                                                        <%--<asp:Label ID="lblOtherSubU32" runat="server" Text='<%# Eval("OTHER_SUB") %>'></asp:Label>--%>
                                                        <%--</td>--%>
                                                        <%--  <td id="td1U32" runat="server" style="text-align: center;">
                                                                    <asp:Label ID="Label2U32" runat="server" Text='<%# Eval("YEAR_OF_PASSING") %>'></asp:Label>
                                                                </td>--%>

                                                        <td>

                                                            <asp:TextBox ID="txtObtMarksU32" runat="server" AutoPostBack="true" OnTextChanged="txtObtMarksU32_TextChanged" Text='<%# Eval("OBTAINED_MARKS") %>' ValidationGroup="Submit" Width="90%">          
                                                                        <%--onchange="onload(this);"--%>                                      
                                                            </asp:TextBox>

                                                            <asp:HiddenField ID="hdnOldObtMarksU32" runat="server" Value='<%# Eval("OBTAINED_MARKS") %>' />
                                                        </td>

                                                        <asp:UpdatePanel ID="updApplicantU32" runat="server" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <td>

                                                                    <asp:TextBox ID="txtMaxMarksU32" runat="server" AutoPostBack="true" OnTextChanged="txtMaxMarksU32_TextChanged" Text='<%# Eval("OUT_OF_MARKS") %>' ValidationGroup="Submit" Width="90%">
                                                                    </asp:TextBox>

                                                                    <asp:HiddenField ID="hdnOldMaxMarksU32" runat="server" Value='<%# Eval("OUT_OF_MARKS") %>' />
                                                                </td>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <td style="text-align: center">
                                                            <asp:TextBox ID="lblPercentU32" runat="server" Text='<%# Eval("PERCENTAGE")%>' />
                                                            <asp:HiddenField ID="hdnOldPercentU32" runat="server" Value='<%# Eval("PERCENTAGE") %>' />
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblAvgPercent" runat="server" Text='<%# Eval("XII_AVG_PER")%>' />
                                                            <asp:HiddenField ID="hdnAvgPercent" runat="server" Value='<%# Eval("XII_AVG_PER") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRemarkMultU32" runat="server" ToolTip="Enter Remark" MaxLength="100" Text='<%# Eval("REMARK") %>' Width="250px" AutoComplete="off" onkeyup="return CountCharacters();"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlPG" runat="server" Visible="false">
                                            <asp:ListView ID="lvPG" runat="server" Visible="false">
                                                <LayoutTemplate>
                                                    <div id="listViewGrid" class="vista-grid">
                                                        <div class="demo-grid">
                                                            <h4>Applicant Marks Details </h4>
                                                            <table id="tblstudDetails" class="dataTable table table-bordered table-striped table-hover">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th style="width: 5%">Sr No. </th>
                                                                        <th>Degree </th>
                                                                        <th>Branch</th>
                                                                        <th>Qualifying Exam/Subject </th>
                                                                        <%-- <th>Compulsory Subject </th>
                                                                                <th>Other Subject </th>--%>
                                                                        <th>Year of Passing </th>
                                                                        <th>Marks Obtained in Qualifying Exam/Subject </th>
                                                                        <th>Total Marks in Qualifying Exam/Subject </th>
                                                                        <th>Percentage </th>
                                                                       
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </LayoutTemplate>
                                                <EmptyDataTemplate>
                                                </EmptyDataTemplate>
                                                <ItemTemplate>
                                                    <tr class="item">
                                                        <td>
                                                            <asp:Label ID="lblsrnoPG" runat="server" Text="<%#Container.DataItemIndex+1 %>" Width="10px" />
                                                        </td>
                                                        <td>
                                                           <asp:Label ID="lbldegree" runat="server" Text='<%# Eval("DEGREENAME") %>'></asp:Label>
                                                            <asp:HiddenField ID="hidusernoPG" runat="server" Value='<%# Eval("UNO") %>' />
                                                            <asp:HiddenField ID="hidstlqnoPG" runat="server" Value='<%# Eval("STLQNO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("LONGNAME") %>'></asp:Label>
                                                        </td>
                                                        <td id="qualifynoPG" runat="server">
                                                            <asp:Label ID="lblExamPG" runat="server" Text='<%# Eval("QUALIFYEXAMNAME")%>' />
                                                            <asp:HiddenField ID="hdnQualifyPG" runat="server" Value='<%# Eval("QUALIFYNO") %>' />
                                                        </td>
                                                        
                                                        <td id="td1PG" runat="server" style="text-align: center;">
                                                            <asp:Label ID="Label2PG" runat="server" Text='<%# Eval("YEAR_OF_PASSING") %>'></asp:Label>
                                                        </td>

                                                        <td>

                                                            <asp:TextBox ID="txtObtMarksPG" runat="server" AutoPostBack="true" OnTextChanged="txtObtMarksPG_TextChanged" Text='<%# Eval("OBTAINED_MARKS") %>' ValidationGroup="Submit" Width="90%">          
                                                                        <%--onchange="onload(this);"--%>                                      
                                                            </asp:TextBox>

                                                            <asp:HiddenField ID="hdnOldObtMarksPG" runat="server" Value='<%# Eval("OBTAINED_MARKS") %>' />
                                                        </td>

                                                        <asp:UpdatePanel ID="updApplicantPG" runat="server" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <td>

                                                                    <asp:TextBox ID="txtMaxMarksPG" runat="server" AutoPostBack="true" OnTextChanged="txtMaxMarksPG_TextChanged" Text='<%# Eval("OUT_OF_MARKS") %>' ValidationGroup="Submit" Width="90%" onblur="return calculate(this);">
                                                                    </asp:TextBox>

                                                                    <asp:HiddenField ID="hdnOldMaxMarksPG" runat="server" Value='<%# Eval("OUT_OF_MARKS") %>' />
                                                                </td>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblPercentPG" runat="server" Text='<%# Eval("PERCENTAGE")%>' />
                                                            <asp:HiddenField ID="hdnOldPercentPG" runat="server" Value='<%# Eval("PERCENTAGE") %>' />
                                                        </td>                                                     
                                                     
                                                      
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>

                                    <div class="col-sm-12" style="display: none">
                                        <div class="row">
                                            <div class="form-group col-md-4">
                                                <label>
                                                    Subject</label>
                                                <asp:TextBox ID="txtMailSubject" runat="server" AutoPostBack="true" CssClass="form-control" Width="90%">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMailSubject" Display="None" ErrorMessage="Please Enter Subject" ValidationGroup="mail"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label>
                                                    Message
                                                </label>
                                                <asp:TextBox ID="txtMailMsg" runat="server" AutoPostBack="true" CssClass="form-control" TextMode="MultiLine" Width="90%">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMailMsg" Display="None" ErrorMessage="Please Enter Message" ValidationGroup="mail"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-4 form-group" style="padding-top: 30px;">
                                                <asp:Button ID="btnSendMail" runat="server" CssClass="btn btn-success" OnClick="btnSendMail_Click" Text="Send Mail" ValidationGroup="mail" />
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="mail" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                                                    <%-- </ContentTemplate>
                                            </asp:UpdatePanel>--%>


                       </ContentTemplate>
                       <Triggers>
                       <%-- <asp:PostBackTrigger ControlID="btnSubmit" />--%>
                         <asp:PostBackTrigger ControlID="btnVerify" />
             
                        <asp:PostBackTrigger ControlID="btnCancel" />
                         <asp:PostBackTrigger ControlID="btnexport" />
           
                          <asp:PostBackTrigger ControlID="lvApplicantMarksDetail" />
                         </Triggers>
                         </asp:UpdatePanel>

                           </div>
                                                
                    </div>
                </div>  
            <div id="divMsg" runat="server">
            </div>

                        </div>
                    </div>
                </div>
           </div>
        


    <ajaxToolKit:ModalPopupExtender ID="mpeViewDocument" BehaviorID="mpeViewDocument" runat="server" PopupControlID="pnlPopup"
        TargetControlID="lnkPreview" CancelControlID="btnClose" BackgroundCssClass="modalBackground"
        OnOkScript="ResetSession()">
    </ajaxToolKit:ModalPopupExtender>
<%--    <asp:LinkButton ID="lnkPreview" runat="server" CommandArgument='<%# Eval("PREVIEW_PATH") %>'></asp:LinkButton>--%>
        <asp:LinkButton ID="lnkPreview" runat="server" CommandArgument='<%# Eval("FILENAME") %>'></asp:LinkButton>

    <!-- The Modal -->
    <div class="modal fade" id="PassModel" style="width:100%">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup">
                            <div class="modal-header">
                                <h4 class="modal-title">Document</h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>

                            <!-- Modal body -->
                            <div class="modal-body">
                                <div class="col-12">
                                    <iframe runat="server" width="100%" height="550px" id="iframeView"></iframe>
                                </div>
                            </div>

                            <!-- Modal footer -->
                            <div class="modal-footer d-none">
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-danger no" />
                            </div>
                        </asp:Panel>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    
       <%-- <ajaxToolKit:ModalPopupExtender ID="mpeViewerpdocument" BehaviorID="mpeViewerpdocument" runat="server" PopupControlID="pnlPopup1"
        TargetControlID="lnkPrevieerp" CancelControlID="btnclose1"  BackgroundCssClass="modalBackground"
        OnOkScript="ResetSession()">
    </ajaxToolKit:ModalPopupExtender>
    <asp:LinkButton ID="lnkPrevieerp" runat="server" CommandArgument='<%# Eval("FILENAME") %>'></asp:LinkButton>
     <asp:Panel ID="pnlPopup1" runat="server" CssClass="modalPopup">
        <div class="header">
          Document
                 
        </div>
        <div class="body">
           <iframe runat="server" width="680px" height="550px" id="iframeViewerp" ></iframe>
        </div>
        <div class="footer" align="right">
            <asp:Button ID="btnClose1" runat="server" Text="Close" CssClass="btn btn-danger no" />
<%--           <asp:Button ID="btnClose1" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Larger" Text="Close" CssClass="no" />--%>
        <%--</div>
    </asp:Panel>
--%>


    <%--<div class="modal fade" id="PassModel" role="dialog">
        <div class="modal-dialog text-center">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <!-- Modal content-->
                    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup">
                        <div class="modal-content">
                            <div class="header">
                                Document
                 
                            </div>
                            <div class="modal-body">
                                <div class="col-md-12">
                                    <iframe runat="server" width="500px" height="550px" id="iframeView"></iframe>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnClose" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Larger" Text="Close" CssClass="no" />
                            </div>
                        </div>
                    </asp:Panel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <%-- <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup">
        <div class="header">
            Document
                 
        </div>
        <div class="body">
            <iframe runat="server" width="680px" height="550px" id="iframeView"></iframe>
        </div>
        <div class="footer" align="right">
            <asp:Button ID="btnClose" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Larger" Text="Close" CssClass="no" />
        </div>
    </asp:Panel>--%>

    <%--<ajaxToolKit:ModalPopupExtender ID="mpeViewDocument" BehaviorID="mpeViewDocument" runat="server" PopupControlID="pnlPopup"
                TargetControlID="lnkPreview" CancelControlID="btnClose" BackgroundCssClass="modalBackground"
                OnOkScript="ResetSession()">
            </ajaxToolKit:ModalPopupExtender>
            <asp:LinkButton ID="lnkPreview" runat="server" CommandArgument='<%# Eval("PREVIEW_PATH") %>'></asp:LinkButton>--%>
    <%-- <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup">
        <div class="header">
            Document
                 
        </div>
        <div class="body">
            <iframe runat="server" width="680px" height="550px" id="iframeView"></iframe>
        </div>
        <div class="footer" align="right">
            <asp:Button ID="btnClose" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Larger" Text="Close" CssClass="no" />
        </div>
    </asp:Panel>--%>
    <%--</div>
    </div>--%>




    <%--                <script>
                    function calculate() {
                        alert('aa');
                        var calculate;
                        var txt1 = document.getElementById('ctl00_ContentPlaceHolder1_lvU32_ctrl0_lblPercentU32');
                        var txt2 = document.getElementById('ctl00_ContentPlaceHolder1_lvU32_ctrl1_lblPercentU32');
                        var txt3 = document.getElementById('ctl00_ContentPlaceHolder1_lvU32_ctrl2_lblPercentU32');
                        var txt4 = document.getElementById('ctl00_ContentPlaceHolder1_lvU32_ctrl3_lblPercentU32');
                        var txt5 = document.getElementById('ctl00_ContentPlaceHolder1_lvU32_ctrl4_lblPercentU32');
                        alert(txt1);
                        
                        //var txt1 = document.getElementById('ctl00_ContentPlaceHolder1_lvU32_ctrl5_lblPercentU32');
                    }
            </script>--%>
    <%--<ajaxToolKit:ModalPopupExtender ID="mpeViewDocument" BehaviorID="mpeViewDocument" runat="server" PopupControlID="pnlPopup"
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
    </asp:Panel>--%>
</asp:Content>
