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

                                    <asp:ListView ID="lvonlinemapping" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>Online Degree subject Mapping List</h5>
                                                </div>
                                                <%-- <div class="sub-heading">
                                                       <h5>Online Degree subject Mapping List</h5>
                                                        </div>--%>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tab-le">
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
</asp:Content>
