
<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="GraceRuleConfiguration.aspx.cs" Inherits="ACADEMIC_EXAMINATION_Exam_Configue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<asp:UpdatePanel ID="mainpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Grace Rule Configuration</h3>--%>
                             <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>
                          <div class="box-body">
                            <div class="col-12">
                              <div class="row">
                                  <div class="form-group col-lg-4 col-md-3 col-12">
                                      <sup>* </sup>
                                      <%--<label for="ddlGrace" style="font-size: small;">Grace Category</label>--%>
                                      <label for="ddlGrace" style="font-size: small;"><asp:Label ID="lblDYGraceCategory" runat="server" Font-Bold="true"></asp:Label></label>
                                      <asp:DropDownList ID="ddlGrace" runat="server" TabIndex="1" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                          ValidationGroup="Save" OnSelectedIndexChanged="ddlGrace_SelectedIndexChanged" data-select2-enable="true">
                                          <asp:ListItem Value="0">Please Select</asp:ListItem>
                                      </asp:DropDownList>
                                  </div>
                                  <div class="form-group col-lg-4 col-md-3 col-12">
                                      <span>
                                          <%--<label for="chk_GMaxMarks" style="font-size: small;" id="rdActivetimeslot">Grace Max Marks</label>--%>
                                          <label for="chk_GMaxMarks" style="font-size: small;" id="rdActivetimeslot"><asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label></label>
                                      </span>
                                      <div class="custom-control custom-switch">
                                          <input type="checkbox" class="custom-control-input" id="chk_GMaxMarks" name="rdGMarks" onclick="return SetStat(this);" TabIndex="2">
                                          <label class="custom-control-label" for="chk_GMaxMarks"></label>
                                          <span id="lblGMarks"></span>
                                          <asp:HiddenField ID="hdfGMaxMarks" runat="server" ClientIDMode="Static" />
                                      </div>
                                      <div id="divGrace">
                                          <div class="label-dynamic"></div>
                                          <sup>* </sup>
                                          <%--<label for="txtGMaxMarks">Enter Grace Marks</label>--%>
                                          <label for="txtGMaxMarks"><asp:Label ID="lblDYtxtEnterGraceMarks" runat="server" Font-Bold="true"></asp:Label></label>
                                          <asp:TextBox runat="server" ID="txtGMaxMarks" TabIndex="3" CssClass="form-control number-only" 
                                               ToolTip="Please Enter Grace Marks" MaxLength="5" AutoComplete="off" 
                                               placeholder="Enter Grace Marks"></asp:TextBox>
                                      </div>
                                  </div>
                                  <div class="form-group col-lg-4 col-md-3 col-12">
                                      <span>
                                          <%--<label for="chk_MCourse" style="font-size: small;">Max Course</label>--%>
                                          <label for="chk_MCourse" style="font-size: small;"><asp:Label ID="lblDYMaxCourse" runat="server" Font-Bold="true"></asp:Label></label>
                                      </span>

                                      <div class="custom-control custom-switch">
                                          <input type="checkbox" class="custom-control-input" id="chk_MCourse" onclick="return SetStat(this);" TabIndex="4">
                                          <label class="custom-control-label" for="chk_MCourse"></label>
                                          <span id="lblMCourse"></span>
                                          <asp:HiddenField ID="hdfMCourse" runat="server" ClientIDMode="Static" />
                                      </div>
                                      <div id="divMCourse">
                                          <div class="label-dynamic"></div>
                                          <sup>* </sup>
                                          <%--<label for="txtMCourse">Enter Maximum Course</label>--%>
                                          <label for="txtMCourse"><asp:Label ID="lblDYtxtEnterMaximumCourse" runat="server" Font-Bold="true"></asp:Label></label>

                                          <asp:TextBox runat="server" ID="txtMCourse" TabIndex="5" CssClass="form-control number-only" 
                                              ToolTip="Please Enter Maximum Course" MaxLength="5" 
                                               placeholder="Enter Maximum Course"></asp:TextBox>
                                      </div>
                                  </div>
                                  <div class="form-group col-lg-4 col-md-3 col-12">
                                      <span>
                                          <%--<label for="chk_PGMarks" style="font-size: small;">Per Course Grace Mark</label>--%>
                                          <label for="chk_PGMarks" style="font-size: small;"><asp:Label ID="lblDYPerCourseGraceMark" runat="server" Font-Bold="true"></asp:Label></label>
                                      </span>
                                      <div class="custom-control custom-switch">
                                          <input type="checkbox" class="custom-control-input" id="chk_PGMarks" onclick="return SetStat(this);" TabIndex="6">
                                          <label class="custom-control-label" for="chk_PGMarks"></label>
                                          <span id="lblPGMarks"></span>
                                          <asp:HiddenField ID="hdfPGMarks" runat="server" ClientIDMode="Static" />
                                      </div>
                                      <div id="divPGMarks">
                                          <div class="label-dynamic"></div>
                                          <sup>* </sup>
                                         <%-- <label for="txtPGMarks">Enter Per Course Grace Marks</label>--%>
                                           <label for="txtPGMarks"><asp:Label ID="lblDYtxtPCGMarks" runat="server" Font-Bold="true"></asp:Label></label>
                                          <asp:TextBox runat="server" ID="txtPGMarks" TabIndex="7" CssClass="form-control number-only"
                                               ToolTip="Please Enter Per Course Grace Marks" MaxLength="5" 
                                               placeholder="Enter per Course Grace Marks"></asp:TextBox>
                                      </div>
                                  </div>
                                  <div class="form-group col-lg-4 col-md-3 col-12">
                                      <span>
                                          <%--<label for="chk_PTMarks" style="font-size: small;">Percentage of Total Max Marks</label>--%>
                                          <label for="chk_PTMarks" style="font-size: small;"><asp:Label ID="lblDYPercentageofTotalMaxMarks" runat="server" Font-Bold="true"></asp:Label></label>
                                      </span>
                                      <div class="custom-control custom-switch">
                                          <input type="checkbox" class="custom-control-input" id="chk_PTMarks" onclick="return SetStat(this);" TabIndex="8">
                                          <label class="custom-control-label" for="chk_PTMarks"></label>
                                          <span id="lblPTMarks"></span>
                                          <asp:HiddenField ID="hdfPTMarks" runat="server" ClientIDMode="Static" />
                                      </div>
                                      <div id="divPTMarks">
                                          <div class="label-dynamic"></div>
                                          <sup>* </sup>
                                          <%--<label for ="txtPTMarks">Enter Percentage of Total Maximum Marks</label>--%>
                                          <label for ="txtPTMarks"><asp:Label ID="lblDYtxtPerTMaxMarks" runat="server" Font-Bold="true"></asp:Label></label>
                                          <asp:TextBox runat="server" ID="txtPTMarks" TabIndex="9" CssClass="form-control number-only"
                                               ToolTip="Please Enter Percentage of total Maximum Marks" MaxLength="6
                                              " 
                                               placeholder="Enter Percentage of total Maximum Marks"></asp:TextBox>
                                      </div>
                                  </div>
                                  <div class="form-group col-lg-4 col-md-3 col-12">
                                       <span>
                                          <%--<label for="chk_active" style="font-size: small;">Status</label>--%>
                                           <label for="chk_active" style="font-size: small;"><asp:Label ID="lblDYStatus" runat="server" Font-Bold="true"></asp:Label></label>
                                      </span>
                                      <div class="custom-control custom-switch">
                                          <input type="checkbox" class="custom-control-input" id="chk_active" onclick="return SetStat(this);" tabindex="10">
                                          <label class="custom-control-label" for="chk_active"></label>
                                          <span id="lblActive"></span>
                                          <asp:HiddenField ID="hdfActive" runat="server" ClientIDMode="Static" />
                                      </div>
                                  </div>
                              </div>
                          </div>
                          <div class="col-12 text-center mt-3">
                              <asp:Button ID="btnSave" runat="server" ToolTip="Submit" Text="Save" CssClass="btn btn-primary" TabIndex="11" OnClick="btnSave_Click"  OnClientClick="return Validate() && SetStat(this);"/>
                              <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="12" OnClick="btnCancel_Click" />
                          </div>
                          <div class="col-md-12">
                              <br />
                              <asp:ListView ID="lvGraceRule" Visible="false" runat="server">
                                  <LayoutTemplate>
                                      <div id="demo-grid">
                                          <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                              <thead class="bg-light-blue">
                                                  <tr>
                                                      <th>Category </th>
                                                      <th>Grace Max Marks </th>
                                                      <th>Max Course</th>
                                                      <th>Per Course Grace Mark</th>
                                                      <th>Percentage of total Max Marks</th>
                                                      <th>Status</th>
                                                      <th>Action</th>
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
                                              <asp:Label ID="lblGrace" runat="server" Text='<%# Eval("GRACE_NAME")%>'></asp:Label>
                                          </td>
                                          <td>
                                              <asp:Label ID="lblGMarks" runat="server" CssClass='<%# Eval("GRACE_MARKS")%>' Text='<%# Eval("GRACE_MARKS")%>' ForeColor='<%# Eval("GRACE_MARKS").ToString().Equals("YES")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                          </td>
                                          <td>
                                              <asp:Label ID="lblMaxMarks" runat="server" CssClass='<%# Eval("MAX_GRACE_MARKS")%>' Text='<%# Eval("MAX_GRACE_MARKS")%>' ForeColor='<%# Eval("MAX_GRACE_MARKS").ToString().Equals("YES")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                          </td>
                                          <td>
                                              <asp:Label ID="lblPGMarks" runat="server" CssClass='<%# Eval("PER_COURSE_GRACE_MARKS")%>' Text='<%# Eval("PER_COURSE_GRACE_MARKS")%>' ForeColor='<%# Eval("PER_COURSE_GRACE_MARKS").ToString().Equals("YES")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                          </td>
                                          <td>
                                              <asp:Label ID="lblPTMarks" runat="server" CssClass='<%# Eval("PERCENT_OF_MAX_MARKS")%>' Text='<%# Eval("PERCENT_OF_MAX_MARKS")%>' ForeColor='<%# Eval("PERCENT_OF_MAX_MARKS").ToString().Equals("YES")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                          </td>
                                          <td>
                                              <asp:Label ID="lblIsActive" runat="server" CssClass='<%# Eval("STATUS")%>' Text='<%# Eval("STATUS")%>' ForeColor='<%# Eval("STATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                          </td>
                                          <td>
                                              <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                  CommandArgument='<%# Eval("SRNO")%>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" OnClientClick="return SetStat(); "
                                                                    TabIndex="13" />
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

    <%--<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>--%>
<script>
        //used to hide textbox
        $(document).ready(function () {
            SetStat();
            $('#ddlGrace').click(function (event) {
                //debugger;
                event.stopPropagation();
                SetStat();
            });
        });
        function SetStat() {
            //debugger;
                   var gmarks = document.getElementById("chk_GMaxMarks");
                var maxcourse = document.getElementById("chk_MCourse");
                var pergmarks = document.getElementById("chk_PGMarks");
            var pertotalmarks = document.getElementById("chk_PTMarks");
                   var active = document.getElementById("chk_active");

            $('#divGrace').show();
            $('#divMCourse').show();
            $('#divPGMarks').show();
            $('#divPTMarks').show();

            if (gmarks.checked) {
                var data = "Yes";
                $('#hdfGMaxMarks').val(true);
                $('#lblGMarks').text(data);
                $('#divGrace').removeClass('d-none');
            }
            else
            {
                var data = "No";
                $('#hdfGMaxMarks').val(false);
                $('#lblGMarks').text(data);
                $('#divGrace').addClass('d-none');
                
            }
            if (maxcourse.checked)
            {
                var data = "Yes";
                $('#hdfMCourse').val(true);
                $('#lblMCourse').text(data);
                $('#divMCourse').removeClass('d-none');
            }
            else
            {
                var data = "No";
                $('#hdfMCourse').val(false);
                $('#lblMCourse').text(data);
                $('#divMCourse').addClass('d-none');
            }
            if (pergmarks.checked)
            {
                var data = "Yes";
                $('#hdfPGMarks').val(true);
                $('#lblPGMarks').text(data);
                $('#divPGMarks').removeClass('d-none');
            }
            else
            {
                var data = "No";
                $('#hdfPGMarks').val(false);
                $('#lblPGMarks').text(data);
                $('#divPGMarks').addClass('d-none');
            }
            if (pertotalmarks.checked)
            {
                var data = "Yes";
                $('#hdfPTMarks').val(true);
                $('#lblPTMarks').text(data);
                $('#divPTMarks').removeClass('d-none');
            }
            else
            {
                var data = "No";
                $('#hdfPTMarks').val(false);
                $('#lblPTMarks').text(data);
                $('#divPTMarks').addClass('d-none');
                $("txtPTMarks").attr("value", "true")
            }
            if (active.checked)
            {
                var data = "Active";
                $('#hdfActive').val(true);
                $('#lblActive').text(data);
            }
            else
            {
                var data = "Check if Active";
                $('#hdfActive').val(false);
                $('#lblActive').text(data);
            }
        }
    </script>

<script type="text/javascript">
        $(document).ready(function () {
            window.history.replaceState('', '', window.location.href) // it prevent page refresh to firing the event again
        })
    </script>

<script>
        //status
        function Active(val) {
            $('#chk_active').prop('checked', val);
        }
</script>

<script>
    //set checkbox onclicking edit btn
    function setCheckbox(id, val) {
        $('#' + id).prop('checked', val);
    }
    </script>

<script>
        //validation
        function Validate() {
            var txtGMaxMarks = document.getElementById('<%= txtGMaxMarks.ClientID %>');
            var txtMCourse = document.getElementById('<%= txtMCourse.ClientID %>');
            var txtPGMarks = document.getElementById('<%= txtPGMarks.ClientID %>');
            var txtPTMarks = document.getElementById('<%= txtPTMarks.ClientID %>');
            var ddlGrace = document.getElementById('<%= ddlGrace.ClientID %>');

            // Check if the textboxes are visible
            var divGraceVisible = $('#divGrace').is(':visible');
            var divMCourseVisible = $('#divMCourse').is(':visible');
            var divPGMarksVisible = $('#divPGMarks').is(':visible');
            var divPTMarksVisible = $('#divPTMarks').is(':visible');

            if (ddlGrace.selectedIndex === 0) {
                alert("Please select Grace Category.");
                ddlGrace.focus();
                return false;
            }
            if (divGraceVisible && (txtGMaxMarks.value.trim() === ""))
            {
                alert("Please Enter Grace Marks.");
                txtGMaxMarks.focus();
                return false;
            }
            if (divMCourseVisible && (txtMCourse.value.trim() === ""))
            {
                alert("Please Enter Max Course.");
                txtMCourse.focus();
                return false;
            }
            if (divPGMarksVisible && (txtPGMarks.value.trim() === ""))
            {
                alert("Please Enter Per Course Grace Marks.");
                txtPGMarks.focus();
                return false;
            }
            if (divPTMarksVisible && (txtPTMarks.value.trim() === ""))
            {
                alert("Please Enter Percentage of total Max Marks.");
                txtPTMarks.focus();
                return false;
            }
            return true;
        }
    </script>

<script>
    //validation to Enter only number
    $(document).ready(function () {
        $('.number-only').keypress(function (event) {
            if (!(event.key === 'Backspace' || (event.key >= '0' && event.key <= '9'))){
                alert("Please enter numbers only.");
                event.preventDefault();
            }
            var newValue = this.value + event.key;
            if (newValue < 0 || newValue > 10) {
                alert("Marks should be between 5 and 10.");
                event.preventDefault();
            }
        });
    });
</script>

</asp:Content>

