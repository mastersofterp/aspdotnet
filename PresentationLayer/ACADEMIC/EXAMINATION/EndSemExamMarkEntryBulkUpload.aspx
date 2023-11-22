<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="EndSemExamMarkEntryBulkUpload.aspx.cs" Inherits="Academic_MarkEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

    <style>
        .tbl-class {
            position: sticky;
            z-index: 1;
            top: 0;
            background: #fff !important;
            box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;
        }

        #ctl00_ContentPlaceHolder1_gvStudent.table-bordered > tbody > tr > th {
            border-top: 0px solid var(--table-border-color);
        }
    </style>

    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>

            <%--<style>
                table#ctl00_ContentPlaceHolder1_gvStudent_ctl02_cecomTutMarks_popupTable {
                    left: 980px !important;
                    top: 85px !important;
                }
            </style>--%>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>EXAM MARK ENTRY BY ADMIN</b></h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlMarkEntry" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-lg-8 col-md-8 col-12">
                                            <div class="row">
                                                <div class="col-lg-4 col-md-6 col-12 form-group">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                         <%--<asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>--%>
                                                        <label>College & Session</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlcollege" TabIndex="1" runat="server" CssClass="form-control" AppendDataBoundItems="true" ToolTip="Please Select College & Session" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlcollege_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                      <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlcollege"
                                                        Display="None" ErrorMessage="Please Select College & Session Name." InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                               
                                                </div>

                                                <div class="col-lg-4 col-md-6 col-12 form-group">
                                                    <div class="label-dynamic">
                                                        <%--<sup>* </sup>--%>
                                                         <%--<asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>--%>
                                                        <label>Semester</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSemester" TabIndex="2" runat="server" CssClass="form-control" AppendDataBoundItems="true" ToolTip="Please Select Semester" data-select2-enable="true"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                   <%--   <asp:RequiredFieldValidator ID="rfvsemester" runat="server" ControlToValidate="ddlSemester"
                                                        Display="None" ErrorMessage="Please Select College & Session Name." InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                               
                                                </div>
                                                

                                                 <div class="form-group col-lg-4 col-md-6 col-12 form-group">
                                                    <div class="label-dynamic">
                                                        <label>Excel File</label>
                                                    </div>

                                                   <%--<asp:FileUpload ID="FuBrowse" runat="server" ToolTip="Select file to Upload" TabIndex="3" />--%>

                                                      <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="uplFileUpload">
                                                    <ContentTemplate>
                                                    <asp:FileUpload ID="FuBrowse" runat="server" ToolTip="Select file to Import" TabIndex="14" />
                                                    </ContentTemplate>                                                       
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-lg-4 col-md-6 col-12 form-group" id="divSubExamName" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Sub Exam Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSubExamName" runat="server" AppendDataBoundItems="True" data-select2-enable="true"
                                                        CssClass="form-control" TabIndex="11" AutoPostBack="True">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-lg-4 col-md-6 col-12 form-group mt-4">
                                                    <asp:Label ID="lblStudents" runat="server" Font-Bold="true" style="box-shadow: rgb(0 0 0 / 20%) 0px 2px 4px 0px, rgb(0 0 0 / 19%) 0px 3px 10px 0px; padding: 5px;"/>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-4 col-md-4 col-12">
                                            <div class=" note-div" style="margin-top: 22px">
                                                <h5 class="heading">Note </h5>
                                                <%-- <p><i class="fa fa-star" aria-hidden="true"></i><span>Session is mandatory for generating Marks Entry Report</span>  </p>--%>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span style="color: #000; font-weight: bold">Please Save and Lock for Final Submission.</span>  </p>
                                                <p>
                                                    <i class="fa fa-star" aria-hidden="true"></i><span><span style="color: green; font-weight: bold">(902) for Absent  </span>
                                                        <br />
                                                        <span style="color: green; margin-left: 30px; font-weight: bold">(903) for UFM </span>
                                                        <br />
                                                        <span style="color: green; margin-left: 30px; font-weight: bold">(904) for Detention </span>
                                                        <br />
                                                        <span style="color: green; margin-left: 30px; font-weight: bold">(905) for Incomplete </span>
                                                        <br />
                                                        <span style="color: green; margin-left: 30px; font-weight: bold">(906) for Withdrawl </span></span>
                                                </p>
                                            </div>

                                        </div>
                                        <%-- <div class="col-md-4" style="margin-top: 25px">
                                        </div>--%>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer mb-3 ">

                                    <asp:Button ID="btnDownloadTemplate" runat="server" TabIndex="3" 
                                        Text="DownLoad Temaplate" ValidationGroup="show" OnClick="btnBlankDownld_Click"
                                        CssClass="btn btn-primary" />

                                    <asp:Button ID="btnprocess" runat="server" TabIndex="4" 
                                        Text="Process Mark" ValidationGroup="show" OnClick="btnprocess_Click"
                                        CssClass="btn btn-primary" />

                                      <asp:Button ID="btnreportbeforemark" runat="server" TabIndex="5" 
                                        Text="Report Before Mark Entry" ValidationGroup="show" OnClick="btnreportbeforemark_Click"
                                        CssClass="btn btn-primary" />

                                      <asp:Button ID="btnreportaftermark" runat="server" TabIndex="6" 
                                        Text="Report After Mark Entry" ValidationGroup="show" OnClick="btnreportaftermark_Click"
                                        CssClass="btn btn-primary" />



                                  <asp:Button ID="btnSave" runat="server" TabIndex="13" Enabled="false" CausesValidation="false" Visible="false"
                                        Text="Save" CssClass="btn btn-primary btnSaveEnabled" ValidationGroup="val" />

                                    <asp:Button ID="btnLock" runat="server" TabIndex="14" Enabled="false" Visible="false"
                                        OnClientClick="return showLockConfirm(this,'val');" Text="Lock"
                                        CssClass="btn btn-warning" />

                                   <%-- <asp:Button ID="btnGrade" runat="server" TabIndex="15" Enabled="false" Visible="false"
                                        OnClick="btnGrade_Click" OnClientClick="return showGradeConfirm(this,'val');" Text="Generate Grade"
                                        CssClass="btn btn-primary btnSaveEnabled" />

                                    <asp:Button ID="btnReGrade" runat="server" TabIndex="16" Enabled="false" Visible="false"
                                        OnClick="btnReGrade_Click" OnClientClick="return showGradeConfirm(this,'val');" Text="ReGenerate Grade"
                                        CssClass="btn btn-success btnSaveEnabled" />--%>
                                

                          
                                    
                                    <asp:Button ID="btnCancel2" runat="server" TabIndex="7" Font-Bold="true"  OnClick="btnCancel2_Click"
                                        Text="Cancel" CssClass="btn btn-warning" />

                                    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="show" DisplayMode="List"
                                        ShowSummary="false" ShowMessageBox="true" />
                                    <asp:ValidationSummary runat="server" ID="ValidationSummary2" ValidationGroup="MarksModifyReport" DisplayMode="List"
                                        ShowSummary="false" ShowMessageBox="true" />
                                    <asp:ValidationSummary runat="server" ID="ValidationSummary3" ValidationGroup="ExternalMark" DisplayMode="List"
                                        ShowSummary="false" ShowMessageBox="true" />


                                </div>
                             
                                <%--prafull added end here--%>
                                <div class="col-12" id="tdStudent" runat="server">
                                    <asp:Panel ID="pnlStudGrid" runat="server" Visible="False">
                                        <%--<span style="color: red">* Checkbox selection is mandatory only for Save Marks.</span>--%>
                                        <div class="sub-heading mt-3 mb-2">
                                            <h5>Enter Marks for following Students</h5>
                                        </div>
                                        <div class="table-responsive" style="height: 400px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                            <asp:HiddenField ID="hfdMaxMark" runat="server" />
                                            <asp:HiddenField ID="hfdMinMark" runat="server" />
                                            <asp:GridView ID="gvStudent" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered nowrap"
                                                BackColor="#ffffff" BorderStyle="None">
                                                <HeaderStyle CssClass="bg-light-blue tbl-class" />
                                                <RowStyle Height="0px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="1%" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Height="15px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Reg No./Decode No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                         <%--   <asp:Label ID="lblIDNO" runat="server" Text='<%# Session["OrgId"].ToString()=="8"? Bind("DECODENO"): Bind("REGNO") %>' ToolTip='<%# Bind("IDNO") %>'
                                                                Font-Size="9pt"  />--%>

                                                            <asp:Label ID="lblIDNO" runat="server" Text='<%# Session["OrgId"].ToString()=="8"? DataBinder.Eval(Container.DataItem,"DECODENO"):DataBinder.Eval(Container.DataItem,"REGNO") %>' ToolTip='<%# Bind("IDNO") %>'
                                                                Font-Size="9pt"  />

                                                          <%--  <asp:Label ID="lblDECODENO" runat="server" Text='<%# Bind("DECODENO") %>' ToolTip='<%# Bind("DECODENO") %>'
                                                                Font-Size="9pt"  />--%>
                                                        </ItemTemplate>
                                                        <ItemStyle />
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="STUDNAME" HeaderText="Student Name" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="25%">
                                                        <ItemStyle />
                                                    </asp:BoundField>


                                                    <asp:TemplateField HeaderText="Internal Marks" Visible="false" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblInternal" runat="server" Width="80px" Text='<%# Bind("INTERMARK") %>' ToolTip='<%# Bind("INTERMARK") %>'
                                                                Enabled="false" MaxLength="5" Font-Bold="true" Style="text-align: center; box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.2), 0 3px 10px 0 rgba(0, 0, 0, 0.19);" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="MARKS" Visible="false" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtintMarks" runat="server" Text='<%# Bind("TESTMARK") %>' Width="80px"
                                                                Enabled='<%# (Eval("TESTLOCK").ToString() == "True") ? false : true %>'
                                                                MaxLength="5" Font-Bold="true" Style="text-align: center; box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.2), 0 3px 10px 0 rgba(0, 0, 0, 0.19);" onkeyup="return CheckMark(this);" />
                                                            <asp:Label ID="lblintMarks" runat="server" Text='<%# Bind("SMAX") %>' ToolTip='<%# Bind("LOCK") %>'
                                                                Visible="false" />
                                                            <asp:Label ID="lblintMinMarks" runat="server" Text='<%# Bind("SMIN") %>' Visible="false" />

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="MARKS" Visible="false" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtMarks" runat="server" Text='<%# Bind("SMARK") %>' Width="80px"
                                                                Enabled='<%# (Eval("LOCK").ToString() == "True") ? false : true %>'
                                                                MaxLength="5" Font-Bold="true" Style="text-align: center; box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.2), 0 3px 10px 0 rgba(0, 0, 0, 0.19);" onkeyup="return CheckMark(this);" />
                                                            <asp:Label ID="lblMarks" runat="server" Text='<%# Bind("SMAX") %>' ToolTip='<%# Bind("LOCK") %>'
                                                                Visible="false" />
                                                            <asp:Label ID="lblMinMarks" runat="server" Text='<%# Bind("SMIN") %>' Visible="false" />

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total Marks" Visible="false" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtTotal" runat="server" Text='<%# Bind("MARKTOT") %>' Width="80px"
                                                                Enabled='<%# (Eval("LOCK").ToString() == "True") ? false : true %>'
                                                                MaxLength="5" Font-Bold="true" Style="text-align: center; box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.2), 0 3px 10px 0 rgba(0, 0, 0, 0.19);" onkeyup="return CheckMark(this);" />


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Grade" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrade" runat="server" Text='<%# Bind("GRADE") %>' Font-Bold="true"
                                                                Font-Size="9pt" />
                                                        </ItemTemplate>
                                                        <ItemStyle />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnDownloadTemplate" />
             <asp:PostBackTrigger ControlID="btnprocess" />
              <asp:PostBackTrigger ControlID="btnreportbeforemark" />
              <asp:PostBackTrigger ControlID="btnreportaftermark" />
        </Triggers>
    </asp:UpdatePanel>


    <div id="divMsg" runat="server">
    </div>
    <asp:HiddenField ID="hdfDegree" runat="server" Value="" />
    <script language="javascript" type="text/javascript">

        function CheckMark(id) {
            //alert(id.value);
            if (id.value < 0) {
                if (id.value == -1 || id.value == -2 || id.value == -3 || id.value == -4)  /// || id.value == -3
                {
                    //alert("You have marked student as ABSENT.");
                }
                else {
                    alert("Marks Below Zero are Not allowed. Only -1 ,-2,-3 and -4 can be entered.");
                    id.value = '';
                    id.focus();
                }
            }
        }

        function validateMark(txttut, txtmidsem, txtCT, txtCE, txtatt, txttot, col) {

            var mark1, mark2, mark3, mark4, mark5, total;

            if (txttut.value == "") {
                txttot.value = "";
                mark1 = 0
            }
            else {
                mark1 = txttut.value;

            }
            if (txtmidsem.value == "") {
                txttot.value = "";
                mark2 = 0;
            }
            else {
                mark2 = txtmidsem.value;

            }
            if (txtCT.value == "") {
                txttot.value = "";
                mark3 = 0;
            }
            else {
                mark3 = txtCT.value;

            }
            if (txtCE.value == "") {
                txttot.value = "";
                mark4 = 0;
            }
            else {
                mark4 = txtCE.value;

            }
            if (txtatt.value == "") {
                txttot.value = "";
                mark5 = 0;
            }
            else {
                mark5 = txtatt.value;

            }
            if (col == 2) {
                total = parseFloat(mark2) + parseFloat(mark3) + parseFloat(mark5);
            }
            else if (col == 1) {
                total = parseFloat(mark2) + parseFloat(mark3) + parseFloat(mark5) + parseFloat(mark1) + parseFloat(mark4);
            }
            else if (col == 3) {
                total = parseFloat(mark4);
            }
            else if (col == 4) {
                total = parseFloat(mark2) + parseFloat(mark3) + parseFloat(mark5) + parseFloat(mark1);
            }
            txttot.value = total;
        }

        function showLockConfirm(btn, group) {
            debugger;
            var validate = false;
            if (Page_ClientValidate(group)) {
                validate = ValidatorOnSubmit();
            }
            if (validate)    //show div only if page is valid and ready to submit
            {
                var ret = confirm('Do you really want to lock marks for selected exam?\n\nOnce Locked it cannot be modified or changed.');
                if (ret == true) {
                    var ret2 = confirm('You are about to lock entered marks, be sure before locking.\n\nClick OK to Continue....');
                    if (ret2 == true) {
                        validate = true;
                    }
                    else
                        validate = false;
                }
                else
                    validate = false;
            }
            return validate;
        }

        function showUnLockConfirm(btn, group) {
            debugger;
            var validate = false;
            if (Page_ClientValidate(group)) {
                validate = ValidatorOnSubmit();
            }
            if (validate)    //show div only if page is valid and ready to submit
            {
                var ret = confirm('Do you really want to unlock marks for selected exam?');
                if (ret == true) {
                    //                    var ret2 = confirm('You are about to unlock entered marks, be sure before locking.\n\nClick OK to Continue....');
                    //                    if (ret2 == true) {
                    validate = true;
                }
                    //                    else
                    //                        validate = false;
                    //                }
                else
                    validate = false;
            }
            return validate;
        }

        function showLockConfirm_old() {
            var ret = confirm('Do you really want to lock marks for selected exam?');
            if (ret == true)
                return true;
            else
                return false;
        }

        function showGradeConfirm(btn, group) {
            debugger;
            var validate = false;
            if (Page_ClientValidate(group)) {
                validate = ValidatorOnSubmit();
            }
            if (validate)    //show div only if page is valid and ready to submit
            {
                var ret = confirm('Do you really want to Generate Grade for selected exam?\n\nOnce Grade Generated it cannot be modified or changed.');
                if (ret == true) {
                    var ret2 = confirm('You are about to Generate Grade, be sure before generating grade.\n\nClick OK to Continue....');
                    if (ret2 == true) {
                        validate = true;
                    }
                    else
                        validate = false;
                }
                else
                    validate = false;
            }
            return validate;
        }
    </script>


    <script type="text/javascript">
        //jq1833 = jQuery.noConflict();
        $(document).ready(function () {

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {

                $("#ctl00_ContentPlaceHolder1_gvStudent .form-control").keypress(function (e) {
                    if (((e.which != 46 || (e.which == 46 && $(this).val() == '')) || $(this).val().indexOf('.') != -1) && (e.which < 48 || e.which > 57)) {
                        e.preventDefault();
                        //$(this).val("Digits Only").show().fadeOut("slow");
                        $(this).fadeOut("slow").css("border", "1px solid red");
                        $(this).fadeIn("slow");
                    }
                    else {
                        $(this).css("border", "1px solid #3c8dbc");
                    }
                }).on('paste', function (e) {
                    e.preventDefault();
                });

                $("#ctl00_ContentPlaceHolder1_gvStudent .form-control").focusout(function () {

                    debugger;

                    $(this).css("border", "1px solid #d2d6de");
                    var MaxMarks = parseFloat($('input[id$=hfdMaxMark]').val().trim());//$(".MaxMarks").html().split(':')[1].slice(0, -1).trim();
                    //alert('hi Beerla');
                    //alert(parseInt(MaxMarks));
                    if (parseInt($(this).val()) == 90 || parseInt($(this).val()) == 9 || parseInt($(this).val()) == 6) {
                        if (parseInt($(this).val()) > MaxMarks) {
                            alert('Marks should not greater than Max Marks !!');
                            $(this).val('');
                            $(this).focus();
                        }
                    }
                });

                $("#ctl00_ContentPlaceHolder1_gvStudent .form-control").keyup(function () {
                    debugger

                    if (parseFloat($(this).val().trim()) > parseFloat($('input[id$=hfdMaxMark]').val().trim())) {

                        if (("902").indexOf($(this).val()) == -1 && ("903").indexOf($(this).val()) == -1 && ("904").indexOf($(this).val()) == -1 && ("905").indexOf($(this).val()) == -1 && ("906").indexOf($(this).val()) == -1) {
                            alert('Marks should not greater than Max Marks !!');
                            $(this).val('');
                        }

                    }
                    //else if (parseFloat($(this).val().trim()) < parseFloat($('input[id$=hfdMinMark]').val().trim())) {
                    //    alert('Marks should not less than Min Marks !!');
                    //    $(this).val('');
                    //}
                });

            });

        });

    </script>
</asp:Content>
