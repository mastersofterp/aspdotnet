<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TP_Admin_Lock_Unlock.aspx.cs" Inherits="TRAININGANDPLACEMENT_TP_Admin_Lock_Unlock" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">STUDENT PROFILE LOCK UNLOCK</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">

                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divColg">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>TP Student</label>
                                </div>
                                <asp:DropDownList ID="ddlStudent" runat="server" AppendDataBoundItems="true"
                                    CssClass="form-control" data-select2-enable="true" >
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlStudent"
                                    SetFocusOnError="true" InitialValue="0" Display="None" ErrorMessage="Plese Select College"
                                    ValidationGroup="Report"></asp:RequiredFieldValidator>
                            </div>

                           
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="Show" runat="server" Text="Show" OnClick="Show_Click" class="btn btn-primary" CausesValidation="false" />

                        <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click" class="btn btn-primary" CausesValidation="false" />

                        <asp:Button ID="Cancel" runat="server" Text="Cancel" OnClick="Cancel_Click" class="btn btn-warning" CausesValidation="false" />

                    </div>


                           <div class="col-12 mt-3">

                                                    <asp:ListView ID="lvlockunlock" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Lock Unlock List</h5>
                                                            </div>

                                                            <table class="table table-striped table-bordered nowrap " style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Student Name</th>
                                                                        <th>Regidtration Id</th>
                                                                        <th>Exam Details</th>
                                                                        <th>Work Experience</th>
                                                                        <th>Technical Skills</th>
                                                                        <th>Projects</th>
                                                                        <th>Certifications</th>
                                                                        <th>Languages</th>
                                                                        <th>Awards & Recognitions</th>
                                                                        <th>Competitions</th>
                                                                        <th>Training & Workshop</th>
                                                                        <th>Test Scores</th>
                                                                        <th>Build Resume</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>

                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("STUDNAME")%></td>
                                                                <td><%# Eval("REGNO")%>
                                                                      <asp:HiddenField runat="server" ID="hdidno" Value='<%# Eval(string.Format("IDNO")) %>' />
                                                                </td>
                                                                <td><asp:CheckBox runat="server" ID="Exmchk" ToolTip='<%# Eval(string.Format("EXAM_LOCK_UNLOCK")) %>' Checked='<%# GetStatus(Eval("EXAM_LOCK_UNLOCK")) %>'/></td>
                                                                <td><asp:CheckBox runat="server" ID="WorkExpchk" ToolTip='<%# Eval("WORK_EXP_LOCK_UNLOCK") %>' Checked='<%# GetStatus(Eval("WORK_EXP_LOCK_UNLOCK")) %>'/></td>
                                                                <td><asp:CheckBox runat="server" ID="TechSkilchk" ToolTip='<%# Eval("TECH_SKIL_LOCK_UNLOCK") %>' Checked='<%# GetStatus(Eval("TECH_SKIL_LOCK_UNLOCK")) %>'/></td>
                                                                <td><asp:CheckBox runat="server" ID="Projectchk" ToolTip='<%# Eval("PROJECT_LOCK_UNLOCK") %>' Checked='<%# GetStatus(Eval("PROJECT_LOCK_UNLOCK")) %>'/></td>
                                                                <td><asp:CheckBox runat="server" ID="certificationschk" ToolTip='<%# Eval("CERTIFICATION_LOCK_UNLOCK") %>' Checked='<%# GetStatus(Eval("CERTIFICATION_LOCK_UNLOCK")) %>'/></td>
                                                                <td><asp:CheckBox runat="server" ID="langchk" ToolTip='<%# Eval("LANGUAGE_LOCK_UNLOCK") %>' Checked='<%# GetStatus(Eval("LANGUAGE_LOCK_UNLOCK")) %>'/></td>
                                                                <td><asp:CheckBox runat="server" ID="awrdchk" ToolTip='<%# Eval("AWARD_LOCK_UNLOCK") %>' Checked='<%# GetStatus(Eval("AWARD_LOCK_UNLOCK")) %>'/></td>
                                                                <td><asp:CheckBox runat="server" ID="comptchk" ToolTip='<%# Eval("COMPETITION_LOCK_UNLOCK") %>' Checked='<%# GetStatus(Eval("COMPETITION_LOCK_UNLOCK")) %>'/></td>
                                                                <td><asp:CheckBox runat="server" ID="trngchk" ToolTip='<%# Eval("TRAINING_LOCK_UNLOCK") %>' Checked='<%# GetStatus(Eval("TRAINING_LOCK_UNLOCK")) %>'/></td>
                                                                <td><asp:CheckBox runat="server" ID="tessctchk" ToolTip='<%# Eval("TEST_SCORE_LOCK_UNLOCK") %>' Checked='<%# GetStatus(Eval("TEST_SCORE_LOCK_UNLOCK")) %>'/></td>
                                                                <td><asp:CheckBox runat="server" ID="BildReschk" ToolTip='<%# Eval("BUILD_RESUME_LOCK_UNLOCK") %>' Checked='<%# GetStatus(Eval("BUILD_RESUME_LOCK_UNLOCK")) %>'/></td>

                                                            </tr>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <tr>
                                                               <td><%# Eval("STUDNAME")%></td>
                                                                <td><%# Eval("REGNO")%>
                                                                    <asp:HiddenField runat="server" ID="hdidno" Value='<%# Eval(string.Format("IDNO")) %>' />
                                                                </td>
                                                                <td><asp:CheckBox runat="server" ID="Exmchk" ToolTip='<%# Eval(string.Format("EXAM_LOCK_UNLOCK")) %>' Checked='<%# GetStatus(Eval("EXAM_LOCK_UNLOCK")) %>'/></td>
                                                                <td><asp:CheckBox runat="server" ID="WorkExpchk" ToolTip='<%# Eval("WORK_EXP_LOCK_UNLOCK") %>' Checked='<%# GetStatus(Eval("WORK_EXP_LOCK_UNLOCK")) %>'/></td>
                                                                <td><asp:CheckBox runat="server" ID="TechSkilchk" ToolTip='<%# Eval("TECH_SKIL_LOCK_UNLOCK") %>' Checked='<%# GetStatus(Eval("TECH_SKIL_LOCK_UNLOCK")) %>'/></td>
                                                                <td><asp:CheckBox runat="server" ID="Projectchk" ToolTip='<%# Eval("PROJECT_LOCK_UNLOCK") %>' Checked='<%# GetStatus(Eval("PROJECT_LOCK_UNLOCK")) %>'/></td>
                                                                <td><asp:CheckBox runat="server" ID="certificationschk" ToolTip='<%# Eval("CERTIFICATION_LOCK_UNLOCK") %>' Checked='<%# GetStatus(Eval("CERTIFICATION_LOCK_UNLOCK")) %>'/></td>
                                                                <td><asp:CheckBox runat="server" ID="langchk" ToolTip='<%# Eval("LANGUAGE_LOCK_UNLOCK") %>' Checked='<%# GetStatus(Eval("LANGUAGE_LOCK_UNLOCK")) %>'/></td>
                                                                <td><asp:CheckBox runat="server" ID="awrdchk" ToolTip='<%# Eval("AWARD_LOCK_UNLOCK") %>' Checked='<%# GetStatus(Eval("AWARD_LOCK_UNLOCK")) %>'/></td>
                                                                <td><asp:CheckBox runat="server" ID="comptchk" ToolTip='<%# Eval("COMPETITION_LOCK_UNLOCK") %>' Checked='<%# GetStatus(Eval("COMPETITION_LOCK_UNLOCK")) %>'/></td>
                                                                <td><asp:CheckBox runat="server" ID="trngchk" ToolTip='<%# Eval("TRAINING_LOCK_UNLOCK") %>' Checked='<%# GetStatus(Eval("TRAINING_LOCK_UNLOCK")) %>'/></td>
                                                                <td><asp:CheckBox runat="server" ID="tessctchk" ToolTip='<%# Eval("TEST_SCORE_LOCK_UNLOCK") %>' Checked='<%# GetStatus(Eval("TEST_SCORE_LOCK_UNLOCK")) %>'/></td>
                                                                <td><asp:CheckBox runat="server" ID="BildReschk" ToolTip='<%# Eval("BUILD_RESUME_LOCK_UNLOCK") %>' Checked='<%# GetStatus(Eval("BUILD_RESUME_LOCK_UNLOCK")) %>'/></td>

                                                            </tr>
                                                        </AlternatingItemTemplate>
                                                    </asp:ListView>

                                                </div>

       
                  
                </div>
            </div>
        </div>

     
    </div>
</asp:Content>

