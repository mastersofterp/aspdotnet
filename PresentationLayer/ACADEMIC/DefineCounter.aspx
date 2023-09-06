<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="DefineCounter.aspx.cs" Inherits="DefineCounter"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
     <%--<script src="../../INCLUDES/prototype.js" type="text/javascript"></script>
    <script src="../../INCLUDES/scriptaculous.js" type="text/javascript"></script>
    <script src="../../INCLUDES/modalbox.js" type="text/javascript"></script>--%>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updplRoom"
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
        .checkbox-list-box {
            height: 46px;
        }
         @media (max-width: 767px) {
            .checkbox-list-box {
                min-height: 25px; 
                height: auto;
            }
        }
    </style>

    <asp:UpdatePanel ID="updplRoom" runat="server">
        <ContentTemplate>

            <script type="text/javascript">
                // Move an element directly on top of another element (and optionally
                // make it the same size)
                function Cover(bottom, top, ignoreSize) {
                    var location = Sys.UI.DomElement.getLocation(bottom);
                    top.style.position = 'absolute';
                    top.style.top = location.y + 'px';
                    top.style.left = location.x + 'px';
                    if (!ignoreSize) {
                        top.style.height = bottom.offsetHeight + 'px';
                        top.style.width = bottom.offsetWidth + 'px';
                    }
                }
            </script>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">DEFINE COUNTER</h3>
                        </div>
                        <div class="box-body">                           
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Counter Name</label>
                                        </div>
                                        <asp:TextBox ID="txtCounterName" runat="server" TabIndex="1"/>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCounterName"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please enter counter name." />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Short Print Name</label>
                                        </div>
                                        <asp:TextBox ID="txtPrintName" runat="server" TabIndex="2"/>
                                        <asp:RequiredFieldValidator ID="valPrintName" runat="server" ControlToValidate="txtPrintName"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please enter print name." />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Counter User</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCounterUser" runat="server" AppendDataBoundItems="true" TabIndex="3"  data-select2-enable="true"/>
                                        <asp:RequiredFieldValidator ID="valCounterUser" runat="server" ControlToValidate="ddlCounterUser"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please enter counter user."
                                            InitialValue="0" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Permission for Receipts</label>
                                        </div>
                                        <div class="form-group col-md-12 checkbox-list-box">
                                            <asp:RadioButtonList ID="chkListReceiptTypes" runat="server" CellPadding="3" CellSpacing="2" RepeatColumns="2" CssClass="checkbox-list-style">
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="Submit"  OnClick="btnSubmit_Click"/> 
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummary" runat="server"   DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" />
                            </div>

                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>List of Counters</h5>
                                </div>
                                <asp:ListView ID="lvCounters" runat="server">
                                    <LayoutTemplate>       
                                        <table id="tblHead"  class="table table-striped table-bordered nowrap display"  style="width: 100%;">
                                            <thead class="bg-light-blue">
                                                <tr id="trRow">
                                                    <th>Edit
                                                    </th>
                                                    <th>Counter Name
                                                    </th>
                                                    <th>Print Name
                                                    </th>
                                                    <th>Counter User
                                                    </th>
                                                    <th>Permission for Receipts
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                    CommandArgument='<%# Eval("COUNTERNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                    OnClick="btnEdit_Click" TabIndex="10" />
                                            </td>
                                            <td>
                                                <%# ((Eval("COUNTERNAME").ToString() != string.Empty)? Eval("COUNTERNAME") : "--") %>
                                            </td>
                                            <td>
                                                <%# ((Eval("PRINTNAME").ToString() != string.Empty) ? Eval("PRINTNAME") : "--") %>
                                            </td>
                                            <td>
                                                <%# ((Eval("UA_FULLNAME").ToString() != string.Empty) ? Eval("UA_FULLNAME") : "--") %>
                                            </td>
                                            <td>
                                                <%# ((Eval("RECEIPT_PERMISSION").ToString() != string.Empty) ? Eval("RECEIPT_PERMISSION") : "--")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

     <script type="text/javascript">
         //$(function () {
         //    debugger;
         //    //Attach the validate class to each RadioButton.
         //    $("table[id*=chkListReceiptTypes]").validationEngine('attach', { promptPosition: "topRight" });
         //    $("table[id*=chkListReceiptTypes] input").addClass("validate[required]");
         //    $("[id*=btnSubmit]").bind("click", function () {
         //        if (!$("table[id*=chkListReceiptTypes]").validationEngine('validate')) {
         //            return false;
                     
         //        }
         //        return true;
         //        alert('success');
         //    });
         //});
         debugger;
        
         $(document).ready(function() {
             debugger;
             $('#btnSubmit').on('click', function(e) {
                 var cnt = $("#chkListReceiptTypes :radio:checked").length;
                 if (cnt == 0)
                 {
                     alert('Select any option.');

                     e.preventDefault();

                 }

                 else

                     alert('Well Done!!!!');

             });

         });​
         </script>
</asp:Content>
