<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentMessAllotment.aspx.cs" Inherits="HOSTEL_MessAllotment" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
      function RunThisAfterEachAsyncPostback()
       {
            RepeaterDiv();

       }
    
   function RepeaterDiv()
{
  $(document).ready(function() {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
 
}
    </script>

    <script src="../Content/jquery.js" type="text/javascript"></script>

    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript" charset="utf-8">
        $(document).ready(function() {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <asp:Panel ID="pnlMain" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
              
                <div class="row">
                            <div class="col-md-12">
                                <div class="box box-primary">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">MESS ALLOTMENT</h3>
                                        <div class="box-tools pull-right"></div>
                                    </div>

                                    <div style="color: Red; font-weight: bold">
                                        &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                        <asp:Label ID="lblHelp" runat="server"></asp:Label>
                                    </div>

                                    <div class="box-body">
                                        <div class="form-group col-md-3">
                                            <label><span style="color: red;">*</span>Session</label>
                                             <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" ValidationGroup="teacherallot"
                                      CssClass="form-control">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                            </div>
                                        <div class="form-group col-md-3">
                                            <label>Hostel:</label>
                                      <asp:DropDownList ID="ddlhostel" runat="server" AppendDataBoundItems="true"  
                                        AutoPostBack="True"  ValidationGroup="teacherallot" CssClass="form-control">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="ddlSemester" Display="None" Enabled="false"  
                                        ErrorMessage="Please Select Semester" InitialValue="0" 
                                        ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                            </div>
                                          <div class="form-group col-md-3">
                                            <label>Mess:</label>
                                     <asp:DropDownList ID="ddlmessfilter" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherassign"
                                        CssClass="form-control">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlMess"
                                        Display="None" ErrorMessage="Please Select Batch" InitialValue="0" ValidationGroup="teacherassign"></asp:RequiredFieldValidator>
                  

                                              </div>
                                              <div class="form-group col-md-3">
                                            <label>Degree:</label>
                                  <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                        ValidationGroup="teacherallot"  AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlDepartment" runat="server" ControlToValidate="ddlDegree"
                                      Enabled="false"   Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                                  </div>
                                         <div class="form-group col-md-3">
                                            <label>Branch:</label>
                                   <asp:DropDownList ID="ddlBranch"   runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                        ValidationGroup="teacherallot" >
                                        
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                       Enabled="false"  Display="None" InitialValue="0" ErrorMessage="Please Select Branch"
                                         ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                             </div>
                                                <div class="form-group col-md-3">
                                            <label>Semester:</label>
                               <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" 
                                        AutoPostBack="True"  ValidationGroup="teacherallot"  CssClass="form-control">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSemester" runat="server" 
                                        ControlToValidate="ddlSemester" Display="None" Enabled="false"  
                                        ErrorMessage="Please Select Semester" InitialValue="0" 
                                        ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                                    </div>
                                                           <div class="form-group col-md-3" style ="display :none";>
                                            <label>Section:</label>
                               <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true"  CssClass="form-control"
                                        ValidationGroup="teacherallot">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection"
                                        Display="None" InitialValue="0" Enabled="false" ErrorMessage="Please Select Section" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                                               </div>
                                        <div class="form-group col-md-3" id = "trFilter" runat = "server">
                                            <label>Filter By:</label>

                                            </div>
                                        <div id = "trRollNo" runat = "server" class="form-group col-md-3">
                                       <label>By Adm. No From:</label>
                                               <asp:TextBox ID="txtFromRollNo"  CssClass="form-control" runat="server" />
                                        <asp:CompareValidator ID="svFromTo" runat="server" ControlToCompare="txtFromRollNo"
                                        ControlToValidate="txtTotStud" Display="None" ErrorMessage="Please Valid Range"
                                        Operator="LessThanEqual" Type="Integer" ValidationGroup="teacherallot"></asp:CompareValidator>
                                            </div>
                        <div class="form-group col-md-3">
                                                <label>To:</label>
                                    <asp:TextBox ID="txtToRollNo" runat="server"  CssClass="form-control" />
                                

                            </div>
                                <div id = "trRdo" runat = "server" class="form-group col-md-3">
                                            <asp:RadioButton ID="rbAll" runat="server" GroupName="stud" Text="All Students" Checked="True" />&nbsp;&nbsp;
                                    <asp:RadioButton ID="rbRemaining" runat="server" GroupName="stud" Text="Remaining Students" />
                 
                                      </div>
                                        </div>

                                       <div class="box-footer">
                                        <p class="text-center">
                                    <asp:Button ID="btnFilter" runat="server" CssClass="btn btn-primary" Text="Show" ValidationGroup="teacherallot"
                                   OnClick="btnFilter_Click" />&nbsp;
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-warning" OnClick="btnClear_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        
                                            </p>
                                            </div>
                                     <div class="box-body">
                                        <p class="text-center">
                                     <div class="form-group col-md-3">
                                     <label>Mess:</label>
                                       <asp:DropDownList ID="ddlMess" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherassign"
                                        CssClass="form-control">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBatch" runat="server" ControlToValidate="ddlMess"
                                        Display="None" ErrorMessage="Please Select Batch" InitialValue="0" ValidationGroup="teacherassign"></asp:RequiredFieldValidator>
                     
                                  </div>
                                            </p>
                                            </div>
                                    <div class="box-body">
                                        <p class="text-center">
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="teacherassign"
                                        OnClientClick="return validateAssign();" Width="100px" OnClick="btnSave_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="teacherassign"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                            </p>
                                        </div>
                                      <div class="box-body">
                                        <p class="text-center">
                                             <div class="form-group col-md-3">
                                            <label>Total Selected Students:</label>
                                       <asp:TextBox ID="txtTotStud" runat="server"  Enabled="false" CssClass="watermarked" />
                                    <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                       <asp:Label ID="lblStatus2" runat="server" SkinID="Errorlbl" />
                                            </p>
                                          </div>
                                   
                          
                    <asp:Panel ID="pnlStudent" runat="server">
                         <asp:ListView ID="lvStudents" runat="server" OnItemDataBound="lvStudents_ItemDataBound">
                        <LayoutTemplate>
                            <div id="demo-grid" class="vista-grid">
                                <div class="titlebar">
                                    Student List</div>
                                <table class="table table-hover table-bordered display">
                                    <tr class="bg-light-blue">
                                        <th>
                                            <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" />
                                        </th>
                                        <th>
                                            Reg No.
                                        </th>
                                        <th>
                                           Name
                                        </th>
                                        <th>
                                           Hostel Name
                                        </th>
                                        <th>
                                            Mess Name
                                        </th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server" />
                                </table>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class="item" >
                                <td>
                                    <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNo")%>' />
                                </td>
                                <td>
                                    <%# Eval("REGNO")%>
                                </td>
                                <td>
                                    <%# Eval("STUDNAME")%>
                                </td>
                                <td>
                                    <%# Eval("HOSTELNAME")%>
                                </td>
                                <td>
                                    <%# Eval("MESS_NAME")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                <td>
                                    <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNo")%>' />
                                </td>
                                 <td>
                                    <%# Eval("REGNO")%>
                                </td>
                                <td>
                                   <%# Eval("STUDNAME")%>
                                </td>
                                <td>
                                      <%# Eval("HOSTELNAME")%>
                                </td>
                                <td>
                                    <%# Eval("MESS_NAME")%>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:ListView>
                </asp:Panel>
         </div>
                    </div>
                
                                   </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>

    <script type="text/javascript" language="javascript">

    function totSubjects(chk)
	{				    				
	    var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');	    
	    				
		if (chk.checked == true)					
			txtTot.value = Number(txtTot.value) + 1 ;		 
		else		
			txtTot.value = Number(txtTot.value) - 1 ;			
	}	
	
	function totAllSubjects(headchk)
	{				    				
	    var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');	    
	    var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');	    
		
		var frm = document.forms[0]
		for (i=0; i < document.forms[0].elements.length; i++)  
		{				
			var e = frm.elements[i];
			if (e.type == 'checkbox')
			{
			   if (headchk.checked == true)				  	    
			       e.checked = true;			       	
			   else
			       e.checked = false;			       
			}
		}
				
		if (headchk.checked == true)
		    txtTot.value = hdfTot.value;
		else
		    txtTot.value = 0;
	}	
	
	function validateAssign()
	{				    				
	    var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;

	    if ( document.getElementById('<%= ddlMess.ClientID %>').selectedIndex == 0) {
	        alert('Please Select Mess');
	        return false;
	    }
        else if (txtTot == 0 ) {
               
               alert('Select at least 1 student');
               return false;
         }
		else		
			return true;
	}		
	
    </script>

</asp:Content>
