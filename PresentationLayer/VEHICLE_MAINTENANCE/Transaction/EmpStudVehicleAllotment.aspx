<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EmpStudVehicleAllotment.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_EmpStudVehicleAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">EMPLOYEE/ STUDENT VEHICLE ALLOTMENT</h3>
                        </div>
                        <div>
                            <form role="form">
                                <div class="box-body">
                                    <div class="col-md-12">
                                        <div class="form-group col-md-12">Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span></div>
                                        <div class="form-group col-md-12">
                                            <asp:Panel ID="pnl" runat="server">
                                                <div class="panel panel-info">
                                                    <div class="panel panel-heading">Employee/Student Vehicle Allotment</div>
                                                    <div class="panel panel-body">
                                                          <div class="form-group col-md-12">
                                                            <div class="form-group row">
                                                                <div class="form-group col-md-2">
                                                                    <label>Select User Type :</label>
                                                                </div>
                                                                 <div class="form-group col-md-4">
                                                                    <asp:RadioButtonList ID="rdbUserType" runat="server" RepeatDirection="Horizontal" ToolTip="Select User Type"
                                                                        OnSelectedIndexChanged="rdbUserType_SelectedIndexChanged" AutoPostBack="true" TabIndex="1">
                                                                        <asp:ListItem Selected="True" Value="1">Employee</asp:ListItem>
                                                                        <asp:ListItem Value="2">Student</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                </div>                                                            
                                                        </div>

                                                        <div class="form-group col-md-12">
                                                            <div class="form-group row">
                                                                <div class="form-group col-md-3" id="divEmp" runat="server">
                                                                    <label><span style="color: #FF0000">*</span>Employee :</label>
                                                                    <asp:DropDownList ID="ddlEmployee" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                                        ToolTip="Select Employee" TabIndex="2">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lblVType" runat="server" Text=""></asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfvEmployee" runat="server" ErrorMessage="Please Select Employee."
                                                                        ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlEmployee" Display="None" >
                                                                    </asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-md-3" id="divDegree" runat="server" visible="false">
                                                                    <label><span style="color: #FF0000">*</span>Degree :</label>
                                                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                                        ToolTip="Select Degree" TabIndex="3" AutoPostBack="true"
                                                                         OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ErrorMessage="Please Select Degree."
                                                                        ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlDegree" Display="None">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-md-3" id="divBranch" runat="server" visible="false">
                                                                    <label><span style="color: #FF0000">*</span>Branch :</label>
                                                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                                        ToolTip="Select Branch" TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ErrorMessage="Please Select Branch."
                                                                        ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlBranch" Display="None">
                                                                    </asp:RequiredFieldValidator>
                                                                </div> 
                                                                   
                                                                <div class="form-group col-md-3" id="divSem" runat="server" visible="false">
                                                                    <label><span style="color: #FF0000">*</span>Semester :</label>
                                                                    <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                                        ToolTip="Select Semester" TabIndex="5" AutoPostBack="true" 
                                                                        OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvSem" runat="server" ErrorMessage="Please Select Semester."
                                                                        ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlSem" Display="None">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-md-3" id="divStud" runat="server" visible="false">
                                                                    <label><span style="color: #FF0000">*</span>Student :</label>
                                                                    <asp:DropDownList ID="ddlStudent" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                                        ToolTip="Select Student" TabIndex="6">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvStudent" runat="server" ErrorMessage="Please Select Student."
                                                                        ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlStudent" Display="None">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-md-3" id="trRouteDrop" runat="server" visible="true">
                                                                    <label><span style="color: #FF0000">*</span>Vehicle :</label>
                                                                    <asp:DropDownList ID="ddlVehicle" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlVehicle_SelectedIndexChanged" TabIndex="7" ToolTip="Select Vehicle">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvVehicle" runat="server" ErrorMessage="Please Select Vehicle." 
                                                                        ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlVehicle" Display="None">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-md-3" id="trRoute" runat="server" visible="false">
                                                                    <label>Route :</label>
                                                                    <asp:TextBox ID="txtRoute" runat="server" CssClass="form-control" ToolTip="Enter Route" TabIndex="8"></asp:TextBox>
                                                                </div>

                                                                <div class="form-group col-md-3" id="divBP" runat="server" visible="false">
                                                                    <label><span style="color: #FF0000">*</span>Boarding Point</label>
                                                                    <asp:DropDownList ID="ddlBoardingPoint" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                                        TabIndex="9" ToolTip="Select Boarding Point">
                                                                        <%--<asp:ListItem Value="0">Please Select</asp:ListItem>--%>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvBP" runat="server" ErrorMessage="Please Select Boarding Point." 
                                                                        ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlBoardingPoint" Display="None">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>

                                                            </div>
                                                        </div>

                                                    </div>
                                               </div>
                                            </asp:Panel>
                                        </div>

                                    </div>
                                </div>
                            </form>
                        </div>
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="Submit"
                                    OnClick="btnSubmit_Click" TabIndex="10" ToolTip="Click here to Submit" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click"
                                    TabIndex="11" ToolTip="Click here to Reset" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnReport_Click"
                                    TabIndex="12" ToolTip="Click here to Show Report" />
                                 <asp:Button ID="btnStudReport" runat="server" Text="Student Report" CssClass="btn btn-info" OnClick="btnStudReport_Click"
                                    TabIndex="13" ToolTip="Student Report" />
                                <asp:ValidationSummary ID="VS1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="Submit"/>
                                <asp:ValidationSummary ID="VS2" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="Add" />
                            </p>
                            <div class="col-md-12">
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvAllotment" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <h4 class="box-title">Vehicle Allotment List
                                                </h4>
                                                <table class="table table-bordered table-hover">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th style="width:5%">ACTION                        
                                                            </th>
                                                            <th style="width:15%">
                                                                <asp:Label ID="lblUserName" runat="server" Text="EMPLOYEE NAME"></asp:Label>
                                                            </th> 
                                                            <th style="width:15%">VEHICLE NAME
                                                            </th>                                                           
                                                            <th style="width:65%">ROUTE NAME-> Path
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
                                            <tr>
                                                <td style="width:5%">                                                                                           
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" 
                                                    CommandArgument='<%# Eval("URID") %>'
                                                ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" />
                                                </td>
                                                <td style="width:15%">
                                                    <%# Eval("NAME")%>
                                                </td>
                                                <td style="width:15%">
                                                    <%# Eval("VNAME")%>
                                                </td>
                                                <td style="width:65%">
                                                    <%# Eval("ROUTENAME")%>
                                                </td>                                                                                              
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                     <div class="vista-grid_datapager">
                                <div class="text-center">
                                    <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvAllotment" PageSize="10" OnPreRender="dpPager_PreRender">
                                        <Fields>
                                            <asp:NextPreviousPagerField
                                                FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true"
                                                ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                ShowLastPageButton="false" ShowNextPageButton="false" />
                                            <asp:NumericPagerField ButtonType="Link"
                                                ButtonCount="7" CurrentPageLabelCssClass="current" />
                                            <asp:NextPreviousPagerField
                                                LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true"
                                                ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                ShowLastPageButton="true" ShowNextPageButton="true" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </div>


                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
