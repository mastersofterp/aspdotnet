<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EmpLabTest.aspx.cs" Inherits="Health_LaboratoryTest_EmpLabTest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <div>
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
    </div>--%>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">EMPLOYEE LAB TEST</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlEmpTest" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Employee Name</label>
                                            </div>
                                            <asp:TextBox ID="txtPatientName" runat="server" CssClass="form-control"
                                                MaxLength="100" Enabled="false" ToolTip="Employee Name"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Visit Date</label>
                                            </div>
                                            <asp:DropDownList ID="ddlVisitDate" runat="server" AutoPostBack="true" TabIndex="2"
                                                AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Visit Date"
                                                OnSelectedIndexChanged="ddlVisitDate_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvVDt" runat="server" SetFocusOnError="true" Display="None"
                                                ErrorMessage="Please select visit date." ControlToValidate="ddlVisitDate"
                                                ValidationGroup="Submit" InitialValue="0"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-6 col-md-6 col-12" id="trDependent" runat="server" visible="false">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Dependent Name</label>
                                                    </div>
                                                    <asp:Label ID="lblDeptName" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Reference By</label>
                                                    </div>
                                                    <asp:Label ID="lblRefBy" runat="server" Text=""></asp:Label>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="col-12 mt-3">
                                <asp:Panel ID="pnlEmpTestList" runat="server">
                                    <asp:ListView ID="lvObservation" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Employee Lab Test List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>TEST TITLE
                                                            </th>
                                                            <th>REFERENCE BY
                                                            </th>
                                                            <th>TEST SAMPLE DATE
                                                            </th>
                                                            <th>VISITING DATE
                                                            </th>
                                                            <th>COMMON REMARK
                                                            </th>
                                                            <th>PRINT
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
                                                    <%# Eval("TITLE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DRNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TEST_SAMPLE_DT", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("OPDDATE", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("COMMON_REMARK")%>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnPrint" runat="server" Text="Print" CommandArgument='<%# Eval("OBSERNO") %>'
                                                        OnClick="btnPrint_Click" CssClass="btn btn-outline-info" ToolTip="Click here to Print" />

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
    </asp:UpdatePanel>

</asp:Content>

