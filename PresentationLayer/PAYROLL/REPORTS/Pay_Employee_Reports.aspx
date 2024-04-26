<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_Employee_Reports.aspx.cs" Inherits="PAYROLL_REPORTS_Pay_Employee_Reports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <asp:UpdatePanel runat="server" ID="UPDLedger">
        <ContentTemplate>
    <asp:Panel ID="pnDisplay" runat="server">



        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div id="div3" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">Joining Report</h3>
                    </div>

                    <div class="box-body">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <%--<div class="sub-heading">
                                        <h5>General Information</h5>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                        <div class="form-group col-md-12">


                            <div class="col-md-12" id="DivSerach" runat="server">
                                <asp:ListView ID="ListView1" runat="server">
                                      <LayoutTemplate>
                                                    

                                            <%--<h4>Login Details</h4>--%>
                                            <asp:Panel ID="pnlSearch" runat="server" ScrollBars="Vertical" Height="350px">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%;">
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
                            </div>
                        </div>



                        <asp:Panel ID="pnlId" runat="server" Visible="false" >
                            <div class="col-12" style="background-color:#ffffcc">
                                   <div class="form-group col-md-12" style="text-align: center">
                                &nbsp;&nbsp;&nbsp;
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-4">
                                        <div class="form-group col-lg-12" style="display:none">

                                            <label>ID No.</label>
                                            <asp:Label ID="lblIDNo" runat="server"></asp:Label>

                                        </div>

                                         <div class="form-group col-lg-12">
                                            <label>Employee Id. :</label>
                                            <asp:Label ID="lblEmpcode" runat="server"></asp:Label>
                                        </div>
                                         <div class="form-group col-lg-12">
                                            <label>Employee Name :</label>
                                            <asp:Label ID="lbltitle" runat="server"></asp:Label>
                                            <asp:Label ID="lblFName" runat="server"></asp:Label>
                                            <asp:Label ID="lblMname" runat="server"></asp:Label>
                                            <asp:Label ID="lblLname" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-group col-lg-12">
                                            <label>Date of Joining :</label>
                                            <asp:Label ID="lblDOJ" runat="server"></asp:Label>
                                        </div>


                                    </div>
                                    <div class="form-group col-lg-4 ">
                                        <div class="form-group col-lg-12">

                                            <label>Department : </label>
                                            <asp:Label ID="lblDepart" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-group col-lg-12">

                                            <label>Designation :</label>
                                            <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-group col-lg-12">

                                            <label>Mobile No :</label>
                                            <asp:Label ID="lblMob" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-group col-lg-12">

                                            <label>Email :</label>
                                            <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-4">
                                        <div class="form-group col-md-6 text-right">
                                            <asp:Image ID="imgPhoto" runat="server" Width="150px" Height="150px" Visible="false" Style="margin-left: 2px" />
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="form-group col-md-12" style="text-align: center">
                                &nbsp;&nbsp;&nbsp;
                                </div>


                            <div class="form-group col-md-12" style="text-align: center">
                                <asp:Button ID="btnReport" runat="server" Text="Joining Report"
                                    CssClass="btn btn-primary" OnClick="btnReport_Click" />&nbsp;

                                       <asp:Button ID="btnBack" runat="server" Text="<< Back"
                                    CssClass="btn btn-primary" OnClick="btnBack_Click" />&nbsp;


                                    <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="search"
                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                            </div>

                        </asp:Panel>

                        <div class="col-12" runat="server" id="pnlList">
                        </div>

                        <div id="List" runat="server">
                            <div class="col-12">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>







    </asp:Panel>
              </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

</asp:Content>

