<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Link_user.aspx.cs" Inherits="Link_user" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlUser"
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

    <%--<script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title">USER LINK ASSIGN</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <asp:UpdatePanel ID="updpnlUser" runat="server">
                        <ContentTemplate>
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-8 col-12 col-md-12">
                                        <div class="sub-heading">
                                            <h5>User Details</h5>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>User Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlUserType" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged" TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="req_usertype" runat="server" ControlToValidate="ddlUserType"
                                                    ErrorMessage="User Type Required" Display="None" InitialValue="0" ValidationGroup="submit" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlUserType"
                                                    Display="None" ErrorMessage="Please Select User Type" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="SubmitGroup"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>User ID</label>
                                                </div>
                                                <asp:TextBox ID="txtUserID" runat="server" MaxLength="10" CssClass="form-control" TabIndex="2" />&nbsp;
                                                <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                                                <asp:RequiredFieldValidator ID="req_userid" runat="server" ErrorMessage="User ID Required"
                                                    ControlToValidate="txtUserID" Display="None" ValidationGroup="CheckID" />
                                                <asp:ValidationSummary ID="val_summary" runat="server" ShowMessageBox="True" ShowSummary="False"
                                                    DisplayMode="List" ValidationGroup="CheckID" />
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12" id="trDept" visible="false" runat="server">
                                                <div class="label-dynamic">
                                                    <label>Department (Acd)</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="3">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <%--  <asp:RequiredFieldValidator ID="rfvddlDept" runat="server" ControlToValidate="ddlDept"
                                                ErrorMessage="Department Required !" InitialValue="0" Display="None" ValidationGroup="submit">
                                            </asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12" id="trSubDept" visible="false" runat="server">
                                                <div class="label-dynamic">
                                                    <label>Department (Pay)</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSubDept" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSubDept_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true" CssClass="form-control" TabIndex="4">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Designation</label>
                                                </div>
                                                <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" Enabled="false" MaxLength="50" Style="text-transform: uppercase" TabIndex="5" />
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Full Name</label>
                                                </div>
                                                <asp:TextBox ID="txtFName" runat="server" CssClass="form-control" MaxLength="50" Enabled="false" Style="text-transform: uppercase" TabIndex="6" />
                                                <asp:RequiredFieldValidator ID="rfvFullName" runat="server" ControlToValidate="txtFName"
                                                    ErrorMessage="Full Name Required !" Display="None" ValidationGroup="submit">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Username</label>
                                                </div>
                                                <asp:TextBox ID="txtUsername" runat="server" MaxLength="20" Enabled="false" ValidationGroup="SubmitGroup" CssClass="form-control" TabIndex="7" />
                                                <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUsername"
                                                    ErrorMessage="Username Required !" Display="None" ValidationGroup="submit">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Email</label>
                                                </div>
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Enabled="false" MaxLength="50" TabIndex="8" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please Enter Valid Email Id"
                                                    ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ValidationGroup="submit"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail"
                                                    ErrorMessage="Email Id Required !" Display="None" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12" style="display: none;">
                                                <div class="label-dynamic">
                                                    <label>Status</label>
                                                </div>
                                                <asp:UpdatePanel ID="Updpnldetails" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:CheckBox ID="chkActive" runat="server" Text="Active" AutoPostBack="True" Font-Bold="True"
                                                            OnCheckedChanged="chkActive_CheckedChanged" ForeColor="#005500" Checked="True" TabIndex="9" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                            
                                                    <asp:CheckBox ID="chkDEC" runat="server" Text="D.E.C" TextAlign="Left" TabIndex="10" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="chkActive" EventName="CheckedChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>

                                        <div class="col-12 pl-0 pr-0" id="pnlStudent" runat="server" visible="false">
                                            <div class="row">
                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>College</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" data-select2-enable="true" CssClass="form-control" AutoPostBack="true" TabIndex="11">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Degree</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="12"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Branch</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="13">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Semester</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSemester" runat="server" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" TabIndex="14">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit" TabIndex="16"
                                                OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                                            <asp:Button ID="btnReset" runat="server" Text="Cancel" ValidationGroup="SubmitGroup" TabIndex="17"
                                                OnClick="btnReset_Click" CausesValidation="False" CssClass="btn btn-warning" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                ShowSummary="False" DisplayMode="List" ValidationGroup="submit" />
                                        </div>
                                    </div>

                                    <div class="col-lg-4 col-md-12 col-12">
                                        <div class="sub-heading">
                                            <h5>Access Link Domain </h5>
                                            <span style="font-size: small; color: green;">(Note :For new user Please check Access Domain(s) then click on edit button of user)</span>
                                        </div>
                                        <asp:CheckBoxList ID="chkListAccLink" Enabled="false" runat="server" OnSelectedIndexChanged="chkListAccLink_SelectedIndexChanged" AutoPostBack="true" RepeatColumns="2" TabIndex="15"
                                            RepeatDirection="Horizontal" Width="100%">
                                        </asp:CheckBoxList>
                                    </div>

                                    <div class="col-lg-8 col-md-12 col-12">
                                        <div class="btn-foter">
                                            <asp:Label ID="lblSubmitStatus" runat="server" SkinID="lblmsg" />
                                            <div id="divMsg" runat="server">
                                            </div>
                                            <asp:Label ID="lblEmpty" Font-Bold="true" Text="No Record!" Visible="false" runat="server"></asp:Label>
                                        </div>

                                        <table class="table table-striped table-bordered border-top-0 nowrap display" style="width: 100%" id="tbluser1">
                                            <asp:Repeater ID="lvlinks" runat="server">
                                                <HeaderTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Users Account Info List</h5>
                                                    </div>
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th style="text-align: center;">Action
                                                            </th>
                                                            <th>Name
                                                            </th>
                                                            <th>Login Name
                                                            </th>
                                                            <th>Type
                                                            </th>
                                                            <th>Status
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <%-- <tr id="itemPlaceholder" runat="server" />--%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <tr>
                                                                <td style="text-align: center;">
                                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit1.png" CommandArgument='<%# Eval("UA_NO") %>'
                                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="18" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblUser" runat="server" Text='<%# Eval("UA_FULLNAME")%>' ToolTip='<%# Eval("UA_NO")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("UA_NAME")%>
                                                                </td>

                                                                <td>
                                                                    <%# Eval("USERDESC")%>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblUStatus" Style="font-size: 9pt;" runat="server" Text='<%# Eval("UA_STATUS")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <%--<asp:PostBackTrigger ControlID="btnEdit" />--%>
                                                        </Triggers>

                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>

                                    <div class="col-lg-4 col-md-12 col-12">
                                        <div class="sub-heading">
                                            <h5>Assign Links</h5>
                                            <span style="font-size: small; color: green;">(Note :The Links in Gray colour/Disabled are assigned from User Role System and not editable)</span>
                                        </div>
                                        <asp:UpdatePanel ID="Upd_treepnl" runat="server" UpdateMode="Always">
                                            <ContentTemplate>
                                                <asp:Panel ID="pnlTree" runat="server" ScrollBars="Auto" Height="300px">
                                                    <asp:TreeView ID="tvLinks" runat="server" ExpandDepth="0">
                                                    </asp:TreeView>
                                                </asp:Panel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <%--<asp:AsyncPostBackTrigger ControlID="chkListAccLink" EventName="SelectedIndexChanged" />--%>
                                                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <%--  <asp:PostBackTrigger ControlID="btnSubmit" />
                            <asp:PostBackTrigger ControlID="ddlDept" />
                            <asp:PostBackTrigger ControlID="chkListAccLink" />
                            <asp:PostBackTrigger ControlID="ddlSemester" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <%--<div class="box-footer">
                     <div id="divdemo2" style="display: none;" class="col-md-12">
                        <asp:UpdatePanel ID="updEdit" runat="server">
                            <ContentTemplate>
                                <div class="col-md-4">
                                    <label>Search Criteria</label>
                                    <asp:RadioButton ID="rbName" runat="server" Text="Name" GroupName="edit" Checked="true" />
                                    <asp:RadioButton ID="rbIdNo" runat="server" Text="IdNo" GroupName="edit" />
                                    <asp:RadioButton ID="rbUserName" runat="server" Text="User Name" GroupName="edit" />
                                    <asp:RadioButton ID="rbUserType" runat="server" Text="User Type" GroupName="edit" />
                                </div>
                                <div class="col-md-4">
                                    <label>Search String </label>
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-12">
                                    <p class="text-center">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="return submitPopup(this.name);" />
                                        <asp:Button ID="btnCancelModal" runat="server" Text="Cancel" OnClientClick="return ClearSearchBox(this.name)" />
                                    </p>
                                </div>
                                <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />

                                <asp:ListView ID="lvUsers" runat="server">
                                    <LayoutTemplate>
                                        <div class="vista-grid">

                                            <h3>Login Details </h3>

                                            <table class="table table-hover table-bordered">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Name
                                                        </th>
                                                        <th>IDNo
                                                        </th>
                                                        <th>UserName
                                                        </th>
                                                        <th>User Type
                                                        </th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </div>
                                        <div class="listview-container">
                                            <div id="demo-grid">
                                                <table class="table table-hover table-bordered">
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                            <td>
                                                <asp:LinkButton ID="lnkID" runat="server" Text='<%# Eval("UA_FULLNAME") %>' ToolTip='<%# Eval("UA_NO") %>'
                                                    OnClientClick="return lnkIDClick(this);"></asp:LinkButton>
                                            </td>
                                            <td>
                                                <%# Eval("UA_NO")%>
                                            </td>
                                            <td>
                                                <%# Eval("UA_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("UA_TYPE")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>--%>
            </div>
        </div>
    </div>

    <script language="javascript" type="text/javascript">
        function lnkIDClick(lnkID) {
            __doPostBack(lnkID.id, lnkID.title);
            __doPostBack(lnkID.id, lnkID.title);
            __doPostBack(lnkID.id, lnkID.title);
            alert('User ID : ' + lnkID.title);
            return true;
        }
    </script>
    <script type="text/javascript">
        function DisableButtons() {
            var inputs = document.getElementsByTagName("INPUT");
            for (var i in inputs) {
                if (inputs[i].type == "button" || inputs[i].type == "submit") {
                    inputs[i].disabled = true;
                }
            }
        }
        window.onbeforeunload = DisableButtons;
    </script>
    <script language="javascript" type="text/javascript">
        function OnTreeClick(evt) {

            var src = window.event != window.undefined ? window.event.srcElement : evt.target;
            // alert(src);
            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
            if (isChkBoxClick) {
                var parentTable = GetParentByTagName("table", src);
                var nxtSibling = parentTable.nextSibling;
                if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
                {
                    if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                    {
                        //check or uncheck children at all levels
                        CheckUncheckChildren(parentTable.nextSibling, src.checked);
                    }
                }
                //check or uncheck parents at all levels
                CheckUncheckParents(src, src.checked);
            }
        }

        function CheckUncheckChildren(childContainer, check) {
            var childChkBoxes = childContainer.getElementsByTagName("input");
            var childChkBoxCount = childChkBoxes.length;
            for (var i = 0; i < childChkBoxCount; i++) {
                childChkBoxes[i].checked = check;
            }
        }

        function CheckUncheckParents(srcChild, check) {
            var parentDiv = GetParentByTagName("div", srcChild);
            var parentNodeTable = parentDiv.previousSibling;

            if (parentNodeTable) {
                var checkUncheckSwitch;

                if (check) //checkbox checked
                {
                    var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);

                    if (isAllSiblingsChecked)
                        checkUncheckSwitch = true;
                    else
                        return; //do not need to check parent if any(one or more) child not checked
                }
                else //checkbox unchecked
                {
                    //checkUncheckSwitch = false;

                    //# Prashant
                    var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);

                    if (isAllSiblingsChecked)
                        checkUncheckSwitch = true;
                    else
                        checkUncheckSwitch = false;

                }

                var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                if (inpElemsInParentTable.length > 0) {
                    var parentNodeChkBox = inpElemsInParentTable[0];
                    parentNodeChkBox.checked = checkUncheckSwitch;
                    //do the same recursively
                    CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                }
            }
        }

        function AreAllSiblingsChecked(chkBox) {
            var parentDiv = GetParentByTagName("div", chkBox);
            var childCount = parentDiv.childNodes.length;
            for (var i = 0; i < childCount; i++) {
                if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
                {
                    if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                        var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                        //if any of sibling nodes are not checked, return false
                        if (prevChkBox.checked) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //utility function to get the container of an element by tagname
        function GetParentByTagName(parentTagName, childElementObj) {
            var parent = childElementObj.parentNode;
            while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
                parent = parent.parentNode;
            }
            return parent;
        }

    </script>




    <script language="javascript" type="text/javascript">
        //Added  JS Script block by Arjun on Date :27012023 for Disabled Default Role Links

        //if ( $(this).hasClass("age") ) {
        //$(this).addClass("tr-disable").find("td :checkbox").prop("checked", false);
        //}

        // Called via a startup script created in Code Behind.
        // Disables all treeview checkboxes that have a text with a class=disabledTreeviewNode.
        // treeviewID is the ClientID of the treeView
        function DisableCheckBoxes() {

            ////var treeObj = document.getElementById("ctl00_ContentPlaceHolder1_tvLinks").ej2_instances[0];
            ////treeObj.disableNodes(document.getElementsByClassName("disableDefault"));

            //$("span").closest("input").css({ "color": "red", "border": "2px solid red" });
            ////var name = $("#ctl00_ContentPlaceHolder1_tvLinks").closest('tr').find('.disable_default').text();
            //alert(name);

            $('span.disable_default').closest('td').find('input:checkbox').prop('disabled', true);
            ////$(".disableDefault").closest('td').prev().find('[type=checkbox]').prop('disabled', true);
            //jQuery(this).closest('td').prev().find('[type=checkbox]').prop('checked', true);
            ////$('td a span .disableDefault').find("input, a").prop("disabled", true);

            //var rows = 
            //alert(JSON.stringify(rows));

            // $("div span:first-child")

            //, select, button, textarea 
            ///$("#ctl00_ContentPlaceHolder1_tvLinks").find("input, a").prop("disabled", true);//.removeAttr('href').removeAttr('onclick');

            ////$("#ctl00_ContentPlaceHolder1_tvLinks").find("input, a").prop("disabled", true);

            ////if ($("#ctl00_ContentPlaceHolder1_tvLinks").hasClass("disableDefault")) {



            //$(this).addClass("tr-disable").find("td :checkbox").prop("disabled", true);


            ////$("#ctl00_ContentPlaceHolder1_tvLinks").find("td :input").prop("disabled", true);
            //prop("checked", false);
            ////}

        }


    </script>

    <%--<script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#tbluser1').DataTable({

            });
        }
    </script>--%>
</asp:Content>

