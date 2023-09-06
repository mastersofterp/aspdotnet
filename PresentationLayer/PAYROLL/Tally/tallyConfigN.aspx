<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="tallyConfigN.aspx.cs" Inherits="PAYROLL_Tally_tallyConfigN" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   
    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)
        function Cover(bottom, top, ignoreSize) {
            var location = Sys.UI.DomElement.getLocation(bottom);
            top.style.position = 'absolute';
            top.style.top = location.y + 'px';
            top.style.left = location.x + 'px';
            if (!ignoreSize) {
                top.style.height = bottom.offsetHeight + 'px';
                top.style.width = bottom.offsetWidth + 'px';
            }
        }
    </script>

    <script>
        $(document).ready(function () {
            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#tblSearchResults').DataTable({
            });
        }
    </script>


    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upDetails"
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
    <asp:UpdatePanel ID="upDetails" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Tally Configuration</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>College</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege1" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" ControlToValidate="ddlCollege1" Display="None" ErrorMessage="Please Select College" ValidationGroup="Submit"
                                            SetFocusOnError="True" Text="*"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Tally Server IP</label>
                                        </div>
                                        <asp:TextBox ID="txtServerIp" onkeydown="return (event.keyCode!=13);" runat="server" MaxLength="20" placeholder="Please Enter Tally server IP" TabIndex="1" CssClass="form-control">
                                        </asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbBankCode" runat="server"
                                            TargetControlID="txtServerIp"
                                            FilterType="Numbers,Custom"
                                            FilterMode="ValidChars"
                                            ValidChars=".">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvBankCode" runat="server" ControlToValidate="txtServerIp" Display="None" ErrorMessage="Please Enter Tally Server IP" ValidationGroup="Submit"
                                            SetFocusOnError="True" Text="*"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Port Number</label>
                                        </div>
                                        <asp:TextBox ID="txtPortNumber" onkeydown="return (event.keyCode!=13);" runat="server" placeholder="Please Enter Port Number" MaxLength="8" TabIndex="2" CssClass="form-control">
                                        </asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteBankName" runat="server"
                                            TargetControlID="txtPortNumber"
                                            FilterType="Custom,Numbers"
                                            FilterMode="ValidChars"
                                            ValidChars=".-_ ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvPortNumber" runat="server" ControlToValidate="txtPortNumber" Display="None" ErrorMessage="Please Enter Port Number" ValidationGroup="Submit"
                                            SetFocusOnError="True" Text="*"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Active</label>
                                        </div>
                                        <asp:CheckBox ID="chkIsActive" onkeydown="return (event.keyCode!=13);" runat="server" TabIndex="4" Checked="true" />

                                    </div>

                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" TabIndex="5" Text="Submit" ToolTip="Click to Save" ValidationGroup="Submit" OnClick="btnSubmit_Click" UseSubmitBehavior="false" CssClass="btn btn-primary progress-button" />
                                <%--<asp:Button ID="btnReport" runat="server" TabIndex="6" Text="Report" ToolTip="Click to Report" class="btn btn-info" OnClick="btnReport_Click" />--%>
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" class="btn btn-warning" TabIndex="7" Text="Cancel" ToolTip="Click to Cancel" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />

                            </div>

                            <div class="col-12 mb-4">
                                <asp:Panel ID="DivCompany" runat="server" Visible="false">
                                    <asp:ListView ID="Rep_Company" runat="server">
                                        <LayoutTemplate>
                                            <div id="listViewGrid" class="vista-grid">
                                                <div class="titlebar" style="display: none">
                                                    Tally Configuration
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>SR.NO.
                                                            </th>

                                                            <th>ACTION
                                                            </th>

                                                            <th>TALLY SERVER IP
                                                            </th>

                                                            <th>PORT NUMBER
                                                            </th>

                                                            <th>ACTIVE STATUS  
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <div id="itemPlaceholder" runat="server" />
                                                    </tbody>

                                                </table>
                                            </div>

                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </td>

                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" TabIndex="8" Text="EDIT"
                                                        CommandArgument='<%# Eval("TallyConfigId") %>' ToolTip="Edit Record"
                                                        ImageUrl="~/Images/edit.png" OnClick="btnEdit_click" />
                                                </td>

                                                <td>

                                                    <%# Eval("ServerName")%>
                                                                        
                                                </td>

                                                <td>

                                                    <%# Eval("PortNumber")%>
                                                                    
                                                </td>

                                                <td>
                                                    <%--<strong><%# Eval("ActiveStatus")%></strong>--%>
                                                    <asp:Label ID="lblActiveStatus" runat="server"
                                                        Text='<%# ((Eval("ActiveStatus")))%>'
                                                        ForeColor='<%#(Convert.ToBoolean(Eval("IsActive"))==true?System.Drawing.Color.DarkGreen:System.Drawing.Color.Red)%>'></asp:Label>
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
        </Triggers>
    </asp:UpdatePanel>




    <div id="divMsg" runat="server" />

</asp:Content>

