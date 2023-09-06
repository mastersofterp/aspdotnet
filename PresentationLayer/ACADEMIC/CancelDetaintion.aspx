<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="CancelDetaintion.aspx.cs" Inherits="ACADEMIC_CancelDetaintion" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPnl"
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

    <asp:UpdatePanel ID="updPnl" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlCourse" runat="server">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div id="div1" runat="server"></div>
                            <div class="box-header with-border">
                                <%-- <h3 class="box-title">CANCEL DETENTION</h3>--%>
                                <h3 class="box-title">
                                    <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                            </div>

                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" TabIndex="1"
                                                OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname"
                                                Display="None" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="show">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                                ErrorMessage="Please Select College" ControlToValidate="ddlClgname" SetFocusOnError="True"
                                                ValidationGroup="canreport" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" AppendDataBoundItems="true" ValidationGroup="canreport" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="rfvSess" runat="server" Display="None" ErrorMessage="Please Select Session" ControlToValidate="ddlSession" SetFocusOnError="True" ValidationGroup="canreport" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSem" runat="server" AutoPostBack="true" AppendDataBoundItems="true" ValidationGroup="canreport" TabIndex="3" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                                Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="rfvSemesterno" runat="server" Display="None" ErrorMessage="Please Select Semester" ValidationGroup="canreport" ControlToValidate="ddlSem" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Registration No</label>--%>
                                                <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:TextBox ID="txtStudent" runat="server" Visible="true" AutoComplete="off"
                                                TabIndex="4" />
                                            <asp:RequiredFieldValidator ID="rfvRegNo" runat="server" ControlToValidate="txtStudent"
                                                Display="None" ErrorMessage="Please Enter Registration No." ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" TabIndex="5" Text="Show"
                                        ValidationGroup="show" CssClass="btn btn-info" OnClick="btnShow_Click" />
                                    <asp:Button ID="btnReport" runat="server" Text="Report"
                                        Visible="true" TabIndex="6" ValidationGroup="canreport" CssClass="btn btn-primary" OnClick="btnReport_Click" />
                                    <asp:Button ID="btnCan" runat="server" Text="Cancel" OnClick="btnCan_Click"
                                        Visible="true" TabIndex="7" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="vsStud" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="show" />

                                    <asp:ValidationSummary ID="vsReports" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="canreport" />
                                </div>
                                <div class="col-12" id="divInfo" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Student Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblStudName" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                    :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblRegNo" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b><asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label> :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblDegree" runat="server" Text="" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b><asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label> :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblBranch" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b><asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label> :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblScheme" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Mobile No. :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblStudMobile" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>E-Mail ID  :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblEmail" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>

                                            </ul>
                                        </div>

                                    </div>
                                </div>
                                <br />
                                <div class="col-12" id="divNote" runat="server" style="color: Red; font-weight: bold;" visible="false">
                                    Note :This Student is Detained in All Courses, click on Submit button to cancel the detention.
                                </div>
                                <div class="col-12 btn-footer" id="divCheck" runat="server" style="color: Red; font-weight: bold;" visible="false">
                                    Note :Please Check the CheckBox For Cancel Detention
                                </div>
                                <br />
                                <div class="col-12">
                                    <asp:ListView ID="lvDetend" runat="server" OnItemDataBound="lvDetend_ItemDataBound" Style="margin-top: -33px;">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Detention Course List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr id="Tr1" runat="server">
                                                        <%-- <th style="text-align: center; width: 10%;">Reg No. </th>
                                                        <th style="text-align: left; width: 25%;">Student Name </th>--%>
                                                        <th style="text-align: left; width: 25%;"><asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label> </th>
                                                        <th style="text-align: left; width: 25%;">Provisional Detention Faculty </th>
                                                        <th style="text-align: left; width: 25%;">Final Detention Faculty </th>
                                                        <th style="text-align: center; width: 10%;">Final Detention Status </th>
                                                        <th style="text-align: center; width: 5%;">Cancel Detention </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>

                                                <td style="text-align: left; width: 25%;">
                                                    <asp:Label ID="lblCourseNo" runat="server" Text='<%# Eval("COURSE") %>' ToolTip='<%# Eval("COURSENO") %>' />

                                                </td>
                                                <td style="text-align: left; width: 25%;">
                                                    <asp:Label ID="lbaprofac" runat="server" Text='<%# Eval("PROV_FKLTY") %>' ToolTip='<%# Eval("PROV_FKLTY") %>' />
                                                </td>
                                                <td style="text-align: left; width: 25%;">
                                                    <asp:Label ID="lblfinalfac" runat="server" Text='<%# Eval("FINAL_FKLTY") %>' ToolTip='<%# Eval("FINAL_FKLTY") %>' />
                                                </td>
                                                <td style="text-align: center; width: 10%;">
                                                    <asp:Label ID="lblProv" runat="server" Text='<%# Eval("FINALDETAIN") %>' ToolTip='<%# Eval("FINAL_DETAIN")%>' />
                                                </td>
                                                <td style="text-align: center; width: 5%;">
                                                    <asp:CheckBox ID="chkAccept" runat="server" ToolTip='<%# Eval("IDNO") %>' />

                                                    <%--Enabled='<%#(Convert.ToInt32(Eval("FINAL_DETAIN"))==1 ? false : true)%>'--%></td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr>

                                                <td style="text-align: left; width: 25%;">
                                                    <asp:Label ID="lblCourseNo" runat="server" Text='<%# Eval("COURSE") %>' ToolTip='<%# Eval("COURSENO") %>' />
                                                </td>
                                                <td style="text-align: left; width: 25%;">
                                                    <asp:Label ID="lbaprofac" runat="server" Text='<%# Eval("PROV_FKLTY") %>' ToolTip='<%# Eval("PROV_FKLTY") %>' />
                                                </td>
                                                <td style="text-align: left; width: 25%;">
                                                    <asp:Label ID="lblfinalfac" runat="server" Text='<%# Eval("FINAL_FKLTY") %>' ToolTip='<%# Eval("FINAL_FKLTY") %>' />
                                                </td>
                                                <td style="text-align: center; width: 10%;">
                                                    <asp:Label ID="lblProv" runat="server" Text='<%# Eval("FINALDETAIN") %>' ToolTip='<%# Eval("FINAL_DETAIN")%>' />
                                                </td>
                                                <%-- <td>
                                                    <%--  <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("COURSE") %>' ToolTip='<%# Eval("COURSENO")%>' />--%><%--<asp:HiddenField ID="hdnCourseno" runat="server" Value='<%# Eval("COURSENO")%>' />--%></td>
                                                <td style="text-align: center; width: 5%;">
                                                    <asp:CheckBox ID="chkAccept" runat="server" ToolTip='<%# Eval("IDNO") %>' />

                                                    <%--Enabled='<%#(Convert.ToInt32(Eval("FINAL_DETAIN"))==1 ? false : true)%>'--%></td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                    </asp:ListView>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" OnClientClick="return validation();" TabIndex="6" Text="Submit" ValidationGroup="show" Visible="false" Width="109px" />
                                        <%--<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="7" Text="Cancel" Visible="false" />--%>
                                        <asp:HiddenField ID="hdcount" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
                <div id="divMsg" runat="server">
                </div>
            </asp:Panel>

            <script type="text/javascript">

                function FreezeScreen(msg) {
                    scroll(0, 0);
                    var outerPane = document.getElementById('FreezePane');
                    var innerPane = document.getElementById('InnerFreezePane');
                    if (outerPane) outerPane.className = 'FreezePaneOn';
                    if (innerPane) innerPane.innerHTML = msg;
                }

                function showConfirm() {
                    var ret = confirm('Are You Sure Want To Cancel Detention?');
                    if (ret == true) {
                        FreezeScreen('Please Wait, Your Data is Being Processed...');
                        validate = true;
                    }
                    else
                        validate = false;
                    return validate;
                }

                function validation() {
                    var hdncount = document.getElementById('<%=hdcount.ClientID%>');
                    var count = 0;
                    for (var i = 0; i < hdncount.value; i++) {
                        var chk = document.getElementById('ctl00_ContentPlaceHolder1_lvDetend_ctrl' + i + '_chkAccept');
                        if (chk.type == 'checkbox') {
                            if (chk.checked) {
                                count++;
                            }
                        }
                    }
                    if (count > 0) {
                        var ret = confirm('Are You Sure To Cancel Selected Student Detention?');
                        if (ret == true)
                            return true;
                        else
                            return false;
                    }
                    else {
                        //alert('Please Check Student for Cancel Detention');
                        //return false;
                    }
                }
            </script>
            <%--     <script type="text/javascript">

                         function FreezeScreen(msg) {
                             scroll(0, 0);
                             var outerPane = document.getElementById('FreezePane');
                             var innerPane = document.getElementById('InnerFreezePane');
                             if (outerPane) outerPane.className = 'FreezePaneOn';
                             if (innerPane) innerPane.innerHTML = msg;
                         }

                         function showConfirm() {
                             var ret = confirm('Do you Really want to Confirm');
                             if (ret == true)
                             {
                                 var lsv = document.getElementById('ctl00_ContentPlaceHolder1_lvDetend');
                                 if(lsv != null)
                                 {
                                     var unchecked = false;
                                     for (var i = 0; i < lsv.length - 1; i++)
                                     {
                                         lsv

                                     }
                                 }
                 
                                 FreezeScreen('Please Wait, Your Data is Being Processed...');
                                 validate = true;
                             }
                             else
                                 validate = false;
                             return validate;
                         }
                    </script>--%>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>


