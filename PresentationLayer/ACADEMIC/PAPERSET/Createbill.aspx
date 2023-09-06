<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Createbill.aspx.cs" Inherits="ACADEMIC_Createbill" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        var ddlText, ddlValue, ddl, lblMesg;
        function CacheItems() {
            ddlText = new Array();
            ddlValue = new Array();
            ddl = document.getElementById("<%=ddlFac.ClientID %>");
            for (var i = 0; i < ddl.options.length; i++) {
                ddlText[ddlText.length] = ddl.options[i].text;
                ddlValue[ddlValue.length] = ddl.options[i].value;
            }
        }
        window.onload = CacheItems;
        function FilterItems(value) {
            ddl.options.length = 0;
            for (var i = 0; i < ddlText.length; i++) {
                if (ddlText[i].toLowerCase().indexOf(value) != -1) {
                    AddItem(ddlText[i], ddlValue[i]);
                }
            }
            lblMesg.innerHTML = ddl.options.length + " items found.";
            if (ddl.options.length == 0) {
                AddItem("No items found.", "");
            }
        }
        function AddItem(text, value) {
            var opt = document.createElement("option");
            opt.text = text;
            opt.value = value;
            ddl.options.add(opt);
        }
    </script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updFacAllot"
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
    <asp:UpdatePanel ID="updFacAllot" runat="server">
        <ContentTemplate>
            <div class="page_help_text">
                <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
            </div>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">CREATE BILL</h3>
                        </div>
                        <div class="box-body">
                            <div id="divbill" runat="server" class="col-12">
                                <div class="sub-heading">
                                    <h5>Bill</h5>
                                </div>
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-12" id="trsession" runat="server">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Session Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSession" AutoPostBack="true" runat="server" AppendDataBoundItems="true"
                                                    Font-Bold="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Select Session"
                                                    ControlToValidate="ddlSession" Display="None" ValidationGroup="submit" InitialValue="0" />
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12" runat="server">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Remuneration Bill for</label>
                                                </div>
                                                <asp:TextBox ID="txtBill" runat="server" ReadOnly="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please enter Remuneration Bill"
                                                    ControlToValidate="txtBill" Display="None" ValidationGroup="submit" InitialValue="0" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-12" id="trdegree" runat="server">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Degree</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ErrorMessage="Please Select Degree"
                                                    ControlToValidate="ddlDegree" Display="None" ValidationGroup="submit" InitialValue="0" />
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12" id="Div1" runat="server">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Name of Examiner (In Capital)</label>
                                                </div>
                                                <asp:TextBox ID="txtExminer" runat="server" ReadOnly="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Name of Examiner"
                                                    ControlToValidate="txtExminer" Display="None" ValidationGroup="submit" InitialValue="0" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6 col-md-6 col-12" id="tremail" runat="server">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Email ID</label>
                                                </div>
                                                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please Enter Valid Email ID"
                                                    ValidationGroup="submit" ControlToValidate="txtEmail" CssClass="requiredFieldValidateStyle"
                                                    ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Mobile No</label>
                                                </div>
                                                <asp:TextBox ID="txtmobile" runat="server" MaxLength="10"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftv" runat="server" TargetControlID="txtmobile"
                                                    FilterType="Numbers" FilterMode="ValidChars" ValidChars="0123456789.">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6 col-md-6 col-12" id="trbank" runat="server">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Bank Branch</label>
                                                </div>
                                                <asp:TextBox ID="txtbankbranch" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12" runat="server">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Name of Department</label>
                                                </div>
                                                <asp:TextBox ID="txtNod" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Name of Department"
                                                    ControlToValidate="txtNod" Display="None" ValidationGroup="submit" InitialValue="0" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6 col-md-6 col-12" id="trifsc" runat="server">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>IFSC Code</label>
                                                </div>
                                                <asp:TextBox ID="txtIfsccode" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12" runat="server">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>SBI A/C No</label>
                                                </div>
                                                <asp:TextBox ID="txtSbiacno" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please enter SBI A/C NO"
                                                    ControlToValidate="txtSbiacno" Display="None" ValidationGroup="submit" InitialValue="0" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6 col-md-6 col-12" id="trcourse" runat="server">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12 d-none">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Course Code</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCourse_auto" runat="server" AppendDataBoundItems="true"
                                                    AutoPostBack="True">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvCourse_auto" runat="server" ControlToValidate="ddlCourse_auto"
                                                    Display="None" ErrorMessage="Please Select Course" InitialValue="0" ValidationGroup="submit" />
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12" runat="server">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Postal Address</label>
                                                </div>
                                                <asp:TextBox ID="txtPostaladdres" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPostaladdres"
                                                    Display="None" ErrorMessage="Please Select Postal Address" InitialValue="0" ValidationGroup="submit" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6 col-md-6 col-12" id="tradmin" runat="server">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:Button ID="btnUpdate" runat="server" Text="ADD" CausesValidation="false" ValidationGroup="submit" class="btn btn-primary"
                                                    OnClick="btnUpdate_Click" Enabled="true" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:DropDownList ID="ddlFacUpd" runat="server" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:Label ID="lblyear" runat="server" Visible="false"></asp:Label>
                                            </div>

                                        </div>
                                    </div>

                                    <div id="trbutton" class="col-12 btn-footer" runat="server">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CausesValidation="false" class="btn btn-primary"
                                            ValidationGroup="submit" OnClientClick="return Validation();" OnClick="btnSubmit_Click"
                                            Visible="false" />

                                        <asp:Button ID="btnCal" runat="server" Text="Submit" CausesValidation="false" ValidationGroup="submit" class="btn btn-primary"
                                            OnClientClick="return Validation();" OnClick="btnCal_Click" Enabled="true" />

                                        <asp:Button ID="btnReport" runat="server" Text="Bill Report" CausesValidation="false" class="btn btn-primary"
                                            OnClick="btnReport_Click" Enabled="true" />

                                        <asp:Button ID="btnLock" runat="server" Text="Lock" OnClick="btnLock_Click" class="btn btn-primary" />

                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" class="btn btn-warning"
                                            OnClick="btnCancel_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                    </div>
                                    <div class="col-12 mt-3 mb-3">
                                        <asp:ListView ID="lvCreatebill" runat="server" Enabled="true" OnItemDataBound="lvCreatebill_ItemDataBound">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>LIST</h5>
                                                </div>
                                                <table id="tblbill" class="table table-striped table-bordered nowrap display">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>SrNo
                                                            </th>
                                                            <th>Remuneration For
                                                            </th>
                                                            <th>Scheme
                                                            </th>
                                                            <th>Semester
                                                            </th>
                                                            <th>Branch
                                                            </th>
                                                            <th>Subject & Paper Code
                                                            </th>
                                                            <th>Number Examined
                                                            </th>
                                                            <th>Amount
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <body>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </body>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item">
                                                    <td>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRmno" runat="Server" Text='<%# Eval("REMUNERATION")%>' ToolTip='<%# Eval("RMNO") %>'
                                                            Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSchemetype" runat="server" Text='<%# Eval("SCHEMETYPE")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblsemester" runat="server" Text='<%# Eval("SEMESTERNO")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblbranch" runat="server" Text='<%# Eval("BRANCH")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%#Eval("RMNO") %>' onclick="bill(this);" />
                                                        <asp:Label ID="lblcode" runat="server" Text='<%# Eval("COURSE")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblcourse" runat="server" Text='<%# Eval("COURSENO")%>'></asp:Label>
                                                        <asp:HiddenField ID="hdnSelect" runat="server" Value='<%#Eval("RMNO") %>' />
                                                    </td>
                                                    <td>
                                                        <%--  <asp:TextBox ID="txtNum" runat="server" Width="25%"   onblur="return bill(this);"  OnTextChanged="txtNum_TextChanged" AutoPostBack="true" ></asp:TextBox>--%>
                                                        <asp:TextBox ID="txtNum" runat="server" OnTextChanged="txtNum_TextChanged" CssClass="form-control"
                                                            AutoPostBack="true" Visible="false"></asp:TextBox>

                                                        <asp:DropDownList ID="ddlNum" runat="server"
                                                            OnSelectedIndexChanged="ddlNum_onselectedIndexChanged" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                                            Enabled="false">
                                                            <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                        </asp:DropDownList>

                                                        <asp:TextBox ID="txtNex" runat="server" Text='<%#Eval("COUNT") %>'
                                                            onclick="mark(this);" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftv" runat="server" TargetControlID="txtNum" FilterType="Numbers" FilterMode="ValidChars"
                                                            ValidChars="0123456789">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ToolTip='<%#Eval("AMOUNT")%>' ID="txtAmount" runat="server" onclick="amt(this);"
                                                            onblur="return checkvalue(this);" Enabled="true" CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                            TargetControlID="txtAmount" FilterType="Numbers" FilterMode="ValidChars" ValidChars="0123456789">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>

                                    </div>

                                    <div class="row">
                                        <asp:Panel runat="server" ID="pnlRem">
                                            <div class="form-group col-md-12">
                                                <asp:Label runat="server" ID="lblMsg" Text="HOD Remuneration  and Varification Officer amount is not submitted for this department"
                                                    Style="color: Green; font-size: large" Font-Bold="true"></asp:Label>
                                            </div>

                                            <div class="form-group col-md-4">
                                                <label for="city">HOD Remuneration</label>
                                                <asp:TextBox runat="server" ID="txtRem" MaxLength="10" onclick="onedisable(this)"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender
                                                    ID="FilteredTextBoxExtenderrem" runat="server" TargetControlID="txtRem" FilterType="Numbers" FilterMode="ValidChars"
                                                    ValidChars="0123456789">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                            <div class="form-group col-md-4">
                                                <label for="city">Varification Officer</label>
                                                <asp:TextBox runat="server" ID="txtVar" MaxLength="10" onclick="onedisable(this)" onblur="checkVar(this)"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender
                                                    ID="FilteredTextBoxExtendervar" runat="server" TargetControlID="txtVar" FilterType="Numbers" FilterMode="ValidChars"
                                                    ValidChars="0123456789">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                            <div class="form-group col-md-1">
                                                <br />
                                                <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" class="btn btn-primary" />

                                            </div>

                                        </asp:Panel>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note</h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Please do not click on "Lock" button if you find any discrepancy/ mismatch in the bill. </span></p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Contact Exam Cell or MIS Team members for Clarification. </span></p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Fill in the particulars. </span></p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Select the tick box as applicable. </span></p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Press 'SUBMIT' button. </span></p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Preview the bill and ensure that you are satisfied with the bill being shown.</span></p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>If satisfied, press 'LOCK' button. You can not modify the bill after this. </span></p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Print the bill, sign over it and submit it to exam cell for processing.</span></p>
                                        </div>
                                    </div>


                                </div>
                                <div id="divMsg" runat="server">
                                </div>
                            </div>
                            <div class="col-12" id="unlock" runat="server">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label><span style="color: red;">*</span> School/College</label>
                                        </div>
                                        <asp:DropDownList ID="ddlcollege" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlcollege_SelectedIndexChanged1"
                                            AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="True" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlcollege"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSessionunlock" runat="server" AutoPostBack="True" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlSessionunlock_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Faculty</label>
                                        </div>
                                        <div class="d-none">
                                            <asp:TextBox ID="txtfac" runat="server" onkeyup="FilterItems(this.value)"></asp:TextBox>
                                            <ajaxToolKit:TextBoxWatermarkExtender ID="twe" runat="server" TargetControlID="txtfac"
                                                WatermarkText="Search Here">
                                            </ajaxToolKit:TextBoxWatermarkExtender>
                                        </div>
                                        <asp:DropDownList ID="ddlFac" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnUnlock" runat="server" Text="Unlock" OnClick="btnUnlock_Click" class="btn btn-primary"
                                        CausesValidation="False" />
                                    <asp:Button ID="btnExcel" runat="server" Text="Excel" CausesValidation="False" OnClick="btnExcel_Click"
                                        class="btn btn-primary" />
                                    <asp:Button ID="btnFaculty" runat="server" Text="Faculty" CausesValidation="False" class="btn btn-primary"
                                        OnClick="btnFaculty_Click" />
                                    <asp:Button ID="btnAdminshow" runat="server" Text="Display" class="btn btn-primary"
                                        CausesValidation="False" OnClick="btnAdminshow_Click" />
                                    <asp:Button ID="btnExcel2" runat="server" class="btn btn-primary" Text="Not Filled Remuneration Excel"
                                        CausesValidation="False" OnClick="btnExcel2_Click" />
                                    <asp:Button ID="btnCancellock" runat="server" Text="Clear" class="btn btn-warning"
                                        CausesValidation="False" OnClick="btnCancellock_Click" />
                                </div>
                            </div>
                            <div id="divlock" runat="server" class="col-12">
                                <asp:ListView ID="lvstatus" runat="server" Enabled="true">
                                    <LayoutTemplate>
                                        <div id="demo-grid">
                                            <table class="table table-striped table-bordered nowrap display">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>SrNo
                                                        </th>
                                                        <th>Faculty
                                                        </th>
                                                        <th>Status
                                                        </th>
                                                        <th>Lock
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
                                                <%#Container.DataItemIndex + 1 %>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblStatusexmr" runat="Server" Text='<%# Eval("EXMRNAME")%>' ToolTip='<%# Eval("EXMRUANO") %>'
                                                    Font-Bold="true"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblStatustype" runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnLockunlock" runat="server" Text="Lock" CssClass="btn btn-primary" CommandArgument='<%# Eval("EXMRUANO") %>'
                                                    OnCommand="btnLockunlock_Command" CommandName="Lock" />
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
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnExcel" />
            <asp:PostBackTrigger ControlID="btnExcel2" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        function bill(chk) {
            var a = chk.id;
            var len = a.length;
            //alert(chk.id);
            if (len == 54) {
                var b = a.substring(0, 44) + '_txtAmount';
                var c = a.substring(0, 44) + '_hdnSelect';
                var e = a.substring(0, 44) + '_chkSelect';
                var f = a.substring(0, 44) + '_txtNex';
                var numexm = a.substring(0, 44) + '_ddlNum';
                var d = document.getElementById('' + c + '').value;

            }
            else {
                var b = a.substring(0, 45) + '_txtAmount';
                var c = a.substring(0, 45) + '_hdnSelect';
                var e = a.substring(0, 45) + '_chkSelect';
                var f = a.substring(0, 45) + '_txtNex';
                var numexm = a.substring(0, 45) + '_ddlNum';
                var d = document.getElementById('' + c + '').value;
            }

            if (d == 1) {
                document.getElementById('' + b + '').disabled = false;
                document.getElementById('' + b + '').value = 250;
            }
            if (document.getElementById('' + a + '').checked == false) {
                document.getElementById('' + e + '').checked = false;
                document.getElementById('' + b + '').disabled = true;
                document.getElementById('' + b + '').value = '';
            }

            if (d == 3) {
                document.getElementById('' + b + '').disabled = false;
                document.getElementById('' + b + '').value = '';
                document.getElementById('' + numexm + '').disabled = false;

                if (document.getElementById('' + numexm + '').value == 1 || document.getElementById('' + numexm + '').value == 2) {
                    document.getElementById('' + b + '').value = 50;
                }
                if (document.getElementById('' + numexm + '').value == 3) {
                    document.getElementById('' + b + '').value = 75;
                }
                if (document.getElementById('' + numexm + '').value == 4) {
                    document.getElementById('' + b + '').value = 100;
                }
                if (document.getElementById('' + numexm + '').value == 5) {
                    document.getElementById('' + b + '').value = 125;
                }
                if (document.getElementById('' + numexm + '').value == 6) {
                    document.getElementById('' + b + '').value = 150;
                }
            }
            if (d == 4) {

                var m = document.getElementById('' + f + '').value;
                if (m == 1 || m == 2 || m == 3 || m == 4 || m == 5 || m == 6) {
                    document.getElementById('' + b + '').disabled = false;
                    document.getElementById('' + b + '').value = 50;
                }
                else {
                    document.getElementById('' + b + '').disabled = false;
                    document.getElementById('' + b + '').value = m * 8;
                }
            }

            if (d == 10) {
                document.getElementById('' + b + '').disabled = false;
                document.getElementById('' + b + '').value = 2500;

                //document.getElementById('ctl00_ContentPlaceHolder1_lvCreatebill_ctrl5_txtAmount').disabled = false;
            }
            if (document.getElementById('' + e + '').checked == true && d == 10) {
                //alert("First");
                document.getElementById('ctl00_ContentPlaceHolder1_lvCreatebill_ctrl5_txtAmount').disabled = true;
                document.getElementById('ctl00_ContentPlaceHolder1_lvCreatebill_ctrl4_txtAmount').disabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_lvCreatebill_ctrl5_txtAmount').value = "";
            }

            if (document.getElementById('' + e + '').checked == true && d == 11) {
                // alert("Second");
                document.getElementById('ctl00_ContentPlaceHolder1_lvCreatebill_ctrl4_txtAmount').disabled = true;
                document.getElementById('ctl00_ContentPlaceHolder1_lvCreatebill_ctrl5_txtAmount').disabled = false;
                document.getElementById('ctl00_ContentPlaceHolder1_lvCreatebill_ctrl4_txtAmount').value = "";
            }
            if (document.getElementById('' + e + '').checked == false && d == 11) {
                document.getElementById('ctl00_ContentPlaceHolder1_lvCreatebill_ctrl5_txtAmount').disabled = true;
            }


            if (document.getElementById('' + e + '').checked == false) {
                if (d != 2) {
                    //alert(d);
                    if (d == 11) {
                        document.getElementById('' + b + '').disabled = false;
                    }
                    else {
                        document.getElementById('' + e + '').checked = false;
                        document.getElementById('' + b + '').value = '';
                        document.getElementById('' + b + '').disabled = true;
                        document.getElementById('' + numexm + '').disabled = true;
                        document.getElementById('' + f + '').disabled = true;

                        document.getElementById('' + numexm + '').selectedIndex = 0;
                    }

                }

                else {

                    document.getElementById('' + b + '').disabled = false;
                }
            }


        }

        function mark(txt) {
            var a = txt.id;

            var b = a.length;
            var c = a.substring(0, 44) + '_txtNex';
            var d = a.substring(0, 44) + '_txtAmount';
            var f = a.substring(0, 44) + '_hdnSelect';
            var e = txt.value;
            var e = document.getElementById('' + c + '').value;
            var g = document.getElementById('' + f + '').value;

            if (g == 4) {

                if (e == 1 || e == 2 || e == 3 || e == 4 || e == 5 || e == 6) {
                    document.getElementById('' + d + '').disabled = false;
                    document.getElementById('' + d + '').value = 50;
                }
                else {
                    document.getElementById('' + d + '').disabled = false;
                    document.getElementById('' + d + '').value = e * 8;
                }
            }
            else {
                document.getElementById('' + c + '').disabled = true;
                document.getElementById('' + d + '').disabled = true;
            }

        }
        function amt(txt) {
            var a = txt.id;
            //alert(a.value);
            // alert(txt.id);
            var b = a.length;
            var c = a.substring(0, 44) + '_txtNex';
            var d = a.substring(0, 44) + '_txtAmount';
            var f = a.substring(0, 44) + '_hdnSelect';
            var e = txt.value;
            var e = document.getElementById('' + c + '').value;
            var g = document.getElementById('' + f + '').value;

            //alert(g);
            if (g == 2 || g == 11) {

                document.getElementById('' + d + '').disabled = false;
            }
            else {
                //document.getElementById('' + d + '').disabled = true;
            }


        }

        function checkvalue(txt) {
            var a = txt.id;
            //alert(a.value);
            var b = a.length;
            var c = a.substring(0, 44) + '_txtNex';
            var d = a.substring(0, 44) + '_txtAmount';
            var f = a.substring(0, 44) + '_hdnSelect';
            var e = txt.value;
            var e = document.getElementById('' + c + '').value;
            var g = document.getElementById('' + f + '').value;
            if (g == 11) {
                //alert("hi");

                var amt = document.getElementById('' + d + '').value;
                //alert(amt);
                if (amt > Number(1500)) {
                    alert("You Could Not Enter Amount More Than Rs. 1500");
                    document.getElementById('' + d + '').value = "";
                }
            }
        }


        function Validation() {
            var rem = document.getElementById('<%=txtBill.ClientID %>').value;
            var name = document.getElementById('<%=txtExminer.ClientID %>').value;
            var mob = document.getElementById('<%=txtmobile.ClientID %>').value;
            var nod = document.getElementById('<%=txtNod.ClientID %>').value;
            var sbiac = document.getElementById('<%=txtSbiacno.ClientID %>').value;
            var add = document.getElementById('<%=txtPostaladdres.ClientID %>').value;
            var Session = document.getElementById('<%=ddlSession.ClientID %>').value;
            var bankname = document.getElementById('<%=txtbankbranch.ClientID %>').value;
            var ifsc = document.getElementById('<%=txtIfsccode.ClientID %>').value;
            var email = document.getElementById('<%=txtEmail.ClientID %>').value;

            if (rem == '') {
                alert('Please Enter Remuneration bill for!!');
                return false;
            }
            if (name == '') {
                alert('Please Enter Examiner name!!');
                return false;
            }
            if (mob == '') {
                alert('Please Enter Mobile Number!!');
                return false;
            }
            if (nod == '') {
                alert('Please Enter Department Name!!');
                return false;
            }
            if (sbiac == '') {
                alert('Please Enter SBI A/c Number!!');
                return false;
            }
            if (add == '') {
                alert('Please Enter Postal Address!!');
                return false;
            }
            if (Session == 0) {
                alert('Please Select Session!!');
                return false;
            }
            if (bankname == '') {
                alert('Please Enter Bank Name!!');
                return false;
            }
            if (ifsc == '') {
                alert('Please Enter IFSC code!!');
                return false;
            }
            if (email == '') {
                alert('Please Enter EmailID!!');
                return false;
            }

        }
        function onedisable(txt) {
            var a = document.getElementById("<%=txtRem.ClientID%>");
            var b = document.getElementById("<%=txtVar.ClientID%>");
            var a = txt.id;
            ///if (document.getElementById('ctl00_ContentPlaceHolder1_txtVar').readOnly != true && document.getElementById('ctl00_ContentPlaceHolder1_txtRem').readOnly != true) {
            if (txt.id == 'ctl00_ContentPlaceHolder1_txtRem') {
                //a.removeAttribute("readonly",0);
                document.getElementById('ctl00_ContentPlaceHolder1_txtRem').readOnly = 'true';
                document.getElementById('ctl00_ContentPlaceHolder1_txtRem').value = '2500';
                document.getElementById('ctl00_ContentPlaceHolder1_txtVar').value = '';
                document.getElementById('ctl00_ContentPlaceHolder1_txtVar').readOnly = 'true';
            }
            else {
                //b.removeAttribute("readonly",0);
                document.getElementById('ctl00_ContentPlaceHolder1_txtRem').value = '';
                document.getElementById('ctl00_ContentPlaceHolder1_txtRem').readOnly = 'true';
                document.getElementById('ctl00_ContentPlaceHolder1_txtVar').readOnly = '';
            }

            // }

        }
        function checkVar(txt) {
            var a = txt.id;
            var amount = txt.value;
            if (amount > 1500) {
                document.getElementById('ctl00_ContentPlaceHolder1_txtVar').value = '';
                alert('Varification Officer amount should not greater than 1500.');
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)
        function Cover(bottom, top, ignoreSize) {
            var location = Sys.UI.DomElement.getLocation(bottom);
            top.style.position = 'absolute';
            top.style.top = location.y + 'px';
            top.style.left = location.x + 'px';
            if (!ignoreSize) {
                top.style.height = bottom.offsetHeight + 'px';
                top.style.width = bottom.offsetWidth + 'px';
            }
        }
    </script>

</asp:Content>
