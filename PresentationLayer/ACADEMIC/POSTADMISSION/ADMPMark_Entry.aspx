<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPMark_Entry.aspx.cs" Inherits="mark_entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>--%>

     <script type="text/javascript">
         function myFunction(_this) {
               debugger
             totalcolumns = $("#TabInterview tr th").length
             row = $(_this).closest("tr")
             var _value = 0;
             var cellvalue = 0;

             for (var i = 2; i < totalcolumns-1; i++) {
                 cellvalue = parseInt(row.find("td:eq(" + i + ") input[type='text']").val());
                 MaxMarks = parseInt(row.find("td:eq(" + i + ") input[type='hidden']").val());
                 if (cellvalue > MaxMarks)
                 {
                     alert("Marks Should Not be Greater  Than'" + MaxMarks + "'");
                     row.find("td:eq(" + i + ") input[type='text']").val('');
                 }
                 if (isNaN(cellvalue)) { cellvalue = 0; }
                 _value = parseInt(_value) + cellvalue;
             }
             row.find("td:eq(" + (totalcolumns-1) + ") input[type='text']").val(_value);
         }
    </script>
        <script type="text/javascript">
            $(document).ready(function () {

            });     

            function onDDLChange(id) {

               
                var BatchNo = $('#ctl00_ContentPlaceHolder1_ddlAdmBatch').find('option:selected').val();
                var BranchNo = $('#ctl00_ContentPlaceHolder1_ddlProgram').find('option:selected').val();

                //alert(BatchNo + " " + DegreeNo);

                bindListViewData(BatchNo, BranchNo);

            }

            function bindListViewData(BatchNo, BranchNo) {
                //alert(BatchNo + " " + DegreeNo);
              
                $.ajax({
                    type: 'POST',
                    dataType: 'text',
                    url: '<%=Page.ResolveUrl("ACDOnlinePostAdmission.asmx/GetDataForMarkEntry") %>',
                //data: '{BatchNo:' + BatchNo + ', DegreeNo:' + DegreeNo + '}',
                    data: { BatchNo: BatchNo, BranchNo: BranchNo },

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
                    var html = '<thead class="bg-light-blue"><tr><th> Application Id</th><th> Name </th>'

                    for (var i = 0; i < arr1.length; i++) {
                        //console.log(arr1[i]["PANELFOR_NAME"]);

                        html += '<th>' + arr1[i]["PANELFOR_NAME"] + '<input type="hidden" id="PANELFORID"  value = "' + arr1[i]["PANELFORID"] + '">' + '</th>'
                    }
                    //html += '</tr></thead>'
                    html += '<th> Total </th></tr></thead>'
                    $('#TabInterview').html(html);


                    // Table data Dynamic creation from databse
                    var totVal = 0;
                    var lastdata = '';
                    var alldatahtml = "<tbody>";

                    for (var j = 0; j < arr2.length; j++) {
                        if (lastdata != arr2[j]["APPLICATION_ID"]) {
                       
                            alldatahtml += '<tr><td>' + arr2[j]["APPLICATION_ID"] + '</td> <td>' + arr2[j]["FIRSTNAME"] + '<input type="hidden" id="hdnUserNo"  value = "' + arr2[j]["USERNO"] + '">' + '</td>  ';

                            for (var k = 0; k < arr2.length; k++) {

                                if (arr2[j]["APPLICATION_ID"] == arr2[k]["APPLICATION_ID"]) {
                                    //onkeyup="myFunction(this)"   onKeyUp = "setMaxLength(this)"  form-control//alldatahtml += '<td><input type="text" id="txtmarks" onkeypress="return functionx(event);" value = "' + arr2[j]["MAXMARKS"] + '" class = "form-control"  onKeyUp="setMaxLength(this)" isMaxLength="3" MaxLength="3" min="0"></td>';
                                    //alldatahtml += '<td><input type="text" id="txtmarks' + k + '" onkeypress="return functionx(event);" value = "' + arr2[j]["MAXMARKS"] + '" class = "form-control" onKeyUp = "setMaxLength(this)"  isMaxLength="3" MaxLength="3" min="0"></td>';
                                    //alldatahtml += '<td><input type="text" id="txtmarks' + k + '"  value = "' + arr2[k]["MAXMARKS"] + '" class ="quetext"  onkeyup="myFunction(this)"  isMaxLength="3" MaxLength="3" min="0"></td>';
                                    alldatahtml += '<td><input type="text" id="txtmarks' + k + '" onkeyup="myFunction(this)"   value = "' + arr2[k]["MARKS"] + '"  class ="quetext"   isMaxLength="3" MaxLength="3" min="0">' + '<input type="hidden" id="hdnPNLMaxMarks"  value = "' + arr2[k]["MAXMARKS"] + '">'
                                         + '<input type="hidden" id="hdnPnlforId"  value = "' + arr2[k]["PANELFORID"] + '">' + '</td>';
                                    totVal = totVal + arr2[k]["MARKS"];
                                }
                            }

                            alldatahtml += '<td><input type="text" id="txtTotal' + j + '" disabled="true" value="' + totVal + '" ></td>';
                            totVal = 0;
                            alldatahtml += '</tr>';
                        }
                        lastdata = arr2[j]["APPLICATION_ID"];

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
<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">MARK ENTRY</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Admission Batch</label>
                                </div>
                                <%--<asp:DropDownList ID="ddlAdmBatch" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>--%>

                                <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                           
                                        </asp:DropDownList>

                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Schedule</label>
                                </div>

                                   <asp:DropDownList ID="ddlSchedule" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddlSchedule_SelectedIndexChanged" AutoPostBack="true" >
                                       
                                        </asp:DropDownList>

                             <%--   <asp:DropDownList ID="ddlSchedule" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" >
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>--%>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Program</label>
                                </div>

                                <asp:DropDownList ID="ddlProgram" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1" onchange="onDDLChange(this);" >
                            
                                        </asp:DropDownList>

                             <%--    <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" onchange="onDDLChange(this);">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>--%>

                            </div>


                        </div>
                    </div>

                   <div class="col-12 btn-footer">
                        <%--<asp:Button ID="btnshow" runat="server" Text="Show" CssClass="btn btn-primary" />--%>
                     <%--   <button id="btnSave">Submit</button>--%>
                       <%-- <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" />--%>
                    </div>

                      <div id="divInter" class="col-12" runat="server">
                                <table class="table table-striped table-bordered nowrap display" style="width: 100%;" id="TabInterview" >
                                </table>

                                <div id="CalTR">

                                </div>

                                <%--<asp:Panel ID="pnlInterview" runat="server" Visible="false">
                                    <asp:ListView ID="lvInterview" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divInterview">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Program</th>
                                                        <th>Interview</th>
                                                        <th>GD</th>
                                                        <th>Test</th>
                                                        <th>Total</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <asp:UpdatePanel runat="server" ID="UpdateLV">
                                                <ContentTemplate>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:HiddenField ID="hfdBranchNo" runat="server" Value='<%# Eval("BRANCHNO") %>' />
                                                            <asp:Label ID="lblBranchname" runat="server" Text='<%# Eval("SHORTNAME") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField ID="hdfPanelForInter" runat="server" Value='<%# Eval("PANELFORID_INTERVIEW") %>' />
                                                            <asp:CheckBox ID="ChkInterMark" runat="server" CssClass="chkbox_addsubject" onchange="checkedActiveInt_Change(this);" Checked='<%# Convert.ToInt32( Eval("INTERVIEW_MAXMARKS"))>0?true:false %>' />
                                                            <asp:TextBox ID="txtInterMarks" runat="server" CssClass="form-control" Enabled='<%# Convert.ToInt32( Eval("INTERVIEW_MAXMARKS"))>0?true:false %>' onKeyUp="setMaxLength(this)" isMaxLength="3" MaxLength="3" min="0" 
                                                                Text='<%# Eval("INTERVIEW_MAXMARKS") %>' onkeypress="return functionx(event);if(this.value.length==3) return false;" ToolTip="Please Enter Interview Marks" type="number"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField ID="hdfPanelForGD" runat="server" Value='<%# Eval("PANELFORID_GD") %>' />
                                                            <asp:CheckBox ID="ChkGDMark" runat="server" CssClass="chkbox_addsubject" onchange="checkedActiveGD_Change(this);" Checked='<%# Convert.ToInt32( Eval("GD_MAXMARKS"))>0?true:false %>' />
                                                            <asp:TextBox ID="txtGDMarks" runat="server"  CssClass="form-control" Enabled='<%# Convert.ToInt32( Eval("GD_MAXMARKS"))>0?true:false %>' onKeyUp="setMaxLength(this)" isMaxLength="3" MaxLength="3" min="0" 
                                                                Text='<%# Eval("GD_MAXMARKS") %>' onkeypress="return functionx(event);if(this.value.length==3) return false;" ToolTip="Please Enter GD Marks" type="number"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField ID="hdfPanelForTest" runat="server" Value='<%# Eval("PANELFORID_Test") %>' />
                                                            <asp:CheckBox ID="ChkTestMark" runat="server" CssClass="chkbox_addsubject" onchange="checkedActiveTst_Change(this);" Checked='<%# Convert.ToInt32( Eval("Test_MAXMARKS"))>0?true:false %>' />
                                                            <asp:TextBox ID="txtTest" runat="server" CssClass="form-control" Enabled='<%# Convert.ToInt32( Eval("Test_MAXMARKS"))>0?true:false %>' onKeyUp="setMaxLength(this)" isMaxLength="3" MaxLength="3" min="0" 
                                                                Text='<%# Eval("Test_MAXMARKS") %>' onkeypress="return functionx(event);if(this.value.length==3) return false;" ToolTip="Please Enter GD Marks" type="number"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblTotalMarks" runat="server" Text='<%# Convert.ToInt32( Eval("INTERVIEW_MAXMARKS"))+Convert.ToInt32( Eval("GD_MAXMARKS"))+Convert.ToInt32( Eval("Test_MAXMARKS")) %>' />
                                                        </td>
                                                    </tr>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>--%>
                            </div>

                     <div class="col-12 btn-footer">
                                <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" />--%>
                                <button id="btnSubmit" class="btn btn-outline-info btnX">Submit</button>
                                <%--ValidationGroup="InterviewTest" OnClick="btnSubmit_Click"--%>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" />
                                <%--OnClick="btnCancel_Click"--%>

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="InterviewTest"
                                    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                            </div>
                </div>
            </div>
        </div>
    </div>
           <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>

        <script type="text/javascript">
            $("#btnSubmit").click(function () {
                debugger
                var BatchNo = $('#ctl00_ContentPlaceHolder1_ddlAdmBatch').find('option:selected').val();
                var BranchNo = $('#ctl00_ContentPlaceHolder1_ddlProgram').find('option:selected').val();

                var ScheduleNo = $('#ctl00_ContentPlaceHolder1_ddlSchedule').find('option:selected').val();


                var totalcolumns = $("#TabInterview tr th").length


                var marks = new Array();
                var coverageid = 0;


                $("#TabInterview tbody tr").each(function () {

                    for (var i = 1; i <= totalcolumns-2 ; i++) {
                        debugger
                        var row = $(this);
                        var mark = {};

                        mark.UserNo = row.find("TD").eq(1).find('[type="hidden"]').val();
                        mark.PANELFORID = row.find("TD").eq(i+1).find('[id="hdnPnlforId"]').val();
                      
                        mark.EnteredMark= parseInt(row.find("td:eq(" + (i+1)+ ") input[type='text']").val());

                        mark.BATCHNO = BatchNo;
                        mark.BRANCHNO = BranchNo;
                        mark.ScheduleNo = ScheduleNo;

                   
                        marks.push(mark);

                    }
                });

                $.ajax({

                    url: '<%= ResolveUrl("ACDOnlinePostAdmission.asmx/SavePanelMarkEntry") %>',
                data: "{marks:'" + JSON.stringify(marks) + "'}",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (d) {

                    var data = JSON.stringify(d.d).replace('"', "").replace('"', "");
                    //alert(data);
                    if (data == "2") {

                        //var BatchNo = $('#ctl00_ContentPlaceHolder1_ddlAdmBatch').find('option:selected').val();
                        //var BranchNo = $('#ctl00_ContentPlaceHolder1_ddlProgram').find('option:selected').val();

 
                        window.location.reload();
                        alert('Cource coverage Topic already exists for Date ');

                    }
                    else if (data == "3") {

                        window.location.reload();
                        alert('Mark update successfully.');

                    }
                    else if (data == "1") {
                        window.location.reload();
                        alert('Marked saved successfully.');

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

<%--    <script type="text/javascript">
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
    </script>--%>

   
</asp:Content>

