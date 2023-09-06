<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ClubRegistration.aspx.cs" Inherits="ACADEMIC_StudentAchievement_ClubRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <link href="../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
     <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
           <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updclub"
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

  
 <asp:UpdatePanel ID="updclub" runat="server">
        <ContentTemplate>
             <asp:Panel runat="server" ID="pnlMain">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Club Registration Form</h3>
                            <%--<h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>--%>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    
                                 <div class="form-group col-lg-3 col-md-6 col-12" id="divSubCounsellor" runat="server">
                                   <div class="label-dynamic">
                                     <sup>* </sup>
                                     <label>Select Club</label>
                                                       </div> 
                                        <asp:ListBox ID="ddlclub" runat="server" AppendDataBoundItems="true"   CssClass="form-control multi-select-demo" SelectionMode="Multiple"   ></asp:ListBox>
                                                                  
                                     </div>
                                  
                            </div>
                            </div>
                                     <div class="col-12 btn-footer">
                              <%--  <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                                    TabIndex="5" CssClass="btn btn-primary" OnClick="btnSubmit_Click"  OnClientClick="return getVal();"/>
                              --%>
                                   <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                    TabIndex="11" CssClass="btn btn-primary" OnClick="btnSubmit_Click" OnClientClick="return getVal();"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    TabIndex="7" CssClass="btn btn-warning" OnClick="btnCancel_Click"  />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                            </div>
                            </div>
                    </div>
                </div>
                <div id="divMsg" runat="server">
                            </div>
            </div>
       </asp:Panel>  
        </ContentTemplate>
     
    </asp:UpdatePanel>
      <script type="text/javascript">
          $(document).ready(function () {
              $('.multi-select-demo').multiselect({
                  includeSelectAllOption: true,
                  maxHeight: 200
              });
          });
          var parameter = Sys.WebForms.PageRequestManager.getInstance();
          parameter.add_endRequest(function () {
              $('.multi-select-demo').multiselect({
                  includeSelectAllOption: true,
                  maxHeight: 200
              });
          });


    </script>
     <script>
         function getVal() {
             var array = []
             var nbChecked = 0;
             var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

             for (var i = 0; i < checkboxes.length; i++) {
                 //array.push(checkboxes[i].value)    
                 if (nbChecked == undefined) {
                     nbChecked = checkboxes[i].value + ',';
                 }
                 else {
                     nbChecked = nbChecked + 1;
                     //semNo += checkboxes[i].value + ',';
                 }
             }
             //alert(semNo);


             if (nbChecked == 0) {
                 alert('Please select atleast one Club.')
                 return false
             }
             else if (nbChecked > 3) {

                 alert('Please select Maximum Three Club.');
                 //checkboxes.length[j].selected = false;
                 return false
             }


             //document.getElementById(inpHide).value = "degreeNo";
         }

    </script>
    </asp:Content>
