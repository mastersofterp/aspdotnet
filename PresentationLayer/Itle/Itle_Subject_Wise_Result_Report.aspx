<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Itle_Subject_Wise_Result_Report.aspx.cs" Inherits="Itle_Itle_Subject_Wise_Result_Report"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">SUBJECT WISE RESULT REPORT</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="col-12">
                            <asp:Panel ID="pnl1" runat="server">
                                <asp:UpdatePanel ID="updTestResult" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Test Type</label>
                                                </div>
                                                <asp:RadioButtonList ID="rbtTestType" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                                    Font-Bold="true">
                                                    <asp:ListItem Value="O" Text="Objective" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="D" Text="Descriptive"></asp:ListItem>
                                                </asp:RadioButtonList>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Session</label>
                                                </div>
                                                <asp:DropDownList ID="Ddlsession" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                    OnSelectedIndexChanged="Ddlsession_SelectedIndexChanged" TabIndex="1"
                                                    CssClass="form-control" data-select2-enable="true" ToolTip="Select Session From the List">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="Ddlsession"
                                                    Display="None" ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Degree</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="2"
                                                    CssClass="form-control" data-select2-enable="true" ToolTip="Select Degree From the List">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="course">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Branch/Programmes</label>
                                                </div>
                                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="3"
                                                    ToolTip="Select Branch/Basic Course From the List">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="course">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="DropDownList2" runat="server" Visible="false" CssClass="form-control"
                                                    AppendDataBoundItems="true">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Regulation</label>
                                                </div>
                                                <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" TabIndex="4"
                                                    ToolTip="Select Regulations From the list">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Progam" ValidationGroup="course">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlHidden" runat="server" Visible="false" CssClass="form-control" AppendDataBoundItems="true">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Semester</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" TabIndex="5"
                                                    ToolTip="Select Semester From the List">
                                                    <%--<asp:ListItem Value="0"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="course">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Course</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="True" TabIndex="6" ToolTip="Select Course From the list">
                                                    <%--<asp:ListItem Value="0"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCourse"
                                                    Display="None" ErrorMessage="Please Select Course" InitialValue="0" ValidationGroup="course">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </div>

                        <div class="col-12 btn-footer">
                            <asp:UpdatePanel ID="upButton" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Show Result" TabIndex="7"
                                        OnClick="btnSubmit_Click" ToolTip="Click here to Submit" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="8"
                                        ValidationGroup="Cancel Button" ToolTip="Click here to Reset" CssClass="btn btn-warning" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>

                        <div class=" col-12">
                            <asp:HiddenField ID="hdnSession" runat="server" />
                            <asp:HiddenField ID="hdnCourse" runat="server" />
                            <asp:HiddenField ID="hdnDivision" runat="server" />
                            <asp:HiddenField ID="hdnSubjectType" runat="server" />
                            <asp:HiddenField ID="hdnSubject" runat="server" />
                            <asp:HiddenField ID="hdnTeacher" runat="server" />
                        </div>

                        <div class="col-12">
                            <asp:Panel ID="pnlReport" runat="server">
                                <asp:GridView ID="grdResultReport" runat="server"
                                    AlternatingRowStyle-BackColor="#FFFFAA" CssClass="vista-grid"
                                    HeaderStyle-BackColor="ActiveBorder" Height="10px">
                                </asp:GridView>
                            </asp:Panel>
                        </div>

                        <div class="col-12" id="trExcelButton" runat="server">
                            <div class="text-center">
                                <asp:ImageButton ID="imgbutExporttoexcel" runat="server" Height="50px"
                                    ImageUrl="~/images/excel.jpg" ToolTip="Export to excel" Width="50px" />

                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        //    function IDSelected(source, eventArgs) {
        //        var idno=eventArgs.get_value().split("*");
        //        var Name = idno[0].split("-");
        //        document.getElementById('ctl00_ContentPlaceHolder1_txtStudent_Id').value =Name;
        //        document.getElementById('ctl00_ContentPlaceHolder1_hdn1').value = Name;            
        //    }

        function GetnName(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            var Name = idno[0].split("-");
            document.getElementById('ctl00_ContentPlaceHolder1_txtStudent_Name').value = Name[0];
            document.getElementById('ctl00_ContentPlaceHolder1_txtStudent_Id').value = idno[1];
        }


        function GetIDNo(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            var Name = idno[0].split("-");
            document.getElementById('ctl00_ContentPlaceHolder1_txtStudent_Name').value = idno[1];
            document.getElementById('ctl00_ContentPlaceHolder1_txtStudent_Id').value = Name[0];
        }

    </script>

</asp:Content>
