<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="UploadOtherFees.aspx.cs" Inherits="ACADEMIC_UploadOtherFees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updotherfees"
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

    <asp:UpdatePanel ID="updotherfees" runat="server">
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
                                            <%--<label>College</label>--%>
                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" ToolTip="Please Select School/Institute." AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" ValidationGroup="submit" TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select School/Institute." InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="excel">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" data-select2-enable="true" TabIndex="1" ToolTip="Select Session">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" SetFocusOnError="true" ValidationGroup="excel"
                                            InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>

                                        <asp:DropDownList ID="ddlsemester" runat="server" AppendDataBoundItems="True" data-select2-enable="true" TabIndex="2" ToolTip="Select Semester">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlsemester"
                                            Display="None" ErrorMessage="Please Select Semester" SetFocusOnError="true" ValidationGroup="excel"
                                            InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Reciept Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlrecieptcode" runat="server" TabIndex="3" data-select2-enable="true" ToolTip="Select Reciept Type" AppendDataBoundItems="True">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Fee Head</label>
                                        </div>
                                        <asp:DropDownList ID="ddlFeehead" runat="server" AppendDataBoundItems="True" data-select2-enable="true" TabIndex="4" ToolTip="Select Fee Head">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlFeehead"
                                            Display="None" ErrorMessage="Please Select Fee Head" SetFocusOnError="true" ValidationGroup="excel"
                                            InitialValue="0" />

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Upload File</label>
                                        </div>
                                        <asp:FileUpload ID="fuUploadexcel" runat="server" ToolTip="Upload File" TabIndex="5" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="fuUploadexcel"
                                            Display="None" ErrorMessage="Please Upload File" SetFocusOnError="true" ValidationGroup="excel" />
                                    </div>


                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" TabIndex="6"
                                    ValidationGroup="excel" CssClass="btn btn-outline-primary" />
                                <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click" CssClass="btn btn-success margin" TabIndex="7"
                                    Text="Download Blank Excel Sheet" ToolTip="Click to download blank excel format file" Enabled="true"><i class="fa fa-file-excel-o"></i> Download Blank Excel Sheet</asp:LinkButton>
                                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" TabIndex="8" CssClass="btn btn-outline-danger" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="excel" Style="text-align: center" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlStudent" runat="server">
                                    <asp:ListView ID="lvStudent" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr class="bg-light-blue">
                                                        <th>Sr No.</th>
                                                        <th>Registration No</th>
                                                        <th>Amount
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
                                                <td><%# Container.DataItemIndex +1 %></td>
                                                <td>
                                                    <asp:Label ID="lblRegno" runat="server" Text='<%# Eval("RegistrationNo")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount")%>'></asp:Label>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
