<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CarrierMaster.aspx.cs" Inherits="Dispatch_Masters_CarrierMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <%--===== Data Table Script added by gaurav =====--%>
        <script>
            $(document).ready(function () {
                var table = $('#tblweight').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0, 1];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblweight').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                               {
                                   extend: 'copyHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0, 1];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblweight').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },
                               {
                                   extend: 'excelHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0, 1];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblweight').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },

                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                $(document).ready(function () {
                    var table = $('#tblweight').DataTable({
                        responsive: true,
                        lengthChange: true,
                        scrollY: 320,
                        scrollX: true,
                        scrollCollapse: true,
                        paging: false, // Added by Gaurav for Hide pagination

                        dom: 'lBfrtip',
                        buttons: [
                            {
                                extend: 'colvis',
                                text: 'Column Visibility',
                                columns: function (idx, data, node) {
                                    var arr = [0, 1];
                                    if (arr.indexOf(idx) !== -1) {
                                        return false;
                                    } else {
                                        return $('#tblweight').DataTable().column(idx).visible();
                                    }
                                }
                            },
                            {
                                extend: 'collection',
                                text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                                buttons: [
                                   {
                                       extend: 'copyHtml5',
                                       exportOptions: {
                                           columns: function (idx, data, node) {
                                               var arr = [0, 1];
                                               if (arr.indexOf(idx) !== -1) {
                                                   return false;
                                               } else {
                                                   return $('#tblweight').DataTable().column(idx).visible();
                                               }
                                           },
                                           format: {
                                               body: function (data, column, row, node) {
                                                   var nodereturn;
                                                   if ($(node).find("input:text").length > 0) {
                                                       nodereturn = "";
                                                       nodereturn += $(node).find("input:text").eq(0).val();
                                                   }
                                                   else if ($(node).find("input:checkbox").length > 0) {
                                                       nodereturn = "";
                                                       $(node).find("input:checkbox").each(function () {
                                                           if ($(this).is(':checked')) {
                                                               nodereturn += "On";
                                                           } else {
                                                               nodereturn += "Off";
                                                           }
                                                       });
                                                   }
                                                   else if ($(node).find("a").length > 0) {
                                                       nodereturn = "";
                                                       $(node).find("a").each(function () {
                                                           nodereturn += $(this).text();
                                                       });
                                                   }
                                                   else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                       nodereturn = "";
                                                       $(node).find("span").each(function () {
                                                           nodereturn += $(this).text();
                                                       });
                                                   }
                                                   else if ($(node).find("select").length > 0) {
                                                       nodereturn = "";
                                                       $(node).find("select").each(function () {
                                                           var thisOption = $(this).find("option:selected").text();
                                                           if (thisOption !== "Please Select") {
                                                               nodereturn += thisOption;
                                                           }
                                                       });
                                                   }
                                                   else if ($(node).find("img").length > 0) {
                                                       nodereturn = "";
                                                   }
                                                   else if ($(node).find("input:hidden").length > 0) {
                                                       nodereturn = "";
                                                   }
                                                   else {
                                                       nodereturn = data;
                                                   }
                                                   return nodereturn;
                                               },
                                           },
                                       }
                                   },
                                   {
                                       extend: 'excelHtml5',
                                       exportOptions: {
                                           columns: function (idx, data, node) {
                                               var arr = [0, 1];
                                               if (arr.indexOf(idx) !== -1) {
                                                   return false;
                                               } else {
                                                   return $('#tblweight').DataTable().column(idx).visible();
                                               }
                                           },
                                           format: {
                                               body: function (data, column, row, node) {
                                                   var nodereturn;
                                                   if ($(node).find("input:text").length > 0) {
                                                       nodereturn = "";
                                                       nodereturn += $(node).find("input:text").eq(0).val();
                                                   }
                                                   else if ($(node).find("input:checkbox").length > 0) {
                                                       nodereturn = "";
                                                       $(node).find("input:checkbox").each(function () {
                                                           if ($(this).is(':checked')) {
                                                               nodereturn += "On";
                                                           } else {
                                                               nodereturn += "Off";
                                                           }
                                                       });
                                                   }
                                                   else if ($(node).find("a").length > 0) {
                                                       nodereturn = "";
                                                       $(node).find("a").each(function () {
                                                           nodereturn += $(this).text();
                                                       });
                                                   }
                                                   else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                       nodereturn = "";
                                                       $(node).find("span").each(function () {
                                                           nodereturn += $(this).text();
                                                       });
                                                   }
                                                   else if ($(node).find("select").length > 0) {
                                                       nodereturn = "";
                                                       $(node).find("select").each(function () {
                                                           var thisOption = $(this).find("option:selected").text();
                                                           if (thisOption !== "Please Select") {
                                                               nodereturn += thisOption;
                                                           }
                                                       });
                                                   }
                                                   else if ($(node).find("img").length > 0) {
                                                       nodereturn = "";
                                                   }
                                                   else if ($(node).find("input:hidden").length > 0) {
                                                       nodereturn = "";
                                                   }
                                                   else {
                                                       nodereturn = data;
                                                   }
                                                   return nodereturn;
                                               },
                                           },
                                       }
                                   },

                                ]
                            }
                        ],
                        "bDestroy": true,
                    });
                });
            });

        </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">CARRIER MASTER</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading"><h5>Add Courier Carrier</h5></div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Carrier Name</label>
                                            </div>
                                            <asp:TextBox ID="txtCarrierName" runat="server" MaxLength="30"  CssClass="form-control" TabIndex="1" onkeypress="return CheckAlphabet(event,this);" ToolTip="Enter Carrier Name" />
                                            <asp:RequiredFieldValidator ID="rfvCarrierName" runat="server" ControlToValidate="txtCarrierName"
                                                Display="None" ErrorMessage="Please Enter Courier Carrier Name" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="2"
                                    OnClick="btnSubmit_Click"  CausesValidation="true"  CssClass="btn btn-primary" ToolTip="CLick here to Submit"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"  CssClass="btn btn-warning" ToolTip="Click here to Reset"
                                    CausesValidation="false" TabIndex="3" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"  ShowSummary="false" ValidationGroup="Submit" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvCarrier" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading"><h5>Courier Carrier List </h5></div>
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblweight">
                                                    <thead class="bg-light-blue">
                                                        <tr>                                    
                                                            <th>DELETE</th>
                                                            <th>ACTION</th>                                                      
                                                            <th>CARRIER NAME</th>    
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
                                                <td>
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("CARRIERNO")%>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                        OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("CARRIERNO")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                <td>
                                                    <%# Eval("CARRIERNAME")%>
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


        
            <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
            <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
            <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
                runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
                OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
                BackgroundCssClass="modalBackground" />
              <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                            <div class="text-center">
                                <div class="modal-content">
                                    <div class="modal-body">
                                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
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

            <%--END MODAL POPUP EXTENDER FOR DELETE CONFIRMATION --%>

            <script type="text/javascript" language="javascript">
                function validateNumeric(txt) {
                    if (isNaN(txt.value)) {
                        txt.value = '';
                        alert('Only Numeric Characters Allowed!');
                        txt.focus();
                        return;
                    }
                }
                function validateAlphabet(txt) {
                    var expAlphabet = /^[A-Za-z]+$/;
                    if (txt.value.search(expAlphabet) == -1) {
                        txt.value = txt.value.substring(0, (txt.value.length) - 1);
                        txt.value = '';
                        txt.focus = true;
                        alert("Only Alphabets allowed!");
                        return false;
                    }
                    else
                        return true;
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

