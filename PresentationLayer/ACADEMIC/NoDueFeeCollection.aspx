<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="NoDueFeeCollection.aspx.cs" Inherits="ACADEMIC_NoDueFeeCollection" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <script src="../Content/jquery.js" type="text/javascript"></script>
    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>
    <script type="text/javascript" charset="utf-8">
        $(document).ready(function() {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>
    <div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">NO DUE FEE COLLECTION</h3>               
            </div>
          
                <div class="box-body">
                      <div id="divMsg" runat="server" class="col-md-12">
        </div>
                    <div class="col-md-2"></div>
                    <div class="col-md-4">
                        <label>Degree</label>
                        <asp:DropDownList ID="ddlDegree" AppendDataBoundItems="true" runat="server"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                            <asp:ListItem Value="0">Please Select
                            </asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                            Display="None" ErrorMessage="Please select Degree." ValidationGroup="submit"
                            InitialValue="0" SetFocusOnError="true" />
                    </div>
                    <div class="col-md-4">
                        <label>Branch</label>
                        <asp:DropDownList ID="ddlBranch" AppendDataBoundItems="true" runat="server" 
                         onselectedindexchanged="ddlBranch_SelectedIndexChanged" >
                        <asp:ListItem Value="0">Please Select
                        </asp:ListItem>
                    </asp:DropDownList>
                    
                    <asp:RequiredFieldValidator ID="valBranch" runat="server" ControlToValidate="ddlBranch"
                        Display="None" ErrorMessage="Please select Branch." ValidationGroup="submit"
                        InitialValue="0" SetFocusOnError="true" />
                    </div>

                </div>
            <div class="box-footer">
                <p class="text-center">
                    <asp:Button ID="btnShow" runat="server" Text="Show"
                        OnClick="btnShow_Click" ValidationGroup="submit" CssClass="btn btn-primary" />
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                        OnClick="btnSubmit_Click" ValidationGroup="submit" CssClass="btn btn-primary"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                        OnClick="btnCancel_Click" CssClass="btn btn-warning"/>
                    <asp:Button ID="btnReport" runat="server" Text="Report"
                        OnClick="btnReport_Click" ValidationGroup="submit" CssClass="btn btn-info"/>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                </p>
                <div id="divSelectedStudents" runat="server" visible="false" class="table table-responsive col-md-12">
        <fieldset>
            <legend> Students</legend>
               <asp:Repeater ID="lvStudents" runat="server" onitemdatabound="lvStudents_ItemDataBound" >
                          <HeaderTemplate>
                           <div id="listViewGrid" >                             
                                 <h4> List of Students</h4>
                          </div>
                        <table id="tblSearchResults" class="table table-hover table-bordered">
                            <thead>
                                <tr class="bg-light-blue">
                                <th>
                                    Roll No.
                                </th>
                                <th>
                                    Name
                                </th>
                                <th>
                                    Due Fee
                                </th>
                                <th>
                                    Status
                                </th>
                            </tr>                          
                           </thead>
                            <tbody>  <tr id="itemPlaceholder" runat="server" /></tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                        <td>
                           <%# Eval("REGNO") %>
                          <asp:Label ID="lblRegno" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                          
                         <asp:HiddenField ID="hidid" runat="server" Value ='<%# Eval("IDNO") %>' />
                         <asp:HiddenField ID="hidRegno" runat="server" Value ='<%# Eval("REGNO") %>' />
                        </td>
                        <td>
                            <%# Eval("STUDNAME")%>
                             <asp:HiddenField ID="hidName" runat="server" Value ='<%# Eval("STUDNAME") %>' />
                        </td>
                        <td>
                          <asp:TextBox ID="txtDue" runat="server" onkeyup="validateNumeric(this)"></asp:TextBox>
                        </td>
                        <td>
                           <asp:DropDownList ID="ddlStatus" AppendDataBoundItems="true" runat="server">
                        <asp:ListItem Value="0">No
                        </asp:ListItem>
                        <asp:ListItem Value="1">Yes
                        </asp:ListItem>
                  
                    </asp:DropDownList>
                        </td>
                    </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody></table></FooterTemplate>
                </asp:Repeater>
                 
        </fieldset>
        <c
    </div>
            </div>
        </div>
    </div>
</div>





    <table cellpadding="0" cellspacing="0" border="0" width="100%">
       
        <%--<tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                NO DUE FEE COLLECTION&nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>--%>
        <%--  Reset the sample so it can be played again --%>
       <%-- <tr>
            <td>
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                    border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);
                    font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                            font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
                    </div>
                </div>

                <script type="text/javascript" language="javascript">
                // Move an element directly on top of another element (and optionally
                // make it the same size)
                function Cover(bottom, top, ignoreSize) 
                {
                    var location = Sys.UI.DomElement.getLocation(bottom);
                    top.style.position = 'absolute';
                    top.style.top = location.y + 'px';
                    top.style.left = location.x + 'px';
                    if (!ignoreSize) 
                    {
                        top.style.height = bottom.offsetHeight + 'px';
                        top.style.width = bottom.offsetWidth + 'px';
                    }
                }
                </script>

                <ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
                    <Animations>
                    <OnClick>
                        <Sequence>
                            <EnableAction Enabled="false" />
                            
                            <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                            
                            <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                            <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                            <FadeIn AnimationTarget="info" Duration=".2"/>
                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                            
                            <Parallel AnimationTarget="info" Duration=".5">
                                <Color PropertyKey="color" StartValue="#666666" EndValue="#FF0000" />
                                <Color PropertyKey="borderColor" StartValue="#666666" EndValue="#FF0000" />
                            </Parallel>
                            <Parallel AnimationTarget="info" Duration=".5">
                                <Color PropertyKey="color" StartValue="#FF0000" EndValue="#666666" />
                                <Color PropertyKey="borderColor" StartValue="#FF0000" EndValue="#666666" />
                                <FadeIn AnimationTarget="btnCloseParent" MaximumOpacity=".9" />
                            </Parallel>
                        </Sequence>
                    </OnClick>
                    </Animations>
                </ajaxToolKit:AnimationExtender>
                <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
                    <Animations>
                    <OnClick>
                        <Sequence AnimationTarget="info">
                            <StyleAction Attribute="overflow" Value="hidden"/>
                            <Parallel Duration=".3" Fps="15">
                                <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                <FadeOut />
                            </Parallel>
                            
                            <StyleAction Attribute="display" Value="none"/>
                            <StyleAction Attribute="width" Value="250px"/>
                            <StyleAction Attribute="height" Value=""/>
                            <StyleAction Attribute="fontSize" Value="12px"/>
                            <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />
                            
                            <EnableAction AnimationTarget="btnHelp" Enabled="true" />
                        </Sequence>
                    </OnClick>
                    <OnMouseOver>
                        <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                    </OnMouseOver>
                    <OnMouseOut>
                        <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                    </OnMouseOut>
                    </Animations>
                </ajaxToolKit:AnimationExtender>
            </td>
        </tr>--%>
   <%-- </table>
    
    <br />
    <div style="color: Red; font-weight: bold">
        &nbsp;Note : * marked fields are Mandatory</div>
        
    <fieldset class="fieldset">
        <legend class="legend">Select criteria</legend>--%>
         <table width="100%" cellpadding="2" cellspacing="2" border="0">
      <%--   <div id="divMsg" runat="server">
        </div>--%>
           <%-- <tr>
                <td>
                    <span class="validstar">*</span>Degree:</td>
                <td>
                <asp:DropDownList ID="ddlDegree" AppendDataBoundItems="true" runat="server"
                        Width="140px" AutoPostBack="True" 
                        onselectedindexchanged="ddlDegree_SelectedIndexChanged" ><asp:ListItem Value="0">Please Select
                        </asp:ListItem></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                        Display="None" ErrorMessage="Please select Degree." ValidationGroup="submit"
                        InitialValue="0" SetFocusOnError="true" />
                    </td>
            </tr>--%>
           <%-- <tr>
                <td>
                    <span class="validstar">*</span>Branch:
                </td>
                <td>
                    <asp:DropDownList ID="ddlBranch" AppendDataBoundItems="true" runat="server" 
                        Width="140px" onselectedindexchanged="ddlBranch_SelectedIndexChanged" >
                        <asp:ListItem Value="0">Please Select
                        </asp:ListItem>
                    </asp:DropDownList>
                    
                    <asp:RequiredFieldValidator ID="valBranch" runat="server" ControlToValidate="ddlBranch"
                        Display="None" ErrorMessage="Please select Branch." ValidationGroup="submit"
                        InitialValue="0" SetFocusOnError="true" />
                </td>
            </tr>--%>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
           <%-- <tr>
                <td>
                    &nbsp;</td>
                <td>
                <asp:Button ID="btnShow" runat="server" Text="Show"
                    OnClick="btnShow_Click" ValidationGroup="submit" 
                    />
                &nbsp;
                <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                    OnClick="btnSubmit_Click" ValidationGroup="submit" 
                     />
                &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel"
                    OnClick="btnCancel_Click" 
                    />
                     &nbsp;<asp:Button ID="btnReport" runat="server" Text="Report"
                    OnClick="btnReport_Click" ValidationGroup="submit" 
                     />
                &nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                </td>
            </tr>--%>
        </table>
    </fieldset>
    <br />
    <%--<div id="divSelectedStudents" runat="server" visible="false">
        <fieldset class="fieldset">
            <legend class="legend"> Students</legend>
               <asp:Repeater ID="lvStudents" runat="server" 
                onitemdatabound="lvStudents_ItemDataBound" >
                          <HeaderTemplate>
                           <div id="listViewGrid" class="vista-grid" style="width: 100%;">
                              <div class="titlebar">
                                  List of Students
                              </div>
                          </div>
                        <table id="tblSearchResults" cellpadding="0" cellspacing="0" class="display" style="width: 100%;">
                            <thead>
                                <tr class="header">
                                <th>
                                    Roll No.
                                </th>
                                <th>
                                    Name
                                </th>
                                <th>
                                    Due Fee
                                </th>
                                <th>
                                    Status
                                </th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server" />
                           <thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="item" >
                        <td align="center">
                           <%# Eval("REGNO") %>
                          <asp:Label ID="lblRegno" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                          
                         <asp:HiddenField ID="hidid" runat="server" Value ='<%# Eval("IDNO") %>' />
                         <asp:HiddenField ID="hidRegno" runat="server" Value ='<%# Eval("REGNO") %>' />
                        </td>
                        <td>
                            <%# Eval("STUDNAME")%>
                             <asp:HiddenField ID="hidName" runat="server" Value ='<%# Eval("STUDNAME") %>' />
                        </td>
                        <td align="center">
                          <asp:TextBox ID="txtDue" runat="server" onkeyup="validateNumeric(this)"></asp:TextBox>
                        </td>
                        <td align="center">
                           <asp:DropDownList ID="ddlStatus" AppendDataBoundItems="true" runat="server" 
                        Width="50px" >
                        <asp:ListItem Value="0">No
                        </asp:ListItem>
                        <asp:ListItem Value="1">Yes
                        </asp:ListItem>
                  
                    </asp:DropDownList>
                        </td>
                    </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody></table></FooterTemplate>
                </asp:Repeater>
                 
        </fieldset>
        <c
    </div>--%>
    

  
                
  <script type="text/javascript" language="javascript">
      //VALIDATIONS
      function validateNumeric(txt) {
          if (isNaN(txt.value)) {
              txt.value = '';
              alert('Only Numeric Characters Allowed!');
              txt.focus();
              return;
          }
      }
  </script>
</asp:Content>

