<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ITLE_Import_Data.aspx.cs" Inherits="Itle_ITLE_Import_Data" Title="" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .br-pnel {
            border: 1px solid #ccc;
            padding: 15px 0px;
            border-radius: 8px;
            height:250px;
        }
        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">IMPORT QUESTION BANK</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlImport" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-lg-5 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Session :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblSession" runat="server" Font-Bold="True" ForeColor="#006600"></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-lg-7 col-md-12 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Course Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblCorseName" runat="server" Font-Bold="True" ForeColor="#006600"></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>
                                 
                            </div>
                        </div>

                        <div class="col-12 mt-3">
                            <div class="row">
                                <div class="col-md-12 col-lg-6 col-12">
                                    <div class="br-pnel">
                                        <asp:Panel ID="pnlImoprtFrmExcel" runat="server">
                                             
                                            <div class="col-12">
                                                
                                                <div class="sub-heading">
                                                    <h5>Import Question Bank </h5>
                                                </div>
                                                 <div class="row">
                                              <div class="col-md-12 col-lg-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>File Format</label>
                                                </div>
                                                <asp:DropDownList ID="ddlfileformate" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                     TabIndex="1" AutoPostBack="true" ToolTip="Select Formate" OnSelectedIndexChanged="ddlfileformate_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                              </div>
                                              <div class="col-md-12 col-lg-6 col-12" id="divtopic" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Topic Name</label>
                                                </div>
                                               <asp:TextBox ID="txtTopicName" runat="server" ></asp:TextBox>
                                                 </div>
                                                </div>
                                              <br />
                                                <div>
                                                <asp:FileUpload ID="fuRFIDFILE" runat="server" TabIndex="1" ToolTip="Click here to attach File" />
                                                </div>
                                            </div>
                                            
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnAdd" runat="server" Text="Show" OnClick="btnAdd_Click" CssClass="btn btn-primary"
                                                    ToolTip="Click here to Show" TabIndex="2" />
                                                <asp:HiddenField ID="hfRF" runat="server" />
                                                <asp:Button ID="btnSubmitToDatabase" runat="server" Text="Submit To Database" CssClass="btn btn-primary"
                                                    OnClick="btnSubmitToDatabase_Click" ToolTip="Click here to Submit To Database" TabIndex="3" Visible="false" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="4"
                                                    OnClick="btnCancel_Click" ToolTip="Click here to Reset" />
                                                <asp:HiddenField ID="hfAccno" runat="server" />
                                                <asp:HiddenField ID="hfAcno" runat="server" />
                                                <asp:HiddenField ID="hfAccSer" runat="server" />

                                            </div>
                                      
                                           
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-12" runat="server" id="divdownloadfile">
                                    <div class="br-pnel">
                                        <asp:Panel ID="pnlDownloadEcelFile" runat="server">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Download Sample Template File</h5>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <%--<asp:ImageButton ID="imgbtnDownloadExcelFile" runat="server" ToolTip="Download Sample Excel File"
                                                    ImageUrl="~/images/excel.jpeg" Height="35px" Width="35px" AlternateText="Download Excel File Sample" />--%>

                                                <asp:HyperLink ID="hlnkDownload" runat="server" Font-Bold="True" Font-Size="13px" TabIndex="5"
                                                    Text="Click here to Download" NavigateUrl="~\ITLE\upload_files\QuestionBank\download_Sample_file\QUESTION.xls"
                                                    Target="_blank" ToolTip="Click here to Download File"></asp:HyperLink>

                                                   <asp:HyperLink ID="hlnkAikenDwonload" runat="server" Font-Bold="True" Font-Size="13px" TabIndex="5" Visible="false"
                                                    Text="Click here to Download" NavigateUrl="~\ITLE\upload_files\QuestionBank\download_Sample_file\QUESTION.txt"
                                                    Target="_blank" ToolTip="Click here to Download File"></asp:HyperLink>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>

                            </div>
                            </div>
                        
                    </asp:Panel>
                    <asp:Panel ID="pnlTotalQuestions" runat="server" Visible="false">
                        <div class="col-12 mt-3">
                            <div class="row">
                                <div class="col-lg-3 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Total Number of Questions are :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblTotalQues" runat="server" Text=""></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-lg-3 col-md-6 col-12">
                                    <asp:GridView ID="lvBooksInLib" runat="server" CssClass="vista-grid" HeaderStyle-BackColor="ActiveBorder">
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                </div>
            </div>
        
    </div>
   
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div class="text-center">
            <div class="modal-content">
                <div class="modal-body">
                    <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.png" />
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                    <div class="text-center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }
    </script>
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

