<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BusStructureMapping.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_BusStructureMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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
    <style>
        .new {
            width: 100px;
            text-align: center;
            background-color: transparent;
        }

        .firstdiv {
            width: 200px;
            text-align: center;
            background-color: transparent;
        }

        td {
            /*padding: 5px;*/
            text-align: center;
        }

        .main {
            background-color: #fff;
            border-top: 1px solid #fff !important;
        }

        .table-bordered, .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            border: 1px solid #5b5656;
        }
    </style>
     <script type="text/javascript">
         //var jq = $.noConflict();

         function ShowpImagePreview(input) {
             if (input.files && input.files[0]) {;
                 var reader = new FileReader();
                 reader.onload = function (e) {
                     jq('#ctl00_ContentPlaceHolder1_fuUploadBusStructure').attr('src', e.target.result);
                 }
                 reader.readAsDataURL(input.files[0]);
             }
         }




         function LoadImage() {
             alert('d');
             document.getElementById("ctl00_ContentPlaceHolder1_fuUploadBusStructure").src = document.getElementById("ctl00_ContentPlaceHolder1_fuUploadBusStructure").value;
         }
    </script>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">BUS STRUCTURE MAPPING</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Create Strcture Mapping </h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Route </label>
                                        </div>
                                        <asp:DropDownList ID="ddlRoute" runat="server" TabIndex="3" AppendDataBoundItems="true"
                                            CssClass="form-control" data-select2-enable="true" ToolTip="Select Stop Name">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblStopSeq" runat="server" Text=""></asp:Label>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Bus Structure </label>
                                        </div>
                                        <asp:DropDownList ID="ddlbusStructure" runat="server" TabIndex="3" AppendDataBoundItems="true" AutoPostBack="true"
                                            CssClass="form-control"  ToolTip="Select Bus Structure Name">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                            
                                        </asp:DropDownList>
                                     
                                    </div>

                                 <%--    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Upload Bus Structure</label>
                                                                </div>
                                                                <div id="Div1" class="logoContainer" runat="server">
                                                                    <asp:Image ID="imgUpFile" runat="server" ImageUrl="~/Images/default-fileupload.png" /><br />
                                                                      <div class="fileContainer sprite pl-1">
                                                                    <span runat="server" id="ufFile"
                                                                        cssclass="form-control" tabindex="7">Upload File</span>
                                                                    <asp:FileUpload ID="fuUploadBusStructure" runat="server" ToolTip="Select file to upload"
                                                                        CssClass="form-control" accept=".jpg,.jpeg,.JPG,.JPEG,.PNG" onchange="ShowpImagePreview(this);" onkeypress="" TabIndex="5" />
                                                                </div>
                                                                </div>
                                                              
                                                            </div>
                                </div>--%>
                                             <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Upload Bus Structure <small style="color: red;">(Max.Size<asp:Label ID="lblFileSize" runat="server" Font-Bold="true"></asp:Label>)</small></label>
                                        </div>
                                        <asp:FileUpload ID="fuUploadBusStructure" runat="server" EnableViewState="true" TabIndex="5"
                                            ToolTip="Click here to Attach File" />

                                        <asp:Label ID="lblBusStructure" runat="server" Text="Label" Visible="false"></asp:Label>
                                        <asp:HiddenField ID="hdnFile" runat="server" />
                                    </div>


                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShowBusStr" runat="server" CausesValidation="true" TabIndex="10"
                                        Text="Show Bus Structure" ValidationGroup="Submit" CssClass="btn btn-info" ToolTip="Click here to Show Bus Structure" OnClick="btnShowBusStr_Click" />
                                    

                                     <asp:Button ID="btnUploadStructure" runat="server" CausesValidation="true" TabIndex="10"
                                        Text="Upload Bus Structure" ValidationGroup="Submit" CssClass="btn btn-info" ToolTip="Click here to Upload Bus Structure" OnClick="btnUploadStructure_Click" />
                                    <asp:Button ID="btnSubmit" runat="server" TabIndex="12" Text="Submit" OnClick="btnSubmit_Click"
                                        CssClass="btn btn-primary" ToolTip="Click here to Submit" />

                                      <asp:Button ID="btnPaymentReport" runat="server" TabIndex="12" Text="Payment Report" OnClick="btnPaymentReport_Click"
                                        CssClass="btn btn-info" ToolTip="Click here to Show Report" />

                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false"
                                        TabIndex="11" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click"/>
                                     <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                            ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                </div>
                            </div>
                          

                           
                           <div class="col-12 mt-3">
                               <div class="row">
                               <div class="form-group col-lg-6 col-md-6 col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvstructure" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5></h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th></th>
                                                            <th style="text-align: center">Seats</th>
                                                            <th style="text-align: center">Status</th>
                                                            
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
                                               
                                                <td><asp:CheckBox ID="chkSeat" runat="server" Text=""  /></td>
                                                <td> <asp:Label ID="lblcount" runat="server" Text='<%# Eval("SrNo")%>'></asp:Label></td>
                                                <td> <asp:Label ID="lblselectedvalue" runat="server" Text='<%# Eval("STATUS_ID")%>'></asp:Label></td>
                                               
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>


                                </asp:Panel>
                                   </div>
                               
                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divStatus" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Status </label>
                                        </div>
                                        <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="3" AppendDataBoundItems="true"
                                            CssClass="form-control" data-select2-enable="true" ToolTip="Select Stop Name">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                        </asp:DropDownList>

                                    </div>

                                   </div>
                              
                            </div>

                            <div class="col-12 mt-3">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvBusStructure" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Route Entry List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th style="text-align: center">SR.NO. </th>
                                                            <th style="text-align: center">ROUTE</th>
                                                            <th style="text-align: center">ROUTE DETAILS </th>
                                                            <th style="text-align: center">BUS STRUCTURE </th>
                                                            <th style="text-align: center">BLOCK</th>
                                                            <th style="text-align: center">BOYS</th>
                                                            <th style="text-align: center">GIRLS</th>
                                                            <th style="text-align: center">STAFF</th>
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
                                                <td><%# Container.DataItemIndex + 1%></td>
                                                <td><%# Eval("ROUTENAME")%></td>
                                                <td><%# Eval("ROUTEPATH")%></td>
                                                <td><%# Eval("SEATING_STRUCTURE")%></td>
                                                <td><%# Eval("Block")%></td>
                                                <td><%#Eval("Boys")%></td>
                                                <td><%# Eval("Girls")%></td>
                                                <td><%#Eval("Staff")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>


                                </asp:Panel>
                            </div>

                             <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                            <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                            <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                        </div>
                        </div>
                    </div>
                </div>
            </div>
       
         </ContentTemplate>
        <Triggers>
          
            <asp:PostBackTrigger ControlID="btnUploadStructure" />
            <asp:PostBackTrigger ControlID="btnPaymentReport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

