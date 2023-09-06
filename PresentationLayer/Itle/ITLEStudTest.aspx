<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ITLEStudTest.aspx.cs" Inherits="Itle_ITLEStudTest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ONLINE TEST</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                <asp:Panel ID="pnlStudentTest" runat="server">
                                    <div class="col-md-12">
                                        <asp:Panel ID="pnlTest" runat="server">
                                            <div class="panel panel-info">
                                                <div class="panel panel-heading">Select Test</div>
                                                <div class="panel panel-body">
                                                    <div class="form-group">
                                                        <div class="col-md-12">
                                                            <div class="col-md-2">
                                                                <label>Session&nbsp;&nbsp;:</label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-md-12">
                                                            <div class="col-md-2">
                                                                <label>Course Name&nbsp;&nbsp;:</label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:Label ID="lblCourse" runat="server" Font-Bold="True"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="form-group col-md-12">
                                                            <div class="col-md-2">
                                                                <label><span style="color: Red">*</span>Select Test Type&nbsp;&nbsp;:</label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:DropDownList ID="ddlTestType" runat="server" CssClass="form-control" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlTestType_SelectedIndexChanged" ToolTip="Select Test Type" Enabled="false">
                                                                    <asp:ListItem Text="Please Select" Value="0" ></asp:ListItem>
                                                                    <asp:ListItem Text="Objective" Selected="True" Value="O"></asp:ListItem>
                                                                    <asp:ListItem Text="Descriptive" Value="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-md-12">
                                                            <div class="col-md-2">
                                                                <label></label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:HiddenField ID="HF1" Value="014373149466545347614:dygl7dqicp4" runat="server" />
                                                                <asp:HiddenField ID="HF2" Value="FORID:9" runat="server" />
                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning"
                                                                    OnClick="btnCancel_Click" ToolTip="Click here to Reset" />
                                                            </div>
                                                        </div>
                                                        <div>
                                                           <%--  <asp:Button ID="Button1" runat="server" Text="sdd" CssClass="btn btn-warning"
                                                                    OnClick="Button1_Click" ToolTip="Click here to Reset" />--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-12" id="DivTestList" runat="server" visible="false">
                                            <div class="row" style="border: solid 1px #CCCCCC">
                                                <div style="font-weight: bold; background-color: #72A9D3; color: white" class="panel-heading">Test List</div>
                                                <div class="table-responsive">
                                                    <table class="customers">
                                                        <tr style="font-weight: bold; background-color: #808080; color: white">
                                                            <th style="width: 6%; padding-left: 8px; text-align: left">Test Name</th>
                                                            <th style="width: 3%; text-align: left">Total Question</th>
                                                            <th style="width: 3%; text-align: left">Test Duration</th>
                                                            <th style="width: 3%; text-align: left">Attempts Allowed</th>
                                                            <th style="width: 4%; text-align: left">Test Time</th>
                                                            <th style="width: 4%; text-align: left">Test Type</th>
                                                            <th style="width: 4%; text-align: left">Start Date</th>
                                                            <th style="width: 4%; text-align: left">End Date</th>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="DocumentList">
                                                    <asp:Panel ID="pnlTestList" runat="server" ScrollBars="Vertical" Height="300px" BackColor="#FFFFFF">
                                                        <asp:ListView ID="lvTest" runat="server" DataKeyNames="TESTNO" OnSelectedIndexChanged="lvTest_SelectedIndexChanged">
                                                            <LayoutTemplate>
                                                                <div id="demo-grid">
                                                                    <table class="table table-bordered table-hover">
                                                                        <thead>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="width: 10%; padding-left: 8px;">
                                                                        <asp:LinkButton ID="btnlnkSelect" runat="server" Text='<%# Eval("TESTNAME")%>'
                                                                            CommandName='<%# Eval("TESTNO") %>' CommandArgument='<%# Eval("TESTNO") %>' ToolTip='<%# Eval("TEST_TYPE")%>'
                                                                            OnClick="btnlnkSelect_Click" OnClientClick="return confirm('Are you sure you want to start this Test?');"></asp:LinkButton>
                                                                    </td>
                                                                    <td style="width: 5%; text-align: left">
                                                                        <%# Eval("TOTALQUE")%>
                                                                    </td>
                                                                    <td style="width: 4%; text-align: left">
                                                                        <%# Eval("TESTDURATION")%>
                                                                    </td>
                                                                    <td style="width: 5%; text-align: center">
                                                                        <%# Eval("ATTEMPTS")%>
                                                                    </td>
                                                                    <td style="width: 7%; text-align: left">
                                                                        <%# Eval("STARTDATE", "{0:hh:mm:ss tt}")%>
                                                                        -
                                                                        <%# Eval("ENDDATE", "{0:hh:mm:ss tt}")%>
                                                                    </td>
                                                                    <td style="width: 5%; text-align: left">
                                                                        <%# Eval("TEST_TYPE")%>
                                                                    </td>
                                                                    <td style="width: 5%; text-align: left">
                                                                        <%# Eval("STARTDATE","{0:dd-MMM-yyyy}")%>
                                                                    </td>
                                                                    <td style="width: 5%; text-align: left">
                                                                        <%# Eval("ENDDATE","{0:dd-MMM-yyyy}")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divMsg" runat="server"></div>
            </div>
        </ContentTemplate>
    
    </asp:UpdatePanel>
      

    <script type="text/javascript" language="javascript">

        function totAllQUESTIONS(headchk) {
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



        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

        function IsNumeric(txt) {
            var ValidChars = "0123456789.-";
            var num = true;
            var mChar;

            for (i = 0 ; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);
                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Error! Only Numeric Values Are Allowed")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }

    </script>
    <!-- iCheck -->
    <script type="text/javascript">
        window.history.forward();
        function noBack() {
            window.history.forward();
        }
    </script>
    <%--<script language="javascript">
var browser =  navigator.appName; // Get browser
var silverlightInstalled = false;

if (browser == 'Microsoft Internet Explorer')
{
    try 
    {
        var slControl = new ActiveXObject('AgControl.AgControl');
        silverlightInstalled = true;
    }
    catch (e) 
    {
        // Error. Silverlight not installed.
    }
}
else 
{
    // Handle Netscape, FireFox, Google chrome etc
    try 
    {
        if (navigator.plugins["Silverlight Plug-In"]) 
        {
            silverlightInstalled = true;
        }
    }
    catch (e) 
    {
        // Error. Silverlight not installed.
    }
}

//alert(silverlightInstalled);
</script>--%>
</asp:Content>
