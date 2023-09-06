<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ACPC_DCR_CREATION.aspx.cs" Inherits="ACADEMIC_ACPC_DCR_CREATION" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <%--<h6> <span style="color:red;">NOTE : * Marked fields are mandatory</span></h6>--%>
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"> ADJUSTMENT OF KEA FEE COLLECTION</h3>
                             <div class="box-tools pull-right">
                             <div style="color: Red; font-weight: bold">
                             &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory</div>
                            </div>
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <div class="form-group col-md-3">
                                <label><span style="color: red;">*</span> Session</label>
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="valSession" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please select Session." ValidationGroup="showstud"
                                    InitialValue="0" SetFocusOnError="true" />
                            </div>
                                <div class="form-group col-md-3" >
                                    <label> <span style="color:red;">*</span> Institute Name</label>
                                    <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Please Select Institute">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Institute" ControlToValidate="ddlCollege" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="showstud"></asp:RequiredFieldValidator>
                                </div>
                                 <div class="form-group col-md-3" >
                                    <label> <span style="color:red;">*</span> Degree</label>
                                   <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            ValidationGroup="showstud" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Degree" ControlToValidate="ddlDegree" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="showstud"></asp:RequiredFieldValidator>
                                    
                                </div>
                                <div class="form-group col-md-3" style="display: none">
                                    <label> <span style="color:red;">*</span> Batch Year</label>
                                    <asp:DropDownList ID="ddlBatchYear" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            ValidationGroup="showstud"
                                            Visible="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-3">
                                    <label> <span style="color:red;">*</span> Branch</label>
                                     <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true"
                                            ValidationGroup="showstud" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="showstud">
                                        </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-3">
                                    <label>  <span style="color:red;">*</span> Adm Batch</label>
                                    <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" 
                                            ValidationGroup="showstud" AutoPostBack="True" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="rfvBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Admission Batch" ValidationGroup="showstud">
                                        </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-3">
                                    <label>  <span style="color:red;">*</span> Receipt Code</label>
                                    <asp:DropDownList ID="ddlReceipt" runat="server" AppendDataBoundItems="true"
                                            ValidationGroup="showstud"  AutoPostBack="True" >
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Receipt Code" ValidationGroup="showstud">
                                        </asp:RequiredFieldValidator>
                                </div> 
                                <div class="form-group col-md-3">
                                    <label>  <span style="color:red;">*</span> Govt Category</label>
                                    <asp:DropDownList ID="ddlcategory" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                            ValidationGroup="showstud" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlcategory"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Govt Category" ValidationGroup="showstud">
                                        </asp:RequiredFieldValidator>
                                </div>   
                                <div class="form-group col-md-3">
                                    <label>  <span style="color:red;">*</span>College Code(Aided/UnAided)</label>
                                    <asp:DropDownList ID="ddlcolcodejss" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlcolcodejss_SelectedIndexChanged"
                                            ValidationGroup="showstud" >
                                           
                                        <asp:ListItem Value="0">Please select</asp:ListItem>
                                     <asp:ListItem Value="1">E021</asp:ListItem>
                                     <asp:ListItem Value="2">E057</asp:ListItem>
                                     <asp:ListItem Value="3">B292</asp:ListItem>
                                     <asp:ListItem Value="4">B292BC</asp:ListItem>
                                     <asp:ListItem Value="5">B292BR</asp:ListItem>
                                     <asp:ListItem Value="6">E721 </asp:ListItem>
                                     <asp:ListItem Value="7">B292BD</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlcolcodejss"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select College Code(Aided/UnAided)" ValidationGroup="showstud">
                                        </asp:RequiredFieldValidator>
                                </div>    
                                <div class="form-group col-md-3">
                                    <label>  <span style="color:red;">*</span> Semester</label>
                                    <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true"
                                            ValidationGroup="showstud" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="True" >
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="showstud">
                                        </asp:RequiredFieldValidator>
                                </div>
                                                             
                            </div>
                        </form>
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnAd" runat="server" OnClick="btnAd_Click" Text="Submit" ValidationGroup="offered"
                                    CssClass="btn btn-primary" />

                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Reset"
                                    CssClass="btn btn-warning" />
                               <%-- <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Report"
                                    CssClass="btn btn-info" Visible="false" />--%>
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="offered" />
                                <div class="col-md-12">
                                    <asp:Panel ID="pnlCourse" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="lvStudent" runat="server" OnItemDataBound="lvStudent_ItemDataBound" >
                                            <LayoutTemplate>
                                                <div id="demo-grid">
                                                    <h4>KEA FEE Details</h4>
                                                    <table class="table table-hover table-bordered">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                
                                                                <th><asp:CheckBox ID="chkAll" runat="server" onclick="SelectAll(this)" ToolTip="Select all students"/> Select</th>
                                                                <th>USN No.</th>
                                                                <th>Student Name</th>
                                                                <th>Amount</th>
                                                                <th>Date</th>
                                                                <th>Remark</th>
                                                                <th>Status</th>
                                                                <th>Print</th>
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
                                                        <asp:CheckBox ID="chkSelect" runat="server" Enabled='<%# Eval("EXIST_FLAG").ToString() == "1" ? false : true %>' />
                                                    </td>
                                                    <td><asp:Label ID="lblEnrollNo" runat="server" Text='<% #Eval("ENROLLNMENTNO")%>' ToolTip='<% #Eval("IDNO")%>'></asp:Label></td>
                                                    <td><%# Eval("NAME")%></td>
                                                    <td>
                                                        <asp:Label ID="lbltotamt" runat="server" Visible="true" 
 Text='<%# ( (Convert.ToInt32(Eval("categoryno"))==1 ||Convert.ToInt32(Eval("categoryno"))==3)&& Eval("COLLEGE_JSS").ToString()=="E021" && Convert.ToInt32(Eval("idtype"))==1)?"18590":
                                                        ( (Convert.ToInt32(Eval("categoryno"))==1 ||Convert.ToInt32(Eval("categoryno"))==3)&& Eval("COLLEGE_JSS").ToString()=="E021" && Convert.ToInt32(Eval("idtype"))==2)?"17590":
                                                        ( (Convert.ToInt32(Eval("categoryno"))==1 ||Convert.ToInt32(Eval("categoryno"))==3)&& Eval("COLLEGE_JSS").ToString()=="E057" && Convert.ToInt32(Eval("idtype"))==1)?"55500":
                                                        ( (Convert.ToInt32(Eval("categoryno"))==1 ||Convert.ToInt32(Eval("categoryno"))==3)&& Eval("COLLEGE_JSS").ToString()=="E057" && Convert.ToInt32(Eval("idtype"))==2)?"55500":
                                                        ( (Convert.ToInt32(Eval("categoryno"))==4)&& Eval("COLLEGE_JSS").ToString()=="E021" && Convert.ToInt32(Eval("idtype"))==1)?"55000":
                                                        ( (Convert.ToInt32(Eval("categoryno"))==4)&& Eval("COLLEGE_JSS").ToString()=="E021" && Convert.ToInt32(Eval("idtype"))==2)?"55100":
                                                        ( (Convert.ToInt32(Eval("categoryno"))==4)&& Eval("COLLEGE_JSS").ToString()=="E057" && Convert.ToInt32(Eval("idtype"))==1)?"52000":
                                                        ( (Convert.ToInt32(Eval("categoryno"))==4)&& Eval("COLLEGE_JSS").ToString()=="E057" && Convert.ToInt32(Eval("idtype"))==2)?"53000":"0"%>'>

                                                        </asp:Label>
                                                         <%--<label id="lbltotamt" visible="true" Text="18590" runat="server"></label>--%>
                                                        <label id="Label1" Text='<%# (Eval("categoryno")=="1" || Eval("categoryno")=="3") && Eval("COLLEGE_JSS")=="E021" && Eval("idtype")=="1"?"18590":"0"%>' runat="server"></label> 
                                                       <%-- <asp:HiddenField ID="hdntotamt" runat="server" />--%>

                                                    </td>
                                                    <td><%# Eval("RECON_DATE") %></td>
                                                    <td>
                                                        <asp:TextBox ID="txtRemark" runat="server" Text='<% #Eval("REMARK") %>' ToolTip="Please enter Remark" Enabled='<%# Eval("EXIST_FLAG").ToString() == "1" ? false : true %>'></asp:TextBox>
                                                    </td>
                                                    <td>
                                                      <%--<asp:Label ID="lblIStatus" runat="server" Visible="true" Text="Pending"></asp:Label>--%>
                                                         <asp:Label ID="lblIStatus" runat="server" Visible="true" Text='<%# Eval("EXIST_FLAG").ToString() == "1" ? "Received" : "Pending" %>'></asp:Label>
                                                      
                                                        <asp:HiddenField ID="hdfDmNo" runat="server" Value='<% #Eval("DM_NO")%>' />
                                                        <asp:Label ID="catgoryno" runat="server" Text='<%# Eval("categoryno")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="colcode" runat="server" Text='<%# Eval("COLLEGE_JSS")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="idtype" runat="server" Text='<%# Eval("idtype")%>' Visible="false"></asp:Label>
                                                    </td>
                                                    <th>
                                                        <asp:Button ID="btnprint" runat="server" Text="Receipt"
                                                            CommandArgument='<%# Eval("DCR_NO") %>' Enabled='<%# Eval("EXIST_FLAG").ToString() == "1" ? true : false %>'
                                                        ToolTip='<%# Eval("idno")%>' CausesValidation="False" OnClick="btnprint_Click" CssClass="btn btn-primary" />
                                                      
                                                    </th>
                                                </tr>
                                                
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
<%--                                <p>
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
                                </p>--%>
                           <%--     <p>
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
                            </p>--%>


                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
         <Triggers>
             <asp:PostBackTrigger ControlID="btnAd" />
             <asp:PostBackTrigger ControlID="btnCancel" />
            
         </Triggers>
    </asp:UpdatePanel>

    <%--  Shrink the info panel out of view --%> <%--  Reset the sample so it can be played again --%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel" runat="server"
        TargetControlID="div" PopupControlID="div"
        OkControlID="btnOkDel" OnOkScript="okDelClick();"
        CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" AlternateText="Warning" /></td>
                    <td>&nbsp;&nbsp;Are you sure you want to submit?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
     <div id="divMsg" runat="server" />
                        <div id="divScript" runat="server" />
    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }

        function SelectAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        if (e.disabled == true) {
                            e.checked = false;
                        }
                        else {
                            e.checked = true;
                        }
                    else
                        e.checked = false;
                }
            }
        }
    </script>
</asp:Content>

