<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" 
    CodeFile="StudentNameUpdationByExcel.aspx.cs" Inherits="ACADEMIC_StudentNameUpdationByExcel" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
     <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size:50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>BASIC DETAILS UPDATION</b></h3>  
                             <asp:Label ID="lblTotalMsg" Style="font-weight: bold; color: red;" runat="server"></asp:Label>                         
                        </div>
                        <form role="form">
                            <div class="box-body">
                               
                                <div class="form-group col-md-5">
                                    <fieldset class="fieldset">
                                            <legend class="legend" style="text-align:center;">Download Format</legend>
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lbExcelFormat" OnClick="lbExcelFormat_Click" runat="server" >Pre-requisite excel format for upload</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                </div>

                                 <div class="form-group col-md-6">
                                    <label>Excel File</label>
                                    <asp:FileUpload ID="FileUpload1"  runat="server" ToolTip="Select file to Import" />
                                    
                                </div>
                                </div>

                                 <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnShow" runat="server" Text="UPLOAD"  CssClass="btn btn-success" OnClick="btnShow_Click"
                                        />
                                     
                               </div>
                               
                                
    </div>
                        </form>
                    
                        </ContentTemplate>
          <Triggers>
                         
                            <asp:PostBackTrigger ControlID="lbExcelFormat" />
                        </Triggers>
                            </asp:UpdatePanel>
                        </asp:Content>

