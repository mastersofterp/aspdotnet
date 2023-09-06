<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="MarkEntryComparision.aspx.cs" Inherits="ACADEMIC_MarkEntryComparision" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div style="z-index: 1; position: absolute; top: 75px; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updCompare"
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
        <asp:UpdatePanel ID="updCompare" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><b>EXAM MARK ENTRY COMPARISION</b> </h3>
                                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                    AlternateText="Page Help" ToolTip="Page Help" Visible="false" />
                                <div class="box-tools pull-right">
                                    <div style="color: Red; font-weight: bold;">
                                        &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                    </div>
                                </div>
                            </div>

                            <div class="box-body">

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
                                    <FadeIn AnimationTarget="btnCloseParent" MaximumOpacity=".9" />
                                </Parallel>
                            </Sequence>
                        </OnClick>
                                    </Animations>
                                </ajaxToolKit:AnimationExtender>
                                <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
                                    <Animations>
                        <OnClick>
                            <Sequence AnimationTarget="info">
                                <%--  Shrink the info panel out of view --%>
                                <StyleAction Attribute="overflow" Value="hidden"/>
                                <Parallel Duration=".3" Fps="15">
                                    <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                    <FadeOut />
                                </Parallel>
                                
                                <%--  Reset the sample so it can be played again --%>
                                <StyleAction Attribute="display" Value="none"/>
                                <StyleAction Attribute="width" Value="250px"/>
                                <StyleAction Attribute="height" Value=""/>
                                <StyleAction Attribute="fontSize" Value="12px"/>
                                <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />
                                
                                <%--  Enable the button so it can be played again --%>
                                <EnableAction AnimationTarget="btnHelp" Enabled="true" />
                            </Sequence>
                        </OnClick>
                        <OnMouseOver>
                            <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                        </OnMouseOver>
                        <OnMouseOut>
                            <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                        </OnMouseOut>
                                    </Animations>
                                </ajaxToolKit:AnimationExtender>

                                <div class="form-group col-md-3">
                                    <label><span id="Span1" runat="server" style="color: red;">*</span>Session</label>
                                    <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="true" Font-Bold="True">
                                        <asp:ListItem>Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSession"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="compare">
                                    </asp:RequiredFieldValidator>

                                </div>

                                <div class="form-group col-md-3">
                                    <label><span id="branch" runat="server" style="color: red;">*</span> College /School Name:</label>
                                    <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="teacherallot" AutoPostBack="true" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlColg"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select College /School Name" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div  runat="server" class="form-group col-md-3" >
                                    <label><span id="faculty" runat="server" style="color: red;">*</span> Degree :</label>
                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="3" ValidationGroup="teacherallot" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-3" style="display:none">
                                    <label><span style="color: red;">*</span> Branch :</label>
                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="4" ValidationGroup="teacherallot" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                  <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Department" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="form-group col-md-3">
                                    <label><span style="color: red;">*</span>Scheme :</label>
                                    <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" TabIndex="5" ValidationGroup="teacherallot" AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlScheme"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlScheme"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="compare">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-md-3">
                                    <label><span style="color: red;">*</span> Semester :</label>
                                    <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" TabIndex="6" ValidationGroup="teacherallot"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSemester"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlSemester"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="compare">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-md-3" style="display:none">
                                    <label><span style="color: red;">*</span> Subject Type:</label>
                                    <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="true" TabIndex="7" AutoPostBack="true" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged1"
                                        ValidationGroup="teacherallot">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSubjectType"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Subject Type" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>--%>
                                </div>

                                <div class="form-group col-md-3" style="display:none">
                                    <label><span style="color: red;">*</span> Internal/External :</label>
                                    <asp:DropDownList ID="ddlInEx" runat="server" AppendDataBoundItems="true" TabIndex="8" ValidationGroup="teacherallot">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">Internal</asp:ListItem>
                                        <asp:ListItem Value="2">External</asp:ListItem>
                                    </asp:DropDownList>
                                <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlInEx"
                                        Display="None" ErrorMessage="Please Select Internal/External" InitialValue="0" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="form-group col-md-3" style="display:none">
                                    <label><span style="color: red;">*</span>Course :</label>
                                    <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" TabIndex="9" ValidationGroup="teacherallot" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                              <%--      <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlCourse"
                                        Display="None" ErrorMessage="Please Select Course" InitialValue="0" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>--%>

                                </div>

                                  <div class="form-group col-md-3">
                                       <label><span style="color: red;">*</span>Exam Name :</label>
                                        <asp:DropDownList ID="ddlExamName" runat="server" class="form-group"  AppendDataBoundItems="true"  TabIndex="10" AutoPostBack="true" OnSelectedIndexChanged="ddlExamName_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">MID SEM</asp:ListItem>
                                            <asp:ListItem Value="2">END SEM</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvExamName" runat="server" ControlToValidate="ddlExamName"
                                            Display="None" ErrorMessage="Please Select Exam Name" InitialValue="0" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlExamName"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Exam Name" ValidationGroup="compare">
                                    </asp:RequiredFieldValidator>
                                  </div>

                                <div class="form-group col-md-3">
                                </div>

                                <div class="clearfix"></div>

                            </div>

                            <div class="box-footer">
                                <div class="text-center">
                                    <!--Buttons-->
                                    <asp:Button ID="btnCompare" runat="server" TabIndex="10" Text="Compare and Save" ValidationGroup="teacherallot"
                                        OnClick="btnCompare_Click" ToolTip="Comparison of operator 1 and operator 2 mismatch" CssClass="btn btn-info" />
                                    <asp:Button ID="btnUnlock" CssClass="btn btn-info" runat="server" Enabled="false" TabIndex="11" Text="Unlock Marks" ValidationGroup="teacherallot" OnClick="btnUnlock_Click" ToolTip="Unlock"  />
                                    <asp:Button ID="btnMove" CssClass="btn btn-info" runat="server" Enabled="false" Visible="false" TabIndex="12" Text="Transfer Match data" ValidationGroup="teacherallot" OnClick="btnMove_Click" ToolTip="Transfer Match data into Main" />
                                    <asp:Button ID="btnRmove" Visible="false" CssClass="btn btn-info" runat="server" TabIndex="13" Text="Remove Compare data" ValidationGroup="teacherallot" ToolTip="Remove save data" OnClick="btnRmove_Click" />
                                    <asp:Button ID="btnreport" runat="server" CssClass="btn btn-primary" TabIndex="14" Text="Mismatch Report (Excel)" ValidationGroup="teacherallot" OnClick="btnreport_Click" ToolTip="Mismatch Report" />&nbsp;&nbsp;
                                    
                                   <asp:Button ID="btnMatchReport" runat="server" CssClass="btn btn-primary" TabIndex="15" Text="Match Report (Excel)" ValidationGroup="teacherallot" OnClick="btnMatchReport_Click" ToolTip="Match Report" />&nbsp;&nbsp;
                                   <asp:Button ID="btnCompareReport" runat="server" CssClass="btn btn-primary" TabIndex="16" Text="Compare Status Report (Excel)" OnClick="btnCompareReport_Click" ValidationGroup="compare" ToolTip="Compare Status Report" />&nbsp;&nbsp;
                                   <asp:Button ID="btnClear" runat="server" CssClass="btn btn-danger" TabIndex="17" Text="Clear" Width="8%" OnClick="btnClear_Click" ToolTip="Clear" />
                                   <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="teacherallot" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                   <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="compare" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    <br />
                                    <br />
                                    <br />

                                    <asp:Label ID="lblNote" runat="server" style="margin-left:-700px" Text="NOTE : AB - ABSENT, CC- COPY CASE, WD - WITHDRAW, DR - DROP" Visible="false" Font-Bold="true" Font-Size="12px"></asp:Label>
                                    <asp:GridView runat="server" CssClass="table table-bordered table-hover table-responsive" ID="gvmismatch" AutoGenerateColumns="false">

                                        <Columns>
                                            <asp:BoundField DataField="IDNO" HeaderText="IDNO" Visible="false" />
                                            <asp:BoundField DataField="ENROLLMENTNO" HeaderText="ENROLLMENT NO" />
                                            <asp:BoundField DataField="ROLLNO" HeaderText="ROLL NO" />
                                            <asp:BoundField DataField="COURSENAME" HeaderText="COURSE NAME" />
                                            <asp:BoundField DataField="OPERATOR1" HeaderText="FACULTY / OPERATOR 1" />
                                            <asp:BoundField DataField="OP1_MARKS" HeaderText="OPERATOR 1 MARKS" />
                                            <asp:BoundField DataField="OPERATOR2" HeaderText="OPERATOR 2"  />
                                            <asp:BoundField DataField="OP2_MARKS" HeaderText="OPERATOR 2 MARKS" />
                                            <asp:BoundField DataField="OPID" HeaderText="OPID" Visible="false" />
                                        </Columns>


                                          <HeaderStyle CssClass="bg-light-blue"  HorizontalAlign="Center" />
                                    </asp:GridView>

                                      <asp:GridView runat="server" Visible="false" CssClass="table table-bordered table-hover table-responsive" ID="gvData" AutoGenerateColumns="false">

                                        <Columns>
                                            <asp:BoundField DataField="IDNO" HeaderText="IDNO" Visible="false" />
                                            <asp:BoundField DataField="ENROLLMENTNO" HeaderText="ENROLLMENT NO" />
                                            <asp:BoundField DataField="ROLLNO" HeaderText="ROLL NO" />
                                            <asp:BoundField DataField="COURSENAME" HeaderText="COURSE NAME" />
                                            <asp:BoundField DataField="OP" HeaderText="FACULTY / OPERATOR"  />
                                          
                                        </Columns>

                                          <HeaderStyle CssClass="bg-light-blue"  HorizontalAlign="Center" />
                                    </asp:GridView>

                                    <asp:Repeater ID="lvDetails" runat="server">
                                        <HeaderTemplate>
                                            <table cellpadding="0" cellspacing="0" class="display" style="width: 100%;">
                                                <thead>
                                                    <tr class="header" style="background-color: black">
                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                    <thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody></table>
                                        </FooterTemplate>
                                    </asp:Repeater>


                                </div>
                            </div>

                            <div class="col-sm-12 form-group">
                                <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                                    <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                        ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                                    <p class="page_help_head">
                                        <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                                        <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                                        Edit Record&nbsp;&nbsp;
                                        <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                                        Delete Record
                                    </p>
                                    <p class="page_help_text">
                                        <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>




                <%--STUDENT LIST--%>
            </ContentTemplate>
            <Triggers>

                <asp:PostBackTrigger ControlID="btnreport" />
                <asp:PostBackTrigger ControlID="btnMatchReport" />

                <asp:PostBackTrigger ControlID="btnCompare" />
                <asp:PostBackTrigger ControlID="btnUnlock" />
                
                <asp:PostBackTrigger ControlID="btnCompareReport" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

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

    <script type="text/javascript" language="javascript">
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
    </script>

</asp:Content>
