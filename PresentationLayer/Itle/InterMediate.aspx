<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="InterMediate.aspx.cs" Inherits="Itle_InterMediate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">ITLE SESSION STARTED</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlIntermediate" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>ITLE Session Details</h5>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Label ID="Label1" runat="server" Text="WELCOME TO ITLE WORKAREA" Font-Bold="True"
                                        Font-Size="24pt" ForeColor="#6666FF"></asp:Label>
                                </div>

                                <div class="form-group col-md-12">
                                    <div class="form-group col-md-12" runat="server" visible="false">
                                        <div class="text-right">
                                            <asp:Image ID="imgPhoto" runat="server"
                                                Style="height: 100px; width: 100px; border-width: 0px;" Height="20px" Width="40px" />
                                        </div>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <div class="form-group col-md-12">
                                            <div class="text-center">
                                                <label>Session Term :</label>
                                                <asp:Label ID="lblSession" runat="server" Font-Bold="True" ForeColor="#006600"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-12">
                                            <div class="text-center">
                                                <label>User Name :</label>
                                                <asp:Label ID="lblUserName" runat="server" Font-Bold="True" ForeColor="#006600"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-12">
                                            <div class="text-center">
                                                <label>Selected Course Name :</label>
                                                <asp:Label ID="lblCourseName" runat="server" Font-Bold="True"
                                                    ForeColor="#006600"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-12" runat="server" visible="false">
                                            <div class="text-center">
                                                <label>Country :</label>
                                                <asp:Label ID="lblCountry" runat="server" Font-Bold="True" ForeColor="#006600" col></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-12" runat="server" visible="false">
                                            <div class="text-center">
                                                <label>City/Town :</label>
                                                <asp:Label ID="lblCity" runat="server" Font-Bold="True" ForeColor="#006600"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-12" runat="server" visible="false">
                                            <div class="text-center">
                                                <label>Roll No :</label>
                                                <asp:Label ID="lblRollNo" runat="server" Font-Bold="True"
                                                    ForeColor="#006600"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-12" runat="server" visible="false">
                                            <div class="text-center">
                                                <label>Division :</label>
                                                <asp:Label ID="lblDivision" runat="server" Font-Bold="True" ForeColor="#006600" col></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-12" runat="server" visible="false">
                                            <div class="text-center">
                                                <label>Father Name :</label>
                                                <asp:Label ID="lblFatherName" runat="server" Font-Bold="True" ForeColor="#006600"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-12" runat="server" visible="false">
                                            <div class="text-center">
                                                <label>Mobile No :</label>
                                                <asp:Label ID="lblMobileNo" runat="server" Font-Bold="True"
                                                    ForeColor="#006600"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-12" runat="server" visible="false">
                                            <div class="text-center">
                                                <label>Email :</label>
                                                <asp:Label ID="lblEmail" runat="server" Font-Bold="True" ForeColor="#006600"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-12" runat="server" visible="false">
                                            <div class="text-center">
                                                <label>Course Profile :</label>
                                                <asp:Label ID="lblCourseProfile" runat="server" Font-Bold="True"
                                                    ForeColor="#006600" Width="500px"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group col-md-12">
                                    <asp:Panel ID="pnlFaculty" runat="server" Visible="false">
                                        <div class="form-group col-md-12">
                                            <p style="font-size: large; color: Red">Virtual Notice Board</p>
                                        </div>
                                        <div class="form-group col-md-12" id="tdFaculty" runat="server">
                                            <div class="panel panel-info">
                                                <div class="panel panel-heading">Announcement by Faculty</div>
                                                <div class="panel panel-body">
                                                    <div class="form-group col-md-12">
                                                        <asp:Panel ID="pnlAnnounce" runat="server" Height="300px" Width="100%" ScrollBars="Vertical">
                                                            <div style="height: 300px; width: 100%; overflow: auto">
                                                                <%=fMarquee%>
                                                                <%--<asp:Label ID="lblFacultyAnnounce" runat="server" ></asp:Label>--%>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
