<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BranchWiseIntake.aspx.cs" Inherits="ACADEMIC_BranchWiseIntake" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .gridAdvance
        {
            text-align: center;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBatch"
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
    <asp:UpdatePanel ID="updBatch" runat="server">
        <ContentTemplate>
            <script>
                function Contact() {
                    var intake = document.getElementById('<%=txtIntake.ClientID%>').value

                    var len = intake.toString().length;


                    if (isNaN(intake)) {
                        alert("Only Number are Accepted");
                        document.getElementById('<%=txtIntake.ClientID%>').value = '';
                        inputField.value = " ";
                        return false;

                    }

                    else {
                        return true;
                    }

                }
            </script>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-4 col-md-6 col-12">

                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged"
                                            AppendDataBoundItems="True" ToolTip="Please Select Admission Batch" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                            AppendDataBoundItems="True" ToolTip="Please Select Degree" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" ToolTip="Please Select Branch">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Intake</label>
                                        </div>
                                        <asp:TextBox ID="txtIntake" runat="server" TabIndex="4" ToolTip="Please Enter Intake" MaxLength="3" Onkeyup="return Contact(this)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvIntake" runat="server" ControlToValidate="txtIntake"
                                            Display="None" ErrorMessage="Please Enter Intake" ValidationGroup="submit" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit"
                                CssClass="btn btn-primary" ValidationGroup="submit" TabIndex="5" OnClick="btnSave_Click" />
                            <%--<asp:Button ID="btnSaveAdvance" runat="server" Text="Submit Advance Configuration" ToolTip="Submit Advance Configuration"
                                CssClass="btn btn-primary" TabIndex="5" OnClick="btnSaveAdvance_Click" Visible="false"/>--%>

                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                CssClass="btn btn-warning" TabIndex="6" OnClick="btnCancel_Click" />


                        </div>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                        <%-- <layouttemplate>
                                <div id="demo-grid">
                        <div class="sub-heading">
                            <h5>Advanced Configuration</h5>
                        </div>
                        <asp:GridView ID="grvAdvanceConfig" runat="server" class="table table-striped table-bordered nowrap display" style="width: 100%;text-align:center" EmptyDataText="No data found" OnRowDataBound="grvAdvanceConfig_RowDataBound" OnDataBound="grvAdvanceConfig_DataBound">
                            <HeaderStyle CssClass="gridAdvance"/>
                            <Columns>
                                
                            </Columns>
                        </asp:GridView>
                                    </div>
                             </layouttemplate>--%>


                        <%-- <asp:ListView ID="lvAdvanceConfigdemo" runat="server">
                            <LayoutTemplate>
                                <div id="demo-grid">
                                    <div class="sub-heading">
                                        <h5>Advanced Configuration</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="mytable">
                                        
                                        <thead>
                                            <tr class="bg-light-blue">
                                                <th style="text-align: center">CATEGORY</th>
                                                <th id="th0" style="text-align: center;">
                                                    <asp:Label ID="lbl" runat="server" Text="0"></asp:Label>

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
                                       <asp:Label ID="lblcat" runat="server" Text='<%# Eval("CATEGORY") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>--%>

                        <div id="dvAdvanceConfig" runat="server" class="col-12">
                            <asp:Panel ID="pnlAdvanceConfig" runat="server" Height="500px" ScrollBars="Auto" Visible="false">
                                <asp:ListView ID="lvAdvanceConfig" runat="server">
                                    <LayoutTemplate>
                                        <div id="demo-grid">
                                            <div class="sub-heading">
                                                <h5>Advanced Configuration</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="mytable">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th colspan="22" style="text-align: center">ADMISSION TYPE</th>
                                                    </tr>
                                                    <tr class="bg-light-blue">
                                                         <th colspan="1" style="text-align: center">ACTION</th>
                                                        <th colspan="1" style="text-align: center">CATEGORY</th>
                                                        <th id="tbl_Rule1" colspan="20" style="text-align: center">PAYMENT TPYE</th>
                                                        <%--<th id="tbl_Rule2" colspan="1" style="text-align: center">TOTAL</th>--%>
                                                    </tr>
                                                </thead>
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th style="text-align: center"></th>
                                                        <th style="text-align: center"></th>
                                                        <th id="th0" style="text-align: center; display: none;"></th>
                                                        <th id="th1" style="text-align: center; display: none;"></th>
                                                        <th id="th2" style="text-align: center; display: none;"></th>
                                                        <th id="th3" style="text-align: center; display: none;"></th>
                                                        <th id="th4" style="text-align: center; display: none;"></th>
                                                        <th id="th5" style="text-align: center; display: none;"></th>
                                                        <th id="th6" style="text-align: center; display: none;"></th>
                                                        <th id="th7" style="text-align: center; display: none;"></th>
                                                        <th id="th8" style="text-align: center; display: none;"></th>
                                                        <th id="th9" style="text-align: center; display: none;"></th>
                                                        <th id="th10" style="text-align: center; display: none;"></th>
                                                        <th id="th11" style="text-align: center; display: none;"></th>
                                                        <th id="th12" style="text-align: center; display: none;"></th>
                                                        <th id="th13" style="text-align: center; display: none;"></th>
                                                        <th id="th14" style="text-align: center; display: none;"></th>
                                                        <th id="th15" style="text-align: center; display: none;"></th>
                                                        <th id="th16" style="text-align: center; display: none;"></th>
                                                        <th id="th17" style="text-align: center; display: none;"></th>
                                                        <th id="th18" style="text-align: center; display: none;"></th>
                                                        <th id="th19" style="text-align: center; display: none;"></th>
                                                        <%--<th style="text-align: center;"></th>--%>
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
                                            <td style="display: none;">
                                                <center>
                                                        <asp:CheckBox ID="chkAccept" runat="server" OnCheckedChanged="chkAccept_CheckedChanged" AutoPostBack="true" TabIndex="13" ToolTip='<%# Eval("CATEGORYNO") %>' />
                                                       </center>
                                            </td>
                                            <td style="">
                                                <asp:Label ID="lblcat" runat="server" Text='<%# Eval("CATEGORY") %>' />
                                            </td>
                                            <td id="td0" runat="server" style="display: none;">
                                                <center>
                                                            <asp:TextBox ID="txtCat1" runat="server" CssClass="form-control NumVal" MaxLength="3" Enabled="false"></asp:TextBox>
                                                      <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtCat1" FilterMode="ValidChars"
                                                    ValidChars="0123456789">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:HiddenField ID="hdfCat1" runat="server" />
                                                        </center>
                                            </td>
                                            <td id="td1" runat="server" style="display: none;">
                                                <center>
                                                            <asp:TextBox ID="txtCat1asn"  runat="server" CssClass="form-control NumVal" MaxLength="3" Enabled="false"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCat1asn" FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                            <asp:HiddenField ID="hdfCat1_Asign" runat="server" />
                                                        </center>
                                            </td>
                                            <td id="td2" runat="server" style="display: none;">
                                                <center>
                                                            <asp:TextBox ID="txtCat2" runat="server" CssClass="form-control NumVal" MaxLength="3" Enabled="false"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtCat2" FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                    <asp:HiddenField ID="hdfCat2" runat="server" />
                                                        </center>
                                            </td>
                                            <td id="td3" runat="server" style="display: none;">
                                                <center>
                                                            <asp:TextBox ID="txtCat2asn" runat="server" CssClass="form-control NumVal" MaxLength="3" Enabled="false"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtCat2asn" FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                            <asp:HiddenField ID="hdfCat2_Asign" runat="server" />
                                                        </center>
                                            </td>
                                            <td id="td4" runat="server" style="display: none;">


                                                <center>
                                                            <asp:TextBox ID="txtCat3" runat="server" CssClass="form-control NumVal" MaxLength="3" Enabled="false"></asp:TextBox>
                                                           <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtCat3" FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                     <asp:HiddenField ID="hdfCat3" runat="server" />
                                                        </center>
                                            </td>
                                            <td id="td5" runat="server" style="display: none;">
                                                <center>
                                                            <asp:TextBox ID="txtCat3asn" runat="server" CssClass="form-control NumVal" MaxLength="3" Enabled="false"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtCat3asn" FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                     <asp:HiddenField ID="hdfCat3_Asign" runat="server" />
                                                        </center>
                                            </td>
                                            <td id="td6" runat="server" style="display: none;">
                                                <center>
                                                            <asp:TextBox ID="txt7" runat="server" CssClass="form-control NumVal" MaxLength="3" Enabled="false"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txt7" FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                     <asp:HiddenField ID="hdf7" runat="server" />
                                                        </center>
                                            </td>
                                            <td id="td7" runat="server" style="display: none;">
                                                <center>
                                                            <asp:TextBox ID="txt8" runat="server" CssClass="form-control NumVal" MaxLength="3" Enabled="false"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txt8" FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                     <asp:HiddenField ID="hdf8" runat="server" />
                                                        </center>
                                            </td>
                                            <td id="td8" runat="server" style="display: none;">
                                                <center>
                                                            <asp:TextBox ID="txt9" runat="server" CssClass="form-control NumVal" MaxLength="3" Enabled="false"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txt9" FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                     <asp:HiddenField ID="hdf9" runat="server" />
                                                        </center>
                                            </td>
                                            <td id="td9" runat="server" style="display: none;">
                                                <center>
                                                            <asp:TextBox ID="txt10" runat="server"  CssClass="form-control NumVal" MaxLength="3" Enabled="false"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txt10" FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                     <asp:HiddenField ID="hdf10" runat="server" />
                                                        </center>
                                            </td>


                                            <td id="td10" runat="server" style="display: none;">
                                                <center>
                                                            <asp:TextBox ID="txt11" runat="server"  CssClass="form-control NumVal" MaxLength="3" Enabled="false"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txt11" FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                     <asp:HiddenField ID="hdf11" runat="server" />
                                                        </center>

                                            </td>
                                            <td id="td11" runat="server" style="display: none;">
                                                <center>
                                                            <asp:TextBox ID="txt12" runat="server"  CssClass="form-control NumVal" MaxLength="3" Enabled="false"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txt12" FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                     <asp:HiddenField ID="hdf12" runat="server" />
                                                        </center>

                                            </td>
                                            <td id="td12" runat="server" style="display: none;">
                                                <center>
                                                            <asp:TextBox ID="txt13" runat="server"  CssClass="form-control NumVal" MaxLength="3" Enabled="false"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txt13" FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                     <asp:HiddenField ID="hdf13" runat="server" />
                                                        </center>

                                            </td>
                                            <td id="td13" runat="server" style="display: none;">
                                                <center>
                                                            <asp:TextBox ID="txt14" runat="server"  CssClass="form-control NumVal" MaxLength="3" Enabled="false"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txt14" FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                     <asp:HiddenField ID="hdf14" runat="server" />
                                                        </center>

                                            </td>
                                            <td id="td14" runat="server" style="display: none;">
                                                <center>
                                                            <asp:TextBox ID="txt15" runat="server"  CssClass="form-control NumVal" MaxLength="3" Enabled="false"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="txt15" FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                     <asp:HiddenField ID="hdf15" runat="server" />
                                                        </center>

                                            </td>
                                            <td id="td15" runat="server" style="display: none;">
                                                <center>
                                                            <asp:TextBox ID="txt16" runat="server"  CssClass="form-control NumVal" MaxLength="3" Enabled="false"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" TargetControlID="txt16" FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                     <asp:HiddenField ID="hdf16" runat="server" />
                                                        </center>

                                            </td>
                                            <td id="td16" runat="server" style="display: none;">
                                                <center>
                                                            <asp:TextBox ID="txt17" runat="server"  CssClass="form-control NumVal" MaxLength="3" Enabled="false"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" TargetControlID="txt17" FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                     <asp:HiddenField ID="hdf17" runat="server" />
                                                        </center>

                                            </td>
                                            <td id="td17" runat="server" style="display: none;">
                                                <center>
                                                            <asp:TextBox ID="txt18" runat="server"  CssClass="form-control NumVal" MaxLength="3" Enabled="false"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" TargetControlID="txt18" FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                     <asp:HiddenField ID="hdf18" runat="server" />
                                                        </center>

                                            </td>
                                            <td id="td18" runat="server" style="display: none;">
                                                <center>
                                                            <asp:TextBox ID="txt19" runat="server"  CssClass="form-control NumVal" MaxLength="3" Enabled="false"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" TargetControlID="txt19" FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                     <asp:HiddenField ID="hdf19" runat="server" />
                                                        </center>

                                            </td>
                                            <td id="td19" runat="server" style="display: none;">
                                                <center>
                                                            <asp:TextBox ID="txt20" runat="server"  CssClass="form-control NumVal" MaxLength="3" Enabled="false"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" TargetControlID="txt20" FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                     <asp:HiddenField ID="hdf20" runat="server" />
                                                        </center>

                                            </td>

                                           <%-- <td style="display: none;">
                                                <center>
                                                            <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control NumVal" MaxLength="3" Enabled="false" Text='<%# Eval("TOTAL") %>'></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" TargetControlID="txtTotal" FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                     </center>
                                            </td>
                                        </tr>--%>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>

                        <div class="col-12">

                            <asp:Panel ID="PanelIntake" runat="server">
                                <div class="sub-heading">
                                    <h5>Branch Wise Intake</h5>
                                </div>
                                <asp:ListView ID="lvBranchWiseIntake" runat="server">

                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Edit</th>
                                                    <th>Admission Batch</th>
                                                    <th>Degree</th>
                                                    <th>Branch</th>
                                                    <th>Intake</th>
                                                    <%--<th>Configuration</th>--%>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>


                                        <asp:UpdatePanel runat="server" ID="updEventCategory">
                                            <ContentTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png"
                                                            AlternateText="Edit Record" ToolTip="Edit Record" CommandArgument='<%# Eval("INTAKE_ID")%>' TabIndex="5" OnClick="btnEdit_Click" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("BATCHNAME")%></td>
                                                    <td>
                                                        <%# Eval("DEGREENAME")%></td>
                                                    <td>
                                                        <%# Eval("LONGNAME")%></td>
                                                    <td>
                                                        <%# Eval("INTAKE")%></td>
                                                  <%--  <td>
                                                        <asp:ImageButton ID="btnEditAdvance" runat="server" ImageUrl="~/Images/view.png"
                                                            AlternateText="Edit Record" ToolTip="Edit Record" CommandArgument='<%# Eval("INTAKE_ID")%>' TabIndex="6" OnClick="btnEditAdvance_Click" />
                                                    </td>--%>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>