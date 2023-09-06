<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ClubActivityRegistrationReport.aspx.cs" Inherits="ACADEMIC_ClubActivityRegistrationReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                <div class="col-md-12 col-sm-12 col-12" id="divclub" runat="server">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Club Activity Registration Report</h3>
                            <%--<h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>--%>
                        </div>
                         <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divSubCounsellor" runat="server">
                                                 <div class="label-dynamic">
                                                      <sup>*</sup>
                                                     <label>Select Club</label>
                                                    <%-- <asp:Label ID="lblDYsubuser" runat="server" Font-Bold="true" Text="SubCounsellors"></asp:Label>--%>
                                                       </div> 
                                         <asp:DropDownList ID="ddlclub" runat="server" CssClass="form-control" TabIndex="1" data-select2-enable="true"
                                            AppendDataBoundItems="True"  ToolTip="Please Select Club">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        
                                           
                                      </div>
                                                            
                                       <div class="form-group col-lg-4 col-md-6 col-12" >
                                                 <div class="label-dynamic">
                                                      <sup></sup>
                                                     <label>Select College</label>
                                                    <%-- <asp:Label ID="lblDYsubuser" runat="server" Font-Bold="true" Text="SubCounsellors"></asp:Label>--%>
                                                       </div> 
                              <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" TabIndex="1" data-select2-enable="true"
                                            AppendDataBoundItems="True"  ToolTip="Please Select College">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                      
                                      </div>
                               
                                     <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                    TabIndex="11" CssClass="btn btn-primary"   OnClientClick="return getVal();" Visible="false"  />
                                <asp:Button ID="btnReport" runat="server" Text="Report(Excel)"
                                    TabIndex="12" CssClass="btn btn-info" OnClick="btnReport_Click" ValidationGroup="submit"  />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    TabIndex="13" CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                            </div>


                                  
                                    </div>
                                  </div>
                                </div>
                  </div>
                                </div>
                            </div>
                           
                 </asp:Panel>
                            </ContentTemplate>
       <Triggers>
          <asp:PostBackTrigger ControlID="btnReport" />
         <%--   <asp:PostBackTrigger ControlID="ddlsubuser" />--%>
        </Triggers>
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
    </asp:Content>