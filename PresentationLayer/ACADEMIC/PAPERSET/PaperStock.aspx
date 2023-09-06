<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PaperStock.aspx.cs" Theme="THEME1" Inherits="ACADEMIC_PaperStock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpaperStock"
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
    <asp:UpdatePanel ID="updpaperStock" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PAPER STOCK</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                            ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                         <asp:DropDownList ID="ddlSession" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                            AppendDataBoundItems="True" TabIndex="1" ToolTip="Please Select Session">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" ValidationGroup="Show" InitialValue="0"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" TabIndex="1" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="true" ToolTip="Please Select Semester" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" ValidationGroup="Show" InitialValue="0"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" TabIndex="1" runat="server" Text="Show"
                                    ToolTip="Click to show" OnClick="btnShow_Click" ValidationGroup="Show" CssClass="btn btn-primary" />
                                <asp:Button ID="btnClear" TabIndex="1" runat="server" ToolTip="Click to Clear"
                                    Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummaryShow" runat="server" DisplayMode="List"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />
                            </div>
                            <div class="col-12 mt-4">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvCourse" runat="server" OnItemDataBound="lvCourse_ItemDataBound">
                                        <LayoutTemplate>
                                            <div>
                                                <div class="sub-heading">
                                                    <h5>Courses</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <%-- <th>Scheme Type </th>--%>
                                                            <th>Degree </th>
                                                            <th>Code </th>
                                                            <th>Course </th>
                                                            <th>Available Quantity </th>
                                                            <th>Required Quantity </th>
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
                                                <%-- <td>
                                                         <asp:Label ID="lblSchemetype" runat="server" Text='<%# Eval("SCHEMETYPE") %>' ToolTip="Scheme Type" />
                                                     </td>--%>
                                                <td>
                                                    <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREENAME") %>' />
                                                    <asp:HiddenField runat="server" ID="hfBosLock" Value='<%# Eval("BOS_LOCK") %>' />
                                                    <asp:HiddenField runat="server" ID="hfDeanLock" Value='<%# Eval("DEAN_LOCK") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip="Cours code" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' ToolTip="Course name" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtQTY" runat="server" Enabled="true" MaxLength="1" TabIndex="6" Text='<%# Eval("QTY") %>' ToolTip="Enter Available Quantiy" CssClass="form-control"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtQTY" runat="server" FilterType="Numbers" TargetControlID="txtQTY">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtReorder" runat="server" Enabled="true" MaxLength="1" CssClass="form-control" TabIndex="7" Text='<%# Eval("REQ_LEVEL") %>' ToolTip="Enter Required Quantity"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtReorder" runat="server" FilterType="Numbers" TargetControlID="txtReorder">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12 mt-3 btn-footer">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click" TabIndex="1" Text="Save" ToolTip="Click to save" Visible="false" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="1" Text="Cancel" ToolTip="Click to cancel" Visible="false" />
                                <asp:HiddenField ID="hdftotalrows" runat="server" />

                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>








    <script type="text/javascript" language="javascript">
        function checkQty(txt) {
            if (txt != null && txt.value != "")
                if (Number(txt.value) == 0) {
                    alert("Quantity cannot be 0");
                    txt.value = "";
                }
            if (txt.value == "")
            { alert("Quantity cannot be blank"); }
        }

        function checkReq(txt) {
            if (txt != null && txt.value != "")
                if (Number(txt.value) == 0) {
                    alert("Requirement cannot be 0");
                    txt.value = "";
                }
            if (txt.value == "")
            { alert("Requirement cannot be blank"); }
        }


        function check(txt) {
            var hdftotalrows = document.getElementById('ctl00_ContentPlaceHolder1_hdftotalrows');

            for (var i = 0; i < Number(hdftotalrows.value) ; i++) {
                var txtQTY = document.getElementById('ctl00_ContentPlaceHolder1_lvCourse_ctrl' + i + '_txtQTY');
                var txtReorder = document.getElementById('ctl00_ContentPlaceHolder1_lvCourse_ctrl' + i + '_txtReorder');
                var lbl = document.getElementById('ctl00_ContentPlaceHolder1_lvCourse_ctrl' + i + '_lblCourseName');
                if (txt == txtQTY || txt == txtReorder)
                    if (Number(txtQTY.value) > Number(txtReorder.value))
                        alert("Quantity " + txtQTY.value + " cannot be greater than requirements " + txtReorder.value + "!");
            }
        }
    </script>

</asp:Content>
