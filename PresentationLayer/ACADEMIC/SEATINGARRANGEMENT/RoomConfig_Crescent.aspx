<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="RoomConfig_Crescent.aspx.cs" Inherits="ACADEMIC_EXAMINATION_RoomConfig" %>

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

    <asp:UpdatePanel ID="updplRoom" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ROOM CONFIGURATION</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <asp:RadioButton ID="rbInternal" CssClass="rbInt" runat="server" Text="&nbsp;Internal Exam" GroupName="IntExt"
                                            TabIndex="1" Checked="true" OnCheckedChanged="rbInternal_CheckedChanged" AutoPostBack="true" />
                                        <asp:RadioButton ID="rbExternal" CssClass="rbExt" runat="server" Checked="false" Text="&nbsp;External Exam" OnCheckedChanged="rbExternal_CheckedChanged" AutoPostBack="true"
                                            GroupName="IntExt" TabIndex="2" />
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Panel ID="roomconfig" runat="server">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Room Configuration For
                                                        <asp:Label ID="lblHead" runat="server" Text="External"></asp:Label> Exam</h5>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Room Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlRoom" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="True"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlRoom_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvRoom" runat="server" ControlToValidate="ddlRoom"
                                                        Display="None" ErrorMessage="Please Select Room Name" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfvRoomNm" runat="server" ControlToValidate="ddlRoom"
                                                        Display="None" ErrorMessage="Please select Room Name" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Configuration</label>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-6">
                                                            <div class="label-dynamic">
                                                                <label>Row</label>
                                                            </div>
                                                            <asp:TextBox ID="txtRows" runat="server" Onblur="totalCapacity();" CssClass="form-control" TabIndex="2"
                                                                MaxLength="2" Style="text-align: center"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvRows" runat="server" ControlToValidate="txtRows"
                                                                Display="None" ErrorMessage="Please Enter No. of Rows" ValidationGroup="configure"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtRows" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtRows">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <div class="label-dynamic">
                                                                <label>Column</label>
                                                            </div>
                                                            <asp:TextBox ID="txtColumns" runat="server" Onblur="totalCapacity();" CssClass="form-control"
                                                                TabIndex="3" MaxLength="2" Style="text-align: center"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvColumns" runat="server" ControlToValidate="txtColumns"
                                                                Display="None" ErrorMessage="Please Enter No. of Columns" ValidationGroup="configure"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FTEtxtColumns" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtColumns">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Room Capacity</label>
                                                    </div>
                                                    <asp:TextBox ID="txtRoomCapacity" runat="server" CssClass="form-control" TabIndex="4" MaxLength="20"
                                                        Enabled="False" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Actual Capacity</label>
                                                    </div>
                                                    <asp:TextBox ID="txtActual" runat="server" CssClass="form-control" TabIndex="4" MaxLength="20"
                                                        Enabled="False" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Seat Not in Use</label>
                                                    </div>
                                                    <asp:TextBox ID="txtId" CssClass="form-control" runat="server" onblur="IsValid(this)" TextMode="SingleLine"
                                                        Enabled="False"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtId" runat="server" FilterType="Custom"
                                                        FilterMode="ValidChars" ValidChars="0123456789," TargetControlID="txtId">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:HiddenField ID="hdfbenchid" runat="server" />
                                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class=" note-div">
                                                        <h5 class="heading">Note</h5>
                                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>To enter not in use bench ID's please enter comma separated values.</span> </p>
                                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>Gray highlighted benches in Room Configuration View are not in use.</span> </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </asp:Panel>

                                    <div class="col-lg-3 col-md-3 col-12">
                                        <asp:Panel ID="pnlconfig" runat="server">
                                            <div>Measurement</div>
                                            <div class="sub-heading">
                                                <h5>Configurations</h5>
                                            </div>
                                            <asp:Panel ID="pnlfactors" runat="server">
                                                <asp:ListView ID="lvFactors" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="demp_grid" class="vista-grid">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Rows * Columns </th>
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
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnConfigure" runat="server" class="btn btn-primary" Text="Configure" ValidationGroup="configure"
                                    TabIndex="5" OnClick="btnConfigure_Click" />
                                <asp:Button ID="btnClear" runat="server" class="btn btn-warning" OnClick="btnCancel_Click" Text="Cancel" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ScrollBars="Auto" ID="pnlRoom" runat="server" Visible="false" Width="100%" Height="200px">
                                    <asp:GridView ID="gvRoom" runat="server" Width="100%" Visible="False" AutoGenerateColumns="False"
                                        BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="3" CellSpacing="2" OnSelectedIndexChanged="gvRoom_SelectedIndexChanged" CssClass="GridHeader">
                                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" class="btn btn-primary"
                                    Visible="False" ValidationGroup="Submit" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" class="btn btn-warning"
                                    TabIndex="9" OnClick="btnCancel_Click" Visible="False" />
                                <asp:ValidationSummary ID="vsRoom" runat="server" DisplayMode="List" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="Show" />
                                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="configure" />

                                <asp:ValidationSummary ID="vsRoomNm" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />
                                <asp:UpdatePanel ID="pnlroomConfig" runat="server">
                                    <ContentTemplate>
                                        <div visible="false"></div>
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
            debugger;
            var txtActual = document.getElementById('<%= txtActual.ClientID %>');
            var txtRoom = document.getElementById('<%= txtRoomCapacity.ClientID %>');
            var txtrow = document.getElementById('<%= txtRows.ClientID %>').value;
            var txtcolumn = document.getElementById('<%= txtColumns.ClientID %>').value;

            if (isNaN(txtrow)) {
                alert("Only numeric chraters allowed!");
                return;
            }
            else
                if (isNaN(txtcolumn)) {
                    alert("Only numeric chraters allowed!");
                    return;
                }
                else
                    if (txtrow != '' && txtcolumn != '') {
                        if (txtrow * txtcolumn <= txtRoom.value)
                            txtActual.value = (txtrow * txtcolumn);
                        else
                            alert("Actual Capacity " + txtrow * txtcolumn + " exceeds Room Capacity " + txtRoom.value);
                    }
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
                num = false;
            }
        }

    </script>

    <%--<script>
        $(document).ready(function () {
            $('.rbInt').click(function () {
                $('.lblHead').text('Internal');
            })
            $('.rbExt').click(function () {
                $('.lblHead').text('External');
            })
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {

            $(document).ready(function () {
                $('.rbInt').click(function () {
                    $('.lblHead').text('Internal');
                })
                $('.rbExt').click(function () {
                    $('.lblHead').text('External');
                })
            });

        });
    </script>--%>
    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)
        function Cover(bottom, top, ignoreSize) {
            var location = Sys.UI.DomElement.getLocation(bottom);
            top.style.position = 'absolute';
            top.style.top = location.y + 'px';
            top.style.left = location.x + 'px';
            if (!ignoreSize) {
                top.style.height = bottom.offsetHeight + 'px';
                top.style.width = bottom.offsetWidth + 'px';
            }
        }
    </script>
</asp:Content>
