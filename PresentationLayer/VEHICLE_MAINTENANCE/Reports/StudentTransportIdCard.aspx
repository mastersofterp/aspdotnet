<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentTransportIdCard.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Reports_StudentTransportIdCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:UpdatePanel ID="updStudent" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">STUDENT ID CARD</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Route</label>
                                            <asp:Label ID="lblRoute" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlRoute" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAdmission" runat="server" ControlToValidate="ddlRoute"
                                            Display="None" ErrorMessage="Please Select Route" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>

                              

                                   
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Student" CssClass="btn btn-primary" 
                                    ToolTip="Shows Students under Selected Criteria." ValidationGroup="show" OnClick="btnShow_Click"/>

                              <asp:Button ID="btnIdCardReport" runat="server" Text="Student ID Card" CssClass="btn btn-primary" 
                                    ToolTip="Students ID Card Report." ValidationGroup="show" OnClick="btnIdCardReport_Click"/>
                            
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    CssClass="btn btn-warning" ToolTip="Cancel Selected under Selected Criteria." />

                                <asp:ValidationSummary ID="ValidationsUM" ValidationGroup="show" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" runat="server"/>
                            </div>

                           

                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvStudentTransportRecords" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Search Results</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">

                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="chkIdentityCard" runat="server" onClick="SelectAll(this);" ToolTip="Select or Deselect All Records" />
                                                        </th>
                                                        <th><%--<asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>--%>RRNO
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th><%--<asp:Label ID="lblDYddlSemester_Tab2" runat="server" Font-Bold="true"></asp:Label>--%>Semester
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
                                                    <asp:CheckBox ID="chkReport" runat="server" onClick="totSubjects(this);" />
                                                    <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("REGNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STUDNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTERNAME")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
      
    </asp:UpdatePanel>
      <div id="divMsg" runat="server">
    </div>

     <script type="text/javascript" language="javascript">
         function SelectAll(headchk) {
             var frm = document.forms[0]
             for (i = 0; i < document.forms[0].elements.length; i++) {
                 var e = frm.elements[i];
                 if (e.type == 'checkbox') {
                     if (headchk.checked == true)
                         e.checked = true;
                     else
                         e.checked = false;
                 }
             }
         }
    </script>
</asp:Content>

