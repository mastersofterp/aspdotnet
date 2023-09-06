<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SubDegreeType_Mapping.aspx.cs" Inherits="ACADEMIC_SubDegreeType_Mapping" %>

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
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div id="Div2" class="form-group col-lg-3 col-md-6 col-12" runat="server" >
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDtype" runat="server" TabIndex="1" CssClass="form-control" ToolTip="Please Select Degree Type" data-select2-enable="true" AutoPostBack="true"   AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDType" runat="server" ControlToValidate="ddlDtype"
                                            Display="None" ValidationGroup="Submit" InitialValue="0"  ErrorMessage="Please Select Degree Type"></asp:RequiredFieldValidator>
                                        
                                    </div>

                                       
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>SubDegree Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubtype" runat="server" TabIndex="2" CssClass="form-control" ToolTip="Please Select SubDegree Type" data-select2-enable="true" AutoPostBack="true"   AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSType" runat="server" ControlToValidate="ddlSubtype"
                                            Display="None" ValidationGroup="Submit" InitialValue="0"  ErrorMessage="Please Select SubDegree Type"></asp:RequiredFieldValidator>
                                    </div>

                                 
                                    
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddldegree" runat="server" TabIndex="3" CssClass="form-control" ToolTip="Please Select Degree" data-select2-enable="true" AutoPostBack="true"   AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvdegree" runat="server" ControlToValidate="ddldegree"
                                            Display="None" ValidationGroup="Submit" ErrorMessage="Please Select Degree" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                   
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree Code</label>
                                        </div>
                                        <asp:TextBox ID="txtDcode" runat="server"    TabIndex="4"  CssClass="form-control" ToolTip="Enter Degree Code" MaxLength=2  onkeypress="return allowOnlyNumber(event);"
                                                         ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDcode" runat="server" ControlToValidate="txtDcode"
                                            Display="None" ValidationGroup="Submit"  ErrorMessage="Enter Degree Code"></asp:RequiredFieldValidator>
                                    </div>
                                                                                                    
                                    </div>
                                </div>
                            </div>


                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="Submit"  TabIndex="5"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="6"/>

                                <asp:ValidationSummary ID="vsSubmit" runat="server" ShowMessageBox="true" DisplayMode="List"  ShowSummary="false" ValidationGroup="Submit" />
                            </div>
                             
                            <div class="col-md-12">
                                <asp:Panel ID="pnlListView" runat="server">
                                    <asp:ListView ID="lvOAdmission" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Online Admission Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divadmissionlist">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            Edit
                                                        </th>
                                                        <th>
                                                            Degree Type
                                                        </th>
                                                        <th>
                                                            SubDegree Type
                                                        </th>
                                                        <th>
                                                            Degree
                                                        </th>
                                                        <th>
                                                            Degree Code
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
                                                CommandArgument='<%# Eval("DEGREE_NO")%>'  AlternateText="Edit Record" ToolTip="Edit Record"
                                                    />
                                                    </td>
                                             
                                                <td>
                                                    <%# Eval("UA_SECTIONNAME") %>
                                                </td>
                                                <td >
                                                    <%# Eval("SUB_DEGREE_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DEGREENAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DEGREE_CODE")%>
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



        function allowOnlyNumber(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Enter Number Only..!");
                return false;
            }
            return true;
           
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

