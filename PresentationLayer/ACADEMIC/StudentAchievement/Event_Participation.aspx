<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Event_Participation.aspx.cs" Inherits="ACADEMIC_StudentAchievement_Event_Participation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
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
                            var arr = [0,6];
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
            var arr = [0,6];
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
                                var arr = [0, 6];
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
                var arr = [0, 6];
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
    <asp:HiddenField ID="hfdAmount" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <style>
        input[type=checkbox], input[type=radio] {
            margin: 0px 3px 0;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12" id="div3" runat="server">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                    </h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12" id="div">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Academic Year</label>
                                </div>
                                <asp:DropDownList ID="ddlAcademicYear" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlAcademicYear" runat="server" ControlToValidate="ddlAcademicYear"
                                    Display="None" ErrorMessage="Please Select Academic Year" SetFocusOnError="True"
                                    ValidationGroup="AcademicYearValidationId" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Event Category</label>
                                </div>
                                <asp:DropDownList ID="ddlEventCategory" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfcddlEventCategory" runat="server" ControlToValidate="ddlEventCategory"
                                    Display="None" ErrorMessage="Please Select Event Category" SetFocusOnError="True"
                                    ValidationGroup="AcademicYearValidationId" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Activity Category</label>
                                </div>
                                <asp:DropDownList ID="ddlActivityCategory" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfcddlActivityCategory" runat="server" ControlToValidate="ddlActivityCategory"
                                    Display="None" ErrorMessage="Please Select Activity Category" SetFocusOnError="True"
                                    ValidationGroup="AcademicYearValidationId" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Event Title</label>
                                </div>
                                <asp:DropDownList ID="ddlEventTitle" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfcddlEventTitle" runat="server" ControlToValidate="ddlEventTitle"
                                    Display="None" ErrorMessage="Please Select Event Title" SetFocusOnError="True"
                                    ValidationGroup="AcademicYearValidationId" InitialValue="0"></asp:RequiredFieldValidator>

                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Participation Type</label>
                                </div>
                                <asp:DropDownList ID="ddlParticipationType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfcddlParticipationType" runat="server" ControlToValidate="ddlParticipationType"
                                    Display="None" ErrorMessage="Please Select Participation Type" SetFocusOnError="True"
                                    ValidationGroup="AcademicYearValidationId" InitialValue="0"></asp:RequiredFieldValidator>

                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Whether you got Financial Assistance</label>
                                </div>
                                <div class="switch form-inline">
                                    <input type="checkbox" id="rdActive" name="switch" />
                                    <label data-on="Yes" data-off="No" for="rdActive"></label>
                                </div>

                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>If Yes, Amount in Rs</label>
                                </div>
                                <asp:TextBox ID="txtAmount" runat="server" MaxLength="10" Enabled="false" CssClass="form-control"></asp:TextBox>
                                <ajaxToolKit:FilteredTextBoxExtender ID="Filteredtextboxextender2" runat="server" Enabled="True" TargetControlID="txtAmount" FilterMode="ValidChars" ValidChars="0123456789">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Upload Certificate <small style="color: red;">(Choose only PDF file)</small></label>
                                </div>
                                <div id="Div1" class="logoContainer" runat="server">
                                    <img src="../../Images/default-fileupload.png" alt="upload image" runat="server" id="imgUpFile" />
                                </div>
                                <div class="fileContainer sprite pl-1">
                                    <span runat="server" id="ufFile"
                                        cssclass="form-control" tabindex="7">Upload File</span>
                                    <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Select file to upload"
                                        CssClass="form-control" onkeypress="" />
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="btnSubmitAcademicYear" runat="server" CssClass="btn btn-primary" OnClick="btnSubmitAcademicYear_Click" OnClientClick="return valiParticipation();" ValidationGroup="AcademicYearValidationId">Submit</asp:LinkButton>
                        <asp:LinkButton ID="btnCancelAcademicYear" runat="server" CssClass="btn btn-warning" OnClick="btnCancelAcademicYear_Click">Cancel</asp:LinkButton>
                    </div>
                    <asp:ValidationSummary ID="AcademicYearValidation" runat="server" DisplayMode="List" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="AcademicYearValidationId" />
                    <div class="col-12 mt-3">
                         <div class="sub-heading">
                                     <h5>Event Participation List</h5>
                                 </div>
                        <asp:Panel ID="pnlPraticipation" runat="server" Visible="false">
                            <asp:ListView ID="lvPraticipation" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="lvPraticipation_ItemEditing">
                                <LayoutTemplate>
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblEvent">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Edit</th>
                                                <th>Academic Year</th>
                                                <th>Event Category</th>
                                                <th>Activity Categor</th>
                                                <th>Event Title</th>
                                                <th>Participation Type</th>
                                                <th>Download</th>
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
                                            <asp:LinkButton ID="btnEditEventPartion" runat="server" OnClick="btnEditEventPartion_Click" CssClass="fas fa-edit" CommandArgument='<%# Eval("EVENT_PARTICIPATION_ID") %>' CommandName="Edit"></asp:LinkButton>
                                        </td>
                                        <td><%# Eval("ACADMIC_YEAR_NAME ") %></td>
                                        <td><%# Eval("EVENT_CATEGORY_NAME") %></td>
                                        <td><%# Eval("ACTIVITY_CATEGORY_NAME") %></td>
                                        <td><%# Eval("EVENT_TITLE") %></td>
                                        <td><%# Eval("PARTICIPATION_TYPE") %></td>
                                        <td>
                                            <asp:Button ID="btnDownload" runat="server" Text="DOWNLOAD" OnClick="btnDownload_Click" CssClass="btn btn-primary" CommandArgument='<%# Eval("FILE_NAME") %>' ToolTip='<%# Eval("FILE_NAME") %>' />
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


    <script>
        $(document).ready(function () {
            $(document).on("click", ".logoContainer", function () {
                $("#ctl00_ContentPlaceHolder1_FileUpload1").click();
            });
            $(document).on("keydown", ".logoContainer", function () {
                if (event.keyCode === 13) {
                    // Cancel the default action, if needed
                    event.preventDefault();
                    // Trigger the button element with a click
                    $("#ctl00_ContentPlaceHolder1_FileUpload1").click();
                }
            });
        });
    </script>

    <script type="text/javascript">
        function Focus() {
            //  alert("hii");
            document.getElementById("<%=imgUpFile.ClientID%>").focus();
        }
    </script>

    <script>
        $("input:file").change(function () {
            //$('.fuCollegeLogo').on('change', function () {

            var maxFileSize = 1000000;
            var fi = document.getElementById('ctl00_ContentPlaceHolder1_FileUpload1');
            myfile = $(this).val();
            var ext = myfile.split('.').pop();
            var res = ext.toUpperCase();
            if (res != "PDF") {
                alert("Please Select pdf File Only.");
                $('.logoContainer img').attr('src', "../../Images/default-fileupload.png");
                $(this).val('');
            }

            for (var i = 0; i <= fi.files.length - 1; i++) {
                var fsize = fi.files.item(i).size;
                if (fsize >= maxFileSize) {
                    alert("File Size should be less than 1 MB");
                    $('.logoContainer img').attr('src', "../../Images/default-fileupload.png");
                    $("#ctl00_ContentPlaceHolder1_FileUpload1").val("");

                }
            }

        });
    </script>

    <script>
        $("input:file").change(function () {
            var fileName = $(this).val();

            newText = fileName.replace(/fakepath/g, '');
            var newtext1 = newText.replace(/C:/, '');
            //newtext2 = newtext1.replace('//', ''); 
            var result = newtext1.substring(2, newtext1.length);


            if (result.length > 0) {
                $(this).parent().children('span').html(result);
            }
            else {
                $(this).parent().children('span').html("Choose file");
            }
        });
        //file input preview
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('.logoContainer img').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        $("input:file").change(function () {
            readURL(this);
        });
    </script>

    <script>
        function Setvalidation(val) {


            $('#rdActive').prop('checked', val);
        }

        function valiParticipation() {

            $('#hfdActive').val($('#rdActive').prop('checked'));
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitAcademicYear').click(function () {
                    validate();
                });
            });
        });
    </script>

    <script>
        $('#rdActive').click(function () {


            var txt = document.getElementById("<%=txtAmount.ClientID%>");


            if (txt.disabled) {

                document.getElementById("<%=txtAmount.ClientID%>").disabled = false;
             }
             else {

                 document.getElementById("<%=txtAmount.ClientID%>").disabled = true;
             }

        })

    </script>


</asp:Content>

