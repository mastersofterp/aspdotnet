<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="OfferedCourseNew.aspx.cs" Inherits="ACADEMIC_OfferedCourseNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <style>
        #ctl00_ContentPlaceHolder1_pnlCourse .form-control {
            padding: 0.15rem 0.15rem;
        }
    </style>

  
   <%-- <style>
        #ctl00_ContentPlaceHolder1_pnlCourse .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>--%>
   
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl"
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

    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">OFFERED COURSE</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1"  CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True"
                                            ToolTip="Please Select Session Long Name">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session Long Name" InitialValue="0"
                                            ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div1" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>School</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" TabIndex="2"  CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True"
                                            ToolTip="Please Select Faculty/School Name" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select Faculty/School Name" InitialValue="0"
                                            ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divDegree" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="3"  CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true"
                                            ToolTip="Please Select Degree">
                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0"
                                            ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" TabIndex="4"  CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged1"
                                            ToolTip="Please Select Regulation">
                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select Regulation" InitialValue="0"
                                            ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divBranch" runat="server">
                                        <div class="label-dynamic">
                                           <%-- <sup>* </sup>--%>
                                            <label>Inter Department</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDept" runat="server" TabIndex="5"  CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" AutoPostBack="True"
                                            ToolTip="Please Select Department" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDept"
                                            Display="None" ErrorMessage="Please Select Department" InitialValue="0" />--%>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" OnClick="btnSave_Click" TabIndex="6" ValidationGroup="submit" Visible="false" OnClientClick="return getVal();" class="btn btn-primary" />
                                    <asp:Button ID="btnCancel" class="btn btn-warning" TabIndex="7"
                                        runat="server" Text="Cancel" CausesValidation="False" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    <asp:HiddenField ID="hdfsem" runat="server" />
                                </div>

                                 <div class="col-md-12">
                                <asp:Panel ID="pnlCourse" runat="server" >
                                    <asp:ListView ID="lvCourse" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Course Offered List</h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap Display" id="mytable" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <%-- <td>Sr No.</td>--%>
                                                        <th> Code - Course Name </th>
                                                         <th>SubName</th>
                                                        <th style="text-align: center">Credits</th>
                                                        <th style="text-align: center">Capacity</th>
                                                        <th style="text-align: center">Core/Elective</th>
                                                        <th style="text-align: center;display:none">Course Group</th>
                                                        <th style="text-align: center">Offered</th>
                                                        <th style="text-align: center">Semester</th>
                                                        <th style="text-align: center">CA </th>
                                                        <th style="text-align: center">Final</th>
                                                        <th style="text-align: center">CA%</th>
                                                        <th style="text-align: center">Final%</th>
                                                        <th style="text-align: center">Overall %</th>
                                                        <th style="text-align: center">Course LIC</th>
                                                        <%--  <th>Total</th>--%>
                                                        <%-- <th>Seq No.</th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>

                                        <ItemTemplate>
                                            <tr>
                                                <%-- <th><%# Container.DataItemIndex + 1 %></th>--%>
                                                <td><%# Eval("CCODE")%> - <%# Eval("COURSE_NAME")%> </td>
                                                 <td><%# Eval("SUBNAME")%> </td>
                                                <td style="text-align: center">
                                                    <asp:TextBox ID="txtcredits" runat="server" CssClass="form-control" Text='<% #Eval("CREDITS")%>'>></asp:TextBox></td>
                                                <td style="text-align: center">
                                                    <asp:TextBox ID="txtcapacity" runat="server" CssClass="form-control" Text='<% #Eval("CAPACITY")%>'>></asp:TextBox></td>
                                                <%--  <td><%# Eval("CREDITS") %></td>
                                       <td><%# Eval("CAPACITY") %></td>--%>
                                                <td style="text-align: center">
                                                    <asp:DropDownList ID="ddlcore" runat="server" CssClass="form-control test" AppendDataBoundItems="True"
                                                        OnSelectedIndexChanged="ddlcore_SelectedIndexChanged" ToolTip='<%# Container.DataItemIndex + 1 %>'>
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblcore" runat="server" Text='<% #Eval("ELECTIVEGP")%>' Visible="false"></asp:Label>
                                                    <asp:HiddenField ID="hdfcourse" runat="server" Value='<% #Eval("COURSENO")%>' />

                                                </td>
                                                <td style="text-align: center; display:none">
                                                    <asp:DropDownList ID="ddlcoregroup" runat="server" CssClass="form-control"
                                                        AppendDataBoundItems="True" Enabled="false">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hdfValue1" runat="server" Value='<%# Container.DataItemIndex + 1 %>' />
                                                    <asp:Label ID="lblelective" runat="server" Text='<% #Eval("GROUPNO")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="chkoffered" runat="server" ToolTip='<%# Eval("COURSENO") %>' /></td>
                                                <td style="text-align: center">
                                                   <%-- <asp:DropDownList ID="ddlsem" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>--%>
                                                 <asp:ListBox ID="ddlsem" runat="server" CssClass="form-control multi-select-demo"
                                                    SelectionMode="Multiple" AppendDataBoundItems="true" onblur="getVal();" ></asp:ListBox>
                                                    <asp:Label ID="LblSemNo" runat="server" Text='<% #Eval("SEMESTERNO")%>' ToolTip='<% #Eval("OFFERED")%>' Visible="false"></asp:Label></td>

                                                <td style="text-align: center">
                                                    <asp:TextBox ID="txtIntern" runat="server" CssClass="form-control" Text='<% #Eval("INTERNAL_WEIGHTAGE")%>'>></asp:TextBox></td>
                                                <td style="text-align: center">
                                                    <asp:TextBox ID="txtExtern" runat="server" CssClass="form-control" Text='<% #Eval("EXTERNAL_WEIGHTAGE")%>'>></asp:TextBox></td>
                                                
                                                 <td style="text-align: center">
                                                    <asp:TextBox ID="txtca" runat="server" CssClass="form-control" Text='<% #Eval("CA_PERCENTAGE")%>'> ></asp:TextBox>
                                                </td>
                                                <td style="text-align: center">
                                                     <asp:TextBox ID="txtfinal" runat="server" CssClass="form-control" Text='<% #Eval("FINAL_PERCENTAGE")%>'>></asp:TextBox></td>
                                            
                                                <td style="text-align: center">
                                                    <asp:TextBox ID="txtoverall" runat="server" CssClass="form-control" Text='<% #Eval("OVERALL_PERCENTAGE")%>'> ></asp:TextBox>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:DropDownList ID="ddlmodulelic" runat="server" CssClass="form-control select2 select-click" AppendDataBoundItems="True">
                                                         <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                     <asp:Label ID="lbllic" runat="server" Text='<% #Eval("COURSELIC")%>'  Visible="false"></asp:Label></td>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        function getVal() {
            // alert("hii")
            var array = []
            var semno;
            var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

            for (var i = 0; i < checkboxes.length; i++) {

                //array.push(checkboxes[i].value)    
                if (semno === "undefined") {
                    semno = checkboxes[i].value + ',';
                }
                else {
                    semno += checkboxes[i].value + ',';

                }
            }
            alert(degreeNo);

            $('#<%= hdfsem.ClientID %>').val(semno);
         }
    </script>

    <%--<script>
        function getValCheck() {
            $(<%=ddlsem.ClientID%>).multiselect({ selectAll: true });
        }
    </script>--%>

     <script type="text/javascript">
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

    </script>

     <script>
         $("[id*=ddlcore]").bind("change", function () {
             //Find and reference the GridView.
             var List = $(this).closest("table");
             var ddlValue = $(this).val();
             //If the CheckBox is Checked then enable the TextBoxes in thr Row.
             if (ddlValue == "2") {
                 var td = $("td", $(this).closest("tr"));
                 $("[id*=ddlcoregroup]", td).removeAttr("disabled");

             } else {
                 var td = $("td", $(this).closest("tr"));
                 $("[id*=ddlcoregroup]", td).attr("disabled", "disabled");
                 //td.css({ "background-color": "#D8EBF2" });
                 //$("input[type=dropdown]", td).removeAttr("disabled");
                 //$("select", td).removeAttr("disabled");

             }
         });
    </script>

     <script>
         var prm = Sys.WebForms.PageRequestManager.getInstance();

         prm.add_endRequest(function () {
             $("[id*=ddlcore]").bind("change", function () {
                 //Find and reference the GridView.
                 var List = $(this).closest("table");
                 var ddlValue = $(this).val();
                 //If the CheckBox is Checked then enable the TextBoxes in thr Row.
                 if (ddlValue == "2") {
                     var td = $("td", $(this).closest("tr"));
                     $("[id*=ddlcoregroup]", td).removeAttr("disabled");

                 } else {
                     var td = $("td", $(this).closest("tr"));
                     $("[id*=ddlcoregroup]", td).attr("disabled", "disabled");
                     //td.css({ "background-color": "#D8EBF2" });
                     //$("input[type=dropdown]", td).removeAttr("disabled");
                     //$("select", td).removeAttr("disabled");

                 }
             });
         });
    </script>
</asp:Content>

