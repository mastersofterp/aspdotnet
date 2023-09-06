<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BulkTranscriptReport.aspx.cs" Inherits="ACADEMIC_EXAMINATION_BulkTranscriptReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 75px; left: 600px;">
        <asp:UpdateProgress ID="upupdDetained" runat="server" AssociatedUpdatePanelID="updpnlExam">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>

            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updpnlExam" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>BULK TRANSCRIPT REPORT</b> </h3>
                            <div class="box-tools pull-right">
                                <div style="color: Red; font-weight: bold;">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>
                            </div>
                        </div>

                        <div class="box-body">
                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span> Session</label>
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                               

                            </div>
                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span> Degree</label>
                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                    TabIndex="1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                               

                            </div>
                            <div class="form-group col-md-4">
                                <label><span id="branch" runat="server" style="color: red;">*</span> Branch</label>
                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                    TabIndex="2">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                    Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                

                            </div>
                        </div>
                        <div class="box-footer">
                            <p class="text-center">
                                 <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show Students"
                                                      ValidationGroup="Show" CssClass="btn btn-primary" />                                                                                              
                                <asp:Button ID="btnTranscriptReport" runat="server" Text="Transcript Report"  ValidationGroup="Submit"
                                OnClick="btnTranscriptReport_Click" visible="false" CssClass="btn btn-info"/>
                                <asp:Button ID="btnReport" runat="server" Text="All Result" visible="false" CssClass="btn btn-info" ValidationGroup="submit" OnClick="btnReport_Click" />
                               &nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"  CssClass="btn btn-danger" OnClick="btnCancel_Click" />                         
                              <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show" />
                                 </p>
                           <div class="col-md-12">

                                <asp:Panel ID="Panel1" runat="server" Style="padding-left: 10px;" Width="98%">
                                    <asp:ListView ID="lvStudentRecords" runat="server">

                                        <LayoutTemplate>
                                              <div id="listViewGrid" class="vista-grid">
                                               <div class="box-header">
                                                <h3 class="box-title new-header-lv" style="margin-left:-5px; font-size: 16px; font-weight: bold; margin-top:-2px;">Student List</h3>
                                            </div>

                                                <table id="tblStudent" class="display table table-hover table-bordered">
                                                    <thead>
                                                        <tr id="Tr1" class="bg-light-blue">
                                                            <th>
                                                                <asp:CheckBox ID="chkIdentityCard" runat="server" onclick="return totAll(this);" ToolTip="Select or Deselect All Records" />
                                                            </th>
                                                            <th>Reg. No.
                                                            </th>
                                                            <th>Roll No.</th>
                                                            <th>Student Name
                                                            </th>
                                                            <th>Semester
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
                                                    <asp:CheckBox ID="chkreport" runat="server" ToolTip='<%# Eval("idno") %>' />
                                                    <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("REGNO")%>
                                                </td>
                                                <td><%# Eval("ROLLNO")%></td>
                                                <td>
                                                    <%# Eval("STUDNAME")%>
                                                    <asp:HiddenField ID="hdfAppliid" runat="server" Value='<%# Eval("STUDNAME") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTERNAME")%>
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
        <Triggers>
            
            <asp:PostBackTrigger ControlID="btnTranscriptReport" />
        </Triggers>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">
        function totAll(headchk) {
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
    </script>
    <script type="text/javascript" language="javascript">

        /* To collapse and expand page sections */
        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../../IMAGES/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../../IMAGES/collapse_blue.jpg";
            }
        }
    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

