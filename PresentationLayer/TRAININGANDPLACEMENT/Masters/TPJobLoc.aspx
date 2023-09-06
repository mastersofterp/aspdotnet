<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TPJobLoc.aspx.cs" Inherits="TRAININGANDPLACEMENT_Masters_TPJobLoc" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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

    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">JOB LOCATION</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Job Location</label>
                                        </div>
                                        <asp:TextBox ID="txtJobloc" runat="server" MaxLength="100" class="form-control" placeholder="Enter Job Location" AutoComplete="off" />
                                        <asp:RequiredFieldValidator ID="rfvJobloc" runat="server" ControlToValidate="txtJobloc"
                                            Display="None" ErrorMessage="Please enter Job Locaction" SetFocusOnError="true"
                                            ValidationGroup="Submit" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtJobloc"
                                            InvalidChars="0123456789`~!@#$%^&*_+={[}}|\:;'?/" FilterMode="InvalidChars">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit"
                                    OnClick="btnSubmit_Click" class="btn btn-primary" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    class="btn btn-warning" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" />
                            </div>

                            <div class="col-12">
                                <asp:ListView ID="lvJobLoc" runat="server">
                                    <LayoutTemplate>
                                        <div id="demo-grid">
                                            <div class="sub-heading">
                                                <h5>JOB LOCATION LIST</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit
                                                        </th>
                                                        <th>Job Loc No.
                                                        </th>
                                                        <th>Job Location
                                                        </th>
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
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                    CommandArgument='<%# Eval("JOBLOCNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                    OnClick="btnEdit_Click" />
                                            </td>
                                            <td>
                                                <%# Eval("JOBLOCNO")%>
                                            </td>
                                            <td>
                                                <%# Eval("JOBLOCATION")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                    CommandArgument='<%# Eval("JOBLOCNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                    OnClick="btnEdit_Click" />
                                            </td>
                                            <td>
                                                <%# Eval("JOBLOCNO")%>
                                            </td>
                                            <td>
                                                <%# Eval("JOBLOCATION")%>
                                            </td>

                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

