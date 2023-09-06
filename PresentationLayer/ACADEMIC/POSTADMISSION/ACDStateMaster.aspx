<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ACDStateMaster.aspx.cs" Inherits="ACADEMIC_POSTADMISSION_ACDStateMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>                    
                </div>

                <div id="Tabs" role="tabpanel">
                    <div id="divqualification">
                        <div class="col-12">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1">Country</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2">State</a>
                                    </li>
                                </ul>

                                <div class="tab-content" id="my-tab-content">
                                    <%--  TAB : COUNTRY  --%>
                                    <div class="tab-pane active" id="tab_1">
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Nationality</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlNationality_Country" runat="server" TabIndex="5" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlNationality_Country_SelectedIndexChanged">
                                                            <%--OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"--%>
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlNationality_Country" runat="server" ControlToValidate="ddlNationality_Country"
                                                            Display="None" ValidationGroup="Country" InitialValue="0"
                                                            ErrorMessage="Please Select Nationality"></asp:RequiredFieldValidator>
                                                    </div>


                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Country</label>
                                                        </div>
                                                        <asp:TextBox ID="txtCountryName" runat="server" TabIndex="3" onkeypress="allowAlphaNumericSpace(event)" CssClass="form-control" MaxLength="35"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtxtCountryName" runat="server" ControlToValidate="txtCountryName"
                                                            Display="None" ErrorMessage="Please Enter Country Name" ValidationGroup="Country" SetFocusOnError="true">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <asp:HiddenField ID="hfd_ActiveStatus" runat="server" ClientIDMode="Static" />
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chk_ActiveStatus" name="switch" checked />
                                                            <label data-on="Active" data-off="InActive" for="chk_ActiveStatus"></label>
                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:LinkButton ID="btnSubmitCountry" runat="server" Text="Submit" ValidationGroup="Country" OnClientClick="return validateActive();" TabIndex="1" CssClass="btn btn-primary" OnClick="btnSubmitCountry_Click" >Submit</asp:LinkButton>
                                                        <%--OnClick="btnSubmit_Click"--%>
                                                        <asp:Button ID="btnCancelCountry" runat="server" Text="Cancel" TabIndex="1" CssClass="btn btn-warning" OnClick="btnCancelCountry_Click" />
                                                        <%--OnClick="btnCancel_Click"--%>
                                                        <asp:ValidationSummary ID="vsCountry" runat="server" ValidationGroup="Country"
                                                            DisplayMode="List" ShowMessageBox="True" ShowSummary="false" />
                                                    </div>


                                                    <div class="col-lg-8 col-12">
                                                        <asp:Panel ID="pnlCountry" runat="server" Visible="true">
                                                            <asp:ListView ID="lvCountry" runat="server">
                                                                <LayoutTemplate>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divCountry">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th style="text-align: center; width: 10%">Edit</th>
                                                                                <th style="text-align: center; width: 200px">Nationality</th>
                                                                                <th style="text-align: center; width: 200px">Country Name</th>
                                                                                <th style="text-align: center; width: 20px">Status</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel runat="server" ID="updCountry">
                                                                        <ContentTemplate>
                                                                            <tr>

                                                                                <td style="text-align: center;">
                                                                                    <asp:ImageButton ID="btnEdit_Country" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                                        CommandArgument='<%# Eval("COUNTRYNO")%>' AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" OnClick="btnEdit_Country_Click" />
                                                                                    <%--OnClick="btnEdit_Click"                  --%>                                               
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("NATIONALITY")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("COUNTRYNAME")%>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblStatus_Country" runat="server" CssClass="badge" Text='<%# Eval("ACTIVE_STATUS") %>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <%--  TAB:STATE  --%>
                                    <div class="tab-pane fade" id="tab_2">
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Country</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCountry_State" runat="server" TabIndex="5" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_State_SelectedIndexChanged">
                                                            <%--OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"--%>
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlCountry_State" runat="server" ControlToValidate="ddlCountry_State"
                                                            Display="None" ValidationGroup="State" InitialValue="Please Select"
                                                            ErrorMessage="Please Select Country"></asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCountry_State"
                                                            Display="None" ValidationGroup="State" InitialValue="0"
                                                            ErrorMessage="Please Select Country"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>State</label>
                                                        </div>
                                                        <asp:TextBox ID="txtStateName" runat="server" TabIndex="3" onkeypress="allowAlphaNumericSpace(event)" CssClass="form-control" MaxLength="35"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtxtStateName" runat="server" ControlToValidate="txtStateName"
                                                            Display="None" ErrorMessage="Please Enter State Name" ValidationGroup="State" SetFocusOnError="true">
                                                        </asp:RequiredFieldValidator>
                                                    </div>


                                                    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkStatus" name="switch" checked />
                                                            <label data-on="Active" data-off="InActive" for="chkStatus"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-6 mt-lg-4">
                                                    <div class="note-div">
                                                        <h2 class="heading">Note</h2>
                                                        <p>
                                                            <i class="fa fa-star" aria-hidden="true"></i><span>
                                                                <asp:Label ID="lblInfo" runat="server" Font-Bold="false" Text="On selection of country, State list will be displayed."></asp:Label></span>
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="btnSubmitState" runat="server" Text="Submit" ValidationGroup="State" OnClientClick="return validate_Active();" TabIndex="1" CssClass="btn btn-primary" OnClick="btnSubmitState_Click">Submit</asp:LinkButton>
                                                <%--OnClick="btnSubmit_Click"--%>
                                                <asp:Button ID="btnCancelState" runat="server" Text="Cancel" TabIndex="1" CssClass="btn btn-warning" OnClick="btnCancelState_Click" />
                                                <%--OnClick="btnCancel_Click"--%>
                                                <asp:ValidationSummary ID="vsState" runat="server" ValidationGroup="State"
                                                    DisplayMode="List" ShowMessageBox="True" ShowSummary="false" />
                                            </div>

                                            <div class="col-lg-8 col-12">
                                                <asp:Panel ID="pnlState" runat="server" Visible="true">
                                                    <asp:ListView ID="lvState" runat="server">
                                                        <LayoutTemplate>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divState">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th style="text-align: center; width: 10%">Edit</th>
                                                                        <th style="width: 200px">State</th>
                                                                        <th style="width: 20px">Status</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel runat="server" ID="updState">
                                                                <ContentTemplate>
                                                                    <tr>
                                                                        <td style="text-align: center;">
                                                                            <asp:ImageButton ID="btnEdit_State" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                                CommandArgument='<%# Eval("STATENO")%>' AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" OnClick="btnEditState_Click" />
                                                                            <%--OnClick="btnEdit_Click"                                                                
                                                        </td>--%>
                                                                        <td>
                                                                            <%# Eval("STATENAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblStatus_State" runat="server" CssClass="badge" Text='<%# Eval("ACTIVESTATUS") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ContentTemplate>                                                               
                                                            </asp:UpdatePanel>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        function TabShow(tabName) {
            //alert('hii')
            //var tabName = "tab_2";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        }
    </script>

    <script>
        function functionx(evt) {
            if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57)) {
                //alert("Enter Only Numeric Value ");
                return false;
            }

        }
        function allowAlphaNumericSpace(e) {
            var code = ('charCode' in e) ? e.charCode : e.keyCode;
            if (!(code == 32) && // space
              !(code > 64 && code < 91) && // upper alpha (A-Z)
              !(code > 96 && code < 123)) { // lower alpha (a-z)
                e.preventDefault();
            }
        }
        function setMaxLength(control) {
            //get the isMaxLength attribute
            var mLength = control.getAttribute ? parseInt(control.getAttribute("isMaxLength")) : ""

            //was the attribute found and the length is more than the max then trim it
            if (control.getAttribute && control.value.length > mLength) {
                control.value = control.value.substring(0, mLength);
                //alert('Length exceeded');
            }

            //display the remaining characters
            var modid = control.getAttribute("id") + "_remain";
            if (document.getElementById(modid) != null) {
                document.getElementById(modid).innerHTML = mLength - control.value.length;
            }
        }
    </script>


     <script>
         function SetActive(val) {
             $('#chkStatus').prop('checked', val);

         }
         function validate_Active() {

             $('#hfdActive').val($('#chkStatus').prop('checked'));

         }

         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_endRequest(function () {
             $(function () {
                 $('#btnSubmitState').click(function () {
                     validate_Active();
                 });
             });
         });
    </script>




    <script>
        function Set_ActiveStatus_Country(val) {
            $('#chk_ActiveStatus').prop('checked', val);

        }
        function validateActive() {

            $('#hfd_ActiveStatus').val($('#chk_ActiveStatus').prop('checked'));

        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitCountry').click(function () {
                    validateActive();
                });
            });
        });
    </script>

    
</asp:Content>


