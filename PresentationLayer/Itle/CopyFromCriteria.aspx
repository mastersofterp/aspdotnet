<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CopyFromCriteria.aspx.cs" Inherits="Itle_CopyFromCriteria" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

 
 <script type="text/javascript">
 function totAllIDs(headchk) {

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        e.checked = true;

                    }
                    else {
                        e.checked = false;

                    }
                }
            }
        }
  </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-16">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SELECT COPY  TO WORK IN ITLE SESSION</h3>
                            <div class="box-tools pull-right">
                              
                            </div>
                        </div>
                        <div>
                            <form role="form">
                                <div class="box-body">
                                    <div class="col-md-12">
                                        Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                        <asp:Panel ID="pnlSelectCourse" runat="server">
                                          
                                                <asp:Panel ID="pnlCourse" class="form-group col-md-12" runat="server">
                                                    <div class="panel panel-info">
                                                        <div class="panel panel-heading">Select Session and Course </div>
                                                        <div class="panel panel-body">

                                                       <div class="form-group col-md-6">
                                                        <div id="tblCopyFromCriteria" runat="server">
                                                            <div id="divUpcomingTests">
                                                                <div class="row" style="border: solid 1px #CCCCCC">
                                                                    <div style="font-weight: bold; background-color: #72A9D3; color: white" class="panel-heading">Select Copy From Criteria</div>
                                                                    <div class="DocumentList">
                                                                        <asp:Panel ID="PnlList" runat="server"  Height="305px" BackColor="#FFFFFF">
                                                                   
                                                                                <div class="col-md-8">
                                                                                        <label><span style="color: Red">*</span>Session :</label>
                                                                                   <asp:DropDownList ID="ddlFromSession" runat="server" AppendDataBoundItems="true" TabIndex="1" 
                                                                                     AutoPostBack="true" OnSelectedIndexChanged="ddlFromSession_SelectedIndexChanged"  CssClass="form-control" ToolTip="Select Session">
                                                                                    </asp:DropDownList>
                                                                                        <br />
                                                                                </div>
                                                             
                                                                                <div class="col-md-8">
                                                                                    <label><span style="color: Red">*</span>Course :</label>
                                                                                    <asp:DropDownList ID="ddlFromCourse" runat="server" AppendDataBoundItems="true"
                                                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlFromCourse_SelectedIndexChanged"   TabIndex="1"
                                                                                         CssClass="form-control" ToolTip="Select Session">
                                                                                    </asp:DropDownList>
                                                                                    <br />
                                                                                   
                                                                                </div>

                                                                            <div class="col-md-8" >
                                                                                    <label > Topic :</label>
                                                                                    <asp:DropDownList ID="ddlTopic"  runat="server" AppendDataBoundItems="true" Visible="true"
                                                                                          TabIndex="1"
                                                                                         CssClass="form-control" ToolTip="Select Session">
                                                                                    </asp:DropDownList>
                                                                                    <br />
                                                                                </div>
                                                                           

                                                                                <div class="form-group">
                                                                                <div class="col-md-12">
                                                                                    <div class="col-md-6">
                                                                                        <label>Select the Type of Question :</label>
                                                                                       
                                                                                    </div>
                                                                                    <div class="col-md-6">
                                                                                        <asp:RadioButtonList ID="rbnObjectiveDescriptive" Font-Bold="true" AutoPostBack="true"
                                                                                            runat="server" RepeatDirection="Horizontal" TabIndex="1" ToolTip="Select Question Type"
                                                                                             OnSelectedIndexChanged="rbnObjectiveDescriptive_SelectedIndexChanged"  Width="216px">
                                                                                            <asp:ListItem Text="Objective" Value="O" Selected="True"></asp:ListItem> 
                                                                                            <asp:ListItem Text="Descriptive" Value="D"></asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                         <br />
                                                                                         <br />
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                             <div class="col-md-8" >
                                                                                  <div class="text-center">
                                                                                  <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show"  TabIndex="16"
                                                                                    CssClass="btn btn-primary" ToolTip="Click here to Show Question" />
                                                                                      
                                                                                 </div>
                                                                                 <br />
                                                                                 <br />
                                                                              </div>

                                                                        </asp:Panel>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                      <div class="form-group col-md-6">
                                                        <div id="divCopyToCriteria">                                                          
                                                            <div class="row" style="border: solid 1px #CCCCCC">
                                                                <div style="font-weight: bold; background-color: #72A9D3; color: white" class="panel-heading">Select Copy To Criteria</div>
                                                                <div class="DocumentList">
                                                                    <asp:Panel ID="pnlCopyToCriteria" runat="server"  Height="300px" BackColor="#FFFFFF">
                                                                        <br />
                                                                        <div class="col-md-8">
                                                                            <label><span style="color: Red">*</span>Session :</label>
                                                                            <asp:DropDownList ID="ddlToSession" runat="server" AppendDataBoundItems="true"
                                                                               AutoPostBack="true" OnSelectedIndexChanged="ddlToSession_SelectedIndexChanged"  TabIndex="1"
                                                                                 CssClass="form-control" ToolTip="Select Session">
                                                                            </asp:DropDownList>
                                                                             <br />
                                                                                 
                                                                        </div>
                                                         
                                                                  
                                                                        <div class="col-md-8">
                                                                             <label><span style="color: Red">*</span>Course :</label>
                                                                            <asp:DropDownList ID="ddlToCourses" runat="server" AppendDataBoundItems="true"
                                                                                TabIndex="1"
                                                                               CssClass="form-control" ToolTip="Select Session">
                                                                            </asp:DropDownList>
                                                                             <br />
                                                                                  <br /> <br />
                                                                                  <br /> <br />
                                                                                  <br /> <br />
                                                                                 <br /> <br />
                                                                             <br />
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            <asp:Panel ID="Panel1" runat="server">

                                                                           <div class="form-group">
                                                                            <div class="col-md-12">
                                                                             <div class="text-center">
                                                                                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Transfer" ValidationGroup="Submit" TabIndex="16"
                                                                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                                                                    &nbsp;<asp:Button ID="btnCancel" runat="server"  Text="Cancel" TabIndex="17"
                                                                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" />

                                                                                  <br />
                                                                                  <br />
                                                                              <%--  <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                                     ShowSummary="false" ValidationGroup="Submit" />--%>
                                                                                      </div>
                                                                              </div>
                                                                            </div>
                                                 </asp:Panel>
                                              <asp:Panel ID="pnllvquestion" runat="server"  Visible="false">

                                                                              <div class="form-group col-md-12">
                                                                              <div class="col-sm-12" id="grid">
                                                                               <div class="row" style="border: solid 1px #CCCCCC">
                                                                                 <div style="font-weight: bold; background-color: #72A9D3; color: white" class="panel-heading">Questions List</div>
                                                                                 <div class="table-responsive">
                                                                            <table class="customers">
                                                                                <tr style="font-weight: bold; background-color: #808080; color: white">
                                                                                   <th style="width: 6%; padding-left: 8px; text-align: left">
                                                                                        <asp:CheckBox ID="cbHead" runat="server" Checked="false"
                                                                                            onclick="totAllIDs(this)" />
                                                                                    </th>
                                                                                    <th style="width: 34%; text-align: left">Question Text</th>
                                                                                    <th style="width: 8%; text-align: left">Topic</th>
                                                                                    <th style="width: 3%; text-align: left">Q.Marks</th>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                                 <div class="DocumentList">
                                                                            <asp:Panel ID="pnllvView" runat="server" ScrollBars="Both"  Height="400px" BackColor="#FFFFFF">
                                                                                <asp:ListView ID="lvQuestions" runat="server">
                                                                                    <LayoutTemplate>
                                                                                        <div id="demo-grid">
                                                                                            <table class="table table-bordered table-hover">
                                                                                                <thead>
                                                                                                </thead>
                                                                                                <tbody>
                                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </div>
                                                                                    </LayoutTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            
                                                                                              <td style="width: 6%; padding-left: 8px;">
                                                                                                 <asp:CheckBox ID="chkQueNo" runat="server" ToolTip='<%# Eval("QUESTIONNO") %>' />
                                                                                                <asp:HiddenField ID="hidQueNo" runat="server" Value='<%# Eval("QUESTIONNO") %>' />

                                                                                            </td>
                                                                                            <td style="width: 44%; text-align: left">
                                                                                                <asp:Label ID="LblQUESTIONTEXT" runat="server" Text='<%# Eval("QUESTIONTEXT")%>' />
                                                                                               
                                                                                            </td>
                                                                                            <td style="width: 18%; text-align: left">
                                                                                                <asp:Label ID="LblTOPIC" runat="server" Text='<%# Eval("TOPIC")%>' />
                                                                                               
                                                                                            </td>

                                                                                            <td style="width: 4%; text-align: left">
                                                                                                <asp:Label ID="LblMARKS_FOR_QUESTION" runat="server" Text='<%# Eval("MARKS_FOR_QUESTION")%>' />
                                                                                               
                                                                                           </td>

                                                                                                    </tr>
                                                                                                </ItemTemplate>
                                                                                            </asp:ListView>
                                                                                        </asp:Panel>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                         </asp:Panel>
                                        </asp:Panel>


                                   

                                    </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

 
</asp:Content>
