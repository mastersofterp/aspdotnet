<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="DocumentMapping.aspx.cs" Inherits="ACADEMIC_DocumentMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<div style="z-index: 1; position: absolute; top: 10px; left: 550px;">--%>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updGradeEntry"
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
    <%--</div>--%>
    <script src="../Content/jquery.js" type="text/javascript"></script>

    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <script type="text/javascript">
        function ConfirmToDelete() {
            return confirm("Are You Sure Want to Delete This Entry?");

        }
    </script>

    <asp:UpdatePanel ID="updGradeEntry" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><strong>DOCUMENT MAPPING</strong> </h3>
                            <div class="pull-right">
                                <div style="color: Red; font-weight: bold">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>
                            </div>
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Admission Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAdm" runat="server" TabIndex="1"
                                                AppendDataBoundItems="True"
                                                ToolTip="Please Select Admission Type." AutoPostBack="true" OnSelectedIndexChanged="ddlAdm_SelectedIndexChanged" CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Regular</asp:ListItem>
                                                <asp:ListItem Value="2">Lateral</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdm"
                                                Display="None" ErrorMessage="Please Select Admission Type." ValidationGroup="submit"
                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Degree</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="1"
                                                AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                                ToolTip="Please Select Degree" CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" ErrorMessage="Please Select Degree." ValidationGroup="submit"
                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Document</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDocument" runat="server" TabIndex="2"
                                                AppendDataBoundItems="True"
                                                ToolTip="Please Select Document" CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDocument"
                                                Display="None" ErrorMessage="Please Select Document." ValidationGroup="submit"
                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                             <div class="label-dynamic">
                                                <%--<sup>* </sup>--%>
                                                <label> Status</label>
                                            </div>
                                            <div style="margin-top:10px">
                                            <asp:CheckBox ID="chkActive"  runat="server" ToolTip="Check To Active." TabIndex="1" Text="&nbsp;Active"/> 
                                                </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                        CssClass="btn btn-primary" TabIndex="3" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                        CssClass="btn btn-warning" TabIndex="4" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </div>
                        </form>
                        <div class="box-footer">
                            <p class="text-center">

                                <div class="col-md-12">
                                    <asp:ListView ID="lvlist" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                <h5>Document Mapping Details</h5>
                                            </div>
                                                <table class="table table-hover table-bordered">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th style="text-align: center;">Sr.No</th>
                                                            <th>Admission Type</th>
                                                          <%--  <th style="/*text-align: center;*/">Action </th>--%>
                                                            <th>Degree-Document Mapping </th>
                                                            <th>Status</th>
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
                                                <td style="text-align: center;"><%#Container.DataItemIndex+1 %></td>
                                                <td><%# Eval("ADM_TYPE") %> </td>
                                              <%--  <td style="text-align: center;">
                                                    <asp:ImageButton ID="btnDel" runat="server" AlternateText="Delete Record" CommandArgument='<%# Eval("DOC_DEGREE_NO") %>' ImageUrl="~/IMAGES/delete.gif" OnClick="btnDel_Click" OnClientClick="return ConfirmToDelete(this);" TabIndex="6" ToolTip='<%# Eval("DOC_DEGREE_NO") %>' />
                                                </td>--%>
                                                <td><%# Eval("DEGREENAME") %>- <%# Eval("DOCUMENTNAME") %></td>
                                                <td>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("ACTIVE_STATUS") %>' ForeColor='<%# Eval("ACTIVE_STATUS").ToString().Equals("Active")?System.Drawing.Color.Green: System.Drawing.Color.Red %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                            </p>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

