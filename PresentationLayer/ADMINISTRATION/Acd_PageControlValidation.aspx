<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Acd_PageControlValidation.aspx.cs" Inherits="Acd_PageControlValidation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl_details"
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
    <asp:UpdatePanel ID="updpnl_details" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">

                            <div class="col-12">


                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <span style="color: red;">* </span>
                                            <label>Page Name </label>
                                        </div>
                                        <select id="ddlPageName" class="form-control" data-select2-enable="true" tabindex="3" name="ddlPageName" onchange="handleDropDownChange();">
                                        </select>
                                    </div>
                                    <div id="section" class="form-group col-lg-3 col-md-6 col-12 d-none ">
                                        <div class="label-dynamic">
                                            <span style="color: red;">* </span>
                                            <label>Section</label>
                                        </div>
                                        <select id="ddlsection" class="form-control" data-select2-enable="true" tabindex="3" name="ddlsection" onchange="handleDropDownChange1();">
                                        </select>
                                    </div>
                                    <div id="Chksection" class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <label>Active</label>

                                        <div class='switch form-inline'>
                                            <input type='checkbox' id='Chkdisplay_section' name='switch' onclick='return SetSectionCheckbox(this);' onchange="ddlpagedisplay(this);" />
                                            <label data-on='Yes' class='newAddNew Tab' data-off='No' for='Chkdisplay_section'></label>
                                        </div>
                                    </div>
                                </div>
                                <div id="divStudentConfig" class="mt-3 d-none">
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <input type="button" value="Submit" id="btnStudentSubmit" class="btn btn-primary" runat="server" />
                                <input type="button" value="Reset" id="btnReset" class="btn btn-warning" />
                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $("#ctl00_ContentPlaceHolder1_btnStudentSubmit").click(function()
        {
           
            if(pagenamevalid() == true)
            {
                var arrItems = [];
                $('#StudentConfig').find('tr').each(function () 
                {
                    
                    var objArray= {};
                    var row = $(this);

                    var _studconfig_id,_caption_name,_field_name,_isactive,_ismandatory,_iseditable,_organization_id,_page_no,_page_name,_displaysection;

                    _studconfig_id = row.find('td').eq(0).text();
                    _caption_name = row.find('td').eq(1).text();
                    _field_name = row.find('td').eq(2).text();
                    _isactive = row.find("#rdISACTIVE" + row.find('td').eq(0).text()).is(":checked")
                    _ismandatory = row.find("#rdISMANDATORY" + row.find('td').eq(0).text()).is(":checked")
                    _iseditable = row.find("#rdISEDITABLE" + row.find('td').eq(0).text()).is(":checked")
                    _organization_id = row.find('td').eq(5).text();
                    _page_no = row.find('td').eq(6).text();
                    _page_name = row.find('td').eq(7).text();
                

                    if(document.getElementById('Chkdisplay_section').checked) 
                    {
                        _displaysection = true;
                    } 
                    else 
                    {
                        _displaysection = false;
                    }
                    if (_studconfig_id != '') 
                    {
                        objArray["studconfig_id"] = _studconfig_id;
                        objArray["caption_name"] = _caption_name;
                        objArray["isactive"] = _isactive;
                        objArray["ismandatory"] = _ismandatory;
                        objArray["iseditable"] = _iseditable;
                        objArray["organization_id"] = _organization_id;
                        objArray["page_no"] = _page_no;
                        objArray["pagename"] = _page_name;
                        objArray["displaysection"] = _displaysection;
                        arrItems.push(objArray);
                    }
                });
                SaveUpdateStudentConfig(arrItems);
            }
        });

        function SaveUpdateStudentConfig(_studentConfig)
        {
            var JData = '{StudentConfig: ' + JSON.stringify(_studentConfig) + '}';
           
            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("Acd_PageControlValidation.aspx/SaveUpdateStudentconfig") %>',
                data: JData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) 
                {
                    var Jdata = data.d;
                    alert(Jdata);
                },
                failure: function (response)
                {
                    alert("failure");
                },
                error: function (response)
                {
                    alert("error");
                    alert(response.responseText);
                }
            });
        }

    </script>
    <script>
        function ddlpagedisplay(Chksection)  
        {
            if (Chksection.checked)
            {
                $('#divStudentConfig').removeClass('d-none');
            } else {
                $('#divStudentConfig').addClass('d-none');
            }
        }
    </script>

    <script>
        function pagenamevalid()
        {
            var section = sessionStorage.getItem('session');
            if ($('#ddlPageName').val() == 0) 
            {
                alert("Please Select Page Name");
                return false;
            } 
            else if(section=="1" && $('#ddlsection').val() == 0)
            {
                alert("Please Select Section");
                return false;
            }
            return true;
        }
    </script>
    <script>
        document.getElementById("btnReset").addEventListener("click", function () 
        {
            $('#section').addClass('d-none');
            $('#Chksection').addClass('d-none');
            $('#divStudentConfig').addClass('d-none');
            $('#Chkdisplay_section').prop('checked', false); 
            getpagename();
        });
    </script>
    <script type="">
        $(document).ready(function () {
            acd_getpagename();
        })

        function acd_getpagename() {
            $("#ddlPageName").empty();
            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("Acd_PageControlValidation.aspx/ACD_Getpagename") %>',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var Jdata = JSON.parse(data.d);
                    $("#ddlPageName").append($("<option></option>").val(0).html('Please Select'));
                    $.each(Jdata, function (key, value) {
                        if (value.DISPLAYPAGENAME != null) {
                            $("#ddlPageName").append($("<option></option>").val(value.ORGANIZATION_ID).html(value.DISPLAYPAGENAME));
                        }
                    });
                },
                error: function (xhr, status, error) {
                    console.error(xhr.responseText);
                }
            });
        }
    </script>

    <script>
        $(document).ready(function ()
        {
            var sessionvalue = "<%=Session["OrgId"]%>";
            BindStudentconfig(sessionvalue,"","AddressDetails.aspx");
        });
        function BindStudentconfig(OrgID_,PageNo_,PageName_)
        {
            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("Acd_PageControlValidation.aspx/GetStudentConfigData") %>',               
                data: JSON.stringify({ OrgID:OrgID_, PageNo:PageNo_, PageName:PageName_,SectionName:""}),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) 
                {
                    var Jdata = JSON.parse(data.d);                  
                    var htmlpage = "<table class='table table-striped table-bordered nowrap ' id='StudentConfig'>" ;
                    htmlpage += "<thead class='bg-light-blue'><tr>";
                    htmlpage += "<th hidden>STUDCONFIG_ID</th>";
                    htmlpage += "<th>Caption Name</th>";
                    htmlpage += "<th>Is Active</th>";
                    htmlpage += "<th>Is Mandatory</th>";
                    htmlpage += "<th>Is Editable</th>";
                    htmlpage += "<th hidden>Organization ID</th>";
                    htmlpage += "<th hidden>Page No</th>";
                    htmlpage += "<th hidden>Page Name</th>";
                    htmlpage += "</tr></thead><tbody>";
                       
                    var IS_EDITABLE="";
                    if(i.ISACTIVE==true)
                    {
                        IS_EDITABLE="checked";
                    }
                    var output = Jdata.map(i => 
                        "<tr>"+
                        "<td hidden>" + i.STUDCONFIG_ID +  "</td>" +
                        "<td>" + i.CAPTION_NAME + "</td>" + 
                        "<td class='text-center; vertical-align:middle'><div class='switch form-inline'>" +
                        "<input type='checkbox' id='rdISACTIVE"+ i.STUDCONFIG_ID +"' name='switch' onclick='return SetStudent("+ i.STUDCONFIG_ID +");' "+ i.ISACTIVE +"/>" +
                        "<label data-on='Yes' class='newAddNew Tab'  data-off='No' for='rdISACTIVE"+ i.STUDCONFIG_ID +"' ></label></td>"+
                        "<td><div class='switch form-inline'>" +
                        "<input type='checkbox' id='rdISMANDATORY"+ i.STUDCONFIG_ID +"' name='switch' onclick='return SetStudentCheckbox(this);' "+ i.ISMANDATORY +"/>" +
                        "<label data-on='Yes' class='newAddNew Tab'  data-off='No' for='rdISMANDATORY"+ i.STUDCONFIG_ID +"' ></label></td>"+
                        "<td><div class='switch form-inline'>" +
                        "<input type='checkbox' id='rdEDITABLE"+ i.STUDCONFIG_ID +"' name='switch' onclick='return SetStudentCheckbox(this);' "+ IS_EDITABLE +"/>" +
                        "<label data-on='Yes' class='newAddNew Tab'  data-off='No' for='rdEDITABLE"+ i.STUDCONFIG_ID +"' ></label></td>"+
                        "<td style='text-align:center; vertical-align:middle' hidden>" + i.ORGANIZATION_ID + "</td>"+
                        "<td style='text-align:center; vertical-align:middle' hidden>" + i.PAGE_NO + "</td>"+
                        "<td style='text-align:center; vertical-align:middle' hidden>" + i.PAGE_NAME + "</td></tr>");
                   
                     
                    for (var i = 0; i < output.length; i++) 
                    {
                        htmlpage =  htmlpage + output[i];
                       
                    }
                    
                    htmlpage = (htmlpage + '</tbody></table>');

                    $('#divStudentConfig').html('');
                    $('#divStudentConfig').append(htmlpage);
                 
                    for (var i = 0; i < Jdata.length; i++)
                    {
                        if (Jdata[i].ISMANDATORY != 'checked' && Jdata[i].ISACTIVE != 'checked' ) 
                        {
                            $('#rdISMANDATORY' + Jdata[i].STUDCONFIG_ID).prop('checked', false);
                            $("#rdISMANDATORY" + Jdata[i].STUDCONFIG_ID).attr("disabled", true);
                        }
                    }
                },
                failure: function (response) 
                {
                    alert("failure");
                },
                error: function (response) {
                  
                    alert("error");
                    alert(response.responseText);
                }
            });
        }
        function SetStudent(val) {
            if ($('#rdISACTIVE' + val)[0].checked == false) 
            {
                $('#rdISMANDATORY' + val).prop('checked', false);
                $("#rdISMANDATORY" + val).attr("disabled", true);
            }
            else
            {
                $('#rdISMANDATORY' + val).prop('checked', false);
                $("#rdISMANDATORY" + val).attr("disabled", false);
            }
           
        }
        function SetStudentCheckbox(val)
        {
            var chk = val.id;
        }
    </script>
    <script type="text/javascript">
        var pageName = "";
        function handleDropDownChange() {
            var selectElement = document.getElementById("ddlPageName");
            var selectedOption = selectElement.options[selectElement.selectedIndex];
            var selectedText = selectedOption.text;
            var selectedvalue = $('#ddlPageName').val();

            $('#Chksection').addClass('d-none');
            $('#Chkdisplay_section').prop('checked', false);

            if (selectedvalue == 0) {
                $('#section').addClass('d-none');   
                $('#divStudentConfig').addClass('d-none');     
            }
            else {

                var orgID = '<%= Session["OrgId"] %>';
                var pageNo = "";

                if (selectedText === "New Student") {
                    pageNo = "73";
                }
                else if (selectedText === "Personal Details") {
                    pageName = "PersonalDetails.aspx";
                }
                else if (selectedText === "Admission Details") {
                    pageName = "AdmissionDetails.aspx";
                }
                else if (selectedText === "Address Details") {
                    pageName = "AddressDetails.aspx";
                }
                else if (selectedText === "Qualification Details") {
                    pageName = "QualificationDetails.aspx";
                }
                else if (selectedText === "Other Information") {
                    pageName = "OtherInformation.aspx";
                }

                getsection(pageName);
            }
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=-]").val($(this).attr("href").replace("#", ""));

            });
        }

        function handleDropDownChange1() {
            var selectedvalue = $('#ddlsection').val();
            if (selectedvalue == 0) {
                $('#Chkdisplay_section').prop('checked', false);
                $('#divStudentConfig').addClass('d-none');
                $('#Chksection').addClass('d-none');
            }
            else {
                var orgID = '<%= Session["OrgId"] %>';
                  var pageNo = "";
                  var pagename = '<%= Session["PageName"] %>';
                var sectionName = $("#ddlsection option:selected").text();
                $('#Chksection').removeClass('d-none');
                BindStudnetConfiguration(orgID, pageNo, pageName, sectionName)
            }
        }

        function BindStudnetConfiguration(orgID, pageNo, pageName, sectionName) {
            $.ajax({

                type: "POST",
                url: '<%= ResolveUrl("Acd_PageControlValidation.aspx/GetStudentConfigData") %>',
                data: JSON.stringify({ OrgID: orgID, PageNo: pageNo, PageName: pageName, SectionName: sectionName }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {

                    var Jdata = JSON.parse(data.d);
                    if (Jdata != "") {
                        if (Jdata[0].IS_DISPLAY_SECTION_NAME == true) {
                            $('#Chkdisplay_section').prop('checked', true);
                            $('#divStudentConfig').removeClass('d-none');
                        }
                        else {
                            if (Jdata[0].SECTIONNAME == null) {
                                $('#divStudentConfig').removeClass('d-none');
                            }
                            else {
                                $('#Chkdisplay_section').prop('checked', false);
                                $('#divStudentConfig').addClass('d-none');
                            }
                        }
                        var htmlpage = "<table class='table table-striped table-bordered nowrap ' id='StudentConfig'>";
                        htmlpage += "<thead class='bg-light-blue'><tr>";
                        htmlpage += "<th hidden>STUDCONFIG_ID</th>";
                        htmlpage += "<th>Caption Name</th>";
                        htmlpage += "<th>Is Active</th>";
                        htmlpage += "<th>Is Mandatory</th>";
                        htmlpage += "<th>Is Editable</th>";
                        htmlpage += "<th hidden>Organization ID</th>";
                        htmlpage += "<th hidden>Page No</th>";
                        htmlpage += "<th hidden>Page Name</th>";
                        htmlpage += "</tr></thead><tbody>";

                        var output = Jdata.map(function (i) {

                            var IS_EDITABLE = "";
                            if (i.IS_EDITABLE == true) {
                                IS_EDITABLE = "checked";

                            }

                            return "<tr>" +
                                "<td hidden>" + i.STUDCONFIG_ID + "</td>" +
                                "<td>" + i.CAPTION_NAME + "</td>" +
                                "<td class='text-center; vertical-align:middle'><div class='switch form-inline'>" +
                                "<input type='checkbox' id='rdISACTIVE" + i.STUDCONFIG_ID + "' name='switch' onclick='return SetStudent(" + i.STUDCONFIG_ID + ");' " + i.ISACTIVE + "/>" +
                                "<label data-on='Yes' class='newAddNew Tab'  data-off='No' for='rdISACTIVE" + i.STUDCONFIG_ID + "' ></label></td>" +
                                "<td><div class='switch form-inline'>" +
                                "<input type='checkbox' id='rdISMANDATORY" + i.STUDCONFIG_ID + "' name='switch' onclick='return SetStudentCheckbox(this);' " + i.ISMANDATORY + "/>" +
                                "<label data-on='Yes' class='newAddNew Tab'  data-off='No' for='rdISMANDATORY" + i.STUDCONFIG_ID + "' ></label></td>" +
                                 "<td><div class='switch form-inline'>" +
                            "<input type='checkbox' id='rdISEDITABLE" + i.STUDCONFIG_ID + "' name='switch' onclick='return SetStudentCheckbox(this);' " + IS_EDITABLE + "/>" +
                                "<label data-on='Yes' class='newAddNew Tab'  data-off='No' for='rdISEDITABLE" + i.STUDCONFIG_ID + "' ></label></td>" +
                                "<td style='text-align:center; vertical-align:middle' hidden>" + i.ORGANIZATION_ID + "</td>" +
                                "<td style='text-align:center; vertical-align:middle' hidden>" + i.PAGE_NO + "</td>" +
                                "<td style='text-align:center; vertical-align:middle' hidden>" + i.PAGE_NAME + "</td></tr>";
                        });
                        for (var i = 0; i < output.length; i++) {
                            htmlpage += output[i];
                        }

                        htmlpage += '</tbody></table>';

                        $('#divStudentConfig').html('');
                        $('#divStudentConfig').append(htmlpage);

                        for (var i = 0; i < Jdata.length; i++) {
                            if (Jdata[i].ISMANDATORY !== 'checked' && Jdata[i].ISACTIVE !== 'checked') {
                                $('#rdISMANDATORY' + Jdata[i].STUDCONFIG_ID).prop('checked', false);
                                $("#rdISMANDATORY" + Jdata[i].STUDCONFIG_ID).attr("disabled", true);
                            }
                        }
                    }
                    else {
                        $('#Chkdisplay_section').prop('checked', false);
                        $('#divStudentConfig').addClass('d-none');
                    }

                },
                failure: function (response) {
                    alert("failure");
                },
                error: function (response) {
                    alert("error");
                }
            });
        }
    </script>

    <script>
        function getsection(pageName) {
            $("#ddlsection").empty();
            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("Acd_PageControlValidation.aspx/Getsection") %>',
                data: JSON.stringify({ PageName: pageName }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var Jdata = JSON.parse(data.d);
                    if (Jdata != "") {
                        $("#ddlsection").append($("<option></option>").val(0).html('Please Select'));
                        $.each(Jdata, function (key, value) {
                            if (value.SECTIONNAME != null) {
                                $("#ddlsection").append($("<option></option>").val(value.ORGANIZATION_ID).html(value.SECTIONNAME));
                            }
                        });
                        $('#section').removeClass('d-none');
                        $('#divStudentConfig').addClass('d-none');
                        sessionStorage.setItem('session', '1');
                    }
                    else {

                        var orgID = '<%= Session["OrgId"] %>';
                        var pagename = '<%= Session["PageName"] %>';
                        BindStudnetConfiguration(orgID, "", pageName, "")
                        $('#section').addClass('d-none');
                        $('#Chksection').addClass('d-none');
                        $('#divStudentConfig').removeClass('d-none');
                        sessionStorage.setItem('session', '0');
                    }

                },
                error: function (xhr, status, error) {
                    console.error(xhr.responseText);
                }
            });
        }
    </script>

</asp:Content>

