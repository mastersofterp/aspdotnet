<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="SlotMaster.aspx.cs" Inherits="ACADEMIC_MASTERS_SlotMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updSlot"
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

    <asp:UpdatePanel ID="updSlot" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div id="div1" runat="server"></div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Slot Name</label>
                                        </div>
                                        <asp:TextBox ID="txtSlotName" runat="server" TabIndex="1" CssClass="form-control"
                                            MaxLength="70" ToolTip="Please Enter Slot Name " placeholder="Enter Slot Name here." />
                                        <asp:RequiredFieldValidator ID="rfvFeedbackName" runat="server" ControlToValidate="txtSlotName"
                                            Display="None" ErrorMessage="Please Enter Slot Name" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-6 col-6">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Active Status</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="chkActive" name="switch" checked />
                                            <label data-on="Active" data-off="Deactive" for="chkActive"></label>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                    TabIndex="3" OnClick="btnSave_Click" CssClass="btn btn-primary" OnClientClick="return getactivestatus();" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    TabIndex="4" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <asp:HiddenField ID="hdnActive" runat="server" ClientIDMode="Static" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlSlot" runat="server">
                                    <asp:ListView ID="lvSlots" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Slots List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action</th>
                                                        <%-- <th>Grade Type</th>--%>
                                                        <th>Slot Name</th>
                                                        <th>
                                                            Active Status
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("SLOTTYPENO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" OnClick="btnEdit_Click" />
                                                </td>
                                                <%-- <td>
                                                        <%# Eval("GRADE_TYPE")%>
                                                    </td>--%>
                                                <td>
                                                    <%# Eval("SLOTTYPE_NAME")%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" Font-Bold="true" Text='<%# Eval("ACTIVESTATUS").ToString()=="1" ? "Active" :"Deactive" %>'
                                                        ForeColor='<%# Eval("ACTIVESTATUS").ToString()=="1" ? System.Drawing.Color.Green :System.Drawing.Color.Red %>' ></asp:Label>
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

            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function getactivestatus() {
            var txt = $('#ctl00_ContentPlaceHolder1_txtSlotName').val();
            if (txt == "") {
                alert('Please enter Slot Name');
                return false
            }
            else {
                $('#hdnActive').val($('#chkActive').prop('checked'));
                return true;
            }
        }
        function SetActive(val) {
            $('[id*=chkActive]').prop('checked', val);
        }
    </script>
</asp:Content>


