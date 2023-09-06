<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BulkDetaintionEntry.aspx.cs" Inherits="ACADEMIC_EXAMINATION_BulkDetaintionEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
   <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updDetained"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size:50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>




     <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><b>DETAINTION ENTRY</b></h3>
                    <div class="pull-right">
                         <div style="color: Red; font-weight: bold">
                             &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory</div>
                </div>   
                </div>
                <div class="box-body">
                    <asp:UpdatePanel ID="updDetained" runat="server">
                        <ContentTemplate>
                          <div class="row">
                              <div class="col-md-7">
                            
                                <div class="form-group col-md-4">
                                    <label><span style="color: red;">*</span>Session</label>
                                    <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" Font-Bold="True"
                                        AutoPostBack="True" >
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                </div>

                                 <div class="form-group col-md-4">
                                    <label><span style="color: red;">*</span>Institute Name</label>
                                    <asp:DropDownList ID="ddlcollege" runat="server" AppendDataBoundItems="True" Font-Bold="True"
                                        AutoPostBack="True" ToolTip="Please Select Institute">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlcollege"
                                        Display="None" ErrorMessage="Please Select Institute" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                </div>
                               

                                  <div class="form-group col-md-4">
                                    <label><span style="color: red;">*</span>Degree</label>
                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        ValidationGroup="report" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                </div>

                                  <div class="form-group col-md-4">
                                    <label><span style="color: red;">*</span>Branch</label>
                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        ValidationGroup="report" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                </div>
                                  <div class="form-group col-md-4">
                                    <label><span style="color: red;">*</span>Scheme</label>
                                    <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="True"  ValidationGroup="report" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="report"></asp:RequiredFieldValidator>
                                </div>
                               <div class="form-group col-md-4">
                                    <label><span style="color: red;">*</span>Semester</label>
                                    <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" ValidationGroup="report">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                </div>
                                     <div class="form-group col-md-4" style="display:none">
                                       <label><span style="color: red;">*</span>Percentage From </label>
                                         <asp:TextBox ID="TxtAttFrom" MaxLength="3"  Text="0" Width="80px" MaximumValue="100" runat="server"></asp:TextBox>
                                          <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtAttFrom"
                                        Display="None" ErrorMessage="Please Select Percentage From" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>--%>
                                           <asp:RangeValidator ID="rvPercentage" runat="server" ControlToValidate="TxtAttFrom"
                                    Display="None" ErrorMessage="Please Enter Valid Percentage." MaximumValue="100"
                                    MinimumValue="0" Type="Integer"></asp:RangeValidator>
                                    
                                      
                                </div>

                                    <div class="form-group col-md-4" style="display:none">
                                       <label><span style="color: red;">*</span>Operator</label>
                                          <asp:DropDownList ID="ddloperator" runat="server" AppendDataBoundItems="True" ValidationGroup="report">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                               <asp:ListItem Value="1"><</asp:ListItem>
                                               <asp:ListItem Value="2">=</asp:ListItem>
                                               <asp:ListItem Value="3">></asp:ListItem>
                                               <asp:ListItem Value="4">>=</asp:ListItem>
                                            
                                    </asp:DropDownList>
                                         <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddloperator"
                                        Display="None" ErrorMessage="Please Select Operator" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>--%>
                                    
                                      
                                </div>
                                   
                                    <div class="form-group col-md-4" style="display:none">
                                       <label><span style="color: red;">*</span>Percentage To </label>
                                         <asp:TextBox ID="TxtAttTo" MaxLength="3" Text="100" Width="80px" runat="server"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtAttTo"
                                        Display="None" ErrorMessage="Please Select Percentage To" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>--%>
                                       <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TxtAttTo"
                                    Display="None" ErrorMessage="Please Enter Valid Percentage." MaximumValue="100"
                                    MinimumValue="0" Type="Integer"></asp:RangeValidator>
                                    
                                </div>
                           </div>
                            
                            <div class="col-md-5">

                           
                               <div class="col-md-4">
                                <fieldset class="fieldset" style="width:400px" color: Green">
                                    <legend class="legend">Note</legend>
                                                                  
                               <span style="color:red">*</span>  For Detaining the particular course please check the check box for the corrosponding student. <br />
                                     <span style="color:red">*</span> Already Checked and Disable checkbox shows that you have already Detained the Courses for particular student. <br />
                                    <span style="font-weight: bold; color: Red;">For Report Please Select </span>
                                    <br />
                                    Session->Institute->Degree->Semester->
                                             <br />
                                   
                                </fieldset>
                               
                            </div>
                           </div>
                           </div>

                               <div class="col-md-12" style="margin-top: 25px">
                                    <p class="text-center">
                                        <asp:Button ID="btnShowStudentlist" runat="server" Text="Show Students" ValidationGroup="report"
                                            CssClass="btn btn-primary" OnClick="btnShowStudentlist_Click" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Font-Bold="True"  CssClass="btn btn-success"
                                            ValidationGroup="submit" OnClick="btnSubmit_Click"  />
                                   
                                        <asp:Button ID="btnReport" runat="server" Text="Report"
                                            CssClass="btn btn-info" OnClick="btnReport_Click" />
                                        
                                        <asp:Button ID="btnExcelReport" runat="server"
                                            Text="Excel Report" OnClick="btnExcelReport_Click" CssClass="btn btn-info" />
                                         <asp:Button ID="btnlock" runat="server" Text="Lock"
                                           CssClass="btn btn-warning" Visible="false" />
                                             <asp:Button ID="butCancel" runat="server"  Text="Cancel" OnClick="butCancel_Click" CssClass="btn btn-danger" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />



                                        
                               <div class="col-md-12">
                                    <%--   <asp:Panel ID="pnlCourse" runat="server" ScrollBars="Auto">--%>
                                    <asp:ListView ID="lvCourse" runat="server" >
                                        <LayoutTemplate>
                                                <div id="demo-grid" >
                                                    <div class="titlebar">
                                                       <h3> Course List</h3>
                                                    </div>
                                                    <table  class="table table-hover table-bordered table-striped" id="divbulkdetentrylist">
                                                          <thead>
                                                        <tr class="bg-light-blue">
                                                            
                                                            <th>USN No</th>
                                                            
                                                            <th>Student Name </th>
                                                             <th>List Of Subjects</th>
                                                            
                                                           
                                                        </tr></thead>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                   
                                                 
                                                  
                                                      <td><%# Eval("ENROLLMENT_NO")%></td>
                                                    <asp:HiddenField ID="hdnenroll"  Value='<%# Eval("ENROLLMENT_NO")%>' runat="server" />
                                                      <td><%# Eval("STUDENT_NAME")%></td>
                                                    <td>
                                                          <asp:CheckBox ID="lblcode1"   Text=' <%# Eval("FINAL_COURSE_CODE1")==null?null:Eval("FINAL_COURSE_CODE1") %>' ToolTip='<%# Eval("DETAIND1")%>' runat ="server" />
                                                        <asp:CheckBox ID="CheckBox1"  Text='<%# Eval("FINAL_COURSE_CODE2")==null?null:Eval("FINAL_COURSE_CODE2") %>'   ToolTip='<%# Eval("DETAIND2")%>' runat="server" />
                                                        <asp:CheckBox ID="CheckBox2"  Text='<%# Eval("FINAL_COURSE_CODE3")==null?null:Eval("FINAL_COURSE_CODE3") %>'   ToolTip='<%# Eval("DETAIND3")%>' runat="server" />
                                                        <asp:CheckBox ID="CheckBox3"  Text='<%# Eval("FINAL_COURSE_CODE4")==null?null:Eval("FINAL_COURSE_CODE4") %>'   ToolTip='<%# Eval("DETAIND4")%>' runat ="server" />
                                                        <asp:CheckBox ID="CheckBox4"  Text='<%# Eval("FINAL_COURSE_CODE5")==null?null:Eval("FINAL_COURSE_CODE5") %>'   ToolTip='<%# Eval("DETAIND5")%>' runat ="server" />
                                                        <asp:CheckBox ID="CheckBox5"  Text='<%# Eval("FINAL_COURSE_CODE6")==null?null:Eval("FINAL_COURSE_CODE6") %>'   ToolTip='<%# Eval("DETAIND6")%>'  runat ="server" />
                                                        <asp:CheckBox ID="CheckBox6"  Text='<%# Eval("FINAL_COURSE_CODE7")==null?null:Eval("FINAL_COURSE_CODE7") %>'   ToolTip='<%# Eval("DETAIND7")%>'  runat ="server" />
                                                        <asp:CheckBox ID="CheckBox7"  Text='<%# Eval("FINAL_COURSE_CODE8")==null?null:Eval("FINAL_COURSE_CODE8") %>'   ToolTip='<%# Eval("DETAIND8")%>'  runat ="server" />
                                                        <asp:CheckBox ID="CheckBox8"  Text='<%# Eval("FINAL_COURSE_CODE9")==null?null:Eval("FINAL_COURSE_CODE9") %>'   ToolTip='<%# Eval("DETAIND9")%>'  runat ="server" />
                                                        <asp:CheckBox ID="CheckBox9"  Text='<%# Eval("FINAL_COURSE_CODE10")==null?null:Eval("FINAL_COURSE_CODE10") %>' ToolTip='<%# Eval("DETAIND10")%>'  runat ="server" />
                                                        <asp:CheckBox ID="CheckBox10" Text='<%# Eval("FINAL_COURSE_CODE11")==null?null:Eval("FINAL_COURSE_CODE11") %>' ToolTip='<%# Eval("DETAIND11")%>'  runat="server" />
                                                        <asp:CheckBox ID="CheckBox11" Text='<%# Eval("FINAL_COURSE_CODE12")==null?null:Eval("FINAL_COURSE_CODE12") %>' ToolTip='<%# Eval("DETAIND12")%>'  runat ="server" />
                                                        <asp:CheckBox ID="CheckBox12" Text='<%# Eval("FINAL_COURSE_CODE13")==null?null:Eval("FINAL_COURSE_CODE13") %>' ToolTip='<%# Eval("DETAIND13")%>'  runat ="server" />
                                                        <asp:CheckBox ID="CheckBox13" Text='<%# Eval("FINAL_COURSE_CODE14")==null?null:Eval("FINAL_COURSE_CODE14") %>' ToolTip='<%# Eval("DETAIND14")%>'  runat ="server" />
                                                        <asp:CheckBox ID="CheckBox14" Text='<%# Eval("FINAL_COURSE_CODE15")==null?null:Eval("FINAL_COURSE_CODE15") %>' ToolTip='<%# Eval("DETAIND15")%>'  runat ="server" />
                                                        <asp:CheckBox ID="CheckBox15" Text='<%# Eval("FINAL_COURSE_CODE16")==null?null:Eval("FINAL_COURSE_CODE16") %>' ToolTip='<%# Eval("DETAIND16")%>'  runat ="server" />  
                                                        <asp:CheckBox ID="CheckBox16" Text='<%# Eval("FINAL_COURSE_CODE17")==null?null:Eval("FINAL_COURSE_CODE17") %>' ToolTip='<%# Eval("DETAIND17")%>'  runat ="server" />
                                                        <asp:CheckBox ID="CheckBox17" Text='<%# Eval("FINAL_COURSE_CODE18")==null?null:Eval("FINAL_COURSE_CODE18") %>' ToolTip='<%# Eval("DETAIND18")%>'  runat ="server" />
                                                        <asp:CheckBox ID="CheckBox18" Text='<%# Eval("FINAL_COURSE_CODE19")==null?null:Eval("FINAL_COURSE_CODE19") %>' ToolTip='<%# Eval("DETAIND19")%>'  runat ="server" /> 
                                                        <asp:Label ID="chlbl1" Visible="false"   Text='<%#Eval("PERCENTAGE1")==null?0.00:Eval("PERCENTAGE1") %>' ToolTip='<%#Eval("STATUS1")%>'  runat="server"></asp:Label>  
                                                        <asp:Label ID="chlbl2" Visible="false"   Text='<%#Eval("PERCENTAGE2")==null?0.00:Eval("PERCENTAGE2")%>'  ToolTip='<%#Eval("STATUS2")%>'  runat="server"></asp:Label>  
                                                        <asp:Label ID="chlbl3" Visible="false"   Text='<%#Eval("PERCENTAGE3")==null?0.00:Eval("PERCENTAGE3")%>'  ToolTip='<%#Eval("STATUS3")%>'  runat="server"></asp:Label>  
                                                        <asp:Label ID="chlbl4" Visible="false"   Text='<%#Eval("PERCENTAGE4")==null?0.00:Eval("PERCENTAGE4")%>'  ToolTip='<%#Eval("STATUS4")%>'  runat="server"></asp:Label>  
                                                        <asp:Label ID="chlbl5" Visible="false"   Text='<%#Eval("PERCENTAGE5")==null?0.00:Eval("PERCENTAGE5")%>'  ToolTip='<%#Eval("STATUS5")%>'  runat="server"></asp:Label>  
                                                        <asp:Label ID="chlbl6" Visible="false"   Text='<%#Eval("PERCENTAGE6")==null?0.00:Eval("PERCENTAGE6")%>'  ToolTip='<%#Eval("STATUS6")%>'  runat="server"></asp:Label>  
                                                        <asp:Label ID="chlbl7" Visible="false"   Text='<%#Eval("PERCENTAGE7")==null?0.00:Eval("PERCENTAGE7")%>'  ToolTip='<%#Eval("STATUS7")%>'  runat="server"></asp:Label>  
                                                        <asp:Label ID="chlbl8" Visible="false"   Text='<%#Eval("PERCENTAGE8")==null?0.00:Eval("PERCENTAGE8")%>'  ToolTip='<%#Eval("STATUS8")%>'  runat="server"></asp:Label>  
                                                        <asp:Label ID="chlbl9" Visible="false"   Text='<%#Eval("PERCENTAGE9")==null?0.00:Eval("PERCENTAGE9")%>'  ToolTip='<%#Eval("STATUS9")%>'  runat="server"></asp:Label>  
                                                        <asp:Label ID="chlbl10" Visible="false"  Text='<%#Eval("PERCENTAGE10")==null?0.00:Eval("PERCENTAGE10")%>' ToolTip='<%#Eval("STATUS10")%>'    runat="server"></asp:Label>  
                                                        <asp:Label ID="chlbl11" Visible="false"  Text='<%#Eval("PERCENTAGE11")==null?0.00:Eval("PERCENTAGE11")%>' ToolTip='<%#Eval("STATUS11")%>'    runat="server"></asp:Label>  
                                                        <asp:Label ID="chlbl12" Visible="false"  Text='<%#Eval("PERCENTAGE12")==null?0.00:Eval("PERCENTAGE12")%>' ToolTip='<%#Eval("STATUS12")%>'    runat="server"></asp:Label>  
                                                        <asp:Label ID="chlbl13" Visible="false"  Text='<%#Eval("PERCENTAGE13")==null?0.00:Eval("PERCENTAGE13")%>' ToolTip='<%#Eval("STATUS13")%>'    runat="server"></asp:Label>  
                                                        <asp:Label ID="chlbl14" Visible="false"  Text='<%#Eval("PERCENTAGE14")==null?0.00:Eval("PERCENTAGE14")%>' ToolTip='<%#Eval("STATUS14")%>'    runat="server"></asp:Label>  
                                                        <asp:Label ID="chlbl15" Visible="false"  Text='<%#Eval("PERCENTAGE15")==null?0.00:Eval("PERCENTAGE15")%>' ToolTip='<%#Eval("STATUS15")%>'    runat="server"></asp:Label>  
                                                        <asp:Label ID="chlbl16" Visible="false"  Text='<%#Eval("PERCENTAGE16")==null?0.00:Eval("PERCENTAGE16")%>' ToolTip='<%#Eval("STATUS16")%>'    runat="server"></asp:Label>  
                                                        <asp:Label ID="chlbl17" Visible="false"  Text='<%#Eval("PERCENTAGE17")==null?0.00:Eval("PERCENTAGE17")%>' ToolTip='<%#Eval("STATUS17")%>'    runat="server"></asp:Label>  
                                                        <asp:Label ID="chlbl18" Visible="false"  Text='<%#Eval("PERCENTAGE18")==null?0.00:Eval("PERCENTAGE18")%>' ToolTip='<%#Eval("STATUS18")%>'    runat="server"></asp:Label>  
                                                        <asp:Label ID="chlbl19" Visible="false"  Text='<%#Eval("PERCENTAGE19")==null?0.00:Eval("PERCENTAGE19")%>' ToolTip='<%#Eval("STATUS19")%>'    runat="server"></asp:Label>  
                                                       </td>


                                                  
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                  
                              
                                    </div>
                                        <div id="divMsg" runat="server"></div>

     </ContentTemplate>
  

                        <Triggers>
<%-- <asp:AsyncPostBackTrigger ControlID="btnShowStudentlist"  />--%>

                             <asp:PostBackTrigger ControlID="btnExcelReport" />   

 <%-- <asp:AsyncPostBackTrigger ControlID="btnExcelReport"  />--%>
                            </Triggers>

                            </asp:UpdatePanel>
               

                       </div>
                            </div>
                            </div>
                            </div>

    
    <script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#divbulkdetentrylist').DataTable({

            });
        }

        </script>
</asp:Content>

