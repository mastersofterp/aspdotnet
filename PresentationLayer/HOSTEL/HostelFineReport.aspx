<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="HostelFineReport.aspx.cs" Inherits="HOSTEL_REPORT_HostelFineReport"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/javascript" language="javascript" src="../Javascripts/FeeCollection.js"></script>

    <script type="text/javascript" language="javascript" src="../includes/prototype.js"></script>

    <script type="text/javascript" language="javascript" src="../includes/scriptaculous.js"></script>

    <script type="text/javascript" language="javascript" src="../includes/modalbox.js"></script>

     <div class="row">
           <div class="col-md-12">
                  <div class="box box-primary">
                      <div class="box-header with-border">
                                        <h3 class="box-title">Hostel Fine Report</h3>
                                        <div class="box-tools pull-right"></div>
                                    </div>

                                    <div style="color: Red; font-weight: bold">
                                        &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                        <asp:Label ID="Label1" runat="server" Font-Names="Trebuchet MS" />
                                    </div>
                            
                                <asp:Panel ID="pnlSelect" runat="server" Style="padding-left: 10px;" Width="100%" >

                                        <fieldset class="fieldsetPay box-header">
                                <div  style="justify-content:center" id="divStudentSearch" runat="server">
                                    
                       
                                   
                        <div class="box-body row" style="justify-content:center" id="divStudentFine" runat="server">
                          

                          
                            <div class="form-group col-md-4">
                                <div>
                                <label><span style="color: Red;">*</span> Session : </label>
                                <asp:DropDownList ID="ddlHostelSessionNo" runat="server"  AppendDataBoundItems="True"
                                    TabIndex="1" data-select2-enable="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvHostelSessionNo" runat="server" ErrorMessage="Please Select Hostel Session"
                                    ControlToValidate="ddlHostelSessionNo" Display="None" InitialValue="0" ValidationGroup="Submit" />  
                                </div>
                            </div>
                       
                            <div class="form-group col-md-4">
                                <div style="width: 100%; float: left; " class="vista-grid">
                               
                                    
                                  <label><span style="color: Red;">*</span> HOSTEL : </label>
                                   <asp:DropDownList ID="ddlHostelNo" runat="server"
                                    AppendDataBoundItems="True" AutoPostBack="True" onselectedindexchanged="ddlHostelNo_SelectedIndexChanged" TabIndex="2" data-select2-enable="true">
                                   
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvHostelNo" runat="server" ErrorMessage="Please Select Hostel Name"
                                    ControlToValidate="ddlHostelNo" Display="None" InitialValue="0" ValidationGroup="Submit" />
                                    </div>
                                
                                </div>
                              
                                <br />
                                
                                
         
                            </div>
                                        

                        </div>
                                    <div class="box-footer" align="center">
                                    <asp:Button ID="btnShow" runat="server" Text="Show" Width="88px"
                                     ToolTip="Show Fine Student"  CssClass="btn btn-primary"
                                    TabIndex="3" ValidationGroup="Submit" onclick="btnShow_Click" />
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="88px"
                                     ToolTip="Submit Fine Student"  CssClass="btn btn-primary"
                                    TabIndex="4" ValidationGroup="Submit" onclick="btnSubmit_Click" />
                                    <asp:Button ID="btnReport" runat="server" Text="Report" Width="88px"
                                     ToolTip="Report Fine Student" CssClass="btn btn-info"
                                    TabIndex="5" ValidationGroup="Submit" onclick="btnReport_Click" />
                                &nbsp;<asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancel"
                                    Width="88px"  TabIndex="6" onclick="btnCancel_Click" CssClass="btn btn-warning"/>
                            </div>
                 
                        </fieldset>
                            
                      </asp:Panel>
                      <asp:Panel ID="PnlLv" runat="server" Style="padding-left: 10px;" Width="100% " ScrollBars="Both">
                      <div align="center" width="95%">

                          <asp:ListView ID="lvDetails" runat="server" 
                        onitemdatabound="lvDetails_ItemDataBound" class="vista-grid" >
                        <LayoutTemplate>
                            <div id="demo-grid" class="vista-grid">
                                <div class="titlebar">
                                    <b>Student List</b></div>
                                <table class="table table-striped table-bordered nowrap display" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="bg-light-blue"  style="flex-align: center">
                                        <th>
                                            Edit
                                        <th >
                                            Roll No
                                        </th>
                                        <th >
                                            Student Name
                                        </th>
                                        <th >
                                            Degree
                                        </th>
                                        <th >
                                            Branch
                                        </th>
                                        <th >
                                            Semester
                                        </th>
                                        <th >
                                            Room 
                                        </th>
                                        <th >
                                            Fine
                                        </th>
                                        <th >
                                            Remark
                                        </th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server" />
                                </table>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                <td style="width: 5%;">       
                                   <asp:ImageButton ID="btnEditFine" runat="server" OnClick="btnEditFine_Click"
                                    CommandArgument='<%# Eval("IDNO") %>' ImageUrl="~/Images/edit.png" ToolTip='<%# Eval("IDNO") %>' />                                     
                                </td>
                                <td >
                                    <%# Eval("IDNO") %> 
                                       <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                </td>
                                <td >
                                    <%# Eval("STUDNAME") %>
                                </td>
                                 <td >
                                    <%# Eval("DEGREE") %>
                                </td>
                                  <td >
                                    <%# Eval("BRANCH") %>
                                </td>
                                  <td >
                                    <%# Eval("SEMESTERNAME")%>
                                </td>
                                <td >
                                    <%# Eval("ROOM_NAME") %>
                                </td>
                                <td >
                                  <asp:TextBox Id="txtFine" runat="server"  MaxLength="8" Width="70px"></asp:TextBox> 
                                  <ajaxToolKit:FilteredTextBoxExtender     
                                ID="ftbd" runat="server" TargetControlID="txtFine" ValidChars="1234567890." >
                                </ajaxToolKit:FilteredTextBoxExtender >
                                </td>
                                <td >
                                   <asp:TextBox ID="txtRemark" runat="server" ></asp:TextBox>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                      </div>
                          </asp:Panel>
                      </div>
               </div>
         </div>
<div id="divMsg" runat="server">
    </div>


</asp:Content>

