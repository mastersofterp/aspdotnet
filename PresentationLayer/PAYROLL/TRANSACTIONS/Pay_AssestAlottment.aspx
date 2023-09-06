<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage2.master" AutoEventWireup="true" CodeFile="Pay_AssestAlottment.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_AssestAlottment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="pnDisplay" runat="server">
          <div class="row">
            <div class="col-md-12">
                <!-- general form elements -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Employee Asset Allotment</h3>
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                        <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">

                                <div class="box box-primary ">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">General Information</h3><br />
                                        <div class="box-tools pull-right">
                                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                                <i class="fa fa-minus"></i>
                                            </button>

                                        </div>
                                    </div>
                                    <!-- /.box-header -->
                                    <!-- form start -->
                                    <%--   <form role="form">--%>

                                    <div class="form-group col-md-12">
                                        <%-- <asp:RadioButtonList runat="server" ID="rdoSelection" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="IDNO" Selected="True" > IDNO</asp:ListItem>
                                                         <asp:ListItem Value="NAME" > NAME</asp:ListItem>
                                                         <asp:ListItem Value="RFID" > RFID</asp:ListItem>
                                                        <asp:ListItem Value="PFILENO" > EMPLOYEE CODE</asp:ListItem>
                                                    </asp:RadioButtonList>--%>
                                        <%--      <label>ID No. </label>--%>


                                        <div class="col-md-12" id="DivSerach" runat="server">
                                            <div class="form-group col-md-12">
                                                <div class="form-group col-md-6">
                                                    <asp:RadioButtonList runat="server" ID="rdoSelection" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="IDNO" Selected="True"> IDNO</asp:ListItem>
                                                        <asp:ListItem Value="NAME"> NAME</asp:ListItem>

                                                        <asp:ListItem Value="PFILENO"> EMPLOYEE CODE</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <asp:TextBox ID="txtSearch" runat="server" TabIndex="5" placeholder="Search for..."
                                                        CssClass="form-control" ToolTip="Enter Search String"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <p class="text-center">
                                                        <asp:Button ID="Search" runat="server" Text="Search" OnClick="Search_Click"
                                                            CssClass="btn btn-info" TabIndex="6" ToolTip="Click here to Search Employee" />
                                                        <asp:Button ID="btnCanceModal" runat="server" Text="Cancel" OnClick="btnCanceModal_Click"
                                                            CssClass="btn btn-warning" TabIndex="7" ToolTip="Click here to Reset" />
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <%--<asp:Panel ID="pnlSearch" runat="server" ScrollBars="Auto" Height="300px">--%>
                                                <%--<asp:Panel ID="pnlSearch" runat="server" ScrollBars="Auto" Height="300px" Width="520px">--%>
                                                <asp:ListView ID="ListView1" runat="server">
                                                    <LayoutTemplate>
                                                        <div>
                                                            <%--<h4>Login Details</h4>--%>
                                                            <asp:Panel ID="pnlSearch" runat="server" ScrollBars="Vertical" Height="350px">
                                                                <table class="table table-hover table-bordered">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>Name
                                                                            </th>
                                                                            <th>Department
                                                                            </th>
                                                                            <th>Designation
                                                                            </th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                            </asp:Panel>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name")%>' CommandArgument='<%# Eval("IDNo")%>' OnClick="lnkId_Click"></asp:LinkButton>
                                                                <%-- --%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("SUBDEPT")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("SUBDESIG")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                                <%--</asp:Panel>--%>
                                            </div>                                       
                                        </div>
                                        <br />
                                        <asp:Panel ID="pnlId" runat="server" Visible="false">
                                            <div class="form-group col-md-12">
                                                <div class="form-group col-md-8">
                                                    <div class="form-group col-md-6">

                                                        <label>Employee No.</label>
                                                        <asp:Label ID="lblIDNo" runat="server"></asp:Label>
                                                    </div>

                                                  <%--  <div class="form-group col-md-6">
                                                        <label>Employee Code.</label>
                                                        <asp:Label ID="lblEmpcode" runat="server"></asp:Label>
                                                    </div>--%>
                                                    <div class="form-group col-md-6">
                                                        <label>Employee Name :</label>
                                                        <asp:Label ID="lbltitle" runat="server"></asp:Label>
                                                        <asp:Label ID="lblFName" runat="server"></asp:Label>
                                                        <asp:Label ID="lblMname" runat="server"></asp:Label>
                                                        <asp:Label ID="lblLname" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="form-group col-md-6">
                                                        <label>Date of Joining :</label>
                                                        <asp:Label ID="lblDOJ" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="form-group col-md-6">
                                                        <label>Department : </label>
                                                        <asp:Label ID="lblDepart" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="form-group col-md-6">
                                                        <label>Designation :</label>
                                                        <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="form-group col-md-6">
                                                        <label>Mobile No :</label>
                                                        <asp:Label ID="lblMob" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="form-group col-md-6">
                                                        <label>Email :</label>
                                                        <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                                    </div>

                                                </div>

                                                <div class="form-group col-md-4">
                                                    <div class="form-group col-md-6 text-right">
                                                        <asp:Image ID="imgPhoto" runat="server" Width="234px" Height="250px" Visible="false" Style="margin-left: 2px" />
                                                    </div>
                                                </div>
                                                <%--------- here new code added  by me------%>
                                                 <div class="form-group col-md-6">
                                                    <label>Asset Type: </label>
                                                    <span style="color: #FF0000">*</span>
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlasseststype" runat="server"
                                                                ValidationGroup="search" AppendDataBoundItems="True" AutoPostBack="true" class="form-control">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                 <asp:ListItem Value="1">LAPTOP</asp:ListItem>
                                                                  <asp:ListItem Value="2">MOBILE</asp:ListItem>
                                                                 <asp:ListItem Value="3">INTERNET</asp:ListItem>
                                                                 <asp:ListItem Value="4">SIM CARD</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlasseststype"
                                                                InitialValue="0" Display="None" ErrorMessage="Please Select Assests type" SetFocusOnError="true" ValidationGroup="search" />
                                                        </ContentTemplate>
                                                     <%--   <Triggers >
                                                            <asp:PostBackTrigger ControlID="ddlAuthorityType" />
                                                        </Triggers>--%>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="form-group col-md-6">
                                                    <label>Asset Remark: </label>
                                                    <span style="color: #FF0000">*</span>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtassestsremark" runat="server" ControlToValidate="txtassestsremark" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtassestsremark"
                                                                InitialValue="0" Display="None" ErrorMessage="Please Enter Asset Remark" SetFocusOnError="true" ValidationGroup="search" />
                                                        </ContentTemplate>
                                                      <%--  <Triggers >
                                                            <asp:PostBackTrigger ControlID="ddlAuthorityName" />
                                                        </Triggers>--%>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="form-group col-md-6">
                                                  <%--  <label>College No: </label>--%>
                                                   
                                                           <asp:TextBox ID="txtcollegeno" runat="server" Visible="false" CssClass="form-control" ></asp:TextBox>

                                                </div>
                                                <br />
                                                <div class="form-group col-md-12" style="text-align: center">

                                                    <br />
                                                    &nbsp;&nbsp; 
                                                <asp:Button ID="Btnsubmit" runat="server" Text="Submit"
                                                    ValidationGroup="search" CssClass="btn btn-primary" OnClick="Btnsubmit_Click" />&nbsp;
                                                    <asp:Button ID="BtnCancel" runat="server" Text="Cancel"
                                                        CssClass="btn btn-warning" OnClick="BtnCancel_Click"/>
                                                    <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="search"
                                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                                </div>
                                            </div>


                                            <div class="box-footer">
                                                <div class="form-group col-md-12">
                                                    <asp:ListView ID="lstEmpAsset" runat="server">
                                                          <LayoutTemplate>
                                                            <div class="vista-grid">
                                                                <div class="titlebar">
                                                                    <h4>
                                                                        <label class="label label-default">Asset Details</label>
                                                                    </h4>
                                                                </div>
                                                                <table id="id1" class="table table-hover table-bordered">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th style="text-align: left;">Action
                                                                            </th>
                                                                            <th style="text-align: left;">Employee No
                                                                            </th>
                                                                            <th style="text-align: left;">Name
                                                                            </th>
                                                                             <th style="text-align: left;">Department
                                                                            </th>
                                                                             <th style="text-align: left;">
                                                                              Asset Name
                                                                            </th>                                                                           
                                                                             <th style="text-align: left;">
                                                                                Application Date
                                                                            </th>
                                                                            <th style="text-align: left;">Status
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item">
                                                                <td style="text-align: center;">
                                                                    <asp:ImageButton ID="btneditasset" runat="server"  OnClick="btneditasset_Click"
                                                                        CommandArgument='<%# Eval("ASSETALLOTID")%>' ImageUrl="~/images/edit.gif" ToolTip="Edit Record"  />
                                                                    <asp:HiddenField ID="hdnidno" runat="server" Value='<%# Eval("IDNO")%>' />
                                                                </td>                                       
                                                                <td>
                                                                     <%# Eval("EmployeeId")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("TITLE")%>
                                                                    <%# Eval("FNAME")%>
                                                                    <%# Eval("MNAME")%>
                                                                    <%# Eval("LNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SUBDEPT")%>
                                                                </td>
                                                                 <td>
                                                                    <%# Eval("ASSETNAME")%>
                                                                </td>
                                                                 <td>
                                                                      <%# Eval("CREATED_DATE")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ISAPPROVED_DETAILS")%>
                                                                    <asp:Label ID="lblISAPPROVED_STATUS" runat="server" Text='<%# Eval("ISAPPROVED")%>' Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
    
                                                    </asp:ListView>
                                                    <div class="vista-grid_datapager text-center">
                                                <asp:DataPager ID="dpPager" runat="server" PagedControlID="lstEmpAsset" PageSize="10" OnPreRender="dpPager_PreRender">
                                                    <Fields>
                                                        <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                            ShowLastPageButton="false" ShowNextPageButton="false" />
                                                        <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                        <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                            ShowLastPageButton="true" ShowNextPageButton="true" />
                                                    </Fields>
                                                </asp:DataPager>
                                            </div> 
                                                       
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>

