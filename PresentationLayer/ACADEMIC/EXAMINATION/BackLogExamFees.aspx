<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BackLogExamFees.aspx.cs" Inherits="BackLogExamFees" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="updBacklogExamFees" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <%--     <div id="div1" runat="server"></div>--%>
                        <div class="box-header with-border">
                            <h4>BACKLOG EXAM FEES CONFIG</h4>
                            <div class="box-tools pull-right">
                                <div style="color: Red; font-weight: bold">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>
                            </div>
                        </div>

                        <form role="form">
                            <div class="box-body">
                                <%--    <div class="col-md-3" style="text-align: right">
                                    

                                </div>--%>

                                <div class="col-md-3">
                                    <label><span style="color: red">* </span>BackLog Per Paper Fees</label>
                                    <asp:TextBox ID="txtPerPaperFees" runat="server" MaxLength="4" class="form-control" placeholder="Enter Per Paper Fees" onkeypress="javascript:return isNumber(event)" /><%-- class="form-control" --%>
                                    <asp:RequiredFieldValidator ID="rfvPerPaperFees" runat="server" ControlToValidate="txtPerPaperFees"
                                        Display="None" ErrorMessage="Please Enter Per Paper Fees" SetFocusOnError="true"
                                        ValidationGroup="Submit" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                        ControlToValidate="txtPerPaperFees" runat="server"
                                        ErrorMessage="Only Numbers allowed"
                                        ValidationExpression="\d+">
                                    </asp:RegularExpressionValidator>

                                    <br />

                                </div>


                                <div class="col-md-3">
                                    <label><span style="color: red">* </span>BackLog More Than Papers</label>
                                    <asp:TextBox ID="txtNoOfPapers" runat="server" MaxLength="4" class="form-control" placeholder="Enter No of Papers" onkeypress="javascript:return isNumber(event)" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNoOfPapers"
                                        Display="None" ErrorMessage="Please Enter No of Paper" SetFocusOnError="true"
                                        ValidationGroup="Submit" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                        ControlToValidate="txtNoOfPapers" runat="server"
                                        ErrorMessage="Only Numbers allowed"
                                        ValidationExpression="\d+">
                                    </asp:RegularExpressionValidator>
                                </div>

                                <%-- <div class="col-md-3" style="text-align: right">
                                    
                                </div>--%>

                                <div class="col-md-3">
                                    <label><span style="color: red">* </span>More Than Papers Fees</label>
                                    <asp:TextBox ID="txtMoreFees" runat="server" MaxLength="4" class="form-control" placeholder="Enter Max Paper Fees " onkeypress="javascript:return isNumber(event)" />
                                    <asp:RequiredFieldValidator ID="rfvPaperNumber" runat="server" ControlToValidate="txtMoreFees"
                                        Display="None" ErrorMessage="Please Enter More Than Paper Fees " SetFocusOnError="true"
                                        ValidationGroup="Submit" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                        ControlToValidate="txtMoreFees" runat="server"
                                        ErrorMessage="Only Numbers allowed"
                                        ValidationExpression="\d+">
                                    </asp:RegularExpressionValidator>
                                </div>


                            </div>

                            <div class="box-footer">
                                <p class="text-center">
                                    <div class="col-md-12 text-center">
                                        <%--text-center--%>

                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click"
                                            class="btn btn-success" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                            class="btn btn-danger" />
                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Submit" />
                                    </div>

                                </p>
                            </div>

                        </form>
                    </div>

                </div>


            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">

        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }



    </script>


</asp:Content>

