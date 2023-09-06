<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MIDSemResult.aspx.cs" Inherits="ACADEMIC_EXAMINATION_MIDSemResult" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updUpdate"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <div runat="server" id="divDetails">

      <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title"><b>MID SEM EXAM MARKSHEET</b></h3>
                       <div class="pull-right">
                                <div style="color: Red; font-weight: bold;">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are Mandatory
                                </div>
                            </div>
                    </div>
                    <asp:UpdatePanel ID="updUpdate" runat="server">
                        <ContentTemplate>

                            <div class="box-body">
                                <div class="col-md-12">
                                    <div class="form-group col-md-4">
                                        <label><span style="color: red">*</span>Session</label>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>
                                  </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblNote" runat="server" style="margin-left:25px" Text="No Published Result Found !!!!" Visible="false" ForeColor="Red" Font-Bold="true" Font-Size="Large"></asp:Label>
                                </div>
                            </div>
                            <div class="box-footer"> 
                                </p>
                                <div class="col-md-12">
                                    <p class="text-center">
                                      
                                         <asp:Button ID="btnPrint" Visible="false" runat="server" OnClick="btnPrint_Click"
                                            Text="Print MarkSheet" CssClass="btn btn-info" />
                                    </p>
                                    <p>
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="report" />
                                    </p>

                                      <div class="col-md-12">

                                          <div id="divStudDetails" runat="server" visible="false" >

                                              <div class="col-md-12" style="color:black;margin-top:15px">
                                                  <div class="col-md-6">
                                                      <label>Name :</label>
                                                      <asp:Label ID="lblName" runat="server" Font-Bold="true"></asp:Label>
                                                  </div>

                                                  <div class="col-md-6">
                                                      <label>Reg. No. :</label>
                                                      <asp:Label ID="lblRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                  </div>

                                                  <div class="col-md-6">
                                                      <label>Session :</label>
                                                      <asp:Label ID="lblSession" runat="server" Font-Bold="true"></asp:Label>
                                                  </div>

                                                   <div class="col-md-6">
                                                      <label>SGPA :</label>
                                                      <asp:Label ID="lblSGPA" runat="server" Font-Bold="true"></asp:Label>
                                                  </div>
                                              </div>
                                          </div>

                                       <div class="col-md-12">
                                         <asp:Panel ID="pnlStudent" runat="server" ScrollBars="Auto" style="margin-top:25px">
                                            <asp:ListView ID="lvStudent" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%# Eval("COURSE")%>
                                                        </td>

                                                        <td style="text-align:center">
                                                            <%# Eval("SEMESTERNAME")%>
                                                        </td>
                                                         <td style="text-align:center">
                                                            <%# Eval("S1MARK")%>
                                                        </td>
                                                         <td style="text-align:center">
                                                            <%# Eval("S2MARK")%>
                                                        </td>
                                                         <td style="text-align:center">
                                                            <%# Eval("S3MARK")%>
                                                        </td>
                                                          <td style="text-align:center">
                                                            <%# Eval("S3MARK_SCALEDOWN")%>
                                                        </td>
                                                         <td style="text-align:center">
                                                            <%# Eval("EXTERMARK")%>
                                                        </td>
                                                         <td style="text-align:center">
                                                            <%# Eval("S6MARK")%>
                                                        </td>
                                                         <td style="text-align:center">
                                                            <%# Eval("S7MARK")%>
                                                        </td>
                                                         <td style="text-align:center">
                                                            <%# Eval("MARKTOT")%>
                                                        </td>
                                                        <td style="text-align:center">
                                                            <%# Eval("GRADE")%> 
                                                        </td>
                                                    </tr>
                                                   
                                                </ItemTemplate>
                                                <LayoutTemplate>
                                                    <div id="listViewGrid">
                                                        <%--<h4>Course List</h4>--%>
                                                        <table class="table table-hover table-bordered table-responsive">
                                                            <thead>
                                                                <tr class="bg-light-blue">

                                                                    <th style="text-align:center">Subject Name
                                                                    </th>

                                                                    <th style="text-align:center">Semester
                                                                    </th>
                                                                    <th style="text-align:center">Assignment
                                                                    </th>
                                                                      <th style="text-align:center">Attendance
                                                                    </th>
                                                                      <th style="text-align:center">MID SEM
                                                                    </th>
                                                                      <th style="text-align:center">MID SEM Scaledown
                                                                    </th>
                                                                     <th style="text-align:center">END SEM
                                                                    </th>
                                                                      <th style="text-align:center">Internal Assessment
                                                                    </th>
                                                                      <th style="text-align:center">External Assessment
                                                                    </th> 
                                                                     <th style="text-align:center">Total Marks
                                                                    </th>  
                                                                     <th style="text-align:center">Grade
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                           
                                                    </div>
                                                </LayoutTemplate>
                                            </asp:ListView>
                                             
                                        </asp:Panel>
                                       </div>
                                          
                                    </div>
                                     

                                </div>
                                
                            </div>
                              <span id="spanNote" runat="server" visible="false" style="font-size:11px"> <b> NOTE : AB - ABSENT, CC - COPYCASE, WR - WITHDRAW, DR - DROP, * - GRACE</b></span>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                </div>
              
            </div>
           
        </div>
    
    </div>

    <div id="divMsg" runat="server">
    </div>
   
</asp:Content>

