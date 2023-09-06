<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="phd_BulkAnnexure.aspx.cs" Inherits="ACADEMIC_PHD_BulkAnnexure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .dynamic-nav-tabs li.active a {
            color: #255282;
            background-color: #fff;
            border-top: 3px solid #255282 !important;
            border-color: #255282 #255282 #fff !important;
        }

        .chiller-theme .nav-tabs.dynamic-nav-tabs > li.active {
            border-radius: 8px;
            background-color: transparent !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updbulkanne"
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
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>
                    </h3>
                </div>

                <div class="box-body">

                    <asp:UpdatePanel ID="updbulkanne" runat="server">
                        <ContentTemplate>
                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Admission Batch</label>
                                                <asp:DropDownList ID="ddlAdmBatch" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" runat="server" ErrorMessage="Please Select Admission Batch"
                                                    ControlToValidate="ddlAdmBatch" Display="None" SetFocusOnError="true" ValidationGroup="show" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue="0" runat="server" ErrorMessage="Please Select Admission Batch"
                                                    ControlToValidate="ddlAdmBatch" Display="None" SetFocusOnError="true" ValidationGroup="submit" />
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>School Applied for</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSchool" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" ToolTip="Please select school applied for" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ErrorMessage="Please select school applied for."
                                                ControlToValidate="ddlSchool" InitialValue="0" Display="None" SetFocusOnError="true" ValidationGroup="show" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" runat="server" ErrorMessage="Please select school applied for."
                                                    ControlToValidate="ddlAdmBatch" Display="None" SetFocusOnError="true" ValidationGroup="submit" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <%-- <sup>* </sup>--%>
                                                <label>PhD/Programme Applied For</label>
                                                <asp:DropDownList ID="ddlprogram" runat="server" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlprogram_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>              
                                        </div>


                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnshow" runat="server" TabIndex="4" Text="Show" CssClass="btn btn-primary" ValidationGroup="show" OnClick="btnshow_Click" />
                                    <asp:Button ID="btnSubmit" runat="server" ValidationGroup="submit" TabIndex="5" Text="submit" CssClass="btn btn-primary" Visible="false" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btncancel" runat="server" TabIndex="6" Text="Cancel" CssClass="btn btn-warning" OnClick="btncancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="show"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                                <asp:Panel ID="Panel3" runat="server" Visible="false">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Supervisor</label>
                                                </div>

                                                <asp:DropDownList ID="ddlSupervisor" runat="server" AppendDataBoundItems="True" TabIndex="13"
                                                    ToolTip="Please Select Supervisor" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" />
                                                <asp:CheckBox ID="CheckBox1" runat="server" Text="Is External" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="0" runat="server" ErrorMessage="Please Select Supervisor"
                                                    ControlToValidate="ddlSupervisor" Display="None" SetFocusOnError="true" ValidationGroup="submit" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Supervisor Role</label>
                                                </div>

                                                <asp:DropDownList ID="ddlSupervisorrole" runat="server" AutoPostBack="true" AppendDataBoundItems="True"
                                                    TabIndex="15" ToolTip="Please Select Supervisor role" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="S">Singly</asp:ListItem>
                                                    <asp:ListItem Value="J">Jointly</asp:ListItem>
                                                    <asp:ListItem Value="T">Multiple</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0" runat="server" ErrorMessage="Please Select Supervisor Role"
                                                    ControlToValidate="ddlSupervisorrole" Display="None" SetFocusOnError="true" ValidationGroup="submit" />
                                            </div>
                                        </div>
                                    </div>
                                    <asp:ListView ID="LvPhdBulk" runat="server" EnableModelValidation="True">
                                        <EmptyDataTemplate>
                                            <div>
                                                -- No Student Record Found --
                                            </div>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Search List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="chkAll" runat="server" onclick="SelectAll(this)" /></th>
                                                        <th>Student Name</th>
                                                        <th>Enrollment No </th>
                                                        <th>Supervisor</th>
                                                        <th>Supervisor Role</th>
                                                        <th><sup>* </sup>Area of Research</th>
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
                                                    <asp:CheckBox ID="chkallotment" runat="server" Enabled='<%# Convert.ToString(Eval("UANAME"))==string.Empty?true:false %>' />
                                                     <asp:HiddenField ID="hidIdNo" runat="server"
                                                       Value='<%# Eval("IDNO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblname" runat="server" Text='<% #Eval("STUDNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblenrollno" runat="server" Text='<% #Eval("ENROLLNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblsuper" runat="server" Text='<% #Eval("UANAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblrole" runat="server" Text='<% #Eval("SUPERROLE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtarea" runat="server" CssClass="unwatermarked" Rows="1" class="form-control" TextMode="MultiLine" Enabled='<%# Convert.ToString(Eval("RESEARCH"))==string.Empty?true:false %>' Text='<% #Eval("RESEARCH")%>'></asp:TextBox>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
    <script type="text/javascript" language="javascript">

        function SelectAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$LvPhdBulk$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$LvPhdBulk$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>
    <script type="text/javascript" language="javascript">

        function SelectAlloffer(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$LvOffer$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$LvOffer$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>

    

</asp:Content>
