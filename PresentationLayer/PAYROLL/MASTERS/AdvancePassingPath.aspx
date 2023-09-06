<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AdvancePassingPath.aspx.cs" Inherits="PAYROLL_MASTERS_AdvancePassingPath" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>

    <script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table2').DataTable();
        });
    </script>

    <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };
    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>
     <script type="text/javascript" language="javascript">
         // Move an element directly on top of another element (and optionally
         // make it the same size)


         function totAllSubjects(headchk) {
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
    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ADVANCE PASSING AUTHORITY PATH</h3>
                        </div>
                        <div class="box-body">

                            <div class="col-md-12">
                                Note : <span style="color: #FF0000">* Marked Is Mandatory !</span><br />
                                <div class="col-md-8">
                                    <asp:Panel ID="pnlAdd" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">Add/Edit Advance Passing Authority/ Path </div>

                                            <div class="panel-body">

                                                <br />
                                                <div class="col-md-12">

                                                    <%--<fieldset class="fieldsetPay">
                                                <legend class="legendPay"></legend>--%>
                                                    <div class="box-tools pull-left">
                                                        <b>*Note &nbsp;:&nbsp;&nbsp; Selection of 'Passing Authority 01' is mandatory.</b>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <div class="form-group col-md-10">

                                                        <div class="form-group col-md-12">

                                                            <label>College Name&nbsp; :</label>


                                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College Name"
                                                                AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                                              >
                                                                  <%--OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"--%>
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                                                Display="None" ErrorMessage="Please Select College Name" ValidationGroup="PAuthority" SetFocusOnError="true"
                                                                InitialValue="0"></asp:RequiredFieldValidator>

                                                        </div>
                                                        <div class="form-group col-md-12">
                                                            <label>Department :<span style="color: #FF0000">*</span> </label>
                                                            <asp:DropDownList ID="ddlDept" runat="server" TabIndex="2"
                                                                ToolTip="Select Department" AppendDataBoundItems="true"
                                                                AutoPostBack="true"
                                                                CssClass="form-control" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                                                 <%--OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"--%>
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                                                                Display="None" ErrorMessage="Please Select Department" SetFocusOnError="true"
                                                                ValidationGroup="PAPath" InitialValue="0">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-12" id="trEmp" runat="server" style="color: #FF0000;" visible="false">


                                                            <label>Employee :<span style="color: #FF0000">*</span> </label>
                                                            <asp:Label ID="lblEmpName" runat="server" class="form-control" Font-Bold="true" TabIndex="3"></asp:Label>

                                                        </div>
                                                        <div class="form-group col-md-12">
                                                            <label>Sectional Head 01<span style="color: #FF0000">*</span></label>
                                                            <asp:DropDownList ID="ddlPA01" runat="server" TabIndex="4" ToolTip="Select Sectional Head 01" AppendDataBoundItems="true" CssClass="form-control"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlPA01_SelectedIndexChanged"  >
                                                                <%--OnSelectedIndexChanged="ddlPA01_SelectedIndexChanged"--%>
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvPA01" runat="server" ControlToValidate="ddlPA01"
                                                                Display="None" ErrorMessage="Please select Passing Authority 01" SetFocusOnError="true"
                                                                ValidationGroup="PAPath" InitialValue="0">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="text-center">
                                                            <asp:Image ID="darrow1" runat="server" ImageUrl="~/Images/action_down.png" Height="20px" Width="20px" />
                                                        </div>

                                                        <div class="form-group col-md-12">
                                                            <label>Sectional Head 02</label>
                                                            <asp:DropDownList ID="ddlPA02" runat="server" AppendDataBoundItems="true" TabIndex="5" ToolTip="Select Sectional Head 02" CssClass="form-control"
                                                                Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA02_SelectedIndexChanged" >
                                                                <%--OnSelectedIndexChanged="ddlPA02_SelectedIndexChanged1"--%>
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="text-center">
                                                            <asp:Image ID="darrow2" runat="server" ImageUrl="~/Images/action_down.png" Height="20px" Width="20px" />
                                                        </div>
                                                        <div class="form-group col-md-12">
                                                            <label>Sectional Head 03 </label>

                                                            <asp:DropDownList ID="ddlPA03" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="6" ToolTip="Select Sectional Head 03"
                                                                Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA03_SelectedIndexChanged" >
                                                                <%--OnSelectedIndexChanged="ddlPA03_SelectedIndexChanged1"--%>
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="text-center">
                                                            <asp:Image ID="daarow3" runat="server" ImageUrl="~/Images/action_down.png" Height="20px" Width="20px" />
                                                        </div>

                                                        <div class="form-group col-md-12">
                                                            <label>Sectional Head 04 </label>

                                                            <asp:DropDownList ID="ddlPA04" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="7" ToolTip="Select Sectional Head 04"
                                                                Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA04_SelectedIndexChanged">
                                                                 <%--OnSelectedIndexChanged="ddlPA04_SelectedIndexChanged"--%>
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="text-center">
                                                            <asp:Image ID="darrow4" runat="server" ImageUrl="~/Images/action_down.png" Height="20px" Width="20px" />
                                                        </div>

                                                        <div class="form-group col-md-12">
                                                            <label>Sectional Head 05 </label>
                                                            <asp:DropDownList ID="ddlPA05" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="6" ToolTip="Select Sectional Head 05"
                                                                Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="ddlPA05_SelectedIndexChanged" >
                                                                <%--OnSelectedIndexChanged="ddlPA05_SelectedIndexChanged"--%>
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-md-12">
                                                            <label>Path </label>
                                                            <asp:TextBox ID="txtPAPath" runat="server" CssClass="form-control" ReadOnly="true" TextMode="MultiLine"
                                                                Height="40px" TabIndex="7" ToolTip="Path"></asp:TextBox>

                                                        </div>

                                                    </div>

                                                    <%--<div class="form-group col-md-6">
                                                    </div>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>

                                <div class="col-md-4 table-responsive">
                                    <asp:Panel ID="pnlEmpList" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">Employees</div>
                                            <div class="panel-body">
                                                <asp:Panel ID="pnlEmp" runat="server" Height="600px" ScrollBars="Vertical">
                                                    <asp:ListView ID="lvEmployees" runat="server">
                                                        <EmptyDataTemplate>
                                                            <br />
                                                            <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl"
                                                                Text="Employee Not Found!" />
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div id="listViewGrid" class="vista-grid">
                                                                <h4 class="box-title">List of Employees</h4>

                                                                <table class="table table-bordered table-hover" id="tblSearchResults">
                                                                    <%--class="datatable"--%>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Sr.No
                                                                        </th>
                                                                        <th>
                                                                            <asp:CheckBox ID="cbAl" runat="server" onclick="totAllSubjects(this)" TabIndex="8" />

                                                                        </th>
                                                                        <th>Employee Name
                                                                        </th>


                                                                    </tr>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                                <td>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkIdNo" runat="server" ToolTip='<%# Eval("IDNO") %>' TabIndex="9" />

                                                                    <asp:HiddenField ID="hidEmployeeNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("NAME")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-12 text-center">
                                    <asp:Panel ID="pnlList" runat="server">

                                        <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew"  ToolTip="Click Add New To Enter Advance Passing Authority/Sectional Heads Path" Text="Add New" TabIndex="10" CssClass="btn btn-primary" OnClick="btnAdd_Click"></asp:LinkButton>
                                        <%--OnClick="btnAdd_Click"--%>
                                        <asp:Button ID="btnShowReport" TabIndex="11" runat="server" Text="Show Report" CssClass="btn btn-info"
                                             ToolTip="Click here to Show the report" OnClick="btnShowReport_Click" Visible="false"/>
                                        <%--OnClick="btnShowReport_Click"--%>
                                    </asp:Panel>
                                </div>
                                <asp:Panel ID="pnlbtn" runat="server">
                                    <div class="box-footer">
                                        <p class="text-center">
                                            <asp:Button ID="btnSave" runat="server" Text="Submit" TabIndex="12" ToolTip="Click here to Submit" ValidationGroup="PAPath"
                                                CssClass="btn btn-success" OnClick="btnSave_Click"  />
                                             <%--OnClick="btnSave_Click"--%>
                                            <asp:Button ID="btnCancel" runat="server" TabIndex="13" Text="Cancel" CausesValidation="false" ToolTip="Click here to Reset"
                                               CssClass="btn btn-danger" OnClick="btnCancel_Click" />&nbsp;
                                             <%--OnClick="btnCancel_Click"--%> 
                                                               <asp:Button ID="btnBack" runat="server" TabIndex="14" Text="Back" ToolTip="Click here to go back to previous" CausesValidation="false" 
                                                                   CssClass="btn btn-info" OnClick="btnBack_Click" />
                                            <%--OnClick="btnBack_Click"--%>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PAPath"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            <div class="col-md-12">
                                            </div>
                                            <p>
                                            </p>
                                            <p>
                                            </p>
                                            <p>
                                            </p>
                                        </p>
                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="col-md-12 table-responsive">
                                <asp:Panel ID="pnlPAPaList" runat="server">
                                    <asp:Repeater ID="lvPAPath" runat="server" OnItemCommand="lvPAPath_ItemCommand">
                                        <HeaderTemplate>
                                            <h4 class="box-title">Advance Passing Authority/ Sectional Head Path</h4>
                                            <table id="table2" class="table table-striped dt-responsive nowrap">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Action</th>
                                                        <th>Department
                                                        </th>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Sectional Head 01
                                                        </th>
                                                        <th>Sectional Head 02
                                                        </th>
                                                        <th>Sectional Head 03
                                                        </th>
                                                        <th>Sectional Head 04
                                                        </th>
                                                        <th>Sectional Head 05
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PAPNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="15" OnClick="btnEdit_Click"/>
                                                     <%--OnClick="btnEdit_Click"--%>
                                                    <asp:ImageButton ID="btnDelete" Visible="false" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("PAPNO") %>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;"
                                                         />
                                                   
                                                </td>
                                                <td>
                                                    <%# Eval("SUBDEPT")%>                                                                 
                                                </td>
                                                <td>
                                                    <%# Eval("NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PANAME1")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PANAME2")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PANAME3")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PANAME4")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PANAME5")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody></table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
     </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShowReport" />
        </Triggers>
    </asp:UpdatePanel>

    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    
    

   
    <div id="divMsg" runat="server">
    </div>

</asp:Content>

