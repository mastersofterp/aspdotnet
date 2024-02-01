<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Assign_Doc_Category.aspx.cs" Inherits="DOCUMENTANDSCANNING_DCMNTSCN_Assign_Doc_Category" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">ASSIGN CATEGORY</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnl" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Select Criteria to Assign Category</h5>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>User Type  </label>
                                    </div>
                                    <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True"
                                        ValidationGroup="show" ToolTip="Select User Type" TabIndex="1">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvUserType" runat="server" ValidationGroup="show"
                                        ErrorMessage="Please Select User Type" ControlToValidate="ddlUserType" Display="None"
                                        InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="show"
                                OnClick="btnShow_Click" ToolTip="Click here to Show User Type" TabIndex="2" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" CausesValidation="false"
                                OnClick="btnCancel_Click" ToolTip="Click here to Reset" TabIndex="3" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="show" />
                            <asp:HiddenField ID="hdfTot" runat="server" Value="0" />

                        </div>

                    </asp:Panel>
                </div>
                <div class="col-12">
                    <label>Assign Category</label>
                    <asp:Panel ID="pnlTree" runat="server" ScrollBars="Auto" Height="100px">
                        <asp:TreeView ID="tv" OnTreeNodePopulate="pp" runat="server"
                            ShowCheckBoxes="All" TabIndex="7">
                        </asp:TreeView>
                    </asp:Panel>
                </div>
                <div class="col-12">
                    <asp:Panel ID="pnlListMain" runat="server" Visible="false">
                        <div class="sub-heading">
                            <h5>Select UserName</h5>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnAssign" runat="server" Text="Assign Category" CssClass="btn btn-primary"
                                OnClick="btnAssign_Click" ToolTip="Click here to Assign Category" TabIndex="4" />
                        </div>
                        <div class="col-12">
                            <asp:Panel ID="pnlAssigList" runat="server" ScrollBars="Auto" Height="300px">
                                <asp:ListView ID="lvUsers" runat="server">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <div class="sub-heading">
                                                <h5>User List </h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="chkHead" runat="server" Checked="true" TabIndex="5"
                                                                onclick="totAllSubjects(this)" ToolTip="Click to Select All" />
                                                        </th>
                                                        <th>UserName
                                                        </th>
                                                        <th>Name
                                                        </th>
                                                        <th>ASSIGN CATEGORY
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
                                                <asp:CheckBox ID="chkAccept" runat="server" Checked="true" TabIndex="6"
                                                    ToolTip='<%# Eval("UA_NO")%>' />
                                            </td>
                                            <td>
                                                <%# Eval("UA_NAME") %>
                                            </td>
                                            <td>
                                                <%# Eval("UA_FULLNAME") %>
                                            </td>
                                            <td>
                                                <%#Eval("CATEGORYNAMES") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>

                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    </div>


        
   
                            

    <script language="javascript" type="text/javascript">
        function totAllSubjects(headchk) {
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (e.name.endsWith('chkAccept')) {
                        if (headchk.checked == true) {
                            e.checked = true;
                            hdfTot.value = Number(hdfTot.value) + 1;
                        }
                        else
                            e.checked = false;

                    }
                }
            }

            if (headchk.checked == false) hdfTot.value = "0";
        }

        function validateAssign() {
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>').value;

            if (hdfTot == 0) {
                alert('Please Select Atleast One User from User List');
                return false;
            }
            else
                return true;
        }
    </script>

    <script language="javascript" type="text/javascript">
        function OnTreeClick(evt) {

            var src = window.event != window.undefined ? window.event.srcElement : evt.target;
            //alert(src);
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
            return true;// It was 'return false', change done by shrikant to let parent checked after all childs are unchecked. 
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

    <div id="divMsg" runat="server"></div>
</asp:Content>

