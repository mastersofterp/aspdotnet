<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PayEmpNoDuesProforma.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_PayEmpNoDuesProforma" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlStart" runat="server">


        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">NO DUES PROFORMA FOR EMPLOYEE</h3>
                <div class="notice"><%--<span>Note : * marked fields are Mandatory</span>--%></div>
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" Style="display: none;" />
            </div>
            <div class="box-body">
                <div class="col-md-12 form-group">
                    <div class="row">

                        <div class="col-md-12" id="DivSerach" runat="server" >
                            <div class="form-group col-md-12" style="display:none">
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
                                        <asp:Button ID="Button1" runat="server" Text="Search" OnClick="btnSearch_Click"
                                            CssClass="btn btn-info" TabIndex="6" ToolTip="Click here to Search Employee" />
                                        <asp:Button ID="btnCanceModal" runat="server" Text="Cancel" OnClick="BtnCancelSearch_Click"
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
                        <%--<div class="form-group col-md-4">

                            <label>ID No. </label>
                            <div class="input-group  date">
                                <div class="input-group-addon">
                                    <asp:TextBox ID="txtIdNo" runat="server" ValidationGroup="submit" class="form-control"></asp:TextBox>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtIdNo"
                                        ValidChars="0123456789" FilterMode="ValidChars">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                    <%--  Enable the button so it can be played again --

                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" OnClick="btnSearch_Click"
                                        ValidationGroup="submit" />
                                    <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtIdNo"
                                        Display="None" ErrorMessage="Please Enter ID No." ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="submit" />
                                </div>
                                <asp:TextBox ID="txt" runat="server" Visible="false"></asp:TextBox>
                            </div>
                        </div>--%>
                    </div>
                </div>
                <div id="divCourses" runat="server" visible="false">
                    <h4>
                        <label class="label label-default">No Dues Proforma for Employee </label>
                    </h4>


                    <div class="col-md-12 form-group">

                        <div class="row" style="margin-top: 20px;">
                            <div class="col-md-9">
                                <div class="form-group col-md-6">
                                    <label>ID No.</label>
                                    <asp:Label ID="lblIDNo" runat="server"></asp:Label>

                                </div>

                                <div class="form-group col-md-6">
                                    <label>Employee Id.</label>
                                    <asp:Label ID="lblEmpcode" runat="server"></asp:Label>
                                </div>
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

                                <div class="form-group col-md-6" style="display:none">

                                    <label for="city"><span style="color: red;">*</span> Purpose of No Dues :</label>
                                    <asp:DropDownList ID="ddlduepurpose" runat="server" CssClass="form-control"
                                        AppendDataBoundItems="True" TabIndex="2" ToolTip="Please Select Purpose of No Dues">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">Resigning</asp:ListItem>
                                        <asp:ListItem Value="2">Retiring</asp:ListItem>
                                        <asp:ListItem Value="3">Transferring</asp:ListItem>
                                        <asp:ListItem Value="4">Terminating Contracton</asp:ListItem>
                                    </asp:DropDownList>
                                <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlduepurpose"
                                        Display="None" ErrorMessage="Please Select Purpose of No Dues" ValidationGroup="Report"></asp:RequiredFieldValidator>--%>
                                </div>
                                <%-- <div class="form-group col-md-6">
                                    <label>DueFee  :</label>
                                    <asp:Label ID="lblDueFee" runat="server" Font-Bold="True"></asp:Label>

                                </div>--%>
                            </div>
                            <div class="col-md-3 ">
                                <div class="form-group col-md-6">

                                    <asp:Image ID="imgPhoto" runat="server" Width="234px" Height="250px" Visible="false" Style="margin-left: 2px" />
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12  form-group text-center">
                            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Report" CssClass="btn btn-primary" ValidationGroup="Report" />
                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                Text="Cancel" ValidationGroup="backsem" CssClass="btn btn-danger" />
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="Report" />
                        </div>

                    </div>




                    <div class="box-footer">
                        <div class="form-group col-md-12">
                            <asp:ListView ID="lvNoDuesDetails" runat="server">
                                <LayoutTemplate>
                                    <div class="vista-grid">
                                        <div class="titlebar">
                                            <h4>
                                                <label class="label label-default">No Dues Details</label>
                                            </h4>
                                        </div>
                                        <table id="id1" class="table table-hover table-bordered">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="text-align: center;">Sr.No.
                                                    </th>
                                                    <th style="text-align: center;">Emp.Code
                                                    </th>
                                                    <th>Employee Name
                                                    </th>
                                                    <th style="text-align: center;">No Dues Status
                                                    </th>
                                                    <th>Remark
                                                    </th>
                                                    <th>Created By</th>
                                                    <th>Created Dept.</th>
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
                                            <%#Container.DataItemIndex+1 %>
                                        </td>
                                        <td style="text-align: center;">
                                            <%# Eval("REGNO")%>
                                        </td>
                                        <td>
                                            <%# Eval("NAME")%>
                                        </td>
                                        <td style="text-align: center;">
                                            <%# Eval("NODUES_STATUS_DETAILS")%>
                                            <asp:Label ID="lblNODUES_STATUS" runat="server" Text='<%# Eval("NODUES_STATUS")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblOverNoDuesStatus" runat="server" Text='<%# Eval("OVERALL_DUES_STATUS")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <%# Eval("REMARK")%>
                                        </td>
                                        <td>
                                            <%# Eval("UA_FULLNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("DEPTNAME")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>





                </div>



            </div>
        </div>





        <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
        <div id="divMsg" runat="server">
        </div>
    </asp:Panel>

    <script type="text/javascript" language="javascript">
        //        //To change the colour of a row on click of check box inside the row..
        //        $("tr :checkbox").live("click", function() {
        //        $(this).closest("tr").css("background-color", this.checked ? "#FFFFD2" : "#FFFFFF");
        //        });

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


        function CheckSelectionCount(chk) {
            var count = -1;
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (count == 2) {
                    chk.checked = false;
                    alert("You have reached maximum limit!");
                    return;
                }
                else if (count < 2) {
                    if (e.checked == true) {
                        count += 1;
                    }
                }
                else {
                    return;
                }
            }
        }



    </script>
</asp:Content>

