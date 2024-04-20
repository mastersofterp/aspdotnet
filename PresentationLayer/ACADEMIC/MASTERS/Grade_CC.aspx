<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Grade_CC.aspx.cs" Inherits="ACADEMIC_MASTERS_Grade_CC" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager;
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updGradeEntry"
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

    <asp:UpdatePanel ID="updGradeEntry" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfGradenew" runat="server" ClientIDMode="Static" />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">GRADE ENTRY</h3>--%>
                            <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Programme Type</label>--%>
                                             <asp:Label ID="lblDYddlProgrammeType" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true"
                                            ToolTip="Please Select Programme Type" data-select2-enable="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Programme Type" ValidationGroup="submit"
                                            SetFocusOnError="True" InitialValue="0" Width="200px"></asp:RequiredFieldValidator>
                                    </div>
                                    <%--     <div class="form-group col-lg-6 col-md-6 col-12">
                                   <%--<div class="form-group col-lg-3 col-md-6 col-12">--%>
                                    <%--<div class="label-dynamic">
                                                  <sup>* </sup>
                                                <label>College/Scheme</label>
                                           </div>                                       
                                                    <asp:ListBox ID="ddlcollege" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo"
                                                       OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged1"  AutoPostBack="false"></asp:ListBox>

                                               <%-- <asp:DropDownList ID="ddlcollege"  runat="server" CssClass="form-control"
                                                    AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="ddlcollege_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem> </asp:DropDownList>--%>
                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlcollege"
                                                    Display="None" ErrorMessage="Please Select College/Scheme" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>--%>


                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>College/Scheme</label>--%>
                                           <asp:Label ID="lblDYddlcollege" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:ListBox ID="ddlcollege" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo"
                                            OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged1" AutoPostBack="true"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlcollege"
                                            Display="None" ErrorMessage="Please Select at least one College/Scheme" InitialValue="" SetFocusOnError="True"
                                            ValidationGroup="submit" Width="200px"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Grade Type </label>--%>
                                            <asp:Label ID="lblDYddlGradeType" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlGradeType" runat="server" AppendDataBoundItems="true"
                                            ToolTip="Please Select Grade Type" data-select2-enable="true" OnSelectedIndexChanged="ddlGradeType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvGradeTYpe" runat="server" ControlToValidate="ddlGradeType"
                                            Display="None" ErrorMessage="Please Select Grade Type" ValidationGroup="submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                   <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Subject Type </label>
                                        </div>--%>
                                       <%-- <asp:DropDownList ID="ddlSubType" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                            ValidationGroup="submit" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSubType_SelectedIndexChanged">
                                            <%--<asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Theory</asp:ListItem>
                                            <asp:ListItem Value="2">Practical</asp:ListItem>--%>
                                            <%--Added By Nikhil V.Lambe on 24/02/2021 for Sessional Sub Type in MAKAUT--%>
                                            <%--    <asp:ListItem Value="3">Sessional</asp:ListItem>--%>
                                       <%-- </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSubType" runat="server" ControlToValidate="ddlSubType"
                                            Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>--%>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Subject Type</label>--%>
                                            <asp:Label ID="lblDYddlSubtype" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:ListBox ID="ddlSubType" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo"
                                            OnSelectedIndexChanged="ddlSubType_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="rfvSubType" runat="server" ControlToValidate="ddlSubType"
                                            Display="None" ErrorMessage="Please Select at least one Subject Type" InitialValue="" SetFocusOnError="True"
                                            ValidationGroup="submit" Width="200px"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Programme Type</label>--%>
                                            <asp:Label ID="lblDYddlProgrammeType_TAB" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:ListBox ID="ddlALType" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"
                                            SelectionMode="multiple" ClientIDMode="Static"></asp:ListBox>


                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlALType"
                                            Display="None" ErrorMessage="Please Select Programme Type" ValidationGroup="submit"
                                            SetFocusOnError="True" InitialValue="0" Width="200px"></asp:RequiredFieldValidator>
                                    </div>


                                </div>
                            </div>
                            <div class="col-12 text-center box-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Show" ToolTip="Show" ValidationGroup="submit"
                                    OnClick="btnSave_Click" CssClass="btn btn-success" Visible="True" ClientIDMode="Static" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" CausesValidation="false" CssClass="btn btn-primary" Visible="false" ValidationGroup="submit" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnLock" runat="server" Text="Lock" ToolTip="Lock" CausesValidation="false" CssClass="btn btn-info" Visible="false" OnClick="btnLock_Click" />
                                <asp:Button ID="btnUnlock" runat="server" Text="Unlock" ToolTip="UnLock" CausesValidation="false" CssClass="btn btn-primary" Visible="false" OnClick="btnUnlock_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false" OnClick="btnCancel_Click" CssClass="btn btn-warning" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <div class="col-12 ">
                                <%--<table class="table table-striped table-bordered display " style="width: 100%">
                                    <asp:Repeater ID="lvGrade" Visible="false" runat="server">
                                        <HeaderTemplate>

                                            <div class="sub-heading">
                                                <h5>Grade List</h5>
                                            </div>
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    
                                                    <th>Grade
                                                    </th>
                                                    <th>Grade Point
                                                   </th>
                                                    <th>Max Mark
                                                    </th>
                                                    <th>Min Mark
                                                    </th>
                                                    <th>Grade Description
                                                    </th>
                                                   <th>Result</th>
                                                    <th>Status</th>
                                                  
                                                </tr>
                                            </thead>
                                            <tbody>
                                                
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                              
                                                <td >
                                                    
                                                  <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("GRADE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                  <asp:TextBox ID="txtGradePoint" runat="server"  placeholder="Enter Grade Point"  MaxLength="6" ToolTip="Please Enter Grade Point " Text='<%# Eval("GRADEPOINT")%>' ></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="rfvGradePoint" runat="server" ControlToValidate="txtGradePoint"
                                            Display="None" ErrorMessage="Please Enter Grade Point" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftxtGradePoint" runat="server" ValidChars=".0123456789"
                                             TargetControlID="txtGradePoint">
                                             </ajaxToolKit:FilteredTextBoxExtender> 

                                                </td>
                                             
                                                <td>
                                                  <asp:TextBox ID="txtMaxMark" runat="server" Text='<%# Eval("MAXMARK")%>'  placeholder="Enter Maximum Mark"
                                             ToolTip="Please Enter Maximum Mark " OnTextChanged="txtMaxMark_TextChanged" />
                                        <asp:RequiredFieldValidator ID="rfvMaxMark" runat="server" ControlToValidate="txtMaxMark"
                                            Display="None" ErrorMessage="Please Enter Maximum Mark" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                         <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtMaxMarks" runat="server" ValidChars=".0123456789"
                                                        TargetControlID="txtMaxMark">
                                                    </ajaxToolKit:FilteredTextBoxExtender> 
                                                </td>
                                                <td>
                                                <asp:TextBox ID="txtMinMark" runat="server"  Text='<%# Eval("MINMARK")%>'  placeholder="Enter Minimum Mark"
                                            MaxLength="2" ToolTip="Please Enter Minimum Mark " />
                                        <asp:RequiredFieldValidator ID="rfvMinMark" runat="server" ControlToValidate="txtMinMark"
                                            Display="None" ErrorMessage="Please Enter Minimum Mark" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtMinMarks" runat="server" ValidChars=".0123456789"
                                                        TargetControlID="txtMinMark">
                                                    </ajaxToolKit:FilteredTextBoxExtender>     
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGradeDesc" runat="server" Text='<%# Eval("DESC_GRADE")%>'   
                                            MaxLength="50" TextMode="MultiLine" />
                                        <asp:RequiredFieldValidator ID="txtGradevalid" runat="server" ControlToValidate="txtGradeDesc"
                                          Display="None" ErrorMessage="Please Enter Grade Description" ValidationGroup="submit"
                                          SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </td>
                                             
                                                <td>
                                                      <asp:DropDownList ID="ddlResult" runat="server"  AppendDataBoundItems="true"
                                            ToolTip="Please Select Result "  data-select2-enable="true">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Fail</asp:ListItem>
                                            <asp:ListItem Value="1">Pass</asp:ListItem>
                                                      </asp:DropDownList>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlResult"
                                          Display="None" ErrorMessage="Please Enter Result" ValidationGroup="submit"
                                          SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </td>
                                                 <td>
                                                    <asp:CheckBox ID="chkStatus" runat="server" />
                                                  
                                                </td>
                                            </tr>
                                             
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>--%>
                                <asp:ListView ID="lvGrade" Visible="false" runat="server">
                                    <LayoutTemplate>
                                        <div id="demo-grid">
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="mytable">
                                                <thead class="bg-light-blue">
                                                    <tr>

                                                        <th>Grade
                                                        </th>
                                                        <th>Grade Point
                                                        </th>
                                                        <th>Max Mark
                                                        </th>
                                                        <th>Min Mark
                                                        </th>
                                                        <th>Grade Description
                                                        </th>
                                                        <th>Result</th>
                                                        <th>Status</th>

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

                                                <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("GRADE")%>' ToolTip=' <%# Eval("GRADENO")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtGradePoint" runat="server" MaxLength="6" Enabled='<%# Eval("IsLock").ToString()=="0"?true:false %>' Text='<%# Eval("GRADEPOINT")%>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvGradePoint" runat="server" ControlToValidate="txtGradePoint"
                                                    Display="None" ErrorMessage="Please Enter Grade Point" ValidationGroup="submit"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftxtGradePoint" runat="server" ValidChars=".0123456789"
                                                    TargetControlID="txtGradePoint">
                                                </ajaxToolKit:FilteredTextBoxExtender>

                                            </td>

                                            <td>
                                                <asp:TextBox ID="txtMaxMark" runat="server" MaxLength="6" Text='<%# Eval("MAXMARK")%>' placeholder="Enter Maximum Mark" Enabled='<%# Eval("IsLock").ToString()=="0"?true:false %>'
                                                    OnTextChanged="txtMaxMark_TextChanged" />
                                                <asp:RequiredFieldValidator ID="rfvMaxMark" runat="server" ControlToValidate="txtMaxMark"
                                                    Display="None" ErrorMessage="Please Enter Maximum Mark" ValidationGroup="submit"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtMaxMarks" runat="server" ValidChars=".0123456789"
                                                    TargetControlID="txtMaxMark">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMinMark" runat="server" Text='<%# Eval("MINMARK")%>' placeholder="Enter Minimum Mark" Enabled='<%# Eval("IsLock").ToString()=="0"?true:false %>'
                                                    MaxLength="6" />
                                                <asp:RequiredFieldValidator ID="rfvMinMark" runat="server" ControlToValidate="txtMinMark"
                                                    Display="None" ErrorMessage="Please Enter Minimum Mark" ValidationGroup="submit"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtMinMarks" runat="server" ValidChars=".0123456789"
                                                    TargetControlID="txtMinMark">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtGradeDesc" runat="server" Text='<%# Eval("DESC_GRADE")%>'
                                                    MaxLength="50" Enabled='<%# Eval("IsLock").ToString()=="0"?true:false %>' TextMode="MultiLine" />
                                                <asp:RequiredFieldValidator ID="txtGradevalid" runat="server" ControlToValidate="txtGradeDesc"
                                                    Display="None"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </td>

                                            <td>
                                                <asp:DropDownList ID="ddlResult" runat="server" AppendDataBoundItems="true"
                                                    Enabled='<%# Eval("IsLock").ToString()=="0"?true:false %>' SelectedValue='<%# Eval("RESULT")%> ' data-select2-enable="true">
                                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="0">Fail</asp:ListItem>
                                                    <asp:ListItem Value="1">Pass</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlResult"
                                                    Display="None" ErrorMessage="Please Enter Result" ValidationGroup="submit"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkStatus" runat="server" Enabled='<%# Eval("IsLock").ToString()=="0"?true:false %>' Checked='<%# Eval("ACTIVESTATUS").ToString().Equals("Active") %>' />
                                                <%-- <asp:CheckBox ID="chkStatus" Text='<%# Eval("ACTIVESTATUS")%> ' runat="server" />--%>
                                                  
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            
        </Triggers>--%>
    </asp:UpdatePanel>

    <%-- Grade Master--%>
    <%-- <script>
        function settimeslotgradenew(val) {

            //$('#rdActivegradenew').prop('checked', val);
            // $('#hftimeslot').val($('#rdActivetimeslot').prop('checked'));
            $('#chkStatus').val($('#rdActivetimeslot').prop('checked'));
        }

        function validateGrade() {
            //alert("he");
            debugger
            $('#hfGradenew').val($('#chkStatus').prop('checked'));

            var Gradename = $("[id$=ddlGradeType]").attr("id");
            var Gradename = document.getElementById(Gradename);
            var Program = $("[id$=ddlSection]").attr("id");
            var Program = document.getElementById(Program);
            
            
            //var txtMinMark = $("[id$=txtMinMark]").attr("id");
           // var txtMinMark = document.getElementById(txtMinMark);
            if (Gradename.value == 0 && Program.value==0) {
                alert('Please Enter GradeType and Programme Type ', 'Warning!');
               // $(Gradename).focus();
               return false;
            }
            else if (Program.value != 0 && Gradename.value == 0) {
                alert('Please Enter Grade Type ', 'Warning!');
              //  $(Program).focus();
                return false;
            }
            else if (Program.value == 0 && Gradename.value != 0) {
                alert('Please Enter Program Type ', 'Warning!');
                //  $(Program).focus();
                return false;
            }
            //else if (txtMinMark.value==0) {
               // alert('Please Ente min', 'Warning!');
                //  $(Program).focus();
               // return false;
            
            //else { alert(' ', 'Warning!'); return false; }
           
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        $('#btnSave').click(function () {
            validateGrade();
        });
        //prm.add_endRequest(function () {
        //    $(function () {
        //        $('#btnSave').click(function () {
                   
        //            //alert("hi");
        //            validateGrade();
        //        });
        //    });
        //});

        
    </script>--%>

    <%--   <script>
         function SetStat(val) {
             $('#switch').prop('checked', val);
         }

         var summary = "";
         $(function () {

             $('#btnSave').click(function () {
                 localStorage.setItem("currentId", "#btnSave,Submit");
                 debugger;
                 ShowLoader('#btnSave');

                 if ($('#txtRuleName').val() == "")
                     summary += '<br>Please Enter Rule Name';
                 if ($('#ddlStreamAL').val() == "0")
                     summary += '<br>Please Select AL Stream ';
                 if ($('#ddlMinCourseAl').val() == "0")
                


                 if (summary != "") {
                     customAlert(summary);
                     summary = "";
                     return false
                 }
                 $('#hfdStat').val($('#switch').prop('checked'));

             });
         });

         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_endRequest(function () {
             $(function () {
                 $('#btnSave').click(function () {
                     localStorage.setItem("currentId", "#btnSave,Submit");
                     ShowLoader('#btnSave');

                     if ($('#txtRuleName').val() == "")
                         summary += '<br>Please Enter Rule Name';
                     if ($('#ddlStreamAL').val() == "0")
                         summary += '<br>Please Select AL Stream ';
                     if ($('#ddlMinCourseAl').val() == "0")
                         summary += '<br>Please Select Min Course AL';
                     if ($('#ddlMinGradeAL').val() == "0")
                     

                     if (summary != "") {
                         customAlert(summary);
                         summary = "";
                         return false
                     }
                     $('#hfdStat').val($('#switch').prop('checked'));

                 });
             });
         });
    </script>--%>
    <script>
        //$(document).ready(function () {
        //    $('.multi-select-demo').multiselect({
        //        includeSelectAllOption: true,
        //        maxHeight: 200
        //    });
        //});

        //var parameter = Sys.WebForms.PageRequestManager.getInstance();
        //parameter.add_endRequest(function () {
        //    $(document).ready(function () {
        //        $('.multi-select-demo').multiselect({
        //            includeSelectAllOption: true,
        //            maxHeight: 200
        //        });
        //    });
        //});


    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>
</asp:Content>



<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .btn-danger {
            height: 26px;
        }
    </style>
</asp:Content>




