<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="RoomConfig.aspx.cs" Inherits="ACADEMIC_EXAMINATION_RoomConfig" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updplRoom"
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
    <asp:UpdatePanel ID="updplRoom" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">


                            <h3 class="box-title">
                                <asp:Label ID="lblDYtxtRoomconfig" runat="server"></asp:Label>
                            </h3>


                            <%-- <h3 class="box-title">Room / Block Configuration</h3>  --%>
                            <%-- <h5> <asp:Label ID="lblDYtxtRoomconfig" runat="server"></asp:Label>  </h5>--%>
                        </div>

                        <div class="box-body">
                            <div class="col-12">

                                <asp:Panel ID="roomconfig" runat="server">

                                    <div class="row">
                                        <div class="col-lg-3 col-md-6 col-12 form-group">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--                                                    <label>School/College name</label>--%>
                                                <asp:Label ID="lblDYtxtSchoolname" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" TabIndex="1" data-select2-enable="true" CausesValidation="false">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College/School/Institute Name" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="configure"></asp:RequiredFieldValidator>
                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College/School" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="Report"></asp:RequiredFieldValidator>--%>

                                        </div>

                                        <%--     <div class="form-group col-lg-3 col-md-6 col-12 Display-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                          
                                            <asp:Label ID="lblDYtxtFloor" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlFloorNo" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlBlockNo_SelectedIndexChanged" AutoPostBack="true"
                                            TabIndex="2" data-select2-enable="true">
                                            <asp:ListItem value="0"> Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvFloorNo" runat="server" ControlToValidate="ddlBlockNo"
                                            ValidationGroup="Show" Display="None" ErrorMessage="Please Select Block."
                                            SetFocusOnError="true" InitialValue="-1" />
                                    </div>
                                        --%>

                                        <div class="form-group col-lg-2 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%-- <label>Floor</label>--%>
                                                <asp:Label ID="lblBlock" runat="server" Font-Bold="true">Block Name</asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlBlockNo" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlBlockNo_SelectedIndexChanged"
                                                TabIndex="2" data-select2-enable="true">
                                                <asp:ListItem Value="0"> Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBlockNo"
                                                Display="None" ErrorMessage="Please Select Block." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="configure"></asp:RequiredFieldValidator>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlBlockNo"
                                                ValidationGroup="Show" Display="None" ErrorMessage="Please Select Block."
                                                SetFocusOnError="true" InitialValue="-1" />--%>
                                        </div>


                                        <div class="col-lg-3 col-md-6 col-12 form-group">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%-- <label>Block Name</label> --%>
                                                <asp:Label ID="lblDYtxtRoomname" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlRoom" runat="server" TabIndex="3" AppendDataBoundItems="True"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlRoom_SelectedIndexChanged" data-select2-enable="true" CausesValidation="false">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvRoom" runat="server" ControlToValidate="ddlRoom"
                                                Display="None" ErrorMessage="Please Select Room Name" InitialValue="0" SetFocusOnError="True" ValidationGroup="configure"></asp:RequiredFieldValidator>
                                         <%--   <asp:RequiredFieldValidator ID="rfvRoomNm" runat="server" ControlToValidate="ddlRoom"
                                                Display="None" ErrorMessage="Please select Room Name" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>--%>

                                        </div>


                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="row">
                                                <div class="col-lg-6 col-md-6 col-12 form-group">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Configuration Row</label>
                                                        <%--<asp:Label ID="lblDYtxtRow" runat="server" Font-Bold="true"></asp:Label>
                                                            <asp:Label ID="lblDYtxtConfig" runat="server" Font-Bold="true"></asp:Label>--%>
                                                    </div>
                                                    <asp:TextBox ID="txtRows" runat="server" onkeyup="totalCapacity();" TabIndex="4"
                                                        MaxLength="3"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvRows" runat="server" ControlToValidate="txtRows"
                                                        Display="None" ErrorMessage="Please Enter No. of Rows" ValidationGroup="configure"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtRows" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtRows">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-12 form-group">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Configuration Column</label>
                                                        <%--<asp:Label ID="lblDYtxtColumn" runat="server" Font-Bold="true"></asp:Label>--%>
                                                    </div>
                                                    <asp:TextBox ID="txtColumns" runat="server" onkeyup="totalCapacity();"
                                                        TabIndex="5" MaxLength="3"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvColumns" runat="server" ControlToValidate="txtColumns"
                                                        Display="None" ErrorMessage="Please Enter No. of Columns" ValidationGroup="configure"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FTEtxtColumns" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtColumns">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-lg-3 col-md-6 col-12 form-group">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--label>Block Capacity</label>  --%>
                                                <asp:Label ID="lblDYtxtBlockCapcity" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:TextBox ID="txtRoomCapacity" runat="server" TabIndex="6" MaxLength="20"
                                                Enabled="False" />

                                        </div>
                                        <div class="col-lg-3 col-md-6 col-12 form-group">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Actual Capacity</label> --%>
                                                <asp:Label ID="lblDYtxtActualCap" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:TextBox ID="txtActual" runat="server" TabIndex="7" MaxLength="20"
                                                Enabled="False" />
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-12 form-group">
                                            <div class="label-dynamic">
                                                <%--  <sup>* </sup>--%>
                                                <label>Not In Use Bench ID</label>
                                            </div>
                                            <asp:TextBox ID="txtId" runat="server" onblur="IsValid(this)" TabIndex="8" TextMode="SingleLine"
                                                Enabled="False"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtId" runat="server" FilterType="Custom"
                                                FilterMode="ValidChars" ValidChars="0123456789," TargetControlID="txtId">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:HiddenField ID="hdfbenchid" runat="server" />
                                            <asp:Label ID="lblMsg" runat="server"></asp:Label>

                                        </div>
                                    </div>

                                </asp:Panel>

                                <asp:Panel ID="pnlconfig" runat="server" class="row mt-3">
                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note</h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>To enter not in use seat ID's please enter comma separated values</span>  </p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Yellow highlighted benches in Room Configuration View are not in use</span>  </p>
                                        </div>
                                    </div>

                                    <div class="col-lg-6 col-md-12 col-12">
                                        <div class="sub-heading">
                                            <h5>Measurement Configurations</h5>
                                        </div>

                                        <asp:Panel ID="pnlfactors" runat="server">
                                            <asp:ListView ID="lvFactors" runat="server">
                                                <LayoutTemplate>
                                                    <div class="table-responsive" style="height: 120px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="MainLeadTable">
                                                            <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                <tr>
                                                                    <th>Rows * Columns
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
                                                    <tr class="item">
                                                        <td>
                                                            <asp:Label ID="lblRows" runat="server" Text='<%# Eval("Rows")%>' />
                                                            * 
                                                                    <asp:Label ID="lblColumns" runat="server" Text='<%# Eval("col")%>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </asp:Panel>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnConfigure" runat="server" class="btn btn-primary" Text="Configure"  ValidationGroup="configure"
                                        TabIndex="9" OnClick="btnConfigure_Click" />
                                    <asp:Button ID="btnClear" runat="server" class="btn btn-warning" TabIndex="10" OnClick="btnCancel_Click"
                                        Text="Cancel" />
                                </div>

                                <div class="col-12" >
                                    <asp:Panel ScrollBars="Auto" ID="pnlRoom" runat="server" Visible="false" Width="100%" Height="200px">
                                        <asp:GridView ID="gvRoom" runat="server" Width="100%" Visible="False" AutoGenerateColumns="False"
                                            BackColor="white" BorderColor="#e5e5e5" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="3" CellSpacing="2" OnSelectedIndexChanged="gvRoom_SelectedIndexChanged">
                                            <RowStyle BackColor="white" ForeColor="#212529" />
                                            <AlternatingRowStyle  BackColor="#e8effa" ForeColor="#212529" />
                                            <FooterStyle BackColor="white" ForeColor="#212529" />
                                            <PagerStyle ForeColor="#212529" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="white" ForeColor="#212529" />
                                            <HeaderStyle BackColor="white" ForeColor="#212529" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                                <div class="col-12 btn-footer mt-4">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" class="btn btn-primary" OnClientClick="return confirm('Are you sure? Do you Want to Submit Data ?')"
                                        TabIndex="11" Visible="False" ValidationGroup="Submit" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"   CausesValidation="false"  class="btn btn-warning"
                                        TabIndex="12" OnClick="btnCancel_Click" Visible="False"  />
                                    <asp:ValidationSummary ID="vsRoom" runat="server" DisplayMode="List" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="Show" />
                                </div>
                                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="configure" />
                                <asp:ValidationSummary ID="vsRoomNm" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />
                                <asp:UpdatePanel ID="pnlroomConfig" runat="server">
                                    <ContentTemplate>
                                        <td align="center" style="padding: 10px; text-align: center; width: 100%" align="top" visible="false"></td>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

    <script language="javascript" type="text/javascript">

        function totalCapacity() {
            var txtActual = document.getElementById('<%= txtActual.ClientID %>');
            var txtRoom = document.getElementById('<%= txtRoomCapacity.ClientID %>');
            var txtrow = document.getElementById('<%= txtRows.ClientID %>').value;
            var txtcolumn = document.getElementById('<%= txtColumns.ClientID %>').value;

            debugger
            if (parseInt(txtrow) > 20) {
                alert("Below 20 rows are allowed!");
                document.getElementById('<%= txtRows.ClientID %>').value = '';
                //return;
            }
            else if (parseInt(txtcolumn) > 10) {
                alert("Below 10 columns are allowed!");
                document.getElementById('<%= txtColumns.ClientID %>').value = '';
            }


        if (isNaN(txtrow)) {
            alert("Only numeric chraters allowed!");
            return;
        }
        else if (isNaN(txtcolumn)) {
            alert("Only numeric chraters allowed!");
            return;
        }
        else if (txtrow != '' && txtcolumn != '') {

            if (txtrow * txtcolumn <= txtRoom.value)
                txtActual.value = (txtrow * txtcolumn);
            else
                alert("Actual Capacity " + txtrow * txtcolumn + " exceeds Room Capacity " + txtRoom.value);
            // document.getElementById('<%= txtRows.ClientID %>').value = '';
           // document.getElementById('<%= txtColumns.ClientID %>').value = '';
           //return;
       }
       else { }
}


function totCount(chk) {
    var txtActualCap = document.getElementById('<%= txtActual.ClientID %>');
    if (chk.checked == true) {
        txtActualCap.value = Number(txtActualCap.value) + 1;

    }
    else {
        txtActualCap.value = Number(txtActualCap.value) - 1;

    }
}
function IsNumeric() {
    var txt = document.getElementById('<%= txtId.ClientID %>');
            var ValidChars = "0123456789,";
            var num = true;
            var mChar;
            if (txt.value.length > 0) {
                for (i = 0; i < txt.value.length && num == true; i++) {
                    mChar = txt.value.charAt(i);

                    if (ValidChars.indexOf(mChar) == -1) {
                        num = false;
                        txt.value = '';
                        alert("Only Comma(,) Separeted Numeric Values Are Allowed");
                        txt.select();
                        txt.focus();
                    }
                }
            }
            return num;
        }
        function ValidateNumeric(txt) {
            //var ValidChars = "0123456789.-,";
            var ValidChars = "0123456789,";
            var num = true;
            var mChar;

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);
                if (ValidChars.indexOf(mChar) == -1 && txt.value.length > 0) {
                    num = false;
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }

        function totids() {
            var txtrow = document.getElementById('<%= txtRows.ClientID %>').value;
            var txtcolumn = document.getElementById('<%= txtColumns.ClientID %>').value;
            var txtActualCap = document.getElementById('<%= txtActual.ClientID %>');
            var txtids = document.getElementById('<%=txtId.ClientID %>');
            var i = 1;
            var ids = Number(txtids.value + ",");

            var idss = txtids.value.split(",");
            var count = 0;


            var j = 0;

            for (j = 0; j < idss.Length; j++) {

                txtActualCap.value = (txtrow * txtcolumn) - ids.length;
                count++;
            }
        }
        function IsValid(txt) {
            var txtActualCap = document.getElementById('<%= txtActual.ClientID %>');
            IsNumeric();

            if (txt.value.indexOf("0") >= 0 && txt.value.charAt(txt.value.indexOf("0") - 1) == "," || txt.value.charAt(0) == "0") {
                alert("You have entered incorrect Bench ID!");
                txt.value = "";
                num = false;
            }
            //           else
            //           {
            //            if(txt.value.charAt(txt.value.length-1) == ",")
            //            {
            //            alert("String ending with ,");
            //            txt.value =""; //txt.value.RomoveAt(txt.value.charAt(txt.value.length-1));
            //            }
            //            }
        }

    </script>

</asp:Content>
