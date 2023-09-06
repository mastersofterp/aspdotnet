<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPProvisionalAdmission_Aproval.aspx.cs" Inherits="ACADEMIC_POSTADMISSION_ADMPProvisionalAdmission_Aproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

    <asp:UpdatePanel ID="upProvisionalADM" runat="server">
        <ContentTemplate>
            <div class="row">
                <asp:UpdateProgress ID="UpdateProgress4" runat="server"
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
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Provisional Admission Approval</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch </label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmissionBatch" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddlAdmissionBatch_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Program Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlProgramType" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlProgramType_SelectedIndexChanged" TabIndex="2" AutoPostBack="true">
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
                                        <asp:Panel runat="server" ID="pnlProg" Visible="false">
                                        <div class="label-dynamic">
                                            <sup id="">* </sup>
                                             <label id="lblSubProgBranch">Program/ Branch </label>
                                           <%-- <asp:Label runat="server" ID="lblSubProgBranch" Font-Bold="true"> *Program/ Branch</asp:Label>--%>
                                        </div>
                                        <asp:ListBox ID="lstProgram" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" TabIndex="6" AutoPostBack="true"></asp:ListBox>
                                   </asp:Panel>
                                   </div>
                                  
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Student List" TabIndex="5" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="5" CssClass="btn btn-primary"  OnClick="btnSubmit_Click" Visible="false"/>
                             <%--   <asp:Button ID="btnGenerateAdmissionNote" runat="server" Text="Generate Admission Note" TabIndex="6" CssClass="btn btn-primary" OnClick="btnGenerateAdmissionNote_Click" Visible="false" />
                                <asp:Button ID="btnSendEMail" runat="server" Text="Send E-Mail" TabIndex="7" CssClass="btn btn-primary" Visible="false" />
                                <asp:Button ID="btnPrintAdmissionNote" runat="server" Text="Print Admission Note" TabIndex="8" CssClass="btn btn-primary" Visible="false" />--%>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="9" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                            </div>
                            <asp:Panel runat="server" ID="pnlCount" Width="100%" Visible="False">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group form-inline col-lg-4 col-md-4 col-4">
                                            <div class="label-dynamic">
                                                <label style="color: red">Total Students :- </label>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtTotalCount" Font-Bold="true" Enabled="false" ForeColor="Green" CssClass="ml-2"> </asp:TextBox>
                                        </div>
                                        <div class="form-group form-inline col-lg-4 col-md-4 col-4">
                                            <div class="label-dynamic">
                                                <label style="color: red">Fees Allowed to Pay Students :- </label>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtPaidStudent" Font-Bold="true" Enabled="false" ForeColor="Green" CssClass="ml-2"> </asp:TextBox>
                                        </div>
                                         <div class="form-group form-inline col-lg-4 col-md-4 col-4">
                                            <div class="label-dynamic">
                                                <label style="color: red">Fess UnPaid Students :- </label>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtUnPaidStudent" Font-Bold="true" Enabled="false" ForeColor="Green" CssClass="ml-2"> </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlPaymentCat" runat="server" Visible="false">
                            <div class="col-12">
                                    <div class="row">
                                        <div class="form-group form-inline col-lg-4 col-md-4 col-4">
                                            <div class="label-dynamic">
                                                <label style="color: red">Payment Category :- </label>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtPaymentCatgory" Font-Bold="true" Enabled="false" ForeColor="Green" CssClass="ml-2"> </asp:TextBox>
                                        </div>
                                        </div>
                                </div>
                                </asp:Panel>
                            <asp:Panel ID="pnlGV1" runat="server" Visible="false">
                                <div class="col-12">
                                    <asp:GridView ID="gvProvADM" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvProvADM_RowDataBound" class="table table-striped table-bordered nowrap" Style="width: 100%">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                 <asp:CheckBox ID="chkAll" runat="server" onclick="ToAllPayment(this);" /></th>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                     <asp:CheckBox ID="chkRecon" OnCheckedChanged="chkRecon_CheckedChanged" runat="server" AutoPostBack="true" Checked='<%#(Convert.ToInt32(Eval("Paystatus"))== 1 ? true : false) %>'  />
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
                                                    <asp:HiddenField ID="hdfUserNo" runat="server" Value='<%# Eval("USERNO") %>' />
                                                    <asp:HiddenField ID="hdfBatchNo" runat="server" Value='<%# Eval("BATCHNO") %>' />
                                                    <asp:HiddenField ID="hdfDegreeNo" runat="server" Value='<%# Eval("DEGREENO") %>' />  
                                                     <asp:HiddenField ID="hdfBranchNo" runat="server" Value='<%# Eval("BRANCHNO") %>' />    
                                                     <asp:HiddenField ID="hdfPaymentTypeNo" runat="server" Value='<%# Eval("PAYMENTTYPENO") %>' />      
                                                     <asp:HiddenField ID="hdfIDNO" runat="server" Value='<%# Eval("IDNO") %>' />                                         
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="600px" HeaderText="Application Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApplicationId" runat="server" Text='<%#Eval("APPLICATION_ID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="600px" HeaderText="STUDENT NAME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStudentName" runat="server" Text='<%#Eval("STUDENTNAME")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="600px" HeaderText="DEGREE NAME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDegreeName" runat="server" Text='<%#Eval("DEGREENAME")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="600px" HeaderText="BRANCH NAME">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true" Enabled="false" TabIndex="2" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged1">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField ItemStyle-Width="600px" HeaderText="PAYMENT TYPE">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlPayment" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlPayment_SelectedIndexChanged" Enabled="false"
                                                        TabIndex="2" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="600px" HeaderText="PAYMENT Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Convert.ToString(Eval("AMOUNT")) %>' ></asp:Label>
                                                       <asp:HiddenField ID="hdnStandardFee" runat="server" Value='<%# Eval("STANDARDFEE") %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField ItemStyle-Width="600px" HeaderText="Payment Status">
                                                <ItemTemplate>
                                                      <asp:Label ID="lblPaymentStatus" runat="server" Text='<%# Convert.ToInt32(Eval("Paystatus"))== 1 ? "Allowed":"Not Allowed" %>' ForeColor='<%#Convert.ToInt32(Eval("Paystatus"))==1?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>                                                     
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
      <script type="text/javascript">
          function ToAllPayment(headchk) {
              debugger;
              var sum = 0;
              var frm = document.forms[0]
              try {
                  for (i = 0; i < document.forms[0].elements.length; i++) {
                      var e = frm.elements[i];
                      if (e.type == 'checkbox') {
                          if (headchk.checked == true) {
                              // SumTotal();
                              // var j = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvFailCourse_ctrl' + i + '_lblAmt').innerText);
                              //// alert(j);
                              // sum += parseFloat(j);
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

                          // x = sum.toString();
                      }

                  }
              }
              catch (err) {
                  alert("Error : " + err.message);
              }
          }
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
</asp:Content>
