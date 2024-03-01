<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="TP_Reg_Approval.aspx.cs" Inherits="TRAININGANDPLACEMENT_Transactions_TP_Reg_Approval"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updRegApp"
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
    </div>--%>

    <%-- <asp:UpdatePanel ID="updRegApp" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">STUDENT REGISTRATION APPROVAL</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">

                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divColg">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>College</label>
                                </div>
                                <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                    CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                    SetFocusOnError="true" InitialValue="0" Display="None" ErrorMessage="Plese Select College"
                                    ValidationGroup="Report"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Degree</label>
                                </div>
                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                    CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:RegularExpressionValidator runat="server" ID="rfvdegree" ControlToValidate="ddlDegree" ErrorMessage="Please Select Degree" SetFocusOnError="true" Display="None"></asp:RegularExpressionValidator>--%>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Branch</label>
                                </div>
                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="Show" runat="server" Text="Show" OnClick="btnShow_Click" class="btn btn-primary" CausesValidation="false" />

                        <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary" CausesValidation="false" />

                        <asp:Button ID="Cancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" class="btn btn-warning" CausesValidation="false" />

                    </div>


                    <div class="col-12" id="pnllist" runat="server">
                        <div class="sub-heading">
                            <h5>List of Students</h5>
                        </div>
                        <asp:ListView ID="lvStudent" runat="server"
                            OnSelectedIndexChanged="lvStudent_SelectedIndexChanged">
                            <EmptyDataTemplate>
                                <br />
                                <asp:Label ID="lblErr" runat="server" Text="No More Students to approve">
                                </asp:Label>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <div class="vista-grid">

                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Preview
                                                </th>
                                                <th>
                                                    <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" />
                                                </th>
                                                <th>Reg. No.
                                                </th>
                                                <th>Name
                                                </th>
                                                <th>Student Type
                                                </th>
                                                <th>Email ID
                                                </th>
                                                <th>SSC
                                                </th>
                                                <th>HSC
                                                </th>
                                                <th>Contact No
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
                                        <%-- <asp:Button runat="server" ID="btnPreview" Text="Preview"  CommandArgument='<%# Eval("FILENAME") %>' Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>'
                                                  ToolTip='<%# Eval("FILENAME") %>'  OnClick="btnPreview_Click" CssClass="btn btn-primary btn-sm" CausesValidation="false" /> --%>
                                        <%-- CommandArgument='<%# Eval("ENROLLMENTNO") %>' CommandName='<%# Eval("IDNO") %>'--%>
                                        <asp:ImageButton ID="btnPreview" runat="server" OnClick="btnPreview_Click" Text="Preview" ImageUrl="~/Images/view.png" ToolTip='<%# Eval("FILENAME") %>'
                                            CommandArgument='<%# Eval("FILENAME") %>' Visible="false"></asp:ImageButton> <%-- Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>'--%>

                                        <%-- <asp:ImageButton ID="btncareer" runat="server" OnClick="btncareer_Click" Text="Preview" ImageUrl="~/Images/view.png" ToolTip='<%# Eval("FILENAME") %>'
                                                                 CommandArgument='<%# Eval("FILENAME") %>' Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>'></asp:ImageButton>--%>
                                      <%--  <asp:LinkButton ID="btnAddCompany" runat="server" CssClass="btn btn-outline-info" data-toggle="modal" data-target="#Request_Modal">Add Company</asp:LinkButton>--%>
                                        <asp:Button ID="btnStatus" runat="server" Text="Student Profile" ToolTip="Check Transaction Status" CssClass="btn btn-primary" OnClick="btnStatus_Click"  TabIndex="9" />  <%--OnClientClick="SHOPOPUP(this);"--%>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                          <asp:HiddenField ID="hdidno" runat="server" Value='<%# Eval("IDNO")%>' />
                                    </td>
                                    <td>
                                        <%# Eval("REGNO") %>
                                        <asp:HiddenField ID="txtRegNo" runat="server" Value='<%# Eval("REGNO") %>' />
                                    </td>
                                    <td>
                                        <%# Eval("STUDNAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("STUDENT_TYPE")%>
                                    </td>
                                    <td>
                                        <%# Eval("EMAILID")%>
                                    </td>
                                    <td>
                                        <%# Eval("SSC")%>
                                    </td>
                                    <td>
                                        <%# Eval("HSC")%>
                                    </td>

                                    <td>
                                        <%# Eval("CONTACT_NOS")%>
                                    </td>
                                </tr>
                            </ItemTemplate>

                        </asp:ListView>
                    </div>

                    <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                        <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                        <asp:HiddenField ID="hdnBlobCon" runat="server" />
                        <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                        <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                    </div>
                </div>
            </div>
        </div>

        <%--  --------------------start----------24-02-2024--%>

          <div class="col-md-6">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup"  align="center" Style="display: none; width: 90%; height: 90%">
                                                <div class="form-group">
                                                    <div class="col-sm-12" style="text-align: right">

                                                        <asp:ImageButton ID="hlClose" runat="server" AlternateText="Close" ImageUrl="~/Images/cancel.gif"></asp:ImageButton>
                                                    </div>
                                                </div>
                                                <asp:Label ID="lblpopup" CssClass="control-label" runat="server"></asp:Label>
                                                <div class="col-md-12">
                                                    <div class="form-horizontal">
                                                       
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <ajaxtoolkit:ModalPopupExtender ID="mdlopenPage" runat="server" PopupControlID="pnlPopUp" TargetControlID="lblpopup"
                                                BackgroundCssClass="modalBackground">
                                            </ajaxtoolkit:ModalPopupExtender>
                                        </div>
                                    </div>
                                </div>
         <iframe id="ViewForm" frameborder="0" runat="server" style="width: 100%; height: 530px"></iframe>
        <%-- --------------------------end----------24-02-2024--%>
    </div>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>

    <script>
        //  view More Modal
        //function MoreDistUserModal() {
        //    var xhttp = new XMLHttpRequest();
        //    xhttp.onreadystatechange = function () {
        //        if (this.readyState === 1) {
        //            document.getElementById("dynamicModal").innerHTML = "Please Wait.....";
        //        } else if (this.readyState === 4 && this.status === 200) {
        //            document.getElementById("dynamicModal").innerHTML =
        //            this.responseText;
        //        }
        //    };
        //    xhttp.open("GET", "../PresentationLayer/TRAININGANDPLACEMENT/Career_Profile.aspx", true)
        //    xhttp.send();

        //}


        function SHOPOPUP(vall) {
            debugger;
            var myArr = new Array();
            myString = "" + vall.id + "";
            myArr = myString.split("_");
            var index = myArr[0] + '_' + myArr[1] + '_' + myArr[2] + '_' + myArr[3] + '_' + 'hdBillNo';
            var Id = document.getElementById(index).value;
            //var Id = document.getElementById(index).innerText;
            var popUrl = '/PresentationLayer/TRAININGANDPLACEMENT/Transactions/TP_Career_Profile.aspx?obj=' + Id;
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=no,' +
                                  'status=no,toolbar=no,titlebar=no,' +
                                  'left=50,top=35,width=900px,height=650px';
            var openWindow = window.open(popUrl, name, appearence);
            //var openWindow = window.showModalDialog(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }


        

    </script>

    <script type="text/javascript">

        function openNewWin(url) {

            var x = window.open(url, 'mynewwin', 'width=950,height=600,toolbar=1');

            x.focus();

        }


        function totAllSubjects(headchk) {

            var frm = document.forms[0]

            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];

                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }


        }
    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
