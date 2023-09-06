<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Phd_Thesis_Status_Master.aspx.cs" Inherits="ACADEMIC_PHD_Phd_Thesis_Status_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updStatus"
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

    <asp:UpdatePanel ID="updStatus" runat="server" UpdateMode="Conditional">
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
                                            <label>Academic Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAcademicBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Academic Batch" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="submit" ControlToValidate="ddlAcademicBatch" Display="None"
                                            ErrorMessage="Please Select Academic Batch." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Status Name</label>
                                        </div>
                                        <asp:TextBox ID="txtStatus" runat="server" TabIndex="2" CssClass="form-control"
                                            MaxLength="70" ToolTip="Please Enter Status Name" placeholder="Enter Status here." />
                                        <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="txtStatus"
                                            Display="None" ErrorMessage="Please Enter Status Name" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Sequence No</label>
                                        </div>
                                        <asp:TextBox ID="txtSeq" runat="server" TabIndex="3" CssClass="form-control"
                                            MaxLength="2" ToolTip="Please Enter Sequence No" placeholder="Enter Sequence No." />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSeq"
                                            Display="None" ErrorMessage="Please Enter Sequence No" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ajaxToolkit1" runat="server" TargetControlID="txtSeq" FilterMode="ValidChars"
                                            ValidChars="0123456789" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-6">
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

                            <div class="col-12" id="divstatuslist" runat="server">
                                <asp:Panel ID="pnlStatus" runat="server">
                                    <asp:ListView ID="lvStatus" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Status List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action</th>
                                                        <%-- <th>Grade Type</th>--%>
                                                        <th>Status Name</th>
                                                        <th>Academic Btach</th>
                                                        <th>Sequence No.</th>
                                                        <th>Active Status
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("STATUSNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <%# Eval("STATUSNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("BATCHNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SEQUENCE")%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="true" Text='<%# Eval("ACTIVESTATUS").ToString()=="1" ? "Active" :"Deactive" %>'
                                                        ForeColor='<%# Eval("ACTIVESTATUS").ToString()=="1" ? System.Drawing.Color.Green :System.Drawing.Color.Red %>'></asp:Label>
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
            if (document.getElementById("<%=ddlAcademicBatch.ClientID%>").value == '0') {
                alert("Please Select Academic Batch");
                document.getElementById("<%=ddlAcademicBatch.ClientID%>").focus();
                return false;
            }
            var txt = $('#ctl00_ContentPlaceHolder1_txtStatus').val();
            if (txt == "") {
                alert('Please Enter Status Name');
                return false
            }
            var txt = $('#ctl00_ContentPlaceHolder1_txtSeq').val();
            if (txt == "") {
                alert('Please Enter Sequence No');
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




