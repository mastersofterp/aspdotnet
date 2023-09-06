<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="BulkNoDuesFormGeneration.aspx.cs"
    Inherits="ACADEMIC_BulkNoDuesFormGeneration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updtime"
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

    <asp:UpdatePanel ID="updtime" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Bulk No Dues Form-Generation</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Admission Batch</label>--%>
                                            <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmbatch" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" ToolTip="Please Select Admbatch"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Admbatch" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Institute Name</label>--%>
                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="True" TabIndex="1" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged"
                                            CssClass="form-control" data-select2-enable="true" AutoPostBack="True" ToolTip="Please Select Institute">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvColg" runat="server" ControlToValidate="ddlColg"
                                            Display="None" ErrorMessage="Please Select Institute Name" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Degree</label>--%>
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" ToolTip="Please Select Degree" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Branch</label>--%>
                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" TabIndex="4" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ToolTip="Please Select Branch" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlYear" runat="server" TabIndex="5" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select Admission Year" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" />
                                        <%--  <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                                                            Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Admission Year"
                                                            ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Semester" TabIndex="6"
                                            CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:RadioButtonList ID="rbRegEx" runat="server" RepeatDirection="Horizontal" TabIndex="777" AutoPostBack="true" OnSelectedIndexChanged="rbRegEx_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Selected="True">&nbsp;Regular Student &nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="1">&nbsp;Ex Student</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Student" OnClick="btnShow_Click" TabIndex="7"
                                    ToolTip="Shows Students under Selected Criteria." ValidationGroup="show" CssClass="btn btn-primary" />

                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="8"
                                    ValidationGroup="show" CssClass="btn btn-primary" CausesValidation="false" Visible="false" />

                                <asp:Button ID="btnPrintReport" runat="server" Text="Print No-Dues Form" TabIndex="999" CssClass="btn btn-info"
                                    OnClick="btnPrintReport_Click" ToolTip="Print Card under Selected Criteria." ValidationGroup="show" />

                                <asp:Button ID="btnLock" runat="server" Text="Lock" OnClick="btnLock_Click" TabIndex="9" Visible="false"
                                    ValidationGroup="show" CssClass="btn btn-primary" />

                                <asp:Button ID="btnUnlock" runat="server" Text="Unlock" OnClick="btnUnlock_Click" TabIndex="10" Visible="false"
                                    ValidationGroup="show" CssClass="btn btn-primary" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="11"
                                    ToolTip="Cancel Selected under Selected Criteria." CssClass="btn btn-warning" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                <div class="label-dynamic">
                                    <label>Total Selected</label>
                                </div>
                                <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False" CssClass="form-control"
                                    Style="text-align: center;" Font-Bold="True" Font-Size="Small" Visible="false"></asp:TextBox>
                                <%--  Reset the sample so it can be played again --%>
                                <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtTotStud"
                                    WatermarkText="0" WatermarkCssClass="watermarked" Enabled="True" />
                                <asp:HiddenField ID="hftot" runat="server" />
                            </div>

                            <div class="vista-grid_datapager col-12">
                                <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvStudentRecords" PageSize="100"
                                    OnPreRender="dpPager_PreRender">
                                    <Fields>
                                        <asp:NextPreviousPagerField FirstPageText="First" PreviousPageText="Prev" ButtonType="Link"
                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                            ShowLastPageButton="false" ShowNextPageButton="false" />
                                        <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                        <asp:NextPreviousPagerField LastPageText="Next" NextPageText="Last" ButtonType="Link"
                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                            ShowLastPageButton="true" ShowNextPageButton="true" />
                                    </Fields>
                                </asp:DataPager>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvStudentRecords" runat="server">
                                        <LayoutTemplate>
                                            <div id="listViewGrid">
                                                <div class="sub-heading">
                                                    <h5>Student List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblStudent">
                                                    <thead class="bg-light-blue">
                                                        <tr id="Tr1">
                                                            <th>
                                                                <asp:CheckBox ID="chkIdentityCard" runat="server" OnClick="checkAllCheckbox(this);" ToolTip="Select or Deselect All Records" />
                                                            </th>
                                                            <th>Reg. No.
                                                            </th>
                                                            <th>Roll No.</th>
                                                            <th>Student Name
                                                            </th>
                                                            <th>Semester
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkreport" runat="server" onclick="totsubjects(this);" ToolTip='<%# Eval("idno") %>' />
                                                    <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("REGNO")%>
                                                </td>
                                                <td><%# Eval("ROLLNO")%></td>
                                                <td>
                                                    <%# Eval("STUDNAME")%>
                                                    <asp:HiddenField ID="hdfAppliid" runat="server" Value='<%# Eval("STUDNAME") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTERNAME")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="show" DisplayMode="List" />
                            <div id="divMsg" runat="server">
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvStudentRecords$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvStudentRecords$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        if (headchk.checked == false)
                            e.checked = false;
                }
            }
        }

    </script>
    <script type="text/javascript">
        function SelectAll(chk) {
            debugger;
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
              var hftot = document.getElementById('<%= hftot.ClientID %>');
              for (i = 0; i < hftot.value; i++) {
                  var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_chkReport');
                  if (lst.type == 'checkbox') {
                      if (chk.checked == true) {
                          lst.checked = true;
                          txtTot.value = hftot.value;
                      }
                      else {

                          lst.checked = false;
                          txtTot.value = 0;
                      }
                  }

              }
          }
          function totSubjects(chk) {

              var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

              if (chk.checked == true)
                  txtTot.value = Number(txtTot.value) + 1;
              else
                  txtTot.value = Number(txtTot.value) - 1;

          }
    </script>
    <%--<script type="text/javascript" language="javascript">
         $(function () {
             $("[id*=tvLinks] input[type=checkbox]").bind("click", function () {
                 var table = $(this).closest("table");
                 if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                     //Is Parent CheckBox
                     var childDiv = table.next();
                     var isChecked = $(this).is(":checked");
                     $("input[type=checkbox]", childDiv).each(function () {
                         if (isChecked) {

                             $(this).attr("checked", "checked");
                         } else {

                             $(this).removeAttr("checked");
                         }
                     });
                 } else {
                     //Is Child CheckBox
                     var parentDIV = $(this).closest("DIV");
                     if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {



                         $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                     } else {

                         ////                   
                         if (($("input[type=checkbox]:checked", parentDIV).length == 0)) {

                             $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                         }
                         else {
                             $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                         }
                     }
                 }
             });
         })

    </script>--%>


    <%--<script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#tblStudent').DataTable({
                //scrollX: 'true'
                //"pageLength": 10
            });
        }

    </script>--%>
</asp:Content>
