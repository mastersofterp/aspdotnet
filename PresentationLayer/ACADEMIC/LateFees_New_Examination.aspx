<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LateFees_New_Examination.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_EXAMINATION_LateFees_New_Examination" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css") %>" rel="stylesheet" /><script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>
    <style>
  
        .checkbox-list-box
         {
            min-height: 100%;
            height: auto;
         }
    </style>
    <script type="text/javascript">

        function dateValidation() {
            var obj = document.getElementById("<%=txtToDate.ClientID%>");
            var day = obj.value.split("/")[0];
            var month = obj.value.split("/")[1];
            var year = obj.value.split("/")[2];
            if ((day < 1 || day > 31) || (month < 1 || month > 12) || (year.length != 4))
            {
                alert("Invalid Date Selected");
                return false;
            }
        }

        function SelectAllDegree() {
            var CHK = document.getElementById("<%=chkDegrees.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");
            var chkDeg = document.getElementById('ctl00_ContentPlaceHolder1_chkDegree');
            for (var i = 0; i < checkbox.length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_chkDegrees_' + i);
                if (chkDeg.checked == true) {
                    chk.checked = true;
                }
                else {
                    chk.checked = false;
                }
            }
        }
        function UnSelectAllDegree() {
            var CHK = document.getElementById("<%=chkDegrees.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");
            var chkDeg = document.getElementById('ctl00_ContentPlaceHolder1_chkDegree');
            for (var i = 0; i < checkbox.length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_chkDegrees_' + i);
                if (chk.checked == false) {
                    chkDeg.checked = false;
                    return;
                } else {
                    chkDeg.checked = true;
                }

            }
        }

        function ShowReactivationControls() {
            var chkRActFee = document.getElementById('ctl00_ContentPlaceHolder1_chkReactivationFee');
            var divRActFees = $('#ctl00_ContentPlaceHolder1_dvRActFees');
            if (chkRActFee.checked == true) {
                divRActFees.show();

            } else {
                divRActFees.hide();
            }
        }
        //window.onload = ShowReactivationControls();


    
      
    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnlFeeTable"
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

    <asp:UpdatePanel ID="pnlFeeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">CHARGE LATE FEE</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-6 col-md-12 col-12">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-12 col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-12 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                           <%-- <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>--%>
                                                              <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" ToolTip="Please Select School/Institute." AutoPostBack="true"
                                                            OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" ValidationGroup="submit">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                                            Display="None" ErrorMessage="Please Select School/Institute." InitialValue="0" SetFocusOnError="true"
                                                            ValidationGroup="submit">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-12 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSession" AppendDataBoundItems="true" runat="server"
                                                            AutoPostBack="true" TabIndex="1" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSession"
                                                            Display="None" ErrorMessage="Please select Session." ValidationGroup="submit"
                                                            InitialValue="0" SetFocusOnError="true" />
                                                    </div>

                                                    <div class="form-group col-lg-12 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Exam Type</label>--%>
                                                            <asp:Label ID="lblDYddlExamType" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>

                                                        <asp:DropDownList ID="ddlReceiptType" AppendDataBoundItems="true" runat="server" OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged"
                                                            AutoPostBack="true" TabIndex="1" CssClass="form-control" data-select2-enable="true" />
                                                        <asp:RequiredFieldValidator ID="valReceiptType" runat="server" ControlToValidate="ddlReceiptType"
                                                            Display="None" ErrorMessage="Please select Receipt Type." ValidationGroup="submit"
                                                            InitialValue="0" SetFocusOnError="true" />
                                                    </div>

                                                    <div class="form-group col-lg-12 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <%-- <sup>* </sup>--%>
                                                            <label>Accept under Fee Item</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlFeeItems" AppendDataBoundItems="true" runat="server"
                                                            AutoPostBack="true" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-12 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Last Date For Fees</label>--%>
                                                            <asp:Label ID="lblDYDtLastDateForFees" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <div class="input-group">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar" id="date" runat="server"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtToDate" runat="server" TabIndex="6" CssClass="form-control" ValidationGroup="submit" />
                                                            <%-- <asp:Image ID="imgExamDate" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtToDate" PopupButtonID="date" Enabled="true" EnableViewState="true" />
                                                            <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                                                                Display="None" ErrorMessage="Please enter last date for charging late fee." SetFocusOnError="true"
                                                                ValidationGroup="submit" />
                                                            <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" MessageValidatorTip="true" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="true" MaskType="Date" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-12 col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-12 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Degree</label>--%>
                                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <div class="well checkbox-list-box">
                                                            <%--style="max-height: 100px; overflow: auto;"--%>
                                                            <ul id="check-list-box" class="list-group checked-list-box">
                                                                <li class="list-group-item">
                                                                    <asp:CheckBox ID="chkDegree" runat="server" Text="All Degree" onClick="SelectAllDegree()" CssClass="select-all-checkbox" TabIndex="6" />
                                                                    <asp:CheckBoxList ID="chkDegrees" runat="server" CssClass="checkbox-list-style" onClick="UnSelectAllDegree()">
                                                                    </asp:CheckBoxList></li>
                                                            </ul>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-12 col-md-6 col-12 mt-4 d-none">
                                                        <div class="label-dynamic">
                                                            <asp:CheckBox ID="chkReactivationFee" runat="server" Text=" Allow Reactivation Fee" onClick="ShowReactivationControls()" />
                                                        </div>
                                                    </div>
                                                    <div id="dvRActFees" runat="server" class="form-group col-lg-12 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <asp:Label ID="lblReactivationFee" runat="server" Font-Bold="true" Text=" Reactivation Amount"></asp:Label>
                                                        </div>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtReactivationfees" runat="server" CssClass="form-control"
                                                                onkeydown="return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106) && event.keyCode!=32);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                                <div class="col-lg-6 col-md-12 col-12">
                                        <div class="row">
                                            <div id="fsem" runat="server" class="form-group col-lg-12 col-md-6 col-12">

                                                <div class="label-dynamic">

                                                    <sup>*</sup>
                                                    <%--<label>Semester</label>--%>
                                                    <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>

                                                </div>

                                                <asp:ListBox ID="lstSemester" runat="server" CssClass="form-control multi-select-demo" AutoPostBack="true" SelectionMode="Multiple" TabIndex="6" OnSelectedIndexChanged="lstSemester_SelectedIndexChanged"></asp:ListBox>

                                                <asp:RequiredFieldValidator ID="rfSemester" runat="server" ControlToValidate="lstSemester" Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>

                                                <%--ValidationGroup="Show"--%>
                                            </div>

                                         
                                        </div>
                                    </div>



                                        </div>
                                    </div>

                                    <div class="col-lg-6 col-md-12 col-12 ">

                                        <div class="sub-heading">
                                            <h5><sup>*</sup>Criteria for Charging Late Fee</h5>
                                        </div>


                                        <asp:GridView ID="gvLateFees" runat="server" AutoGenerateColumns="false" OnRowDeleting="gvLateFees_RowDeleting" OnRowCreated="gvLateFees_RowCreated"
                                            CssClass="table table-striped table-bordered table-hover" ShowFooter="true">
                                            <Columns>
                                                <asp:BoundField DataField="RowNumber" HeaderText="Sr.No." HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:TemplateField HeaderText="Day From" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="visible-desktop" ItemStyle-CssClass="visible-desktop">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDayNoFrom" runat="server"
                                                            CssClass="form-control">
                                                        </asp:TextBox>
                                                        <asp:HiddenField ID="hdnval" runat="server" Value='<%# Bind("ROWNUMBER") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Day To" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="visible-desktop" ItemStyle-CssClass="visible-desktop">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDayNoTo" runat="server"
                                                            CssClass="form-control">
                                                        </asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fees" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="visible-desktop" ItemStyle-CssClass="visible-desktop">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtFees" runat="server"
                                                            CssClass="form-control">
                                                        </asp:TextBox>
                                                        <asp:LinkButton ID="lnkRemove" runat="server" OnClick="lnkRemove_Click"
                                                            Style="float: none;">
                                                                                   Remove
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <FooterTemplate>
                                                        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                                                            CssClass="btn btn-primary" Text="Add New Fee" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>


                                        <div class="form-group col-lg-12 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <asp:CheckBox ID="chkFixedAmtFlag" runat="server" Text="Late fee Should be Fixed"></asp:CheckBox>
                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnChargeLateFee" runat="server" Text="Charge Late Fee" OnClick="btnChargeLateFee_Click"
                                ValidationGroup="submit" TabIndex="9" CssClass="btn btn-primary" OnClientClick="return dateValidation();" />&nbsp;&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                            TabIndex="10" CssClass="btn btn-warning" />
                            <asp:ValidationSummary ID="valSummery" DisplayMode="List" runat="server" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="submit" />
                            

                        </div>
                        <div class="col-12">
                            <asp:ListView ID="lvLateFeesDEtails" runat="server">
                                <LayoutTemplate>
                                    <div id="demo-grid" class="vista-grid">
                                        <div class="sub-heading">
                                            <h5>Late Fees Details</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr.No.
                                                    </th>
                                                    <th>Edit
                                                    </th>
                                                    <th>Delete
                                                    </th>
                                                    <th>School/Institute
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label>
                                                    </th>
                                                    <th>Receipt Type
                                                    </th>
                                                    <th>Last Date
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
                                    <tr class="item">
                                        <td>
                                            <%# Container.DataItemIndex + 1%>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" OnClick="btnEdit_Click"
                                                CommandArgument='<%# Eval("LATE_FEE_NO") %>' ImageUrl="~/images/edit.png"
                                                ToolTip="Edit Record" />
                                            <%--<asp:HiddenField ID="hdnSeqNo" runat="server" Value='<%# Eval("SEQ_NO") %>' />--%>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Delete Record" OnClick="btnDelete_Click"
                                                OnClientClick="return confirm('Are You Sure,You Want Delete this Entry?')" CommandArgument='<%# Eval("LATE_FEE_NO") %>' ImageUrl="~/Images/delete.png"
                                                ToolTip="Delete Record" />
                                        </td>
                                        <td>
                                            <%# Eval("COLLEGE_NAME") %>
                                        </td>
                                        <td>
                                            <%# Eval("DEGREENAME") %>
                                        </td>
                                        <td>
                                            <%# Eval("RECEIPT_TYPE")%>
                                        </td>
                                        <td>
                                            <%# Eval("LAST_DATE","{0:MMM dd, yyyy}")%><%-- hh:mm tt dd/MM/yyyy--%>
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
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>


      <script type="text/javascript">


       


          $(document).ready(function () {
              debugger;
              $('.multi-select-demo').multiselect({
                  includeSelectAllOption: true,
                  maxHeight: 200,
                  enableFiltering: true,
                  filterPlaceholder: 'Search',
                  enableCaseInsensitiveFiltering: true,
              });
          });

          var parameter = Sys.WebForms.PageRequestManager.getInstance();
          parameter.add_endRequest(function () {
              
              $(document).ready(function () {
                  $('.multi-select-demo').multiselect({
                      includeSelectAllOption: true,
                      maxHeight: 200,
                      enableFiltering: true,
                      filterPlaceholder: 'Search',
                      enableCaseInsensitiveFiltering: true,
                  });
              });
          });


    </script>
        <script>

           

    </script>


</asp:Content>
  