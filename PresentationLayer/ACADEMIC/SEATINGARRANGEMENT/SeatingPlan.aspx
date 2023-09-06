<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="SeatingPlan.aspx.cs" Inherits="ACADEMIC_NewSeatingPlan" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../../INCLUDES/prototype.js" type="text/javascript"></script>

    <script src="../../INCLUDES/scriptaculous.js" type="text/javascript"></script>

    <script src="../../INCLUDES/modalbox.js" type="text/javascript"></script>


    <div style="z-index: 1; position: absolute; top: 60px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updplRoom"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 212px; padding-left: 5px; height: 18px;">
                    <img src="../../IMAGES/ajax-loader.gif" alt="Loading" />
                    Please Wait..
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updplRoom" runat="server">
        <ContentTemplate>



            <div id="Div2" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
            </div>
            <!-- Info panel to be displayed as a flyout when the button is clicked -->
            <div id="Div3" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                <div id="Div4" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return false;" Text="X"
                        ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                </div>
                <div>
                    <p class="page_help_head">
                        <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                        Edit Record
                    </p>
                    <p class="page_help_text">
                        <asp:Label ID="Label1" runat="server" Font-Names="Trebuchet MS" />
                    </p>
                </div>
            </div>

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

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">SEATING ARRANGEMENT </h3>
                            <asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>
                        </div>


                        <div class="box-body">
                            <div class="col-md-12">
                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span> Session  :</label>

                                <asp:DropDownList ID="ddlSession" runat="server" Width="85%" TabIndex="1" AppendDataBoundItems="True"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span>   College :  </label>
                                <asp:DropDownList ID="ddlcollege" runat="server" Width="85%" TabIndex="1" AppendDataBoundItems="True"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlcollege_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlcollege"
                                    Display="None" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span>  Degree :</label>
                                <asp:DropDownList ID="ddldegree" runat="server" Width="85%" TabIndex="1" AppendDataBoundItems="True"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddldegree_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddldegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>
                            </div>


                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span>  Block Name :</label>
                                <asp:DropDownList ID="ddlRoom" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlRoom_SelectedIndexChanged" TabIndex="2" Width="85%">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvRoom" runat="server" ControlToValidate="ddlRoom"
                                    Display="None" ErrorMessage="Please Select Room" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>
                            </div>


                            <div class="form-group col-md-4">
                                <label>Arrangement  :</label>
                                <asp:DropDownList ID="ddlArrangement" runat="server" AppendDataBoundItems="True"
                                    AutoPostBack="True" TabIndex="7" Width="85%">

                                    <asp:ListItem Value="1">Vertical</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                               <div class="form-group col-md-1">
                                   </div>
                            <br />&nbsp;&nbsp;
                            <div class="form-group col-md-2" id="trRoomCapacity" runat="server" visible="false">

                                <label>&nbsp;  Actual Capacity :</label>
                                <br />

                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblActualCapacity" runat="server"></asp:Label>

                                <asp:Label ID="lbl1" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lbl2" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lbl3" runat="server" Visible="false"></asp:Label>
                            </div>


                            </div>
                          
                            
                            
                            <div visible="false" runat="server" id="divbranch1">
                               
                                <div class="col-md-12">

                                    <div class="col-md-3">

                                        <label><span style="color: red;">*</span>Branch 1 :</label>


                                        <asp:DropDownList ID="ddlBranch1" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlBranch1_SelectedIndexChanged" TabIndex="3" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-3">
                                        <label><span style="color: red;">*</span>
                                            Semester 1 :</label>
                                        <asp:DropDownList ID="ddlSemester1" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            TabIndex="1" CssClass="form-control" OnSelectedIndexChanged="ddlSemester1_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                      <div class="form-group col-md-3">
                                        <label><span style="color: red;">*</span>
                                            Slot</label>


                                        <asp:TextBox ID="TXTFORSLOT1" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>

                                    </div>
                                     <div class="form-group col-md-2" >
                                         </div>
                                    <div class="form-group col-md-2" style="margin-left:15px">
                                        <label>Add Branch :</label>
                                        <br />

                                        &nbsp;&nbsp;<asp:CheckBox ID="chkAdd1" runat="server" Enabled="False"
                                            AutoPostBack="True" OnCheckedChanged="chkAdd1_CheckedChanged" />
                                    </div>
                                   
                                    </div>
                                    <div class="col-md-12">
                                    <div class="form-group col-md-3">
                                        <label>
                                            Total Students:</label>
                                        <asp:TextBox ID="txttotstudb1" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>

                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>
                                            Alloted Students:</label>

                                        <asp:TextBox ID="txtallotstud1" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>

                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>
                                            Remain Students:</label>


                                        <asp:TextBox ID="txtremainstud1" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>

                                    </div>
                                   


                                        </div>
                                      
                                </div>

                        





   


                            
                           
                            <%--       aaushi--%>



                            <div visible="false" runat="server" id="divbranch2" >

                                <div class="col-md-12">


                                    <div class="form-group col-md-3">



                                        <label>Branch 2 :</label>


                                        <asp:DropDownList ID="ddlBranch2" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlBranch2_SelectedIndexChanged" TabIndex="4" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                   <div class="form-group col-md-3">

                                        <label>
                                            Semester 2:</label>
                                        <asp:DropDownList ID="ddlSemester2" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            TabIndex="1" CssClass="form-control" OnSelectedIndexChanged="dlSemester2_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                  
                                     <div class="form-group col-md-3">
                                    <label>
                                        Slot</label>
                                  
                                        <asp:TextBox ID="TXTFORSLOT2" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>

                                   



                                     <div class="form-group col-md-3">
                                        <label>Add Branch :</label>



                                        <asp:CheckBox ID="chkAdd2" runat="server" Enabled="False"
                                            AutoPostBack="True" OnCheckedChanged="chkAdd2_CheckedChanged" />
                                    </div>
                                    </div>
                                <div class="form-group col-md-12">
                                     <div class="form-group col-md-3">
                                        <label>
                                            Total Students:</label>
                                        <asp:TextBox ID="txttotstudb2" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                     <div class="form-group col-md-3">
                                        <label>
                                            Allot Students</label>
                                        <asp:TextBox ID="txtallotstud2" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>

                                    </div>

                                     <div class="form-group col-md-3">
                                        <label>
                                            Remain</label>
                                        <asp:TextBox ID="txtremainstud2" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>

                                    </div>
                                     
                                   

                                </div>
                            </div>



                            <div visible="false" runat="server" id="divbranch3">

                                <div class="col-md-12">


                                    <div class="form-group col-md-3">


                                        <label>Branch3 :</label>


                                        <asp:DropDownList ID="ddlBranch3" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlRoom_SelectedIndexChanged" TabIndex="5" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>


                                    <div class=" col-md-3">
                                        <label>Semester 3</label>


                                        <asp:DropDownList ID="ddlSemester3" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            TabIndex="6" CssClass="form-control" OnSelectedIndexChanged="dlSemester3_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>

                                    <div class="  col-md-3">
                                        <label>
                                            Total Students:</label>
                                        <asp:TextBox ID="txttotstudb3" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                                  
                                       <div class="form-group col-md-12">
                                             <div class="  col-md-3">
                                        <label>
                                            Allot Students</label>
                                        <asp:TextBox ID="txtallotstud3" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>

                                    </div>
                                    <div class="  col-md-3">
                                        <label>
                                            Remain</label>
                                        <asp:TextBox ID="txtremainstud3" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>

                                    </div>

                                    <div class=" form-group col-md-3">
                                        <label>
                                            Slot</label>
                                        <asp:TextBox ID="TXTFORSLOT3" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    </div>
                                    </div>

                                </div>
                            </div>



                       




                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnConfigure" runat="server" Text="Configure" Width="90px" ValidationGroup="configure"
                                    TabIndex="8" CssClass="btn btn-primary" OnClick="btnConfigure_Click" />
                                <asp:Button ID="btnStastical" runat="server" Text="Statistical Report" ValidationGroup="process"
                                    Width="15%" OnClick="btnStastical_Click" TabIndex="15" Visible="false" />
                                <asp:Button ID="btnMasterSeating" runat="server" CssClass="btn btn-primary" Text="Master Seating Plan" ValidationGroup="process"
                                    Width="15%" OnClick="btnMasterSeating_Click" TabIndex="15" />


                                <asp:Button ID="btnRoomwise" Width="150px" CssClass="btn btn-primary" runat="server" Text="Block Arrangement"
                                    OnClick="btnRoomwise_Click" />

                                <asp:Button ID="studAttenreport" runat="server" Text="Student Attendence" CssClass="btn btn-primary" ValidationGroup="process"
                                    OnClick="studAttenreport_Click" />
                                <asp:Button ID="btnconsolidated" Width="150px" CssClass="btn btn-primary" runat="server" Text="Consolidated Report" ValidationGroup="process"
                                    OnClick="btnConsolidateSeating_Click" Visible="false"/>
                                <asp:Button ID="btnClear" runat="server" Width="100px" CssClass="btn btn-warning" OnClick="btnCancel_Click"
                                    Text="Cancel" />

                                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="configure" />


                                <p>
                                    &nbsp;<asp:ValidationSummary ID="vsRoomNm" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />
                                    <p>
                                        <asp:Button ID="btnRoomSeatsEx" runat="server" OnClick="btnRoomSeatsEx_Click" TabIndex="16" Text="Export Room Seat Report" ValidationGroup="process" Visible="false" Width="17%" />
                                    </p>
                                    <div class="col-md-12">
                                        <asp:Panel ID="pnldetails" runat="server" Height="400px" ScrollBars="Auto" Visible="false">
                                            <asp:ListView ID="lvdetails" runat="server">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <h4>STUDENTS LIST </h4>
                                                        <table class="table table-hover table-bordered table-responsive">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Sr No. </th>
                                                                    <th>Enrollment No </th>
                                                                    <th>Student Name </th>
                                                                    <th>Branch </th>
                                                                    <th>Bench No </th>
                                                                    <th>Block Name </th>
                                                                    <th>Exam Date </th>
                                                                    <th>Course Code </th>
                                                                 
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <%--  <tr>
                            <td>
                                <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/IMAGES/delete.gif" CommandArgument='<%# Eval("EXDTNO") %>'
                                    AlternateText="Delete Record" OnClientClick="return ConfirmToDelete(this);" OnClick="btnDel_Click"
                                    TabIndex="6" ToolTip='DELETE' />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                    CommandArgument='<%# Eval("EXDTNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                    OnClick="btnEdit_Click" TabIndex="7" />--%><%-- </td>--%>
                                                    <td><%#Container.DataItemIndex+1 %></td>
                                                    <td><%# Eval("REGNO")%></td>
                                                    <td><%# Eval("STUDNAME")%></td>
                                                    <td><%# Eval("SHORTNAME")%></td>
                                                    <td><%# Eval("SEATNO")%></td>
                                                    <td><%# Eval("ROOMNAME")%></td>
                                                    <td><%# Eval("EXAMDATE")%></td>
                                                        <td><%# Eval("CCODE")%></td>
                                                
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                </p>


                            </p>


                        </div>
                    </div>
                </div>
            </div>
             </div>

            </div>




            <tr>
                <td align="center">&nbsp;
                </td>
                <td align="left">&nbsp;
                                       
                                       
                                        
                                       
                                        &nbsp;  
                                         &nbsp;
                                        <asp:Button ID="btnConsolidateSeating" runat="server" CssClass="btn btn-primary" Text="Consolidate Seating Plan" ValidationGroup="process"
                                            Width="20%" Visible="false" TabIndex="15" />
                    <asp:Button ID="btnRoomSeats" runat="server" Text="Room Seat Report" ValidationGroup="process"
                        Width="15%" OnClick="btnRoomSeats_Click" TabIndex="16" Visible="false" />
        </ContentTemplate>
        <%-- <Triggers>
            <asp:PostBackTrigger ControlID="ddlRoom" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

</asp:Content>
