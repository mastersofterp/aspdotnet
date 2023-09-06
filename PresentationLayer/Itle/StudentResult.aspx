<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentResult.aspx.cs" Inherits="Itle_StudentResult" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">TEST RESULT</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <asp:Panel ID="pnlStudentResult" runat="server">
                            <div class="row">
                                <div class="col-lg-5 col-md-6 col-12 mb-3">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Session  :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblSession" runat="server" Font-Bold="True" ForeColor="#006600"></asp:Label>
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-lg-7 col-md-12 col-12 mb-3">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Course Name  :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblCourseName" runat="server" Font-Bold="True" ForeColor="#006600"></asp:Label>
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                            </div>

                            <div class="row mt-3">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Test Type</label>
                                    </div>
                                    <asp:RadioButtonList ID="rbtTestType" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                        Font-Bold="true" OnSelectedIndexChanged="rbtTestType_SelectedIndexChanged">
                                        <asp:ListItem Value="O" Text="Objective" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="D" Text="Descriptive"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Select Course</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                        ValidationGroup="Select sessionno" AutoPostBack="true" ToolTip="Select Course Name"
                                        TabIndex="1" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label></label>
                                    </div>
                                    <asp:UpdatePanel ID="updTestReport" runat="server">
                                        <ContentTemplate>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Select Test</label>
                                    </div>
                                    <asp:DropDownList ID="ddlTest" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                        ValidationGroup="Select Schemeno" ToolTip="Select Test" TabIndex="2"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlTest_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Order by</label>
                                    </div>
                                    <asp:DropDownList ID="ddlOrderBy" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlOrderBy_SelectedIndexChanged" ToolTip="Select Order By" TabIndex="3">
                                        <asp:ListItem Text="Roll Number" Value="R" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Name" Value="N"></asp:ListItem>
                                        <asp:ListItem Text="Marks Obtained" Value="M"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label></label>
                                    </div>
                                    <asp:CheckBox ID="chkAbsentStudent" runat="server" AutoPostBack="true" TabIndex="4"
                                        OnCheckedChanged="chkAbsentStudent_CheckedChanged" ToolTip="Check To View Absent Student" />
                                    <b>View Absent Student</b>
                                </div>

                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-primary"
                                    OnClick="btnShowReport_Click" TabIndex="5" ToolTip="Click here to Show Report" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    ValidationGroup="Cancel Button" ToolTip="Click here to Reset"
                                    CssClass="btn btn-warning" TabIndex="6" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlResultList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvRewsult" runat="server">
                                        <LayoutTemplate>

                                            <div class="sub-heading">
                                                <h5>Test Result</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sr.No
                                                        </th>
                                                        <th>RRN
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Test Name
                                                        </th>
                                                        <th>Total Marks
                                                        </th>
                                                        <th>Marks Obtained
                                                        </th>
                                                        <th>Test Date
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
                                                    <%#Container.DataItemIndex + 1%>
                                                </td>

                                                <td>
                                                    <%# Eval("REGNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STUDNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TESTNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TOTALMARKS")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CORRECTMARKS")%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("TESTDATE", "{0:MMMM d yyyy,hh:mm:ss tt}") %>' />
                                                    <%--<%# Eval("TESTDATE")%>--%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </asp:Panel>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

        function totAllIDs(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }

        }
        //    function totAllIDs(headchk) {
        //      

        //        var frm = document.forms[0]
        //        for (i = 0; i < document.forms[0].elements.length; i++) {
        //            var e = frm.elements[i];
        //            if (e.type == 'checkbox') {
        //                if (e.name.endsWith('chkAccept')) {
        //                    if (headchk.checked == true) {
        //                        e.checked = true;
        //                        
        //                    }
        //                    else
        //                        e.checked = false;

        //                }
        //            }
        //        }


        //            var frm = document.forms[0]
        //            for (i = 0; i < document.forms[0].elements.length; i++) {
        //                var e = frm.elements[i];
        //                if (e.type == 'checkbox') {
        //                    if (headchk.checked == true)
        //                        e.checked = true;
        //                   
        //                    else
        //                        e.checked = false;
        //                }
        //            }



    </script>

    <%--  Enable the button so it can be played again --%>
</asp:Content>
