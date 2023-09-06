<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dropcourses.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_EXAMINATION_Dropcourses" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="z-index: 1; position: absolute; top: 40%; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updDetails"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <script>
    function exefunction(chks1Thoery, lbl) {
            debugger;
            //str = 20;
            list = 'lvFailCourse';
            var dataRows = document.getElementsByTagName('tr');
            if (dataRows != null) {
                for (i = 0; i < dataRows.length - 1; i++) {
                    var credist = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_' + lbl;
                    if (chks1Thoery.checked) {
                       
                        document.getElementById('<%= hdfcurrcredits.ClientID %>').value = Number(document.getElementById('<%= hdfcurrcredits.ClientID %>').value) - Number(document.getElementById(credist).value);
                        document.getElementById('ctl00_ContentPlaceHolder1_lblcred').innerText = document.getElementById('<%= hdfcurrcredits.ClientID %>').value;
                        
                        if (document.getElementById('<%= hdfcurrcredits.ClientID %>').value < 20) {
                            alert("The minimum total registered credits should not be less than 20 credits !");
                            document.getElementById('<%= hdfcurrcredits.ClientID %>').value = Number(document.getElementById('<%= hdfcurrcredits.ClientID %>').value) + Number(document.getElementById(credist).value);
                            document.getElementById('ctl00_ContentPlaceHolder1_lblcred').innerText = document.getElementById('<%= hdfcurrcredits.ClientID %>').value;
                            //document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) - Number(document.getElementById("ctl00_ContentPlaceHolder1_lblstuendth").innerHTML);
                            chks1Thoery.checked = false;
                        }
                        break;
                    }
                    else {
                      
                        document.getElementById('<%= hdfcurrcredits.ClientID %>').value = Number(document.getElementById('<%= hdfcurrcredits.ClientID %>').value) + Number(document.getElementById(credist).value);
                        document.getElementById('ctl00_ContentPlaceHolder1_lblcred').innerText = document.getElementById('<%= hdfcurrcredits.ClientID %>').value;
                        break;
                    }
                }
            }
        }
        function exefunction2(chks6practical) {
            debugger;
            //str = 20;
            var dataRows = document.getElementsByTagName('tr');
            if (dataRows != null) {
                for (i = 0; i < dataRows.length - 1; i++) {

                    if (chks6practical.checked) {
                        // document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) + Number(str);
                        document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) + Number(document.getElementById("ctl00_ContentPlaceHolder1_lblstuendpr").innerHTML);

                        break;
                    }

                    else {

                        // document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) - Number(str);
                        document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = Number(document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value) - Number(document.getElementById("ctl00_ContentPlaceHolder1_lblstuendpr").innerHTML);
                        break;
                    }
                }
            }
        }
    </script>

    <asp:UpdatePanel ID="updDetails" runat="server">
        <ContentTemplate>

            <asp:Panel ID="pnlStart" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">DROP COURSES </h3>
                            </div>

                            <div class="box-body">
                                <asp:Panel ID="pnlSearch" runat="server">
                                    <div class="row">
                                        <div class="col-md-12">
                                          
                                                <div class="col-md-12">
                                                    <div class="form-group col-md-2"></div>
                                                    <div class="form-group col-md-4" id="div_enrollno" runat="server">
                                                        <span style="color: red">*</span><label>Session :</label>
                                                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" AppendDataBoundItems="true" Font-Bold="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>&nbsp;&nbsp;
                             <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                 Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="search"></asp:RequiredFieldValidator>

                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>USN No :</label>
                                                        <asp:TextBox ID="txtEnrollno" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-2"></div>
                                                </div>
                                                <div class="row text-center" id="div_btn" runat="server">
                                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary"  ValidationGroup="search" OnClick="btnSearch_Click"
                                                        Text="Show" />
                                                    &nbsp;<asp:Button ID="btnCancel" runat="server"
                                                         Text="Clear" CssClass="btn btn-danger" CausesValidation="false" />
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                        ValidationGroup="search" ShowSummary="false" />
                                                </div>
                                        
                                        </div>
                                    </div>
                                  <hr />
                                </asp:Panel>
                                <br />
               
                                <div class="row" id="divCourses" runat="server" visible="false">
                                    <div class="col-md-12" id="tblInfo" runat="server">
                                        <div class="row">
                                            <div class="col-md-10">

                                                <%-- <h4>Backlog Exam Registration</h4>
                <hr />--%>
                                                <div class="form-group col-md-4">
                                                    <b>Student Name</b> :
                     <asp:Label ID="lblName" runat="server" Font-Bold="True" ForeColor="Green"/>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" ForeColor="Green"/><br />
                                                    <asp:Label ID="lblMotherName" runat="server" Font-Bold="False" ForeColor="Green"/>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <b>College</b> :
                     <asp:Label ID="lblCollege" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label>
                                                    <asp:Label ID="lblprintfor" runat="server" Visible="false" Text="aayushi" />
                                                    <asp:HiddenField ID="hdnCollege" Value="" runat="server" />
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <b>USN No.</b> :
                      <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <b>Session</b> :
                                                    <asp:Label ID="lblsession" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <b>Admission Batch</b> :<asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <b>Current Semester</b> :
                                                    <asp:Label ID="lblSemester" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <b>Degree / Branch</b> :
                                                    <asp:Label ID="lblBranch" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <b>Scheme</b> :<asp:Label ID="lblScheme" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label>
                                                </div>
                                                
                                               

                                            </div>
                                            <%-- <div class="col-md-1"></div>--%>
                                            <div class="col-md-2" >
                                                <asp:Image ID="imgPhoto" AlternateText="image" runat="server" style="width:130px;height:130px"  />
                                                <asp:HiddenField ID="hdfcurrcredits" runat="server" />
                                            </div>
                                        </div>
                                       <div class="row" runat="server" id="credits">
                                            <div class="col-md-5">
                                                <div class="container-fluid">
                                                     <span style="font-weight: bold; text-align: center; "><u>Total Registered Credits:</u>:</span>&nbsp;&nbsp;<span style="border:1px solid black;padding:5px"><asp:Label ID="lblcred" runat="server" Style="font-weight: bold; text-align: center; color: black;font-size:17px"></asp:Label></span>
                                                </div>
                                            </div>
                                           <div class="col-md-7" id="divnote" runat="server">
                                               <fieldset class="fieldset" style="color: Red">
                                                        <legend class="legend" style="color:red">Note :</legend>
                                                    <span style="font-weight: bold; text-align: center; color: green;"><u>Please select the courses from course list and click on submit button to drop courses.</u></span><br />
                                                        <span style="font-weight: bold; text-align: center; color: green;"><u>The minimum total registered credits should not be less than <span style="color:red">20 credits</span></u>:</span>
                                                    </fieldset>
                                               </div>
                                        </div>
                                        <div class="row" id="trFailList" runat="server">
                                            <div class="col-md-12">
                                                <div class="container-fluid">
                                                    <asp:ListView ID="lvFailCourse" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="vista-grid">
                                                                <div class="titlebar">
                                                                    <h4>Course List:</h4>
                                                                </div>
                                                                <table id="tblFailCourse" class="table table-hover table-bordered table-responsive">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <%--  <th>--%>
                                                                            <%--<asp:CheckBox ID="cbHead" runat="server" onclick="SelectAll(this)" ToolTip="Select/Select all" />--%>
                                                                            <%-- Select--%>
                                                                            <%--  </th>--%>
                                                                            <th>
                                                                                <asp:CheckBox ID="cbHead" runat="server" Visible="false" ToolTip="Select/Select all" onclick="SelectAll(this,'chkAccept','lblcredits')" />
                                                                            </th>
                                                                            <th>Course Code
                                                                            </th>
                                                                            <th>Course Name
                                                                            </th>
                                                                            <th>Semester
                                                                            </th>

                                                                            <%--<th>ENDSEM-TH</th>
                                                              
                                                               
                                                                <th>ENDSEM-PR</th>--%>

                                                                            <th>Subject Type
                                                                            </th>
                                                                            <th>Credits
                                                                            </th>
                                                                            <th>
                                                                                Approved Status
                                                                            </th>


                                                                        </tr>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </thead>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr id="trCurRow" class="item">
                                                                <%--  <td>--%>
                                                                <%--<asp:CheckBox ID="chkAccept" runat="server" Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' Enabled='<%# Eval("ACCEPTED").ToString() == "0" ? true : false %>' />--%>
                                                                <%--  <asp:CheckBox ID="chkAccept" runat="server" Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' />--%>
                                                                <%--Enabled='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? false : true %>'--%>

                                                                <%-- </td>--%>
                                                                <td>
                                                                    <asp:CheckBox ID="chkAccept" runat="server" onclick="exefunction(this,'hdncurcredits')" />
                                                                    <asp:HiddenField ID="hdncurcredits" runat="server" Value='<%# (Eval("CREDITS").ToString())==""?"0":Eval("CREDITS") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SEMESTER") %>
                                                                </td>
                                                                <%--<td>
                                                           <asp:CheckBox ID="ckhendsemtheory" runat="server" onclick="exefunction(this)"    />
                                                    </td>
                                                   
                                                    <td>
                                                        <asp:CheckBox ID="chks8practical" runat="server" onclick="exefunction2(this)"  />
                                                    </td>--%>

                                                                <%-- <td>--%>
                                                                <%-- <asp:CheckBox ID="chkThoery" runat="server" Checked='<%# Eval("S1IND").ToString() == "F" ? true : false %>' Enabled='<%# Eval("S1IND").ToString() == "P" ? false : true %>' />--%>
                                                                <%--Enabled='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? false : true %>'--%>

                                                                <%-- </td>--%>
                                                                <%--  <td>--%>

                                                                <%-- <asp:CheckBox ID="chkPract" runat="server" Checked='<%# Eval("S8IND").ToString() == "F" ? true : false %>' Enabled='<%# Eval("S8IND").ToString() == "P" ? false : true %>' />--%>

                                                                <%-- </td>--%>
                                                                <td style="font-weight: bold" align="center">
                                                                    <%# (Eval("SUBID").ToString()) == "1" ? "Theory" : ((Eval("SUBID").ToString()) == "3" ? "Theory cum Practical" : "Practical")%>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblcredits" runat="server" Text='<%# (Eval("CREDITS").ToString())==""?"0":Eval("CREDITS") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblstat" runat="server" Text='<%# (Eval("STATUS").ToString())=="1"?"APPROVED":(Eval("STATUS").ToString())=="0"?"PENDING":"NOT APPLIED" %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>

                                                        <%--<EmptyDataTemplate>
                                            <span style="background-color: #00E171; font-size:large; font-style:normal; border:1px solid #000000;">
                                                  No Backlog Courses.
                                            </span>
                                            </EmptyDataTemplate>--%>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row" id="trdroplist" runat="server">
                                             <div class="col-md-12">
                                                <div class="container-fluid">
                                                    <asp:ListView ID="lvdropcourse" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="vista-grid">
                                                                <div class="titlebar">
                                                                    <h4>Dropped Course List:</h4>
                                                                </div>
                                                                <table id="tbldropcourseCourse" class="table table-hover table-bordered table-responsive">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                        
                                                                            <th>
                                                                                <asp:CheckBox ID="cbHead" runat="server" Visible="false" ToolTip="Select/Select all" onclick="SelectAll(this,'chkAccept','lblcredits')" />
                                                                            </th>
                                                                            <th>Course Code
                                                                            </th>
                                                                            <th>Course Name
                                                                            </th>
                                                                            <th>Semester
                                                                            </th>
                                                                            <th>Subject Type
                                                                            </th>
                                                                            <th>Credits
                                                                            </th>
                                                                            <th>
                                                                                Approved Status
                                                                            </th>


                                                                        </tr>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </thead>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr id="trCurRow" class="item">
                                                               
                                                                <td>
                                                                    <asp:CheckBox ID="chkAccept" runat="server" onclick="exefunction(this,'hdncurcredits')" />
                                                                    <asp:HiddenField ID="hdncurcredits" runat="server" Value='<%# (Eval("CREDITS").ToString())==""?"0":Eval("CREDITS") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SEMESTER") %>
                                                                </td>
                                                               
                                                                <td style="font-weight: bold" align="center">
                                                                    <%# (Eval("SUBID").ToString()) == "1" ? "Theory" : ((Eval("SUBID").ToString()) == "3" ? "Theory cum Practical" : "Practical")%>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblcredits" runat="server" Text='<%# (Eval("CREDITS").ToString())==""?"0":Eval("CREDITS") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblstat" runat="server" Text='<%# (Eval("STATUS").ToString())=="1"?"APPROVED":(Eval("STATUS").ToString())=="0"?"PENDING":"NOT APPLIED" %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>

                                                       
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" id="divremark" runat="server" visible="false">
                                            <div class="col-md-12">
                                                 <label><span style="color: red;">*</span> Remark :</label>
                                        <asp:TextBox ID="txtRemark" runat="server" Rows="2" TabIndex="9" TextMode="MultiLine" ToolTip="Please Enter Remark" MaxLength="100" Height="100" Width="500px"/>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRemark" Display="None" ErrorMessage="Please Enter Remark"
                                            SetFocusOnError="True" ValidationGroup="Search"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>



                                        <div class="row">
                                            <div class="col-md-12" id="divreason" runat="server">
                                                <span style="color:red">*</span><label>Reason:</label>
                                                 <asp:TextBox ID="txtreason" runat="server" TextMode="MultiLine" CssClass="form-control" MaxLength="100" Height="100" Width="500px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtreason" runat="server" ControlToValidate="txtreason"
                                Display="None"  ErrorMessage="Please Enter Reason" ValidationGroup="Search"></asp:RequiredFieldValidator>
                                                 <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False"
                                        ValidationGroup="Search" />
                                            </div>
                                        </div>
                                       


                                        &nbsp;
            <div class="row text-center">
                <asp:Button ID="btnSubmit" runat="server"  Text="Submit" Font-Bold="true" Visible="false" OnClick="btnSubmit_Click" ValidationGroup="Search"
                    CssClass="btn btn-primary" />
                <asp:Button ID="btnapprove" runat="server"  Text="Approve" Font-Bold="true" Visible="false" OnClick="btnapprove_Click" ValidationGroup="Search"
                    CssClass="btn btn-primary" />
                &nbsp;<asp:Button ID="btnReport" runat="server"
                     Text="Print Reciept" ValidationGroup="backsem" CssClass="btn btn-warning" Font-Bold="true" Visible="false" />
                &nbsp;<asp:Button ID="btnRemoveList" runat="server"  Text="Clear List" Font-Bold="true" Visible="false" OnClick="btnRemoveList_Click"
                    CssClass="btn btn-danger" />


            </div>
                                        <br />
                                       

                                    </div>

                                </div>
                            </div>

                        </div>

                    </div>
                    <div id="divMsg" runat="server">
                    </div>

            </asp:Panel>

        </ContentTemplate>
       
    </asp:UpdatePanel>



    </asp:Content>