<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TransportACFeeUpdation.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_TransportACFeeUpdation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updCollege"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>


    <div class="row">
        <div class="col-md-12">

            <asp:UpdatePanel ID="updCollege" runat="server">

                <ContentTemplate>
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h4 class="box-title">Transport AC Fee Updation</h4>
                            <div style="color: Red; font-weight: bold;">
                                Note : * Marked fields are mandatory
                            </div>
                            <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                AlternateText="Page Help" Visible="false" ToolTip="Page Help" />

                        </div>

                        <asp:Label ID="lblHelp" runat="server" Font-Bold="True" Style="color: Red"></asp:Label>

                        <!-- /.box-header -->
                        <!-- form start -->
                        <form role="form">
                            <div class="box-body">
                                <div class="form-group col-md-12">

                                    <div class="form-group col-md-12">
                                        <asp:Panel ID="Studpanel" runat="server">
                                            <fieldset>
                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-3">
                                                        <label><span style="color: red">*</span>Admission Batch</label>
                                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control">
                                                            <%--AutoPostBack="false" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged"--%>
                                                            <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAdmBatch"
                                                            Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" SetFocusOnError="True" ValidationGroup="Show">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label><span style="color: red;">*</span>Degree</label>
                                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="1" CssClass="form-control">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDegree"
                                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True" ValidationGroup="Show">
                                                        </asp:RequiredFieldValidator>

                                                    </div>

                                                    <div class="form-group col-md-3">
                                                        <label><span style="color: red;">*</span>Branch</label>
                                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="2" CssClass="form-control">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlBranch"
                                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True" ValidationGroup="Show">
                                                        </asp:RequiredFieldValidator>

                                                    </div>

                                                    <div class="form-group col-md-3">
                                                        <label><span style="color: red;">*</span>Semester</label>
                                                        <asp:DropDownList ID="ddlsemester" runat="server" AppendDataBoundItems="true" TabIndex="3" CssClass="form-control">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlsemester"
                                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True" ValidationGroup="Show">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>


                                                <div class="formm-group col-md-12">
                                                    <p class="text-center">
                                                        <asp:Button ID="btnShow" runat="server" CssClass="btn btn-info" TabIndex="12" Text="Show" ValidationGroup="Show" ToolTip="Show" OnClick="btnShow_Click" />
                                                        <asp:Button ID="btnSubmit" runat="server" Visible="false" CssClass="btn btn-primary" TabIndex="12" Text="Submit" ValidationGroup="ShowStudent" OnClick="btnSubmit_Click" ToolTip="Save Record" />
                                                        <asp:Button ID="btnReport" runat="server" CssClass="btn btn-info" TabIndex="12" Text="Report" ValidationGroup="ShowStudent" />
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" TabIndex="12" Text="Cancel" ToolTip="Clear Records" OnClick="btnCancel_Click" />
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                            ShowSummary="false" ValidationGroup="Show" />
                                                    </p>
                                                </div>
                                            </fieldset>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <!-- /.box-body -->
                                <div class="col-md-12">
                                    <asp:Panel ID="pnlstud" runat="server" Visible="false">
                                        <div class="col-md-12 table-responsive">
                                            <asp:ListView ID="lvStudent" runat="server">
                                                <EmptyDataTemplate>
                                                    <br />
                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Student To Shows" />
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <%-- <div id="demp_grid" class="vista-grid">--%>
                                                    <div class="titlebar">
                                                        <h3>Student List</h3>
                                                    </div>
                                                    <div class="box-tools pull-right">
                                                        <span style="color: red; text-align: right">Note : If AC Fee Is Applicable Then Click On Check Box, Else Directly Click On Submit Button &nbsp;&nbsp;&nbsp;</span>
                                                    </div>
                                                    <table class="table table-hover table-bordered" style="width:100%;margin-bottom:0px">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th style="width:6%">Sr.No.</th>
                                                                <%--100%">Temp No.</th>--%>
                                                                <th style="width:27%">Admission No.</th>
                                                                <th style="width:20%">Registration No.</th>
                                                                <th style="width:27%">Student Name</th>
                                                                <th style="width:20%">
                                                                    <asp:CheckBox ID="chkSelect1" runat="server" onclick="totAllSubjects(this)" />Transport AC Fee
                                                                </th>
                                                                <th></th>
                                                            </tr>
                                                        </thead>
                                                    </table>
                                                    <%-- </div>--%>
                                                    <div class="listview-container" style="overflow-y: scroll; overflow-x: hidden; height: 300px;width:100%">
                                                        <div id="Div1" class="vista-grid">
                                                            <table class="table table-hover table-bordered table-responsive">
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="item" ><%--onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'"--%>
                                                        <td style="width:6%">
                                                            <%# Container.DataItemIndex + 1 %>
                                                            <asp:HiddenField ID="hdnIdno" runat="server" Value='<%#Eval("IDNO")%>' />
                                                        </td>
                                                        <%--<td>
                                                            <asp:Label ID="lblIdno" runat="server" Text='<%#Eval("IDNO")%>'></asp:Label>
                                                           
                                                        </td>--%>
                                                        <td style="width:27%">
                                                            <asp:Label ID="lblAdmissionNo" runat="server" Text='<%#Eval("ENROLLNO")%>'></asp:Label>
                                                             
                                                        </td>
                                                        <td style="width:20%">
                                                            <asp:Label ID="lblRegNo" runat="server" Text='<%#Eval("REGNO")%>'></asp:Label>
                                                        </td >
                                                        <td style="width:27%">
                                                            <asp:Label ID="lblStudname" runat="server" Text='<%#Eval("STUDNAME")%>'></asp:Label>
                                                        </td>
                                                        <td style="width:20%">
                                                            <asp:CheckBox ID="chkSelect" runat="server" Checked='<%#Eval("TRANSPORT_WITH_AC").ToString() == "1" ? true : false %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </asp:Panel>
                                </div>


                            </div>
                        </form>
                        <br />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <script>
        function totAllSubjects(headchk) {
            debugger;
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
    <div id="divMsg" runat="server">
    </div>

</asp:Content>


