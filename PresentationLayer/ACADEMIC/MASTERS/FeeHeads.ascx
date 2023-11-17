<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FeeHeads.ascx.cs" Inherits="Academic_Masters_FeeHeads"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<%--===== Data Table Script added by gaurav =====--%>
<script>
    $(document).ready(function () {
        var table = $('#mytable').DataTable({
            responsive: true,
            lengthChange: true,
            scrollY: 450,
            scrollX: true,
            scrollCollapse: true,

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
                            return $('#mytable').DataTable().column(idx).visible();
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
                                            return $('#mytable').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).html();
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
                                            return $('#mytable').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).html();
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
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },
                            {
                                extend: 'pdfHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#mytable').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).html();
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
            var table = $('#mytable').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 450,
                scrollX: true,
                scrollCollapse: true,

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
                                return $('#mytable').DataTable().column(idx).visible();
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
                                                return $('#mytable').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
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
                                                return $('#mytable').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
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
                                                else {
                                                    nodereturn = data;
                                                }
                                                return nodereturn;
                                            },
                                        },
                                    }
                                },
                                {
                                    extend: 'pdfHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#mytable').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
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

 <script>
     
  
     //$(function () {
     //    $("input").change(function (e) {
     //        e.preventDefault();
     //        var table = $('#tblFeeHead').DataTable();
     //        table.$("input[type=checkbox]").prop("checked", false);
     //        $(this).prop("checked", true);
     //    });
     //});

     $(function () {
         $("#tblFeeHead tr td:nth-of-type(5) input:checkbox").on('click', function () {
            // alert();
             var $box = $(this);
             if ($box.is(":checked")) {
                 var group = $("tr td:nth-of-type(5) input:checkbox");
                 var group1 = $(this).closest("tr").find("input:checkbox");
                 group.prop("checked", false);
                 group1.prop("checked", false);
                 $box.prop("checked", true);
             } else {
                 $box.prop("checked", false);
             }
             //e.preventDefault();
             //var table = $('#tblFeeHead').DataTable();
             //table.$("input[type=checkbox]").prop("checked", false);
             //$(this).prop("checked", true);
         });
         $("#tblFeeHead tr td:nth-of-type(6) input:checkbox").on('click', function () {
             // alert();
             var $box = $(this);
             if ($box.is(":checked")) {
                 var group = $("tr td:nth-of-type(6) input:checkbox");
                 var group1 = $(this).closest("tr").find("input:checkbox");
                 group.prop("checked", false);
                 group1.prop("checked", false);
                 $box.prop("checked", true);
             } else {
                 $box.prop("checked", false);
             }
             //e.preventDefault();
             //var table = $('#tblFeeHead').DataTable();
             //table.$("input[type=checkbox]").prop("checked", false);
             //$(this).prop("checked", true);
         });
         $("#tblFeeHead tr td:nth-of-type(7) input:checkbox").on('click', function () {
             // alert();
             var $box = $(this);
             if ($box.is(":checked")) {
                 var group = $("tr td:nth-of-type(7) input:checkbox");
                 var group1 = $(this).closest("tr").find("input:checkbox");
                 //group.prop("checked", false);
                 group1.prop("checked", false);
                 $box.prop("checked", true);                 
                 var checkedCount = $("tr td:nth-of-type(7) input:checkbox:checked").length;
                 //alert(checkedCount);
                 if (checkedCount > 2) {
                     group.prop("checked", false);
                 }
                     
                 //    group1.prop("checked", false);
                 //}
                 //else {
                 //    //group1.prop("checked", true);

                 //}
                 //{
                 //    //$("tr td:nth-of-type(7) input:checkbox").prop('checked', true);
                 //    group.prop("checked", true);

                 //}
                 //else
                 //{
                 //    group.prop("checked", false);
                 //}
                 
                 //group1.prop("checked", false);
                 //$box.prop("checked", true);
             }
             else {
                 $box.prop("checked", false);
             }
             //e.preventDefault();
             //var table = $('#tblFeeHead').DataTable();
             //table.$("input[type=checkbox]").prop("checked", false);
             //$(this).prop("checked", true);
         });

        
       
     });

    </script>
<div class="row">
    <div class="col-md-12 col-sm-12 col-12">
        <div class="box box-primary">
            <%--<div id="div2" runat="server"></div>--%>
            <div class="box-header with-border">
               <h3 class="box-title">
                                Fees Head</h3>
            </div>
            <div class="box-body">
                <div class="col-12">
                    <div class="row">
                        <div class="form-group col-lg-3 col-md-6 col-12">
                            <div class="label-dynamic">
                                <sup>*</sup>
                                <label>Select Receipt Type</label>
                            </div>
                            <asp:DropDownList ID="ddlRecType" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                OnSelectedIndexChanged="ddlRecType_SelectedIndexChanged" AppendDataBoundItems="true"
                                ValidationGroup="Fees" ToolTip="Please Select Receipt Type" TabIndex="1">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvRecType" runat="server" ErrorMessage="Please Select Receipt Type"
                                Display="None" ControlToValidate="ddlRecType" InitialValue="0" SetFocusOnError="true"
                                ValidationGroup="Fees" />
                        </div>
                    </div>
                </div>
                <asp:Panel ID="pnlfees" runat="server">
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="Fees"
                            OnClick="btnSave_Click" CssClass="btn btn-primary" TabIndex="2" />
                        <asp:Button ID="btnReport" runat="server" TabIndex="3" Text="Report"
                            OnClick="btnReport_Click" Enabled="False" CssClass="btn btn-info" Visible="false" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                            OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="4" />

                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Fees"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        <div style="text-align: center">
                            <asp:Label ID="lblerror" runat="server" SkinID="Errorlbl" />
                            <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg" />
                        </div>
                    </div>
                    <div class="col-12">
                        <asp:ListView ID="lvFeesHead" runat="server">
                            <LayoutTemplate>
                                <div class="vista-grid">
                                    <div class="sub-heading">
                                        <h5>Fees Heads Defination</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblFeeHead">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Head
                                                </th>
                                                <th>Long Name
                                                </th>
                                                <th>Short Name
                                                </th>
                                                <th>Receipt Group
                                                </th>
                                                <th id="thScholarship" runat="server">Is Scholarship
                                                </th>
                                                   <th id="thReactivateStudent" runat="server">Student's Reactivation Fee
                                                </th>
                                                 <th id="thIsCautionMoney" runat="server">Is Caution Money
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
                                        <asp:Label ID="txtHead" runat="server" Text='<%#Eval("FEE_HEAD")%>' ToolTip='<%#Eval("FEE_TITLE_NO")%>'
                                            Width="10px" Font-Bold="true" />
                                    </td>

                                    <td>
                                        <asp:TextBox ID="txtLName" MaxLength="30" runat="server" Text='<%#Eval("FEE_LONGNAME")%>'
                                            CssClass="form-control" onchange="return FeeheadValidation(this);" />                                       

                                    </td>

                                    <td>
                                        <asp:TextBox ID="txtSName" MaxLength="8" runat="server" Text='<%#Eval("FEE_SHORTNAME")%>'
                                            CssClass="form-control" onchange="return FeeheadShortNameValidation(this);" />
                                         <asp:CustomValidator ClientValidationFunction="ValidateShortName" ControlToValidate="txtLName"
                                            Display="None" EnableClientScript="true" ErrorMessage="Please Enter Short Name as well."
                                            ID="valShortName" runat="server" ValidateEmptyText="false" ValidationGroup="Fees" />
                                    </td>

                                    <td>
                                        <asp:DropDownList ID="ddlReceiptShow" runat="server" data-select2-enable="true" CssClass="form-control" AppendDataBoundItems="true"
                                            CausesValidation="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdn_fld" runat="server" Value='<%#Eval("RECEIPTSHOW")%>' />
                                    </td>

                                     <td id="tdScholarship" runat="server">
                                           <asp:CheckBox ID="chkIsScholarship"  runat="server"  tooltip='<%#Eval("ISSCHOLARSHIP")%>'    />
                                       </td>
                                     <td id="tdIsReactivateStudent" runat="server">
                                           <asp:CheckBox ID="chkIsReactivateStudent"  runat="server"  tooltip='<%#Eval("ISREACTIVATESTUDENT")%>'    />
                                       </td>

                                      <td id="tdIsCautionMoney" runat="server">
                                           <asp:CheckBox ID="ChkIsCautionMoney"  runat="server"  tooltip='<%#Eval("ISCAUTION_MONEY")%>'    />
                                       </td>

                                </tr>
                            </ItemTemplate>
                        </asp:ListView>

                        <%-- <div>
                                <asp:Label ID="lblerror" runat="server" SkinID="Errorlbl" />
                            <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg" />
                                </div>--%>
                        <div id="divMsg" runat="server">
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript" language="javascript">

    function ValidateShortName(sender, args) {
        try {
            if (args.Value.length > 0 && sender.id != '') {
                var txtShortNameId = sender.id.substr(0, sender.id.indexOf('valShortName'));
                txtShortNameId += 'txtSName';

                if (txtShortNameId != null && txtShortNameId != '') {
                    var txtShortName = document.getElementById(txtShortNameId);
                    if (txtShortName != null && txtShortName.value.trim() == '') {
                        args.IsValid = false;
                        document.getElementById(txtShortName.id).focus();
                    }
                }
            }
        }
        catch (e) {
            alert("Error: " + e.description);
        }
    }

    //ctl00_ContentPlaceHolder1_ucFeeshead_lvFeesHead_ctrl1_txtHead


    function FeeheadValidation(txt) {
        try {
            debugger;
           // alert('A');
            var count = $("[id*=tblFeeHead] td").closest('tr').length;

            for (var i = 0; i < count; i++) {
                var txtvalue = document.getElementById('ctl00_ContentPlaceHolder1_ucFeeshead_lvFeesHead_ctrl' + i + '_txtLName').value;
                //alert(txtvalue);
                var id = 'ctl00_ContentPlaceHolder1_ucFeeshead_lvFeesHead_ctrl' + i + '_txtLName';
                if (txt.value.toUpperCase().trim() == txtvalue.toUpperCase().trim() && txt.id != id) {
                    alert(txtvalue + ' Long Name Already Exists.');
                    txt.value = '';
                    txt.focus();
                    return false;
                }
            }
        }
        catch (err) {
            alert("Error Messege : " + err.message);
        }
    }


    function FeeheadShortNameValidation(txt) {
        try {
            debugger;
            // alert('A');
            var count = $("[id*=tblFeeHead] td").closest('tr').length;

            for (var i = 0; i < count; i++) {

                var txtvalue = document.getElementById('ctl00_ContentPlaceHolder1_ucFeeshead_lvFeesHead_ctrl' + i + '_txtSName').value;
               // txtvalue = txtvalue.toUpperCase();
                //alert(txtvalue);
                var id = 'ctl00_ContentPlaceHolder1_ucFeeshead_lvFeesHead_ctrl' + i + '_txtSName';
                if (txt.value.toUpperCase().trim() == txtvalue.toUpperCase().trim() && txt.id != id) {
                    alert(txtvalue + ' Short Name Already Exists.');
                    txt.value = '';
                    txt.focus();
                    return false;
                }
            }
        }
        catch (err) {
            alert("Error Messege : " + err.message);
        }
    }


</script>

<%--<script type="text/javascript" language="javascript">
    function cbChange(obj) {
        debugger;
        alert('hello');
        var instate = (obj.checked);
        alert(instate);
        var cbs = document.getElementById("ctl00_ContentPlaceHolder1_ucFeeshead_lvFeesHead_ctrl0_chkIsScholarship");
        alert(cbs);
        for (var i = 0; i < cbs.value; i++) {
            alert('h2');
    
        cbs[i].checked = false;
    }
    if(instate)obj.checked = true;
}
</script>--%>

<%--<script type="text/javascript" language="javascript">
    $('#ctl00_ContentPlaceHolder1_ucFeeshead_lvFeesHead_ctrl0_chkIsScholarship').click(function () {
        alert('a');
    if($( 'input[name="option1"]:checked' ).length>0){//any one is checked
        $('.button input').prop('checked', false);
    }
    else{
        $('.button input').prop('checked', true);
    }
    })
</script>--%>