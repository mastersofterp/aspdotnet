<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="NoduesApprovalReport.aspx.cs" Inherits="ACADEMIC_NoduesApprovalReport" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updTeach"
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
    <asp:UpdatePanel ID="updTeach" runat="server">

        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <%--<asp:Label ID="lblHeading" runat="server" Text=""></asp:Label>--%>
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Passout Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlPassBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlPassBatch" ValidationGroup="Show" Display="None"
                                            ErrorMessage="Please Select Passout Batch" InitialValue="0" SetFocusOnError="true" />
                                    </div>

                                   
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShowapprovedstud" runat="server" CssClass=" btn btn-info"
                                    ValidationGroup="Show" Text="Show Student List" OnClick="btnShowapprovedstud_Click"  />

                                <asp:Button ID="btnsavestatus" runat="server" CssClass="btn btn-primary" Text="Submit Status" Visible="false"
                                              />
                                <asp:ValidationSummary ID="VSCaution" runat="server" ValidationGroup="Show" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" />
                            </div>
                           <asp:HiddenField ID="hfcount" runat="server" />
                              <div class="col-12">
                                <div class="row" id="divshow" runat="server" visible="false">

                                       <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Select to Show No Dues Status Student List </label>
                                        </div>
                                        <asp:DropDownList ID="ddlstatus" CssClass="form-control" runat="server" 
                                            data-select2-enable="true"  AutoPostBack="true" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Approved</asp:ListItem>
                                            <asp:ListItem Value="0">Pending</asp:ListItem>

                                        </asp:DropDownList>                                   
                                    </div>
                                                             
                                        
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Status" ShowMessageBox="true"
                                            ShowSummary="false" DisplayMode="List" />
                                    
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-12">
                                    <div id="dvListView">
                                        <asp:ListView ID="lvNoDuesApproved" runat="server">
                                            <LayoutTemplate>
                                                <div id="demo-grid">
                                                    <div class="sub-heading">
                                                        <h5>STUDENT LIST</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="example2">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Sr No.</th>
                                                                <th>
                                                                    <asp:Label ID="lblDYRNo" runat="server" Font-Bold="true">
                                                                    </asp:Label></th>
                                                                <th>Student Name</th>
                                                                <th>Apply Date</th>
                                                                <th>
                                                                    <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                </th>
                                                                <th>
                                                                    No Dues/Dues Pending Status
                                                                </th>
                                                                <th>View Report</th>
                                                                <%--<th>Download</th>--%>
                                                               
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
                                                        <%#Container.DataItemIndex+1%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRegno" runat="server" Text='<%# Eval("REGISTER NO")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label></td>
                                                    <td>
                                                        <asp:LinkButton ID="btnName" runat="server" Text='<%# Eval("STUDENT NAME")%>' ToolTip='<%# Eval("EMAILID")%>' Enabled="false"> </asp:LinkButton></td>
                                                    <td><%# Eval("APPLY DATE","{0: dd/MM/yyyy}")%></td>
                                                    <td><%# Eval("DEGREENAME")%></td>
                                                    <td><%# Eval("LONGNAME")%></td>
                                                    <td>
                                                        <asp:Label ID="lblnodues" runat="server" Text='<%# Eval("STATUS")%>' ToolTip='<%# Eval("APPROVED")%>' 
                                                            ForeColor='<%# (Eval("STATUS").ToString() == "Pending" ?System.Drawing.Color.Red:System.Drawing.Color.Green)%>' ></asp:Label></td>
                                                    <td>
                                                    <asp:Button ID="btnviewreport" CssClass="btn btn-primary" runat="server" Text="View" OnClick="btnviewreport_Click" CommandArgument='<%# Eval("IDNO") %>'/>  
                                                    </td>
                                                     <asp:HiddenField ID="hfidno" runat="server" Value='<%# Eval("IDNO")%>' />
                                                   <%--<asp:LinkButton ID="btnprint" runat="server" Text="Download" ></asp:LinkButton>--%>
                                                    <%-- <td>
                                                          
                                                      <asp:Button ID="btnDownload" runat="server" Text="Download"  CssClass="btn btn-info" OnClick="btnDownload_Click"  CommandArgument='<%# Eval("IDNO") %>'/>
                                                     </td>--%>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                        <asp:HyperLink ID="lnkDown" runat="server" Target="_blank"></asp:HyperLink>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnShowapprovedstud" />--%>
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        //function Show() {
        //    //$("#modalBank").modal('Show');
        //    $('#modalBank').modal('show');
        //}
        function Validate() {
            var isValid = $("#dvListView input[type=checkbox]:checked").length > 0;
            if (!isValid) {
                alert("Please Select atleast one Student.");
            }
            return isValid;
        }

        function SelectAll(cbSAll) {
            var i = 0;
            var hftot = document.getElementById('<%= hfcount.ClientID %>').value;
            var count = 0;
            for (i = 0; i < Number(hftot) ; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvCautionMoneyApproved_ctrl' + i + '_chkRow');
                if (lst.type == 'checkbox') {
                    if (cbSAll.checked == true) {
                        if (lst.disabled == false) {
                            lst.checked = true;
                            count = count + 1;
                        }
                    }
                    else {
                        lst.checked = false;
                    }
                }
            }
        }

    </script>
</asp:Content>


