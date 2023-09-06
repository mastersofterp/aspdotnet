<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SubModuleMaster.aspx.cs" Inherits="ACADEMIC_SubModuleMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updDetails"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size:50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

        <asp:UpdatePanel ID="updDetails" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><b>SUBMODULE MASTER</b></h3>
                                <div class="pull-right">
                         <div style="color: Red; font-weight: bold">
                             &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory</div>
                </div> 
                            </div>
                            <div class="box-body">
                                    <div class="form-group col-md-4">
                                        <label><span style="color:red;">*</span> Module</label>
                                        <asp:DropDownList ID="ddlModule" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" AutoPostBack="true" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged"
                                            CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                       <%--<asp:RequiredFieldValidator ID="rfvModule" runat="server" ControlToValidate="ddlModule"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Module" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <label><span style="color:red;">*</span> Sub-Module</label>
                                       <asp:TextBox ID="txtSubModule" runat="server"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ID="rfvSubModule" runat="server" ControlToValidate="txtSubModule"
                                            Display="None" ErrorMessage="Please Enter Sub-Module" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>--%>
                                    </div>
                                 <div class="col-md-4 form-group" style="height: 100px;overflow: auto;">
                                        <label>User Types :</label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;<br />
                                        <asp:CheckBox ID="chkUserRights" runat="server" Text="All User" onclick="SelectAllUsers()" />
                                        <asp:CheckBoxList ID="chkUserRightsList" runat="server" RepeatColumns="1" RepeatDirection="Horizontal">
                                        </asp:CheckBoxList>
                                     <%-- <asp:RequiredFieldValidator ID="rfvUserRights" runat="server" ControlToValidate="chkUserRightsList"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select User Types" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>--%>
                                  <%--   <asp:CustomValidator Display="None"  runat="server"  ID="cblOptionsValidator" ControlId="chkUserRightsList" ClientValidationFunction="ValidateCheckBox" 
                                         ErrorMessage="Please Select User Types" ></asp:CustomValidator>--%>
                                    </div>
                            
                                 
                                </div>

                            <div class="box-footer">
                                <p class="text-center">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="teacherallot" 
                                        OnClick="btnSave_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-danger" OnClick="btnClear_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </p>
                                                         
                                <div class="col-md-12">
                                    <asp:Panel ID="pnlDetails" runat="server" ScrollBars="Auto" Height="400px">
                                        <div class="table table-responsive">
                                            <asp:ListView ID="lvDetails" runat="server">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <h4>Sub-Module List</h4>
                                                        <table class="table table-hover table-bordered">
                                                            <thead>
                                                                <tr class="bg-light-blue"> 
                                                                    <th>Select</th>                                                   
                                                                    <th>Module</th>
                                                                    <th>Sub-Module
                                                                    </th> 
                                                                    <th>User Types
                                                                    </th>                                                
                                                                </tr>
                                                            </thead>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>  
                                                        <td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" ToolTip="Edit"  ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("SMID") %>' OnClick="btnEdit_Click"/>
                                                        </td>                                                   
                                                        <td>
                                                            <%# Eval("As_Title")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SUB_MODULE_NAME")%>
                                                        </td> 
                                                        <td>
                                                            <%# Eval("userdesc")%>
                                                        </td>                                           
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                  </ContentTemplate>        
        </asp:UpdatePanel>     
    <script type="text/javascript">
        function SelectAllUsers() {
            var CHK = document.getElementById("<%=chkUserRightsList.ClientID%>");
              var checkbox = CHK.getElementsByTagName("input");

              var chkUser = document.getElementById('ctl00_ContentPlaceHolder1_chkUserRights');

              for (var i = 0; i < checkbox.length; i++) {
                  var chk = document.getElementById('ctl00_ContentPlaceHolder1_chkUserRightsList_' + i);
                  if (chkUser.checked == true) {
                      chk.checked = true;
                  }
                  else {
                      chk.checked = false;
                  }
              }
          }
    </script>

    <script language="javascript" type="text/javascript">
        function ValidateCheckBox(sender, args) {
            if (document.getElementById("<%= chkUserRightsList.ClientID %>").checked == true) {
                args.IsValid = true;
                alert('hi');
            } else {
                args.IsValid = false;
                
            }
        }
</script>

</asp:Content>

