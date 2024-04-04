<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SubWiseContentDetialsNot_in_Syllabus.aspx.cs" Inherits="ACADEMIC_SubWiseContentDetialsNot_in_Syllabus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <link href="../../../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../plugins/multiselect/bootstrap-multiselect.js"></script>--%>

    <div>
        <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="updSection"
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


    <asp:UpdatePanel ID="updSection" runat="server" UpdateMode="Always">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Subject Wise Content Details Not In Syllabus.</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--  <label>College & Scheme</label>--%>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" CssClass="form-control" TabIndex="1"
                                            ValidationGroup="offered" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hfid" runat="server" />
                                        <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="validate">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Session</label>--%>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="2" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select Session" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="validate"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourseName" runat="server" AppendDataBoundItems="True" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select Session" AutoPostBack="true" OnSelectedIndexChanged="ddlCourseName_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCourseName"
                                            Display="None" ErrorMessage="Please Select Course" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="validate"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divfromdate" runat="server">
                                        <div class="label-dynamic">
                                            <sup id="frmdtspan" runat="server">* </sup>
                                            <label>Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="dvcal1" runat="server" class="fa fa-calendar text-green"></i>
                                            </div>
                                            <asp:TextBox ID="txtDate" runat="server" ValidationGroup="Show" onpaste="return false;"
                                                TabIndex="4" ToolTip="Please Enter Date" CssClass="form-control" Style="z-index: 0;" />
                                            <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtDate" PopupButtonID="dvcal1" />
                                            <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtDate"
                                                Display="None" ErrorMessage="Please Enter Date" SetFocusOnError="True"
                                                ValidationGroup="validate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />

                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblTopicName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>

                                        <asp:TextBox ID="txtTopicName" runat="server" MaxLength="100" ViewStateMode="Enabled"
                                            CssClass="form-control" ToolTip="Topic Name" TabIndex="5" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTopicName"
                                            Display="None" ErrorMessage="Please Enter Topic Name" SetFocusOnError="True"
                                            ValidationGroup="validate" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblContentofTopic" runat="server" Font-Bold="true"></asp:Label>
                                        </div>

                                        <asp:TextBox ID="txtContentOfTopic" runat="server" MaxLength="100" ViewStateMode="Enabled"
                                            CssClass="form-control" ToolTip="Content of topic" TabIndex="6" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtContentOfTopic"
                                            Display="None" ErrorMessage="Please Enter Content Of Topic" SetFocusOnError="True"
                                            ValidationGroup="validate" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblMappinglevel" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtMappingLevel" runat="server" MaxLength="100" ViewStateMode="Enabled"
                                            CssClass="form-control" ToolTip="Mapping Level With PEO" TabIndex="7" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMappingLevel"
                                            Display="None" ErrorMessage="Please Enter Mapping Level With PEO" SetFocusOnError="True"
                                            ValidationGroup="validate" />
                                    </div>
                                    <div id="divMsg" runat="server"></div>


                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" TabIndex="8" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ValidationGroup="validate" />
                                <asp:LinkButton ID="btnReport" runat="server" TabIndex="9" OnClick="btnReport_Click1" CssClass="btn btn-primary">Report</asp:LinkButton>
                                <%--<asp:Button ID="btnReport" runat="server" TabIndex="9" Text="Report" OnClick="btnReport_Click1" CssClass="btn btn-primary" />--%>
                                <asp:Button ID="btnCancel" runat="server" TabIndex="10" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="validate" />
                            </div>


                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <asp:ListView ID="lvSubWiseContent" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center; width: 5%;">Edit</th>
                                                        <th style="text-align: center; width: 5%;">Remove</th>
                                                        <th style="text-align: center; width: 20%;">Topic Name</th>
                                                        <th style="text-align: center; width: 10%;">Date</th>
                                                        <th style="text-align: center; width: 50%;">Content of Topic</th>
                                                        <th style="text-align: center; width: 10%;">Mapping</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center; width: 5%;">
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                        CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Images/edit.png" ToolTip="Edit Record" OnClick="btnEdit_Click"/>
                                                </td>
                                                <td style="text-align: center; width: 5%;">
                                                    <asp:ImageButton ID="btnRemove" runat="server" AlternateText="Delete Record" CausesValidation="false"
                                                        CommandArgument='<%# Eval("ID")%>' ImageUrl="~/Images/delete.png" ToolTip="Delete Record" OnClick="btnRemove_Click"/>
                                                </td>
                                                <td style="text-align: left; width: 20%;">
                                                    <asp:Label ID="lblTopicName" Text='<%# Eval("TOPIC_NAME")%>' runat="server" />
                                                </td>
                                                <td style="text-align: center; width: 10%;">
                                                    <asp:Label ID="lblDate" Text='<%# Eval("DATE")%>' runat="server" />
                                                </td>
                                                <td style="text-align: left; width: 50%;">
                                                    <asp:Label ID="lblContent" Text='<%# Eval("CONTENT")%>' runat="server" />
                                                </td>
                                                <td style="text-align: center; width: 10%;">
                                                    <asp:Label ID="lblMapping" Text='<%# Eval("MAPP_PEO")%>' runat="server" />
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
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>



</asp:Content>


