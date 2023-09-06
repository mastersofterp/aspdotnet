<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ExamFeeDefination.aspx.cs" Inherits="ACADEMIC_EXAMINATION_ExamFeeDefination" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updFee"
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



    <asp:UpdatePanel ID="updFee" runat="server">
        <ContentTemplate>
            <div class="row">

                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Exam Fee Defination</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Institute Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlColg" runat="server" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="1"
                                            AutoPostBack="True" ToolTip="Please Select Institute" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvColg" runat="server" ControlToValidate="ddlColg"
                                            Display="None" ErrorMessage="Please Select Institute" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session </label>
                                        </div>
                                        <%--<asp:TextBox ID="lblSession" runat="server"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="1"
                                            AutoPostBack="True" ToolTip="Please Select Session" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="eligible"></asp:RequiredFieldValidator>
                                        <%--<asp:Label ID="lblSession" runat="server"  ></asp:Label>--%>
                                    </div>

                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" data-select2-enable="true" runat="server" AppendDataBoundItems="True" TabIndex="4"
                                            ToolTip="Please Select Degree Type" AutoPostBack="True" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Programme/Branch" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>



                                </div>
                            </div>
                            <div class="col-12">
                                <center> <asp:Button  ID="btnSave" runat="server" Text="Save" ToolTip="Save" CausesValidation="false"
                                    CssClass="btn btn-primary" Visible="false"  ValidationGroup="Save" OnClick="btnSave_Click" /></center>
                            </div>


                        </div>
                    </div>
                </div>

            </div>


            <div class="col-12 ">
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                    <asp:ListView ID="lvFeeDefination" runat="server">
                        <LayoutTemplate>
                            <div class="sub-heading">
                                <h5>Search Results</h5>
                            </div>

                            <table id="tblStudents" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                <thead>
                                    <tr class="bg-light-blue">
                                        <th>Subject Id</th>
                                        <th>Subject Name</th>
                                        <th>Regular</th>
                                        <th>Backlog</th>
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
                                    <asp:Label runat="server" ID="lblSubId" Text='<%# Eval("SUBID")%>'></asp:Label></td>
                                <td>
                                    <asp:Label runat="server" ID="lblSubName" Text='<%# Eval("SUBNAME")%>'></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtRegular" runat="server" autocomplete="off" placeholder="Please Enter Amount.." Enabled='<%# Eval("Regular").ToString()=="0"?true:false %>' Text='<%# Eval("Regular")%>'></asp:TextBox>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="fte" runat="server" ValidChars="0123456789" 
                                        TargetControlID="txtRegular">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBacklog" runat="server" autocomplete="off" Enabled='<%# Eval("Backlog").ToString()=="0"?true:false %>' placeholder="Please Enter Amount.." Text='<%# Eval("Backlog")%>'></asp:TextBox>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="0123456789"
                                        TargetControlID="txtBacklog">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                </td>


                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </asp:Panel>
            </div>




        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnPrintReport" />--%>
            <%--<asp:PostBackTrigger ControlID="btnShow" />--%>
        </Triggers>
    </asp:UpdatePanel>



</asp:Content>
