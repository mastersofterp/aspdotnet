<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CompanyCollegeMapping.aspx.cs" Inherits="ACCOUNT_CompanyCollegeMapping"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .checkbox {
            padding-left: 20px;
        }

            .checkbox label {
                display: inline-block;
                vertical-align: middle;
                position: relative;
                padding-left: 5px;
            }

                .checkbox label::before {
                    content: "";
                    display: inline-block;
                    position: absolute;
                    width: 17px;
                    height: 17px;
                    left: 0;
                    margin-left: -20px;
                    border: 1px solid #cccccc;
                    border-radius: 3px;
                    background-color: #fff;
                    -webkit-transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
                    -o-transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
                    transition: border 0.15s ease-in-out, color 0.15s ease-in-out;
                }

                .checkbox label::after {
                    display: inline-block;
                    position: absolute;
                    width: 16px;
                    height: 16px;
                    left: 0;
                    top: 0;
                    margin-left: -20px;
                    padding-left: 3px;
                    padding-top: 1px;
                    font-size: 11px;
                    color: #555555;
                }

            .checkbox input[type="checkbox"] {
                opacity: 0;
                z-index: 1;
            }

                .checkbox input[type="checkbox"]:checked + label::after {
                    font-family: 'Font Awesome 5 Free';
                    content: "\f00c";
                }

        .checkbox-primary input[type="checkbox"]:checked + label::before {
            background-color: #337ab7;
            border-color: #337ab7;
        }

        .checkbox-primary input[type="checkbox"]:checked + label::after {
            color: #fff;
        }
    </style>

    <script type="text/javascript">
        function CallConfirmBox() {
            //if (confirm("Do You Want To Remove This College")) {
            //    //OK - Do your stuff or call any callback method here..
            //    alert("Removed Successfully!");
            //} else {
            //    //CANCEL - Do your stuff or call any callback method here..
            //    alert("You pressed Cancel!");
            //}
         
                if (confirm("Are you sure you want to delete ...?")) {
                    return true;
                }
            
                return false;
          
        }
    </script>
        <%--===== Data Table Script added by gaurav =====--%>
       <%-- <script>
            $(document).ready(function () {
                var table = $('#colg_tbl').DataTable({
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
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#colg_tbl').DataTable().column(idx).visible();
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
            var arr = [0];
            if (arr.indexOf(idx) !== -1) {
                return false;
            } else {
                return $('#colg_tbl').DataTable().column(idx).visible();
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
            var arr = [0];
            if (arr.indexOf(idx) !== -1) {
                return false;
            } else {
                return $('#colg_tbl').DataTable().column(idx).visible();
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
                    var table = $('#colg_tbl').DataTable({
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
                                    var arr = [0];
                                    if (arr.indexOf(idx) !== -1) {
                                        return false;
                                    } else {
                                        return $('#colg_tbl').DataTable().column(idx).visible();
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
               var arr = [0];
               if (arr.indexOf(idx) !== -1) {
                   return false;
               } else {
                   return $('#colg_tbl').DataTable().column(idx).visible();
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
               var arr = [0];
               if (arr.indexOf(idx) !== -1) {
                   return false;
               } else {
                   return $('#colg_tbl').DataTable().column(idx).visible();
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

        </script>--%>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UPDMainGroup"
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
    <asp:UpdatePanel ID="UPDMainGroup" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ASSIGN COLLEGE TO COMPANY</h3>
                        </div>
                        <div class="box-body">
                            <div id="divCompName" runat="server">
                            </div>
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Company Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"
                                                ValidationGroup="show">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvUserType" runat="server" ValidationGroup="Add"
                                                Display="None" ErrorMessage="Please Select Company" ControlToValidate="ddlCompany"
                                                InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                     <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnShow_Click"  ValidationGroup="Add"/>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="Add"
                                        OnClick="btnAdd_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" CausesValidation="false"
                                        OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Add" />
                                    <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                </div>


                                <asp:Panel ID="pnlListMain" runat="server" Visible="false">

                                    <asp:UpdatePanel ID="updCashBookAssign" runat="server">
                                    </asp:UpdatePanel>
                                    <div class="col-12 mt-3">
                                        <asp:ListView ID="lstCollege" runat="server">
                                            <LayoutTemplate>
                                                <div id="demo-grid">
                                                    <div class="sub-heading">
                                                        <h5>College List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="colg_tbl">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th></th>
                                                                <th>Company Name
                                                                </th>
                                                                <th>College Name
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
                                                <tr class="item">
                                                    <td>
                                                        <asp:ImageButton runat="server" ID="btnDelete" CommandArgument='<%# Container.DataItemIndex + 1%>'
                                                            ImageUrl="~/Images/delete.png" OnClick="btnDelete_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblCompanyName" Text='<%# Eval("COMPANY_NAME") %>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hdnCompanyId" Value='<%# Eval("COMPANY_NO") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label1" Text='<%# Eval("COLLEGE_NAME") %>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hdnCollegeId" Value='<%# Eval("COLLEGE_ID") %>' />
                                                    </td>
                                                    <td>
                                                        <div id="divCashBook" runat="server">
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="altitem">
                                                    <td>
                                                        <asp:ImageButton runat="server" ID="btnDelete" CommandArgument='<%# Container.DataItemIndex + 1%>'
                                                            ImageUrl="~/Images/delete.png"  OnClick="btnDelete_Click" />

                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblCompanyName" Text='<%# Eval("COMPANY_NAME") %>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hdnCompanyId" Value='<%# Eval("COMPANY_NO") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label1" Text='<%# Eval("COLLEGE_NAME") %>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hdnCollegeId" Value='<%# Eval("COLLEGE_ID") %>' />
                                                    </td>
                                                    <td>
                                                        <div id="divCashBook" runat="server">
                                                        </div>
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:ListView>

                                    </div>
                                    <div class="col-12 btn-footer mt-3" id="trSubmit" runat="server">
                                          
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnClear" runat="server" Text="Cancel" CssClass="btn btn-warning" CausesValidation="false"
                                            OnClick="btnClear_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="show" />
                                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                                    </div>

                                </asp:Panel>
                                <div class="col-12 mt-3 mb-4">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Select College</label>
                                            </div>
                                            <div class="form-group col-md-12 checkbox-list-box">
                                                <asp:Panel ID="pnlTree" runat="server" Height="300px">
                                                    <asp:CheckBoxList ID="chkCollege" runat="server" CssClass="checkbox checkbox-primary checkbox-list-style" Font-Size="Smaller">
                                                    </asp:CheckBoxList>
                                                </asp:Panel>
                                            </div>
                                        </div>

                                    </div>
                                </div>


                               
                                 <asp:Panel ID="pnldata" runat="server" Visible="false">
                                     
                                    <div class="col-12 mt-3">
                                        <asp:ListView ID="listdata" runat="server">
                                            <LayoutTemplate>
                                                <div id="demo-grid">
                                                    
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="colg_tbl">
                                                        <thead >
                                                            <tr>
                                                               
                                                                <th>Company Name
                                                                </th>
                                                                <th>College Name
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
                                                <tr class="altitem">
                                                    
                                                    <td>
                                                        <asp:Label runat="server" ID="lblCompanyName" Text='<%# Eval("COMPANY_NAME") %>'></asp:Label>
                                                        
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="Label1" Text='<%# Eval("COLLEGE_NAME") %>'></asp:Label>
                                                        
                                                    </td>
                                                    <td>
                                                        <div id="divCashBook" runat="server">
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        
                                        </asp:ListView>

                                    </div>
                                  
                                </asp:Panel>
                            
                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">
        function totAllSubjects(headchk) {
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (e.name.endsWith('chkAccept')) {
                        if (headchk.checked == true) {
                            e.checked = true;
                            hdfTot.value = Number(hdfTot.value) + 1;
                        }
                        else
                            e.checked = false;
                    }
                }
            }

            if (headchk.checked == false) hdfTot.value = "0";
        }

        function validateAssign() {
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>').value;

            if (hdfTot == 0) {
                alert('Please Select Atleast One User from User List');
                return false;
            }
            else
                return true;
        }
    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
