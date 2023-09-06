<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DegreeSpecMapping.aspx.cs" Inherits="ACADEMIC_DegreeSpecMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
#ctl00_ContentPlaceHolder1_pnlListView .dataTables_scrollHeadInner {
width: max-content !important;
}
</style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updNotify"
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

    <asp:UpdatePanel ID="updNotify" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Degree Specialization Mapping </h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">                                                                                                         
                                    <div id="Div2" class="form-group col-lg-3 col-md-6 col-12" runat="server" >
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="1" CssClass="form-control" ToolTip="Please Select Degree" data-select2-enable="true"  AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ValidationGroup="Submit" InitialValue="0"  ErrorMessage="Please Select Degree"></asp:RequiredFieldValidator>
                                        
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Specialization</label>
                                        </div>
                                        <asp:TextBox ID="txtspecialization" runat="server"    TabIndex="2"  CssClass="form-control" ToolTip="Enter Degree Specialization" onkeypress="allowAlphaNumericSpace(event)" MaxLength="128"></asp:TextBox>                                                         
                                        <asp:RequiredFieldValidator ID="rfvspecialization" runat="server" ControlToValidate="txtspecialization"
                                            Display="None" ValidationGroup="Submit"  ErrorMessage="Enter Degree Specialization"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                             <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Status</label>
                                                 <div style="margin-top:10px">
                                                 <asp:CheckBox ID="chkActive" runat="server" Text="Active" TextAlign="Right" TabIndex="3" ToolTip="Check to Active"/>
                                                     </div>
                                        </div>                                                                                                                                                                                                                                                                         
                                    </div>
                                </div>
                            </div>


                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary"  ValidationGroup="Submit" OnClick="btnSubmit_Click"  TabIndex="4"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="5"/>

                                <asp:ValidationSummary ID="vsSubmit" runat="server" ShowMessageBox="true" DisplayMode="List"  ShowSummary="false" ValidationGroup="Submit" />
                            </div>
                             
                            <div class="col-md-12">
                                <asp:Panel ID="pnlListView" runat="server">
                                    <asp:ListView ID="lvSpecialization" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Degree Specialization Mapping</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 50%" id="divspecialization">
                                                <thead class="bg-light-blue" >
                                                    <tr>
                                                        <th>
                                                            Edit
                                                        </th>
                                                        <th>
                                                            Degree 
                                                        </th>
                                                        <th>
                                                            Specialization
                                                        </th>                                                   
                                                       <th>
                                                           Active Status
                                                       </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td style="text-align: center;">
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit1.gif" OnClick="btnEdit_Click"
                                                CommandArgument='<%# Eval("SPEC_NO")%>'  AlternateText="Edit Record" ToolTip="Edit Record"/>
                                                    </td>                                                                                            
                                                <td>
                                                    <%# Eval("DEGREENAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SPECIALIZATION")%>
                                                </td>
                                               <td>
                                                   <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("ACTIVE_STATUS") %>' ForeColor='<%# (Convert.ToString(Eval("ACTIVE_STATUS") )== "Active" ?System.Drawing.Color.Green:System.Drawing.Color.Red)%>'></asp:Label>
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
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
           
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function allowAlphaNumericSpace(e) {
            var code = ('charCode' in e) ? e.charCode : e.keyCode;
            if (!(code == 32) && // space
              !(code > 47 && code < 58) && // numeric (0-9)
              !(code > 64 && code < 91) && // upper alpha (A-Z)
              !(code > 96 && code < 123)) { // lower alpha (a-z)
                e.preventDefault();
                alert("Not Allowed Special Character..!");
                return true;
            }         
        else           
            return false;           
        }
        //function IsNumeric(txtDcode) {
        //    if (txtDcode != null && txtDcode.value != "") {
        //        if (isNaN(txtDcode.value)) {
        //            document.getElementById(txtDcode.id).value = '';
        //        }
        //    }
        //}
    </script>

</asp:Content>
