<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_AuthorityTypeEmployeeDetail.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_AuthorityTypeEmployeeDetail" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>


    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
              <%--  <div id="div1" runat="server"></div>--%>
                <div class="box-header with-border">
                    <h3 class="box-title">EMPLOYEE AUTHORITY TYPE UPDATE</h3>
                    <p class="text-center">
                    </p>
                    <div class="box-tools pull-right">
                    </div>
                </div>

                <form role="form">
                    <div class="box-body">
                        <div class="col-md-12">
                            <h5>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br />
                            </h5>
                            <div class="panel panel-info">
                                <div class="panel-heading">Select College/Staff</div>
                                <div class="panel-body">
                                    <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                    <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                    <asp:Panel ID="pnlSelect" runat="server">

                                        <div class="form-group col-md-12">
                                            <div class="form-group col-md-6">
                                                
                                                <div class="form-group col-md-10">
                                                    <label>College :<span style="color: Red">*</span></label>

                                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College" AppendDataBoundItems="true" AutoPostBack="true"
                                                        TabIndex="2" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege" ValidationGroup="payroll"
                                                        ErrorMessage="Please Select College" SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-md-10">
                                                    <label>Staff :<span style="color: Red">*</span></label>

                                                    <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                                        ToolTip="Select Staff" TabIndex="3" CssClass="form-control" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                                        <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="0">All Staff</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStaff" ValidationGroup="payroll"
                                                        ErrorMessage="Please Select Staff" SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                </div>

                                            </div>
                                            <div class="form-group col-md-6">
                                            </div>
                                        </div>

                                    </asp:Panel>

                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnShow" runat="server" Text="Show" TabIndex="4" ValidationGroup="payroll" CssClass="btn btn-info"
                                    OnClick="btnShow_Click" Visible="false"/>
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="payroll"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                            </div>

                            <asp:Panel ID="pnlMonthlyChanges" runat="server">
                               
                                <div >

                                    <asp:ListView ID="lvMonthlyChanges" runat="server" OnItemDataBound="lvMonthlyChanges_ItemDataBound">
                                        <EmptyDataTemplate>
                                            <br />
                                            <center>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" /></center>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div  style="width: 100%">
                                                <div class="titlebar">
                                                    <h4>Employee List</h4>
                                                </div>

                                                <table class="table table-hover table-bordered">
                                                    <thead>
                                                        <tr class="bg-light-blue" >
                                                            <th width="3%">Idno
                                                            </th>
                                                            <th width="23%">Name
                                                            </th>
                                                             <th width="15%">
                                                                Department
                                                            </th>
                                                             <th width="15%">
                                                                Designation
                                                            </th>
                                                            <th width="15%">
                                                                 Authority Type
                                                            </th>

                                                        </tr>
                                                        <thead>
                                                </table>

                                            </div>
                                            <div class="listview-container" style="width: 100%">
                                                <div id="Div1" class="vista-grid" >
                                                    <table >
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>

                                            <tr>
                                                <td width="3%">
                                                    <%#Eval("IDNO")%>
                                                </td>
                                                <td width="23%">
                                                    <%#Eval("NAME")%>
                                                </td>
                                                  <td width="15%">
                                                    <%#Eval("SUBDEPT")%>
                                                </td>
                                                  <td width="15%">
                                                    <%#Eval("SUBDESIG")%>
                                                </td>

                                               
                                                

                                                <td width="15%">
                                                  


                                                    <asp:DropDownList ID="ddlAuthoType" runat="server" AppendDataBoundItems="true" CssClass="form-control"></asp:DropDownList>
                                                    <asp:HiddenField ID="hdnAuthoTypeStatus" runat="server" Value='<%#Eval("CHECK_STATUS")%>' /> 
                                                    <asp:HiddenField ID="hdnAuthoTypeId" runat="server" Value='<%#Eval("AUTHO_TYP_ID")%>' />                                       
                                                    <asp:HiddenField ID="hdnIDNO" runat="server" Value='<%#Eval("IDNO")%>' /> 
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>

                                </div>
                                <div class="col-md-12 text-center">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll"
                                        OnClick="btnSub_Click" OnClientClick="return ConfirmMessage();" CssClass="btn btn-primary" TabIndex="5" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="6" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>

                            </asp:Panel>
                        </div>

                    </div>
                </form>
            </div>

        </div>


    </div>

    

    <div id="divMsg" runat="server">
    </div>
   
    <script type="text/javascript" language="javascript">
       


        function ConfirmMessage() {
           

            if (confirm("Do you want to save changes in " )) {
                return true;
            }
            else {
                return false;
            }
        }


      

    </script>


</asp:Content>




