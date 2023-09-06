<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DefineSpecialization.aspx.cs" Inherits="RFC_CONFIG_Masters_DefineSpecialization" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hfSpstatus" runat="server" ClientIDMode="Static" />
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                    <%--<h3 class="box-title">BASIC MASTER CREATION</h3>--%>
                </div>

                <div class="box-body">
                    <div>
                        <%--<asp:UpdateProgress ID="UpdateProgSpecialisation" runat="server" AssociatedUpdatePanelID="updSpecialisation"
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
                        </asp:UpdateProgress>--%>
                    </div>
                    <%--<asp:UpdatePanel ID="updSpecialisation" runat="server">
                        <ContentTemplate>--%>

                    <div class="box-body">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">

                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <%-- <label>Degree</label>--%>
                                        <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <%--<asp:DropDownList ID="ddlDegree" runat="server" TabIndex="4" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>--%>
                                    <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="1" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">

                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" ErrorMessage="Please Select Degree." ValidationGroup="submit"
                                        SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">

                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <%-- <label>Degree</label>--%>
                                        <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfcBranch" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" ErrorMessage="Please Select Programme/Branch." ValidationGroup="submit"
                                        SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>



                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Specialisation</label>

                                    </div>
                                    <asp:DropDownList ID="ddlSpecialisation" runat="server" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvtxtStudMobile" runat="server" ControlToValidate="ddlSpecialisation"
                                        Display="None" ErrorMessage="Please Enter Specialization Name. " SetFocusOnError="True" InitialValue="0"
                                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">

                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Knowledge Partner</label>
                                        <%--<asp:Label ID="lblKnowledgepartner" runat="server" Font-Bold="true"></asp:Label>--%>
                                    </div>
                                    <asp:DropDownList ID="ddlKnowledgepartner" runat="server" TabIndex="4" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlKnowledgepartner"
                                            Display="None" ErrorMessage="Please Select Knowledge Partner." ValidationGroup="submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Intake</label>

                                    </div>
                                    <asp:TextBox ID="txtIntake" AutoComplete="off" TabIndex="4" placeholder="Enter Intake " runat="server" MaxLength="3" CssClass="form-control"
                                        ToolTip="Please Enter Intake" />
                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" ValidChars=".,0123456789+-&() " TargetControlID="txtIntake" />
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtIntake"
                                                Display="None" ErrorMessage="Please Enter Intake." ValidationGroup="submit"
                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Status</label>
                                    </div>
                                    <div class="switch form-inline">
                                        <input type="checkbox" id="rdActiveSp" name="switch" class="switch" checked />
                                        <label data-on="Active" data-off="Inactive" for="rdActiveSp"></label>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <br />
                        <div class="box-footer">
                            <p class="text-center">
                                <%--<asp:Button ID="btnSpSubmit" runat="server" Text="Submit" ToolTip="Submit"
                                                                CssClass="btn btn-primary" TabIndex="8" OnClick="btnSpSubmit_Click" OnClientClick="return ValidatedefineSp();" />--%>
                                <asp:Button ID="btnSpSubmit" runat="server" Text="Submit" ToolTip="Submit" CssClass="btn btn-primary" OnClick="btnSpSubmit_Click" OnClientClick="return ValidatedefineSp();" />
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click" ToolTip="Cancel"
                                    CssClass="btn btn-warning" TabIndex="9" />
                                <%--  <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="teacherallot"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />--%>
                            </p>
                        </div>

                        <div class="col-md-12">
                            <asp:Panel ID="Panelsp" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="lvSpecialization" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Specialization List</h5>
                                        </div>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th style="text-align: center;">Edit </th>
                                                    <%--<th>ID </th>--%>
                                                    <th>Degree
                                                    </th>
                                                    <th>Branch
                                                    </th>
                                                    <th>Specialization
                                                    </th>
                                                    <th>Knowledge Partner
                                                    </th>
                                                    <th>Intake
                                                    </th>
                                                    <th>Status </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <asp:UpdatePanel runat="server" ID="updSpecialisationsp">
                                            <ContentTemplate>
                                                <tr>
                                                    <td style="text-align: center;">
                                                        <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" class="newAddNew Tab" CommandArgument='<%# Eval("SPECIAL_MAP_NO") %>' ImageUrl="~/images/edit.png" TabIndex="10" ToolTip="Edit Record" OnClick="btnEdit_Click1" />
                                                    </td>
                                                    <%-- <td><%# Eval("SPECIALISATIONNO")%></td>--%>
                                                    <td><%# Eval("DEGREENAME")%></td>
                                                    <td><%# Eval("LONGNAME")%></td>
                                                    <td><%# Eval("SPECIALISATION_NAME")%></td>
                                                    <td><%# Eval("KNOWLEDGE_PARTNER")%></td>
                                                    <td><%# Eval("INTAKE ")%></td>
                                                    <td>
                                                        <asp:Label ID="lblActive3" runat="server" ForeColor='<%# Eval("ACTIVESTATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' Text='<%# Eval("ACTIVESTATUS")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ContentTemplate>
                                            <%--<Triggers>
                                                        <asp:PostBackTrigger ControlID="btnSpSubmit" />
                                                    </Triggers>--%>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                    </div>
                    <%--</ContentTemplate>
                             <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnSpSubmit" />
                                                    </Triggers>
                    </asp:UpdatePanel>--%>
                </div>
            </div>
        </div>
    </div>



    <script>

        function setdefinesp(val) {
            $('#rdActiveSp').prop('checked', val);
        }

        function ValidatedefineSp() {
            // alert('A');
            $('#hfSpstatus').val($('#rdActiveSp').prop('checked'));

            var ddljobtype = $("[id$=ddlJobTypes]").attr("id");

            var ss = document.getElementById('<%=ddlDegree.ClientID%>').value;

            if (ss == '0') {

                alert('Please Select Degree ', 'Warning!');

                $(ddljobtype).focus();
                return false;
            }


            var ddlBranchd = $("[id$=ddlBranch]").attr("id");

            var sss = document.getElementById('<%=ddlBranch.ClientID%>').value;

            if (sss == '0') {

                alert('Please Select Branch. ', 'Warning!');

                $(ddlBranchd).focus();
                return false;
            }


            var txtCurrency = $("[id$=txtSpName]").attr("id");
            var txtCurrency = document.getElementById(txtCurrency);
            if (txtCurrency.value.length == 0) {

                alert('Please Enter Specialisation Name ', 'Warning!');

                $(txtCurrency).focus();
                return false;
            }



            var ddlKnowledgepartne = $("[id$=ddlKnowledgepartner]").attr("id");

            var knowlege = document.getElementById('<%=ddlKnowledgepartner.ClientID%>').value;

           if (knowlege == '0') {

               alert('Please Select Knowledge Partner. ', 'Warning!');

               $(ddlKnowledgepartne).focus();
               return false;

           }

           var txtIntake = $("[id$=txtIntake]").attr("id");
           var txtIntake = document.getElementById(txtIntake);
           if (txtIntake.value.length == 0) {

               alert('Please Enter Intake. ', 'Warning!');

               $(txtIntake).focus();
               return false;
           }



       }

       var prm = Sys.WebForms.PageRequestManager.getInstance();
       prm.add_endRequest(function () {
           $(function () {
               $('#btnSpSubmit').click(function () {
                   ValidatedefineSp();
               });
           });
       });

    </script>



    <%--  <script>
        function SetIntervals(val) {
            $('#chkStatusIntervals').prop('checked', val);
        }

        function validateint() {

            $('#hfIntervals').val($('#chkStatusIntervals').prop('checked'));

            var txtRound = $("[id$=txtIntervals]").attr("id");
            var txtRound = document.getElementById(txtRound);

            if (txtRound.value.length == 0) {
                alert('Please Enter Intervals ', 'Warning!');
                //   $(txtCurrency).css('border-color', 'red');
                $(txtRound).focus();
                return false;
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitIntervals').click(function () {
                    validateint();
                });
            });
        });
        </script>--%>
</asp:Content>

