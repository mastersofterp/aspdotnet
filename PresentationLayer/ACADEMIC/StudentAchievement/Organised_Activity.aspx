<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Organised_Activity.aspx.cs" Inherits="ACADEMIC_StudentAchievement_Organised_Activity" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script>
        $(document).ready(function () {
            var table = $('#tblEvent').DataTable({
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
                                return $('#tblEvent').DataTable().column(idx).visible();
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
                return $('#tblEvent').DataTable().column(idx).visible();
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
                        nodereturn += $(this).html();
                    });
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
                return $('#tblEvent').DataTable().column(idx).visible();
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
                        nodereturn += $(this).html();
                    });
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
                var table = $('#tblEvent').DataTable({
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
                                    return $('#tblEvent').DataTable().column(idx).visible();
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
                    return $('#tblEvent').DataTable().column(idx).visible();
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
                            nodereturn += $(this).html();
                        });
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
                var arr = [0, 5];
                if (arr.indexOf(idx) !== -1) {
                    return false;
                } else {
                    return $('#tblEvent').DataTable().column(idx).visible();
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
                            nodereturn += $(this).html();
                        });
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
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

       <style>
        input[type=checkbox], input[type=radio] {
            margin: 0px 3px 0;
        }
    </style>

       <script type="text/javascript">
           $(document).ready(function () {
               $('#picker').daterangepicker({
                   startDate: moment().subtract(00, 'days'),
                   endDate: moment(),
                   locale: {
                       format: 'DD MMM, YYYY'
                   },
                   ranges: {
                   },
               },
           function (start, end) {
               $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
               document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
          });

              $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
              document.getElementById('<%=hdnDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
          });
    </script>

       <script>
          function Setdate(date) {

              var prm = Sys.WebForms.PageRequestManager.getInstance();

              //prm.add_endRequest(function () {

              $(document).ready(function () {
                  //debugger;
                  //alert("in");
                  var startDate = moment(date.split('-')[0], "MMM DD, YYYY");
                  var endtDate = moment(date.split('-')[1], "MMM DD, YYYY");
                  //$('#date').html(date);
                  $('#date').html(startDate.format("MMM DD, YYYY") + ' - ' + endtDate.format("MMM DD, YYYY"));
                  document.getElementById('<%=hdnDate.ClientID%>').value = date;
                  //$('#picker').daterangepicker({ startDate: startDate, endDate: endtDate});
                  $('#picker').daterangepicker({
                      startDate: startDate.format("MM/DD/YYYY"),
                      endDate: endtDate.format("MM/DD/YYYY"),
                      ranges: {
                      },
                  },
          function (start, end) {
              alert(start);
              //debugger
              $('#date').html(start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
              document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
             });

              });
              //});
     };
    </script>

  
        
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                 <h3 class="box-title">
                    <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Academic Year</label>
                                </div>
                                <asp:DropDownList ID="ddlAcademicYear" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfcddlAcademicYear" runat="server" ControlToValidate="ddlAcademicYear"
                                                    Display="None" ErrorMessage="Please Select Academic Year" SetFocusOnError="True"
                                                    ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Title of Activity</label>
                                </div>
                                <asp:TextBox ID="txtTitleofActivity" runat="server" MaxLength="256" CssClass="form-control"></asp:TextBox>
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"  TargetControlID="txtTitleofActivity"  FilterType="Custom,LowercaseLetters,UppercaseLetters" FilterMode="ValidChars"  ValidChars="(),- " >   
                                </ajaxToolKit:FilteredTextBoxExtender> 
                                <asp:RequiredFieldValidator ID="rfctxtTitleofActivity" runat="server" ControlToValidate="txtTitleofActivity"
                                    Display="None" ErrorMessage="Please Enter Title of Activity" SetFocusOnError="True"
                                     ValidationGroup="Academic"></asp:RequiredFieldValidator>
                               </div>
                             
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Type of Activity</label>
                                </div>
                                <asp:DropDownList ID="ddlTypeofActivity" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfcddlTypeofActivity" runat="server" ControlToValidate="ddlTypeofActivity"
                                 Display="None" ErrorMessage="Please Select Type of Activity" SetFocusOnError="True"
                                ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                             
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                 <sup>* </sup>
                                <label>Organize By</label>
                                </div>
                                <asp:TextBox ID="txtOrganizeBy" runat="server" MaxLength="1000" CssClass="form-control"></asp:TextBox>
                                 <ajaxToolKit:FilteredTextBoxExtender    ID="FilteredTextBoxExtender2" runat="server"  Enabled="True" TargetControlID="txtOrganizeBy" FilterType="Custom,LowercaseLetters,UppercaseLetters"  FilterMode="ValidChars" ValidChars="(),- " >   
                                </ajaxToolKit:FilteredTextBoxExtender>  
                                 <asp:RequiredFieldValidator ID="rfctxtOrganizeBy" runat="server" ControlToValidate="txtOrganizeBy"
                                          Display="None" ErrorMessage="Please Enter Organized By" SetFocusOnError="True"
                                          ValidationGroup="Academic"></asp:RequiredFieldValidator>
                            </div>
                            
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Conduct By</label>
                                </div>
                                <asp:TextBox ID="txtConductBy" runat="server" MaxLength="1000" CssClass="form-control"></asp:TextBox>
                                 <ajaxToolKit:FilteredTextBoxExtender    
                                   ID="FilteredTextBoxExtender3"  
                                      runat="server"    
   Enabled="True"    
   TargetControlID="txtConductBy"    
   FilterType="Custom,LowercaseLetters,UppercaseLetters"    
   FilterMode="ValidChars"    
   ValidChars="(),- " >   
                                </ajaxToolKit:FilteredTextBoxExtender>  
                                  <asp:RequiredFieldValidator ID="rfctxtConductBy" runat="server" ControlToValidate="txtConductBy"
                                    Display="None" ErrorMessage="Please Enter Conduct By" SetFocusOnError="True"
                                   ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                </div>
                           
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Event Level</label>
                                </div>
                                <asp:DropDownList ID="ddlEventLevel" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                              
                                    <asp:RequiredFieldValidator ID="rfcddlEventLevel" runat="server" ControlToValidate="ddlEventLevel"
                                                    Display="None" ErrorMessage="Please Select Event Level" SetFocusOnError="True"
                                                    ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Start End Date</label>
                                                        
                                                    </div>
                                 <asp:HiddenField ID="hdnDate" runat="server" />
                               <div id="picker" class="form-control">
                                                        <i class="fa fa-calendar"></i>&nbsp;
                                                    <span id="date"></span>
                                                        <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                    </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session Start Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="dvcal1" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtStartDate" runat="server" ValidationGroup="Academic" onpaste="return false;"
                                                TabIndex="3" ToolTip="Please Enter Session Start Date" CssClass="form-control" Style="z-index: 0;" />
                                            <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtStartDate" PopupButtonID="dvcal1" />
                                            <%-- <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                                Display="None" ErrorMessage="Please Enter Session Start Date" SetFocusOnError="True"
                                                ValidationGroup="submit" />--%>
                                            <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtStartDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" ControlExtender="meeStartDate"
                                                ControlToValidate="txtStartDate" EmptyValueMessage="Please Enter Start Date"
                                                InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                TooltipMessage="Please Enter Start Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                ValidationGroup="Academic" SetFocusOnError="True" />
                                        </div>
                                    </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session End Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="dvcal2" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtEndDate" runat="server" ValidationGroup="Academic" TabIndex="4"
                                                ToolTip="Please Enter Session End Date" CssClass="form-control" Style="z-index: 0;" />
                                           <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtEndDate" PopupButtonID="dvcal2" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeEndDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtEndDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevEndDate" runat="server" ControlExtender="meeEndDate"
                                                ControlToValidate="txtEndDate" EmptyValueMessage="Please Enter Session End Date"
                                                InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                TooltipMessage="Please Enter Session End Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Academic" SetFocusOnError="True" />
                                        </div>
                                    </div>
  
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Venue</label>
                                </div>
                                <asp:TextBox ID="txtVenue" runat="server" MaxLength="1000" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfctxtVenue" runat="server" ControlToValidate="txtVenue"
                                          Display="None" ErrorMessage="Please Enter Venue" SetFocusOnError="True"
                                          ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                  <ajaxToolKit:FilteredTextBoxExtender    
   ID="FilteredTextBoxExtender4"  
   runat="server"    
   Enabled="True"    
   TargetControlID="txtVenue"    
   FilterType="Custom,LowercaseLetters,UppercaseLetters"    
   FilterMode="ValidChars"    
   ValidChars="(),- " >   
                                </ajaxToolKit:FilteredTextBoxExtender> 
                            </div>
                            
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Event Mode</label>
                                </div>
                                <asp:DropDownList ID="ddlMode" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                     <asp:ListItem Value="1">OnLine</asp:ListItem>
                                     <asp:ListItem Value="2">OffLine</asp:ListItem>
                                </asp:DropDownList>
                               <asp:RequiredFieldValidator ID="rfcddlMode" runat="server" ControlToValidate="ddlMode"
                                                    Display="None" ErrorMessage="Please Select Event Mode" SetFocusOnError="True"
                                                    ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Duration</label>
                                </div>
                                 <asp:DropDownList ID="ddlDuration" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                               
                              
                                 <asp:RequiredFieldValidator ID="rfcddlDuration" runat="server" ControlToValidate="ddlDuration"
                                                    Display="None" ErrorMessage="Please Select Duration" SetFocusOnError="True"
                                                    ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>No. of Student's Participants</label>
                                </div>
                                <asp:TextBox ID="txtStudentParticipants" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                 <ajaxToolKit:filteredtextboxextender ID="Filteredtextboxextender6" runat="server" Enabled="True" TargetControlID="txtStudentParticipants" FilterMode="ValidChars" ValidChars="0123456789">    
                                </ajaxToolKit:filteredtextboxextender>  
                            </div>
                            
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>No. of Teacher's/Staff Participants</label>
                                </div>
                                <asp:TextBox ID="txtTeacherStaffParticipants" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                 <ajaxToolKit:filteredtextboxextender  ID="Filteredtextboxextender5" runat="server" Enabled="True" TargetControlID="txtTeacherStaffParticipants" FilterMode="ValidChars" ValidChars="0123456789">    
                                  </ajaxToolKit:filteredtextboxextender>  
                            </div>
                             
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Funded By</label>
                                </div>
                                <asp:TextBox ID="txtFundedBy" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Sanctioned Amount in Rs</label>
                                </div>
                                <asp:TextBox ID="txtSanctionedAmount" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                 <ajaxToolKit:filteredtextboxextender ID="Filteredtextboxextender7"  runat="server" Enabled="True" TargetControlID="txtSanctionedAmount" FilterMode="ValidChars" ValidChars="0123456789">    
                                 </ajaxToolKit:filteredtextboxextender>  
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading"><h5>Organizing Committee</h5></div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Convener</label>
                                </div>
                                <asp:ListBox ID="lstbxConvener" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>
                                  <asp:RequiredFieldValidator ID = "rfclstbxConvener"  ValidationGroup="Academic" 
                                     ControlToValidate ="lstbxConvener"   runat="server" 
                                     Display="None"  ErrorMessage = "Please select Convener"></asp:RequiredFieldValidator> 
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Co-Ordinator</label>
                                </div>
                                <asp:ListBox ID="lstbxCoordinator" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>
                                 <asp:RequiredFieldValidator ID = "rfclstbxCoordinator"  ValidationGroup="Academic" 
                                     ControlToValidate ="lstbxCoordinator"   runat="server" 
                                     Display="None"  ErrorMessage = "Please select Co-ordinator"></asp:RequiredFieldValidator> 
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Member's</label>
                                </div>
                                <asp:ListBox ID="lstbxMemberOrg" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>
                                 <asp:RequiredFieldValidator ID = "rfclstbxMemberOrg"  ValidationGroup="Academic" 
                                     ControlToValidate ="lstbxMemberOrg"   runat="server" 
                                     Display="None"  ErrorMessage = "Please select Member's"></asp:RequiredFieldValidator> 
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="btnSubmitOrganisedActivity" runat="server" CssClass="btn btn-primary" ValidationGroup="Academic" OnClick="btnSubmitOrganisedActivity_Click">Submit</asp:LinkButton>
                        <asp:LinkButton ID="btnCancelOrganisedActivity" runat="server" CssClass="btn btn-warning" OnClick="btnCancelOrganisedActivity_Click">Cancel</asp:LinkButton>
                          <asp:LinkButton ID="btnReport" runat="server" CssClass="btn btn-info" OnClick="btnReport_Click" >Excel Report</asp:LinkButton>
                    </div>
                      <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                         ShowSummary="False" ValidationGroup="Academic" />

                    <div class="col-12 mt-3">

                        <div class="sub-heading">
                                     <h5>Activity Organised List</h5>
                                 </div>
                        <asp:Panel ID="pnlOrg" runat="server" Visible="false">
                                    <asp:ListView ID="lvOrg" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="lvOrg_ItemEditing" >
                                        <LayoutTemplate>
                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblEvent">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th>Edit</th>
                                    <th>Academic Year</th>
                                    <th>Title of Activity</th>
                                    <th>Start Date</th>
                                     <th>End Date</th>
                                     <th>Venue</th>
                                

                                    
                                    
                                </tr>
                             </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                    <td>
                                        <asp:LinkButton ID="btnEditACtivityOrganised" runat="server" CssClass="fas fa-edit" OnClick="btnEditACtivityOrganised_Click" CommandArgument='<%#Eval("ACTIVITY_ORGANISED_ID") %>' CommandName="Edit"></asp:LinkButton>
                                    </td>
                                   <td><%# Eval("ACADMIC_YEAR_NAME") %></td>
                                   <td><%# Eval("TITLE_OF_ACITIVITY") %></td>
                                   <td><%# Convert.ToDateTime (Eval("STDATE")).ToString("d") %></td>
                                   <td><%# Convert.ToDateTime (Eval("ENDDATE")).ToString("d") %></td>
                                   <td> <%# Eval("VENUE") %></td>
                                      </tr>
                                </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                    </div>

                </div>
            </div>
        </div>
    </div>

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

    <!-- Start End Date Script -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                ranges: {
                },
            },
        function (start, end) {
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
        });

            $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
        });
    </script>

</asp:Content>

