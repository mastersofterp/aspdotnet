<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendEmail_Bulk.aspx.cs" Inherits="ADMINISTRATION_SendEmail_Bulk" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function getCurrentYear() {
            var cDate = new Date();
            return cDate.getFullYear();
        }

    </script>
    <div>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server"
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
    <asp:UpdatePanel ID="updpnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblschool" runat="server" Font-Bold="true">School/Institute Name </asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" TabIndex="2" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Please Select Institute" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ErrorMessage="Please Select Institute" ControlToValidate="ddlCollege" Display="None"
                                            SetFocusOnError="True" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--  <sup>* </sup>--%>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                            TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <asp:Label ID="Label2" runat="server" Font-Bold="true">Branch </asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                            TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--  <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--  <sup>* </sup>--%>
                                            <asp:Label ID="Label3" runat="server" Font-Bold="true">Semester </asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--  <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="Label4" runat="server" Font-Bold="true">Subject </asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtSubject" CssClass="form-control" TextMode="MultiLine" runat="server"
                                            PlaceHolder="Please Enter Email Subject here" Height="46px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rf1" runat="server" ControlToValidate="txtSubject"
                                            Display="None" ErrorMessage="Please Enter Email Subject" SetFocusOnError="true"
                                            ValidationGroup="sendEmail" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="Label5" runat="server" Font-Bold="true">Message </asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtMessage" CssClass="form-control" TextMode="MultiLine" runat="server"
                                            PlaceHolder="Please Enter Email Message here"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rf2" runat="server" ControlToValidate="txtMessage"
                                            Display="None" ErrorMessage="Please Enter Email Message" SetFocusOnError="true"
                                            ValidationGroup="sendEmail" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divSubject" runat="server">
                                        <div class="label-dynamic">
                                            <label>Attach File</label>
                                        </div>
                                        <asp:FileUpload ID="fuAttachment" TabIndex="1" runat="server" AllowMultiple="true" />
                                        <label style="color:red">Please Upload file with .pdf format only</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show"
                                    ValidationGroup="SubPercentage" OnClick="btnShow_Click" CssClass="btn btn-info" />
                                <asp:Button ID="btnSendEmail" runat="server" Text="Send Email" CssClass="btn btn-primary"
                                    TabIndex="12"
                                    ValidationGroup="sendEmail" OnClick="btnSendEmail_Click" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    CssClass="btn btn-warning" TabIndex="14" OnClick="btnCancel_Click" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="SubPercentage" Style="text-align: center" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="sendEmail" Style="text-align: center" />
                            </div>

                            <div class="col-12" runat="server" id="divStudentmeeting">
                                <asp:Panel ID="PnlStudentmeeting" runat="server" Visible="false">
                                    <asp:ListView ID="lvStudents" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Student List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sr. No.</th>
                                                        <th align="center">
                                                            <asp:CheckBox ID="cbHead" runat="server" Text="Select"
                                                                onclick="totAll(this)" TabIndex="10" />
                                                        </th>
                                                        <th>Univ.Reg. No.</th>
                                                        <th>Student Name </th>
                                                        <th>Father Mobile Number</th>
                                                        <th>Father Email id</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td style="text-align: center"><%#Container.DataItemIndex+1 %></td>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="cbRow" runat="server" TabIndex="11" />
                                                </td>

                                                <td><%# Eval("REGNO")%>
                                                    <asp:HiddenField ID="hdnidno" Value='<%# Eval("IDNO")%>' runat="server" />
                                                    <asp:HiddenField ID="hdnenroll" Value='<%# Eval("REGNO")%>' runat="server" />
                                                </td>
                                                <td><%# Eval("STUDNAME")%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblParMobile" Text='<%# Eval("FATHERMOBILE")%>'>  </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblParEmail" Text='<%# Eval("FATHER_EMAIL")%>'>  </asp:Label>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
        <Triggers>
             <asp:PostBackTrigger ControlID="btnSendEmail" />
        </Triggers>
    </asp:UpdatePanel>

    <br />

    <div id="divMsg" runat="server">
    </div>
    <script language="javascript" type="text/javascript">
        function totAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>
</asp:Content>
