<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentVehicleSchedule.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_StudentVehicleSchedule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">
   <%-- <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    
        <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">STUDENT VEHICLE SCHEDULE</h3>
                        </div>

                        <div class="box-body">
                          <div class="col-12 btn-footer">
                        <asp:Button ID="btnrootdetails" runat="server" Text="Route Details" CssClass="btn btn-primary" ToolTip="Click here to Root Details" OnClick="btnRootDetails_Click"/>
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary" ToolTip="Click here to Back" OnClick="btnBack_Click"/>
                       </div>
                       
                          <div class="col-12">                      
                        <asp:ListView ID="lvVehicleSchedule" runat="server" ItemPlaceholderID="itemPlaceholder">
                            <LayoutTemplate>
                               <div class="sub-heading"><h5>Vehicle Schedule Entry List</h5> </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>
                                                    Date
                                                </th>
                                                <th>
                                                    Morning Trip
                                                </th>
                                                <th>
                                                    Special Trip
                                                </th>
                                                <th>
                                                    Evening Trip (03:30pm)
                                                </th>
                                                <th>
                                                    Late Trip (04:30pm)
                                                </th>                                                
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                               </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Eval("SCHEDULE_DATE","{0:dd-MMM-yyyy}")%>
                                    </td>
                                    <td>
                                        <%# Eval("MORNING_TRIP")%>
                                    </td>
                                    <td>
                                        <%# Eval("SPECIAL_TRIP")%>
                                    </td>
                                    <td>
                                        <%# Eval("EVENING_TRIP")%>
                                    </td>
                                    <td>
                                        <%# Eval("LATE_TRIP")%>
                                    </td>                                   
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>    
                        </div>
                          <div class="col-12 mt-3">
                        
                        <asp:ListView ID="lvRootDetails" runat="server" ItemPlaceholderID="itemPlaceholder2">
                            <LayoutTemplate>
                                <div id="Div2">
                                    <div class="sub-heading"><h5>Route Entry List</h5>
                                    </div>
                                  <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="Table1">
                                    <thead class="bg-light-blue">
                                        <tr>
                                                    <th>ROUTE NUMBER</th>
                                                    <th>ROUTE NAME </th>
                                                    <th>ROUTE PATH </th>
                                                    <th>DISTANCE (IN KM)</th>
                                                    <th>VEHICLE TYPE</th>
                                                                                 
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder2" runat="server" />
                                        </tbody>
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                     <td><%# Eval("ROUTE_NUMBER")%></td>
                                        <td><%# Eval("ROUTENAME")%></td>
                                        <td><%# Eval("ROUTEPATH")%></td>
                                        <td><%# Eval("DISTANCE")%></td>
                                        <td><%#Eval("VEHICLE_TYPE")%></td>
                              </tr>
                            </ItemTemplate>
                        </asp:ListView>   
                          </div>
                      
                                
                    </div>
                        </div>
                    </div>
            </div>
             
          
</asp:content>
