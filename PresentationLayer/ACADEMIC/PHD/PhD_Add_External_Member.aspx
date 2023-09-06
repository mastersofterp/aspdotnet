<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhD_Add_External_Member.aspx.cs" Inherits="ACADEMIC_PHD_PhD_Add_External_Member"  Title="" MasterPageFile="~/SiteMasterPage.master"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function Validate() {
            var alerMsg = "";
            var name = document.getElementById('<%=txtName.ClientID%>');
            var institute = document.getElementById('<%=txtInstituteName.ClientID%>');
            var mobile = document.getElementById('<%=txtMobile.ClientID%>');
            var email = document.getElementById('<%=txtEmail.ClientID%>');
            var desig = document.getElementById('<%=txtDesig.ClientID%>');
            if (name.value == "" || institute.value == "" || mobile.value == "" || email.value == "" || desig.value == "")
            {
                if (name.value == "")
                {
                    alerMsg = "Please enter name.\n";
                }
                if (institute.value == "") {
                    alerMsg += "Please enter institute name.\n";
                }
                if (mobile.value == "") {
                    alerMsg += "Please enter mobile no.\n";
                }
                if (email.value == "") {
                    alerMsg += "Please enter email id.\n";
                }
                if (desig.value == "") {
                    alerMsg += "Please enter designation.\n";
                }
                alert(alerMsg);
                return false;
            }
        }
    </script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updDist" DynamicLayout="true" DisplayAfter="0">
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
    <asp:UpdatePanel ID="updDist" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Name</label>
                                        </div>
                                        <asp:TextBox ID="txtName" runat="server" ToolTip="Please enter name." TabIndex="1" MaxLength="128" AutoComplete="off" placeholder="Please enter name" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ajtbeName" runat="server" TargetControlID="txtName" FilterMode="InvalidChars" InvalidChars="~`!@#$%^&*()_-+={[}]:;<,>?/'|\"></ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Institute Name</label>
                                        </div>
                                        <asp:TextBox ID="txtInstituteName" runat="server" ToolTip="Please enter institute name." TabIndex="1" MaxLength="128" AutoComplete="off" placeholder="Please enter institute name" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ajtbeInstituteName" runat="server" TargetControlID="txtInstituteName" FilterMode="InvalidChars" InvalidChars="~`!@#$%^&*()_+={[}]:;<,>?'|\"></ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Mobile No</label>
                                        </div>
                                        <asp:TextBox ID="txtMobile" runat="server" ToolTip="Please enter mobile no." TabIndex="1" MaxLength="10" AutoComplete="off" placeholder="Please enter mobile no." CssClass="form-control"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMobile" FilterMode="ValidChars" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Email Id</label>
                                        </div>
                                        <asp:TextBox ID="txtEmail" runat="server" ToolTip="Please enter email id." TabIndex="1" MaxLength="64" AutoComplete="off" placeholder="Please enter email id" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtEmail" FilterMode="InvalidChars" InvalidChars="~`!#$%^&*()-+={}][:;'<,>?/|\"></ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Designation</label>
                                        </div>
                                        <asp:TextBox ID="txtDesig" runat="server" ToolTip="Please enter designation." TabIndex="1" MaxLength="128" AutoComplete="off" placeholder="Please enter designation" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtDesig" FilterMode="InvalidChars" InvalidChars="~`!#$%^&*()_+={}][:;'<,>?|\"></ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <p class="text-center">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" ToolTip="Click to save." TabIndex="1" OnClick="btnSave_Click" OnClientClick="return Validate(this);" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click to cancel." OnClick="btnCancel_Click" TabIndex="1" />
                                </p>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlMember" runat="server" Visible="false">
                                    <asp:ListView ID="lvMember" runat="server">
                                        <LayoutTemplate>
                                              <div class="sub-heading">
                                                    <h5>External Member List</h5>
                                                </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center;">Edit
                                                            </th>
                                                         <th>Name
                                                            </th>
                                                            <th>Institute Name
                                                            </th>
                                                            <th>Mobile No
                                                            </th>
                                                         <th>Email Id
                                                            </th>
                                                         <th>Designation
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
                                                <td style="text-align: center;">
                                                    <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" ImageUrl="~/images/edit.png"
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="1" CommandArgument='<%#Eval("DESIG_NO")%>' />
                                                </td>
                                                <td>
                                                    <%#Eval("NAME") %>
                                                </td>
                                                 <td>
                                                    <%#Eval("INSTITIUTE_NAME") %>
                                                </td>
                                                 <td>
                                                    <%#Eval("MOBILE_NO") %>
                                                </td>
                                                <td>
                                                    <%#Eval("EMAIL_ID") %>
                                                </td>
                                                <td>
                                                    <%#Eval("DESIGNATION") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
