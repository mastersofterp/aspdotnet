<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ExitLevel.aspx.cs" Inherits="exitLevel" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updExitCriteria"
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

    <asp:UpdatePanel ID="updExitCriteria" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Exit Criteria</h3>
                        </div>
                        <div class="box-body">
                            <div class="nav-tabs-custom ml-md-2">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1">Exit Criteria</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2">Criteria Allotment</a>
                                    </li>
                                </ul>

                                <div class="tab-content" id="my-tab-content">
                                    <div id="tab_1" class="tab-pane active">
                                        <div>
                                            <asp:UpdatePanel ID="updExitCrt" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="col-12 mt-3">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Criteria Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtCriteriaName" runat="server" CssClass="form-control" onkeyup="validateAlphabet(this);"/>
                                                                <asp:RequiredFieldValidator ID="rfvtxtCriteriaName" runat="server" ValidationGroup="submit" ControlToValidate="txtCriteriaName" Display="None" ErrorMessage="Please Enter Criteria Name!!!" ></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-2 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Status</label>
                                                                </div>
                                                                <div class="switch form-inline">
                                                                    <input type="checkbox" id="rdActive" name="switch" checked runat="server"/>
                                                                    <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                                                </div>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>With Effect From</label>
                                                                </div>
                                                                <%--<asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" ValidationGroup="submit" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>--%>

                                                                <div class="input-group">
                                                                    <div class="input-group-addon">
                                                                        <i class="fa fa-calendar" id="date" runat="server"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="6" CssClass="form-control" ValidationGroup="submit" />
                                                                    <%-- <asp:Image ID="imgExamDate" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                        TargetControlID="txtToDate" PopupButtonID="date" Enabled="true" EnableViewState="true" />
                                                                    <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                                                                        Display="None" ErrorMessage="Please Enter Effective Date for Criteria!!!" SetFocusOnError="true"
                                                                        ValidationGroup="submit" />
                                                                    <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" MessageValidatorTip="true" DisplayMoney="Left"
                                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" MaskType="Date" />
                                                                </div>
                                                                <%--<asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ValidationGroup="submit" ControlToValidate="ddlBranch" InitialValue="0" Display="None" ErrorMessage="Please Select Branch!!!" ></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Level</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlLevels" runat="server" AppendDataBoundItems="true" ValidationGroup="submit" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">LEVEL 1</asp:ListItem>
                                                                    <asp:ListItem Value="2">LEVEL 2</asp:ListItem>
                                                                    <asp:ListItem Value="3">LEVEL 3</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlLevels" runat="server" ValidationGroup="submit" ControlToValidate="ddlLevels" InitialValue="0" Display="None" ErrorMessage="Please Select Levels!!!" ></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-2 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Credits</label>
                                                                </div>
                                                                <asp:TextBox ID="txtCredits" runat="server" MaxLength="2" CssClass="form-control" onkeyup="validateNumeric(this);" ValidationGroup="submit" />
                                                                <asp:RequiredFieldValidator ID="rfvtxtCredits" runat="server" ValidationGroup="submit" ControlToValidate="txtCredits" Display="None" ErrorMessage="Please Enter Credits!!!" ></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Exit Semester</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlExitSem" runat="server" AppendDataBoundItems="true" CssClass="form-control" ValidationGroup="submit" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlExitSem" runat="server" ValidationGroup="submit" ControlToValidate="ddlExitSem" InitialValue="0" Display="None" ErrorMessage="Please Select Exit Semester!!!" ></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-2 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Min CGPA </label>
                                                                </div>
                                                                <asp:TextBox ID="txtCGPA" runat="server" MaxLength="5" onkeyup="validateNumeric(this);" CssClass="form-control" />
                                                            </div>
                                                            <div class="form-group col-lg-2 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label></label>
                                                                </div>
                                                                <asp:Button ID="btnAdd" runat="server" Text="Add" ValidationGroup="submit" OnClick="btnAdd_Click" CssClass="btn btn-primary" TabIndex="9" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <asp:HiddenField ID="hdnCrt" runat="server" />
                                                    <div class="col-12 mt-3 mb-3">
                                                        <asp:Panel ID="pnlAdd" runat="server" Visible="false">
                                                            <asp:ListView ID="lvAdd" runat="server" Visible="true" >
                                                                <LayoutTemplate>
                                                                    <div class="table-responsive">
                                                                        <table id="example" class="table table-striped table-bordered display">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th>Edit</th>
                                                                                    <th>Criteria Name</th>
                                                                                    <th>Effect From</th>
                                                                                    <th>Levels</th>
                                                                                    <th>Credits</th>
                                                                                    <th>Exit Semester</th>
                                                                                    <th>Min CGPA</th>
                                                                                    <th>Status</th>
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
                                                                            <asp:ImageButton ID="imagebtn" runat="server" OnClick="imagebtn_Click" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("CRITERIA_NAME")%>' AlternateText="Edit Record" ToolTip="Edit Record" /></td>
                                                                        <td>
                                                                            <asp:Label ID="lblCrtN" runat="server" Text='<%# Eval("CRITERIA_NAME") %>' ></asp:Label></td> <%--ToolTip='<%#Eval("")%>'--%>
                                                                        <td>
                                                                            <asp:Label ID="lblEffect" runat="server" Text='<%# Eval("EFFECTIVE_DATE") %>' ></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblLevel" runat="server" Text='<%# Eval("LEVELS") %>' ></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' ></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblSem" runat="server" Text='<%# Eval("EXIT_SEMESTER") %>' ></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblCgpa" runat="server" Text='<%# Eval("CGPA") %>' ></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblStat" runat="server" Text='<%# Eval("STATUS") %>' ></asp:Label></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="BtnSubmit" runat="server" Text="Submit" OnClick="BtnSubmit_Click" CssClass="btn btn-primary" TabIndex="9" />
                                                        <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="BtnCancel_Click" TabIndex="10" />
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                                    </div>

                                                    <div class="col-12 mt-3">
                                                        <asp:Panel ID="pnlExitLevel" runat="server" Visible="false">
                                                            <asp:ListView ID="lvExitLevel" runat="server" Visible="true" >
                                                                <LayoutTemplate>
                                                                    <div class="table-responsive">
                                                                        <table id="exampleFinal" class="table table-striped table-bordered display">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th>Edit</th>
                                                                                    <th>Criteria Name</th>
                                                                                    <th>Status</th>
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
                                                                            <asp:ImageButton ID="imgbtn" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("EXIT_CRITERIA_NO")%>' OnClick="imgbtn_Click" AlternateText="Edit Record" ToolTip="Edit Record" /></td>
                                                                        <td>
                                                                            <asp:Label ID="lblCriteriaName" runat="server" Text='<%# Eval("CRITERIA_NAME") %>' ></asp:Label></td> <%--ToolTip='<%#Eval("")%>'--%>
                                                                        <td>
                                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUS") %>' ></asp:Label></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                        <%--<table id="" class="table table-striped table-bordered display" style="width: 100%;">
                                                            <thead>
                                                                <tr>
                                                                    <th>Edit</th>
                                                                    <th>Criteria Name</th>
                                                                    <th>Status</th>
                                                
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/edit.png" AlternateText="Edit Record" ToolTip="Edit Record" /></td>
                                                                    <td>Criteria For Cancellation</td>
                                                                    <td style="color:green !important;">Active</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>--%>
                                                    </div>
                                                 </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div id="tab_2" class="tab-pane fade">
                                        <div>
                                            <asp:UpdatePanel ID="updCrtAllotment" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="col-12 mt-3">
                                                        <div class="row">
                                                            <asp:HiddenField ID="hdnCrtId" runat="server" />
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>College & Scheme</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ValidationGroup="submit2" ControlToValidate="ddlCollege" InitialValue="0" Display="None" ErrorMessage="Please Select College & Scheme!!!" ></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Criteria Name</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlCriteriaName" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlCriteriaName" runat="server" ValidationGroup="submit2" ControlToValidate="ddlCriteriaName" InitialValue="0" Display="None" ErrorMessage="Please Select Criteria Name!!!" ></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer mt-3">
                                                        <asp:Button ID="btnSubmit1" runat="server" Text="Submit" OnClick="btnSubmit1_Click" ValidationGroup="submit2" CssClass="btn btn-primary"/>
                                                        <asp:Button ID="btnCancel1" runat="server" Text="Cancel" OnClick="btnCancel1_Click" CssClass="btn btn-warning" />
                                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" ValidationGroup="submit2" />
                                                    </div>

                                                    <div class="col-12 mt-3">
                                                        <asp:Panel ID="pnlCrtAllot" runat="server" Visible="false">
                                                            <asp:ListView ID="lvCrtAllot" runat="server" Visible="true" >
                                                                <LayoutTemplate>
                                                                    <div class="table-responsive">
                                                                        <table id="exampleCrtAllot" class="table table-striped table-bordered display">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th style="width:5%">Edit</th>
                                                                                    <th>College & Scheme</th>
                                                                                    <th>Criteria Name</th>
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
                                                                        <td style="width:5%">
                                                                            <asp:ImageButton ID="imagebtnAllot" runat="server" OnClick="imagebtnAllot_Click" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("EXIT_CRT_ALLOT_NO")%>' AlternateText="Edit Record" ToolTip="Edit Record" /></td>
                                                                        <td>
                                                                            <asp:Label ID="lblClgScheme" runat="server" Text='<%# Eval("COL_SCHEME_NAME") %>' ></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblCrtN" runat="server" Text='<%# Eval("CRITERIA_NAME") %>' ></asp:Label></td> <%--ToolTip='<%#Eval("")%>'--%>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>

                                                        <%--<table id="example" class="table table-striped table-bordered display" style="width: 100%;">
                                                            <thead>
                                                                <tr>
                                                                    <th>Edit</th>
                                                                    <th>College & Scheme</th>
                                                                    <th>Criteria Name</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton4" class="newAddNew Tab" runat="server" ImageUrl="~/Images/edit.png" AlternateText="Edit Record" ToolTip="Edit Record" /></td>
                                                                    <td>School of Design - Computer Science</td>
                                                                    <td>Criteria For Cancellation </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>--%>
                                                    </div>
                                                 </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        $(document).ready(function () {
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

    <script type="text/javascript" language="javascript">

            function validateNumeric(txt) {
                if (isNaN(txt.value)) {
                    txt.value = txt.value.substring(0, (txt.value.length) - 1);
                    txt.value = '';
                    txt.focus = true;
                    alert("Only Numeric Characters allowed !");
                    return false;
                }
                else
                    return true;
            }

            function validateAlphabet(txt) {
                var expAlphabet = /^[A-Za-z ]+$/;
                if (txt.value.search(expAlphabet) == -1) {
                    txt.value = txt.value.substring(0, (txt.value.length) - 1);
                    txt.value = '';
                    txt.focus = true;
                    alert("Only Alphabets allowed!");
                    return false;
                }
                //else
                //    return true;

                var textbox = document.getElementById('<%= txtCriteriaName.ClientID %>');
                var text = textbox.value;

                // Remove extra spaces
                text = text.replace(/\s+/g, " ");

                // Restrict to only one space
                if (text.indexOf(" ") !== -1 && text.lastIndexOf(" ") !== text.indexOf(" ")) {
                    var firstSpaceIndex = text.indexOf(" ");
                    text = text.substring(0, firstSpaceIndex) + text.substring(firstSpaceIndex + 1);
                }

                // Update the textbox value
                textbox.value = text;

                return true;
            }

    </script>

</asp:Content>

