<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPAdhocReports.aspx.cs" Inherits="ACADEMIC_POSTADMISSION_ADMPAdhocReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <script type="text/javascript">
        $(document).ready(function () {
            debugger
            //var display = $("#btnDisplay").css("display");
            //if(display!="none")
            //{
            //    $("#btnDisplay").attr("style", "display:none");
            //}

            //$("#ctl00_ContentPlaceHolder1_btnDisplay").attr("style", "display:none");
            $("#divDispTable").attr("style", "display:none");
        });
    </script>--%>

    <script>
        function DisplayTable() {
            GenerateAllReports();
            $('#divDispTable').show();
        }
    </script>

    <style>
       table.dataTable.nowrap td {
            white-space: break-spaces;
        }
    </style>

    <script type="text/javascript">

        function GenerateAllReports() {

            debugger
            //var AdmBatch = $('#' + '<%=ddlAdmissionBatch.ClientID%>').val();           

            //var BatchNo = "10";


            //var Reportname = $("#ddlReportName option:selected").text();
            //var Reportname = $('#' + '<%=ddlReportName.ClientID %>').find(":selected").text()

            //var ADHOCID = $('#' + '<%=ddlReportName.ClientID %>').val();.

            var ControllerName1 = "";
            var Cnrtl1Value = "";

            var ControllerName2 = "";
            var Cnrtl2Value = "";

            var ControllerName3 = "";
            var Cnrtl3Value = "";

            var ControllerName4 = "";
            var Cnrtl4Value = "";

            var ControllerName5 = "";
            var Cnrtl5Value = "";


            ControllerName1 = $('#' + '<%=ddlReportName.ClientID %>').attr('id');
            Cnrtl1Value = $('#' + '<%=ddlReportName.ClientID%>').val();


            ControllerName2 = $('#' + '<%=ddlAdmissionBatch.ClientID %>').attr('id');
            Cnrtl2Value = $('#' + '<%=ddlAdmissionBatch.ClientID%>').val();

            ControllerName3 = $('#' + '<%=ddlADMPtype.ClientID %>').attr('id');
            Cnrtl3Value = $('#' + '<%=ddlADMPtype.ClientID%>').val();

            ControllerName4 = $('#' + '<%=ddlDegree.ClientID %>').attr('id');
            Cnrtl4Value = $('#' + '<%=ddlDegree.ClientID%>').val();

            ControllerName5 = $('#' + '<%=ddlBranch.ClientID %>').attr('id');
            Cnrtl5Value = $('#' + '<%=ddlBranch.ClientID%>').val();


            if (ControllerName1 === undefined || ControllerName1 === null) {
                ControllerName1 = "";
            }
            if (ControllerName2 === undefined || ControllerName2 === null) {
                ControllerName2 = "";
            }
            if (ControllerName3 === undefined || ControllerName3 === null) {

                ControllerName3 = "";
            }
            if (ControllerName4 === undefined || ControllerName4 === null) {
                ControllerName4 = "";
            }
            if (ControllerName5 === undefined || ControllerName5 === null) {
                ControllerName5 = "";
            }

            if (Cnrtl1Value === undefined || Cnrtl1Value === null) {
                Cnrtl1Value = "0";
            }
            if (Cnrtl2Value === undefined || Cnrtl2Value === null) {
                Cnrtl2Value = "0";
            }
            if (Cnrtl3Value === undefined || Cnrtl3Value === null) {

                Cnrtl3Value = "0";
            }
            if (Cnrtl4Value === undefined || Cnrtl4Value === null) {
                Cnrtl4Value = "0";
            }
            if (Cnrtl5Value === undefined || Cnrtl5Value === null) {
                Cnrtl5Value = "0";
            }

            $.ajax(
            {

                type: 'POST',
                dataType: 'text',
                url: '<%= ResolveUrl("ACDOnlinePostAdmission.asmx/GenerateAllReports") %>',
                //data: '{BatchNo:' + BatchNo + ', DegreeNo:' + DegreeNo + '}',
                data: {
                    ControllerName1: ControllerName1, ControllerName2: ControllerName2, ControllerName3: ControllerName3, ControllerName4: ControllerName4, ControllerName5: ControllerName5,
                    Cnrtl1Value: Cnrtl1Value, Cnrtl2Value: Cnrtl2Value, Cnrtl3Value: Cnrtl3Value, Cnrtl4Value: Cnrtl4Value, Cnrtl5Value: Cnrtl5Value
                },

                success: function (data) {
                    debugger
                    //alert(data);
                    var arr = JSON.parse(data);
                    var arr1, arr2, arr3;
                    // Loop throught the array.
                    for (var i = 0; i < arr.length; i++) {
                        console.log(arr[i]);
                        if (i == 0) {
                            arr1 = arr[i];
                        } else if (i == 1) {
                            arr2 = arr[i]
                        }
                        //else if (i == 2) {
                        //    arr3 = arr[i]
                        //}
                    }


                    var Active = "";
                    var Display = "";
                    if (arr1.length > 0) {
                        //for (var k = 0; k < arr3.length; k++) {
                        //    Active = arr3[k]["Active"];
                        //    Display = arr3[k]["Display"];
                        //}

                        //if (Active == true && Display == true) {

                        // $('#btnDisplay').show();
                        // Table header Dynamic creation from databse
                        var html = '<thead class="bg-light-blue"  style="position: sticky; z-index:1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;"><tr>'

                        for (var i = 0; i < arr2.length; i++) {
                            //console.log(arr1[i]["PANELFOR_NAME"]);

                            html += '<th>' + arr2[i]["HeadName"] + '</th>'
                        }
                        html += '</tr></thead>'
                        $('#TabInterview').html(html);


                        // Table data Dynamic creation from databse
                        var totVal = 0;
                        var lastdata = '';
                        var alldatahtml = "<tbody>";



                        for (var j = 0; j < arr1.length; j++) {
                            alldatahtml += '<tr>'
                            for (var i = 0; i < arr2.length; i++) {

                                var value = "";
                                if (arr1[j][arr2[i]["HeadName"]] == null) {
                                    var value = "";
                                }
                                else {
                                    var value = arr1[j][arr2[i]["HeadName"]];
                                }
                                //alldatahtml += '<td>' + arr1[j][arr2[i]["HeadName"]] + '</td>';

                                alldatahtml += '<td>' + value + '</td>';
                            }

                            alldatahtml += '</tr>';
                        }

                        //for (var j = 0; j < arr2.length; j++) {
                        //    if (lastdata != arr2[j]["BRANCHNO"]) {

                        //        alldatahtml += '<tr><td>' + arr2[j]["SHORTNAME"] + '<input type="hidden" id="hdnBranch"  value = "' + arr2[j]["BRANCHNO"] + '">' + '</td>';

                        //        for (var k = 0; k < arr2.length; k++) {

                        //            if (arr2[j]["BRANCHNO"] == arr2[k]["BRANCHNO"]) {
                        //                alldatahtml += '<td><input type="text" id="txtmarks' + k + '"  value = "' + arr2[k]["MAXMARKS"] + '" class ="quetext"  onkeyup="myFunction(this)"  isMaxLength="3" MaxLength="3" min="0"></td>';
                        //                totVal = totVal + arr2[k]["MAXMARKS"];
                        //            }
                        //        }

                        //        alldatahtml += '<td><input type="text" id="txtTotal' + j + '" disabled="true" value="' + totVal + '" ></td>';
                        //        totVal = 0;
                        //        alldatahtml += '</tr>';
                        //    }
                        //    lastdata = arr2[j]["BRANCHNO"];

                        //}

                        alldatahtml += '</tbody>';
                        $('#TabInterview').append(alldatahtml);
                         $('#TabInterview').dataTable();
                    }
                    else {

                        $('#TabInterview').empty();
                        $('#divDispTable').hide();
                        alert('No Record Found');
                    }

                },

                failure: function (response) {
                    debugger
                    alert("failure");
                },
                error: function (response) {
                    debugger
                    alert("error");
                    alert(response.responseText);
                }
            });

        }
    </script>
    <%--<asp:UpdatePanel ID="updSession" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Report Generation</h3>
                    <%--<h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                            </h3>--%>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-5 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Report Name</label>
                                </div>
                                <asp:DropDownList ID="ddlReportName" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlReportName_SelectedIndexChanged">
                                    <%--OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged"--%>
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlReportName" runat="server" ControlToValidate="ddlReportName"
                                    Display="None" ValidationGroup="Report" InitialValue="0"
                                    ErrorMessage="Please Select Report Name"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12" id="divADMPBatch" visible="false" runat="server">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Admission Batch</label>
                                </div>
                                <asp:DropDownList ID="ddlAdmissionBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                    <%--OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged"--%>
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="rfvddlAdmissionBatch" runat="server" ControlToValidate="ddlAdmissionBatch"
                                            Display="None" ValidationGroup="Report" InitialValue="0"
                                            ErrorMessage="Please Select Admission Batch"></asp:RequiredFieldValidator>--%>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12" id="divADMPtype" visible="false" runat="server">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Admission Type</label>
                                </div>
                                <asp:DropDownList ID="ddlADMPtype" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlADMPtype_SelectedIndexChanged">
                                    <%--OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged"--%>
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="rfvddlADMPtype" runat="server" ControlToValidate="ddlADMPtype"
                                            Display="None" ValidationGroup="Rport" InitialValue="0"
                                            ErrorMessage="Please Select Admission Type"></asp:RequiredFieldValidator>--%>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12" id="divDegree" visible="false" runat="server">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Degree</label>
                                </div>
                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                    <%--OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"--%>
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ValidationGroup="indextest" InitialValue="0"
                                            ErrorMessage="Please Select Degree"></asp:RequiredFieldValidator>--%>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12" id="divBranch" visible="false" runat="server">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Branch</label>
                                </div>
                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                    <%--OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"--%>
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ValidationGroup="Report" InitialValue="0"
                                            ErrorMessage="Please Select Branch"></asp:RequiredFieldValidator>--%>
                            </div>

                        </div>
                    </div>



                    <div class="col-12 btn-footer">
                        <button id="btnDisplay" value="Display" type="button" onclick="DisplayTable()" runat="server" visible="false" class="btn btn-primary">Display</button>
                        <%--    <asp:Button ID="btnDisplay" runat="server" Visible="false" Text="Display" ValidationGroup="Report" TabIndex="1" CssClass="btn btn-primary" OnClientClick="DisplayTable()"/>--%>

                        <asp:Button ID="btnReport" runat="server" Text="Excel Report" ValidationGroup="Report" TabIndex="1" CssClass="btn btn-primary" OnClick="btnReport_Click" />
                        <%--OnClick="btnShow_Click"--%>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="1" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                        <%--OnClick="btnCancel_Click"--%>
                        <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="Report"
                            DisplayMode="List" ShowMessageBox="True" ShowSummary="false" />
                    </div>




                    <div id="divDispTable" class="col-12 mt-4" style="display:none">
                        <div class="table-responsive" style="height: 300px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                            <table class="table table-striped table-bordered nowrap display" style="width: 100%;" id="TabInterview">
                            </table>

                        </div>

                    </div>



                </div>
            </div>
        </div>
        </div>
        <%--</ContentTemplate>--%>
        <%--<Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlAdmBatch" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlDegree" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlBranch" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="rdoConfigType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="rdoAdvType" EventName="SelectedIndexChanged" />

            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>--%>
        <%--</asp:UpdatePanel>--%>
</asp:Content>


