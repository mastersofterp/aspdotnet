<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TP_Category_Updation.aspx.cs" Inherits="TP_Category_Updation" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updcategory"
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
    <style>
        .note-div {
            border: 1px solid #d9d0d0;
        }
    </style>
    <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
    <asp:UpdatePanel ID="updcategory" runat="server" Visible="true">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">T&P APPLICATION CATEGORY</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-8 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note </h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Application Category Available Only From <asp:Label runat="server" ID="lblsemester"></asp:Label> Semester Student Onwards.  </span></p>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Application Category Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlApplicationType" runat="server"
                                            CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Placement</asp:ListItem>
                                            <asp:ListItem Value="2">Entrepreneurship</asp:ListItem>
                                            <asp:ListItem Value="3">Higher Education</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>


                                </div>
                            </div>

                         
                                <div class="col-12 btn-footer mt-3">
                                    <asp:Button ID="btnTPSubmitType" runat="server" Text="Submit" ToolTip="Submit" CssClass="btn btn-primary" OnClick="btnTPSubmitType_Click" OnClientClick="return confirm('Are you sure you want to Submit !');" />
                                    <asp:Button ID="btnTpchangeRequest" runat="server" Text="Modify Category" ToolTip="Modify Category" CssClass="btn btn-primary" OnClick="btnTpchangeRequest_Click" OnClientClick="return confirm('Are you sure you want to Modify the Category !');"  Visible="false"/>
                                    <asp:Button ID="btnTPCancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="btn btn-warning" OnClick="btnTPCancel_Click" Style="margin-left: 10px" Visible="false" />

                                </div>


                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


    <div id="divMsg" runat="server">
    </div>


</asp:Content>

