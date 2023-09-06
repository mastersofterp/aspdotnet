<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="IndexScoreConfiguration.aspx.cs" Inherits="IndexScoreConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updSession" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Index Score Configuration</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                            </h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                            Display="None" ValidationGroup="indextest" InitialValue="0"
                                            ErrorMessage="Please Select Admission Batch"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAdmBatch"
                                            Display="None" ValidationGroup="indextest" InitialValue="Please Select"
                                            ErrorMessage="Please Select Admission Batch"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ValidationGroup="indextest" InitialValue="0"
                                            ErrorMessage="Please Select Degree"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ValidationGroup="indextest" InitialValue="Please Select"
                                            ErrorMessage="Please Select Degree"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <%--OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"--%>
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ValidationGroup="indextest" InitialValue="0"
                                            ErrorMessage="Please Select Branch"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ValidationGroup="indextest" InitialValue="Please Select"
                                            ErrorMessage="Please Select Branch"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Configuration Type</label>
                                        </div>
                                        <asp:RadioButtonList ID="rdoConfigType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdoConfigType_SelectedIndexChanged">
                                            <asp:ListItem Value="I" Selected="True"> Index Score &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="T"> Test Score </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Advantage Type</label>
                                        </div>
                                        <asp:RadioButtonList ID="rdoAdvType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdoAdvType_SelectedIndexChanged">
                                            <asp:ListItem Value="O" Selected="True"> Optional Subject &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="W"> Weightage Subject</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Max. Marks</label>
                                        </div>
                                        <asp:TextBox ID="txtMaxMarks" runat="server" onKeyUp="setMaxLength(this)" isMaxLength="3" MaxLength="3" min="0" max="999" onkeypress="return functionx(event)" type="number" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvftxtMaxMarks" runat="server" ControlToValidate="txtMaxMarks" Display="None" ErrorMessage="Please Enter Max Marks." ValidationGroup="indextest" SetFocusOnError="true">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-lg-6 col-md-6 col-12">
                                        <asp:Panel ID="pnlSubjectMarks" runat="server" Visible="false">
                                            <asp:ListView ID="lvSubjectMarks" runat="server">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divSubjectMarksList">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>SrNo</th>
                                                                <th>Subject</th>
                                                                <th>Marks</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <%# Container.DataItemIndex + 1 %>
                                                            <asp:HiddenField ID="hfdMarksSubNo" runat="server" Value='<%# Eval("SUBJECTNO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSubName" runat="server" Text='<%# Eval("SUBJECTNAME") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtMarks" runat="server" onKeyUp="setMaxLength(this)" isMaxLength="5" onkeypress="return functionx(event)" type="number" CssClass="form-control" Width="200px" MaxLength="5" min="0" Text='<%# Eval("MARKS") %>' ToolTip="Please Enter Marks" placeholder="Enter Marks"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="indextest" TabIndex="1" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                <%--OnClick="btnSubmit_Click"--%>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="1" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <%--OnClick="btnCancel_Click"--%>
                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="indextest"
                                    DisplayMode="List" ShowMessageBox="True" ShowSummary="false" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlIndexScoreConfigList" runat="server" Visible="true">
                                    <asp:ListView ID="lvIndexScoreConfigList" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divIndexScoreConfigList">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center;">Edit</th>
                                                        <th>Admission Batch</th>
                                                        <th>Degree</th>
                                                        <th>Branch</th>
                                                        <th>Configuration Type</th>
                                                        <th>Advantage Type</th>
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
                                                    <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                        CommandArgument='<%# Eval("ADMBATCHNO")+","+Eval("DEGREENO")+","+Eval("BRANCHNO")+","+Eval("CONFIGURATION_TYPE")+","+Eval("ADVANTAGE_TYPE")+","+Eval("MAX_MARKS")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click" TabIndex="6" />
                                                    <%--OnClick="btnEdit_Click"--%>                                                                 
                                                </td>
                                                <td>
                                                    <%# Eval("BATCHNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DEGREENAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("LONGNAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("CONFIGURATION_TYPE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("ADVANTAGE_TYPE") %>
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
            <asp:AsyncPostBackTrigger ControlID="ddlAdmBatch" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlDegree" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlBranch" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="rdoConfigType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="rdoAdvType" EventName="SelectedIndexChanged" />

            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>
    <script>
        function functionx(evt) {
            if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57)) {
                //alert("Enter Only Numeric Value ");
                return false;
            }
        }
        function setMaxLength(control) {
            //get the isMaxLength attribute
            var mLength = control.getAttribute ? parseInt(control.getAttribute("isMaxLength")) : ""

            //was the attribute found and the length is more than the max then trim it
            if (control.getAttribute && control.value.length > mLength) {
                control.value = control.value.substring(0, mLength);
                //alert('Length exceeded');
            }

            //display the remaining characters
            var modid = control.getAttribute("id") + "_remain";
            if (document.getElementById(modid) != null) {
                document.getElementById(modid).innerHTML = mLength - control.value.length;
            }
        }
    </script>
</asp:Content>

