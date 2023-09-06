<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PhdMeeting.aspx.cs" Inherits="ACADEMIC_PHD_PhdMeeting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updDist" DynamicLayout="true" DisplayAfter="0">
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
    <asp:UpdatePanel ID="updDist" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12  mt-3">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Meeting Title</label>
                                        </div>
                                        <asp:TextBox ID="txttitle" runat="server" ToolTip="Please Enter Meeting Title" TabIndex="1" MaxLength="128" AutoComplete="off" placeholder="Please Enter Name" CssClass="form-control"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txttitle"
                                            Display="None" ValidationGroup="Submit" ErrorMessage="Please Enter Meeting Title"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <label><span style="color: red">* </span>Meeting Date</label>
                                        <div class="input-group">
                                            <div class="input-group-addon">
                                                <div class="fa fa-calendar text-blue" id="icon"></div>
                                            </div>
                                            <asp:TextBox ID="txtStartDate" runat="server" TabIndex="2" CssClass="form-control" ToolTip="Please Enter Date" AutoComplete="off"></asp:TextBox>
                                        </div>
                                        <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtStartDate" PopupButtonID="icon" Enabled="true">
                                        </ajaxToolKit:CalendarExtender>

                                        <ajaxToolKit:MaskedEditExtender ID="meStartdate" runat="server" TargetControlID="txtStartDate"
                                            Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                            CultureTimePlaceholder="" Enabled="True" />
                                        <asp:RequiredFieldValidator ID="rfvStartdate" runat="server" ControlToValidate="txtStartDate"
                                            Display="None" ValidationGroup="Submit" ErrorMessage="Please Enter Date."></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Alloted Student</label>
                                        </div>
                                        <asp:ListBox ID="lboDesignation" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="lboDesignation"
                                            Display="None" ValidationGroup="Submit" ErrorMessage="Please Select Alloted Student" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Internal Supervisor</label>
                                        </div>
                                        <asp:ListBox ID="lboSupervisor" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="lboSupervisor"
                                            Display="None" ValidationGroup="Submit" ErrorMessage="Please Select Alloted Student" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>External Supervisor</label>
                                        </div>
                                        <asp:ListBox ID="lboextSupervisor" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="lboextSupervisor"
                                            Display="None" ValidationGroup="Submit" ErrorMessage="Please Select Alloted Student" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>MOM Document Upload</label>
                                        </div>
                                        <asp:FileUpload ID="fuDoc" runat="server" Width="220px" />
                                        <asp:Label ID="lblFileName" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblalert" runat="server" ForeColor="Red" Text="Note:-Upload file Below or Equal to 500 kb only !"></asp:Label>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Description</label>
                                        </div>
                                        <asp:TextBox ID="txtDescription" TabIndex="4" runat="server" TextMode="MultiLine" ToolTip="Please Enter Description" placeholder="Please Enter Description" Height="50px" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDescription"
                                            Display="None" ValidationGroup="Submit" ErrorMessage="Please Select Description"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <p class="text-center">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSave_Click" ToolTip="Click to Submit." TabIndex="5" ValidationGroup="Submit" OnClientClick="return validationMapping();" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" ToolTip="Click to cancel." TabIndex="6" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" Style="text-align: center" />
                                </p>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlMember" runat="server">
                                    <asp:ListView ID="lvMeeting" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>External Member List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <%--<th style="text-align: center;" runat="server" visible="false">Edit
                                                        </th>--%>
                                                        <th>Sr.No
                                                        </th>
                                                        <th>Meeting Title
                                                        </th>
                                                        <th>Meeting Date
                                                        </th>
                                                        <%--<th>Alloted Student
                                                        </th>--%>
                                                        <th>Download Document 
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
                                                <%--          <td runat="server" visible="false">
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="../../Images/edit.png"
                                                        CommandArgument='<%# Eval("MEETINGNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click" />
                                                </td>--%>
                                                <td>
                                                    <%# Container.DataItemIndex + 1%>
                                                </td>
                                                <td>
                                                    <%#Eval("MEETING_TITLE") %>
                                                </td>
                                                <td>
                                                    <%#Eval("MEETINGDATE") %>
                                                </td>
                                                <%-- <td>
                                                    <%#Eval("STUDNAME") %>
                                                </td>--%>
                                                <td>
                                                    <asp:UpdatePanel ID="updnpfPreview" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ImageButton ID="imgbtnpfPrevDoc" runat="server" CommandArgument='<%# Eval("FILENAME") %>' ImageUrl="~/images/downarrow.jpg" Text="Preview" ToolTip='<%# Eval("FILENAME") %>' Width="20px" Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>' OnClick="imgbtnpfPrevDoc_Click" />
                                                            <asp:Label ID="lblnpfPreview" Text='<%# Convert.ToString(Eval("FILENAME"))==string.Empty ?"Preview not available":"" %>' runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="imgbtnpfPrevDoc" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
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
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="lvMeeting" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>
    <script>
        function SetMapping(val) {

            $('#chkMapping').prop('checked', val);
        }
        function validationMapping() {
            debugger;

            var lboDesignation = $("[id$=lboDesignation]").attr("id");
            var lboSupervisor = $("[id$=lboSupervisor]").attr("id");
            var txttitle = document.getElementById('<%=txttitle.ClientID%>').value;
            var txtStartDate = document.getElementById('<%=txtStartDate.ClientID%>').value;
            var txtDescription = document.getElementById('<%=txtDescription.ClientID%>').value;
            //alert(lboDesignation);
            var lboDesignation = document.getElementById(lboDesignation);
            var lboSupervisor = document.getElementById(lboSupervisor);
            if (txttitle == "") {
                // alert('hg');
                alert('Please Enter Meeting Title.', 'Warning!');
                return false;
            } else if (txtStartDate == "") {
                // alert('fd');
                alert('Please Enter Date.', 'Warning!');
                return false;

            } else
                if (lboDesignation.value == 0) {
                    // alert('kd');
                    alert('Please Select Alloted Student', 'Warning!');
                    //  $(lboDesignation).focus();
                    return false;
                } else
                    if (lboSupervisor.value == 0) {
                        // alert('kd');
                        alert('Please Select Internal Supervisor', 'Warning!');
                        //  $(lboDesignation).focus();
                        return false;
                    } else
                        if (txtDescription == "") {
                            //alert('md');
                            alert('Please Select Description', 'Warning!');
                            return false;
                        }
                        else {

                            $('#hfdStart').val($('#chkMapping').prop('checked'));
                        }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSave').click(function () {
                    validationMapping();
                });
            });
        });

    </script>
</asp:Content>

