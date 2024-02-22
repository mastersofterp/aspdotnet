<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Faculty_Dairy_Certificate.aspx.cs" Inherits="ACADEMIC_Faculty_Dairy_Certificate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div>
        <asp:UpdateProgress ID="updProg" runat="server"
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

      
    <asp:UpdatePanel ID="updFaculty" runat="server">
        <ContentTemplate>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">II Generate Faculty Dairy Certificate</h3>
                </div>
                <div class="box-body mt-2">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12 ">
                                <div class="label-dynamic">
                                    <asp:Label ID="lblName" runat="server" Font-Bold="true" > Name : </asp:Label>
                                </div>
                                 <a class="sub-label">
                                <asp:Label ID="lblNameNew" runat="server" Text="Hello" CssClass="styleclass" ></asp:Label>
                                     </a>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <asp:Label ID="lblMobile" runat="server" Font-Bold="true"> Mobile No : </asp:Label>
                                </div>
                                 <a class="sub-label">
                                <asp:Label ID="lblMobileNew" runat="server" Text="Hello" CssClass="styleclass" ></asp:Label>
                                     </a>

                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <asp:Label ID="lblDept" runat="server" Font-Bold="true"> Department Name : </asp:Label>
                                </div>
                                 <a class="sub-label">
                                <asp:Label ID="lblDeptName" runat="server" Text="Hello" CssClass="styleclass" ></asp:Label>
                                     </a>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <asp:Label ID="lblClg" runat="server" Font-Bold="true"> College Name : </asp:Label>
                                </div>
                                <a class="sub-label">
                                <asp:Label ID="lblClgName" runat="server" Text="Hello" CssClass="styleclass" ></asp:Label>
                                    </a>
                            </div>
                        </div>
                    </div>

                    <div class="col-10 btn-footer">
                        <asp:Button ID="btnReport" runat="server" Text="Certificate" ToolTip="Submit"
                            CssClass="btn btn-primary" TabIndex="1" OnClick="btnReport_Click" />

                    </div>

                </div>
        </div>
    </div>
    <asp:HiddenField ID="TabName" runat="server" />
         </ContentTemplate>     
    </asp:UpdatePanel>

</asp:Content>

