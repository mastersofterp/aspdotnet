<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Company.aspx.cs" Inherits="TRAININGANDPLACEMENT_Masters_Company" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updCompany"
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

    <asp:UpdatePanel ID="updCompany" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">COMPANY REGISTRATION</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12" id="pnlAdd" runat="server">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Company Details</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Approved</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="Y">YES</asp:ListItem>
                                            <asp:ListItem Value="N">NO</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlStatus" runat="server" ControlToValidate="ddlStatus"
                                            Display="None" ErrorMessage="Please Select Approved/Not Approved.  " ValidationGroup="Company"
                                            SetFocusOnError="true" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Company Name</label>
                                        </div>
                                        <asp:TextBox ID="txtCompany" runat="server" MaxLength="100" AutoPostBack="true" OnTextChanged="txtCompany_TextChanged" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvCompany" runat="server" ControlToValidate="txtCompany"
                                            Display="None" ErrorMessage="Please Enter Company Name" ValidationGroup="Company"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:Label ID="lblExist" runat="server" Visible="false"></asp:Label>
                                        <span id="message"></span>
                                    </div>
                                    <%--onblur="CheckAvailability();"--%>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Short Name</label>
                                        </div>
                                        <asp:TextBox ID="txtShortname" runat="server" MaxLength="20" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvShortname" runat="server" ControlToValidate="txtShortname"
                                            Display="None" ErrorMessage="Please Enter Short Name" ValidationGroup="Company"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Category</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvCategory"  runat="server" ControlToValidate="ddlCategory"
                                                        Display="None" ErrorMessage="Please Select Category  " ValidationGroup="Company"
                                                        SetFocusOnError="true" InitialValue="0">
                                                    </asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Director Name</label>
                                        </div>
                                        <asp:TextBox ID="txtDirector" runat="server" MaxLength="50" CssClass="form-control" />

                                    </div>
                                </div>

                                <div class="row" id="Fieldset2" runat="server">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Company Address Details</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Company Address</label>
                                        </div>

                                        <asp:TextBox ID="txtCompAdd" runat="server" MaxLength="200" CssClass="form-control"
                                            TextMode="MultiLine" Rows="1" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>City </label>
                                        </div>
                                        <asp:DropDownList ID="ddlCity" runat="server" AppendDataBoundItems="true"
                                            CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Pincode </label>
                                        </div>
                                        <asp:TextBox ID="txtPincode" runat="server" MaxLength="6" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Phone No</label>
                                        </div>
                                        <asp:TextBox ID="txtPhoneNo" runat="server" MaxLength="12"
                                            onkeyup="validateNumeric(this);" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>FAX No</label>
                                        </div>
                                        <asp:TextBox ID="txtFaxNo" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Email Id</label>
                                        </div>
                                        <asp:TextBox ID="txtemailid" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regEmail"
                                            runat="server" ErrorMessage="Please Enter Valid Email ID"
                                            ValidationGroup="Company" ControlToValidate="txtemailid"
                                            CssClass="requiredFieldValidateStyle"
                                            ForeColor="Red"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                        </asp:RegularExpressionValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Web Site</label>
                                        </div>
                                        <asp:TextBox ID="txtWebSite" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-12" id="divto">
                                    <asp:ListView ID="lvTo" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Delete
                                                        </th>
                                                        <th>Branch Name
                                                        </th>
                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </thead>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("SrNo") %>'
                                                        ImageUrl="~/Images/delete.png" OnClick="btnDelete_Click" ToolTip="Delete Record" />
                                                </td>
                                                <td id="IOTRANNO" runat="server">
                                                    <%# Eval("BRANCHNAME") %>
                                                    <asp:HiddenField ID="hfBranchno" Value='<%# Eval("BRANCHNO")%>' runat="server" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>

                                <div class="row">
                                    <div class="col-12" id="Div2" runat="server">

                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Contact Person Details</h5>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Address </label>
                                                </div>
                                                <asp:TextBox ID="txtContAddress" runat="server" MaxLength="300" TextMode="MultiLine" Rows="1"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Name </label>
                                                </div>
                                                <asp:TextBox ID="txtContPerson" runat="server" MaxLength="50" class="form-control"></asp:TextBox>

                                                <%-- changes by sumit-- 14092019 (changes into ErrorMessage)--%>

                                                <asp:RequiredFieldValidator ID="rfvContPerson" runat="server" ControlToValidate="txtContPerson"
                                                    SetFocusOnError="true" ErrorMessage="Please Enter Contact Person Name" Display="None"
                                                    ValidationGroup="Company" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Designation </label>
                                                </div>
                                                <asp:TextBox ID="txtContDesignation" runat="server" MaxLength="50"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Contact No. </label>
                                                </div>
                                                <asp:TextBox ID="txtContPhone" onkeyup="validateNumeric(this);" runat="server" MaxLength="10"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Email </label>
                                                </div>
                                                <asp:TextBox ID="txtContMailId" runat="server" MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvContMailid" runat="server" ControlToValidate="txtContMailId"
                                                    Display="None" ErrorMessage="Please Enter Contact Person Mail ID" ValidationGroup="Company"
                                                    SetFocusOnError="true" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                    runat="server" ErrorMessage="Please Enter Valid Email ID"
                                                    ValidationGroup="Company" ControlToValidate="txtContMailId"
                                                    CssClass="requiredFieldValidateStyle"
                                                    ForeColor="Red"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12" style="display: none">
                                    <asp:ImageButton ID="imgbToCopyLocalAddress" runat="server" ImageUrl="~/Images/copy.png"
                                        OnClientClick="copyDetails(this)" ToolTip="Copy Contact Person Deatils" />
                                </div>

                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Other Information</label>
                                        </div>

                                        <asp:TextBox ID="txtOthInfo" runat="server" MaxLength="300" TextMode="MultiLine"
                                            class="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Remark</label>
                                        </div>
                                        <asp:TextBox ID="txtRemark" runat="server" MaxLength="300" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Company" OnClick="btnSave_Click"
                                        class="btn btn-primary" />
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click"
                                        class="btn btn-primary" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        OnClick="btnCancel_Click" class="btn btn-warning" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Company"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                    <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>

                                </div>
                            </div>

                            <div id="pnlList" runat="server">
                                <div class="btn-footer col-12">
                                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add New" CssClass="btn btn-primary"></asp:Button>

                                    <asp:Button ID="btnShowReport" runat="server" Text="Show Report" OnClick="btnShowReport_Click" CssClass="btn btn-outline-primary" />

                                    <asp:Button ID="btnExcel" runat="server" Text="Excel Report" ValidationGroup="Report"
                                        OnClick="btnExcel_Click" CssClass="btn btn-info" />
                                </div>

                                <div class="col-12">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <asp:RadioButtonList ID="radlStatus" runat="server" RepeatDirection="Horizontal" TextAlign="Right" class="form-check-input"
                                            OnSelectedIndexChanged="radlStatus_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="B" Selected="True">All</asp:ListItem>
                                            <%--<asp:ListItem Value="P">Pending</asp:ListItem>--%>
                                            <asp:ListItem Value="N">Pending</asp:ListItem>
                                            <asp:ListItem Value="A">Approved</asp:ListItem>
                                        </asp:RadioButtonList>
                                        </label>
                                    </div>
                                </div>

                                <div class="col-12 mt-5">
                                    <div class="sub-heading">
                                        <h5>COMPANY LIST</h5>
                                    </div>
                                    <asp:ListView ID="lvCompany" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Click Add New To register Company " />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div id="demo_grid">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action
                                                            </th>
                                                            <th>Company
                                                            </th>
                                                            <th>Category
                                                            </th>
                                                            <th>City
                                                            </th>
                                                            <th>Website
                                                            </th>
                                                            <th>Contact Person
                                                            </th>
                                                            <th>Ph. No.
                                                            </th>
                                                            <th>Email id
                                                            </th>
                                                            <th>Status
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("COMPID") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                <%-- <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("COMPID") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />--%>
                                                </td>
                                                <td>
                                                    <%# Eval("COMPNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CATNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CITY")%>
                                                </td>
                                                <%--<td style="width: 10%; word-wrap: break-word; word-break: break-all;">--%>
                                                <td>
                                                    <%# Eval("WEBSITE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CONTPERSON")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CONTPHONE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CONTMAILID")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STATUS")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr class="altitem">
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("COMPID") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                <%-- <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("COMPID") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />--%>
                                                </td>
                                                <td>
                                                    <%# Eval("COMPNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CATNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CITY")%>
                                                </td>
                                                <td>
                                                    <%# Eval("WEBSITE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CONTPERSON")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CONTPHONE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CONTMAILID")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STATUS")%>
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                    </asp:ListView>
                                    <div class="pager d-none" style="text-align: center; color: #AACFE4">
                                        <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvCompany" PageSize="10"
                                            OnPreRender="dpPager_PreRender">
                                            <Fields>
                                                <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                                <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
                                                <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                                            </Fields>
                                        </asp:DataPager>
                                        </>

                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>

    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                    </td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-primary" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-warning" />
                    </td>
                </tr>
            </table>
        </div>

    </asp:Panel>

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
            this._source = null;
            this._popup = null;
        }


    </script>

    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

    </script>

    <%--<script type="text/javascript">
    function CheckAvailability() {
        //alert('hi');
        var companyname = document.getElementById('ctl00_ContentPlaceHolder1_txtCompany').value;
        //alert(companyname);
        PageMethods.CheckCompanyName(companyname, function (response) {
            
            var message = document.getElementById("message");
            if (response) {
                alert('Hi');
                //Username available.
                message.style.color = "green";
                message.innerHTML = "Company is already available";
            }
            else {
                //alert('Low');
                //Username not available.
                //message.style.color = "red";
                //message.innerHTML = "Company is NOT available";
              
            }
        });
    };

    function ClearMessage() {
        document.getElementById("message").innerHTML = "";
    };

  

</script>--%>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
