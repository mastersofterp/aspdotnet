<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="VehicleReqApproval.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_VehicleReqApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPanel"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div id="div2" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">Vehicle Requisition Approval</h3>
                    </div>

                    <div class="box-body">
                        <div class="col-12" id="divControls" runat="server" >
                            <div id="divSearch" runat="server">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Name Of The Institution</label>
                                    </div>
                                    <asp:DropDownList ID="ddlInstitute" runat="server"  CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Date Of Journey </label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="imgFromDate">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtDateOfJourney" runat="server"  CssClass="form-control" TabIndex="2"></asp:TextBox>
                                        <ajaxToolKit:CalendarExtender ID="ceFromdate" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgFromDate" TargetControlID="txtDateOfJourney">
                                        </ajaxToolKit:CalendarExtender>

                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label></label>
                                    </div>
                                    <asp:RadioButtonList ID="rdlOneWay"  runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="1">One-Way &nbsp;</asp:ListItem>
                                        <asp:ListItem Value="2">Two-Way</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                                </div>
                            <div class="col-12" id="divReqList" runat="server">
                                <asp:ListView ID="lvVehicleReq" runat="server">
                                    <LayoutTemplate>
                                        <div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Name Of The Institution</th>
                                                        <th>Date Of Journey</th>
                                                        <th>One-Way/Two-Way</th>
                                                        <th>Approve/Reject</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                        <%--<div class="listwokingday">
                                            <div id="demo-grid" class="vista-grid">
                                                <table class="table table-bordered table-hover">
                                                   
                                                </table>
                                            </div>
                                        </div>--%>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%#Eval("COLLEGE_NAME")%>                                                              
                                            </td>
                                            <td>
                                                <%#Eval("DATE_OF_JOURNEY","{0:dd/MM/yyyy}")%>                                                        
                                            </td>
                                            <td>
                                                <%#Eval("ONE_WAY")%>                                                        
                                            </td>
                                            <td>

                                                <asp:Button ID="btnSelect" runat="server" CommandArgument='<%#Eval("VEH_REQ_ID")%>' Text="Select" CssClass="btn btn-primary" OnClick="btnSelect_Click" />

                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>


                            </div>

                            <div class="col-12">
                                <asp:ListView ID="lvGuestStaff" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div>
                                            <div class="sub-heading">
                                                <h5>Details Of Guest/Staff</h5>
                                            </div>
                                            <table class="table table-striped table-bordered " style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Guest/Staff Name</th>
                                                        <th>Pickup Location </th>
                                                        <th>Pickup Time</th>
                                                        <th>Dropping Location</th>
                                                        <th>Contact Number</th>
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
                                            <td>
                                                <%#Eval("ISGUEST").ToString() == "Y" ? Eval("GUEST_NAME") : Eval("EMP_NAME") %>                                                                                                                                             
                                            </td>

                                            <td>
                                                <%#Eval("PIKUP_LOC")%>                                                        
                                            </td>

                                            <td>
                                                <%#Eval("PIKUP_TIME","{0:hh:mm tt}")%>          
                                            </td>

                                            <td>
                                                <%#Eval("DROP_LOCATION") %> 
                                            </td>
                                            <td>
                                                <%#Eval("PHONE") %> 
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                            </div>

                            <div class="col-12 mt-3">
                                <asp:ListView ID="lvVehicle" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div>
                                            <h4>Required Vehicle Details</h4>
                                            <table class="table table-striped table-bordered " style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Vehicle Name</th>
                                                        <th>Vehicle Type</th>

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

                                            <td>

                                                <%#Eval("VNAME")%>                                                              
                                            </td>

                                            <td>
                                                <%#Eval("VEHICLE_AC_NONAC")%>                                                        
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>


                            </div>

                        </div>

                        <div class="col-12" id="divApprove" runat="server" visible="false">
                            <div class=" sub-heading">
                                <h5>Approval Or Rejection Table</h5>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Select</label>
                                    </div>
                                    <asp:DropDownList ID="ddlSelect" runat="server" CssClass="form-control" data-select2-enable="true"
                                        AppendDataBoundItems="true">
                                        <asp:ListItem Value="A">Approve</asp:ListItem>
                                        <asp:ListItem Value="R">Reject</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-6 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Remarks</label>
                                    </div>
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine"
                                        Height="50px" />
                                </div>

                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="11" ValidationGroup="Submit" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary" TabIndex="11" OnClick="btnBack_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="11" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="vsbuss" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            </div>

                        </div>


                    </div>
                </div>
            </div>

        </ContentTemplate>
        <%--<Triggers>
            <asp:AsyncPostBackTrigger ControlID="updPanel" />
        </Triggers>--%>
    </asp:UpdatePanel>



</asp:Content>

