<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="OnlineDegreeSubjectMapping.aspx.cs" Inherits="ACADEMIC_OnlineDegreeSubjectMapping" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:HiddenField ID="hfdiscompulsory" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdiscutoff" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdisothers" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <div>
        <asp:UpdateProgress ID="UpdateProgress4" runat="server"
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
    <asp:UpdatePanel ID="uponlineMapping" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                    </div>--%>
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
                                                <label>Program Type</label>
                                                <asp:Label ID="lblprogramtype" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlprogramt" runat="server" TabIndex="1" AppendDataBoundItems="True" ToolTip="Please Select Program Type" OnSelectedIndexChanged="ddlprogramt_SelectedIndexChanged"
                                                CssClass="form-control" data-select2-enable="true" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvtype" runat="server" ControlToValidate="ddlprogramt" Display="None"
                                                ErrorMessage="Please Select Program Type" ValidationGroup="submit" SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Degree</label>
                                                <asp:Label ID="lblddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="2" AppendDataBoundItems="True" ToolTip="Please Select Degree"
                                                CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" Display="None"
                                                ErrorMessage="Please Select Degree" ValidationGroup="submit" SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-2 col-md-6 col-12" style="margin-top: 22px">
                                            <div class="label-dynamic">
                                                <%--<sup>* </sup>
                                                <label>Specilization</label>--%>
                                                <asp:CheckBox ID="chkSpec" runat="server" ToolTip="Check for specialization." Text="&nbsp;Specilization" TabIndex="1" onchange="checkSpec(this);" />
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divBranch" runat="server" style="display: none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Branch</label>
                                                <asp:DropDownList ID="ddlBranch" runat="server" ToolTip="Please select branch." TabIndex="1" AppendDataBoundItems="true"
                                                    CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Subject</label>
                                                <%-- <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlsubject" runat="server" TabIndex="3" AppendDataBoundItems="True" ToolTip="Please Select Subject" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Subject 1</asp:ListItem>
                                                <asp:ListItem Value="2">Subject 2</asp:ListItem>
                                                <asp:ListItem Value="3">Subject 3</asp:ListItem>
                                                <asp:ListItem Value="4">Subject 4</asp:ListItem>
                                                <asp:ListItem Value="5">Subject 5</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvsubject" runat="server" ControlToValidate="ddlsubject" Display="None"
                                                ErrorMessage="Please Select Subject" ValidationGroup="submit" SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Subject Name</label>
                                            </div>
                                            <asp:TextBox ID="txtSubName" runat="server" ToolTip="Please enter subject name." TabIndex="1" AutoComplete="off" MaxLength="128" CssClass="form-control">
                                            </asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtAjax" runat="server" TargetControlID="txtSubName" FilterMode="InvalidChars"
                                                InvalidChars="~`!@#$%^&*()_+={[}]|\:;<,>?'">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <div class="form-group col-lg-2 col-md-6 col-6">
                                            <div class="label-dynamic">
                                                <%--<sup>* </sup>--%>
                                                <label>IsCompulsory</label>
                                            </div>
                                            <div class="switch form-inline">
                                                <input type="checkbox" id="rdiscompulsoryYes" name="switch" checked />
                                                <label data-on="Yes" data-off="No" for="rdiscompulsoryYes"></label>
                                            </div>

                                        </div>

                                        <div class="form-group col-lg-2 col-md-6 col-6">
                                            <div class="label-dynamic">
                                                <%--<sup>* </sup>--%>
                                                <label>IsCutOff</label>
                                            </div>
                                            <div class="switch form-inline">
                                                <input type="checkbox" id="rdiscutoffYes" name="switch" checked />
                                                <label data-on="Yes" data-off="No" for="rdiscutoffYes"></label>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-2 col-md-6 col-6">
                                            <div class="label-dynamic">
                                                <%--<sup>* </sup>--%>
                                                <label>IsOthers</label>
                                            </div>
                                            <div class="switch form-inline">
                                                <input type="checkbox" id="rdisothersYes" name="switch" checked />
                                                <label data-on="Yes" data-off="No" for="rdisothersYes"></label>
                                            </div>
                                        </div>
                                        <%--<div class="form-group col-lg-2 col-md-6 col-6">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>IsOthers</label>
                                    </div>
                                    <div class="switch form-inline">
                                        <input type="checkbox" id="rdisothersYes" name="switch" checked />
                                        <label data-on="Yes" data-off="No" for="rdisothersYes"></label>
                                    </div>
                                </div>--%>
                                        <div class="form-group col-lg-2 col-md-6 col-6">
                                            <div class="label-dynamic">
                                                <%--<sup>* </sup>--%>
                                                <label>IsActive</label>
                                            </div>
                                            <div class="switch form-inline">
                                                <input type="checkbox" id="rdActive" name="switch" checked />
                                                <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" TabIndex="8" CausesValidation="false" OnClientClick="return validate();" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="9" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                        ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                </div>


                                <div class="col-12">
                                   <%-- <div runat="server" visible="false" class="row">
                                        <div class="col-lg-9 col-md-9 col-sm-9 col-12 d-flex mb-0 pb-0 mt-3">
                                            <div class="form-group d-flex mb-0 pb-0">
                                                <label class="mt-1 mr-1">Show</label>
                                                <asp:DropDownList ID="NumberDropDown" runat="server" CssClass="custom-dropdown" AutoPostBack="true" OnSelectedIndexChanged="NumberDropDown_SelectedIndexChanged">
                                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                    <asp:ListItem Text="200" Value="200"></asp:ListItem>
                                                    <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                                    <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                                                    <asp:ListItem Text="1500" Value="1500"></asp:ListItem>
                                                    <asp:ListItem Text="2000" Value="2000"></asp:ListItem>
                                                     <asp:ListItem Text="3000" Value="3000"></asp:ListItem>
                                                     <asp:ListItem Text="4000" Value="4000"></asp:ListItem>
                                                </asp:DropDownList>
                                                <label class="mt-1 ml-1">Entries</label>
                                            </div>
                                        </div>
                                        <div></div>
                                        <div></div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 col-12 d-none">
                                            <div class="form-group" style="text-align: right;">
                                                <label for="FilterData"></label>
                                                <input type="text" id="FilterData" class="form-control sfilter" placeholder="Search" />
                                            </div>
                                        </div>OnPagePropertiesChanging="lvBulkDetail_PagePropertiesChanging"
                                    </div>--%>
                                    <asp:ListView ID="lvonlinemapping" runat="server" >
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>Online Degree subject Mapping List</h5>
                                                </div>
                                                <%-- <div class="sub-heading">
                                                       <h5>Online Degree subject Mapping List</h5>
                                                        </div>--%>

                                                <table class="table table-striped table-bordered nowrap display2" style="width: 100%" id="tab-le">
                                                    <%--  <table class="table table-striped table-bordered nowrap" style="width: 100% !important" id="tab-le">--%>
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th style="text-align: center;">Edit
                                                            </th>
                                                            <th>Degree</th>
                                                            <th>Branch</th>
                                                            <th>Subject </th>
                                                            <th>Subject Name</th>
                                                            <th>Compulsory</th>
                                                            <th>CutOff</th>
                                                            <th>Others</th>
                                                            <th>Active Status</th>
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" CommandArgument='<%# Eval("SUB_ID")%>' ImageUrl="~/Images/edit.png" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>
                                                <%-- <td><%#Eval("SESSION_PNAME") %></td>--%>
                                                <td><%#Eval("DEGREE")%></td>
                                                <td><%#Eval("BRANCHNAME")%></td>
                                                <td><%#Eval("SUBJECT")%></td>
                                                <td><%#Eval("SUB_NAME")%></td>
                                                <td>
                                                    <asp:Label ID="lblComp" runat="server" Text='<%#Eval("IS_COMPULSORY")%>' ForeColor='<%#Eval("IS_COMPULSORY").ToString().Equals("YES") ? System.Drawing.Color.Green : System.Drawing.Color.Red%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblCut" runat="server" Text='<%#Eval("IS_CUTOFF")%>' ForeColor='<%#Eval("IS_CUTOFF").ToString().Equals("YES") ? System.Drawing.Color.Green : System.Drawing.Color.Red%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblOther" runat="server" Text='<%#Eval("IS_OTHERS")%>' ForeColor='<%#Eval("IS_OTHERS").ToString().Equals("YES") ? System.Drawing.Color.Green : System.Drawing.Color.Red%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblActive" runat="server" Text='<%#Eval("ACTIVESTATUS")%>' ForeColor='<%#Eval("ACTIVESTATUS").ToString().Equals("Active") ? System.Drawing.Color.Green : System.Drawing.Color.Red%>'></asp:Label></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                 <%--   <div runat="server" visible="false" style="text-align: left; margin-top: 0px;">
                                        <asp:DataPager ID="DataPager2" runat="server" PagedControlID="lvonlinemapping" PageSize="50">
                                            <Fields>
                                                <asp:TemplatePagerField>
                                                    <PagerTemplate>
                                                        <b>Showing
                                                                <asp:Label runat="server" ID="CurrentPageLabel"
                                                                    Text="<%# Container.StartRowIndex+1 %>" />
                                                            to
                                                                <asp:Label runat="server" ID="TotalPagesLabel"
                                                                    Text="<%# Convert.ToInt32(Container.StartRowIndex + Container.PageSize) > Convert.ToInt32(Container.TotalRowCount) ? Convert.ToInt32(Container.TotalRowCount):Convert.ToInt32(Container.StartRowIndex+ Container.PageSize) %>" />
                                                            ( of
                                                                <asp:Label runat="server" ID="TotalItemsLabel"
                                                                    Text="<%# Container.TotalRowCount%>" />
                                                            records)
                                                                <br />
                                                        </b>
                                                    </PagerTemplate>
                                                </asp:TemplatePagerField>
                                            </Fields>
                                        </asp:DataPager>
                                    </div>
                                    <div style="text-align: right; margin-top: 0px;">
                                        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvonlinemapping" PageSize="1000">
                                            <Fields>
                                                <asp:NumericPagerField />
                                            </Fields>
                                        </asp:DataPager>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        function SetStatcompulsory(val) {
            $('[id*=rdiscompulsoryYes]').prop('checked', val);
        }
        function SetStatcutoff(val) {
            $('[id*=rdiscutoffYes]').prop('checked', val);
        }
        function SetStatothers(val) {
            $('#rdisothersYes').prop('checked', val);
        }

        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }

        function validate() {
            //var school = document.getElementById("ctl00_ContentPlaceHolder1_ddlSchoolInstitute").value;
            var Programtype = document.getElementById("ctl00_ContentPlaceHolder1_ddlprogramt").value;
            var degree = document.getElementById("ctl00_ContentPlaceHolder1_ddlDegree").value;
            var subject = document.getElementById("ctl00_ContentPlaceHolder1_ddlsubject").value;
            var txtSub = document.getElementById('<%=txtSubName.ClientID%>').value;
            var branch = document.getElementById('<%=ddlBranch.ClientID%>').value;
            var check = document.getElementById('<%=chkSpec.ClientID%>');
            if (Programtype == "0") {
                alert("Please Select Program Type.");
                return false;
            }
            if (degree == "0") {
                alert("Please Select Degree.");
                return false;
            }
            if (check.checked == true) {
                if (branch == "0") {
                    alert("Please Select Branch.");
                    return false;
                }
            }
            if (subject == "0") {
                alert("Please Select Subject Type.");
                return false;
            }
            if (txtSub == "") {
                alert("Please Enter Subject Name.");
                return false;
            }


            $('#hfdiscompulsory').val($('#rdiscompulsoryYes').prop('checked'));
            $('#hfdiscutoff').val($('#rdiscutoffYes').prop('checked'));
            $('#hfdisothers').val($('#rdisothersYes').prop('checked'));
            $('#hfdActive').val($('#rdActive').prop('checked'));
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });


        function checkSpec() {
            var chk = document.getElementById('<%=chkSpec.ClientID%>');
            var degree = document.getElementById('<%=ddlDegree.ClientID%>');
            var progType = document.getElementById('<%=ddlprogramt.ClientID%>');
            if (chk.checked == true) {
                if (degree.value > 0) {
                    document.getElementById('<%=divBranch.ClientID%>').style.display = "block";

                }
                else {
                    document.getElementById('<%=divBranch.ClientID%>').style.display = "none";
                    document.getElementById('<%=chkSpec.ClientID%>').checked = false;
                    if (progType.value == 0) {
                        progType.focus();
                        return alert('Please Select Program Type.');
                    }
                    else {
                        degree.focus();
                        return alert('Please Select Degree.');
                    }
                }
            }
            else {
                document.getElementById('<%=divBranch.ClientID%>').style.display = "none";

            }
        }
    </script>
   <script type="text/javascript">
        function Datatable()
        {
            $(document).ready(function () {
                var table = $('.display2').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: true,
                    //lengthMenu: [
                    // [500, 1000, 2000, 3000, 4000, 5000, 10000],
                    // [500 + " - " + 1000, 1000 + " - " + 2000, 2000 + " - " + 3000, 3000 + " - " + 4000, 4000 + " - " + 5000, 5000 + " - " + 10000, "All"]
                    //],
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
                                    return $('.display2').DataTable().column(idx).visible();
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
                                                return $('.display2').DataTable().column(idx).visible();
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
                                                        nodereturn += $(this).text();
                                                    });
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).text();
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
                                                return $('.display2').DataTable().column(idx).visible();
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
                                                        nodereturn += $(this).text();
                                                    });
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).text();
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
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var table = $('.display2').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: true,
                //lengthMenu: [
                // [500, 1000, 2000, 3000, 4000, 5000, 10000],
                // [500 + " - " + 1000, 1000 + " - " + 2000, 2000 + " - " + 3000, 3000 + " - " + 4000, 4000 + " - " + 5000, 5000 + " - " + 10000, "All"]
                //],
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
                                return $('.display2').DataTable().column(idx).visible();
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
                                            return $('.display2').DataTable().column(idx).visible();
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
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
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
                                            return $('.display2').DataTable().column(idx).visible();
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
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
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
    </script>
</asp:Content>
