<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentTransportStatus.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_StudentTransportStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../jquery/jquery-3.2.1.min.js"></script>
    <link href="../../jquery/jquery.multiselect.css" rel="stylesheet" />
    <script src="../../jquery/jquery.multiselect.js"></script>--%>
    <script type="text/javascript">
        window.onload = function () {
            document.getElementById('<%=txtscearcheno.ClientID%>').focus();
        }
        function Clear() {
            document.getElementById('<%=txtscearcheno.ClientID%>').value = '';
            document.getElementById('<%=lblBranch.ClientID%>').innerText = '';
            document.getElementById('<%=lblDegree.ClientID%>').value = '';
            document.getElementById('<%=lblsem.ClientID%>').value = '';
            document.getElementById('<%=lblStatus.ClientID%>').value = '';
            document.getElementById('<%=lblstudentname.ClientID%>').value = '';
            document.getElementById('<%=lblyear.ClientID%>').value = ''
            document.getElementById('<%=rdostatus.ClientID%>').value = "0";
        }
    </script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UPDSTUDENT"
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
    <asp:UpdatePanel ID="UPDSTUDENT" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">STUDENT TRANSPORT STATUS</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                        <div class="input-group date">
                                            <asp:TextBox ID="txtscearcheno" runat="server" class="form-control" placeholder="Search" OnTextChanged="txtscearcheno_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <span class="input-group-addon" data-target="#myModal2" data-toggle="modal">

                                                <i class="fa fa-search"></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblstudentname" runat="server" Text=""></asp:Label>

                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Degree:</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDegree" runat="server" Text=""></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Branch :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblBranch" runat="server" Text=""></asp:Label>

                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Year :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblyear" runat="server" Text=""></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Semester :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblsem" runat="server" Text=""></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Status :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>

                            </div>
                            <div class="form-group col-sm-12 mt-3">
                                <asp:RadioButtonList ID="rdostatus" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0" Text="Transport Cancel &nbsp&nbsp" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Transport Apply &nbsp&nbsp"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Transport Exempted"></asp:ListItem>
                                </asp:RadioButtonList>

                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnsubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnsubmit_Click" />
                                <asp:Button ID="btnclear" runat="server" CssClass="btn  btn-warning" Text="Clear" OnClick="btnclear_Click" />
                            </div>
                            <div class="col-12 mt-3">
                                <asp:ListView ID="LvStudent" runat="server">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <div class="sub-heading">
                                                <h5>Student Transport Status List</h5>
                                            </div>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action</th>
                                                    <th>Enrollment No.</th>
                                                    <th>Student Name</th>
                                                    <%--<th style="width: 15%">Transaction Date</th>--%>
                                                    <th>Status</th>
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
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                    CommandArgument='<%# Eval("ENROLLNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                    OnClick="btnEdit_Click" /></td>
                                            <td><%# Eval("ENROLLNO") %></td>
                                            <td><%# Eval("STUDNAME") %></td>
                                            <%-- <td style="width: 15%"><%# Eval("S_CANCELED_DATE","{0:dd/MM/yyyy}") %></td>--%>
                                            <td><%# Eval("TRANSPORT") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

