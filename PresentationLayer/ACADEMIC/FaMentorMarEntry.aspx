<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="FaMentorMarEntry.aspx.cs" Inherits="ACADEMIC_FaMentorMarEntry" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="updActivity" DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>


    <asp:UpdatePanel ID="updActivity" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>FA/MENTOR MAR ENTRY</b> </h3>
                            <asp:HiddenField ID="hidSumAquirPoint" runat="server" />



                            <div class="pull-right">
                                <div style="color: Red; font-weight: bold;">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>
                            </div>
                        </div>


                        <div class="box-body">



                            <div class="form-group col-md-3 ">
                                <%-- <span style="color: red;">*</span><b>Academic Year<b /> </b>--%>

                                <label><span style="color: red;">*</span>Academic Year</label>

                                <asp:DropDownList ID="ddlAcdYear" runat="server" TabIndex="1" OnSelectedIndexChanged="ddlAcdYear_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true"
                                    CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <%--  <asp:ListItem  Value="1">V-1.0</asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvacdyear" runat="server" ControlToValidate="ddlAcdYear"
                                    InitialValue="0" ValidationGroup="submit" SetFocusOnError="true"
                                    Display="None" ErrorMessage="Please Select Academic Year"></asp:RequiredFieldValidator>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlAcdYear"
                                    InitialValue="0" ValidationGroup="report" SetFocusOnError="true"
                                    Display="None" ErrorMessage="Please Select Academic Year"></asp:RequiredFieldValidator>


                            </div>



                            <div class="form-group col-md-3 ">
                                <%--  <span style="color: red;">*</span><b><b /> </b>--%>
                                <label><span style="color: red;">*</span>School/Institute Name</label>
                                <asp:DropDownList ID="ddlInstitute" runat="server" OnSelectedIndexChanged="ddlInstitute_SelectedIndexChanged" TabIndex="2" AppendDataBoundItems="true" AutoPostBack="true"
                                    CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <%--   <asp:ListItem  Value="1"></asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlInstitute"
                                    InitialValue="0" ValidationGroup="submit" SetFocusOnError="true"
                                    Display="None" ErrorMessage="Please Select School/Institute Name"></asp:RequiredFieldValidator>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlInstitute"
                                    InitialValue="0" ValidationGroup="report" SetFocusOnError="true"
                                    Display="None" ErrorMessage="Please Select School/Institute Name"></asp:RequiredFieldValidator>

                            </div>

                            <div class=" form-group col-md-3 ">
                                <label><span style="color: red;">*</span>Degree</label>
                                <%--<span style="color: red;">*</span><b><b /> </b>--%>

                                <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="3" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true"
                                    CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <%--   <asp:ListItem  Value="1"></asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDegree"
                                    InitialValue="0" ValidationGroup="submit" SetFocusOnError="true"
                                    Display="None" ErrorMessage="Please Select Degree"></asp:RequiredFieldValidator>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlDegree"
                                    InitialValue="0" ValidationGroup="report" SetFocusOnError="true"
                                    Display="None" ErrorMessage="Please Select Degree"></asp:RequiredFieldValidator>

                            </div>




                            <div class=" form-group col-md-3 ">
                                <label><span style="color: red;">*</span>Programme/Branch</label>
                                <%--<span style="color: red;">*</span><b><b /> </b>--%>

                                <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="4" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true"
                                    CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <%--   <asp:ListItem  Value="1"></asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBranch"
                                    InitialValue="0" ValidationGroup="submit" SetFocusOnError="true"
                                    Display="None" ErrorMessage="Please Select Programme/Branch"></asp:RequiredFieldValidator>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlBranch"
                                    InitialValue="0" ValidationGroup="report" SetFocusOnError="true"
                                    Display="None" ErrorMessage="Please Select Programme/Branch"></asp:RequiredFieldValidator>

                            </div>


                            <div class=" form-group col-md-4 ">

                                <%--   <label><span style="color: red;">*</span>Activity</label>--%>
                                <asp:DropDownList ID="ddlactvity" Visible="false" runat="server" Width="1090px" TabIndex="5" OnSelectedIndexChanged="ddlactvity_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true"
                                    CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>

                                </asp:DropDownList>
                                <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlactvity"
                                    InitialValue="0" ValidationGroup="submit" SetFocusOnError="true"
                                    Display="None" ErrorMessage="Please Select Activity"></asp:RequiredFieldValidator>--%>
                            </div>


                            <div class="row">
                                <center>
                                        <div class="col-md-4 col-md-offset-4" style="margin-top: 25px">
                                        <asp:Button ID="btnShow" runat="server" Text="Show" ToolTip="Show"   ValidationGroup="submit"
                                            TabIndex="6" CssClass="btn btn-primary" OnClick="btnShow_Click" />

                                            
                                               <asp:Button ID="btnReport" runat="server" Text="MAR Report" Visible="false" ToolTip="MAR Report" OnClick="btnReport_Click"  ValidationGroup="report"
                                            TabIndex="7"  CssClass="btn btn-info" />

                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"  ToolTip="Cancel" CausesValidation="false"
                                            TabIndex="8"  CssClass="btn btn-danger" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                               <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="report"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                               </div>
                                    </center>
                            </div>
                            <%--  <asp:Panel ID="pnlStudent" runat="server" Visible="false">
                                <div class="col-md-12">
                                    <table id="example" class="table table-striped table-bordered" style="width: 100%">
                                        <asp:ListView ID="lvStudent" runat="server">
                                            <LayoutTemplate>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center">Sr. No.</th>
                                                        <th style="text-align: center">Student Name </th>
                                                        <th style="text-align: center">Reg. No. </th>
                                                        <th style="text-align: center">Current ACD Points</th>
                                                        <th style="text-align: center">Current PER Points</th>
                                                        <th style="text-align: center">Current Status</th>
                                                        <th style="text-align: center">Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center">
                                                        <%# Container.DataItemIndex + 1%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("STUDNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REGNO")%>
                                                    </td>
                                                    <td style="text-align: center">

                                                        <%# Eval("CURRENT_ACQ_POINTS")%>

                                                    </td>
                                                    <td>
                                                        <%# Eval("CURRENT_PER_POINTS")%>

                                                    </td>
                                                    <td>
                                                        <%# Eval("CURRENT_STATUS")%>
                                                    </td>

                                                    <td style="text-align: center">
                                                        <asp:Button ID="btnMar" runat="server" CssClass="btn btn-success" Text="Enter MAR" OnClick="btnMar_Click" CommandArgument='<%# Eval("IDNO") %>' />


                                                    </td>

                                                </tr>
                                            </ItemTemplate>

                                        </asp:ListView>
                                    </table>





                                </div>

                            </asp:Panel>--%>

                            <%--  <asp:Panel ID="pnlActivity" runat="server" Visible="false" >

                                <div class="col-md-12">
                                <table id="Table2" class="table table-striped table-bordered" style="width:100%">
                                    <asp:ListView ID="lvActivity" runat="server">
                                     <LayoutTemplate>
                                            <thead class="bg-light-blue">
                                                <tr >
                                                    
                                               
                                                      <th style="width:65px">Sr. No.</th>
                                              
                                                    <th style="text-align:center">Acitivity Name</th>
                                                     <th style="text-align:center">Points </th>
                                                </tr>
                                            </thead>
                                        <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>     
                                       </LayoutTemplate>
                                       
                                        <ItemTemplate>
                                                <tr>
                                                
                                                   <td style="text-align:center" >
                                                       <%# Container.DataItemIndex + 1%>
                                                    </td>
                                          

                                                    <td>
                                                       <asp:LinkButton ID="lnkBtn" runat="server" CommandArgument='<%# Eval("ACTIVITYNO") %>' OnClick="lnkBtn_Click" Text='<%# Eval("ACTIVITY_NAME") %>' ></asp:LinkButton>
                                                        
                                                        
                                                        
                                            
                                                    </td>
                                                      <td style="text-align:center">
                                                        <%# Eval("POINTS")%>
                                                    </td>
                                                       
                                                </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
                                </table>
                                
                            </div>
                               
                                </asp:Panel>--%>


                            <asp:Panel ID="pnlMarEntry" runat="server" Visible="false">

                                <br />
                                <div class="col-md-12">

                                    <div class="col-md-6">
                                        <ul class="list-group list-group-unbordered" style="margin-bottom: 0px;">
                                            <li class="list-group-item">
                                                <b>Registration No. :</b><a class="">
                                                    <asp:Label ID="lblRegno2" runat="server" ForeColor="#337ab7"></asp:Label></a>
                                            </li>


                                            <li class="list-group-item">
                                                <b>School/Institute Name  :</b><a class="">
                                                    <asp:Label ID="lblCollege2" runat="server" ForeColor="#337ab7"></asp:Label></a>
                                            </li>


                                            <li class="list-group-item">
                                                <b>Programme/Branch Name:</b><a class="">
                                                    <asp:Label ID="lblbranch2" runat="server" ForeColor="#337ab7"></asp:Label></a>
                                            </li>




                                        </ul>
                                    </div>

                                    <div class="col-md-6">
                                        <ul class="list-group list-group-unbordered" style="margin-bottom: 0px;">
                                            <li class="list-group-item">
                                                <b>Student Name  :</b><a class="">
                                                    <asp:Label ID="lblName2" runat="server" ForeColor="#337ab7"></asp:Label></a>
                                            </li>

                                            <li class="list-group-item">
                                                <b>Degree Name :</b><a class="">
                                                    <asp:Label ID="lblDegree2" runat="server" ForeColor="#337ab7"></asp:Label></a>
                                            </li>


                                        </ul>
                                    </div>

                                    <div class="col-md-12">
                                        <ul class="list-group list-group-unbordered" style="margin-top: 0px;">

                                            <li class="list-group-item">
                                                <b>Activity Name :</b>
                                                <asp:Label ID="lblactivity2" runat="server" ForeColor="#337ab7"></asp:Label>
                                            </li>

                                        </ul>
                                    </div>




                                    <div class="col-md-12">
                                        <table id="Table1" class="table table-striped table-bordered" style="width: 100%">
                                            <asp:ListView ID="lvMarActivity" runat="server">
                                                <LayoutTemplate>
                                                    <thead class="bg-light-blue">
                                                        <tr>

                                                            <%--   <th>Edit</th>--%>
                                                            <th style="text-align: center; width: 75px">Sr. No.</th>
                                                            <th style="text-align: center; width: 600px">Sub Acitivity Name</th>
                                                            <th style="text-align: center">Points</th>
                                                            <th style="text-align: center">No.of Times Participated </th>
                                                            <th style="text-align: center">Point Acquired</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>


                                                        <td style="text-align: center">
                                                            <%# Eval("SR_NO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SUB_ACTIVITY_NAME")%>
                                                        </td>

                                                        <td style="text-align: center">
                                                            <%# Eval("POINTS")%>
                                                        </td>

                                                        <td>
                                                            <asp:DropDownList ID="ddlSubPoints" runat="server" AutoPostBack="true" Width="170px" OnSelectedIndexChanged="ddlSubPoints_SelectedIndexChanged" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>



                                                            </asp:DropDownList>


                                                            <asp:HiddenField ID="hdnfldActivityNo" runat="server" Value='<%# Eval("ACTIVITYNO")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAcqPoint" runat="server" Width="100px" Enabled="false" ToolTip='<%# Eval("POINTS") %>'></asp:TextBox>

                                                            <asp:HiddenField ID="hdnPoints" runat="server" Value='<%# Eval("POINTS")%>' />
                                                            <asp:HiddenField ID="hdnfldSubActivityNo" runat="server" Value='<%# Eval("SUBACTIVITYNO")%>' />

                                                        </td>

                                                        <%--  <asp:HiddenField  ID="hdnbranch" runat="server"  Value='<%# Eval("LONGNAME")%>' />--%>
                                                    </tr>
                                                </ItemTemplate>

                                            </asp:ListView>
                                        </table>


                                        <div class="row">
                                            <center>
                                        <div class="col-md-4 col-md-offset-4" style="margin-top: 25px">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" OnClick="btnSubmit_Click"   
                                            TabIndex="8" CssClass="btn btn-info " />
                                        <asp:Button ID="btnBack" runat="server" Text="Back To Student List"  ToolTip="Back"  OnClick="btnBack_Click" CausesValidation="false"
                                            TabIndex="9"  CssClass="btn btn-warning" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="MarEntry"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                               </div>
                                    </center>
                                        </div>
                            </asp:Panel>

                            <%--<div id="collapse1" class="panel-collapse collapse in"> </div> --%>
                            <div style="display:none">
                            <asp:Panel ID="pnlActivity" runat="server" Visible="False" Width="100%">
                                <table id="Table3" runat="server" width="100%">
                                    <tr>
                                        <td>
                                            <asp:ListView ID="lvActivity" runat="server" OnSelectedIndexChanged="lvActivity_SelectedIndexChanged" OnItemDataBound="lvActivity_ItemDataBound">
                                                <LayoutTemplate>
                                                    <div id="demo-grid" class="vista-grid" style="height: 900px">
                                                        <div class="titlebar heading">
                                                        </div>
                                                        <table style="width: 100%">
                                                            <tr data-toggle="toggle">
                                                                <td>
                                                                    <table class="dataTable table table-bordered table-striped table-hover" style="width: 100%">
                                                                        <thead>

                                                                            <tr style="height: 20px" class="header bg-light-blue">

                                                                                <th style="text-align: center; width: 38px">Sr.No</th>

                                                                                <th style="text-align: center; width: 1025px">Acitivity Name</th>
                                                                                <th style="text-align: center; width: 75px">Points </th>
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="Tr7" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>

                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0" class="datatable" width="100%">
                                                        <tr style="padding-bottom: 15px">
                                                            <td>
                                                                <table style="width: 100%">
                                                                    <tr id="MAIN" runat="server" style="padding-bottom: 15px">
                                                                        <td>
                                                                            <tr id="MainTableRow" runat="server" class="item" onmouseout="this.style.backgroundColor='#FFFFFF'"
                                                                                onmouseover="this.style.backgroundColor='#a2cbecf5'" style="padding-bottom: 15px">

                                                                                <td style="text-align: center; padding: 6px 2px"><%# Container.DataItemIndex + 1%>  </td>

                                                                                <td width="80%" style="padding: 6px 2px">

                                                                                    <asp:Panel ID="pnlDetails" runat="server" Style="cursor: pointer; vertical-align: top; float: left">



                                                                                        <%# Eval("ACTIVITY_NAME" )%>
                                                                                        <%--<asp:Image ID="imgExp" runat="server" ImageUrl="~/images/action_down.gif" />--%>
                                                                                    </asp:Panel>
                                                                                    <asp:Label ID="lblActno" runat="server" Text='<%# Eval("ACTIVITYNO") %>'
                                                                                        ToolTip='<%# Eval ("ACTIVITYNO") %>' Visible="false"></asp:Label>

                                                                                </td>



                                                                                <%--   <td style="text-align: center">--%>



                                                                                <td style="text-align: center; padding: 6px 2px">
                                                                                    <%# Eval("POINTS")%>
                                                                                    <%-- </td>--%>
                                                                                    <ajaxToolKit:CollapsiblePanelExtender ID="cpeCourt2" runat="server" CollapseControlID="pnlDetails" ExpandedText="(Collapse...)" CollapsedText="(Expand...)"
                                                                                        Collapsed="true" CollapsedImage="~/images/action_down.gif" ExpandControlID="pnlDetails" OnDataBinding="cpeCourt2_DataBinding"
                                                                                        ExpandedImage="~/images/action_up.gif" TargetControlID="pnlStudent">
                                                                                    </ajaxToolKit:CollapsiblePanelExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr id="divtest">
                                                                        <td>
                                                                            <table border="0" width="100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Panel ID="pnlStudent" runat="server" ScrollBars="Auto" CssClass="collapsePanel">
                                                                                            <table class="hideTr" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                <tr>
                                                                                                    <td align="center">
                                                                                                        <%-- <b>Students List For MAR Entry </b>--%>
                                                                                                        <asp:ListView ID="lvStudent" runat="server">
                                                                                                            <LayoutTemplate>
                                                                                                                <div id="demo-grid" class="vista-grid" style="width: 99%;">
                                                                                                                    <table class="dataTable table table-bordered table-striped table-hover" width="99%">
                                                                                                                        <thead>
                                                                                                                            <tr class="header bg-light-blue">
                                                                                                                                <th style="text-align: center">Sr.No</th>
                                                                                                                                <th style="text-align: center">Student Name </th>
                                                                                                                                <th style="text-align: center">Reg. No. </th>
                                                                                                                                <th style="text-align: center">Current ACD Points</th>
                                                                                                                                <th style="text-align: center">Current PER Points</th>
                                                                                                                                <th style="text-align: center">Current Status</th>
                                                                                                                                <th style="text-align: center">Action</th>
                                                                                                                                </th>
                                                                                                                            </tr>
                                                                                                                        </thead>
                                                                                                                        <tbody>
                                                                                                                            <tr id="itemPlaceholder" runat="server">
                                                                                                                            </tr>
                                                                                                                        </tbody>
                                                                                                                    </table>
                                                                                                                </div>
                                                                                                            </LayoutTemplate>
                                                                                                            <EmptyDataTemplate>
                                                                                                                <div style="text-align: center; font-family: Arial; font-size: medium">
                                                                                                                    No Student Found
                                                                                                                </div>
                                                                                                            </EmptyDataTemplate>
                                                                                                            <ItemTemplate>
                                                                                                                <tr class="item">
                                                                                                                    <td style="text-align: center">
                                                                                                                        <%# Container.DataItemIndex + 1%>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%# Eval("STUDNAME")%>
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%# Eval("REGNO")%>
                                                                                                                    </td>
                                                                                                                    <td style="text-align: center">

                                                                                                                        <%# Eval("CURRENT_ACQ_POINTS")%>

                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%# Eval("CURRENT_PER_POINTS")%>

                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <%-- <asp:HiddenField ID="hidNewActivityNo" runat="server"  Values='<%# Eval("CURRENT_STATUS")%>'/>--%>
                                                                                                                        <%# Eval("CURRENT_STATUS")%>
                                                      
                                                                                                                    </td>


                                                                                                                    <td style="text-align: center">
                                                                                                                        <asp:Button ID="btnMar" ToolTip='<%# Eval("ACTIVITYNO") %>' runat="server" CssClass="btn btn-success" Text="Enter MAR" OnClick="btnMar_Click" CommandArgument='<%# Eval("IDNO") %>' />


                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:ListView>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:ListView>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            </div>
                           <asp:Panel ID="pnlActivityNew" runat="server" Visible="false">
                               <asp:ListView ID="lvStudentNew" runat="server" Visible="false" OnItemDataBound="lvStudentNew_ItemDataBound1">
                                   <LayoutTemplate>
                                       <div>
                                           <strong>Students List</strong>
                                           <table class="dataTable table table-bordered table-striped table-hover" style="width: 100%">
                                               <thead>
                                                   <tr class="bg-light-blue" style="background-color: #337ab7; color: white">
                                                       <th style="width:7%">Show
                                                                </th>
                                                       <th style="width:10%">
                                                           Registration No.
                                                       </th>
                                                       <th style="width:41%">
                                                           Student Name
                                                       </th>
                                                       <th  style="width:10%">Current ACQ Points</th>
                                                       <th  style="width:10%">Current PER Points</th>
                                                       <th  style="width:10%">Current Status</th>

                                                   </tr>
                                               </thead>
                                               <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                           </table>
                                       </div>
                                   </LayoutTemplate>
                                   <ItemTemplate>
                                       <table cellpadding="0" cellspacing="0" class="datatable" width="100%">
                                           <table class="dataTable table table-bordered table-striped table-hover" style="width: 100%">
                                               <tr id="MainTableRow" runat="server" class="item">
                                                   <td style="width:7%">
                                                       <asp:Panel ID="pnlDetails" runat="server" Style="cursor: pointer; vertical-align: top; float: left">
                                                           <asp:Image ID="imgExp" runat="server" ImageUrl="~/images/action_down.gif" />
                                                       </asp:Panel>
                                                   </td>
                                                   <td style="width:10%">
                                                       <asp:Label ID="lblRegNo" runat="server" Text='<%# Eval("REGNO") %>' ToolTip=<%# Eval("IDNO") %>></asp:Label>
                                                   </td>
                                                   <td style="width:50%">
                                                       <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("STUDNAME") %>'></asp:Label>
                                                   </td>
                                                   <td style="width:10%">
                                                       <asp:Label ID="lblACQPoint" runat="server" Text='<%# Eval("CURRENT_ACQ_POINTS") %>'></asp:Label>
                                                   </td>
                                                   <td style="width:10%">
                                                       <asp:Label ID="lblCurrPerPoints" runat="server" Text='<%# Eval("CURRENT_PER_POINTS") %>'></asp:Label>
                                                   </td>
                                                   <td style="width:10%">
                                                       <asp:Label ID="lblCurrStatus" runat="server" Text='<%# Eval("CURRENT_STATUS") %>'></asp:Label>
                                                   </td>
                                                   <td>
                                                                <ajaxToolKit:CollapsiblePanelExtender ID="cpeCourt2" runat="server" CollapseControlID="pnlDetails"
                                                                    Collapsed="true" CollapsedImage="~/images/action_down.gif" ExpandControlID="pnlDetails"
                                                                    ExpandedImage="~/images/action_up.gif" ImageControlID="imgExp" TargetControlID="pnlShowCDetails">
                                                                </ajaxToolKit:CollapsiblePanelExtender>
                                                            </td>
                                               </tr>
                                           </table>
                                           <tr>
                                               <td>
                                                   <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                       <tr>
                                                           <td>
                                                               <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                   <tr>
                                                                       <td>
                                                                            <asp:Panel ID="pnlShowCDetails" runat="server" CssClass="collapsePanel">
                                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tr>
                                                                                        <td align="center">
                                                                                            <asp:ListView ID="lvAcitivityNew" runat="server">
                                                                                                <LayoutTemplate>
                                                                                                    <div id="demo-grid" class="vista-grid" style="width: 99%; height: 60%">
                                                                                                        <table class="dataTable table table-bordered table-striped table-hover" width="99%">
                                                                                                            <thead>
                                                                                                                <tr class="header bg-light-blue">
                                                                                                                    <th>
                                                                                                                        Sr.No.
                                                                                                                    </th>
                                                                                                                    <th>
                                                                                                                        Activity Name 
                                                                                                                    </th>
                                                                                                                    <th>
                                                                                                                        Points
                                                                                                                    </th>
                                                                                                                    <th>Action</th>
                                                                                                                </tr>
                                                                                                            </thead>
                                                                                                            <tbody>
                                                                                                                        <tr id="itemPlaceholder" runat="server">
                                                                                                                        </tr>
                                                                                                                    </tbody>
                                                                                                        </table>
                                                                                                    </div>
                                                                                                </LayoutTemplate>
                                                                                                  <EmptyDataTemplate>
                                                                                                            <div style="text-align: center; font-family: Arial; font-size: medium">
                                                                                                                No Record Found
                                                                                                            </div>
                                                                                                        </EmptyDataTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <tr class="item">
                                                                                                        <td>
                                                                                                            <%# Container.DataItemIndex+1 %>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblActivityName" runat="server" Text='<%# Eval("ACTIVITY_NAME") %>'></asp:Label>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:Label ID="lblPoints" runat="server" Text='<%# Eval("POINTS") %>'></asp:Label>
                                                                                                        </td>
                                                                                                     <%--   <td style="text-align: center">
                                                                                                                        <asp:Button ID="btnMarNew" ToolTip='<%# Eval("ACTIVITYNO") %>' runat="server" CssClass="btn btn-success" Text="Enter MAR" OnClick="btnMarNew_Click" CommandArgument='<%# Eval("IDNO") %>' />
                                                                                                                    </td>--%>
                                                                                                        <td>
                                                                                                            <asp:Button ID="btnMarNew" runat="server" CssClass="btn btn-success" Text="Enter MAR" OnClick="btnMarNew_Click1" ToolTip='<%# Eval("ACTIVITYNO") %>'  CommandArgument='<%# Eval("IDNO") %>'/>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </ItemTemplate>
                                                                                            </asp:ListView>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                       </td>
                                                                   </tr>
                                                               </table>
                                                           </td>
                                                       </tr>
                                                   </table>
                                               </td>
                                           </tr>
                                       </table>
                                   </ItemTemplate>
                               </asp:ListView>
                           </asp:Panel>

                        </div>
                        <div id="divMsg" runat="server">
                        </div>

                    </div>
                </div>
            </div>
            </div>
            <br />
            </b></b></b></b>
        </ContentTemplate>

        <Triggers>
            <%--  <asp:PostBackTrigger ControlID="btnMar" />--%>
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnBack" />

            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>

    </asp:UpdatePanel>


    <%--<script>
    </script>

    <script type="text/javascript">

        // Initialization logic
        $(function () {
            $("input.algc-level-visiblity").each(function () {
                if ($(this).val())
                    $(this).closest("div").show();
                else
                    $(this).closest("div").hide();
                // TODO: Add other rules that dictate parent div visibility toggle.
            });
        });

</script>--%>






    <%--  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>  

   <script type="text/javascript">
       $(document).ready(function () {
           debugger;
           $('.hideTr').slideUp(600);
           $('[data-toggle="toggle"]').click(function () {
               if ($(this).parents().next(".hideTr").is(':visible')) {
                   $(this).parents().next('.hideTr').slideUp(600);
                   $(".plusminus" + $(this).children().children().attr("id")).text('+');
                   $(this).css('background-color', 'white');
               }
               else {
                   $(this).parents().next('.hideTr').slideDown(600);
                   $(".plusminus" + $(this).children().children().attr("id")).text('- ');
                   $(this).css('background-color', '#c1eaff ');
               }
           });
       });
</script>  --%>
</asp:Content>


