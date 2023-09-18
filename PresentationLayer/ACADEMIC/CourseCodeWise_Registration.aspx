<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CourseCodeWise_Registration.aspx.cs" ViewStateEncryptionMode="Always" EnableViewStateMac="true" 
  MasterPageFile="~/SiteMasterPage.master"
    Inherits="ACADEMIC_CourseCodeWise_Registration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size:50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
     </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
             <div class="row">
     <div class="col-md-12">
        <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><b>COURSE REGISTRATION REPORTS</b></h3>
                    <div class="pull-right">
                              <div style="color: Red; font-weight: bold">
                             &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory</div>
                    </div>
                </div>
          
                <div class="box-body">
                            <div class="col-md-12">
                                <div class="form-group col-md-4">
                                    <label><span style="color:red;">*</span> Session</label>
                                     <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4" style="display:none">
                                    <label><span style="color:red;">*</span> Institute Name</label>
                                     <asp:DropDownList ID="ddlCollegeName" runat="server" AppendDataBoundItems="True"
                                            ValidationGroup="Show" ToolTip="Please Select Institute" AutoPostBack="True" OnSelectedIndexChanged="ddlCollegeName_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                      
                                </div>
                                <div class="form-group col-md-4" style="display:none">
                                    <label><span style="color:red;">*</span> Degree</label>
                                     <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                     
                                </div>
                                <div class="form-group col-md-4" style="display:none">
                                    <label><span id="spbr" runat="server" style="color:red;">*</span> Branch</label>
                                     <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                      
                                </div>
                                <div class="form-group col-md-4" style="display:none">
                                    <label><span id="spsc" runat="server" style="color:red;">*</span> Scheme</label>
                                     <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                 
                                </div>
                                <div class="form-group col-md-4">
                                    <label><span id="spse" runat="server" style="color:red;">*</span> Semester</label>
                                    <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            ValidationGroup="Show" CssClass="form-control" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                </div>

                                 <div class="form-group col-md-4" style="display:none">
                                    <label>Section</label>
                                    <asp:DropDownList ID="ddlSec" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            ValidationGroup="Show" CssClass="form-control" >
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>                                    
                                </div>

                                <div class="form-group col-md-4" style="display:none">
                                     <label><span id="spsub" runat="server" style="color:red;">*</span> Subject Type</label>
                                      <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="true"
                                            ValidationGroup="Show" AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                     
                                 </div>

                                 <div class="form-group col-md-4">
                                    <label><span id="spcourse" runat="server" style="color:red;">*</span> Course</label>
                                     <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                             OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                            Display="None" InitialValue="0" SetFocusOnError="true" ErrorMessage="Please Select Course" ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4" id="trsection" runat="server" visible="false">
                                    <label>Section</label>
                                     <asp:DropDownList ID="ddlSection" runat="server" ToolTip="Section" AppendDataBoundItems="True" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    
                                </div>
                                 <div class="form-group col-md-8"  style="display:none" >
                                    <legend>Report</legend>
                                    <div class="radio">
                                         <asp:RadioButtonList ID="rdbReport" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbReport_SelectedIndexChanged"
                                            AutoPostBack="true" Width="100%" RepeatColumns="2">
                                            <%--<asp:ListItem Selected="True" Value="1">Course Registered Student</asp:ListItem>--%>
                                         <%--   <asp:ListItem Value="2">Coursewise Student Count</asp:ListItem>--%>
                                            <%--<asp:ListItem  Value="5">Coursewise Student Count(University)</asp:ListItem>--%>
                                         <%--   <asp:ListItem Value="3" >Roll List</asp:ListItem>--%>
                                         <%--   <asp:ListItem Value="4">Course Registration List</asp:ListItem>--%>
                                        <%--    <asp:ListItem Value="6">Course Registered Intake/Balance</asp:ListItem>--%>
                                         <%--   <asp:ListItem Value="7" Selected="True" >Course Registered List</asp:ListItem>--%>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3" style="display:none">
                               <fieldset class="fieldset" style="padding: 5px; color: Green">
                                            <legend class="legend" style="text-align:center;">Note</legend>Please Select<br />
                                            <span style="font-weight: bold; color: Red;">Course Registered Student : </span>
                                            <br />
                                            Session->Institute->Degree->Branch->
                                            Scheme->Semester->Subject type<br />
                                            <span style="font-weight: bold; color: Red;">Coursewise Student Count : </span>
                                            <br />
                                           <%-- Session->College->Degree<br />
                                            <span style="font-weight: bold; color: Red;">Coursewise Student Count (University) : </span>--%>
                                            <%--<br />--%>
                                            Session->Institute->Degree<br />
                                            <span style="font-weight: bold; color: Red;">Student Roll List : </span>
                                            <br />
                                            Session->Institute->Degree->Branch->
                                            Scheme->Semester<br />
                                            <span style="font-weight: bold; color: Red;">Course Registration List : </span>
                                            <br />
                                            Session->Institute->Degree->Branch->
                                            Scheme->Semester<br />
                                           <span style="font-weight: bold; color: Red;">Course Registered Intake/Balance: </span>
                                            <br />
                                            Session->College->Degree<br />
                                            <span style="font-weight: bold; color: Red;">Elective Course Registered List : </span>
                                            <br />
                                            Session->Institute->Degree->Branch->
                                            Scheme->Semester->Subject type->Course<br />
                                        </fieldset>
                            </div>
                </div>
                 
                <div class="box-footer">
                        <p class="text-center">
                             <asp:Button ID="btnReport" ToolTip="Report" runat="Server" Text="Report" ValidationGroup="Show" OnClick="btnReport_Click"
                                    CssClass="btn btn-info" />                              
                                    <asp:Button ID="btnExcel" runat="Server" ToolTip="Excel" Text="Excel Report" ValidationGroup="Show"
                                       CssClass="btn btn-info" OnClick="btnExcel_Click" />                               
                                <asp:Button ID="btnCancel" runat="server" ToolTip="Cancel" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" />
                        </p>
                        <div class="col-md-12"><asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" /></div>
                        <div class="col-md-12" onscroll="auto">
                            <asp:ListView ID="lvStudents" runat="server">
                    <LayoutTemplate>
                        <div>                           
                                Students List For Registered Courses                           
                            <table class="table table-hover table-bordered table-responsive">
                                <thead>
                                    <tr class="bg-light-blue">
                                        <th>Regno/EnrollNo
                                        </th>
                                        <th>Roll No
                                        </th>
                                        <th>Section Name
                                        </th>
                                        <th>Student Name
                                        </th>
                                    </tr>
                                    </thead><tbody>
                                    <tr id="itemPlaceholder" runat="server" /></tbody>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr id="Tr1" runat="server">
                            <td>
                                <%# Eval("UNIQUENO")%>
                            </td>
                            <td>
                                <%# Eval("ROLL_NO")%>
                            </td>
                            <td>
                                <%# Eval("SECTIONNAME")%>
                            </td>
                            <td>
                                <%# Eval("STUDNAME")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
                        </div>
                    </div>     
        </div>
    </div>
    </div>
     </ContentTemplate>
          <Triggers>
        
           <asp:PostBackTrigger ControlID="btnExcel" />
      
       </Triggers>
     </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

    <script language="javascript" type="text/javascript">
        function onreport() {

            var a = document.getElementById("ctl00_ContentPlaceHolder1_rdbReport_4");
            if (a.checked) {
                document.getElementById("ctl00_ContentPlaceHolder1_rfvDegree").enabled = false;
                document.getElementById("ctl00_ContentPlaceHolder1_rfvBranch").enabled = false;
                document.getElementById("ctl00_ContentPlaceHolder1_rfvProgram").enabled = false;
                document.getElementById("ctl00_ContentPlaceHolder1_rfvSemester").enabled = false;
            }
        }
    </script>


</asp:Content>
