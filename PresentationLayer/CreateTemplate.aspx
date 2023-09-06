<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CreateTemplate.aspx.cs"
    Inherits="CreateTemplate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../plugins/Select2/select2.min.css" rel="stylesheet" />
    <style>
        .MyTopMar {
            margin-top:10px;
        }

        .MyLRMar {
            margin-left:5px;
            margin-right:5px;
        }

        .select2-container--default .select2-selection--multiple .select2-selection__choice {
            background-color:#3c8dbc;
        }
        .select2-container--default .select2-selection--multiple .select2-selection__choice__remove {
            color:white;
        }
        .select2-container--default .select2-results > .select2-results__options {
            width:100%;
        }

        /* Absolute Center Spinner */
        .loadingg {
            position: fixed;
            z-index: 9999;
            height: 2em;
            width: 2em;
            overflow: visible;
            margin: auto;
            top: 0;
            left: 0;
            bottom: 0;
            right: 0;
        }
        /* Transparent Overlay */
        .loadingg:before {
            content: '';
            display: block;
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0,0,0,0.3);
        }
    </style>

    <script src="../plugins/Select2/select2.min.js"></script>
    <script src="../plugins/TinyMce/jquery.tinymce.min.js"></script>


    <div style="z-index: 1; position: relative;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updTemplate"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="position:fixed;margin-left:40%;margin-top:18%">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px;"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updTemplate" runat="server">
        <ContentTemplate>

            <asp:HiddenField ID="hfdUserType" runat="server" />
            <asp:HiddenField ID="hfdPage" runat="server" />
            <asp:HiddenField ID="hfdTemplate" runat="server" />
            <asp:HiddenField ID="hfdCategoryType" runat="server" />
            <asp:HiddenField ID="hfdStatus" runat="server" />
            <asp:HiddenField ID="hfdDataList" runat="server" />
            <asp:HiddenField ID="hfdOperation" runat="server" Value="1" />
            <asp:HiddenField ID="hfdMailType" runat="server" />

            <div class="row">
                <%--<div class="loading">Loading&#8230;</div>--%>
           <%--     <div class="loadingg hidden">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px;"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>--%>
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">CREATE TEMPLATE</h3>
                        </div>

                        <div class="box-body">
                             <div class="col-md-4">
                                <label><span style="color: red;">* </span>Template Name :</label>
                                <asp:TextBox ID="txtTempleteName" MaxLength="100" runat="server" CssClass="form-control" placeholder="Enter Template Name here" ClientIDMode="Static"></asp:TextBox>
                            </div> 

                            <div class="col-md-4">
                                <label><span style="color: red;">* </span>Subject :</label>
                                <asp:TextBox ID="txtSubject" MaxLength="100" runat="server" CssClass="form-control" placeholder="Enter Subject here" ClientIDMode="Static"></asp:TextBox>
                            </div> 

                             <div class="col-md-4">
                                <label><span style="color: red;">* </span>Mail Type :</label>
                                <asp:DropDownList ID="ddlMailType" runat="server" CssClass="form-control" AppendDataBoundItems="True" ClientIDMode="Static">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">Manual</asp:ListItem>
                                    <asp:ListItem Value="2">Schedular</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-4 MyTopMar">
                                        <label><span style="color: red;">* </span>User Type :</label>
                                         <select class="select2UserType form-control ddlSelect2" id="ddlUserType" multiple="multiple" style="height: 36px;"></select>
                                    </div>  

                                    <div class="col-md-8 MyTopMar">
                                        <label><span style="color: red;">* </span>Page For Template :</label>
                                        <select class="select2Page form-control ddlSelect2" id="ddlPage" multiple="multiple" style="height: 36px;"></select>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4 MyTopMar">
                                <label><span style="color: red;">* </span>Category :</label>
                                <asp:DropDownList ID="ddlCategoty" runat="server" CssClass="form-control" AppendDataBoundItems="True" ClientIDMode="Static">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-6 MyTopMar">
                                <label><span style="color: red;">* </span>Data List : <span id="spnSP_NAME" class="badge"></span></label>
                                <asp:DropDownList ID="ddlDataType" runat="server" CssClass="form-control" AppendDataBoundItems="True" ClientIDMode="Static">
                                </asp:DropDownList>
                            </div>
                            
                            <div class="col-md-2 MyTopMar">
                                <label for="customFile">Status</label><br />
                                <label class="radio-inline"><input type="radio" id="rbtnActive" name="optradio" checked>Active</label>
                                <label class="radio-inline"><input type="radio" id="rbtnDeactive" name="optradio">Inactive</label>
                            </div>

                            <div class="col-md-12 MyTopMar">
                                <h4><code style="color:red">* Note : </code><code style="color:black">Drag and Drop below feilds on Text Editor where you want dynamic values.</code></h4> 
                            </div>  

                            <div class="col-md-12 MyTopMar" id="CategoryType">
                                <%--<center>
                                    <asp:Repeater ID="rptCategory" runat="server">
                                        <ItemTemplate>
                                            <b class="MyLRMar badge" style="font-size:14px;background-color:#abd2e8;padding:7px; color:black"><%#Eval("SUB_CATEGORY_NAME")%></b>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </center>--%>
                            </div>

                            <div class="col-md-12 MyTopMar">
                                <label><span style="color: red;">* </span>Template Body :</label>
                                <%--<textarea id="templateEditor"></textarea>--%>
                                <asp:TextBox ID="templateEditor" runat="server" ClientIDMode="Static" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            
                            
                        </div>
                        
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" Width="85px" OnClick="btnSubmit_Click" ClientIDMode="Static" />
                                &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" Width="85px" OnClick="btnCancel_Click" />
                                
                                <div class="col-md-12">
                                    <asp:Panel ID="Panel1" runat="server" Height="400px" ScrollBars="Auto">
                                        <div class="titlebar">
                                            <h3>
                                                <span class="label label-default pull-left">Template List</span> 
                                            </h3>
                                        </div>
                                        <table class="table table-hover table-bordered">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th width="6%"><center>Sr. No.</center></th>
                                                    <th width="6%"><center>Edit</center></th>
                                                    <th width="6%"><center>Delete</center></th>
                                                    <th>Name</th>
                                                    <th>Subject</th>
                                                    <th>Status</th>
                                                    <th>Category</th>
                                                    <th>Mail Type</th>
                                                    <th width="15%">User</th>
                                                    <th width="20%">Pages</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptTemplate" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><center><%#Container.ItemIndex+1 %></center></td>
                                                            <td>
                                                                <center>
                                                                <img  src='<%=Page.ResolveUrl("~/images/edit.gif") %>' class="btnEditX" name='<%# Eval("NAME")+"$"+Eval("SUBJECT")+"$"+Eval("USER")+"$"+Eval("PAGE")+"$"+Eval("CATEGORY")+"$"+Eval("STAT")+"$"+Eval("TEMPLATE")+"$"+Eval("DATA_LIST")+"$"+Eval("MAIL_TYPE") %>' title="Edit" />
                                                                </center>
                                                            </td>
                                                            <td>
                                                                <center>
                                                                    <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Delete" CommandArgument='<%# Eval("NAME") %>' ImageUrl="~/images/delete.gif" OnClick="btnDelete_Click" OnClientClick="return ConfirmSubmit();" ToolTip="Delete" />
                                                                </center>
                                                            </td>
                                                            <td><%# Eval("NAME")%></td>
                                                            <td><%# Eval("SUBJECT")%></td>
                                                            <td><%# Eval("STATUS")%></td>
                                                            <td><%# Eval("CATEGORY_NAME")%></td>
                                                            <td><%# Eval("MAIL_TYPE_NAME")%></td>
                                                            <td><%# "<b>•</b> "+Eval("USERS").ToString().Replace(",","<br> <b>•</b> ") %></td>
                                                            <td><%# "<b>•</b> "+Eval("PAGES").ToString().Replace(",","<br> <b>•</b> ") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                            </table>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
            <asp:AsyncPostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="div1" runat="Server">
    </div>
    <div id="divMsg" runat="Server">
    </div>

    <script>

        function ShowLoader() {
            //debugger;
            $('.loadingg').removeClass('hidden');
            return true;
        }
        function HideLoader() {
            $('.loadingg').addClass('hidden');
            return true;
        }

        function LoadTinyMCE() {
            $('#templateEditor').tinymce({
                script_url: '../plugins/TinyMce/tinymce.min.js',
                placeholder: 'Enter template content here ...',
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
                //init_instance_callback: function (editor) {
                //    editor.on('mouseup', function (e) {
                //        alert('okoko');
                //    });
                //}
            });
        }


        $(document).ready(function () {
            ShowLoader();
            LoadTinyMCE();

            $('.select2Page').select2({
                placeholder: 'Enter Page name for this template',
            });

            $('.select2UserType').select2({
                placeholder: 'Enter User Name',
            });

            //$(document).ready(function () {
            //    $('#example').DataTable();
            //});

            $.ajax({
                type: "POST",
                url: "CreateTemplate.aspx/FillModules",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (d) {
                    //debugger
                    var data = JSON.parse(d.d);
                    var options = "";
                    var firstVal = "";
                    var nextVal = "";
                    options += "<option value='0'>Please Select</option>";
                    $.each(data, function (a, b) {

                        if (firstVal == "") {
                            firstVal = b.AS_Title;
                            options += '<optgroup label="' + firstVal + '">';
                        }

                        nextVal = b.AS_Title;

                        if (firstVal != nextVal) {
                            firstVal = nextVal;
                            options += '</optgroup> <optgroup label="' + firstVal + '">';
                        }

                        options += "<option value=" + b.AL_No + ">" + b.AL_Link + "</option>";
                    });
                    $("#ddlPage").html(options);
                    HideLoader();
                },
                failure: function (response) {
                    HideLoader();
                    alert("Err1");
                },
                error: function (response) {
                    //debugger
                    HideLoader();
                    alert(response.responseText);
                }
            });


            $.ajax({
                type: "POST",
                url: "CreateTemplate.aspx/UserType",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (d) {
                    //debugger
                    var data = JSON.parse(d.d);
                    var options = "";

                    $.each(data, function (a, b) {
                        options += "<option value=" + b.USERTYPEID + ">" + b.USERDESC + "</option>";
                    });
                    $("#ddlUserType").html(options);
                    HideLoader();
                },
                failure: function (response) {
                    HideLoader();
                    alert("Err1");
                },
                error: function (response) {
                    //debugger
                    HideLoader();
                    alert(response.responseText);
                }
            });

            $('#<%=ddlCategoty.ClientID%>').change(function () {
                debugger
                ShowLoader();
                tinyMCE.triggerSave();
                $.ajax({
                    type: "POST",
                    url: "CreateTemplate.aspx/DataList",
                    data: '{val:'+parseInt($(this).val())+'}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (d) {
                        var data = JSON.parse(d.d);
                        var options = "";
                        options += "<option value='0'>Please Select</option>";
                        $.each(data, function (a, b) {
                            options += "<option value=" + b.ID + "^" + b.SP_NAME + ">" + b.NAME + "</option>";
                        });
                        $("#ddlDataType").html(options);
                        $("#CategoryType").html('');
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

            $('#<%=ddlDataType.ClientID%>').change(function () {
                debugger
                if ($(this).val().split('^')[0] == "0") {
                    $("#CategoryType").html('');
                    return;
                }
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
                            iHtml += '<b class="MyLRMar badge" draggable="true" style="font-size:14px;background-color:#abd2e8;padding:7px; color:black;margin-bottom:5px;">[' + b.NAME + ']</b>';
                        });

                        $("#CategoryType").html(iHtml + "</center>");
                    },
                    failure: function (response) {
                        alert("Err1");
                    },
                    error: function (response) {
                        alert(response.responseText);
                    }
                });
            });

            $('#<%=btnSubmit.ClientID%>').click(function () {

                if ($('#txtTempleteName').val() == '') {
                    alert('Please Enter Template Name.');
                    $('#txtTempleteName').focus();
                    return false;
                }
                else if ($('#txtSubject').val() == '') {
                    alert('Please Enter Subject.');
                    $('#txtSubject').focus();
                    return false;
                }
                else if ($('#ddlMailType').val() == '0') {
                    alert('Please Select Mail Type.');
                    $('#ddlMailType').focus();
                    return false;
                }
                else if ($('.select2UserType').val() == '') {
                    alert('Please Select User Type.');
                    $('.select2UserType').focus();
                    return false;
                }
                else if ($('.select2Page').val() == '' && $('#ddlMailType').val() == '1') {
                    alert('Please Select Page for Template.');
                    $('.select2Page').focus();
                    return false;
                }
                else if ($('#ddlCategoty').val() == '0') {
                    alert('Please Select Category.');
                    $('#ddlCategoty').focus();
                    return false;
                }
                else if ($('#ddlDataType').val() == '0') {
                    alert('Please Data List.');
                    $('#ddlDataType').focus();
                    return false;
                }
                else if ($('#templateEditor').val() == '') {
                    alert('Template body should not be empty.');
                    $('#templateEditor').focus();
                    return false;
                }

                $('#<%=hfdUserType.ClientID%>').val($('.select2UserType').val());
                $('#<%=hfdPage.ClientID%>').val($('.select2Page').val());
                $('#<%=hfdTemplate.ClientID%>').val($('#templateEditor').val().replace('MyLRMar', '').replace('badge', ''));
                $('#<%=hfdCategoryType.ClientID%>').val($('#<%=ddlCategoty.ClientID%>').val());
                $('#<%=hfdDataList.ClientID%>').val($('#<%=ddlDataType.ClientID%>').val().split('^')[0]); 
                $('#<%=hfdMailType.ClientID%>').val($('#ddlMailType').val());

                if ($("#rbtnActive").prop("checked") == true) {
                    $('#<%=hfdStatus.ClientID%>').val(1);
                }
                else {
                    $('#<%=hfdStatus.ClientID%>').val(0);
                }
            });

            $('#<%=btnCancel.ClientID%>').click(function () {
                //tinyMCE.triggerSave();
                tinymce.activeEditor.setContent('');
            });

            document.addEventListener('dragstart', function (event) {
                event.dataTransfer.setData('Text', event.target.innerHTML);
            });

            $('.btnEditX').click(function () {
                debugger
                ShowLoader();
                $('#txtTempleteName').focus();
                $('#txtTempleteName').val($(this).attr('name').split('$')[0]);
                $('#txtTempleteName').attr("disabled", "true");
                $('#txtSubject').val($(this).attr('name').split('$')[1]);
            
                var arr_UserType = new Array();
                for (var i = 0; i < $(this).attr('name').split('$')[2].split(',').length; i++) {
                    arr_UserType[i] = $(this).attr('name').split('$')[2].split(',')[i];
                }
                $('.select2UserType').val(arr_UserType).trigger('change');

                var arr_Page = new Array();
                for (var i = 0; i < $(this).attr('name').split('$')[3].split(',').length; i++) {
                    arr_Page[i] = $(this).attr('name').split('$')[3].split(',')[i];
                }
                $('.select2Page').val(arr_Page).trigger('change');

                $('#ddlCategoty').val($(this).attr('name').split('$')[4]).trigger('change');
                
                if ($(this).attr('name').split('$')[5] == 1) {
                    $("#rbtnActive").prop("checked", true);
                }
                else{
                    $("#rbtnDeactive").prop("checked", true);
                }

                $('#templateEditor').val($(this).attr('name').split('$')[6]);
                $('#<%=hfdOperation.ClientID%>').val(3);
                $('#ddlMailType').val($(this).attr('name').split('$')[8]).trigger('change');
                $('#ddlDataType').val($(this).attr('name').split('$')[7]).trigger('change');
                HideLoader();
            });

            $('#<%=ddlMailType.ClientID%>').change(function () {
                if ($(this).val() == 2) {
                    $('.select2Page').prop('disabled', true);
                    $(".select2Page").val('').trigger('change');
                }
                else
                    $('.select2Page').prop('disabled', false);
            });

        });


        //var prm = Sys.WebForms.PageRequestManager.getInstance();
        //prm.add_beginRequest(function () {
        //    tinyMCE.triggerSave();
        //});


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {

            $(document).ready(function () {

                tinyMCE.remove('#templateEditor');
                LoadTinyMCE();

                $('.select2Page').select2({
                    placeholder: 'Enter Page name for this template',
                });

                $('.select2UserType').select2({
                    placeholder: 'Enter User Name',
                });

                //$(document).ready(function () {
                //    $('#example').DataTable();
                //});

                $.ajax({
                    type: "POST",
                    url: "CreateTemplate.aspx/FillModules",
                    data: '{}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (d) {
                        //debugger
                        var data = JSON.parse(d.d);
                        var options = "";
                        var firstVal = "";
                        var nextVal = "";
                        options += "<option value='0'>Please Select</option>";
                        $.each(data, function (a, b) {

                            if (firstVal == "") {
                                firstVal = b.AS_Title;
                                options += '<optgroup label="' + firstVal + '">';
                            }

                            nextVal = b.AS_Title;

                            if (firstVal != nextVal) {
                                firstVal = nextVal;
                                options += '</optgroup> <optgroup label="' + firstVal + '">';
                            }

                            options += "<option value=" + b.AL_No + ">" + b.AL_Link + "</option>";
                        });
                        $("#ddlPage").html(options);
                        HideLoader();
                    },
                    failure: function (response) {
                        HideLoader();
                        alert("Err1");
                    },
                    error: function (response) {
                        //debugger
                        HideLoader();
                        alert(response.responseText);
                    }
                });


                $.ajax({
                    type: "POST",
                    url: "CreateTemplate.aspx/UserType",
                    data: '{}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (d) {
                        //debugger
                        var data = JSON.parse(d.d);
                        var options = "";

                        $.each(data, function (a, b) {
                            options += "<option value=" + b.USERTYPEID + ">" + b.USERDESC + "</option>";
                        });
                        $("#ddlUserType").html(options);
                        HideLoader();
                    },
                    failure: function (response) {
                        HideLoader();
                        alert("Err1");
                    },
                    error: function (response) {
                        //debugger
                        HideLoader();
                        alert(response.responseText);
                    }
                });

                $('#<%=ddlCategoty.ClientID%>').change(function () {
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
                            $("#ddlDataType").html(options);
                            $("#CategoryType").html('');
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

                $('#<%=ddlDataType.ClientID%>').change(function () {
                    debugger
                    if ($(this).val().split('^')[0] == "0") {
                        $("#CategoryType").html('');
                        return;
                    }
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

                            $("#CategoryType").html(iHtml + "</center>");
                        },
                        failure: function (response) {
                            alert("Err1");
                        },
                        error: function (response) {
                            alert(response.responseText);
                        }
                    });
                });

                $('#<%=btnSubmit.ClientID%>').click(function () {

                    if ($('#txtTempleteName').val() == '') {
                        alert('Please Enter Template Name.');
                        $('#txtTempleteName').focus();
                        return false;
                    }
                    else if ($('#txtSubject').val() == '') {
                        alert('Please Enter Subject.');
                        $('#txtSubject').focus();
                        return false;
                    }
                    else if ($('#ddlMailType').val() == '0') {
                        alert('Please Select Mail Type.');
                        $('#ddlMailType').focus();
                        return false;
                    }
                    else if ($('.select2UserType').val() == '') {
                        alert('Please Select User Type.');
                        $('.select2UserType').focus();
                        return false;
                    }
                    else if ($('.select2Page').val() == '' && $('#ddlMailType').val() == '1') {
                        alert('Please Select Page for Template.');
                        $('.select2Page').focus();
                        return false;
                    }
                    else if ($('#ddlCategoty').val() == '0') {
                        alert('Please Select Category.');
                        $('#ddlCategoty').focus();
                        return false;
                    }
                    else if ($('#ddlDataType').val() == '0') {
                        alert('Please Data List.');
                        $('#ddlDataType').focus();
                        return false;
                    }
                    else if ($('#templateEditor').val() == '') {
                        alert('Template body should not be empty.');
                        $('#templateEditor').focus();
                        return false;
                    }

                    $('#<%=hfdUserType.ClientID%>').val($('.select2UserType').val());
                    $('#<%=hfdPage.ClientID%>').val($('.select2Page').val());
                    $('#<%=hfdTemplate.ClientID%>').val($('#templateEditor').val().replace('MyLRMar', '').replace('badge', ''));
                    $('#<%=hfdCategoryType.ClientID%>').val($('#<%=ddlCategoty.ClientID%>').val());
                    $('#<%=hfdDataList.ClientID%>').val($('#<%=ddlDataType.ClientID%>').val().split('^')[0]);
                    $('#<%=hfdMailType.ClientID%>').val($('#ddlMailType').val());

                    if ($("#rbtnActive").prop("checked") == true) {
                        $('#<%=hfdStatus.ClientID%>').val(1);
                    }
                    else {
                        $('#<%=hfdStatus.ClientID%>').val(0);
                    }
                });

                $('#<%=btnCancel.ClientID%>').click(function () {
                    //tinyMCE.triggerSave();
                    tinymce.activeEditor.setContent('');
                });

                document.addEventListener('dragstart', function (event) {
                    event.dataTransfer.setData('Text', event.target.innerHTML);
                });

                $('.btnEditX').click(function () {
                    ShowLoader();
                    $('#txtTempleteName').focus();
                    $('#txtTempleteName').val($(this).attr('name').split('$')[0]);
                    $('#txtSubject').val($(this).attr('name').split('$')[1]);

                    var arr_UserType = new Array();
                    for (var i = 0; i < $(this).attr('name').split('$')[2].split(',').length; i++) {
                        arr_UserType[i] = $(this).attr('name').split('$')[2].split(',')[i];
                    }
                    $('.select2UserType').val(arr_UserType).trigger('change');

                    var arr_Page = new Array();
                    for (var i = 0; i < $(this).attr('name').split('$')[3].split(',').length; i++) {
                        arr_Page[i] = $(this).attr('name').split('$')[3].split(',')[i];
                    }
                    $('.select2Page').val(arr_Page).trigger('change');

                    $('#ddlCategoty').val($(this).attr('name').split('$')[4]).trigger('change');

                    if ($(this).attr('name').split('$')[5] == 1) {
                        $("#rbtnActive").prop("checked", true);
                    }
                    else {
                        $("#rbtnDeactive").prop("checked", true);
                    }

                    $('#templateEditor').val($(this).attr('name').split('$')[6]);
                    $('#<%=hfdOperation.ClientID%>').val(3);
                    $('#ddlMailType').val($(this).attr('name').split('$')[8]).trigger('change');
                    $('#ddlDataType').val($(this).attr('name').split('$')[7]).trigger('change');
                    HideLoader();
                });

                $('#<%=ddlMailType.ClientID%>').change(function () {
                    if ($(this).val() == 2) {
                        $('.select2Page').prop('disabled', true);
                        $(".select2Page").val('').trigger('change');
                    }
                    else
                        $('.select2Page').prop('disabled', false);
                });

            });

        });
       
    </script>
   
</asp:Content>
