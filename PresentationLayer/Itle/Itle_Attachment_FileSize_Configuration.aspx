<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Itle_Attachment_FileSize_Configuration.aspx.cs" Inherits="Itle_Itle_Attachment_FileSize_Configuration"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        function InsertSymbol(divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";

            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
            }
        }

        function GetTotal() {

            debugger;
            var Total = document.getElementById('<%= txtFileSize.ClientID %>').value;


            if (Number(Total) > 10) {
                alert("Size Should Not be Greater than 10MB !");
                document.getElementById('<%= txtFileSize.ClientID %>').value = "";
            }

        }
    </script>
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updFileConfig"
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
    <asp:UpdatePanel ID="updFileConfig" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ATTACHMENT SIZE CONFIGURATIONS</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlAttachment" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="text-center">
                                                <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class=" note-div">
                                                <h5 class="heading">Steps To Edit</h5>
                                                <asp:Label ID="lblSteps" runat="server">
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Select User Type</span> </p>
                                                  <p><i class="fa fa-star" aria-hidden="true"></i><span>Select Page Name.</span> </p>
                                            
                                                  <p><i class="fa fa-star" aria-hidden="true"></i><span>Enter File Size</span> </p>
                                                </asp:Label>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>User Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlUserType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ValidationGroup="Select sessionno" ToolTip="Select User Type" TabIndex="1"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Admin</asp:ListItem>
                                                <asp:ListItem Value="2">Student</asp:ListItem>
                                                <asp:ListItem Value="3">Faculty</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trPageName" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>User Type</label>
                                            </div>
                                            <asp:Label ID="lblPageName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="form-group col-lg-2 col-md-4 col-6">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Enter File Size</label>
                                            </div>
                                            <asp:TextBox ID="txtFileSize" runat="server" CssClass="form-control" ToolTip="Enter File Size" MaxLength="2" onkeyup="javascript:GetTotal();"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-1 col-md-2 col-6">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:DropDownList ID="ddlSizeUnit" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                ValidationGroup="Select Schemeno" ToolTip="Select File Size" TabIndex="2" AutoPostBack="True">

                                                <%--<asp:ListItem Selected="True" Value="0">KB</asp:ListItem>--%>
                                                <asp:ListItem Value="1">MB</asp:ListItem>
                                                <%-- <asp:ListItem Value="2">GB</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Select Roll List Report Button"
                                                ToolTip="Click here to Show RollList under Selected Criteria" CssClass="btn btn-primary"
                                                TabIndex="4" OnClick="btnSubmit_Click" />
                                            <asp:Button ID="btnOpenWindow" runat="server" Text="Add New Page" OnClick="btnOpenWindow_Click"
                                                ToolTip="Click here to Show RollList under Selected Criteria" CssClass="btn btn-primary"
                                                TabIndex="7" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" ValidationGroup="Cancel Button" TabIndex="6"
                                                ToolTip="Click here to Cancel Field under Selected criteria" CssClass="btn btn-warning"
                                                OnClick="btnCancel_Click" />

                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSubmit" />
                                            <asp:PostBackTrigger ControlID="btnCancel" />
                                            <asp:PostBackTrigger ControlID="btnOpenWindow" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12" id="dvAddPages" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Add New Page</h5>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Select Module</label>
                                            </div>
                                            <asp:DropDownList ID="ddlModule" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                ValidationGroup="Select sessionno" ToolTip="Select Module" TabIndex="1"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Select Page</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPages" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                TabIndex="2" AutoPostBack="True" ToolTip="Select Page">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnAddPage" runat="server" Text="Add" ValidationGroup="Select Roll List Report Button"
                                            ToolTip="Click here to Add Page for Configuration" CssClass="btn btn-primary" TabIndex="4"
                                            OnClick="btnAddPage_Click" />
                                        <asp:Button ID="btnReset" runat="server" Text="Cancel" ValidationGroup="Cancel Button"
                                            ToolTip="Click here to Go to Back" CssClass="btn btn-warning" TabIndex="6"
                                            OnClick="btnReset_Click" />
                                    </div>
                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="pnlPageList" runat="server" Visible="false">

                                        <div class="sub-heading">
                                            <h5>Page Name</h5>
                                        </div>

                                        <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                            <asp:ListView ID="lvPageList" runat="server">
                                                <LayoutTemplate>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Action
                                                                </th>
                                                                <th>Page Name
                                                                </th>
                                                                <th>Size for Faculty
                                                                </th>
                                                                <th>Size for Students
                                                                </th>
                                                                <th>Size for Admin
                                                                </th>
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
                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit1.png"
                                                                OnClick="btnEdit_Click" CommandArgument='<%# Eval("AL_NO") %>' AlternateText="Edit Record"
                                                                ToolTip="Edit Record" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("AL_LINK")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("FILE_SIZE_FACULTY")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("FILE_SIZE_STUDENT")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("FILE_SIZE_ADMIN")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>

                                    </asp:Panel>
                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
