<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="FeeCollectionOptions.aspx.cs" Inherits="Academic_FeeCollectionOptions"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div id="divStudentSearch" runat="server" class="col-md-12">
                                <div class="row">
                                    <div class="form-group col-12">
                                        <%--Search Pannel Start by Swapnil --%>
                                        <div id="myModal2" role="dialog" runat="server">
                                            <div>
                                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updEdit"
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

                                            <asp:UpdatePanel ID="updEdit" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Search Criteria</label>
                                                                </div>

                                                                <%--onchange=" return ddlSearch_change();"--%>
                                                                <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">
                                                                <asp:Panel ID="pnltextbox" runat="server">
                                                                    <div id="divtxt" runat="server">
                                                                        <div class="label-dynamic">
                                                                             <sup>* </sup>
                                                                            <label>Search String</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                                                    </div>
                                                                </asp:Panel>

                                                                <asp:Panel ID="pnlDropdown" runat="server">
                                                                    <div id="divDropDown" runat="server">
                                                                        <div class="label-dynamic">
                                                                             <sup>* </sup>
                                                                            <%-- <label id="lblDropdown"></label>--%>
                                                                            <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>
                                                            <%--   <div class="col-12 btn-footer">--%>
                                                            <div class="form-group col-lg-3 col-12">
                                                                <div class="label-dynamic">
                                                                    <label></label>
                                                                </div>
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" />
                                                                <%--                                       <asp:Button ID="btnClose" runat="server" Text="Clear Search" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />--%>
                                                                <asp:Button ID="btnCancel" runat="server" Text="Clear" CausesValidation="false"
                                                                    CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="4" />

                                                                <asp:ValidationSummary ID="valSummery1" DisplayMode="List" runat="server" ShowMessageBox="true"
                                                                    ShowSummary="false" ValidationGroup="submit" />



                                                            </div>



                                                        </div>
                                                        <div id="divfooter" runat="server" visible="false">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Payment for Semester</label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlSemester" AppendDataBoundItems="true" runat="server" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true"
                                                                        CssClass="form-control" Enabled="true" TabIndex="2">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <%# Eval("DD_NO") %>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSemester"
                                                                        Display="None" InitialValue="0" ErrorMessage="Please select semester" SetFocusOnError="true"
                                                                        ValidationGroup="studSearch" />
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Select Receipt Type</label>
                                                                    </div>
                                                                    <%--  OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged" AutoPostBack="true"--%>
                                                                    <asp:DropDownList ID="ddlReceiptType" runat="server" CssClass="form-control" TabIndex="3" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged" AutoPostBack="true" />
                                                                </div>
                                                            </div>
                                                        </div>


                                                    </div>

                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                </div>

                                <div class="col-12">
                                   <asp:Panel ID="pnlLV" runat="server">
                                                    <asp:ListView ID="lvStudent" runat="server">
                                                        <LayoutTemplate>
                                                            <div id="listViewGrid" class="vista-grid">
                                                                <div class="sub-heading">
                                                                    <h5>Student List</h5>
                                                                </div>
                                                                <asp:Panel ID="Panel2" runat="server">
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Name
                                                                                </th>
                                                                                <th>IdNo
                                                                                </th>
                                                                                <th><asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th><asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th><asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>Father Name
                                                                                </th>
                                                                                <th>Mother Name
                                                                                </th>
                                                                                <th>Mobile No.
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </asp:Panel>
                                                            </div>

                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                        OnClick="lnkId_Click"></asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("idno")%>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <%# Eval("SEMESTERNO")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("FATHERNAME") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("MOTHERNAME") %>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("STUDENTMOBILE") %>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                                </div>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                            OnClick="lnkId_Click"></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <%# Eval("idno")%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstudsemester" runat="server" Text='<%# Eval("SEMESTERNAME")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("FATHERNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("MOTHERNAME") %>
                                                    </td>
                                                    <td>
                                                        <%#Eval("STUDENTMOBILE") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <%--Search Pannel End--%>
                                <div class="col-12">

                                    <div class="box-footer">
                                        <p class="text-center">
                                        </p>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hdnID" runat="server" />


                                <div class="col-12 mb-3">
                                    <asp:ListView ID="lvFeeCollectionModes" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div id="divlvFeeCollectionModes">
                                                <div class="sub-heading">
                                                    <h5>Available Modes of Fee Collection</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Modes
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
                                                    <asp:HyperLink ID="hlkFeeCollection" runat="server" Text='<%# Eval("LINK_CAPTION") %>'
                                                        NavigateUrl='<%# Eval("LINK_URL") + "&RecTitle=" + ddlReceiptType.SelectedItem.Text + "&RecType=" + ddlReceiptType.SelectedValue + "&id=" + hdnID.Value+ "&semesterno=" + ddlSemester.SelectedValue %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
    <br />
    <%--Search Box Script Start--%>
    <script type="text/javascript" lang="javascript">

        $(document).ready(function () {
            debugger
            $("#<%= divpanel.ClientID %>").css("display", "none");
            $("#<%= pnltextbox.ClientID %>").css("display", "none");
            $("#<%= pnlDropdown.ClientID %>").css("display", "none");
        });
        function submitPopup(btnsearch) {

            debugger
            var rbText;
            var searchtxt;

            var e = document.getElementById("<%=ddlSearch.ClientID%>");
            var rbText = e.options[e.selectedIndex].text;
            var ddlname = e.options[e.selectedIndex].text;
            if (rbText == "Please Select") {
                alert('Please select Criteria as you want search...')
            }

            else {


                if (rbText == "ddl") {
                    var skillsSelect = document.getElementById("<%=ddlDropdown.ClientID%>").value;

                    var searchtxt = skillsSelect;
                    if (searchtxt == "0") {
                        alert('Please Select ' + ddlname + '..!');
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        return true;
                        $("#<%= pnltextbox.ClientID %>").hide();

                    }
                }
                else if (rbText == "BRANCH") {

                    if (searchtxt == "Please Select") {
                        alert('Please Select Branch..!');

                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);

                        return true;
                    }

                }
                else {
                    searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;
                    if (searchtxt == "" || searchtxt == null) {
                        alert('Please Enter Data you want to search..');
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        //$("#<%= divpanel.ClientID %>").hide();
                        //$("#<%= pnltextbox.ClientID %>").show();

                        return true;
                    }
                }
        }
    }

    function ClearSearchBox(btncancelmodal) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btncancelmodal, '');
        return true;
    }
    function CloseSearchBox(btnClose) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btnClose, '');
        return true;
    }




    function Validate() {

        debugger

        var rbText;

        var e = document.getElementById("<%=ddlSearch.ClientID%>");
        var rbText = e.options[e.selectedIndex].text;

        if (rbText == "IDNO" || rbText == "Mobile") {

            var char = (event.which) ? event.which : event.keyCode;
            if (char >= 48 && char <= 57) {
                return true;
            }
            else {
                return false;
            }
        }
        else if (rbText == "NAME") {

            var char = (event.which) ? event.which : event.keyCode;

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122)) {
                return true;
            }
            else {
                return false;
            }

        }
    }


    </script>
    <%--Search Box Script End--%>
</asp:Content>
