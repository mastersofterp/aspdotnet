<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Application_Status.aspx.cs" Inherits="EXAMINATION_Projects_Application_Status" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

        td .fa-eye {
            font-size: 18px;
            color: #0d70fd;
            margin-left: 10px;
        }
    </style>

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {

            var table = $('#My_Table').DataTable({
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
                                return $('#My_Table').DataTable().column(idx).visible();
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
                                                return $('#My_Table').DataTable().column(idx).visible();
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
                                                return $('#My_Table').DataTable().column(idx).visible();
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
                                                return $('#My_Table').DataTable().column(idx).visible();
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

                var table = $('#My_Table').DataTable({
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
                                    return $('#My_Table').DataTable().column(idx).visible();
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
                                                    return $('#My_Table').DataTable().column(idx).visible();
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
                                                    return $('#My_Table').DataTable().column(idx).visible();
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
                                                    return $('#My_Table').DataTable().column(idx).visible();
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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Application Status</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-6 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Job Announcement</label>
                                </div>
                                <asp:DropDownList ID="ddlJobAnnouncement" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlstatus"
                                                                Display="None" ErrorMessage="Please Select Job Announcement." ValidationGroup="appstatus"
                                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-6 col-md-6 col-12 text-right">
                                <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-outline-info" data-toggle="modal" data-target="#Send_Email"><i class="fa fa-envelope-o" aria-hidden="true"></i> Email</asp:LinkButton>
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Status</label>
                                </div>
                                <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">Confirmed</asp:ListItem>
                                    <asp:ListItem Value="2">Reject</asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvstudent" runat="server" ControlToValidate="ddlstatus"
                                                                Display="None" ErrorMessage="Please Select Status." ValidationGroup="appstatus"
                                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label></label>
                                </div>
                                <asp:LinkButton ID="btnApply" runat="server" CssClass="btn btn-outline-info" OnClick="btnApply_Click" OnClientClick="return validateApplicationStatus();">Show Details</asp:LinkButton>
                            </div>
                        </div>
                    </div>

                    <%--                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-outline-info">Show</asp:LinkButton>
                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger">Cancel</asp:LinkButton>
                    </div>--%>

                    <div class="col-12 mt-3">
                        <%-- <table class="table table-striped table-bordered nowrap" style="width: 100%" id="My_Table">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th></th>
                                    <th>Student ID</th>
                                    <th>Student Name</th>
                                    <th>Program</th>
                                    <th>Semester</th>
                                    <th>Resume</th>
                                    <th>Status</th>
                                    <th>Remark</th>
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
                                    <td>
                                        <i class="fa fa-download" aria-hidden="true" style="color: #28a745; font-size: 24px;"></i>
                                        <i class="fa fa-eye" aria-hidden="true" data-toggle="modal" data-target="#Details_Veiw"></i>
                                    </td>
                                    <td><span class="badge badge-success">Confirmed</span></td>
                                    <td>
                                        <asp:TextBox ID="txtRemark" runat="server" Visible="true" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                     <asp:CheckBox ID="CheckBox1" runat="server" /></td>
                                    <td>ID000004</td>
                                    <td>Ajanta Mendis</td>
                                    <td>Program 1</td>
                                    <td>Sem2</td>
                                    <td>
                                        <i class="fa fa-download" aria-hidden="true" style="color: #28a745; font-size: 24px;"></i>
                                        <i class="fa fa-eye" aria-hidden="true" data-toggle="modal" data-target="#Details_Veiw"></i>
                                    </td>
                                    <td><span class="badge badge-danger">Rejected</span></td>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server" Visible="true" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></td>
                                    
                                </tr>
                            </tbody>
                        </table>--%>
                         <asp:HiddenField ID="hfcount" runat="server"/>
                        <asp:UpdatePanel ID="upnlapplicationstatus" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                        <asp:ListView ID="lvApplicationStatus" runat="server">
                            <LayoutTemplate>
                                <table class="table table-striped table-bordered display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th><asp:CheckBox ID="cbSAll" runat="server" onclick="javascript:SelectAll(this)" /></th>
                                            <th>Student ID</th>
                                            <th>Student Name</th>
                                            <th>Program</th>
                                            <th>Semester</th>
                                            <th>Resume</th>
                                            <th>Status</th>
                                            <th>Remark</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><asp:CheckBox ID="chkRow" runat="server" ToolTip='<%# Eval("EMAILID") %>'/> 
                                        <asp:HiddenField ID="hdIdno" runat="server" Value='<%# Eval("IDNO") %>'/>
                                    </td>
                                    <td><%# Eval("IDNO") %></td> 
                                    <td><%# Eval("STUDNAME") %></td>
                                    <td><%# Eval("PROGRAME") %></td>
                                    <td><%# Eval("SEMESTERNAME") %></td>
                                    <td><%--<i class="fa fa-download" aria-hidden="true" style="color: #28a745; font-size: 24px;"></i>--%>
                                        <asp:UpdatePanel ID="upnlpreview" runat="server" >
                                            <ContentTemplate>
                                        <asp:LinkButton ID="lnkdownload" runat="server" CssClass="fa fa-download" ToolTip='<%# Eval("RESUME") %>' OnClick="lnkdownload_Click"></asp:LinkButton>
                                        <%--<i class="fa fa-eye" aria-hidden="true" data-toggle="modal" data-target="#Details_Veiw">--%>
                                            <asp:LinkButton ID="lnkdetails" runat="server" CssClass="fa fa-eye"  ToolTip='<%# Eval("RESUME") %>' OnClick="lnkdetails_Click" ></asp:LinkButton></td>
                                                </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lnkdownload" />
                                                <asp:PostBackTrigger ControlID="lnkdetails" />
                                            </Triggers>
                                    </asp:UpdatePanel>

                                        
                                    <td><span class="badge badge-success"><%# Eval("STATUS") %></span></td>
                                    <td>
                                        <asp:TextBox ID="txtRemark" runat="server" Visible="true" TextMode="MultiLine" CssClass="form-control" onkeypress="return alphaOnly(event);"></asp:TextBox>
                                    <%--<ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                                TargetControlID="txtRemark" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                ControlToValidate="txtRemark" Display="None"
                                                                ErrorMessage="Please Enter Remark." SetFocusOnError="True"
                                                                ValidationGroup="appstatus" />--%>
                                        </td>
                                </tr>
                            </ItemTemplate>
                            
                        </asp:ListView>
                            </ContentTemplate>
                            <Triggers>
                               <%-- <asp:PostBackTrigger ControlID="lvApplicationStatus" />
                                <asp:PostBackTrigger ControlID="btnCanceltab" />--%>
                            </Triggers>
                        </asp:UpdatePanel>    
                    </div>
                    <%--<asp:UpdatePanel ID="upnlfooter" runat="server">
                        <ContentTemplate>--%>

                        
                    <div class="col-12 btn-footer mt-3">
                        <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click" ValidationGroup="appstatus">Submit</asp:LinkButton>
                         <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="appstatus" />
                        <asp:LinkButton ID="btnCanceltab" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCanceltab_Click">Cancel</asp:LinkButton>
                    </div>
                           <%-- </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnCanceltab" />
                            <asp:PostBackTrigger ControlID="btnSubmit" />
                        </Triggers>
                        </asp:UpdatePanel>--%>
                     

                </div>
            </div>
        </div>
    </div>

    <!-- The Modal -->
    <div class="modal fade" id="Send_Email">
        <div class="modal-dialog">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Email</h4>
                    <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                    <asp:ImageButton ID="clsepop" runat="server" OnClick="clsepop_Click" ImageUrl="~/Images/cancel.gif"  />  
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-12 col-md-12 col-12" runat="server" id="divemail" visible="false">
                                <asp:RadioButton ID="rdoConfirmed" runat="server" Text="Confirmed" GroupName="status"/>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdoRejected" runat="server" Text="Rejected" GroupName="status" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdoBoth" runat="server" Text="Both" GroupName="status" />
                            </div>
                            <div class="form-group col-lg-12 col-md-12 col-12">
                                <div class="label-dynamic">
                                    <label>Subject</label>
                                </div>
                                <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group col-lg-12 col-md-12 col-12">
                                <div class="label-dynamic">
                                    <label>Description</label>
                                </div>
                                <asp:TextBox ID="txtDescription" runat="server" Visible="true" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-lg-12 col-md-12 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label class="mb-0">Attach Document <small style="color: #ff0000;">(Upload Pdf Only - Max. size 5mb)</small></label>
                                </div>
                                <%--<input type="file" id="myfile" name="myfile">--%>
                                <asp:FileUpload ID="fuemailattach" runat="server" />
                                <%--<asp:RequiredFieldValidator ID="requpload" runat="server" ControlToValidate="fuemailattach" ValidationGroup="abcd" Display="None" CssClass="color-red" ErrorMessage="Please select the file"></asp:RequiredFieldValidator>
                           --%>
                                 <asp:RequiredFieldValidator ID="requpload" runat="server" ControlToValidate="fuemailattach" Display="None"
                                                                    ErrorMessage="Please select the file" InitialValue="" ValidationGroup="abcd">
                                                                </asp:RequiredFieldValidator>
                                 </div>
                            <div class="col-12 btn-footer mt-3">
                                <asp:LinkButton ID="BtnSendEmail" runat="server" CssClass="btn btn-outline-info" OnClick="BtnSendEmail_Click" ValidationGroup="abcd">Send</asp:LinkButton>
                                <asp:LinkButton ID="btnCancelEmail" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelEmail_Click">Cancel</asp:LinkButton>
                                 <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="abcd" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
         <div id="divMsg" runat="server">
      </div>
    <script>


        function Show() {
            //$("#Details_Veiw").Show();
          
            document.getElementById('ctl00_ContentPlaceHolder1_txtSubject').value = '';
            document.getElementById('ctl00_ContentPlaceHolder1_txtDescription').value = '';
            $("#Details_Veiw").modal('show');
         
        }

     function SelectAll(cbSAll) {
            var i = 0;
            var hftot = document.getElementById('<%= hfcount.ClientID %>').value;
            var count = 0;
            for (i = 0; i < Number(hftot) ; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvApplicationStatus_ctrl' + i + '_chkRow');
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


        function validateApplicationStatus() {

            var ddljobtype = $("[id$=ddlJobAnnouncement]").attr("id");
            var ss = document.getElementById('<%=ddlJobAnnouncement.ClientID%>').value;

            if (ss == '0') {

                alert('Please Select Job Announcement.', 'Warning!');
                $(ddljobtype).focus();
                return false;
            }

            var ddlstatus = $("[id$=ddlstatus]").attr("id");
            var s1 = document.getElementById('<%=ddlstatus.ClientID%>').value;

            if (s1 == '0') {

                alert('Please Select Status.', 'Warning!');
                $(ddlstatus).focus();
                return false;
            }

            
        }



        function validateStatusSubmit() {

            var ddljobtype = $("[id$=ddlJobAnnouncement]").attr("id");
            var ss = document.getElementById('<%=ddlJobAnnouncement.ClientID%>').value;
            //if (ss == '0') {

            //    alert('Please Select Job Announcement.', 'Warning!');
            //    $(ddljobtype).focus();
            //    return false;
            //}
            var ddlstatus = $("[id$=ddlstatus]").attr("id");
            var s1 = document.getElementById('<%=ddlstatus.ClientID%>').value;

            if ((ss == '0') &&(s1 == '0')) {

                alert('Please Select Job Announcement and Status.', 'Warning!');
                $(ddljobtype).focus();
                $(ddlstatus).focus();
                return false;
            }


        }

        function alphaOnly(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }


        //function seepdf(path) {
        //    debugger;
        //    //alert(path);

        //    // window.open(path);
        //    window.open(path, 'mywindow', 'fullscreen=yes, scrollbars=auto');
        //}
       
        </script>
</asp:Content>

