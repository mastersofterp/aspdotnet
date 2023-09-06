<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPInterviewTestConfig.aspx.cs" Inherits="InterviewTestConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .table-bordered > thead > tr > th {
            border-top: 1px solid #e5e5e5;
        }
    </style>
    <script type="text/javascript">
        function EditPanelNameFor(_this)
        {
            debugger
            totalcolumns = $("#tblPnlFor tr td").length
            row = $(_this).closest("tr");
            var mainval = row.find("td").find("[type='hidden']").val();
            var mainval2 = row.find("td").eq(1).html();
            var mainval3 = row.find("td").eq(2).html();
            //row.find("td:eq(" + (totalcolumns - 1) + ") input[type='text']").val(_value);
            //var mainval = row.find("input[type='hidden']").val();

           // var mainval = $(_this).closest("td").find("input[type='hidden']").val();
            $('#txtPanelName').val(mainval2);
            $('#hdnPanelId').val(mainval);
            if (mainval3 == "Active") {
                //$('#hfdShowStatus').val($('#rdActive').prop('checked'));
                $('#rdActive').prop('checked',true);
            }
            else {
                $('#rdActive').prop('checked', false);
            }
            //alert(mainval3);
        }

    </script>
    <script type="text/javascript">
        function myFunction(_this) {
            //   debugger
            totalcolumns = $("#TabInterview tr th").length
            row = $(_this).closest("tr")
            var _value = 0;
            var cellvalue = 0;

            for (var i = 1; i < totalcolumns - 1; i++) {
                cellvalue = parseInt(row.find("td:eq(" + i + ") input[type='text']").val());
                if (isNaN(cellvalue)) { cellvalue = 0; }
                _value = parseInt(_value) + cellvalue;
            }
            row.find("td:eq(" + (totalcolumns - 1) + ") input[type='text']").val(_value);
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
           
            $.ajax({
                url: '<%= ResolveUrl("ACDOnlinePostAdmission.asmx/GetDataPanelFor") %>',
                data: {},
                  type: "GET",
                  //contentType: "application/json; charset=utf-8",
                  dataType: "text",
                  success: function (data) {
                    
                      //var arr = JSON.parse(data);

                      var arr = JSON.parse(data);
                      var arr1, arr2;
                      // Loop throught the array.
                      for (var i = 0; i < arr.length; i++) {
                          console.log(arr[i]);
                          if (i == 0) {
                              arr1 = arr[i];
                          } else if (i == 1) {
                              arr2 = arr[i]
                          }
                      }
                      var html = '<thead class="bg-light-blue"><tr><th> Sr.No</th>'
                      html += '<th>  Panel Name </th>'
                      html += '<th> Status </th></tr></thead>'

                      var a=1;
                       html += '<tbody><tr>'

                      for (var i = 0; i < arr1.length; i++) {
                          //var Status = arr1[i]["ACTIVE_STATUS"];
                          //if (Status == true) {
                          //    Status = 'Active';
                          //} else (Status == false)
                          //{
                          //    Status = 'InActive';
                          //}
                          //html += '<td>' + Number(a) +'</td>'

                          html += '<td>' + '<img src="../../Images/edit.png"  onclick="EditPanelNameFor(this)" />' + '<input type="hidden" id="PANELFORID"  value = "' + arr1[i]["PANELFORID"] + '">'+'</td>'
                          html += '<td>' + arr1[i]["PANELFOR_NAME"]  + '</td>'
                          //html += '<td>' + Status + '</td>'
                          html += '<td>' + arr1[i]["ACTIVE_STATUS"]+ '</td>'
                          html += '</tr>'
                          //a= Number(a) + 1;
                      }
                      html += '</tbody>'
                      $('#tblPnlFor').html(html);


                  },
                  failure: function (response) {
                      alert("failure");
                  },
                  error: function (response) {
                      //debugger
                      alert("error");
                      alert(response.responseText);
                  }
              });
        });

        function onDDLChange(id) {

            debugger
            var BatchNo = $('#ctl00_ContentPlaceHolder1_ddlAdmissionBatch').find('option:selected').val();
            var DegreeNo = $('#ctl00_ContentPlaceHolder1_ddlDegree').find('option:selected').val();

            //alert(BatchNo + " " + DegreeNo);

            bindListViewData(BatchNo, DegreeNo);

        }

        function bindListViewData(BatchNo, DegreeNo) {
            //alert(BatchNo + " " + DegreeNo);
            debugger
            $.ajax({
                type: 'POST',
                dataType: 'text',
                url: '<%=Page.ResolveUrl("ACDOnlinePostAdmission.asmx/GetInterviewData") %>',
                //data: '{BatchNo:' + BatchNo + ', DegreeNo:' + DegreeNo + '}',
                data: { BatchNo: BatchNo, DegreeNo: DegreeNo },

                success: function (data) {
                    debugger
                    //alert(data);
                    var arr = JSON.parse(data);
                    var arr1, arr2;
                    // Loop throught the array.
                    for (var i = 0; i < arr.length; i++) {
                        console.log(arr[i]);
                        if (i == 0) {
                            arr1 = arr[i];
                        } else if (i == 1) {
                            arr2 = arr[i]
                        }
                    }

                    // Table header Dynamic creation from databse
                    var html = '<thead class="bg-light-blue"><tr><th> Program </th>'

                    for (var i = 0; i < arr1.length; i++) {
                        //console.log(arr1[i]["PANELFOR_NAME"]);

                        html += '<th>' + arr1[i]["PANELFOR_NAME"] + '<input type="hidden" id="PANELFORID"  value = "' + arr1[i]["PANELFORID"] + '">' + '</th>'
                    }
                    html += '<th> Total </th></tr></thead>'
                    $('#TabInterview').html(html);


                    // Table data Dynamic creation from databse
                    var totVal = 0;
                    var lastdata = '';
                    var alldatahtml = "<tbody>";

                    for (var j = 0; j < arr2.length; j++) {
                        if (lastdata != arr2[j]["BRANCHNO"]) {

                            alldatahtml += '<tr><td>' + arr2[j]["SHORTNAME"] + '<input type="hidden" id="hdnBranch"  value = "' + arr2[j]["BRANCHNO"] + '">' + '</td>';

                            for (var k = 0; k < arr2.length; k++) {

                                if (arr2[j]["BRANCHNO"] == arr2[k]["BRANCHNO"]) {
                                    // onKeyUp = "setMaxLength(this)"  form-control//alldatahtml += '<td><input type="text" id="txtmarks" onkeypress="return functionx(event);" value = "' + arr2[j]["MAXMARKS"] + '" class = "form-control"  onKeyUp="setMaxLength(this)" isMaxLength="3" MaxLength="3" min="0"></td>';
                                    //alldatahtml += '<td><input type="text" id="txtmarks' + k + '" onkeypress="return functionx(event);" value = "' + arr2[j]["MAXMARKS"] + '" class = "form-control" onKeyUp = "setMaxLength(this)"  isMaxLength="3" MaxLength="3" min="0"></td>';
                                    alldatahtml += '<td><input type="text" id="txtmarks' + k + '"  value = "' + arr2[k]["MAXMARKS"] + '" class ="quetext"  onkeyup="myFunction(this)"  isMaxLength="3" MaxLength="3" min="0"></td>';
                                    totVal = totVal + arr2[k]["MAXMARKS"];
                                }
                            }

                            alldatahtml += '<td><input type="text" id="txtTotal' + j + '" disabled="true" value="' + totVal + '" ></td>';
                            totVal = 0;
                            alldatahtml += '</tr>';
                        }
                        lastdata = arr2[j]["BRANCHNO"];

                    }

                    alldatahtml += '</tbody>';
                    $('#TabInterview').append(alldatahtml);
                    $('#TabInterview').dataTable();
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
    </script>

    <div class="col-md-12 col-sm-12 col-12">
        <div class="box box-primary">
            <div id="div1" runat="server"></div>
            <div class="box-header with-border">
                <div class="box-header with-border">
                    <%--<h3 class="box-title">Interview And Test Configurations</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                    </h3>
                </div>
            </div>

            <div id="Tabs" role="tabpanel">
                <div class="card-body">
                    <div id="divqualification">
                        <div class="col-12">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1">Configuration</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2">Interview And Test</a>
                                    </li>

                                </ul>

                                <div class="tab-content" id="my-tab-content">

                                    <%-- TAB: Configuration Test --%>
                                    <div class="tab-pane active" id="tab_1">
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-12">
                                                <div class="box-header with-border">
                                                    <div class="box-header with-border">

                                                        <h3 class="box-title">
                                                            <asp:Label ID="Label2" runat="server"></asp:Label>
                                                        </h3>
                                                    </div>
                                                </div>

                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Panel Name For</label>
                                                            </div>
                                                            <input type="text"  id="txtPanelName" class="form-control"> 
                                                            <input type="hidden" id="hdnPanelId" value="0"/>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Status</label>
                                                            </div>
                                                            <div class="switch form-inline">
                                                                <input type="checkbox" id="rdActive" name="switch" checked />
                                                                <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                                            </div>
                                                            <%--<asp:HiddenField ID="hfdShowStatus" runat="server" ClientIDMode="Static" />--%>
                                                            <input type="hidden" id="hfdShowStatus"/>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="div2" class="col-12" runat="server">
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%;" id="Table1">
                                                    </table>

                                                    <div id="Div3">
                                                          <table class="table table-striped table-bordered nowrap display" style="width: 100%;" id="tblPnlFor">
                                                    </table>
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <button id="btnSave" class="btn btn-outline-info btnX">Submit</button>
                                                        <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="btn btn-warning" />

                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <%-- TAB:Interview And Test --%>
                                    <div class="tab-pane fade" id="tab_2">
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-12">
                                                <div class="box-header with-border">
                                                    <div class="box-header with-border">

                                                        <h3 class="box-title">
                                                            <asp:Label ID="Label1" runat="server"></asp:Label>
                                                        </h3>
                                                    </div>
                                                </div>

                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Admission Batch</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlAdmissionBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" onchange="onDDLChange(this);" TabIndex="1">
                                                                <%--OnSelectedIndexChanged="ddlAdmissionBatch_SelectedIndexChanged"--%>
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfddlAdmissionBatch" runat="server" ControlToValidate="ddlAdmissionBatch"
                                                                ErrorMessage="Please Select Admission Batch" Display="None" ValidationGroup="InterviewTest" InitialValue="Please Select"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Degree</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" onchange="onDDLChange(this);" CssClass="form-control" data-select2-enable="true" TabIndex="2" AutoPostBack="false">
                                                                <%-- OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"--%>
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                                                ErrorMessage="Please Select Degree" Display="None" ValidationGroup="InterviewTest" InitialValue="Please Select"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="divInter" class="col-12" runat="server">
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%;" id="TabInterview">
                                                    </table>

                                                    <div id="CalTR">
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                    
                                                        <button id="btnSubmit" class="btn btn-outline-info btnX"  >Submit</button>
                                                  
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" />
                                                    

                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="InterviewTest"
                                                            DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $("#btnSubmit").click(function () {
            debugger;
            var _issave = 'Y';

            var BatchNo = $('#ctl00_ContentPlaceHolder1_ddlAdmissionBatch').find('option:selected').val();
            var DegreeNo = $('#ctl00_ContentPlaceHolder1_ddlDegree').find('option:selected').val();


            var totalcolumns = $("#TabInterview tr th").length
            var columns = new Array();

            var colname = '';

            var columnValue = new Array();
            var colValue = '';

            for (var i = 0; i <= totalcolumns - 1; i++) {
                colname = $("#TabInterview tr th").eq(i).text();
                columns.push(colname);
                var colValue = {};
                colValue.PANELFORID = $("#TabInterview tr th").eq(i).find('[type="hidden"]').val();
                // alert(colValue);
                if (colValue.PANELFORID != undefined) {
                    columnValue.push(colValue);
                }
            }

            var marks = new Array();
            var coverageid = 0;


            $("#TabInterview tbody tr").each(function () {

                for (var i = 1; i <= totalcolumns - 2; i++) {

                    var row = $(this);
                    var mark = {};

                    var SHORTNAME = row.find("TD").eq(0).html();

                    mark.BRANCHNO = row.find("TD").eq(0).find('[type="hidden"]').val();

                    mark.BATCHNO = BatchNo;
                    mark.DEGREENO = DegreeNo;

                    var colname = columns[i];
                    mark.PanelFor = colname;
                    var colvalue = $(this).find("td:eq(" + i + ") input[type='text']").val();
                    mark.MaxMarks = colvalue;
                    marks.push(mark);

                }
            });

            $.ajax({

                url: '<%= ResolveUrl("ACDOnlinePostAdmission.asmx/SaveMarks") %>',
                data: "{marks:'" + JSON.stringify(marks) + "', columnValue:'" + JSON.stringify(columnValue) + "'}",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (d) {

                    var data = JSON.stringify(d.d).replace('"', "").replace('"', "");
                    //alert(data);
                    if (data == "2") {


                        alert('Cource coverage Topic already exists for Date ');

                    }
                    else if (data == "3") {


                        alert('Test Configuration update successfully.');

                    }
                    else if (data == "1") {

                        alert('Test Configuration saved successfully.');

                    }



                },
                failure: function (response) {
                    alert("failure");
                },
                error: function (response) {
                    //debugger
                    alert("error");
                    alert(response.responseText);
                }
            });
        });
    </script>
    <script>
        $("#btnSave").click(function () {
            debugger
            var PANELNAME ='';
            var ACTIVESTATUS = '';
            var PANELFORID = '';

            var PANELNAME = $('#txtPanelName').val();

            var ACTIVESTATUS = $('#hfdShowStatus').val();

            var PANELFORID = $('#hdnPanelId').val();

            if (ACTIVESTATUS == "")
            {
                ACTIVESTATUS = 'true'
            }

          
          
            $.ajax({

           url: '<%= ResolveUrl("ACDOnlinePostAdmission.asmx/SavePanelFor") %>',
                data: "{PANELNAME:'" + PANELNAME + "', ACTIVESTATUS:'" + ACTIVESTATUS + "', PANELFORID:'" + PANELFORID + "'}",
           type: "POST",
           contentType: "application/json; charset=utf-8",
           dataType: "json",
           success: function (d) {
               debugger
               var data = JSON.stringify(d.d).replace('"', "").replace('"', "");
               //alert(data);
               if (data == "3") {
                   alert('Panel For Name Entry Already Exist');

               }
               else if (data == "2") {


                   alert('Recored update successfully.');

               }
               else if (data == "1") {

                   alert('Recored saved successfully.');

               }
           },
           failure: function (response) {
               alert("failure");
           },
           error: function (response) {
               //debugger
               alert("error");
               alert(response.responseText);
           }
       });
        });
            
        </script>

    <script>
        $("#rdActive").change(function () {
            debugger
            var sts = $('#rdActive').prop('checked');
            $('#hfdShowStatus').val(sts);
        });
    </script>
    <script>
        function checkedActiveInt_Change(ChkInterMark) {

            var row = ChkInterMark.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;//get current row index                      
            var row, id;
            row = $(ChkInterMark).parent().parent();

            var checked = $("#ctl00_ContentPlaceHolder1_lvInterview_ctrl" + rowIndex + "_ChkInterMark").is(':checked');

            if (checked) {
                $("#ctl00_ContentPlaceHolder1_lvInterview_ctrl" + rowIndex + "_txtInterMarks").removeAttr("disabled");
                $("#ctl00_ContentPlaceHolder1_lvInterview_ctrl" + rowIndex + "_txtInterMarks").attr("enabled", "enabled");


            } else {
                $("#ctl00_ContentPlaceHolder1_lvInterview_ctrl" + rowIndex + "_txtInterMarks", row).val('');
                $("#ctl00_ContentPlaceHolder1_lvInterview_ctrl" + rowIndex + "_txtInterMarks").attr("disabled", "disabled");

            }


        };
        function checkedActiveGD_Change(ChkGDMark) {

            var row = ChkGDMark.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;//get current row index                      
            var row, id;
            row = $(ChkGDMark).parent().parent();

            var checked = $("#ctl00_ContentPlaceHolder1_lvInterview_ctrl" + rowIndex + "_ChkGDMark").is(':checked');

            if (checked) {
                $("#ctl00_ContentPlaceHolder1_lvInterview_ctrl" + rowIndex + "_txtGDMarks").removeAttr("disabled");
                $("#ctl00_ContentPlaceHolder1_lvInterview_ctrl" + rowIndex + "_txtGDMarks").attr("enabled", "enabled");


            } else {
                $("#ctl00_ContentPlaceHolder1_lvInterview_ctrl" + rowIndex + "_txtGDMarks", row).val('');
                $("#ctl00_ContentPlaceHolder1_lvInterview_ctrl" + rowIndex + "_txtGDMarks").attr("disabled", "disabled");

            }


        };
        function checkedActiveTst_Change(ChkTestMark) {

            var row = ChkTestMark.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;//get current row index                      
            var row, id;
            row = $(ChkTestMark).parent().parent();

            var checked = $("#ctl00_ContentPlaceHolder1_lvInterview_ctrl" + rowIndex + "_ChkTestMark").is(':checked');

            if (checked) {
                $("#ctl00_ContentPlaceHolder1_lvInterview_ctrl" + rowIndex + "_txtTest").removeAttr("disabled");
                $("#ctl00_ContentPlaceHolder1_lvInterview_ctrl" + rowIndex + "_txtTest").attr("enabled", "enabled");


            } else {
                $("#ctl00_ContentPlaceHolder1_lvInterview_ctrl" + rowIndex + "_txtTest", row).val('');
                $("#ctl00_ContentPlaceHolder1_lvInterview_ctrl" + rowIndex + "_txtTest").attr("disabled", "disabled");

            }


        };
        function functionx(evt) {

            if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57)) {
                //alert("Enter Only Numeric Value ");
                // qNo = $('#ctl00_ContentPlaceHolder1_TabInterview').eq(i).closest('tr').find('td').eq(0).find('div').eq(j).text().trim();

                return false;
            }
        }


        function allowAlphaNumericSpace(e) {
            var code = ('charCode' in e) ? e.charCode : e.keyCode;
            if (!(code == 32) && // space
              !(code == 45) && // Dash
              !(code > 47 && code < 58) && // numeric (0-9)
              !(code > 64 && code < 91) && // upper alpha (A-Z)
              !(code > 96 && code < 123)) { // lower alpha (a-z)
                e.preventDefault();
            }
        }

        function setMaxLength(control) {
            debugger

            control.closest('td');
        }
    </script>

       <script>
           function SetStatActive(val) {
               $('#rdActive').prop('checked', val);
              
           }

           function validate() {

               $('#hfdShowStatus').val($('#rdActive').prop('checked'));
              
           }
            </script>
</asp:Content>


