<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Weight.aspx.cs" Inherits="Dispatch_Masters_Weight" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


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
                                var arr = [0,1];
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
                                           var arr = [0,1];
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
                                           var arr = [0,1];
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

    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">WEIGHT MASTER</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Add/Edit Weight</h5>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Post Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPostType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                ToolTip="Select Post Type" TabIndex="1">
                                                <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvPostType" runat="server" ControlToValidate="ddlPostType"
                                                Display="None" InitialValue="0" ErrorMessage="Please select Post Type" SetFocusOnError="true"
                                                ValidationGroup="Submit" />
                                        </div>

                                      <%--  <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>From Weight</label>
                                            </div>
                                            <asp:TextBox ID="txtFromWeight" runat="server" CssClass="form-control" MaxLength="10"
                                                ToolTip="Enter From Weight" AutoPostBack="True" TabIndex="2" />--%>
                                            <%--onkeydown="return CheckNumeric(event,this);"--%>
                                       <%--     <asp:RequiredFieldValidator ID="rfvFromWeight" runat="server" ControlToValidate="txtFromWeight"
                                                Display="None" ErrorMessage="Please enter From Weight" SetFocusOnError="true"
                                                ValidationGroup="Submit" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbtxtContNo" runat="server" ValidChars="0123456789."
                                                FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtFromWeight">
                                            </ajaxToolKit:FilteredTextBoxExtender>--%>
                                            <%--gayatri 06-04-2022  onkeypress="return CheckNumeric(event,this);"--%>
                                       <%-- </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>From Weight</label>
                                            </div>
                                            <asp:TextBox ID="txtFromWeight" runat="server" CssClass="form-control" MaxLength="10"
                                                onkeypress="return CheckNumeric(event,this);" TabIndex="5" ToolTip="Enter Cost" />
                                            <asp:RequiredFieldValidator ID="rfvFromWeight" runat="server" ControlToValidate="txtFromWeight"
                                                Display="None" ErrorMessage="Please enter From Weight" SetFocusOnError="true" ValidationGroup="Submit" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbtxtContNo" runat="server" ValidChars="0123456789."
                                                FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtFromWeight">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <%--    gayatri rode 08/03/2022--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>To Weight</label>
                                            </div>
                                            <asp:TextBox ID="txtToWeight" runat="server" CssClass="form-control" MaxLength="10" OnTextChanged="txtToWeight_TextChanged"
                                                onkeypress="return CheckNumeric(event,this);" TabIndex="5" ToolTip="Enter To Weight" />
                                            <asp:RequiredFieldValidator ID="rfvToWeight" runat="server" ControlToValidate="txtToWeight"
                                                Display="None" ErrorMessage="Please enter To Weight." SetFocusOnError="true" ValidationGroup="Submit" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" ValidChars="0123456789."
                                                FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtToWeight">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <%--    gayatri rode 08/03/2022--%>
                                        </div>












<%--                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>To Weight</label>
                                            </div>
                                            <asp:TextBox ID="txtToWeight" runat="server" MaxLength="10" CssClass="form-control"
                                                OnTextChanged="txtToWeight_TextChanged"
                                                AutoPostBack="True" TabIndex="3" ToolTip="Enter To Weight" />
                                            <%-- onkeydown="return CheckNumeric(event,this);"--%>
                                          <%--  <asp:RequiredFieldValidator ID="rfvToWeight" runat="server" ControlToValidate="txtToWeight"
                                                Display="None" ErrorMessage="Please enter To Weight." SetFocusOnError="true"
                                                ValidationGroup="Submit" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="0123456789."
                                                FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtToWeight">
                                            </ajaxToolKit:FilteredTextBoxExtender>--%>
                                            <%--gayatri 06-04-2022  onkeypress="return CheckNumeric(event,this);"--%>
                                       <%-- </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Unit</label>
                                            </div>
                                            <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select Unit" TabIndex="4">

                                                <asp:ListItem Value="0">GM</asp:ListItem>
                                                <asp:ListItem Value="1">KG</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvUnit" runat="server" ControlToValidate="ddlUnit"
                                                Display="None" ErrorMessage="Please select unit" SetFocusOnError="true"
                                                ValidationGroup="Submit" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Cost in Rs</label>
                                            </div>
                                            <asp:TextBox ID="txtCost" runat="server" CssClass="form-control" MaxLength="10"
                                                onkeypress="return CheckNumeric(event,this);" TabIndex="5" ToolTip="Enter Cost" />
                                            <asp:RequiredFieldValidator ID="rfvCost" runat="server" ControlToValidate="txtCost"
                                                Display="None" ErrorMessage="Please enter Cost" SetFocusOnError="true" ValidationGroup="Submit" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredtxtCost" runat="server" ValidChars="0123456789."
                                                FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtCost">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <%--    gayatri rode 08/03/2022--%>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit"
                                    OnClick="btnSubmit_Click" TabIndex="6" CssClass="btn btn-primary" ToolTip="CLick here to Submit" AutoPostBack="True" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    TabIndex="7" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvWeight" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>WEIGHT LIST </h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblweight">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>DELETE</th>
                                                            <th>ACTION
                                                            </th>
                                                            <th>FROM WEIGHT
                                                            </th>
                                                            <th>TO WEIGHT
                                                            </th>
                                                            <th>UNIT
                                                            </th>
                                                            <th>COST
                                                            </th>
                                                            <th>POST TYPE 
                                                            </th>
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
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("WEIGHTNO")%>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                        OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                        CommandArgument='<%# Eval("WEIGHTNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click" TabIndex="13" />
                                                </td>
                                                <td>
                                                    <%# Eval("WEIGHTFROM")%>
                                                </td>
                                                <td>
                                                    <%# Eval("WEIGHTTO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("UNIT")%>
                                                </td>
                                                <td>
                                                    <%# Eval("COST", "{0:N2}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("POSTTYPENAME")%>
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

            <script language="javascript" type="text/javascript">
                function IsNumeric(textbox) {
                    if (textbox != null && textbox.value != "") {
                        if (isNaN(textbox.value)) {
                            document.getElementById(textbox.id).value = 0;
                        }
                    }
                }
            </script>

        </ContentTemplate>
        <%-- <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" />--%>
        <%-- <asp:PostBackTrigger ControlID="btnSubmit" />--%>
        <%--</Triggers>--%>
    </asp:UpdatePanel>
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

        function CheckNumeric(event, obj) {                                                    //gayatri rode 13-01-2021
            var k = (window.event) ? event.keyCode : event.which;
            //alert(k);
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k > 45 && k < 58) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter numeric Value');
                obj.focus();
            }
            return false;
        }
    </script>
</asp:Content>

