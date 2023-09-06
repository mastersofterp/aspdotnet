<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPDocumentVariefication.aspx.cs" Inherits="ACADEMIC_POSTADMISSION_ADMPDocumentVariefication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>
        <style>
        .EU_DataTable {
            border: 1px solid #e5e5e5;
        }

            .EU_DataTable th, .EU_DataTable td {
                padding: 5px 8px;
            }
    </style>
    <asp:UpdatePanel ID="updDocumentVerification" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfdShowStatus" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnScheduleNo" runat="server" Value="0" />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Document Verification</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch </label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmissionBatch" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>                                    
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Program Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlProgramType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlProgramType_SelectedIndexChanged" >
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                            <asp:ListItem Value="1">UG</asp:ListItem>
                                            <asp:ListItem Value="2">PG</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree </label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Program/Branch </label>
                                        </div>
                                        <asp:ListBox ID="lstProgram" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" TabIndex="6" AutoPostBack="true"></asp:ListBox>
                                    </div>                                                        
                                </div>
                            </div>
                        </div>

                           <div class="col-12 btn-footer">
                                 <asp:Button ID="btnShow" runat="server" Text="Show Students" TabIndex="5" CssClass="btn btn-primary" OnClick="btnShow_Click"  />  
                                 <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="7" CssClass="btn btn-primary" OnClick="btnSubmit_Click" OnClientClick="return validate();" />
                                 <asp:Button ID="btnSendMail" runat="server" Text="Send Mail" TabIndex="7" CssClass="btn btn-primary" Visible="false" />
                                 <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="8" CssClass="btn btn-warning" OnClick="btnCancel_Click" />                                                   
                            </div>

                      

                        <asp:Panel ID="pnlGV1" runat="server" Visible="false">
                          <div class="col-12">
                                 <asp:GridView ID="gvDocVerification" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvDocVerification_RowDataBound" class="table table-striped table-bordered nowrap" Style="width: 100%">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                <%-- <asp:CheckBox ID="chkAll" runat="server" onclick="ToAllPayment(this);" /></th>--%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                      <asp:CheckBox ID="chkCallLrt" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="SR.NO">
                                                <ItemTemplate>                                          
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="600px" HeaderText="Admission Batch">
                                                <ItemTemplate>
                                             <asp:Label ID="lblAdmBatch" runat="server" Text='<%#Eval("ADMBATCH")%>'></asp:Label>
                                            <asp:HiddenField ID="hdnAdmBatch" runat="server"  Value='<%#Eval("BATCHNO")%>' />
                                            <asp:HiddenField ID="hdnDegreeNo" runat="server"  Value='<%# Eval("DEGREENO") %>' />
                                            <asp:HiddenField ID="hdnBranchNo" runat="server"  Value='<%# Eval("BRANCHNO") %>' />
                                            <asp:HiddenField ID="hdnUserNo" runat="server"  Value='<%# Eval("USERNO") %>'/>   
                                           <asp:HiddenField ID="hdnStatus" runat="server"  Value='<%# Eval("DOCSTATUS") %>'/>                                       
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField ItemStyle-Width="600px" HeaderText="STUDENT NAME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStudentName" runat="server" Text='<%#Eval("STUDENTNAME")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="600px" HeaderText="EMAIL ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EMAILID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="600px" HeaderText="Degree Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDegreeName" runat="server" Text='<%# Convert.ToString(Eval("DEGREENAME")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="600px" HeaderText="Branch Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBranchName" runat="server" Text='<%# Convert.ToString(Eval("BRANCHNAME")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="600px" HeaderText="Document Status">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="Please Select" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                        <%--    <asp:ListView ID="lvSchedule" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <div id="demo-grid">
                                        <table id="tbllist" class="dataTable table table-bordered table-striped table-hover" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="width: 5%">
                                                    <asp:CheckBox ID="chkAll" runat="server" onclick="totAllSubjects(this);" />
                                                    </th>
                                                    <th style="width: 5%">Admission Batch</th>
                                                    <th style="width: 10%">Student Name</th>
                                                    <th style="width: 10%">Email Id</th>
                                                     <th style="width: 20%">Degree Name</th>
                                                    <th style="width: 20%">Program/Branch</th>
                                                    <th style="width: 15%">Status</th>                                                    
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
                                        <asp:CheckBox ID="chkCallLrt" runat="server" />
                                            <asp:HiddenField ID="hdnAdmBatch" runat="server"  Value='<%#Eval("BATCHNO")%>' />
                                            <asp:HiddenField ID="hdnDegreeNo" runat="server"  Value='<%# Eval("DEGREENO") %>' />
                                            <asp:HiddenField ID="hdnBranchNo" runat="server"  Value='<%# Eval("BRANCHNO") %>' />
                                            <asp:HiddenField ID="hdnUserNo" runat="server"  Value='<%# Eval("USERNO") %>'/>
                                        </td>                          
                                        <td><%# Eval("ADMBATCH") %></td>
                                        <td><%# Eval("STUDENTNAME") %></td>
                                        <td><%# Eval("EMAILID") %></td>
                                        <td><%# Eval("DEGREENAME") %></td>
                                        <td><%# Eval("BRANCHNAME") %></td>
                                        <td>
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Please Select" Value="2"></asp:ListItem>
                                                 <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                                 <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>                               
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>--%>
                        </div>
                            </asp:Panel>                     
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger  ControlID="btnSubmit"/>
        </Triggers>
    </asp:UpdatePanel>

    <script>
        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }

        function validate() {
            // alert('Hii');
            $('#hfdShowStatus').val($('#rdActive').prop('checked'));
            //alert($('#hfdShowStatus').val());
            //if (Page_ClientValidate()) {
            //    alert('Inside');
            //    //$('#hfdShowStatus').val($('#rdActive').prop('checked'));
            //    alert($('#hfdShowStatus').val());
            //}
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });
    </script>

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
    <script type="text/javascript">
        function totAllSubjects(headchk) {
            debugger;
            var sum = 0;
            var frm = document.forms[0]
            try {
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    var e = frm.elements[i];
                    if (e.type == 'checkbox') {
                        if (headchk.checked == true) {
                            if (e.disabled == false) {
                                e.checked = true;
                            }
                        }
                        else {
                            if (e.disabled == false) {
                                e.checked = false;
                                headchk.checked = false;
                            }
                        }

                    }

                }
            }
            catch (err) {
                alert("Error : " + err.message);
            }
        }
    </script>
</asp:Content>



