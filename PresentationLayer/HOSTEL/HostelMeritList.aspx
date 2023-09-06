<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="HostelMeritList.aspx.cs"
    Inherits="Hostel_Merit_List" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlExam"
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

    <asp:UpdatePanel ID="updpnlExam" runat="server">
        <ContentTemplate>
            <%--<script type="text/javascript">
            //On Page Load
            $(document).ready(function () {
                $('#table2').DataTable();
            });
    </script>--%>

            <script type="text/javascript">
                //On UpdatePanel Refresh
                //var prm = Sys.WebForms.PageRequestManager.getInstance();
                //if (prm != null) {
                //    prm.add_endRequest(function (sender, e) {
                //        if (sender._postBackSettings.panelsToUpdate != null) {
                //            $('#table2').dataTable();
                //        }
                //    });
                //};

                function CheckNumeric(event, obj) {
                    var k = (window.event) ? event.keyCode : event.which;
                    //alert(k);
                    if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                        obj.style.backgroundColor = "White";
                        return true;
                    }
                    if (k > 45 && k < 58) {
                        obj.style.backgroundColor = "White";
                        return true;

                    }
                    else {
                        alert('Please Enter numeric Value');
                        obj.focus();
                    }
                    return false;
                }
                onkeypress = "return CheckAlphabet(event,this);"
                function CheckAlphabet(event, obj) {

                    var k = (window.event) ? event.keyCode : event.which;
                    if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                        obj.style.backgroundColor = "White";
                        return true;

                    }
                    if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                        obj.style.backgroundColor = "White";
                        return true;

                    }
                    else {
                        alert('Please Enter Alphabets Only!');
                        obj.focus();
                    }
                    return false;
                }
            </script>


            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">HOSTEL MERIT LIST</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlSelect" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Hostel Session </label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                AutoPostBack="true" TabIndex="1" data-select2-enable="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                                ErrorMessage="Please Select Session" ValidationGroup="academic"
                                                SetFocusOnError="true" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Exam Session </label>
                                            </div>
                                            <asp:DropDownList ID="ddlExamSession" runat="server" AppendDataBoundItems="True" ValidationGroup="show"
                                                CssClass="form-control" TabIndex="2" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvExamSession" runat="server" ControlToValidate="ddlExamSession"
                                                Display="None" ErrorMessage="Please Select Exam Session" ValidationGroup="submit"
                                                SetFocusOnError="True" InitialValue="0" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Semester </label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="3" CssClass="form-control"
                                                AppendDataBoundItems="True" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvsemster" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" ErrorMessage="Please Select Semester" ValidationGroup="submit"
                                                SetFocusOnError="True" InitialValue="0" />

                                            <asp:DropDownList ID="ddlHostelNo" runat="server" CssClass="form-control" TabIndex="3" AppendDataBoundItems="True" Visible="false" data-select2-enable="true"></asp:DropDownList>
                                            <%--Below code commented by Saurabh L on 03/10/2022  --%>
                                            <%--<asp:RequiredFieldValidator ID="rfvddlHostelNo" runat="server" ControlToValidate="ddlHostelNo"
                                                Display="None" ErrorMessage="Please Select Hostel" ValidationGroup="submit"
                                                SetFocusOnError="True" InitialValue="0" Enabled="false" />--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Overwrite </label>
                                            </div>
                                            <asp:CheckBox ID="chkoverwrite" runat="server" Text="" TabIndex="4" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Hostel </label>
                                            </div>
                                            <div class="form-group col-md-12 checkbox-list-box">
                                                <asp:CheckBoxList ID="cblstHostel" runat="server" ToolTip="Click to Select Hostel" TabIndex="5"
                                                    RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style">
                                                </asp:CheckBoxList>
                                            </div>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Degree </label>
                                            </div>
                                            <div class="form-group col-md-12 checkbox-list-box">
                                                <asp:CheckBoxList ID="cblstDegree" runat="server" ToolTip="Click to Select Degree" TabIndex="6"
                                                    RepeatColumns="1" RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-12 btn-footer mt-4">
                                    <asp:Button ID="btnShow" runat="server" Text="Prepare Data" ValidationGroup="submit"
                                        OnClick="btnShow_Click" CssClass="btn btn-primary" TabIndex="7" />
                                    <asp:Button ID="btnRankList" runat="server" Text="Rank List Modification" CssClass="btn btn-info"
                                        OnClick="btnRankList_Click" TabIndex="8" />
                                    <asp:Button ID="btnCuttofRange" runat="server" Text="Cuttoff Range Report" TabIndex="9"
                                        ValidationGroup="submit" OnClick="btnCuttofRange_Click" CssClass="btn btn-info" />

                                    <asp:Button ID="btnList" Visible="false" runat="server" Text="Rank List Report" TabIndex="10" OnClick="btnList_Click" CssClass="btn btn-info" />

                                    <asp:Button ID="btnCan" runat="server" Text="Cancel" TabIndex="11" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="submit" />
                                </div>

                            </asp:Panel>

                            <div class="col-12 btn-footer">
                                <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                            </div>

                            <div id="trmodify" runat="server" visible="false">
                                <asp:ImageButton ID="imgbutBack" runat="server" ToolTip="Back" ImageUrl="~/IMAGES/arrow1.jpg" OnClick="btnBack_Click" />
                                <asp:Panel runat="server" ID="pnlrank" Height="600px" ScrollBars="Vertical">
                                    <asp:GridView ID="GridView8" CssClass="vista-grid" HeaderStyle-BackColor="ActiveBorder"
                                        runat="server" EmptyDataText="No Records Found" AutoGenerateColumns="false"
                                        OnRowCreated="GridView8_RowCreated" OnRowCommand="GridView8_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="RANK" HeaderText="RANK" />
                                            <asp:BoundField DataField="RECORDID" HeaderText="MODIFYSRNO" />
                                            <asp:BoundField DataField="REGNO" HeaderText="REGNO" />
                                            <asp:BoundField DataField="IDNO" HeaderText="IDNO" />
                                            <asp:BoundField DataField="STUDNAME" HeaderText="STUDNAME" />
                                            <asp:BoundField DataField="BRANCHNAME" HeaderText="BRANCHNAME" />
                                            <asp:BoundField DataField="CGPA" HeaderText="CGPA" />
                                            <asp:BoundField DataField="SGPA" HeaderText="SGPA" />
                                        </Columns>
                                    </asp:GridView>

                                </asp:Panel>

                                <div class="col-12 btn-footer">
                                    <asp:ImageButton ID="imgUpOrderBy" ToolTip="UP" runat="server" ImageUrl="~/IMAGES/uparrow.jpg"
                                        OnClick="imgUpOrderBy_Click" /><br />
                                    <asp:ImageButton ID="imgDownOrderBy" ToolTip="DOWN" runat="server" ImageUrl="~/IMAGES/downarrow.jpg"
                                        OnClick="imgDownOrderBy_Click" /><br />
                                    <asp:Button ID="Button2" runat="server" Text="Save" OnClick="Save_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="Button3" runat="server" Text="Report" OnClick="btnreport_Click" CssClass="btn btn-info" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Save" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>
</asp:Content>


