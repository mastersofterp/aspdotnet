<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Fitnessdoc_Submission.aspx.cs" Inherits="ACADEMIC_Fitnessdoc_Submission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updCollege"
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

    <asp:UpdatePanel ID="updCollege" runat="server">
        <ContentTemplate>
            <div id="divMsg" runat="server">
            </div>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Fitness Certificate Submission</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    
                                     <div class="col-lg-6 col-md-12 col-12" id="divadmin" runat="server" visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-5 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Search By</label>
                                        </div>
                                        <asp:RadioButton ID="rdoEnrollmentNo" runat="server" Text="Enrollment No." GroupName="search" Checked="true" Visible="true" TabIndex="1"/>&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoRollNo" runat="server" Text="Roll No." GroupName="search" Visible="true" TabIndex="2"/>&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoIdno" runat="server" Text="Id No." Checked="true" GroupName="search" Visible="false" />
                                    </div>

                                    <div class="form-group col-lg-5 col-md-6 col-12 mt-2">
                                        <asp:TextBox ID="txtTempIdno" runat="server" ValidationGroup="search" class="form-control" TabIndex="3"></asp:TextBox>
                                    </div>

                                    <div class="col-lg-2 col-md-12 col-12 btn-footer">
                                        <asp:Button ID="btnShow" runat="server" Text="Search" TabIndex="4" ValidationGroup="search" OnClick="btnShow_Click" CssClass="btn btn-primary" />
                                        <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtTempIdno" Display="None"
                                            ErrorMessage="Please Enter Enrollment No. or RollNo" SetFocusOnError="True" ValidationGroup="search" />
                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                            ValidationGroup="search" />
                                    </div>
                                </div>
                            </div>
                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note</h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Upload the Documents only with following formats - <span style="color: green;font-weight:bold"> .jpg, .jpeg, .doc, .pdf</span></span>  </p>
                                        </div>
                                    </div>
                                </div>
                            </div>

                           

                            <div id="divDetails" class="col-12 mb-3" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Student Details</h5>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Roll Number :</b>
                                                <a class="sub-label"><asp:Label ID="lblroll" runat="server" Font-Bold="True"></asp:Label> </a>
                                            </li>
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label"><asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Registration No. :</b>
                                                <a class="sub-label"><asp:Label ID="lblRegno" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>      
                                        </ul>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Degree :</b>
                                                <a class="sub-label"><asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label> </a>
                                            </li>
                                            <li class="list-group-item"><b>Semester :</b>
                                                <a class="sub-label"><asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Branch :</b>
                                                <a class="sub-label"><asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>      
                                        </ul>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Label ID="lblMsg" runat="server" SkinID="lblmsg"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12" id="divuplod" runat="server" visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><asp:Label ID="lblfit1" runat="server" Text="Fitness Certificate : "></asp:Label></label>
                                        </div>
                                        <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Select file to Import" TabIndex="5" />
                                    </div>

                                    <div class="form-group col-lg-1 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnsubmit" runat="server" Text="Upload" ToolTip="Click to Upload" TabIndex="6" CssClass="btn btn-primary" OnClick="btnsubmit_Click" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:Label ID="lblstat" runat="server" Text="Document Status : Not Uploaded" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlSession" runat="server">
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="table2">
                                        <asp:Repeater ID="lvStud" runat="server">
                                            <HeaderTemplate>
                                                <div class="sub-heading">
                                                    <h5>Student Details</h5>
                                                </div>
                                                <div id="table3" class="dataTables_length" runat="server">
                                                    <label>
                                                    </label>
                                                </div>
                                                <thead class="bg-light-blue">
                                                    <tr id="itemPlaceholder" runat="server">

                                                        <th style="white-space: nowrap">ROLL NO.
                                                        </th>
                                                        <%--<th style="white-space: nowrap">STUDENT NAME
                                                        </th>--%>
                                                        <th>DOCUMENT STATUS
                                                        </th>
                                                        <th>DOCUMENT NAME
                                                        </th>
                                                        <%--<th>DOCUMENT TYPE
                                                        </th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("ROLLNO") %>
                                                    </td>
                                                    <%-- <td>
                                                        <%# Eval("STUDNAME") %>
                                                    </td>--%>

                                                    <%--<td>
                                                        <%# Eval("DOCUMENTNO")%>
                                                    </td>--%>
                                                    <td>
                                                        <asp:Label ID="lblfit" Font-Bold="true" runat="server" Text="Uploaded" ForeColor="Green" Visible="True"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DOCUMENT_NAME")%>
                                                    </td>
                                                    <%--<td>
                                                        <%# Eval("DOCTYPE")%>
                                                    </td>--%>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Eval("ROLLNO") %>
                                                    </td>
                                                    <%--<td>
                                                        <%# Eval("STUDNAME") %>
                                                    </td>--%>

                                                    <%--<td>
                                                        <%# Eval("DOCUMENTNO")%>
                                                    </td>--%>
                                                    <td>
                                                        <asp:Label ID="lblfit" Font-Bold="true" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DOCUMENT_NAME")%>
                                                    </td>
                                                    <%--<td>
                                                        <%# Eval("DOCTYPE")%>
                                                    </td>--%>
                                                </tr>
                                            </AlternatingItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnsubmit" />
        </Triggers>

    </asp:UpdatePanel>






</asp:Content>

