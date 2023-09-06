<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Grand_Master.aspx.cs" Inherits="Stores_Masters_Grand_Master" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">GRANT MASTER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Add/Edit DEPARTMENT USER</h5>
                                </div>
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Grant Code</label>

                                            </div>
                                            <asp:TextBox ID="txtGrndCode" runat="server" class="form-control" TabIndex="1" ToolTip="Enter Grant Code" MaxLength="20" onKeyUp="LovercaseToUppercase(this);">
                                            </asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="ftbGrantCode" FilterType="Custom,Numbers,LowerCaseLetters,UpperCaseLetters"
                                                runat="server" Enabled="True" TargetControlID="txtGrndCode" FilterMode="InvalidChars"
                                                InvalidChars=".-_@!#$%^&* ">
                                            </cc1:FilteredTextBoxExtender>

                                            <asp:RequiredFieldValidator ID="rfvtxtGrndCode" runat="server" ControlToValidate="txtGrndCode" Display="None" ErrorMessage="Please Enter Grant Code" SetFocusOnError="True" ValidationGroup="store"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Grant Name</label>

                                            </div>
                                            <asp:TextBox ID="txtGrndName" runat="server" CssClass="form-control" MaxLength="50" onKeyUp="LovercaseToUppercase(this);" TabIndex="2" ToolTip="Enter Grant Name"> </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtGrndName" runat="server" ControlToValidate="txtGrndName" Display="None" ErrorMessage="Please Enter Grant Name" SetFocusOnError="True" ValidationGroup="store"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Grant Details</label>

                                            </div>
                                            <asp:TextBox ID="txtGrndDet" runat="server" CssClass="form-control" Height="50px" MaxLength="200" TextMode="MultiLine" TabIndex="3" ToolTip="Enter Grant Details"></asp:TextBox>
                                            <ajaxtoolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                TargetControlID="txtGrndDet"
                                                FilterType="Custom,Numbers,LowerCaseLetters,UpperCaseLetters"
                                                FilterMode="InvalidChars"
                                                InvalidChars=".-_@!#$%^&*">
                                            </ajaxtoolkit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvtxtGrndDet" runat="server" ControlToValidate="txtGrndDet" Display="None" ErrorMessage="Please Enter Grant Details" SetFocusOnError="True" ValidationGroup="store"></asp:RequiredFieldValidator>
                                        </div>
                                </asp:Panel>

                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="butSubmit" runat="server" OnClick="butSubmit_Click" Text="Submit" TabIndex="4" ToolTip="Click To Submit" ValidationGroup="store" CssClass="btn btn-primary" />
                                <asp:Button ID="btnshowrpt" runat="server" Text="Report" TabIndex="5" ToolTip="Click To Show Report " Visible="true" CssClass="btn btn-info" OnClick="btnshowrpt_Click"/>
                                <asp:Button ID="butCancel" runat="server" OnClick="butCancel_Click" Text="Cancel" TabIndex="6" ToolTip="Click To Reset" CssClass="btn btn-warning" />
                                
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="store" />

                            </div>
                        </div>

                        <asp:Panel ID="pnlTaxMaster" runat="server">
                            <div class="col-12 table-responsive">
                                <asp:ListView ID="lvGrandMaster" runat="server" OnPreRender="lvGrandMaster_PreRender">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" />

                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div id="demo-grid">
                                            <div class="sub-heading">
                                                <h5>GRANT MASTER</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action </th>
                                                        <th>Grant Code </th>
                                                        <th>Grant Name </th>
                                                        <th>Grant Details </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>

                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CommandArgument='<%# Eval("GRANDNO") %>' ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                &nbsp; </td>
                                            <td><%# Eval("GRAND_CODE")%></td>
                                            <td><%# Eval("GRAND_NAME")%></td>
                                            <td><%# Eval("GRAND_DETAILS")%></td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CommandArgument='<%# Eval("GRANDNO") %>' ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                &nbsp; </td>
                                            <td><%# Eval("GRAND_CODE")%></td>
                                            <td><%# Eval("GRAND_NAME")%></td>
                                            <td><%# Eval("GRAND_DETAILS")%></td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>

                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>

        </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="btnshowrpt" />
        </Triggers>

    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>

    <script type="text/javascript">

        function LovercaseToUppercase(txt) {
            var textValue = txt.value;
            txt.value = textValue.toUpperCase();

        }

    </script>
</asp:Content>
