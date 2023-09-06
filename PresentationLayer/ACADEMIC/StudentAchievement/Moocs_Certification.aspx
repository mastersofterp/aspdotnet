<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Moocs_Certification.aspx.cs" Inherits="ACADEMIC_StudentAchievement_Moocs_Certification" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <script>
         $(document).ready(function () {
             var table = $('#tblEvent').DataTable({
                 responsive: true,
                 lengthChange: true,
                 scrollY: 320,
                 scrollX: true,
                 scrollCollapse: true,
                 paging: false, // Added by Gaurav for Hide pagination

                 dom: 'lBfrtip',
                 buttons: [
                     {
                         extend: 'colvis',
                         text: 'Column Visibility',
                         columns: function (idx, data, node) {
                             var arr = [0, 6];
                             if (arr.indexOf(idx) !== -1) {
                                 return false;
                             } else {
                                 return $('#tblEvent').DataTable().column(idx).visible();
                             }
                         }
                     },
                     {
                         extend: 'collection',
                         text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                         buttons: [
 {
     extend: 'copyHtml5',
     exportOptions: {
         columns: function (idx, data, node) {
             var arr = [0];
             if (arr.indexOf(idx) !== -1) {
                 return false;
             } else {
                 return $('#tblEvent').DataTable().column(idx).visible();
             }
         },
         format: {
             body: function (data, column, row, node) {
                 var nodereturn;
                 if ($(node).find("input:text").length > 0) {
                     nodereturn = "";
                     nodereturn += $(node).find("input:text").eq(0).val();
                 }
                 else if ($(node).find("input:checkbox").length > 0) {
                     nodereturn = "";
                     $(node).find("input:checkbox").each(function () {
                         if ($(this).is(':checked')) {
                             nodereturn += "On";
                         } else {
                             nodereturn += "Off";
                         }
                     });
                 }
                 else if ($(node).find("a").length > 0) {
                     nodereturn = "";
                     $(node).find("a").each(function () {
                         nodereturn += $(this).html();
                     });
                 }
                 else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                     nodereturn = "";
                     $(node).find("span").each(function () {
                         nodereturn += $(this).html();
                     });
                 }
                 else if ($(node).find("select").length > 0) {
                     nodereturn = "";
                     $(node).find("select").each(function () {
                         var thisOption = $(this).find("option:selected").text();
                         if (thisOption !== "Please Select") {
                             nodereturn += thisOption;
                         }
                     });
                 }
                 else if ($(node).find("img").length > 0) {
                     nodereturn = "";
                 }
                 else if ($(node).find("input:hidden").length > 0) {
                     nodereturn = "";
                 }
                 else {
                     nodereturn = data;
                 }
                 return nodereturn;
             },
         },
     }
 },
 {
     extend: 'excelHtml5',
     exportOptions: {
         columns: function (idx, data, node) {
             var arr = [0, 6];
             if (arr.indexOf(idx) !== -1) {
                 return false;
             } else {
                 return $('#tblEvent').DataTable().column(idx).visible();
             }
         },
         format: {
             body: function (data, column, row, node) {
                 var nodereturn;
                 if ($(node).find("input:text").length > 0) {
                     nodereturn = "";
                     nodereturn += $(node).find("input:text").eq(0).val();
                 }
                 else if ($(node).find("input:checkbox").length > 0) {
                     nodereturn = "";
                     $(node).find("input:checkbox").each(function () {
                         if ($(this).is(':checked')) {
                             nodereturn += "On";
                         } else {
                             nodereturn += "Off";
                         }
                     });
                 }
                 else if ($(node).find("a").length > 0) {
                     nodereturn = "";
                     $(node).find("a").each(function () {
                         nodereturn += $(this).html();
                     });
                 }
                 else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                     nodereturn = "";
                     $(node).find("span").each(function () {
                         nodereturn += $(this).html();
                     });
                 }
                 else if ($(node).find("select").length > 0) {
                     nodereturn = "";
                     $(node).find("select").each(function () {
                         var thisOption = $(this).find("option:selected").text();
                         if (thisOption !== "Please Select") {
                             nodereturn += thisOption;
                         }
                     });
                 }
                 else if ($(node).find("img").length > 0) {
                     nodereturn = "";
                 }
                 else if ($(node).find("input:hidden").length > 0) {
                     nodereturn = "";
                 }
                 else {
                     nodereturn = data;
                 }
                 return nodereturn;
             },
         },
     }
 },

                         ]
                     }
                 ],
                 "bDestroy": true,
             });
         });
         var parameter = Sys.WebForms.PageRequestManager.getInstance();
         parameter.add_endRequest(function () {
             $(document).ready(function () {
                 var table = $('#tblEvent').DataTable({
                     responsive: true,
                     lengthChange: true,
                     scrollY: 320,
                     scrollX: true,
                     scrollCollapse: true,
                     paging: false, // Added by Gaurav for Hide pagination

                     dom: 'lBfrtip',
                     buttons: [
                         {
                             extend: 'colvis',
                             text: 'Column Visibility',
                             columns: function (idx, data, node) {
                                 var arr = [0, 6];
                                 if (arr.indexOf(idx) !== -1) {
                                     return false;
                                 } else {
                                     return $('#tblEvent').DataTable().column(idx).visible();
                                 }
                             }
                         },
                         {
                             extend: 'collection',
                             text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                             buttons: [
     {
         extend: 'copyHtml5',
         exportOptions: {
             columns: function (idx, data, node) {
                 var arr = [0];
                 if (arr.indexOf(idx) !== -1) {
                     return false;
                 } else {
                     return $('#tblEvent').DataTable().column(idx).visible();
                 }
             },
             format: {
                 body: function (data, column, row, node) {
                     var nodereturn;
                     if ($(node).find("input:text").length > 0) {
                         nodereturn = "";
                         nodereturn += $(node).find("input:text").eq(0).val();
                     }
                     else if ($(node).find("input:checkbox").length > 0) {
                         nodereturn = "";
                         $(node).find("input:checkbox").each(function () {
                             if ($(this).is(':checked')) {
                                 nodereturn += "On";
                             } else {
                                 nodereturn += "Off";
                             }
                         });
                     }
                     else if ($(node).find("a").length > 0) {
                         nodereturn = "";
                         $(node).find("a").each(function () {
                             nodereturn += $(this).html();
                         });
                     }
                     else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                         nodereturn = "";
                         $(node).find("span").each(function () {
                             nodereturn += $(this).html();
                         });
                     }
                     else if ($(node).find("select").length > 0) {
                         nodereturn = "";
                         $(node).find("select").each(function () {
                             var thisOption = $(this).find("option:selected").text();
                             if (thisOption !== "Please Select") {
                                 nodereturn += thisOption;
                             }
                         });
                     }
                     else if ($(node).find("img").length > 0) {
                         nodereturn = "";
                     }
                     else if ($(node).find("input:hidden").length > 0) {
                         nodereturn = "";
                     }
                     else {
                         nodereturn = data;
                     }
                     return nodereturn;
                 },
             },
         }
     },
     {
         extend: 'excelHtml5',
         exportOptions: {
             columns: function (idx, data, node) {
                 var arr = [0, 6];
                 if (arr.indexOf(idx) !== -1) {
                     return false;
                 } else {
                     return $('#tblEvent').DataTable().column(idx).visible();
                 }
             },
             format: {
                 body: function (data, column, row, node) {
                     var nodereturn;
                     if ($(node).find("input:text").length > 0) {
                         nodereturn = "";
                         nodereturn += $(node).find("input:text").eq(0).val();
                     }
                     else if ($(node).find("input:checkbox").length > 0) {
                         nodereturn = "";
                         $(node).find("input:checkbox").each(function () {
                             if ($(this).is(':checked')) {
                                 nodereturn += "On";
                             } else {
                                 nodereturn += "Off";
                             }
                         });
                     }
                     else if ($(node).find("a").length > 0) {
                         nodereturn = "";
                         $(node).find("a").each(function () {
                             nodereturn += $(this).html();
                         });
                     }
                     else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                         nodereturn = "";
                         $(node).find("span").each(function () {
                             nodereturn += $(this).html();
                         });
                     }
                     else if ($(node).find("select").length > 0) {
                         nodereturn = "";
                         $(node).find("select").each(function () {
                             var thisOption = $(this).find("option:selected").text();
                             if (thisOption !== "Please Select") {
                                 nodereturn += thisOption;
                             }
                         });
                     }
                     else if ($(node).find("img").length > 0) {
                         nodereturn = "";
                     }
                     else if ($(node).find("input:hidden").length > 0) {
                         nodereturn = "";
                     }
                     else {
                         nodereturn = data;
                     }
                     return nodereturn;
                 },
             },
         }
     },

                             ]
                         }
                     ],
                     "bDestroy": true,
                 });
             });
         });

    </script>
    <style>
        input[type=checkbox], input[type=radio] {
            margin: 0px 3px 0;
        }
    </style>

    

      <script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                ranges: {
                },
            },
        function (start, end) {
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
            document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
                document.getElementById('<%=hdnDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            });
    </script>

      <script>
          function Setdate(date) {

              var prm = Sys.WebForms.PageRequestManager.getInstance();

              //prm.add_endRequest(function () {

              $(document).ready(function () {
                  //debugger;
                  //alert("in");
                  var startDate = moment(date.split('-')[0], "MMM DD, YYYY");
                  var endtDate = moment(date.split('-')[1], "MMM DD, YYYY");
                  //$('#date').html(date);
                  $('#date').html(startDate.format("MMM DD, YYYY") + ' - ' + endtDate.format("MMM DD, YYYY"));
                  document.getElementById('<%=hdnDate.ClientID%>').value = date;
                     //$('#picker').daterangepicker({ startDate: startDate, endDate: endtDate});
                     $('#picker').daterangepicker({
                         startDate: startDate.format("MM/DD/YYYY"),
                         endDate: endtDate.format("MM/DD/YYYY"),
                         ranges: {
                         },
                     },
             function (start, end) {
                 alert(start);
                 //debugger
                 $('#date').html(start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
                 document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
             });

                 });
             //});
     };
    </script>
    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12" id="div2" runat="server">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                    <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                    </h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                             <div class="form-group col-lg-3 col-md-6 col-12" id="div">
                               <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Academic Year</label>
                                </div>
                                <asp:DropDownList ID="ddlAcademicYear" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlAcademicYear" runat="server" ControlToValidate="ddlAcademicYear"
                                    Display="None" ErrorMessage="Please Select Academic Year" SetFocusOnError="True"
                                    ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>

                             <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Name of Course</label>
                                </div>
                                <asp:TextBox ID="txtNameofCourse" runat="server" MaxLength="256" CssClass="form-control"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfctxtNameofCourse" runat="server" ControlToValidate="txtNameofCourse"
                                          Display="None" ErrorMessage="Please Enter Name Of Course" SetFocusOnError="True"
                                          ValidationGroup="Academic"></asp:RequiredFieldValidator>
                          
                                <ajaxToolKit:FilteredTextBoxExtender ID="txtCharacters_FilteredTextBoxExtender"  runat="server"  Enabled="True"  TargetControlID="txtNameofCourse"  FilterType="Custom,LowercaseLetters,UppercaseLetters"  FilterMode="ValidChars"  ValidChars="(),- " >    

                                </ajaxToolKit:FilteredTextBoxExtender>
                                 </div>   

                             <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Platform</label>
                                </div>
                                <asp:DropDownList ID="ddlPlatform" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="rfcddlPlatform" runat="server" ControlToValidate="ddlPlatform"
                                     Display="None" ErrorMessage="Please Select Platform" SetFocusOnError="True"
                                     ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>

                             <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Offered By Institute/University</label>
                                </div>
                                <asp:TextBox ID="txtOfferedByInstitute" runat="server" MaxLength="1000" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfctxtOfferedByInstitute" runat="server" ControlToValidate="txtOfferedByInstitute"
                                 Display="None" ErrorMessage="Please Enter Offered By Institute/University" SetFocusOnError="True"
                                ValidationGroup="Academic"></asp:RequiredFieldValidator>

                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"    
                                    TargetControlID="txtOfferedByInstitute"    FilterType="Custom,LowercaseLetters,UppercaseLetters"  FilterMode="ValidChars" ValidChars="(),- " >   
                                </ajaxToolKit:FilteredTextBoxExtender> 
                          </div>

                             <div class="form-group col-lg-3 col-md-6 col-12" >
                               <div class="label-dynamic">
                                <sup>* </sup>
                                <label>Start End Date</label>
                                  </div>
                                 <asp:HiddenField ID="hdnDate" runat="server" />
                               <div id="picker" class="form-control">
                              <i class="fa fa-calendar"></i>&nbsp;
                                <span id="date"></span>
                               <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                              </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                 <div class="label-dynamic">
                                  <sup>* </sup>
                                 <label>Session Start Date</label>
                                 </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="dvcal1" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtStartDate" runat="server" ValidationGroup="Academic" onpaste="return false;"
                                                TabIndex="3" ToolTip="Please Enter Session Start Date" CssClass="form-control" Style="z-index: 0;" />
                                            <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtStartDate" PopupButtonID="dvcal1" />
                                            <%-- <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                                Display="None" ErrorMessage="Please Enter Session Start Date" SetFocusOnError="True"
                                                ValidationGroup="submit" />--%>
                                            <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtStartDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" ControlExtender="meeStartDate"
                                                ControlToValidate="txtStartDate" EmptyValueMessage="Please Enter Start Date"
                                                InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                TooltipMessage="Please Enter Start Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                ValidationGroup="Academic" SetFocusOnError="True" />
                                        </div>
                                
                                     <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session End Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="dvcal2" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtEndDate" runat="server" ValidationGroup="Academic" TabIndex="4"
                                                ToolTip="Please Enter Session End Date" CssClass="form-control" Style="z-index: 0;" />
                                           <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtEndDate" PopupButtonID="dvcal2" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeEndDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtEndDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevEndDate" runat="server" ControlExtender="meeEndDate"
                                                ControlToValidate="txtEndDate" EmptyValueMessage="Please Enter Session End Date"
                                                InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                TooltipMessage="Please Enter Session End Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Academic" SetFocusOnError="True" />
                                      </div>
                                  
                                     </div> 
                                    </div>
                                 </div>

                             <div class="form-group col-lg-3 col-md-6 col-12" >
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Duration</label>
                                </div>
                                <asp:DropDownList ID="ddlDuration" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="rfcddlDuration" runat="server" ControlToValidate="ddlDuration"
                                   Display="None" ErrorMessage="Please Select Duration" SetFocusOnError="True"
                                   ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>

                             <div class="form-group col-lg-3 col-md-6 col-12" >
                                <div class="label-dynamic">
                                    <label>Whether you got Financial Assistance</label>
                                </div>
                                <div class="switch form-inline">
                                    <input type="checkbox" id="rdActive" name="switch"  />
                                    <label data-on="Yes" data-off="No" for="rdActive"></label>
                                   
                                </div>
                                <%--<asp:RadioButtonList ID="chk" runat="server" RepeatColumns="2">
                                    <asp:ListItem ID="chkYes" runat="server" Text="Yes"></asp:ListItem>
                                    <asp:ListItem ID="chkNo" runat="server" Text="No"></asp:ListItem>
                                </asp:RadioButtonList>--%>
                            </div>

                             <div class="form-group col-lg-3 col-md-6 col-12" >
                                <div class="label-dynamic">
                                    <label>If Yes, Amount in Rs</label>
                                </div>
                                <asp:TextBox ID="txtAmount" runat="server" MaxLength="10" Enabled="false" CssClass="form-control"></asp:TextBox>
                                       <ajaxToolKit:filteredtextboxextender    ID="Filteredtextboxextender2"  runat="server"  Enabled="True"  TargetControlID="txtAmount" FilterMode="ValidChars" ValidChars="0123456789" >    
                                 </ajaxToolKit:filteredtextboxextender>  
                            </div>

                             <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Upload Certificate <small style="color: red;">(Choose only PDF file)</small></label>
                                </div>
                                <div id="Div1" class="logoContainer" runat="server">
                                    <img src="../../Images/default-fileupload.png" alt="upload image" runat="server" id="imgUpFile" />
                                </div>
                                <div class="fileContainer sprite pl-1">
                                    <span runat="server" id="ufFile"
                                        cssclass="form-control" tabindex="7">Upload File</span>
                                    <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Select file to upload"
                                        CssClass="form-control" onkeypress="" />
                                </div>
                             </div>
                         </div>
             
                     <div class="col-12 btn-footer">
                        <asp:LinkButton ID="btnSubmitMoocsCertification" runat="server" CssClass="btn btn-primary" ValidationGroup="Academic" OnClientClick="return valiMoocs();"  OnClick="btnSubmitMoocsCertification_Click">Submit</asp:LinkButton>
                        <asp:LinkButton ID="btnCancelMoocsCertification" runat="server" CssClass="btn btn-warning" OnClick="btnCancelMoocsCertification_Click">Cancel</asp:LinkButton>
                    </div>
                       <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="Academic" />


                    <div class="col-12 mt-3">
                           <div class="sub-heading">
                                     <h5>Moocs Certification List</h5>
                                 </div>
                        <asp:Panel ID="pnlMoocs" runat="server" Visible="false">
                            <asp:ListView ID="lvMoocs" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="lvMoocs_ItemEditing">
                                <LayoutTemplate>
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblEvent">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Edit</th>
                                                <th>Name of Course</th>
                                                <th>Platform</th>
                                                <th>Offered By Institute/University</th>
                                                <th>Start Date</th>
                                                 <th>End Date</th>
                                                 <th>Download</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>

                                            <asp:LinkButton ID="btnEditMoocs" runat="server" CssClass="fas fa-edit" OnClick="btnEditMoocs_Click" CommandArgument='<%#Eval("MOOCD_CERTIFICATION_ID") %>' CommandName="Edit"></asp:LinkButton>
                                        </td>
                                        <td><%# Eval("NAME_OF_COURSE") %></td>
                                        <td><%# Eval("MOOCS_PLATFORM") %></td>
                                        <td><%# Eval("OFFERED_BY_INSTITUTE_UNIVERSITY") %></td>
                                         <td><%# Convert.ToDateTime (Eval("STDATE")).ToString("d") %></td>
                                        <td><%# Convert.ToDateTime (Eval("ENDDATE")).ToString("d") %></td>
                                         <td>
                                        <%-- <asp:ImageButton ID="lnkDownload" runat="server" ImageUrl="~/IMAGES/download.png" AlternateText="DownLoad"  CommandArgument='<%# Eval("FILE_NAME") %>' />--%>
                                        <asp:Button ID="btnDownload" runat="server" Text="Download"  CssClass="btn btn-primary" OnClick="btnDownload_Click" CommandArgument='<%# Eval("FILE_NAME") %>' ToolTip='<%# Eval("FILE_NAME") %>' />
                                    </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                  </div>
            </div>
        </div>
    </div>
           
    <!-- Start End Date Script -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                ranges: {
                },
            },
        function (start, end) {
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
        });

            $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
        });
    </script>

    <!-- pdf upload Script -->
    <script>
        $(document).ready(function () {
            $(document).on("click", ".logoContainer", function () {
                $("#ctl00_ContentPlaceHolder1_FileUpload1").click();
            });
            $(document).on("keydown", ".logoContainer", function () {
                if (event.keyCode === 13) {
                    // Cancel the default action, if needed
                    event.preventDefault();
                    // Trigger the button element with a click
                    $("#ctl00_ContentPlaceHolder1_FileUpload1").click();
                }
            });
        });
    </script>

    <script type="text/javascript">
        function Focus() {
           
            document.getElementById("<%=imgUpFile.ClientID%>").focus();
        }
    </script>

    <script>
        $("input:file").change(function () {
            //$('.fuCollegeLogo').on('change', function () {

            var maxFileSize = 1000000;
            var fi = document.getElementById('ctl00_ContentPlaceHolder1_FileUpload1');
            myfile = $(this).val();
            var ext = myfile.split('.').pop();
            var res = ext.toUpperCase();

            //alert(res)
            //===========zip/rar file upload changes==========
            //if (res != "RAR" && res != "ZIP") {
            //  alert("Please Select rar,zip File Only.");

            if (res != "PDF" && res != "XLSX" && res != "PNG" && res != "XLS") {
                alert("Please Select pdf,xlsx,XLS File Only.");
                $('.logoContainer img').attr('src', "../../Images/default-fileupload.png");
                $(this).val('');
            }

            for (var i = 0; i <= fi.files.length - 1; i++) {
                var fsize = fi.files.item(i).size;
                if (fsize >= maxFileSize) {
                    alert("File Size should be less than 1 MB");
                    $('.logoContainer img').attr('src', "../../Images/default-fileupload.png");
                    $("#ctl00_ContentPlaceHolder1_FileUpload1").val("");

                }
            }

        });
    </script>

    <script>
        $("input:file").change(function () {
            var fileName = $(this).val();

            newText = fileName.replace(/fakepath/g, '');
            var newtext1 = newText.replace(/C:/, '');
            //newtext2 = newtext1.replace('//', ''); 
            var result = newtext1.substring(2, newtext1.length);


            if (result.length > 0) {
                $(this).parent().children('span').html(result);
            }
            else {
                $(this).parent().children('span').html("Choose file");
            }
        });
        //file input preview
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('.logoContainer img').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        $("input:file").change(function () {
            readURL(this);
        });
    </script>

    <script>
             function SetMoocs(val) {
                 $('#rdActive').prop('checked', val);
             }

             function valiMoocs() {

                 $('#hfdActive').val($('#rdActive').prop('checked'));
             }
             var prm = Sys.WebForms.PageRequestManager.getInstance();
             prm.add_endRequest(function () {
                 $(function () {
                     $('#btnSubmitMoocsCertification').click(function () {
                         validate();
                     });
                 });
             });
    </script>
     
    <script>
        $('#rdActive').click(function () {
           


                var txt = document.getElementById("<%=txtAmount.ClientID%>");


             if (txt.disabled) {

                 document.getElementById("<%=txtAmount.ClientID%>").disabled = false;
             }
             else {

                 document.getElementById("<%=txtAmount.ClientID%>").disabled = true;
             }

         })

  </script>
   

    
</asp:Content>

