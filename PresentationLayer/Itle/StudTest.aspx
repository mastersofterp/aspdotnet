<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudTest.aspx.cs" Inherits="ITLE_StudTest" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ONLINE TEST</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlStudentTest" runat="server">
                                <asp:Panel ID="pnlTest" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <%--    <div class="sub-heading">Select Test</div>--%>
                                            <div class="col-lg-5 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Session  :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label></a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-7 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Course Name  :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblCourse" runat="server" Font-Bold="True"></asp:Label></a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Select Test Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlTestType" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlTestType_SelectedIndexChanged" ToolTip="Select Test Type">
                                                    <asp:ListItem Text="Please Select" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Objective" Value="O"></asp:ListItem>
                                                    <asp:ListItem Text="Descriptive" Value="D"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-12 col-12">
                                                <div class=" note-div">
                                                    <h5 class="heading">Note </h5>
                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Please login before 15 min of test start time.</span> </p>
                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Don't close the test window once it is started.</span> </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning"
                                            OnClick="btnCancel_Click" ToolTip="Click here to Reset" />
                                    </div>


                                </asp:Panel>

                                <div class="col-12">
                                    <div id="DivTestList" runat="server" visible="false">
                                        <div class="sub-heading">
                                            <h5>Descriptive Test List</h5>
                                        </div>

                                        <asp:Panel ID="pnlTestList" runat="server">
                                            <asp:ListView ID="lvTest" runat="server" DataKeyNames="TESTNO">
                                                <LayoutTemplate>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Test Name</th>
                                                                <th>Total Question</th>
                                                                <th>Test Duration</th>
                                                                <th>Attempts Allowed</th>
                                                                <th>Test Time</th>
                                                                <th>Test Type</th>
                                                                <th>Test Date</th>
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
                                                            <asp:LinkButton ID="btnlnkSelect" runat="server" Text='<%# Eval("TESTNAME")%>'
                                                                CommandName='<%# Eval("TESTNO") %>' CommandArgument='<%# Eval("TESTNO") %>' ToolTip='<%# Eval("TEST_TYPE")%>'
                                                                OnClick="btnlnkSelect_Click" OnClientClick="return confirm('Are you sure, you want to start this Test?');"></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <%# Eval("TOTALQUE")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("TESTDURATION")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ATTEMPTS")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("STARTDATE", "{0:hh:mm:ss tt}")%>
                                                                        -
                                                                        <%# Eval("ENDDATE", "{0:hh:mm:ss tt}")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("TEST_TYPE")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("STARTDATE", "{0:dd/MM/yyyy}")%>
                                                                        -
                                                                        <%# Eval("ENDDATE", "{0:dd/MM/yyyy}")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="col-12 mt-3">
                                    <div id="DivObjTestList" runat="server" visible="false">
                                        <div class="sub-heading">
                                            <h5>Objective Test List</h5>
                                        </div>
                                        <asp:Panel ID="pnObjTest" runat="server">
                                            <asp:ListView ID="lvObjTest" runat="server" DataKeyNames="TESTNO">
                                                <LayoutTemplate>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Test Name</th>
                                                                <th>Total Question</th>
                                                                <th>Test Duration</th>
                                                                <th>Attempts Allowed</th>
                                                                <th>Test Time</th>
                                                                <th>Test Type</th>
                                                                <th>Start Date</th>
                                                                <th>End Date</th>
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
                                                            <asp:LinkButton ID="btnObjlnkSelect" runat="server" Text='<%# Eval("TESTNAME")%>'
                                                                CommandName='<%# Eval("TESTNO") %>' CommandArgument='<%# Eval("TESTNO") %>' ToolTip='<%# Eval("TEST_TYPE")%>'
                                                                OnClick="btnObjlnkSelect_Click" OnClientClick="return confirm('Are you sure you want to start this Test?');"></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <%# Eval("TOTALQUE")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("TESTDURATION")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ATTEMPTS")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("STARTDATE", "{0:hh:mm:ss tt}")%>
                                                                        -
                                                                        <%# Eval("ENDDATE", "{0:hh:mm:ss tt}")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("TEST_TYPE")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("STARTDATE","{0:dd-MMM-yyyy}")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ENDDATE","{0:dd-MMM-yyyy}")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
             <div class="form-group row">
                  <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
            <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
                runat="server" TargetControlID="lnkDummy" PopupControlID="div"
                BackgroundCssClass="modalBackground" />

            <asp:Panel ID="div" runat="server" Style="display: none; box-shadow: rgba(0, 0, 0, 0.2) 1px 5px 5px; padding: 20px; margin-bottom:25px; border-radius: 10px;" CssClass="modalPopup" Height="450px">
               
                <div style="text-align: center">
                    <div class="col-md-12">
                        <div >
                            <div >
                                <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/online.png" Height="200px"  />
                            </div>
                            <br />
                            <br />
                            <div>
                                 <div class="label-dynamic">
                                   <sup></sup>
                                  <b> <label>Enter Password For Attempts the Exam</label></b>
                               </div>
                            <asp:TextBox ID="txtpassword" runat="server" TabIndex="2"  onCopy="return false" onPaste="return false" onCut="return false" CssClass="form-control" ></asp:TextBox>
                            <asp:HiddenField ID="hdntsetno" runat="server" />
                            <asp:HiddenField ID="hdntestname" runat="server" />
                            <asp:HiddenField ID="hdntesttype" runat="server" />
                       
                            </div>
                        </div>
                        <br />
                        <br />
                        <div >
                            <div >
                                <asp:Button ID="btnOkDel" runat="server" Text="Close"  CssClass="btn btn-primary" OnClick="btnOkDel_Click" />
                                <asp:Button ID="btnLogin" runat="server" Text="Login"  CssClass="btn btn-primary"  OnClick="btnLogin_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>


              <div class="form-group row">
                  <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
            <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mdlPopupDel"
                runat="server" TargetControlID="LinkButton1" PopupControlID="Panel1"
                BackgroundCssClass="modalBackground" />

            <asp:Panel ID="Panel1" runat="server" Style="display: none; box-shadow: rgba(0, 0, 0, 0.2) 1px 5px 5px; padding: 20px; margin-bottom:25px; border-radius: 10px;" CssClass="modalPopup" Height="450px">
               
                <div style="text-align: center">
                    <div class="col-md-12">
                        <div >
                            <div >
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/online.png" Height="200px"  />
                            </div>
                            <br />
                            <br />
                            <div>
                                 <div class="label-dynamic">
                                   <sup></sup>
                                  <b> <label>Enter Password For Attempts the Exam</label></b>
                               </div>
                            <asp:TextBox ID="txtpass" runat="server" TabIndex="2"  onCopy="return false" onPaste="return false" onCut="return false" CssClass="form-control" ></asp:TextBox>
                            <asp:HiddenField ID="hdntsetno1" runat="server" />
                            <asp:HiddenField ID="hdntestname1" runat="server" />
                            <asp:HiddenField ID="hdntesttype1" runat="server" />
                       
                            </div>
                        </div>
                        <br />
                        <br />
                        <div >
                            <div >
                                <asp:Button ID="btnclosepop" runat="server" Text="Close"  CssClass="btn btn-primary" OnClick="btnclosepop_Click" />
                                <asp:Button ID="btnlogondesc" runat="server" Text="Login"  CssClass="btn btn-primary"  OnClick="btnlogondesc_Click"/>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>

        </ContentTemplate>
    </asp:UpdatePanel>
     <style type="text/css">
        .modalBackground {
            background-color: #ccc;
            filter: alpha(opacity=60);
            opacity: 0.9;
        }

        .modalPopup {
            background-color: white;
            padding-top: 10px;
            padding-bottom: 10px;
            padding-left: 10px;
            padding-right: 20px;
            width: 500px;
            height: 500px;
            overflow-y: auto;
        }

    </style>
    <style>
        .danger {
            margin-bottom: 15px;
            padding: 4px 12px;
        }

        .danger {
            background-color: #ffdddd;
            border-left: 6px solid #f44336;
        }
    </style>
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
