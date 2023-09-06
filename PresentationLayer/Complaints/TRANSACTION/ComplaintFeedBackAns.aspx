<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ComplaintFeedBackAns.aspx.cs"
     Inherits="Complaints_TRANSACTION_ComplaintFeedBackAns" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function chk(txt) {

            var answer = confirm('Do you want to lock answers ?');
            if (answer == true) {
                return true;
            }
            else {
                return false;
            }
        }

    </script>

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">COMPLAINT FEEDBACK</h3>
                </div>
                <div class="box-body">
                    <div class="col-md-12">
                        <asp:Panel ID="pnl" runat="server">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    Complaint Feedback
                                </div>
                                <div class="panel-body">
                                    <div class="form-group row">
                                        <div class="col-md-12">
                                            <div id="divstud" runat="server" visible="false" style="color: Red">All questions are mandetory</div>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnlchkinfo" runat="server" Visible="false">
                                        <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Reg. No.:</label>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtSearch" runat="server" MaxLength="10" ToolTip="Please Enter Student Registration No. "
                                                        Font-Bold="True" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtSearch"
                                                        ErrorMessage="Please Enter User Id" SetFocusOnError="True" ValidationGroup="Search"
                                                        Display="None"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="Search"
                                                        OnClick="btnSearch_Click" CssClass="btn btn-info" />
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Search" />
                                                </div>
                                            </div>
                                        </asp:Panel>


                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnlStudInfo" runat="server" Visible="false">
                                                <div class="form-group row">
                                                    <div class="col-md-4">
                                                        <label>Name :</label>
                                                    </div>
                                                    <div class="col-md-8">
                                                        <asp:Label ID="lblName" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-4">
                                                        <label>Session:</label>
                                                    </div>
                                                    <div class="col-md-8">
                                                        <asp:Label ID="lblSession" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group row" id="Tr1" runat="server" visible="false">
                                                    <div class="col-md-4">
                                                        <label>Scheme:</label>
                                                    </div>
                                                    <div class="col-md-8">
                                                        <asp:Label ID="lblScheme" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-4">
                                                        <label>Degree:</label>
                                                    </div>
                                                    <div class="col-md-8">
                                                        <asp:Label ID="lblDegree" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-4">
                                                        <label>Branch:</label>
                                                    </div>
                                                    <div class="col-md-8">
                                                        <asp:Label ID="lblBranch" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-4">
                                                        <label>Semester:</label>
                                                    </div>
                                                    <div class="col-md-8">
                                                        <asp:Label ID="lblSemester" runat="server" Font-Bold="true"></asp:Label>
                                                        <%--&nbsp;&nbsp; &nbsp;&nbsp;
                                                 Section :&nbsp;<asp:Label ID="lblSection" runat="server" Font-Bold="true"></asp:Label>--%>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-4">
                                                        <label>Complaint No.:</label>
                                                    </div>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Course"
                                                            ValidationGroup="Submit" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ErrorMessage="Please Select Course"
                                                            ControlToValidate="ddlCourse" Display="None" InitialValue="-1" SetFocusOnError="True"
                                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="rfvCourseR" runat="server" ErrorMessage="Please Select Course"
                                                            ControlToValidate="ddlCourse" Display="None" InitialValue="0" SetFocusOnError="True"
                                                            ValidationGroup="Report"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <%--<tr>
                        <td> 
                            Section :
                        </td>
                            <td style="width: 353px">
                                <asp:DropDownList ID="ddlSection" runat="server"  
                                    ToolTip="Please Select Section"  Width="65%" 
                                    AutoPostBack="True" 
                                    onselectedindexchanged="ddlSection_SelectedIndexChanged"> 
                                    <asp:ListItem Value="0">No Section</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>--%>
                                                <div class="form-group row">
                                                    <%--   <td>
                                                     Teacher :
                                                      </td>--%>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList ID="ddlTecher" runat="server" Visible="false" AppendDataBoundItems="True" ToolTip="Please Select Teacher"
                                                            ValidationGroup="Submit" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlTecher_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Teacher"
                                                            ControlToValidate="ddlTecher" Display="None" InitialValue="0" SetFocusOnError="True"
                                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Teacher"
                                                            ControlToValidate="ddlTecher" Display="None" InitialValue="0" SetFocusOnError="True"
                                                            ValidationGroup="Report"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <%--<tr>
                            <td></td>
                            <td>
                               <b> <asp:Label ID="lblTecher" runat="server" ></asp:Label></b>
                            </td>
                            </tr>--%>
                                                <asp:Panel ID="Panel3" runat="server">
                                                    <div class="form-grooup row" id="stuInfo" runat="server" visible="false">
                                                        <div class="form-group row">
                                                            <div class="col-md-3">
                                                                <label>Complaint Date:</label>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:Label ID="lblComplaintDate" runat="server" Text=""></asp:Label>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <label>Complaint:</label>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:Label ID="lblComplaint" runat="server" Text=""></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <div class="col-md-3">
                                                                <label>Complainer Name:</label>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:Label ID="lblCompName" runat="server" Text=""></asp:Label>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <label>Project Incharge:</label>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:Label ID="lblfaculty" runat="server" Text=""></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <div class="form-group row">
                                                    <div class="col-md-12">
                                                        <asp:RadioButtonList ID="rblist1" runat="server">
                                                            <asp:ListItem Text="Excellent" Value="1" />
                                                            <asp:ListItem Text="Good" Value="2" />
                                                            <asp:ListItem Text="Average" Value="3" />
                                                            <asp:ListItem Text="Poor" Value="4" />
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <asp:Image ID="imgPhoto" runat="server" Width="96 px" Height="110px" />
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <asp:Panel ID="pnlSubject" runat="server" BorderColor="#CDCDCD" BorderStyle="Solid"
                                                        BorderWidth="1px" Width="100%" ScrollBars="Vertical" Height="150 px" ToolTip="FeedBack Course List">
                                                        <asp:ListView ID="lvSelected" runat="server" Visible="false">
                                                            <LayoutTemplate>
                                                                <table class="table table-bordered table-hover">
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <%# Eval("COURSENAME")%> for <%# Eval("FACULTY")%>
                                                                        <asp:Label ID="lblfac" runat="server" Visible="false" Text=' <%# Eval("FACULTY")%>'></asp:Label>
                                                                    </td>
                                                                    <td>

                                                                        <asp:CheckBox ID="chkCourseNO" runat="server" Visible="false" ToolTip='<%# Eval("COURSENO") %>'
                                                                            Enabled="false" />
                                                                        <asp:CheckBox ID="chkElectiveNo" runat="server" Visible="false" ToolTip='<%# Eval("ELECTIVENO") %>'
                                                                            Enabled="false" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblComplete" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-md-12">
                                                <label>Any suggestion along with what do you want to learn or general comment about the complaint or the complaint instructor.</label>
                                                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="form-control" OnTextChanged="txtRemark_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-md-12 text-center">
                                                <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                                <%-- <asp:Button ID="btnLock" runat="server" Text="Lock" OnClientClick="return chk(this);"  onclick="btnLock_Click"
                                                    ValidationGroup="Submit" /> --%>
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancel_Click" />
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <asp:Label ID="lblMsg" runat="server" Visible="false">
                                                 <span id="spMsg" style="color:Red;" ></span>
                                            </asp:Label>
                                            <div class="col-md-10">
                                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" CssClass="btn btn-info" Text="Report" ValidationGroup="Report" Visible="false" />
                                                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" CssClass="btn btn-warning" Text="Cancel" Visible="false" />
                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Report" />
                                            </div>
                                        </div>
                                        <asp:Panel ID="pnlMsg" runat="server" Visible="false">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <span style="font-size: large; color: Red;"><b>Teacher Not Allot!! You Cann't Give FeedBack!<br />
                                                        Please Contact Administrator! </b></span>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </asp:Panel>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

