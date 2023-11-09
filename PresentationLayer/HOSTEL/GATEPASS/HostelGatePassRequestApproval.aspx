<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HostelGatePassRequestApproval.aspx.cs" Inherits="HOSTEL_GATEPASS_HostelGatePassRequestApproval" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>

        

    </style>
    <div class="row" runat="server" id="DivShowRequest">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--style="display:flex;--%>
                    <h3 class="box-title">Hostel Gate Pass Request Approval</h3>
                </div>

                <div class="col-12"  >
                    <asp:ListView ID="lvReq" runat="server">
                        <LayoutTemplate>
                            <div id="demo-grid">
                                <div class="sub-heading">
                                    <h5>Requests List</h5>
                                </div>
                                <table class="table table-striped table-bordered nowrap dt-responsive display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th style="width: 10%;">NAME</th>
                                            <th style="width: 5%;">PURPOSE</th>
                                            <th style="width: 5%;">OUT</th>
                                            <th style="width: 5%;">IN</th>
                                            <th style="width: 5%;">REQUESTED DATE</th>
                                            <th style="width: 7%;">STATUS</th>
                                            <th style="width: 3%;">View Details</th>

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
                                    <asp:Label ID="lblname" runat="server" Text='<%# Eval("REGNO")+" - "+Eval("STUDNAME") %>'></asp:Label>
                                    <asp:HiddenField ID="hdnidno" runat="server" Value='<%# Eval("IDNO") %>' />
                                    <asp:HiddenField ID="hdnhgpid" runat="server" Value='<%# Eval("HGP_ID") %>' />
                                </td>
                                
                                <td>
                                    <%# Eval("PURPOSE_NAME") %>
                                </td>
                                <td>
                                    <%# Eval("OUT_TIME","{0:dd/MM/yyyy hh:mm tt}") %>
                                </td>
                                <td>
                                    <%# Eval("IN_TIME","{0:dd/MM/yyyy hh:mm tt}") %>
                                </td>
                                <td>
                                    <%# Eval("APPLY_DATE") %>
                                </td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server" Text=' <%# Eval("FINAL_STATUS") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-outline-primary" CommandArgument='<%# Eval("HGP_ID") %>' OnClick="btnShow_Click"   />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
    </div>
    <div class="row" runat="server" visible="false" id="DivShowRequestDetails">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Gatepass Request Details</h3>
                </div>

                <div class="box-body">
                    <div id="divStudent" runat="server">
                        <div class="col-12">

                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Session :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblSessionName" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Reg. No. :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblRegno" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Hostel Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblHostel" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Room :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblRoomName" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Passing Path :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblPassingpath" Font-Bold="true" runat="server" />
                                                <asp:HiddenField ID="hdnidno" runat="server" />
                                                <asp:HiddenField ID="hdnhgpid" runat="server" />
                                                <asp:HiddenField ID="hdnAttachmenturl" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Upload File:</b>
                                            <a class="sub-label">
                                                <div class="input-group">
                                                    <div class="custom-file"  >
                                                        <asp:FileUpload ID="FileAttach" runat="server" CssClass="custom-file-input" Width="275px"  />
                                                        <label class="custom-file-label" for="FileAttach">Choose file</label>
                                                    </div>
                                                </div>
                                            </a>
                                        </li>
                                       
                                        
                                    </ul>
                                </div>
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Semester :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblSemester" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Gender :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblGender" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Student Type :</b>
                                            <a class="sub-label">
                                                 <asp:Label ID="lblStudentType" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Apply Date :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblapplydate" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Remark :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblRemark" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>View Attachment :</b>
                                            <a class="sub-label">
                                                <asp:Button ID="btnShowAttachment" runat="server" CssClass="btn btn-primary text-center" Text="View" OnClick="btnShowAttachment_Click" Width="64px"  />
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        
                                        <li class="list-group-item"><b>Status:</b>
                                            <a class="sub-label">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" Height="18px" Width="138px" >
                                                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                    <asp:ListItem Value="A" Text="Approved"></asp:ListItem>
                                                    <asp:ListItem Value="R" Text="Reject"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvvalstatus" InitialValue="0" ControlToValidate="ddlStatus" ErrorMessage="Please Select Status" runat="server" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </a>
                                        </li>
                                         
                                    </ul>
                                </div>
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        
                                        
                                    </ul>
                                </div><br />
                                <div class="col-lg-12 col-md-6 col-12 text-center"  >
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-footer">
                    <p class="text-center">
                     <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ></asp:Button>
                     <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-danger" OnClick="btnBack_Click" Width="65px" />
                     <asp:ValidationSummary ID="VsSubmit" runat="server" Visible="false" ValidationGroup="submit"   DisplayMode="List" ShowValidationErrors="true"   />
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
    <script src="../../Itle/plugins/jQuery/jQuery-2.2.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $('.custom-file-input').on('change', function () {
                var fileName = $(this).val().split('\\').pop();
                $(this).next('.custom-file-label').html(fileName);
                $('.custom-file-label').css('overflow', 'hidden');
            });
        });
</script>

</asp:Content>
