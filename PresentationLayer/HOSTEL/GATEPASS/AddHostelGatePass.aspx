<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AddHostelGatePass.aspx.cs" Inherits="HOSTEL_GATEPASS_AddHostelGatePass" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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

        function CheckNumeric(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            //alert(k);
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k > 45 && k < 58) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter numeric Value');
                obj.focus();
            }
            return false;
        }


    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">GENERATE HOSTEL GATE PASS</h3>
                </div>
                <br />
                <div class="box-body">
                    <div class="col-6">
                        <div class="form-group col-lg-8 col-md-4 col-12">
                             <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Gate Pass Number</label>
                                </div>
                                <asp:TextBox ID="txtPass" runat="server" CssClass="form-control" TabIndex="1" onkeypress="return CheckNumeric(event,this);" MaxLength="8" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPass" runat="server" ErrorMessage="Please Enter Hostel Gate Pass Number."
                                    Display="None" ControlToValidate="txtPass" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                         </div>
                    </div>
                    <br />
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="submit" TabIndex="2"
                            CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnReport" runat="server" Text="Gate Pass Report" ValidationGroup="submit" TabIndex="3"
                            CssClass="btn btn-outline-success" Visible="false" OnClick="btnReport_Click"/>
                        &nbsp;<asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="submit" />
                        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-danger" OnClick="btnBack_Click" Text="Back" />
                    </div>
                </div>
            </div>
            <br />
            <asp:Panel ID="pnlinfo" runat="server" Visible="False">
            <div class="box box-primary">
                <div id="div2" runat="server" style="text-align:center;padding:9px;" >
                    <asp:Image ID="imgPhoto" runat="server" Height="120px" Width="128px" style="border-radius:50%;"/>
                </div>
                <div id="div3" runat="server" style="text-align:center;" >
                    <b><asp:Label ID="lblName" runat="server">Name</asp:Label></b>
                </div>
                <br />
                <div class="container">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-6 col-md-6 col-lg-6">
                                <%--<div class="col-6">  --%>
                                <div class="form-group col-lg-8 col-md-4 col-12">
                                    <div class="label-dynamic">
                                        <label style="font-size: medium;"><b>Admission Number</b></label>
                                    </div>
                                    <asp:Label ID="lbladno" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="form-group col-lg-8 col-md-4 col-12">
                                    <div class="label-dynamic">
                                        <label style="font-size: medium;"><b>Hostel Name</b></label>
                                    </div>
                                    <asp:Label ID="lblhostel" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="form-group col-lg-8 col-md-4 col-12">
                                    <div class="label-dynamic">
                                        <label style="font-size: medium;"><b>Room No</b></label>
                                    </div>
                                    <asp:Label ID="lblRoom" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="form-group col-lg-8 col-md-4 col-12">
                                    <div class="label-dynamic">
                                        <label style="font-size: medium;"><b>Valid From</b></label>
                                    </div>
                                    <asp:Label ID="lblvalFrom" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="form-group col-lg-8 col-md-4 col-12">
                                    <div class="label-dynamic">
                                        <label style="font-size: medium;"><b>Valid To</b></label>
                                    </div>
                                    <asp:Label ID="lblvalTo" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="form-group col-lg-8 col-md-4 col-12">
                                    <div class="label-dynamic">
                                        <label style="font-size: medium;"><b>Approved By & Date</b></label>
                                    </div>
                                    <asp:Label ID="lblapproval1" runat="server" Text="Label"></asp:Label><br />
                                    <asp:Label ID="lblapproval2" runat="server" Text="Label"></asp:Label><br />
                                    <asp:Label ID="lblapproval3" runat="server" Text="Label"></asp:Label><br />
                                    <asp:Label ID="lblapproval4" runat="server" Text="Label"></asp:Label><br />
                                </div>
                            </div>

                            <div class="col-6 col-md-6 col-lg-6">
                                <div class="form-group col-lg-8 col-md-4 col-12">
                                    <div class="label-dynamic">
                                        <label style="font-size: medium;"><b>Out Time Entry</b></label>
                                    </div>
                                    <asp:Label ID="lblOutTimeEntry" runat="server" ></asp:Label>
                                </div>
                                <div class="form-group col-lg-8 col-md-4 col-12">
                                    <div class="label-dynamic">
                                        <label style="font-size: medium;"><b>In Time Entry</b></label>
                                    </div>
                                    <asp:Label ID="lblInTimeEntry" runat="server" Text=" "></asp:Label>
                                </div>
                                <div class="form-group col-lg-8 col-md-4 col-12">
                                    <asp:RadioButtonList ID="rdoEntrySelection" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdoEntrySelection_SelectedIndexChanged" >
                                        <asp:ListItem Value="0">&nbsp;In Time Entry&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="1">&nbsp;Out Time Entry</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="form-group col-lg-8 col-md-4 col-12">
                                    <asp:Button ID="btnInTimeEntry" runat="server" Text="IN TIME ENTRY" TabIndex="4" Visible="false" 
                                        CssClass="btn btn-primary" OnClick="btnInTimeEntry_Click" />

                                    &nbsp;&nbsp;
                          <asp:Button ID="btnOutEntry" runat="server" CssClass="btn btn-primary" TabIndex="5" Visible="false"  Text="OUT TIME ENTRY" OnClick="btnOutEntry_Click" />
                                </div>


                            </div>

                        </div>
                    </div>
                </div>
            </div>
           </asp:Panel>

            <asp:Panel ID="pnlList" runat="server">
                <div class="box box-primary">
                
                        <asp:Repeater ID="lvPurpose" runat="server">
                            <HeaderTemplate>
                                <div class="sub-heading">
                                    <h5>List of Students</h5>
                                </div>
                                <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>RRN Number
                                            </th>
                                            <th>Student Name
                                            </th>
                                            <th>Gate Pass No
                                            </th>
                                            <th>Out Date
                                            </th>
                                            <th>In Date
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Eval("REGNO") %>
                                    </td>
                                    <td>
                                         <%# Eval("STUDNAME") %>
                                    </td>
                                    <td>
                                        <%# Eval("GATEPASSNO") %>
                                    </td>
                                    <td>
                                        <%# Eval("OUTDETAILS","{0:dd/MM/yyyy hh:mm tt}") %>
                                    </td>
                                    <td>
                                        <%# Eval("INDETAILS","{0:dd/MM/yyyy hh:mm tt}") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody></table>
                            </FooterTemplate>
                        </asp:Repeater>
                </div>
            </asp:Panel>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .form-control {}
    </style>
</asp:Content>



