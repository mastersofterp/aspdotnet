<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentFeeDetails.aspx.cs" Inherits="FEESCOLLECTION_Transaction_FeeDetails" Title="" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_divFeeReceipts .dataTables_scrollHeadInner,
        #ctl00_ContentPlaceHolder1_lvStudent_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <%-- <h3 class="box-title">FEE DEMAND MODIFICATION</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
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
                                                            <%-- <asp:ListItem>Please Select</asp:ListItem>
                                                        <asp:ListItem>BRANCH</asp:ListItem>
                                                        <asp:ListItem>ENROLLMENT NUMBER</asp:ListItem>
                                                        <asp:ListItem>REGISTRATION NUMBER</asp:ListItem>
                                                        <asp:ListItem>FatherName</asp:ListItem>
                                                        <asp:ListItem>IDNO</asp:ListItem>
                                                        <asp:ListItem>MOBILE NUMBER</asp:ListItem>
                                                        <asp:ListItem>MotherName</asp:ListItem>
                                                        <asp:ListItem>NAME</asp:ListItem>
                                                        <asp:ListItem>ROLLNO</asp:ListItem>
                                                        <asp:ListItem>SEMESTER</asp:ListItem>--%>
                                                        </asp:DropDownList>

                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">


                                                        <asp:Panel ID="pnltextbox" runat="server">
                                                            <div id="divtxt" runat="server" style="display: block">
                                                                <div class="label-dynamic">
                                                                    <label>Search String</label>
                                                                </div>
                                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                                            </div>
                                                        </asp:Panel>

                                                        <asp:Panel ID="pnlDropdown" runat="server">
                                                            <div id="divDropDown" runat="server" style="display: block">
                                                                <div class="label-dynamic">
                                                                    <%-- <label id="lblDropdown"></label>--%>
                                                                    <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                                </asp:DropDownList>
                                                            </div>
                                                        </asp:Panel>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <%-- OnClientClick="return submitPopup(this.name);"--%>
                                                <asp:Button ID="Button1" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" OnClientClick="return submitPopup(this.name);" />
                                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                            </div>

                                            <div class="col-12">
                                                <asp:Panel ID="pnlLV" runat="server">
                                                    <asp:ListView ID="lvStudent" runat="server">
                                                        <LayoutTemplate>
                                                            <div>
                                                                <div class="sub-heading">
                                                                    <h5>Student List</h5>
                                                                </div>
                                                                <asp:Panel ID="Panel2" runat="server">
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Name
                                                                                </th>
                                                                                <th >IdNo
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
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
                                                                    <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CausesValidation="false"  CommandArgument='<%# Eval("IDNo") %>'
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
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <%--Search Pannel End--%>
                            </div>
                        </div>




                        <div>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="col-md-12" style="display: block;" runat="server" id="divStudInfo" visible="false">

                                    <div class="label-dynamic">
                                        <div class="sub-heading">
                                            <h5>STUDENT INFORMATION</h5>
                                        </div>
                                    </div>
                                   
                                    <div class="row">
                                        <div class="col-md-4">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Student Name :</b><a class="pull-right">
                                                    <asp:Label ID="lblStudName" runat="server" Font-Bold="True"></asp:Label></a> </li>
                                                <li class="list-group-item"><b>Roll No. :</b><a class="pull-right">
                                                    <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label></a> </li>
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                    :</b><a class="pull-right">
                                                        <asp:Label ID="lblEnroll" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                    :</b><a class="pull-right">
                                                        <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                    :</b><a class="pull-right">
                                                        <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a> </li>

                                            </ul>
                                        </div>
                                        <div class="col-md-4">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Date Of Admission:</b><a class="pull-right">
                                                    <asp:Label ID="lblDateOfAdm" runat="server" Font-Bold="True"></asp:Label></a> </li>
                                                <li class="list-group-item"><b>Year:</b><a class="pull-right">
                                                    <asp:Label ID="lblYear" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Payment Type:</b><a class="pull-right">
                                                    <asp:Label ID="lblPaymentType" runat="server" Font-Bold="True"></asp:Label></a> </li>
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                    :</b><a class="pull-right">
                                                        <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                    :</b><a class="pull-right">
                                                        <asp:Label ID="lblBatch" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-md-4">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Date of Birth :</b><a class="pull-right">
                                                    <asp:Label ID="lblDOB" runat="server" Font-Bold="True"></asp:Label></a> </li>
                                                <li class="list-group-item"><b>Gender:</b><a class="pull-right">
                                                    <asp:Label ID="lblSex" runat="server" Font-Bold="True"></asp:Label></a> </li>
                                                <li class="list-group-item"><b>Category:</b><a class="pull-right">
                                                    <asp:Label ID="lblCategory" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Mobile No:</b><a class="pull-right">
                                                    <asp:Label ID="lblMobile" runat="server" Font-Bold="True"></asp:Label></a> </li>

                                            </ul>
                                    </div>
                                       
                                   </div>

                                </div>
                                    </div>
                           
                             <%--<div class="col-md-12 mt-3">
                            <div id="divFeesDetails" runat="server" visible="false">
                            </div>
                            <asp:ListView ID="lvFeesDetails" runat="server">
                                <LayoutTemplate>
                                    <div id="divlvFeesDetails">
                                        <div class="sub-heading mt-3">
                                            <h5>Previous Fees Details</h5>
                                        </div>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Semester</th>
                                                    <th>Applied
                                                    </th>
                                                    <th>Paid
                                                    </th>
                                                    <th>Balance
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
                                            <%# Eval("YEAR") %>
                                        </td>
                                        <td>
                                            <%# Eval("APPLIED")%>
                                        </td>
                                        <td>
                                            <%# Eval("PAID")%>
                                        </td>
                                        <td>
                                            <%# Eval("BALANCE")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                        </div>
                    --%>
                                  <div id="divFeeReceipts" runat="server" visible="false">
                                            <div class="col-12">
                                                 <div class="row">
                                                    <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>
                                      <img alt="Show/Hide" src="../images/action_down.gif" onclick="ShowHideDiv('ctl00_ContentPlaceHolder1_divHidPreviousReceipts', this);" style="display: none;" />
                                                    </h5>
                                                </div>
                                                        </div>
                                                        </div>
                                                <div id="divHidPreviousReceipts" runat="server" >
                                                    <%# Eval("DD_BANK")%>
                                                    <asp:Panel ID="Panel2" runat="server">
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <asp:Repeater ID="lvPaidReceipts" runat="server">
                                                                <HeaderTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Previous Receipts Information</h5>
                                                                    </div>

                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                              <th>Rec. Date
                                                                </th>
                                                                <th>Rec. No.
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblDYddlSemester_Tab2" runat="server" Font-Bold="true"></asp:Label>
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                </th>
                                                                <th>Total
                                                                </th>
                                                                <th>Refund Amount
                                                                </th>
                                                                <th>Payment Mode
                                                                </th>
                                                                <th>Receipt Type
                                                                </th>
                                                                <th>Remark
                                                                </th>
                                                                <th>Print
                                                                </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <%--<tr id="itemPlaceholder" runat="server" />--%>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <%--  <tr class="item">--%>
                                                                         <td>
                                                     <%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %>
                                                   
                                                     
                                                    </td>
                                                    <td>
                                                        <%# Eval("REC_NO") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTERNO") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("BRANCH") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TOTAL_AMT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("EXCESS_AMOUNT") %>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <%# Eval("PAY_TYPE") %>
                                                    </td>
                                                    <td>
                                                        <%#Eval ("RECIEPT_TITLE") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REMARK")%>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnPrintReceipt" runat="server" OnClick="btnPrintReceipt_Click"
                                                            CommandArgument='<%# Eval("DCR_NO") %>' ImageUrl="~/Images/print.png" ToolTip='<%# Eval("RECIEPT_CODE")%>' CausesValidation="False" />
                                                    </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    </tbody>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </table>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                  </ContentTemplate>
                            </asp:UpdatePanel>
                        <div>
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
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


                    </div>
                </div>
            </div>
        </div>
    </div>



    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }
    </script>
    <script type="text/javascript" language="javascript">

        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../images/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../images/collapse_blue.jpg";
            }
        }

    </script>

    <%--Search Box Script Start--%>
    <script type="text/javascript" lang="javascript">

        $(document).ready(function () {
            debugger
            //$("#<%= pnltextbox.ClientID %>").hide();

            $("#<%= pnlDropdown.ClientID %>").hide();
        });
        function submitPopup(btnsearch) {

            debugger
            var rbText;
            var searchtxt;

            var e = document.getElementById("<%=ddlSearch.ClientID%>");
            var rbText = e.options[e.selectedIndex].text;
            var ddlname = e.options[e.selectedIndex].text;
            if (rbText == "Please Select") {
                alert('Please Select Search Criteria.')
                $(e).focus();
                return false;
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
                        //$("#<%= divpanel.ClientID %>").hide();
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
                        alert('Please Enter Data to Search.');
                        //$(searchtxt).focus();
                        document.getElementById('<%=txtSearch.ClientID %>').focus();
                        return false;
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

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122) || (char =32)) {
                return true;
            }
            else {
                return false;
            }

        }
    }





    </script>



    <script>
        function ShowHideDiv(divId, img) {
            try {
                alert(1);
              
                if (document.getElementById(divId) != null &&
                    document.getElementById(divId).style.display == 'none') {
                    document.getElementById(divId).style.display = 'block';
                    img.src = '../images/action_up.gif';
                }
                else {
                    document.getElementById(divId).style.display = 'none';
                    img.src = '../images/action_down.gif';
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }
    </script>
    <%--Search Box Script End--%>
       <script type="text/javascript">
           $(function () {
               $(':text').bind('keydown', function (e) {
                   //on keydown for all textboxes prevent from postback
                   if (e.target.className != "searchtextbox") {
                       if (e.keyCode == 13) { //if this is enter key
                           document.getElementById('<%=Button1.ClientID%>').click();
                        e.preventDefault();
                        return true;
                    }
                    else
                        return true;
                }
                else
                    return true;
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $(':text').bind('keydown', function (e) {
                    //on keydown for all textboxes
                    if (e.target.className != "searchtextbox") {
                        if (e.keyCode == 13) { //if this is enter key
                            document.getElementById('<%=Button1.ClientID%>').click();
                            e.preventDefault();
                            return true;
                        }
                        else
                            return true;
                    }
                    else
                        return true;
                });
            });

        });
    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

