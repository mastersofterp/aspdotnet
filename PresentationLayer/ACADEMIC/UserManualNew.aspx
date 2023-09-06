<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="UserManualNew.aspx.cs" Inherits="ACADEMIC_UserManualNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script>
          $(document).ready(function () {

              bindDataTable();
              Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
          });

          function bindDataTable() {

              if (!$.fn.DataTable.isDataTable('#example2')) {
                  $('#example2').dataTable();
              }
              if (!$.fn.DataTable.isDataTable('#example3')) {
                  $('#example3').dataT
                  if (!$.fn.DataTable.isDataTable('#example1')) {
                      $('#example1').dataTable();
                  }


              }
          }
    </script>

    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }

 

        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }

 

        .modalPopup
        {
            background-color: #FFFFFF;
            width: 700px;
            border: 3px solid #858788; /*#0DA9D0;*/
            border-radius: 12px;
            padding: 0;
        }

 

            .modalPopup.right
            {
                right: 0 !important;
                top: 0 !important;
                left: inherit !important;
                border-radius: 12px;
                height: 100%;
            }

 

            .modalPopup .header
            {
                background-color: #858788; /*#2FBDF1;*/
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }

 

            .modalPopup .body
            {
                padding: 10px;
                min-height: 50px;
                text-align: center;
                font-weight: bold;
            }

 

            .modalPopup .footer
            {
                padding: 6px;
            }

 

            .modalPopup .yes, .modalPopup .no
            {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }

 

            .modalPopup .yes
            {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }

 

            .modalPopup .no
            {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }

 

        element.style
        {
            font-family: Verdana !important;
            font-size: 10pt !important;
            color: red !important;
        }
    </style>

    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updUserManual"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updUserManual" runat="server">
        <ContentTemplate>

            <div id="divMsg" runat="server">
            </div>

            <div class="row">
                <div class="col-sm-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title"><b> User Manual Upload / Download </b></h3>--%>
                            <asp:Label ID="lblHeading" runat="server" style="font-weight:bold;font-size:20px" ></asp:Label>
                            <div style="color: Red; font-weight: bold; padding-top: 15px" id="divNote" runat="server" visible="false">
                                &nbsp;&nbsp;Note : 
                              Upload the User Manual Files only with following formats: .PDF, .PNG, .JPEG, .JPG
                            </div>
                        </div>
   
                        <div class="col-md-12">

                           <%--  <table id="Table3" runat="server" width="100%">
                                                                <tr>
                                                                    <td>--%>
                              <asp:ListView ID="lvDetailsList" runat="server" OnItemDataBound="lvDetailsList_ItemDataBound" >
                                  <LayoutTemplate>
                                      <div id="demo-grid" class="vista-grid">
                                         <%-- <div class="titlebar heading">
                                             Module Wise User Manual Details
                                          </div>--%>
                                          <table  class="dataTable table table-bordered table-striped table-hover" style="width: 100%">
                                              <thead>
                                              <tr class="header bg-light-blue">
                                                 <%-- <td>--%>
                                                    <%--  <table  class="dataTable table table-bordered table-striped table-hover" style="width: 100%">--%>
                                                         <%-- <thead>--%>

                                                              <%--<tr class="header bg-light-blue">--%>
                                                                  <th width="7%">
                                                                  </th>
                                                                  <th>Sub - Module
                                                                  </th>      
                                                            <%--  </tr>--%>
                                                         <%-- </thead>--%>
                                                         <%-- <tbody>
                                                              <tr id="Tr7" runat="server" />
                                                          </tbody>--%>
                                                     <%-- </table>--%>
                                                  <%--</td>--%>
                                              </tr>
                                                  </thead>
                                              <tbody>
                                                  <tr id="itemPlaceholder" runat="server" />
                                              </tbody>
                                          </table>
                                      </div>
                                  </LayoutTemplate>
                                
                                  <ItemTemplate>
                                      <table cellpadding="0" cellspacing="0" class="datatable" width="100%">
                                          <tr>
                                              <td>
                                                  <table  class="dataTable table table-bordered table-striped table-hover" style="width: 100%">
                                                      <tr id="MAIN" runat="server">
                                                          <td>
                                                              <tr id="MainTableRow" runat="server" class="item" ><%--onmouseout="this.style.backgroundColor='#FFFFFF'"
                                                                  onmouseover="this.style.backgroundColor='YELLOW'"--%>
                                                                  <td width="7%">
                                                                      <asp:Panel ID="pnlDetails" runat="server" Style="cursor: pointer; vertical-align: top; float: left">
                                                                          <asp:Image ID="imgExp" runat="server" ImageUrl="~/images/action_down.gif" />
                                                                      </asp:Panel>
                                                                      &nbsp;&nbsp;<asp:Label ID="lbIoNo" runat="server" Text='<%# Eval("As_Title") %>'
                                                                          ToolTip='<%# Eval ("As_NO") %>' Visible="false"></asp:Label></td> 
                                                                  <td>
                                                                      <asp:Label ID="lblAsTitle" runat="server" Text='<%# Eval("As_Title")%>' ToolTip='<%# Eval("As_NO") %>'></asp:Label>

                                                                       <ajaxToolKit:CollapsiblePanelExtender ID="cpeCourt2" runat="server" CollapseControlID="pnlDetails"
                                                                          Collapsed="true" CollapsedImage="~/images/action_down.gif" ExpandControlID="pnlDetails"
                                                                          ExpandedImage="~/images/action_up.gif" ImageControlID="imgExp" TargetControlID="pnlShowCDetails">
                                                                      </ajaxToolKit:CollapsiblePanelExtender>
                                                                  </td>  
                                                                <%--   <td>--%>
                                                                     
                                                                  <%--</td> --%>                                                                                                        
                                                              </tr>
                                                          </td>
                                                      </tr>
                                                  </table>
                                              </td>
                                          </tr>
                                        <%--  <tr>
                                              <td></td>
                                          </tr>--%>
                                          <tr>
                                            <%--  <td>
                                                  <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                      <tr>
                                                          <td>--%>
                                               <%--<div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
                                                     <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdLv"
                                                         DynamicLayout="true" DisplayAfter="0">
                                                         <ProgressTemplate>
                                                             <div style="width: 120px; padding-left: 5px">
                                                                 <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                                                                 <p class="text-success"><b>Loading..</b></p>
                                                             </div>
                                                         </ProgressTemplate>
                                                     </asp:UpdateProgress>
                                                 </div>--%>
                                              <%--<asp:UpdatePanel runat="server" ID="UpdLv">
                                                  <ContentTemplate>--%>
                                                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                  <tr>
                                                                      <td>
                                                                          <asp:Panel ID="pnlShowCDetails" runat="server" CssClass="collapsePanel">
                                                                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                  <tr>
                                                                                      <td align="center">
                                                                                          <b> User Manual Details </b>
                                                                                          <asp:ListView ID="lvUserManualList" runat="server" OnItemDataBound="lvUserManualList_ItemDataBound">
                                                                                              <LayoutTemplate>
                                                                                                  <div id="demo-grid" class="vista-grid" style="width: 99%;">
                                                                                                      <table  class="dataTable table table-bordered table-striped table-hover" width="99%">
                                                                                                          <thead>
                                                                                                              <tr class="header bg-light-blue">
                                                                                                                  <th>Module
                                                                                                                  </th>
                                                                                                                  <th style="text-align:center">Preview</th>
                                                                                                                  <th>Download
                                                                                                                  </th>
                                                                                                                  <th><asp:Label ID="lblUserManual" runat="server" Text="Upload User Manual"></asp:Label></th>
                                                                                                                  <th><asp:Label ID="lblUpload" runat="server" Text="Upload" style="text-align:center"></asp:Label></th>  
                                                                                                              </tr>
                                                                                                          </thead>
                                                                                                          <tbody>
                                                                                                              <tr id="itemPlaceholder" runat="server">
                                                                                                              </tr>
                                                                                                          </tbody>
                                                                                                      </table>
                                                                                                  </div>
                                                                                              </LayoutTemplate>
                                                                                              <EmptyDataTemplate>
                                                                                                  <div style="text-align: center; font-family: Arial; font-size: medium;color:red;font-weight:bold">
                                                                                                      No Record Found !
                                                                                                  </div>
                                                                                              </EmptyDataTemplate>
                                                                                              <ItemTemplate>
                                                                                                  <tr class="item">
                                                                                                      <td style="width: 30%">
                                                                                                          <asp:Label ID="lblUMNo" ToolTip='<%# Eval("ASNo") %>' runat="server" Text='<%# Eval("SUB_MODULE_NAME") %>'></asp:Label>
                                                                                                          <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("AS_No") %>' />
                                                                                                          <asp:HiddenField runat="server" ID="HiddenField2" Value='<%# Eval("SMID") %>' />
                                                                                                      </td>
                                                                                                       <td class="text-center" style="width: 30%">
                                                                                                          <asp:LinkButton ID="lnkView" runat="server" CommandName='<%# Eval("FILE_NAME") %>' OnClick="lnkView_Click" CommandArgument='<%# Eval("PREVIEW_PATH") %>' ToolTip='<%# Eval("SMID") %>'><image style="height:25px" src="../IMAGES/view.gif" data-toggle="modal" data-target="#myModal22"></image></asp:LinkButton>
                                                                                                           <asp:Label ID="lblPreview" Font-Bold="true" runat="server"></asp:Label>
                                                                                                           
                                                                                                       </td>
                                                                                                       <td style="width: 30%">               
                                                                                                          <asp:LinkButton ID="lnkDownloadDoc" runat="server" OnClick="lnkDownloadDoc_Click" Visible="false"
                                                                                                              Text="Click to Download" ToolTip="Click here to download User Manual"
                                                                                                              CommandArgument='<%# Eval("ASNo") %>' CommandName='<%# Eval("SMID") %>'> 
                                                                                                          </asp:LinkButton>
                                                                                                          <asp:Label ID="lblDownload" Font-Bold="true" runat="server"></asp:Label>
                                                                                                      </td>
                                                                                                       <td style="width: 8%">
                                                                                                          <asp:FileUpload ID="fuFile" runat="server" ToolTip="UMNO" />
                                                                                                          <asp:HiddenField ID="hdnFile" runat="server" />
                                                                                                          <br />
                                                                                                      </td>
                                                                                                      <td>
                                                                                                        <%--  <asp:Button ID="btnSubmit" runat="server" Text="Upload" ValidationGroup="Submit" OnClick="btnSubmit_Click" Width="80px" Height="35px"
                                                                                                              CssClass="btn btn-success" CommandArgument='<%# Eval("AS_Title") %>' ToolTip='<%# Eval("AS_Title") %>' />--%>
                                                                                                            <asp:Button ID="btnSubmit" runat="server" Text="Upload" ValidationGroup="Submit" OnClick="btnSubmit_Click" Width="80px" Height="35px"
                                                                                                              CssClass="btn btn-success" CommandArgument='<%# Eval("SUB_MODULE_NAME") %>' ToolTip='<%# Eval("SUB_MODULE_NAME") %>' />
                                                                                                      </td>
                                                                                                      
                                                                                                  </tr>
                                                                                              </ItemTemplate>
                                                                                          </asp:ListView>
                                                                                      </td>
                                                                                  </tr>
                                                                              </table>
                                                                          </asp:Panel>
                                                                      </td>
                                                                  </tr>
                                                              </table>
                                                  <%-- </ContentTemplate>
                                              </asp:UpdatePanel>--%>
                                                          <%--</td>
                                                      </tr>
                                                  </table>
                                              </td>--%>
                                          </tr>
                                      </table>
                                  </ItemTemplate>
                              </asp:ListView>
                       <%--   </td>
                                                                </tr>                                    
                                                            </table>   --%>             
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-5 form-group">
                                </div>
                                <div class="col-md-7 form-group" style="padding-left: 75px">
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-danger" Visible="false" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <%--  <div class="container">
                <div id="myModal22" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content" style="margin-top: -25px">
                            <div class="modal-body">                 
                                <iframe id="iframe1" runat="server" frameborder="0" width="100%" height="547px"></iframe>
                              
                                <div class="modal-footer" style="height: 0px">
                                    <button type="button" style="margin-top: -10px" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>


              


        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="lvDetailsList" />
          
        </Triggers>

    </asp:UpdatePanel>

    <ajaxToolKit:ModalPopupExtender ID="mpeViewDocument" BehaviorID="mpeViewDocument" runat="server" PopupControlID="pnlPopup"
        TargetControlID="lnkFake" CancelControlID="btnClose" BackgroundCssClass="modalBackground"
        OnOkScript="ResetSession()">
    </ajaxToolKit:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup">
        <div class="header">
            Document
                 
        </div>
        <div class="body">
            <iframe runat="server" width="680px" height="550px" id="iframeView"></iframe>
        </div>
        <div class="footer" align="right">
            <asp:Button ID="btnClose" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Larger" Text="Close" CssClass="no" />
        </div>
    </asp:Panel>
      <script type="text/javascript" language="javascript">

          /* To collapse and expand page sections */
          function toggleExpansion(imageCtl, divId) {
              if (document.getElementById(divId).style.display == "block") {
                  document.getElementById(divId).style.display = "none";
                  imageCtl.src = "../../images/expand_blue.jpg";
              }
              else if (document.getElementById(divId).style.display == "none") {
                  document.getElementById(divId).style.display = "block";
                  imageCtl.src = "../../images/collapse_blue.jpg";
              }
          }
    </script>
    <%-- <div id="divMsg" runat="server">
    </div>--%>


</asp:Content>

