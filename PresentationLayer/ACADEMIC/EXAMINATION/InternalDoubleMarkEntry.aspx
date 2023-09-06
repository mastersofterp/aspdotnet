<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="InternalDoubleMarkEntry.aspx.cs" Inherits="ACADEMIC_InternalDoubleMarkEntry" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 75px; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updSection"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();

        }

        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });

        }
    </script>
    <script type="text/javascript">
        function chkmax(txt) {
            debugger;
            var id = 0; var s1 = 0;
            id = document.getElementById('hdS1max').value;
            s1 = document.getElementById('txtTotMarks').value;
            if (s1 > id) {
                alert('mark should not be greather than max mark');
            }

        }

    </script>
    <script src="../Content/jquery.js" type="text/javascript"></script>

    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <%--<script type="text/javascript" charset="utf-8">
        $(document).ready(function () {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });

    </script>--%>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>
    



    <asp:Panel ID="pnlMain" runat="server">
          <asp:UpdatePanel ID="updSection" runat="server">
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
        <ContentTemplate>
            <div class="row">

                <div class="col-sm-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>INTERNAL EXAM MARK ENTRY</b></h3>
                              <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;" Visible="false"
                        AlternateText="Page Help" ToolTip="Page Help" />
                            <div class="pull-right">
                                <div style="color: Red; font-weight: bold">
                                    Note : * Marked fields are mandatory
                                </div>
                            </div>
                        </div>


                        <div class="box-body" style="padding-bottom: 1px">
                            <div class="form-group col-sm-12">
                                <div class="col-sm-3 form-group">
                                <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                                 <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                               </div>
                                </div>
                                 <%-- <p class="page_help_head">
                                      <%--  <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />--%>
                                     <%-- <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" Visible="false" />
                                Edit Record&nbsp;&nbsp;
                                        <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" Visible="false" />
                                Delete Record
                            </p>--%>
                            <p class="page_help_text">
                                <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                            </p>
                            </div>

                        </div>

                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="teacherallot"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />


                            <div class="col-sm-12">
                                <div class="row">
                                    
                                    <div class="col-sm-3" style="display: none">
                                      

                    <ajaxToolKit:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="btnHelp">
                        <Animations>
                        <OnClick>
                            <Sequence>
                                <%-- Disable the button so it can't be clicked again --%>
                                <EnableAction Enabled="false" />
                                
                                <%-- Position the wire frame on top of the button and show it --%>
                                <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                                <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                                
                                <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
                                <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                                <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                                <FadeIn AnimationTarget="info" Duration=".2"/>
                                <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                                
                                <%-- Flash the text/border red and fade in the "close" button --%>
                                <Parallel AnimationTarget="info" Duration=".5">
                                    <Color PropertyKey="color" StartValue="#666666" EndValue="#FF0000" />
                                    <Color PropertyKey="borderColor" StartValue="#666666" EndValue="#FF0000" />
                                </Parallel>
                                <Parallel AnimationTarget="info" Duration=".5">
                                    <Color PropertyKey="color" StartValue="#FF0000" EndValue="#666666" />
                                    <Color PropertyKey="borderColor" StartValue="#FF0000" EndValue="#666666" />
                                 <%--   <FadeIn AnimationTarget="btnCloseParent" MaximumOpacity=".9" />--%>
                                </Parallel>
                            </Sequence>
                        </OnClick>
                        </Animations>
                    </ajaxToolKit:AnimationExtender>
                    <%--<ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
                        <Animations>
                        <OnClick>
                            <Sequence AnimationTarget="info">
                                <%--  Shrink the info panel out of view --%>
                               <%-- <StyleAction Attribute="overflow" Value="hidden"/>
                                <Parallel Duration=".3" Fps="15">
                                    <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                    <FadeOut />
                                </Parallel>--%>
                                
                                <%--  Reset the sample so it can be played again --%>
                               <%-- <StyleAction Attribute="display" Value="none"/>
                                <StyleAction Attribute="width" Value="250px"/>
                                <StyleAction Attribute="height" Value=""/>
                                <StyleAction Attribute="fontSize" Value="12px"/>
                                <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />--%>
                                
                                <%--  Enable the button so it can be played again --%>
                              <%--  <EnableAction AnimationTarget="btnHelp" Enabled="true" />
                            </Sequence>
                        </OnClick>
                        <OnMouseOver>
                            <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                        </OnMouseOver>
                        <OnMouseOut>
                            <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                        </OnMouseOut>
                        </Animations>
                    </ajaxToolKit:AnimationExtender>--%>
                                    </div>
                                </div>
                            </div>

                            <div class="box-footer col-sm-12">


                                <%--<asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" Text="Submit" />--%>
                                <div class="col-sm-12 table table-responsive" style="margin-top: 1px" >
                                    <div>
                                       <asp:ListView ID="lvCourse" runat="server">
                                <LayoutTemplate>
                                    <div id="demo-grid" >
                                        <h4> Course List </h4>
                                        <table class="table table-hover table-bordered" cellpadding="0" cellspacing="0" width="100%">
                                            <tr class="bg-light-blue">
                                                <th>Course Name
                                                </th>
                                                <th>Status</th>
                                                <th>Lock/Unlock</th>
                                                <th style="text-align: center;">Mark Entry Report</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                        <td>
                                            <asp:LinkButton ID="lnkbtnCourse" runat="server" Text='<%# Eval("ccode") + " - " + Eval("COURSE_NAME")  + "-" + Eval("college_name") %>'
                                                ToolTip='<%# Eval("COURSENO")%>' CommandArgument='<%# Eval("COLLEGE_ID") + "+" +  Eval("OPID")%>'
                                                OnClick="lnkbtnCourse_Click" />
                                            <asp:HiddenField runat="server" ID="hdncolg" Value='<%# Eval("COLLEGE_ID")%>' />
                                            <asp:HiddenField runat="server" ID="hdnopid" Value='<%# Eval("OPID")%>' />
                                            <asp:HiddenField runat="server" ID="hdnlock" Value='<%# Eval("lock")%>' />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblcompstatus" Text="-"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lbllockstatus" Text="-"></asp:Label>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:ImageButton ID="ImgReport" runat="server" CausesValidation="false" ImageUrl="~/IMAGES/print.gif"
                                                AlternateText='<%# Eval("OPID")%>' ToolTip="Report" CommandArgument='<%# Eval("COURSENO")%>'
                                                OnClick="btnReportS_Click" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView> 
                                    </div>
                                </div>

                                  <div class="text-center">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="teacherallot" OnClick="btnSave_Click" Visible="false"
                                Width="100px" ToolTip="SAVE" CssClass="btn btn-success" />&nbsp;&nbsp;
                            <asp:Button ID="btnLock" runat="server" Visible="false" Text="Lock" ValidationGroup="teacherallot" OnClick="btnLock_Click"
                                Width="100px" ToolTip="Lock" CssClass="btn btn-danger" />&nbsp;&nbsp;
                                    <asp:Button ID="btnClear" runat="server" Text="Back to course list" Visible="false" Width="150px" OnClick="btnClear_Click" CssClass="btn btn-primary" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>
                               
                                <!--ListView for marks--->
                               <div class="col-md-12" id="tdStudent" runat="server">
                                    <asp:Panel ID="pnlStudGrid" runat="server" Visible="false">
                                          <br />
                                          <br />
                                        <div class="titlebar" runat="server" id="title" style="font-weight: bold; font-size: medium; text-align: center;">
                                        </div>
                                         <br />
                                        
                                         
                                        <div id="Div2">
                                            <%--<h4>Enter Marks for following Students </h4>--%>
                                             <asp:GridView ID="gvStudent" runat="server" OnRowDataBound="gvStudent_RowDataBound" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-hover table-responsive">
                    <HeaderStyle CssClass="bg-light-blue" />
                    <AlternatingRowStyle  />
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex+1%>' Font-Size="9pt" />
                            </ItemTemplate>
                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField Visible="true" HeaderText="Student Name" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblCoursename" runat="server" Text='<%# Bind("STUDNAME") %>' 
                                    Font-Size="9pt" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="left" Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Enroll No." ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblIDNO" runat="server" Text='<%# Bind("REGNO") %>' ToolTip='<%# Bind("IDNO") %>'
                                    Font-Size="9pt" />
                                <asp:HiddenField runat="server" ID="hdnlock" Value='<%# Bind("lock") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                       
                                                
                                                <%--EXAM MARK ENTRY--%>
                        <asp:TemplateField HeaderText="C1" Visible="false" ItemStyle-Width="15%"  HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtTotMarks" runat="server" Text='<%# Bind("S1MARK")  %>' Width="70px" MaxLength="5" onblur="return CheckMark(this);"
                                    Font-Bold="true" Style="text-align: center; text-align-last: center" ToolTip='<%# Bind("S1MARK") %>' />
                                <asp:HiddenField ID="hdS1max" runat="server" Value='<%# Bind("S1MAX") %>' />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtTutMarks" runat="server" FilterType="Custom"
                                    ValidChars="0123456789-." TargetControlID="txtTotMarks">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:CompareValidator ID="comTutMarks" ValueToCompare='<%# Bind("S1MAX") %>' ControlToValidate="txtTotMarks"
                                    Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                    ValidationGroup="teacherallot" Text="*"></asp:CompareValidator>
                                <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comTutMarks" ID="cecomTutMarks" runat="server">
                                </ajaxToolKit:ValidatorCalloutExtender>
                                <asp:HiddenField runat="server" ID="hdnpaternno" Value='<%#Eval("patternno")%>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="C2" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtTAMarks" runat="server" Text='<%# Bind("S2MARK") %>' Width="70px" MaxLength="5" onblur="return CheckMark(this);"
                                    Font-Bold="true" Style="text-align: center" />
                                <asp:HiddenField ID="hdS2max" runat="server" Value='<%# Bind("S2MAX") %>' />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtMidsemMarks" runat="server" FilterType="Custom"
                                    ValidChars="0123456789-." TargetControlID="txtTAMarks">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:CompareValidator ID="comMidsemMarks" ValueToCompare='<%# Bind("S2MAX") %>' ControlToValidate="txtTAMarks"
                                    Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                    Text="*" ValidationGroup="teacherallot"></asp:CompareValidator>
                                <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comMidsemMarks" ID="cecomMidsemMinMarks" runat="server">
                                </ajaxToolKit:ValidatorCalloutExtender>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="C3" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtESPRMarks" runat="server" Text='<%# Bind("S3MARK") %>' Width="70px" MaxLength="5" onblur="return CheckMark(this);"
                                    Font-Bold="true" Style="text-align: center" />
                                <asp:HiddenField ID="hdS3max" runat="server" Value='<%# Bind("S3MAX") %>' />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAttMarks" runat="server" FilterType="Custom"
                                    ValidChars="0123456789-." TargetControlID="txtESPRMarks">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:CompareValidator ID="comAttMarks" ValueToCompare='<%# Bind("S3MAX") %>' ControlToValidate="txtESPRMarks"
                                    Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks" Text="*" ValidationGroup="teacherallot"></asp:CompareValidator>
                                <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAttMarks" ID="cecomAttMarks" runat="server">
                                </ajaxToolKit:ValidatorCalloutExtender>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="C4" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txts4mark" runat="server" Text='<%# Bind("S4MARK") %>' Width="70px" MaxLength="5" onblur="return CheckMark(this);"
                                    Font-Bold="true" Style="text-align: center" />
                                <asp:HiddenField ID="hdS4max" runat="server" Value='<%# Bind("S4MAX") %>' />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAtt1Marks" runat="server" FilterType="Custom"
                                    ValidChars="0123456789-." TargetControlID="txts4mark">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:CompareValidator ID="comAtt1Marks" ValueToCompare='<%# Bind("S4MAX") %>' ControlToValidate="txts4mark"
                                    Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                    Text="*" ValidationGroup="teacherallot"></asp:CompareValidator>
                                <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAtt1Marks" ID="cecom1AttMarks" runat="server">
                                </ajaxToolKit:ValidatorCalloutExtender>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="C5" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txts5mark" runat="server" Text='<%# Bind("S5MARK") %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);"
                                    Font-Bold="true" Style="text-align: center" />
                                <asp:HiddenField ID="hdS5max" runat="server" Value='<%# Bind("S5MAX") %>' />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAtt2Marks" runat="server" FilterType="Custom"
                                    ValidChars="0123456789-." TargetControlID="txts5mark">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:CompareValidator ID="comAtt2Marks" ValueToCompare='<%# Bind("S5MAX") %>' ControlToValidate="txts5mark"
                                    Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                    Text="*" ValidationGroup="teacherallot"></asp:CompareValidator>
                                <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAtt2Marks" ID="cecom2AttMarks" runat="server">
                                </ajaxToolKit:ValidatorCalloutExtender>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="C6" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txts6mark" runat="server" Text='<%# Bind("S6MARK") %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);"
                                    Font-Bold="true" Style="text-align: center" />
                                <asp:HiddenField ID="hdS6max" runat="server" Value='<%# Bind("S6MAX") %>' />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAtt6Marks" runat="server" FilterType="Custom"
                                    ValidChars="0123456789-." TargetControlID="txts6mark">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:CompareValidator ID="comAtt6Marks" ValueToCompare='<%# Bind("S6MAX") %>' ControlToValidate="txts6mark"
                                    Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                    Text="*" ValidationGroup="teacherallot"></asp:CompareValidator>
                                <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAtt6Marks" ID="cecom6AttMarks" runat="server">
                                </ajaxToolKit:ValidatorCalloutExtender>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="C7" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txts7mark" runat="server" Text='<%# Bind("S7MARK") %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);"
                                    Font-Bold="true" Style="text-align: center" />
                                <asp:HiddenField ID="hdS7max" runat="server" Value='<%# Bind("S7MAX") %>' />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAtt7Marks" runat="server" FilterType="Custom"
                                    ValidChars="0123456789-." TargetControlID="txts7mark">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:CompareValidator ID="comAtt7Marks" ValueToCompare='<%# Bind("S7MAX") %>' ControlToValidate="txts7mark"
                                    Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                    Text="*" ValidationGroup="teacherallot"></asp:CompareValidator>
                                <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAtt7Marks" ID="cecom7AttMarks" runat="server">
                                </ajaxToolKit:ValidatorCalloutExtender>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="C8" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txts8mark" runat="server" Text='<%# Bind("S8MARK") %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);"
                                    Font-Bold="true" Style="text-align: center" />
                                <asp:HiddenField ID="hdS8max" runat="server" Value='<%# Bind("S8MAX") %>' />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAtt8Marks" runat="server" FilterType="Custom"
                                    ValidChars="0123456789-." TargetControlID="txts8mark">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:CompareValidator ID="comAtt8Marks" ValueToCompare='<%# Bind("S8MAX") %>' ControlToValidate="txts8mark"
                                    Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                    Text="*" ValidationGroup="teacherallot"></asp:CompareValidator>
                                <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAtt8Marks" ID="cecom8AttMarks" runat="server">
                                </ajaxToolKit:ValidatorCalloutExtender>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="C10" Visible="false" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txts10mark" runat="server" Text='<%# Bind("S10MARK") %>' Width="50px" MaxLength="5" onblur="return CheckMark(this);"
                                    Font-Bold="true" Style="text-align: center" />
                                <asp:HiddenField ID="hdS10max" runat="server" Value='<%# Bind("S10MAX") %>' />
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtAtt10Marks" runat="server" FilterType="Custom"
                                    ValidChars="0123456789-." TargetControlID="txts10mark">
                                </ajaxToolKit:FilteredTextBoxExtender>
                                <asp:CompareValidator ID="comAtt10Marks" ValueToCompare='<%# Bind("S10MAX") %>' ControlToValidate="txts10mark"
                                    Operator="LessThanEqual" Type="Double" runat="server" ErrorMessage="Marks should be less than or equal to max marks"
                                    Text="*" ValidationGroup="teacherallot"></asp:CompareValidator>
                                <ajaxToolKit:ValidatorCalloutExtender TargetControlID="comAtt10Marks" ID="cecom10AttMarks" runat="server">
                                </ajaxToolKit:ValidatorCalloutExtender>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                     <%--   <asp:TemplateField HeaderText="Enroll No." ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblIDNO1" runat="server" Text='<%# Bind("ROLL_NO") %>' ToolTip='<%# Bind("IDNO") %>'
                                    Font-Size="9pt" />
                                <asp:HiddenField runat="server" ID="hdnlock1" Value='<%# Bind("lock") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
                                        </div>
                                       
                                    </asp:Panel>
                                </div>


                                <!--ListView for marks--->

                              
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            </div>


        </ContentTemplate>












       
        

      


        <%--STUDENT LIST--%>
        <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
               </asp:UpdatePanel>
    </asp:Panel>

    <div id="divMsg" runat="server"></div>


     <script type="text/javascript" language="javascript">
         // Move an element directly on top of another element (and optionally
         // make it the same size)
         function Cover(bottom, top, ignoreSize) {
             var location = Sys.UI.DomElement.getLocation(bottom);
             top.style.position = 'absolute';
             top.style.top = location.y + 'px';
             top.style.left = location.x + 'px';
             if (!ignoreSize) {
                 top.style.height = bottom.offsetHeight + 'px';
                 top.style.width = bottom.offsetWidth + 'px';
             }
         }
                    </script>
    <script language="javascript" type="text/javascript">
        //function validateMark(txt, maxmrk, txt1, txt2, txt3) {
        //    //alert('vijay');
        //    //alert(txt.value);
        //    //alert(txt3.value);
        //    //check for max marks
        //    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
        //    if (Number(txt.value) > maxmrk || Number(txt.value) < 0) {
        //        if (Number(txt.value) == 401 || Number(txt.value) == 402 || Number(txt.value) == 403)
        //        { }
        //        else {
        //            alert("Please Enter Marks in the Range of 0 to " + maxmrk.toString() + ' & Note : 401 for Absent; 402 for Debar; 403 for Malpractice ');
        //            txt.value = '';
        //            txt.select();
        //            txt.focus();
        //        }
        //    }

        //    //check for numeric
        //    IsNumeric(txt);

        //    if (Number(txt1.value) > maxmrk || Number(txt1.value) < 0) {
        //        if (Number(txt1.value) == 401 || Number(txt1.value) == 402 || Number(txt1.value) == 403)
        //        { }
        //        else {
        //            alert("Please Enter Marks in the Range of 0 to " + maxmrk.toString() + ' & Note : 401 for Absent; 402 for Debar; 403 for Malpractice ');
        //            txt1.value = '';
        //            txt1.select();
        //            txt1.focus();
        //        }
        //    }

        //    //check for numeric
        //    IsNumeric(txt1);

        //    if (Number(txt2.value) > maxmrk || Number(txt2.value) < 0) {
        //        if (Number(txt2.value) == 401 || Number(txt2.value) == 402 || Number(txt2.value) == 403)
        //        { }
        //        else {
        //            alert("Please Enter Marks in the Range of 0 to " + maxmrk.toString() + ' & Note : 401 for Absent; 402 for Debar; 403 for Malpractice ');
        //            txt2.value = '';
        //            txt2.select();
        //            txt2.focus();
        //        }
        //    }

        //    //check for numeric
        //    IsNumeric(txt2);

        //    if (Number(txt3.value) > maxmrk || Number(txt3.value) < 0) {
        //        if (Number(txt3.value) == 401 || Number(txt3.value) == 402 || Number(txt3.value) == 403)
        //        { }
        //        else {
        //            alert("Please Enter Marks in the Range of 0 to " + maxmrk.toString() + ' & Note : 401 for Absent; 402 for Debar; 403 for Malpractice ');
        //            txt3.value = '';
        //            txt3.select();
        //            txt3.focus();
        //        }
        //    }

        //    //check for numeric
        //    IsNumeric(txt3);
        //}



        function IsNumeric(txt) {
            var ValidChars = "0123456789.-";
            var num = true;
            var mChar;

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);
                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Error! Only Numeric Values Are Allowed")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }

        function IsNumeric(txt1) {
            var ValidChars = "0123456789.-";
            var num = true;
            var mChar;

            for (i = 0; i < txt1.value.length && num == true; i++) {
                mChar = txt1.value.charAt(i);
                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt1.value = '';
                    alert("Error! Only Numeric Values Are Allowed")
                    txt1.select();
                    txt1.focus();
                }
            }
            return num;
        }

        function IsNumeric(txt2) {
            var ValidChars = "0123456789.-";
            var num = true;
            var mChar;

            for (i = 0; i < txt2.value.length && num == true; i++) {
                mChar = txt2.value.charAt(i);
                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt2.value = '';
                    alert("Error! Only Numeric Values Are Allowed")
                    txt2.select();
                    txt2.focus();
                }
            }
            return num;
        }

        function IsNumeric(txt3) {
            var ValidChars = "0123456789.-";
            var num = true;
            var mChar;

            for (i = 0; i < txt3.value.length && num == true; i++) {
                mChar = txt3.value.charAt(i);
                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt3.value = '';
                    alert("Error! Only Numeric Values Are Allowed")
                    txt3.select();
                    txt3.focus();
                }
            }
            return num;
        }


        function showLockConfirm() {
            var ret = confirm('Do you want to really Lock Mark Statement for selected Exam ');
            if (ret == true)
                return true;
            else
                return false;
        }
        function CheckMark(id) {
            if (id.value < 0) {
                if (id.value == -1) {
                }
                else {
                    alert("Marks Below Zero are Not allowed. Only -1 can be entered.");
                    id.value = '';
                    id.focus();
                }
            }
        }

    </script>

    <script type="text/javascript" language="javascript">
        function totAllSubjects(headchk) {
            debugger;
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
    </script>

</asp:Content>
