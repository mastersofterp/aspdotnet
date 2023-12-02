<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ConvocationEligibleStudReport.aspx.cs" Inherits="ACADEMIC_EXAMINATION_TabulationChart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlExam"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="   ">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updpnlExam" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Convocation / Passout Student List</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College & Scheme</label>
                                            <%--<asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" TabIndex="1"
                                            data-select2-enable="true" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" TabIndex="2" data-select2-enable="true" runat="server" AutoPostBack="true" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College/Session</label>
                                        </div>
                                        <asp:ListBox ID="ddlCollege" runat="server" AppendDataBoundItems="true" ValidationGroup="configure" TabIndex="1"
                                            CssClass="form-control multi-select-demo" SelectionMode="multiple" AutoPostBack="true"></asp:ListBox>
                                    </div>

                                </div>
                            </div>

                            <div class="col-12 btn-footer" style="text-align: center;">
                                <asp:Button ID="btnConvocationExcelReport" Text="Convocation Excel Report" runat="server" TabIndex="2" Visible="false" CssClass="btn btn-info" CausesValidation="false" OnClick="btnConvocationExcelReport_Click1" />
                                <asp:Button ID="btnPassStudList" Text="Passout Student Excel Report" runat="server" TabIndex="3" Visible="false" CssClass="btn btn-info" OnClientClick="return validateField();" CausesValidation="false" OnClick="btnConvocationEligible_Click" />

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnConvocationExcelReport" />
            <asp:PostBackTrigger ControlID="btnPassStudList" />
        </Triggers>
    </asp:UpdatePanel>
        
    <!-- MultiSelect Script -->
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

    <script language="javascript" type="text/javascript">

        function validateField() {

            debugger;

            var summary = "";

            summary += isvalidCollege();
            //summary += isvalidSession();


            if (summary != "") {
                alert(summary);
                return false;
            }
            else {
                return true;
            }
        }
        function isvalidCollege() {
            debugger;
            var uid;
            var temp = document.getElementById("<%=ddlCollege.ClientID %>");
            uid = temp.value;
            if (uid == 0) {
                return ("Please Select College & Session" + "\n");
            }
            else {
                return "";
            }
        }  
    </script>

</asp:Content>
