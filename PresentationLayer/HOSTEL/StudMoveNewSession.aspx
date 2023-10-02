<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudMoveNewSession.aspx.cs" Inherits="HOSTEL_StudMoveNewSession" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
             <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                     <div class="box-header with-border">
                       <h3 class="box-title">STUDENT PROMOTE TO NEW SESSION</h3>
                     </div>

                <div class="box-body">

                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Old Session </label>
                                </div>
                               <asp:DropDownList ID="ddlCurSession" runat="server" AppendDataBoundItems="True" CssClass="form-control" TabIndex="1">
                         </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCurSession"
                          ErrorMessage="Please Select Current Session" Display="None" ValidationGroup="show" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel Name </label>
                                </div>
                                <asp:DropDownList ID="ddlHostel" runat="server" AppendDataBoundItems="True" CssClass="form-control" TabIndex="2">
                           </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvHostelNo" runat="server" ControlToValidate="ddlHostel"
                            ErrorMessage="Please Select Hostel Name" Display="None" ValidationGroup="show" SetFocusOnError="true" InitialValue="0">
                        </asp:RequiredFieldValidator>
                            </div>

                           <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                 <div class="label-dynamic">
                                    <label>Block Name</label>
                                </div>
                                <asp:DropDownList ID="ddlBlock" runat="server" TabIndex="1" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                     ToolTip="Block Name" AutoPostBack="True"  InitialValue="0" />
                                
                             </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                 <div class="label-dynamic">
                                    <label>Semester</label>
                                </div>
                                <asp:DropDownList ID="ddSemester" runat="server" TabIndex="1" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                     ToolTip="Semester" AutoPostBack="True"  InitialValue="0" />
                                
                             </div>--%>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Promote To Session </label>
                                </div>
                                <asp:DropDownList ID="ddlNewSession" runat="server" AppendDataBoundItems="True" Enabled="false" TabIndex="3" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlNewSession_SelectedIndexChanged">
                     </asp:DropDownList>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlNewSession"
                        ErrorMessage="Please Select Pormote New Session" Display="None" SetFocusOnError="true" InitialValue="0" ValidationGroup="submit"> </asp:RequiredFieldValidator>
                             
                            </div>

                             <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>From Date </label>
                                </div>
                                <div class="input-group date">
                            <div class="input-group-addon">
                                <i id="I1" runat="server" class="fa fa-calendar"></i>
                            </div>
                            <asp:TextBox ID="txtFromdate" runat="server" Enabled="false" ValidationGroup="submit" CssClass="form-control" TabIndex="4"/>
                            
                            <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromdate" PopupButtonID="imgFromDate" />
                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromdate"
                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />

                        </div>
                             
                            </div>

                             <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>To Date </label>
                                </div>
                               <div class="input-group date">
                            <div class="input-group-addon">
                                <i id="I2" runat="server" class="fa fa-calendar"></i>
                            </div>
                            <asp:TextBox ID="txtTodate" runat="server" Enabled="false" CssClass="form-control" TabIndex="5"/>

                            <ajaxToolKit:CalendarExtender ID="ceTodate" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtTodate" PopupButtonID="imgTodate" />
                            <ajaxToolKit:MaskedEditExtender ID="meeTodate" runat="server" TargetControlID="txtTodate"
                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                OnInvalidCssClass="errordate" />

                        </div>
                    </div>
                         </div>
                             
                            </div>
                     <div class="col-12 btn-footer">
                        <asp:Button ID="btnShow" runat="server" Text="Show" TabIndex="6" ValidationGroup="show" CssClass="btn btn-primary" OnClick="btnShow_Click"/>
                      <asp:Button ID="btnSubmit" runat="server" ValidationGroup="submit" TabIndex="7" CssClass="btn btn-primary" Text="Submit"  Enabled="false" OnClick="btnSubmit_Click" />
                      <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="8" OnClick="btnCancel_Click" />
                      <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" DisplayMode="List" />
                      <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="show" DisplayMode="List" />
                    </div>
 
                      <div class="col-12">
                          <div class="sub-heading">
                          <h5> Student List Promotion to New Session </h5>
                          </div> 
                         <asp:ListView ID="lvStudents" runat="server">
                            <LayoutTemplate>
                                <div id="demo-grid">
                                      
                                   <table id="tblSearch" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead><tr class="bg-light-blue">
                                         
                                      <th>
                                          <asp:CheckBox ID="cbHead" runat="server" TabIndex="9" onclick="totAllSubjects(this)" />
                                      </th>
                                      <th>Registration No.</th>
                                      <th>Student Name</th>
                                      <th>Degree</th>
                                      <th>BRANCH</th>
                                      <th>Semester</th>
                                      <th>College</th>
                                      <th>Room</th>
                                      <th>Status</th>
                                      </tr> </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                        
                                    </table>
                                </div>
                              </LayoutTemplate>
                            <ItemTemplate>
                                  <%--<tr>--%>
                                   <tr class="item" >
                                      <td>
                                          <asp:CheckBox ID="chkhostel" runat="server" TabIndex="10" Font-Bold="true" />
                                   
                                          <asp:HiddenField ID="hdfIdno" runat="server" Value='<%# Eval("IDNO")%>' />
                                         
                                         <asp:HiddenField ID="hdfhost" runat="server" Value='<%# Eval("PROMOTE")%>' />
                                      </td>
                                       <td><asp:Label ID="lblREGNO" runat="server" Text='<%# Eval("REGNO")%>' ></asp:Label> </td>
                                      
                                      <td><asp:Label ID="Label1" runat="server" Text='<%# Eval("STUDNAME")%>' ></asp:Label></td>
                                     <td><%# Eval("DEGREENAME")%></td>
                                     <td><%# Eval("BRANCH")%></td>
                                     <td><%# Eval("SEMESTER")%></td>
                                     <td><%# Eval("COLLEGE_NAME")%></td>

                                      <td><%# Eval("ROOM_NAME")%></td>

                                      <%-- <td><asp:Label ID="Label2" runat="server" Text='<%# Eval("ALLOTMENT_DATE")%>' Width="70px"></asp:Label></td>
                                      
                                
                                     <td><asp:Label ID="lblEDate" runat="server" Text='<%# Eval("TODATE")%>' ></asp:Label> </td>--%>
                                    <td>
                                      <asp:Label ID="lblStatus" runat="server" Font-Bold="true" />
                                     </td>
                                    </tr>
                                  </ItemTemplate>
                            
                         </asp:ListView>

                       <%-- remove <AlternatingItemTemplate> on 02/02/2023 by Saurabh L -----%>
                    </div>       

                        </div>
                    </div>

                   
                       </div>                    
                     </div>
                    <%--</div>
                 </div>--%>
     </ContentTemplate>

        </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">

        function totAllSubjects(headchk) {

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        e.checked = true;
                        headchk.checked == true;
                        document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_cbHead').checked = true;
                    }
                    else {
                        e.checked = false;
                        headchk.checked == false;
                        document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_cbHead').checked = false;

                    }
                }
            }
        }
    </script>
</asp:Content>

