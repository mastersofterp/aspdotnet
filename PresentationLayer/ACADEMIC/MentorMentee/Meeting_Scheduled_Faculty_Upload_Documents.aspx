<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Meeting_Scheduled_Faculty_Upload_Documents.aspx.cs" Inherits="ACADEMIC_MentorMentee_Meeting_Scheduled_Faculty_Upload_Documents" ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="modal fade" id="preview" role="dialog">
                 <div class="modal-dialog modal-lg">
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                         <ContentTemplate>
                             <div class="modal-content">

                                 <!-- Modal Header -->
                                 <div class="modal-header">
                                     <h4 class="modal-title">Document</h4>
                                     <button type="button" class="close" data-dismiss="modal">&times;</button>
                                 </div>

                                 <!-- Modal body -->
                                 <div class="modal-body text-center">
                                     <asp:Literal ID="ltEmbed" runat="server" />
                                 </div>

                                 <!-- Modal footer -->
                                 <div class="modal-footer">
                                     <button type="button" class="btn btn-danger" data-dismiss="modal" id="btnclose">Close</button>
                                 </div>

                             </div>
                         </ContentTemplate>
                     </asp:UpdatePanel>
                 </div>
             </div>


    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
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
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Meeting Scheduled Faculty Upload Documnets</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlDesig" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Committee</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCommitee" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCommitee_SelectedIndexChanged" AutoPostBack="true"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Select Committee" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCommitee" runat="server" ErrorMessage="Please Select Committee"
                                                ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlCommitee" Display="None"></asp:RequiredFieldValidator>

                                        </div>

                                        <div id="Div2" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Meeting Scheduled Date Time</label>
                                            </div>
                                            <asp:DropDownList ID="ddlMeeting" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Select Meeting Scheduled Date Time" TabIndex="2">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvMeetingTime" runat="server" ErrorMessage="Please Select Scheduled Date Time"
                                                ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlMeeting" Display="None"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Meeting Description</label>
                                            </div>
                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" ToolTip="Enter Meeting Description" TabIndex="3" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtitle" runat="server" ControlToValidate="txtDescription"
                                             Display="None" ErrorMessage="Please Enter Meeting Description" ValidationGroup="Submit" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Upload File </label>
                                            </div>
                                            <asp:FileUpload ID="fuDescription" runat="server" ValidationGroup="Submit" ToolTip="Select a File to Upload!" TabIndex="4" accept=".pdf" />
                                            <asp:Label ID="lblfile" runat="server" />

                                        </div>

                                         <div class="col-12 btn-footer mt-5">
                                            <asp:LinkButton ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" TabIndex="13" CausesValidation="true"></asp:LinkButton>
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" OnClick="btnCancel_Click" TabIndex="14" CausesValidation="false" />
                                            <asp:ValidationSummary ID="vsFacultyAssign" runat="server" DisplayMode="List"
                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                        </div>
                                        </div>
                                    </div>


                                        <div class="col-12 mb-4">
                                            <asp:Panel ID="pnlDescription" runat="server" ScrollBars="Auto">
                                                <asp:ListView ID="lvDescription" runat="server" Visible="false">
                                                    <LayoutTemplate>
                                                        <div id="lgv1">
                                                            <%--<div class="sub-heading">
                                                    <h5>MEETING TITLE LIST </h5>
                                                </div>--%>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                <thead class="bg-light-blue">
                                                                    <tr>

                                                                        <th>Edit </th>
                                                                        <th>Committee </th>
                                                                        <th>Meeting Scheduled Date Time </th>
                                                                        <th>Meeting Description </th>
                                                                        <th>Document Preview</th>

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
                                                                <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("DEC_ID") %>' ImageUrl="~/images/edit.png" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCommittee" runat="server" Text='<%#Eval("NAME") %>' TabIndex="1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblMeetingDateTime" runat="server" Text='<%#Eval("MEETINGDATETIME") %>' TabIndex="1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("DESCRIPT_ION") %>' TabIndex="1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click"
                                                                    Text="Preview" ImageUrl="~/Images/search-svg.png" ToolTip='<%# Eval("FILE_NAME") %>' data-toggle="modal" data-target="#preview"
                                                                    CommandArgument='<%# Eval("FILE_NAME") %>' Visible='<%# Convert.ToString(Eval("FILE_NAME"))==string.Empty?false:true %>'></asp:ImageButton>
                                                            </td>
                                                          
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                       
                            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit"/>
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

