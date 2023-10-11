<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DecodingGeneration.aspx.cs" Inherits="ACADEMIC_DecodingGeneration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updDCode" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>DECODING NUMBER GENERATION</b></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-md-4" style="display: none;">
                                        <label>Term</label>
                                        <asp:Label ID="lblPreSession" runat="server" Font-Bold="true" />
                                    </div>
<<<<<<< HEAD
                                     <div class="form-group col-lg-3 col-md-6 col-12">
=======
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlClgScheme" runat="server" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlClgScheme_SelectedIndexChanged" Width="69px" TabIndex="1"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfcClgScheme" runat="server" InitialValue="0" SetFocusOnError="true" ControlToValidate="ddlClgScheme" Display="None" ErrorMessage="Please Select College & Scheme" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0" SetFocusOnError="true" ControlToValidate="ddlClgScheme" Display="None" ErrorMessage="Please Select College & Scheme" ValidationGroup="ShowStat"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
>>>>>>> 9a9e4fd ([ENHANCEMENT] [48077] Barcode and seat number generation)
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
<<<<<<< HEAD
<<<<<<< HEAD
                                        <asp:DropDownList ID="ddlSession" runat="server" AutoPostBack="true" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" data-select2-enable="true">
=======
=======
>>>>>>> 9a9e4fd ([ENHANCEMENT] [48077] Barcode and seat number generation)
                                        <asp:DropDownList ID="ddlSession" runat="server" AutoPostBack="true" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
>>>>>>> 9a9e4fd ([ENHANCEMENT] [48077] Barcode and seat number generation)
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDept" runat="server" InitialValue="0" SetFocusOnError="true"
                                            ControlToValidate="ddlSession" Display="None" ErrorMessage="Please Select Session."
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
<<<<<<< HEAD
                                     <div class="form-group col-lg-3 col-md-6 col-12">
=======

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true" />
                                        </div>
                                        <asp:DropDownList ID="ddlsemester" runat="server"
                                            AppendDataBoundItems="True" AutoPostBack="True"
                                            data-select2-enable="true" OnSelectedIndexChanged="ddlsemester_SelectedIndexChanged" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" SetFocusOnError="true"
                                            ControlToValidate="ddlsemester" Display="None" ErrorMessage="Please Select Semester."
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="0" SetFocusOnError="true"
                                            ControlToValidate="ddlsemester" Display="None" ErrorMessage="Please Select Semester."
                                            ValidationGroup="ShowStat"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
>>>>>>> 9a9e4fd ([ENHANCEMENT] [48077] Barcode and seat number generation)
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rvfDegree" runat="server" InitialValue="0" SetFocusOnError="true"
                                            ControlToValidate="ddlDegree" Display="None" ErrorMessage="Please Select Degree"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server"
                                            AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" InitialValue="0" SetFocusOnError="true"
                                            ControlToValidate="ddlBranch" Display="None" ErrorMessage="Please Select Branch."
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server"
                                            AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" InitialValue="0" SetFocusOnError="true"
                                            ControlToValidate="ddlScheme" Display="None" ErrorMessage="Please Select Scheme"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Course</label>
                                        </div>
<<<<<<< HEAD
<<<<<<< HEAD
                                        <asp:DropDownList ID="ddlCourse" runat="server"
                                            AppendDataBoundItems="True" AutoPostBack="True"
=======
                                        <asp:DropDownList ID="ddlCourse" runat="server" CssClass="d-none"
                                            AppendDataBoundItems="True" AutoPostBack="True" TabIndex="1"
>>>>>>> 9a9e4fd ([ENHANCEMENT] [48077] Barcode and seat number generation)
=======
                                        <asp:DropDownList ID="ddlCourse" runat="server" CssClass="d-none"
                                            AppendDataBoundItems="True" AutoPostBack="True" TabIndex="1"
>>>>>>> 9a9e4fd ([ENHANCEMENT] [48077] Barcode and seat number generation)
                                            OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" SetFocusOnError="true" 
                                            ControlToValidate="ddlCourse" Display="None" ErrorMessage="Please Select Course."
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Digits</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDigits" runat="server" Enabled="false" data-select2-enable="true">
                                            <asp:ListItem Value="5" Selected="True">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rvDigits" runat="server" InitialValue="0" ControlToValidate="ddlDigits"
                                            Display="None" ErrorMessage="Please Select Digits." ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
<<<<<<< HEAD
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                              <p><asp:Label ID="lblTot" runat="server" Font-Bold="true" />
                                <asp:Label ID="lblAb" runat="server" Font-Bold="true"  CssClass="ml-4"/></p>
                                <asp:Button ID="btnGenNo" runat="Server" Text="Generate Number" CssClass="btn btn-primary"
                                    ValidationGroup="Show" OnClick="btnGenNo_Click" />
                                <asp:Button ID="btnLock" runat="server" Text="Lock" CssClass="btn btn-primary"
                                    ValidationGroup="Show" OnClientClick="return confirmLock();"
                                    OnClick="btnLock_Click" />
                                <asp:Button ID="btnReport" runat="server" Text="Print Decode No" OnClick="btnReport_Click"
                                    CssClass="btn btn-info" ValidationGroup="Show" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" />
=======

                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12 ">
                                        <%--<asp:RadioButtonList ID="rblNumType" runat="server" RepeatDirection="Horizontal" CausesValidation="false" AutoPostBack="true" OnSelectedIndexChanged="rblNumType_SelectedIndexChanged">
                                           <asp:ListItem Value="0"> Decode No</asp:ListItem>
                                            <asp:ListItem Value="1"> Seat No</asp:ListItem>
                                        </asp:RadioButtonList>--%>
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Number Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlNumType" runat="server" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlNumType_SelectedIndexChanged" TabIndex="1">
                                            <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                            <%--<asp:ListItem Value="0">Decode Number</asp:ListItem>
                                            <asp:ListItem Value="1">Seat Number</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdfNumtypeStat" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlNumType"
                                            Display="None" ErrorMessage="Please Select Number Type Decode/Seat." ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div id="divtxtNumberSeries" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>False Number Series</label>
                                        </div>
                                        <asp:TextBox ID="txtNumSeries" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="fltnumseries" runat="server" FilterType="Numbers" TargetControlID="txtNumSeries"></ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <p id="pStatics" visible="false" runat="server">
                                    <asp:Label ID="lblTot" runat="server" Font-Bold="true" />
                                    <asp:Label ID="lblAb" runat="server" Font-Bold="true" CssClass="ml-4" />
                                    <asp:Label ID="lblHDec" runat="server" Font-Bold="true" CssClass="ml-4" />
                                    <asp:Label ID="lblNDec" runat="server" Font-Bold="true" CssClass="ml-4" />
                                </p>
                                <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" OnClick="btnShow_Click" Text="Show" ValidationGroup="Show" TabIndex="1" />
                                <asp:Button ID="btnGenNo" runat="Server" CssClass="btn btn-primary" OnClick="btnGenNo_Click" Text="Generate Number" ValidationGroup="Show" TabIndex="1" />
                                <asp:Button ID="btnLock" runat="server" CssClass="btn btn-primary" OnClick="btnLock_Click" OnClientClick="return confirmLock();" Text="Lock" ValidationGroup="Show" Visible="false" Enabled="false" TabIndex="1" />
                                <span id="spanDecodebtn" runat="server" visible="false">
                                    <asp:Button ID="btnReport" runat="server" CssClass="btn btn-info" OnClick="btnReport_Click" Text="Print Decode No" ValidationGroup="Show" TabIndex="1" />
                                    <asp:Button ID="btnStatDecode" runat="server" CssClass="btn btn-primary" Text="Show Generated" OnClick="btnStatDecode_Click" ValidationGroup="ShowStat" TabIndex="1" />
                                    <asp:Button ID="btnNotGenDe" runat="server" CssClass="btn btn-primary" Text="Show Not Generated" OnClick="btnNotGenDe_Click" ValidationGroup="ShowStat" TabIndex="1" />
                                </span>
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" OnClick="btnCancel_Click" Text="Cancel" TabIndex="1" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />
                                <%-- ShowStat --%>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ShowStat" />
>>>>>>> 9a9e4fd ([ENHANCEMENT] [48077] Barcode and seat number generation)
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvDecodeNo" runat="server"
                                        OnItemDataBound="lvDecodeNo_ItemDataBound">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Decoding Number Generation List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Enrollment No.
                                                        </th>
                                                        <th>Roll No.
                                                        </th>
                                                        <th>Decode No.
                                                        </th>
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
                                                    <%# Eval("REGNO") %>
                                                    <asp:HiddenField ID="hdfAB" runat="server" Value='<%# Eval("EXTERMARK") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("ROLL_NO") %>
                                                </td>
                                                <td>
                                                    <%# Eval("DECODENO")%>
                                                    <asp:Label ID="lblAB" runat="server" />
                                                    <asp:Label runat="server" ID="lblABP" Text=' <%# Eval("EXTERMARK") %> '> <%# Eval("EXTERMARK") %> </asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server" />

    <script language="javascript" type="text/javascript">

        function confirmLock() {
            var ret = confirm('Do you want to Lock Decode Nos. for Current Selecttion.');
            return ret;
        }
    </script>

</asp:Content>

