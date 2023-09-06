<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AdmissionStatusMaster.aspx.cs" Inherits="ACADEMIC_AdmissionStatusMaster" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

      <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
   <asp:HiddenField ID="hfdStart" runat="server" ClientIDMode="Static" />
   <asp:UpdatePanel ID="updstatus" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Admission Status Master</h3>
                           <%-- <h3 class="box-title">--%>
                                <%--<asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>--%>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                      <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Admission Status Description</label>
                                                                </div>
                                                                <asp:TextBox ID="txtstatusdescp" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="100" TabIndex="1"
                                                                    ToolTip="Please Enter Admission Status Description" placeholder="Enter Admission Status Description" />
                                                                <asp:RequiredFieldValidator ID="rfvStatusdescp" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Enter Admission Status Description" ControlToValidate="txtstatusdescp"
                                            Display="None" ValidationGroup="show" />
                                                            </div>
                                  
                                                        <div class="form-group col-lg-2 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Status</label>
                                                                </div>
                                                                <div class="switch form-inline">
                                                                    <input type="checkbox" id="rdActive" name="switch" checked />
                                                                    <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                                                </div>
                                                                
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label></label>
                                                                </div>
                                                                <asp:CheckBox ID="chkIsAdmcancel" runat="server" CssClass="checkbox" />
                                                                <label>Is Consider for Admission Cancel</label>
                                                            </div>
                                  
                                   
                                  
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                               
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit"  TabIndex="2" OnClick="btnSubmit_Click"
                                    ValidationGroup="show" CssClass="btn btn-primary" OnClientClick="return validate();" />

                            
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"  TabIndex="3" OnClick="btnCancel_Click"
                                    ToolTip="Cancel Selected under Selected Criteria." CssClass="btn btn-warning"  />
                            </div>


                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="show" DisplayMode="List" />


                                   <div class="col-12" >                                   
                                    <asp:ListView ID="lvAdmissionstatus" runat="server">

                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                             <h5></h5>
                                           </div>
                                             <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="lstTable">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                       
                                                        <th>
                                                           Action
                                                        </th>
                                                        <th>Admission Status Description
                                                        </th>
                                                        <th>Is Active
                                                            
                                                        </th>
                                                        <th>
                                                           Is Consider for Admission Cancel
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
                                               <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("STUDENT_ADMISSION_STATUS_ID")%>'
                                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click"/>
                                                </td>
                                                <td>
                                                  <%# Eval("STUDENT_ADMISSION_STATUS_DESCRIPTION")%>
                                                </td>
                                                  <td>
                                                   <asp:Label ID="lblIsActive" runat="server" Text='<%# Eval("ACTIVE_STATUS")%>' ForeColor='<%# Eval("ACTIVE_STATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                    </td>
                                                     <td>
                                                     <asp:Label ID="lblIsFinancialYear" runat="server" Text='<%# Eval("IS_ADM_CANCEL")%>' ForeColor='<%# Eval("IS_ADM_CANCEL").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                     </td>
                                            
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>

                           
                        </div>
                    </div>
                </div>
                 <div id="divMsg" runat="server">
                            </div>
            </div>
        </ContentTemplate>
       <%-- <Triggers>
            <asp:PostBackTrigger  ControlID="btnSubmit" />
          
        </Triggers>--%>
    </asp:UpdatePanel>
  
        <script>
       function SetStatActive(val) {
           $('#rdActive').prop('checked', val);
       }
        function SetStatStart(val) {
            $('#rdStart').prop('checked', val);
        }

        function validate() {

            $('#hfdActive').val($('#rdActive').prop('checked'));
            $('#hfdStart').val($('#rdStart').prop('checked'));

            var idtxtweb = $("[id$=txtstatusdescp]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Admission Status Description', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }

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


    </asp:Content>