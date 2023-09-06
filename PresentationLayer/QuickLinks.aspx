<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="QuickLinks.aspx.cs" Inherits="QuickLinks" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div style="z-index: 1; position: absolute; top: 8px; left: 500px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updatepanel"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size:50px"></i>
                    <p class="text-success"><b>Loading..</b></p>

                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

  <asp:UpdatePanel ID="updatepanel" runat="server" UpdateMode="Conditional">
      <ContentTemplate>

      
     <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
               
                <div class="box-header with-border">
                    <h3 class="box-title"><b>FAVOURITE LINKS</b></h3>                   
                </div>

                    <div class="box-body">
                        <div class="row">
                        
                            </div>
                          <div class="row text-center">
                              <asp:Button runat="server" ID="btnSubmit" Text="Submit"  CssClass="btn btn-success" ValidationGroup="Qlinks" OnClick="btnSubmit_Click" />  
                              <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="Qlinks" ShowMessageBox="true" ShowSummary="false" />                               
                           </div>
                        </div>
                <div class="box-footer">
                    <div class="col-md-12 form-group" style="overflow-y:auto;height:450px">
                         <asp:ListView ID="lvALinks" runat="server">
                                <LayoutTemplate>
                                    <div id="demo-grid" class="vista-grid">
                                      
                                        <table class="table table-responsive table-hover table-bordered" id="tbl">
                                            <tr>
                                                <thead style="background-color:rgba(120, 120, 120, 0.48);font-size:15px">
                                                    <th style="text-align:center;font-weight:bold">
                                                        Select
                                                    </th>
                                                     <th style="font-weight:bold">
                                                        Module Name
                                                    </th>
                                                    <th style="font-weight:bold">
                                                        Page Title
                                                    </th>
                                                </thead>                                               
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="text-align:center">
                                            <asp:CheckBox runat="server" ID="chkSelect" Checked='<%# (Eval("STATUS").ToString() == "1" ? true : false) %>' ToolTip='<%# Eval("AL_No") %>' onclick="CheckSelectionCount(this)" />
                                        </td> 
                                         <td>
                                            <asp:Label runat="server" ID="lblModule" Text='<%# Eval("AS_Title") %>'></asp:Label>
                                        </td>  
                                         <td>
                                            <asp:Label runat="server" ID="lblPage" Text='<%# Eval("AL_Link") %>' ToolTip='<%# Eval("AL_Link") %>' ></asp:Label>
                                        </td>                                                                      
                                    </tr>
                                </ItemTemplate>
                           
                            </asp:ListView>
                                         
                          <%-- <div class="row text-center">
                               <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvALinks" PageSize="10" OnPreRender="dpPager_PreRender">                                   
                                    <Fields>
                                        <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                            ShowLastPageButton="false" ShowNextPageButton="false" />
                                        <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                        <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                            ShowLastPageButton="true" ShowNextPageButton="true" />
                                    </Fields>
                                </asp:DataPager>
                            </div>--%>
                    </div>
                                                     
                </div>
               </div>
            </div>
         </div>

       
    </ContentTemplate>
  </asp:UpdatePanel>
      <script>
          function CheckSelectionCount(chk) {
              var count = 0;
              debugger;

              var count = -2;
              var tbl = '';
              var list = '';
              var alltbl = ["tbl"];
              var count = 0;

              for (i = 0; i < alltbl.length; i++) {
                  tbl = document.getElementById(alltbl[i]);
                  if (tbl != null) {
                      var dataRows = tbl.getElementsByTagName('tr');
                      if (dataRows != null) {
                          list = 'lvALinks';
                          var chkid1 = chk.id;
                          var dataRows = tbl.getElementsByTagName('tr');
                          for (j = 0; j < dataRows.length ; j++) {
                              var chkid = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + j + '_chkSelect';
                              if (chkid != chkid1)
                                  if (document.getElementById(chkid).checked) {
                                      count++;
                                  }
                              if (count > 4) {
                                  alert('You can select maximum 5 Links!');
                                  document.getElementById(chkid1).checked = false;
                                  return;
                              }
                          }

                      }
                  }
              }

          }
    </script>
</asp:Content>

