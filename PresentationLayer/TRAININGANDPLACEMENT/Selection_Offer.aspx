<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Selection_Offer.aspx.cs" Inherits="EXAMINATION_Projects_Selection_Offer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src='<%=Page.ResolveUrl("~/plugins/TinyMce/jquery.tinymce.min.js") %>'></script>

    <style>
        .badge {
            display: inline-block;
            padding: 5px 10px 7px;
            border-radius: 15px;
            font-size: 100%;
            width: 90px;
        }

        .badge-warning {
            color: #fff !important;
        }
    </style>

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {

            var table = $('#Table_Selection').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false,

                dom: 'lBfrtip',

                //Export functionality
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#Table_Selection').DataTable().column(idx).visible();
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
                                                return $('#Table_Selection').DataTable().column(idx).visible();
                                            }
                                        }
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
                                                return $('#Table_Selection').DataTable().column(idx).visible();
                                            }
                                        }
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
                                                return $('#Table_Selection').DataTable().column(idx).visible();
                                            }
                                        }
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

                var table = $('#Table_Selection').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false,

                    dom: 'lBfrtip',

                    //Export functionality
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#Table_Selection').DataTable().column(idx).visible();
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
                                                    return $('#Table_Selection').DataTable().column(idx).visible();
                                                }
                                            }
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
                                                    return $('#Table_Selection').DataTable().column(idx).visible();
                                                }
                                            }
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
                                                    return $('#Table_Selection').DataTable().column(idx).visible();
                                                }
                                            }
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
        $(document).ready(function () {

            var table = $('#Table_Offer_Letter').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false,

                dom: 'lBfrtip',

                //Export functionality
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#Table_Offer_Letter').DataTable().column(idx).visible();
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
                                                return $('#Table_Offer_Letter').DataTable().column(idx).visible();
                                            }
                                        }
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
                                                return $('#Table_Offer_Letter').DataTable().column(idx).visible();
                                            }
                                        }
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
                                                return $('#Table_Offer_Letter').DataTable().column(idx).visible();
                                            }
                                        }
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

                var table = $('#Table_Offer_Letter').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false,

                    dom: 'lBfrtip',

                    //Export functionality
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#Table_Offer_Letter').DataTable().column(idx).visible();
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
                                                    return $('#Table_Offer_Letter').DataTable().column(idx).visible();
                                                }
                                            }
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
                                                    return $('#Table_Offer_Letter').DataTable().column(idx).visible();
                                                }
                                            }
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
                                                    return $('#Table_Offer_Letter').DataTable().column(idx).visible();
                                                }
                                            }
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
     <asp:HiddenField ID="hfdTemplate" runat="server" />
    <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><span>Selection Offer Letter</span></h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1">Selection </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2">Offer Letter </a>
                            </li>
                        </ul>

                        <div class="tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <asp:UpdatePanel ID="upnlroundselection" runat="server">
                                    <ContentTemplate>
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Job Announcement</label>
                                            </div>
                                            <asp:DropDownList ID="ddlJobAnnouncement" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlJobAnnouncement_SelectedIndexChanged" AutoPostBack="true" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvstudent" runat="server" ControlToValidate="ddlJobAnnouncement"
                                                                Display="None" ErrorMessage="Please Select Job Announcement." ValidationGroup="show"
                                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Round</label>
                                            </div>
                                            <asp:DropDownList ID="ddlRound" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="2" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlRound"
                                                                Display="None" ErrorMessage="Please Select Round." ValidationGroup="show"
                                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Date</label>
                                            </div>
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" type="date" TabIndex="3"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>

                                                <label>Status</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="4">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Selected</asp:ListItem>
                                                <asp:ListItem Value="2">Rejected</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlStatus"
                                                                Display="None" ErrorMessage="Please Select Status." ValidationGroup="show"
                                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:LinkButton ID="btnRoundSelection" runat="server" CssClass="btn btn-outline-info" OnClick="btnRoundSelection_Click"  TabIndex="5" ValidationGroup="show" >Submit</asp:LinkButton>
                                    <asp:LinkButton ID="btnCanceltab" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCanceltab_Click">Cancel</asp:LinkButton>
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="show"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                                 </ContentTemplate>
                                    <Triggers>
                                        <%--<asp:PostBackTrigger ControlID="ddlJobAnnouncement" />--%>
                                       <%-- <asp:AsyncPostBackTrigger ControlID="btnCanceltab" />--%>
                                    </Triggers>
                                </asp:UpdatePanel>
                                <div class="col-12">
                                    <%--<table class="table table-striped table-bordered nowrap" style="width: 100%" id="Table_Selection">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th></th>
                                                <th>Student ID</th>
                                                <th>Student Name</th>
                                                <th>Program</th>
                                                <th>Semester</th>
                                                <th>Status</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkapp" runat="server" /></td>
                                                <td>ID000004</td>
                                                <td>Ajanta Mendis</td>
                                                <td>Program 1</td>
                                                <td>Sem2</td>
                                                <td><span class="badge badge-success">Selected</span></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" /></td>
                                                <td>ID000004</td>
                                                <td>Ajanta Mendis</td>
                                                <td>Program 1</td>
                                                <td>Sem2</td>
                                                <td><span class="badge badge-success">Selected</span></td>
                                            </tr>
                                        </tbody>
                                    </table>--%>
                                    <asp:UpdatePanel ID="upnlintselect" runat="server">
                                        <ContentTemplate>
                                    <asp:HiddenField ID="hfcount" runat="server"/>
                                    <div class="col-12 mt-3">
                                        <asp:ListView ID="lvSelectionOffer" runat="server">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="Table_Selection">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th><asp:CheckBox ID="cbSAll" runat="server" onclick="javascript:SelectAll(this)" /></th>
                                                            <th>Student ID</th>
                                                            <th>Student Name</th>
                                                            <th>Program</th>
                                                            <th>Semester</th>
                                                            <th>Round Status</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                   <td><asp:CheckBox ID="chkRow" runat="server" ToolTip='<%# Eval("IDNO") %>'/> </td>
                                                   <td> <asp:Label ID="lblIdno"   CssClass="badge"  runat="server" Text='<%# Eval("IDNO")%>'></asp:Label></td>
                                                   <td><%# Eval("STUDNAME") %></td>
                                                   <td><%# Eval("PROGRAME") %></td>
                                                   <td><%# Eval("SEMESTERNAME") %></td>
                                                   <td>
                                                       <asp:label ID="lblStatus" runat="server" CssClass="badge" Text='<%# Eval("STATUS") %>' ></asp:label>      <%--  </span>--%></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                        
                                    </div>
                                            </ContentTemplate>
                                        <%--<Triggers>
                                            <asp:PostBackTrigger ControlID="lvSelectionOffer" />
                                        </Triggers>--%>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <div class="tab-pane fade" id="tab_2">
                                <asp:UpdatePanel ID="unlintselect" runat="server">
                                    <ContentTemplate>
                                   
                                <div class="col-12 mt-3">
                                   <%-- <asp:UpdatePanel ID="upnlsendofferletter" runat="server">
                                        <ContentTemplate>--%>

                                        
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Job Announcement</label>
                                            </div>
                                            <asp:DropDownList ID="ddlJobAnnouncementOffer" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlJobAnnouncementOffer_SelectedIndexChanged" AutoPostBack="true" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlJobAnnouncementOffer"
                                                                Display="None" ErrorMessage="Please Select Job Announcement." ValidationGroup="showoffer"
                                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                 <sup>* </sup>
                                                <label>Date</label>
                                            </div>
                                            <asp:TextBox ID="txtDateOffer" runat="server" CssClass="form-control" type="date" TabIndex="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                                ControlToValidate="txtDateOffer" Display="None"
                                                                ErrorMessage="Please Enter Date." SetFocusOnError="True"
                                                                ValidationGroup="showoffer" />
                                        </div>
                                    </div>                                             
                                </div>

                                        <%-- data-toggle="modal" data-target="#Send_Offer_Letter"--%>
                                <div class="col-12 btn-footer">
                                    <asp:LinkButton ID="btnSendOfferLetter" runat="server" CssClass="btn btn-outline-info" OnClientClick=" return validateOfferLetter();" TabIndex="3" >Send Offer Letter</asp:LinkButton>
                                    <%-- <asp:Button ID="btn13" runat="server" Text="btntest" CssClass="btn btn-outline-info" OnClientClick=" return validateOfferLetter();" TabIndex="3" />--%>
                                    <asp:LinkButton ID="btnCancelOffer" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelOffer_Click" TabIndex="4">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="showoffer"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                               
                                </ContentTemplate>                                       
                                    </asp:UpdatePanel>

                                <asp:UpdatePanel ID="upnloffer" runat="server" >
                                    <ContentTemplate>
                                    <asp:HiddenField ID="hfoffer" runat="server"/>
                                    <div class="col-12 mt-3">
                                        <asp:ListView ID="lvofferLetter" runat="server">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="Table_Selection">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th><asp:CheckBox ID="cbSAllOffer" runat="server" onclick="javascript:SelectAlloffer(this)" /></th>
                                                            <th>Student ID</th>
                                                            <th>Student Name</th>
                                                            <th>Program</th>
                                                            <th>Semester</th>                                                          
                                                            <th>Status</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                   <td><asp:CheckBox ID="chkRowoffer" runat="server" ToolTip='<%# Eval("IDNO") %>' /> <%--Text='<%# Eval("EMAILID") %>'/>--%></td>
                                                   <td><asp:Label ID="lblIdno1"   CssClass="badge"  runat="server" Text='<%# Eval("IDNO")%>'></asp:Label><%--<%# Eval("IDNO") %>--%></td>
                                                   <td><%# Eval("STUDNAME") %></td>
                                                   <td><%# Eval("PROGRAME") %></td>
                                                   <td><%# Eval("SEMESTERNAME") %></td>
                                                   <td><asp:label ID="lblStatus" runat="server" CssClass="badge" Text='<%# Eval("STATUS") %>' Style="text-align:center; width:auto" ></asp:label></td>
                                                </tr>
                                            </ItemTemplate>

                                        </asp:ListView>
                                    </div>
                                        </ContentTemplate>
                                   <%-- <Triggers>
                                        <asp:PostBackTrigger ControlID="lvofferLetter" />
                                    </Triggers>--%>
                                    </asp:UpdatePanel>

                                <!-- View Modal -->
                                <asp:UpdatePanel ID="upnlOfferletter" runat="server">
                                    <ContentTemplate>
                                <div class="modal" id="Send_Offer_Letter">
                                    <div class="modal-dialog">
                                        <div class="modal-content">

                                            <!-- Modal Header -->
                                            <div class="modal-header">
                                                <h4 class="modal-title">Offer Letter Details</h4>
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            </div>

                                            <!-- Modal body -->
                                            
                                            <div class="modal-body pl-0 pr-0">
                                                <div class="col-12 mb-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Offer Description</label>
                                                            </div>
                                                            <asp:TextBox ID="templateEditor" runat="server" Visible="true" TextMode="MultiLine" ClientIDMode="Static" CssClass="form-control TextBox1" MaxLength="300"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                                            <p style="color:red;">NOTE: Please Upload Only PDF File.</p>
                                                            <div class="label-dynamic">
                                                                
                                                                <label>Attach Documents</label>
                                                            </div>
                                                            <%--<input type="file" id="myfile" name="myfile">--%>
                                                            <asp:FileUpload ID="fuoffer" runat="server" />
                                                             <asp:RequiredFieldValidator ID="requpload" runat="server" ControlToValidate="fuoffer" ValidationGroup="abc" CssClass="color-red" ErrorMessage="Please select the file"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <%--<asp:LinkButton ID="btnSubmitOfferLetter" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmitOfferLetter_Click" OnClientClick="return validateOfferLetter();" ValidationGroup="abc">Send Offer</asp:LinkButton>--%>
                                                    <asp:Button ID="btnSubmitOfferLetter" runat="server" CssClass="btn btn-outline-info" Text="Send Offer" OnClick="btnSubmitOfferLetter_Click" />  <%--OnClientClick="return validateOfferLetter();"--%>
                                                   <%-- <asp:LinkButton ID="btnCancelOfferLetter" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelOfferLetter_Click" >Cancel</asp:LinkButton>--%>
                                                </div>
                                            </div>
                                            </ContentTemplate>
                                                 <Triggers>
                                                     <asp:PostBackTrigger ControlID="btnSubmitOfferLetter" />
                                                   <%--  <asp:AsyncPostBackTrigger ControlID="btnSubmitOfferLetter" />--%>
                                                 </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
    </asp:UpdatePanel>
    <!-- TinyMce Script -->
    <script>
        $(document).ready(function () {
            LoadTinyMCE();
        });
    </script>
  <%--  <script>

        function LoadTinyMCE() {
            $('.TextBox1').tinymce({
                script_url: '../plugins/TinyMce/tinymce.min.js',
                placeholder: 'Enter the course content here ...',
                height: 300,
                menubar: 'file edit view insert format tools table tc help',
                plugins: [
                  'advlist autolink lists link image charmap print preview anchor',
                  'searchreplace visualblocks code fullscreen',
                  'insertdatetime media table paste code help wordcount'
                ],
                toolbar: 'undo redo | formatselect | ' +
                'bold italic backcolor | alignleft aligncenter ' +
                'alignright alignjustify | bullist numlist outdent indent | ' +
                'removeformat | help',
                content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:14px }',
                //encoding: 'xml'
                //init_instance_callback: function (editor) {
                //    editor.on('mouseup', function (e) {
                //        alert('okoko');
                //    });
                //}
            });
        }
    </script>--%>
    <script>
        function SelectAll(cbSAll) {
            var i = 0;
            var hftot = document.getElementById('<%= hfcount.ClientID %>').value;
            var count = 0;
            for (i = 0; i < Number(hftot) ; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvSelectionOffer_ctrl' + i + '_chkRow');
                if (lst.type == 'checkbox') {
                    if (cbSAll.checked == true) {
                        if (lst.disabled == false) {
                            lst.checked = true;
                            count = count + 1;
                        }
                    }
                    else {
                        lst.checked = false;
                    }
                }
            }
        }
        function SelectAlloffer(cbSAllOffer) {
            //alert("A");
            var i = 0;
            var hftotoff = document.getElementById('<%= hfoffer.ClientID %>').value;
            var count = 0;
            for (i = 0; i < Number(hftotoff) ; i++) {

                var lstoff = document.getElementById('ctl00_ContentPlaceHolder1_lvofferLetter_ctrl' + i + '_chkRowoffer');
                if (lstoff.type == 'checkbox') {
                    if (cbSAllOffer.checked == true) {
                        if (lstoff.disabled == false) {
                            lstoff.checked = true;
                            count = count + 1;
                        }
                    }
                    else {
                        lstoff.checked = false;
                    }
                }
            }
        }
        </script>
    <%-- <script>
         $('#<%=btnSubmitOfferLetter.ClientID%>').click(function () {
             // alert('a');
             $('#<%=hfdTemplate.ClientID%>').val($('#templateEditor').val().replace('MyLRMar', '').replace('badge', ''));
        });

        $.ajax({
            type: "POST",
            url: "CreateTemplate.aspx/CategoryType",
            data: '{val:"' + val + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (d) {
                debugger
                var data = JSON.parse(d.d);
                var iHtml = "<center>";
                $.each(data, function (a, b) {
                    debugger;
                    iHtml += '<b class="MyLRMar badge" draggable="true" style="font-size:14px;background-color:#abd2e8;padding:7px; color:black;margin-bottom:5px;">[' + b.NAME + ']</b>';
                });


            },
            failure: function (response) {
                alert("Err1");
            },
            error: function (response) {
                alert(response.responseText);
            }
        });




        $('#<%=templateEditor.ClientID%>').change(function () {

            var val = $(this).val().split('^')[1].toString();
            $("#spnSP_NAME").html(val);

            $.ajax({
                type: "POST",
                url: "CreateTemplate.aspx/CategoryType",
                data: '{val:"' + val + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (d) {
                    debugger
                    var data = JSON.parse(d.d);
                    var iHtml = "<center>";
                    $.each(data, function (a, b) {
                        debugger;
                        iHtml += '<b class="MyLRMar badge" draggable="true" style="font-size:14px;background-color:#abd2e8;padding:7px; color:black">[' + b.NAME + ']</b>';
                    });
                },
                failure: function (response) {
                    alert("Err1");
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
        });

        $('#<%=templateEditor.ClientID%>').change(function () {
             debugger
             ShowLoader();
             tinyMCE.triggerSave();
             $.ajax({
                 type: "POST",
                 url: "CreateTemplate.aspx/DataList",
                 data: '{val:' + parseInt($(this).val()) + '}',
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (d) {
                     var data = JSON.parse(d.d);
                     var options = "";
                     options += "<option value='0'>Please Select</option>";
                     $.each(data, function (a, b) {
                         options += "<option value=" + b.ID + "^" + b.SP_NAME + ">" + b.NAME + "</option>";
                     });
                     $("#spnSP_NAME").html('');
                     HideLoader();
                 },
                 failure: function (response) {
                     HideLoader();
                     alert("Err1");
                 },
                 error: function (response) {
                     HideLoader();
                     alert(response.responseText);
                 }
             });
         });
    </script>--%>
    <script>

        function validateRoundDetails() {

            var ddljobtype = $("[id$=ddlJobAnnouncement]").attr("id");
            var ss = document.getElementById('<%=ddlJobAnnouncement.ClientID%>').value;

            if (ss == '0') {

                alert('Please Select Job Announcement.', 'Warning!');
                $(ddljobtype).focus();
                return false;
            }

            var round = $("[id$=ddlRound]").attr("id");
            var rr = document.getElementById('<%=ddlRound.ClientID%>').value;

            if (rr == '0') {

                alert('Please Select Round.', 'Warning!');
                $(round).focus();
                return false;
            }
        }

        function validateOfferLetter() {
            debugger;
            var ddloffer = $("[id$=ddlJobAnnouncementOffer]").attr("id");
            var ss = document.getElementById('<%=ddlJobAnnouncementOffer.ClientID%>').value;

            if (ss == '0') {

                alert('Please Select Job Announcement.', 'Warning!');
              //  $(ddloffer).focus();
                //alert(ddloffer);
                Close();
                return false;
            }
            else {
                $('#Send_Offer_Letter').modal('show');
            }
            var txtdate = $("[id$=txtDateOffer]").attr("id");
            var dd = document.getElementById('<%=txtDateOffer.ClientID%>').value;

            if (dd == "") {

                alert('Please Select Date.', 'Warning!');
                $(txtdate).focus();
               Close();
                return false;
            }
            else {
                $('#Send_Offer_Letter').modal('show');
            }

            return false;
        }
        function Close() {
            debugger
            //$("#Details_Veiw").Show();
           // alert('close');
            //alert("Close");
         
           
            $('#Send_Offer_Letter').modal('hide');
           

            //$(function () {
                //$("#btnSendOfferLetter").click(function (evt) {
                //    alert('B');
                //    evt.preventDefault();
                //    $('#Send_Offer_Letter').toggle('fast');
                //});
            // });
            //if (Page_IsValid) {
            //  //  alert('B');
            //    $("div[name$='Send_Offer_Letter']").removeClass("hidden");
            //  //  alert('C');
            //}
            return false;
        }
    </script>
</asp:Content>

