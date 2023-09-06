<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Complaint_Allotment.aspx.cs"
    Inherits="Estate_Complaint_Allotment" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div class="row" runat="server" id="tblMain">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SERVICE REQUEST WORKOUT DETAILS</h3>
                        </div>
                        <asp:Panel ID="pnl" runat="server">
                            <div class="box-body">

                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Service Request Workout Details</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Service Requester Date</label>
                                            </div>
                                            <asp:TextBox ID="txtWorkoutDate" CssClass="form-control" runat="server" disabled="true" TabIndex="1"></asp:TextBox>
                                            <%-- &nbsp;<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" />
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtWorkoutDate" PopupButtonID="Image1">
                                                        </ajaxToolKit:CalendarExtender>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Preferable Date for Visit/Contact </label>
                                            </div>


                                            <asp:TextBox ID="txtComplaintDt" runat="server" CssClass="form-control" TabIndex="1" Enabled="false"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfvtxtComplaintDt" runat="server"
                                                        ControlToValidate="txtComplaintDt" Display="None" ErrorMessage="Please Select Complaint Date"
                                                        ValidationGroup="complaint"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Preferable Time From</label>
                                            </div>

                                            <asp:TextBox ID="txtPerferTime" runat="server" CssClass="form-control" Text=" " TabIndex="1" Enabled="false"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Preferable Time To</label>
                                            </div>
                                            <asp:TextBox ID="txtPerferTo" runat="server" CssClass="form-control" Text=" " TabIndex="1" Enabled="false"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Service Requester No</label>
                                            </div>

                                            <asp:TextBox ID="lblCompNo" CssClass="form-control" runat="server" disabled="true" TabIndex="1"></asp:TextBox>
                                            <%-- <asp:Label ID="lblCompNo" runat="server" Font-Bold="True"></asp:Label>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Requester Name</label>
                                            </div>
                                            <asp:TextBox ID="lblComplainteeName" CssClass="form-control" runat="server" disabled="true" TabIndex="1"></asp:TextBox>
                                            <%-- <asp:Label ID="lblComplainteeName" runat="server" Text=""></asp:Label>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Requester Location </label>
                                            </div>
                                            <asp:TextBox ID="lblLocation" CssClass="form-control" runat="server" disabled="true" TabIndex="1"></asp:TextBox>
                                            <%-- <asp:Label ID="lblLocation" runat="server" Text=""></asp:Label>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Requester Mobile No. </label>
                                            </div>
                                            <asp:TextBox ID="lblContactNo" CssClass="form-control" runat="server" disabled="true" TabIndex="1"></asp:TextBox>
                                            <%--<asp:Label ID="lblContactNo" runat="server" Text=""></asp:Label>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Department Remark</label>
                                            </div>
                                            <%--<asp:Label ID="lblRemark" runat="server" Text=""></asp:Label>--%><%--Requester Cell Remark:--%>
                                            <asp:TextBox ID="lblRemark" CssClass="form-control" runat="server" disabled="true" TabIndex="1"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Requester Details</label>
                                            </div>
                                            <asp:TextBox ID="txtcomplaintsdt" MaxLength="350" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine" TabIndex="1" />
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDetails"
                                                    Display="None" ErrorMessage="Please Enter Requester Details." ValidationGroup="complaint"></asp:RequiredFieldValidator>--%>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Complaintpwd" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <%--  <label>Priority Of Work<span style="color: red;">*</span>:</label>--%>
                                            <asp:TextBox ID="txtcomplaintpwd" MaxLength="100" runat="server" CssClass="form-control" Enabled="false" Visible="false" TabIndex="1" />
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDetails"  
                                                        Display="None" ErrorMessage="Please Enter Complaint Details." ValidationGroup="complaint"></asp:RequiredFieldValidator>   --%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Action Taken </label>
                                            </div>
                                            <asp:TextBox ID="txtDetails" MaxLength="3000" runat="server" TabIndex="1" TextMode="MultiLine" CssClass="form-control" />
                                            <%-- <asp:RequiredFieldValidator ID="rfvDetails" runat="server" ControlToValidate="txtDetails" Display="None" ErrorMessage="Please Enter Workout Details."
                                                        ValidationGroup="complaint"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Service Request Type </label>
                                            </div>
                                            <asp:DropDownList ID="ddlRMCompnature" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="1" data-select2-enable="true">
                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvcompnature" runat="server" ControlToValidate="ddlRMCompnature" Display="None" ErrorMessage="Please Select Service Request Type."
                                                ValidationGroup="complaint" InitialValue="-1"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Work Assigned To</label>
                                            </div>
                                            <asp:TextBox ID="txtWorkAllotedTo" MaxLength="350" runat="server"
                                                CssClass="form-control" TabIndex="1" />
                                            <asp:RequiredFieldValidator ID="rfvWorkAllotedTo" runat="server" ControlToValidate="txtWorkAllotedTo" Display="None" ErrorMessage="Please Enter Work Assigned To"
                                                ValidationGroup="complaint"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Service Status </label>
                                            </div>
                                            <asp:RadioButton ID="rdocomp1" runat="server" GroupName="rdocomp" Text="Incomplete" Checked="true" TabIndex="1" />
                                            <asp:RadioButton ID="rdocomp2" runat="server" GroupName="rdocomp" Text="Completed" TabIndex="1" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divReply" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Service Requester Reply</label>
                                            </div>
                                            <asp:TextBox ID="txtReply" runat="server" TabIndex="8" CssClass="form-control" ReadOnly="true" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divReallotment" visible="false">
                                            <div class="label-dynamic">
                                                <label>Department Reallotment Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtReallotment" CssClass="form-control" runat="server" disabled="true"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divAction" visible="false">
                                            <div class="label-dynamic">
                                                <%--  <sup>*</sup>--%>
                                                <label>Reworked </label>
                                            </div>
                                            <asp:TextBox ID="txtRework" MaxLength="3000" runat="server" TabIndex="1" TextMode="MultiLine" CssClass="form-control" />
                                            <%-- <asp:RequiredFieldValidator ID="rfvDetails" runat="server" ControlToValidate="txtDetails" Display="None" ErrorMessage="Please Enter Workout Details."
                                                        ValidationGroup="complaint"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <%--  <div class="col-md-3">
                                                      <asp:RadioButton ID="rdocomp2" runat="server" GroupName="rdocomp" Text="Completed" />
                                                     </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="chkDiv" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Is Available </label>
                                            </div>
                                            <asp:CheckBox ID="chkAvailableItem" runat="server" Text="" TabIndex="1" />
                                        </div>

                                    </div>
                                </div>


                                <asp:Panel ID="pnlAdditem" runat="server">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Add Items and Quantity</h5>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Select Item </label>
                                                </div>
                                                <asp:DropDownList ID="ddlitemlist" AppendDataBoundItems="true" TabIndex="1" runat="server" CssClass="form-control"
                                                    data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlitemlist_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvitemlist" runat="server" ControlToValidate="ddlitemlist" Display="None" ErrorMessage="Please select Item."
                                                    ValidationGroup="AddItems" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Item Unit </label>
                                                </div>

                                                <asp:Label ID="lblItemUnit" runat="server" CssClass="form-control"></asp:Label>
                                                <%--<asp:DropDownList ID="ddlItemUnit" runat="server" CssClass="form-control" Enabled="false"
                                                                                AppendDataBoundItems="true" TabIndex="4">
                                                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                                                <asp:ListItem Value="Nos">Nos</asp:ListItem>
                                                                                <asp:ListItem Value="Kg">Kg</asp:ListItem>
                                                                                <asp:ListItem Value="Ltr">Ltr</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                                ControlToValidate="ddlItemUnit" ErrorMessage="Please Enter Item Unit."
                                                                                ValidationGroup="CItem" Display="None" InitialValue="-1"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Item Quantity </label>
                                                </div>

                                                <asp:TextBox ID="txtItemQuantity" MaxLength="10" TabIndex="1" runat="server"
                                                    CssClass="form-control" />
                                                <asp:RequiredFieldValidator ID="rfvquantity" runat="server" MaxLength="9" ControlToValidate="txtItemQuantity" Display="None" ErrorMessage="Please enter quantity."
                                                    ValidationGroup="AddItems"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTxtExtmobileno" runat="server"
                                                    FilterType="Numbers" TargetControlID="txtItemQuantity" ValidChars="">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Is Available </label>
                                                </div>

                                                <asp:CheckBox ID="chkItem" runat="server" Checked="true" TabIndex="1" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add Items" ValidationGroup="AddItems" OnClick="btnAdd_Click1"
                                            CssClass="btn btn-primary" TabIndex="1" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="AddItems"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>

                                    <div class="col-12">
                                        <asp:ListView ID="lvitems" runat="server">
                                            <EmptyDataTemplate>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading">
                                                        <h5>ADDED ITEMS LIST</h5>
                                                    </div>

                                                    <table class="table table-hover table-bordered table-striped display" style="width: 100%">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th id="delete" runat="server">SELECT/ DELETE</th>
                                                                <th>Available/Not Available                                                                                                            
                                                                </th>
                                                                <th>ITEMS</th>
                                                                <th>ITEM UNIT</th>
                                                                <th>QUANTITY</th>
                                                                <th id="itemid" runat="server" visible="false">ITEMS ID</th>
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
                                                        <asp:ImageButton ID="btnEditItem" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("CWID")%>'
                                                            AlternateText="Edit Record" ToolTip="Work Out" OnClick="btnEditItem_Click" />
                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("CWID") %>' ImageUrl="~/Images/delete.png"
                                                            OnClick="btnDelete_Click" OnClientClick="return DeleteItem()" ToolTip="Delete Record" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkItem" runat="server" Checked='<%#Eval("IS_AVAILABLE").ToString() == "0" ? false : true   %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEMNAME")%>'></asp:Label>
                                                    </td>
                                                    <td><%# Eval("ITEMUNIT")%></td>
                                                    <td><%# Eval("QTYISSUED")%></td>
                                                    <td id="lbiitem" runat="server" visible="false">
                                                        <asp:Label ID="lblitemId" runat="server" Visible="false" Text='<%# Eval("ITEMID")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>

                                </asp:Panel>


                                <div id="DivAttch" runat="server" visible="false">
                                    <div class="col-12">
                                        <label>
                                            <span style="color: #FF0000">Valid files : (.jpg, .bmp, .gif, .png, .pdf, .xls, .doc,.zip, .txt, .docx, .xlsx should be of 100 kb size.)</span>
                                        </label>
                                    </div>

                                    <div class="col-12">
                                        <div class="row">
                                            <asp:UpdatePanel ID="updfile" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Attach File </label>
                                                        </div>
                                                        <asp:FileUpload ID="FileUpload1" runat="server" TabIndex="1" ValidationGroup="complaint" ToolTip="Select file to upload" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label></label>
                                                        </div>
                                                        <asp:Button ID="btnAddfile" runat="server" Text="Add" TabIndex="1" OnClick="btnAddfile_Click"
                                                            ValidationGroup="complaint" CssClass="btn btn-primary"
                                                            CausesValidation="False" />
                                                        <asp:Label ID="lblResult" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnAddfile" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>





                                <div id="listfile" runat="server" visible="false">

                                    <div class="col-12">
                                        <asp:ListView ID="lvfile" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">

                                                    <div class="sub-heading">
                                                        <h5>Download files</h5>
                                                    </div>
                                                    <table class="table table-hover table-bordered table-striped display" style="width: 100%">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Action
                                                                </th>
                                                                <th>File Name
                                                                </th>
                                                                <th>Service Request No
                                                                </th>
                                                                <th>Download
                                                                </th>

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
                                                        <asp:ImageButton ID="btnAttachDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%#DataBinder.Eval(Container, "DataItem") %>'
                                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnAttachDelete_Click"
                                                            OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" />
                                                    </td>
                                                    <td>
                                                        <%#GetFileName(DataBinder.Eval(Container, "DataItem")) %>
                                                    </td>
                                                    <td>
                                                        <%#GetFileNameCaseNo(DataBinder.Eval(Container, "DataItem")) %>
                                                    </td>
                                                    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgFileDownload" runat="Server" ImageUrl="~/images/action_down.png"
                                                                    AlternateText='<%#DataBinder.Eval(Container, "DataItem") %>' ToolTip='<%#DataBinder.Eval(Container, "DataItem")%>'
                                                                    OnClick="imgFileDownload_Click" />
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="imgFileDownload" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>

                                </div>


                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="complaint" OnClick="btnSave_Click" CssClass="btn btn-primary" TabIndex="1" />
                                    <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="complaint" OnClick="btnReport_Click" CssClass="btn btn-info" TabIndex="1" />
                                    <%--  <asp:Button ID="btnClear" runat="server" Text="Cancel"  OnClick="btnClear_Click" CssClass="btn btn-warning" TabIndex="18" />--%>
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="1" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="complaint" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                                <div>
                                    <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                        <ProgressTemplate>
                                            <div id="preloader">
                                                <div id="loader-img">
                                                    <div id="loader">
                                                    </div>
                                                    <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                                                </div>
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>--%>
                                </div>
                                <asp:Panel ID="PnlCompAllot" runat="server">
                                </asp:Panel>
                                <%-- <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                     <ContentTemplate>--%>
                                <div class="col-12">
                                    <asp:Panel ID="pnlList" runat="server">
                                        <asp:ListView ID="lvComplaint" runat="server">
                                            <EmptyDataTemplate>
                                                <div class="text-center">
                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Service Request For Allotment"></asp:Label>
                                                </div>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading">
                                                        <h5>ALLOTTED SERVICE REQUEST LIST</h5>
                                                    </div>
                                                    <table id="table2" class="table table-hover table-bordered table-striped display" style="width: 100%">
                                                        <%-- <table class="table table-bordered table-hover">--%>
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>SELECT
                                                                                        <%--  <asp:CheckBox ID="chkcomplaint" runat="server" onclick="totalcomplaints(this)" />--%>
                                                                </th>
                                                                <th>SERVICE REQUEST NO.</th>
                                                                <%-- <th>COMP. DATE</th>--%>
                                                                <%-- <th>COMPLAINT</th>--%>
                                                                <%--<th>PRIORITY OF WORK</th>--%>
                                                                <th>SERVICE REQUEST CATEGORY</th>
                                                                <%--<th>Department</th>  --%>
                                                                <th>PRIORITY</th>
                                                                <th>STATUS</th>
                                                                <%--<th>REMARK</th>--%>
                                                                <%-- <th>REOPEN</th>--%>
                                                                <th>Print</th>
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
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("COMPLAINTID")%>'
                                                            AlternateText="Edit Record" ToolTip="Work Out" OnClick="btnEdit_Click" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("COMPLAINTNO")%>
                                                    </td>
                                                    <%-- <td><%# Eval("COMPLAINTDATE","{0:dd-MMM-yyyy}")%></td>--%>
                                                    <%--<td><%# Eval("COMPLAINT")%></td>--%>
                                                    <%--<td><%# Eval("PRIORITY")%></td>--%>
                                                    <td><%# Eval("TYPENAME")%></td>
                                                    <%--   <td><%# Eval("DEPTNAME")%></td>--%>
                                                    <td>
                                                        <%# Eval("PRIORITY") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("COMPLAINTSTATUS")%></td>
                                                    <%--<td><%# Eval("REMARK")%></td>--%>
                                                    <%-- <td>
                                                        <%# Eval("REOPEN") %>
                                                    </td>--%>
                                                    <td>
                                                        <asp:Button ID="btnPrint" runat="server" CausesValidation="false" CommandName="Print"
                                                            Text="Print" CommandArgument='<%# Eval("COMPLAINTID") %>' OnClick="btnPrint_Click" CssClass="btn btn-info" />

                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>

                                        <div class="vista-grid_datapager d-none">
                                            <%--<asp:DataPager ID="dpLett" runat="server" PagedControlID="lvComplaint" PageSize="10"
                                                OnPreRender="dpPager_PreRender">
                                                <Fields>
                                                    <asp:NextPreviousPagerField ButtonType="Link" ShowPreviousPageButton="true" ShowNextPageButton="false" ShowLastPageButton="false" />
                                                    <asp:NumericPagerField ButtonCount="10" ButtonType="Link" />
                                                    <asp:NextPreviousPagerField ButtonType="Link" ShowPreviousPageButton="false" ShowNextPageButton="true" ShowLastPageButton="false" />
                                                </Fields>
                                            </asp:DataPager>--%>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <%-- </ContentTemplate>
                                      </asp:UpdatePanel>--%>
                            </div>
                        </asp:Panel>

                    </div>
                </div>
            </div>
            <%-- </ContentTemplate>--%>
            <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnAddfile" />
        </Triggers>--%>
            <%--</asp:UpdatePanel>--%>
            <script type="text/javascript" language="javascript">
                function totalcomplaints(chkcomplaint) {
                    var frm = document.forms[0];
                    for (i = 0; i < document.forms[0].elements.length; i++) {
                        var e = frm.elements[i];
                        if (e.type == 'checkbox') {
                            if (chkcomplaint.checked == true)
                                e.checked = true;
                            else
                                e.checked = false;
                        }
                    }
                }
            </script>
            <script type="text/javascript">
                function DeleteItem() {
                    if (confirm("Are you sure you want to delete ...?")) {
                        return true;
                    }
                    return false;
                }
            </script>
        <%--</ContentTemplate>--%>
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnAddfile" />
        </Triggers>--%>
    <%--</asp:UpdatePanel>--%>
</asp:Content>
